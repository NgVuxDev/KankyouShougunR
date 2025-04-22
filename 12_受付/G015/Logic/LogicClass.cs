// $Id: LogicClass.cs 55987 2015-07-17 05:05:14Z hoangvu@e-mall.co.jp $

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.ContenaShitei.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using Seasar.Framework.Exceptions;
using Seasar.Dao;
using System.Drawing;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using r_framework.Configuration;
using GrapeCity.Win.MultiRow;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// EnterかTabボタンが押下されたかどうかの判定フラグ
        /// </summary>
        internal bool pressedEnterOrTab = false;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeaderForm headerForm;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private T_UKETSUKE_SS_ENTRYDao daoUketsukeEntry;

        /// <summary>
        /// 受付（収集）明細のDao
        /// </summary>
        private T_UKETSUKE_SS_DETAILDao daoUketsukeDetail;

        /// <summary>
        /// コンテナ稼働予定のDao
        /// </summary>
        private T_CONTENA_RESERVEDao daoContena;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// IM_HINMEIDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// IM_DENPYOU_KBNDao
        /// </summary>
        private IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// IM_UNITDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SEIKYUUDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// IM_SHARYOUDao(車輌Dao)
        /// </summary>
        private IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// IM_SHASHUDao(車種Dao)
        /// </summary>
        private IM_SHASHUDao shashuDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// IM_KYOTENDao(拠点Dao)
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
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
        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　end

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場のDao
        /// </summary>
        internal IM_SMS_RECEIVER_LINK_GENBADao smsReceiverLinkGenbaDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 受付（収集）入力検索結果
        /// </summary>
        internal DataTable dtUketsukeEntry;

        /// <summary>
        /// Inxs Pickup request data
        /// </summary>
        internal DataTable dtPickupRequestInxs = null;

        /// <summary>
        /// 修正前配車状況
        /// </summary>
        private short beforeHaishaJoukyou = -1;

        /// <summary>
        /// 受付（収集）明細検索結果
        /// </summary>
        private DataTable dtUketsukeDetail;

        /// <summary>
        /// 明細DataGridView
        /// </summary>
        private DataGridView dgvDetail;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>
        /// HeaderFormのクリアコントロール名一覧
        /// </summary>
        private string[] clearHeaderControlNames = { "CreateUser", "CreateDate", "LastUpdateUser", "LastUpdateDate" };

        /// <summary>
        /// UIFormのクリアコントロール名一覧
        /// </summary>
        private string[] clearUiFormControlNames = { "HAISHA_JOKYO_CD", "HAISHA_JOKYO_NAME", "HAISHA_SHURUI_CD", "HAISHA_SHURUI_NAME", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", "HAISHA_SIJISHO_FLG", "UKETSUKE_DATE", "UKETSUKE_DATE_HOUR", "UKETSUKE_DATE_MINUTE", "UKETSUKE_NUMBER", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "GYOUSHA_CD", "GYOUSHA_NAME", "GYOSHA_TEL", "GENBA_CD", "GENBA_NAME", "GENBA_ADDRESS1", "GENBA_ADDRESS2", "GENBA_TEL", "TANTOSHA_NAME", "TANTOSHA_TEL", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME", "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME", "UKETSUKE_BIKOU1", "UKETSUKE_BIKOU2", "UKETSUKE_BIKOU3", "SAGYOU_DATE", "NIOROSHI_DATE", "SAGYOU_DATE_BEGIN", "SAGYOU_DATE_END", "GENCHAKU_TIME_CD", "GENCHAKU_TIME_NAME", "GENCHAKU_TIME_HOUR", "GENCHAKU_TIME_MINUTE", "SAGYOU_TIME_HOUR", "SAGYOU_TIME_MINUTE", "SAGYOU_TIME_BEGIN_HOUR", "SAGYOU_TIME_BEGIN_MINUTE", "SAGYOU_TIME_END_HOUR", "SAGYOU_TIME_END_MINUTE", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", "SHASHU_CD", "SHASHU_NAME", "SHASHU_DAISU_NUMBER", "SHASHU_DAISU_TOTAL", "SHARYOU_CD", "SHARYOU_NAME", "UNTENSHA_CD", "UNTENSHA_NAME", "HOJOIN_CD", "HOJOIN_NAME", "MANIFEST_SHURUI_CD", "MANIFEST_SHURUI_NAME_RYAKU", "MANIFEST_TEHAI_CD", "MANIFEST_TEHAI_NAME_RYAKU", "CONTENA_SOUSA_CD", "CONTENA_SOUSA_NAME_RYAKU", "SETTI_DAISUU", "HIKIAGE_DAISUU", "COURSE_KUMIKOMI_CD", "COURSE_KUMIKOMI_NAME", "COURSE_NAME_CD", "COURSE_NAME_RYAKU", "UNTENSHA_SIJIJIKOU1", "UNTENSHA_SIJIJIKOU2", "UNTENSHA_SIJIJIKOU3", "URIAGE_TOTAL", "SHIHARAI_TOTAL", "SASHIHIKIGAKU" };

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD", "KYOTEN_NAME_RYAKU", "CreateUser", "CreateDate", "LastUpdateUser", "LastUpdateDate" };

        /// <summary>
        /// UIFormの入力コントロール名一覧(確定状態の入力できるコントロールを除く)
        /// </summary>
        private string[] inputUiFormControlNames = { "HAISHA_JOKYO_CD", "HAISHA_JOKYO_NAME", "HAISHA_SHURUI_CD", "HAISHA_SHURUI_NAME", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", "HAISHA_SIJISHO_FLG", "UKETSUKE_DATE", "UKETSUKE_DATE_HOUR", "UKETSUKE_DATE_MINUTE", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "TORIHIKISAKI_SEARCH_BUTTON", "GYOUSHA_CD", "GYOUSHA_NAME", "GYOUSHA_SEARCH_BUTTON", "GYOSHA_TEL", "GENBA_CD", "GENBA_NAME", "GENBA_SEARCH_BUTTON", "GENBA_ADDRESS1", "GENBA_ADDRESS2", "GENBA_TEL", "TANTOSHA_NAME", "TANTOSHA_TEL", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME", "NIOROSHI_GYOUSHA_SEARCH_BUTTON", "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME", "NIOROSHI_GENBA_SEARCH_BUTTON", "UKETSUKE_BIKOU1", "UKETSUKE_BIKOU2", "UKETSUKE_BIKOU3", "SAGYOU_DATE", "NIOROSHI_DATE", "SAGYOU_DATE_BEGIN", "SAGYOU_DATE_END", "GENCHAKU_TIME_CD", "GENCHAKU_TIME_NAME", "GENCHAKU_TIME_HOUR", "GENCHAKU_TIME_MINUTE", "SAGYOU_TIME_HOUR", "SAGYOU_TIME_MINUTE", "SAGYOU_TIME_BEGIN_HOUR", "SAGYOU_TIME_BEGIN_MINUTE", "SAGYOU_TIME_END_HOUR", "SAGYOU_TIME_END_MINUTE", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", "UNPAN_GYOUSHA_SEARCH_BUTTON", "SHASHU_CD", "SHASHU_NAME", "SHASHU_DAISU_NUMBER", "SHASHU_DAISU_TOTAL", "SHARYOU_CD", "SHARYOU_NAME", "UNTENSHA_CD", "UNTENSHA_NAME", "HOJOIN_CD", "HOJOIN_NAME", "MANIFEST_SHURUI_CD", "MANIFEST_SHURUI_NAME_RYAKU", "MANIFEST_TEHAI_CD", "MANIFEST_TEHAI_NAME_RYAKU", "CONTENA_SOUSA_CD", "CONTENA_SOUSA_NAME_RYAKU", "SETTI_DAISUU", "HIKIAGE_DAISUU", "COURSE_KUMIKOMI_CD", "COURSE_KUMIKOMI_NAME", "COURSE_NAME_CD", "COURSE_NAME_RYAKU", "UNTENSHA_SIJIJIKOU1", "UNTENSHA_SIJIJIKOU2", "UNTENSHA_SIJIJIKOU3", "URIAGE_TOTAL", "SHIHARAI_TOTAL", "SASHIHIKIGAKU" };

        /// <summary>
        /// Detailの入力コントロール名一覧
        /// </summary>
        private string[] inputDetailControlNames = { "HINMEI_CD", "HINMEI_SEARCH_BUTTON", "SUURYOU", "UNIT_CD", "TANKA", "MEISAI_BIKOU", "HINMEI_KINGAKU" };

        /// <summary>
        /// 確定状態の入力できるコントロール名一覧
        /// </summary>
        private List<string> kakuteiControlNames = new List<string> { "UKETSUKE_NUMBER", "UKETSUKE_PREVIOUS_BUTTON", "UKETSUKE_NEXT_BUTTON", "DAISUU_PREVIOUS_BUTTON", "DAISUU_NEXT_BUTTON" };

        /// <summary>
        /// 受付収集入力Entityを格納
        /// </summary>
        private List<T_UKETSUKE_SS_ENTRY> insEntryEntityList = new List<T_UKETSUKE_SS_ENTRY>();

        /// <summary>
        /// 受付収集明細Entityを格納
        /// </summary>
        private List<T_UKETSUKE_SS_DETAIL> insDetailEntityList = new List<T_UKETSUKE_SS_DETAIL>();

        /// <summary>
        /// コンテナ指定Entityを格納
        /// </summary>
        private List<T_CONTENA_RESERVE> insContenaEntityList = new List<T_CONTENA_RESERVE>();

        /// <summary>
        /// 受付収集入力を削除Entity
        /// </summary>
        internal T_UKETSUKE_SS_ENTRY delEntryEntity;

        /// <summary>
        /// コンテナ(コンテナ指定画面との受取で使う)
        /// </summary>
        internal List<T_CONTENA_RESERVE> listContenaParam = new List<T_CONTENA_RESERVE>();

        /// <summary>
        /// 修正前コンテナ
        /// </summary>
        private List<T_CONTENA_RESERVE> beforeListContenaParam = new List<T_CONTENA_RESERVE>();

        /// <summary>
        /// コンテナ稼働予定検索結果(初期表示、論理削除時使う)
        /// </summary>
        private T_CONTENA_RESERVE[] arrContenaReserve;

        /// <summary>
        /// コンテナのDao
        /// </summary>
        private IM_CONTENADao daoMasterContena;

        /// <summary>
        /// コンテナマスタEntityを格納
        /// </summary>
        private List<M_CONTENA> contenaMasterList;

        /// <summary>
        /// 車種台数
        /// </summary>
        internal int groupNumber;

        /// <summary>
        /// 設置台数
        /// </summary>
        internal int settiDaisu = 0;

        /// <summary>
        /// 引揚台数
        /// </summary>
        internal int hikiageDaisu = 0;

        /// <summary>
        /// システム設定．売上支払情報差引基準
        /// </summary>
        internal String systemUrShCalcBaseKbn;

        /// <summary>
        /// 請求端数CD
        /// </summary>
        private string seikyuuHasuuCD;

        /// <summary>
        /// 支払端数CD
        /// </summary>
        private string siharaiHasuuCD;

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>
        /// 伝票区分全件
        /// </summary>
        Dictionary<short, M_DENPYOU_KBN> denpyouKbnDictionary = new Dictionary<short, M_DENPYOU_KBN>();

        /// <summary>
        /// 単位区分全件
        /// </summary>
        Dictionary<short, M_UNIT> unitDictionary = new Dictionary<short, M_UNIT>();

        /// <summary>
        /// 取得した社員エンティティを保持する
        /// </summary>
        private List<M_SHAIN> shainList = new List<M_SHAIN>();

        /// <summary>
        /// 取得した取引先エンティティを保持する
        /// </summary>
        private List<M_TORIHIKISAKI> torihikisakiList = new List<M_TORIHIKISAKI>();

        /// <summary>
        /// 取得した業者エンティティを保持する
        /// </summary>
        private List<M_GYOUSHA> gyoushaList = new List<M_GYOUSHA>();

        /// <summary>
        /// 取得した現場エンティティを保持する
        /// </summary>
        private List<M_GENBA> genbaList = new List<M_GENBA>();

        /// <summary>
        /// 取得した品名エンティティを保持する
        /// </summary>
        private List<M_HINMEI> hinmeiList = new List<M_HINMEI>();

        /// <summary>
        /// 取得したマニフェスト種類エンティティを保持する
        /// </summary>
        private List<M_MANIFEST_SHURUI> manifestShuruiList = new List<M_MANIFEST_SHURUI>();

        /// <summary>
        /// 取得したマニフェスト手配エンティティを保持する
        /// </summary>
        private List<M_MANIFEST_TEHAI> manifestTehaiList = new List<M_MANIFEST_TEHAI>();

        /// <summary>
        /// 車輌CDを保持する
        /// </summary>
        internal string sharyouCd;

        /// <summary>
        /// 配車状況で使用するデータ
        /// </summary>
        private DataTable haishaJokyoDataTable;

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// 個別品名単価DAO
        /// </summary>
        private IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao;

        /// <summary>
        /// モバイル連携用の受付番号
        /// </summary>
        internal string Renkei_UketsukeNumber;

        /// <summary>
        /// モバイル連携用データテーブル
        /// </summary>
        private DataTable ResultTable;

        private int MobileTryTime;

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

        // MAILAN #158988 START
        internal bool isTankaMessageShown = false;
        internal bool isContinueCheck = true;
        // MAILAN #158988 END

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        internal LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;
                // DataGridViewコントロール
                this.dgvDetail = this.form.dgvDetail;
                // ControlUtility
                this.controlUtil = new ControlUtility();
                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();
                // DTO
                this.dto = new DTOClass();
                // 受付（収集）入力のDao
                this.daoUketsukeEntry = DaoInitUtility.GetComponent<T_UKETSUKE_SS_ENTRYDao>();
                // 受付（収集）明細のDao
                this.daoUketsukeDetail = DaoInitUtility.GetComponent<T_UKETSUKE_SS_DETAILDao>();
                // コンテナ稼働予定のDao
                this.daoContena = DaoInitUtility.GetComponent<T_CONTENA_RESERVEDao>();

                // 共通Dao
                // システム情報Dao
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                //this.eigyouTantoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_EIGYOU_TANTOUSHADao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
                this.hinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HINMEIDao>();
                this.denpyouKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_DENPYOU_KBNDao>();
                this.unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();
                this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao>();
                this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
                this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
                this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
                /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
                this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
                this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
                this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
                /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　end
                this.daoMasterContena = DaoInitUtility.GetComponent<IM_CONTENADao>();
                this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
                this.kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEI_TANKADao>();
                //モバイル連携
                this.TmobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
                this.TmobisyoCTNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_CONTENADao>();
                this.TmobisyoRtDTLDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_DTLDao>();
                this.TmobisyoRtHNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_HANNYUUDao>();
                // ｼｮｰﾄﾒｯｾｰｼﾞ
                this.smsReceiverLinkGenbaDao = DaoInitUtility.GetComponent<IM_SMS_RECEIVER_LINK_GENBADao>();

                this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();

                // 伝票区分一覧全件取得
                var denpyous = this.GetAllDenpyouKbn();
                foreach (var denpyou in denpyous)
                {
                    denpyouKbnDictionary.Add((short)denpyou.DENPYOU_KBN_CD, denpyou);
                }
                // 単位一覧全件取得
                var units = this.GetAllUnit();
                foreach (var unit in units)
                {
                    unitDictionary.Add((short)unit.UNIT_CD, unit);
                }

                this.sharyouCd = String.Empty;
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

        #region 初期処理

        #region 画面初期化
        /// <summary>
        /// 画面初期化
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
                headerForm = (UIHeaderForm)parentForm.headerForm;
                // DataGrid行の高さ設定
                this.dgvDetail.RowTemplate.Height = 22;

                // システム情報を取得
                this.GetSysInfoInit();

                // イベントの初期化処理
                this.EventInit();

                // 新規モードの場合
                if (this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 拠点初期値設定
                    this.SetInitKyoten();
                }

                // 画面初期表示処理
                bool catchErr = true;
                this.DisplayInit(out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }

                // 配車状況ポップアップ初期表示処理
                this.HaishaJokyoPopUpDataInit();

                // 配車種類ポップアップ初期表示処理
                this.HaishaShuruiPopUpDataInit();

                // コース組込ポップアップ初期表示処理
                this.CourseKumikomiPopUpDataInit();

                this.RirekeShow(); //CongBinh 20210713 #152801
            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

                // ﾎﾞﾀﾝEnabled制御
                var controlUtil = new ControlUtility();
                foreach (var button in buttonSetting)
                {
                    var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                    if (cont != null && !string.IsNullOrEmpty(cont.Text))
                    {
                        cont.Enabled = true;
                    }
                }

                // 処理No　Enabled制御
                this.parentForm.txb_process.Enabled = true;
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

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {

            ButtonSetting[] returnVal = null;

            try
            {
                LogUtility.DebugMethodStart();

                var thisAssembly = Assembly.GetExecutingAssembly();
                var buttonSetting = new ButtonSetting();
                returnVal = buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
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

        #region 拠点初期値設定

        #region 拠点設定
        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            LogUtility.DebugMethodStart();

            // 拠点
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, "拠点CD");
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text.ToString()))
            {
                this.headerForm.KYOTEN_CD.Text = this.headerForm.KYOTEN_CD.Text.ToString().PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                this.CheckKyotenCd();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ユーザー定義情報取得処理
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

        #region ヘッダーの拠点CDの存在チェック
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyoteCd))
                {
                    return;
                }

                M_KYOTEN keyEntity = new M_KYOTEN();
                keyEntity.KYOTEN_CD = kyoteCd;
                var kyotens = this.kyotenDao.GetAllValidData(keyEntity);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E020", "拠点");
                    //this.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
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
            this.haishaJokyoDataTable.TableName = ConstClass.POPUP_TITLE_HAISHA_JOKYO;

            // 列定義
            this.haishaJokyoDataTable.Columns.Add(ConstClass.COLUMN_HAISHA_JOKYO_CD, typeof(String));
            this.haishaJokyoDataTable.Columns.Add(ConstClass.COLUMN_HAISHA_JOKYO_NAME, typeof(String));

            // データ
            this.haishaJokyoDataTable.Rows.Add(ConstClass.HAISHA_JOKYO_CD_JUCHU, ConstClass.HAISHA_JOKYO_NAME_JUCHU);
            this.haishaJokyoDataTable.Rows.Add(ConstClass.HAISHA_JOKYO_CD_HAISHA, ConstClass.HAISHA_JOKYO_NAME_HAISHA);
            this.haishaJokyoDataTable.Rows.Add(ConstClass.HAISHA_JOKYO_CD_KEIJO, ConstClass.HAISHA_JOKYO_NAME_KEIJO);
            this.haishaJokyoDataTable.Rows.Add(ConstClass.HAISHA_JOKYO_CD_CANCEL, ConstClass.HAISHA_JOKYO_NAME_CANCEL);
            this.haishaJokyoDataTable.Rows.Add(ConstClass.HAISHA_JOKYO_CD_NASHI, ConstClass.HAISHA_JOKYO_NAME_NASHI);

            // 列名とデータソース設定
            this.form.HAISHA_JOKYO_CD.PopupDataHeaderTitle = new string[] { ConstClass.HEADER_HAISHA_JOKYO_CD, ConstClass.HEADER_HAISHA_JOKYO_NAME };
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

        #region コース組込 ポップアップ初期化
        /// <summary>
        /// コース組込 ポップアップ初期化
        /// </summary>
        public void CourseKumikomiPopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 表示用データ設定
                DataTable courseKumikomiDataTable = new DataTable();

                // データ（列）
                courseKumikomiDataTable.Columns.Add("COURSE_KUMIKOMI_CD", Type.GetType("System.String"));
                courseKumikomiDataTable.Columns.Add("COURSE_KUMIKOMI_NAME", Type.GetType("System.String"));
                // データ（行）
                courseKumikomiDataTable.Rows.Add("1", "臨時");
                courseKumikomiDataTable.Rows.Add("2", "組込");

                // TableNameを設定すれば、ポップアップのタイトルになる
                courseKumikomiDataTable.TableName = "コース組込選択";

                // 列名とデータソース設定
                this.form.COURSE_KUMIKOMI_CD.PopupDataHeaderTitle = new string[] { "コース組込CD", "コース組込名" };
                this.form.COURSE_KUMIKOMI_CD.PopupDataSource = courseKumikomiDataTable;
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

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 新規ボタン(F2)イベント
                parentForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);
                //// LostFocusでチェックを行わない
                //parentForm.bt_func2.CausesValidation = false;

                // 修正ボタン(F3)イベント
                parentForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);
                //// LostFocusでチェックを行わない
                //parentForm.bt_func3.CausesValidation = false;

                // コンテナ(F6)イベント
                // 元々サブファンクション[1]で実装していたものをF6へ変更
                // (メソッド名はそのままにしておく。いずれはメソッド名を妥当な名前に変更したい)
                parentForm.bt_func6.Click += new EventHandler(this.form.bt_process1_Click);

                // 一覧ボタン(F7)イベント
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                //検索ボタン(F8)イベント生成
                // F8ボタンを使用不可にする（№3811）
                //parentForm.bt_func8.Click += new EventHandler(this.form.Print);

                ////登録ボタン(F9)イベント生成
                //this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                //parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 行挿入ボタン(F10)イベント
                parentForm.bt_func10.Click += new EventHandler(this.form.AddRow);

                // 行挿入ボタン(F11)イベント
                parentForm.bt_func11.Click += new EventHandler(this.form.RemoveRow);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 地図表示ボタン(subF1)イベント生成
                parentForm.bt_process1.Click += new EventHandler(this.form.MapOpen);

                // ｼｮｰﾄﾒｯｾｰｼﾞボタン(subF2)イベント生成
                parentForm.bt_process2.Click += new EventHandler(this.form.SmsNyuuryoku);

                // コントロールのイベント(諸口設定関連)
                this.form.TORIHIKISAKI_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIOROSHI_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.NIOROSHI_GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.UNPAN_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                //this.form.SHARYOU_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);

                //Communicate InxsSubApplication Start
                this.form.REQUEST_INXS_BUTTON.Click += new EventHandler(this.form.REQUEST_INXS_BUTTON_Click);
                parentForm.OnReceiveMessageEvent += form.ParentForm_OnReceiveMessageEvent;
                parentForm.FormClosing += new FormClosingEventHandler(form.Form_FormClosing);
                //Communicate InxsSubApplication End
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

        #region 受付番号変わった後、連動ティクスボックスのイベントの初期化処理
        /// <summary>
        /// 受付番号変わった後、連動ティクスボックスのインターイベントの初期化処理
        /// </summary>
        private void EnterEventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先
                this.form.TORIHIKISAKI_CD.Enter -= this.form.Control_Enter;
                this.form.TORIHIKISAKI_CD.Enter += this.form.Control_Enter;

                //業者
                this.form.GYOUSHA_CD.Enter -= this.form.Control_Enter;
                this.form.GYOUSHA_CD.Enter += this.form.Control_Enter;
                //現場
                this.form.GENBA_CD.Enter -= this.form.Control_Enter;
                this.form.GENBA_CD.Enter += this.form.Control_Enter;
                //運搬業者
                this.form.UNPAN_GYOUSHA_CD.Enter -= this.form.Control_Enter;
                this.form.UNPAN_GYOUSHA_CD.Enter += this.form.Control_Enter;
                //荷卸し業者
                this.form.NIOROSHI_GYOUSHA_CD.Enter -= this.form.Control_Enter;
                this.form.NIOROSHI_GYOUSHA_CD.Enter += this.form.Control_Enter;
                //荷卸し現場
                this.form.NIOROSHI_GENBA_CD.Enter -= this.form.Control_Enter;
                this.form.NIOROSHI_GENBA_CD.Enter += this.form.Control_Enter;
                //受付番号
                this.form.UKETSUKE_NUMBER.Enter -= this.form.Control_Enter;
                this.form.UKETSUKE_NUMBER.Enter += this.form.Control_Enter;

                //営業担当者
                this.form.EIGYOU_TANTOUSHA_CD.Enter -= this.form.Control_Enter;
                this.form.EIGYOU_TANTOUSHA_CD.Enter += this.form.Control_Enter;

                // 作業日
                this.form.SAGYOU_DATE.Enter -= this.form.Control_Enter;
                this.form.SAGYOU_DATE.Enter += this.form.Control_Enter;

                //CongBinh 20210713 #152801 S
                this.form.SHARYOU_CD.Enter -= this.form.Control_Enter;
                this.form.SHARYOU_CD.Enter += this.form.Control_Enter;

                this.form.SHASHU_CD.Enter -= this.form.Control_Enter;
                this.form.SHASHU_CD.Enter += this.form.Control_Enter;
                //CongBinh 20210713 #152801 E
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

        #region モード別の初期表示処理
        /// <summary>
        /// モード別の初期表示処理
        /// </summary>
        internal bool DisplayInit(out bool catchErr)
        {
            catchErr = true;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();
                // オプション
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    // mapbox用ボタン無効化
                    parentForm.bt_process1.Text = string.Empty;
                    parentForm.bt_process1.Enabled = false;
                }

                // 画面モードチェック（ｼｮｰﾄﾒｯｾｰｼﾞ）
                this.CheckWindowTypeSms(this.form.WindowType);
                
                // モードより必須項目設定
                this.SetRegistCheck();

                // 値クリア
                this.ClearControls();

                // フィールドクリア
                this.ClearFields();

                //連動ティクスボックスのインターイベントの初期化処理
                this.EnterEventInit();

                //Communicate InxsSubApplication -> reset color of status button 
                this.ResetButtonRequestStatusInxs();
                this.form.btnSayouDate.Enabled = false;//CongBinh 20210713 #152801 

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.form.btnSayouDate.Enabled = true;//CongBinh 20210713 #152801 
                        // 新規モードの場合
                        // 諸口関連項目のReadonly初期設定
                        this.SetShokuchiControlsReadonlyProperty();
                        // 20160121 chenzz 諸口関連修正 end

                        // コントロール活性に制御(確定項目除く)
                        this.ChangeEnabledByCtrls(true);
                        // 確定項目活性に制御
                        this.ChangeEnabledByKakuteiCtrls(true);
                        // 受付番号、車種台数初期制御（活性・非活性制御の後に呼出）
                        this.ControlInit();

                        // 受付番号ありの場合
                        if (!string.IsNullOrEmpty(this.form.UketsukeNumber))
                        {
                            // データを検索
                            if (this.Search() > 0)
                            {
                                // データを表示
                                this.SetValueToForm();

                                //合計値を計算
                                this.CalcTotalValues();

                                // 受付番号をクリア
                                this.form.UKETSUKE_NUMBER.Text = string.Empty;

                                // コースをクリア
                                this.form.COURSE_NAME_CD.Text = string.Empty;
                                this.form.COURSE_NAME_RYAKU.Text = string.Empty;
                                
                                // コンテナ
                                this.form.CONTENA_SOUSA_CD.Text = string.Empty;
                                this.form.CONTENA_SOUSA_NAME_RYAKU.Text = string.Empty;
                                //設置
                                this.form.SETTI_DAISUU.Text = string.Empty;
                                //引揚
                                this.form.HIKIAGE_DAISUU.Text = string.Empty;
                                this.settiDaisu = 0;
                                this.hikiageDaisu = 0;
                                this.listContenaParam = new List<T_CONTENA_RESERVE>();
                                this.beforeListContenaParam = new List<T_CONTENA_RESERVE>();
                                this.arrContenaReserve = new T_CONTENA_RESERVE[0];
                            
                            }

                            this.form.UketsukeNumber = string.Empty;
                        }
                        else
                        {
                            // 初期値設定
                            this.InitDataForNewMode();
                        }

                        // chenzz 20160121 複写すると諸口の場合、名称入力ができない　start
                        if (!this.CheckTorihikisakiShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        if (!this.CheckGyoushaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        if (!this.CheckGenbaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        if (!this.CheckNioroshiGyoushaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        this.CheckNioroshiGenbaShokuchi();
                        if (!this.CheckUpanGyoushaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        // chenzz 20160121 複写すると諸口の場合、名称入力ができない　end

                        this.headerForm.windowTypeLabel.Text = "新規";
                        this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                        this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 更新モードの場合
                        // 諸口関連項目のReadonly初期設定
                        this.SetShokuchiControlsReadonlyProperty();
                        // 20160121 chenzz 諸口関連修正 end

                        // コントロール活性に制御(確定項目除く)
                        this.ChangeEnabledByCtrls(true);
                        // 確定項目活性に制御
                        this.ChangeEnabledByKakuteiCtrls(true);
                        // 受付番号、車種台数初期制御（活性・非活性制御の後に呼出）
                        this.ControlInit();

                        // 受付番号がない場合
                        if (string.IsNullOrEmpty(this.form.UketsukeNumber))
                        {
                            // 処理終了
                            ret = true;
                            return ret;
                        }

                        // データを検索
                        if (this.Search() < 1)
                        {
                            // 受付番号をセット
                            this.form.UKETSUKE_NUMBER.Text = this.form.UketsukeNumber;
                            // メッセージ表示
                            this.msgLogic.MessageBoxShow("E045");
                            // 処理終了
                            ret = false;
                            return ret;
                        }

                        // GROUP_NUMBER
                        this.groupNumber = this.GetGroupNumber((long)this.ChgDBNullToValue(this.dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"], -1L));
                        // データを表示
                        this.SetValueToForm();

                        //合計値を計算
                        this.CalcTotalValues();

                        // 配車種類は確定の場合
                        if (!string.IsNullOrEmpty(this.form.HAISHA_SHURUI_CD.Text) &&
                            int.Parse(this.form.HAISHA_SHURUI_CD.Text) == 3)
                        {
                            // コントロール非活性に制御(確定項目除く)
                            this.ChangeEnabledByCtrls(false);

                            // 修正モード + 配車種類：確定の場合、配車種類は修正できるようにしておく
                            this.ChangeControlsEnabledProperty(new List<string> { "HAISHA_SHURUI_CD", "HAISHA_SHURUI_NAME" }, true);

                            // footerのﾎﾞﾀﾝ制御
                            this.parentForm.bt_func10.Enabled = false;
                            this.parentForm.bt_func11.Enabled = false;

                        }

                        this.sharyouCd = this.form.SHARYOU_CD.Text;

                        // コース組込済の場合は参照モードにする
                        if (!String.IsNullOrEmpty(this.form.COURSE_NAME_CD.Text))
                        {
                            var messageLogic = new MessageBoxShowLogic();
                            messageLogic.MessageBoxShow("E161", "修正");

                            this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            this.headerForm.windowTypeLabel.Text = this.form.WindowType.ToTypeString();
                            this.headerForm.windowTypeLabel.BackColor = this.form.WindowType.ToLabelColor();
                            this.headerForm.windowTypeLabel.ForeColor = this.form.WindowType.ToLabelForeColor();

                            // コントロール非活性に制御(確定項目除く)
                            this.ChangeEnabledByCtrls(false);
                            // 確定項目非活性に制御
                            this.ChangeEnabledByKakuteiCtrls(true);
                            // 受付番号、車種台数初期制御（活性・非活性制御の後に呼出）
                            this.ControlInit();

                            this.ButtonInit();

                            // footerのﾎﾞﾀﾝ制御
                            this.parentForm.bt_func3.Enabled = false;
                            this.parentForm.bt_func6.Enabled = false;
                            this.parentForm.bt_func9.Enabled = false;
                            this.parentForm.bt_func10.Enabled = false;
                            this.parentForm.bt_func11.Enabled = false;
                        }
                        //20150928 hoanghm start
                        ////ThangNguyen [Add] 20150825 #10907 Start
                        //this.CheckTorihikisaki();
                        //this.CheckGyousha();
                        //this.CheckGenba();
                        //this.CheckUnpanGyoushaCd();
                        //this.CheckNioroshiGyoushaCd();
                        //this.ChechNioroshiGenbaCd();
                        ////ThangNguyen [Add] 20150825 #10907 End
                        if (!this.CheckTorihikisakiShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        if (!this.CheckGyoushaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        if (!this.CheckGenbaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        if (!this.CheckNioroshiGyoushaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        this.CheckNioroshiGenbaShokuchi();
                        if (!this.CheckUpanGyoushaShokuchi())
                        {
                            catchErr = false;
                            return ret;
                        }
                        //20150928 hoanghm end

                        this.headerForm.windowTypeLabel.Text = "修正";
                        this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                        this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 参照モードの場合

                        // コントロール非活性に制御(確定項目除く)
                        this.ChangeEnabledByCtrls(false);
                        // 確定項目非活性に制御
                        this.ChangeEnabledByKakuteiCtrls(true);
                        // 受付番号、車種台数初期制御（活性・非活性制御の後に呼出）
                        this.ControlInit();

                        // データを検索
                        if (this.Search() < 1)
                        {
                            this.form.UKETSUKE_NUMBER.Text = this.form.UketsukeNumber;
                            // メッセージ表示
                            this.msgLogic.MessageBoxShow("E045");
                            ret = false;
                            return ret;
                        }
                        // GROUP_NUMBER
                        this.groupNumber = this.GetGroupNumber((long)this.ChgDBNullToValue(this.dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"], -1L));
                        // データを表示
                        this.SetValueToForm();

                        //合計値を計算
                        this.CalcTotalValues();

                        // footerのﾎﾞﾀﾝ制御
                        this.parentForm.bt_func3.Enabled = false;
                        this.parentForm.bt_func6.Enabled = true;
                        this.parentForm.bt_func9.Enabled = false;
                        this.parentForm.bt_func10.Enabled = false;
                        this.parentForm.bt_func11.Enabled = false;

                        this.headerForm.windowTypeLabel.Text = "参照";
                        this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                        this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 削除モードの場合

                        // コントロール非活性に制御(確定項目除く)
                        this.ChangeEnabledByCtrls(false);
                        // 確定項目非活性に制御
                        this.ChangeEnabledByKakuteiCtrls(false);
                        // 受付番号、車種台数初期制御（活性・非活性制御の後に呼出）
                        this.ControlInit();

                        // データを検索
                        if (this.Search() < 1)
                        {
                            this.form.UKETSUKE_NUMBER.Text = this.form.UketsukeNumber;
                            // メッセージ表示
                            this.msgLogic.MessageBoxShow("E045");
                            ret = false;
                            return ret;
                        }
                        // GROUP_NUMBER
                        this.groupNumber = this.GetGroupNumber((long)this.ChgDBNullToValue(this.dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"], -1L));
                        // データを表示
                        this.SetValueToForm();

                        //合計値を計算
                        this.CalcTotalValues();

                        this.headerForm.windowTypeLabel.Text = "削除";
                        this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                        this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.White;

                        break;
                    default:
                        break;
                }

                //作業日の有無に応じて荷卸日のEnabledを切り替える
                this.form.ChangeNioroshiDateEnabled();

                // コントロールの表示・ReadOnly制御
                this.ResetControlInit();

                // [F8]指示書ボタンの状態を切り替え
                this.ChangeBt_func8Enable();

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DisplayInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DisplayInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }
        #endregion

        #region 諸口区分関連項目のReadonly設定(true)
        /// <summary>
        /// 諸口区分関連項目のReadonly設定(true)
        /// </summary>
        internal void SetShokuchiControlsReadonlyProperty()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<string> shokuchiCtrlList = new List<string> { "TORIHIKISAKI_NAME"
                                                                , "GYOUSHA_NAME"
                                                                , "GYOSHA_TEL"
                                                                , "GENBA_NAME"
                                                                , "GENBA_ADDRESS1"
                                                                , "GENBA_ADDRESS2"
                                                                , "GENBA_TEL"
                                                                , "TANTOSHA_NAME"
                                                                , "TANTOSHA_TEL"
                                                                , "UNPAN_GYOUSHA_NAME"
                                                                , "NIOROSHI_GYOUSHA_NAME"
                                                                , "NIOROSHI_GENBA_NAME" };

                // UIFormのコントロールを制御
                foreach (var controlName in shokuchiCtrlList)
                {
                    // メインフォームからコントロールを取得
                    Control control = controlUtil.FindControl(this.form, controlName);
                    // コントロール取得できない場合
                    if (control == null)
                    {
                        // 次へ
                        continue;
                    }

                    // Readonlyプロパティを取得
                    var property = control.GetType().GetProperty("Enabled");
                    if (property != null)
                    {
                        this.SetCtrlReadonly((CustomTextBox)control, true);
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

        #region 明細の活性・非活性制御
        /// <summary>
        /// 明細活性・非活性制御
        /// </summary>
        /// <param name="isLock">true:Readonly false:入力可</param>
        internal void ChangeReadonlyForMeisai(bool isLock)
        {
            try
            {
                LogUtility.DebugMethodStart(isLock);

                // Detailのコントロールを制御
                foreach (var detaiControlName in inputDetailControlNames)
                {
                    // ReadOnly制御
                    dgvDetail.Columns[detaiControlName].ReadOnly = isLock;

                    // 品名のポップアップ制御
                    if (detaiControlName.Equals("HINMEI_CD"))
                    {
                        // ReadOnlyの場合
                        if (isLock)
                        {
                            // ポップアップクリア
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowId = WINDOW_ID.NONE;
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowName = "";
                        }
                        else
                        {
                            // ポップアップ設定
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowId = WINDOW_ID.M_HINMEI_SEARCH;
                            //((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowName = "マスタ共通ポップアップ";
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowName = "複数キー用検索共通ポップアップ";
                        }
                    }
                    else if (detaiControlName.Equals("UNIT_CD"))
                    {
                        // ReadOnlyの場合
                        if (isLock)
                        {
                            // 単位名取得設定クリア
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).FocusOutCheckMethod = null;
                            // ポップアップクリア
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowId = WINDOW_ID.NONE;
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowName = "";
                        }
                        else
                        {
                            // ポップアップ設定
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowId = WINDOW_ID.M_UNIT;
                            ((DgvCustomTextBoxColumn)dgvDetail.Columns[detaiControlName]).PopupWindowName = "マスタ共通ポップアップ";
                        }
                    }
                }

                if (isLock)
                {
                    //明細欄の背景色と文字色をグレーにします
                    var rows = this.form.dgvDetail.Rows;
                    foreach (DataGridViewRow row in rows)
                    {
                        var cells = row.Cells;
                        foreach (DataGridViewCell cell in cells)
                        {
                            var changeColor = cell as ICustomAutoChangeBackColor;
                            if (changeColor != null)
                            {
                                ((ICustomAutoChangeBackColor)cell).AutoChangeBackColorEnabled = false;
                                cell.Style.BackColor = Constans.DISABLE_COLOR;
                                cell.Style.ForeColor = Constans.DISABLE_COLOR_FORE;
                                cell.Style.SelectionBackColor = Constans.DISABLE_COLOR;
                                cell.Style.SelectionForeColor = Constans.DISABLE_COLOR_FORE;
                            }
                        }
                    }
                }
                else
                {
                    //明細欄の背景色と文字色をノーマルカラーにします
                    var rows = this.form.dgvDetail.Rows;
                    foreach (DataGridViewRow row in rows)
                    {
                        var cells = row.Cells;
                        foreach (DataGridViewCell cell in cells)
                        {
                            var targetCell = cell as ICustomAutoChangeBackColor;
                            if (null != targetCell)
                            {
                                CustomControlExtLogic.UpdateBackColor(targetCell);
                            }
                        }
                    }
                }

                // イベント削除
                this.dgvDetail.CellEnter -= this.form.dgvDetail_OnCellEnter;
                this.dgvDetail.CellValidating -= this.form.dgvDetail_CellValidating;
                this.dgvDetail.CellValidated -= this.form.dgvDetail_OnValidated;
                // NotReadOnlyの場合
                if (!isLock)
                {
                    // イベント追加
                    this.dgvDetail.CellEnter += this.form.dgvDetail_OnCellEnter;
                    this.dgvDetail.CellValidating += this.form.dgvDetail_CellValidating;
                    this.dgvDetail.CellValidated += this.form.dgvDetail_OnValidated;
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

        #region 活性・非活性制御（確定場合の制御項目）
        /// <summary>
        /// 活性・非活性制御（確定場合の制御項目）
        /// </summary>
        /// <param name="isEnabled">true:Enabled false:UnEnabled</param>
        internal void ChangeEnabledByKakuteiCtrls(bool isEnabled)
        {
            try
            {
                LogUtility.DebugMethodStart(isEnabled);

                // 活性・非活性制御
                this.ChangeControlsEnabledProperty(kakuteiControlNames, isEnabled);

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

        #region 活性・非活性制御（確定場合の制御項目を除く）
        /// <summary>
        /// 活性・非活性制御（確定場合の制御項目を除く）
        /// </summary>
        /// <param name="isEnabled">true:Enabled false:UnEnabled</param>
        internal void ChangeEnabledByCtrls(bool isEnabled)
        {
            try
            {
                LogUtility.DebugMethodStart(isEnabled);

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(inputUiFormControlNames);
                formControlNameList.AddRange(inputHeaderControlNames);

                // 活性・非活性制御
                this.ChangeControlsEnabledProperty(formControlNameList, isEnabled);

                // 明細のReadOnlyプロパティ設定
                this.ChangeReadonlyForMeisai(!isEnabled);

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

        #region Enabledプロパティ設定
        /// <summary>
        /// Enabledプロパティ設定
        /// </summary>
        /// <param name="ctrlList">コントロールリスト</param>
        /// <param name="isEnabled">true:Enabled false:UnEnabled</param>
        internal void ChangeControlsEnabledProperty(List<string> ctrlList, bool isEnabled)
        {
            try
            {
                LogUtility.DebugMethodStart(ctrlList, isEnabled);

                // UIFormのコントロールを制御
                foreach (var controlName in ctrlList)
                {
                    // メインフォームからコントロールを取得
                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        // ヘッダフォームからコントロールを取得
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }
                    // コントロール取得できない場合
                    if (control == null)
                    {
                        // 次へ
                        continue;
                    }

                    // Enabledプロパティを取得
                    var property = control.GetType().GetProperty("Enabled");
                    if (property != null)
                    {
                        property.SetValue(control, isEnabled, null);
                    }

                    //var enabledProperty = control.GetType().GetProperty("Enabled");
                    //var readOnlyProperty = control.GetType().GetProperty("ReadOnly");
                    //if (enabledProperty != null)
                    //{
                    //    bool readOnlyValue = false;
                    //    if (readOnlyProperty != null)
                    //    {
                    //        readOnlyValue = (bool)readOnlyProperty.GetValue(control, null);
                    //    }
                    //    if (!readOnlyValue)
                    //    {
                    //        enabledProperty.SetValue(control, isEnabled, null);
                    //    }
                    //}
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

        #region （受付番号、車種台数）表示・ReadOnlyの初期制御
        /// <summary>
        /// （受付番号、車種台数）表示・ReadOnlyの初期制御
        /// </summary>
        internal void ControlInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                switch (this.form.WindowType)
                {
                    // 新規モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = false;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = true;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = true;
                        // 車種台数
                        this.form.SHASHU_DAISU_NUMBER.ReadOnly = false;
                        this.form.SHASHU_DAISU_NUMBER.TabStop = true;
                        // 車種台数ラベル
                        this.form.SHASHU_DAISU_TOTAL.Visible = false;
                        // 車種台数[前]ﾎﾞﾀﾝ
                        this.form.DAISUU_PREVIOUS_BUTTON.Visible = false;
                        // 車種台数[次]ﾎﾞﾀﾝ
                        this.form.DAISUU_NEXT_BUTTON.Visible = false;

                        break;

                    // 修正モードの場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = true;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = true;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = true;

                        // 車種台数
                        this.form.SHASHU_DAISU_NUMBER.ReadOnly = true;
                        this.form.SHASHU_DAISU_NUMBER.TabStop = false;
                        // 車種台数ラベル
                        this.form.SHASHU_DAISU_TOTAL.Visible = false;
                        // 車種台数[前]ﾎﾞﾀﾝ
                        this.form.DAISUU_PREVIOUS_BUTTON.Visible = false;
                        // 車種台数[次]ﾎﾞﾀﾝ
                        this.form.DAISUU_NEXT_BUTTON.Visible = false;

                        break;

                    // 参照モードの場合
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = true;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = true;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = true;

                        // 車種台数
                        this.form.SHASHU_DAISU_NUMBER.ReadOnly = true;
                        // 車種台数ラベル
                        this.form.SHASHU_DAISU_TOTAL.Visible = false;
                        // 車種台数[前]ﾎﾞﾀﾝ
                        this.form.DAISUU_PREVIOUS_BUTTON.Visible = false;
                        // 車種台数[次]ﾎﾞﾀﾝ
                        this.form.DAISUU_NEXT_BUTTON.Visible = false;

                        break;

                    // 削除モードの場合
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = true;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = false;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = false;

                        // 車種台数
                        this.form.SHASHU_DAISU_NUMBER.ReadOnly = true;
                        // 車種台数ラベル
                        this.form.SHASHU_DAISU_TOTAL.Visible = false;
                        // 車種台数[前]ﾎﾞﾀﾝ
                        this.form.DAISUU_PREVIOUS_BUTTON.Visible = false;
                        // 車種台数[次]ﾎﾞﾀﾝ
                        this.form.DAISUU_NEXT_BUTTON.Visible = false;

                        break;

                    default:
                        break;
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

        #region （受付番号、車種台数）表示・ReadOnlyの再設定制御
        /// <summary>
        /// （受付番号、車種台数）表示・ReadOnlyの再設定制御
        /// </summary>
        internal void ResetControlInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                switch (this.form.WindowType)
                {
                    // 新規モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // ラベル表示設定
                        this.form.SHASHU_DAISU_LBL.Text = "車種台数※";
                        break;

                    // 修正モードの場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // ラベル表示設定
                        this.form.SHASHU_DAISU_LBL.Text = "車種台数";

                        // 車種台数 == 1の場合
                        if (this.groupNumber <= 1)
                        {
                            // 車種台数ラベル
                            this.form.SHASHU_DAISU_TOTAL.Visible = false;
                            // 車種台数[前]ﾎﾞﾀﾝ
                            this.form.DAISUU_PREVIOUS_BUTTON.Visible = false;
                            // 車種台数[次]ﾎﾞﾀﾝ
                            this.form.DAISUU_NEXT_BUTTON.Visible = false;
                        }
                        else
                        {
                            // 車種台数ラベル
                            this.form.SHASHU_DAISU_TOTAL.Visible = true;
                            // 車種台数[前]ﾎﾞﾀﾝ
                            this.form.DAISUU_PREVIOUS_BUTTON.Visible = true;
                            // 車種台数[次]ﾎﾞﾀﾝ
                            this.form.DAISUU_NEXT_BUTTON.Visible = true;
                        }

                        break;

                    // 削除モードの場合
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // ラベル表示設定
                        this.form.SHASHU_DAISU_LBL.Text = "車種台数";

                        // 車種台数 == 1の場合
                        if (this.groupNumber <= 1)
                        {
                            // 車種台数ラベル
                            this.form.SHASHU_DAISU_TOTAL.Visible = false;
                        }
                        else
                        {
                            // 車種台数ラベル
                            this.form.SHASHU_DAISU_TOTAL.Visible = true;
                        }

                        break;
                    default:
                        break;
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

        #region フィールド値をクリア
        /// <summary>
        /// フィールド値をクリア
        /// </summary>
        internal bool ClearFields()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // Form関連
                this.form.dicControl.Clear();

                // 受付収集入力関連
                this.insEntryEntityList.Clear();
                this.insDetailEntityList.Clear();
                this.delEntryEntity = null;

                // コンテナ関連
                this.arrContenaReserve = new T_CONTENA_RESERVE[0];
                this.listContenaParam.Clear();
                this.insContenaEntityList.Clear();
                this.beforeListContenaParam.Clear();

                this.sharyouCd = String.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region コントロールの値をクリア
        /// <summary>
        /// コントロールの値をクリア
        /// </summary>
        internal void ClearControls()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(clearUiFormControlNames);
                formControlNameList.AddRange(clearHeaderControlNames);
                // 新規モード以外の場合
                if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 拠点クリア
                    string[] headTmp = { "KYOTEN_CD", "KYOTEN_NAME_RYAKU" };
                    formControlNameList.AddRange(headTmp);
                }
                foreach (var controlName in formControlNameList)
                {
                    // メインフォームからコントロールを取得
                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        // ヘッダフォームからコントロールを取得
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    PropertyInfo property;
                    // 日付コントロールの場合
                    if (control is CustomDateTimePicker)
                    {
                        // Valueをクリア
                        ((CustomDateTimePicker)control).Value = null;
                    }
                    else
                    {
                        // Textプロパティを取得
                        property = control.GetType().GetProperty("Text");
                        if (property != null)
                        {
                            // クリア
                            property.SetValue(control, string.Empty, null);
                        }
                    }

                    // IsInputErrorOccuredプロパティを取得
                    property = control.GetType().GetProperty("IsInputErrorOccured");
                    if (property != null)
                    {
                        // クリア
                        property.SetValue(control, false, null);
                    }
                }

                // 明細クリア
                this.dgvDetail.CellValidating -= this.form.dgvDetail_CellValidating;
                this.dgvDetail.CellValidated -= this.form.dgvDetail_OnValidated;
                this.dgvDetail.Rows.Clear();
                //this.dgvDetail.CellValidated += this.form.dgvDetail_OnValidated;

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

        #region 新規モードの初期値設定

        #region 新規モードの初期値設定
        /// <summary>
        /// 新規モードの初期値設定
        /// </summary>
        private void InitDataForNewMode()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 配車状況
                this.form.HAISHA_JOKYO_CD.Text = "1";
                this.form.HAISHA_JOKYO_NAME.Text = "受注";

                // 配車種類
                this.form.HAISHA_SHURUI_CD.Text = "1";
                this.form.HAISHA_SHURUI_NAME.Text = "通常";

                // 配車指示書
                this.form.HAISHA_SIJISHO_FLG.Text = "未";

                // 受付日
                this.form.UKETSUKE_DATE.Value = parentForm.sysDate.Date;
                this.form.UKETSUKE_DATE_HOUR.Text = DateTime.Now.Hour.ToString();
                //this.form.UKETSUKE_DATE_MINUTE.Text = "0";
                this.form.UKETSUKE_DATE_MINUTE.Text = (DateTime.Now.Minute / 5 * 5).ToString();

                // 作業日(指定)
                this.form.SAGYOU_DATE.Value = parentForm.sysDate.Date;

                // 荷降日
                this.form.NIOROSHI_DATE.Value = parentForm.sysDate.Date;

                // 現着時間
                this.form.GENCHAKU_TIME_CD.Text = String.Empty;
                this.form.GENCHAKU_TIME_NAME.Text = String.Empty;

                // 車種台数
                this.form.SHASHU_DAISU_NUMBER.Text = "1";

                // コース組込
                this.form.COURSE_KUMIKOMI_CD.Text = "1";
                this.form.COURSE_KUMIKOMI_NAME.Text = "臨時";

                // 運搬業者
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    // 運搬業者の存在チェック及び、受入業者かチェック
                    var gyousha = gyoushaDao.GetAllValidData(new M_GYOUSHA
                    {
                        GYOUSHA_CD = sysInfo[0].UKETSUKE_UPN_GYOUSHA_CD_DEFALUT,
                        GYOUSHAKBN_UKEIRE = true
                    }).FirstOrDefault();

                    if (gyousha != null)
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = sysInfo[0].UKETSUKE_UPN_GYOUSHA_CD_DEFALUT;
                    }
                }
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text.ToString()))
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = this.form.UNPAN_GYOUSHA_CD.Text.ToString().PadLeft(this.form.UNPAN_GYOUSHA_CD.MaxLength, '0');
                    // 運搬業者名設定
                    this.CheckUnpanGyoushaCdForDisplay();
                }

                // No.3817-->
                if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.form.WindowType))
                {   // 新規の場合
                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                    // 荷降業者
                    const string NIOROSHI_GYOUSHA_CD = "荷降業者CD";
                    this.form.NIOROSHI_GYOUSHA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GYOUSHA_CD);
                    if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text.ToString()))
                    {
                        this.form.NIOROSHI_GYOUSHA_CD.Text = this.form.NIOROSHI_GYOUSHA_CD.Text.ToString().PadLeft(this.form.NIOROSHI_GYOUSHA_CD.MaxLength, '0');
                        CheckNioroshiGyoushaCd();
                    }
                    // 荷降現場
                    const string NIOROSHI_GENBA_CD = "荷降現場CD";
                    this.form.NIOROSHI_GENBA_CD.Text = this.GetUserProfileValue(userProfile, NIOROSHI_GENBA_CD);
                    if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text.ToString()))
                    {
                        this.form.NIOROSHI_GENBA_CD.Text = this.form.NIOROSHI_GENBA_CD.Text.ToString().PadLeft(this.form.NIOROSHI_GENBA_CD.MaxLength, '0');
                        ChechNioroshiGenbaCd();
                    }
                }
                // No.3817<--

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

        #region 運搬業者CDの存在チェック（初期表示）
        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        internal void CheckUnpanGyoushaCdForDisplay()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    return;
                }

                //short unpanGyoushaCd = -1;
                //if (!short.TryParse(this.form.UNPAN_GYOUSHA_CD.Text, out unpanGyoushaCd))
                //{
                //    return;
                //}

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                var unpanGyoushas = this.gyoushaDao.GetAllValidData(keyEntity);

                // 存在チェック
                if (unpanGyoushas == null || unpanGyoushas.Length < 1)
                {
                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E020", "運搬業者");
                    //this.form.UNPAN_GYOUSHA_CD.Focus();
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_GYOUSHA unpanGyousha = unpanGyoushas[0];
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.SHOKUCHI_KBN.IsTrue ? unpanGyousha.GYOUSHA_NAME1 : unpanGyousha.GYOUSHA_NAME_RYAKU.ToString();
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

        #endregion

        #region モードより、必須項目設定
        /// <summary>
        /// モードより、必須項目設定
        /// </summary>
        internal void SetRegistCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                switch (this.form.WindowType)
                {
                    // 新規モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 受付番号
                        this.form.UKETSUKE_NUMBER_LBL.Text = "受付番号";
                        this.form.UKETSUKE_NUMBER.RegistCheckMethod = null;

                        break;

                    // 修正モードの場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 削除モードの場合
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 必須チェック設定
                        SelectCheckDto existCheck = new SelectCheckDto();
                        existCheck.CheckMethodName = "必須チェック";
                        Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                        excitChecks.Add(existCheck);

                        // 受付番号
                        this.form.UKETSUKE_NUMBER_LBL.Text = "受付番号※";
                        this.form.UKETSUKE_NUMBER.RegistCheckMethod = excitChecks;

                        break;
                    default:
                        break;
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

        #region 必須項目チェック設定
        /// <summary>
        /// 必須項目チェック設定
        /// </summary>
        internal void SetRegistCheckMethod()
        {
            // 必須チェック設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
            excitChecks.Add(existCheck);
        }
        #endregion

        #region 指定した受付番号のデータが存在するか返す
        /// <summary>
        /// 指定した受付番号のデータが存在するか返す
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistData(string uketsukeNumber)
        {
            // 戻り値初期化
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                if (!string.IsNullOrEmpty(uketsukeNumber))
                {
                    // 検索条件設定
                    this.dto.UketsukeNumber = long.Parse(this.form.UketsukeNumber);
                    // 入力データを検索
                    this.dtUketsukeEntry = this.daoUketsukeEntry.GetDataToDataTable(this.dto);

                    //Communicate InxsSubApplication -> Get request information
                    this.GetInxsRequestData();

                    // 0件の場合
                    if (this.dtUketsukeEntry.Rows.Count > 0)
                    {
                        // 戻り値
                        returnVal = true;
                    }
                }
                else if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 戻り値
                    returnVal = true;
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
                    // 売上支払情報差引基準を取得(1.売上 or 2.支払)
                    this.systemUrShCalcBaseKbn = this.ChgDBNullToValue(this.sysInfoEntity.UR_SH_CALC_BASE_KBN, string.Empty).ToString();
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

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // ****************************
                // 受付（収集）入力データを検索
                // ****************************
                // 検索条件設定
                this.dto.UketsukeNumber = long.Parse(this.form.UketsukeNumber);
                this.dto.SEQ = int.Parse(this.form.SEQ);
                // 入力データを検索
                this.dtUketsukeEntry = this.daoUketsukeEntry.GetDataToDataTable(this.dto);
                // 件数
                result = this.dtUketsukeEntry.Rows.Count;
                // 0件の場合
                if (result == 0)
                {
                    // 処理終了
                    return result;
                }

                this.beforeHaishaJoukyou = (short)this.dtUketsukeEntry.Rows[0]["HAISHA_JOKYO_CD"];

                // ****************************
                // 受付（収集）明細データを検索
                // ****************************
                // 検索条件設定
                this.dto.SystemID = (long)this.dtUketsukeEntry.Rows[0]["SYSTEM_ID"];
                this.dto.SEQ = (int)this.dtUketsukeEntry.Rows[0]["SEQ"];

                //Communicate InxsSubApplication -> Get request information
                this.GetInxsRequestData();

                // 明細データを検索
                this.dtUketsukeDetail = this.daoUketsukeDetail.GetDataToDataTable(this.dto);

                // ****************************
                // コンテナ稼働予定データを検索
                // ****************************
                // コンテナ稼働予定データを検索(初期表示・データ削除時用)
                this.arrContenaReserve = this.daoContena.GetDataForEntity(this.dto);
                // 設置台数を取得
                settiDaisu = this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI).Sum(r => (int)r.DAISUU_CNT);
                // 引揚台数を取得
                hikiageDaisu = this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE).Sum(r => (int)r.DAISUU_CNT);

                // コンテナ指定画面との受取データ作成
                foreach (T_CONTENA_RESERVE entity in this.arrContenaReserve)
                {
                    T_CONTENA_RESERVE contena = new T_CONTENA_RESERVE();
                    // 設置引揚区分
                    contena.CONTENA_SET_KBN = entity.CONTENA_SET_KBN;
                    // コンテナ種類CD
                    contena.CONTENA_SHURUI_CD = entity.CONTENA_SHURUI_CD;
                    // コンテナCD
                    contena.CONTENA_CD = entity.CONTENA_CD;
                    // 台数
                    contena.DAISUU_CNT = entity.DAISUU_CNT;
                    // 削除Flg
                    contena.DELETE_FLG = entity.DELETE_FLG;

                    listContenaParam.Add(contena);
                }

                this.beforeListContenaParam = new List<T_CONTENA_RESERVE>();
                this.beforeListContenaParam = new List<T_CONTENA_RESERVE>(listContenaParam);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                result = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                result = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }
        #endregion

        #region GroupNumberを取得
        /// <summary>
        /// GroupNumberを取得
        /// </summary>
        /// <param name="groupNumber">車種台数グループ番号</param>
        /// <returns>count</returns>
        private int GetGroupNumber(long groupNumber)
        {
            int returnVal = 0;
            try
            {
                LogUtility.DebugMethodStart(groupNumber);

                // SQL文作成
                DataTable dt = new DataTable();
                string selectStr = "SELECT COUNT(SYSTEM_ID) AS CNT FROM T_UKETSUKE_SS_ENTRY";
                selectStr += " WHERE SHASHU_DAISU_GROUP_NUMBER = " + groupNumber;
                selectStr += " AND DELETE_FLG = 0";

                // データを検索
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                returnVal = (int)dt.Rows[0]["CNT"];

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

        #region 検索結果を画面に表示
        /// <summary>
        /// 検索結果を画面に表示
        /// </summary>
        internal void SetValueToForm()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string strDate;
                string strHour;
                string strMinute;

                var dt = this.dtUketsukeEntry;
                dt.BeginLoadData();

                //Communicate InxsSubApplication -> reset color of status button
                this.SetButtonRequestStatusInxs();

                // 請求端数CD
                seikyuuHasuuCD = this.ChgDBNullToValue(dt.Rows[0]["SEIKYUU_TAX_HASUU_CD"], string.Empty).ToString();
                // 支払端数CD
                siharaiHasuuCD = this.ChgDBNullToValue(dt.Rows[0]["SHIHARAI_TAX_HASUU_CD"], string.Empty).ToString();

                // ヘッダフォーム設定
                // 拠点
                this.headerForm.KYOTEN_CD.Text = this.FieldPadLeft(dt.Rows[0]["KYOTEN_CD"], 2, '0');
                this.headerForm.KYOTEN_NAME_RYAKU.Text = this.ChgDBNullToValue(dt.Rows[0]["KYOTEN_NAME_RYAKU"], string.Empty).ToString();

                // 新規モード以外の場合
                if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 作成者
                    this.headerForm.CreateUser.Text = dt.Rows[0]["CREATE_USER"].ToString();
                    // 作成日
                    this.headerForm.CreateDate.Text = dt.Rows[0]["CREATE_DATE"].ToString();
                    // 更新者
                    this.headerForm.LastUpdateUser.Text = dt.Rows[0]["UPDATE_USER"].ToString();
                    // 更新日
                    this.headerForm.LastUpdateDate.Text = dt.Rows[0]["UPDATE_DATE"].ToString();


                    // 配車指示書(非表示)
                    if ((bool)this.ChgDBNullToValue(dt.Rows[0]["HAISHA_SIJISHO_FLG"], false))
                    {
                        this.form.HAISHA_SIJISHO_FLG.Text = "済";
                    }
                    else
                    {
                        this.form.HAISHA_SIJISHO_FLG.Text = "未";
                    }
                }
                else
                {
                    this.form.HAISHA_SIJISHO_FLG.Text = "未";
                }

                // メインフォーム設定

                // 配車状況
                // 複写で呼ばれた場合 かつ
                // 配車状況が 計上、キャンセル、回収無し の状態の伝票を複写した場合
                if ((this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.form.UketsukeNumber))
                    && ((ConstClass.HAISHA_JOKYO_CD_KEIJO == this.ChgDBNullToValue(dt.Rows[0]["HAISHA_JOKYO_CD"], string.Empty).ToString()
                    || ConstClass.HAISHA_JOKYO_CD_CANCEL == this.ChgDBNullToValue(dt.Rows[0]["HAISHA_JOKYO_CD"], string.Empty).ToString()
                    || ConstClass.HAISHA_JOKYO_CD_NASHI == this.ChgDBNullToValue(dt.Rows[0]["HAISHA_JOKYO_CD"], string.Empty).ToString())))
                {
                    // 車輌、運転者が組み込まれている場合
                    if (!string.IsNullOrEmpty(this.ChgDBNullToValue(dt.Rows[0]["SHARYOU_CD"], string.Empty).ToString())
                        && !string.IsNullOrEmpty(this.ChgDBNullToValue(dt.Rows[0]["UNTENSHA_CD"], string.Empty).ToString()))
                    {
                        // 配車状況に「２．配車」をセット
                        this.form.HAISHA_JOKYO_CD.Text = ConstClass.HAISHA_JOKYO_CD_HAISHA;
                        this.form.HAISHA_JOKYO_NAME.Text = ConstClass.HAISHA_JOKYO_NAME_HAISHA;
                    }
                    else
                    {
                        // 配車状況に「１．受注」をセット
                        this.form.HAISHA_JOKYO_CD.Text = ConstClass.HAISHA_JOKYO_CD_JUCHU;
                        this.form.HAISHA_JOKYO_NAME.Text = ConstClass.HAISHA_JOKYO_NAME_JUCHU;
                    }
                }
                else
                {
                    this.form.HAISHA_JOKYO_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["HAISHA_JOKYO_CD"], string.Empty).ToString();
                    this.form.HAISHA_JOKYO_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["HAISHA_JOKYO_NAME"], string.Empty).ToString();
                }

                // 配車種類
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == this.form.WindowType)
                {
                    this.form.HAISHA_SHURUI_CD.Text = ConstClass.HAISHA_SHURUI_CD_TSUJO;
                    this.form.HAISHA_SHURUI_NAME.Text = ConstClass.HAISHA_SHURUI_NAME_TSUJO;
                }
                else
                {
                    this.form.HAISHA_SHURUI_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["HAISHA_SHURUI_CD"], string.Empty).ToString();
                    this.form.HAISHA_SHURUI_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["HAISHA_SHURUI_NAME"], string.Empty).ToString();
                }
                this.form.dispHaishaShruiCd = this.form.HAISHA_SHURUI_CD.Text;

                // 営業担当
                this.form.EIGYOU_TANTOUSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["EIGYOU_TANTOUSHA_CD"], string.Empty).ToString();
                this.form.EIGYOU_TANTOUSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["EIGYOU_TANTOUSHA_NAME"], string.Empty).ToString();

                // 受付日
                if (WINDOW_TYPE.NEW_WINDOW_FLAG != this.form.WindowType)
                {
                    if (this.TryChgDateTimeToDHM(dt.Rows[0]["UKETSUKE_DATE"], out strDate, out strHour, out strMinute))
                    {
                        this.form.UKETSUKE_DATE.Value = (DateTime)dt.Rows[0]["UKETSUKE_DATE"];
                        this.form.UKETSUKE_DATE_HOUR.Text = strHour;
                        this.form.UKETSUKE_DATE_MINUTE.Text = strMinute;
                    }
                }
                else
                {
                    this.form.UKETSUKE_DATE.Value = parentForm.sysDate.Date;
                    this.form.UKETSUKE_DATE_HOUR.Text = DateTime.Now.Hour.ToString();
                    this.form.UKETSUKE_DATE_MINUTE.Text = (DateTime.Now.Minute / 5 * 5).ToString();
                }

                // 受付番号
                this.form.UKETSUKE_NUMBER.Text = this.ChgDBNullToValue(dt.Rows[0]["UKETSUKE_NUMBER"], string.Empty).ToString();
                // 受付番号を使用して、ｼｮｰﾄﾒｯｾｰｼﾞ送信状況を取得
                if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    if (!string.IsNullOrEmpty(this.form.UKETSUKE_NUMBER.Text))
                    {
                        this.headerForm.SMS_SEND_JOKYO.Text = this.daoUketsukeEntry.GetSmsJokyo(this.form.UKETSUKE_NUMBER.Text);
                    }
                }

                // 取引先
                this.form.TORIHIKISAKI_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["TORIHIKISAKI_CD"], string.Empty).ToString();
                this.form.TORIHIKISAKI_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["TORIHIKISAKI_NAME"], string.Empty).ToString();

                // 業者
                this.form.GYOUSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["GYOUSHA_CD"], string.Empty).ToString();
                this.form.GYOUSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["GYOUSHA_NAME"], string.Empty).ToString();

                // 業者電話
                this.form.GYOSHA_TEL.Text = this.ChgDBNullToValue(dt.Rows[0]["GYOSHA_TEL"], string.Empty).ToString();

                // 現場
                this.form.GENBA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_CD"], string.Empty).ToString();
                this.form.GENBA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_NAME"], string.Empty).ToString();

                // 現場住所１
                this.form.GENBA_ADDRESS1.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_ADDRESS1"], string.Empty).ToString();

                // 現場住所２
                this.form.GENBA_ADDRESS2.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_ADDRESS2"], string.Empty).ToString();

                // 現場電話
                this.form.GENBA_TEL.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_TEL"], string.Empty).ToString();

                // 担当者
                this.form.TANTOSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["TANTOSHA_NAME"], string.Empty).ToString();

                // 担当携帯
                this.form.TANTOSHA_TEL.Text = this.ChgDBNullToValue(dt.Rows[0]["TANTOSHA_TEL"], string.Empty).ToString();

                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["UNPAN_GYOUSHA_CD"], string.Empty).ToString();
                this.form.UNPAN_GYOUSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["UNPAN_GYOUSHA_NAME"], string.Empty).ToString();

                // 荷降業者
                this.form.NIOROSHI_GYOUSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"], string.Empty).ToString();
                this.form.NIOROSHI_GYOUSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["NIOROSHI_GYOUSHA_NAME"], string.Empty).ToString();

                // 荷降現場
                this.form.NIOROSHI_GENBA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["NIOROSHI_GENBA_CD"], string.Empty).ToString();
                this.form.NIOROSHI_GENBA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["NIOROSHI_GENBA_NAME"], string.Empty).ToString();

                // 受付備考
                this.form.UKETSUKE_BIKOU1.Text = this.ChgDBNullToValue(dt.Rows[0]["UKETSUKE_BIKOU1"], string.Empty).ToString();
                this.form.UKETSUKE_BIKOU2.Text = this.ChgDBNullToValue(dt.Rows[0]["UKETSUKE_BIKOU2"], string.Empty).ToString();
                this.form.UKETSUKE_BIKOU3.Text = this.ChgDBNullToValue(dt.Rows[0]["UKETSUKE_BIKOU3"], string.Empty).ToString();

                // 作業日(指定)
                if (WINDOW_TYPE.NEW_WINDOW_FLAG != this.form.WindowType)
                {
                    this.form.SAGYOU_DATE.Value = this.ChgDBNullToValue(dt.Rows[0]["SAGYOU_DATE"], null);
                }
                else
                {
                    this.form.SAGYOU_DATE.Value = parentForm.sysDate.Date;
                }

                // 荷降日
                this.form.NIOROSHI_DATE.Value = this.ChgDBNullToValue(dt.Rows[0]["NIOROSHI_DATE"], null);

                // 作業日(期間)
                this.form.SAGYOU_DATE_BEGIN.Value = this.ChgDBNullToValue(dt.Rows[0]["SAGYOU_DATE_BEGIN"], null);
                this.form.SAGYOU_DATE_END.Value = this.ChgDBNullToValue(dt.Rows[0]["SAGYOU_DATE_END"], null);

                // 現着時間
                if (!String.IsNullOrEmpty(this.ChgDBNullToValue(dt.Rows[0]["GENCHAKU_TIME_CD"], string.Empty).ToString()))
                {
                    this.form.GENCHAKU_TIME_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["GENCHAKU_TIME_CD"], string.Empty).ToString().PadLeft(3, '0');
                }

                this.form.GENCHAKU_TIME_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["GENCHAKU_TIME_NAME"], string.Empty).ToString();
                if (this.TryChgTimeToHM(dt.Rows[0]["GENCHAKU_TIME"], out strHour, out strMinute))
                {
                    this.form.GENCHAKU_TIME_HOUR.Text = strHour;
                    this.form.GENCHAKU_TIME_MINUTE.Text = strMinute;
                }

                // 作業時間
                if (this.TryChgTimeToHM(dt.Rows[0]["SAGYOU_TIME"], out strHour, out strMinute))
                {
                    this.form.SAGYOU_TIME_HOUR.Text = strHour;
                    this.form.SAGYOU_TIME_MINUTE.Text = strMinute;
                }

                // 移動時間
                if (this.TryChgTimeToHM(dt.Rows[0]["SAGYOU_TIME_BEGIN"], out strHour, out strMinute))
                {
                    this.form.SAGYOU_TIME_BEGIN_HOUR.Text = strHour;
                    this.form.SAGYOU_TIME_BEGIN_MINUTE.Text = strMinute;
                }
                if (this.TryChgTimeToHM(dt.Rows[0]["SAGYOU_TIME_END"], out strHour, out strMinute))
                {
                    this.form.SAGYOU_TIME_END_HOUR.Text = strHour;
                    this.form.SAGYOU_TIME_END_MINUTE.Text = strMinute;
                }

                // 車種
                this.form.SHASHU_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["SHASHU_CD"], string.Empty).ToString();
                this.form.SHASHU_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["SHASHU_NAME"], string.Empty).ToString();

                // 新規モード以外の場合
                if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 車種台数
                    this.form.SHASHU_DAISU_NUMBER.Text = this.ChgDBNullToValue(dt.Rows[0]["SHASHU_DAISU_NUMBER"], string.Empty).ToString();
                    this.form.SHASHU_DAISU_TOTAL.Text = "/" + " " + this.groupNumber.ToString();
                }
                else
                {
                    // 車種台数
                    this.form.SHASHU_DAISU_NUMBER.Text = "1";
                }

                // 車輌
                this.form.SHARYOU_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["SHARYOU_CD"], string.Empty).ToString();
                this.form.SHARYOU_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["SHARYOU_NAME"], string.Empty).ToString();

                // 運転者
                this.form.UNTENSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["UNTENSHA_CD"], string.Empty).ToString();
                this.form.UNTENSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["UNTENSHA_NAME"], string.Empty).ToString();

                // 補助員
                this.form.HOJOIN_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["HOJOIN_CD"], string.Empty).ToString();
                this.form.HOJOIN_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["HOJOIN_NAME"], string.Empty).ToString();

                // マニ種類
                this.form.MANIFEST_SHURUI_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["MANIFEST_SHURUI_CD"], string.Empty).ToString();
                this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = this.ChgDBNullToValue(dt.Rows[0]["MANIFEST_SHURUI_NAME_RYAKU"], string.Empty).ToString();

                // マニ手配
                this.form.MANIFEST_TEHAI_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["MANIFEST_TEHAI_CD"], string.Empty).ToString();
                this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = this.ChgDBNullToValue(dt.Rows[0]["MANIFEST_TEHAI_NAME_RYAKU"], string.Empty).ToString();

                // コンテナ操作
                this.form.CONTENA_SOUSA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["CONTENA_SOUSA_CD"], string.Empty).ToString();
                this.form.CONTENA_SOUSA_NAME_RYAKU.Text = this.ChgDBNullToValue(dt.Rows[0]["CONTENA_JOUKYOU_NAME_RYAKU"], string.Empty).ToString();

                // 設置
                this.form.SETTI_DAISUU.Text = this.settiDaisu.ToString();

                // 引揚
                this.form.HIKIAGE_DAISUU.Text = this.hikiageDaisu.ToString();

                // コース組込
                this.form.COURSE_KUMIKOMI_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["COURSE_KUMIKOMI_CD"], string.Empty).ToString();
                this.form.COURSE_KUMIKOMI_NAME.Text = this.GetCourseKumikomiName(this.form.COURSE_KUMIKOMI_CD.Text);

                // コース名
                this.form.COURSE_NAME_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["COURSE_NAME_CD"], string.Empty).ToString();
                this.form.COURSE_NAME_RYAKU.Text = this.ChgDBNullToValue(dt.Rows[0]["COURSE_NAME_RYAKU"], string.Empty).ToString();

                // 運転者指示事項
                this.form.UNTENSHA_SIJIJIKOU1.Text = this.ChgDBNullToValue(dt.Rows[0]["UNTENSHA_SIJIJIKOU1"], string.Empty).ToString();
                this.form.UNTENSHA_SIJIJIKOU2.Text = this.ChgDBNullToValue(dt.Rows[0]["UNTENSHA_SIJIJIKOU2"], string.Empty).ToString();
                this.form.UNTENSHA_SIJIJIKOU3.Text = this.ChgDBNullToValue(dt.Rows[0]["UNTENSHA_SIJIJIKOU3"], string.Empty).ToString();

                // No.2840-->
                if (this.form.insHaisyaList != null)
                {
                    //if (this.form.SHASHU_CD.Text.Equals("") || this.form.SHARYOU_CD.Text.Equals("") && this.form.UNTENSHA_CD.Text.Equals(""))
                    //{
                    this.form.SHASHU_CD.Text = this.form.insHaisyaList[0];
                    this.form.SHASHU_NAME.Text = this.form.insHaisyaList[1];
                    this.form.SHARYOU_CD.Text = this.form.insHaisyaList[2];
                    this.form.SHARYOU_NAME.Text = this.form.insHaisyaList[3];
                    this.form.UNTENSHA_CD.Text = this.form.insHaisyaList[4];
                    this.form.UNTENSHA_NAME.Text = this.form.insHaisyaList[5];
                    this.form.UNPAN_GYOUSHA_CD.Text = this.form.insHaisyaList[6];
                    this.form.UNPAN_GYOUSHA_NAME.Text = this.form.insHaisyaList[7];
                    //}
                }
                // No.2840<--

                // 明細データを設定
                dt = this.dtUketsukeDetail;
                dt.BeginLoadData();

                // 明細クリア
                this.dgvDetail.Rows.Clear();
                if (dt.Rows.Count == 0)
                {
                    return;
                }

                // 画面にデータを表示

                // 数量フォーマット
                String systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
                // 単価フォーマット
                String systemTankaFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();

                // 明細行を追加
                this.dgvDetail.Rows.Add(dt.Rows.Count);
                // 検索結果設定
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 行NO
                    this.dgvDetail["ROW_NO", i].Value = dt.Rows[i]["ROW_NO"];

                    // 品名CD
                    this.dgvDetail["HINMEI_CD", i].Value = dt.Rows[i]["HINMEI_CD"];
                    // 品名
                    this.dgvDetail["HINMEI_NAME", i].Value = this.ChgDBNullToValue(dt.Rows[i]["HINMEI_NAME"], string.Empty);
                    // 伝票CD（非表示）
                    this.dgvDetail["DENPYOU_KBN_CD", i].Value = this.ChgDBNullToValue(dt.Rows[i]["DENPYOU_KBN_CD"], string.Empty);
                    // 伝票区分
                    this.dgvDetail["DENPYOU_KBN_NAME_RYAKU", i].Value = this.ChgDBNullToValue(dt.Rows[i]["DENPYOU_KBN_NAME_RYAKU"], string.Empty);
                    // 数量
                    this.dgvDetail["SUURYOU", i].Value = this.SuuryouAndTankFormat(this.ChgDBNullToValue(dt.Rows[i]["SUURYOU"], null), systemSuuryouFormat);
                    // 単位ＣＤ
                    this.dgvDetail["UNIT_CD", i].Value = this.ChgDBNullToValue(dt.Rows[i]["UNIT_CD"], string.Empty).ToString();
                    // 単位
                    this.dgvDetail["UNIT_NAME_RYAKU", i].Value = this.ChgDBNullToValue(dt.Rows[i]["UNIT_NAME_RYAKU"], string.Empty);
                    // 単価
                    this.dgvDetail["TANKA", i].Value = this.SuuryouAndTankFormat(this.ChgDBNullToValue(dt.Rows[i]["TANKA"], null), systemTankaFormat);
                    // 明細備考
                    this.dgvDetail["MEISAI_BIKOU", i].Value = this.ChgDBNullToValue(dt.Rows[i]["MEISAI_BIKOU"], string.Empty);
                    // 明細システムID
                    this.dgvDetail["DETAIL_SYSTEM_ID", i].Value = this.ChgDBNullToValue(dt.Rows[i]["DETAIL_SYSTEM_ID"], string.Empty);
                    // TIME_STAMP
                    this.dgvDetail["TIME_STAMP", i].Value = this.ChgDBNullToValue(dt.Rows[i]["TIME_STAMP"], string.Empty);
                    // 金額
                    if (dt.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["KINGAKU"].ToString()))
                    {
                        this.dgvDetail["HINMEI_KINGAKU", i].Value = CommonCalc.DecimalFormat((decimal)this.ChgDBNullToValue(dt.Rows[i]["KINGAKU"], 0M));
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
                // 単価と金額の活性/非活性制御
                this.form.SetIchranReadOnlyForAll();

                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region 各マスター情報を取得
        /// <summary>
        /// 取引CDで取引先を取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先エンティティ</returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                var keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();

                if (null != torihikisaki)
                {
                    string strBegin = torihikisaki.TEKIYOU_BEGIN.ToString();
                    string strEnd = torihikisaki.TEKIYOU_END.ToString();
                    string sagyobi = string.Empty;
                    if (this.form.SAGYOU_DATE.Value != null)
                    {
                        sagyobi = this.form.SAGYOU_DATE.Value.ToString();
                    }
                    else
                    {
                        sagyobi = this.parentForm.sysDate.Date.ToString();
                    }

                    if (torihikisaki.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (torihikisaki.TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            LogUtility.DebugMethodEnd(null, catchErr);
                            return null;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(torihikisaki, catchErr);

                return torihikisaki;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 業者CDで業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>業者エンティティ</returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                var keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity).FirstOrDefault();


                if (null != gyousha)
                {
                    string strBegin = gyousha.TEKIYOU_BEGIN.ToString();
                    string strEnd = gyousha.TEKIYOU_END.ToString();
                    string sagyobi = string.Empty;
                    if (this.form.SAGYOU_DATE.Value != null)
                    {
                        sagyobi = this.form.SAGYOU_DATE.Value.ToString();
                    }
                    else
                    {
                        sagyobi = this.parentForm.sysDate.Date.ToString();
                    }

                    if (gyousha.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (gyousha.TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            LogUtility.DebugMethodEnd(null, catchErr);
                            return null;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(gyousha, catchErr);

                return gyousha;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 現場CDと業者CDで現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティ</returns>
        public M_GENBA GetGenba(string genbaCd, string gyoushaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                var keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity).FirstOrDefault();


                if (null != genba)
                {
                    string strBegin = genba.TEKIYOU_BEGIN.ToString();
                    string strEnd = genba.TEKIYOU_END.ToString();
                    string sagyobi = string.Empty;
                    if (this.form.SAGYOU_DATE.Value != null)
                    {
                        sagyobi = this.form.SAGYOU_DATE.Value.ToString();
                    }
                    else
                    {
                        sagyobi = this.parentForm.sysDate.Date.ToString();
                    }

                    if (genba.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (genba.TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            LogUtility.DebugMethodEnd(null, catchErr);
                            return null;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(genba, catchErr);

                return genba;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 業者CD、現場CDで現場リストを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティリスト</returns>
        public M_GENBA[] GetGenbaList(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            List<M_GENBA> retlist = new List<M_GENBA>();
            IEnumerable<M_GENBA> gList = null;

            var keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            gList = this.genbaDao.GetAllValidData(keyEntity);


            if (null != gList && gList.Count() != 0)
            {
                M_GENBA[] temp = gList.ToArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    string strBegin = temp[i].TEKIYOU_BEGIN.ToString();
                    string strEnd = temp[i].TEKIYOU_END.ToString();
                    string sagyobi = string.Empty;
                    if (this.form.SAGYOU_DATE.Value != null)
                    {
                        sagyobi = this.form.SAGYOU_DATE.Value.ToString();
                    }
                    else
                    {
                        sagyobi = this.parentForm.sysDate.Date.ToString();
                    }

                    if (temp[i].TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (temp[i].TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            continue;
                        }
                        else
                        {
                            retlist.Add(temp[i]);
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd();

            return retlist.ToArray();
        }

        /// <summary>
        /// 社員CDで社員を取得します
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns>社員エンティティ</returns>
        public M_SHAIN GetShain(string shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            // 取得済みの社員リストから取得
            var shain = this.shainList.Where(g => g.SHAIN_CD == shainCd).FirstOrDefault();
            if (null == shain)
            {
                // なければDBから取得
                var keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                shain = this.shainDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (null != shain)
                {
                    this.shainList.Add(shain);
                }
            }

            LogUtility.DebugMethodEnd();

            return shain;
        }

        /// <summary>
        /// 単位区分取得
        /// </summary>
        /// <param name="unitCd">単位区分CD</param>
        /// <returns></returns>
        public M_UNIT[] GetUnit(short unitCd)
        {
            M_UNIT[] returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(unitCd);

                if (unitCd < 0)
                {
                    return returnVal;
                }

                M_UNIT keyEntity = new M_UNIT();
                keyEntity.UNIT_CD = unitCd;
                var units = this.unitDao.GetAllValidData(keyEntity);
                if (units != null && units.Length > 0)
                {
                    returnVal = units;
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
        /// 品名テーブルの情報を取得
        /// 適用開始日、終了日、削除フラグについては
        /// 有効なものだけを検索します
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_HINMEI[] GetAllValidHinmeiData(string key = null)
        {
            M_HINMEI[] returnVal = null;

            try
            {
                LogUtility.DebugMethodStart(key);

                M_HINMEI keyEntity = new M_HINMEI();
                if (!string.IsNullOrEmpty(key))
                {
                    keyEntity.HINMEI_CD = key;
                }

                returnVal = this.hinmeiDao.GetAllValidData(keyEntity);
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
        /// 品名テーブルの情報を取得
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_HINMEI GetHinmeiDataByCd(string key)
        {
            LogUtility.DebugMethodStart(key);

            var keyEntity = new M_HINMEI();
            keyEntity.HINMEI_CD = key;
            var hinmei = this.hinmeiDao.GetAllValidData(keyEntity).FirstOrDefault();


            LogUtility.DebugMethodEnd();

            return hinmei;

        }

        /// <summary>
        /// 伝票区分一覧取得
        /// </summary>
        /// <returns></returns>
        public M_DENPYOU_KBN[] GetAllDenpyouKbn()
        {
            M_DENPYOU_KBN[] returnVal = null;
            try
            {
                LogUtility.DebugMethodStart();

                M_DENPYOU_KBN keyEntity = new M_DENPYOU_KBN();
                returnVal = this.denpyouKbnDao.GetAllValidData(keyEntity);
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
        /// 単位一覧取得
        /// </summary>
        /// <returns></returns>
        public M_UNIT[] GetAllUnit()
        {
            M_UNIT[] returnVal = null;
            try
            {
                LogUtility.DebugMethodStart();

                M_UNIT keyEntity = new M_UNIT();
                returnVal = this.unitDao.GetAllValidData(keyEntity);
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
        /// 車輌取得
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        public M_SHARYOU GetSharyou(string gyoushaCd, string sharyouCd)
        {
            M_SHARYOU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                M_SHARYOU keyEntity = new M_SHARYOU();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.SHARYOU_CD = sharyouCd;
                var sharyou = this.sharyouDao.GetAllValidData(keyEntity);

                if (sharyou != null && sharyou.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = sharyou[0];
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
        /// 車輌取得(複数)
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyouList(string sharyouCd)
        {
            M_SHARYOU[] returnVal = null;

            try
            {
                LogUtility.DebugMethodStart(sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                M_SHARYOU keyEntity = new M_SHARYOU();
                keyEntity.SHARYOU_CD = sharyouCd;
                var sharyou = this.sharyouDao.GetAllValidData(keyEntity);

                if (sharyou != null && sharyou.Length > 0)
                {
                    returnVal = sharyou;
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
        /// 車種取得
        /// </summary>
        /// <param name="shashuCd">車種CD</param>
        /// <returns></returns>
        public M_SHASHU GetSharshu(string shashuCd)
        {
            M_SHASHU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shashuCd);

                if (string.IsNullOrEmpty(shashuCd))
                {
                    return returnVal;
                }

                M_SHASHU keyEntity = new M_SHASHU();
                keyEntity.SHASHU_CD = shashuCd;
                var shashu = this.shashuDao.GetAllValidData(keyEntity);

                if (shashu != null && shashu.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = shashu[0];
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
        /// 現着時間略名を取得
        /// </summary>
        /// <param name="genshakuCd">現着時間CD</param>
        /// <returns>現着時間略名</returns>
        private string GetGenshakuTimeName(short genshakuCd)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(genshakuCd);

                M_GENCHAKU_TIME keyEntity = new M_GENCHAKU_TIME();
                keyEntity.GENCHAKU_TIME_CD = genshakuCd;
                var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENCHAKU_TIMEDao>();
                var result = dao.GetAllValidData(keyEntity);

                if (result != null && result.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = result[0].GENCHAKU_TIME_NAME_RYAKU;
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
        /// マニフェスト種類CDでマニフェスト種類を取得します
        /// </summary>
        /// <param name="manifestShuruiCd">マニフェスト種類CD</param>
        /// <returns>マニフェスト種類エンティティ</returns>
        public M_MANIFEST_SHURUI GetManifestShurui(SqlInt16 manifestShuruiCd)
        {
            LogUtility.DebugMethodStart(manifestShuruiCd);

            // 取得済みのマニフェスト種類リストから取得
            var manifestShurui = this.manifestShuruiList.Where(m => (bool)(m.MANIFEST_SHURUI_CD == manifestShuruiCd)).FirstOrDefault();
            if (null == manifestShurui)
            {
                // なければDBから取得
                var keyEntity = new M_MANIFEST_SHURUI();
                keyEntity.MANIFEST_SHURUI_CD = manifestShuruiCd;
                var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_SHURUIDao>();
                manifestShurui = dao.GetAllValidData(keyEntity).FirstOrDefault();
                if (null != manifestShurui)
                {
                    this.manifestShuruiList.Add(manifestShurui);
                }
            }

            LogUtility.DebugMethodEnd();

            return manifestShurui;
        }

        /// <summary>
        /// マニフェスト手配CDでマニフェスト手配を取得します
        /// </summary>
        /// <param name="manifestTehaiCd">マニフェスト手配CD</param>
        /// <returns>マニフェスト手配エンティティ</returns>
        public M_MANIFEST_TEHAI GetManifestTehai(SqlInt16 manifestTehaiCd)
        {
            LogUtility.DebugMethodStart(manifestTehaiCd);

            // 取得済みのマニフェスト手配リストから取得
            var manifestTehai = this.manifestTehaiList.Where(m => (bool)(m.MANIFEST_TEHAI_CD == manifestTehaiCd)).FirstOrDefault();
            if (null == manifestTehai)
            {
                // なければDBから取得
                var keyEntity = new M_MANIFEST_TEHAI();
                keyEntity.MANIFEST_TEHAI_CD = manifestTehaiCd;
                var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_TEHAIDao>();
                manifestTehai = dao.GetAllValidData(keyEntity).FirstOrDefault();
                if (null != manifestTehai)
                {
                    this.manifestTehaiList.Add(manifestTehai);
                }
            }

            LogUtility.DebugMethodEnd();

            return manifestTehai;
        }

        #endregion

        #region 各マスターチェック

        #region 取引先チェック
        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;

                // Readonly初期化
                this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, true);

                // 入力されていない場合
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    // 関連項目クリア
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;

                    if (!this.form.oldShokuchiKbn || this.form.Key.Shift)
                    {
                        // フレームワーク側のフォーカス処理を行わない
                        this.form.isNotMoveFocusFW = true;
                    }
                    else
                    {
                        // フレームワーク側のフォーカス処理を行う
                        this.form.isNotMoveFocusFW = false;
                    }

                    return true;
                }

                // 取引先を取得
                bool catchErr = true;
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (torihikisaki == null)
                {
                    // 取引先名設定
                    this.form.TORIHIKISAKI_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "取引先");

                    this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                    return false;
                }

                // 取引先と拠点の関係をチェック
                if (false == this.CheckTorihikisakiKyoten(this.headerForm.KYOTEN_CD.Text, torihikisakiCd))
                {
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;

                    this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                    return false;
                }

                // 取引先を設定
                if (!this.form.dicControl.ContainsKey("TORIHIKISAKI_CD") || !this.form.dicControl["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                {
                    // 取引先名設定
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisaki.SHOKUCHI_KBN);

                // 諸口区分によってフォーカスを制御
                if ((bool)torihikisaki.SHOKUCHI_KBN)
                {
                    this.form.SetFocusControl(this.form.TORIHIKISAKI_NAME);
                }
                else
                {
                    if (this.form.oldShokuchiKbn)
                    {
                        this.form.isNotMoveFocusFW = false;
                    }
                    return true;
                }

                //コントロールにフォーカスを設定します
                if (!this.form.boolMoveFocusControl())
                {
                    // フレームワーク側のフォーカス処理を行わない
                    this.form.isNotMoveFocusFW = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // 営業担当者を設定
                this.SetEigyouTantousha();

                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先と拠点の関係をチェックします
        /// </summary>
        /// <param name="headerKyotenCd">拠点CD</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckTorihikisakiKyoten(string headerKyotenCd, string torihikisakiCd)
        {
            try
            {
                //取引先が空だったらReturn
                if (string.Empty == torihikisakiCd)
                {
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                    return true;
                }

                // 取引先の拠点をチェック
                if (String.IsNullOrEmpty(headerKyotenCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E146");

                    this.isInputError = true;
                    return false;
                }

                string oldTorihikisakiCd = string.Empty;
                string oldKyotenCd = string.Empty;
                if (this.dtUketsukeEntry != null && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    oldTorihikisakiCd = this.ChgDBNullToValue(this.dtUketsukeEntry.Rows[0]["TORIHIKISAKI_CD"], string.Empty).ToString();
                    oldKyotenCd = this.FieldPadLeft(dtUketsukeEntry.Rows[0]["KYOTEN_CD"], 2, '0');
                }
                if (torihikisakiCd == oldTorihikisakiCd && headerKyotenCd == oldKyotenCd)
                {
                    return true;
                }

                bool catchErr = true;
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                if (null == torihikisaki)
                {
                    // 取引先名設定
                    this.form.TORIHIKISAKI_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "取引先");

                    this.isInputError = true;
                    return false;
                }

                var kyotenCd = (int)torihikisaki.TORIHIKISAKI_KYOTEN_CD;
                if (99 != kyotenCd && Convert.ToInt16(headerKyotenCd) != kyotenCd)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E146");

                    this.isInputError = true;
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisakiKyoten", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiKyoten", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 業者チェック
        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var torihikisakiName = this.form.TORIHIKISAKI_NAME.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var gyoushaName = this.form.GYOUSHA_NAME.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                var genbaName = this.form.GENBA_NAME.Text;

                // Readonly初期化
                this.SetCtrlReadonly(this.form.GYOUSHA_NAME, true);
                this.SetCtrlReadonly(this.form.GYOSHA_TEL, true);
                this.SetCtrlReadonly(this.form.GENBA_NAME, true);
                this.SetCtrlReadonly(this.form.GENBA_ADDRESS1, true);
                this.SetCtrlReadonly(this.form.GENBA_ADDRESS2, true);
                this.SetCtrlReadonly(this.form.GENBA_TEL, true);
                this.SetCtrlReadonly(this.form.TANTOSHA_NAME, true);
                this.SetCtrlReadonly(this.form.TANTOSHA_TEL, true);

                // 現場項目クリア
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME.Text = String.Empty;
                this.form.GENBA_ADDRESS1.Text = String.Empty;
                this.form.GENBA_ADDRESS2.Text = String.Empty;
                this.form.GENBA_TEL.Text = String.Empty;
                this.form.TANTOSHA_NAME.Text = String.Empty;
                this.form.TANTOSHA_TEL.Text = String.Empty;
                //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
                this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
                this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
                //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end

                // 入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    // 関連項目クリア
                    this.form.GYOUSHA_NAME.Text = String.Empty;
                    this.form.GYOSHA_TEL.Text = String.Empty;

                    if (!this.form.oldShokuchiKbn || this.form.Key.Shift)
                    {
                        // フレームワーク側のフォーカス処理を行わない
                        this.form.isNotMoveFocusFW = true;
                    }
                    else
                    {
                        // フレームワーク側のフォーカス処理を行う
                        this.form.isNotMoveFocusFW = false;
                    }

                    return true;
                }

                // 業者を取得
                // (入力エラーチェックは前回値と比較する前に行っているためここではチェックしない）
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                // 業者を設定
                if (!this.form.dicControl.ContainsKey("GYOUSHA_CD") || !this.form.dicControl["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)gyousha.SHOKUCHI_KBN)
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    this.form.GYOSHA_TEL.Text = gyousha.GYOUSHA_TEL;
                }
                this.SetCtrlReadonly(this.form.GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);
                this.SetCtrlReadonly(this.form.GYOSHA_TEL, !(bool)gyousha.SHOKUCHI_KBN);

                // 取引先を再設定
                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(gyousha.TORIHIKISAKI_CD, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = gyousha.TORIHIKISAKI_CD;

                    // 取引先と拠点の関係をチェック
                    if (false == this.CheckTorihikisakiKyoten(this.headerForm.KYOTEN_CD.Text, gyousha.TORIHIKISAKI_CD))
                    {
                        this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.isInputError = true;
                        return false;
                    }

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisaki.SHOKUCHI_KBN);
                }

                // 諸口区分によってフォーカスを制御
                var zenShokuchi = (bool)gyousha.SHOKUCHI_KBN;
                if ((bool)gyousha.SHOKUCHI_KBN)
                {
                    this.form.SetFocusControl(this.form.GYOUSHA_NAME);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        if (this.form.oldShokuchiKbn)
                        {
                            this.form.isNotMoveFocusFW = false;
                        }
                        return true;
                    }
                }

                // 現場を再設定
                // 現場を取得
                var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != genba)
                {
                    this.form.GENBA_CD.Text = genba.GENBA_CD;
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    this.form.GENBA_ADDRESS1.Text = genba.GENBA_ADDRESS1;
                    this.form.GENBA_ADDRESS2.Text = genba.GENBA_ADDRESS2;
                    this.form.GENBA_TEL.Text = genba.GENBA_TEL;
                    this.form.TANTOSHA_NAME.Text = genba.TANTOUSHA;
                    this.form.TANTOSHA_TEL.Text = genba.GENBA_KEITAI_TEL;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                    this.form.UNTENSHA_SIJIJIKOU1.Text = genba.UNTENSHA_SHIJI_JIKOU1;
                    this.form.UNTENSHA_SIJIJIKOU2.Text = genba.UNTENSHA_SHIJI_JIKOU2;
                    this.form.UNTENSHA_SIJIJIKOU3.Text = genba.UNTENSHA_SHIJI_JIKOU3;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end

                    this.SetCtrlReadonly(this.form.GENBA_NAME, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_ADDRESS1, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_ADDRESS2, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_TEL, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.TANTOSHA_NAME, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.TANTOSHA_TEL, !(bool)genba.SHOKUCHI_KBN);
                }

                //コントロールにフォーカスを設定します
                if (!this.form.boolMoveFocusControl())
                {
                    // フレームワーク側のフォーカス処理を行わない
                    this.form.isNotMoveFocusFW = true;
                }
                this.RirekeShow(); //CongBinh 20210713 #152801

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // 営業担当者を設定
                this.SetEigyouTantousha();

                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 業者CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGyousha()
        {
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            bool ren = true;

            if (!String.IsNullOrEmpty(gyoushaCd))
            {
                // 業者を取得
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null == gyousha)
                {
                    // 業者名、業者電話設定
                    this.form.GYOUSHA_NAME.Text = String.Empty;
                    this.form.GYOSHA_TEL.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "業者");

                    this.isInputError = true;
                    this.form.SetFocusControl(this.form.GYOUSHA_CD);
                    ren = false;
                }
                else
                {
                    // nullはFalseとして扱う
                    if (false == gyousha.GYOUSHAKBN_UKEIRE.IsTrue)
                    {
                        // 業者名、業者電話設定
                        this.form.GYOUSHA_NAME.Text = String.Empty;
                        this.form.GYOSHA_TEL.Text = String.Empty;
                        this.msgLogic.MessageBoxShow("E058");

                        this.isInputError = true;
                        this.form.SetFocusControl(this.form.GYOUSHA_CD);
                        ren = false;
                    }
                }
            }
            return ren;
        }

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                var eigyouTantoushaCd = this.form.EIGYOU_TANTOUSHA_CD.Text;

                // Readonly初期化
                this.SetCtrlReadonly(this.form.GENBA_NAME, true);
                this.SetCtrlReadonly(this.form.GENBA_ADDRESS1, true);
                this.SetCtrlReadonly(this.form.GENBA_ADDRESS2, true);
                this.SetCtrlReadonly(this.form.GENBA_TEL, true);
                this.SetCtrlReadonly(this.form.TANTOSHA_NAME, true);
                this.SetCtrlReadonly(this.form.TANTOSHA_TEL, true);

                // 入力されてない場合
                if (String.IsNullOrEmpty(genbaCd))
                {
                    if (!this.form.oldShokuchiKbn || this.form.Key.Shift)
                    {
                        // フレームワーク側のフォーカス処理を行わない
                        this.form.isNotMoveFocusFW = true;
                    }
                    else
                    {
                        // フレームワーク側のフォーカス処理を行う
                        this.form.isNotMoveFocusFW = false;
                    }

                    return true;
                }

                // 現場情報を取得
                if (this.GetGenbaList(gyoushaCd, genbaCd).Count() == 0)
                {
                    // マスタに現場が存在しない場合
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.form.GENBA_ADDRESS1.Text = String.Empty;
                    this.form.GENBA_ADDRESS2.Text = String.Empty;
                    this.form.GENBA_TEL.Text = String.Empty;
                    this.form.TANTOSHA_NAME.Text = String.Empty;
                    this.form.TANTOSHA_TEL.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");

                    this.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    return false;
                }

                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                this.RirekeShow(); //CongBinh 20210713 #152801
                var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                //マスタに業者CDが存在しない場合
                //又は取引日外の業者CDが選択された場合

                if (null == genba)
                {
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.form.GENBA_ADDRESS1.Text = String.Empty;
                    this.form.GENBA_ADDRESS2.Text = String.Empty;
                    this.form.GENBA_TEL.Text = String.Empty;
                    this.form.TANTOSHA_NAME.Text = String.Empty;
                    this.form.TANTOSHA_TEL.Text = String.Empty;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                    this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
                    this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
                    this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end
                    this.msgLogic.MessageBoxShow("E062", "業者");

                    this.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    return false;
                }
                else
                {
                    // 現場が見つかったので現場名などをセット
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    this.form.GENBA_ADDRESS1.Text = genba.GENBA_ADDRESS1;
                    this.form.GENBA_ADDRESS2.Text = genba.GENBA_ADDRESS2;
                    this.form.GENBA_TEL.Text = genba.GENBA_TEL;
                    this.form.TANTOSHA_NAME.Text = genba.TANTOUSHA;
                    this.form.TANTOSHA_TEL.Text = genba.GENBA_KEITAI_TEL;

                    this.SetCtrlReadonly(this.form.GENBA_NAME, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_ADDRESS1, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_ADDRESS2, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_TEL, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.TANTOSHA_NAME, !(bool)genba.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.TANTOSHA_TEL, !(bool)genba.SHOKUCHI_KBN);

                    torihikisakiCd = genba.TORIHIKISAKI_CD;

                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                    this.form.UNTENSHA_SIJIJIKOU1.Text = genba.UNTENSHA_SHIJI_JIKOU1;
                    this.form.UNTENSHA_SIJIJIKOU2.Text = genba.UNTENSHA_SHIJI_JIKOU2;
                    this.form.UNTENSHA_SIJIJIKOU3.Text = genba.UNTENSHA_SHIJI_JIKOU3;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end
                }

                // 業者を設定
                if (!this.form.dicControl.ContainsKey("GYOUSHA_CD") || !this.form.dicControl["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    // 業者名
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyousha.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    // 業者TEL
                    // ポップアップからの入力だった場合
                    if (this.form.popupFlg)
                    {
                        // ポップアップがキャンセルされたかを見れないので、業者の空チェックで判断
                        if (!string.IsNullOrEmpty(gyoushaCd))
                        {
                            this.form.GYOSHA_TEL.Text = gyousha.GYOUSHA_TEL;
                            this.form.popupFlg = false;
                        }
                    }
                }

                this.SetCtrlReadonly(this.form.GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);
                this.SetCtrlReadonly(this.form.GYOSHA_TEL, !(bool)gyousha.SHOKUCHI_KBN);

                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != torihikisaki)
                {
                    // 取引先設定
                    this.form.TORIHIKISAKI_CD.Text = torihikisaki.TORIHIKISAKI_CD;

                    // 取引先と拠点の関係をチェック
                    if (false == this.CheckTorihikisakiKyoten(this.headerForm.KYOTEN_CD.Text, torihikisakiCd))
                    {
                        this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.isInputError = true;
                        return false;
                    }

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisaki.SHOKUCHI_KBN);
                }

                // マニ種類の自動表示
                // 初期化
                this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;
                if (!genba.MANIFEST_SHURUI_CD.IsNull)
                {
                    var manifestShuruiEntity = this.GetManifestShurui(genba.MANIFEST_SHURUI_CD);
                    if (manifestShuruiEntity != null)
                    {
                        this.form.MANIFEST_SHURUI_CD.Text = genba.MANIFEST_SHURUI_CD.ToString();
                        this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                    }
                }

                // マニ手配の自動表示
                // 初期化
                this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;
                if (!genba.MANIFEST_TEHAI_CD.IsNull)
                {
                    var manifestTehaiEntity = this.GetManifestTehai(genba.MANIFEST_TEHAI_CD);
                    if (manifestTehaiEntity != null)
                    {
                        this.form.MANIFEST_TEHAI_CD.Text = genba.MANIFEST_TEHAI_CD.ToString();
                        this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                    }
                }

                // 諸口区分によってフォーカスを制御
                if ((bool)genba.SHOKUCHI_KBN)
                {
                    this.form.SetFocusControl(this.form.GENBA_NAME);
                }
                else
                {
                    if (this.form.oldShokuchiKbn)
                    {
                        this.form.isNotMoveFocusFW = false;
                    }
                    return true;
                }

                //コントロールにフォーカスを設定します
                if (!this.form.boolMoveFocusControl())
                {
                    // フレームワーク側のフォーカス処理を行わない
                    this.form.isNotMoveFocusFW = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // 営業担当者を設定
                this.SetEigyouTantousha();

                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 現場CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGenba()
        {
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            var genbaCd = this.form.GENBA_CD.Text;

            bool ren = true;

            if (string.IsNullOrEmpty(genbaCd))
            {
                // 現場の関連情報をクリア
                this.form.GENBA_NAME.Text = String.Empty;
                this.form.GENBA_ADDRESS1.Text = String.Empty;
                this.form.GENBA_ADDRESS2.Text = String.Empty;
                this.form.GENBA_TEL.Text = String.Empty;
                this.form.TANTOSHA_NAME.Text = String.Empty;
                this.form.TANTOSHA_TEL.Text = String.Empty;
                //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
                this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
                this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
                //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end
                return ren;
            }

            // 業者入力されてない場合
            if (String.IsNullOrEmpty(gyoushaCd))
            {
                this.msgLogic.MessageBoxShow("E051", "業者");
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME.Text = String.Empty;
                this.isInputError = true;
                this.form.SetFocusControl(this.form.GENBA_CD);
                ren = false;
                return ren;
            }

            // 現場情報を取得
            if (!string.IsNullOrEmpty(genbaCd))
            {
                if (this.GetGenbaList(gyoushaCd, genbaCd).Count() == 0)
                {
                    // マスタに現場が存在しない場合
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.form.GENBA_ADDRESS1.Text = String.Empty;
                    this.form.GENBA_ADDRESS2.Text = String.Empty;
                    this.form.GENBA_TEL.Text = String.Empty;
                    this.form.TANTOSHA_NAME.Text = String.Empty;
                    this.form.TANTOSHA_TEL.Text = String.Empty;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                    this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
                    this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
                    this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end
                    this.msgLogic.MessageBoxShow("E020", "現場");

                    this.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    ren = false;
                    return ren;
                }
            }

            bool catchErr = true;
            var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
            if (!catchErr)
            {
                return false;
            }
            if (null == gyousha)
            {
                // 業者及び現場の関連情報をクリア
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME.Text = String.Empty;
                this.form.GENBA_ADDRESS1.Text = String.Empty;
                this.form.GENBA_ADDRESS2.Text = String.Empty;
                this.form.GENBA_TEL.Text = String.Empty;
                this.form.GYOUSHA_NAME.Text = String.Empty;
                //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
                this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
                this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
                //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end
                this.msgLogic.MessageBoxShow("E020", "業者");

                this.isInputError = true;
                this.form.SetFocusControl(this.form.GENBA_CD);
                ren = false;
            }
            else
            {
                // nullはFalseとして扱う
                if (false == gyousha.GYOUSHAKBN_UKEIRE.IsTrue)
                {
                    // 業者及び現場の関連情報をクリア
                    this.form.GENBA_CD.Text = String.Empty;
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.form.GENBA_ADDRESS1.Text = String.Empty;
                    this.form.GENBA_ADDRESS2.Text = String.Empty;
                    this.form.GENBA_TEL.Text = String.Empty;
                    this.form.GYOUSHA_NAME.Text = String.Empty;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm start
                    this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
                    this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
                    this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
                    //20150609 #10699 現場に登録されている運転者指示事項をセット hoanghm end
                    this.msgLogic.MessageBoxShow("E062", "業者");

                    this.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    ren = false;
                }
            }
            return ren;
        }

        /// <summary>
        /// 営業担当者を設定します
        /// （現場の営業担当者 → 業者の営業担当者 → 取引先の営業担当者の優先順）
        /// </summary>
        private void SetEigyouTantousha()
        {
            var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            var genbaCd = this.form.GENBA_CD.Text;
            this.form.EIGYOU_TANTOUSHA_CD.Text = String.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = String.Empty;

            // 取引先があればセット
            bool catchErr = true;
            var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != torihikisaki && !String.IsNullOrEmpty(torihikisaki.EIGYOU_TANTOU_CD))
            {
                // 取得した取引先の営業担当CDが設定されている場合
                this.form.EIGYOU_TANTOUSHA_CD.Text = torihikisaki.EIGYOU_TANTOU_CD;
            }

            // 業者があればセット
            var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != gyousha && !String.IsNullOrEmpty(gyousha.EIGYOU_TANTOU_CD))
            {
                // 取得した業者の営業担当CDが設定されている場合
                this.form.EIGYOU_TANTOUSHA_CD.Text = gyousha.EIGYOU_TANTOU_CD;
            }

            // 現場があればセット
            var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != genba && !String.IsNullOrEmpty(genba.EIGYOU_TANTOU_CD))
            {
                // 取得した現場の営業担当CDが設定されている場合
                this.form.EIGYOU_TANTOUSHA_CD.Text = genba.EIGYOU_TANTOU_CD;
            }

            // 営業担当者CDが設定されたら名称を設定
            var eigyouTantoushaCd = this.form.EIGYOU_TANTOUSHA_CD.Text;
            if (!String.IsNullOrEmpty(eigyouTantoushaCd))
            {
                var shain = this.GetShain(eigyouTantoushaCd);
                if (null != shain)
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shain.SHAIN_NAME_RYAKU;
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = String.Empty;
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = String.Empty;
                }
            }
        }

        #region 運搬業者チェック
        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal bool CheckUnpanGyoushaCd()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // Readonly初期化
                this.SetCtrlReadonly(this.form.UNPAN_GYOUSHA_NAME, true);

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 運搬業者名を設定
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                    // 処理終了
                    returnVal = true;
                    return true;
                }

                // 業者情報取得
                bool catchErr = true;
                var gyoushaEntity = this.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return returnVal;
                }
                // 取得できない場合
                if (gyoushaEntity == null)
                {
                    // 背景色変更
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    // 運搬業者名を設定
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "運搬業者");

                    this.isInputError = true;
                    // 処理終了
                    return returnVal;
                }

                // 区分
                // 20151026 BUNN #12040 STR
                if (!gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    // 背景色変更
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    this.msgLogic.MessageBoxShow("E062", "運搬業者");

                    this.isInputError = true;
                    return returnVal;
                }

                // 業者区分チェック（nullはFalseとして扱う）
                if (false == gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue)
                {
                    this.msgLogic.MessageBoxShow("E058");

                    this.isInputError = true;
                    return returnVal;
                }

                // 変更ありの場合
                if (!this.form.dicControl.ContainsKey("UNPAN_GYOUSHA_CD") ||
                    !this.form.dicControl["UNPAN_GYOUSHA_CD"].Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 運搬業者名を設定
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }

                // 諸口区分チェック
                if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                {
                    // 運搬業者名編集可
                    this.SetCtrlReadonly(this.form.UNPAN_GYOUSHA_NAME, false);
                }

                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region 荷降業者チェック
        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // Readonly初期化
                this.SetCtrlReadonly(this.form.NIOROSHI_GYOUSHA_NAME, true);

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // 荷降業者名を設定
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    // 処理終了
                    returnVal = true;
                    return true;
                }

                // 業者情報取得
                bool catchErr = true;
                var gyoushaEntity = this.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return returnVal;
                }
                // 取得できない場合
                if (gyoushaEntity == null)
                {
                    // 荷降業者名を設定
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    // 背景色変更
                    this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    this.msgLogic.MessageBoxShow("E020", "荷降業者");

                    this.isInputError = true;
                    // 処理終了
                    return returnVal;
                }

                // 区分
                // 20151026 BUNN #12040 STR
                if (!gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue && !gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    // 背景色変更
                    this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    this.msgLogic.MessageBoxShow("E062", "荷降業者");

                    this.isInputError = true;
                    return returnVal;
                }

                // 業者区分チェック（nullはFalseとして扱う）
                if (false == gyoushaEntity.GYOUSHAKBN_UKEIRE.IsTrue)
                {
                    this.msgLogic.MessageBoxShow("E058");

                    this.isInputError = true;
                    return returnVal;
                }

                // 変更ありの場合
                if (!this.form.dicControl.ContainsKey("NIOROSHI_GYOUSHA_CD") ||
                    !this.form.dicControl["NIOROSHI_GYOUSHA_CD"].Equals(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // 荷降業者名を設定
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }

                // 諸口区分チェック
                if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                {
                    // 荷降業者名編集可
                    this.SetCtrlReadonly(this.form.NIOROSHI_GYOUSHA_NAME, false);
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region 荷降現場チェック
        /// <summary>
        /// 荷降現場チェック
        /// </summary>
        internal bool ChechNioroshiGenbaCd()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // Readonly初期化
                this.SetCtrlReadonly(this.form.NIOROSHI_GENBA_NAME, true);

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    // 荷降現場の関連情報をクリア
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;

                    // 処理終了
                    returnVal = true;
                    return true;
                }

                // 荷降業者入力されてない場合
                if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    this.msgLogic.MessageBoxShow("E051", "荷降業者");
                    // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.isInputError = true;
                    return returnVal;
                }

                //// 荷降現場CDで現場情報取得（複数）
                //var genbaEntityList = this.GetGenba(this.form.NIOROSHI_GENBA_CD.Text);
                //if (genbaEntityList == null)
                //{
                //    this.msgLogic.MessageBoxShow("E020", "荷降現場");
                //    // 処理終了
                //    return returnVal;
                //}

                // 荷降現場情報を取得
                bool catchErr = true;
                M_GENBA genbaEntity = new M_GENBA();
                genbaEntity = GetGenba(this.form.NIOROSHI_GENBA_CD.Text, this.form.NIOROSHI_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return returnVal;
                }
                // 取得できない場合
                if (genbaEntity == null)
                {
                    // 荷降現場の関連情報をクリア
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;

                    // 背景色変更
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    // 一致するデータがないのでエラー
                    this.msgLogic.MessageBoxShow("E020", "現場");

                    this.isInputError = true;
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    // 処理終了
                    return returnVal;
                }

                // 「積み替え保管」「処分事業場」「荷積降現場」「最終処分場」区分チェック
                // 20151026 BUNN #12040 STR
                if (!genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue && !genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue
                    && !genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    // 背景色変更
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    // 一致するデータがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "荷降現場");

                    this.isInputError = true;

                    // 処理終了
                    return returnVal;
                }

                var gyousha = this.GetGyousha(genbaEntity.GYOUSHA_CD, out catchErr);
                if (!catchErr)
                {
                    return returnVal;
                }
                if (null == gyousha)
                {
                    // 業者及び現場の関連情報をクリア
                    this.form.NIOROSHI_GENBA_CD.Text = String.Empty;
                    this.form.NIOROSHI_GENBA_NAME.Text = String.Empty;
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "業者");

                    this.isInputError = true;
                    this.form.SetFocusControl(this.form.NIOROSHI_GYOUSHA_CD);
                    this.form.boolMoveFocusControl();

                    return returnVal;
                }
                else
                {
                    // nullはFalseとして扱う
                    if (false == gyousha.GYOUSHAKBN_UKEIRE.IsTrue)
                    {
                        // 業者及び現場の関連情報をクリア
                        this.form.NIOROSHI_GENBA_CD.Text = String.Empty;
                        this.form.NIOROSHI_GENBA_NAME.Text = String.Empty;
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = String.Empty;
                        this.msgLogic.MessageBoxShow("E058");

                        this.isInputError = true;
                        this.form.SetFocusControl(this.form.NIOROSHI_GYOUSHA_CD);
                        this.form.boolMoveFocusControl();

                        return returnVal;
                    }
                }

                // 変更ありの場合
                if (!this.form.dicControl.ContainsKey("NIOROSHI_GENBA_CD") ||
                    !this.form.dicControl["NIOROSHI_GENBA_CD"].Equals(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    // 荷降現場情報設定
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }

                // 諸口区分チェック
                if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                {
                    // 荷降業者名編集可
                    this.SetCtrlReadonly(this.form.NIOROSHI_GENBA_NAME, false);
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region 営業担当者チェック
        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        internal bool CheckEigyouTantousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                var before = this.form.GetBeforeText(this.form.EIGYOU_TANTOUSHA_CD.Name);
                if (before == this.form.EIGYOU_TANTOUSHA_CD.Text && !this.isInputError)
                {
                    return ret;
                }

                // 初期化
                this.isInputError = false;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    // 営業担当者CDがなければ既にエラーが表示されているので何もしない。
                    return ret;
                }

                var shainEntity = this.GetShain(this.form.EIGYOU_TANTOUSHA_CD.Text);
                if (shainEntity == null)
                {
                    return ret;
                }
                else if (shainEntity.EIGYOU_TANTOU_KBN.Equals(SqlBoolean.False))
                {
                    this.isInputError = true;
                    this.form.EIGYOU_TANTOUSHA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E057", "営業担当の登録が", "入力");
                    this.form.EIGYOU_TANTOUSHA_CD.Focus();
                    return ret;
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEigyouTantousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 車種チェック
        /// <summary>
        /// 車種チェック
        /// </summary>
        internal bool ChechShashuCd()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 車種または車輌入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHASHU_CD.Text) || String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return returnVal;
                }

                // 車輌取得
                var sharyou = this.GetSharyou(this.form.UNPAN_GYOUSHA_CD.Text, this.form.SHARYOU_CD.Text);
                if (sharyou == null)
                {
                    //// メッセージ表示
                    //this.msgLogic.MessageBoxShow("E020", "車輌");
                    //this.form.SHARYOU_CD.Focus();
                    this.form.SHARYOU_CD.Text = string.Empty;
                    this.form.SHARYOU_NAME.Text = string.Empty;
                    returnVal = true;
                    return returnVal;
                }

                // 車種一致チェックを行う
                if (!String.IsNullOrEmpty(sharyou.SHASYU_CD) && this.form.SHASHU_CD.Text != sharyou.SHASYU_CD)
                {
                    // 背景色変更
                    this.form.SHASHU_CD.IsInputErrorOccured = true;
                    // メッセージ表示
                    this.msgLogic.MessageBoxShow("E104", "車輌CD", "車種");
                    return returnVal;
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechShashuCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechShashuCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <param name="uintenshaCd">運転者CD</param>
        /// <returns></returns>
        internal bool ChechSharyouCd(ref string uintenshaCd)
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(uintenshaCd);

                // 車輌の関連情報をクリア
                this.form.SHARYOU_NAME.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    returnVal = true;
                    // 処理終了
                    return returnVal;
                }

                // 車輌取得
                var sharyou = this.GetSharyou(this.form.UNPAN_GYOUSHA_CD.Text, this.form.SHARYOU_CD.Text);
                if (sharyou == null)
                {
                    // 背景色変更
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    // メッセージ表示
                    this.msgLogic.MessageBoxShow("E020", "車輌");
                    return returnVal;
                }

                // 車輌名設定
                this.form.SHARYOU_NAME.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 車種入力された場合
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    // 車種と一致ではない場合
                    if (!String.IsNullOrEmpty(sharyou.SHASYU_CD) && this.form.SHASHU_CD.Text != sharyou.SHASYU_CD)
                    {
                        // 背景色変更
                        this.form.SHARYOU_CD.IsInputErrorOccured = true;
                        // メッセージ表示
                        this.msgLogic.MessageBoxShow("E104", "車輌CD", "車種");
                        return returnVal;
                    }
                }
                else
                {
                    // 車種取得
                    var shashu = this.GetSharshu(sharyou.SHASYU_CD);
                    if (shashu != null)
                    {
                        // 車種情報設定
                        this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                        this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                // 運転者Cdを返す
                uintenshaCd = sharyou.SHAIN_CD;

                returnVal = true;
                // 処理終了
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, uintenshaCd);
            }
        }
        #endregion

        #region 運転者チェック
        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntenshasha()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.UNTENSHA_NAME.Text = string.Empty;

                // 入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    returnVal = true;
                    return returnVal;
                }

                // 社員取得
                var shainEntity = this.GetShain(this.form.UNTENSHA_CD.Text);
                if (shainEntity == null)
                {
                    // 背景色変更
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E020", "社員");
                    return returnVal;
                }
                else if (!shainEntity.UNTEN_KBN.IsTrue)
                {
                    // 背景色変更
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E057", "運転者担当の登録が", "入力");
                    return returnVal;
                }
                else if (!string.IsNullOrEmpty(this.form.HOJOIN_CD.Text) &&
                       this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    return returnVal;
                }
                else
                {
                    this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUntenshasha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshasha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region 補助員チェック
        /// <summary>
        /// 補助員チェック
        /// </summary>
        internal bool CheckHojyoin()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.HOJOIN_NAME.Text = string.Empty;

                // 入力されてない場合
                if (string.IsNullOrEmpty(this.form.HOJOIN_CD.Text))
                {
                    returnVal = true;
                    return returnVal;
                }

                // 社員取得
                var shainEntity = this.GetShain(this.form.HOJOIN_CD.Text);
                if (shainEntity == null)
                {
                    // 背景色変更
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E020", "社員");
                    return returnVal;
                }
                else if (!shainEntity.UNTEN_KBN.IsTrue)
                {
                    // 背景色変更
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E057", "運転者担当の登録が", "入力");
                    return returnVal;
                }
                else if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text) &&
                        this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    return returnVal;
                }
                else
                {
                    this.form.HOJOIN_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

                returnVal = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHojyoin", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHojyoin", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #endregion

        #region 業務処理

        #region 前の受付番号を取得
        /// <summary>
        /// 前の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>前の受付番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            String kyoten = this.headerForm.KYOTEN_CD.Text;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;

                // データ取得
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPreviousNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        #endregion

        #region 次の受付番号を取得
        /// <summary>
        /// 次の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>次の受付番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            String kyoten = this.headerForm.KYOTEN_CD.Text;
            catchErr = true;

            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;

                // データ取得
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        #endregion

        #region 前の台数の受付番号を取得
        /// <summary>
        /// 前の台数の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="numberValue">台数</param>
        /// <returns>前の台数の受付番号</returns>
        internal String GetPreviousDaisuuNumber(String tableName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = true;

            try
            {
                LogUtility.DebugMethodStart(tableName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    return String.Empty;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MAX(UKETSUKE_NUMBER) AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE SHASHU_DAISU_GROUP_NUMBER = " + (long)dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"];
                selectStr += "   AND SHASHU_DAISU_NUMBER < " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 ";

                // データ取得
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPreviousDaisuuNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousDaisuuNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion

        #region 次の台数の受付番号を取得
        /// <summary>
        /// 次の台数の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="numberValue">台数</param>
        /// <returns>次の台数の受付番号</returns>
        internal String GetNextDaisuuNumber(String tableName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = true;

            try
            {
                LogUtility.DebugMethodStart(tableName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    return String.Empty;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MIN(UKETSUKE_NUMBER) AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE SHASHU_DAISU_GROUP_NUMBER = " + (long)dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"];
                selectStr += "   AND SHASHU_DAISU_NUMBER > " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 ";

                // データ取得
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextDaisuuNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextDaisuuNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        #endregion

        #region SYSTEM_IDを採番
        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createSystemIdForUketsuke()
        {
            SqlInt64 returnVal = 1;

            try
            {
                LogUtility.DebugMethodStart();

                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();

                // IS_NUMBER_SYSTEMDao(共通)
                IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

                var updateEntity = numberSystemDao.GetNumberSystemData(entity);
                returnVal = numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();
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

        #region 受付番号を採番
        /// <summary>
        /// 受付番号を採番
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createUketsukeNumber()
        {
            SqlInt64 returnVal = -1;

            try
            {
                LogUtility.DebugMethodStart();

                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();

                // IS_NUMBER_DENSHUDao(共通)
                IS_NUMBER_DENSHUDao numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
                var updateEntity = numberDenshuDao.GetNumberDenshuData(entity);
                returnVal = numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();
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

        #region 伝票区分により金額の端数処理を行う
        /// <summary>
        /// 伝票区分により金額の端数処理を行う
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="kingaku">金額</param>
        /// <returns>端数処理した金額</returns>
        private decimal CalcMoneyByHasu(string kbnCD, decimal kingaku)
        {
            decimal returnVal = 0;

            try
            {
                LogUtility.DebugMethodStart(kbnCD, kingaku);

                // 端数CD（初期0：処理なし）
                int hasuuCD = 0;

                // TODO: 伝票区分はConstクラスの値で判定
                switch (kbnCD)
                {
                    // 売上
                    case "1":
                        // 端数CDを取得
                        int.TryParse(seikyuuHasuuCD, out hasuuCD);
                        break;
                    // 支払
                    case "2":
                        // 端数CDを取得
                        int.TryParse(siharaiHasuuCD, out hasuuCD);
                        break;
                    default:
                        break;
                }

                // 端数処理を行う
                returnVal = CommonCalc.FractionCalc(kingaku, int.Parse(seikyuuHasuuCD));
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

        #region 受付指示書印刷するか
        /// <summary>
        /// 受付指示書印刷するか
        /// </summary>
        /// <returns>true:印刷　false:印刷しない</returns>
        internal bool IsPrint(out bool catchNoErr)
        {
            bool retResult = false;
            catchNoErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.sysInfoEntity != null)
                {
                    // 配車状況が「3:配車」かつ配車種類が「3:確定」の場合
                    if (ConstClass.HAISHA_JOKYO_CD_HAISHA == this.form.HAISHA_JOKYO_CD.Text &&
                        ConstClass.HAISHA_SHURUI_CD_KAKUTEI == this.form.HAISHA_SHURUI_CD.Text)
                    {
                        // trueを返す
                        retResult = true;
                    }
                    // システム設定の受付指示書印刷区分が「１．する」の場合
                    if (this.ChgDBNullToValue(sysInfoEntity.UKETSUKE_SIJISHO_PRINT_KBN, string.Empty).ToString().Equals("1"))
                    {
                        // 配車状況が「1:受注」の場合
                        if (ConstClass.HAISHA_JOKYO_CD_JUCHU == this.form.HAISHA_JOKYO_CD.Text)
                        {
                            // trueを返す
                            retResult = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchNoErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retResult, catchNoErr);
            }
            return retResult;
        }
        #endregion

        #region 受付指示書(控え)印刷するか
        /// <summary>
        /// 受付指示書(控え)印刷するか
        /// </summary>
        /// <returns>true:印刷　false:印刷しない</returns>
        internal bool IsHikaePrint()
        {
            bool retResult = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.sysInfoEntity != null)
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
        #endregion

        #region コース組込CDよりコース組込名を取得
        /// <summary>
        /// コース組込CDよりコース組込名を取得
        /// </summary>
        /// <param name="kumikomiCD">コース組込CD</param>
        /// <returns>コース組込名</returns>
        private string GetCourseKumikomiName(string kumikomiCD)
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(kumikomiCD);

                if (string.IsNullOrEmpty(kumikomiCD))
                {
                    return returnVal;
                }

                switch (int.Parse(kumikomiCD))
                {
                    case 1:
                        returnVal = "臨時";
                        break;
                    case 2:
                        returnVal = "組込";
                        break;
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

        #region コンテナ関連情報設定
        /// <summary>
        /// コンテナ関連情報設定
        /// </summary>
        internal bool SetContenaInfo()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 設置台数
                this.form.SETTI_DAISUU.Text = this.settiDaisu.ToString();
                // 引揚台数
                this.form.HIKIAGE_DAISUU.Text = this.hikiageDaisu.ToString();

                // コンテナCDをセット
                if (this.settiDaisu > 0 && this.hikiageDaisu > 0)
                {
                    this.form.CONTENA_SOUSA_CD.Text = "1";
                }
                else if (this.settiDaisu > 0 && this.hikiageDaisu == 0)
                {
                    this.form.CONTENA_SOUSA_CD.Text = "2";
                }
                else if (this.settiDaisu == 0 && this.hikiageDaisu > 0)
                {
                    this.form.CONTENA_SOUSA_CD.Text = "3";
                }
                else
                {
                    this.form.CONTENA_SOUSA_CD.Text = string.Empty;
                }

                // コンテナ名をセット
                this.form.CONTENA_SOUSA_NAME_RYAKU.Text = string.Empty;
                M_CONTENA_JOUKYOU contenaJoukyou = new M_CONTENA_JOUKYOU();
                if (!string.IsNullOrEmpty(this.form.CONTENA_SOUSA_CD.Text))
                {
                    var contenaJoukyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CONTENA_JOUKYOUDao>();
                    contenaJoukyou = contenaJoukyouDao.GetDataByCd(this.form.CONTENA_SOUSA_CD.Text);
                }
                if (contenaJoukyou != null)
                {
                    this.form.CONTENA_SOUSA_NAME_RYAKU.Text = contenaJoukyou.CONTENA_JOUKYOU_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetContenaInfo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetContenaInfo", ex);
                this.msgLogic.MessageBoxShow("E245", ""); ;
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        /// <summary>
        /// コードよりコンテナ名を取得
        /// </summary>
        private string GetContenaName(string code)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(code);

                switch (code)
                {
                    case "1":
                        returnVal = "設置";
                        break;
                    case "2":
                        returnVal = "引揚";
                        break;
                    case "3":
                        returnVal = "交換";
                        break;
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

        #region Entitysデータを作成
        /// <summary>
        /// Entitysデータを作成
        /// </summary>
        public bool CreateEntitys()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // Entityデータをクリア
                this.insEntryEntityList.Clear();
                this.insDetailEntityList.Clear();
                this.insContenaEntityList.Clear();

                // 画面モード
                switch (this.form.WindowType)
                {
                    // 登録処理
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // ***************
                        // 登録データ作成
                        // ***************
                        // FirstシステムIDを退避
                        string systemID = string.Empty;

                        // 入力した台数分ループし、登録データを作成
                        int cntDaisuu = int.Parse(this.form.SHASHU_DAISU_NUMBER.Text);
                        //CongBinh 20210713 #152801 S
                        if (this.form.ListSagyouBi == null || this.form.ListSagyouBi.Count <= 1)
                        {
                            for (int i = 1; i <= cntDaisuu; i++)
                            {
                                // 受付収集入力Entityを作成
                                T_UKETSUKE_SS_ENTRY entryEntity = this.CreateEntryEntity(ref systemID, i);

                                // 受付収集入力Entityをリストに追加
                                this.insEntryEntityList.Add(entryEntity);

                                // 受付収集明細Entityを作成
                                this.CreateDetailEntity(entryEntity.SYSTEM_ID, (int)entryEntity.SEQ, entryEntity.UKETSUKE_NUMBER);

                                // コンテナ指定Entity作成
                                this.CreateContenaReserveEntity(entryEntity.SYSTEM_ID, (int)entryEntity.SEQ);

                                // コンテナマスタ更新用Entity作成
                                this.CreateMasterContena();
                            }
                        }
                        else
                        {
                            foreach (var item in this.form.ListSagyouBi)
                            {
                                systemID = string.Empty;
                                for (int i = 1; i <= cntDaisuu; i++)
                                {
                                    // 受付収集入力Entityを作成
                                    T_UKETSUKE_SS_ENTRY entryEntity = this.CreateEntryEntity(ref systemID, i);

                                    entryEntity.SAGYOU_DATE = item;

                                    // 受付収集入力Entityをリストに追加
                                    this.insEntryEntityList.Add(entryEntity);

                                    // 受付収集明細Entityを作成
                                    this.CreateDetailEntity(entryEntity.SYSTEM_ID, (int)entryEntity.SEQ, entryEntity.UKETSUKE_NUMBER);

                                    // コンテナ指定Entity作成
                                    this.CreateContenaReserveEntity(entryEntity.SYSTEM_ID, (int)entryEntity.SEQ);

                                    // コンテナマスタ更新用Entity作成
                                    this.CreateMasterContena();
                                }
                            }
                        }
                        //CongBinh 20210713 #152801 E

                        break;

                    // 更新処理
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // ***************
                        // 削除データ作成
                        // ***************
                        // 受付収集入力論理削除Entityを作成
                        this.CreateDelEntryEntity();

                        // コンテナ削除Entity作成
                        this.CreateDelContenaEntity();

                        // ***************
                        // 登録データ作成
                        // ***************
                        // 受付収集入力Entityを作成
                        string updSystemID = string.Empty;
                        T_UKETSUKE_SS_ENTRY updEntryEntity = this.CreateEntryEntity(ref updSystemID);

                        // 受付収集入力Entityをリストに追加
                        this.insEntryEntityList.Add(updEntryEntity);

                        // MAXのSEQを取得
                        //int maxSEQ = (int)updEntryEntity.SEQ;
                        //int maxSEQ = this.GetMesaiMaxSEQ(updEntryEntity.SYSTEM_ID);

                        // 受付収集明細Entityを作成
                        this.CreateDetailEntity(updEntryEntity.SYSTEM_ID, (int)updEntryEntity.SEQ, updEntryEntity.UKETSUKE_NUMBER);

                        // コンテナ指定Entity作成
                        this.CreateContenaReserveEntity(updEntryEntity.SYSTEM_ID, (int)updEntryEntity.SEQ);

                        // コンテナマスタ更新用Entity作成
                        this.CreateMasterContena();

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // ***************
                        // 削除データ作成
                        // ***************
                        // 受付収集入力論理削除Entityを作成
                        this.CreateDelEntryEntity();

                        // コンテナ削除Entity作成
                        this.CreateDelContenaEntity();

                        // ***************
                        // 登録データ作成
                        // ***************
                        // 受付収集入力Entityを作成
                        string delSystemID = string.Empty;
                        T_UKETSUKE_SS_ENTRY delEntryEntity = this.CreateEntryEntity(ref delSystemID);

                        // 受付収集入力Entityをリストに追加
                        this.insEntryEntityList.Add(delEntryEntity);

                        // 受付収集明細Entityを作成
                        this.CreateDetailEntity(delEntryEntity.SYSTEM_ID, (int)delEntryEntity.SEQ, delEntryEntity.UKETSUKE_NUMBER);

                        // コンテナ指定Entity作成
                        this.CreateContenaReserveEntity(delEntryEntity.SYSTEM_ID, (int)delEntryEntity.SEQ);

                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntitys", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 論理削除のEntryEntityを作成
        /// <summary>
        /// 論理削除のEntityを作成
        /// </summary>
        private void CreateDelEntryEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.delEntryEntity = new T_UKETSUKE_SS_ENTRY();

                // SYSTEM_ID(元データのシステムID)
                this.delEntryEntity.SYSTEM_ID = (long)dtUketsukeEntry.Rows[0]["SYSTEM_ID"];

                // SEQ(元データのSEQ)
                this.delEntryEntity.SEQ = (int)dtUketsukeEntry.Rows[0]["SEQ"];

                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKETSUKE_SS_ENTRY>(this.delEntryEntity);
                dbLogic.SetSystemProperty(this.delEntryEntity, false);

                // 削除フラグ
                this.delEntryEntity.DELETE_FLG = true;

                // TIME_STAMP
                this.delEntryEntity.TIME_STAMP = (byte[])dtUketsukeEntry.Rows[0]["TIME_STAMP"];

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

        #region 論理削除のコンテナEntityを作成
        /// <summary>
        /// 論理削除のコンテナEntityを作成
        /// </summary>
        private void CreateDelContenaEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.arrContenaReserve == null)
                {
                    return;
                }

                // コンテナエンティティループ
                foreach (T_CONTENA_RESERVE entity in this.arrContenaReserve)
                {
                    // 作成と更新情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.T_CONTENA_RESERVE>(entity);
                    dbLogic.SetSystemProperty(entity, false);

                    // 削除フラグ
                    entity.DELETE_FLG = true;
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

        #region 登録用のEntryEntityを作成
        /// <summary>
        /// 登録用のEntityを作成
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="no">車輌台数連番</param>
        /// <returns>T_UKETSUKE_SS_ENTRY</returns>
        private T_UKETSUKE_SS_ENTRY CreateEntryEntity(ref string systemID, int no = 1)
        {

            T_UKETSUKE_SS_ENTRY entryEntity = new T_UKETSUKE_SS_ENTRY();

            try
            {
                LogUtility.DebugMethodStart(systemID, no);

                string strDatetime;
                string strTime;

                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKETSUKE_SS_ENTRY>(entryEntity);
                dbLogic.SetSystemProperty(entryEntity, false);

                // 画面モード
                switch (this.form.WindowType)
                {
                    // 登録処理の場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // SYSTEM_IDの採番
                        entryEntity.SYSTEM_ID = this.createSystemIdForUketsuke();
                        if (string.IsNullOrEmpty(systemID))
                        {
                            systemID = entryEntity.SYSTEM_ID.ToString();
                        }

                        // 受入番号の採番
                        entryEntity.UKETSUKE_NUMBER = this.createUketsukeNumber();
                        //モバイル連携用
                        if (string.IsNullOrEmpty(this.Renkei_UketsukeNumber))
                        {
                            this.Renkei_UketsukeNumber = entryEntity.UKETSUKE_NUMBER.ToString();
                        }
                        else
                        {
                            this.Renkei_UketsukeNumber = this.Renkei_UketsukeNumber + ", " + entryEntity.UKETSUKE_NUMBER.ToString();
                        }
                        // SEQ
                        entryEntity.SEQ = 1;

                        // SHASHU_DAISU_GROUP_NUMBER(車種台数一番目のレコードのSYSTEM_ID)
                        entryEntity.SHASHU_DAISU_GROUP_NUMBER = SqlInt64.Parse(systemID);
                        // 車種台数(車種台数レコード順通番)
                        entryEntity.SHASHU_DAISU_NUMBER = (SqlInt16)no;
                        break;

                    // 更新処理の場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // SYSTEM_ID(元データのシステムID)
                        entryEntity.SYSTEM_ID = (long)dtUketsukeEntry.Rows[0]["SYSTEM_ID"];

                        // 受入番号(元データの受入番号)
                        entryEntity.UKETSUKE_NUMBER = (long)dtUketsukeEntry.Rows[0]["UKETSUKE_NUMBER"];
                        //モバイル連携用
                        this.Renkei_UketsukeNumber = dtUketsukeEntry.Rows[0]["UKETSUKE_NUMBER"].ToString();
                        // SEQ（+1連番）
                        entryEntity.SEQ = ((int)dtUketsukeEntry.Rows[0]["SEQ"] + 1);

                        // SHASHU_DAISU_GROUP_NUMBER(元データのSYSTEM_ID)
                        entryEntity.SHASHU_DAISU_GROUP_NUMBER = (long)dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"];
                        // 車種台数(元データ通番)
                        entryEntity.SHASHU_DAISU_NUMBER = (Int16)dtUketsukeEntry.Rows[0]["SHASHU_DAISU_NUMBER"];

                        // 作成者
                        entryEntity.CREATE_USER = dtUketsukeEntry.Rows[0]["CREATE_USER"].ToString();
                        // 作成日
                        entryEntity.CREATE_DATE = (DateTime)dtUketsukeEntry.Rows[0]["CREATE_DATE"];
                        // 作成PC
                        entryEntity.CREATE_PC = dtUketsukeEntry.Rows[0]["CREATE_PC"].ToString();

                        break;
                }

                // 拠点
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
                }

                // 配車状況
                if (!string.IsNullOrEmpty(this.form.HAISHA_JOKYO_CD.Text))
                {
                    entryEntity.HAISHA_JOKYO_CD = SqlInt16.Parse(this.form.HAISHA_JOKYO_CD.Text);
                    entryEntity.HAISHA_JOKYO_NAME = this.form.HAISHA_JOKYO_NAME.Text;
                }

                // 配車種類
                if (!string.IsNullOrEmpty(this.form.HAISHA_SHURUI_CD.Text))
                {
                    entryEntity.HAISHA_SHURUI_CD = SqlInt16.Parse(this.form.HAISHA_SHURUI_CD.Text);
                    entryEntity.HAISHA_SHURUI_NAME = this.form.HAISHA_SHURUI_NAME.Text;
                }

                // 営業担当
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    entryEntity.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                    entryEntity.EIGYOU_TANTOUSHA_NAME = this.form.EIGYOU_TANTOUSHA_NAME.Text;
                }

                // 配車指示書
                if (!string.IsNullOrEmpty(this.form.HAISHA_SIJISHO_FLG.Text) && this.form.HAISHA_SIJISHO_FLG.Text.Equals("済"))
                {
                    entryEntity.HAISHA_SIJISHO_FLG = true;
                }
                else
                {
                    entryEntity.HAISHA_SIJISHO_FLG = false;
                }

                // 受付日
                if (this.TryChgDHMtoDateTime(this.form.UKETSUKE_DATE.Value, this.form.UKETSUKE_DATE_HOUR.Text, this.form.UKETSUKE_DATE_MINUTE.Text, out strDatetime))
                {
                    entryEntity.UKETSUKE_DATE = DateTime.Parse(strDatetime);
                }

                // 取引先
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    entryEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    entryEntity.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME.Text;
                }

                // 業者
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    entryEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    entryEntity.GYOUSHA_NAME = this.form.GYOUSHA_NAME.Text;
                }

                // 業者電話
                if (!string.IsNullOrEmpty(this.form.GYOSHA_TEL.Text))
                {
                    entryEntity.GYOSHA_TEL = this.form.GYOSHA_TEL.Text;
                }

                // 現場
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    entryEntity.GENBA_CD = this.form.GENBA_CD.Text;
                    entryEntity.GENBA_NAME = this.form.GENBA_NAME.Text;
                }

                // 現場住所１
                if (!string.IsNullOrEmpty(this.form.GENBA_ADDRESS1.Text))
                {
                    entryEntity.GENBA_ADDRESS1 = this.form.GENBA_ADDRESS1.Text;
                }

                // 現場住所２
                if (!string.IsNullOrEmpty(this.form.GENBA_ADDRESS2.Text))
                {
                    entryEntity.GENBA_ADDRESS2 = this.form.GENBA_ADDRESS2.Text;
                }

                // 現場電話
                if (!string.IsNullOrEmpty(this.form.GENBA_TEL.Text))
                {
                    entryEntity.GENBA_TEL = this.form.GENBA_TEL.Text;
                }

                // 担当者
                if (!string.IsNullOrEmpty(this.form.TANTOSHA_NAME.Text))
                {
                    entryEntity.TANTOSHA_NAME = this.form.TANTOSHA_NAME.Text;
                }

                // 担当携帯
                if (!string.IsNullOrEmpty(this.form.TANTOSHA_TEL.Text))
                {
                    entryEntity.TANTOSHA_TEL = this.form.TANTOSHA_TEL.Text;
                }

                // 運搬業者
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    entryEntity.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                    entryEntity.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
                }

                // 荷降業者
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    entryEntity.NIOROSHI_GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    entryEntity.NIOROSHI_GYOUSHA_NAME = this.form.NIOROSHI_GYOUSHA_NAME.Text;
                }

                // 荷降現場
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    entryEntity.NIOROSHI_GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
                    entryEntity.NIOROSHI_GENBA_NAME = this.form.NIOROSHI_GENBA_NAME.Text;
                }

                // 受付備考
                if (!string.IsNullOrEmpty(this.form.UKETSUKE_BIKOU1.Text))
                {
                    entryEntity.UKETSUKE_BIKOU1 = this.form.UKETSUKE_BIKOU1.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKETSUKE_BIKOU2.Text))
                {
                    entryEntity.UKETSUKE_BIKOU2 = this.form.UKETSUKE_BIKOU2.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKETSUKE_BIKOU3.Text))
                {
                    entryEntity.UKETSUKE_BIKOU3 = this.form.UKETSUKE_BIKOU3.Text;
                }

                // 作業日(指定)
                if (this.form.SAGYOU_DATE.Value != null)
                {
                    entryEntity.SAGYOU_DATE = ((DateTime)this.form.SAGYOU_DATE.Value).ToShortDateString();
                }

                // 荷降日
                #region 荷卸日のNULL設定(暫定対応)

                // TODO:収集受付の荷卸日を表示する際、合わせて修正こと

                // 不具合No3322により、荷卸日を非表示の対応に合わせてデータも設定しない
                // 荷卸日が表示後、下記コメントアウトは解除すること

                //if (this.form.NIOROSHI_DATE.Value != null)
                //{
                //    entryEntity.NIOROSHI_DATE = ((DateTime)this.form.NIOROSHI_DATE.Value).ToShortDateString();
                //}
                #endregion

                // 作業日(期間)
                if (this.form.SAGYOU_DATE_BEGIN.Value != null)
                {
                    entryEntity.SAGYOU_DATE_BEGIN = ((DateTime)this.form.SAGYOU_DATE_BEGIN.Value).ToShortDateString();
                }
                if (this.form.SAGYOU_DATE_END.Value != null)
                {
                    entryEntity.SAGYOU_DATE_END = ((DateTime)this.form.SAGYOU_DATE_END.Value).ToShortDateString();
                }

                // 現着時間
                if (!string.IsNullOrEmpty(this.form.GENCHAKU_TIME_CD.Text))
                {
                    entryEntity.GENCHAKU_TIME_CD = SqlInt16.Parse(this.form.GENCHAKU_TIME_CD.Text);
                    entryEntity.GENCHAKU_TIME_NAME = this.form.GENCHAKU_TIME_NAME.Text;
                }
                if (this.TryChgHMtoTime(this.form.GENCHAKU_TIME_HOUR.Text, this.form.GENCHAKU_TIME_MINUTE.Text, out strTime))
                {
                    entryEntity.GENCHAKU_TIME = strTime;
                }

                // 作業時間
                if (this.TryChgHMtoTime(this.form.SAGYOU_TIME_HOUR.Text, this.form.SAGYOU_TIME_MINUTE.Text, out strTime))
                {
                    entryEntity.SAGYOU_TIME = strTime;
                }

                // 移動時間
                if (this.TryChgHMtoTime(this.form.SAGYOU_TIME_BEGIN_HOUR.Text, this.form.SAGYOU_TIME_BEGIN_MINUTE.Text, out strTime))
                {
                    entryEntity.SAGYOU_TIME_BEGIN = strTime;
                }
                if (this.TryChgHMtoTime(this.form.SAGYOU_TIME_END_HOUR.Text, this.form.SAGYOU_TIME_END_MINUTE.Text, out strTime))
                {
                    entryEntity.SAGYOU_TIME_END = strTime;
                }

                // 車種
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    entryEntity.SHASHU_CD = this.form.SHASHU_CD.Text;
                    entryEntity.SHASHU_NAME = this.form.SHASHU_NAME.Text;
                }

                // 車輌と運転者はセットで更新すること
                // どちらかが入力されていれば、未入力の項目は空文字で更新する
                if (!String.IsNullOrEmpty(this.form.SHARYOU_CD.Text) || !String.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    // 車輌
                    entryEntity.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                    entryEntity.SHARYOU_NAME = this.form.SHARYOU_NAME.Text;

                    // 運転者
                    entryEntity.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
                    entryEntity.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
                }

                // 補助員
                if (!string.IsNullOrEmpty(this.form.HOJOIN_CD.Text))
                {
                    entryEntity.HOJOIN_CD = this.form.HOJOIN_CD.Text;
                    entryEntity.HOJOIN_NAME = this.form.HOJOIN_NAME.Text;
                }

                // マニ種類
                if (!string.IsNullOrEmpty(this.form.MANIFEST_SHURUI_CD.Text))
                {
                    entryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(this.form.MANIFEST_SHURUI_CD.Text);
                }

                // マニ手配
                if (!string.IsNullOrEmpty(this.form.MANIFEST_TEHAI_CD.Text))
                {
                    entryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(this.form.MANIFEST_TEHAI_CD.Text);
                }

                // コンテナ
                if (!string.IsNullOrEmpty(this.form.CONTENA_SOUSA_CD.Text))
                {
                    entryEntity.CONTENA_SOUSA_CD = SqlInt16.Parse(this.form.CONTENA_SOUSA_CD.Text);
                }

                // コース組合
                if (!string.IsNullOrEmpty(this.form.COURSE_KUMIKOMI_CD.Text))
                {
                    entryEntity.COURSE_KUMIKOMI_CD = SqlInt16.Parse(this.form.COURSE_KUMIKOMI_CD.Text);
                }

                // コース名
                if (!string.IsNullOrEmpty(this.form.COURSE_NAME_CD.Text))
                {
                    entryEntity.COURSE_NAME_CD = this.form.COURSE_NAME_CD.Text;
                }

                // 運転者指示事項
                if (!string.IsNullOrEmpty(this.form.UNTENSHA_SIJIJIKOU1.Text))
                {
                    entryEntity.UNTENSHA_SIJIJIKOU1 = this.form.UNTENSHA_SIJIJIKOU1.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNTENSHA_SIJIJIKOU2.Text))
                {
                    entryEntity.UNTENSHA_SIJIJIKOU2 = this.form.UNTENSHA_SIJIJIKOU2.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNTENSHA_SIJIJIKOU3.Text))
                {
                    entryEntity.UNTENSHA_SIJIJIKOU3 = this.form.UNTENSHA_SIJIJIKOU3.Text;
                }

                // 消費税率
                entryEntity.SHOUHIZEI_RATE = SqlDecimal.Null;

                // 伝票毎の消費税外税
                entryEntity.TAX_SOTO = SqlDecimal.Null;

                // 伝票毎の消費税内税
                entryEntity.TAX_UCHI = SqlDecimal.Null;

                // 明細毎の消費税外税合計
                entryEntity.TAX_SOTO_TOTAL = SqlDecimal.Null;

                // 明細毎の消費税内税合計
                entryEntity.TAX_UCHI_TOTAL = SqlDecimal.Null;

                // 金額計
                decimal kingakuTotal;
                decimal.TryParse(this.form.SASHIHIKIGAKU.Text, out kingakuTotal);
                entryEntity.KINGAKU_TOTAL = kingakuTotal;

                // 消費税
                entryEntity.SHOUHIZEI_TOTAL = SqlDecimal.Null;

                // 合計金額
                entryEntity.GOUKEI_KINGAKU_TOTAL = kingakuTotal;

                // 削除フラグ
                entryEntity.DELETE_FLG = false;

                return entryEntity;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entryEntity, systemID);
            }
        }
        #endregion

        #region 登録用のDetailEntityを作成
        /// <summary>
        /// 登録用のDetailEntityを作成
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="SEQ">枝番</param>
        /// <param name="uketsukeNumber">受付番号</param>
        private void CreateDetailEntity(SqlInt64 systemID, int SEQ, SqlInt64 uketsukeNumber)
        {
            try
            {
                LogUtility.DebugMethodStart(SEQ, systemID, uketsukeNumber);

                for (int i = 0; i < this.dgvDetail.RowCount - 1; i++)
                {
                    T_UKETSUKE_SS_DETAIL detailEntity = new T_UKETSUKE_SS_DETAIL();

                    DataGridViewRow row = this.dgvDetail.Rows[i];


                    // 受付収集入力テーブルのSYSTEM_ID
                    detailEntity.SYSTEM_ID = systemID;

                    // 受入番号の採番
                    detailEntity.UKETSUKE_NUMBER = uketsukeNumber;

                    // SEQ
                    detailEntity.SEQ = SEQ;

                    // 新規レコードの場合
                    if (row.Cells["DETAIL_SYSTEM_ID"].Value == null || string.IsNullOrEmpty(row.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                    {
                        // DETAIL_SYSTEM_ID(採番)
                        detailEntity.DETAIL_SYSTEM_ID = this.createSystemIdForUketsuke();
                    }
                    else // レコード更新の場合
                    {
                        // DETAIL_SYSTEM_ID(元データのSYSTEM_ID)
                        detailEntity.DETAIL_SYSTEM_ID = (long)row.Cells["DETAIL_SYSTEM_ID"].Value;
                    }

                    // ROW_NO
                    detailEntity.ROW_NO = (SqlInt16)(i + 1);
                    // 品名CD（必須項目）
                    if (row.Cells["HINMEI_CD"].Value != null)
                    {
                        detailEntity.HINMEI_CD = row.Cells["HINMEI_CD"].Value.ToString();
                    }
                    // 品名（必須項目）
                    if (row.Cells["HINMEI_NAME"].Value != null)
                    {
                        detailEntity.HINMEI_NAME = row.Cells["HINMEI_NAME"].Value.ToString();
                    }
                    // 伝票区分CD
                    if (row.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                    {
                        detailEntity.DENPYOU_KBN_CD = SqlInt16.Parse(row.Cells["DENPYOU_KBN_CD"].Value.ToString());
                    }
                    // 数量（必須項目）
                    decimal suuryou;
                    if (decimal.TryParse(row.Cells["SUURYOU"].FormattedValue.ToString(), out suuryou))
                    {
                        detailEntity.SUURYOU = suuryou;
                    }

                    // 単位CD
                    short unitCd;
                    if (row.Cells["UNIT_CD"].Value != null
                        && short.TryParse(row.Cells["UNIT_CD"].Value.ToString(), out unitCd))
                    {
                        detailEntity.UNIT_CD = unitCd;
                    }
                    // 単価（必須項目）
                    decimal tanka;
                    if (decimal.TryParse(row.Cells["TANKA"].FormattedValue.ToString(), out tanka))
                    {
                        detailEntity.TANKA = tanka;
                    }

                    // 金額
                    decimal kingaku = 0;
                    if (row.Cells["HINMEI_KINGAKU"].Value != null)
                    {
                        decimal.TryParse(row.Cells["HINMEI_KINGAKU"].FormattedValue.ToString(), out kingaku);
                        detailEntity.KINGAKU = kingaku;
                    }

                    // 明細備考
                    if (row.Cells["MEISAI_BIKOU"].Value != null && !string.IsNullOrEmpty(row.Cells["MEISAI_BIKOU"].Value.ToString()))
                    {
                        detailEntity.MEISAI_BIKOU = row.Cells["MEISAI_BIKOU"].Value.ToString();
                    }

                    // TIME_STAMP（新規作成不要）

                    // リストに追加
                    this.insDetailEntityList.Add(detailEntity);

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

        #region 登録用のContenaReserveを作成
        /// <summary>
        /// 登録用のContenaReserveを作成
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="SEQ">枝番</param>
        private void CreateContenaReserveEntity(SqlInt64 systemID, int SEQ)
        {
            try
            {
                LogUtility.DebugMethodStart(systemID, SEQ);

                if (this.listContenaParam == null)
                {
                    return;
                }

                // コンテナ指定画面データに基づく、登録データ作成
                foreach (T_CONTENA_RESERVE entity in this.listContenaParam)
                {
                    // 削除されたデータ登録しない
                    if (entity.DELETE_FLG.IsTrue)
                    {
                        continue;
                    }

                    T_CONTENA_RESERVE contenaEntity = new T_CONTENA_RESERVE();

                    // SYSTEM_ID
                    contenaEntity.SYSTEM_ID = systemID;

                    // SEQ
                    contenaEntity.SEQ = SEQ;

                    // コンテナ設置区分
                    contenaEntity.CONTENA_SET_KBN = entity.CONTENA_SET_KBN;

                    // コンテナ種類CD
                    contenaEntity.CONTENA_SHURUI_CD = entity.CONTENA_SHURUI_CD;

                    // コンテナCD
                    contenaEntity.CONTENA_CD = entity.CONTENA_CD;

                    // コンテナ指定台数
                    contenaEntity.DAISUU_CNT = entity.DAISUU_CNT;

                    // 台数計算フラグ
                    // コンテナ稼動実績が登録されるまでは、台数計算の対象にするためTrueで登録
                    contenaEntity.CALC_DAISUU_FLG = SqlBoolean.True;

                    // 削除フラグ
                    contenaEntity.DELETE_FLG = false;

                    // 作成と更新情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.T_CONTENA_RESERVE>(contenaEntity);
                    dbLogic.SetSystemProperty(contenaEntity, false);

                    // 修正モードの場合、受付収集入力Entityの作成情報を設定
                    if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                    {
                        // 作成者
                        contenaEntity.CREATE_USER = this.insEntryEntityList[0].CREATE_USER;
                        // 作成日
                        contenaEntity.CREATE_DATE = this.insEntryEntityList[0].CREATE_DATE;
                        // 作成PC
                        contenaEntity.CREATE_PC = this.insEntryEntityList[0].CREATE_PC;
                    }

                    // TIME_STAMP（新規作成不要）

                    // リストに追加
                    this.insContenaEntityList.Add(contenaEntity);
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

        #region 登録用のM_CONTENAを作成
        /// <summary>
        /// コンテナマスタ登録、更新用のEntityを作成
        /// </summary>
        private void CreateMasterContena()
        {
            this.contenaMasterList = new List<M_CONTENA>();

            // 計上、回収なしの場合にのみマスタを更新する
            if (!ConstClass.HAISHA_JOKYO_CD_KEIJO.Equals(this.form.HAISHA_JOKYO_CD.Text)
                && !ConstClass.HAISHA_JOKYO_CD_NASHI.Equals(this.form.HAISHA_JOKYO_CD.Text))
            {
                return;
            }

            // 変更前のコンテナ設定情報
            foreach (T_CONTENA_RESERVE contenaReserve in this.beforeListContenaParam)
            {
                if (string.IsNullOrEmpty(contenaReserve.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaReserve.CONTENA_CD))
                {
                    // キーが無い場合はスキップ
                    continue;
                }

                // 前回から変更のないコンテナは除外する。
                bool sameContenaFlg = false;
                foreach (T_CONTENA_RESERVE contenaReserveNow in this.listContenaParam)
                {
                    if (contenaReserve.CONTENA_SHURUI_CD.Equals(contenaReserveNow.CONTENA_SHURUI_CD)
                        && contenaReserve.CONTENA_CD.Equals(contenaReserveNow.CONTENA_CD))
                    {
                        sameContenaFlg = true;
                        break;
                    }
                }
                if (sameContenaFlg)
                {
                    continue;
                }

                M_CONTENA conditionValue = new M_CONTENA();
                conditionValue.CONTENA_SHURUI_CD = contenaReserve.CONTENA_SHURUI_CD;
                conditionValue.CONTENA_CD = contenaReserve.CONTENA_CD;
                var contenaMaster = this.daoMasterContena.GetDataByCd(conditionValue);

                if (contenaMaster != null)
                {
                    // 重複して設置されているデータがあるか検索する。
                    // SQL文作成
                    DataTable dt = new DataTable();
                    string selectStr = "SELECT SYSTEM_ID FROM T_CONTENA_RESERVE";
                    selectStr += " WHERE CONTENA_SHURUI_CD = " + "'" + contenaMaster.CONTENA_SHURUI_CD + "'";
                    selectStr += " AND CONTENA_CD = " + "'" + contenaMaster.CONTENA_CD + "'";
                    selectStr += " AND CONTENA_SET_KBN = 1";
                    selectStr += " AND DELETE_FLG = 0";
                    dt = daoContena.GetDateForStringSql(selectStr);
                    if (dt.Rows.Count == 1)
                    {
                        // T_CONTENA_RESERVEに1件のみの場合、重複がないため、M_CONTENAを更新する。
                        // 状況区分を取り消す。
                        contenaMaster.JOUKYOU_KBN = SqlInt16.Null;
                        // 設置日を取り消す。
                        contenaMaster.SECCHI_DATE = SqlDateTime.Null;
                        // 自動設定項目
                        string createUser = contenaMaster.CREATE_USER;
                        SqlDateTime createDate = contenaMaster.CREATE_DATE;
                        string createPC = contenaMaster.CREATE_PC;
                        var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMaster);
                        dataBinderUkeireEntry.SetSystemProperty(contenaMaster, false);
                        // Create情報は前の状態を引き継ぐ
                        contenaMaster.CREATE_USER = createUser;
                        contenaMaster.CREATE_DATE = createDate;
                        contenaMaster.CREATE_PC = createPC;

                        this.contenaMasterList.Add(contenaMaster);
                    }
                }
            }

            foreach (T_CONTENA_RESERVE contenaReserve in this.listContenaParam)
            {
                if (string.IsNullOrEmpty(contenaReserve.CONTENA_SHURUI_CD)
                    || string.IsNullOrEmpty(contenaReserve.CONTENA_CD))
                {
                    // キーが無い場合はスキップ
                    continue;
                }

                IM_CONTENADao contenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
                M_CONTENA conditionValue = new M_CONTENA();
                conditionValue.CONTENA_SHURUI_CD = contenaReserve.CONTENA_SHURUI_CD;
                conditionValue.CONTENA_CD = contenaReserve.CONTENA_CD;

                var contenaMaster = contenaDao.GetDataByCd(conditionValue);
                // 画面上削除済み、適用期間外のデータは選択できなはずだが念のためチェック
                if (contenaMaster.DELETE_FLG)
                {
                    continue;
                }

                // 設置日、引揚日をチェック
                if ((!contenaMaster.SECCHI_DATE.IsNull
                    && contenaMaster.SECCHI_DATE.Value.Date > SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString()).Value.Date)
                    || (!contenaMaster.HIKIAGE_DATE.IsNull
                    && contenaMaster.HIKIAGE_DATE.Value.Date > SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString()).Value.Date))
                {
                    // 設置日、引揚日が作業日より新しい場合は何もしない。
                    continue;
                }

                if (contenaMaster != null)
                {
                    // 画面の入力内容をコンテナマスタに反映させる
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        contenaMaster.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        contenaMaster.GENBA_CD = this.form.GENBA_CD.Text;
                    }
                    contenaMaster.SHARYOU_CD = string.Empty;
                    if (contenaReserve.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                    {
                        // 設置の場合
                        if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                        {
                            contenaMaster.SECCHI_DATE = SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                            contenaMaster.HIKIAGE_DATE = SqlDateTime.Null;
                        }
                        contenaMaster.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_SECCHI;
                    }
                    else if (contenaReserve.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                    {
                        // 引揚の場合
                        contenaMaster.HIKIAGE_DATE = SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                        contenaMaster.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_HIKIAGE;
                    }
                    // 自動設定項目
                    string createUser = contenaMaster.CREATE_USER;
                    SqlDateTime createDate = contenaMaster.CREATE_DATE;
                    string createPC = contenaMaster.CREATE_PC;
                    var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMaster);
                    dataBinderUkeireEntry.SetSystemProperty(contenaMaster, false);
                    // Create情報は前の状態を引き継ぐ
                    contenaMaster.CREATE_USER = createUser;
                    contenaMaster.CREATE_DATE = createDate;
                    contenaMaster.CREATE_PC = createPC;

                    this.contenaMasterList.Add(contenaMaster);
                }

            }
        }
        #endregion

        #region 指示書Flg更新用のEntryEntityを作成
        /// <summary>
        /// 指示書Flg更新用のEntryEntityを作成
        /// </summary>
        internal bool CreateEntityForUpdPrintFlg()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // クリア
                this.insEntryEntityList.Clear();

                T_UKETSUKE_SS_ENTRY entryEntity = new T_UKETSUKE_SS_ENTRY();

                // SYSTEM_ID(元データのシステムID)
                entryEntity.SYSTEM_ID = (long)dtUketsukeEntry.Rows[0]["SYSTEM_ID"];

                // SEQ(元データの連番)
                entryEntity.SEQ = (int)dtUketsukeEntry.Rows[0]["SEQ"];

                // 受付番号
                entryEntity.UKETSUKE_NUMBER = (long)dtUketsukeEntry.Rows[0]["UKETSUKE_NUMBER"];

                this.insEntryEntityList.Add(entryEntity);

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 指示書印刷フラグを更新
        /// <summary>
        /// 指示書印刷フラグを更新
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="SEQ">枝番</param>
        [Transaction]
        private int UpdPrintFlg(SqlInt64 systemID, int SEQ)
        {
            int returnVal = 0;

            try
            {
                LogUtility.DebugMethodStart(systemID, SEQ);

                    // データ更新
                    returnVal = this.daoUketsukeEntry.UpdatePrintFlg(systemID, SEQ);
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

        #region [F8]指示書出力
        /// <summary>
        /// [F8]指示書出力
        /// </summary>
        internal bool Print()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                string msg;
                string hikaePrintFlg;
                // レポートのWindowID
                WINDOW_ID windowID = WINDOW_ID.NONE;

                // 確認メッセージ設定
                msg = "指示書";
                windowID = WINDOW_ID.R_SAGYOU_SIJISYO;

                // 出力の確認メッセージを表示、いいえの場合
                if (msgLogic.MessageBoxShow("C047", msg) != DialogResult.Yes)
                {
                    // 処理終了
                    // #12121の対応によりreturn値の意味合いが変わりfalse:エラー、true:エラー無しになったため、印刷しない場合もtrueを返す
                    returnVal = true;
                    return returnVal;
                }

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
                foreach (T_UKETSUKE_SS_ENTRY entity in this.insEntryEntityList)
                {
                    // printクラスを呼出
                    ReportInfoR345_R350 reportInfoR345_R350 = new ReportInfoR345_R350(windowID, this.daoUketsukeEntry);

                    // 控え印刷(0：正のみ　1:正・控え２部)
                    reportInfoR345_R350.ParameterList["HikaeType"] = hikaePrintFlg;
                    // 伝票番号(受付番号)
                    reportInfoR345_R350.ParameterList["DenpyouNumber"] = entity.UKETSUKE_NUMBER.ToString();
                    // 受付種類(1：収集　2:出荷)
                    reportInfoR345_R350.ParameterList["UketukeType"] = "1";

                    reportInfoR345_R350.Create(@".\Template\R345_R350-Form.xml", "LAYOUT1", new DataTable());

                    reportInfoR345_R350.Title = "指示書";

                    //FormReport formReport = new FormReport(reportInfoR345_R350, WINDOW_ID.UKETSUKE_SHUSHU);
                    FormReportPrintPopup formReport = new FormReportPrintPopup(reportInfoR345_R350, "R350", WINDOW_ID.UKETSUKE_SHUSHU);

                    // 印刷設定の取得（11：配車指示書）
                    //formReport.SetPrintSetting(11);

                    // 印刷ポップ画面表示（テスト用）
                    //formReport.ShowDialog();

                    // 印刷アプリ初期動作(直印刷)
                    formReport.PrintInitAction = 1;

                    //直接印刷
                    formReport.PrintXPS();

                    // 印刷されてない場合、印刷フラグを更新
                    if (this.form.HAISHA_SIJISHO_FLG.Text.Equals("未"))
                    {
                        if (this.form.HAISHA_JOKYO_CD.Text != ConstClass.HAISHA_JOKYO_CD_JUCHU)
                        {
                            // 配車状況が受注のとき以外の場合に更新
                            // 配車指示書フラグを更新（印刷された場合、更新を行う）
                            this.UpdPrintFlg((long)entity.SYSTEM_ID, (int)entity.SEQ);
                            // 配車指示書を「済」に変更（印刷された場合）
                            this.form.HAISHA_SIJISHO_FLG.Text = "済";
                        }
                    }

                }
                returnVal = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Print", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Print", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region [F9]登録処理
        /// <summary>
        /// [F9]登録処理
        /// </summary>
        [Transaction]
        public bool RegistData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    //データ作成
                    if (!this.CreateEntitys())
                    {
                        return false;
                    }

                    // **********
                    // 新規登録
                    // **********
                    // 受付収集入力レコードをループ
                    foreach (T_UKETSUKE_SS_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeEntry.Insert(entity);
                    }

                    // 受付収集明細レコードをループ
                    foreach (T_UKETSUKE_SS_DETAIL entity in this.insDetailEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeDetail.Insert(entity);
                    }

                    // コンテナ操作レコードをループ
                    foreach (T_CONTENA_RESERVE entity in this.insContenaEntityList)
                    {
                        // 登録処理を行う
                        this.daoContena.Insert(entity);
                    }

                    // コンテナマスタの更新
                    foreach (M_CONTENA contenaMaster in this.contenaMasterList)
                    {
                        this.daoMasterContena.Update(contenaMaster);
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex);
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region [F9]更新処理
        /// <summary>
        /// [F9]更新処理
        /// </summary>
        [Transaction]
        public bool UpdateData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    //データ作成
                    if (!this.CreateEntitys())
                    {
                        return false;
                    }

                    // **********
                    // 論理削除
                    // **********
                    // 受付収集入力の元レコードを論理削除
                    this.daoUketsukeEntry.Update(this.delEntryEntity);

                    // アンテナレ指定コードを論理削除
                    foreach (T_CONTENA_RESERVE entity in this.arrContenaReserve)
                    {
                        // 登録処理を行う
                        this.daoContena.Update(entity);
                    }

                    // **********
                    // 新規登録
                    // **********
                    // 受付収集入力レコードをループ
                    foreach (T_UKETSUKE_SS_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeEntry.Insert(entity);
                    }

                    // 受付収集明細レコードをループ
                    foreach (T_UKETSUKE_SS_DETAIL entity in this.insDetailEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeDetail.Insert(entity);
                    }

                    // アンテナレ指定コードをループ
                    foreach (T_CONTENA_RESERVE entity in this.insContenaEntityList)
                    {
                        // 登録処理を行う
                        this.daoContena.Insert(entity);
                    }


                    // コンテナマスタの更新
                    foreach (M_CONTENA contenaMaster in this.contenaMasterList)
                    {
                        this.daoMasterContena.Update(contenaMaster);
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex);
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region [F9]論理削除処理
        /// <summary>
        /// [F9]論理削除処理
        /// </summary>
        [Transaction]
        public bool LogicalDeleteData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    //データ作成
                    if (!this.CreateEntitys())
                    {
                        return false;
                    }

                    // **********
                    // 論理削除
                    // **********
                    // 受付収集入力の元レコードを論理削除
                    this.daoUketsukeEntry.Update(this.delEntryEntity);

                    // アンテナレ指定コードを論理削除
                    foreach (T_CONTENA_RESERVE entity in this.arrContenaReserve)
                    {
                        // 登録処理を行う
                        this.daoContena.Update(entity);
                    }

                    // **********
                    // 新規登録
                    // **********
                    // 受付収集入力レコードをループ
                    foreach (T_UKETSUKE_SS_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        entity.DELETE_FLG = true;
                        this.daoUketsukeEntry.Insert(entity);
                    }

                    // 受付収集明細レコードをループ
                    foreach (T_UKETSUKE_SS_DETAIL entity in this.insDetailEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeDetail.Insert(entity);
                    }

                    // アンテナレ指定コードをループ
                    foreach (T_CONTENA_RESERVE entity in this.insContenaEntityList)
                    {
                        // 登録処理を行う
                        entity.DELETE_FLG = true;
                        this.daoContena.Insert(entity);
                    }

                    // 車種台数番号を更新
                    this.daoUketsukeEntry.UpdateShashuDaisuNumber(
                        (long)this.dtUketsukeEntry.Rows[0]["SHASHU_DAISU_GROUP_NUMBER"],
                        (Int16)this.dtUketsukeEntry.Rows[0]["SHASHU_DAISU_NUMBER"]);

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); 
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region [F10]行挿入処理
        /// <summary>
        /// [F10]行挿入処理
        /// </summary>
        internal bool AddNewRow()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //// 選択されていない場合
                //if (this.form.dgvDetail.SelectedRows.Count < 1)
                //{
                //    // 処理終了
                //    return;
                //}

                this.dgvDetail.CellValidating -= this.form.dgvDetail_CellValidating;
                this.dgvDetail.CellValidated -= this.form.dgvDetail_OnValidated;

                // 行挿入
                this.form.dgvDetail.Rows.Insert(this.form.dgvDetail.CurrentRow.Index, 1);

                // フォーカス設定
                this.form.dgvDetail["HINMEI_CD", this.form.dgvDetail.CurrentRow.Index - 1].Selected = true;
                this.form.dgvDetail.Focus();

                this.dgvDetail.CellValidating += this.form.dgvDetail_CellValidating;
                this.dgvDetail.CellValidated += this.form.dgvDetail_OnValidated;

            }
            catch (Exception ex)
            {
                LogUtility.Error("AddNewRow", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region [F11]行削除処理
        /// <summary>
        /// [F11]行削除処理
        /// </summary>
        internal bool RemoveSelectedRow()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 選択されていない場合
                //if (this.form.dgvDetail.SelectedRows.Count < 1 ||
                //    this.form.dgvDetail.CurrentRow.Index == this.form.dgvDetail.Rows.Count - 1)
                //{
                //    // 処理終了
                //    return;
                //}
                if (this.form.dgvDetail.CurrentRow.Index == this.form.dgvDetail.Rows.Count - 1)
                {
                    // 処理終了
                    return ret;
                }

                // 行削除
                this.form.dgvDetail.Rows.RemoveAt(this.form.dgvDetail.CurrentRow.Index);

                // 合計値を再計算
                this.CalcTotalValues();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる　処理
        /// </summary>
        internal void CloseForm()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.Close();
                this.parentForm.Close();

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

        #region [subF1]地図表示
        /// <summary>
        /// [subF1]地図表示　処理
        /// </summary>
        internal void MapOpen()
        {
            try
            {
                this.form.Close();
                this.parentForm.Close();

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

        #region ポップアップボタン名により、テキスト名を取得
        /// <summary>
        /// ポップアップボタン名により、テキスト名を取得
        /// </summary>
        /// <returns></returns>
        internal string GetTextName(String buttonName)
        {
            string textName = "";

            try
            {
                LogUtility.DebugMethodStart(buttonName);

                switch (buttonName)
                {
                    case "TORIHIKISAKI_SEARCH_BUTTON":
                        textName = "TORIHIKISAKI_CD";
                        break;
                    case "GYOUSHA_SEARCH_BUTTON":
                        textName = "GYOUSHA_CD";
                        break;
                    case "GENBA_SEARCH_BUTTON":
                        textName = "GENBA_CD";
                        break;
                    case "UNPAN_GYOUSHA_SEARCH_BUTTON":
                        textName = "UNPAN_GYOUSHA_CD";
                        break;
                    case "NIOROSHI_GYOUSHA_SEARCH_BUTTON":
                        textName = "NIOROSHI_GYOUSHA_CD";
                        break;
                    case "NIOROSHI_GENBA_SEARCH_BUTTON":
                        textName = "NIOROSHI_GENBA_CD";
                        break;
                    default:
                        break;
                }
                return textName;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(textName);
            }
        }
        #endregion

        #region 移動時間チェック
        /// <summary>
        /// 移動時間の未入力チェックをします
        /// </summary>
        /// <param name="idouHourFrom">移動開始時</param>
        /// <param name="idouMinuteFrom">移動開始分</param>
        /// <param name="idouHourTo">移動終了時</param>
        /// <param name="idouMinuteTo">移動終了分</param>
        /// <returns>チェック結果</returns>
        internal bool CheckIdouJikan(string idouHourFrom, string idouMinuteFrom, string idouHourTo, string idouMinuteTo)
        {
            LogUtility.DebugMethodStart(idouHourFrom, idouMinuteFrom, idouHourTo, idouMinuteTo);

            var ret = true;
            var items = String.Empty;

            // どれかが入力してある場合はチェックする
            if (!String.IsNullOrEmpty(idouHourFrom) || !String.IsNullOrEmpty(idouMinuteFrom) || !String.IsNullOrEmpty(idouHourTo) || !String.IsNullOrEmpty(idouMinuteTo))
            {
                // 移動開始時間の未入力チェック
                if ((String.IsNullOrEmpty(idouHourFrom) || String.IsNullOrEmpty(idouMinuteFrom)))
                {
                    ret = false;
                    items += "移動開始時間、";
                }
                // 移動終了時間の未入力チェック
                if ((String.IsNullOrEmpty(idouHourTo) || String.IsNullOrEmpty(idouMinuteTo)))
                {
                    ret = false;
                    items += "移動終了時間、";
                }

                // エラーがあった場合はアラートを表示
                if (false == ret)
                {
                    this.msgLogic.MessageBoxShow("E012", items.TrimEnd('、'));
                }
            }

            LogUtility.DebugMethodStart(idouHourFrom, idouMinuteFrom, idouHourTo, idouMinuteTo);
            return ret;
        }
        #endregion

        #region 運搬業者補助員チェック
        /// <summary>
        /// 移動時間チェック
        /// </summary>
        internal bool UnpanGyoushaHojoinCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text) &&
                    !string.IsNullOrEmpty(this.form.HOJOIN_CD.Text) &&
                    this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region DataGrid明細関連処理

        #region　品名CDに紐づく伝種区分チェック
        /// <summary>
        /// 品名CDに紐づく伝種区分をチェック
        /// </summary>
        /// <returns>
        /// false   伝種区分が、１．受入　３．売上支払　９．共通　以外の場合
        /// true    伝種区分が、１．受入　３．売上支払　９．共通　の場合
        /// </returns>
        internal bool CheckHinmeiDensyu(out bool catchErr)
        {

            LogUtility.DebugMethodStart();

            //初期値設定
            bool hinmeiDenshu = false;
            catchErr = true;
            try
            {
                // 現在の行を取得
                var targetRow = this.form.dgvDetail.CurrentRow;
                DgvCustomTextBoxCell control = (DgvCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                var before = this.form.beforeValuesForDetail[control.Name];

                if (targetRow.Cells["HINMEI_CD"].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells["HINMEI_CD"].Value.ToString()))
                {
                    this.isInputError = false;
                    return false;
                }

                if (targetRow.Cells["HINMEI_CD"].Value.ToString() == before && !this.isInputError)
                {
                    return true;
                }

                this.isInputError = false;

                // 現在の品名CDの値を取得
                var targetHimei = this.GetHinmeiDataByCd(targetRow.Cells["HINMEI_CD"].Value.ToString());
                if (targetHimei == null)
                {
                    return false;
                }

                // 伝票区分を一度キャンセルし、二回目に伝票区分を設定すると、ここは実行されないため
                // 品名を設定するには不適切のためdgvDetail_CellValidatingメソッドに移動。

                // 伝種区分が、１．受入　３．売上支払　９．共通　の場合
                if (targetHimei.DENSHU_KBN_CD == ConstClass.DENSHU_KBN_CD_UKEIRE ||
                   targetHimei.DENSHU_KBN_CD == ConstClass.DENSHU_KBN_CD_UR_SH ||
                   targetHimei.DENSHU_KBN_CD == ConstClass.DENSHU_KBN_CD_KYOTU)
                {
                    hinmeiDenshu = true;
                }
            }

            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHinmeiDensyu", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiDensyu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hinmeiDenshu, catchErr);
            }

            return hinmeiDenshu;

        }
        #endregion

        #region　単位CDチェック
        /// <summary>
        /// 単位CDチェック
        /// </summary>
        /// <returns>
        /// false   存在しない場合
        /// true    存在する場合
        /// </returns>
        internal bool CheckUnit()
        {
            LogUtility.DebugMethodStart();

            // 現在の行を取得
            var targetRow = this.form.dgvDetail.CurrentRow;
            DgvCustomTextBoxCell control = (DgvCustomTextBoxCell)targetRow.Cells["UNIT_CD"];
            var before = this.form.beforeValuesForDetail[control.Name];

            if (targetRow.Cells["UNIT_CD"].Value == null
                || string.IsNullOrEmpty(targetRow.Cells["UNIT_CD"].Value.ToString()))
            {
                targetRow.Cells["UNIT_NAME_RYAKU"].Value = string.Empty;
                this.isInputError = false;
                return true;
            }

            if (targetRow.Cells["UNIT_CD"].Value.ToString() == before && !this.isInputError)
            {
                return true;
            }

            this.isInputError = false;
            // 現在の品名CDの値を取得
            var cellValue = targetRow.Cells["UNIT_CD"].Value.ToString();
            Int16 cd = -1;
            if (Int16.TryParse(cellValue, out cd))
            {
                var targetUnit = this.GetUnit(cd);
                if (targetUnit == null || targetUnit.Length == 0)
                {
                    return false;
                }
                targetRow.Cells["UNIT_NAME_RYAKU"].Value = targetUnit[0].UNIT_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();

            return true;

        }
        #endregion

        #region 伝票区分設定
        /// <summary>
        /// 伝票区分を設定します。（品名の伝票区分が「9:共通」の場合は、伝票区分選択ポップアップを表示します）
        /// </summary>
        /// <returns>伝票区分の設定がされない場合は False</returns>
        internal bool SetDenpyouKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var targetRow = this.form.dgvDetail.CurrentRow;

                if (targetRow == null)
                {
                    return true;
                }

                // 初期化
                targetRow.Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                targetRow.Cells["DENPYOU_KBN_NAME_RYAKU"].Value = string.Empty;

                if (targetRow.Cells["HINMEI_CD"].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells["HINMEI_CD"].Value.ToString()))
                {
                    return true;
                }

                var targetHimei = this.GetHinmeiDataByCd(targetRow.Cells["HINMEI_CD"].Value.ToString());

                if (targetHimei == null || string.IsNullOrEmpty(targetHimei.HINMEI_CD))
                {
                    // 存在しない品名が選択されている場合
                    return true;
                }

                switch (targetHimei.DENPYOU_KBN_CD.ToString())
                {
                    case ConstClass.DENPYOU_KBN_CD_URIAGE_STR:
                    case ConstClass.DENPYOU_KBN_CD_SHIHARAI_STR:
                        targetRow.Cells["DENPYOU_KBN_CD"].Value = (short)targetHimei.DENPYOU_KBN_CD;
                        targetRow.Cells["DENPYOU_KBN_NAME_RYAKU"].Value = denpyouKbnDictionary[(short)targetHimei.DENPYOU_KBN_CD].DENPYOU_KBN_NAME_RYAKU;
                        break;

                    default:
                        // ポップアップを打ち上げ、ユーザに選択してもらう
                        var pos = this.form.dgvDetail.CurrentCell.RowIndex;
                        CustomControlExtLogic.PopUp((ICustomControl)this.form.dgvDetail.Rows[pos].Cells["DENPYOU_KBN_CD"]);

                        var denpyouKbnCd = targetRow.Cells["DENPYOU_KBN_CD"].Value;
                        if (denpyouKbnCd == null || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
                        {
                            // ポップアップでキャンセルが押された
                            // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                            targetRow.Cells["HINMEI_NAME"].Value = string.Empty;
                            targetRow.Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                            targetRow.Cells["DENPYOU_KBN_NAME_RYAKU"].Value = string.Empty;

                            //ポップアップキャンセルフラグをTrueにする。
                            this.form.bCancelDenpyoPopup = true;

                            return false;
                        }

                        break;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetDenpyouKbn", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDenpyouKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            LogUtility.DebugMethodStart();

            return true;
        }
        #endregion

        #region 伝票区分チェック
        /// <summary>
        /// 伝票区分チェック
        /// </summary>
        internal bool CheckDenpyouKbnCd(DataGridViewCellValidatingEventArgs e)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(e);

                // 伝票区分CD取得
                string cellvalue = this.dgvDetail["DENPYOU_KBN_NAME_RYAKU", e.RowIndex].FormattedValue.ToString();
                if ("1".Equals(cellvalue) || "売上".Equals(cellvalue))
                {
                    this.dgvDetail.Rows[e.RowIndex].Cells["DENPYOU_KBN_NAME_RYAKU"].Value = "売上";
                    this.dgvDetail.Rows[e.RowIndex].Cells["DENPYOU_KBN_CD"].Value = "1";
                }
                else if ("2".Equals(cellvalue) || "支払".Equals(cellvalue))
                {
                    this.dgvDetail.Rows[e.RowIndex].Cells["DENPYOU_KBN_NAME_RYAKU"].Value = "支払";
                    this.dgvDetail.Rows[e.RowIndex].Cells["DENPYOU_KBN_CD"].Value = "2";
                }
                else if ("9".Equals(cellvalue) || "共通".Equals(cellvalue))
                {
                    ControlUtility.SetInputErrorOccuredForDgvCell(this.dgvDetail["DENPYOU_KBN_NAME_RYAKU", e.RowIndex], true);
                    // メッセージを表示し、処理中止
                    msgLogic.MessageBoxShow("E029", "伝票区分", "売上、支払");
                    return returnVal;
                }
                else
                {
                    ControlUtility.SetInputErrorOccuredForDgvCell(this.dgvDetail["DENPYOU_KBN_NAME_RYAKU", e.RowIndex], true);
                    // メッセージを表示し、処理中止
                    msgLogic.MessageBoxShow("E034", "伝票区分");
                    return returnVal;
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
        #endregion

        #region 個別品名単価チェック
        /// <summary>
        /// 品名CDが個別品名単価に登録済みかチェック
        /// </summary>
        /// <param name="hinmeiCd">品名CD</param>
        /// <returns>true:エラー(未登録) false:正常(登録済み or チェック対象外)</returns>
        internal bool HasWarnKobetsuHinmeiTanka(string hinmeiCd)
        {
            if (sysInfoEntity == null)
            {
                return false;
            }

            if (sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value != 1)
            {
                // 品名検索抽出区分が「1.個別品名単価」の場合のみチェック
                return false;
            }

            var keyEntity = new M_KOBETSU_HINMEI_TANKA();
            keyEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            keyEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            keyEntity.GENBA_CD = this.form.GENBA_CD.Text;
            keyEntity.HINMEI_CD = hinmeiCd;
            
            var date = DateTime.Now;
            if (!string.IsNullOrEmpty(this.form.UKETSUKE_DATE.Text))
            {
                date = Convert.ToDateTime(this.form.UKETSUKE_DATE.Text);
            }

            var entity = this.kobetsuHinmeiTankaDao.GetDataForHinmei(keyEntity, date);
            if (entity == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 単位設定
        /// <summary>
        /// 単位を設定します
        /// </summary>
        /// <param name="e">イベント引数</param>
        internal bool SetUnit(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                var mHinmei = this.GetHinmeiDataByCd(this.dgvDetail["HINMEI_CD", e.RowIndex].Value.ToString());

                // 単位を設定
                var unitCd = mHinmei.UNIT_CD.IsNull ? null : mHinmei.UNIT_CD.ToString();
                this.dgvDetail["UNIT_CD", e.RowIndex].Value = unitCd;
                if (false == String.IsNullOrEmpty(unitCd) && true == unitDictionary.ContainsKey(short.Parse(unitCd)))
                {
                    this.dgvDetail["UNIT_NAME_RYAKU", e.RowIndex].Value = unitDictionary[short.Parse(unitCd)].UNIT_NAME_RYAKU;
                }
                else
                {
                    this.dgvDetail["UNIT_NAME_RYAKU", e.RowIndex].Value = String.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetUnit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUnit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 単価設定
        /// <summary>
        /// 単価設定
        /// </summary>
        internal bool CalcTanka(DataGridViewRow targetRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null || string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    return ret;
                }
                // 品名CDまたは伝票区分未入力の場合
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["HINMEI_CD"].Value))
                    || string.IsNullOrEmpty(Convert.ToString(targetRow.Cells["DENPYOU_KBN_CD"].Value)))
                {
                    return ret;
                }

                // 単位が空白の場合
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["UNIT_CD"].Value)))
                {
                    return ret;
                }

                var oldTanka = targetRow.Cells["TANKA"].Value == null ? string.Empty : targetRow.Cells["TANKA"].Value.ToString(); // MAILAN #158988 START

                // 単価
                decimal tanka = 0;

                // 取得したい優先順で伝種区分のリストを作成
                var denshuKbnList = new List<DENSHU_KBN>();
                denshuKbnList.Add(DENSHU_KBN.UKEIRE);
                denshuKbnList.Add(DENSHU_KBN.URIAGE_SHIHARAI);
                denshuKbnList.Add(DENSHU_KBN.KYOUTSUU);

                var updateTanka = string.Empty; // MAILAN #158988 START
                // 個別品名単価から取得
                M_KOBETSU_HINMEI_TANKA kobetsuHinmeiTanka = null;
                foreach (DENSHU_KBN denshuKbn in denshuKbnList)
                {
                    kobetsuHinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka((short)denshuKbn,
                                                                                   Convert.ToInt16(targetRow.Cells["DENPYOU_KBN_CD"].Value),
                                                                                   this.form.TORIHIKISAKI_CD.Text,
                                                                                   this.form.GYOUSHA_CD.Text,
                                                                                   this.form.GENBA_CD.Text,
                                                                                   this.form.UNPAN_GYOUSHA_CD.Text,
                                                                                   this.form.NIOROSHI_GYOUSHA_CD.Text,
                                                                                   this.form.NIOROSHI_GENBA_CD.Text,
                                                                                   targetRow.Cells["HINMEI_CD"].Value.ToString(),
                                                                                   Convert.ToInt16(targetRow.Cells["UNIT_CD"].Value),
                                                                                   this.form.SAGYOU_DATE.Text);
                    if (kobetsuHinmeiTanka != null && kobetsuHinmeiTanka.DENSHU_KBN_CD.Value == (short)denshuKbn)
                    {
                        // 取得したい伝種区分のレコードが見つかった時点で検索処理は終了
                        break;
                    }
                }

                // 個別品名単価から情報が取れない場合は基本品名単価の検索
                if (kobetsuHinmeiTanka == null)
                {
                    M_KIHON_HINMEI_TANKA kihonHinmeiTanka = null;
                    foreach (DENSHU_KBN denshuKbn in denshuKbnList)
                    {
                        kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka((short)denshuKbn,
                                                                                   Convert.ToInt16(targetRow.Cells["DENPYOU_KBN_CD"].Value),
                                                                                   this.form.UNPAN_GYOUSHA_CD.Text,
                                                                                   this.form.NIOROSHI_GYOUSHA_CD.Text,
                                                                                   this.form.NIOROSHI_GENBA_CD.Text,
                                                                                   targetRow.Cells["HINMEI_CD"].Value.ToString(),
                                                                                   Convert.ToInt16(targetRow.Cells["UNIT_CD"].Value),
                                                                                   this.form.SAGYOU_DATE.Text);
                        if (kihonHinmeiTanka != null && kihonHinmeiTanka.DENSHU_KBN_CD.Value == (short)denshuKbn)
                        {
                            // 取得したい伝種区分のレコードが見つかった時点で検索処理は終了
                            break;
                        }
                    }

                    if (kihonHinmeiTanka != null)
                    {
                        decimal.TryParse(Convert.ToString(kihonHinmeiTanka.TANKA.Value), out tanka);
                        updateTanka = kihonHinmeiTanka.TANKA.Value.ToString(); // MAILAN #158988 START
                    }
                    else
                    {
                        if (this.form.WindowType != WINDOW_TYPE.UPDATE_WINDOW_FLAG) // ban chuan
                        {
                            // 個別品名単価及び基本品名単価にも該当しない場合単価を空にする
                            targetRow.Cells["TANKA"].Value = string.Empty;
                            return ret; // ban chuan
                        }
                        updateTanka = string.Empty; // MAILAN #158988 START
                    }
                }
                else
                {
                    decimal.TryParse(Convert.ToString(kobetsuHinmeiTanka.TANKA.Value), out tanka);
                    updateTanka = kobetsuHinmeiTanka.TANKA.Value.ToString(); // MAILAN #158988 START
                }

                // MAILAN #158988 START
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    decimal oldTankaValue = -1;
                    decimal updateTankaValue = -1;
                    if (oldTanka != null && !string.IsNullOrEmpty(oldTanka.ToString()))
                    {
                        oldTankaValue = decimal.Parse(oldTanka.ToString());
                    }
                    if (updateTanka != null && !string.IsNullOrEmpty(updateTanka.ToString()))
                    {
                        updateTankaValue = decimal.Parse(updateTanka.ToString());
                    }

                    if (!oldTankaValue.Equals(updateTankaValue))
                    {
                        if (!this.isTankaMessageShown)
                        {
                            this.isTankaMessageShown = true;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            if (msgLogic.MessageBoxShow("C127") == DialogResult.Yes)
                            {
                                targetRow.Cells["TANKA"].Value = updateTanka;
                            }
                            else
                            {
                                //this.ResetTankaCheck();
                                //return false;
                                this.isContinueCheck = false;
                            }
                        }
                        else
                        {
                            if (this.isContinueCheck)
                            {
                                targetRow.Cells["TANKA"].Value = updateTanka;
                            }
                        }
                    }
                }
                // MAILAN #158988 END
                else // ban chuan
                {
                    // 単価を設定
                    targetRow.Cells["TANKA"].Value = tanka; // ban chuan
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcTanka", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTanka", ex);
                this.msgLogic.MessageBoxShow("E245", "");
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
        internal bool CalcDetailKingaku(DataGridViewRow targetRow)
        {
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
                short kingakuHasuuCd = 0;

                // 端数取得
                kingakuHasuuCd = CalcHasuu(targetRow);

                if (decimal.TryParse(Convert.ToString(targetRow.Cells["SUURYOU"].FormattedValue), out suuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells["TANKA"].FormattedValue), out tanka))
                {
                    // 数量と単価がNullではない場合、金額を自動計算する
                    //var kingaku = CommonCalc.DecimalFormat(CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd));
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);
                    //var maxLength = ((DataGridViewTextBoxColumn)this.form.dgvDetail.Columns["HINMEI_KINGAKU"]).MaxInputLength;
                    //kingaku = kingaku.Replace(",", "");
                    //if (kingaku.Length > maxLength)
                    //{
                    //    kingaku = kingaku.Substring(0, maxLength);
                    //}

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    //targetRow.Cells["HINMEI_KINGAKU"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(kingaku));
                    targetRow.Cells["HINMEI_KINGAKU"].Value = kingaku;
                }
                else
                {
                    // 数量と単価どちらかがNull、かつ単価が編集可能の場合、金額をクリアする
                    if (!targetRow.Cells["TANKA"].ReadOnly)
                    {
                        targetRow.Cells["HINMEI_KINGAKU"].Value = null;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetailKingaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 合計系の計算
        /// <summary>
        /// 合計系の計算
        /// </summary>
        internal bool CalcTotalValues()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 売上金額合計
                decimal uriageKingakuGoukei = this.form.dgvDetail.Rows.Cast<DataGridViewRow>().Where(r => null != r.Cells["DENPYOU_KBN_CD"].Value
                                                                                                          && ConstClass.DENPYOU_KBN_CD_URIAGE_STR == r.Cells["DENPYOU_KBN_CD"].Value.ToString()
                                                                                                          && null != r.Cells["HINMEI_KINGAKU"].Value)
                                                                                              .Sum(r => decimal.Parse(r.Cells["HINMEI_KINGAKU"].Value.ToString()));

                // 支払金額合計
                decimal shiharaiKingakuGoukei = this.form.dgvDetail.Rows.Cast<DataGridViewRow>().Where(r => null != r.Cells["DENPYOU_KBN_CD"].Value
                                                                                                          && ConstClass.DENPYOU_KBN_CD_SHIHARAI_STR == r.Cells["DENPYOU_KBN_CD"].Value.ToString()
                                                                                                          && null != r.Cells["HINMEI_KINGAKU"].Value)
                                                                                                .Sum(r => decimal.Parse(r.Cells["HINMEI_KINGAKU"].Value.ToString()));

                // 差引額
                decimal sashihikigaku = 0;
                if (ConstClass.DENPYOU_KBN_CD_URIAGE_STR == this.systemUrShCalcBaseKbn)
                {
                    sashihikigaku = uriageKingakuGoukei - shiharaiKingakuGoukei;
                }
                else
                {
                    sashihikigaku = shiharaiKingakuGoukei - uriageKingakuGoukei;
                }

                // コントロールにセット
                this.form.URIAGE_TOTAL.Text = CommonCalc.DecimalFormat(uriageKingakuGoukei);
                this.form.SHIHARAI_TOTAL.Text = CommonCalc.DecimalFormat(shiharaiKingakuGoukei);
                this.form.SASHIHIKIGAKU.Text = CommonCalc.DecimalFormat(sashihikigaku);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcTotalValues", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcTotalValues", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        #region 全ての明細と合計の計算
        /// <summary>
        /// 全ての明細と合計の計算
        /// </summary>
        internal bool CalcAllDetailAndTotal()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 1行しかない場合
                if (this.form.dgvDetail.Rows == null || this.form.dgvDetail.Rows.Count <= 1)
                {
                    // 処理終了
                    return ret;
                }

                // 明細行ループ
                for (int i = 0; i < this.form.dgvDetail.Rows.Count - 1; i++)
                {
                    DataGridViewRow dr = this.form.dgvDetail.Rows[i];
                    // 品名CDが未入力または、単価がReadOnlyの場合、計算しない
                    if (String.IsNullOrEmpty(Convert.ToString(dr.Cells["HINMEI_CD"].Value))
                        || dr.Cells["TANKA"].ReadOnly)
                    {
                        continue;
                    }

                    // 単価設定
                    this.CalcTanka(dr);

                    // 単価が未入力の場合かつ、金額がReadOnlyじゃない場合は計算しない
                    if (String.IsNullOrEmpty(Convert.ToString(dr.Cells["TANKA"].Value))
                        && !dr.Cells["HINMEI_KINGAKU"].ReadOnly)
                    {
                        continue;
                    }

                    // 明細金額計算
                    this.CalcDetailKingaku(dr);
                }
                this.ResetTankaCheck(); // MAILAN #158988 START

                // 合計系の計算
                this.CalcTotalValues();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                this.form.SetIchranReadOnlyForAll();
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 端数を取得
        /// <summary>
        /// 端数を取得
        /// </summary>
        internal short CalcHasuu(DataGridViewRow targetRow)
        {
            short returnVal = 0;

            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return returnVal;
                }

                // 伝票区分により、端数を設定
                switch (Convert.ToString(targetRow.Cells["DENPYOU_KBN_CD"].Value))
                {
                    case "1":
                        // 取引先請求
                        var torihikisakiSeikyuu = this.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);
                        if (torihikisakiSeikyuu != null)
                        {
                            short.TryParse(Convert.ToString(torihikisakiSeikyuu.KINGAKU_HASUU_CD), out returnVal);
                        }
                        break;
                    case "2":
                        // 取引先支払
                        var torihikisakiShiharai = this.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
                        if (torihikisakiShiharai != null)
                        {
                            short.TryParse(Convert.ToString(torihikisakiShiharai.KINGAKU_HASUU_CD), out returnVal);
                        }
                        break;
                    default:
                        break;
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

        #region 取引先_請求情報を取得
        /// <summary>
        /// 取引先_請求情報を取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuu(string torihikisakiCd)
        {
            M_TORIHIKISAKI_SEIKYUU returnVal = null;

            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return returnVal;
                }

                IM_TORIHIKISAKI_SEIKYUUDao dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                returnVal = dao.GetDataByCd(torihikisakiCd);

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

        #region 取引先_支払情報を取得
        /// <summary>
        /// 取引先_支払情報を取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharai(string torihikisakiCd)
        {
            M_TORIHIKISAKI_SHIHARAI returnVal = null;
            try
            {
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return returnVal;
                }

                IM_TORIHIKISAKI_SHIHARAIDao dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                returnVal = dao.GetDataByCd(torihikisakiCd);

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

        #region 行NO設定
        /// <summary>
        /// 行NO設定
        /// </summary>
        internal void SetRowNo()
        {
            // 行NO設定
            for (int i = 0; i < this.dgvDetail.Rows.Count - 1; i++)
            {
                this.dgvDetail["ROW_NO", i].Value = i + 1;
            }
        }
        #endregion

        #endregion

        #region Utility

        #region [Date,Hour,Minute]をDateTime[yyyy/mm/dd hh:mm:ss]に組合
        /// <summary>
        /// [Date,Hour,Minute]をDateTime[yyyy/mm/dd hh:mm:ss]に組合
        /// </summary>
        /// <param name="objDate">date</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <param name="strDatetime">組合値</param>
        /// <returns>bool</returns>
        private bool TryChgDHMtoDateTime(object objDate, string hour, string minute, out string strDatetime)
        {
            // 戻り値初期化
            bool returnVal = false;
            strDatetime = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(objDate, hour, minute, strDatetime);

                // DateがNullの場合
                if (objDate == null)
                {
                    // 処理終了
                    return returnVal;
                }

                strDatetime += ((DateTime)objDate).ToShortDateString();
                strDatetime += " ";
                strDatetime += this.ChgNullToValue(hour, "00");
                strDatetime += ":";
                strDatetime += this.ChgNullToValue(minute, "00");
                strDatetime += ":00";

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
                LogUtility.DebugMethodEnd(returnVal, strDatetime);
            }
        }
        #endregion

        #region [Hour,Minute]を[hh:mm]に組合
        /// <summary>
        /// [Hour,Minute]を[hh:mm]に組合
        /// </summary>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <param name="strTime">組合値</param>
        /// <returns>bool</returns>
        private bool TryChgHMtoTime(string hour, string minute, out string strTime)
        {
            // 戻り値初期化
            bool returnVal = false;
            strTime = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(hour, minute, strTime);

                if (string.IsNullOrEmpty(hour) && string.IsNullOrEmpty(minute))
                {
                    return returnVal;
                }

                strTime += this.ChgNullToValue(hour, "00");
                strTime += ":";
                strTime += this.ChgNullToValue(minute, "00");

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
                LogUtility.DebugMethodEnd(returnVal, strTime);
            }
        }
        #endregion

        #region DateTime[yyyy/mm/dd hh:mm:ss]値を分解
        /// <summary>
        /// DateTime[yyyy/mm/dd hh:mm:ss]値を分解
        /// </summary>
        /// <param name="objDateTime">分解対象</param>
        /// <param name="date">date</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <returns>bool</returns>
        private bool TryChgDateTimeToDHM(object objDateTime, out string date, out string hour, out string minute)
        {
            // 戻り値初期化
            bool returnVal = false;
            date = string.Empty;
            hour = string.Empty;
            minute = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(objDateTime, date, hour, minute);

                // DBNullの場合
                if (objDateTime is DBNull)
                {
                    // 処理終了
                    return returnVal;
                }

                // DateTimeに変換
                DateTime dateTime = (DateTime)objDateTime;

                // Date
                date = dateTime.Date.ToShortDateString();
                // Hour
                hour = dateTime.Hour.ToString();
                // Minute
                minute = dateTime.Minute.ToString();

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
                LogUtility.DebugMethodEnd(returnVal, date, hour, minute);
            }
        }
        #endregion

        #region Time値[hh:mm]を分解
        /// <param name="objTime">分解対象</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <returns>bool</returns>
        private bool TryChgTimeToHM(object objTime, out string hour, out string minute)
        {
            // 戻り値初期化
            bool returnVal = false;
            hour = string.Empty;
            minute = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(objTime, hour, minute);

                // DBNullの場合
                if (objTime is DBNull)
                {
                    // 処理終了
                    return returnVal;
                }

                // Timeを配列に変換
                string[] temp = objTime.ToString().Split(':');
                if (temp.Length > 1)
                {
                    // Hour
                    hour = string.IsNullOrEmpty(temp[0]) ? string.Empty : int.Parse(temp[0]).ToString();
                    // Minute
                    minute = string.IsNullOrEmpty(temp[1]) ? string.Empty : int.Parse(temp[1]).ToString();
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
                LogUtility.DebugMethodEnd(returnVal, hour, minute);
            }
        }
        #endregion

        #region DBNull値を指定値に変換
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
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return value;
                    else
                        return obj;
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
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region Null値を指定値に変換
        /// <summary>
        /// Null値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>string</returns>
        private string ChgNullToValue(string obj, string value)
        {
            string returnVal = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(obj))
                {
                    returnVal = value;
                }
                else
                {
                    returnVal = obj;
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

        #region フィールドPadding
        /// <summary>
        /// フィールドの値をPadding
        /// </summary>
        /// <param name="obj">フィールド対象</param>
        /// <param name="len">length</param>
        /// <param name="padChar">char</param>
        /// <returns>string</returns>
        private string FieldPadLeft(object obj, int len, char padChar)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(obj, len, padChar);

                string temp = this.ChgDBNullToValue(obj, string.Empty).ToString();

                if (!string.IsNullOrEmpty(temp))
                {
                    returnVal = temp.PadLeft(len, padChar);
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

        #region 単価、数量の共通フォーマット
        /// <summary>
        /// 単価、数量の共通フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string SuuryouAndTankFormat(object num, String format)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(num, format);

                returnVal = string.Format("{0:" + format + "}", num);

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

        #region コントロールのReadonlyプロパティ設定
        /// <summary>
        /// コントロールのReadonlyプロパティ設定
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="isReadOnly"></param>
        internal void SetCtrlReadonly(CustomTextBox ctrl, bool isReadOnly)
        {
            try
            {
                LogUtility.DebugMethodStart(ctrl, isReadOnly);

                ctrl.ReadOnly = isReadOnly;
                ctrl.TabStop = !isReadOnly;
                if (isReadOnly)
                {
                    ctrl.Tag = string.Empty;
                    return;
                }

                switch (ctrl.Name)
                {
                    case "TORIHIKISAKI_NAME":
                    case "GYOUSHA_NAME":
                    case "GENBA_NAME":
                    case "GENBA_ADDRESS1":
                    case "GENBA_ADDRESS2":
                    case "UNPAN_GYOUSHA_NAME":
                    case "NIOROSHI_GYOUSHA_NAME":
                    case "NIOROSHI_GENBA_NAME":
                        ctrl.Tag = string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv((ctrl.MaxLength/2).ToString(), VbStrConv.Wide));
                        break;
                    case "GYOSHA_TEL":
                    case "GENBA_TEL":
                    case "TANTOSHA_TEL":
                        ctrl.Tag = string.Format(ConstClass.Hint.HANKAKU, Strings.StrConv(ctrl.MaxLength.ToString(), VbStrConv.Wide));
                        break;
                    case "TANTOSHA_NAME":
                        ctrl.Tag = "全角８桁以内で入力してください";
                        break;
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

        #region 諸口区分用プレビューキーダウンイベント
        /// <summary>
        /// 諸口区分用プレビューキーダウンイベント
        /// 諸口区分が存在する取引先、業者、現場で使用する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDownForShokuchikbnCheck(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.Tab:
                        this.pressedEnterOrTab = true;
                        break;

                    default:
                        this.pressedEnterOrTab = false;
                        break;
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

        #region 諸口区分用フォーカス移動処理
        /// <summary>
        /// 諸口区分用フォーカス移動処理
        /// </summary>
        /// <param name="control"></param>
        internal bool MoveToNextControlForShokuchikbnCheck(ICustomControl control)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(control);

                if (this.pressedEnterOrTab)
                {
                    var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                    this.form.SelectNextControl((Control)control, !isPressShift, true, true, true);
                }

                // マウス操作を考慮するためpressedEnterOrTabを初期化
                pressedEnterOrTab = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #endregion

        #region 共通で使用する計算処理クラス
        /// <summary>
        /// 共通で使用する計算処理クラス
        /// </summary>
        public static class CommonCalc
        {
            /// <summary>
            /// 端数処理種別
            /// </summary>
            private enum fractionType : int
            {
                CEILING = 1,	// 切り上げ
                FLOOR,		// 切り捨て
                ROUND,		// 四捨五入
            }

            /// <summary>
            /// 端数処理桁用Enum
            /// </summary>
            private enum hasuKetaType : short
            {
                NONE = 1,       // 1の位
                ONEPOINT,       // 小数第一位
                TOWPOINT,       // 小数第二位
                THREEPOINT,     // 小数第三位
                FOUR,           // 小数第四位
            }

            /// <summary>
            /// 指定された端数CDに従い、金額の端数処理を行う
            /// </summary>
            /// <param name="kingaku">端数処理対象金額</param>
            /// <param name="calcCD">端数CD</param>
            /// <returns name="decimal">端数処理後の金額</returns>
            public static decimal FractionCalc(decimal kingaku, int calcCD)
            {
                LogUtility.DebugMethodStart(kingaku, calcCD);

                decimal returnVal = 0;		// 戻り値
                decimal sign = 1;
                if (kingaku < 0)
                {
                    sign = -1;
                }

                kingaku = Math.Abs(kingaku);

                switch ((fractionType)calcCD)
                {
                    case fractionType.CEILING:
                        returnVal = Math.Ceiling(kingaku);
                        break;
                    case fractionType.FLOOR:
                        returnVal = Math.Floor(kingaku);
                        break;
                    case fractionType.ROUND:
                        returnVal = Math.Round(kingaku, 0, MidpointRounding.AwayFromZero);
                        break;
                    default:
                        // 何もしない
                        returnVal = kingaku;
                        break;
                }

                returnVal = returnVal * sign;

                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }

            /// <summary>
            /// 指定された端数CD、端数桁数に従い、金額の端数処理を行う
            /// </summary>
            /// <param name="kingaku">端数処理対象金額</param>
            /// <param name="calcCD">端数CD</param>
            /// <param name="hasuKeta">端数処理桁数(SYS_INFOの端数処理桁数をそのまま指定してください)</param>
            /// <returns name="decimal">端数処理後の金額</returns>
            public static decimal FractionCalc(decimal kingaku, int calcCD, short hasuKeta)
            {

                LogUtility.DebugMethodStart(kingaku, calcCD, hasuKeta);

                decimal returnVal = 0;		// 戻り値
                double hasuKetaCoefficient = 1;
                decimal sign = 1;
                if (kingaku < 0)
                {
                    sign = -1;
                }

                switch ((hasuKetaType)hasuKeta)
                {
                    case hasuKetaType.NONE:
                        break;

                    default:
                        hasuKetaCoefficient = Math.Pow(10, hasuKeta - 2);
                        break;
                }

                kingaku = Math.Abs(kingaku);

                switch ((fractionType)calcCD)
                {
                    case fractionType.CEILING:
                        returnVal = Math.Ceiling(kingaku * (decimal)hasuKetaCoefficient) / (decimal)hasuKetaCoefficient;
                        break;
                    case fractionType.FLOOR:
                        returnVal = Math.Floor(kingaku * (decimal)hasuKetaCoefficient) / (decimal)hasuKetaCoefficient;
                        break;
                    case fractionType.ROUND:
                        returnVal = Math.Round(kingaku * (decimal)hasuKetaCoefficient, 0, MidpointRounding.AwayFromZero) / (decimal)hasuKetaCoefficient;
                        break;
                    default:
                        // NOTHING
                        break;
                }
                
                returnVal = returnVal * sign;

                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }

            /// <summary>
            /// 金額の共通フォーマットメソッド
            /// 単価などM_SYS_INFO等にフォーマットが設定されている
            /// ものについては使用しないでください
            /// </summary>
            /// <param name="num"></param>
            /// <returns></returns>
            public static string DecimalFormat(decimal num)
            {
                LogUtility.DebugMethodStart(num);

                string returnVal = string.Empty;		// 戻り値

                string format = "#,##0";
                returnVal = string.Format("{0:" + format + "}", num);

                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }
        }
        #endregion 共通で使用する計算処理クラス

        #region その他(使わない)

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

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        internal bool SetErrorFocus()
        {
            bool ret = true;
            try
            {
                //必須チェックフォーカス処理
                bool errorFlg = false;
                if (this.form.RegistErrorFlag)
                {
                    var allControl = this.form.GetAllControl();
                    Control[] sortControl = allControl.OfType<Control>().OrderBy(s => s.TabIndex).ToArray();


                    var k = this.form.allControl.Where(c => c.Name == "KYOTEN_CD");

                    foreach (Control ctr in sortControl)
                    {
                        // TextBoxの場合
                        var textBox = ctr as ICustomTextBox;

                        if (textBox != null)
                        {
                            if (textBox.IsInputErrorOccured)
                            {
                                ctr.Focus();
                                errorFlg = true;
                            }
                        }

                        // CustomDateTimePickerの場合
                        var CustomDateTimePicker = ctr as CustomDateTimePicker;

                        if (CustomDateTimePicker != null)
                        {
                            if (CustomDateTimePicker.IsInputErrorOccured)
                            {
                                ctr.Focus();
                                errorFlg = true;
                            }
                        }


                        if (errorFlg)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetErrorFocus", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion 必須チェックエラーフォーカス処理

        /// <summary>
        /// 車輌CD入力チェック
        /// </summary>
        internal bool CheckSharyou(bool isRireki = false)//CongBinh 20210713 #152801
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isRireki);//CongBinh 20210713 #152801

                // 参照・削除モード時は処理しない
                if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG == this.form.WindowType || WINDOW_TYPE.DELETE_WINDOW_FLAG == this.form.WindowType)
                {
                    return ret;
                }

                var inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                var inputShashuCd = this.form.SHASHU_CD.Text;
                var inputSharyouCd = this.form.SHARYOU_CD.Text;
                var inputUntenshaCd = this.form.UNTENSHA_CD.Text;

                // 車輌CDが入力されていなければ処理しない
                if (String.IsNullOrEmpty(inputSharyouCd))
                {
                    this.sharyouCd = String.Empty;
                    this.form.SHARYOU_NAME.Text = String.Empty;
                    return ret;
                }

                // 前回チェック時と変更がなければ処理しない（運搬業者CD、車種CDが入力されていなければ複数該当の可能性があるので処理）
                if (inputSharyouCd == this.sharyouCd && (!String.IsNullOrEmpty(inputUnpanGyoushaCd) || !String.IsNullOrEmpty(inputShashuCd)))
                {
                    return ret;
                }

                // 入力されている条件で車輌を取得
                var keyEntity = new M_SHARYOU();
                if (!String.IsNullOrEmpty(inputSharyouCd))
                {
                    keyEntity.SHARYOU_CD = inputSharyouCd;
                }
                if (!string.IsNullOrEmpty(inputUnpanGyoushaCd))
                {
                    keyEntity.GYOUSHA_CD = inputUnpanGyoushaCd;
                }
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
                SqlDateTime sagyouDate = SqlDateTime.Null;
                if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    sagyouDate = SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                }
                var mSharyouList = this.sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "1", sagyouDate, true, false, false).Cast<M_SHARYOU>();
                // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END


                // ポップアップ表示フラグ
                bool isPopup = false;

                if (mSharyouList.Count() == 0)
                {
                    // マスタに無い
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "車輌");
                    this.form.SHARYOU_CD.Focus();
                    this.sharyouCd = String.Empty;
                    this.form.SHARYOU_NAME.Text = String.Empty;
                }
                else if (mSharyouList.Count() == 1)
                {
                    // 該当が1件
                    SetSharyou(mSharyouList.FirstOrDefault(), isRireki);//CongBinh 20210713 #152801
                }
                else
                {
                    if (!String.IsNullOrEmpty(inputUnpanGyoushaCd) && !String.IsNullOrEmpty(inputShashuCd))
                    {
                        if (mSharyouList.Where(s => s.GYOUSHA_CD == inputUnpanGyoushaCd && s.SHASYU_CD == inputShashuCd).Count() == 0)
                        {
                            // 運搬業者CD、車種CDの条件で該当が0件
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "運搬業者、車種");
                            this.form.SHARYOU_CD.Focus();
                            this.sharyouCd = String.Empty;
                            this.form.SHARYOU_NAME.Text = String.Empty;
                        }
                        else if (mSharyouList.Where(s => s.GYOUSHA_CD == inputUnpanGyoushaCd && s.SHASYU_CD == inputShashuCd).Count() == 1)
                        {
                            // 運搬業者CD、車種CDの条件で該当が1件
                            SetSharyou(mSharyouList.Where(s => s.GYOUSHA_CD == inputUnpanGyoushaCd && s.SHASYU_CD == inputShashuCd).FirstOrDefault(), isRireki);//CongBinh 20210713 #152801
                        }
                        else
                        {
                            // 複数が該当
                            isPopup = true;
                        }
                    }
                    else if (!String.IsNullOrEmpty(inputUnpanGyoushaCd) && String.IsNullOrEmpty(inputShashuCd))
                    {
                        if (mSharyouList.Where(s => s.GYOUSHA_CD == inputUnpanGyoushaCd).Count() == 0)
                        {
                            // 運搬業者CDの条件で該当が0件
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "運搬業者");
                            this.form.SHARYOU_CD.Focus();
                            this.sharyouCd = String.Empty;
                            this.form.SHARYOU_NAME.Text = String.Empty;
                        }
                        else if (mSharyouList.Where(s => s.GYOUSHA_CD == inputUnpanGyoushaCd).Count() == 1)
                        {
                            // 運搬業者CDの条件で該当が1件
                            SetSharyou(mSharyouList.Where(s => s.GYOUSHA_CD == inputUnpanGyoushaCd).FirstOrDefault(), isRireki);//CongBinh 20210713 #152801
                        }
                        else
                        {
                            // 複数が該当
                            isPopup = true;
                        }
                    }
                    else if (String.IsNullOrEmpty(inputUnpanGyoushaCd) && !String.IsNullOrEmpty(inputShashuCd))
                    {
                        if (mSharyouList.Where(s => s.SHASYU_CD == inputShashuCd).Count() == 0)
                        {
                            // 車種CDの条件で該当が0件
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E062", "車種");
                            this.form.SHARYOU_CD.Focus();
                            this.sharyouCd = String.Empty;
                            this.form.SHARYOU_NAME.Text = String.Empty;
                        }
                        else if (mSharyouList.Where(s => s.SHASYU_CD == inputShashuCd).Count() == 1)
                        {
                            // 車種CDの条件で該当が1件
                            SetSharyou(mSharyouList.Where(s => s.SHASYU_CD == inputShashuCd).FirstOrDefault(), isRireki);//CongBinh 20210713 #152801
                        }
                        else
                        {
                            // 複数が該当
                            isPopup = true;
                        }
                    }
                    else
                    {
                        isPopup = true;
                    }

                    if (isPopup)
                    {
                        // 複数が該当する場合はポップアップを表示
                        this.form.SHARYOU_CD.Focus();
                        CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);

                        this.sharyouCd = this.form.SHARYOU_CD.Text;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckSharyou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 引数の車輌エンティティの情報を画面にセットします
        /// </summary>
        /// <param name="sharyou">車輌エンティティ</param>
        private void SetSharyou(M_SHARYOU sharyou, bool isRireki)//CongBinh 20210713 #152801
        {
            LogUtility.DebugMethodStart(sharyou, isRireki);//CongBinh 20210713 #152801

            this.form.SHARYOU_NAME.Text = sharyou.SHARYOU_NAME_RYAKU;
            //CongBinh 20210713 #152801 S
            if (!isRireki)
            {
                var gyousha = this.gyoushaDao.GetDataByCd(sharyou.GYOUSHA_CD);
                if (null != gyousha)
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                    this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = String.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.Text = String.Empty;
                }

                var shashu = this.shashuDao.GetDataByCd(sharyou.SHASYU_CD);
                if (null != shashu)
                {
                    this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                    this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                }
                else
                {
                    this.form.SHASHU_CD.Text = String.Empty;
                    this.form.SHASHU_NAME.Text = String.Empty;
                }
            }
            //CongBinh 20210713 #152801 E

            var untensha = this.shainDao.GetDataByCd(sharyou.SHAIN_CD);
            if (null != untensha)
            {
                this.form.UNTENSHA_CD.Text = untensha.SHAIN_CD;
                this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
            }
            else
            {
                this.form.UNTENSHA_CD.Text = String.Empty;
                this.form.UNTENSHA_NAME.Text = String.Empty;
            }

            this.sharyouCd = this.form.SHARYOU_CD.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 配車状況CDチェック
        /// </summary>
        /// <param name="haishaJokyoCd">チェック対象の配車CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckHaishaJokyoCd(string haishaJokyoCd)
        {
            LogUtility.DebugMethodStart(haishaJokyoCd);

            bool ret = false;

            if (String.IsNullOrEmpty(haishaJokyoCd))
            {
                ret = true;
            }

            var count = this.haishaJokyoDataTable.Rows.Cast<DataRow>().Where(r => !String.IsNullOrEmpty(haishaJokyoCd) && r.ItemArray[0].ToString() == haishaJokyoCd)
                                                                      .Count();
            if (0 < count)
            {
                ret = true;
            }

            LogUtility.DebugMethodEnd(ret);

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
                ret = this.haishaJokyoDataTable.Rows.Cast<DataRow>().Where(r => r.ItemArray[0].ToString() == haishaJokyoCd)
                                                                    .Select(r => r.ItemArray[1].ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHaishaJokyo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        /// <summary>
        /// 画面の状態とシステム設定によって、[F8]指示書ボタンの状態を切り替えます
        /// </summary>
        private void ChangeBt_func8Enable()
        {
            LogUtility.DebugMethodStart();

            // [F8]指示書ボタンを使用不可にする（№3811）
            //if ((this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG &&
            //    !this.sysInfoEntity.UKETSUKE_SIJISHO_PRINT_KBN.IsNull &&
            //    1 == this.sysInfoEntity.UKETSUKE_SIJISHO_PRINT_KBN.Value) ||
            //    (ConstClass.HAISHA_JOKYO_CD_HAISHA == this.form.HAISHA_JOKYO_CD.Text &&
            //    ConstClass.HAISHA_SHURUI_CD_KAKUTEI == this.form.HAISHA_SHURUI_CD.Text))
            //{
            //    this.parentForm.bt_func8.Enabled = true;
            //}
            //else
            //{
            //    this.parentForm.bt_func8.Enabled = false;
            //}
            this.parentForm.bt_func8.Enabled = false;

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// データ移動処理
        /// </summary>
        internal bool SetMoveData()
        {

            if (this.form.moveData_flg)
            {
                this.form.TORIHIKISAKI_CD.Text = this.form.moveData_torihikisakiCd;
                if (!this.form.SetTorihikisaki())
                {
                    return false;
                }
                this.form.GYOUSHA_CD.Text = this.form.moveData_gyousyaCd;
                if (!this.form.SetGyousha())
                {
                    return false;
                }
                this.form.GENBA_CD.Text = this.form.moveData_genbaCd;
                if (!this.form.SetGenba())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// コンテナ情報が変更されているかチェック
        /// </summary>
        /// <returns>true:変更有, false:変更無</returns>
        internal bool IsChengeContenaData()
        {
            bool returnVal = false;

            // 配車状況が変更されていれば変更有と判断して、コンテナチェックさせる
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.form.WindowType)
            {
                var haisyaJoukyou = -1;
                if (!string.IsNullOrEmpty(this.form.HAISHA_JOKYO_CD.Text))
                {
                    haisyaJoukyou = short.Parse(this.form.HAISHA_JOKYO_CD.Text);
                }

                if (haisyaJoukyou > -1
                    && beforeHaishaJoukyou != haisyaJoukyou
                    && (ConstClass.HAISHA_JOKYO_CD_KEIJO.Equals(haisyaJoukyou.ToString())
                        || ConstClass.HAISHA_JOKYO_CD_NASHI.Equals(haisyaJoukyou.ToString())))
                {
                    return true;
                }
            }

            // 修正前の要素がなければ変更有と判断
            if ((this.beforeListContenaParam == null
                || this.beforeListContenaParam.Count < 1)
                && (this.listContenaParam == null
                || this.listContenaParam.Count < 1))
            {
                // 両方空なら変更無と判断
                return false;
            }
            else if ((this.beforeListContenaParam == null || this.beforeListContenaParam.Count < 1)
                || (this.listContenaParam == null || this.listContenaParam.Count < 1))
            {
                // どちらか一方が空だった場合、以降の処理で不都合があるため、
                // このタイミングで変更有と判断
                return true;
            }

            // 要素数が同じ場合にチェック
            if (this.beforeListContenaParam.Count == this.listContenaParam.Count)
            {
                foreach (var contenaData in this.listContenaParam)
                {
                    bool isChengeData = true;

                    foreach (var beforeData in this.beforeListContenaParam)
                    {
                        if (beforeData.CONTENA_SHURUI_CD.Equals(contenaData.CONTENA_SHURUI_CD)
                            && beforeData.CONTENA_CD.Equals(contenaData.CONTENA_CD)
                            && beforeData.CONTENA_SET_KBN == contenaData.CONTENA_SET_KBN)
                        {
                            isChengeData = false;
                            break;
                        }
                    }

                    if (isChengeData)
                    {
                        // 変更を検知
                        returnVal = true;
                        break;
                    }
                }

                // コンテナ情報が変更されていなくても、日付が変わってたら変更有
                string beforeSagyouDate = Convert.ToString(this.dtUketsukeEntry.Rows[0]["SAGYOU_DATE"]);
                if (!string.IsNullOrEmpty(beforeSagyouDate)
                    && this.form.SAGYOU_DATE.Value != null
                    && (Convert.ToDateTime(beforeSagyouDate)).Date != ((DateTime)this.form.SAGYOU_DATE.Value).Date)
                {
                    return true;
                }
            }
            else
            {
                // 要素数が変わっている場合は変更有
                returnVal = true;
            }

            return returnVal;
        }

        /// <summary>
        /// 変更前と変更後のコンテナデータを比較して、削除されているコンテナデータのリストを返す
        /// </summary>
        /// <returns>true:変更有, false:変更無</returns>
        internal List<T_CONTENA_RESERVE> getDeleteContenaData()
        {
            List<T_CONTENA_RESERVE> returnVal = new List<T_CONTENA_RESERVE>();

            // 修正前の要素がなければ変更有と判断
            if ((this.beforeListContenaParam == null
                || this.beforeListContenaParam.Count < 1)
                && (this.listContenaParam == null
                || this.listContenaParam.Count < 1))
            {
                // 両方空なら変更無と判断
                return returnVal;
            }
            else if ((this.beforeListContenaParam == null || this.beforeListContenaParam.Count < 1)
                || (this.listContenaParam == null || this.listContenaParam.Count < 1))
            {
                // どちらか一方が空だった場合、以降の処理で不都合があるため、
                // このタイミングで変更有と判断
                return returnVal;
            }

            // 要素数が同じ場合にチェック
            foreach (var beforeData in this.beforeListContenaParam)
            {
                bool isDeleteDeta = true;

                foreach (var contenaData in this.listContenaParam)
                {
                    if (beforeData.CONTENA_SHURUI_CD.Equals(contenaData.CONTENA_SHURUI_CD)
                        && beforeData.CONTENA_CD.Equals(contenaData.CONTENA_CD)
                        && beforeData.CONTENA_SET_KBN == contenaData.CONTENA_SET_KBN)
                    {
                        isDeleteDeta = false;
                        break;
                    }
                }

                if (isDeleteDeta)
                {
                    returnVal.Add(beforeData);
                }
            }

            return returnVal;
        }

        /// <summary>
        /// 変更前と変更後のコンテナデータを比較して、動きのないコンテナデータのリストを返す
        /// </summary>
        /// <returns></returns>
        internal List<T_CONTENA_RESERVE> GetStayContenaData()
        {
            List<T_CONTENA_RESERVE> returnVal = new List<T_CONTENA_RESERVE>();

            // 修正前の要素がなければ画面で指定しているすべてが追加
            if ((this.beforeListContenaParam == null
                || this.beforeListContenaParam.Count < 1))
            {
                // 両方空なら変更無と判断
                return returnVal;
            }

            foreach (var contena in this.listContenaParam)
            {
                foreach (var beforeContena in this.beforeListContenaParam)
                {
                    if (beforeContena.CONTENA_SHURUI_CD.Equals(contena.CONTENA_SHURUI_CD)
                        && beforeContena.CONTENA_CD.Equals(contena.CONTENA_CD)
                        && beforeContena.CONTENA_SET_KBN == contena.CONTENA_SET_KBN)
                    {
                        returnVal.Add(contena);
                    }
                }
            }

            return returnVal;
        }

        // 20141104 Houkakou 委託契約チェック start
        #region 委託契約書チェック
        /// <summary>
        /// 委託契約書チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckItakukeiyaku()
        {
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                M_SYS_INFO sysInfo = new M_SYS_INFO();
                IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

                M_SYS_INFO[] sysInfos = sysInfoDao.GetAllData();
                if (sysInfos != null && sysInfos.Length > 0)
                {
                    sysInfo = sysInfos[0];
                }
                else
                {
                    return true;
                }

                //作業日を取得
                List<string> listDate = new List<string>();
                if (this.form.ListSagyouBi != null && this.form.ListSagyouBi.Count > 1)
                {
                    foreach (var item in this.form.ListSagyouBi)
                    {
                        listDate.Add(item);
                    }
                }
                else if (!String.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    listDate.Add(this.form.SAGYOU_DATE.Value.ToString());
                }

                //作業日が入力されない場合、チェックしない
                if (listDate.Count == 0)
                {
                    return true;
                }

                CustomAlphaNumTextBox txtGyoushaCd = this.form.GYOUSHA_CD;
                CustomAlphaNumTextBox txtGenbaCd = this.form.GENBA_CD;
                CustomDateTimePicker txtSagyouDate = null;
                // Control for set input error
                if (!String.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    txtSagyouDate = this.form.SAGYOU_DATE;
                }
                CustomDataGridView gridDetail = this.form.dgvDetail;
                string CTL_NAME_DETAIL = "HINMEI_CD";
                string CTL_NAME_DETAIL_NAME = "HINMEI_NAME";

                //委託契約チェックDtoを取得
                ItakuCheckDTO checkDto = new ItakuCheckDTO();
                checkDto.MANIFEST_FLG = false;
                checkDto.GYOUSHA_CD = txtGyoushaCd.Text;
                checkDto.GENBA_CD = txtGenbaCd.Text;
                // Set first 作業日 for check [itakuLogic.IsCheckItakuKeiyaku]
                checkDto.SAGYOU_DATE = listDate[0];
                checkDto.LIST_HINMEI_HAIKISHURUI = new List<DetailDTO>();

                foreach (DataGridViewRow row in gridDetail.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    DetailDTO detailDto = new DetailDTO();
                    detailDto.CD = Convert.ToString(row.Cells[CTL_NAME_DETAIL].Value);
                    detailDto.NAME = Convert.ToString(row.Cells[CTL_NAME_DETAIL_NAME].Value);
                    checkDto.LIST_HINMEI_HAIKISHURUI.Add(detailDto);
                }

                ItakuKeiyakuCheckLogic itakuLogic = new ItakuKeiyakuCheckLogic();
                bool isCheck = itakuLogic.IsCheckItakuKeiyaku(sysInfo, checkDto);
                //委託契約チェックを処理しない場合
                if (isCheck == false)
                {
                    return true;
                }

                foreach (var item in listDate)
                {
                    //作業日
                    checkDto.SAGYOU_DATE = item;

                    //委託契約チェック
                    ItakuErrorDTO error = itakuLogic.ItakuKeiyakuCheck(checkDto);

                    //エラーなし
                    if (error.ERROR_KBN == (short)ITAKU_ERROR_KBN.NONE)
                    {
                        continue;
                    }

                    bool ret = itakuLogic.ShowError(error, sysInfo.ITAKU_KEIYAKU_ALERT_AUTH, checkDto.MANIFEST_FLG, txtGyoushaCd, txtGenbaCd, txtSagyouDate, gridDetail, CTL_NAME_DETAIL);
                    return ret;
                }

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckItakukeiyaku", ex2);
                msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckItakukeiyaku", ex);
                msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion
        // 20141104 Houkakou 委託契約チェック start

        /// 20141009 Houkakou 「収集受付入力画面」の車輌休動を追加する　start
        #region 車輌休動チェック
        internal bool SharyouDateCheck()
        {
            try
            {
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.SAGYOU_DATE.Value);

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
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    this.form.SHARYOU_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E206", "車輌", "作業日：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 運転者休動チェック
        internal bool UntenshaDateCheck()
        {
            try
            {
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.SAGYOU_DATE.Value);

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
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E206", "運転者", "作業日：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 補助員休動チェック
        internal bool HojoinDateCheck()
        {
            try
            {
                string inputHojoinCd = this.form.HOJOIN_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.SAGYOU_DATE.Value);

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();

                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputHojoinCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workcloseduntenshaList.Count() >= 1)
                {
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.form.HOJOIN_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E206", "補助員", "作業日：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HojoinDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HojoinDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck()
        {
            try
            {
                string inputNioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                string inputNioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.SAGYOU_DATE.Value);

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
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    this.form.NIOROSHI_GENBA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E206", "荷降現場", "作業日：" + workclosedhannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion
        /// 20141009 Houkakou 「収集受付入力画面」の搬入先休動を追加する　end

        #region 登録時金額チェック

        /// <summary>
        /// 登録時に数量*単価が金額に一致するかの金額チェックを実行します
        /// </summary>
        /// <returns>True：正常　False：エラー</returns>
        internal bool CheckDetailKingaku()
        {
            decimal suuryou = 0;
            decimal tanka = 0;
            short kingakuHasuuCd = 0;

            foreach (DataGridViewRow row in this.form.dgvDetail.Rows)
            {
                if (row.IsNewRow) continue;

                // 端数取得
                kingakuHasuuCd = CalcHasuu(row);

                if (decimal.TryParse(Convert.ToString(row.Cells["SUURYOU"].FormattedValue), out suuryou)
                    && decimal.TryParse(Convert.ToString(row.Cells["TANKA"].FormattedValue), out tanka))
                {
                    // 数量と単価がNullではない場合、DataGridViewの金額と、数量*単価で計算した値が等しいかチェック
                    decimal kingaku = decimal.Parse(CommonCalc.DecimalFormat(CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd)));
                    decimal hinmeiKingaku = decimal.Parse(row.Cells["HINMEI_KINGAKU"].Value.ToString());

                    if (!hinmeiKingaku.Equals(kingaku))
                    {
                        // 一致しない場合はエラー
                        msgLogic.MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        //VUNGUYEN 20150717 #11589 検索での呼び出しに時間がかかる START
        public void getListTorihikisakiDefault()
        {
            var keyEntity = new M_TORIHIKISAKI();
            this.torihikisakiList = this.torihikisakiDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListGyoushaDefault()
        {
            var keyEntity = new M_GYOUSHA();
            this.gyoushaList = this.gyoushaDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListGenbaDefault()
        {
            var keyEntity = new M_GENBA();
            this.genbaList = this.genbaDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListManifestShuruiDefault()
        {
            var keyEntity = new M_MANIFEST_SHURUI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_SHURUIDao>();
            this.manifestShuruiList = dao.GetAllValidData(keyEntity).ToList();
        }

        public void getListManifestTeihaiDefault()
        {
            var keyEntity = new M_MANIFEST_TEHAI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_TEHAIDao>();
            this.manifestTehaiList = dao.GetAllValidData(keyEntity).ToList();
        }

        public bool getDefaultList()
        {
            bool ret = true;
            try
            {
                getListManifestShuruiDefault();
                getListManifestTeihaiDefault();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getDefaultList", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getDefaultList", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        //VUNGUYEN 20150717 #11589 検索での呼び出しに時間がかかる END

        #region 20150928 hoanghm #10907

        private bool CheckTorihikisakiShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                bool catchErr = true;
                var torihikisakiEntity = this.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != torihikisakiEntity)
                {
                    //this.form.TORIHIKISAKI_NAME.ReadOnly = !(bool)torihikisakiEntity.SHOKUCHI_KBN;
                    //this.form.TORIHIKISAKI_NAME.Tag = (bool)torihikisakiEntity.SHOKUCHI_KBN ? string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv(this.form.TORIHIKISAKI_NAME.MaxLength.ToString(), VbStrConv.Wide)) : string.Empty;
                    this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisakiEntity.SHOKUCHI_KBN);
                }
            }
            return true;
        }

        private bool CheckGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var gyoushaEntity = this.GetGyousha(this.form.GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != gyoushaEntity)
                {
                    //this.form.GYOUSHA_NAME.ReadOnly = !(bool)gyoushaEntity.SHOKUCHI_KBN;
                    //this.form.GYOUSHA_NAME.Tag = (bool)gyoushaEntity.SHOKUCHI_KBN ? string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv(this.form.GYOUSHA_NAME.MaxLength.ToString(), VbStrConv.Wide)) : string.Empty;
                    this.SetCtrlReadonly(this.form.GYOUSHA_NAME, !(bool)gyoushaEntity.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GYOSHA_TEL, !(bool)gyoushaEntity.SHOKUCHI_KBN);
                }
            }
            return true;
        }

        private bool CheckGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                bool catchErr = true;
                var genbaEntity = this.GetGenba(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != genbaEntity)
                {
                    //this.form.GENBA_NAME.ReadOnly = !(bool)genbaEntity.SHOKUCHI_KBN;
                    //this.form.GENBA_NAME.Tag = (bool)genbaEntity.SHOKUCHI_KBN ? string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv(this.form.GENBA_NAME.MaxLength.ToString(), VbStrConv.Wide)) : string.Empty;
                    this.SetCtrlReadonly(this.form.GENBA_NAME, !(bool)genbaEntity.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_ADDRESS1, !(bool)genbaEntity.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_ADDRESS2, !(bool)genbaEntity.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.GENBA_TEL, !(bool)genbaEntity.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.TANTOSHA_NAME, !(bool)genbaEntity.SHOKUCHI_KBN);
                    this.SetCtrlReadonly(this.form.TANTOSHA_TEL, !(bool)genbaEntity.SHOKUCHI_KBN);
                }
            }
            return true;
        }

        private bool CheckNioroshiGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var gyousha = this.GetGyousha(this.form.NIOROSHI_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (gyousha != null)
                {
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                    {
                        //this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = !(bool)gyousha.SHOKUCHI_KBN;
                        //this.form.NIOROSHI_GYOUSHA_NAME.Tag = (bool)gyousha.SHOKUCHI_KBN ? string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv(this.form.NIOROSHI_GYOUSHA_NAME.MaxLength.ToString(), VbStrConv.Wide)) : string.Empty;
                        this.SetCtrlReadonly(this.form.NIOROSHI_GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);
                    }
                }
            }
            return true;
        }

        private void CheckNioroshiGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                var genbaEntityList = this.GetGenbaList(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
                M_GENBA genba = new M_GENBA();

                if (genbaEntityList != null && genbaEntityList.Length > 0)
                {
                    genba = genbaEntityList[0];
                    if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    {
                        //this.form.NIOROSHI_GENBA_NAME.ReadOnly = !(bool)genba.SHOKUCHI_KBN;
                        //this.form.NIOROSHI_GENBA_NAME.Tag = (bool)genba.SHOKUCHI_KBN ? string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv(this.form.NIOROSHI_GENBA_NAME.MaxLength.ToString(), VbStrConv.Wide)) : string.Empty;
                        this.SetCtrlReadonly(this.form.NIOROSHI_GENBA_NAME, !(bool)genba.SHOKUCHI_KBN);
                    }
                }
            }
        }

        private bool CheckUpanGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var gyousha = this.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (gyousha != null)
                {
                    if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    {
                        //this.form.UNPAN_GYOUSHA_NAME.ReadOnly = !(bool)gyousha.SHOKUCHI_KBN;
                        //this.form.UNPAN_GYOUSHA_NAME.Tag = (bool)gyousha.SHOKUCHI_KBN ? string.Format(ConstClass.Hint.ZENKAKU, Strings.StrConv(this.form.UNPAN_GYOUSHA_NAME.MaxLength.ToString(), VbStrConv.Wide)) : string.Empty;
                        this.SetCtrlReadonly(this.form.UNPAN_GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);
                    }
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 現場CDが空の時の処理
        /// </summary>
        internal void GenbaCdEnptyProcess()
        {
            // 現場の関連情報をクリア
            this.form.GENBA_NAME.Text = String.Empty;
            this.form.GENBA_ADDRESS1.Text = String.Empty;
            this.form.GENBA_ADDRESS2.Text = String.Empty;
            this.form.GENBA_TEL.Text = String.Empty;
            this.form.TANTOSHA_NAME.Text = String.Empty;
            this.form.TANTOSHA_TEL.Text = String.Empty;
            this.form.UNTENSHA_SIJIJIKOU1.Text = String.Empty;
            this.form.UNTENSHA_SIJIJIKOU2.Text = String.Empty;
            this.form.UNTENSHA_SIJIJIKOU3.Text = String.Empty;
            this.form.GENBA_CD.IsInputErrorOccured = false;
            this.SetCtrlReadonly(this.form.GENBA_NAME, true);
            this.SetCtrlReadonly(this.form.GENBA_ADDRESS1, true);
            this.SetCtrlReadonly(this.form.GENBA_ADDRESS2, true);
            this.SetCtrlReadonly(this.form.GENBA_TEL, true);
            this.SetCtrlReadonly(this.form.TANTOSHA_NAME, true);
            this.SetCtrlReadonly(this.form.TANTOSHA_TEL, true);
            this.form.GENBA_CD.UpdateBackColor(false);
            this.isInputError = false;

            // 全ての明細と合計の計算
            this.CalcAllDetailAndTotal();

            if (this.pressedEnterOrTab)
            {
                if (this.form.Key.Shift)
                {
                    this.form.SetFocusControl(this.form.GYOUSHA_CD);
                }
                else
                {
                    this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                }
                //フォーカス設定処理
                this.form.boolMoveFocusControl();
            }

            // 営業担当者を設定
            this.SetEigyouTantousha();
        }

        #region 連携チェック
        internal bool RenkeiCheck(string uketsukeNum)
        {
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData("1", uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.msgLogic.MessageBoxShow("E261","現在回収中","編集");
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
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.msgLogic.MessageBoxShow("E261", "ロジこんぱす連携中", "編集");
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RenkeiCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RenkeiCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region Communicate InxsSubApplication

        private void ResetButtonRequestStatusInxs()
        {
            this.dtPickupRequestInxs = null;
            this.form.REQUEST_INXS_BUTTON.Text = string.Empty;
            this.form.REQUEST_INXS_BUTTON.BackColor = Color.Empty;
            this.form.REQUEST_INXS_BUTTON.ForeColor = Color.Empty;
            this.form.REQUEST_INXS_BUTTON.Visible = false;
        }

        internal void SetButtonRequestStatusInxs()
        {
            RequestStatusInxsDto requestStatusInfo = null;
            if (this.dtPickupRequestInxs != null && this.dtPickupRequestInxs.Rows.Count > 0)
            {
                InxsClass inxsCls = new InxsClass();
                requestStatusInfo = inxsCls.GetRequestStatusInfo(Convert.ToInt32(this.dtPickupRequestInxs.Rows[0]["REQUEST_STATUS"]));
            }

            if (requestStatusInfo != null)
            {
                this.form.REQUEST_INXS_BUTTON.Text = requestStatusInfo.DisplayText;
                this.form.REQUEST_INXS_BUTTON.BackColor = requestStatusInfo.BackColor;
                this.form.REQUEST_INXS_BUTTON.ForeColor = requestStatusInfo.ForeColor;
                this.form.REQUEST_INXS_BUTTON.Visible = true;
            }
        }

        internal void CompareSagyouDateAndConfirmDate()
        {
            try
            {
                if (!r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                {
                    return;
                }
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                    && !string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text)
                    && this.dtPickupRequestInxs != null && this.dtPickupRequestInxs.Rows.Count > 0
                    && this.ChgDBNullToValue(this.dtPickupRequestInxs.Rows[0]["REQUEST_STATUS"], string.Empty).ToString() == CommonConst.RequestStatusInxs.CONFIRMED_VALUE.ToString())
                {
                    var sagyuoDate = new DateTime();
                    var kakuteiDate = new DateTime();
                    if (DateTime.TryParse(this.form.SAGYOU_DATE.Text, out sagyuoDate)
                        && DateTime.TryParse(this.ChgDBNullToValue(this.dtPickupRequestInxs.Rows[0]["CONFIRM_DATE"], string.Empty).ToString(), out kakuteiDate)
                        && DateTime.Compare(sagyuoDate, kakuteiDate) != 0)
                    {
                        this.msgLogic.MessageBoxShow("W011", "収集");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }

        internal void GetInxsRequestData()
        {
            try
            {
                if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke() && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    DTOClass searchDto = new DTOClass()
                    {
                        SystemID = (long)this.dtUketsukeEntry.Rows[0]["SYSTEM_ID"]
                    };
                    this.dtPickupRequestInxs = this.daoUketsukeEntry.GetPickupRequestInxsData(searchDto);
                }
                else
                {
                    this.dtPickupRequestInxs = null;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }

        #endregion

        #region mapbox連携

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        internal List<mapDtoList> createMapboxDto()
        {
            try
            {
                string gyoushaCd = string.Empty;
                string genbaCd = string.Empty;

                List<mapDtoList> dtoLists = new List<mapDtoList>();

                // レイヤー追加
                mapDtoList dtoList = new mapDtoList();
                dtoList.layerId = 1;


                List<mapDto> dtos = new List<mapDto>();

                DataTable dt = null;

                // 地図出力に必要な情報を収集
                gyoushaCd = this.form.GYOUSHA_CD.Text;
                genbaCd = this.form.GENBA_CD.Text;

                string sql = string.Empty;
                StringBuilder sb = new StringBuilder();

                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    // 業者のみの場合
                    sb.Append(" SELECT ");
                    sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstClass.GYOUSHA_CD);
                    sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstClass.GYOUSHA_NAME_RYAKU);
                    sb.AppendFormat(",'' AS {0} ", ConstClass.GENBA_CD);
                    sb.AppendFormat(",'' AS {0} ", ConstClass.GENBA_NAME_RYAKU);
                    sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstClass.TODOUFUKEN_NAME);
                    sb.AppendFormat(",GYO.GYOUSHA_ADDRESS1 AS {0} ", ConstClass.ADDRESS1);
                    sb.AppendFormat(",GYO.GYOUSHA_ADDRESS2 AS {0} ", ConstClass.ADDRESS2);
                    sb.AppendFormat(",GYO.GYOUSHA_LATITUDE AS {0} ", ConstClass.LATITUDE);
                    sb.AppendFormat(",GYO.GYOUSHA_LONGITUDE AS {0} ", ConstClass.LONGITUDE);
                    sb.AppendFormat(",GYO.GYOUSHA_POST AS {0} ", ConstClass.POST);
                    sb.AppendFormat(",GYO.GYOUSHA_TEL AS {0} ", ConstClass.TEL);
                    sb.AppendFormat(",GYO.BIKOU1 AS {0} ", ConstClass.BIKOU1);
                    sb.AppendFormat(",GYO.BIKOU2 AS {0} ", ConstClass.BIKOU2);
                    sb.Append(" FROM M_GYOUSHA AS GYO ");
                    sb.Append(" LEFT JOIN M_TODOUFUKEN TDF ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.Append(" WHERE GYO.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND GYO.GYOUSHA_CD = '{0}'", gyoushaCd);
                }
                else
                { 
                    // 現場ありの場合
                    sb.Append(" SELECT ");
                    sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstClass.GYOUSHA_CD);
                    sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstClass.GYOUSHA_NAME_RYAKU);
                    sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstClass.GENBA_CD);
                    sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstClass.GENBA_NAME_RYAKU);
                    sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstClass.TODOUFUKEN_NAME);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", ConstClass.ADDRESS1);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", ConstClass.ADDRESS2);
                    sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstClass.LATITUDE);
                    sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstClass.LONGITUDE);
                    sb.AppendFormat(",GEN.GENBA_POST AS {0} ", ConstClass.POST);
                    sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", ConstClass.TEL);
                    sb.AppendFormat(",GEN.BIKOU1 AS {0} ", ConstClass.BIKOU1);
                    sb.AppendFormat(",GEN.BIKOU2 AS {0} ", ConstClass.BIKOU2);
                    sb.Append(" FROM M_GYOUSHA AS GYO ");
                    sb.Append(" LEFT JOIN M_GENBA GEN ON GYO.GYOUSHA_CD = GEN.GYOUSHA_CD ");
                    sb.Append(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.Append(" WHERE GYO.DELETE_FLG = 0 ");
                    sb.Append(" AND GEN.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND GEN.GYOUSHA_CD = '{0}'", gyoushaCd);
                    sb.AppendFormat(" AND GEN.GENBA_CD = '{0}'", genbaCd);
                }


                dt = this.genbaDao.GetDateForStringSql(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        // 緯度経度なしデータを排除する
                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][ConstClass.LATITUDE]))) continue;

                        mapDto dto = new mapDto();
                        dto.id = 1;
                        dto.layerNo = 1;
                        dto.torihikisakiCd = string.Empty;
                        dto.torihikisakiCd = string.Empty;
                        dto.torihikisakiName = string.Empty;
                        dto.gyoushaCd = Convert.ToString(dt.Rows[j][ConstClass.GYOUSHA_CD]);
                        dto.gyoushaName = this.form.GYOUSHA_NAME.Text;
                        dto.genbaCd = Convert.ToString(dt.Rows[j][ConstClass.GENBA_CD]);
                        dto.genbaName = this.form.GENBA_NAME.Text;
                        dto.post = Convert.ToString(dt.Rows[j][ConstClass.POST]);
                        dto.address = Convert.ToString(dt.Rows[j][ConstClass.TODOUFUKEN_NAME]) + Convert.ToString(dt.Rows[j][ConstClass.ADDRESS1]) + Convert.ToString(dt.Rows[j][ConstClass.ADDRESS2]);
                        dto.tel = Convert.ToString(dt.Rows[j][ConstClass.TEL]);
                        dto.bikou1 = Convert.ToString(dt.Rows[j][ConstClass.BIKOU1]);
                        dto.bikou2 = Convert.ToString(dt.Rows[j][ConstClass.BIKOU2]);
                        dto.latitude = Convert.ToString(dt.Rows[j][ConstClass.LATITUDE]);
                        dto.longitude = Convert.ToString(dt.Rows[j][ConstClass.LONGITUDE]);
                        dto.sagyouDate = string.Empty;
                        dto.genbaChaku = string.Empty;
                        dto.shasyu = string.Empty;
                        dto.sharyou = string.Empty;
                        dto.driver = string.Empty;
                        dto.sijijikou1 = string.Empty;
                        dto.sijijikou2 = string.Empty;
                        dto.sijijikou3 = string.Empty;
                        dtos.Add(dto);
                    }
                }

                // 1コース終わったらリストにセット
                dtoList.dtos = dtos;

                dtoLists.Add(dtoList);

                return dtoLists;
            }
            catch (Exception ex)
            {
                this.msgLogic.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 業者の緯度経度チェック
        /// </summary>
        /// <returns></returns>
        internal bool gyoushaLocationChk()
        {
            M_GYOUSHA dto = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
            if (string.IsNullOrEmpty(dto.GYOUSHA_LATITUDE))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 現場の緯度経度チェック
        /// </summary>
        /// <returns></returns>
        internal bool genbaLocationChk()
        {
            M_GENBA genba = new M_GENBA();
            genba.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            genba.GENBA_CD = this.form.GENBA_CD.Text;

            M_GENBA dto = this.genbaDao.GetDataByCd(genba);
            if (string.IsNullOrEmpty(dto.GENBA_LATITUDE))
            {
                return false;
            }
            return true;
        }

        #endregion

        /////////////////////////////////////
        //モバイル連携
        /////////////////////////////////////
        #region モバイル登録
        /// <summary>
        /// モバイル登録
        /// </summary>
        /// <returns></returns>       
        [Transaction]
        public bool MobileRegist()
        {
            bool MobileRegistC = false;

            //リトライは5回まで
            for (MobileTryTime = 1; MobileTryTime <= 5; MobileTryTime++)
            {
                try
                {
                    if (!this.CreateMobileEntity())
                    {
                        //エラーならリトライ
                        continue;
                    }
                    using (Transaction tran = new Transaction())
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
                        // トランザクション終了
                        tran.Commit();
                    }
                    return true;
                }
                catch (NotSingleRowUpdatedRuntimeException ex1)
                {
                    //該当データは他ユーザーにより更新されています
                    LogUtility.Error("MobileRegist", ex1);
                }
                catch (SQLRuntimeException ex2)
                {
                    //データの登録または検索に失敗しました
                    LogUtility.Error("MobileRegist", ex2);
                }
                catch (Exception ex)
                {
                    //予期しないエラーが発生しました。
                    LogUtility.Error("MobileRegist", ex);
                }
            }
            return MobileRegistC;
        }
        #endregion モバイル登録

        #region モバイル登録チェック
        /// <summary>
        /// モバイル登録チェック
        /// ※モバイルオプションかつ、作業日＝当日が前提条件
        /// 　かつ、モバイル連携可能条件を満たしている事
        /// </summary>
        /// <returns></returns>
        public bool MobileRegistCheck()
        {
            bool mobileRegist = false;

            bool is_mobile = r_framework.Configuration.AppConfig.AppOptions.IsMobile();
            if (is_mobile)
            {
                this.dto = new DTOClass();
                this.dto.Renkei_UketsukeNumber = this.Renkei_UketsukeNumber;
                this.dto.SAGYOU_DATE_FROM = DateTime.Now.ToString("yyyy/MM/dd");
                this.dto.SAGYOU_DATE_TO = DateTime.Now.ToString("yyyy/MM/dd");
                this.ResultTable = new DataTable();
                this.ResultTable = this.daoUketsukeEntry.GetDataToMRDataTable(this.dto);
                if (0 < this.ResultTable.Rows.Count)
                {
                    // 現場CDが未入力の場合、モバイル連携を行わない。
                    if (this.ResultTable.Rows[0]["GENBA_CD"] == null || string.IsNullOrEmpty(this.ResultTable.Rows[0]["GENBA_CD"].ToString()))
                    {
                        return mobileRegist;
                    }

                    DialogResult dr = new DialogResult();
                    if ((this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG)) && (this.form.SHASHU_DAISU_NUMBER.Text != "1"))
                    {
                        dr = MessageBox.Show("車種台数分を全てモバイル将軍へ連携しますか？\r\n\r\n"
                                                                        + "はい　⇒　全て連携する\r\n\r\n"
                                                                        + "いいえ　⇒　当該データのみ連携する\r\n\r\n"
                                                                        + "キャンセル　⇒　全て連携しない", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dr != DialogResult.Cancel)
                        {
                            mobileRegist = true;
                            //1伝票以外削除
                            if (dr == DialogResult.No)
                            {
                                string Renkei_Bunkatu = this.Renkei_UketsukeNumber.Substring(0, this.Renkei_UketsukeNumber.IndexOf(","));
                                foreach (DataRow dtlRow in ResultTable.Rows)
                                {
                                    if (dtlRow["UKETSUKE_NUMBER"].ToString() != Renkei_Bunkatu)
                                    {
                                        dtlRow.Delete();
                                    }
                                }
                                ResultTable.AcceptChanges();//更新を確定
                            }
                        }
                    }
                    else
                    {
                        dr = this.msgLogic.MessageBoxShow("C110");
                        if (dr == DialogResult.OK || dr == DialogResult.Yes)
                        {
                            mobileRegist = true;
                        }
                    }
                }
            }

            return mobileRegist;
        }
        #endregion モバイル登録チェック

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
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode();

            // IS_NUMBER_SYSTEMDao(共通)
            IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            var updateEntity = numberSystemDao.GetNumberSystemData(entity);
            returnInt = numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode();
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
                this.entitysMobisyoRtCTNList = new List<T_MOBISYO_RT_CONTENA>();
                this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();

                // 持ってきたデータ
                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    #region T_MOBISYO_RT
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
                    this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = true;
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

                    this.entitysMobisyoRt.GENCHAKU_TIME_NAME = tableRow["GENCHAKU_TIME_NAME"].ToString();

                    if (!String.IsNullOrEmpty(tableRow["GENCHAKU_TIME"].ToString()))
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

                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(this.entitysMobisyoRt);
                    dataBinderContenaResult.SetSystemProperty(this.entitysMobisyoRt, false);

                    // Listに追加
                    this.entitysMobisyoRtList.Add(this.entitysMobisyoRt);
                    #endregion

                    #region T_MOBISYO_RT_CONTENA

                    SqlInt64 SYSTEM_ID = SqlInt64.Parse(tableRow["SYSTEM_ID"].ToString());
                    SqlInt32 SEQ = SqlInt32.Parse(tableRow["SEQ"].ToString());

                    DataTable ContenaData = this.daoContena.GetSyuusyuuUketukeContenaData(SYSTEM_ID, SEQ);
                    foreach (DataRow ctnRow in ContenaData.Rows)
                    {
                        this.entitysMobisyoRtCTN = new T_MOBISYO_RT_CONTENA();
                        // シーケンシャルナンバー
                        this.entitysMobisyoRtCTN.SEQ_NO = this.entitysMobisyoRt.SEQ_NO;
                        // コンテナシーケンシャルナンバー 伝種区分125で最新のID + 1を採番
                        this.entitysMobisyoRtCTN.CONTENA_SEQ_NO = this.CreateMobileSeqNo();
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
                    #endregion

                    SqlInt64 HANYU_SEQ_NO = -1;
                    DataTable SyuusyuuUketukeDetailData = this.daoUketsukeDetail.GetSyuusyuuUketukeDetailData(SYSTEM_ID, SEQ);
                    if (!string.IsNullOrEmpty(SyuusyuuUketukeDetailData.Rows[0]["HANNYUU_GENBACD"].ToString()))
                    {
                        HANYU_SEQ_NO = this.CreateMobileSeqNo();
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
                    }
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

        #region CongBinh 20210713 #152801
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bikouShow"></param>
        /// <param name="isSharyou"></param>
        public void ChangeRirekiTemplate(bool bikouShow, bool isSharyou = false)
        {
            this.form.rirekeIchiran.SuspendLayout();
            var newTemplate = this.form.rirekeIchiran.Template;
            if (isSharyou)
            {
                this.form.rirekeIchiran.Template.Height = 77;
                this.form.rirekeIchiran.Template.Row.Height = 77;
            }
            else
            {
                if (bikouShow)
                {
                    this.form.rirekeIchiran.Template.Height = 133;
                    this.form.rirekeIchiran.Template.Row.Height = 133;
                }
                else
                {
                    this.form.rirekeIchiran.Template.Height = 49;
                    this.form.rirekeIchiran.Template.Row.Height = 49;
                }
            }
            this.form.rirekeIchiran.Template = newTemplate;
            this.form.rirekeIchiran.ResumeLayout();
            this.form.rirekeIchiran.Refresh();
        }
        /// <summary>
        /// 
        /// </summary>
        internal void RirekeShow()
        {
            this.ChangeRirekiTemplate(true);
            M_TORIHIKISAKI torihikisakiData = this.torihikisakiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
            M_GYOUSHA gyoushaData = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
            M_GENBA genbaData = this.genbaDao.GetDataByCd(new M_GENBA() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text });
            int index = 0;
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                this.RirekeTorihikisakiShow(torihikisakiData, ref index);
                this.RirekeGyoushaShow(gyoushaData, ref index);
                this.RirekeGenbaShow(genbaData, ref index);
                this.form.rirekeIchiran.EndEdit();
                this.form.rirekeIchiran.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.rirekeIchiran);
            }
            else if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                this.RirekeTorihikisakiShow(torihikisakiData, ref index);
                this.RirekeGyoushaShow(gyoushaData, ref index);
                this.form.rirekeIchiran.EndEdit();
                this.form.rirekeIchiran.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.rirekeIchiran);
            }
            else if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                this.RirekeGyoushaShow(gyoushaData, ref index);
                this.RirekeGenbaShow(genbaData, ref index);
                this.form.rirekeIchiran.EndEdit();
                this.form.rirekeIchiran.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.rirekeIchiran);
            }
            else if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                this.RirekeGyoushaShow(gyoushaData, ref index);
                this.form.rirekeIchiran.EndEdit();
                this.form.rirekeIchiran.NotifyCurrentCellDirty(false);
                SelectionActions.MoveToFirstCell.Execute(this.form.rirekeIchiran);
            }
            else
            {
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="torihikisakiData"></param>
        /// <param name="index"></param>
        private void RirekeTorihikisakiShow(M_TORIHIKISAKI torihikisakiData, ref int index)
        {
            if (torihikisakiData != null)
            {
                if (!string.IsNullOrEmpty(torihikisakiData.BIKOU1) || !string.IsNullOrEmpty(torihikisakiData.BIKOU2)
                    || !string.IsNullOrEmpty(torihikisakiData.BIKOU3) || !string.IsNullOrEmpty(torihikisakiData.BIKOU4))
                {
                    this.form.rirekeIchiran.Rows.Add();
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SS].Value = "取引先備考";
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA1].Value = StringExtension.SubStringByByte(torihikisakiData.BIKOU1, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA2].Value = StringExtension.SubStringByByte(torihikisakiData.BIKOU2, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA3].Value = StringExtension.SubStringByByte(torihikisakiData.BIKOU3, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA4].Value = StringExtension.SubStringByByte(torihikisakiData.BIKOU4, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKI_KBN].Value = "1";
                    index++;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gyoushaData"></param>
        /// <param name="index"></param>
        private void RirekeGyoushaShow(M_GYOUSHA gyoushaData, ref int index)
        {
            if (gyoushaData != null)
            {
                if (!string.IsNullOrEmpty(gyoushaData.BIKOU1) || !string.IsNullOrEmpty(gyoushaData.BIKOU2)
                    || !string.IsNullOrEmpty(gyoushaData.BIKOU3) || !string.IsNullOrEmpty(gyoushaData.BIKOU4))
                {
                    this.form.rirekeIchiran.Rows.Add();
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SS].Value = "業者備考";
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA1].Value = StringExtension.SubStringByByte(gyoushaData.BIKOU1, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA2].Value = StringExtension.SubStringByByte(gyoushaData.BIKOU2, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA3].Value = StringExtension.SubStringByByte(gyoushaData.BIKOU3, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA4].Value = StringExtension.SubStringByByte(gyoushaData.BIKOU4, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKI_KBN].Value = "1";
                    index++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genbaData"></param>
        /// <param name="index"></param>
        private void RirekeGenbaShow(M_GENBA genbaData, ref int index)
        {
            if (genbaData != null)
            {
                if (!string.IsNullOrEmpty(genbaData.BIKOU1) || !string.IsNullOrEmpty(genbaData.BIKOU2)
                    || !string.IsNullOrEmpty(genbaData.BIKOU3) || !string.IsNullOrEmpty(genbaData.BIKOU4))
                {
                    this.form.rirekeIchiran.Rows.Add();
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SS].Value = "現場備考";
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA1].Value = StringExtension.SubStringByByte(genbaData.BIKOU1, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA2].Value = StringExtension.SubStringByByte(genbaData.BIKOU2, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA3].Value = StringExtension.SubStringByByte(genbaData.BIKOU3, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKE_SHITA4].Value = StringExtension.SubStringByByte(genbaData.BIKOU4, 22, 2);
                    this.form.rirekeIchiran.Rows[index].Cells[ConstClass.CELL_NAME_RIREKI_KBN].Value = "1";
                    index++;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void RirekeSharyouShow()
        {
            if (!(string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text) ||
                string.IsNullOrEmpty(this.form.SHARYOU_CD.Text) ||
                string.IsNullOrEmpty(this.form.SHASHU_CD.Text)))
            {
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                return;
            }
            this.ChangeRirekiTemplate(false, true);
            string dateFrom = string.Empty;
            string dateTo = string.Empty;
            if (this.form.ListSagyouBi.Count > 1)
            {
                dateFrom = DateTime.Parse(this.form.ListSagyouBi[this.form.ListSagyouBi.Count - 1]).AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Parse(this.form.ListSagyouBi[this.form.ListSagyouBi.Count - 1]).ToString("yyyy/MM/dd");
            }
            else if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                dateFrom = DateTime.Parse(this.form.SAGYOU_DATE.Text).AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Parse(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd");
            }
            else if (!string.IsNullOrEmpty(this.form.UKETSUKE_DATE.Text))
            {
                dateFrom = DateTime.Parse(this.form.UKETSUKE_DATE.Text).AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Parse(this.form.UKETSUKE_DATE.Text).ToString("yyyy/MM/dd");
            }
            else
            {
                dateFrom = DateTime.Now.AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Now.ToString("yyyy/MM/dd");
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) &&
                (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text) ||
                string.IsNullOrEmpty(this.form.SHARYOU_CD.Text) ||
                string.IsNullOrEmpty(this.form.SHASHU_CD.Text)))
            {
                string unpanGyoushaCd = null;
                string sharyouCd = null;
                string shashuCd = null;
                string genbaCd = null;
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    unpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    sharyouCd = this.form.SHARYOU_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    shashuCd = this.form.SHASHU_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    genbaCd = this.form.GENBA_CD.Text;
                }
                var sharyouData = this.daoUketsukeEntry.GetSharyou3MonthData(dateFrom, dateTo, this.form.GYOUSHA_CD.Text, genbaCd, unpanGyoushaCd, sharyouCd, shashuCd);
                if (sharyouData != null)
                {
                    this.form.rirekeIchiran.BeginEdit(false);
                    while (this.form.rirekeIchiran.Rows.Count != 0)
                    {
                        this.form.rirekeIchiran.Rows.RemoveAt(0);
                    }
                    for (int i = 0; i < sharyouData.Rows.Count; i++)
                    {
                        this.form.rirekeIchiran.Rows.Add();
                        this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SS].Value = "車輌情報";
                        this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SHITA1].Value = StringExtension.SubStringByByte(sharyouData.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString(), 22, 2);
                        if (!string.IsNullOrEmpty(sharyouData.Rows[i]["SHASHU_NAME"].ToString()))
                        {
                            this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SHITA2].Value = StringExtension.SubStringByByte(sharyouData.Rows[i]["SHASHU_NAME"].ToString(), 22) + "\r\n" +
                                                                                                              StringExtension.SubStringByByte(sharyouData.Rows[i]["SHARYOU_NAME"].ToString(), 22);
                        }
                        else
                        {
                            this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SHITA2].Value = StringExtension.SubStringByByte(sharyouData.Rows[i]["SHARYOU_NAME"].ToString(), 22, 2);
                        }
                        this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKI_KBN].Value = "2";
                        this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SHITA3].Value = sharyouData.Rows[i]["TMP"];
                    }
                    this.form.rirekeIchiran.EndEdit();
                    this.form.rirekeIchiran.NotifyCurrentCellDirty(false);
                    SelectionActions.MoveToFirstCell.Execute(this.form.rirekeIchiran);
                }
                else
                {
                    this.form.rirekeIchiran.BeginEdit(false);
                    while (this.form.rirekeIchiran.Rows.Count != 0)
                    {
                        this.form.rirekeIchiran.Rows.RemoveAt(0);
                    }
                    this.form.rirekeIchiran.EndEdit();
                }
            }
            else
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                this.form.rirekeIchiran.EndEdit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void RirekeHinmeiShow()
        {
            this.ChangeRirekiTemplate(false);
            string dateFrom = string.Empty;
            string dateTo = string.Empty;
            if (this.form.ListSagyouBi.Count > 1)
            {
                dateFrom = DateTime.Parse(this.form.ListSagyouBi[this.form.ListSagyouBi.Count - 1]).AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Parse(this.form.ListSagyouBi[this.form.ListSagyouBi.Count - 1]).ToString("yyyy/MM/dd");
            }
            else if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                dateFrom = DateTime.Parse(this.form.SAGYOU_DATE.Text).AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Parse(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd");
            }
            else if (!string.IsNullOrEmpty(this.form.UKETSUKE_DATE.Text))
            {
                dateFrom = DateTime.Parse(this.form.UKETSUKE_DATE.Text).AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Parse(this.form.UKETSUKE_DATE.Text).ToString("yyyy/MM/dd");
            }
            else
            {
                dateFrom = DateTime.Now.AddMonths(-3).ToString("yyyy/MM/dd");
                dateTo = DateTime.Now.ToString("yyyy/MM/dd");
            }
            string torihikisakiCd = null;
            string gyoushaCd = null;
            string genbaCd = null;
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                gyoushaCd = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                genbaCd = this.form.GENBA_CD.Text;
            }
            DataTable hinmeiData = this.daoUketsukeEntry.GetHinmei3MonthData(dateFrom, dateTo, torihikisakiCd, gyoushaCd, genbaCd);
            if (hinmeiData != null)
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                for (int i = 0; i < hinmeiData.Rows.Count; i++)
                {
                    this.form.rirekeIchiran.Rows.Add();
                    this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SS].Value = hinmeiData.Rows[i]["HINMEI_CD"];
                    this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKE_SHITA1].Value = StringExtension.SubStringByByte(hinmeiData.Rows[i]["HINMEI_NAME"].ToString(), 22, 2);
                    this.form.rirekeIchiran.Rows[i].Cells[ConstClass.CELL_NAME_RIREKI_KBN].Value = "3";
                }
                this.form.rirekeIchiran.EndEdit();
                this.form.rirekeIchiran.NotifyCurrentCellDirty(false);
                //SelectionActions.MoveToFirstCell.Execute(this.form.rirekeIchiran);
            }
            else
            {
                this.form.rirekeIchiran.BeginEdit(false);
                while (this.form.rirekeIchiran.Rows.Count != 0)
                {
                    this.form.rirekeIchiran.Rows.RemoveAt(0);
                }
                this.form.rirekeIchiran.EndEdit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void RirekeSet()
        {
            Row row = this.form.rirekeIchiran.CurrentRow;
            if (row == null)
            {
                return;
            }
            if (row[ConstClass.CELL_NAME_RIREKI_KBN].Value != null && row[ConstClass.CELL_NAME_RIREKI_KBN].Value.Equals("2"))
            {
                string[] rirekiSharyou = row[ConstClass.CELL_NAME_RIREKE_SHITA3].Value.ToString().Split('：');
                if (!string.IsNullOrEmpty(rirekiSharyou[0]))
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = rirekiSharyou[0];
                    this.form.UNPAN_GYOUSHA_NAME.Text = rirekiSharyou[1];
                    if (!this.CheckUnpanGyoushaCd())
                    {
                        this.form.UNPAN_GYOUSHA_CD.Focus();
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(rirekiSharyou[2]))
                {
                    this.form.SHASHU_CD.Text = rirekiSharyou[2];
                    this.form.SHASHU_NAME.Text = rirekiSharyou[3];
                }
                if (!string.IsNullOrEmpty(rirekiSharyou[4]))
                {
                    this.form.SHARYOU_CD.Text = rirekiSharyou[4];
                    this.form.SHARYOU_NAME.Text = rirekiSharyou[5];
                    if (!this.CheckSharyou(true))
                    {
                        this.form.SHARYOU_CD.Focus();
                        return;
                    }
                }
                this.form.SHARYOU_CD.Focus();
            }
            else if (row[ConstClass.CELL_NAME_RIREKI_KBN].Value != null && row[ConstClass.CELL_NAME_RIREKI_KBN].Value.Equals("3"))
            {
                M_HINMEI hinmeiTmp = null;
                if (row[ConstClass.CELL_NAME_RIREKE_SS].Value != null && !string.IsNullOrEmpty(row[ConstClass.CELL_NAME_RIREKE_SS].Value.ToString()))
                {
                    string[] rirekiSS = row[ConstClass.CELL_NAME_RIREKE_SS].Value.ToString().Split('：');
                    hinmeiTmp = this.GetAllValidHinmeiData(rirekiSS[1]).FirstOrDefault();
                    if (hinmeiTmp == null || (hinmeiTmp != null && hinmeiTmp.DENSHU_KBN_CD == ConstClass.DENSHU_KBN_CD_SHUKKA))
                    {
                        this.msgLogic.MessageBoxShow("E020", "品名");
                        return;
                    }
                }
                this.dgvDetail.CellEnter -= this.form.dgvDetail_OnCellEnter;
                var rowIndex = this.form.dgvDetail.RowCount - 1;
                this.form.dgvDetail.Rows.Insert(rowIndex, 1);
                DataGridViewRow rowTmp = this.form.dgvDetail.Rows[rowIndex];
                if (row[ConstClass.CELL_NAME_RIREKE_SS].Value != null && !string.IsNullOrEmpty(row[ConstClass.CELL_NAME_RIREKE_SS].Value.ToString()))
                {
                    string[] rirekiSS = row[ConstClass.CELL_NAME_RIREKE_SS].Value.ToString().Split('：');
                    rowTmp.Cells["HINMEI_CD"].Value = rirekiSS[1];
                    rowTmp.Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                    rowTmp.Cells["DENPYOU_KBN_NAME_RYAKU"].Value = string.Empty;
                }
                if (row[ConstClass.CELL_NAME_RIREKE_SHITA1].Value != null && !string.IsNullOrEmpty(row[ConstClass.CELL_NAME_RIREKE_SHITA1].Value.ToString()))
                {
                    rowTmp.Cells["HINMEI_NAME"].Value = row[ConstClass.CELL_NAME_RIREKE_SHITA1].Value.ToString();
                }
                this.dgvDetail.CellEnter += this.form.dgvDetail_OnCellEnter;
                this.dgvDetail.CurrentCell = rowTmp.Cells["HINMEI_CD"];
                this.dgvDetail.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sagyoubi"></param>
        /// <returns></returns>
        internal bool CheckKyuuJitsu(DateTime sagyoubi)
        {
            if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT * ");
                sql.AppendLine(" FROM M_WORK_CLOSED_HANNYUUSAKI ");
                sql.AppendLine(" WHERE DELETE_FLG = 0 ");
                sql.AppendLine(" AND CLOSED_DATE = '" + sagyoubi + "'");
                sql.AppendLine(" AND GYOUSHA_CD = '" + this.form.NIOROSHI_GYOUSHA_CD.Text + "'");
                sql.AppendLine(" AND GENBA_CD = '" + this.form.NIOROSHI_GENBA_CD.Text + "'");
                var dtTmp = this.gyoushaDao.GetDateForStringSql(sql.ToString());
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShowError("荷降現場が休日設定されているため、選択できません。（作業日：" + sagyoubi.ToString("yyyy年MM月dd日") + "）");
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text) && !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT * ");
                sql.AppendLine(" FROM M_WORK_CLOSED_SHARYOU ");
                sql.AppendLine(" WHERE DELETE_FLG = 0 ");
                sql.AppendLine(" AND CLOSED_DATE = '" + sagyoubi + "'");
                sql.AppendLine(" AND GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "'");
                sql.AppendLine(" AND SHARYOU_CD = '" + this.form.SHARYOU_CD.Text + "'");
                var dtTmp = this.gyoushaDao.GetDateForStringSql(sql.ToString());
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShowError("車輌が休日設定されているため、選択できません。（作業日：" + sagyoubi.ToString("yyyy年MM月dd日") + "）");
                    return true;
                }
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT * ");
                sql.AppendLine(" FROM M_WORK_CLOSED_UNTENSHA ");
                sql.AppendLine(" WHERE DELETE_FLG = 0 ");
                sql.AppendLine(" AND CLOSED_DATE = '" + sagyoubi + "'");
                sql.AppendLine(" AND SHAIN_CD = '" + this.form.UNTENSHA_CD.Text + "'");
                var dtTmp = this.gyoushaDao.GetDateForStringSql(sql.ToString());
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.msgLogic.MessageBoxShowError("運転者が休日設定されているため、選択できません。（作業日：" + sagyoubi.ToString("yyyy年MM月dd日") + "）");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool CheckListSagyouBi()
        {
            if (this.form.ListSagyouBi != null && this.form.ListSagyouBi.Count > 1)
            {
                foreach (var item in this.form.ListSagyouBi)
                {
                    if (this.CheckKyuuJitsu(DateTime.Parse(item)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        // MAILAN #158988 START
        internal void ResetTankaCheck()
        {
            this.isTankaMessageShown = false;
            this.isContinueCheck = true;
        }
        // MAILAN #158988 END

        #region 画面モードチェック（ｼｮｰﾄﾒｯｾｰｼﾞ）
        /// <summary>
        /// 画面モードチェック（ｼｮｰﾄﾒｯｾｰｼﾞ）
        /// </summary>
        /// <returns>現在の画面モード</returns>
        public void CheckWindowTypeSms(WINDOW_TYPE windowType)
        {
            if (!AppConfig.AppOptions.IsSMS())
            {
                // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン無効化
                parentForm.bt_process2.Text = string.Empty;
                parentForm.bt_process2.Enabled = false;
                headerForm.SMS_LABEL.Visible = false;
                headerForm.SMS_SEND_JOKYO.Visible = false;
            }
            else
            {
                parentForm.bt_process2.Text = "[2]ｼｮｰﾄﾒｯｾｰｼﾞ";
                headerForm.SMS_LABEL.Visible = true;
                headerForm.SMS_SEND_JOKYO.Visible = true;

                if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG || windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG )
                    {
                        headerForm.SMS_SEND_JOKYO.Text = "未送信";
                    }
                    // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン活性
                    parentForm.bt_process2.Enabled = true;
                }
                else
                {
                    // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン非活性
                    parentForm.bt_process2.Enabled = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
        /// </summary>
        /// <returns></returns>
        internal string[] SmsParamListSetting()
        {
            string[] smsparamList = new string[7];

            // 新規モードである場合、登録処理時のEntityを参照
            if(this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                T_UKETSUKE_SS_ENTRY entity = this.insEntryEntityList.FirstOrDefault();

                smsparamList[0] = ConstClass.DENPYOU_SHURUI_SS;
                smsparamList[1] = entity.SEQ.ToString();
                smsparamList[2] = entity.UKETSUKE_NUMBER.ToString();
                smsparamList[3] = entity.GYOUSHA_CD;
                smsparamList[4] = entity.GENBA_CD;
                smsparamList[5] = DateTime.Parse(entity.SAGYOU_DATE).ToString("yyyy/MM/dd(ddd)");
            }
            // 新規以外のモードである場合、受付画面の項目を参照
            else
            {
                smsparamList[0] = ConstClass.DENPYOU_SHURUI_SS;
                smsparamList[1] = this.daoUketsukeEntry.GetMaxSeq(this.form.UKETSUKE_NUMBER.Text);
                smsparamList[2] = this.form.UKETSUKE_NUMBER.Text;
                smsparamList[3] = this.form.GYOUSHA_CD.Text;
                smsparamList[4] = this.form.GENBA_CD.Text;
                smsparamList[5] = this.form.SAGYOU_DATE.Text;
            }
            smsparamList[6] = null;
            return smsparamList;
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト取得
        /// </summary>
        /// <returns></returns>
        internal List<int> SmsReceiverListSetting()
        {
            List<int> smsReceiverList = null;
            List<M_SMS_RECEIVER_LINK_GENBA> smsReceiverLink = null;

            // 新規モードの場合、登録処理時のEntityを参照
            if(this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                T_UKETSUKE_SS_ENTRY entity = this.insEntryEntityList.FirstOrDefault();

                smsReceiverLink = this.smsReceiverLinkGenbaDao.GetDataForSmsNyuuryoku(entity.GYOUSHA_CD, entity.GENBA_CD);
            }
            // 新規以外のモードである場合、受付画面の項目を参照
            else
            {
                smsReceiverLink = this.smsReceiverLinkGenbaDao.GetDataForSmsNyuuryoku(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
            }
            
            if (smsReceiverLink != null)
            {
                smsReceiverList = smsReceiverLink.Select(n => n.SYSTEM_ID.Value).ToList();
            }

            return smsReceiverList;
        }
    }
}
