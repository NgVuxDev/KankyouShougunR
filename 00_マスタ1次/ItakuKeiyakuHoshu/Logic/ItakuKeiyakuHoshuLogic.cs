// $Id: ItakuKeiyakuHoshuLogic.cs 55866 2015-07-16 06:07:38Z huangxy@oec-h.com $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using ItakuKeiyakuHoshu.APP;
using ItakuKeiyakuHoshu.Const;
using ItakuKeiyakuHoshu.Dto;
using ItakuKeiyakuHoshu.Properties;
using ItakuKeiyakuHoshu.Validator;
using MasterCommon.Logic;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.MasterAccess;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Function.ShougunCSCommon.Dto;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;
using Shougun.Core.ExternalConnection.FileUpload;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using System.Threading;
using Shougun.Core.Common.BusinessCommon.Dto;

namespace ItakuKeiyakuHoshu.Logic
{
    /// <summary>
    /// 委託契約登録画面のビジネスロジック
    /// </summary>
    public class ItakuKeiyakuHoshuLogic : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 表示している受付番号
        /// </summary>
        public String SystemId { get; set; }

        /// <summary>
        /// 委託契約書種類
        /// </summary>
        public String KeiyakuShurui { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        ///　初期判定フラグ
        /// </summary>
        public bool isFirst { get; set; }

        /// <summary>
        /// 別表１（予定）タブが選択されたかどうかのフラグ
        /// </summary>
        //public bool IsYoteiTabSelect { get; set; }

        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label ITAKU_KEIYAKU_STATUS_NAME;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal BusinessBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        private bool kihonNewRowFlg = false;
        private bool hinmeiNewRowFlg = false;
        private bool houkokuNewRowFlg = false;
        private bool tsumikaeNewRowFlg = false;
        private bool sbnNewRowFlg = false;
        private bool lastNewRowFlg = false;
        private bool oboeNewRowFlg = false;

        #region フィールド

        private readonly string ButtonInfoXmlPath = "ItakuKeiyakuHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ITAKU_KEIYAKU_KIHON_HST_GENBA_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuKihonHstGenbaDataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU1_HST_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu1HstDataSql.sql";
        private readonly string GET_ITAKU_KEIYAKU_HIMEI_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuHinmeiDataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU1_YOTEI_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu1YoteiDataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU2_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu2DataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_TSUMIKAE_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuTsumikaeDataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU3_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu3DataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU4_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu4DataSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_OBOE_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuOboeDataSql.sql";

        private readonly string GET_ITAKU_UPN_KYOKASHO_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuUpnKyokashoDataSql.sql";

        private readonly string GET_ITAKU_SBN_KYOKASHO_DATA_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuSbnKyokashoDataSql.sql";

        private readonly string GET_INPUTCD_DATA_ITAKUKEIYAKU_KIHON_SQL = "ItakuKeiyakuHoshu.Sql.GetInputCddataItakuKeiyakuKihonSql.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_DATA_SQL_1 = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaDataSql_1.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_DATA_SQL_2 = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaDataSql_2.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_DATA_SQL_3 = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaDataSql_3.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_DATA_SQL_4 = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaDataSql_4.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_DATA_SQL_5 = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaDataSql_5.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_DATA_SQL_6 = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaDataSql_6.sql";

        private readonly string GET_ITAKU_KEIYAKU_KIHON_HST_GENBA_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuKihonHstGenbaStructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU1_HST_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu1HstStructSql.sql";
        private readonly string GET_ITAKU_KEIYAKU_HINMEI_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuHinmeiStructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU1_YOTEI_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu1YoteiStructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_TSUMIKAE_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuTsumikaeStructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU2_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu2StructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU3_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu3StructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_BETSU4_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuBetsu4StructSql.sql";

        private readonly string GET_ITAKU_KEIYAKU_OBOE_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuKeiyakuOboeStructSql.sql";

        private readonly string GET_ITAKU_UPN_KYOKASHO_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuUpnKyokashoStructSql.sql";

        private readonly string GET_ITAKU_SBN_KYOKASHO_STRUCT_SQL = "ItakuKeiyakuHoshu.Sql.GetItakuSbnKyokashoStructSql.sql";

        private readonly string CHECK_GENBA_DATA_SQL = "ItakuKeiyakuHoshu.Sql.CheckGenbaDataSql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_KIHON_HST_GENBA_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuKihonHstGenbaSql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_BETSU1_HST_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuBetsu1HstSql.sql";
        private readonly string DELETE_ITAKU_KEIYAKU_HINMEI_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuHinmeiSql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_BETSU1_YOTEI_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuBetsu1YoteiSql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_BETSU2_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuBetsu2Sql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_TSUMIKAE_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuTsumikaeSql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_BETSU3_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuBetsu3Sql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_BETSU4_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuBetsu4Sql.sql";

        private readonly string DELETE_ITAKU_KEIYAKU_OBOE_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuKeiyakuOboeSql.sql";

        private readonly string DELETE_ITAKU_UPN_KYOKASHO_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuUpnKyokashoSql.sql";

        private readonly string DELETE_ITAKU_SBN_KYOKASHO_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuSbnKyokashoSql.sql";

        private readonly string DELETE_ITAKU_DENSHI_SOUHUSAKI_SQL = "ItakuKeiyakuHoshu.Sql.DeleteItakuDenshiSouhusakiSql.sql";

        private readonly string GET_SBNB_PATTERN_SBN_SQL = "ItakuKeiyakuHoshu.Sql.GetSbnbPatternSql.sql";

        private readonly string DELETE_SBNB_PATTERN_SBN_SQL = "ItakuKeiyakuHoshu.Sql.DeleteSbnbPatternSql.sql";

        private readonly string GET_CHIIKIBETSU_KYOKA_MEIGARA_SQL = "ItakuKeiyakuHoshu.Sql.GetChiikibetsuKyokaMeigaraDataSql.sql";

        /// <summary>
        /// 委託契約登録画面のDTO
        /// </summary>
        public ItakuKeiyakuHoshuDto dto;

        /// <summary>
        /// 委託契約登録画面Form
        /// </summary>
        private ItakuKeiyakuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;

        /// <summary>
        /// 報告書分類のDao
        /// </summary>
        private IM_HOUKOKUSHO_BUNRUIDao houokushoBunruiDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 地域のDao
        /// </summary>
        private IM_CHIIKIDao chiikiDao;

        /// <summary>
        /// 地域別許可番号のDao
        /// </summary>
        private IM_CHIIKIBETSU_KYOKADao chiikibetsuKyokaDao;

        /// <summary>
        /// 委託契約基本のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao kihonDao;

        /// <summary>
        /// 委託契約基本＿排出事業場のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHON_HST_GENBADao kihonHstGenbaDao;

        /// <summary>
        /// 委託契約別表1（排出）のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_BETSU1_HSTDao betsu1HstDao;

        /// <summary>
        /// 委託契約品名のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_HINMEIDao hinmeiDao;

        /// <summary>
        /// 委託契約別表1（予定）のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_BETSU1_YOTEIDao betsu1YoteiDao;

        /// <summary>
        /// 委託契約別表2（運搬）のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_BETSU2Dao betsu2Dao;

        /// <summary>
        /// 委託契約別表2（運搬）のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_TSUMIKAEDao tsumikaeDao;

        /// <summary>
        /// 委託契約別表3（処分）のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_BETSU3Dao betsu3Dao;

        /// <summary>
        /// 委託契約別表4（最終処分）のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_BETSU4Dao betsu4Dao;

        /// <summary>
        /// 委託契約別表覚書のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_OBOEDao oboeDao;

        /// <summary>
        /// 委託契約運搬許可証紐付のDao
        /// </summary>
        private IM_ITAKU_UPN_KYOKASHODao upnKyokashoDao;

        /// <summary>
        /// 委託契約処分許可証紐付のDao
        /// </summary>
        private IM_ITAKU_SBN_KYOKASHODao sbnKyokashoDao;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// 処分方法のDao
        /// </summary>
        private IM_SHOBUN_HOUHOUDao shobunHouhouDao;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao imHinmeiDao;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// 社内経路Dao
        /// </summary>
        private IM_DENSHI_KEIYAKU_SHANAI_KEIRODao shanaiKeiroDao;

        /// <summary>
        /// 社内経路名Dao
        /// </summary>
        private IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao shanaiKeiroNameDao;

        /// <summary>
        /// 電子契約送付先Dao
        /// </summary>
        private DenshiKeiyakuSouhusakiDAO denshiKeiyakuSouhusakiDao;

        /// <summary>
        /// 委託契約基本のDao
        /// </summary>
        private IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao fileLinkItakuKeiyakuKihonDao;

        /// <summary>
        /// ファイルデータDao
        /// </summary>
        private FILE_DATADAO fileDataDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private r_framework.Entity.M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 委託契約基本＿排出事業場のTable
        /// </summary>
        private DataTable kihonHstGenbaTable;

        /// <summary>
        /// 委託契約別表1（排出）のTable
        /// </summary>
        private DataTable betsu1HstTable;

        /// <summary>
        /// 委託契約品名のTable
        /// </summary>
        private DataTable hinmeiTable;

        /// <summary>
        /// 委託契約別表1（予定）のTable
        /// </summary>
        private DataTable betsu1YoteiTable;

        /// <summary>
        /// 委託契約別表2（運搬）のTable
        /// </summary>
        private DataTable betsu2Table;

        /// <summary>
        /// 委託契約積替のTable
        /// </summary>
        private DataTable tsumikaeTable;

        /// <summary>
        /// 委託契約別表3（処分）のTable
        /// </summary>
        private DataTable betsu3Table;

        /// <summary>
        /// 委託契約別表4（最終処分）のTable
        /// </summary>
        private DataTable betsu4Table;

        /// <summary>
        /// 委託契約別表覚書のTable
        /// </summary>
        private DataTable oboeTable;

        /// <summary>
        /// 委託契約運搬許可証紐付のTable
        /// </summary>
        private DataTable upnKyokashoTable;

        /// <summary>
        /// 委託契約処分許可証紐付のTable
        /// </summary>
        private DataTable sbnKyokashoTable;

        /// <summary>
        /// Error処理をキャンセルするか判断します
        /// </summary>
        internal bool errorCancelFlg = true;

        private string previousErrorCd = string.Empty;
        public bool isSeted = false;

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        public FileUploadLogic uploadLogic;

        /// <summary>
        /// 委託契約書の登録処理の成否（ファイルアップロード画面起動判定）
        /// </summary>
        public bool isFileUploadOK = false;

        /// <summary>
        /// ファイルアップロード用画面用のシステムID
        /// </summary>
        private string systemIdForUpload;

        /// <summary>
        /// 自社情報マスタのDao
        /// </summary>
        private IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// 自社名
        /// </summary>
        private string corpName;

        /// <summary>
        /// 登録メッセージ非表示フラグ（false:非表示）
        /// </summary>
        private bool registMsgFlg = true;

        /// <summary>
        /// Inxs logic
        /// </summary>
        private InxsContractLogic inxsContractLogic;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ItakuKeiyakuHoshuLogic(ItakuKeiyakuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new ItakuKeiyakuHoshuDto();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.houokushoBunruiDao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.chiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
            this.chiikibetsuKyokaDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            this.kihonDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.kihonHstGenbaDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHON_HST_GENBADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_HINMEIDao>();
            this.betsu1YoteiDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_BETSU1_YOTEIDao>();
            this.betsu1HstDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_BETSU1_HSTDao>();
            this.betsu2Dao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_BETSU2Dao>();
            this.tsumikaeDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_TSUMIKAEDao>();
            this.betsu3Dao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_BETSU3Dao>();
            this.betsu4Dao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_BETSU4Dao>();
            this.oboeDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_OBOEDao>();
            this.upnKyokashoDao = DaoInitUtility.GetComponent<IM_ITAKU_UPN_KYOKASHODao>();
            this.sbnKyokashoDao = DaoInitUtility.GetComponent<IM_ITAKU_SBN_KYOKASHODao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.shobunHouhouDao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.imHinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.shanaiKeiroDao = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRODao>();
            this.shanaiKeiroNameDao = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao>();
            this.denshiKeiyakuSouhusakiDao = DaoInitUtility.GetComponent<DenshiKeiyakuSouhusakiDAO>();
            this.fileLinkItakuKeiyakuKihonDao = DaoInitUtility.GetComponent<IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao>();
            this.isRegist = false;
            this.isError = false;
            this.isFirst = true;

            this.fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
            this.uploadLogic = new FileUploadLogic();
            this.isFileUploadOK = false;
            this.corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();

            this.inxsContractLogic = new InxsContractLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="windowType">画面種別</param>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                this.label20 = new System.Windows.Forms.Label();
                this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
                this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.label20.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                this.label20.ForeColor = System.Drawing.Color.White;
                this.label20.Location = new System.Drawing.Point(550, 10);
                this.label20.Name = "label20";
                this.label20.Size = new System.Drawing.Size(108, 20);
                this.label20.TabIndex = 0;
                this.label20.Text = "契約状況";
                this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                this.ITAKU_KEIYAKU_STATUS_NAME = new System.Windows.Forms.Label();
                this.ITAKU_KEIYAKU_STATUS_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
                this.ITAKU_KEIYAKU_STATUS_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.ITAKU_KEIYAKU_STATUS_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.ITAKU_KEIYAKU_STATUS_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                this.ITAKU_KEIYAKU_STATUS_NAME.ForeColor = System.Drawing.Color.Black;
                this.ITAKU_KEIYAKU_STATUS_NAME.Location = new System.Drawing.Point(658, 10);
                this.ITAKU_KEIYAKU_STATUS_NAME.Name = "ITAKU_KEIYAKU_STATUS_NAME";
                this.ITAKU_KEIYAKU_STATUS_NAME.Size = new System.Drawing.Size(60, 20);
                this.ITAKU_KEIYAKU_STATUS_NAME.TabIndex = 0;
                this.ITAKU_KEIYAKU_STATUS_NAME.Text = "";
                this.ITAKU_KEIYAKU_STATUS_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                if (this.form.dispTourokuHouhou == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON)
                {
                    this.label20.Visible = false;
                    this.ITAKU_KEIYAKU_STATUS_NAME.Visible = false;
                }

                parentForm.headerForm.Controls.Add(this.label20);
                parentForm.headerForm.Controls.Add(this.ITAKU_KEIYAKU_STATUS_NAME);

                // システム設定を読み込む
                this.sysInfoEntity = CommonShogunData.SYS_INFO;
                //M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                //if (sysInfo != null)
                //{
                //    this.sysInfoEntity = sysInfo[0];
                //}

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化
                this.EventInit(parentForm);

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);

                this.allControl = this.form.allControl;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType">画面種別</param>
        /// <param name="parentForm">親フォーム</param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            bool catchErr = false;
            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNew(parentForm);
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    catchErr = this.WindowInitUpdate(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(parentForm);
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    catchErr = this.WindowInitReference(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew(parentForm);
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            if (string.IsNullOrEmpty(this.SystemId))
            {
                // 【新規】モードで初期化
                bool catchErr = WindowInitNewMode(parentForm);
                if (catchErr)
                {
                    throw new Exception("");
                }
            }
            else
            {
                // 【複写】モードで初期化
                WindowInitNewCopyMode(parentForm);
            }
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロール操作可能とする
                this.AllControlLock(false);

                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;
                this.ITAKU_KEIYAKU_STATUS_NAME.Text = string.Empty;

                // グリッド
                // ※必ず別表1(予定)からクリアすること！！
                // 　排出現場や別表1(排出)と関連付けされているため、エラーになってしまう
                this.SetIchiran(this.form.listHoukokushoBunrui, null);
                this.SetIchiran(this.form.listKihonHstGenba, null);
                this.SetIchiran(this.form.listHinmei, null);
                this.SetIchiran(this.form.listBetsu2, null);
                this.SetIchiran(this.form.listBetsu3, null);
                this.SetIchiran(this.form.listBetsu4, null);
                this.SetIchiran(this.form.listOboe, null);
                this.SetIchiran(this.form.listUpnKyokasho, null);
                this.SetIchiran(this.form.listSbnKyokasho, null);
                this.SetIchiran(this.form.listTsumikae, null);

                // 入力項目
                this.form.HAISHUTSU_JIGYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
                this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
                this.form.HAISHUTSU_JIGYOUJOU_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_ADDRESS1.Text = string.Empty;
                this.form.GENBA_ADDRESS2.Text = string.Empty;
                this.form.GYOUSHA_TODOUFUKEN_NAME.Text = string.Empty;
                this.form.GENBA_TODOUFUKEN_NAME.Text = string.Empty;
                this.form.HST_FREE_COMMENT.Text = string.Empty;
                this.form.HST_FREE_COMMENT.Visible = false;
                this.form.ITAKU_KEIYAKU_FILE_PATH.Text = string.Empty;
                this.form.customDataGridView1.Rows.Clear();
                // TODO 後でhearderへ移動します
                this.form.ITAKU_KEIYAKU_STATUS.Text = string.Empty;
                this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = string.Empty;
                this.form.SYSTEM_ID.Text = string.Empty;
                this.form.ITAKU_KEIYAKU_NO.Text = string.Empty;
                if (String.IsNullOrEmpty(this.KeiyakuShurui) || AppConfig.IsManiLite)
                {
                    this.form.ITAKU_KEIYAKU_SHURUI.Text = "1";
                    this.form.ITAKU_KEIYAKU_SHURUI_NAME.Text = this.GetItakuKeiyakuShuruiName("1");
                }
                else
                {
                    this.form.ITAKU_KEIYAKU_SHURUI.Text = this.KeiyakuShurui;
                    this.form.ITAKU_KEIYAKU_SHURUI_NAME.Text = this.GetItakuKeiyakuShuruiName(this.KeiyakuShurui);
                }
                this.form.SHIHARAI_HOUHOU.Text = string.Empty;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.KEIYAKUSHO_KEIYAKU_DATE.Value = DateTime.Today;
                //this.form.KEIYAKUSHO_CREATE_DATE.Value = DateTime.Today;
                this.form.KEIYAKUSHO_KEIYAKU_DATE.Value = this.parentForm.sysDate.Date;
                this.form.KEIYAKUSHO_CREATE_DATE.Value = this.parentForm.sysDate.Date;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                this.form.KEIYAKUSHO_SEND_DATE.Value = null;
                this.form.KEIYAKUSHO_RETURN_DATE.Value = null;
                this.form.KEIYAKUSHO_END_DATE.Value = null;
                this.form.KOBETSU_SHITEI_CHECK.Checked = true;
                this.form.KOUSHIN_SHUBETSU.Text = "1";
                //this.form.YUUKOU_BEGIN.Value = DateTime.Today;
                this.form.YUUKOU_BEGIN.Value = null;
                this.form.YUUKOU_END.Value = null;
                this.form.KOUSHIN_END_DATE.Value = null;
                this.form.BIKOU1.Text = string.Empty;
                this.form.BIKOU2.Text = string.Empty;

                this.form.SHOBUN_PATTERN_SYSTEM_ID.Text = string.Empty;
                this.form.SHOBUN_PATTERN_SEQ.Text = string.Empty;
                this.form.SHOBUN_PATTERN_NAME.Text = string.Empty;
                this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text = string.Empty;
                this.form.LAST_SHOBUN_PATTERN_SEQ.Text = string.Empty;
                this.form.LAST_SHOBUN_PATTERN_NAME.Text = string.Empty;
                this.form.UPNKYOKA_GYOUSHA_CD.Text = string.Empty;
                this.form.UPNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.UPNKYOKA_CHIIKI_CD.Text = string.Empty;
                this.form.UPNKYOKA_CHIIKI_NAME_RYAKU.Text = string.Empty;
                if (this.form.dispTourokuHouhou.Equals(ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI))
                {
                    this.form.UPNKYOKA_KBN.Text = "1";
                    this.form.SBNKYOKA_KBN.Text = "3";
                }
                this.form.UPNKYOKA_BEGIN.Value = null;
                this.form.UPNKYOKA_END.Value = null;
                this.form.UPNKYOKA_NO.Text = null;
                this.form.SBNKYOKA_GYOUSHA_CD.Text = string.Empty;
                this.form.SBNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
                this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.SBNKYOKA_CHIIKI_CD.Text = string.Empty;
                this.form.SBNKYOKA_CHIIKI_NAME_RYAKU.Text = string.Empty;
                this.form.SBNKYOKA_BEGIN.Value = null;
                this.form.SBNKYOKA_END.Value = null;
                this.form.SBNKYOKA_NO.Text = null;

                // 一覧のデータをセットする

                // 一覧にデータをセットする際に、いずれかのエラーによりイベントがキャンセルされると
                // システムエラーが発生するため、セットし終わるまでエラー処理を全てキャンセルする
                this.errorCancelFlg = false;
                this.kihonHstGenbaTable = this.kihonHstGenbaDao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_KIHON_HST_GENBA_STRUCT_SQL, new M_ITAKU_KEIYAKU_KIHON_HST_GENBA());
                this.kihonHstGenbaTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.kihonHstGenbaTable.Columns["SEQ"].AllowDBNull = true;
                this.kihonHstGenbaTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.kihonHstGenbaTable.Columns["TIME_STAMP"].Unique = false;
                this.form.listKihonHstGenba.DataSource = this.kihonHstGenbaTable;
                this.form.listKihonHstGenba.Enabled = false;

                this.form.listHinmei.CellFormatting -= new EventHandler<CellFormattingEventArgs>(this.form.ListBetsu1HstCellFormatting);
                this.hinmeiTable = this.hinmeiDao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_HINMEI_STRUCT_SQL, new M_ITAKU_KEIYAKU_HINMEI());
                this.hinmeiTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.hinmeiTable.Columns["SEQ"].AllowDBNull = true;
                this.hinmeiTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.hinmeiTable.Columns["TIME_STAMP"].Unique = false;

                this.form.listHinmei.DataSource = this.hinmeiTable;
                this.form.listHinmei.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListBetsu1HstCellFormatting);
                /*this.betsu1YoteiTable = this.betsu1YoteiDao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU1_YOTEI_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU1_YOTEI());
                this.betsu1YoteiTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.betsu1YoteiTable.Columns["SEQ"].AllowDBNull = true;
                this.betsu1YoteiTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.betsu1YoteiTable.Columns["TIME_STAMP"].Unique = false;
                this.form.listHoukokushoBunrui.DataSource = this.betsu1YoteiTable;*/

                this.betsu1HstTable = this.betsu1HstDao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU1_HST_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU1_HST());
                this.betsu1HstTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.betsu1HstTable.Columns["SEQ"].AllowDBNull = true;
                this.betsu1HstTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.betsu1HstTable.Columns["TIME_STAMP"].Unique = false;
                this.form.listHoukokushoBunrui.DataSource = this.betsu1HstTable;

                this.betsu2Table = this.betsu2Dao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU2_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU2());
                this.betsu2Table.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.betsu2Table.Columns["SEQ"].AllowDBNull = true;
                this.betsu2Table.Columns["TIME_STAMP"].AllowDBNull = true;
                this.betsu2Table.Columns["TIME_STAMP"].Unique = false;
                this.form.listBetsu2.DataSource = this.betsu2Table;

                this.form.listTsumikae.CellFormatting -= new EventHandler<CellFormattingEventArgs>(this.form.ListTsumikaeCellFormatting);
                this.tsumikaeTable = this.tsumikaeDao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_TSUMIKAE_STRUCT_SQL, new M_ITAKU_KEIYAKU_TSUMIKAE());
                this.tsumikaeTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.tsumikaeTable.Columns["SEQ"].AllowDBNull = true;
                this.tsumikaeTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.tsumikaeTable.Columns["TIME_STAMP"].Unique = false;
                foreach (DataColumn col in this.tsumikaeTable.Columns)
                {
                    col.ReadOnly = false;
                }
                this.form.listTsumikae.DataSource = this.tsumikaeTable;
                this.form.listTsumikae.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListTsumikaeCellFormatting);

                this.betsu3Table = this.betsu3Dao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU3_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU3());
                this.betsu3Table.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.betsu3Table.Columns["SEQ"].AllowDBNull = true;
                this.betsu3Table.Columns["TIME_STAMP"].AllowDBNull = true;
                this.betsu3Table.Columns["TIME_STAMP"].Unique = false;
                foreach (DataColumn col in this.betsu3Table.Columns)
                {
                    col.ReadOnly = false;
                }
                this.form.listBetsu3.DataSource = this.betsu3Table;
                this.betsu4Table = this.betsu4Dao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU4_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU4());
                this.betsu4Table.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.betsu4Table.Columns["SEQ"].AllowDBNull = true;
                this.betsu4Table.Columns["TIME_STAMP"].AllowDBNull = true;
                this.betsu4Table.Columns["TIME_STAMP"].Unique = false;
                this.form.listBetsu4.DataSource = this.betsu4Table;
                this.oboeTable = this.oboeDao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_OBOE_STRUCT_SQL, new M_ITAKU_KEIYAKU_OBOE());
                this.oboeTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.oboeTable.Columns["SEQ"].AllowDBNull = true;
                this.oboeTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.oboeTable.Columns["TIME_STAMP"].Unique = false;
                this.form.listOboe.DataSource = this.oboeTable;
                this.upnKyokashoTable = this.upnKyokashoDao.GetDataBySqlFile(this.GET_ITAKU_UPN_KYOKASHO_STRUCT_SQL, new M_ITAKU_UPN_KYOKASHO());
                this.upnKyokashoTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.upnKyokashoTable.Columns["SEQ"].AllowDBNull = true;
                this.upnKyokashoTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.upnKyokashoTable.Columns["TIME_STAMP"].Unique = false;
                this.form.listUpnKyokasho.DataSource = this.upnKyokashoTable;
                this.sbnKyokashoTable = this.sbnKyokashoDao.GetDataBySqlFile(this.GET_ITAKU_SBN_KYOKASHO_STRUCT_SQL, new M_ITAKU_SBN_KYOKASHO());
                this.sbnKyokashoTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.sbnKyokashoTable.Columns["SEQ"].AllowDBNull = true;
                this.sbnKyokashoTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.sbnKyokashoTable.Columns["TIME_STAMP"].Unique = false;
                this.form.listSbnKyokasho.DataSource = this.sbnKyokashoTable;
                this.errorCancelFlg = true;

                FileUploadButtonSetting();

                DenshiKeiyakuButtonSetting();

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                PrintButtonSetting();
                parentForm.bt_func5.Enabled = false;     // 印刷 157928
                // システム情報から初期値をセットする
                if (this.sysInfoEntity != null)
                {
                    this.form.KOUSHIN_SHUBETSU.Text = this.sysInfoEntity.ITAKU_KEIYAKU_KOUSHIN_SHUBETSU.Value.ToString();
                }

                // 内部データ
                this.dto = new ItakuKeiyakuHoshuDto();

                // 処理モードを新規に設定
                this.form.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);

                // 初期タブ選択
                this.form.tabItakuKeiyakuData.SelectedTab = this.form.tabPage1;
                bool catchErr = this.TabSelect();
                if (catchErr)
                {
                    return true;
                }
                //this.form.tabItakuKeiyakuData.TabPages[3].Visible = false;
                //this.form.tabItakuKeiyakuData.TabPages[4].Visible = true;
                //this.form.tabItakuKeiyakuData.TabPages[5].Visible = true;

                // 委託契約ステータス等をセットする
                catchErr = this.KoushinShubetsuTextChanged();
                if (catchErr)
                {
                    return true;
                }
                catchErr = this.CheckKeiyakuDate();
                if (catchErr)
                {
                    return true;
                }
                catchErr = this.CheckStatus();
                if (catchErr)
                {
                    return true;
                }

                // 初期フォーカス設定
                this.form.HAISHUTSU_JIGYOUSHA_CD.Focus();

                // 委託契約運搬タブの制御
                this.ListBtu2SeiGyo();
                // 委託契約処分タブの制御
                this.ListBtu3SeiGyo();

                // 電子契約タブの制御
                //this.form.ACCESS_CD.Text = string.Empty;
                //this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = string.Empty;
                //this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty;

                // 自社名を取得
                M_CORP_INFO[] corpInfo = this.corpInfoDao.GetAllData();
                corpName = corpInfo[0].CORP_NAME;

                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    // 電子契約⑤アクセスコード初期表示
                    if (!string.IsNullOrWhiteSpace(sysInfoEntity.DENSHI_KEIYAKU_ACCESS_CODE))
                    {
                        this.form.ACCESS_CD.Text = sysInfoEntity.DENSHI_KEIYAKU_ACCESS_CODE;
                    }

                    // ユーザ定義情報を取得
                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                    this.form.SHANAI_KEIRO.Text = this.uploadLogic.GetUserProfileValue(userProfile, "電子契約社内経路");

                    // システム個別設定ー電子経路ー社内経路名「設定あり」
                    if (this.form.SHANAI_KEIRO.Text == "1")
                    {
                        this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = this.uploadLogic.GetUserProfileValue(userProfile, "電子契約社内経路CD");
                        this.ShanaiKeiroCDValidated();
                    }

                    // システム個別設定ー電子経路ー社内経路名「設定なし」
                    else if (this.form.SHANAI_KEIRO.Text == "2")
                    {
                        this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = string.Empty;
                        this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Enabled = false;
                    }

                    this.form.keiroIchiran2.Rows.Clear();
                }

                if (this.form.dispTourokuHouhou == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON)
                { 
                    
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitNewMode", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            // 全コントロールを操作可能とする
            this.AllControlLock(false);

            // 検索結果を画面に設定
            bool catchErr = this.WindowInitNewMode(parentForm);
            if (catchErr)
            {
                throw new Exception("");
            }
            this.SetWindowData();

            // 入力項目制御
            this.form.SYSTEM_ID.Text = string.Empty;

            // ヘッダー項目
            DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;
            header.CreateDate.Text = string.Empty;
            header.CreateUser.Text = string.Empty;
            header.LastUpdateDate.Text = string.Empty;
            header.LastUpdateUser.Text = string.Empty;

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消
            parentForm.bt_func12.Enabled = true;    // 閉じる

            PrintButtonSetting();
            parentForm.bt_func5.Enabled = false;     // 印刷 157928
            // 処理モードを新規に設定
            this.form.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);

            // 委託契約運搬タブの制御
            this.ListBtu2SeiGyo();
            // 委託契約処分タブの制御
            this.ListBtu3SeiGyo();

            // 初期タブ選択
            this.errorCancelFlg = false;
            for (int i = this.form.tabItakuKeiyakuData.TabCount - 1; i >= 0; i--)
            {
                this.form.tabItakuKeiyakuData.SelectedIndex = i;
            }
            this.errorCancelFlg = true;
            //this.TabSelect();

            // 委託契約ステータス等をセットする
            catchErr = this.KoushinShubetsuTextChanged();
            if (catchErr)
            {
                throw new Exception("");
            }
            catchErr = this.CheckKeiyakuDate();
            if (catchErr)
            {
                throw new Exception("");
            }
            catchErr = this.CheckStatus();
            if (catchErr)
            {
                throw new Exception("");
            }

            // 初期フォーカス設定
            this.form.HAISHUTSU_JIGYOUSHA_CD.Focus();
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                this.form.HAISHUTSU_JIGYOUSHA_CD.Validated -= new EventHandler(this.form.HaishutsuJigyoushaCDValidated);//157928
                // 全コントロールを操作可能とする
                this.AllControlLock(false);

                // 検索結果を画面に設定
                bool catchErr = this.WindowInitNewMode(parentForm);
                if (catchErr)
                {
                    return true;
                }
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.HAISHUTSU_JIGYOUSHA_CD.Enabled = false;   // 排出事業者CD
                this.form.HAISHUTSU_JIGYOUSHA_SEARCH_BUTTON.Enabled = false;
                this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = false;   // 排出事業場CD
                this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = false;
                this.form.SYSTEM_ID.ReadOnly = true;                // システムID
                this.form.SYSTEM_ID.Enabled = false;                // システムID

                FileUploadButtonSetting();

                DenshiKeiyakuButtonSetting();
                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                PrintButtonSetting();
                
                // 処理モードを修正に設定
                this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

                // 委託契約運搬タブの制御
                this.ListBtu2SeiGyo();
                // 委託契約処分タブの制御
                this.ListBtu3SeiGyo();

                // 初期タブ選択
                this.errorCancelFlg = false;
                for (int i = this.form.tabItakuKeiyakuData.TabCount - 1; i >= 0; i--)
                {
                    this.form.tabItakuKeiyakuData.SelectedIndex = i;
                }
                this.errorCancelFlg = true;
                this.form.HAISHUTSU_JIGYOUSHA_CD.Validated += new EventHandler(this.form.HaishutsuJigyoushaCDValidated);//157928
                //this.TabSelect();

                // 初期フォーカス設定
                this.form.ITAKU_KEIYAKU_NO.Focus();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitUpdate", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            // 検索結果を画面に設定
            bool catchErr = this.WindowInitNewMode(parentForm);
            if (catchErr)
            {
                throw new Exception("");
            }
            this.SetWindowData();

            // 削除モード固有UI設定
            this.AllControlLock(true);

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = false;   // 取消
            parentForm.bt_func12.Enabled = true;    // 閉じる

            PrintButtonSetting();
            parentForm.bt_func5.Enabled = false;     // 印刷 157928
            // 処理モードを削除に設定
            this.form.SetWindowType(WINDOW_TYPE.DELETE_WINDOW_FLAG);

            // 委託契約運搬タブの制御
            this.ListBtu2SeiGyo();
            // 委託契約処分タブの制御
            this.ListBtu3SeiGyo();

            // 初期タブ選択
            for (int i = this.form.tabItakuKeiyakuData.TabCount - 1; i >= 0; i--)
            {
                this.form.tabItakuKeiyakuData.SelectedIndex = i;
            }
            //this.TabSelect();
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                this.form.HAISHUTSU_JIGYOUSHA_CD.Validated -= new EventHandler(this.form.HaishutsuJigyoushaCDValidated);
                // 検索結果を画面に設定
                bool catchErr = this.WindowInitNewMode(parentForm);
                if (catchErr)
                {
                    throw new Exception("");
                }
                this.SetWindowData();

                // 参照モード固有UI設定
                this.AllControlLock(true);
                this.form.HAISHUTSU_JIGYOUSHA_CD.Validated += new EventHandler(this.form.HaishutsuJigyoushaCDValidated);
                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = true;    // 修正//157928
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                PrintButtonSetting();

                // 処理モードを参照に設定
                this.form.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

                // 委託契約運搬タブの制御
                this.ListBtu2SeiGyo();
                // 委託契約処分タブの制御
                this.ListBtu3SeiGyo();

                // 初期タブ選択
                for (int i = this.form.tabItakuKeiyakuData.TabCount - 1; i >= 0; i--)
                {
                    this.form.tabItakuKeiyakuData.SelectedIndex = i;
                }
                //this.TabSelect();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitReference", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitReference", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            //this.IsYoteiTabSelect = false;

            this.Search();

            // ヘッダー項目
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = this.dto.ItakuKeiyakuKihon.CREATE_DATE.ToString();
            header.CreateUser.Text = this.dto.ItakuKeiyakuKihon.CREATE_USER;
            header.LastUpdateDate.Text = this.dto.ItakuKeiyakuKihon.UPDATE_DATE.ToString();
            header.LastUpdateUser.Text = this.dto.ItakuKeiyakuKihon.UPDATE_USER;

            // 各テキストボックス設定
            this.form.HAISHUTSU_JIGYOUSHA_CD.Text = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD;
            bool catchErr = this.SetJigyoushaData(this.form.HAISHUTSU_JIGYOUSHA_CD.Text, this.form.GYOUSHA_NAME_RYAKU, this.form.GYOUSHA_ADDRESS1, this.form.GYOUSHA_ADDRESS2, this.form.GYOUSHA_TODOUFUKEN_NAME);
            if (catchErr)
            {
                throw new Exception("");
            }
            this.form.HAISHUTSU_JIGYOUJOU_CD.Text = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD;
            this.SetJigyoujouData(this.form.HAISHUTSU_JIGYOUSHA_CD.Text, this.form.HAISHUTSU_JIGYOUJOU_CD.Text, this.form.GENBA_NAME_RYAKU, this.form.GENBA_ADDRESS1, this.form.GENBA_ADDRESS2, this.form.GENBA_TODOUFUKEN_NAME, out catchErr);
            if (catchErr)
            {
                throw new Exception("");
            }
            if (!string.IsNullOrEmpty(this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD))
            {
                this.form.HST_FREE_COMMENT.Text = string.Empty;
                this.form.HST_FREE_COMMENT.Visible = false;
                this.form.KOBETSU_SHITEI_CHECK.Enabled = true;
            }
            else
            {
                this.form.HST_FREE_COMMENT.Visible = true;
                this.form.HST_FREE_COMMENT.Text = this.dto.ItakuKeiyakuKihon.HST_FREE_COMMENT;
                this.form.KOBETSU_SHITEI_CHECK.Enabled = true;
            }
            // ファイル一覧へ設定
            this.form.ITAKU_KEIYAKU_FILE_PATH.Text = this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_FILE_PATH;

            if (AppConfig.AppOptions.IsFileUpload())
            {
                this.SetFileUploadIchiran();
            }

            // TODO 後で HEARD へ　移動します
            if (!this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_STATUS.IsNull)
            {
                this.form.ITAKU_KEIYAKU_STATUS.Text = this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_STATUS.Value.ToString();
                this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_STATUS.Value);
                this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_STATUS.Value);
            }
            this.form.SYSTEM_ID.Text = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.form.ITAKU_KEIYAKU_NO.Text = this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_NO;
            this.form.ITAKU_KEIYAKU_SHURUI.Text = this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI.Value.ToString();
            this.form.ITAKU_KEIYAKU_SHURUI_NAME.Text = this.GetItakuKeiyakuShuruiName(this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI.Value.ToString());
            this.form.SHIHARAI_HOUHOU.Text = this.dto.ItakuKeiyakuKihon.SHIHARAI_HOUHOU;
            if (!this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE.IsNull)
            {
                this.form.KEIYAKUSHO_KEIYAKU_DATE.Value = (DateTime)this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE;
            }
            else
            {
                this.form.KEIYAKUSHO_KEIYAKU_DATE.Value = null;
            }
            if (!this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_CREATE_DATE.IsNull)
            {
                this.form.KEIYAKUSHO_CREATE_DATE.Value = (DateTime)this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_CREATE_DATE;
            }
            if (!this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_SEND_DATE.IsNull)
            {
                this.form.KEIYAKUSHO_SEND_DATE.Value = (DateTime)this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_SEND_DATE;
            }
            if (!this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_RETURN_DATE.IsNull)
            {
                this.form.KEIYAKUSHO_RETURN_DATE.Value = (DateTime)this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_RETURN_DATE;
            }
            if (!this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_END_DATE.IsNull)
            {
                this.form.KEIYAKUSHO_END_DATE.Value = (DateTime)this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_END_DATE;
            }
            if (!this.dto.ItakuKeiyakuKihon.KOBETSU_SHITEI_CHECK.IsNull)
            {
                this.form.KOBETSU_SHITEI_CHECK.Checked = this.dto.ItakuKeiyakuKihon.KOBETSU_SHITEI_CHECK.Value;
            }

            this.form.listKihonHstGenba.Enabled = !this.form.KOBETSU_SHITEI_CHECK.Checked;
            if (!this.dto.ItakuKeiyakuKihon.KOUSHIN_SHUBETSU.IsNull)
            {
                this.form.KOUSHIN_SHUBETSU.Text = this.dto.ItakuKeiyakuKihon.KOUSHIN_SHUBETSU.Value.ToString();
            }
            if (!this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.IsNull)
            {
                this.form.YUUKOU_BEGIN.Value = (DateTime)this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN;
            }
            if (!this.dto.ItakuKeiyakuKihon.YUUKOU_END.IsNull)
            {
                this.form.YUUKOU_END.Value = (DateTime)this.dto.ItakuKeiyakuKihon.YUUKOU_END;
            }
            else
            {
                // YUUKOU_BEGIN設定時に自動(DateControlValueChanged)で+1年が設定されるため、再度初期化
                this.form.YUUKOU_END.Value = null;
            }
            if (!this.dto.ItakuKeiyakuKihon.KOUSHIN_END_DATE.IsNull)
            {
                this.form.KOUSHIN_END_DATE.Value = (DateTime)this.dto.ItakuKeiyakuKihon.KOUSHIN_END_DATE;
            }
            this.form.BIKOU1.Text = this.dto.ItakuKeiyakuKihon.BIKOU1;
            this.form.BIKOU2.Text = this.dto.ItakuKeiyakuKihon.BIKOU2;

            if (!this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_SYSTEM_ID.IsNull)
            {
                this.form.SHOBUN_PATTERN_SYSTEM_ID.Text = this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_SYSTEM_ID.ToString();
            }
            if (!this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_SEQ.IsNull)
            {
                this.form.SHOBUN_PATTERN_SEQ.Text = this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_SEQ.ToString();
            }
            this.form.SHOBUN_PATTERN_NAME.Text = this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_NAME;
            if (!this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_SYSTEM_ID.IsNull)
            {
                this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text = this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_SYSTEM_ID.ToString();
            }
            if (!this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_SEQ.IsNull)
            {
                this.form.LAST_SHOBUN_PATTERN_SEQ.Text = this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_SEQ.ToString();
            }
            this.form.LAST_SHOBUN_PATTERN_NAME.Text = this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_NAME;

            // データソースバインド処理
            this.SetIchiran(this.form.listKihonHstGenba, kihonHstGenbaTable);
            this.SetIchiran(this.form.listHinmei, hinmeiTable);
            this.SetIchiran(this.form.listHoukokushoBunrui, betsu1HstTable);
            this.SetIchiran(this.form.listBetsu2, betsu2Table);
            this.SetIchiran(this.form.listTsumikae, tsumikaeTable);
            this.SetIchiran(this.form.listBetsu3, betsu3Table);
            this.SetIchiran(this.form.listBetsu4, betsu4Table);
            this.SetIchiran(this.form.listOboe, oboeTable);
            this.SetIchiran(this.form.listUpnKyokasho, upnKyokashoTable);
            this.SetIchiran(this.form.listSbnKyokasho, sbnKyokashoTable);

            // 電子契約タブへ設定（電子契約オプション = ONの場合）
            if (AppConfig.AppOptions.IsDenshiKeiyaku()
                && this.dto.itakuKeiyakuDenshiSouhusaki != null
                && this.dto.itakuKeiyakuDenshiSouhusaki.Length > 0)
            {
                // アクセスコード
                this.form.ACCESS_CD.Text = this.dto.itakuKeiyakuDenshiSouhusaki[0].ACCESS_CD;
                // 電子経路名CD
                if (!this.dto.itakuKeiyakuDenshiSouhusaki[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.IsNull)
                {
                    this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = this.dto.itakuKeiyakuDenshiSouhusaki[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ToString();
                    // 電子経路名CDをもとに名称を取得して設定する。
                    M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME param = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();
                    param.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = this.dto.itakuKeiyakuDenshiSouhusaki[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD;
                    M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[] keiroName = this.shanaiKeiroNameDao.GetAllValidData(param);
                    if (keiroName != null && keiroName.Length > 0)
                    {
                        this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = keiroName[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
                    }
                }
                //社内経路（使用する/しない）
                if (!this.dto.itakuKeiyakuDenshiSouhusaki[0].DENSHI_KEIYAKU_SHANAI_KEIRO.IsNull)
                {
                    this.form.SHANAI_KEIRO.Text = this.dto.itakuKeiyakuDenshiSouhusaki[0].DENSHI_KEIYAKU_SHANAI_KEIRO.ToString();
                }

                if (this.form.keiroIchiran.Rows.Count > 0)
                {
                    this.form.keiroIchiran.Rows.Clear();
                }

                if (this.form.keiroIchiran2.Rows.Count > 0)
                {
                    this.form.keiroIchiran2.Rows.Clear();
                }

                // 経路一覧へ設定
                if (this.dto.itakuKeiyakuDenshiSouhusaki[0].MAIL_ADDRESS != null
                    || !string.IsNullOrEmpty(this.dto.itakuKeiyakuDenshiSouhusaki[0].MAIL_ADDRESS))
                {
                    int j = 0;
                    for (int i = 0; i < this.dto.itakuKeiyakuDenshiSouhusaki.Length; i++)
                    {
                        //this.form.keiroIchiran.Rows.Add();
                        //// 削除
                        //this.form.keiroIchiran.Rows[i].Cells["chb_delete"].Value = false;
                        //// 優先
                        //this.form.keiroIchiran.Rows[i].Cells["YUUSEN_NO"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].PRIORITY_NO;
                        //// 社員CD
                        //this.form.keiroIchiran.Rows[i].Cells["SHAIN_CD"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].SHAIN_CD;
                        //// 社員名
                        //this.form.keiroIchiran.Rows[i].Cells["SHAIN_NAME"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].SOUHU_TANTOUSHA_NAME;
                        //// メールアドレス
                        //this.form.keiroIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].MAIL_ADDRESS;
                        //// TEL
                        //this.form.keiroIchiran.Rows[i].Cells["TEL_NO"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].TEL_NO;
                        //// 宛先名
                        //this.form.keiroIchiran.Rows[i].Cells["ATESAKI_NAME"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].ATESAKI_NAME;
                        //// 部署
                        //this.form.keiroIchiran.Rows[i].Cells["BUSHO_NAME"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].BUSHO_NAME;
                        //// 送付先備考
                        //this.form.keiroIchiran.Rows[i].Cells["SOUHUSAKI_BIKO"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].SOUHUSAKI_BIKO;

                        // 社員CDが登録されている場合、社員名とメールアドレスをReadOnlyにする。
                        if (!string.IsNullOrEmpty(this.dto.itakuKeiyakuDenshiSouhusaki[i].SHAIN_CD))
                        {
                            this.form.keiroIchiran.Rows.Add();
                            // 削除
                            this.form.keiroIchiran.Rows[i].Cells["chb_delete"].Value = false;
                            // 送付順
                            this.form.keiroIchiran.Rows[i].Cells["YUUSEN_NO"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].PRIORITY_NO;
                            // 会社名
                            this.form.keiroIchiran.Rows[i].Cells["CORP_NAME"].Value = corpName;
                            // 社員CD
                            this.form.keiroIchiran.Rows[i].Cells["SHAIN_CD"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].SHAIN_CD;
                            // 社員名
                            this.form.keiroIchiran.Rows[i].Cells["SHAIN_NAME"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].SOUHU_TANTOUSHA_NAME;
                            // メールアドレス
                            this.form.keiroIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].MAIL_ADDRESS;

                            // 社員CDが登録されている場合、社員名とメールアドレスをReadOnlyにする。
                            this.form.keiroIchiran.Rows[i].Cells["CORP_NAME"].ReadOnly = true;
                            this.form.keiroIchiran.Rows[i].Cells["SHAIN_NAME"].ReadOnly = true;
                            this.form.keiroIchiran.Rows[i].Cells["MAIL_ADDRESS"].ReadOnly = true;
                        }
                        else
                        {
                            this.form.keiroIchiran2.Rows.Add();
                            // 削除
                            this.form.keiroIchiran2.Rows[j].Cells["chb_delete_keiyakusaki"].Value = false;
                            // 送付順
                            this.form.keiroIchiran2.Rows[j].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value = j + 1;
                            // 会社名
                            this.form.keiroIchiran2.Rows[j].Cells["KEIYAKUSAKI_CORP_NAME"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].KEIYAKUSAKI_CORP_NAME;
                            // 社員名
                            this.form.keiroIchiran2.Rows[j].Cells["KEIYAKUSAKI_SHAIN_NAME"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].SOUHU_TANTOUSHA_NAME;
                            // メールアドレス
                            this.form.keiroIchiran2.Rows[j].Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value = this.dto.itakuKeiyakuDenshiSouhusaki[i].MAIL_ADDRESS;

                            j++;
                        }
                    }
                }
            }

            // 共通の排出事業場が入力済であれば、基本排出現場は表示のみとする
            if (!string.IsNullOrWhiteSpace(this.form.HAISHUTSU_JIGYOUJOU_CD.Text))
            {
                this.form.listKihonHstGenba.ReadOnly = true;
            }
            // 更新種別による制御
            catchErr = this.KoushinShubetsuTextChanged();
            if (catchErr)
            {
                throw new Exception("");
            }
            // 契約日付状態による制御
            catchErr = this.CheckKeiyakuDate();
            if (catchErr)
            {
                throw new Exception("");
            }

            // タブ制御
            //switch (this.form.ITAKU_KEIYAKU_SHURUI.Text)
            //{
            //    case "1":
            //        this.form.tabItakuKeiyakuData.TabPages[3].Visible = false;
            //        this.form.tabItakuKeiyakuData.TabPages[4].Visible = false;
            //        this.form.tabItakuKeiyakuData.TabPages[5].Visible = true;
            //        this.form.tabItakuKeiyakuData.TabPages[6].Visible = true;
            //        break;
            //    case "2":
            //        this.form.tabItakuKeiyakuData.TabPages[3].Visible = true;
            //        this.form.tabItakuKeiyakuData.TabPages[4].Visible = true;
            //        this.form.tabItakuKeiyakuData.TabPages[5].Visible = false;
            //        this.form.tabItakuKeiyakuData.TabPages[6].Visible = false;
            //        break;
            //    default:
            //        this.form.tabItakuKeiyakuData.TabPages[3].Visible = true;
            //        this.form.tabItakuKeiyakuData.TabPages[4].Visible = true;
            //        this.form.tabItakuKeiyakuData.TabPages[5].Visible = true;
            //        this.form.tabItakuKeiyakuData.TabPages[6].Visible = true;
            //        break;
            //}
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        /// <param name="control">一覧コントロール</param>
        /// <param name="dt">データテーブル</param>
        internal void SetIchiran(GcCustomMultiRow control, DataTable dt)
        {
            var table = dt;

            this.RemoveCheckMethod(control);
            if (table != null)
            {
                control.DataSource = null;
                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                control.DataSource = table;
            }
        }

        /// <summary>
        /// 生成されているセルのFocusOutチェックメソッドをクリア
        /// </summary>
        /// <param name="list"></param>
        private void RemoveCheckMethod(GcCustomMultiRow list)
        {
            foreach (Row row in list.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    if (cell.GetType() == typeof(GcCustomTextBoxCell)
                        || cell.GetType() == typeof(GcCustomAlphaNumTextBoxCell)
                        || cell.GetType() == typeof(GcCustomNumericTextBox2Cell))
                    {
                        ((GcCustomTextBoxCell)cell).FocusOutCheckMethod = null;
                    }
                }
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void AllControlLock(bool isBool)
        {
            this.form.ITAKU_KEIYAKU_FILE_PATH.ReadOnly = isBool;        // 委託契約書
            //this.form.SYSTEM_ID.ReadOnly = isBool;                      // システムID
            this.form.ITAKU_KEIYAKU_NO.ReadOnly = isBool;               // 委託契約NO
            //this.form.SHIHARAI_HOUHOU.ReadOnly = isBool;                // 支払方法
            this.form.listKihonHstGenba.ReadOnly = isBool;              // 排出現場一覧
            this.form.listHinmei.ReadOnly = isBool;                  // 別紙1排出一覧
            this.form.listHoukokushoBunrui.ReadOnly = isBool;                // 別紙1予定一覧
            this.form.listBetsu2.ReadOnly = isBool;                     // 別紙2一覧
            this.form.listBetsu3.ReadOnly = isBool;                     // 別紙3一覧
            this.form.listBetsu4.ReadOnly = isBool;                     // 別紙4一覧
            this.form.listOboe.ReadOnly = isBool;                       // 覚書一覧
            this.form.listTsumikae.ReadOnly = isBool;                   // 積替保管タブ
            this.form.UPNKYOKA_GYOUSHA_CD.ReadOnly = isBool;            // (運搬許可証紐付管理)業者CD
            this.form.UPNKYOKA_GYOUSHA_SEARCH_BUTTON.Enabled = !isBool;
            this.form.UPNKYOKA_CHIIKI_CD.ReadOnly = isBool;             // (運搬許可証紐付管理)地域CD
            this.form.UPNKYOKA_CHIIKI_SEARCH_BUTTON.Enabled = !isBool;
            this.form.UPNKYOKA_KBN.ReadOnly = isBool;                   // (運搬許可証紐付管理)行政許可区分
            this.form.UPNKYOKA_NO.ReadOnly = isBool;                    // (運搬許可証紐付管理)許可番号
            this.form.listUpnKyokasho.ReadOnly = isBool;                // 運搬許可証一覧
            this.form.SBNKYOKA_GYOUSHA_CD.ReadOnly = isBool;            // (処分許可証紐付管理)業者CD
            this.form.SBNKYOKA_GYOUSHA_SEARCH_BUTTON.Enabled = !isBool;
            this.form.SBNKYOKA_GENBA_CD.ReadOnly = isBool;              // (処分許可証紐付管理)現場CD
            this.form.SBNKYOKA_GENBA_SEARCH_BUTTON.Enabled = !isBool;
            this.form.SBNKYOKA_CHIIKI_CD.ReadOnly = isBool;             // (処分許可証紐付管理)地域CD
            this.form.SBNKYOKA_CHIIKI_SEARCH_BUTTON.Enabled = !isBool;
            this.form.SBNKYOKA_KBN.ReadOnly = isBool;                   // (処分許可証紐付管理)行政許可区分
            this.form.SBNKYOKA_NO.ReadOnly = isBool;                    // (処分許可証紐付管理)許可番号
            this.form.listSbnKyokasho.ReadOnly = isBool;                // 処分許可証一覧

            this.form.HAISHUTSU_JIGYOUSHA_CD.Enabled = !isBool;         // 排出事業者CD
            this.form.HAISHUTSU_JIGYOUSHA_SEARCH_BUTTON.Enabled = !isBool;
            this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = !isBool;         // 排出事業場CD
            this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = !isBool;
            //this.form.HST_FREE_COMMENT.Visible = !isBool;
            this.form.HST_FREE_COMMENT.Enabled = !isBool;
            this.form.btnFileRef.Enabled = !isBool;                     // 委託契約書参照ボタン
            //this.form.btnBrowse.Enabled = !isBool;                      // 委託契約書閲覧ボタン
            //this.form.SYSTEM_ID.Enabled = !isBool;                      // システムID
            this.form.KEIYAKUSHO_KEIYAKU_DATE.Enabled = !isBool;        // 契約日
            this.form.KEIYAKUSHO_CREATE_DATE.Enabled = !isBool;         // 作成日
            this.form.KEIYAKUSHO_SEND_DATE.Enabled = !isBool;           // 送付日
            this.form.KEIYAKUSHO_RETURN_DATE.Enabled = !isBool;         // 返送日
            this.form.KEIYAKUSHO_END_DATE.Enabled = !isBool;            // 保管日
            this.form.KOBETSU_SHITEI_CHECK.Enabled = !isBool;           // 個別指定
            this.form.KOUSHIN_SHUBETSU.ReadOnly = isBool;               // 更新種別
            this.form.KOUSHIN_SHUBETSU_1.Enabled = !isBool;             // 更新種別(1)
            this.form.KOUSHIN_SHUBETSU_2.Enabled = !isBool;             // 更新種別(2)
            this.form.YUUKOU_BEGIN.Enabled = !isBool;                   // 有効期限開始
            this.form.YUUKOU_END.Enabled = !isBool;                     // 有効期限終了
            this.form.KOUSHIN_END_DATE.Enabled = !isBool;               // 自動更新終了日
            this.form.BIKOU1.ReadOnly = isBool;                         // 備考1
            this.form.BIKOU2.ReadOnly = isBool;                         // 備考2

            this.form.btnGetSbnPtn.Enabled = !isBool;                   // (処分場)中間処分場パターン呼出し
            this.form.btnSetSbnPtn.Enabled = !isBool;                   // (処分場)中間処分場パターン登録
            this.form.SHOBUN_PATTERN_SYSTEM_ID.Enabled = !isBool;       // 中間処分パターンシステムID
            this.form.SHOBUN_PATTERN_SEQ.Enabled = !isBool;             // 中間処分パターンSEQ
            //this.form.SHOBUN_PATTERN_NAME.Enabled = !isBool;            // 中間処分パターン名
            this.form.btnGetLastSbnPtn.Enabled = !isBool;               // (最終処分場)最終処分場パターン呼出し
            this.form.btnSetLastSbnPtn.Enabled = !isBool;               // (最終処分場)最終処分場パターン登録
            this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Enabled = !isBool;  // 最終処分パターンシステムID
            this.form.LAST_SHOBUN_PATTERN_SEQ.Enabled = !isBool;        // 最終処分パターンSEQ
            //this.form.LAST_SHOBUN_PATTERN_NAME.Enabled = !isBool;       // 最終処分パターン名
            this.form.UPNKYOKA_GYOUSHA_CD.Enabled = !isBool;            // (運搬許可証紐付管理)業者CD
            this.form.UPNKYOKA_CHIIKI_CD.Enabled = !isBool;             // (運搬許可証紐付管理)地域CD
            this.form.UPNKYOKA_BEGIN.Enabled = !isBool;                 // (運搬許可証紐付管理)期限開始
            this.form.UPNKYOKA_END.Enabled = !isBool;                   // (運搬許可証紐付管理)期限開始
            this.form.btnUpnSearch.Enabled = !isBool;                   // (運搬許可証紐付管理)検索ボタン
            this.form.btnUpnDust.Enabled = !isBool;                     // (運搬許可証紐付管理)ゴミ箱
            this.form.SBNKYOKA_GYOUSHA_CD.Enabled = !isBool;            // (処分許可証紐付管理)業者CD
            this.form.SBNKYOKA_GENBA_CD.Enabled = !isBool;              // (処分許可証紐付管理)現場CD
            this.form.SBNKYOKA_CHIIKI_CD.Enabled = !isBool;             // (処分許可証紐付管理)地域CD
            this.form.SBNKYOKA_BEGIN.Enabled = !isBool;                 // (処分許可証紐付管理)期限開始
            this.form.SBNKYOKA_END.Enabled = !isBool;                   // (処分許可証紐付管理)期限開始
            this.form.btnSbnSearch.Enabled = !isBool;                   // (運搬許可証紐付管理)検索ボタン
            this.form.btnSbnDust.Enabled = !isBool;                     // (運搬許可証紐付管理)ゴミ箱

            // 電子契約タブ
            this.form.ACCESS_CD.Enabled = !isBool;
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Enabled = !isBool;
            this.form.keiroIchiran.ReadOnly = isBool;
            this.form.SHANAI_KEIRO.Enabled = !isBool;
            this.form.SHANAI_KEIRO_1.Enabled = !isBool;
            this.form.SHANAI_KEIRO_2.Enabled = !isBool;
            this.form.keiroIchiran2.ReadOnly = isBool;
            this.form.btnUpRow.Enabled = !isBool;
            this.form.btnDownRow.Enabled = !isBool;
            if (isBool)
            {
                // 電子契約タブの社員CD Validatingイベント削除
                this.form.keiroIchiran.CellValidating -= new DataGridViewCellValidatingEventHandler(this.form.Ichiran_CellValidating);
                this.form.keiroIchiran2.CellValidating -= new DataGridViewCellValidatingEventHandler(this.form.IchiranKeiyakusaki_CellValidating);
            }

            // ファイルタブ
            this.form.btnBrowse.Enabled = !isBool;
            this.form.btnUpload.Enabled = !isBool;
            this.form.customDataGridView1.Columns["BTN_PREVIEW"].ReadOnly = isBool;
            if (isBool)
            {
                this.form.customDataGridView1.CellClick -= new DataGridViewCellEventHandler(this.form.customDataGridView1_CellClick);
            }
        }

        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public bool Saiban()
        {
            try
            {
                // 委託契約基本マスタの最大値+1を取得
                ItakuKeiyakuKihonMasterAccess itakuKeiyakuKihonMasterAccess = new ItakuKeiyakuKihonMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keySystemID = -1;

                var keyItakuKeiyakuKihonSaibanFlag = itakuKeiyakuKihonMasterAccess.IsOverCDLimit(out keySystemID);
                if (keyItakuKeiyakuKihonSaibanFlag)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.SYSTEM_ID.Text = "";
                }

                // 取得した値を使用する
                if (keySystemID < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.SYSTEM_ID.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.SYSTEM_ID.Text = String.Format("{0:D" + this.form.SYSTEM_ID.MaxLength + "}", keySystemID);

                    // ファイルアップロード用にシステムIDを保持
                    this.systemIdForUpload = this.form.SYSTEM_ID.Text;

                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Saiban", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool Cancel(BusinessBaseForm parentForm)
        {
            try
            {
                this.form.tabItakuKeiyakuData.SelectedTab = this.form.tabPage1;
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規モードの場合は空画面を表示する
                    bool catchErr = this.WindowInitNewMode(parentForm);
                    if (catchErr)
                    {
                        return true;
                    }
                }
                else
                {
                    // 委託契約登録画面表示時のシステムIDで再検索・再表示
                    this.errorCancelFlg = false;
                    this.SetWindowData();
                    this.errorCancelFlg = true;
                    this.isError = true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("Cancel", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        public bool ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                FormManager.OpenFormWithAuth("M012", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.ITAKU_KEIYAKUSHO);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.dto.ItakuKeiyakuKihon.SYSTEM_ID = this.SystemId;
            this.dto.ItakuKeiyakuKihon = this.kihonDao.GetDataBySystemId(this.dto.ItakuKeiyakuKihon);

            int count = this.dto.ItakuKeiyakuKihon == null ? 0 : 1;

            // 情報が存在する場合のみ明細情報の取得を行う
            if (count != 0)
            {
                // 委託契約書種類の保持
                this.KeiyakuShurui = this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI.ToString();

                // 委託契約基本TAB1---排出事業場の検索
                M_ITAKU_KEIYAKU_KIHON_HST_GENBA kihonHstGenbaSearchSetting = new M_ITAKU_KEIYAKU_KIHON_HST_GENBA();
                kihonHstGenbaSearchSetting.SYSTEM_ID = this.SystemId;
                this.kihonHstGenbaTable = this.kihonHstGenbaDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_KIHON_HST_GENBA_DATA_SQL, kihonHstGenbaSearchSetting);

                // 委託契約TAB2---品名の検索
                M_ITAKU_KEIYAKU_HINMEI hinmeiSearchSetting = new M_ITAKU_KEIYAKU_HINMEI();
                hinmeiSearchSetting.SYSTEM_ID = this.SystemId;
                this.hinmeiTable = this.hinmeiDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_HIMEI_DATA_SQL, hinmeiSearchSetting);

                // 委託契約別表1（予定）の検索(報告書分類)
                /*M_ITAKU_KEIYAKU_BETSU1_YOTEI betsu1YoteiSearchSetting = new M_ITAKU_KEIYAKU_BETSU1_YOTEI();
                betsu1YoteiSearchSetting.SYSTEM_ID = this.SystemId;
                this.betsu1YoteiTable = this.betsu1YoteiDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_BETSU1_YOTEI_DATA_SQL, betsu1YoteiSearchSetting);*/

                // 委託契約 (報告書分類)の検索
                M_ITAKU_KEIYAKU_BETSU1_HST betsu1HstSearchSetting = new M_ITAKU_KEIYAKU_BETSU1_HST();
                betsu1HstSearchSetting.SYSTEM_ID = this.SystemId;
                this.betsu1HstTable = this.betsu1HstDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_BETSU1_HST_DATA_SQL, betsu1HstSearchSetting);

                // 委託契約別表2の検索(運搬)
                M_ITAKU_KEIYAKU_BETSU2 betsu2SearchSetting = new M_ITAKU_KEIYAKU_BETSU2();
                betsu2SearchSetting.SYSTEM_ID = this.SystemId;
                this.betsu2Table = this.betsu2Dao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_BETSU2_DATA_SQL, betsu2SearchSetting);

                // 委託契約積替の検索
                M_ITAKU_KEIYAKU_TSUMIKAE tsumikaeSearchSetting = new M_ITAKU_KEIYAKU_TSUMIKAE();
                tsumikaeSearchSetting.SYSTEM_ID = this.SystemId;
                this.tsumikaeTable = this.tsumikaeDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_TSUMIKAE_DATA_SQL, tsumikaeSearchSetting);
                foreach (DataColumn col in this.tsumikaeTable.Columns)
                {
                    col.ReadOnly = false;
                }
                foreach (DataRow row in this.tsumikaeTable.Rows)
                {
                    if (row["KONGOU"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["KONGOU"].ToString()))
                    {
                        row["KONGOU_NAME"] = this.GetKongouName((Int16)row["KONGOU"]);
                    }
                }

                // 委託契約別表3の検索(処分)
                M_ITAKU_KEIYAKU_BETSU3 betsu3SearchSetting = new M_ITAKU_KEIYAKU_BETSU3();
                betsu3SearchSetting.SYSTEM_ID = this.SystemId;
                this.betsu3Table = this.betsu3Dao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_BETSU3_DATA_SQL, betsu3SearchSetting);

                // 委託契約別表4の検索(最終)
                M_ITAKU_KEIYAKU_BETSU4 betsu4SearchSetting = new M_ITAKU_KEIYAKU_BETSU4();
                betsu4SearchSetting.SYSTEM_ID = this.SystemId;
                this.betsu4Table = this.betsu4Dao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_BETSU4_DATA_SQL, betsu4SearchSetting);

                // 委託契約別表覚書の検索
                M_ITAKU_KEIYAKU_OBOE oboeSearchSetting = new M_ITAKU_KEIYAKU_OBOE();
                oboeSearchSetting.SYSTEM_ID = this.SystemId;
                this.oboeTable = this.oboeDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_OBOE_DATA_SQL, oboeSearchSetting);

                // 委託契約運搬許可証紐付の検索
                M_ITAKU_UPN_KYOKASHO upnKyokashoSearchSetting = new M_ITAKU_UPN_KYOKASHO();
                upnKyokashoSearchSetting.SYSTEM_ID = this.SystemId;
                this.upnKyokashoTable = this.upnKyokashoDao.GetDataBySqlFile(GET_ITAKU_UPN_KYOKASHO_DATA_SQL, upnKyokashoSearchSetting);

                // 委託契約処分許可証紐付の検索
                M_ITAKU_SBN_KYOKASHO sbnKyokashoSearchSetting = new M_ITAKU_SBN_KYOKASHO();
                sbnKyokashoSearchSetting.SYSTEM_ID = this.SystemId;
                this.sbnKyokashoTable = this.sbnKyokashoDao.GetDataBySqlFile(GET_ITAKU_SBN_KYOKASHO_DATA_SQL, sbnKyokashoSearchSetting);

                // 委託契約電子送付先の検索
                this.dto.itakuKeiyakuDenshiSouhusaki = this.denshiKeiyakuSouhusakiDao.GetDataByCd(this.SystemId);

            }
            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20151014 委託契約書入力(M001)システムエラー発生 Start
                //          TIME_STAMP設定部分修正
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.dto.ItakuKeiyakuKihon == null ||
                    this.dto.ItakuKeiyakuKihon.TIME_STAMP == null || this.dto.ItakuKeiyakuKihon.TIME_STAMP.Length != 8)
                {
                    this.dto.ItakuKeiyakuKihon = new M_ITAKU_KEIYAKU_KIHON();
                }
                else
                {
                    //this.dto.ItakuKeiyakuKihon = new M_ITAKU_KEIYAKU_KIHON() { TIME_STAMP = this.dto.ItakuKeiyakuKihon.TIME_STAMP };
                    this.dto.ItakuKeiyakuKihon = this.kihonDao.GetDataBySystemId(this.dto.ItakuKeiyakuKihon);
                }

                // 20151014 委託契約書入力(M001)システムエラー発生 End
                this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_TYPE = 1;

                // 現在の入力内容でEntity作成
                this.dto.ItakuKeiyakuKihon.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_STATUS = short.Parse(this.form.ITAKU_KEIYAKU_STATUS.Text);
                if (!String.IsNullOrWhiteSpace(this.form.ITAKU_KEIYAKU_SHURUI.Text))
                {
                    this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI = short.Parse(this.form.ITAKU_KEIYAKU_SHURUI.Text);
                }
                this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD = this.form.HAISHUTSU_JIGYOUSHA_CD.Text;
                this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD = this.form.HAISHUTSU_JIGYOUJOU_CD.Text;
                this.dto.ItakuKeiyakuKihon.HST_FREE_COMMENT = this.form.HST_FREE_COMMENT.Text;
                this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_FILE_PATH = this.form.ITAKU_KEIYAKU_FILE_PATH.Text;
                if (!String.IsNullOrWhiteSpace(this.form.KOUSHIN_SHUBETSU.Text))
                {
                    this.dto.ItakuKeiyakuKihon.KOUSHIN_SHUBETSU = short.Parse(this.form.KOUSHIN_SHUBETSU.Text);
                }
                if (this.form.KOUSHIN_END_DATE.Value != null)
                {
                    this.dto.ItakuKeiyakuKihon.KOUSHIN_END_DATE = (DateTime)this.form.KOUSHIN_END_DATE.Value;
                }
                if (this.form.YUUKOU_BEGIN.Value != null)
                {
                    this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN = (DateTime)this.form.YUUKOU_BEGIN.Value;
                }
                if (this.form.YUUKOU_END.Value != null)
                {
                    this.dto.ItakuKeiyakuKihon.YUUKOU_END = (DateTime)this.form.YUUKOU_END.Value;
                }
                //this.dto.ItakuKeiyakuKihon.SHIHARAI_HOUHOU = this.form.SHIHARAI_HOUHOU.Text;
                if (this.form.KEIYAKUSHO_CREATE_DATE.Value != null)
                {
                    this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_CREATE_DATE = (DateTime)this.form.KEIYAKUSHO_CREATE_DATE.Value;
                }

                //画面＝基本マスタ
                if (this.form.dispTourokuHouhou.Equals(ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON))
                {
                    if (this.form.KEIYAKUSHO_KEIYAKU_DATE.Value == null)
                    {
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE = SqlDateTime.Null;
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_SEND_DATE = SqlDateTime.Null;
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_RETURN_DATE = SqlDateTime.Null;
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_END_DATE = SqlDateTime.Null;
                    }
                    else
                    {

                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE = (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value;
                        if ( (DateTime)this.form.KEIYAKUSHO_CREATE_DATE.Value >  (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value)
                        {
                            this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_SEND_DATE = (DateTime)this.form.KEIYAKUSHO_CREATE_DATE.Value;
                            this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_RETURN_DATE = (DateTime)this.form.KEIYAKUSHO_CREATE_DATE.Value;
                            this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_END_DATE = (DateTime)this.form.KEIYAKUSHO_CREATE_DATE.Value;
                        }
                        else if((DateTime)this.form.KEIYAKUSHO_CREATE_DATE.Value <=  (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value)
                        {
                            this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_SEND_DATE = (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value;
                            this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_RETURN_DATE = (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value;
                            this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_END_DATE = (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value;
                        }
                    }

                }
                //画面＝詳細マスタ
                else if (this.form.dispTourokuHouhou.Equals(ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI))
                {
                    if (this.form.KEIYAKUSHO_SEND_DATE.Value != null)
                    {
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_SEND_DATE = (DateTime)this.form.KEIYAKUSHO_SEND_DATE.Value;
                    }
                    if (this.form.KEIYAKUSHO_RETURN_DATE.Value != null)
                    {
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_RETURN_DATE = (DateTime)this.form.KEIYAKUSHO_RETURN_DATE.Value;
                    }
                    if (this.form.KEIYAKUSHO_END_DATE.Value != null)
                    {
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_END_DATE = (DateTime)this.form.KEIYAKUSHO_END_DATE.Value;
                    }

                    if (this.form.KEIYAKUSHO_KEIYAKU_DATE.Value == null)
                    {
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE = SqlDateTime.Null;
                    }
                    else
                    {
                        this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE = (DateTime)this.form.KEIYAKUSHO_KEIYAKU_DATE.Value;
                    }
                }

                //委託契約登録方法
                this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_TOUROKU_HOUHOU = this.form.dispTourokuHouhou;
                
                this.dto.ItakuKeiyakuKihon.KOBETSU_SHITEI_CHECK = this.form.KOBETSU_SHITEI_CHECK.Checked;
                this.dto.ItakuKeiyakuKihon.BIKOU1 = this.form.BIKOU1.Text;
                this.dto.ItakuKeiyakuKihon.BIKOU2 = this.form.BIKOU2.Text;
                this.dto.ItakuKeiyakuKihon.DELETE_FLG = false;

                // 登録方法＝詳細の時のみ、更新する項目（基本の時はDtoの値を維持）
                if (this.form.dispTourokuHouhou == ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI)
                {
                    // パターン名設定
                    Int64 tempPetternSysId = -1;
                    Int32 tempPatternSeq = -1;

                    if (!String.IsNullOrWhiteSpace(this.form.SHOBUN_PATTERN_SYSTEM_ID.Text)
                        && Int64.TryParse(this.form.SHOBUN_PATTERN_SYSTEM_ID.Text, out tempPetternSysId))
                    {
                        this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_SYSTEM_ID = tempPetternSysId;
                    }

                    if (!String.IsNullOrWhiteSpace(this.form.SHOBUN_PATTERN_SEQ.Text)
                        && Int32.TryParse(this.form.SHOBUN_PATTERN_SEQ.Text, out tempPatternSeq))
                    {
                        this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_SEQ = tempPatternSeq;
                    }

                    this.dto.ItakuKeiyakuKihon.SHOBUN_PATTERN_NAME = this.form.SHOBUN_PATTERN_NAME.Text;

                    if (!String.IsNullOrWhiteSpace(this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text)
                        && Int64.TryParse(this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text, out tempPetternSysId))
                    {
                        this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_SYSTEM_ID = tempPetternSysId;
                    }

                    if (!String.IsNullOrWhiteSpace(this.form.LAST_SHOBUN_PATTERN_SEQ.Text)
                        && Int32.TryParse(this.form.LAST_SHOBUN_PATTERN_SEQ.Text, out tempPatternSeq))
                    {
                        this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_SEQ = tempPatternSeq;
                    }

                    this.dto.ItakuKeiyakuKihon.LAST_SHOBUN_PATTERN_NAME = this.form.LAST_SHOBUN_PATTERN_NAME.Text;
                }

                // 更新者情報設定
                var dataBinderLogicItakuKeiyakuKihon = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_KIHON>(this.dto.ItakuKeiyakuKihon);
                dataBinderLogicItakuKeiyakuKihon.SetSystemProperty(this.dto.ItakuKeiyakuKihon, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.dto.ItakuKeiyakuKihon);

                // 一覧インデックス記録
                int idx;

                // 一覧設定(委託契約基本排出現場)
                List<M_ITAKU_KEIYAKU_KIHON_HST_GENBA> itakuKihonHstGenba = new List<M_ITAKU_KEIYAKU_KIHON_HST_GENBA>();
                idx = 0;
                foreach (Row dr in this.form.listKihonHstGenba.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()))
                    {
                        // 新規行、排出事業場CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_KIHON_HST_GENBA temp = new M_ITAKU_KEIYAKU_KIHON_HST_GENBA();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.HAISHUTSU_JIGYOUSHA_CD = this.form.HAISHUTSU_JIGYOUSHA_CD.Text;
                    temp.HAISHUTSU_JIGYOUJOU_CD = dr.Cells["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString();
                    temp.HAISHUTSU_JIGYOUJOU_NAME = dr.Cells["GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value.ToString();
                    temp.HAISHUTSU_JIGYOUJOU_ADDRESS1 = dr.Cells["GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value.ToString();
                    temp.HAISHUTSU_JIGYOUJOU_ADDRESS2 = dr.Cells["GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2"].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_KIHON_HST_GENBA>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuKihonHstGenba.Add(temp);
                }
                this.dto.itakuKeiyakuKihonHstGenba = new M_ITAKU_KEIYAKU_KIHON_HST_GENBA[itakuKihonHstGenba.Count];
                this.dto.itakuKeiyakuKihonHstGenba = itakuKihonHstGenba.ToArray<M_ITAKU_KEIYAKU_KIHON_HST_GENBA>();

                // 一覧設定(委託契約--品名)
                List<M_ITAKU_KEIYAKU_HINMEI> itakuHinmei = new List<M_ITAKU_KEIYAKU_HINMEI>();
                idx = 0;
                foreach (Row dr in this.form.listHinmei.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["HINMEI_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["HINMEI_CD"].Value.ToString()))
                    {
                        // 新規行、報告書分類CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_HINMEI temp = new M_ITAKU_KEIYAKU_HINMEI();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.HINMEI_CD = dr.Cells["HINMEI_CD"].Value.ToString();
                    temp.HINMEI_NAME = dr.Cells["HINMEI_NAME"].Value.ToString();
                    temp.TSUMIKAE = false;
                    if (dr.Cells["TSUMIKAE"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["TSUMIKAE"].Value.ToString()))
                    {
                        temp.TSUMIKAE = (bool)dr.Cells["TSUMIKAE"].Value;
                    }
                    temp.YUNYU = false;
                    if (dr.Cells["YUNYU"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["YUNYU"].Value.ToString()))
                    {
                        temp.YUNYU = (bool)dr.Cells["YUNYU"].Value;
                    }
                    if (dr.Cells["BUNSEKISHOUMEISHO_TEIJIJIKI"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["BUNSEKISHOUMEISHO_TEIJIJIKI"].Value.ToString()))
                    {
                        temp.BUNSEKISHOUMEISHO_TEIJIJIKI = dr.Cells["BUNSEKISHOUMEISHO_TEIJIJIKI"].Value.ToString();
                    }
                    if (dr.Cells["UNPAN_YOTEI_SUU"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["UNPAN_YOTEI_SUU"].Value.ToString()))
                    {
                        temp.UNPAN_YOTEI_SUU = Convert.ToDecimal(dr.Cells["UNPAN_YOTEI_SUU"].Value);
                    }
                    if (dr.Cells["UNPAN_YOTEI_SUU_UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["UNPAN_YOTEI_SUU_UNIT_CD"].Value.ToString()))
                    {
                        temp.UNPAN_YOTEI_SUU_UNIT_CD = Convert.ToInt16(dr.Cells["UNPAN_YOTEI_SUU_UNIT_CD"].Value);
                    }
                    if (dr.Cells["UNPAN_ITAKU_TANKA"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["UNPAN_ITAKU_TANKA"].Value.ToString()))
                    {
                        temp.UNPAN_ITAKU_TANKA = Convert.ToDecimal(dr.Cells["UNPAN_ITAKU_TANKA"].Value);
                    }
                    if (dr.Cells["SHOBUN_YOTEI_SUU"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["SHOBUN_YOTEI_SUU"].Value.ToString()))
                    {
                        temp.SHOBUN_YOTEI_SUU = Convert.ToDecimal(dr.Cells["SHOBUN_YOTEI_SUU"].Value);
                    }
                    if (dr.Cells["SHOBUN_YOTEI_SUU_UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["SHOBUN_YOTEI_SUU_UNIT_CD"].Value.ToString()))
                    {
                        temp.SHOBUN_YOTEI_SUU_UNIT_CD = Convert.ToInt16(dr.Cells["SHOBUN_YOTEI_SUU_UNIT_CD"].Value);
                    }
                    if (dr.Cells["SHOBUN_ITAKU_TANKA"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["SHOBUN_ITAKU_TANKA"].Value.ToString()))
                    {
                        temp.SHOBUN_ITAKU_TANKA = Convert.ToDecimal(dr.Cells["SHOBUN_ITAKU_TANKA"].Value);
                    }
                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_HINMEI>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuHinmei.Add(temp);
                }
                this.dto.itakuKeiyakuHinmei = new M_ITAKU_KEIYAKU_HINMEI[itakuHinmei.Count];
                this.dto.itakuKeiyakuHinmei = itakuHinmei.ToArray<M_ITAKU_KEIYAKU_HINMEI>();

                // 一覧設定(委託契約--報告書分類)
                //List<M_ITAKU_KEIYAKU_BETSU1_YOTEI> itakuBetsu1Yotei = new List<M_ITAKU_KEIYAKU_BETSU1_YOTEI>();
                List<M_ITAKU_KEIYAKU_BETSU1_HST> itakuBetsu1Hst = new List<M_ITAKU_KEIYAKU_BETSU1_HST>();
                idx = 0;
                foreach (Row dr in this.form.listHoukokushoBunrui.Rows)
                {
                    if (dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value.ToString()))
                    {
                        // 新規行、分類CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_BETSU1_HST temp = new M_ITAKU_KEIYAKU_BETSU1_HST();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    //temp.HAISHUTSU_JIGYOUSHA_CD = this.form.HAISHUTSU_JIGYOUSHA_CD.Text;
                    //temp.HAISHUTSU_JIGYOUJOU_CD = dr.Cells["YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString();
                    //temp.HAISHUTSU_JIGYOUJOU_NAME = dr.Cells["YOTEI_HAISHUTSU_JIGYOUJOU_NAME"].Value.ToString();
                    //temp.HAISHUTSU_JIGYOUJOU_ADDRESS = dr.Cells["YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS"].Value.ToString();
                    temp.HOUKOKUSHO_BUNRUI_CD = dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value.ToString();
                    temp.HOUKOKUSHO_BUNRUI_NAME = dr.Cells["HOUKOKUSHO_BUNRUI_NAME"].Value.ToString();
                    /*if (dr.Cells["YOTEI_SUU"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["YOTEI_SUU"].Value.ToString()))
                    {
                        temp.YOTEI_SUU = Double.Parse(dr.Cells["YOTEI_SUU"].Value.ToString());
                    }
                    if (dr.Cells["YOTEI_SUU_UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["YOTEI_SUU_UNIT_CD"].Value.ToString()))
                    {
                        temp.YOTEI_SUU_UNIT_CD = short.Parse(dr.Cells["YOTEI_SUU_UNIT_CD"].Value.ToString());
                    }
                    if (dr.Cells["YOTEI_KIKAN"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["YOTEI_KIKAN"].Value.ToString()))
                    {
                        temp.YOTEI_KIKAN = short.Parse(dr.Cells["YOTEI_KIKAN"].Value.ToString());
                    }
                    if (dr.Cells["YOTEI_ITAKU_TANKA"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["YOTEI_ITAKU_TANKA"].Value.ToString()))
                    {
                        temp.ITAKU_TANKA = Decimal.Parse(dr.Cells["YOTEI_ITAKU_TANKA"].Value.ToString());
                    }
                    if (!temp.YOTEI_SUU.IsNull && !temp.ITAKU_TANKA.IsNull)
                    {
                        temp.ITAKU_RYOUKIN = temp.YOTEI_SUU.ToSqlDecimal().Value * temp.ITAKU_TANKA.Value;
                    }
                    if (!temp.YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        temp.ITAKU_RYOUKIN_UNIT_CD = temp.YOTEI_SUU_UNIT_CD;
                    }*/

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_BETSU1_HST>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuBetsu1Hst.Add(temp);
                }
                this.dto.itakuKeiyakuBetsu1Hst = new M_ITAKU_KEIYAKU_BETSU1_HST[itakuBetsu1Hst.Count];
                this.dto.itakuKeiyakuBetsu1Hst = itakuBetsu1Hst.ToArray<M_ITAKU_KEIYAKU_BETSU1_HST>();

                // 一覧設定(委託契約--運搬)
                List<M_ITAKU_KEIYAKU_BETSU2> itakuBetsu2 = new List<M_ITAKU_KEIYAKU_BETSU2>();
                idx = 0;
                foreach (Row dr in this.form.listBetsu2.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["UNPAN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // 新規行、運搬業者CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_BETSU2 temp = new M_ITAKU_KEIYAKU_BETSU2();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.UNPAN_GYOUSHA_CD = dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                    temp.UNPAN_GYOUSHA_NAME = dr.Cells["UNPAN_GYOUSHA_NAME"].Value.ToString();
                    temp.UNPAN_GYOUSHA_ADDRESS1 = dr.Cells["UNPAN_GYOUSHA_ADDRESS1"].Value.ToString();
                    temp.UNPAN_GYOUSHA_ADDRESS2 = dr.Cells["UNPAN_GYOUSHA_ADDRESS2"].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_BETSU2>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuBetsu2.Add(temp);
                }
                this.dto.itakuKeiyakuBetsu2 = new M_ITAKU_KEIYAKU_BETSU2[itakuBetsu2.Count];
                this.dto.itakuKeiyakuBetsu2 = itakuBetsu2.ToArray<M_ITAKU_KEIYAKU_BETSU2>();

                // 一覧設定(委託契約--積替)
                List<M_ITAKU_KEIYAKU_TSUMIKAE> itakuTsumikae = new List<M_ITAKU_KEIYAKU_TSUMIKAE>();
                idx = 0;
                foreach (Row dr in this.form.listTsumikae.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["UNPAN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // 新規行、運搬業者CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_TSUMIKAE temp = new M_ITAKU_KEIYAKU_TSUMIKAE();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.UNPAN_GYOUSHA_CD = dr.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                    temp.UNPAN_GYOUSHA_NAME = dr.Cells["UNPAN_GYOUSHA_NAME"].Value.ToString();
                    temp.TSUMIKAE_HOKANBA_CD = dr.Cells["TSUMIKAE_HOKANBA_CD"].Value.ToString();
                    temp.TSUMIKAE_HOKANBA_NAME = dr.Cells["TSUMIKAE_HOKANBA_NAME"].Value.ToString();
                    temp.TSUMIKAE_HOKANBA_ADDRESS1 = dr.Cells["TSUMIKAE_HOKANBA_ADDRESS1"].Value.ToString();
                    temp.TSUMIKAE_HOKANBA_ADDRESS2 = dr.Cells["TSUMIKAE_HOKANBA_ADDRESS2"].Value.ToString();
                    if (dr.Cells["HOKAN_JOGEN"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["HOKAN_JOGEN"].Value.ToString()))
                    {
                        temp.HOKAN_JOGEN = Convert.ToDecimal(dr.Cells["HOKAN_JOGEN"].Value.ToString());
                    }
                    else
                    {
                        temp.HOKAN_JOGEN = SqlDecimal.Null;
                    }
                    if (dr.Cells["HOKAN_JOGEN_UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["HOKAN_JOGEN_UNIT_CD"].Value.ToString()))
                    {
                        temp.HOKAN_JOGEN_UNIT_CD = Convert.ToInt16(dr.Cells["HOKAN_JOGEN_UNIT_CD"].Value.ToString());
                    }
                    else
                    {
                        temp.HOKAN_JOGEN_UNIT_CD = SqlInt16.Null;
                    }
                    if (dr.Cells["KONGOU"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["KONGOU"].Value.ToString()))
                    {
                        temp.KONGOU = Convert.ToInt16(dr.Cells["KONGOU"].Value.ToString());
                    }
                    else
                    {
                        temp.KONGOU = SqlInt16.Null;
                    }

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_TSUMIKAE>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuTsumikae.Add(temp);
                }
                this.dto.itakuKeiyakuTsumikae = new M_ITAKU_KEIYAKU_TSUMIKAE[itakuTsumikae.Count];
                this.dto.itakuKeiyakuTsumikae = itakuTsumikae.ToArray<M_ITAKU_KEIYAKU_TSUMIKAE>();

                // 一覧設定(委託契約--処分)
                List<M_ITAKU_KEIYAKU_BETSU3> itakuBetsu3 = new List<M_ITAKU_KEIYAKU_BETSU3>();
                idx = 0;
                foreach (Row dr in this.form.listBetsu3.Rows)
                {
                    if (dr.IsNewRow ||
                        (this.form.ITAKU_KEIYAKU_SHURUI.Text == "1" && (string.IsNullOrEmpty(Convert.ToString(dr.Cells["SHOBUN_GYOUSHA_CD"].Value)) || string.IsNullOrEmpty(Convert.ToString(dr.Cells["SHOBUN_JIGYOUJOU_CD"].Value)))) ||
                        ((this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" || this.form.ITAKU_KEIYAKU_SHURUI.Text == "3") && string.IsNullOrEmpty(Convert.ToString(dr.Cells["SHOBUN_JIGYOUJOU_CD"].Value))))
                    {
                        // 新規行、処分業者CD、処分事業場CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_BETSU3 temp = new M_ITAKU_KEIYAKU_BETSU3();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.SHOBUN_GYOUSHA_CD = dr.Cells["SHOBUN_GYOUSHA_CD"].Value.ToString();
                    temp.SHOBUN_GYOUSHA_NAME = dr.Cells["SHOBUN_GYOUSHA_NAME"].Value.ToString();
                    temp.SHOBUN_GYOUSHA_ADDRESS1 = dr.Cells["SHOBUN_GYOUSHA_ADDRESS1"].Value.ToString();
                    temp.SHOBUN_GYOUSHA_ADDRESS2 = dr.Cells["SHOBUN_GYOUSHA_ADDRESS2"].Value.ToString();
                    temp.SHOBUN_JIGYOUJOU_CD = dr.Cells["SHOBUN_JIGYOUJOU_CD"].Value.ToString();
                    temp.SHOBUN_JIGYOUJOU_NAME = dr.Cells["SHOBUN_JIGYOUJOU_NAME"].Value.ToString();
                    temp.SHOBUN_JIGYOUJOU_ADDRESS1 = dr.Cells["SHOBUN_JIGYOUJOU_ADDRESS1"].Value.ToString();
                    temp.SHOBUN_JIGYOUJOU_ADDRESS2 = dr.Cells["SHOBUN_JIGYOUJOU_ADDRESS2"].Value.ToString();
                    temp.SHOBUN_HOUHOU_CD = dr.Cells["SHOBUN_HOUHOU_CD"].Value.ToString();
                    temp.SHISETSU_CAPACITY = dr.Cells["SHISETSU_CAPACITY"].Value.ToString();
                    /*if (dr.Cells["HOKAN_JOGEN"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["HOKAN_JOGEN"].Value.ToString()))
                    {
                        temp.HOKAN_JOGEN = float.Parse(dr.Cells["HOKAN_JOGEN"].Value.ToString());
                    }
                    if (dr.Cells["HOKAN_JOGEN_UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["HOKAN_JOGEN_UNIT_CD"].Value.ToString()))
                    {
                        temp.HOKAN_JOGEN_UNIT_CD = (Int16)dr.Cells["HOKAN_JOGEN_UNIT_CD"].Value;
                    }
                    if (dr.Cells["UNPAN_FROM"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["UNPAN_FROM"].Value.ToString()))
                    {
                        temp.UNPAN_FROM = (Int16)dr.Cells["UNPAN_FROM"].Value;
                    }
                    if (dr.Cells["UNPAN_END"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["UNPAN_END"].Value.ToString()))
                    {
                        temp.UNPAN_END = (Int16)dr.Cells["UNPAN_END"].Value;
                    }
                    if (dr.Cells["KONGOU"].Value != null && !string.IsNullOrWhiteSpace(dr.Cells["KONGOU"].Value.ToString()))
                    {
                        temp.KONGOU = (Int16)dr.Cells["KONGOU"].Value;
                    }*/

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_BETSU3>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuBetsu3.Add(temp);
                }
                this.dto.itakuKeiyakuBetsu3 = new M_ITAKU_KEIYAKU_BETSU3[itakuBetsu3.Count];
                this.dto.itakuKeiyakuBetsu3 = itakuBetsu3.ToArray<M_ITAKU_KEIYAKU_BETSU3>();

                // 一覧設定(委託契約--最終)
                List<M_ITAKU_KEIYAKU_BETSU4> itakuBetsu4 = new List<M_ITAKU_KEIYAKU_BETSU4>();
                idx = 0;
                foreach (Row dr in this.form.listBetsu4.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["LAST_SHOBUN_GYOUSHA_CD"].Value == null || dr.Cells["LAST_SHOBUN_JIGYOUJOU_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["LAST_SHOBUN_GYOUSHA_CD"].Value.ToString()) || string.IsNullOrWhiteSpace(dr.Cells["LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                    {
                        // 新規行、処分業者CD、処分事業場CDがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_BETSU4 temp = new M_ITAKU_KEIYAKU_BETSU4();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.LAST_SHOBUN_GYOUSHA_CD = dr.Cells["LAST_SHOBUN_GYOUSHA_CD"].Value.ToString();
                    temp.LAST_SHOBUN_GYOUSHA_NAME = dr.Cells["LAST_SHOBUN_GYOUSHA_NAME"].Value.ToString();
                    temp.LAST_SHOBUN_GYOUSHA_ADDRESS1 = dr.Cells["LAST_SHOBUN_GYOUSHA_ADDRESS1"].Value.ToString();
                    temp.LAST_SHOBUN_GYOUSHA_ADDRESS2 = dr.Cells["LAST_SHOBUN_GYOUSHA_ADDRESS2"].Value.ToString();
                    temp.LAST_SHOBUN_JIGYOUJOU_CD = dr.Cells["LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString();
                    temp.LAST_SHOBUN_JIGYOUJOU_NAME = dr.Cells["LAST_SHOBUN_JIGYOUJOU_NAME"].Value.ToString();
                    temp.LAST_SHOBUN_JIGYOUJOU_ADDRESS1 = dr.Cells["LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value.ToString();
                    temp.LAST_SHOBUN_JIGYOUJOU_ADDRESS2 = dr.Cells["LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value.ToString();
                    temp.SHOBUN_HOUHOU_CD = dr.Cells["SHOBUN_HOUHOU_CD"].Value.ToString();
                    temp.SHORI_SPEC = dr.Cells["SHORI_SPEC"].Value.ToString();
                    //temp.OTHER = dr.Cells["OTHER"].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_BETSU4>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuBetsu4.Add(temp);
                }
                this.dto.itakuKeiyakuBetsu4 = new M_ITAKU_KEIYAKU_BETSU4[itakuBetsu4.Count];
                this.dto.itakuKeiyakuBetsu4 = itakuBetsu4.ToArray<M_ITAKU_KEIYAKU_BETSU4>();

                // 一覧設定(委託契約覚書)
                List<M_ITAKU_KEIYAKU_OBOE> itakuOboe = new List<M_ITAKU_KEIYAKU_OBOE>();
                idx = 0;
                foreach (Row dr in this.form.listOboe.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["UPDATE_DATE"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["UPDATE_DATE"].Value.ToString()))
                    {
                        // 新規行、更新日がnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_KEIYAKU_OBOE temp = new M_ITAKU_KEIYAKU_OBOE();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.MEMO = dr.Cells["MEMO"].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_OBOE>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);

                    // dbLogic.SetSystemPropertyの後に設定しないとシステム日付で更新される
                    temp.UPDATE_DATE = (DateTime)dr.Cells["UPDATE_DATE"].Value;

                    itakuOboe.Add(temp);
                }
                this.dto.itakuKeiyakuOboe = new M_ITAKU_KEIYAKU_OBOE[itakuOboe.Count];
                this.dto.itakuKeiyakuOboe = itakuOboe.ToArray<M_ITAKU_KEIYAKU_OBOE>();

                // 一覧設定(委託運搬許可証紐付)
                List<M_ITAKU_UPN_KYOKASHO> itakuUpnKyoka = new List<M_ITAKU_UPN_KYOKASHO>();
                idx = 0;
                foreach (Row dr in this.form.listUpnKyokasho.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["UPNKYOKA_LINK"].Value == null || !(bool)dr.Cells["UPNKYOKA_LINK"].Value)
                    {
                        // 新規行、チェックがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_UPN_KYOKASHO temp = new M_ITAKU_UPN_KYOKASHO();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.GYOUSHA_CD = dr.Cells["UPNKYOKA_GYOUSHA_CD"].Value.ToString();
                    temp.GENBA_CD = dr.Cells["UPNKYOKA_GENBA_CD"].Value.ToString();
                    temp.CHIIKI_CD = dr.Cells["UPNKYOKA_CHIIKI_CD"].Value.ToString();
                    temp.KYOKA_KBN = short.Parse(dr.Cells["UPNKYOKA_KYOKA_KBN"].Value.ToString());
                    temp.KYOKA_NO = dr.Cells["UPNKYOKA_KYOKA_NO"].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_UPN_KYOKASHO>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuUpnKyoka.Add(temp);
                }
                this.dto.itakuUpnKyokasho = new M_ITAKU_UPN_KYOKASHO[itakuUpnKyoka.Count];
                this.dto.itakuUpnKyokasho = itakuUpnKyoka.ToArray<M_ITAKU_UPN_KYOKASHO>();

                // 一覧設定(委託処分許可証紐付)
                List<M_ITAKU_SBN_KYOKASHO> itakuSbnKyoka = new List<M_ITAKU_SBN_KYOKASHO>();
                idx = 0;
                foreach (Row dr in this.form.listSbnKyokasho.Rows)
                {
                    if (dr.IsNewRow || dr.Cells["SBNKYOKA_LINK"].Value == null || !(bool)dr.Cells["SBNKYOKA_LINK"].Value)
                    {
                        // 新規行、チェックがnullの場合は飛ばす
                        continue;
                    }
                    idx++;

                    M_ITAKU_SBN_KYOKASHO temp = new M_ITAKU_SBN_KYOKASHO();
                    temp.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    temp.SEQ = idx;
                    temp.ITAKU_KEIYAKU_NO = this.form.ITAKU_KEIYAKU_NO.Text;
                    temp.GYOUSHA_CD = dr.Cells["SBNKYOKA_GYOUSHA_CD"].Value.ToString();
                    temp.GENBA_CD = dr.Cells["SBNKYOKA_GENBA_CD"].Value.ToString();
                    temp.CHIIKI_CD = dr.Cells["SBNKYOKA_CHIIKI_CD"].Value.ToString();
                    temp.KYOKA_KBN = short.Parse(dr.Cells["SBNKYOKA_KYOKA_KBN"].Value.ToString());
                    temp.KYOKA_NO = dr.Cells["SBNKYOKA_KYOKA_NO"].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_SBN_KYOKASHO>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    itakuSbnKyoka.Add(temp);
                }
                this.dto.itakuSbnKyokasho = new M_ITAKU_SBN_KYOKASHO[itakuSbnKyoka.Count];
                this.dto.itakuSbnKyokasho = itakuSbnKyoka.ToArray<M_ITAKU_SBN_KYOKASHO>();

                // 新規モードでない、且つ画面の登録方法＝基本の時、非表示タブのデータを再取得
                if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && this.form.dispTourokuHouhou ==ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON)
                {
                    this.dto.itakuKeiyakuKihonHstGenba = this.kihonHstGenbaDao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuKeiyakuHinmei = this.hinmeiDao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuKeiyakuBetsu2 = this.betsu2Dao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuKeiyakuTsumikae = this.tsumikaeDao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuKeiyakuBetsu3 = this.betsu3Dao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuKeiyakuBetsu4 = this.betsu4Dao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuUpnKyokasho = this.upnKyokashoDao.GetDataBySystemId(this.SystemId);
                    this.dto.itakuSbnKyokasho = this.sbnKyokashoDao.GetDataBySystemId(this.SystemId);
                }

                // 電子契約
                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    List<M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI> itakuDenshiSouhusaki = new List<M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI>();
                    idx = 0;
                    foreach (DataGridViewRow dr in this.form.keiroIchiran.Rows)
                    {
                        if (dr.IsNewRow || this.IsAllNullOrEmpty(dr)
                            || (dr.Cells["chb_delete"].Value != null && bool.Parse(dr.Cells["chb_delete"].Value.ToString())))
                        {
                            // 新規行、有効行ではない、削除がONの行の場合は飛ばす
                            continue;
                        }
                        idx++;

                        M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI souhusaki = new M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI();
                        souhusaki.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                        souhusaki.SEQ = idx;
                        souhusaki.ACCESS_CD = this.form.ACCESS_CD.Text;
                        if (!string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
                        {
                            souhusaki.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
                        }
                        if (!string.IsNullOrEmpty(this.form.SHANAI_KEIRO.Text))
                        {
                            souhusaki.DENSHI_KEIYAKU_SHANAI_KEIRO = SqlInt16.Parse(this.form.SHANAI_KEIRO.Text);
                        }
                        if (dr.Cells["YUUSEN_NO"].Value != null)
                        {
                            souhusaki.PRIORITY_NO = SqlInt16.Parse(dr.Cells["YUUSEN_NO"].Value.ToString());
                        }
                        if (dr.Cells["SHAIN_CD"].Value != null)
                        {
                            souhusaki.SHAIN_CD = dr.Cells["SHAIN_CD"].Value.ToString();
                        }
                        if (dr.Cells["SHAIN_NAME"].Value != null)
                        {
                            souhusaki.SOUHU_TANTOUSHA_NAME = dr.Cells["SHAIN_NAME"].Value.ToString();
                        }
                        if (dr.Cells["MAIL_ADDRESS"].Value != null)
                        {
                            souhusaki.MAIL_ADDRESS = dr.Cells["MAIL_ADDRESS"].Value.ToString();
                        }
                        //if (dr.Cells["TEL_NO"].Value != null)
                        //{
                        //    souhusaki.TEL_NO = dr.Cells["TEL_NO"].Value.ToString();
                        //}
                        if (dr.Cells["CORP_NAME"].Value != null)
                        {
                            souhusaki.ATESAKI_NAME = dr.Cells["CORP_NAME"].Value.ToString();
                        }
                        //if (dr.Cells["BUSHO_NAME"].Value != null)
                        //{
                        //    souhusaki.BUSHO_NAME = dr.Cells["BUSHO_NAME"].Value.ToString();
                        //}
                        //if (dr.Cells["SOUHUSAKI_BIKO"].Value != null)
                        //{
                        //    souhusaki.SOUHUSAKI_BIKO = dr.Cells["SOUHUSAKI_BIKO"].Value.ToString();
                        //}

                        // 更新者情報設定
                        var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI>(souhusaki);
                        dbLogic.SetSystemProperty(souhusaki, false);
                        MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), souhusaki);
                        itakuDenshiSouhusaki.Add(souhusaki);
                    }

                    // 社外契約先送付一覧
                    string shanaiCount = idx.ToString();
                    foreach (DataGridViewRow dr in this.form.keiroIchiran2.Rows)
                    {
                        if (dr.IsNewRow || this.IsAllNullOrEmptyKeiyakusaki(dr)
                            || (dr.Cells["chb_delete_keiyakusaki"].Value != null && bool.Parse(dr.Cells["chb_delete_keiyakusaki"].Value.ToString())))
                        {
                            // 新規行、有効行ではない、削除がONの行の場合は飛ばす
                            continue;
                        }
                        idx++;

                        M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI souhusaki = new M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI();
                        souhusaki.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                        souhusaki.SEQ = idx;
                        souhusaki.ACCESS_CD = this.form.ACCESS_CD.Text;
                        if (!string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
                        {
                            souhusaki.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
                        }
                        if (!string.IsNullOrEmpty(this.form.SHANAI_KEIRO.Text))
                        {
                            souhusaki.DENSHI_KEIYAKU_SHANAI_KEIRO = SqlInt16.Parse(this.form.SHANAI_KEIRO.Text);
                        }
                        if (dr.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value != null)
                        {
                            souhusaki.PRIORITY_NO = SqlInt16.Parse(dr.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()) + SqlInt16.Parse(shanaiCount);
                        }
                        if (dr.Cells["KEIYAKUSAKI_CORP_NAME"].Value != null)
                        {
                            souhusaki.KEIYAKUSAKI_CORP_NAME = dr.Cells["KEIYAKUSAKI_CORP_NAME"].Value.ToString();
                        }
                        if (dr.Cells["KEIYAKUSAKI_SHAIN_NAME"].Value != null)
                        {
                            souhusaki.SOUHU_TANTOUSHA_NAME = dr.Cells["KEIYAKUSAKI_SHAIN_NAME"].Value.ToString();
                        }
                        if (dr.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value != null)
                        {
                            souhusaki.MAIL_ADDRESS = dr.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value.ToString();
                        }

                        // 更新者情報設定
                        var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI>(souhusaki);
                        dbLogic.SetSystemProperty(souhusaki, false);
                        MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), souhusaki);
                        itakuDenshiSouhusaki.Add(souhusaki);
                    }

                    // 経路一覧に入力情報がない場合
                    if (itakuDenshiSouhusaki.Count == 0)
                    {
                        // アクセスコード、もしくは社内経路名CDに入力がある場合には、登録する。
                        if (!string.IsNullOrEmpty(this.form.ACCESS_CD.Text)
                            || string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
                        {
                            M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI souhusaki = new M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI();
                            souhusaki.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                            souhusaki.SEQ = 1;
                            if (!string.IsNullOrEmpty(this.form.ACCESS_CD.Text))
                            {
                                souhusaki.ACCESS_CD = this.form.ACCESS_CD.Text;
                            }
                            if (!string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
                            {
                                souhusaki.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = SqlInt16.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
                            }
                            if (!string.IsNullOrEmpty(this.form.SHANAI_KEIRO.Text))
                            {
                                souhusaki.DENSHI_KEIYAKU_SHANAI_KEIRO = SqlInt16.Parse(this.form.SHANAI_KEIRO.Text);
                            }
                            // 更新者情報設定
                            var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI>(souhusaki);
                            dbLogic.SetSystemProperty(souhusaki, false);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), souhusaki);
                            itakuDenshiSouhusaki.Add(souhusaki);
                        }
                    }

                    this.dto.itakuKeiyakuDenshiSouhusaki = new M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[itakuDenshiSouhusaki.Count];
                    this.dto.itakuKeiyakuDenshiSouhusaki = itakuDenshiSouhusaki.ToArray<M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI>();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputCd">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.SYSTEM_ID.GetType().GetProperty(ItakuKeiyakuHoshuConstans.CHARACTERS_NUMBER);
            decimal charNumber = (decimal)pi.GetValue(this.form.SYSTEM_ID, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// システムID重複チェック
        /// </summary>
        /// <returns></returns>
        public ItakuKeiyakuHoshuConstans.SystemIdLeaveResult DupliCheckSystemId(string zeroPadCd)
        {
            LogUtility.DebugMethodStart();

            // 委託契約基本マスタ検索
            M_ITAKU_KEIYAKU_KIHON kihonSearch = new M_ITAKU_KEIYAKU_KIHON();
            kihonSearch.SYSTEM_ID = zeroPadCd;
            DataTable kihonTable =
                this.kihonDao.GetDataBySqlFile(GET_INPUTCD_DATA_ITAKUKEIYAKU_KIHON_SQL, kihonSearch);

            // 重複チェック
            ItakuKeiyakuHoshuValidator vali = new ItakuKeiyakuHoshuValidator();
            DialogResult resultDialog = new DialogResult();
            bool resultDupli = vali.SystemIDValidator(kihonTable, out resultDialog);

            ItakuKeiyakuHoshuConstans.SystemIdLeaveResult ViewUpdateWindow = 0;

            // 重複あり かつ [はい]ボタン選択時
            if (!resultDupli && resultDialog == DialogResult.Yes)
            {
                ViewUpdateWindow = ItakuKeiyakuHoshuConstans.SystemIdLeaveResult.FALSE_ON;
            }
            else if (!resultDupli && resultDialog == DialogResult.No)
            {
                ViewUpdateWindow = ItakuKeiyakuHoshuConstans.SystemIdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli)
            {
                ViewUpdateWindow = ItakuKeiyakuHoshuConstans.SystemIdLeaveResult.FALSE_NONE;
            }
            else
            {
                ViewUpdateWindow = ItakuKeiyakuHoshuConstans.SystemIdLeaveResult.TURE_NONE;
            }

            LogUtility.DebugMethodEnd();

            return ViewUpdateWindow;
        }

        /// <summary>
        /// 事業者CDから事業者略称、住所を設定する
        /// </summary>
        public bool SetJigyoushaData(string gyoushaCD, object itemName, object itemAddress1, object itemAddress2, object itemTodoufuken)
        {
            try
            {
                LogUtility.DebugMethodStart(gyoushaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken);

                // 業者マスタデータを取得する
                M_GYOUSHA gyousha = this.gyoushaDao.GetDataByCd(gyoushaCD);
                M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                if (gyousha != null && !gyousha.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    todoufuken = this.todoufukenDao.GetDataByCd(gyousha.GYOUSHA_TODOUFUKEN_CD.ToString());
                }

                // 略称をセットする
                if (gyousha != null && itemName != null)
                {
                    if (itemName.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemName).Value = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemName).Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 都道府県をセットする
                if (todoufuken != null && itemTodoufuken != null)
                {
                    if (itemTodoufuken.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemTodoufuken).Value = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemTodoufuken).Text = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }
                }

                // 住所1をセットする
                if (gyousha != null && itemAddress1 != null)
                {
                    if (itemAddress1.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemAddress1).Value = gyousha.GYOUSHA_ADDRESS1;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemAddress1).Text = gyousha.GYOUSHA_ADDRESS1;
                    }
                }

                // 住所2をセットする
                if (gyousha != null && itemAddress2 != null)
                {
                    if (itemAddress2.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemAddress2).Value = gyousha.GYOUSHA_ADDRESS2;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemAddress2).Text = gyousha.GYOUSHA_ADDRESS2;
                    }
                }

                LogUtility.DebugMethodEnd(gyoushaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetJigyoushaData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(gyoushaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetJigyoushaData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(gyoushaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken);
                return true;
            }
        }

        /// <summary>
        /// 現場CDから現場略称、住所を設定する
        /// </summary>
        public bool SetJigyoujouData(string gyoushaCD, string genbaCD, object itemName, object itemAddress1, object itemAddress2, object itemTodoufuken, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(gyoushaCD, genbaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken);

                bool ret = false;
                catchErr = false;

                // 業者マスタデータを取得する
                M_GENBA queryParam = new M_GENBA();
                queryParam.GYOUSHA_CD = gyoushaCD;
                queryParam.GENBA_CD = genbaCD;
                M_GENBA result = this.genbaDao.GetDataByCd(queryParam);

                M_GENBA genba = null;
                if (result != null)
                {
                    ret = true;
                    genba = result;
                }

                // 略称をセットする
                if (ret && itemName != null)
                {
                    if (itemName.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemName).Value = genba.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemName).Text = genba.GENBA_NAME_RYAKU;
                    }
                }

                if (ret)
                {
                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (!genba.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(genba.GENBA_TODOUFUKEN_CD.ToString());
                    }
                    //都道府県をセットする
                    if (todoufuken != null && itemTodoufuken != null)
                    {
                        if (itemTodoufuken.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                        {
                            ((r_framework.CustomControl.GcCustomTextBoxCell)itemTodoufuken).Value = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }
                        else
                        {
                            ((r_framework.CustomControl.CustomTextBox)itemTodoufuken).Text = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }
                    }
                }

                // 住所をgenba1
                if (ret && itemAddress1 != null)
                {
                    if (itemAddress1.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemAddress1).Value = genba.GENBA_ADDRESS1;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemAddress1).Text = genba.GENBA_ADDRESS1;
                    }
                }

                // 住所をgenba2
                if (ret && itemAddress2 != null)
                {
                    if (itemAddress2.GetType() == typeof(r_framework.CustomControl.GcCustomTextBoxCell))
                    {
                        ((r_framework.CustomControl.GcCustomTextBoxCell)itemAddress2).Value = genba.GENBA_ADDRESS2;
                    }
                    else
                    {
                        ((r_framework.CustomControl.CustomTextBox)itemAddress2).Text = genba.GENBA_ADDRESS2;
                    }
                }

                LogUtility.DebugMethodEnd(gyoushaCD, genbaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetJigyoujouData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(gyoushaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken, catchErr);
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SetJigyoujouData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(gyoushaCD, itemName, itemAddress1, itemAddress2, itemTodoufuken, catchErr);
                return true;
            }
        }

        /// <summary>
        /// 登録時ユーザーコードチェック処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void RegistUserCheck(object source, r_framework.Event.RegistCheckEventArgs e)
        {
            LogUtility.DebugMethodStart(source, e);

            MessageUtility msgUtil = new MessageUtility();
            if (e.multiRow == null)
            {
                // 通常コントロールのチェック
                //Control ctrl = (Control)source;

                // 排出事業場=nullかつ、排出現場の排出事業場が未入力の場合エラーとする
                /*if (ctrl.Name.Equals("HAISHUTSU_JIGYOUJOU_CD") && string.IsNullOrWhiteSpace(ctrl.Text))
                {
                    bool isInput = false;
                    foreach (Row eRow in this.form.listKihonHstGenba.Rows)
                    {
                        if (eRow["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value != null && !string.IsNullOrWhiteSpace(eRow["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()))
                        {
                            isInput = true;
                        }
                    }
                    if (!isInput)
                    {
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E001").MESSAGE, ItakuKeiyakuHoshu.Properties.Resources.HAISHUTSU_JIGYOUJOU_CD));
                    }
                }*/
            }
            else
            {
                // グリッドセル内のチェック
                var cell = source as GcCustomTextBoxCell;
                if (cell != null)
                {
                    if (this.form.ITAKU_KEIYAKU_SHURUI.Text == "1" && cell.Name.Equals("SHOBUN_GYOUSHA_CD") && cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        var genbaCell = e.multiRow[cell.RowIndex, "SHOBUN_JIGYOUJOU_CD"] as GcCustomTextBoxCell;
                        if (genbaCell.Value == null || string.IsNullOrWhiteSpace(genbaCell.Value.ToString()))
                        {
                            e.errorMessages.Add(string.Format(msgUtil.GetMessage("E001").MESSAGE, genbaCell.DisplayItemName));
                            genbaCell.Style.BackColor = Constans.ERROR_COLOR;
                        }
                        else
                        {
                            genbaCell.Style.BackColor = Constans.NOMAL_COLOR;
                        }
                    }
                    if (cell.Name.Equals("LAST_SHOBUN_GYOUSHA_CD"))
                    {
                        if ((e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value != null && !string.IsNullOrWhiteSpace(e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString()))
                            || (e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value != null && !string.IsNullOrWhiteSpace(e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                            || (e.multiRow[cell.RowIndex, "SHOBUN_HOUHOU_CD"].Value != null && !string.IsNullOrWhiteSpace(e.multiRow[cell.RowIndex, "SHOBUN_HOUHOU_CD"].Value.ToString()))
                            || (e.multiRow[cell.RowIndex, "SHORI_SPEC"].Value != null && !string.IsNullOrWhiteSpace(e.multiRow[cell.RowIndex, "SHORI_SPEC"].Value.ToString())))
                        {
                            if (e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString()))
                            {
                                e.errorMessages.Add(string.Format(msgUtil.GetMessage("E001").MESSAGE, ((ICustomControl)e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"]).DisplayItemName));
                                e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Style.BackColor = Constans.ERROR_COLOR;
                            }
                            else
                            {
                                e.multiRow[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                            }
                            if (e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value == null || string.IsNullOrWhiteSpace(e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                            {
                                e.errorMessages.Add(string.Format(msgUtil.GetMessage("E001").MESSAGE, ((ICustomControl)e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"]).DisplayItemName));
                                e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Style.BackColor = Constans.ERROR_COLOR;
                            }
                            else
                            {
                                e.multiRow[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                            }
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 印刷処理
        /// </summary>
        internal bool Print()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 帳票用データ作成
                var table = new DataTable();

                ///**********************************************************************/
                /// 画面からデータ抽出
                ///**********************************************************************/
                bool catchErr = this.CreateEntity(false);
                if (catchErr)
                {
                    return true;
                }

                DataTable tableSub01 = null;
                DataTable tableSub02 = null;
                DataTable tableSub03 = null;
                DataTable tableSub04 = null;
                DataTable tableSub05 = null;
                DataTable tableSub06 = null;
                DataTable tableSub07 = null;
                DataTable tableSub08 = null;
                DataTable tableSub09 = null;
                DataTable tableSub10 = null;

                var reportName = "R001_02Report";
                var xmlPath = Const.ItakuKeiyakuHoshuConstans.ReportInfoXmlPath02;
                if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_SBN)
                {
                    //処分（産廃）
                    reportName = "R001_02Report";
                    xmlPath = Const.ItakuKeiyakuHoshuConstans.ReportInfoXmlPath02;
                    table = this.CreateReportData();
                    tableSub01 = this.CreateSubReport02Data(false, ref table);          //乙の事業範囲（処分）〔産廃〕
                    tableSub02 = this.CreateSubReport02Data(true, ref table);           //乙の事業範囲（処分）〔特管〕
                    tableSub03 = this.CreateSubReport03Data(0, false, ref table);          //委託する産業廃棄物の種類、数量及び単価（処分）
                    tableSub04 = this.CreateSubReport04Data("", reportName, ref table); //処分の場所、方法及び処理能力
                    tableSub05 = this.CreateSubReport05Data("", ref table);             //最終処分の場所、方法及び処理能力
                    tableSub08 = this.CreateSubReport08Data(false, "", ref table);      //搬入業者〔産廃〕
                    tableSub09 = this.CreateSubReport08Data(true, "", ref table);       //搬入業者〔特管〕
                    tableSub07 = this.CreateSubReport07Data("", ref table);             //適正処理に必要な情報の提供
                }
                else if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_UPN)
                {
                    //収集運搬（産廃）
                    reportName = "R001_01Report";
                    xmlPath = Const.ItakuKeiyakuHoshuConstans.ReportInfoXmlPath01;
                    table = this.CreateReportData();
                    tableSub01 = this.CreateSubReport01Data(false, ref table);          //乙の事業範囲（収集運搬）〔産廃〕
                    tableSub02 = this.CreateSubReport01Data(true, ref table);           //乙の事業範囲（収集運搬）〔特管〕
                    tableSub03 = this.CreateSubReport03Data(0, true, ref table);          //委託する産業廃棄物の種類、数量及び単価（収集運搬）
                    tableSub04 = this.CreateSubReport04Data("", reportName, ref table); //運搬の最終目的地
                    tableSub06 = this.CreateSubReport06Data("", ref table);             //積替保管に記載
                    tableSub07 = this.CreateSubReport07Data("", ref table);             //適正処理に必要な情報
                }
                else if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_UPNSBN)
                {
                    //収集処分（産廃）
                    reportName = "R001_03Report";
                    xmlPath = Const.ItakuKeiyakuHoshuConstans.ReportInfoXmlPath03;
                    table = this.CreateR001_03Data(ref tableSub01, ref tableSub02, ref tableSub03, ref tableSub04, ref tableSub05, ref tableSub06, ref tableSub07, ref tableSub08, ref tableSub09, ref tableSub10);
                }
                if (table != null)
                {
                    // 現在表示されている一覧をレポート情報として生成
                    var reportInfo = new CommonChouhyouPopup.App.ReportInfoBase();
                    var dic = new Dictionary<string, DataTable>();
                    dic.Add(reportName, table);
                    if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_SBN)
                    {
                        dic.Add("R001_sheet01_CTL", tableSub01);
                        dic.Add("R001_sheet02_CTL", tableSub02);
                        dic.Add("R001_sheet03_CTL", tableSub03);
                        dic.Add("R001_sheet06_CTL", tableSub04);
                        dic.Add("R001_sheet05_CTL", tableSub05);
                        dic.Add("R001_sheet04_1_CTL", tableSub08);
                        dic.Add("R001_sheet04_2_CTL", tableSub09);
                        dic.Add("R001_sheet07_CTL", tableSub07);
                    }
                    else if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_UPN)
                    {
                        dic.Add("R001_sheet01_CTL", tableSub01);
                        dic.Add("R001_sheet02_CTL", tableSub02);
                        dic.Add("R001_sheet03_CTL", tableSub03);
                        dic.Add("R001_sheet04_CTL", tableSub04);
                        dic.Add("R001_sheet06_CTL", tableSub06);
                        dic.Add("R001_sheet07_CTL", tableSub07);
                    }
                    else if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_UPNSBN)
                    {
                        dic.Add("R001_sheetA_CTL", tableSub01);
                        dic.Add("R001_sheetB_CTL", tableSub02);
                        dic.Add("R001_sheetC_CTL", tableSub03);
                        dic.Add("R001_sheetD_CTL", tableSub04);
                        dic.Add("R001_sheetE_CTL", tableSub05);
                        dic.Add("R001_sheetF_CTL", tableSub06);
                        dic.Add("R001_sheetG_CTL", tableSub07);
                        dic.Add("R001_sheetH_CTL", tableSub08);
                        dic.Add("R001_sheetI_CTL", tableSub09);
                        dic.Add("R001_sheetJ_CTL", tableSub10);
                    }
                    reportInfo.Create(xmlPath, reportName, dic);

                    // 印刷ポップアップ表示
                    CommonChouhyouPopup.App.FormReportPrintPopup reportPopup = new CommonChouhyouPopup.App.FormReportPrintPopup(reportInfo);
                    reportPopup.ShowDialog();
                    reportPopup.Dispose();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        private DataTable CreateReportData()
        {
            DataTable reportTable = new DataTable();

            ///**********************************************************************/
            /// DBからデータ抽出
            ///**********************************************************************/
            //this.CreateEntity(false);

            // 排出事業者
            var HAISHUTSU_JIGYOUSHA = this.gyoushaDao.GetDataByCd(this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD);
            if (HAISHUTSU_JIGYOUSHA == null)
            {
                HAISHUTSU_JIGYOUSHA = new M_GYOUSHA();
            }
            // 排出事業場
            var HAISHUTSU_JIGYOUJOU = new M_GENBA();
            HAISHUTSU_JIGYOUJOU.GYOUSHA_CD = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD;
            HAISHUTSU_JIGYOUJOU.GENBA_CD = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD;
            HAISHUTSU_JIGYOUJOU = this.genbaDao.GetDataByCd(HAISHUTSU_JIGYOUJOU);
            if (HAISHUTSU_JIGYOUJOU == null)
            {
                HAISHUTSU_JIGYOUJOU = new M_GENBA();
            }

            // 別表1(排出)
            // 輸入廃棄物
            var yunyu = false;
            var yunyuNames = new System.Text.StringBuilder();
            //foreach (var BETSU1HST in this.dto.itakuKeiyakuBetsu1Hst)
            //{
            //    if (BETSU1HST.YUNYU)
            //    {
            //        var HOUKOKUSHO_BUNRUI = houokushoBunruiDao.GetDataByCd(BETSU1HST.HOUKOKUSHO_BUNRUI_CD);
            //        if (HOUKOKUSHO_BUNRUI != null && !string.IsNullOrEmpty(HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME))
            //        {
            //            yunyu = true;
            //            if (yunyuNames.Length > 0)
            //            {
            //                yunyuNames.Append(Const.ItakuKeiyakuHoshuConstans.OUTPUT_SEPARATOR);
            //            }
            //            yunyuNames.Append(HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME);
            //        }
            //    }
            //}
            // itakuKeiyakuHinmei　のうち、YUNYU=ＴｒｕｅのメンバのHINMEI_NAMEを、メンバ毎に "、"で区切って印字する。

            var BUNSEKISHOUMEISHO_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            foreach (var HINMEI in this.dto.itakuKeiyakuHinmei)
            {
                if (HINMEI.YUNYU)
                {
                    yunyu = true;
                    if (yunyuNames.Length > 0)
                    {
                        yunyuNames.Append(Const.ItakuKeiyakuHoshuConstans.OUTPUT_SEPARATOR);
                    }
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei = this.imHinmeiDao.GetDataByCd(HINMEI.HINMEI_CD);

                    yunyuNames.Append(HINMEI.HINMEI_NAME);
                }

                // 分析証明書定時時期
                if (!string.IsNullOrEmpty(HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI))
                {
                    BUNSEKISHOUMEISHO_LIST.Add(HINMEI);
                }
            }

            // 分析証明書定時時期
            //var BUNSEKISHOUMEISHO_LIST = new List<M_ITAKU_KEIYAKU_BETSU1_HST>();
            var HOUKOKUBUNRUI_LIST = new List<M_ITAKU_KEIYAKU_BETSU1_HST>();
            foreach (var BETSU1HST in this.dto.itakuKeiyakuBetsu1Hst)
            {
                if (!string.IsNullOrEmpty(BETSU1HST.HOUKOKUSHO_BUNRUI_CD))
                {
                    HOUKOKUBUNRUI_LIST.Add(BETSU1HST);
                }
            }

            // 別表2(運搬)
            // 最初
            var BETSU2_FST = new M_ITAKU_KEIYAKU_BETSU2();
            if (this.dto.itakuKeiyakuBetsu2.Length > 0)
            {
                BETSU2_FST = this.dto.itakuKeiyakuBetsu2[0];
            }
            var UNPAN_GYOUSHA = this.gyoushaDao.GetDataByCd(BETSU2_FST.UNPAN_GYOUSHA_CD);
            if (UNPAN_GYOUSHA == null)
            {
                UNPAN_GYOUSHA = new M_GYOUSHA();
            }

            // 別表3(処分)
            // 最初
            var BETSU3_FST = new M_ITAKU_KEIYAKU_BETSU3();
            if (this.dto.itakuKeiyakuBetsu3.Length > 0)
            {
                BETSU3_FST = this.dto.itakuKeiyakuBetsu3[0];
            }
            var BETSU3_FST_GYA = this.gyoushaDao.GetDataByCd(BETSU3_FST.SHOBUN_GYOUSHA_CD);
            if (BETSU3_FST_GYA == null)
            {
                BETSU3_FST_GYA = new M_GYOUSHA();
            }

            // 積替保管施設
            int tsumihoCheck = 1;
            var TSUMIHO_GEN = new M_GENBA();
            var TSUMIHO_CHIIKI = new M_CHIIKI();
            var TSUMIHO_KYOKA = new M_CHIIKIBETSU_KYOKA();
            var TSUMIHO_MEIGARA = string.Empty;
            var TSUMIHO_KYOKA_NO = string.Empty;
            var TSUMIHO_KYOKA_KIGEN = string.Empty;
            foreach (var tsumikae in this.dto.itakuKeiyakuTsumikae)
            {
                if (!string.IsNullOrEmpty(tsumikae.UNPAN_GYOUSHA_CD))
                {
                    if (tsumikae.KONGOU == 1)
                    {
                        tsumihoCheck = 2;
                        break;
                    }
                    tsumihoCheck = 3;
                }
            }

            // 収集運搬に関する事業範囲
            var UPN_KYOKASHO1 = new M_ITAKU_UPN_KYOKASHO();
            var UPN_KYOKASHO2 = new M_ITAKU_UPN_KYOKASHO();
            var UPN_KYOKA1 = new M_CHIIKIBETSU_KYOKA();
            var UPN_KYOKA2 = new M_CHIIKIBETSU_KYOKA();
            var UPN_KYOKA_CHIIKI1 = new M_CHIIKI();
            var UPN_KYOKA_CHIIKI2 = new M_CHIIKI();
            for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
            {
                var tempKyokasho = this.dto.itakuUpnKyokasho[i];
                if (tempKyokasho.KYOKA_KBN == 1)
                {
                    UPN_KYOKASHO1 = tempKyokasho;
                    UPN_KYOKA_CHIIKI1 = this.chiikiDao.GetDataByCd(UPN_KYOKASHO1.CHIIKI_CD);
                    UPN_KYOKA1.KYOKA_KBN = Const.ItakuKeiyakuHoshuConstans.KYOKA_KBN_UPN;
                    UPN_KYOKA1.GYOUSHA_CD = UPN_KYOKASHO1.GYOUSHA_CD;
                    UPN_KYOKA1.GENBA_CD = UPN_KYOKASHO1.GENBA_CD;
                    UPN_KYOKA1.CHIIKI_CD = UPN_KYOKASHO1.CHIIKI_CD;
                    UPN_KYOKA1 = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(UPN_KYOKA1);
                }
            }
            for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
            {
                var tempKyokasho = this.dto.itakuUpnKyokasho[i];
                if (tempKyokasho.KYOKA_KBN == 2)
                {
                    UPN_KYOKASHO2 = tempKyokasho;
                    UPN_KYOKA_CHIIKI2 = this.chiikiDao.GetDataByCd(UPN_KYOKASHO2.CHIIKI_CD);
                    UPN_KYOKA2.KYOKA_KBN = Const.ItakuKeiyakuHoshuConstans.KYOKA_KBN_UPN;
                    UPN_KYOKA2.GYOUSHA_CD = UPN_KYOKASHO2.GYOUSHA_CD;
                    UPN_KYOKA2.GENBA_CD = UPN_KYOKASHO2.GENBA_CD;
                    UPN_KYOKA2.CHIIKI_CD = UPN_KYOKASHO2.CHIIKI_CD;
                    UPN_KYOKA2 = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(UPN_KYOKA2);
                }
            }

            // 処分に関する事業範囲
            var SBN_KYOKASHO1 = new M_ITAKU_SBN_KYOKASHO();
            var SBN_KYOKASHO2 = new M_ITAKU_SBN_KYOKASHO();
            var SBN_KYOKA1 = new M_CHIIKIBETSU_KYOKA();
            var SBN_KYOKA2 = new M_CHIIKIBETSU_KYOKA();
            var SBN_KYOKA_CHIIKI1 = new M_CHIIKI();
            var SBN_KYOKA_CHIIKI2 = new M_CHIIKI();
            var dtSBN_KYOKA1 = new DataTable();
            var dtSBN_KYOKA2 = new DataTable();
            var sbnGenbaTemp = new M_GENBA();
            short kyokaKbn;
            for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
            {
                var tempKyokasho = this.dto.itakuSbnKyokasho[i];
                if (tempKyokasho.KYOKA_KBN == 3 || tempKyokasho.KYOKA_KBN == 5)
                {
                    sbnGenbaTemp = new M_GENBA();
                    kyokaKbn = tempKyokasho.KYOKA_KBN == 3 ? Const.ItakuKeiyakuHoshuConstans.KYOKA_KBN_SBN : Const.ItakuKeiyakuHoshuConstans.KYOKA_KBN_LASTSBN;
                    sbnGenbaTemp.GYOUSHA_CD = tempKyokasho.GYOUSHA_CD;
                    sbnGenbaTemp.GENBA_CD = tempKyokasho.GENBA_CD;
                    sbnGenbaTemp.CHIIKI_CD = tempKyokasho.CHIIKI_CD;
                    dtSBN_KYOKA1 = this.GetKyokaByGenba(kyokaKbn, sbnGenbaTemp, ref SBN_KYOKA_CHIIKI1, ref SBN_KYOKA1);
                    if (SBN_KYOKA1 != null)
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
            {
                var tempKyokasho = this.dto.itakuSbnKyokasho[i];
                if (tempKyokasho.KYOKA_KBN == 4 || tempKyokasho.KYOKA_KBN == 6)
                {
                    sbnGenbaTemp = new M_GENBA();
                    kyokaKbn = tempKyokasho.KYOKA_KBN == 4 ? Const.ItakuKeiyakuHoshuConstans.KYOKA_KBN_SBN : Const.ItakuKeiyakuHoshuConstans.KYOKA_KBN_LASTSBN;
                    sbnGenbaTemp.GYOUSHA_CD = tempKyokasho.GYOUSHA_CD;
                    sbnGenbaTemp.GENBA_CD = tempKyokasho.GENBA_CD;
                    sbnGenbaTemp.CHIIKI_CD = tempKyokasho.CHIIKI_CD;
                    dtSBN_KYOKA2 = this.GetKyokaByGenba(kyokaKbn, sbnGenbaTemp, ref SBN_KYOKA_CHIIKI2, ref SBN_KYOKA2);
                    if (SBN_KYOKA2 != null)
                    {
                        break;
                    }
                }
            }

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("HAISHUTSU_JIGYOUSHA1", typeof(string));
            reportTable.Columns.Add("UNPAN_GYOUSHA1", typeof(string));
            reportTable.Columns.Add("HAISHUTSU_JIGYOUJOU1", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_TODOUHUKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_KIGEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_JIGYOU_KUBUN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_HAIKIBUTSU_SYURUI01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_TODOUHUKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_KIGEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_JIGYOU_KUBUN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_HAIKIBUTSU_SYURUI02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_BANGOU02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_03_YUNYUU_NASHI", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_03_YUNYUU_ARI", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_03_YUNYUU_HAIKIBUTSU", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_SHIMEI", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_ADDRESS", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_CHIIKI", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_KYOKA_KIGEN", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_JIGYO_KBN", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_HAIKI_SHURUI", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_KYOKA_JOUKEN", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_KYOKA_NO", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_JIGYOUJOU_NAME", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_JIGYOUJOU_ADDRESS", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_SHOBUN_HOUHOU", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_SHISETSU_CAPCITY", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_NOTE02_LBL", typeof(string));
            reportTable.Columns.Add("ARTICLE02_05_01", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_05_02", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_05_03", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_05_03_HAIKI_SHURUI", typeof(string));
            reportTable.Columns.Add("ARTICLE02_05_03_ADDRESS", typeof(string));
            reportTable.Columns.Add("ARTICLE02_05_03_HOKAN_LIMIT", typeof(string));
            ///積替保管タブ
            reportTable.Columns.Add("ARTICLE02_05_04_NOTE01_LBL", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_NAME", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_ADDRESS", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_TODOUHUKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_TODOUHUKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_KIGEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_KIGEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_JIGYOU_HANI01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_JIGYOU_HANI02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_01_KYOKA_BANGOU02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_02_NOTE01_LBL", typeof(string));
            reportTable.Columns.Add("ARTICLE02_06_03_NOTE01_LBL", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_HAIKIBUTSU_SHURUI01", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_HAIKIBUTSU_SHURUI02", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_HAIKIBUTSU_SHURUI03", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_TEIJI_JIKI01", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_TEIJI_JIKI02", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_TEIJI_JIKI03", typeof(string));
            //分析証明書提示時期
            reportTable.Columns.Add("ARTICLE03_06_NOTE01_LBL", typeof(string));

            reportTable.Columns.Add("ARTICLE14_TAN_FROM", typeof(string));
            reportTable.Columns.Add("ARTICLE14_TAN_TO", typeof(string));
            reportTable.Columns.Add("ARTICLE14_AUTO_FROM", typeof(string));
            reportTable.Columns.Add("ARTICLE14_AUTO_TO", typeof(string));
            reportTable.Columns.Add("ARTICLE14_TAN", typeof(bool));
            reportTable.Columns.Add("ARTICLE14_AUTO", typeof(bool));
            reportTable.Columns.Add("ARTICLE14_AUTO_SPAN", typeof(string));
            reportTable.Columns.Add("CREATE_DATE", typeof(string));
            reportTable.Columns.Add("KOU_ADDRESS", typeof(string));
            reportTable.Columns.Add("KOU_NAME", typeof(string));
            reportTable.Columns.Add("KOU_DAIHYOU_NAME", typeof(string));
            reportTable.Columns.Add("OTSU_ADDRESS", typeof(string));
            reportTable.Columns.Add("OTSU_NAME", typeof(string));
            reportTable.Columns.Add("OTSU_DAIHYOU_NAME", typeof(string));
            ///委託契約書入力改修仕様
            reportTable.Columns.Add("CHIIKI_NAME1", typeof(string));
            reportTable.Columns.Add("CHIIKI_NAME2", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN1", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN2", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI1", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI2", typeof(string));
            reportTable.Columns.Add("HAIKI_SHURUI1", typeof(string));
            reportTable.Columns.Add("HAIKI_SHURUI2", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN1", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN2", typeof(string));
            reportTable.Columns.Add("KYOKA_NO1", typeof(string));
            reportTable.Columns.Add("KYOKA_NO2", typeof(string));
            reportTable.Columns.Add("CHIIKI_NAME3", typeof(string));
            reportTable.Columns.Add("CHIIKI_NAME4", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN3", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN4", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI3", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI4", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN3", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN4", typeof(string));
            reportTable.Columns.Add("KYOKA_NO3", typeof(string));
            reportTable.Columns.Add("KYOKA_NO4", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_NOTE04_LBL", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_NOTE06_LBL", typeof(string));
            //品名タブ
            reportTable.Columns.Add("HINMEI_NAME1", typeof(string));
            reportTable.Columns.Add("HINMEI_NAME2", typeof(string));
            reportTable.Columns.Add("HINMEI_NAME3", typeof(string));
            reportTable.Columns.Add("UNPAN_YOTEI_SUU1", typeof(string));
            reportTable.Columns.Add("UNPAN_YOTEI_SUU2", typeof(string));
            reportTable.Columns.Add("UNPAN_YOTEI_SUU3", typeof(string));
            reportTable.Columns.Add("UNPAN_YOTEI_SUU_UNIT1", typeof(string));
            reportTable.Columns.Add("UNPAN_YOTEI_SUU_UNIT2", typeof(string));
            reportTable.Columns.Add("UNPAN_YOTEI_SUU_UNIT3", typeof(string));
            reportTable.Columns.Add("UNPAN_ITAKU_TANKA1", typeof(string));
            reportTable.Columns.Add("UNPAN_ITAKU_TANKA2", typeof(string));
            reportTable.Columns.Add("UNPAN_ITAKU_TANKA3", typeof(string));
            reportTable.Columns.Add("ARTICLE02_02_NOTE02_LBL", typeof(string));

            //最終処分の場所、方法及び処理能力
            for (int i = 1; i < 8; i++)
            {
                reportTable.Columns.Add("SBN_NO" + i, typeof(string));
                reportTable.Columns.Add("JIGYOJOU_NAME" + i, typeof(string));
                reportTable.Columns.Add("JIGYOJOU_ADDRESS" + i, typeof(string));
                reportTable.Columns.Add("SBN_HOUHOU" + i, typeof(string));
                reportTable.Columns.Add("CAPACITY" + i, typeof(string));
            }
            reportTable.Columns.Add("ARTICLE02_05_02_NOTE01_LBL", typeof(string));

            ///**********************************************************************/
            /// 行データ
            ///**********************************************************************/
            var row = reportTable.NewRow();
            // 甲
            M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
            if (!HAISHUTSU_JIGYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
            {
                todoufuken = this.todoufukenDao.GetDataByCd(HAISHUTSU_JIGYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
            }
            string tdhk = "";
            if (todoufuken != null)
            {
                tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
            }
            row["HAISHUTSU_JIGYOUSHA1"] = this.ConvertTwoText(HAISHUTSU_JIGYOUSHA.GYOUSHA_NAME1, HAISHUTSU_JIGYOUSHA.GYOUSHA_NAME2);
            row["KOU_ADDRESS"] = tdhk + this.ConvertTwoText(HAISHUTSU_JIGYOUSHA.GYOUSHA_ADDRESS1, HAISHUTSU_JIGYOUSHA.GYOUSHA_ADDRESS2);
            row["KOU_NAME"] = this.ConvertTwoText(HAISHUTSU_JIGYOUSHA.GYOUSHA_NAME1, HAISHUTSU_JIGYOUSHA.GYOUSHA_NAME2);
            row["KOU_DAIHYOU_NAME"] = HAISHUTSU_JIGYOUSHA.GYOUSHA_DAIHYOU;
            var gyousha = new M_GYOUSHA();
            // 乙
            if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_SBN)
            {
                gyousha = this.gyoushaDao.GetDataByCd(BETSU3_FST.SHOBUN_GYOUSHA_CD);
                tdhk = "";
                if (gyousha != null && !gyousha.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    todoufuken = this.todoufukenDao.GetDataByCd(gyousha.GYOUSHA_TODOUFUKEN_CD.ToString());
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }
                }
                row["UNPAN_GYOUSHA1"] = this.ConvertTwoText(BETSU3_FST_GYA.GYOUSHA_NAME1, BETSU3_FST_GYA.GYOUSHA_NAME2);
                row["OTSU_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU3_FST.SHOBUN_GYOUSHA_ADDRESS1, BETSU3_FST.SHOBUN_GYOUSHA_ADDRESS2);
                row["OTSU_NAME"] = this.ConvertTwoText(BETSU3_FST_GYA.GYOUSHA_NAME1, BETSU3_FST_GYA.GYOUSHA_NAME2);
                row["OTSU_DAIHYOU_NAME"] = BETSU3_FST_GYA.GYOUSHA_DAIHYOU;
            }
            else if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_UPN)
            {
                gyousha = this.gyoushaDao.GetDataByCd(BETSU2_FST.UNPAN_GYOUSHA_CD);
                tdhk = "";
                if (gyousha != null && !gyousha.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    todoufuken = this.todoufukenDao.GetDataByCd(gyousha.GYOUSHA_TODOUFUKEN_CD.ToString());
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }
                }
                row["UNPAN_GYOUSHA1"] = this.ConvertTwoText(UNPAN_GYOUSHA.GYOUSHA_NAME1, UNPAN_GYOUSHA.GYOUSHA_NAME2);
                row["OTSU_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU2_FST.UNPAN_GYOUSHA_ADDRESS1, BETSU2_FST.UNPAN_GYOUSHA_ADDRESS2);
                row["OTSU_NAME"] = this.ConvertTwoText(UNPAN_GYOUSHA.GYOUSHA_NAME1, UNPAN_GYOUSHA.GYOUSHA_NAME2);
                row["OTSU_DAIHYOU_NAME"] = UNPAN_GYOUSHA.GYOUSHA_DAIHYOU;
            }
            else if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_UPNSBN)
            {
                gyousha = this.gyoushaDao.GetDataByCd(BETSU3_FST_GYA.GYOUSHA_CD);
                tdhk = "";
                if (gyousha != null && !gyousha.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    todoufuken = this.todoufukenDao.GetDataByCd(gyousha.GYOUSHA_TODOUFUKEN_CD.ToString());
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }
                }
                row["UNPAN_GYOUSHA1"] = this.ConvertTwoText(BETSU3_FST_GYA.GYOUSHA_NAME1, BETSU3_FST_GYA.GYOUSHA_NAME2);
                row["OTSU_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU3_FST_GYA.GYOUSHA_ADDRESS1, BETSU3_FST_GYA.GYOUSHA_ADDRESS2);
                row["OTSU_NAME"] = this.ConvertTwoText(BETSU3_FST_GYA.GYOUSHA_NAME1, BETSU3_FST_GYA.GYOUSHA_NAME2);
                row["OTSU_DAIHYOU_NAME"] = BETSU3_FST_GYA.GYOUSHA_DAIHYOU;
            }
            // 処分に関する事業範囲
            var sbnMeigara = string.Empty;
            var sbnNumber = string.Empty;
            var sbnKigen = string.Empty;
            this.GetKyokaDetailFutsu(dtSBN_KYOKA1, out sbnMeigara, out sbnNumber, out sbnKigen);
            row["ARTICLE02_01_KYOKA_TODOUHUKEN01"] = SBN_KYOKA_CHIIKI1.CHIIKI_NAME;
            row["ARTICLE02_01_KYOKA_KIGEN01"] = sbnKigen;
            row["ARTICLE02_01_JIGYOU_KUBUN01"] = this.GetJigyouKbn(SBN_KYOKA1);
            row["ARTICLE02_01_HAIKIBUTSU_SYURUI01"] = sbnMeigara;
            row["ARTICLE02_01_KYOKA_JOKEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
            row["ARTICLE02_01_KYOKA_BANGOU01"] = sbnNumber;
            this.GetKyokaDetailFutsu(dtSBN_KYOKA2, out sbnMeigara, out sbnNumber, out sbnKigen);
            row["ARTICLE02_01_KYOKA_TODOUHUKEN02"] = SBN_KYOKA_CHIIKI2.CHIIKI_NAME;
            row["ARTICLE02_01_KYOKA_KIGEN02"] = sbnKigen;
            row["ARTICLE02_01_JIGYOU_KUBUN02"] = this.GetJigyouKbn(SBN_KYOKA2);
            row["ARTICLE02_01_HAIKIBUTSU_SYURUI02"] = sbnMeigara;
            row["ARTICLE02_01_KYOKA_JOKEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
            row["ARTICLE02_01_KYOKA_BANGOU02"] = sbnNumber;
            //甲の事業場
            if (!string.IsNullOrWhiteSpace(this.dto.ItakuKeiyakuKihon.HST_FREE_COMMENT))
            {
                row["HAISHUTSU_JIGYOUJOU1"] = this.dto.ItakuKeiyakuKihon.HST_FREE_COMMENT;
            }
            else if (!string.IsNullOrWhiteSpace(this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD))
            {
                var HST_GENBA = new M_GENBA();
                HST_GENBA.GYOUSHA_CD = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD;
                HST_GENBA.GENBA_CD = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD;
                HST_GENBA = this.genbaDao.GetDataByCd(HST_GENBA);
                if (HST_GENBA == null)
                {
                    HST_GENBA = new M_GENBA();
                }

                row["HAISHUTSU_JIGYOUJOU1"] = this.ConvertTwoText(HST_GENBA.GENBA_NAME1, HST_GENBA.GENBA_NAME2);
            }
            else
            {
                row["HAISHUTSU_JIGYOUJOU1"] = string.Empty;
            }
            row["ARTICLE02_03_YUNYUU_NASHI"] = !yunyu;
            row["ARTICLE02_03_YUNYUU_ARI"] = yunyu;
            row["ARTICLE02_03_YUNYUU_HAIKIBUTSU"] = yunyuNames.ToString();
            row["ARTICLE02_04_SHIMEI"] = string.Empty;
            row["ARTICLE02_04_ADDRESS"] = string.Empty;
            row["ARTICLE02_04_CHIIKI"] = string.Empty;
            row["ARTICLE02_04_KYOKA_KIGEN"] = string.Empty;
            row["ARTICLE02_04_JIGYO_KBN"] = string.Empty;
            row["ARTICLE02_04_HAIKI_SHURUI"] = string.Empty;
            row["ARTICLE02_04_KYOKA_JOUKEN"] = string.Empty;
            row["ARTICLE02_04_KYOKA_NO"] = string.Empty;
            row["ARTICLE02_04_JIGYOUJOU_NAME"] = string.Empty;
            row["ARTICLE02_04_JIGYOUJOU_ADDRESS"] = string.Empty;
            row["ARTICLE02_04_SHOBUN_HOUHOU"] = string.Empty;
            row["ARTICLE02_04_SHISETSU_CAPCITY"] = string.Empty;
            row["ARTICLE02_04_NOTE02_LBL"] = string.Empty;
            row["ARTICLE02_05_01"] = tsumihoCheck == 1;
            row["ARTICLE02_05_02"] = tsumihoCheck == 2;
            row["ARTICLE02_05_03"] = tsumihoCheck == 3;
            row["ARTICLE02_05_03_HAIKI_SHURUI"] = string.Empty;
            row["ARTICLE02_05_03_ADDRESS"] = string.Empty;
            row["ARTICLE02_05_03_HOKAN_LIMIT"] = string.Empty;
            row["ARTICLE02_05_04_NOTE01_LBL"] = string.Empty;

            // 搬入業者
            row["ARTICLE02_06_01_NAME"] = string.Empty;
            row["ARTICLE02_06_01_ADDRESS"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_TODOUHUKEN01"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_KIGEN01"] = string.Empty;
            row["ARTICLE02_06_01_JIGYOU_HANI01"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_JOKEN01"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_BANGOU01"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_TODOUHUKEN02"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_KIGEN02"] = string.Empty;
            row["ARTICLE02_06_01_JIGYOU_HANI02"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_JOKEN02"] = string.Empty;
            row["ARTICLE02_06_01_KYOKA_BANGOU02"] = string.Empty;
            // 分析証明書定時
            row["ARTICLE03_05_HAIKIBUTSU_SHURUI01"] = string.Empty;
            row["ARTICLE03_05_HAIKIBUTSU_SHURUI02"] = string.Empty;
            row["ARTICLE03_05_HAIKIBUTSU_SHURUI03"] = string.Empty;
            //分析証明書提示時期
            row["ARTICLE03_06_NOTE01_LBL"] = string.Empty;

            //M_HOUKOKUSHO_BUNRUI houkokushoTemp;
            if (BUNSEKISHOUMEISHO_LIST.Count < 4)
            {
                M_HINMEI himeiTemp;
                if (BUNSEKISHOUMEISHO_LIST.Count > 0)
                {
                    row["ARTICLE03_05_TEIJI_JIKI01"] = BUNSEKISHOUMEISHO_LIST[0].BUNSEKISHOUMEISHO_TEIJIJIKI;
                    /*houkokushoTemp = this.houokushoBunruiDao.GetDataByCd(BUNSEKISHOUMEISHO_LIST[0].HOUKOKUSHO_BUNRUI_CD);
                    if (houkokushoTemp != null)
                    {
                        eRow["ARTICLE03_05_HAIKIBUTSU_SHURUI01"] = houkokushoTemp.HOUKOKUSHO_BUNRUI_NAME;
                    }*/
                    himeiTemp = this.imHinmeiDao.GetDataByCd(BUNSEKISHOUMEISHO_LIST[0].HINMEI_CD);
                    if (himeiTemp != null)
                    {
                        row["ARTICLE03_05_HAIKIBUTSU_SHURUI01"] = BUNSEKISHOUMEISHO_LIST[0].HINMEI_NAME;
                    }
                }
                if (BUNSEKISHOUMEISHO_LIST.Count > 1)
                {
                    row["ARTICLE03_05_TEIJI_JIKI02"] = BUNSEKISHOUMEISHO_LIST[1].BUNSEKISHOUMEISHO_TEIJIJIKI;
                    /*houkokushoTemp = this.houokushoBunruiDao.GetDataByCd(BUNSEKISHOUMEISHO_LIST[1].HOUKOKUSHO_BUNRUI_CD);
                    if (houkokushoTemp != null)
                    {
                        eRow["ARTICLE03_05_HAIKIBUTSU_SHURUI02"] = houkokushoTemp.HOUKOKUSHO_BUNRUI_NAME;
                    }*/
                    himeiTemp = this.imHinmeiDao.GetDataByCd(BUNSEKISHOUMEISHO_LIST[1].HINMEI_CD);
                    if (himeiTemp != null)
                    {
                        row["ARTICLE03_05_HAIKIBUTSU_SHURUI02"] = himeiTemp.HINMEI_NAME;
                    }
                }
                if (BUNSEKISHOUMEISHO_LIST.Count > 2)
                {
                    row["ARTICLE03_05_TEIJI_JIKI03"] = BUNSEKISHOUMEISHO_LIST[2].BUNSEKISHOUMEISHO_TEIJIJIKI;
                    /*houkokushoTemp = this.houokushoBunruiDao.GetDataByCd(BUNSEKISHOUMEISHO_LIST[2].HOUKOKUSHO_BUNRUI_CD);
                    if (houkokushoTemp != null)
                    {
                        eRow["ARTICLE03_05_HAIKIBUTSU_SHURUI03"] = houkokushoTemp.HOUKOKUSHO_BUNRUI_NAME;
                    }*/
                    himeiTemp = this.imHinmeiDao.GetDataByCd(BUNSEKISHOUMEISHO_LIST[2].HINMEI_CD);
                    if (himeiTemp != null)
                    {
                        row["ARTICLE03_05_HAIKIBUTSU_SHURUI03"] = himeiTemp.HINMEI_NAME;
                    }
                }
            }

            //和暦変更のため
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
            System.Globalization.Calendar cal = new System.Globalization.JapaneseCalendar();
            ci.DateTimeFormat.Calendar = cal;

            var initDate = DateTime.Today.Date.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_DATE_WAREKI_FORMAT_INIT, ci);
            var strYUUKOU_BEGIN = initDate;
            if (!this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.IsNull)
            {
                strYUUKOU_BEGIN = this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.Value.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_YUUKOU_KIKAN_WAREKI_FORMAT, ci);
            }
            var strYUUKOU_END = initDate;
            if (!this.dto.ItakuKeiyakuKihon.YUUKOU_END.IsNull)
            {
                strYUUKOU_END = this.dto.ItakuKeiyakuKihon.YUUKOU_END.Value.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_YUUKOU_KIKAN_WAREKI_FORMAT, ci);
            }

            // 更新種別が「自動更新」の場合
            if (this.dto.ItakuKeiyakuKihon.KOUSHIN_SHUBETSU == 1)
            {
                row["ARTICLE14_TAN"] = false;
                row["ARTICLE14_AUTO"] = true;
                row["ARTICLE14_TAN_FROM"] = initDate;
                row["ARTICLE14_TAN_TO"] = initDate;
                row["ARTICLE14_AUTO_FROM"] = strYUUKOU_BEGIN;
                row["ARTICLE14_AUTO_TO"] = strYUUKOU_END;

                // 契約期間
                var strYUUKOU_SPAN = "";
                if (!this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.IsNull && !this.dto.ItakuKeiyakuKihon.YUUKOU_END.IsNull)
                {
                    DateTime begin = this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.Value;
                    DateTime end = this.dto.ItakuKeiyakuKihon.YUUKOU_END.Value;
                    strYUUKOU_SPAN = this.GetDateYearSpan(begin, end);
                }
                row["ARTICLE14_AUTO_SPAN"] = strYUUKOU_SPAN;
            }
            // 更新種別が「単発」の場合
            else
            {
                row["ARTICLE14_TAN"] = true;
                row["ARTICLE14_AUTO"] = false;
                row["ARTICLE14_TAN_FROM"] = strYUUKOU_BEGIN;
                row["ARTICLE14_TAN_TO"] = strYUUKOU_END;
                row["ARTICLE14_AUTO_FROM"] = initDate;
                row["ARTICLE14_AUTO_TO"] = initDate;
            }

            // 契約日
            if (this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE.IsNull)
            {
                row["CREATE_DATE"] = Const.ItakuKeiyakuHoshuConstans.KEIYAKUSHO_KEIYAKU_DATE_INIT;
            }
            else
            {
                row["CREATE_DATE"] = this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE.Value.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_DATE_WAREKI_FORMAT, ci);
            }

            ///委託契約書入力改修仕様
            row["CHIIKI_NAME1"] = string.Empty;
            row["CHIIKI_NAME2"] = string.Empty;
            row["KYOKA_YUUKOUKIGEN1"] = string.Empty;
            row["KYOKA_YUUKOUKIGEN2"] = string.Empty;
            row["GIGYOU_HANI1"] = string.Empty;
            row["GIGYOU_HANI2"] = string.Empty;
            row["HAIKI_SHURUI1"] = string.Empty;
            row["HAIKI_SHURUI2"] = string.Empty;
            row["KYOKA_JOKEN1"] = string.Empty;
            row["KYOKA_JOKEN2"] = string.Empty;
            row["KYOKA_NO1"] = string.Empty;
            row["KYOKA_NO2"] = string.Empty;
            row["CHIIKI_NAME3"] = string.Empty;
            row["CHIIKI_NAME4"] = string.Empty;
            row["KYOKA_YUUKOUKIGEN3"] = string.Empty;
            row["KYOKA_YUUKOUKIGEN4"] = string.Empty;
            row["GIGYOU_HANI3"] = string.Empty;
            row["GIGYOU_HANI4"] = string.Empty;
            row["KYOKA_JOKEN3"] = string.Empty;
            row["KYOKA_JOKEN4"] = string.Empty;
            row["KYOKA_NO3"] = string.Empty;
            row["KYOKA_NO4"] = string.Empty;
            row["ARTICLE02_01_NOTE04_LBL"] = string.Empty;
            row["ARTICLE02_01_NOTE06_LBL"] = string.Empty;
            //品名タブ
            row["HINMEI_NAME1"] = string.Empty;
            row["HINMEI_NAME2"] = string.Empty;
            row["HINMEI_NAME3"] = string.Empty;
            row["UNPAN_YOTEI_SUU1"] = string.Empty;
            row["UNPAN_YOTEI_SUU2"] = string.Empty;
            row["UNPAN_YOTEI_SUU3"] = string.Empty;
            row["UNPAN_YOTEI_SUU_UNIT1"] = string.Empty;
            row["UNPAN_YOTEI_SUU_UNIT2"] = string.Empty;
            row["UNPAN_YOTEI_SUU_UNIT3"] = string.Empty;
            row["UNPAN_ITAKU_TANKA1"] = string.Empty;
            row["UNPAN_ITAKU_TANKA2"] = string.Empty;
            row["UNPAN_ITAKU_TANKA3"] = string.Empty;
            row["ARTICLE02_02_NOTE02_LBL"] = string.Empty;

            //最終処分の場所、方法及び処理能力
            for (int i = 1; i < 8; i++)
            {
                row["SBN_NO" + i] = string.Empty;
                row["JIGYOJOU_NAME" + i] = string.Empty;
                row["JIGYOJOU_ADDRESS" + i] = string.Empty;
                row["SBN_HOUHOU" + i] = string.Empty;
                row["CAPACITY" + i] = string.Empty;
            }
            row["ARTICLE02_05_02_NOTE01_LBL"] = string.Empty;

            reportTable.Rows.Add(row);

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="tokubetsuFlg">特管フラグ</returns>
        private DataTable CreateSubReport01Data(bool tokubetsuFlg, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();
            var headerTitle1 = string.Empty;
            var headerTitle2 = tokubetsuFlg ? "〔特管〕" : "〔産廃〕";
            var headerTitle3 = string.Empty;

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NAIYOU", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NO01", typeof(string));
            reportTable.Columns.Add("TODOUHUKEN_SEIREISHI01", typeof(string));
            reportTable.Columns.Add("TODOUHUKEN_SEIREISHI02", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN01", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN02", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI01", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI02", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("KYOKA_BANGOU02", typeof(string));

            //var CHIIKI_KYOKA_LIST = new List<M_CHIIKIBETSU_KYOKA>();
            //var CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
            // 運搬・処分
            var KYOKASHO_LIST = new List<M_ITAKU_UPN_KYOKASHO>();
            // 運搬(収集運搬に関する事業範囲)
            headerTitle1 = "別紙  乙の事業範囲（収集運搬）" + headerTitle2;

            bool equals = false;
            for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
            {
                var kyokasho = this.dto.itakuUpnKyokasho[i];
                if (!tokubetsuFlg && kyokasho.KYOKA_KBN == 1)
                {
                    equals = false;
                    foreach (var betsu2 in this.dto.itakuKeiyakuBetsu2)
                    {
                        if (kyokasho.GYOUSHA_CD == betsu2.UNPAN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    var CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                    CHIIKI_KYOKA.KYOKA_KBN = 1;
                    CHIIKI_KYOKA.GYOUSHA_CD = kyokasho.GYOUSHA_CD;
                    CHIIKI_KYOKA.GENBA_CD = kyokasho.GENBA_CD;
                    CHIIKI_KYOKA.CHIIKI_CD = kyokasho.CHIIKI_CD;
                    CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                    if (CHIIKI_KYOKA != null)
                    {
                        KYOKASHO_LIST.Add(kyokasho);
                    }
                }
                else if (tokubetsuFlg && kyokasho.KYOKA_KBN == 2)
                {
                    equals = false;
                    foreach (var betsu2 in this.dto.itakuKeiyakuBetsu2)
                    {
                        if (kyokasho.GYOUSHA_CD == betsu2.UNPAN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    var CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                    CHIIKI_KYOKA.KYOKA_KBN = 1;
                    CHIIKI_KYOKA.GYOUSHA_CD = kyokasho.GYOUSHA_CD;
                    CHIIKI_KYOKA.GENBA_CD = kyokasho.GENBA_CD;
                    CHIIKI_KYOKA.CHIIKI_CD = kyokasho.CHIIKI_CD;
                    CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                    if (CHIIKI_KYOKA != null)
                    {
                        KYOKASHO_LIST.Add(kyokasho);
                    }
                }
            }

            var row = reportTable.NewRow();
            var noAdd = true;
            if (KYOKASHO_LIST.Count > 2)
            {
                if (!tokubetsuFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE04_LBL"] = "※別紙  乙の事業範囲（収集運搬）〔産廃〕に記載";
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE06_LBL"] = "※別紙  乙の事業範囲（収集運搬）〔特管〕に記載";
                }

                for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                {
                    var CHIIKI_KYOKA = KYOKASHO_LIST[i];
                    var KYOKA_CHIIKI = this.chiikiDao.GetDataByCd(CHIIKI_KYOKA.CHIIKI_CD);
                    if (KYOKA_CHIIKI == null)
                    {
                        KYOKA_CHIIKI = new M_CHIIKI();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (i % 2 == 0)
                    {
                        row = reportTable.NewRow();
                        noAdd = true;
                        row["FORMAT_LBL"] = headerTitle3;
                        row["SAISHUU_SHOBUN_NAIYOU"] = headerTitle1;
                        //eRow["SAISHUU_SHOBUN_NO01"] = headerTitle2;
                        row["SAISHUU_SHOBUN_NO01"] = string.Empty;
                        row["TODOUHUKEN_SEIREISHI01"] = KYOKA_CHIIKI.CHIIKI_NAME;
                        //eRow["KYOKA_YUUKOUKIGEN01"] = tokubetsuFlg ? this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END) : this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                        row["KYOKA_YUUKOUKIGEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        row["GIGYOU_HANI01"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        row["KYOKA_JOKEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                        row["KYOKA_BANGOU01"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                    }
                    else
                    {
                        row["TODOUHUKEN_SEIREISHI02"] = KYOKA_CHIIKI.CHIIKI_NAME;
                        //eRow["KYOKA_YUUKOUKIGEN02"] = tokubetsuFlg ? this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END) : this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                        row["KYOKA_YUUKOUKIGEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        row["GIGYOU_HANI02"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        row["KYOKA_JOKEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                        row["KYOKA_BANGOU02"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                        reportTable.Rows.Add(row);
                        noAdd = false;
                    }
                }
            }
            else
            {
                if (!tokubetsuFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE04_LBL"] = string.Empty;
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE06_LBL"] = string.Empty;
                }

                for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                {
                    var CHIIKI_KYOKA = KYOKASHO_LIST[i];
                    var KYOKA_CHIIKI = this.chiikiDao.GetDataByCd(CHIIKI_KYOKA.CHIIKI_CD);
                    if (KYOKA_CHIIKI == null)
                    {
                        KYOKA_CHIIKI = new M_CHIIKI();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (!tokubetsuFlg)
                    {
                        if (i == 0)
                        {
                            dtTableHead.Rows[0]["CHIIKI_NAME1"] = KYOKA_CHIIKI.CHIIKI_NAME;
                            //dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN1"] = this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                            dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN1"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            dtTableHead.Rows[0]["GIGYOU_HANI1"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            dtTableHead.Rows[0]["KYOKA_JOKEN1"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            dtTableHead.Rows[0]["KYOKA_NO1"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["CHIIKI_NAME2"] = KYOKA_CHIIKI.CHIIKI_NAME;
                            //dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN2"] = this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                            dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN2"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            dtTableHead.Rows[0]["GIGYOU_HANI2"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            dtTableHead.Rows[0]["KYOKA_JOKEN2"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            dtTableHead.Rows[0]["KYOKA_NO2"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            dtTableHead.Rows[0]["CHIIKI_NAME3"] = KYOKA_CHIIKI.CHIIKI_NAME;
                            //dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN3"] = this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END);
                            dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN3"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            dtTableHead.Rows[0]["GIGYOU_HANI3"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            dtTableHead.Rows[0]["KYOKA_JOKEN3"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            dtTableHead.Rows[0]["KYOKA_NO3"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["CHIIKI_NAME4"] = KYOKA_CHIIKI.CHIIKI_NAME;
                            //dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN4"] = this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END);
                            dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN4"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            dtTableHead.Rows[0]["GIGYOU_HANI4"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            dtTableHead.Rows[0]["KYOKA_JOKEN4"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            dtTableHead.Rows[0]["KYOKA_NO4"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                        }
                    }
                }
            }

            if (noAdd)
            {
                //eRow["FORMAT_LBL"] = headerTitle3;
                row["SAISHUU_SHOBUN_NAIYOU"] = headerTitle1;
                //eRow["SAISHUU_SHOBUN_NO01"] = headerTitle2;
                row["SAISHUU_SHOBUN_NO01"] = string.Empty;
                reportTable.Rows.Add(row);
                noAdd = false;
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="tokubetsuFlg">特管フラグ</returns>
        private DataTable CreateSubReport02Data(bool tokubetsuFlg, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();
            var headerTitle1 = string.Empty;
            var headerTitle2 = tokubetsuFlg ? "〔特管〕" : "〔産廃〕";
            var headerTitle3 = string.Empty;

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NAIYOU", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NO01", typeof(string));
            reportTable.Columns.Add("TODOUHUKEN_SEIREISHI01", typeof(string));
            reportTable.Columns.Add("TODOUHUKEN_SEIREISHI02", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN01", typeof(string));
            reportTable.Columns.Add("KYOKA_YUUKOUKIGEN02", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI01", typeof(string));
            reportTable.Columns.Add("GIGYOU_HANI02", typeof(string));
            reportTable.Columns.Add("HAIKI_SHURUI01", typeof(string));
            reportTable.Columns.Add("HAIKI_SHURUI02", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("KYOKA_BANGOU02", typeof(string));

            var KYOKASHO_LIST = new List<M_ITAKU_SBN_KYOKASHO>();
            // 処分(処分に関する事業範囲)
            headerTitle1 = "別紙　乙の事業範囲（処分）" + headerTitle2;

            bool equals = false;
            for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
            {
                var kyokasho = this.dto.itakuSbnKyokasho[i];
                if (!tokubetsuFlg && (kyokasho.KYOKA_KBN == 3 || kyokasho.KYOKA_KBN == 5))
                {
                    equals = false;
                    foreach (var betsu3 in this.dto.itakuKeiyakuBetsu3)
                    {
                        if (kyokasho.GYOUSHA_CD == betsu3.SHOBUN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    var CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                    if (kyokasho.KYOKA_KBN == 3)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 2;
                        CHIIKI_KYOKA.GYOUSHA_CD = kyokasho.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = kyokasho.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = kyokasho.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            KYOKASHO_LIST.Add(kyokasho);
                        }
                    }
                    else if (kyokasho.KYOKA_KBN == 5)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 3;
                        CHIIKI_KYOKA.GYOUSHA_CD = kyokasho.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = kyokasho.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = kyokasho.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            KYOKASHO_LIST.Add(kyokasho);
                        }
                    }
                }
                else if (tokubetsuFlg && (kyokasho.KYOKA_KBN == 4 || kyokasho.KYOKA_KBN == 6))
                {
                    equals = false;
                    foreach (var betsu3 in this.dto.itakuKeiyakuBetsu3)
                    {
                        if (kyokasho.GYOUSHA_CD == betsu3.SHOBUN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    var CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                    if (kyokasho.KYOKA_KBN == 4)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 2;
                        CHIIKI_KYOKA.GYOUSHA_CD = kyokasho.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = kyokasho.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = kyokasho.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            KYOKASHO_LIST.Add(kyokasho);
                        }
                    }
                    else if (kyokasho.KYOKA_KBN == 6)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 3;
                        CHIIKI_KYOKA.GYOUSHA_CD = kyokasho.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = kyokasho.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = kyokasho.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            KYOKASHO_LIST.Add(kyokasho);
                        }
                    }
                }
            }

            var row = reportTable.NewRow();
            var noAdd = true;
            if (KYOKASHO_LIST.Count > 1)
            {
                if (!tokubetsuFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE04_LBL"] = "※別紙  乙の事業範囲（処分）〔産廃〕に記載";
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE06_LBL"] = "※別紙  乙の事業範囲（処分）〔特管〕に記載";
                }

                for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                {
                    var CHIIKI_KYOKA = KYOKASHO_LIST[i];
                    var KYOKA_CHIIKI = this.chiikiDao.GetDataByCd(CHIIKI_KYOKA.CHIIKI_CD);
                    if (KYOKA_CHIIKI == null)
                    {
                        KYOKA_CHIIKI = new M_CHIIKI();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (i % 2 == 0)
                    {
                        row = reportTable.NewRow();
                        noAdd = true;
                        row["FORMAT_LBL"] = headerTitle3;
                        row["SAISHUU_SHOBUN_NAIYOU"] = headerTitle1;
                        //eRow["SAISHUU_SHOBUN_NO01"] = headerTitle2;
                        row["TODOUHUKEN_SEIREISHI01"] = KYOKA_CHIIKI.CHIIKI_NAME;
                        //eRow["KYOKA_YUUKOUKIGEN01"] = tokubetsuFlg ? this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END) : this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                        row["KYOKA_YUUKOUKIGEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        row["GIGYOU_HANI01"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        row["HAIKI_SHURUI01"] = Const.ItakuKeiyakuHoshuConstans.HAIKI_SHURUI;
                        row["KYOKA_JOKEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                        row["KYOKA_BANGOU01"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                    }
                    else
                    {
                        row["TODOUHUKEN_SEIREISHI02"] = KYOKA_CHIIKI.CHIIKI_NAME;
                        //eRow["KYOKA_YUUKOUKIGEN02"] = tokubetsuFlg ? this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END) : this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                        row["KYOKA_YUUKOUKIGEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        row["GIGYOU_HANI02"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        row["HAIKI_SHURUI02"] = Const.ItakuKeiyakuHoshuConstans.HAIKI_SHURUI;
                        row["KYOKA_JOKEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                        row["KYOKA_BANGOU02"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                        reportTable.Rows.Add(row);
                        noAdd = false;
                    }
                }
            }
            else
            {
                if (!tokubetsuFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE04_LBL"] = string.Empty;
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_01_NOTE06_LBL"] = string.Empty;
                }

                for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                {
                    var CHIIKI_KYOKA = KYOKASHO_LIST[i];
                    var KYOKA_CHIIKI = this.chiikiDao.GetDataByCd(CHIIKI_KYOKA.CHIIKI_CD);
                    if (KYOKA_CHIIKI == null)
                    {
                        KYOKA_CHIIKI = new M_CHIIKI();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (!tokubetsuFlg)
                    {
                        dtTableHead.Rows[0]["CHIIKI_NAME1"] = KYOKA_CHIIKI.CHIIKI_NAME;
                        //dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN1"] = this.ConvertHiduke(CHIIKI_KYOKA.FUTSUU_KYOKA_END);
                        dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN1"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        dtTableHead.Rows[0]["GIGYOU_HANI1"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        dtTableHead.Rows[0]["HAIKI_SHURUI1"] = Const.ItakuKeiyakuHoshuConstans.HAIKI_SHURUI;
                        dtTableHead.Rows[0]["KYOKA_JOKEN1"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                        dtTableHead.Rows[0]["KYOKA_NO1"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                    }
                    else
                    {
                        dtTableHead.Rows[0]["CHIIKI_NAME2"] = KYOKA_CHIIKI.CHIIKI_NAME;
                        //dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN2"] = this.ConvertHiduke(CHIIKI_KYOKA.TOKUBETSU_KYOKA_END);
                        dtTableHead.Rows[0]["KYOKA_YUUKOUKIGEN2"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        dtTableHead.Rows[0]["GIGYOU_HANI2"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        dtTableHead.Rows[0]["HAIKI_SHURUI2"] = Const.ItakuKeiyakuHoshuConstans.HAIKI_SHURUI;
                        dtTableHead.Rows[0]["KYOKA_JOKEN2"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                        dtTableHead.Rows[0]["KYOKA_NO2"] = this.ConvertKyokaNo(CHIIKI_KYOKA.KYOKA_NO);
                    }
                }
            }

            if (noAdd)
            {
                //eRow["FORMAT_LBL"] = headerTitle3;
                row["SAISHUU_SHOBUN_NAIYOU"] = headerTitle1;
                //eRow["SAISHUU_SHOBUN_NO01"] = headerTitle2;
                reportTable.Rows.Add(row);
                noAdd = false;
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        /// <returns name="searchKbn">0:全て、1:収集・運搬、2:処分</returns>
        /// <returns name="title">タイトル</returns>
        private DataTable CreateSubReport03Data(int searchKbn, bool upnFlg, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NAIYOU", typeof(string));
            reportTable.Columns.Add("SHURUI_NAME01", typeof(string));
            reportTable.Columns.Add("SHURUI_NAME02", typeof(string));
            reportTable.Columns.Add("SHURUI_NAME03", typeof(string));
            reportTable.Columns.Add("SUURYOU01", typeof(string));
            reportTable.Columns.Add("SUURYOU02", typeof(string));
            reportTable.Columns.Add("SUURYOU03", typeof(string));
            reportTable.Columns.Add("SUURYOU_TANI01", typeof(string));
            reportTable.Columns.Add("SUURYOU_TANI02", typeof(string));
            reportTable.Columns.Add("SUURYOU_TANI03", typeof(string));
            reportTable.Columns.Add("TANKA01", typeof(string));
            reportTable.Columns.Add("TANKA02", typeof(string));
            reportTable.Columns.Add("TANKA03", typeof(string));

            // 委託する産業廃棄物の種類、数量及び単価（別紙３）
            var HINMEI_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
            {
                M_HINMEI hinmei = new M_HINMEI();
                hinmei = this.imHinmeiDao.GetDataByCd(this.dto.itakuKeiyakuHinmei[i].HINMEI_CD);
                // this.dto.itakuKeiyakuHinmei[i].HINMEI_NAME = hinmei.HINMEI_NAME;
                if (searchKbn == 0)
                {
                    HINMEI_LIST.Add(this.dto.itakuKeiyakuHinmei[i]);
                }
                else
                {
                    var GYOUSHA = this.gyoushaDao.GetDataByCd(this.dto.itakuKeiyakuHinmei[i].SHOBUN_JIGYOUJOU_CD);
                    // 20151102 BUNN #12040 STR
                    if (searchKbn == 1 && !GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN)
                    {
                        HINMEI_LIST.Add(this.dto.itakuKeiyakuHinmei[i]);
                    }
                    else if (searchKbn == 2 && GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN)
                    {
                        HINMEI_LIST.Add(this.dto.itakuKeiyakuHinmei[i]);
                    }
                    // 20151102 BUNN #12040 END
                }
            }

            var row = reportTable.NewRow();
            var noAdd = true;
            if (HINMEI_LIST.Count > 3)
            {
                if (upnFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_02_NOTE02_LBL"] = "※別紙  委託する産業廃棄物の種類、数量及び単価（収集運搬）に記載";
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_02_NOTE02_LBL"] = "※別紙  委託する産業廃棄物の種類、数量及び単価（処分）に記載";
                }
                for (int i = 0; i < HINMEI_LIST.Count; i++)
                {
                    var HINMEI = HINMEI_LIST[i];
                    var UNIT = new M_UNIT();
                    if (upnFlg && !HINMEI.UNPAN_YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        UNIT = this.unitDao.GetDataByCd(HINMEI.UNPAN_YOTEI_SUU_UNIT_CD.Value);
                    }
                    else if (!upnFlg && !HINMEI.SHOBUN_YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        UNIT = this.unitDao.GetDataByCd(HINMEI.SHOBUN_YOTEI_SUU_UNIT_CD.Value);
                    }
                    if (UNIT == null)
                    {
                        UNIT = new M_UNIT();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (i % 3 == 0)
                    {
                        row = reportTable.NewRow();
                        noAdd = true;
                        row["FORMAT_LBL"] = string.Empty;
                        if (upnFlg)
                        {
                            row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  委託する産業廃棄物の種類、数量及び単価（収集運搬）";
                        }
                        else
                        {
                            row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  委託する産業廃棄物の種類、数量及び単価（処分）";
                        }
                        row["SHURUI_NAME01"] = HINMEI.HINMEI_NAME;
                        if (upnFlg)
                        {
                            row["SUURYOU01"] = this.ConvertSuuryo(HINMEI.UNPAN_YOTEI_SUU);
                            row["TANKA01"] = this.ConvertTanka(HINMEI.UNPAN_ITAKU_TANKA);
                        }
                        else
                        {
                            row["SUURYOU01"] = this.ConvertSuuryo(HINMEI.SHOBUN_YOTEI_SUU);
                            row["TANKA01"] = this.ConvertTanka(HINMEI.SHOBUN_ITAKU_TANKA);
                        }
                        row["SUURYOU_TANI01"] = UNIT.UNIT_NAME;
                    }
                    else if (i % 3 == 1)
                    {
                        row["SHURUI_NAME02"] = HINMEI.HINMEI_NAME;
                        if (upnFlg)
                        {
                            row["SUURYOU02"] = this.ConvertSuuryo(HINMEI.UNPAN_YOTEI_SUU);
                            row["TANKA02"] = this.ConvertTanka(HINMEI.UNPAN_ITAKU_TANKA);
                        }
                        else
                        {
                            row["SUURYOU02"] = this.ConvertSuuryo(HINMEI.SHOBUN_YOTEI_SUU);
                            row["TANKA02"] = this.ConvertTanka(HINMEI.SHOBUN_ITAKU_TANKA);
                        }
                        row["SUURYOU_TANI02"] = UNIT.UNIT_NAME;
                    }
                    else
                    {
                        row["SHURUI_NAME03"] = HINMEI.HINMEI_NAME;
                        if (upnFlg)
                        {
                            row["SUURYOU03"] = this.ConvertSuuryo(HINMEI.UNPAN_YOTEI_SUU);
                            row["TANKA03"] = this.ConvertTanka(HINMEI.UNPAN_ITAKU_TANKA);
                        }
                        else
                        {
                            row["SUURYOU03"] = this.ConvertSuuryo(HINMEI.SHOBUN_YOTEI_SUU);
                            row["TANKA03"] = this.ConvertTanka(HINMEI.SHOBUN_ITAKU_TANKA);
                        }
                        row["SUURYOU_TANI03"] = UNIT.UNIT_NAME;
                        reportTable.Rows.Add(row);
                        noAdd = false;
                    }
                }
            }
            else
            {
                dtTableHead.Rows[0]["ARTICLE02_02_NOTE02_LBL"] = string.Empty;
                for (int i = 0; i < HINMEI_LIST.Count; i++)
                {
                    var HINMEI = HINMEI_LIST[i];
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei = this.imHinmeiDao.GetDataByCd(HINMEI.HINMEI_CD);
                    //HINMEI_LIST[i].HINMEI_NAME = hinmei.HINMEI_NAME;
                    var UNIT = new M_UNIT();
                    if (upnFlg && !HINMEI.UNPAN_YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        UNIT = this.unitDao.GetDataByCd(HINMEI.UNPAN_YOTEI_SUU_UNIT_CD.Value);
                    }
                    else if (!upnFlg && !HINMEI.SHOBUN_YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        UNIT = this.unitDao.GetDataByCd(HINMEI.SHOBUN_YOTEI_SUU_UNIT_CD.Value);
                    }
                    if (UNIT == null)
                    {
                        UNIT = new M_UNIT();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (i == 0)
                    {
                        dtTableHead.Rows[0]["HINMEI_NAME1"] = HINMEI.HINMEI_NAME;
                        if (upnFlg)
                        {
                            dtTableHead.Rows[0]["UNPAN_YOTEI_SUU1"] = this.ConvertSuuryo(HINMEI.UNPAN_YOTEI_SUU);
                            dtTableHead.Rows[0]["UNPAN_ITAKU_TANKA1"] = this.ConvertTanka(HINMEI.UNPAN_ITAKU_TANKA);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["UNPAN_YOTEI_SUU1"] = this.ConvertSuuryo(HINMEI.SHOBUN_YOTEI_SUU);
                            dtTableHead.Rows[0]["UNPAN_ITAKU_TANKA1"] = this.ConvertTanka(HINMEI.SHOBUN_ITAKU_TANKA);
                        }
                        dtTableHead.Rows[0]["UNPAN_YOTEI_SUU_UNIT1"] = UNIT.UNIT_NAME;
                    }
                    else if (i == 1)
                    {
                        dtTableHead.Rows[0]["HINMEI_NAME2"] = HINMEI.HINMEI_NAME;
                        if (upnFlg)
                        {
                            dtTableHead.Rows[0]["UNPAN_YOTEI_SUU2"] = this.ConvertSuuryo(HINMEI.UNPAN_YOTEI_SUU);
                            dtTableHead.Rows[0]["UNPAN_ITAKU_TANKA2"] = this.ConvertTanka(HINMEI.UNPAN_ITAKU_TANKA);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["UNPAN_YOTEI_SUU2"] = this.ConvertSuuryo(HINMEI.SHOBUN_YOTEI_SUU);
                            dtTableHead.Rows[0]["UNPAN_ITAKU_TANKA2"] = this.ConvertTanka(HINMEI.SHOBUN_ITAKU_TANKA);
                        }
                        dtTableHead.Rows[0]["UNPAN_YOTEI_SUU_UNIT2"] = UNIT.UNIT_NAME;
                    }
                    else
                    {
                        dtTableHead.Rows[0]["HINMEI_NAME3"] = HINMEI.HINMEI_NAME;
                        if (upnFlg)
                        {
                            dtTableHead.Rows[0]["UNPAN_YOTEI_SUU3"] = this.ConvertSuuryo(HINMEI.UNPAN_YOTEI_SUU);
                            dtTableHead.Rows[0]["UNPAN_ITAKU_TANKA3"] = this.ConvertTanka(HINMEI.UNPAN_ITAKU_TANKA);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["UNPAN_YOTEI_SUU3"] = this.ConvertSuuryo(HINMEI.SHOBUN_YOTEI_SUU);
                            dtTableHead.Rows[0]["UNPAN_ITAKU_TANKA3"] = this.ConvertTanka(HINMEI.SHOBUN_ITAKU_TANKA);
                        }
                        dtTableHead.Rows[0]["UNPAN_YOTEI_SUU_UNIT3"] = UNIT.UNIT_NAME;
                    }
                }
            }

            if (noAdd)
            {
                row["FORMAT_LBL"] = string.Empty;
                if (upnFlg)
                {
                    row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  委託する産業廃棄物の種類、数量及び単価（収集運搬）";
                }
                else
                {
                    row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  委託する産業廃棄物の種類、数量及び単価（処分）";
                }
                reportTable.Rows.Add(row);
                noAdd = false;
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        private DataTable CreateSubReport04Data(string title, string rptNo, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NAIYOU", typeof(string));
            reportTable.Columns.Add("SHOBUN_SHIMEI", typeof(string));
            reportTable.Columns.Add("SHOBUN_ADDRESS", typeof(string));
            reportTable.Columns.Add("SHOBUN_CHIIKI", typeof(string));
            reportTable.Columns.Add("SHOBUN_KYOKA_KIGEN", typeof(string));
            reportTable.Columns.Add("SHIOBUN_JIGYO_KBN", typeof(string));
            reportTable.Columns.Add("SHOBUN_HAIKI_SHURUI", typeof(string));
            reportTable.Columns.Add("SHOBUN_KYOKA_JOUKEN", typeof(string));
            reportTable.Columns.Add("SHOBUN_KYOKA_NO", typeof(string));
            reportTable.Columns.Add("SHOBUN_JIGYOUJOU_NAME", typeof(string));
            reportTable.Columns.Add("SHOBUN_JIGYOUJOU_ADDRESS", typeof(string));
            reportTable.Columns.Add("SHOBUN_HOUHOU", typeof(string));
            reportTable.Columns.Add("SHOBUN_SHISETSU_CAPCITY", typeof(string));

            // 運搬の最終目的地
            var row = reportTable.NewRow();
            var noAdd = true;
            var itakuKeiyakuBetsu3List = new List<M_ITAKU_KEIYAKU_BETSU3>();
            var itakuSbnKyokashoList = new List<M_ITAKU_SBN_KYOKASHO>();
            for (int i = 0; i < this.dto.itakuKeiyakuBetsu3.Length; i++)
            {
                var betsu3 = this.dto.itakuKeiyakuBetsu3[i];
                for (int j = 0; j < this.dto.itakuSbnKyokasho.Length; j++)
                {
                    var sbnKyokasho = this.dto.itakuSbnKyokasho[j];
                    if (betsu3.SHOBUN_GYOUSHA_CD == sbnKyokasho.GYOUSHA_CD && betsu3.SHOBUN_JIGYOUJOU_CD == sbnKyokasho.GENBA_CD)
                    {
                        itakuSbnKyokashoList.Add(sbnKyokasho);
                    }
                }
                itakuKeiyakuBetsu3List.Add(betsu3);
            }

            if (itakuKeiyakuBetsu3List.Count > 1)
            {
                if (rptNo == "R001_01Report")
                {
                    dtTableHead.Rows[0]["ARTICLE02_04_NOTE02_LBL"] = "※別紙  運搬の最終目的地に記載";
                }
                else if (rptNo == "R001_02Report")
                {
                    dtTableHead.Rows[0]["ARTICLE02_04_NOTE02_LBL"] = "※別紙  処分の場所、方法及び処理能力に記載";
                }

                for (int i = 0; i < itakuKeiyakuBetsu3List.Count; i++)
                {
                    var BETSU3 = itakuKeiyakuBetsu3List[i];

                    // 処分方法
                    var SAISHUU_SBN_HOUHOU = shobunHouhouDao.GetDataByCd(BETSU3.SHOBUN_HOUHOU_CD);
                    if (SAISHUU_SBN_HOUHOU == null)
                    {
                        SAISHUU_SBN_HOUHOU = new M_SHOBUN_HOUHOU();
                    }
                    // 運搬の最終目的地(業者)
                    var SAISHUU_SBN_GYO = new M_GYOUSHA();
                    SAISHUU_SBN_GYO = this.gyoushaDao.GetDataByCd(BETSU3.SHOBUN_GYOUSHA_CD);
                    if (SAISHUU_SBN_GYO == null)
                    {
                        SAISHUU_SBN_GYO = new M_GYOUSHA();
                    }

                    // 運搬の最終目的地(現場)
                    var SAISHUU_SBN_GEN = new M_GENBA();
                    SAISHUU_SBN_GEN.GYOUSHA_CD = BETSU3.SHOBUN_GYOUSHA_CD;
                    SAISHUU_SBN_GEN.GENBA_CD = BETSU3.SHOBUN_JIGYOUJOU_CD;
                    SAISHUU_SBN_GEN = this.genbaDao.GetDataByCd(SAISHUU_SBN_GEN);
                    if (SAISHUU_SBN_GEN == null)
                    {
                        SAISHUU_SBN_GEN = new M_GENBA();
                    }

                    // 運搬の最終目的地：許可番号
                    var KYOKA_NO = string.Empty;
                    if (itakuSbnKyokashoList.Count - 1 >= i)
                    {
                        KYOKA_NO = itakuSbnKyokashoList[i].KYOKA_NO;
                    }

                    // 運搬の最終目的地(地域＆許可)
                    var SAISHUU_SBN_CHIIKI = new M_CHIIKI();
                    SAISHUU_SBN_CHIIKI = this.chiikiDao.GetDataByCd(SAISHUU_SBN_GYO.CHIIKI_CD);
                    if (SAISHUU_SBN_CHIIKI == null)
                    {
                        SAISHUU_SBN_CHIIKI = new M_CHIIKI();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    row = reportTable.NewRow();
                    row["FORMAT_LBL"] = title;
                    if (rptNo == "R001_01Report")
                    {
                        row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  運搬の最終目的地";
                    }
                    else if (rptNo == "R001_02Report")
                    {
                        row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  処分の場所、方法及び処理能力";
                    }

                    M_TODOUFUKEN gyoushaTodoufuken = new M_TODOUFUKEN();
                    string gyoushatodoufukenName = "";
                    if (SAISHUU_SBN_GYO != null && !SAISHUU_SBN_GYO.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        gyoushaTodoufuken = this.todoufukenDao.GetDataByCd(SAISHUU_SBN_GYO.GYOUSHA_TODOUFUKEN_CD.ToString());
                        gyoushatodoufukenName = gyoushaTodoufuken.TODOUFUKEN_NAME;
                    }

                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (!SAISHUU_SBN_GEN.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(SAISHUU_SBN_GEN.GENBA_TODOUFUKEN_CD.ToString());
                    }
                    string tdhk = "";
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }

                    if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_SBN)
                    {
                        row["SHOBUN_SHIMEI"] = this.ConvertTwoText(SAISHUU_SBN_GEN.GENBA_NAME1, SAISHUU_SBN_GEN.GENBA_NAME2);
                        row["SHOBUN_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU3.SHOBUN_JIGYOUJOU_ADDRESS1, BETSU3.SHOBUN_JIGYOUJOU_ADDRESS2);
                    }
                    else
                    {
                        row["SHOBUN_SHIMEI"] = this.ConvertTwoText(SAISHUU_SBN_GYO.GYOUSHA_NAME1, SAISHUU_SBN_GYO.GYOUSHA_NAME2);
                        row["SHOBUN_ADDRESS"] = gyoushatodoufukenName + this.ConvertTwoText(BETSU3.SHOBUN_GYOUSHA_ADDRESS1, BETSU3.SHOBUN_GYOUSHA_ADDRESS2);
                    }
                    row["SHOBUN_CHIIKI"] = SAISHUU_SBN_CHIIKI.CHIIKI_NAME;
                    if (string.IsNullOrEmpty(KYOKA_NO))
                    {
                        row["SHOBUN_KYOKA_KIGEN"] = string.Empty;
                        row["SHIOBUN_JIGYO_KBN"] = string.Empty;
                        row["SHOBUN_HAIKI_SHURUI"] = string.Empty;
                        row["SHOBUN_KYOKA_JOUKEN"] = string.Empty;
                    }
                    else
                    {
                        row["SHOBUN_KYOKA_KIGEN"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        row["SHIOBUN_JIGYO_KBN"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        row["SHOBUN_HAIKI_SHURUI"] = Const.ItakuKeiyakuHoshuConstans.HAIKI_SHURUI;
                        row["SHOBUN_KYOKA_JOUKEN"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                    }

                    row["SHOBUN_KYOKA_NO"] = this.ConvertKyokaNo(KYOKA_NO);
                    row["SHOBUN_JIGYOUJOU_NAME"] = this.ConvertTwoText(SAISHUU_SBN_GEN.GENBA_NAME1, SAISHUU_SBN_GEN.GENBA_NAME2);
                    row["SHOBUN_JIGYOUJOU_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU3.SHOBUN_JIGYOUJOU_ADDRESS1, BETSU3.SHOBUN_JIGYOUJOU_ADDRESS2);
                    row["SHOBUN_HOUHOU"] = SAISHUU_SBN_HOUHOU.SHOBUN_HOUHOU_NAME;
                    row["SHOBUN_SHISETSU_CAPCITY"] = this.ConvertSpecUnit(BETSU3.SHISETSU_CAPACITY);
                    reportTable.Rows.Add(row);
                    noAdd = false;
                }
            }
            else
            {
                if (rptNo == "R001_01Report")
                {
                    dtTableHead.Rows[0]["ARTICLE02_04_NOTE02_LBL"] = string.Empty;
                }
                else if (rptNo == "R001_02Report")
                {
                    dtTableHead.Rows[0]["ARTICLE02_04_NOTE02_LBL"] = string.Empty;
                }

                for (int i = 0; i < itakuKeiyakuBetsu3List.Count; i++)
                {
                    var BETSU3 = itakuKeiyakuBetsu3List[i];

                    // 処分方法
                    var SAISHUU_SBN_HOUHOU = shobunHouhouDao.GetDataByCd(BETSU3.SHOBUN_HOUHOU_CD);
                    if (SAISHUU_SBN_HOUHOU == null)
                    {
                        SAISHUU_SBN_HOUHOU = new M_SHOBUN_HOUHOU();
                    }
                    // 運搬の最終目的地(業者)
                    var SAISHUU_SBN_GYO = new M_GYOUSHA();
                    SAISHUU_SBN_GYO = this.gyoushaDao.GetDataByCd(BETSU3.SHOBUN_GYOUSHA_CD);
                    if (SAISHUU_SBN_GYO == null)
                    {
                        SAISHUU_SBN_GYO = new M_GYOUSHA();
                    }

                    // 運搬の最終目的地(現場)
                    var SAISHUU_SBN_GEN = new M_GENBA();
                    SAISHUU_SBN_GEN.GYOUSHA_CD = BETSU3.SHOBUN_GYOUSHA_CD;
                    SAISHUU_SBN_GEN.GENBA_CD = BETSU3.SHOBUN_JIGYOUJOU_CD;
                    SAISHUU_SBN_GEN = this.genbaDao.GetDataByCd(SAISHUU_SBN_GEN);
                    if (SAISHUU_SBN_GEN == null)
                    {
                        SAISHUU_SBN_GEN = new M_GENBA();
                    }

                    // 運搬の最終目的地：許可番号
                    var KYOKA_NO = string.Empty;
                    if (itakuSbnKyokashoList.Count - 1 >= i)
                    {
                        KYOKA_NO = itakuSbnKyokashoList[i].KYOKA_NO;
                    }

                    // 運搬の最終目的地(地域＆許可)
                    var SAISHUU_SBN_CHIIKI = new M_CHIIKI();
                    SAISHUU_SBN_CHIIKI = this.chiikiDao.GetDataByCd(SAISHUU_SBN_GEN.CHIIKI_CD);
                    if (SAISHUU_SBN_CHIIKI == null)
                    {
                        SAISHUU_SBN_CHIIKI = new M_CHIIKI();
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    M_TODOUFUKEN gyoushaTodoufuken = new M_TODOUFUKEN();
                    string gyoushatodoufukenName = "";
                    if (SAISHUU_SBN_GYO != null && !SAISHUU_SBN_GYO.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        gyoushaTodoufuken = this.todoufukenDao.GetDataByCd(SAISHUU_SBN_GYO.GYOUSHA_TODOUFUKEN_CD.ToString());
                        gyoushatodoufukenName = gyoushaTodoufuken.TODOUFUKEN_NAME;
                    }

                    M_GENBA genba = new M_GENBA();
                    genba.GYOUSHA_CD = BETSU3.SHOBUN_GYOUSHA_CD;
                    genba.GENBA_CD = BETSU3.SHOBUN_JIGYOUJOU_CD;
                    genba = this.genbaDao.GetDataByCd(genba);
                    if (genba == null)
                    {
                        // SAISHUU_SBN_GYOと同じように制御
                        genba = new M_GENBA();
                    }

                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    string todoufukenName = "";
                    if (genba != null && !genba.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(genba.GENBA_TODOUFUKEN_CD.ToString());
                        todoufukenName = todoufuken.TODOUFUKEN_NAME;
                    }

                    if (this.dto.ItakuKeiyakuKihon.ITAKU_KEIYAKU_SHURUI == Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_SHURUI_SBN)
                    {
                        dtTableHead.Rows[0]["ARTICLE02_04_SHIMEI"] = this.ConvertTwoText(genba.GENBA_NAME1, genba.GENBA_NAME2);
                        dtTableHead.Rows[0]["ARTICLE02_04_ADDRESS"] = todoufukenName + this.ConvertTwoText(BETSU3.SHOBUN_JIGYOUJOU_ADDRESS1, BETSU3.SHOBUN_JIGYOUJOU_ADDRESS2);
                    }
                    else
                    {
                        dtTableHead.Rows[0]["ARTICLE02_04_SHIMEI"] = this.ConvertTwoText(SAISHUU_SBN_GYO.GYOUSHA_NAME1, SAISHUU_SBN_GYO.GYOUSHA_NAME2);
                        dtTableHead.Rows[0]["ARTICLE02_04_ADDRESS"] = gyoushatodoufukenName + this.ConvertTwoText(BETSU3.SHOBUN_GYOUSHA_ADDRESS1, BETSU3.SHOBUN_GYOUSHA_ADDRESS2);
                    }
                    dtTableHead.Rows[0]["ARTICLE02_04_CHIIKI"] = SAISHUU_SBN_CHIIKI.CHIIKI_NAME;

                    if (string.IsNullOrEmpty(KYOKA_NO))
                    {
                        dtTableHead.Rows[0]["ARTICLE02_04_KYOKA_KIGEN"] = string.Empty;
                        dtTableHead.Rows[0]["ARTICLE02_04_JIGYO_KBN"] = string.Empty;
                        dtTableHead.Rows[0]["ARTICLE02_04_HAIKI_SHURUI"] = string.Empty;
                        dtTableHead.Rows[0]["ARTICLE02_04_KYOKA_JOUKEN"] = string.Empty;
                    }
                    else
                    {
                        dtTableHead.Rows[0]["ARTICLE02_04_KYOKA_KIGEN"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                        dtTableHead.Rows[0]["ARTICLE02_04_JIGYO_KBN"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                        dtTableHead.Rows[0]["ARTICLE02_04_HAIKI_SHURUI"] = Const.ItakuKeiyakuHoshuConstans.HAIKI_SHURUI;
                        dtTableHead.Rows[0]["ARTICLE02_04_KYOKA_JOUKEN"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                    }

                    dtTableHead.Rows[0]["ARTICLE02_04_KYOKA_NO"] = this.ConvertKyokaNo(KYOKA_NO);
                    dtTableHead.Rows[0]["ARTICLE02_04_JIGYOUJOU_NAME"] = this.ConvertTwoText(SAISHUU_SBN_GEN.GENBA_NAME1, SAISHUU_SBN_GEN.GENBA_NAME2);
                    dtTableHead.Rows[0]["ARTICLE02_04_JIGYOUJOU_ADDRESS"] = todoufukenName + this.ConvertTwoText(BETSU3.SHOBUN_JIGYOUJOU_ADDRESS1, BETSU3.SHOBUN_JIGYOUJOU_ADDRESS2);
                    dtTableHead.Rows[0]["ARTICLE02_04_SHOBUN_HOUHOU"] = SAISHUU_SBN_HOUHOU.SHOBUN_HOUHOU_NAME;
                    dtTableHead.Rows[0]["ARTICLE02_04_SHISETSU_CAPCITY"] = this.ConvertSpecUnit(BETSU3.SHISETSU_CAPACITY);
                }
            }

            if (noAdd)
            {
                row["FORMAT_LBL"] = title;
                if (rptNo == "R001_01Report")
                {
                    row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  運搬の最終目的地";
                }
                else if (rptNo == "R001_02Report")
                {
                    row["SAISHUU_SHOBUN_NAIYOU"] = "別紙  処分の場所、方法及び処理能力";
                }

                reportTable.Rows.Add(row);
                noAdd = false;
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        private DataTable CreateSubReport05Data(string title, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("SBN_NO", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_ADDRESS", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU", typeof(string));
            reportTable.Columns.Add("CAPACITY", typeof(string));

            // 委託された産業廃棄物の最終処分（予定）（別紙５）
            var row = reportTable.NewRow();
            if (this.dto.itakuKeiyakuBetsu4.Length > 7)
            {
                dtTableHead.Rows[0]["ARTICLE02_05_02_NOTE01_LBL"] = "※別紙  最終処分の場所、方法及び処理能力に記載";
                for (int i = 0; i < this.dto.itakuKeiyakuBetsu4.Length; i++)
                {
                    var BETSU4 = this.dto.itakuKeiyakuBetsu4[i];

                    // 運搬の最終目的地(現場)
                    var SAISHUU_SBN_GEN = new M_GENBA();
                    SAISHUU_SBN_GEN.GYOUSHA_CD = BETSU4.LAST_SHOBUN_GYOUSHA_CD;
                    SAISHUU_SBN_GEN.GENBA_CD = BETSU4.LAST_SHOBUN_JIGYOUJOU_CD;
                    SAISHUU_SBN_GEN = this.genbaDao.GetDataByCd(SAISHUU_SBN_GEN);
                    if (SAISHUU_SBN_GEN == null)
                    {
                        SAISHUU_SBN_GEN = new M_GENBA();
                    }

                    // 処分方法
                    var BETSU4_SHOBUN_HOUHOU = shobunHouhouDao.GetDataByCd(BETSU4.SHOBUN_HOUHOU_CD);
                    if (BETSU4_SHOBUN_HOUHOU == null)
                    {
                        BETSU4_SHOBUN_HOUHOU = new M_SHOBUN_HOUHOU();
                    }

                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (!SAISHUU_SBN_GEN.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(SAISHUU_SBN_GEN.GENBA_TODOUFUKEN_CD.ToString());
                    }
                    string tdhk = "";
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    row = reportTable.NewRow();
                    row["FORMAT_LBL"] = title;
                    row["SBN_NO"] = (string.IsNullOrWhiteSpace(SAISHUU_SBN_GEN.SHOBUNSAKI_NO)) ? "- 　　" : SAISHUU_SBN_GEN.SHOBUNSAKI_NO;
                    row["JIGYOJOU_NAME"] = this.ConvertTwoText(SAISHUU_SBN_GEN.GENBA_NAME1, SAISHUU_SBN_GEN.GENBA_NAME2);
                    row["JIGYOJOU_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU4.LAST_SHOBUN_JIGYOUJOU_ADDRESS1, BETSU4.LAST_SHOBUN_JIGYOUJOU_ADDRESS2);
                    row["SBN_HOUHOU"] = BETSU4_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME;
                    row["CAPACITY"] = this.ConvertSpecUnit(BETSU4.SHORI_SPEC);
                    reportTable.Rows.Add(row);
                }
            }
            else
            {
                dtTableHead.Rows[0]["ARTICLE02_05_02_NOTE01_LBL"] = string.Empty;
                for (int i = 0; i < this.dto.itakuKeiyakuBetsu4.Length; i++)
                {
                    var BETSU4 = this.dto.itakuKeiyakuBetsu4[i];

                    // 運搬の最終目的地(現場)
                    var SAISHUU_SBN_GEN = new M_GENBA();
                    SAISHUU_SBN_GEN.GYOUSHA_CD = BETSU4.LAST_SHOBUN_GYOUSHA_CD;
                    SAISHUU_SBN_GEN.GENBA_CD = BETSU4.LAST_SHOBUN_JIGYOUJOU_CD;
                    SAISHUU_SBN_GEN = this.genbaDao.GetDataByCd(SAISHUU_SBN_GEN);
                    if (SAISHUU_SBN_GEN == null)
                    {
                        SAISHUU_SBN_GEN = new M_GENBA();
                    }

                    // 処分方法
                    var BETSU4_SHOBUN_HOUHOU = shobunHouhouDao.GetDataByCd(BETSU4.SHOBUN_HOUHOU_CD);
                    if (BETSU4_SHOBUN_HOUHOU == null)
                    {
                        BETSU4_SHOBUN_HOUHOU = new M_SHOBUN_HOUHOU();
                    }

                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (!SAISHUU_SBN_GEN.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(SAISHUU_SBN_GEN.GENBA_TODOUFUKEN_CD.ToString());
                    }
                    string tdhk = "";
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }

                    dtTableHead.Rows[0]["SBN_NO" + (i + 1)] = (string.IsNullOrWhiteSpace(SAISHUU_SBN_GEN.SHOBUNSAKI_NO)) ? "- 　　　　" : SAISHUU_SBN_GEN.SHOBUNSAKI_NO;
                    dtTableHead.Rows[0]["JIGYOJOU_NAME" + (i + 1)] = this.ConvertTwoText(SAISHUU_SBN_GEN.GENBA_NAME1, SAISHUU_SBN_GEN.GENBA_NAME2);
                    dtTableHead.Rows[0]["JIGYOJOU_ADDRESS" + (i + 1)] = tdhk + this.ConvertTwoText(BETSU4.LAST_SHOBUN_JIGYOUJOU_ADDRESS1, BETSU4.LAST_SHOBUN_JIGYOUJOU_ADDRESS2);
                    dtTableHead.Rows[0]["SBN_HOUHOU" + (i + 1)] = BETSU4_SHOBUN_HOUHOU.SHOBUN_HOUHOU_NAME;
                    dtTableHead.Rows[0]["CAPACITY" + (i + 1)] = this.ConvertSpecUnit(BETSU4.SHORI_SPEC);
                }
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        private DataTable CreateSubReport06Data(string title, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("TSUMIKAE_HAIKI_SHURUI", typeof(string));
            reportTable.Columns.Add("TSUMIKAE_ADDRESS", typeof(string));
            reportTable.Columns.Add("TSUMIKAE_HOKAN_LIMIT", typeof(string));

            // 運搬の最終目的地（別紙６）
            var row = reportTable.NewRow();
            var TSUMIKAE_HAIKI_SHURUI = new System.Text.StringBuilder();
            var unit = new M_UNIT();
            string unitName = "";
            if (this.dto.itakuKeiyakuTsumikae.Length > 1)
            {
                dtTableHead.Rows[0]["ARTICLE02_05_04_NOTE01_LBL"] = "※別紙  積替保管に記載";
                //HINMEI_NAMEを メンバ毎に "、"で区切って印字する。
                foreach (var HINMEI in this.dto.itakuKeiyakuHinmei)
                {
                    if (HINMEI.TSUMIKAE)
                    {
                        if (TSUMIKAE_HAIKI_SHURUI.Length > 0)
                        {
                            TSUMIKAE_HAIKI_SHURUI.Append(Const.ItakuKeiyakuHoshuConstans.OUTPUT_SEPARATOR);
                        }
                        TSUMIKAE_HAIKI_SHURUI.Append(HINMEI.HINMEI_NAME);
                    }
                }

                for (int i = 0; i < this.dto.itakuKeiyakuTsumikae.Length; i++)
                {
                    var Tsumikae = this.dto.itakuKeiyakuTsumikae[i];
                    if (!Tsumikae.HOKAN_JOGEN_UNIT_CD.IsNull)
                    {
                        unit = this.unitDao.GetDataByCd(Tsumikae.HOKAN_JOGEN_UNIT_CD.Value);
                        unitName = " " + unit.UNIT_NAME;
                    }
                    else
                    {
                        unitName = "";
                    }

                    var gyousha = this.gyoushaDao.GetDataByCd(Tsumikae.UNPAN_GYOUSHA_CD);
                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (!gyousha.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(gyousha.GYOUSHA_TODOUFUKEN_CD.ToString());
                    }
                    string tdhk = "";
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }
                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    row = reportTable.NewRow();
                    row["FORMAT_LBL"] = title;
                    row["TSUMIKAE_HAIKI_SHURUI"] = TSUMIKAE_HAIKI_SHURUI.ToString();
                    row["TSUMIKAE_ADDRESS"] = tdhk + this.ConvertTwoText(Tsumikae.TSUMIKAE_HOKANBA_ADDRESS1, Tsumikae.TSUMIKAE_HOKANBA_ADDRESS2);
                    row["TSUMIKAE_HOKAN_LIMIT"] = this.ConvertSuuchi(Tsumikae.HOKAN_JOGEN) + unitName;
                    reportTable.Rows.Add(row);
                }
            }
            else
            {
                //HINMEI_NAMEを メンバ毎に "、"で区切って印字する。
                foreach (var HINMEI in this.dto.itakuKeiyakuHinmei)
                {
                    if (HINMEI.TSUMIKAE)
                    {
                        if (TSUMIKAE_HAIKI_SHURUI.Length > 0)
                        {
                            TSUMIKAE_HAIKI_SHURUI.Append(Const.ItakuKeiyakuHoshuConstans.OUTPUT_SEPARATOR);
                        }
                        TSUMIKAE_HAIKI_SHURUI.Append(HINMEI.HINMEI_NAME);
                    }
                }

                dtTableHead.Rows[0]["ARTICLE02_05_04_NOTE01_LBL"] = string.Empty;
                for (int i = 0; i < this.dto.itakuKeiyakuTsumikae.Length; i++)
                {
                    var Tsumikae = this.dto.itakuKeiyakuTsumikae[i];
                    if (!Tsumikae.HOKAN_JOGEN_UNIT_CD.IsNull)
                    {
                        unit = this.unitDao.GetDataByCd(Tsumikae.HOKAN_JOGEN_UNIT_CD.Value);
                        unitName = " " + unit.UNIT_NAME;
                    }
                    else
                    {
                        unitName = "";
                    }

                    var gyousha = this.gyoushaDao.GetDataByCd(Tsumikae.UNPAN_GYOUSHA_CD);
                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (!gyousha.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(gyousha.GYOUSHA_TODOUFUKEN_CD.ToString());
                    }
                    string tdhk = "";
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    dtTableHead.Rows[0]["ARTICLE02_05_03_HAIKI_SHURUI"] = TSUMIKAE_HAIKI_SHURUI.ToString();
                    dtTableHead.Rows[0]["ARTICLE02_05_03_ADDRESS"] = tdhk + this.ConvertTwoText(Tsumikae.TSUMIKAE_HOKANBA_ADDRESS1, Tsumikae.TSUMIKAE_HOKANBA_ADDRESS2);
                    dtTableHead.Rows[0]["ARTICLE02_05_03_HOKAN_LIMIT"] = this.ConvertSuuchi(Tsumikae.HOKAN_JOGEN) + unitName;
                }
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="DataTable">帳票用データ</returns>
        /// <returns name="searchKbn">0:全て、1:収集・運搬、2:処分</returns>
        /// <returns name="title">タイトル</returns>
        private DataTable CreateSubReport07Data(string title, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("HAIKIBUTSU_SHURUI1", typeof(string));
            reportTable.Columns.Add("HAIKIBUTSU_SHURUI2", typeof(string));
            reportTable.Columns.Add("TEIJI_JIKI1", typeof(string));
            reportTable.Columns.Add("TEIJI_JIKI2", typeof(string));

            // 委託する産業廃棄物の種類、数量及び単価（別紙３）
            var HINMEI_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
            {
                var HiNMEI7 = this.dto.itakuKeiyakuHinmei[i];
                if (!string.IsNullOrEmpty(HiNMEI7.BUNSEKISHOUMEISHO_TEIJIJIKI))
                {
                    HINMEI_LIST.Add(this.dto.itakuKeiyakuHinmei[i]);
                }
            }

            var row = reportTable.NewRow();
            var noAdd = true;
            if (HINMEI_LIST.Count > 3)
            {
                dtTableHead.Rows[0]["ARTICLE03_06_NOTE01_LBL"] = "※別紙  適正処理に必要な情報の提供に記載";
                for (int i = 0; i < HINMEI_LIST.Count; i++)
                {
                    var HINMEI = HINMEI_LIST[i];

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (i % 2 == 0)
                    {
                        row = reportTable.NewRow();
                        noAdd = true;
                        row["FORMAT_LBL"] = title;
                        row["HAIKIBUTSU_SHURUI1"] = HINMEI.HINMEI_NAME;
                        row["TEIJI_JIKI1"] = HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                    }
                    else
                    {
                        row["HAIKIBUTSU_SHURUI2"] = HINMEI.HINMEI_NAME;
                        row["TEIJI_JIKI2"] = HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                        reportTable.Rows.Add(row);
                        noAdd = false;
                    }
                }
            }
            else
            {
                dtTableHead.Rows[0]["ARTICLE03_06_NOTE01_LBL"] = string.Empty;
                for (int i = 0; i < HINMEI_LIST.Count; i++)
                {
                    var HINMEI = HINMEI_LIST[i];

                    ///**********************************************************************/
                    /// 行データ
                    ///**********************************************************************/
                    if (i == 0)
                    {
                        dtTableHead.Rows[0]["ARTICLE03_05_HAIKIBUTSU_SHURUI01"] = HINMEI.HINMEI_NAME;
                        dtTableHead.Rows[0]["ARTICLE03_05_TEIJI_JIKI01"] = HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                    }
                    else if (i == 1)
                    {
                        dtTableHead.Rows[0]["ARTICLE03_05_HAIKIBUTSU_SHURUI02"] = HINMEI.HINMEI_NAME;
                        dtTableHead.Rows[0]["ARTICLE03_05_TEIJI_JIKI02"] = HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                    }
                    else
                    {
                        dtTableHead.Rows[0]["ARTICLE03_05_HAIKIBUTSU_SHURUI03"] = HINMEI.HINMEI_NAME;
                        dtTableHead.Rows[0]["ARTICLE03_05_TEIJI_JIKI03"] = HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                    }
                }
            }

            if (noAdd)
            {
                row["FORMAT_LBL"] = title;
                reportTable.Rows.Add(row);
                noAdd = false;
            }

            return reportTable;
        }

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        /// <returns name="tokubetsuFlg">特管フラグ</returns>
        private DataTable CreateSubReport08Data(bool tokubetsuFlg, string title, ref DataTable dtTableHead)
        {
            DataTable reportTable = new DataTable();
            var headerTitle1 = string.Empty;
            var headerTitle2 = tokubetsuFlg ? "〔特管〕" : "〔産廃〕";

            ///**********************************************************************/
            /// カラム追加
            ///**********************************************************************/
            reportTable = new DataTable();
            reportTable.Columns.Add("FORMAT_LBL", typeof(string));
            reportTable.Columns.Add("SAISHUU_SHOBUN_NAIYOU", typeof(string));
            reportTable.Columns.Add("SHOBUN_SHIMEI", typeof(string));
            reportTable.Columns.Add("SHOBUN_ADDRESS", typeof(string));
            reportTable.Columns.Add("KYOKA_TODOUHUKEN", typeof(string));
            reportTable.Columns.Add("KYOKA_KIGEN", typeof(string));
            reportTable.Columns.Add("JIGYOU_HANI", typeof(string));
            reportTable.Columns.Add("KYOKA_JOKEN", typeof(string));
            reportTable.Columns.Add("KYOKA_BANGOU", typeof(string));

            // 収集運搬に関する事業範囲
            var UPN_KYOKA_CHIIKI1 = new M_CHIIKI();
            var UPN_KYOKA_CHIIKI2 = new M_CHIIKI();

            // 委託する産業廃棄物の種類、数量及び単価（別紙３）
            var KYOKASHO_LIST = new List<M_ITAKU_UPN_KYOKASHO>();
            var BETSU2 = new List<M_ITAKU_KEIYAKU_BETSU2>();
            var Kyokasho = new M_ITAKU_UPN_KYOKASHO();
            for (int i = 0; i < this.dto.itakuKeiyakuBetsu2.Length; i++)
            {
                var BETSU2_FST = this.dto.itakuKeiyakuBetsu2[i];

                int cnt = 0;
                for (int j = 0; j < this.dto.itakuUpnKyokasho.Length; j++)
                {
                    Kyokasho = this.dto.itakuUpnKyokasho[j];

                    if (BETSU2_FST.UNPAN_GYOUSHA_CD == Kyokasho.GYOUSHA_CD)
                    {
                        if (!tokubetsuFlg && Kyokasho.KYOKA_KBN == 1 ||
                            tokubetsuFlg && Kyokasho.KYOKA_KBN == 2)
                        {
                            cnt++;
                            KYOKASHO_LIST.Add(Kyokasho);
                            BETSU2.Add(BETSU2_FST);
                        }
                    }
                }
                if (cnt == 0)
                {
                    KYOKASHO_LIST.Add(null);
                    BETSU2.Add(BETSU2_FST);
                }
            }

            var row = reportTable.NewRow();
            var noAdd = true;

            if (KYOKASHO_LIST.Count > 1)
            {
                if (!tokubetsuFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_06_02_NOTE01_LBL"] = "※別紙　搬入業者〔産廃〕に記載";

                    for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                    {
                        var tempKyokasho = KYOKASHO_LIST[i];
                        // 別表2(運搬)
                        var UNPAN_GYOUSHA = this.gyoushaDao.GetDataByCd(BETSU2[i].UNPAN_GYOUSHA_CD);
                        if (UNPAN_GYOUSHA == null)
                        {
                            UNPAN_GYOUSHA = new M_GYOUSHA();
                        }

                        if (tempKyokasho != null)
                        {
                            UPN_KYOKA_CHIIKI1 = this.chiikiDao.GetDataByCd(tempKyokasho.CHIIKI_CD);
                        }

                        M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                        if (!UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
                        {
                            todoufuken = this.todoufukenDao.GetDataByCd(UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
                        }
                        string tdhk = "";
                        if (todoufuken != null)
                        {
                            tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }

                        ///**********************************************************************/
                        /// 行データ
                        ///**********************************************************************/
                        row = reportTable.NewRow();
                        row["FORMAT_LBL"] = title;
                        row["SAISHUU_SHOBUN_NAIYOU"] = "別紙　搬入業者〔産廃〕";
                        row["SHOBUN_SHIMEI"] = this.ConvertTwoText(UNPAN_GYOUSHA.GYOUSHA_NAME1, UNPAN_GYOUSHA.GYOUSHA_NAME2);
                        row["SHOBUN_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU2[i].UNPAN_GYOUSHA_ADDRESS1, BETSU2[i].UNPAN_GYOUSHA_ADDRESS2);

                        if (tempKyokasho != null)
                        {
                            row["KYOKA_TODOUHUKEN"] = UPN_KYOKA_CHIIKI1.CHIIKI_NAME;
                            row["KYOKA_KIGEN"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            row["JIGYOU_HANI"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            row["KYOKA_JOKEN"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            row["KYOKA_BANGOU"] = this.ConvertKyokaNo(tempKyokasho.KYOKA_NO);
                        }
                        else
                        {
                            row["KYOKA_TODOUHUKEN"] = string.Empty;
                            row["KYOKA_KIGEN"] = string.Empty;
                            row["JIGYOU_HANI"] = string.Empty;
                            row["KYOKA_JOKEN"] = string.Empty;
                            row["KYOKA_BANGOU"] = string.Empty;
                        }
                        reportTable.Rows.Add(row);
                        noAdd = false;
                    }
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_06_03_NOTE01_LBL"] = "※別紙　搬入業者〔特管〕に記載";
                    for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                    {
                        var tempKyokasho = KYOKASHO_LIST[i];

                        var UNPAN_GYOUSHA = this.gyoushaDao.GetDataByCd(BETSU2[i].UNPAN_GYOUSHA_CD);
                        if (UNPAN_GYOUSHA == null)
                        {
                            UNPAN_GYOUSHA = new M_GYOUSHA();
                        }

                        if (tempKyokasho != null)
                        {
                            UPN_KYOKA_CHIIKI2 = this.chiikiDao.GetDataByCd(tempKyokasho.CHIIKI_CD);
                        }

                        M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                        if (!UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
                        {
                            todoufuken = this.todoufukenDao.GetDataByCd(UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
                        }
                        string tdhk = "";
                        if (todoufuken != null)
                        {
                            tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }

                        ///**********************************************************************/
                        /// 行データ
                        ///**********************************************************************/
                        row = reportTable.NewRow();
                        row["FORMAT_LBL"] = title;
                        row["SAISHUU_SHOBUN_NAIYOU"] = "別紙　搬入業者〔特管〕";
                        row["SHOBUN_SHIMEI"] = this.ConvertTwoText(UNPAN_GYOUSHA.GYOUSHA_NAME1, UNPAN_GYOUSHA.GYOUSHA_NAME2);
                        row["SHOBUN_ADDRESS"] = tdhk + this.ConvertTwoText(BETSU2[i].UNPAN_GYOUSHA_ADDRESS1, BETSU2[i].UNPAN_GYOUSHA_ADDRESS2);
                        if (tempKyokasho != null)
                        {
                            row["KYOKA_TODOUHUKEN"] = UPN_KYOKA_CHIIKI2.CHIIKI_NAME;
                            row["KYOKA_KIGEN"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            row["JIGYOU_HANI"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            row["KYOKA_JOKEN"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            row["KYOKA_BANGOU"] = this.ConvertKyokaNo(tempKyokasho.KYOKA_NO);
                        }
                        else
                        {
                            row["KYOKA_TODOUHUKEN"] = string.Empty;
                            row["KYOKA_KIGEN"] = string.Empty;
                            row["JIGYOU_HANI"] = string.Empty;
                            row["KYOKA_JOKEN"] = string.Empty;
                            row["KYOKA_BANGOU"] = string.Empty;
                        }
                        reportTable.Rows.Add(row);
                        noAdd = false;
                    }
                }
            }
            else
            {
                if (!tokubetsuFlg)
                {
                    dtTableHead.Rows[0]["ARTICLE02_06_03_NOTE01_LBL"] = string.Empty;

                    for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                    {
                        var tempKyokasho = KYOKASHO_LIST[i];

                        var UNPAN_GYOUSHA = this.gyoushaDao.GetDataByCd(BETSU2[i].UNPAN_GYOUSHA_CD);
                        if (UNPAN_GYOUSHA == null)
                        {
                            UNPAN_GYOUSHA = new M_GYOUSHA();
                        }

                        if (tempKyokasho != null)
                        {
                            UPN_KYOKA_CHIIKI1 = this.chiikiDao.GetDataByCd(tempKyokasho.CHIIKI_CD);
                        }

                        M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                        if (!UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
                        {
                            todoufuken = this.todoufukenDao.GetDataByCd(UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
                        }
                        string tdhk = "";
                        if (todoufuken != null)
                        {
                            tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }

                        ///**********************************************************************/
                        /// 行データ
                        ///**********************************************************************/
                        dtTableHead.Rows[0]["ARTICLE02_06_01_NAME"]
                            = this.ConvertTwoText(UNPAN_GYOUSHA.GYOUSHA_NAME1, UNPAN_GYOUSHA.GYOUSHA_NAME2);
                        dtTableHead.Rows[0]["ARTICLE02_06_01_ADDRESS"]
                            = tdhk + this.ConvertTwoText(BETSU2[i].UNPAN_GYOUSHA_ADDRESS1, BETSU2[i].UNPAN_GYOUSHA_ADDRESS2);
                        if (tempKyokasho != null)
                        {
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_TODOUHUKEN01"] = UPN_KYOKA_CHIIKI1.CHIIKI_NAME;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_KIGEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_JIGYOU_HANI01"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_JOKEN01"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_BANGOU01"] = this.ConvertKyokaNo(tempKyokasho.KYOKA_NO);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_TODOUHUKEN01"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_KIGEN01"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_JIGYOU_HANI01"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_JOKEN01"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_BANGOU01"] = string.Empty;
                        }
                    }
                }
                else
                {
                    dtTableHead.Rows[0]["ARTICLE02_06_03_NOTE01_LBL"] = string.Empty;
                    for (int i = 0; i < KYOKASHO_LIST.Count; i++)
                    {
                        var tempKyokasho = KYOKASHO_LIST[i];

                        var UNPAN_GYOUSHA = this.gyoushaDao.GetDataByCd(BETSU2[i].UNPAN_GYOUSHA_CD);
                        if (UNPAN_GYOUSHA == null)
                        {
                            UNPAN_GYOUSHA = new M_GYOUSHA();
                        }

                        if (tempKyokasho != null)
                        {
                            UPN_KYOKA_CHIIKI2 = this.chiikiDao.GetDataByCd(tempKyokasho.CHIIKI_CD);
                        }

                        M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                        if (!UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
                        {
                            todoufuken = this.todoufukenDao.GetDataByCd(UNPAN_GYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
                        }
                        string tdhk = "";
                        if (todoufuken != null)
                        {
                            tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }

                        ///**********************************************************************/
                        /// 行データ
                        ///**********************************************************************/
                        dtTableHead.Rows[0]["ARTICLE02_06_01_NAME"]
                            = this.ConvertTwoText(UNPAN_GYOUSHA.GYOUSHA_NAME1, UNPAN_GYOUSHA.GYOUSHA_NAME2);
                        dtTableHead.Rows[0]["ARTICLE02_06_01_ADDRESS"]
                            = tdhk + this.ConvertTwoText(BETSU2[i].UNPAN_GYOUSHA_ADDRESS1, BETSU2[i].UNPAN_GYOUSHA_ADDRESS2);
                        if (tempKyokasho != null)
                        {
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_TODOUHUKEN02"] = UPN_KYOKA_CHIIKI2.CHIIKI_NAME;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_KIGEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOUKA_KIGEN;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_JIGYOU_HANI02"] = Const.ItakuKeiyakuHoshuConstans.JIGYOU_HANI;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_JOKEN02"] = Const.ItakuKeiyakuHoshuConstans.KYOKA_JOUKEN;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_BANGOU02"] = this.ConvertKyokaNo(tempKyokasho.KYOKA_NO);
                        }
                        else
                        {
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_TODOUHUKEN02"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_KIGEN02"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_JIGYOU_HANI02"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_JOKEN02"] = string.Empty;
                            dtTableHead.Rows[0]["ARTICLE02_06_01_KYOKA_BANGOU02"] = string.Empty;
                        }
                    }
                }
            }

            if (noAdd)
            {
                row["FORMAT_LBL"] = title;
                if (!tokubetsuFlg)
                {
                    row["SAISHUU_SHOBUN_NAIYOU"] = "別紙　搬入業者〔産廃〕";
                }
                else
                {
                    row["SAISHUU_SHOBUN_NAIYOU"] = "別紙　搬入業者〔特管〕";
                }
                reportTable.Rows.Add(row);
                noAdd = false;
            }

            return reportTable;
        }

        /// <summary>
        /// 処理能力フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSpecUnit(string value)
        {
            return value;
        }

        /// <summary>
        /// 数値フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSuuchi(SqlDecimal value)
        {
            string result = string.Empty;
            string format = "#,###.#";
            if (!value.IsNull)
            {
                result = value.Value.ToString(format);
            }
            return result;
        }

        /// <summary>
        /// 数量フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertSuuryo(SqlDecimal value)
        {
            string result = string.Empty;
            string format = "#,###.#";
            if (!string.IsNullOrEmpty(this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT))
            {
                format = this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT;
            }
            if (!value.IsNull)
            {
                result = value.Value.ToString(format);
            }
            return result;
        }

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertTanka(SqlDecimal value)
        {
            string result = string.Empty;
            string format = "#,###.#";
            if (!string.IsNullOrEmpty(this.sysInfoEntity.ITAKU_KEIYAKU_TANKA_FORMAT))
            {
                format = this.sysInfoEntity.ITAKU_KEIYAKU_TANKA_FORMAT;
            }
            if (!value.IsNull)
            {
                result = value.Value.ToString(format);
            }
            return result;
        }

        /// <summary>
        /// 日付フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertHiduke(SqlDateTime value)
        {
            string result = string.Empty;
            string format = Const.ItakuKeiyakuHoshuConstans.OUTPUT_DATE_FORMAT;
            if (!value.IsNull)
            {
                result = value.Value.ToString(format);
            }
            return result;
        }

        /// <summary>
        /// 許可番号フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertKyokaNo(string value)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                result = string.Format("第{0}号", value);
            }
            return result;
        }

        /// <summary>
        /// 2テキスト結合
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        private string ConvertTwoText(string text1, string text2)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(text1))
            {
                result += text1;
            }
            if (!string.IsNullOrEmpty(text2))
            {
                if (!string.IsNullOrEmpty(result))
                {
                    result += System.Environment.NewLine;
                }
                result += text2;
            }
            return result;
        }

        /// <summary>
        /// 現場から許可情報と地域を取得
        /// </summary>
        /// <param name="kyokaKbn">許可区分</param>
        /// <param name="genba">現場</param>
        /// <param name="chiiki">地域</param>
        /// <param name="kyoka">許可</param>
        private DataTable GetKyokaByGenba(short kyokaKbn, M_GENBA genba, ref M_CHIIKI chiiki, ref M_CHIIKIBETSU_KYOKA kyoka)
        {
            var result = new DataTable();

            // 地域
            chiiki = this.chiikiDao.GetDataByCd(genba.CHIIKI_CD);
            if (chiiki == null)
            {
                chiiki = new M_CHIIKI();
            }

            // 許可情報
            kyoka = new M_CHIIKIBETSU_KYOKA();
            kyoka.KYOKA_KBN = kyokaKbn;
            kyoka.GYOUSHA_CD = genba.GYOUSHA_CD;
            kyoka.GENBA_CD = genba.GENBA_CD;
            kyoka.CHIIKI_CD = genba.CHIIKI_CD;
            kyoka = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(kyoka);

            if (kyoka != null)
            {
                result = this.chiikibetsuKyokaDao.GetDataBySqlFile(GET_CHIIKIBETSU_KYOKA_MEIGARA_SQL, kyoka);
            }

            return result;
        }

        /// <summary>
        /// 許可情報テーブルより産業廃棄物の種類、許可番号を取得
        /// </summary>
        /// <param name="dtKyoka">許可情報テーブル</param>
        /// <param name="meigara">産業廃棄物の種類</param>
        /// <param name="number">許可番号</param>
        /// <param name="kigen">許可期限</param>
        private void GetKyokaDetail(DataTable dtKyoka, out string meigara, out string number, out string kigen)
        {
            var meigaraTokubetsu = string.Empty;
            var numberTokubetsu = string.Empty;
            var kigenTokubetsu = string.Empty;
            meigara = string.Empty;
            number = string.Empty;
            kigen = string.Empty;

            if (dtKyoka.Rows.Count == 0)
            {
                return;
            }

            // 産業廃棄物の種類[普通]
            this.GetKyokaDetailFutsu(dtKyoka, out meigara, out number, out kigen);
            if (!string.IsNullOrEmpty(meigara))
            {
                meigara = "[普通] : " + meigara;
            }
            if (!string.IsNullOrEmpty(number))
            {
                number = "[普通] : " + number;
            }
            if (!string.IsNullOrEmpty(kigen))
            {
                kigen = "[普通] : " + kigen;
            }

            // 産業廃棄物の種類[特別]
            this.GetKyokaDetailFutsu(dtKyoka, out meigaraTokubetsu, out numberTokubetsu, out kigenTokubetsu);
            if (!string.IsNullOrEmpty(meigara) && !string.IsNullOrEmpty(meigaraTokubetsu))
            {
                meigara += System.Environment.NewLine + "[特別] : " + meigaraTokubetsu;
            }
            if (!string.IsNullOrEmpty(number) && !string.IsNullOrEmpty(numberTokubetsu))
            {
                number += System.Environment.NewLine + "[特別] : " + numberTokubetsu;
            }
            if (!string.IsNullOrEmpty(kigen) && !string.IsNullOrEmpty(kigenTokubetsu))
            {
                kigen += System.Environment.NewLine + "[特別] : " + kigenTokubetsu;
            }

            if (string.IsNullOrEmpty(meigara))
            {
                meigara += "[特別] : " + meigaraTokubetsu;
            }
            if (string.IsNullOrEmpty(number))
            {
                number += "[特別] : " + numberTokubetsu;
            }
            if (string.IsNullOrEmpty(kigen))
            {
                kigen += "[特別] : " + kigenTokubetsu;
            }
        }

        /// <summary>
        /// 許可情報テーブルより産業廃棄物の種類、許可番号を取得
        /// </summary>
        /// <param name="dtKyoka">許可情報テーブル</param>
        /// <param name="meigara">産業廃棄物の種類</param>
        /// <param name="number">許可番号</param>
        /// <param name="kigen">許可期限</param>
        private void GetKyokaDetailFutsu(DataTable dtKyoka, out string meigara, out string number, out string kigen)
        {
            meigara = string.Empty;
            number = string.Empty;
            kigen = string.Empty;

            DateTime dtTemp;

            if (dtKyoka.Rows.Count == 0)
            {
                return;
            }

            // 産業廃棄物の種類[普通]
            var meigaraStrB = new System.Text.StringBuilder();
            var rows = dtKyoka.Select("TOKUBETSU_KANRI_KBN = 0");
            if (rows.Length > 0)
            {
                // 許可番号[普通]
                number += this.ConvertKyokaNo((string)rows[0]["KYOKA_NO"]);
                // 許可期限[普通]
                if (rows[0]["KYOKA_END"] != null && DateTime.TryParse(rows[0]["KYOKA_END"].ToString(), out dtTemp))
                {
                    kigen += dtTemp.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_DATE_FORMAT);
                }
            }
            foreach (var rowKyoka in rows)
            {
                var strTemp = (string)rowKyoka["HOUKOKUSHO_BUNRUI_NAME_RYAKU"];
                if (meigaraStrB.Length > 0)
                {
                    meigaraStrB.Append(Const.ItakuKeiyakuHoshuConstans.OUTPUT_SEPARATOR);
                }
                meigaraStrB.Append(strTemp);
            }
            if (meigaraStrB.Length > 0)
            {
                meigara += meigaraStrB.ToString();
            }
        }

        /// <summary>
        /// 許可情報テーブルより産業廃棄物の種類、許可番号を取得
        /// </summary>
        /// <param name="dtKyoka">許可情報テーブル</param>
        /// <param name="meigara">産業廃棄物の種類</param>
        /// <param name="number">許可番号</param>
        /// <param name="kigen">許可期限</param>
        private void GetKyokaDetailTokubetsu(DataTable dtKyoka, out string meigara, out string number, out string kigen)
        {
            meigara = string.Empty;
            number = string.Empty;
            kigen = string.Empty;

            DateTime dtTemp;

            if (dtKyoka.Rows.Count == 0)
            {
                return;
            }

            // 産業廃棄物の種類[特別]
            var meigaraStrB = new System.Text.StringBuilder();
            var rows = dtKyoka.Select("TOKUBETSU_KANRI_KBN = 1");
            if (rows.Length > 0)
            {
                // 許可番号[特管]
                number += this.ConvertKyokaNo((string)rows[0]["KYOKA_NO"]);
                // 許可期限[特管]
                if (rows[0]["KYOKA_END"] != null && DateTime.TryParse(rows[0]["KYOKA_END"].ToString(), out dtTemp))
                {
                    kigen += dtTemp.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_DATE_FORMAT);
                }
            }
            foreach (var rowKyoka in rows)
            {
                var strTemp = (string)rowKyoka["HOUKOKUSHO_BUNRUI_NAME_RYAKU"];
                if (meigaraStrB.Length > 0)
                {
                    meigaraStrB.Append(Const.ItakuKeiyakuHoshuConstans.OUTPUT_SEPARATOR);
                }
                meigaraStrB.Append(strTemp);
            }
            if (meigaraStrB.Length > 0)
            {
                meigara += meigaraStrB.ToString();
            }
        }

        /// <summary>
        /// 事業区分取得
        /// </summary>
        /// <param name="kyoka"></param>
        /// <returns></returns>
        private string GetJigyouKbn(M_CHIIKIBETSU_KYOKA kyoka)
        {
            var result = string.Empty;
            if (kyoka == null)
            {
                return result;
            }

            if (kyoka.KYOKA_KBN == 1)
            {
                result = "運搬";
            }
            else if (kyoka.KYOKA_KBN == 2)
            {
                result = "処分";
            }
            else if (kyoka.KYOKA_KBN == 3)
            {
                result = "最終処分";
            }

            return result;
        }

        /// <summary>
        /// 年間にマッチする期間のみ返却する。その他は空を返す。
        /// </summary>
        /// <param name="smallDate"></param>
        /// <param name="bigDate"></param>
        /// <returns></returns>
        private string GetDateYearSpan(DateTime smallDate, DateTime bigDate)
        {
            var result = string.Empty;
            int iYear;

            // 年を算出
            iYear = bigDate.Year - smallDate.Year;
            if (smallDate.Month == 1 && smallDate.Day == 1)
            {
                iYear = iYear + 1;
            }
            smallDate = smallDate.AddYears(iYear);
            smallDate = smallDate.AddDays(-1);

            if (bigDate.Month == smallDate.Month && bigDate.Day == smallDate.Day)
            {
                // 年間の値を返す。
                result = iYear.ToString();
            }
            return result;
        }

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理の事前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistBefore(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // グリッドの入力中データがある場合は確定処理を行っておく
            if (this.form.listKihonHstGenba.Focused)
            {
                this.form.MoveFocus();
                this.form.listKihonHstGenba.Focus();
                Application.DoEvents();
            }
            if (this.form.listHinmei.Focused)
            {
                this.form.MoveFocus();
                this.form.listHinmei.Focus();
                Application.DoEvents();
            }
            if (this.form.listHoukokushoBunrui.Focused)
            {
                this.form.MoveFocus();
                this.form.listHoukokushoBunrui.Focus();
                Application.DoEvents();
            }
            if (this.form.listBetsu2.Focused)
            {
                this.form.MoveFocus();
                this.form.listBetsu2.Focus();
                Application.DoEvents();
            }
            if (this.form.listBetsu3.Focused)
            {
                this.form.MoveFocus();
                this.form.listBetsu3.Focus();
                Application.DoEvents();
            }
            if (this.form.listBetsu4.Focused)
            {
                this.form.MoveFocus();
                this.form.listBetsu4.Focus();
                Application.DoEvents();
            }
            if (this.form.listOboe.Focused)
            {
                this.form.MoveFocus();
                this.form.listOboe.Focus();
                Application.DoEvents();
            }

            // 必須チェックを実施するため、排出事業者・排出事業場を一時的に有効とする
            //this.form.HAISHUTSU_JIGYOUSHA_CD.Enabled = true;
            //this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録処理の事後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistAfter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 事前処理の適用を選択されているタブに応じて解除する
            this.TabSelect();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                if (errorFlag)
                {
                    return;
                }

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    // 委託契約基本マスタ登録
                    this.kihonDao.Insert(this.dto.ItakuKeiyakuKihon);

                    // 委託契約基本_排出現場マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuKihonHstGenba.Length; i++)
                    {
                        this.kihonHstGenbaDao.Insert(this.dto.itakuKeiyakuKihonHstGenba[i]);
                    }

                    // 委託契約別表1排出マスタ登録(報告書分類)
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu1Hst.Length; i++)
                    {
                        this.betsu1HstDao.Insert(this.dto.itakuKeiyakuBetsu1Hst[i]);
                    }

                    // 委託契約--品名マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
                    {
                        this.hinmeiDao.Insert(this.dto.itakuKeiyakuHinmei[i]);
                    }

                    // 委託契約--報告書分類マスタ登録
                    /*for (int i = 0; i < this.dto.itakuKeiyakuBetsu1Yotei.Length; i++)
                    {
                        this.betsu1YoteiDao.Insert(this.dto.itakuKeiyakuBetsu1Yotei[i]);
                    }*/

                    // 委託契約--運搬マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu2.Length; i++)
                    {
                        this.betsu2Dao.Insert(this.dto.itakuKeiyakuBetsu2[i]);
                    }

                    // 委託契約--積替マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuTsumikae.Length; i++)
                    {
                        this.tsumikaeDao.Insert(this.dto.itakuKeiyakuTsumikae[i]);
                    }

                    // 委託契約--処分マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu3.Length; i++)
                    {
                        this.betsu3Dao.Insert(this.dto.itakuKeiyakuBetsu3[i]);
                    }

                    // 委託契約--最終マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu4.Length; i++)
                    {
                        this.betsu4Dao.Insert(this.dto.itakuKeiyakuBetsu4[i]);
                    }

                    // 委託契約覚書マスタ登録
                    for (int i = 0; i < this.dto.itakuKeiyakuOboe.Length; i++)
                    {
                        this.oboeDao.Insert(this.dto.itakuKeiyakuOboe[i]);
                    }

                    // 委託契約運搬許可証紐付マスタ登録
                    for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
                    {
                        this.upnKyokashoDao.Insert(this.dto.itakuUpnKyokasho[i]);
                    }

                    // 委託契約処分許可証紐付マスタ登録
                    for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
                    {
                        this.sbnKyokashoDao.Insert(this.dto.itakuSbnKyokasho[i]);
                    }

                    // 委託契約電子送付先マスタ登録
                    if (AppConfig.AppOptions.IsDenshiKeiyaku())
                    {
                        for (int i = 0; i < this.dto.itakuKeiyakuDenshiSouhusaki.Length; i++)
                        {
                            this.denshiKeiyakuSouhusakiDao.Insert(this.dto.itakuKeiyakuDenshiSouhusaki[i]);
                        }
                    }

                    // トランザクション終了
                    tran.Commit();
                }

                if (this.registMsgFlg)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
                this.isRegist = true;
                this.isFileUploadOK = true;

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (errorFlag)
                {
                    return;
                }

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    // 委託契約基本マスタ更新
                    this.kihonDao.Update(this.dto.ItakuKeiyakuKihon);

                    // 委託契約基本マスタの該当システムIDデータ全削除
                    this.PhysicalDelete();

                    // 委託契約基本_排出現場マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuKihonHstGenba.Length; i++)
                    {
                        this.kihonHstGenbaDao.Insert(this.dto.itakuKeiyakuKihonHstGenba[i]);
                    }

                    // 委託契約別表1排出マスタ更新(報告書分類)
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu1Hst.Length; i++)
                    {
                        this.betsu1HstDao.Insert(this.dto.itakuKeiyakuBetsu1Hst[i]);
                    }

                    //委託契約_品名マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
                    {
                        this.hinmeiDao.Insert(this.dto.itakuKeiyakuHinmei[i]);
                    }

                    // 委託契約_報告書分類マスタ更新
                    /*for (int i = 0; i < this.dto.itakuKeiyakuBetsu1Yotei.Length; i++)
                    {
                        this.betsu1YoteiDao.Insert(this.dto.itakuKeiyakuBetsu1Yotei[i]);
                    }*/

                    // 委託契約_運搬マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu2.Length; i++)
                    {
                        this.betsu2Dao.Insert(this.dto.itakuKeiyakuBetsu2[i]);
                    }

                    // 委託契約_積替マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuTsumikae.Length; i++)
                    {
                        this.tsumikaeDao.Insert(this.dto.itakuKeiyakuTsumikae[i]);
                    }

                    // 委託契約_処分マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu3.Length; i++)
                    {
                        this.betsu3Dao.Insert(this.dto.itakuKeiyakuBetsu3[i]);
                    }

                    // 委託契約_最終マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuBetsu4.Length; i++)
                    {
                        this.betsu4Dao.Insert(this.dto.itakuKeiyakuBetsu4[i]);
                    }

                    // 委託契約覚書マスタ更新
                    for (int i = 0; i < this.dto.itakuKeiyakuOboe.Length; i++)
                    {
                        this.oboeDao.Insert(this.dto.itakuKeiyakuOboe[i]);
                    }

                    // 委託契約運搬許可証紐付マスタ更新
                    for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
                    {
                        this.upnKyokashoDao.Insert(this.dto.itakuUpnKyokasho[i]);
                    }

                    // 委託契約処分許可証紐付マスタ更新
                    for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
                    {
                        this.sbnKyokashoDao.Insert(this.dto.itakuSbnKyokasho[i]);
                    }

                    // 委託契約電子送付先マスタ登録
                    if (AppConfig.AppOptions.IsDenshiKeiyaku())
                    {
                        for (int i = 0; i < this.dto.itakuKeiyakuDenshiSouhusaki.Length; i++)
                        {
                            this.denshiKeiyakuSouhusakiDao.Insert(this.dto.itakuKeiyakuDenshiSouhusaki[i]);
                        }
                    }

                    // トランザクション終了
                    tran.Commit();
                }

                if (this.registMsgFlg)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "更新");
                }
                this.isRegist = true;

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //var result = msgLogic.MessageBoxShow("C026");
                //if (result == DialogResult.Yes)
                //{

                //INXS check for delete INXS data start  refs #158006
                bool isUploadToInxs = false;
                if (AppConfig.AppOptions.IsInxsItaku())
                {
                    isUploadToInxs = this.inxsContractLogic.CheckIsUploadContractToInxs(this.dto.ItakuKeiyakuKihon.SYSTEM_ID);
                    if (isUploadToInxs && msgLogic.MessageBoxShow("C120") == DialogResult.No)
                    {
                        this.form.RegistErrorFlag = true;
                        return;
                    }
                }
                //INXS end

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    this.dto.ItakuKeiyakuKihon.DELETE_FLG = true;

                    this.kihonDao.Update(this.dto.ItakuKeiyakuKihon);

                    // ファイルデータ削除
                    var list = fileLinkItakuKeiyakuKihonDao.GetDataBySystemId(this.dto.ItakuKeiyakuKihon.SYSTEM_ID);
                    if (list != null && 0 < list.Count)
                    {
                        // ファイルデータを物理削除する。
                        var fileIdList = list.Select(n => n.FILE_ID.Value).ToList();
                        this.uploadLogic.DeleteFileData(fileIdList);

                        // 連携データ削除
                        string sql = string.Format("DELETE FROM M_FILE_LINK_ITAKU_KEIYAKU_KIHON WHERE SYSTEM_ID = {0}", this.dto.ItakuKeiyakuKihon.SYSTEM_ID);
                        fileLinkItakuKeiyakuKihonDao.GetDateForStringSql(sql);
                    }
                    
                    // トランザクション終了
                    tran.Commit();
                }

                //INXS delete INXS data start  refs #158006
                if (isUploadToInxs)
                {
                    this.inxsContractLogic.DeleteInxsData(this.dto.ItakuKeiyakuKihon.SYSTEM_ID, this.form.transactionId, this.parentForm.Text);
                }
                //INXS end

                msgLogic.MessageBoxShow("I001", "削除");

                this.isRegist = true;
                //}

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        //[Transaction]
        public virtual void PhysicalDelete()
        {
            // 委託契約基本_排出現場マスタ削除
            M_ITAKU_KEIYAKU_KIHON_HST_GENBA itakuKihonHstGenba = new M_ITAKU_KEIYAKU_KIHON_HST_GENBA();
            itakuKihonHstGenba.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.kihonHstGenbaDao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_KIHON_HST_GENBA_SQL, itakuKihonHstGenba);

            // 委託契約別表1排出マスタ削除(報告書分類)
            M_ITAKU_KEIYAKU_BETSU1_HST itakuBetsu1Hst = new M_ITAKU_KEIYAKU_BETSU1_HST();
            itakuBetsu1Hst.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.betsu1HstDao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_HINMEI_SQL, itakuBetsu1Hst);

            // 委託契約品名マスタ削除
            M_ITAKU_KEIYAKU_HINMEI itakuHinmei = new M_ITAKU_KEIYAKU_HINMEI();
            itakuHinmei.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.hinmeiDao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_BETSU1_HST_SQL, itakuHinmei);

            // 委託契約報告書分類マスタ削除
            /*M_ITAKU_KEIYAKU_BETSU1_YOTEI itakuBetsu1Yotei = new M_ITAKU_KEIYAKU_BETSU1_YOTEI();
            itakuBetsu1Yotei.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.betsu1YoteiDao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_BETSU1_YOTEI_SQL, itakuBetsu1Yotei);*/

            // 委託契約運搬マスタ削除
            M_ITAKU_KEIYAKU_BETSU2 itakuBetsu2 = new M_ITAKU_KEIYAKU_BETSU2();
            itakuBetsu2.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.betsu2Dao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_BETSU2_SQL, itakuBetsu2);

            // 委託契約積替マスタ削除
            M_ITAKU_KEIYAKU_TSUMIKAE itakuTsumikae = new M_ITAKU_KEIYAKU_TSUMIKAE();
            itakuTsumikae.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.tsumikaeDao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_TSUMIKAE_SQL, itakuTsumikae);

            // 委託契約別表3マスタ削除
            M_ITAKU_KEIYAKU_BETSU3 itakuBetsu3 = new M_ITAKU_KEIYAKU_BETSU3();
            itakuBetsu3.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.betsu3Dao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_BETSU3_SQL, itakuBetsu3);

            // 委託契約別表4マスタ削除
            M_ITAKU_KEIYAKU_BETSU4 itakuBetsu4 = new M_ITAKU_KEIYAKU_BETSU4();
            itakuBetsu4.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.betsu4Dao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_BETSU4_SQL, itakuBetsu4);

            // 委託契約覚書マスタ削除
            M_ITAKU_KEIYAKU_OBOE itakuOboe = new M_ITAKU_KEIYAKU_OBOE();
            itakuOboe.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.oboeDao.GetDataBySqlFile(DELETE_ITAKU_KEIYAKU_OBOE_SQL, itakuOboe);

            // 委託契約運搬許可証紐付マスタ削除
            M_ITAKU_UPN_KYOKASHO upnKyoka = new M_ITAKU_UPN_KYOKASHO();
            upnKyoka.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.upnKyokashoDao.GetDataBySqlFile(DELETE_ITAKU_UPN_KYOKASHO_SQL, upnKyoka);

            // 委託契約処分許可証紐付マスタ削除
            M_ITAKU_SBN_KYOKASHO sbnKyoka = new M_ITAKU_SBN_KYOKASHO();
            sbnKyoka.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.sbnKyokashoDao.GetDataBySqlFile(DELETE_ITAKU_SBN_KYOKASHO_SQL, sbnKyoka);

            // 委託契約電子送付先マスタ削除
            M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI souhusaki = new M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI();
            souhusaki.SYSTEM_ID = this.dto.ItakuKeiyakuKihon.SYSTEM_ID;
            this.denshiKeiyakuSouhusakiDao.GetDataBySqlFile(DELETE_ITAKU_DENSHI_SOUHUSAKI_SQL, souhusaki);

        }

        #endregion

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            ItakuKeiyakuHoshuLogic localLogic = other as ItakuKeiyakuHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region Privateメソッド

        #region 画面操作

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        cont.Text = button.NewButtonName;
                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        cont.Text = button.ReferButtonName;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        cont.Text = button.UpdateButtonName;
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        cont.Text = button.DeleteButtonName;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart();

            //新規ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

            //修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Print);

            //一覧ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.RegistBefore);
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
            parentForm.bt_func9.Click += new EventHandler(this.RegistAfter);

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 画面表示処理
            this.form.Shown += new EventHandler(this.form.FormShown);

            // SuperFormイベント処理
            this.form.UserRegistCheck += new SuperForm.UserRegistCheckHandler(this.RegistUserCheck);

            // 日付入力のフォーカスイン処理
            this.form.KEIYAKUSHO_CREATE_DATE.Enter += new EventHandler(this.form.DateControlEnter);
            this.form.KEIYAKUSHO_SEND_DATE.Enter += new EventHandler(this.form.DateControlEnter);
            this.form.KOUSHIN_END_DATE.Enter += new EventHandler(this.form.DateControlEnter);
            this.form.YUUKOU_BEGIN.Enter += new EventHandler(this.form.DateControlEnter);
            this.form.KEIYAKUSHO_RETURN_DATE.Enter += new EventHandler(this.form.DateControlEnter);
            this.form.KEIYAKUSHO_END_DATE.Enter += new EventHandler(this.form.DateControlEnter);

            //排出事業者CD Validatedイベント生成
            this.form.HAISHUTSU_JIGYOUSHA_CD.Validated += new EventHandler(this.form.HaishutsuJigyoushaCDValidated);

            //排出事業場CD Validatedイベント生成
            this.form.HAISHUTSU_JIGYOUJOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.HaishutsuJigyoujouCDValidated);

            //排出事業場CD Enterイベント生成
            this.form.HAISHUTSU_JIGYOUJOU_CD.Enter += new EventHandler(this.form.HaishutsuJigyoujouCDEnter);

            //委託契約書ファイル参照ボタンクリックイベント生成
            this.form.btnFileRef.Click += new EventHandler(this.form.FileRefClick);

            //委託契約書ファイル参照ボタンクリックイベント生成
            this.form.btnBrowse.Click += new EventHandler(this.form.BrowseClick);

            //タブ選択イベント生成
            this.form.tabItakuKeiyakuData.SelectedIndexChanged += new EventHandler(this.form.TabSelectIndexChanged);

            //システムID Validatedイベント生成
            this.form.SYSTEM_ID.Validated += new EventHandler(this.form.SystemIdValidated);

            //作成日値変更イベント生成
            this.form.KEIYAKUSHO_CREATE_DATE.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.KEIYAKUSHO_CREATE_DATE.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            //送付日値変更イベント生成
            this.form.KEIYAKUSHO_SEND_DATE.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.KEIYAKUSHO_SEND_DATE.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            //自動更新終了日値変更イベント生成
            this.form.KOUSHIN_END_DATE.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.KOUSHIN_END_DATE.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            //更新種別テキスト変更イベント生成
            this.form.KOUSHIN_SHUBETSU.TextChanged += new EventHandler(this.form.KoushinShubetsuTextChanged);

            //有効期限開始値変更イベント生成
            this.form.YUUKOU_BEGIN.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.YUUKOU_BEGIN.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            //有効期限終了値変更イベント生成
            this.form.YUUKOU_END.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.YUUKOU_END.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            //返送日値変更イベント生成
            this.form.KEIYAKUSHO_RETURN_DATE.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.KEIYAKUSHO_RETURN_DATE.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            //完了日値変更イベント生成
            this.form.KEIYAKUSHO_END_DATE.Validating += new CancelEventHandler(this.form.DateControlValidating);
            this.form.KEIYAKUSHO_END_DATE.ValueChanged += new EventHandler(this.form.DateControlValueChanged);

            // 基本情報タブ セルエンターイベント
            this.form.listKihonHstGenba.CellEnter += new EventHandler<CellEventArgs>(this.form.ListKihonHstGenbaCellEnter);

            // 委託契約別表1予定セルエンターイベント生成
            this.form.listHoukokushoBunrui.CellEnter += new EventHandler<CellEventArgs>(this.form.ListBetsu1YoteiCellEnter);

            // 委託契約別表1予定行エンターイベント生成
            //this.form.listHoukokushoBunrui.RowEnter += new EventHandler<CellEventArgs>(this.form.ListBetsu1Yotei_RowEnter);

            // 委託契約別表1予定セルフォーマットイベント生成
            //this.form.listHoukokushoBunrui.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListBetsu1YoteiCellFormatting);

            // 委託契約別表1予定データエラーイベント生成
            //this.form.listHoukokushoBunrui.DataError += new EventHandler<DataErrorEventArgs>(this.form.listBetsu1YoteiDataError);

            // 委託契約 品名セルフォーマットイベント生成
            this.form.listHinmei.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListBetsu1HstCellFormatting);

            // 委託契約 品名セルエンターイベント
            this.form.listHinmei.CellEnter += new EventHandler<CellEventArgs>(this.form.ListBetsu1HstCellEnter);

            // 委託契約 品名データエラーイベント生成
            this.form.listHinmei.DataError += new EventHandler<DataErrorEventArgs>(this.form.ListBetsu1HstDataError);

            // 委託契約別表3セル編集開始イベント生成
            this.form.listBetsu3.CellBeginEdit += new EventHandler<CellBeginEditEventArgs>(this.form.ListBetsu3CellBeginEdit);

            // 委託契約別表3セルエンターイベント
            this.form.listBetsu3.CellEnter += new EventHandler<CellEventArgs>(this.form.ListBetsu3HstCellEnter);

            // 委託契約別表3セルフォーマットイベント生成
            //this.form.listBetsu3.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListBetsu3CellFormatting);

            // 委託契約別表3データエラーイベント生成
            //this.form.listBetsu3.DataError += new EventHandler<DataErrorEventArgs>(this.form.ListBetsu3DataError);

            // 委託契約別表3編集コントロール表示イベント生成
            //this.form.listBetsu3.EditingControlShowing += new EventHandler<EditingControlShowingEventArgs>(this.form.listBetsu3_EditingControlShowing);

            // 委託契約積替編集コントロール表示イベント生成
            this.form.listTsumikae.EditingControlShowing += new EventHandler<EditingControlShowingEventArgs>(this.form.ListTsumikaeEditingControlShowing);

            // 委託契約積替セルフォーマットイベント生成
            this.form.listTsumikae.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListTsumikaeCellFormatting);

            // 委託契約積替セルエンターイベント
            this.form.listTsumikae.CellEnter += new EventHandler<CellEventArgs>(this.form.ListTsumikaeCellEnter);

            // 委託契約積替データエラーイベント生成
            this.form.listTsumikae.DataError += new EventHandler<DataErrorEventArgs>(this.form.ListTsumikaeDataError);

            // 委託契約別表3中間処分場パターン呼出しボタン押下
            this.form.btnGetSbnPtn.Click += new EventHandler(this.form.BtnGetSbnPtn);

            // 委託契約別表3中間処分場パターン登録ボタン押下
            this.form.btnSetSbnPtn.Click += new EventHandler(this.form.BtnSetSbnPtn);

            // 委託契約別表4セル編集開始イベント生成
            this.form.listBetsu4.CellBeginEdit += new EventHandler<CellBeginEditEventArgs>(this.form.ListBetsu4CellBeginEdit);

            // 委託契約別表4セルエンターイベント
            this.form.listBetsu4.CellEnter += new EventHandler<CellEventArgs>(this.form.ListBetsu4HstCellEnter);

            // 委託契約別表4最終処分場パターン呼出しボタン押下
            this.form.btnGetLastSbnPtn.Click += new EventHandler(this.form.BtnGetLastSbnPtn);

            // 委託契約別表4最終処分場パターン登録ボタン押下
            this.form.btnSetLastSbnPtn.Click += new EventHandler(this.form.BtnSetLastSbnPtn);

            // 運搬許可区分テキスト更新イベント生成
            this.form.UPNKYOKA_KBN.TextChanged += new EventHandler(this.form.UpnKyokaKbnTextChanged);

            // 運搬紐付業者選択ボタン押下イベント生成
            this.form.btnUpnSearch.Click += new EventHandler(this.form.UpnKyokaSearch);

            // 運搬紐付業者ゴミ箱ボタン押下イベント生成
            this.form.btnUpnDust.Click += new EventHandler(this.form.UpnKyokaDust);

            // 運搬紐付業者行政許可区分表示変更
            this.form.listUpnKyokasho.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListKyokashoCellFormatting);

            // 処分業者CD Validatedイベント生成
            //this.form.SBNKYOKA_GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SbnKyokaGyoushaValidating);

            // 処分事業場CD Validatedイベント生成
            this.form.SBNKYOKA_GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SbnKyokaGenbaValidating);

            // 処分許可区分テキスト更新イベント生成
            this.form.SBNKYOKA_KBN.TextChanged += new EventHandler(this.form.SbnKyokaKbnTextChanged);

            // 処分紐付業者選択ボタン押下イベント生成
            this.form.btnSbnSearch.Click += new EventHandler(this.form.SbnKyokaSearch);

            // 処分紐付業者ゴミ箱ボタン押下イベント生成
            this.form.btnSbnDust.Click += new EventHandler(this.form.SbnKyokaDust);

            // 処分紐付業者行政許可区分表示変更
            this.form.listSbnKyokasho.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.ListKyokashoCellFormatting);

            // VUNGUYEN 20150525 #1294 START
            // 有効期限終了のダブルクリックイベント
            this.form.YUUKOU_END.MouseDoubleClick += new MouseEventHandler(YUUKOU_END_MouseDoubleClick);

            // 期限終了のダブルクリックイベント
            this.form.UPNKYOKA_END.MouseDoubleClick += new MouseEventHandler(UPNKYOKA_END_MouseDoubleClick);

            // 期限終了のダブルクリックイベント
            this.form.SBNKYOKA_END.MouseDoubleClick += new MouseEventHandler(SBNKYOKA_END_MouseDoubleClick);
            // VUNGUYEN 20150525 #1294 END

            // 社内経路CD Enterイベント生成
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Enter += new EventHandler(this.form.ShanaiKeiroCDTextEnter);

            // 社内経路CD Validatedイベント生成
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Validated += new EventHandler(this.form.ShanaiKeiroCDTextValidated);

            // 電子契約タブの一覧 Enterイベント生成
            // 一覧のEnterイベント
            this.form.keiroIchiran.CellEnter += new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);

            // 電子契約タブの社員CD Validatingイベント生成
            this.form.keiroIchiran.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.Ichiran_CellValidating);

            // 電子契約タブの契約先一覧 Enterイベント生成
            // 一覧のEnterイベント
            this.form.keiroIchiran2.CellEnter += new DataGridViewCellEventHandler(this.form.IchiranKeiyakusaki_CellEnter);

            // 電子契約タブの契約先（社外） Validatingイベント生成
            this.form.keiroIchiran2.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.IchiranKeiyakusaki_CellValidating);

            // ファイルアップロードボタン(process1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.RegistBefore);
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);
            parentForm.bt_process1.ProcessKbn = PROCESS_KBN.NEW;
            parentForm.bt_process1.Click += new EventHandler(this.RegistAfter);

            // 電子契約送信ボタン(process2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);
            // 電子契約照会ボタン(process3)イベント生成
            parentForm.bt_process3.Click += new EventHandler(this.bt_process3_Click);

            // ファイルアップロードボタンクリックイベント生成
            this.form.btnUpload.Click += new EventHandler(this.btn_upload_Click);
            // ファイルアップロード一覧クリックイベント生成
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellClick);

            // 社内経路（有り、無し）テキストチェンジイベント生成
            this.form.SHANAI_KEIRO.TextChanged += new EventHandler(this.SHANAI_KEIRO_TextChanged);

            // 契約先一覧の上下移動ボタンクリックイベント生成
            this.form.btnUpRow.Click += new EventHandler(this.btnUpRow_Click);
            this.form.btnDownRow.Click += new EventHandler(this.btnDownRow_Click);

			//Receive message from INXS Subapp refs #158006
            if (AppConfig.AppOptions.IsInxsItaku())
            {
                parentForm.OnReceiveMessageEvent += new BaseBaseForm.OnReceiveMessage(ParentForm_OnReceiveMessageEvent);
            }            

            LogUtility.DebugMethodEnd();
        }

        ///// <summary>
        ///// 動的イベント設定処理
        ///// </summary>
        //public void SetDynamicEvent()
        //{
        //    LogUtility.DebugMethodStart();

        //    // 委託契約基本情報セル編集終了イベント生成
        //    this.form.listKihonHstGenba.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListKihonHstGenbaCellValidating);

        //    // 委託契約別表1排出セル編集終了イベント生成
        //    this.form.listHimei.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1HstCellValidating);

        //    // 委託契約別表1予定セル編集終了イベント生成
        //    this.form.listHoukokushoBunrui.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1YoteiCellValidating);

        //    // 委託契約積替セル編集終了イベント生成
        //    this.form.listTSUMIKAE.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListTSUMIKAECellValidating);

        //    // 委託契約別表2セル編集終了イベント生成
        //    this.form.listBetsu2.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu2CellValidating);

        //    // 委託契約別表3セル編集終了イベント生成
        //    this.form.listBetsu3.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu3CellValidating);

        //    // 委託契約別表4セル編集終了イベント生成
        //    this.form.listBetsu4.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu4CellValidating);

        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// 動的イベント削除処理
        ///// </summary>
        //public void RemoveDynamicEvent()
        //{
        //    LogUtility.DebugMethodStart();

        //    // 委託契約基本情報セル編集終了イベント生成
        //    this.form.listKihonHstGenba.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListKihonHstGenbaCellValidating);

        //    // 委託契約別表1排出セル編集終了イベント生成
        //    this.form.listHimei.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1HstCellValidating);

        //    // 委託契約別表1予定セル編集終了イベント生成
        //    this.form.listHoukokushoBunrui.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1YoteiCellValidating);

        //    // 委託契約別表2セル編集終了イベント生成
        //    this.form.listBetsu2.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu2CellValidating);

        //    // 委託契約積替セル編集終了イベント生成
        //    this.form.listTSUMIKAE.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListTSUMIKAECellValidating);

        //    // 委託契約別表3セル編集終了イベント生成
        //    this.form.listBetsu3.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu3CellValidating);

        //    // 委託契約別表4セル編集終了イベント生成
        //    this.form.listBetsu4.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu4CellValidating);

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// Baseのフォーム情報に初期値を設定する
        /// </summary>
        private BusinessBaseForm BaseFormInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            LogUtility.DebugMethodEnd(parentForm);
            return parentForm;
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region 収集処分（産廃）帳票出力用のデータ作成

        /// <summary>
        /// 帳票出力用のデータ作成
        /// </summary>
        private DataTable CreateR001_03Data(ref DataTable dtBesshi_A, ref DataTable dtBesshi_B, ref DataTable dtBesshi_C, ref DataTable dtBesshi_D, ref DataTable dtBesshi_E, ref DataTable dtBesshi_F, ref DataTable dtBesshi_G, ref DataTable dtBesshi_H, ref DataTable dtBesshi_I, ref DataTable dtBesshi_J)
        {
            DataTable reportTable = new DataTable();
            string CONST_MOJI = "許可証記載の通り";

            #region カラム追加
            reportTable.Columns.Add("HAISHUTSU_JIGYOUSHA1", typeof(string));
            reportTable.Columns.Add("UNPAN_GYOUSHA1", typeof(string));
            reportTable.Columns.Add("HAISHUTSU_JIGYOUJOU1", typeof(string));
            // ◎収集運搬に関する事業範囲_産廃
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_TODOUHUKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_KIGEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_JIGYOU_HANI01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_TODOUHUKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_KIGEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_JIGYOU_HANI02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_01_KYOKA_BANGOU02", typeof(string));
            reportTable.Columns.Add("BESSHI_A", typeof(string));
            // ◎収集運搬に関する事業範囲_特管
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_TODOUHUKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_KIGEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_JIGYOU_HANI01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_TODOUHUKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_KIGEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_JIGYOU_HANI02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE01_02_KYOKA_BANGOU02", typeof(string));
            reportTable.Columns.Add("BESSHI_B", typeof(string));
            // ◎　処分に関する事業範囲
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_TODOUHUKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_KIGEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_JIGYOU_KUBUN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_HAIKIBUTSU_SYURUI01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_JOKEN01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_BANGOU01", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_TODOUHUKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_KIGEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_JIGYOU_KUBUN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_HAIKIBUTSU_SYURUI02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_JOKEN02", typeof(string));
            reportTable.Columns.Add("ARTICLE02_01_KYOKA_BANGOU02", typeof(string));
            reportTable.Columns.Add("BESSHI_C", typeof(string));
            reportTable.Columns.Add("BESSHI_D", typeof(string));
            // ◎　収集・運搬に関する種類、数量及び委託単価
            reportTable.Columns.Add("SHURUI01_NAME01", typeof(string));
            reportTable.Columns.Add("SHURUI01_NAME02", typeof(string));
            reportTable.Columns.Add("SHURUI01_NAME03", typeof(string));
            reportTable.Columns.Add("SUURYOU01_01", typeof(string));
            reportTable.Columns.Add("SUURYOU01_02", typeof(string));
            reportTable.Columns.Add("SUURYOU01_03", typeof(string));
            reportTable.Columns.Add("SUURYOU01_TANI01", typeof(string));
            reportTable.Columns.Add("SUURYOU01_TANI02", typeof(string));
            reportTable.Columns.Add("SUURYOU01_TANI03", typeof(string));
            reportTable.Columns.Add("TANKA01_01", typeof(string));
            reportTable.Columns.Add("TANKA01_02", typeof(string));
            reportTable.Columns.Add("TANKA01_03", typeof(string));
            reportTable.Columns.Add("BESSHI_E", typeof(string));
            // ◎　処分に関する種類、数量及び委託単価
            reportTable.Columns.Add("SHURUI02_NAME01", typeof(string));
            reportTable.Columns.Add("SHURUI02_NAME02", typeof(string));
            reportTable.Columns.Add("SHURUI02_NAME03", typeof(string));
            reportTable.Columns.Add("SUURYOU02_01", typeof(string));
            reportTable.Columns.Add("SUURYOU02_02", typeof(string));
            reportTable.Columns.Add("SUURYOU02_03", typeof(string));
            reportTable.Columns.Add("SUURYOU02_TANI01", typeof(string));
            reportTable.Columns.Add("SUURYOU02_TANI02", typeof(string));
            reportTable.Columns.Add("SUURYOU02_TANI03", typeof(string));
            reportTable.Columns.Add("TANKA02_01", typeof(string));
            reportTable.Columns.Add("TANKA02_02", typeof(string));
            reportTable.Columns.Add("TANKA02_03", typeof(string));
            reportTable.Columns.Add("BESSHI_F", typeof(string));
            // ３．（輸入廃棄物の有・無）
            reportTable.Columns.Add("ARTICLE02_03_YUNYUU_NASHI", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_03_YUNYUU_ARI", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_03_YUNYUU_HAIKIBUTSU", typeof(string));
            // ４．（処分の場所、方法及び処理\能力）
            reportTable.Columns.Add("ARTICLE02_04_SHIMEI", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_ADDRESS", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_SHOBUN_HOUHOU", typeof(string));
            reportTable.Columns.Add("ARTICLE02_04_SHISETSU_CAPCITY", typeof(string));
            reportTable.Columns.Add("BESSHI_G", typeof(string));
            // ５．（最終処分の場所、方法及び処理\能力）
            reportTable.Columns.Add("SBN_NO_01", typeof(string));
            reportTable.Columns.Add("SBN_NO_02", typeof(string));
            reportTable.Columns.Add("SBN_NO_03", typeof(string));
            reportTable.Columns.Add("SBN_NO_04", typeof(string));
            reportTable.Columns.Add("SBN_NO_05", typeof(string));
            reportTable.Columns.Add("SBN_NO_06", typeof(string));
            reportTable.Columns.Add("SBN_NO_07", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_01", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_02", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_03", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_04", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_05", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_06", typeof(string));
            reportTable.Columns.Add("JIGYOJOU_NAME_07", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_01", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_02", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_03", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_04", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_05", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_06", typeof(string));
            reportTable.Columns.Add("JIGYOUJOU_ADDRESS_07", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_01", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_02", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_03", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_04", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_05", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_06", typeof(string));
            reportTable.Columns.Add("SBN_HOUHOU_07", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_01", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_02", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_03", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_04", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_05", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_06", typeof(string));
            reportTable.Columns.Add("SHISETSU_NOURYOKU_07", typeof(string));
            reportTable.Columns.Add("BESSHI_H", typeof(string));
            // ６．（収集・運搬過程における積替保管）
            reportTable.Columns.Add("ARTICLE02_05_01", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_05_02", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_05_03", typeof(bool));
            reportTable.Columns.Add("ARTICLE02_05_03_HAIKI_SHURUI", typeof(string));
            reportTable.Columns.Add("ARTICLE02_05_03_ADDRESS", typeof(string));
            reportTable.Columns.Add("ARTICLE02_05_03_HOKAN_LIMIT", typeof(string));
            reportTable.Columns.Add("BESSHI_I", typeof(string));
            // 第３条（適正処理に必要な情報の提供）
            reportTable.Columns.Add("ARTICLE03_05_HAIKIBUTSU_SHURUI01", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_HAIKIBUTSU_SHURUI02", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_HAIKIBUTSU_SHURUI03", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_TEIJI_JIKI01", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_TEIJI_JIKI02", typeof(string));
            reportTable.Columns.Add("ARTICLE03_05_TEIJI_JIKI03", typeof(string));
            reportTable.Columns.Add("BESSHI_J", typeof(string));

            reportTable.Columns.Add("ARTICLE14_TAN_FROM", typeof(string));
            reportTable.Columns.Add("ARTICLE14_TAN_TO", typeof(string));
            reportTable.Columns.Add("ARTICLE14_AUTO_FROM", typeof(string));
            reportTable.Columns.Add("ARTICLE14_AUTO_TO", typeof(string));
            reportTable.Columns.Add("ARTICLE14_TAN", typeof(bool));
            reportTable.Columns.Add("ARTICLE14_AUTO", typeof(bool));
            reportTable.Columns.Add("ARTICLE14_AUTO_SPAN", typeof(string));

            reportTable.Columns.Add("CREATE_DATE", typeof(string));
            reportTable.Columns.Add("KOU_ADDRESS", typeof(string));
            reportTable.Columns.Add("KOU_NAME", typeof(string));
            reportTable.Columns.Add("KOU_DAIHYOU_NAME", typeof(string));
            reportTable.Columns.Add("OTSU_ADDRESS", typeof(string));
            reportTable.Columns.Add("OTSU_NAME", typeof(string));
            reportTable.Columns.Add("OTSU_DAIHYOU_NAME", typeof(string));
            #endregion

            #region データ取得
            // 地域
            M_CHIIKI[] chikis = chiikiDao.GetAllValidData(new M_CHIIKI());
            List<M_CHIIKI> CHIIKI_LIST = chikis.ToList();
            // 単位
            M_UNIT[] units = unitDao.GetAllValidData(new M_UNIT());
            List<M_UNIT> UNIT_LIST = units.ToList();
            // 現場
            M_GENBA[] genbas = genbaDao.GetAllValidData(new M_GENBA());
            List<M_GENBA> GENBA_LIST = genbas.ToList();
            // 処分方法
            M_SHOBUN_HOUHOU[] sbnHouhous = shobunHouhouDao.GetAllValidData(new M_SHOBUN_HOUHOU());
            List<M_SHOBUN_HOUHOU> SBN_HOUHOU_LIST = sbnHouhous.ToList();
            // 都道府県
            M_TODOUFUKEN[] todofukens = todoufukenDao.GetAllValidData(new M_TODOUFUKEN());
            List<M_TODOUFUKEN> TODOFUKEN_LIST = todofukens.ToList();

            var row = reportTable.NewRow();

            // 排出事業者
            string gyouhsaCd = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD;
            var HST_GYOUHSA = this.gyoushaDao.GetDataByCd(gyouhsaCd);
            if (HST_GYOUHSA == null)
            {
                HST_GYOUHSA = new M_GYOUSHA();
            }
            // 収集運搬及び処分業者
            gyouhsaCd = "";
            if (this.dto.itakuKeiyakuBetsu3.Length > 0)
            {
                gyouhsaCd = this.dto.itakuKeiyakuBetsu3[0].SHOBUN_GYOUSHA_CD;
            }
            var UPN_GYOUHSA = this.gyoushaDao.GetDataByCd(gyouhsaCd);
            if (UPN_GYOUHSA == null)
            {
                UPN_GYOUHSA = new M_GYOUSHA();
            }
            row["HAISHUTSU_JIGYOUSHA1"] = this.ConvertTwoText(HST_GYOUHSA.GYOUSHA_NAME1, HST_GYOUHSA.GYOUSHA_NAME2);
            row["UNPAN_GYOUSHA1"] = this.ConvertTwoText(UPN_GYOUHSA.GYOUSHA_NAME1, UPN_GYOUHSA.GYOUSHA_NAME2);
            // 排出事業場
            if (!string.IsNullOrWhiteSpace(this.dto.ItakuKeiyakuKihon.HST_FREE_COMMENT))
            {
                row["HAISHUTSU_JIGYOUJOU1"] = this.dto.ItakuKeiyakuKihon.HST_FREE_COMMENT;
            }
            else if (!string.IsNullOrWhiteSpace(this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD))
            {
                var HST_GENBA = new M_GENBA();
                HST_GENBA.GYOUSHA_CD = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUSHA_CD;
                HST_GENBA.GENBA_CD = this.dto.ItakuKeiyakuKihon.HAISHUTSU_JIGYOUJOU_CD;
                HST_GENBA = this.genbaDao.GetDataByCd(HST_GENBA);
                if (HST_GENBA == null)
                {
                    HST_GENBA = new M_GENBA();
                }

                row["HAISHUTSU_JIGYOUJOU1"] = this.ConvertTwoText(HST_GENBA.GENBA_NAME1, HST_GENBA.GENBA_NAME2);
            }
            else
            {
                row["HAISHUTSU_JIGYOUJOU1"] = string.Empty;
            }
            #endregion

            #region ◎収集運搬に関する事業範囲_産廃
            var CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
            string chikiName = "";
            string strCnt = "";
            bool equalFlg = false;
            // 普通産廃運搬許可証
            var UPN_KYOKA_LIST_HUTSUU = new List<M_ITAKU_UPN_KYOKASHO>();

            bool equals = false;
            for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
            {
                var UPN_KYOKASHO = this.dto.itakuUpnKyokasho[i];
                if (UPN_KYOKASHO.KYOKA_KBN == 1)
                {
                    equals = false;
                    foreach (var betsu2 in this.dto.itakuKeiyakuBetsu2)
                    {
                        if (UPN_KYOKASHO.GYOUSHA_CD == betsu2.UNPAN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                    CHIIKI_KYOKA.KYOKA_KBN = 1;
                    CHIIKI_KYOKA.GYOUSHA_CD = UPN_KYOKASHO.GYOUSHA_CD;
                    CHIIKI_KYOKA.GENBA_CD = UPN_KYOKASHO.GENBA_CD;
                    CHIIKI_KYOKA.CHIIKI_CD = UPN_KYOKASHO.CHIIKI_CD;
                    CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                    if (CHIIKI_KYOKA != null)
                    {
                        UPN_KYOKA_LIST_HUTSUU.Add(UPN_KYOKASHO);
                    }
                }
            }

            // 普通産廃運搬許可証が２明細以下の場合
            if (UPN_KYOKA_LIST_HUTSUU.Count <= 2)
            {
                var KYOKA = new M_ITAKU_UPN_KYOKASHO();
                for (int i = 0; i < 2; i++)
                {
                    strCnt = (i + 1).ToString("00");
                    if (UPN_KYOKA_LIST_HUTSUU.Count > i)
                    {
                        chikiName = "";
                        equalFlg = false;
                        KYOKA = UPN_KYOKA_LIST_HUTSUU[i];
                        for (int j = 0; j < this.dto.itakuKeiyakuBetsu2.Length; j++)
                        {
                            if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu2[j].UNPAN_GYOUSHA_CD)
                            {
                                equalFlg = true;
                            }
                        }

                        if (equalFlg)
                        {
                            var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                            if (list != null && list.Count > 0)
                            {
                                chikiName = list[0].CHIIKI_NAME;
                            }
                            row["ARTICLE01_01_KYOKA_BANGOU" + strCnt] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row["ARTICLE01_01_KYOKA_BANGOU" + strCnt] = string.Empty;
                        }
                        row["ARTICLE01_01_KYOKA_TODOUHUKEN" + strCnt] = chikiName;
                        row["ARTICLE01_01_KYOKA_KIGEN" + strCnt] = CONST_MOJI;
                        row["ARTICLE01_01_JIGYOU_HANI" + strCnt] = CONST_MOJI;
                        row["ARTICLE01_01_KYOKA_JOKEN" + strCnt] = CONST_MOJI;
                    }
                    else
                    {
                        row["ARTICLE01_01_KYOKA_TODOUHUKEN" + strCnt] = string.Empty;
                        row["ARTICLE01_01_KYOKA_BANGOU" + strCnt] = string.Empty;
                        row["ARTICLE01_01_KYOKA_KIGEN" + strCnt] = string.Empty;
                        row["ARTICLE01_01_JIGYOU_HANI" + strCnt] = string.Empty;
                        row["ARTICLE01_01_KYOKA_JOKEN" + strCnt] = string.Empty;
                    }
                }
                row["BESSHI_A"] = "";
            }
            // 普通産廃運搬許可証が３明細以上の場合
            else
            {
                dtBesshi_A = new DataTable();
                dtBesshi_A.Columns.Add("TODOUHUKEN_SEIREISHI01", typeof(string));
                dtBesshi_A.Columns.Add("KYOKA_YUUKOUKIGEN01", typeof(string));
                dtBesshi_A.Columns.Add("GIGYOU_HANI01", typeof(string));
                dtBesshi_A.Columns.Add("KYOKA_JOKEN01", typeof(string));
                dtBesshi_A.Columns.Add("KYOKA_BANGOU01", typeof(string));
                dtBesshi_A.Columns.Add("TODOUHUKEN_SEIREISHI02", typeof(string));
                dtBesshi_A.Columns.Add("KYOKA_YUUKOUKIGEN02", typeof(string));
                dtBesshi_A.Columns.Add("GIGYOU_HANI02", typeof(string));
                dtBesshi_A.Columns.Add("KYOKA_JOKEN02", typeof(string));
                dtBesshi_A.Columns.Add("KYOKA_BANGOU02", typeof(string));
                var KYOKA = new M_ITAKU_UPN_KYOKASHO();
                var row_A = dtBesshi_A.NewRow();
                for (int i = 0; i < UPN_KYOKA_LIST_HUTSUU.Count; i++)
                {
                    KYOKA = UPN_KYOKA_LIST_HUTSUU[i];
                    chikiName = "";
                    equalFlg = false;
                    for (int j = 0; j < this.dto.itakuKeiyakuBetsu2.Length; j++)
                    {
                        if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu2[j].UNPAN_GYOUSHA_CD)
                        {
                            equalFlg = true;
                        }
                    }

                    if (equalFlg)
                    {
                        var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                        if (list != null && list.Count > 0)
                        {
                            chikiName = list[0].CHIIKI_NAME;
                        }
                    }

                    if (i % 2 == 0)
                    {
                        row_A = dtBesshi_A.NewRow();
                        if (equalFlg)
                        {
                            row_A["KYOKA_BANGOU01"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_A["KYOKA_BANGOU01"] = string.Empty;
                        }
                        row_A["TODOUHUKEN_SEIREISHI01"] = chikiName;
                        row_A["KYOKA_YUUKOUKIGEN01"] = CONST_MOJI;
                        row_A["GIGYOU_HANI01"] = CONST_MOJI;
                        row_A["KYOKA_JOKEN01"] = CONST_MOJI;
                        if (i == UPN_KYOKA_LIST_HUTSUU.Count - 1)
                        {
                            row_A["KYOKA_BANGOU02"] = string.Empty;
                            row_A["TODOUHUKEN_SEIREISHI02"] = string.Empty;
                            row_A["KYOKA_YUUKOUKIGEN02"] = string.Empty;
                            row_A["GIGYOU_HANI02"] = string.Empty;
                            row_A["KYOKA_JOKEN02"] = string.Empty;
                            dtBesshi_A.Rows.Add(row_A);
                        }
                    }
                    else
                    {
                        if (equalFlg)
                        {
                            row_A["KYOKA_BANGOU02"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_A["KYOKA_BANGOU02"] = string.Empty;
                        }
                        row_A["TODOUHUKEN_SEIREISHI02"] = chikiName;
                        row_A["KYOKA_YUUKOUKIGEN02"] = CONST_MOJI;
                        row_A["GIGYOU_HANI02"] = CONST_MOJI;
                        row_A["KYOKA_JOKEN02"] = CONST_MOJI;
                        dtBesshi_A.Rows.Add(row_A);
                    }
                }
                row["BESSHI_A"] = "※別紙  乙の事業範囲（収集運搬）〔産廃〕に記載";
            }
            #endregion

            #region ◎収集運搬に関する事業範囲_特管
            // 特管産廃運搬許可証
            var UPN_KYOKA_LIST_TOKKAN = new List<M_ITAKU_UPN_KYOKASHO>();

            for (int i = 0; i < this.dto.itakuUpnKyokasho.Length; i++)
            {
                var UPN_KYOKASHO = this.dto.itakuUpnKyokasho[i];
                if (UPN_KYOKASHO.KYOKA_KBN == 2)
                {
                    equals = false;
                    foreach (var betsu2 in this.dto.itakuKeiyakuBetsu2)
                    {
                        if (UPN_KYOKASHO.GYOUSHA_CD == betsu2.UNPAN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                    CHIIKI_KYOKA.KYOKA_KBN = 1;
                    CHIIKI_KYOKA.GYOUSHA_CD = UPN_KYOKASHO.GYOUSHA_CD;
                    CHIIKI_KYOKA.GENBA_CD = UPN_KYOKASHO.GENBA_CD;
                    CHIIKI_KYOKA.CHIIKI_CD = UPN_KYOKASHO.CHIIKI_CD;
                    CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                    if (CHIIKI_KYOKA != null)
                    {
                        UPN_KYOKA_LIST_TOKKAN.Add(UPN_KYOKASHO);
                    }
                }
            }

            // 特管産廃運搬許可証が２明細以下の場合
            if (UPN_KYOKA_LIST_TOKKAN.Count <= 2)
            {
                var KYOKA = new M_ITAKU_UPN_KYOKASHO();
                for (int i = 0; i < 2; i++)
                {
                    strCnt = (i + 1).ToString("00");
                    if (UPN_KYOKA_LIST_TOKKAN.Count > i)
                    {
                        KYOKA = UPN_KYOKA_LIST_TOKKAN[i];
                        chikiName = "";
                        equalFlg = false;
                        for (int j = 0; j < this.dto.itakuKeiyakuBetsu2.Length; j++)
                        {
                            if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu2[j].UNPAN_GYOUSHA_CD)
                            {
                                equalFlg = true;
                            }
                        }
                        if (equalFlg)
                        {
                            var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                            if (list != null && list.Count > 0)
                            {
                                chikiName = list[0].CHIIKI_NAME;
                            }
                            row["ARTICLE01_02_KYOKA_BANGOU" + strCnt] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row["ARTICLE01_02_KYOKA_BANGOU" + strCnt] = string.Empty;
                        }
                        row["ARTICLE01_02_KYOKA_TODOUHUKEN" + strCnt] = chikiName;
                        row["ARTICLE01_02_KYOKA_KIGEN" + strCnt] = CONST_MOJI;
                        row["ARTICLE01_02_JIGYOU_HANI" + strCnt] = CONST_MOJI;
                        row["ARTICLE01_02_KYOKA_JOKEN" + strCnt] = CONST_MOJI;
                    }
                    else
                    {
                        row["ARTICLE01_02_KYOKA_TODOUHUKEN" + strCnt] = string.Empty;
                        row["ARTICLE01_02_KYOKA_BANGOU" + strCnt] = string.Empty;
                        row["ARTICLE01_02_KYOKA_KIGEN" + strCnt] = string.Empty;
                        row["ARTICLE01_02_JIGYOU_HANI" + strCnt] = string.Empty;
                        row["ARTICLE01_02_KYOKA_JOKEN" + strCnt] = string.Empty;
                    }
                }
                row["BESSHI_B"] = "";
            }
            // 特管産廃運搬許可証が３明細以上の場合
            else
            {
                dtBesshi_B = new DataTable();
                dtBesshi_B.Columns.Add("TODOUHUKEN_SEIREISHI01", typeof(string));
                dtBesshi_B.Columns.Add("KYOKA_YUUKOUKIGEN01", typeof(string));
                dtBesshi_B.Columns.Add("GIGYOU_HANI01", typeof(string));
                dtBesshi_B.Columns.Add("KYOKA_JOKEN01", typeof(string));
                dtBesshi_B.Columns.Add("KYOKA_BANGOU01", typeof(string));
                dtBesshi_B.Columns.Add("TODOUHUKEN_SEIREISHI02", typeof(string));
                dtBesshi_B.Columns.Add("KYOKA_YUUKOUKIGEN02", typeof(string));
                dtBesshi_B.Columns.Add("GIGYOU_HANI02", typeof(string));
                dtBesshi_B.Columns.Add("KYOKA_JOKEN02", typeof(string));
                dtBesshi_B.Columns.Add("KYOKA_BANGOU02", typeof(string));
                var KYOKA = new M_ITAKU_UPN_KYOKASHO();
                var row_B = dtBesshi_B.NewRow();
                for (int i = 0; i < UPN_KYOKA_LIST_TOKKAN.Count; i++)
                {
                    KYOKA = UPN_KYOKA_LIST_TOKKAN[i];
                    chikiName = "";
                    equalFlg = false;
                    for (int j = 0; j < this.dto.itakuKeiyakuBetsu2.Length; j++)
                    {
                        if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu2[j].UNPAN_GYOUSHA_CD)
                        {
                            equalFlg = true;
                        }
                    }

                    if (equalFlg)
                    {
                        var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                        if (list != null && list.Count > 0)
                        {
                            chikiName = list[0].CHIIKI_NAME;
                        }
                    }

                    if (i % 2 == 0)
                    {
                        row_B = dtBesshi_B.NewRow();
                        if (equalFlg)
                        {
                            row_B["KYOKA_BANGOU01"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_B["KYOKA_BANGOU01"] = string.Empty;
                        }
                        row_B["TODOUHUKEN_SEIREISHI01"] = chikiName;
                        row_B["KYOKA_YUUKOUKIGEN01"] = CONST_MOJI;
                        row_B["GIGYOU_HANI01"] = CONST_MOJI;
                        row_B["KYOKA_JOKEN01"] = CONST_MOJI;
                        if (i == UPN_KYOKA_LIST_TOKKAN.Count - 1)
                        {
                            row_B["KYOKA_BANGOU02"] = string.Empty;
                            row_B["TODOUHUKEN_SEIREISHI02"] = string.Empty;
                            row_B["KYOKA_YUUKOUKIGEN02"] = string.Empty;
                            row_B["GIGYOU_HANI02"] = string.Empty;
                            row_B["KYOKA_JOKEN02"] = string.Empty;
                            dtBesshi_B.Rows.Add(row_B);
                        }
                    }
                    else
                    {
                        if (equalFlg)
                        {
                            row_B["KYOKA_BANGOU02"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_B["KYOKA_BANGOU02"] = string.Empty;
                        }
                        row_B["TODOUHUKEN_SEIREISHI02"] = chikiName;
                        row_B["KYOKA_YUUKOUKIGEN02"] = CONST_MOJI;
                        row_B["GIGYOU_HANI02"] = CONST_MOJI;
                        row_B["KYOKA_JOKEN02"] = CONST_MOJI;
                        dtBesshi_B.Rows.Add(row_B);
                    }
                }
                row["BESSHI_B"] = "※別紙  乙の事業範囲（収集運搬）〔特管〕に記載";
            }
            #endregion

            #region 乙の事業範囲（処分）[産廃]
            // 普通産廃処分許可証
            var SBN_KYOKA_LIST_HUTSUU = new List<M_ITAKU_SBN_KYOKASHO>();

            for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
            {
                var SBN_KYOKASHO = this.dto.itakuSbnKyokasho[i];
                if (SBN_KYOKASHO.KYOKA_KBN == 3 || SBN_KYOKASHO.KYOKA_KBN == 5)
                {
                    equals = false;
                    foreach (var betsu3 in this.dto.itakuKeiyakuBetsu3)
                    {
                        if (SBN_KYOKASHO.GYOUSHA_CD == betsu3.SHOBUN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    if (SBN_KYOKASHO.KYOKA_KBN == 3)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 2;
                        CHIIKI_KYOKA.GYOUSHA_CD = SBN_KYOKASHO.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = SBN_KYOKASHO.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = SBN_KYOKASHO.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            SBN_KYOKA_LIST_HUTSUU.Add(SBN_KYOKASHO);
                        }
                    }
                    else if (SBN_KYOKASHO.KYOKA_KBN == 5)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 3;
                        CHIIKI_KYOKA.GYOUSHA_CD = SBN_KYOKASHO.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = SBN_KYOKASHO.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = SBN_KYOKASHO.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            SBN_KYOKA_LIST_HUTSUU.Add(SBN_KYOKASHO);
                        }
                    }
                }
            }

            // 普通産廃処分許可証が0明細の場合
            if (SBN_KYOKA_LIST_HUTSUU.Count == 0)
            {
                row["ARTICLE02_01_KYOKA_TODOUHUKEN01"] = "";
                row["ARTICLE02_01_KYOKA_BANGOU01"] = "";
                row["ARTICLE02_01_KYOKA_KIGEN01"] = "";
                row["ARTICLE02_01_JIGYOU_KUBUN01"] = "";
                row["ARTICLE02_01_HAIKIBUTSU_SYURUI01"] = "";
                row["ARTICLE02_01_KYOKA_JOKEN01"] = "";
                row["BESSHI_C"] = "";
            }
            else if (SBN_KYOKA_LIST_HUTSUU.Count == 1)
            {
                var KYOKA = SBN_KYOKA_LIST_HUTSUU[0];
                chikiName = "";
                equalFlg = false;
                for (int j = 0; j < this.dto.itakuKeiyakuBetsu3.Length; j++)
                {
                    if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu3[j].SHOBUN_GYOUSHA_CD)
                    {
                        equalFlg = true;
                    }
                }

                if (equalFlg)
                {
                    var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                    if (list != null && list.Count > 0)
                    {
                        chikiName = list[0].CHIIKI_NAME;
                    }
                    row["ARTICLE02_01_KYOKA_BANGOU01"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                }
                else
                {
                    row["ARTICLE02_01_KYOKA_BANGOU01"] = "";
                }

                row["ARTICLE02_01_KYOKA_TODOUHUKEN01"] = chikiName;
                row["ARTICLE02_01_KYOKA_KIGEN01"] = CONST_MOJI;
                row["ARTICLE02_01_JIGYOU_KUBUN01"] = CONST_MOJI;
                row["ARTICLE02_01_HAIKIBUTSU_SYURUI01"] = CONST_MOJI;
                row["ARTICLE02_01_KYOKA_JOKEN01"] = CONST_MOJI;
                row["BESSHI_C"] = "";
            }
            // 普通産廃処分許可証が２明細以上の場合
            else if (SBN_KYOKA_LIST_HUTSUU.Count >= 2)
            {
                dtBesshi_C = new DataTable();
                dtBesshi_C.Columns.Add("TODOUHUKEN_SEIREISHI01", typeof(string));
                dtBesshi_C.Columns.Add("KYOKA_YUUKOUKIGEN01", typeof(string));
                dtBesshi_C.Columns.Add("GIGYOU_KBN01", typeof(string));
                dtBesshi_C.Columns.Add("HAIKI_SHURUI01", typeof(string));
                dtBesshi_C.Columns.Add("KYOKA_JOKEN01", typeof(string));
                dtBesshi_C.Columns.Add("KYOKA_BANGOU01", typeof(string));
                dtBesshi_C.Columns.Add("TODOUHUKEN_SEIREISHI02", typeof(string));
                dtBesshi_C.Columns.Add("KYOKA_YUUKOUKIGEN02", typeof(string));
                dtBesshi_C.Columns.Add("GIGYOU_KBN02", typeof(string));
                dtBesshi_C.Columns.Add("HAIKI_SHURUI02", typeof(string));
                dtBesshi_C.Columns.Add("KYOKA_JOKEN02", typeof(string));
                dtBesshi_C.Columns.Add("KYOKA_BANGOU02", typeof(string));
                var KYOKA = new M_ITAKU_SBN_KYOKASHO();
                var row_C = dtBesshi_C.NewRow();
                for (int i = 0; i < SBN_KYOKA_LIST_HUTSUU.Count; i++)
                {
                    KYOKA = SBN_KYOKA_LIST_HUTSUU[i];
                    chikiName = "";
                    equalFlg = false;
                    for (int j = 0; j < this.dto.itakuKeiyakuBetsu3.Length; j++)
                    {
                        if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu3[j].SHOBUN_GYOUSHA_CD)
                        {
                            equalFlg = true;
                        }
                    }
                    if (equalFlg)
                    {
                        var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                        if (list != null && list.Count > 0)
                        {
                            chikiName = list[0].CHIIKI_NAME;
                        }
                    }

                    if (i % 2 == 0)
                    {
                        row_C = dtBesshi_C.NewRow();
                        if (equalFlg)
                        {
                            row_C["KYOKA_BANGOU01"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_C["KYOKA_BANGOU01"] = "";
                        }
                        row_C["TODOUHUKEN_SEIREISHI01"] = chikiName;
                        row_C["KYOKA_YUUKOUKIGEN01"] = CONST_MOJI;
                        row_C["GIGYOU_KBN01"] = CONST_MOJI;
                        row_C["HAIKI_SHURUI01"] = CONST_MOJI;
                        row_C["KYOKA_JOKEN01"] = CONST_MOJI;
                        if (i == SBN_KYOKA_LIST_HUTSUU.Count - 1)
                        {
                            row_C["KYOKA_BANGOU02"] = string.Empty;
                            row_C["TODOUHUKEN_SEIREISHI02"] = string.Empty;
                            row_C["KYOKA_YUUKOUKIGEN02"] = string.Empty;
                            row_C["GIGYOU_KBN02"] = string.Empty;
                            row_C["HAIKI_SHURUI02"] = string.Empty;
                            row_C["KYOKA_JOKEN02"] = string.Empty;
                            dtBesshi_C.Rows.Add(row_C);
                        }
                    }
                    else
                    {
                        if (equalFlg)
                        {
                            row_C["KYOKA_BANGOU02"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_C["KYOKA_BANGOU02"] = "";
                        }
                        row_C["TODOUHUKEN_SEIREISHI02"] = chikiName;
                        row_C["KYOKA_YUUKOUKIGEN02"] = CONST_MOJI;
                        row_C["GIGYOU_KBN02"] = CONST_MOJI;
                        row_C["HAIKI_SHURUI02"] = CONST_MOJI;
                        row_C["KYOKA_JOKEN02"] = CONST_MOJI;
                        dtBesshi_C.Rows.Add(row_C);
                    }
                }
                row["BESSHI_C"] = "※別紙  乙の事業範囲（処分）〔産廃〕に記載";
            }
            #endregion

            #region 乙の事業範囲（処分）〔特管〕
            // 特管産廃処分許可証
            var SBN_KYOKA_LIST_TOKKAN = new List<M_ITAKU_SBN_KYOKASHO>();

            for (int i = 0; i < this.dto.itakuSbnKyokasho.Length; i++)
            {
                var SBN_KYOKASHO = this.dto.itakuSbnKyokasho[i];
                if (SBN_KYOKASHO.KYOKA_KBN == 4 || SBN_KYOKASHO.KYOKA_KBN == 6)
                {
                    equals = false;
                    foreach (var betsu3 in this.dto.itakuKeiyakuBetsu3)
                    {
                        if (SBN_KYOKASHO.GYOUSHA_CD == betsu3.SHOBUN_GYOUSHA_CD)
                        {
                            equals = true;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        continue;
                    }
                    if (SBN_KYOKASHO.KYOKA_KBN == 4)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 2;
                        CHIIKI_KYOKA.GYOUSHA_CD = SBN_KYOKASHO.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = SBN_KYOKASHO.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = SBN_KYOKASHO.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            SBN_KYOKA_LIST_TOKKAN.Add(SBN_KYOKASHO);
                        }
                    }
                    else if (SBN_KYOKASHO.KYOKA_KBN == 6)
                    {
                        CHIIKI_KYOKA = new M_CHIIKIBETSU_KYOKA();
                        CHIIKI_KYOKA.KYOKA_KBN = 3;
                        CHIIKI_KYOKA.GYOUSHA_CD = SBN_KYOKASHO.GYOUSHA_CD;
                        CHIIKI_KYOKA.GENBA_CD = SBN_KYOKASHO.GENBA_CD;
                        CHIIKI_KYOKA.CHIIKI_CD = SBN_KYOKASHO.CHIIKI_CD;
                        CHIIKI_KYOKA = this.chiikibetsuKyokaDao.GetDataByPrimaryKey(CHIIKI_KYOKA);
                        if (CHIIKI_KYOKA != null)
                        {
                            SBN_KYOKA_LIST_TOKKAN.Add(SBN_KYOKASHO);
                        }
                    }
                }
            }

            // 特管産廃処分許可証が0明細の場合
            if (SBN_KYOKA_LIST_TOKKAN.Count == 0)
            {
                row["ARTICLE02_01_KYOKA_TODOUHUKEN02"] = "";
                row["ARTICLE02_01_KYOKA_BANGOU02"] = "";
                row["ARTICLE02_01_KYOKA_KIGEN02"] = "";
                row["ARTICLE02_01_JIGYOU_KUBUN02"] = "";
                row["ARTICLE02_01_HAIKIBUTSU_SYURUI02"] = "";
                row["ARTICLE02_01_KYOKA_JOKEN02"] = "";
                row["BESSHI_D"] = "";
            }
            // 特管産廃処分許可証が１明細の場合
            else if (SBN_KYOKA_LIST_TOKKAN.Count == 1)
            {
                var KYOKA = SBN_KYOKA_LIST_TOKKAN[0];
                chikiName = "";
                equalFlg = false;
                for (int j = 0; j < this.dto.itakuKeiyakuBetsu3.Length; j++)
                {
                    if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu3[j].SHOBUN_GYOUSHA_CD)
                    {
                        equalFlg = true;
                    }
                }
                if (equalFlg)
                {
                    var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                    if (list != null && list.Count > 0)
                    {
                        chikiName = list[0].CHIIKI_NAME;
                    }
                    row["ARTICLE02_01_KYOKA_BANGOU02"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                }
                else
                {
                    row["ARTICLE02_01_KYOKA_BANGOU02"] = string.Empty;
                }

                row["ARTICLE02_01_KYOKA_TODOUHUKEN02"] = chikiName;
                row["ARTICLE02_01_KYOKA_KIGEN02"] = CONST_MOJI;
                row["ARTICLE02_01_JIGYOU_KUBUN02"] = CONST_MOJI;
                row["ARTICLE02_01_HAIKIBUTSU_SYURUI02"] = CONST_MOJI;
                row["ARTICLE02_01_KYOKA_JOKEN02"] = CONST_MOJI;
                row["BESSHI_D"] = "";
            }
            // 特管産廃処分許可証が２明細以上の場合
            else
            {
                dtBesshi_D = new DataTable();
                dtBesshi_D.Columns.Add("TODOUHUKEN_SEIREISHI01", typeof(string));
                dtBesshi_D.Columns.Add("KYOKA_YUUKOUKIGEN01", typeof(string));
                dtBesshi_D.Columns.Add("GIGYOU_KBN01", typeof(string));
                dtBesshi_D.Columns.Add("HAIKI_SHURUI01", typeof(string));
                dtBesshi_D.Columns.Add("KYOKA_JOKEN01", typeof(string));
                dtBesshi_D.Columns.Add("KYOKA_BANGOU01", typeof(string));
                dtBesshi_D.Columns.Add("TODOUHUKEN_SEIREISHI02", typeof(string));
                dtBesshi_D.Columns.Add("KYOKA_YUUKOUKIGEN02", typeof(string));
                dtBesshi_D.Columns.Add("GIGYOU_KBN02", typeof(string));
                dtBesshi_D.Columns.Add("HAIKI_SHURUI02", typeof(string));
                dtBesshi_D.Columns.Add("KYOKA_JOKEN02", typeof(string));
                dtBesshi_D.Columns.Add("KYOKA_BANGOU02", typeof(string));
                var KYOKA = new M_ITAKU_SBN_KYOKASHO();
                var row_D = dtBesshi_D.NewRow();
                for (int i = 0; i < SBN_KYOKA_LIST_TOKKAN.Count; i++)
                {
                    KYOKA = SBN_KYOKA_LIST_TOKKAN[i];
                    chikiName = "";
                    equalFlg = false;
                    for (int j = 0; j < this.dto.itakuKeiyakuBetsu3.Length; j++)
                    {
                        if (KYOKA.GYOUSHA_CD == this.dto.itakuKeiyakuBetsu3[j].SHOBUN_GYOUSHA_CD)
                        {
                            equalFlg = true;
                        }
                    }
                    if (equalFlg)
                    {
                        var list = CHIIKI_LIST.Where(o => o.CHIIKI_CD == KYOKA.CHIIKI_CD).ToList();
                        if (list != null && list.Count > 0)
                        {
                            chikiName = list[0].CHIIKI_NAME;
                        }
                    }
                    if (i % 2 == 0)
                    {
                        row_D = dtBesshi_D.NewRow();
                        if (equalFlg)
                        {
                            row_D["KYOKA_BANGOU01"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_D["KYOKA_BANGOU01"] = string.Empty;
                        }
                        row_D["TODOUHUKEN_SEIREISHI01"] = chikiName;
                        row_D["KYOKA_YUUKOUKIGEN01"] = CONST_MOJI;
                        row_D["GIGYOU_KBN01"] = CONST_MOJI;
                        row_D["HAIKI_SHURUI01"] = CONST_MOJI;
                        row_D["KYOKA_JOKEN01"] = CONST_MOJI;
                        if (i == SBN_KYOKA_LIST_TOKKAN.Count - 1)
                        {
                            row_D["KYOKA_BANGOU02"] = string.Empty;
                            row_D["TODOUHUKEN_SEIREISHI02"] = string.Empty;
                            row_D["KYOKA_YUUKOUKIGEN02"] = string.Empty;
                            row_D["GIGYOU_KBN02"] = string.Empty;
                            row_D["HAIKI_SHURUI02"] = string.Empty;
                            row_D["KYOKA_JOKEN02"] = string.Empty;
                            dtBesshi_D.Rows.Add(row_D);
                        }
                    }
                    else
                    {
                        if (equalFlg)
                        {
                            row_D["KYOKA_BANGOU02"] = this.ConvertKyokaNo(KYOKA.KYOKA_NO);
                        }
                        else
                        {
                            row_D["KYOKA_BANGOU02"] = string.Empty;
                        }
                        row_D["TODOUHUKEN_SEIREISHI02"] = chikiName;
                        row_D["KYOKA_YUUKOUKIGEN02"] = CONST_MOJI;
                        row_D["GIGYOU_KBN02"] = CONST_MOJI;
                        row_D["HAIKI_SHURUI02"] = CONST_MOJI;
                        row_D["KYOKA_JOKEN02"] = CONST_MOJI;
                        dtBesshi_D.Rows.Add(row_D);
                    }
                }
                row["BESSHI_D"] = "※別紙  乙の事業範囲（処分）〔特管〕に記載";
            }
            #endregion

            #region 委託する産業廃棄物の種類、数量及び単価
            var ITAKU_HINMEI = new M_ITAKU_KEIYAKU_HINMEI();
            // 品名
            var ITAKU_HINMEI_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            var UPN_HINMEI_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            var SBN_HINMEI_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            var hinmei = new M_ITAKU_KEIYAKU_HINMEI();

            for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
            {
                hinmei = this.dto.itakuKeiyakuHinmei[i];
                ITAKU_HINMEI_LIST.Add(hinmei);
                if (!hinmei.UNPAN_YOTEI_SUU.IsNull && hinmei.UNPAN_YOTEI_SUU.Value >= 0)
                {
                    UPN_HINMEI_LIST.Add(hinmei);
                }
                if (!hinmei.SHOBUN_YOTEI_SUU.IsNull && hinmei.SHOBUN_YOTEI_SUU.Value >= 0)
                {
                    SBN_HINMEI_LIST.Add(hinmei);
                }
            }

            string unitName = "";
            // 運搬品名が３明細以下の場合
            if (UPN_HINMEI_LIST.Count <= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    strCnt = (i + 1).ToString("00");
                    if (UPN_HINMEI_LIST.Count > i)
                    {
                        hinmei = UPN_HINMEI_LIST[i];
                        unitName = "";
                        if (!hinmei.UNPAN_YOTEI_SUU_UNIT_CD.IsNull)
                        {
                            var list = UNIT_LIST.Where(o => o.UNIT_CD.Value == hinmei.UNPAN_YOTEI_SUU_UNIT_CD.Value).ToList();
                            if (list != null && list.Count > 0)
                            {
                                unitName = list[0].UNIT_NAME;
                            }
                        }

                        row["SHURUI01_NAME" + strCnt] = hinmei.HINMEI_NAME;
                        row["SUURYOU01_" + strCnt] = this.ConvertSuuryo(hinmei.UNPAN_YOTEI_SUU);
                        row["SUURYOU01_TANI" + strCnt] = unitName;
                        row["TANKA01_" + strCnt] = this.ConvertTanka(hinmei.UNPAN_ITAKU_TANKA);
                    }
                    else
                    {
                        row["SHURUI01_NAME" + strCnt] = string.Empty;
                        row["SUURYOU01_" + strCnt] = string.Empty;
                        row["SUURYOU01_TANI" + strCnt] = string.Empty;
                        row["TANKA01_" + strCnt] = string.Empty;
                    }
                }
                row["BESSHI_E"] = "";
            }
            // 品名が４明細以上の場合
            else
            {
                row["BESSHI_E"] = "※別紙  委託する産業廃棄物の種類、数量及び単価（収集運搬）に記載";
                dtBesshi_E = new DataTable();
                dtBesshi_E.Columns.Add("SHURUI_NAME01", typeof(string));
                dtBesshi_E.Columns.Add("SHURUI_NAME02", typeof(string));
                dtBesshi_E.Columns.Add("SHURUI_NAME03", typeof(string));
                dtBesshi_E.Columns.Add("SUURYOU01", typeof(string));
                dtBesshi_E.Columns.Add("SUURYOU02", typeof(string));
                dtBesshi_E.Columns.Add("SUURYOU03", typeof(string));
                dtBesshi_E.Columns.Add("SUURYOU_TANI01", typeof(string));
                dtBesshi_E.Columns.Add("SUURYOU_TANI02", typeof(string));
                dtBesshi_E.Columns.Add("SUURYOU_TANI03", typeof(string));
                dtBesshi_E.Columns.Add("TANKA01", typeof(string));
                dtBesshi_E.Columns.Add("TANKA02", typeof(string));
                dtBesshi_E.Columns.Add("TANKA03", typeof(string));

                var row_E = dtBesshi_E.NewRow();
                for (int i = 0; i < UPN_HINMEI_LIST.Count; i++)
                {
                    hinmei = UPN_HINMEI_LIST[i];
                    unitName = "";
                    if (!hinmei.UNPAN_YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        var list = UNIT_LIST.Where(o => o.UNIT_CD.Value == hinmei.UNPAN_YOTEI_SUU_UNIT_CD.Value).ToList();
                        if (list != null && list.Count > 0)
                        {
                            unitName = list[0].UNIT_NAME;
                        }
                    }
                    if (i % 3 == 0)
                    {
                        row_E = dtBesshi_E.NewRow();
                        row_E["SHURUI_NAME01"] = hinmei.HINMEI_NAME;
                        row_E["SUURYOU01"] = this.ConvertSuuryo(hinmei.UNPAN_YOTEI_SUU);
                        row_E["SUURYOU_TANI01"] = unitName;
                        row_E["TANKA01"] = this.ConvertTanka(hinmei.UNPAN_ITAKU_TANKA);
                        if (i == UPN_HINMEI_LIST.Count - 1)
                        {
                            row_E["SHURUI_NAME02"] = string.Empty;
                            row_E["SUURYOU02"] = string.Empty;
                            row_E["SUURYOU_TANI02"] = string.Empty;
                            row_E["TANKA02"] = string.Empty;
                            row_E["SHURUI_NAME03"] = string.Empty;
                            row_E["SUURYOU03"] = string.Empty;
                            row_E["SUURYOU_TANI03"] = string.Empty;
                            row_E["TANKA03"] = string.Empty;
                            dtBesshi_E.Rows.Add(row_E);
                        }
                    }
                    else if (i % 3 == 1)
                    {
                        row_E["SHURUI_NAME02"] = hinmei.HINMEI_NAME;
                        row_E["SUURYOU02"] = this.ConvertSuuryo(hinmei.UNPAN_YOTEI_SUU);
                        row_E["SUURYOU_TANI02"] = unitName;
                        row_E["TANKA02"] = this.ConvertTanka(hinmei.UNPAN_ITAKU_TANKA);
                        if (i == UPN_HINMEI_LIST.Count - 1)
                        {
                            row_E["SHURUI_NAME03"] = string.Empty;
                            row_E["SUURYOU03"] = string.Empty;
                            row_E["SUURYOU_TANI03"] = string.Empty;
                            row_E["TANKA03"] = string.Empty;
                            dtBesshi_E.Rows.Add(row_E);
                        }
                    }
                    else
                    {
                        row_E["SHURUI_NAME03"] = hinmei.HINMEI_NAME;
                        row_E["SUURYOU03"] = this.ConvertSuuryo(hinmei.UNPAN_YOTEI_SUU);
                        row_E["SUURYOU_TANI03"] = unitName;
                        row_E["TANKA03"] = this.ConvertTanka(hinmei.UNPAN_ITAKU_TANKA);
                        dtBesshi_E.Rows.Add(row_E);
                    }
                }
            }

            // 処分品名が３明細以下の場合
            if (SBN_HINMEI_LIST.Count <= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    strCnt = (i + 1).ToString("00");
                    if (SBN_HINMEI_LIST.Count > i)
                    {
                        hinmei = SBN_HINMEI_LIST[i];
                        unitName = "";
                        if (!hinmei.SHOBUN_YOTEI_SUU_UNIT_CD.IsNull)
                        {
                            var list = UNIT_LIST.Where(o => o.UNIT_CD.Value == hinmei.SHOBUN_YOTEI_SUU_UNIT_CD.Value).ToList();
                            if (list != null && list.Count > 0)
                            {
                                unitName = list[0].UNIT_NAME;
                            }
                        }

                        row["SHURUI02_NAME" + strCnt] = hinmei.HINMEI_NAME;
                        row["SUURYOU02_" + strCnt] = this.ConvertSuuryo(hinmei.SHOBUN_YOTEI_SUU);
                        row["SUURYOU02_TANI" + strCnt] = unitName;
                        row["TANKA02_" + strCnt] = this.ConvertTanka(hinmei.SHOBUN_ITAKU_TANKA);
                    }
                    else
                    {
                        row["SHURUI02_NAME" + strCnt] = string.Empty;
                        row["SUURYOU02_" + strCnt] = string.Empty;
                        row["SUURYOU02_TANI" + strCnt] = string.Empty;
                        row["TANKA02_" + strCnt] = string.Empty;
                    }
                }
                row["BESSHI_F"] = "";
            }
            // 処分品名が４明細以上の場合
            else
            {
                row["BESSHI_F"] = "※別紙  委託する産業廃棄物の種類、数量及び単価（処分）に記載";
                dtBesshi_F = new DataTable();
                dtBesshi_F.Columns.Add("SHURUI_NAME01", typeof(string));
                dtBesshi_F.Columns.Add("SHURUI_NAME02", typeof(string));
                dtBesshi_F.Columns.Add("SHURUI_NAME03", typeof(string));
                dtBesshi_F.Columns.Add("SUURYOU01", typeof(string));
                dtBesshi_F.Columns.Add("SUURYOU02", typeof(string));
                dtBesshi_F.Columns.Add("SUURYOU03", typeof(string));
                dtBesshi_F.Columns.Add("SUURYOU_TANI01", typeof(string));
                dtBesshi_F.Columns.Add("SUURYOU_TANI02", typeof(string));
                dtBesshi_F.Columns.Add("SUURYOU_TANI03", typeof(string));
                dtBesshi_F.Columns.Add("TANKA01", typeof(string));
                dtBesshi_F.Columns.Add("TANKA02", typeof(string));
                dtBesshi_F.Columns.Add("TANKA03", typeof(string));

                var row_F = dtBesshi_F.NewRow();
                for (int i = 0; i < SBN_HINMEI_LIST.Count; i++)
                {
                    hinmei = SBN_HINMEI_LIST[i];
                    unitName = "";
                    if (!hinmei.SHOBUN_YOTEI_SUU_UNIT_CD.IsNull)
                    {
                        var list = UNIT_LIST.Where(o => o.UNIT_CD.Value == hinmei.SHOBUN_YOTEI_SUU_UNIT_CD.Value).ToList();
                        if (list != null && list.Count > 0)
                        {
                            unitName = list[0].UNIT_NAME;
                        }
                    }
                    if (i % 3 == 0)
                    {
                        row_F = dtBesshi_F.NewRow();
                        row_F["SHURUI_NAME01"] = hinmei.HINMEI_NAME;
                        row_F["SUURYOU01"] = this.ConvertSuuryo(hinmei.SHOBUN_YOTEI_SUU);
                        row_F["SUURYOU_TANI01"] = unitName;
                        row_F["TANKA01"] = this.ConvertTanka(hinmei.SHOBUN_ITAKU_TANKA);
                        if (i == SBN_HINMEI_LIST.Count - 1)
                        {
                            row_F["SHURUI_NAME02"] = string.Empty;
                            row_F["SUURYOU02"] = string.Empty;
                            row_F["SUURYOU_TANI02"] = string.Empty;
                            row_F["TANKA02"] = string.Empty;
                            row_F["SHURUI_NAME03"] = string.Empty;
                            row_F["SUURYOU03"] = string.Empty;
                            row_F["SUURYOU_TANI03"] = string.Empty;
                            row_F["TANKA03"] = string.Empty;
                            dtBesshi_F.Rows.Add(row_F);
                        }
                    }
                    else if (i % 3 == 1)
                    {
                        row_F["SHURUI_NAME02"] = hinmei.HINMEI_NAME;
                        row_F["SUURYOU02"] = this.ConvertSuuryo(hinmei.SHOBUN_YOTEI_SUU);
                        row_F["SUURYOU_TANI02"] = unitName;
                        row_F["TANKA02"] = this.ConvertTanka(hinmei.SHOBUN_ITAKU_TANKA);
                        if (i == SBN_HINMEI_LIST.Count - 1)
                        {
                            row_F["SHURUI_NAME03"] = string.Empty;
                            row_F["SUURYOU03"] = string.Empty;
                            row_F["SUURYOU_TANI03"] = string.Empty;
                            row_F["TANKA03"] = string.Empty;
                            dtBesshi_F.Rows.Add(row_F);
                        }
                    }
                    else
                    {
                        row_F["SHURUI_NAME03"] = hinmei.HINMEI_NAME;
                        row_F["SUURYOU03"] = this.ConvertSuuryo(hinmei.SHOBUN_YOTEI_SUU);
                        row_F["SUURYOU_TANI03"] = unitName;
                        row_F["TANKA03"] = this.ConvertTanka(hinmei.SHOBUN_ITAKU_TANKA);
                        dtBesshi_F.Rows.Add(row_F);
                    }
                }
            }
            #endregion

            #region 輸入廃棄物の有無
            // 輸入廃棄物の有無
            string haikiBetsu = "";
            for (int i = 0; i < ITAKU_HINMEI_LIST.Count; i++)
            {
                if (ITAKU_HINMEI_LIST[i].YUNYU)
                {
                    haikiBetsu += ITAKU_HINMEI_LIST[i].HINMEI_NAME + "、";
                }
            }
            if (haikiBetsu.Length > 0)
            {
                haikiBetsu = haikiBetsu.Substring(0, haikiBetsu.Length - 1);
                row["ARTICLE02_03_YUNYUU_NASHI"] = false;
                row["ARTICLE02_03_YUNYUU_ARI"] = true;
            }
            else
            {
                row["ARTICLE02_03_YUNYUU_NASHI"] = true;
                row["ARTICLE02_03_YUNYUU_ARI"] = false;
            }
            row["ARTICLE02_03_YUNYUU_HAIKIBUTSU"] = haikiBetsu;
            #endregion

            #region 処分の場所、方法及び処理能力
            // 処分の場所、方法及び処理能力
            var SBN_LIST = new List<M_ITAKU_KEIYAKU_BETSU3>();

            for (int i = 0; i < this.dto.itakuKeiyakuBetsu3.Length; i++)
            {
                SBN_LIST.Add(this.dto.itakuKeiyakuBetsu3[i]);
            }

            string jigyojouName = "";
            string sbnHouhouName = "";
            string todoufukenName = "";
            var list_todoufuken = new List<M_TODOUFUKEN>();
            // 処分が0の場合
            if (SBN_LIST.Count == 0)
            {
                row["ARTICLE02_04_SHIMEI"] = "";
                row["ARTICLE02_04_ADDRESS"] = "";
                row["ARTICLE02_04_SHOBUN_HOUHOU"] = "";
                row["ARTICLE02_04_SHISETSU_CAPCITY"] = "";
                row["BESSHI_G"] = "";
            }
            // 処分が1の場合
            else if (SBN_LIST.Count == 1)
            {
                var SBN = SBN_LIST[0];
                jigyojouName = "";
                todoufukenName = "";
                sbnHouhouName = "";
                string jigyojouAddress = "";

                var list_genba = GENBA_LIST.Where(o => o.GYOUSHA_CD == SBN.SHOBUN_GYOUSHA_CD && o.GENBA_CD == SBN.SHOBUN_JIGYOUJOU_CD).ToList();
                if (list_genba != null && list_genba.Count > 0)
                {
                    jigyojouName = this.ConvertTwoText(list_genba[0].GENBA_NAME1, list_genba[0].GENBA_NAME2);
                    jigyojouAddress = this.ConvertTwoText(list_genba[0].GENBA_ADDRESS1, list_genba[0].GENBA_ADDRESS2);
                    if (!list_genba[0].GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        list_todoufuken = TODOFUKEN_LIST.Where(o => o.TODOUFUKEN_CD.Value == list_genba[0].GENBA_TODOUFUKEN_CD.Value).ToList();
                        if (list_todoufuken != null && list_todoufuken.Count > 0)
                        {
                            todoufukenName = list_todoufuken[0].TODOUFUKEN_NAME;
                        }
                    }
                }

                var list_sbnHouhou = SBN_HOUHOU_LIST.Where(o => o.SHOBUN_HOUHOU_CD == SBN.SHOBUN_HOUHOU_CD).ToList();
                if (list_sbnHouhou != null && list_sbnHouhou.Count > 0)
                {
                    sbnHouhouName = list_sbnHouhou[0].SHOBUN_HOUHOU_NAME;
                }

                row["ARTICLE02_04_SHIMEI"] = jigyojouName;
                row["ARTICLE02_04_ADDRESS"] = todoufukenName + jigyojouAddress;
                row["ARTICLE02_04_SHOBUN_HOUHOU"] = sbnHouhouName;
                row["ARTICLE02_04_SHISETSU_CAPCITY"] = SBN.SHISETSU_CAPACITY;
                row["BESSHI_G"] = "";
            }
            // 処分が２明細以上の場合
            else
            {
                dtBesshi_G = new DataTable();
                dtBesshi_G.Columns.Add("JIGYOJOU_NAME", typeof(string));
                dtBesshi_G.Columns.Add("JIGYOJOU_ADDRESS", typeof(string));
                dtBesshi_G.Columns.Add("SBN_HOUHOU", typeof(string));
                dtBesshi_G.Columns.Add("CAPACITY", typeof(string));
                var SBN = new M_ITAKU_KEIYAKU_BETSU3();
                var row_G = dtBesshi_G.NewRow();
                for (int i = 0; i < SBN_LIST.Count; i++)
                {
                    SBN = SBN_LIST[i];
                    jigyojouName = "";
                    todoufukenName = "";
                    sbnHouhouName = "";
                    var list_genba = GENBA_LIST.Where(o => o.GYOUSHA_CD == SBN.SHOBUN_GYOUSHA_CD && o.GENBA_CD == SBN.SHOBUN_JIGYOUJOU_CD).ToList();
                    if (list_genba != null && list_genba.Count > 0)
                    {
                        jigyojouName = this.ConvertTwoText(list_genba[0].GENBA_NAME1, list_genba[0].GENBA_NAME2);
                        todoufukenName = "";
                        if (!list_genba[0].GENBA_TODOUFUKEN_CD.IsNull)
                        {
                            list_todoufuken = TODOFUKEN_LIST.Where(o => o.TODOUFUKEN_CD.Value == list_genba[0].GENBA_TODOUFUKEN_CD.Value).ToList();
                            if (list_todoufuken != null && list_todoufuken.Count > 0)
                            {
                                todoufukenName = list_todoufuken[0].TODOUFUKEN_NAME;
                            }
                        }
                    }

                    var list_sbnHouhou = SBN_HOUHOU_LIST.Where(o => o.SHOBUN_HOUHOU_CD == SBN.SHOBUN_HOUHOU_CD).ToList();
                    if (list_sbnHouhou != null && list_sbnHouhou.Count > 0)
                    {
                        sbnHouhouName = list_sbnHouhou[0].SHOBUN_HOUHOU_NAME;
                    }
                    row_G = dtBesshi_G.NewRow();
                    row_G["JIGYOJOU_NAME"] = jigyojouName;
                    row_G["JIGYOJOU_ADDRESS"] = todoufukenName + this.ConvertTwoText(SBN.SHOBUN_JIGYOUJOU_ADDRESS1, SBN.SHOBUN_JIGYOUJOU_ADDRESS2);
                    row_G["SBN_HOUHOU"] = sbnHouhouName;
                    row_G["CAPACITY"] = SBN.SHISETSU_CAPACITY;
                    dtBesshi_G.Rows.Add(row_G);
                }
                row["BESSHI_G"] = "※別紙  処分の場所、方法及び処理能力に記載";
            }
            #endregion

            #region 最終処分の場所、方法及び処理能力
            // 最終処分の場所、方法及び処理能力
            var SAISYU_SBN_LIST = new List<M_ITAKU_KEIYAKU_BETSU4>();

            for (int i = 0; i < this.dto.itakuKeiyakuBetsu4.Length; i++)
            {
                SAISYU_SBN_LIST.Add(this.dto.itakuKeiyakuBetsu4[i]);
            }
            string snbsakiNo = "";
            var LAST_SBN = new M_ITAKU_KEIYAKU_BETSU4();
            // 最終処分が７明細以下の場合
            if (SAISYU_SBN_LIST.Count <= 7)
            {
                for (int i = 0; i < 7; i++)
                {
                    strCnt = (i + 1).ToString("00");
                    if (SAISYU_SBN_LIST.Count > i)
                    {
                        LAST_SBN = SAISYU_SBN_LIST[i];
                        snbsakiNo = "";
                        jigyojouName = "";
                        todoufukenName = "";
                        sbnHouhouName = "";
                        var list_genba = GENBA_LIST.Where(o => o.GYOUSHA_CD == LAST_SBN.LAST_SHOBUN_GYOUSHA_CD && o.GENBA_CD == LAST_SBN.LAST_SHOBUN_JIGYOUJOU_CD).ToList();
                        if (list_genba != null && list_genba.Count > 0)
                        {
                            snbsakiNo = list_genba[0].SHOBUNSAKI_NO;
                            jigyojouName = this.ConvertTwoText(list_genba[0].GENBA_NAME1, list_genba[0].GENBA_NAME2);
                            if (!list_genba[0].GENBA_TODOUFUKEN_CD.IsNull)
                            {
                                list_todoufuken = TODOFUKEN_LIST.Where(o => o.TODOUFUKEN_CD.Value == list_genba[0].GENBA_TODOUFUKEN_CD.Value).ToList();
                                if (list_todoufuken != null && list_todoufuken.Count > 0)
                                {
                                    todoufukenName = list_todoufuken[0].TODOUFUKEN_NAME;
                                }
                            }
                        }

                        var list_sbnHouhou = SBN_HOUHOU_LIST.Where(o => o.SHOBUN_HOUHOU_CD == LAST_SBN.SHOBUN_HOUHOU_CD).ToList();
                        if (list_sbnHouhou != null && list_sbnHouhou.Count > 0)
                        {
                            sbnHouhouName = list_sbnHouhou[0].SHOBUN_HOUHOU_NAME;
                        }
                        row["SBN_NO_" + strCnt] = (string.IsNullOrWhiteSpace(snbsakiNo)) ? "-　　　　　" : snbsakiNo;
                        row["JIGYOJOU_NAME_" + strCnt] = jigyojouName;
                        row["JIGYOUJOU_ADDRESS_" + strCnt] = todoufukenName + this.ConvertTwoText(LAST_SBN.LAST_SHOBUN_JIGYOUJOU_ADDRESS1, LAST_SBN.LAST_SHOBUN_JIGYOUJOU_ADDRESS2);
                        row["SBN_HOUHOU_" + strCnt] = sbnHouhouName;
                        row["SHISETSU_NOURYOKU_" + strCnt] = LAST_SBN.SHORI_SPEC;
                    }
                    else
                    {
                        row["SBN_NO_" + strCnt] = "-　　　　　";
                        row["JIGYOJOU_NAME_" + strCnt] = "";
                        row["JIGYOUJOU_ADDRESS_" + strCnt] = "";
                        row["SBN_HOUHOU_" + strCnt] = "";
                        row["SHISETSU_NOURYOKU_" + strCnt] = "";
                    }
                }
                row["BESSHI_H"] = "";
            }
            // 最終処分が８明細以上の場合
            else if (SAISYU_SBN_LIST.Count >= 8)
            {
                dtBesshi_H = new DataTable();
                dtBesshi_H.Columns.Add("SBN_NO", typeof(string));
                dtBesshi_H.Columns.Add("JIGYOJOU_NAME", typeof(string));
                dtBesshi_H.Columns.Add("JIGYOJOU_ADDRESS", typeof(string));
                dtBesshi_H.Columns.Add("SBN_HOUHOU", typeof(string));
                dtBesshi_H.Columns.Add("CAPACITY", typeof(string));
                var row_H = dtBesshi_H.NewRow();
                for (int i = 0; i < SAISYU_SBN_LIST.Count; i++)
                {
                    LAST_SBN = SAISYU_SBN_LIST[i];
                    snbsakiNo = "";
                    jigyojouName = "";
                    todoufukenName = "";
                    sbnHouhouName = "";
                    var list_genba = GENBA_LIST.Where(o => o.GYOUSHA_CD == LAST_SBN.LAST_SHOBUN_GYOUSHA_CD && o.GENBA_CD == LAST_SBN.LAST_SHOBUN_JIGYOUJOU_CD).ToList();
                    if (list_genba != null && list_genba.Count > 0)
                    {
                        snbsakiNo = list_genba[0].SHOBUNSAKI_NO;
                        jigyojouName = this.ConvertTwoText(list_genba[0].GENBA_NAME1, list_genba[0].GENBA_NAME2);
                        if (!list_genba[0].GENBA_TODOUFUKEN_CD.IsNull)
                        {
                            list_todoufuken = TODOFUKEN_LIST.Where(o => o.TODOUFUKEN_CD.Value == list_genba[0].GENBA_TODOUFUKEN_CD.Value).ToList();
                            if (list_todoufuken != null && list_todoufuken.Count > 0)
                            {
                                todoufukenName = list_todoufuken[0].TODOUFUKEN_NAME;
                            }
                        }
                    }

                    var list_sbnHouhou = SBN_HOUHOU_LIST.Where(o => o.SHOBUN_HOUHOU_CD == LAST_SBN.SHOBUN_HOUHOU_CD).ToList();
                    if (list_sbnHouhou != null && list_sbnHouhou.Count > 0)
                    {
                        sbnHouhouName = list_sbnHouhou[0].SHOBUN_HOUHOU_NAME;
                    }
                    row_H = dtBesshi_H.NewRow();
                    row_H["SBN_NO"] = (string.IsNullOrWhiteSpace(snbsakiNo)) ? "- 　　" : snbsakiNo;
                    row_H["JIGYOJOU_NAME"] = jigyojouName;
                    row_H["JIGYOJOU_ADDRESS"] = todoufukenName + this.ConvertTwoText(LAST_SBN.LAST_SHOBUN_JIGYOUJOU_ADDRESS1, LAST_SBN.LAST_SHOBUN_JIGYOUJOU_ADDRESS2);
                    row_H["SBN_HOUHOU"] = sbnHouhouName;
                    row_H["CAPACITY"] = LAST_SBN.SHORI_SPEC;
                    dtBesshi_H.Rows.Add(row_H);
                }
                row["BESSHI_H"] = "※別紙  最終処分の場所、方法及び処理能力に記載";
            }
            #endregion

            #region 積替
            var tsumihoCheck = 1;
            foreach (var tsumikae in this.dto.itakuKeiyakuTsumikae)
            {
                if (!string.IsNullOrEmpty(tsumikae.UNPAN_GYOUSHA_CD))
                {
                    if (tsumikae.KONGOU == 1)
                    {
                        tsumihoCheck = 2;
                        break;
                    }
                    tsumihoCheck = 3;
                }
            }

            row["ARTICLE02_05_01"] = tsumihoCheck == 1;
            row["ARTICLE02_05_02"] = tsumihoCheck == 2;
            row["ARTICLE02_05_03"] = tsumihoCheck == 3;

            // 積替
            var TSUMIKAE_LIST = new List<M_ITAKU_KEIYAKU_TSUMIKAE>();

            for (int i = 0; i < this.dto.itakuKeiyakuTsumikae.Length; i++)
            {
                TSUMIKAE_LIST.Add(this.dto.itakuKeiyakuTsumikae[i]);
            }

            var TSUMIKAE = new M_ITAKU_KEIYAKU_TSUMIKAE();
            if (TSUMIKAE_LIST.Count == 0)
            {
                row["ARTICLE02_05_03_HAIKI_SHURUI"] = "";
                row["ARTICLE02_05_03_ADDRESS"] = "";
                row["ARTICLE02_05_03_HOKAN_LIMIT"] = "";
                row["BESSHI_I"] = "";
            }
            else if (TSUMIKAE_LIST.Count == 1)
            {
                TSUMIKAE = TSUMIKAE_LIST[0];
                unitName = "";
                var HINMEI = new M_ITAKU_KEIYAKU_HINMEI();
                string TSUMIKAE_HAIKI_SHURUI = "";
                for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
                {
                    HINMEI = this.dto.itakuKeiyakuHinmei[i];

                    if (HINMEI.TSUMIKAE)
                    {
                        TSUMIKAE_HAIKI_SHURUI += HINMEI.HINMEI_NAME + "、";
                    }
                }
                if (TSUMIKAE_HAIKI_SHURUI != "")
                {
                    TSUMIKAE_HAIKI_SHURUI = TSUMIKAE_HAIKI_SHURUI.Substring(0, TSUMIKAE_HAIKI_SHURUI.Length - 2);
                }
                if (!TSUMIKAE.HOKAN_JOGEN_UNIT_CD.IsNull)
                {
                    var list_unit = UNIT_LIST.Where(o => o.UNIT_CD.Value == TSUMIKAE.HOKAN_JOGEN_UNIT_CD.Value).ToList();
                    if (list_unit != null && list_unit.Count > 0)
                    {
                        unitName = " " + list_unit[0].UNIT_NAME;
                    }
                }

                var genba = new M_GENBA();
                genba.GYOUSHA_CD = TSUMIKAE.UNPAN_GYOUSHA_CD;
                genba.GENBA_CD = TSUMIKAE.TSUMIKAE_HOKANBA_CD;
                genba = this.genbaDao.GetDataByCd(genba);
                M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                if (genba != null && !genba.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    todoufuken = this.todoufukenDao.GetDataByCd(genba.GENBA_TODOUFUKEN_CD.ToString());
                }
                string tdhk = "";
                if (todoufuken != null)
                {
                    tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                }
                row["ARTICLE02_05_03_HAIKI_SHURUI"] = TSUMIKAE_HAIKI_SHURUI;
                row["ARTICLE02_05_03_ADDRESS"] = tdhk + this.ConvertTwoText(TSUMIKAE.TSUMIKAE_HOKANBA_ADDRESS1, TSUMIKAE.TSUMIKAE_HOKANBA_ADDRESS2);
                row["ARTICLE02_05_03_HOKAN_LIMIT"] = this.ConvertSuuryo(TSUMIKAE.HOKAN_JOGEN) + unitName;
                row["BESSHI_I"] = "";
            }
            // 積替が２明細以上の場合
            else
            {
                dtBesshi_I = new DataTable();
                dtBesshi_I.Columns.Add("TSUMIKAE_HAIKI_SHURUI", typeof(string));
                dtBesshi_I.Columns.Add("TSUMIKAE_ADDRESS", typeof(string));
                dtBesshi_I.Columns.Add("TSUMIKAE_HOKAN_LIMIT", typeof(string));
                var row_I = dtBesshi_I.NewRow();
                var HINMEI = new M_ITAKU_KEIYAKU_HINMEI();
                string TSUMIKAE_HAIKI_SHURUI = "";
                for (int i = 0; i < this.dto.itakuKeiyakuHinmei.Length; i++)
                {
                    HINMEI = this.dto.itakuKeiyakuHinmei[i];

                    if (HINMEI.TSUMIKAE)
                    {
                        TSUMIKAE_HAIKI_SHURUI += HINMEI.HINMEI_NAME + "、";
                    }
                }
                if (TSUMIKAE_HAIKI_SHURUI != "")
                {
                    TSUMIKAE_HAIKI_SHURUI = TSUMIKAE_HAIKI_SHURUI.Substring(0, TSUMIKAE_HAIKI_SHURUI.Length - 2);
                }
                for (int i = 0; i < TSUMIKAE_LIST.Count; i++)
                {
                    TSUMIKAE = TSUMIKAE_LIST[i];
                    unitName = "";
                    if (!TSUMIKAE.HOKAN_JOGEN_UNIT_CD.IsNull)
                    {
                        var list_unit = UNIT_LIST.Where(o => o.UNIT_CD.Value == TSUMIKAE.HOKAN_JOGEN_UNIT_CD.Value).ToList();
                        if (list_unit != null && list_unit.Count > 0)
                        {
                            unitName = " " + list_unit[0].UNIT_NAME;
                        }
                    }

                    var genba = new M_GENBA();
                    genba.GYOUSHA_CD = TSUMIKAE.UNPAN_GYOUSHA_CD;
                    genba.GENBA_CD = TSUMIKAE.TSUMIKAE_HOKANBA_CD;
                    genba = this.genbaDao.GetDataByCd(genba);
                    M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                    if (genba != null && !genba.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        todoufuken = this.todoufukenDao.GetDataByCd(genba.GENBA_TODOUFUKEN_CD.ToString());
                    }
                    string tdhk = "";
                    if (todoufuken != null)
                    {
                        tdhk = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    }

                    row_I = dtBesshi_I.NewRow();
                    row_I["TSUMIKAE_HAIKI_SHURUI"] = TSUMIKAE_HAIKI_SHURUI;
                    row_I["TSUMIKAE_ADDRESS"] = tdhk + this.ConvertTwoText(TSUMIKAE.TSUMIKAE_HOKANBA_ADDRESS1, TSUMIKAE.TSUMIKAE_HOKANBA_ADDRESS2);
                    row_I["TSUMIKAE_HOKAN_LIMIT"] = this.ConvertSuuryo(TSUMIKAE.HOKAN_JOGEN) + unitName;
                    dtBesshi_I.Rows.Add(row_I);
                }
                row["BESSHI_I"] = "※別紙  積替保管に記載";
            }
            #endregion

            #region 産業廃棄物の種類
            // 産業廃棄物の種類
            var HINMEI_TEIJI_LIST = new List<M_ITAKU_KEIYAKU_HINMEI>();
            for (int i = 0; i < ITAKU_HINMEI_LIST.Count; i++)
            {
                ITAKU_HINMEI = ITAKU_HINMEI_LIST[i];
                if (!string.IsNullOrEmpty(ITAKU_HINMEI.BUNSEKISHOUMEISHO_TEIJIJIKI))
                {
                    HINMEI_TEIJI_LIST.Add(ITAKU_HINMEI);
                }
            }
            var HINMEI_TEIJI = new M_ITAKU_KEIYAKU_HINMEI();
            // 品名３明細以下の場合
            if (HINMEI_TEIJI_LIST.Count <= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    strCnt = (i + 1).ToString("00");
                    if (HINMEI_TEIJI_LIST.Count > i)
                    {
                        HINMEI_TEIJI = HINMEI_TEIJI_LIST[i];
                        row["ARTICLE03_05_HAIKIBUTSU_SHURUI" + strCnt] = HINMEI_TEIJI.HINMEI_NAME;
                        row["ARTICLE03_05_TEIJI_JIKI" + strCnt] = HINMEI_TEIJI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                    }
                    else
                    {
                        row["ARTICLE03_05_HAIKIBUTSU_SHURUI" + strCnt] = "";
                        row["ARTICLE03_05_TEIJI_JIKI" + strCnt] = "";
                    }
                }
                row["BESSHI_J"] = "";
            }
            // 品名４明細以上の場合
            else
            {
                dtBesshi_J = new DataTable();
                dtBesshi_J.Columns.Add("HAIKIBUTSU_SHURUI1", typeof(string));
                dtBesshi_J.Columns.Add("HAIKIBUTSU_SHURUI2", typeof(string));
                dtBesshi_J.Columns.Add("TEIJI_JIKI1", typeof(string));
                dtBesshi_J.Columns.Add("TEIJI_JIKI2", typeof(string));
                var row_J = dtBesshi_J.NewRow();
                for (int i = 0; i < HINMEI_TEIJI_LIST.Count; i++)
                {
                    HINMEI_TEIJI = HINMEI_TEIJI_LIST[i];
                    if (i % 2 == 0)
                    {
                        row_J = dtBesshi_J.NewRow();
                        row_J["HAIKIBUTSU_SHURUI1"] = HINMEI_TEIJI.HINMEI_NAME;
                        row_J["TEIJI_JIKI1"] = HINMEI_TEIJI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                        if (i == HINMEI_TEIJI_LIST.Count - 1)
                        {
                            row_J["HAIKIBUTSU_SHURUI2"] = string.Empty;
                            row_J["TEIJI_JIKI2"] = string.Empty;
                            dtBesshi_J.Rows.Add(row_J);
                        }
                    }
                    else
                    {
                        row_J["HAIKIBUTSU_SHURUI2"] = HINMEI_TEIJI.HINMEI_NAME;
                        row_J["TEIJI_JIKI2"] = HINMEI_TEIJI.BUNSEKISHOUMEISHO_TEIJIJIKI;
                        dtBesshi_J.Rows.Add(row_J);
                    }
                }
                row["BESSHI_J"] = "※別紙  適正処理に必要な情報の提供に記載";
            }
            #endregion

            #region 契約期間
            //和暦変更のため
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
            System.Globalization.Calendar cal = new System.Globalization.JapaneseCalendar();
            ci.DateTimeFormat.Calendar = cal;

            var initDate = DateTime.Today.Date.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_DATE_WAREKI_FORMAT_INIT, ci);
            var strYUUKOU_BEGIN = initDate;
            if (!this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.IsNull)
            {
                strYUUKOU_BEGIN = this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.Value.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_YUUKOU_KIKAN_WAREKI_FORMAT, ci);
            }
            var strYUUKOU_END = initDate;
            if (!this.dto.ItakuKeiyakuKihon.YUUKOU_END.IsNull)
            {
                strYUUKOU_END = this.dto.ItakuKeiyakuKihon.YUUKOU_END.Value.ToString(Const.ItakuKeiyakuHoshuConstans.OUTPUT_YUUKOU_KIKAN_WAREKI_FORMAT, ci);
            }

            // 更新種別が「自動更新」の場合
            if (this.dto.ItakuKeiyakuKihon.KOUSHIN_SHUBETSU == 1)
            {
                row["ARTICLE14_TAN"] = false;
                row["ARTICLE14_AUTO"] = true;
                row["ARTICLE14_TAN_FROM"] = initDate;
                row["ARTICLE14_TAN_TO"] = initDate;
                row["ARTICLE14_AUTO_FROM"] = strYUUKOU_BEGIN;
                row["ARTICLE14_AUTO_TO"] = strYUUKOU_END;

                // 契約期間
                var strYUUKOU_SPAN = "";
                if (!this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.IsNull && !this.dto.ItakuKeiyakuKihon.YUUKOU_END.IsNull)
                {
                    DateTime begin = this.dto.ItakuKeiyakuKihon.YUUKOU_BEGIN.Value;
                    DateTime end = this.dto.ItakuKeiyakuKihon.YUUKOU_END.Value;
                    strYUUKOU_SPAN = this.GetDateYearSpan(begin, end);
                }
                row["ARTICLE14_AUTO_SPAN"] = strYUUKOU_SPAN;
            }
            // 更新種別が「単発」の場合
            else
            {
                row["ARTICLE14_TAN"] = true;
                row["ARTICLE14_AUTO"] = false;
                row["ARTICLE14_TAN_FROM"] = strYUUKOU_BEGIN;
                row["ARTICLE14_TAN_TO"] = strYUUKOU_END;
                row["ARTICLE14_AUTO_FROM"] = initDate;
                row["ARTICLE14_AUTO_TO"] = initDate;
            }

            // 契約日
            if (this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE.IsNull)
            {
                row["CREATE_DATE"] = ItakuKeiyakuHoshuConstans.KEIYAKUSHO_KEIYAKU_DATE_INIT;
            }
            else
            {
                row["CREATE_DATE"] = this.dto.ItakuKeiyakuKihon.KEIYAKUSHO_KEIYAKU_DATE.Value.ToString(ItakuKeiyakuHoshuConstans.OUTPUT_DATE_WAREKI_FORMAT, ci);
            }
            todoufukenName = "";
            // 甲
            if (!HST_GYOUHSA.GYOUSHA_TODOUFUKEN_CD.IsNull)
            {
                list_todoufuken = TODOFUKEN_LIST.Where(o => o.TODOUFUKEN_CD.Value == HST_GYOUHSA.GYOUSHA_TODOUFUKEN_CD.Value).ToList();
                if (list_todoufuken != null && list_todoufuken.Count > 0)
                {
                    todoufukenName = list_todoufuken[0].TODOUFUKEN_NAME;
                }
            }
            row["KOU_ADDRESS"] = todoufukenName + this.ConvertTwoText(HST_GYOUHSA.GYOUSHA_ADDRESS1, HST_GYOUHSA.GYOUSHA_ADDRESS2);
            row["KOU_NAME"] = this.ConvertTwoText(HST_GYOUHSA.GYOUSHA_NAME1, HST_GYOUHSA.GYOUSHA_NAME2);
            row["KOU_DAIHYOU_NAME"] = HST_GYOUHSA.GYOUSHA_DAIHYOU;
            // 乙
            todoufukenName = "";
            if (!UPN_GYOUHSA.GYOUSHA_TODOUFUKEN_CD.IsNull)
            {
                list_todoufuken = TODOFUKEN_LIST.Where(o => o.TODOUFUKEN_CD.Value == UPN_GYOUHSA.GYOUSHA_TODOUFUKEN_CD.Value).ToList();
                if (list_todoufuken != null && list_todoufuken.Count > 0)
                {
                    todoufukenName = list_todoufuken[0].TODOUFUKEN_NAME;
                }
            }
            row["OTSU_ADDRESS"] = todoufukenName + this.ConvertTwoText(UPN_GYOUHSA.GYOUSHA_ADDRESS1, UPN_GYOUHSA.GYOUSHA_ADDRESS2);
            row["OTSU_NAME"] = this.ConvertTwoText(UPN_GYOUHSA.GYOUSHA_NAME1, UPN_GYOUHSA.GYOUSHA_NAME2);
            row["OTSU_DAIHYOU_NAME"] = UPN_GYOUHSA.GYOUSHA_DAIHYOU;
            #endregion
            reportTable.Rows.Add(row);
            return reportTable;
        }

        #endregion

        #region イベント処理

        /// <summary>
        /// 排出事業者CD Validated時処理
        /// </summary>
        public bool CheckHaishutsuJigyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 入力値がクリアされていた場合、関連項目をクリアする
                if (this.form.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(string.Empty))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.form.GYOUSHA_TODOUFUKEN_NAME.Text = string.Empty;
                    this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
                    this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
                    this.form.HAISHUTSU_JIGYOUJOU_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_TODOUFUKEN_NAME.Text = string.Empty;
                    this.form.GENBA_ADDRESS1.Text = string.Empty;
                    this.form.GENBA_ADDRESS2.Text = string.Empty;
                    this.kihonHstGenbaTable.Rows.Clear();
                    this.form.listKihonHstGenba.ReadOnly = false;
                    this.form.KOBETSU_SHITEI_CHECK.Enabled = true;
                }
                else
                {
                    if (!this.form.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(this.form.PreviousValue))
                    {
                        // コードから略称、住所をセットする
                        bool catchErr = this.SetJigyoushaData(this.form.HAISHUTSU_JIGYOUSHA_CD.Text, this.form.GYOUSHA_NAME_RYAKU, this.form.GYOUSHA_ADDRESS1, this.form.GYOUSHA_ADDRESS2, this.form.GYOUSHA_TODOUFUKEN_NAME);
                        if (catchErr)
                        {
                            return true;
                        }
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Text = string.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        this.form.GENBA_TODOUFUKEN_NAME.Text = string.Empty;
                        this.form.GENBA_ADDRESS1.Text = string.Empty;
                        this.form.GENBA_ADDRESS2.Text = string.Empty;
                        this.kihonHstGenbaTable.Rows.Clear();
                        this.form.listKihonHstGenba.ReadOnly = false;
                        this.form.KOBETSU_SHITEI_CHECK.Enabled = true;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHaishutsuJigyoushaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 積替業者CD Validated時処理
        /// </summary>
        public bool CheckUnpanGyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                GcMultiRow list = this.form.listBetsu2;
                // 入力値がクリアされていた場合、関連項目をクリアする
                if (Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value).Equals(string.Empty))
                {
                    list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "UNPAN_TODOUFUKEN_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                }
                else
                {
                    if (!Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value).Equals(this.form.PreviousValue))
                    {
                        // コードから略称、住所をセットする
                        bool catchErr = this.SetJigyoushaData(Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value), list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_NAME"], list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"], list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"], list[list.CurrentCell.RowIndex, "UNPAN_TODOUFUKEN_NAME"]);
                        if (catchErr)
                        {
                            return true;
                        }
                        //this.form.PreviousValue = Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value);
                    }
                }

                if (this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" && list.CurrentCell.RowIndex == this.form.listBetsu2.Rows.Count - 1)
                {
                    Row dr1 = this.form.listBetsu2.Rows[list.CurrentCell.RowIndex];
                    if (!string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_CD"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_TODOUFUKEN_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_ADDRESS1"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_ADDRESS2"].Value)))
                    {
                        DataTable dt = ((DataTable)this.form.listBetsu2.DataSource);
                        dt.Rows.Add();
                        this.form.listBetsu2.DataSource = dt;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 積替業者CD Validated時処理
        /// </summary>
        public bool CheckTSUMIKAEGyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                GcMultiRow list = this.form.listTsumikae;
                // 入力値がクリアされていた場合、関連項目をクリアする
                if (Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value).Equals(string.Empty))
                {
                    list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"].Value = string.Empty;
                }
                else
                {
                    if (!Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value).Equals(this.form.PreviousValue))
                    {
                        // コードから略称、住所をセットする
                        bool catchErr = this.SetJigyoushaData(Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value), list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_NAME"], null, null, null);
                        if (catchErr)
                        {
                            return true;
                        }
                        list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_NAME"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"].Value = string.Empty;
                        this.form.PreviousValue = Convert.ToString(list[list.CurrentCell.RowIndex, "UNPAN_GYOUSHA_CD"].Value);
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTSUMIKAEGyoushaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 処分業者CD Validated時処理
        /// </summary>
        public bool CheckShobunGyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                GcMultiRow list = this.form.listBetsu3;
                // 入力値がクリアされていた場合、関連項目をクリアする
                if (Convert.ToString(list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value).Equals(string.Empty))
                {
                    list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                }
                else
                {
                    if (!Convert.ToString(list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value).Equals(this.form.PreviousValue))
                    {
                        // コードから略称、住所をセットする
                        bool catchErr = this.SetJigyoushaData(Convert.ToString(list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value), list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_NAME"], list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_ADDRESS1"], list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_ADDRESS2"], list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"]);
                        if (catchErr)
                        {
                            return true;
                        }
                        list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        //this.form.PreviousValue = Convert.ToString(list[list.CurrentCell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value);
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunGyoushaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 最終処分業者CD Validated時処理
        /// </summary>
        public bool CheckLastShobunGyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                GcMultiRow list = this.form.listBetsu4;
                // 入力値がクリアされていた場合、関連項目をクリアする
                if (Convert.ToString(list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value).Equals(string.Empty))
                {
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                    list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                }
                else
                {
                    if (!Convert.ToString(list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value).Equals(this.form.PreviousValue))
                    {
                        // コードから略称、住所をセットする
                        bool catchErr = this.SetJigyoushaData(Convert.ToString(list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value), list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"], list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS1"], list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS2"], list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"]);
                        if (catchErr)
                        {
                            return true;
                        }
                        list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        list[list.CurrentCell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        this.form.PreviousValue = Convert.ToString(list[list.CurrentCell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value);
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckLastShobunGyoushaCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 排出事業場CD Validated時処理
        /// </summary>
        /// <param name="e"></param>
        public bool CheckHaishutsuJigyoujouCD(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                r_framework.Logic.MessageBoxShowLogic showLogic = new r_framework.Logic.MessageBoxShowLogic();

                // 排出事業場CDが入力されている状態で、排出事業者CDがクリアされていた場合、エラーとする
                if (!this.form.HAISHUTSU_JIGYOUJOU_CD.Text.Equals(string.Empty) && this.form.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(string.Empty))
                {
                    if (this.errorCancelFlg)
                    {
                        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                        showLogic.MessageBoxShow("E051", "排出事業者");
                        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Text = string.Empty;
                        e.Cancel = true;
                    }
                }

                // 排出事業場CDがクリアされた場合、排出事業場一覧をクリアする
                if (this.form.HAISHUTSU_JIGYOUJOU_CD.Text.Equals(string.Empty))
                {
                    if (this.kihonHstGenbaTable.Rows.Count > 0)
                    {
                        this.kihonHstGenbaTable.Rows.Clear();
                        this.form.listKihonHstGenba.ReadOnly = false;
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        this.form.GENBA_ADDRESS1.Text = string.Empty;
                        this.form.GENBA_ADDRESS2.Text = string.Empty;
                        this.form.GENBA_TODOUFUKEN_NAME.Text = string.Empty;
                    }
                }
                else
                {
                    // マスタ存在チェックを行う
                    M_GENBA cond = new M_GENBA();
                    cond.GYOUSHA_CD = this.form.HAISHUTSU_JIGYOUSHA_CD.Text;
                    cond.GENBA_CD = this.form.HAISHUTSU_JIGYOUJOU_CD.Text;
                    // 20151021 BUNN #12040 STR
                    cond.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                    // 20151021 BUNN #12040 END
                    M_GENBA[] gen = this.genbaDao.GetAllValidData(cond);
                    if (gen == null || gen.Length == 0)
                    {
                        if (this.errorCancelFlg)
                        {
                            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                            this.form.GENBA_ADDRESS1.Text = string.Empty;
                            this.form.GENBA_ADDRESS2.Text = string.Empty;
                            this.form.GENBA_TODOUFUKEN_NAME.Text = string.Empty;

                            showLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.M_GENBA);
                            this.form.HAISHUTSU_JIGYOUJOU_CD.SelectAll();
                            e.Cancel = true;
                        }

                        this.isError = true;
                        LogUtility.DebugMethodEnd(e);
                        return false;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.Text = gen[0].GENBA_NAME_RYAKU;
                        this.form.GENBA_ADDRESS1.Text = gen[0].GENBA_ADDRESS1;
                        this.form.GENBA_ADDRESS2.Text = gen[0].GENBA_ADDRESS2;
                        M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                        if (!gen[0].GENBA_TODOUFUKEN_CD.IsNull)
                        {
                            todoufuken = this.todoufukenDao.GetDataByCd(gen[0].GENBA_TODOUFUKEN_CD.ToString());
                        }
                        if (todoufuken != null)
                        {
                            this.form.GENBA_TODOUFUKEN_NAME.Text = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        }
                    }
                    if (!this.form.listKihonHstGenba.ReadOnly)
                    {
                        this.form.listKihonHstGenba.ReadOnly = true;
                    }
                    this.kihonHstGenbaTable.Rows.Clear();
                    DataRow row = this.kihonHstGenbaTable.NewRow();
                    row["HAISHUTSU_JIGYOUJOU_CD"] = this.form.HAISHUTSU_JIGYOUJOU_CD.Text;
                    row["HAISHUTSU_JIGYOUJOU_NAME"] = this.form.GENBA_NAME_RYAKU.Text;
                    row["HAISHUTSU_JIGYOUJOU_ADDRESS1"] = this.form.GENBA_ADDRESS1.Text;
                    row["HAISHUTSU_JIGYOUJOU_ADDRESS2"] = this.form.GENBA_ADDRESS2.Text;
                    row["HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME"] = this.form.GENBA_TODOUFUKEN_NAME.Text;
                    this.kihonHstGenbaTable.Rows.Add(row);
                    this.SetIchiran(this.form.listKihonHstGenba, kihonHstGenbaTable);
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckHaishutsuJigyoujouCD", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHaishutsuJigyoujouCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約書参照ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool FileRefClick()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ユーザ定義情報を取得
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ファイルアップロード参照先のフォルダを取得
                string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                //※※※※※暫定措置※※※※※
                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                if (!string.IsNullOrEmpty(serverPath))
                {
                    initialPath = serverPath;
                }
                var windowHandle = this.form.Handle;
                var isFileSelect = true;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.ITAKU_KEIYAKU_FILE_PATH.Text = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約書閲覧ボタン押下処理
        /// </summary>
        public bool BrowseClick()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.ITAKU_KEIYAKU_FILE_PATH.Text))
                {
                    if (SystemProperty.IsTerminalMode)
                    {
                        if (string.IsNullOrEmpty(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectoryNonMsg()))
                        {
                            MessageBox.Show("閲覧を行う前に、印刷設定の出力先フォルダを指定してください。",
                                            "アラート",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                            return false;
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
                                    writer.Write(this.form.ITAKU_KEIYAKU_FILE_PATH.Text);
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
                        System.Diagnostics.Process.Start(this.form.ITAKU_KEIYAKU_FILE_PATH.Text);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    // SQLエラー用メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E200");
                    return true;
                }
                else
                {
                    LogUtility.Error("BrowseClick", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                    return true;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// タブ切り替えイベント処理
        /// </summary>
        public bool TabSelect()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 基本情報タブ選択
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 0 && this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage1") && this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.form.HAISHUTSU_JIGYOUSHA_CD.Enabled = true;
                    this.form.HAISHUTSU_JIGYOUSHA_SEARCH_BUTTON.Enabled = true;
                    if (this.isCommonJigyoujouInput())
                    {
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = true;
                        this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = true;
                    }
                    else
                    {
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = false;
                        this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = false;
                    }
                }
                else
                {
                    this.form.HAISHUTSU_JIGYOUSHA_CD.Enabled = false;
                    this.form.HAISHUTSU_JIGYOUSHA_SEARCH_BUTTON.Enabled = false;
                    if (this.isCommonJigyoujouInput())
                    {
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = true;
                        this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = true;
                    }
                    else
                    {
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = false;
                        this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = false;
                    }
                }

                // 品名タブ選択
                //if (this.isFirst && this.form.tabItakuKeiyakuData.SelectedIndex == 1 && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage2") && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 委託契約TAB2---品名の検索
                    M_ITAKU_KEIYAKU_HINMEI hinmeiSearchSetting = new M_ITAKU_KEIYAKU_HINMEI();
                    hinmeiSearchSetting.SYSTEM_ID = this.SystemId;
                    this.hinmeiTable = this.hinmeiDao.GetDataBySqlFile(GET_ITAKU_KEIYAKU_HIMEI_DATA_SQL, hinmeiSearchSetting);
                    this.SetIchiran(this.form.listHinmei, hinmeiTable);
                    this.isFirst = false;
                }

                // 委託契約基本情報セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 0)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage1"))
                {
                    this.form.listKihonHstGenba.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListKihonHstGenbaCellValidating);
                    this.form.listKihonHstGenba.BeginEdit(false);
                    this.form.listKihonHstGenba.EndEdit();
                    this.form.listKihonHstGenba.NotifyCurrentCellDirty(false);

                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listKihonHstGenba, this.kihonNewRowFlg);
                    this.kihonNewRowFlg = false;
                }
                else
                {
                    this.form.listKihonHstGenba.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListKihonHstGenbaCellValidating);
                }

                // 委託契約別表1排出セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 1)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage2"))
                {
                    this.form.listHinmei.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1HstCellValidating);
                    this.form.listHinmei.CellValidated -= new EventHandler<CellEventArgs>(this.form.ListBetsu1HstCellValidated);
                    this.form.listHinmei.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1HstCellValidating);
                    this.form.listHinmei.CellValidated += new EventHandler<CellEventArgs>(this.form.ListBetsu1HstCellValidated);
                    
                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listHinmei, this.hinmeiNewRowFlg);
                    this.hinmeiNewRowFlg = false;
                }
                else
                {
                    this.form.listHinmei.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1HstCellValidating);
                    this.form.listHinmei.CellValidated -= new EventHandler<CellEventArgs>(this.form.ListBetsu1HstCellValidated);
                }

                // 委託契約別表1予定セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 2)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage3"))
                {
                    this.form.listHoukokushoBunrui.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1YoteiCellValidating);
                    
                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listHoukokushoBunrui, this.houkokuNewRowFlg);
                    this.houkokuNewRowFlg = false;
                }
                else
                {
                    this.form.listHoukokushoBunrui.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu1YoteiCellValidating);
                }

                // 委託契約別表2セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 3)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage4"))
                {
                    this.form.listBetsu2.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu2CellValidating);
                }
                else
                {
                    this.form.listBetsu2.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu2CellValidating);
                }

                // 委託契約積替セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 4)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage5"))
                {
                    this.form.listTsumikae.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListTsumikaeCellValidating);
                    this.form.listTsumikae.CellValidated += new EventHandler<CellEventArgs>(this.form.ListTsumikaeCellValidated);

                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listTsumikae, this.tsumikaeNewRowFlg);
                    this.tsumikaeNewRowFlg = false;
                }
                else
                {
                    this.form.listTsumikae.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListTsumikaeCellValidating);
                    this.form.listTsumikae.CellValidated -= new EventHandler<CellEventArgs>(this.form.ListTsumikaeCellValidated);
                }

                // 委託契約別表3セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 5)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage6"))
                {
                    this.form.listBetsu3.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu3CellValidating);

                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listBetsu3, this.sbnNewRowFlg);
                    this.sbnNewRowFlg = false;
                }
                else
                {
                    this.form.listBetsu3.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu3CellValidating);
                }

                // 委託契約別表4セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 6)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage7"))
                {
                    this.form.listBetsu4.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu4CellValidating);

                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listBetsu4, this.lastNewRowFlg);
                    this.lastNewRowFlg = false;
                }
                else
                {
                    this.form.listBetsu4.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu4CellValidating);
                }

                // 委託契約覚書セル編集終了イベント
                //if (this.form.tabItakuKeiyakuData.SelectedIndex == 7)
                if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage8"))
                {
                    this.form.listOboe.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListOboeCellValidating);

                    // セル位置を制御する。
                    this.ChangeCurrentCellPosition(this.form.listOboe, this.oboeNewRowFlg);
                    this.oboeNewRowFlg = false;
                }
                else
                {
                    this.form.listOboe.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListOboeCellValidating);
                }

                // フォーカスをタブに設定する。
                this.form.tabItakuKeiyakuData.Focus();

                // 別表１（予定）タブ選択
                /*if (this.form.tabItakuKeiyakuData.SelectedIndex == 2)
                {
                    // 予定情報が未登録の場合、排出現場情報・排出情報から作成を行う
                    if (this.betsu1YoteiTable.Rows.Count == 0)
                    {
                        for (int i = 0; i < this.kihonHstGenbaTable.Rows.Count; i++)
                        {
                            for (int j = 0; j < this.hinmeiTable.Rows.Count; j++)
                            {
                                // 事業場CDが未入力の行は対象としない
                                if (string.IsNullOrWhiteSpace(this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_CD"].ToString())) continue;
                                // 品名CDが未入力の行は対象としない
                                if (string.IsNullOrWhiteSpace(this.hinmeiTable.Rows[j]["HINMEI_CD"].ToString())) continue;

                                DataRow eRow = this.betsu1YoteiTable.NewRow();
                                eRow["HAISHUTSU_JIGYOUJOU_CD"] = this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_CD"];
                                if (!string.IsNullOrWhiteSpace(this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_NAME"].ToString()))
                                {
                                    eRow["HAISHUTSU_JIGYOUJOU_NAME"] = this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_NAME"];
                                }
                                else
                                {
                                    eRow["HAISHUTSU_JIGYOUJOU_NAME"] = this.form.listKihonHstGenba.Rows[j]["GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value;
                                }
                                if (!string.IsNullOrWhiteSpace(this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_ADDRESS1"].ToString()))
                                {
                                    eRow["HAISHUTSU_JIGYOUJOU_ADDRESS1"] = this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_ADDRESS1"];
                                }
                                else
                                {
                                    eRow["HAISHUTSU_JIGYOUJOU_ADDRESS1"] = this.form.listKihonHstGenba.Rows[j]["GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value;
                                }
                                if (!string.IsNullOrWhiteSpace(this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_ADDRESS2"].ToString()))
                                {
                                    eRow["HAISHUTSU_JIGYOUJOU_ADDRESS2"] = this.kihonHstGenbaTable.Rows[i]["HAISHUTSU_JIGYOUJOU_ADDRESS2"];
                                }
                                else
                                {
                                    eRow["HAISHUTSU_JIGYOUJOU_ADDRESS2"] = this.form.listKihonHstGenba.Rows[j]["GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2"].Value;
                                }
                                eRow["HINMEI_CD"] = this.hinmeiTable.Rows[j]["HINMEI_CD"];
                                if (!string.IsNullOrWhiteSpace(this.hinmeiTable.Rows[j]["HINMEI_NAME"].ToString()))
                                {
                                    eRow["HINMEI_NAME"] = this.hinmeiTable.Rows[j]["HINMEI_NAME"];
                                }
                                else
                                {
                                    eRow["HINMEI_NAME"] = this.form.listHimei.Rows[j]["HINMEI_NAME"].Value;
                                }
                                this.betsu1YoteiTable.Rows.Add(eRow);
                            }
                        }
                        this.SetIchiran(this.form.listHoukokushoBunrui, this.betsu1YoteiTable);
                    }

                    // 排出現場情報・排出情報が未登録の場合、予定データは入力を許可しない
                    bool canInput = false;
                    for (int i = 0; i < this.kihonHstGenbaTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.hinmeiTable.Rows.Count; j++)
                        {
                            canInput = true;
                            break;
                        }
                        if (canInput) break;
                    }
                    if (canInput)
                    {
                        this.form.listHoukokushoBunrui.Enabled = true;
                    }
                    else
                    {
                        this.form.listHoukokushoBunrui.Enabled = false;
                    }

                    this.form.tabItakuKeiyakuData.SelectedTab.Focus();
                }*/

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("TabSelect", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TabSelect", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// セル位置を制御する。
        /// </summary>
        /// <param name="row"></param>
        private void ChangeCurrentCellPosition(GcCustomMultiRow row, bool flg)
        {
            CellPosition pos = row.CurrentCellPosition;
            if (pos.RowIndex != -1 && flg)
            {
                if ((row.RowCount - 1) == pos.RowIndex)
                {
                    return;
                }
                row.CurrentCellPosition = new CellPosition(pos.RowIndex + 1, pos.CellIndex);
            }
        }

        /// <summary>
        /// 共通部事業場入力可否
        /// </summary>
        /// <returns></returns>
        public bool isCommonJigyoujouInput()
        {
            LogUtility.DebugMethodStart();

            bool result = true;

            // 排出事業場CDが未入力でかつ現場一覧に入力がある場合、共通部分の排出事業場CDは入力不可とする
            if (string.IsNullOrWhiteSpace(this.form.HAISHUTSU_JIGYOUJOU_CD.Text))
            {
                foreach (Row row in this.form.listKihonHstGenba.Rows)
                {
                    if (row["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value != null && !string.IsNullOrWhiteSpace(row["GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()))
                    {
                        result = false;
                        break;
                    }
                }
                if (this.form.KOBETSU_SHITEI_CHECK.Checked)
                {
                    result = true;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// システムIDチェック処理
        /// </summary>
        public bool CheckSystemId()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param;
                bool catchErr = false;

                if (!this.form.SYSTEM_ID.Text.Equals(string.Empty) && this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 入力されたキーで検索を実施する
                    M_ITAKU_KEIYAKU_KIHON itakuKeiyakuKihonSearchParam = new M_ITAKU_KEIYAKU_KIHON();
                    itakuKeiyakuKihonSearchParam.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    M_ITAKU_KEIYAKU_KIHON itakuKeiyakuKihon = kihonDao.GetDataBySystemId(itakuKeiyakuKihonSearchParam);

                    // データがある場合でかつ論理削除されていない場合
                    if (itakuKeiyakuKihon != null && !itakuKeiyakuKihon.DELETE_FLG.Value)
                    {
                        if (itakuKeiyakuKihon.ITAKU_KEIYAKU_TYPE.ToString().Equals("2"))
                        {
                            msgLogic.MessageBoxShow("E137", "システムID", "建廃個別様式契約書");
                            //フォーカスを移動させない
                            this.form.SYSTEM_ID.Focus();
                            this.form.SYSTEM_ID.SelectAll();
                        }
                        else
                        {
                            var result = msgLogic.MessageBoxShow("C022");
                            if (result == DialogResult.Yes)
                            {
                                // 権限チェック
                                // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                                if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    //修正モードに移行する
                                    this.SystemId = this.form.SYSTEM_ID.Text;
                                    this.form.ITAKU_KEIYAKU_NO.Focus();
                                    this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                                    catchErr = this.WindowInitUpdate((BusinessBaseForm)this.form.ParentForm);
                                    if (catchErr)
                                    {
                                        return true;
                                    }
                                }
                                else if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    //参照モードに移行する
                                    this.SystemId = this.form.SYSTEM_ID.Text;
                                    this.form.ITAKU_KEIYAKU_NO.Focus();
                                    this.form.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                                    catchErr = this.WindowInitReference((BusinessBaseForm)this.form.ParentForm);
                                    if (catchErr)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    msgLogic.MessageBoxShow("E158", "修正");
                                    //システムIDをクリアしフォーカスを移動させない
                                    this.form.SYSTEM_ID.Text = string.Empty;
                                    this.form.SYSTEM_ID.Focus();
                                }
                            }
                            else
                            {
                                //システムIDをクリアしフォーカスを移動させない
                                this.form.SYSTEM_ID.Text = string.Empty;
                                this.form.SYSTEM_ID.Focus();
                            }
                        }
                    }
                    // レコードがある場合でかつ論理削除されている場合
                    if (itakuKeiyakuKihon != null && itakuKeiyakuKihon.DELETE_FLG.Value)
                    {
                        if (itakuKeiyakuKihon.ITAKU_KEIYAKU_TYPE.ToString().Equals("2"))
                        {
                            msgLogic.MessageBoxShow("E137", "システムID", "建廃個別様式契約書");
                            //フォーカスを移動させない
                            this.form.SYSTEM_ID.Focus();
                            this.form.SYSTEM_ID.SelectAll();
                        }
                        else
                        {
                            var result = msgLogic.MessageBoxShow("C057");
                            if (result == DialogResult.Yes)
                            {
                                // 権限チェック
                                // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                                if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    //修正モードに移行する
                                    this.SystemId = this.form.SYSTEM_ID.Text;
                                    this.form.ITAKU_KEIYAKU_NO.Focus();
                                    this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                                    catchErr = this.WindowInitUpdate((BusinessBaseForm)this.form.ParentForm);
                                    if (catchErr)
                                    {
                                        return true;
                                    }
                                }
                                else if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    //参照モードに移行する
                                    this.SystemId = this.form.SYSTEM_ID.Text;
                                    this.form.ITAKU_KEIYAKU_NO.Focus();
                                    this.form.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                                    catchErr = this.WindowInitReference((BusinessBaseForm)this.form.ParentForm);
                                    if (catchErr)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    msgLogic.MessageBoxShow("E158", "修正");
                                    //フォーカスを移動させない
                                    this.form.SYSTEM_ID.Clear();
                                    this.form.SYSTEM_ID.Focus();
                                    this.form.SYSTEM_ID.SelectAll();
                                }
                            }
                            else
                            {
                                //フォーカスを移動させない
                                this.form.SYSTEM_ID.Clear();
                                this.form.SYSTEM_ID.Focus();
                                this.form.SYSTEM_ID.SelectAll();
                            }
                        }
                    }

                    // レコードがない場合
                    if (itakuKeiyakuKihon == null)
                    {
                        param = new string[1];
                        param[0] = ItakuKeiyakuHoshu.Properties.Resources.SYSTEM_ID;
                        msgLogic.MessageBoxShow("C023", param);
                        //フォーカスを移動させない
                        this.form.SYSTEM_ID.Text = string.Empty;
                        this.form.SYSTEM_ID.Focus();
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSystemId", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 更新種別テキスト変更時処理
        /// </summary>
        public bool KoushinShubetsuTextChanged()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ステータスチェック
                bool catchErr = this.CheckStatus();
                if (catchErr)
                {
                    return true;
                }

                // 関連項目制御
                switch (this.form.KOUSHIN_SHUBETSU.Text)
                {
                    case "1":
                        this.form.KOUSHIN_END_DATE.Enabled = true;
                        break;

                    case "2":
                        this.form.KOUSHIN_END_DATE.Value = null;
                        this.form.KOUSHIN_END_DATE.Enabled = false;
                        break;

                    default:
                        this.form.KOUSHIN_END_DATE.Enabled = true;
                        break;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("KoushinShubetsuTextChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// セル編集終了時処理
        /// </summary>
        /// <param name="e"></param>
        public bool ListKihonHstGenbaCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                string[] str = new string[1];
                GcCustomMultiRow list = this.form.listKihonHstGenba;

                if (list.Rows[e.RowIndex].IsNewRow)
                {
                    this.kihonNewRowFlg = true;
                }

                r_framework.Logic.MessageBoxShowLogic msgLogic = new r_framework.Logic.MessageBoxShowLogic();

                if (e.CellName.Equals("GENBA_HAISHUTSU_JIGYOUJOU_CD") && list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    // 排出事業場CDが入力されている状態で、排出事業者CDがクリアされていた場合、エラーとする
                    if (list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value != null && !string.IsNullOrWhiteSpace(list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()) && string.IsNullOrWhiteSpace(this.form.HAISHUTSU_JIGYOUSHA_CD.Text))
                    {
                        if (this.errorCancelFlg)
                        {
                            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                            msgLogic.MessageBoxShow("E051", "排出事業者");
                            // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value = string.Empty;
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }
                            e.Cancel = true;
                        }
                        this.isError = true;
                        LogUtility.DebugMethodEnd(e, false);
                        return false;
                    }

                    string jigyoujou = list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(jigyoujou) && !string.IsNullOrWhiteSpace(this.form.HAISHUTSU_JIGYOUSHA_CD.Text))
                    {
                        // 重複チェック
                        for (int i = 0; i < list.Rows.Count; i++)
                        {
                            if (list.Rows[i].IsNewRow) continue;
                            if (i == e.RowIndex) continue;

                            if (jigyoujou.Equals(list[i, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()))
                            {
                                if (this.errorCancelFlg)
                                {
                                    if (list.EditingControl != null)
                                    {
                                        list.EditingControl.Text = string.Empty;
                                    }

                                    msgLogic.MessageBoxShow("E003", ItakuKeiyakuHoshu.Properties.Resources.HAISHUTSU_JIGYOUJOU, jigyoujou);
                                    e.Cancel = true;
                                    isSeted = true;
                                }
                                this.isError = true;
                                LogUtility.DebugMethodEnd(e, false);
                                return false;
                            }
                        }

                        // マスタ存在チェック
                        M_GENBA genba = new M_GENBA();
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        genba.GYOUSHA_CD = this.form.HAISHUTSU_JIGYOUSHA_CD.Text;
                        genba.GENBA_CD = jigyoujou;
                        // 20151021 BUNN #12040 STR
                        genba.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                        // 20151021 BUNN #12040 END
                        DataTable dt = this.genbaDao.GetDataBySqlFileWithGyousha(CHECK_GENBA_DATA_SQL, genba, gyousha);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                            list[e.RowIndex, "HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME"].Value = dt.Rows[0]["TODOUFUKEN_NAME"].ToString();
                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value = dt.Rows[0]["GENBA_ADDRESS1"].ToString();
                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2"].Value = dt.Rows[0]["GENBA_ADDRESS2"].ToString();
                            isSeted = true;
                        }
                        else
                        {
                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value = string.Empty;
                            list[e.RowIndex, "HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                            list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.M_GENBA);
                                e.Cancel = true;
                                previousErrorCd = jigyoujou;
                            }
                            this.isError = true;
                        }

                        this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = this.isCommonJigyoujouInput();
                        this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = this.isCommonJigyoujouInput();
                    }
                    else
                    {
                        list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value = string.Empty;
                        list[e.RowIndex, "HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                        list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        list[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = this.isCommonJigyoujouInput();
                        this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = this.isCommonJigyoujouInput();
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("ListKihonHstGenbaCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 別表1排出一覧のセルエンターイベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu1HstCellEnter(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.CellName.Equals("HINMEI_CD"))
                {
                    this.form.PreviousCd = Convert.ToString(this.form.listHinmei[e.RowIndex, "HINMEI_CD"].Value);
                    this.form.PreviousName = Convert.ToString(this.form.listHinmei[e.RowIndex, "HINMEI_NAME"].Value);
                }

                //if ((e.CellName.Equals("UNPAN_YOTEI_SUU") || e.CellName.Equals("SHOBUN_YOTEI_SUU"))
                //    && !this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.IsNull)
                //{
                //    var eCell = this.form.listHimei.CurrentCell as GcCustomNumericTextBox2Cell;
                //    int cd = this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Value;
                //    double num = 0;
                //    char[] chars = null;
                //    switch (cd)
                //    {
                //        case 1:
                //        case 2:
                //            num = 9999999;
                //            chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
                //            break;
                //        case 3:
                //            num = 9999999.9;
                //            break;
                //        case 4:
                //            num = 9999999.99;
                //            break;
                //        case 5:
                //            num = 9999999.99;
                //            break;
                //        default:
                //            num = 9999999.99;
                //            break;
                //    }
                //    eCell.CharacterLimitList = chars;
                //    eCell.RangeSetting.Max = Convert.ToDecimal(num);
                //}

                //if ((e.CellName.Equals("UNPAN_ITAKU_TANKA") || e.CellName.Equals("UNPAN_ITAKU_TANKA"))
                //    && !this.sysInfoEntity.ITAKU_KEIYAKU_TANKA_FORMAT_CD.IsNull)
                //{
                //    var eCell = this.form.listHimei.CurrentCell as GcCustomNumericTextBox2Cell;
                //    int cd = this.sysInfoEntity.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Value;
                //    double num = 0;
                //    char[] chars = null;
                //    switch (cd)
                //    {
                //        case 1:
                //        case 2:
                //            num = 9999999;
                //            chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
                //            break;
                //        case 3:
                //            num = 9999999.9;
                //            break;
                //        case 4:
                //            num = 9999999.99;
                //            break;
                //        case 5:
                //            num = 9999999.99;
                //            break;
                //        default:
                //            num = 9999999.99;
                //            break;
                //    }
                //    eCell.CharacterLimitList = chars;
                //    eCell.RangeSetting.Max = Convert.ToDecimal(num);
                //}

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 別表1排出一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu1HstCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];

                DataRowCollection list = this.hinmeiTable.Rows;
                if (e.CellName.Equals("HINMEI_CD") && this.form.listHinmei[e.RowIndex, "HINMEI_CD"].Value != null)
                {
                    if (!this.isError && this.form.listHinmei[e.RowIndex, "HINMEI_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    string hinmeiCd = this.form.listHinmei[e.RowIndex, "HINMEI_CD"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(hinmeiCd))
                    {
                        // koukouei 20150129 品目CDの重複登録を許可する start
                        //// 重複チェック
                        //for (int i = 0; i < list.Count; i++)
                        //{
                        //    if (i == e.RowIndex) continue;

                        //    if (hinmeiCd.Equals(list[i]["HINMEI_CD"].ToString()))
                        //    {
                        //        if (this.errorCancelFlg)
                        //        {
                        //            if (this.form.listHimei.EditingControl != null)
                        //            {
                        //                this.form.listHimei.EditingControl.Text = string.Empty;
                        //            }

                        //            if (this.errorCancelFlg)
                        //            {
                        //                msgLogic.MessageBoxShow("E003", "品名", hinmeiCd);
                        //                e.Cancel = true;
                        //            }
                        //        }
                        //        LogUtility.DebugMethodEnd(e);
                        //        return;
                        //    }
                        //}
                        // koukouei 20150129 品目CDの重複登録を許可する end

                        // マスタ存在チェック
                        M_HINMEI hinmei = new M_HINMEI();
                        hinmei.HINMEI_CD = hinmeiCd;
                        M_HINMEI[] hinmeis = this.imHinmeiDao.GetAllValidData(hinmei);
                        if (hinmeis.Length > 0)
                        {
                            if (!this.isError && hinmeiCd == this.form.PreviousCd)
                            {
                                this.form.listHinmei[e.RowIndex, "HINMEI_NAME"].Value = this.form.PreviousName;
                            }
                            else
                            {
                                this.form.listHinmei[e.RowIndex, "HINMEI_NAME"].Value = hinmeis[0].HINMEI_NAME;
                            }
                        }
                        else
                        {
                            // this.form.listHimei[e.RowIndex, "HINMEI_CD"].Value = string.Empty;
                            this.form.listHinmei[e.RowIndex, "HINMEI_NAME"].Value = string.Empty;

                            if (this.form.listHinmei.EditingControl != null)
                            {
                                var textBox = this.form.listHinmei.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", "品名");
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        this.form.listHinmei[e.RowIndex, "HINMEI_NAME"].Value = string.Empty;
                    }
                }
                // 運搬予定数量単位
                if (e.CellName.Equals("UNPAN_YOTEI_SUU_UNIT_CD") && this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_CD"].Value != null)
                {
                    if (!this.isError && this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    string unitCd = this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_CD"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(unitCd))
                    {
                        // マスタ存在チェック
                        M_UNIT unit = new M_UNIT();
                        unit.UNIT_CD = Convert.ToInt16(unitCd);
                        M_UNIT[] units = this.unitDao.GetAllValidData(unit);
                        if (units.Length > 0)
                        {
                            this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_NAME"].Value = units[0].UNIT_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_NAME"].Value = string.Empty;

                            if (this.form.listHinmei.EditingControl != null)
                            {
                                var textBox = this.form.listHinmei.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", "単位");
                                e.Cancel = true;
                            }

                            this.isError = true;
                        }
                    }
                    else
                    {
                        this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_NAME"].Value = string.Empty;
                    }
                }
                // 処分予定数量単位
                if (e.CellName.Equals("SHOBUN_YOTEI_SUU_UNIT_CD") && this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_CD"].Value != null)
                {
                    if (!this.isError && this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    string unitCd = this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_CD"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(unitCd))
                    {
                        // マスタ存在チェック
                        M_UNIT unit = new M_UNIT();
                        unit.UNIT_CD = Convert.ToInt16(unitCd);
                        M_UNIT[] units = this.unitDao.GetAllValidData(unit);
                        if (units.Length > 0)
                        {
                            this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_NAME"].Value = units[0].UNIT_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_NAME"].Value = string.Empty;

                            if (this.form.listHinmei.EditingControl != null)
                            {
                                var textBox = this.form.listHinmei.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", "単位");
                                e.Cancel = true;
                            }

                            this.isError = true;
                        }
                    }
                    else
                    {
                        this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_NAME"].Value = string.Empty;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                this.isError = true;
                e.Cancel = true;
                LogUtility.Error("ListBetsu1HstCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool ListBetsu1HstCellSwitchCdName(CellEventArgs e, ItakuKeiyakuHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.ItakuKeiyakuHoshuConstans.FocusSwitch.IN:
                    // 名称にフォーカス時実行
                    if (e.CellName.Equals("UNPAN_YOTEI_SUU_UNIT_NAME"))
                    {
                        this.form.listHinmei.CurrentCell = this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_CD"];
                        this.form.listHinmei[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    else if (e.CellName.Equals("SHOBUN_YOTEI_SUU_UNIT_NAME"))
                    {
                        this.form.listHinmei.CurrentCell = this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_CD"];
                        this.form.listHinmei[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.ItakuKeiyakuHoshuConstans.FocusSwitch.OUT:
                    // CDに検証成功後実行
                    if (e.CellName.Equals("UNPAN_YOTEI_SUU_UNIT_CD"))
                    {
                        this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_NAME"].Visible = true;
                        this.form.listHinmei[e.RowIndex, "UNPAN_YOTEI_SUU_UNIT_NAME"].UpdateBackColor(false);
                    }
                    else if (e.CellName.Equals("SHOBUN_YOTEI_SUU_UNIT_CD"))
                    {
                        this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_NAME"].Visible = true;
                        this.form.listHinmei[e.RowIndex, "SHOBUN_YOTEI_SUU_UNIT_NAME"].UpdateBackColor(false);
                    }
                    if (this.form.listHinmei.Rows[e.RowIndex].IsNewRow)
                    {
                        this.hinmeiNewRowFlg = true;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 委託契約 別表1予定一覧のセルエンターイベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu1YoteiCellEnter(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (this.form.listHoukokushoBunrui.Rows[e.RowIndex].IsNewRow)
                {
                    this.houkokuNewRowFlg = true;
                }

                /*if (e.CellName.Equals("YOTEI_HAISHUTSU_JIGYOUJOU_CD"))
                {
                    // 基本情報の排出現場一覧から現場CDの検索条件を生成する
                    List<string> whereValue = new List<string>();
                    for (int i = 0; i < this.form.listKihonHstGenba.Rows.Count; i++)
                    {
                        if (this.form.listKihonHstGenba.Rows[i].IsNewRow) continue;
                        if (this.form.listKihonHstGenba[i, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value == null) continue;
                        whereValue.Add(this.form.listKihonHstGenba[i, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString());
                    }

                    // 生成した検索条件をポップアップの条件に差し替える
                    if (((GcCustomAlphaNumTextBoxCell)this.form.listHoukokushoBunrui[e.RowIndex, e.CellIndex]).PopupSearchSendParams != null)
                    {
                        ((GcCustomAlphaNumTextBoxCell)this.form.listHoukokushoBunrui[e.RowIndex, e.CellIndex]).PopupSearchSendParams[1].Value = string.Join(",", whereValue.ToArray());
                    }
                }
                if (e.CellName.Equals("HINMEI_CD"))
                {
                    // 別表1(排出)情報の報告書分類一覧から報告書分類CDの検索条件を生成する
                    List<string> whereValue = new List<string>();
                    for (int i = 0; i < this.form.listHimei.Rows.Count; i++)
                    {
                        if (this.form.listHimei.Rows[i].IsNewRow) continue;
                        if (this.form.listHimei[i, "HINMEI_CD"].Value == null) continue;
                        whereValue.Add(this.form.listHimei[i, "HINMEI_CD"].Value.ToString());
                    }

                    // 生成した検索条件をポップアップの条件に差し替える
                    if (((GcCustomAlphaNumTextBoxCell)this.form.listHoukokushoBunrui[e.RowIndex, e.CellIndex]).popupWindowSetting != null && ((GcCustomAlphaNumTextBoxCell)this.form.listHoukokushoBunrui[e.RowIndex, e.CellIndex]).popupWindowSetting[0].SearchCondition != null)
                    {
                        ((GcCustomAlphaNumTextBoxCell)this.form.listHoukokushoBunrui[e.RowIndex, e.CellIndex]).popupWindowSetting[0].SearchCondition[0].Value = string.Join(",", whereValue.ToArray());
                    }
                }*/

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListBetsu1YoteiCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 別表1予定一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu1YoteiCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (this.form.listHoukokushoBunrui.Rows[e.RowIndex].IsNewRow)
                {
                    this.houkokuNewRowFlg = true;
                }

                #region
                /*if (!this.IsYoteiTabSelect)
            {
                return;
            }

            GcCustomMultiRow list = this.form.listHoukokushoBunrui;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (e.CellName.Equals("YOTEI_HAISHUTSU_JIGYOUJOU_CD") && this.betsu1YoteiTable.Rows.Count > 0 && list[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value != null)
            {
                if (!this.isError && list[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString().Equals(this.form.PreviousValue))
                {
                    return;
                }
                this.isError = false;

                if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()))
                {
                    // 重複チェック
                    if (!this.CheckYoteiDuplicate(list[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value, list[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_CD"].Value, e.RowIndex))
                    {
                        if (list.EditingControl != null)
                        {
                            list.EditingControl.Text = string.Empty;
                        }

                        if (this.errorCancelFlg)
                        {
                            e.Cancel = true;
                        }

                        LogUtility.DebugMethodEnd(e);
                        return;
                    }

                    // 排出現場の存在チェック
                    bool isExists = false;
                    for (int i = 0; i < this.form.listKihonHstGenba.Rows.Count; i++)
                    {
                        if (this.form.listKihonHstGenba.Rows[i].IsNewRow) continue;

                        if (list[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString().Equals(this.form.listKihonHstGenba[i, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString()))
                        {
                            // 名称設定
                            this.form.listHoukokushoBunrui[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_NAME"].Value = this.form.listKihonHstGenba[i, "GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value.ToString();
                            this.form.listHoukokushoBunrui[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value = this.form.listKihonHstGenba[i, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"].Value.ToString();
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)
                    {
                        if (this.errorCancelFlg)
                        {
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E028");
                                e.Cancel = true;
                            }
                        }
                        this.isError = true;
                        LogUtility.DebugMethodEnd(e);
                        return;
                    }
                }
                else
                {
                    this.form.listHoukokushoBunrui[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_NAME"].Value = string.Empty;
                    this.form.listHoukokushoBunrui[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS"].Value = string.Empty;
                }
            }
            if (e.CellName.Equals("YOTEI_HOUKOKUSHO_BUNRUI_CD") && this.betsu1YoteiTable.Rows.Count > 0 && list[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_CD"].Value != null)
            {
                if (!this.isError && list[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_CD"].Value.ToString().Equals(this.form.PreviousValue))
                {
                    return;
                }
                this.isError = false;

                if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_CD"].Value.ToString()))
                {
                    // 重複チェック
                    if (!this.CheckYoteiDuplicate(list[e.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value, list[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_CD"].Value, e.RowIndex))
                    {
                        if (this.errorCancelFlg)
                        {
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }

                            if (this.errorCancelFlg)
                            {
                                e.Cancel = true;
                            }
                        }
                        LogUtility.DebugMethodEnd(e);
                        return;
                    }

                    // 別表1(排出)の存在チェック
                    bool isExists = false;
                    for (int i = 0; i < this.form.listHimei.Rows.Count; i++)
                    {
                        if (this.form.listHimei.Rows[i].IsNewRow) continue;

                        if (list[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_CD"].Value.ToString().Equals(this.form.listHimei[i, "HOUKOKUSHO_BUNRUI_CD"].Value.ToString()))
                        {
                            // 名称設定
                            this.form.listHoukokushoBunrui[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_NAME_RYAKU"].Value = this.form.listHimei[i, "HOUKOKUSHO_BUNRUI_NAME_RYAKU"].Value.ToString();
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)
                    {
                        if (this.errorCancelFlg)
                        {
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E028");
                                e.Cancel = true;
                            }
                        }
                        this.isError = true;
                        LogUtility.DebugMethodEnd(e);
                        return;
                    }
                }
                else
                {
                    this.form.listHoukokushoBunrui[e.RowIndex, "YOTEI_HOUKOKUSHO_BUNRUI_NAME_RYAKU"].Value = string.Empty;
                }
            }*/
                #endregion

                GcCustomMultiRow list = this.form.listHoukokushoBunrui;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string houkokuCd = Convert.ToString(this.form.listHoukokushoBunrui[e.RowIndex, "HOUKOKUSHO_BUNRUI_CD"].Value);
                string key = string.Empty;
                if (e.CellName.Equals("HOUKOKUSHO_BUNRUI_CD") && !string.IsNullOrEmpty(houkokuCd))
                {
                    if (!this.isError && houkokuCd.Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    for (int i = 0; i < e.RowIndex; i++)
                    {
                        Row dr = this.form.listHoukokushoBunrui.Rows[i];
                        key = Convert.ToString(dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value);
                        if (string.IsNullOrWhiteSpace(key))
                        {
                            continue;
                        }

                        if (houkokuCd == key)
                        {
                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E031", "報告書分類CD");
                                e.Cancel = true;
                            }
                            this.isError = true;
                            return false;
                        }
                    }

                    // マスタ存在チェック
                    M_HOUKOKUSHO_BUNRUI houkoku = new M_HOUKOKUSHO_BUNRUI();
                    houkoku.HOUKOKUSHO_BUNRUI_CD = houkokuCd;
                    M_HOUKOKUSHO_BUNRUI[] houkokus = this.houokushoBunruiDao.GetAllValidData(houkoku);
                    if (houkokus.Length > 0)
                    {
                        list[e.RowIndex, "HOUKOKUSHO_BUNRUI_NAME"].Value = houkokus[0].HOUKOKUSHO_BUNRUI_NAME_RYAKU;
                    }
                    else
                    {
                        list[e.RowIndex, "HOUKOKUSHO_BUNRUI_NAME"].Value = string.Empty;

                        if (list.EditingControl != null)
                        {
                            var textBox = list.EditingControl as TextBox;
                            if (textBox != null)
                            {
                                textBox.SelectAll();
                            }
                        }

                        if (this.errorCancelFlg)
                        {
                            msgLogic.MessageBoxShow("E020", "報告書分類");
                            e.Cancel = true;
                        }

                        this.isError = true;
                    }
                }
                else if (e.CellName.Equals("HOUKOKUSHO_BUNRUI_CD") && string.IsNullOrEmpty(houkokuCd))
                {
                    list[e.RowIndex, "HOUKOKUSHO_BUNRUI_NAME"].Value = string.Empty;
                }
                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("ListBetsu1YoteiCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 排出事業場、報告書分類の組み合わせでの重複チェック
        /// </summary>
        /// <param name="jigyoujou"></param>
        /// <param name="houkokuBunrui"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool CheckYoteiDuplicate(object jigyoujou, object houkokuBunrui, int rowIndex)
        {
            LogUtility.DebugMethodStart(jigyoujou, houkokuBunrui, rowIndex);
            GcCustomMultiRow list = this.form.listHoukokushoBunrui;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string[] param = new string[1];
            bool ret = true;

            // 入力値のどちらかが未入力の場合、処理しない
            if (houkokuBunrui == null || string.IsNullOrWhiteSpace(houkokuBunrui.ToString()))
            {
                LogUtility.DebugMethodEnd(jigyoujou, houkokuBunrui, rowIndex);
                return ret;
            }

            // 重複チェック
            for (int i = 0; i < list.Rows.Count; i++)
            {
                if (list.Rows[i].IsNewRow) continue;
                if (i == rowIndex) continue;

                if (houkokuBunrui.ToString().Equals(list[i, "HOUKOKUSHO_BUNRUI_CD"].Value.ToString()))
                {
                    msgLogic.MessageBoxShow("E037", ItakuKeiyakuHoshu.Properties.Resources.HOUKOKUSHO_BUNRUI);
                    ret = false;
                    break;
                }
            }

            LogUtility.DebugMethodEnd(jigyoujou, houkokuBunrui, rowIndex);
            return ret;
        }

        /// <summary>
        /// 委託契約 別表2一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu2CellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                DataRowCollection list = this.betsu2Table.Rows;

                if (e.CellName.Equals("UNPAN_GYOUSHA_CD") && this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value != null)
                {
                    // 2014.12.16 佐藤
                    // アラートE216を表示する部分を移動
                    //if (list.Count > 0 && this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" && e.RowIndex > 0)
                    //{
                    //    msgLogic.MessageBoxShow("E216");
                    //    e.Cancel = true;
                    //    LogUtility.DebugMethodEnd(e);
                    //    return;
                    //}

                    if (!this.isError && this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    string cdGyousha = this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(cdGyousha))
                    {
                        // 重複チェック
                        //for (int i = 0; i < list.Count; i++)
                        //{
                        //    if (i == e.RowIndex) continue;

                        //    if (cdGyousha.Equals(list[i]["UNPAN_GYOUSHA_CD"].ToString()))
                        //    {
                        //        this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                        //        this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                        //        this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                        //        this.form.listBetsu2[e.RowIndex, "UNPAN_TODOUFUKEN_NAME"].Value = string.Empty;

                        //        if (this.errorCancelFlg)
                        //        {
                        //            if (this.form.listBetsu2.EditingControl != null)
                        //            {
                        //                var textBox = this.form.listBetsu2.EditingControl as TextBox;
                        //                if (textBox != null)
                        //                {
                        //                    textBox.SelectAll();
                        //                }
                        //            }

                        //            if (this.errorCancelFlg)
                        //            {
                        //                msgLogic.MessageBoxShow("E003", ItakuKeiyakuHoshu.Properties.Resources.UNPAN_GYOUSHA, cdGyousha);
                        //                e.Cancel = true;
                        //            }
                        //        }
                        //        this.isError = true;
                        //        LogUtility.DebugMethodEnd(e, false);
                        //        return false;
                        //    }
                        //}

                        // マスタ存在チェック
                        M_GYOUSHA condGyousha = new M_GYOUSHA();
                        condGyousha.GYOUSHA_CD = cdGyousha;
                        M_GYOUSHA[] aryGyousha = this.gyoushaDao.GetAllValidData(condGyousha);
                        // 20151021 BUNN #12040 STR
                        if (aryGyousha != null && aryGyousha.Length > 0 && aryGyousha[0].UNPAN_JUTAKUSHA_KAISHA_KBN)
                        // 20151021 BUNN #12040 END
                        {
                            this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = aryGyousha[0].GYOUSHA_NAME_RYAKU;
                            this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"].Value = aryGyousha[0].GYOUSHA_ADDRESS1;
                            this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"].Value = aryGyousha[0].GYOUSHA_ADDRESS2;
                            M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                            if (!aryGyousha[0].GYOUSHA_TODOUFUKEN_CD.IsNull)
                            {
                                todoufuken = this.todoufukenDao.GetDataByCd(aryGyousha[0].GYOUSHA_TODOUFUKEN_CD.ToString());
                            }
                            if (todoufuken != null)
                            {
                                this.form.listBetsu2[e.RowIndex, "UNPAN_TODOUFUKEN_NAME"].Value = todoufuken.TODOUFUKEN_NAME_RYAKU;
                            }
                        }
                        else
                        {
                            this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                            this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                            this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                            this.form.listBetsu2[e.RowIndex, "UNPAN_TODOUFUKEN_NAME"].Value = string.Empty;

                            if (this.form.listBetsu2.EditingControl != null)
                            {
                                var textBox = this.form.listBetsu2.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.GYOUSHA);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                        this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                        this.form.listBetsu2[e.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                        this.form.listBetsu2[e.RowIndex, "UNPAN_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                }

                if (this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" && !this.isError && e.RowIndex == this.form.listBetsu2.Rows.Count - 1)
                {
                    Row dr1 = this.form.listBetsu2.Rows[e.RowIndex];
                    if (!string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_CD"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_TODOUFUKEN_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_ADDRESS1"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr1["UNPAN_GYOUSHA_ADDRESS2"].Value)))
                    {
                        DataTable dt = ((DataTable)this.form.listBetsu2.DataSource);
                        dt.Rows.Add();
                        this.form.listBetsu2.DataSource = dt;
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("ListBetsu2CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 積替一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListTsumikaeCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                GcCustomMultiRow list = this.form.listTsumikae;

                if (list.Rows[e.RowIndex].IsNewRow)
                {
                    this.tsumikaeNewRowFlg = true;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //string[] param = new string[1];
                //param[0] = ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA;

                if (e.CellName.Equals("UNPAN_GYOUSHA_CD") && list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // 重複チェック
                        //if (!this.CheckTsumikaeDuplicate(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value, list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value, e.RowIndex))
                        //{
                        //    this.isError = true;

                        //    if (list.EditingControl != null)
                        //    {
                        //        var textBox = list.EditingControl as TextBox;
                        //        if (textBox != null)
                        //        {
                        //            textBox.SelectAll();
                        //        }
                        //    }

                        //    if (this.errorCancelFlg)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    LogUtility.DebugMethodEnd(e, false);
                        //    return false;
                        //}

                        // マスタ存在チェック
                        M_GYOUSHA condGyousha = new M_GYOUSHA();
                        condGyousha.GYOUSHA_CD = list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString();
                        // 20151021 BUNN #12040 STR
                        condGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                        // 20151021 BUNN #12040 END
                        M_GYOUSHA[] aryGyousha = this.gyoushaDao.GetAllValidData(condGyousha);
                        if (aryGyousha != null && aryGyousha.Length > 0)
                        {
                            list[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = aryGyousha[0].GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            list[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.GYOUSHA);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        list[e.RowIndex, "UNPAN_GYOUSHA_NAME"].Value = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.PreviousValue))
                    {
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_NAME"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                }
                if (e.CellName.Equals("TSUMIKAE_HOKANBA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString()))
                    {
                        // 処分事業場CDが入力されている状態で、処分業者CDがクリアされていた場合、エラーとする
                        if (list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString()))
                        {
                            if (this.errorCancelFlg)
                            {
                                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                                msgLogic.MessageBoxShow("E051", "運搬業者");
                                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                                e.Cancel = true;
                            }
                            this.isError = true;

                            list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value = string.Empty;
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }
                            LogUtility.DebugMethodEnd(e, false);
                            return false;
                        }

                        // 重複チェック
                        //if (!this.CheckTsumikaeDuplicate(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value, list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value, e.RowIndex))
                        //{
                        //    this.isError = true;

                        //    if (list.EditingControl != null)
                        //    {
                        //        var textBox = list.EditingControl as TextBox;
                        //        if (textBox != null)
                        //        {
                        //            textBox.SelectAll();
                        //        }
                        //    }

                        //    if (this.errorCancelFlg)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    LogUtility.DebugMethodEnd(e, false);
                        //    return false;
                        //}

                        if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString()) && !string.IsNullOrWhiteSpace(this.form.listTsumikae[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString()))
                        {
                            // マスタ存在チェック
                            M_GENBA genba = new M_GENBA();
                            M_GYOUSHA gyousha = new M_GYOUSHA();
                            genba.GYOUSHA_CD = list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString();
                            genba.GENBA_CD = list[e.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString();
                            genba.TSUMIKAEHOKAN_KBN = true;
                            DataTable dt = this.genbaDao.GetDataBySqlFileWithGyousha(CHECK_GENBA_DATA_SQL, genba, gyousha);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_NAME"].Value = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"].Value = dt.Rows[0]["GENBA_ADDRESS1"].ToString();
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"].Value = dt.Rows[0]["GENBA_ADDRESS2"].ToString();
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = dt.Rows[0]["TODOUFUKEN_NAME"].ToString();
                            }
                            else
                            {
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_NAME"].Value = string.Empty;
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"].Value = string.Empty;
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"].Value = string.Empty;
                                list[e.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = string.Empty;

                                if (list.EditingControl != null)
                                {
                                    var textBox = list.EditingControl as TextBox;
                                    if (textBox != null)
                                    {
                                        textBox.SelectAll();
                                    }
                                }

                                if (this.errorCancelFlg)
                                {
                                    msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.M_GENBA);
                                    e.Cancel = true;
                                }
                                this.isError = true;
                            }
                        }
                    }
                    else
                    {
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_NAME"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"].Value = string.Empty;
                        list[e.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                }

                // 保管上限単位
                if (e.CellName.Equals("HOKAN_JOGEN_UNIT_CD") && list[e.RowIndex, "HOKAN_JOGEN_UNIT_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "HOKAN_JOGEN_UNIT_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    string unitCd = list[e.RowIndex, "HOKAN_JOGEN_UNIT_CD"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(unitCd))
                    {
                        // マスタ存在チェック
                        M_UNIT unit = new M_UNIT();
                        unit.UNIT_CD = Convert.ToInt16(unitCd);
                        M_UNIT[] units = this.unitDao.GetAllValidData(unit);
                        if (units.Length > 0)
                        {
                            list[e.RowIndex, "HOKAN_JOGEN_UNIT_NAME"].Value = units[0].UNIT_NAME_RYAKU;
                        }
                        else
                        {
                            list[e.RowIndex, "HOKAN_JOGEN_UNIT_NAME"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", "単位");
                                e.Cancel = true;
                            }

                            this.isError = true;
                        }
                    }
                    else
                    {
                        list[e.RowIndex, "HOKAN_JOGEN_UNIT_NAME"].Value = string.Empty;
                    }
                }

                //foreach (DataColumn col in this.tsumikaeTable.Columns)
                //{
                //    col.ReadOnly = false;
                //}

                if (e.CellName.Equals("KONGOU"))
                {
                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        list[e.RowIndex, "KONGOU_NAME"].Value = this.GetKongouName(short.Parse(e.FormattedValue.ToString()));
                    }
                    else
                    {
                        list[e.RowIndex, "KONGOU_NAME"].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("ListTsumikaeCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool ListTsumikaeCellSwitchCdName(CellEventArgs e, ItakuKeiyakuHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.ItakuKeiyakuHoshuConstans.FocusSwitch.IN:
                    // 名称にフォーカス時実行
                    if (e.CellName.Equals("HOKAN_JOGEN_UNIT_NAME"))
                    {
                        this.form.listTsumikae.CurrentCell = this.form.listTsumikae[e.RowIndex, "HOKAN_JOGEN_UNIT_CD"];
                        this.form.listTsumikae[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.ItakuKeiyakuHoshuConstans.FocusSwitch.OUT:
                    // CDに検証成功後実行
                    if (e.CellName.Equals("HOKAN_JOGEN_UNIT_CD"))
                    {
                        this.form.listTsumikae[e.RowIndex, "HOKAN_JOGEN_UNIT_NAME"].Visible = true;
                        this.form.listTsumikae[e.RowIndex, "HOKAN_JOGEN_UNIT_NAME"].UpdateBackColor(false);
                    }
                    if (this.form.listTsumikae.Rows[e.RowIndex].IsNewRow)
                    {
                        this.tsumikaeNewRowFlg = true;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 委託契約 別表3一覧のセル編集開始イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu3CellBeginEdit(CellBeginEditEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];
                param[0] = ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA;

                if (e.CellName.Equals("SHOBUN_JIGYOUJOU_CD") && this.form.listBetsu3.Rows.Count > 0 && this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value != null && !string.IsNullOrWhiteSpace(this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                {
                    if (this.form.listBetsu3[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value == null)
                    {
                        msgLogic.MessageBoxShow("E027", param);
                    }
                    else if (this.form.listBetsu3[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty))
                    {
                        msgLogic.MessageBoxShow("E027", param);
                    }
                }
                if (e.CellName.Equals("SHOBUN_GYOUSHA_CD") && (this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" || this.form.ITAKU_KEIYAKU_SHURUI.Text == "3"))
                {
                    for (int i = 1; i < this.form.listBetsu3.Rows.Count; i++)
                    {
                        this.form.listBetsu3[i, "SHOBUN_GYOUSHA_CD"].ReadOnly = true;
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("ListBetsu3CellBeginEdit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 別表3一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu3CellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                GcCustomMultiRow list = this.form.listBetsu3;

                if (list.Rows[e.RowIndex].IsNewRow)
                {
                    this.sbnNewRowFlg = true;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value != null)
                {
                    // 契約種類が「処分」、「収集運搬処分」の場合は、処分タブに同じ処分受託者は入力を不可にする
                    if (list.Rows.Count > 1 && (this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" || this.form.ITAKU_KEIYAKU_SHURUI.Text == "3") && e.RowIndex > 0)
                    {
                        bool result = true;
                        for (int i = 0; i < list.Rows.Count; i++)
                        {
                            if (list.Rows[i].IsNewRow) continue;
                            if (i == e.RowIndex) continue;
                            if (list[i, "SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty)) continue;

                            if (!list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty) && !list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(list[i, "SHOBUN_GYOUSHA_CD"].Value.ToString()))
                            {
                                msgLogic.MessageBoxShow("E215");
                                list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value = string.Empty;
                                this.CheckShobunGyoushaCD();
                                result = false;
                                break;
                            }
                        }

                        if (!result)
                        {
                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                e.Cancel = true;
                            }
                            LogUtility.DebugMethodEnd(e, false);
                            return false;
                        }
                    }
                }

                string[] param = new string[1];
                param[0] = ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA;
                bool catchErr = false;

                if (e.CellName.Equals("SHOBUN_GYOUSHA_CD") && list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // 重複チェック
                        //if (!this.CheckBetsu3Duplicate(list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value, list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value, list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value, e.RowIndex, out catchErr))
                        //{
                        //    if (catchErr)
                        //    {
                        //        e.Cancel = true;
                        //        this.isError = true;
                        //        return true;
                        //    }
                        //    this.isError = true;

                        //    if (list.EditingControl != null)
                        //    {
                        //        var textBox = list.EditingControl as TextBox;
                        //        if (textBox != null)
                        //        {
                        //            textBox.SelectAll();
                        //        }
                        //    }

                        //    if (this.errorCancelFlg)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    LogUtility.DebugMethodEnd(e, false);
                        //    return false;
                        //}

                        // マスタ存在チェック
                        M_GYOUSHA condGyousha = new M_GYOUSHA();
                        condGyousha.GYOUSHA_CD = list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString();
                        // 20151021 BUNN #12040 STR
                        condGyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                        // 20151021 BUNN #12040 END
                        M_GYOUSHA[] aryGyousha = this.gyoushaDao.GetAllValidData(condGyousha);
                        if (aryGyousha != null && aryGyousha.Length > 0)
                        {
                            list[e.RowIndex, "SHOBUN_GYOUSHA_NAME"].Value = aryGyousha[0].GYOUSHA_NAME_RYAKU;
                            list[e.RowIndex, "SHOBUN_GYOUSHA_ADDRESS1"].Value = aryGyousha[0].GYOUSHA_ADDRESS1;
                            list[e.RowIndex, "SHOBUN_GYOUSHA_ADDRESS2"].Value = aryGyousha[0].GYOUSHA_ADDRESS2;
                            M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                            if (!aryGyousha[0].GYOUSHA_TODOUFUKEN_CD.IsNull)
                            {
                                todoufuken = this.todoufukenDao.GetDataByCd(aryGyousha[0].GYOUSHA_TODOUFUKEN_CD.ToString());
                            }
                            if (todoufuken != null)
                            {
                                list[e.RowIndex, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = todoufuken.TODOUFUKEN_NAME_RYAKU;
                            }
                        }
                        else
                        {
                            list[e.RowIndex, "SHOBUN_GYOUSHA_NAME"].Value = string.Empty;
                            list[e.RowIndex, "SHOBUN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                            list[e.RowIndex, "SHOBUN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                            list[e.RowIndex, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.GYOUSHA);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        list[e.RowIndex, "SHOBUN_GYOUSHA_NAME"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = string.Empty;
                    }

                    if (!string.IsNullOrWhiteSpace(this.form.PreviousValue))
                    {
                        list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        list[e.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                    if (e.RowIndex == 0 && !this.isError)
                    {
                        for (int i = 1; i < list.Rows.Count; i++)
                        {
                            if (Convert.ToString(list[i, "SHOBUN_GYOUSHA_CD"].Value) != Convert.ToString(list[0, "SHOBUN_GYOUSHA_CD"].Value))
                            {
                                list[i, "SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                                list[i, "SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                                list[i, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                                list[i, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                                list[i, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                            }
                        }
                    }
                }
                if (e.CellName.Equals("SHOBUN_JIGYOUJOU_CD") && list.Rows.Count > 0 && list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                    {
                        // 処分事業場CDが入力されている状態で、処分業者CDがクリアされていた場合、エラーとする
                        if (list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString()))
                        {
                            if (this.errorCancelFlg)
                            {
                                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                                msgLogic.MessageBoxShow("E051", "処分受託者");
                                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                                e.Cancel = true;
                            }
                            list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }
                            LogUtility.DebugMethodEnd(e, false);
                            return false;
                        }

                        // 重複チェック
                        //if (!this.CheckBetsu3Duplicate(list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value, list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value, list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value, e.RowIndex, out catchErr))
                        //{
                        //    if (catchErr)
                        //    {
                        //        e.Cancel = true;
                        //        this.isError = true;
                        //        return true;
                        //    }

                        //    this.isError = true;

                        //    if (list.EditingControl != null)
                        //    {
                        //        var textBox = list.EditingControl as TextBox;
                        //        if (textBox != null)
                        //        {
                        //            textBox.SelectAll();
                        //        }
                        //    }

                        //    if (this.errorCancelFlg)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    LogUtility.DebugMethodEnd(e, false);
                        //    return false;
                        //}

                        if (!string.IsNullOrWhiteSpace(this.form.listBetsu3[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString()) && !string.IsNullOrWhiteSpace(this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                        {
                            // マスタ存在チェック
                            M_GENBA genba = new M_GENBA();
                            M_GYOUSHA gyousha = new M_GYOUSHA();
                            genba.GYOUSHA_CD = this.form.listBetsu3[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString();
                            genba.GENBA_CD = this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString();
                            // 20151021 BUNN #12040 STR
                            genba.SHOBUN_NIOROSHI_GENBA_KBN = true;
                            gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                            // 20151021 BUNN #12040 END
                            DataTable dt = this.genbaDao.GetDataBySqlFileWithGyousha(CHECK_GENBA_DATA_SQL, genba, gyousha);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_NAME"].Value = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = dt.Rows[0]["GENBA_ADDRESS1"].ToString();
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = dt.Rows[0]["GENBA_ADDRESS2"].ToString();
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = dt.Rows[0]["TODOUFUKEN_NAME"].ToString();
                            }
                            else
                            {
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                                this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;

                                if (list.EditingControl != null)
                                {
                                    var textBox = list.EditingControl as TextBox;
                                    if (textBox != null)
                                    {
                                        textBox.SelectAll();
                                    }
                                }

                                if (this.errorCancelFlg)
                                {
                                    msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.M_GENBA);
                                    e.Cancel = true;
                                }
                                this.isError = true;
                            }
                        }
                    }
                    else
                    {
                        this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                        this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        this.form.listBetsu3[e.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                }
                /*if (e.CellName.Equals("UNPAN_FROM"))
                {
                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        this.form.listBetsu3[e.RowIndex, "UNPAN_FROM_NAME"].Value = this.GetUnpanFromName(short.Parse(e.FormattedValue.ToString()));
                    }
                    else
                    {
                        this.form.listBetsu3[e.RowIndex, "UNPAN_FROM_NAME"].Value = string.Empty;
                    }
                }
                if (e.CellName.Equals("UNPAN_END"))
                {
                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        if (e.FormattedValue.ToString().Equals("2"))
                        {
                            this.form.listBetsu3[e.RowIndex, "UNPAN_END_NAME"].Value = this.GetUnpanEndName(short.Parse("1"));
                        }
                        else if (e.FormattedValue.ToString().Equals("3"))
                        {
                            this.form.listBetsu3[e.RowIndex, "UNPAN_END_NAME"].Value = this.GetUnpanEndName(short.Parse("2"));
                        }
                    }
                    else
                    {
                        this.form.listBetsu3[e.RowIndex, "UNPAN_END_NAME"].Value = string.Empty;
                    }
                }*/
                string houhouCd = Convert.ToString(list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value);
                if (e.CellName.Equals("SHOBUN_HOUHOU_CD"))
                {
                    // 重複チェック
                    //if (!this.CheckBetsu3Duplicate(list[e.RowIndex, "SHOBUN_GYOUSHA_CD"].Value, list[e.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value, list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value, e.RowIndex, out catchErr))
                    //{
                    //    if (catchErr)
                    //    {
                    //        e.Cancel = true;
                    //        this.isError = true;
                    //        return true;
                    //    }

                    //    if (list.EditingControl != null)
                    //    {
                    //        var textBox = list.EditingControl as TextBox;
                    //        if (textBox != null)
                    //        {
                    //            textBox.SelectAll();
                    //        }
                    //    }

                    //    if (this.errorCancelFlg)
                    //    {
                    //        e.Cancel = true;
                    //    }
                    //    LogUtility.DebugMethodEnd(e, false);
                    //    return false;
                    //}

                    if (!string.IsNullOrWhiteSpace(houhouCd))
                    {
                        // マスタ存在チェック
                        M_SHOBUN_HOUHOU houhou = new M_SHOBUN_HOUHOU();
                        houhou.SHOBUN_HOUHOU_CD = houhouCd;
                        M_SHOBUN_HOUHOU[] houhous = this.shobunHouhouDao.GetAllValidData(houhou);
                        if (houhous != null && houhous.Length > 0)
                        {
                            list[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = houhous[0].SHOBUN_HOUHOU_NAME_RYAKU;
                        }
                        else
                        {
                            list[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_HOUHOU);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        list[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = string.Empty;
                    }
                }

                if ((this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" || this.form.ITAKU_KEIYAKU_SHURUI.Text == "3") &&
                    !this.isError && e.RowIndex == list.Rows.Count - 1)
                {
                    Row dr = list.Rows[e.RowIndex];
                    if ((!string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_CD"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS1"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS2"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_HOUHOU_CD"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_HOUHOU_NAME_RYAKU"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHISETSU_CAPACITY"].Value))) ||
                        (list.Rows.Count == 1 && (!string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_GYOUSHA_CD"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_GYOUSHA_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_GYOUSHA_ADDRESS1"].Value)) ||
                        !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_GYOUSHA_ADDRESS2"].Value)))))
                    {
                        this.betsu3Table.Rows.Add();
                        this.form.listBetsu3.DataSource = this.betsu3Table;
                    }
                }

                if ((this.form.ITAKU_KEIYAKU_SHURUI.Text == "2" || this.form.ITAKU_KEIYAKU_SHURUI.Text == "3") && !this.isError)
                {
                    for (int i = 1; i < list.Rows.Count; i++)
                    {
                        list[i, "SHOBUN_GYOUSHA_CD"].ReadOnly = true;
                        list[i, "SHOBUN_GYOUSHA_CD"].Value = list[0, "SHOBUN_GYOUSHA_CD"].Value;
                        list[i, "SHOBUN_GYOUSHA_NAME"].Value = list[0, "SHOBUN_GYOUSHA_NAME"].Value;
                        list[i, "SHOBUN_GYOUSHA_ADDRESS1"].Value = list[0, "SHOBUN_GYOUSHA_ADDRESS1"].Value;
                        list[i, "SHOBUN_GYOUSHA_ADDRESS2"].Value = list[0, "SHOBUN_GYOUSHA_ADDRESS2"].Value;
                        list[i, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = list[0, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value;
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("ListBetsu3CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 積替事業者、積替事業場の組み合わせでの重複チェック
        /// </summary>
        /// <param name="jigyousha"></param>
        /// <param name="jigyoujou"></param>
        /// <param name="shobunHouhou"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool CheckTsumikaeDuplicate(object jigyousha, object jigyoujou, int rowIndex)
        {
            LogUtility.DebugMethodStart(jigyousha, jigyoujou, rowIndex);
            GcCustomMultiRow list = this.form.listTsumikae;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string[] param = new string[1];
            bool ret = true;

            // 入力値のどちらかが未入力の場合、処理しない
            if (jigyousha == null || jigyoujou == null
                || string.IsNullOrWhiteSpace(jigyousha.ToString())
                || string.IsNullOrWhiteSpace(jigyoujou.ToString()))
            {
                LogUtility.DebugMethodEnd(jigyousha, jigyoujou, rowIndex);
                return ret;
            }

            // 重複チェック
            for (int i = 0; i < list.Rows.Count; i++)
            {
                if (list.Rows[i].IsNewRow) continue;
                if (i == rowIndex) continue;

                if (jigyousha.ToString().Equals(list[i, "UNPAN_GYOUSHA_CD"].Value.ToString())
                    && jigyoujou.ToString().Equals(list[i, "TSUMIKAE_HOKANBA_CD"].Value.ToString()))
                {
                    msgLogic.MessageBoxShow("E031", "運搬業者CD、積替保管場所CD");
                    ret = false;
                    break;
                }
            }

            LogUtility.DebugMethodEnd(jigyousha, jigyoujou, rowIndex);
            return ret;
        }

        /// <summary>
        /// 処分事業者、処分事業場、処分方法の組み合わせでの重複チェック
        /// </summary>
        /// <param name="jigyousha"></param>
        /// <param name="jigyoujou"></param>
        /// <param name="shobunHouhou"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool CheckBetsu3Duplicate(object jigyousha, object jigyoujou, object shobunHouhou, int rowIndex, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(jigyousha, jigyoujou, shobunHouhou, rowIndex);
                GcCustomMultiRow list = this.form.listBetsu3;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];
                bool ret = true;
                catchErr = false;

                // 入力値のどちらかが未入力の場合、処理しない
                if (jigyousha == null || jigyoujou == null || shobunHouhou == null
                    || string.IsNullOrWhiteSpace(jigyousha.ToString())
                    || string.IsNullOrWhiteSpace(jigyoujou.ToString())
                    || string.IsNullOrEmpty(shobunHouhou.ToString()))
                {
                    LogUtility.DebugMethodEnd(jigyousha, jigyoujou, shobunHouhou, rowIndex);
                    return ret;
                }

                // 重複チェック
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i].IsNewRow) continue;
                    if (i == rowIndex) continue;

                    if (jigyousha.ToString().Equals(list[i, "SHOBUN_GYOUSHA_CD"].Value.ToString())
                        && jigyoujou.ToString().Equals(list[i, "SHOBUN_JIGYOUJOU_CD"].Value.ToString())
                        && shobunHouhou.ToString().Equals(list[i, "SHOBUN_HOUHOU_CD"].Value.ToString()))
                    {
                        msgLogic.MessageBoxShow("E037", ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA + "," + ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_JIGYOUJOU
                            + "," + ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_HOUHOU);
                        ret = false;
                        break;
                    }
                }

                LogUtility.DebugMethodEnd(jigyousha, jigyoujou, shobunHouhou, rowIndex, catchErr);
                return ret;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckBetsu3Duplicate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(jigyousha, jigyoujou, shobunHouhou, rowIndex, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 委託契約 別表4一覧のセル編集開始イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu4CellBeginEdit(CellBeginEditEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];
                param[0] = ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA;

                if (e.CellName.Equals("LAST_SHOBUN_JIGYOUJOU_CD") && this.form.listBetsu4.Rows.Count > 0 && this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value != null && !string.IsNullOrWhiteSpace(this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                {
                    if (this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value == null)
                    {
                        msgLogic.MessageBoxShow("E027", param);
                    }
                    else if (this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty))
                    {
                        msgLogic.MessageBoxShow("E027", param);
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListBetsu4CellBeginEdit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 別表4一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListBetsu4CellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                GcCustomMultiRow list = this.form.listBetsu4;

                if (list.Rows[e.RowIndex].IsNewRow)
                {
                    this.lastNewRowFlg = true;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];
                param[0] = ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA;
                bool catchErr = false;

                if (e.CellName.Equals("LAST_SHOBUN_GYOUSHA_CD") && list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // 重複チェック
                        //if (!this.CheckBetsu4Duplicate(list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value, list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value, list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value, e.RowIndex, out catchErr))
                        //{
                        //    if (catchErr)
                        //    {
                        //        this.isError = true;
                        //        e.Cancel = true;
                        //        return true;
                        //    }
                        //    this.isError = true;

                        //    if (list.EditingControl != null)
                        //    {
                        //        var textBox = list.EditingControl as TextBox;
                        //        if (textBox != null)
                        //        {
                        //            textBox.SelectAll();
                        //        }
                        //    }

                        //    if (this.errorCancelFlg)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    LogUtility.DebugMethodEnd(e, false);
                        //    return false;
                        //}

                        // マスタ存在チェック
                        M_GYOUSHA condGyousha = new M_GYOUSHA();
                        condGyousha.GYOUSHA_CD = list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString();
                        // 20151021 BUNN #12040 STR
                        condGyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                        // 20151021 BUNN #12040 END
                        M_GYOUSHA[] aryGyousha = this.gyoushaDao.GetAllValidData(condGyousha);
                        if (aryGyousha != null && aryGyousha.Length > 0)
                        {
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"].Value = aryGyousha[0].GYOUSHA_NAME_RYAKU;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS1"].Value = aryGyousha[0].GYOUSHA_ADDRESS1;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS2"].Value = aryGyousha[0].GYOUSHA_ADDRESS2;
                            M_TODOUFUKEN todoufuken = new M_TODOUFUKEN();
                            if (!aryGyousha[0].GYOUSHA_TODOUFUKEN_CD.IsNull)
                            {
                                todoufuken = this.todoufukenDao.GetDataByCd(aryGyousha[0].GYOUSHA_TODOUFUKEN_CD.ToString());
                            }
                            if (todoufuken != null)
                            {
                                this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = todoufuken.TODOUFUKEN_NAME_RYAKU;
                            }
                        }
                        else
                        {
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"].Value = string.Empty;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.GYOUSHA);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS1"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS2"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.PreviousValue))
                    {
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                }
                if (e.CellName.Equals("LAST_SHOBUN_JIGYOUJOU_CD") && list.Rows.Count > 0 && list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value != null)
                {
                    if (!this.isError && list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(this.form.PreviousValue))
                    {
                        return false;
                    }
                    this.isError = false;

                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString()))
                    {
                        // 処分事業場CDが入力されている状態で、処分業者CDがクリアされていた場合、エラーとする
                        if (list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString()))
                        {
                            if (this.errorCancelFlg)
                            {
                                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                                msgLogic.MessageBoxShow("E051", "処分受託者");
                                // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                                e.Cancel = true;
                            }
                            list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value = string.Empty;
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }
                            LogUtility.DebugMethodEnd(e, false);
                            return false;
                        }

                        // 重複チェック
                        //if (!this.CheckBetsu4Duplicate(list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value, list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value, list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value, e.RowIndex, out catchErr))
                        //{
                        //    if (catchErr)
                        //    {
                        //        this.isError = true;
                        //        e.Cancel = true;
                        //        return true;
                        //    }
                        //    this.isError = true;

                        //    if (list.EditingControl != null)
                        //    {
                        //        var textBox = list.EditingControl as TextBox;
                        //        if (textBox != null)
                        //        {
                        //            textBox.SelectAll();
                        //        }
                        //    }

                        //    if (this.errorCancelFlg)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //    LogUtility.DebugMethodEnd(e, false);
                        //    return false;
                        //}

                        // マスタ存在チェック
                        M_GENBA genba = new M_GENBA();
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        genba.GYOUSHA_CD = this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString();
                        genba.GENBA_CD = this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString();
                        genba.SAISHUU_SHOBUNJOU_KBN = true;
                        // 20151021 BUNN #12040 STR
                        gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                        // 20151021 BUNN #12040 END
                        DataTable dt = this.genbaDao.GetDataBySqlFileWithGyousha(CHECK_GENBA_DATA_SQL, genba, gyousha);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"].Value = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value = dt.Rows[0]["GENBA_ADDRESS1"].ToString();
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value = dt.Rows[0]["GENBA_ADDRESS2"].ToString();
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"].Value = dt.Rows[0]["TODOUFUKEN_NAME"].ToString();
                        }
                        else
                        {
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                            this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.M_GENBA);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"].Value = string.Empty;
                        this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    }
                }
                string houhouCd = Convert.ToString(list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value);
                if (e.CellName.Equals("SHOBUN_HOUHOU_CD"))
                {
                    // 重複チェック
                    //if (!this.CheckBetsu4Duplicate(list[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value, list[e.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value, list[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value, e.RowIndex, out catchErr))
                    //{
                    //    if (catchErr)
                    //    {
                    //        this.isError = true;
                    //        e.Cancel = true;
                    //        return true;
                    //    }
                    //    if (list.EditingControl != null)
                    //    {
                    //        var textBox = list.EditingControl as TextBox;
                    //        if (textBox != null)
                    //        {
                    //            textBox.SelectAll();
                    //        }
                    //    }

                    //    if (this.errorCancelFlg)
                    //    {
                    //        e.Cancel = true;
                    //    }
                    //    LogUtility.DebugMethodEnd(e, false);
                    //    return false;
                    //}
                    if (!string.IsNullOrWhiteSpace(houhouCd))
                    {
                        // マスタ存在チェック
                        M_SHOBUN_HOUHOU houhou = new M_SHOBUN_HOUHOU();
                        houhou.SHOBUN_HOUHOU_CD = houhouCd;
                        M_SHOBUN_HOUHOU[] houhous = this.shobunHouhouDao.GetAllValidData(houhou);
                        if (houhous != null && houhous.Length > 0)
                        {
                            list[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = houhous[0].SHOBUN_HOUHOU_NAME_RYAKU;
                        }
                        else
                        {
                            list[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = string.Empty;

                            if (list.EditingControl != null)
                            {
                                var textBox = list.EditingControl as TextBox;
                                if (textBox != null)
                                {
                                    textBox.SelectAll();
                                }
                            }

                            if (this.errorCancelFlg)
                            {
                                msgLogic.MessageBoxShow("E020", ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_HOUHOU);
                                e.Cancel = true;
                            }
                            this.isError = true;
                        }
                    }
                    else
                    {
                        list[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListBetsu4CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約 覚書のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool ListOboeCellValidating(CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            GcCustomMultiRow list = this.form.listOboe;
            if (list.Rows[e.RowIndex].IsNewRow)
            {
                this.oboeNewRowFlg = true;
            }

            return true;
        }

        /// <summary>
        /// 処分事業者、最終処分事業場、処分方法の組み合わせでの重複チェック
        /// </summary>
        /// <param name="jigyousha"></param>
        /// <param name="jigyoujou"></param>
        /// <param name="shobunHouhou"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool CheckBetsu4Duplicate(object jigyousha, object jigyoujou, object shobunHouhou, int rowIndex, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(jigyousha, jigyoujou, shobunHouhou, rowIndex);
                GcCustomMultiRow list = this.form.listBetsu4;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];
                bool ret = true;
                catchErr = false;

                // 入力値のどちらかが未入力の場合、処理しない
                if (jigyousha == null || jigyoujou == null || shobunHouhou == null
                    || string.IsNullOrWhiteSpace(jigyousha.ToString())
                    || string.IsNullOrWhiteSpace(jigyoujou.ToString())
                    || string.IsNullOrWhiteSpace(shobunHouhou.ToString()))
                {
                    LogUtility.DebugMethodEnd(jigyousha, jigyoujou, shobunHouhou, rowIndex);
                    return ret;
                }

                // 重複チェック
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    if (list.Rows[i].IsNewRow) continue;
                    if (i == rowIndex) continue;

                    if (jigyousha.ToString().Equals(list[i, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString())
                        && jigyoujou.ToString().Equals(list[i, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString())
                        && shobunHouhou.ToString().Equals(list[i, "SHOBUN_HOUHOU_CD"].Value.ToString()))
                    {
                        msgLogic.MessageBoxShow("E037", ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_GYOUSHA
                            + "," + ItakuKeiyakuHoshu.Properties.Resources.LAST_SHOBUN_JIGYOUJOU
                            + "," + ItakuKeiyakuHoshu.Properties.Resources.SHOBUN_HOUHOU);
                        ret = false;
                        break;
                    }
                }

                LogUtility.DebugMethodEnd(jigyousha, jigyoujou, shobunHouhou, rowIndex, catchErr);
                return ret;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckBetsu4Duplicate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(jigyousha, jigyoujou, shobunHouhou, rowIndex, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 運搬許可証紐付管理タブ内の行政許可区分入力制御
        /// </summary>
        public void ChangeKyokaKbnText(CustomTextBox item, CustomTextBox setItem)
        {
            LogUtility.DebugMethodStart(item, setItem);

            // 値によって表示内容を制御する
            switch (item.Text)
            {
                case "1":
                    setItem.Text = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_1;
                    break;

                case "2":
                    setItem.Text = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_2;
                    break;

                case "3":
                    setItem.Text = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_3;
                    break;

                case "4":
                    setItem.Text = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_4;
                    break;

                case "5":
                    setItem.Text = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_5;
                    break;

                case "6":
                    setItem.Text = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_6;
                    break;

                default:
                    setItem.Text = string.Empty;
                    break;
            }

            LogUtility.DebugMethodEnd(item, setItem);
        }

        /// <summary>
        /// 運搬紐付業者選択ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool UpnKyokaSearch()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_CHIIKIBETSU_KYOKA chiikiKyoka = new M_CHIIKIBETSU_KYOKA();
                DataTable dt1 = null;
                DataTable dt2 = null;

                // テーブルの関連付けを解除する
                this.form.listUpnKyokasho.DataSource = null;

                // 共通検索条件を設定する
                if (!this.form.UPNKYOKA_GYOUSHA_CD.Text.Equals(string.Empty))
                {
                    chiikiKyoka.GYOUSHA_CD = this.form.UPNKYOKA_GYOUSHA_CD.Text;
                }
                if (!this.form.UPNKYOKA_CHIIKI_CD.Text.Equals(string.Empty))
                {
                    chiikiKyoka.CHIIKI_CD = this.form.UPNKYOKA_CHIIKI_CD.Text;
                }

                // 行政許可区分の入力に従ってデータ検索を行う
                if (this.form.UPNKYOKA_KBN.Text.Equals(string.Empty) || this.form.UPNKYOKA_KBN.Text.Equals("1"))
                {
                    if (!this.form.UPNKYOKA_NO.Text.Equals(string.Empty))
                    {
                        chiikiKyoka.FUTSUU_KYOKA_NO = this.form.UPNKYOKA_NO.Text;
                    }
                    if (this.form.UPNKYOKA_BEGIN.Value != null)
                    {
                        chiikiKyoka.FUTSUU_KYOKA_BEGIN = DateTime.Parse(((DateTime)this.form.UPNKYOKA_BEGIN.Value).ToString("yyyy/MM/dd"));
                    }
                    if (this.form.UPNKYOKA_END.Value != null)
                    {
                        chiikiKyoka.FUTSUU_KYOKA_END = DateTime.Parse(((DateTime)this.form.UPNKYOKA_END.Value).ToString("yyyy/MM/dd"));
                    }
                    dt1 = this.chiikibetsuKyokaDao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_DATA_SQL_1, chiikiKyoka);
                }
                if (this.form.UPNKYOKA_KBN.Text.Equals(string.Empty) || this.form.UPNKYOKA_KBN.Text.Equals("2"))
                {
                    if (!this.form.UPNKYOKA_NO.Text.Equals(string.Empty))
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_NO = this.form.UPNKYOKA_NO.Text;
                    }
                    if (this.form.UPNKYOKA_BEGIN.Value != null)
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_BEGIN = DateTime.Parse(((DateTime)this.form.UPNKYOKA_BEGIN.Value).ToString("yyyy/MM/dd"));
                    }
                    if (this.form.UPNKYOKA_END.Value != null)
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_END = DateTime.Parse(((DateTime)this.form.UPNKYOKA_END.Value).ToString("yyyy/MM/dd"));
                    }
                    dt2 = this.chiikibetsuKyokaDao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_DATA_SQL_2, chiikiKyoka);
                }

                // 登録済の許可証データを検索し、初期値としてリセットする
                if (this.upnKyokashoTable == null)
                {
                    M_ITAKU_UPN_KYOKASHO searchParam = new M_ITAKU_UPN_KYOKASHO();
                    searchParam.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    this.upnKyokashoTable = upnKyokashoDao.GetDataBySqlFile(this.GET_ITAKU_UPN_KYOKASHO_DATA_SQL, searchParam);
                }
                else
                {
                    bool catchErr = this.UpnKyokaDust();
                    if (catchErr)
                    {
                        return true;
                    }
                }
                this.upnKyokashoTable.Columns["KYOKA_LINK"].ReadOnly = false;
                this.upnKyokashoTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.upnKyokashoTable.Columns["SEQ"].AllowDBNull = true;
                this.upnKyokashoTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.upnKyokashoTable.Columns["TIME_STAMP"].Unique = false;

                // dt1のデータをマージする
                this.MergeTable(dt1, this.upnKyokashoTable);

                // dt2のデータをマージする
                this.MergeTable(dt2, this.upnKyokashoTable);

                // データをソートする
                DataTable sortTable = this.upnKyokashoTable.Copy();
                this.upnKyokashoTable.Rows.Clear();
                if (sortTable.Rows.Count > 0)
                {
                    DataRow[] rows = sortTable.Select("1 = 1", "KYOKA_LINK DESC, GYOUSHA_CD ASC, GENBA_CD ASC, CHIIKI_CD ASC, KYOKA_KBN ASC");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        this.upnKyokashoTable.Rows.Add(rows[i].ItemArray);
                    }
                }

                // 一覧の中で、許可番号がセットされていない行は削除する
                for (int i = (this.upnKyokashoTable.Rows.Count - 1); i >= 0; i--)
                {
                    DataRow row = this.upnKyokashoTable.Rows[i];
                    if (row["KYOKA_NO"] == null || string.IsNullOrWhiteSpace(row["KYOKA_NO"].ToString()))
                    {
                        this.upnKyokashoTable.Rows.Remove(row);
                    }
                }

                // テーブルの関連付けを再設定する
                this.form.listUpnKyokasho.DataSource = this.upnKyokashoTable;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UpnKyokaSearch", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpnKyokaSearch", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 運搬紐付業者ゴミ箱ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool UpnKyokaDust()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.upnKyokashoTable == null || this.upnKyokashoTable.Rows.Count == 0)
                {
                    return false;
                }
                DataTable dt = this.upnKyokashoTable.Copy();
                // 紐付チェックのついていないレコードを削除する
                for (int i = (dt.Rows.Count - 1); i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (!((bool)dr[0]))
                    {
                        dt.Rows.Remove(dr);
                    }
                }
                // テーブルの関連付けを再設定する
                this.upnKyokashoTable = dt;
                this.form.listUpnKyokasho.DataSource = this.upnKyokashoTable;
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpnKyokaDust", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        ///// <summary>
        ///// 処分許可証　業者CD Validatedイベント
        ///// </summary>
        ///// <param name="e"></param>
        //public void SbnKyokaGyoushaValidating(System.ComponentModel.CancelEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(e);

        //    // 業者CDがクリアされた場合、現場もクリアする
        //    if (string.IsNullOrWhiteSpace(this.form.SBNKYOKA_GYOUSHA_CD.Text))
        //    {
        //        this.form.SBNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
        //        this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
        //        this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;
        //    }

        //    LogUtility.DebugMethodEnd(e);
        //}

        /// <summary>
        /// 処分許可証　現場CD Validatedイベント
        /// </summary>
        /// <param name="e"></param>
        public bool SbnKyokaGenbaValidating(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] param = new string[1];
                param[0] = ItakuKeiyakuHoshu.Properties.Resources.GYOUSHA;

                // 業者CDが未入力の場合、エラー
                if (!string.IsNullOrWhiteSpace(this.form.SBNKYOKA_GENBA_CD.Text) && string.IsNullOrWhiteSpace(this.form.SBNKYOKA_GYOUSHA_CD.Text))
                {
                    if (this.errorCancelFlg)
                    {
                        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                        msgLogic.MessageBoxShow("E051", "業者");
                        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) S
                        e.Cancel = true;
                        this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
                        LogUtility.DebugMethodEnd(e, false);
                        return false;
                    }
                }

                // 現場CDの存在チェック
                if (!string.IsNullOrWhiteSpace(this.form.SBNKYOKA_GENBA_CD.Text))
                {
                    M_GENBA genba = new M_GENBA();
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    genba.GYOUSHA_CD = this.form.SBNKYOKA_GYOUSHA_CD.Text;
                    genba.GENBA_CD = this.form.SBNKYOKA_GENBA_CD.Text;
                    // 20151021 BUNN #12040 STR
                    genba.SHOBUN_NIOROSHI_GENBA_KBN = true;
                    genba.SAISHUU_SHOBUNJOU_KBN = true;
                    gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                    // 20151021 BUNN #12040 END
                    DataTable dt = this.genbaDao.GetDataBySqlFileWithGyousha(CHECK_GENBA_DATA_SQL, genba, gyousha);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
                        this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;

                        if (this.errorCancelFlg)
                        {
                            msgLogic.MessageBoxShow("E020", "現場");
                            e.Cancel = true;
                        }
                    }
                }
                else
                {
                    this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
                    this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd(e, false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                e.Cancel = true;
                LogUtility.Error("SbnKyokaGenbaValidating", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogUtility.Error("SbnKyokaGenbaValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e, true);
                return true;
            }
        }

        /// <summary>
        /// 処分紐付業者選択ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SbnKyokaSearch()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_CHIIKIBETSU_KYOKA chiikiKyoka = new M_CHIIKIBETSU_KYOKA();
                DataTable dt3 = null;
                DataTable dt4 = null;
                DataTable dt5 = null;
                DataTable dt6 = null;

                // テーブルの関連付けを解除する
                this.form.listSbnKyokasho.DataSource = null;

                // 共通検索条件を設定する
                if (!this.form.SBNKYOKA_GYOUSHA_CD.Text.Equals(string.Empty))
                {
                    chiikiKyoka.GYOUSHA_CD = this.form.SBNKYOKA_GYOUSHA_CD.Text;
                }
                if (!this.form.SBNKYOKA_GENBA_CD.Text.Equals(string.Empty))
                {
                    chiikiKyoka.GENBA_CD = this.form.SBNKYOKA_GENBA_CD.Text;
                }
                if (!this.form.SBNKYOKA_CHIIKI_CD.Text.Equals(string.Empty))
                {
                    chiikiKyoka.CHIIKI_CD = this.form.SBNKYOKA_CHIIKI_CD.Text;
                }

                // 行政許可区分の入力に従ってデータ検索を行う
                if (this.form.SBNKYOKA_KBN.Text.Equals(string.Empty) || this.form.SBNKYOKA_KBN.Text.Equals("3"))
                {
                    if (!this.form.SBNKYOKA_NO.Text.Equals(string.Empty))
                    {
                        chiikiKyoka.FUTSUU_KYOKA_NO = this.form.SBNKYOKA_NO.Text;
                    }
                    if (this.form.SBNKYOKA_BEGIN.Value != null)
                    {
                        chiikiKyoka.FUTSUU_KYOKA_BEGIN = DateTime.Parse(((DateTime)this.form.SBNKYOKA_BEGIN.Value).ToString("yyyy/MM/dd"));
                    }
                    if (this.form.SBNKYOKA_END.Value != null)
                    {
                        chiikiKyoka.FUTSUU_KYOKA_END = DateTime.Parse(((DateTime)this.form.SBNKYOKA_END.Value).ToString("yyyy/MM/dd"));
                    }
                    dt3 = this.chiikibetsuKyokaDao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_DATA_SQL_3, chiikiKyoka);
                }
                if (this.form.SBNKYOKA_KBN.Text.Equals(string.Empty) || this.form.SBNKYOKA_KBN.Text.Equals("4"))
                {
                    if (!this.form.SBNKYOKA_NO.Text.Equals(string.Empty))
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_NO = this.form.SBNKYOKA_NO.Text;
                    }
                    if (this.form.SBNKYOKA_BEGIN.Value != null)
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_BEGIN = DateTime.Parse(((DateTime)this.form.SBNKYOKA_BEGIN.Value).ToString("yyyy/MM/dd"));
                    }
                    if (this.form.SBNKYOKA_END.Value != null)
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_END = DateTime.Parse(((DateTime)this.form.SBNKYOKA_END.Value).ToString("yyyy/MM/dd"));
                    }
                    dt4 = this.chiikibetsuKyokaDao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_DATA_SQL_4, chiikiKyoka);
                }
                if (this.form.SBNKYOKA_KBN.Text.Equals(string.Empty) || this.form.SBNKYOKA_KBN.Text.Equals("5"))
                {
                    if (!this.form.SBNKYOKA_NO.Text.Equals(string.Empty))
                    {
                        chiikiKyoka.FUTSUU_KYOKA_NO = this.form.SBNKYOKA_NO.Text;
                    }
                    if (this.form.SBNKYOKA_BEGIN.Value != null)
                    {
                        chiikiKyoka.FUTSUU_KYOKA_BEGIN = DateTime.Parse(((DateTime)this.form.SBNKYOKA_BEGIN.Value).ToString("yyyy/MM/dd"));
                    }
                    if (this.form.SBNKYOKA_END.Value != null)
                    {
                        chiikiKyoka.FUTSUU_KYOKA_END = DateTime.Parse(((DateTime)this.form.SBNKYOKA_END.Value).ToString("yyyy/MM/dd"));
                    }
                    dt5 = this.chiikibetsuKyokaDao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_DATA_SQL_5, chiikiKyoka);
                }
                if (this.form.SBNKYOKA_KBN.Text.Equals(string.Empty) || this.form.SBNKYOKA_KBN.Text.Equals("6"))
                {
                    if (!this.form.SBNKYOKA_NO.Text.Equals(string.Empty))
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_NO = this.form.SBNKYOKA_NO.Text;
                    }
                    if (this.form.SBNKYOKA_BEGIN.Value != null)
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_BEGIN = DateTime.Parse(((DateTime)this.form.SBNKYOKA_BEGIN.Value).ToString("yyyy/MM/dd"));
                    }
                    if (this.form.SBNKYOKA_END.Value != null)
                    {
                        chiikiKyoka.TOKUBETSU_KYOKA_END = DateTime.Parse(((DateTime)this.form.SBNKYOKA_END.Value).ToString("yyyy/MM/dd"));
                    }
                    dt6 = this.chiikibetsuKyokaDao.GetDataBySqlFile(this.GET_CHIIKIBETSU_KYOKA_DATA_SQL_6, chiikiKyoka);
                }

                // 登録済の許可証データを検索し、初期値としてリセットする
                if (this.sbnKyokashoTable == null)
                {
                    M_ITAKU_SBN_KYOKASHO searchParam = new M_ITAKU_SBN_KYOKASHO();
                    searchParam.SYSTEM_ID = this.form.SYSTEM_ID.Text;
                    this.sbnKyokashoTable = sbnKyokashoDao.GetDataBySqlFile(this.GET_ITAKU_SBN_KYOKASHO_DATA_SQL, searchParam);
                }
                else
                {
                    this.SbnKyokaDust();
                }
                this.sbnKyokashoTable.Columns["KYOKA_LINK"].ReadOnly = false;
                this.sbnKyokashoTable.Columns["SYSTEM_ID"].AllowDBNull = true;
                this.sbnKyokashoTable.Columns["SEQ"].AllowDBNull = true;
                this.sbnKyokashoTable.Columns["TIME_STAMP"].AllowDBNull = true;
                this.sbnKyokashoTable.Columns["TIME_STAMP"].Unique = false;

                // dt3のデータをマージする
                this.MergeTable(dt3, this.sbnKyokashoTable);

                // dt4のデータをマージする
                this.MergeTable(dt4, this.sbnKyokashoTable);

                // dt5のデータをマージする
                this.MergeTable(dt5, this.sbnKyokashoTable);

                // dt6のデータをマージする
                this.MergeTable(dt6, this.sbnKyokashoTable);

                // データをソートする
                DataTable sortTable = this.sbnKyokashoTable.Copy();
                this.sbnKyokashoTable.Rows.Clear();
                if (sortTable.Rows.Count > 0)
                {
                    DataRow[] rows = sortTable.Select("1 = 1", "KYOKA_LINK DESC, GYOUSHA_CD ASC, GENBA_CD ASC, CHIIKI_CD ASC, KYOKA_KBN ASC");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        this.sbnKyokashoTable.Rows.Add(rows[i].ItemArray);
                    }
                }

                // 一覧の中で、許可番号がセットされていない行は削除する
                for (int i = (this.sbnKyokashoTable.Rows.Count - 1); i >= 0; i--)
                {
                    DataRow row = this.sbnKyokashoTable.Rows[i];
                    if (row["KYOKA_NO"] == null || string.IsNullOrWhiteSpace(row["KYOKA_NO"].ToString()))
                    {
                        this.sbnKyokashoTable.Rows.Remove(row);
                    }
                }

                // テーブルの関連付けを再設定する
                this.form.listSbnKyokasho.DataSource = this.sbnKyokashoTable;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SbnKyokaSearch", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SbnKyokaSearch", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 紐付管理用データテーブルのマージ処理
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="targetTable"></param>
        private void MergeTable(DataTable sourceTable, DataTable targetTable)
        {
            if (sourceTable != null && targetTable != null)
            {
                for (int i = 0; i < sourceTable.Rows.Count; i++)
                {
                    DataRow row = sourceTable.Rows[i];
                    DataRow[] res = targetTable.Select("KYOKA_KBN = " + row["KYOKA_KBN"].ToString() + " AND GYOUSHA_CD = '" + row["GYOUSHA_CD"].ToString() + "' AND GENBA_CD = '" + row["GENBA_CD"].ToString() + "' AND CHIIKI_CD = '" + row["CHIIKI_CD"].ToString() + "' AND KYOKA_NO = '" + row["KYOKA_NO"].ToString() + "'");

                    if (res.Length == 0)
                    {
                        DataRow newRow = targetTable.NewRow();
                        newRow["KYOKA_LINK"] = false;
                        newRow["GYOUSHA_CD"] = row["GYOUSHA_CD"];
                        newRow["GYOUSHA_NAME_RYAKU"] = row["GYOUSHA_NAME_RYAKU"];
                        newRow["GENBA_CD"] = row["GENBA_CD"];
                        newRow["GENBA_NAME_RYAKU"] = row["GENBA_NAME_RYAKU"];
                        newRow["CHIIKI_CD"] = row["CHIIKI_CD"];
                        newRow["CHIIKI_NAME_RYAKU"] = row["CHIIKI_NAME_RYAKU"];
                        newRow["KYOKA_KBN"] = row["KYOKA_KBN"];
                        newRow["KYOKA_NO"] = row["KYOKA_NO"];
                        newRow["KYOKA_END"] = row["KYOKA_END"];
                        targetTable.Rows.Add(newRow);
                    }
                }
            }
        }

        /// <summary>
        /// 処分紐付業者ゴミ箱ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SbnKyokaDust()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.sbnKyokashoTable == null || this.sbnKyokashoTable.Rows.Count == 0)
                {
                    return false;
                }
                DataTable dt = this.sbnKyokashoTable.Copy();
                // 紐付チェックのついていないレコードを削除する
                for (int i = (dt.Rows.Count - 1); i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (!((bool)dr[0]))
                    {
                        dt.Rows.Remove(dr);
                    }
                }
                // テーブルの関連付けを再設定する
                this.sbnKyokashoTable = dt;
                this.form.listSbnKyokasho.DataSource = this.sbnKyokashoTable;
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SbnKyokaDust", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 行政許可区分表示変更処理
        /// </summary>
        /// <param name="e"></param>
        public bool ListKyokashoCellFormatting(CellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null)
                {
                    return false;
                }
                string def = e.Value.ToString();
                switch (def)
                {
                    case "1":
                        e.Value = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_1;
                        break;

                    case "2":
                        e.Value = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_2;
                        break;

                    case "3":
                        e.Value = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_3;
                        break;

                    case "4":
                        e.Value = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_4;
                        break;

                    case "5":
                        e.Value = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_5;
                        break;

                    case "6":
                        e.Value = ItakuKeiyakuHoshu.Properties.Resources.KYOKA_KBN_6;
                        break;

                    default:
                        e.Value = string.Empty;
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListKyokashoCellFormatting", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 日付相関チェック処理
        /// </summary>
        /// <param name="ctrlName"></param>
        /// <returns></returns>
        public bool CheckDateCorrelation(string ctrlName)
        {
            try
            {
                bool ret = true;
                DateTime from;
                DateTime to;
                List<string> msg = new List<string>();

                switch (ctrlName)
                {
                    case "KEIYAKUSHO_CREATE_DATE":
                        if (this.form.KEIYAKUSHO_CREATE_DATE.Value != null && this.form.KEIYAKUSHO_SEND_DATE.Value != null &&
                            DateTime.TryParse(this.form.KEIYAKUSHO_CREATE_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.KEIYAKUSHO_SEND_DATE.Value.ToString(), out to))
                        {
                            if (from > to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT2, Resources.KEIYAKUSHO_CREATE_DATE, Resources.KEIYAKUSHO_SEND_DATE));
                            }
                        }
                        break;

                    case "KEIYAKUSHO_SEND_DATE":
                        if (this.form.KEIYAKUSHO_SEND_DATE.Value != null && this.form.KEIYAKUSHO_CREATE_DATE.Value != null &&
                            DateTime.TryParse(this.form.KEIYAKUSHO_SEND_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.KEIYAKUSHO_CREATE_DATE.Value.ToString(), out to))
                        {
                            if (from < to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT1, Resources.KEIYAKUSHO_SEND_DATE, Resources.KEIYAKUSHO_CREATE_DATE));
                            }
                        }
                        if (this.form.KEIYAKUSHO_SEND_DATE.Value != null && this.form.KEIYAKUSHO_RETURN_DATE.Value != null &&
                            DateTime.TryParse(this.form.KEIYAKUSHO_SEND_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.KEIYAKUSHO_RETURN_DATE.Value.ToString(), out to))
                        {
                            if (from > to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT2, Resources.KEIYAKUSHO_SEND_DATE, Resources.KEIYAKUSHO_RETURN_DATE));
                            }
                        }
                        break;

                    case "KOUSHIN_END_DATE":
                        if (this.form.KOUSHIN_END_DATE.Value != null && this.form.YUUKOU_BEGIN.Value != null &&
                            DateTime.TryParse(this.form.KOUSHIN_END_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.YUUKOU_BEGIN.Value.ToString(), out to))
                        {
                            if (from < to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT1, Resources.KOUSHIN_END_DATE, Resources.YUUKOU_BEGIN));
                            }
                        }
                        break;

                    case "YUUKOU_BEGIN":
                        if (this.form.YUUKOU_BEGIN.Value != null && this.form.KOUSHIN_END_DATE.Value != null &&
                            DateTime.TryParse(this.form.YUUKOU_BEGIN.Value.ToString(), out from) && DateTime.TryParse(this.form.KOUSHIN_END_DATE.Value.ToString(), out to))
                        {
                            if (from > to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT2, Resources.YUUKOU_BEGIN, Resources.KOUSHIN_END_DATE));
                            }
                        }
                        //if (this.form.YUUKOU_BEGIN.Value != null && this.form.YUUKOU_END.Value != null &&
                        //    DateTime.TryParse(this.form.YUUKOU_BEGIN.Value.ToString(), out from) && DateTime.TryParse(this.form.YUUKOU_END.Value.ToString(), out to))
                        //{
                        //    if (from > to)
                        //    {
                        //        msg.Add(string.Format(Resources.DATE_ERR_FMT2, Resources.YUUKOU_BEGIN, Resources.YUUKOU_END));
                        //    }
                        //}
                        break;

                    case "YUUKOU_END":
                        //if (this.form.YUUKOU_END.Value != null && this.form.YUUKOU_BEGIN.Value != null &&
                        //    DateTime.TryParse(this.form.YUUKOU_END.Value.ToString(), out from) && DateTime.TryParse(this.form.YUUKOU_BEGIN.Value.ToString(), out to))
                        //{
                        //    if (from < to)
                        //    {
                        //        msg.Add(string.Format(Resources.DATE_ERR_FMT1, Resources.YUUKOU_END, Resources.YUUKOU_BEGIN));
                        //    }
                        //}
                        break;

                    case "KEIYAKUSHO_RETURN_DATE":
                        if (this.form.KEIYAKUSHO_RETURN_DATE.Value != null && this.form.KEIYAKUSHO_SEND_DATE.Value != null &&
                            DateTime.TryParse(this.form.KEIYAKUSHO_RETURN_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.KEIYAKUSHO_SEND_DATE.Value.ToString(), out to))
                        {
                            if (from < to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT1, Resources.KEIYAKUSHO_RETURN_DATE, Resources.KEIYAKUSHO_SEND_DATE));
                            }
                        }
                        if (this.form.KEIYAKUSHO_RETURN_DATE.Value != null && this.form.KEIYAKUSHO_END_DATE.Value != null &&
                            DateTime.TryParse(this.form.KEIYAKUSHO_RETURN_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.KEIYAKUSHO_END_DATE.Value.ToString(), out to))
                        {
                            if (from > to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT2, Resources.KEIYAKUSHO_RETURN_DATE, Resources.KEIYAKUSHO_END_DATE));
                            }
                        }
                        break;

                    case "KEIYAKUSHO_END_DATE":
                        if (this.form.KEIYAKUSHO_END_DATE.Value != null && this.form.KEIYAKUSHO_RETURN_DATE.Value != null &&
                            DateTime.TryParse(this.form.KEIYAKUSHO_END_DATE.Value.ToString(), out from) && DateTime.TryParse(this.form.KEIYAKUSHO_RETURN_DATE.Value.ToString(), out to))
                        {
                            if (from < to)
                            {
                                msg.Add(string.Format(Resources.DATE_ERR_FMT1, Resources.KEIYAKUSHO_END_DATE, Resources.KEIYAKUSHO_RETURN_DATE));
                            }
                        }
                        break;
                }

                if (msg.Count > 0)
                {
                    MessageBox.Show(string.Join("\r\n", msg.ToArray()), Properties.Resources.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    ret = false;
                }

                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDateCorrelation", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 契約日付チェック
        /// </summary>
        public bool CheckKeiyakuDate()
        {
            try
            {
                // 作成日が未入力の場合
                if (!this.form.KEIYAKUSHO_CREATE_DATE.Enabled || this.form.KEIYAKUSHO_CREATE_DATE.Value == null)
                {
                    this.form.KEIYAKUSHO_SEND_DATE.Value = null;
                    this.form.KEIYAKUSHO_SEND_DATE.Enabled = false;
                    this.form.KEIYAKUSHO_RETURN_DATE.Value = null;
                    this.form.KEIYAKUSHO_RETURN_DATE.Enabled = false;
                    this.form.KEIYAKUSHO_END_DATE.Value = null;
                    this.form.KEIYAKUSHO_END_DATE.Enabled = false;
                }
                else
                {
                    this.form.KEIYAKUSHO_SEND_DATE.Enabled = true;
                    this.form.KEIYAKUSHO_RETURN_DATE.Enabled = true;
                    this.form.KEIYAKUSHO_END_DATE.Enabled = true;
                }
                // 送付日が未入力の場合
                if (!this.form.KEIYAKUSHO_SEND_DATE.Enabled || this.form.KEIYAKUSHO_SEND_DATE.Value == null)
                {
                    this.form.KEIYAKUSHO_RETURN_DATE.Value = null;
                    this.form.KEIYAKUSHO_RETURN_DATE.Enabled = false;
                    this.form.KEIYAKUSHO_END_DATE.Value = null;
                    this.form.KEIYAKUSHO_END_DATE.Enabled = false;
                }
                else
                {
                    this.form.KEIYAKUSHO_RETURN_DATE.Enabled = true;
                    this.form.KEIYAKUSHO_END_DATE.Enabled = true;
                }
                // 返送日が未入力の場合
                if (!this.form.KEIYAKUSHO_RETURN_DATE.Enabled || this.form.KEIYAKUSHO_RETURN_DATE.Value == null)
                {
                    this.form.KEIYAKUSHO_END_DATE.Value = null;
                    this.form.KEIYAKUSHO_END_DATE.Enabled = false;
                }
                else
                {
                    this.form.KEIYAKUSHO_END_DATE.Enabled = true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeiyakuDate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ステータスチェック
        /// </summary>
        public bool CheckStatus()
        {
            try
            {
                // 送付日が入力済の場合
                if (this.form.KEIYAKUSHO_CREATE_DATE.Value != null)
                {
                    this.form.ITAKU_KEIYAKU_STATUS.Text = "1";
                    this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(1);
                    this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(1);
                }
                // 送付日が入力済の場合
                if (this.form.KEIYAKUSHO_SEND_DATE.Value != null)
                {
                    this.form.ITAKU_KEIYAKU_STATUS.Text = "2";
                    this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(2);
                    this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(2);
                }
                // 返送日が入力済の場合
                if (this.form.KEIYAKUSHO_RETURN_DATE.Value != null)
                {
                    this.form.ITAKU_KEIYAKU_STATUS.Text = "3";
                    this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(3);
                    this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(3);
                }
                // 完了日が入力済の場合
                if (this.form.KEIYAKUSHO_END_DATE.Value != null)
                {
                    this.form.ITAKU_KEIYAKU_STATUS.Text = "4";
                    this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(4);
                    this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(4);
                }
                // 自動更新でかつ、自動更新終了日が過去の場合
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //if ((this.form.KOUSHIN_END_DATE.Value != null) && this.form.KOUSHIN_SHUBETSU.Text.Equals("1") && ((DateTime)this.form.KOUSHIN_END_DATE.Value < DateTime.Today.Date))
                if ((this.form.KOUSHIN_END_DATE.Value != null) && this.form.KOUSHIN_SHUBETSU.Text.Equals("1") && ((DateTime)this.form.KOUSHIN_END_DATE.Value < this.parentForm.sysDate.Date))
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                {
                    this.form.ITAKU_KEIYAKU_STATUS.Text = "5";
                    this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(5);
                    this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(5);
                }
                // 単発でかつ、有効期限終了日が過去の場合
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //if ((this.form.YUUKOU_END.Value != null) && this.form.KOUSHIN_SHUBETSU.Text.Equals("2") && ((DateTime)this.form.YUUKOU_END.Value < DateTime.Today.Date))
                if ((this.form.YUUKOU_END.Value != null) && this.form.KOUSHIN_SHUBETSU.Text.Equals("2") && ((DateTime)this.form.YUUKOU_END.Value < this.parentForm.sysDate.Date))
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                {
                    this.form.ITAKU_KEIYAKU_STATUS.Text = "5";
                    this.form.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(5);
                    this.ITAKU_KEIYAKU_STATUS_NAME.Text = this.GetItakuKeiyakuStatusName(5);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckStatus", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 数量フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemSuuryou(decimal num, out bool catchErr)
        {
            try
            {
                catchErr = false;
                //string format = "#,##0";
                string format = "#,##0.00";
                if (!string.IsNullOrWhiteSpace(this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT))
                {
                    format = this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT;
                }
                return string.Format("{0:" + format + "}", num);
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("FormatSystemSuuryou", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// 数量フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemSuuryouHinmei(decimal num, out bool catchErr)
        {
            try
            {
                catchErr = false;
                string format = "#,##0.00";
                if (!string.IsNullOrWhiteSpace(this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT))
                {
                    format = this.sysInfoEntity.ITAKU_KEIYAKU_SUURYOU_FORMAT;
                }
                return string.Format("{0:" + format + "}", num);
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("FormatSystemSuuryouHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemTanka(decimal num, out bool catchErr)
        {
            try
            {
                catchErr = false;
                string format = "#,##0";
                if (!string.IsNullOrWhiteSpace(this.sysInfoEntity.ITAKU_KEIYAKU_TANKA_FORMAT))
                {
                    format = this.sysInfoEntity.ITAKU_KEIYAKU_TANKA_FORMAT;
                }
                return string.Format("{0:" + format + "}", num);
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("FormatSystemTanka", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// 重量フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemJuuryou(decimal num)
        {
            string format = "#,##0";
            if (!string.IsNullOrWhiteSpace(this.sysInfoEntity.SYS_JYURYOU_FORMAT))
            {
                format = this.sysInfoEntity.SYS_JYURYOU_FORMAT;
            }
            return string.Format("{0:" + format + "}", num);
        }

        /// <summary>
        /// 委託契約ステータス名称取得処理
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string GetItakuKeiyakuStatusName(int status)
        {
            LogUtility.DebugMethodStart(status);

            string retName = string.Empty;

            switch (status)
            {
                case 1:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_STATUS_1;
                    break;

                case 2:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_STATUS_2;
                    break;

                case 3:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_STATUS_3;
                    break;

                case 4:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_STATUS_4;
                    break;

                case 5:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_STATUS_5;
                    break;

                default:
                    retName = string.Empty;
                    break;
            }

            LogUtility.DebugMethodEnd(status);
            return retName;
        }

        /// <summary>
        /// 委託契約種類名称取得処理
        /// </summary>
        /// <param name="shurui"></param>
        /// <returns></returns>
        private string GetItakuKeiyakuShuruiName(string shurui)
        {
            LogUtility.DebugMethodStart(shurui);

            string retName = string.Empty;

            switch (shurui)
            {
                case "1":
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_SHURUI_1;
                    break;

                case "2":
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_SHURUI_2;
                    break;

                case "3":
                    retName = ItakuKeiyakuHoshu.Properties.Resources.ITAKU_KEIYAKU_SHURUI_3;
                    break;

                default:
                    retName = string.Empty;
                    break;
            }

            LogUtility.DebugMethodEnd(shurui);
            return retName;
        }

        /// <summary>
        /// 運搬区間開始名取得処理
        /// </summary>
        /// <param name="shurui"></param>
        /// <returns></returns>
        private string GetUnpanFromName(Int16 cd)
        {
            LogUtility.DebugMethodStart(cd);

            string retName = string.Empty;

            switch (cd)
            {
                case 1:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.UNPAN_FROM_1;
                    break;

                case 2:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.UNPAN_FROM_2;
                    break;

                default:
                    retName = string.Empty;
                    break;
            }

            LogUtility.DebugMethodEnd(retName);
            return retName;
        }

        /// <summary>
        /// 運搬区間開始名取得処理
        /// </summary>
        /// <param name="shurui"></param>
        /// <returns></returns>
        private string GetUnpanEndName(Int16 cd)
        {
            LogUtility.DebugMethodStart(cd);

            string retName = string.Empty;

            switch (cd)
            {
                case 1:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.UNPAN_END_1;
                    break;

                case 2:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.UNPAN_END_2;
                    break;

                default:
                    retName = string.Empty;
                    break;
            }

            LogUtility.DebugMethodEnd(retName);
            return retName;
        }

        /// <summary>
        /// 混合名取得処理
        /// </summary>
        /// <param name="shurui"></param>
        /// <returns></returns>
        private string GetKongouName(Int16 cd)
        {
            LogUtility.DebugMethodStart(cd);

            string retName = string.Empty;

            switch (cd)
            {
                case 1:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.KONGOU_1;
                    break;

                case 2:
                    retName = ItakuKeiyakuHoshu.Properties.Resources.KONGOU_2;
                    break;

                default:
                    retName = string.Empty;
                    break;
            }

            LogUtility.DebugMethodEnd(retName);
            return retName;
        }

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public bool CheckListTSUMIKAEPopup(KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.KeyCode == Keys.Space)
                {
                    if (this.form.listTsumikae.Columns[this.form.listTsumikae.CurrentCell.CellIndex].Name.Equals("KONGOU"))
                    {
                        MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("CD", typeof(string));
                        dt.Columns.Add("VALUE", typeof(string));
                        dt.Columns[0].ReadOnly = true;
                        dt.Columns[1].ReadOnly = true;
                        DataRow row;
                        row = dt.NewRow();
                        row["CD"] = string.Empty;
                        row["VALUE"] = string.Empty;
                        dt.Rows.Add(row);
                        row = dt.NewRow();
                        row = dt.NewRow();
                        row["CD"] = "1";
                        row["VALUE"] = ItakuKeiyakuHoshu.Properties.Resources.KONGOU_1;
                        dt.Rows.Add(row);
                        row = dt.NewRow();
                        row["CD"] = "2";
                        row["VALUE"] = ItakuKeiyakuHoshu.Properties.Resources.KONGOU_2;
                        dt.Rows.Add(row);
                        form.table = dt;
                        //form.title = "混合選択";
                        //form.headerList = new List<string>();
                        //form.headerList.Add("混合CD");
                        //form.headerList.Add("混合");
                        form.PopupTitleLabel = "混合選択";
                        form.PopupGetMasterField = "CD,VALUE";
                        form.PopupDataHeaderTitle = new string[] { "混合CD", "混合" };
                        form.ShowDialog();
                        if (form.ReturnParams != null)
                        {
                            this.form.listTsumikae.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                            this.form.listTsumikae[this.form.listTsumikae.CurrentCell.RowIndex, "KONGOU_NAME"].Value = form.ReturnParams[1][0].Value.ToString();
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckListTSUMIKAEPopup", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 中間処分場パターン呼出し
        /// </summary>
        public bool GetSbnPtn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var callHeader = new Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP.UIHeader();
                var callForm = new Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP.UIForm(r_framework.Const.DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI);
                var baseForm = new r_framework.APP.Base.BusinessBaseForm(callForm, callHeader);

                var isExistForm = new r_framework.Logic.FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    baseForm.StartPosition = FormStartPosition.CenterScreen;
                    baseForm.ShowDialog();
                }

                string retVal = callForm.OutSelectedPatternName;

                if (!string.IsNullOrWhiteSpace(retVal))
                {
                    this.betsu3Table.Rows.Clear();

                    M_SBNB_PATTERN cond = new M_SBNB_PATTERN();
                    cond.PATTERN_NAME = retVal;
                    cond.LAST_SBN_KBN = 1;
                    cond.ITAKU_KEIYAKU_TYPE = 1;
                    DataTable dt = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile(this.GET_SBNB_PATTERN_SBN_SQL, cond);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.SHOBUN_PATTERN_SYSTEM_ID.Text = dt.Rows[0]["SYSTEM_ID"].ToString();
                        this.form.SHOBUN_PATTERN_SEQ.Text = dt.Rows[0]["SEQ"].ToString();
                        this.form.SHOBUN_PATTERN_NAME.Text = dt.Rows[0]["PATTERN_NAME"].ToString();
                        foreach (DataRow row in dt.Rows)
                        {
                            DataRow newRow = this.betsu3Table.NewRow();
                            newRow["SHOBUN_GYOUSHA_CD"] = row["GYOUSHA_CD"].ToString();
                            newRow["SHOBUN_GYOUSHA_NAME"] = row["GYOUSHA_NAME"].ToString();
                            newRow["SHOBUN_GYOUSHA_ADDRESS1"] = row["GYOUSHA_ADDRESS1"].ToString();
                            newRow["SHOBUN_GYOUSHA_ADDRESS2"] = row["GYOUSHA_ADDRESS2"].ToString();
                            newRow["SHOBUN_GYOUSHA_TODOUFUKEN_NAME"] = row["GYOUSHA_TODOUFUKEN_NAME"].ToString();
                            newRow["SHOBUN_JIGYOUJOU_CD"] = row["GENBA_CD"].ToString();
                            newRow["SHOBUN_JIGYOUJOU_NAME"] = row["GENBA_NAME"].ToString();
                            newRow["SHOBUN_JIGYOUJOU_ADDRESS1"] = row["GENBA_ADDRESS1"].ToString();
                            newRow["SHOBUN_JIGYOUJOU_ADDRESS2"] = row["GENBA_ADDRESS2"].ToString();
                            newRow["SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"] = row["GENBA_TODOUFUKEN_NAME"].ToString();
                            newRow["SHOBUN_HOUHOU_CD"] = row["SHOBUN_HOUHOU_CD"].ToString();
                            newRow["SHOBUN_HOUHOU_NAME_RYAKU"] = row["SHOBUN_HOUHOU_NAME_RYAKU"].ToString();
                            /*if (eRow["HOKAN_JOGEN"] != null && !string.IsNullOrEmpty(eRow["HOKAN_JOGEN"].ToString()))
                            {
                                newRow["HOKAN_JOGEN"] = Double.Parse(eRow["HOKAN_JOGEN"].ToString());
                            }
                            if (eRow["HOKAN_JOGEN_UNIT_CD"] != null && !string.IsNullOrEmpty(eRow["HOKAN_JOGEN_UNIT_CD"].ToString()))
                            {
                                newRow["HOKAN_JOGEN_UNIT_CD"] = short.Parse(eRow["HOKAN_JOGEN_UNIT_CD"].ToString());
                                newRow["HOKAN_JOGEN_UNIT_NAME"] = eRow["HOKAN_JOGEN_UNIT_NAME"].ToString();
                            }*/
                            newRow["SHISETSU_CAPACITY"] = row["SHORI_SPEC"].ToString();
                            /*if (eRow["UNPAN_FROM"] != null && !string.IsNullOrEmpty(eRow["UNPAN_FROM"].ToString()))
                            {
                                var unpanFrom = short.Parse(eRow["UNPAN_FROM"].ToString());
                                newRow["UNPAN_FROM"] = unpanFrom;
                                newRow["UNPAN_FROM_NAME"] = this.GetUnpanFromName(unpanFrom);
                            }
                            if (eRow["UNPAN_END"] != null && !string.IsNullOrEmpty(eRow["UNPAN_END"].ToString()))
                            {
                                var unpanEnd = short.Parse(eRow["UNPAN_END"].ToString());
                                switch (unpanEnd)
                                {
                                    case (1):
                                        newRow["UNPAN_END"] = 2;
                                        break;

                                    case (2):
                                        newRow["UNPAN_END"] = 3;
                                        break;
                                }
                                newRow["UNPAN_END_NAME"] = this.GetUnpanEndName(unpanEnd);
                            }
                            if (eRow["KONGOU"] != null && !string.IsNullOrEmpty(eRow["KONGOU"].ToString()))
                            {
                                var kongou = short.Parse(eRow["KONGOU"].ToString());
                                newRow["KONGOU"] = kongou;
                                newRow["KONGOU_NAME"] = this.GetKongouName(kongou);
                            }*/

                            this.betsu3Table.Rows.Add(newRow);
                        }

                        // datasource変更時にイベントが実行されるので、一度イベントをクリアする
                        //if (this.form.tabItakuKeiyakuData.SelectedIndex == 5)
                        if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage6"))
                        {
                            this.form.listBetsu3.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu3CellValidating);
                        }

                        this.SetIchiran(this.form.listBetsu3, this.betsu3Table);

                        //if (this.form.tabItakuKeiyakuData.SelectedIndex == 5)
                        if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage6"))
                        {
                            this.form.listBetsu3.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu3CellValidating);
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSbnPtn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 中間処分場パターン登録
        /// </summary>
        [Transaction]
        public bool SetSbnPtn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                PatternRegistForm form = new PatternRegistForm();
                if (form != null)
                {
                    Int64 patternSystemId = 0;
                    Int32 patternSeq = 0;
                    if (!string.IsNullOrWhiteSpace(this.form.SHOBUN_PATTERN_SYSTEM_ID.Text))
                    {
                        patternSystemId = long.Parse(this.form.SHOBUN_PATTERN_SYSTEM_ID.Text);
                        patternSeq = int.Parse(this.form.SHOBUN_PATTERN_SEQ.Text);

                        M_SBNB_PATTERN cond = new M_SBNB_PATTERN();
                        cond.SYSTEM_ID = patternSystemId;
                        cond.SEQ = patternSeq;
                        cond.ROW_NO = 1;
                        cond.LAST_SBN_KBN = 1;
                        cond.ITAKU_KEIYAKU_TYPE = 1;
                        DataTable dt = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile(this.GET_SBNB_PATTERN_SBN_SQL, cond);
                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["PATTERN_FURIGANA"] != null)
                        {
                            form.PATTERN_FURIGANA.Text = dt.Rows[0]["PATTERN_FURIGANA"].ToString();
                        }
                        else
                        {
                            form.PATTERN_FURIGANA.Text = string.Empty;
                        }
                        form.PATTERN_NAME.Text = this.form.SHOBUN_PATTERN_NAME.Text;
                        form.SystemId = patternSystemId;
                        form.Seq = patternSeq;
                    }
                    else
                    {
                        form.PATTERN_FURIGANA.Text = string.Empty;
                        form.PATTERN_NAME.Text = string.Empty;
                        form.SystemId = 0;
                        form.Seq = 0;
                    }

                    // ダイアログを表示する
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.Text = "中間処分場パターン登録";
                    form.LABEL_TITLE.Text = "中間処分場パターン登録";
                    form.LastSbnKbn = 1;
                    form.ItakuKeiyakuType = 1;
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.OK)
                    {
                        // F5.登録を押下して戻ったときのみ登録処理を実行する
                        patternSystemId = form.SystemId;
                        patternSeq = form.Seq;

                        // 新規の場合、システムIDを採番する
                        if (patternSystemId == 0)
                        {
                            patternSystemId = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetMaxPlusKey();
                            patternSeq = 1;
                        }

                        // 一旦、全データ削除
                        M_SBNB_PATTERN cond = new M_SBNB_PATTERN();
                        cond.SYSTEM_ID = patternSystemId;
                        cond.SEQ = patternSeq;
                        DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile(this.DELETE_SBNB_PATTERN_SBN_SQL, cond);

                        Int16 rowNo = 0;
                        foreach (DataRow row in this.betsu3Table.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_GYOUSHA_CD"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_GYOUSHA_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_GYOUSHA_TODOUFUKEN_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_GYOUSHA_ADDRESS1"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_GYOUSHA_ADDRESS2"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_JIGYOUJOU_CD"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_JIGYOUJOU_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_JIGYOUJOU_ADDRESS1"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_JIGYOUJOU_ADDRESS2"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_HOUHOU_CD"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_HOUHOU_NAME_RYAKU"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHISETSU_CAPACITY"])))
                            {
                                rowNo++;
                                M_SBNB_PATTERN data = new M_SBNB_PATTERN();
                                data.SYSTEM_ID = patternSystemId;
                                data.SEQ = patternSeq;
                                data.ROW_NO = rowNo;
                                data.ITAKU_KEIYAKU_TYPE = 1;
                                data.LAST_SBN_KBN = 1;
                                data.PATTERN_NAME = form.PATTERN_NAME.Text;
                                data.PATTERN_FURIGANA = form.PATTERN_FURIGANA.Text;
                                data.GYOUSHA_CD = Convert.ToString(row["SHOBUN_GYOUSHA_CD"]);
                                data.GYOUSHA_NAME = Convert.ToString(row["SHOBUN_GYOUSHA_NAME"]);
                                data.GYOUSHA_ADDRESS1 = Convert.ToString(row["SHOBUN_GYOUSHA_ADDRESS1"]);
                                data.GYOUSHA_ADDRESS2 = Convert.ToString(row["SHOBUN_GYOUSHA_ADDRESS2"]);
                                data.GENBA_CD = Convert.ToString(row["SHOBUN_JIGYOUJOU_CD"]);
                                data.GENBA_NAME = Convert.ToString(row["SHOBUN_JIGYOUJOU_NAME"]);
                                data.GENBA_ADDRESS1 = Convert.ToString(row["SHOBUN_JIGYOUJOU_ADDRESS1"]);
                                data.GENBA_ADDRESS2 = Convert.ToString(row["SHOBUN_JIGYOUJOU_ADDRESS2"]);
                                data.SHORI_SPEC = Convert.ToString(row["SHISETSU_CAPACITY"]);
                                data.OTHER = string.Empty;
                                data.SHOBUN_HOUHOU_CD = Convert.ToString(row["SHOBUN_HOUHOU_CD"]);
                                /*if (eRow["HOKAN_JOGEN"] != null && !string.IsNullOrEmpty(eRow["HOKAN_JOGEN"].ToString()))
                                {
                                    data.HOKAN_JOGEN = SqlDouble.Parse(eRow["HOKAN_JOGEN"].ToString());
                                }
                                if (eRow["HOKAN_JOGEN_UNIT_CD"] != null && !string.IsNullOrEmpty(eRow["HOKAN_JOGEN_UNIT_CD"].ToString()))
                                {
                                    data.HOKAN_JOGEN_UNIT_CD = Sqlshort.Parse(eRow["HOKAN_JOGEN_UNIT_CD"].ToString());
                                }
                                if (eRow["UNPAN_FROM"] != null && !string.IsNullOrEmpty(eRow["UNPAN_FROM"].ToString()))
                                {
                                    data.UNPAN_FROM = Sqlshort.Parse(eRow["UNPAN_FROM"].ToString());
                                }
                                if (eRow["UNPAN_END"] != null && !string.IsNullOrEmpty(eRow["UNPAN_END"].ToString()))
                                {
                                    // DBの値に合わせて保存
                                    switch (eRow["UNPAN_END"].ToString())
                                    {
                                        case ("2"):
                                            data.UNPAN_END = Sqlshort.Parse("1");
                                            break;

                                        case ("3"):
                                            data.UNPAN_END = Sqlshort.Parse("2");
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                if (eRow["KONGOU"] != null && !string.IsNullOrEmpty(eRow["KONGOU"].ToString()))
                                {
                                    data.KONGOU = Sqlshort.Parse(eRow["KONGOU"].ToString());
                                }*/
                                data.HOKAN_JOGEN = SqlDecimal.Null;
                                data.HOKAN_JOGEN_UNIT_CD = SqlInt16.Null;
                                data.UNPAN_FROM = SqlInt16.Null;
                                data.UNPAN_END = SqlInt16.Null;
                                data.KONGOU = SqlInt16.Null;

                                data.SHUSENBETU = SqlInt16.Null;
                                data.BUNRUI = SqlInt16.Null;
                                data.END_KUBUN = SqlInt16.Null;
                                data.DELETE_FLG = false;

                                // 更新者情報設定
                                var dbLogic = new DataBinderLogic<r_framework.Entity.M_SBNB_PATTERN>(data);
                                dbLogic.SetSystemProperty(data, false);
                                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), data);

                                DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().Insert(data);
                            }
                        }

                        this.form.SHOBUN_PATTERN_SYSTEM_ID.Text = patternSystemId.ToString();
                        this.form.SHOBUN_PATTERN_SEQ.Text = patternSeq.ToString();
                        this.form.SHOBUN_PATTERN_NAME.Text = form.PATTERN_NAME.Text;

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("SetSbnPtn", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetSbnPtn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSbnPtn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 最終処分場パターン呼出し
        /// </summary>
        public bool GetLastSbnPtn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var callHeader = new Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP.UIHeader();
                var callForm = new Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP.UIForm(r_framework.Const.DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI);
                var baseForm = new r_framework.APP.Base.BusinessBaseForm(callForm, callHeader);

                var isExistForm = new r_framework.Logic.FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    baseForm.StartPosition = FormStartPosition.CenterScreen;
                    baseForm.ShowDialog();
                }

                string retVal = callForm.OutSelectedPatternName;

                if (!string.IsNullOrWhiteSpace(retVal))
                {
                    this.betsu4Table.Rows.Clear();

                    M_SBNB_PATTERN cond = new M_SBNB_PATTERN();
                    cond.PATTERN_NAME = retVal;
                    cond.LAST_SBN_KBN = 2;
                    cond.ITAKU_KEIYAKU_TYPE = 1;
                    DataTable dt = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile(this.GET_SBNB_PATTERN_SBN_SQL, cond);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text = dt.Rows[0]["SYSTEM_ID"].ToString();
                        this.form.LAST_SHOBUN_PATTERN_SEQ.Text = dt.Rows[0]["SEQ"].ToString();
                        this.form.LAST_SHOBUN_PATTERN_NAME.Text = dt.Rows[0]["PATTERN_NAME"].ToString();
                        foreach (DataRow row in dt.Rows)
                        {
                            DataRow newRow = this.betsu4Table.NewRow();
                            newRow["LAST_SHOBUN_GYOUSHA_CD"] = row["GYOUSHA_CD"].ToString();
                            newRow["LAST_SHOBUN_GYOUSHA_NAME"] = row["GYOUSHA_NAME"].ToString();
                            newRow["LAST_SHOBUN_GYOUSHA_ADDRESS1"] = row["GYOUSHA_ADDRESS1"].ToString();
                            newRow["LAST_SHOBUN_GYOUSHA_ADDRESS2"] = row["GYOUSHA_ADDRESS2"].ToString();
                            newRow["LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"] = row["GYOUSHA_TODOUFUKEN_NAME"].ToString();
                            newRow["LAST_SHOBUN_JIGYOUJOU_CD"] = row["GENBA_CD"].ToString();
                            newRow["LAST_SHOBUN_JIGYOUJOU_NAME"] = row["GENBA_NAME"].ToString();
                            newRow["LAST_SHOBUN_JIGYOUJOU_ADDRESS1"] = row["GENBA_ADDRESS1"].ToString();
                            newRow["LAST_SHOBUN_JIGYOUJOU_ADDRESS2"] = row["GENBA_ADDRESS2"].ToString();
                            newRow["LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"] = row["GENBA_TODOUFUKEN_NAME"].ToString();
                            newRow["SHOBUN_HOUHOU_CD"] = row["SHOBUN_HOUHOU_CD"].ToString();
                            newRow["SHOBUN_HOUHOU_NAME_RYAKU"] = row["SHOBUN_HOUHOU_NAME_RYAKU"].ToString();
                            newRow["SHORI_SPEC"] = row["SHORI_SPEC"].ToString();
                            //newRow["OTHER"] = eRow["OTHER"].ToString();

                            this.betsu4Table.Rows.Add(newRow);
                        }

                        // datasource変更時にイベントが実行されるので、一度イベントをクリアする
                        //if (this.form.tabItakuKeiyakuData.SelectedIndex == 6)
                        if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage7"))
                        {
                            this.form.listBetsu4.CellValidating -= new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu4CellValidating);
                        }

                        this.SetIchiran(this.form.listBetsu4, this.betsu4Table);

                        //if (this.form.tabItakuKeiyakuData.SelectedIndex == 6)
                        if (this.form.tabItakuKeiyakuData.SelectedTab.Name.Equals("tabPage7"))
                        {
                            this.form.listBetsu4.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.ListBetsu4CellValidating);
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetLastSbnPtn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 最終処分場パターン登録
        /// </summary>
        [Transaction]
        public bool SetLastSbnPtn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                PatternRegistForm form = new PatternRegistForm();
                if (form != null)
                {
                    Int64 patternSystemId = 0;
                    Int32 patternSeq = 0;
                    if (!string.IsNullOrWhiteSpace(this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text))
                    {
                        patternSystemId = long.Parse(this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text);
                        patternSeq = int.Parse(this.form.LAST_SHOBUN_PATTERN_SEQ.Text);

                        M_SBNB_PATTERN cond = new M_SBNB_PATTERN();
                        cond.SYSTEM_ID = patternSystemId;
                        cond.SEQ = patternSeq;
                        cond.ROW_NO = 1;
                        cond.LAST_SBN_KBN = 2;
                        cond.ITAKU_KEIYAKU_TYPE = 1;
                        DataTable dt = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile(this.GET_SBNB_PATTERN_SBN_SQL, cond);
                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["PATTERN_FURIGANA"] != null)
                        {
                            form.PATTERN_FURIGANA.Text = dt.Rows[0]["PATTERN_FURIGANA"].ToString();
                        }
                        else
                        {
                            form.PATTERN_FURIGANA.Text = string.Empty;
                        }
                        form.PATTERN_NAME.Text = this.form.LAST_SHOBUN_PATTERN_NAME.Text;
                        form.SystemId = patternSystemId;
                        form.Seq = patternSeq;
                    }
                    else
                    {
                        form.PATTERN_FURIGANA.Text = string.Empty;
                        form.PATTERN_NAME.Text = string.Empty;
                        form.SystemId = 0;
                        form.Seq = 0;
                    }

                    // ダイアログを表示する
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.Text = "最終処分場パターン登録";
                    form.LABEL_TITLE.Text = "最終処分場パターン登録";
                    form.LastSbnKbn = 2;
                    form.ItakuKeiyakuType = 1;
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.OK)
                    {
                        // F5.登録を押下して戻ったときのみ登録処理を実行する
                        patternSystemId = form.SystemId;
                        patternSeq = form.Seq;

                        // 新規の場合、システムIDを採番する
                        if (patternSystemId == 0)
                        {
                            patternSystemId = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetMaxPlusKey();
                            patternSeq = 1;
                        }

                        // 一旦、全データ削除
                        M_SBNB_PATTERN cond = new M_SBNB_PATTERN();
                        cond.SYSTEM_ID = patternSystemId;
                        cond.SEQ = patternSeq;
                        DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile(this.DELETE_SBNB_PATTERN_SBN_SQL, cond);

                        Int16 rowNo = 0;
                        foreach (DataRow row in this.betsu4Table.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_GYOUSHA_CD"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_GYOUSHA_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_GYOUSHA_ADDRESS1"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_GYOUSHA_ADDRESS2"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_CD"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_ADDRESS1"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_ADDRESS2"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_HOUHOU_CD"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHOBUN_HOUHOU_NAME_RYAKU"])) ||
                                !string.IsNullOrWhiteSpace(Convert.ToString(row["SHORI_SPEC"])))
                            {
                                rowNo++;
                                M_SBNB_PATTERN data = new M_SBNB_PATTERN();
                                data.SYSTEM_ID = patternSystemId;
                                data.SEQ = patternSeq;
                                data.ROW_NO = rowNo;
                                data.ITAKU_KEIYAKU_TYPE = 1;
                                data.LAST_SBN_KBN = 2;
                                data.PATTERN_NAME = form.PATTERN_NAME.Text;
                                data.PATTERN_FURIGANA = form.PATTERN_FURIGANA.Text;
                                data.GYOUSHA_CD = Convert.ToString(row["LAST_SHOBUN_GYOUSHA_CD"]);
                                data.GYOUSHA_NAME = Convert.ToString(row["LAST_SHOBUN_GYOUSHA_NAME"]);
                                data.GYOUSHA_ADDRESS1 = Convert.ToString(row["LAST_SHOBUN_GYOUSHA_ADDRESS1"]);
                                data.GYOUSHA_ADDRESS2 = Convert.ToString(row["LAST_SHOBUN_GYOUSHA_ADDRESS2"]);
                                data.GENBA_CD = Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_CD"]);
                                data.GENBA_NAME = Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_NAME"]);
                                data.GENBA_ADDRESS1 = Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_ADDRESS1"]);
                                data.GENBA_ADDRESS2 = Convert.ToString(row["LAST_SHOBUN_JIGYOUJOU_ADDRESS2"]);
                                data.SHOBUN_HOUHOU_CD = Convert.ToString(row["SHOBUN_HOUHOU_CD"]);
                                data.SHORI_SPEC = Convert.ToString(row["SHORI_SPEC"]);
                                //data.OTHER = eRow["OTHER"].ToString();
                                data.DELETE_FLG = false;

                                // 更新者情報設定
                                var dbLogic = new DataBinderLogic<r_framework.Entity.M_SBNB_PATTERN>(data);
                                dbLogic.SetSystemProperty(data, false);
                                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), data);

                                DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().Insert(data);
                            }
                        }

                        this.form.LAST_SHOBUN_PATTERN_SYSTEM_ID.Text = patternSystemId.ToString();
                        this.form.LAST_SHOBUN_PATTERN_SEQ.Text = patternSeq.ToString();
                        this.form.LAST_SHOBUN_PATTERN_NAME.Text = form.PATTERN_NAME.Text;

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("SetLastSbnPtn", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetLastSbnPtn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetLastSbnPtn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        private bool firstFlg = true;

        public bool KOBETSU_SHITEI_CHECK_CheckedChanged(bool check_flag)
        {
            try
            {
                if (!check_flag)
                {
                    this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = false;
                    this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = false;
                    this.form.listKihonHstGenba.Enabled = true;
                    this.form.listKihonHstGenba.ReadOnly = false;
                    this.form.HST_FREE_COMMENT.Visible = true;
                    this.form.HAISHUTSU_JIGYOUJOU_CD.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_TODOUFUKEN_NAME.Text = string.Empty;
                    this.form.GENBA_ADDRESS1.Text = string.Empty;
                    this.form.GENBA_ADDRESS2.Text = string.Empty;
                    if (this.kihonHstGenbaTable != null && this.kihonHstGenbaTable.Rows.Count > 0 && !firstFlg)
                    {
                        this.kihonHstGenbaTable.Rows.Clear();
                    }
                    firstFlg = false;
                }
                else
                {
                    this.form.HAISHUTSU_JIGYOUJOU_CD.Enabled = true;
                    this.form.HAISHUTSU_JIGYOUJOU_SEARCH_BUTTON.Enabled = true;
                    //  this.form.HAISHUTSU_JIGYOUJOU_CD.ReadOnly = false;
                    if (this.kihonHstGenbaTable != null && this.kihonHstGenbaTable.Rows.Count > 0)
                    {
                        this.kihonHstGenbaTable.Rows.Clear();
                    }
                    this.form.HST_FREE_COMMENT.Text = string.Empty;
                    this.form.HST_FREE_COMMENT.Visible = false;

                    /*if (this.kihonHstGenbaTable.Rows.Count > 0)
                    {
                        this.form.listKihonHstGenba.Enabled = true;
                    }
                    else
                    {
                        this.form.listKihonHstGenba.Enabled = false;
                    }*/
                    this.form.listKihonHstGenba.Enabled = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("KOBETSU_SHITEI_CHECK_CheckedChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        /// <summary>
        /// 委託契約運搬タブの制御
        /// </summary>
        /// <param name="windowType"></param>
        public void ListBtu2SeiGyo()
        {
            switch (this.form.ITAKU_KEIYAKU_SHURUI.Text)
            {
                case "1":
                case "3":
                    if (this.betsu2Table == null || this.betsu2Table.Rows.Count == 0)
                    {
                        this.betsu2Table = this.betsu2Dao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU2_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU2());
                        this.betsu2Table.Columns["SYSTEM_ID"].AllowDBNull = true;
                        this.betsu2Table.Columns["SEQ"].AllowDBNull = true;
                        this.betsu2Table.Columns["TIME_STAMP"].AllowDBNull = true;
                        this.betsu2Table.Columns["TIME_STAMP"].Unique = false;
                        this.betsu2Table.Rows.Add();
                        this.form.listBetsu2.DataSource = this.betsu2Table;
                    }
                    this.form.listBetsu2.AllowUserToAddRows = false;
                    break;

                case "2":
                    if (this.betsu2Table == null || this.betsu2Table.Rows.Count == 0)
                    {
                        this.betsu2Table = this.betsu2Dao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU2_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU2());
                        this.betsu2Table.Columns["SYSTEM_ID"].AllowDBNull = true;
                        this.betsu2Table.Columns["SEQ"].AllowDBNull = true;
                        this.betsu2Table.Columns["TIME_STAMP"].AllowDBNull = true;
                        this.betsu2Table.Columns["TIME_STAMP"].Unique = false;
                        this.betsu2Table.Rows.Add();
                        this.form.listBetsu2.DataSource = this.betsu2Table;
                    }
                    else
                    {
                        DataRow dr = this.betsu2Table.Rows[this.betsu2Table.Rows.Count - 1];
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["UNPAN_GYOUSHA_CD"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["UNPAN_GYOUSHA_NAME"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["UNPAN_TODOUFUKEN_NAME"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["UNPAN_GYOUSHA_ADDRESS1"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["UNPAN_GYOUSHA_ADDRESS2"])))
                        {
                            this.betsu2Table.Rows.Add();
                            this.form.listBetsu2.DataSource = this.betsu2Table;
                        }
                    }
                    this.form.listBetsu2.AllowUserToAddRows = false;
                    break;

                default:
                    this.form.listBetsu2.AllowUserToAddRows = true;
                    break;
            }
        }

        /// <summary>
        /// 委託契約運搬タブの制御
        /// </summary>
        /// <param name="windowType"></param>
        public void ListBtu3SeiGyo()
        {
            switch (this.form.ITAKU_KEIYAKU_SHURUI.Text)
            {
                case "2":
                case "3":
                    if (this.betsu3Table == null || this.betsu3Table.Rows.Count == 0)
                    {
                        this.betsu3Table = this.betsu3Dao.GetDataBySqlFile(this.GET_ITAKU_KEIYAKU_BETSU3_STRUCT_SQL, new M_ITAKU_KEIYAKU_BETSU3());
                        this.betsu3Table.Columns["SYSTEM_ID"].AllowDBNull = true;
                        this.betsu3Table.Columns["SEQ"].AllowDBNull = true;
                        this.betsu3Table.Columns["TIME_STAMP"].AllowDBNull = true;
                        this.betsu3Table.Columns["TIME_STAMP"].Unique = false;
                        this.betsu3Table.Rows.Add();
                        this.form.listBetsu3.DataSource = this.betsu3Table;
                    }
                    else
                    {
                        DataRow dr = this.betsu3Table.Rows[this.betsu3Table.Rows.Count - 1];
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_CD"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_NAME"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS1"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS2"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_HOUHOU_CD"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHOBUN_HOUHOU_NAME_RYAKU"])) ||
                            !string.IsNullOrEmpty(Convert.ToString(dr["SHISETSU_CAPACITY"])))
                        {
                            this.betsu3Table.Rows.Add();
                            DataRow row0 = this.betsu3Table.Rows[0];
                            DataRow row = this.betsu3Table.Rows[this.betsu3Table.Rows.Count - 1];
                            row["SHOBUN_GYOUSHA_CD"] = row0["SHOBUN_GYOUSHA_CD"];
                            row["SHOBUN_GYOUSHA_NAME"] = row0["SHOBUN_GYOUSHA_NAME"];
                            row["SHOBUN_GYOUSHA_ADDRESS1"] = row0["SHOBUN_GYOUSHA_ADDRESS1"];
                            row["SHOBUN_GYOUSHA_ADDRESS2"] = row0["SHOBUN_GYOUSHA_ADDRESS2"];
                            row["SHOBUN_GYOUSHA_TODOUFUKEN_NAME"] = row0["SHOBUN_GYOUSHA_TODOUFUKEN_NAME"];
                            this.form.listBetsu3.DataSource = this.betsu3Table;
                        }
                    }
                    this.form.listBetsu3.AllowUserToAddRows = false;
                    break;

                default:
                    this.form.listBetsu3.AllowUserToAddRows = true;
                    break;
            }
        }

        #endregion

        /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.YUUKOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.YUUKOU_END.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrWhiteSpace(this.form.YUUKOU_BEGIN.Text))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(this.form.YUUKOU_END.Text))
                {
                    return false;
                }

                DateTime date_from = Convert.ToDateTime(this.form.YUUKOU_BEGIN.Value);
                DateTime date_to = Convert.ToDateTime(this.form.YUUKOU_END.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    //this.form.YUUKOU_BEGIN.IsInputErrorOccured = true;
                    //this.form.YUUKOU_END.IsInputErrorOccured = true;
                    this.form.YUUKOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    this.form.YUUKOU_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "有効期限From", "有効期限To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.YUUKOU_BEGIN.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion

        /// <summary>
        /// 排出チェック
        /// </summary>
        /// <returns></returns>
        internal bool HstCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                this.form.HAISHUTSU_JIGYOUSHA_CD.BackColor = Constans.NOMAL_COLOR;
                this.form.HAISHUTSU_JIGYOUJOU_CD.BackColor = Constans.NOMAL_COLOR;
                if (string.IsNullOrWhiteSpace(this.form.HAISHUTSU_JIGYOUSHA_CD.Text))
                {
                    this.form.HAISHUTSU_JIGYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E001", "排出事業者CD");
                    this.form.tabItakuKeiyakuData.SelectedTab = this.form.tabPage1;
                    this.form.HAISHUTSU_JIGYOUSHA_CD.Focus();
                    return true;
                }
                //else
                //{
                //    if (string.IsNullOrWhiteSpace(this.form.HAISHUTSU_JIGYOUJOU_CD.Text) && string.IsNullOrWhiteSpace(this.form.HST_FREE_COMMENT.Text))
                //    {
                //        this.form.tabItakuKeiyakuData.SelectedTab = this.form.tabPage1;
                //        this.form.HAISHUTSU_JIGYOUJOU_CD.BackColor = Constans.ERROR_COLOR;
                //        msgLogic.MessageBoxShow("E001", "排出事業場CD");
                //        this.form.HAISHUTSU_JIGYOUJOU_CD.Focus();
                //        return true;
                //    }
                //}
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HstCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region YUUKOU_BEGIN_Leaveイベント

        /// <summary>
        /// YUUKOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void YUUKOU_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.YUUKOU_END.Text))
            {
                //this.form.YUUKOU_END.IsInputErrorOccured = false;
                this.form.YUUKOU_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region YUUKOU_END_Leaveイベント

        /// <summary>
        /// YUUKOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void YUUKOU_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.YUUKOU_BEGIN.Text))
            {
                //this.form.YUUKOU_BEGIN.IsInputErrorOccured = false;
                this.form.YUUKOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region UPN日付チェック

        /// <summary>
        /// UPN日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool UPNDateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.UPNKYOKA_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.UPNKYOKA_END.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrWhiteSpace(this.form.UPNKYOKA_BEGIN.Text))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(this.form.UPNKYOKA_END.Text))
                {
                    return false;
                }

                DateTime date_from = Convert.ToDateTime(this.form.UPNKYOKA_BEGIN.Value);
                DateTime date_to = Convert.ToDateTime(this.form.UPNKYOKA_END.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    //this.form.UPNKYOKA_BEGIN.IsInputErrorOccured = true;
                    //this.form.UPNKYOKA_END.IsInputErrorOccured = true;
                    this.form.UPNKYOKA_BEGIN.BackColor = Constans.ERROR_COLOR;
                    this.form.UPNKYOKA_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "期限From", "期限To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.UPNKYOKA_BEGIN.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UPNDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region UPNKYOKA_BEGIN_Leaveイベント

        /// <summary>
        /// UPNKYOKA_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void UPNKYOKA_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.UPNKYOKA_END.Text))
            {
                //this.form.UPNKYOKA_END.IsInputErrorOccured = false;
                this.form.UPNKYOKA_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region UPNKYOKA_END_Leaveイベント

        /// <summary>
        /// UPNKYOKA_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void UPNKYOKA_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.UPNKYOKA_BEGIN.Text))
            {
                //this.form.UPNKYOKA_BEGIN.IsInputErrorOccured = false;
                this.form.UPNKYOKA_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region SBN日付チェック

        /// <summary>
        /// SBN日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool SBNDateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.SBNKYOKA_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.SBNKYOKA_END.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrWhiteSpace(this.form.SBNKYOKA_BEGIN.Text))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(this.form.SBNKYOKA_END.Text))
                {
                    return false;
                }

                DateTime date_from = Convert.ToDateTime(this.form.SBNKYOKA_BEGIN.Value);
                DateTime date_to = Convert.ToDateTime(this.form.SBNKYOKA_END.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    //this.form.SBNKYOKA_BEGIN.IsInputErrorOccured = true;
                    //this.form.SBNKYOKA_END.IsInputErrorOccured = true;
                    this.form.SBNKYOKA_BEGIN.BackColor = Constans.ERROR_COLOR;
                    this.form.SBNKYOKA_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "期限From", "期限To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.SBNKYOKA_BEGIN.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SBNDateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region SBNKYOKA_BEGIN_Leaveイベント

        /// <summary>
        /// SBNKYOKA_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SBNKYOKA_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.SBNKYOKA_END.Text))
            {
                //this.form.SBNKYOKA_END.IsInputErrorOccured = false;
                this.form.SBNKYOKA_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region SBNKYOKA_END_Leaveイベント

        /// <summary>
        /// SBNKYOKA_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void SBNKYOKA_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.SBNKYOKA_BEGIN.Text))
            {
                //this.form.SBNKYOKA_BEGIN.IsInputErrorOccured = false;
                this.form.SBNKYOKA_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion
        /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end

        #region UPNKYOKA_GYOUSHA_CD_Validatingイベント

        internal bool UPNKYOKA_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string gyoushaCd = this.form.UPNKYOKA_GYOUSHA_CD.Text;
                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    this.form.UPNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = gyoushaCd;
                    // 20151021 BUNN #12040 STR
                    gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                    // 20151021 BUNN #12040 END
                    M_GYOUSHA[] gyoushas = this.gyoushaDao.GetAllValidData(gyousha);
                    if (gyoushas != null && gyoushas.Length > 0)
                    {
                        this.form.UPNKYOKA_GYOUSHA_NAME_RYAKU.Text = gyoushas[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.UPNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;

                        var textBox = this.form.UPNKYOKA_GYOUSHA_CD as TextBox;
                        if (textBox != null)
                        {
                            textBox.SelectAll();
                        }

                        if (this.errorCancelFlg)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }

                        this.isError = true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("UPNKYOKA_GYOUSHA_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region UPNKYOKA_CHIIKI_CD_Validatingイベント

        internal bool UPNKYOKA_CHIIKI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string chiikiCd = this.form.UPNKYOKA_CHIIKI_CD.Text;
                if (string.IsNullOrEmpty(chiikiCd))
                {
                    this.form.UPNKYOKA_CHIIKI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    M_CHIIKI chiiki = new M_CHIIKI();
                    chiiki.CHIIKI_CD = chiikiCd;
                    M_CHIIKI[] chiikis = this.chiikiDao.GetAllValidData(chiiki);
                    if (chiikis != null && chiikis.Length > 0)
                    {
                        this.form.UPNKYOKA_CHIIKI_NAME_RYAKU.Text = chiikis[0].CHIIKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.UPNKYOKA_CHIIKI_NAME_RYAKU.Text = string.Empty;

                        var textBox = this.form.UPNKYOKA_CHIIKI_CD as TextBox;
                        if (textBox != null)
                        {
                            textBox.SelectAll();
                        }

                        if (this.errorCancelFlg)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "地域");
                            e.Cancel = true;
                        }

                        this.isError = true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("UPNKYOKA_CHIIKI_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region SBNKYOKA_GYOUSHA_CD_Validatingイベント

        internal bool SBNKYOKA_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string gyoushaCd = this.form.SBNKYOKA_GYOUSHA_CD.Text;
                if (!this.isError && gyoushaCd == this.form.PreviousValue)
                {
                    return false;
                }
                this.isError = false;
                if (!string.IsNullOrEmpty(gyoushaCd))
                {
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = gyoushaCd;
                    // 20151021 BUNN #12040 STR
                    gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                    // 20151021 BUNN #12040 END
                    M_GYOUSHA[] gyoushas = this.gyoushaDao.GetAllValidData(gyousha);
                    if (gyoushas != null && gyoushas.Length > 0)
                    {
                        this.form.SBNKYOKA_GYOUSHA_NAME_RYAKU.Text = gyoushas[0].GYOUSHA_NAME_RYAKU;
                        this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
                        this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SBNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;

                        var textBox = this.form.SBNKYOKA_GYOUSHA_CD as TextBox;
                        if (textBox != null)
                        {
                            textBox.SelectAll();
                        }

                        if (this.errorCancelFlg)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }

                        this.isError = true;
                    }
                }
                else
                {
                    this.form.SBNKYOKA_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    this.form.SBNKYOKA_GENBA_CD.Text = string.Empty;
                    this.form.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;
                }
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("SBNKYOKA_GYOUSHA_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region SBNKYOKA_CHIIKI_CD_Validatingイベント

        internal bool SBNKYOKA_CHIIKI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string chiikiCd = this.form.SBNKYOKA_CHIIKI_CD.Text;
                if (string.IsNullOrEmpty(chiikiCd))
                {
                    this.form.SBNKYOKA_CHIIKI_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    M_CHIIKI chiiki = new M_CHIIKI();
                    chiiki.CHIIKI_CD = chiikiCd;
                    M_CHIIKI[] chiikis = this.chiikiDao.GetAllValidData(chiiki);
                    if (chiikis != null && chiikis.Length > 0)
                    {
                        this.form.SBNKYOKA_CHIIKI_NAME_RYAKU.Text = chiikis[0].CHIIKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.SBNKYOKA_CHIIKI_NAME_RYAKU.Text = string.Empty;

                        var textBox = this.form.SBNKYOKA_CHIIKI_CD as TextBox;
                        if (textBox != null)
                        {
                            textBox.SelectAll();
                        }

                        if (this.errorCancelFlg)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "地域");
                            e.Cancel = true;
                        }

                        this.isError = true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.isError = true;
                LogUtility.Error("SBNKYOKA_CHIIKI_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        /// <summary>
        /// 品名CD PopupAfter処理
        /// </summary>
        public bool PopupAfterHinmei()
        {
            try
            {
                LogUtility.DebugMethodStart();
                GcMultiRow list = this.form.listHinmei;
                string hinmeiCd = Convert.ToString(list[list.CurrentCell.RowIndex, "HINMEI_CD"].Value);
                // 入力値がクリアされていた場合、関連項目をクリアする
                if (string.IsNullOrEmpty(hinmeiCd))
                {
                    list[list.CurrentCell.RowIndex, "HINMEI_NAME"].Value = string.Empty;
                }
                else
                {
                    // コードから品名をセットする
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = hinmeiCd;
                    M_HINMEI[] hinmeis = this.imHinmeiDao.GetAllValidData(hinmei);
                    if (hinmeis != null && hinmeis.Length > 0)
                    {
                        list[list.CurrentCell.RowIndex, "HINMEI_NAME"].Value = hinmeis[0].HINMEI_NAME;
                    }
                    else
                    {
                        list[list.CurrentCell.RowIndex, "HINMEI_NAME"].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 基本情報タブのセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool ListKihonHstGenbaCellEnter(object sender, CellEventArgs e)
        {
            try
            {
                if (e.CellName.Equals("GENBA_HAISHUTSU_JIGYOUJOU_CD"))
                {
                    this.form.PreviousCd = Convert.ToString(this.form.listKihonHstGenba[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value);
                    this.form.PreviousName = Convert.ToString(this.form.listKihonHstGenba[e.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_NAME"].Value);
                    isSeted = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListKihonHstGenbaCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 処分タブのセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool ListBetsu3HstCellEnter(object sender, CellEventArgs e)
        {
            try
            {
                if (e.CellName.Equals("SHOBUN_HOUHOU_CD"))
                {
                    this.form.PreviousCd = Convert.ToString(this.form.listBetsu3[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value);
                    this.form.PreviousName = Convert.ToString(this.form.listBetsu3[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListBetsu3HstCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 最終タブのセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool ListBetsu4HstCellEnter(object sender, CellEventArgs e)
        {
            try
            {
                switch (e.CellName)
                {
                    case ("SHOBUN_HOUHOU_CD"):
                        if (e.CellName.Equals("SHOBUN_HOUHOU_CD"))
                        {
                            this.form.PreviousCd = Convert.ToString(this.form.listBetsu4[e.RowIndex, "SHOBUN_HOUHOU_CD"].Value);
                            this.form.PreviousName = Convert.ToString(this.form.listBetsu4[e.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value);
                        }
                        break;

                    case ("LAST_SHOBUN_GYOUSHA_CD"):
                        if (e.CellName.Equals("LAST_SHOBUN_GYOUSHA_CD"))
                        {
                            this.form.PreviousCd = Convert.ToString(this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value);
                            this.form.PreviousName = Convert.ToString(this.form.listBetsu4[e.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"].Value);
                        }
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListBetsu4HstCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        // VUNGUYEN 20150525 #1294 START
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YUUKOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!string.IsNullOrEmpty(this.form.YUUKOU_BEGIN.Text))
            {
                this.form.YUUKOU_END.Text = this.form.YUUKOU_BEGIN.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UPNKYOKA_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!string.IsNullOrEmpty(this.form.UPNKYOKA_BEGIN.Text))
            {
                this.form.UPNKYOKA_END.Text = this.form.UPNKYOKA_BEGIN.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBNKYOKA_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!string.IsNullOrEmpty(this.form.SBNKYOKA_BEGIN.Text))
            {
                this.form.SBNKYOKA_END.Text = this.form.SBNKYOKA_BEGIN.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        public bool UPNKYOKA_CheckDateRelation()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.UPNKYOKA_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.UPNKYOKA_END.BackColor = Constans.NOMAL_COLOR;

                DateTime date_from;
                DateTime date_to;

                if (DateTime.TryParse(this.form.UPNKYOKA_BEGIN.Text, out date_from)
                    && DateTime.TryParse(this.form.UPNKYOKA_END.Text, out date_to))
                {
                    // 日付FROM > 日付TO 場合
                    if (date_to.CompareTo(date_from) < 0)
                    {
                        this.form.UPNKYOKA_BEGIN.BackColor = Constans.ERROR_COLOR;
                        this.form.UPNKYOKA_END.BackColor = Constans.ERROR_COLOR;
                        string[] errorMsg = { "期限開始", "期限終了" };
                        msgLogic.MessageBoxShow("E030", errorMsg);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UPNKYOKA_CheckDateRelation", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        public bool SBNKYOKA_CheckDateRelation()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.SBNKYOKA_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.SBNKYOKA_END.BackColor = Constans.NOMAL_COLOR;

                DateTime date_from;
                DateTime date_to;

                if (DateTime.TryParse(this.form.SBNKYOKA_BEGIN.Text, out date_from)
                    && DateTime.TryParse(this.form.SBNKYOKA_END.Text, out date_to))
                {
                    // 日付FROM > 日付TO 場合
                    if (date_to.CompareTo(date_from) < 0)
                    {
                        this.form.SBNKYOKA_BEGIN.BackColor = Constans.ERROR_COLOR;
                        this.form.SBNKYOKA_END.BackColor = Constans.ERROR_COLOR;
                        string[] errorMsg = { "期限開始", "期限終了" };
                        msgLogic.MessageBoxShow("E030", errorMsg);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SBNKYOKA_CheckDateRelation", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 登録データをチェックする。
        /// </summary>
        public bool CheckRegistData()
        {
            try
            {
                string message = "";
                bool isError = false;

                if (string.IsNullOrEmpty(this.form.KEIYAKUSHO_CREATE_DATE.Text))
                {
                    message = message + "作成日は必須項目です。入力してください。" + "\n";
                    this.form.KEIYAKUSHO_CREATE_DATE.BackColor = Constans.ERROR_COLOR;
                    isError = true;
                }

                if (string.IsNullOrEmpty(this.form.YUUKOU_BEGIN.Text))
                {
                    message = message + "有効期限開始は必須項目です。入力してください。" + "\n";
                    this.form.YUUKOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    isError = true;
                }

                if (isError)
                {
                    MessageBox.Show(message, Constans.ERROR_MESSAGE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return isError;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegistData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 電子契約データをチェックする。
        /// </summary>
        /// <returns></returns>
        internal bool CheckDenshiKeiyakuData()
        {
            bool keiyakuNoErr = false;
            bool accessCodeErr = false;
            bool yuusenErr = false;
            bool shainNameErr = false;
            bool addressErr = false;
            bool existEmail = false;
            string message = "";

            // 必須項目チェック
            // 契約番号
            if (String.IsNullOrEmpty(this.form.ITAKU_KEIYAKU_NO.Text))
            {
                keiyakuNoErr = true;
            }

            // アクセスコード（システム設定の電子契約アクセスコード = 1の場合）
            if (!this.sysInfoEntity.DENSHI_KEIYAKU_ACCESS_CODE_CHECK.IsNull
                && this.sysInfoEntity.DENSHI_KEIYAKU_ACCESS_CODE_CHECK == 1)
            {
                if (string.IsNullOrEmpty(this.form.ACCESS_CD.Text))
                {
                    accessCodeErr = true;
                }
            }

            foreach (DataGridViewRow row in this.form.keiroIchiran.Rows)
            {
                if (row.IsNewRow
                    || this.IsAllNullOrEmpty(row)
                    || (row.Cells["chb_delete"].Value != null && bool.Parse(row.Cells["chb_delete"].Value.ToString())))
                {
                    continue;
                }
                // 優先
                if (row.Cells["YUUSEN_NO"].Value == null)
                {
                    yuusenErr = true;
                }
                // 社員名
                if (row.Cells["SHAIN_NAME"].Value == null)
                {
                    shainNameErr = true;
                }
                // メールアドレス
                if (row.Cells["MAIL_ADDRESS"].Value == null)
                {
                    addressErr = true;
                }
                else
                {
                    existEmail = true;
                }
            }
            if (keiyakuNoErr)
            {
                message = message + "契約番号は必須項目です。入力してください。\n";
            }
            if (yuusenErr)
            {
                //message = message + "優先は必須項目です。入力してください。\n";
                message = message + "送付順は必須項目です。入力してください。\n";
            }
            if (shainNameErr)
            {
                message = message + "社員名は必須項目です。入力してください。\n";
            }
            if (addressErr)
            {
                message = message + "メールアドレスは必須項目です。入力してください。\n";
            }
            if (!string.IsNullOrEmpty(message))
            {
                this.form.errmessage.MessageBoxShowError(message);
                return true;
            }

            // 順序チェック
            var myTable = new Dictionary<int, string>();
            foreach (DataGridViewRow row in this.form.keiroIchiran.Rows)
            {
                if (!row.IsNewRow)
                {
                    if ((row.Cells["chb_delete"].Value == null || !bool.Parse(row.Cells["chb_delete"].Value.ToString()))
                        && !this.IsAllNullOrEmpty(row))
                    {
                        int number = int.Parse(row.Cells["YUUSEN_NO"].Value.ToString());
                        string shainCD = "";
                        if (row.Cells["SHAIN_CD"].Value != null)
                        {
                            shainCD = row.Cells["YUUSEN_NO"].Value.ToString();
                        }

                        // 同一の優先順位が設定されている場合はエラーとする。
                        if (myTable.ContainsKey(number))
                        {
                            //this.form.errmessage.MessageBoxShowError("同一の優先順位は設定できません。");
                            this.form.errmessage.MessageBoxShowError("同一の送付順は設定できません。");
                            return true;
                        }

                        // 優先と社員CDを格納する。
                        myTable.Add(number, shainCD);
                    }
                }
            }

            //if (myTable.Count > 0)
            //{
            //    // 優先の降順に並べる。
            //    var newTable = myTable.OrderByDescending(x => x.Key);
            //    KeyValuePair<int, string>[] datas = newTable.ToArray();
            //    // 降順に並べた要素の最初の社員CDで判定する。
            //    if (!string.IsNullOrEmpty(datas[0].Value))
            //    {
            //        this.form.errmessage.MessageBoxShowError("優先順位の最後には、社外の担当者を設定してください。");
            //        return true;
            //    }
            //}

            // 送付先（社外契約）
            foreach (DataGridViewRow row in this.form.keiroIchiran2.Rows)
            {
                if (row.IsNewRow
                    || this.IsAllNullOrEmptyKeiyakusaki(row)
                    || (row.Cells["chb_delete_keiyakusaki"].Value != null && bool.Parse(row.Cells["chb_delete_keiyakusaki"].Value.ToString())))
                {
                    continue;
                }
                // 送付順
                if (row.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value == null)
                {
                    yuusenErr = true;
                }
                // 担当者名
                if (row.Cells["KEIYAKUSAKI_SHAIN_NAME"].Value == null)
                {
                    shainNameErr = true;
                }
                // メールアドレス
                if (row.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value == null)
                {
                    addressErr = true;
                }
                else
                {
                    existEmail = true;
                }
            }
            if (yuusenErr)
            {
                message = message + "社外契約経路ー送付順は必須項目です。入力してください。\n";
            }
            if (shainNameErr)
            {
                message = message + "社外契約経路ー氏名は必須項目です。入力してください。\n";
            }
            if (addressErr)
            {
                message = message + "社外契約経路ーメールアドレスは必須項目です。入力してください。\n";
            }
            if (accessCodeErr && existEmail)
            {
                message = message + "アクセスコードは必須項目です。入力してください。\n";
            }
            if (!string.IsNullOrEmpty(message))
            {
                this.form.errmessage.MessageBoxShowError(message);
                return true;
            }
            
            // 順序チェック
            var myList = new List<int>();
            foreach (DataGridViewRow row in this.form.keiroIchiran2.Rows)
            {
                if (!row.IsNewRow)
                {
                    if ((row.Cells["chb_delete_keiyakusaki"].Value == null || !bool.Parse(row.Cells["chb_delete_keiyakusaki"].Value.ToString()))
                        && !this.IsAllNullOrEmptyKeiyakusaki(row))
                        {
                        int number = int.Parse(row.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString());

                        // 同一の優先順位が設定されている場合はエラーとする。
                        if (myList.Contains(number))
                        {
                            this.form.errmessage.MessageBoxShowError("同一の送付順は設定できません。");
                            return true;
                        }
                        // 送付順を格納する。
                        myList.Add(number);
                    }
                }
            }

            // 社内に有効行があり、社外に有効行がない場合エラーとする。
            if (myTable.Count > 0 && myList.Count < 1)
            {
                this.form.errmessage.MessageBoxShowError("契約先の担当者を１名以上設定してください。");
                return true;
            }

            // メールアドレスの妥当性チェック
            if (this.MailAddressCheck())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// メールアドレスのチェック処理
        /// </summary>
        internal bool MailAddressCheck()
        {
            if (this.form.keiroIchiran.RowCount == 0)
            {
                return false;
            }

            HashSet<string> hashAddList = new HashSet<string>();
            int yuukouRowCount = 0;
            string totalAddress = "";

            foreach (DataGridViewRow row in this.form.keiroIchiran.Rows)
            {
                // 未確定行、全て空の行、削除がONの行は対象外にする。
                if (row.IsNewRow
                    || this.IsAllNullOrEmpty(row)
                    || (row.Cells["chb_delete"].Value != null && bool.Parse(row.Cells["chb_delete"].Value.ToString())))
                {
                    continue;
                }
                
                // 妥当性チェック
                bool ret = this.IsValidMailAddress(row.Cells["MAIL_ADDRESS"].Value.ToString());
                if (!ret)
                {
                    this.form.errmessage.MessageBoxShowError("メールアドレスに必要な形式ではありません。");
                    return true;
                }

                hashAddList.Add(row.Cells["MAIL_ADDRESS"].Value.ToString());
                yuukouRowCount++;

                // 送付先のアドレスを連結する。
                totalAddress = totalAddress + row.Cells["MAIL_ADDRESS"].Value.ToString();

            }

            //社外契約先
            foreach (DataGridViewRow row in this.form.keiroIchiran2.Rows)
            {
                // 未確定行、全て空の行、削除がONの行は対象外にする。
                if (row.IsNewRow
                    || this.IsAllNullOrEmptyKeiyakusaki(row)
                    || (row.Cells["chb_delete_keiyakusaki"].Value != null && bool.Parse(row.Cells["chb_delete_keiyakusaki"].Value.ToString())))
                {
                    continue;
                }

                // 妥当性チェック
                bool ret = this.IsValidMailAddress(row.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value.ToString());
                if (!ret)
                {
                    this.form.errmessage.MessageBoxShowError("メールアドレスに必要な形式ではありません。");
                    return true;
                }

                hashAddList.Add(row.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value.ToString());
                yuukouRowCount++;

                // 送付先のアドレスを連結する。
                totalAddress = totalAddress + row.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value.ToString();
            }

            // 一覧の件数と重複を取り除いた件数が異なっている場合は、重複ありとしてエラーとする。
            if (yuukouRowCount != hashAddList.Count)
            {
                this.form.errmessage.MessageBoxShowError("経路一覧内に同一のメールアドレスが設定されています。");
                return true;
            }

            // 送付先のアドレスを連結した長さが「254」を超えている場合は、エラーとする。
            if (totalAddress.Length > 254)
            {
                this.form.errmessage.MessageBoxShowError("電子契約書の送付先件数が多いため、送付が行えません。送付先を減らしてください。");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 有効行であるか判断する。
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsAllNullOrEmpty(DataGridViewRow row)
        {
            if (row.Cells["YUUSEN_NO"].Value == null
                && row.Cells["SHAIN_CD"].Value == null
                && (row.Cells["SHAIN_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["SHAIN_NAME"].Value.ToString()))
                && row.Cells["MAIL_ADDRESS"].Value == null
                && (row.Cells["TEL_NO"].Value == null || string.IsNullOrEmpty(row.Cells["TEL_NO"].Value.ToString()))
                && row.Cells["ATESAKI_NAME"].Value == null
                && row.Cells["BUSHO_NAME"].Value == null
                && row.Cells["SOUHUSAKI_BIKO"].Value == null
                )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 有効行であるか判断する。（社外契約先）
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsAllNullOrEmptyKeiyakusaki(DataGridViewRow row)
        {
            if (row.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value == null
                && (row.Cells["KEIYAKUSAKI_CORP_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KEIYAKUSAKI_CORP_NAME"].Value.ToString()))
                && (row.Cells["KEIYAKUSAKI_SHAIN_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KEIYAKUSAKI_SHAIN_NAME"].Value.ToString()))
                && row.Cells["KEIYAKUSAKI_MAIL_ADDRESS"].Value == null
                )
            {
                return true;
            }

            return false;
        }

        private DataTable SearchResult;

        /// <summary>
        /// 社内経路CD Validatedイベント
        /// </summary>
        internal void ShanaiKeiroCDValidated()
        {
            // 一覧をクリアする。
            this.form.keiroIchiran.Rows.Clear();

            if (string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
            {
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty; 
                return;
            }

            // 初期化
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty;
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.ReadOnly = true;
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Tag = string.Empty;
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.TabStop = false;

            // 社内経路名取得
            M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME param = new M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME();
            param.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = short.Parse(this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
            M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME[] keiro = DaoInitUtility.GetComponent<IM_DENSHI_KEIYAKU_SHANAI_KEIRO_NAMEDao>().GetAllValidData(param);

            // 存在チェック
            if (keiro == null || keiro.Length < 1)
            {
                // 存在しない
                this.form.errmessage.MessageBoxShow("E020", "社内経路名（電子）");
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Focus();
                return;
            }

            // 経路名を表示
            this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = keiro[0].DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;

            // 社内経路情報を取得する。
            this.getShanaiKeiroInfo();

            // 取得した社内経路情報を一覧に設定する。
            if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
            {
                for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                {
                    DataRow row = SearchResult.Rows[i];
                    this.form.keiroIchiran.Rows.Add();
                    this.form.keiroIchiran.Rows[i].Cells["YUUSEN_NO"].Value = i + 1;
                    this.form.keiroIchiran.Rows[i].Cells["CORP_NAME"].Value = corpName; 
                    this.form.keiroIchiran.Rows[i].Cells["SHAIN_CD"].Value = row["SHAIN_CD"].ToString();
                    this.form.keiroIchiran.Rows[i].Cells["SHAIN_NAME"].Value = row["SHAIN_NAME_RYAKU"].ToString();

                    if (row["MAIL_ADDRESS"] != null)
                    {
                        if (row["MAIL_ADDRESS"].ToString().Contains(','))
                        {
                            string[] addDatas = row["MAIL_ADDRESS"].ToString().Split(',');
                            this.form.keiroIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value = addDatas[0];
                        }
                        else
                        {
                            this.form.keiroIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value = row["MAIL_ADDRESS"].ToString();
                        }
                    }

                    // 社員名、メールアドレスを読み取り専用にする。
                    this.form.keiroIchiran.Rows[i].Cells["CORP_NAME"].ReadOnly = true; 
                    this.form.keiroIchiran.Rows[i].Cells["SHAIN_NAME"].ReadOnly = true;
                    this.form.keiroIchiran.Rows[i].Cells["MAIL_ADDRESS"].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 社内経路情報を取得する。
        /// </summary>
        private void getShanaiKeiroInfo()
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" keiro.SHAIN_CD, ");
            sql.Append(" shain.SHAIN_NAME_RYAKU, ");
            sql.Append(" shain.MAIL_ADDRESS ");
            sql.Append(" FROM M_DENSHI_KEIYAKU_SHANAI_KEIRO keiro");
            sql.Append(" LEFT JOIN M_SHAIN shain");
            sql.Append(" ON keiro.SHAIN_CD = shain.SHAIN_CD");
            sql.Append(" WHERE ");
            sql.AppendFormat(" keiro.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = {0} ", this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text);
            sql.Append(" AND keiro.DELETE_FLG = 0 ");

            this.SearchResult = this.shanaiKeiroDao.GetDateForStringSql(sql.ToString());
        }

        private object PreviousShainCd = "";
        private object PreviousShainName = "";

        /// <summary>
        /// 一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void CellEnter(int columnIndex)
        {
            string cellName = this.form.keiroIchiran.Columns[columnIndex].Name;

            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }

            // 社員CD、社員名を保持
            if (cellName.Equals("SHAIN_CD"))
            {
                this.PreviousShainCd = this.form.keiroIchiran.CurrentRow.Cells["SHAIN_CD"].Value;
                this.PreviousShainName = this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].Value;
            }

            // IME制御
            switch (cellName)
            {
                case "chb_delete":
                case "YUUSEN_NO":
                case "SHAIN_CD":
                case "MAIL_ADDRESS":
                case "TEL_NO":
                    this.form.keiroIchiran.ImeMode = ImeMode.Disable;
                    break;

                default:
                    this.form.keiroIchiran.ImeMode = ImeMode.Hiragana;
                    break;
            }
        }

        /// <summary>
        /// 社員CD Validatingイベント
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void ShainCDValidating(int columnIndex)
        {
            string cellName = this.form.keiroIchiran.Columns[columnIndex].Name;

            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }

            if (cellName.Equals("SHAIN_CD"))
            {
                this.SetShainInfo();
            }
        }

        /// <summary>
        /// 社員名、メールアドレスの制御処理
        /// </summary>
        internal void SetShainInfo()
        {
            if (this.form.keiroIchiran.CurrentRow.Cells["SHAIN_CD"].Value == null)
            {
                if (this.form.keiroIchiran.CurrentRow.Cells["SHAIN_CD"].Value != this.PreviousShainCd)
                {
                    // メールアドレスをクリアする。
                    this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].Value = null;
                    this.form.keiroIchiran.CurrentRow.Cells["CORP_NAME"].Value = null;

                    // 社員名、メールアドレスを読み取り専用を解除する。
                    this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].ReadOnly = false;
                    this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].ReadOnly = false;
                    this.form.keiroIchiran.CurrentRow.Cells["CORP_NAME"].ReadOnly = false;
                }
                else
                {
                    this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].Value = this.PreviousShainName;
                }
            }
            else
            {
                // 入力された社員CDをもとに略称とメールアドレスを取得する。
                M_SHAIN shainData = this.shainDao.GetDataByCd(this.form.keiroIchiran.CurrentRow.Cells["SHAIN_CD"].Value.ToString());
                if (shainData != null)
                {
                    string address = shainData.MAIL_ADDRESS;

                    if (address == null || string.IsNullOrEmpty(address))
                    {
                        this.form.errmessage.MessageBoxShowError("メールアドレスが設定されていない社員です。");

                        // 社員CD、社員名、メールアドレスをクリアする。
                        this.form.keiroIchiran.CurrentRow.Cells["SHAIN_CD"].Value = null;
                        this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].Value = null;
                        this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].Value = null;
                        this.form.keiroIchiran.CurrentRow.Cells["CORP_NAME"].Value = null;

                        // 社員名、メールアドレスを読み取り専用を解除する。
                        this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].ReadOnly = false;
                        this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].ReadOnly = false;
                        this.form.keiroIchiran.CurrentRow.Cells["CORP_NAME"].ReadOnly = false;

                        return;
                    }

                    // 略称を設定する。
                    this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].Value = shainData.SHAIN_NAME_RYAKU;

                    // カンマが含まれている場合、カンマ区切りで分割し、１件目を取得して設定する。
                    if (address.Contains(","))
                    {
                        string[] addDatas = address.Split(',');
                        this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].Value = addDatas[0];
                    }
                    else
                    {
                        this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].Value = address;
                    }

                    // 社員名、メールアドレスを読み取り専用にする。
                    this.form.keiroIchiran.CurrentRow.Cells["SHAIN_NAME"].ReadOnly = true;
                    this.form.keiroIchiran.CurrentRow.Cells["MAIL_ADDRESS"].ReadOnly = true;

                    // 表示している送付順の最大値+1
                    var myList = new List<int>();
                    foreach (DataGridViewRow row in this.form.keiroIchiran.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if (row.Cells["YUUSEN_NO"].Value != null && !(row.Cells["YUUSEN_NO"].Value.Equals(string.Empty)))
                            {
                                int number = int.Parse(row.Cells["YUUSEN_NO"].Value.ToString());
                                myList.Add(number);
                            }
                        }
                    }

                    if (myList.Count < 1)
                    {
                        // 1件もなければ1
                        this.form.keiroIchiran.CurrentRow.Cells["YUUSEN_NO"].Value = 1;
                    }
                    else
                    {
                        // 降順に並び替え、最大値+1
                        myList.Sort();
                        myList.Reverse();
                        if (this.form.keiroIchiran.CurrentRow.Cells["YUUSEN_NO"].Value == null || this.form.keiroIchiran.CurrentRow.Cells["YUUSEN_NO"].Value.Equals(string.Empty))
                        {
                            this.form.keiroIchiran.CurrentRow.Cells["YUUSEN_NO"].Value = myList[0] + 1;
                        }
                    }
                    this.form.keiroIchiran.CurrentRow.Cells["CORP_NAME"].Value = this.corpName;
                    this.form.keiroIchiran.CurrentRow.Cells["CORP_NAME"].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 指定された文字列がメールアドレスとして正しい形式か検証する
        /// </summary>
        /// <param name="address">検証する文字列</param>
        /// <returns>正しい時はTrue。正しくない時はFalse。</returns>
        internal bool IsValidMailAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            try
            {
                MailAddress mail = new MailAddress(address);
            }
            catch (FormatException)
            {
                //FormatExceptionがスローされた時は、正しくない
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルアップロードボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            // ユーザ定義情報を取得
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            // ファイルアップロード参照先のフォルダを取得
            string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

            // ファイルアップロード用DB接続を確立
            if (!this.uploadLogic.CanConnectDB())
            {
                this.form.errmessage.MessageBoxShowError("ファイルアップロード用DBに接続できませんでした。\n接続情報を確認してください。");
            }
            // システム個別設定入力の初期フォルダの設定有無をチェックする。
            else if (string.IsNullOrEmpty(serverPath) || !Directory.Exists(serverPath))
            {
                this.form.errmessage.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
            }
            else
            {
                List<long> fileIdList = null;
                // ファイルアップロード画面に渡すシステムID
                string[] paramList = new string[2];

                // 新規モードの場合
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    if (this.form.errmessage.MessageBoxShowConfirm("ファイルアップロードの事前処理として登録処理を行います。よろしいですか？", MessageBoxDefaultButton.Button1)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        // 委託契約書の登録処理を行う。
                        this.form.RegistErrorFlag = false;
                        this.form.Regist(sender, e);

                        if (this.systemIdForUpload == null)
                        {
                            return;
                        }

                        //排出事業者にフォーカスがあると、ファイルアップロードPOPを閉じた後に排出事業場がクリアされてしまう為。
                        this.form.ITAKU_KEIYAKU_NO.Focus();

                        this.SystemId = this.systemIdForUpload;

                        //個別指定がONのデータだと、基本情報の明細がクリアされてしまう為。
                        firstFlg = true;

                        this.SetWindowData();

                        this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

                        if (this.isFileUploadOK)
                        {
                            // ファイルアップロード画面を起動
                            paramList[0] = this.systemIdForUpload;
                            paramList[1] = this.form.ITAKU_KEIYAKU_FILE_PATH.Text;
                            FormManager.OpenFormModal("G730", fileIdList, WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI, paramList);
                            this.isFileUploadOK = false;
                        }
                    }
                }
                // 修正モードの場合
                else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 委託契約書データからファイルIDを取得する。
                    var fileLink = fileLinkItakuKeiyakuKihonDao.GetDataBySystemId(this.dto.ItakuKeiyakuKihon.SYSTEM_ID);
                    if (fileLink != null)
                    {
                        fileIdList = fileLink.Select(n => n.FILE_ID.Value).ToList();
                    }

                    // ファイルアップロード画面を起動
                    paramList[0] = this.form.SYSTEM_ID.Text.ToString();
                    paramList[1] = this.form.ITAKU_KEIYAKU_FILE_PATH.Text;
                    FormManager.OpenFormModal("G730", fileIdList, WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI, paramList);
                    this.isFileUploadOK = false;
                }
            }
        }

        /// <summary>
        /// ファイルアップロード連携情報が存在するか
        /// </summary>
        /// <returns>true:有 false:無</returns>
        internal bool ExistFileLinkItakuKeiyakuKihon()
        {
            var list = fileLinkItakuKeiyakuKihonDao.GetDataBySystemId(this.dto.ItakuKeiyakuKihon.SYSTEM_ID);
            if (list != null && 0 < list.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// オプション対応時にファイルアップロードボタンを表示
        /// </summary>
        private void FileUploadButtonSetting()
        {
            // ボタンのテキストを初期化
            this.ButtonInit(parentForm);

            // オプション非対応
            if (!AppConfig.AppOptions.IsFileUpload())
            {
                // ファイルアップロードボタン無効化
                this.parentForm.bt_process1.Text = string.Empty;
                this.parentForm.bt_process1.Enabled = false;

                this.form.btnUpload.Visible = false;
                this.form.customDataGridView1.Visible = false;
            }
        }

        /// <summary>
        /// 電子契約ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process2_Click(object sender, EventArgs e)
        {
            if (this.form.errmessage.MessageBoxShowConfirm("電子契約送信を行いますか？", MessageBoxDefaultButton.Button1)
                == System.Windows.Forms.DialogResult.Yes)
            {

                if (this.HstCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end
                if (!this.form.ItakuKeiyakuHinmeiCheck())
                {
                    return;
                }

                if (!this.form.ItakuKeiyakuHoukokushoBunruiCheck())
                {
                    return;
                }

                if (!this.form.ItakuKeiyakuTsumikaeCheck())
                {
                    return;
                }

                if (!this.form.ItakuKeiyakuBetsu3Check())
                {
                    return;
                }

                // VUNGUYEN 20150525 #1294 START
                if (this.CheckRegistData())
                {
                    return;
                }
                // VUNGUYEN 20150525 #1294 END

                // 電子契約オプション = ONの場合、電子契約タブのチェックを行う。
                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    // 電子契約データのチェックを行う。
                    if (this.CheckDenshiKeiyakuData())
                    {
                        return;
                    }
                }

                bool catchErr = false;
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();

                // 新規の場合のみ、システムIDを採番する
                // 登録データ作成前に行う必要あり
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規採番
                    catchErr = this.Saiban();
                    if (catchErr)
                    {
                        return;
                    }
                    this.SystemId = this.form.SYSTEM_ID.Text.ToString();
                }

                // 登録用データの作成
                catchErr = this.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }

                switch (this.form.WindowType)
                {
                    // 新規追加
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 重複チェック
                        bool result = this.form.DupliUpdateViewCheck(e, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }
                        if (result)
                        {
                            // 重複していなければ登録を行う
                            this.registMsgFlg = false;
                            this.Regist(false);
                            this.registMsgFlg = true;
                        }
                        break;

                    // 更新
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.registMsgFlg = false;
                        this.Update(false);
                        this.registMsgFlg = true;
                        break;

                    default:
                        break;
                }

                //画面を閉じる
                this.form.FormClose(sender, e);

                //電子契約入力画面を表示
                FormManager.OpenForm("G715", WINDOW_TYPE.NEW_WINDOW_FLAG, this.SystemId, "");

            }

        }

        /// <summary>
        /// 電子契約最新照会ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                FormManager.OpenFormWithAuth("M734", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

                LogUtility.DebugMethodEnd(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process3_Click", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
            }
        }

        /// <summary>
        /// オプション対応時に電子契約サブファンクションボタンを表示
        /// </summary>
        private void DenshiKeiyakuButtonSetting()
        {
            // 電子契約オプションがfalseの場合、サブファンクションの2,3は非表示/押下不可とする。
            if (!AppConfig.AppOptions.IsDenshiKeiyaku())
            {
                // テキストクリア
                this.parentForm.bt_process2.Text = "";
                this.parentForm.bt_process3.Text = "";

                // 非活性
                this.parentForm.bt_process2.Enabled = false;
                this.parentForm.bt_process3.Enabled = false;
            }
        }

        /// <summary>
        /// 印刷ボタンの非活性処理
        /// </summary>
        private void PrintButtonSetting()
        {
            // 画面の登録方法＝詳細マスタの時、活性
            if (this.form.dispTourokuHouhou.Equals(ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI))
            {
                // 活性
                this.parentForm.bt_func5.Enabled = true;
            }
            // 画面の登録方法＝基本マスタの時、非活性
            else if (this.form.dispTourokuHouhou.Equals(ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON))
            {
                // 非活性
                this.parentForm.bt_func5.Enabled = false;
            }
        }

        /// <summary>
        /// ファイルアップロードボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void btn_upload_Click(object sender, EventArgs e)
        {
            String fileDirPath = "";

            // 委託契約書が未入力の時
            if (this.form.ITAKU_KEIYAKU_FILE_PATH.Text.Equals(string.Empty))
            {
                this.form.errmessage.MessageBoxShowError("アップロードを行う委託契約書をセットください。");
                return;
            }

            //ファイルパス上に委託契約書が存在する
            fileDirPath = this.form.ITAKU_KEIYAKU_FILE_PATH.Text;
            FileInfo fileInfo = new FileInfo(fileDirPath);
            //ファイルの存在チェック
            if (fileInfo.Exists == false)
            {
                this.form.errmessage.MessageBoxShowError("入力されたファイルパスの委託契約書が見つかりませんでした。再度、委託契約書をセットしてください。");
                return;
            }

            var fileId = GetRegistedFileId(this.form.ITAKU_KEIYAKU_FILE_PATH.Text);
            if (0 < fileId)
            {
                if (this.form.errmessage.MessageBoxShowConfirm("アップロード済みのファイルが存在します。ファイルの上書きをしますか？") == DialogResult.No)
                {
                    return;
                }
            }

            //事前処理
            if (this.form.errmessage.MessageBoxShowConfirm("ファイルアップロードの事前処理として登録処理を行います。よろしいですか？", MessageBoxDefaultButton.Button1)
                == System.Windows.Forms.DialogResult.Yes)
            {

                //入力エラーチェック
                if (this.HstCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end
                if (!this.form.ItakuKeiyakuHinmeiCheck())
                {
                    return;
                }

                if (!this.form.ItakuKeiyakuHoukokushoBunruiCheck())
                {
                    return;
                }

                if (!this.form.ItakuKeiyakuTsumikaeCheck())
                {
                    return;
                }

                if (!this.form.ItakuKeiyakuBetsu3Check())
                {
                    return;
                }

                // VUNGUYEN 20150525 #1294 START
                if (this.CheckRegistData())
                {
                    return;
                }
                // VUNGUYEN 20150525 #1294 END

                // 電子契約オプション = ONの場合、電子契約タブのチェックを行う。
                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    // 電子契約データのチェックを行う。
                    if (this.CheckDenshiKeiyakuData())
                    {
                        return;
                    }
                }

                // ユーザ定義情報を取得
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ファイルアップロード参照先のフォルダを取得
                string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                // ファイルアップロード用DB接続を確立
                if (!this.uploadLogic.CanConnectDB())
                {
                    this.form.errmessage.MessageBoxShowError("ファイルアップロードの事前処理が未完了です。接続先データベースを確認してください。");
                }
                // システム個別設定入力の初期フォルダの設定有無をチェックする。
                else if (string.IsNullOrEmpty(serverPath) || !Directory.Exists(serverPath))
                {
                    this.form.errmessage.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
                }
                else
                {

                    // 委託契約書の登録処理を行う。
                    //this.Regist(false);
                    // 新規の場合のみ、システムIDを採番する
                    // 登録データ作成前に行う必要あり
                    bool catchErr = false;
                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        // 新規採番
                        catchErr = this.Saiban();
                        if (catchErr)
                        {
                            return;
                        }
                    }

                    // 登録用データの作成
                    catchErr = this.CreateEntity(false);
                    if (catchErr)
                    {
                        return;
                    }

                    switch (this.form.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 重複チェック
                            bool result = this.form.DupliUpdateViewCheck(e, out catchErr);
                            if (catchErr)
                            {
                                return;
                            }
                            if (result)
                            {
                                // 重複していなければ登録を行う
                                this.registMsgFlg = false;
                                this.Regist(false);
                                this.registMsgFlg = true;

                                this.SystemId = this.systemIdForUpload;
                                this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                            }
                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.registMsgFlg = false;
                            this.Update(false);
                            this.registMsgFlg = true;
                            break;

                        default:
                            break;
                    }

                    // ファイルアップロードlogicに渡す引数セット
                    string[] paramList = new string[1];
                    paramList[0] = this.SystemId;

                    //アップロード
                    if (!this.uploadLogic.UploadForOneFile(this.form.ITAKU_KEIYAKU_FILE_PATH.Text, fileId, WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI, paramList))
                    {
                        return;
                    }

                    //画面データ更新
                    this.SetWindowData();

                    this.form.errmessage.MessageBoxShowInformation("アップロードしました。");

                }
            }

        }

        /// <summary>
        /// 指定されたファイルパスと一致するファイルIDを取得
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>ファイルID</returns>
        private long GetRegistedFileId(string path)
        {
            long result = 0;

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return result;
            }

            // 更新対象のファイル名取得
            var fileName = Path.GetFileName(path);

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                //if (fileName.Equals(row.Cells["FILE_NAME"].Value.ToString()))
                if (path.Equals(row.Cells["FILE_PATH"].Value.ToString()))
                {
                    // 更新対象と同一のファイル名からファイルID取得
                    result = long.Parse(row.Cells["FILE_ID"].Value.ToString());
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 一覧にデータを設定する。
        /// </summary>
        private void SetFileUploadIchiran()
        {
            // 一覧をクリアする。
            this.form.customDataGridView1.Rows.Clear();

            // 最新のファイルIDリストに更新
            var dtoList = this.fileLinkItakuKeiyakuKihonDao.GetDataBySystemId(this.SystemId);
            List<long> fileIdList = null;
            if (dtoList != null)
            {
                fileIdList = dtoList.Select(n => n.FILE_ID.Value).ToList();
            }

            if (fileIdList != null && 0 < fileIdList.Count)
            {
                // ファイルIDをもとに、DBからアップロード済のファイルパスを取得する。
                List<T_FILE_DATA> fileDataList = this.fileDataDao.GetLightDataByKeyList(fileIdList);
                if (fileDataList != null)
                {
                    foreach (var fileData in fileDataList)
                    {
                        this.form.customDataGridView1.Rows.Add();
                        // FILE_ID
                        this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["FILE_ID"].Value = fileData.FILE_ID;
                        // FILE_PATH
                        this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["FILE_PATH"].Value = fileData.FILE_PATH;
                        // ファイル名
                        this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["FILE_NAME"].Value = Path.GetFileName(fileData.FILE_PATH);
                    }
                }
            }

        }

        /// <summary>
        /// アップロード一覧閲覧セルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void previewClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.form.customDataGridView1.RowCount == 0 || this.form.customDataGridView1.CurrentRow == null)
                {
                    this.form.errmessage.MessageBoxShowError("プレビュー対象の行を選択してください。");
                    return;
                }

                int columnIndex = e.ColumnIndex;
                string cellName = this.form.customDataGridView1.Columns[columnIndex].Name;

                if (string.IsNullOrEmpty(cellName) || !cellName.Equals("BTN_PREVIEW"))
                {
                    return;
                }

                DataGridViewCell currentCell = this.form.customDataGridView1.CurrentCell;
                var filePath = this.form.customDataGridView1.CurrentRow.Cells["FILE_PATH"].Value.ToString();
                var cellHidden_fileId = this.form.customDataGridView1.CurrentRow.Cells["FILE_ID"].Value;
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return;
                }
                var fileId = long.Parse(cellHidden_fileId.ToString());
                this.uploadLogic.Preview(fileId);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                if (ex is SQLRuntimeException)
                {
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E245");
                }
            }
        }

        /// <summary>
        /// 社内経路（有り、無し）テキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        private void SHANAI_KEIRO_TextChanged(object sender, EventArgs e)
        {
            string shanaiKeiro = this.form.SHANAI_KEIRO.Text;

            if (shanaiKeiro == "1")
            {
                //社内経路リスト利用可
                this.form.keiroIchiran.ReadOnly = false;
                foreach (DataGridViewRow row in this.form.keiroIchiran.Rows)
                {
                    row.UpdateRowBackColor(false);
                }

                //社内経路CD名利用可
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Enabled = true;
            }

            else if (shanaiKeiro == "2")
            {
                //社内経路リスト利用不可
                this.form.keiroIchiran.Rows.Clear();
                this.form.keiroIchiran.ReadOnly = true;
                foreach (DataGridViewRow row in this.form.keiroIchiran.Rows)
                {
                    row.UpdateRowBackColor(false);
                }

                //社内経路CD名利用不可
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text = string.Empty;
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Enabled = false;
                this.form.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 契約先（社外）一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void CellEnterKeiyakusaki(int columnIndex)
        {
            string cellName = this.form.keiroIchiran2.Columns[columnIndex].Name;

            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }

            // IME制御
            switch (cellName)
            {
                case "chb_delete_keiyakusaki":
                case "KEIYAKUSAKI_YUUSEN_NO":
                case "KEIYAKUSAKI_MAIL_ADDRESS":
                    this.form.keiroIchiran2.ImeMode = ImeMode.Disable;
                    break;

                default:
                    this.form.keiroIchiran2.ImeMode = ImeMode.Hiragana;
                    break;
            }
        }

        /// <summary>
        /// 契約先（社外）一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void CellValidatingKeiyakusaki(int columnIndex)
        {
            // 行番号が出力済
            if (this.form.keiroIchiran2.CurrentRow.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value != null)
            {
                return;
            }

            string cellName = this.form.keiroIchiran2.Columns[columnIndex].Name;
            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }
            if (this.form.keiroIchiran2.CurrentRow.Cells[cellName].Value == null)
            {
                return;
            }

            switch (cellName)
            {
                case "KEIYAKUSAKI_CORP_NAME":
                case "KEIYAKUSAKI_SHAIN_NAME":
                case "KEIYAKUSAKI_MAIL_ADDRESS":

                    if(!this.form.keiroIchiran2.CurrentRow.Cells[cellName].Value.Equals(string.Empty))
                    {
                        this.form.keiroIchiran2.CurrentRow.Cells["KEIYAKUSAKI_YUUSEN_NO"].Value = this.form.keiroIchiran2.CurrentRow.Index + 1;
                    }
                    break;
            
                default:
                    break;
            }

        }
        
        /// <summary>
        /// 1行上げる
        /// </summary>
        private void btnUpRow_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //選択確認
                if (this.form.keiroIchiran2.SelectedCells.Count > 0)
                {
                    //1行目でないこと
                    int i = this.form.keiroIchiran2.CurrentCell.RowIndex;
                    if (this.form.keiroIchiran2.Rows[i].IsNewRow)
                    {
                        return;
                    }
                    if (i > 0)
                    {
                        //移動先行の挿入
                        this.form.keiroIchiran2.Rows.Insert(i - 1, this.CloneWithValues(this.form.keiroIchiran2.Rows[i]));

                        //移動元行の削除
                        this.form.keiroIchiran2.Rows.RemoveAt(i + 1);

                        //Noを元に戻す
                        if (this.form.keiroIchiran2.Rows[i - 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value != null
                            && !string.IsNullOrWhiteSpace(this.form.keiroIchiran2.Rows[i - 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()))
                        {
                            this.form.keiroIchiran2.Rows[i - 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value = int.Parse(this.form.keiroIchiran2.Rows[i - 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()) - 1;
                        }

                        if (this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value != null
                            && !string.IsNullOrWhiteSpace(this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()))
                        {
                            this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value = int.Parse(this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()) + 1;
                        }

                        //フォーカス移動
                        this.form.keiroIchiran2.Focus();
                        for (int j = 3; j > 0; j--)
                        {
                            this.form.keiroIchiran2.CurrentCell = this.form.keiroIchiran2.Rows[i - 1].Cells[j - 1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("btnUpRow_Click", ex);
                this.form.errmessage.MessageBoxShow("E245");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 1行下げる
        /// </summary>
        private void btnDownRow_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択確認
                if (this.form.keiroIchiran2.SelectedCells.Count > 0)
                {
                    //最終行目でないこと
                    int i = this.form.keiroIchiran2.CurrentCell.RowIndex;
                    if (this.form.keiroIchiran2.Rows[i].IsNewRow)
                    {
                        return;
                    }
                    if (i < this.form.keiroIchiran2.RowCount - 2)
                    {
                        //移動先行の挿入
                        this.form.keiroIchiran2.Rows.Insert(i + 2, this.CloneWithValues(this.form.keiroIchiran2.Rows[i]));

                        //移動元行の削除
                        this.form.keiroIchiran2.Rows.RemoveAt(i);

                        //Noを元に戻す
                        if (this.form.keiroIchiran2.Rows[i + 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value != null
                            && !string.IsNullOrWhiteSpace(this.form.keiroIchiran2.Rows[i + 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()))
                        {
                            this.form.keiroIchiran2.Rows[i + 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value = int.Parse(this.form.keiroIchiran2.Rows[i + 1].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()) + 1;
                        }

                        if (this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value != null
                            && !string.IsNullOrWhiteSpace(this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()))
                        {
                            this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value = int.Parse(this.form.keiroIchiran2.Rows[i].Cells["KEIYAKUSAKI_YUUSEN_NO"].Value.ToString()) - 1;
                        }

                        //フォーカス移動
                        this.form.keiroIchiran2.Focus();
                        for (int j = 3; j > 0; j--)
                        {
                            this.form.keiroIchiran2.CurrentCell = this.form.keiroIchiran2.Rows[i + 1].Cells[j - 1];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("btnDownRow_Click", ex);
                this.form.errmessage.MessageBoxShow("E245");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///DataGridViewの行を複製する。
        /// </summary>
        private DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart();

            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 i = 0; i < row.Cells.Count; i++)
            {
                clonedRow.Cells[i].Value = row.Cells[i].Value;
            }

            LogUtility.DebugMethodEnd();
            return clonedRow;
        }

        #region INXS処理  refs #158006

        private void ParentForm_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!AppConfig.AppOptions.IsInxsItaku())
                {
                    return;
                }
                this.inxsContractLogic.HandleResponse(message, this.form.transactionId);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

    }
}