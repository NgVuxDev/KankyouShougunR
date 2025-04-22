using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Enums;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Logic;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Setting.ButtonSetting.xml";
        private string ButtonInfoXmlPathForTuuchiRireki = "Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.Setting.ButtonSettingForTuuchiRireki.xml";
        ///<summary>
        ///ComponentResourceManager
        ///</summary>
        private ComponentResourceManager resources;

        /// <summary>
        ///処理区分：1新規、2参照、3削除、4更新
        /// </summary>
        public WINDOW_TYPE Mode { get; set; }

        /// <summary>
        /// パラメータ：システムID(更新、修正、参照のみ)
        /// </summary>
        public String KanriId { get; set; }

        /// <summary>
        /// パラメータ：該当KanriIDのSEQのStringフォマート(更新、修正、参照のみ)
        /// </summary>
        public string strSeq { get; set; }
        /// <summary>
        /// 最新マニ伝票のSEQ
        /// </summary>
        public string latestSeq { get; set; }
        /// <summary>
        /// 承認が発生しているマニ伝票のSEQ
        /// </summary>
        public string approvalSeq { get; set; }
        /// <summary>
        /// 最新マニ伝票のSEQと承認が発生しているマニ伝票のSEQ比較用FLAG
        /// </summary>
        public bool seqFlag { get; set; }
        /// <summary>
        /// 「通知履歴明細」画面の通知コード
        /// </summary>
        public string tuuchiCd { get; set; }
        /// <summary>
        /// 「通知履歴明細」画面から呼び出させるかどうか
        /// </summary>
        public bool tuuchiRirekiFlg = false;
        /// <summary>
        /// パラメータ：該当KanriIDのSEQ(更新、修正、参照のみ)
        /// </summary>
        public SqlInt16 Seq { get; set; }

        /// <summary>
        /// パタン一覧画面から伝送されたSYSTEM_ID
        /// </summary>
        public string Ptn_System_ID { get; set; }

        /// <summary>
        /// パタン一覧画面から伝送されたSYSTEM_ID関連SEQ
        /// </summary>
        public string Ptn_SEQ { get; set; }

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        public MessageBoxShowLogic msgLogic;

        /// <summary>
        ///電子マニフェストテーブル用Dao
        /// </summary>
        private DT_R18DaoCls DT_R18Dao;

        /// <summary>
        /// キュー情報[QUE_INFO]用DAO
        /// </summary>
        private QUE_INFODaoCls QUE_INFODao;

        /// <summary>
        ///マニフェスト目次情報[DT_MF_TOC]用Dao
        /// </summary>
        public DT_MF_TOCDaoCls DT_MF_TOCDao;

        /// <summary>
        ///加入者番号[DT_MF_MEMBER]用Dao
        /// </summary>
        internal DT_MF_MEMBERDaoCls DT_MF_MEMBERDao;

        /// <summary>
        /// 収集運搬情報[DT_R19]用Dao
        /// </summary>
        private DT_R19DaoCls DT_R19Dao;
        /// <summary>
        /// 有害物質情報[DT_R02]用Dao
        /// </summary>
        private DT_R02DaoCls DT_R02Dao;

        /// <summary>
        /// 最終処分事業場(予定)情報[DT_R04]用DAO
        /// </summary>
        private DT_R04DaoCls DT_R04Dao;

        /// <summary>
        /// 連絡番号情報[DT_R05]用DAO
        /// </summary>
        private DT_R05DaoCls DT_R05Dao;

        /// <summary>
        /// 備考情報[DT_R06]用DAO
        /// </summary>
        private DT_R06DaoCls DT_R06Dao;

        /// <summary>
        /// 最終処分終了日・事業場情報[DT_R13]用DAO
        /// </summary>
        private DT_R13DaoCls DT_R13Dao;

        /// <summary>
        /// 電子マニフェスト基本拡張テーブル[DT_R18_EX]登録更新削除用Dao
        /// </summary>
        private DT_R18_EXDaoCls DT_R18_EXDao;

        /// <summary>
        /// 電子マニフェスト収集運搬拡張[DT_R19_EX]登録更新削除用Dao
        /// </summary>
        private DT_R19_EXDaoCls DT_R19_EXDao;

        /// <summary>
        ///電子マニフェスト最終処分（予定）拡張[DT_R04_EX]登録更新削除用Dao
        /// </summary>
        private DT_R04_EXDaoCls DT_R04_EXDao;

        /// <summary>
        /// 電子マニフェスト最終処分拡張[DT_R13_EX]登録更新削除用Dao
        /// </summary>
        private DT_R13_EXDaoCls DT_R13_EXDao;

        /// <summary>
        /// 1次マニフェスト情報[DT_R08]登録更新削除用Dao
        /// </summary>
        private DT_R08DaoCls DT_R08Dao;

        /// <summary>
        /// 一次マニフェスト情報拡張登録更新削除用Dao
        /// </summary>
        private DT_R08_EXDaoCls DT_R08_EXDao;

        /// <summary>
        /// 電子廃棄物種類細分類マスタ検索用Dao
        /// </summary>
        internal IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao im_denshi_haiki_shurui_saibunruidao;

        /// <summary>
        /// 電子廃棄物種類細分類マスタ
        /// </summary>
        internal M_DENSHI_HAIKI_SHURUI_SAIBUNRUI[] ListDenshiHaikiShuruiSaibunrui;

        /// <summary>
        /// 電子廃棄物種類マスタ検索用Dao
        /// </summary>
        IM_DENSHI_HAIKI_SHURUIDao im_denshi_haiki_shuruidao;

        /// <summary>
        /// 電子廃棄物名称マスタ検索用Dao
        /// </summary>
        internal IM_DENSHI_HAIKI_NAMEDao im_denshi_haiki_namedao;

        /// <summary>
        /// 電子廃棄物種類マスタ
        /// </summary>
        internal M_DENSHI_HAIKI_SHURUI[] ListDenshiHaikiShurui;

        /// <summary>
        /// 紐付け情報登録更新削除用Dao
        /// </summary>
        internal T_MANIFEST_RELATIONDaoCls T_MANIFEST_RELATIONDao;

        /// <summary>
        /// マニフェスト明細の検索、更新用Dao
        /// </summary>
        private TMDDaoCls TMDDao;

        //電子マニフェストパターン有害物質Dao
        DT_PT_R02DaoCls dtPtDao02;
        //電子マニフェストパターン最終処分(予定)Dao
        DT_PT_R04DaoCls dtPt04Dao;
        //電子マニフェストパターン連絡番号Dao
        DT_PT_R05DaoCls dtPt05Dao;
        //電子マニフェストパターン備考Dao
        DT_PT_R06DaoCls dtPt06Dao;
        //電子マニフェストパターン最終処分Dao
        DT_PT_R13DaoCls dtPt13Dao;
        //電子マニフェストパターンDao
        DT_PT_R18DaoCls dtPt18Dao;
        //電子マニフェストパターン収集運搬Dao
        DT_PT_R19DaoCls dtPt19Dao;
        //電子マニフェストパターン拡張Dao
        DT_PT_R18EXDaoCls dtPt18EXDao;

        /// <summary>
        /// 電子マニフェスト存在チェック検索用Dao
        /// </summary>
        private DT_R18SearchDaoCls DT_R18SearchDao;

        /// <summary>
        /// 新規以外モードで、DBに既存データのEntity
        /// </summary>
        public DenshiManifestInfoCls ManiInfo = null;

        /// <summary>
        /// 電子マニフェスト伝票の比較用データのEntity
        /// </summary>
        public DenshiManifestInfoCls SEQManiInfo = null;

        public DenshiManifestPatternInfoCls ManiPtnInfo = null;
        /// <summary>
        /// 目次情報の最新SEQ
        /// </summary>
        public SqlInt16 LastSEQ = 0;

        public SqlInt16 APPROVAL_SEQ = 0;
        /// <summary>
        /// 電子マニフェスト入力画面フォーム
        /// </summary>
        private UIForm form;

        /// <summary>電子マニフェスト入力画面のHeader</summary>
        public UIHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        public ConstCls ConstCls { get; set; }

        /// <summary>
        /// マニFlag
        /// </summary>
        internal int maniFlag = 1;

        /// <summary>
        /// 中間処理産業廃棄物情報Dao
        /// </summary>
        internal FirstManifestInfoDaoCls FirstManifestInfoDao;

        /// <summary>
        /// 最終処分終了報告の取消情報Dao
        /// </summary>
        private LastSbnEndrepCancelInfoDaoCls LastSbnEndrepCancelInfoDao;

        /// <summary>
        /// 最終処分終了報告情報Dao
        /// </summary>
        private LastSbnEndrepInfoDaoCls LastSbnEndrepInfoDao;

        /// <summary>
        /// 空のデータを作るか判断します
        /// </summary>
        internal bool isMakeEmptyData_R19_EX = false;

        /// <summary>
        /// 中間処理産業廃棄物情報検索結果
        /// </summary>
        public DataTable FirstManifestInfo { get; set; }

        /// <summary>
        /// 最終処分終了報告の取消情報検索結果
        /// </summary>
        public DataTable LastSbnEndrepCancelInfo { get; set; }

        /// <summary>
        /// 最終処分終了報告情報検索結果
        /// </summary>
        public DataTable LastSbnEndrepInfo { get; set; }

        /// <summary>
        /// マニフェストID
        /// </summary>
        public String ManifestId { get; set; }

        /// <summary>
        /// システム情報
        /// </summary>
        public M_SYS_INFO mSysInfo { get; set; }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        internal bool isRegistErr = false;

        internal bool HoryuFlg = false;

        internal bool HoryuDelFlg = false;

        internal bool HoryuINSFlg = false;

        /// <summary>
        /// INXS manifest logic refs #158004
        /// </summary>
        internal InxsManifestLogic inxsManifestLogic;

        /// <summary>
        /// Is upload manifest to INXS refs #158004
        /// </summary>
        internal bool isUploadToInxs = false;

        #endregion

        #region プロパティ(DTO)


        /// <summary>
        /// 電子マニフェスト基本拡張で既存データ判断用検索条件DTO
        /// </summary>
        public SearchMasterDataDTOCls SearchExistDto { get; set; }

        /// <summary>
        /// 紐付した一次情報検索結果
        /// </summary>
        public DataTable RelationResult { get; set; }

        /// <summary>
        /// 紐付したい対象検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }
        /// <summary>
        /// 結果紐付したと紐付したい対象の合計
        /// </summary>
        public DataTable TotalResults { get; set; }

        /// <summary>
        /// 電子廃棄物名称CDマスタ情報
        /// </summary>
        public DataTable DenshiHaikiNameCodeResult { get; set; }
        /// <summary>
        /// 電子廃棄物種類マスタ情報
        /// </summary>
        public DataTable DenshiHaikiShuruiCodeResult { get; set; }
        /// <summary>
        /// マスタデータ検索条件DTO
        /// </summary>
        public SearchMasterDataDTOCls SearchExistDTO { get; set; }

        /// <summary>
        /// 運搬情報入力抑制フラグ
        /// </summary>
        public bool IsUnpanDisable { get; set; }

        /// <summary>
        /// 運搬情報入力有効行
        /// </summary>
        public int EnabledUnpanRow { get; set; }

        /// <summary>
        /// 紐付時登録実行フラグ
        /// </summary>
        public bool HimodukeUpdate { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.resources = new ComponentResourceManager(typeof(UIForm));

            this.DT_R18Dao = DaoInitUtility.GetComponent<DT_R18DaoCls>();
            this.QUE_INFODao = DaoInitUtility.GetComponent<QUE_INFODaoCls>();
            this.DT_MF_TOCDao = DaoInitUtility.GetComponent<DT_MF_TOCDaoCls>();
            this.DT_MF_MEMBERDao = DaoInitUtility.GetComponent<DT_MF_MEMBERDaoCls>();
            this.DT_R19Dao = DaoInitUtility.GetComponent<DT_R19DaoCls>();
            this.DT_R02Dao = DaoInitUtility.GetComponent<DT_R02DaoCls>();
            this.DT_R18SearchDao = DaoInitUtility.GetComponent<DT_R18SearchDaoCls>();

            this.DT_R04Dao = DaoInitUtility.GetComponent<DT_R04DaoCls>();
            this.DT_R05Dao = DaoInitUtility.GetComponent<DT_R05DaoCls>();
            this.DT_R06Dao = DaoInitUtility.GetComponent<DT_R06DaoCls>();
            this.DT_R13Dao = DaoInitUtility.GetComponent<DT_R13DaoCls>();

            this.DT_R18_EXDao = DaoInitUtility.GetComponent<DT_R18_EXDaoCls>();
            this.DT_R19_EXDao = DaoInitUtility.GetComponent<DT_R19_EXDaoCls>();
            this.DT_R04_EXDao = DaoInitUtility.GetComponent<DT_R04_EXDaoCls>();
            this.DT_R13_EXDao = DaoInitUtility.GetComponent<DT_R13_EXDaoCls>();

            this.DT_R08Dao = DaoInitUtility.GetComponent<DT_R08DaoCls>();
            this.DT_R08_EXDao = DaoInitUtility.GetComponent<DT_R08_EXDaoCls>();
            this.T_MANIFEST_RELATIONDao = DaoInitUtility.GetComponent<T_MANIFEST_RELATIONDaoCls>();
            this.im_denshi_haiki_shurui_saibunruidao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao>();
            this.im_denshi_haiki_shuruidao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
            this.im_denshi_haiki_namedao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_NAMEDao>();

            this.DT_R08_EXDao = DaoInitUtility.GetComponent<DT_R08_EXDaoCls>();
            this.DT_R08Dao = DaoInitUtility.GetComponent<DT_R08DaoCls>();
            this.T_MANIFEST_RELATIONDao = DaoInitUtility.GetComponent<T_MANIFEST_RELATIONDaoCls>();

            //電子マニフェストパターン有害物質Dao
            this.dtPtDao02 = DaoInitUtility.GetComponent<DT_PT_R02DaoCls>();
            //電子マニフェストパターン最終処分(予定)Dao
            this.dtPt04Dao = DaoInitUtility.GetComponent<DT_PT_R04DaoCls>();
            //電子マニフェストパターン連絡番号Dao
            this.dtPt05Dao = DaoInitUtility.GetComponent<DT_PT_R05DaoCls>();
            //電子マニフェストパターン備考Dao
            this.dtPt06Dao = DaoInitUtility.GetComponent<DT_PT_R06DaoCls>();
            //電子マニフェストパターン最終処分Dao
            this.dtPt13Dao = DaoInitUtility.GetComponent<DT_PT_R13DaoCls>();
            //電子マニフェストパターンDao
            this.dtPt18Dao = DaoInitUtility.GetComponent<DT_PT_R18DaoCls>();
            //電子マニフェストパターン収集運搬Dao
            this.dtPt19Dao = DaoInitUtility.GetComponent<DT_PT_R19DaoCls>();
            //電子マニフェストパターン拡張Dao
            this.dtPt18EXDao = DaoInitUtility.GetComponent<DT_PT_R18EXDaoCls>();

            this.msgLogic = new MessageBoxShowLogic();

            // 中間処理産業廃棄物情報
            this.FirstManifestInfoDao = DaoInitUtility.GetComponent<FirstManifestInfoDaoCls>();

            // 最終処分終了報告の取消情報
            this.LastSbnEndrepCancelInfoDao = DaoInitUtility.GetComponent<LastSbnEndrepCancelInfoDaoCls>();

            // 最終処分終了報告情報を取得する
            this.LastSbnEndrepInfoDao = DaoInitUtility.GetComponent<LastSbnEndrepInfoDaoCls>();

            // マニフェスト明細検索・更新用Dao
            this.TMDDao = DaoInitUtility.GetComponent<TMDDaoCls>();

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            //システム情報
            mSysInfo = new DBAccessor().GetSysInfo();

            //運搬入力抑制フラグ
            this.IsUnpanDisable = false;
            this.EnabledUnpanRow = -1;

            this.inxsManifestLogic = new InxsManifestLogic(EnumManifestType.DENSHI);

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            if (this.tuuchiRirekiFlg)
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPathForTuuchiRireki);
            else
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20140606 katen 不具合No.4691 start‏
                if (this.form.fromManiFirstFlag != null)
                {
                    //他の画面に一次二次区分をもらった場合
                    this.maniFlag = this.form.fromManiFirstFlag.Value;
                }
                // 20140606 katen 不具合No.4691 end‏

                // フォームインスタンスを取得
                this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                this.headerform = (UIHeader)parentbaseform.headerForm;

                // ボタンを初期化
                this.ButtonInit();

                //修正モード時以外の場合
                if (!WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.Mode))
                {
                    this.parentbaseform.bt_func8.Text = string.Empty;
                    this.parentbaseform.bt_func8.Tag = string.Empty;
                    this.parentbaseform.bt_func8.Enabled = false;
                }

                //初期値を設定する
                this.form.cantxt_HakkouCnt.Text = "1";

                if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.Mode))
                {
                    //追加モード時のみ編集可
                    this.form.cantxt_HakkouCnt.Enabled = true;
                }
                else
                {
                    this.form.cantxt_HakkouCnt.Enabled = false;
                }

                //電子廃棄物種類細分類マスタ
                this.GetDenshiHaikiShuruiSaibunrui();


                //footボタン処理イベントを初期化
                EventInit();

                maniRelation = null; //紐付初期化

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //新規ボタン(F2)イベント生成
            parentform.bt_func2.Click += new EventHandler(this.form.ClearFormByNewInfoMode);
            //修正ボタン(F3)イベント生成
            this.form.C_Regist(parentform.bt_func3);
            parentform.bt_func3.Click += new EventHandler(this.form.ReloadModifiedManiInfo);
            //二次マニボタン(F4)イベント生成
            parentform.bt_func4.Click += new EventHandler(this.form.SetManifestForm);
            //部分更新ボタン(F5)イベント生成
            parentform.bt_func5.Click += new EventHandler(this.form.partialUpdate);
            //明細削除ボタン(F6)イベント生成
            parentform.bt_func6.Click += new EventHandler(this.form.DeleteRow);
            //一覧ボタン(F7)イベント生成
            this.form.C_Regist(parentform.bt_func7);
            parentform.bt_func7.Click += new EventHandler(this.form.ToIchiRan);
            //受渡確認ボタン(F8)イベント生成
            this.form.C_Regist(parentform.bt_func8);
            parentform.bt_func8.Click += new EventHandler(this.form.UkewatashiKakuninHyouPrint);
            //JWNET送信ボタン(F9)イベント生成
            this.form.C_Regist(parentform.bt_func9);
            parentform.bt_func9.Click += new EventHandler(this.form.RegistManiToJWNET);
            //保留保存ボタン(F10)イベント生成
            this.form.C_Regist(parentform.bt_func10);
            parentform.bt_func10.Click += new EventHandler(this.form.RegistManiHouryou);
            //【1】パターン登録ボタンイベント生成
            this.form.C_Regist(parentform.bt_process1);
            parentform.bt_process1.Click += new EventHandler(this.SavePatternData);
            //【2】パターン呼出ボタンイベント生成
            this.form.C_Regist(parentform.bt_process2);
            parentform.bt_process2.Click += new EventHandler(this.LoadPatternData);
            //【3】1次マニ紐付ボタンイベント生成
            this.form.C_Regist(parentform.bt_process3);
            parentform.bt_process3.Click += new EventHandler(this.form.ToHimodukeForm);

            parentform.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.FormClose);

            //ESCテキストイベント生成
            parentform.txb_process.KeyDown += new KeyEventHandler(this.form.txb_process_Enter);

            //最終処分終了報告イベント生成
            parentform.bt_process4.Click += new EventHandler(this.form.LastSbnEndrep);

            //最終処分終了報告の取消イベント生成
            parentform.bt_process5.Click += new EventHandler(this.form.LastSbnEndrepCancel);

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.Mode))
            {
                this.form.cantxt_HakkouCnt.Validating += new CancelEventHandler(this.form.HakkouCntValidating);
                this.form.cantxt_HakkouCnt.KeyPress += new KeyPressEventHandler(this.form.HakkouCnt_KeyPress);
            }

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.Mode) || WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.Mode))
            {
                this.form.cdgv_Tyukanshori.CellValidating += new DataGridViewCellValidatingEventHandler(this.form.TyukanshoriCellValidating);
                //this.form.cantxt_HakkouCnt.KeyPress += new KeyPressEventHandler(this.form.HakkouCnt_KeyPress);

            }

            this.form.ctxt_Haisyutu_KanyushaNo.TextChanged += new EventHandler(this.form.ctxt_Haisyutu_KanyushaNo_TextChanged);
            this.form.cdgv_Haikibutu.CellValidated += new DataGridViewCellEventHandler(this.form.cdgv_Haikibutu_CellValidated);

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
            // ファンクション4(2次マニ)機能の非表示

            // ボタン初期化
            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;
            parentform.bt_func4.Text = string.Empty;
            parentform.bt_func4.Enabled = false;

            // イベント削除
            //二次マニボタン(F4)イベント
            parentform.bt_func4.Click -= new EventHandler(this.form.SetManifestForm);
        }

        /// <summary>
        /// 更新後や新規等では、初期化(null代入)すること
        /// </summary>
        internal Shougun.Core.PaperManifest.ManifestHimoduke.ManiRelrationResult maniRelation = null;

        /// <summary>
        /// マニフェスト紐付画面へ
        /// </summary>
        public bool ToHimodukeForm()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // ２次マニ情報から紐付情報の有無と最終処分終了日の比較を行う。
                    // 電子１次に紐付されていて、かつ電子１次マニの最終処分終了日が設定済の場合、かつ２次マニの最終処分終了日が設定済の場合に確認メッセージを表示する。
                    if (this.CheckLastSbnDate())
                    {
                        if (this.msgLogic.MessageBoxShow("C046", "1次の最終処分終了日と2次の最終処分終了日に差異があります。\n登録") != DialogResult.Yes)
                        {
                            return true;
                        }
                    }
                }

                if (this.msgLogic.MessageBoxShow("C065") != DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    // 手動の場合、紐付画面表示前に登録処理実行
                    // 下のコメントは自動入力の場合には有効
                    if (!ManiInfo.bIsAutoMode)
                    {
                        if (!this.HimodukeRegistDenshiManifest(true))
                        {
                            return true;
                        }
                    }

                    // 紙マニとは違い、紐付け画面起動時にデータの登録はしない。(必須チェックも不要)
                    // JWNETと通信するため、安易にデータの修正はできないため。

                    //マニフェスト紐付画面を呼び出し
                    Shougun.Core.PaperManifest.ManifestHimoduke.UIForm.DoRelation(
                        this.form.WindowType,
                        "4", //電子は4
                        this.ManiInfo.dt_r18ExOld == null ? "" : this.ManiInfo.dt_r18ExOld.SYSTEM_ID.ToString(),
                        this.ManiInfo.dt_r18ExOld == null ? "" : this.ManiInfo.dt_r18ExOld.SYSTEM_ID.ToString(),
                        this.ManiInfo.dt_r18ExOld == null ? "" : (this.ManiInfo.dt_r18ExOld.KANSAN_SUU.IsNull ? "" : this.ManiInfo.dt_r18ExOld.KANSAN_SUU.ToString()),
                        ManiInfo.dt_r18.KANRI_ID,
                        ref this.maniRelation);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToHimodukeForm", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// ２次マニ情報から紐付情報の有無と最終処分終了日の比較を行う。
        /// </summary>
        public bool CheckLastSbnDate()
        {
            bool ret = false;
            ManifestoLogic mlogic = new ManifestoLogic();

            // 電子１次マニに紐付されているかチェックする。
            DataTable firstManiData = mlogic.SelectFirstManiSystemID(this.ManiInfo.dt_r18ExOld.SYSTEM_ID.ToString(), ConstCls.DENSHI_MEDIA_TYPE);
            if (firstManiData.Rows.Count != 0)
            {
                for (int i = 0; i < firstManiData.Rows.Count; i++)
                {
                    // 最終処分場（実績）の明細分チェックする。
                    for (int j = 0; j < this.form.cdgv_LastSBN_Genba_Jiseki.Rows.Count; j++)
                    {
                        // 電子１次マニの最終処分終了日が設定されている、かつ２次マニの最終処分場（実績）の最終処分終了日が設定済の場合
                        // メッセージを表示するためフラグをtrueに設定する。
                        if (!string.IsNullOrEmpty(firstManiData.Rows[i]["LAST_SBN_END_DATE"].ToString())
                            && this.form.cdgv_LastSBN_Genba_Jiseki.Rows[j].Cells["LAST_SBN_END_DATE"].Value != null)
                        {
                            if (!firstManiData.Rows[i]["LAST_SBN_END_DATE"].ToString()
                                .Equals(Convert.ToDateTime(this.form.cdgv_LastSBN_Genba_Jiseki.Rows[j].Cells["LAST_SBN_END_DATE"].Value).ToString("yyyyMMdd")))
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
        /// マニフェスト一覧画面へ
        /// </summary>
        public void ToIchiRan()
        {
            // 20140601 katen 不具合No.4129 start‏
            FormManager.OpenFormWithAuth("G126", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, "4", this.maniFlag);
            // 20140601 katen 不具合No.4129 end‏
        }

        #endregion

        #region データ登録(インサート)
        /// <summary>
        /// 電子マニ情報を登録処理
        /// </summary>
        ///<param name="bIsAutoMode"></param> 
        /// <param name="bHouryuFlg"></param>
        /// <param name="dt_r18"></param>
        /// <param name="que_info"></param>
        /// <param name="dt_mf_toc"></param>
        /// <param name="dt_mf_member"></param>
        /// <param name="lstDT_R19"></param>
        /// <param name="lstDT_R02"></param>
        /// <param name="lstDT_R04"></param>
        /// <param name="lstDT_R05"></param>
        /// <param name="lstDT_R06"></param>
        /// <param name="lstDT_R13"></param>
        ///<param name="lstDT_R08"></param> 
        /// <param name="dt_r18Ex"></param>
        /// <param name="lstDT_R19_EX"></param>
        /// <param name="lstDT_R04_EX"></param>
        /// <param name="lstDT_R13_EX"></param>
        /// <param name="lstDT_R08_EX"></param>
        /// <param name="lstT_MANIFEST_RELATION"></param>
        [Transaction]
        public void Insert(bool bIsAutoMode,                   //自動フラグ
                            bool bHouryuFlg,                    //保留登録フラグ
                            DT_R18 dt_r18,                      //電子マニ情報
                            QUE_INFO que_info,                  //キュー情報
                            DT_MF_TOC dt_mf_toc,                //目次情報
                            DT_MF_MEMBER dt_mf_member,          //加入者番号情報
                            List<DT_R19> lstDT_R19,             //運搬情報
                            List<DT_R02> lstDT_R02,             //有害物質情報
                            List<DT_R04> lstDT_R04,             //最終処分事業場（予定）情報
                            List<DT_R05> lstDT_R05,             //連絡番号情報
                            List<DT_R06> lstDT_R06,             //備考情報
                            List<DT_R13> lstDT_R13,             //最終処分終了日・事業場情報
                            List<DT_R08> lstDT_R08,             //一次マニフェスト情報
                            DT_R18_EX dt_r18Ex,              //電子基本拡張
                            List<DT_R19_EX> lstDT_R19_EX,       //電子運搬拡張
                            List<DT_R04_EX> lstDT_R04_EX,       //電子最終処分(予定)拡張
                            List<DT_R13_EX> lstDT_R13_EX,        //電子最終処分(実績)拡張
                            List<DT_R08_EX> lstDT_R08_EX,        //一次マニフェスト情報拡張
                            List<T_MANIFEST_RELATION> lstT_MANIFEST_RELATION //紐付け情報
                           )
        {
            LogUtility.DebugMethodStart(bIsAutoMode, bHouryuFlg, dt_r18, que_info, dt_mf_toc, dt_mf_member, lstDT_R19, lstDT_R02, lstDT_R04, lstDT_R05, lstDT_R06, lstDT_R13, lstDT_R08, dt_r18Ex, lstDT_R19_EX, lstDT_R04_EX, lstDT_R13_EX, lstDT_R08_EX, lstT_MANIFEST_RELATION);

            this.isRegistErr = false;

            //データ更新
            if (dt_r18 == null) return;

            string NewKanriNo = "";
            string firstKanriNo = string.Empty;
            bool haikibutuFlg = false;//廃棄物データフラグ（true:データが存在しない）

            this.form.cdgv_Haikibutu.AllowUserToAddRows = false;

            for (int i = 0; i < this.form.cdgv_Haikibutu.Rows.Count; i++)
            {   //行の有効フラグ
                bool bIsValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_Haikibutu.Rows[i], false);
                if (!bIsValidRow)
                {
                    this.form.cdgv_Haikibutu.Rows.RemoveAt(i);
                }
            }

            //電子マニフェスト基本拡張(DT_R18_EX)を作成する
            if (!this.CreatDT_R18_EXData())
            {
                //異常の場合
                return;
            }

            try
            {
                //データ更新
                using (Transaction tran = new Transaction())
                {
                    int haikibutuCnt = 1;
                    if (this.form.cdgv_Haikibutu.RowCount != 0)
                    {
                        haikibutuCnt = this.form.cdgv_Haikibutu.RowCount;
                    }
                    else
                    {
                        //廃棄物データグリッドにデータが存在しない
                        haikibutuFlg = true;
                    }

                    int hakkouKensu = 1;
                    //廃棄物複数件数は優先
                    if (!string.IsNullOrEmpty(this.form.cantxt_HakkouCnt.Text))
                        hakkouKensu = (haikibutuCnt != 1) ? 1 : Convert.ToInt32(this.form.cantxt_HakkouCnt.Text);

                    //発行件数
                    for (int cnt = 0; cnt < hakkouKensu; cnt++)
                    {
                        //廃棄物
                        for (int rowCnt = 0; rowCnt < haikibutuCnt; rowCnt++)
                        {
                            //管理番号の採番　
                            DT_R18Dao.GetByJob(out NewKanriNo);

                            if (string.IsNullOrEmpty(firstKanriNo)) firstKanriNo = NewKanriNo;

                            //電子マニフェスト
                            dt_r18.KANRI_ID = NewKanriNo;
                            dt_r18.SEQ = 1;
                            //登録情報承認待ちフラグ
                            dt_r18.SHOUNIN_FLAG = 1;//待ちなし

                            //先ずリスト情報が画面から取得する
                            lstDT_R02.Clear();

                            if (!haikibutuFlg)
                            {
                                //有害物質リストDT_R02
                                for (int i = 0; i < 6; i++)
                                {
                                    string ColName_CD = "YUUGAI_CODE" + (i + 1).ToString();
                                    string ColName_Name = "YUUGAI_NAME" + (i + 1).ToString();
                                    object tmpObj_CD = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells[ColName_CD].Value;
                                    object tmpObj_NAME = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells[ColName_Name].Value;

                                    if (tmpObj_CD != null)
                                    {
                                        if (!string.IsNullOrEmpty(tmpObj_CD.ToString()))
                                        {
                                            //有害物質Entityの宣言
                                            DT_R02 dt_r02 = new DT_R02();
                                            dt_r02.REC_SEQ = SqlInt16.Parse((lstDT_R02.Count + 1).ToString());
                                            dt_r02.YUUGAI_CODE = tmpObj_CD.ToString();
                                            if (tmpObj_NAME != null)
                                            {
                                                dt_r02.YUUGAI_NAME = tmpObj_NAME.ToString();
                                            }
                                            lstDT_R02.Add(dt_r02);
                                        }
                                    }

                                }

                                //マニフェスト情報DT_R18
                                //廃棄物種類から大中小分類を設定する
                                object tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_SHURUI_CD"].Value;
                                if (tmpObjHaiki != null)
                                {
                                    if (!string.IsNullOrEmpty(tmpObjHaiki.ToString()))
                                    {
                                        string haikisyuruyiCD = tmpObjHaiki.ToString();
                                        //大分類コード
                                        dt_r18.HAIKI_DAI_CODE = haikisyuruyiCD.Substring(0, 2);
                                        //中分類コード
                                        dt_r18.HAIKI_CHU_CODE = haikisyuruyiCD.Substring(2, 1);
                                        //小分類コード
                                        dt_r18.HAIKI_SHO_CODE = haikisyuruyiCD.Substring(3, 1);
                                        //細分類コード
                                        dt_r18.HAIKI_SAI_CODE = haikisyuruyiCD.Substring(4, 3);
                                        //廃棄物種類名
                                        tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_SHURUI_NAME"].Value;
                                        if (tmpObjHaiki != null)
                                        {
                                            dt_r18.HAIKI_SHURUI = tmpObjHaiki.ToString();
                                        }
                                        // 廃棄物の大分類名称
                                        dt_r18.HAIKI_BUNRUI = this.GetHaikiBunruiName(dt_r18.HAIKI_DAI_CODE);
                                    }
                                }
                                //廃棄物名称
                                tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_NAME"].Value;
                                if (tmpObjHaiki != null)
                                {
                                    //廃棄物名称
                                    dt_r18.HAIKI_NAME = tmpObjHaiki.ToString();
                                }
                                //廃棄物の数量
                                tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_SUU"].Value;
                                if (tmpObjHaiki != null)
                                {
                                    //廃棄物の数量
                                    dt_r18.HAIKI_SUU = SqlDecimal.Parse(tmpObjHaiki.ToString().Replace(",", ""));
                                }
                                //廃棄物の数量単位コード
                                tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["UNIT_CD"].Value;
                                if (tmpObjHaiki != null)
                                {
                                    //廃棄物の数量単位コード
                                    int len = (tmpObjHaiki as string).Length;
                                    dt_r18.HAIKI_UNIT_CODE = (tmpObjHaiki as string).Substring(len - 1, 1);
                                }
                                //数量確定者コード
                                tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["SUU_KAKUTEI_CODE"].Value;
                                if (tmpObjHaiki != null)
                                {
                                    //数量確定者コード
                                    dt_r18.SUU_KAKUTEI_CODE = tmpObjHaiki.ToString();
                                }

                                //荷姿コード
                                object tmpObjNisu = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["NISUGATA_CD"].Value;
                                if (tmpObjNisu != null)
                                {
                                    dt_r18.NISUGATA_CODE = tmpObjNisu.ToString();
                                }
                                //荷姿名
                                tmpObjNisu = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["NISUGATA_NAME"].Value;
                                if (tmpObjNisu != null)
                                {
                                    dt_r18.NISUGATA_NAME = tmpObjNisu.ToString();
                                }
                                //荷姿の数量
                                tmpObjNisu = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["NISUGATA_SUU"].Value;
                                if (tmpObjNisu != null)
                                {
                                    dt_r18.NISUGATA_SUU = tmpObjNisu.ToString().Replace(",", "");
                                }

                                //有害物質情報件数	
                                dt_r18.YUUGAI_CNT = SqlInt16.Parse(lstDT_R02.Count.ToString());

                            }

                            //電子マニ情報登録
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //dt_r18.CREATE_DATE = DateTime.Now;
                            dt_r18.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            dt_r18.CANCEL_FLAG = 0;
                            DT_R18Dao.Insert(dt_r18);
                            //自動の場合はキュー情報登録する
                            if (bIsAutoMode)
                            {
                                //キュー情報
                                que_info = new QUE_INFO();
                                que_info.KANRI_ID = dt_r18.KANRI_ID;
                                que_info.SEQ = dt_r18.SEQ;
                                que_info.QUE_SEQ = 1;
                                //機能番号
                                if (dt_r18.MANIFEST_KBN == 1)//予約
                                {
                                    que_info.FUNCTION_ID = maniFlag.Equals(1) ? "0101" : "0102";//機能番号[一次場合は0101、二次場合"0102"]
                                }
                                else if (dt_r18.MANIFEST_KBN == 2)//マニフェスト登録
                                {
                                    que_info.FUNCTION_ID = maniFlag.Equals(1) ? "0501" : "0502";//機能番号[一次場合は0501、二次場合0502]
                                }
                                //キュー状態フラグ
                                if (bHouryuFlg)//保留登録
                                {
                                    que_info.STATUS_FLAG = 7;//送信保留
                                }
                                else
                                {
                                    que_info.STATUS_FLAG = 0;//送信待ち
                                }
                                //キュー情報を登録
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //que_info.CREATE_DATE = DateTime.Now;
                                que_info.CREATE_DATE = this.getDBDateTime();
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                QUE_INFODao.Insert(que_info);
                            }

                            //目次情報
                            DT_MF_TOC Dt_Mf_TocDto = new DT_MF_TOC();
                            Dt_Mf_TocDto.KANRI_ID = dt_r18.KANRI_ID;
                            DT_MF_TOC Dt_Mf_Toc = DT_MF_TOCDao.GetDataForEntity(Dt_Mf_TocDto);
                            if (Dt_Mf_Toc == null)
                            {
                                Dt_Mf_Toc = new DT_MF_TOC();
                            }
                            Dt_Mf_Toc.LATEST_SEQ = 1;
                            Dt_Mf_Toc.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //自動の場合
                            if (bIsAutoMode)
                            {
                                //既読フラグ
                                Dt_Mf_Toc.READ_FLAG = 2;
                                //自動の場合
                                if (dt_r18.MANIFEST_KBN == 1)//予約場合
                                {
                                    //状態フラグ
                                    Dt_Mf_Toc.STATUS_FLAG = 1;//1:予約未
                                }
                                else if (dt_r18.MANIFEST_KBN == 2)//マニフェスト場合
                                {
                                    //状態フラグ
                                    Dt_Mf_Toc.STATUS_FLAG = 2;//2:登録未
                                }
                                if (bHouryuFlg)//送信保留の場合
                                {
                                    //状態詳細フラグ
                                    Dt_Mf_Toc.STATUS_DETAIL = 0;//通常
                                }
                                else//マニフェスト登録の場合
                                {
                                    //状態詳細フラグ
                                    Dt_Mf_Toc.STATUS_DETAIL = 1;//通信中
                                }
                                Dt_Mf_Toc.KIND = 4;//電子マニフェスト
                            }
                            //手動の場合
                            else
                            {
                                Dt_Mf_Toc.STATUS_FLAG = 4;      //4:登録
                                Dt_Mf_Toc.STATUS_DETAIL = 0;    //0:通常
                                Dt_Mf_Toc.KIND = 5;             //紙マニフェスト
                            }
                            //目次情報を登録

                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //Dt_Mf_Toc.CREATE_DATE = DateTime.Now;
                            Dt_Mf_Toc.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            DT_MF_TOCDao.Update(Dt_Mf_Toc);

                            //加入者番号情報
                            if (dt_mf_member != null)
                            {
                                dt_mf_member.KANRI_ID = dt_r18.KANRI_ID;

                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //dt_mf_member.CREATE_DATE = DateTime.Now;
                                dt_mf_member.CREATE_DATE = this.getDBDateTime();
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                DT_MF_MEMBERDao.Insert(dt_mf_member);
                            }
                            //電子収集運搬情報
                            if (lstDT_R19 != null && lstDT_R19.Count() > 0)
                            {
                                foreach (DT_R19 r19 in lstDT_R19)
                                {
                                    r19.KANRI_ID = dt_r18.KANRI_ID;
                                    r19.SEQ = dt_r18.SEQ;
                                    r19.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r19.CREATE_DATE = DateTime.Now;
                                    r19.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    DT_R19Dao.Insert(r19);
                                }
                            }
                            //電子有害物質情報
                            if (lstDT_R02 != null && lstDT_R02.Count() > 0)
                            {
                                foreach (DT_R02 r02 in lstDT_R02)
                                {
                                    r02.KANRI_ID = dt_r18.KANRI_ID;
                                    r02.SEQ = dt_r18.SEQ;
                                    r02.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r02.CREATE_DATE = DateTime.Now;
                                    r02.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    DT_R02Dao.Insert(r02);
                                }
                            }
                            //最終処分事業場（予定）情報
                            if (lstDT_R04 != null && lstDT_R04.Count() > 0)
                            {
                                foreach (DT_R04 r04 in lstDT_R04)
                                {
                                    r04.KANRI_ID = dt_r18.KANRI_ID;
                                    r04.SEQ = dt_r18.SEQ;
                                    r04.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r04.CREATE_DATE = DateTime.Now;
                                    r04.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    DT_R04Dao.Insert(r04);
                                }
                            }
                            //連絡番号情報
                            if (lstDT_R05 != null && lstDT_R05.Count() > 0)
                            {
                                foreach (DT_R05 r05 in lstDT_R05)
                                {
                                    r05.KANRI_ID = dt_r18.KANRI_ID;
                                    r05.SEQ = dt_r18.SEQ;
                                    r05.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r05.CREATE_DATE = DateTime.Now;
                                    r05.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    DT_R05Dao.Insert(r05);
                                }
                            }
                            //備考情報
                            if (lstDT_R06 != null && lstDT_R06.Count() > 0)
                            {
                                foreach (DT_R06 r06 in lstDT_R06)
                                {
                                    r06.KANRI_ID = dt_r18.KANRI_ID;
                                    r06.SEQ = dt_r18.SEQ;
                                    r06.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r06.CREATE_DATE = DateTime.Now;
                                    r06.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    DT_R06Dao.Insert(r06);
                                }
                            }

                            //最終処分終了日・事業場情報
                            if (lstDT_R13 != null && lstDT_R13.Count() > 0)
                            {
                                foreach (DT_R13 r13 in lstDT_R13)
                                {
                                    r13.KANRI_ID = dt_r18.KANRI_ID;
                                    r13.SEQ = dt_r18.SEQ;
                                    r13.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r13.CREATE_DATE = DateTime.Now;
                                    r13.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    DT_R13Dao.Insert(r13);
                                }
                            }

                            //電子マニフェスト基本拡張[DT_R18_EX]
                            //最新データを生成
                            dt_r18Ex = CreateDT_R18ExEntity(false, dt_r18, null, rowCnt, haikibutuFlg);
                            var dataBinderEntry18 = new DataBinderLogic<DT_R18_EX>(dt_r18Ex);
                            dataBinderEntry18.SetSystemProperty(dt_r18Ex, false);
                            //データ追加
                            DT_R18_EXDao.Insert(dt_r18Ex);

                            //電子マニフェスト収集運搬拡張[DT_R19_EX]
                            //最新データを生成
                            List<DT_R19_EX> lstNewDT_R19Ex = CreateDT_R19ExEntityList(dt_r18Ex);//画面から情報を取得
                            foreach (DT_R19_EX dt_r19Ex in lstNewDT_R19Ex)
                            {
                                dt_r19Ex.DELETE_FLG = false;
                                var dataBinderEntry19 = new DataBinderLogic<DT_R19_EX>(dt_r19Ex);
                                dataBinderEntry19.SetSystemProperty(dt_r19Ex, false);
                                //データ追加
                                DT_R19_EXDao.Insert(dt_r19Ex);
                            }

                            //最終処分（予定）拡張[DT_R04_EX]
                            //最新データ作成
                            List<DT_R04_EX> lstNewDT_R04Ex = CreateDT_R04ExEntityList(dt_r18Ex);//画面から情報を取得
                            foreach (DT_R04_EX dt_r04Ex in lstNewDT_R04Ex)
                            {
                                dt_r04Ex.DELETE_FLG = false;
                                var dataBinderEntry04 = new DataBinderLogic<DT_R04_EX>(dt_r04Ex);
                                dataBinderEntry04.SetSystemProperty(dt_r04Ex, false);
                                //データ追加
                                DT_R04_EXDao.Insert(dt_r04Ex);
                            }

                            //最終処分拡張[DT_R13_EX]最新データ作成
                            List<DT_R13_EX> lstNewDT_R13Ex = CreateDT_R13ExEntityList(dt_r18Ex);//画面から情報を取得
                            foreach (DT_R13_EX dt_r13Ex in lstNewDT_R13Ex)
                            {
                                dt_r13Ex.DELETE_FLG = false;
                                var dataBinderEntry13 = new DataBinderLogic<DT_R13_EX>(dt_r13Ex);
                                dataBinderEntry13.SetSystemProperty(dt_r13Ex, false);
                                DT_R13_EXDao.Insert(dt_r13Ex);
                            }

                            //一次マニフェスト情報拡張[DT_R08_EX]最新データ作成
                            List<DT_R08_EX> lstNewDT_R08Ex = CreateDT_R08ExEntityList(dt_r18Ex);//画面から情報を取得
                            foreach (DT_R08_EX dt_r08Ex in lstNewDT_R08Ex)
                            {
                                dt_r08Ex.DELETE_FLG = false;
                                var dataBinderEntry08 = new DataBinderLogic<DT_R08_EX>(dt_r08Ex);
                                dataBinderEntry08.SetSystemProperty(dt_r08Ex, true);
                                //データ追加
                                DT_R08_EXDao.Insert(dt_r08Ex);
                            }
                            //一次マニフェスト情報[DT_R08]データ作成
                            if (lstDT_R08 != null)
                            {
                                //自動と手動同じ処理です
                                foreach (DT_R08 r08 in lstDT_R08)
                                {
                                    r08.KANRI_ID = dt_r18.KANRI_ID;
                                    r08.SEQ = dt_r18.SEQ;
                                    //作成日付
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                    //r08.CREATE_DATE = DateTime.Now;
                                    r08.CREATE_DATE = this.getDBDateTime();
                                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                    //一次マニフェスト情報を追加
                                    DT_R08Dao.Insert(r08);
                                }
                            }
                        }
                    }


                    tran.Commit();

                    this.form.cdgv_Haikibutu.AllowUserToAddRows = true;

                    this.msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Insert", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                this.isRegistErr = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Insert", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                this.isRegistErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Insert", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                this.isRegistErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(bIsAutoMode, bHouryuFlg, dt_r18, que_info, dt_mf_toc, dt_mf_member, lstDT_R19, lstDT_R02, lstDT_R04, lstDT_R05, lstDT_R06, lstDT_R13, lstDT_R08, dt_r18Ex, lstDT_R19_EX, lstDT_R04_EX, lstDT_R13_EX, lstDT_R08_EX, lstT_MANIFEST_RELATION);
            }
        }

        /// <summary>
        /// 新規画面を表示
        /// </summary>
        internal void ChangeNewWindowMode()
        {
            int beforeManiFlag = this.maniFlag;
            this.form.isOpenG142 = false;
            if (!this.form.InitializeFormByMode(WINDOW_TYPE.NEW_WINDOW_FLAG)) { return; }
            if (beforeManiFlag == 2)
            {
                this.maniFlag = beforeManiFlag;
                this.SetManifestForm("Non");
            }
        }

        /// <summary>
        /// 登録時に中間処理産業廃棄物が電マニで電子マニフェスト基本拡張(DT_R18_EX)が登録されていない場合は、
        /// マニフェスト紐付データの作成のために、システムIDを採番した電子マニフェスト基本拡張(DT_R18_EX)を作成する。
        /// </summary>
        /// <param name="dt_r18"></param>
        /// <param name="lstDT_R08"></param>
        [Transaction]
        private bool CreatDT_R18_EXData()
        {

            try
            {
                //画面から情報を取得
                List<DT_R18_EX> lstNew_DT_R18_EX = this.CreateNew_DT_R18_EXEntityList();

                //データ更新
                using (Transaction tran = new Transaction())
                {
                    foreach (DT_R18_EX dt_r18_ex in lstNew_DT_R18_EX)
                    {
                        var dataBinderEntry18 = new DataBinderLogic<DT_R18_EX>(dt_r18_ex);
                        dataBinderEntry18.SetSystemProperty(dt_r18_ex, false);

                        //データ追加
                        DT_R18_EXDao.Insert(dt_r18_ex);

                    }

                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }

            return true;
        }

        #endregion データ登録(インサート)

        #region データ登録（修正）
        /// <summary>
        /// 電子マニ修正
        /// </summary>
        /// <param name="bIsAutoMode"></param>
        /// <param name="bHouryuFlg"></param>
        /// <param name="dt_r18"></param>
        /// <param name="que_Info"></param>
        /// <param name="dt_mf_toc"></param>
        /// <param name="dt_mf_member"></param>
        /// <param name="lstDT_R19"></param>
        /// <param name="lstDT_R02"></param>
        /// <param name="lstDT_R04"></param>
        /// <param name="lstDT_R05"></param>
        /// <param name="lstDT_R06"></param>
        /// <param name="lstDT_R13"></param>
        /// <param name="lstDT_R08"></param>
        /// <param name="dt_r18ExOld"></param>
        /// <param name="lstDT_R19_EX"></param>
        /// <param name="lstDT_R04_EX"></param>
        /// <param name="lstDT_R13_EX"></param>
        /// <param name="lstDT_R08_EX"></param>
        /// <param name="lstT_MANIFEST_RELATION"></param>
        [Transaction]
        public void Update(bool bIsAutoMode,                   //自動フラグ
                            bool bHouryuFlg,                    //保留登録フラグ
                            DT_R18 dt_r18,                      //電子マニ情報
                            QUE_INFO que_Info,                  //キュー情報
                            DT_MF_TOC dt_mf_toc,                //目次情報
                            DT_MF_MEMBER dt_mf_member,          //加入者番号情報
                            List<DT_R19> lstDT_R19,             //運搬情報
                            List<DT_R02> lstDT_R02,             //有害物質情報
                            List<DT_R04> lstDT_R04,             //最終処分事業場（予定）情報
                            List<DT_R05> lstDT_R05,             //連絡番号情報
                            List<DT_R06> lstDT_R06,             //備考情報
                            List<DT_R13> lstDT_R13,             //最終処分終了日・事業場情報
                            List<DT_R08> lstDT_R08,             //一次マニフェスト情報
                            DT_R18_EX dt_r18ExOld,              //電子基本拡張[既存データ]
                            List<DT_R19_EX> lstDT_R19_EX,       //電子運搬拡張
                            List<DT_R04_EX> lstDT_R04_EX,       //電子最終処分(予定)拡張
                            List<DT_R13_EX> lstDT_R13_EX,       //電子最終処分拡張
                            List<DT_R08_EX> lstDT_R08_EX,        //一次マニフェスト情報拡張
                            List<T_MANIFEST_RELATION> lstT_MANIFEST_RELATION //紐付け情報
                           )
        {
            LogUtility.DebugMethodStart(bIsAutoMode, bHouryuFlg, dt_r18, que_Info, dt_mf_toc, dt_mf_member, lstDT_R19, lstDT_R02, lstDT_R04, lstDT_R05, lstDT_R06, lstDT_R13, lstDT_R08, dt_r18ExOld, lstDT_R19_EX, lstDT_R04_EX, lstDT_R13_EX, lstDT_R08_EX, lstT_MANIFEST_RELATION);

            this.isRegistErr = false;
            List<SuperEntity> createInfoList = new List<SuperEntity>();
            int count = 0;

            if (dt_r18 == null) return;  //電子マニ情報設定しない場合、戻る
            //管理IDの設定
            dt_r18.KANRI_ID = this.KanriId;
            //同管理IDで枝番号の最大値 + 1に設定する
            SqlInt16 MaxSeq = 0;
            SearchMasterDataDTOCls MaxSeqDto = new SearchMasterDataDTOCls();
            MaxSeqDto.KANRI_ID = dt_r18.KANRI_ID;
            DataTable dtSeq = DT_R18Dao.GetMaxSeqFromDT_R18(MaxSeqDto);
            //データある
            if (dtSeq.Rows.Count == 1)
            {
                if (dtSeq.Rows[0]["MAXSEQ"] != null)
                {
                    MaxSeq = SqlInt16.Parse(dtSeq.Rows[0]["MAXSEQ"].ToString());
                }
            }
            else
            {
                this.msgLogic.MessageBoxShow("E045");
                return;
            }

            try
            {
                //データ更新
                using (Transaction tran = new Transaction())
                {
                    //電子マニフェスト[DT_R18]の更新
                    dt_r18.SEQ = bIsAutoMode ? MaxSeq + 1 : LastSEQ + 1;
                    //登録情報承認待ちフラグ
                    dt_r18.SHOUNIN_FLAG = bIsAutoMode ? 1 : 0;
                    //作成日付
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //dt_r18.CREATE_DATE = DateTime.Now;
                    dt_r18.CREATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    dt_r18.CANCEL_FLAG = 0;
                    //[DT_R18]のインサート
                    DT_R18Dao.Insert(dt_r18);

                    //自動モードでキュー情報修正
                    #region QUE_INFO
                    if (bIsAutoMode)
                    {
                        //キュー情報修正(QUE_SEQの最大値 + 1インサート)
                        que_Info.KANRI_ID = dt_r18.KANRI_ID;//管理ID
                        que_Info.SEQ = dt_r18.SEQ;          //管理SEQ
                        SearchMasterDataDTOCls dto = new SearchMasterDataDTOCls();
                        dto.KANRI_ID = que_Info.KANRI_ID;
                        DataTable tmpdt = QUE_INFODao.GetQue_SeqInfo(dto);
                        if (tmpdt.Rows.Count > 0)
                        {
                            if (this.HoryuFlg) //新規でも修正でも保留の場合は上書き
                            {
                                //キューレコード枝番号
                                que_Info.QUE_SEQ = System.Data.SqlTypes.SqlInt16.Parse(tmpdt.Rows[0]["QUE_SEQ"].ToString());
                                que_Info.UPDATE_TS = (DateTime)tmpdt.Rows[0]["UPDATE_TS"];
                            }
                            else
                            {
                                //キューレコード枝番号
                                que_Info.QUE_SEQ = System.Data.SqlTypes.SqlInt16.Parse(tmpdt.Rows[0]["QUE_SEQ"].ToString()) + 1;
                            }
                        }
                        else
                        {
                            que_Info.QUE_SEQ = 1;
                        }
                        if (dt_r18.MANIFEST_KBN == 1)//予約登録
                        {
                            //機能番号の判断
                            dto = new SearchMasterDataDTOCls();
                            //排出事業者加入者番号設定される場合
                            if (!string.IsNullOrEmpty(dt_r18.HST_SHA_EDI_MEMBER_ID))
                            {
                                dto.EDI_MEMBER_ID = dt_r18.HST_SHA_EDI_MEMBER_ID;
                                DataTable dt1 = QUE_INFODao.GetMS_JWNET_MEMBERInfo(dto);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (this.HoryuFlg && this.HoryuINSFlg)
                                    {
                                        que_Info.FUNCTION_ID = maniFlag.Equals(1) ? "0101" : "0102";//機能番号[一次場合は0101、二次場合"0102"]
                                    }
                                    else
                                    {
                                        que_Info.FUNCTION_ID = maniFlag.Equals(1) ? "0201" : "0204";
                                    }
                                }
                            }
                            //排出事業者加入者番号無し場合は運搬業者の判断
                            else
                            {
                                bool bIsUnpan = false;
                                if (lstDT_R19.Count > 0)
                                {
                                    for (int i = 0; i < lstDT_R19.Count; i++)
                                    {
                                        if (!string.IsNullOrEmpty(lstDT_R19[i].UPN_SHA_EDI_MEMBER_ID))
                                        {
                                            dto = new SearchMasterDataDTOCls();
                                            dto.EDI_MEMBER_ID = lstDT_R19[i].UPN_SHA_EDI_MEMBER_ID;
                                            DataTable dt2 = QUE_INFODao.GetMS_JWNET_MEMBERInfo(dto);
                                            if (dt2.Rows.Count > 0)
                                            {
                                                //データある
                                                if (maniFlag.Equals(1))
                                                {
                                                    if (this.HoryuFlg && this.HoryuINSFlg)
                                                    {
                                                        que_Info.FUNCTION_ID = "0101";
                                                    }
                                                    else
                                                    {
                                                        que_Info.FUNCTION_ID = "0202";
                                                    }
                                                }
                                                else if (maniFlag.Equals(2))
                                                {
                                                    if (this.HoryuFlg && this.HoryuINSFlg)
                                                    {
                                                        que_Info.FUNCTION_ID = "0102";
                                                    }
                                                    else
                                                    {
                                                        que_Info.FUNCTION_ID = "0205";
                                                    }
                                                }

                                                bIsUnpan = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!bIsUnpan)//運搬が設定無し場合、処分業者の判断を行う
                                {
                                    //処分情報の判断
                                    if (!string.IsNullOrEmpty(dt_r18.SBN_SHA_MEMBER_ID))
                                    {
                                        dto.EDI_MEMBER_ID = dt_r18.SBN_SHA_MEMBER_ID;
                                        DataTable dt3 = QUE_INFODao.GetMS_JWNET_MEMBERInfo(dto);
                                        if (dt3.Rows.Count > 0)
                                        {
                                            //データある
                                            if (maniFlag.Equals(1))
                                            {
                                                if (this.HoryuFlg && this.HoryuINSFlg)
                                                {
                                                    que_Info.FUNCTION_ID = "0101";
                                                }
                                                else
                                                {
                                                    que_Info.FUNCTION_ID = "0203";
                                                }
                                            }
                                            else if (maniFlag.Equals(2))
                                            {
                                                if (this.HoryuFlg && this.HoryuINSFlg)
                                                {
                                                    que_Info.FUNCTION_ID = "0102";
                                                }
                                                else
                                                {
                                                    que_Info.FUNCTION_ID = "0206";
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        else if (dt_r18.MANIFEST_KBN == 2)//マニフェスト登録
                        {
                            // 保留情報の呼び出し登録の場合は0501or0502でセットしないとJWに飛ばない
                            if (this.HoryuFlg && this.HoryuINSFlg)
                            {
                                que_Info.FUNCTION_ID = maniFlag.Equals(1) ? "0501" : "0502";//機能番号[一次場合は0501、二次場合0502]
                            }
                            else
                            {
                                //予約からマニフェスト変更時に処理
                                if (this.ManiInfo.dt_r18.MANIFEST_KBN == 1)
                                {
                                    que_Info.FUNCTION_ID = maniFlag.Equals(1) ? "0401" : "0402";//機能番号[一次場合は0401、二次場合0402]
                                }
                                else
                                {
                                    que_Info.FUNCTION_ID = maniFlag.Equals(1) ? "0601" : "0603";//機能番号[一次場合は0601、二次場合0603]
                                }
                            }
                        }
                        //キュー状態フラグ
                        if (bHouryuFlg)//保留保存ボタンクリック登録場合
                        {
                            //7:送信保留
                            que_Info.STATUS_FLAG = 7;//7:送信保留
                        }
                        else
                        {
                            que_Info.STATUS_FLAG = 0;//0:送信前;
                        }
                        //キュー情報の更新を行う

                        // 保留情報呼び出し時はUpdateにする
                        if (this.HoryuFlg)
                        {
                            QUE_INFODao.Update(que_Info);
                        }
                        else
                        {
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //que_Info.CREATE_DATE = DateTime.Now;
                            que_Info.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            QUE_INFODao.Insert(que_Info);
                        }
                    }
                    #endregion

                    //マニフェスト目次情報[DT_MF_TOC]の修正
                    #region DT_MF_TOC
                    if (dt_mf_toc != null)
                    {
                        if (bIsAutoMode)//自動
                        {
                            //修正/取消中SEQ
                            if (!que_Info.FUNCTION_ID.Equals("0401")
                                && !que_Info.FUNCTION_ID.Equals("0402")
                                && !que_Info.FUNCTION_ID.Equals("0501")
                                && !que_Info.FUNCTION_ID.Equals("0502"))
                            {
                                dt_mf_toc.APPROVAL_SEQ = dt_r18.SEQ;
                            }
                            //JWNET登録場合
                            if (!bHouryuFlg)
                            {
                                //状態詳細フラグ
                                dt_mf_toc.STATUS_DETAIL = 1;//修正/取消中
                            }
                            //既読フラグ
                            dt_mf_toc.READ_FLAG = 2;//2(送信データ)

                            if (this.HoryuFlg && this.HoryuINSFlg)
                            {
                                // 保留保存から遷移した場合はJWNET送信していないので新規登録となる

                                //自動の場合
                                if (dt_r18.MANIFEST_KBN == 1)//予約場合
                                {
                                    //状態フラグ
                                    dt_mf_toc.STATUS_FLAG = 1;//1:予約未
                                }
                                else if (dt_r18.MANIFEST_KBN == 2)//マニフェスト場合
                                {
                                    //状態フラグ
                                    dt_mf_toc.STATUS_FLAG = 2;//2:登録未
                                }
                            }

                        }
                        else//手動の場合,状態詳細フラグだけ設定する
                        {
                            //最新SEQ
                            dt_mf_toc.LATEST_SEQ = dt_mf_toc.LATEST_SEQ + 1;
                            //状態詳細フラグ
                            dt_mf_toc.STATUS_DETAIL = 0;//通常
                        }
                        //目次情報更新
                        DT_MF_TOCDao.Update(dt_mf_toc);
                    }
                    #endregion

                    //加入者番号[DT_MF_MEMBER]
                    #region DT_MF_MEMBER
                    if (dt_mf_member != null)
                    {
                        //***加入者番号[DT_MF_MEMBER]***Start**************************************
                        //排出事業者加入者番号
                        dt_mf_member.HST_MEMBER_ID = dt_r18.HST_SHA_EDI_MEMBER_ID;
                        //運搬情報あった場合
                        if (lstDT_R19.Count > 0)
                        {
                            //収集運搬業者1加入者番号
                            dt_mf_member.UPN1_MEMBER_ID = lstDT_R19[0].UPN_SHA_EDI_MEMBER_ID;
                        }
                        if (lstDT_R19.Count > 1)
                        {
                            //収集運搬業者2加入者番号
                            dt_mf_member.UPN2_MEMBER_ID = lstDT_R19[1].UPN_SHA_EDI_MEMBER_ID;
                        }
                        if (lstDT_R19.Count > 2)
                        {
                            //収集運搬業者3加入者番号
                            dt_mf_member.UPN3_MEMBER_ID = lstDT_R19[2].UPN_SHA_EDI_MEMBER_ID;
                        }
                        if (lstDT_R19.Count > 3)
                        {
                            //収集運搬業者4加入者番号
                            dt_mf_member.UPN4_MEMBER_ID = lstDT_R19[3].UPN_SHA_EDI_MEMBER_ID;
                        }
                        if (lstDT_R19.Count > 4)
                        {
                            //収集運搬業者5加入者番号
                            dt_mf_member.UPN5_MEMBER_ID = lstDT_R19[4].UPN_SHA_EDI_MEMBER_ID;
                        }
                        //処分業者加入者番号
                        dt_mf_member.SBN_MEMBER_ID = dt_r18.SBN_SHA_MEMBER_ID;
                        //***加入者番号[DT_MF_MEMBER]***End**************************************

                        //加入者番号データの更新
                        DT_MF_MEMBERDao.Update(dt_mf_member);

                    }
                    #endregion

                    //収集運搬情報[DT_R19]
                    #region DT_R19
                    if (lstDT_R19 != null)
                    {
                        //自動と手動同じ処理です
                        foreach (DT_R19 r19 in lstDT_R19)
                        {
                            r19.KANRI_ID = dt_r18.KANRI_ID;
                            r19.SEQ = dt_r18.SEQ;
                            r19.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //r19.CREATE_DATE = DateTime.Now;
                            r19.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            //運搬情報を追加
                            DT_R19Dao.Insert(r19);
                        }
                    }
                    #endregion

                    //有害物質情報[DT_R02]
                    #region DT_R02
                    if (lstDT_R02 != null)
                    {
                        //自動と手動同じ処理です
                        foreach (DT_R02 r02 in lstDT_R02)
                        {
                            r02.KANRI_ID = dt_r18.KANRI_ID;
                            r02.SEQ = dt_r18.SEQ;
                            r02.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //r02.CREATE_DATE = DateTime.Now;
                            r02.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            //運搬情報を追加
                            DT_R02Dao.Insert(r02);
                        }
                    }
                    #endregion

                    //最終処分事業場(予定)情報[DT_R04]
                    #region DT_R04
                    if (lstDT_R04 != null)
                    {
                        //自動と手動同じ処理です
                        foreach (DT_R04 r04 in lstDT_R04)
                        {
                            r04.KANRI_ID = dt_r18.KANRI_ID;
                            r04.SEQ = dt_r18.SEQ;
                            r04.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //r04.CREATE_DATE = DateTime.Now;
                            r04.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            //運搬情報を追加
                            DT_R04Dao.Insert(r04);
                        }
                    }
                    #endregion

                    //連絡番号情報[DT_R05]
                    #region DT_R05
                    if (lstDT_R05 != null)
                    {
                        //自動と手動同じ処理です
                        foreach (DT_R05 r05 in lstDT_R05)
                        {
                            r05.KANRI_ID = dt_r18.KANRI_ID;
                            r05.SEQ = dt_r18.SEQ;
                            r05.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //r05.CREATE_DATE = DateTime.Now;
                            r05.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            //運搬情報を追加
                            DT_R05Dao.Insert(r05);
                        }
                    }
                    #endregion

                    //備考情報[DT_R06]
                    #region DT_R06
                    if (lstDT_R06 != null)
                    {
                        //自動と手動同じ処理です
                        foreach (DT_R06 r06 in lstDT_R06)
                        {
                            r06.KANRI_ID = dt_r18.KANRI_ID;
                            r06.SEQ = dt_r18.SEQ;
                            r06.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //r06.CREATE_DATE = DateTime.Now;
                            r06.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            //備考情報を追加
                            DT_R06Dao.Insert(r06);
                        }
                    }
                    #endregion

                    //最終処分終了日・事業場情報[DT_R13]
                    #region DT_R13
                    if (lstDT_R13 != null)
                    {
                        if (!bIsAutoMode)//手動の場合、最新データ追加
                        {
                            foreach (DT_R13 r13 in lstDT_R13)
                            {
                                r13.KANRI_ID = dt_r18.KANRI_ID;
                                r13.SEQ = dt_r18.SEQ;
                                r13.MANIFEST_ID = dt_r18.MANIFEST_ID;
                                //作成日付
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //r13.CREATE_DATE = DateTime.Now;
                                r13.CREATE_DATE = this.getDBDateTime();
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                //最終処分終了日・事業場情報
                                DT_R13Dao.Insert(r13);
                            }
                        }
                        else//自動の場合元データがコピーしてインサートする
                        {
                            foreach (DT_R13 r13_Old in this.ManiInfo.lstDT_R13)
                            {
                                r13_Old.KANRI_ID = dt_r18.KANRI_ID;
                                r13_Old.SEQ = dt_r18.SEQ;
                                //作成日付
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //r13_Old.CREATE_DATE = DateTime.Now;
                                r13_Old.CREATE_DATE = this.getDBDateTime();
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                //最終処分終了日・事業場情報の追加
                                DT_R13Dao.Insert(r13_Old);
                            }
                        }
                    }
                    #endregion

                    //中間産業廃棄物情報[DT_R08]
                    #region DT_R08
                    if (lstDT_R08 != null)
                    {
                        foreach (DT_R08 r08 in lstDT_R08)
                        {
                            r08.KANRI_ID = dt_r18.KANRI_ID;
                            r08.SEQ = dt_r18.SEQ;
                            r08.MANIFEST_ID = dt_r18.MANIFEST_ID;
                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //r08.CREATE_DATE = DateTime.Now;
                            r08.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            //中間産業廃棄物情報
                            DT_R08Dao.Insert(r08);
                        }
                    }
                    #endregion

                    //電子マニフェスト基本拡張[DT_R18_EX]
                    #region DT_R18_EX
                    DT_R18_EX dt_r18Ex;//最新電子基本拡張データ
                    if (dt_r18ExOld != null)
                    {
                        SuperEntity createInfo = new SuperEntity();
                        createInfo.CREATE_DATE = dt_r18ExOld.CREATE_DATE;
                        createInfo.CREATE_USER = dt_r18ExOld.CREATE_USER;
                        createInfo.CREATE_PC = dt_r18ExOld.CREATE_PC;
                        createInfoList.Add(createInfo);

                        //データ存在する場合、論理削除を行う
                        //削除フラグ
                        dt_r18ExOld.DELETE_FLG = true;
                        //電子基本拡張データ論理削除
                        var dataBinderEntry1 = new DataBinderLogic<DT_R18_EX>(dt_r18ExOld);
                        dataBinderEntry1.SetSystemProperty(dt_r18ExOld, true);
                        //論理削除
                        DT_R18_EXDao.Update(dt_r18ExOld);
                    }
                    else
                    {
                        createInfoList = new List<SuperEntity>();
                    }

                    //最新データを生成
                    dt_r18Ex = CreateDT_R18ExEntity(false, dt_r18, dt_r18ExOld);
                    var dataBinderEntry18 = new DataBinderLogic<DT_R18_EX>(dt_r18Ex);
                    dataBinderEntry18.SetSystemProperty(dt_r18Ex, false);

                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                    if (createInfoList != null && createInfoList.Count > 0)
                    {
                        dt_r18Ex.CREATE_DATE = createInfoList[0].CREATE_DATE;
                        dt_r18Ex.CREATE_USER = createInfoList[0].CREATE_USER;
                        dt_r18Ex.CREATE_PC = createInfoList[0].CREATE_PC;
                    }
                    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start

                    //データ追加
                    DT_R18_EXDao.Insert(dt_r18Ex);
                    #endregion

                    //電子マニフェスト収集運搬拡張[DT_R19_EX]
                    #region DT_R19_EX
                    if (lstDT_R19_EX != null)//既存データある
                    {
                        //論理削除
                        foreach (DT_R19_EX dt_r19Ex in lstDT_R19_EX)
                        {
                            SuperEntity createInfo = new SuperEntity();
                            createInfo.CREATE_DATE = dt_r19Ex.CREATE_DATE;
                            createInfo.CREATE_USER = dt_r19Ex.CREATE_USER;
                            createInfo.CREATE_PC = dt_r19Ex.CREATE_PC;
                            createInfoList.Add(createInfo);

                            dt_r19Ex.DELETE_FLG = true;
                            var dataBinderEntry19 = new DataBinderLogic<DT_R19_EX>(dt_r19Ex);
                            dataBinderEntry19.SetSystemProperty(dt_r19Ex, true);
                            //論理削除
                            DT_R19_EXDao.Update(dt_r19Ex);
                        }
                    }
                    else
                    {
                        createInfoList = new List<SuperEntity>();
                    }
                    count = 0;

                    //最新収集運搬拡張[DT_R19_EX]データ作成
                    List<DT_R19_EX> lstNewDT_R19Ex = CreateDT_R19ExEntityList(dt_r18Ex);//画面から情報を取得
                    foreach (DT_R19_EX dt_r19Ex in lstNewDT_R19Ex)
                    {
                        dt_r19Ex.DELETE_FLG = false;
                        var dataBinderEntry19 = new DataBinderLogic<DT_R19_EX>(dt_r19Ex);
                        dataBinderEntry19.SetSystemProperty(dt_r19Ex, true);

                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        count += 1;
                        if (createInfoList != null && createInfoList.Count > 0 && createInfoList.Count > count)
                        {
                            dt_r19Ex.CREATE_DATE = createInfoList[count].CREATE_DATE;
                            dt_r19Ex.CREATE_USER = createInfoList[count].CREATE_USER;
                            dt_r19Ex.CREATE_PC = createInfoList[count].CREATE_PC;
                        }
                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start

                        //データ追加
                        DT_R19_EXDao.Insert(dt_r19Ex);
                    }
                    #endregion

                    //電子マニフェスト最終処分（予定）拡張[DT_R04_EX]
                    #region DT_R04_EX
                    if (lstDT_R04_EX != null)//既存データある
                    {
                        //論理削除
                        foreach (DT_R04_EX dt_r04Ex in lstDT_R04_EX)
                        {
                            SuperEntity createInfo = new SuperEntity();
                            createInfo.CREATE_DATE = dt_r04Ex.CREATE_DATE;
                            createInfo.CREATE_USER = dt_r04Ex.CREATE_USER;
                            createInfo.CREATE_PC = dt_r04Ex.CREATE_PC;
                            createInfoList.Add(createInfo);

                            dt_r04Ex.DELETE_FLG = true;
                            var dataBinderEntry04 = new DataBinderLogic<DT_R04_EX>(dt_r04Ex);
                            dataBinderEntry04.SetSystemProperty(dt_r04Ex, true);
                            //論理削除
                            DT_R04_EXDao.Update(dt_r04Ex);
                        }
                    }
                    else
                    {
                        createInfoList = new List<SuperEntity>();
                    }
                    count = 0;

                    //最終処分（予定）拡張[DT_R04_EX]最新データ作成
                    List<DT_R04_EX> lstNewDT_R04Ex = CreateDT_R04ExEntityList(dt_r18Ex);//画面から情報を取得
                    foreach (DT_R04_EX dt_r04Ex in lstNewDT_R04Ex)
                    {
                        dt_r04Ex.DELETE_FLG = false;
                        var dataBinderEntry04 = new DataBinderLogic<DT_R04_EX>(dt_r04Ex);
                        dataBinderEntry04.SetSystemProperty(dt_r04Ex, true);

                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        count += 1;
                        if (createInfoList != null && createInfoList.Count > 0 && createInfoList.Count > count)
                        {
                            dt_r04Ex.CREATE_DATE = createInfoList[count].CREATE_DATE;
                            dt_r04Ex.CREATE_USER = createInfoList[count].CREATE_USER;
                            dt_r04Ex.CREATE_PC = createInfoList[count].CREATE_PC;
                        }
                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start

                        //データ追加
                        DT_R04_EXDao.Insert(dt_r04Ex);
                    }
                    #endregion

                    //電子マニフェスト最終処分拡張[DT_R13_EX]
                    #region DT_R13_EX
                    if (lstDT_R13_EX != null)
                    {
                        //論理削除
                        foreach (DT_R13_EX dt_r13Ex in lstDT_R13_EX)
                        {
                            SuperEntity createInfo = new SuperEntity();
                            createInfo.CREATE_DATE = dt_r13Ex.CREATE_DATE;
                            createInfo.CREATE_USER = dt_r13Ex.CREATE_USER;
                            createInfo.CREATE_PC = dt_r13Ex.CREATE_PC;
                            createInfoList.Add(createInfo);

                            dt_r13Ex.DELETE_FLG = true;
                            var dataBinderEntry13 = new DataBinderLogic<DT_R13_EX>(dt_r13Ex);
                            dataBinderEntry13.SetSystemProperty(dt_r13Ex, true);
                            //論理削除
                            DT_R13_EXDao.Update(dt_r13Ex);
                        }
                    }
                    else
                    {
                        createInfoList = new List<SuperEntity>();
                    }
                    count = 0;

                    //最終処分拡張[DT_R13_EX]最新データ作成
                    List<DT_R13_EX> lstNewDT_R13Ex = CreateDT_R13ExEntityList(dt_r18Ex);//画面から情報を取得
                    foreach (DT_R13_EX dt_r13Ex in lstNewDT_R13Ex)
                    {
                        dt_r13Ex.DELETE_FLG = false;
                        var dataBinderEntry13 = new DataBinderLogic<DT_R13_EX>(dt_r13Ex);
                        dataBinderEntry13.SetSystemProperty(dt_r13Ex, true);

                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        count += 1;
                        if (createInfoList != null && createInfoList.Count > 0 && createInfoList.Count > count)
                        {
                            dt_r13Ex.CREATE_DATE = createInfoList[count].CREATE_DATE;
                            dt_r13Ex.CREATE_USER = createInfoList[count].CREATE_USER;
                            dt_r13Ex.CREATE_PC = createInfoList[count].CREATE_PC;
                        }
                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start

                        //データ追加
                        DT_R13_EXDao.Insert(dt_r13Ex);
                    }
                    #endregion

                    //一次マニフェスト情報拡張[DT_R08_EX]
                    #region DT_R08_EX
                    if (lstDT_R08_EX != null)
                    {
                        //論理削除
                        foreach (DT_R08_EX dt_r08Ex in lstDT_R08_EX)
                        {
                            SuperEntity createInfo = new SuperEntity();
                            createInfo.CREATE_DATE = dt_r08Ex.CREATE_DATE;
                            createInfo.CREATE_USER = dt_r08Ex.CREATE_USER;
                            createInfo.CREATE_PC = dt_r08Ex.CREATE_PC;
                            createInfoList.Add(createInfo);

                            dt_r08Ex.DELETE_FLG = true;
                            var dataBinderEntry08 = new DataBinderLogic<DT_R08_EX>(dt_r08Ex);
                            dataBinderEntry08.SetSystemProperty(dt_r08Ex, true);
                            //論理削除
                            DT_R08_EXDao.Update(dt_r08Ex);
                        }
                    }
                    else
                    {
                        createInfoList = new List<SuperEntity>();
                    }
                    count = 0;

                    //一次マニフェスト情報拡張[DT_R08_EX]最新データ作成
                    List<DT_R08_EX> lstNewDT_R08Ex = CreateDT_R08ExEntityList(dt_r18Ex);//画面から情報を取得
                    foreach (DT_R08_EX dt_r08Ex in lstNewDT_R08Ex)
                    {
                        dt_r08Ex.DELETE_FLG = false;
                        var dataBinderEntry08 = new DataBinderLogic<DT_R08_EX>(dt_r08Ex);
                        dataBinderEntry08.SetSystemProperty(dt_r08Ex, true);

                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        count += 1;
                        if (createInfoList != null && createInfoList.Count > 0 && createInfoList.Count > count)
                        {
                            dt_r08Ex.CREATE_DATE = createInfoList[count].CREATE_DATE;
                            dt_r08Ex.CREATE_USER = createInfoList[count].CREATE_USER;
                            dt_r08Ex.CREATE_PC = createInfoList[count].CREATE_PC;
                        }
                        // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start

                        //データ追加
                        DT_R08_EXDao.Insert(dt_r08Ex);
                    }
                    //紐付けデータ最新データ作成
                    #endregion

                    //コミット
                    tran.Commit();
                    if (!this.HimodukeUpdate)
                    {
                        // 紐付時の登録の場合はアラートは表示しない
                        this.msgLogic.MessageBoxShow("I001", "更新");
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Update", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                this.isRegistErr = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Update", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                this.isRegistErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                this.isRegistErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(bIsAutoMode, bHouryuFlg, dt_r18, que_Info, dt_mf_toc, dt_mf_member, lstDT_R19, lstDT_R02, lstDT_R04, lstDT_R05, lstDT_R06, lstDT_R13, lstDT_R08, dt_r18ExOld, lstDT_R19_EX, lstDT_R04_EX, lstDT_R13_EX, lstDT_R08_EX, lstT_MANIFEST_RELATION);
            }
        }

        #endregion データ登録（更新）

        #region データ削除処理
        [Transaction]
        public void Delete(
                            bool bIsAutoMode,                   //自動フラグ
                            bool bHouryuFlg,                    //保留登録フラグ
                            DT_R18 dt_r18,                      //電子マニ情報
                            QUE_INFO que_Info,                  //キュー情報
                            DT_MF_TOC dt_mf_toc,                //目次情報
                            DT_MF_MEMBER dt_mf_member,          //加入者番号情報
                            DT_R18_EX dt_r18ExOld,              //電子基本拡張[既存データ]
                            List<DT_R19_EX> lstDT_R19_EX,       //電子運搬拡張
                            List<DT_R04_EX> lstDT_R04_EX,       //電子最終処分(予定)拡張
                            List<DT_R13_EX> lstDT_R13_EX,        //電子最終処分拡張)
                            List<DT_R08_EX> lstDT_R08_EX
                            )
        {
            LogUtility.DebugMethodStart(bIsAutoMode, bHouryuFlg, dt_r18, que_Info, dt_mf_toc, dt_mf_member, dt_r18ExOld, lstDT_R19_EX, lstDT_R04_EX, lstDT_R13_EX, lstDT_R08_EX);

            SqlDateTime create_date;
            string create_user = string.Empty;
            string create_pc = string.Empty;

            //同管理IDで枝番号の最大値 + 1に設定する
            SqlInt16 MaxSeq = 0;
            SearchMasterDataDTOCls MaxSeqDto = new SearchMasterDataDTOCls();
            MaxSeqDto.KANRI_ID = dt_r18.KANRI_ID;
            DataTable dt = DT_R18Dao.GetMaxSeqFromDT_R18(MaxSeqDto);
            //データある
            if (dt.Rows.Count == 1)
            {
                MaxSeq = SqlInt16.Parse(dt.Rows[0]["MAXSEQ"].ToString());
            }
            else
            {
                this.msgLogic.MessageBoxShow("E045");
                return;
            }

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //マニフェスト情報[DT_R18]の削除処理
                    DT_R18 delDt_r18 = new DT_R18();
                    delDt_r18.KANRI_ID = dt_r18.KANRI_ID;
                    //マニフェスト番号/交付番号
                    delDt_r18.MANIFEST_ID = dt_r18.MANIFEST_ID;
                    //同管理番号のSEQの最大値+1にデータを追加
                    delDt_r18.SEQ = MaxSeq + 1;
                    //自動
                    if (bIsAutoMode)
                    {
                        //登録情報承認待ちフラグ
                        delDt_r18.SHOUNIN_FLAG = 2;//2:取消承認待ち
                        delDt_r18.CANCEL_FLAG = 0;
                    }
                    //手動場合
                    else
                    {
                        //登録情報承認待ちフラグ
                        delDt_r18.SHOUNIN_FLAG = 1;//9:削除済み
                        //取消フラグ
                        delDt_r18.CANCEL_FLAG = 1; //
                        //取消日
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //delDt_r18.CANCEL_DATE = DateTime.Now.ToShortDateString().Replace("/", "").Replace("-", "");
                        delDt_r18.CANCEL_DATE = this.getDBDateTime().ToShortDateString().Replace("/", "").Replace("-", "");
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    }
                    //作成日付
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //delDt_r18.CREATE_DATE = DateTime.Now;
                    delDt_r18.CREATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    //DT_R18に削除データ追加
                    DT_R18Dao.Insert(delDt_r18);

                    //キュー情報取消(QUE_SEQの最大値 + 1インサート)
                    if (que_Info != null)
                    {
                        if (bIsAutoMode)//自動
                        {
                            //仕様変更で削除モードでキュー情報が何もしない
                            que_Info.KANRI_ID = dt_r18.KANRI_ID;//管理ID
                            que_Info.SEQ = dt_r18.SEQ;          //管理SEQ
                            SearchMasterDataDTOCls dto = new SearchMasterDataDTOCls();
                            dto.KANRI_ID = que_Info.KANRI_ID;
                            DataTable tmpdt = QUE_INFODao.GetQue_SeqInfo(dto);
                            if (tmpdt.Rows.Count > 0)
                            {
                                //キューレコード枝番号
                                que_Info.QUE_SEQ = System.Data.SqlTypes.SqlInt16.Parse(tmpdt.Rows[0]["QUE_SEQ"].ToString()) + 1;
                            }
                            else
                            {
                                que_Info.QUE_SEQ = 1;
                            }
                            //機能番号
                            que_Info.FUNCTION_ID = (dt_r18.MANIFEST_KBN == 1) ? "0300" : "0800";
                            //キュー状態フラグ
                            que_Info.STATUS_FLAG = (bHouryuFlg) ? SqlInt16.Parse("7") : SqlInt16.Parse("0");

                            //作成日付
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //que_Info.CREATE_DATE = DateTime.Now;
                            que_Info.CREATE_DATE = this.getDBDateTime();
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            QUE_INFODao.Insert(que_Info);
                        }
                    }

                    //マニフェスト目次情報[DT_MF_TOC]の取消
                    if (dt_mf_toc != null)
                    {
                        if (bIsAutoMode)//自動
                        {
                            //修正/取消中SEQ
                            dt_mf_toc.APPROVAL_SEQ = delDt_r18.SEQ;
                            //JWNET登録場合
                            if (!bHouryuFlg)
                            {
                                //状態詳細フラグ
                                dt_mf_toc.STATUS_DETAIL = 1;//修正/取消中
                            }
                            //既読フラグ
                            dt_mf_toc.READ_FLAG = 2;//2(送信データ)
                        }
                        else//手動の場合,最後のSEQ更新、状態詳細フラグだけ設定する
                        {
                            //最新SEQ
                            dt_mf_toc.LATEST_SEQ = dt_mf_toc.LATEST_SEQ + 1;
                            dt_mf_toc.STATUS_DETAIL = 0;//通常
                            dt_mf_toc.STATUS_FLAG = 9;  //削除済み
                        }
                        //目次情報更新
                        DT_MF_TOCDao.Update(dt_mf_toc);
                    }
                    //電子マニフェスト基本拡張[DT_R18_EX]削除処理
                    if (dt_r18ExOld != null && !bIsAutoMode)
                    {
                        create_date = dt_r18ExOld.CREATE_DATE;
                        create_user = dt_r18ExOld.CREATE_USER;
                        create_pc = dt_r18ExOld.CREATE_PC;

                        //削除フラグ
                        dt_r18ExOld.DELETE_FLG = true;
                        //電子基本拡張データ論理削除
                        var dataBinderEntry1 = new DataBinderLogic<DT_R18_EX>(dt_r18ExOld);
                        dataBinderEntry1.SetSystemProperty(dt_r18ExOld, true);
                        //論理削除
                        DT_R18_EXDao.Update(dt_r18ExOld);

                        // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        // 更新者情報設定
                        var updateInfo = new DataBinderLogic<r_framework.Entity.DT_R18_EX>(dt_r18ExOld);
                        updateInfo.SetSystemProperty(dt_r18ExOld, false);
                        dt_r18ExOld.SEQ += 1;
                        dt_r18ExOld.CREATE_DATE = create_date;
                        dt_r18ExOld.CREATE_USER = create_user;
                        dt_r18ExOld.CREATE_PC = create_pc;
                        DT_R18_EXDao.Insert(dt_r18ExOld);
                        // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end
                    }
                    //電子マニフェスト収集運搬拡張[DT_R19_EX]
                    if (lstDT_R19_EX != null && !bIsAutoMode)//既存データある
                    {
                        //論理削除
                        foreach (DT_R19_EX dt_r19Ex in lstDT_R19_EX)
                        {
                            create_date = dt_r19Ex.CREATE_DATE;
                            create_user = dt_r19Ex.CREATE_USER;
                            create_pc = dt_r19Ex.CREATE_PC;

                            dt_r19Ex.DELETE_FLG = true;
                            var dataBinderEntry19 = new DataBinderLogic<DT_R19_EX>(dt_r19Ex);
                            dataBinderEntry19.SetSystemProperty(dt_r19Ex, true);
                            //論理削除
                            DT_R19_EXDao.Update(dt_r19Ex);

                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                            // 更新者情報設定
                            var updateInfo = new DataBinderLogic<r_framework.Entity.DT_R19_EX>(dt_r19Ex);
                            updateInfo.SetSystemProperty(dt_r19Ex, false);
                            dt_r19Ex.SEQ += 1;
                            dt_r19Ex.CREATE_DATE = create_date;
                            dt_r19Ex.CREATE_USER = create_user;
                            dt_r19Ex.CREATE_PC = create_pc;
                            DT_R19_EXDao.Insert(dt_r19Ex);
                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end
                        }
                    }
                    //電子マニフェスト最終処分（予定）拡張[DT_R04_EX]
                    if (lstDT_R04_EX != null && !bIsAutoMode)//既存データある
                    {
                        //論理削除
                        foreach (DT_R04_EX dt_r04Ex in lstDT_R04_EX)
                        {
                            create_date = dt_r04Ex.CREATE_DATE;
                            create_user = dt_r04Ex.CREATE_USER;
                            create_pc = dt_r04Ex.CREATE_PC;

                            dt_r04Ex.DELETE_FLG = true;
                            var dataBinderEntry04 = new DataBinderLogic<DT_R04_EX>(dt_r04Ex);
                            dataBinderEntry04.SetSystemProperty(dt_r04Ex, true);
                            //論理削除
                            DT_R04_EXDao.Update(dt_r04Ex);

                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                            // 更新者情報設定
                            var updateInfo = new DataBinderLogic<r_framework.Entity.DT_R04_EX>(dt_r04Ex);
                            updateInfo.SetSystemProperty(dt_r04Ex, false);
                            dt_r04Ex.SEQ += 1;
                            dt_r04Ex.CREATE_DATE = create_date;
                            dt_r04Ex.CREATE_USER = create_user;
                            dt_r04Ex.CREATE_PC = create_pc;
                            DT_R04_EXDao.Insert(dt_r04Ex);
                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end
                        }
                    }
                    //電子マニフェスト最終処分拡張[DT_R13_EX]
                    if (lstDT_R13_EX != null && !bIsAutoMode)
                    {
                        //論理削除
                        foreach (DT_R13_EX dt_r13Ex in lstDT_R13_EX)
                        {
                            create_date = dt_r13Ex.CREATE_DATE;
                            create_user = dt_r13Ex.CREATE_USER;
                            create_pc = dt_r13Ex.CREATE_PC;

                            dt_r13Ex.DELETE_FLG = true;
                            var dataBinderEntry13 = new DataBinderLogic<DT_R13_EX>(dt_r13Ex);
                            dataBinderEntry13.SetSystemProperty(dt_r13Ex, true);
                            //論理削除
                            DT_R13_EXDao.Update(dt_r13Ex);

                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                            // 更新者情報設定
                            var updateInfo = new DataBinderLogic<r_framework.Entity.DT_R13_EX>(dt_r13Ex);
                            updateInfo.SetSystemProperty(dt_r13Ex, false);
                            dt_r13Ex.SEQ += 1;
                            dt_r13Ex.CREATE_DATE = create_date;
                            dt_r13Ex.CREATE_USER = create_user;
                            dt_r13Ex.CREATE_PC = create_pc;
                            DT_R13_EXDao.Insert(dt_r13Ex);
                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end
                        }
                    }

                    //一次マニフェスト情報拡張[DT_R08_EX]
                    if (lstDT_R08_EX != null && !bIsAutoMode)
                    {
                        //論理削除
                        foreach (DT_R08_EX dt_r08Ex in lstDT_R08_EX)
                        {
                            create_date = dt_r08Ex.CREATE_DATE;
                            create_user = dt_r08Ex.CREATE_USER;
                            create_pc = dt_r08Ex.CREATE_PC;

                            dt_r08Ex.DELETE_FLG = true;
                            var dataBinderEntry08 = new DataBinderLogic<DT_R08_EX>(dt_r08Ex);
                            dataBinderEntry08.SetSystemProperty(dt_r08Ex, true);
                            //論理削除
                            DT_R08_EXDao.Update(dt_r08Ex);

                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                            // 更新者情報設定
                            var updateInfo = new DataBinderLogic<r_framework.Entity.DT_R08_EX>(dt_r08Ex);
                            updateInfo.SetSystemProperty(dt_r08Ex, false);
                            dt_r08Ex.SEQ += 1;
                            dt_r08Ex.CREATE_DATE = create_date;
                            dt_r08Ex.CREATE_USER = create_user;
                            dt_r08Ex.CREATE_PC = create_pc;
                            DT_R08_EXDao.Insert(dt_r08Ex);
                            // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end
                        }
                    }

                    //コミット
                    tran.Commit();

                    //INXS start Delete INXS manifets refs #158004
                    if (AppConfig.AppOptions.IsInxsManifest() && this.isUploadToInxs)
                    {
                        this.inxsManifestLogic.DeleteInxsData(dt_r18.KANRI_ID, this.form.transactionId, ((BusinessBaseForm)this.form.Parent).Text);
                    }
                    //INXS end

                    this.msgLogic.MessageBoxShow("I001", "削除");

                    if (Manager.CheckAuthority("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        //新規モードに変更
                        this.form.InitializeFormByMode(WINDOW_TYPE.NEW_WINDOW_FLAG);
                    }
                    else
                    {
                        // 画面を閉じる
                        this.form.FormClose(null, null);
                    }

                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Delete", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Delete", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Delete", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(bIsAutoMode, bHouryuFlg, dt_r18, que_Info, dt_mf_toc, dt_mf_member, dt_r18ExOld, lstDT_R19_EX, lstDT_R04_EX, lstDT_R13_EX, lstDT_R08_EX);
            }
        }

        #endregion データ削除処理

        #region 未登録データ削除処理
        [Transaction]
        public void MDelete(
                            DT_R18 dt_r18,                      //電子マニ情報
                            QUE_INFO que_Info                  //キュー情報
                            )
        {
            LogUtility.DebugMethodStart( dt_r18, que_Info);

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //キュー情報取消
                    if (que_Info != null)
                    {
                        que_Info.KANRI_ID = dt_r18.KANRI_ID;//管理ID
                        SearchMasterDataDTOCls dto = new SearchMasterDataDTOCls();
                        dto.KANRI_ID = que_Info.KANRI_ID;
                        DataTable tmpdt = QUE_INFODao.GetQue_SeqInfo_DEL(dto);
                        if (tmpdt.Rows.Count > 0)
                        {
                            //キューレコード枝番号
                            que_Info.QUE_SEQ = System.Data.SqlTypes.SqlInt16.Parse(tmpdt.Rows[0]["QUE_SEQ"].ToString());
                            //キュー状態フラグ
                            que_Info.STATUS_FLAG = 6;
                            //作成日付
                            que_Info.UPDATE_TS = (DateTime)tmpdt.Rows[0]["UPDATE_TS"];

                            QUE_INFODao.UpdateM(que_Info);
                        }
                    }

                    //コミット
                    tran.Commit();

                    //INXS start Delete INXS manifets refs #158004
                    if (AppConfig.AppOptions.IsInxsManifest() && this.isUploadToInxs)
                    {
                        this.inxsManifestLogic.DeleteInxsData(dt_r18.KANRI_ID, this.form.transactionId, ((BusinessBaseForm)this.form.Parent).Text);
                    }
                    //INXS end

                    this.msgLogic.MessageBoxShow("I001", "削除");

                    if (Manager.CheckAuthority("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        //新規モードに変更
                        this.form.InitializeFormByMode(WINDOW_TYPE.NEW_WINDOW_FLAG);
                    }
                    else
                    {
                        // 画面を閉じる
                        this.form.FormClose(null, null);
                    }

                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("MDelete", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("MDelete", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("MDelete", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(dt_r18, que_Info);
            }
        }

        #endregion 未登録データ削除処理

        /// <summary>
        /// 加入者番号より報告不要状態の取得処理
        /// </summary>
        /// <param name="EDI_MEMBER_ID">加入者番号</param>
        /// <returns></returns>
        public bool GetNO_REP_FLG(string EDI_MEMBER_ID)
        {
            //報告不要フラグ取得処理
            bool bIsNotRep = false;
            if (string.IsNullOrEmpty(EDI_MEMBER_ID)) return bIsNotRep;
            DenshiMasterDataLogic dmdl = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
            dto.EDI_MEMBER_ID = EDI_MEMBER_ID;
            DataTable dt = dmdl.GetDenshiGyoushaData(dto);
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["HOUKOKU_HUYOU_KBN"] != null)
                {
                    bIsNotRep = (dt.Rows[0]["HOUKOKU_HUYOU_KBN"] != null) ? (bool)dt.Rows[0]["HOUKOKU_HUYOU_KBN"] : false;
                }
            }
            return bIsNotRep;
        }

        #region 修正、削除モードでデータがDBから読込処理 開始
        /// <summary>
        /// DBからマニ情報を読込処理
        /// </summary>
        /// <param name="KanriId"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public DenshiManifestInfoCls GetManiInfoFromDB(string KanriId, SqlInt16 Seq)
        {
            DenshiManifestInfoCls ManiInfo = new DenshiManifestInfoCls();

            //電子マニフェスト[DT_R18]
            DT_R18 dt_r18 = new DT_R18();
            dt_r18.KANRI_ID = KanriId;
            dt_r18.SEQ = Seq;
            ManiInfo.dt_r18 = DT_R18Dao.GetDataForEntity(dt_r18);

            //キュー情報[QUE_INFO]
            QUE_INFO que_info = new QUE_INFO();
            //que_info.KANRI_ID = KanriId;
            //ManiInfo.que_Info = QUE_INFODao.GetDataForEntity(que_info);
            //マニフェスト目次情報[DT_MF_TOC]
            DT_MF_TOC dt_mf_toc = new DT_MF_TOC();
            dt_mf_toc.KANRI_ID = KanriId;
            ManiInfo.dt_mf_toc = DT_MF_TOCDao.GetDataForEntity(dt_mf_toc);
            //手動自動モードの設定
            //2013.12.19 naitou upd start
            //ManiInfo.bIsAutoMode = (bool)(ManiInfo.dt_mf_toc.KIND != SqlDecimal.Parse("5"));

            ManiInfo.bIsAutoMode = (ManiInfo.dt_mf_toc.KIND == 5) ? false : true;
            //2013.12.19 naitou upd end

            //加入者番号[DT_MF_MEMBER]
            DT_MF_MEMBER dt_mf_member = new DT_MF_MEMBER();
            dt_mf_member.KANRI_ID = KanriId;
            ManiInfo.dt_mf_member = DT_MF_MEMBERDao.GetDataForEntity(dt_mf_member);
            //収集運搬情報[DT_R19]
            DT_R19 dt_r19 = new DT_R19();
            dt_r19.KANRI_ID = KanriId;
            dt_r19.SEQ = Seq;
            DT_R19[] aryDT_R19 = DT_R19Dao.GetAllValidData(dt_r19);
            for (int i = 0; i < aryDT_R19.Length; i++)
            {
                ManiInfo.lstDT_R19.Add(aryDT_R19[i]);
            }
            //有害物質情報[DT_R02]
            DT_R02 dt_r02 = new DT_R02();
            dt_r02.KANRI_ID = KanriId;
            dt_r02.SEQ = Seq;
            DT_R02[] aryDT_R02 = DT_R02Dao.GetAllValidData(dt_r02);
            for (int i = 0; i < aryDT_R02.Length; i++)
            {
                ManiInfo.lstDT_R02.Add(aryDT_R02[i]);
            }
            //最終処分事業場(予定)情報[DT_R04]
            DT_R04 dt_r04 = new DT_R04();
            dt_r04.KANRI_ID = KanriId;
            dt_r04.SEQ = Seq;
            DT_R04[] aryDT_R04 = DT_R04Dao.GetAllValidData(dt_r04);
            for (int i = 0; i < aryDT_R04.Length; i++)
            {
                ManiInfo.lstDT_R04.Add(aryDT_R04[i]);
            }
            //連絡番号情報[DT_R05]
            DT_R05 dt_r05 = new DT_R05();
            dt_r05.KANRI_ID = KanriId;
            dt_r05.SEQ = Seq;
            DT_R05[] aryDT_R05 = DT_R05Dao.GetAllValidData(dt_r05);
            for (int i = 0; i < aryDT_R05.Length; i++)
            {
                ManiInfo.lstDT_R05.Add(aryDT_R05[i]);
            }
            //備考情報[DT_R06]
            DT_R06 dt_r06 = new DT_R06();
            dt_r06.KANRI_ID = KanriId;
            dt_r06.SEQ = Seq;
            DT_R06[] aryDT_R06 = DT_R06Dao.GetAllValidData(dt_r06);
            for (int i = 0; i < aryDT_R06.Length; i++)
            {
                ManiInfo.lstDT_R06.Add(aryDT_R06[i]);
            }
            //最終処分終了日・事業場情報[DT_R13]
            DT_R13 dt_r13 = new DT_R13();
            dt_r13.KANRI_ID = KanriId;
            dt_r13.SEQ = Seq;
            DT_R13[] aryDT_R13 = DT_R13Dao.GetAllValidData(dt_r13);
            for (int i = 0; i < aryDT_R13.Length; i++)
            {
                ManiInfo.lstDT_R13.Add(aryDT_R13[i]);
            }
            //電子マニフェスト基本拡張[DT_R18_EX]
            DT_R18_EX de_r18ExDto = new DT_R18_EX();
            de_r18ExDto.KANRI_ID = KanriId;
            ManiInfo.dt_r18ExOld = DT_R18_EXDao.GetDataForEntity(de_r18ExDto);

            //電子マニフェスト基本拡張[DT_R18_EX]データあった場合、連携テーブルをデータ取得
            if (ManiInfo.dt_r18ExOld != null)
            {
                //電子マニフェスト収集運搬拡張[DT_R19_EX]
                DT_R19_EX dt_r19_ExDto = new DT_R19_EX();
                dt_r19_ExDto.SYSTEM_ID = ManiInfo.dt_r18ExOld.SYSTEM_ID;
                dt_r19_ExDto.SEQ = ManiInfo.dt_r18ExOld.SEQ;
                DT_R19_EX[] aryDT_R19_EX = DT_R19_EXDao.GetAllValidData(dt_r19_ExDto);
                for (int i = 0; i < aryDT_R19_EX.Length; i++)
                {
                    ManiInfo.lstDT_R19_EX.Add(aryDT_R19_EX[i]);
                }
                //電子マニフェスト最終処分（予定）拡張[DT_R04_EX]
                DT_R04_EX dt_r04_ExDto = new DT_R04_EX();
                dt_r04_ExDto.SYSTEM_ID = ManiInfo.dt_r18ExOld.SYSTEM_ID;
                dt_r04_ExDto.SEQ = ManiInfo.dt_r18ExOld.SEQ;
                DT_R04_EX[] aryDT_R04_EX = DT_R04_EXDao.GetAllValidData(dt_r04_ExDto);
                for (int i = 0; i < aryDT_R04_EX.Length; i++)
                {
                    ManiInfo.lstDT_R04_EX.Add(aryDT_R04_EX[i]);
                }
                //電子マニフェスト最終処分拡張[DT_R13_EX]
                DT_R13_EX dt_r13_ExDto = new DT_R13_EX();
                dt_r13_ExDto.SYSTEM_ID = ManiInfo.dt_r18ExOld.SYSTEM_ID;
                dt_r13_ExDto.SEQ = ManiInfo.dt_r18ExOld.SEQ;
                DT_R13_EX[] aryDT_R13_EX = DT_R13_EXDao.GetAllValidData(dt_r13_ExDto);
                for (int i = 0; i < aryDT_R13_EX.Length; i++)
                {
                    ManiInfo.lstDT_R13_EX.Add(aryDT_R13_EX[i]);
                }

                //1次マニフェスト情報[DT_R08]
                DT_R08 dt_r08Dto = new DT_R08();
                dt_r08Dto.KANRI_ID = ManiInfo.dt_r18.KANRI_ID;
                dt_r08Dto.SEQ = Seq;
                DT_R08[] aryDT_R08 = DT_R08Dao.GetAllValidData(dt_r08Dto);
                for (int i = 0; i < aryDT_R08.Length; i++)
                {
                    ManiInfo.lstDT_R08.Add(aryDT_R08[i]);
                }

                //電子マニフェスト基本拡張[DT_R08_EX]
                DT_R08_EX dt_r08_ExDto = new DT_R08_EX();
                dt_r08_ExDto.SYSTEM_ID = ManiInfo.dt_r18ExOld.SYSTEM_ID;
                dt_r08_ExDto.SEQ = ManiInfo.dt_r18ExOld.SEQ;
                DT_R08_EX[] aryDT_R08_EX = DT_R08_EXDao.GetAllValidData(dt_r08_ExDto);
                for (int i = 0; i < aryDT_R08_EX.Length; i++)
                {
                    ManiInfo.lstDT_R08_EX.Add(aryDT_R08_EX[i]);
                }

                //マニフェスト紐付[T_MANIFEST_RELATION]
                T_MANIFEST_RELATION t_Manifest_RelationDto = new T_MANIFEST_RELATION();
                t_Manifest_RelationDto.NEXT_SYSTEM_ID = ManiInfo.dt_r18ExOld.SYSTEM_ID;
                t_Manifest_RelationDto.SEQ = ManiInfo.dt_r18ExOld.SEQ;
                T_MANIFEST_RELATION[] aryT_Manifest_Relation = T_MANIFEST_RELATIONDao.GetAllValidData(t_Manifest_RelationDto);
                for (int i = 0; i < aryT_Manifest_Relation.Length; i++)
                {
                    ManiInfo.lstT_MANIFEST_RELATION.Add(aryT_Manifest_Relation[i]);
                }

            }


            return ManiInfo;
        }

        #endregion 修正、削除モードでデータがDBから読込処理 終了

        #region チェック処理
        /// <summary>
        /// 必須入力項目チェック処理
        /// </summary>
        /// <returns></returns>
        public bool CHk_MustbeInputItem(DenshiManifestInfoCls maniInfo)
        {

            if (this.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG) return true;
            bool bRet = false;

            //排出事業者CD入力必須チェック
            if (string.IsNullOrEmpty(maniInfo.dt_r18.HST_SHA_EDI_MEMBER_ID))
            {
                //マニ修正と手動でマニ新規登録場合空白可
                if (this.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG && !maniInfo.bIsAutoMode ||
                    this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && maniInfo.dt_r18.MANIFEST_KBN == 2)
                {
                    //
                }
                else
                {
                    msgLogic.MessageBoxShow("E001", "排出事業者");
                    this.form.cantxt_HaisyutuGyousyaCd.Focus();
                    this.form.cantxt_HaisyutuGyousyaCd.SelectAll();
                    return bRet;
                }
            }


            //[新規モードで予約登録](JWNET登録失敗からのMODE=UPDATEも含む)入力チェック
            //複数明細行あり、「廃棄物種類CD、数量、単位CD、荷姿CD、数量確定者CD」すべて未入力のデータはアラート対象とする
            if (this.Mode != WINDOW_TYPE.DELETE_WINDOW_FLAG && maniInfo.dt_r18.MANIFEST_KBN == 1 && maniInfo.bIsAutoMode)
            {
                int RecordCnt = 0;
                for (int i = 0; i < this.form.cdgv_Haikibutu.Rows.Count; i++)
                {
                    if (!this.form.cdgv_Haikibutu.Rows[i].IsNewRow)
                    {
                        RecordCnt +=1;
                    }
                }

                if (RecordCnt > 1)
                {
                    object tmpObjHaikiY = null;
                    int Delflg = 0;
                    for (int i = RecordCnt-1; i >= 0; i--)
                    {
                        int CheckCnt = 0;
                        if (!this.form.cdgv_Haikibutu.Rows[i].IsNewRow)
                        {
                            RecordCnt += 1;
                            //【産業廃棄物-廃棄物種類CD】
                            tmpObjHaikiY = this.form.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SHURUI_NAME"].Value;
                            if ((tmpObjHaikiY == null) || (string.IsNullOrEmpty(tmpObjHaikiY.ToString()))) { CheckCnt += 1; }
                            //【産業廃棄物-数量】
                            tmpObjHaikiY = this.form.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SUU"].Value;
                            if (tmpObjHaikiY == null) { CheckCnt += 1; }
                            //【産業廃棄物-単位CD】
                            tmpObjHaikiY = this.form.cdgv_Haikibutu.Rows[i].Cells["UNIT_CD"].Value;
                            if ((tmpObjHaikiY == null) || (string.IsNullOrEmpty(tmpObjHaikiY.ToString()))) { CheckCnt += 1; }
                            //【産業廃棄物-荷姿CD】
                            tmpObjHaikiY = this.form.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_CD"].Value;
                            if ((tmpObjHaikiY == null) || (string.IsNullOrEmpty(tmpObjHaikiY.ToString()))) { CheckCnt += 1; }
                            //【産業廃棄物-数量確定者CD】
                            tmpObjHaikiY = this.form.cdgv_Haikibutu.Rows[i].Cells["SUU_KAKUTEI_CODE"].Value;
                            if ((tmpObjHaikiY == null) || (string.IsNullOrEmpty(tmpObjHaikiY.ToString()))) { CheckCnt += 1; }
                        }

                        if (CheckCnt == 5)
                        {
                            if (Delflg == 0)
                            {
                                DialogResult result = this.msgLogic.MessageBoxShowConfirm("産業廃棄物（明細行）が2行以上登録されています。\n廃棄物種類CD、数量、単位CD、荷姿CD、数量確定者CD が\n全て未入力の行を破棄しますがよろしいでしょうか？", MessageBoxDefaultButton.Button1);
                                if (result.Equals(DialogResult.Yes))
                                {
                                    Delflg = 1;
                                    this.form.cdgv_Haikibutu.Rows.RemoveAt(i);
                                }
                                else
                                {
                                    //削除しないで抜ける
                                    return bRet;
                                }
                            }
                            else
                            {
                                //該当行を削除
                                this.form.cdgv_Haikibutu.Rows.RemoveAt(i);
                            }
                        }
                    }
                }
            }


            //[修正モードで予約利用]または[新規モードでマニ登録]または[修正モードでマニ登録]の場合、入力必須チェック
            if ((this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && (maniInfo.dt_r18.MANIFEST_KBN == 2 && this.ManiInfo.dt_r18.MANIFEST_KBN == 1)) ||
                ((this.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG && maniInfo.dt_r18.MANIFEST_KBN == 2 && maniInfo.bIsAutoMode)))
            {
                //引渡し日付チェック
                if (string.IsNullOrEmpty(maniInfo.dt_r18.HIKIWATASHI_DATE))
                {
                    msgLogic.MessageBoxShow("E001", "引渡日");
                    this.form.cdate_HikiwataDate.Focus();
                    this.form.cdate_HikiwataDate.Select();
                    return bRet;
                }
                //【引渡担当者】
                if (string.IsNullOrEmpty(maniInfo.dt_r18.HIKIWATASHI_TAN_NAME))
                {
                    msgLogic.MessageBoxShow("E001", "引渡担当者");
                    this.form.ctxt_HikiwataTantouSha.Focus();
                    this.form.ctxt_HikiwataTantouSha.Select();
                    return bRet;
                }
                //【排出事業場-CD】
                if (string.IsNullOrEmpty(this.form.cantxt_HaisyutuGenbaCd.Text))
                {
                    //マニ修正場合【排出事業場-CD】空白可
                    if ((this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && maniInfo.dt_r18.MANIFEST_KBN == 2))
                    {
                        //
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E001", "排出事業場");
                        this.form.cantxt_HaisyutuGenbaCd.Focus();
                        this.form.cantxt_HaisyutuGenbaCd.SelectAll();
                        return bRet;
                    }
                }

                //産業廃棄物の項目は全ての行をチェックする
                object tmpObjHaiki = null;
                for (int i = 0; i < this.form.cdgv_Haikibutu.Rows.Count; i++)
                {
                    if (!this.form.cdgv_Haikibutu.Rows[i].IsNewRow)
                    {
                        //【産業廃棄物-廃棄物種類CD】
                        tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SHURUI_NAME"].Value;
                        if ((tmpObjHaiki == null) || (string.IsNullOrEmpty(tmpObjHaiki.ToString())))
                        {
                            msgLogic.MessageBoxShow("E001", "廃棄物種類");
                            this.form.cdgv_Haikibutu.Focus();
                            this.form.cdgv_Haikibutu.CurrentCell = this.form.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SHURUI_CD"];
                            return bRet;
                        }
                    
                        //【産業廃棄物-数量】
                        tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SUU"].Value;
                        if (tmpObjHaiki == null)
                        {
                            msgLogic.MessageBoxShow("E001", "廃棄物の数量");
                            this.form.cdgv_Haikibutu.Focus();
                            this.form.cdgv_Haikibutu.CurrentCell = this.form.cdgv_Haikibutu.Rows[i].Cells["HAIKI_SUU"];
                            return bRet;
                        }

                        //【産業廃棄物-単位CD】
                        tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[i].Cells["UNIT_CD"].Value;
                        if ((tmpObjHaiki == null) || (string.IsNullOrEmpty(tmpObjHaiki.ToString())))
                        {
                            msgLogic.MessageBoxShow("E001", "廃棄物の単位");
                            this.form.cdgv_Haikibutu.Focus();
                            this.form.cdgv_Haikibutu.CurrentCell = this.form.cdgv_Haikibutu.Rows[i].Cells["UNIT_CD"];
                            return bRet;
                        }

                        //【産業廃棄物-荷姿CD】
                        tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_CD"].Value;
                        if ((tmpObjHaiki == null) || (string.IsNullOrEmpty(tmpObjHaiki.ToString())))
                        {
                            msgLogic.MessageBoxShow("E001", "廃棄物の荷姿");
                            this.form.cdgv_Haikibutu.Focus();
                            this.form.cdgv_Haikibutu.CurrentCell = this.form.cdgv_Haikibutu.Rows[i].Cells["NISUGATA_CD"];
                            return bRet;
                        }

                        //【産業廃棄物-数量確定者CD】
                        tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[i].Cells["SUU_KAKUTEI_CODE"].Value;
                        if ((tmpObjHaiki == null) || (string.IsNullOrEmpty(tmpObjHaiki.ToString())))
                        {
                            msgLogic.MessageBoxShow("E001", "数量確定者");
                            this.form.cdgv_Haikibutu.Focus();
                            this.form.cdgv_Haikibutu.CurrentCell = this.form.cdgv_Haikibutu.Rows[i].Cells["SUU_KAKUTEI_CODE"];
                            return bRet;
                        }
                    }
                }
                //【最終処分の場所(予定)-委託契約書記載のとおり】
                //【最終処分の場所(予定)-当欄記載のとおり】
                if (string.IsNullOrEmpty(maniInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG))
                {
                    msgLogic.MessageBoxShow("E001", "最終処分の場所(予定)のチェックボックス");
                    this.form.ccbx_Toulanshitei.Focus();
                    return bRet;
                }
                //【最終処分の場所(予定)-最終処分事業場CD】
                //当欄記載場合最終処分事業場CD入力必須
                if (maniInfo.dt_r18.LAST_SBN_JOU_KISAI_FLAG == "1")
                {
                    List<DT_R04_EX> lstNewDT_R04Ex = SetDT_R04ExEntityListFromForm();
                    if (lstNewDT_R04Ex == null || lstNewDT_R04Ex.Count < 1)
                    {
                        // 「当欄記載のとおり」の場合は必須
                        msgLogic.MessageBoxShow("E001", "最終処分の場所(予定)");
                        this.form.cdgv_LastSBNbasyo_yotei.Focus();
                        return bRet;
                    }
                    else if (lstNewDT_R04Ex.Count > 0)
                    {
                        foreach (DT_R04_EX dt_r04_Ex in lstNewDT_R04Ex)
                        {
                            if (string.IsNullOrEmpty(dt_r04_Ex.LAST_SBN_GENBA_CD))
                            {
                                //マニ修正場合【最終処分の場所(予定)-最終処分事業場CD】空白可
                                if ((this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && maniInfo.dt_r18.MANIFEST_KBN == 2))
                                {
                                    //
                                }
                                else
                                {
                                    msgLogic.MessageBoxShow("E001", "最終処分の場所(予定)の最終処分事業場CD");
                                    this.form.cdgv_LastSBNbasyo_yotei.Focus();
                                    this.form.cdgv_LastSBNbasyo_yotei.CurrentCell = this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_GYOUSHA_CD"];
                                    return bRet;
                                }
                            }
                        }
                    }

                }
                //【処分受託者-CD】
                if (string.IsNullOrEmpty(this.form.cantxt_SBN_JyutakuShaCD.Text))
                {
                    //マニ修正場合【処分受託者-CD】空白可
                    if ((this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && maniInfo.dt_r18.MANIFEST_KBN == 2))
                    {
                        //
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E001", "処分受託者");
                        this.form.cantxt_SBN_JyutakuShaCD.Focus();
                        this.form.cantxt_SBN_JyutakuShaCD.SelectAll();
                        return bRet;
                    }
                }
            }

            // 有害物質CDが重複しているかチェックする。
            bool yuugaiCdError = false;
            List<string> yuugaiCdList = new List<string>();
            for (int i = 0; i < this.form.cdgv_Haikibutu.Rows.Count; i++)
            {
                // 有害物質CD１～６をリストに格納し、重複していたらエラーとする。
                for (int cnt = 1; cnt < 7; cnt++)
                {
                    object yuugaiCd = this.form.cdgv_Haikibutu.Rows[i].Cells["YUUGAI_CODE" + cnt].Value;
                    if (yuugaiCd != null && !string.IsNullOrEmpty(yuugaiCd.ToString()))
                    {
                        if (yuugaiCdList.Contains(yuugaiCd.ToString()))
                        {
                            yuugaiCdError = true;
                            break;
                        }
                        else
                        {
                            yuugaiCdList.Add(yuugaiCd.ToString());
                        }
                    }
                }

                if (yuugaiCdError)
                {
                    msgLogic.MessageBoxShowError("有害物質が重複しているため、登録できません。");
                    return bRet;
                }

                // 最後にリストはクリアする。
                yuugaiCdList.Clear();
            }

            // 予約マニの場合、収集運搬情報と運搬先情報のチェックは行わない。
            if (maniInfo.dt_r18.MANIFEST_KBN == 1)
            {
                //予約マニ修正）処分受託者情報が変更されていないかのチェックを行う
                if (this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (!string.IsNullOrEmpty(this.form.yoyaku_shobun_gyousyaCd))
                    {
                        if (this.form.yoyaku_shobun_gyousyaCd != this.form.cantxt_SBN_JyutakuShaCD.Text)
                        {
                            msgLogic.MessageBoxShowError("処分受託者情報が更新されている為、登録できません。");
                            return bRet;
                        }
                    }

                    if (!string.IsNullOrEmpty(this.form.yoyaku_shobun_genbaCd))
                    {
                        if (this.form.yoyaku_shobun_genbaCd != this.form.cantxt_SBN_Genba_CD.Text)
                        {
                            msgLogic.MessageBoxShowError("処分受託者情報が更新されている為、登録できません。");
                            return bRet;
                        }
                    }
                }
            }
            else
            {
                //1区間以上の場合は区間チェックを行う
                List<DT_R19_EX> lstNewDT_R19Ex = SetDT_R19ExEntityListFromForm();

                // 必須チェック
                foreach (var r19 in lstNewDT_R19Ex)
                {
                    // 収集運搬業者
                    if (r19.UPN_GYOUSHA_CD == null
                            || string.IsNullOrEmpty(r19.UPN_GYOUSHA_CD))
                    {
                        msgLogic.MessageBoxShow("E001", string.Format("収集運搬業者CD(区間{0})", r19.UPN_ROUTE_NO));
                        return bRet;
                    }
                }

                //区間チェック
                //①.運搬先業者と次の区間の収集運搬業者が一致しチェック
                for (int i = 0; i < lstNewDT_R19Ex.Count; i++)
                {
                    //中間区間場合
                    if (i != lstNewDT_R19Ex.Count - 1)
                    {
                        // 運搬先業者と次の運搬業者と一致させる制約はない
                    }
                    //最終区間場合
                    else
                    {
                        string UPNSAKI_GYOUSHA_CD = lstNewDT_R19Ex[i].UPNSAKI_GYOUSHA_CD;
                        string SBN_GYOUSHA_CD = this.form.cantxt_SBN_JyutakuShaCD.Text;
                        if (!SBN_GYOUSHA_CD.Equals(UPNSAKI_GYOUSHA_CD))
                        {
                            msgLogic.MessageBoxShow("E118", "運搬情報で最終区間の運搬先業者と処分受託者が\r\n一致しないため登録できません。");
                            this.form.cantxt_SBN_JyutakuShaCD.Focus();
                            this.form.cantxt_SBN_JyutakuShaCD.SelectAll();
                            return bRet;
                        }
                        //最終区間の運搬先事業場と処分事業場一致しチェック
                        string UPNSAKI_GENBA_CD = lstNewDT_R19Ex[i].UPNSAKI_GENBA_CD;
                        string SBN_GENBA_CD = this.form.cantxt_SBN_Genba_CD.Text;
                        if (!SBN_GENBA_CD.Equals(UPNSAKI_GENBA_CD))
                        {
                            msgLogic.MessageBoxShow("E119", "運搬情報で最終区間の運搬先事業場と処分事業場が\r\n一致しないため登録できません。");
                            this.form.cantxt_SBN_Genba_CD.Focus();
                            this.form.cantxt_SBN_Genba_CD.SelectAll();
                            return bRet;
                        }
                    }
                }
                //加入者番号チェック
                if (maniInfo.dt_mf_member.HST_MEMBER_ID.Equals(maniInfo.dt_mf_member.SBN_MEMBER_ID))
                {
                    bool bIsEqual = true;
                    foreach (DT_R19 dt_r19 in maniInfo.lstDT_R19)
                    {
                        if (!dt_r19.UPN_SHA_EDI_MEMBER_ID.Equals(maniInfo.dt_mf_member.HST_MEMBER_ID))
                        {
                            bIsEqual = false;
                            break;
                        }
                    }
                    if (bIsEqual)
                    {
                        msgLogic.MessageBoxShow("E121", "1者間運用（収集運搬業者と処分業者の全てが、自己もしくは報告不要である場合）となる情報は、登録できません。");
                        return bRet;
                    }
                }
                //報告不要チェック
                DT_R18_EX dt_r18Ex = SetDT_R18ExDataFromForm(maniInfo.dt_r18, 0, false);
                if (dt_r18Ex != null)
                {
                    //処分受託者報告不要判断し
                    bool bIsNoRep = (dt_r18Ex.NO_REP_SBN_EDI_MEMBER_ID != null);
                    //毎区間で、収集運搬業者の報告不要判断し
                    foreach (DT_R19_EX dt_r19_Ex in lstNewDT_R19Ex)
                    {
                        if (dt_r19_Ex.NO_REP_UPN_EDI_MEMBER_ID == null)
                        {
                            bIsNoRep = false;
                            break;
                        }
                    }
                    //全て報告不要場合、エラー出し
                    if (bIsNoRep)
                    {
                        msgLogic.MessageBoxShow("E121", "1者間運用（収集運搬業者と処分業者の全てが、自己もしくは報告不要である場合）となる情報は、登録できません。");
                        return bRet;
                    }
                    //自己処分場合の判断
                    if (!string.IsNullOrEmpty(dt_r18Ex.HST_GYOUSHA_CD) && !string.IsNullOrEmpty(dt_r18Ex.SBN_GYOUSHA_CD))
                    {
                        //自己処分場合、運搬区間の収集運搬業者の報告不要判断し
                        bIsNoRep = (dt_r18Ex.HST_GYOUSHA_CD == dt_r18Ex.SBN_GYOUSHA_CD);
                        if (bIsNoRep)
                        {
                            foreach (DT_R19_EX dt_r19_Ex in lstNewDT_R19Ex)
                            {
                                if (dt_r19_Ex.NO_REP_UPN_EDI_MEMBER_ID == null)
                                {
                                    bIsNoRep = false;
                                    break;
                                }
                            }
                        }
                        //自己処分かつ運搬業者が全部報告不要の場合、エラー出す
                        if (bIsNoRep)
                        {
                            msgLogic.MessageBoxShow("E121", "1者間運用（収集運搬業者と処分業者の全てが、自己もしくは報告不要である場合）となる情報は、登録できません。");
                            return bRet;
                        }

                    }
                    //自己運搬の判断
                    bIsNoRep = true;
                    foreach (DT_R19 dt_r19 in maniInfo.lstDT_R19)
                    {
                        if (!dt_r19.UPN_SHA_EDI_MEMBER_ID.Equals(maniInfo.dt_mf_member.HST_MEMBER_ID))
                        {
                            bIsNoRep = false;
                            break;
                        }
                    }
                    //自己運搬の場合
                    if (bIsNoRep)
                    {
                        //処分受託者の報告不要の場合、エラー出す
                        if (dt_r18Ex.NO_REP_SBN_EDI_MEMBER_ID != null)
                        {
                            msgLogic.MessageBoxShow("E121", "1者間運用（収集運搬業者と処分業者の全てが、自己もしくは報告不要である場合）となる情報は、登録できません。");
                            return bRet;
                        }
                    }

                }
            }

            // 住所に半角英数字が含まれるかチェック
            if (this.CheckAddress(maniInfo))
            {
                return bRet;
            }

            // 20141103 koukouei 委託契約チェック start
            if (this.maniFlag == 1 && !this.CheckItakukeiyaku())
            {
                return false;
            }
            // 20141103 koukouei 委託契約チェック end

            //日付チェック
            if (maniInfo.dt_r18.MANIFEST_KBN != 1 && !Chk_All_Date_IsValid(maniInfo))
            {
                return bRet;
            }


            return true;
        }
        /// <summary>
        /// 日付チェック処理
        /// </summary>
        /// <param name="maniInfo"></param>
        /// <returns></returns>
        private bool Chk_All_Date_IsValid(DenshiManifestInfoCls maniInfo)
        {
            bool bRet = false;
            //日付チェック
            //登録日<=運搬終了日<=廃棄物の受領日<=処分終了日<=最終処分終了日<未来日

            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
            //DateTime Today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime Today = Convert.ToDateTime(this.parentbaseform.sysDate.ToShortDateString());
            // 20150922 katen #12048 「システム日付」の基準作成、適用 end

            //手動の場合は、各種終了日チェックを行う
            if (!maniInfo.bIsAutoMode)
            {
                //運搬終了日チェック
                //運搬終了日が設定された区間リスト
                List<string> lstUpnDate = new List<string>();
                for (int i = 0; i < maniInfo.lstDT_R19.Count; i++)
                {
                    if (!string.IsNullOrEmpty(maniInfo.lstDT_R19[i].UPN_END_DATE))
                    {
                        DateTime dtUpn = DateTime.ParseExact(maniInfo.lstDT_R19[i].UPN_END_DATE,
                                                             "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        if (DateTime.Compare(dtUpn, Today) > 0)
                        {
                            msgLogic.MessageBoxShow("E124");
                            return bRet;
                        }

                        //リストに追加
                        lstUpnDate.Add(maniInfo.lstDT_R19[i].UPN_END_DATE);
                    }
                }

                //運搬終了日入力した区間数が１つ以上の場合チェックを行う
                if (lstUpnDate.Count > 1)
                {
                    DateTime dtUpn1 = DateTime.ParseExact(lstUpnDate[0], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime dtUpn2 = DateTime.ParseExact(lstUpnDate[1], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    if (DateTime.Compare(dtUpn1, dtUpn2) > 0)
                    {
                        msgLogic.MessageBoxShow("E124");
                        return bRet;
                    }
                }
                if (lstUpnDate.Count > 2)
                {
                    DateTime dtUpn2 = DateTime.ParseExact(lstUpnDate[1], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime dtUpn3 = DateTime.ParseExact(lstUpnDate[2], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    if (DateTime.Compare(dtUpn2, dtUpn3) > 0)
                    {
                        msgLogic.MessageBoxShow("E124");
                        return bRet;
                    }
                }
                if (lstUpnDate.Count > 3)
                {
                    DateTime dtUpn3 = DateTime.ParseExact(lstUpnDate[2], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime dtUpn4 = DateTime.ParseExact(lstUpnDate[3], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    if (DateTime.Compare(dtUpn3, dtUpn4) > 0)
                    {
                        msgLogic.MessageBoxShow("E124");
                        return bRet;
                    }
                }
                if (lstUpnDate.Count > 4)
                {
                    DateTime dtUpn4 = DateTime.ParseExact(lstUpnDate[3], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime dtUpn5 = DateTime.ParseExact(lstUpnDate[4], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    if (DateTime.Compare(dtUpn4, dtUpn5) > 0)
                    {
                        msgLogic.MessageBoxShow("E124");
                        return bRet;
                    }
                }
                //入力したの最終区間の運搬終了日の取得
                string LastEndDate = string.Empty;
                if (lstUpnDate.Count > 0)
                {
                    LastEndDate = lstUpnDate[lstUpnDate.Count - 1];
                }
                //廃棄物の受領日<=処分終了日<=最終処分終了日<未来日
                if (!string.IsNullOrEmpty(LastEndDate))
                {
                    DateTime lastEndUpnDate = DateTime.ParseExact(LastEndDate,
                                                                  "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    //廃棄物の受領日
                    if (!string.IsNullOrEmpty(maniInfo.dt_r18.HAIKI_IN_DATE))
                    {
                        DateTime HAIKI_IN_DATE = DateTime.ParseExact(maniInfo.dt_r18.HAIKI_IN_DATE,
                                                                     "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        if (DateTime.Compare(lastEndUpnDate, HAIKI_IN_DATE) > 0)
                        {
                            msgLogic.MessageBoxShow("E124");
                            this.form.cdtp_HaikiAcceptDate.Focus();
                            this.form.cdtp_HaikiAcceptDate.SelectAll();
                            return bRet;
                        }
                    }
                }
                //運搬終了日未設定の場合、廃棄物の受領日と登録日を比較
                else
                {
                    // 廃棄物の受領日と登録日を比較
                    if (!string.IsNullOrEmpty(maniInfo.dt_r18.HAIKI_IN_DATE))
                    {
                        DateTime HAIKI_IN_DATE = DateTime.ParseExact(maniInfo.dt_r18.HAIKI_IN_DATE,
                            "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        if (DateTime.Compare(HAIKI_IN_DATE, Today) > 0)
                        {
                            msgLogic.MessageBoxShow("E124");
                            this.form.cdtp_HaikiAcceptDate.Focus();
                            this.form.cdtp_HaikiAcceptDate.SelectAll();
                            return bRet;
                        }
                    }
                }
                //処分終了日のチェック
                if (!string.IsNullOrEmpty(maniInfo.dt_r18.SBN_END_DATE))
                {
                    DateTime SBN_END_DATE = DateTime.ParseExact(maniInfo.dt_r18.SBN_END_DATE,
                        "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    if (!string.IsNullOrEmpty(maniInfo.dt_r18.HAIKI_IN_DATE))
                    {
                        DateTime HAIKI_IN_DATE = DateTime.ParseExact(maniInfo.dt_r18.HAIKI_IN_DATE,
                            "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        if (DateTime.Compare(HAIKI_IN_DATE, SBN_END_DATE) > 0)
                        {
                            msgLogic.MessageBoxShow("E124");
                            this.form.cdtp_SBNEndDate.Focus();
                            this.form.cdtp_SBNEndDate.SelectAll();
                            return bRet;
                        }
                    }
                    //廃棄物の受領日未設定の場合、処分終了日と比較
                    else
                    {
                        if (!string.IsNullOrEmpty(LastEndDate))
                        {
                            DateTime lastEndUpnDate = DateTime.ParseExact(LastEndDate,
                                                                         "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            if (DateTime.Compare(lastEndUpnDate, SBN_END_DATE) > 0)
                            {
                                msgLogic.MessageBoxShow("E124");
                                this.form.cdtp_SBNEndDate.Focus();
                                this.form.cdtp_SBNEndDate.SelectAll();
                                return bRet;
                            }
                        }
                        //処分終了日未設定の場合、登録日と比較
                        else
                        {
                            // 登録日と比較
                            if (DateTime.Compare(SBN_END_DATE, Today) > 0)
                            {
                                msgLogic.MessageBoxShow("E124");
                                this.form.cdtp_SBNEndDate.Focus();
                                this.form.cdtp_SBNEndDate.SelectAll();
                                return bRet;
                            }
                        }
                    }
                }
                //最終処分終了報告日チェック
                if (!string.IsNullOrEmpty(maniInfo.dt_r18.LAST_SBN_END_REP_DATE))
                {
                    DateTime LAST_SBN_END_REP_DATE = DateTime.ParseExact(maniInfo.dt_r18.LAST_SBN_END_REP_DATE,
                        "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    if (!string.IsNullOrEmpty(maniInfo.dt_r18.SBN_END_DATE))
                    {
                        DateTime SBN_END_DATE = DateTime.ParseExact(maniInfo.dt_r18.SBN_END_DATE,
                            "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        if (DateTime.Compare(SBN_END_DATE, LAST_SBN_END_REP_DATE) > 0)
                        {
                            msgLogic.MessageBoxShow("E124");
                            this.form.cdpt_LastSBNEndDate.Focus();
                            this.form.cdpt_LastSBNEndDate.SelectAll();
                            return bRet;
                        }
                    }
                    //最終処分終了報告日未設定の場合、廃棄物の受領日と比較
                    else
                    {
                        if (!string.IsNullOrEmpty(maniInfo.dt_r18.HAIKI_IN_DATE))
                        {
                            DateTime HAIKI_IN_DATE = DateTime.ParseExact(maniInfo.dt_r18.HAIKI_IN_DATE,
                                "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                            if (DateTime.Compare(HAIKI_IN_DATE, LAST_SBN_END_REP_DATE) > 0)
                            {
                                msgLogic.MessageBoxShow("E124");
                                this.form.cdpt_LastSBNEndDate.Focus();
                                this.form.cdpt_LastSBNEndDate.SelectAll();
                                return bRet;
                            }
                        }
                        //廃棄物の受領日未設定の場合、最終処分終了報告日と比較
                        else
                        {
                            if (!string.IsNullOrEmpty(LastEndDate))
                            {
                                DateTime lastEndUpnDate = DateTime.ParseExact(LastEndDate,
                                 "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                if (DateTime.Compare(lastEndUpnDate, LAST_SBN_END_REP_DATE) > 0)
                                {
                                    msgLogic.MessageBoxShow("E124");
                                    this.form.cdpt_LastSBNEndDate.Focus();
                                    this.form.cdpt_LastSBNEndDate.SelectAll();
                                    return bRet;
                                }
                            }
                            //最終処分終了報告日未設定の場合、登録日と比較
                            else
                            {
                                // 登録日と比較
                                if (DateTime.Compare(LAST_SBN_END_REP_DATE, Today) > 0)
                                {
                                    msgLogic.MessageBoxShow("E124");
                                    this.form.cdpt_LastSBNEndDate.Focus();
                                    this.form.cdpt_LastSBNEndDate.SelectAll();
                                    return bRet;
                                }
                            }
                        }
                    }
                }

                // 引渡日チェック
                DateTime HIKIWATASHI_DATE = DateTime.MinValue;
                if (!string.IsNullOrEmpty(maniInfo.dt_r18.HIKIWATASHI_DATE))
                {
                    // 引渡し日を取得
                    HIKIWATASHI_DATE = DateTime.ParseExact(maniInfo.dt_r18.HIKIWATASHI_DATE,
                                                                   "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                    // 登録日と引渡し日を比較
                    if (DateTime.Compare(HIKIWATASHI_DATE, Today) > 0)
                    {
                        // 引渡し日が登録日より未来日になっていた場合はメッセージ表示
                        if (DialogResult.No == msgLogic.MessageBoxShow("C075"))
                        {
                            // 「いいえ」を選択した場合はエラー扱い
                            this.form.cdate_HikiwataDate.Focus();
                            this.form.cdate_HikiwataDate.SelectAll();
                            return bRet;
                        }
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// グリッドの中に禁則文字チェック処理
        /// </summary>
        /// <returns></returns>
        private bool Chk_Dgv_MoziDisable()
        {
            bool bIsCheckOK = true;
            //廃棄物種類グリッド
            for (int i = 0; i < this.form.cdgv_Haikibutu.Rows.Count; i++)
            {
                for (int j = 0; j < this.form.cdgv_Haikibutu.Columns.Count; j++)
                {
                    if (this.form.cdgv_Haikibutu.Rows[i].Cells[j].ReadOnly == false)
                    {
                        DgvCustomTextBoxCell cell = this.form.cdgv_Haikibutu.Rows[i].Cells[j] as DgvCustomTextBoxCell;
                        if (cell != null)
                        {
                            //フォマート未設定の場合、禁則文字チェックを行う
                            if (string.IsNullOrEmpty(cell.CustomFormatSetting))
                            {
                                object tmpobj = this.form.cdgv_Haikibutu.Rows[i].Cells[j].Value;
                                if (tmpobj != null)
                                {
                                    bool catchErr = false;
                                    var retcheck = KinsokuMoziCheck(tmpobj.ToString(), out catchErr);
                                    if (catchErr)
                                    {
                                        return false;
                                    }

                                    if (retcheck == false)
                                    {
                                        this.form.cdgv_Haikibutu.Focus();
                                        this.form.cdgv_Haikibutu.CurrentCell = this.form.cdgv_Haikibutu.Rows[i].Cells[j];
                                        return false;
                                    }
                                }
                            }
                        }

                    }
                }
            }

            //中間処理産業廃棄物
            for (int i = 0; i < this.form.cdgv_Tyukanshori.Rows.Count; i++)
            {
                for (int j = 0; j < this.form.cdgv_Tyukanshori.Columns.Count; j++)
                {
                    if (this.form.cdgv_Tyukanshori.Rows[i].Cells[j].ReadOnly == false)
                    {
                        object tmpobj = this.form.cdgv_Tyukanshori.Rows[i].Cells[j].Value;
                        if (tmpobj != null)
                        {
                            bool catchErr = false;
                            var retcheck = this.KinsokuMoziCheck(tmpobj.ToString(), out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }

                            if (retcheck == false)
                            {
                                this.form.cdgv_Tyukanshori.Focus();
                                this.form.cdgv_Tyukanshori.CurrentCell = this.form.cdgv_Tyukanshori.Rows[i].Cells[j];
                                return false;
                            }
                        }
                    }
                }
            }
            //最終処分情報『予定』
            for (int i = 0; i < this.form.cdgv_LastSBNbasyo_yotei.Rows.Count; i++)
            {
                for (int j = 0; j < this.form.cdgv_LastSBNbasyo_yotei.Columns.Count; j++)
                {
                    if (this.form.cdgv_LastSBNbasyo_yotei.Rows[i].Cells[j].ReadOnly == false)
                    {
                        object tmpobj = this.form.cdgv_LastSBNbasyo_yotei.Rows[i].Cells[j].Value;
                        if (tmpobj != null)
                        {
                            bool catchErr = false;
                            var retcheck = this.KinsokuMoziCheck(tmpobj.ToString(), out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                            if (retcheck == false)
                            {
                                this.form.cdgv_LastSBNbasyo_yotei.Focus();
                                this.form.cdgv_LastSBNbasyo_yotei.CurrentCell = this.form.cdgv_LastSBNbasyo_yotei.Rows[i].Cells[j];
                                return false;
                            }
                        }
                    }

                }
            }
            //運搬情報
            for (int i = 0; i < this.form.cdgv_UnpanInfo.Rows.Count; i++)
            {
                for (int j = 0; j < this.form.cdgv_UnpanInfo.Columns.Count; j++)
                {
                    if (this.form.cdgv_UnpanInfo.Rows[i].Cells[j].ReadOnly == false)
                    {
                        object tmpobj = this.form.cdgv_UnpanInfo.Rows[i].Cells[j].Value;
                        if (tmpobj != null)
                        {
                            bool catchErr = false;
                            var retcheck = this.KinsokuMoziCheck(tmpobj.ToString(), out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                            if (retcheck == false)
                            {
                                this.form.cdgv_UnpanInfo.Focus();
                                this.form.cdgv_UnpanInfo.CurrentCell = this.form.cdgv_UnpanInfo.Rows[i].Cells[j];
                                return false;
                            }
                        }
                    }
                }
            }
            //最終処分情報『実績』
            for (int i = 0; i < this.form.cdgv_LastSBN_Genba_Jiseki.Rows.Count; i++)
            {
                for (int j = 0; j < this.form.cdgv_LastSBN_Genba_Jiseki.Columns.Count; j++)
                {
                    if (this.form.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells[j].ReadOnly == false)
                    {
                        object tmpobj = this.form.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells[j].Value;
                        if (tmpobj != null)
                        {
                            bool catchErr = false;
                            var retcheck = this.KinsokuMoziCheck(tmpobj.ToString(), out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                            if (retcheck == false)
                            {
                                this.form.cdgv_LastSBN_Genba_Jiseki.Focus();
                                this.form.cdgv_LastSBN_Genba_Jiseki.CurrentCell = this.form.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells[j];
                                return false;
                            }
                        }
                    }
                }
            }
            //備考
            for (int i = 0; i < this.form.cdgv_Bikou.Rows.Count; i++)
            {
                for (int j = 0; j < this.form.cdgv_Bikou.Columns.Count; j++)
                {
                    if (this.form.cdgv_Bikou.Rows[i].Cells[j].ReadOnly == false)
                    {
                        object tmpobj = this.form.cdgv_Bikou.Rows[i].Cells[j].Value;
                        if (tmpobj != null)
                        {
                            bool catchErr = false;
                            var retcheck = this.KinsokuMoziCheck(tmpobj.ToString(), out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                            if (retcheck == false)
                            {
                                this.form.cdgv_Bikou.Focus();
                                this.form.cdgv_Bikou.CurrentCell = this.form.cdgv_Bikou.Rows[i].Cells[j];
                                return false;
                            }
                        }
                    }
                }
            }

            return bIsCheckOK;
        }

        /// <summary>
        /// コントロールの中に禁則文字チェック
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool Chk_All_MoziDisable(Control root)
        {
            Control[] all = this.GetAllControls(root);

            foreach (Control ctl in all)
            {
                if (ctl is CustomTextBox)
                {
                    CustomTextBox ctb = (CustomTextBox)ctl;

                    if (!ctb.ReadOnly)
                    {
                        //フォマート未設定の場合、禁則文字チェックを行う
                        if (string.IsNullOrEmpty(ctb.CustomFormatSetting))
                        {
                            bool catchErr = false;
                            var retcheck = this.KinsokuMoziCheck(ctb.Text, out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }

                            if (!retcheck)
                            {
                                ctb.Focus();
                                ctb.SelectAll();
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// すべてのコントロールを再帰的に取得
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public Control[] GetAllControls(Control top)
        {
            System.Collections.ArrayList buf = new System.Collections.ArrayList();
            foreach (Control c in top.Controls)
            {
                buf.Add(c);
                buf.AddRange(GetAllControls(c));
            }
            return (Control[])buf.ToArray(typeof(Control));
        }

        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="insertVal">登録項目</param>
        public bool KinsokuMoziCheck(string insertVal, out bool catchErr)
        {
            catchErr = false;
            try
            {
                Validator v = new Validator();

                if (!v.isJWNetValidShiftJisCharForSign(insertVal))
                {
                    this.msgLogic.MessageBoxShow("E071", "該当箇所");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("KinsokuMoziCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// マニデータ存在チェック[新規以外モードで使用]
        /// </summary>
        /// <param name="KanriID"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public bool IsManiInfoExist(string KanriID, SqlInt16 Seq, out bool catchErr)
        {
            bool bExistFlg = false;
            catchErr = false;
            try
            {
                DT_R18 dt_r18Dto = new DT_R18();
                dt_r18Dto.KANRI_ID = KanriID;
                dt_r18Dto.SEQ = Seq;
                DT_R18 dt_r18 = DT_R18Dao.GetDataForEntity(dt_r18Dto);
                if (dt_r18 != null)
                {
                    bExistFlg = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsManiInfoExist", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsManiInfoExist", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            return bExistFlg;
        }

        /// <summary>
        /// 交付番号入力チェック
        /// </summary>
        public bool ChkKohuNo(string ManifestNo, out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {

                LogUtility.DebugMethodStart();

                if (ManifestNo.Length != 11 ||
                    System.Text.RegularExpressions.Regex.IsMatch(ManifestNo, "^[0-9]+$") == false)
                {
                    this.msgLogic.MessageBoxShow("E012", "11桁の数値");
                    return false;
                }

                string msg = this.ChkDigitKohuNo(ManifestNo);
                if (msg != string.Empty)
                {
                    this.msgLogic.MessageBoxShow("E126", msg);
                    return false;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChkKohuNo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkKohuNo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }
        /// <summary>
        /// 交付番号文字チェック
        /// </summary>
        public string ChkDigitKohuNo(string ManifestNo)
        {
            LogUtility.DebugMethodStart();

            if (ManifestNo == string.Empty)
            {
                return string.Empty;
            }

            long lSumResult;
            lSumResult = 0;
            long lResult;
            lResult = 0;
            //1文字ずつ10文字までを加算
            for (int i = 0; i <= 9; ++i)
            {
                int iTemp;
                iTemp = 0;
                if (int.TryParse(ManifestNo.Substring(i, 1), out iTemp) == true)
                {
                    lSumResult += iTemp;
                }
            }

            lResult = (lSumResult % 10);

            if (lResult.ToString() == ManifestNo.Substring(10, 1))
            {
                //成功で戻る
                return string.Empty;
            }

            LogUtility.DebugMethodEnd();
            return lResult.ToString();
        }

        /// <summary>
        /// 交付番号存在チェック
        /// </summary>
        public bool ExistManifestNo(string ManifestNo, out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                //新規モードでチェックを行う
                if (this.Mode == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {

                    LogUtility.DebugMethodStart();

                    DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                    dto.MANIFEST_ID = ManifestNo;
                    DataTable dt = new DataTable();
                    DenshiMasterDataLogic mstLogic = new DenshiMasterDataLogic();
                    dt = mstLogic.SearchDenshiManifestNo(dto);
                    if (dt.Rows.Count > 0)
                    {
                        this.msgLogic.MessageBoxShow("E031", "マニフェスト番号");
                        return true;
                    }
                }
                //新規モード以外場合はチェックしない
                ret = false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ExistManifestNo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                //新規モード以外場合はチェックしない
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExistManifestNo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                //新規モード以外場合はチェックしない
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 中間処理産業廃棄物チェック
        /// 二次マニモード時のみチェックする
        /// </summary>
        /// <returns>
        /// true:エラーの場合
        /// false:正常の場合
        /// </returns>
        public bool ChkFirstManifest()
        {
            //二次マニモード以外の場合、
            if (this.maniFlag != 2 || this.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG) return false;

            if ("1".Equals(this.form.cntxt_ManiKBN.Text))
            {
                if (!this.form.ccbx_Touranshitei.Checked && !this.form.ccbx_ChouboKisai.Checked)
                {
                    //中間処理産業廃棄物のチェックボックス
                    this.msgLogic.MessageBoxShow("E001", "中間処理産業廃棄物のチェックボックス");
                    if (this.form.ccbx_Touranshitei.Focused) { }
                    else if (this.form.ccbx_ItijiFuyou.Focused) { }
                    else if (this.form.ccbx_ChouboKisai.Focused) { }
                    return true;
                }
            }
            else if ("2".Equals(this.form.cntxt_ManiKBN.Text))
            {
                if (!this.form.ccbx_Touranshitei.Checked && !this.form.ccbx_ItijiFuyou.Checked && !this.form.ccbx_ChouboKisai.Checked)
                {
                    //中間処理産業廃棄物のチェックボックス
                    this.msgLogic.MessageBoxShow("E001", "中間処理産業廃棄物のチェックボックス");
                    if (this.form.ccbx_Touranshitei.Focused) { }
                    else if (this.form.ccbx_ItijiFuyou.Focused) { }
                    else if (this.form.ccbx_ChouboKisai.Focused) { }
                    return true;
                }
            }

            //正常の場合
            return false;
        }

        /// <summary>
        /// 中間処理産業廃棄物登録チェック
        /// 二次マニモード時のみチェックする
        /// </summary>
        /// <returns>
        /// true:1件もない場合
        /// false:データが存在する場合
        /// </returns>
        public bool ChkRegistFirstManifest()
        {
            //二次マニモード以外の場合、
            if (this.maniFlag != 2 || this.Mode == WINDOW_TYPE.DELETE_WINDOW_FLAG
                || !"2".Equals(this.form.cntxt_ManiKBN.Text)) return true;

            bool isExistsManifest = true;

            if (this.form.ccbx_Touranshitei.Checked)
            {
                for (int i = 0; i < this.form.cdgv_Tyukanshori.Rows.Count; i++)
                {
                    if (this.form.cdgv_Tyukanshori.Rows.Count == 1
                        || i != this.form.cdgv_Tyukanshori.Rows.Count - 1)
                    {
                        if (this.form.cdgv_Tyukanshori.Rows[i].Cells["FM_MANIFEST_ID"].Value == null)
                        {
                            isExistsManifest = false;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return isExistsManifest;

        }

        /// <summary>
        /// 紐付済かどうかチェック
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <param name="maniFlg"></param>
        /// <returns>true:紐付け済み、false:未紐付け</returns>
        internal bool ChkRelation(SqlInt64 SYSTEM_ID, int maniFlg)
        {
            // TODO: BusinessCommonのManifestoLogic#ChkRelationと共通にしたい
            var dt = new DataTable();

            if (maniFlg == 1)
            {
                // 一次
                dt = this.T_MANIFEST_RELATIONDao.GetRelationNexttMani(SYSTEM_ID);
            }
            else
            {
                // 二次
                dt = this.T_MANIFEST_RELATIONDao.GetRelationFirstMani(SYSTEM_ID);
            }
            return (dt.Rows.Count > 0); //データがあったら紐付あり
        }

        /// <summary>
        /// QUE_INFOが保留/JWNETエラー状態かチェックする
        /// </summary>
        /// <returns></returns>
        internal bool que_info_chk()
        {
            QUE_INFO que_info = new QUE_INFO();
            que_info.KANRI_ID = KanriId;
            que_info.SEQ = Seq;

            QUE_INFO que_info2 = new QUE_INFO();
            que_info2 = QUE_INFODao.GetDataForEntity(que_info);
            if (que_info2 != null)
            {
                if (que_info2.FUNCTION_ID == "0101" || que_info2.FUNCTION_ID == "0102" || que_info2.FUNCTION_ID == "0501" || que_info2.FUNCTION_ID == "0502")
                {
                    HoryuINSFlg = true;
                }
                
                if (que_info2.STATUS_FLAG == 7 || que_info2.STATUS_FLAG == 8 || que_info2.STATUS_FLAG == 9)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion チェック処理　完了

        #region 各種名称取得処理

        /// <summary>
        /// 数量確定者名称取得処理
        /// </summary>
        /// <param name="KakuteiCD"></param>
        /// <returns></returns>
        public string GetKakuteishaName(string KakuteiCD)
        {
            string Name = string.Empty;
            if (KakuteiCD == "01") return "排出事業者";
            if (KakuteiCD == "02") return "処分業者";
            if (KakuteiCD == "03") return "収集運搬業者（区間1）";
            if (KakuteiCD == "04") return "収集運搬業者（区間2）";
            if (KakuteiCD == "05") return "収集運搬業者（区間3）";
            if (KakuteiCD == "06") return "収集運搬業者（区間4）";
            if (KakuteiCD == "07") return "収集運搬業者（区間5）";

            return Name;
        }

        #endregion 各種名称取得処理 完了

        #region 電子拡張データ作成メソッド

        /// <summary>
        /// 電子マニフェスト最終処分拡張[DT_R13_EX]データ作成処理
        /// </summary>
        /// <param name="Newdt_r18Ex"></param>
        /// <returns></returns>
        private List<DT_R13_EX> CreateDT_R13ExEntityList(DT_R18_EX Newdt_r18Ex)
        {
            if (Newdt_r18Ex == null) { return null; }
            LogUtility.DebugMethodStart(Newdt_r18Ex);
            List<DT_R13_EX> lstNewDT_R13Ex = new List<DT_R13_EX>();
            //画面から、最終処分拡張[実績]データ取得
            lstNewDT_R13Ex = SetDT_R13ExEntityListFromForm();

            foreach (DT_R13_EX dt_r13Ex in lstNewDT_R13Ex)
            {
                dt_r13Ex.SYSTEM_ID = Newdt_r18Ex.SYSTEM_ID;         //最新データのシステムID
                dt_r13Ex.SEQ = Newdt_r18Ex.SEQ;                     //最新データのSEQ
                dt_r13Ex.MANIFEST_ID = Newdt_r18Ex.MANIFEST_ID;     //最新データの交付番号
                dt_r13Ex.KANRI_ID = Newdt_r18Ex.KANRI_ID;           //最新データの管理番号
            }

            LogUtility.DebugMethodEnd(Newdt_r18Ex);

            return lstNewDT_R13Ex;
        }

        /// <summary>
        /// 最終処分拡張[DT_R13_EX]データ画面から取得処理
        /// </summary>
        /// <returns></returns>
        private List<DT_R13_EX> SetDT_R13ExEntityListFromForm()
        {
            LogUtility.DebugMethodStart();
            List<DT_R13_EX> lstNewDT_R13Ex = new List<DT_R13_EX>();
            for (int i = 0; i < this.form.cdgv_LastSBN_Genba_Jiseki.Rows.Count; i++)
            {
                //行の有効チェック
                bool bValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_LastSBN_Genba_Jiseki.Rows[i]);
                if (!bValidRow) break;
                DT_R13_EX dt_r13Ex = new DT_R13_EX();
                //レコード枝番号
                dt_r13Ex.REC_SEQ = SqlDecimal.Parse((lstNewDT_R13Ex.Count + 1).ToString());
                //最終処分業者CD(実績)
                object tmpobj = this.form.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].Value;
                if (tmpobj != null)
                {
                    dt_r13Ex.LAST_SBN_GYOUSHA_CD = tmpobj.ToString();
                }
                //最終処分事業場CD(実績)
                tmpobj = this.form.cdgv_LastSBN_Genba_Jiseki.Rows[i].Cells["LAST_SBN_JOU_JISEKI_CD"].Value;
                if (tmpobj != null)
                {
                    dt_r13Ex.LAST_SBN_GENBA_CD = tmpobj.ToString();
                }
                //リストに追加
                lstNewDT_R13Ex.Add(dt_r13Ex);
            }
            LogUtility.DebugMethodEnd();
            return lstNewDT_R13Ex;
        }

        /// <summary>
        /// 電子マニフェスト最終処分（予定）拡張[DT_R04_EX]データ作成処理
        /// </summary>
        /// <param name="Newdt_r18Ex"></param>
        /// <returns></returns>
        private List<DT_R04_EX> CreateDT_R04ExEntityList(DT_R18_EX Newdt_r18Ex)
        {
            if (Newdt_r18Ex == null) { return null; }
            LogUtility.DebugMethodStart(Newdt_r18Ex);
            //画面から、運搬情報拡張データ取得
            List<DT_R04_EX> lstNewDT_R04Ex = SetDT_R04ExEntityListFromForm();
            foreach (DT_R04_EX dt_r04Ex in lstNewDT_R04Ex)
            {
                dt_r04Ex.SYSTEM_ID = Newdt_r18Ex.SYSTEM_ID;         //最新データのシステムID
                dt_r04Ex.SEQ = Newdt_r18Ex.SEQ;                     //最新データのSEQ
                dt_r04Ex.MANIFEST_ID = Newdt_r18Ex.MANIFEST_ID;     //最新データの交付番号
                dt_r04Ex.KANRI_ID = Newdt_r18Ex.KANRI_ID;           //最新データの管理番号
            }

            LogUtility.DebugMethodEnd(Newdt_r18Ex);

            return lstNewDT_R04Ex;
        }
        /// <summary>
        /// 最終処分（予定）拡張[DT_R04_EX]データ画面から取得処理
        /// </summary>
        /// <returns></returns>
        private List<DT_R04_EX> SetDT_R04ExEntityListFromForm()
        {
            LogUtility.DebugMethodStart();
            List<DT_R04_EX> lstNewDT_R04Ex = new List<DT_R04_EX>();

            for (int i = 0; i < this.form.cdgv_LastSBNbasyo_yotei.Rows.Count; i++)
            {
                //行の有効チェック
                bool bValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_LastSBNbasyo_yotei.Rows[i]);
                if (!bValidRow) break;
                DT_R04_EX dt_r04Ex = new DT_R04_EX();
                //レコード枝番号
                dt_r04Ex.REC_SEQ = SqlDecimal.Parse((lstNewDT_R04Ex.Count + 1).ToString());
                //最終処分業者CD
                object tmpobj = this.form.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_GYOUSHA_CD"].Value;
                if (tmpobj != null)
                {
                    dt_r04Ex.LAST_SBN_GYOUSHA_CD = tmpobj.ToString();
                }
                //最終処分事業場CD
                tmpobj = this.form.cdgv_LastSBNbasyo_yotei.Rows[i].Cells["LAST_SBN_JOU_CD"].Value;
                if (tmpobj != null)
                {
                    dt_r04Ex.LAST_SBN_GENBA_CD = tmpobj.ToString();
                }
                //リストに追加
                lstNewDT_R04Ex.Add(dt_r04Ex);
            }
            LogUtility.DebugMethodEnd();
            return lstNewDT_R04Ex;
        }

        /// <summary>
        /// 電子マニフェスト収集運搬拡張[DT_R19_EX]データ作成処理
        /// </summary>
        /// <param name="Newdt_r18Ex"></param>
        /// <returns></returns>
        private List<DT_R19_EX> CreateDT_R19ExEntityList(DT_R18_EX Newdt_r18Ex)
        {
            if (Newdt_r18Ex == null) { return null; }
            LogUtility.DebugMethodStart(Newdt_r18Ex);

            //画面から、運搬情報拡張データ取得
            List<DT_R19_EX> lstNewDT_R19Ex = SetDT_R19ExEntityListFromForm();
            foreach (DT_R19_EX dt_r19Ex in lstNewDT_R19Ex)
            {
                dt_r19Ex.SYSTEM_ID = Newdt_r18Ex.SYSTEM_ID;         //最新データのシステムID
                dt_r19Ex.SEQ = Newdt_r18Ex.SEQ;                    //最新データのSEQ
                dt_r19Ex.MANIFEST_ID = Newdt_r18Ex.MANIFEST_ID;     //最新データの交付番号
                dt_r19Ex.KANRI_ID = Newdt_r18Ex.KANRI_ID;           //最新データの管理番号

            }

            LogUtility.DebugMethodEnd(Newdt_r18Ex);

            return lstNewDT_R19Ex;
        }
        /// <summary>
        /// 収集運搬拡張[DT_R19_EX]データ作成処理
        /// </summary>
        /// <returns></returns>
        private List<DT_R19_EX> SetDT_R19ExEntityListFromForm()
        {
            LogUtility.DebugMethodStart();
            List<DT_R19_EX> lstNewDT_R19Ex = new List<DT_R19_EX>();
            for (int i = 0; i < this.form.cdgv_UnpanInfo.Rows.Count; i++)
            {
                if (this.form.cdgv_UnpanInfo.Rows[i].IsNewRow) continue;

                //行の有効チェック
                bool bValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_UnpanInfo.Rows[i], true);
                if (!bValidRow) break;
                DT_R19_EX dt_r19Ex = new DT_R19_EX();
                object tmp = null;
                //運搬区間番号
                dt_r19Ex.UPN_ROUTE_NO = SqlDecimal.Parse((lstNewDT_R19Ex.Count + 1).ToString());
                //画面でデータ取得する
                //収集運搬業者CD
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_CD"].Value;
                if (tmp != null)
                {
                    dt_r19Ex.UPN_GYOUSHA_CD = tmp.ToString();
                }
                //報告不要収集運搬業者加入者番号
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_EDI_MEMBER_ID"].Value;
                if (tmp != null)
                {
                    //報告不要フラグ取得処理
                    dt_r19Ex.NO_REP_UPN_EDI_MEMBER_ID = this.GetNO_REP_FLG(tmp.ToString()) ? tmp.ToString() : null;
                }
                //運搬先業者CD
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GYOUSHA_CD"].Value;
                if (tmp != null)
                {
                    dt_r19Ex.UPNSAKI_GYOUSHA_CD = tmp.ToString();
                }
                //運搬先業者加入者番号
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["Unpansaki_KanyushaCD"].Value;
                if (tmp != null)
                {
                    //報告不要フラグ取得処理
                    dt_r19Ex.NO_REP_UPNSAKI_EDI_MEMBER_ID = this.GetNO_REP_FLG(tmp.ToString()) ? tmp.ToString() : null;
                }
                //運搬先事業場CD
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["UNPANSAKI_GENBA_CD"].Value;
                if (tmp != null)
                {
                    dt_r19Ex.UPNSAKI_GENBA_CD = tmp.ToString();
                }
                //運搬担当者CD
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["UNPANTAN_CD"].Value;
                if (tmp != null)
                {
                    dt_r19Ex.UPN_TANTOUSHA_CD = tmp.ToString();
                }

                //運搬車輌CD
                tmp = this.form.cdgv_UnpanInfo.Rows[i].Cells["SHARYOU_CD"].Value;
                if (tmp != null)
                {
                    dt_r19Ex.SHARYOU_CD = tmp.ToString();
                }
                //手動の場合は、運搬報告記載の運搬担当者、運搬報告記載の車輌CD、運搬報告記載の報告担当者CDを設定する
                if (this.form.cntxt_InputKBN.Text == "2")//手動
                {
                    tmp = this.form.cdgv_UnpanInfo.Rows[i].Tag;
                    if (tmp != null)
                    {
                        //運搬報告記載の運搬担当者
                        if (!string.IsNullOrEmpty((tmp as UnpanHoukokuDataDTOCls).cantxt_UnpanTantoushaCd))
                        {
                            dt_r19Ex.UPNREP_UPN_TANTOUSHA_CD = (tmp as UnpanHoukokuDataDTOCls).cantxt_UnpanTantoushaCd;
                        }
                        //運搬報告記載の車輌CD
                        if (!string.IsNullOrEmpty((tmp as UnpanHoukokuDataDTOCls).cantxt_SyaryoNo))
                        {
                            dt_r19Ex.UPNREP_SHARYOU_CD = (tmp as UnpanHoukokuDataDTOCls).cantxt_SyaryoNo;
                        }
                        //運搬報告記載の報告担当者CD
                        if (!string.IsNullOrEmpty((tmp as UnpanHoukokuDataDTOCls).cantxt_HoukokuTantoushaCD))
                        {
                            dt_r19Ex.HOUKOKU_TANTOUSHA_CD = (tmp as UnpanHoukokuDataDTOCls).cantxt_HoukokuTantoushaCD;
                        }
                    }
                }
                //リストに追加
                lstNewDT_R19Ex.Add(dt_r19Ex);
            }

            // 処分事業場CDが空の時は処分事業場内のデータを運搬情報の最後に登録する為、
            // 収集運搬拡張[DT_R19_EX]データに空データを作成する
            if (this.isMakeEmptyData_R19_EX)
            {
                DT_R19_EX dt_r19Ex = new DT_R19_EX();
                //運搬区間番号
                dt_r19Ex.UPN_ROUTE_NO = SqlDecimal.Parse((lstNewDT_R19Ex.Count + 1).ToString());

                //リストに追加
                lstNewDT_R19Ex.Add(dt_r19Ex);
            }

            LogUtility.DebugMethodEnd();
            return lstNewDT_R19Ex;
        }

        /// <summary>
        /// DT_R18EXデータEntityの作成処理
        /// </summary>
        /// <param name="dt_r18">対象データ</param>
        /// <param name="dt_r18ExOld">更新前のデータ</param>
        /// <returns></returns>
        private DT_R18_EX CreateDT_R18ExEntity(bool useTran, DT_R18 dt_r18, DT_R18_EX dt_r18ExOld = null, int rowCnt = 0, bool haikibutuFlg = false)
        {
            if (dt_r18 == null) { return null; }

            LogUtility.DebugMethodStart(useTran, dt_r18, dt_r18ExOld, rowCnt, haikibutuFlg);
            DT_R18_EX dt_r18Ex = new DT_R18_EX();
            //基本拡張データが新規場合
            if (dt_r18ExOld == null)
            {
                //画面から基本拡張データの設定
                dt_r18Ex = SetDT_R18ExDataFromForm(dt_r18, rowCnt, haikibutuFlg);
                //システムIDの採番
                Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                if (useTran)
                {
                    dt_r18Ex.SYSTEM_ID = dba.createSystemIdWithTableLock((int)DENSHU_KBN.DENSHI_MANIFEST);
                }
                else
                {
                    dt_r18Ex.SYSTEM_ID = dba.createSystemIdWithTableLockNoTransaction((int)DENSHU_KBN.DENSHI_MANIFEST);
                }
                dt_r18Ex.SEQ = 1;//新規場合1から
            }
            //更新場合
            else
            {
                //画面から基本拡張データの設定
                dt_r18Ex = SetDT_R18ExDataFromForm(dt_r18, rowCnt, haikibutuFlg);
                dt_r18Ex.SYSTEM_ID = dt_r18ExOld.SYSTEM_ID;
                dt_r18Ex.SEQ = dt_r18ExOld.SEQ + 1;
            }

            dt_r18Ex.DELETE_FLG = false;
            dt_r18Ex.KANRI_ID = dt_r18.KANRI_ID;
            dt_r18Ex.MANIFEST_ID = dt_r18.MANIFEST_ID;

            LogUtility.DebugMethodEnd(useTran, dt_r18, dt_r18ExOld, rowCnt, haikibutuFlg);

            return dt_r18Ex;
        }
        /// <summary>
        /// 画面から電子マニフェスト基本拡張DT_R18EXデータ設定
        /// </summary>
        /// <param name="dt_r18"></param>
        /// <param name="rowCnt"></param>
        /// <param name="haikibutuFlg"></param>
        /// <returns></returns>
        private DT_R18_EX SetDT_R18ExDataFromForm(DT_R18 dt_r18, int rowCnt, bool haikibutuFlg)
        {
            LogUtility.DebugMethodStart(dt_r18, rowCnt, haikibutuFlg);
            DT_R18_EX dt_r18Ex = new DT_R18_EX();

            //排出事業者CDが画面から取得する
            dt_r18Ex.HST_GYOUSHA_CD = string.IsNullOrEmpty(this.form.cantxt_HaisyutuGyousyaCd.Text) ? null : this.form.cantxt_HaisyutuGyousyaCd.Text;
            //排出事業場CDが画面から取得する
            dt_r18Ex.HST_GENBA_CD = string.IsNullOrEmpty(this.form.cantxt_HaisyutuGenbaCd.Text) ? null : this.form.cantxt_HaisyutuGenbaCd.Text;
            //処分受託者CDが画面から取得する
            dt_r18Ex.SBN_GYOUSHA_CD = string.IsNullOrEmpty(this.form.cantxt_SBN_JyutakuShaCD.Text) ? null : this.form.cantxt_SBN_JyutakuShaCD.Text;
            //処分事業場CDが画面から取得する
            dt_r18Ex.SBN_GENBA_CD = string.IsNullOrEmpty(this.form.cantxt_SBN_Genba_CD.Text) ? null : this.form.cantxt_SBN_Genba_CD.Text;
            //報告不要処分事業者加入者番号
            if (!string.IsNullOrEmpty(this.form.ctxt_SBN_KanyuShaNo.Text))
            {
                //報告不要フラグ取得処理
                dt_r18Ex.NO_REP_SBN_EDI_MEMBER_ID = this.GetNO_REP_FLG(this.form.ctxt_SBN_KanyuShaNo.Text) ? this.form.ctxt_SBN_KanyuShaNo.Text : null;
            }

            //処分受託者許可番号が画面から取得する
            //dt_r18Ex.SBN_KYOKA_NO = this.form.ctxt_SBN_KyokaNo.Text;
            //廃棄物名称CDが画面から取得する
            object tmpObj = null;
            if (!haikibutuFlg)
            {
                tmpObj = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_NAME_CD"].Value;
            }

            if (tmpObj != null)
            {
                dt_r18Ex.HAIKI_NAME_CD = tmpObj.ToString();
            }
            //将軍処分方法CDが画面から取得する
            dt_r18Ex.SBN_HOUHOU_CD = string.IsNullOrEmpty(this.form.cantxt_Shogun_SBN_houhouCD.Text) ? null : this.form.cantxt_Shogun_SBN_houhouCD.Text;
            //報告担当者CDが画面から取得する
            dt_r18Ex.HOUKOKU_TANTOUSHA_CD = string.IsNullOrEmpty(this.form.cantxt_SBN_HoukokuTantouShaCD.Text) ? null : this.form.cantxt_SBN_HoukokuTantouShaCD.Text;
            //処分担当者CDが画面から取得する
            dt_r18Ex.SBN_TANTOUSHA_CD = string.IsNullOrEmpty(this.form.cantxt_SBN_SBNTantouShaCD.Text) ? null : this.form.cantxt_SBN_SBNTantouShaCD.Text;
            //運搬担当者CDが画面から取得する
            dt_r18Ex.UPN_TANTOUSHA_CD = string.IsNullOrEmpty(this.form.cantxt_SBN_UnpanTantouShaCD.Text) ? null : this.form.cantxt_SBN_UnpanTantouShaCD.Text;
            //車輌CDが画面から取得する
            dt_r18Ex.SHARYOU_CD = string.IsNullOrEmpty(this.form.cantxt_SBN_SyaryoNoCD.Text) ? null : this.form.cantxt_SBN_SyaryoNoCD.Text;
            // 換算値を画面から取得
            if (this.form.cdgv_Haikibutu.Rows.Count != 0)
            {
                var kansanSuu = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["KANSAN_SUU"].Value;
                if (kansanSuu != null && !String.IsNullOrEmpty(kansanSuu.ToString()))
                {
                    dt_r18Ex.KANSAN_SUU = SqlDecimal.Parse(kansanSuu.ToString());
                }
            }

            //減容後数量[換算値計算できる前提で]
            if (this.maniFlag == 1)
            {
                if (!haikibutuFlg && dt_r18Ex.KANSAN_SUU != 0)
                {
                    SqlDecimal genyougou_suu = 0;
                    bool catchErr = false;
                    var refGenyogou = this.GetGenYougou_suu(dt_r18Ex.KANSAN_SUU, ref genyougou_suu, rowCnt, out catchErr);
                    if (catchErr)
                    {
                        return null;
                    }

                    if (refGenyogou)
                    {
                        ManifestoLogic maniLogic = new ManifestoLogic();
                        dt_r18Ex.GENNYOU_SUU = maniLogic.Round((decimal)genyougou_suu, SystemProperty.Format.ManifestSuuryou);
                    }
                }
            }

            LogUtility.DebugMethodEnd(dt_r18, rowCnt, haikibutuFlg);
            return dt_r18Ex;
        }

        /// <summary>
        /// 換算後数量の計算処理
        /// </summary>
        /// <param name="Kansan_suu">out 換算後数量</param>
        /// <returns>換算値取得成功フラグ</returns>
        public bool GetKansan_suu(ref SqlDecimal Kansan_suu)
        {
            //換算値取得成功フラグ
            bool bIsSuccess = false;

            int rowIndex = this.form.cdgv_Haikibutu.CurrentCell.RowIndex;

            //数量確定者コード
            string SUU_KAKUTEI_CODE = string.Empty;
            //廃棄物の確定数量
            SqlDecimal HAIKI_KAKUTEI_SUU = 0;
            //廃棄物の確定数量の単位
            string HAIKI_KAKUTEI_UNIT_CODE = string.Empty;

            // 必要な数量が空であるか判断するフラグ
            bool isEmptySuuryou = true;
            // 廃棄物数量、廃棄物単位の一次格納用
            object tmpHaikiSuu = null;
            object tmpUnpUnit = null;

            // システム設定から帳票数量区分を取得
            string reportSuuKbn = mSysInfo.MANIFEST_REPORT_SUU_KBN.ToString();
            switch (reportSuuKbn)
            {
                case "1":
                    // 1.確定数量
                    // 数量確定者CDにより、どの数量を使うかを判定し換算値計算
                    object tmpObjHaiki = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["SUU_KAKUTEI_CODE"].Value;
                    if (tmpObjHaiki != null
                        && !string.IsNullOrEmpty(tmpObjHaiki.ToString()))
                    {
                        SUU_KAKUTEI_CODE = tmpObjHaiki.ToString();
                    }
                    else
                    {
                        SUU_KAKUTEI_CODE = "01";
                    }

                    // 数量確定者が排出事業者の場合
                    if (SUU_KAKUTEI_CODE == "01")
                    {
                        tmpHaikiSuu = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SUU"].Value;
                        tmpUnpUnit = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["UNIT_CD"].Value;
                        if (tmpHaikiSuu != null)
                        {
                            //確定数量
                            HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(tmpHaikiSuu.ToString().Replace(",", ""));
                            isEmptySuuryou = false;
                        }
                        if (tmpUnpUnit != null)
                        {
                            //確定数量の単位
                            HAIKI_KAKUTEI_UNIT_CODE = tmpUnpUnit.ToString();
                        }
                    }
                    //数量確定者が処分事業者の場合
                    else if (SUU_KAKUTEI_CODE == "02")
                    {
                        if (!string.IsNullOrEmpty(this.form.cntxt_Jyunyuryo.Text))
                        {
                            //確定数量
                            HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(this.form.cntxt_Jyunyuryo.Text.Replace(",", ""));
                            //確定数量の単位
                            HAIKI_KAKUTEI_UNIT_CODE = this.form.cantxt_JyunyuryoUnitCD.Text;
                            isEmptySuuryou = false;
                        }
                    }
                    //確定者が運搬業者の場合
                    else
                    {
                        //運搬情報の中にごみデータ絞込み
                        this.form.cdgv_UnpanInfo.AllowUserToAddRows = false;
                        for (int i = 0; i < this.form.cdgv_UnpanInfo.Rows.Count; i++)
                        {   //行の有効フラグ
                            bool bIsValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_UnpanInfo.Rows[i], true);
                            if (!bIsValidRow)
                            {
                                //無効行を削除
                                this.form.cdgv_UnpanInfo.Rows.RemoveAt(i);
                            }
                        }
                        //確定者CD(3～7で区間を判断する)
                        int nConfirmCd = int.Parse(SUU_KAKUTEI_CODE);
                        for (int i = 0; i < 5; i++)
                        {
                            if (this.form.cdgv_UnpanInfo.Rows.Count > i)
                            {
                                if (i == nConfirmCd - 3)
                                {
                                    //廃棄物の確定数量
                                    SqlDecimal Upn_suu = (this.form.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cntxt_UnpanRyo;
                                    if (!Upn_suu.IsNull && Upn_suu != 0)
                                    {
                                        HAIKI_KAKUTEI_SUU = Upn_suu;
                                        isEmptySuuryou = false;
                                    }
                                    else
                                    {
                                        if (this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SUU"].Value != null)
                                        {
                                            HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SUU"].Value.ToString());
                                        }
                                        this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();
                                    }
                                    string temobj = (this.form.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cantxt_UnpanRyoUnitCd;
                                    if (!string.IsNullOrEmpty(temobj))
                                    {
                                        //廃棄物の確定数量の単位コード
                                        HAIKI_KAKUTEI_UNIT_CODE = temobj;
                                    }
                                    else
                                    {
                                        if (this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["UNIT_CD"].Value != null)
                                        {
                                            HAIKI_KAKUTEI_UNIT_CODE = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["UNIT_CD"].Value.ToString();
                                        }
                                        this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();
                            }
                        }
                        this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();
                    }
                    break;

                case "2":
                    // 2.排出事業者
                    tmpHaikiSuu = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SUU"].Value;
                    tmpUnpUnit = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["UNIT_CD"].Value;
                    if (tmpHaikiSuu != null)
                    {
                        HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(tmpHaikiSuu.ToString());
                        isEmptySuuryou = false;
                    }
                    if (tmpUnpUnit != null)
                    {
                        HAIKI_KAKUTEI_UNIT_CODE = tmpUnpUnit.ToString();
                    }
                    break;

                case "3":
                    // 3.収集運搬業者
                    //運搬情報の中にごみデータ絞込み
                    this.form.cdgv_UnpanInfo.AllowUserToAddRows = false;
                    for (int i = 0; i < this.form.cdgv_UnpanInfo.Rows.Count; i++)
                    {   //行の有効フラグ
                        bool bIsValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_UnpanInfo.Rows[i], true);
                        if (!bIsValidRow)
                        {
                            //無効行を削除
                            this.form.cdgv_UnpanInfo.Rows.RemoveAt(i);
                        }
                    }
                    // 運搬情報が0件の場合はスキップ
                    if (this.form.cdgv_UnpanInfo.Rows.Count == 0)
                    {
                        this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();
                        break;
                    }

                    var tmpDto = new SearchMasterDataDTOCls();

                    // 複数区間が存在する場合、最後の区間の数量を使用
                    // 最終区間から順に加入者情報が存在するかチェック
                    for (int i = this.form.cdgv_UnpanInfo.Rows.Count - 1; i >= 0; i--)
                    {
                        // 加入者情報
                        if (this.form.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_EDI_MEMBER_ID"].Value == null)
                        {
                            // 加入者番号が空の場合はスキップ
                            continue;
                        }
                        tmpDto.EDI_MEMBER_ID = this.form.cdgv_UnpanInfo.Rows[i].Cells["UPN_SHA_EDI_MEMBER_ID"].Value.ToString();
                        var table = this.QUE_INFODao.GetMS_JWNET_MEMBERInfo(tmpDto);
                        if (table == null || table.Rows.Count == 0)
                        {
                            // 加入者情報が存在しない場合はスキップ
                            continue;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(table.Rows[0]["EDI_PASSWORD"].ToString()))
                            {
                                // 運搬量、単位を設定
                                SqlDecimal Upn_suu = (this.form.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cntxt_UnpanRyo;
                                string upnUnit = (this.form.cdgv_UnpanInfo.Rows[i].Tag as UnpanHoukokuDataDTOCls).cantxt_UnpanRyoUnitCd;
                                if (!Upn_suu.IsNull && Upn_suu != 0)
                                {
                                    HAIKI_KAKUTEI_SUU = Upn_suu;
                                    HAIKI_KAKUTEI_UNIT_CODE = upnUnit;
                                    isEmptySuuryou = false;
                                }
                                break;
                            }
                        }
                    }
                    this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();
                    break;

                case "4":
                    // 4.処分事業者
                    if (!string.IsNullOrEmpty(this.form.cntxt_Jyunyuryo.Text))
                    {
                        // 受入量を設定
                        HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(this.form.cntxt_Jyunyuryo.Text.Replace(",", ""));
                        // 受入量の単位を設定
                        HAIKI_KAKUTEI_UNIT_CODE = this.form.cantxt_JyunyuryoUnitCD.Text;
                        isEmptySuuryou = false;
                    }
                    break;
            }
            // 必要な数量が空の場合、産業廃棄物の数量を参照
            if (isEmptySuuryou)
            {
                tmpHaikiSuu = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SUU"].Value;
                tmpUnpUnit = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["UNIT_CD"].Value;
                if (tmpHaikiSuu != null)
                {
                    HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(tmpHaikiSuu.ToString());
                }
                if (tmpUnpUnit != null)
                {
                    HAIKI_KAKUTEI_UNIT_CODE = tmpUnpUnit.ToString();
                }
            }

            //換算後数量の計算
            if (!HAIKI_KAKUTEI_SUU.IsNull)
            {
                //換算後数量の計算を行う
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                //廃棄物種類CDが画面から取得する
                object tmpObj = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value;
                if (tmpObj != null)
                {
                    //前頭4桁が廃棄物種類CD取得
                    dto.HAIKI_SHURUI_CD = tmpObj.ToString().Substring(0, 4);
                }
                else
                {
                    dto.HAIKI_SHURUI_CD = string.Empty;
                }
                //画面から取得の廃棄物名称CD
                tmpObj = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["HAIKI_NAME_CD"].Value;
                if (tmpObj != null)
                {
                    dto.HAIKI_NAME_CD = tmpObj.ToString();
                }
                else
                {
                    dto.HAIKI_NAME_CD = string.Empty;
                }
                //確定数量の単位CD
                dto.UNIT_CD = HAIKI_KAKUTEI_UNIT_CODE;

                //荷姿CD
                tmpObj = this.form.cdgv_Haikibutu.Rows[rowIndex].Cells["NISUGATA_CD"].Value;
                if (tmpObj != null)
                {
                    dto.NISUGATA_CD = tmpObj.ToString();
                }
                else
                {
                    dto.NISUGATA_CD = string.Empty;
                }
                //換算式と換算値取得
                DataTable tbl = new DenshiMasterDataLogic().GetDenmaniKansanData(dto);
                if (tbl.Rows.Count == 1)
                {   //換算式の取得
                    if (tbl.Rows[0]["KANSANCHI"] != null)
                    {
                        string val = tbl.Rows[0]["KANSANCHI"].ToString();
                        //乗算式
                        if (tbl.Rows[0]["KANSANSHIKI"].ToString() == "0")
                        {
                            Kansan_suu = SqlDecimal.Multiply(HAIKI_KAKUTEI_SUU, SqlDecimal.Parse(val));
                        }
                        //除算式
                        else
                        {
                            Kansan_suu = SqlDecimal.Divide(HAIKI_KAKUTEI_SUU, SqlDecimal.Parse(val));
                        }
                        //取得成功フラグを設定
                        bIsSuccess = true;
                    }
                }
            }

            return bIsSuccess;
        }

        /// <summary>
        /// 減容後数量の計算処理
        /// </summary>
        /// <param name="kansan_suu"></param>
        /// <param name="genyou_suu"></param>
        /// <param name="rowCnt"></param>
        /// <returns>計算成功フラグ</returns>
        public bool GetGenYougou_suu(SqlDecimal kansan_suu, ref SqlDecimal genyou_suu, int rowCnt, out bool catchErr)
        {
            catchErr = false;
            try
            {
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう start
                //bool bIsSuccess = false;
                bool bIsSuccess = true;
                genyou_suu = kansan_suu;
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう end
                if (kansan_suu != 0)
                {
                    //減容率の取得
                    SqlDecimal GENNYOURITSU = 0;
                    SearchMasterDataDTOCls dto = new SearchMasterDataDTOCls();
                    //廃棄物種類CDが画面から取得する
                    object tmpObj = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_SHURUI_CD"].Value;
                    if (tmpObj != null)
                    {
                        //前頭4桁が廃棄物種類CD取得
                        dto.HAIKI_SHURUI_CD = tmpObj.ToString().Substring(0, 4);
                    }
                    else
                    {
                        return bIsSuccess;
                    }
                    //画面から取得の廃棄物名称CD
                    tmpObj = this.form.cdgv_Haikibutu.Rows[rowCnt].Cells["HAIKI_NAME_CD"].Value;
                    if (tmpObj != null)
                    {
                        dto.HAIKI_NAME_CD = tmpObj.ToString();
                    }
                    //画面から処分方法CD取得する
                    if (!string.IsNullOrEmpty(this.form.cantxt_Shogun_SBN_houhouCD.Text))
                    {
                        dto.SHOBUN_HOUHOU_CD = this.form.cantxt_Shogun_SBN_houhouCD.Text;
                    }

                    DataTable tbl = new DataTable();

                    // 報告書分類＋廃棄物名称＋処分方法＋減容率 で検索
                    if (!string.IsNullOrEmpty(dto.HAIKI_SHURUI_CD)
                        && !string.IsNullOrEmpty(dto.HAIKI_NAME_CD)
                        && !string.IsNullOrEmpty(dto.SHOBUN_HOUHOU_CD))
                    {
                        tbl = DT_R18Dao.GetGenYourituData(dto);
                    }

                    if (tbl.Rows.Count < 1)
                    {
                        // 報告書分類＋処分方法＋減容率
                        if (!string.IsNullOrEmpty(dto.HAIKI_SHURUI_CD)
                            && !string.IsNullOrEmpty(dto.SHOBUN_HOUHOU_CD))
                        {
                            dto.HAIKI_NAME_CD = string.Empty;
                            tbl = DT_R18Dao.GetGenYourituData(dto);
                        }
                    }

                    if (tbl.Rows.Count < 1)
                    {
                        // 報告書分類＋減容率
                        if (!string.IsNullOrEmpty(dto.HAIKI_SHURUI_CD))
                        {
                            dto.HAIKI_NAME_CD = string.Empty;
                            dto.SHOBUN_HOUHOU_CD = string.Empty;
                            tbl = DT_R18Dao.GetGenYourituData(dto);
                        }
                    }

                    if (tbl.Rows.Count == 1)
                    {   //減容率の取得
                        if (tbl.Rows[0]["GENNYOURITSU"] != null)
                        {
                            try
                            {
                                GENNYOURITSU = SqlDecimal.Parse(tbl.Rows[0]["GENNYOURITSU"].ToString());
                                genyou_suu = SqlDecimal.Divide(SqlDecimal.Multiply(kansan_suu, 100 - GENNYOURITSU), 100.00m);
                                bIsSuccess = true;
                            }
                            catch
                            {
                                return bIsSuccess;
                            }
                        }
                        else
                        {
                            return bIsSuccess;
                        }
                    }
                    else
                    {
                        return bIsSuccess;
                    }
                }

                return bIsSuccess;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenYougou_suu", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenYougou_suu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }

        /// <summary>
        /// 一次マニフェスト情報拡張作成処理
        /// </summary>
        /// <param name="Newdt_r18Ex"></param>
        /// <returns></returns>
        private List<DT_R08_EX> CreateDT_R08ExEntityList(DT_R18_EX Newdt_r18Ex)
        {
            LogUtility.DebugMethodStart(Newdt_r18Ex);

            //画面から、運搬情報拡張データ取得
            List<DT_R08_EX> lstNewDT_R08Ex = SetDT_R08ExEntityListFromForm();
            foreach (DT_R08_EX dt_r08Ex in lstNewDT_R08Ex)
            {
                dt_r08Ex.SYSTEM_ID = Newdt_r18Ex.SYSTEM_ID;         //最新データのシステムID
                dt_r08Ex.SEQ = Newdt_r18Ex.SEQ;                     //最新データのSEQ
                dt_r08Ex.MANIFEST_ID = Newdt_r18Ex.MANIFEST_ID;     //最新データの交付番号
                dt_r08Ex.KANRI_ID = Newdt_r18Ex.KANRI_ID;           //最新データの管理番号

            }

            LogUtility.DebugMethodEnd(Newdt_r18Ex);

            return lstNewDT_R08Ex;
        }
        /// <summary>
        /// 画面から一次マニフェスト情報拡張取得処理
        /// </summary>
        /// <returns></returns>
        private List<DT_R08_EX> SetDT_R08ExEntityListFromForm()
        {
            LogUtility.DebugMethodStart();
            List<DT_R08_EX> lstNewDT_R08Ex = new List<DT_R08_EX>();
            for (int i = 0; i < this.form.cdgv_Tyukanshori.Rows.Count; i++)
            {
                //行の有効チェック
                bool bValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_Tyukanshori.Rows[i]);
                if (!bValidRow) break;
                DT_R08_EX dt_r08Ex = new DT_R08_EX();
                //レコード枝番号
                dt_r08Ex.REC_SEQ = SqlDecimal.Parse((lstNewDT_R08Ex.Count + 1).ToString());
                //排出事業者CD
                object tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GYOUSHA_CD"].Value;
                if (tmpObj != null)
                {
                    dt_r08Ex.HST_GYOUSHA_CD = tmpObj.ToString();
                }
                //排出事業場CD
                tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["FM_HST_GENBA_CD"].Value;
                if (tmpObj != null)
                {
                    dt_r08Ex.HST_GENBA_CD = tmpObj.ToString();
                }
                //廃棄物種類CD
                tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["FM_HAIKI_SHURUI_CD"].Value;
                if (tmpObj != null)
                {
                    dt_r08Ex.HAIKI_SHURUI_CD = tmpObj.ToString();
                }

                lstNewDT_R08Ex.Add(dt_r08Ex);
            }

            LogUtility.DebugMethodEnd();
            return lstNewDT_R08Ex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<DT_R18_EX> CreateNew_DT_R18_EXEntityList()
        {
            LogUtility.DebugMethodStart();

            Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();

            //画面から、データ取得
            List<DT_R18_EX> lstNew_DT_R18_EX = new List<DT_R18_EX>();

            for (int i = 0; i < this.form.cdgv_Tyukanshori.Rows.Count; i++)
            {
                //行の有効チェック
                bool bValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_Tyukanshori.Rows[i]);
                if (!bValidRow) break;

                object tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_MEDIA_TYPE"].Value;

                if (tmpObj != null && tmpObj.ToString() == "4" &&
                    string.Empty.Equals(this.GetDbValue(this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_SYSTEM_ID"].Value)))
                {
                    DT_R18_EX dt_r18_ex = new DT_R18_EX();
                    //システムIDの採番
                    dt_r18_ex.SYSTEM_ID = dba.createSystemId((int)DENSHU_KBN.DENSHI_MANIFEST);
                    dt_r18_ex.SEQ = 1;//新規場合1から

                    //紐付する1次マニフェストのKANRI_IDをセット 
                    tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_KANRI_ID"].Value;
                    if (tmpObj != null && tmpObj.ToString() != "")
                    {
                        dt_r18_ex.KANRI_ID = tmpObj.ToString();
                    }

                    //紐付する1次マニフェストのマニ番号をセット
                    tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_R18_MANIFEST_ID"].Value;
                    if (tmpObj != null && tmpObj.ToString() != "")
                    {
                        dt_r18_ex.MANIFEST_ID = tmpObj.ToString();
                    }

                    lstNew_DT_R18_EX.Add(dt_r18_ex);

                    //システムIDを設定する
                    this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_SYSTEM_ID"].Value = dt_r18_ex.SYSTEM_ID;
                }
            }

            LogUtility.DebugMethodEnd();

            return lstNew_DT_R18_EX;

        }
        /// <summary>
        /// 画面から一次マニフェスト情報拡張取得処理
        /// </summary>
        /// <returns></returns>
        private List<T_MANIFEST_RELATION> SetT_Manifest_RelationEntityListFrom()
        {
            LogUtility.DebugMethodStart();
            List<T_MANIFEST_RELATION> lstNewT_Manifest_Relation = new List<T_MANIFEST_RELATION>();
            for (int i = 0; i < this.form.cdgv_Tyukanshori.Rows.Count; i++)
            {
                //行の有効チェック
                bool bValidRow = this.form.IsValidRowOfEveryInfo(this.form.cdgv_Tyukanshori.Rows[i]);
                if (!bValidRow) break;
                T_MANIFEST_RELATION T_Manifest_Relation = new T_MANIFEST_RELATION();
                //レコード枝番号
                T_Manifest_Relation.REC_SEQ = SqlInt32.Parse((lstNewT_Manifest_Relation.Count + 1).ToString());
                //一次システムID
                object tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_SYSTEM_ID"].Value;
                if (tmpObj != null && tmpObj.ToString() != "")
                {
                    T_Manifest_Relation.FIRST_SYSTEM_ID = SqlInt64.Parse(tmpObj.ToString());
                }
                //一次廃棄物区分CD
                tmpObj = this.form.cdgv_Tyukanshori.Rows[i].Cells["Hidden_FM_MEDIA_TYPE"].Value;
                if (tmpObj != null && tmpObj.ToString() != "")
                {
                    T_Manifest_Relation.FIRST_HAIKI_KBN_CD = SqlInt16.Parse(tmpObj.ToString());
                }

                lstNewT_Manifest_Relation.Add(T_Manifest_Relation);
            }

            LogUtility.DebugMethodEnd();
            return lstNewT_Manifest_Relation;
        }
        #endregion 電子拡張データ作成メソッド 完了

        #region DBからPattern呼ぶEntityの作成

        //DBからPattern呼ぶEntityの作成
        /// <summary>
        /// DBから電子マニパタン情報を取得処理
        /// </summary>
        /// <param name="system_ID"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public DenshiManifestPatternInfoCls GetManifestPatternInfoFromDB(string system_ID, string Seq)
        {
            if (string.IsNullOrEmpty(system_ID) || string.IsNullOrEmpty(Seq))
            {
                return null;
            }
            LogUtility.DebugMethodStart(system_ID, Seq);
            SqlInt64 sysId = SqlInt64.Parse(system_ID);
            SqlInt32 seq = SqlInt32.Parse(Seq);

            DenshiManifestPatternInfoCls ManiPtnInfo = new DenshiManifestPatternInfoCls();

            DT_PT_R02 dtoPt02 = new DT_PT_R02();
            DT_PT_R04 dtoPt04 = new DT_PT_R04();
            DT_PT_R05 dtoPt05 = new DT_PT_R05();
            DT_PT_R06 dtoPt06 = new DT_PT_R06();
            DT_PT_R13 dtoPt13 = new DT_PT_R13();
            DT_PT_R18 dtoPt18 = new DT_PT_R18();
            DT_PT_R19 dtoPt19 = new DT_PT_R19();
            DT_PT_R18_EX dtoPt18EX = new DT_PT_R18_EX();

            //電子マニフェストパターン有害物質
            dtoPt02.SYSTEM_ID = sysId;
            dtoPt02.SEQ = seq;
            DT_PT_R02[] dto02all = dtPtDao02.GetAllValidData(dtoPt02);

            //電子マニフェストパターン最終処分(予定)
            dtoPt04.SYSTEM_ID = sysId;
            dtoPt04.SEQ = seq;
            DT_PT_R04[] dto04all = dtPt04Dao.GetAllValidData(dtoPt04);

            //電子マニフェストパターン連絡番号
            dtoPt05.SYSTEM_ID = sysId;
            dtoPt05.SEQ = seq;
            DT_PT_R05[] dto05all = dtPt05Dao.GetAllValidData(dtoPt05);

            //電子マニフェストパターン備考
            dtoPt06.SYSTEM_ID = sysId;
            dtoPt06.SEQ = seq;
            DT_PT_R06[] dto06all = dtPt06Dao.GetAllValidData(dtoPt06);

            //電子マニフェストパターン最終処分
            dtoPt13.SYSTEM_ID = sysId;
            dtoPt13.SEQ = seq;
            DT_PT_R13[] dto13all = dtPt13Dao.GetAllValidData(dtoPt13);

            //電子マニフェストパターン
            dtoPt18.SYSTEM_ID = sysId;
            dtoPt18.SEQ = seq;
            DT_PT_R18 dtoPt18Datail = dtPt18Dao.GetDataForEntity(dtoPt18);

            //電子マニフェストパターン収集運搬
            dtoPt19.SYSTEM_ID = sysId;
            dtoPt19.SEQ = seq;
            DT_PT_R19[] dto19all = dtPt19Dao.GetAllValidData(dtoPt19);

            //電子マニフェストパターン拡張
            dtoPt18EX.SYSTEM_ID = sysId;
            dtoPt18EX.SEQ = seq;
            DT_PT_R18_EX[] dto18EXall = dtPt18EXDao.GetAllValidData(dtoPt18EX);

            //電子マニフェストパターン有害物質
            if (dto02all != null && dto02all.Length > 0)
            {
                for (int i = 0; i < dto02all.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R02.Add(dto02all[i]);
                }
            }
            //電子マニフェストパターン最終処分(予定)
            if (dto04all != null && dto04all.Length > 0)
            {
                for (int i = 0; i < dto04all.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R04.Add(dto04all[i]);
                }
            }

            //電子マニフェストパターン連絡番号
            if (dto05all != null && dto05all.Length > 0)
            {
                for (int i = 0; i < dto05all.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R05.Add(dto05all[i]);
                }
            }

            //電子マニフェストパターン備考
            if (dto06all != null && dto06all.Length > 0)
            {
                for (int i = 0; i < dto06all.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R06.Add(dto06all[i]);
                }
            }

            //電子マニフェストパターン最終処分
            if (dto13all != null && dto13all.Length > 0)
            {
                for (int i = 0; i < dto13all.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R13.Add(dto13all[i]);
                }
            }

            //電子マニフェストパターン
            if (dtoPt18Datail != null)
            {
                ManiPtnInfo.dt_PT_R18 = dtoPt18Datail;
            }

            //電子マニフェストパターン収集運搬
            if (dto19all != null && dto19all.Length > 0)
            {
                for (int i = 0; i < dto19all.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R19.Add(dto19all[i]);
                }
            }

            // 電子マニフェストパターン拡張
            if (dto18EXall != null && dto18EXall.Length > 0)
            {
                for (int i = 0; i < dto18EXall.Length; i++)
                {
                    ManiPtnInfo.lstDT_PT_R18_EX.Add(dto18EXall[i]);
                }
            }

            LogUtility.DebugMethodEnd(system_ID, Seq);

            return ManiPtnInfo;
        }
        /// <summary>
        /// マニ情報からパタン情報にコピー
        /// </summary>
        /// <param name="ManiInfo"></param>
        /// <returns></returns>
        public DenshiManifestPatternInfoCls CopyManiInfoToPtnInfo(DenshiManifestInfoCls ManiInfo)
        {
            DenshiManifestPatternInfoCls ManiPtnInfo = new DenshiManifestPatternInfoCls();
            //有害物質情報
            List<DT_R02> lstDT_R02 = ManiInfo.lstDT_R02;

            //最終処分事業場（予定）情報
            List<DT_R04> lstDT_R04 = ManiInfo.lstDT_R04;

            //電子最終処分(予定)拡張
            List<DT_R04_EX> lstDT_R04_EX = ManiInfo.lstDT_R04_EX;

            //連絡番号情報
            List<DT_R05> lstDT_R05 = ManiInfo.lstDT_R05;

            //備考情報
            List<DT_R06> lstDT_R06 = ManiInfo.lstDT_R06;

            //最終処分終了日・事業場情報
            List<DT_R13> lstDT_R13 = ManiInfo.lstDT_R13;

            //電子最終処分拡張
            List<DT_R13_EX> lstDT_R13_EX = ManiInfo.lstDT_R13_EX;

            //電子マニフェスト情報
            DT_R18 dt_r18 = ManiInfo.dt_r18;

            //電子基本拡張
            DT_R18_EX dt_r18ExOld = ManiInfo.dt_r18ExOld;

            //運搬情報
            List<DT_R19> lstDT_R19 = ManiInfo.lstDT_R19;

            //電子運搬拡張
            List<DT_R19_EX> lstDT_R19_EX = ManiInfo.lstDT_R19_EX;

            //有害物質
            if (lstDT_R02 != null && lstDT_R02.Count > 0)
            {
                for (int i = 0; i < lstDT_R02.Count; i++)
                {
                    DT_R02 dt_R02 = lstDT_R02[i];
                    DT_PT_R02 dt_PtR02 = SetDT_PT_R02(dt_R02);
                    ManiPtnInfo.lstDT_PT_R02.Add(dt_PtR02);
                }
            }

            //最終処分事業場
            if (lstDT_R04 != null && lstDT_R04.Count > 0 &&
                lstDT_R04_EX != null && lstDT_R04.Count == lstDT_R04_EX.Count)
            {
                for (int i = 0; i < lstDT_R04.Count; i++)
                {
                    DT_R04 dt_R04 = lstDT_R04[i];
                    DT_R04_EX dt_R04_EX = lstDT_R04_EX[i];
                    DT_PT_R04 dt_PtR04 = SetDT_PT_R04(dt_R04, dt_R04_EX);
                    ManiPtnInfo.lstDT_PT_R04.Add(dt_PtR04);
                }
            }

            //連絡番号
            if (lstDT_R05 != null && lstDT_R05.Count > 0)
            {
                for (int i = 0; i < lstDT_R05.Count; i++)
                {
                    DT_R05 dt_R05 = lstDT_R05[i];
                    DT_PT_R05 dt_PtR05 = SetDT_PT_R05(dt_R05);
                    ManiPtnInfo.lstDT_PT_R05.Add(dt_PtR05);
                }
            }

            //備考
            if (lstDT_R06 != null && lstDT_R06.Count > 0)
            {
                for (int i = 0; i < lstDT_R06.Count; i++)
                {
                    DT_R06 dt_R06 = lstDT_R06[i];
                    DT_PT_R06 dt_PtR06 = SetDT_PT_R06(dt_R06);
                    ManiPtnInfo.lstDT_PT_R06.Add(dt_PtR06);
                }
            }

            //最終処分
            if (lstDT_R13 != null && lstDT_R13.Count > 0
                && lstDT_R13_EX != null && lstDT_R13.Count == lstDT_R13_EX.Count)
            {
                for (int i = 0; i < lstDT_R13.Count; i++)
                {
                    DT_R13 dt_R13 = lstDT_R13[i];
                    DT_R13_EX dt_R13_EX = lstDT_R13_EX[i];
                    DT_PT_R13 dt_PtR13 = SetDT_PT_R13(dt_R13, dt_R13_EX);
                    ManiPtnInfo.lstDT_PT_R13.Add(dt_PtR13);
                }
            }
            //収集運搬
            if (lstDT_R19 != null && lstDT_R19.Count > 0
                && lstDT_R19_EX != null && lstDT_R19.Count == lstDT_R19_EX.Count)
            {
                for (int i = 0; i < lstDT_R19.Count; i++)
                {
                    DT_R19 dt_R19 = lstDT_R19[i];
                    DT_R19_EX dt_R19_EX = lstDT_R19_EX[i];
                    DT_PT_R19 dt_PtR19 = SetDT_PT_R19(dt_R19, dt_R19_EX);
                    ManiPtnInfo.lstDT_PT_R19.Add(dt_PtR19);
                }
            }
            //電子マニフェスト
            if (dt_r18 != null && dt_r18ExOld != null)
            {
                ManiPtnInfo.dt_PT_R18 = SetDT_PT_R18(dt_r18, dt_r18ExOld);
            }

            // 電子マニフェスト拡張と有害物質拡張
            for (int i = 0; i < this.form.cdgv_Haikibutu.Rows.Count; i++)
            {
                // 確定されていない行は含めない。
                if (this.form.cdgv_Haikibutu.Rows[i].IsNewRow)
                {
                    continue;
                }

                // 電子マニフェスト拡張
                DT_PT_R18_EX dt_PtR18EX = this.SetDT_PT_R18_EX(this.form.cdgv_Haikibutu.Rows[i].Cells);
                dt_PtR18EX.REC_SEQ = i + 1;
                ManiPtnInfo.lstDT_PT_R18_EX.Add(dt_PtR18EX);
            }

            return ManiPtnInfo;
        }
        ///有害物質情報→電子マニフェストパターン有害物質
        private DT_PT_R02 SetDT_PT_R02(DT_R02 dt_R02)
        {
            if (dt_R02 == null) return null;
            DT_PT_R02 dtPtr02 = new DT_PT_R02();
            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //管理番号(PK)
                dtPtr02.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr02.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //レコード連番(PK)
            dtPtr02.REC_SEQ = dt_R02.REC_SEQ;
            //マニフェスト／予約番号
            dtPtr02.MANIFEST_ID = dt_R02.MANIFEST_ID;
            //有害物質コード
            dtPtr02.YUUGAI_CODE = dt_R02.YUUGAI_CODE;
            //有害物質名
            dtPtr02.YUUGAI_NAME = dt_R02.YUUGAI_NAME;
            //レコード作成日時
            dtPtr02.CREATE_DATE = dt_R02.CREATE_DATE;
            //タイムスタンプ
            //dtPtr02.TIME_STAMP = dt_R02.UPDATE_TS;

            return dtPtr02;
        }

        ///最終処分事業場（予定）情報→電子マニフェストパターン最終処分(予定)
        private DT_PT_R04 SetDT_PT_R04(DT_R04 dt_R04, DT_R04_EX dt_R04_EX)
        {
            if (dt_R04 == null || dt_R04_EX == null) return null;

            DT_PT_R04 dtPtr04 = new DT_PT_R04();

            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //システムID(PK)
                dtPtr04.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr04.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //レコード連番(PK)
            dtPtr04.REC_SEQ = dt_R04.REC_SEQ;
            //マニフェスト／予約番号
            dtPtr04.MANIFEST_ID = dt_R04.MANIFEST_ID;
            //最終処分事業場名称
            dtPtr04.LAST_SBN_JOU_NAME = dt_R04.LAST_SBN_JOU_NAME;
            //最終処分事業場所在地郵便番号
            dtPtr04.LAST_SBN_JOU_POST = dt_R04.LAST_SBN_JOU_POST;
            //最終処分事業場所在地１
            dtPtr04.LAST_SBN_JOU_ADDRESS1 = dt_R04.LAST_SBN_JOU_ADDRESS1;
            //最終処分事業場所在地２
            dtPtr04.LAST_SBN_JOU_ADDRESS2 = dt_R04.LAST_SBN_JOU_ADDRESS2;
            //最終処分事業場所在地３
            dtPtr04.LAST_SBN_JOU_ADDRESS3 = dt_R04.LAST_SBN_JOU_ADDRESS3;
            //最終処分事業場所在地４
            dtPtr04.LAST_SBN_JOU_ADDRESS4 = dt_R04.LAST_SBN_JOU_ADDRESS4;
            //最終処分事業場電話番号
            dtPtr04.LAST_SBN_JOU_TEL = dt_R04.LAST_SBN_JOU_TEL;
            //最終処分業者CD(DT_R04項目なし)
            dtPtr04.LAST_SBN_GYOUSHA_CD = dt_R04_EX.LAST_SBN_GYOUSHA_CD;
            //最終処分事業場CD(DT_R04項目なし)
            dtPtr04.LAST_SBN_GENBA_CD = dt_R04_EX.LAST_SBN_GENBA_CD;
            //レコード作成日時
            dtPtr04.CREATE_DATE = dt_R04.CREATE_DATE;
            //タイムスタンプ
            //dtPtr04.TIME_STAMP = dt_R04.UPDATE_TS;

            return dtPtr04;
        }

        ///連絡番号→電子マニフェストパターン連絡番号
        private DT_PT_R05 SetDT_PT_R05(DT_R05 dt_R05)
        {
            if (dt_R05 == null) return null;
            DT_PT_R05 dtPtr05 = new DT_PT_R05();

            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //システムID(PK)
                dtPtr05.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr05.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //連絡番号No(PK)
            dtPtr05.RENRAKU_ID_NO = dt_R05.RENRAKU_ID_NO;
            //マニフェスト／予約番号
            dtPtr05.MANIFEST_ID = dt_R05.MANIFEST_ID;
            //連絡番号
            dtPtr05.RENRAKU_ID = dt_R05.RENRAKU_ID;
            //レコード作成日時
            dtPtr05.CREATE_DATE = dt_R05.CREATE_DATE;
            //タイムスタンプ
            //dtPtr05.TIME_STAMP = dt_R05.UPDATE_TS;

            return dtPtr05;
        }

        ///備考→電子マニフェストパターン備考
        private DT_PT_R06 SetDT_PT_R06(DT_R06 dt_R06)
        {
            if (dt_R06 == null) return null;

            DT_PT_R06 dtPtr06 = new DT_PT_R06();
            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //システムID(PK)
                dtPtr06.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr06.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //備考No(PK)
            dtPtr06.BIKOU_NO = dt_R06.BIKOU_NO;
            //マニフェスト／予約番号
            dtPtr06.MANIFEST_ID = dt_R06.MANIFEST_ID;
            //備考
            dtPtr06.BIKOU = dt_R06.BIKOU;
            //レコード作成日時
            dtPtr06.CREATE_DATE = dt_R06.CREATE_DATE;
            //タイムスタンプ
            //dtPtr06.TIME_STAMP = dt_R06.UPDATE_TS;
            return dtPtr06;
        }

        ///最終処分→電子マニフェストパターン最終処分
        private DT_PT_R13 SetDT_PT_R13(DT_R13 dt_R13, DT_R13_EX dt_R13_EX)
        {
            if (dt_R13 == null || dt_R13_EX == null) return null;

            DT_PT_R13 dtPtr13 = new DT_PT_R13();
            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //システムID(PK)
                dtPtr13.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr13.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //レコード連番(PK)
            dtPtr13.REC_SEQ = dt_R13.REC_SEQ;
            //マニフェスト／予約番号
            dtPtr13.MANIFEST_ID = dt_R13.MANIFEST_ID;
            //最終処分事業場名称
            dtPtr13.LAST_SBN_JOU_NAME = dt_R13.LAST_SBN_JOU_NAME;
            //最終処分事業場所在地の郵便番号
            dtPtr13.LAST_SBN_JOU_POST = dt_R13.LAST_SBN_JOU_POST;
            //最終処分事業場所在地１
            dtPtr13.LAST_SBN_JOU_ADDRESS1 = dt_R13.LAST_SBN_JOU_ADDRESS1;
            //最終処分事業場所在地２
            dtPtr13.LAST_SBN_JOU_ADDRESS2 = dt_R13.LAST_SBN_JOU_ADDRESS2;
            //最終処分事業場所在地３
            dtPtr13.LAST_SBN_JOU_ADDRESS3 = dt_R13.LAST_SBN_JOU_ADDRESS3;
            //最終処分事業場所在地４
            dtPtr13.LAST_SBN_JOU_ADDRESS4 = dt_R13.LAST_SBN_JOU_ADDRESS4;
            //最終処分事業場電話番号
            dtPtr13.LAST_SBN_JOU_TEL = dt_R13.LAST_SBN_JOU_TEL;
            //最終処分終了日
            dtPtr13.LAST_SBN_END_DATE = dt_R13.LAST_SBN_END_DATE;
            //最終処分業者CD(DT_R13項目なし)
            dtPtr13.LAST_SBN_GYOUSHA_CD = dt_R13_EX.LAST_SBN_GYOUSHA_CD;
            //最終処分事業場CD(DT_R13項目なし)
            dtPtr13.LAST_SBN_GENBA_CD = dt_R13_EX.LAST_SBN_GENBA_CD;
            //レコード作成日時
            dtPtr13.CREATE_DATE = dt_R13.CREATE_DATE;
            //タイムスタンプ
            //dtPtr13.TIME_STAMP = dt_R13.UPDATE_TS;

            return dtPtr13;
        }

        ///電子マニフェスト→電子マニフェストパターン
        private DT_PT_R18 SetDT_PT_R18(DT_R18 dt_R18, DT_R18_EX dt_r18ExOld)
        {
            if (dt_R18 == null || dt_r18ExOld == null) return null;
            DT_PT_R18 dtPtr18 = new DT_PT_R18();

            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //システムID(PK)
                dtPtr18.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr18.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //パターン名(DT_R18項目なし)
            //dtPtr18.PATTERN_NAME = dt_r18ExOld.PATTERN_NAME;
            //パターンふりがなDT_R18項目なし)
            //dtPtr18.PATTERN_FURIGANA = dt_R18.0;
            //マニフェスト／予約番号
            dtPtr18.MANIFEST_ID = dt_R18.MANIFEST_ID;
            //予約/ﾏﾆﾌｪｽﾄ区分
            dtPtr18.MANIFEST_KBN = dt_R18.MANIFEST_KBN;
            //登録情報承認待ちフラグ
            dtPtr18.SHOUNIN_FLAG = dt_R18.SHOUNIN_FLAG;
            //引渡し日
            dtPtr18.HIKIWATASHI_DATE = dt_R18.HIKIWATASHI_DATE;
            //運搬終了報告済フラグ
            dtPtr18.UPN_ENDREP_FLAG = dt_R18.UPN_ENDREP_FLAG;
            //処分終了報告済フラグ
            dtPtr18.SBN_ENDREP_FLAG = dt_R18.SBN_ENDREP_FLAG;
            //最終処分終了報告済フラグ
            dtPtr18.LAST_SBN_ENDREP_FLAG = dt_R18.LAST_SBN_ENDREP_FLAG;
            //課金日
            dtPtr18.KAKIN_DATE = dt_R18.KAKIN_DATE;
            //登録日
            dtPtr18.REGI_DATE = dt_R18.REGI_DATE;
            //運搬・処分終了報告期限日
            dtPtr18.UPN_SBN_REP_LIMIT_DATE = dt_R18.UPN_SBN_REP_LIMIT_DATE;
            //最終処分終了報告期限日
            dtPtr18.LAST_SBN_REP_LIMIT_DATE = dt_R18.LAST_SBN_REP_LIMIT_DATE;
            //予約情報有効期限日
            dtPtr18.RESV_LIMIT_DATE = dt_R18.RESV_LIMIT_DATE;
            //処分終了報告区分
            dtPtr18.SBN_ENDREP_KBN = dt_R18.SBN_ENDREP_KBN;
            //排出事業者の加入者番号
            dtPtr18.HST_SHA_EDI_MEMBER_ID = dt_R18.HST_SHA_EDI_MEMBER_ID;
            //排出事業者名称
            dtPtr18.HST_SHA_NAME = dt_R18.HST_SHA_NAME;
            //排出事業者郵便番号
            dtPtr18.HST_SHA_POST = dt_R18.HST_SHA_POST;
            //排出事業者所在地1
            dtPtr18.HST_SHA_ADDRESS1 = dt_R18.HST_SHA_ADDRESS1;
            //排出事業者所在地2
            dtPtr18.HST_SHA_ADDRESS2 = dt_R18.HST_SHA_ADDRESS2;
            //排出事業者所在地3
            dtPtr18.HST_SHA_ADDRESS3 = dt_R18.HST_SHA_ADDRESS3;
            //排出事業者所在地4
            dtPtr18.HST_SHA_ADDRESS4 = dt_R18.HST_SHA_ADDRESS4;
            //排出事業者の代表番号
            dtPtr18.HST_SHA_TEL = dt_R18.HST_SHA_TEL;
            //排出事業者の代表FAX
            dtPtr18.HST_SHA_FAX = dt_R18.HST_SHA_FAX;
            //排出事業場名称
            dtPtr18.HST_JOU_NAME = dt_R18.HST_JOU_NAME;
            //排出事業場所在地の郵便番号
            dtPtr18.HST_JOU_POST_NO = dt_R18.HST_JOU_POST_NO;
            //排出事業場所在地1
            dtPtr18.HST_JOU_ADDRESS1 = dt_R18.HST_JOU_ADDRESS1;
            //排出事業場所在地2
            dtPtr18.HST_JOU_ADDRESS2 = dt_R18.HST_JOU_ADDRESS2;
            //排出事業場所在地3
            dtPtr18.HST_JOU_ADDRESS3 = dt_R18.HST_JOU_ADDRESS3;
            //排出事業場所在地4
            dtPtr18.HST_JOU_ADDRESS4 = dt_R18.HST_JOU_ADDRESS4;
            //排出事業場電話番号
            dtPtr18.HST_JOU_TEL = dt_R18.HST_JOU_TEL;
            //登録担当者
            dtPtr18.REGI_TAN = dt_R18.REGI_TAN;
            //引渡し担当者
            dtPtr18.HIKIWATASHI_TAN_NAME = dt_R18.HIKIWATASHI_TAN_NAME;
            //大分類コード
            dtPtr18.HAIKI_DAI_CODE = dt_R18.HAIKI_DAI_CODE;
            //中分類コード
            dtPtr18.HAIKI_CHU_CODE = dt_R18.HAIKI_CHU_CODE;
            //小分類コード
            dtPtr18.HAIKI_SHO_CODE = dt_R18.HAIKI_SHO_CODE;
            //細分類コード
            dtPtr18.HAIKI_SAI_CODE = dt_R18.HAIKI_SAI_CODE;
            //廃棄物の大分類名称
            dtPtr18.HAIKI_BUNRUI = dt_R18.HAIKI_BUNRUI;
            //廃棄物の種類
            dtPtr18.HAIKI_SHURUI = dt_R18.HAIKI_SHURUI;
            //廃棄物の名称
            dtPtr18.HAIKI_NAME = dt_R18.HAIKI_NAME;
            //廃棄物の数量
            dtPtr18.HAIKI_SUU = dt_R18.HAIKI_SUU;
            //廃棄物の数量単位コード
            dtPtr18.HAIKI_UNIT_CODE = dt_R18.HAIKI_UNIT_CODE;
            //数量確定者コード
            dtPtr18.SUU_KAKUTEI_CODE = dt_R18.SUU_KAKUTEI_CODE;
            //廃棄物の確定数量
            dtPtr18.HAIKI_KAKUTEI_SUU = dt_R18.HAIKI_KAKUTEI_SUU;
            //廃棄物の確定数量の単位コード
            dtPtr18.HAIKI_KAKUTEI_UNIT_CODE = dt_R18.HAIKI_KAKUTEI_UNIT_CODE;
            //荷姿コード
            dtPtr18.NISUGATA_CODE = dt_R18.NISUGATA_CODE;
            //荷姿名
            dtPtr18.NISUGATA_NAME = dt_R18.NISUGATA_NAME;
            //荷姿の数量
            dtPtr18.NISUGATA_SUU = dt_R18.NISUGATA_SUU;
            //処分業者加入者番号
            dtPtr18.SBN_SHA_MEMBER_ID = dt_R18.SBN_SHA_MEMBER_ID;
            //処分業者名
            dtPtr18.SBN_SHA_NAME = dt_R18.SBN_SHA_NAME;
            //処分業者郵便番号
            dtPtr18.SBN_SHA_POST = dt_R18.SBN_SHA_POST;
            //処分業者所在地1
            dtPtr18.SBN_SHA_ADDRESS1 = dt_R18.SBN_SHA_ADDRESS1;
            //処分業者所在地2
            dtPtr18.SBN_SHA_ADDRESS2 = dt_R18.SBN_SHA_ADDRESS2;
            //処分業者所在地3
            dtPtr18.SBN_SHA_ADDRESS3 = dt_R18.SBN_SHA_ADDRESS3;
            //処分業者所在地4
            dtPtr18.SBN_SHA_ADDRESS4 = dt_R18.SBN_SHA_ADDRESS4;
            //処分業者電話番号
            dtPtr18.SBN_SHA_TEL = dt_R18.SBN_SHA_TEL;
            //処分業者FAX
            dtPtr18.SBN_SHA_FAX = dt_R18.SBN_SHA_FAX;
            //処分業者統一許可番号
            dtPtr18.SBN_SHA_KYOKA_ID = dt_R18.SBN_SHA_KYOKA_ID;
            //再委託先処分業者加入者番号
            dtPtr18.SAI_SBN_SHA_MEMBER_ID = dt_R18.SAI_SBN_SHA_MEMBER_ID;
            //再委託先処分業者名
            dtPtr18.SAI_SBN_SHA_NAME = dt_R18.SAI_SBN_SHA_NAME;
            //再委託先処分業者郵便場号
            dtPtr18.SAI_SBN_SHA_POST = dt_R18.SAI_SBN_SHA_POST;
            //再委託先処分業者所在地1
            dtPtr18.SAI_SBN_SHA_ADDRESS1 = dt_R18.SAI_SBN_SHA_ADDRESS1;
            //再委託先処分業者所在地2
            dtPtr18.SAI_SBN_SHA_ADDRESS2 = dt_R18.SAI_SBN_SHA_ADDRESS2;
            //再委託先処分業者所在地3
            dtPtr18.SAI_SBN_SHA_ADDRESS3 = dt_R18.SAI_SBN_SHA_ADDRESS3;
            //再委託先処分業者所在地4
            dtPtr18.SAI_SBN_SHA_ADDRESS4 = dt_R18.SAI_SBN_SHA_ADDRESS4;
            //再委託先処分業者電話番号
            dtPtr18.SAI_SBN_SHA_TEL = dt_R18.SAI_SBN_SHA_TEL;
            //再委託先処分業者FAX
            dtPtr18.SAI_SBN_SHA_FAX = dt_R18.SAI_SBN_SHA_FAX;
            //再委託先処分業者統一許可番号
            dtPtr18.SAI_SBN_SHA_KYOKA_ID = dt_R18.SAI_SBN_SHA_KYOKA_ID;
            //処分方法コード
            dtPtr18.SBN_WAY_CODE = dt_R18.SBN_WAY_CODE;
            //処分方法名
            dtPtr18.SBN_WAY_NAME = dt_R18.SBN_WAY_NAME;
            //処分報告情報承認待ちフラグ
            dtPtr18.SBN_SHOUNIN_FLAG = dt_R18.SBN_SHOUNIN_FLAG;
            //処分終了日
            dtPtr18.SBN_END_DATE = dt_R18.SBN_END_DATE;
            //廃棄物の受領日
            dtPtr18.HAIKI_IN_DATE = dt_R18.HAIKI_IN_DATE;
            //受入量
            dtPtr18.RECEPT_SUU = dt_R18.RECEPT_SUU;
            //受入量の単位コード
            dtPtr18.RECEPT_UNIT_CODE = dt_R18.RECEPT_UNIT_CODE;
            //運搬担当者
            dtPtr18.UPN_TAN_NAME = dt_R18.UPN_TAN_NAME;
            //車両番号
            dtPtr18.CAR_NO = dt_R18.CAR_NO;
            //報告担当者
            dtPtr18.REP_TAN_NAME = dt_R18.REP_TAN_NAME;
            //処分担当者
            dtPtr18.SBN_TAN_NAME = dt_R18.SBN_TAN_NAME;
            //処分終了報告日
            dtPtr18.SBN_END_REP_DATE = dt_R18.SBN_END_REP_DATE;
            //処分報告備考
            dtPtr18.SBN_REP_BIKOU = dt_R18.SBN_REP_BIKOU;
            //予約登録の修正権限コード
            dtPtr18.KENGEN_CODE = dt_R18.KENGEN_CODE;
            //最終処分事業場記載フラグ
            dtPtr18.LAST_SBN_JOU_KISAI_FLAG = dt_R18.LAST_SBN_JOU_KISAI_FLAG;
            //中間処理産業廃棄物情報管理方法フラグ
            dtPtr18.FIRST_MANIFEST_FLAG = dt_R18.FIRST_MANIFEST_FLAG;
            //最終処分終了日
            dtPtr18.LAST_SBN_END_DATE = dt_R18.LAST_SBN_END_DATE;
            //最終処分終了報告日
            dtPtr18.LAST_SBN_END_REP_DATE = dt_R18.LAST_SBN_END_REP_DATE;
            //修正日
            dtPtr18.SHUSEI_DATE = dt_R18.SHUSEI_DATE;
            //取消フラグ
            dtPtr18.CANCEL_FLAG = dt_R18.CANCEL_FLAG;
            //取消日
            dtPtr18.CANCEL_DATE = dt_R18.CANCEL_DATE;
            //最終更新日
            dtPtr18.LAST_UPDATE_DATE = dt_R18.LAST_UPDATE_DATE;
            //有害物質情報件数
            dtPtr18.YUUGAI_CNT = dt_R18.YUUGAI_CNT;
            //収集運搬情報件数
            dtPtr18.UPN_ROUTE_CNT = dt_R18.UPN_ROUTE_CNT;
            //最終処分事業場（予定）情報件数
            dtPtr18.LAST_SBN_PLAN_CNT = dt_R18.LAST_SBN_PLAN_CNT;
            //最終処分終了日･事業場情報件数
            dtPtr18.LAST_SBN_CNT = dt_R18.LAST_SBN_CNT;
            //連絡番号情報件数
            dtPtr18.RENRAKU_CNT = dt_R18.RENRAKU_CNT;
            //備考情報件数
            dtPtr18.BIKOU_CNT = dt_R18.BIKOU_CNT;
            //中間処理産業廃棄物情報件数
            dtPtr18.FIRST_MANIFEST_CNT = dt_R18.FIRST_MANIFEST_CNT;
            //排出事業者CD(DT_R18項目なし)
            dtPtr18.HST_GYOUSHA_CD = dt_r18ExOld.HST_GYOUSHA_CD;
            //排出事業場CD(DT_R18項目なし)
            dtPtr18.HST_GENBA_CD = dt_r18ExOld.HST_GENBA_CD;
            //処分受託者CD(DT_R18項目なし)
            dtPtr18.SBN_GYOUSHA_CD = dt_r18ExOld.SBN_GYOUSHA_CD;
            //処分事業場CD(DT_R18項目なし)
            dtPtr18.SBN_GENBA_CD = dt_r18ExOld.SBN_GENBA_CD;
            //報告不要処分事業者加入者番号(DT_R18項目なし)
            dtPtr18.NO_REP_SBN_EDI_MEMBER_ID = dt_r18ExOld.NO_REP_SBN_EDI_MEMBER_ID;
            //処分受託者許可番号(DT_R18項目なし)
            //dtPtr18.SBN_KYOKA_NO = dt_r18ExOld.SBN_KYOKA_NO;
            //廃棄物名称CD(DT_R18項目なし)
            dtPtr18.HAIKI_NAME_CD = dt_r18ExOld.HAIKI_NAME_CD;
            //処分方法CD(DT_R18項目なし)
            dtPtr18.SBN_HOUHOU_CD = dt_r18ExOld.SBN_HOUHOU_CD;
            //報告担当者CD(DT_R18項目なし)
            dtPtr18.HOUKOKU_TANTOUSHA_CD = dt_r18ExOld.HOUKOKU_TANTOUSHA_CD;
            //処分担当者CD(DT_R18項目なし)
            dtPtr18.SBN_TANTOUSHA_CD = dt_r18ExOld.SBN_TANTOUSHA_CD;
            //運搬担当者CD(DT_R18項目なし)
            dtPtr18.UPN_TANTOUSHA_CD = dt_r18ExOld.UPN_TANTOUSHA_CD;
            //車輌CD(DT_R18項目なし)
            dtPtr18.SHARYOU_CD = dt_r18ExOld.SHARYOU_CD;
            //換算後数量(DT_R18項目なし)
            dtPtr18.KANSAN_SUU = dt_r18ExOld.KANSAN_SUU;
            //作成者(DT_R18項目なし)
            dtPtr18.CREATE_USER = dt_r18ExOld.CREATE_USER;
            //作成日時
            dtPtr18.CREATE_DATE = dt_R18.CREATE_DATE;
            //作成PC(DT_R18項目なし)
            dtPtr18.CREATE_PC = dt_r18ExOld.CREATE_PC;
            //最終更新者(DT_R18項目なし)
            dtPtr18.UPDATE_USER = dt_r18ExOld.UPDATE_USER;
            //最終更新日時(DT_R18項目なし)
            dtPtr18.UPDATE_DATE = dt_r18ExOld.UPDATE_DATE;
            //最終更新PC(DT_R18項目なし)
            dtPtr18.UPDATE_PC = dt_r18ExOld.UPDATE_PC;
            //削除フラグ(DT_R18項目なし)
            dtPtr18.DELETE_FLG = dt_r18ExOld.DELETE_FLG;
            //タイムスタンプ
            if (this.ManiPtnInfo != null)
            {
                dtPtr18.TIME_STAMP = this.ManiPtnInfo.dt_PT_R18.TIME_STAMP;
            }

            return dtPtr18;
        }

        /// <summary>
        /// 廃棄物明細→電子マニフェストパターン拡張
        /// </summary>
        /// <returns></returns>
        private DT_PT_R18_EX SetDT_PT_R18_EX(DataGridViewCellCollection col)
        {
            DT_PT_R18_EX dtPtr18EX = new DT_PT_R18_EX();

            if (col["HAIKI_SHURUI_CD"].Value != null)
            {
                dtPtr18EX.HAIKI_SHURUI_CODE = col["HAIKI_SHURUI_CD"].Value.ToString();
            }
            if (col["HAIKI_SHURUI_NAME"].Value != null)
            {
                dtPtr18EX.HAIKI_SHURUI_NAME = col["HAIKI_SHURUI_NAME"].Value.ToString();
            }
            if (col["HAIKI_NAME_CD"].Value != null)
            {
                dtPtr18EX.HAIKI_NAME_CODE = col["HAIKI_NAME_CD"].Value.ToString();
            }
            if (col["HAIKI_NAME"].Value != null)
            {
                dtPtr18EX.HAIKI_NAME = col["HAIKI_NAME"].Value.ToString();
            }
            if (col["HAIKI_SUU"].Value != null)
            {
                dtPtr18EX.HAIKI_SUU = SqlDecimal.Parse(col["HAIKI_SUU"].Value.ToString());
            }
            if (col["UNIT_CD"].Value != null)
            {
                dtPtr18EX.UNIT_CODE = col["UNIT_CD"].Value.ToString();
            }
            if (col["UNIT_NAME"].Value != null)
            {
                dtPtr18EX.UNIT_NAME = col["UNIT_NAME"].Value.ToString();
            }
            if (col["KANSAN_SUU"].Value != null)
            {
                dtPtr18EX.KANSAN_SUU = SqlDecimal.Parse(col["KANSAN_SUU"].Value.ToString());
            }
            if (col["GENNYOU_SUU"].Value != null)
            {
                dtPtr18EX.GENNYOU_SUU = SqlDecimal.Parse(col["GENNYOU_SUU"].Value.ToString());
            }
            if (col["NISUGATA_CD"].Value != null)
            {
                dtPtr18EX.NISUGATA_CODE = col["NISUGATA_CD"].Value.ToString();
            }
            if (col["NISUGATA_NAME"].Value != null)
            {
                dtPtr18EX.NISUGATA_NAME = col["NISUGATA_NAME"].Value.ToString();
            }
            if (col["NISUGATA_SUU"].Value != null)
            {
                dtPtr18EX.NISUGATA_SUU = col["NISUGATA_SUU"].Value.ToString();
            }
            if (col["SUU_KAKUTEI_CODE"].Value != null)
            {
                dtPtr18EX.SUU_KAKUTEI_CODE = col["SUU_KAKUTEI_CODE"].Value.ToString();
            }
            if (col["SUU_KAKUTEI_NAME"].Value != null)
            {
                dtPtr18EX.SUU_KAKUTEI_NAME = col["SUU_KAKUTEI_NAME"].Value.ToString();
            }
            if (col["YUUGAI_CODE1"].Value != null)
            {
                dtPtr18EX.YUUGAI_CODE1 = col["YUUGAI_CODE1"].Value.ToString();
            }
            if (col["YUUGAI_NAME1"].Value != null)
            {
                dtPtr18EX.YUUGAI_NAME1 = col["YUUGAI_NAME1"].Value.ToString();
            }
            if (col["YUUGAI_CODE2"].Value != null)
            {
                dtPtr18EX.YUUGAI_CODE2 = col["YUUGAI_CODE2"].Value.ToString();
            }
            if (col["YUUGAI_NAME2"].Value != null)
            {
                dtPtr18EX.YUUGAI_NAME2 = col["YUUGAI_NAME2"].Value.ToString();
            }
            if (col["YUUGAI_CODE3"].Value != null)
            {
                dtPtr18EX.YUUGAI_CODE3 = col["YUUGAI_CODE3"].Value.ToString();
            }
            if (col["YUUGAI_NAME3"].Value != null)
            {
                dtPtr18EX.YUUGAI_NAME3 = col["YUUGAI_NAME3"].Value.ToString();
            }
            if (col["YUUGAI_CODE4"].Value != null)
            {
                dtPtr18EX.YUUGAI_CODE4 = col["YUUGAI_CODE4"].Value.ToString();
            }
            if (col["YUUGAI_NAME4"].Value != null)
            {
                dtPtr18EX.YUUGAI_NAME4 = col["YUUGAI_NAME4"].Value.ToString();
            }
            if (col["YUUGAI_CODE5"].Value != null)
            {
                dtPtr18EX.YUUGAI_CODE5 = col["YUUGAI_CODE5"].Value.ToString();
            }
            if (col["YUUGAI_NAME5"].Value != null)
            {
                dtPtr18EX.YUUGAI_NAME5 = col["YUUGAI_NAME5"].Value.ToString();
            }
            if (col["YUUGAI_CODE6"].Value != null)
            {
                dtPtr18EX.YUUGAI_CODE6 = col["YUUGAI_CODE6"].Value.ToString();
            }
            if (col["YUUGAI_NAME6"].Value != null)
            {
                dtPtr18EX.YUUGAI_NAME6 = col["YUUGAI_NAME6"].Value.ToString();
            }

            //入力区分
            dtPtr18EX.INPUT_KBN = SqlDecimal.Parse(this.form.cntxt_InputKBN.Text);

            return dtPtr18EX;
        }

        ///収集運搬→電子マニフェストパターン収集運搬
        private DT_PT_R19 SetDT_PT_R19(DT_R19 dt_R19, DT_R19_EX dt_R19_EX)
        {
            if (dt_R19 == null || dt_R19_EX == null) return null;
            DT_PT_R19 dtPtr19 = new DT_PT_R19();
            if (!string.IsNullOrEmpty(this.Ptn_System_ID))
            {
                //システムID(PK)
                dtPtr19.SYSTEM_ID = SqlInt64.Parse(this.Ptn_System_ID);
                //枝番(PK)
                dtPtr19.SEQ = SqlInt32.Parse(this.Ptn_SEQ);
            }
            //区間番号(PK)
            dtPtr19.UPN_ROUTE_NO = dt_R19.UPN_ROUTE_NO;
            //マニフェスト／予約番号
            dtPtr19.MANIFEST_ID = dt_R19.MANIFEST_ID;
            //収集運搬業者加入者番号
            dtPtr19.UPN_SHA_EDI_MEMBER_ID = dt_R19.UPN_SHA_EDI_MEMBER_ID;
            //収集運搬業者名
            dtPtr19.UPN_SHA_NAME = dt_R19.UPN_SHA_NAME;
            //収集運搬業者郵便番号
            dtPtr19.UPN_SHA_POST = dt_R19.UPN_SHA_POST;
            //収集運搬業者所在地1
            dtPtr19.UPN_SHA_ADDRESS1 = dt_R19.UPN_SHA_ADDRESS1;
            //収集運搬業者所在地2
            dtPtr19.UPN_SHA_ADDRESS2 = dt_R19.UPN_SHA_ADDRESS2;
            //収集運搬業者所在地3
            dtPtr19.UPN_SHA_ADDRESS3 = dt_R19.UPN_SHA_ADDRESS3;
            //収集運搬業者所在地4
            dtPtr19.UPN_SHA_ADDRESS4 = dt_R19.UPN_SHA_ADDRESS4;
            //収集運搬業者電話番号
            dtPtr19.UPN_SHA_TEL = dt_R19.UPN_SHA_TEL;
            //収集運搬業者FAX
            dtPtr19.UPN_SHA_FAX = dt_R19.UPN_SHA_FAX;
            //収集運搬業者統一許可番号
            dtPtr19.UPN_SHA_KYOKA_ID = dt_R19.UPN_SHA_KYOKA_ID;
            //再委託先収集運搬業者加入者番号
            dtPtr19.SAI_UPN_SHA_EDI_MEMBER_ID = dt_R19.SAI_UPN_SHA_EDI_MEMBER_ID;
            //再委託先収集運搬業者名
            dtPtr19.SAI_UPN_SHA_NAME = dt_R19.SAI_UPN_SHA_NAME;
            //再委託先収集運搬業者郵便番号
            dtPtr19.SAI_UPN_SHA_POST = dt_R19.SAI_UPN_SHA_POST;
            //再委託先収集運搬業者所在地1
            dtPtr19.SAI_UPN_SHA_ADDRESS1 = dt_R19.SAI_UPN_SHA_ADDRESS1;
            //再委託先収集運搬業者所在地2
            dtPtr19.SAI_UPN_SHA_ADDRESS2 = dt_R19.SAI_UPN_SHA_ADDRESS2;
            //再委託先収集運搬業者所在地3
            dtPtr19.SAI_UPN_SHA_ADDRESS3 = dt_R19.SAI_UPN_SHA_ADDRESS3;
            //再委託先収集運搬業者所在地4
            dtPtr19.SAI_UPN_SHA_ADDRESS4 = dt_R19.SAI_UPN_SHA_ADDRESS4;
            //再委託先収集運搬業者電話番号
            dtPtr19.SAI_UPN_SHA_TEL = dt_R19.SAI_UPN_SHA_TEL;
            //再委託先収集運搬業者FAX
            dtPtr19.SAI_UPN_SHA_FAX = dt_R19.SAI_UPN_SHA_FAX;
            //再委託先収集運搬業者統一許可番号
            dtPtr19.SAI_UPN_SHA_KYOKA_ID = dt_R19.SAI_UPN_SHA_KYOKA_ID;
            //運搬方法コード
            dtPtr19.UPN_WAY_CODE = dt_R19.UPN_WAY_CODE;
            //運搬担当者
            dtPtr19.UPN_TAN_NAME = dt_R19.UPN_TAN_NAME;
            //車両番号
            dtPtr19.CAR_NO = dt_R19.CAR_NO;
            //運搬先加入者番号
            dtPtr19.UPNSAKI_EDI_MEMBER_ID = dt_R19.UPNSAKI_EDI_MEMBER_ID;
            //運搬先加入者名
            dtPtr19.UPNSAKI_NAME = dt_R19.UPNSAKI_NAME;
            //運搬先事業場番号
            dtPtr19.UPNSAKI_JOU_ID = dt_R19.UPNSAKI_JOU_ID;
            //運搬先事業場区分
            dtPtr19.UPNSAKI_JOU_KBN = dt_R19.UPNSAKI_JOU_KBN;
            //運搬先事業場名
            dtPtr19.UPNSAKI_JOU_NAME = dt_R19.UPNSAKI_JOU_NAME;
            //運搬先事業場郵便番号
            dtPtr19.UPNSAKI_JOU_POST = dt_R19.UPNSAKI_JOU_POST;
            //運搬先事業場所在地1
            dtPtr19.UPNSAKI_JOU_ADDRESS1 = dt_R19.UPNSAKI_JOU_ADDRESS1;
            //運搬先事業場所在地2
            dtPtr19.UPNSAKI_JOU_ADDRESS2 = dt_R19.UPNSAKI_JOU_ADDRESS2;
            //運搬先事業場所在地3
            dtPtr19.UPNSAKI_JOU_ADDRESS3 = dt_R19.UPNSAKI_JOU_ADDRESS3;
            //運搬先事業場所在地4
            dtPtr19.UPNSAKI_JOU_ADDRESS4 = dt_R19.UPNSAKI_JOU_ADDRESS4;
            //運搬先事業場電話番号
            dtPtr19.UPNSAKI_JOU_TEL = dt_R19.UPNSAKI_JOU_TEL;
            //運搬報告情報承認待ちフラグ
            dtPtr19.UPN_SHOUNIN_FLAG = dt_R19.UPN_SHOUNIN_FLAG;
            //運搬終了日
            dtPtr19.UPN_END_DATE = dt_R19.UPN_END_DATE;
            //運搬報告記載の運搬担当者
            dtPtr19.UPNREP_UPN_TAN_NAME = dt_R19.UPNREP_UPN_TAN_NAME;
            //運搬報告記載の車両番号
            dtPtr19.UPNREP_CAR_NO = dt_R19.UPNREP_CAR_NO;
            //運搬量
            dtPtr19.UPN_SUU = dt_R19.UPN_SUU;
            //運搬量の単位コード
            dtPtr19.UPN_UNIT_CODE = dt_R19.UPN_UNIT_CODE;
            //有価物拾集量
            dtPtr19.YUUKA_SUU = dt_R19.YUUKA_SUU;
            //有価物拾集量の単位コード
            dtPtr19.YUUKA_UNIT_CODE = dt_R19.YUUKA_UNIT_CODE;
            //報告担当者
            dtPtr19.REP_TAN_NAME = dt_R19.REP_TAN_NAME;
            //備考
            dtPtr19.BIKOU = dt_R19.BIKOU;
            //収集運搬業者CD(DT_R19項目なし)
            dtPtr19.UPN_GYOUSHA_CD = dt_R19_EX.UPN_GYOUSHA_CD;
            //報告不要収集運搬業者加入者番号(DT_R19項目なし)
            dtPtr19.NO_REP_UPN_EDI_MEMBER_ID = dt_R19_EX.NO_REP_UPN_EDI_MEMBER_ID;
            //運搬先業者CD(DT_R19項目なし)
            dtPtr19.UPNSAKI_GYOUSHA_CD = dt_R19_EX.UPNSAKI_GYOUSHA_CD;
            //報告不要運搬先業者加入者番号(DT_R19項目なし)
            dtPtr19.NO_REP_UPNSAKI_EDI_MEMBER_ID = dt_R19_EX.NO_REP_UPNSAKI_EDI_MEMBER_ID;
            //運搬先事業場CD(DT_R19項目なし)
            dtPtr19.UPNSAKI_GENBA_CD = dt_R19_EX.UPNSAKI_GENBA_CD;
            //運搬担当者CD(DT_R19項目なし)
            dtPtr19.UPN_TANTOUSHA_CD = dt_R19_EX.UPN_TANTOUSHA_CD;
            //車輌CD(DT_R19項目なし)
            dtPtr19.SHARYOU_CD = dt_R19_EX.SHARYOU_CD;
            //運搬報告記載の運搬担当者CD(DT_R19項目なし)
            dtPtr19.UPNREP_UPN_TANTOUSHA_CD = dt_R19_EX.UPNREP_UPN_TANTOUSHA_CD;
            //運搬報告記載の車輌CD(DT_R19項目なし)
            dtPtr19.UPNREP_SHARYOU_CD = dt_R19_EX.UPNREP_SHARYOU_CD;
            //報告担当者CD(DT_R19項目なし)
            dtPtr19.HOUKOKU_TANTOUSHA_CD = dt_R19_EX.HOUKOKU_TANTOUSHA_CD;
            //レコード作成日時
            dtPtr19.CREATE_DATE = dt_R19.CREATE_DATE;
            //タイムスタンプ
            //dtPtr19.TIME_STAMP = dt_R19.UPDATE_TS;

            return dtPtr19;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ManiPtnInfo"></param>
        /// <returns></returns>
        public DenshiManifestInfoCls CopyManiPtnInfoToManiInfo(DenshiManifestPatternInfoCls ManiPtnInfo)
        {
            if (ManiPtnInfo == null) return null;

            //以下画面情報から電子マニフェストパターンに設定する
            //電子マニフェストパターン
            DenshiManifestInfoCls ManiInfo = new DenshiManifestInfoCls();
            //パタン呼出フラグ
            ManiInfo.bIsFromPattern = true;
            //入力モード「自動手動」
            ManiInfo.bIsAutoMode = (this.form.cntxt_InputKBN.Text == "1");

            //有害物質情報
            List<DT_R02> lstDT_R02 = new List<DT_R02>();

            //最終処分事業場（予定）情報
            List<DT_R04> lstDT_R04 = new List<DT_R04>();

            //連絡番号情報
            List<DT_R05> lstDT_R05 = new List<DT_R05>();

            //備考情報
            List<DT_R06> lstDT_R06 = new List<DT_R06>();

            //最終処分終了日・事業場情報
            List<DT_R13> lstDT_R13 = new List<DT_R13>();

            //電子マニフェスト情報
            DT_R18 dt_r18 = new DT_R18();

            //運搬情報
            List<DT_R19> lstDT_R19 = new List<DT_R19>();

            //電子基本拡張[既存データ]
            DT_R18_EX dt_r18ExOld = new DT_R18_EX();

            //電子運搬拡張
            List<DT_R19_EX> lstDT_R19_EX = new List<DT_R19_EX>();

            //電子最終処分(予定)拡張 
            List<DT_R04_EX> lstDT_R04_EX = new List<DT_R04_EX>();

            //電子最終処分(実績)拡張
            List<DT_R13_EX> lstDT_R13_EX = new List<DT_R13_EX>();

            //有害物質情報パターン
            List<DT_PT_R02> lstDT_PT_R02 = new List<DT_PT_R02>();

            //最終処分事業場（予定）情報パターン
            List<DT_PT_R04> lstDT_PT_R04 = new List<DT_PT_R04>();

            //連絡番号情報パターン
            List<DT_PT_R05> lstDT_PT_R05 = new List<DT_PT_R05>();

            //備考情報パターン
            List<DT_PT_R06> lstDT_PT_R06 = new List<DT_PT_R06>();

            //最終処分終了日・事業場情報パターン
            List<DT_PT_R13> lstDT_PT_R13 = new List<DT_PT_R13>();

            //電子マニフェスト情報
            DT_PT_R18 dt_PT_r18 = new DT_PT_R18();

            //運搬情報パターン
            List<DT_PT_R19> lstDT_PT_R19 = new List<DT_PT_R19>();

            //パターン
            if (ManiPtnInfo != null)
            {
                dt_PT_r18 = ManiPtnInfo.dt_PT_R18;
                lstDT_PT_R02 = ManiPtnInfo.lstDT_PT_R02;
                lstDT_PT_R04 = ManiPtnInfo.lstDT_PT_R04;
                lstDT_PT_R05 = ManiPtnInfo.lstDT_PT_R05;
                lstDT_PT_R06 = ManiPtnInfo.lstDT_PT_R06;
                lstDT_PT_R13 = ManiPtnInfo.lstDT_PT_R13;
                lstDT_PT_R19 = ManiPtnInfo.lstDT_PT_R19;
            }

            //有害物質
            if (lstDT_PT_R02 != null && lstDT_PT_R02.Count > 0)
            {
                for (int i = 0; i < lstDT_PT_R02.Count; i++)
                {
                    DT_PT_R02 dt_PT_R02 = lstDT_PT_R02[i];
                    DT_R02 dt_R02 = SetDT_R02(dt_PT_R02);
                    lstDT_R02.Add(dt_R02);
                }
            }

            //最終処分事業場
            if (lstDT_PT_R04 != null && lstDT_PT_R04.Count > 0)
            {
                for (int i = 0; i < lstDT_PT_R04.Count; i++)
                {
                    DT_PT_R04 dt_PT_R04 = lstDT_PT_R04[i];
                    DT_R04 dtR04 = SetDT_R04(dt_PT_R04);
                    DT_R04_EX dtR04EX = SetDT_R04EX(dt_PT_R04);
                    lstDT_R04.Add(dtR04);
                    lstDT_R04_EX.Add(dtR04EX);
                }
            }


            //連絡番号
            if (lstDT_PT_R05 != null && lstDT_PT_R05.Count > 0)
            {
                for (int i = 0; i < lstDT_PT_R05.Count; i++)
                {
                    DT_PT_R05 dt_PT_R05 = lstDT_PT_R05[i];
                    DT_R05 dt_R05 = SetDT_R05(dt_PT_R05);
                    lstDT_R05.Add(dt_R05);
                }
            }

            //備考
            if (lstDT_PT_R06 != null && lstDT_PT_R06.Count > 0)
            {
                for (int i = 0; i < lstDT_PT_R06.Count; i++)
                {
                    DT_PT_R06 dt_PT_R06 = lstDT_PT_R06[i];
                    DT_R06 dt_R06 = SetDT_R06(dt_PT_R06);
                    lstDT_R06.Add(dt_R06);
                }
            }

            //最終処分(実績)拡張
            //自動モード場合、実績情報を表示しない[障害表No.604対応]
            if (!ManiInfo.bIsAutoMode)
            {
                if (lstDT_PT_R13 != null && lstDT_PT_R13.Count > 0)
                {
                    for (int i = 0; i < lstDT_PT_R13.Count; i++)
                    {
                        DT_PT_R13 dt_PT_R13 = lstDT_PT_R13[i];
                        DT_R13 dt_R13 = SetDT_R13(dt_PT_R13);
                        DT_R13_EX dt_R13EX = SetDT_R13EX(dt_PT_R13);
                        lstDT_R13.Add(dt_R13);
                        lstDT_R13_EX.Add(dt_R13EX);
                    }
                }
            }

            //電子マニフェスト
            if (dt_PT_r18 != null)
            {
                dt_r18 = SetDT_R18(dt_PT_r18);

                dt_r18ExOld = SetDT_R18EX(dt_PT_r18);
            }

            //収集運搬
            if (lstDT_PT_R19 != null && lstDT_PT_R19.Count > 0)
            {
                for (int i = 0; i < lstDT_PT_R19.Count; i++)
                {
                    DT_PT_R19 dt_PT_R19 = lstDT_PT_R19[i];
                    DT_R19 dt_R19 = SetDT_R19(dt_PT_R19);
                    DT_R19_EX dt_R19EX = SetDT_R19EX(dt_PT_R19);
                    lstDT_R19.Add(dt_R19);
                    lstDT_R19_EX.Add(dt_R19EX);
                }
            }

            //有害物質情報
            ManiInfo.lstDT_R02 = lstDT_R02;

            //最終処分事業場（予定）情報
            ManiInfo.lstDT_R04 = lstDT_R04;

            //電子最終処分(予定)拡張
            ManiInfo.lstDT_R04_EX = lstDT_R04_EX;

            //連絡番号情報
            ManiInfo.lstDT_R05 = lstDT_R05;

            //備考情報
            ManiInfo.lstDT_R06 = lstDT_R06;

            //最終処分終了日・事業場情報
            ManiInfo.lstDT_R13 = lstDT_R13;

            //電子最終処分拡張(実績)
            ManiInfo.lstDT_R13_EX = lstDT_R13_EX;

            //電子マニフェスト情報
            ManiInfo.dt_r18 = dt_r18;

            //電子マニフェスト情報拡展
            ManiInfo.dt_r18ExOld = dt_r18ExOld;

            //運搬情報
            ManiInfo.lstDT_R19 = lstDT_R19;

            //電子運搬拡張
            ManiInfo.lstDT_R19_EX = lstDT_R19_EX;

            return ManiInfo;
        }

        ///有害物質情報→電子マニフェストパターン有害物質
        private DT_R02 SetDT_R02(DT_PT_R02 dtPtr02)
        {
            if (dtPtr02 == null) return null;

            DT_R02 dt_R02 = new DT_R02();
            //管理番号(PK)
            //dt_R02.KANRI_ID = dtPtr02.SYSTEM_ID;
            //枝番(PK)
            //dt_R02.SEQ = dtPtr02.SEQ;
            //レコード連番(PK)
            //dt_R02.REC_SEQ = dtPtr02.REC_SEQ;
            //マニフェスト／予約番号
            dt_R02.MANIFEST_ID = dtPtr02.MANIFEST_ID;
            //有害物質コード
            dt_R02.YUUGAI_CODE = dtPtr02.YUUGAI_CODE;
            //有害物質名
            dt_R02.YUUGAI_NAME = dtPtr02.YUUGAI_NAME;
            //レコード作成日時
            dt_R02.CREATE_DATE = dtPtr02.CREATE_DATE;
            //タイムスタンプ
            //dt_R02.UPDATE_TS = dtPtr02.TIME_STAMP;

            return dt_R02;
        }

        ///最終処分事業場（予定）情報→電子マニフェストパターン最終処分(予定)
        private DT_R04 SetDT_R04(DT_PT_R04 dtPtr04)
        {
            if (dtPtr04 == null) return null;

            DT_R04 dt_R04 = new DT_R04();
            //システムID(PK)
            //dt_R04.KANRI_ID = dtPtr04.SYSTEM_ID;
            //枝番(PK)
            //dt_R04.SEQ = dtPtr04.SEQ;
            //レコード連番(PK)
            //dt_R04.REC_SEQ = dtPtr04.REC_SEQ;
            //マニフェスト／予約番号
            dt_R04.MANIFEST_ID = dtPtr04.MANIFEST_ID;
            //最終処分事業場名称
            dt_R04.LAST_SBN_JOU_NAME = dtPtr04.LAST_SBN_JOU_NAME;
            //最終処分事業場所在地郵便番号
            dt_R04.LAST_SBN_JOU_POST = dtPtr04.LAST_SBN_JOU_POST;
            //最終処分事業場所在地１
            dt_R04.LAST_SBN_JOU_ADDRESS1 = dtPtr04.LAST_SBN_JOU_ADDRESS1;
            //最終処分事業場所在地２
            dt_R04.LAST_SBN_JOU_ADDRESS2 = dtPtr04.LAST_SBN_JOU_ADDRESS2;
            //最終処分事業場所在地３
            dt_R04.LAST_SBN_JOU_ADDRESS3 = dtPtr04.LAST_SBN_JOU_ADDRESS3;
            //最終処分事業場所在地４
            dt_R04.LAST_SBN_JOU_ADDRESS4 = dtPtr04.LAST_SBN_JOU_ADDRESS4;
            //最終処分事業場電話番号
            dt_R04.LAST_SBN_JOU_TEL = dtPtr04.LAST_SBN_JOU_TEL;
            //最終処分業者CD(DT_R04項目なし)
            //dt_R04.0 = dtPtr04.LAST_SBN_GYOUSHA_CD;
            //最終処分事業場CD(DT_R04項目なし)
            // dt_R04.0 = dtPtr04.LAST_SBN_GENBA_CD;
            //レコード作成日時
            dt_R04.CREATE_DATE = dtPtr04.CREATE_DATE;
            //タイムスタンプ
            //dt_R04.UPDATE_TS = dtPtr04.TIME_STAMP;

            return dt_R04;
        }

        ///最終処分事業場（予定）情報→電子マニフェストパターン最終処分(予定)拡展
        private DT_R04_EX SetDT_R04EX(DT_PT_R04 dtPtr04)
        {
            DT_R04_EX dt_R04EX = new DT_R04_EX();

            if (dtPtr04 == null) return null;
            //システムID(PK)
            //dt_R04EX.SYSTEM_ID = dtPtr04.SYSTEM_ID;
            //枝番(PK)
            //dt_R04EX.SEQ = dtPtr04.SEQ;
            //レコード連番(PK)
            //dt_R04EX.REC_SEQ = dtPtr04.REC_SEQ;
            //管理番号
            //dt_R04EX.KANRI_ID = dtPtr04.KANRI_ID;
            //マニフェスト／予約番号
            dt_R04EX.MANIFEST_ID = dtPtr04.MANIFEST_ID;

            //最終処分業者CD(DT_R04項目なし)
            dt_R04EX.LAST_SBN_GYOUSHA_CD = dtPtr04.LAST_SBN_GYOUSHA_CD;
            //最終処分事業場CD(DT_R04項目なし)
            dt_R04EX.LAST_SBN_GENBA_CD = dtPtr04.LAST_SBN_GENBA_CD;

            //削除フラグ
            //dt_R04EX.DELETE_FLG = dtPtr04.DELETE_FLG;
            //タイムスタンプ
            //dt_R04EX.UPDATE_TS = dtPtr04.TIME_STAMP;

            return dt_R04EX;
        }

        ///連絡番号→電子マニフェストパターン連絡番号
        private DT_R05 SetDT_R05(DT_PT_R05 dtPtr05)
        {
            if (dtPtr05 == null) return null;
            DT_R05 dt_R05 = new DT_R05();
            //システムID(PK)
            //dt_R05.KANRI_ID = dtPtr05.SYSTEM_ID;
            //枝番(PK)
            //dt_R05.SEQ = dtPtr05.SEQ;
            //マニフェスト／予約番号
            dt_R05.MANIFEST_ID = dtPtr05.MANIFEST_ID;
            //連絡番号No(PK)
            dt_R05.RENRAKU_ID_NO = dtPtr05.RENRAKU_ID_NO;
            //連絡番号
            dt_R05.RENRAKU_ID = dtPtr05.RENRAKU_ID;
            //レコード作成日時
            dt_R05.CREATE_DATE = dtPtr05.CREATE_DATE;
            //タイムスタンプ
            //dt_R05.UPDATE_TS = dtPtr05.TIME_STAMP;

            return dt_R05;

        }

        ///備考→電子マニフェストパターン備考
        private DT_R06 SetDT_R06(DT_PT_R06 dtPtr06)
        {
            if (dtPtr06 == null) return null;
            DT_R06 dt_R06 = new DT_R06();
            //システムID(PK)
            //dt_R06.KANRI_ID = dtPtr06.SYSTEM_ID;
            //枝番(PK)
            //dt_R06.SEQ = dtPtr06.SEQ;
            //マニフェスト／予約番号
            dt_R06.MANIFEST_ID = dtPtr06.MANIFEST_ID;
            //備考No(PK)
            dt_R06.BIKOU_NO = dtPtr06.BIKOU_NO;
            //備考
            dt_R06.BIKOU = dtPtr06.BIKOU;
            //レコード作成日時
            dt_R06.CREATE_DATE = dtPtr06.CREATE_DATE;
            //タイムスタンプ
            //dt_R06.UPDATE_TS = dtPtr06.TIME_STAMP;

            return dt_R06;
        }

        ///最終処分→電子マニフェストパターン最終処分
        private DT_R13 SetDT_R13(DT_PT_R13 dtPtr13)
        {
            if (dtPtr13 == null) { return null; }
            DT_R13 dt_R13 = new DT_R13();

            //システムID(PK)
            //dt_R13.KANRI_ID = dtPtr13.SYSTEM_ID;
            //枝番(PK)
            //dt_R13.SEQ = dtPtr13.SEQ;
            //レコード連番(PK)
            //dt_R13.REC_SEQ = dtPtr13.REC_SEQ;
            //マニフェスト／予約番号
            dt_R13.MANIFEST_ID = dtPtr13.MANIFEST_ID;
            //最終処分事業場名称
            dt_R13.LAST_SBN_JOU_NAME = dtPtr13.LAST_SBN_JOU_NAME;
            //最終処分事業場所在地の郵便番号
            dt_R13.LAST_SBN_JOU_POST = dtPtr13.LAST_SBN_JOU_POST;
            //最終処分事業場所在地１
            dt_R13.LAST_SBN_JOU_ADDRESS1 = dtPtr13.LAST_SBN_JOU_ADDRESS1;
            //最終処分事業場所在地２
            dt_R13.LAST_SBN_JOU_ADDRESS2 = dtPtr13.LAST_SBN_JOU_ADDRESS2;
            //最終処分事業場所在地３
            dt_R13.LAST_SBN_JOU_ADDRESS3 = dtPtr13.LAST_SBN_JOU_ADDRESS3;
            //最終処分事業場所在地４
            dt_R13.LAST_SBN_JOU_ADDRESS4 = dtPtr13.LAST_SBN_JOU_ADDRESS4;
            //最終処分事業場電話番号
            dt_R13.LAST_SBN_JOU_TEL = dtPtr13.LAST_SBN_JOU_TEL;
            //最終処分終了日
            dt_R13.LAST_SBN_END_DATE = dtPtr13.LAST_SBN_END_DATE;
            //最終処分業者CD(DT_R13項目なし)
            //dt_R13.0 = dtPtr13.LAST_SBN_GYOUSHA_CD;
            //最終処分事業場CD(DT_R13項目なし)
            //dt_R13.0 = dtPtr13.LAST_SBN_GENBA_CD;
            //レコード作成日時
            dt_R13.CREATE_DATE = dtPtr13.CREATE_DATE;
            //タイムスタンプ
            //dt_R13.UPDATE_TS = dtPtr13.TIME_STAMP;

            return dt_R13;
        }

        ///最終処分→電子マニフェストパターン最終処分拡張
        private DT_R13_EX SetDT_R13EX(DT_PT_R13 dtPtr13)
        {
            if (dtPtr13 == null) return null;
            DT_R13_EX dt_R13EX = new DT_R13_EX();

            //システムID(PK)
            //dt_R13EX.KANRI_ID = dtPtr13.SYSTEM_ID;
            //枝番(PK)
            //dt_R13EX.SEQ = dtPtr13.SEQ;
            //レコード連番(PK)
            //dt_R13EX.REC_SEQ = dtPtr13.REC_SEQ;

            //管理番号
            //dt_R13EX.KANRI_ID = dtPtr13.KANRI_ID;

            //マニフェスト／予約番号
            dt_R13EX.MANIFEST_ID = dtPtr13.MANIFEST_ID;

            //最終処分業者CD(DT_R13項目なし)
            dt_R13EX.LAST_SBN_GYOUSHA_CD = dtPtr13.LAST_SBN_GYOUSHA_CD;
            //最終処分事業場CD(DT_R13項目なし)
            dt_R13EX.LAST_SBN_GENBA_CD = dtPtr13.LAST_SBN_GENBA_CD;
            //レコード作成日時
            dt_R13EX.CREATE_DATE = dtPtr13.CREATE_DATE;
            //タイムスタンプ
            //dt_R13.UPDATE_TS = dtPtr13.TIME_STAMP;

            return dt_R13EX;
        }

        ///電子マニフェスト→電子マニフェストパターン
        private DT_R18 SetDT_R18(DT_PT_R18 dtPtr18)
        {
            if (dtPtr18 == null) return null;
            DT_R18 dt_R18 = new DT_R18();
            //システムID(PK)
            //dt_R18.KANRI_ID = dtPtr18.SYSTEM_ID;
            //枝番(PK)
            //dt_R18.SEQ = dtPtr18.SEQ;
            //パターン名(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.PATTERN_NAME;
            //パターンふりがな(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.PATTERN_FURIGANA;

            //マニフェスト／予約番号が設定しない
            //dt_R18.MANIFEST_ID = dtPtr18.MANIFEST_ID;

            //予約/ﾏﾆﾌｪｽﾄ区分
            dt_R18.MANIFEST_KBN = dtPtr18.MANIFEST_KBN.ToSqlInt16();
            //登録情報承認待ちフラグ
            dt_R18.SHOUNIN_FLAG = dtPtr18.SHOUNIN_FLAG.ToSqlInt16();
            //引渡し日
            dt_R18.HIKIWATASHI_DATE = dtPtr18.HIKIWATASHI_DATE;
            //運搬終了報告済フラグ
            dt_R18.UPN_ENDREP_FLAG = dtPtr18.UPN_ENDREP_FLAG.ToSqlInt16();
            //処分終了報告済フラグ
            dt_R18.SBN_ENDREP_FLAG = dtPtr18.SBN_ENDREP_FLAG.ToSqlInt16();
            //最終処分終了報告済フラグ
            dt_R18.LAST_SBN_ENDREP_FLAG = dtPtr18.LAST_SBN_ENDREP_FLAG.ToSqlInt16();
            //課金日
            dt_R18.KAKIN_DATE = dtPtr18.KAKIN_DATE;
            //登録日
            dt_R18.REGI_DATE = dtPtr18.REGI_DATE;
            //運搬・処分終了報告期限日
            dt_R18.UPN_SBN_REP_LIMIT_DATE = dtPtr18.UPN_SBN_REP_LIMIT_DATE;
            //最終処分終了報告期限日
            dt_R18.LAST_SBN_REP_LIMIT_DATE = dtPtr18.LAST_SBN_REP_LIMIT_DATE;
            //予約情報有効期限日
            dt_R18.RESV_LIMIT_DATE = dtPtr18.RESV_LIMIT_DATE;
            //処分終了報告区分
            dt_R18.SBN_ENDREP_KBN = dtPtr18.SBN_ENDREP_KBN.ToSqlInt16();
            //排出事業者の加入者番号
            dt_R18.HST_SHA_EDI_MEMBER_ID = dtPtr18.HST_SHA_EDI_MEMBER_ID;
            //排出事業者名称
            dt_R18.HST_SHA_NAME = dtPtr18.HST_SHA_NAME;
            //排出事業者郵便番号
            dt_R18.HST_SHA_POST = dtPtr18.HST_SHA_POST;
            //排出事業者所在地1
            dt_R18.HST_SHA_ADDRESS1 = dtPtr18.HST_SHA_ADDRESS1;
            //排出事業者所在地2
            dt_R18.HST_SHA_ADDRESS2 = dtPtr18.HST_SHA_ADDRESS2;
            //排出事業者所在地3
            dt_R18.HST_SHA_ADDRESS3 = dtPtr18.HST_SHA_ADDRESS3;
            //排出事業者所在地4
            dt_R18.HST_SHA_ADDRESS4 = dtPtr18.HST_SHA_ADDRESS4;
            //排出事業者の代表番号
            dt_R18.HST_SHA_TEL = dtPtr18.HST_SHA_TEL;
            //排出事業者の代表FAX
            dt_R18.HST_SHA_FAX = dtPtr18.HST_SHA_FAX;
            //排出事業場名称
            dt_R18.HST_JOU_NAME = dtPtr18.HST_JOU_NAME;
            //排出事業場所在地の郵便番号
            dt_R18.HST_JOU_POST_NO = dtPtr18.HST_JOU_POST_NO;
            //排出事業場所在地1
            dt_R18.HST_JOU_ADDRESS1 = dtPtr18.HST_JOU_ADDRESS1;
            //排出事業場所在地2
            dt_R18.HST_JOU_ADDRESS2 = dtPtr18.HST_JOU_ADDRESS2;
            //排出事業場所在地3
            dt_R18.HST_JOU_ADDRESS3 = dtPtr18.HST_JOU_ADDRESS3;
            //排出事業場所在地4
            dt_R18.HST_JOU_ADDRESS4 = dtPtr18.HST_JOU_ADDRESS4;
            //排出事業場電話番号
            dt_R18.HST_JOU_TEL = dtPtr18.HST_JOU_TEL;
            //登録担当者
            dt_R18.REGI_TAN = dtPtr18.REGI_TAN;
            //引渡し担当者
            dt_R18.HIKIWATASHI_TAN_NAME = dtPtr18.HIKIWATASHI_TAN_NAME;
            //大分類コード
            dt_R18.HAIKI_DAI_CODE = dtPtr18.HAIKI_DAI_CODE;
            //中分類コード
            dt_R18.HAIKI_CHU_CODE = dtPtr18.HAIKI_CHU_CODE;
            //小分類コード
            dt_R18.HAIKI_SHO_CODE = dtPtr18.HAIKI_SHO_CODE;
            //細分類コード
            dt_R18.HAIKI_SAI_CODE = dtPtr18.HAIKI_SAI_CODE;
            //廃棄物の大分類名称
            dt_R18.HAIKI_BUNRUI = dtPtr18.HAIKI_BUNRUI;
            //廃棄物の種類
            dt_R18.HAIKI_SHURUI = dtPtr18.HAIKI_SHURUI;
            //廃棄物の名称
            dt_R18.HAIKI_NAME = dtPtr18.HAIKI_NAME;
            //廃棄物の数量
            dt_R18.HAIKI_SUU = dtPtr18.HAIKI_SUU;
            //廃棄物の数量単位コード
            dt_R18.HAIKI_UNIT_CODE = dtPtr18.HAIKI_UNIT_CODE;
            //数量確定者コード
            dt_R18.SUU_KAKUTEI_CODE = dtPtr18.SUU_KAKUTEI_CODE;
            //廃棄物の確定数量
            dt_R18.HAIKI_KAKUTEI_SUU = dtPtr18.HAIKI_KAKUTEI_SUU;
            //廃棄物の確定数量の単位コード
            dt_R18.HAIKI_KAKUTEI_UNIT_CODE = dtPtr18.HAIKI_KAKUTEI_UNIT_CODE;
            //荷姿コード
            dt_R18.NISUGATA_CODE = dtPtr18.NISUGATA_CODE;
            //荷姿名
            dt_R18.NISUGATA_NAME = dtPtr18.NISUGATA_NAME;
            //荷姿の数量
            dt_R18.NISUGATA_SUU = dtPtr18.NISUGATA_SUU;
            //処分業者加入者番号
            dt_R18.SBN_SHA_MEMBER_ID = dtPtr18.SBN_SHA_MEMBER_ID;
            //処分業者名
            dt_R18.SBN_SHA_NAME = dtPtr18.SBN_SHA_NAME;
            //処分業者郵便番号
            dt_R18.SBN_SHA_POST = dtPtr18.SBN_SHA_POST;
            //処分業者所在地1
            dt_R18.SBN_SHA_ADDRESS1 = dtPtr18.SBN_SHA_ADDRESS1;
            //処分業者所在地2
            dt_R18.SBN_SHA_ADDRESS2 = dtPtr18.SBN_SHA_ADDRESS2;
            //処分業者所在地3
            dt_R18.SBN_SHA_ADDRESS3 = dtPtr18.SBN_SHA_ADDRESS3;
            //処分業者所在地4
            dt_R18.SBN_SHA_ADDRESS4 = dtPtr18.SBN_SHA_ADDRESS4;
            //処分業者電話番号
            dt_R18.SBN_SHA_TEL = dtPtr18.SBN_SHA_TEL;
            //処分業者FAX
            dt_R18.SBN_SHA_FAX = dtPtr18.SBN_SHA_FAX;
            //処分業者統一許可番号
            dt_R18.SBN_SHA_KYOKA_ID = dtPtr18.SBN_SHA_KYOKA_ID;
            //再委託先処分業者加入者番号
            dt_R18.SAI_SBN_SHA_MEMBER_ID = dtPtr18.SAI_SBN_SHA_MEMBER_ID;
            //再委託先処分業者名
            dt_R18.SAI_SBN_SHA_NAME = dtPtr18.SAI_SBN_SHA_NAME;
            //再委託先処分業者郵便場号
            dt_R18.SAI_SBN_SHA_POST = dtPtr18.SAI_SBN_SHA_POST;
            //再委託先処分業者所在地1
            dt_R18.SAI_SBN_SHA_ADDRESS1 = dtPtr18.SAI_SBN_SHA_ADDRESS1;
            //再委託先処分業者所在地2
            dt_R18.SAI_SBN_SHA_ADDRESS2 = dtPtr18.SAI_SBN_SHA_ADDRESS2;
            //再委託先処分業者所在地3
            dt_R18.SAI_SBN_SHA_ADDRESS3 = dtPtr18.SAI_SBN_SHA_ADDRESS3;
            //再委託先処分業者所在地4
            dt_R18.SAI_SBN_SHA_ADDRESS4 = dtPtr18.SAI_SBN_SHA_ADDRESS4;
            //再委託先処分業者電話番号
            dt_R18.SAI_SBN_SHA_TEL = dtPtr18.SAI_SBN_SHA_TEL;
            //再委託先処分業者FAX
            dt_R18.SAI_SBN_SHA_FAX = dtPtr18.SAI_SBN_SHA_FAX;
            //再委託先処分業者統一許可番号
            dt_R18.SAI_SBN_SHA_KYOKA_ID = dtPtr18.SAI_SBN_SHA_KYOKA_ID;
            //処分方法コード
            dt_R18.SBN_WAY_CODE = dtPtr18.SBN_WAY_CODE.ToSqlInt16();
            //処分方法名
            dt_R18.SBN_WAY_NAME = dtPtr18.SBN_WAY_NAME;
            //処分報告情報承認待ちフラグ
            dt_R18.SBN_SHOUNIN_FLAG = dtPtr18.SBN_SHOUNIN_FLAG.ToSqlInt16();
            //手動の場合はコピーする、自動場合は何もしない
            if (this.form.cntxt_InputKBN.Text == "2")
            {
                //処分終了日
                dt_R18.SBN_END_DATE = dtPtr18.SBN_END_DATE;
                //廃棄物の受領日
                dt_R18.HAIKI_IN_DATE = dtPtr18.HAIKI_IN_DATE;
                //受入量
                dt_R18.RECEPT_SUU = dtPtr18.RECEPT_SUU;
                //受入量の単位コード
                dt_R18.RECEPT_UNIT_CODE = dtPtr18.RECEPT_UNIT_CODE;
                //運搬担当者
                dt_R18.UPN_TAN_NAME = dtPtr18.UPN_TAN_NAME;
                //車両番号
                dt_R18.CAR_NO = dtPtr18.CAR_NO;
                //報告担当者
                dt_R18.REP_TAN_NAME = dtPtr18.REP_TAN_NAME;
                //処分担当者
                dt_R18.SBN_TAN_NAME = dtPtr18.SBN_TAN_NAME;
                //処分終了報告日
                dt_R18.SBN_END_REP_DATE = dtPtr18.SBN_END_REP_DATE;
                //処分報告備考
                dt_R18.SBN_REP_BIKOU = dtPtr18.SBN_REP_BIKOU;
            }
            //予約登録の修正権限コード
            dt_R18.KENGEN_CODE = dtPtr18.KENGEN_CODE.ToSqlInt16();
            //最終処分事業場記載フラグ
            dt_R18.LAST_SBN_JOU_KISAI_FLAG = dtPtr18.LAST_SBN_JOU_KISAI_FLAG;
            //中間処理産業廃棄物情報管理方法フラグ
            dt_R18.FIRST_MANIFEST_FLAG = dtPtr18.FIRST_MANIFEST_FLAG;
            //最終処分終了日
            dt_R18.LAST_SBN_END_DATE = dtPtr18.LAST_SBN_END_DATE;
            //最終処分終了報告日
            dt_R18.LAST_SBN_END_REP_DATE = dtPtr18.LAST_SBN_END_REP_DATE;
            //修正日
            dt_R18.SHUSEI_DATE = dtPtr18.SHUSEI_DATE;
            //取消フラグ
            dt_R18.CANCEL_FLAG = dtPtr18.CANCEL_FLAG.ToSqlInt16();
            //取消日
            dt_R18.CANCEL_DATE = dtPtr18.CANCEL_DATE;
            //最終更新日
            dt_R18.LAST_UPDATE_DATE = dtPtr18.LAST_UPDATE_DATE;
            //有害物質情報件数
            dt_R18.YUUGAI_CNT = dtPtr18.YUUGAI_CNT.ToSqlInt16();
            //収集運搬情報件数
            dt_R18.UPN_ROUTE_CNT = dtPtr18.UPN_ROUTE_CNT.ToSqlInt16();
            //最終処分事業場（予定）情報件数
            dt_R18.LAST_SBN_PLAN_CNT = dtPtr18.LAST_SBN_PLAN_CNT.ToSqlInt16();
            //最終処分終了日･事業場情報件数
            dt_R18.LAST_SBN_CNT = dtPtr18.LAST_SBN_CNT.ToSqlInt16();
            //連絡番号情報件数
            dt_R18.RENRAKU_CNT = dtPtr18.RENRAKU_CNT.ToSqlInt16();
            //備考情報件数
            dt_R18.BIKOU_CNT = dtPtr18.BIKOU_CNT.ToSqlInt16();
            //中間処理産業廃棄物情報件数
            dt_R18.FIRST_MANIFEST_CNT = dtPtr18.FIRST_MANIFEST_CNT.ToSqlInt16();
            //排出事業者CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.HST_GYOUSHA_CD;
            //排出事業場CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.HST_GENBA_CD;
            //処分受託者CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.SBN_GYOUSHA_CD;
            //処分事業場CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.SBN_GENBA_CD;
            //報告不要処分事業者加入者番号(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.NO_REP_SBN_EDI_MEMBER_ID;
            //処分受託者許可番号(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.SBN_KYOKA_NO;
            //廃棄物名称CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.HAIKI_NAME_CD;
            //処分方法CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.SBN_HOUHOU_CD;
            //報告担当者CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.HOUKOKU_TANTOUSHA_CD;
            //処分担当者CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.SBN_TANTOUSHA_CD;
            //運搬担当者CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.UPN_TANTOUSHA_CD;
            //車輌CD(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.SHARYOU_CD;
            //換算後数量(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.KANSAN_SUU;
            //作成者(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.CREATE_USER;
            //作成日時(DT_R18項目なし)
            dt_R18.CREATE_DATE = dtPtr18.CREATE_DATE;
            //作成PC(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.CREATE_PC;
            //最終更新者(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.UPDATE_USER;
            //最終更新日時(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.UPDATE_DATE;
            //最終更新PC(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.UPDATE_PC;
            //削除フラグ(DT_R18項目なし)
            //dt_R18.0 = dtPtr18.DELETE_FLG;
            //タイムスタンプ
            //dt_R18.UPDATE_TS = dtPtr18.TIME_STAMP;

            return dt_R18;
        }
        ///電子マニフェストパターン→電子マニフェスト拡展
        private DT_R18_EX SetDT_R18EX(DT_PT_R18 dtPtr18)
        {
            if (dtPtr18 == null) return null;
            DT_R18_EX dt_R18EX = new DT_R18_EX();
            //システムID(PK)
            //dt_R18EX.KANRI_ID = dtPtr18.SYSTEM_ID;
            //枝番(PK)
            //dt_R18EX.SEQ = dtPtr18.SEQ;

            //管理番号(項目なし)
            //dt_R18EX.KANRI_ID = dtPtr18.KANRI_ID;

            //マニフェスト／予約番号
            dt_R18EX.MANIFEST_ID = dtPtr18.MANIFEST_ID;

            //排出事業者CD(DT_R18項目なし)
            dt_R18EX.HST_GYOUSHA_CD = dtPtr18.HST_GYOUSHA_CD;
            //排出事業場CD(DT_R18項目なし)
            dt_R18EX.HST_GENBA_CD = dtPtr18.HST_GENBA_CD;
            //処分受託者CD(DT_R18項目なし)
            dt_R18EX.SBN_GYOUSHA_CD = dtPtr18.SBN_GYOUSHA_CD;
            //処分事業場CD(DT_R18項目なし)
            dt_R18EX.SBN_GENBA_CD = dtPtr18.SBN_GENBA_CD;
            //報告不要処分事業者加入者番号(DT_R18項目なし)
            dt_R18EX.NO_REP_SBN_EDI_MEMBER_ID = dtPtr18.NO_REP_SBN_EDI_MEMBER_ID;
            //処分受託者許可番号(DT_R18項目なし)
            //dt_R18EX.SBN_KYOKA_NO = dtPtr18.SBN_KYOKA_NO;
            //廃棄物名称CD(DT_R18項目なし)
            dt_R18EX.HAIKI_NAME_CD = dtPtr18.HAIKI_NAME_CD;
            //処分方法CD(DT_R18項目なし)
            dt_R18EX.SBN_HOUHOU_CD = dtPtr18.SBN_HOUHOU_CD;
            //報告担当者CD(DT_R18項目なし)
            dt_R18EX.HOUKOKU_TANTOUSHA_CD = dtPtr18.HOUKOKU_TANTOUSHA_CD;
            //処分担当者CD(DT_R18項目なし)
            dt_R18EX.SBN_TANTOUSHA_CD = dtPtr18.SBN_TANTOUSHA_CD;
            //運搬担当者CD(DT_R18項目なし)
            dt_R18EX.UPN_TANTOUSHA_CD = dtPtr18.UPN_TANTOUSHA_CD;
            //車輌CD(DT_R18項目なし)
            dt_R18EX.SHARYOU_CD = dtPtr18.SHARYOU_CD;
            //換算後数量(DT_R18項目なし)
            dt_R18EX.KANSAN_SUU = dtPtr18.KANSAN_SUU;
            //作成者(DT_R18項目なし)
            dt_R18EX.CREATE_USER = dtPtr18.CREATE_USER;
            //作成日時(DT_R18項目なし)
            dt_R18EX.CREATE_DATE = dtPtr18.CREATE_DATE;
            //作成PC(DT_R18項目なし)
            dt_R18EX.CREATE_PC = dtPtr18.CREATE_PC;
            //最終更新者(DT_R18項目なし)
            dt_R18EX.UPDATE_USER = dtPtr18.UPDATE_USER;
            //最終更新日時(DT_R18項目なし)
            dt_R18EX.UPDATE_DATE = dtPtr18.UPDATE_DATE;
            //最終更新PC(DT_R18項目なし)
            dt_R18EX.UPDATE_PC = dtPtr18.UPDATE_PC;
            //削除フラグ(DT_R18項目なし)
            dt_R18EX.DELETE_FLG = dtPtr18.DELETE_FLG;
            //タイムスタンプ
            //dt_R18EX.UPDATE_TS = dtPtr18.TIME_STAMP;
            return dt_R18EX;
        }
        ///収集運搬→電子マニフェストパターン収集運搬
        private DT_R19 SetDT_R19(DT_PT_R19 dtPtr19)
        {
            if (dtPtr19 == null) return null;
            DT_R19 dt_R19 = new DT_R19();
            //システムID(PK)
            //dt_R19.KANRI_ID = dtPtr19.SYSTEM_ID;
            //枝番(PK)
            //dt_R19.SEQ = dtPtr19.SEQ;
            //マニフェスト／予約番号
            dt_R19.MANIFEST_ID = dtPtr19.MANIFEST_ID;
            //区間番号(PK)
            //dt_R19.UPN_ROUTE_NO = dtPtr19.UPN_ROUTE_NO;
            //収集運搬業者加入者番号
            dt_R19.UPN_SHA_EDI_MEMBER_ID = dtPtr19.UPN_SHA_EDI_MEMBER_ID;
            //収集運搬業者名
            dt_R19.UPN_SHA_NAME = dtPtr19.UPN_SHA_NAME;
            //収集運搬業者郵便番号
            dt_R19.UPN_SHA_POST = dtPtr19.UPN_SHA_POST;
            //収集運搬業者所在地1
            dt_R19.UPN_SHA_ADDRESS1 = dtPtr19.UPN_SHA_ADDRESS1;
            //収集運搬業者所在地2
            dt_R19.UPN_SHA_ADDRESS2 = dtPtr19.UPN_SHA_ADDRESS2;
            //収集運搬業者所在地3
            dt_R19.UPN_SHA_ADDRESS3 = dtPtr19.UPN_SHA_ADDRESS3;
            //収集運搬業者所在地4
            dt_R19.UPN_SHA_ADDRESS4 = dtPtr19.UPN_SHA_ADDRESS4;
            //収集運搬業者電話番号
            dt_R19.UPN_SHA_TEL = dtPtr19.UPN_SHA_TEL;
            //収集運搬業者FAX
            dt_R19.UPN_SHA_FAX = dtPtr19.UPN_SHA_FAX;
            //収集運搬業者統一許可番号
            dt_R19.UPN_SHA_KYOKA_ID = dtPtr19.UPN_SHA_KYOKA_ID;
            //再委託先収集運搬業者加入者番号
            dt_R19.SAI_UPN_SHA_EDI_MEMBER_ID = dtPtr19.SAI_UPN_SHA_EDI_MEMBER_ID;
            //再委託先収集運搬業者名
            dt_R19.SAI_UPN_SHA_NAME = dtPtr19.SAI_UPN_SHA_NAME;
            //再委託先収集運搬業者郵便番号
            dt_R19.SAI_UPN_SHA_POST = dtPtr19.SAI_UPN_SHA_POST;
            //再委託先収集運搬業者所在地1
            dt_R19.SAI_UPN_SHA_ADDRESS1 = dtPtr19.SAI_UPN_SHA_ADDRESS1;
            //再委託先収集運搬業者所在地2
            dt_R19.SAI_UPN_SHA_ADDRESS2 = dtPtr19.SAI_UPN_SHA_ADDRESS2;
            //再委託先収集運搬業者所在地3
            dt_R19.SAI_UPN_SHA_ADDRESS3 = dtPtr19.SAI_UPN_SHA_ADDRESS3;
            //再委託先収集運搬業者所在地4
            dt_R19.SAI_UPN_SHA_ADDRESS4 = dtPtr19.SAI_UPN_SHA_ADDRESS4;
            //再委託先収集運搬業者電話番号
            dt_R19.SAI_UPN_SHA_TEL = dtPtr19.SAI_UPN_SHA_TEL;
            //再委託先収集運搬業者FAX
            dt_R19.SAI_UPN_SHA_FAX = dtPtr19.SAI_UPN_SHA_FAX;
            //再委託先収集運搬業者統一許可番号
            dt_R19.SAI_UPN_SHA_KYOKA_ID = dtPtr19.SAI_UPN_SHA_KYOKA_ID;
            //運搬方法コード
            dt_R19.UPN_WAY_CODE = dtPtr19.UPN_WAY_CODE;
            //運搬担当者
            dt_R19.UPN_TAN_NAME = dtPtr19.UPN_TAN_NAME;
            //車両番号
            dt_R19.CAR_NO = dtPtr19.CAR_NO;
            //運搬先加入者番号
            dt_R19.UPNSAKI_EDI_MEMBER_ID = dtPtr19.UPNSAKI_EDI_MEMBER_ID;
            //運搬先加入者名
            dt_R19.UPNSAKI_NAME = dtPtr19.UPNSAKI_NAME;
            //運搬先事業場番号
            dt_R19.UPNSAKI_JOU_ID = dtPtr19.UPNSAKI_JOU_ID.ToSqlInt16();
            //運搬先事業場区分
            dt_R19.UPNSAKI_JOU_KBN = dtPtr19.UPNSAKI_JOU_KBN.ToSqlInt16();
            //運搬先事業場名
            dt_R19.UPNSAKI_JOU_NAME = dtPtr19.UPNSAKI_JOU_NAME;
            //運搬先事業場郵便番号
            dt_R19.UPNSAKI_JOU_POST = dtPtr19.UPNSAKI_JOU_POST;
            //運搬先事業場所在地1
            dt_R19.UPNSAKI_JOU_ADDRESS1 = dtPtr19.UPNSAKI_JOU_ADDRESS1;
            //運搬先事業場所在地2
            dt_R19.UPNSAKI_JOU_ADDRESS2 = dtPtr19.UPNSAKI_JOU_ADDRESS2;
            //運搬先事業場所在地3
            dt_R19.UPNSAKI_JOU_ADDRESS3 = dtPtr19.UPNSAKI_JOU_ADDRESS3;
            //運搬先事業場所在地4
            dt_R19.UPNSAKI_JOU_ADDRESS4 = dtPtr19.UPNSAKI_JOU_ADDRESS4;
            //運搬先事業場電話番号
            dt_R19.UPNSAKI_JOU_TEL = dtPtr19.UPNSAKI_JOU_TEL;
            //運搬報告情報承認待ちフラグ
            dt_R19.UPN_SHOUNIN_FLAG = dtPtr19.UPN_SHOUNIN_FLAG.ToSqlInt16();
            //手動の場合はコピーする、自動場合は何もしない
            if (this.form.cntxt_InputKBN.Text == "2")
            {
                //運搬終了日
                dt_R19.UPN_END_DATE = dtPtr19.UPN_END_DATE;
                //運搬報告記載の運搬担当者
                dt_R19.UPNREP_UPN_TAN_NAME = dtPtr19.UPNREP_UPN_TAN_NAME;
                //運搬報告記載の車両番号
                dt_R19.UPNREP_CAR_NO = dtPtr19.UPNREP_CAR_NO;
                //運搬量
                dt_R19.UPN_SUU = dtPtr19.UPN_SUU;
                //運搬量の単位コード
                dt_R19.UPN_UNIT_CODE = dtPtr19.UPN_UNIT_CODE;
                //有価物拾集量
                dt_R19.YUUKA_SUU = dtPtr19.YUUKA_SUU;
                //有価物拾集量の単位コード
                dt_R19.YUUKA_UNIT_CODE = dtPtr19.YUUKA_UNIT_CODE;
                //報告担当者
                dt_R19.REP_TAN_NAME = dtPtr19.REP_TAN_NAME;
                //備考
                dt_R19.BIKOU = dtPtr19.BIKOU;
            }
            //収集運搬業者CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.UPN_GYOUSHA_CD;
            //報告不要収集運搬業者加入者番号(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.NO_REP_UPN_EDI_MEMBER_ID;
            //運搬先業者CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.UPNSAKI_GYOUSHA_CD;
            //報告不要運搬先業者加入者番号(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.NO_REP_UPNSAKI_EDI_MEMBER_ID;
            //運搬先事業場CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.UPNSAKI_GENBA_CD;
            //運搬担当者CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.UPN_TANTOUSHA_CD;
            //車輌CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.SHARYOU_CD;
            //運搬報告記載の運搬担当者CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.UPNREP_UPN_TANTOUSHA_CD;
            //運搬報告記載の車輌CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.UPNREP_SHARYOU_CD;
            //報告担当者CD(DT_R19項目なし)
            //dt_R19.0 = dtPtr19.HOUKOKU_TANTOUSHA_CD;
            //レコード作成日時
            dt_R19.CREATE_DATE = dtPtr19.CREATE_DATE;
            //タイムスタンプ
            //dt_R19.UPDATE_TS = dtPtr19.TIME_STAMP;

            return dt_R19;
        }

        ///収集運搬→電子マニフェストパターン収集運搬
        private DT_R19_EX SetDT_R19EX(DT_PT_R19 dtPtr19)
        {
            if (dtPtr19 == null) return null;
            DT_R19_EX dt_R19EX = new DT_R19_EX();
            //システムID(PK)
            //dt_R19.KANRI_ID = dtPtr19.SYSTEM_ID;
            //枝番(PK)
            //dt_R19.SEQ = dtPtr19.SEQ;
            //管理番号
            //dt_R19EX.KANRI_ID = dtPtr19.KANRI_ID;
            //マニフェスト／予約番号
            dt_R19EX.MANIFEST_ID = dtPtr19.MANIFEST_ID;
            //区間番号(PK)
            //dt_R19.UPN_ROUTE_NO = dtPtr19.UPN_ROUTE_NO;

            //収集運搬業者CD(DT_R19項目なし)
            dt_R19EX.UPN_GYOUSHA_CD = dtPtr19.UPN_GYOUSHA_CD;
            //報告不要収集運搬業者加入者番号(DT_R19項目なし)
            dt_R19EX.NO_REP_UPN_EDI_MEMBER_ID = dtPtr19.UPN_SHA_EDI_MEMBER_ID;
            //運搬先業者CD(DT_R19項目なし)
            dt_R19EX.UPNSAKI_GYOUSHA_CD = dtPtr19.UPNSAKI_GYOUSHA_CD;
            //報告不要運搬先業者加入者番号(DT_R19項目なし)
            dt_R19EX.NO_REP_UPNSAKI_EDI_MEMBER_ID = dtPtr19.NO_REP_UPNSAKI_EDI_MEMBER_ID;
            //運搬先事業場CD(DT_R19項目なし)
            dt_R19EX.UPNSAKI_GENBA_CD = dtPtr19.UPNSAKI_GENBA_CD;
            //運搬担当者CD(DT_R19項目なし)
            dt_R19EX.UPN_TANTOUSHA_CD = dtPtr19.UPN_TANTOUSHA_CD;
            //車輌CD(DT_R19項目なし)
            dt_R19EX.SHARYOU_CD = dtPtr19.SHARYOU_CD;
            //運搬報告記載の運搬担当者CD(DT_R19項目なし)
            dt_R19EX.UPNREP_UPN_TANTOUSHA_CD = dtPtr19.UPNREP_UPN_TANTOUSHA_CD;
            //運搬報告記載の車輌CD(DT_R19項目なし)
            dt_R19EX.UPNREP_SHARYOU_CD = dtPtr19.UPNREP_SHARYOU_CD;
            //報告担当者CD(DT_R19項目なし)
            dt_R19EX.HOUKOKU_TANTOUSHA_CD = dtPtr19.HOUKOKU_TANTOUSHA_CD;
            //レコード作成日時
            dt_R19EX.CREATE_DATE = dtPtr19.CREATE_DATE;
            //削除フラグ
            //dt_R19EX.DELETE_FLG = dtPtr19.DELETE_FLG;
            //タイムスタンプ
            //dt_R19.UPDATE_TS = dtPtr19.TIME_STAMP;

            return dt_R19EX;
        }
        #endregion DBからPattern呼ぶEntityの作成 終了

        /// <summary>
        /// パタン呼出処理
        /// </summary>
        public void LoadPatternData(object sender, EventArgs e)
        {
            if (this.CallPattern())
            {
                this.GetPatternInfo();
            }
        }
        /// <summary>
        /// パタン情報ロードるす
        /// </summary>
        private bool GetPatternInfo()
        {
            try
            {
                this.ManiPtnInfo = this.GetManifestPatternInfoFromDB(this.Ptn_System_ID, this.Ptn_SEQ);
                if (this.ManiPtnInfo != null)
                {
                    //パタン情報からマニ情報にコピーする
                    DenshiManifestInfoCls ManiInfoFromPtn = new DenshiManifestInfoCls();
                    //コピー処理
                    ManiInfoFromPtn = this.CopyManiPtnInfoToManiInfo(this.ManiPtnInfo);
                    //対象を画面に設定
                    this.form.SetFormFromEntity(ManiInfoFromPtn);
                    //画面に一次/二次状態の設定処理を行う
                    if (!this.SetManifestForm("PtLoad"))
                    {
                        return false;
                    }
                    //フォーカスの設定
                    this.form.cntxt_ManiKBN.Focus();
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPatternInfo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPatternInfo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }
        /// <summary>
        /// パターン呼出
        /// </summary>
        public virtual bool CallPattern()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;
            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            string[] useInfo = new string[] { string.Empty };
            //var callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.DENSHI_MANI_PATTERN_ICHIRAN, "0", "4");
            var callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.DENSHI_MANI_PATTERN_ICHIRAN, "0", "4", this.maniFlag.ToString());
            //var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader();
            var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader("4", useInfo);
            // 20140529 syunrei No.730 マニフェストパターン一覧 end

            var businessForm = new BusinessBaseForm(callForm, callHeader);
            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                businessForm.ShowDialog();
            }
            if (callForm.ParamOut_SysID != string.Empty)
            {
                this.Ptn_System_ID = callForm.ParamOut_SysID;
                this.Ptn_SEQ = callForm.ParamOut_Seq;
                ret = true;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }
        /// <summary>
        /// パタン登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SavePatternData(object sender, EventArgs e)
        {

            //【中間処理産業廃棄物-マニフェスト番号／交付】がセットされたレコードが1件もない場合
            //はエラーメッセージを表示し登録不可
            if (!this.ChkRegistFirstManifest())
            {
                this.msgLogic.MessageBoxShow("E001", "中間処理産業廃棄物");
                return;
            }

            //画面からマニ情報に設定
            bool catchErr = false;
            DenshiManifestInfoCls ManiInfo = this.form.MakeAllData(out catchErr);
            if (catchErr)
            {
                return;
            }
            //拡張データ作成
            ManiInfo.dt_r18ExOld = this.CreateDT_R18ExEntity(true, ManiInfo.dt_r18);
            ManiInfo.lstDT_R19_EX = this.CreateDT_R19ExEntityList(ManiInfo.dt_r18ExOld);
            ManiInfo.lstDT_R04_EX = this.CreateDT_R04ExEntityList(ManiInfo.dt_r18ExOld);
            ManiInfo.lstDT_R13_EX = this.CreateDT_R13ExEntityList(ManiInfo.dt_r18ExOld);

            //マニ情報からパタン情報に変換
            DenshiManifestPatternInfoCls ManiPtnInfo = this.CopyManiInfoToPtnInfo(ManiInfo);
            List<DT_PT_R18> entrylist = new List<DT_PT_R18>();
            entrylist.Add(ManiPtnInfo.dt_PT_R18);

            Int32 firstManifestKbn = 0;

            if (this.maniFlag == 2)
            {
                firstManifestKbn = 1;
            }

            //パタン新規登録
            var callForm = new Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.DenshiManifestPatternTouroku(
                firstManifestKbn,
                entrylist,
                ManiPtnInfo.lstDT_PT_R19,
                ManiPtnInfo.lstDT_PT_R13,
                ManiPtnInfo.lstDT_PT_R06,
                ManiPtnInfo.lstDT_PT_R05,
                ManiPtnInfo.lstDT_PT_R04,
                ManiPtnInfo.lstDT_PT_R02,
                ManiPtnInfo.lstDT_PT_R18_EX
                );

            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                callForm.ShowDialog();
                //パターン登録結果を保存。次読み出し時には最新情報で再表示。
                if (!callForm.RegistedSystemId.IsNull)
                {
                    //パタン情報再設定
                    this.Ptn_System_ID = callForm.RegistedSystemId.ToString();
                    this.Ptn_SEQ = callForm.RegistedSeq.ToString();
                    //this.GetPatternInfo(); //登録は呼び出し不要
                }
            }

        }

        #region 実現必須メソッド
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion


        /// <summary>
        /// 受渡確認票印刷処理
        /// </summary>
        public bool UkewatashiKakuninHyouPrint()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //修正モード時のみ使用可
                if (!WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.Mode))
                {
                    return true;
                }

                UkewatashiKakuninHyouLogic Logic = new UkewatashiKakuninHyouLogic();

                Logic.UkewatashiKakuninHyouPrint(this.KanriId);

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UkewatashiKakuninHyouPrint", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UkewatashiKakuninHyouPrint", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 1次/2次マニフェスト設定
        /// </summary>
        public virtual bool SetManifestForm(String Kbn)
        {
            try
            {
                LogUtility.DebugMethodStart();

                //新規モードの場合、メッセージを表示
                if ((WINDOW_TYPE.NEW_WINDOW_FLAG).Equals(this.Mode) && Kbn == "F4")
                {
                    DialogResult dialogResult = this.msgLogic.MessageBoxShow("C053");

                    //[N]の場合
                    if (!DialogResult.Yes.Equals(dialogResult))
                    {
                        //処理を中止する
                        return true;
                    }

                    if (this.form.cntxt_InputKBN.Text == "1")
                    {
                        //自動
                        if (!this.form.SetControlReadOnlyByInput_KBN(true, true))
                        {
                            return false;
                        }
                    }
                    else if (this.form.cntxt_InputKBN.Text == "2")
                    {
                        //手動
                        if (!this.form.SetControlReadOnlyByInput_KBN(false, true))
                        {
                            return false;
                        }
                    }

                    //発行件数の初期化
                    this.form.cantxt_HakkouCnt.Text = "1";
                }

                //タイトル
                string strTitelName = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_DENSHI_MANIFEST);

                //背景色
                Color BackColor = Color.FromArgb(0, 105, 51);

                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "Non"://初期化起動
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                strTitelName = strTitelName + "一次";
                                break;

                            case "F4"://[F4]で切り替え
                                strTitelName = strTitelName + "二次";
                                BackColor = Color.FromArgb(0, 51, 160);
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

                //process3ボタンの活性
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        this.parentbaseform.bt_process3.Text = "[3]1次マニ紐付";
                                        this.parentbaseform.bt_process3.Tag = "マニフェスト紐付画面に切り替わります";
                                        this.parentbaseform.bt_process3.Enabled = true;
                                        break;

                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        this.parentbaseform.bt_process3.Text = string.Empty;
                                        this.parentbaseform.bt_process3.Enabled = false;
                                        this.parentbaseform.bt_process3.Tag = string.Empty;
                                        break;

                                }

                                //this.form.cdgv
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
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        this.parentbaseform.bt_process3.Text = "[3]1次マニ紐付";
                                        this.parentbaseform.bt_process3.Tag = "マニフェスト紐付画面に切り替わります";
                                        this.parentbaseform.bt_process3.Enabled = true;
                                        break;

                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        parentbaseform.bt_process3.Text = string.Empty;
                                        parentbaseform.bt_process3.Enabled = false;
                                        break;

                                }
                                break;
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        this.parentbaseform.bt_process3.Text = "[3]1次マニ紐付";
                                        this.parentbaseform.bt_process3.Tag = "マニフェスト紐付画面に切り替わります";
                                        this.parentbaseform.bt_process3.Enabled = true;
                                        break;

                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                        parentbaseform.bt_process3.Text = string.Empty;
                                        parentbaseform.bt_process3.Enabled = false;
                                        break;

                                }
                                break;
                        }
                        break;
                }

                //process4のボタン活性制御
                switch (maniFlag)
                {
                    case 1://１次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        this.parentbaseform.bt_process4.Text = "[4]最終処分終了報告";
                                        this.parentbaseform.bt_process4.Enabled = true;
                                        this.parentbaseform.bt_process5.Text = "[5]最終処分終了取消";
                                        this.parentbaseform.bt_process5.Enabled = true;
                                        break;

                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                        this.parentbaseform.bt_process4.Text = string.Empty;
                                        this.parentbaseform.bt_process4.Enabled = false;
                                        this.parentbaseform.bt_process5.Text = string.Empty;
                                        this.parentbaseform.bt_process5.Enabled = false;
                                        break;

                                }
                                maniFlag = 2;
                                break;

                            case "Non"://初期化起動
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                        this.parentbaseform.bt_process4.Text = string.Empty;
                                        this.parentbaseform.bt_process4.Enabled = false;
                                        this.parentbaseform.bt_process5.Text = string.Empty;
                                        this.parentbaseform.bt_process5.Enabled = false;
                                        break;

                                }
                                break;
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                this.parentbaseform.bt_process4.Text = string.Empty;
                                this.parentbaseform.bt_process4.Enabled = false;
                                this.parentbaseform.bt_process5.Text = string.Empty;
                                this.parentbaseform.bt_process5.Enabled = false;
                                maniFlag = 1;
                                break;
                        }
                        break;

                    case 2://２次マニフェスト
                        switch (Kbn)
                        {
                            case "F4"://[F4]で切り替え
                                this.parentbaseform.bt_process4.Text = string.Empty;
                                this.parentbaseform.bt_process4.Enabled = false;
                                this.parentbaseform.bt_process5.Text = string.Empty;
                                this.parentbaseform.bt_process5.Enabled = false;
                                maniFlag = 1;
                                break;

                            case "Non"://初期化起動
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        this.parentbaseform.bt_process4.Text = "[4]最終処分終了報告";
                                        this.parentbaseform.bt_process4.Enabled = true;
                                        this.parentbaseform.bt_process5.Text = "[5]最終処分終了取消";
                                        this.parentbaseform.bt_process5.Enabled = true;
                                        break;

                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                        this.parentbaseform.bt_process4.Text = string.Empty;
                                        this.parentbaseform.bt_process4.Enabled = false;
                                        this.parentbaseform.bt_process5.Text = string.Empty;
                                        this.parentbaseform.bt_process5.Enabled = false;
                                        break;

                                }
                                break;
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                switch (this.Mode)
                                {
                                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                        this.parentbaseform.bt_process4.Text = "[4]最終処分終了報告";
                                        this.parentbaseform.bt_process4.Enabled = true;
                                        this.parentbaseform.bt_process5.Text = "[5]最終処分終了取消";
                                        this.parentbaseform.bt_process5.Enabled = true;
                                        break;

                                    case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                                    case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード
                                        this.parentbaseform.bt_process4.Text = string.Empty;
                                        this.parentbaseform.bt_process4.Enabled = false;
                                        this.parentbaseform.bt_process5.Text = string.Empty;
                                        this.parentbaseform.bt_process5.Enabled = false;
                                        break;
                                }
                                maniFlag = 2;
                                break;

                        }
                        break;
                }

                //20150611 #1120 ２次の電子マニフェストを開いている時は[F4]１次マニとする。hoanghm start
                bool enableButtonF4 = this.parentbaseform.bt_func4.Enabled;
                switch (maniFlag)
                {
                    case 1:
                        switch (Kbn)
                        {
                            case "Non"://初期化起動
                                this.parentbaseform.bt_func4.Text = "[F4]" + Environment.NewLine + "2次マニ";
                                this.parentbaseform.bt_func4.Enabled = enableButtonF4;
                                break;
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                break;
                            case "F4"://[F4]で切り替え
                                this.parentbaseform.bt_func4.Text = "[F4]" + Environment.NewLine + "2次マニ";
                                this.parentbaseform.bt_func4.Enabled = enableButtonF4;
                                break;
                        }
                        break;
                    case 2:
                        switch (Kbn)
                        {
                            case "Non"://初期化起動                            
                                this.parentbaseform.bt_func4.Text = "[F4]" + Environment.NewLine + "1次マニ";
                                this.parentbaseform.bt_func4.Enabled = enableButtonF4;
                                break;
                            case "DataLoad"://データロード
                            case "PtLoad"://パターンロード
                                break;
                            case "F4"://[F4]で切り替え
                                this.parentbaseform.bt_func4.Text = "[F4]" + Environment.NewLine + "1次マニ";
                                this.parentbaseform.bt_func4.Enabled = enableButtonF4;
                                break;
                        }
                        break;
                }
                //20150611 #1120 ２次の電子マニフェストを開いている時は[F4]１次マニとする。hoanghm end

                if (AppConfig.IsManiLite)
                {
                    // マニライトの場合、二次マニは無し
                    this.parentbaseform.bt_func4.Text = string.Empty;
                    this.parentbaseform.bt_func4.Enabled = false;
                }

                //産業廃棄物グリッドのチェックとグリッドの編集
                //産業廃棄物-減容後数量と排出事業者区分の条件を設定
                switch (maniFlag)
                {
                    case 1://１次マニフェスト

                        this.form.ccbx_Touranshitei.Enabled = false;
                        this.form.ccbx_ItijiFuyou.Enabled = false;
                        this.form.ccbx_ChouboKisai.Enabled = false;
                        this.form.ccbx_Touranshitei.Checked = false;
                        this.form.ccbx_ItijiFuyou.Checked = false;
                        this.form.ccbx_ChouboKisai.Checked = false;

                        this.form.cdgv_Tyukanshori.AllowUserToAddRows = true;
                        //全て列編集不可を設定する
                        this.form.SetTyukanshoriReadOnly();

                        //部分列編集不可を設定する
                        this.form.SetTyukanshoriReadOnly(true);

                        //産業廃棄物-減容後数量
                        this.form.cdgv_Haikibutu.Columns["GENNYOU_SUU"].Visible = true;
                        //排出事業者区分の条件の設定を使用
                        this.form.cantxt_HaisyutuGyousyaCd.HST_KBN = true;
                        this.form.cantxt_HaisyutuGyousyaCd.SBN_KBN = false;
                        this.form.cantxt_HaisyutuGenbaCd.HST_KBN = true;
                        this.form.cantxt_HaisyutuGenbaCd.SBN_KBN = false;
                        this.form.cantxt_HaisyutuGenbaCd.JIGYOUJOU_KBN = "1";

                        break;

                    case 2://２次マニフェスト
                        switch (this.Mode)
                        {
                            case WINDOW_TYPE.DELETE_WINDOW_FLAG://削除モード
                            case WINDOW_TYPE.REFERENCE_WINDOW_FLAG://参照モード
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG://修正モード
                                this.form.ccbx_Touranshitei.Enabled = true;
                                this.form.ccbx_ItijiFuyou.Enabled = true;
                                this.form.ccbx_ChouboKisai.Enabled = true;
                                this.form.ccbx_Touranshitei.Checked = false;
                                this.form.ccbx_ItijiFuyou.Checked = false;
                                this.form.ccbx_ChouboKisai.Checked = false;

                                this.form.cdgv_Tyukanshori.AllowUserToAddRows = true;
                                //全て列編集不可を設定する
                                this.form.SetTyukanshoriReadOnly();

                                //部分列編集不可を設定する
                                this.form.SetTyukanshoriReadOnly(true);

                                break;
                            case WINDOW_TYPE.NEW_WINDOW_FLAG://新規モード

                                this.form.ccbx_Touranshitei.Enabled = true;

                                if ("1".Equals(this.form.cntxt_ManiKBN.Text))
                                {
                                    this.form.ccbx_ItijiFuyou.Enabled = false;
                                }
                                else
                                {
                                    this.form.ccbx_ItijiFuyou.Enabled = true;
                                }

                                this.form.ccbx_ChouboKisai.Enabled = true;

                                if (!Kbn.Equals("PtLoad"))
                                {
                                    // パターン呼び出しの場合は、既にパターンデータで設定済み
                                    this.form.ccbx_Touranshitei.Checked = false;
                                    this.form.ccbx_ItijiFuyou.Checked = false;
                                    this.form.ccbx_ChouboKisai.Checked = true;
                                }

                                if (this.form.ccbx_Touranshitei.Checked)
                                {
                                    //レコードの追加・編集を可能とする
                                    this.form.cdgv_Tyukanshori.AllowUserToAddRows = true;
                                    this.form.cdgv_Tyukanshori.Columns["Tyukanshori_chb_delete"].ReadOnly = false;
                                    this.form.cdgv_Tyukanshori.Columns["FM_MANIFEST_ID"].ReadOnly = false;

                                }
                                else if (this.form.ccbx_ItijiFuyou.Checked ||
                                    this.form.ccbx_ChouboKisai.Checked)
                                {
                                    //レコードが存在する場合はクリアする
                                    //レコードの追加・編集を不可とする
                                    this.form.cdgv_Tyukanshori.AllowUserToAddRows = true;
                                    this.form.SetTyukanshoriReadOnly();
                                }
                                break;

                        }

                        //産業廃棄物-減容後数量(編集不可、2次マニモード時は非表示)
                        this.form.cdgv_Haikibutu.Columns["GENNYOU_SUU"].Visible = false;
                        //排出事業者区分の条件の設定を使用
                        this.form.cantxt_HaisyutuGyousyaCd.HST_KBN = false;
                        this.form.cantxt_HaisyutuGyousyaCd.SBN_KBN = true;
                        this.form.cantxt_HaisyutuGenbaCd.HST_KBN = false;
                        this.form.cantxt_HaisyutuGenbaCd.SBN_KBN = true;
                        this.form.cantxt_HaisyutuGenbaCd.JIGYOUJOU_KBN = "3";

                        break;

                }
                //ヘッダーの設定
                SetHeader(strTitelName, BackColor);

                //ボディーの設定
                SetBody(this.form.Controls, BackColor);

                this.maniRelation = null; //紐付初期化

                // 最終処分の場所（予定）と最終処分事業場（実績）の行追加プロパティをtrueにする。
                this.form.cdgv_LastSBNbasyo_yotei.AllowUserToAddRows = true;
                this.form.cdgv_LastSBN_Genba_Jiseki.AllowUserToAddRows = true;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetManifestForm", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManifestForm", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 発行件数チェック処理
        /// </summary>
        public bool HakkouCntValidating()
        {
            Int32 hakkouCnt = 0;

            if (!string.IsNullOrEmpty(this.form.cantxt_HakkouCnt.Text))
            {
                hakkouCnt = Convert.ToInt32(this.form.cantxt_HakkouCnt.Text);

                if (hakkouCnt < 1 || hakkouCnt > 100)
                {
                    this.form.cantxt_HakkouCnt.IsInputErrorOccured = true;
                    this.form.cantxt_HakkouCnt.BackColor = Constans.ERROR_COLOR;
                    this.msgLogic.MessageBoxShow("E002", "発行件数", "1～100");
                    return false;
                }
            }
            else
            {
                //未入力の場合
                this.form.cantxt_HakkouCnt.Text = "1";
            }

            this.form.cantxt_HakkouCnt.BackColor = Constans.NOMAL_COLOR;
            //正常の場合
            return true;
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
            this.headerform.lbl_FirstRegistSha.BackColor = BackColor;
            this.headerform.lbl_LastUpdateSha.BackColor = BackColor;
            LogUtility.DebugMethodEnd(strTitleName, BackColor);
        }

        /// <summary>
        /// ラベルの背景色を変更
        /// </summary>
        /// <param name="BackColor">設定する色</param>
        /// <param name="ctls">コントロール</param>
        public void SetBody(Control.ControlCollection ctls, Color BackColor)
        {
            //LogUtility.DebugMethodStart(BackColor);

            parentbaseform.lb_process.BackColor = BackColor;

            foreach (Control con in ctls)
            {
                if ((con as Label) != null)
                {
                    if (!(con as Label).BorderStyle.Equals(BorderStyle.None))
                    {
                        (con as Label).BackColor = BackColor;
                        (con as Label).ForeColor = Color.White;
                    }

                }
                else if ((con as CustomPopupDataGridView) != null)
                {
                    (con as CustomPopupDataGridView).ColumnHeadersDefaultCellStyle.BackColor = BackColor;
                }
                else if ((con as Panel) != null)
                {
                    if (con.Controls.Count > 0) SetBody(con.Controls, BackColor);
                }
                else if ((con as r_framework.CustomControl.CustomPanel) != null)
                {
                    if (con.Controls.Count > 0) SetBody(con.Controls, BackColor);
                }

            }

            //LogUtility.DebugMethodEnd(BackColor);
        }
        /// <summary>
        /// 最終処分ポップアップ
        /// </summary>
        /// <param name="isLastSbnEndrepFlg">
        /// 最終処分終了報告フラグ(false:最終処分終了報告の取消の場合)
        /// </param>
        public bool LastSbnEndrepPopup(bool isLastSbnEndrepFlg)
        {

            LogUtility.DebugMethodStart(isLastSbnEndrepFlg);

            //2次マニかつ修正モード時のみ表示・使用可
            if (!(this.maniFlag == 2
                && WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.Mode)))
            {
                return false;
            }

            try
            {
                // 検索条件を設定する
                SearchMasterDataDTOCls Serch = new SearchMasterDataDTOCls();
                // マニフェストのシステムID
                Serch.KANRI_ID = this.KanriId;

                // 登録対象を検索する
                DataTable dt;

                if (isLastSbnEndrepFlg)
                {
                    // 最終処分終了報告
                    dt = this.LastSbnEndrepInfoDao.GetLastSbnEndrepInfo(Serch);

                    // 電マニ混廃用ロジック
                    List<string> tempKanriIds = new List<string>();
                    foreach (DataRow tempDataRow in dt.Rows)
                    {
                        if (!tempKanriIds.Contains(tempDataRow["KANRI_ID"].ToString()))
                        {
                            tempKanriIds.Add(tempDataRow["KANRI_ID"].ToString());
                        }
                    }

                    if (tempKanriIds.Count > 0)
                    {
                        // 電マニ混廃振分で区分：最終にしたデータを取得
                        // 最終処分終了報告できるようにする
                        var tempDt = this.LastSbnEndrepInfoDao.GetMixManifestForLastSbnData(tempKanriIds);
                        if (tempDt != null && tempDt.Rows.Count > 0)
                        {
                            dt.Merge(tempDt);
                        }
                    }

                    // 混廃の場合は紐付いている全ての二次マニが処分終了していること
                    List<string> delKanriIds = new List<string>();
                    var filteringDt = dt.Select("LAST_SBN_END_DATE IS NULL OR LAST_SBN_JOU_NAME IS NULL OR (LAST_SBN_JOU_ADDRESS1 = '' AND LAST_SBN_JOU_ADDRESS2 = '' AND LAST_SBN_JOU_ADDRESS3 = '' AND LAST_SBN_JOU_ADDRESS4 = '')");
                    foreach (var tempRow in filteringDt)
                    {
                        if (!delKanriIds.Contains(tempRow["KANRI_ID"].ToString()))
                        {
                            delKanriIds.Add(tempRow["KANRI_ID"].ToString());
                        }
                    }

                    foreach (var kanriId in delKanriIds)
                    {
                        var tempDt = dt.Select("KANRI_ID = '" + kanriId + "'");
                        foreach (var row in tempDt)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                }
                else
                {
                    // 最終処分終了報告の取消
                    dt = this.LastSbnEndrepCancelInfoDao.GetLastSbnEndrepCancelInfo(Serch);
                }

                /**
                 * エラーチェック
                 */
                List<string> unModifiedKanriIdList = new List<string>();
                ManifestoLogic maniLogic = new ManifestoLogic();

                // メッセージ表示
                if (!maniLogic.ChkLastSbnEndrepReport(dt, isLastSbnEndrepFlg, out unModifiedKanriIdList))
                {
                    if (DialogResult.Yes != this.msgLogic.MessageBoxShow("C070"))
                    {
                        return true;
                    }
                }

                // 最終処分終了日 ≦ 最終処分終了の報告日チェック
                if (isLastSbnEndrepFlg)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime HAIKI_IN_DATE = DateTime.ParseExact(this.GetDbValue(row["LAST_SBN_END_DATE"]),
                                            "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        //if (HAIKI_IN_DATE.Date.CompareTo(DateTime.Now.Date) > 0)
                        if (HAIKI_IN_DATE.Date.CompareTo(this.parentbaseform.sysDate.Date) > 0)
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                        {
                            this.msgLogic.MessageBoxShow("E218");
                            return true;
                        }
                    }
                }

                /**
                 * 登録データ作成
                 */
                // 最終処分保留
                List<T_LAST_SBN_SUSPEND> lastSbnSusPendList = new List<T_LAST_SBN_SUSPEND>();
                List<string> systemIdList = new List<string>();
                // キュー情報
                List<QUE_INFO> queList = new List<QUE_INFO>();
                List<string> kanriIdList = new List<string>();
                // D12 2次マニフェスト情報
                List<DT_D12> manifastList = new List<DT_D12>();
                // D13 最終処分終了日・事業場情報
                List<DT_D13> jigyoubaList = new List<DT_D13>();
                // マニフェスト目次情報
                List<DT_MF_TOC> mokujiList = new List<DT_MF_TOC>();

                // 件数
                int index = 0;
                for (; index < dt.Rows.Count; index++)
                {
                    if (!unModifiedKanriIdList.Contains(dt.Rows[index]["KANRI_ID"].ToString()))
                    {
                        if (!systemIdList.Contains(dt.Rows[index]["SYSTEM_ID"].ToString())
                            && !kanriIdList.Contains(dt.Rows[index]["KANRI_ID"].ToString()))
                        {
                            // 最終処分保留(T_LAST_SBN_SUSPEND)データ作成
                            T_LAST_SBN_SUSPEND LastSbnSusPend = new T_LAST_SBN_SUSPEND();
                            // システムID
                            LastSbnSusPend.SYSTEM_ID = this.ManiInfo.dt_r18ExOld.SYSTEM_ID;
                            systemIdList.Add(dt.Rows[index]["SYSTEM_ID"].ToString());
                            // 削除フラグ
                            LastSbnSusPend.DELETE_FLG = false;
                            if (!isLastSbnEndrepFlg && !string.IsNullOrEmpty(dt.Rows[index]["TIME_STAMP"].ToString()) && ((byte[])dt.Rows[index]["TIME_STAMP"]).Length != 0)
                            {
                                LastSbnSusPend.TIME_STAMP = (byte[])dt.Rows[index]["TIME_STAMP"];
                            }
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderLastSbnSusPend = new DataBinderLogic<T_LAST_SBN_SUSPEND>(LastSbnSusPend);
                            dataBinderLastSbnSusPend.SetSystemProperty(LastSbnSusPend, true);
                            lastSbnSusPendList.Add(LastSbnSusPend);
                        }

                        if (!kanriIdList.Contains(dt.Rows[index]["KANRI_ID"].ToString()))
                        {
                            // キュー情報(QUE_INFO)データ作成
                            QUE_INFO Que = new QUE_INFO();
                            // 管理番号
                            Que.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            kanriIdList.Add(dt.Rows[index]["KANRI_ID"].ToString());
                            // 枝番
                            Que.SEQ = Convert.ToInt16(dt.Rows[index]["LATEST_SEQ"].ToString());
                            if (isLastSbnEndrepFlg)
                            {
                                // 機能番号
                                Que.FUNCTION_ID = CommonConst.FUNCTION_ID_2000;
                            }
                            else
                            {
                                // 機能番号
                                Que.FUNCTION_ID = CommonConst.FUNCTION_ID_2100;
                            }
                            // キュー状態フラグ
                            Que.STATUS_FLAG = 0;
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderQue = new DataBinderLogic<QUE_INFO>(Que);
                            dataBinderQue.SetSystemProperty(Que, true);
                            queList.Add(Que);

                            // マニフェスト目次情報データ作成
                            DT_MF_TOC mokuji = new DT_MF_TOC();
                            // 管理番号
                            mokuji.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            // 状態詳細フラグ
                            if (isLastSbnEndrepFlg)
                            {
                                mokuji.STATUS_DETAIL = 0;
                            }
                            else
                            {
                                mokuji.STATUS_DETAIL = 1;
                            }
                            mokuji.UPDATE_TS = (DateTime)dt.Rows[index]["UPDATE_TS"];
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderMokuji = new DataBinderLogic<DT_MF_TOC>(mokuji);
                            dataBinderMokuji.SetSystemProperty(mokuji, false);
                            mokujiList.Add(mokuji);
                        }
                        // 最終処分終了報告の判断
                        if (isLastSbnEndrepFlg)
                        {
                            // D12 2次マニフェスト情報データ作成
                            DT_D12 manifast = new DT_D12();
                            // 管理番号
                            manifast.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            // 2次マニフェスト番号
                            manifast.SCND_MANIFEST_ID = dt.Rows[index]["MANIFEST_ID"].ToString();
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderManifast = new DataBinderLogic<DT_D12>(manifast);
                            dataBinderManifast.SetSystemProperty(manifast, true);
                            manifastList.Add(manifast);

                            // 最終処分終了日・事業場情報データ作成
                            DT_D13 jigyouba = new DT_D13();
                            // 管理番号
                            jigyouba.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            // 最終処分事業場名称
                            jigyouba.LAST_SBN_JOU_NAME = this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_NAME"]);
                            // 最終処分事業場所在地の郵便番号
                            jigyouba.LAST_SBN_JOU_POST = this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_POST"]);

                            // 住所分割
                            string tempAddress1;
                            string tempAddress2;
                            string tempAddress3;
                            string tempAddress4;

                            maniLogic.SetAddress1ToAddress4(this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_ADDRESS1"]) + this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_ADDRESS2"])
                                + this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_ADDRESS3"]) + this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_ADDRESS4"]),
                                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                            // 最終処分事業場所在地1
                            jigyouba.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                            // 最終処分事業場所在地2
                            jigyouba.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                            // 最終処分事業場所在地3
                            jigyouba.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                            // 最終処分事業場所在地4
                            jigyouba.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                            // 最終処分事業場電話番号
                            jigyouba.LAST_SBN_JOU_TEL = this.GetDbValue(dt.Rows[index]["LAST_SBN_JOU_TEL"]);
                            // 最終処分終了日
                            jigyouba.LAST_SBN_END_DATE = this.GetDbValue(dt.Rows[index]["LAST_SBN_END_DATE"]);
                            // レコード作成日時
                            // タイムスタンプ
                            var dataBinderjigyouba = new DataBinderLogic<DT_D13>(jigyouba);
                            dataBinderjigyouba.SetSystemProperty(jigyouba, true);
                            jigyoubaList.Add(jigyouba);
                        }
                    }
                }

                // 登録対象存在する場合
                if ((lastSbnSusPendList.Count > 0 || queList.Count > 0
                    || manifastList.Count > 0 || jigyoubaList.Count > 0
                    || mokujiList.Count > 0))
                {
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
                    if (isLastSbnEndrepFlg)
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
                }
                else
                {
                    if (isLastSbnEndrepFlg)
                    {
                        this.msgLogic.MessageBoxShow("W002", "最終処分終了報告");
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShow("W002", "最終処分終了報告の取消");
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LastSbnEndrepPopup", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LastSbnEndrepPopup", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isLastSbnEndrepFlg);
            }

        }

        /// <summary>
        /// 中間処理産業廃棄物情報の取得と設定
        /// </summary>
        private void SetFirstManifestInfo()
        {
            int fmIndex = 0;

            fmIndex = this.form.cdgv_Tyukanshori.CurrentCell.RowIndex;

            SearchMasterDataDTOCls search = new SearchMasterDataDTOCls();

            search.MANIFEST_ID = this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_MANIFEST_ID"].Value.ToString();
            FirstManifestInfo = this.FirstManifestInfoDao.GetFirstManifestInfo(search);

            //データが存在する場合
            if (FirstManifestInfo != null && FirstManifestInfo.Rows.Count > 0)
            {
                DataRow dr = FirstManifestInfo.Rows[0];
                //this.form.cdgv_Tyukanshori.Rows.Clear();
                //foreach (DataRow dr in FirstManifestInfo.Rows)
                //{
                //this.form.cdgv_Tyukanshori.Rows.Add();
                //this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_MANIFEST_ID"].Value = dr["MANIFEST_ID"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_RENRAKU_ID1"].Value = dr["RENRAKU_ID1"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_RENRAKU_ID2"].Value = dr["RENRAKU_ID2"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_RENRAKU_ID3"].Value = dr["RENRAKU_ID3"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HST_GYOUSHA_CD"].Value = dr["HST_GYOUSHA_CD"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = dr["HST_GYOUSHA_NAME"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HST_GENBA_CD"].Value = dr["HST_GENBA_CD"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HST_GENBA_NAME"].Value = dr["HST_GENBA_NAME"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_KOUFU_DATE"].Value = dr["KOUFU_DATE"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_SBN_END_DATE"].Value = dr["SBN_END_DATE"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HAIKI_SHURUI_CD"].Value = dr["HAIKI_SHURUI_CD"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = dr["HAIKI_SHURUI_NAME_RYAKU"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HAIKI_SUU"].Value = dr["HAIKI_SUU"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_HAIKI_UNIT_CD"].Value = (dr["HAIKI_UNIT_CD"] == null) ? null : this.PadLeftUnitCd(this.GetDbValue(dr["HAIKI_UNIT_CD"]));
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["FM_UNIT_NAME_RYAKU"].Value = dr["UNIT_NAME_RYAKU"];
                //電子/紙区分　非表示列
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["Hidden_FM_MEDIA_TYPE"].Value = dr["HAIKI_KBN_CD"];
                this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["Hidden_FM_SYSTEM_ID"].Value = dr["SYSTEM_ID"];
                //this.form.cdgv_Tyukanshori.Rows[fmIndex].Cells["Hidden_FM_HAIKI_KBN_CD"].Value = dr["HAIKI_KBN_CD"];
                //fmIndex += 1;
                //}
            }
        }

        /// <summary>
        /// DBデータを取得
        /// </summary>
        private string GetDbValue(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 中間処理産業廃棄物チェック
        /// </summary>
        public bool TyukanshoriCellValidating(out bool catchErr)
        {
            catchErr = false;

            try
            {
                string fmManifestId = string.Empty;

                fmManifestId = this.GetDbValue(this.form.cdgv_Tyukanshori.CurrentCell.Value);

                if (string.IsNullOrEmpty(fmManifestId))
                {
                    this.form.SetTyukanshoriCellReadOnly(true, this.form.cdgv_Tyukanshori.CurrentRow, true);
                    return false;
                }

                DataTable dt = new DataTable();

                SearchMasterDataDTOCls search = new SearchMasterDataDTOCls();
                search.MANIFEST_ID = fmManifestId;

                dt = this.FirstManifestInfoDao.GetFirstManifestInfo(search);

                //データが存在しない場合
                if (dt == null || dt.Rows.Count == 0)
                {
                    //ﾏｽﾀ存在しない場合、エラーメッセージを表示
                    this.msgLogic.MessageBoxShow("E028");
                    this.form.SetTyukanshoriCellReadOnly(true, this.form.cdgv_Tyukanshori.CurrentRow, true);
                    return true;
                }

                //this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_MANIFEST_ID"].Value = dt.Rows[0][""];
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_RENRAKU_ID1"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["RENRAKU_ID1"])) ? null : this.GetDbValue(dt.Rows[0]["RENRAKU_ID1"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_RENRAKU_ID2"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["RENRAKU_ID2"])) ? null : this.GetDbValue(dt.Rows[0]["RENRAKU_ID2"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_RENRAKU_ID3"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["RENRAKU_ID3"])) ? null : this.GetDbValue(dt.Rows[0]["RENRAKU_ID3"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HST_GYOUSHA_CD"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HST_GYOUSHA_CD"])) ? null : this.GetDbValue(dt.Rows[0]["HST_GYOUSHA_CD"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HST_GYOUSHA_NAME"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HST_GYOUSHA_NAME"])) ? null : this.GetDbValue(dt.Rows[0]["HST_GYOUSHA_NAME"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HST_GENBA_CD"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HST_GENBA_CD"])) ? null : this.GetDbValue(dt.Rows[0]["HST_GENBA_CD"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HST_GENBA_NAME"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HST_GENBA_NAME"])) ? null : this.GetDbValue(dt.Rows[0]["HST_GENBA_NAME"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_KOUFU_DATE"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["KOUFU_DATE"])) ? null : this.GetDbValue(dt.Rows[0]["KOUFU_DATE"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["Hidden_FM_MEDIA_TYPE"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HAIKI_KBN_CD"])) ? null : this.GetDbValue(dt.Rows[0]["HAIKI_KBN_CD"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["Hidden_FM_SYSTEM_ID"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["SYSTEM_ID"])) ? null : this.GetDbValue(dt.Rows[0]["SYSTEM_ID"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["Hidden_KANRI_ID"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["KANRI_ID"])) ? null : this.GetDbValue(dt.Rows[0]["KANRI_ID"]);
                this.form.cdgv_Tyukanshori.CurrentRow.Cells["Hidden_R18_MANIFEST_ID"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["R18_MANIFEST_ID"])) ? null : this.GetDbValue(dt.Rows[0]["R18_MANIFEST_ID"]);

                // マニフェストの明細が１行の場合のみ、廃棄物種類CD、処分終了日、数量、単位CDを設定する。
                if (dt.Rows.Count == 1)
                {
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_SBN_END_DATE"].Value =
                    string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["SBN_END_DATE"])) ? null : this.GetDbValue(dt.Rows[0]["SBN_END_DATE"]);
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_SHURUI_CD"].Value =
                        string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HAIKI_SHURUI_CD"])) ? null : this.GetDbValue(dt.Rows[0]["HAIKI_SHURUI_CD"]);
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value =
                        string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"])) ? null : this.GetDbValue(dt.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_SUU"].Value =
                        string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HAIKI_SUU"])) ? null : this.GetDbValue(dt.Rows[0]["HAIKI_SUU"]);
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_UNIT_CD"].Value =
                        string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["HAIKI_UNIT_CD"])) ? null : this.PadLeftUnitCd(this.GetDbValue(dt.Rows[0]["HAIKI_UNIT_CD"]));
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_UNIT_NAME_RYAKU"].Value =
                        string.IsNullOrEmpty(this.GetDbValue(dt.Rows[0]["UNIT_NAME_RYAKU"])) ? null : this.GetDbValue(dt.Rows[0]["UNIT_NAME_RYAKU"]);
                }
                else
                {
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_SBN_END_DATE"].Value = null;
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_SHURUI_CD"].Value = null;
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = null;
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_SUU"].Value = null;
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_HAIKI_UNIT_CD"].Value = null;
                    this.form.cdgv_Tyukanshori.CurrentRow.Cells["FM_UNIT_NAME_RYAKU"].Value = null;
                }

                //電子の場合、編集不可を設定する
                if (ConstCls.DENSHI_MEDIA_TYPE.Equals(this.GetDbValue(dt.Rows[0]["HAIKI_KBN_CD"])))
                {
                    this.form.SetTyukanshoriCellReadOnly(true, this.form.cdgv_Tyukanshori.CurrentRow, false);
                }
                else
                {
                    this.form.SetTyukanshoriCellReadOnly(false, this.form.cdgv_Tyukanshori.CurrentRow, false);
                }

                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TyukanshoriCellValidating", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TyukanshoriCellValidating", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }

        }


        /// <summary>
        /// 処理実行メソッド
        /// </summary>
        public virtual int DoProcess(KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                int iret = 0;
                if (e.KeyData != Keys.Enter)
                {
                    return iret;
                }
                if (parentbaseform.txb_process.Text.Trim() == "1" &&
                    parentbaseform.bt_process1.Enabled == true)
                {
                    //電子マニフェスト登録
                    this.SavePatternData(new object(), new EventArgs());
                    iret = 1;
                }
                else if (parentbaseform.txb_process.Text.Trim() == "2" &&
                    parentbaseform.bt_process2.Enabled == true)
                {
                    //パターン呼出
                    if (this.CallPattern())
                    {
                        iret = 2;
                    }
                }
                else if (parentbaseform.txb_process.Text.Trim() == "3" &&
                    parentbaseform.bt_process3.Enabled == true)
                {
                    //マニフェスト紐付
                    if (this.ToHimodukeForm())
                    {
                        iret = 3;
                    }
                    else
                    {
                        iret = -1;
                    }
                }
                else if (parentbaseform.txb_process.Text.Trim() == "4" &&
                    parentbaseform.bt_process4.Enabled == true)
                {
                    //最終処分ポップアップ
                    if (this.LastSbnEndrepPopup(true))
                    {
                        iret = 4;
                    }
                    else
                    {
                        iret = -1;
                    }
                }
                else if (parentbaseform.txb_process.Text.Trim() == "5" &&
                    parentbaseform.bt_process5.Enabled == true)
                {
                    //最終処分終了報告の取消
                    if (this.LastSbnEndrepPopup(false))
                    {
                        iret = 5;
                    }
                    else
                    {
                        iret = -1;
                    }
                }

                return iret;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DoProcess", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DoProcess", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }

        /// <summary>
        /// 単位CDのゼロパディング処理
        /// </summary>
        /// <param name="unitCd"></param>
        /// <returns></returns>
        public string PadLeftUnitCd(string unitCd)
        {
            string unitCdRetVal = string.Empty;

            if (!string.IsNullOrEmpty(unitCd))
            {
                unitCdRetVal = unitCd.PadLeft(2, '0');
            }

            return unitCdRetVal;
        }

        /// <summary>
        /// 電子廃棄物種類細分類マスタデータを取得（区分='3'の全てデータ）
        /// </summary>
        /// <returns></returns>
        private void GetDenshiHaikiShuruiSaibunrui()
        {
            LogUtility.DebugMethodStart();

            try
            {

                M_DENSHI_HAIKI_SHURUI_SAIBUNRUI dataSaibunrui = new M_DENSHI_HAIKI_SHURUI_SAIBUNRUI();
                dataSaibunrui.HAIKI_KBN = 3;

                this.ListDenshiHaikiShuruiSaibunrui = this.im_denshi_haiki_shurui_saibunruidao.GetAllValidData(dataSaibunrui);

                M_DENSHI_HAIKI_SHURUI data = new M_DENSHI_HAIKI_SHURUI();
                data.HAIKI_KBN = 3;

                this.ListDenshiHaikiShurui = this.im_denshi_haiki_shuruidao.GetAllValidData(data);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 管理IDから最新のSEQを取得
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        public string GetSeq(string kanriId)
        {
            var dt = DT_MF_TOCDao.GetLATEST_APPROVAL_SEQ(" WHERE KANRI_ID = '" + kanriId + "'");

            if (dt.Rows.Count == 0)
            {
                return ""; //見つからなかったら空文字
            }

            //KANRI_ID,MANIFEST_ID,LATEST_SEQ,APPROVAL_SEQ
            return dt.Rows[0]["LATEST_SEQ"].ToString();


        }

        /// <summary>
        /// データ移動処理
        /// </summary>
        internal bool SetMoveData()
        {
            try
            {
                if (this.form.moveData_flg)
                {

                    this.form.cantxt_HaisyutuGyousyaCd.Text = this.form.moveData_gyousyaCd;
                    this.MovoDataForGyousya(this.form.cantxt_HaisyutuGyousyaCd);

                    this.form.cantxt_HaisyutuGenbaCd.Text = this.form.moveData_genbaCd;
                    this.MovoDataForGenba(this.form.cantxt_HaisyutuGenbaCd);

                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetMoveData", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetMoveData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// データ移行時、業者の値セット用
        /// </summary>
        /// <param name="ex"></param>
        private void MovoDataForGyousya(CustomForMasterKyoutuuPopup_Ex ex)
        {
            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
            //事業者区分の設定
            dto.HST_KBN = (ex.HST_KBN) ? "1" : null;
            dto.UPN_KBN = (ex.UPN_KBN) ? "1" : null;
            dto.SBN_KBN = (ex.SBN_KBN) ? "1" : null;
            dto.HOUKOKU_HUYOU_KBN = (ex.HOUKOKU_HUYOU_KBN) ? "1" : null;
            if (dto.HST_KBN != null && dto.SBN_KBN != null)
            {
                //OR条件判断
                dto.JIGYOUSHA_KBN_OR = "1";
                dto.HST_KBN = null;
                dto.SBN_KBN = null;
            }
            //業者CDの設定
            dto.GYOUSHA_CD = ex.Text;
            DataTable dt = DsMasterLogic.GetDenshiGyoushaData(dto);

            this.SetControlTextValue(ex, dt, "電子事業者");
        }

        /// <summary>
        /// データ移行時、現場の値セット用
        /// </summary>
        /// <param name="ex"></param>
        private void MovoDataForGenba(CustomForMasterKyoutuuPopup_Ex ex)
        {
            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();


            //加入者番号取得
            if (!string.IsNullOrEmpty(ex.EDI_MEMBER_ID_ControlName))
            {
                Control ctl = ex.FindControl(ex.EDI_MEMBER_ID_ControlName);
                if (ctl != null)
                {
                    if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                    {
                        //加入者番号の設定
                        dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                    }
                }
            }


            //事業場番号取得
            if (!string.IsNullOrEmpty(ex.JIGYOUJOU_CD_ControlName))
            {
                Control ctl = ex.FindControl(ex.JIGYOUJOU_CD_ControlName);
                if (ctl != null)
                {
                    if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                    {
                        //加入者番号の設定
                        dto.JIGYOUJOU_CD = (ctl as TextBox).Text;
                    }
                }
            }

            dto.GENBA_CD = ex.Text;
            dto.JIGYOUJOU_KBN = ex.JIGYOUJOU_KBN;
            DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);

            this.SetControlTextValue(ex, dt, "電子事業場");

        }

        /// <summary>
        /// データ移行時、コントロールのテキスト値を設定
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CodeName"></param>
        private void SetControlTextValue(CustomForMasterKyoutuuPopup_Ex ex, DataTable dt, string CodeName)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E020", CodeName);
                    //リンクされたControl内容をクリアする
                    ex.ClearLikedControlText();
                }
                else if (dt.Rows.Count > 1)
                {
                    msgLogic.MessageBoxShow("E031", CodeName);
                    //リンクされたControl内容をクリアする
                    ex.ClearLikedControlText();
                }
                else
                {//一件正常内容を設定
                    string[] ctls = ex.SetFormField.Split(',');
                    string[] colName = ex.GetCodeMasterField.Split(',');
                    //取得カラム数量が設定したいコントロール数量不整合の場合、メッセージを出す
                    if (ctls.Length > colName.Length)
                    {
                        msgLogic.MessageBoxShow("E084", "リンクされたコントロール");

                        return;
                    }
                    //チェックOKしたら取得されたデータをコントロールに設定する
                    for (int i = 0; i < ctls.Length; i++)
                    {
                        Control ctl = ex.FindControl(ctls[i]);
                        if (ctl != null)
                        {
                            (ctl as TextBox).Text = (DBNull.Value == dt.Rows[0][colName[i]]) ?
                                string.Empty : (string)dt.Rows[0][colName[i]];
                        }
                    }


                    //電子事業者チェックOK場合
                    if (ex.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                    {
                        string sKanyusha = ex.GetKaNyuShaNo();
                        if (sKanyusha != null)
                        {
                            //関連現場CDコントロールのインスタンスを取得
                            if (!string.IsNullOrEmpty(ex.LinkedGENBA_CD_ControlName))
                            {
                                Control ctl = ex.FindControl(ex.LinkedGENBA_CD_ControlName);
                                if (ctl != null && (ctl as CustomForMasterKyoutuuPopup_Ex) != null)
                                {
                                    CustomForMasterKyoutuuPopup_Ex ctl_Ex = (CustomForMasterKyoutuuPopup_Ex)ctl;
                                    if (ctl_Ex.CheckOK_KanyushaCD != sKanyusha)
                                    {
                                        ctl_Ex.Text = string.Empty;
                                        ctl_Ex.ClearLikedControlText();
                                    }
                                }
                            }
                        }
                    }

                    //電子事業現場チェックOK場合
                    if (ex.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUJOU)
                    {
                        string sKanyusha = ex.GetKaNyuShaNo();
                        ex.CheckOK_KanyushaCD = sKanyusha;
                    }
                }
            }
        }

        /// <summary>
        /// 運搬先事業場チェック処理
        /// </summary>
        /// <pparam name="initFlag">true:処分受託者情報初期化</pparam>
        public bool ChkUnpansakiJigyoujou(bool initFlag)
        {
            try
            {
                bool isClearInfo = true;
                int lastIndex = this.form.cdgv_UnpanInfo.Rows.Count - 1;
                if (this.form.cdgv_UnpanInfo.Rows[lastIndex].IsNewRow)
                {
                    // 新規行は省く
                    lastIndex -= 1;
                }

                switch (lastIndex)
                {
                    case -1:
                        break;

                    default:
                        // 最後の行だけチェック
                        // 一番最後が運搬だった場合には処分場情報を空にする→登録時の処分場情報の必須チェックではじくため。
                        if (lastIndex < 0) break;

                        // 必須項目チェック
                        if (this.form.cdgv_UnpanInfo["Unpansaki_KanyushaCD", lastIndex].Value == null) break;
                        if (this.form.cdgv_UnpanInfo["UNPANSAKI_JIGYOUJOU_CD", lastIndex].Value == null) break;
                        if (string.IsNullOrWhiteSpace(this.form.cdgv_UnpanInfo["Unpansaki_KanyushaCD", lastIndex].Value.ToString())) break;
                        if (string.IsNullOrWhiteSpace(this.form.cdgv_UnpanInfo["UNPANSAKI_JIGYOUJOU_CD", lastIndex].Value.ToString())) break;

                        // 現場情報の取得を行う
                        DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
                        var ediMemberId = this.form.cdgv_UnpanInfo["Unpansaki_KanyushaCD", lastIndex].Value.ToString();
                        var jigyoujouCd = this.form.cdgv_UnpanInfo["UNPANSAKI_JIGYOUJOU_CD", lastIndex].Value.ToString();
                        M_DENSHI_JIGYOUSHA jigyousha = DaoInitUtility.GetComponent<DENSHI_JIGYOUSHA_SearchDaoCls>().GetDataByCd(ediMemberId);
                        M_DENSHI_JIGYOUJOU jigyoujou = DaoInitUtility.GetComponent<DENSHI_JIGYOUJOU_SearchDaoCls>().GetDataByCd(ediMemberId, jigyoujouCd);

                        if (jigyousha == null || jigyoujou == null) break;

                        if (jigyoujou.JIGYOUSHA_KBN == ConstCls.JIGYOUJOU_KBN_SBN)
                        {
                            // 行指定チェックの場合
                            if (!initFlag)
                            {
                                // 処分事業者情報をセットする
                                this.form.cantxt_SBN_JyutakuShaCD.Text = jigyousha.GYOUSHA_CD;
                                this.form.ctxt_SBN_JyutakuShaName.Text = jigyousha.JIGYOUSHA_NAME;
                                this.form.ctxt_SBN_GyouShaPost.Text = jigyousha.JIGYOUSHA_POST;
                                this.form.ctxt_SBN_GyouShaTel.Text = jigyousha.JIGYOUSHA_TEL;
                                this.form.ctxt_SBN_GyouShaAddr.Text = jigyousha.JIGYOUSHA_ADDRESS1 + jigyousha.JIGYOUSHA_ADDRESS2 + jigyousha.JIGYOUSHA_ADDRESS3 + jigyousha.JIGYOUSHA_ADDRESS4;
                                this.form.ctxt_SBN_KanyuShaNo.Text = jigyousha.EDI_MEMBER_ID;
                                this.form.ctxt_SBN_KyokaNo.Text = string.Empty;

                                // 処分事業場情報をセットする
                                this.form.cantxt_SBN_Genba_CD.Text = jigyoujou.GENBA_CD;
                                this.form.ctxt_SBN_Genba_Name.Text = jigyoujou.JIGYOUJOU_NAME;
                                this.form.ctxt_SBN_GenbaPost.Text = jigyoujou.JIGYOUJOU_POST;
                                this.form.ctxt_SBN_GenbaTel.Text = jigyoujou.JIGYOUJOU_TEL;
                                this.form.SBN_GenbaAddr.Text = jigyoujou.JIGYOUJOU_ADDRESS1 + jigyoujou.JIGYOUJOU_ADDRESS2 + jigyoujou.JIGYOUJOU_ADDRESS3 + jigyoujou.JIGYOUJOU_ADDRESS4;
                                this.form.ctxt_SBN_JIGYOUJYOU_CD.Text = jigyoujou.JIGYOUJOU_CD;

                                isClearInfo = false;
                                break;
                            }
                        }

                        // もし積替え保管のを考慮する必要があればここで制御
                        break;

                }

                // 処分受託者情報のクリア処理
                if (isClearInfo || initFlag)
                {
                    this.form.cantxt_SBN_JyutakuShaCD.Text = string.Empty;
                    this.form.ctxt_SBN_JyutakuShaName.Text = string.Empty;
                    this.form.ctxt_SBN_GyouShaPost.Text = string.Empty;
                    this.form.ctxt_SBN_GyouShaTel.Text = string.Empty;
                    this.form.ctxt_SBN_GyouShaAddr.Text = string.Empty;
                    this.form.ctxt_SBN_KanyuShaNo.Text = string.Empty;
                    this.form.ctxt_SBN_KyokaNo.Text = string.Empty;
                    this.form.cantxt_SBN_Genba_CD.Text = string.Empty;
                    this.form.ctxt_SBN_Genba_Name.Text = string.Empty;
                    this.form.ctxt_SBN_GenbaPost.Text = string.Empty;
                    this.form.ctxt_SBN_GenbaTel.Text = string.Empty;
                    this.form.SBN_GenbaAddr.Text = string.Empty;
                    this.form.ctxt_SBN_JIGYOUJYOU_CD.Text = string.Empty;
                }

                this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.IsAllowUnpanInfoAddRow();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChkUnpansakiJigyoujou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkUnpansakiJigyoujou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 運搬情報内の最終区間の運搬先事業場を取得
        /// </summary>
        /// <returns>運搬情報内の最終区間の運搬先事業場を取得</returns>
        public M_DENSHI_JIGYOUJOU LastUnpansakiJigyoujou()
        {
            M_DENSHI_JIGYOUJOU lastJigyoujou = null;
            int lastIndex = this.form.cdgv_UnpanInfo.Rows.Count - 1;

            switch (lastIndex)
            {
                case -1:
                    break;

                default:
                    // 最後の行だけチェック
                    // 一番最後が運搬だった場合には処分場情報を空にする→登録時の処分場情報の必須チェックではじくため。
                    if (lastIndex < 0) break;

                    // 必須項目チェック
                    if (this.form.cdgv_UnpanInfo["Unpansaki_KanyushaCD", lastIndex].Value == null) break;
                    if (this.form.cdgv_UnpanInfo["UNPANSAKI_JIGYOUJOU_CD", lastIndex].Value == null) break;
                    if (string.IsNullOrWhiteSpace(this.form.cdgv_UnpanInfo["Unpansaki_KanyushaCD", lastIndex].Value.ToString())) break;
                    if (string.IsNullOrWhiteSpace(this.form.cdgv_UnpanInfo["UNPANSAKI_JIGYOUJOU_CD", lastIndex].Value.ToString())) break;

                    // 現場情報の取得を行う
                    DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
                    var ediMemberId = this.form.cdgv_UnpanInfo["Unpansaki_KanyushaCD", lastIndex].Value.ToString();
                    var jigyoujouCd = this.form.cdgv_UnpanInfo["UNPANSAKI_JIGYOUJOU_CD", lastIndex].Value.ToString();
                    lastJigyoujou = DaoInitUtility.GetComponent<DENSHI_JIGYOUJOU_SearchDaoCls>().GetDataByCd(ediMemberId, jigyoujouCd);

                    if (lastJigyoujou == null)
                    {
                        break;
                    }
                    break;
            }
            return lastJigyoujou;
        }

        /// <summary>
        /// 運搬情報行追加可否判定
        /// </summary>
        /// <returns></returns>
        public bool IsAllowUnpanInfoAddRow()
        {
            bool bRet = true;
            //既に5行入力済
            if (this.form.cdgv_UnpanInfo.Rows.Count >= 5)
            {
                if (!this.form.cdgv_UnpanInfo.Rows[4].IsNewRow)
                {
                    bRet = false;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 廃棄物大分類CDから廃棄物類名を取得する
        /// </summary>
        /// <param name="haikiDaiCode"></param>
        /// <returns></returns>
        internal string GetHaikiBunruiName(string haikiDaiCode)
        {
            string returnVal = null;
            if (string.IsNullOrEmpty(haikiDaiCode))
            {
                return returnVal;
            }

            M_DENSHI_HAIKI_SHURUI conditionData = new M_DENSHI_HAIKI_SHURUI();
            conditionData.HAIKI_SHURUI_CD = haikiDaiCode + "00";
            var denshiHaikiDaibunruiList = this.im_denshi_haiki_shuruidao.GetAllValidData(conditionData);
            if (denshiHaikiDaibunruiList != null
                && denshiHaikiDaibunruiList.Count() == 1)
            {
                returnVal = denshiHaikiDaibunruiList[0].HAIKI_SHURUI_NAME;
            }

            return returnVal;
        }

        #region 住所半角英数字チェック
        /// <summary>
        /// 住所半角英数字チェック
        /// 以下の住所をチェックし、半角英数字が含まれる場合にtrueを返す。
        /// 排出事業者、排出事業場、処分事業者、処分事業場、収集運搬業者、運搬先の事業場、最終処分事業場（予定）、最終処分事業場（実績）
        /// </summary>
        /// <param name="maniInfo"></param>
        /// <returns>true：チェック対象の住所に半角英数字が含まれる。false：半角英数字が含まれない。</returns>
        internal bool CheckAddress(DenshiManifestInfoCls maniInfo)
        {
            // チェック対象無し
            if (maniInfo == null) return false;

            // 住所半角英数字チェック
            string hstGyoushaAddress = maniInfo.dt_r18.HST_SHA_ADDRESS1 + maniInfo.dt_r18.HST_SHA_ADDRESS2 + maniInfo.dt_r18.HST_SHA_ADDRESS3;
            if (StringUtil.ContainsHankakuAlphaNum(hstGyoushaAddress))
            {
                msgLogic.MessageBoxShow("E273", "排出事業者（事業者入力画面）の都道府県、市区町村、町域");
                this.form.cantxt_HaisyutuGyousyaCd.Focus();
                this.form.cantxt_HaisyutuGyousyaCd.SelectAll();
                return true;
            }


            // 住所半角英数字チェック
            string hstJouAddress = maniInfo.dt_r18.HST_JOU_ADDRESS1 + maniInfo.dt_r18.HST_JOU_ADDRESS2 + maniInfo.dt_r18.HST_JOU_ADDRESS3;
            if (StringUtil.ContainsHankakuAlphaNum(hstJouAddress))
            {
                msgLogic.MessageBoxShow("E273", "排出事業場（事業場入力画面）の都道府県、市区町村、町域");
                this.form.cantxt_HaisyutuGenbaCd.Focus();
                this.form.cantxt_HaisyutuGenbaCd.SelectAll();
                return true;
            }


            foreach (var tempDtr04 in maniInfo.lstDT_R04)
            {
                // 最終処分事業場（予定） 住所半角英数字チェック
                string lastSbnJouAddress = tempDtr04.LAST_SBN_JOU_ADDRESS1 + tempDtr04.LAST_SBN_JOU_ADDRESS2 + tempDtr04.LAST_SBN_JOU_ADDRESS3;
                if (StringUtil.ContainsHankakuAlphaNum(lastSbnJouAddress))
                {
                    msgLogic.MessageBoxShow("E273", string.Format("最終処分事業場（予定）{0}行目（事業場入力画面）の都道府県、市区町村、町域", tempDtr04.REC_SEQ));
                    this.form.cdgv_LastSBNbasyo_yotei.Focus();
                    return true;
                }
            }


            // 住所半角英数字チェック(処分事業者)
            string sbnShaAddress = maniInfo.dt_r18.SBN_SHA_ADDRESS1 + maniInfo.dt_r18.SBN_SHA_ADDRESS2 + maniInfo.dt_r18.SBN_SHA_ADDRESS3;
            if (StringUtil.ContainsHankakuAlphaNum(sbnShaAddress))
            {
                msgLogic.MessageBoxShow("E273", "処分事業者（事業者入力画面）の都道府県、市区町村、町域");
                this.form.cantxt_SBN_JyutakuShaCD.Focus();
                this.form.cantxt_SBN_JyutakuShaCD.SelectAll();
                return true;
            }

            // 各区間の住所半角英数字チェック
            foreach (var tempDtr19 in maniInfo.lstDT_R19)
            {
                // 収集運搬業者
                string upnShaAddress = tempDtr19.UPN_SHA_ADDRESS1 + tempDtr19.UPN_SHA_ADDRESS2 + tempDtr19.UPN_SHA_ADDRESS3;
                if (StringUtil.ContainsHankakuAlphaNum(upnShaAddress))
                {
                    msgLogic.MessageBoxShow("E273", string.Format("収集運搬事業者 区間{0} （事業者入力画面）の都道府県、市区町村、町域", tempDtr19.UPN_ROUTE_NO));
                    return true;
                }

                if (maniInfo.dt_r18.UPN_ROUTE_CNT == tempDtr19.UPN_ROUTE_NO)
                {
                    // 最終区間

                    // 住所半角英数字チェック(処分事業場)
                    string sbnJouAddress = tempDtr19.UPNSAKI_JOU_ADDRESS1 + tempDtr19.UPNSAKI_JOU_ADDRESS2 + tempDtr19.UPNSAKI_JOU_ADDRESS3;
                    if (StringUtil.ContainsHankakuAlphaNum(sbnJouAddress))
                    {
                        msgLogic.MessageBoxShow("E273", "処分事業場（事業場入力画面）の都道府県、市区町村、町域");
                        this.form.cantxt_SBN_Genba_CD.Focus();
                        this.form.cantxt_SBN_Genba_CD.SelectAll();
                        return true;
                    }
                }
                else
                {
                    // 運搬先の事業場
                    string upnSahkiAddress = tempDtr19.UPNSAKI_JOU_ADDRESS1 + tempDtr19.UPNSAKI_JOU_ADDRESS2 + tempDtr19.UPNSAKI_JOU_ADDRESS3;
                    if (StringUtil.ContainsHankakuAlphaNum(upnSahkiAddress))
                    {
                        msgLogic.MessageBoxShow("E273", string.Format("運搬先の事業場 区間{0}（事業場入力画面）の都道府県、市区町村、町域", tempDtr19.UPN_ROUTE_NO));
                        return true;
                    }
                }
            }

            foreach (var tempDtr13 in maniInfo.lstDT_R13)
            {
                // 最終処分事業場 住所半角英数字チェック
                string lastSbnJouAddress = tempDtr13.LAST_SBN_JOU_ADDRESS1 + tempDtr13.LAST_SBN_JOU_ADDRESS2 + tempDtr13.LAST_SBN_JOU_ADDRESS3;
                if (StringUtil.ContainsHankakuAlphaNum(lastSbnJouAddress))
                {
                    msgLogic.MessageBoxShow("E273", string.Format("最終処分事業場（実績） {0}行目 （事業場入力画面）の都道府県、市区町村、町域", tempDtr13.REC_SEQ));
                    return true;
                }
            }

            return false;
        }
        #endregion

        // 20141103 koukouei 委託契約チェック start

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
                CustomAlphaNumTextBox txtGenbaCd = this.form.cantxt_HaisyutuGenbaCd;
                CustomDateTimePicker txtSagyouDate = this.form.cdate_HikiwataDate;
                CustomDataGridView gridDetail = this.form.cdgv_Haikibutu;
                string CTL_NAME_DETAIL = "HAIKI_SHURUI_CD";
                string CTL_NAME_DETAIL_NAME = "HAIKI_SHURUI_NAME";

                //委託契約チェックDtoを取得
                ItakuCheckDTO checkDto = new ItakuCheckDTO();
                checkDto.MANIFEST_FLG = true;
                checkDto.HAIKI_KBN_CD = Shougun.Core.Common.BusinessCommon.Const.CommonConst.HAIKI_KBN_DENSHI;//4.電子
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

        // 20141103 koukouei 委託契約チェック end

        #region 最終処分事業場(実績)から最新の日付を取得する
        /// <summary>
        /// 最終処分事業場(実績)の情報から最新の日付を取得する
        /// </summary>
        /// <returns>最新の日付。もし何も入力されていない場合等、日付をチェックできない場合はDateTime.MinValueを返す。</returns>
        internal DateTime GetLatestLastSbnDate()
        {
            DateTime returnVal = DateTime.MinValue;

            if (this.form.cdgv_LastSBN_Genba_Jiseki.Rows.Count < 0)
            {
                return returnVal;
            }

            foreach (DataGridViewRow row in this.form.cdgv_LastSBN_Genba_Jiseki.Rows)
            {
                var lastSbnEndDate = row.Cells["LAST_SBN_END_DATE"].EditedFormattedValue;
                DateTime tempDate = DateTime.MinValue;

                if (lastSbnEndDate != null
                    && !string.IsNullOrEmpty(lastSbnEndDate.ToString())
                    && DateTime.TryParse(lastSbnEndDate.ToString(), out tempDate))
                {
                    if (DateTime.Compare(returnVal, tempDate.Date) < 0)
                    {
                        returnVal = tempDate.Date;
                    }
                }
                else
                {
                    // 最終処分日に有効な入力値が無く、かつ、最終処分業者に入力が有る場合
                    if (!string.IsNullOrEmpty(row.Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].EditedFormattedValue.ToString()))
                    {
                        returnVal = DateTime.MinValue;
                        break;
                    }
                }
            }

            return returnVal;
        }
        #endregion

        #region マニフェスト紐付時の登録処理
        /// <summary>
        /// マニフェスト紐付時の登録処理
        /// </summary>
        /// <param name="bIsHoryu">保留保存フラグ</param>
        /// <returns>True：正常に登録、False：チェックエラー</returns>
        internal bool HimodukeRegistDenshiManifest(bool bIsHoryu = false)
        {
            // 前提：
            // ・マニフェスト紐付時に実行される処理であるため、修正モード以外で実行されることはありえない
            // ・自動入力ではない場合のみ実行される
            bool retVal = false;

            // 登録処理前の各種チェック

            // 「送信中」データは登録不可
            if (this.Mode != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (this.ManiInfo.dt_mf_toc.STATUS_DETAIL == 1)
                {
                    this.msgLogic.MessageBoxShow("E128");
                    return retVal;
                }
            }

            Cursor.Current = Cursors.WaitCursor;

            // 中間処理産業廃棄物のチェックボックスのチェック
            if (this.ChkFirstManifest())
            {
                return retVal;
            }

            // 「中間処理産業廃棄物-マニフェスト番号／交付」がセットされたレコードが1件もない場合はエラーメッセージを表示し登録不可
            if (!this.ChkRegistFirstManifest())
            {
                this.msgLogic.MessageBoxShow("E001", "中間処理産業廃棄物");
                return retVal;
            }
            bool catchErr = false;
            DenshiManifestInfoCls ManiInfo = this.form.MakeAllData(out catchErr);
            if (catchErr)
            {
                return false;
            }
            ManiInfo.bHouryuFlg = bIsHoryu;

            // 必須入力チェック
            if (!this.CHk_MustbeInputItem(ManiInfo))
            {
                this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.form.GetUnpanInfoAddDeleteFlg();
                return retVal;
            }

            // マニフェスト番号チェック
            if (this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG && !ManiInfo.bIsAutoMode)
            {
                if (string.IsNullOrEmpty(ManiInfo.dt_r18.MANIFEST_ID))
                {
                    if (this.msgLogic.MessageBoxShow("C046", "マニフェスト番号が未入力です。更新") != DialogResult.Yes)
                    {
                        this.form.cantxt_ManifestNo.Focus();
                        this.form.cantxt_ManifestNo.SelectAll();
                        this.form.cdgv_UnpanInfo.AllowUserToAddRows = this.form.GetUnpanInfoAddDeleteFlg();
                        return retVal;
                    }
                }
            }

            // 登録処理
            if (ManiInfo.dt_r18 != null)
            {
                // 最新LATEST_SEQと修正/取消SEQの取得
                if (this.Mode != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    DataTable dt = this.DT_MF_TOCDao.GetLATEST_APPROVAL_SEQ(" WHERE KANRI_ID = '" + this.KanriId + "'");
                    this.LastSEQ = Convert.ToInt16(dt.Rows[0]["LATEST_SEQ"]);
                    if (DBNull.Value != dt.Rows[0]["APPROVAL_SEQ"])
                    {
                        this.APPROVAL_SEQ = Convert.ToInt16(dt.Rows[0]["APPROVAL_SEQ"]);
                    }
                }

                // 更新
                if (this.Mode == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 最終処分終了済みかつ、最終処分情報に未入力が存在する場合、報告済みの内容と差異が発生する可能性があるのでアラートを表示
                    var commonManiLogic = new ManifestoLogic();
                    var isFixedFirstElecMani = commonManiLogic.IsFixedRelationFirstMani(this.ManiInfo.dt_r18ExOld.SYSTEM_ID, 4);
                    if (this.maniFlag == 2 && isFixedFirstElecMani && !this.form.existAllLastSbnInfo)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        if (msgLogic.MessageBoxShow("C080") == DialogResult.No)
                        {
                            return retVal;
                        }
                    }

                    // 更新前に最新の状態を取得
                    this.ManiInfo = this.GetManiInfoFromDB(this.KanriId, this.Seq);

                    this.HimodukeUpdate = true;
                    this.Update(ManiInfo.bIsAutoMode,
                                      ManiInfo.bHouryuFlg,
                                      ManiInfo.dt_r18,
                                      ManiInfo.que_Info,
                                      this.ManiInfo.dt_mf_toc,
                                      this.ManiInfo.dt_mf_member,
                                      ManiInfo.lstDT_R19,
                                      ManiInfo.lstDT_R02,
                                      ManiInfo.lstDT_R04,
                                      ManiInfo.lstDT_R05,
                                      ManiInfo.lstDT_R06,
                                      ManiInfo.lstDT_R13,
                                      ManiInfo.lstDT_R08,                       //一次マニフェスト情報
                                      this.ManiInfo.dt_r18ExOld,
                                      this.ManiInfo.lstDT_R19_EX,
                                      this.ManiInfo.lstDT_R04_EX,
                                      this.ManiInfo.lstDT_R13_EX,
                                      this.ManiInfo.lstDT_R08_EX,               //電子最終処分拡張
                                      this.ManiInfo.lstT_MANIFEST_RELATION      //紐付け情報      
                              );
                    if (this.isRegistErr)
                    {
                        retVal = false;
                        return retVal;
                    }
                    this.Seq = (SqlInt16)ManiInfo.dt_r18.SEQ;
                    this.HimodukeUpdate = false;
                }
                Cursor.Current = Cursors.Default;
                retVal = true;
            }
            return retVal;
        }
        #endregion

        #region [F9]保存ボタン押下時の、紐づく1次マニフェスト更新
        /// <summary>
        /// [F9]保存ボタン押下時の、紐づく1次マニフェスト更新
        /// </summary>
        internal void UpdateFirstMani()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.isRegistErr = false;
                if (this.maniFlag == 2)
                {
                    // システムIDを取得
                    SqlInt64 system_id = this.ManiInfo.dt_r18ExOld.SYSTEM_ID;

                    // 取得したシステムIDから、紐付先の1次マニの情報を取得
                    List<T_MANIFEST_DETAIL> listFirstDetailForUpdate = new List<T_MANIFEST_DETAIL>();

                    #region 一次電マニ更新用

                    // 更新用変数
                    var mfTocList = new List<DT_MF_TOC>();
                    var r18List = new List<DT_R18>();
                    var r19List = new List<DT_R19[]>();
                    var r02List = new List<DT_R02[]>();
                    var r04List = new List<DT_R04[]>();
                    var r05List = new List<DT_R05[]>();
                    var r08List = new List<DT_R08[]>();
                    var R13List = new List<DT_R13[]>();
                    var r18ExList = new List<DT_R18_EX>();
                    var r19ExList = new List<DT_R19_EX[]>();
                    var r04ExList = new List<DT_R04_EX[]>();
                    var r08ExList = new List<DT_R08_EX[]>();
                    var oldR13ExList = new List<DT_R13_EX[]>();

                    var newR13ExList = new List<DT_R13_EX[]>();

                    // 一次電マニの重複更新防止用変数
                    var executedKanriIds = new List<string>();
                    #region 電マニ用Dao生成
                    var mfTocDao = DaoInitUtility.GetComponent<CommonDT_MF_TOCDaoCls>();
                    var r18Dao = DaoInitUtility.GetComponent<CommonDT_R18DaoCls>();
                    var r19Dao = DaoInitUtility.GetComponent<CommonDT_R19DaoCls>();
                    var r02Dao = DaoInitUtility.GetComponent<CommonDT_R02DaoCls>();
                    var r04Dao = DaoInitUtility.GetComponent<CommonDT_R04DaoCls>();
                    var r05Dao = DaoInitUtility.GetComponent<CommonDT_R05DaoCls>();
                    var r06Dao = DaoInitUtility.GetComponent<CommonDT_R06DaoCls>();
                    var r08Dao = DaoInitUtility.GetComponent<CommonDT_R08DaoCls>();
                    var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
                    var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
                    var r19ExDao = DaoInitUtility.GetComponent<CommonDT_R19_EXDaoCls>();
                    var r04ExDao = DaoInitUtility.GetComponent<CommonDT_R04_EXDaoCls>();
                    var r08ExDao = DaoInitUtility.GetComponent<CommonDT_R08_EXDaoCls>();
                    var r13ExDao = DaoInitUtility.GetComponent<CommonDT_R13_EXDaoCls>();
                    var getManiRelDao = DaoInitUtility.GetComponent<GetManifestRelationDaoCls>();
                    #endregion

                    #endregion

                    var commonManifestLgic = new ManifestoLogic();
                    DataTable ManiRelationSysIdInfo = commonManifestLgic.SelectFirstManiSystemID(system_id.ToString(), CommonConst.RELATIION_HAIKI_KBN_CD_DENSHI.ToString());

                    // 更新前に最新の状態を取得
                    this.ManiInfo = this.GetManiInfoFromDB(this.KanriId, this.Seq);

                    foreach (DataRow r in ManiRelationSysIdInfo.Rows)
                    {
                        if (CommonConst.RELATIION_HAIKI_KBN_CD_DENSHI.ToString().Equals(r["FIRST_HAIKI_KBN_CD"].ToString()))
                        {
                            #region 一次電マニ更新用
                            // 一次電マニの更新情報をセット

                            #region チェック処理
                            if (r["KANRI_ID"] == null
                                || string.IsNullOrEmpty(r["KANRI_ID"].ToString())
                                || r["LATEST_SEQ"] == null
                                || string.IsNullOrEmpty(r["LATEST_SEQ"].ToString())
                                || r["EX_SEQ"] == null
                                || string.IsNullOrEmpty(r["EX_SEQ"].ToString())
                                )
                            {
                                // この行に入るときは、SQLがいけてないか、DBのデータがおかしい
                                continue;
                            }

                            if (executedKanriIds.Contains(r["KANRI_ID"].ToString()))
                            {
                                // 処理済み
                                continue;
                            }
                            else
                            {
                                executedKanriIds.Add(r["KANRI_ID"].ToString());
                            }
                            #endregion

                            // 一次電マニの場合は紙マニと違い、一括で紐付いている二次マニを参照してDT_R13, DT_R13_EXを作成しないとならい
                            string kanriId = r["KANRI_ID"].ToString();
                            string manifestId = r["MANIFEST_ID"].ToString();
                            SqlInt32 latestSeq = SqlInt32.Parse(r["LATEST_SEQ"].ToString());
                            SqlInt32 exSystemid = SqlInt32.Parse(r["EX_SYSTEM_ID"].ToString());
                            SqlInt32 exSeq = SqlInt32.Parse(r["EX_SEQ"].ToString());

                            #region 現在のデータを取得
                            DT_MF_TOC mfToc = mfTocDao.GetDataForEntity(new DT_MF_TOC() { KANRI_ID = kanriId });
                            DT_R18 r18 = r18Dao.GetDataForEntity(new DT_R18() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R19[] r19 = r19Dao.GetDataForEntity(new DT_R19() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R02[] r02 = r02Dao.GetDataForEntity(new DT_R02() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R04[] r04 = r04Dao.GetDataForEntity(new DT_R04() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R05[] r05 = r05Dao.GetDataForEntity(new DT_R05() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R08[] r08 = r08Dao.GetDataForEntity(new DT_R08() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R13[] r13 = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = kanriId, SEQ = latestSeq });
                            DT_R18_EX oldR18Ex = r18ExDao.GetDataForEntity(new DT_R18_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                            DT_R19_EX[] oldR19Ex = r19ExDao.GetDataForEntity(new DT_R19_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                            DT_R04_EX[] oldR04Ex = r04ExDao.GetDataForEntity(new DT_R04_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                            DT_R08_EX[] oldR08Ex = r08ExDao.GetDataForEntity(new DT_R08_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                            DT_R13_EX[] oldR13Ex = r13ExDao.GetDataForEntity(new DT_R13_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                            #endregion

                            if (mfToc.KIND.IsNull
                                || mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)
                            {
                                // 一次電マニがNot EDI(手動)以外の場合、最終処分情報は更新しない。
                                // Not EDI(手動)以外の場合は[3] 最終処分終了報告ボタンから最終処分情報を更新する。
                                continue;
                            }

                            SqlDecimal updateLatestSeq = mfToc.LATEST_SEQ + 1;
                            SqlInt32 updateExSeq = exSeq + 1;

                            #region DT_R13, DT_R13_EXの更新データをセット
                            // 二次マニ全件取得
                            DataTable nextManis = new DataTable();
                            nextManis = getManiRelDao.GetLastSbnInfoForNexttMani(SqlInt64.Parse(r["FIRST_SYSTEM_ID"].ToString()));

                            // 業者、現場の一覧を生成
                            var gyoushaAndGenbaList = nextManis.AsEnumerable().Select(result => new
                            {
                                SECOND_HAIKI_KBN_CD = result.Field<int>("SECOND_HAIKI_KBN_CD"),
                                SECOND_SYS_ID = result.Field<long>("SECOND_SYSTEM_ID"),
                                SECOND_DETAIL_SYS_ID = result.Field<decimal>("SECOND_DETAIL_SYSTEM_ID"),
                                LAST_SBN_JOU_NAME = result.Field<string>("LAST_SBN_JOU_NAME"),
                                LAST_SBN_JOU_ADDRESS = result.Field<string>("LAST_SBN_JOU_ADDRESS")
                            }).
                                                        GroupBy(gryoup => new { gryoup.SECOND_HAIKI_KBN_CD, gryoup.SECOND_SYS_ID, gryoup.SECOND_DETAIL_SYS_ID, gryoup.LAST_SBN_JOU_NAME, gryoup.LAST_SBN_JOU_ADDRESS });

                            int recSeq = 1;
                            // DT_R13_EX用のCreate情報をセット
                            string createUser = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_USER;
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? DateTime.Now : oldR13Ex[0].CREATE_DATE;
                            SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? this.getDBDateTime() : oldR13Ex[0].CREATE_DATE;
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            string createPc = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_PC;
                            DateTime lastSbnEndDate = DateTime.MinValue;

                            var commonManiLogic = new ManifestoLogic();
                            var tempR13 = new List<DT_R13>();
                            var tempR13Ex = new List<DT_R13_EX>();

                            // 業者、現場毎に最終処分終了報告情報を生成
                            foreach (var gyoushaAndGenbaRow in gyoushaAndGenbaList)
                            {
                                if (string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME)
                                    || string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS))
                                {
                                    // 最終処分場がない場合、データの作りようがないので、除外
                                    continue;
                                }

                                DateTime tempLastSbnEndDate = DateTime.MinValue;
                                var groupData = nextManis.Select(string.Format(
                                        "SECOND_HAIKI_KBN_CD = {0} AND SECOND_SYSTEM_ID = {1} AND SECOND_DETAIL_SYSTEM_ID = {2} AND LAST_SBN_JOU_NAME = '{3}' AND LAST_SBN_JOU_ADDRESS = '{4}'"
                                        , gyoushaAndGenbaRow.Key.SECOND_HAIKI_KBN_CD, gyoushaAndGenbaRow.Key.SECOND_SYS_ID, gyoushaAndGenbaRow.Key.SECOND_DETAIL_SYS_ID
                                        , gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME, gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS)
                                        );

                                foreach (var tempRow in groupData)
                                {
                                    if (tempRow["LAST_SBN_END_DATE"] == null
                                        || string.IsNullOrEmpty(tempRow["LAST_SBN_END_DATE"].ToString()))
                                    {
                                        // 最終処分終了日が設定されていないものがあれば最終処分未完了
                                        tempLastSbnEndDate = DateTime.MinValue;
                                        break;
                                    }

                                    // 一番新しい日付をセット
                                    if (DateTime.Compare(tempLastSbnEndDate, DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString())) < 0)
                                    {
                                        tempLastSbnEndDate = DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString());
                                    }
                                }

                                // 
                                var firstManiR13 = new DT_R13();
                                var firstManiR13EX = new DT_R13_EX();

                                // 住所分割
                                string tempAddress1;
                                string tempAddress2;
                                string tempAddress3;
                                string tempAddress4;
                                commonManiLogic.SetAddress1ToAddress4(groupData[0].Field<string>("LAST_SBN_JOU_ADDRESS"),
                                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                                // set keys
                                firstManiR13.KANRI_ID = kanriId;
                                firstManiR13.SEQ = updateLatestSeq;
                                firstManiR13.REC_SEQ = recSeq;
                                firstManiR13EX.KANRI_ID = kanriId;
                                firstManiR13EX.SYSTEM_ID = exSystemid;
                                firstManiR13EX.SEQ = updateExSeq;
                                firstManiR13EX.REC_SEQ = recSeq;

                                // DT_R18.LAST_SBN_END_DATE用の日付
                                lastSbnEndDate = DateTime.Compare(lastSbnEndDate, tempLastSbnEndDate) < 0 ? tempLastSbnEndDate : lastSbnEndDate;

                                // DT_R13
                                firstManiR13.LAST_SBN_END_DATE = tempLastSbnEndDate.Equals(DateTime.MinValue) ? null : tempLastSbnEndDate.ToString("yyyyMMdd");
                                firstManiR13.MANIFEST_ID = manifestId;
                                firstManiR13.LAST_SBN_JOU_NAME = groupData[0].Field<string>("LAST_SBN_JOU_NAME");
                                firstManiR13.LAST_SBN_JOU_POST = groupData[0].Field<string>("LAST_SBN_JOU_POST");
                                firstManiR13.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                                firstManiR13.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                                firstManiR13.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                                firstManiR13.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                                firstManiR13.LAST_SBN_JOU_TEL = groupData[0].Field<string>("LAST_SBN_JOU_TEL");
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                DateTime now = this.getDBDateTime();
                                //firstManiR13.CREATE_DATE = DateTime.Now;
                                //firstManiR13.UPDATE_TS = DateTime.Now;
                                firstManiR13.CREATE_DATE = now;
                                firstManiR13.UPDATE_TS = now;
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                                // DT_R13_EX
                                firstManiR13EX.MANIFEST_ID = manifestId;
                                firstManiR13EX.LAST_SBN_GYOUSHA_CD = groupData[0].Field<string>("LAST_SBN_GYOUSHA_CD");
                                firstManiR13EX.LAST_SBN_GENBA_CD = groupData[0].Field<string>("LAST_SBN_GENBA_CD");
                                firstManiR13EX.CREATE_USER = createUser;
                                firstManiR13EX.CREATE_DATE = createDate;
                                firstManiR13EX.CREATE_PC = createPc;
                                firstManiR13EX.UPDATE_USER = SystemProperty.UserName;
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //firstManiR13EX.UPDATE_DATE = DateTime.Now;
                                firstManiR13EX.UPDATE_DATE = now;
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                firstManiR13EX.UPDATE_PC = SystemInformation.ComputerName;
                                firstManiR13EX.DELETE_FLG = false;

                                // DT_R13, DT_R13_EX追加
                                tempR13.Add(firstManiR13);
                                tempR13Ex.Add(firstManiR13EX);

                                recSeq++;
                            }

                            // DT_R18のLAST_SBNEND_REP_FLGを一番最後にチェックするため、DT_R18の追加も一番最後に行う
                            decimal lastSbnEndRepFlg = 0;
                            var blankLastSbnJou = nextManis.Select("ISNULL(LAST_SBN_JOU_NAME, '') = '' OR ISNULL(LAST_SBN_JOU_ADDRESS, '') = ''");
                            var blankLstSbnEndDate = nextManis.Select("LAST_SBN_END_DATE IS NULL");
                            lastSbnEndRepFlg = (nextManis.Rows.Count > 0 && blankLastSbnJou.Count() < 1 && blankLstSbnEndDate.Count() < 1) ? 1 : 0;

                            bool blankLastSbnEndDateFlg = false;
                            for (int i = 0; i < tempR13.Count; i++)
                            {
                                var tmpLastSbnEndDate = tempR13[i].LAST_SBN_END_DATE;
                                DateTime tempDate = DateTime.MinValue;

                                if (tmpLastSbnEndDate != null
                                    && !string.IsNullOrEmpty(tmpLastSbnEndDate.ToString()))
                                {
                                    if (DateTime.Compare(lastSbnEndDate, tempDate.Date) < 0)
                                    {
                                        lastSbnEndDate = tempDate.Date;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(tempR13Ex[i].LAST_SBN_GENBA_CD))
                                    {
                                        blankLastSbnEndDateFlg = true;
                                        break;
                                    }
                                }
                            }

                            R13List.Add(tempR13.ToArray());
                            newR13ExList.Add(tempR13Ex.ToArray());
                            oldR13ExList.Add(oldR13Ex);
                            #endregion

                            #region DT_R13, DT_R13_EX以外の更新データをセット
                            mfToc.LATEST_SEQ = updateLatestSeq;
                            mfTocList.Add(mfToc);

                            r18.SEQ = updateLatestSeq;
                            r18.LAST_SBN_ENDREP_FLAG = lastSbnEndRepFlg;
                            r18.LAST_SBN_END_DATE = null;
                            r18.LAST_SBN_END_REP_DATE = null;
                            if (lastSbnEndRepFlg == 1)
                            {
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //r18.LAST_SBN_END_REP_DATE = DateTime.Now.ToString("yyyyMMdd");
                                r18.LAST_SBN_END_REP_DATE = this.getDBDateTime().ToString("yyyyMMdd");
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            }
                            if (!blankLastSbnEndDateFlg)
                            {
                                r18.LAST_SBN_END_DATE = lastSbnEndDate.Equals(DateTime.MinValue) ? null : lastSbnEndDate.ToString("yyyyMMdd");
                            }
                            else
                            {
                                r18.LAST_SBN_END_DATE = null;
                            }
                            r18List.Add(r18);

                            foreach (var tempR19 in r19)
                            {
                                tempR19.SEQ = updateLatestSeq;
                            }
                            r19List.Add(r19);

                            foreach (var tempR02 in r02)
                            {
                                tempR02.SEQ = updateLatestSeq;
                            }
                            r02List.Add(r02);

                            foreach (var tempR04 in r04)
                            {
                                tempR04.SEQ = updateLatestSeq;
                            }
                            r04List.Add(r04);

                            foreach (var tempR05 in r05)
                            {
                                tempR05.SEQ = updateLatestSeq;
                            }
                            r05List.Add(r05);

                            foreach (var tempR08 in r08)
                            {
                                tempR08.SEQ = updateLatestSeq;
                            }
                            r08List.Add(r08);

                            r18ExList.Add(oldR18Ex);
                            r19ExList.Add(oldR19Ex);
                            r04ExList.Add(oldR04Ex);
                            r08ExList.Add(oldR08Ex);
                            #endregion

                            #endregion
                        }
                        else
                        {
                            #region 紙マニ
                            SqlInt64 First_SysId = SqlInt64.Parse(r["PAPER_SYSTEM_ID"].ToString());
                            SqlInt32 SEQ = SqlInt32.Parse(r["SEQ"].ToString());
                            SqlInt64 First_DetailSysId = SqlInt64.Parse(r["FIRST_SYSTEM_ID"].ToString());
                            SqlInt16 HaikiKbn = SqlInt16.Parse(r["FIRST_HAIKI_KBN_CD"].ToString());

                            if (HaikiKbn != 4)
                            {
                                // 1次マニフェストが電マニの場合は別機能で登録させるため、ここで更新は行わない。

                                // 紐付先の1次マニを取得
                                T_MANIFEST_DETAIL dtl = TMDDao.GetDataForEntity(First_SysId, SEQ, First_DetailSysId);

                                // 紐付先の1次マニを更新（最終処分日、最終処分業者、最終処分場）

                                // 最終処分終了日
                                if (!string.IsNullOrEmpty(this.ManiInfo.dt_r18.LAST_SBN_END_DATE))
                                {
                                    DateTime date;
                                    if (DateTime.TryParseExact(this.ManiInfo.dt_r18.LAST_SBN_END_DATE,
                                                        "yyyyMMdd",
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out date))
                                    {
                                        // 電マニの最終処分日付の形式にあわせる
                                        dtl.LAST_SBN_END_DATE = date;
                                    }
                                    else
                                    {
                                        dtl.LAST_SBN_END_DATE = Convert.ToDateTime(this.ManiInfo.dt_r18.LAST_SBN_END_DATE);
                                    }
                                }
                                else
                                {
                                    dtl.LAST_SBN_END_DATE = SqlDateTime.Null;
                                }

                                // 最終処分業者、最終処分場
                                if (this.ManiInfo.lstDT_R13_EX.Count <= 1)
                                {
                                    // 最終処分業者
                                    if (this.ManiInfo.lstDT_R13_EX.Count == 1
                                        && !string.IsNullOrEmpty(this.ManiInfo.lstDT_R13_EX[0].LAST_SBN_GYOUSHA_CD))
                                    {
                                        dtl.LAST_SBN_GYOUSHA_CD = this.ManiInfo.lstDT_R13_EX[0].LAST_SBN_GYOUSHA_CD;
                                    }
                                    else
                                    {
                                        dtl.LAST_SBN_GYOUSHA_CD = null;
                                    }
                                    // 最終処分現場
                                    if (this.ManiInfo.lstDT_R13_EX.Count == 1
                                        && !string.IsNullOrEmpty(this.ManiInfo.lstDT_R13_EX[0].LAST_SBN_GENBA_CD))
                                    {
                                        dtl.LAST_SBN_GENBA_CD = this.ManiInfo.lstDT_R13_EX[0].LAST_SBN_GENBA_CD;
                                    }
                                    else
                                    {
                                        dtl.LAST_SBN_GENBA_CD = null;
                                    }
                                }
                                else
                                {
                                    // 二次マニフェストの実績が2行以上ある場合
                                    dtl.LAST_SBN_GYOUSHA_CD = null;
                                    dtl.LAST_SBN_GENBA_CD = null;
                                    SqlInt64 Second_SystemID = this.ManiInfo.dt_r18ExOld.SYSTEM_ID;
                                    SqlInt32 Second_SEQ = this.ManiInfo.dt_r18ExOld.SEQ;
                                    DataTable SecondPaperLastsbnInfo = DT_R13_EXDao.GetDataForEntitySecondLastSbnForElecMani(Second_SystemID, Second_SEQ);
                                    if (SecondPaperLastsbnInfo.Rows.Count <= 1)
                                    {
                                        // 全ての行で最終処分業者、最終処分場所が一致している場合
                                        if (SecondPaperLastsbnInfo.Rows.Count > 0)
                                        {
                                            if (!string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString())
                                                && !string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString()))
                                            {
                                                dtl.LAST_SBN_GYOUSHA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                                dtl.LAST_SBN_GENBA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 最終処分業者、最終処分場所が異なる行がある場合
                                        DataView SecondInfoDataView = new DataView(SecondPaperLastsbnInfo);
                                        DataTable tblLastSbnGyousha = SecondInfoDataView.ToTable("DistinctTable", true, new string[] { "LAST_SBN_GYOUSHA_CD" });
                                        if (tblLastSbnGyousha.Rows.Count <= 1)
                                        {
                                            // 最終処分業者が全て同じ場合
                                            dtl.LAST_SBN_GYOUSHA_CD = tblLastSbnGyousha.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                        }
                                    }
                                }
                                listFirstDetailForUpdate.Add(dtl);
                            }
                            #endregion
                        }
                    }
                    using (Transaction tran = new Transaction())
                    {
                        // 一次マニフェスト情報明細更新（最終処分業者、最終処分場、最終処分終了日更新）
                        if (listFirstDetailForUpdate != null)
                        {
                            foreach (var r in listFirstDetailForUpdate)
                            {
                                TMDDao.Update(r);
                            }
                        }

                        // 一次電マニの最終処分情報を更新
                        this.UpdateFirstElecMani(mfTocList, r18List, r19List, r02List, r04List, r05List, r08List, R13List
                            , r18ExList, r19ExList, r04ExList, r08ExList, oldR13ExList, newR13ExList);

                        tran.Commit();
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("UpdateFirstMani", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                this.isRegistErr = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("UpdateFirstMani", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                this.isRegistErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateFirstMani", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                this.isRegistErr = true;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 一次電マニ更新処理
        /// <summary>
        /// 一次電マニ更新処理
        /// ManiRelrationResult#Regist()でDT_R18_EXを更新しているため、このメソッドでは更新しない
        /// </summary>
        /// <param name="mfTocList"></param>
        /// <param name="dtR18List"></param>
        /// <param name="dtR19List"></param>
        /// <param name="dtR02List"></param>
        /// <param name="dtR04List"></param>
        /// <param name="dtR05List"></param>
        /// <param name="dtR08List"></param>
        /// <param name="dtR13List"></param>
        /// <param name="dtR18ExList"></param>
        /// <param name="dtR19ExList"></param>
        /// <param name="dtR04ExList"></param>
        /// <param name="dtR08ExList"></param>
        /// <param name="oldDtR13ExList"></param>
        /// <param name="newDtR13ExList"></param>
        internal void UpdateFirstElecMani(List<DT_MF_TOC> mfTocList, List<DT_R18> dtR18List, List<DT_R19[]> dtR19List,
                                            List<DT_R02[]> dtR02List, List<DT_R04[]> dtR04List, List<DT_R05[]> dtR05List,
                                            List<DT_R08[]> dtR08List, List<DT_R13[]> dtR13List, List<DT_R18_EX> dtR18ExList,
                                            List<DT_R19_EX[]> dtR19ExList, List<DT_R04_EX[]> dtR04ExList, List<DT_R08_EX[]> dtR08ExList,
                                            List<DT_R13_EX[]> oldDtR13ExList, List<DT_R13_EX[]> newDtR13ExList)
        {
            #region Dao生成
            var mfTocDao = DaoInitUtility.GetComponent<CommonDT_MF_TOCDaoCls>();
            var r18Dao = DaoInitUtility.GetComponent<CommonDT_R18DaoCls>();
            var r19Dao = DaoInitUtility.GetComponent<CommonDT_R19DaoCls>();
            var r02Dao = DaoInitUtility.GetComponent<CommonDT_R02DaoCls>();
            var r04Dao = DaoInitUtility.GetComponent<CommonDT_R04DaoCls>();
            var r05Dao = DaoInitUtility.GetComponent<CommonDT_R05DaoCls>();
            var r06Dao = DaoInitUtility.GetComponent<CommonDT_R06DaoCls>();
            var r08Dao = DaoInitUtility.GetComponent<CommonDT_R08DaoCls>();
            var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
            var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
            var r19ExDao = DaoInitUtility.GetComponent<CommonDT_R19_EXDaoCls>();
            var r04ExDao = DaoInitUtility.GetComponent<CommonDT_R04_EXDaoCls>();
            var r08ExDao = DaoInitUtility.GetComponent<CommonDT_R08_EXDaoCls>();
            var r13ExDao = DaoInitUtility.GetComponent<CommonDT_R13_EXDaoCls>();
            #endregion

            // データ追加、更新
            #region XX_EX以外の更新
            foreach (var mfToc in mfTocList)
            {
                mfTocDao.Update(mfToc);
            }

            foreach (var tempR18 in dtR18List)
            {
                r18Dao.Insert(tempR18);
            }

            foreach (var tempR19s in dtR19List)
            {
                foreach (var tempR19 in tempR19s)
                {
                    r19Dao.Insert(tempR19);
                }
            }

            foreach (var tempR02s in dtR02List)
            {
                foreach (var tempR02 in tempR02s)
                {
                    r02Dao.Insert(tempR02);
                }
            }

            foreach (var tempR04s in dtR04List)
            {
                foreach (var tempR04 in tempR04s)
                {
                    r04Dao.Insert(tempR04);
                }
            }

            foreach (var tempR05s in dtR05List)
            {
                foreach (var tempR05 in tempR05s)
                {
                    r05Dao.Insert(tempR05);
                }
            }

            foreach (var tempR08s in dtR08List)
            {
                foreach (var tempR08 in tempR08s)
                {
                    r08Dao.Insert(tempR08);
                }
            }

            foreach (var tempR13s in dtR13List)
            {
                foreach (var tempR13 in tempR13s)
                {
                    r13Dao.Insert(tempR13);
                }
            }
            #endregion

            #region XX_EXの更新
            foreach (var tempR18Ex in dtR18ExList)
            {
                if (tempR18Ex != null)
                {
                    tempR18Ex.DELETE_FLG = true;
                    r18ExDao.Update(tempR18Ex);
                    tempR18Ex.SEQ = tempR18Ex.SEQ + 1;
                    tempR18Ex.UPDATE_USER = SystemProperty.UserName;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //tempR18Ex.UPDATE_DATE = DateTime.Now;
                    tempR18Ex.UPDATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    tempR18Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR18Ex.DELETE_FLG = false;
                    r18ExDao.Insert(tempR18Ex);
                }
            }

            foreach (var tempR19Exs in dtR19ExList)
            {
                foreach (var tempR19Ex in tempR19Exs)
                {
                    tempR19Ex.DELETE_FLG = true;
                    r19ExDao.Update(tempR19Ex);
                    tempR19Ex.SEQ = tempR19Ex.SEQ + 1;
                    tempR19Ex.UPDATE_USER = SystemProperty.UserName;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //tempR19Ex.UPDATE_DATE = DateTime.Now;
                    tempR19Ex.UPDATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    tempR19Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR19Ex.DELETE_FLG = false;
                    r19ExDao.Insert(tempR19Ex);
                }
            }

            foreach (var tempR04Exs in dtR04ExList)
            {
                foreach (var tempR04Ex in tempR04Exs)
                {
                    tempR04Ex.DELETE_FLG = true;
                    r04ExDao.Update(tempR04Ex);
                    tempR04Ex.SEQ = tempR04Ex.SEQ + 1;
                    tempR04Ex.UPDATE_USER = SystemProperty.UserName;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //tempR04Ex.UPDATE_DATE = DateTime.Now;
                    tempR04Ex.UPDATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    tempR04Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR04Ex.DELETE_FLG = false;
                    r04ExDao.Insert(tempR04Ex);
                }
            }

            foreach (var tempR08Exs in dtR08ExList)
            {
                foreach (var tempR08Ex in tempR08Exs)
                {
                    tempR08Ex.DELETE_FLG = true;
                    r08ExDao.Update(tempR08Ex);
                    tempR08Ex.SEQ = tempR08Ex.SEQ + 1;
                    tempR08Ex.UPDATE_USER = SystemProperty.UserName;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //tempR08Ex.UPDATE_DATE = DateTime.Now;
                    tempR08Ex.UPDATE_DATE = this.getDBDateTime();
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    tempR08Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR08Ex.DELETE_FLG = false;
                    r08ExDao.Insert(tempR08Ex);
                }
            }

            foreach (var oldTempR13Exs in oldDtR13ExList)
            {
                foreach (var oldTempR13Ex in oldTempR13Exs)
                {
                    oldTempR13Ex.DELETE_FLG = true;
                    r13ExDao.Update(oldTempR13Ex);
                }
            }

            foreach (var newTempR13Exs in newDtR13ExList)
            {
                foreach (var newTempR13Ex in newTempR13Exs)
                {
                    r13ExDao.Insert(newTempR13Ex);
                }
            }
            #endregion
        }
        #endregion

        #region - 部分更新処理 -
        /// <summary>
        /// 部分更新処理
        /// </summary>
        [Transaction]
        internal bool partialUpdate()
        {
            try
            {
                // DT_R18_EX取得
                var findR18Ex = new DT_R18_EX();
                findR18Ex.KANRI_ID = this.KanriId;
                var r18Ex = this.DT_R18_EXDao.GetDataForEntity(findR18Ex);

                // 該当するDT_R18_EXデータがあった場合、連携データも取得・更新を行う
                if (r18Ex != null)
                {
                    // 交付番号
                    r18Ex.MANIFEST_ID = this.form.cantxt_ManifestNo.Text;

                    // 将軍関連情報の更新
                    // 換算後数量
                    r18Ex.KANSAN_SUU = SqlDecimal.Null;
                    if (null != this.form.cdgv_Haikibutu.Rows[0].Cells["KANSAN_SUU"].Value)
                    {
                        var kansanSuu = this.form.cdgv_Haikibutu.Rows[0].Cells["KANSAN_SUU"].Value.ToString();
                        if (false == string.IsNullOrEmpty(kansanSuu))
                        {
                            r18Ex.KANSAN_SUU = SqlDecimal.Parse(kansanSuu);
                        }
                    }

                    // 減容後数量
                    r18Ex.GENNYOU_SUU = SqlDecimal.Null;
                    if (null != this.form.cdgv_Haikibutu.Rows[0].Cells["GENNYOU_SUU"].Value)
                    {
                        var genyouSuu = this.form.cdgv_Haikibutu.Rows[0].Cells["GENNYOU_SUU"].Value.ToString();
                        if (false == string.IsNullOrEmpty(genyouSuu))
                        {
                            r18Ex.GENNYOU_SUU = SqlDecimal.Parse(genyouSuu);
                        }
                    }

                    // (将軍)処分方法CD
                    r18Ex.SBN_HOUHOU_CD = string.IsNullOrEmpty(this.form.cantxt_Shogun_SBN_houhouCD.Text) ? null : this.form.cantxt_Shogun_SBN_houhouCD.Text;


                    using (Transaction tran = new Transaction())
                    {
                        // DT_R18_EX更新
                        this.updateForR18Ex(r18Ex);

                        // DT_R19_EX更新
                        this.updateForR19Ex(r18Ex);

                        // DT_R04_EX更新
                        this.updateForR04Ex(r18Ex);

                        // DT_R08_EX更新
                        this.updateForR08Ex(r18Ex);

                        // DT_R13_EX更新
                        this.updateForR13Ex(r18Ex);

                        // コミット
                        tran.Commit();

                        // 紐づく1次マニフェストの更新
                        this.UpdateFirstMani();
                        if (this.isRegistErr) { return false; }

                        // 完了メッセージ表示
                        this.msgLogic.MessageBoxShow("I001", "更新");

                        // 権限チェック
                        if (Manager.CheckAuthority("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 画面をクリアし新規入力状態とする。
                            this.ChangeNewWindowMode();
                        }
                        else
                        {
                            // 新規権限が無ければ閉じる
                            this.form.ParentForm.Close();
                        }
                    }
                }
                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("partialUpdate", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("partialUpdate", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("partialUpdate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal bool delChk = false;
        private List<int> haikibutuDelList = new List<int>();
        private List<int> tyukanshoriDelList = new List<int>();
        private List<int> lastSBNbasyoDelList = new List<int>();
        private List<int> unpanInfoDelList = new List<int>();
        private List<int> lastSBNGenbaDelList = new List<int>();

        /// <summary>
        /// 明細削除
        /// </summary>
        internal void DeleteRow()
        {
            // 各明細の削除チェックボックスを確認する。
            this.CheckDeleteRow();
            
            // 各明細の削除にチェックが入っている行がある場合、メッセージを表示する。
            if (this.delChk)
            {
                DialogResult result = this.msgLogic.MessageBoxShowConfirm("行削除を行いますか？", MessageBoxDefaultButton.Button2);
                if (result.Equals(DialogResult.Yes))
                {
                    // 産業廃棄物
                    if (this.haikibutuDelList.Count > 0)
                    {
                        this.DeleteSelectRow(this.haikibutuDelList, 1);
                    }
                    // 中間処理産業廃棄物
                    if (this.tyukanshoriDelList.Count > 0)
                    {
                        this.DeleteSelectRow(this.tyukanshoriDelList, 2);
                    }
                    // 最終処分の場所（予定）
                    if (this.lastSBNbasyoDelList.Count > 0)
                    {
                        this.DeleteSelectRow(this.lastSBNbasyoDelList, 3);
                    }
                    // 運搬情報
                    if (this.unpanInfoDelList.Count > 0)
                    {
                        this.DeleteSelectRow(this.unpanInfoDelList, 4);
                    }
                    // 最終処分事業場（実績）
                    if (this.lastSBNGenbaDelList.Count > 0)
                    {
                        this.DeleteSelectRow(this.lastSBNGenbaDelList, 5);
                    }
                }
            }
        }

        /// <summary>
        /// 各明細の削除チェックボックスを確認する。
        /// </summary>
        public void CheckDeleteRow()
        {
            this.delChk = false;
            this.haikibutuDelList = new List<int>();
            this.tyukanshoriDelList = new List<int>();
            this.lastSBNbasyoDelList = new List<int>();
            this.unpanInfoDelList = new List<int>();
            this.lastSBNGenbaDelList = new List<int>();

            // 産業廃棄物
            foreach (DataGridViewRow row in this.form.cdgv_Haikibutu.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Haikibutu_chb_delete"].Value))
                {
                    this.delChk = true;
                    this.haikibutuDelList.Add(row.Index);
                }
            }
            // 中間処理産業廃棄物
            foreach (DataGridViewRow row in this.form.cdgv_Tyukanshori.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Tyukanshori_chb_delete"].Value))
                {
                    this.delChk = true;
                    this.tyukanshoriDelList.Add(row.Index);
                }
            }
            // 最終処分の場所（予定）
            foreach (DataGridViewRow row in this.form.cdgv_LastSBNbasyo_yotei.Rows)
            {
                if (Convert.ToBoolean(row.Cells["LastSBNbasyo_chb_delete"].Value))
                {
                    this.delChk = true;
                    this.lastSBNbasyoDelList.Add(row.Index);
                }
            }
            // 運搬情報
            foreach (DataGridViewRow row in this.form.cdgv_UnpanInfo.Rows)
            {
                if (Convert.ToBoolean(row.Cells["UnpanInfo_chb_delete"].Value))
                {
                    this.delChk = true;
                    this.unpanInfoDelList.Add(row.Index);
                }
            }
            // 最終処分事業場（実績）
            foreach (DataGridViewRow row in this.form.cdgv_LastSBN_Genba_Jiseki.Rows)
            {
                if (Convert.ToBoolean(row.Cells["LastSBN_Genba_chb_delete"].Value))
                {
                    this.delChk = true;
                    this.lastSBNGenbaDelList.Add(row.Index);
                }
            }
        }

        /// <summary>
        /// 削除チェックが入っている行を削除する。
        /// 行番号がある明細は再設定する。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="shurui"></param>
        internal void DeleteSelectRow(List<int> list, int shurui)
        {
            bool rowNumResetFlg = false;
            DataGridViewRowCollection rows = null;
            switch (shurui)
            {
                case 1:
                    rows = this.form.cdgv_Haikibutu.Rows;
                    break;
                case 2:
                    rows = this.form.cdgv_Tyukanshori.Rows;
                    break;
                case 3:
                    rows = this.form.cdgv_LastSBNbasyo_yotei.Rows;
                    rowNumResetFlg = true;
                    break;
                case 4:
                    rows = this.form.cdgv_UnpanInfo.Rows;
                    rowNumResetFlg = true;
                    break;
                case 5:
                    rows = this.form.cdgv_LastSBN_Genba_Jiseki.Rows;
                    rowNumResetFlg = true;
                    break;
                default:
                    break;
            }

            // 行削除
            int cnt = 0;
            foreach (int i in list)
            {
                if (i == 0)
                {
                    if (!rows[i].IsNewRow)
                    {
                        rows.RemoveAt(i);
                    }
                }
                else
                {
                    if (!rows[i - cnt].IsNewRow)
                    {
                        rows.RemoveAt(i - cnt);
                    }
                }
                cnt++;
            }

            // 明細行が全て削除された場合に新規行を追加する。
            if (rows.Count == 0)
            {
                rows.Add();
            }

            // 行番号の再設定
            if (rowNumResetFlg)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    rows[i].Cells[1].Value = i + 1;
                }
            }
        }

        /// <summary>
        /// 排出事業者加入番号の存在チェック
        /// </summary>
        /// <returns name="bool">TRUE:存在する, FALSE:存在しない</returns>
        /// <remarks>
        /// 排出事業者加入番号がMS_JWNET_MEMBERに存在するかどうかをチェックする
        /// ※尚、チェックが有効なのは、入力区分が自動モード時のみ
        /// </remarks>
        internal bool CheckEdiMemberExistence(out bool catchErr)
        {
            bool bRet = true;
            catchErr = false;

            try
            {
                // 入力区分が自動モードの場合
                if (this.ManiInfo != null && this.ManiInfo.bIsAutoMode == true)
                {
                    // 加入者情報取得
                    var dto = new SearchMasterDataDTOCls();
                    dto.EDI_MEMBER_ID = this.ManiInfo.dt_r18.HST_SHA_EDI_MEMBER_ID;
                    var table = this.QUE_INFODao.GetMS_JWNET_MEMBERInfo(dto);
                    if (table.Rows.Count <= 0)
                    {
                        // 排出事業者加入番号がMS_JWNET_MEMBERに存在しない
                        bRet = false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEdiMemberExistence", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEdiMemberExistence", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }

            return bRet;
        }

        /// <summary>
        /// 部分更新モードへ変更
        /// </summary>
        internal bool setPartialUpdateMode()
        {
            try
            {
                // 修正モード
                this.Mode = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.headerform.windowTypeLabel.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.headerform.windowTypeLabel.Text = WINDOW_TYPE.UPDATE_WINDOW_FLAG.ToTypeString();
                this.headerform.windowTypeLabel.BackColor = WINDOW_TYPE.UPDATE_WINDOW_FLAG.ToLabelColor();
                this.headerform.windowTypeLabel.ForeColor = WINDOW_TYPE.UPDATE_WINDOW_FLAG.ToLabelForeColor();

                // Function有効化
                this.parentbaseform.bt_func2.Enabled = true;    // 新規
                this.parentbaseform.bt_func8.Enabled = true;    // 受渡確認

                // SubFunction有効化
                if (this.maniFlag == 2)
                {
                    // 二次マニ修正時は、マニ紐付を有効化
                    this.parentbaseform.bt_process3.Text = "[3]1次マニ紐付";
                    this.parentbaseform.bt_process3.Tag = "マニフェスト紐付画面に切り替わります";
                    this.parentbaseform.bt_process3.Enabled = true;
                }

                // 換算後数量・(将軍)処分方法のみ有効
                this.form.cdgv_Haikibutu.Columns["KANSAN_SUU"].ReadOnly = false;

                foreach (DataGridViewRow row in this.form.cdgv_Haikibutu.Rows)
                {
                    // 換算後数量項目の背景色を再描画
                    row.Cells["KANSAN_SUU"].UpdateBackColor(false);
                }

                this.form.cantxt_Shogun_SBN_houhouCD.ReadOnly = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setPartialUpdateMode", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 参照モードへ変更
        /// </summary>
        internal void setReferenceMode()
        {
            // 参照モード
            this.Mode = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            this.headerform.windowTypeLabel.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            this.headerform.windowTypeLabel.Text = WINDOW_TYPE.REFERENCE_WINDOW_FLAG.ToTypeString();
            this.headerform.windowTypeLabel.BackColor = WINDOW_TYPE.REFERENCE_WINDOW_FLAG.ToLabelColor();
            this.headerform.windowTypeLabel.ForeColor = WINDOW_TYPE.REFERENCE_WINDOW_FLAG.ToLabelForeColor();
        }

        /// <summary>
        /// DT_R18_EX更新
        /// </summary>
        /// <param name="targetEntity">更新対象Entity</param>
        /// <remarks>
        /// 元のEntityを論理削除⇒SEQ+1として更新データ作成を行う
        /// </remarks>
        private void updateForR18Ex(DT_R18_EX targetEntity)
        {
            // DT_R18_EXの元のEntity取得
            var delEntity = this.DT_R18_EXDao.GetDataForEntity(targetEntity);

            // 元のEntityを論理削除
            delEntity.DELETE_FLG = true;
            this.DT_R18_EXDao.Update(delEntity);

            // 更新対象Entityを追加
            targetEntity.SEQ += 1;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //targetEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            targetEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            targetEntity.UPDATE_USER = SystemProperty.UserName;
            targetEntity.UPDATE_PC = SystemInformation.ComputerName;
            this.DT_R18_EXDao.Insert(targetEntity);
        }

        /// <summary>
        /// DT_R19_EX更新
        /// </summary>
        /// <param name="r18Ex">紐付くDT_R18_EXのEntity</param>
        private void updateForR19Ex(DT_R18_EX r18Ex)
        {
            // SYSTEM_ID, SEQに紐付くEntity取得
            var findEntity = new DT_R19_EX();
            findEntity.SYSTEM_ID = r18Ex.SYSTEM_ID;
            findEntity.SEQ = r18Ex.SEQ - 1;
            var entityList = this.DT_R19_EXDao.GetAllValidData(findEntity);

            if (entityList.Length > 0)
            {
                // 論理削除
                foreach (var delEntity in entityList)
                {
                    delEntity.DELETE_FLG = true;
                    this.DT_R19_EXDao.Update(delEntity);
                }

                // 更新Entityを追加
                foreach (var updEntity in entityList)
                {
                    updEntity.DELETE_FLG = false;
                    updEntity.SEQ += 1;
                    updEntity.MANIFEST_ID = r18Ex.MANIFEST_ID;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //updEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    updEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    updEntity.UPDATE_USER = SystemProperty.UserName;
                    updEntity.UPDATE_PC = SystemInformation.ComputerName;
                    this.DT_R19_EXDao.Insert(updEntity);
                }
            }
        }

        /// <summary>
        /// DT_R04_EX更新
        /// </summary>
        /// <param name="r18Ex">紐付くDT_R18_EXのEntity</param>
        private void updateForR04Ex(DT_R18_EX r18Ex)
        {
            // SYSTEM_ID, SEQに紐付くEntity取得
            var findEntity = new DT_R04_EX();
            findEntity.SYSTEM_ID = r18Ex.SYSTEM_ID;
            findEntity.SEQ = r18Ex.SEQ - 1;
            var entityList = this.DT_R04_EXDao.GetAllValidData(findEntity);

            if (entityList.Length > 0)
            {
                // 論理削除
                foreach (var delEntity in entityList)
                {
                    delEntity.DELETE_FLG = true;
                    this.DT_R04_EXDao.Update(delEntity);
                }

                // 更新Entityを追加
                foreach (var updEntity in entityList)
                {
                    updEntity.DELETE_FLG = false;
                    updEntity.SEQ += 1;
                    updEntity.MANIFEST_ID = r18Ex.MANIFEST_ID;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //updEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    updEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    updEntity.UPDATE_USER = SystemProperty.UserName;
                    updEntity.UPDATE_PC = SystemInformation.ComputerName;
                    this.DT_R04_EXDao.Insert(updEntity);
                }
            }
        }

        /// <summary>
        /// DT_R08_EX更新
        /// </summary>
        /// <param name="r18Ex">紐付くDT_R18_EXのEntity</param>
        private void updateForR08Ex(DT_R18_EX r18Ex)
        {
            // SYSTEM_ID, SEQに紐付くEntity取得
            var findEntity = new DT_R08_EX();
            findEntity.SYSTEM_ID = r18Ex.SYSTEM_ID;
            findEntity.SEQ = r18Ex.SEQ - 1;
            var entityList = this.DT_R08_EXDao.GetAllValidData(findEntity);

            if (entityList.Length > 0)
            {
                // 論理削除
                foreach (var delEntity in entityList)
                {
                    delEntity.DELETE_FLG = true;
                    this.DT_R08_EXDao.Update(delEntity);
                }

                // 更新Entityを追加
                foreach (var updEntity in entityList)
                {
                    updEntity.DELETE_FLG = false;
                    updEntity.SEQ += 1;
                    updEntity.MANIFEST_ID = r18Ex.MANIFEST_ID;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //updEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    updEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    updEntity.UPDATE_USER = SystemProperty.UserName;
                    updEntity.UPDATE_PC = SystemInformation.ComputerName;
                    this.DT_R08_EXDao.Insert(updEntity);
                }
            }
        }

        /// <summary>
        /// DT_R13_EX更新
        /// </summary>
        /// <param name="r18Ex">紐付くDT_R18_EXのEntity</param>
        private void updateForR13Ex(DT_R18_EX r18Ex)
        {
            // SYSTEM_ID, SEQに紐付くEntity取得
            var findEntity = new DT_R13_EX();
            findEntity.SYSTEM_ID = r18Ex.SYSTEM_ID;
            findEntity.SEQ = r18Ex.SEQ - 1;
            var entityList = this.DT_R13_EXDao.GetAllValidData(findEntity);

            if (entityList.Length > 0)
            {
                // 論理削除
                foreach (var delEntity in entityList)
                {
                    delEntity.DELETE_FLG = true;
                    this.DT_R13_EXDao.Update(delEntity);
                }

                // 更新Entityを追加
                foreach (var updEntity in entityList)
                {
                    updEntity.DELETE_FLG = false;
                    updEntity.SEQ += 1;
                    updEntity.MANIFEST_ID = r18Ex.MANIFEST_ID;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //updEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    updEntity.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    updEntity.UPDATE_USER = SystemProperty.UserName;
                    updEntity.UPDATE_PC = SystemInformation.ComputerName;
                    this.DT_R13_EXDao.Insert(updEntity);
                }
            }
        }

        #endregion - 部分更新処理 -

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

        /// <summary>
        /// 排出事業場情報の初期化
        /// </summary>
        internal void InitHstGenbaInfo()
        {
            this.form.cantxt_HaisyutuGenbaCd.Text = string.Empty;
            this.form.ctxt_HaisyutuGenbaName.Text = string.Empty;
            this.form.ctxt_Haisyutu_GenbaZip.Text = string.Empty;
            this.form.cnt_HaisyutuGenbaTel.Text = string.Empty;
            this.form.ctxt_HaisyutuGenbaAddr.Text = string.Empty;
            this.form.ctxt_JIGYOUJYOU_CD.Text = string.Empty;
        }

        /// <summary>
        /// 排出事業場連携情報項目の読込専用制御の設定
        /// </summary>
        /// <param name="isBool"></param>
        internal void SetHstGenbaLikedControlsReadOnlyStatus(bool isBool)
        {
            this.form.ctxt_HaisyutuGenbaName.ReadOnly = isBool;
            this.form.ctxt_Haisyutu_GenbaZip.ReadOnly = isBool;
            this.form.cnt_HaisyutuGenbaTel.ReadOnly = isBool;
            this.form.ctxt_HaisyutuGenbaAddr.ReadOnly = isBool;
            this.form.ctxt_JIGYOUJYOU_CD.ReadOnly = isBool;
        }

        /// <summary>
        /// 非活性時の背景色セット
        /// </summary>
        internal void ReadOnlyColorSet()
        {
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_GYOUSHA_CD"].Style.BackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_GYOUSHA_CD"].Style.SelectionBackColor = Constans.READONLY_COLOR;
　          this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_CD"].Style.BackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_CD"].Style.SelectionBackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_NAME"].Style.BackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_NAME"].Style.SelectionBackColor = Constans.READONLY_COLOR;
　          this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_POST"].Style.BackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_POST"].Style.SelectionBackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_ADDRESS"].Style.BackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_ADDRESS"].Style.SelectionBackColor = Constans.READONLY_COLOR;
　          this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_TEL"].Style.BackColor = Constans.READONLY_COLOR;
            this.form.cdgv_LastSBNbasyo_yotei.Rows[0].Cells["LAST_SBN_JOU_TEL"].Style.SelectionBackColor = Constans.READONLY_COLOR;
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
    }
}
