using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.PaperManifest.InsatsuBusuSettei;
using Shougun.Core.Common.BusinessCommon.Enums;

namespace Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>ボタンの設定用ファイルパス</summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 建廃マニフェスト入力のForm
        /// </summary>
        public KenpaiManifestoNyuryoku form { get; set; }

        /// <summary>
        /// 建廃マニフェスト入力のHeader
        /// </summary>
        public KenpaiManifestoNyuryokuHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        /// <summary>建廃マニフェスト入力のDTO</summary>
        private SerchParameterDtoCls dto;

        /// <summary>パターン一覧のDao</summary>
        private ParameterDaoCls mopdao;

        /// <summary>受入Dao</summary>
        private UkeireDaoCls Ukeiredao;

        /// <summary>出荷Dao</summary>
        private ShukkaDaoCls Shukkadao;

        /// <summary>売上Dao</summary>
        private UriageDaoCls Uriagedao;

        /// <summary>売上Dao</summary>
        private HaishaDaoCls Haishadao;

        /// <summary>売上Dao</summary>
        private KeiryouDaoCls Keiryoudao;

        // 20160510 koukoukon #12041 定期配車関連の全面見直し start
        /// <summary>定期実績Dao</summary>
        private TeikiJissekiDaoCls TeikiJissekidao;
        // 20160510 koukoukon #12041 定期配車関連の全面見直し end

        /// <summary>システム設定入力Dao</summary>
        private SysInfoDaoCls SysInfodao;

        /// <summary>混合種類名検索Dao</summary>
        private GetKongouNameDaoCls GetKongouNamedao;

        ///// <summary>交付番号検索Dao</summary>
        //private SerchKohuDaoCls SerchKohu;

        /// <summary>システムID採番Dao</summary>
        private IS_NUMBER_SYSTEMDao GetIsNoumber;

        /// <summary>マニ印字_建廃_形状更新Dao</summary>
        private KeijyouDaoCls KeijyouDao;

        /// <summary>マニ印字_建廃_荷姿更新Dao</summary>
        private NisugataDaoCls NisugataDao;

        /// <summary>マニ印字_建廃_処分方法更新Dao</summary>
        private HouhouDaoCls HouhouDao;

        /// <summary>マニフェスト更新Dao</summary>
        private MaxSeqDaoCls MaxSeqDao;

        /// <summary>マニフェスト返却検索Dao</summary>
        private MaxRetDateDaoCls MaxRetDateDao;

        /// <summary>伝種区分取得Dao</summary>
        private GetDenshuDaoCls GetDenshuDao;

        /// <summary>マニフェスト更新Dao</summary>
        private PtMaxSeqDaoCls PtMaxSeqDao;

        /// <summary>マニ印字_建廃_形状更新Dao</summary>
        private PtKeijyouDaoCls PtKeijyouDao;

        /// <summary>マニ印字_建廃_荷姿更新Dao</summary>
        private PtNisugataDaoCls PtNisugataDao;

        /// <summary>マニ印字_建廃_処分方法更新Dao</summary>
        private PtHouhouDaoCls PtHouhouDao;

        /// <summary>廃棄物種類ポップアップ用Dao</summary>
        private GetHaikiShuruiDaoCls GetHaikiShuruiDao;

        /// <summary>運転者検索Dao</summary>
        private GetUntenshaDaoCls GetUntenshaDao;

        ///// <summary>処分担当者検索Dao</summary>
        //private GetShobunTantoushaDaoCls GetShobunTantoushaDao;

        /// <summary>車輌検索Dao</summary>
        private GetCarDataDaoCls GetCarDataDao;

        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
        /// <summary>機能間連携検索Dao</summary>
        private GetKenpaiDataDaoCls GetKenpaiDataDao;
        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

        // 20140609 katen No.730 規定値機能の追加について start
        /// <summary>規定値検索Dao</summary>
        private KiteiValueSsDaoCls KiteiValueDao;
        // 20140609 katen No.730 規定値機能の追加について end

        // 20140611 katen 不具合No.4469 start‏
        /// <summary>業者検索Dao</summary>
        private GyoushaDaoCls GyoushaDao;

        /// <summary>現場検索Dao</summary>
        private GenbaDaoCls GenbaDao;
        // 20140611 katen 不具合No.4469 start‏

        private IM_NISUGATADao IMNisugataDao;
        private IM_SHOBUN_HOUHOUDao IMShobunHouhouDao;
        private IM_UNITDao IMUnitDao;

        /// <summary>廃棄区分(建廃)</summary>
        internal string FormHaikiKbn = "2";

        /// <summary>画面初期表示Flag</summary>
        private bool firstLoadFlg = true;

        /// <summary>Entity</summary>
        public SuperEntity Entity { get; set; }

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }

        /// <summary>検索条件</summary>
        public SerchParameterDtoCls SearchString { get; set; }

        /// <summary>マニ</summary>
        private byte[] TIME_STAMP_ENTRY = null;

        /// <summary>マニ返却日</summary>
        private byte[] TIME_STAMP_RET_DATE = null;

        /// <summary>マニFlag</summary>
        internal int maniFlag = 1;

        /// <summary>共通</summary>
        ManifestoLogic mlogic = null;

        /// <summary>マニフェスト情報数量書式CD</summary>
        internal string ManifestSuuryoFormatCD = String.Empty;

        /// <summary>マニフェスト情報数量書式</summary>
        internal string ManifestSuuryoFormat = String.Empty;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        /// <summary>取引先マスタ</summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;
        // 2014.05.19　by胡 strat
        //システム情報
        private M_SYS_INFO mSysInfo;
        // 2014.05.19　by胡 end

        // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 start
        //収集受付
        private GetUketsukeSsDaoCls GetUketsukeSsDataDao;
        //持込受付
        private GetUketsukeMkDaoCls GetUketsukeMkDataDao;
        //出荷受付
        private GetUketsukeSkDaoCls GetUketsukeSkDataDao;
        //伝種区分名称
        private GetDenshuKbnNameDaoCls GetDenshuKbnNameDataDao;
        // 20160510 koukoukon #12041 定期配車関連の全面見直し start
        /// <summary>運搬方法Dao</summary>
        private IM_UNPAN_HOUHOUDao UnpanhouhouDao;
        // 20160510 koukoukon #12041 定期配車関連の全面見直し end
        //連携データテーブル
        internal DataTable renkei_dt;
        // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 end
        // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
        private string unit_name;
        // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end

        // 20140604 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
        //システム設定に登録チェック
        private string manifest_validation_check;
        // 20140604 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end

        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
        private List<Control> controlList;
        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある end

        private List<Control> manifestMercuryList;

        private List<Control> manifestKamiList;

        /// <summary>参照モード時の非活性対象のコントロールリスト</summary>
        private List<Control> EnabledCtrlList;

        /// <summary>参照モード時の読み取り専用のカラムリスト</summary>
        private List<DataGridViewColumn> ReadOnlyColumnList;

        /// <summary>検索条件の削除フラグ使用有無</summary>
        /// <remarks>横連携時のみTrueとします</remarks>
        internal bool isNotNeedDeleteFlg = false;

        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
        private T_MANIFEST_ENTRYdaocls ManifestEntryDao;
        private T_MANIFEST_DETAILdaocls ManifestDetailDao;
        private T_MANIFEST_RELATIONDaoCls ManifestRetDateDao;
        private T_MANIFEST_UPNDaoCls ManifestUpnDao;
        private T_MANIFEST_PRTDaoCls ManifestPrtDao;
        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

        /// <summary>最終処分情報入力済みフラグ</summary>
        bool existAllLastSbnInfo = false;
        private MessageBoxShowLogic MsgBox;
        private T_MANIFEST_RELATIONSDaoCls Relationdao;

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// INXS manifest logic refs #158004
        /// </summary>
        private InxsManifestLogic inxsManifestLogic;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(KenpaiManifestoNyuryoku targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dto = new SerchParameterDtoCls();
            this.mopdao = DaoInitUtility.GetComponent<ParameterDaoCls>();
            this.Ukeiredao = DaoInitUtility.GetComponent<UkeireDaoCls>();
            this.Shukkadao = DaoInitUtility.GetComponent<ShukkaDaoCls>();
            this.Uriagedao = DaoInitUtility.GetComponent<UriageDaoCls>();
            this.Haishadao = DaoInitUtility.GetComponent<HaishaDaoCls>();
            this.Keiryoudao = DaoInitUtility.GetComponent<KeiryouDaoCls>();
            // 20160510 koukoukon #12041 定期配車関連の全面見直し start
            this.TeikiJissekidao = DaoInitUtility.GetComponent<TeikiJissekiDaoCls>();
            // 20160510 koukoukon #12041 定期配車関連の全面見直し end
            this.SysInfodao = DaoInitUtility.GetComponent<SysInfoDaoCls>();
            this.GetKongouNamedao = DaoInitUtility.GetComponent<GetKongouNameDaoCls>();
            this.GetIsNoumber = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            this.KeijyouDao = DaoInitUtility.GetComponent<KeijyouDaoCls>();
            this.NisugataDao = DaoInitUtility.GetComponent<NisugataDaoCls>();
            this.HouhouDao = DaoInitUtility.GetComponent<HouhouDaoCls>();
            this.MaxSeqDao = DaoInitUtility.GetComponent<MaxSeqDaoCls>();
            this.MaxRetDateDao = DaoInitUtility.GetComponent<MaxRetDateDaoCls>();
            //this.SerchKohu = DaoInitUtility.GetComponent<SerchKohuDaoCls>();
            this.GetDenshuDao = DaoInitUtility.GetComponent<GetDenshuDaoCls>();
            this.PtMaxSeqDao = DaoInitUtility.GetComponent<PtMaxSeqDaoCls>();
            this.PtKeijyouDao = DaoInitUtility.GetComponent<PtKeijyouDaoCls>();
            this.PtNisugataDao = DaoInitUtility.GetComponent<PtNisugataDaoCls>();
            this.PtHouhouDao = DaoInitUtility.GetComponent<PtHouhouDaoCls>();
            this.GetHaikiShuruiDao = DaoInitUtility.GetComponent<GetHaikiShuruiDaoCls>();
            this.GetUntenshaDao = DaoInitUtility.GetComponent<GetUntenshaDaoCls>();
            //this.GetShobunTantoushaDao = DaoInitUtility.GetComponent<GetShobunTantoushaDaoCls>();
            this.GetCarDataDao = DaoInitUtility.GetComponent<GetCarDataDaoCls>();
            // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
            this.GetKenpaiDataDao = DaoInitUtility.GetComponent<GetKenpaiDataDaoCls>();
            // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

            // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 start
            this.GetUketsukeSsDataDao = DaoInitUtility.GetComponent<GetUketsukeSsDaoCls>();
            this.GetUketsukeMkDataDao = DaoInitUtility.GetComponent<GetUketsukeMkDaoCls>();
            this.GetUketsukeSkDataDao = DaoInitUtility.GetComponent<GetUketsukeSkDaoCls>();
            this.GetDenshuKbnNameDataDao = DaoInitUtility.GetComponent<GetDenshuKbnNameDaoCls>();
            // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 end

            // 20140609 katen No.730 規定値機能の追加について start
            this.KiteiValueDao = DaoInitUtility.GetComponent<KiteiValueSsDaoCls>();
            // 20140609 katen No.730 規定値機能の追加について end

            // 20140611 katen 不具合No.4469 start‏
            this.GyoushaDao = DaoInitUtility.GetComponent<GyoushaDaoCls>();
            this.GenbaDao = DaoInitUtility.GetComponent<GenbaDaoCls>();
            // 20140611 katen 不具合No.4469 end‏

            // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
            this.ManifestEntryDao = DaoInitUtility.GetComponent<T_MANIFEST_ENTRYdaocls>();
            this.ManifestDetailDao = DaoInitUtility.GetComponent<T_MANIFEST_DETAILdaocls>();
            this.ManifestRetDateDao = DaoInitUtility.GetComponent<T_MANIFEST_RELATIONDaoCls>();
            this.ManifestUpnDao = DaoInitUtility.GetComponent<T_MANIFEST_UPNDaoCls>();
            this.ManifestPrtDao = DaoInitUtility.GetComponent<T_MANIFEST_PRTDaoCls>();
            // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

            this.IMNisugataDao = DaoInitUtility.GetComponent<IM_NISUGATADao>();
            this.IMShobunHouhouDao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
            this.IMUnitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            // 20160510 koukoukon #12041 定期配車関連の全面見直し start
            this.UnpanhouhouDao = DaoInitUtility.GetComponent<IM_UNPAN_HOUHOUDao>();
            // 20160510 koukoukon #12041 定期配車関連の全面見直し end
            this.Relationdao = DaoInitUtility.GetComponent<T_MANIFEST_RELATIONSDaoCls>();
            this.MsgBox = new MessageBoxShowLogic();

            this.form.ismobile_mode = r_framework.Configuration.AppConfig.AppOptions.IsMobile();

            this.inxsManifestLogic = new InxsManifestLogic(EnumManifestType.KAMI);

            LogUtility.DebugMethodEnd(targetForm);
        }

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LogicClass other)
        {
            return this.Equals(other);
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
            LogUtility.DebugMethodStart();

            try
            {
                DialogResult result =
                        this.form.messageShowLogic.MessageBoxShow("C026");
                if (result == DialogResult.No)
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                //INXS start Check and confirm delete INXS manifets refs #158004
                bool isUploadToInxs = false;
                if (AppConfig.AppOptions.IsInxsManifest())
                {
                    isUploadToInxs = this.inxsManifestLogic.IsUploadManifestToInxs(this.form.parameters.SystemId);
                    if (isUploadToInxs && this.form.messageShowLogic.MessageBoxShow("C117") == DialogResult.No)
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                //INXS end

                using (Transaction tran = new Transaction())
                {
                    //システムID(全般･マニ返却日)
                    String SystemID = this.form.parameters.SystemId;

                    //枝番(全般)
                    String Seq = this.form.parameters.Seq;

                    //枝番(マニ返却日)
                    String SeqRD = this.form.parameters.SeqRD;

                    mlogic.LogicalEntityDel(SystemID, Seq, TIME_STAMP_ENTRY);

                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                    this.LogicalDelEntityInsert(SystemID, Seq);
                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

                    if (String.IsNullOrEmpty(SeqRD) == false)
                    {
                        mlogic.LogicalRetDateDel(SystemID, SeqRD, TIME_STAMP_RET_DATE);

                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        this.LogicalDelRetDateInsert(SystemID, Seq);
                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end
                    }

                    tran.Commit();
                }

                ////システムID(全般･マニ返却日)
                //this.form.parameters.SystemId = string.Empty;

                ////枝番(全般)
                //this.form.parameters.Seq = string.Empty;

                ////枝番(マニ返却日)
                //this.form.parameters.SeqRD = string.Empty;

                //this.form.parameters.RenkeiSystemId = string.Empty;
                //this.form.parameters.RenkeiMeisaiSystemId = string.Empty;
                //this.form.parameters.Save();

                //INXS start Delete INXS manifets refs #158004
                if (AppConfig.AppOptions.IsInxsManifest() && isUploadToInxs)
                {
                    this.inxsManifestLogic.DeleteInxsData(this.form.parameters.SystemId, this.form.transactionId, ((BusinessBaseForm)this.form.Parent).Text);
                }
                //INXS end

                this.form.messageShowLogic.MessageBoxShow("I001", "削除");

            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 start
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Warn(ex);
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E080");
                return false;
            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 end
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd(true);
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
        private bool RegistResult = false;
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            RegistResult = false;

            try
            {
                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                using (Transaction tran = new Transaction())
                {
                    //登録データ作成
                    //システムID(全般･マニ返却日)
                    String SystemID = this.form.parameters.SystemId;

                    //枝番(全般)
                    String Seq = this.form.parameters.Seq;

                    //枝番(マニ返却日)
                    String SeqRD = this.form.parameters.SeqRD;

                    this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, false, SystemID, Seq, SeqRD, false);

                    mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist);

                    if (this.maniRelation != null) //紐付処理
                    {
                        this.maniRelation.Regist(tran, entrylist[0].SYSTEM_ID);
                    }

                    tran.Commit();

                    this.maniRelation = null; //紐付情報クリア
                }

                this.form.messageShowLogic.MessageBoxShow("I001", "登録");
                RegistResult = true;
            }
            catch (ManifestHimoduke.RelationDupulicateException ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                //紐付済みのためロールバック
                LogUtility.Warn(ex);
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(ex.Message);
            }
            catch (Exception ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// update成功時True
        /// </summary>
        private bool UpdateResult = false;
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
                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                using (Transaction tran = new Transaction())
                {
                    //システムID(全般･マニ返却日)
                    String SystemID = this.form.parameters.SystemId;

                    //枝番(全般)
                    String Seq = this.form.parameters.Seq;

                    //枝番(マニ返却日)
                    String SeqRD = this.form.parameters.SeqRD;

                    mlogic.LogicalEntityDel(SystemID, Seq, TIME_STAMP_ENTRY);

                    if (String.IsNullOrEmpty(SeqRD) == false)
                    {
                        mlogic.LogicalRetDateDel(SystemID, SeqRD, TIME_STAMP_RET_DATE);
                    }

                    //登録データ作成
                    this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, true, SystemID, Seq, SeqRD, false);

                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                    this.UpdateCreateInfo(ref entrylist, ref retdatelist, SystemID, Seq);
                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

                    mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist);

                    if (this.maniRelation != null) //紐付処理
                    {
                        this.maniRelation.Regist(tran, entrylist[0].SYSTEM_ID);
                    }

                    // 20140710 ria EV005141 2次マニを修正モードにて登録しても紐づいた1次マニが更新されない start
                    if (this.maniFlag == 2)
                    {
                        //this.mlogic.UpdateFirstManiDetail(SystemID, this.form.HaikiKbnCD);
                        if (detaillist != null && detaillist.Count() > 0)
                        {
                            foreach (T_MANIFEST_DETAIL detail in detaillist)
                            {
                                this.mlogic.UpdateFirstManiDetail(detail.DETAIL_SYSTEM_ID.Value.ToString(), this.form.HaikiKbnCD);
                            }
                        }
                    }
                    // 20140710 ria EV005141 2次マニを修正モードにて登録しても紐づいた1次マニが更新されない end

                    tran.Commit();

                    this.maniRelation = null; //紐付情報クリア
                }
                this.form.messageShowLogic.MessageBoxShow("I001", "修正");
                UpdateResult = true;

            }
            catch (ManifestHimoduke.RelationDupulicateException ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                //紐付済みのためロールバック
                LogUtility.Warn(ex);
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(ex.Message);
            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 start
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Warn(ex);

                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E080");
            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 end
            catch (Exception ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                LogUtility.Error(ex);
                throw;

            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        #endregion

        #region 検索

        /// <summary>
        /// パラメータを条件に番号検索
        /// </summary>
        [Transaction]
        public virtual DataTable SerchParameter()
        {
            LogUtility.DebugMethodStart();

            this.SearchResult = new DataTable();
            this.SearchString = new SerchParameterDtoCls();

            if (this.form.parameters.RenkeiDenshuKbnCd != string.Empty)
            {
                this.SearchString.RENKEI_SYSTEM_ID = this.form.parameters.RenkeiSystemId;
                this.SearchString.RENKEI_DENSHU_KBN_CD = this.form.parameters.RenkeiDenshuKbnCd;
                this.SearchString.RENKEI_MEISAI_SYSTEM_ID = this.form.parameters.RenkeiMeisaiSystemId;
            }
            else
            {
                this.SearchString.SYSTEM_ID = this.form.parameters.SystemId;
            }

            this.SearchResult = mopdao.GetDataForEntity(this.SearchString);

            LogUtility.DebugMethodEnd();

            return this.SearchResult;
        }

        /// <summary>
        /// パラメータを条件に番号検索
        /// </summary>
        [Transaction]
        public virtual DataTable SerchDenshu()
        {
            LogUtility.DebugMethodStart();

            this.SearchResult = new DataTable();
            this.SearchString = new SerchParameterDtoCls();

            this.SearchString.RENKEI_DENSHU_KBN_CD = this.form.parameters.RenkeiDenshuKbnCd;
            this.SearchString.RENKEI_SYSTEM_ID = this.form.parameters.RenkeiSystemId;
            this.SearchString.RENKEI_MEISAI_SYSTEM_ID = this.form.parameters.RenkeiMeisaiSystemId;
            this.form.parameters.Save();

            if (this.form.parameters.RenkeiDenshuKbnCd != string.Empty)
            {
                int iKbn = Convert.ToInt32(this.form.parameters.RenkeiDenshuKbnCd);
                switch (iKbn)
                {
                    case (int)DENSHU_KBN.UKEIRE: //受入
                        if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                        {
                            this.SearchResult = Ukeiredao.GetDataForJissekiEntity(this.SearchString);
                        }
                        else
                        {
                            this.SearchResult = Ukeiredao.GetDataForEntity(this.SearchString);
                        }
                        break;

                    case (int)DENSHU_KBN.SHUKKA: //出荷
                        this.SearchResult = Shukkadao.GetDataForEntity(this.SearchString);
                        break;

                    case (int)DENSHU_KBN.URIAGE_SHIHARAI://売上支払
                        this.SearchResult = Uriagedao.GetDataForEntity(this.SearchString);
                        break;

                    case (int)DENSHU_KBN.KEIRYOU://計量
                        if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                        {
                            this.SearchResult = Keiryoudao.GetDataForJissekiEntity(this.SearchString);
                        }
                        else
                        {
                            this.SearchResult = Keiryoudao.GetDataForEntity(this.SearchString);
                        }
                        break;

                    case (int)DENSHU_KBN.HAISHA://配車
                        this.SearchResult = Haishadao.GetDataForEntity(this.SearchString);
                        break;
                    // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                    case (int)DENSHU_KBN.TEIKI_JISSEKI://定期実績
                        this.SearchResult = TeikiJissekidao.GetDataForEntity(this.SearchString);
                        break;
                    // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd();

            return this.SearchResult;
        }

        /// <summary>
        /// パラメータを条件にシステム設定入力検索
        /// </summary>
        [Transaction]
        public virtual DataTable SerchSysInfo()
        {
            LogUtility.DebugMethodStart();

            this.SearchResult = new DataTable();
            this.SearchString = new SerchParameterDtoCls();
            this.SearchString.SYSTEM_ID = "0";
            this.SearchResult = SysInfodao.GetDataForEntity(this.SearchString);

            LogUtility.DebugMethodEnd();

            return this.SearchResult;
        }

        /// <summary>
        /// 混合種別検索
        /// </summary>
        [Transaction]
        public virtual DataTable GetKongouName(string KongouName)
        {
            LogUtility.DebugMethodStart(KongouName);

            this.SearchResult = new DataTable();
            GetKongouNameDtoCls SearchString = new GetKongouNameDtoCls();
            SearchString.KONGOU_SHURUI_CD = KongouName;
            this.SearchResult = GetKongouNamedao.GetDataForEntity(SearchString);

            LogUtility.DebugMethodEnd(KongouName);

            return this.SearchResult;
        }

        ///// <summary>
        ///// 交付番号検索
        ///// </summary>
        //[Transaction]
        //public virtual DataTable SerchKohuNo(SerchKohuDtoCls serch)
        //{
        //    LogUtility.DebugMethodStart(SearchString);

        //    this.SearchResult = new DataTable();
        //    this.SearchResult = SerchKohu.GetDataForEntity(serch);

        //    LogUtility.DebugMethodEnd(SearchString);

        //    return this.SearchResult;
        //}

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
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit(String Kbn, out bool catchErr)
        {
            bool ret = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // システム設定日付を加工(時分以下を0にする)
                DateTime dt = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                ((BusinessBaseForm)this.form.ParentForm).sysDate = dt;

                this.form.cdate_KohuDate.IsInputErrorOccured = false;
                this.form.cdate_KohuDate.BackColor = Constans.NOMAL_COLOR;
                if (this.firstLoadFlg)
                {
                    // フォームインスタンスを取得
                    this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                    this.headerform = (KenpaiManifestoNyuryokuHeader)parentbaseform.headerForm;

                    // ボタンのテキストを初期化
                    this.ButtonInit();

                    // イベントの初期化処理
                    this.EventInit();

                    if (AppConfig.IsManiLite)
                    {
                        // マニライト版(C8)の初期化処理
                        ManiLiteInit();
                    }

                    this.allControl = this.form.allControl;
                    // 20140606 katen 不具合No.4691 start‏
                    if (this.form.fromManiFirstFlag != null)
                    {
                        //他の画面に一次二次区分をもらった場合
                        this.maniFlag = this.form.fromManiFirstFlag.Value;
                    }
                    // 20140606 katen 不具合No.4691 end‏
                    // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                    this.mlogic.sysDate = this.parentbaseform.sysDate;
                    // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                    // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
                    this.controlList = new List<Control>();
                    ManifestoLogic.MakeControl(this.parentbaseform, this.controlList);
                    if (controlList.Count > 1)
                    {
                        (controlList[1] as CustomNumericTextBox2).Text = "2";
                    }
                    // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある end
                    this.manifestMercuryList = new List<Control>();
                    ManifestoLogic.MakeReiautoKbnControl(this.parentbaseform, this.manifestMercuryList);

                    var LayoutKbn = manifestMercuryList[1] as CustomNumericTextBox2;
                    LayoutKbn.Validated += new EventHandler(LayoutKbn_Validated);

                    this.manifestKamiList = new List<Control>();
                    ManifestoLogic.MakeGyoushaGenbaControl(this.parentbaseform, this.manifestKamiList);
                }

                //[F3]ボタン
                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                        parentbaseform.bt_func3.Enabled = false;
                        break;

                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://更新モード
                        parentbaseform.bt_func3.Enabled = true;
                        break;
                }

                // システム設定入力から返却日入力の入力可否を設定
                this.SetSysInfo();

                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                if (manifestMercuryList.Count > 1)
                {
                    (manifestMercuryList[1] as CustomNumericTextBox2).Text = mSysInfo.KENPAI_MANIFEST_MERCURY_CHECK.ToString();
                }
                LayoutKbnCheck(manifestMercuryList[1].Text);

                if (manifestKamiList.Count > 1)
                {
                    (manifestKamiList[1] as CustomNumericTextBox2).Text = "2";
                }
                LayoutKamiKbnCheck(manifestKamiList[1].Text);

                // パターン呼び出し以外の場合
                if (!Kbn.Equals("bt_process2"))
                {
                    // ラベル設定処理
                    this.SetLabel();
                }

                //数値フォーマット情報取得
                ManifestSuuryoFormatCD = mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();
                ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

                // コントロールを初期化
                this.ControlInit(Kbn);

                if (!Kbn.Equals("bt_process2"))
                {
                    this.SetCheckCtl();
                    //this.SetUnpanJyutaku2Enable();
                }

                // 参照モード用にコントロールの活性・非活性を制御
                this.EnabledControl();
                
                // モード表示を設定
                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                        this.headerform.windowTypeLabel.Text = "新規";
                        this.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                        this.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;

                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://更新モード
                        this.headerform.windowTypeLabel.Text = "修正";
                        this.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                        this.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;

                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                        this.headerform.windowTypeLabel.Text = "削除";
                        this.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                        this.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.White;
                        break;

                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                        this.headerform.windowTypeLabel.Text = "参照";
                        this.headerform.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                        this.headerform.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        break;
                }

                // データをコントロールに設定
                // 1次/2次マニフェスト設定
                switch (Kbn)
                {
                    case "cantxt_KohuNo"://交付番号
                    case "SetUpdateFrom"://[F3]修正
                        //case "SetRegistFrom_New"://[F9]登録：新規モード
                        //case "SetRegistFrom_Update"://[F9]登録：修正モード
                        if (this.SetAllData())
                        {
                            return true;
                        }
                        if (!this.SetManifestFrom("DataLoad"))
                        {
                            catchErr = true;
                            ret = false;
                            return ret;
                        }
                        break;

                    case "bt_process2"://パターン
                        this.SetAllPtData();
                        if (!this.SetManifestFrom("PtLoad"))
                        {
                            catchErr = true;
                            ret = false;
                            return ret;
                        }
                        break;

                    case "OnLoad"://初期起動
                        switch (this.form.parameters.Mode)
                        {
                            case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                if (!this.SetManifestFrom("Non"))
                                {
                                    catchErr = true;
                                    ret = false;
                                    return ret;
                                }

                                DataTable dtKeiryo = new DataTable();
                                SerchParameterDtoCls dataKeiryo = new SerchParameterDtoCls();
                                if (this.form.parameters.RenkeiDenshuKbnCd.Equals("140"))
                                {
                                    dataKeiryo.RENKEI_SYSTEM_ID = this.form.parameters.RenkeiSystemId;
                                    if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                                    {
                                        dtKeiryo = Keiryoudao.GetDataForJissekiEntity(dataKeiryo);
                                    }
                                    else
                                    {
                                        dtKeiryo = Keiryoudao.GetDataForEntity(dataKeiryo);
                                    }
                                }

                                // 連携をロード 
                                DataTable dtRenkei = new DataTable();
                                SerchParameterDtoCls conditionRenkei = new SerchParameterDtoCls();
                                conditionRenkei.RENKEI_SYSTEM_ID = this.form.parameters.RenkeiSystemId;
                                if (this.form.parameters.RenkeiDenshuKbnCd.Equals(((int)DENSHU_KBN.UKEIRE).ToString()))
                                {
                                    dtRenkei = Ukeiredao.GetDataForEntity(conditionRenkei);
                                }
                                else if (this.form.parameters.RenkeiDenshuKbnCd.Equals(((int)DENSHU_KBN.SHUKKA).ToString()))
                                {
                                    dtRenkei = Shukkadao.GetDataForEntity(conditionRenkei);
                                }
                                else if (this.form.parameters.RenkeiDenshuKbnCd.Equals(((int)DENSHU_KBN.URIAGE_SHIHARAI).ToString()))
                                {
                                    dtRenkei = Uriagedao.GetDataForEntity(conditionRenkei);
                                }

                                //システムID(全般･マニ返却日)
                                this.form.parameters.SystemId = string.Empty;

                                //枝番(全般)
                                this.form.parameters.Seq = string.Empty;

                                //枝番(マニ返却日)
                                this.form.parameters.SeqRD = string.Empty;

                                this.form.parameters.ManifestID = string.Empty;
                                this.form.parameters.RenkeiSystemId = string.Empty;
                                this.form.parameters.RenkeiMeisaiSystemId = string.Empty;
                                this.form.parameters.PtSystemId = string.Empty;
                                this.form.parameters.PtSeq = string.Empty;
                                this.form.parameters.Save();

                                // 20140609 katen No.730 規定値機能の追加について start‏
                                if (this.SetKiteiValue(Kbn))
                                {
                                    this.SetAllPtData();
                                    if (!this.SetManifestFrom("PtLoad"))
                                    {
                                        catchErr = true;
                                        ret = false;
                                        return ret;
                                    }
                                }
                                this.headerform.old_kyoten = this.headerform.ctxt_KyotenCd.Text;
                                // 20140609 katen No.730 規定値機能の追加について end‏
                                if (this.form.parameters.RenkeiDenshuKbnCd.Equals("140"))
                                {
                                    // 伝種区分
                                    this.form.cantxt_DenshuKbn.Text = this.form.parameters.RenkeiDenshuKbnCd;
                                    this.form.ctxt_DenshuName.Text = "計量";
                                    //連携項目
                                    this.form.lbl_No.Text = "計量番号";
                                    // 番号
                                    this.form.cantxt_No.Text = dtKeiryo.Rows[0].ItemArray[0].ToString();
                                    // 明細行
                                    this.form.cantxt_Meisaigyou.Text = dtKeiryo.Rows[0].ItemArray[1].ToString();
                                    this.setDetail(this.form.cantxt_DenshuKbn, this.form.cantxt_No, this.form.cantxt_Meisaigyou, true, true);
                                }

                                // 連携をロード 
                                if (this.form.parameters.RenkeiDenshuKbnCd.Equals(((int)DENSHU_KBN.UKEIRE).ToString()))
                                {
                                    // 伝種区分
                                    this.form.cantxt_DenshuKbn.Text = this.form.parameters.RenkeiDenshuKbnCd;
                                    this.form.ctxt_DenshuName.Text = "受入";
                                    //連携項目
                                    this.form.lbl_No.Text = "受入番号";
                                    // 番号
                                    this.form.cantxt_No.Text = dtRenkei.Rows[0].ItemArray[0].ToString();
                                    // 明細行
                                    this.form.cantxt_Meisaigyou.Text = dtRenkei.Rows[0].ItemArray[1].ToString();
                                    this.setDetail(this.form.cantxt_DenshuKbn, this.form.cantxt_No, this.form.cantxt_Meisaigyou, true, true);
                                }
                                else if (this.form.parameters.RenkeiDenshuKbnCd.Equals(((int)DENSHU_KBN.SHUKKA).ToString()))
                                {
                                    // 伝種区分
                                    this.SetManifestFrom("F4");
                                    this.form.cantxt_DenshuKbn.Text = this.form.parameters.RenkeiDenshuKbnCd;
                                    this.form.ctxt_DenshuName.Text = "出荷";
                                    //連携項目
                                    this.form.lbl_No.Text = "出荷番号";
                                    // 番号
                                    this.form.cantxt_No.Text = dtRenkei.Rows[0].ItemArray[0].ToString();
                                    // 明細行
                                    this.maniFlag = 2;
                                    this.form.cantxt_Meisaigyou.Text = dtRenkei.Rows[0].ItemArray[1].ToString();
                                    this.setDetail(this.form.cantxt_DenshuKbn, this.form.cantxt_No, this.form.cantxt_Meisaigyou, true, true);
                                }
                                else if (this.form.parameters.RenkeiDenshuKbnCd.Equals(((int)DENSHU_KBN.URIAGE_SHIHARAI).ToString()))
                                {
                                    // 伝種区分
                                    this.form.cantxt_DenshuKbn.Text = this.form.parameters.RenkeiDenshuKbnCd;
                                    this.form.ctxt_DenshuName.Text = "売上支払";
                                    //連携項目
                                    this.form.lbl_No.Text = "売上支払番号";
                                    // 番号
                                    this.form.cantxt_No.Text = dtRenkei.Rows[0].ItemArray[0].ToString();
                                    // 明細行
                                    this.form.cantxt_Meisaigyou.Text = dtRenkei.Rows[0].ItemArray[1].ToString();
                                    this.setDetail(this.form.cantxt_DenshuKbn, this.form.cantxt_No, this.form.cantxt_Meisaigyou, true, true);
                                }
                                break;

                            case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://更新モード
                            case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                            case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                if (this.SetAllData())
                                {
                                    return true;
                                }
                                if (!this.SetManifestFrom("DataLoad"))
                                {
                                    catchErr = true;
                                    ret = false;
                                    return ret;
                                }
                                break;
                        }
                        break;

                    case "SetAddFrom"://[F2]追加
                    case "SetRegistFrom_Delete"://[F9]登録：削除モード
                    case "SetRegistFrom_New"://[F9]登録：新規モード
                    case "SetRegistFrom_Update"://[F9]登録：修正モード
                        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
                        bool continueFlag = (Kbn == "SetRegistFrom_New" || Kbn == "SetRegistFrom_Update") && (this.controlList.Count > 1 && (this.controlList[1] as CustomNumericTextBox2).Text == "1");
                        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある end
                        if (!this.SetManifestFrom("Non"))
                        {
                            catchErr = true;
                            ret = false;
                            return ret;
                        }
                        //システムID(全般･マニ返却日)
                        this.form.parameters.SystemId = string.Empty;

                        //枝番(全般)
                        this.form.parameters.Seq = string.Empty;

                        //枝番(マニ返却日)
                        this.form.parameters.SeqRD = string.Empty;

                        this.form.parameters.ManifestID = string.Empty;
                        this.form.parameters.RenkeiSystemId = string.Empty;
                        this.form.parameters.RenkeiMeisaiSystemId = string.Empty;
                        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
                        if (!continueFlag)
                        {
                            // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある end
                            this.form.parameters.PtSystemId = string.Empty;
                            this.form.parameters.PtSeq = string.Empty;
                            // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
                        }
                        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある end
                        this.form.parameters.Save();
                        // 20140609 katen No.730 規定値機能の追加について start

                        if (this.form.parameters.Mode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG))
                        {
                            // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
                            if (continueFlag)
                            {
                                this.SetAllPtData();
                                if (!this.SetManifestFrom("PtLoad"))
                                {
                                    catchErr = true;
                                    ret = false;
                                    return ret;
                                }
                            }
                            else if (this.SetKiteiValue(Kbn))
                            // if (this.SetKiteiValue())
                            // 20140609 katen No.730 規定値機能の追加について end
                            {
                                this.SetAllPtData();
                                if (!this.SetManifestFrom("PtLoad"))
                                {
                                    catchErr = true;
                                    ret = false;
                                    return ret;
                                }
                            }
                        }
                        this.headerform.old_kyoten = this.headerform.ctxt_KyotenCd.Text;
                        // 20140609 katen No.730 規定値機能の追加について end

                        break;
                }

                //数値計算
                if (!this.SetTotal())
                {
                    catchErr = true;
                    return ret;
                }

                //数値フォーマット適用
                this.SetNumFormst();

                //フォーカス位置を設定
                this.form.cdate_KohuDate.Focus();

                //画面初期表示Flag
                firstLoadFlg = false;

                //紐付情報クリア
                this.maniRelation = null;
                // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
                //伝種区分、連携番号、連携明細行はalphabet入力不可に設定する
                this.form.cantxt_DenshuKbn.AlphabetLimitFlag = false;
                this.form.cantxt_No.AlphabetLimitFlag = false;
                this.form.cantxt_Meisaigyou.AlphabetLimitFlag = false;
                if (!string.IsNullOrEmpty(this.form.cantxt_DenshuKbn.Text))
                {
                    this.SetRenkeiLabel();
                }
                else
                {
                    this.form.lbl_No.Text = "連携番号";
                    this.form.lbl_Misaigyou.Text = "明細行";
                }
                // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

                //VAN 20201125 #143822, #143823, #143824 S
                //// 20140612 ria EV004603_交付番号がブランクで登録後、修正モードで開くと非活性となっており入力が出来ない start
                //if (this.form.parameters.Mode == (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                //{
                //    if (string.IsNullOrEmpty(this.form.cantxt_KohuNo.Text))
                //    {
                //        this.form.crdo_KohuTujyo.Enabled = true;
                //        this.form.crdo_KohuReigai.Enabled = true;
                //        this.form.cantxt_KohuNo.ReadOnly = false;
                //    }
                //}
                //// 20140612 ria EV004603_交付番号がブランクで登録後、修正モードで開くと非活性となっており入力が出来ない  end‏
                //VAN 20201125 #143822, #143823, #143824 E

                switch (this.form.tbc_Sanpai.SelectedIndex)
                {
                    case 0:
                        this.ControlEnabledSet(false);
                        break;
                    case 1:
                        this.ControlEnabledSet(true);
                        break;
                }
                this.form.tbc_SaishuShobunBaai.Refresh();

                if (maniFlag == 1)
                {
                    this.form.cdgrid_Jisseki.AllowUserToAddRows = true;
                }
                else if (maniFlag == 2)
                {
                    // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
                    //this.form.cdgrid_Jisseki.AllowUserToAddRows = false;
                    // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end
                    if (this.form.cdgrid_Jisseki.Rows.Count == 0)
                    {
                        this.form.cdgrid_Jisseki.Rows.Add();
                    }
                }

                this.form.Renkei_Mode_1.Left = 8;
                this.form.Renkei_Mode_2.Left = 170;
                this.form.pl_Mobile.Size = new Size(323, 27);
                this.form.cbtn_Previous.Left = 881;
                this.form.cbtn_Next.Left = 910;
                this.form.pl_Mobile_next.Left = 872;

                //モバイルオプション
                MobileInit();

                // 明細連携区分テキストを不可視にする。
                this.form.cantxt_Renkei_Mode.Visible = false;
                // 明細連携区分の活性/非活性を制御する。
                this.form.MeisaiRenkeiKbnSetting(this.form.cantxt_DenshuKbn.Text);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 読取専用モード(入力不可)か判定
        /// </summary>
        /// <returns></returns>
        private bool IsReadOnlyMode()
        {
            if (this.form.parameters == null)
            {
                return false;
            }

            // 削除モードも読取専用にする際は、下記に条件追加すること
            if (this.form.parameters.Mode == (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 画面内のコントロールに対して活性・非活性の制御を行う
        /// </summary>
        private void EnabledControl()
        {
            var isReadOnlyMode = IsReadOnlyMode();

            // Enabled設定
            if (this.EnabledCtrlList == null)
            {
                this.EnabledCtrlList = GetEnabledControlList();
            }

            foreach (var ctrl in this.EnabledCtrlList)
            {
                ctrl.Enabled = !isReadOnlyMode;
            }

            // ReadOnly設定(対象はデータグリッドビューのみ想定)
            if (this.ReadOnlyColumnList == null)
            {
                this.ReadOnlyColumnList = GetReadOnlyColumnList();
            }

            foreach (var column in this.ReadOnlyColumnList)
            {
                column.ReadOnly = isReadOnlyMode;
            }

            // ヘッダー部分
            this.headerform.ctxt_KyotenCd.Enabled = !isReadOnlyMode;

            // ボタン
            parentbaseform.bt_func9.Enabled = !isReadOnlyMode;      // 登録
            parentbaseform.bt_func10.Enabled = !isReadOnlyMode;     // 契約参照
            parentbaseform.bt_func11.Enabled = !isReadOnlyMode;     // 行削除

            parentbaseform.bt_process1.Enabled = !isReadOnlyMode;   // パターン登録
            parentbaseform.bt_process2.Enabled = !isReadOnlyMode;   // パターン呼出

            if (AppConfig.IsManiLite)
            {
                // マニライト時は、契約参照機能OFF
                parentbaseform.bt_func10.Enabled = false;
            }

            // 常に活性なコントロール
            this.form.cbtn_Previous.Enabled = true;
            this.form.cbtn_Next.Enabled = true;
        }

        /// <summary>
        /// 画面のEnabled設定候補のコントロールリストを取得
        /// </summary>
        /// <returns></returns>
        private List<Control> GetEnabledControlList()
        {
            List<Control> result = new List<Control>();

            if (this.form.allControl == null)
            {
                return result;
            }

            foreach (var ctrl in this.form.allControl)
            {
                var type = ctrl.GetType();

                // 型指定による取得のため、下記以外の新規コントロールが増える場合は条件追加
                if (typeof(CustomTextBox) == type
                    || typeof(CustomAlphaNumTextBox) == type
                    || typeof(CustomNumericTextBox2) == type
                    || typeof(CustomCheckBox) == type
                    || typeof(CustomComboBox) == type
                    || typeof(CustomDateTimePicker) == type
                    || typeof(CustomPopupOpenButton) == type
                    || typeof(CustomButton) == type
                    || typeof(CustomAddressSearchButton) == type
                    || typeof(CustomRadioButton) == type
                    || typeof(CustomPhoneNumberTextBox) == type
                    || typeof(CustomPostalCodeTextBox) == type)
                {
                    if (!ctrl.Enabled)
                    {
                        // 既に非活性のコントロールは除外
                        continue;
                    }

                    if (ctrl is TextBoxBase)
                    {
                        var text = ctrl as TextBoxBase;
                        if (text.ReadOnly)
                        {
                            // ReadOnlyの項目も除外
                            continue;
                        }
                    }

                    result.Add(ctrl);
                }
            }

            return result;
        }

        /// <summary>
        /// 実績一覧からReadOnly設定候補のColumnリストを取得
        /// </summary>
        /// <returns></returns>
        private List<DataGridViewColumn> GetReadOnlyColumnList()
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            // データグリッドビューはReadOnly設定
            foreach (DataGridViewColumn column in this.form.cdgrid_Jisseki.Columns)
            {
                if (column.ReadOnly)
                {
                    // 既にReadOnlyの項目は除外
                    continue;
                }

                result.Add(column);
            }

            return result;
        }

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            LogUtility.DebugMethodStart();

            var allControlAndHeaderControls = allControl.ToList();
            allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headerform));
            var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.form.RegistErrorFlag)
            {
                //必須チェックエラーフォーカス処理
                this.SetErrorFocus();

                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
        /// <summary>
        /// 1次明細の紐付チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean ChkRelationDetail(DataGridViewRow dgr)
        {
            LogUtility.DebugMethodStart();

            if (this.maniFlag != 1)
            {
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
                //LogUtility.DebugMethodEnd(false);
                //return false;
                if (this.mlogic.ChkRelationDetail2(dgr.Cells["DetailSystemID"].Value))
                {
                    this.form.messageShowLogic.MessageBoxShow("E177", "1次");
                    LogUtility.DebugMethodEnd(true);
                    return true;

                }
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
            }
            if (this.mlogic.ChkRelationDetail(dgr.Cells["DetailSystemID"].Value))
            {
                this.form.messageShowLogic.MessageBoxShow("E177", "2次");
                LogUtility.DebugMethodEnd(true);
                return true;

            }
            LogUtility.DebugMethodEnd(false);
            return false;
        }
        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end

        #region 紐付けた一次マニが最終処分終了報告済みかチェック
        /// <summary>
        /// 紐付けた一次マニが最終処分終了報告済みかチェック
        /// </summary>
        /// <param name="nextSysId"></param>
        /// <param name="nextHaikiKbnCd"></param>
        /// <param name="catchErr"></param>
        /// <returns>true:最終処分終了報告済み、false:未報告</returns>
        internal bool IsFixedRelationFirstMani(SqlInt64 nextSysId, out bool catchErr)
        {
            bool isFixedFirstElecMani = false;
            catchErr = false;
            try
            {
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.GetHashCode() == this.form.parameters.Mode)
                {
                    isFixedFirstElecMani = this.mlogic.IsFixedRelationFirstMani(SqlInt64.Parse(this.form.parameters.SystemId), 3);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsFixedRelationFirstMani", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            return isFixedFirstElecMani;
        }
        #endregion

        #region 紐付けた一次マニが最終処分終了報告中かチェック
        /// <summary>
        /// 紐付けた一次マニが最終処分終了報告済みかチェック
        /// </summary>
        /// <param name="nextSysId"></param>
        /// <param name="nextHaikiKbnCd"></param>
        /// <param name="catchErr"></param>
        /// <returns>true:最終処分終了報告中、false:未報告</returns>
        internal bool IsExecutingLastSbnEndRep(SqlInt64 nextSysId, out bool catchErr)
        {
            bool isExecutingFirstElecMani = false;
            catchErr = false;

            try
            {
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.GetHashCode() == this.form.parameters.Mode)
                {
                    isExecutingFirstElecMani = this.mlogic.IsExecutingLastSbnEndRep(SqlInt64.Parse(this.form.parameters.SystemId), 2);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExecutingLastSbnEndRep", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            return isExecutingFirstElecMani;
        }
        #endregion

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
                // 201400707 syunrei EV005144_№4840でかけた制御をはずす　start
                //// 20140617 kayo EV004840 全ての処分終了が入力されないと紐付不可のように修正 start
                //var gcDgvCustom = control as DgvCustom;

                //// 明細内、必須チェック
                //if (gcDgvCustom != null && gcDgvCustom.Name.Equals("cdgrid_Jisseki"))
                //{
                //    foreach (DataGridViewRow row in gcDgvCustom.Rows)
                //    {

                //        if (row.Cells["SyobunEndDate"].Style.BackColor == System.Drawing.Color.FromArgb(255, 100, 100))
                //        {
                //            row.Cells["SyobunEndDate"].Selected = true;
                //            gcDgvCustom.Focus();
                //            break;
                //        }
                //    }
                //}
                //// 20140617 kayo EV004840 全ての処分終了が入力されないと紐付不可のように修正 end
                // 201400707 syunrei EV005144_№4840でかけた制御をはずす　end
            }
            //ヘッダーチェック
            foreach (Control control in this.headerform.allControl)
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
        public void ControlInit(string kbn = "")
        {
            LogUtility.DebugMethodStart(kbn);

            switch (kbn)
            {
                case ("bt_process2"):
                    // パターン呼び出し時
                    //交付年月日
                    this.form.cdate_KohuDate.Value = null;

                    //交付番号
                    this.form.cantxt_KohuNo.Text = string.Empty;
                    this.form.bak_ManifestId = string.Empty;
                    this.form.bak_ManifestKbn = string.Empty;

                    //照合確認日
                    this.form.cdate_SyougouKakuninB1.Value = null;
                    this.form.cdate_SyougouKakuninB2.Value = null;
                    this.form.cdate_SyougouKakuninD.Value = null;
                    this.form.cdate_SyougouKakuninE.Value = null;

                    //運搬の受託（１）
                    this.form.cdate_UnpanJyu1.Value = null;

                    //運搬の受託（２）
                    this.form.cdate_UnpanJyu2.Value = null;

                    //処分の受託（受領）
                    this.form.cdate_SyobunJyu.Value = null;

                    //返却日入力
                    this.form.cdate_HenkyakuA.Value = null;
                    this.form.cdate_HenkyakuB1.Value = null;
                    this.form.cdate_HenkyakuB2.Value = null;
                    this.form.cdate_HenkyakuC1.Value = null;
                    this.form.cdate_HenkyakuC2.Value = null;
                    this.form.cdate_HenkyakuD.Value = null;
                    this.form.cdate_HenkyakuE.Value = null;
                    break;

                default:
                    // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
                    //伝種区分
                    this.form.cantxt_DenshuKbn.Text = string.Empty;
                    this.form.ctxt_DenshuName.Text = string.Empty;
                    // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

                    //売上支払番号
                    this.form.cantxt_No.Text = string.Empty;

                    //売上/支払明細行
                    this.form.cantxt_Meisaigyou.Text = string.Empty;

                    //交付年月日
                    this.form.cdate_KohuDate.Value = null;

                    //交付番号
                    this.form.cantxt_KohuNo.Text = string.Empty;
                    this.form.bak_ManifestId = string.Empty;
                    this.form.bak_ManifestKbn = string.Empty;

                    //照合確認日
                    this.form.cdate_SyougouKakuninB1.Value = null;
                    this.form.cdate_SyougouKakuninB2.Value = null;
                    this.form.cdate_SyougouKakuninD.Value = null;
                    this.form.cdate_SyougouKakuninE.Value = null;

                    //運搬の受託（１）
                    this.form.cdate_UnpanJyu1.Value = null;

                    //運搬の受託（２）
                    this.form.cdate_UnpanJyu2.Value = null;

                    //処分の受託（受領）
                    this.form.cdate_SyobunJyu.Value = null;

                    //返却日入力
                    this.form.cdate_HenkyakuA.Value = null;
                    this.form.cdate_HenkyakuB1.Value = null;
                    this.form.cdate_HenkyakuB2.Value = null;
                    this.form.cdate_HenkyakuC1.Value = null;
                    this.form.cdate_HenkyakuC2.Value = null;
                    this.form.cdate_HenkyakuD.Value = null;
                    this.form.cdate_HenkyakuE.Value = null;

                    break;
            }

            // 運搬終了年月日のReadOnlyを動的に変更しているので、一度falseに戻す。
            this.form.cdate_UnpanJyu1.ReadOnly = false;
            this.form.cdate_UnpanJyu2.ReadOnly = false;

            // 20140604 kayo 不具合#4470　混合種類名と明細が生じされない対応 start
            this.form.parameters.KongoCd = String.Empty;
            // 20140604 kayo 不具合#4470　混合種類名と明細が生じされない対応 end

            //ヘッダー：タイトル
            this.headerform.lb_title.Text = string.Empty;

            //ヘッダー：拠点
            this.headerform.ctxt_KyotenCd.Text = string.Empty;
            this.headerform.ctxt_KyotenMei.Text = string.Empty;
            this.mlogic.SetKyoten(this.headerform.ctxt_KyotenCd, this.headerform.ctxt_KyotenMei);

            //ヘッダー：初回登録
            this.headerform.customTextBox4.Text = string.Empty;
            this.headerform.customTextBox2.Text = string.Empty;

            //ヘッダー：最終更新
            this.headerform.customTextBox3.Text = string.Empty;
            this.headerform.customTextBox1.Text = string.Empty;

            //取引先
            this.form.cantxt_TorihikiCd.Text = string.Empty;
            this.form.ctxt_TorihikiName.Text = string.Empty;

            //事前協議　番号/年月日
            this.form.cntxt_jizenkyougi.Text = string.Empty;
            this.form.cdate_JizenKyougi.Value = null;

            this.form.lineShape6.Visible = false;
            this.form.cntxt_jizenkyougi.Enabled = true;
            this.form.cdate_JizenKyougi.Enabled = true;

            //交付番号
            this.form.crdo_KohuTujyo.Checked = true;
            this.form.crdo_KohuReigai.Checked = false;
            switch (this.form.parameters.Mode)
            {
                case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://更新モード//VAN 20201125 #143822, #143823, #143824
                    this.form.crdo_KohuTujyo.Enabled = true;
                    this.form.crdo_KohuReigai.Enabled = true;
                    this.form.cantxt_KohuNo.ReadOnly = false;
                    break;

                case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                default://その他
                    this.form.crdo_KohuTujyo.Enabled = false;
                    this.form.crdo_KohuReigai.Enabled = false;
                    this.form.cantxt_KohuNo.ReadOnly = true;
                    break;
            }

            //交付担当者
            this.form.ctxt_KohuTantou.Text = string.Empty;
            //交付担当者所属
            this.form.ctxt_KohuTantouShozoku.Text = string.Empty;

            //整理番号
            this.form.cantxt_SeiriNo.Text = string.Empty;

            //排出事業者
            this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
            this.form.ctxt_HaisyutuGyousyaName1.Text = string.Empty;
            this.form.cnt_HaisyutuGyousyaZip.Text = string.Empty;
            this.form.cnt_HaisyutuGyousyaTel.Text = string.Empty;
            this.form.ctxt_HaisyutuGyousyaAdd1.Text = string.Empty;
            // 20140611 katen 不具合No.4469 start‏
            this.form.ctxt_HaisyutuGyousyaName2.Text = string.Empty;
            this.form.ctxt_HaisyutuGyousyaAdd2.Text = string.Empty;
            // 20140611 katen 不具合No.4469 end‏

            //排出事業場
            this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaName1.Text = string.Empty;
            this.form.cnt_HaisyutuJigyoubaZip.Text = string.Empty;
            this.form.cnt_HaisyutuJigyoubaTel.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaAdd1.Text = string.Empty;
            // 20140611 katen 不具合No.4469 start‏
            this.form.ctxt_HaisyutuJigyoubaName2.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaAdd2.Text = string.Empty;
            // 20140611 katen 不具合No.4469 end‏

            //照合確認日
            this.form.lineShape1.Visible = false;
            this.form.cdate_SyougouKakuninB1.Enabled = true;
            this.form.lineShape2.Visible = false;
            this.form.cdate_SyougouKakuninB2.Enabled = true;
            this.form.lineShape3.Visible = false;
            this.form.cdate_SyougouKakuninD.Enabled = true;
            this.form.lineShape4.Visible = false;
            this.form.cdate_SyougouKakuninE.Enabled = true;

            //実績：混合種類
            this.form.cantxt_KongoCd.Text = string.Empty;
            this.form.ctxt_KongoName.Text = string.Empty;

            // 20140613 ria EV004740 混合種類を入力した後に、[F2]新規ボタンを押下すると単位CDが入力不可となってしまう。 start
            this.form.cdgrid_Jisseki.Columns[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = false;
            // 20140613 ria EV004740 混合種類を入力した後に、[F2]新規ボタンを押下すると単位CDが入力不可となってしまう。 end

            //実績：混合数量
            this.form.cntxt_JissekiSuryo.Text = string.Empty;
            this.form.cntxt_JissekiSuryo.CustomFormatSetting = this.ManifestSuuryoFormat;
            this.form.cntxt_JissekiSuryo.FormatSetting = "カスタム";

            //実績：混合単位
            this.form.canTxt_JissekiTaniCd.Text = string.Empty;
            this.form.ctxt_JissekiTaniName.Text = string.Empty;

            //20250402
            this.form.cdate_UnpanDate_1.Value = null;
            this.form.cdate_UnpanDate_2.Value = null;
            this.form.cdate_ShobunJuryoDate.Value = null;
            this.form.cdate_ShobunShuryoDate.Value = null;

            //isClearForm = true => do not check valid on cdgrid_Jisseki when clear form
            this.form.isClearForm = true;
            //実績（グリッド）
            this.form.cdgrid_Jisseki.Rows.Clear();
            //reset variable
            this.form.isClearForm = false;
            this.form.cdgrid_Jisseki.AllowUserToAddRows = true;

            //実績（グリッド）：数量
            var colSuu = this.form.cdgrid_Jisseki.Columns[(int)Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku.enumCols.Suryo] as r_framework.CustomControl.DgvCustomTextBoxColumn;
            colSuu.CustomFormatSetting = this.ManifestSuuryoFormat;
            colSuu.FormatSetting = "カスタム";

            //実績（グリッド）：換算後数量
            var colKansanSuu = this.form.cdgrid_Jisseki.Columns[(int)Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku.enumCols.KansangoSuryo] as r_framework.CustomControl.DgvCustomTextBoxColumn;
            colKansanSuu.CustomFormatSetting = this.ManifestSuuryoFormat;
            colKansanSuu.FormatSetting = "カスタム";

            //実績：割合合計
            this.form.ctxt_TotalWariai.Text = "0";

            //実績：合計数量
            this.form.ctxt_TotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat);

            //実績：換算後数量合計
            // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
            //this.form.ctxt_KansangoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat);
            this.form.ctxt_KansangoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat) + this.unit_name;
            // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end

            //実績：減容後数量合計
            // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
            //this.form.ctxt_GenyoyugoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat);
            this.form.ctxt_GenyoyugoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat) + this.unit_name;
            // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end

            //原本：産業廃棄物の種類 単位
            this.form.crdo_SanpaiSyuruiT.Checked = false;
            this.form.crdo_SanpaiSyuruiKg.Checked = false;
            this.form.crdo_SanpaiSyuruiM3.Checked = false;
            this.form.crdo_SanpaiSyuruiRittoru.Checked = false;
            this.form.cntxt_SanpaiTani.Text = string.Empty;
            this.form.ctxt_TaniName.Text = this.GetTaniName(string.Empty);

            //原本：産業廃棄物の名称
            this.form.cantxt_SanpaiSyuruiCd.Text = string.Empty;
            this.form.ctxt_SanpaiSyuruiName.Text = string.Empty;

            //原本：0100 コンクリートがら
            this.form.cckb_Antei01.Checked = false;
            this.form.cntxt_AnteiSuryo01.Text = string.Empty;
            this.form.cntxt_AnteiSuryo01.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo01.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0200 アスコンがら
            this.form.cckb_Antei02.Checked = false;
            this.form.cntxt_AnteiSuryo02.Text = string.Empty;
            this.form.cntxt_AnteiSuryo02.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo02.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0300 その他がれき類
            this.form.cckb_Antei03.Checked = false;
            this.form.cntxt_AnteiSuryo03.Text = string.Empty;
            this.form.cntxt_AnteiSuryo03.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo03.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0400 ガラス・陶磁器くず
            this.form.cckb_Antei04.Checked = false;
            this.form.cntxt_AnteiSuryo04.Text = string.Empty;
            this.form.cntxt_AnteiSuryo04.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo04.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0500 廃プラスチック類
            this.form.cckb_Antei05.Checked = false;
            this.form.cntxt_AnteiSuryo05.Text = string.Empty;
            this.form.cntxt_AnteiSuryo05.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo05.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0600 金属くず
            this.form.cckb_Antei06.Checked = false;
            this.form.cntxt_AnteiSuryo06.Text = string.Empty;
            this.form.cntxt_AnteiSuryo06.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo06.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0700 混合（安定型のみ）
            this.form.cckb_Antei07.Checked = false;
            this.form.cntxt_AnteiSuryo07.Text = string.Empty;
            this.form.cntxt_AnteiSuryo07.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo07.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：0800 石綿含有産業廃棄物
            this.form.cckb_Antei08.Checked = false;
            this.form.cntxt_AnteiSuryo08.Text = string.Empty;
            this.form.cntxt_AnteiSuryo08.FormatSetting = "カスタム";
            this.form.cntxt_AnteiSuryo08.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free1 安定型品目
            this.form.cckb_AnteiFree01.Checked = false;
            this.form.cantxt_AnteiFreeCd01.Text = string.Empty;
            this.form.ctxt_AnteiFreeName01.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo01.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo01.FormatSetting = "カスタム";
            this.form.cntxt_AnteiFreeSuryo01.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free2 安定型品目
            this.form.cckb_AnteiFree02.Checked = false;
            this.form.cantxt_AnteiFreeCd02.Text = string.Empty;
            this.form.ctxt_AnteiFreeName02.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo02.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo02.FormatSetting = "カスタム";
            this.form.cntxt_AnteiFreeSuryo02.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free3 安定型品目
            this.form.cckb_AnteiFree03.Checked = false;
            this.form.cantxt_AnteiFreeCd03.Text = string.Empty;
            this.form.ctxt_AnteiFreeName03.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo03.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo03.FormatSetting = "カスタム";
            this.form.cntxt_AnteiFreeSuryo03.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free4 安定型品目
            this.form.cckb_AnteiFree04.Checked = false;
            this.form.cantxt_AnteiFreeCd04.Text = string.Empty;
            this.form.ctxt_AnteiFreeNam04.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo04.Text = string.Empty;
            this.form.cntxt_AnteiFreeSuryo04.FormatSetting = "カスタム";
            this.form.cntxt_AnteiFreeSuryo04.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1100 建設汚泥
            this.form.cckb_Kanri01.Checked = false;
            this.form.cntxt_KanriSuryo11.Text = string.Empty;
            this.form.cntxt_KanriSuryo11.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo11.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1200 紙くず
            this.form.cckb_Kanri02.Checked = false;
            this.form.cntxt_KanriSuryo12.Text = string.Empty;
            this.form.cntxt_KanriSuryo12.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo12.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1300 木くず
            this.form.cckb_Kanri03.Checked = false;
            this.form.cntxt_KanriSuryo13.Text = string.Empty;
            this.form.cntxt_KanriSuryo13.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo13.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1400 繊維くず
            this.form.cckb_Kanri04.Checked = false;
            this.form.cntxt_KanriSuryo14.Text = string.Empty;
            this.form.cntxt_KanriSuryo14.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo14.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1500 廃石膏ボード
            this.form.cckb_Kanri05.Checked = false;
            this.form.cntxt_KanriSuryo15.Text = string.Empty;
            this.form.cntxt_KanriSuryo15.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo15.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1600 混合（管理型含む）
            this.form.cckb_Kanri06.Checked = false;
            this.form.cntxt_KanriSuryo16.Text = string.Empty;
            this.form.cntxt_KanriSuryo16.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo16.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：1700 石綿含有産業廃棄物
            this.form.cckb_Kanri17.Checked = false;
            this.form.cntxt_KanriSuryo17.Text = string.Empty;
            this.form.cntxt_KanriSuryo17.FormatSetting = "カスタム";
            this.form.cntxt_KanriSuryo17.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free1 管理型品目
            this.form.cckb_KanriFree01.Checked = false;
            this.form.cantxt_KanriFreeCd01.Text = string.Empty;
            this.form.ctxt_KanriFreeName01.Text = string.Empty;
            this.form.cntxt_KanriFreeSuryo01.Text = string.Empty;
            this.form.cntxt_KanriFreeSuryo01.FormatSetting = "カスタム";
            this.form.cntxt_KanriFreeSuryo01.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free2 管理型品目
            this.form.cckb_KanriFree02.Checked = false;
            this.form.cantxt_KanriFreeCd02.Text = string.Empty;
            this.form.ctxt_KanriFreeName02.Text = string.Empty;
            this.form.cntxt_KanriFreeSuryo02.Text = string.Empty;
            this.form.cntxt_KanriFreeSuryo02.FormatSetting = "カスタム";
            this.form.cntxt_KanriFreeSuryo02.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free3 管理型品目
            this.form.cckb_KanriFree03.Checked = false;
            this.form.cantxt_KanriFreeCd03.Text = string.Empty;
            this.form.ctxt_KanriFreeName03.Text = string.Empty;
            this.form.cntxt_KanriFreeSuryo03.Text = string.Empty;
            this.form.cntxt_KanriFreeSuryo03.FormatSetting = "カスタム";
            this.form.cntxt_KanriFreeSuryo03.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：2100 廃石綿等
            this.form.cckb_Tokubetu21.Checked = false;
            this.form.cntxt_TokubetuSuryo21.Text = string.Empty;
            this.form.cntxt_TokubetuSuryo21.FormatSetting = "カスタム";
            this.form.cntxt_TokubetuSuryo21.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free1 特別管理産廃
            this.form.cckb_TokubetuFree01.Checked = false;
            this.form.cantxt_TokubetuFreeCd01.Text = string.Empty;
            this.form.cantxt_TokubetuFreeName01.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo01.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo01.FormatSetting = "カスタム";
            this.form.cntxt_TokubetuFreeSuryo01.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free2 特別管理産廃
            this.form.cckb_TokubetuFree02.Checked = false;
            this.form.cantxt_TokubetuFreeCd02.Text = string.Empty;
            this.form.cantxt_TokubetuFreeName02.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo02.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo02.FormatSetting = "カスタム";
            this.form.cntxt_TokubetuFreeSuryo02.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free3 特別管理産廃
            this.form.cckb_TokubetuFree03.Checked = false;
            this.form.cantxt_TokubetuFreeCd03.Text = string.Empty;
            this.form.cantxt_TokubetuFreeName03.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo03.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo03.FormatSetting = "カスタム";
            this.form.cntxt_TokubetuFreeSuryo03.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free4 特別管理産廃
            this.form.cckb_TokubetuFree04.Checked = false;
            this.form.cantxt_TokubetuFreeCd04.Text = string.Empty;
            this.form.cantxt_TokubetuFreeName04.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo04.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo04.FormatSetting = "カスタム";
            this.form.cntxt_TokubetuFreeSuryo04.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：Free5 特別管理産廃
            this.form.cckb_TokubetuFree05.Checked = false;
            this.form.cantxt_TokubetuFreeCd05.Text = string.Empty;
            this.form.cantxt_TokubetuFreeName05.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo05.Text = string.Empty;
            this.form.cntxt_TokubetuFreeSuryo05.FormatSetting = "カスタム";
            this.form.cntxt_TokubetuFreeSuryo05.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：総重量または総容量
            this.form.cntxt_TotalJyuryo.Text = string.Empty;
            this.form.cntxt_TotalJyuryo.FormatSetting = "カスタム";
            this.form.cntxt_TotalJyuryo.CustomFormatSetting = ManifestSuuryoFormat;

            //原本：固形状
            this.form.cckb_KeijyoKokei.Checked = false;

            //原本：泥状
            this.form.cckb_KeijyoDoro.Checked = false;

            //原本：液状
            this.form.cckb_KeijyoEkitai.Checked = false;

            //原本：Free1 形状
            this.form.cckb_KeijyoFree01.Checked = false;
            this.form.cantxt_KeijyoFreeCd01.Text = string.Empty;

            //原本：Free2 形状
            this.form.cckb_KeijyoFree02.Checked = false;
            this.form.cantxt_KeijyoFreeCd02.Text = string.Empty;

            //原本：Free3 形状
            this.form.cckb_KeijyoFree03.Checked = false;
            this.form.cantxt_KeijyoFreeCd03.Text = string.Empty;

            //原本：Free4 形状
            this.form.cckb_KeijyoFree04.Checked = false;
            this.form.cantxt_KeijyoFreeCd04.Text = string.Empty;

            //原本：バラ
            this.form.cckb_NisugataBara.Checked = false;

            //原本：コンテナ
            this.form.cckb_NisugataKontena.Checked = false;

            //原本：ドラム缶
            this.form.cckb_NisugataDoramu.Checked = false;

            //原本：袋
            this.form.cckb_NisugataHukuro.Checked = false;

            //原本：Free1 荷姿
            this.form.cckb_NisugataFree01.Checked = false;
            this.form.cantxt_NisugataFreeCd01.Text = string.Empty;
            this.form.cantxt_NisugataFreeName01.Text = string.Empty;

            //原本：Free2 荷姿
            this.form.cckb_NisugataFree02.Checked = false;
            this.form.cantxt_NisugataFreeCd02.Text = string.Empty;
            this.form.cantxt_NisugataFreeName02.Text = string.Empty;

            //原本：Free3 荷姿
            this.form.cckb_NisugataFree03.Checked = false;
            this.form.cantxt_NisugataFreeCd03.Text = string.Empty;
            this.form.cantxt_NisugataFreeName03.Text = string.Empty;

            //中間処理産業廃棄物
            this.form.ccbx_TyukanTyoubo.Checked = false;
            this.form.ccbx_TyukanKisai.Checked = false;
            this.form.ctxt_TyukanHaikibutu.Text = string.Empty;

            this.form.lineShape5.Visible = false;
            this.form.ccbx_TyukanTyoubo.Enabled = true;
            this.form.ccbx_TyukanKisai.Enabled = true;
            this.form.ctxt_TyukanHaikibutu.Enabled = true;

            //最終処分の場所
            this.form.ccbx_SaisyuTyoubo.Checked = false;
            this.form.ccbx_SaisyuKisai.Checked = false;
            this.form.cantxt_SaisyuGyousyaCd.Text = string.Empty;
            this.form.cantxt_SaisyuGyousyaNameCd.Text = string.Empty;
            this.form.cantxt_SaisyuGyousyaName.Text = string.Empty;
            this.form.ctxt_SaisyuGyousyaAdd.Text = string.Empty;

            //運搬受託者（収集運搬業者）（１）
            this.form.cantxt_UnpanJyutaku1NameCd.Text = string.Empty;
            this.form.cantxt_UnpanJyutaku1Name.Text = string.Empty;
            this.form.cnt_UnpanJyutaku1Zip.Text = string.Empty;
            this.form.cnt_UnpanJyutaku1Tel.Text = string.Empty;
            this.form.ctxt_UnpanJyutaku1Add.Text = string.Empty;

            this.form.cantxt_UnpanHouhou1CD.Text = string.Empty;
            this.form.ctxt_UnpanHouhouMei1.Text = string.Empty;

            this.form.cckb_Jyutaku1HokanAri.Checked = true;
            this.form.cckb_Jyutaku1HokanNasi.Checked = false;

            this.form.cantxt_Jyutaku1Syasyu.Text = string.Empty;
            this.form.ctxt_Jyutaku1Syasyu.Text = string.Empty;

            this.form.cantxt_Jyutaku1SyaNo.Text = string.Empty;
            this.form.ctxt_Jyutaku1SyaNo.Text = string.Empty;

            this.form.customTextBox116.Text = string.Empty;//不明
            this.form.customTextBox118.Text = string.Empty;//不明
            this.form.customTextBox120.Text = string.Empty;//不明
            this.form.customTextBox124.Text = string.Empty;//不明
            this.form.customDateTimePicker14.Value = null;//不明
            this.form.customDateTimePicker15.Value = null;//不明

            //運搬受託者（収集運搬業者）（２）
            this.form.cantxt_UnpanJyutaku2NameCd.Text = string.Empty;
            this.form.cantxt_UnpanJyutaku2Name.Text = string.Empty;
            this.form.cnt_UnpanJyutaku2Zip.Text = string.Empty;
            this.form.cnt_UnpanJyutaku2Tel.Text = string.Empty;
            this.form.ctxt_UnpanJyutaku2Add.Text = string.Empty;

            this.form.cantxt_UnpanHouhou2CD.Text = string.Empty;
            this.form.ctxt_UnpanHouhouMei2.Text = string.Empty;

            this.form.cckb_Jyutaku2HokanAri.Checked = true;
            this.form.cckb_Jyutaku2HokanNasi.Checked = false;

            this.form.cantxt_Jyutaku2Syasyu.Text = string.Empty;
            this.form.ctxt_Jyutaku2Syasyu.Text = string.Empty;

            this.form.cantxt_Jyutaku2SyaNo.Text = string.Empty;
            this.form.ctxt_Jyutaku2SyaNo.Text = string.Empty;

            this.form.lineShape8.Visible = false;
            this.form.cantxt_UnpanJyutaku2NameCd.Enabled = true;
            this.form.cantxt_UnpanJyutaku2Name.Enabled = true;
            this.form.cnt_UnpanJyutaku2Zip.Enabled = true;
            this.form.cnt_UnpanJyutaku2Tel.Enabled = true;
            this.form.ctxt_UnpanJyutaku2Add.Enabled = true;

            this.form.cantxt_UnpanHouhou2CD.Enabled = true;
            this.form.ctxt_UnpanHouhouMei2.Enabled = true;

            this.form.cckb_Jyutaku2HokanAri.Enabled = true;
            this.form.cckb_Jyutaku2HokanNasi.Enabled = true;

            this.form.cantxt_Jyutaku2Syasyu.Enabled = true;
            this.form.ctxt_Jyutaku2Syasyu.Enabled = true;

            this.form.cantxt_Jyutaku2SyaNo.Enabled = true;
            this.form.ctxt_Jyutaku2SyaNo.Enabled = true;

            this.form.cbtn_UnpanJyutaku2San.Enabled = true;
            this.form.cbtn_UnpanJyutaku2Del.Enabled = true;

            //処分受託者（処分業者）
            this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
            this.form.cantxt_SyobunJyutakuName.Text = string.Empty;
            this.form.cnt_SyobunJyutakuZip.Text = string.Empty;
            this.form.cnt_SyobunJyutakuTel.Text = string.Empty;
            this.form.ctxt_SyobunJyutakuAdd.Text = string.Empty;

            //運搬先の事業場（処分業者の処理施設）
            this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.form.cantxt_UnpanJyugyobaName.Text = string.Empty;
            this.form.cnt_UnpanJyugyobaZip.Text = string.Empty;
            this.form.cnt_UnpanJyugyobaTel.Text = string.Empty;
            this.form.ctxt_UnpanJyugyobaAdd.Text = string.Empty;

            //処分方法　中間処理
            this.form.cckb_UnpanJigyobaDassui.Checked = false;
            this.form.cckb_UnpanJigyobaSyoukyaku.Checked = false;
            this.form.cckb_UnpanJigyobaHasai.Checked = false;

            this.form.cckb_UnpanJigyobaFree1.Checked = false;
            this.form.cantxt_UnpanJigyobaFreeCd1.Text = string.Empty;
            this.form.ctxt_UnpanJigyobaFreeName1.Text = string.Empty;

            this.form.cckb_UnpanJigyobaFree2.Checked = false;
            this.form.cantxt_UnpanJigyobaFreeCd2.Text = string.Empty;
            this.form.ctxt_UnpanJigyobaFreeName2.Text = string.Empty;

            this.form.cckb_UnpanJigyobaFree3.Checked = false;
            this.form.cantxt_UnpanJigyobaFreeCd3.Text = string.Empty;
            this.form.ctxt_UnpanJigyobaFreeName3.Text = string.Empty;

            this.form.cckb_UnpanJigyobaFree4.Checked = false;
            this.form.cantxt_UnpanJigyobaFreeCd4.Text = string.Empty;
            this.form.ctxt_UnpanJigyobaFreeName4.Text = string.Empty;

            this.form.cckb_UnpanJigyobaFree5.Checked = false;
            this.form.cantxt_UnpanJigyobaFreeCd5.Text = string.Empty;
            this.form.ctxt_UnpanJigyobaFreeName5.Text = string.Empty;

            //処分方法　最終処分
            this.form.cckb_UnpanJigyobaAntei.Checked = false;
            this.form.cckb_UnpanJigyobaKanri.Checked = false;
            this.form.cckb_UnpanJigyobaSyadan.Checked = false;

            //積替え又は保管
            this.form.cantxt_TumiGyoCd.Text = string.Empty;
            this.form.ctxt_TumiGyoName.Text = string.Empty;
            this.form.cantxt_TumiHokaNameCd.Text = string.Empty;
            this.form.ctxt_TumiHokaName.Text = string.Empty;
            this.form.cnt_TumiHokaZip.Text = string.Empty;
            this.form.cnt_TumiHokaTel.Text = string.Empty;
            this.form.ctxt_TumiHokaAdd.Text = string.Empty;

            this.form.lineShape7.Visible = false;
            this.form.cantxt_TumiGyoCd.Enabled = true;
            this.form.ctxt_TumiGyoName.Enabled = true;
            this.form.cantxt_TumiHokaNameCd.Enabled = true;
            this.form.ctxt_TumiHokaName.Enabled = true;
            this.form.cnt_TumiHokaZip.Enabled = true;
            this.form.cnt_TumiHokaTel.Enabled = true;
            this.form.ctxt_TumiHokaAdd.Enabled = true;
            this.form.cbtn_TumiHokaSan.Enabled = true;
            this.form.cbtn_TumiHokaName.Enabled = true;
            this.form.cbtn_TumiGyo.Enabled = true;
            this.form.cbtn_TumiHokaDel.Enabled = true;

            //積替え又は保管　有価物収集
            this.form.cckb_TumiHokaAri.Checked = true;
            this.form.cckb_TumiHokaNasi.Checked = false;

            this.form.cckb_TumiHokaAri.Enabled = true;
            this.form.cckb_TumiHokaNasi.Enabled = true;

            //積替え又は保管　実績数量
            this.form.cntxt_TumiHokaJisseki.Text = string.Empty;
            this.form.cntxt_TumiHokaJisseki.FormatSetting = "カスタム";
            this.form.cntxt_TumiHokaJisseki.CustomFormatSetting = ManifestSuuryoFormat;

            this.form.cckb_TumiHokaT.Checked = false;
            this.form.cckb_TumiHokaM3.Checked = false;

            this.form.cntxt_TumiHokaJisseki.Enabled = true;
            this.form.cckb_TumiHokaT.Enabled = true;
            this.form.cckb_TumiHokaM3.Enabled = true;

            //追加特記事項
            this.form.ctxt_UnpanJigyobaTokki.Text = string.Empty;

            this.form.lineShape9.Visible = false;
            this.form.ctxt_UnpanJigyobaTokki.Enabled = true;

            //運搬の受託（１）
            this.form.cantxt_UnpanJyuCd1.Text = string.Empty;
            this.form.ctxt_UnpanJyuName1.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenCd1.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenName1.Text = string.Empty;

            //20250402
            this.form.cdate_UnpanJyu1.Enabled = false;

            //運搬の受託（２）
            this.form.cantxt_UnpanJyuCd2.Text = string.Empty;
            this.form.ctxt_UnpanJyuName2.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenCd2.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenName2.Text = string.Empty;

            this.form.lineShape10.Visible = false;
            this.form.cantxt_UnpanJyuCd2.Enabled = true;
            this.form.ctxt_UnpanJyuName2.Enabled = true;
            this.form.cantxt_UnpanJyuUntenCd2.Enabled = true;
            this.form.cantxt_UnpanJyuUntenName2.Enabled = true;
            this.form.customButton2.Enabled = true;

            //20250402
            this.form.cdate_UnpanJyu2.Enabled = false;

            //処分の受託（受領）
            this.form.cantxt_SyobunJyuCd.Text = string.Empty;
            this.form.ctxt_SyobunJyuName.Text = string.Empty;
            this.form.cantxt_SyobunJyuUntenCd.Text = string.Empty;
            this.form.cantxt_SyobunJyuUntenName.Text = string.Empty;

            //20250402
            this.form.cdate_SyobunJyu.Enabled = false;

            //処分の受託（処分）
            this.form.cantxt_SyobunSyoCd.Text = string.Empty;
            this.form.ctxt_SyobunSyoName.Text = string.Empty;
            this.form.cantxt_SyobunSyoUntenCd.Text = string.Empty;
            this.form.cantxt_SyobunSyoUntenName.Text = string.Empty;
            this.form.cdate_SyobunSyo.Value = null;

            //最終処分終了日（埋立処分、再生等）
            this.form.cdate_SaisyuSyobunDate.Value = null;
            this.form.ctxt_SaisyuSyobun.Text = string.Empty;

            //最終処分(埋立処分、再生等)を行った場所　所在地/名称(委託契約書記載の最終処分場所については、処分先Noでも可)
            this.form.cantxt_SaisyuSyobunGyoCd.Text = string.Empty;
            this.form.cantxt_SaisyuSyobunbaCD.Text = string.Empty;
            this.form.ctxt_SaisyuSyobunGyoName.Text = string.Empty;
            this.form.ctxt_SaisyuSyobunGyoZip.Text = string.Empty;
            this.form.ctxt_SaisyuBasyoSyozai.Text = string.Empty;

            this.form.ctxt_SaisyuBasyoNo.Text = string.Empty;

            //返却日入力
            //返却日は読み取り専用にする(タブストップもやめる）
            this.form.cdate_HenkyakuA.ReadOnly = false;
            this.form.cdate_HenkyakuB1.ReadOnly = false;
            this.form.cdate_HenkyakuB2.ReadOnly = false;
            this.form.cdate_HenkyakuC1.ReadOnly = false;
            this.form.cdate_HenkyakuC2.ReadOnly = false;
            this.form.cdate_HenkyakuD.ReadOnly = false;
            this.form.cdate_HenkyakuE.ReadOnly = false;

            this.form.cdate_HenkyakuA.TabStop = true;
            this.form.cdate_HenkyakuB1.TabStop = true;
            this.form.cdate_HenkyakuB2.TabStop = true;
            this.form.cdate_HenkyakuC1.TabStop = true;
            this.form.cdate_HenkyakuC2.TabStop = true;
            this.form.cdate_HenkyakuD.TabStop = true;
            this.form.cdate_HenkyakuE.TabStop = true;

            //処理No（ESC）
            parentbaseform.txb_process.Text = "1";

            //数値系コントロール フォーマット設定
            // 単独

            //var cu = new ControlUtility();
            //var all = cu.GetAllControls(this.form);
            //var nums = all.Where(x=> x is CustomNumericTextBox2).Cast<CustomNumericTextBox2>();
            //mlogic.SetupNumericFormat(this.ManifestSuuryoFormat, nums.ToArray()); //デバッグ用 これで候補の一覧作成可能

            mlogic.SetupNumericFormat(this.ManifestSuuryoFormat,
                //実績：混合数量,
                this.form.cntxt_JissekiSuryo,
                //原本：0100 コンクリートがら,
                this.form.cntxt_AnteiSuryo01,
                //原本：0200 アスコンがら,
                this.form.cntxt_AnteiSuryo02,
                //原本：0300 その他がれき類,
                this.form.cntxt_AnteiSuryo03,
                //原本：0400 ガラス・陶磁器くず,
                this.form.cntxt_AnteiSuryo04,
                //原本：0500 廃プラスチック類,
                this.form.cntxt_AnteiSuryo05,
                //原本：0600 金属くず,
                this.form.cntxt_AnteiSuryo06,
                //原本：0700 混合（安定型のみ）,
                this.form.cntxt_AnteiSuryo07,
                //原本：0800 石綿含有産業廃棄物,
                this.form.cntxt_AnteiSuryo08,
                //原本：Free1 安定型品目,
                this.form.cntxt_AnteiFreeSuryo01,
                //原本：Free2 安定型品目,
                this.form.cntxt_AnteiFreeSuryo02,
                //原本：Free3 安定型品目,
                this.form.cntxt_AnteiFreeSuryo03,
                //原本：Free4 安定型品目,
                this.form.cntxt_AnteiFreeSuryo04,
                //原本：1100 建設汚泥,
                this.form.cntxt_KanriSuryo11,
                //原本：1200 紙くず,
                this.form.cntxt_KanriSuryo12,
                //原本：1300 木くず,
                this.form.cntxt_KanriSuryo13,
                //原本：1400 繊維くず,
                this.form.cntxt_KanriSuryo14,
                //原本：1500 廃石膏ボード,
                this.form.cntxt_KanriSuryo15,
                //原本：1600 混合（管理型含む）,
                this.form.cntxt_KanriSuryo16,
                //原本：1700 石綿含有産業廃棄物,
                this.form.cntxt_KanriSuryo17,
                //原本：Free1 管理型品目,
                this.form.cntxt_KanriFreeSuryo01,
                //原本：Free2 管理型品目,
                this.form.cntxt_KanriFreeSuryo02,
                //原本：Free3 管理型品目,
                this.form.cntxt_KanriFreeSuryo03,
                //原本：2100 廃石綿等,
                this.form.cntxt_TokubetuSuryo21,
                //原本：Free1 特別管理産廃,
                this.form.cntxt_TokubetuFreeSuryo01,
                //原本：Free2 特別管理産廃,
                this.form.cntxt_TokubetuFreeSuryo02,
                //原本：Free3 特別管理産廃,
                this.form.cntxt_TokubetuFreeSuryo03,
                //原本：Free4 特別管理産廃,
                this.form.cntxt_TokubetuFreeSuryo04,
                //原本：Free5 特別管理産廃,
                this.form.cntxt_TokubetuFreeSuryo05,
                //原本：総重量または総容量,
                this.form.cntxt_TotalJyuryo,
                //積替え又は保管　実績数量,
                this.form.cntxt_TumiHokaJisseki);


            // DGV
            mlogic.SetupNumericFormatDgv(this.ManifestSuuryoFormat, this.form.GetNumericColumns());

            LogUtility.DebugMethodEnd();

        }

        // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） start‏
        /// <summary>
        /// 「原本」タブのコントロールが入力できるかどうか設定
        /// </summary>
        /// <param name="enabled">入力できるかどうか</param>
        public void ControlEnabledSet(bool enabled)
        {
            if (IsReadOnlyMode())
            {
                // 読取専用時は常に非活性
                enabled = false;
            }

            this.form.ctxt_SaisyuSyobunGyoZip.Enabled = enabled;
            this.form.casbtn_SaisyuSyobun.Enabled = enabled;
            this.form.cbtn_SaisyuBasyoDel.Enabled = enabled;
            this.form.ctxt_SaisyuBasyoSyozai.Enabled = enabled;
            this.form.casbtn_SaisyuBasyo.Enabled = enabled;
            this.form.cantxt_SaisyuSyobunGyoCd.Enabled = enabled;
            this.form.cantxt_SaisyuSyobunbaCD.Enabled = enabled;
            this.form.ctxt_SaisyuSyobunGyoName.Enabled = enabled;
            this.form.ctxt_SaisyuBasyoNo.Enabled = enabled;
        }
        // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） end‏

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, (WINDOW_TYPE)this.form.parameters.Mode);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //切替ボタン(F1)イベント生成
            parentform.bt_func1.Click += new EventHandler(this.form.SetKirikaeFrom);

            //追加ボタン(F2)イベント生成
            parentform.bt_func2.Click += new EventHandler(this.form.SetAddFrom);

            //修正ボタン(F3)イベント生成
            parentform.bt_func3.Click += new EventHandler(this.form.SetUpdateFrom);

            //1次/2次マニフェスト切替ボタン(F4)イベント生成
            parentform.bt_func4.Click += new EventHandler(this.form.SetManifestFrom);

            //連票ボタン(F5)イベント生成
            parentform.bt_func5.Click += new EventHandler(this.form.SetRenhyouFrom);

            //単票ボタン(F6)イベント生成
            parentform.bt_func6.Click += new EventHandler(this.form.SetTahyouFrom);

            //一覧ボタン(F7)イベント生成
            parentform.bt_func7.Click += new EventHandler(this.form.SetItiranFrom);

            //状況ボタン(F8)イベント生成
            parentform.bt_func8.Click += new EventHandler(this.form.SetJokyoFrom);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentform.bt_func9);
            parentform.bt_func9.Click += new EventHandler(this.form.SetRegistFrom);
            parentform.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //契約参照ボタン(F10)イベント生成
            parentform.bt_func10.Click += new EventHandler(this.form.SetKeiyakuFrom);

            //契約参照ボタン(F11)イベント生成
            parentform.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.FormClose);

            //パターン登録イベント生成
            parentform.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //パターン呼出イベント生成
            parentform.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            //1次マニ紐付イベント生成
            parentform.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);

            //最終処分終了報告イベント生成
            parentform.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);

            //最終処分終了報告の取消イベント生成
            parentform.bt_process5.Click += new EventHandler(this.form.bt_process5_Click);

            //ESCテキストイベント生成
            parentform.txb_process.KeyDown += new KeyEventHandler(this.form.txb_process_Enter);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();

            //業者コード入力時、現場をクリアするイベントの紐付
            this.GyousyaCd_Validated_EventInit();


            //正式名称対応


            var dto = new JoinMethodDto();
            var searchDto = new SearchConditionsDto();
            searchDto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
            searchDto.Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS;
            searchDto.LeftColumn = "TEKIYOU_FLG";
            searchDto.Value = "FALSE";
            dto.SearchCondition.Add(searchDto);
            //廃棄物種類
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_AnteiFreeCd01, this.form.ctxt_AnteiFreeName01, Int16.Parse(this.form.HaikiKbnCD), "0801", "1099");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_AnteiFreeCd02, this.form.ctxt_AnteiFreeName02, Int16.Parse(this.form.HaikiKbnCD), "0801", "1099");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_AnteiFreeCd03, this.form.ctxt_AnteiFreeName03, Int16.Parse(this.form.HaikiKbnCD), "0801", "1099");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_AnteiFreeCd04, this.form.ctxt_AnteiFreeNam04, Int16.Parse(this.form.HaikiKbnCD), "0801", "1099");

            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_KanriFreeCd01, this.form.ctxt_KanriFreeName01, Int16.Parse(this.form.HaikiKbnCD), "1701", "2099");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_KanriFreeCd02, this.form.ctxt_KanriFreeName02, Int16.Parse(this.form.HaikiKbnCD), "1701", "2099");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_KanriFreeCd03, this.form.ctxt_KanriFreeName03, Int16.Parse(this.form.HaikiKbnCD), "1701", "2099");

            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_TokubetuFreeCd01, this.form.cantxt_TokubetuFreeName01, Int16.Parse(this.form.HaikiKbnCD), "2101", "9999");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_TokubetuFreeCd02, this.form.cantxt_TokubetuFreeName02, Int16.Parse(this.form.HaikiKbnCD), "2101", "9999");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_TokubetuFreeCd03, this.form.cantxt_TokubetuFreeName03, Int16.Parse(this.form.HaikiKbnCD), "2101", "9999");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_TokubetuFreeCd04, this.form.cantxt_TokubetuFreeName04, Int16.Parse(this.form.HaikiKbnCD), "2101", "9999");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_SHURUI, this.form.cantxt_TokubetuFreeCd05, this.form.cantxt_TokubetuFreeName05, Int16.Parse(this.form.HaikiKbnCD), "2101", "9999");
            dto = new JoinMethodDto();
            dto.Join = JOIN_METHOD.WHERE;
            dto.LeftTable = "M_HAIKI_SHURUI";
            dto.SearchCondition.Add(searchDto);
            this.form.cantxt_AnteiFreeCd01.popupWindowSetting.Add(dto);
            this.form.cantxt_AnteiFreeCd02.popupWindowSetting.Add(dto);
            this.form.cantxt_AnteiFreeCd03.popupWindowSetting.Add(dto);
            this.form.cantxt_AnteiFreeCd04.popupWindowSetting.Add(dto);
            this.form.cantxt_KanriFreeCd01.popupWindowSetting.Add(dto);
            this.form.cantxt_KanriFreeCd02.popupWindowSetting.Add(dto);
            this.form.cantxt_KanriFreeCd03.popupWindowSetting.Add(dto);
            this.form.cantxt_TokubetuFreeCd01.popupWindowSetting.Add(dto);
            this.form.cantxt_TokubetuFreeCd02.popupWindowSetting.Add(dto);
            this.form.cantxt_TokubetuFreeCd03.popupWindowSetting.Add(dto);
            this.form.cantxt_TokubetuFreeCd04.popupWindowSetting.Add(dto);
            this.form.cantxt_TokubetuFreeCd05.popupWindowSetting.Add(dto);

            //単位 独特のため要個別対応
            //ManifestoLogic.SetupXxxCd(WINDOW_ID.M_UNIT, this.form.cntxt_SanpaiTani, this.form.ctxt_TaniName, Int16.Parse(this.form.HaikiKbnCD), null, null);

            //形状
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_KEIJOU, this.form.cantxt_KeijyoFreeCd01, this.form.customTextBox116, Int16.Parse(this.form.HaikiKbnCD), "01", "99");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_KEIJOU, this.form.cantxt_KeijyoFreeCd02, this.form.customTextBox118, Int16.Parse(this.form.HaikiKbnCD), "01", "99");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_KEIJOU, this.form.cantxt_KeijyoFreeCd03, this.form.customTextBox120, Int16.Parse(this.form.HaikiKbnCD), "01", "99");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_KEIJOU, this.form.cantxt_KeijyoFreeCd04, this.form.customTextBox124, Int16.Parse(this.form.HaikiKbnCD), "01", "99");
            dto = new JoinMethodDto();
            dto.Join = JOIN_METHOD.WHERE;
            dto.LeftTable = "M_KEIJOU";
            dto.SearchCondition.Add(searchDto);
            this.form.cantxt_KeijyoFreeCd01.popupWindowSetting.Add(dto);
            this.form.cantxt_KeijyoFreeCd02.popupWindowSetting.Add(dto);
            this.form.cantxt_KeijyoFreeCd03.popupWindowSetting.Add(dto);
            this.form.cantxt_KeijyoFreeCd04.popupWindowSetting.Add(dto);

            //荷姿
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_NISUGATA, this.form.cantxt_NisugataFreeCd01, this.form.cantxt_NisugataFreeName01, Int16.Parse(this.form.HaikiKbnCD), "01", "99");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_NISUGATA, this.form.cantxt_NisugataFreeCd02, this.form.cantxt_NisugataFreeName02, Int16.Parse(this.form.HaikiKbnCD), "01", "99");
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_NISUGATA, this.form.cantxt_NisugataFreeCd03, this.form.cantxt_NisugataFreeName03, Int16.Parse(this.form.HaikiKbnCD), "01", "99");

            //産業廃棄物の名称
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_HAIKI_NAME, this.form.cantxt_SanpaiSyuruiCd, this.form.ctxt_SanpaiSyuruiName, Int16.Parse(this.form.HaikiKbnCD), null, null);
            dto = new JoinMethodDto();
            dto.Join = JOIN_METHOD.WHERE;
            dto.LeftTable = "M_HAIKI_NAME";
            dto.SearchCondition.Add(searchDto);
            this.form.cantxt_SanpaiSyuruiCd.popupWindowSetting.Add(dto);

            //処分方法
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHOBUN_HOUHOU, this.form.cantxt_UnpanJigyobaFreeCd1, this.form.ctxt_UnpanJigyobaFreeName1, Int16.Parse(this.form.HaikiKbnCD), null, null);
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHOBUN_HOUHOU, this.form.cantxt_UnpanJigyobaFreeCd2, this.form.ctxt_UnpanJigyobaFreeName2, Int16.Parse(this.form.HaikiKbnCD), null, null);
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHOBUN_HOUHOU, this.form.cantxt_UnpanJigyobaFreeCd3, this.form.ctxt_UnpanJigyobaFreeName3, Int16.Parse(this.form.HaikiKbnCD), null, null);
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHOBUN_HOUHOU, this.form.cantxt_UnpanJigyobaFreeCd4, this.form.ctxt_UnpanJigyobaFreeName4, Int16.Parse(this.form.HaikiKbnCD), null, null);
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHOBUN_HOUHOU, this.form.cantxt_UnpanJigyobaFreeCd5, this.form.ctxt_UnpanJigyobaFreeName5, Int16.Parse(this.form.HaikiKbnCD), null, null);

            //運搬受託者:運搬方法
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_UNPAN_HOUHOU, this.form.cantxt_UnpanHouhou1CD, this.form.ctxt_UnpanHouhouMei1, Int16.Parse(this.form.HaikiKbnCD), null, null);
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_UNPAN_HOUHOU, this.form.cantxt_UnpanHouhou2CD, this.form.ctxt_UnpanHouhouMei2, Int16.Parse(this.form.HaikiKbnCD), null, null);

            //車種
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHASHU, this.form.cantxt_Jyutaku1Syasyu, this.form.ctxt_Jyutaku1Syasyu, Int16.Parse(this.form.HaikiKbnCD), null, null);
            ManifestoLogic.SetupXxxCd(WINDOW_ID.M_SHASHU, this.form.cantxt_Jyutaku2Syasyu, this.form.ctxt_Jyutaku2Syasyu, Int16.Parse(this.form.HaikiKbnCD), null, null);
            dto = new JoinMethodDto();
            dto.Join = JOIN_METHOD.WHERE;
            dto.LeftTable = "M_SHASHU";
            dto.SearchCondition.Add(searchDto);
            this.form.cantxt_Jyutaku1Syasyu.popupWindowSetting.Add(dto);
            this.form.cantxt_Jyutaku2Syasyu.popupWindowSetting.Add(dto);

            //車輌
            ManifestoLogic.SetupSyaryouCd(
                this.form.cantxt_Jyutaku1SyaNo, this.form.ctxt_Jyutaku1SyaNo,
                this.form.cantxt_Jyutaku1Syasyu, this.form.ctxt_Jyutaku1Syasyu,
                this.form.cantxt_UnpanJyutaku1NameCd, this.form.cantxt_UnpanJyutaku1Name, this.form.cnt_UnpanJyutaku1Tel, this.form.ctxt_UnpanJyutaku1Add, this.form.cnt_UnpanJyutaku1Zip
                // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                , this.form.cantxt_UnpanJyuCd1, this.form.ctxt_UnpanJyuName1
                // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                , this.form.cdate_KohuDate);
            ManifestoLogic.SetupSyaryouCd(
                this.form.cantxt_Jyutaku2SyaNo, this.form.ctxt_Jyutaku2SyaNo,
                this.form.cantxt_Jyutaku2Syasyu, this.form.ctxt_Jyutaku2Syasyu,
                this.form.cantxt_UnpanJyutaku2NameCd, this.form.cantxt_UnpanJyutaku2Name, this.form.cnt_UnpanJyutaku2Tel, this.form.ctxt_UnpanJyutaku2Add, this.form.cnt_UnpanJyutaku2Zip
                // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                , this.form.cantxt_UnpanJyuCd2, this.form.ctxt_UnpanJyuName2
                // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                , this.form.cdate_KohuDate);
            var paramDto = new PopupSearchSendParamDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                KeyName = "GYOUSHA_TEKIYOU_FLG",
                Control = "cdate_KohuDate"
            };

            var paramDto2 = new PopupSearchSendParamDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN",
                Value = "TRUE"

            };

            var paramDto3 = new PopupSearchSendParamDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                KeyName = "GYOUSHAKBN_MANI",
                Value = "TRUE"
            };

            this.form.cantxt_Jyutaku1SyaNo.PopupSearchSendParams.Add(paramDto);
            this.form.cantxt_Jyutaku2SyaNo.PopupSearchSendParams.Add(paramDto);
            this.form.cantxt_Jyutaku1SyaNo.PopupSearchSendParams.Add(paramDto2);
            this.form.cantxt_Jyutaku2SyaNo.PopupSearchSendParams.Add(paramDto2);
            this.form.cantxt_Jyutaku1SyaNo.PopupSearchSendParams.Add(paramDto3);
            this.form.cantxt_Jyutaku2SyaNo.PopupSearchSendParams.Add(paramDto3);

            //社員マスタ系
            //　運転
            ManifestoLogic.SetupShainCd(this.form.cantxt_UnpanJyuUntenCd1, this.form.cantxt_UnpanJyuUntenName1, ManifestoLogic.SHAIN_KBN.UNTEN_KBN);
            ManifestoLogic.SetupShainCd(this.form.cantxt_UnpanJyuUntenCd2, this.form.cantxt_UnpanJyuUntenName2, ManifestoLogic.SHAIN_KBN.UNTEN_KBN);
            dto = new JoinMethodDto();
            dto.Join = JOIN_METHOD.WHERE;
            dto.LeftTable = "M_UNTENSHA";
            dto.SearchCondition.Add(searchDto);
            this.form.cantxt_UnpanJyuUntenCd1.popupWindowSetting.Add(dto);
            this.form.cantxt_UnpanJyuUntenCd2.popupWindowSetting.Add(dto);

            //　処分
            ManifestoLogic.SetupShainCd(this.form.cantxt_SyobunJyuUntenCd, this.form.cantxt_SyobunJyuUntenName, ManifestoLogic.SHAIN_KBN.SHOBUN_TANTOU_KBN);
            ManifestoLogic.SetupShainCd(this.form.cantxt_SyobunSyoUntenCd, this.form.cantxt_SyobunSyoUntenName, ManifestoLogic.SHAIN_KBN.SHOBUN_TANTOU_KBN);

            //交付番号とラジオボタン
            mlogic.SetupKoufuNo(this.form.crdo_KohuTujyo, this.form.crdo_KohuReigai, this.form.cantxt_KohuNo);
            this.form.crdo_KohuTujyo.Checked = false;
            this.form.crdo_KohuTujyo.Checked = true; //強制変更して プロパティをセット。

            //交付年月日 Validatingイベント
            this.form.cdate_KohuDate.Validating += new System.ComponentModel.CancelEventHandler(this.form.cdate_KohuDate_Validating);
            //運搬終了日 Validatingイベント
            this.form.cdate_UnpanJyu1.Validating += new System.ComponentModel.CancelEventHandler(this.form.cdate_UnpanJyu1_Validating);
            this.form.cdate_UnpanJyu2.Validating += new System.ComponentModel.CancelEventHandler(this.form.cdate_UnpanJyu2_Validating);
            this.form.cantxt_UnpanJigyobaFreeCd1.Validating += new System.ComponentModel.CancelEventHandler(this.form.cantxt_UnpanJigyobaFreeCd1_Validating);
            this.form.cantxt_UnpanJigyobaFreeCd2.Validating += new System.ComponentModel.CancelEventHandler(this.form.cantxt_UnpanJigyobaFreeCd2_Validating);
            this.form.cantxt_UnpanJigyobaFreeCd3.Validating += new System.ComponentModel.CancelEventHandler(this.form.cantxt_UnpanJigyobaFreeCd3_Validating);
            this.form.cantxt_UnpanJigyobaFreeCd4.Validating += new System.ComponentModel.CancelEventHandler(this.form.cantxt_UnpanJigyobaFreeCd4_Validating);
            this.form.cantxt_UnpanJigyobaFreeCd5.Validating += new System.ComponentModel.CancelEventHandler(this.form.cantxt_UnpanJigyobaFreeCd5_Validating);

            //Add popupWindowSetting
            this.AddPopupWindowSetting();

            //Receive message from subapp event refs #158004
            if (AppConfig.AppOptions.IsInxsManifest())
            {
                parentform.OnReceiveMessageEvent += new BaseBaseForm.OnReceiveMessage(Parentform_OnReceiveMessageEvent);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // ファンクション4,8,10(2次マニ、状況、契約参照)機能の非表示

            // ボタン初期化
            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;
            parentform.bt_func4.Text = string.Empty;
            parentform.bt_func8.Text = string.Empty;
            parentform.bt_func10.Text = string.Empty;
            parentform.bt_func4.Enabled = false;
            parentform.bt_func8.Enabled = false;
            parentform.bt_func10.Enabled = false;

            // イベント削除
            //1次/2次マニフェスト切替ボタン(F4)イベント
            parentform.bt_func4.Click -= new EventHandler(this.form.SetManifestFrom);
            //状況ボタン(F8)イベント
            parentform.bt_func8.Click -= new EventHandler(this.form.SetJokyoFrom);
            //契約参照ボタン(F10)イベント
            parentform.bt_func10.Click -= new EventHandler(this.form.SetKeiyakuFrom);
        }

        #endregion

        #region チェックロジック


        /// <summary>
        /// 廃棄物種類のチェック
        /// </summary>
        /// <param name="obj1">コードコントロール</param>
        /// <param name="obj2">名称コントロール</param>
        /// <returns></returns>
        public int ChkHaiki(object obj1, object obj2)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(obj1, obj2);

                TextBox txtCd = (TextBox)obj1;
                TextBox txtName = (TextBox)obj2;
                if (txtCd.Text == string.Empty)
                {
                    ret = 1;
                    txtName.Text = string.Empty;
                    return ret;
                }

                GetHaikiShuruiDtoCls search = new GetHaikiShuruiDtoCls();
                search.HAIKI_KBN_CD = "2";
                search.HAIKI_SHURUI_CD = txtCd.Text;

                var dt = GetHaikiShuruiDao.GetDataForEntity(search);

                if (dt.Rows.Count > 0)
                {
                    txtCd.Text = dt.Rows[0]["HAIKI_SHURUI_CD"].ToString();
                    txtName.Text = dt.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"].ToString();

                }
                else
                {
                    this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    txtCd.Focus();
                    txtCd.SelectAll();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaiki", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }


        /// 交付番号入力チェック
        /// </summary>
        /// <returns>true:異常 false:正常</returns>
        public bool ChkKohuNo()
        {
            string ret = string.Empty;
            try
            {
                LogUtility.DebugMethodStart();
                ret = ManifestoLogic.ChkKoufuNo(this.form.cantxt_KohuNo.Text, this.form.crdo_KohuTujyo.Checked);

                if (!string.IsNullOrEmpty(ret))
                {
                    //エラー時は自前で表示
                    Message.MessageBoxUtility.MessageBoxShowError(ret);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkKohuNo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = string.Format(Message.MessageUtility.GetMessageString("E245"));
            }
            finally
            {
                LogUtility.DebugMethodEnd(!string.IsNullOrEmpty(ret));
            }
            return !string.IsNullOrEmpty(ret);
        }


        /// <summary>
        /// 文字数チェック
        /// </summary>
        public bool ChkTxtLength()
        {
            LogUtility.DebugMethodStart();

            var lName = new List<r_framework.CustomControl.CustomTextBox>();
            var lName1 = new List<r_framework.CustomControl.CustomTextBox>();
            var lAdd = new List<r_framework.CustomControl.CustomTextBox>();

            //排出事業者　名称
            lName.Add(this.form.ctxt_HaisyutuGyousyaName1);

            //排出事業者　住所
            lAdd.Add(this.form.ctxt_HaisyutuGyousyaAdd1);

            //排出事業場　名称
            lName.Add(this.form.ctxt_HaisyutuJigyoubaName1);

            //排出事業場　住所
            lAdd.Add(this.form.ctxt_HaisyutuJigyoubaAdd1);

            // 20140611 katen 不具合No.4469 start‏
            //排出事業者　名称
            lName.Add(this.form.ctxt_HaisyutuGyousyaName2);

            //排出事業者　住所
            lAdd.Add(this.form.ctxt_HaisyutuGyousyaAdd2);

            //排出事業場　名称
            lName.Add(this.form.ctxt_HaisyutuJigyoubaName2);

            //排出事業場　住所
            lAdd.Add(this.form.ctxt_HaisyutuJigyoubaAdd2);
            // 20140611 katen 不具合No.4469 end‏

            //最終処分の場所　名称
            lName.Add(this.form.cantxt_SaisyuGyousyaName);

            //最終処分の場所　住所
            lAdd.Add(this.form.ctxt_SaisyuGyousyaAdd);

            //運搬受託者（区間１）　名称
            lName.Add(this.form.cantxt_UnpanJyutaku1Name);

            //運搬受託者（区間１）　住所
            lAdd.Add(this.form.ctxt_UnpanJyutaku1Add);

            //運搬受託者（区間２）　名称
            lName.Add(this.form.cantxt_UnpanJyutaku2Name);

            //運搬受託者（区間２）　住所
            lAdd.Add(this.form.ctxt_UnpanJyutaku2Add);

            //処分受託者　名称
            lName.Add(this.form.cantxt_SyobunJyutakuName);

            //処分受託者　住所
            lAdd.Add(this.form.ctxt_SyobunJyutakuAdd);

            //運搬の受託者（区間１）　名称
            //lName.Add(this.form.ctxt_UnpanJyuName1);
            lName1.Add(this.form.ctxt_UnpanJyuName1);

            //運搬の受託者（区間２）　名称
            //lName.Add(this.form.ctxt_UnpanJyuName2);
            lName1.Add(this.form.ctxt_UnpanJyuName2);

            //処分の受託者（受領）　名称
            //lName.Add(this.form.ctxt_SyobunJyuName);
            lName1.Add(this.form.ctxt_SyobunJyuName);

            //処分の受託者（処分）　名称
            //lName.Add(this.form.ctxt_SyobunSyoName);
            lName1.Add(this.form.ctxt_SyobunSyoName);

            //運搬先の事業場　名称
            lName.Add(this.form.cantxt_UnpanJyugyobaName);

            //運搬先の事業場　住所
            lAdd.Add(this.form.ctxt_UnpanJyugyobaAdd);

            //積替え又は保管　業者名称
            lName.Add(this.form.ctxt_TumiGyoName);

            //積替え又は保管　名称
            lName.Add(this.form.ctxt_TumiHokaName);

            //積替え又は保管　住所
            lAdd.Add(this.form.ctxt_TumiHokaAdd);

            //最終処分を行った場所　名称
            lName.Add(this.form.ctxt_SaisyuSyobunGyoName);

            //最終処分を行った場所　住所
            lAdd.Add(this.form.ctxt_SaisyuBasyoSyozai);

            //名称(正式名称1 + 正式名称2)
            Boolean ErrName = false;
            var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E152");
            if (this.mlogic.ChkNameLength(lName, 80))
            {
                msg1 = string.Format(msg1, "名称", "40");
                ErrName = true;
            }

            //名称(正式名称1のみ)
            Boolean ErrName1 = false;
            var msg3 = Shougun.Core.Message.MessageUtility.GetMessageString("E152");
            if (this.mlogic.ChkNameLength(lName1, 40))
            {
                msg3 = string.Format(msg3, "名称", "20");
                ErrName1 = true;
            }

            //住所(都道府県名称 + 住所1 +住所2)
            Boolean ErrAdd = false;
            var msg2 = Shougun.Core.Message.MessageUtility.GetMessageString("E152");
            if (this.mlogic.ChkAddLength(lAdd))
            {
                msg2 = string.Format(msg2, "住所", "44");
                ErrAdd = true;
            }

            if (ErrName == true && ErrName1 == true && ErrAdd == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1 + Environment.NewLine + msg3 + Environment.NewLine + msg2);
                return true;
            }
            else if (ErrName == true && ErrName1 == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1 + Environment.NewLine + msg3);
                return true;
            }
            else if (ErrName == true && ErrAdd == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1 + Environment.NewLine + msg2);
                return true;
            }
            else if (ErrName1 == true && ErrAdd == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg3 + Environment.NewLine + msg2);
                return true;
            }
            else if (ErrName == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
                return true;
            }
            else if (ErrName1 == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg3);
                return true;
            }
            else if (ErrAdd == true)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg2);
                return true;
            }
            LogUtility.DebugMethodEnd();
            return false;
        }

        // 20140604 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
        /// <summary>
        /// 登録前チェック
        /// </summary>
        public bool ManifestCheckRegist()
        {
            LogUtility.DebugMethodStart();

            if (maniFlag == 2)
            {
                if (!string.IsNullOrEmpty(this.form.parameters.SystemId))
                {
                    Int64 systemId = Convert.ToInt64(this.form.parameters.SystemId);
                    var entitys = this.Relationdao.GetDataBySystemId(systemId, 2);

                    if (entitys != null && entitys.Length > 0 && string.IsNullOrEmpty(this.form.cantxt_KohuNo.Text))
                    {
                        Message.MessageBoxUtility.MessageBoxShow("E001", "交付番号");
                        this.form.cantxt_KohuNo.Focus();
                        LogUtility.DebugMethodEnd();
                        return true;
                    }
                }
            }

            string msg = string.Empty;

            //交付年月日
            if (string.IsNullOrEmpty(this.form.cdate_KohuDate.Text))
            {
                msg += "、";
                msg += "交付年月日";
            }

            //交付番号
            if (string.IsNullOrEmpty(this.form.cantxt_KohuNo.Text))
            {
                msg += "、";
                msg += "交付番号";
            }

            //排出事業者CD
            if (string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text))
            {
                msg += "、";
                msg += "排出事業者CD";
            }

            //排出事業場CD
            if (string.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
            {
                msg += "、";
                msg += "排出事業場CD";
            }

            //grid
            int ret = 0;
            if (this.form.cdgrid_Jisseki.Rows.Count <= 1)
            {
                ret = this.form.cdgrid_Jisseki.Rows.Count;
            }
            else
            {
                ret = this.form.cdgrid_Jisseki.Rows.Count - 1;
            }

            for (int i = 0; i < ret; i++)
            {
                //廃棄物種類CD
                if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value)))
                {
                    msg += "、";
                    msg += "廃棄物種類CD" + "(" + (i + 1) + "行目)";
                }

                //数量
                if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value)))
                {
                    msg += "、";
                    msg += "数量" + "(" + (i + 1) + "行目)";
                }

                //単位CD
                if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value)))
                {
                    msg += "、";
                    msg += "単位CD" + "(" + (i + 1) + "行目)";
                }

                //換算後数量
                if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)))
                {
                    msg += "、";
                    msg += "換算後数量" + "(" + (i + 1) + "行目)";
                }

                //処分方法CD
                if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value)))
                {
                    msg += "、";
                    msg += "処分方法CD" + "(" + (i + 1) + "行目)";
                }
            }

            //運搬受託者(区間1)CD
            if (string.IsNullOrEmpty(this.form.cantxt_UnpanJyutaku1NameCd.Text))
            {
                msg += "、";
                msg += "運搬受託者(区間1)CD";
            }

            //運搬受託者(区間2)CD
            //if (string.IsNullOrEmpty(this.form.cantxt_UnpanJyutaku2NameCd.Text))
            //{
            //    msg += "、";
            //    msg += "運搬受託者(区間2)CD";
            //}

            //処分受託者CD
            if (string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
            {
                msg += "、";
                msg += "処分受託者CD";
            }

            if (string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
            {
                msg += "、";
                msg += "運搬先の事業場CD";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                if (Message.MessageBoxUtility.MessageBoxShow("C061", msg.Substring(1)) != DialogResult.Yes)
                {
                    LogUtility.DebugMethodEnd();
                    return true;
                }
            }

            LogUtility.DebugMethodEnd();
            return false;
        }
        // 20140604 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end

        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkGyosya(object obj, string colname)
        {
            TextBox txt = (TextBox)obj;
            try
            {
                LogUtility.DebugMethodStart(obj, colname);

                if (txt.Text == string.Empty)
                {
                    return 1;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt.Text;
                Serch.GYOUSHAKBN_MANI = "1";
                //最終処分業者の場合、最終処分場区分の条件を追加した
                if (txt != null && (txt.Name == this.form.cantxt_SaisyuGyousyaCd.Name || txt.Name == this.form.cantxt_SaisyuSyobunGyoCd.Name))
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                }
                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenbaAll(Serch);
                if (dt.Rows.Count > 0)
                {
                    SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.cdate_KohuDate.Text) && (DateTime.TryParse(this.form.cdate_KohuDate.Text, out date)))
                    {
                        tekiyou = date;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            SqlDateTime from = SqlDateTime.Null;
                            SqlDateTime to = SqlDateTime.Null;
                            string begin = Convert.ToString(dt.Rows[i]["TEKIYOU_BEGIN"]);
                            string end = Convert.ToString(dt.Rows[i]["TEKIYOU_END"]);
                            if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                            {
                                from = date;
                            }
                            if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                            {
                                to = date;
                            }

                            if (from.IsNull && to.IsNull)
                            {
                                return 0;
                            }
                            else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                            {
                                return 0;
                            }
                            else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                            {
                                return 0;
                            }
                            else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                    && tekiyou.CompareTo(to) <= 0)
                            {
                                return 0;
                            }
                        }
                    }
                }
                this.form.messageShowLogic.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyosya", ex);
                if (ex is SQLRuntimeException)
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
                LogUtility.DebugMethodEnd(txt, colname);
            }
            return 2;
        }

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="genba">現場CD</param>
        /// <param name="gyosya">事業者CD</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkJigyouba(object genba, object gyosya, string colname)
        {
            TextBox txt1 = (TextBox)genba;
            TextBox txt2 = (TextBox)gyosya;
            try
            {
                LogUtility.DebugMethodStart(genba, gyosya, colname);

                //空
                if (txt1.Text == string.Empty)
                {
                    return 1;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GENBA_CD = txt1.Text;
                Serch.GYOUSHA_CD = txt2.Text;

                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        break;

                    case 1:
                        SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.cdate_KohuDate.Text) && (DateTime.TryParse(this.form.cdate_KohuDate.Text, out date)))
                        {
                            tekiyou = date;
                        }
                        if (dt.Rows[0][colname].ToString() == "True")
                        {
                            SqlDateTime from = SqlDateTime.Null;
                            SqlDateTime to = SqlDateTime.Null;
                            string begin = Convert.ToString(dt.Rows[0]["TEKIYOU_BEGIN"]);
                            string end = Convert.ToString(dt.Rows[0]["TEKIYOU_END"]);
                            if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                            {
                                from = date;
                            }
                            if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                            {
                                to = date;
                            }

                            if (from.IsNull && to.IsNull)
                            {
                                txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                return 0;
                            }
                            else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                            {
                                txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                return 0;
                            }
                            else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                            {
                                txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                return 0;
                            }
                            else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                    && tekiyou.CompareTo(to) <= 0)
                            {
                                txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                return 0;
                            }
                            else
                            {
                                this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                                break;
                            }
                        }
                        this.form.messageShowLogic.MessageBoxShow("E058");
                        break;

                    default:
                        switch (colname)
                        {
                            case "HAISHUTSU_NIZUMI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "排出事業者");
                                break;

                            case "SAISHUU_SHOBUNJOU_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "最終処分の業者");
                                break;

                            case "SHOBUN_NIOROSHI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "処分受託者");
                                break;

                            case "TSUMIKAEHOKAN_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "積換保管事業者");
                                break;

                            case "UNPAN_JUTAKUSHA_KAISHA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "運搬の受託者");
                                break;
                        }
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJigyouba", ex);
                if (ex is SQLRuntimeException)
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
                LogUtility.DebugMethodEnd(genba, gyosya, colname);
            }
            return 2;
        }

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者) ※複数可
        /// </summary>
        /// <param name="genba">現場CD</param>
        /// <param name="gyosya">事業者CD</param>
        /// <param name="colNames">
        /// チェックカラム名称の配列
        /// （エラーで出力される文言は一番最初の要素に依存します）
        /// </param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkJigyouba(object genba, object gyosya, string[] colNames)
        {
            try
            {
                LogUtility.DebugMethodStart(genba, gyosya, colNames);

                TextBox txt1 = (TextBox)genba;
                TextBox txt2 = (TextBox)gyosya;

                //空
                if (txt1.Text == string.Empty)
                {
                    return 1;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GENBA_CD = txt1.Text;
                Serch.GYOUSHA_CD = txt2.Text;

                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        break;

                    case 1:
                        SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.cdate_KohuDate.Text) && (DateTime.TryParse(this.form.cdate_KohuDate.Text, out date)))
                        {
                            tekiyou = date;
                        }
                        for (int i = 0; colNames.Count() > i; i++)
                        {
                            if (dt.Rows[0][colNames[i]].ToString() == "True")
                            {
                                SqlDateTime from = SqlDateTime.Null;
                                SqlDateTime to = SqlDateTime.Null;
                                string begin = Convert.ToString(dt.Rows[0]["TEKIYOU_BEGIN"]);
                                string end = Convert.ToString(dt.Rows[0]["TEKIYOU_END"]);
                                if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                                {
                                    from = date;
                                }
                                if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                                {
                                    to = date;
                                }

                                if (from.IsNull && to.IsNull)
                                {
                                    txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                    txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                    return 0;
                                }
                                else if (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                                {
                                    txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                    txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                    return 0;
                                }
                                else if (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                                {
                                    txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                    txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                    return 0;
                                }
                                else if (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                        && tekiyou.CompareTo(to) <= 0)
                                {
                                    txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                    txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                    return 0;
                                }
                            }
                        }

                        this.form.messageShowLogic.MessageBoxShow("E058");
                        break;

                    default:
                        switch (colNames[0])
                        {
                            case "HAISHUTSU_NIZUMI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "排出事業者");
                                break;

                            case "SAISHUU_SHOBUNJOU_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "最終処分の業者");
                                break;

                            case "SHOBUN_NIOROSHI_GENBA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "処分受託者");
                                break;

                            case "TSUMIKAEHOKAN_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "積換保管事業者");
                                break;

                            case "UNPAN_JUTAKUSHA_KAISHA_KBN":
                                this.form.messageShowLogic.MessageBoxShow("E034", "運搬の受託者");
                                break;
                        }
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJigyouba", ex);
                if (ex is SQLRuntimeException)
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
                LogUtility.DebugMethodEnd(genba, gyosya, colNames);
            }
            return 2;
        }

        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
        /// <summary>
        /// 伝種区分入力チェック
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns>true：正常　false：エラー</returns>
        public int ChkDenshuKbn(object denshuKbn)
        {
            int intRet = 0;
            try
            {
                LogUtility.DebugMethodStart(denshuKbn);
                TextBox txt1 = (TextBox)denshuKbn;
                if (string.IsNullOrEmpty(txt1.Text))
                {
                    intRet = 1;
                }
                else
                {
                    //伝種区分入力が有る場合
                    bool inputKbnError = true;
                    //伝種区分に1,2,3,100,140のいずれかの値が入力された場合、フラグをセットし、連携番号のみ入力が無い場合用文字列に対する内容を設定する
                    if (this.maniFlag == 1)
                    {
                        switch (Convert.ToInt32(txt1.Text))
                        {
                            case (int)DENSHU_KBN.UKEIRE: //受入
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            case (int)DENSHU_KBN.URIAGE_SHIHARAI://売上支払
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            case (int)DENSHU_KBN.UKETSUKE://受付
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            // Ver 2.6では計量入力リリースのため表示する
                            case (int)DENSHU_KBN.KEIRYOU://計量
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            // Ver2.1では定期実績のマニ連携は非表示
                            // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                            //case (int)DENSHU_KBN.TEIKI_JISSEKI://定期実績
                            //    this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                            //    inputKbnError = false;
                            //    break;
                            // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                            default:
                                this.form.lbl_No.Text = "連携番号";
                                break;
                        }
                    }
                    else
                    {
                        switch (Convert.ToInt32(txt1.Text))
                        {
                            case (int)DENSHU_KBN.SHUKKA: //出荷
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            case (int)DENSHU_KBN.UKETSUKE://受付
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            // Ver 2.6では計量入力リリースのため表示する
                            case (int)DENSHU_KBN.KEIRYOU://計量
                                this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                                inputKbnError = false;
                                break;
                            // Ver2.1では定期実績のマニ連携は非表示
                            // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                            //case (int)DENSHU_KBN.TEIKI_JISSEKI://定期実績
                            //    this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                            //    inputKbnError = false;
                            //    break;
                            // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                            default:
                                this.form.lbl_No.Text = "連携番号";
                                break;
                        }
                    }
                    if (inputKbnError)
                    {
                        //伝種区分に1,2,3,100,140以外の値が入力された場合、「伝種区分は不正な値です。」というErrorMessageを表示する。
                        this.form.messageShowLogic.MessageBoxShow("E084", "伝種区分");
                        this.form.ctxt_DenshuName.Text = string.Empty;
                        intRet = 2;
                        //エラーあり
                        txt1.Focus();
                        txt1.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkDenshuKbn", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                intRet = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(intRet);
            }
            return intRet;
        }

        /// <summary>
        /// 連携明細行入力チェック
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="renkeiNo">連携番号</param>
        /// <param name="renkeiMeisaigyo">連携明細行</param>
        /// <param name="isRelevantPart">該当フラグ</param>
        /// <param name="isChange">変更フラグ</param>
        /// <returns>1:正常　10:正常（存在チェック不要）　11:(値リセット不要)　98:空(検索処理不要)　99：エラー </returns>
        public int ChkRenkeiMeisaigyo(object denshuKbn, object renkeiNo, object renkeiMeisaigyo, Boolean isRelevantPart, Boolean isChange)
        {
            LogUtility.DebugMethodStart(denshuKbn, renkeiNo, renkeiMeisaigyo, isRelevantPart, isChange);
            TextBox txt1 = (TextBox)denshuKbn;
            TextBox txt2 = (TextBox)renkeiNo;
            TextBox txt3 = (TextBox)renkeiMeisaigyo;

            int intRet = 0;
            //伝種区分、連携番号、連携明細行は入力したかどうか判断用
            //伝種区分を入力した場合は「1XX」、入力しなかった場合は「0XX」
            //連携番号を入力した場合は「X1X」、入力しなかった場合は「X0X」
            //連携明細行を入力した場合は「XX1」、入力しなかった場合は「XX0」
            String emptyCheck = (string.IsNullOrEmpty(txt1.Text) ? "0" : "1") + (string.IsNullOrEmpty(txt2.Text) ? "0" : "1") + (string.IsNullOrEmpty(txt3.Text) ? "0" : "1");

            switch (emptyCheck)
            {
                case "000":
                    //伝種区分、連携番号、連携行番号の入力が無い場合
                    //何もしない。
                    intRet = 98;
                    break;
                case "001":
                //伝種区分と連携番号の入力が無い場合（＝連携行番号のみ入力が有る場合）
                case "011":
                //伝種区分のみ入力が無い場合
                case "010":
                    //伝種区分と連携行番号の入力が無い場合（＝連携番号のみ入力が有る場合）
                    if (isRelevantPart)
                    {
                        // フォーカスは該当項目にいる
                        return 98;
                    }
                    else
                    {
                        // フォーカスは該当項目にいない
                        this.form.messageShowLogic.MessageBoxShow("E051", "伝種区分");
                        this.form.cantxt_DenshuKbn.Focus();
                        intRet = 99;
                        break;
                    }

                case "100":
                    //連携番号と連携行番号の入力が無い場合（＝伝種区分のみ入力が有る場合）
                    intRet = 98;
                    break;
                case "101":
                    //連携番号のみ入力が無い場合
                    if (isRelevantPart)
                    {
                        // フォーカスは該当項目にいる
                        return 98;
                    }
                    else
                    {
                        this.SearchResult = new DataTable();
                        this.SearchString = new SerchParameterDtoCls();
                        this.SearchString.RENKEI_DENSHU_KBN_CD = txt1.Text;
                        this.SearchResult = GetDenshuKbnNameDataDao.GetDataForEntity(SearchString);
                        string result = this.SearchResult.Rows.Count > 0 ? this.SearchResult.Rows[0]["DENSHU_KBN_NAME_RYAKU"].ToString() : "連携";
                        this.form.messageShowLogic.MessageBoxShow("E051", result + "番号");
                        this.form.cantxt_No.Focus();
                        intRet = 99;
                    }

                    break;
                case "110":
                // 20140605 ria No.679 伝種区分連携 start
                ////連携行番号のみ入力が無い場合
                //if (Convert.ToInt32(txt1.Text) != (int)DENSHU_KBN.UKETSUKE)
                //{
                //    //受付ではない場合
                //    if (!isRelevantPart)
                //    {
                //        // フォーカスは該当項目にいない
                //        //伝種区分が100（受付）以外の場合、「明細の行番号の値を入力してください。」というErrorMessageを表示する。
                //        this.form.messageShowLogic.MessageBoxShow("E051", "明細行");
                //        this.form.cantxt_Meisaigyou.Focus();
                //        intRet = 99;
                //    }
                //    else
                //    {
                //        // フォーカスは該当項目にいる
                //        intRet = 98;
                //    }
                //}
                //else
                //{
                //    //受付である場合
                //    if (!isRelevantPart)
                //    {
                //        // フォーカスは該当項目にいない
                //        if (isChange)
                //        {
                //            //値が変更されている
                //            intRet = 1;
                //        }
                //        else
                //        {
                //            //値が変更されてない
                //            intRet = 11;
                //        }
                //    }
                //    else
                //    {
                //        // フォーカスは該当項目にいる
                //        if (isChange)
                //        {
                //            //値が変更されている
                //            intRet = 10;
                //        }
                //        else
                //        {
                //            //値が変更されてない
                //            intRet = 98;
                //        }
                //    }

                //}
                //break;
                // 20140605 ria No.679 伝種区分連携 end
                case "111":
                    //伝種区分、連携番号、連携明細行の3項目全てに値が入力されている場合
                    if (!isRelevantPart)
                    {
                        // フォーカスは該当項目にいない
                        if (isChange)
                        {
                            //値が変更されている
                            intRet = 1;
                        }
                        else
                        {
                            //値が変更されてない
                            intRet = 11;
                        }
                    }
                    else
                    {
                        // フォーカスは該当項目にいる
                        if (isChange)
                        {
                            //値が変更されている
                            intRet = 10;
                        }
                        else
                        {
                            //値が変更されてない
                            intRet = 98;
                        }
                    }
                    break;
                default:
                    break;
            }
            LogUtility.DebugMethodEnd(denshuKbn, renkeiNo, renkeiMeisaigyo, isRelevantPart, isChange);
            return intRet;
        }
        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

        // 20140529 katen No.679 伝種区分、連携番号、連携明細行連携 end‏
        /// <summary>
        /// 連携明細行入力チェック
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="renkeiNo">連携番号</param>
        /// <param name="renkeiMeisaigyo">連携明細行</param>
        /// <param name="isRelevantPart">該当フラグ</param>
        /// <param name="isChange">変更フラグ</param>
        public bool setDetail(object denshuKbn, object renkeiNo, object renkeiMeisaigyo, Boolean isRelevantPart, Boolean isChange)
        {
            bool ret = true;
            try
            {
                String errorCD = "";
                this.isNotNeedDeleteFlg = true;

                switch (this.ChkRenkeiMeisaigyo(denshuKbn, renkeiNo, renkeiMeisaigyo, isRelevantPart, isChange))
                {
                    case 1://正常
                        //伝種区分、連携番号、連携明細行の3項目全てに値が入力されている、又は伝種区分=100の場合は、伝種区分、連携番号の2項目に値が入力された場合
                        errorCD = this.GetRenkeiData(denshuKbn, renkeiNo, renkeiMeisaigyo, true, 1);
                        if (!string.IsNullOrEmpty(errorCD))
                        {
                            if (!(errorCD == "C134")) 
                            {
                                this.form.messageShowLogic.MessageBoxShow(errorCD);
                            }
                            switch (errorCD)
                            {
                                case "C134":
                                    
                                    DataTable dt = new DataTable();
                                    
                                    if (maniFlag == 1)
                                    {
                                        if (this.form.ismobile_mode && this.form.Renkei_Mode_2.Checked)
                                        {
                                            // 受入実績明細連携の場合 実績以外のデータだけ取得
                                            TextBox txt2 = (TextBox)renkeiNo;
                                            TextBox txt3 = (TextBox)renkeiMeisaigyo;
                                            GetKenpaiDataDtoCls search = new GetKenpaiDataDtoCls();
                                            search.RENKEI_NUMBER = Convert.ToDecimal(txt2.Text);
                                            search.RENKEI_ROW_NO = string.IsNullOrEmpty(txt3.Text) ? (int?)null : Convert.ToInt32(txt3.Text);
                                            search.RENKEI_MANI_KBN = maniFlag;
                                            dt = GetKenpaiDataDao.GetUkeireForRenkei(search);
                                            // 計量の場合
                                            if (Convert.ToInt32(((TextBox)denshuKbn).Text) == (Int32)DENSHU_KBN.KEIRYOU)
                                            {
                                                dt = GetKenpaiDataDao.GetKeiryoForRenkei(search);
                                            }
                                        }
                                    }
                                    // 確認メッセージの表示
                                    var result = this.form.messageShowLogic.MessageBoxShow(errorCD);

                                    // 確認メッセージで「いいえ」の場合
                                    if (!(result == DialogResult.Yes))
                                    {
                                        this.form.cantxt_No.Focus();
                                        break;
                                    }
                                    // 実績以外のデータが存在する場合
                                    if (dt.Rows.Count > 0)
                                    {
                                        // 画面の連携項目クリア
                                        this.ClearGamenItem();
                                        // 画面項目の設定
                                        this.SetValueToFormRenkeiData(dt, denshuKbn);
                                    }
                                    break;
                                // 20140617 katen 不具合No.4712 start‏
                                case "E175":
                                    // 20140617 katen 不具合No.4712 end‏
                                    this.form.cantxt_No.Focus();
                                    break;
                                case "E173":
                                    if ((!this.form.ismobile_mode) || (this.form.ismobile_mode && (!(this.form.cantxt_Renkei_Mode.Focused || this.form.Renkei_Mode_1.Focused || this.form.Renkei_Mode_2.Focused))))
                                    {
                                        this.form.cantxt_Meisaigyou.Focus();
                                    }
                                    break;
                            }
                        }
                        break;
                    case 10://正常　存在チェック不要
                        // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                        if (Convert.ToInt32(((TextBox)denshuKbn).Text) == (Int32)DENSHU_KBN.TEIKI_JISSEKI)
                        {
                            //  画面の連携項目をクリアする
                            this.ClearGamenItemForTeikiJisseki();
                        }
                        else
                        {
                            //  画面の連携項目をクリアする
                            this.ClearGamenItem();
                        }
                        // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                        //伝種区分、連携番号、連携明細行の3項目全てに値が入力されている、又は伝種区分=100の場合は、伝種区分、連携番号の2項目に値が入力された場合
                        this.GetRenkeiData(denshuKbn, renkeiNo, renkeiMeisaigyo, true, 1);
                        break;
                    case 11://正常　値のリセット不要
                        //伝種区分、連携番号、連携明細行の3項目全てに値が入力されている、又は伝種区分=100の場合は、伝種区分、連携番号の2項目に値が入力された場合
                        errorCD = this.GetRenkeiData(denshuKbn, renkeiNo, renkeiMeisaigyo, false, 1);
                        if (!string.IsNullOrEmpty(errorCD))
                        {
                            if (!(errorCD == "C134"))
                            {
                                this.form.messageShowLogic.MessageBoxShow(errorCD);
                            }
                            switch (errorCD)
                            {
                                case "C134":
                                    
                                    DataTable dt = new DataTable();
                                    
                                    if (maniFlag == 1)
                                    {
                                        if (this.form.ismobile_mode && this.form.Renkei_Mode_2.Checked)
                                        {
                                            // 受入実績明細連携の場合 実績以外のデータだけ取得
                                            TextBox txt2 = (TextBox)renkeiNo;
                                            TextBox txt3 = (TextBox)renkeiMeisaigyo;
                                            GetKenpaiDataDtoCls search = new GetKenpaiDataDtoCls();
                                            search.RENKEI_NUMBER = Convert.ToDecimal(txt2.Text);
                                            search.RENKEI_ROW_NO = string.IsNullOrEmpty(txt3.Text) ? (int?)null : Convert.ToInt32(txt3.Text);
                                            search.RENKEI_MANI_KBN = maniFlag;
                                            dt = GetKenpaiDataDao.GetUkeireForRenkei(search);
                                            // 計量の場合
                                            if (Convert.ToInt32(((TextBox)denshuKbn).Text) == (Int32)DENSHU_KBN.KEIRYOU)
                                            {
                                                dt = GetKenpaiDataDao.GetKeiryoForRenkei(search);
                                            }
                                        }
                                    }
                                    // 確認メッセージの表示
                                    var result = this.form.messageShowLogic.MessageBoxShow(errorCD);

                                    // 確認メッセージで「いいえ」の場合
                                    if (!(result == DialogResult.Yes))
                                    {
                                        this.form.cantxt_No.Focus();
                                        break;
                                    }
                                    // 実績以外のデータが存在する場合
                                    if (dt.Rows.Count > 0)
                                    {
                                        // 画面項目の初期化
                                        this.ClearGamenItem();
                                        // 画面項目の設定
                                        this.SetValueToFormRenkeiData(dt, denshuKbn);
                                    }
                                    break;
                                // 20140617 katen 不具合No.4712 start‏
                                case "E175":
                                    // 20140617 katen 不具合No.4712 end‏
                                    this.form.cantxt_No.Focus();
                                    break;
                                case "E173":
                                    if ((!this.form.ismobile_mode) || (this.form.ismobile_mode && (!(this.form.cantxt_Renkei_Mode.Focused || this.form.Renkei_Mode_1.Focused || this.form.Renkei_Mode_2.Focused))))
                                    {
                                        this.form.cantxt_Meisaigyou.Focus();
                                    }
                                    break;
                            }
                        }
                        // 20140611 ria No.679 伝種区分連携 end
                        break;
                }
                this.isNotNeedDeleteFlg = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDetail", ex);
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
            return ret;
        }


        // 20140529 katen No.679 伝種区分、連携番号、連携明細行連携 end‏
        #endregion

        #region 画面処理

        /// <summary>
        /// 処理実行メソッド
        /// </summary>
        public virtual int DoProcess(KeyEventArgs e)
        {
            int iret = 0;
            bool catchErr = false;

            try
            {
                LogUtility.DebugMethodStart(e);
                if (e.KeyData != Keys.Enter)
                {
                    return iret;
                }
                if (parentbaseform.txb_process.Text.Trim() == "1" &&
                    parentbaseform.bt_process1.Enabled == true)
                {
                    this.UpdatePattern(out catchErr);
                    if (catchErr)
                    {
                        iret = -1;
                    }
                    else
                    {
                        iret = 1;
                    }
                }
                else if (parentbaseform.txb_process.Text.Trim() == "2" &&
                    parentbaseform.bt_process2.Enabled == true)
                {
                    if (this.CallPattern(out catchErr))
                    {
                        iret = 2;
                    }
                    if (catchErr) { iret = -1; }
                }
                else if (parentbaseform.txb_process.Text.Trim() == "3" &&
                    parentbaseform.bt_process3.Enabled == true)
                {
                    if (this.ManiHimozuke())
                    {
                        iret = 3;
                    }
                    else
                    {
                        iret = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DoProcess", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                iret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(iret);
            }
            return iret;
        }

        /// <summary>
        /// システム設定入力から返却日入力の入力可否を設定
        /// </summary>
        public virtual void SetSysInfo()
        {
            LogUtility.DebugMethodStart();
            DataTable dt = null;
            // 初期処理
            dt = SerchSysInfo();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].ItemArray[0].ToString() == "2")
                {
                    this.form.cdate_HenkyakuA.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuA.Enabled = true;
                }
                if (dt.Rows[0].ItemArray[1].ToString() == "2")
                {
                    this.form.cdate_HenkyakuB1.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuB1.Enabled = true;
                }
                if (dt.Rows[0].ItemArray[2].ToString() == "2")
                {
                    this.form.cdate_HenkyakuB2.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuB2.Enabled = true;
                }
                if (dt.Rows[0].ItemArray[3].ToString() == "2")
                {
                    this.form.cdate_HenkyakuC1.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuC1.Enabled = true;
                }
                if (dt.Rows[0].ItemArray[4].ToString() == "2")
                {
                    this.form.cdate_HenkyakuC2.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuC2.Enabled = true;
                }
                if (dt.Rows[0].ItemArray[5].ToString() == "2")
                {
                    this.form.cdate_HenkyakuD.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuD.Enabled = true;
                }
                if (dt.Rows[0].ItemArray[6].ToString() == "2")
                {
                    this.form.cdate_HenkyakuE.Enabled = false;
                }
                else
                {
                    this.form.cdate_HenkyakuE.Enabled = true;
                }
                this.form.ctxt_TaniName.Text = dt.Rows[0].ItemArray[7].ToString();

                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                this.unit_name = dt.Rows[0].ItemArray[7].ToString();
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end

                // 20140604 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
                this.manifest_validation_check = dt.Rows[0].ItemArray[8].ToString();
                // 20140604 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end

                //計量・受入から連携する明細設定(1:明細、2:実績明細)
                if (this.form.parameters.Mode.Equals((int)WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    if (DBNull.Value.Equals(dt.Rows[0].ItemArray[9]))
                    {
                        this.form.cantxt_Renkei_Mode.Text = "1";
                    }
                    else
                    {
                        if (int.Parse(dt.Rows[0].ItemArray[9].ToString()) == 2)
                        {
                            this.form.cantxt_Renkei_Mode.Text = "2";
                        }
                        else
                        {
                            this.form.cantxt_Renkei_Mode.Text = "1";
                        }
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ヘッダーフォームの設定
        /// タイトルラベル、ラベルの背景色を変更
        /// </summary>
        public void SetHeader(string strTitleName, Color BackColor)
        {
            LogUtility.DebugMethodStart(strTitleName, BackColor);
            this.headerform.lb_title.Text = strTitleName;
            this.headerform.lb_title.BackColor = BackColor;
            this.headerform.lbl_Kyoten.BackColor = BackColor;
            this.headerform.lbl_syokaitouroku.BackColor = BackColor;
            this.headerform.lbl_lastupdate.BackColor = BackColor;
            LogUtility.DebugMethodEnd(strTitleName, BackColor);
        }

        /// <summary>
        /// ラベルの背景色を変更
        /// </summary>
        /// <param name="BackColor">設定する色</param>
        public void SetBody(Color BackColor)
        {
            LogUtility.DebugMethodStart(BackColor);

            parentbaseform.lb_process.BackColor = BackColor;
            this.form.cdgrid_Jisseki.ColumnHeadersDefaultCellStyle.BackColor = BackColor;
            this.form.lbl_Torihikisaki.BackColor = BackColor;
            // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
            this.form.lbl_DenshuKbn.BackColor = BackColor;
            // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏
            this.form.lbl_No.BackColor = BackColor;
            this.form.lbl_Misaigyou.BackColor = BackColor;
            this.form.cbtn_JizenKyougi.BackColor = BackColor;
            this.form.lbl_KohuDate.BackColor = BackColor;
            this.form.lbl_KohuNo.BackColor = BackColor;
            this.form.lbl_KohuTantou.BackColor = BackColor;
            this.form.lbl_SeiriNo.BackColor = BackColor;
            this.form.lbl_HaisyutuGyousya.BackColor = BackColor;
            this.form.lbl_HaisyutuJigyouba.BackColor = BackColor;
            this.form.lbl_SyougouKakunin.BackColor = BackColor;
            this.form.cbtn_SyougouKakuninB1.BackColor = BackColor;
            this.form.cbtn_SyougouKakuninB2.BackColor = BackColor;
            this.form.cbtn_SyougouKakuninD.BackColor = BackColor;
            this.form.cbtn_SyougouKakuninE.BackColor = BackColor;
            this.form.lbl_SanpaiTani.BackColor = BackColor;
            this.form.lbl_SanpaiSyurui.BackColor = BackColor;
            this.form.lbl_keijyo.BackColor = BackColor;
            this.form.lbl_Nisugata.BackColor = BackColor;
            this.form.lbl_Antei1.BackColor = BackColor;
            this.form.lbl_Suryo.BackColor = BackColor;
            this.form.lbl_Antei2.BackColor = BackColor;
            this.form.lbl_Suryo2.BackColor = BackColor;
            this.form.lbl_Kanri.BackColor = BackColor;
            this.form.lbl_Suryo3.BackColor = BackColor;
            this.form.lbl_Kanri2.BackColor = BackColor;
            this.form.lbl_Suryo4.BackColor = BackColor;
            this.form.lbl_Tokubetu.BackColor = BackColor;
            this.form.lbl_Suryo5.BackColor = BackColor;
            this.form.lbl_KongoSyurui.BackColor = BackColor;
            this.form.lbl_JissekiSuryo.BackColor = BackColor;
            this.form.lbl_Suryo5.BackColor = BackColor;
            this.form.lbl_JissekiTani.BackColor = BackColor;
            this.form.lbl_TotalWariai.BackColor = BackColor;
            this.form.lbl_TotalSuryo.BackColor = BackColor;
            this.form.lbl_KansangoTotalSuryo.BackColor = BackColor;
            this.form.lbl_GenyoyugoTotalSuryo.BackColor = BackColor;
            this.form.cbtn_TyukanHaikibutu.BackColor = BackColor;
            this.form.lbl_SaisyuSyobun.BackColor = BackColor;
            this.form.lbl_UnpanJyutaku1.BackColor = BackColor;
            this.form.btn_UnpanJyutaku2.BackColor = BackColor;
            this.form.lbl_UnpanJigyoba.BackColor = BackColor;
            this.form.lbl_Hokan1.BackColor = BackColor;
            this.form.lbl_Jyutaku1SyaNo.BackColor = BackColor;
            this.form.lbl_Jyutaku1Syasyu.BackColor = BackColor;
            this.form.lbl_Hokan2.BackColor = BackColor;
            this.form.lbl_Jyutaku2SyaNo.BackColor = BackColor;
            this.form.lbl_Jyutaku2Syasyuyu.BackColor = BackColor;
            this.form.lbl_UnpanJigyobaSyobun.BackColor = BackColor;
            this.form.lbl_UnpanJigyobaTukan.BackColor = BackColor;
            this.form.lbl_UnpanJigyobaSaisyu.BackColor = BackColor;
            this.form.lbl_SyobunJyutaku.BackColor = BackColor;
            this.form.cbtn_TumiHoka.BackColor = BackColor;
            this.form.lbl_Yukabutu.BackColor = BackColor;
            this.form.cbtn_TumiHokaJissekiSyuryo.BackColor = BackColor;
            this.form.cbtn_UnpanJigyobaTokki.BackColor = BackColor;
            this.form.lbl_UnpanJyu1.BackColor = BackColor;
            this.form.cbtn_UnpanJyu2.BackColor = BackColor;
            this.form.btn_UnpanJyutaku2.BackColor = BackColor;
            this.form.lbl_SyobunJyu.BackColor = BackColor;
            this.form.lbl_SyobunSyo.BackColor = BackColor;
            this.form.lbl_SaisyuSyobunDate.BackColor = BackColor;
            this.form.lbl_SaisyuBasyo.BackColor = BackColor;
            this.form.lbl_Henkyaku.BackColor = BackColor;
            this.form.lbl_HenkyakuA.BackColor = BackColor;
            this.form.lbl_HenkyakuB1.BackColor = BackColor;
            this.form.lbl_HenkyakuB2.BackColor = BackColor;
            this.form.lbl_HenkyakuC1.BackColor = BackColor;
            this.form.lbl_HenkyakuC2.BackColor = BackColor;
            this.form.lbl_HenkyakuD.BackColor = BackColor;
            this.form.lbl_HenkyakuE.BackColor = BackColor;
            // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
            if (this.controlList != null && this.controlList.Count > 0)
            {
                (this.controlList[0] as Label).BackColor = BackColor;
            }
            // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある end

            if (this.manifestMercuryList != null && this.manifestMercuryList.Count > 0)
            {
                (this.manifestMercuryList[0] as Label).BackColor = BackColor;
            }

            if (this.manifestKamiList != null && this.manifestKamiList.Count > 0)
            {
                (this.manifestKamiList[0] as Label).BackColor = BackColor;
            }
            LogUtility.DebugMethodEnd(BackColor);
        }

        /// <summary>
        /// 1次/2次マニフェスト設定
        /// </summary>
        public virtual bool SetManifestFrom(String Kbn)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart();

                //タイトル
                string strTitelName = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_KENPAI_MANIFEST);
                this.form.cdate_KohuDate.IsInputErrorOccured = false;
                this.form.cdate_KohuDate.BackColor = Constans.NOMAL_COLOR;

                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
                /*
                if (maniFlag == 1 && Kbn == "F4" && this.form.cdgrid_Jisseki.Rows.Count > 2)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult ret = msgLogic.MessageBoxShow("C055", "二次マニフェストは有効な明細が1行のみとなるため、2行目以降を削除");
                    if (ret == DialogResult.Yes)
                    {
                        DataGridViewRow row;
                        for (int i = this.form.cdgrid_Jisseki.Rows.Count - 1; i > 0; i--)
                        {
                            row = this.form.cdgrid_Jisseki.Rows[i];
                            if (row.IsNewRow)
                            {
                                continue;
                            }
                            this.form.cdgrid_Jisseki.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
                 */
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

                //背景色
                Color BackColor = Color.FromArgb(0, 105, 51);
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                strTitelName = strTitelName + "二次";
                                BackColor = Color.FromArgb(0, 51, 160);
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                strTitelName = strTitelName + "一次";
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                strTitelName = strTitelName + "一次";
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                strTitelName = strTitelName + "二次";
                                BackColor = Color.FromArgb(0, 51, 160);
                                break;
                        }
                        break;
                }
                //②2次マニは自社が排出事業者となる為、返送管理の必要が無いので、2次マニの返却日入力の項目は削除 2014.05.15 by胡 start→
                //返却日入力の情報表示可否
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え                         
                                this.form.customPanel24.Visible = false;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード                          
                                this.form.customPanel24.Visible = true;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え                            
                                this.form.customPanel24.Visible = true;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード                          
                                this.form.customPanel24.Visible = false;
                                break;
                        }
                        break;
                }
                //②2次マニは自社が排出事業者となる為、返送管理の必要が無いので、2次マニの返却日入力の項目は削除 2014.05.15 by胡 end←

                //process3ボタンの活性
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                switch (this.form.parameters.Mode)
                                {
                                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        parentbaseform.bt_process3.Text = "[3]1次マニ紐付"; //2013.11.22 naitou update
                                        parentbaseform.bt_process3.Enabled = true;
                                        break;

                                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process3.Text = string.Empty;
                                        parentbaseform.bt_process3.Enabled = false;
                                        break;

                                }
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                parentbaseform.bt_process3.Text = string.Empty;
                                parentbaseform.bt_process3.Enabled = false;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                parentbaseform.bt_process3.Text = string.Empty;
                                parentbaseform.bt_process3.Enabled = false;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                switch (this.form.parameters.Mode)
                                {
                                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        parentbaseform.bt_process3.Text = "[3]1次マニ紐付"; //2013.11.22 naitou update
                                        this.maniRelation = null; //紐付情報クリア
                                        parentbaseform.bt_process3.Enabled = true;
                                        break;

                                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process3.Text = string.Empty;
                                        parentbaseform.bt_process3.Enabled = false;
                                        break;

                                }
                                break;
                        }
                        break;
                }

                //process4ボタンの活性
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                switch (this.form.parameters.Mode)
                                {
                                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        parentbaseform.bt_process4.Text = "[4]最終処分終了報告";
                                        parentbaseform.bt_process4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                                        parentbaseform.bt_process4.Enabled = true;
                                        break;

                                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process4.Text = string.Empty;
                                        parentbaseform.bt_process4.Enabled = false;
                                        break;

                                }
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                parentbaseform.bt_process4.Text = string.Empty;
                                parentbaseform.bt_process4.Enabled = false;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                parentbaseform.bt_process4.Text = string.Empty;
                                parentbaseform.bt_process4.Enabled = false;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                switch (this.form.parameters.Mode)
                                {
                                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        parentbaseform.bt_process4.Text = "[4]最終処分終了報告";
                                        parentbaseform.bt_process4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                                        parentbaseform.bt_process4.Enabled = true;
                                        break;

                                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process4.Text = string.Empty;
                                        parentbaseform.bt_process4.Enabled = false;
                                        break;

                                }
                                break;
                        }
                        break;
                }

                //process5ボタンの活性
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                switch (this.form.parameters.Mode)
                                {
                                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        parentbaseform.bt_process5.Text = "[5]最終処分終了取消";
                                        parentbaseform.bt_process5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                                        parentbaseform.bt_process5.Enabled = true;
                                        break;

                                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process5.Text = string.Empty;
                                        parentbaseform.bt_process5.Enabled = false;
                                        break;

                                }
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                parentbaseform.bt_process5.Text = string.Empty;
                                parentbaseform.bt_process5.Enabled = false;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                parentbaseform.bt_process5.Text = string.Empty;
                                parentbaseform.bt_process5.Enabled = false;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                switch (this.form.parameters.Mode)
                                {
                                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        parentbaseform.bt_process5.Text = "[5]最終処分終了取消";
                                        parentbaseform.bt_process5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                                        parentbaseform.bt_process5.Enabled = true;
                                        break;

                                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process5.Text = string.Empty;
                                        parentbaseform.bt_process5.Enabled = false;
                                        break;

                                }
                                break;
                        }
                        break;
                }

                //減容量カラム表示
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                this.form.lbl_GenyoyugoTotalSuryo.Visible = false;
                                this.form.ctxt_GenyoyugoTotalSuryo.Visible = false;

                                //減容量コーラム非表示にする。
                                for (int col = 0; col < this.form.cdgrid_Jisseki.ColumnCount; col++)
                                {
                                    if (col == (int)Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo)
                                    {
                                        this.form.cdgrid_Jisseki.Columns[col].Visible = false;
                                    }
                                }
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                this.form.lbl_GenyoyugoTotalSuryo.Visible = true;
                                this.form.ctxt_GenyoyugoTotalSuryo.Visible = true;

                                //減容量コーラム表示にする。
                                for (int col = 0; col < this.form.cdgrid_Jisseki.ColumnCount; col++)
                                {
                                    if (col == (int)Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo)
                                    {
                                        //20250402
                                        this.form.cdgrid_Jisseki.Columns[col].Visible = false;
                                    }
                                }
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                this.form.lbl_GenyoyugoTotalSuryo.Visible = true;
                                this.form.ctxt_GenyoyugoTotalSuryo.Visible = true;

                                //減容量コーラム表示にする。
                                for (int col = 0; col < this.form.cdgrid_Jisseki.ColumnCount; col++)
                                {
                                    if (col == (int)Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo)
                                    {
                                        this.form.cdgrid_Jisseki.Columns[col].Visible = true;
                                    }
                                }
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                this.form.lbl_GenyoyugoTotalSuryo.Visible = false;
                                this.form.ctxt_GenyoyugoTotalSuryo.Visible = false;

                                //減容量コーラム非表示にする。
                                for (int col = 0; col < this.form.cdgrid_Jisseki.ColumnCount; col++)
                                {
                                    if (col == (int)Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo)
                                    {
                                        this.form.cdgrid_Jisseki.Columns[col].Visible = false;
                                    }
                                }
                                break;

                        }
                        break;
                }

                //二次マニ交付番号
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                this.form.cdgrid_Jisseki.Columns["NijiManiNo"].Visible = true;

                                break;

                            case "F4"://[F4]で切り替え
                                this.form.cdgrid_Jisseki.Columns["NijiManiNo"].Visible = false;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                this.form.cdgrid_Jisseki.Columns["NijiManiNo"].Visible = true;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                this.form.cdgrid_Jisseki.Columns["NijiManiNo"].Visible = false;
                                break;
                        }
                        break;
                }

                //F4のボタン表示内容
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                parentbaseform.bt_func4.Text = "[F4]\r\n1次マニ";
                                maniFlag = 2;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                parentbaseform.bt_func4.Text = "[F4]\r\n2次マニ";
                                maniFlag = 1;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                parentbaseform.bt_func4.Text = "[F4]\r\n2次マニ";
                                maniFlag = 1;
                                break;

                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                parentbaseform.bt_func4.Text = "[F4]\r\n1次マニ";
                                maniFlag = 2;
                                break;

                        }
                        break;
                }
                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                        parentbaseform.bt_func4.Enabled = true;
                        break;

                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                    case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                        parentbaseform.bt_func4.Enabled = false;
                        break;
                }

                if (AppConfig.IsManiLite)
                {
                    // マニライトの場合、二次マニは無し
                    parentbaseform.bt_func4.Text = string.Empty;
                    parentbaseform.bt_func4.Enabled = false;
                }

                this.form.cantxt_DenshuKbn.popupWindowSetting.Clear();

                // 20140530 katen No.679 start‏
                JoinMethodDto dtowhere = new JoinMethodDto();
                dtowhere.IsCheckLeftTable = false;
                dtowhere.IsCheckRightTable = false;
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_DENSHU_KBN";

                SearchConditionsDto serdto = new SearchConditionsDto();
                switch (maniFlag)
                {
                    case 1:
                        //1次は2（出荷）を除外する
                        serdto.And_Or = CONDITION_OPERATOR.AND;
                        serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                        serdto.LeftColumn = "DENSHU_KBN_CD";
                        serdto.Value = "1";
                        serdto.ValueColumnType = DB_TYPE.SMALLINT;
                        dtowhere.SearchCondition.Add(serdto);

                        serdto = new r_framework.Dto.SearchConditionsDto();
                        serdto.And_Or = CONDITION_OPERATOR.OR;
                        serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                        serdto.LeftColumn = "DENSHU_KBN_CD";
                        serdto.Value = "3";
                        serdto.ValueColumnType = DB_TYPE.SMALLINT;
                        dtowhere.SearchCondition.Add(serdto);
                        break;
                    case 2:
                        //2次は1（受入）、3（売上支払）を除外する
                        serdto.And_Or = CONDITION_OPERATOR.AND;
                        serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                        serdto.LeftColumn = "DENSHU_KBN_CD";
                        serdto.Value = "2";
                        serdto.ValueColumnType = DB_TYPE.SMALLINT;
                        dtowhere.SearchCondition.Add(serdto);
                        break;
                }

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "DENSHU_KBN_CD";
                serdto.Value = "100";
                serdto.ValueColumnType = DB_TYPE.SMALLINT;
                dtowhere.SearchCondition.Add(serdto);

                // Ver 2.6では計量入力リリースのため表示する
                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "DENSHU_KBN_CD";
                serdto.Value = "140";
                serdto.ValueColumnType = DB_TYPE.SMALLINT;
                dtowhere.SearchCondition.Add(serdto);

                // Ver 2.0では計量入力は未リリースのため非表示
                //serdto = new r_framework.Dto.SearchConditionsDto();
                //serdto.And_Or = CONDITION_OPERATOR.OR;
                //serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                //serdto.LeftColumn = "DENSHU_KBN_CD";
                //serdto.Value = "140";
                //serdto.ValueColumnType = DB_TYPE.SMALLINT;
                //dtowhere.SearchCondition.Add(serdto);

                // Ver2.1では定期実績のマニ連携は非表示
                // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                //serdto = new r_framework.Dto.SearchConditionsDto();
                //serdto.And_Or = CONDITION_OPERATOR.OR;
                //serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                //serdto.LeftColumn = "DENSHU_KBN_CD";
                //serdto.Value = "130";
                //serdto.ValueColumnType = DB_TYPE.SMALLINT;
                //dtowhere.SearchCondition.Add(serdto);
                // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                this.form.cantxt_DenshuKbn.popupWindowSetting.Add(dtowhere);
                if (this.maniFlag == 1)
                {
                    this.form.cantxt_DenshuKbn.Tag = "1,3,100のいずれかの値を入力してください。";
                }
                else
                {
                    this.form.cantxt_DenshuKbn.Tag = "2,100のいずれかの値を入力してください。";
                }
                this.form.cantxt_DenshuKbn.popupWindowSetting.Add(dtowhere);
                // 20140530 katen No.679 end‏
                //ヘッダーの設定
                SetHeader(strTitelName, BackColor);

                //ボディーの設定
                SetBody(BackColor);

                switch (maniFlag)
                {
                    case 1:
                        int cnt = this.form.cdgrid_Jisseki.Rows.Count;
                        if (!this.form.cdgrid_Jisseki.AllowUserToAddRows && cnt > 0)
                        {
                            bool emptyFlg = true;
                            var row = this.form.cdgrid_Jisseki.Rows[cnt - 1];
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                                {
                                    emptyFlg = false;
                                    break;
                                }
                            }
                            if (emptyFlg)
                            {
                                this.form.cdgrid_Jisseki.Rows.RemoveAt(cnt - 1);
                            }
                        }
                        this.form.cdgrid_Jisseki.AllowUserToAddRows = true;
                        this.form.cdgrid_Jisseki.CurrentCell = this.form.cdgrid_Jisseki.Rows[0].Cells[0];
                        break;
                    case 2:
                        // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
                        //this.form.cdgrid_Jisseki.AllowUserToAddRows = false;
                        // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end
                        if (this.form.cdgrid_Jisseki.Rows.Count == 0)
                        {
                            this.form.cdgrid_Jisseki.Rows.Add();
                        }
                        this.form.cdgrid_Jisseki.CurrentCell = this.form.cdgrid_Jisseki.Rows[0].Cells[0];
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManifestFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }

        /// <summary>
        /// 切替設定
        /// </summary>
        public virtual void SetKirikaeFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();
                if (this.form.tbc_Sanpai.SelectedIndex == 1)
                {
                    this.form.tbc_Sanpai.SelectTab(0);
                }
                else
                {
                    this.form.tbc_Sanpai.SelectTab(1);
                }
                this.form.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKirikaeFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 連票処理
        /// </summary>
        /// <returns>true:画面更新、false:画面そのまま</returns>
        public virtual bool SetRenhyouFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                //登録データ作成
                //システムID(全般･マニ返却日)
                String SystemID = this.form.parameters.SystemId;

                //枝番(全般)
                String Seq = this.form.parameters.Seq;

                //枝番(マニ返却日)
                String SeqRD = this.form.parameters.SeqRD;

                this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, true, SystemID, Seq, SeqRD, true);

                var callForm = new Shougun.Core.PaperManifest.InsatsuBusuSettei.InsatsuBusuSettei(
                    2,
                    this.form.parameters.Mode,
                    2,
                    entrylist,
                    upnlist,
                    prtlist,
                    detailprtlist,
                    keijyoulist,
                    niugatalist,
                    houhoulist,
                    detaillist,
                    retdatelist,
                    this.maniFlag);

                if (this.manifestMercuryList.Count > 1 && (manifestMercuryList[1] as CustomNumericTextBox2).Text == "2")
                {
                    callForm.manifest_mercury_check = true;
                }

                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    if (callForm.ShowDialog() == DialogResult.Yes) //印刷したとき
                    {
                        if (callForm.cbx_ManifestToroku.Checked)//登録したとき
                        {
                            // 20141031 koukouei 委託契約チェック start
                            if (SetBackColor(callForm.retDto))
                            {
                                return false;
                            }
                            // 20141031 koukouei 委託契約チェック end

                            //システムID(全般･マニ返却日)
                            this.form.parameters.SystemId = callForm.SystemId.ToString(); //連続印刷の場合、最後のシステムIDを利用

                            //枝番(全般)
                            this.form.parameters.Seq = "1";

                            //枝番(マニ返却日)
                            this.form.parameters.SeqRD = "1";

                            this.form.parameters.Save();

                            LogUtility.DebugMethodEnd(true);
                            return true;
                        }
                    }
                    // 20141031 koukouei 委託契約チェック start
                    if (SetBackColor(callForm.retDto))
                    {
                        return false;
                    }
                    // 20141031 koukouei 委託契約チェック end
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRenhyouFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(false);
            }
            return false;
        }

        /// <summary>
        /// 単票処理
        /// </summary>
        /// <returns>true:画面更新、false:画面そのまま</returns>
        public virtual bool SetTahyouFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                //登録データ作成
                //システムID(全般･マニ返却日)
                String SystemID = this.form.parameters.SystemId;

                //枝番(全般)
                String Seq = this.form.parameters.Seq;

                //枝番(マニ返却日)
                String SeqRD = this.form.parameters.SeqRD;

                //this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, true, ref lMakeSysId);
                this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, true, SystemID, Seq, SeqRD, true);

                var callForm = new Shougun.Core.PaperManifest.InsatsuBusuSettei.InsatsuBusuSettei(
                    2,
                    this.form.parameters.Mode,
                    1,
                    entrylist,
                    upnlist,
                    prtlist,
                    detailprtlist,
                    keijyoulist,
                    niugatalist,
                    houhoulist,
                    detaillist,
                    retdatelist,
                    this.maniFlag);

                if (this.manifestMercuryList.Count > 1 && (manifestMercuryList[1] as CustomNumericTextBox2).Text == "2")
                {
                    callForm.manifest_mercury_check = true;
                }

                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    if (callForm.ShowDialog() == DialogResult.Yes) //印刷したとき
                    {
                        if (callForm.cbx_ManifestToroku.Checked)//登録したとき
                        {
                            // 20141031 koukouei 委託契約チェック start
                            if (SetBackColor(callForm.retDto))
                            {
                                return false;
                            }
                            // 20141031 koukouei 委託契約チェック end
                            //システムID(全般･マニ返却日)
                            this.form.parameters.SystemId = callForm.SystemId.ToString(); //連続印刷の場合、最後のシステムIDを利用

                            //枝番(全般)
                            this.form.parameters.Seq = "1";

                            //枝番(マニ返却日)
                            this.form.parameters.SeqRD = "1";

                            this.form.parameters.Save();

                            LogUtility.DebugMethodEnd(true);
                            return true;
                        }
                    }

                    // 20141031 koukouei 委託契約チェック start
                    if (SetBackColor(callForm.retDto))
                    {
                        return false;
                    }
                    // 20141031 koukouei 委託契約チェック end
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTahyouFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(false);
            }
            return false;
        }

        /// <summary>
        /// 一覧設定
        /// </summary>
        /// <returns>true:画面更新、false:画面そのまま</returns>
        public virtual void SetItiranFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20140530 katen 不具合No.4129 start‏
                FormManager.OpenForm("G126", "3", this.maniFlag);
                // 20140530 katen 不具合No.4129 end‏
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetItiranFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 状況処理
        /// </summary>
        public virtual void SetJokyoFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く start
                //FormManager.OpenForm("G449", DENSHU_KBN.DENPYOU_HIMODUKE_ICHIRAN, SystemProperty.Shain.CD);
                FormManager.OpenForm("G589", this.maniFlag, this.FormHaikiKbn);
                // 20140618 ria EV004875 マニフェスト入力の[F7]状況ボタンを押下時に伝票紐付一覧が開く end
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetJokyoFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 契約処理
        /// </summary>
        public virtual void SetKeiyakuFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 委託契約情報検索(G475)をモーダル表示
                var PopUpHeadForm = new Shougun.Core.Common.ItakuKeiyakuSearch.HeaderForm();
                var PopUpForm = new Shougun.Core.Common.ItakuKeiyakuSearch.ItakuKeiyakuSearchForm(PopUpHeadForm);
                BasePopForm PopForm = new BasePopForm(PopUpForm, PopUpHeadForm);

                // 排出事業者CD
                PopUpForm.tb_JigyoushaCd.Text = this.form.cantxt_HaisyutuGyousyaCd.Text;
                // 排出事業場CD
                PopUpForm.tb_JigyouJouCd.Text = this.form.cantxt_HaisyutuJigyoubaName.Text;
                // 運搬受託者CD
                PopUpForm.tb_UnpanshaCd.Text = this.form.cantxt_UnpanJyutaku1NameCd.Text;
                // 処分受託者CD
                PopUpForm.tb_ShobunshaCd.Text = this.form.cantxt_SyobunJyutakuNameCd.Text;
                PopForm.ShowDialog();

                // 実行結果
                switch (PopUpForm.DialogResult)
                {
                    case DialogResult.OK:

                        //現在のスクロール位置を保持
                        Point scrollPos = this.form.AutoScrollPosition;
                        this.form.AutoScroll = false;

                        //反映したコントロール全てロストフォーカスの処理を実行する
                        // 排出事業者CDフォーカス
                        this.form.cantxt_HaisyutuGyousyaCd.Focus();
                        // 排出事業者CD
                        this.form.cantxt_HaisyutuGyousyaCd.Text = !string.IsNullOrEmpty(PopUpForm.retHaishutsuShaCD) ? PopUpForm.retHaishutsuShaCD : string.Empty;

                        if (this.form.cantxt_HaisyutuGyousyaCd.Focused)
                        {
                            // 排出事業場CDフォーカス
                            this.form.cantxt_HaisyutuJigyoubaName.Focus();
                        }
                        if (this.form.cantxt_HaisyutuJigyoubaName.Focused)
                        {
                            // 排出事業場CD
                            this.form.cantxt_HaisyutuJigyoubaName.Text = !string.IsNullOrEmpty(PopUpForm.retHaishutsuJouCD) ? PopUpForm.retHaishutsuJouCD : string.Empty;
                            // 運搬受託者CDフォーカス
                            this.form.cantxt_UnpanJyutaku1NameCd.Focus();
                        }

                        if (this.form.cantxt_UnpanJyutaku1NameCd.Focused)
                        {
                            // 運搬受託者CD
                            this.form.cantxt_UnpanJyutaku1NameCd.Text = !string.IsNullOrEmpty(PopUpForm.retUnpanShaCD) ? PopUpForm.retUnpanShaCD : string.Empty;
                            // 処分受託者CDフォーカス
                            this.form.cantxt_SyobunJyutakuNameCd.Focus();
                        }

                        if (this.form.cantxt_SyobunJyutakuNameCd.Focused)
                        {
                            // 処分受託者CD
                            this.form.cantxt_SyobunJyutakuNameCd.Text = !string.IsNullOrEmpty(PopUpForm.retShobunShaCD) ? PopUpForm.retShobunShaCD : string.Empty;
                            // 運搬先の事業場CDフォーカス
                            this.form.cantxt_UnpanJyugyobaNameCd.Focus();
                        }

                        if (this.form.cantxt_UnpanJyugyobaNameCd.Focused)
                        {
                            // 運搬先の事業場CD
                            this.form.cantxt_UnpanJyugyobaNameCd.Text = !string.IsNullOrEmpty(PopUpForm.retShobunJouCD) ? PopUpForm.retShobunJouCD : string.Empty;
                            // 最終処分業者CDフォーカス
                            this.form.cantxt_SaisyuSyobunGyoCd.Focus();
                        }

                        if (this.form.cantxt_SaisyuSyobunGyoCd.Focused)
                        {
                            // 最終処分業者CD
                            this.form.cantxt_SaisyuSyobunGyoCd.Text = !string.IsNullOrEmpty(PopUpForm.retSaishuuShobunShaCD) ? PopUpForm.retSaishuuShobunShaCD : string.Empty;
                            // 最終処分場所CD
                            this.form.cantxt_SaisyuSyobunbaCD.Focus();
                        }

                        if (this.form.cantxt_SaisyuSyobunbaCD.Focused)
                        {
                            // 最終処分場所CD
                            this.form.cantxt_SaisyuSyobunbaCD.Text = !string.IsNullOrEmpty(PopUpForm.retSaishuuShobunJouCD) ? PopUpForm.retSaishuuShobunJouCD : string.Empty;
                            // F10フォーカス
                            parentbaseform.bt_func10.Focus();
                        }
                        this.form.AutoScroll = true;
                        //保持したスクロール位置に戻す
                        this.form.AutoScrollPosition = new Point(-scrollPos.X, -scrollPos.Y);
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
                LogUtility.Error("SetKeiyakuFrom", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） start‏
        /// <summary>
        /// 紐付け上で登録ボタンが押下された場合に行う登録処理
        /// </summary>
        public virtual bool SetRegistForHimoduke()
        {
            bool isErr = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 201400707 syunrei EV005144_№4840でかけた制御をはずす　start
                //// 20140616 syunrei EV004840_処分終了日なしの場合の紐付け制御 start
                //if (SearchCheckForUnit())
                //{
                //    LogUtility.DebugMethodEnd(true);
                //    return true;
                //}
                //// 20140616 syunrei EV004840_処分終了日なしの場合の紐付け制御 end
                // 201400707 syunrei EV005144_№4840でかけた制御をはずす　end

                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
                /*
                if (maniFlag == 2 && this.form.cdgrid_Jisseki.Rows.Count > 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult ret = msgLogic.MessageBoxShow("C055", "二次マニフェストは有効な明細が1行のみとなるため、2行目以降を削除");
                    if (ret == DialogResult.Yes)
                    {
                        DataGridViewRow row;
                        for (int i = this.form.cdgrid_Jisseki.Rows.Count - 1; i > 0; i--)
                        {
                            row = this.form.cdgrid_Jisseki.Rows[i];
                            if (row.IsNewRow)
                            {
                                continue;
                            }
                            this.form.cdgrid_Jisseki.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        return isErr;
                    }
                }
                 */
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

                //必須チェック
                if (SearchCheck())
                {
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                //交付番号入力チェック
                if (this.ChkKohuNo())
                {
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                //名称、住所長さチェック
                if (this.ChkTxtLength())
                {
                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }

                // 20140619 katen EV004472 明細行を空にして登録しても修正モードにて開いた時行が消えない start
                Type type = typeof(KenpaiManifestoNyuryoku.enumCols);
                bool isEmpty = true;
                for (int i = 0; i < this.form.cdgrid_Jisseki.RowCount - 1; i++)
                {
                    isEmpty = true;
                    foreach (KenpaiManifestoNyuryoku.enumCols column in Enum.GetValues(type))
                    {
                        if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Visible && !string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Value)))
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                    if (isEmpty)
                    {
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E176");
                        this.form.tbc_Sanpai.SelectedIndex = 0;
                        this.form.cdgrid_Jisseki.Focus();
                        this.form.cdgrid_Jisseki.CurrentCell = this.form.cdgrid_Jisseki[0, i];
                        LogUtility.DebugMethodEnd(isErr);
                        return isErr;
                    }
                }
                // 20140619 katen EV004472 明細行を空にして登録しても修正モードにて開いた時行が消えない end

                // 20140611 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
                //登録前チェック
                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                        if (this.manifest_validation_check.Equals("1"))
                        {
                            if (this.ManifestCheckRegist())
                            {
                                LogUtility.DebugMethodEnd(isErr);
                                return isErr;
                            }
                        }
                        break;
                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                    default://その他
                        //処理継続
                        break;
                }
                // 20140611 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end

                //紐付チェック
                // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
                //switch (this.form.parameters.Mode)
                //{
                //    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                //    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード

                //        if (mlogic.ChkRelation(SqlInt64.Parse(this.form.parameters.SystemId)))
                //        {
                //            //メッセージ    <Message id="C050" kubun="1">{0}されています。{1}を続行しますか？</Message>
                //            if (Message.MessageBoxUtility.MessageBoxShow("C050", "既に２次マニフェストと紐付", "更新処理") != DialogResult.Yes)
                //            {
                //                LogUtility.DebugMethodEnd(true);
                //                return true;
                //            }
                //        }

                //        break;
                //    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                //    default://その他
                //        //処理継続
                //        break;
                //}
                // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end

                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                        if (this.form.ctxt_TotalWariai.Text == string.Empty ||
                        (Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 0 ||
                        Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 100))
                        {
                            // 最終処分終了済みかつ、最終処分情報に未入力が存在する場合、報告済みの内容と差異が発生する可能性があるのでアラートを表示
                            var isFixedFirstElecMani = this.mlogic.IsFixedRelationFirstMani(SqlInt64.Parse(this.form.parameters.SystemId), 1);
                            if (this.maniFlag == 2 && isFixedFirstElecMani && !existAllLastSbnInfo)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                if (msgLogic.MessageBoxShow("C080") == DialogResult.No)
                                {
                                    return isErr;
                                }
                            }

                            var updateResult = this.UpdateForHimoduke(isErr);
                            //if (this.maniFlag == 2 && updateResult)
                            //{
                            //    this.mlogic.UpdateFirstManiDetail(this.form.parameters.SystemId, this.form.HaikiKbnCD);
                            //}
                            LogUtility.DebugMethodEnd(!this.UpdateResult);
                            return !this.UpdateResult;
                        }
                        else
                        {
                            this.form.messageShowLogic.MessageBoxShow("E040");
                            LogUtility.DebugMethodEnd(isErr);
                            return isErr;
                        }

                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                        if (!this.LogicalDelete2())
                        {
                            LogUtility.DebugMethodEnd(isErr);
                            return isErr; //処理中止がTRUE
                        }
                        break;

                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    default://その他
                        if (this.form.ctxt_TotalWariai.Text == string.Empty ||
                        (Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 0 ||
                        Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 100))
                        {
                            if (mlogic.RegistrationCheck(FormHaikiKbn, this.form.cantxt_KohuNo.Text))
                            {
                                this.form.messageShowLogic.MessageBoxShow("E022", "この交付番号は");
                                LogUtility.DebugMethodEnd(isErr);
                                return isErr;
                            }
                            this.RegistForHimoduke(isErr);
                            LogUtility.DebugMethodEnd(!this.RegistResult);
                            return !this.RegistResult;
                        }
                        else
                        {
                            this.form.messageShowLogic.MessageBoxShow("E040");
                            LogUtility.DebugMethodEnd(isErr);
                            return isErr;
                        }
                }
                isErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRegistForHimoduke", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        /// <summary>
        /// 紐付け上で登録ボタンが押下された場合に行う登録処理
        /// </summary>
        [Transaction]
        public void RegistForHimoduke(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            RegistResult = false;

            try
            {
                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                using (Transaction tran = new Transaction())
                {
                    //登録データ作成
                    //システムID(全般･マニ返却日)
                    String SystemID = this.form.parameters.SystemId;

                    //枝番(全般)
                    String Seq = this.form.parameters.Seq;

                    //枝番(マニ返却日)
                    String SeqRD = this.form.parameters.SeqRD;

                    this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, false, SystemID, Seq, SeqRD, false);

                    mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist);

                    //if (this.maniRelation != null) //紐付処理
                    //{
                    //    this.maniRelation.Regist(tran, entrylist[0].SYSTEM_ID);
                    //}

                    tran.Commit();

                    this.maniRelation = null; //紐付情報クリア
                }

                RegistResult = true;
            }
            catch (ManifestHimoduke.RelationDupulicateException ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                //紐付済みのためロールバック
                LogUtility.Warn(ex);
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(ex.Message);
            }
            catch (Exception ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 紐付け上で登録ボタンが押下された場合に行う修正処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns>true:正常, false:異常</returns>
        [Transaction]
        public bool UpdateForHimoduke(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            UpdateResult = false;

            try
            {
                List<T_MANIFEST_ENTRY> entrylist = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnlist = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_PRT> prtlist = new List<T_MANIFEST_PRT>();
                List<T_MANIFEST_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
                List<T_MANIFEST_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
                List<T_MANIFEST_KP_NISUGATA> niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
                List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
                List<T_MANIFEST_DETAIL> detaillist = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                using (Transaction tran = new Transaction())
                {
                    //システムID(全般･マニ返却日)
                    String SystemID = this.form.parameters.SystemId;

                    //枝番(全般)
                    String Seq = this.form.parameters.Seq;

                    //枝番(マニ返却日)
                    String SeqRD = this.form.parameters.SeqRD;

                    mlogic.LogicalEntityDel(SystemID, Seq, TIME_STAMP_ENTRY);

                    if (String.IsNullOrEmpty(SeqRD) == false)
                    {
                        mlogic.LogicalRetDateDel(SystemID, SeqRD, TIME_STAMP_RET_DATE);
                    }

                    //登録データ作成
                    this.MakeData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, true, SystemID, Seq, SeqRD, false);

                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                    this.UpdateCreateInfo(ref entrylist, ref retdatelist, SystemID, Seq);
                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

                    mlogic.Insert(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist);

                    //if (this.maniRelation != null) //紐付処理
                    //{
                    //    this.maniRelation.Regist(tran, entrylist[0].SYSTEM_ID);
                    //}

                    if (this.maniFlag == 2)
                    {
                        if (detaillist != null && detaillist.Count() > 0)
                        {
                            foreach (T_MANIFEST_DETAIL detail in detaillist)
                            {
                                this.mlogic.UpdateFirstManiDetail(detail.DETAIL_SYSTEM_ID.Value.ToString(), this.form.HaikiKbnCD);
                            }
                        }
                    }

                    tran.Commit();

                    this.maniRelation = null; //紐付情報クリア
                }
                UpdateResult = true;

            }
            catch (ManifestHimoduke.RelationDupulicateException ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                //紐付済みのためロールバック
                LogUtility.Warn(ex);
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(ex.Message);
                return false;
            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 start
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Warn(ex);

                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E080");
                return false;
            }
            // 20140625 kayo EV005020 排他チェックのメッセージ不正 end
            catch (Exception ex)
            {
                if (this.needRollbackProperties)
                {
                    //ロールバック時の不整合回避
                    this.form.parameters.SystemId = bkSystemId;
                    this.form.parameters.Seq = bkSeq;
                    this.form.parameters.SeqRD = bkSeqRD;

                    this.needRollbackProperties = false;
                    this.form.parameters.Save();
                }

                LogUtility.Error(ex);
                throw;

            }

            LogUtility.DebugMethodEnd(errorFlag);
            return false;
        }
        // 20140519 katen No.730 マニフェスト入力画面に対する機能追加（建廃マニフェスト入力） end‏

        /// <summary>
        /// 登録処理
        /// </summary>
        public virtual bool SetRegist()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.form.isRegistErr = false;
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
                /*
                if (maniFlag == 2 && this.form.cdgrid_Jisseki.Rows.Count > 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult ret = msgLogic.MessageBoxShow("C055", "二次マニフェストは有効な明細が1行のみとなるため、2行目以降を削除");
                    if (ret == DialogResult.Yes)
                    {
                        DataGridViewRow row;
                        for (int i = this.form.cdgrid_Jisseki.Rows.Count - 1; i > 0; i--)
                        {
                            row = this.form.cdgrid_Jisseki.Rows[i];
                            if (row.IsNewRow)
                            {
                                continue;
                            }
                            this.form.cdgrid_Jisseki.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                 */
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

                if (this.form.Window_Type != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //必須チェック
                    if (SearchCheck())
                    {
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }

                    //交付番号入力チェック
                    if (this.ChkKohuNo())
                    {
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }

                    //名称、住所長さチェック
                    if (this.ChkTxtLength())
                    {
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }

                    // 20140619 katen EV004472 明細行を空にして登録しても修正モードにて開いた時行が消えない start
                    Type type = typeof(KenpaiManifestoNyuryoku.enumCols);
                    bool isEmpty = true;
                    for (int i = 0; i < this.form.cdgrid_Jisseki.RowCount - 1; i++)
                    {
                        isEmpty = true;
                        foreach (KenpaiManifestoNyuryoku.enumCols column in Enum.GetValues(type))
                        {
                            if (column == KenpaiManifestoNyuryoku.enumCols.Wariai)
                            {
                                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Visible
                                    && !string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Value))
                                    && Convert.ToInt32(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Value) != 0)
                                {
                                    isEmpty = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Visible && !string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)column].Value)))
                                {
                                    isEmpty = false;
                                    break;
                                }
                            }
                        }
                        if (isEmpty)
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E176");
                            this.form.tbc_Sanpai.SelectedIndex = 0;
                            this.form.cdgrid_Jisseki.Focus();
                            this.form.cdgrid_Jisseki.CurrentCell = this.form.cdgrid_Jisseki[0, i];
                            LogUtility.DebugMethodEnd(true);
                            return true;
                        }
                    }
                    // 20140619 katen EV004472 明細行を空にして登録しても修正モードにて開いた時行が消えない end
                }

                // 20140611 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
                //登録前チェック
                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                        if (this.manifest_validation_check.Equals("1"))
                        {
                            if (this.ManifestCheckRegist())
                            {
                                LogUtility.DebugMethodEnd(true);
                                return true;
                            }
                        }
                        // 20141103 koukouei 委託契約チェック start
                        if (!CheckBlank())
                        {
                            return true;
                        }
                        if (this.maniFlag == 1 && !this.CheckItakukeiyaku())
                        {
                            return true;
                        }
                        // 20141103 koukouei 委託契約チェック end
                        break;
                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                    default://その他
                        //処理継続
                        break;
                }
                // 20140611 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end

                //紐付チェック
                switch (this.form.parameters.Mode)
                {
                    // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
                    //case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                    // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end
                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード

                        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
                        //if (mlogic.ChkRelation(SqlInt64.Parse(this.form.parameters.SystemId)))
                        //{
                        //    //メッセージ    <Message id="C050" kubun="1">{0}されています。{1}を続行しますか？</Message>
                        //    if (Message.MessageBoxUtility.MessageBoxShow("C050", "既に２次マニフェストと紐付", "更新処理") != DialogResult.Yes)
                        //    {
                        //        LogUtility.DebugMethodEnd(true);
                        //        return true;
                        //    }
                        //}
                        if (mlogic.ChkRelation(SqlInt64.Parse(this.form.parameters.SystemId), this.maniFlag))
                        {
                            //メッセージ 「すでに別の{0}マニフェストと紐付られているため、削除できません。」
                            if (this.maniFlag == 1)
                            {
                                this.form.messageShowLogic.MessageBoxShow("E177", "2次");

                            }
                            else
                            {
                                this.form.messageShowLogic.MessageBoxShow("E177", "1次");

                            }
                            LogUtility.DebugMethodEnd(true);
                            return true;
                        }
                        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end

                        break;
                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    default://その他
                        //処理継続
                        break;
                }

                switch (this.form.parameters.Mode)
                {
                    case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード

                        //VAN 20201125 #143822, #143823, #143824 S
                        //交付番号存在チェック
                        string SystemId = string.Empty;
                        string Seq = string.Empty;
                        string SeqRD = string.Empty;

                        //すでに他のマニフェスト伝票に登録されている交付番号
                        if (!this.mlogic.ExistKohuNo(this.FormHaikiKbn, this.form.cantxt_KohuNo.Text, ref SystemId, ref Seq, ref SeqRD) &&
                            this.form.parameters.SystemId != SystemId)
                        {
                            this.form.messageShowLogic.MessageBoxShow("E022", "この交付番号は");
                            return true;
                        }
                        //VAN 20201125 #143822, #143823, #143824 E

                        // ２次マニの場合
                        if (this.maniFlag == 2)
                        {
                            // ２次マニ情報から紐付情報の有無と最終処分終了日の比較を行う。
                            // 電子１次に紐付されていて、かつ電子１次の最終処分終了日が設定済の場合、かつ２次マニの最終処分終了日が設定済の場合に確認メッセージを表示する。
                            if (this.CheckLastSbnDate())
                            {
                                if (this.form.messageShowLogic.MessageBoxShow("C046", "1次の最終処分終了日と2次の最終処分終了日に差異があります。\n登録") != DialogResult.Yes)
                                {
                                    return true;
                                }
                            }
                        }

                        if (this.form.ctxt_TotalWariai.Text == string.Empty ||
                        (Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 0 ||
                        Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 100))
                        {
                            // 最終処分終了済みかつ、最終処分情報に未入力が存在する場合、報告済みの内容と差異が発生する可能性があるのでアラートを表示
                            var isFixedFirstElecMani = this.mlogic.IsFixedRelationFirstMani(SqlInt64.Parse(this.form.parameters.SystemId), 2);
                            if (this.maniFlag == 2 && isFixedFirstElecMani && !existAllLastSbnInfo)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                if (msgLogic.MessageBoxShow("C080") == DialogResult.No)
                                {
                                    return true;
                                }
                            }

                            this.Update(true);
                            LogUtility.DebugMethodEnd(!this.UpdateResult);
                            return !this.UpdateResult;
                        }
                        else
                        {
                            this.form.messageShowLogic.MessageBoxShow("E040");
                            LogUtility.DebugMethodEnd(true);
                            return true;
                        }
                        break;

                    case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                        if (!this.LogicalDelete2())
                        {
                            LogUtility.DebugMethodEnd(true);
                            return true; //処理中止がTRUE
                        }
                        break;

                    case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    default://その他
                        if (this.form.ctxt_TotalWariai.Text == string.Empty ||
                        (Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 0 ||
                        Convert.ToDecimal(this.form.ctxt_TotalWariai.Text) == 100))
                        {
                            if (mlogic.RegistrationCheck(FormHaikiKbn, this.form.cantxt_KohuNo.Text))
                            {
                                this.form.messageShowLogic.MessageBoxShow("E022", "この交付番号は");
                                LogUtility.DebugMethodEnd(true);
                                return true;
                            }
                            this.Regist(true);
                            LogUtility.DebugMethodEnd(!this.RegistResult);
                            return !this.RegistResult;
                        }
                        else
                        {
                            this.form.messageShowLogic.MessageBoxShow("E040");
                            LogUtility.DebugMethodEnd(true);
                            return true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRegist", ex);
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
                this.form.isRegistErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(false);
            }
            return false;
        }

        /// <summary>
        /// パターン登録
        /// </summary>
        public virtual void UpdatePattern(out bool catchErr)
        {
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 20140612 syunrei EV004722_拠点について start
                //if (string.IsNullOrEmpty(this.headerform.ctxt_KyotenCd.Text))
                //{
                //    this.form.messageShowLogic.MessageBoxShow("E001", "拠点CD");
                //    this.headerform.ctxt_KyotenCd.Focus();
                //    return;
                //}
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
                /*
                if (maniFlag == 2 && this.form.cdgrid_Jisseki.Rows.Count > 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult ret = msgLogic.MessageBoxShow("C055", "二次マニフェストは有効な明細が1行のみとなるため、2行目以降を削除");
                    if (ret == DialogResult.Yes)
                    {
                        DataGridViewRow row;
                        for (int i = this.form.cdgrid_Jisseki.Rows.Count - 1; i > 0; i--)
                        {
                            row = this.form.cdgrid_Jisseki.Rows[i];
                            if (row.IsNewRow)
                            {
                                continue;
                            }
                            this.form.cdgrid_Jisseki.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                */
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

                //必須チェック
                if (SearchCheck())
                {
                    LogUtility.DebugMethodEnd(true);
                    return;
                }
                // 20140612 syunrei EV004722_拠点について end

                List<T_MANIFEST_PT_ENTRY> entrylist = new List<T_MANIFEST_PT_ENTRY>();
                List<T_MANIFEST_PT_UPN> upnlist = new List<T_MANIFEST_PT_UPN>();
                List<T_MANIFEST_PT_PRT> prtlist = new List<T_MANIFEST_PT_PRT>();
                List<T_MANIFEST_PT_DETAIL_PRT> detailprtlist = new List<T_MANIFEST_PT_DETAIL_PRT>();
                List<T_MANIFEST_PT_KP_KEIJYOU> keijyoulist = new List<T_MANIFEST_PT_KP_KEIJYOU>();
                List<T_MANIFEST_PT_KP_NISUGATA> niugatalist = new List<T_MANIFEST_PT_KP_NISUGATA>();
                List<T_MANIFEST_PT_KP_SBN_HOUHOU> houhoulist = new List<T_MANIFEST_PT_KP_SBN_HOUHOU>();
                List<T_MANIFEST_PT_DETAIL> detaillist = new List<T_MANIFEST_PT_DETAIL>();
                List<T_MANIFEST_RET_DATE> retdatelist = new List<T_MANIFEST_RET_DATE>();

                //登録データ作成
                String lSysId = this.form.parameters.PtSystemId;
                String iSeq = this.form.parameters.PtSeq;
                this.MakePtData(ref entrylist, ref upnlist, ref prtlist, ref detailprtlist, ref keijyoulist, ref niugatalist, ref houhoulist, ref detaillist, ref retdatelist, true, lSysId, iSeq);

                //1次マニ
                int maniKbn = 0;
                if (parentbaseform.bt_func4.Text == "[F4]\r\n1次マニ")
                {
                    //2次マニ
                    maniKbn = 1;
                }

                var callForm = new Shougun.Core.PaperManifest.ManifestPatternTouroku.ManifestPatternTouroku(Convert.ToInt32(FormHaikiKbn),
                     maniKbn,
                     false,
                     entrylist,
                     upnlist,
                     prtlist,
                     detailprtlist,
                     detaillist,
                     keijyoulist,
                     niugatalist,
                     houhoulist);

                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    callForm.ShowDialog();

                    //パターン登録結果を保存。次読み出し時には最新情報で再表示。
                    if (!callForm.RegistedSystemId.IsNull)
                    {
                        this.form.parameters.PtSystemId = callForm.RegistedSystemId.ToString();
                        this.form.parameters.PtSeq = callForm.RegistedSeq.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdatePattern", ex);
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
                LogUtility.DebugMethodEnd(catchErr);
            }
        }

        /// <summary>
        /// パターン呼出
        /// </summary>
        public virtual bool CallPattern(out bool catchErr)
        {
            bool ret = false;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart();
                // 20140529 syunrei No.730 マニフェストパターン一覧 start
                string[] useInfo = new string[] { this.headerform.ctxt_KyotenCd.Text, this.headerform.ctxt_KyotenMei.Text };

                //var callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.MANI_PATTERN_ICHIRAN, "0", FormHaikiKbn);
                var callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.MANI_PATTERN_ICHIRAN, "0", FormHaikiKbn, this.maniFlag.ToString());
                //var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader();
                var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader(FormHaikiKbn, useInfo);
                // 20140529 syunrei No.730 マニフェストパターン一覧 end

                var businessForm = new BusinessBaseForm(callForm, callHeader);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    businessForm.ShowDialog();
                }

                if (callForm.ParamOut_SysID != string.Empty)
                {
                    this.form.parameters.PtSystemId = callForm.ParamOut_SysID;
                    this.form.parameters.PtSeq = callForm.ParamOut_Seq;
                    this.form.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.form.parameters.Save();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CallPattern", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }
        /// <summary>
        /// 更新後や新規等では、初期化(null代入)すること
        /// </summary>
        internal ManifestHimoduke.ManiRelrationResult maniRelation = null;

        /// <summary>
        /// 1次マニ紐付
        /// </summary>
        public virtual bool ManiHimozuke()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //マニフェスト紐付画面を呼び出し
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
                //Shougun.Core.PaperManifest.ManifestHimoduke.UIForm.DoRelation(this.form.WindowType, this.form.HaikiKbnCD, this.form.parameters.SystemId, this.form.ctxt_KansangoTotalSuryo.Text, null, ref this.maniRelation);
                if (this.form.cdgrid_Jisseki.CurrentRow != null)
                {
                    string kansangoSuryou = string.Empty;
                    if (this.form.cdgrid_Jisseki.CurrentRow.Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].FormattedValue != null &&
                       this.form.cdgrid_Jisseki.CurrentRow.Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].FormattedValue.ToString() != string.Empty)
                    {
                        kansangoSuryou = this.form.cdgrid_Jisseki.CurrentRow.Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].FormattedValue.ToString() + this.unit_name;
                    }
                    string detailSystemId = this.form.cdgrid_Jisseki.CurrentRow.Cells["DetailSystemID"].Value.ToString();
                    Shougun.Core.PaperManifest.ManifestHimoduke.UIForm.DoRelation(this.form.WindowType, this.form.HaikiKbnCD, this.form.parameters.SystemId, detailSystemId, kansangoSuryou, null, ref this.maniRelation);
                }
                // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHimozuke", ex);
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
        /// ラベル設定処理
        /// </summary>
        public void SetLabel()
        {
            LogUtility.DebugMethodStart();
            DataTable dt = null;
            DataTable denshudt = null;
            string strRet = string.Empty;
            if (this.form.parameters.RenkeiDenshuKbnCd == string.Empty)
            {

                // 初期処理
                dt = this.SerchParameter();
                if (dt.Rows.Count > 0)
                {
                    strRet = dt.Rows[0].ItemArray[1].ToString();
                }

            }
            else
            {
                SerchParameterDtoCls serch = new SerchParameterDtoCls();
                serch.RENKEI_DENSHU_KBN_CD = this.form.parameters.RenkeiDenshuKbnCd;
                // 初期処理
                dt = GetDenshuDao.GetDataForEntity(serch);
                if (dt.Rows.Count > 0)
                {
                    strRet = dt.Rows[0].ItemArray[0].ToString();
                }
            }

            if (dt.Rows.Count > 0)
            {
                if (strRet != string.Empty)
                {
                    this.form.lbl_No.Text = strRet.ToString() + "番号";
                    // 20140523 syunrei No.679 伝種区分、連携番号、連携明細行連携 start‏
                    //this.form.lbl_Misaigyou.Text = strRet.ToString() + "明細行";
                    this.form.lbl_Misaigyou.Text = "明細行";
                    // 20140523 syunrei No.679 伝種区分、連携番号、連携明細行連携 end‏


                    if (this.form.parameters.RenkeiDenshuKbnCd != string.Empty)
                    {
                        denshudt = SerchDenshu();
                        if (denshudt.Rows.Count > 0)
                        {
                            //番号
                            this.form.cantxt_No.Text = denshudt.Rows[0].ItemArray[0].ToString();
                            //明細行
                            this.form.cantxt_Meisaigyou.Text = denshudt.Rows[0].ItemArray[1].ToString();
                        }
                        else
                        {
                            this.form.cantxt_No.Text = String.Empty;
                            this.form.cantxt_Meisaigyou.Text = String.Empty;
                        }
                    }
                    else
                    {
                        this.form.parameters.RenkeiDenshuKbnCd = dt.Rows[0].ItemArray[2].ToString();
                        this.form.parameters.RenkeiSystemId = dt.Rows[0].ItemArray[3].ToString();
                        this.form.parameters.RenkeiMeisaiSystemId = dt.Rows[0].ItemArray[4].ToString();
                        denshudt = SerchDenshu();
                        if (denshudt.Rows.Count > 0)
                        {
                            //番号
                            this.form.cantxt_No.Text = denshudt.Rows[0].ItemArray[0].ToString();
                            //明細行
                            this.form.cantxt_Meisaigyou.Text = denshudt.Rows[0].ItemArray[1].ToString();
                        }
                        else
                        {
                            this.form.cantxt_No.Text = String.Empty;
                            this.form.cantxt_Meisaigyou.Text = String.Empty;
                        }
                    }
                }
                else
                {
                    this.form.lbl_No.Text = "連携番号";
                    // 20140523 syunrei No.679 伝種区分、連携番号、連携明細行連携 start‏
                    // this.form.lbl_Misaigyou.Text = "連携明細行";
                    this.form.lbl_Misaigyou.Text = "明細行";
                    // 20140523 syunrei No.679 伝種区分、連携番号、連携明細行連携 end‏
                    this.form.cantxt_No.Text = String.Empty;
                    this.form.cantxt_Meisaigyou.Text = String.Empty;
                }
            }
            else
            {
                this.form.lbl_No.Text = "連携番号";
                // 20140523 syunrei No.679 伝種区分、連携番号、連携明細行連携 start‏
                // this.form.lbl_Misaigyou.Text = "連携明細行";
                this.form.lbl_Misaigyou.Text = "明細行";
                // 20140523 syunrei No.679 伝種区分、連携番号、連携明細行連携 end‏
                this.form.cantxt_No.Text = String.Empty;
                this.form.cantxt_Meisaigyou.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業者削除
        /// </summary>
        public void HaisyutuGyousyaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_HaisyutuGyousyaZip.Text = string.Empty;
            this.form.cnt_HaisyutuGyousyaTel.Text = string.Empty;
            this.form.ctxt_HaisyutuGyousyaAdd1.Text = string.Empty;
            this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
            this.form.ctxt_HaisyutuGyousyaName1.Text = string.Empty;
            // 20140611 katen 不具合No.4469 start‏
            this.form.ctxt_HaisyutuGyousyaAdd2.Text = string.Empty;
            this.form.ctxt_HaisyutuGyousyaName2.Text = string.Empty;
            // 20140611 katen 不具合No.4469 end‏

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業場削除
        /// </summary>
        public void HaisyutuGyoubaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_HaisyutuJigyoubaZip.Text = string.Empty;
            this.form.cnt_HaisyutuJigyoubaTel.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaAdd1.Text = string.Empty;
            this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaName1.Text = string.Empty;
            // 20140611 katen 不具合No.4469 start‏
            this.form.ctxt_HaisyutuJigyoubaAdd2.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaName2.Text = string.Empty;
            // 20140611 katen 不具合No.4469 end‏

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬受託者削除
        /// </summary>
        public void UnpanJyutaku1Del()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_UnpanJyutaku1Zip.Text = string.Empty;
            this.form.cnt_UnpanJyutaku1Tel.Text = string.Empty;
            this.form.ctxt_UnpanJyutaku1Add.Text = string.Empty;
            this.form.cantxt_UnpanJyutaku1NameCd.Text = string.Empty;
            this.form.cantxt_UnpanJyutaku1Name.Text = string.Empty;

            //this.form.cantxt_UnpanHouhou1CD.Text = string.Empty;
            //this.form.ctxt_UnpanHouhouMei1.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  運搬受託者（収集運搬業者）（２）削除
        /// </summary>
        public void UnpanJyutaku2Del()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_UnpanJyutaku2Zip.Text = string.Empty;
            this.form.cnt_UnpanJyutaku2Tel.Text = string.Empty;
            this.form.ctxt_UnpanJyutaku2Add.Text = string.Empty;
            this.form.cantxt_UnpanJyutaku2NameCd.Text = string.Empty;
            this.form.cantxt_UnpanJyutaku2Name.Text = string.Empty;

            //this.form.cantxt_UnpanHouhou2CD.Text = string.Empty;
            //this.form.ctxt_UnpanHouhouMei2.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  処分受託者（処分業者）削除
        /// </summary>
        public void SyobunJyutakuDel()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_SyobunJyutakuZip.Text = string.Empty;
            this.form.cnt_SyobunJyutakuTel.Text = string.Empty;
            this.form.ctxt_SyobunJyutakuAdd.Text = string.Empty;
            this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
            this.form.cantxt_SyobunJyutakuName.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) start
        /// <summary>
        ///  運搬の受託（１）削除
        /// </summary>
        public void UnpanJyuCd1Del()
        {
            LogUtility.DebugMethodStart();

            //運搬受託者情報
            //運搬受託者積替
            this.form.cckb_Jyutaku1HokanAri.Checked = true;
            this.form.cckb_Jyutaku1HokanNasi.Checked = false;
            //運搬受託者の車輌
            this.form.cantxt_Jyutaku1SyaNo.Text = string.Empty;
            this.form.ctxt_Jyutaku1SyaNo.Text = string.Empty;
            //運搬受託者の車種
            this.form.cantxt_Jyutaku1Syasyu.Text = string.Empty;
            this.form.ctxt_Jyutaku1Syasyu.Text = string.Empty;

            //運搬の受託
            this.form.cantxt_UnpanJyuCd1.Text = string.Empty;
            this.form.ctxt_UnpanJyuName1.Text = string.Empty;

            this.form.cantxt_UnpanJyuUntenCd1.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenName1.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  運搬の受託（２）削除
        /// </summary>
        public void UnpanJyuCd2Del()
        {
            LogUtility.DebugMethodStart();

            //運搬受託者情報
            //運搬受託者積替
            this.form.cckb_Jyutaku2HokanAri.Checked = true;
            this.form.cckb_Jyutaku2HokanNasi.Checked = false;
            //運搬受託者の車輌
            this.form.cantxt_Jyutaku2SyaNo.Text = string.Empty;
            // 20140617 ria EV004894 運搬受託者(２)の業者CDをクリアすると運搬受託者(１)の車輌名がクリアされてしまう。 start
            //this.form.ctxt_Jyutaku1SyaNo.Text = string.Empty;
            this.form.ctxt_Jyutaku2SyaNo.Text = string.Empty;
            // 20140617 ria EV004894 運搬受託者(２)の業者CDをクリアすると運搬受託者(１)の車輌名がクリアされてしまう。 end

            //運搬受託者の車種
            this.form.cantxt_Jyutaku2Syasyu.Text = string.Empty;
            this.form.ctxt_Jyutaku2Syasyu.Text = string.Empty;

            //運搬の受託
            this.form.cantxt_UnpanJyuCd2.Text = string.Empty;
            this.form.ctxt_UnpanJyuName2.Text = string.Empty;

            this.form.cantxt_UnpanJyuUntenCd2.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenName2.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  処分の受託削除
        /// </summary>
        public void SyobunJyuCDDel()
        {
            LogUtility.DebugMethodStart();

            //処分の受託(受領)
            this.form.cantxt_SyobunJyuCd.Text = string.Empty;
            this.form.ctxt_SyobunJyuName.Text = string.Empty;

            //this.form.cantxt_SyobunJyuUntenCd.Text = string.Empty;
            //this.form.cantxt_SyobunJyuUntenName.Text = string.Empty;

            //処分の受託(処分)
            this.form.cantxt_SyobunSyoCd.Text = string.Empty;
            this.form.ctxt_SyobunSyoName.Text = string.Empty;

            //this.form.cantxt_SyobunSyoUntenCd.Text = string.Empty;
            //this.form.cantxt_SyobunSyoUntenName.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }
        // 20140604 ria EV004130 運搬受託者の情報を消した際、運搬の受託の情報も同時に消す(処分も) end

        /// <summary>
        ///  運搬先の事業場（処分業者の処理施設）削除削除
        /// </summary>
        public void UnpanJyugyobaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_UnpanJyugyobaZip.Text = string.Empty;
            this.form.cnt_UnpanJyugyobaTel.Text = string.Empty;
            this.form.ctxt_UnpanJyugyobaAdd.Text = string.Empty;
            this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.form.cantxt_UnpanJyugyobaName.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  積替え又は保管削除
        /// </summary>
        public void TumiHokaDel(String Kbn)
        {
            LogUtility.DebugMethodStart();

            switch (Kbn)
            {
                case "cbtn_TumiHokaDel"://削除ボタン
                case "cantxt_TumiGyoCd"://積換保管業者CDが空の場合
                    this.form.cantxt_TumiGyoCd.Text = string.Empty;
                    this.form.ctxt_TumiGyoName.Text = string.Empty;
                    break;

                case "cantxt_TumiHokaNameCd"://積換保管事業場CDが空の場合
                    break;

            }
            this.form.cantxt_TumiHokaNameCd.Text = string.Empty;
            this.form.ctxt_TumiHokaName.Text = string.Empty;
            this.form.cnt_TumiHokaZip.Text = string.Empty;
            this.form.ctxt_TumiHokaAdd.Text = string.Empty;
            this.form.cnt_TumiHokaTel.Text = string.Empty;

            LogUtility.DebugMethodEnd();
            return;
        }

        /// <summary>
        ///  最終処分削除
        /// </summary>
        public void SaisyuBasyoSyozaiDel(String Kbn)
        {
            LogUtility.DebugMethodStart();

            switch (Kbn)
            {
                case "cbtn_SaisyuBasyoDel"://削除ボタン
                case "cantxt_SaisyuSyobunGyoCd"://最終処分場CDが空の場合
                    this.form.cantxt_SaisyuSyobunGyoCd.Text = string.Empty;
                    break;

                case "cantxt_SaisyuSyobunbaCD"://最終処分場所CDが空の場合
                    break;

            }
            this.form.cantxt_SaisyuSyobunbaCD.Text = string.Empty;
            this.form.ctxt_SaisyuSyobunGyoName.Text = string.Empty;
            this.form.ctxt_SaisyuSyobunGyoZip.Text = string.Empty;
            this.form.ctxt_SaisyuBasyoSyozai.Text = string.Empty;

            this.form.ctxt_SaisyuBasyoNo.Text = string.Empty;

            LogUtility.DebugMethodEnd();
            return;
        }

        /// <summary>
        /// 排出作業場削除
        /// </summary>
        public void HaisyutuJigyoubaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.cnt_HaisyutuJigyoubaZip.Text = string.Empty;
            this.form.cnt_HaisyutuJigyoubaTel.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaAdd1.Text = string.Empty;
            this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaName1.Text = string.Empty;
            // 20140611 katen 不具合No.4469 start‏
            this.form.ctxt_HaisyutuJigyoubaAdd2.Text = string.Empty;
            this.form.ctxt_HaisyutuJigyoubaName2.Text = string.Empty;
            // 20140611 katen 不具合No.4469 end‏

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ライン設定処理
        /// </summary>
        public void SetLineCtl(object obj, object obj2)
        {
            LogUtility.DebugMethodStart(obj, obj2);
            Microsoft.VisualBasic.PowerPacks.LineShape ls = (Microsoft.VisualBasic.PowerPacks.LineShape)obj2;
            if (obj is DateTimePicker)
            {
                DateTimePicker picker;
                picker = (DateTimePicker)obj;
                if (picker.Enabled == false)
                {
                    ls.Visible = true;
                }
                else
                {
                    ls.Visible = false;
                }
                return;
            }
            if (obj is TextBox)
            {
                TextBox txt;
                txt = (TextBox)obj;
                if (txt.Enabled == false)
                {
                    ls.Visible = true;
                }
                else
                {
                    ls.Visible = false;
                }
                return;
            }
            LogUtility.DebugMethodEnd(obj, obj2);

        }

        /// <summary>
        /// 入力制限
        /// </summary>
        public void SetEnableCtl(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            DateTimePicker pker;
            CheckBox ccbk;
            TextBox ctxt;
            Button cbtn;
            RadioButton rdbtn;

            if (obj is Button)
            {
                cbtn = (Button)obj;
                if (cbtn.Enabled == true)
                {
                    cbtn.Enabled = false;
                }
                else
                {
                    cbtn.Enabled = true;
                }
                return;
            }

            if (obj is CheckBox)
            {
                ccbk = (CheckBox)obj;
                if (ccbk.Enabled == true)
                {
                    ccbk.Enabled = false;
                }
                else
                {
                    ccbk.Enabled = true;
                }
                return;
            }

            if (obj is TextBox)
            {
                ctxt = (TextBox)obj;
                if (ctxt.Enabled == true)
                {
                    ctxt.Enabled = false;
                }
                else
                {
                    ctxt.Enabled = true;
                }
                return;
            }

            if (obj is DateTimePicker)
            {
                pker = (DateTimePicker)obj;
                if (pker.Enabled == true)
                {
                    pker.Enabled = false;
                }
                else
                {
                    pker.Enabled = true;
                }
                return;
            }

            if (obj is RadioButton)
            {
                rdbtn = (RadioButton)obj;
                if (rdbtn.Enabled == true)
                {
                    rdbtn.Enabled = false;
                }
                else
                {
                    rdbtn.Enabled = true;
                }
                return;
            }
            LogUtility.DebugMethodEnd(obj);
        }

        /// <summary>
        /// 入力制限
        /// </summary>
        /// <param name="obj">設定するコントロールオブジェクト</param>
        /// <param name="setflg">設定フラグ</param>
        public void SetCheckedCtl(object obj, bool setflg)
        {
            LogUtility.DebugMethodStart(obj, setflg);

            RadioButton rdb;
            CheckBox ccbk;

            if (obj is RadioButton)
            {
                rdb = (RadioButton)obj;
                rdb.Checked = setflg;
                return;
            }

            if (obj is CheckBox)
            {
                ccbk = (CheckBox)obj;
                ccbk.Enabled = setflg;
                return;
            }

            LogUtility.DebugMethodEnd(obj, setflg);
        }

        /// <summary>
        /// 帳簿当欄チェックボックス入力制限
        /// </summary>
        public void SetCheckCtl(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            if (this.form.ccbx_TyukanTyoubo.Text == ((CheckBox)obj).Text)
            {
                if (this.form.ccbx_TyukanTyoubo.Checked == true && this.form.ccbx_TyukanKisai.Checked == true)
                {
                    this.form.ccbx_TyukanKisai.Checked = false;
                }
            }

            if (this.form.ccbx_TyukanKisai.Text == ((CheckBox)obj).Text)
            {
                if (this.form.ccbx_TyukanKisai.Checked == true && this.form.ccbx_TyukanTyoubo.Checked == true)
                {
                    this.form.ccbx_TyukanTyoubo.Checked = false;
                }
            }

            if (this.form.ccbx_SaisyuTyoubo.Text == ((CheckBox)obj).Text)
            {
                if (this.form.ccbx_SaisyuTyoubo.Checked == true && this.form.ccbx_SaisyuKisai.Checked == true)
                {
                    this.form.ccbx_SaisyuKisai.Checked = false;
                }
            }

            if (this.form.ccbx_SaisyuKisai.Text == ((CheckBox)obj).Text)
            {
                if (this.form.ccbx_SaisyuKisai.Checked == true && this.form.ccbx_SaisyuTyoubo.Checked == true)
                {
                    this.form.ccbx_SaisyuTyoubo.Checked = false;
                }

            }

            if (this.form.cckb_TumiHokaT.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_TumiHokaT.Checked == true && this.form.cckb_TumiHokaM3.Checked == true)
                {
                    this.form.cckb_TumiHokaM3.Checked = false;
                }
            }

            if (this.form.cckb_TumiHokaM3.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_TumiHokaM3.Checked == true && this.form.cckb_TumiHokaT.Checked == true)
                {
                    this.form.cckb_TumiHokaT.Checked = false;
                }
            }

            // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
            if (this.form.cckb_Jyutaku1HokanAri.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_Jyutaku1HokanAri.Checked == true && this.form.cckb_Jyutaku1HokanNasi.Checked == true)
                {
                    this.form.cckb_Jyutaku1HokanNasi.Checked = false;
                }
            }

            if (this.form.cckb_Jyutaku1HokanNasi.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_Jyutaku1HokanNasi.Checked == true && this.form.cckb_Jyutaku1HokanAri.Checked == true)
                {
                    this.form.cckb_Jyutaku1HokanAri.Checked = false;
                }
            }

            if (this.form.cckb_Jyutaku2HokanAri.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_Jyutaku2HokanAri.Checked == true && this.form.cckb_Jyutaku2HokanNasi.Checked == true)
                {
                    this.form.cckb_Jyutaku2HokanNasi.Checked = false;
                }
            }

            if (this.form.cckb_Jyutaku2HokanNasi.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_Jyutaku2HokanNasi.Checked == true && this.form.cckb_Jyutaku2HokanAri.Checked == true)
                {
                    this.form.cckb_Jyutaku2HokanAri.Checked = false;
                }
            }

            if (this.form.cckb_TumiHokaAri.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_TumiHokaAri.Checked == true && this.form.cckb_TumiHokaNasi.Checked == true)
                {
                    this.form.cckb_TumiHokaNasi.Checked = false;
                }
            }

            if (this.form.cckb_TumiHokaNasi.Text == ((CheckBox)obj).Text)
            {
                if (this.form.cckb_TumiHokaNasi.Checked == true && this.form.cckb_TumiHokaAri.Checked == true)
                {
                    this.form.cckb_TumiHokaAri.Checked = false;
                }
            }
            // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end

            LogUtility.DebugMethodEnd(obj);
        }

        public void SetCheckCtl()
        {
            this.form.cckb_Jyutaku1HokanAri.Checked = false;
            this.form.cckb_Jyutaku1HokanNasi.Checked = true;
            this.form.cckb_Jyutaku2HokanAri.Checked = false;
            this.form.cckb_Jyutaku2HokanNasi.Checked = false;
            this.form.cckb_TumiHokaM3.Checked = false;
            this.form.cckb_TumiHokaT.Checked = false;
            this.form.cckb_TumiHokaNasi.Checked = false;
            this.form.cckb_TumiHokaAri.Checked = false;
        }

        /// <summary>
        /// 最終処分の場所削除
        /// </summary>
        public void SaisyuSyobunDel(String Kbn)
        {
            LogUtility.DebugMethodStart();

            switch (Kbn)
            {
                case "cbtn_SaisyuGyousyaDel"://削除ボタン
                case "cantxt_SaisyuGyousyaCd"://最終処分の場所　業者CD欄が空の場合
                    this.form.ccbx_SaisyuTyoubo.Checked = true;
                    this.form.ccbx_SaisyuKisai.Checked = false;
                    this.form.cantxt_SaisyuGyousyaCd.Text = string.Empty;
                    break;

                case "cantxt_SaisyuGyousyaNameCd"://最終処分の場所　名称欄が空の場合
                    break;
            }
            this.form.cantxt_SaisyuGyousyaNameCd.Text = string.Empty;
            this.form.cantxt_SaisyuGyousyaName.Text = string.Empty;
            this.form.ctxt_SaisyuGyousyaAdd.Text = string.Empty;

            LogUtility.DebugMethodEnd();
            return;
        }

        /// <summary>
        ///  数量設定処理
        /// </summary>
        public void SetYukaJyuuryo(object obj1)
        {
            LogUtility.DebugMethodStart(obj1);

            TextBox txt = (TextBox)obj1;
            decimal d = 0;

            //decimalに変換できるか確かめる
            if (decimal.TryParse(txt.Text, out d))
            {
                //変換出来たら、dにその数値が入る
                txt.Text = mlogic.GetSuuryoRound(d, this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                return;
            }

            LogUtility.DebugMethodEnd(obj1);
            return;
        }

        /// <summary>
        ///  数量設定処理
        /// </summary>
        public void SetTotalJyuuryo(object obj1, object obj2)
        {
            try
            {
                LogUtility.DebugMethodStart(obj1, obj2);

                string strTani = string.Empty;
                decimal lTotal = 0;
                decimal lnum = 0;

                TextBox txt = (TextBox)obj2;
                decimal d = 0;

                CheckBox ckb = (CheckBox)obj1;

                //チェックボックスにチェックを付ける
                if (txt.Text != string.Empty)
                {
                    ckb.Checked = true;
                }
                else
                {
                    ckb.Checked = false;
                }

                //decimalに変換できるか確かめる
                if (decimal.TryParse(txt.Text, out d))
                {
                    //変換出来たら、dにその数値が入る
                    txt.Text = mlogic.GetSuuryoRound(d, this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                }
                else
                {
                    return;
                }

                //安定数量１
                if (this.form.cntxt_AnteiSuryo01.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo01.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量２
                if (this.form.cntxt_AnteiSuryo02.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo02.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量３
                if (this.form.cntxt_AnteiSuryo03.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo03.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量４
                if (this.form.cntxt_AnteiSuryo04.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo04.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量５
                if (this.form.cntxt_AnteiSuryo05.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo05.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量６
                if (this.form.cntxt_AnteiSuryo06.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo06.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量７
                if (this.form.cntxt_AnteiSuryo07.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo07.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量８
                if (this.form.cntxt_AnteiSuryo08.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiSuryo08.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量フリー１
                if (this.form.cntxt_AnteiFreeSuryo01.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiFreeSuryo01.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量フリー２
                if (this.form.cntxt_AnteiFreeSuryo02.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiFreeSuryo02.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量フリー３
                if (this.form.cntxt_AnteiFreeSuryo03.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiFreeSuryo03.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //安定数量フリー４
                if (this.form.cntxt_AnteiFreeSuryo04.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_AnteiFreeSuryo04.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１１
                if (this.form.cntxt_KanriSuryo11.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo11.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１２
                if (this.form.cntxt_KanriSuryo12.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo12.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１３
                if (this.form.cntxt_KanriSuryo13.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo13.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１４
                if (this.form.cntxt_KanriSuryo14.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo14.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１５
                if (this.form.cntxt_KanriSuryo15.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo15.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１６
                if (this.form.cntxt_KanriSuryo16.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo16.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目数量１７
                if (this.form.cntxt_KanriSuryo17.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriSuryo17.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目フリー１
                if (this.form.cntxt_KanriFreeSuryo01.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriFreeSuryo01.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目フリー２
                if (this.form.cntxt_KanriFreeSuryo02.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriFreeSuryo02.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //管理品目フリー３
                if (this.form.cntxt_KanriFreeSuryo03.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_KanriFreeSuryo03.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //特別管理品目２１
                if (this.form.cntxt_TokubetuSuryo21.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_TokubetuSuryo21.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //特別管理品目フリー１
                if (this.form.cntxt_TokubetuFreeSuryo01.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_TokubetuFreeSuryo01.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //特別管理品目フリー２
                if (this.form.cntxt_TokubetuFreeSuryo02.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_TokubetuFreeSuryo02.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //特別管理品目フリー３
                if (this.form.cntxt_TokubetuFreeSuryo03.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_TokubetuFreeSuryo03.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //特別管理品目フリー４
                if (this.form.cntxt_TokubetuFreeSuryo04.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_TokubetuFreeSuryo04.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                //特別管理品目フリー５
                if (this.form.cntxt_TokubetuFreeSuryo05.Text.Trim() != string.Empty)
                {
                    lnum = Convert.ToDecimal(this.form.cntxt_TokubetuFreeSuryo05.Text.Replace(",", ""));
                    lTotal = lTotal + lnum;
                    lnum = 0;
                }

                this.form.cntxt_TotalJyuryo.Text = lTotal.ToString(this.ManifestSuuryoFormat);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTotalJyuuryo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(obj1, obj2);
            }
        }

        /// <summary>
        ///  混合種別名称設定処理
        /// </summary>
        public bool SetKongouName()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = null;

                if (this.form.cantxt_KongoCd.Text == string.Empty)
                {
                    this.form.ctxt_KongoName.Text = string.Empty;

                    this.form.parameters.KongoCd = String.Empty;
                    this.form.parameters.Save();

                    return ret;
                }

                //2013-11-16 Add ogawamut PT 東北 No.831
                if (this.form.parameters.KongoCd == this.form.cantxt_KongoCd.Text)
                {
                    return ret;
                }

                // 初期処理
                dt = GetKongouName(this.form.cantxt_KongoCd.Text);
                if (dt.Rows.Count > 0)
                {
                    this.form.ctxt_KongoName.Text = dt.Rows[0].ItemArray[0].ToString();
                    this.isInputError = false;
                }
                else
                {
                    this.form.messageShowLogic.MessageBoxShow("E020", "混合種類");

                    this.form.cantxt_KongoCd.Focus();
                    this.form.cantxt_KongoCd.SelectAll();
                    this.isInputError = true;
                    return ret;
                }

                if (this.form.cdgrid_Jisseki.Rows.Count > 1)
                {
                    DialogResult result =
                        this.form.messageShowLogic.MessageBoxShow("C027");
                    if (result == DialogResult.No)
                    {
                        this.form.cantxt_KongoCd.Text = string.Empty;
                        this.form.ctxt_KongoName.Text = string.Empty;
                        return ret;
                    }
                }

                //2013-11-16 Add ogawamut PT 東北 No.831
                this.form.parameters.KongoCd = this.form.cantxt_KongoCd.Text;
                this.form.parameters.Save();

                //2013-12-06 Upd ogawamut E3 No.350
                var SearchString = new CommonKongouHaikibutsuDtoCls();
                SearchString.HAIKI_KBN_CD = this.form.HaikiKbnCD;
                SearchString.KONGOU_SHURUI_CD = this.form.cantxt_KongoCd.Text;

                DataTable kongoudt = mlogic.GetKongouHaikibutu(SearchString);

                //isClearForm = true => do not check valid on cdgrid_Jisseki when clear form
                this.form.isClearForm = true;
                this.form.cdgrid_Jisseki.Rows.Clear();
                //reset variable
                this.form.isClearForm = false;
                this.form.cdgrid_Jisseki.RowCount = kongoudt.Rows.Count + 1;
                this.form.cdgrid_Jisseki.Rows[kongoudt.Rows.Count].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value = null;
                this.form.cdgrid_Jisseki.Rows[kongoudt.Rows.Count].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value = null;
                for (int i = 0; i < kongoudt.Rows.Count; i++)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value = kongoudt.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value = kongoudt.Rows[i]["HAIKI_HIRITSU"].ToString();
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value = kongoudt.Rows[i]["HAIKI_SHURUI_NAME_RYAKU"].ToString();
                }
                this.form.cdgrid_Jisseki.RefreshEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKongouName", ex);
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

        /// <summary>
        /// 換算値算出処理
        /// </summary>
        public bool SetKansanti(int iRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(iRow);

                //換算値計算を共通化
                mlogic.SetKansanti(
                    this.form.HaikiKbnCD,
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["HaikiCd", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["HaikiNameCd", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["Suryo", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["NisugataCd", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["TaniCd", iRow],
                    this.ManifestSuuryoFormatCD,
                    this.ManifestSuuryoFormat,
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["KansangoSuryo", iRow]
                    );
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKansanti", ex);
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
        /// 減容値算出処理
        /// </summary>
        public bool SetGenyouti(int iRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(iRow);

                //減容値計算を共通化
                mlogic.SetGenyouti(
                    this.form.HaikiKbnCD,
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["HaikiCd", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["HaikiNameCd", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["SyobunCd", iRow],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["KansangoSuryo", iRow],
                    this.ManifestSuuryoFormatCD,
                    this.ManifestSuuryoFormat,
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.cdgrid_Jisseki["GenyoyugoTotalSuryo", iRow]
                    );
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenyouti", ex);
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
        /// 実績単位CDチェック
        /// </summary>
        public int ChkJissekiTani()
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart();

                if (String.IsNullOrEmpty(this.form.canTxt_JissekiTaniCd.Text))
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                string strTaniCd = this.form.canTxt_JissekiTaniCd.Text;

                var sertch = new CommonUnitDtoCls();
                sertch.UNIT_CD = strTaniCd;
                sertch.KAMI_USE_KBN = "True";

                DataTable dt = mlogic.GetUnit(sertch);
                if (dt.Rows.Count > 0)
                {
                    ret = 0;
                }
                else
                {
                    this.form.ctxt_JissekiTaniName.Clear();
                    this.MsgBox.MessageBoxShow("E020", "単位");
                    this.form.canTxt_JissekiTaniCd.Focus();
                    this.form.canTxt_JissekiTaniCd.SelectAll();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJissekiTani", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 実績単位名称の削除
        /// </summary>
        public void DelJissekiTani()
        {
            LogUtility.DebugMethodStart();

            this.form.ctxt_JissekiTaniName.Text = "";

            for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            {
                this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = null;
                this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = null;
            }
            this.form.cdgrid_Jisseki.Columns[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = false;

            LogUtility.DebugMethodEnd();

            return;
        }

        /// <summary>
        /// 実績単位CDのセット
        /// </summary>
        public void SetJissekiTani()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string strTaniCd = this.form.canTxt_JissekiTaniCd.Text;
                string strTaniName = this.GetTaniName(this.form.canTxt_JissekiTaniCd.Text);
                // 20140618 syunrei EV004738_建廃マニで混合種類を選択すると、新規行を追加した際に単位が選択できない start
                //this.form.cdgrid_Jisseki.Columns[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = true;
                // 20140618 syunrei EV004738_建廃マニで混合種類を選択すると、新規行を追加した際に単位が選択できない end
                if (this.form.cdgrid_Jisseki.Rows.Count <= 1)
                {
                    return;
                }

                for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count - 1; i++)
                {
                    //if (strTaniCd.Length == 1) //単位は0埋めしない！（拠点以外はやらない方針）
                    //{
                    //    strTaniCd = "0" + strTaniCd;
                    //}
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = strTaniCd;
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = strTaniName;

                    //換算値、減容値の順に再計算
                    if (!this.SetKansanti(i)) { return; }
                    if (!this.SetGenyouti(i)) { return; }
                    // 20140618 syunrei EV004738_建廃マニで混合種類を選択すると、新規行を追加した際に単位が選択できない start
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = true;
                    // 20140618 syunrei EV004738_建廃マニで混合種類を選択すると、新規行を追加した際に単位が選択できない end
                }
                // 20140618 syunrei EV004738_建廃マニで混合種類を選択すると、新規行を追加した際に単位が選択できない start
                this.form.cdgrid_Jisseki.Rows[this.form.cdgrid_Jisseki.Rows.Count - 1].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = false;
                // 20140618 syunrei EV004738_建廃マニで混合種類を選択すると、新規行を追加した際に単位が選択できない end

                //合計
                if (!this.SetTotal()) { return; }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetJissekiTani", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 単位名称取得
        /// </summary>
        //public string GetTaniName()
        public string GetTaniName(string strTaniCd)
        {
            LogUtility.DebugMethodStart();

            string strTaniName = string.Empty;

            if (strTaniCd != string.Empty)
            {
                var sertch = new CommonUnitDtoCls();
                sertch.UNIT_CD = strTaniCd;

                DataTable dt = mlogic.GetUnit(sertch);
                if (dt.Rows.Count > 0)
                {
                    strTaniName = dt.Rows[0]["UNIT_NAME"].ToString();
                }
            }

            LogUtility.DebugMethodEnd();
            return strTaniName;
        }

        /// <summary>
        /// 明細合計算出処理
        /// </summary>
        public bool SetTotal()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                decimal dWariai = 0;
                decimal dSuryo = 0;
                decimal dKansango = 0;
                decimal dGenyoyugo = 0;
                decimal tmp = 0;

                for (int i = 0; i < this.form.cdgrid_Jisseki.RowCount; i++)
                {
                    tmp = 0;
                    if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value != null &&
                        this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value.ToString() != string.Empty)
                    {
                        tmp = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value.ToString().Replace(",", ""));
                    }
                    dWariai = dWariai + tmp;

                    tmp = 0;
                    if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value != null &&
                        this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value.ToString() != string.Empty)
                    {
                        tmp = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value.ToString().Replace(",", ""));
                    }
                    dSuryo = dSuryo + tmp;

                    tmp = 0;
                    if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value != null &&
                        this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value.ToString() != string.Empty)
                    {
                        tmp = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value.ToString().Replace(",", ""));
                    }
                    dKansango = dKansango + tmp;

                    tmp = 0;
                    if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value != null &&
                        this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value.ToString() != string.Empty)
                    {
                        tmp = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value.ToString().Replace(",", ""));
                    }
                    dGenyoyugo = dGenyoyugo + tmp;
                }

                //割合
                this.form.ctxt_TotalWariai.Text = dWariai.ToString();

                //合計数量
                dSuryo = mlogic.GetSuuryoRound(dSuryo, this.ManifestSuuryoFormatCD);
                this.form.ctxt_TotalSuryo.Text = dSuryo.ToString(this.ManifestSuuryoFormat);

                //換算後合計数量
                dKansango = mlogic.GetSuuryoRound(dKansango, this.ManifestSuuryoFormatCD);
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //this.form.ctxt_KansangoTotalSuryo.Text = dKansango.ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_KansangoTotalSuryo.Text = dKansango.ToString(this.ManifestSuuryoFormat) + this.unit_name;
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end

                //減容後合計数量
                dGenyoyugo = mlogic.GetSuuryoRound(dGenyoyugo, this.ManifestSuuryoFormatCD);
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //this.form.ctxt_GenyoyugoTotalSuryo.Text = dGenyoyugo.ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_GenyoyugoTotalSuryo.Text = dGenyoyugo.ToString(this.ManifestSuuryoFormat) + this.unit_name;
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTotal", ex);
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
        /// 数値フォーマット適用
        /// </summary>
        public void SetNumFormst()
        {
            LogUtility.DebugMethodStart();

            //混合数量
            if (string.IsNullOrEmpty(this.form.cntxt_JissekiSuryo.Text) == false)
            {
                decimal JissekiSuryo = Convert.ToDecimal(this.form.cntxt_JissekiSuryo.Text.ToString().Replace(",", ""));
                this.form.cntxt_JissekiSuryo.Text = this.mlogic.GetSuuryoRound(JissekiSuryo, this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
            }

            LogUtility.DebugMethodEnd();
            return;
        }

        /// <summary>
        /// 最大　処分終了日　算出処理
        /// </summary>
        public void SetMaxSyobunEndDate()
        {
            DateTime dt1;
            DateTime dt2;

            // 20140617 ria EV004828 処分終了日、最終処分終了日 start
            bool chkflg = false;
            // 20140617 ria EV004828 処分終了日、最終処分終了日 end

            this.form.cdate_SyobunSyo.Value = null;
            for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            {
                if (this.form.cdgrid_Jisseki.Rows[i].IsNewRow)
                {
                    continue;
                }
                dt1 = Convert.ToDateTime(this.form.cdate_SyobunSyo.Value);
                dt2 = this.parentbaseform.sysDate.Date;

                // 20140617 ria EV004828 処分終了日、最終処分終了日 start
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value == null)
                {
                    chkflg = true;
                }
                else
                {
                    DateTime result;
                    if (DateTime.TryParse(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value.ToString(), out result))
                    {
                        dt2 = Convert.ToDateTime(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value);
                    }
                }
                // 20140617 ria EV004828 処分終了日、最終処分終了日 end

                if (String.IsNullOrEmpty(Convert.ToString(dt2)))
                {
                }
                else if (String.IsNullOrEmpty(Convert.ToString(dt1)))
                {
                    this.form.cdate_SyobunSyo.Value = dt2;
                }
                else if (dt2 > dt1)
                {
                    this.form.cdate_SyobunSyo.Value = dt2;
                }
            }
            // 20140617 ria EV004828 処分終了日、最終処分終了日 start
            if (chkflg)
            {
                this.form.cdate_SyobunSyo.Value = string.Empty;
            }
            // 20140617 ria EV004828 処分終了日、最終処分終了日 end
        }

        /// <summary>
        /// 最大　最終処分終了日　算出処理
        /// </summary>
        public void SetMaxSaisyuSyobunEndDate()
        {
            DateTime dt1;
            DateTime dt2;

            // 20140617 ria EV004828 処分終了日、最終処分終了日 start
            bool chkflg = false;
            // 20140617 ria EV004828 処分終了日、最終処分終了日 end

            this.form.cdate_SaisyuSyobunDate.Value = null;
            for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            {
                if (this.form.cdgrid_Jisseki.Rows[i].IsNewRow)
                {
                    continue;
                }
                dt1 = Convert.ToDateTime(this.form.cdate_SaisyuSyobunDate.Value);
                dt2 = this.parentbaseform.sysDate.Date;

                // 20140617 ria EV004828 処分終了日、最終処分終了日 start
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value == null)
                {
                    chkflg = true;
                }
                else
                {
                    DateTime result;
                    if (DateTime.TryParse(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value.ToString(), out result))
                    {
                        dt2 = Convert.ToDateTime(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value);
                    }
                }
                // 20140617 ria EV004828 処分終了日、最終処分終了日 end

                if (String.IsNullOrEmpty(Convert.ToString(dt2)))
                {
                }
                else if (String.IsNullOrEmpty(Convert.ToString(dt1)))
                {
                    this.form.cdate_SaisyuSyobunDate.Value = dt2;
                }
                else if (dt2 > dt1)
                {
                    this.form.cdate_SaisyuSyobunDate.Value = dt2;
                }
            }
            // 20140617 ria EV004828 処分終了日、最終処分終了日 start
            if (chkflg)
            {
                this.form.cdate_SaisyuSyobunDate.Value = string.Empty;
            }
            // 20140617 ria EV004828 処分終了日、最終処分終了日 end
        }

        #region マニ解除抑止制御
        /// <summary>
        /// 運搬終了日のReadOnlyを制御する(一次マニフェストのみ判定)
        /// マニ紐付がされている場合は、運搬終了日を読み取り専用(ReadOnly=tru)にする。
        /// </summary>
        private void ChengeReadOnlyForUpnEndDate(DataTable dt)
        {
            if (dt == null || dt.Rows.Count < 1 || this.maniFlag == 2)
            {
                return;
            }

            bool isRelatedMani = false;
            foreach (DataRow row in dt.Rows)
            {
                if (row != null && !string.IsNullOrEmpty(row["NEXT_SYSTEM_ID"].ToString()))
                {
                    // 明細行一件でも紐付がされている場合は運搬終了報告を読み取り専用にする。
                    isRelatedMani = true;
                    break;
                }
            }

            // 運搬終了日のReadOnly変更
            if (isRelatedMani)
            {
                this.form.cdate_UnpanJyu1.ReadOnly = true;
                this.form.cdate_UnpanJyu2.ReadOnly = true;
            }
        }
        #endregion

        #endregion

        #region データ作成処理(マニ)

        /// <summary>
        /// データ作成
        /// </summary>
        internal virtual Boolean MakeData(
            ref List<T_MANIFEST_ENTRY> entrylist,
            ref List<T_MANIFEST_UPN> upnlist,
            ref List<T_MANIFEST_PRT> prtlist,
            ref List<T_MANIFEST_DETAIL_PRT> detailprtlist,
            ref List<T_MANIFEST_KP_KEIJYOU> keijyoulist,
            ref List<T_MANIFEST_KP_NISUGATA> niugatalist,
            ref List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist,
            ref List<T_MANIFEST_DETAIL> detaillist,
            ref List<T_MANIFEST_RET_DATE> retdatelist,
            bool delflg,
            String SystemId,
            String Seq,
            String SeqRD,
            bool isPrint
        )
        {
            LogUtility.DebugMethodStart(entrylist, upnlist, prtlist, prtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist, delflg, SystemId, Seq, SeqRD, isPrint);

            //システムID(全般･マニ返却日)
            long lSysId = 0;

            //枝番(全般)
            int iSeq = 0;

            //枝番(マニ返却日)
            int iSeqRD = 0;

            switch (this.form.parameters.Mode)
            {
                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード(単票･連票)
                case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード(単票･連票)

                    if (string.IsNullOrEmpty(SeqRD))
                    {
                        MessageBox.Show("旧バージョンで作られたデータの為、返却日データがありません。\r\n最新Verで作成されたデータを使用してください。", "この後、システムエラーが発生します");
                    }

                    lSysId = long.Parse(SystemId);
                    iSeq = int.Parse(Seq) + 1;
                    iSeqRD = int.Parse(SeqRD) + 1;
                    break;

                case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                    // 印刷時はSYSTEM_IDの採番不要(印刷部数ポップアップ側で採番するため)
                    if (isPrint == false)
                    {
                        Common.BusinessCommon.DBAccessor dba = null;
                        dba = new Common.BusinessCommon.DBAccessor();
                        lSysId = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                        iSeq = 1;
                        iSeqRD = 1;
                    }
                    break;

                default://その他モード
                    LogUtility.DebugMethodEnd(false);
                    return false;
            }

            // 一覧から遷移したとき等、伝種区分・連携番号・明細行のコントロールにフォーカス移動されない場合、
            // 連携システムID・連携明細システムIDをクリアされることを防ぐ
            if (!string.IsNullOrEmpty(this.form.cantxt_DenshuKbn.Text)
                && !string.IsNullOrEmpty(this.form.cantxt_No.Text))
            {
                this.GetRenkeiData(this.form.cantxt_DenshuKbn, this.form.cantxt_No, this.form.cantxt_Meisaigyou, false, 2);
            }

            //マニフェスト(T_MANIFEST_ENTRY)データ作成
            entrylist = new List<T_MANIFEST_ENTRY>();
            var ManiEntry = new T_MANIFEST_ENTRY();
            MakeManifestEntry(lSysId, iSeq, ref ManiEntry);
            entrylist.Add(ManiEntry);

            //マニ収集運搬(T_MANIFEST_UPN)データ作成
            upnlist = new List<T_MANIFEST_UPN>();
            T_MANIFEST_UPN ManiUpn = null;
            ManiUpn = new T_MANIFEST_UPN();
            MakeManifestUpn(lSysId, iSeq, 1, ref ManiUpn);
            upnlist.Add(ManiUpn);
            ManiUpn = new T_MANIFEST_UPN();
            MakeManifestUpn(lSysId, iSeq, 2, ref ManiUpn);
            upnlist.Add(ManiUpn);

            //マニ印字(T_MANIFEST_PRT)データ作成
            prtlist = new List<T_MANIFEST_PRT>();
            T_MANIFEST_PRT ManiPrt = new T_MANIFEST_PRT();
            MakeManifestPrt(lSysId, iSeq, ref ManiPrt);
            prtlist.Add(ManiPrt);

            //マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
            detailprtlist = new List<T_MANIFEST_DETAIL_PRT>();
            MakeManifestDetailPrtList(lSysId, iSeq, ref detailprtlist);

            //マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)データ作成
            keijyoulist = new List<T_MANIFEST_KP_KEIJYOU>();
            MakeManifestKeijyouList(lSysId, iSeq, ref keijyoulist);

            //マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
            niugatalist = new List<T_MANIFEST_KP_NISUGATA>();
            MakeManifestNisugataList(lSysId, iSeq, ref niugatalist);

            //マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)データ作成
            houhoulist = new List<T_MANIFEST_KP_SBN_HOUHOU>();
            MakeManifestHouhouList(lSysId, iSeq, ref houhoulist);

            //マニ明細(T_MANIFEST_DETAIL)データ作成
            detaillist = new List<T_MANIFEST_DETAIL>();
            MakeManifestDetailList(lSysId, iSeq, ref detaillist, isPrint);

            //マニ返却日(T_MANIFEST_RET_DATE)データ作成
            retdatelist = new List<T_MANIFEST_RET_DATE>();
            //MakeManifestRetDateList(lSysId, icnt, ref retdatelist);
            MakeManifestRetDateList(lSysId, iSeqRD, ref retdatelist);

            //ここではまだ更新NG 印刷で登録しない限りはDB更新無し
            if (!isPrint)
            {

                //ロールバック時の不整合回避
                this.needRollbackProperties = true;
                bkSystemId = this.form.parameters.SystemId;
                bkSeq = this.form.parameters.Seq;
                bkSeqRD = this.form.parameters.SeqRD;

                //システムID(全般･マニ返却日)
                this.form.parameters.SystemId = lSysId.ToString();

                //枝番(全般)
                this.form.parameters.Seq = iSeq.ToString();

                //枝番(マニ返却日)
                this.form.parameters.SeqRD = iSeqRD.ToString();

                this.form.parameters.Save();
            }

            LogUtility.DebugMethodEnd(entrylist, upnlist, prtlist, prtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist, delflg, SystemId, Seq, SeqRD, isPrint);
            return true;
        }

        /// <summary>登録時エラー対策</summary>
        private bool needRollbackProperties = false;
        private string bkSystemId = "";
        private string bkSeq = "";
        private string bkSeqRD = "";

        /// <summary>
        /// マニフェスト(T_MANIFEST_ENTRY)データ作成
        /// </summary>
        public void MakeManifestEntry(long lSysId, int iSeq, ref T_MANIFEST_ENTRY tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp);

            // システムID  
            tmp.SYSTEM_ID = lSysId;

            // 枝番  
            tmp.SEQ = iSeq;

            // 廃棄物区分CD
            tmp.HAIKI_KBN_CD = 2;

            // 一次マニフェスト区分 
            //if (parentbaseform.bt_func4.Text == "[F4]\r\n1次マニ")
            //{
            //    tmp.FIRST_MANIFEST_KBN = true;
            //}
            //else if (parentbaseform.bt_func4.Text == "[F4]\r\n2次マニ")
            //{
            //    tmp.FIRST_MANIFEST_KBN = false;
            //}
            switch (maniFlag)
            {
                case 1://１次マニフェスト
                    tmp.FIRST_MANIFEST_KBN = false;
                    break;

                case 2://２次マニフェスト
                    tmp.FIRST_MANIFEST_KBN = true;
                    break;
            }

            // 拠点CD 
            if (this.headerform.ctxt_KyotenCd.Text != string.Empty)
            {
                tmp.KYOTEN_CD = Convert.ToInt16(this.headerform.ctxt_KyotenCd.Text);
            }

            // 取引先CD
            if (this.form.cantxt_TorihikiCd.Text != string.Empty)
            {
                tmp.TORIHIKISAKI_CD = this.form.cantxt_TorihikiCd.Text;
            }

            // 事前協議番号 
            if (this.form.cntxt_jizenkyougi.Text != string.Empty)
            {
                tmp.JIZEN_NUMBER = this.form.cntxt_jizenkyougi.Text;
            }

            // 事前協議年月日
            if (this.form.cdate_JizenKyougi.Value != null)
            {
                tmp.JIZEN_DATE = (DateTime)this.form.cdate_JizenKyougi.Value;
            }

            // 交付年月日
            if (this.form.cdate_KohuDate.Value != null)
            {
                tmp.KOUFU_DATE = (DateTime)this.form.cdate_KohuDate.Value;
            }

            // 交付番号区分
            if (this.form.crdo_KohuTujyo.Checked == true)
            {
                tmp.KOUFU_KBN = 1;
            }
            else
            {
                tmp.KOUFU_KBN = 2;
            }

            // 交付番号 
            tmp.MANIFEST_ID = this.form.cantxt_KohuNo.Text;

            // 整理番号 
            tmp.SEIRI_ID = this.form.cantxt_SeiriNo.Text;

            // 交付担当者 
            tmp.KOUFU_TANTOUSHA = this.form.ctxt_KohuTantou.Text;

            // 交付担当者所属 
            tmp.KOUFU_TANTOUSHA_SHOZOKU = this.form.ctxt_KohuTantouShozoku.Text;

            // 排出事業者CD 
            tmp.HST_GYOUSHA_CD = this.form.cantxt_HaisyutuGyousyaCd.Text;

            // 排出事業者名称
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GYOUSHA_NAME = this.form.ctxt_HaisyutuGyousyaName1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuGyousyaName2.Text))
            {
                tmp.HST_GYOUSHA_NAME = this.form.ctxt_HaisyutuGyousyaName1.Text.PadRight(40, ' ') + this.form.ctxt_HaisyutuGyousyaName2.Text;
            }
            else
            {
                tmp.HST_GYOUSHA_NAME = this.form.ctxt_HaisyutuGyousyaName1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業者郵便番号 
            tmp.HST_GYOUSHA_POST = this.form.cnt_HaisyutuGyousyaZip.Text;

            // 排出事業者電話番号 
            tmp.HST_GYOUSHA_TEL = this.form.cnt_HaisyutuGyousyaTel.Text;

            // 排出事業者住所  
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GYOUSHA_ADDRESS = this.form.ctxt_HaisyutuGyousyaAdd1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuGyousyaAdd2.Text))
            {
                tmp.HST_GYOUSHA_ADDRESS = this.form.ctxt_HaisyutuGyousyaAdd1.Text.PadRight(48, ' ') + this.form.ctxt_HaisyutuGyousyaAdd2.Text;
            }
            else
            {
                tmp.HST_GYOUSHA_ADDRESS = this.form.ctxt_HaisyutuGyousyaAdd1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業場CD 
            tmp.HST_GENBA_CD = this.form.cantxt_HaisyutuJigyoubaName.Text;

            // 排出事業場名称 
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GENBA_NAME = this.form.ctxt_HaisyutuJigyoubaName1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuJigyoubaName2.Text))
            {
                tmp.HST_GENBA_NAME = this.form.ctxt_HaisyutuJigyoubaName1.Text.PadRight(40, ' ') + this.form.ctxt_HaisyutuJigyoubaName2.Text;
            }
            else
            {
                tmp.HST_GENBA_NAME = this.form.ctxt_HaisyutuJigyoubaName1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業場郵便番号 
            tmp.HST_GENBA_POST = this.form.cnt_HaisyutuJigyoubaZip.Text;

            // 排出事業場電話番号  
            tmp.HST_GENBA_TEL = this.form.cnt_HaisyutuJigyoubaTel.Text;

            // 排出事業場住所 
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GENBA_ADDRESS = this.form.ctxt_HaisyutuJigyoubaAdd1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuJigyoubaAdd2.Text))
            {
                tmp.HST_GENBA_ADDRESS = this.form.ctxt_HaisyutuJigyoubaAdd1.Text.PadRight(48, ' ') + this.form.ctxt_HaisyutuJigyoubaAdd2.Text;
            }
            else
            {
                tmp.HST_GENBA_ADDRESS = this.form.ctxt_HaisyutuJigyoubaAdd1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 備考 
            tmp.BIKOU = this.form.ctxt_UnpanJigyobaTokki.Text;

            // 混合種類
            tmp.KONGOU_SHURUI_CD = this.form.cantxt_KongoCd.Text;

            // 実績数量 
            if (this.form.cntxt_JissekiSuryo.Text != string.Empty)
            {
                tmp.HAIKI_SUU = Convert.ToDecimal(this.form.cntxt_JissekiSuryo.Text);
            }

            // 実績単位CD
            if (this.form.canTxt_JissekiTaniCd.Text != string.Empty)
            {
                tmp.HAIKI_UNIT_CD = Convert.ToInt16(this.form.canTxt_JissekiTaniCd.Text);
            }

            //20250402
            //運搬終了日1
            if (this.form.cdate_UnpanDate_1.Value != null)
            {
                tmp.UNPAN_DATE = (DateTime)this.form.cdate_UnpanDate_1.Value;
            }

            //運搬終了日2
            if (this.form.cdate_UnpanDate_2.Value != null)
            {
                tmp.UNPAN_DATE_2 = (DateTime)this.form.cdate_UnpanDate_2.Value;
            }

            //処分受領日
            if (this.form.cdate_ShobunJuryoDate.Value != null)
            {
                tmp.SHOBUN_JURYO_DATE = (DateTime)this.form.cdate_ShobunJuryoDate.Value;
            }

            //処分終了日
            if (this.form.cdate_ShobunShuryoDate.Value != null)
            {
                tmp.SHOBUN_SHURYO_DATE = (DateTime)this.form.cdate_ShobunShuryoDate.Value;
            }

            // 数量の合計
            if (this.form.ctxt_TotalSuryo.Text != string.Empty)
            {
                tmp.TOTAL_SUU = Convert.ToDecimal(this.form.ctxt_TotalSuryo.Text);
            }

            // 換算後数量の合計 
            if (!string.IsNullOrEmpty(this.form.ctxt_KansangoTotalSuryo.Text) && !string.IsNullOrEmpty(this.form.ctxt_KansangoTotalSuryo.Text.Replace(this.unit_name, "")))
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                tmp.TOTAL_KANSAN_SUU = Convert.ToDecimal(this.form.ctxt_KansangoTotalSuryo.Text.Replace(this.unit_name, ""));
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            else
            {
                tmp.TOTAL_KANSAN_SUU = 0;
            }

            // 減容後数量の合計 
            if (!string.IsNullOrEmpty(this.form.ctxt_GenyoyugoTotalSuryo.Text) &&
                !string.IsNullOrEmpty(this.form.ctxt_GenyoyugoTotalSuryo.Text.Replace(this.unit_name, "")) && maniFlag == 1)
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                tmp.TOTAL_GENNYOU_SUU = Convert.ToDecimal(this.form.ctxt_GenyoyugoTotalSuryo.Text.Replace(this.unit_name, ""));
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            else
            {
                tmp.TOTAL_GENNYOU_SUU = 0;
            }

            // 中間処理産業廃棄物区分
            if (this.form.ccbx_TyukanTyoubo.Checked == true)
            {
                tmp.CHUUKAN_HAIKI_KBN = 1;
            }
            else if (this.form.ccbx_TyukanKisai.Checked == true)
            {
                tmp.CHUUKAN_HAIKI_KBN = 2;
            }
            else
            {
                tmp.CHUUKAN_HAIKI_KBN = 0;
            }

            // 中間処理産業廃棄物
            tmp.CHUUKAN_HAIKI = this.form.ctxt_TyukanHaikibutu.Text;

            // 最終処分の場所（予定）区分
            //2013-11-08 Upd ogawamut IT No.136
            //if (this.form.ccbx_TyukanTyoubo.Checked == true)
            if (this.form.ccbx_SaisyuTyoubo.Checked == true)
            {
                tmp.LAST_SBN_YOTEI_KBN = 1;
            }
            //2013-11-08 Upd ogawamut IT No.136
            //else if (this.form.ccbx_TyukanKisai.Checked == true)
            else if (this.form.ccbx_SaisyuKisai.Checked == true)
            {
                tmp.LAST_SBN_YOTEI_KBN = 2;
            }
            else
            {
                tmp.LAST_SBN_YOTEI_KBN = 0;
            }

            // 最終処分の場所（予定）業者CD 
            tmp.LAST_SBN_YOTEI_GYOUSHA_CD = this.form.cantxt_SaisyuGyousyaCd.Text;

            // 最終処分の場所（予定）現場CD 
            tmp.LAST_SBN_YOTEI_GENBA_CD = this.form.cantxt_SaisyuGyousyaNameCd.Text;

            // 最終処分の場所（予定）現場名称 
            tmp.LAST_SBN_YOTEI_GENBA_NAME = this.form.cantxt_SaisyuGyousyaName.Text;

            // 最終処分の場所（予定）郵便番号  
            //tmp.LAST_SBN_YOTEI_GENBA_POST = string.Empty;

            // 最終処分の場所（予定）電話番号  
            //tmp.LAST_SBN_YOTEI_GENBA_TEL = string.Empty;

            // 最終処分の場所（予定）住所 
            tmp.LAST_SBN_YOTEI_GENBA_ADDRESS = this.form.ctxt_SaisyuGyousyaAdd.Text;

            // 処分受託者CD 
            tmp.SBN_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;

            // 処分受託者名称 
            tmp.SBN_GYOUSHA_NAME = this.form.cantxt_SyobunJyutakuName.Text;

            // 処分受託者郵便番号
            tmp.SBN_GYOUSHA_POST = this.form.cnt_SyobunJyutakuZip.Text;

            // 処分受託者電話番号
            tmp.SBN_GYOUSHA_TEL = this.form.cnt_SyobunJyutakuTel.Text;

            // 処分受託者住所 
            tmp.SBN_GYOUSHA_ADDRESS = this.form.ctxt_SyobunJyutakuAdd.Text;

            // 積替保管業者CD 
            tmp.TMH_GYOUSHA_CD = this.form.cantxt_TumiGyoCd.Text;

            // 積替保管業者名称 
            tmp.TMH_GYOUSHA_NAME = this.form.ctxt_TumiGyoName.Text;

            // 積替保管場CD 
            tmp.TMH_GENBA_CD = this.form.cantxt_TumiHokaNameCd.Text;

            // 積替保管場名称  
            tmp.TMH_GENBA_NAME = this.form.ctxt_TumiHokaName.Text;

            // 積替保管場郵便番号 
            tmp.TMH_GENBA_POST = this.form.cnt_TumiHokaZip.Text;

            // 積替保管場電話番号
            tmp.TMH_GENBA_TEL = this.form.cnt_TumiHokaTel.Text;

            // 積替保管場住所 
            tmp.TMH_GENBA_ADDRESS = this.form.ctxt_TumiHokaAdd.Text;

            // 有価物拾集有無 
            if (this.form.cckb_TumiHokaAri.Checked == true)
            {
                tmp.YUUKA_KBN = 1;
            }
            else
            {
                // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                if (this.form.cckb_TumiHokaNasi.Checked == true)
                {
                    tmp.YUUKA_KBN = 2;
                }
                // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
            }

            // 有価物拾集量
            if (this.form.cntxt_TumiHokaJisseki.Text != string.Empty)
            {
                tmp.YUUKA_SUU = Convert.ToDecimal(this.form.cntxt_TumiHokaJisseki.Text);
            }

            //有価物拾集量単位CD
            if (this.form.cckb_TumiHokaT.Checked == true)
            {
                tmp.YUUKA_UNIT_CD = 1;
            }
            else if (this.form.cckb_TumiHokaM3.Checked == true)
            {
                tmp.YUUKA_UNIT_CD = 2;
            }

            // 処分の受領者CD 
            tmp.SBN_JYURYOUSHA_CD = this.form.cantxt_SyobunJyuCd.Text;

            // 処分の受領者名称 
            tmp.SBN_JYURYOUSHA_NAME = this.form.ctxt_SyobunJyuName.Text;

            // 処分の受領担当者CD 
            tmp.SBN_JYURYOU_TANTOU_CD = this.form.cantxt_SyobunJyuUntenCd.Text;

            // 処分の受領担当者名
            tmp.SBN_JYURYOU_TANTOU_NAME = this.form.cantxt_SyobunJyuUntenName.Text;

            // 処分受領日  
            if (this.form.cdate_SyobunJyu.Value != null)
            {
                tmp.SBN_JYURYOU_DATE = (DateTime)this.form.cdate_SyobunJyu.Value;
            }

            // 処分の受託者CD  
            tmp.SBN_JYUTAKUSHA_CD = this.form.cantxt_SyobunSyoCd.Text;

            // 処分の受託者 名称 
            tmp.SBN_JYUTAKUSHA_NAME = this.form.ctxt_SyobunSyoName.Text;

            // 処分担当者CD 
            tmp.SBN_TANTOU_CD = this.form.cantxt_SyobunSyoUntenCd.Text;

            // 処分担当者名 
            tmp.SBN_TANTOU_NAME = this.form.cantxt_SyobunSyoUntenName.Text;

            // 最終処分業者CD 
            tmp.LAST_SBN_GYOUSHA_CD = this.form.cantxt_SaisyuSyobunGyoCd.Text;

            // 最終処分場CD 
            tmp.LAST_SBN_GENBA_CD = this.form.cantxt_SaisyuSyobunbaCD.Text;

            // 最終処分場 名称 
            tmp.LAST_SBN_GENBA_NAME = this.form.ctxt_SaisyuSyobunGyoName.Text;

            // 最終処分場 郵便番号
            tmp.LAST_SBN_GENBA_POST = this.form.ctxt_SaisyuSyobunGyoZip.Text;

            // 最終処分場 住所 
            tmp.LAST_SBN_GENBA_ADDRESS = this.form.ctxt_SaisyuBasyoSyozai.Text;

            // 最終処分場 処分先番号 
            tmp.LAST_SBN_GENBA_NUMBER = this.form.ctxt_SaisyuBasyoNo.Text;

            // 最終処分 確認者 
            tmp.LAST_SBN_CHECK_NAME = this.form.ctxt_SaisyuSyobun.Text;

            // 照合確認 B1票  
            if (this.form.cdate_SyougouKakuninB1.Value != null)
            {
                tmp.CHECK_B1 = (DateTime)this.form.cdate_SyougouKakuninB1.Value;
            }

            // 照合確認 B2票 
            if (this.form.cdate_SyougouKakuninB2.Value != null)
            {
                tmp.CHECK_B2 = (DateTime)this.form.cdate_SyougouKakuninB2.Value;
            }

            // 照合確認 D票 
            if (this.form.cdate_SyougouKakuninD.Value != null)
            {
                tmp.CHECK_D = (DateTime)this.form.cdate_SyougouKakuninD.Value;
            }

            // 照合確認 E票 
            if (this.form.cdate_SyougouKakuninE.Value != null)
            {
                tmp.CHECK_E = (DateTime)this.form.cdate_SyougouKakuninE.Value;
            }

            //// 連携伝種区分CD 
            //if (this.form.parameters.RenkeiDenshuKbnCd != string.Empty)
            //{
            //    tmp.RENKEI_DENSHU_KBN_CD = Convert.ToInt16(this.form.parameters.RenkeiDenshuKbnCd);
            //}

            // 連携システムID 
            if (this.form.parameters.RenkeiSystemId != string.Empty)
            {
                tmp.RENKEI_SYSTEM_ID = Convert.ToInt64(this.form.parameters.RenkeiSystemId);
            }

            // 連携明細システムID 
            if (this.form.parameters.RenkeiMeisaiSystemId != string.Empty)
            {
                tmp.RENKEI_MEISAI_SYSTEM_ID = Convert.ToInt64(this.form.parameters.RenkeiMeisaiSystemId);
            }

            var WHO = new DataBinderLogic<T_MANIFEST_ENTRY>(tmp);
            WHO.SetSystemProperty(tmp, false);

            tmp.DELETE_FLG = false;

            // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 start

            //RENKEI_DENSHU_KBN_CD←連携伝種区分
            if (this.form.cantxt_DenshuKbn.Text != string.Empty)
            {
                tmp.RENKEI_DENSHU_KBN_CD = Convert.ToInt16(this.form.cantxt_DenshuKbn.Text);
            }
            else
            {
                tmp.RENKEI_DENSHU_KBN_CD = SqlInt16.Null;
            }
            //RENKEI_SYSTEM_ID←連携System_Id,RENKEI_MEISAI_SYSTEM_ID←連携明細System_Id
            string[] id = new string[2];
            id = GetRenkeiSystemId();
            if (id != null && id.Length != 0)
            {
                tmp.RENKEI_SYSTEM_ID = Convert.ToInt64(id[0]);
                tmp.RENKEI_MEISAI_SYSTEM_ID = Convert.ToInt64(id[1]);
            }
            // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 end

            //連携明細モード
            tmp.RENKEI_MEISAI_MODE = Convert.ToInt16(1);
            if (!string.IsNullOrEmpty(this.form.cantxt_DenshuKbn.Text))
            {
                int iKbn = 0;
                iKbn = Convert.ToInt32(this.form.cantxt_DenshuKbn.Text);

                //受入と計量のみ処理を通す
                if (iKbn.Equals((int)DENSHU_KBN.UKEIRE) || iKbn.Equals((int)DENSHU_KBN.KEIRYOU))
                {
                    if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                    {
                        tmp.RENKEI_MEISAI_MODE = Convert.ToInt16(2);
                    }
                }
            }

            // レイアウト区分
            tmp.MANIFEST_MERCURY_CHECK = Convert.ToInt16(manifestMercuryList[1].Text);

            // ﾏﾆ用紙へ印字区分
            bool checkStatus = r_framework.Configuration.AppConfig.AppOptions.IsManiImport();
            if (checkStatus && !string.IsNullOrEmpty(manifestKamiList[1].Text))
            {
                tmp.MANIFEST_KAMI_CHECK = Convert.ToInt16(manifestKamiList[1].Text);
            }
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp);
        }

        /// <summary>
        /// マニ収集運搬(T_MANIFEST_UPN)データ作成
        /// </summary>
        public void MakeManifestUpn(long lSysId, int iSeq, int No, ref T_MANIFEST_UPN tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, No, tmp);

            tmp.SYSTEM_ID = lSysId;
            tmp.SEQ = iSeq;

            tmp.UPN_ROUTE_NO = Convert.ToInt16(No);
            if (No == 1)
            {
                tmp.UPN_GYOUSHA_CD = this.form.cantxt_UnpanJyutaku1NameCd.Text;
                tmp.UPN_GYOUSHA_NAME = this.form.cantxt_UnpanJyutaku1Name.Text;
                tmp.UPN_GYOUSHA_POST = this.form.cnt_UnpanJyutaku1Zip.Text;
                tmp.UPN_GYOUSHA_TEL = this.form.cnt_UnpanJyutaku1Tel.Text;
                tmp.UPN_GYOUSHA_ADDRESS = this.form.ctxt_UnpanJyutaku1Add.Text;
                tmp.UPN_HOUHOU_CD = this.form.cantxt_UnpanHouhou1CD.Text;
                tmp.SHARYOU_CD = this.form.cantxt_Jyutaku1SyaNo.Text;
                tmp.SHARYOU_NAME = this.form.ctxt_Jyutaku1SyaNo.Text;
                tmp.SHASHU_CD = this.form.cantxt_Jyutaku1Syasyu.Text;
                tmp.SHASHU_NAME = this.form.ctxt_Jyutaku1Syasyu.Text;
                if (this.form.cckb_Jyutaku1HokanAri.Checked == true)
                {
                    tmp.TMH_KBN = 1;
                }
                else
                {
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                    if (this.form.cckb_Jyutaku1HokanNasi.Checked == true)
                    {
                        tmp.TMH_KBN = 0;
                    }
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
                }
                tmp.UPN_SAKI_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;
                tmp.UPN_SAKI_GENBA_CD = this.form.cantxt_UnpanJyugyobaNameCd.Text;
                tmp.UPN_SAKI_GENBA_NAME = this.form.cantxt_UnpanJyugyobaName.Text;
                tmp.UPN_SAKI_GENBA_POST = this.form.cnt_UnpanJyugyobaZip.Text;
                tmp.UPN_SAKI_GENBA_TEL = this.form.cnt_UnpanJyugyobaTel.Text;
                tmp.UPN_SAKI_GENBA_ADDRESS = this.form.ctxt_UnpanJyugyobaAdd.Text;
                tmp.UPN_JYUTAKUSHA_CD = this.form.cantxt_UnpanJyuCd1.Text;
                tmp.UPN_JYUTAKUSHA_NAME = this.form.ctxt_UnpanJyuName1.Text;
                tmp.UNTENSHA_CD = this.form.cantxt_UnpanJyuUntenCd1.Text;
                tmp.UNTENSHA_NAME = this.form.cantxt_UnpanJyuUntenName1.Text;
                if (this.form.cdate_UnpanJyu1.Value != null)
                {
                    tmp.UPN_END_DATE = (DateTime)this.form.cdate_UnpanJyu1.Value;
                }

            }
            else
            {
                //if (this.form.cantxt_TumiGyoCd.Enabled == false) //斜線つけてもデータは保持（印字の時だけ消します ）
                //{
                //    return;
                //}
                tmp.UPN_GYOUSHA_CD = this.form.cantxt_UnpanJyutaku2NameCd.Text;
                tmp.UPN_GYOUSHA_NAME = this.form.cantxt_UnpanJyutaku2Name.Text;
                tmp.UPN_GYOUSHA_POST = this.form.cnt_UnpanJyutaku2Zip.Text;
                tmp.UPN_GYOUSHA_TEL = this.form.cnt_UnpanJyutaku2Tel.Text;
                tmp.UPN_GYOUSHA_ADDRESS = this.form.ctxt_UnpanJyutaku2Add.Text;
                tmp.UPN_HOUHOU_CD = this.form.cantxt_UnpanHouhou2CD.Text;
                tmp.SHARYOU_CD = this.form.cantxt_Jyutaku2SyaNo.Text;
                tmp.SHARYOU_NAME = this.form.ctxt_Jyutaku2SyaNo.Text;
                tmp.SHASHU_CD = this.form.cantxt_Jyutaku2Syasyu.Text;
                tmp.SHASHU_NAME = this.form.ctxt_Jyutaku2Syasyu.Text;
                if (this.form.cckb_Jyutaku2HokanAri.Checked == true)
                {
                    tmp.TMH_KBN = 1;
                }
                else
                {
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                    if (this.form.cckb_Jyutaku2HokanNasi.Checked == true)
                    {
                        tmp.TMH_KBN = 0;
                    }
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
                }
                tmp.UPN_SAKI_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;
                tmp.UPN_SAKI_GENBA_CD = this.form.cantxt_UnpanJyugyobaNameCd.Text;
                tmp.UPN_SAKI_GENBA_NAME = this.form.cantxt_UnpanJyugyobaName.Text;
                tmp.UPN_SAKI_GENBA_POST = this.form.cnt_UnpanJyugyobaZip.Text;
                tmp.UPN_SAKI_GENBA_TEL = this.form.cnt_UnpanJyugyobaTel.Text;
                tmp.UPN_SAKI_GENBA_ADDRESS = this.form.ctxt_UnpanJyugyobaAdd.Text;
                tmp.UPN_JYUTAKUSHA_CD = this.form.cantxt_UnpanJyuCd2.Text;
                tmp.UPN_JYUTAKUSHA_NAME = this.form.ctxt_UnpanJyuName2.Text;
                tmp.UNTENSHA_CD = this.form.cantxt_UnpanJyuUntenCd2.Text;
                tmp.UNTENSHA_NAME = this.form.cantxt_UnpanJyuUntenName2.Text;
                if (this.form.cdate_UnpanJyu2.Value != null)
                {
                    tmp.UPN_END_DATE = (DateTime)this.form.cdate_UnpanJyu2.Value;
                }
            }

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_UPN>(tmp);
            WHO.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, No, tmp);
        }

        /// <summary>
        /// マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
        /// </summary>
        public void MakeManifestDetailPrtList(long lSysId, int iSeq, ref  List<T_MANIFEST_DETAIL_PRT> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            T_MANIFEST_DETAIL_PRT tmp = null;

            //安定
            //1行目:01コンクリートがら
            int icnt = 1;
            string suryo = this.form.cntxt_AnteiSuryo01.Text;
            if (this.form.cckb_Antei01.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei01, this.form.cntxt_AnteiSuryo01,
                    ref icnt, "1", "コンクリートがら", this.form.cckb_Antei01.Checked);
                list.Add(tmp);
            }

            //2行目:02アスコンがら
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo02.Text;
            if (this.form.cckb_Antei02.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei02, this.form.cntxt_AnteiSuryo02,
                    ref icnt, "2", "アスコンがら", this.form.cckb_Antei02.Checked);
                list.Add(tmp);
            }

            //3行目:03その他がれき類
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo03.Text;
            if (this.form.cckb_Antei03.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei03, this.form.cntxt_AnteiSuryo03,
                    ref icnt, "3", "その他がれき類", this.form.cckb_Antei03.Checked);
                list.Add(tmp);
            }

            //4行目:04ガラス・陶磁器くず
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo04.Text;
            if (this.form.cckb_Antei04.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei04, this.form.cntxt_AnteiSuryo04,
                    ref icnt, "4", "ガラス・陶磁器くず", this.form.cckb_Antei04.Checked);
                list.Add(tmp);
            }

            //5行目:05廃プラスチック類
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo05.Text;
            if (this.form.cckb_Antei05.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei05, this.form.cntxt_AnteiSuryo05,
                    ref icnt, "5", "廃プラスチック類", this.form.cckb_Antei05.Checked);
                list.Add(tmp);
            }

            //6行目:06金属くず
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo06.Text;
            if (this.form.cckb_Antei06.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei06, this.form.cntxt_AnteiSuryo06,
                    ref icnt, "6", "金属くず", this.form.cckb_Antei06.Checked);
                list.Add(tmp);
            }

            //7行目:07混合（安定型のみ）
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo07.Text;
            if (this.form.cckb_Antei07.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei07, this.form.cntxt_AnteiSuryo07,
                    ref icnt, "7", "混合（安定型のみ）", this.form.cckb_Antei07.Checked);
                list.Add(tmp);
            }

            //8行目:08石綿含有産業廃棄物
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo08.Text;
            if (this.form.cckb_Antei08.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei08, this.form.cntxt_AnteiSuryo08,
                    ref icnt, "8", "石綿含有産業廃棄物", this.form.cckb_Antei08.Checked);
                list.Add(tmp);
            }

            //9行目:安定フリー1
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo01.Text;
            string code = this.form.cantxt_AnteiFreeCd01.Text;
            if (this.form.cckb_AnteiFree01.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree01, this.form.cntxt_AnteiFreeSuryo01,
                    ref icnt, this.form.cantxt_AnteiFreeCd01.Text, this.form.ctxt_AnteiFreeName01.Text,
                    this.form.cckb_AnteiFree01.Checked);
                list.Add(tmp);
            }

            //10行目:安定フリー2
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo02.Text;
            code = this.form.cantxt_AnteiFreeCd02.Text;
            if (this.form.cckb_AnteiFree02.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree02, this.form.cntxt_AnteiFreeSuryo02,
                    ref icnt, this.form.cantxt_AnteiFreeCd02.Text, this.form.ctxt_AnteiFreeName02.Text,
                    this.form.cckb_AnteiFree02.Checked);
                list.Add(tmp);
            }

            //11行目:安定フリー3
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo03.Text;
            code = this.form.cantxt_AnteiFreeCd03.Text;
            if (this.form.cckb_AnteiFree03.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree03, this.form.cntxt_AnteiFreeSuryo03,
                    ref icnt, this.form.cantxt_AnteiFreeCd03.Text, this.form.ctxt_AnteiFreeName03.Text,
                    this.form.cckb_AnteiFree03.Checked);
                list.Add(tmp);
            }

            //12行目:安定フリー4
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo04.Text;
            code = this.form.cantxt_AnteiFreeCd04.Text;
            if (this.form.cckb_AnteiFree04.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree04, this.form.cntxt_AnteiFreeSuryo04,
                    ref icnt, this.form.cantxt_AnteiFreeCd04.Text, this.form.ctxt_AnteiFreeNam04.Text,
                    this.form.cckb_AnteiFree04.Checked);
                list.Add(tmp);
            }

            //管理
            //13行目:11建設汚泥
            icnt++;
            suryo = this.form.cntxt_KanriSuryo11.Text;
            if (this.form.cckb_Kanri01.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri01, this.form.cntxt_KanriSuryo11,
                    ref icnt, "11", "建設汚泥", this.form.cckb_Kanri01.Checked);
                list.Add(tmp);
            }

            //14行目:12紙くず
            icnt++;
            suryo = this.form.cntxt_KanriSuryo12.Text;
            if (this.form.cckb_Kanri02.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri02, this.form.cntxt_KanriSuryo12,
                    ref icnt, "12", "紙くず", this.form.cckb_Kanri02.Checked);
                list.Add(tmp);
            }

            //15行目:13木くず
            icnt++;
            suryo = this.form.cntxt_KanriSuryo13.Text;
            if (this.form.cckb_Kanri03.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri03, this.form.cntxt_KanriSuryo13,
                    ref icnt, "13", "木くず", this.form.cckb_Kanri03.Checked);
                list.Add(tmp);
            }

            //16行目:14繊維くず
            icnt++;
            suryo = this.form.cntxt_KanriSuryo14.Text;
            if (this.form.cckb_Kanri04.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri04, this.form.cntxt_KanriSuryo14,
                    ref icnt, "14", "繊維くず", this.form.cckb_Kanri04.Checked);
                list.Add(tmp);
            }

            //17行目:15廃石膏ボード
            icnt++;
            suryo = this.form.cntxt_KanriSuryo15.Text;
            if (this.form.cckb_Kanri05.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri05, this.form.cntxt_KanriSuryo15,
                    ref icnt, "15", "廃石膏ボード", this.form.cckb_Kanri05.Checked);
                list.Add(tmp);
            }

            //18行目:16混合（管理型含む）
            icnt++;
            suryo = this.form.cntxt_KanriSuryo16.Text;
            if (this.form.cckb_Kanri06.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri06, this.form.cntxt_KanriSuryo16,
                    ref icnt, "16", "混合（管理型含む）", this.form.cckb_Kanri06.Checked);
                list.Add(tmp);
            }

            //19行目:17石綿含有産業廃棄物
            icnt++;
            suryo = this.form.cntxt_KanriSuryo17.Text;
            if (this.form.cckb_Kanri17.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri17, this.form.cntxt_KanriSuryo17,
                    ref icnt, "17", "石綿含有産業廃棄物", this.form.cckb_Kanri17.Checked);
                list.Add(tmp);
            }

            //20行目:管理フリー1
            icnt++;
            suryo = this.form.cntxt_KanriFreeSuryo01.Text;
            code = this.form.cantxt_KanriFreeCd01.Text;
            if (this.form.cckb_KanriFree01.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_KanriFree01, this.form.cntxt_KanriFreeSuryo01,
                    ref icnt, this.form.cantxt_KanriFreeCd01.Text, this.form.ctxt_KanriFreeName01.Text,
                    this.form.cckb_KanriFree01.Checked);
                list.Add(tmp);
            }

            //21行目:管理フリー2
            icnt++;
            suryo = this.form.cntxt_KanriFreeSuryo02.Text;
            code = this.form.cantxt_KanriFreeCd02.Text;
            if (this.form.cckb_KanriFree02.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_KanriFree02, this.form.cntxt_KanriFreeSuryo02,
                    ref icnt, this.form.cantxt_KanriFreeCd02.Text, this.form.ctxt_KanriFreeName02.Text,
                    this.form.cckb_KanriFree02.Checked);
                list.Add(tmp);
            }

            //22行目:管理フリー3
            icnt++;
            suryo = this.form.cntxt_KanriFreeSuryo03.Text;
            code = this.form.cantxt_KanriFreeCd03.Text;
            if (this.form.cckb_KanriFree03.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_KanriFree03, this.form.cntxt_KanriFreeSuryo03,
                    ref icnt, this.form.cantxt_KanriFreeCd03.Text, this.form.ctxt_KanriFreeName03.Text,
                    this.form.cckb_KanriFree03.Checked);
                list.Add(tmp);
            }

            //特別管理産廃
            //23行目:21廃石綿等
            icnt++;
            suryo = this.form.cntxt_TokubetuSuryo21.Text;
            if (this.form.cckb_Tokubetu21.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                //特別管理産廃
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Tokubetu21, this.form.cntxt_TokubetuSuryo21,
                    ref icnt, "21", "廃石綿等", this.form.cckb_Tokubetu21.Checked);
                list.Add(tmp);
            }

            //24行目:特別管理産廃フリー1
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo01.Text;
            code = this.form.cantxt_TokubetuFreeCd01.Text;
            if (this.form.cckb_TokubetuFree01.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree01, this.form.cntxt_TokubetuFreeSuryo01,
                    ref icnt, this.form.cantxt_TokubetuFreeCd01.Text, this.form.cantxt_TokubetuFreeName01.Text,
                    this.form.cckb_TokubetuFree01.Checked);
                list.Add(tmp);
            }

            //25行目:特別管理産廃フリー2
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo02.Text;
            code = this.form.cantxt_TokubetuFreeCd02.Text;
            if (this.form.cckb_TokubetuFree02.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree02, this.form.cntxt_TokubetuFreeSuryo02,
                    ref icnt, this.form.cantxt_TokubetuFreeCd02.Text, this.form.cantxt_TokubetuFreeName02.Text,
                    this.form.cckb_TokubetuFree02.Checked);
                list.Add(tmp);
            }

            //26行目:特別管理産廃フリー3
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo03.Text;
            code = this.form.cantxt_TokubetuFreeCd03.Text;
            if (this.form.cckb_TokubetuFree03.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree03, this.form.cntxt_TokubetuFreeSuryo03,
                    ref icnt, this.form.cantxt_TokubetuFreeCd03.Text, this.form.cantxt_TokubetuFreeName03.Text,
                    this.form.cckb_TokubetuFree03.Checked);
                list.Add(tmp);
            }

            //27行目:特別管理産廃フリー4
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo04.Text;
            code = this.form.cantxt_TokubetuFreeCd04.Text;
            if (this.form.cckb_TokubetuFree04.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree04, this.form.cntxt_TokubetuFreeSuryo04,
                    ref icnt, this.form.cantxt_TokubetuFreeCd04.Text, this.form.cantxt_TokubetuFreeName04.Text,
                    this.form.cckb_TokubetuFree04.Checked);
                list.Add(tmp);
            }

            //28行目:特別管理産廃フリー5
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo05.Text;
            code = this.form.cantxt_TokubetuFreeCd05.Text;
            if (this.form.cckb_TokubetuFree05.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_DETAIL_PRT();
                MakeManifestDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree05, this.form.cntxt_TokubetuFreeSuryo05,
                    ref icnt, this.form.cantxt_TokubetuFreeCd05.Text, this.form.cantxt_TokubetuFreeName05.Text,
                    this.form.cckb_TokubetuFree05.Checked);
                list.Add(tmp);
            }

            //icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニ印字(T_MANIFEST_PRT)データ作成
        /// </summary>
        public void MakeManifestPrt(long lSysId, int iSeq, ref T_MANIFEST_PRT tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp);

            // システムID  
            tmp.SYSTEM_ID = lSysId;

            // 枝番  
            tmp.SEQ = iSeq;

            // 印字数量
            if (this.form.cntxt_TotalJyuryo.Text != string.Empty)
            {
                tmp.PRT_SUU = Convert.ToDecimal(this.form.cntxt_TotalJyuryo.Text);
            }

            // 印字単位CD
            if (this.form.cntxt_SanpaiTani.Text != string.Empty)
            {
                tmp.PRT_UNIT_CD = Convert.ToInt16(this.form.cntxt_SanpaiTani.Text);
            }

            // 印字廃棄物名称CD 
            tmp.PRT_HAIKI_NAME_CD = this.form.cantxt_SanpaiSyuruiCd.Text;

            // 印字廃棄物名称 
            tmp.PRT_HAIKI_NAME = this.form.ctxt_SanpaiSyuruiName.Text;

            // 斜線項目備考 
            if (this.form.ctxt_UnpanJigyobaTokki.Enabled == false)
            {
                tmp.SLASH_BIKOU_FLG = true;
            }
            else
            {
                tmp.SLASH_BIKOU_FLG = false;
            }

            // 斜線項目中間処理産業廃棄物
            if (this.form.ccbx_TyukanTyoubo.Enabled == false)
            {
                tmp.SLASH_CHUUKAN_FLG = true;
            }
            else
            {
                tmp.SLASH_CHUUKAN_FLG = false;
            }

            // 斜線項目積替保管 
            if (this.form.cantxt_TumiGyoCd.Enabled == false)
            {
                tmp.SLASH_TSUMIHO_FLG = true;
            }
            else
            {
                tmp.SLASH_TSUMIHO_FLG = false;
            }

            // 斜線項目事前協議
            if (this.form.cntxt_jizenkyougi.Enabled == false)
            {
                tmp.SLASH_JIZENKYOUGI_FLG = true;
            }
            else
            {
                tmp.SLASH_JIZENKYOUGI_FLG = false;
            }

            // 斜線項目運搬受託者2
            if (this.form.cnt_UnpanJyutaku2Zip.Enabled == false)
            {
                tmp.SLASH_UPN_GYOUSHA2_FLG = true;
            }
            else
            {
                tmp.SLASH_UPN_GYOUSHA2_FLG = false;
            }

            // 斜線項目運搬受託者3 
            tmp.SLASH_UPN_GYOUSHA3_FLG = false;

            // 斜線項目運搬の受託者2
            if (this.form.cantxt_UnpanJyuCd2.Enabled == false)
            {
                tmp.SLASH_UPN_JYUTAKUSHA2_FLG = true;
            }
            else
            {
                tmp.SLASH_UPN_JYUTAKUSHA2_FLG = false;
            }

            // 斜線項目運搬の受託者3 
            tmp.SLASH_UPN_JYUTAKUSHA3_FLG = false;

            // 斜線項目運搬先事業場2 
            tmp.SLASH_UPN_SAKI_GENBA2_FLG = false;

            // 斜線項目運搬先事業場3 
            tmp.SLASH_UPN_SAKI_GENBA3_FLG = false;

            // 斜線項目B1票
            if (this.form.cdate_SyougouKakuninB1.Enabled == false)
            {
                tmp.SLASH_B1_FLG = true;
            }
            else
            {
                tmp.SLASH_B1_FLG = false;
            }

            // 斜線項目B2票
            if (this.form.cdate_SyougouKakuninB2.Enabled == false)
            {
                tmp.SLASH_B2_FLG = true;
            }
            else
            {
                tmp.SLASH_B2_FLG = false;
            }

            // 斜線項目B4票 
            tmp.SLASH_B4_FLG = false;

            // 斜線項目B6票 
            tmp.SLASH_B6_FLG = false;

            // 斜線項目D票
            if (this.form.cdate_SyougouKakuninD.Enabled == false)
            {
                tmp.SLASH_D_FLG = true;
            }
            else
            {
                tmp.SLASH_D_FLG = false;
            }

            // 斜線項目E票
            if (this.form.cdate_SyougouKakuninE.Enabled == false)
            {
                tmp.SLASH_E_FLG = true;
            }
            else
            {
                tmp.SLASH_E_FLG = false;
            }

            var WHO = new DataBinderLogic<T_MANIFEST_PRT>(tmp);
            WHO.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp);
        }

        /// <summary>
        /// マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
        /// </summary>
        public void MakeManifestDetailPrt(long lSysId, int iSeq, ref T_MANIFEST_DETAIL_PRT tmp, object obj1, object obj2, ref int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, obj2, ino, cd, name, check);

            CheckBox ck = null;
            TextBox tb = null;
            ck = (CheckBox)obj1;
            tb = (TextBox)obj2;

            //if (ck.Checked == true)
            //if (ck.Checked)
            //{
            // システムID
            tmp.SYSTEM_ID = lSysId;

            // 枝番	
            tmp.SEQ = iSeq;

            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);

            // 廃棄物種類CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.HAIKI_SHURUI_CD = cd.PadLeft(4, '0');
            }

            // 廃棄物種類名
            tmp.HAIKI_SHURUI_NAME = name;

            // 数量
            if (tb.Text != string.Empty)
            {
                tmp.HAIKI_SUURYOU = Convert.ToDecimal(tb.Text);
            }

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_DETAIL_PRT>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, obj2, ino, cd, name, check);
        }

        /// <summary>
        /// マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)データ作成
        /// </summary>
        public void MakeManifestKeijyou(long lSysId, int iSeq, ref T_MANIFEST_KP_KEIJYOU tmp, object obj1, int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
            //CheckBox ck = null;
            //ck = (CheckBox)obj1;
            //if (ck.Checked == true)
            //{
            // システムID
            tmp.SYSTEM_ID = lSysId;
            // 枝番	
            tmp.SEQ = iSeq;
            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);
            // 廃棄物種類CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.KEIJOU_CD = cd.PadLeft(2, '0');
            }
            // 廃棄物種類名
            tmp.KEIJOU_NAME = name;

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_KP_KEIJYOU>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)リストデータ作成
        /// </summary>
        public void MakeManifestKeijyouList(long lSysId, int iSeq, ref  List<T_MANIFEST_KP_KEIJYOU> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            T_MANIFEST_KP_KEIJYOU tmp = null;
            int icnt = 1;

            if (this.form.cckb_KeijyoKokei.Checked == true)
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoKokei, icnt, "1", "固形状", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_KeijyoDoro.Checked == true)
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoDoro, icnt, "2", "泥状", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_KeijyoEkitai.Checked == true)
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoEkitai, icnt, "3", "液体状", true);
                list.Add(tmp);
            }
            icnt++;
            string code = this.form.cantxt_KeijyoFreeCd01.Text;
            if (this.form.cckb_KeijyoFree01.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree01, icnt,
                    this.form.cantxt_KeijyoFreeCd01.Text, this.form.customTextBox116.Text,
                    this.form.cckb_KeijyoFree01.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_KeijyoFreeCd02.Text;
            if (this.form.cckb_KeijyoFree02.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree02, icnt,
                    this.form.cantxt_KeijyoFreeCd02.Text, this.form.customTextBox118.Text,
                    this.form.cckb_KeijyoFree02.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_KeijyoFreeCd03.Text;
            if (this.form.cckb_KeijyoFree03.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree03, icnt,
                    this.form.cantxt_KeijyoFreeCd03.Text, this.form.customTextBox120.Text,
                    this.form.cckb_KeijyoFree03.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_KeijyoFreeCd04.Text;
            if (this.form.cckb_KeijyoFree04.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_KEIJYOU();
                MakeManifestKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree04, icnt,
                    this.form.cantxt_KeijyoFreeCd04.Text, this.form.customTextBox124.Text,
                    this.form.cckb_KeijyoFree04.Checked);
                list.Add(tmp);
            }
            icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
        /// </summary>
        public void MakeManifestNisugata(long lSysId, int iSeq, ref T_MANIFEST_KP_NISUGATA tmp, object obj1, ref int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
            //CheckBox ck = null;
            //ck = (CheckBox)obj1;
            //if (ck.Checked == true)
            //{
            // システムID
            tmp.SYSTEM_ID = lSysId;
            // 枝番	
            tmp.SEQ = iSeq;
            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);
            // 荷姿CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.NISUGATA_CD = cd.PadLeft(2, '0');
            }
            // 荷姿名
            tmp.NISUGATA_NAME = name;

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_KP_NISUGATA>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
        }

        /// <summary>
        /// マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)リストデータ作成
        /// </summary>
        public void MakeManifestNisugataList(long lSysId, int iSeq, ref  List<T_MANIFEST_KP_NISUGATA> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            //マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
            T_MANIFEST_KP_NISUGATA tmp = null;
            int icnt = 1;

            if (this.form.cckb_NisugataBara.Checked == true)
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataBara, ref icnt, "1", "バラ", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_NisugataKontena.Checked == true)
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataKontena, ref icnt, "2", "コンテナ", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_NisugataDoramu.Checked == true)
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataDoramu, ref icnt, "3", "ドラム缶", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_NisugataHukuro.Checked == true)
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataHukuro, ref icnt, "4", "袋", true);
                list.Add(tmp);
            }
            icnt++;
            string code = this.form.cantxt_NisugataFreeCd01.Text;
            if (this.form.cckb_NisugataFree01.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataFree01, ref icnt,
                    this.form.cantxt_NisugataFreeCd01.Text, this.form.cantxt_NisugataFreeName01.Text,
                    this.form.cckb_NisugataFree01.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_NisugataFreeCd02.Text;
            if (this.form.cckb_NisugataFree02.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataFree02, ref icnt,
                    this.form.cantxt_NisugataFreeCd02.Text, this.form.cantxt_NisugataFreeName02.Text,
                    this.form.cckb_NisugataFree02.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_NisugataFreeCd03.Text;
            if (this.form.cckb_NisugataFree03.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_NISUGATA();
                MakeManifestNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataFree03, ref icnt,
                    this.form.cantxt_NisugataFreeCd03.Text, this.form.cantxt_NisugataFreeName03.Text,
                    this.form.cckb_NisugataFree03.Checked);
                list.Add(tmp);
            }
            icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)データ作成
        /// </summary>
        public void MakeManifestHouhou(long lSysId, int iSeq, ref T_MANIFEST_KP_SBN_HOUHOU tmp, object obj1, int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
            //CheckBox ck = null;
            //ck = (CheckBox)obj1;
            //if (ck.Checked == true)
            //{
            // システムID
            tmp.SYSTEM_ID = lSysId;

            // 枝番	
            tmp.SEQ = iSeq;

            // 件数
            tmp.REC_NO = Convert.ToInt16(ino);

            // 処分方法CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.SHOBUN_HOUHOU_CD = cd.PadLeft(3, '0');
            }

            // 処分方法名
            tmp.SHOBUN_HOUHOU_NAME = name;

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_KP_SBN_HOUHOU>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
        }

        /// <summary>
        /// マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)リストデータ作成
        /// </summary>
        public void MakeManifestHouhouList(long lSysId, int iSeq, ref  List<T_MANIFEST_KP_SBN_HOUHOU> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            //マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)データ作成
            T_MANIFEST_KP_SBN_HOUHOU tmp = null;

            //中間処理
            //1項目：1.脱水
            int icnt = 1;
            if (this.form.cckb_UnpanJigyobaDassui.Checked == true)
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaDassui, icnt, "1", "脱水", true);
                list.Add(tmp);
            }

            //2項目：2.焼却
            icnt++;
            if (this.form.cckb_UnpanJigyobaSyoukyaku.Checked == true)
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaSyoukyaku, icnt, "2", "焼却", true);
                list.Add(tmp);
            }

            //3項目：3.破砕
            icnt++;
            if (this.form.cckb_UnpanJigyobaHasai.Checked == true)
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaHasai, icnt, "3", "破砕", true);
                list.Add(tmp);
            }

            //4項目：フリー1
            icnt++;
            string code = this.form.cantxt_UnpanJigyobaFreeCd1.Text;
            if (this.form.cckb_UnpanJigyobaFree1.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree1, icnt,
                this.form.cantxt_UnpanJigyobaFreeCd1.Text, this.form.ctxt_UnpanJigyobaFreeName1.Text,
                this.form.cckb_UnpanJigyobaFree1.Checked);
                list.Add(tmp);
            }

            //5項目：フリー2
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd2.Text;
            if (this.form.cckb_UnpanJigyobaFree2.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree2, icnt,
                this.form.cantxt_UnpanJigyobaFreeCd2.Text, this.form.ctxt_UnpanJigyobaFreeName2.Text,
                this.form.cckb_UnpanJigyobaFree2.Checked);
                list.Add(tmp);
            }

            //6項目：フリー3
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd3.Text;
            if (this.form.cckb_UnpanJigyobaFree3.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree3, icnt,
                this.form.cantxt_UnpanJigyobaFreeCd3.Text, this.form.ctxt_UnpanJigyobaFreeName3.Text,
                this.form.cckb_UnpanJigyobaFree3.Checked);
                list.Add(tmp);
            }

            //7項目：フリー4
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd4.Text;
            if (this.form.cckb_UnpanJigyobaFree4.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree4, icnt,
                this.form.cantxt_UnpanJigyobaFreeCd4.Text, this.form.ctxt_UnpanJigyobaFreeName4.Text,
                this.form.cckb_UnpanJigyobaFree4.Checked);
                list.Add(tmp);
            }

            //8項目：フリー5
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd5.Text;
            if (this.form.cckb_UnpanJigyobaFree5.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree5, icnt,
                this.form.cantxt_UnpanJigyobaFreeCd5.Text, this.form.ctxt_UnpanJigyobaFreeName5.Text,
                this.form.cckb_UnpanJigyobaFree5.Checked);
                list.Add(tmp);
            }

            //最終処分
            //9項目：1.安定型
            icnt = 11;
            if (this.form.cckb_UnpanJigyobaAntei.Checked == true)
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaAntei, icnt, "1", "安定型", true);
                list.Add(tmp);
            }

            //10項目：2.管理型
            icnt = 12;
            if (this.form.cckb_UnpanJigyobaKanri.Checked == true)
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaKanri, icnt, "2", "管理型", true);
                list.Add(tmp);
            }

            //11項目：3.遮断型
            icnt = 13;
            if (this.form.cckb_UnpanJigyobaSyadan.Checked == true)
            {
                tmp = new T_MANIFEST_KP_SBN_HOUHOU();
                MakeManifestHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaSyadan, icnt, "3", "遮断型", true);
                list.Add(tmp);
            }

            //icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニ明細(T_MANIFEST_DETAIL)リストデータ作成
        /// </summary>
        private void MakeManifestDetailList(long lSysId, int iSeq, ref  List<T_MANIFEST_DETAIL> list, bool isPrint)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list, isPrint);
            T_MANIFEST_DETAIL tmp = null;

            //20250402
            for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            {
                if (this.form.cdgrid_Jisseki.Rows[i].IsNewRow)
                {
                    continue;
                }

                if (this.form.cdate_ShobunShuryoDate.Value == null)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value = null;
                }
                else
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value = (DateTime)this.form.cdate_ShobunShuryoDate.Value;
                }
            }

            //2013-11-06 Update ogawamut IT No.78
            //for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            {
                if (this.form.cdgrid_Jisseki.Rows[i].IsNewRow)
                {
                    continue;
                }

                ////廃棄物種類CD 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value != null)
                //{
                tmp = new T_MANIFEST_DETAIL();

                //システムID 
                tmp.SYSTEM_ID = lSysId;

                //枝番 
                tmp.SEQ = iSeq;

                //明細システムID 
                String DSysID = Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells["DetailSystemID"].Value);
                if (string.IsNullOrEmpty(DSysID))
                {
                    // 印刷時はSYSTEM_IDの採番不要(印刷部数ポップアップ側で採番するため)
                    if (isPrint == false)
                    {
                        Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                        tmp.DETAIL_SYSTEM_ID = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                    }
                }
                else
                {
                    tmp.DETAIL_SYSTEM_ID = Int64.Parse(DSysID);
                }

                //廃棄物種類CD 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value != null)
                {
                    tmp.HAIKI_SHURUI_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value.ToString();
                }

                //廃棄物名称CD 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].Value)) == false)
                {
                    tmp.HAIKI_NAME_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].Value.ToString();
                }

                //荷姿CD
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].Value)) == false)
                {
                    tmp.NISUGATA_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].Value.ToString();
                }

                //割合 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value)) == false)
                {
                    tmp.WARIAI = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value);
                }

                //数量
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value)) == false)
                {
                    tmp.HAIKI_SUU = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value);
                }

                //単位CD 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value)) == false)
                {
                    tmp.HAIKI_UNIT_CD = Convert.ToInt16(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value);
                }

                //換算後数量 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)) == false)
                {
                    tmp.KANSAN_SUU = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value);
                }

                //減容後数量 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value != null && maniFlag == 1)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value)))
                {
                }
                else if (maniFlag == 1)
                {
                    tmp.GENNYOU_SUU = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value);
                }

                //処分方法CD
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value)) == false)
                {
                    tmp.SBN_HOUHOU_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value.ToString();
                }

                //処分終了日 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value)) == false)
                {
                    tmp.SBN_END_DATE = Convert.ToDateTime(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value);
                }

                //最終処分終了日
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value)) == false)
                {
                    tmp.LAST_SBN_END_DATE = Convert.ToDateTime(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value.ToString());
                }

                //最終処分業者CD
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value)) == false)
                {
                    tmp.LAST_SBN_GYOUSHA_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString();
                }

                //最終処分現場CD 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value != null)
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value)) == false)
                {
                    tmp.LAST_SBN_GENBA_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value.ToString();
                }

                //2013-10-30 Update ogawamut WHO
                //tmp.CREATE_USER = System.Environment.MachineName;
                //tmp.CREATE_DATE = DateTime.Now;
                //tmp.CREATE_PC = System.Environment.MachineName;
                //tmp.UPDATE_USER = System.Environment.MachineName;
                //tmp.UPDATE_DATE = DateTime.Now;
                //tmp.UPDATE_PC = System.Environment.MachineName;
                var WHO = new DataBinderLogic<T_MANIFEST_DETAIL>(tmp);
                WHO.SetSystemProperty(tmp, false);

                list.Add(tmp);
                //}
            }
            LogUtility.DebugMethodEnd(lSysId, iSeq, list, isPrint);
        }

        /// <summary>
        /// マニ返却日(T_MANIFEST_RET_DATE)データ作成
        /// </summary>
        public void MakeManifestRetDateList(long lSysId, int iSeq, ref  List<T_MANIFEST_RET_DATE> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);
            T_MANIFEST_RET_DATE tmp = null;

            tmp = new T_MANIFEST_RET_DATE();

            //システムID
            tmp.SYSTEM_ID = lSysId;
            //枝番
            tmp.SEQ = iSeq;
            //A票
            if (this.form.cdate_HenkyakuA.Value != null)
            {
                tmp.SEND_A = (DateTime)this.form.cdate_HenkyakuA.Value;
            }
            if (this.form.cdate_HenkyakuB1.Value != null)
            {
                //B1票
                tmp.SEND_B1 = (DateTime)this.form.cdate_HenkyakuB1.Value;
            }
            if (this.form.cdate_HenkyakuB2.Value != null)
            {
                //B2票
                tmp.SEND_B2 = (DateTime)this.form.cdate_HenkyakuB2.Value;
            }

            //C1票
            if (this.form.cdate_HenkyakuC1.Value != null)
            {
                tmp.SEND_C1 = (DateTime)this.form.cdate_HenkyakuC1.Value;

            }
            //C2票 
            if (this.form.cdate_HenkyakuC2.Value != null)
            {
                tmp.SEND_C2 = (DateTime)this.form.cdate_HenkyakuC2.Value;
            }
            //D票 
            if (this.form.cdate_HenkyakuD.Value != null)
            {
                tmp.SEND_D = (DateTime)this.form.cdate_HenkyakuD.Value;
            }
            //E票

            if (this.form.cdate_HenkyakuE.Value != null)
            {
                tmp.SEND_E = (DateTime)this.form.cdate_HenkyakuE.Value;
            }
            //削除フラグ
            tmp.DELETE_FLG = false;

            //2013-10-29 ogawamut
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_RET_DATE>(tmp);
            WHO.SetSystemProperty(tmp, false);

            list.Add(tmp);
            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        #endregion

        #region データ作成処理(マニパターン)
        /// <summary>
        /// データ作成
        /// </summary>
        public void MakePtData(
            ref List<T_MANIFEST_PT_ENTRY> entrylist,
            ref List<T_MANIFEST_PT_UPN> upnlist,
            ref List<T_MANIFEST_PT_PRT> prtlist,
            ref List<T_MANIFEST_PT_DETAIL_PRT> detailprtlist,
            ref List<T_MANIFEST_PT_KP_KEIJYOU> keijyoulist,
            ref List<T_MANIFEST_PT_KP_NISUGATA> niugatalist,
            ref List<T_MANIFEST_PT_KP_SBN_HOUHOU> houhoulist,
            ref List<T_MANIFEST_PT_DETAIL> detaillist,
            ref List<T_MANIFEST_RET_DATE> retdatelist,
            bool delflg,
            String lSysId,
            String iSeq
        )
        {
            LogUtility.DebugMethodStart(entrylist, upnlist, prtlist, prtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist, delflg, lSysId, iSeq);

            T_MANIFEST_PT_ENTRY ManiEntry = new T_MANIFEST_PT_ENTRY();

            //マニフェスト(T_MANIFEST_PT_ENTRY)データ作成
            entrylist = new List<T_MANIFEST_PT_ENTRY>();
            MakeManifestPtEntry(lSysId, iSeq, ref ManiEntry);
            entrylist.Add(ManiEntry);

            //マニパターン収集運搬(T_MANIFEST_PT_UPN)データ作成
            upnlist = new List<T_MANIFEST_PT_UPN>();
            T_MANIFEST_PT_UPN ManiUpn = null;
            ManiUpn = new T_MANIFEST_PT_UPN();
            MakeManifestPtUpn(lSysId, iSeq, 1, ref ManiUpn);
            upnlist.Add(ManiUpn);
            ManiUpn = new T_MANIFEST_PT_UPN();
            MakeManifestPtUpn(lSysId, iSeq, 2, ref ManiUpn);
            upnlist.Add(ManiUpn);

            //マニパターン印字(T_MANIFEST_PT_PRT)データ作成
            prtlist = new List<T_MANIFEST_PT_PRT>();
            T_MANIFEST_PT_PRT ManiPrt = new T_MANIFEST_PT_PRT();
            MakeManifestPtPrt(lSysId, iSeq, ref ManiPrt);
            prtlist.Add(ManiPrt);

            //マニパターン印字明細(T_MANIFEST_PT_DETAIL_PRT)データ作成
            detailprtlist = new List<T_MANIFEST_PT_DETAIL_PRT>();
            MakeManifestPtUpnList(lSysId, iSeq, ref detailprtlist);

            //マニパターン印字_建廃_形状(T_MANIFEST_PT_KP_KEIJYOU)データ作成
            keijyoulist = new List<T_MANIFEST_PT_KP_KEIJYOU>();
            MakeManifestPtKeijyouList(lSysId, iSeq, ref keijyoulist);

            //マニパターン印字_建廃_荷姿(T_MANIFEST_PT_KP_NISUGATA)データ作成
            niugatalist = new List<T_MANIFEST_PT_KP_NISUGATA>();
            MakeManifestPtNisugataList(lSysId, iSeq, ref niugatalist);

            //マニパターン印字_建廃_処分方法(T_MANIFEST_PT_KP_SBN_HOUHOU)データ作成
            houhoulist = new List<T_MANIFEST_PT_KP_SBN_HOUHOU>();
            MakeManifestPtHouhouList(lSysId, iSeq, ref houhoulist);

            //マニパターン明細(T_MANIFEST_DETAIL)データ作成
            detaillist = new List<T_MANIFEST_PT_DETAIL>();
            MakeManifestPtDetailList(lSysId, iSeq, ref detaillist);

            LogUtility.DebugMethodEnd(entrylist, upnlist, prtlist, prtlist, keijyoulist, niugatalist, houhoulist, detaillist, retdatelist, delflg, lSysId, iSeq);
        }

        /// <summary>
        /// マニパターン(T_MANIFEST_PT_ENTRY)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtEntry(long lSysId, int iSeq, ref T_MANIFEST_PT_ENTRY tmp)
        public void MakeManifestPtEntry(String lSysId, String iSeq, ref T_MANIFEST_PT_ENTRY tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp);

            var Search = new CommonSerchParameterDtoCls();
            Search.SYSTEM_ID = lSysId;
            Search.SEQ = iSeq;
            Search.HAIKI_KBN_CD = this.form.HaikiKbnCD;

            //データ読み込み
            //マニ(T_MANIFEST_PT_ENTRY)
            DataTable dt = this.mlogic.SearchPtData(Search);
            if (dt != null && dt.Rows.Count > 0)
            {
                tmp.USE_DEFAULT_KBN = (Boolean)(dt.Rows[0]["USE_DEFAULT_KBN"] == DBNull.Value ? false : dt.Rows[0]["USE_DEFAULT_KBN"]);
            }

            // システムID
            //2013-11-06 Upd ogawamut IT No.135
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.135
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            //2013-10-29 Add ogawamut
            //一括登録区分
            tmp.LIST_REGIST_KBN = false;

            // 廃棄物区分CD
            tmp.HAIKI_KBN_CD = 2;

            // 一次マニフェスト区分 
            //if (parentbaseform.bt_func4.Text == "[F4]\r\n1次マニ")
            //{
            //    tmp.FIRST_MANIFEST_KBN = true;
            //}
            //else if (parentbaseform.bt_func4.Text == "[F4]\r\n2次マニ")
            //{
            //    tmp.FIRST_MANIFEST_KBN = false;
            //}
            switch (maniFlag)
            {
                case 1://１次マニフェスト
                    tmp.FIRST_MANIFEST_KBN = false;
                    break;

                case 2://２次マニフェスト
                    tmp.FIRST_MANIFEST_KBN = true;
                    break;
            }

            // 拠点CD 
            if (this.headerform.ctxt_KyotenCd.Text != string.Empty)
            {
                tmp.KYOTEN_CD = Convert.ToInt16(this.headerform.ctxt_KyotenCd.Text);
            }

            // 取引先CD
            if (this.form.cantxt_TorihikiCd.Text != string.Empty)
            {
                tmp.TORIHIKISAKI_CD = this.form.cantxt_TorihikiCd.Text;
            }

            // 事前協議番号 
            if (this.form.cntxt_jizenkyougi.Text != string.Empty)
            {
                tmp.JIZEN_NUMBER = this.form.cntxt_jizenkyougi.Text;
            }

            // 事前協議年月日
            if (this.form.cdate_JizenKyougi.Value != null)
            {
                tmp.JIZEN_DATE = (DateTime)this.form.cdate_JizenKyougi.Value;
            }

            // 交付年月日
            if (this.form.cdate_KohuDate.Value != null)
            {
                tmp.KOUFU_DATE = (DateTime)this.form.cdate_KohuDate.Value;
            }

            // 交付番号区分（1.通常、2.特殊）
            if (this.form.crdo_KohuTujyo.Checked)
            {
                tmp.KOUFU_KBN = 1;
            }
            else
            {
                tmp.KOUFU_KBN = 2;
            }

            // 交付番号 
            //2013-11-13 Upd ogawamut PT 東北 No.1018
            //tmp.MANIFEST_ID = this.form.cantxt_KohuNo.Text;
            tmp.MANIFEST_ID = "";

            // 整理番号
            //2013-11-08 Upd ogawamut IT No.136
            //tmp.SEIRI_ID = this.form.cntxt_jizenkyougi.Text;
            tmp.SEIRI_ID = this.form.cantxt_SeiriNo.Text;

            // 交付担当者 
            tmp.KOUFU_TANTOUSHA = this.form.ctxt_KohuTantou.Text;

            // 交付担当者所属 
            tmp.KOUFU_TANTOUSHA_SHOZOKU = this.form.ctxt_KohuTantouShozoku.Text;

            // 排出事業者CD 
            tmp.HST_GYOUSHA_CD = this.form.cantxt_HaisyutuGyousyaCd.Text;

            // 排出事業者名称
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GYOUSHA_NAME = this.form.ctxt_HaisyutuGyousyaName1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuGyousyaName2.Text))
            {
                tmp.HST_GYOUSHA_NAME = this.form.ctxt_HaisyutuGyousyaName1.Text.PadRight(40, ' ') + this.form.ctxt_HaisyutuGyousyaName2.Text;
            }
            else
            {
                tmp.HST_GYOUSHA_NAME = this.form.ctxt_HaisyutuGyousyaName1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業者郵便番号 
            tmp.HST_GYOUSHA_POST = this.form.cnt_HaisyutuGyousyaZip.Text;

            // 排出事業者電話番号 
            tmp.HST_GYOUSHA_TEL = this.form.cnt_HaisyutuGyousyaTel.Text;

            // 排出事業者住所  
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GYOUSHA_ADDRESS = this.form.ctxt_HaisyutuGyousyaAdd1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuGyousyaAdd2.Text))
            {
                tmp.HST_GYOUSHA_ADDRESS = this.form.ctxt_HaisyutuGyousyaAdd1.Text.PadRight(48, ' ') + this.form.ctxt_HaisyutuGyousyaAdd2.Text;
            }
            else
            {
                tmp.HST_GYOUSHA_ADDRESS = this.form.ctxt_HaisyutuGyousyaAdd1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業場CD 
            tmp.HST_GENBA_CD = this.form.cantxt_HaisyutuJigyoubaName.Text;

            // 排出事業場名称 
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GENBA_NAME = this.form.ctxt_HaisyutuJigyoubaName1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuJigyoubaName2.Text))
            {
                tmp.HST_GENBA_NAME = this.form.ctxt_HaisyutuJigyoubaName1.Text.PadRight(40, ' ') + this.form.ctxt_HaisyutuJigyoubaName2.Text;
            }
            else
            {
                tmp.HST_GENBA_NAME = this.form.ctxt_HaisyutuJigyoubaName1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業場郵便番号 
            tmp.HST_GENBA_POST = this.form.cnt_HaisyutuJigyoubaZip.Text;

            // 排出事業場電話番号  
            tmp.HST_GENBA_TEL = this.form.cnt_HaisyutuJigyoubaTel.Text;

            // 排出事業場住所 
            // 20140611 katen 不具合No.4469 start‏
            //tmp.HST_GENBA_ADDRESS = this.form.ctxt_HaisyutuJigyoubaAdd1.Text;
            if (!string.IsNullOrEmpty(this.form.ctxt_HaisyutuJigyoubaAdd2.Text))
            {
                tmp.HST_GENBA_ADDRESS = this.form.ctxt_HaisyutuJigyoubaAdd1.Text.PadRight(48, ' ') + this.form.ctxt_HaisyutuJigyoubaAdd2.Text;
            }
            else
            {
                tmp.HST_GENBA_ADDRESS = this.form.ctxt_HaisyutuJigyoubaAdd1.Text;
            }
            // 20140611 katen 不具合No.4469 end‏

            // 備考 
            tmp.BIKOU = this.form.ctxt_UnpanJigyobaTokki.Text;

            // 混合種類
            tmp.KONGOU_SHURUI_CD = this.form.cantxt_KongoCd.Text;

            // 実績数量 
            if (this.form.cntxt_JissekiSuryo.Text != string.Empty)
            {
                tmp.HAIKI_SUU = Convert.ToDecimal(this.form.cntxt_JissekiSuryo.Text);
            }

            // 実績単位CD  
            if (this.form.canTxt_JissekiTaniCd.Text != string.Empty)
            {
                tmp.HAIKI_UNIT_CD = Convert.ToInt16(this.form.canTxt_JissekiTaniCd.Text);
            }

            // 数量の合計
            if (this.form.ctxt_TotalSuryo.Text != string.Empty)
            {
                tmp.TOTAL_SUU = Convert.ToDecimal(this.form.ctxt_TotalSuryo.Text);
            }

            // 換算後数量の合計 
            if (!string.IsNullOrEmpty(this.form.ctxt_KansangoTotalSuryo.Text) && !string.IsNullOrEmpty(this.form.ctxt_KansangoTotalSuryo.Text.Replace(this.unit_name, "")))
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //tmp.TOTAL_KANSAN_SUU = Convert.ToDouble(this.form.ctxt_KansangoTotalSuryo.Text);
                tmp.TOTAL_KANSAN_SUU = Convert.ToDecimal(this.form.ctxt_KansangoTotalSuryo.Text.Replace(this.unit_name, ""));
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            else
            {
                tmp.TOTAL_KANSAN_SUU = 0;
            }

            // 減容後数量の合計 
            if (!string.IsNullOrEmpty(this.form.ctxt_GenyoyugoTotalSuryo.Text) &&
                !string.IsNullOrEmpty(this.form.ctxt_GenyoyugoTotalSuryo.Text.Replace(this.unit_name, "")) && maniFlag == 1)
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //tmp.TOTAL_GENNYOU_SUU = Convert.ToDouble(this.form.ctxt_GenyoyugoTotalSuryo.Text);
                tmp.TOTAL_GENNYOU_SUU = Convert.ToDecimal(this.form.ctxt_GenyoyugoTotalSuryo.Text.Replace(this.unit_name, ""));
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            else
            {
                tmp.TOTAL_GENNYOU_SUU = 0;
            }

            // 中間処理産業廃棄物区分
            if (this.form.ccbx_TyukanTyoubo.Checked == true)
            {
                tmp.CHUUKAN_HAIKI_KBN = 1;
            }
            else if (this.form.ccbx_TyukanKisai.Checked == true)
            {
                tmp.CHUUKAN_HAIKI_KBN = 2;
            }
            else
            {
                tmp.CHUUKAN_HAIKI_KBN = 0;
            }

            // 中間処理産業廃棄物
            tmp.CHUUKAN_HAIKI = this.form.ctxt_TyukanHaikibutu.Text;

            // 最終処分の場所（予定）区分
            if (this.form.ccbx_SaisyuTyoubo.Checked == true)
            {
                tmp.LAST_SBN_YOTEI_KBN = 1;
            }
            else if (this.form.ccbx_SaisyuKisai.Checked == true)
            {
                tmp.LAST_SBN_YOTEI_KBN = 2;
            }
            else
            {
                tmp.LAST_SBN_YOTEI_KBN = 0;
            }

            // 最終処分の場所（予定）業者CD 
            tmp.LAST_SBN_YOTEI_GYOUSHA_CD = this.form.cantxt_SaisyuGyousyaCd.Text;

            // 最終処分の場所（予定）現場CD 
            tmp.LAST_SBN_YOTEI_GENBA_CD = this.form.cantxt_SaisyuGyousyaNameCd.Text;

            // 最終処分の場所（予定）現場名称 
            tmp.LAST_SBN_YOTEI_GENBA_NAME = this.form.cantxt_SaisyuGyousyaName.Text;

            // 最終処分の場所（予定）住所 
            tmp.LAST_SBN_YOTEI_GENBA_ADDRESS = this.form.ctxt_SaisyuGyousyaAdd.Text;

            // 処分受託者CD 
            tmp.SBN_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;

            // 処分受託者名称 
            tmp.SBN_GYOUSHA_NAME = this.form.cantxt_SyobunJyutakuName.Text;

            // 処分受託者郵便番号
            tmp.SBN_GYOUSHA_POST = this.form.cnt_SyobunJyutakuZip.Text;

            // 処分受託者電話番号
            tmp.SBN_GYOUSHA_TEL = this.form.cnt_SyobunJyutakuTel.Text;

            // 処分受託者住所 
            tmp.SBN_GYOUSHA_ADDRESS = this.form.ctxt_SyobunJyutakuAdd.Text;

            // 積替保管業者CD 
            tmp.TMH_GYOUSHA_CD = this.form.cantxt_TumiGyoCd.Text;

            // 積替保管業者名称 
            tmp.TMH_GYOUSHA_NAME = this.form.ctxt_TumiGyoName.Text;

            // 積替保管場CD 
            tmp.TMH_GENBA_CD = this.form.cantxt_TumiHokaNameCd.Text;

            // 積替保管場名称  
            tmp.TMH_GENBA_NAME = this.form.ctxt_TumiHokaName.Text;

            // 積替保管場郵便番号 
            tmp.TMH_GENBA_POST = this.form.cnt_TumiHokaZip.Text;

            // 積替保管場電話番号
            tmp.TMH_GENBA_TEL = this.form.cnt_TumiHokaTel.Text;

            // 積替保管場住所 
            tmp.TMH_GENBA_ADDRESS = this.form.ctxt_TumiHokaAdd.Text;

            // 有価物拾集有無 
            if (this.form.cckb_TumiHokaAri.Checked == true)
            {
                tmp.YUUKA_KBN = 1;
            }
            else
            {
                // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                if (this.form.cckb_TumiHokaNasi.Checked == true)
                {
                    tmp.YUUKA_KBN = 2;
                }
                // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
            }

            // 有価物拾集量
            if (this.form.cntxt_TumiHokaJisseki.Text != string.Empty)
            {
                tmp.YUUKA_SUU = Convert.ToDecimal(this.form.cntxt_TumiHokaJisseki.Text);
            }

            //有価物拾集量単位CD
            if (this.form.cckb_TumiHokaT.Checked == true)
            {
                tmp.YUUKA_UNIT_CD = 1;
            }
            else if (this.form.cckb_TumiHokaM3.Checked == true)
            {
                tmp.YUUKA_UNIT_CD = 2;
            }

            // 処分の受領者CD 
            tmp.SBN_JYURYOUSHA_CD = this.form.cantxt_SyobunJyuCd.Text;

            // 処分の受領者名称 
            tmp.SBN_JYURYOUSHA_NAME = this.form.ctxt_SyobunJyuName.Text;

            // 処分の受領担当者CD 
            tmp.SBN_JYURYOU_TANTOU_CD = this.form.cantxt_SyobunJyuUntenCd.Text;

            // 処分の受領担当者名
            tmp.SBN_JYURYOU_TANTOU_NAME = this.form.cantxt_SyobunJyuUntenName.Text;

            // 処分受領日  
            if (this.form.cdate_SyobunJyu.Value != null)
            {
                tmp.SBN_JYURYOU_DATE = (DateTime)this.form.cdate_SyobunJyu.Value;
            }

            // 処分の受託者CD  
            tmp.SBN_JYUTAKUSHA_CD = this.form.cantxt_SyobunSyoCd.Text;

            // 処分の受託者名称 
            tmp.SBN_JYUTAKUSHA_NAME = this.form.ctxt_SyobunSyoName.Text;

            // 処分担当者CD 
            tmp.SBN_TANTOU_CD = this.form.cantxt_SyobunSyoUntenCd.Text;

            // 処分担当者名 
            tmp.SBN_TANTOU_NAME = this.form.cantxt_SyobunSyoUntenName.Text;

            // 最終処分業者CD 
            tmp.LAST_SBN_GYOUSHA_CD = this.form.cantxt_SaisyuSyobunGyoCd.Text;

            // 最終処分場CD 
            tmp.LAST_SBN_GENBA_CD = this.form.cantxt_SaisyuSyobunbaCD.Text;

            // 最終処分場 名称 
            tmp.LAST_SBN_GENBA_NAME = this.form.ctxt_SaisyuSyobunGyoName.Text;

            // 最終処分場 郵便番号
            tmp.LAST_SBN_GENBA_POST = this.form.ctxt_SaisyuSyobunGyoZip.Text;

            // 最終処分場 住所 
            tmp.LAST_SBN_GENBA_ADDRESS = this.form.ctxt_SaisyuBasyoSyozai.Text;

            // 最終処分場 処分先番号 
            tmp.LAST_SBN_GENBA_NUMBER = this.form.ctxt_SaisyuBasyoNo.Text;

            // 最終処分 確認者 
            tmp.LAST_SBN_CHECK_NAME = this.form.ctxt_SaisyuSyobun.Text;

            // 照合確認 B1票  
            if (this.form.cdate_SyougouKakuninB1.Value != null)
            {
                tmp.CHECK_B1 = (DateTime)this.form.cdate_SyougouKakuninB1.Value;
            }

            // 照合確認 B2票 
            if (this.form.cdate_SyougouKakuninB2.Value != null)
            {
                tmp.CHECK_B2 = (DateTime)this.form.cdate_SyougouKakuninB2.Value;
            }

            // 照合確認 D票 
            if (this.form.cdate_SyougouKakuninD.Value != null)
            {
                tmp.CHECK_D = (DateTime)this.form.cdate_SyougouKakuninD.Value;
            }

            // 照合確認 E票 
            if (this.form.cdate_SyougouKakuninE.Value != null)
            {
                tmp.CHECK_E = (DateTime)this.form.cdate_SyougouKakuninE.Value;
            }

            // 連携伝種区分CD 
            if (this.form.parameters.RenkeiDenshuKbnCd != string.Empty)
            {
                tmp.RENKEI_DENSHU_KBN_CD = Convert.ToInt16(this.form.parameters.RenkeiDenshuKbnCd);
            }

            // 連携システムID 
            if (this.form.parameters.RenkeiSystemId != string.Empty)
            {
                tmp.RENKEI_SYSTEM_ID = Convert.ToInt64(this.form.parameters.RenkeiSystemId);
            }

            // 連携明細システムID 
            if (this.form.parameters.RenkeiMeisaiSystemId != string.Empty)
            {
                tmp.RENKEI_MEISAI_SYSTEM_ID = Convert.ToInt64(this.form.parameters.RenkeiMeisaiSystemId);
            }

            // 連携明細モード
            tmp.RENKEI_MEISAI_MODE = Convert.ToInt16(1);
            if (!string.IsNullOrEmpty(this.form.cantxt_DenshuKbn.Text))
            {
                int iKbn = 0;
                iKbn = Convert.ToInt32(this.form.cantxt_DenshuKbn.Text);

                //受入と計量のみ処理を通す
                if (iKbn.Equals((int)DENSHU_KBN.UKEIRE) || iKbn.Equals((int)DENSHU_KBN.KEIRYOU))
                {
                    if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                    {
                        tmp.RENKEI_MEISAI_MODE = Convert.ToInt16(2);
                    }
                }
            }

            // レイアウト区分
            if (!string.IsNullOrEmpty(manifestMercuryList[1].Text))
            {
                tmp.MANIFEST_MERCURY_CHECK = Convert.ToInt16(manifestMercuryList[1].Text);
            }
            // ﾏﾆ用紙へ印字区分
            bool checkStatus = r_framework.Configuration.AppConfig.AppOptions.IsManiImport();
            if (checkStatus && !string.IsNullOrEmpty(manifestKamiList[1].Text))
            {
                tmp.MANIFEST_KAMI_CHECK = Convert.ToInt16(manifestKamiList[1].Text);
            }
            //2013-10-29 Update ogawamut
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(tmp);
            WHO.SetSystemProperty(tmp, false);

            tmp.DELETE_FLG = false;

            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp);
        }

        /// <summary>
        /// マニパターン収集運搬(T_MANIFEST_PT_UPN)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.134
        //public void MakeManifestPtUpn(long lSysId, int iSeq, int No, ref T_MANIFEST_PT_UPN tmp)
        public void MakeManifestPtUpn(String lSysId, String iSeq, int No, ref T_MANIFEST_PT_UPN tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, No, tmp);

            // システムID
            //2013-11-06 Upd ogawamut IT No.134
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.134
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            tmp.UPN_ROUTE_NO = Convert.ToInt16(No);
            if (No == 1)
            {
                tmp.UPN_GYOUSHA_CD = this.form.cantxt_UnpanJyutaku1NameCd.Text;
                tmp.UPN_GYOUSHA_NAME = this.form.cantxt_UnpanJyutaku1Name.Text;
                tmp.UPN_GYOUSHA_POST = this.form.cnt_UnpanJyutaku1Zip.Text;
                tmp.UPN_GYOUSHA_TEL = this.form.cnt_UnpanJyutaku1Tel.Text;
                tmp.UPN_GYOUSHA_ADDRESS = this.form.ctxt_UnpanJyutaku1Add.Text;
                tmp.UPN_HOUHOU_CD = this.form.cantxt_UnpanHouhou1CD.Text;
                tmp.SHARYOU_CD = this.form.cantxt_Jyutaku1SyaNo.Text;
                tmp.SHARYOU_NAME = this.form.ctxt_Jyutaku1SyaNo.Text;
                tmp.SHASHU_CD = this.form.cantxt_Jyutaku1Syasyu.Text;
                tmp.SHASHU_NAME = this.form.ctxt_Jyutaku1Syasyu.Text;
                if (this.form.cckb_Jyutaku1HokanAri.Checked == true)
                {
                    tmp.TMH_KBN = 1;
                }
                else
                {
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                    if (this.form.cckb_Jyutaku1HokanNasi.Checked == true)
                    {
                        tmp.TMH_KBN = 0;
                    }
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
                }
                tmp.UPN_SAKI_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;
                tmp.UPN_SAKI_GENBA_CD = this.form.cantxt_UnpanJyugyobaNameCd.Text;
                tmp.UPN_SAKI_GENBA_NAME = this.form.cantxt_UnpanJyugyobaName.Text;
                tmp.UPN_SAKI_GENBA_POST = this.form.cnt_UnpanJyugyobaZip.Text;
                tmp.UPN_SAKI_GENBA_TEL = this.form.cnt_UnpanJyugyobaTel.Text;
                tmp.UPN_SAKI_GENBA_ADDRESS = this.form.ctxt_UnpanJyugyobaAdd.Text;
                tmp.UPN_JYUTAKUSHA_CD = this.form.cantxt_UnpanJyuCd1.Text;
                tmp.UPN_JYUTAKUSHA_NAME = this.form.ctxt_UnpanJyuName1.Text;
                tmp.UNTENSHA_CD = this.form.cantxt_UnpanJyuUntenCd1.Text;
                tmp.UNTENSHA_NAME = this.form.cantxt_UnpanJyuUntenName1.Text;
                if (this.form.cdate_UnpanJyu1.Value != null)
                {
                    tmp.UPN_END_DATE = (DateTime)this.form.cdate_UnpanJyu1.Value;
                }
            }
            else
            {
                //if (this.form.cantxt_TumiGyoCd.Enabled == false) //斜線つけてもデータは保持（印字の時だけ消します ）
                //{
                //    return;
                //}
                tmp.UPN_GYOUSHA_CD = this.form.cantxt_UnpanJyutaku2NameCd.Text;
                tmp.UPN_GYOUSHA_NAME = this.form.cantxt_UnpanJyutaku2Name.Text;
                tmp.UPN_GYOUSHA_POST = this.form.cnt_UnpanJyutaku2Zip.Text;
                tmp.UPN_GYOUSHA_TEL = this.form.cnt_UnpanJyutaku2Tel.Text;
                tmp.UPN_GYOUSHA_ADDRESS = this.form.ctxt_UnpanJyutaku2Add.Text;
                tmp.UPN_HOUHOU_CD = this.form.cantxt_UnpanHouhou2CD.Text;
                tmp.SHARYOU_CD = this.form.cantxt_Jyutaku2SyaNo.Text;
                tmp.SHARYOU_NAME = this.form.ctxt_Jyutaku2SyaNo.Text;
                tmp.SHASHU_CD = this.form.cantxt_Jyutaku2Syasyu.Text;
                tmp.SHASHU_NAME = this.form.ctxt_Jyutaku2Syasyu.Text;
                if (this.form.cckb_Jyutaku2HokanAri.Checked == true)
                {
                    tmp.TMH_KBN = 1;
                }
                else
                {
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                    if (this.form.cckb_Jyutaku2HokanNasi.Checked == true)
                    {
                        tmp.TMH_KBN = 0;
                    }
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
                }
                tmp.UPN_SAKI_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;
                tmp.UPN_SAKI_GENBA_CD = this.form.cantxt_UnpanJyugyobaNameCd.Text;
                tmp.UPN_SAKI_GENBA_NAME = this.form.cantxt_UnpanJyugyobaName.Text;
                tmp.UPN_SAKI_GENBA_POST = this.form.cnt_UnpanJyugyobaZip.Text;
                tmp.UPN_SAKI_GENBA_TEL = this.form.cnt_UnpanJyugyobaTel.Text;
                tmp.UPN_SAKI_GENBA_ADDRESS = this.form.ctxt_UnpanJyugyobaAdd.Text;
                tmp.UPN_JYUTAKUSHA_CD = this.form.cantxt_UnpanJyuCd2.Text;
                tmp.UPN_JYUTAKUSHA_NAME = this.form.ctxt_UnpanJyuName2.Text;
                tmp.UNTENSHA_CD = this.form.cantxt_UnpanJyuUntenCd2.Text;
                tmp.UNTENSHA_NAME = this.form.cantxt_UnpanJyuUntenName2.Text;
                if (this.form.cdate_UnpanJyu2.Value != null)
                {
                    tmp.UPN_END_DATE = (DateTime)this.form.cdate_UnpanJyu2.Value;
                }
            }

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_UPN>(tmp);
            WHO.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, No, tmp);
        }

        /// <summary>
        /// マニパターン収集運搬(T_MANIFEST_PT_DETAIL_PRT)リストデータ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtUpnList(long lSysId, int iSeq, ref  List<T_MANIFEST_PT_DETAIL_PRT> list)
        public void MakeManifestPtUpnList(String lSysId, String iSeq, ref  List<T_MANIFEST_PT_DETAIL_PRT> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            //マニパターン印字明細(T_MANIFEST_PT_DETAIL_PRT)データ作成
            T_MANIFEST_PT_DETAIL_PRT tmp = null;

            //安定
            //1行目:01コンクリートがら
            int icnt = 1;
            string suryo = this.form.cntxt_AnteiSuryo01.Text;
            if (this.form.cckb_Antei01.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei01, this.form.cntxt_AnteiSuryo01,
                    ref icnt, "1", "コンクリートがら", this.form.cckb_Antei01.Checked);
                list.Add(tmp);
            }

            //2行目:02アスコンがら
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo02.Text;
            if (this.form.cckb_Antei02.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei02, this.form.cntxt_AnteiSuryo02,
                    ref icnt, "2", "アスコンがら", this.form.cckb_Antei02.Checked);
                list.Add(tmp);
            }

            //3行目:03その他がれき類
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo03.Text;
            if (this.form.cckb_Antei03.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei03, this.form.cntxt_AnteiSuryo03,
                    ref icnt, "3", "その他がれき類", this.form.cckb_Antei03.Checked);
                list.Add(tmp);
            }

            //4行目:04ガラス・陶磁器くず
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo04.Text;
            if (this.form.cckb_Antei04.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei04, this.form.cntxt_AnteiSuryo04,
                    ref icnt, "4", "ガラス・陶磁器くず", this.form.cckb_Antei04.Checked);
                list.Add(tmp);
            }

            //5行目:05廃プラスチック類
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo05.Text;
            if (this.form.cckb_Antei05.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei05, this.form.cntxt_AnteiSuryo05,
                    ref icnt, "5", "廃プラスチック類", this.form.cckb_Antei05.Checked);
                list.Add(tmp);
            }

            //6行目:06金属くず
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo06.Text;
            if (this.form.cckb_Antei06.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei06, this.form.cntxt_AnteiSuryo06,
                    ref icnt, "6", "金属くず", this.form.cckb_Antei06.Checked);
                list.Add(tmp);
            }

            //7行目:07混合（安定型のみ）
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo07.Text;
            if (this.form.cckb_Antei07.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei07, this.form.cntxt_AnteiSuryo07,
                    ref icnt, "7", "混合（安定型のみ）", this.form.cckb_Antei07.Checked);
                list.Add(tmp);
            }

            //8行目:08石綿含有産業廃棄物
            icnt++;
            suryo = this.form.cntxt_AnteiSuryo08.Text;
            if (this.form.cckb_Antei08.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Antei08, this.form.cntxt_AnteiSuryo08,
                    ref icnt, "8", "石綿含有産業廃棄物", this.form.cckb_Antei08.Checked);
                list.Add(tmp);
            }

            //9行目:安定フリー1
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo01.Text;
            string code = this.form.cantxt_AnteiFreeCd01.Text;
            if (this.form.cckb_AnteiFree01.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree01, this.form.cntxt_AnteiFreeSuryo01,
                    ref icnt, this.form.cantxt_AnteiFreeCd01.Text, this.form.ctxt_AnteiFreeName01.Text,
                    this.form.cckb_AnteiFree01.Checked);
                list.Add(tmp);
            }

            //10行目:安定フリー2
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo02.Text;
            code = this.form.cantxt_AnteiFreeCd02.Text;
            if (this.form.cckb_AnteiFree02.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                //MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree02, this.form.cntxt_AnteiSuryo01,
                //    ref icnt, this.form.cantxt_AnteiFreeCd02.Text, this.form.ctxt_AnteiFreeName02.Text);
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree02, this.form.cntxt_AnteiFreeSuryo02,
                    ref icnt, this.form.cantxt_AnteiFreeCd02.Text, this.form.ctxt_AnteiFreeName02.Text,
                    this.form.cckb_AnteiFree02.Checked);
                list.Add(tmp);
            }

            //11行目:安定フリー3
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo03.Text;
            code = this.form.cantxt_AnteiFreeCd03.Text;
            if (this.form.cckb_AnteiFree03.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                //MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree03, this.form.cntxt_AnteiSuryo01,
                //    ref icnt, this.form.cantxt_AnteiFreeCd03.Text, this.form.ctxt_AnteiFreeName03.Text);
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree03, this.form.cntxt_AnteiFreeSuryo03,
                    ref icnt, this.form.cantxt_AnteiFreeCd03.Text, this.form.ctxt_AnteiFreeName03.Text,
                    this.form.cckb_AnteiFree03.Checked);
                list.Add(tmp);
            }

            //12行目:安定フリー4
            icnt++;
            suryo = this.form.cntxt_AnteiFreeSuryo04.Text;
            code = this.form.cantxt_AnteiFreeCd04.Text;
            if (this.form.cckb_AnteiFree04.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                //MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree04, this.form.cntxt_AnteiSuryo01,
                //    ref icnt, this.form.cantxt_AnteiFreeCd04.Text, this.form.ctxt_AnteiFreeNam04.Text);
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_AnteiFree04, this.form.cntxt_AnteiFreeSuryo04,
                    ref icnt, this.form.cantxt_AnteiFreeCd04.Text, this.form.ctxt_AnteiFreeNam04.Text,
                    this.form.cckb_AnteiFree04.Checked);
                list.Add(tmp);
            }

            //管理
            //13行目:11建設汚泥
            icnt++;
            suryo = this.form.cntxt_KanriSuryo11.Text;
            if (this.form.cckb_Kanri01.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri01, this.form.cntxt_KanriSuryo11,
                    ref icnt, "11", "建設汚泥", this.form.cckb_Kanri01.Checked);
                list.Add(tmp);
            }

            //14行目:12紙くず
            icnt++;
            suryo = this.form.cntxt_KanriSuryo12.Text;
            if (this.form.cckb_Kanri02.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri02, this.form.cntxt_KanriSuryo12,
                    ref icnt, "12", "紙くず", this.form.cckb_Kanri02.Checked);
                list.Add(tmp);
            }

            //15行目:13木くず
            icnt++;
            suryo = this.form.cntxt_KanriSuryo13.Text;
            if (this.form.cckb_Kanri03.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri03, this.form.cntxt_KanriSuryo13,
                    ref icnt, "13", "木くず", this.form.cckb_Kanri03.Checked);
                list.Add(tmp);
            }

            //16行目:14繊維くず
            icnt++;
            suryo = this.form.cntxt_KanriSuryo14.Text;
            if (this.form.cckb_Kanri04.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri04, this.form.cntxt_KanriSuryo14,
                    ref icnt, "14", "繊維くず", this.form.cckb_Kanri04.Checked);
                list.Add(tmp);
            }

            //17行目:15廃石膏ボード
            icnt++;
            suryo = this.form.cntxt_KanriSuryo15.Text;
            if (this.form.cckb_Kanri05.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri05, this.form.cntxt_KanriSuryo15,
                    ref icnt, "15", "廃石膏ボード", this.form.cckb_Kanri05.Checked);
                list.Add(tmp);
            }

            //18行目:16混合（管理型含む）
            icnt++;
            suryo = this.form.cntxt_KanriSuryo16.Text;
            if (this.form.cckb_Kanri06.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri06, this.form.cntxt_KanriSuryo16,
                    ref icnt, "16", "混合（管理型含む）", this.form.cckb_Kanri06.Checked);
                list.Add(tmp);
            }

            //19行目:17石綿含有産業廃棄物
            icnt++;
            suryo = this.form.cntxt_KanriSuryo17.Text;
            if (this.form.cckb_Kanri17.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Kanri17, this.form.cntxt_KanriSuryo17,
                    ref icnt, "17", "石綿含有産業廃棄物", this.form.cckb_Kanri17.Checked);
                list.Add(tmp);
            }

            //20行目:管理フリー1
            icnt++;
            suryo = this.form.cntxt_KanriFreeSuryo01.Text;
            code = this.form.cantxt_KanriFreeCd01.Text;
            if (this.form.cckb_KanriFree01.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_KanriFree01, this.form.cntxt_KanriFreeSuryo01,
                    ref icnt, this.form.cantxt_KanriFreeCd01.Text, this.form.ctxt_KanriFreeName01.Text,
                    this.form.cckb_KanriFree01.Checked);
                list.Add(tmp);
            }

            //21行目:管理フリー2
            icnt++;
            suryo = this.form.cntxt_KanriFreeSuryo02.Text;
            code = this.form.cantxt_KanriFreeCd02.Text;
            if (this.form.cckb_KanriFree02.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_KanriFree02, this.form.cntxt_KanriFreeSuryo02,
                    ref icnt, this.form.cantxt_KanriFreeCd02.Text, this.form.ctxt_KanriFreeName02.Text,
                    this.form.cckb_KanriFree02.Checked);
                list.Add(tmp);
            }

            //22行目:管理フリー3
            icnt++;
            suryo = this.form.cntxt_KanriFreeSuryo03.Text;
            code = this.form.cantxt_KanriFreeCd03.Text;
            if (this.form.cckb_KanriFree03.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_KanriFree03, this.form.cntxt_KanriFreeSuryo03,
                    ref icnt, this.form.cantxt_KanriFreeCd03.Text, this.form.ctxt_KanriFreeName03.Text,
                    this.form.cckb_KanriFree03.Checked);
                list.Add(tmp);
            }

            //特別管理産廃
            //23行目:21廃石綿等
            icnt++;
            suryo = this.form.cntxt_TokubetuSuryo21.Text;
            if (this.form.cckb_Tokubetu21.Checked == true || !string.IsNullOrEmpty(suryo))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_Tokubetu21, this.form.cntxt_TokubetuSuryo21,
                    ref icnt, "21", "廃石綿等", this.form.cckb_Tokubetu21.Checked);
                list.Add(tmp);
            }

            //24行目:特別管理産廃フリー1
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo01.Text;
            code = this.form.cantxt_TokubetuFreeCd01.Text;
            if (this.form.cckb_TokubetuFree01.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree01, this.form.cntxt_TokubetuFreeSuryo01,
                    ref icnt, this.form.cantxt_TokubetuFreeCd01.Text, this.form.cantxt_TokubetuFreeName01.Text,
                    this.form.cckb_TokubetuFree01.Checked);
                list.Add(tmp);
            }

            //25行目:特別管理産廃フリー2
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo02.Text;
            code = this.form.cantxt_TokubetuFreeCd02.Text;
            if (this.form.cckb_TokubetuFree02.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree02, this.form.cntxt_TokubetuFreeSuryo02,
                    ref icnt, this.form.cantxt_TokubetuFreeCd02.Text, this.form.cantxt_TokubetuFreeName02.Text,
                    this.form.cckb_TokubetuFree02.Checked);
                list.Add(tmp);
            }

            //26行目:特別管理産廃フリー3
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo03.Text;
            code = this.form.cantxt_TokubetuFreeCd03.Text;
            if (this.form.cckb_TokubetuFree03.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree03, this.form.cntxt_TokubetuFreeSuryo03,
                    ref icnt, this.form.cantxt_TokubetuFreeCd03.Text, this.form.cantxt_TokubetuFreeName03.Text,
                    this.form.cckb_TokubetuFree03.Checked);
                list.Add(tmp);
            }

            //27行目:特別管理産廃フリー4
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo04.Text;
            code = this.form.cantxt_TokubetuFreeCd04.Text;
            if (this.form.cckb_TokubetuFree04.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree04, this.form.cntxt_TokubetuFreeSuryo04,
                    ref icnt, this.form.cantxt_TokubetuFreeCd04.Text, this.form.cantxt_TokubetuFreeName04.Text,
                    this.form.cckb_TokubetuFree04.Checked);
                list.Add(tmp);
            }

            //28行目:特別管理産廃フリー5
            icnt++;
            suryo = this.form.cntxt_TokubetuFreeSuryo05.Text;
            code = this.form.cantxt_TokubetuFreeCd05.Text;
            if (this.form.cckb_TokubetuFree05.Checked == true || !string.IsNullOrEmpty(suryo) || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_DETAIL_PRT();
                MakeManifestPtDetailPrt(lSysId, iSeq, ref tmp, this.form.cckb_TokubetuFree05, this.form.cntxt_TokubetuFreeSuryo05,
                    ref icnt, this.form.cantxt_TokubetuFreeCd05.Text, this.form.cantxt_TokubetuFreeName05.Text,
                    this.form.cckb_TokubetuFree05.Checked);
                list.Add(tmp);
            }

            //icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニパターン印字(T_MANIFEST_PT_PRT)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.134
        //public void MakeManifestPtPrt(long lSysId, int iSeq, ref T_MANIFEST_PT_PRT tmp)
        public void MakeManifestPtPrt(String lSysId, String iSeq, ref T_MANIFEST_PT_PRT tmp)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp);

            // システムID
            //2013-11-06 Upd ogawamut IT No.133
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.133
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            // 印字数量
            if (this.form.cntxt_TotalJyuryo.Text != string.Empty)
            {
                tmp.PRT_SUU = Convert.ToDecimal(this.form.cntxt_TotalJyuryo.Text);
            }

            // 印字単位CD
            if (this.form.cntxt_SanpaiTani.Text != string.Empty)
            {
                tmp.PRT_UNIT_CD = Convert.ToInt16(this.form.cntxt_SanpaiTani.Text);
            }

            // 印字廃棄物名称CD 
            tmp.PRT_HAIKI_NAME_CD = this.form.cantxt_SanpaiSyuruiCd.Text;

            // 印字廃棄物名称 
            tmp.PRT_HAIKI_NAME = this.form.ctxt_SanpaiSyuruiName.Text;

            // 斜線項目備考 
            //if (this.form.ctxt_UnpanJigyobaTokki.Enabled == true)
            if (this.form.lineShape9.Visible)
            {
                //tmp.SLASH_BIKOU_FLG = false;
                tmp.SLASH_BIKOU_FLG = true;
            }
            else
            {
                //tmp.SLASH_BIKOU_FLG = true;
                tmp.SLASH_BIKOU_FLG = false;
            }

            // 斜線項目中間処理産業廃棄物 
            //if (this.form.ccbx_TyukanTyoubo.Enabled == true)
            if (this.form.lineShape5.Visible)
            {
                //tmp.SLASH_CHUUKAN_FLG = false;
                tmp.SLASH_CHUUKAN_FLG = true;
            }
            else
            {
                //tmp.SLASH_CHUUKAN_FLG = true;
                tmp.SLASH_CHUUKAN_FLG = false;
            }

            // 斜線項目積替保管 
            //if (this.form.cantxt_TumiGyoCd.Enabled == true)
            if (this.form.lineShape7.Visible)
            {
                //tmp.SLASH_TSUMIHO_FLG = false;
                tmp.SLASH_TSUMIHO_FLG = true;
            }
            else
            {
                //tmp.SLASH_TSUMIHO_FLG = true;
                tmp.SLASH_TSUMIHO_FLG = false;
            }

            // 斜線項目事前協議 
            //if (this.form.cntxt_jizenkyougi.Enabled == true)
            if (this.form.lineShape6.Visible)
            {
                tmp.SLASH_JIZENKYOUGI_FLG = true;
            }
            else
            {
                tmp.SLASH_JIZENKYOUGI_FLG = false;
            }

            // 斜線項目運搬受託者2
            //if (this.form.cantxt_TumiGyoCd.Enabled == true)
            if (this.form.lineShape8.Visible)
            {
                //tmp.SLASH_UPN_GYOUSHA2_FLG = false;
                tmp.SLASH_UPN_GYOUSHA2_FLG = true;
            }
            else
            {
                //tmp.SLASH_UPN_GYOUSHA2_FLG = true;
                tmp.SLASH_UPN_GYOUSHA2_FLG = false;
            }

            // 斜線項目運搬受託者3 
            tmp.SLASH_UPN_GYOUSHA3_FLG = false;

            // 斜線項目運搬の受託者2 
            //if (this.form.cantxt_TumiGyoCd.Enabled == true)
            if (this.form.lineShape10.Visible)
            {
                //tmp.SLASH_UPN_JYUTAKUSHA2_FLG = false;
                tmp.SLASH_UPN_JYUTAKUSHA2_FLG = true;
            }
            else
            {
                //tmp.SLASH_UPN_JYUTAKUSHA2_FLG = true;
                tmp.SLASH_UPN_JYUTAKUSHA2_FLG = false;
            }

            // 斜線項目運搬の受託者3 
            tmp.SLASH_UPN_JYUTAKUSHA3_FLG = false;

            // 斜線項目運搬先事業場2 
            tmp.SLASH_UPN_SAKI_GENBA2_FLG = false;

            // 斜線項目運搬先事業場3 
            tmp.SLASH_UPN_SAKI_GENBA3_FLG = false;

            // 斜線項目B1票
            //if (this.form.cdate_SyougouKakuninB1.Enabled == true)
            if (this.form.lineShape1.Visible)
            {
                //tmp.SLASH_B1_FLG = false;
                tmp.SLASH_B1_FLG = true;
            }
            else
            {
                //tmp.SLASH_B1_FLG = true;
                tmp.SLASH_B1_FLG = false;
            }

            // 斜線項目B2票 
            //if (this.form.cdate_SyougouKakuninB2.Enabled == true)
            if (this.form.lineShape2.Visible)
            {
                //tmp.SLASH_B2_FLG = false;
                tmp.SLASH_B2_FLG = true;
            }
            else
            {
                //tmp.SLASH_B2_FLG = true;
                tmp.SLASH_B2_FLG = false;
            }

            // 斜線項目B4票 
            tmp.SLASH_B4_FLG = false;

            // 斜線項目B6票 
            tmp.SLASH_B6_FLG = false;

            // 斜線項目D票 
            //if (this.form.cdate_SyougouKakuninD.Enabled == true)
            if (this.form.lineShape3.Visible)
            {
                //tmp.SLASH_D_FLG = false;
                tmp.SLASH_D_FLG = true;
            }
            else
            {
                //tmp.SLASH_D_FLG = true;
                tmp.SLASH_D_FLG = false;
            }

            // 斜線項目E票 
            //if (this.form.cdate_SyougouKakuninE.Enabled == true)
            if (this.form.lineShape4.Visible)
            {
                //tmp.SLASH_E_FLG = false;
                tmp.SLASH_E_FLG = true;
            }
            else
            {
                //tmp.SLASH_E_FLG = true;
                tmp.SLASH_E_FLG = false;
            }

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_PRT>(tmp);
            WHO.SetSystemProperty(tmp, false);

            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp);
        }

        /// <summary>
        /// マニパターン印字明細(T_MANIFEST_PT_DETAIL_PRT)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtDetailPrt(long lSysId, int iSeq, ref T_MANIFEST_PT_DETAIL_PRT tmp, object obj1, object obj2, ref int ino, string cd, string name)
        public void MakeManifestPtDetailPrt(String lSysId, String iSeq, ref T_MANIFEST_PT_DETAIL_PRT tmp, object obj1, object obj2, ref int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, obj2, ino, cd, name, check);

            CheckBox ck = null;
            TextBox tb = null;
            ck = (CheckBox)obj1;
            tb = (TextBox)obj2;

            //if (ck.Checked == true)
            //if (ck.Checked)
            //{
            // システムID
            //2013-11-06 Upd ogawamut IT No.133
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.133
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);

            // 廃棄物種類CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.HAIKI_SHURUI_CD = cd.PadLeft(4, '0');
            }

            // 廃棄物種類名
            tmp.HAIKI_SHURUI_NAME = name;

            // 数量
            if (tb.Text != string.Empty)
            {
                tmp.HAIKI_SUURYOU = Convert.ToDecimal(tb.Text);
            }

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_DETAIL_PRT>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, obj2, ino, cd, name, check);
        }

        /// <summary>
        /// マニパターン印字_建廃_形状(T_MANIFEST_PT_KP_KEIJYOU)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtKeijyou(long lSysId, int iSeq, ref T_MANIFEST_PT_KP_KEIJYOU tmp, object obj1, ref int ino, string cd, string name)
        public void MakeManifestPtKeijyou(String lSysId, String iSeq, ref T_MANIFEST_PT_KP_KEIJYOU tmp, object obj1, ref int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
            //CheckBox ck = null;
            //ck = (CheckBox)obj1;
            //if (ck.Checked == true)
            //{
            // システムID
            //2013-11-06 Upd ogawamut IT No.135
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.135
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);

            // 廃棄物種類CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.KEIJOU_CD = cd.PadLeft(2, '0');
            }

            // 廃棄物種類名
            tmp.KEIJOU_NAME = name;

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_KP_KEIJYOU>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            //ino++;
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
        }

        /// <summary>
        /// マニパターン収集運搬(T_MANIFEST_PT_KP_KEIJYOU)リストデータ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtKeijyouList(long lSysId, int iSeq, ref  List<T_MANIFEST_PT_KP_KEIJYOU> list)
        public void MakeManifestPtKeijyouList(String lSysId, String iSeq, ref  List<T_MANIFEST_PT_KP_KEIJYOU> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            //マニパターン収集運搬(T_MANIFEST_PT_KP_KEIJYOU)データ作成
            T_MANIFEST_PT_KP_KEIJYOU tmp = null;
            int icnt = 1;

            if (this.form.cckb_KeijyoKokei.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoKokei, ref icnt, "1", "固形状", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_KeijyoDoro.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoDoro, ref icnt, "2", "泥状", true);
                list.Add(tmp);
            }
            icnt++;
            if (this.form.cckb_KeijyoEkitai.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoEkitai, ref icnt, "3", "液体状", true);
                list.Add(tmp);
            }
            icnt++;
            string code = this.form.cantxt_KeijyoFreeCd01.Text;
            if (this.form.cckb_KeijyoFree01.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree01, ref icnt,
                    this.form.cantxt_KeijyoFreeCd01.Text, this.form.customTextBox116.Text,
                    this.form.cckb_KeijyoFree01.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_KeijyoFreeCd02.Text;
            if (this.form.cckb_KeijyoFree02.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree02, ref icnt,
                    this.form.cantxt_KeijyoFreeCd02.Text, this.form.customTextBox118.Text,
                    this.form.cckb_KeijyoFree02.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_KeijyoFreeCd03.Text;
            if (this.form.cckb_KeijyoFree03.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree03, ref icnt,
                    this.form.cantxt_KeijyoFreeCd03.Text, this.form.customTextBox120.Text,
                    this.form.cckb_KeijyoFree03.Checked);
                list.Add(tmp);
            }
            icnt++;
            code = this.form.cantxt_KeijyoFreeCd04.Text;
            if (this.form.cckb_KeijyoFree04.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_KEIJYOU();
                MakeManifestPtKeijyou(lSysId, iSeq, ref tmp, this.form.cckb_KeijyoFree04, ref icnt,
                    this.form.cantxt_KeijyoFreeCd04.Text, this.form.customTextBox124.Text,
                    this.form.cckb_KeijyoFree04.Checked);
                list.Add(tmp);
            }
            icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニパターン印字_建廃_荷姿(T_MANIFEST_PT_KP_NISUGATA)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtNisugata(long lSysId, int iSeq, ref T_MANIFEST_PT_KP_NISUGATA tmp, object obj1, ref int ino, string cd, string name)
        public void MakeManifestPtNisugata(String lSysId, String iSeq, ref T_MANIFEST_PT_KP_NISUGATA tmp, object obj1, ref int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
            //CheckBox ck = null;
            //ck = (CheckBox)obj1;
            //if (ck.Checked == true)
            //{
            // システムID
            //2013-11-06 Upd ogawamut IT No.135
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.135
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            // 印字番号
            tmp.REC_NO = Convert.ToInt16(ino);

            // 荷姿CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.NISUGATA_CD = cd.PadLeft(2, '0');
            }

            // 荷姿名
            tmp.NISUGATA_NAME = name;

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_KP_NISUGATA>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
        }

        /// <summary>
        /// マニパターン印字_建廃_荷姿(T_MANIFEST_PT_KP_NISUGATA)リストデータ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtNisugataList(long lSysId, int iSeq, ref  List<T_MANIFEST_PT_KP_NISUGATA> list)
        public void MakeManifestPtNisugataList(String lSysId, String iSeq, ref  List<T_MANIFEST_PT_KP_NISUGATA> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            //マニ印字_建廃_荷姿(T_MANIFEST_PT_KP_NISUGATA)データ作成
            T_MANIFEST_PT_KP_NISUGATA tmp = null;
            int icnt = 1;

            if (this.form.cckb_NisugataBara.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataBara, ref icnt, "1", "バラ", true);
                list.Add(tmp);
            }
            icnt++;

            if (this.form.cckb_NisugataKontena.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataKontena, ref icnt, "2", "コンテナ", true);
                list.Add(tmp);
            }
            icnt++;

            if (this.form.cckb_NisugataDoramu.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataDoramu, ref icnt, "3", "ドラム缶", true);
                list.Add(tmp);
            }
            icnt++;

            if (this.form.cckb_NisugataHukuro.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataHukuro, ref icnt, "4", "袋", true);
                list.Add(tmp);
            }
            icnt++;

            string code = this.form.cantxt_NisugataFreeCd01.Text;
            if (this.form.cckb_NisugataFree01.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataFree01, ref icnt,
                    this.form.cantxt_NisugataFreeCd01.Text, this.form.cantxt_NisugataFreeName01.Text,
                    this.form.cckb_NisugataFree01.Checked);
                list.Add(tmp);
            }
            icnt++;

            code = this.form.cantxt_NisugataFreeCd02.Text;
            if (this.form.cckb_NisugataFree02.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataFree02, ref icnt,
                    this.form.cantxt_NisugataFreeCd02.Text, this.form.cantxt_NisugataFreeName02.Text,
                    this.form.cckb_NisugataFree02.Checked);
                list.Add(tmp);
            }
            icnt++;

            code = this.form.cantxt_NisugataFreeCd03.Text;
            if (this.form.cckb_NisugataFree03.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_NISUGATA();
                MakeManifestPtNisugata(lSysId, iSeq, ref tmp, this.form.cckb_NisugataFree03, ref icnt,
                    this.form.cantxt_NisugataFreeCd03.Text, this.form.cantxt_NisugataFreeName03.Text,
                    this.form.cckb_NisugataFree03.Checked);
                list.Add(tmp);
            }
            icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニパターン印字_建廃_処分方法(T_MANIFEST_PT_KP_SBN_HOUHOU)データ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtHouhou(long lSysId, int iSeq, ref T_MANIFEST_PT_KP_SBN_HOUHOU tmp, object obj1, ref int ino, string cd, string name)
        public void MakeManifestPtHouhou(String lSysId, String iSeq, ref T_MANIFEST_PT_KP_SBN_HOUHOU tmp, object obj1, ref int ino, string cd, string name, bool check)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
            //CheckBox ck = null;
            //ck = (CheckBox)obj1;
            //if (ck.Checked == true)
            //{
            // システムID
            //2013-11-06 Upd ogawamut IT No.133
            //tmp.SYSTEM_ID = lSysId;
            if (lSysId != "")
            {
                tmp.SYSTEM_ID = Int64.Parse(lSysId);
            }

            // 枝番
            //2013-11-06 Upd ogawamut IT No.133
            //tmp.SEQ = iSeq;
            if (iSeq != "")
            {
                tmp.SEQ = Int32.Parse(iSeq);
            }

            // 件数
            tmp.REC_NO = Convert.ToInt16(ino);

            // 処分方法CD
            if (!string.IsNullOrEmpty(cd))
            {
                tmp.SHOBUN_HOUHOU_CD = cd.PadLeft(3, '0');
            }

            // 処分方法名
            tmp.SHOBUN_HOUHOU_NAME = name;

            tmp.PRT_FLG = check;

            //2013-10-30 Update ogawamut WHO
            //tmp.CREATE_USER = System.Environment.MachineName;
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_PC = System.Environment.MachineName;
            //tmp.UPDATE_USER = System.Environment.MachineName;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_PC = System.Environment.MachineName;
            var WHO = new DataBinderLogic<T_MANIFEST_PT_KP_SBN_HOUHOU>(tmp);
            WHO.SetSystemProperty(tmp, false);

            //}
            LogUtility.DebugMethodEnd(lSysId, iSeq, tmp, obj1, ino, cd, name, check);
        }

        /// <summary>
        /// マニパターン印字_建廃_処分方法(T_MANIFEST_PT_KP_SBN_HOUHOU)リストデータ作成
        /// </summary>
        public void MakeManifestPtHouhouList(String lSysId, String iSeq, ref  List<T_MANIFEST_PT_KP_SBN_HOUHOU> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);

            //マニ印字_建廃_処分方法(T_MANIFEST_PT_KP_SBN_HOUHOU)データ作成
            T_MANIFEST_PT_KP_SBN_HOUHOU tmp = null;

            //中間処理
            //1項目：1.脱水
            int icnt = 1;
            if (this.form.cckb_UnpanJigyobaDassui.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaDassui, ref icnt
                    , "1"
                    , "脱水"
                    , true);
                list.Add(tmp);
            }

            //2項目：2.焼却
            icnt++;
            if (this.form.cckb_UnpanJigyobaSyoukyaku.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaSyoukyaku, ref icnt
                    , "2"
                    , "焼却"
                    , true);
                list.Add(tmp);
            }

            //3項目：3.破砕
            icnt++;
            if (this.form.cckb_UnpanJigyobaHasai.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaHasai, ref icnt
                    , "3"
                    , "破砕"
                    , true);
                list.Add(tmp);
            }

            //4項目：フリー1
            icnt++;
            string code = this.form.cantxt_UnpanJigyobaFreeCd1.Text;
            if (this.form.cckb_UnpanJigyobaFree1.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree1, ref icnt
                    , this.form.cantxt_UnpanJigyobaFreeCd1.Text
                    , this.form.ctxt_UnpanJigyobaFreeName1.Text
                    , this.form.cckb_UnpanJigyobaFree1.Checked);
                list.Add(tmp);
            }

            //5項目：フリー2
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd2.Text;
            if (this.form.cckb_UnpanJigyobaFree2.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree2, ref icnt
                    , this.form.cantxt_UnpanJigyobaFreeCd2.Text
                    , this.form.ctxt_UnpanJigyobaFreeName2.Text
                    , this.form.cckb_UnpanJigyobaFree2.Checked);
                list.Add(tmp);
            }

            //6項目：フリー3
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd3.Text;
            if (this.form.cckb_UnpanJigyobaFree3.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree3, ref icnt
                    , this.form.cantxt_UnpanJigyobaFreeCd3.Text
                    , this.form.ctxt_UnpanJigyobaFreeName3.Text
                    , this.form.cckb_UnpanJigyobaFree3.Checked);
                list.Add(tmp);
            }

            //7項目：フリー4
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd4.Text;
            if (this.form.cckb_UnpanJigyobaFree4.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree4, ref icnt
                    , this.form.cantxt_UnpanJigyobaFreeCd4.Text
                    , this.form.ctxt_UnpanJigyobaFreeName4.Text
                    , this.form.cckb_UnpanJigyobaFree4.Checked);
                list.Add(tmp);
            }

            //8項目：フリー5
            icnt++;
            code = this.form.cantxt_UnpanJigyobaFreeCd5.Text;
            if (this.form.cckb_UnpanJigyobaFree5.Checked == true || !string.IsNullOrEmpty(code))
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaFree5, ref icnt
                    , this.form.cantxt_UnpanJigyobaFreeCd5.Text
                    , this.form.ctxt_UnpanJigyobaFreeName5.Text
                    , this.form.cckb_UnpanJigyobaFree5.Checked);
                list.Add(tmp);
            }

            //最終処分
            //9項目：1.安定型
            //icnt++;
            icnt = 11;
            if (this.form.cckb_UnpanJigyobaAntei.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                icnt = 11;
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaAntei, ref icnt
                    , "1"
                    , "安定型"
                    , true);
                list.Add(tmp);
            }

            //10項目：2.管理型
            //icnt++;
            icnt = 12;
            if (this.form.cckb_UnpanJigyobaKanri.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaKanri, ref icnt
                    , "2"
                    , "管理型"
                    , true);
                list.Add(tmp);
            }

            //11項目：3.遮断型
            icnt = 13;
            if (this.form.cckb_UnpanJigyobaSyadan.Checked == true)
            {
                tmp = new T_MANIFEST_PT_KP_SBN_HOUHOU();
                MakeManifestPtHouhou(lSysId, iSeq, ref tmp, this.form.cckb_UnpanJigyobaSyadan, ref icnt
                    , "3"
                    , "遮断型"
                    , true);
                list.Add(tmp);
            }

            //icnt++;

            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        /// <summary>
        /// マニパターン明細(T_MANIFEST_PT_DETAIL)リストデータ作成
        /// </summary>
        //2013-11-06 Upd ogawamut IT No.135
        //public void MakeManifestPtDetailList(long lSysId, int iSeq, ref  List<T_MANIFEST_PT_DETAIL> list)
        public void MakeManifestPtDetailList(String lSysId, String iSeq, ref  List<T_MANIFEST_PT_DETAIL> list)
        {
            LogUtility.DebugMethodStart(lSysId, iSeq, list);
            T_MANIFEST_PT_DETAIL tmp = null;

            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 start
            //int rowCount = (maniFlag == 2) ? 1 : this.form.cdgrid_Jisseki.Rows.Count - 1;
            int rowCount = this.form.cdgrid_Jisseki.Rows.Count - 1;
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 end
            //2013-11-06 Update ogawamut IT No.78
            //for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
            for (int i = 0; i < rowCount; i++)
            {
                ////廃棄物種類CD 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value != null)
                //{
                tmp = new T_MANIFEST_PT_DETAIL();

                // システムID
                //2013-11-06 Upd ogawamut IT No.133
                //tmp.SYSTEM_ID = lSysId;
                if (lSysId != "")
                {
                    tmp.SYSTEM_ID = Int64.Parse(lSysId);
                }

                // 枝番
                //2013-11-06 Upd ogawamut IT No.133
                //tmp.SEQ = iSeq;
                if (iSeq != "")
                {
                    tmp.SEQ = Int32.Parse(iSeq);
                }

                //明細システムID 
                //Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                //tmp.DETAIL_SYSTEM_ID = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);

                //明細システムID 
                //2013-11-06 Upd ogawamut IT No.133
                //Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                //tmp.DETAIL_SYSTEM_ID = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                //if (String.IsNullOrEmpty(this.form.cdgrid_Jisseki.Rows[i].Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells["DetailSystemID"].Value))) //2013.11.22 naitou update 
                {
                }
                else
                {
                    //tmp.DETAIL_SYSTEM_ID = Int64.Parse(this.form.cdgrid_Jisseki.Rows[i].Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                    tmp.DETAIL_SYSTEM_ID = Int64.Parse(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells["DetailSystemID"].Value)); //2013.11.22 naitou update 
                }

                //廃棄物種類CD 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value != null)
                {
                    tmp.HAIKI_SHURUI_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value.ToString();
                }

                //廃棄物名称CD 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].Value != null)
                {
                    tmp.HAIKI_NAME_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].Value.ToString();
                }

                //荷姿CD
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].Value != null)
                {
                    tmp.NISUGATA_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].Value.ToString();
                }

                //割合 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value != null)
                {
                    tmp.WARIAI = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value);
                }

                //数量
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value != null)
                {
                    //2014-03-17 Upd ogawamut No.3543
                    //tmp.HAIKI_SUU = Convert.ToDouble(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value);
                    if (String.IsNullOrEmpty(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value.ToString()))
                    {
                        tmp.HAIKI_SUU = 0;
                    }
                    else
                    {
                        tmp.HAIKI_SUU = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value);
                    }
                }

                //単位CD 
                //if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value != null)
                if (!string.IsNullOrWhiteSpace(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells["TaniCd"].Value)))
                {
                    tmp.HAIKI_UNIT_CD = Convert.ToInt16(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value);
                }

                //換算後数量 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value != null)
                {
                    //2014-03-17 Upd ogawamut No.3543
                    //tmp.KANSAN_SUU = Convert.ToDouble(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value);
                    if (String.IsNullOrEmpty(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value.ToString()))
                    {
                        tmp.KANSAN_SUU = 0;
                    }
                    else
                    {
                        tmp.KANSAN_SUU = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value);
                    }
                }

                //減容後数量 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value != null && maniFlag == 1)
                {
                    //2014-03-17 Upd ogawamut No.3543
                    //tmp.GENNYOU_SUU = Convert.ToDouble(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value);
                    if (String.IsNullOrEmpty(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value.ToString()))
                    {
                        tmp.GENNYOU_SUU = 0;
                    }
                    else
                    {
                        tmp.GENNYOU_SUU = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value);
                    }
                }

                //処分方法CD
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value != null)
                {
                    tmp.SBN_HOUHOU_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value.ToString();
                }

                //処分終了日 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value != null)
                {
                    tmp.SBN_END_DATE = Convert.ToDateTime(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value);
                }

                //最終処分終了日
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value != null)
                {
                    tmp.LAST_SBN_END_DATE = Convert.ToDateTime(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value.ToString());
                }

                //最終処分業者CD
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value != null)
                {
                    tmp.LAST_SBN_GYOUSHA_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString();
                }

                //最終処分現場CD 
                if (this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value != null)
                {
                    tmp.LAST_SBN_GENBA_CD = this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value.ToString();
                }

                var WHO = new DataBinderLogic<T_MANIFEST_PT_DETAIL>(tmp);
                WHO.SetSystemProperty(tmp, false);

                list.Add(tmp);
                //}
            }
            LogUtility.DebugMethodEnd(lSysId, iSeq, list);
        }

        #endregion

        #region コントロール値設定処理

        /// <summary>
        /// データ設定
        /// </summary>
        /// pt=0:通常,pt=1:パターン
        public void SetData(DataTable dt, byte pt = 0)
        {
            LogUtility.DebugMethodStart(dt, pt);

            // マニフェスト(T_MANIFEST_ENTRY)データ設定
            SetManifestEntry(dt.Rows[0], pt);

            // マニフェスト運搬(T_MANIFEST_UPN)データ設定
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SetManifestUpn(dt.Rows[i], dt.Rows[i]["UPN_ROUTE_NO"].ToString());
            }

            // マニ印字(T_MANIFEST_PRT)データ設定
            SetManifestPrt(dt.Rows[0]);

            // マニ返却日(T_MANIFEST_RET_DATE)データ設定
            SetManifestRetDateList(dt.Rows[0], pt);

            SetHenkyakuhiNyuuryokuEnabled();

            LogUtility.DebugMethodEnd(dt, pt);
        }

        /// <summary>
        /// データ設定
        /// </summary>
        public Boolean SetAllData()
        {
            LogUtility.DebugMethodStart();

            switch (this.form.parameters.Mode)
            {
                case (int)WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                case (int)WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                case (int)WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                    //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
                    var Search = new CommonSerchParameterDtoCls();
                    Search.SYSTEM_ID = this.form.parameters.SystemId;
                    Search.HAIKI_KBN_CD = this.form.HaikiKbnCD;

                    DataTable dt = this.mlogic.SearchData(Search);
                    if (dt.Rows.Count == 0)
                    {
                        return true;
                    }
                    this.SetData(dt);

                    this.form.parameters.Seq = dt.Rows[0]["SEQ"].ToString();
                    this.form.parameters.SeqRD = dt.Rows[0]["TMRD_SEQ"].ToString();
                    this.form.parameters.ManifestID = dt.Rows[0]["MANIFEST_ID"].ToString();
                    this.form.parameters.Save();

                    GetSysIdSeqDtoCls SearchString = new GetSysIdSeqDtoCls();
                    SearchString.SYSTEM_ID = this.form.parameters.SystemId;
                    SearchString.SEQ = this.form.parameters.Seq;
                    Search.SEQ = this.form.parameters.Seq;

                    //マニ印字明細
                    DataTable dtDetailPrt = this.mlogic.SearchDetailPrtData(Search);
                    if (dtDetailPrt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDetailPrt.Rows.Count; i++)
                        {
                            this.SetManifestDetailPrt(dtDetailPrt.Rows[i]);
                        }
                    }

                    //マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)
                    DataTable dtKpKeijyou = this.KeijyouDao.GetDataForEntity(SearchString);
                    if (dtKpKeijyou.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtKpKeijyou.Rows.Count; i++)
                        {
                            this.SetManifestKpKeijyou(dtKpKeijyou.Rows[i]);
                        }
                    }

                    //マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)
                    DataTable dtKpNisugata = this.NisugataDao.GetDataForEntity(SearchString);
                    if (dtKpNisugata.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtKpNisugata.Rows.Count; i++)
                        {
                            this.SetManifestKpNisugata(dtKpNisugata.Rows[i]);
                        }
                    }

                    //マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)
                    DataTable dtKpHouhou = this.HouhouDao.GetDataForEntity(SearchString);
                    if (dtKpHouhou.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtKpHouhou.Rows.Count; i++)
                        {
                            this.SetManifestKpHouhou(dtKpHouhou.Rows[i]);
                        }
                    }

                    //マニ明細(T_MANIFEST_DETAIL)
                    DataTable dtDetail = this.mlogic.SearchDetailData(Search);
                    if (dtDetail.Rows.Count > 0)
                    {
                        this.SetManifestDetail(dtDetail);
                        this.SetMaxSyobunEndDate();
                        this.SetMaxSaisyuSyobunEndDate();
                        // 紐付済みの場合、運搬終了日が変更されると紐付情報が解除される。
                        this.ChengeReadOnlyForUpnEndDate(dtDetail);
                    }
                    break;

                case (int)WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
            return false;
        }

        // 20140601 katen 不具合No.4133 start‏
        /// <summary>
        /// 前と次のデータ設定
        /// </summary>
        public Boolean SetAllDataForPreviousOrNext(string kbn, out bool catchErr)
        {
            catchErr = false;
            DataTable dt = new DataTable();
            try
            {
                LogUtility.DebugMethodStart(kbn);

                //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
                var SearchForPreviousOrNext = new SerchParameterDtoCls();
                SearchForPreviousOrNext.SYSTEM_ID = this.form.parameters.SystemId;
                SearchForPreviousOrNext.HAIKI_KBN_CD = this.form.HaikiKbnCD;
                SearchForPreviousOrNext.KYOTEN = this.headerform.ctxt_KyotenCd.Text;
                switch (kbn)
                {
                    case "SearchPreviousData":
                        dt = mopdao.GetDataForPrevious(SearchForPreviousOrNext);
                        break;
                    case "SearchNextData":
                        dt = mopdao.GetDataForNext(SearchForPreviousOrNext);
                        break;
                }
                if (dt.Rows.Count > 0)
                {
                    this.form.parameters.SystemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAllDataForPreviousOrNext", ex);
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
                LogUtility.DebugMethodEnd(kbn, catchErr);
            }
            return dt.Rows.Count <= 0;
        }
        // 20140601 katen 不具合No.4133 end‏

        /// <summary>
        /// データ設定
        /// </summary>
        public void SetAllPtData()
        {
            LogUtility.DebugMethodStart();

            var Search = new CommonSerchParameterDtoCls();
            Search.SYSTEM_ID = this.form.parameters.PtSystemId;
            Search.SEQ = this.form.parameters.PtSeq;
            Search.HAIKI_KBN_CD = this.form.HaikiKbnCD;

            //データ読み込み
            //マニ(T_MANIFEST_PT_ENTRY)
            DataTable dt = this.mlogic.SearchPtData(Search);
            if (dt.Rows.Count > 0)
            {
                this.SetData(dt, 1);
            }

            var SearchString = new GetSysIdSeqDtoCls();
            //システムID
            SearchString.SYSTEM_ID = this.form.parameters.PtSystemId;
            //枝番
            SearchString.SEQ = this.form.parameters.PtSeq;

            //データ読み込み
            //マニフェストパターン印字明細(T_MANIFEST_PT_DETAIL_PRT)
            DataTable dtDetailPrt = this.mlogic.SearchPtDetailPrtData(Search);
            if (dtDetailPrt.Rows.Count > 0)
            {
                for (int i = 0; i < dtDetailPrt.Rows.Count; i++)
                {
                    this.SetManifestDetailPrt(dtDetailPrt.Rows[i]);
                }
            }

            //データ読み込み
            //マニ印字_建廃_形状(T_MANIFEST_PT_KP_KEIJYOU)
            DataTable dtKpKeijyou = this.PtKeijyouDao.GetDataForEntity(SearchString);
            if (dtKpKeijyou.Rows.Count > 0)
            {
                for (int i = 0; i < dtKpKeijyou.Rows.Count; i++)
                {
                    this.SetManifestKpKeijyou(dtKpKeijyou.Rows[i]);
                }
            }

            //データ読み込み
            //マニ印字_建廃_荷姿(T_MANIFEST_PT_KP_NISUGATA)
            DataTable dtKpNisugata = this.PtNisugataDao.GetDataForEntity(SearchString);
            if (dtKpNisugata.Rows.Count > 0)
            {
                for (int i = 0; i < dtKpNisugata.Rows.Count; i++)
                {
                    this.SetManifestKpNisugata(dtKpNisugata.Rows[i]);
                }
            }

            //データ読み込み
            //マニ印字_建廃_処分方法(T_MANIFEST_PT_KP_SBN_HOUHOU)
            DataTable dtKpHouhou = this.PtHouhouDao.GetDataForEntity(SearchString);
            if (dtKpHouhou.Rows.Count > 0)
            {
                for (int i = 0; i < dtKpHouhou.Rows.Count; i++)
                {
                    this.SetManifestKpHouhou(dtKpHouhou.Rows[i]);
                }
            }

            //データ読み込み
            //マニ明細(T_MANIFEST_PT_DETAIL)
            DataTable dtDetail = this.mlogic.SearchPtDetailData(Search);
            if (dtDetail.Rows.Count > 0)
            {
                this.SetManifestDetail(dtDetail);
                this.SetMaxSyobunEndDate();
                this.SetMaxSaisyuSyobunEndDate();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニフェスト(T_MANIFEST_ENTRY)データ設定
        /// </summary>
        public void SetManifestEntry(DataRow dr, byte pt = 0)
        {
            LogUtility.DebugMethodStart(dr, pt);

            // マニフェスト区分 
            if (dr["FIRST_MANIFEST_KBN"].ToString() == "True")
            {
                parentbaseform.bt_func4.Text = "[F4]\r\n1次マニ";
                maniFlag = 2;
            }
            else
            {
                parentbaseform.bt_func4.Text = "[F4]\r\n2次マニ";
                maniFlag = 1;
            }

            if (AppConfig.IsManiLite)
            {
                // マニライトの場合、二次マニは無し
                parentbaseform.bt_func4.Text = string.Empty;
            }

            // 拠点CD 
            if (dr["KYOTEN_CD"].ToString() != string.Empty)
            {
                this.headerform.ctxt_KyotenCd.Text = dr["KYOTEN_CD"].ToString().PadLeft(2, '0'); //拠点のみゼロ埋め考慮必要
                this.headerform.ctxt_KyotenMei.Text = dr["KYOTEN_NAME"].ToString();
            }

            // 取引先CD
            if (dr["TORIHIKISAKI_CD"].ToString() != string.Empty)
            {
                this.form.cantxt_TorihikiCd.Text = dr["TORIHIKISAKI_CD"].ToString();
                this.form.ctxt_TorihikiName.Text = dr["TORIHIKISAKI_NAME"].ToString();
            }

            // 事前協議番号 
            if (dr["JIZEN_NUMBER"].ToString() != string.Empty)
            {
                this.form.cntxt_jizenkyougi.Text = dr["JIZEN_NUMBER"].ToString();
            }

            // 事前協議年月日
            if (dr["JIZEN_DATE"].ToString() != string.Empty)
            {
                this.form.cdate_JizenKyougi.Value = Convert.ToDateTime(dr["JIZEN_DATE"].ToString());
            }

            // 交付年月日
            if (dr["KOUFU_DATE"].ToString() != string.Empty)
            {
                this.form.cdate_KohuDate.Value = Convert.ToDateTime(dr["KOUFU_DATE"].ToString());
            }

            // 交付番号 
            switch (pt)
            {
                case 0://通常データの表示
                    this.form.cantxt_KohuNo.Text = dr["MANIFEST_ID"].ToString();
                    this.form.bak_ManifestId = this.form.cantxt_KohuNo.Text;
                    this.form.bak_ManifestKbn = dr["KOUFU_KBN"].ToString();
                    break;

                //case 1://パターンデータの表示
                //    this.form.cantxt_KohuNo.Text = "";
                //    break;
            }

            // 交付番号区分
            if (dr["KOUFU_KBN"].ToString() == "1")
            {
                this.form.crdo_KohuTujyo.Checked = true;
                this.form.crdo_KohuReigai.Checked = false;
            }
            else
            {
                this.form.crdo_KohuTujyo.Checked = false;
                this.form.crdo_KohuReigai.Checked = true;
            }

            // 整理番号 
            this.form.cantxt_SeiriNo.Text = dr["SEIRI_ID"].ToString();

            // 交付担当者 
            this.form.ctxt_KohuTantou.Text = dr["KOUFU_TANTOUSHA"].ToString();

            // 交付担当者所属 
            this.form.ctxt_KohuTantouShozoku.Text = dr["KOUFU_TANTOUSHA_SHOZOKU"].ToString();

            // 排出事業者CD 
            this.form.cantxt_HaisyutuGyousyaCd.Text = dr["HST_GYOUSHA_CD"].ToString();

            // 排出事業者名称
            // 20140611 katen 不具合No.4469 start‏
            //this.form.ctxt_HaisyutuGyousyaName1.Text = dr["HST_GYOUSHA_NAME"].ToString();
            if (dr["HST_GYOUSHA_NAME"].ToString().Length > 40)
            {
                this.form.ctxt_HaisyutuGyousyaName1.Text = dr["HST_GYOUSHA_NAME"].ToString().Substring(0, 40).TrimEnd(' ');
                this.form.ctxt_HaisyutuGyousyaName2.Text = dr["HST_GYOUSHA_NAME"].ToString().Substring(40);
            }
            else
            {
                this.form.ctxt_HaisyutuGyousyaName1.Text = dr["HST_GYOUSHA_NAME"].ToString().TrimEnd(' ');
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業者郵便番号 
            this.form.cnt_HaisyutuGyousyaZip.Text = dr["HST_GYOUSHA_POST"].ToString();

            // 排出事業者電話番号 
            this.form.cnt_HaisyutuGyousyaTel.Text = dr["HST_GYOUSHA_TEL"].ToString();

            // 排出事業者住所  
            // 20140611 katen 不具合No.4469 start‏
            //this.form.ctxt_HaisyutuGyousyaAdd1.Text = dr["HST_GYOUSHA_ADDRESS"].ToString();
            if (dr["HST_GYOUSHA_ADDRESS"].ToString().Length > 48)
            {
                this.form.ctxt_HaisyutuGyousyaAdd1.Text = dr["HST_GYOUSHA_ADDRESS"].ToString().Substring(0, 48).TrimEnd(' ');
                this.form.ctxt_HaisyutuGyousyaAdd2.Text = dr["HST_GYOUSHA_ADDRESS"].ToString().Substring(48);
            }
            else
            {
                this.form.ctxt_HaisyutuGyousyaAdd1.Text = dr["HST_GYOUSHA_ADDRESS"].ToString().TrimEnd(' ');
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業場CD 
            this.form.cantxt_HaisyutuJigyoubaName.Text = dr["HST_GENBA_CD"].ToString();

            // 排出事業場名称 
            // 20140611 katen 不具合No.4469 start‏
            //this.form.ctxt_HaisyutuJigyoubaName1.Text = dr["HST_GENBA_NAME"].ToString();
            if (dr["HST_GENBA_NAME"].ToString().Length > 40)
            {
                this.form.ctxt_HaisyutuJigyoubaName1.Text = dr["HST_GENBA_NAME"].ToString().Substring(0, 40).TrimEnd(' ');
                this.form.ctxt_HaisyutuJigyoubaName2.Text = dr["HST_GENBA_NAME"].ToString().Substring(40);
            }
            else
            {
                this.form.ctxt_HaisyutuJigyoubaName1.Text = dr["HST_GENBA_NAME"].ToString().TrimEnd(' ');
            }
            // 20140611 katen 不具合No.4469 end‏

            // 排出事業場郵便番号 
            this.form.cnt_HaisyutuJigyoubaZip.Text = dr["HST_GENBA_POST"].ToString();

            // 排出事業場電話番号  
            this.form.cnt_HaisyutuJigyoubaTel.Text = dr["HST_GENBA_TEL"].ToString();

            // 排出事業場住所 
            // 20140611 katen 不具合No.4469 start‏
            //this.form.ctxt_HaisyutuJigyoubaAdd1.Text = dr["HST_GENBA_ADDRESS"].ToString();
            if (dr["HST_GENBA_ADDRESS"].ToString().Length > 48)
            {
                this.form.ctxt_HaisyutuJigyoubaAdd1.Text = dr["HST_GENBA_ADDRESS"].ToString().Substring(0, 48).TrimEnd(' ');
                this.form.ctxt_HaisyutuJigyoubaAdd2.Text = dr["HST_GENBA_ADDRESS"].ToString().Substring(48);
            }
            else
            {
                this.form.ctxt_HaisyutuJigyoubaAdd1.Text = dr["HST_GENBA_ADDRESS"].ToString().TrimEnd(' ');
            }
            // 20140611 katen 不具合No.4469 end‏

            // 備考 
            this.form.ctxt_UnpanJigyobaTokki.Text = dr["BIKOU"].ToString();

            // 混合種類
            this.form.cantxt_KongoCd.Text = dr["KONGOU_SHURUI_CD"].ToString();

            // 混合種類名称
            this.form.ctxt_KongoName.Text = dr["KONGOU_SHURUI_NAME"].ToString();

            // 実績数量 
            if (dr["HAIKI_SUU"].ToString() != string.Empty)
            {
                this.form.cntxt_JissekiSuryo.Text = dr["HAIKI_SUU"].ToString();
            }

            // 実績単位CD  
            if (dr["HAIKI_UNIT_CD"].ToString() != string.Empty)
            {
                int Temp = int.Parse(dr["HAIKI_UNIT_CD"].ToString());
                this.form.canTxt_JissekiTaniCd.Text = Temp.ToString("00");
            }

            //20250402
            //運搬終了日1
            if (dr["UNPAN_DATE"].ToString() != string.Empty)
            {
                this.form.cdate_UnpanDate_1.Value = Convert.ToDateTime(dr["UNPAN_DATE"].ToString());
            }

            //運搬終了日2
            if (dr["UNPAN_DATE_2"].ToString() != string.Empty)
            {
                this.form.cdate_UnpanDate_2.Value = Convert.ToDateTime(dr["UNPAN_DATE_2"].ToString());
            }

            //処分受領日
            if (dr["SHOBUN_JURYO_DATE"].ToString() != string.Empty)
            {
                this.form.cdate_ShobunJuryoDate.Value = Convert.ToDateTime(dr["SHOBUN_JURYO_DATE"].ToString());
            }

            //処分終了日
            if (dr["SHOBUN_SHURYO_DATE"].ToString() != string.Empty)
            {
                this.form.cdate_ShobunShuryoDate.Value = Convert.ToDateTime(dr["SHOBUN_SHURYO_DATE"].ToString());
            }

            // 実績単位名
            if (dr["HAIKI_UNIT_NAME"].ToString() != string.Empty)
            {
                this.form.ctxt_JissekiTaniName.Text = dr["HAIKI_UNIT_NAME"].ToString();
            }

            // 数量の合計
            if (dr["TOTAL_SUU"].ToString() != string.Empty)
            {
                this.form.ctxt_TotalSuryo.Text
                    = mlogic.GetSuuryoRound(Convert.ToDecimal(dr["TOTAL_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
            }
            else
            {
                this.form.ctxt_TotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat);
            }

            // 換算後数量の合計 
            if (dr["TOTAL_KANSAN_SUU"].ToString() != string.Empty)
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //this.form.ctxt_KansangoTotalSuryo.Text
                //= mlogic.GetSuuryoRound(Convert.ToDouble(dr["TOTAL_KANSAN_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_KansangoTotalSuryo.Text
                = mlogic.GetSuuryoRound(Convert.ToDecimal(dr["TOTAL_KANSAN_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat) + this.unit_name;
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            else
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //this.form.ctxt_KansangoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_KansangoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat) + this.unit_name;
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }

            // 減容後数量の合計 
            if (dr["TOTAL_GENNYOU_SUU"].ToString() != string.Empty)
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //this.form.ctxt_GenyoyugoTotalSuryo.Text
                //= mlogic.GetSuuryoRound(Convert.ToDouble(dr["TOTAL_GENNYOU_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_GenyoyugoTotalSuryo.Text
                    = mlogic.GetSuuryoRound(Convert.ToDecimal(dr["TOTAL_GENNYOU_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat) + this.unit_name;
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }
            else
            {
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する start
                //this.form.ctxt_GenyoyugoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat);
                this.form.ctxt_GenyoyugoTotalSuryo.Text = (0).ToString(this.ManifestSuuryoFormat) + this.unit_name;
                // 20140603 ria EV003952 マニの基本単位を換算後数量の合計と減容後数量の合計に表示する end
            }

            // 中間処理産業廃棄物区分
            this.form.ccbx_TyukanTyoubo.Checked = false;
            this.form.ccbx_TyukanKisai.Checked = false;
            if (dr["CHUUKAN_HAIKI_KBN"].ToString() == "1")
            {
                this.form.ccbx_TyukanTyoubo.Checked = true;
            }
            else if (dr["CHUUKAN_HAIKI_KBN"].ToString() == "2")
            {
                this.form.ccbx_TyukanKisai.Checked = true;
            }

            // 中間処理産業廃棄物
            this.form.ctxt_TyukanHaikibutu.Text = dr["CHUUKAN_HAIKI"].ToString();

            // 最終処分の場所（予定）区分
            this.form.ccbx_SaisyuTyoubo.Checked = false;
            this.form.ccbx_SaisyuKisai.Checked = false;
            if (dr["LAST_SBN_YOTEI_KBN"].ToString() == "1")
            {
                this.form.ccbx_SaisyuTyoubo.Checked = true;
            }
            else if (dr["LAST_SBN_YOTEI_KBN"].ToString() == "2")
            {
                this.form.ccbx_SaisyuKisai.Checked = true;
            }

            // 最終処分の場所（予定）業者CD 
            this.form.cantxt_SaisyuGyousyaCd.Text = dr["LAST_SBN_YOTEI_GYOUSHA_CD"].ToString();

            // 最終処分の場所（予定）現場CD 
            this.form.cantxt_SaisyuGyousyaNameCd.Text = dr["LAST_SBN_YOTEI_GENBA_CD"].ToString();

            // 最終処分の場所（予定）現場名称 
            this.form.cantxt_SaisyuGyousyaName.Text = dr["LAST_SBN_YOTEI_GENBA_NAME"].ToString();

            // 最終処分の場所（予定）住所 
            this.form.ctxt_SaisyuGyousyaAdd.Text = dr["LAST_SBN_YOTEI_GENBA_ADDRESS"].ToString();

            // 処分受託者CD 
            this.form.cantxt_SyobunJyutakuNameCd.Text = dr["SBN_GYOUSHA_CD"].ToString();

            // 処分受託者名称 
            this.form.cantxt_SyobunJyutakuName.Text = dr["SBN_GYOUSHA_NAME"].ToString();

            // 処分受託者郵便番号
            this.form.cnt_SyobunJyutakuZip.Text = dr["SBN_GYOUSHA_POST"].ToString();

            // 処分受託者電話番号
            this.form.cnt_SyobunJyutakuTel.Text = dr["SBN_GYOUSHA_TEL"].ToString();

            // 処分受託者住所 
            this.form.ctxt_SyobunJyutakuAdd.Text = dr["SBN_GYOUSHA_ADDRESS"].ToString();

            // 積替保管業者CD 
            this.form.cantxt_TumiGyoCd.Text = dr["TMH_GYOUSHA_CD"].ToString();

            // 積替保管業者名称 
            this.form.ctxt_TumiGyoName.Text = dr["TMH_GYOUSHA_NAME"].ToString();

            // 積替保管場CD 
            this.form.cantxt_TumiHokaNameCd.Text = dr["TMH_GENBA_CD"].ToString();

            // 積替保管場名称  
            this.form.ctxt_TumiHokaName.Text = dr["TMH_GENBA_NAME"].ToString();

            // 積替保管場郵便番号 
            this.form.cnt_TumiHokaZip.Text = dr["TMH_GENBA_POST"].ToString();

            // 積替保管場電話番号
            this.form.cnt_TumiHokaTel.Text = dr["TMH_GENBA_TEL"].ToString();

            // 積替保管場住所 
            this.form.ctxt_TumiHokaAdd.Text = dr["TMH_GENBA_ADDRESS"].ToString();

            // 有価物拾集有無 
            if (dr["YUUKA_KBN"].ToString() == "1")
            {
                this.form.cckb_TumiHokaAri.Checked = true;
                this.form.cckb_TumiHokaNasi.Checked = false;
            }
            else if (dr["YUUKA_KBN"].ToString() == "2")
            {
                this.form.cckb_TumiHokaAri.Checked = false;
                this.form.cckb_TumiHokaNasi.Checked = true;
            }
            // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
            else
            {
                this.form.cckb_TumiHokaAri.Checked = false;
                this.form.cckb_TumiHokaNasi.Checked = false;
            }
            // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end

            // 有価物拾集量
            if (dr["YUUKA_SUU"].ToString() != string.Empty)
            {
                this.form.cntxt_TumiHokaJisseki.Text = dr["YUUKA_SUU"].ToString();
            }

            //有価物拾集量単位CD
            if (dr["YUUKA_UNIT_CD"].ToString() == "1")
            {
                this.form.cckb_TumiHokaT.Checked = true;
                this.form.cckb_TumiHokaM3.Checked = false;
            }
            else if (dr["YUUKA_UNIT_CD"].ToString() == "2")
            {
                this.form.cckb_TumiHokaT.Checked = false;
                this.form.cckb_TumiHokaM3.Checked = true;
            }

            // 処分の受領者CD 
            this.form.cantxt_SyobunJyuCd.Text = dr["SBN_JYURYOUSHA_CD"].ToString();

            // 処分の受領者名称 
            this.form.ctxt_SyobunJyuName.Text = dr["SBN_JYURYOUSHA_NAME"].ToString();

            // 処分の受領担当者CD 
            this.form.cantxt_SyobunJyuUntenCd.Text = dr["SBN_JYURYOU_TANTOU_CD"].ToString();

            // 処分の受領担当者名
            this.form.cantxt_SyobunJyuUntenName.Text = dr["SBN_JYURYOU_TANTOU_NAME"].ToString();

            // 処分受領日  
            if (dr["SBN_JYURYOU_DATE"].ToString() != string.Empty)
            {
                this.form.cdate_SyobunJyu.Value = (Convert.ToDateTime(dr["SBN_JYURYOU_DATE"].ToString()));
            }

            // 処分の受託者CD  
            this.form.cantxt_SyobunSyoCd.Text = dr["SBN_JYUTAKUSHA_CD"].ToString();

            // 処分の受託者名称 
            this.form.ctxt_SyobunSyoName.Text = dr["SBN_JYUTAKUSHA_NAME"].ToString();

            // 処分担当者CD 
            this.form.cantxt_SyobunSyoUntenCd.Text = dr["SBN_TANTOU_CD"].ToString();

            // 処分担当者名 
            this.form.cantxt_SyobunSyoUntenName.Text = dr["SBN_TANTOU_NAME"].ToString();

            // 最終処分業者CD 
            this.form.cantxt_SaisyuSyobunGyoCd.Text = dr["LAST_SBN_GYOUSHA_CD"].ToString();

            // 最終処分場CD 
            this.form.cantxt_SaisyuSyobunbaCD.Text = dr["LAST_SBN_GENBA_CD"].ToString();

            // 最終処分場 名称 
            this.form.ctxt_SaisyuSyobunGyoName.Text = dr["LAST_SBN_GENBA_NAME"].ToString();

            // 最終処分場 郵便番号
            this.form.ctxt_SaisyuSyobunGyoZip.Text = dr["LAST_SBN_GENBA_POST"].ToString();

            // 最終処分場 住所 
            this.form.ctxt_SaisyuBasyoSyozai.Text = dr["LAST_SBN_GENBA_ADDRESS"].ToString();

            // 最終処分場 処分先番号 
            this.form.ctxt_SaisyuBasyoNo.Text = dr["LAST_SBN_GENBA_NUMBER"].ToString();

            // 最終処分 確認者 
            this.form.ctxt_SaisyuSyobun.Text = dr["LAST_SBN_CHECK_NAME"].ToString();

            // 照合確認B1票  
            if (dr["CHECK_B1"].ToString() != string.Empty)
            {
                this.form.cdate_SyougouKakuninB1.Value = Convert.ToDateTime(dr["CHECK_B1"].ToString());
            }

            // 照合確認B2票                 
            if (dr["CHECK_B2"].ToString() != string.Empty)
            {
                this.form.cdate_SyougouKakuninB2.Value = Convert.ToDateTime(dr["CHECK_B2"].ToString());
            }

            // 照合確認D票 
            if (dr["CHECK_D"].ToString() != string.Empty)
            {
                this.form.cdate_SyougouKakuninD.Value = Convert.ToDateTime(dr["CHECK_D"].ToString());
            }

            // 照合確認E票 
            if (dr["CHECK_E"].ToString() != string.Empty)
            {
                this.form.cdate_SyougouKakuninE.Value = Convert.ToDateTime(dr["CHECK_E"].ToString());
            }

            // 連携伝種区分CD 
            if (dr["RENKEI_DENSHU_KBN_CD"].ToString() != string.Empty)
            {
                this.form.parameters.RenkeiDenshuKbnCd = dr["RENKEI_DENSHU_KBN_CD"].ToString();
            }

            // 連携システムID 
            if (dr["RENKEI_SYSTEM_ID"].ToString() != string.Empty)
            {
                this.form.parameters.RenkeiSystemId = dr["RENKEI_SYSTEM_ID"].ToString();
            }

            // 連携明細システムID 
            if (dr["RENKEI_MEISAI_SYSTEM_ID"].ToString() != string.Empty)
            {
                this.form.parameters.RenkeiMeisaiSystemId = dr["RENKEI_MEISAI_SYSTEM_ID"].ToString();
            }

            if ((!this.form.ismobile_mode) || this.maniFlag != 1 || string.IsNullOrEmpty(dr["RENKEI_MEISAI_MODE"].ToString()))
            {
                this.form.cantxt_Renkei_Mode.Text = "1";
            }
            else
            {
                if (dr["RENKEI_MEISAI_MODE"].ToString() == "2")
                {
                    this.form.cantxt_Renkei_Mode.Text = "2";
                }
                else
                {
                    this.form.cantxt_Renkei_Mode.Text = "1";
                }
            }

            if (this.form.parameters.Mode != (int)WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (dr["CREATE_USER"].ToString() != string.Empty)
                {
                    this.headerform.customTextBox4.Text = dr["CREATE_USER"].ToString();
                }
                if (dr["CREATE_DATE"].ToString() != string.Empty)
                {
                    this.headerform.customTextBox2.Text = dr["CREATE_DATE"].ToString();
                }
                if (dr["UPDATE_USER"].ToString() != string.Empty)
                {
                    this.headerform.customTextBox3.Text = dr["UPDATE_USER"].ToString();
                }
                if (dr["UPDATE_DATE"].ToString() != string.Empty)
                {
                    this.headerform.customTextBox1.Text = dr["UPDATE_DATE"].ToString();
                }
            }
            if (pt == 1)
            {
                this.headerform.customTextBox4.Text = "";
                this.headerform.customTextBox2.Text = "";
                this.headerform.customTextBox3.Text = "";
                this.headerform.customTextBox1.Text = "";
            }

            //タイムスタンプ
            if (pt == 0 && dr["TME_TIME_STAMP"].ToString() != string.Empty)
            {
                int data2 = (int)dr["TME_TIME_STAMP"];
                byte[] d = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray(data2);
                this.TIME_STAMP_ENTRY = d;
            }

            // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 start

            //修正モードに連携項目の値を設定する
            //伝種区分(パターン表示時は上書きしない）
            if (pt == 0)
            {
                if (!string.IsNullOrEmpty(dr["RENKEI_DENSHU_KBN_CD"].ToString()))
                {
                    this.form.cantxt_DenshuKbn.Text = dr["RENKEI_DENSHU_KBN_CD"].ToString().PadLeft(3, '0');
                }
                else
                {
                    this.form.cantxt_DenshuKbn.Text = null;
                }
                //伝種区分名称
                this.SetDenshuKbnName(dr["RENKEI_DENSHU_KBN_CD"].ToString());
                //連携番号/明細行
                this.SetRenkeiSystemId(dr["RENKEI_DENSHU_KBN_CD"].ToString(), dr["RENKEI_SYSTEM_ID"].ToString(), dr["RENKEI_MEISAI_SYSTEM_ID"].ToString());
            }
            // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 end

            // レイアウト区分 
            if (dr["MANIFEST_MERCURY_CHECK"].ToString() != string.Empty)
            {
                manifestMercuryList[1].Text = dr["MANIFEST_MERCURY_CHECK"].ToString();
            }

            if (manifestMercuryList[1].Text.Equals("1"))
            {
                manifestMercuryList[2].Text = Constans.REIAUTO_KBN_1;
            }
            else
            {
                manifestMercuryList[2].Text = Constans.REIAUTO_KBN_2;
            }

            LayoutKbnCheck(manifestMercuryList[1].Text);

            // ﾏﾆ用紙へ印字区分 
            if (dr["MANIFEST_KAMI_CHECK"].ToString() != string.Empty)
            {
                manifestKamiList[1].Text = dr["MANIFEST_KAMI_CHECK"].ToString();
            }

            if (manifestKamiList[1].Text.Equals("1"))
            {
                manifestKamiList[2].Text = Constans.MANI_KBN_1;
            }
            else
            {
                manifestKamiList[2].Text = Constans.MANI_KBN_2;
            }

            LayoutKamiKbnCheck(manifestKamiList[1].Text);

            LogUtility.DebugMethodEnd(dr, pt);
        }

        /// <summary>
        /// マニフェスト運搬(T_MANIFEST_UPN)データ設定
        /// </summary>
        public void SetManifestUpn(DataRow dr, string No)
        {
            LogUtility.DebugMethodStart(dr, No);

            if (No == "1")
            {
                //運搬受託者CD
                this.form.cantxt_UnpanJyutaku1NameCd.Text = dr["UPN_GYOUSHA_CD"].ToString();

                //運搬受託者名称
                this.form.cantxt_UnpanJyutaku1Name.Text = dr["UPN_GYOUSHA_NAME"].ToString();

                this.form.cnt_UnpanJyutaku1Zip.Text = dr["UPN_GYOUSHA_POST"].ToString();

                this.form.cnt_UnpanJyutaku1Tel.Text = dr["UPN_GYOUSHA_TEL"].ToString();

                this.form.ctxt_UnpanJyutaku1Add.Text = dr["UPN_GYOUSHA_ADDRESS"].ToString();

                this.form.cantxt_UnpanHouhou1CD.Text = dr["UPN_HOUHOU_CD"].ToString();

                this.form.ctxt_UnpanHouhouMei1.Text = dr["UNPAN_HOUHOU_NAME"].ToString();

                this.form.cantxt_Jyutaku1SyaNo.Text = dr["SHARYOU_CD"].ToString();

                this.form.cantxt_Jyutaku1Syasyu.Text = dr["SHASHU_CD"].ToString();
                if (dr["TMH_KBN"].ToString() == "1")
                {
                    this.form.cckb_Jyutaku1HokanAri.Checked = true;
                    this.form.cckb_Jyutaku1HokanNasi.Checked = false;
                }
                else
                {
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                    if (dr["TMH_KBN"].ToString() == "0")
                    {
                        this.form.cckb_Jyutaku1HokanAri.Checked = false;
                        this.form.cckb_Jyutaku1HokanNasi.Checked = true;
                    }
                    else
                    {
                        this.form.cckb_Jyutaku1HokanAri.Checked = false;
                        this.form.cckb_Jyutaku1HokanNasi.Checked = false;
                    }
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
                }

                //this.form.cantxt_SyobunJyutakuNameCd.Text = dr["UPN_SAKI_GYOUSHA_CD"].ToString(); //運搬先で上書きしない！

                this.form.cantxt_UnpanJyugyobaNameCd.Text = dr["UPN_SAKI_GENBA_CD"].ToString();

                this.form.cantxt_UnpanJyugyobaName.Text = dr["UPN_SAKI_GENBA_NAME"].ToString();

                this.form.cnt_UnpanJyugyobaZip.Text = dr["UPN_SAKI_GENBA_POST"].ToString();

                this.form.cnt_UnpanJyugyobaTel.Text = dr["UPN_SAKI_GENBA_TEL"].ToString();

                this.form.ctxt_UnpanJyugyobaAdd.Text = dr["UPN_SAKI_GENBA_ADDRESS"].ToString();

                this.form.cantxt_UnpanJyuCd1.Text = dr["UPN_JYUTAKUSHA_CD"].ToString();

                this.form.ctxt_UnpanJyuName1.Text = dr["UPN_JYUTAKUSHA_NAME"].ToString();

                this.form.cantxt_UnpanJyuUntenCd1.Text = dr["UNTENSHA_CD"].ToString();

                this.form.cantxt_UnpanJyuUntenName1.Text = dr["UNTENSHA_NAME"].ToString();

                //this.form.ctxt_Jyutaku1SyaNo.Text = dr["SHARYOU_NAME_RYAKU"].ToString();
                this.form.ctxt_Jyutaku1SyaNo.Text = dr["INPUT_SHARYOU_NAME"].ToString();

                //this.form.ctxt_Jyutaku1Syasyu.Text = dr["SHASHU_NAME_RYAKU"].ToString();
                this.form.ctxt_Jyutaku1Syasyu.Text = dr["INPUT_SHASHU_NAME"].ToString();

                if (dr["UPN_END_DATE"].ToString() != string.Empty)
                {
                    this.form.cdate_UnpanJyu1.Value = Convert.ToDateTime(dr["UPN_END_DATE"].ToString());
                }
            }
            else
            {
                //運搬受託者CD
                this.form.cantxt_UnpanJyutaku2NameCd.Text = dr["UPN_GYOUSHA_CD"].ToString();

                //運搬受託者名称
                this.form.cantxt_UnpanJyutaku2Name.Text = dr["UPN_GYOUSHA_NAME"].ToString();

                this.form.cnt_UnpanJyutaku2Zip.Text = dr["UPN_GYOUSHA_POST"].ToString();

                this.form.cnt_UnpanJyutaku2Tel.Text = dr["UPN_GYOUSHA_TEL"].ToString();

                this.form.ctxt_UnpanJyutaku2Add.Text = dr["UPN_GYOUSHA_ADDRESS"].ToString();

                this.form.cantxt_UnpanHouhou2CD.Text = dr["UPN_HOUHOU_CD"].ToString();

                this.form.ctxt_UnpanHouhouMei2.Text = dr["UNPAN_HOUHOU_NAME"].ToString();

                this.form.cantxt_Jyutaku2SyaNo.Text = dr["SHARYOU_CD"].ToString();

                this.form.cantxt_Jyutaku2Syasyu.Text = dr["SHASHU_CD"].ToString();

                if (dr["TMH_KBN"].ToString() == "1")
                {
                    this.form.cckb_Jyutaku2HokanAri.Checked = true;
                    this.form.cckb_Jyutaku2HokanNasi.Checked = false;
                }
                else
                {
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし start
                    if (dr["TMH_KBN"].ToString() == "0")
                    {
                        this.form.cckb_Jyutaku2HokanAri.Checked = false;
                        this.form.cckb_Jyutaku2HokanNasi.Checked = true;
                    }
                    else
                    {
                        this.form.cckb_Jyutaku2HokanAri.Checked = false;
                        this.form.cckb_Jyutaku2HokanNasi.Checked = false;
                    }
                    // 20140609 ria EV004635 積替・保管と有価物拾集をチェックボックスにし end
                }

                //this.form.cantxt_SyobunJyutakuNameCd.Text = dr["UPN_SAKI_GYOUSHA_CD"].ToString(); //運搬先で上書きしない！

                this.form.cantxt_UnpanJyugyobaNameCd.Text = dr["UPN_SAKI_GENBA_CD"].ToString();

                this.form.cantxt_UnpanJyugyobaName.Text = dr["UPN_SAKI_GENBA_NAME"].ToString();

                this.form.cnt_UnpanJyugyobaZip.Text = dr["UPN_SAKI_GENBA_POST"].ToString();

                this.form.cnt_UnpanJyugyobaTel.Text = dr["UPN_SAKI_GENBA_TEL"].ToString();

                this.form.ctxt_UnpanJyugyobaAdd.Text = dr["UPN_SAKI_GENBA_ADDRESS"].ToString();

                this.form.cantxt_UnpanJyuCd2.Text = dr["UPN_JYUTAKUSHA_CD"].ToString();

                this.form.ctxt_UnpanJyuName2.Text = dr["UPN_JYUTAKUSHA_NAME"].ToString();

                this.form.cantxt_UnpanJyuUntenCd2.Text = dr["UNTENSHA_CD"].ToString();

                this.form.cantxt_UnpanJyuUntenName2.Text = dr["UNTENSHA_NAME"].ToString();

                this.form.ctxt_Jyutaku2SyaNo.Text = dr["INPUT_SHARYOU_NAME"].ToString();

                this.form.ctxt_Jyutaku2Syasyu.Text = dr["INPUT_SHASHU_NAME"].ToString();

                if (dr["UPN_END_DATE"].ToString() != string.Empty)
                {
                    this.form.cdate_UnpanJyu2.Value = Convert.ToDateTime(dr["UPN_END_DATE"].ToString());
                }
            }

            LogUtility.DebugMethodEnd(dr, No);
        }

        /// <summary>
        /// マニ印字(T_MANIFEST_PRT)データ設定
        /// </summary>
        public void SetManifestPrt(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            // 印字数量
            if (dr["PRT_SUU"].ToString() != string.Empty)
            {
                this.form.cntxt_TotalJyuryo.Text = dr["PRT_SUU"].ToString();
            }

            // 印字単位CD
            if (dr["PRT_UNIT_CD"].ToString() != string.Empty)
            {
                this.form.cntxt_SanpaiTani.Text = dr["PRT_UNIT_CD"].ToString();
                if (this.form.cntxt_SanpaiTani.Text == "1")
                {
                    this.form.crdo_SanpaiSyuruiT.Checked = true;
                }
                if (this.form.cntxt_SanpaiTani.Text == "2")
                {
                    // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう start
                    //this.form.crdo_SanpaiSyuruiKg.Checked = true;
                    this.form.crdo_SanpaiSyuruiM3.Checked = true;
                    // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう end
                }
                if (this.form.cntxt_SanpaiTani.Text == "3")
                {
                    // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう start
                    //this.form.crdo_SanpaiSyuruiM3.Checked = true;
                    this.form.crdo_SanpaiSyuruiKg.Checked = true;
                    // 20140618 katen EV004605 入力画面と一覧で「ｋｇ」と「」が逆に表示されてしまう end
                }
                if (this.form.cntxt_SanpaiTani.Text == "4")
                {
                    this.form.crdo_SanpaiSyuruiRittoru.Checked = true;
                }
            }

            // 印字廃棄物名称CD 
            this.form.cantxt_SanpaiSyuruiCd.Text = dr["PRT_HAIKI_NAME_CD"].ToString();

            // 印字廃棄物名称 
            this.form.ctxt_SanpaiSyuruiName.Text = dr["PRT_HAIKI_NAME"].ToString();

            // 斜線項目備考
            if (dr["SLASH_BIKOU_FLG"].ToString() == "True")
            {
                this.form.ctxt_UnpanJigyobaTokki.Enabled = false;
                this.form.lineShape9.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.ctxt_UnpanJigyobaTokki.Enabled = enabled;
                this.form.lineShape9.Visible = false;
            }

            // 斜線項目中間処理産業廃棄物 
            if (dr["SLASH_CHUUKAN_FLG"].ToString() == "True")
            {
                this.form.ccbx_TyukanTyoubo.Enabled = false;
                this.form.ccbx_TyukanKisai.Enabled = false;
                this.form.ctxt_TyukanHaikibutu.Enabled = false;
                this.form.lineShape5.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.ccbx_TyukanTyoubo.Enabled = enabled;
                this.form.ccbx_TyukanKisai.Enabled = enabled;
                this.form.ctxt_TyukanHaikibutu.Enabled = enabled;
                this.form.lineShape5.Visible = false;
            }

            // 斜線項目積替保管 
            if (dr["SLASH_TSUMIHO_FLG"].ToString() == "True")
            {
                this.form.cantxt_TumiGyoCd.Enabled = false;
                this.form.ctxt_TumiGyoName.Enabled = false;
                this.form.cbtn_TumiGyo.Enabled = false;
                this.form.cantxt_TumiHokaNameCd.Enabled = false;
                this.form.ctxt_TumiHokaName.Enabled = false;
                this.form.cbtn_TumiHokaName.Enabled = false;
                this.form.cnt_TumiHokaZip.Enabled = false;
                this.form.cbtn_TumiHokaSan.Enabled = false;
                this.form.cbtn_TumiHokaDel.Enabled = false;
                this.form.cnt_TumiHokaTel.Enabled = false;
                this.form.ctxt_TumiHokaAdd.Enabled = false;
                this.form.cckb_TumiHokaAri.Enabled = false;
                this.form.cckb_TumiHokaNasi.Enabled = false;
                this.form.cntxt_TumiHokaJisseki.Enabled = false;
                this.form.cckb_TumiHokaT.Enabled = false;
                this.form.cckb_TumiHokaM3.Enabled = false;
                this.form.lineShape7.Visible = true;

            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cantxt_TumiGyoCd.Enabled = enabled;
                this.form.ctxt_TumiGyoName.Enabled = enabled;
                this.form.cbtn_TumiGyo.Enabled = enabled;
                this.form.cantxt_TumiHokaNameCd.Enabled = enabled;
                this.form.ctxt_TumiHokaName.Enabled = enabled;
                this.form.cbtn_TumiHokaName.Enabled = enabled;
                this.form.cnt_TumiHokaZip.Enabled = enabled;
                this.form.cbtn_TumiHokaSan.Enabled = enabled;
                this.form.cbtn_TumiHokaDel.Enabled = enabled;
                this.form.cnt_TumiHokaTel.Enabled = enabled;
                this.form.ctxt_TumiHokaAdd.Enabled = enabled;
                this.form.cckb_TumiHokaAri.Enabled = enabled;
                this.form.cckb_TumiHokaNasi.Enabled = enabled;
                this.form.cntxt_TumiHokaJisseki.Enabled = enabled;
                this.form.cckb_TumiHokaT.Enabled = enabled;
                this.form.cckb_TumiHokaM3.Enabled = enabled;
                this.form.lineShape7.Visible = false;
            }

            // 斜線項目事前協議 
            if (dr["SLASH_JIZENKYOUGI_FLG"].ToString() == "True")
            {
                this.form.cntxt_jizenkyougi.Enabled = false;
                this.form.cdate_JizenKyougi.Enabled = false;
                this.form.lineShape6.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cntxt_jizenkyougi.Enabled = enabled;
                this.form.cdate_JizenKyougi.Enabled = enabled;
                this.form.lineShape6.Visible = false;
            }

            // 斜線項目運搬受託者2
            if (dr["SLASH_UPN_GYOUSHA2_FLG"].ToString() == "True")
            {
                this.form.cnt_UnpanJyutaku2Zip.Enabled = false;
                this.form.cbtn_UnpanJyutaku2San.Enabled = false;
                this.form.cbtn_UnpanJyutaku2Del.Enabled = false;
                this.form.cnt_UnpanJyutaku2Tel.Enabled = false;
                this.form.ctxt_UnpanJyutaku2Add.Enabled = false;
                this.form.cantxt_UnpanJyutaku2NameCd.Enabled = false;
                this.form.cantxt_UnpanJyutaku2Name.Enabled = false;
                this.form.casbtn_UnpanJyutaku2.Enabled = false;
                this.form.cckb_Jyutaku2HokanAri.Enabled = false;
                this.form.cckb_Jyutaku2HokanNasi.Enabled = false;
                this.form.cantxt_Jyutaku2SyaNo.Enabled = false;
                this.form.ctxt_Jyutaku2SyaNo.Enabled = false;
                this.form.cantxt_Jyutaku2Syasyu.Enabled = false;
                this.form.ctxt_Jyutaku2Syasyu.Enabled = false;
                this.form.cantxt_UnpanHouhou2CD.Enabled = false;
                this.form.ctxt_UnpanHouhouMei2.Enabled = false;

                this.form.lineShape8.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cnt_UnpanJyutaku2Zip.Enabled = enabled;
                this.form.cbtn_UnpanJyutaku2San.Enabled = enabled;
                this.form.cbtn_UnpanJyutaku2Del.Enabled = enabled;
                this.form.cnt_UnpanJyutaku2Tel.Enabled = enabled;
                this.form.ctxt_UnpanJyutaku2Add.Enabled = enabled;
                this.form.cantxt_UnpanJyutaku2NameCd.Enabled = enabled;
                this.form.cantxt_UnpanJyutaku2Name.Enabled = enabled;
                this.form.casbtn_UnpanJyutaku2.Enabled = enabled;
                this.form.cckb_Jyutaku2HokanAri.Enabled = enabled;
                this.form.cckb_Jyutaku2HokanNasi.Enabled = enabled;
                this.form.cantxt_Jyutaku2SyaNo.Enabled = enabled;
                this.form.ctxt_Jyutaku2SyaNo.Enabled = enabled;
                this.form.cantxt_Jyutaku2Syasyu.Enabled = enabled;
                this.form.ctxt_Jyutaku2Syasyu.Enabled = enabled;
                this.form.cantxt_UnpanHouhou2CD.Enabled = enabled;
                this.form.ctxt_UnpanHouhouMei2.Enabled = enabled;
                this.form.lineShape8.Visible = false;
            }

            // 斜線項目運搬の受託者2 
            if (dr["SLASH_UPN_JYUTAKUSHA2_FLG"].ToString() == "True")
            {
                this.form.cantxt_UnpanJyuCd2.Enabled = false;
                this.form.ctxt_UnpanJyuName2.Enabled = false;
                this.form.cantxt_UnpanJyuUntenName2.Enabled = false;
                this.form.customButton2.Enabled = false;
                this.form.cantxt_UnpanJyuUntenCd2.Enabled = false;
                this.form.cdate_UnpanJyu2.Enabled = false;
                this.form.lineShape10.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cantxt_UnpanJyuCd2.Enabled = enabled;
                this.form.ctxt_UnpanJyuName2.Enabled = enabled;
                this.form.cantxt_UnpanJyuUntenName2.Enabled = enabled;
                this.form.customButton2.Enabled = enabled;
                this.form.cantxt_UnpanJyuUntenCd2.Enabled = enabled;

                //20250402
                //this.form.cdate_UnpanJyu2.Enabled = enabled;

                this.form.lineShape10.Visible = false;
            }

            // 斜線項目B1票
            if (dr["SLASH_B1_FLG"].ToString() == "True")
            {
                this.form.cdate_SyougouKakuninB1.Enabled = false;
                this.form.lineShape1.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cdate_SyougouKakuninB1.Enabled = enabled;
                this.form.lineShape1.Visible = false;
            }

            // 斜線項目B2票 
            if (dr["SLASH_B2_FLG"].ToString() == "True")
            {
                this.form.cdate_SyougouKakuninB2.Enabled = false;
                this.form.lineShape2.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cdate_SyougouKakuninB2.Enabled = enabled;
                this.form.lineShape2.Visible = false;
            }

            // 斜線項目D票 
            if (dr["SLASH_D_FLG"].ToString() == "True")
            {
                this.form.cdate_SyougouKakuninD.Enabled = false;
                this.form.lineShape3.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cdate_SyougouKakuninD.Enabled = enabled;
                this.form.lineShape3.Visible = false;
            }

            // 斜線項目E票 
            if (dr["SLASH_E_FLG"].ToString() == "True")
            {
                this.form.cdate_SyougouKakuninE.Enabled = false;
                this.form.lineShape4.Visible = true;
            }
            else
            {
                var enabled = !IsReadOnlyMode();

                this.form.cdate_SyougouKakuninE.Enabled = enabled;
                this.form.lineShape4.Visible = false;
            }

            LogUtility.DebugMethodEnd(dr);
        }

        /// <summary>
        /// マニ返却日(T_MANIFEST_RET_DATE)データ設定
        /// </summary>
        public void SetManifestRetDateList(DataRow dr, byte pt = 0)
        {
            LogUtility.DebugMethodStart(dr, pt);

            //パターンは設定しない。
            if (pt == 1)
            {
                return;
            }

            //A票
            if (dr["SEND_A"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuA.Value = Convert.ToDateTime(dr["SEND_A"].ToString());
            }

            //B1票
            if (dr["SEND_B1"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuB1.Value = Convert.ToDateTime(dr["SEND_B1"].ToString());
            }

            //B2票
            if (dr["SEND_B2"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuB2.Value = Convert.ToDateTime(dr["SEND_B2"].ToString());
            }

            //C1票
            if (dr["SEND_C1"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuC1.Value = Convert.ToDateTime(dr["SEND_C1"].ToString());
            }

            //C2票 
            if (dr["SEND_C2"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuC2.Value = Convert.ToDateTime(dr["SEND_C2"].ToString());
            }

            //D票 
            if (dr["SEND_D"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuD.Value = Convert.ToDateTime(dr["SEND_D"].ToString());
            }

            //E票
            if (dr["SEND_E"].ToString() != string.Empty)
            {
                this.form.cdate_HenkyakuE.Value = Convert.ToDateTime(dr["SEND_E"].ToString());
            }

            //タイムスタンプ
            if (string.IsNullOrEmpty(dr["TMRD_TIME_STAMP"].ToString()) == false)
            {
                int data2 = (int)dr["TMRD_TIME_STAMP"];
                byte[] d = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray(data2);
                this.TIME_STAMP_RET_DATE = d;
            }

            LogUtility.DebugMethodEnd(dr, pt);
        }

        /// <summary>
        /// マニ印字明細(T_MANIFEST_DETAIL_PRT)データ作成
        /// </summary>
        public void SetManifestDetailPrt(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            CheckBox ck = null;
            TextBox tb = null;
            string strNo = string.Empty;

            // 印字番号
            strNo = dr["REC_NO"].ToString();

            switch (strNo)
            {
                case "1":
                    // this.form.cckb_Antei01.Checked = true;
                    ck = this.form.cckb_Antei01;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo01;
                    break;

                case "2":
                    // this.form.cckb_Antei02.Checked = true;
                    ck = this.form.cckb_Antei02;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo02;
                    break;

                case "3":
                    // this.form.cckb_Antei03.Checked = true;
                    ck = this.form.cckb_Antei03;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo03;
                    break;

                case "4":
                    // this.form.cckb_Antei04.Checked = true;
                    ck = this.form.cckb_Antei04;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo04;
                    break;

                case "5":
                    // this.form.cckb_Antei05.Checked = true;
                    ck = this.form.cckb_Antei05;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo05;
                    break;

                case "6":
                    // this.form.cckb_Antei06.Checked = true;
                    ck = this.form.cckb_Antei06;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo06;
                    break;

                case "7":
                    // sthis.form.cckb_Antei07.Checked = true;
                    ck = this.form.cckb_Antei07;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo07;
                    break;

                case "8":
                    // this.form.cckb_Antei08.Checked = true;
                    ck = this.form.cckb_Antei08;
                    tb = (TextBox)this.form.cntxt_AnteiSuryo08;
                    break;

                case "9":
                    // this.form.cckb_AnteiFree01.Checked = true;
                    ck = this.form.cckb_AnteiFree01;

                    // 廃棄物種類CD
                    this.form.cantxt_AnteiFreeCd01.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_AnteiFreeName01.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_AnteiFreeSuryo01;
                    break;

                case "10":
                    // this.form.cckb_AnteiFree02.Checked = true;
                    ck = this.form.cckb_AnteiFree02;

                    // 廃棄物種類CD
                    this.form.cantxt_AnteiFreeCd02.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_AnteiFreeName02.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_AnteiFreeSuryo02;
                    break;

                case "11":
                    // this.form.cckb_AnteiFree03.Checked = true;
                    ck = this.form.cckb_AnteiFree03;

                    // 廃棄物種類CD
                    this.form.cantxt_AnteiFreeCd03.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_AnteiFreeName03.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_AnteiFreeSuryo03;
                    break;

                case "12":
                    // this.form.cckb_AnteiFree04.Checked = true;
                    ck = this.form.cckb_AnteiFree04;

                    // 廃棄物種類CD
                    this.form.cantxt_AnteiFreeCd04.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_AnteiFreeNam04.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_AnteiFreeSuryo04;
                    break;

                case "13":
                    // this.form.cckb_Kanri01.Checked = true;
                    ck = this.form.cckb_Kanri01;
                    tb = (TextBox)this.form.cntxt_KanriSuryo11;
                    break;

                case "14":
                    // this.form.cckb_Kanri02.Checked = true;
                    ck = this.form.cckb_Kanri02;
                    tb = (TextBox)this.form.cntxt_KanriSuryo12;
                    break;

                case "15":
                    // this.form.cckb_Kanri03.Checked = true;
                    ck = this.form.cckb_Kanri03;
                    tb = (TextBox)this.form.cntxt_KanriSuryo13;
                    break;

                case "16":
                    // this.form.cckb_Kanri04.Checked = true;
                    ck = this.form.cckb_Kanri04;
                    tb = (TextBox)this.form.cntxt_KanriSuryo14;
                    break;

                case "17":
                    // this.form.cckb_Kanri05.Checked = true;
                    ck = this.form.cckb_Kanri05;
                    tb = (TextBox)this.form.cntxt_KanriSuryo15;
                    break;

                case "18":
                    // this.form.cckb_Kanri06.Checked = true;
                    ck = this.form.cckb_Kanri06;
                    tb = (TextBox)this.form.cntxt_KanriSuryo16;
                    break;

                case "19":
                    // this.form.cckb_Kanri17.Checked = true;
                    ck = this.form.cckb_Kanri17;
                    tb = (TextBox)this.form.cntxt_KanriSuryo17;
                    break;

                case "20":
                    // this.form.cckb_KanriFree01.Checked = true;
                    ck = this.form.cckb_KanriFree01;

                    // 廃棄物種類CD
                    this.form.cantxt_KanriFreeCd01.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_KanriFreeName01.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_KanriFreeSuryo01;
                    break;

                case "21":
                    // this.form.cckb_KanriFree02.Checked = true;
                    ck = this.form.cckb_KanriFree02;

                    // 廃棄物種類CD
                    this.form.cantxt_KanriFreeCd02.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_KanriFreeName02.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_KanriFreeSuryo02;
                    break;

                case "22":
                    // this.form.cckb_KanriFree03.Checked = true;
                    ck = this.form.cckb_KanriFree03;

                    // 廃棄物種類CD
                    this.form.cantxt_KanriFreeCd03.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_KanriFreeName03.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_KanriFreeSuryo03;
                    break;

                case "23":
                    // this.form.cckb_Tokubetu21.Checked = true;
                    ck = this.form.cckb_Tokubetu21;
                    tb = (TextBox)this.form.cntxt_TokubetuSuryo21;
                    break;

                case "24":
                    // this.form.cckb_TokubetuFree01.Checked = true;
                    ck = this.form.cckb_TokubetuFree01;
                    // 廃棄物種類CD
                    this.form.cantxt_TokubetuFreeCd01.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_TokubetuFreeName01.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_TokubetuFreeSuryo01;
                    break;

                case "25":
                    // this.form.cckb_TokubetuFree02.Checked = true;
                    ck = this.form.cckb_TokubetuFree02;

                    // 廃棄物種類CD
                    this.form.cantxt_TokubetuFreeCd02.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_TokubetuFreeName02.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_TokubetuFreeSuryo02;
                    break;

                case "26":
                    // this.form.cckb_TokubetuFree02.Checked = true;
                    ck = this.form.cckb_TokubetuFree03;

                    // 廃棄物種類CD
                    this.form.cantxt_TokubetuFreeCd03.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_TokubetuFreeName03.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_TokubetuFreeSuryo03;
                    break;

                case "27":
                    // this.form.cckb_TokubetuFree04.Checked = true;
                    ck = this.form.cckb_TokubetuFree04;

                    // 廃棄物種類CD
                    this.form.cantxt_TokubetuFreeCd04.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_TokubetuFreeName04.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_TokubetuFreeSuryo04;
                    break;

                case "28":
                    // this.form.cckb_TokubetuFree05.Checked = true;
                    ck = this.form.cckb_TokubetuFree05;

                    // 廃棄物種類CD
                    this.form.cantxt_TokubetuFreeCd05.Text = dr["HAIKI_SHURUI_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_TokubetuFreeName05.Text = dr["HAIKI_SHURUI_NAME"].ToString();

                    tb = (TextBox)this.form.cntxt_TokubetuFreeSuryo05;
                    break;

                default:
                    break;
            }

            // checkBox
            if ((dr["PRT_FLG"] != null && !string.IsNullOrEmpty(dr["PRT_FLG"].ToString())) && ck != null)
            {
                ck.Checked = dr.Field<bool>("PRT_FLG");
            }

            // 数量
            if (String.IsNullOrEmpty(dr["HAIKI_SUURYOU"].ToString()) == false)
            {
                tb.Text = Convert.ToDecimal(dr["HAIKI_SUURYOU"]).ToString(this.ManifestSuuryoFormat);
            }

            LogUtility.DebugMethodEnd(dr);
        }

        /// <summary>
        /// マニ印字_建廃_形状(T_MANIFEST_KP_KEIJYOU)データ作成
        /// </summary>
        public void SetManifestKpKeijyou(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            CheckBox ck = null;
            string strNo = string.Empty;

            // 印字番号
            strNo = dr["REC_NO"].ToString();
            switch (strNo)
            {
                case "1":
                    this.form.cckb_KeijyoKokei.Checked = true;
                    break;

                case "2":
                    this.form.cckb_KeijyoDoro.Checked = true;
                    break;

                case "3":
                    this.form.cckb_KeijyoEkitai.Checked = true;
                    break;

                case "4":
                    // this.form.cckb_KeijyoFree01.Checked = true;
                    ck = this.form.cckb_KeijyoFree01;

                    // 廃棄物種類CD
                    this.form.cantxt_KeijyoFreeCd01.Text = dr["KEIJOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.customTextBox116.Text = dr["KEIJOU_NAME"].ToString();

                    break;

                case "5":
                    // this.form.cckb_KeijyoFree02.Checked = true;
                    ck = this.form.cckb_KeijyoFree02;

                    // 廃棄物種類CD
                    this.form.cantxt_KeijyoFreeCd02.Text = dr["KEIJOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.customTextBox118.Text = dr["KEIJOU_NAME"].ToString();

                    break;

                case "6":
                    // this.form.cckb_KeijyoFree03.Checked = true;
                    ck = this.form.cckb_KeijyoFree03;

                    // 廃棄物種類CD
                    this.form.cantxt_KeijyoFreeCd03.Text = dr["KEIJOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.customTextBox120.Text = dr["KEIJOU_NAME"].ToString();

                    break;

                case "7":
                    // this.form.cckb_KeijyoFree04.Checked = true;
                    ck = this.form.cckb_KeijyoFree04;

                    // 廃棄物種類CD
                    this.form.cantxt_KeijyoFreeCd04.Text = dr["KEIJOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.customTextBox124.Text = dr["KEIJOU_NAME"].ToString();

                    break;

                default:
                    break;
            }

            // checkBox
            if ((dr["PRT_FLG"] != null && !string.IsNullOrEmpty(dr["PRT_FLG"].ToString())) && ck != null)
            {
                ck.Checked = dr.Field<bool>("PRT_FLG");
            }

            LogUtility.DebugMethodEnd(dr);
        }

        /// <summary>
        /// マニ印字_建廃_荷姿(T_MANIFEST_KP_NISUGATA)データ作成
        /// </summary>
        public void SetManifestKpNisugata(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            CheckBox ck = null;
            string strNo = string.Empty;

            // 印字番号
            strNo = dr["REC_NO"].ToString();
            switch (strNo)
            {
                case "1":
                    this.form.cckb_NisugataBara.Checked = true;
                    break;

                case "2":
                    this.form.cckb_NisugataKontena.Checked = true;
                    break;

                case "3":
                    this.form.cckb_NisugataDoramu.Checked = true;
                    break;

                case "4":
                    this.form.cckb_NisugataHukuro.Checked = true;
                    break;

                case "5":
                    // this.form.cckb_NisugataFree01.Checked = true;
                    ck = this.form.cckb_NisugataFree01;

                    // 廃棄物種類CD
                    this.form.cantxt_NisugataFreeCd01.Text = dr["NISUGATA_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_NisugataFreeName01.Text = dr["NISUGATA_NAME"].ToString();

                    break;

                case "6":
                    // this.form.cckb_NisugataFree02.Checked = true;
                    ck = this.form.cckb_NisugataFree02;

                    // 廃棄物種類CD
                    this.form.cantxt_NisugataFreeCd02.Text = dr["NISUGATA_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_NisugataFreeName02.Text = dr["NISUGATA_NAME"].ToString();

                    break;

                case "7":
                    // this.form.cckb_NisugataFree03.Checked = true;
                    ck = this.form.cckb_NisugataFree03;

                    // 廃棄物種類CD
                    this.form.cantxt_NisugataFreeCd03.Text = dr["NISUGATA_CD"].ToString();

                    // 廃棄物種類名
                    this.form.cantxt_NisugataFreeName03.Text = dr["NISUGATA_NAME"].ToString();

                    break;

                default:
                    break;
            }

            // checkBox
            if ((dr["PRT_FLG"] != null && !string.IsNullOrEmpty(dr["PRT_FLG"].ToString())) && ck != null)
            {
                ck.Checked = dr.Field<bool>("PRT_FLG");
            }

            LogUtility.DebugMethodEnd(dr);
        }

        /// <summary>
        /// マニ印字_建廃_処分方法(T_MANIFEST_KP_SBN_HOUHOU)
        /// </summary>
        public void SetManifestKpHouhou(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            CheckBox ck = null;
            string strNo = string.Empty;

            // 印字番号
            strNo = dr["REC_NO"].ToString();
            switch (strNo)
            {
                case "1":
                    this.form.cckb_UnpanJigyobaDassui.Checked = true;
                    break;

                case "2":
                    this.form.cckb_UnpanJigyobaSyoukyaku.Checked = true;
                    break;

                case "3":
                    this.form.cckb_UnpanJigyobaHasai.Checked = true;
                    break;

                case "4":
                    // this.form.cckb_UnpanJigyobaFree1.Checked = true;
                    ck = this.form.cckb_UnpanJigyobaFree1;

                    // 廃棄物種類CD
                    this.form.cantxt_UnpanJigyobaFreeCd1.Text = dr["SHOBUN_HOUHOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_UnpanJigyobaFreeName1.Text = dr["SHOBUN_HOUHOU_NAME"].ToString();

                    break;

                case "5":
                    // this.form.cckb_UnpanJigyobaFree2.Checked = true;
                    ck = this.form.cckb_UnpanJigyobaFree2;

                    // 廃棄物種類CD
                    this.form.cantxt_UnpanJigyobaFreeCd2.Text = dr["SHOBUN_HOUHOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_UnpanJigyobaFreeName2.Text = dr["SHOBUN_HOUHOU_NAME"].ToString();

                    break;

                case "6":
                    // this.form.cckb_UnpanJigyobaFree3.Checked = true;
                    ck = this.form.cckb_UnpanJigyobaFree3;

                    // 廃棄物種類CD
                    this.form.cantxt_UnpanJigyobaFreeCd3.Text = dr["SHOBUN_HOUHOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_UnpanJigyobaFreeName3.Text = dr["SHOBUN_HOUHOU_NAME"].ToString();

                    break;

                case "7":
                    // this.form.cckb_UnpanJigyobaFree4.Checked = true;
                    ck = this.form.cckb_UnpanJigyobaFree4;

                    // 廃棄物種類CD
                    this.form.cantxt_UnpanJigyobaFreeCd4.Text = dr["SHOBUN_HOUHOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_UnpanJigyobaFreeName4.Text = dr["SHOBUN_HOUHOU_NAME"].ToString();

                    break;

                case "8":
                    // this.form.cckb_UnpanJigyobaFree5.Checked = true;
                    ck = this.form.cckb_UnpanJigyobaFree5;

                    // 廃棄物種類CD
                    this.form.cantxt_UnpanJigyobaFreeCd5.Text = dr["SHOBUN_HOUHOU_CD"].ToString();

                    // 廃棄物種類名
                    this.form.ctxt_UnpanJigyobaFreeName5.Text = dr["SHOBUN_HOUHOU_NAME"].ToString();

                    break;

                case "11":
                    this.form.cckb_UnpanJigyobaAntei.Checked = true;
                    break;

                case "12":
                    this.form.cckb_UnpanJigyobaKanri.Checked = true;
                    break;

                case "13":
                    this.form.cckb_UnpanJigyobaSyadan.Checked = true;
                    break;

                default:
                    break;
            }

            // checkBox
            if ((dr["PRT_FLG"] != null && !string.IsNullOrEmpty(dr["PRT_FLG"].ToString())) && ck != null)
            {
                ck.Checked = dr.Field<bool>("PRT_FLG");
            }

            LogUtility.DebugMethodEnd(dr);
        }

        /// <summary>
        /// マニ明細(T_MANIFEST_DETAIL)データ作成
        /// </summary>
        public void SetManifestDetail(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            this.existAllLastSbnInfo = false;

            string strEndDate = string.Empty;
            string strJyutakuDate = string.Empty;

            // 紐付いている一次電マニが最終処分終了報告済みかフラグ
            bool isFixedFirstElecMani = false;
            bool isExecutingFirstElecMani = false;
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.GetHashCode() == this.form.parameters.Mode)
            {
                isFixedFirstElecMani = this.mlogic.IsFixedRelationFirstMani(SqlInt64.Parse(this.form.parameters.SystemId), 3);
                isExecutingFirstElecMani = this.mlogic.IsExecutingLastSbnEndRep(SqlInt64.Parse(this.form.parameters.SystemId), 2);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.form.cdgrid_Jisseki.RowCount++;

                // 入力制限判定用の変数
                bool existLastSbnEndDate = false;
                bool existLastSbnGyousha = false;
                bool existLastSbnGenba = false;

                //廃棄物種類CD
                if (dr["HAIKI_SHURUI_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value = dr["HAIKI_SHURUI_CD"].ToString();
                }

                //廃棄物種類名称
                if (dr["HAIKI_SHURUI_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value = dr["HAIKI_SHURUI_NAME"].ToString();
                }

                //廃棄物名称CD 
                if (dr["HAIKI_NAME_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].Value = dr["HAIKI_NAME_CD"].ToString();
                }

                //廃棄物名称名
                if (dr["HAIKI_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiName].Value = dr["HAIKI_NAME"].ToString();
                }

                //荷姿CD
                if (dr["NISUGATA_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].Value = dr["NISUGATA_CD"].ToString();
                }

                //荷姿名称
                if (dr["NISUGATA_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataName].Value = dr["NISUGATA_NAME"].ToString();
                }

                //割合 
                if (dr["WARIAI"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value = dr["WARIAI"].ToString();
                }

                // 20140625 ria EV005038 混合種類・混合単位を登録したパターンを呼び出したとき、割合が0またはブランクでないのに単位CDが編集可能となっている start
                if (!string.IsNullOrEmpty(this.form.cantxt_KongoCd.Text) && !string.IsNullOrEmpty(this.form.canTxt_JissekiTaniCd.Text))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value)) ||
                        Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value) != 0)
                    {
                        //単位
                        this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = true;
                    }
                }
                // 20140625 ria EV005038 混合種類・混合単位を登録したパターンを呼び出したとき、割合が0またはブランクでないのに単位CDが編集可能となっている end

                //数量
                if (dr["HAIKI_SUU"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells["Suryo"].Value
                      = mlogic.GetSuuryoRound(Convert.ToDecimal(dr["HAIKI_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                }

                //単位CD 
                if (dr["HAIKI_UNIT_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = dr["HAIKI_UNIT_CD"].ToString();
                }

                //単位名称
                if (dr["UNIT_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = dr["UNIT_NAME"].ToString();
                }

                //換算後数量 
                if (dr["KANSAN_SUU"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells["KansangoSuryo"].Value
                        = mlogic.GetSuuryoRound(Convert.ToDecimal(dr["KANSAN_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                }

                //減容後数量 
                if (dr["GENNYOU_SUU"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells["GenyoyugoTotalSuryo"].Value
                        = mlogic.GetSuuryoRound(Convert.ToDecimal(dr["GENNYOU_SUU"].ToString()), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                }
                else
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells["GenyoyugoTotalSuryo"].Value = (0).ToString(this.ManifestSuuryoFormat);
                }

                //処分方法CD
                if (dr["SBN_HOUHOU_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].Value = dr["SBN_HOUHOU_CD"].ToString();
                }

                //処分方法名称
                if (dr["SHOBUN_HOUHOU_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunName].Value = dr["SHOBUN_HOUHOU_NAME"].ToString();
                }

                //処分終了日 
                if (dr["SBN_END_DATE"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value = dr["SBN_END_DATE"].ToString();
                }

                //最終処分終了日
                if (dr["LAST_SBN_END_DATE"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value = dr["LAST_SBN_END_DATE"].ToString();
                    existLastSbnEndDate = true;
                }

                //最終処分業者CD
                if (dr["LAST_SBN_GYOUSHA_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value = dr["LAST_SBN_GYOUSHA_CD"].ToString();
                }

                //最終処分業者名称
                if (dr["GYOUSHA_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaName].Value = dr["GYOUSHA_NAME"].ToString();
                    existLastSbnGyousha = true;
                }

                //最終処分現場CD 
                if (dr["LAST_SBN_GENBA_CD"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value = dr["LAST_SBN_GENBA_CD"].ToString();
                }

                //最終処分現場名称
                if (dr["GENBA_NAME"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunBasyo].Value = dr["GENBA_NAME"].ToString();
                    existLastSbnGenba = true;
                }

                //二次マニ交付番号
                if (dr["NEXT_SYSTEM_ID"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NijiManiNo].Value = dr["NEXT_SYSTEM_ID"].ToString();

                    //二次マニと関連付けされている場合、入力不可
                    //二次マニ交付番号
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NijiManiNo].ReadOnly = true;

                    //最終処分終了日
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].ReadOnly = true;

                    //最終処分業者CD
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].ReadOnly = true;

                    //最終処分現場CD 
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].ReadOnly = true;

                    //廃棄物種類CD
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].ReadOnly = true;

                    //廃棄物の名称CD
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiNameCd].ReadOnly = true;

                    //荷姿CD
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NisugataCd].ReadOnly = true;

                    //割合
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].ReadOnly = true;

                    //数量
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].ReadOnly = true;

                    //単位
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = true;

                    //処分方法CD
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunCd].ReadOnly = true;

                    //処分終了日
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].ReadOnly = true;
                }

                // 二次マニの場合は紐付けた一次電マニが既に最終処分終了報告されていた場合
                // 最終処分情報を入力不可にする
                if (this.maniFlag == 2 && (isFixedFirstElecMani || isExecutingFirstElecMani)
                    && (existLastSbnEndDate && existLastSbnGyousha && existLastSbnGenba))
                {
                    //最終処分終了日
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].ReadOnly = true;

                    //最終処分業者CD
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].ReadOnly = true;

                    //最終処分現場CD 
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].ReadOnly = true;

                    this.existAllLastSbnInfo = true;
                }

                //明細システムID
                if (dr["DETAIL_SYSTEM_ID"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells["DetailSystemID"].Value = dr["DETAIL_SYSTEM_ID"].ToString();
                }

                //タイムスタンプ
                if (dr["TIME_STAMP"].ToString() != string.Empty)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells["TimeStamp"].Value = dr["TIME_STAMP"].ToString();
                }
            }

            //20250402
            var colSyobun = this.form.cdgrid_Jisseki.Columns[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate];

            if (this.form.cdate_ShobunShuryoDate.Value == null)
            {
                colSyobun.DefaultCellStyle.BackColor = Color.White;
                //colSyobun.DefaultCellStyle.ForeColor = Color.Black;
                colSyobun.ReadOnly = false;
            }
            else
            {
                colSyobun.DefaultCellStyle.BackColor = Color.Gainsboro;
                colSyobun.DefaultCellStyle.ForeColor = Color.DimGray;
                colSyobun.ReadOnly = true;
            }

            //処分終了年月日
            if (strEndDate != string.Empty)
            {
                this.form.cdate_SyobunSyo.Value = Convert.ToDateTime(strEndDate);
            }

            //最終処分終了年月日
            if (strJyutakuDate != string.Empty)
            {
                this.form.cdate_SaisyuSyobunDate.Value = Convert.ToDateTime(strJyutakuDate);
            }

            // 二次マニの場合は紐付けた一次電マニが既に最終処分終了報告されていた場合
            // 最終処分情報を入力不可にする
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL start
            //if (this.maniFlag == 2 && isFixedFirstElecMani)
            //{
            //    this.form.cdgrid_Jisseki.AllowUserToAddRows = false;
            //}
            // 2016.11.23 chinkeigen マニ入力と一覧 #101092 DEL end

            //最終行の単位は自動セットしない
            if (!(this.form.canTxt_JissekiTaniCd.Text == null || this.form.canTxt_JissekiTaniCd.Text == ""))
            {
                int intRecordC = (int)this.form.cdgrid_Jisseki.RowCount;
                if (intRecordC > 1)
                {
                    this.form.cdgrid_Jisseki.Rows[intRecordC - 1].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = "";
                    this.form.cdgrid_Jisseki.Rows[intRecordC - 1].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = "";
                }
            }

            LogUtility.DebugMethodEnd(dt);
        }

        #endregion

        #region ポップアップの条件設定
        /// <summary>
        /// 排出検索ポップアップの条件設定
        /// </summary>
        public void SetFilteringHaishutu()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cantxt_HaisyutuGyousyaCd.Text != string.Empty)
            {
                //条件変更
                this.form.cantxt_HaisyutuJigyoubaName.PopupSearchSendParams.Clear();
                this.form.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHA_CD";
                dto.Value = this.form.cantxt_HaisyutuGyousyaCd.Text;
                this.form.cantxt_HaisyutuJigyoubaName.PopupSearchSendParams.Add(dto);
                this.form.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "HAISHUTSU_NIZUMI_GENBA_KBN";
                dto.Value = "True";
                this.form.cantxt_HaisyutuJigyoubaName.PopupSearchSendParams.Add(dto);

                this.form.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams.Add(dto);
            }
            else
            {
                //条件変更
                this.form.cantxt_HaisyutuJigyoubaName.PopupSearchSendParams.Clear();
                this.form.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "HAISHUTSU_NIZUMI_GENBA_KBN";
                dto.Value = "True";
                this.form.cantxt_HaisyutuJigyoubaName.PopupSearchSendParams.Add(dto);
                this.form.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams.Add(dto);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終検索ポップアップの条件設定
        /// </summary>
        public void SetFilteringSaishuu()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cantxt_SaisyuGyousyaCd.Text != string.Empty)
            {
                //条件変更
                this.form.cantxt_SaisyuGyousyaNameCd.PopupSearchSendParams.Clear();
                this.form.casbtn_SaisyuGyousya.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHA_CD";
                dto.Value = this.form.cantxt_SaisyuGyousyaCd.Text;
                this.form.cantxt_SaisyuGyousyaNameCd.PopupSearchSendParams.Add(dto);
                this.form.casbtn_SaisyuGyousya.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SAISHUU_SHOBUNJOU_KBN";
                dto.Value = "True";
                this.form.cantxt_SaisyuGyousyaNameCd.PopupSearchSendParams.Add(dto);
                this.form.casbtn_SaisyuGyousya.PopupSearchSendParams.Add(dto);

                this.form.ccbx_SaisyuKisai.Checked = true;
            }
            else
            {
                //条件変更
                this.form.cantxt_SaisyuGyousyaNameCd.PopupSearchSendParams.Clear();
                this.form.casbtn_SaisyuGyousya.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SAISHUU_SHOBUNJOU_KBN";
                dto.Value = "True";
                this.form.cantxt_SaisyuGyousyaNameCd.PopupSearchSendParams.Add(dto);
                this.form.casbtn_SaisyuGyousya.PopupSearchSendParams.Add(dto);

            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分検索ポップアップの条件設定
        /// </summary>
        public void SetFilteringShobun()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cantxt_SyobunJyutakuNameCd.Text != string.Empty)
            {
                //条件変更
                this.form.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Clear();
                this.form.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHA_CD";
                dto.Value = this.form.cantxt_SyobunJyutakuNameCd.Text;
                this.form.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto);
                this.form.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SHOBUN_NIOROSHI_GENBA_KBN";
                dto.Value = "True";
                this.form.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto);
                this.form.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto);

            }
            else
            {
                //条件変更
                this.form.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Clear();
                this.form.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SHOBUN_NIOROSHI_GENBA_KBN";
                dto.Value = "True";
                this.form.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams.Add(dto);
                this.form.cbtn_UnpanJyugyobaSan.PopupSearchSendParams.Add(dto);

            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分検索ポップアップの条件設定
        /// </summary>
        public void SetFilteringShobun2()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cantxt_SaisyuSyobunGyoCd.Text != string.Empty)
            {
                //条件変更
                this.form.cantxt_SaisyuSyobunbaCD.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "GYOUSHA_CD";
                dto.Value = this.form.cantxt_SaisyuSyobunGyoCd.Text;
                this.form.cantxt_SaisyuSyobunbaCD.PopupSearchSendParams.Add(dto);
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SAISHUU_SHOBUNJOU_KBN";
                dto.Value = "True";
                this.form.cantxt_SaisyuSyobunbaCD.PopupSearchSendParams.Add(dto);

            }
            else
            {
                //条件変更
                this.form.cantxt_SaisyuSyobunbaCD.PopupSearchSendParams.Clear();
                r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto = new r_framework.Dto.PopupSearchSendParamDto();
                dto.And_Or = r_framework.Const.CONDITION_OPERATOR.AND;
                dto.KeyName = "SAISHUU_SHOBUNJOU_KBN";
                dto.Value = "True";
                this.form.cantxt_SaisyuSyobunbaCD.PopupSearchSendParams.Add(dto);

            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 廃棄物種類のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridHaiki(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                var value = Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value);

                if (string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.NijiManiNo].Value)))
                    {
                        ret = 1;
                        this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value = "";
                    }
                    else
                    {
                        ret = 2;
                        this.form.messageShowLogic.MessageBoxShow("E106");
                    }
                    return ret;
                }

                if (value == this.form.preValue && !this.form.isError)
                {
                    return ret;
                }

                this.form.isError = false;
                this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value =
                this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value.ToString().Trim().PadLeft(4, '0').ToUpper();

                GetHaikiShuruiDtoCls search = new GetHaikiShuruiDtoCls();
                search.HAIKI_KBN_CD = FormHaikiKbn;
                search.HAIKI_SHURUI_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value.ToString();

                var dt = GetHaikiShuruiDao.GetDataForEntity(search);

                if (dt.Rows.Count > 0)
                {
                    this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value = dt.Rows[0]["HAIKI_SHURUI_CD"].ToString();
                    this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value = dt.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"].ToString();
                }
                else
                {
                    ret = 2;
                    this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridHaiki", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 割合(％)のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridWariai(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                //総量100%チェック
                if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value)))
                {
                    ret = 1;
                    return ret;
                }

                decimal fsuu = 0;
                fsuu = Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value);

                decimal fcnt = 0;
                for (int i = 0; i < this.form.cdgrid_Jisseki.RowCount; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value)))
                    {
                        fcnt += Convert.ToDecimal(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value);
                    }
                }

                if (fcnt > 100 || fsuu > 100)
                {
                    ret = 2;
                    this.form.messageShowLogic.MessageBoxShow("E039");
                    return ret;
                }

                decimal JissekiSuryo = 0;
                if (this.form.cntxt_JissekiSuryo.Text != string.Empty)
                {
                    JissekiSuryo = Convert.ToDecimal(this.form.cntxt_JissekiSuryo.Text);
                }

                this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value
                    = this.mlogic.GetSuuryoRound((JissekiSuryo * fsuu / 100), this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridWariai", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 数量のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridSuryo(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value)))
                {
                    ret = 1;
                    return ret;
                }

                decimal d = 0;
                String Suryo = (Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["Suryo"].Value).Replace(",", ""));
                //decimalに変換できるか確かめる
                if (decimal.TryParse(Convert.ToString(Suryo), out d) == false)
                {
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridSuryo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 単位のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridTaniCd(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value)))
                {
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridTaniCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 換算後数量のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridKansangoSuryo(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)))
                {
                    ret = 1;
                    return ret;
                }

                decimal d = 0;
                String Suryo = (Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["KansangoSuryo"].Value).Replace(",", ""));
                //decimalに変換できるか確かめる
                if (decimal.TryParse(Convert.ToString(Suryo), out d) == false)
                {
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridKansangoSuryo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 減容後数量のチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public int ChkGridGenyoyugoTotalSuryo(int iRow)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value)))
                {
                    ret = 1;
                    return ret;
                }

                decimal d = 0;
                String Suryo = (Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["GenyoyugoTotalSuryo"].Value).Replace(",", ""));
                //decimalに変換できるか確かめる
                if (decimal.TryParse(Convert.ToString(Suryo), out d) == false)
                {
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridGenyoyugoTotalSuryo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者) ０：正常　1:空　2：エラー
        /// </summary>
        /// <param name="iRow">チェックカラム名称</param>
        /// <returns></returns>
        public int ChkGridGyosya(int iRow)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(iRow);

                var value = Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value);
                if (string.IsNullOrEmpty(value))
                {
                    this.form.isError = false;
                    return 1;
                }

                if (value == this.form.preValue && !this.form.isError)
                {
                    this.form.isError = false;
                    return 0;
                }

                this.form.isError = false;

                this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value =
                    this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString().PadLeft(6, '0');

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString();
                Serch.GYOUSHAKBN_MANI = "True";
                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenbaAll(Serch);
                if (dt.Rows.Count > 0)
                {
                    SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.cdate_KohuDate.Text) && (DateTime.TryParse(this.form.cdate_KohuDate.Text, out date)))
                    {
                        tekiyou = date;
                    }
                    SqlDateTime from = SqlDateTime.Null;
                    SqlDateTime to = SqlDateTime.Null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SHOBUN_NIOROSHI_GYOUSHA_KBN"].ToString() == "True")
                        {
                            from = SqlDateTime.Null;
                            to = SqlDateTime.Null;
                            string begin = Convert.ToString(dt.Rows[0]["TEKIYOU_BEGIN"]);
                            string end = Convert.ToString(dt.Rows[0]["TEKIYOU_END"]);
                            if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                            {
                                from = date;
                            }
                            if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                            {
                                to = date;
                            }

                            if ((from.IsNull && to.IsNull)
                                || (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                                || (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                                || (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                    && tekiyou.CompareTo(to) <= 0))
                            {
                                ret = 0;
                                this.form.isError = false;
                                return 0;
                            }
                        }
                    }
                }

                this.form.isError = true;
                this.form.messageShowLogic.MessageBoxShow("E020", "業者");
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridGyosya", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.form.isError = true;
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 業者のセット
        /// </summary>
        public bool SetGridGyosya(int iRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(iRow);

                var Serch = new CommonGyoushaDtoCls();
                Serch.GYOUSHA_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString();
                Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "True";
                Serch.GYOUSHAKBN_MANI = "True";

                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGyousha(Serch);
                if (dt.Rows.Count > 0)
                {

                    this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaName"].Value = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGridGyosya", ex);
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

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者) 
        /// </summary>
        /// <param name="iRow">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkGridJigyouba(int iRow)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart();

                var value = Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value);
                if (string.IsNullOrEmpty(value))
                {
                    this.form.isError = false;
                    return 1;
                }

                if (value == this.form.preValue && !this.form.isError)
                {
                    this.form.isError = false;
                    return 0;
                }

                this.form.isError = false;

                //20150630 #5005 hoanghm start
                //業者CDを入力しないで現場CDを手入力した時はアラートを表示しフォーカスアウトできないようにする。
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value)))
                {
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                    this.form.messageShowLogic.MessageBoxShow("E051", "最終処分業者");
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                    this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value = string.Empty;
                    if (this.form.cdgrid_Jisseki.EditingControl != null)
                    {
                        this.form.cdgrid_Jisseki.EditingControl.Text = string.Empty;
                    }
                    this.form.isError = true;
                    return 2;
                }
                //20150630 #5005 hoanghm end

                this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value =
                    this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value.ToString().PadLeft(6, '0');

                var Serch = new CommonGenbaDtoCls();
                if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value)))
                {
                    Serch.GYOUSHA_CD = string.Empty;
                }
                else
                {
                    Serch.GYOUSHA_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString();
                }
                Serch.GENBA_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value.ToString();
                Serch.SAISHUU_SHOBUNJOU_KBN = "True";
                Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "True";
                Serch.GYOUSHAKBN_MANI = "True";

                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        this.form.isError = true;
                        break;

                    case 1:
                        SqlDateTime tekiyou = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.cdate_KohuDate.Text) && (DateTime.TryParse(this.form.cdate_KohuDate.Text, out date)))
                        {
                            tekiyou = date;
                        }
                        SqlDateTime from = SqlDateTime.Null;
                        SqlDateTime to = SqlDateTime.Null;
                        string begin = Convert.ToString(dt.Rows[0]["TEKIYOU_BEGIN"]);
                        string end = Convert.ToString(dt.Rows[0]["TEKIYOU_END"]);
                        if (!string.IsNullOrWhiteSpace(begin) && (DateTime.TryParse(begin, out date)))
                        {
                            from = date;
                        }
                        if (!string.IsNullOrWhiteSpace(end) && (DateTime.TryParse(end, out date)))
                        {
                            to = date;
                        }

                        if ((from.IsNull && to.IsNull)
                            || (from.IsNull && !to.IsNull && tekiyou.CompareTo(to) <= 0)
                            || (!from.IsNull && to.IsNull && tekiyou.CompareTo(from) >= 0)
                            || (!from.IsNull && !to.IsNull && tekiyou.CompareTo(from) >= 0
                                && tekiyou.CompareTo(to) <= 0))
                        {
                            this.form.isError = false;
                            ret = 0;
                            return ret;
                        }
                        this.form.isError = true;
                        this.form.messageShowLogic.MessageBoxShow("E020", "現場");
                        break;

                    default:
                        this.form.isError = true;
                        this.form.messageShowLogic.MessageBoxShow("E034", "処分事業者");
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.form.isError = true;
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場のセット
        /// </summary>
        public void SetGridJigyouba(int iRow)
        {
            LogUtility.DebugMethodStart(iRow);

            var Serch = new CommonGenbaDtoCls();
            if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value)))
            {
                Serch.GYOUSHA_CD = string.Empty;
            }
            else
            {
                Serch.GYOUSHA_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value.ToString();
            }
            Serch.GENBA_CD = this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value.ToString();
            Serch.SAISHUU_SHOBUNJOU_KBN = "True";

            this.SearchResult = new DataTable();
            DataTable dt = mlogic.GetGenba(Serch);
            if (dt.Rows.Count > 0)
            {
                this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaCd"].Value = dt.Rows[0]["GYOUSHA_CD"].ToString();
                this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuBasyoCd"].Value = dt.Rows[0]["GENBA_CD"].ToString();
                this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuSyobunBasyo"].Value = dt.Rows[0]["GENBA_NAME"].ToString();
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタから住所情報を取得してTextBoxに設定(Dgv用)
        /// </summary>
        /// <param name="gyoshaCd">業者CD</param>
        /// <param name="gyoshaName">業者名</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetAddressGyoushaForDgv(int iRow)
        {
            LogUtility.DebugMethodStart(iRow);

            string txt_gyoshaCd = string.Empty;

            if (this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value != null)
            {
                txt_gyoshaCd = this.form.cdgrid_Jisseki.Rows[iRow].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value.ToString();
            }

            if (txt_gyoshaCd == string.Empty)
            {
                return 1;
            }

            var Serch = new CommonGyoushaDtoCls();
            Serch.GYOUSHA_CD = txt_gyoshaCd;

            this.SearchResult = new DataTable();
            DataTable dt = mlogic.GetGyousha(Serch);
            if (dt.Rows.Count > 0)
            {
                this.form.cdgrid_Jisseki.Rows[iRow].Cells["SaisyuGyosyaName"].Value = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                return 0;
            }

            LogUtility.DebugMethodEnd(iRow);
            return 2;
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        /// <param name="shaincd">社員CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkUntensha(object shaincd)
        {
            try
            {
                LogUtility.DebugMethodStart(shaincd);

                TextBox txt1 = (TextBox)shaincd;
                if (txt1.Text == string.Empty)
                {
                    return 1;
                }

                GetUntenshaDtoCls Serch = new GetUntenshaDtoCls();
                Serch.SHAIN_CD = txt1.Text;
                this.SearchResult = new DataTable();
                DataTable dt = GetUntenshaDao.GetDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    return 0;
                }

                this.form.messageShowLogic.MessageBoxShow("E020", "運転者");
                txt1.Focus();
                txt1.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkUntensha", ex);
                if (ex is SQLRuntimeException)
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
                LogUtility.DebugMethodEnd(shaincd);
            }
            return 2;
        }

        ///// <summary>
        ///// 処分担当者チェック
        ///// </summary>
        ///// <param name="shaincd">社員CD</param>
        ///// <returns>０：正常　1:空　2：エラー</returns>
        //public int ChkShobunTantousha(object shaincd, object shainname)
        //{
        //    LogUtility.DebugMethodStart(shaincd, shainname);

        //    TextBox txt1 = (TextBox)shaincd;
        //    TextBox txt2 = (TextBox)shainname;
        //    if (txt1.Text == string.Empty)
        //    {
        //        txt2.Text = "";
        //        return 1;
        //    }

        //    GetShobunTantoushaDtoCls Serch = new GetShobunTantoushaDtoCls();
        //    Serch.SHAIN_CD = txt1.Text;
        //    this.SearchResult = new DataTable();
        //    DataTable dt = GetShobunTantoushaDao.GetDataForEntity(Serch);
        //    if (dt.Rows.Count > 0)
        //    {
        //        txt2.Text = dt.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
        //        return 0;
        //    }

        //    this.form.messageShowLogic.MessageBoxShow("E020", "処分担当者");
        //    txt1.Focus();
        //    txt1.SelectAll();
        //    LogUtility.DebugMethodEnd(shaincd, shainname);
        //    return 2;
        //}

        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <param name="sharyocd">車輌CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkCarData(object gyoshacd, object shasyucd, object shasyuname, object sharyocd, object sharyoname)
        {
            try
            {
                LogUtility.DebugMethodStart(gyoshacd, shasyucd, shasyuname, sharyocd, sharyoname);

                TextBox txt_gyoshacd = (TextBox)gyoshacd;
                TextBox txt_shasyucd = (TextBox)shasyucd;
                TextBox txt_shasyuname = (TextBox)shasyuname;
                TextBox txt_sharyocd = (TextBox)sharyocd;
                TextBox txt_sharyoname = (TextBox)sharyoname;

                if (txt_sharyocd.Text == string.Empty)
                {
                    return 1;
                }

                GetCarDataDtoCls Serch = new GetCarDataDtoCls();
                Serch.SHARYOU_CD = txt_sharyocd.Text;
                Serch.GYOUSHA_CD = txt_gyoshacd.Text;
                Serch.SHASYU_CD = txt_shasyucd.Text;
                SqlDateTime kofuDate = SqlDateTime.Null;
                if (!string.IsNullOrEmpty(this.form.cdate_KohuDate.Text))
                {
                    kofuDate = SqlDateTime.Parse(this.form.cdate_KohuDate.Value.ToString());
                }
                Serch.KOFU_DATE = kofuDate;

                this.SearchResult = new DataTable();
                DataTable dt = GetCarDataDao.GetDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    if (txt_gyoshacd.Text.Trim() == "")
                    {
                        txt_gyoshacd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    }
                    if (txt_shasyucd.Text.Trim() == "")
                    {
                        txt_shasyucd.Text = dt.Rows[0]["SHASYU_CD"].ToString();
                        txt_shasyuname.Text = dt.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                    }
                    txt_sharyocd.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                    txt_sharyoname.Text = dt.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                    return 0;
                }

                this.form.messageShowLogic.MessageBoxShow("E020", "車輌");
                txt_sharyocd.Focus();
                txt_sharyocd.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkCarData", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(gyoshacd, shasyucd, shasyuname, sharyocd, sharyoname);
            }
            return 2;
        }

        /// <summary>
        /// 送信保留最終処分報告ポップアップを呼び出し
        /// </summary>
        public void CallSousinnHoryuuPopuUp(bool exeFlg)
        {
            bool catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(exeFlg);

                // 最終処分保留
                List<T_LAST_SBN_SUSPEND> lastSbnSusPendList = new List<T_LAST_SBN_SUSPEND>();
                // キュー情報
                List<QUE_INFO> queList = new List<QUE_INFO>();
                // D12 2次マニフェスト情報
                List<DT_D12> manifastList = new List<DT_D12>();
                // D13 最終処分終了日・事業場情報
                List<DT_D13> jigyoubaList = new List<DT_D13>();
                // マニフェスト目次情報
                List<DT_MF_TOC> mokujiList = new List<DT_MF_TOC>();

                if (this.form.cdgrid_Jisseki.CurrentRow.Cells["DetailSystemID"].Value == null)
                {
                    form.messageShowLogic.MessageBoxShow("E051", "実績タブの明細から処理対象の廃棄物種類CD");
                    return;
                }

                string detailSystemId = this.form.cdgrid_Jisseki.CurrentRow.Cells["DetailSystemID"].Value.ToString();

                if (!this.mlogic.CreateSousinnHoryuuPopuUpParam(exeFlg, detailSystemId, 2,
                    out lastSbnSusPendList, out queList, out manifastList, out jigyoubaList, out mokujiList))
                {
                    return;
                }

                // 登録対象存在する場合
                if ((lastSbnSusPendList.Count > 0 || queList.Count > 0
                    || manifastList.Count > 0 || jigyoubaList.Count > 0
                    || mokujiList.Count > 0))
                {
                    if (exeFlg)
                    {
                        // DBの情報で最終処分終了報告を行うため、ユーザへ知らせる。
                        string confMsg = "登録済みのデータで最終処分報告を行います。\n未登録のデータは破棄されますがよろしいですか。";

                        if (!(form.messageShowLogic.MessageBoxShowConfirm(confMsg) == DialogResult.Yes))
                        {
                            return;
                        }
                    }

                    // 送信保留最終処分報告ポップアップを呼び出し
                    var callForm = new Shougun.Core.PaperManifest.SousinnHoryuuPopup.UIForm();
                    callForm.Params = new object[6];
                    // 最終処分保留
                    callForm.Params[0] = lastSbnSusPendList;
                    // キュー情報
                    callForm.Params[1] = queList;
                    // D12 2次マニフェスト情報
                    callForm.Params[2] = manifastList;
                    // D13 最終処分終了日・事業場情報
                    callForm.Params[3] = jigyoubaList;
                    // マニフェスト目次情報
                    callForm.Params[4] = mokujiList;
                    if (exeFlg)
                    {
                        // 最終処分終了報告の場合
                        callForm.Params[5] = "1";
                    }
                    else
                    {
                        // 最終処分終了報告の取消場合
                        callForm.Params[5] = "2";

                    }
                    // 画面表示
                    callForm.ShowDialog();

                    if (exeFlg)
                    {
                        // 画面をリフレッシュしないと、最終処分情報が更新されてしまい、報告した情報と差異がでてしまう。
                        form.ClearScreen(null);
                        this.WindowInit("SetUpdateFrom", out catchErr);
                        if (catchErr) { return; }
                    }
                }
                else
                {
                    if (exeFlg)
                    {
                        form.messageShowLogic.MessageBoxShow("W002", "最終処分終了報告");
                    }
                    else
                    {
                        form.messageShowLogic.MessageBoxShow("W002", "最終処分終了報告の取消");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CallSousinnHoryuuPopuUp", ex);
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
                LogUtility.DebugMethodEnd(exeFlg);
            }
        }

        /// <summary>
        /// 数値セット
        /// </summary>
        public bool SetSuu()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                for (int i = 0; i < this.form.cdgrid_Jisseki.RowCount - 1; i++)
                {
                    //数量
                    switch (this.ChkGridWariai(i))
                    {
                        case 0://正常
                            break;

                        case 1://空
                            return ret;

                        case 2://エラー
                            return ret;
                    }
                    //換算後数量・減容後数量
                    if (!this.SetKansanti(i)) { return ret; }
                    if (!this.SetGenyouti(i)) { return ret; }
                }
                //合計
                if (!this.SetTotal()) { return ret; }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSuu", ex);
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
        /// イベント紐付
        /// </summary>
        private void GyousyaCd_Validated_EventInit()
        {
            this.form.cantxt_HaisyutuGyousyaCd.Validated += new EventHandler(GyousyaCd_Validated);
            this.form.cantxt_SaisyuGyousyaCd.Validated += new EventHandler(GyousyaCd_Validated);
            this.form.cantxt_SyobunJyutakuNameCd.Validated += new EventHandler(GyousyaCd_Validated);
            this.form.cantxt_TumiGyoCd.Validated += new EventHandler(GyousyaCd_Validated);
            this.form.cantxt_SaisyuSyobunGyoCd.Validated += new EventHandler(GyousyaCd_Validated);
        }

        /// <summary>
        /// 業者のテキスト変更は、現場をクリアする
        /// 汎用イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GyousyaCd_Validated(object sender, EventArgs e)
        {
            if (!this.form.isChanged(sender)) //変更なしの場合
            {
                return; //何もしない
            }

            //変更有の場合 現場をクリア
            if (sender == this.form.cantxt_HaisyutuGyousyaCd)
            {
                this.HaisyutuJigyoubaDel();
            }
            else if (sender == this.form.cantxt_SaisyuGyousyaCd)
            {
                this.SaisyuSyobunDel("cantxt_SaisyuGyousyaNameCd");
            }
            else if (sender == this.form.cantxt_SyobunJyutakuNameCd)
            {
                this.UnpanJyugyobaDel();
            }
            else if (sender == this.form.cantxt_TumiGyoCd)
            {
                this.TumiHokaDel("cantxt_TumiHokaNameCd");
            }
            else if (sender == this.form.cantxt_SaisyuSyobunGyoCd)
            {
                this.SaisyuBasyoSyozaiDel("cantxt_SaisyuSyobunbaCD");
            }
        }


        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="insertVal">登録項目</param>
        public bool KinsokuMoziCheck(string insertVal)
        {
            Validator v = new Validator();

            if (!v.isJWNetValidShiftJisCharForSign(insertVal))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E071", "該当箇所");
                return false;
            }
            return true;
        }

        /// <summary>
        /// データ移動処理
        /// </summary>
        internal void SetMoveData()
        {

            if (this.form.moveData_flg)
            {
                this.form.cantxt_TorihikiCd.Text = this.form.moveData_torihikisakiCd;
                M_TORIHIKISAKI torihikisaki = this.torihikisakiDao.GetDataByCd(this.form.cantxt_TorihikiCd.Text);
                if (torihikisaki != null)
                {
                    this.form.ctxt_TorihikiName.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                }
                this.form.cantxt_HaisyutuGyousyaCd.Text = this.form.moveData_gyousyaCd;
                this.form.cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod();
                this.form.cantxt_HaisyutuJigyoubaName.Text = this.form.moveData_genbaCd;
                this.form.cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod();
            }
        }

        /// <summary>
        ///  画面の連携項目をクリアする
        /// </summary>
        private void ClearGamenItem()
        {
            this.form.cdate_KohuDate.Text = string.Empty;
            this.form.cantxt_TorihikiCd.Text = string.Empty;
            this.form.ctxt_TorihikiName.Text = string.Empty;
            this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);
            this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);
            this.form.cantxt_UnpanJyutaku1NameCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);
            this.form.cantxt_TumiGyoCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
            this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
            this.form.cantxt_TumiHokaNameCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
            this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
            this.form.cantxt_UnpanJyuCd1.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);
            this.form.cantxt_Jyutaku1Syasyu.Text = string.Empty;
            ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);
            this.form.cantxt_Jyutaku1SyaNo.Text = string.Empty;
            this.form.ctxt_Jyutaku1SyaNo.Text = string.Empty;
            this.form.cantxt_UnpanJyuUntenCd1.Text = string.Empty;
            ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
            this.form.cantxt_SyobunJyuCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);
            this.form.cantxt_SyobunSyoCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
            this.form.cdate_UnpanJyu1.Text = string.Empty;
            this.form.cdate_SyobunJyu.Text = string.Empty;
            this.SyobunJyuCDDel();
            this.form.cdgrid_Jisseki.Rows.Clear();
        }

        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 start‏
        /// <summary>
        /// 機能間連携（Data取得）
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="renkeiNo">連携番号</param>
        /// <param name="renkeiMeisaigyo">明細行</param>
        /// <param name="isReset">リセットフラグ</param>
        public String GetRenkeiData(object denshuKbn, object renkeiNo, object renkeiMeisaigyo, Boolean isReset, int checkModeKbn)
        {
            LogUtility.DebugMethodStart(denshuKbn, renkeiNo, renkeiMeisaigyo, isReset, checkModeKbn);
            TextBox txt1 = (TextBox)denshuKbn;
            TextBox txt2 = (TextBox)renkeiNo;
            TextBox txt3 = (TextBox)renkeiMeisaigyo;
            int count = 0;
            GetKenpaiDataDtoCls search = new GetKenpaiDataDtoCls();
            search.RENKEI_NUMBER = Convert.ToDecimal(txt2.Text);
            search.RENKEI_ROW_NO = string.IsNullOrEmpty(txt3.Text) ? (int?)null : Convert.ToInt32(txt3.Text);
            search.RENKEI_MANI_KBN = maniFlag;
            search.CHK_MODE_KBN = checkModeKbn;
            DataTable dt;
            this.SearchResult = new DataTable();
            Boolean isSonzai = false;
            M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
            M_GYOUSHA gyousha = new M_GYOUSHA();
            M_GENBA genba = new M_GENBA();
            bool tsumikaeKbn = false;

            switch (Convert.ToInt32(txt1.Text))
            {
                case (int)DENSHU_KBN.UKEIRE: //受入
                    if (maniFlag == 1)
                    {
                        //※1次マニフェスト
                        if (this.form.ismobile_mode && this.form.Renkei_Mode_2.Checked)
                        {
                            dt = GetKenpaiDataDao.GetUkeireJissekiForRenkei(search);
                        }
                        else
                        {
                            dt = GetKenpaiDataDao.GetUkeireForRenkei(search);
                        }
                        renkei_dt = dt;
                        // 20140617 katen 不具合No.4712 start‏
                        if (dt.Rows.Count > 0 && dt.Rows[0]["MANIFEST_SHURUI_CD"] != DBNull.Value && Convert.ToInt16(dt.Rows[0]["MANIFEST_SHURUI_CD"]) != 1)
                        {
                            return "E175";
                        }
                        // 20140617 katen 不具合No.4712 end‏
                        // 20140611 ria No.679 伝種区分連携 start
                        if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                        {
                            if (!this.CheckManiData())
                            {
                                isSonzai = true;
                            }
                        }
                        // 20140611 ria No.679 伝種区分連携 end
                        count = dt.Rows.Count;
                        if (dt.Rows.Count > 0 && isReset && !isSonzai)
                        {
                            if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                            {
                                torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                            }
                            if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value)
                            {
                                gyousha = this.GetGyoushaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString());
                            }
                            if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["NIOROSHI_GENBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString(), dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString(), out tsumikaeKbn);
                            }

                            //画面の交付年月日に「受入入力.伝票日付」をセットする
                            this.form.cdate_KohuDate.Value = dt.Rows[0]["DENPYOU_DATE"];

                            if (torihikisaki != null)
                            {
                                //画面の取引先に「受入入力.取引先CD」をセットする
                                this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                                //画面の取引先に「受入入力.取引先名」をセットする
                                this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                            }

                            //画面の排出事業者に「受入入力.業者CD」をセットする(*1)
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                            //画面の排出事業場に「受入入力.現場CD」をセットする(*2)
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                            //画面の運搬受託者（１）に「受入入力.運搬業者CD」をセットする（*3）
                            this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に「受入入力.荷降業者CD」をセットする（*4）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                            }
                            else
                            {
                                //画面の処分受託者に「受入入力.荷降業者CD」をセットする（*4）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                            }

                            if (genba != null && genba.TSUMIKAEHOKAN_KBN)
                            {
                                //画面の積替え又は保管の名称に「受入入力.荷降現場CD」をセットする（*5）
                                this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                            }
                            else
                            {
                                //画面の運搬先の事業場に「受入入力.荷降現場CD」をセットする（*5）
                                this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                            }

                            //グリッドに廃棄物種類データ設定
                            this.SetGridDataForRenkei(dt);
                            if (this.form.ismobile_mode && this.form.Renkei_Mode_2.Checked)
                            {
                                this.SetRenkeiHaikiSyuruiToForm(dt);
                            }

                            //画面の運搬の受託（１）上に「受入入力.運搬業者CD」をセットする（*6）
                            this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);

                            //画面の運搬受託者（１）の車種に「受入入力.車種CD」をセットする（*7）
                            this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                            //ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);
                            this.form.ctxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_NAME"].ToString();

                            //画面の運搬受託者（１）の車輌に「受入入力.車輌CD」をセットする
                            this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                            //画面の運搬受託者（１）の車輌に「受入入力.車輌名」をセットする
                            this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                            //画面の運搬の受託（１）下に「受入入力.運転者CD」をセットする（*8）
                            this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                            //ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
                            this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();

                            if (gyousha != null
                                && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                                || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                                || !tsumikaeKbn))
                            {
                                //画面の処分の受託（受領）上に「受入入力.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                                //画面の処分の受託（処分）上に「受入入力.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                            }
                            else
                            {
                                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                                this.form.ctxt_SyobunJyuName.Text = string.Empty;
                                this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                                this.form.ctxt_SyobunSyoName.Text = string.Empty;
                            }

                            //画面の運搬の受託（１）に「受入入力.伝票日付」をセットする
                            this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["DENPYOU_DATE"];

                            //画面の処分の受託（受領）に「受入入力.伝票日付」をセットする
                            if (!tsumikaeKbn)
                            {
                                this.form.cdate_SyobunJyu.Value = dt.Rows[0]["DENPYOU_DATE"];
                            }
                            else
                            {
                                // 画面の処分の受託（受領）をクリア
                                this.SyobunJyuCDDel();
                            }
                        }
                    }
                    break;
                case (int)DENSHU_KBN.SHUKKA: //出荷
                    if (maniFlag == 2)
                    {
                        //※2次マニフェスト
                        dt = GetKenpaiDataDao.GetShukaForRenkei(search);
                        renkei_dt = dt;
                        count = dt.Rows.Count;
                        // 20140617 katen 不具合No.4712 start‏
                        if (dt.Rows.Count > 0 && dt.Rows[0]["MANIFEST_SHURUI_CD"] != DBNull.Value && Convert.ToInt16(dt.Rows[0]["MANIFEST_SHURUI_CD"]) != 1)
                        {
                            return "E175";
                        }
                        // 20140617 katen 不具合No.4712 end‏
                        // 20140611 ria No.679 伝種区分連携 start
                        if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                        {
                            if (!this.CheckManiData())
                            {
                                isSonzai = true;

                            }
                        }
                        // 20140611 ria No.679 伝種区分連携 end
                        if (dt.Rows.Count > 0 && isReset && !isSonzai)
                        {
                            if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                            {
                                torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                            }
                            if (dt.Rows[0]["GYOUSHA_CD"] != DBNull.Value)
                            {
                                gyousha = this.GetGyoushaEntity(dt.Rows[0]["GYOUSHA_CD"].ToString());
                            }
                            if (dt.Rows[0]["GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["GENBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["GYOUSHA_CD"].ToString(), dt.Rows[0]["GENBA_CD"].ToString(), out tsumikaeKbn);
                            }

                            //画面の交付年月日に「出荷入力.伝票日付」をセットする
                            this.form.cdate_KohuDate.Value = dt.Rows[0]["DENPYOU_DATE"];

                            if (torihikisaki != null)
                            {
                                //画面の取引先に「出荷入力.取引先CD」をセットする
                                this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                                //画面の取引先に「出荷入力.取引先名」をセットする
                                this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                            }
                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に「出荷入力.業者CD」をセットする（*1）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                            }
                            else
                            {
                                //画面の処分受託者に「出荷入力.業者CD」をセットする（*1）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                            }

                            if (genba != null)
                            {
                                if (genba.TSUMIKAEHOKAN_KBN)
                                {
                                    //画面の積替え又は保管の名称に「出荷入力.現場CD」をセットする（*2）
                                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                                }
                                else
                                {
                                    //画面の運搬先の事業場に「出荷入力.現場CD」をセットする（*2）
                                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                                }
                            }

                            //画面の運搬受託者（１）に「出荷入力.運搬業者CD」をセットする（*3）
                            this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                            //画面の排出事業者に「出荷入力.荷積業者CD」をセットする（*10）
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                            //画面の排出事業場に「出荷入力.荷積現場CD」をセットする（*11）
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                            //グリッドに廃棄物種類データ設定
                            this.SetGridDataForRenkei(dt);

                            //画面の運搬の受託（１）上に「出荷入力.運搬業者CD」をセットする（*6）
                            this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);

                            //画面の運搬受託者（１）の車種に「出荷入力.車種CD」をセットする（*7）
                            this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                            //ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);
                            this.form.ctxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_NAME"].ToString();


                            //画面の運搬受託者（１）の車輌に「出荷入力.車輌CD」をセットする
                            this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                            //画面の運搬受託者（１）の車輌に「出荷入力.車輌名」をセットする
                            this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                            //画面の運搬の受託（１）下に「出荷入力.運転者CD」をセットする（*8）
                            this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                            //ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
                            this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();

                            if (gyousha != null
                               && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                               || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                               || !tsumikaeKbn))
                            {
                                //画面の処分の受託（受領）上に「出荷入力.業者CD」をセットする（*12）
                                this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                                //画面の処分の受託（処分）上に「出荷入力.業者CD」をセットする（*12）
                                this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                            }
                            else
                            {
                                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                                this.form.ctxt_SyobunJyuName.Text = string.Empty;
                                this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                                this.form.ctxt_SyobunSyoName.Text = string.Empty;
                            }

                            //画面の運搬の受託（１）に「出荷入力.伝票日付」をセットする
                            this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["DENPYOU_DATE"];

                            //画面の処分の受託（受領）に「出荷入力.伝票日付」をセットする
                            if (!tsumikaeKbn)
                            {
                                this.form.cdate_SyobunJyu.Value = dt.Rows[0]["DENPYOU_DATE"];
                            }
                            else
                            {
                                // 画面の処分の受託（受領）をクリア
                                this.SyobunJyuCDDel();
                            }
                        }
                    }
                    break;
                case (int)DENSHU_KBN.URIAGE_SHIHARAI://売上支払
                    if (maniFlag == 1)
                    {
                        //※1次マニフェスト
                        dt = GetKenpaiDataDao.GetUriageShiharaiForRenkei(search);
                        renkei_dt = dt;
                        count = dt.Rows.Count;
                        // 20140617 katen 不具合No.4712 start‏
                        if (dt.Rows.Count > 0 && dt.Rows[0]["MANIFEST_SHURUI_CD"] != DBNull.Value && Convert.ToInt16(dt.Rows[0]["MANIFEST_SHURUI_CD"]) != 1)
                        {
                            return "E175";
                        }
                        // 20140617 katen 不具合No.4712 end‏
                        // 20140611 ria No.679 伝種区分連携 start
                        if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                        {
                            if (!this.CheckManiData())
                            {
                                isSonzai = true;

                            }
                        }
                        // 20140611 ria No.679 伝種区分連携 end
                        if (dt.Rows.Count > 0 && isReset && !isSonzai)
                        {
                            if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                            {
                                torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                            }
                            if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value)
                            {
                                gyousha = this.GetGyoushaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString());
                            }
                            if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["NIOROSHI_GENBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString(), dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString(), out tsumikaeKbn);
                            }

                            //画面の交付年月日に「売上／支払入力.伝票日付」をセットする
                            this.form.cdate_KohuDate.Value = dt.Rows[0]["DENPYOU_DATE"];

                            if (torihikisaki != null)
                            {
                                //画面の取引先に「売上／支払入力.取引先CD」をセットする
                                this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                                //画面の取引先に「売上／支払入力.取引先名」をセットする
                                this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()))
                            {
                                //荷積業者に入力がある場合は、荷積業者を優先する
                                //画面の排出事業者に「売上／支払入力.荷積業者CD」をセットする（*1）
                                this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);
                            }
                            else
                            {
                                //画面の排出事業者に「売上／支払入力.業者CD」をセットする（*1）
                                this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[0]["NIZUMI_GENBA_CD"].ToString()))
                            {
                                //荷積現場に入力がある場合は、荷積現場を優先する
                                //画面の排出事業場に「売上／支払入力.荷積現場CD」をセットする（*2）
                                this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);
                            }
                            else
                            {
                                //画面の排出事業場に「売上／支払入力.現場CD」をセットする（*2）
                                this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["GENBA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);
                            }
                            //画面の運搬受託者（１）に「売上／支払入力.運搬業者CD」をセットする（*3）
                            this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に「売上／支払入力.荷降業者CD」をセットする（*4）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);

                            }
                            else
                            {
                                //画面の処分受託者に「売上／支払入力.荷降業者CD」をセットする（*4）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);

                            }

                            if (genba != null)
                            {
                                if (genba.TSUMIKAEHOKAN_KBN)
                                {
                                    //画面の積替え又は保管の名称に「売上／支払入力.荷降現場CD」をセットする（*5）
                                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                                }
                                else
                                {
                                    //画面の運搬先の事業場に「売上／支払入力.荷降現場CD」をセットする（*5）
                                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                                }
                            }

                            //グリッドに廃棄物種類データ設定
                            this.SetGridDataForRenkei(dt);

                            //画面の運搬の受託（１）上に「売上／支払入力.運搬業者CD」をセットする（*6）
                            this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);

                            //画面の運搬の受託（１）下に「売上／支払入力.運転者CD」をセットする（*8）
                            this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                            //ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
                            this.form.ctxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_NAME"].ToString();

                            //画面の運搬受託者（１）の車種に「売上／支払入力.車種CD」をセットする（*7）
                            this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                            //ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);
                            this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();

                            //画面の運搬受託者（１）の車輌に「売上／支払入力.車輌CD」をセットする
                            this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                            //画面の運搬受託者（１）の車輌に「売上／支払入力.車輌名」をセットする
                            this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                            if (gyousha != null
                                && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                                || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                                || !tsumikaeKbn))
                            {
                                //画面の処分の受託（受領）上に「売上／支払入力.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                                //画面の処分の受託（処分）上に「売上／支払入力.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                            }
                            else
                            {
                                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                                this.form.ctxt_SyobunJyuName.Text = string.Empty;
                                this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                                this.form.ctxt_SyobunSyoName.Text = string.Empty;
                            }

                            //画面の運搬の受託（１）に「売上／支払入力.伝票日付」をセットする
                            this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["DENPYOU_DATE"];

                            //画面の処分の受託（受領）に「売上／支払入力.伝票日付」をセットする
                            if (!tsumikaeKbn)
                            {
                                this.form.cdate_SyobunJyu.Value = dt.Rows[0]["DENPYOU_DATE"];
                            }
                            else
                            {
                                // 画面の処分の受託（受領）をクリア
                                this.SyobunJyuCDDel();
                            }
                        }
                    }
                    break;
                case (int)DENSHU_KBN.UKETSUKE://受付
                    dt = GetKenpaiDataDao.GetUkeTsukeForRenkei(search);
                    renkei_dt = dt;
                    count = dt.Rows.Count;
                    // 20140617 katen 不具合No.4712 start‏
                    if (dt.Rows.Count > 0 && dt.Rows[0]["MANIFEST_SHURUI_CD"] != DBNull.Value && Convert.ToInt16(dt.Rows[0]["MANIFEST_SHURUI_CD"]) != 1)
                    {
                        return "E175";
                    }
                    // 20140617 katen 不具合No.4712 end‏
                    // 20140611 ria No.679 伝種区分連携 start
                    if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                    {
                        if (!this.CheckManiData())
                        {
                            isSonzai = true;

                        }
                    }
                    // 20140611 ria No.679 伝種区分連携 end
                    if (dt.Rows.Count > 0 && isReset && !isSonzai)
                    {
                        //画面の共通項目

                        if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                        {
                            torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                        }
                        if (torihikisaki != null)
                        {
                            //画面の取引先に「取引先CD」をセットする
                            this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                            //画面の取引先に「取引先名」をセットする
                            this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                        }

                        //グリッドに廃棄物種類データ設定
                        this.SetGridDataForRenkei(dt);
                        //画面の運搬受託者（１）の車輌に「車輌CD」をセットする
                        this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        //画面の運搬受託者（１）の車輌に「車輌名」をセットする
                        this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();
                        //画面の運搬の受託（１）に「伝票日付」をセットする
                        this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["JUTAKU_DATE"];

                        // 20140619 ria No.679 伝種区分連携 start
                        ////画面の運搬の受託（１）上に「運搬業者CD」をセットする（*6）
                        //this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        //this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);
                        // 20140619 ria No.679 伝種区分連携 end

                        //画面の運搬受託者（１）の車種に「車種CD」をセットする（*7）
                        this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                        //ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);
                        this.form.ctxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_NAME"].ToString();

                        //収集受付 或いは　持込受付
                        if (dt.Rows[0]["TABLEKUBUN"].ToString() == "1" || dt.Rows[0]["TABLEKUBUN"].ToString() == "2")
                        {
                            //画面の交付年月日に「伝票日付」をセットする
                            this.form.cdate_KohuDate.Value = dt.Rows[0]["KOUFU_DATE"];

                            //画面の排出事業者に「業者CD」をセットする（*1）
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                            //画面の排出事業場に「現場CD」をセットする（*2）
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                            //画面の運搬受託者（１）に「運搬業者CD」をセットする（*3）
                            this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                            if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString(), dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"].ToString(), out tsumikaeKbn);
                            }
                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に「荷降業者CD」をセットする（*4）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                            }
                            else
                            {
                                //画面の処分受託者に「荷降業者CD」をセットする（*4）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                            }

                            if (genba != null)
                            {
                                if (genba.TSUMIKAEHOKAN_KBN)
                                {
                                    //画面の積替え又は保管の名称に「運搬先の事業場CD」をセットする（*5）
                                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                                }
                                else
                                {
                                    //画面の運搬先の事業場に「運搬先の事業場CD」をセットする（*5）
                                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                                }
                            }

                            if (dt.Rows[0]["TABLEKUBUN"].ToString() != "2")
                            {
                                //収集受付Data又は出荷受付Dataが取得できた場合
                                //画面の運搬の受託（１）下に「運転者CD」をセットする（*8）
                                this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                                //ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
                                this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();
                            }

                            //画面の処分の受託（受領）に「伝票日付」をセットする
                            if (!tsumikaeKbn)
                            {
                                this.form.cdate_SyobunJyu.Value = dt.Rows[0]["JUTAKU_DATE"];
                            }
                            else
                            {
                                // 画面の処分の受託（受領）をクリア
                                this.SyobunJyuCDDel();
                            }
                        }
                        //出荷受付
                        else if (dt.Rows[0]["TABLEKUBUN"].ToString() == "3")
                        {
                            //画面の交付年月日に「伝票日付」をセットする
                            this.form.cdate_KohuDate.Value = dt.Rows[0]["JUTAKU_DATE"];

                            if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString(), dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"].ToString(), out tsumikaeKbn);
                            }

                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に「荷降業者CD」をセットする（*1）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                            }
                            else
                            {
                                //画面の処分受託者に「荷降業者CD」をセットする（*1）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                            }

                            if (genba != null)
                            {
                                if (genba.TSUMIKAEHOKAN_KBN)
                                {
                                    //画面の積替え又は保管の名称に「運搬先の事業場CD」をセットする（*2）
                                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                                }
                                else
                                {
                                    //画面の運搬先の事業場に「運搬先の事業場CD」をセットする（*2）
                                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["UNPANSAKI_JIGYOBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                                }
                            }

                            //画面の運搬受託者（１）に「運搬業者CD」をセットする（*3）
                            this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                            //画面の排出事業者に「業者CD」をセットする（*10）
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                            //画面の排出事業場に「現場CD」をセットする（*11）
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                            //画面の運搬の受託（１）下に「運転者CD」をセットする（*8）
                            this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                            //ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
                            this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();

                            //画面の処分の受託（受領）に「伝票日付」をセットする
                            if (!tsumikaeKbn)
                            {
                                this.form.cdate_SyobunJyu.Value = dt.Rows[0]["JUTAKU_DATE"];
                            }
                            else
                            {
                                // 画面の処分の受託（受領）をクリア
                                this.SyobunJyuCDDel();
                            }

                        }

                        // 20140619 ria No.679 伝種区分連携 start
                        //画面の運搬の受託（１）上に「運搬業者CD」をセットする（*6）
                        this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);
                        // 20140619 ria No.679 伝種区分連携 end

                        if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value)
                        {
                            gyousha = this.GetGyoushaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString());
                        }
                        if (gyousha != null
                            && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                            || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                            || !tsumikaeKbn))
                        {
                            //画面の処分の受託（受領）上に「荷降業者CD」をセットする（*9）
                            this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                            //画面の処分の受託（処分）上に「荷降業者CD」をセットする（*9）
                            this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                        }
                        else
                        {
                            this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                            this.form.ctxt_SyobunJyuName.Text = string.Empty;
                            this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                            this.form.ctxt_SyobunSyoName.Text = string.Empty;
                        }
                    }
                    break;
                // Ver 2.6では計量入力リリースのため表示する
                case (int)DENSHU_KBN.KEIRYOU://計量
                    if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                    {
                        dt = GetKenpaiDataDao.GetKeiryoJissekiForRenkei(search);
                    }
                    else
                    {
                        dt = GetKenpaiDataDao.GetKeiryoForRenkei(search);
                    }
                    renkei_dt = dt;
                    count = dt.Rows.Count;
                    if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["KIHON_KEIRYOU"]) != this.maniFlag.ToString())
                    {
                        if (this.maniFlag == 1)
                        {
                            this.form.messageShowLogic.MessageBoxShowError("基本計量が出荷となっている計量伝票は、選択できません。2次マニフェストで登録してください。");
                        }
                        else
                        {
                            this.form.messageShowLogic.MessageBoxShowError("基本計量が受入となっている計量伝票は、選択できません。1次マニフェストで登録してください。");
                        }
                        this.form.cantxt_No.Focus();
                        return "";
                    }
                    // 20140617 katen 不具合No.4712 start‏
                    if (dt.Rows.Count > 0 && dt.Rows[0]["MANIFEST_SHURUI_CD"] != DBNull.Value && Convert.ToInt16(dt.Rows[0]["MANIFEST_SHURUI_CD"]) != 1)
                    {
                        return "E175";
                    }
                    // 20140617 katen 不具合No.4712 end‏
                    // 20140611 ria No.679 伝種区分連携 start
                    if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                    {
                        if (!this.CheckManiData())
                        {
                            isSonzai = true;

                        }
                    }
                    // 20140611 ria No.679 伝種区分連携 end
                    if (dt.Rows.Count > 0 && isReset && !isSonzai)
                    {
                        //画面の交付年月日に「計量.伝票日付」をセットする
                        this.form.cdate_KohuDate.Value = dt.Rows[0]["DENPYOU_DATE"];

                        if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                        {
                            torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                        }
                        if (torihikisaki != null)
                        {
                            //画面の取引先に「計量.取引先CD」をセットする
                            this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                            //画面の取引先に「計量.取引先名」をセットする
                            this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                        }

                        if (maniFlag == 1)
                        {
                            //画面の排出事業者に1次マニフェストの場合は「計量.業者CD」をセットする（*1）
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["HAISYUTU_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                            //画面の排出事業場に1次マニフェストの場合は「計量.現場CD」をセットする（*2）
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["HAISYUTU_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                            if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["UNPAN_GENBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString(), dt.Rows[0]["UNPAN_GENBA_CD"].ToString(), out tsumikaeKbn);
                            }

                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に1次マニフェストの場合は「計量.荷降業者CD」をセットする（*4）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                            }
                            else
                            {
                                //画面の処分受託者に1次マニフェストの場合は「計量.荷降業者CD」をセットする（*4）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                            }

                            if (genba != null)
                            {
                                if (genba.TSUMIKAEHOKAN_KBN)
                                {
                                    //画面の積替え又は保管の名称に1次マニフェストの場合は「荷降現場CD」をセットする（*5）
                                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                                }
                                else
                                {
                                    //画面の運搬先の事業場に1次マニフェストの場合は「荷降現場CD」をセットする（*5）
                                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                                }
                            }

                            if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value)
                            {
                                gyousha = this.GetGyoushaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString());
                            }
                            if (gyousha != null
                                && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                                || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                                || !tsumikaeKbn))
                            {
                                //画面の処分の受託（受領）上に「計量.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                                //画面の処分の受託（処分）上に「計量.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                            }
                            else
                            {
                                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                                this.form.ctxt_SyobunJyuName.Text = string.Empty;
                                this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                                this.form.ctxt_SyobunSyoName.Text = string.Empty;
                            }
                        }
                        else
                        {
                            //画面の排出事業者に2次マニフェストの場合は「計量.荷積業者」をセットする（*1）
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["HAISYUTU_GYOUSHA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                            //画面の排出事業場に2次マニフェストの場合は「計量.荷積現場CD」をセットする（*2）
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["HAISYUTU_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                            if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["UNPAN_GENBA_CD"] != DBNull.Value)
                            {
                                genba = this.GetGenbaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString(), dt.Rows[0]["UNPAN_GENBA_CD"].ToString(), out tsumikaeKbn);
                            }

                            if (tsumikaeKbn)
                            {
                                //画面の積替え又は保管の業者に2次マニフェストの場合は「計量.業者CD」をセットする（*4）
                                this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                            }
                            else
                            {
                                //画面の処分受託者に2次マニフェストの場合は「計量.業者CD」をセットする（*4）
                                this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                            }

                            if (genba != null)
                            {
                                if (genba.TSUMIKAEHOKAN_KBN)
                                {
                                    //画面の積替え又は保管の名称に2次マニフェストの場合は「現場CD」をセットする（*2）
                                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                                }
                                else
                                {
                                    //画面の運搬先の事業場に2次マニフェストの場合は「現場CD」をセットする（*2）
                                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                                }
                            }

                            if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value)
                            {
                                gyousha = this.GetGyoushaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString());
                            }
                            if (gyousha != null
                                && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                                || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                                || !tsumikaeKbn))
                            {
                                //画面の処分の受託（受領）上に「計量.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                                //画面の処分の受託（処分）上に「計量.荷降業者CD」をセットする（*9）
                                this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                                this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                            }
                            else
                            {
                                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                                this.form.ctxt_SyobunJyuName.Text = string.Empty;
                                this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                                this.form.ctxt_SyobunSyoName.Text = string.Empty;
                            }
                        }
                        //グリッドに廃棄物種類データ設定
                        this.SetGridDataForRenkei(dt);
                        if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                        {
                            this.SetRenkeiHaikiSyuruiToForm(dt);
                        }

                        //画面の運搬受託者（１）に「計量.運搬業者CD」をセットする（*3）
                        this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                        //画面の運搬の受託（１）上に「計量.運搬業者CD」をセットする（*6）
                        this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);

                        //画面の運搬の受託（１）下に「計量.運転者CD」をセットする（*8）
                        this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                        ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);


                        //画面の運搬受託者（１）の車種に「計量.車種CD」をセットする
                        this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                        ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);


                        //画面の運搬受託者（１）の車輌に「計量.車輌CD」をセットする
                        this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        //画面の運搬受託者（１）の車輌に「計量.車輌名」をセットする
                        this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                        //画面の運搬の受託（１）に「計量.伝票日付」をセットする
                        this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["DENPYOU_DATE"];

                        //画面の処分の受託（受領）に「計量.伝票日付」をセットする
                        if (!tsumikaeKbn)
                        {
                            this.form.cdate_SyobunJyu.Value = dt.Rows[0]["DENPYOU_DATE"];
                        }
                        else
                        {
                            // 画面の処分の受託（受領）をクリア
                            this.SyobunJyuCDDel();
                        }
                    }
                    break;
                // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                case (int)DENSHU_KBN.TEIKI_JISSEKI://定期実績
                    //※1次マニフェスト
                    dt = GetKenpaiDataDao.GetDenshuTeikiJissekiForRenkei(search);
                    renkei_dt = dt;
                    count = dt.Rows.Count;

                    // 20140611 ria No.679 伝種区分連携 start
                    if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                    {
                        if (!this.CheckManiData())
                        {
                            isSonzai = true;

                        }
                    }
                    // 20140611 ria No.679 伝種区分連携 end
                    if (dt.Rows.Count > 0 && isReset && !isSonzai)
                    {
                        //交付年月日
                        this.form.cdate_KohuDate.Text = dt.Rows[0]["KOUFU_DATE"].ToString();
                        //運搬終了年月日
                        this.form.cdate_UnpanJyu1.Text = dt.Rows[0]["UNPAN_SYURYOU_DATA"].ToString();
                        //運搬受託者CD
                        this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        //運搬受託者情報
                        this.form.cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod();
                        //運搬方法
                        string unpanHouhouCD = "1";
                        M_UNPAN_HOUHOU unpanhouhou = this.UnpanhouhouDao.GetDataByCd(unpanHouhouCD);
                        if (unpanhouhou != null && !string.IsNullOrEmpty(this.form.cantxt_UnpanJyutaku1Name.Text))
                        {
                            this.form.cantxt_UnpanHouhou1CD.Text = unpanHouhouCD;
                            this.form.ctxt_UnpanHouhouMei1.Text = unpanhouhou.UNPAN_HOUHOU_NAME_RYAKU;
                        }
                        //運搬の受託（上）CD
                        this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        //運搬の受託（上）情報
                        this.form.cantxt_UnpanJyuCd1_PopupAfterExecuteMethod();
                        //運搬の受託（下）CD
                        this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                        //運搬の受託（下）
                        this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();
                        //運搬受託者の車種CD
                        this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                        //運搬受託者の車種情報
                        this.form.ctxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_NAME"].ToString();
                        //運搬受託者の車輌CD
                        this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                        //運搬受託者の車輌名
                        this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                        if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                        {
                            //排出事業者CD
                            this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["HAISYUTU_GYOUSHA_CD"].ToString();
                            //排出事業者情報
                            this.form.cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod();
                            //排出事業場CD
                            this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["HAISYUTU_GENBA_CD"].ToString();
                            //排出事業場情報
                            this.form.cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod();

                            /*荷降データ*/
                            //処分受託者CD
                            this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                            //処分受託者情報
                            this.form.cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod();
                            //運搬先の事業場CD
                            this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                            //運搬先の事業場情報
                            this.form.cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod();
                            //処分の受託（上）CD
                            this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                            //処分の受託（上）情報
                            this.form.cantxt_SyobunJyuCd_PopupAfterExecuteMethod();

                            //廃棄物種類の設定
                            this.SetGridDataForRenkeiForTeikiJisseki(dt);
                        }
                    }
                    break;
                // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                default:
                    break;
            }

            // 名称が空だった場合、CDをクリアします
            this.EmptyNameClearCD();

            LogUtility.DebugMethodEnd(denshuKbn, renkeiNo, renkeiMeisaigyo, isReset);

            // 20140611 ria No.679 伝種区分連携 start
            if (count == 0)
            {
                return "C134";

            }
            else
            {
                if (isSonzai)
                {
                    return "E173";

                }
            }
            // 20140611 ria No.679 伝種区分連携 end
            return "";
        }

        /// <summary>
        /// 名称が空だった場合、CDをクリアします
        /// </summary>
        private void EmptyNameClearCD()
        {
            // 取引先
            if (string.IsNullOrEmpty(this.form.ctxt_TorihikiName.Text))
            {
                this.form.cantxt_TorihikiCd.Text = string.Empty;
            }

            // 排出事業者
            if (string.IsNullOrEmpty(this.form.ctxt_HaisyutuGyousyaName1.Text))
            {
                this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
            }

            // 排出事業場
            if (string.IsNullOrEmpty(this.form.ctxt_HaisyutuJigyoubaName1.Text))
            {
                this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
            }

            // 運搬受託者(収集運搬業者)(１)
            if (string.IsNullOrEmpty(this.form.cantxt_UnpanJyutaku1Name.Text))
            {
                this.form.cantxt_UnpanJyutaku1NameCd.Text = string.Empty;
            }

            // 積替え又は保管の業者
            if (string.IsNullOrEmpty(this.form.ctxt_TumiGyoName.Text))
            {
                this.form.cantxt_TumiGyoCd.Text = string.Empty;
            }

            // 積替え又は保管の名称
            if (string.IsNullOrEmpty(this.form.ctxt_TumiHokaName.Text))
            {
                this.form.cantxt_TumiHokaNameCd.Text = string.Empty;
            }

            // 運搬先の事業場(処分業者の処理施設)
            if (string.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaName.Text))
            {
                this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            }

            // 処分受託者(処分業者)
            if (string.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuName.Text))
            {
                this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
            }

            // 運搬の受託
            if (string.IsNullOrEmpty(this.form.ctxt_UnpanJyuName1.Text))
            {
                this.form.cantxt_UnpanJyuCd1.Text = string.Empty;
            }

            // 処分の受託（受領）
            if (string.IsNullOrEmpty(this.form.ctxt_SyobunJyuName.Text))
            {
                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
            }

            // 処分の受託（処分）
            if (string.IsNullOrEmpty(this.form.ctxt_SyobunSyoName.Text))
            {
                this.form.cantxt_SyobunSyoCd.Text = string.Empty;
            }
        }

        /// <summary>
        /// 取引先を取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        private M_TORIHIKISAKI GetTorihikisakiEntity(string torihikisakiCd)
        {
            // 取引先を取得
            IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            M_TORIHIKISAKI search = new M_TORIHIKISAKI();
            search.TORIHIKISAKI_CD = torihikisakiCd;
            search.ISNOT_NEED_DELETE_FLG = true;
            var torihikisaki = torihikisakiDao.GetAllValidData(search).FirstOrDefault();

            return torihikisaki;
        }

        /// <summary>
        /// 業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        private M_GYOUSHA GetGyoushaEntity(string gyoushaCd)
        {
            // 業者を取得
            IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            M_GYOUSHA search = new M_GYOUSHA();
            search.GYOUSHA_CD = gyoushaCd;
            search.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = gyoushaDao.GetAllValidData(search).FirstOrDefault();

            return gyousha;

        }

        /// <summary>
        /// 現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者</param>
        /// <param name="genbaCd">現場</param>
        /// <returns></returns>
        private M_GENBA GetGenbaEntity(string gyoushaCd, string genbaCd)
        {
            // 現場を取得
            M_GENBA search = new M_GENBA();
            search.GYOUSHA_CD = gyoushaCd;
            search.GENBA_CD = genbaCd;
            IM_GENBADao genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            M_GENBA genba = new M_GENBA();
            genba = genbaDao.GetAllValidData(search).FirstOrDefault();

            return genba;
        }

        /// <summary>
        /// 現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者</param>
        /// <param name="genbaCd">現場</param>
        /// <param name="tumikaeKbn">
        /// 削除フラグ、適用期間に関係なく、現場の積替え区分を取得
        /// ※現場が無ければFalse
        /// </param>
        /// <returns></returns>
        private M_GENBA GetGenbaEntity(string gyoushaCd, string genbaCd, out bool tumikaeKbn)
        {
            // 現場を取得
            tumikaeKbn = false;
            M_GENBA search = new M_GENBA();
            search.GYOUSHA_CD = gyoushaCd;
            search.GENBA_CD = genbaCd;
            search.ISNOT_NEED_DELETE_FLG = true;
            IM_GENBADao genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            M_GENBA genba = new M_GENBA();

            // 積替保管区分を取得
            if (genbaDao.GetDataByCd(search) != null)
            {
                tumikaeKbn = (bool)genbaDao.GetDataByCd(search).TSUMIKAEHOKAN_KBN;
            }

            genba = genbaDao.GetAllValidData(search).FirstOrDefault();

            return genba;
        }

        /// <summary>
        /// 機能間連携用グリッドにデータ設定
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        public void SetGridDataForRenkei(DataTable dt)
        {
            // 20140616 ria EV004838 規定パターンからの連携番号入力で換算数量残る start
            //実績：混合種類
            this.form.cantxt_KongoCd.Text = string.Empty;
            this.form.ctxt_KongoName.Text = string.Empty;

            this.form.cdgrid_Jisseki.Columns[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].ReadOnly = false;

            //実績：混合数量
            this.form.cntxt_JissekiSuryo.Text = string.Empty;
            this.form.cntxt_JissekiSuryo.FormatSetting = "カスタム";
            this.form.cntxt_JissekiSuryo.CustomFormatSetting = this.ManifestSuuryoFormat;

            //実績：混合単位
            this.form.canTxt_JissekiTaniCd.Text = string.Empty;
            this.form.ctxt_JissekiTaniName.Text = string.Empty;
            // 20140616 ria EV004838 規定パターンからの連携番号入力で換算数量残る end

            //isClearForm = true => do not check valid on cdgrid_Jisseki when clear form
            this.form.isClearForm = true;
            this.form.cdgrid_Jisseki.Rows.Clear();
            //reset variable
            this.form.isClearForm = false;
            this.form.cdgrid_Jisseki.RowCount = dt.Rows.Count + 1;
            this.form.cdgrid_Jisseki.Rows[dt.Rows.Count].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunEndDate].Value = null;
            this.form.cdgrid_Jisseki.Rows[dt.Rows.Count].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SyobunEndDate].Value = null;

            // 20140621 ria EV004829 実績タブ空レコード start
            int j = 0;
            int iKbn;
            iKbn = Convert.ToInt32(this.form.cantxt_DenshuKbn.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["HAIKI_SHURUI_CD"].ToString()))
                {
                    //画面の運搬先の事業場に「廃棄物種類マスタ.廃棄物種類CD」をセットする
                    this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value‏ = dt.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    //画面の運搬先の事業場に「廃棄物種類マスタ.廃棄物種類名」をセットする
                    this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value‏ = dt.Rows[i]["HAIKI_SHURUI_NAME_RYAKU"].ToString();
                    if (iKbn.Equals((int)DENSHU_KBN.KEIRYOU))
                    {
                        // 最終処分業者CD
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaCd].Value = dt.Rows[i]["LAST_SBN_GYOUSHA_CD"].ToString();
                        // 最終処分業者名
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuGyosyaName].Value = dt.Rows[i]["LAST_SBN_GYOUSHA_NAME"].ToString();
                        // 最終処分場所CD
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuBasyoCd].Value = dt.Rows[i]["LAST_SBN_GENBA_CD"].ToString();
                        // 最終処分場所名
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.SaisyuSyobunBasyo].Value = dt.Rows[i]["LAST_SBN_GENBA_NAME"].ToString();
                    }
                    if ((this.form.ismobile_mode) && (this.maniFlag == 1) && (this.form.Renkei_Mode_2.Checked) && (iKbn.Equals((int)DENSHU_KBN.KEIRYOU) || iKbn.Equals((int)DENSHU_KBN.UKEIRE)))
                    {
                        if (decimal.Parse(dt.Rows[0]["NET_TOTAL"].ToString()) > 0)
                        {
                            //割合(%)
                            this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value = dt.Rows[i]["SUURYOU_WARIAI"].ToString();
                            //単位CD
                            this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = 3;
                            //単位名
                            this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = dt.Rows[0]["UNIT_NAME"].ToString();
                        }
                    }

                    j++;
                }
            }
            this.form.cdgrid_Jisseki.RowCount = j + 1;

            // 20140621 ria EV004829 実績タブ空レコード end
            this.form.cdgrid_Jisseki.RefreshEdit();

            // 20140616 ria EV004838 規定パターンからの連携番号入力で換算数量残る start
            //数値計算
            if (!this.SetTotal()) { return; }

            //数値フォーマット適用
            this.SetNumFormst();
            // 20140616 ria EV004838 規定パターンからの連携番号入力で換算数量残る end

        }
        // 20140512 katen No.679 伝種区分、連携番号、連携明細行連携 end‏

        // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 start
        /// <summary>
        /// 連携番号、連携明細行を取得する
        /// </summary>
        private string[] GetRenkeiSystemId()
        {
            string[] id;
            DataTable dt;
            dt = renkei_dt;
            id = new string[2];
            if (renkei_dt == null)
            {
                return id;
            }

            if (dt.Rows.Count > 0)
            {
                id[0] = dt.Rows[0]["SYSTEM_ID"].ToString();
                if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
                {
                    id[1] = dt.Rows[0]["DETAIL_SYSTEM_ID"].ToString();
                }

            }

            return id;
        }
        /// <summary>
        /// 連携番号,連携明細行を設定する
        /// </summary>
        private void SetRenkeiSystemId(string iKbn, string strSysId, string strSysMeiId)
        {
            LogUtility.DebugMethodStart(iKbn, strSysId, strSysMeiId);

            this.SearchResult = new DataTable();
            this.SearchString = new SerchParameterDtoCls();

            this.SearchString.RENKEI_DENSHU_KBN_CD = iKbn;
            this.SearchString.RENKEI_SYSTEM_ID = strSysId;
            this.SearchString.RENKEI_MEISAI_SYSTEM_ID = strSysMeiId;

            if (iKbn != string.Empty)
            {
                int kbn = Convert.ToInt32(iKbn);
                switch (kbn)
                {
                    case (int)DENSHU_KBN.UKEIRE: //受入
                        if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                        {
                            this.SearchResult = Ukeiredao.GetDataForJissekiEntity(this.SearchString);
                        }
                        else
                        {
                            this.SearchResult = Ukeiredao.GetDataForEntity(this.SearchString);
                        }
                        if (this.SearchResult.Rows.Count > 0)
                        {
                            this.form.cantxt_No.Text = this.SearchResult.Rows[0]["UKEIRE_NUMBER"].ToString();
                            this.form.cantxt_Meisaigyou.Text = this.SearchResult.Rows[0]["ROW_NO"].ToString();
                        }

                        break;

                    case (int)DENSHU_KBN.SHUKKA: //出荷
                        this.SearchResult = Shukkadao.GetDataForEntity(this.SearchString);
                        if (this.SearchResult.Rows.Count > 0)
                        {
                            this.form.cantxt_No.Text = this.SearchResult.Rows[0]["SHUKKA_NUMBER"].ToString();
                            this.form.cantxt_Meisaigyou.Text = this.SearchResult.Rows[0]["ROW_NO"].ToString();
                        }
                        break;

                    case (int)DENSHU_KBN.URIAGE_SHIHARAI://売上支払
                        this.SearchResult = Uriagedao.GetDataForEntity(this.SearchString);
                        if (this.SearchResult.Rows.Count > 0)
                        {
                            this.form.cantxt_No.Text = this.SearchResult.Rows[0]["UR_SH_NUMBER"].ToString();
                            this.form.cantxt_Meisaigyou.Text = this.SearchResult.Rows[0]["ROW_NO"].ToString();
                        }
                        break;

                    case (int)DENSHU_KBN.KEIRYOU://計量
                        if (this.form.ismobile_mode && this.maniFlag == 1 && this.form.Renkei_Mode_2.Checked)
                        {
                            this.SearchResult = Keiryoudao.GetDataForJissekiEntity(this.SearchString);
                        }
                        else
                        {
                            this.SearchResult = Keiryoudao.GetDataForEntity(this.SearchString);
                        }
                        if (this.SearchResult.Rows.Count > 0)
                        {
                            this.form.cantxt_No.Text = this.SearchResult.Rows[0]["KEIRYOU_NUMBER"].ToString();
                            this.form.cantxt_Meisaigyou.Text = this.SearchResult.Rows[0]["ROW_NO"].ToString();
                        }
                        break;

                    case (int)DENSHU_KBN.UKETSUKE://受付

                        //収集受付Data
                        this.SearchResult = GetUketsukeSsDataDao.GetDataForEntity(SearchString);
                        if (this.SearchResult.Rows.Count <= 0)
                        {
                            //持込受付Data
                            this.SearchResult = GetUketsukeMkDataDao.GetDataForEntity(SearchString);
                            if (this.SearchResult.Rows.Count <= 0)
                            {
                                //出荷受付Data
                                this.SearchResult = GetUketsukeSkDataDao.GetDataForEntity(SearchString);
                            }
                        }
                        if (this.SearchResult.Rows.Count > 0)
                        {
                            this.form.cantxt_No.Text = this.SearchResult.Rows[0]["UKETSUKE_NUMBER"].ToString();
                            this.form.cantxt_Meisaigyou.Text = this.SearchResult.Rows[0]["ROW_NO"].ToString();
                        }
                        break;
                    // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                    case (int)DENSHU_KBN.TEIKI_JISSEKI://定期実績
                        this.SearchResult = TeikiJissekidao.GetDataForEntity(this.SearchString);
                        if (this.SearchResult.Rows.Count > 0)
                        {
                            this.form.cantxt_No.Text = this.SearchResult.Rows[0]["TEIKI_JISSEKI_NUMBER"].ToString();
                            this.form.cantxt_Meisaigyou.Text = this.SearchResult.Rows[0]["ROW_NUMBER"].ToString();
                        }
                        break;
                    // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                    default:
                        break;
                }
            }
            LogUtility.DebugMethodEnd(iKbn, strSysId, strSysMeiId);
        }
        /// <summary>
        /// 伝種区分名称を設定する
        /// </summary>
        private void SetDenshuKbnName(string iKbn)
        {
            this.SearchResult = new DataTable();
            this.SearchString = new SerchParameterDtoCls();
            if (iKbn != string.Empty)
            {
                this.SearchString.RENKEI_DENSHU_KBN_CD = iKbn;
                this.SearchResult = GetDenshuKbnNameDataDao.GetDataForEntity(SearchString);
                if (this.SearchResult.Rows.Count > 0)
                {
                    this.form.ctxt_DenshuName.Text = this.SearchResult.Rows[0]["DENSHU_KBN_NAME_RYAKU"].ToString();
                }
            }
        }

        /// <summary>
        ///  伝種区分より、画面の番号と明細行の名をセットする
        /// </summary>
        public void SetRenkeiLabel()
        {
            int iKbn = 0;
            if (!string.IsNullOrEmpty(this.form.cantxt_DenshuKbn.Text))
            {
                iKbn = Convert.ToInt32(this.form.cantxt_DenshuKbn.Text.Trim());
            }
            switch (iKbn)
            {
                case (int)DENSHU_KBN.UKEIRE: //受入
                case (int)DENSHU_KBN.SHUKKA: //出荷
                case (int)DENSHU_KBN.URIAGE_SHIHARAI://売上支払
                case (int)DENSHU_KBN.UKETSUKE://受付
                // 20160510 koukoukon #12041 定期配車関連の全面見直し start
                case (int)DENSHU_KBN.TEIKI_JISSEKI://定期実績
                    // 20160510 koukoukon #12041 定期配車関連の全面見直し end
                    // Ver 2.0では計量入力が未リリースのため非表示
                    // case (int)DENSHU_KBN.KEIRYOU://計量
                    this.form.lbl_No.Text = this.form.ctxt_DenshuName.Text + "番号";
                    break;

                default:
                    this.form.lbl_No.Text = "連携番号";
                    this.form.lbl_Misaigyou.Text = "明細行";
                    break;
            }

        }

        /// <summary>
        ///  テキストボックスの内容により、ポップアップファンクション又はクリアファンクションを使う
        /// </summary>
        private void ClearOrSetValue(string textBoxName, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                switch (textBoxName)
                {
                    case "cantxt_TumiHokaNameCd":
                        this.TumiHokaDel("cantxt_TumiHokaNameCd");
                        break;
                    case "cantxt_UnpanJyugyobaNameCd":
                        this.UnpanJyugyobaDel();
                        break;
                    case "cantxt_HaisyutuGyousyaCd":
                        this.HaisyutuGyousyaDel();
                        break;
                    case "cantxt_TumiGyoCd":
                        this.TumiHokaDel("cbtn_TumiHokaDel");
                        break;
                    case "cantxt_SyobunJyutakuNameCd":
                        this.SyobunJyutakuDel();
                        break;
                    case "cantxt_HaisyutuJigyoubaName":
                        this.HaisyutuGyoubaDel();
                        break;
                    case "cantxt_SyobunJyuCd":
                        this.form.ctxt_SyobunJyuName.Text = string.Empty;
                        break;
                    case "cantxt_SyobunSyoCd":
                        this.form.ctxt_SyobunSyoName.Text = string.Empty;
                        break;
                    case "cantxt_UnpanJyutaku1NameCd":
                        this.UnpanJyutaku1Del();
                        break;
                    case "cantxt_UnpanJyuCd1":
                        this.form.ctxt_UnpanJyuName1.Text = string.Empty;
                        break;
                }
            }
            else
            {
                switch (textBoxName)
                {
                    case "cantxt_TumiHokaNameCd":
                        this.form.cantxt_TumiHokaNameCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_UnpanJyugyobaNameCd":
                        this.form.cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_HaisyutuGyousyaCd":
                        this.form.cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_TumiGyoCd":
                        this.form.cantxt_TumiGyoCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_SyobunJyutakuNameCd":
                        this.form.cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_HaisyutuJigyoubaName":
                        this.form.cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_SyobunJyuCd":
                        this.form.cantxt_SyobunJyuCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_SyobunSyoCd":
                        this.form.cantxt_SyobunSyoCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_UnpanJyutaku1NameCd":
                        this.form.cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod();
                        break;
                    case "cantxt_UnpanJyuCd1":
                        this.form.cantxt_UnpanJyuCd1_PopupAfterExecuteMethod();
                        break;
                }
            }
        }
        // 20140523 syunrei No.679 産廃マニフェスト（建廃）入力連携 end

        // 20140609 katen No.730 規定値機能の追加について start
        /// <summary>
        /// 拠点でパターンデータを設定する
        /// </summary>
        /// <param name="kyotenCd">拠点CD</param>
        public bool SetKiteiValue(string kbn, string kyotenCd = null)
        {
            LogUtility.DebugMethodStart(kbn, kyotenCd);
            DataTable dt = new DataTable();
            SerchParameterDtoCls SearchString = new SerchParameterDtoCls();

            SearchString.KYOTEN = kyotenCd ?? this.headerform.ctxt_KyotenCd.Text;

            SearchString.HAIKI_KBN_CD = this.form.HaikiKbnCD;
            //マニ1次のとき0、2次のとき1
            SearchString.FIRST_MANIFEST_KBN = maniFlag == 1 ? "0" : "1";

            dt = KiteiValueDao.GetDataForEntity(SearchString);
            if (dt.Rows.Count == 0)
            {
                return false;
            }

            if (this.form.isKyotenFocusOut)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (DialogResult.Yes != msgLogic.MessageBoxShow("C089", MessageBoxDefaultButton.Button2, "拠点"))
                {
                    return false;
                }
            }

            // 権限チェック
            if (Manager.CheckAuthority("G121", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
            {
                this.form.parameters.PtSystemId = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.form.parameters.PtSeq = dt.Rows[0]["SEQ"].ToString();
                this.form.parameters.Mode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;

                if (kyotenCd != null)
                {
                    parentbaseform.bt_func3.Enabled = false;

                    // コントロールを初期化
                    this.ControlInit(kbn);

                    this.headerform.windowTypeLabel.Text = "新規";
                    this.SetAllPtData();
                    if (!this.SetManifestFrom("PtLoad")) { return false; }

                    //数値計算
                    if (!this.SetTotal()) { return false; }

                    //数値フォーマット適用
                    this.SetNumFormst();

                    //紐付情報クリア
                    this.maniRelation = null;

                    if (!string.IsNullOrEmpty(this.form.cantxt_DenshuKbn.Text))
                    {
                        this.SetRenkeiLabel();
                    }
                    else
                    {
                        this.form.lbl_No.Text = "連携番号";
                        this.form.lbl_Misaigyou.Text = "明細行";
                    }

                    //初期の場合に、廃棄物タブコントロールは「実績」を選ぶので、下の「原本」タブのコントロールが入力不可になる
                    this.ControlEnabledSet(false);
                }
            }
            LogUtility.DebugMethodEnd();
            return true;
        }
        // 20140609 katen No.730 規定値機能の追加について end

        // 20140611 ria No.679 伝種区分連携 start
        public bool CheckManiData()
        {
            LogUtility.DebugMethodStart();

            Int16 RENKEI_DENSHU_KBN_CD = Convert.ToInt16(this.form.cantxt_DenshuKbn.Text);

            string[] id = new string[2];
            id = GetRenkeiSystemId();

            Int64 RENKEI_SYSTEM_ID = 0;
            Int64 RENKEI_MEISAI_SYSTEM_ID = 0;
            Int16 RENKEI_MEISAI_MODE = 1;

            if (id != null && id.Length != 0)
            {
                RENKEI_SYSTEM_ID = Convert.ToInt64(id[0]);
                RENKEI_MEISAI_SYSTEM_ID = Convert.ToInt64(id[1]);
                if (!string.IsNullOrEmpty(this.form.cantxt_Renkei_Mode.Text) && this.maniFlag == 1)
                {
                    RENKEI_MEISAI_MODE = Convert.ToInt16(this.form.cantxt_Renkei_Mode.Text);
                }
            }
            bool DELETE_FLG = false;

            T_MANIFEST_ENTRY SearchResult = mopdao.GetManiDataForEntity(RENKEI_DENSHU_KBN_CD, RENKEI_SYSTEM_ID, RENKEI_MEISAI_SYSTEM_ID, RENKEI_MEISAI_MODE, DELETE_FLG);

            if (SearchResult != null && (string.IsNullOrEmpty(this.form.parameters.SystemId) || Convert.ToInt64(this.form.parameters.SystemId) != SearchResult.SYSTEM_ID))
            {
                LogUtility.DebugMethodEnd();
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        // 20140611 ria No.679 伝種区分連携 end

        //2014.05.19 by 胡　start       
        /// <summary>
        /// シ排出事業場を選択後、システム設定のA～E票使用区分が「使用しない」となっていた場合でも、
        /// 現場のA～E票使用区分が「使用しない」となっていたときは、グレーアウトする。
        /// </summary>
        public virtual void SetHenkyakuhiNyuuryokuEnabled()
        {
            LogUtility.DebugMethodStart();
            if (!String.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text) && !String.IsNullOrEmpty(this.form.cantxt_HaisyutuJigyoubaName.Text))
            {
                M_GENBA Serch = new M_GENBA();
                Serch.GYOUSHA_CD = this.form.cantxt_HaisyutuGyousyaCd.Text;
                Serch.GENBA_CD = this.form.cantxt_HaisyutuJigyoubaName.Text;

                IM_GENBADao GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                M_GENBA dtGenba = GenbaDao.GetDataByCd(Serch);
                if (dtGenba != null)
                {
                    this.mSysInfo = new DBAccessor().GetSysInfo();
                    if (mSysInfo != null)
                    {
                        if (mSysInfo.MANIFEST_USE_A == 1 && dtGenba.MANI_HENSOUSAKI_USE_A != 2)
                        {
                            this.form.cdate_HenkyakuA.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuA.Enabled = false;
                            this.form.cdate_HenkyakuA.Value = null;
                        }
                        if (mSysInfo.MANIFEST_USE_B1 == 1 && dtGenba.MANI_HENSOUSAKI_USE_B1 != 2)
                        {
                            this.form.cdate_HenkyakuB1.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuB1.Enabled = false;
                            this.form.cdate_HenkyakuB1.Value = null;
                        }
                        if (mSysInfo.MANIFEST_USE_B2 == 1 && dtGenba.MANI_HENSOUSAKI_USE_B2 != 2)
                        {
                            this.form.cdate_HenkyakuB2.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuB2.Enabled = false;
                            this.form.cdate_HenkyakuB2.Value = null;
                        }
                        if (mSysInfo.MANIFEST_USE_C1 == 1 && dtGenba.MANI_HENSOUSAKI_USE_C1 != 2)
                        {
                            this.form.cdate_HenkyakuC1.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuC1.Enabled = false;
                            this.form.cdate_HenkyakuC1.Value = null;
                        }
                        if (mSysInfo.MANIFEST_USE_C2 == 1 && dtGenba.MANI_HENSOUSAKI_USE_C2 != 2)
                        {
                            this.form.cdate_HenkyakuC2.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuC2.Enabled = false;
                            this.form.cdate_HenkyakuC2.Value = null;
                        }
                        if (mSysInfo.MANIFEST_USE_D == 1 && dtGenba.MANI_HENSOUSAKI_USE_D != 2)
                        {
                            this.form.cdate_HenkyakuD.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuD.Enabled = false;
                            this.form.cdate_HenkyakuD.Value = null;
                        }
                        if (mSysInfo.MANIFEST_USE_E == 1 && dtGenba.MANI_HENSOUSAKI_USE_E != 2)
                        {
                            this.form.cdate_HenkyakuE.Enabled = true;
                        }
                        else
                        {
                            this.form.cdate_HenkyakuE.Enabled = false;
                            this.form.cdate_HenkyakuE.Value = null;
                        }
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

        //2014.05.19 by 胡　end

        // 20140611 katen 不具合No.4469 start‏
        /// <summary>
        /// 業者マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_gyoshaName">業者名</param>
        /// <param name="txt_gyoshaPost">郵便番号</param>
        /// <param name="txt_gyoshaTel">電話番号</param>
        /// <param name="txt_gyoshaAdr">住所</param>
        /// <param name="TenkiNameFlg">転記先 業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_TenkiGyoshaCd">転記先 業者CD</param>
        /// <param name="txt_TenkiGyoshaName">転記先 業者名</param>
        /// <param name="HAISHUTSU_NIZUMI_GYOUSHA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GYOUSHA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="UNPAN_JUTAKUSHA_KAISHA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">検索条件の削除フラグの使用有無</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        internal int SetAddrGyousha(string NameFlg, CustomTextBox txt_gyoshaCd, CustomTextBox[] txt_gyoshaName,
                                        CustomTextBox txt_gyoshaPost, CustomTextBox txt_gyoshaTel, CustomTextBox[] txt_gyoshaAdr,
                                        string TenkiNameFlg, CustomTextBox txt_TenkiGyoshaCd, CustomTextBox[] txt_TenkiGyoshaName
                                        , bool HAISHUTSU_NIZUMI_GYOUSHA_KBN
                                        , bool SHOBUN_NIOROSHI_GYOUSHA_KBN
                                        , bool UNPAN_JUTAKUSHA_KAISHA_KBN
                                        , bool TSUMIKAEHOKAN_KBN
                                        , bool ISNOT_NEED_DELETE_FLG = false)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(NameFlg, txt_gyoshaCd, txt_gyoshaName,
                                            txt_gyoshaPost, txt_gyoshaTel, txt_gyoshaAdr,
                                            TenkiNameFlg, txt_TenkiGyoshaCd, txt_TenkiGyoshaName
                                            , HAISHUTSU_NIZUMI_GYOUSHA_KBN, SHOBUN_NIOROSHI_GYOUSHA_KBN, UNPAN_JUTAKUSHA_KAISHA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

                //空
                if (txt_gyoshaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                //エラー
                var Serch = new CommonGyoushaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.GYOUSHAKBN_MANI = "1";
                Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

                //区分セット
                if (HAISHUTSU_NIZUMI_GYOUSHA_KBN)
                {
                    Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                }
                if (SHOBUN_NIOROSHI_GYOUSHA_KBN)
                {
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (UNPAN_JUTAKUSHA_KAISHA_KBN)
                {
                    Serch.UNPAN_JUTAKUSHA_KAISHA_KBN = "1";
                }
                if (TSUMIKAEHOKAN_KBN)
                {
                    Serch.TSUMIKAEHOKAN_KBN = "1";
                }

                DataTable dt = this.GyoushaDao.GetDataForEntity(Serch);
                if (dt.Rows.Count == 0)
                {
                    ret = 2;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                //名称
                if (txt_gyoshaName != null && txt_gyoshaName.Length > 0)
                {
                    switch (NameFlg)
                    {
                        case "All":
                            //「正式名称1」と「正式名称2」をセットする。
                            txt_gyoshaName[0].Text = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                            if (txt_gyoshaName.Length > 1)
                            {
                                txt_gyoshaName[1].Text = dt.Rows[0]["GYOUSHA_NAME2"].ToString();
                            }
                            break;

                        case "Part1":
                            //「正式名称1」をセットする。
                            txt_gyoshaName[0].Text = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                            break;

                        default:
                            break;
                    }
                }

                //郵便番号
                if (txt_gyoshaPost != null)
                {
                    txt_gyoshaPost.Text = dt.Rows[0]["GYOUSHA_POST"].ToString();
                }

                //電話番号
                if (txt_gyoshaTel != null)
                {
                    txt_gyoshaTel.Text = dt.Rows[0]["GYOUSHA_TEL"].ToString();
                }

                //住所
                if (txt_gyoshaAdr != null && txt_gyoshaAdr.Length > 0)
                {
                    txt_gyoshaAdr[0].Text = dt.Rows[0]["TODOUFUKEN_NAME"].ToString() + dt.Rows[0]["GYOUSHA_ADDRESS1"].ToString();
                    if (txt_gyoshaAdr.Length > 1)
                    {
                        txt_gyoshaAdr[1].Text = dt.Rows[0]["GYOUSHA_ADDRESS2"].ToString();
                    }
                }

                //転記先 業者CD
                if (txt_TenkiGyoshaCd != null)
                {
                    txt_TenkiGyoshaCd.Text = txt_gyoshaCd.Text;
                }

                //転記先 業者名
                if (txt_TenkiGyoshaName != null && txt_TenkiGyoshaName.Length > 0)
                {
                    switch (TenkiNameFlg)
                    {
                        case "All"://「正式名称1 + 正式名称2」をセットする。
                            txt_TenkiGyoshaName[0].Text = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                            if (txt_TenkiGyoshaName.Length > 1)
                            {
                                txt_TenkiGyoshaName[1].Text = dt.Rows[0]["GYOUSHA_NAME2"].ToString();
                            }
                            break;

                        case "Part1"://「正式名称1」をセットする。
                            txt_TenkiGyoshaName[0].Text = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddrGyousha", ex);
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
        /// 現場マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">現場名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_JigyoubaCd">現場CD</param>
        /// <param name="txt_JigyoubaName">現場名</param>
        /// <param name="txt_JigyoubaPost">郵便番号</param>
        /// <param name="txt_JigyoubaTel">電話番号</param>
        /// <param name="txt_JigyoubaAdr">住所</param>
        /// <param name="txt_JigyoubaShobunNo">処分No.</param>
        /// <param name="HAISHUTSU_NIZUMI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SAISHUU_SHOBUNJOU_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">検索条件の削除フラグの使用有無</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        internal int SetAddressJigyouba(string NameFlg, CustomTextBox txt_gyoshaCd, CustomTextBox txt_JigyoubaCd, CustomTextBox[] txt_JigyoubaName,
            CustomTextBox txt_JigyoubaPost, CustomTextBox txt_JigyoubaTel, CustomTextBox[] txt_JigyoubaAdr, CustomTextBox txt_JigyoubaShobunNo
            , bool HAISHUTSU_NIZUMI_GENBA_KBN
            , bool SAISHUU_SHOBUNJOU_KBN
            , bool SHOBUN_NIOROSHI_GENBA_KBN
            , bool TSUMIKAEHOKAN_KBN
            , bool ISNOT_NEED_DELETE_FLG = false
            )
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(NameFlg, txt_gyoshaCd, txt_JigyoubaCd, txt_JigyoubaName,
                    txt_JigyoubaPost, txt_JigyoubaTel, txt_JigyoubaAdr, txt_JigyoubaShobunNo
                    , HAISHUTSU_NIZUMI_GENBA_KBN, SAISHUU_SHOBUNJOU_KBN, SHOBUN_NIOROSHI_GENBA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

                //空
                if (txt_gyoshaCd.Text == string.Empty || txt_JigyoubaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.GENBA_CD = txt_JigyoubaCd.Text;
                Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

                //区分
                if (HAISHUTSU_NIZUMI_GENBA_KBN)
                {
                    Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                    Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "1";
                }
                if (SAISHUU_SHOBUNJOU_KBN)
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    Serch.SHOBUN_NIOROSHI_GENBA_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (TSUMIKAEHOKAN_KBN)
                {
                    Serch.TSUMIKAEHOKAN_KBN = "1";
                }

                DataTable dt = this.GenbaDao.GetDataForEntity(Serch);
                switch (dt.Rows.Count)
                {
                    case 1://正常
                        break;

                    default://エラー
                        ret = 2;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                }

                //現場名
                if (txt_JigyoubaName != null && txt_JigyoubaName.Length > 0)
                {

                    switch (NameFlg)
                    {
                        case "All"://「正式名称1」と「正式名称2」をセットする。
                            txt_JigyoubaName[0].Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                            if (txt_JigyoubaName.Length > 1)
                            {
                                txt_JigyoubaName[1].Text = dt.Rows[0]["GENBA_NAME2"].ToString();
                            }
                            break;

                        case "Part1"://「正式名称1」をセットする。
                            txt_JigyoubaName[0].Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                            break;

                        default:
                            break;
                    }
                }

                //郵便番号
                if (txt_JigyoubaPost != null)
                {
                    txt_JigyoubaPost.Text = dt.Rows[0]["GENBA_POST"].ToString();
                }

                //電話番号
                if (txt_JigyoubaTel != null)
                {
                    txt_JigyoubaTel.Text = dt.Rows[0]["GENBA_TEL"].ToString();
                }

                //住所
                if (txt_JigyoubaAdr != null && txt_JigyoubaAdr.Length > 0)
                {
                    txt_JigyoubaAdr[0].Text = dt.Rows[0]["TODOUFUKEN_NAME"].ToString() + dt.Rows[0]["GENBA_ADDRESS1"].ToString();
                    if (txt_JigyoubaAdr != null && txt_JigyoubaAdr.Length > 1)
                    {
                        txt_JigyoubaAdr[1].Text = dt.Rows[0]["GENBA_ADDRESS2"].ToString();
                    }
                }

                //処分No.
                if (txt_JigyoubaShobunNo != null)
                {
                    txt_JigyoubaShobunNo.Text = dt.Rows[0]["SHOBUNSAKI_NO"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddressJigyouba", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        // 20140611 katen 不具合No.4469 end‏
        // 20140617 kayo EV004840 全ての処分終了が入力されないと紐付不可のように修正 start
        /// <summary>
        /// 紐付ボタンの必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheckForUnit()
        {
            LogUtility.DebugMethodStart();

            // 実績の存在チェック
            if (this.form.cdgrid_Jisseki.Rows.Count <= 1)
            {
                this.form.messageShowLogic.MessageBoxShow("E001", "実績タブの明細行");
                return true;
            }

            // チェック項目の設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            existCheck.DisplayMessage = "処分終了日が指定されていません";
            Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
            excitChecks.Add(existCheck);
            this.form.cdgrid_Jisseki.SuspendLayout();

            foreach (DataGridViewRow o in this.form.cdgrid_Jisseki.Rows)
            {
                PropertyUtility.SetValue(o.Cells["SyobunEndDate"], "RegistCheckMethod", excitChecks);
            }
            this.form.cdgrid_Jisseki.ResumeLayout();

            // チェックを行う
            var allControlAndHeaderControls = allControl.ToList();
            allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headerform));
            var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            // チェック項目の初期化
            this.form.cdgrid_Jisseki.SuspendLayout();
            foreach (DataGridViewRow o in this.form.cdgrid_Jisseki.Rows)
            {
                PropertyUtility.SetValue(o.Cells["SyobunEndDate"], "RegistCheckMethod", null);
            }
            this.form.cdgrid_Jisseki.ResumeLayout();

            if (this.form.RegistErrorFlag)
            {
                //必須チェックエラーフォーカス処理
                this.SetErrorFocus();

                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }
        // 20140617 kayo EV004840 全ての処分終了が入力されないと紐付不可のように修正 end

        // 20141103 koukouei 委託契約チェック start
        #region ブランクチェック
        /// <summary>
        /// ブランクチェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckBlank()
        {
            var msgLogic = new MessageBoxShowLogic();
            bool isBlank = true;
            string haikiShuruiCd = "";

            // 廃棄物種類チェック
            foreach (DataGridViewRow row in this.form.cdgrid_Jisseki.Rows)
            {
                if (row.IsNewRow)
                {
                    if (this.form.cdgrid_Jisseki.Rows.Count <= 1)
                    {
                        // 実績タブの明細行に空行が１行のみある場合
                        isBlank = false;
                    }
                    break;
                }

                if (maniFlag == 2)
                {
                    isBlank = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)) && cell.Visible)
                        {
                            isBlank = true;
                        }
                    }
                }

                haikiShuruiCd = Convert.ToString(row.Cells["HaikiCd"].Value);
                if (!string.IsNullOrEmpty(haikiShuruiCd))
                {
                    isBlank = false;
                }
            }

            if (isBlank)
            {
                msgLogic.MessageBoxShow("E176");
                return false;
            }

            return true;
        }
        #endregion

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

                CustomAlphaNumTextBox txtGyoushaCd = this.form.cantxt_HaisyutuGyousyaCd;
                CustomAlphaNumTextBox txtGenbaCd = this.form.cantxt_HaisyutuJigyoubaName;
                CustomDateTimePicker txtSagyouDate = this.form.cdate_KohuDate;
                CustomDataGridView gridDetail = this.form.cdgrid_Jisseki;
                string CTL_NAME_DETAIL = "HaikiCd";
                string CTL_NAME_DETAIL_NAME = "HaikiSyuruiName";

                //委託契約チェックDtoを取得
                ItakuCheckDTO checkDto = new ItakuCheckDTO();
                checkDto.MANIFEST_FLG = true;
                checkDto.HAIKI_KBN_CD = Convert.ToInt32(FormHaikiKbn);
                checkDto.GYOUSHA_CD = txtGyoushaCd.Text;
                checkDto.GENBA_CD = txtGenbaCd.Text;
                checkDto.SAGYOU_DATE = txtSagyouDate.Text;
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

                //委託契約チェック
                ItakuErrorDTO error = itakuLogic.ItakuKeiyakuCheck(checkDto);

                //エラーなし
                if (error.ERROR_KBN == (short)ITAKU_ERROR_KBN.NONE)
                {
                    return true;
                }

                bool ret = itakuLogic.ShowError(error, sysInfo.ITAKU_KEIYAKU_ALERT_AUTH, checkDto.MANIFEST_FLG, txtGyoushaCd, txtGenbaCd, txtSagyouDate, gridDetail, CTL_NAME_DETAIL);
                return ret;
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

        #region 背景色設定
        /// <summary>
        /// 背景色設定
        /// </summary>
        /// <returns></returns>
        internal bool SetBackColor(ItakuErrorDTO dto)
        {
            bool errorFlg = false;
            this.form.cdate_KohuDate.IsInputErrorOccured = false;
            this.form.cdate_KohuDate.BackColor = Constans.NOMAL_COLOR;
            this.form.cantxt_HaisyutuGyousyaCd.IsInputErrorOccured = false;
            this.form.cantxt_HaisyutuGyousyaCd.BackColor = Constans.NOMAL_COLOR;
            this.form.cantxt_HaisyutuJigyoubaName.IsInputErrorOccured = false;
            this.form.cantxt_HaisyutuJigyoubaName.BackColor = Constans.NOMAL_COLOR;
            foreach (DataGridViewRow row in this.form.cdgrid_Jisseki.Rows)
            {
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["HaikiCd"], false);
                row.Cells["HaikiCd"].Style.BackColor = Constans.NOMAL_COLOR;
            }

            if (dto.ERROR_KBN == (short)ITAKU_ERROR_KBN.YUUKOU_KIKAN)
            {
                errorFlg = true;
                this.form.cdate_KohuDate.IsInputErrorOccured = true;
                this.form.cdate_KohuDate.BackColor = Constans.ERROR_COLOR;
            }
            if (dto.ERROR_KBN == (short)ITAKU_ERROR_KBN.GYOUSHA)
            {
                errorFlg = true;
                this.form.cantxt_HaisyutuGyousyaCd.IsInputErrorOccured = true;
                this.form.cantxt_HaisyutuGyousyaCd.BackColor = Constans.ERROR_COLOR;
            }
            if (dto.ERROR_KBN == (short)ITAKU_ERROR_KBN.GENBA_BLANK ||
                dto.ERROR_KBN == (short)ITAKU_ERROR_KBN.GENBA_NOT_FOUND)
            {
                errorFlg = true;
                this.form.cantxt_HaisyutuJigyoubaName.IsInputErrorOccured = true;
                this.form.cantxt_HaisyutuJigyoubaName.BackColor = Constans.ERROR_COLOR;
            }
            if (dto.ERROR_KBN == (short)ITAKU_ERROR_KBN.HOUKOKUSHO_BUNRUI)
            {
                errorFlg = true;

                List<string> listDetailCd = new List<string>();
                if (dto.DETAIL_ERROR != null && dto.DETAIL_ERROR.Count > 0)
                {
                    listDetailCd = dto.DETAIL_ERROR.Select(s => s.CD).ToList();
                }

                foreach (DataGridViewRow row in this.form.cdgrid_Jisseki.Rows)
                {
                    if (listDetailCd.Contains(Convert.ToString(row.Cells["HaikiCd"].Value)))
                    {
                        ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["HaikiCd"], true);
                        row.Cells["HaikiCd"].Style.BackColor = Constans.ERROR_COLOR;
                    }
                }
            }
            return errorFlg;
        }
        #endregion
        // 20141103 koukouei 委託契約チェック end

        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start

        /// <summary>
        /// マニフェスト、マニフェスト明細へ論理削除データを登録する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        private void LogicalDelEntityInsert(string SYSTEM_ID, string SEQ)
        {
            T_MANIFEST_ENTRY entryDto = new T_MANIFEST_ENTRY();
            entryDto.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            entryDto.SEQ = Convert.ToInt32(SEQ);
            T_MANIFEST_ENTRY retDto = this.ManifestEntryDao.GetDataByPrimaryKey(entryDto);

            if (retDto != null)
            {
                // マニフェスト
                SqlDateTime create_date = retDto.CREATE_DATE;
                string create_user = retDto.CREATE_USER;
                string create_pc = retDto.CREATE_PC;
                // 更新者情報設定
                var retEntry = new DataBinderLogic<r_framework.Entity.T_MANIFEST_ENTRY>(retDto);
                retEntry.SetSystemProperty(retDto, false);
                retDto.SEQ += 1;
                retDto.DELETE_FLG = SqlBoolean.Parse("true");
                retDto.CREATE_DATE = create_date;
                retDto.CREATE_USER = create_user;
                retDto.CREATE_PC = create_pc;
                this.ManifestEntryDao.Insert(retDto);

                // マニフェスト明細
                T_MANIFEST_DETAIL detailDto = new T_MANIFEST_DETAIL();
                detailDto.SYSTEM_ID = entryDto.SYSTEM_ID;
                detailDto.SEQ = entryDto.SEQ;
                List<T_MANIFEST_DETAIL> retList = this.ManifestDetailDao.GetDataListByPrimaryKey(detailDto);

                foreach (T_MANIFEST_DETAIL data in retList)
                {
                    data.SEQ = retDto.SEQ;
                    this.ManifestDetailDao.Insert(data);
                }

                // マニフェスト運搬
                T_MANIFEST_UPN upnEntry = new T_MANIFEST_UPN();
                upnEntry.SYSTEM_ID = entryDto.SYSTEM_ID;
                upnEntry.SEQ = entryDto.SEQ;
                List<T_MANIFEST_UPN> upnList = this.ManifestUpnDao.GetDataListByPrimaryKey(upnEntry);
                foreach (T_MANIFEST_UPN data in upnList)
                {
                    data.SEQ = retDto.SEQ;
                    this.ManifestUpnDao.Insert(data);
                }

                // マニフェスト印刷
                T_MANIFEST_PRT prtEntry = new T_MANIFEST_PRT();
                prtEntry.SYSTEM_ID = entryDto.SYSTEM_ID;
                prtEntry.SEQ = entryDto.SEQ;
                T_MANIFEST_PRT prtDto = this.ManifestPrtDao.GetDataByPrimaryKey(prtEntry);
                if (prtDto != null)
                {
                    prtDto.SEQ = retDto.SEQ;
                    this.ManifestPrtDao.Insert(prtDto);
                }
            }
        }

        /// <summary>
        /// マニフェスト返却日へ論理削除データを登録する
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="SEQ"></param>
        private void LogicalDelRetDateInsert(string SYSTEM_ID, string SEQ)
        {
            T_MANIFEST_RET_DATE entryDto = new T_MANIFEST_RET_DATE();
            entryDto.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            entryDto.SEQ = Convert.ToInt32(SEQ);
            T_MANIFEST_RET_DATE retDto = this.ManifestRetDateDao.GetDataByPrimaryKey(entryDto);
            if (retDto != null)
            {
                SqlDateTime create_date = retDto.CREATE_DATE;
                string create_user = retDto.CREATE_USER;
                string create_pc = retDto.CREATE_PC;
                // 更新者情報設定
                var retEntry = new DataBinderLogic<r_framework.Entity.T_MANIFEST_RET_DATE>(retDto);
                retEntry.SetSystemProperty(retDto, false);
                retDto.SEQ += 1;
                retDto.DELETE_FLG = SqlBoolean.Parse("true");
                retDto.CREATE_DATE = create_date;
                retDto.CREATE_USER = create_user;
                retDto.CREATE_PC = create_pc;
                this.ManifestRetDateDao.Insert(retDto);
            }
        }

        /// <summary>
        /// 登録者情報の設定
        /// </summary>
        private void UpdateCreateInfo(
            ref List<T_MANIFEST_ENTRY> entrylist,
            ref List<T_MANIFEST_RET_DATE> retdatelist,
            String SystemId,
            String Seq
            )
        {
            foreach (T_MANIFEST_ENTRY data in entrylist)
            {
                T_MANIFEST_ENTRY entryDto = new T_MANIFEST_ENTRY();
                entryDto.SYSTEM_ID = Convert.ToInt64(SystemId);
                entryDto.SEQ = Convert.ToInt32(Seq);
                T_MANIFEST_ENTRY retDto = this.ManifestEntryDao.GetDataByPrimaryKey(entryDto);
                if (retDto != null)
                {
                    data.CREATE_DATE = retDto.CREATE_DATE;
                    data.CREATE_USER = retDto.CREATE_USER;
                    data.CREATE_PC = retDto.CREATE_PC;
                }
            }

            foreach (T_MANIFEST_RET_DATE data in retdatelist)
            {
                T_MANIFEST_RET_DATE entryDto = new T_MANIFEST_RET_DATE();
                entryDto.SYSTEM_ID = Convert.ToInt64(SystemId);
                entryDto.SEQ = Convert.ToInt32(Seq);
                T_MANIFEST_RET_DATE retDto = this.ManifestRetDateDao.GetDataByPrimaryKey(entryDto);
                if (retDto != null)
                {
                    data.CREATE_DATE = retDto.CREATE_DATE;
                    data.CREATE_USER = retDto.CREATE_USER;
                    data.CREATE_PC = retDto.CREATE_PC;
                }
            }
        }

        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

        #region 項目表示設定
        /// <summary>
        /// 項目表示設定
        /// </summary>
        internal void SetVisible()
        {
            if (maniFlag == 2)
            {
                // 混合種類
                this.form.lbl_KongoSyurui.Visible = false;
                this.form.cantxt_KongoCd.Visible = false;
                this.form.ctxt_KongoName.Visible = false;
                this.form.cantxt_KongoCd.Text = "";
                this.form.ctxt_KongoName.Text = "";

                // 混合数量
                this.form.lbl_JissekiSuryo.Visible = false;
                this.form.cntxt_JissekiSuryo.Visible = false;
                this.form.cntxt_JissekiSuryo.Text = "";

                // 混合単位
                this.form.lbl_JissekiTani.Visible = false;
                this.form.canTxt_JissekiTaniCd.Visible = false;
                this.form.ctxt_JissekiTaniName.Visible = false;
                this.form.canTxt_JissekiTaniCd.Text = "";
                this.form.ctxt_JissekiTaniName.Text = "";

                // 割合の合計
                this.form.lbl_TotalWariai.Visible = false;
                this.form.ctxt_TotalWariai.Visible = false;
                this.form.ctxt_TotalWariai.Text = "0";
            }
            else
            {
                // 混合種類
                this.form.lbl_KongoSyurui.Visible = true;
                this.form.cantxt_KongoCd.Visible = true;
                this.form.ctxt_KongoName.Visible = true;

                // 混合数量
                this.form.lbl_JissekiSuryo.Visible = true;
                this.form.cntxt_JissekiSuryo.Visible = true;

                // 混合単位
                this.form.lbl_JissekiTani.Visible = true;
                this.form.canTxt_JissekiTaniCd.Visible = true;
                this.form.ctxt_JissekiTaniName.Visible = true;

                // 割合の合計
                this.form.lbl_TotalWariai.Visible = true;
                this.form.ctxt_TotalWariai.Visible = true;
                SetTotal();
            }
        }
        #endregion

        #region Add popupWindowSetting

        private void AddPopupWindowSetting()
        {
            //荷姿 
            var dto = new JoinMethodDto()
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = WINDOW_ID.M_NISUGATA.ToString(),
                SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
            };

            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "KAMI_USE_KBN",
                Value = "True",
                ValueColumnType = r_framework.Const.DB_TYPE.BIT
            });
            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "TEKIYOU_FLG",
                Value = "FALSE"
            });

            this.form.cantxt_NisugataFreeCd01.popupWindowSetting.Add(dto);
            this.form.cantxt_NisugataFreeCd02.popupWindowSetting.Add(dto);
            this.form.cantxt_NisugataFreeCd03.popupWindowSetting.Add(dto);

            //処分方法 
            dto = new JoinMethodDto()
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = WINDOW_ID.M_SHOBUN_HOUHOU.ToString(),
                SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
            };

            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "KAMI_USE_KBN",
                Value = "True",
                ValueColumnType = r_framework.Const.DB_TYPE.BIT
            });
            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "TEKIYOU_FLG",
                Value = "FALSE"
            });

            this.form.cantxt_UnpanJigyobaFreeCd1.popupWindowSetting.Add(dto);
            this.form.cantxt_UnpanJigyobaFreeCd2.popupWindowSetting.Add(dto);
            this.form.cantxt_UnpanJigyobaFreeCd3.popupWindowSetting.Add(dto);
            this.form.cantxt_UnpanJigyobaFreeCd4.popupWindowSetting.Add(dto);
            this.form.cantxt_UnpanJigyobaFreeCd5.popupWindowSetting.Add(dto);

            //混合単位
            dto = new JoinMethodDto()
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = WINDOW_ID.M_UNIT.ToString(),
                SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
            };

            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "KAMI_USE_KBN",
                Value = "True",
                ValueColumnType = r_framework.Const.DB_TYPE.BIT
            });
            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "TEKIYOU_FLG",
                Value = "FALSE"
            });

            this.form.canTxt_JissekiTaniCd.popupWindowSetting.Add(dto);

            //運搬方法
            dto = new JoinMethodDto()
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = WINDOW_ID.M_UNPAN_HOUHOU.ToString(),
                SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
            };

            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "KAMI_USE_KBN",
                Value = "True",
                ValueColumnType = r_framework.Const.DB_TYPE.BIT
            });
            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                LeftColumn = "TEKIYOU_FLG",
                Value = "FALSE"
            });

            this.form.cantxt_UnpanHouhou1CD.popupWindowSetting.Add(dto);
            this.form.cantxt_UnpanHouhou2CD.popupWindowSetting.Add(dto);
        }

        #endregion

        /// <summary>
        /// 取引先チェック
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>true：正常　false：エラー</returns>
        public bool SetTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.cantxt_TorihikiCd.Text == string.Empty)
                {
                    this.form.ctxt_TorihikiName.Text = string.Empty;
                    return true;
                }

                M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = this.form.cantxt_TorihikiCd.Text;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();

                if (torihikisaki == null)
                {
                    this.form.ctxt_TorihikiName.Text = string.Empty;
                    this.form.messageShowLogic.MessageBoxShow("E020", "取引先");
                    this.form.cantxt_TorihikiCd.Focus();
                    this.form.cantxt_TorihikiCd.SelectAll();
                    this.form.cantxt_TorihikiCd.IsInputErrorOccured = true;
                    return false;
                }
                string kohuDate = ((BusinessBaseForm)this.form.ParentForm).sysDate.Date.ToString();
                if (this.form.cdate_KohuDate.Value != null)
                {
                    kohuDate = this.form.cdate_KohuDate.Value.ToString();
                }
                string begin = "0001/01/01";
                string end = "9999/12/31";
                if (!torihikisaki.TEKIYOU_BEGIN.IsNull)
                {
                    begin = torihikisaki.TEKIYOU_BEGIN.Value.ToString();
                }
                if (!torihikisaki.TEKIYOU_END.IsNull)
                {
                    end = torihikisaki.TEKIYOU_END.Value.ToString();
                }
                if (kohuDate.CompareTo(begin) < 0 || kohuDate.CompareTo(end) > 0)
                {
                    this.form.ctxt_TorihikiName.Text = string.Empty;
                    this.form.messageShowLogic.MessageBoxShow("E020", "取引先");
                    this.form.cantxt_TorihikiCd.Focus();
                    this.form.cantxt_TorihikiCd.SelectAll();
                    this.form.cantxt_TorihikiCd.IsInputErrorOccured = true;
                    return false;
                }
                //this.form.cantxt_TorihikiCd.IsInputErrorOccured = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisaki", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }

        /// <summary>
        /// 荷姿チェック
        /// </summary>
        /// <param name="genba">荷姿CD</param>
        /// <param name="catchErr"></param>
        /// <returns>true：正常　false：エラー</returns>
        public bool ChkNisugata(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(e);

                int row = e.RowIndex;
                int col = e.ColumnIndex;
                var nisugataCd = Convert.ToString(this.form.cdgrid_Jisseki.Rows[row].Cells[col].Value);
                if (string.IsNullOrEmpty(nisugataCd))
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["NisugataName"].Value = string.Empty;
                    return ret;
                }

                M_NISUGATA keyEntity = new M_NISUGATA();
                keyEntity.NISUGATA_CD = nisugataCd;
                keyEntity.KAMI_USE_KBN = true;
                var nisugata = this.IMNisugataDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (nisugata != null)
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["NisugataName"].Value = nisugata.NISUGATA_NAME_RYAKU;
                }
                else
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["NisugataName"].Value = string.Empty;
                    this.form.messageShowLogic.MessageBoxShow("E020", "荷姿");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkNisugata", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                e.Cancel = true;
                ret = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 処分方法チェック
        /// </summary>
        /// <returns>true：正常　false：エラー</returns>
        public bool ChkSyobunCd(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(e);

                int row = e.RowIndex;
                int col = e.ColumnIndex;
                var cd = Convert.ToString(this.form.cdgrid_Jisseki.Rows[row].Cells[col].Value);
                if (string.IsNullOrEmpty(cd))
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["SyobunName"].Value = string.Empty;
                    return ret;
                }

                M_SHOBUN_HOUHOU keyEntity = new M_SHOBUN_HOUHOU();
                keyEntity.SHOBUN_HOUHOU_CD = cd;
                keyEntity.KAMI_USE_KBN = true;
                var shobunHouhou = this.IMShobunHouhouDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (shobunHouhou != null)
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["SyobunName"].Value = shobunHouhou.SHOBUN_HOUHOU_NAME_RYAKU;
                }
                else
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["SyobunName"].Value = string.Empty;
                    this.form.messageShowLogic.MessageBoxShow("E020", "処分方法");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkSyobunCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                e.Cancel = true;
                ret = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 単位チェック
        /// </summary>
        /// <returns>true：正常　false：エラー</returns>
        public bool ChkTaniCd(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(e);

                int row = e.RowIndex;
                int col = e.ColumnIndex;
                var cd = Convert.ToString(this.form.cdgrid_Jisseki.Rows[row].Cells[col].Value);
                if (string.IsNullOrEmpty(cd))
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["TaniName"].Value = string.Empty;
                    return ret;
                }

                M_UNIT keyEntity = new M_UNIT();
                short intCd = 0;
                short.TryParse(cd, out intCd);
                keyEntity.UNIT_CD = intCd;
                keyEntity.KAMI_USE_KBN = true;
                var shobunHouhou = this.IMUnitDao.GetAllValidData(keyEntity).FirstOrDefault();
                if (shobunHouhou != null)
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["TaniName"].Value = shobunHouhou.UNIT_NAME_RYAKU;
                }
                else
                {
                    this.form.cdgrid_Jisseki.Rows[row].Cells["TaniName"].Value = string.Empty;
                    this.form.messageShowLogic.MessageBoxShow("E020", "単位");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkTaniCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                e.Cancel = true;
                ret = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // 20160510 koukoukon #12041 定期配車関連の全面見直し start
        /// <summary>
        /// 廃棄物種類の設定(定期実績)
        /// </summary>
        public void SetGridDataForRenkeiForTeikiJisseki(DataTable dt)
        {

            this.form.isClearForm = true;
            this.form.cdgrid_Jisseki.Rows.Clear();
            this.form.isClearForm = false;
            this.form.cdgrid_Jisseki.RowCount = dt.Rows.Count + 1;
            //廃棄物種類CD
            this.form.cdgrid_Jisseki.Rows[dt.Rows.Count].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value = null;
            //廃棄物種類名
            this.form.cdgrid_Jisseki.Rows[dt.Rows.Count].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value = null;

            int j = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["HAIKI_SHURUI_CD"].ToString()))
                {
                    //廃棄物種類CD
                    this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value = dt.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    //廃棄物種類名
                    this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiSyuruiName].Value = dt.Rows[i]["HAIKI_SHURUI_NAME_RYAKU"].ToString();
                    //定期配車実績の「単位」にkgが設定されている場合
                    if (dt.Rows[i]["UNIT_CD"].ToString() == "3")
                    {
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value = dt.Rows[i]["SUURYOU"].ToString();
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = dt.Rows[i]["UNIT_CD"].ToString();
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = dt.Rows[i]["UNIT_NAME"].ToString();
                    }
                    //定期配車実績の「換算後単位」にkgが設定されている場合
                    if (dt.Rows[i]["KANSAN_UNIT_CD"].ToString() == "3")
                    {
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value = dt.Rows[i]["KANSAN_SUURYOU"].ToString();
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value = dt.Rows[i]["KANSAN_UNIT_CD"].ToString();
                        this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniName].Value = dt.Rows[i]["KANSAN_UNIT_NAME"].ToString();
                    }

                    //換算値、減容値
                    if (!String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value))
                        && !String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value))
                        && !String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value)))
                    {
                        //換算後数量
                        if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)))
                        {
                            this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value = (0).ToString(this.ManifestSuuryoFormat);
                        }

                        //減算後数量
                        if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value)))
                        {
                            this.form.cdgrid_Jisseki.Rows[j].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value = (0).ToString(this.ManifestSuuryoFormat);
                        }
                    }

                    //換算値、減容値の順に再計算
                    if (!this.SetKansanti(j)) { return; }
                    if (!this.SetGenyouti(j)) { return; }

                    j++;
                }
            }
            this.form.cdgrid_Jisseki.RowCount = j + 1;

            this.form.cdgrid_Jisseki.RefreshEdit();

            //数値計算
            if (!this.SetTotal()) { return; }

            //数値フォーマット適用
            this.SetNumFormst();
        }

        /// <summary>
        ///  画面の連携項目をクリアする(定期実績)
        /// </summary>
        private void ClearGamenItemForTeikiJisseki()
        {
            //交付年月日
            this.form.cdate_KohuDate.Text = string.Empty;
            //運搬終了年月日
            this.form.cdate_UnpanJyu1.Text = string.Empty;
            //運搬受託者CD
            this.form.cantxt_UnpanJyutaku1NameCd.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);
            //運搬方法
            this.form.cantxt_UnpanHouhou1CD.Text = string.Empty;
            this.form.ctxt_UnpanHouhouMei1.Text = string.Empty;
            //運搬の受託（上）CD
            this.form.cantxt_UnpanJyuCd1.Text = string.Empty;
            this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);
            //運搬の受託（下）CD
            this.form.cantxt_UnpanJyuUntenCd1.Text = string.Empty;
            ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);
            //運搬受託者の車種CD
            this.form.cantxt_Jyutaku1Syasyu.Text = string.Empty;
            ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);
            //運搬受託者の車輌CD
            this.form.cantxt_Jyutaku1SyaNo.Text = string.Empty;
            this.form.ctxt_Jyutaku1SyaNo.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.form.cantxt_Meisaigyou.Text))
            {
                //排出事業者CD
                this.form.cantxt_HaisyutuGyousyaCd.Text = string.Empty;
                this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);
                //排出事業場CD
                this.form.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);
                //処分受託者CD
                this.form.cantxt_SyobunJyutakuNameCd.Text = string.Empty;
                this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                //運搬先の事業場CD
                this.form.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                //処分の受託（上）CD
                this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);
            }
        }
        // 20160510 koukoukon #12041 定期配車関連の全面見直し end

        /// <summary>
        /// 入力制限
        /// </summary>
        public void SetUnpanJyutaku2Enable()
        {
            if (!this.form.cckb_Jyutaku1HokanAri.Checked)
            {
                // 運搬業者(２)
                this.form.cnt_UnpanJyutaku2Zip.Enabled = false;
                this.form.cbtn_UnpanJyutaku2San.Enabled = false;
                this.form.cbtn_UnpanJyutaku2Del.Enabled = false;
                this.form.cnt_UnpanJyutaku2Tel.Enabled = false;
                this.form.ctxt_UnpanJyutaku2Add.Enabled = false;
                this.form.cantxt_UnpanJyutaku2NameCd.Enabled = false;
                this.form.cantxt_UnpanJyutaku2Name.Enabled = false;
                this.form.casbtn_UnpanJyutaku2.Enabled = false;
                this.form.ctxt_Jyutaku2SyaNo.Enabled = false;
                this.form.ctxt_Jyutaku2Syasyu.Enabled = false;
                this.form.cantxt_UnpanHouhou2CD.Enabled = false;
                this.form.ctxt_UnpanHouhouMei2.Enabled = false;
                this.form.cckb_Jyutaku2HokanAri.Enabled = false;
                this.form.cckb_Jyutaku2HokanNasi.Enabled = false;
                this.form.cantxt_Jyutaku2SyaNo.Enabled = false;
                this.form.cantxt_Jyutaku2Syasyu.Enabled = false;

                // 積替え又は保管
                this.form.ctxt_TumiGyoName.Enabled = false;
                this.form.ctxt_TumiHokaName.Enabled = false;
                this.form.cantxt_TumiGyoCd.Enabled = false;
                this.form.cbtn_TumiGyo.Enabled = false;
                this.form.cantxt_TumiHokaNameCd.Enabled = false;
                this.form.cbtn_TumiHokaName.Enabled = false;
                this.form.cnt_TumiHokaZip.Enabled = false;
                this.form.cbtn_TumiHokaSan.Enabled = false;
                this.form.cbtn_TumiHokaDel.Enabled = false;
                this.form.cnt_TumiHokaTel.Enabled = false;
                this.form.ctxt_TumiHokaAdd.Enabled = false;
                this.form.cckb_TumiHokaAri.Enabled = false;
                this.form.cckb_TumiHokaNasi.Enabled = false;
                this.form.cntxt_TumiHokaJisseki.Enabled = false;
                this.form.cckb_TumiHokaT.Enabled = false;
                this.form.cckb_TumiHokaM3.Enabled = false;

                // 運搬の受託（2）
                this.form.cantxt_UnpanJyuCd2.Enabled = false;
                this.form.ctxt_UnpanJyuName2.Enabled = false;
                this.form.cantxt_UnpanJyuUntenCd2.Enabled = false;
                this.form.cantxt_UnpanJyuUntenName2.Enabled = false;
                this.form.customButton2.Enabled = false;
                this.form.cdate_UnpanJyu2.Enabled = false;
            }
            else
            {
                // 運搬業者(２)
                if (!this.form.lineShape8.Visible)
                {
                    this.form.cnt_UnpanJyutaku2Zip.Enabled = true;
                    this.form.cbtn_UnpanJyutaku2San.Enabled = true;
                    this.form.cbtn_UnpanJyutaku2Del.Enabled = true;
                    this.form.cnt_UnpanJyutaku2Tel.Enabled = true;
                    this.form.ctxt_UnpanJyutaku2Add.Enabled = true;
                    this.form.cantxt_UnpanJyutaku2NameCd.Enabled = true;
                    this.form.cantxt_UnpanJyutaku2Name.Enabled = true;
                    this.form.casbtn_UnpanJyutaku2.Enabled = true;
                    this.form.ctxt_Jyutaku2SyaNo.Enabled = true;
                    this.form.ctxt_Jyutaku2Syasyu.Enabled = true;
                    this.form.cantxt_UnpanHouhou2CD.Enabled = true;
                    this.form.ctxt_UnpanHouhouMei2.Enabled = true;
                    this.form.cckb_Jyutaku2HokanAri.Enabled = true;
                    this.form.cckb_Jyutaku2HokanNasi.Enabled = true;
                    this.form.cantxt_Jyutaku2SyaNo.Enabled = true;
                    this.form.cantxt_Jyutaku2Syasyu.Enabled = true;
                }

                // 積替え又は保管
                if (!this.form.lineShape7.Visible)
                {
                    this.form.ctxt_TumiGyoName.Enabled = true;
                    this.form.ctxt_TumiHokaName.Enabled = true;
                    this.form.cantxt_TumiGyoCd.Enabled = true;
                    this.form.cbtn_TumiGyo.Enabled = true;
                    this.form.cantxt_TumiHokaNameCd.Enabled = true;
                    this.form.cbtn_TumiHokaName.Enabled = true;
                    this.form.cnt_TumiHokaZip.Enabled = true;
                    this.form.cbtn_TumiHokaSan.Enabled = true;
                    this.form.cbtn_TumiHokaDel.Enabled = true;
                    this.form.cnt_TumiHokaTel.Enabled = true;
                    this.form.ctxt_TumiHokaAdd.Enabled = true;
                    this.form.cckb_TumiHokaAri.Enabled = true;
                    this.form.cckb_TumiHokaNasi.Enabled = true;
                    this.form.cntxt_TumiHokaJisseki.Enabled = true;
                    this.form.cckb_TumiHokaT.Enabled = true;
                    this.form.cckb_TumiHokaM3.Enabled = true;
                }

                // 運搬の受託（2）
                if (!this.form.lineShape10.Visible)
                {
                    this.form.cantxt_UnpanJyuCd2.Enabled = true;
                    this.form.ctxt_UnpanJyuName2.Enabled = true;
                    this.form.cantxt_UnpanJyuUntenCd2.Enabled = true;
                    this.form.cantxt_UnpanJyuUntenName2.Enabled = true;
                    this.form.customButton2.Enabled = true;
                    this.form.cdate_UnpanJyu2.Enabled = true;
                }
            }
        }

        /// <summary>
        /// レイアウト区分制限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutKbn_Validated(object sender, EventArgs e)
        {
            var LayoutKbn = manifestMercuryList[1] as CustomNumericTextBox2;

            if (LayoutKbn.Text.Equals("1"))
            {
                this.form.cantxt_KanriFreeCd01.Visible = true;
                this.form.ctxt_KanriFreeName01.Visible = true;
                this.form.cantxt_KanriFreeCd02.Visible = true;
                this.form.ctxt_KanriFreeName02.Visible = true;
            }
            else if (LayoutKbn.Text.Equals("2"))
            {
                if (!string.IsNullOrEmpty(this.form.cantxt_KanriFreeCd01.Text)
                    || !string.IsNullOrEmpty(this.form.cantxt_KanriFreeCd02.Text))
                {
                    DialogResult ret = this.MsgBox.MessageBoxShowConfirm("水銀対応マニフェストの場合、管理型品目の「17．石綿含有産業廃棄物」以下の項目は印字できません。よろしいですか？");
                    if (ret == DialogResult.Yes)
                    {
                        this.form.cckb_KanriFree01.Checked = false;
                        this.form.cantxt_KanriFreeCd01.Text = "";
                        this.form.ctxt_KanriFreeName01.Text = "";
                        this.form.cckb_KanriFree02.Checked = false;
                        this.form.cantxt_KanriFreeCd02.Text = "";
                        this.form.ctxt_KanriFreeName02.Text = "";

                        this.form.cantxt_KanriFreeCd01.Visible = false;
                        this.form.ctxt_KanriFreeName01.Visible = false;
                        this.form.cantxt_KanriFreeCd02.Visible = false;
                        this.form.ctxt_KanriFreeName02.Visible = false;
                    }
                    else
                    {
                        (manifestMercuryList[1] as CustomNumericTextBox2).Text = "1";
                        LayoutKbnCheck(manifestMercuryList[1].Text);
                    }
                }
                else
                {
                    this.form.cckb_KanriFree01.Checked = false;
                    this.form.cckb_KanriFree02.Checked = false;

                    this.form.cantxt_KanriFreeCd01.Visible = false;
                    this.form.ctxt_KanriFreeName01.Visible = false;
                    this.form.cantxt_KanriFreeCd02.Visible = false;
                    this.form.ctxt_KanriFreeName02.Visible = false;
                }
            }
        }

        /// <summary>
        /// レイアウト区分を変更する
        /// </summary>
        /// <param name="LayoutKbnText"></param>
        public void LayoutKbnCheck(string LayoutKbnText)
        {
            if (LayoutKbnText.Equals("1"))
            {
                manifestMercuryList[2].Text = Constans.REIAUTO_KBN_1;

                this.form.cantxt_KanriFreeCd01.Visible = true;
                this.form.ctxt_KanriFreeName01.Visible = true;
                this.form.cantxt_KanriFreeCd02.Visible = true;
                this.form.ctxt_KanriFreeName02.Visible = true;
            }
            else
            {
                manifestMercuryList[2].Text = Constans.REIAUTO_KBN_2;

                this.form.cantxt_KanriFreeCd01.Visible = false;
                this.form.ctxt_KanriFreeName01.Visible = false;
                this.form.cantxt_KanriFreeCd02.Visible = false;
                this.form.ctxt_KanriFreeName02.Visible = false;
            }
        }

        /// <summary>
        /// ﾏﾆ用紙へ印字区分を変更する
        /// </summary>
        /// <param name="LayoutKbnText"></param>
        public void LayoutKamiKbnCheck(string LayoutKamiKbnText)
        {
            if (LayoutKamiKbnText.Equals("1"))
            {
                manifestKamiList[2].Text = Constans.MANI_KBN_1;
            }
            else
            {
                manifestKamiList[2].Text = Constans.MANI_KBN_2;
            }
        }

        /// <summary>
        /// ２次マニ情報から紐付情報の有無と最終処分終了日の比較を行う。
        /// </summary>
        public bool CheckLastSbnDate()
        {
            bool ret = false;

            foreach (DataGridViewRow o in this.form.cdgrid_Jisseki.Rows)
            {
                // 新規行は除外
                if (o.IsNewRow || o.Cells["DetailSystemID"].Value == null)
                {
                    continue;
                }

                // 電子１次マニに紐付されているかチェックする。
                DataTable firstManiData = this.mlogic.SelectFirstManiSystemID(o.Cells["DetailSystemID"].Value.ToString(), this.FormHaikiKbn);
                if (firstManiData.Rows.Count != 0)
                {
                    for (int i = 0; i < firstManiData.Rows.Count; i++)
                    {
                        // 電子１次マニの最終処分終了日が設定されている、かつ２次マニの最終処分終了日が設定済の場合
                        // メッセージを表示するためフラグをtrueに設定する。
                        if (!string.IsNullOrEmpty(firstManiData.Rows[i]["LAST_SBN_END_DATE"].ToString())
                            && o.Cells["SaisyuSyobunEndDate"].Value != null)
                        {
                            if (!firstManiData.Rows[i]["LAST_SBN_END_DATE"].ToString().Equals(Convert.ToDateTime(o.Cells["SaisyuSyobunEndDate"].Value).ToString("yyyyMMdd")))
                            {
                                ret = true;
                            }
                        }
                    }
                }
            }

            return ret;
        }
        /// <summary>
        /// 廃棄物種類の設定
        /// </summary>
        public void SetRenkeiHaikiSyuruiToForm(DataTable dt)
        {
            if (decimal.Parse(dt.Rows[0]["NET_TOTAL"].ToString()) > 0)
            {
                //混合数量(連携伝票の"正味合計"、混合単位"3:kg"をセット
                this.form.cntxt_JissekiSuryo.Text = dt.Rows[0]["NET_TOTAL"].ToString();
                this.form.canTxt_JissekiTaniCd.Text = "3";

                //混合割振
                this.SetSuu();

                //混合数量・混合単位を初期化(行指定で遷移した時に割合が100にならない為)
                this.form.cntxt_JissekiSuryo.Text = string.Empty;
                this.form.canTxt_JissekiTaniCd.Text = string.Empty;
                //トータル割合
                this.form.ctxt_TotalWariai.Text = "0";

                //明細割合初期化
                for (int i = 0; i < this.form.cdgrid_Jisseki.Rows.Count; i++)
                {
                    this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Wariai].Value = null;

                    if (!String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.HaikiCd].Value))
                         && !String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.Suryo].Value))
                         && !String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.TaniCd].Value)))
                    {
                        //換算後数量
                        if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value)))
                        {
                            this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.KansangoSuryo].Value = (0).ToString(this.ManifestSuuryoFormat);
                        }

                        //減算後数量
                        if (String.IsNullOrEmpty(Convert.ToString(this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value)))
                        {
                            this.form.cdgrid_Jisseki.Rows[i].Cells[(int)KenpaiManifestoNyuryoku.enumCols.GenyoyugoTotalSuryo].Value = (0).ToString(this.ManifestSuuryoFormat);
                        }
                    }


                }
                this.form.cdgrid_Jisseki.RefreshEdit();
            }
        }
        /// <summary>
        /// モバイルオプション用初期化処理
        /// </summary>
        private void MobileInit()
        {
            if (this.form.ismobile_mode && this.maniFlag == 1)
            {
                this.form.pl_Mobile.Visible = true;
                this.form.cantxt_Renkei_Mode.Visible = true;

                //this.form.cbtn_Previous.Left = 901;
                //this.form.cbtn_Next.Left = 931;
            }
            else
            {
                this.form.pl_Mobile.Visible = false;
                this.form.cantxt_Renkei_Mode.Visible = false;

            }

            if (!this.form.ismobile_mode)
            {
                this.form.cbtn_Previous.Left = 561;
                this.form.cbtn_Next.Left = 590;
                this.form.pl_Mobile_Pre.Visible = false;
                this.form.pl_Mobile_next.Visible = false;
            }
            else
            {
                this.form.pl_Mobile_Pre.Visible = true;
                this.form.pl_Mobile_next.Visible = true;
            }
        }

        #region INXS処理 refs #158004

        private void Parentform_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!AppConfig.AppOptions.IsInxsManifest())
                {
                    return;
                }
                this.inxsManifestLogic.HandleResponse(message, this.form.transactionId);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

        #region refs #159110
        /// <summary>
        /// 画面項目(横連携用)の値を設定
        /// </summary>
        public void SetValueToFormRenkeiData(DataTable dt,object ikbn)
        {
            M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
            M_GYOUSHA gyousha = new M_GYOUSHA();
            M_GENBA genba = new M_GENBA();
            bool tsumikaeKbn = false;
            int iKbn;
            iKbn = Convert.ToInt32(this.form.cantxt_DenshuKbn.Text);

            // 伝種区分が受入の場合
            if (iKbn == (int)DENSHU_KBN.UKEIRE)
            {
                // 取引先
                if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                {
                    torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                }
                // 業者
                if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value)
                {
                    gyousha = this.GetGyoushaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString());
                }
                // 現場
                if (dt.Rows[0]["NIOROSHI_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["NIOROSHI_GENBA_CD"] != DBNull.Value)
                {
                    genba = this.GetGenbaEntity(dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString(), dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString(), out tsumikaeKbn);
                }

                //交付年月日
                this.form.cdate_KohuDate.Value = dt.Rows[0]["DENPYOU_DATE"];

                if (torihikisaki != null)
                {
                    //取引先
                    this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    //取引先名
                    this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                }

                //排出事業者
                this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                //排出事業場
                this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["GENBA_CD"].ToString();
                this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                //運搬受託者（１）
                this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                if (tsumikaeKbn)
                {
                    //積替え又は保管業者
                    this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                }
                else
                {
                    //処分受託者
                    this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                }

                if (genba != null && genba.TSUMIKAEHOKAN_KBN)
                {
                    //積替え又は保管名称
                    this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                }
                else
                {
                    //運搬先の事業場
                    this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                }

                //運搬の受託（１）上
                this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);

                //運搬受託者（１）車種
                this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                this.form.ctxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_NAME"].ToString();

                //運搬受託者（１）車輌
                this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                //運搬受託者（１）車輌名
                this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                //運搬の受託（１）下
                this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                this.form.cantxt_UnpanJyuUntenName1.Text = dt.Rows[0]["UNTENSHA_NAME"].ToString();

                if (gyousha != null
                    && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                    || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                    || !tsumikaeKbn))
                {
                    //処分の受託（受領）上
                    this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                    //処分の受託（処分）上
                    this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                }
                else
                {
                    this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                    this.form.ctxt_SyobunJyuName.Text = string.Empty;
                    this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                    this.form.ctxt_SyobunSyoName.Text = string.Empty;
                }

                //運搬の受託（１）
                this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["DENPYOU_DATE"];

                //処分の受託（受領）
                if (!tsumikaeKbn)
                {
                    this.form.cdate_SyobunJyu.Value = dt.Rows[0]["DENPYOU_DATE"];
                }
                else
                {
                    // 処分の受託（受領）をクリア
                    this.SyobunJyuCDDel();
                }
            }
            // 伝種区分が計量の場合
            if (iKbn == (int)DENSHU_KBN.KEIRYOU)
            {
                //交付年月日
                this.form.cdate_KohuDate.Value = dt.Rows[0]["DENPYOU_DATE"];

                if (dt.Rows[0]["TORIHIKISAKI_CD"] != DBNull.Value)
                {
                    torihikisaki = this.GetTorihikisakiEntity(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                }
                if (torihikisaki != null)
                {
                    //取引先CD
                    this.form.cantxt_TorihikiCd.Text = dt.Rows[0]["TORIHIKISAKI_CD"].ToString();
                    //取引先名
                    this.form.ctxt_TorihikiName.Text = dt.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                }

                // 一次マニフェストの場合
                if (maniFlag == 1)
                {
                    //排出事業者
                    this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["HAISYUTU_GYOUSHA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                    //排出事業場
                    this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["HAISYUTU_GENBA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                    if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["UNPAN_GENBA_CD"] != DBNull.Value)
                    {
                        genba = this.GetGenbaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString(), dt.Rows[0]["UNPAN_GENBA_CD"].ToString(), out tsumikaeKbn);
                    }

                    if (tsumikaeKbn)
                    {
                        //積替え又は保管の業者
                        this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                    }
                    else
                    {
                        //処分受託者
                        this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                    }

                    if (genba != null)
                    {
                        if (genba.TSUMIKAEHOKAN_KBN)
                        {
                            //積替え又は保管の名称
                            this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                        }
                        else
                        {
                            //運搬先の事業場
                            this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        }
                    }

                    if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value)
                    {
                        gyousha = this.GetGyoushaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString());
                    }
                    if (gyousha != null
                        && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                        || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                        || !tsumikaeKbn))
                    {
                        //処分の受託（受領）上
                        this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                        //処分の受託（処分）上
                        this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                    }
                    else
                    {
                        this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                        this.form.ctxt_SyobunJyuName.Text = string.Empty;
                        this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                        this.form.ctxt_SyobunSyoName.Text = string.Empty;
                    }
                }
                else
                {
                    //排出事業者
                    this.form.cantxt_HaisyutuGyousyaCd.Text = dt.Rows[0]["HAISYUTU_GYOUSHA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_HaisyutuGyousyaCd.Name, this.form.cantxt_HaisyutuGyousyaCd.Text);

                    //排出事業場
                    this.form.cantxt_HaisyutuJigyoubaName.Text = dt.Rows[0]["HAISYUTU_GENBA_CD"].ToString();
                    this.ClearOrSetValue(this.form.cantxt_HaisyutuJigyoubaName.Name, this.form.cantxt_HaisyutuJigyoubaName.Text);

                    if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value && dt.Rows[0]["UNPAN_GENBA_CD"] != DBNull.Value)
                    {
                        genba = this.GetGenbaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString(), dt.Rows[0]["UNPAN_GENBA_CD"].ToString(), out tsumikaeKbn);
                    }

                    if (tsumikaeKbn)
                    {
                        //積替え又は保管の業者
                        this.form.cantxt_TumiGyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_TumiGyoCd.Name, this.form.cantxt_TumiGyoCd.Text);
                    }
                    else
                    {
                        //処分受託者
                        this.form.cantxt_SyobunJyutakuNameCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_SyobunJyutakuNameCd.Name, this.form.cantxt_SyobunJyutakuNameCd.Text);
                    }

                    if (genba != null)
                    {
                        if (genba.TSUMIKAEHOKAN_KBN)
                        {
                            //積替え又は保管の名称
                            this.form.cantxt_TumiHokaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_TumiHokaNameCd.Name, this.form.cantxt_TumiHokaNameCd.Text);
                        }
                        else
                        {
                            //運搬先の事業場
                            this.form.cantxt_UnpanJyugyobaNameCd.Text = dt.Rows[0]["UNPAN_GENBA_CD"].ToString();
                            this.ClearOrSetValue(this.form.cantxt_UnpanJyugyobaNameCd.Name, this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        }
                    }

                    if (dt.Rows[0]["SYOBUN_GYOUSHA_CD"] != DBNull.Value)
                    {
                        gyousha = this.GetGyoushaEntity(dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString());
                    }
                    if (gyousha != null
                        && (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsFalse
                        || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsFalse
                        || !tsumikaeKbn))
                    {
                        //処分の受託（受領）上
                        this.form.cantxt_SyobunJyuCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_SyobunJyuCd.Name, this.form.cantxt_SyobunJyuCd.Text);

                        //処分の受託（処分）上
                        this.form.cantxt_SyobunSyoCd.Text = dt.Rows[0]["SYOBUN_GYOUSHA_CD"].ToString();
                        this.ClearOrSetValue(this.form.cantxt_SyobunSyoCd.Name, this.form.cantxt_SyobunSyoCd.Text);
                    }
                    else
                    {
                        this.form.cantxt_SyobunJyuCd.Text = string.Empty;
                        this.form.ctxt_SyobunJyuName.Text = string.Empty;
                        this.form.cantxt_SyobunSyoCd.Text = string.Empty;
                        this.form.ctxt_SyobunSyoName.Text = string.Empty;
                    }
                }
                
                //運搬受託者（１）
                this.form.cantxt_UnpanJyutaku1NameCd.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                this.ClearOrSetValue(this.form.cantxt_UnpanJyutaku1NameCd.Name, this.form.cantxt_UnpanJyutaku1NameCd.Text);

                //運搬の受託（１）上
                this.form.cantxt_UnpanJyuCd1.Text = dt.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                this.ClearOrSetValue(this.form.cantxt_UnpanJyuCd1.Name, this.form.cantxt_UnpanJyuCd1.Text);

                //運搬の受託（１）下
                this.form.cantxt_UnpanJyuUntenCd1.Text = dt.Rows[0]["UNTENSHA_CD"].ToString();
                ManifestoLogic.ShainPopupAfterBase(this.form.cantxt_UnpanJyuUntenCd1);

                //運搬受託者（１）車種
                this.form.cantxt_Jyutaku1Syasyu.Text = dt.Rows[0]["SHASHU_CD"].ToString();
                ManifestoLogic.XxxPopupAfterBase(this.form.cantxt_Jyutaku1Syasyu, 0, null, null);

                //運搬受託者（１）車輌
                this.form.cantxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_CD"].ToString();
                //運搬受託者（１）車輌名
                this.form.ctxt_Jyutaku1SyaNo.Text = dt.Rows[0]["SHARYOU_NAME"].ToString();

                //運搬の受託（１）
                this.form.cdate_UnpanJyu1.Value = dt.Rows[0]["DENPYOU_DATE"];

                //処分の受託（受領）
                if (!tsumikaeKbn)
                {
                    this.form.cdate_SyobunJyu.Value = dt.Rows[0]["DENPYOU_DATE"];
                }
                else
                {
                    // 画面の処分の受託（受領）をクリア
                    this.SyobunJyuCDDel();
                }
            }
        }
        #endregion
    }
}
