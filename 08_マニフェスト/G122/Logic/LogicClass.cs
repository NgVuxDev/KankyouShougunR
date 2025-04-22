using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Const;
using System.Linq;
using System.Data.SqlTypes;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタンの設定用ファイルパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Setting.ButtonSetting.xml";

        /// <summary>
        /// Header
        /// </summary>
        private UIHeader header;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable searchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        private DTOClass searchCondition { get; set; }

        ///<summary>
        /// 初期表示のDao
        ///</summary>
        private MOPCDaoCls MOPCDao;

        ///<summary>
        /// パターン一覧のDao
        ///</summary>
        private PIDaoCls PIDao;

        ///<summary>
        /// マニフェストパターン一覧のDao
        ///</summary>
        private MPIDaoCls MPIDao;

        ///<summary>
        /// 車輌マスタのDao
        ///</summary>
        private MSDaoCls MSDao;

        ///<summary>
        /// 廃棄物種類マスタのDao
        ///</summary>
        private MHAIKIDaoCls MHAIKIDao;

        ///<summary>
        /// 業者マスタのDao
        ///</summary>
        private GYOUSHADaoCls GYOUSHADao;

        ///<summary>
        /// 業者マスタのDao
        ///</summary>
        private GENBADaoCls GENBADao;

        ///<summary>
        /// 車輌マスタのDao
        ///</summary>
        private SHARYOUDaoCls SHARYOUDao;

        ///<summary>
        /// 業者マスタのDao
        ///</summary>
        private GAddressDaoCls GAddressDao;

        ///<summary>
        /// 取引先マスタのDao
        ///</summary>
        private TORIHIKISAKIDaoCls TORIHIKISAKIDao;

        //2013.11.23 naitou add 交付番号重複チェック追加 start
        ///<summary>
        /// 交付番号検索のDao
        ///</summary>
        private SearchKohuDaoCls SearchKohuDao;
        //2013.11.23 naitou add 交付番号重複チェック追加 end

        ///<summary>
        /// ComponentResourceManager
        ///</summary>
        private ComponentResourceManager resources;

        /// <summary>
        /// 共通
        /// </summary>
        private ManifestoLogic manifestoLogic;

        /// <summary>
        /// Hashtable
        /// </summary>
        private Hashtable titleTable;

        /// <summary>
        /// Hashtable
        /// </summary>
        private Hashtable titleUpn1Table;

        /// <summary>
        /// Hashtable
        /// </summary>
        private Hashtable titleUpn2Table;

        /// <summary>
        /// Hashtable
        /// </summary>
        private Hashtable titleUpn3Table;

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>マニフェスト情報数量書式CD</summary>
        internal string ManifestSuuryoFormatCD = String.Empty;

        /// <summary>マニフェスト情報数量書式</summary>
        internal string ManifestSuuryoFormat = String.Empty;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.searchCondition = new DTOClass();
            this.MOPCDao = DaoInitUtility.GetComponent<MOPCDaoCls>();
            this.PIDao = DaoInitUtility.GetComponent<PIDaoCls>();
            this.MPIDao = DaoInitUtility.GetComponent<MPIDaoCls>();
            this.MSDao = DaoInitUtility.GetComponent<MSDaoCls>();
            this.MHAIKIDao = DaoInitUtility.GetComponent<MHAIKIDaoCls>();
            this.GYOUSHADao = DaoInitUtility.GetComponent<GYOUSHADaoCls>();
            this.GENBADao = DaoInitUtility.GetComponent<GENBADaoCls>();
            this.GAddressDao = DaoInitUtility.GetComponent<GAddressDaoCls>();
            this.SHARYOUDao = DaoInitUtility.GetComponent<SHARYOUDaoCls>();
            this.TORIHIKISAKIDao = DaoInitUtility.GetComponent<TORIHIKISAKIDaoCls>();
            this.SearchKohuDao = DaoInitUtility.GetComponent<SearchKohuDaoCls>(); //2013.11.23 naitou add 交付番号重複チェック追加 
            this.titleTable = new Hashtable();
            this.titleUpn1Table = new Hashtable();
            this.titleUpn2Table = new Hashtable();
            this.titleUpn3Table = new Hashtable();
            this.resources = new ComponentResourceManager(typeof(UIForm));
            this.manifestoLogic = new ManifestoLogic();
            this.form.TitleList = new ArrayList();
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
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

                //ヘッダーの初期化
                var parentForm = (BusinessBaseForm)this.form.Parent;
                UIHeader targetHeader = (UIHeader)parentForm.headerForm;
                this.header = targetHeader;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //拠点
                this.header.KYOTEN_CD.Text = string.Empty;
                this.header.KYOTEN_NAME.Text = string.Empty;
                this.mlogic.SetKyoten(this.header.KYOTEN_CD, this.header.KYOTEN_NAME);

                //数値フォーマット情報取得
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                ManifestSuuryoFormatCD = mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();
                ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

                ((BusinessBaseForm)this.form.Parent).ProcessButtonPanel.Visible = false;
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //パターン登録(F2)イベント生成
            parentform.bt_func2.Click += new EventHandler(this.form.PatternTouroku);

            //前行コピー(F3)イベント生成
            parentform.bt_func3.Click += new EventHandler(this.form.DataFukusei);

            //項目呼出(F5)イベント生成
            parentform.bt_func5.Click += new EventHandler(this.form.KoumokuYobidasi);

            //パターン呼出(F6)イベント生成
            parentform.bt_func6.Click += new EventHandler(this.form.PatternYobidasi);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentform.bt_func9);
            parentform.bt_func9.Click += new EventHandler(this.form.Regist);
            parentform.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.FormClose);

            if (this.form.NyuryokuIkkatsuItiran.RowCount == 0)
            {
                //パターン登録(F2)
                parentform.bt_func2.Enabled = false;

                //前行コピー(F3)
                parentform.bt_func3.Enabled = false;

                //パターン呼出(F6)
                parentform.bt_func6.Enabled = false;

                //登録ボタン(F9)
                parentform.bt_func9.Enabled = false;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初期表示のデータ取得処理
        /// </summary>
        /// <returns></returns>
        public void SelectItirannData(string shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            try
            {
                // SQL文
                this.searchResult = new DataTable();
                this.searchCondition.shainCd = shainCd;

                this.searchResult = this.MOPCDao.GetDataForEntity(this.searchCondition);
                int count = this.searchResult.Rows.Count;

                //検索結果を設定する
                var table = new DataTable();

                //var table = this.searchResult;
                table = this.searchResult;

                table.BeginLoadData();

                // 項目論理名
                string ronriName = string.Empty;

                // コード項目リスト
                ArrayList listCd = new ArrayList();

                // 表示名称項目リスト
                ArrayList listNm = new ArrayList();

                // 数値項目リスト
                ArrayList listNum = new ArrayList();

                // 入力名称項目リスト
                ArrayList listText = new ArrayList();

                // 日付項目リスト
                ArrayList listDate = new ArrayList();

                // コード項目リストの作成
                CreateListCd(listCd);

                // 表示名称項目リストの作成
                CreateListNm(listNm);

                // 数値項目リストの作成
                CreateListNum(listNum);

                // 入力名称項目リストの作成
                CreateListText(listText);

                // 日付項目リストの作成
                CreateListDate(listDate);

                // Hashtable作成
                CreateHashtable();
                CreateUpn1Table();
                CreateUpn2Table();
                CreateUpn3Table();

                //「削除」項目設定
                this.form.TitleList.Add("削除");
                r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn customCheckBoxColumnCell
                 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
                customCheckBoxColumnCell.PopupTitleLabel = "";
                customCheckBoxColumnCell.ReadOnlyPopUp = false;
                customCheckBoxColumnCell.ViewSearchItem = true;
                customCheckBoxColumnCell.DBFieldsName = "";
                customCheckBoxColumnCell.DisplayItemName = "";
                customCheckBoxColumnCell.ItemDefinedTypes = "";
                customCheckBoxColumnCell.SearchDisplayFlag = 0;
                customCheckBoxColumnCell.ShortItemName = "";
                customCheckBoxColumnCell.DataPropertyName = "";
                customCheckBoxColumnCell.FalseValue = "False";
                customCheckBoxColumnCell.IndeterminateValue = "True";
                customCheckBoxColumnCell.TrueValue = "True";
                customCheckBoxColumnCell.Name = "DEL_FLAG";
                customCheckBoxColumnCell.Tag = "";
                customCheckBoxColumnCell.ReadOnly = false;
                customCheckBoxColumnCell.ThreeState = false;
                customCheckBoxColumnCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                customCheckBoxColumnCell.DividerWidth = 0;
                customCheckBoxColumnCell.FillWeight = 100;
                customCheckBoxColumnCell.Frozen = false;
                customCheckBoxColumnCell.MinimumWidth = 5;
                customCheckBoxColumnCell.Width = 48;
                customCheckBoxColumnCell.DefaultCellStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                customCheckBoxColumnCell.HeaderText = "削除";
                customCheckBoxColumnCell.ToolTipText = "登録しない場合、チェックしてください。";
                customCheckBoxColumnCell.Visible = true;
                this.form.NyuryokuIkkatsuItiran.Columns.Add(customCheckBoxColumnCell);

                // 取得データの件数分
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ronriName = table.Rows[i]["KOUMOKU_RONRI_NAME"].ToString();
                    this.form.TitleList.Add(ronriName);

                    // コード項目の場合
                    if (listCd.Contains(ronriName))
                    {
                        // DgvCustomAlphaNumTextBoxColumnコントロールの作成
                        CreateAlphaNumTextBoxColumn(ronriName);
                    }
                    // 表示名称の場合
                    else if (listNm.Contains(ronriName))
                    {
                        // DgvCustomTextBoxColumnコントロールの作成
                        CreateReadOnlyTextBoxColumn(ronriName);
                    }
                    // 数値項目の場合
                    else if (listNum.Contains(ronriName))
                    {
                        // DgvCustomNumericTextBox2Columnコントロールの作成
                        CreateNumericNumTextBoxColumn(ronriName);
                    }
                    // 入力名称項目の場合
                    else if (listText.Contains(ronriName))
                    {
                        // DgvCustomTextBoxColumnコントロールの作成
                        CreateTextBoxColumn(ronriName);
                    }
                    // 日付項目の場合
                    else if (listDate.Contains(ronriName))
                    {
                        // DgvCustomDataTimeColumnコントロールの作成
                        CreateDataTimeColumn(ronriName);
                    }
                    else
                    {
                        // DgvCustomTextBoxColumnコントロールの作成
                        CreateReadOnlyTextBoxColumn(ronriName);
                    }
                }

                // 運搬区間NOの設定
                if (listCd.Contains(ConstCls.UPN_GYOUSHA_CD_3))
                {
                    this.form.UpnRouteNo = 3;
                }
                else if (listCd.Contains(ConstCls.UPN_GYOUSHA_CD_2))
                {
                    this.form.UpnRouteNo = 2;
                }
                else if (listCd.Contains(ConstCls.UPN_GYOUSHA_CD_1))
                {
                    this.form.UpnRouteNo = 1;
                }

                var hideColumnsName = new[]{
#region 非表示列 M_OUTPUT_PATTERN_COLUMNには定義あり
                    ConstCls.BIKOU
                    ,ConstCls.CHECK_B1
                    ,ConstCls.CHECK_B2
                    ,ConstCls.CHECK_B4
                    ,ConstCls.CHECK_B6
                    ,ConstCls.CHECK_D
                    ,ConstCls.CHECK_E
                    ,ConstCls.GENNYOU_SUU
                    ,ConstCls.HST_GENBA_ADDRESS
                    ,ConstCls.HST_GENBA_POST
                    ,ConstCls.HST_GENBA_TEL
                    ,ConstCls.HST_GYOUSHA_ADDRESS
                    ,ConstCls.HST_GYOUSHA_POST
                    ,ConstCls.HST_GYOUSHA_TEL
                    ,ConstCls.KOUFU_TANTOUSHA
                    ,ConstCls.KOUFU_TANTOUSHA_SHOZOKU
                    ,ConstCls.LAST_SBN_YOTEI_GENBA_NAME
                    ,ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS
                    ,ConstCls.LAST_SBN_YOTEI_GENBA_POST
                    ,ConstCls.LAST_SBN_YOTEI_GENBA_CD
                    ,ConstCls.LAST_SBN_YOTEI_GENBA_TEL
                    ,ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD
                    ,ConstCls.LAST_SBN_YOTEI_KBN
                    ,ConstCls.NISUGATA_CD_RYAKU
                    ,ConstCls.NISUGATA_NAME_RYAKU
                    ,ConstCls.SBN_GYOUSHA_ADDRESS
                    ,ConstCls.SBN_GYOUSHA_POST
                    ,ConstCls.SBN_GYOUSHA_TEL
                    ,ConstCls.SBN_JYURYOU_DATE
                    ,ConstCls.SBN_JYURYOU_TANTOU_CD
                    ,ConstCls.SBN_JYURYOU_TANTOU_NAME
                    ,ConstCls.SBN_JYURYOUSHA_CD
                    ,ConstCls.SBN_JYURYOUSHA_NAME
                    ,ConstCls.SBN_JYUTAKUSHA_CD
                    ,ConstCls.SBN_JYUTAKUSHA_NAME
                    ,ConstCls.SBN_TANTOU_CD
                    ,ConstCls.SBN_TANTOU_NAME
                    ,ConstCls.SHARYOU_CD_1
                    ,ConstCls.SHARYOU_CD_2
                    ,ConstCls.SHARYOU_CD_3
                    ,ConstCls.SHARYOU_NAME_1
                    ,ConstCls.SHARYOU_NAME_2
                    ,ConstCls.SHARYOU_NAME_3
                    ,ConstCls.SHASHU_CD_1
                    ,ConstCls.SHASHU_CD_2
                    ,ConstCls.SHASHU_CD_3
                    ,ConstCls.SHASHU_NAME_1
                    ,ConstCls.SHASHU_NAME_2
                    ,ConstCls.SHASHU_NAME_3
                    ,ConstCls.TMH_KBN_1
                    ,ConstCls.TMH_KBN_2
                    ,ConstCls.TMH_KBN_3
                    ,ConstCls.TMH_KBN_NAME_1
                    ,ConstCls.TMH_KBN_NAME_2
                    ,ConstCls.TMH_KBN_NAME_3
                    ,ConstCls.TORIHIKISAKI_CD
                    ,ConstCls.TORIHIKISAKI_NAME
                    ,ConstCls.UNPAN_HOUHOU_NAME_1
                    ,ConstCls.UNPAN_HOUHOU_NAME_2
                    ,ConstCls.UNPAN_HOUHOU_NAME_3
                    ,ConstCls.UNTENSHA_CD_1
                    ,ConstCls.UNTENSHA_CD_2
                    ,ConstCls.UNTENSHA_CD_3
                    ,ConstCls.UNTENSHA_NAME_1
                    ,ConstCls.UNTENSHA_NAME_2
                    ,ConstCls.UNTENSHA_NAME_3
                    ,ConstCls.UPN_END_DATE_1
                    ,ConstCls.UPN_END_DATE_2
                    ,ConstCls.UPN_END_DATE_3
                    ,ConstCls.UPN_GYOUSHA_ADDRESS_1
                    ,ConstCls.UPN_GYOUSHA_ADDRESS_2
                    ,ConstCls.UPN_GYOUSHA_ADDRESS_3
                    ,ConstCls.UPN_GYOUSHA_CD_2
                    ,ConstCls.UPN_GYOUSHA_CD_3
                    ,ConstCls.UPN_GYOUSHA_NAME_2
                    ,ConstCls.UPN_GYOUSHA_NAME_3
                    ,ConstCls.UPN_GYOUSHA_POST_1
                    ,ConstCls.UPN_GYOUSHA_POST_2
                    ,ConstCls.UPN_GYOUSHA_POST_3
                    ,ConstCls.UPN_GYOUSHA_TEL_1
                    ,ConstCls.UPN_GYOUSHA_TEL_2
                    ,ConstCls.UPN_GYOUSHA_TEL_3
                    ,ConstCls.UPN_HOUHOU_CD_1
                    ,ConstCls.UPN_HOUHOU_CD_2
                    ,ConstCls.UPN_HOUHOU_CD_3
                    ,ConstCls.UPN_JYUTAKUSHA_CD_1
                    ,ConstCls.UPN_JYUTAKUSHA_CD_2
                    ,ConstCls.UPN_JYUTAKUSHA_CD_3
                    ,ConstCls.UPN_JYUTAKUSHA_NAME_1
                    ,ConstCls.UPN_JYUTAKUSHA_NAME_2
                    ,ConstCls.UPN_JYUTAKUSHA_NAME_3
                    ,ConstCls.UPN_ROUTE_NO_1
                    ,ConstCls.UPN_ROUTE_NO_2
                    ,ConstCls.UPN_ROUTE_NO_3
                    ,ConstCls.UPN_SAKI_GENBA_ADDRESS_1
                    ,ConstCls.UPN_SAKI_GENBA_ADDRESS_2
                    ,ConstCls.UPN_SAKI_GENBA_ADDRESS_3
                    //,ConstCls.UPN_SAKI_GENBA_CD_1
                    ,ConstCls.UPN_SAKI_GENBA_CD_2
                    ,ConstCls.UPN_SAKI_GENBA_CD_3
                    //,ConstCls.UPN_SAKI_GENBA_NAME_1
                    ,ConstCls.UPN_SAKI_GENBA_NAME_2
                    ,ConstCls.UPN_SAKI_GENBA_NAME_3
                    ,ConstCls.UPN_SAKI_GENBA_POST_1
                    ,ConstCls.UPN_SAKI_GENBA_POST_2
                    ,ConstCls.UPN_SAKI_GENBA_POST_3
                    ,ConstCls.UPN_SAKI_GENBA_TEL_1
                    ,ConstCls.UPN_SAKI_GENBA_TEL_2
                    ,ConstCls.UPN_SAKI_GENBA_TEL_3
                    ,ConstCls.UPN_SAKI_GYOUSHA_CD_1
                    ,ConstCls.UPN_SAKI_GYOUSHA_CD_2
                    ,ConstCls.UPN_SAKI_GYOUSHA_CD_3
                    //,ConstCls.UPN_SAKI_KBN_1
                    ,ConstCls.UPN_SAKI_KBN_2
                    ,ConstCls.UPN_SAKI_KBN_3
                    //,ConstCls.UPN_SAKI_KBN_NAME_1
                    ,ConstCls.UPN_SAKI_KBN_NAME_2
                    ,ConstCls.UPN_SAKI_KBN_NAME_3
                    ,ConstCls.HAIKI_CD_RYAKU
                    ,ConstCls.HAIKI_NAME_RYAKU
                    ,"パターン名"
                    ,ConstCls.TMH_GENBA_POST
                    ,ConstCls.TMH_GENBA_TEL
                    ,ConstCls.TMH_GENBA_ADDRESS
                    ,"拠点CD"
                    ,"拠点名"
#endregion
                };

                foreach (DataGridViewColumn col in this.form.NyuryokuIkkatsuItiran.Columns)
                {
                    if (hideColumnsName.Contains(col.HeaderText))
                    {
                        col.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Error(ex);
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(shainCd);
        }

        /// <summary>
        /// パターン一覧画面からデータ取得処理
        /// </summary>
        /// <returns></returns>
        public bool KoumokuYobidasi(string systemId)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(systemId);

                // SQL文
                this.searchResult = new DataTable();
                this.searchCondition.systemId = systemId;

                this.searchResult = this.PIDao.GetDataForEntity(this.searchCondition);
                int count = this.searchResult.Rows.Count;

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                // 項目論理名
                string ronriName = string.Empty;

                // コード項目リスト
                ArrayList listCd = new ArrayList();

                // 表示名称項目リスト
                ArrayList listNm = new ArrayList();

                // 数値項目リスト
                ArrayList listNum = new ArrayList();

                // 入力名称項目リスト
                ArrayList listText = new ArrayList();

                // 日付項目リスト
                ArrayList listDate = new ArrayList();

                // コード項目リストの作成
                CreateListCd(listCd);

                // 表示名称項目リストの作成
                CreateListNm(listNm);

                // 数値項目リストの作成
                CreateListNum(listNum);

                // 入力名称項目リストの作成
                CreateListText(listText);

                // 日付項目リストの作成
                CreateListDate(listDate);

                this.form.NyuryokuIkkatsuItiran.Rows.Clear();
                this.form.NyuryokuIkkatsuItiran.Columns.Clear();
                this.form.TitleList = new ArrayList();

                // 取得データの件数分
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ronriName = table.Rows[i]["KOUMOKU_RONRI_NAME"].ToString();
                    this.form.TitleList.Add(ronriName);

                    // コード項目の場合
                    if (listCd.Contains(ronriName))
                    {
                        // DgvCustomAlphaNumTextBoxColumnコントロールの作成
                        CreateAlphaNumTextBoxColumn(ronriName);
                    }
                    // 表示名称の場合
                    else if (listNm.Contains(ronriName))
                    {
                        // DgvCustomTextBoxColumnコントロールの作成
                        CreateReadOnlyTextBoxColumn(ronriName);
                    }
                    // 数値項目の場合
                    else if (listNum.Contains(ronriName))
                    {
                        // DgvCustomNumericTextBox2Columnコントロールの作成
                        CreateNumericNumTextBoxColumn(ronriName);
                    }
                    // 入力名称項目の場合
                    else if (listText.Contains(ronriName))
                    {
                        // DgvCustomTextBoxColumnコントロールの作成
                        CreateTextBoxColumn(ronriName);
                    }
                    // 日付項目の場合
                    else if (listDate.Contains(ronriName))
                    {
                        // DgvCustomDataTimeColumnコントロールの作成
                        CreateDataTimeColumn(ronriName);
                    }
                    else
                    {
                        // DgvCustomTextBoxColumnコントロールの作成
                        CreateReadOnlyTextBoxColumn(ronriName);
                    }
                }

                // 運搬区間NOの設定
                if (listCd.Contains(ConstCls.UPN_GYOUSHA_CD_3))
                {
                    this.form.UpnRouteNo = 3;
                }
                else if (listCd.Contains(ConstCls.UPN_GYOUSHA_CD_2))
                {
                    this.form.UpnRouteNo = 2;
                }
                else if (listCd.Contains(ConstCls.UPN_GYOUSHA_CD_1))
                {
                    this.form.UpnRouteNo = 1;
                }

                BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;
                //パターン登録(F2)
                parentform.bt_func2.Enabled = true;

                //前行コピー(F3)
                parentform.bt_func3.Enabled = true;

                //パターン呼出(F6)
                parentform.bt_func6.Enabled = true;

                //登録ボタン(F9)
                parentform.bt_func9.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("KoumokuYobidasi", ex);
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
        /// 換算後数量の処理
        /// </summary>
        /// <returns></returns>
        public bool SelectKansanData(int rowIndex)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(rowIndex);

                ManifestoLogic maniLogic = new ManifestoLogic();

                //マニフェスト種類が未入力の場合は処理しない
                if (this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_SHURUI_CD, rowIndex].Value == null)
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if ((r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_SHURUI_CD_RYAKU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_CD_RYAKU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_SUU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.NISUGATA_CD_RYAKU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UNIT_CD_RYAKU] == null ||
                    this.ManifestSuuryoFormatCD == null ||
                    this.ManifestSuuryoFormat == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU] == null
                )
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                maniLogic.SetKansanti(this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_SHURUI_CD, rowIndex].Value.ToString(),
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.HAIKI_SHURUI_CD_RYAKU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.HAIKI_CD_RYAKU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.HAIKI_SUU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.NISUGATA_CD_RYAKU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.UNIT_CD_RYAKU, rowIndex],
                    this.ManifestSuuryoFormatCD,
                    this.ManifestSuuryoFormat,
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectKansanData", ex);
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
        /// 換算減容のデータ取得処理
        /// </summary>
        /// <returns></returns>
        public bool SelectGenyouData(int rowIndex)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(rowIndex);

                ManifestoLogic maniLogic = new ManifestoLogic();

                //マニフェスト種類が未入力の場合は処理しない
                if (this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_SHURUI_CD, rowIndex].Value == null)
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if ((r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_SHURUI_CD_RYAKU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_CD_RYAKU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_SUU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.NISUGATA_CD_RYAKU] == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UNIT_CD_RYAKU] == null ||
                    this.ManifestSuuryoFormatCD == null ||
                    this.ManifestSuuryoFormat == null ||
                    (r_framework.CustomControl.DgvCustomTextBoxColumn)this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU] == null
                    )
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                maniLogic.SetGenyouti(this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_SHURUI_CD, rowIndex].Value.ToString(),
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.HAIKI_SHURUI_CD_RYAKU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.HAIKI_CD_RYAKU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.SHOBUN_HOUHOU_CD_RYAKU, rowIndex],
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex],
                    this.ManifestSuuryoFormatCD,
                    this.ManifestSuuryoFormat,
                    (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SelectGenyouData", ex);
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
        /// 車輛ポップアップ後設定処理
        /// </summary>
        /// <returns></returns>
        public bool SetSharyouPopUp(string popKubunn)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart(popKubunn);

                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                if ("1".Equals(popKubunn))
                {
                    // 業者CD
                    this.searchCondition.gyoushaCd
                        = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_1].Value.ToString().PadLeft(6, '0');
                    // 車輌CD
                    this.searchCondition.sharyouCd
                        = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_1].Value.ToString().PadLeft(6, '0');
                }

                if ("2".Equals(popKubunn))
                {
                    // 業者CD
                    this.searchCondition.gyoushaCd
                        = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value.ToString().PadLeft(6, '0');
                    // 車輌CD
                    this.searchCondition.sharyouCd
                        = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value.ToString().PadLeft(6, '0');
                }

                if ("3".Equals(popKubunn))
                {
                    // 業者CD
                    this.searchCondition.gyoushaCd
                        = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value.ToString().PadLeft(6, '0');
                    // 車輌CD
                    this.searchCondition.sharyouCd
                        = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value.ToString().PadLeft(6, '0');
                }

                this.searchResult = this.MSDao.GetDataForEntity(this.searchCondition);

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if ("1".Equals(popKubunn) && this.form.TitleList.Contains(ConstCls.SHASHU_CD_1))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_1].Value = table.Rows[i]["SHASHU_CD"];
                    }
                    if ("1".Equals(popKubunn) && this.form.TitleList.Contains(ConstCls.SHASHU_NAME_1))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_1].Value = table.Rows[i]["SHASHU_NAME_RYAKU"];
                    }
                    if ("2".Equals(popKubunn) && this.form.TitleList.Contains(ConstCls.SHASHU_CD_2))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].Value = table.Rows[i]["SHASHU_CD"];
                    }
                    if ("2".Equals(popKubunn) && this.form.TitleList.Contains(ConstCls.SHASHU_NAME_2))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_2].Value = table.Rows[i]["SHASHU_NAME_RYAKU"];
                    }
                    if ("3".Equals(popKubunn) && this.form.TitleList.Contains(ConstCls.SHASHU_CD_3))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].Value = table.Rows[i]["SHASHU_CD"];
                    }
                    if ("3".Equals(popKubunn) && this.form.TitleList.Contains(ConstCls.SHASHU_NAME_3))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_3].Value = table.Rows[i]["SHASHU_NAME_RYAKU"];
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSharyouPopUp", ex);
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
        /// 車種ポップアップ後設定処理
        /// </summary>
        /// <returns></returns>
        public bool SetShashuPopUp(string popKubunn)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart(popKubunn);

                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                if ("1".Equals(popKubunn))
                {
                    if (this.form.TitleList.Contains(ConstCls.SHASHU_NAME_1))
                    {
                        if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_1].Value == null ||
                            string.Empty.Equals(this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_1].Value.ToString()))
                        {
                            if (this.form.TitleList.Contains(ConstCls.SHARYOU_CD_1))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_1].Value = null;
                            }
                            if (this.form.TitleList.Contains(ConstCls.SHARYOU_NAME_1))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                            }
                        }
                    }
                }

                if ("2".Equals(popKubunn))
                {
                    if (this.form.TitleList.Contains(ConstCls.SHASHU_NAME_2))
                    {
                        if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_2].Value == null ||
                            string.Empty.Equals(this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_2].Value.ToString()))
                        {
                            if (this.form.TitleList.Contains(ConstCls.SHARYOU_CD_2))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value = null;
                            }
                            if (this.form.TitleList.Contains(ConstCls.SHARYOU_NAME_2))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                            }
                        }
                    }
                }

                if ("3".Equals(popKubunn))
                {
                    if (this.form.TitleList.Contains(ConstCls.SHASHU_NAME_3))
                    {
                        if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_3].Value == null ||
                            string.Empty.Equals(this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_3].Value.ToString()))
                        {
                            if (this.form.TitleList.Contains(ConstCls.SHARYOU_CD_3))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value = null;
                            }
                            if (this.form.TitleList.Contains(ConstCls.SHARYOU_NAME_3))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShashuPopUp", ex);
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
        /// 取引先マスタのチェック
        /// </summary>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        public bool ChkTorihikisaki(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart();

                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                string date = ((BusinessBaseForm)this.form.Parent).sysDate.ToString();
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value != null)
                {
                    date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                }

                sql.Append(" select M_TORIHIKISAKI.TORIHIKISAKI_CD, ");
                sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU ");
                sql.Append(" FROM M_TORIHIKISAKI ");
                sql.AppendFormat(" WHERE ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111),120) ", date);
                sql.AppendFormat(" and CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.AppendFormat(" TEKIYOU_END) or (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar,'{0}', 111), 120) ", date);
                sql.Append(" and TEKIYOU_END IS NULL) or (TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" and CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <=TEKIYOU_END) ", date);
                sql.Append(" or (TEKIYOU_BEGIN IS NULL and TEKIYOU_END IS NULL)) ");
                sql.Append(" AND DELETE_FLG = 0 ");
                sql.Append(" AND M_TORIHIKISAKI.TORIHIKISAKI_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_CD].Value.ToString().PadLeft(6, '0') + "'");

                this.searchResult = this.TORIHIKISAKIDao.GetDataForEntity(sql.ToString());

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_CD].Value =
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_CD].Value.ToString().PadLeft(6, '0').ToUpper();

                if (table.Rows.Count > 0)
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_NAME].Value
                        = table.Rows[0]["TORIHIKISAKI_NAME_RYAKU"];
                }
                else
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_NAME].Value = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkTorihikisaki", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物種類のチェック
        /// </summary>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        public bool ChkGridHaiki(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart();
                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();
                // 廃棄物区分CD
                this.searchCondition.haikiKbnCd
                    = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_SHURUI_CD].Value.ToString();
                // 廃棄物種類CD
                this.searchCondition.haikiShuruiCd
                    = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SHURUI_CD_RYAKU].Value.ToString().PadLeft(4, '0');

                this.searchResult = this.MHAIKIDao.GetDataForEntity(this.searchCondition);

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                if (table.Rows.Count > 0)
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SHURUI_CD_RYAKU].Value
                        = table.Rows[0]["HAIKI_SHURUI_CD"];
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SHURUI_NAME_RYAKU].Value
                        = table.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"];
                }
                else
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SHURUI_NAME_RYAKU].Value = null;
                    return false;
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
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
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
                LogUtility.DebugMethodStart(iRow);

                if (String.IsNullOrEmpty(Convert.ToString(this.form.NyuryokuIkkatsuItiran.Rows[iRow].Cells[ConstCls.HAIKI_SUU].Value)))
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                decimal d = 0;
                String Suryo = (Convert.ToString(this.form.NyuryokuIkkatsuItiran.Rows[iRow].Cells[ConstCls.HAIKI_SUU].Value).Replace(",", ""));
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
        /// 業者マスタのチェック（実質、最終処分業者CD用
        /// </summary>
        /// <param name="titleName"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        public bool ChkGyousha(string titleName, out bool catchErr)
        {
            bool result = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(titleName);
                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                string date = ((BusinessBaseForm)this.form.Parent).sysDate.ToString();
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value != null)
                {
                    date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                }

                sql.Append(" SELECT M_GYOUSHA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_ADDRESS1, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_TEL ");
                sql.Append(" FROM M_GYOUSHA LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_CD ");
                sql.Append(" AND M_TODOUFUKEN.DELETE_FLG = 0 ");
                if (titleName == ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD || titleName == ConstCls.LAST_SBN_GYOUSHA_CD)
                {
                    sql.Append(" INNER JOIN M_GENBA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                }
                sql.AppendFormat(" WHERE ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', ",date);
                sql.Append(" 111), 120) ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 ");

                sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True') ");
                sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                sql.Append(" AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True') ");
                sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[titleName].Value.ToString().PadLeft(6, '0') + "'");

                this.searchResult = this.GYOUSHADao.GetDataForEntity(sql.ToString());

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                var colIdx = this.form.NyuryokuIkkatsuItiran.Columns[titleName].Index;
                if (table.Rows.Count > 0)
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value = table.Rows[0]["GYOUSHA_CD"];
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx + 1].Value = table.Rows[0]["GYOUSHA_NAME_RYAKU"];
                }
                else
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value = null;
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx + 1].Value = null;
                    return result;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyousha", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 現場マスタのチェック(実質、最終処分現場CD用
        /// </summary>
        /// <param name="titleName"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        public bool ChkGenba(string titleName, out bool catchErr)
        {
            bool result = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(titleName);
                var msgLogic = new MessageBoxShowLogic();

                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                var colIdx = this.form.NyuryokuIkkatsuItiran.Columns[titleName].Index;

                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                string date = ((BusinessBaseForm)this.form.Parent).sysDate.ToString();
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value != null)
                {
                    date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                }

                sql.Append(" SELECT M_GENBA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_CD, ");
                sql.Append(" M_GENBA.GENBA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_FURIGANA, ");
                sql.Append(" M_GENBA.GENBA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_ADDRESS1, ");
                sql.Append(" M_GENBA.GENBA_TEL ");
                sql.Append(" FROM M_GENBA LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.AppendFormat(" AND ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', ", date);
                sql.Append(" 111), 120) ");
                sql.AppendFormat(" and CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) or (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" and M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" and CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" and M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD ");
                sql.Append("  = M_TODOUFUKEN.TODOUFUKEN_CD ");
                sql.Append(" AND M_TODOUFUKEN.DELETE_FLG = 0 ");
                sql.AppendFormat(" WHERE ((M_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', ", date);
                sql.Append(" 111), 120) ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GENBA.TEKIYOU_END) OR (M_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" AND M_GENBA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GENBA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GENBA.TEKIYOU_END) ");
                sql.Append(" OR (M_GENBA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GENBA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GENBA.DELETE_FLG = 0 ");

                // 業者未指定
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx - 2].Value == null)
                {
                    // MsgBoxが2回出てしまうのでとりあえず無し
                    //msgLogic.MessageBoxShow("E034", "最終処分業者");
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value = null;
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx + 1].Value = null;
                    return result;
                }
                sql.Append(" AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True')");
                sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True') ");
                sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                sql.Append(" AND M_GENBA.GYOUSHA_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx - 2].Value.ToString().PadLeft(6, '0') + "'");
                sql.Append(" AND M_GENBA.GENBA_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value.ToString().PadLeft(6, '0') + "'");

                this.searchResult = this.GENBADao.GetDataForEntity(sql.ToString());

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                if (table.Rows.Count > 0)
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value = table.Rows[0]["GENBA_CD"];
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx + 1].Value = table.Rows[0]["GENBA_NAME_RYAKU"];
                }
                else
                {
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value = null;
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx + 1].Value = null;

                    return result;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 車輌マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkSharyou(string titleName, out bool catchErr)
        {
            bool result = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(titleName);
                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                string date = ((BusinessBaseForm)this.form.Parent).sysDate.ToString();
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value != null)
                {
                    date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                }

                sql.Append(" select M_SHARYOU.SHARYOU_CD, ");
                sql.Append(" M_SHARYOU.GYOUSHA_CD, ");
                sql.Append(" M_SHARYOU.SHARYOU_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU ");
                sql.Append(" FROM M_SHARYOU LEFT OUTER JOIN M_GYOUSHA ON M_SHARYOU.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.AppendFormat(" AND ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG <> '1' ");
                sql.Append(" WHERE M_SHARYOU.DELETE_FLG = 0 ");

                // 区間1：車輌CD
                if (titleName.Equals(ConstCls.SHARYOU_CD_1))
                {
                    sql.Append(" AND M_SHARYOU.SHARYOU_CD = '"
                        + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_1].Value.ToString().PadLeft(6, '0') + "'");
                }
                // 区間2：車輌CD
                if (titleName.Equals(ConstCls.SHARYOU_CD_2))
                {
                    sql.Append(" AND M_SHARYOU.SHARYOU_CD = '"
                        + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value.ToString().PadLeft(6, '0') + "'");
                }
                // 区間3：車輌CD
                if (titleName.Equals(ConstCls.SHARYOU_CD_3))
                {
                    sql.Append(" AND M_SHARYOU.SHARYOU_CD = '"
                        + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value.ToString().PadLeft(6, '0') + "'");
                }

                this.searchResult = this.SHARYOUDao.GetDataForEntity(sql.ToString());

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                if (table.Rows.Count > 0)
                {
                    // 区間1：車輌CD
                    if (titleName.Equals(ConstCls.SHARYOU_CD_1))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_1].Value
                            = table.Rows[0]["GYOUSHA_CD"];
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_1].Value
                            = table.Rows[0]["GYOUSHA_NAME_RYAKU"];
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_1].Value
                            = table.Rows[0]["SHARYOU_NAME_RYAKU"];

                        if (!this.SetSharyouPopUp("1"))
                        {
                            return false;
                        }
                    }
                    // 区間2：車輌CD
                    else if (titleName.Equals(ConstCls.SHARYOU_CD_2))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value
                            = table.Rows[0]["GYOUSHA_CD"];
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_2].Value
                            = table.Rows[0]["GYOUSHA_NAME_RYAKU"];
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value
                            = table.Rows[0]["SHARYOU_NAME_RYAKU"];

                        if (!this.SetSharyouPopUp("2"))
                        {
                            return false;
                        }
                    }
                    // 区間3：車輌CD
                    else if (titleName.Equals(ConstCls.SHARYOU_CD_3))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value
                            = table.Rows[0]["GYOUSHA_CD"];
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value
                            = table.Rows[0]["GYOUSHA_NAME_RYAKU"];
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value
                            = table.Rows[0]["SHARYOU_NAME_RYAKU"];

                        if (!this.SetSharyouPopUp("3"))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // 区間1：車輌CD
                    if (titleName.Equals(ConstCls.SHARYOU_CD_1))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                    }
                    // 区間2：車輌CD
                    else if (titleName.Equals(ConstCls.SHARYOU_CD_2))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                    }
                    // 区間3：車輌CD
                    else if (titleName.Equals(ConstCls.SHARYOU_CD_3))
                    {
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                    }
                    return result;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkSharyou", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// マニフェストパターン一覧画面からデータ取得処理
        /// </summary>
        /// <returns></returns>
        public void PatternYobidasi(string systemId)
        {
            LogUtility.DebugMethodStart(systemId);

            try
            {
                int rowIndex = 0;
                // カラム名
                string columnName = string.Empty;

                ArrayList kansanList = new ArrayList();

                if (this.form.NyuryokuIkkatsuItiran.CurrentRow.Index == this.form.NyuryokuIkkatsuItiran.RowCount - 1)
                {
                    this.form.NyuryokuIkkatsuItiran.Rows.Add();
                    rowIndex = this.form.NyuryokuIkkatsuItiran.RowCount - 2;
                }
                else
                {
                    rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                }

                // 運搬区間
                string upnRouteNo = string.Empty;

                // SQL文
                this.searchResult = new DataTable();
                this.searchCondition.systemId = systemId;

                this.searchResult = this.MPIDao.GetDataForEntity(this.searchCondition);
                int count = this.searchResult.Rows.Count;

                //検索結果を設定する
                var table = this.searchResult;
                table.BeginLoadData();

                // 取得データの件数分
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    upnRouteNo = table.Rows[i]["UPN_ROUTE_NO"].ToString();

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        columnName = table.Columns[j].ToString();

                        // 区間1（運搬区間）の場合
                        if ("1".Equals(upnRouteNo))
                        {
                            if (titleTable.ContainsKey(columnName)
                                && this.form.TitleList.Contains(titleTable[columnName].ToString()))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[titleTable[columnName].ToString()].Value
                                    = table.Rows[i][columnName].ToString();
                                kansanList.Add(titleTable[columnName].ToString());
                            }
                            else if (titleUpn1Table.ContainsKey(columnName)
                                && this.form.TitleList.Contains(titleUpn1Table[columnName].ToString()))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[titleUpn1Table[columnName].ToString()].Value
                                    = table.Rows[i][columnName].ToString();
                            }
                        }
                        // 区間2（運搬区間）の場合
                        else if ("2".Equals(upnRouteNo))
                        {
                            if (titleUpn2Table.ContainsKey(columnName)
                                && this.form.TitleList.Contains(titleUpn2Table[columnName].ToString()))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[titleUpn2Table[columnName].ToString()].Value
                                    = table.Rows[i][columnName].ToString();
                            }
                        }
                        // 区間3（運搬区間）の場合
                        else if ("3".Equals(upnRouteNo))
                        {
                            if (titleUpn3Table.ContainsKey(columnName)
                                && this.form.TitleList.Contains(titleUpn3Table[columnName].ToString()))
                            {
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[titleUpn3Table[columnName].ToString()].Value
                                    = table.Rows[i][columnName].ToString();
                            }
                        }
                    }
                }

                //換算後数量の算出
                if (!this.SelectKansanData(rowIndex)) { return; }

                //減容後数量の算出
                if (!this.SelectGenyouData(rowIndex)) { return; }

                //交付番号の入力制御
                if (this.form.NyuryokuIkkatsuItiran[ConstCls.KOUFU_KBN, rowIndex].Value == null ||
                    string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.KOUFU_KBN, rowIndex].Value.ToString()))
                {
                    //空の場合　読み取り専用
                    this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_ID, rowIndex].ReadOnly = true;
                }
                else
                {
                    //入力有の場合　編集可能に
                    this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_ID, rowIndex].ReadOnly = false;

                    var cell = (r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxCell)this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_ID, rowIndex];
                    var col = (r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn)cell.OwningColumn;


                    if (string.Equals(this.form.NyuryokuIkkatsuItiran[ConstCls.KOUFU_KBN, rowIndex].Value.ToString(),"1"))
                    {
                        //通常は11ケタ
                        cell.CharactersNumber = 11;
                        cell.MaxInputLength = 11;
                        //cell.CharactersNumber = false; //alphabet入力不可
                        cell.Tag = "半角11桁以内で入力してください";
                    }
                    else
                    {
                        //hack:例外は20ケタだが 今は11で仮置き
                        cell.CharactersNumber = 11;
                        cell.MaxInputLength = 11;
                        //cell.AlphabetLimitFlag = true; //alphabet入力可
                        //cell.ChangeUpperCase = true; //自動大文字変換
                        cell.Tag = "半角11桁以内で入力してください";
                    }
                }

            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(systemId);
            }
        }

        /// <summary>
        /// パターン登録
        /// </summary>
        public bool PatternTouroku(ref List<T_MANIFEST_PT_ENTRY> entryList,
                             ref List<T_MANIFEST_PT_UPN> upnList,
                             ref List<T_MANIFEST_PT_DETAIL> detailList)
        {
            bool ret = true;
            try
            {
                //LogUtility.DebugMethodStart(entryList, upnList, detailList);

                // パターン登録
                this.MakePatternData(ref entryList, ref upnList, ref detailList);

                //LogUtility.DebugMethodEnd(entryList, upnList, detailList);
            }
            catch (Exception ex)
            {
                LogUtility.Error("PatternTouroku", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }


        /// <summary>
        /// 登録前確認処理
        /// </summary>
        internal Boolean RegistCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                int rowCount = 0;
                int colManiShuruiCD = this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index;
                int colManiID = this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_ID].Index;
                string maniShurui = string.Empty;
                string koufuNo = string.Empty;
                string currentManiShurui = string.Empty;
                string currentkoufuNo = string.Empty;

                //長さチェック

                //名称（列）リスト
                var lNameCol = new List<int>();

                //住所（列）リスト
                var lAddCol = new List<int>();

                // 排出事業者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GYOUSHA_NAME] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GYOUSHA_NAME].Index);
                }

                // 排出事業者住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GYOUSHA_ADDRESS] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GYOUSHA_ADDRESS].Index);
                }

                // 排出事業場名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GENBA_NAME] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GENBA_NAME].Index);
                }

                // 排出事業場住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GENBA_ADDRESS] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GENBA_ADDRESS].Index);
                }

                // 最終処分の場所（予定）現場名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.LAST_SBN_YOTEI_GENBA_NAME] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Index);
                }

                // 最終処分の場所（予定）現場住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS].Index);
                }

                // 処分受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_GYOUSHA_NAME] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_GYOUSHA_NAME].Index);
                }

                // 処分受託者住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_GYOUSHA_ADDRESS] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_GYOUSHA_ADDRESS].Index);
                }

                // 処分の受領者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_JYURYOUSHA_NAME] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_JYURYOUSHA_NAME].Index);
                }

                // 処分の受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_JYUTAKUSHA_NAME] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_JYUTAKUSHA_NAME].Index);
                }


                // 区間1：運搬受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_NAME_1] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_NAME_1].Index);
                }

                // 区間1：運搬受託者住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_ADDRESS_1] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_ADDRESS_1].Index);
                }

                // 区間1：運搬先の事業場名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_NAME_1] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_NAME_1].Index);
                }

                // 区間1：運搬先の事業場住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_ADDRESS_1] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_ADDRESS_1].Index);
                }

                // 区間1：運搬の受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_1] != null)
                {
                    //lNameColUPN_JYUTAKUSHA_NAME.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_1].Index);
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_1].Index);
                }

                // 区間2：運搬受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_NAME_2] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_NAME_2].Index);
                }

                // 区間2：運搬受託者住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_ADDRESS_2] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_ADDRESS_2].Index);
                }

                // 区間2：運搬先の事業場名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_NAME_2] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_NAME_2].Index);
                }

                // 区間2：運搬先の事業場住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_ADDRESS_2] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].Index);
                }

                // 区間2：運搬の受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_2] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_2].Index);
                }

                // 区間3：運搬受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_NAME_3] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_NAME_3].Index);
                }

                // 区間3：運搬受託者住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_ADDRESS_3] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_ADDRESS_3].Index);
                }

                // 区間3：運搬先の事業場名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_NAME_3] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_NAME_3].Index);
                }

                // 区間3：運搬先の事業場住所
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_ADDRESS_3] != null)
                {
                    lAddCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].Index);
                }

                // 区間3：運搬の受託者名称
                if (this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_3] != null)
                {
                    lNameCol.Add(this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_JYUTAKUSHA_NAME_3].Index);
                }

                //拠点CD 必須チェック用
                bool hasNoKyoten = false;

                //ヘッダーチェック処理(色変え用)
                if (this.header.KYOTEN_CD.Text == String.Empty)
                {
                    this.header.KYOTEN_CD.IsInputErrorOccured = true;
                    this.header.KYOTEN_CD.UpdateBackColor();
                    hasNoKyoten = true;
                }
                else
                {
                    this.header.KYOTEN_CD.IsInputErrorOccured = false;
                    this.header.KYOTEN_CD.UpdateBackColor();
                }

                //マニフェスト種類CD 必須チェック用
                bool hasNoManiShurui = false;

                //1次2次区分 必須チェック用
                bool hasNoFirstSecondKbn = false;

                //交付番号 重複チェック用
                bool hasDupilicate = false;

                //名称 長さチェック用
                bool hasNameLength = false;

                //住所 長さチェック用
                bool hasAddLength = false;

                //一覧チェック処理(色変え用)
                if (this.form.NyuryokuIkkatsuItiran.RowCount == 1)
                {
                    rowCount = 1;
                }
                else
                {
                    rowCount = this.form.NyuryokuIkkatsuItiran.RowCount - 1;
                }
                int count = 0;
                if (this.form.TitleList.Contains(ConstCls.MANIFEST_SHURUI_CD))
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        //削除にチェックの無い項目のみ確認する
                        if (this.form.NyuryokuIkkatsuItiran.Rows[i].Cells["DEL_FLAG"].Value == null ||
                            this.form.NyuryokuIkkatsuItiran.Rows[i].Cells["DEL_FLAG"].Value.ToString() == "False")
                        {
                            //マニフェスト種類CD
                            var t = this.form.NyuryokuIkkatsuItiran[ConstCls.MANIFEST_SHURUI_CD, i] as r_framework.CustomControl.ICustomAutoChangeBackColor;
                            if (string.Empty.Equals(this.form.GetItiranCellValue(i, this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index)))
                            {
                                //赤くする
                                if (t != null)
                                {
                                    t.IsInputErrorOccured = true;
                                    t.UpdateBackColor();
                                }
                                //エラーメッセージ表示
                                hasNoManiShurui = true;
                            }
                            else
                            {
                                //白くする
                                if (t != null)
                                {
                                    t.IsInputErrorOccured = false;
                                    t.UpdateBackColor();
                                }

                            }

                            //1次2次区分
                            t = this.form.NyuryokuIkkatsuItiran[ConstCls.FIRST_SECOND_KBN, i] as r_framework.CustomControl.ICustomAutoChangeBackColor;
                            if (string.Empty.Equals(this.form.GetItiranCellValue(i, this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.FIRST_SECOND_KBN].Index)))
                            {
                                //赤くする
                                if (t != null)
                                {
                                    t.IsInputErrorOccured = true;
                                    t.UpdateBackColor();
                                }
                                //エラーメッセージ表示
                                hasNoFirstSecondKbn = true;
                            }
                            else
                            {
                                //白くする
                                if (t != null)
                                {
                                    t.IsInputErrorOccured = false;
                                    t.UpdateBackColor();
                                }

                            }

                            //交付番号重複チェック(画面)
                            currentManiShurui = this.form.GetItiranCellValue(i, colManiShuruiCD);
                            currentkoufuNo = this.form.GetItiranCellValue(i, colManiID);
                            t = this.form.NyuryokuIkkatsuItiran[colManiID, i] as r_framework.CustomControl.ICustomAutoChangeBackColor;
                            //白くする
                            if (t != null)
                            {
                                t.IsInputErrorOccured = false;
                            }

                            if (!string.Empty.Equals(currentManiShurui) && !string.Empty.Equals(currentkoufuNo))
                            {
                                for (int j = 0; j < this.form.NyuryokuIkkatsuItiran.RowCount; j++)
                                {
                                    if (j != i)
                                    {
                                        maniShurui = this.form.GetItiranCellValue(j, colManiShuruiCD);
                                        koufuNo = this.form.GetItiranCellValue(j, colManiID);


                                        if ((!string.Empty.Equals(maniShurui) && !string.Empty.Equals(koufuNo)) &&
                                            (maniShurui == currentManiShurui && koufuNo == currentkoufuNo))
                                        {
                                            //赤くする
                                            if (t != null)
                                            {
                                                t.IsInputErrorOccured = true;
                                            }
                                            //エラーメッセージ表示
                                            hasDupilicate = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            //交付番号 重複チェック(DB)
                            if (this.ExistKohuNo(this.form.GetItiranCellValue(i, this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index),
                                                       this.form.GetItiranCellValue(i, this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_ID].Index)) != string.Empty)
                            {
                                //赤くする
                                if (t != null)
                                {
                                    t.IsInputErrorOccured = true;
                                }
                                //エラーメッセージ表示
                                hasDupilicate = true;
                                break;
                            }

                            t.UpdateBackColor();//色反映


                            //名称 長さチェック(色変え込み)
                            if (mlogic.ChkGrdtxtLength(this.form.NyuryokuIkkatsuItiran, i, lNameCol, 40))
                            {
                                //エラーメッセージ表示
                                hasNameLength = true;
                            }

                            //住所 長さチェック(色変え用込み)
                            if (mlogic.ChkGrdtxtLength(this.form.NyuryokuIkkatsuItiran, i, lAddCol, 44))
                            {
                                //エラーメッセージ表示
                                hasAddLength = true;
                            }

                            count += 1;
                        }
                    }

                    //エラーメッセージここから　※一括チェックするため　まとめて最後に出す
                    //hasNoKyoten           拠点CD             必須チェック用エラーメッセージ
                    //hasNoManiShurui       マニフェスト種類CD 必須チェック用エラーメッセージ
                    //hasNoFirstSecondKbn   1次2次区分         必須チェック用エラーメッセージ
                    //hasDupilicate         交付番号           重複チェック用エラーメッセージ
                    //hasNameLength         名称               長さチェック用エラーメッセージ
                    //hasAddLength          住所               長さチェック用エラーメッセージ
                    var msgs = new[]{
                        new {hasErr=hasNoKyoten,msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.KYOTEN_CD)},
                        new {hasErr=hasNoManiShurui,msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.MANIFEST_SHURUI_CD)},
                        new {hasErr=hasNoFirstSecondKbn,msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.FIRST_SECOND_KBN)},

                        new {hasErr=hasDupilicate,msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E031"), ConstCls.MANIFEST_ID)},

                        new {hasErr=hasNameLength,msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E152"), "名称", "40")},
                        new {hasErr=hasAddLength,msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E152"),"住所", "44")}
                    };

                    //trueのものだけ抽出し、改行区切りで文字結合
                    string errMsg = string.Join(Environment.NewLine, msgs.Where(x => x.hasErr).Select(x => x.msg));

                    //対象があるかチェック
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(errMsg);
                        isErr = true;
                        LogUtility.DebugMethodEnd(isErr);
                        return isErr;
                    }
                    //エラーメッセージここまで

                    //「登録するデータがありません。」エラー表示 
                    if (count == 0)
                    {
                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E061");
                        isErr = true;
                        LogUtility.DebugMethodEnd(isErr);
                        return isErr;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.isRegistErr = false;
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                DialogResult result = messageShowLogic.MessageBoxShow("C043");
                if (result == DialogResult.No)
                {
                    return;
                }

                List<T_MANIFEST_ENTRY> entryList = new List<T_MANIFEST_ENTRY>();
                List<T_MANIFEST_UPN> upnList = new List<T_MANIFEST_UPN>();
                List<T_MANIFEST_DETAIL> detailList = new List<T_MANIFEST_DETAIL>();
                List<T_MANIFEST_RET_DATE> retList = new List<T_MANIFEST_RET_DATE>();


                //登録データ作成
                this.MakeRegistData(ref entryList, ref upnList, ref detailList, ref retList, true);

                using (Transaction tran = new Transaction())
                {
                    manifestoLogic.Insert(entryList, upnList, null, null, null, null, null, detailList, retList);
                    tran.Commit();
                }

                this.form.NyuryokuIkkatsuItiran.Rows.Clear();
                messageShowLogic.MessageBoxShow("I001", "登録");
            }
            catch (Exception ex)
            {
                this.form.isRegistErr = true;
                LogUtility.Error("Regist", ex);
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
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データ作成
        /// </summary>
        private void MakeRegistData(ref List<T_MANIFEST_ENTRY> entryList,
                             ref List<T_MANIFEST_UPN> upnList,
                             ref List<T_MANIFEST_DETAIL> detailList,
                             ref List<T_MANIFEST_RET_DATE> retList,
                             bool createFlg)
        {
            LogUtility.DebugMethodStart(entryList, upnList, detailList, retList, createFlg);

            try
            {
                long lSysId = 0;
                int iSeq = 1;
                T_MANIFEST_ENTRY maniEntry = null;
                T_MANIFEST_DETAIL maniDetail = null;
                T_MANIFEST_RET_DATE maniRet = null;

                for (int i = 0; i < this.form.NyuryokuIkkatsuItiran.RowCount - 1; i++)
                {
                    if (this.form.NyuryokuIkkatsuItiran.Rows[i].Cells["DEL_FLAG"].Value == null ||
                        this.form.NyuryokuIkkatsuItiran.Rows[i].Cells["DEL_FLAG"].Value.ToString() == "False")
                    {
                        DBAccessor dba = new DBAccessor();

                        lSysId = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);

                        // マニフェスト(T_MANIFEST_ENTRY)データ作成
                        maniEntry = new T_MANIFEST_ENTRY();
                        MakeManifestEntry(ref maniEntry, lSysId, iSeq, i);
                        entryList.Add(maniEntry);

                        // マニ収集運搬(T_MANIFEST_UPN)データ作成
                        MakeManifestUpn(ref upnList, lSysId, iSeq, i);

                        // マニ明細(T_MANIFEST_DETAIL)データ作成
                        maniDetail = new T_MANIFEST_DETAIL();
                        MakeManifestDetailList(ref maniDetail, lSysId, iSeq, i);
                        detailList.Add(maniDetail);

                        //マニフェスト返却部データ作成(初期値で作るのみ)
                        maniRet = new T_MANIFEST_RET_DATE();
                        MakeManifestRetList(ref maniRet, lSysId, iSeq, i);
                        retList.Add(maniRet);
                    }
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(entryList, upnList, detailList, retList);
            }
        }

        /// <summary>
        /// パターンデータ作成
        /// </summary>
        private void MakePatternData(ref List<T_MANIFEST_PT_ENTRY> entryList,
                             ref List<T_MANIFEST_PT_UPN> upnList,
                             ref List<T_MANIFEST_PT_DETAIL> detailList)
        {
            LogUtility.DebugMethodStart(entryList, upnList, detailList);

            T_MANIFEST_PT_ENTRY maniEntry = new T_MANIFEST_PT_ENTRY();
            T_MANIFEST_PT_DETAIL maniDetail = new T_MANIFEST_PT_DETAIL();

            int rowIdex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
            // マニパターン(T_MANIFEST_PT_ENTRY)データ作成
            maniEntry = new T_MANIFEST_PT_ENTRY();
            MakeManifestPtEntry(ref maniEntry, rowIdex);
            entryList.Add(maniEntry);

            // マニパターン収集運搬(T_MANIFEST_PT_UPN)データ作成
            MakeManifestPtUpn(ref upnList, rowIdex);

            // マニパターン明細(T_MANIFEST_PT_DETAIL)データ作成
            MakeManifestPtDetailList(ref maniDetail, rowIdex);
            detailList.Add(maniDetail);

            LogUtility.DebugMethodEnd(entryList, upnList, detailList);
        }
        /// <summary>
        /// マニフェスト(T_MANIFEST_ENTRY)データ作成
        /// </summary>
        private void MakeManifestEntry(ref T_MANIFEST_ENTRY tmp, long lSysId, int iSeq, int rowIndex)
        {
            LogUtility.DebugMethodStart(tmp, lSysId, iSeq, rowIndex);

            try
            {
                // システムID
                tmp.SYSTEM_ID = lSysId;
                // 枝番
                tmp.SEQ = iSeq;
                // 廃棄物区分CD
                tmp.HAIKI_KBN_CD = Convert.ToInt16(
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_SHURUI_CD].Value);

                if (IsHeader(rowIndex, ConstCls.FIRST_SECOND_KBN))
                {
                    // 一次の場合
                    if ("1".Equals(this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.FIRST_SECOND_KBN].Value.ToString()))
                    {
                        // 一次マニフェスト区分
                        tmp.FIRST_MANIFEST_KBN = false;
                    }
                    else
                    {
                        // 一次マニフェスト区分
                        tmp.FIRST_MANIFEST_KBN = true;
                    }
                }

                //拠点CD
                if (this.header.KYOTEN_CD.Text == String.Empty || this.header.KYOTEN_CD.Text == null)
                {
                    tmp.KYOTEN_CD = SqlInt16.Null;
                }
                else
                {
                    tmp.KYOTEN_CD = Convert.ToInt16(this.header.KYOTEN_CD.Text);
                }

                if (IsHeader(rowIndex, ConstCls.TORIHIKISAKI_CD))
                {
                    // 取引先CD
                    tmp.TORIHIKISAKI_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                    // 交付年月日
                    tmp.KOUFU_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                 , Convert.ToInt32(date.Substring(5, 2))
                                                 , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_KBN))
                {
                    // 交付番号区分
                    tmp.KOUFU_KBN = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_KBN].Value);
                }

                if (IsHeader(rowIndex, ConstCls.MANIFEST_ID))
                {
                    // 交付番号 
                    tmp.MANIFEST_ID =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SEIRI_ID))
                {
                    // 整理番号 
                    tmp.SEIRI_ID = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SEIRI_ID].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_TANTOUSHA))
                {
                    // 交付担当者 
                    tmp.KOUFU_TANTOUSHA =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_TANTOUSHA].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_TANTOUSHA_SHOZOKU))
                {
                    // 交付担当者所属 
                    tmp.KOUFU_TANTOUSHA_SHOZOKU =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_TANTOUSHA_SHOZOKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_CD))
                {
                    // 排出事業者CD 
                    tmp.HST_GYOUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_NAME))
                {
                    // 排出事業者名称
                    tmp.HST_GYOUSHA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_POST))
                {
                    // 排出事業者郵便番号 
                    tmp.HST_GYOUSHA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_POST].Value.ToString();
                }


                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_TEL))
                {
                    // 排出事業者電話番号 
                    tmp.HST_GYOUSHA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_TEL].Value.ToString();
                }


                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_ADDRESS))
                {
                    // 排出事業者住所  
                    tmp.HST_GYOUSHA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_CD))
                {
                    // 排出事業場CD 
                    tmp.HST_GENBA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_NAME))
                {
                    // 排出事業場名称 
                    tmp.HST_GENBA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_POST))
                {
                    // 排出事業場郵便番号 
                    tmp.HST_GENBA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_TEL))
                {
                    // 排出事業場電話番号  
                    tmp.HST_GENBA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_ADDRESS))
                {
                    // 排出事業場住所 
                    tmp.HST_GENBA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.BIKOU))
                {
                    // 備考 
                    tmp.BIKOU = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.BIKOU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HAIKI_SUU))
                {
                    // 実績数量 
                    tmp.HAIKI_SUU = Convert.ToDecimal(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value.ToString());
                }

                if (IsHeader(rowIndex, ConstCls.UNIT_CD_RYAKU))
                {
                    // 実績単位CD 
                    tmp.HAIKI_UNIT_CD = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNIT_CD_RYAKU].Value.ToString());
                }

                if (this.form.TitleList.Contains(ConstCls.LAST_SBN_YOTEI_KBN))
                {
                    // 最終処分の場所（予定）区分
                    tmp.LAST_SBN_YOTEI_KBN = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_KBN].Value);
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD))
                {
                    // 最終処分の場所（予定）業者CD 
                    tmp.LAST_SBN_YOTEI_GYOUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_CD))
                {
                    // 最終処分の場所（予定）現場CD 
                    tmp.LAST_SBN_YOTEI_GENBA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_NAME))
                {
                    // 最終処分の場所（予定）現場名称
                    tmp.LAST_SBN_YOTEI_GENBA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_POST))
                {
                    // 最終処分の場所（予定）郵便番号
                    tmp.LAST_SBN_YOTEI_GENBA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_TEL))
                {
                    // 最終処分の場所（予定）電話番号  
                    tmp.LAST_SBN_YOTEI_GENBA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS))
                {
                    // 最終処分の場所（予定）住所 
                    tmp.LAST_SBN_YOTEI_GENBA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_CD))
                {
                    // 処分受託者CD 
                    tmp.SBN_GYOUSHA_CD = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_NAME))
                {
                    // 処分受託者名称 
                    tmp.SBN_GYOUSHA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_POST))
                {
                    // 処分受託者郵便番号
                    tmp.SBN_GYOUSHA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_TEL))
                {
                    // 処分受託者電話番号
                    tmp.SBN_GYOUSHA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_ADDRESS))
                {
                    // 処分受託者住所 
                    tmp.SBN_GYOUSHA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOUSHA_CD))
                {

                    //特殊な処理 直行・建廃は受領者を、処分受託者へ移動させる
                    switch (tmp.HAIKI_KBN_CD.Value)
                    {
                        case 1: //直行
                        case 3: //積替え
                            // 処分の受領者CD
                            tmp.SBN_JYURYOUSHA_CD = null;

                            // 処分の受託者　に  処分の受領者CD をセットする（裏で 3種とも受領者にセットしているため。動的にセルを変更させるとなると、廃棄物区分の変更等考慮が増える）
                            tmp.SBN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value.ToString();

                                break;
                        case 2://建廃

                            // 処分の受領者CD
                            tmp.SBN_JYURYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value.ToString();

                            break;
                    }

                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOUSHA_NAME))
                {

                    //特殊な処理 直行・建廃は受領者を、処分受託者へ移動させる
                    switch (tmp.HAIKI_KBN_CD.Value)
                    {
                        case 1: //直行
                        case 3: //積替え
                            // 処分の受領者名称
                            tmp.SBN_JYURYOUSHA_NAME = null;

                            // 処分の受託者名称
                            // 処分の受託者　に  処分の受領者CD をセットする（裏で 3種とも受領者にセットしているため。動的にセルを変更させるとなると、廃棄物区分の変更等考慮が増える）
                            tmp.SBN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value.ToString();

                            break;
                        case 2://建廃

                            // 処分の受領者名称
                            tmp.SBN_JYURYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value.ToString();

                            break;
                    }

                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOU_TANTOU_CD))
                {
                    // 処分の受領担当者CD
                    tmp.SBN_JYURYOU_TANTOU_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOU_TANTOU_NAME))
                {
                    // 処分の受領担当者名
                    tmp.SBN_JYURYOU_TANTOU_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOU_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_DATE].Value.ToString();
                    // 処分受領日
                    tmp.SBN_JYURYOU_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                      , Convert.ToInt32(date.Substring(5, 2))
                                                      , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYUTAKUSHA_CD))
                {
                    // 処分の受託者CD  
                    tmp.SBN_JYUTAKUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYUTAKUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYUTAKUSHA_NAME))
                {
                    // 処分の受託者名称 
                    tmp.SBN_JYUTAKUSHA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYUTAKUSHA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_TANTOU_CD))
                {
                    // 処分担当者CD 
                    tmp.SBN_TANTOU_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_TANTOU_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_TANTOU_NAME))
                {
                    // 処分担当者名 
                    tmp.SBN_TANTOU_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_TANTOU_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B1))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B1].Value.ToString();
                    // 照合確認B1票
                    tmp.CHECK_B1 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                              , Convert.ToInt32(date.Substring(5, 2))
                                              , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B2))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B2].Value.ToString();
                    // 照合確認B2票
                    tmp.CHECK_B2 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                              , Convert.ToInt32(date.Substring(5, 2))
                                              , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B4))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B4].Value.ToString();
                    // 照合確認B4票
                    tmp.CHECK_B4 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                              , Convert.ToInt32(date.Substring(5, 2))
                                              , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B6))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B6].Value.ToString();
                    // 照合確認B6票
                    tmp.CHECK_B6 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                              , Convert.ToInt32(date.Substring(5, 2))
                                              , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_D))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_D].Value.ToString();
                    // 照合確認D票
                    tmp.CHECK_D = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_E))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_E].Value.ToString();
                    // 照合確認E票
                    tmp.CHECK_E = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                //積替保管　ここから
                //　　　　業者CD
                if (IsHeader(rowIndex, ConstCls.TMH_GYOUSHA_CD))
                {
                    tmp.TMH_GYOUSHA_CD = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GYOUSHA_CD, rowIndex].Value.ToString();
                }
                //　　　　業者名称
                if (IsHeader(rowIndex, ConstCls.TMH_GYOUSHA_NAME))
                {
                    tmp.TMH_GYOUSHA_NAME = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GYOUSHA_NAME, rowIndex].Value.ToString();
                }
                //　　　　現場CD
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_CD))
                {
                    tmp.TMH_GENBA_CD = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_CD, rowIndex].Value.ToString();
                }
                //　　　　現場名
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_NAME))
                {
                    tmp.TMH_GENBA_NAME = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_NAME, rowIndex].Value.ToString();
                }
                //　　　　現場TEL
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_TEL))
                {
                    tmp.TMH_GENBA_TEL = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_TEL, rowIndex].Value.ToString();
                }
                //　　　　現場POST
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_POST))
                {
                    tmp.TMH_GENBA_POST = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_POST, rowIndex].Value.ToString();
                }
                //　　　　現場住所
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_ADDRESS))
                {
                    tmp.TMH_GENBA_ADDRESS = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_ADDRESS, rowIndex].Value.ToString();
                }
                //積替保管 ここまで

                var dataBinderEntry = new DataBinderLogic<T_MANIFEST_ENTRY>(tmp);
                dataBinderEntry.SetSystemProperty(tmp, false);
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(tmp);
            }
        }

        /// <summary>
        /// マニ収集運搬(T_MANIFEST_UPN)データ作成
        /// </summary>
        private void MakeManifestUpn(ref List<T_MANIFEST_UPN> upnList, long lSysId, int iSeq, int rowIndex)
        {
            LogUtility.DebugMethodStart(upnList, lSysId, iSeq, rowIndex);

            try
            {
                int upnRouteNo = this.form.UpnRouteNo;
                string manifestShuruiCd =
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_SHURUI_CD].Value.ToString();

                T_MANIFEST_UPN tmp = null;

                for (int i = 1; i < upnRouteNo + 1; i++)
                {
                    if (i == 1)
                    {
                        tmp = new T_MANIFEST_UPN();

                        //システムID
                        tmp.SYSTEM_ID = lSysId;
                        //枝番
                        tmp.SEQ = iSeq;
                        //運搬区間
                        tmp.UPN_ROUTE_NO = 1;

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_CD_1))
                        {
                            //運搬受託者CD
                            tmp.UPN_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_NAME_1))
                        {
                            //運搬受託者名称
                            tmp.UPN_GYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_POST_1))
                        {
                            //運搬受託者郵便番号
                            tmp.UPN_GYOUSHA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_TEL_1))
                        {
                            //運搬受託者電話番号
                            tmp.UPN_GYOUSHA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_ADDRESS_1))
                        {
                            //運搬受託者住所
                            tmp.UPN_GYOUSHA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_HOUHOU_CD_1))
                        {
                            //運搬方法CD
                            tmp.UPN_HOUHOU_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHASHU_CD_1))
                        {
                            //車種CD
                            tmp.SHASHU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHARYOU_CD_1))
                        {
                            //車輌CD
                            tmp.SHARYOU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.TMH_KBN_1))
                        {
                            // 積替保管有無
                            tmp.TMH_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].Value.ToString());
                        }

                        //運搬先区分
                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_1))
                        {
                            if (this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value == null ||
                                string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString()))
                            {
                                //tmp.UPN_SAKI_KBN = 1; //デフォルトはNULL
                            }
                            else
                            {
                                //入力ある場合はその値
                                tmp.UPN_SAKI_KBN = Int16.Parse(this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString());
                            }
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_1))
                        {
                            //運搬先の事業者CD
                            tmp.UPN_SAKI_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_1))
                        {
                            //運搬先の事業場CD
                            tmp.UPN_SAKI_GENBA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_1))
                        {
                            //運搬先の事業場名称
                            tmp.UPN_SAKI_GENBA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_1))
                        {
                            //運搬先の事業場郵便番号
                            tmp.UPN_SAKI_GENBA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_1))
                        {
                            //運搬先の事業場電話番号
                            tmp.UPN_SAKI_GENBA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_1))
                        {
                            //運搬先の事業場住所
                            tmp.UPN_SAKI_GENBA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_CD_1))
                        {
                            //運搬の受託者CD
                            tmp.UPN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_1))
                        {
                            //運搬の受託者名称
                            tmp.UPN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_1))
                        {
                            //運転者CD
                            if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_1].Value == null)
                            {
                                tmp.UNTENSHA_CD = null;
                            }
                            else
                            {
                                tmp.UNTENSHA_CD =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_1].Value.ToString();
                            }
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_NAME_1))
                        {
                            //運転者名
                            tmp.UNTENSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_END_DATE_1))
                        {
                            string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_1].Value.ToString();
                            // 運搬終了年月日
                            tmp.UPN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                          , Convert.ToInt32(date.Substring(5, 2))
                                                          , Convert.ToInt32(date.Substring(8, 2)));
                        }
                        var dataBinderEntry = new DataBinderLogic<T_MANIFEST_UPN>(tmp);
                        dataBinderEntry.SetSystemProperty(tmp, false);

                        upnList.Add(tmp);
                    }

                    if (i == 2 && ("2".Equals(manifestShuruiCd) || "3".Equals(manifestShuruiCd)))
                    {
                        tmp = new T_MANIFEST_UPN();

                        //システムID
                        tmp.SYSTEM_ID = lSysId;
                        //枝番
                        tmp.SEQ = iSeq;
                        //運搬区間
                        tmp.UPN_ROUTE_NO = 2;

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_CD_2))
                        {
                            //運搬受託者CD
                            tmp.UPN_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_NAME_2))
                        {
                            //運搬受託者名称
                            tmp.UPN_GYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_POST_2))
                        {
                            //運搬受託者郵便番号
                            tmp.UPN_GYOUSHA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_TEL_2))
                        {
                            //運搬受託者電話番号
                            tmp.UPN_GYOUSHA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_ADDRESS_2))
                        {
                            //運搬受託者住所
                            tmp.UPN_GYOUSHA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_HOUHOU_CD_2))
                        {
                            //運搬方法CD
                            tmp.UPN_HOUHOU_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHASHU_CD_2))
                        {
                            //車種CD
                            tmp.SHASHU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHARYOU_CD_2))
                        {
                            //車輌CD
                            tmp.SHARYOU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.TMH_KBN_2))
                        {
                            // 積替保管有無
                            tmp.TMH_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].Value.ToString());
                        }

                        //建廃登録専用処理（パターンでは行わないので注意）
                        if ("2".Equals(manifestShuruiCd))
                        {
                            //区間1をコピーする

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_1))
                            {
                                // 運搬先区分
                                tmp.UPN_SAKI_KBN = Convert.ToInt16(
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_1].Value.ToString());
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_1))
                            {
                                //運搬先の事業者CD
                                tmp.UPN_SAKI_GYOUSHA_CD =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_1].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_1))
                            {
                                //運搬先の事業場CD
                                tmp.UPN_SAKI_GENBA_CD =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_1))
                            {
                                //運搬先の事業場名称
                                tmp.UPN_SAKI_GENBA_NAME =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_1].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_1))
                            {
                                //運搬先の事業場郵便番号
                                tmp.UPN_SAKI_GENBA_POST =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_1].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_1))
                            {
                                //運搬先の事業場電話番号
                                tmp.UPN_SAKI_GENBA_TEL =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_1].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_1))
                            {
                                //運搬先の事業場住所
                                tmp.UPN_SAKI_GENBA_ADDRESS =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_1].Value.ToString();
                            }
                        }
                        else //産廃は普通に
                        {

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_2))
                            {
                                // 運搬先区分
                                tmp.UPN_SAKI_KBN = Convert.ToInt16(
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].Value.ToString());
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_2))
                            {
                                //運搬先の事業者CD
                                tmp.UPN_SAKI_GYOUSHA_CD =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_2].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_2))
                            {
                                //運搬先の事業場CD
                                tmp.UPN_SAKI_GENBA_CD =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_2].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_2))
                            {
                                //運搬先の事業場名称
                                tmp.UPN_SAKI_GENBA_NAME =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_2].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_2))
                            {
                                //運搬先の事業場郵便番号
                                tmp.UPN_SAKI_GENBA_POST =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_2].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_2))
                            {
                                //運搬先の事業場電話番号
                                tmp.UPN_SAKI_GENBA_TEL =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_2].Value.ToString();
                            }

                            if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_2))
                            {
                                //運搬先の事業場住所
                                tmp.UPN_SAKI_GENBA_ADDRESS =
                                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].Value.ToString();
                            }
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_CD_2))
                        {
                            //運搬の受託者CD
                            tmp.UPN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_2))
                        {
                            //運搬の受託者名称
                            tmp.UPN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_CD_2))
                        {
                            //運転者CD
                            tmp.UNTENSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_NAME_2))
                        {
                            //運転者名
                            tmp.UNTENSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_END_DATE_2))
                        {
                            string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_2].Value.ToString();
                            // 運搬終了年月日
                            tmp.UPN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                          , Convert.ToInt32(date.Substring(5, 2))
                                                          , Convert.ToInt32(date.Substring(8, 2)));
                        }
                        var dataBinderEntry = new DataBinderLogic<T_MANIFEST_UPN>(tmp);
                        dataBinderEntry.SetSystemProperty(tmp, false);

                        upnList.Add(tmp);
                    }

                    if (i == 3 && "3".Equals(manifestShuruiCd))
                    {
                        tmp = new T_MANIFEST_UPN();

                        //システムID
                        tmp.SYSTEM_ID = lSysId;
                        //枝番
                        tmp.SEQ = iSeq;
                        //運搬区間
                        tmp.UPN_ROUTE_NO = 3;

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_CD_3))
                        {
                            //運搬受託者CD
                            tmp.UPN_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_NAME_3))
                        {
                            //運搬受託者名称
                            tmp.UPN_GYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_POST_3))
                        {
                            //運搬受託者郵便番号
                            tmp.UPN_GYOUSHA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_TEL_3))
                        {
                            //運搬受託者電話番号
                            tmp.UPN_GYOUSHA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_ADDRESS_3))
                        {
                            //運搬受託者住所
                            tmp.UPN_GYOUSHA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_HOUHOU_CD_3))
                        {
                            //運搬方法CD
                            tmp.UPN_HOUHOU_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHASHU_CD_3))
                        {
                            //車種CD
                            tmp.SHASHU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHARYOU_CD_3))
                        {
                            //車輌CD
                            tmp.SHARYOU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.TMH_KBN_3))
                        {
                            // 積替保管有無
                            tmp.TMH_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].Value.ToString());
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_3))
                        {
                            // 運搬先区分
                            tmp.UPN_SAKI_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].Value.ToString());
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_3))
                        {
                            //運搬先の事業者CD
                            tmp.UPN_SAKI_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_3))
                        {
                            //運搬先の事業場CD
                            tmp.UPN_SAKI_GENBA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_3))
                        {
                            //運搬先の事業場名称
                            tmp.UPN_SAKI_GENBA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_3))
                        {
                            //運搬先の事業場郵便番号
                            tmp.UPN_SAKI_GENBA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_3))
                        {
                            //運搬先の事業場電話番号
                            tmp.UPN_SAKI_GENBA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_3))
                        {
                            //運搬先の事業場住所
                            tmp.UPN_SAKI_GENBA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_CD_3))
                        {
                            //運搬の受託者CD
                            tmp.UPN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_3))
                        {
                            //運搬の受託者名称
                            tmp.UPN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_CD_3))
                        {
                            //運転者CD
                            tmp.UNTENSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_NAME_3))
                        {
                            //運転者名
                            tmp.UNTENSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_END_DATE_3))
                        {
                            string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_3].Value.ToString();
                            // 運搬終了年月日
                            tmp.UPN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                          , Convert.ToInt32(date.Substring(5, 2))
                                                          , Convert.ToInt32(date.Substring(8, 2)));
                        }
                        var dataBinderEntry = new DataBinderLogic<T_MANIFEST_UPN>(tmp);
                        dataBinderEntry.SetSystemProperty(tmp, false);

                        upnList.Add(tmp);
                    }
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(upnList);
            }
        }

        /// <summary>
        /// マニ明細(T_MANIFEST_DETAIL)リストデータ作成
        /// </summary>
        private void MakeManifestDetailList(ref T_MANIFEST_DETAIL tmp, long lSysId, int iSeq, int rowIndex)
        {
            LogUtility.DebugMethodStart(tmp, lSysId, iSeq, rowIndex);

            try
            {
                //システムID 
                tmp.SYSTEM_ID = lSysId;
                //枝番 
                tmp.SEQ = iSeq;

                //明細システムID 
                DBAccessor dba = new DBAccessor();

                tmp.DETAIL_SYSTEM_ID = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);

                if (IsHeader(rowIndex, ConstCls.HAIKI_SHURUI_CD_RYAKU))
                {
                    //廃棄物種類CD 
                    tmp.HAIKI_SHURUI_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SHURUI_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HAIKI_CD_RYAKU))
                {
                    //廃棄物名称CD 
                    tmp.HAIKI_NAME_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.NISUGATA_CD_RYAKU))
                {
                    //荷姿CD
                    tmp.NISUGATA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.NISUGATA_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HAIKI_SUU))
                {
                    //数量
                    tmp.HAIKI_SUU = Convert.ToDecimal(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value.ToString());
                }

                if (IsHeader(rowIndex, ConstCls.UNIT_CD_RYAKU))
                {
                    //単位CD 
                    tmp.HAIKI_UNIT_CD = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNIT_CD_RYAKU].Value.ToString());
                }

                //非表示でも登録は行う

                if (IsHeader(rowIndex, ConstCls.GENNYOU_SUU))
                {
                    if (this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex].Value != null && !string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex].Value.ToString()))
                    {
                        //減容数
                        tmp.GENNYOU_SUU = Convert.ToDecimal(
                            this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex].Value.ToString());
                    }
                }


                if (IsHeader(rowIndex, ConstCls.KANSAN_SUU))
                {
                    if (this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex].Value != null && !string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex].Value.ToString()))
                    {
                        //換算数 
                        tmp.KANSAN_SUU = Convert.ToDecimal(
                            this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex].Value.ToString());
                    }
                }


                if (IsHeader(rowIndex, ConstCls.SHOBUN_HOUHOU_CD_RYAKU))
                {
                    //処分方法CD
                    tmp.SBN_HOUHOU_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHOBUN_HOUHOU_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_END_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_END_DATE].Value.ToString();
                    // 処分終了日
                    tmp.SBN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                  , Convert.ToInt32(date.Substring(5, 2))
                                                  , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_END_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_END_DATE].Value.ToString();
                    // 最終処分終了日
                    tmp.LAST_SBN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                  , Convert.ToInt32(date.Substring(5, 2))
                                                  , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_GYOUSHA_CD))
                {
                    //最終処分業者CD
                    tmp.LAST_SBN_GYOUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_GENBA_CD))
                {
                    //最終処分現場CD
                    tmp.LAST_SBN_GENBA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_CD].Value.ToString();
                }

                var dataBinderEntry = new DataBinderLogic<T_MANIFEST_DETAIL>(tmp);
                dataBinderEntry.SetSystemProperty(tmp, false);
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(tmp);
            }
        }

        /// <summary>
        /// マニ返却日(T_MANIFEST_RET_DATE)リストデータ作成
        /// </summary>
        private void MakeManifestRetList(ref T_MANIFEST_RET_DATE tmp, long lSysId, int iSeq, int rowIndex)
        {
            LogUtility.DebugMethodStart(tmp, lSysId, iSeq, rowIndex);

            try
            {
                //システムID 
                tmp.SYSTEM_ID = lSysId;
                //枝番 
                tmp.SEQ = iSeq;

                //中身は空で良い

                var dataBinderEntry = new DataBinderLogic<T_MANIFEST_RET_DATE>(tmp);
                dataBinderEntry.SetSystemProperty(tmp, false);
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(tmp);
            }
        }

        /// <summary>
        /// マニパターン(T_MANIFEST_PT_ENTRY)データ作成
        /// </summary>
        private void MakeManifestPtEntry(ref T_MANIFEST_PT_ENTRY tmp, int rowIndex)
        {
            LogUtility.DebugMethodStart(tmp, rowIndex);

            try
            {
                // 廃棄物区分CD
                tmp.HAIKI_KBN_CD = Convert.ToInt16(
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_SHURUI_CD].Value);

                // 一括登録区分
                tmp.LIST_REGIST_KBN = true;

                // 一次マニフェスト区分(初期値は1次で作っておく。nullだと一覧で検索されないため。)
                tmp.FIRST_MANIFEST_KBN = false;

                if (IsHeader(rowIndex, ConstCls.FIRST_SECOND_KBN))
                {
                    // 一次の場合
                    if ("1".Equals(this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.FIRST_SECOND_KBN].Value.ToString()))
                    {
                        // 一次マニフェスト区分
                        tmp.FIRST_MANIFEST_KBN = false;
                    }
                    else
                    {
                        // 一次マニフェスト区分
                        tmp.FIRST_MANIFEST_KBN = true;
                    }
                }

                if (IsHeader(rowIndex, ConstCls.KYOTEN_CD))
                {
                    // 拠点CD
                    tmp.KYOTEN_CD = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KYOTEN_CD].Value);
                }

                if (IsHeader(rowIndex, ConstCls.TORIHIKISAKI_CD))
                {
                    // 取引先CD
                    tmp.TORIHIKISAKI_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                    // 交付年月日
                    tmp.KOUFU_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_KBN))
                {
                    // 交付番号区分
                    tmp.KOUFU_KBN = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_KBN].Value);
                }

                if (IsHeader(rowIndex, ConstCls.MANIFEST_ID))
                {
                    // 交付番号 
                    tmp.MANIFEST_ID =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SEIRI_ID))
                {
                    // 整理番号 
                    tmp.SEIRI_ID = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SEIRI_ID].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_TANTOUSHA))
                {
                    // 交付担当者 
                    tmp.KOUFU_TANTOUSHA =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_TANTOUSHA].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.KOUFU_TANTOUSHA_SHOZOKU))
                {
                    // 交付担当者所属 
                    tmp.KOUFU_TANTOUSHA_SHOZOKU =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_TANTOUSHA_SHOZOKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_CD))
                {
                    // 排出事業者CD 
                    tmp.HST_GYOUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_NAME))
                {
                    // 排出事業者名称
                    tmp.HST_GYOUSHA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_POST))
                {
                    // 排出事業者郵便番号 
                    tmp.HST_GYOUSHA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_TEL))
                {
                    // 排出事業者電話番号 
                    tmp.HST_GYOUSHA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GYOUSHA_ADDRESS))
                {
                    // 排出事業者住所  
                    tmp.HST_GYOUSHA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_CD))
                {
                    // 排出事業場CD 
                    tmp.HST_GENBA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_NAME))
                {
                    // 排出事業場名称 
                    tmp.HST_GENBA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_POST))
                {
                    // 排出事業場郵便番号 
                    tmp.HST_GENBA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_TEL))
                {
                    // 排出事業場電話番号  
                    tmp.HST_GENBA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HST_GENBA_ADDRESS))
                {
                    // 排出事業場住所 
                    tmp.HST_GENBA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.BIKOU))
                {
                    // 備考 
                    tmp.BIKOU = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.BIKOU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HAIKI_SUU))
                {
                    // 実績数量 
                    tmp.HAIKI_SUU = Convert.ToDecimal(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value.ToString());
                }

                if (IsHeader(rowIndex, ConstCls.UNIT_CD_RYAKU))
                {
                    // 実績単位CD 
                    tmp.HAIKI_UNIT_CD = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNIT_CD_RYAKU].Value.ToString());
                }

                if (this.form.TitleList.Contains(ConstCls.LAST_SBN_YOTEI_KBN))
                {
                    // 最終処分の場所（予定）区分
                    tmp.LAST_SBN_YOTEI_KBN = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_KBN].Value);
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD))
                {
                    // 最終処分の場所（予定）業者CD 
                    tmp.LAST_SBN_YOTEI_GYOUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_CD))
                {
                    // 最終処分の場所（予定）現場CD 
                    tmp.LAST_SBN_YOTEI_GENBA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_NAME))
                {
                    // 最終処分の場所（予定）現場名称
                    tmp.LAST_SBN_YOTEI_GENBA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_POST))
                {
                    // 最終処分の場所（予定）郵便番号
                    tmp.LAST_SBN_YOTEI_GENBA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_TEL))
                {
                    // 最終処分の場所（予定）電話番号  
                    tmp.LAST_SBN_YOTEI_GENBA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS))
                {
                    // 最終処分の場所（予定）住所 
                    tmp.LAST_SBN_YOTEI_GENBA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_CD))
                {
                    // 処分受託者CD 
                    tmp.SBN_GYOUSHA_CD = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_NAME))
                {
                    // 処分受託者名称 
                    tmp.SBN_GYOUSHA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_POST))
                {
                    // 処分受託者郵便番号
                    tmp.SBN_GYOUSHA_POST =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_POST].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_TEL))
                {
                    // 処分受託者電話番号
                    tmp.SBN_GYOUSHA_TEL =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_TEL].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_ADDRESS))
                {
                    // 処分受託者住所 
                    tmp.SBN_GYOUSHA_ADDRESS =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_ADDRESS].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOUSHA_CD))
                {

                    //特殊な処理 直行・建廃は受領者を、処分受託者へ移動させる
                    switch (tmp.HAIKI_KBN_CD.Value)
                    {
                        case 1: //直行
                        case 3: //積替え
                            // 処分の受領者CD
                            tmp.SBN_JYURYOUSHA_CD = null;

                            // 処分の受託者CD
                            // 処分の受託者　に  処分の受領者CD をセットする（裏で 3種とも受領者にセットしているため。動的にセルを変更させるとなると、廃棄物区分の変更等考慮が増える）
                            tmp.SBN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value.ToString();

                            break;
                        case 2://建廃

                            // 処分の受領者CD
                            tmp.SBN_JYURYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value.ToString();

                            break;
                    }

                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOUSHA_NAME))
                {


                    //特殊な処理 直行・建廃は受領者を、処分受託者へ移動させる
                    switch (tmp.HAIKI_KBN_CD.Value)
                    {
                        case 1: //直行
                        case 3: //積替え
                            // 処分の受領者名称
                            tmp.SBN_JYURYOUSHA_NAME = null;

                            // 処分の受託者名称
                            // 処分の受託者　に  処分の受領者CD をセットする（裏で 3種とも受領者にセットしているため。動的にセルを変更させるとなると、廃棄物区分の変更等考慮が増える）
                            tmp.SBN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value.ToString();

                            break;
                        case 2://建廃

                            // 処分の受領者名称
                            tmp.SBN_JYURYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value.ToString();

                            break;
                    }
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOU_TANTOU_CD))
                {
                    // 処分の受領担当者CD
                    tmp.SBN_JYURYOU_TANTOU_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOU_TANTOU_NAME))
                {
                    // 処分の受領担当者名
                    tmp.SBN_JYURYOU_TANTOU_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYURYOU_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_DATE].Value.ToString();
                    // 処分受領日
                    tmp.SBN_JYURYOU_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYUTAKUSHA_CD))
                {
                    // 処分の受託者CD  
                    tmp.SBN_JYUTAKUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYUTAKUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_JYUTAKUSHA_NAME))
                {
                    // 処分の受託者名称 
                    tmp.SBN_JYUTAKUSHA_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYUTAKUSHA_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_TANTOU_CD))
                {
                    // 処分担当者CD 
                    tmp.SBN_TANTOU_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_TANTOU_CD].Value.ToString();
                }


                if (IsHeader(rowIndex, ConstCls.SBN_TANTOU_NAME))
                {
                    // 処分担当者名 
                    tmp.SBN_TANTOU_NAME =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_TANTOU_NAME].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B1))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B1].Value.ToString();
                    // 照合確認B1票
                    tmp.CHECK_B1 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B2))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B2].Value.ToString();
                    // 照合確認B2票
                    tmp.CHECK_B2 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B4))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B4].Value.ToString();
                    // 照合確認B4票
                    tmp.CHECK_B4 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_B6))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B6].Value.ToString();
                    // 照合確認B6票
                    tmp.CHECK_B6 = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_D))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_D].Value.ToString();
                    // 照合確認D票
                    tmp.CHECK_D = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.CHECK_E))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_E].Value.ToString();
                    // 照合確認E票
                    tmp.CHECK_E = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                //積替保管　ここから
                //　　　　業者CD
                if (IsHeader(rowIndex, ConstCls.TMH_GYOUSHA_CD))
                {
                    tmp.TMH_GYOUSHA_CD = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GYOUSHA_CD, rowIndex].Value.ToString();
                }
                //　　　　業者名称
                if (IsHeader(rowIndex, ConstCls.TMH_GYOUSHA_NAME))
                {
                    tmp.TMH_GYOUSHA_NAME = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GYOUSHA_NAME, rowIndex].Value.ToString();
                }
                //　　　　現場CD
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_CD))
                {
                    tmp.TMH_GENBA_CD = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_CD, rowIndex].Value.ToString();
                }
                //　　　　現場名
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_NAME))
                {
                    tmp.TMH_GENBA_NAME = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_NAME, rowIndex].Value.ToString();
                }
                //　　　　現場TEL
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_TEL))
                {
                    tmp.TMH_GENBA_TEL = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_TEL, rowIndex].Value.ToString();
                }
                //　　　　現場POST
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_POST))
                {
                    tmp.TMH_GENBA_POST = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_POST, rowIndex].Value.ToString();
                }
                //　　　　現場住所
                if (IsHeader(rowIndex, ConstCls.TMH_GENBA_ADDRESS))
                {
                    tmp.TMH_GENBA_ADDRESS = this.form.NyuryokuIkkatsuItiran[ConstCls.TMH_GENBA_ADDRESS, rowIndex].Value.ToString();
                }
                //積替保管 ここまで


                var dataBinderEntry = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(tmp);
                dataBinderEntry.SetSystemProperty(tmp, false);
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(tmp);
            }
        }

        /// <summary>
        /// マニパターン登録収集運搬(T_MANIFEST_PT_UPN)データ作成
        /// </summary>
        private void MakeManifestPtUpn(ref List<T_MANIFEST_PT_UPN> upnList, int rowIndex)
        {
            LogUtility.DebugMethodStart(upnList, rowIndex);

            try
            {
                int upnRouteNo = this.form.UpnRouteNo;
                string manifestShuruiCd =
                    this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_SHURUI_CD].Value.ToString();

                T_MANIFEST_PT_UPN tmp = null;

                for (int i = 1; i < upnRouteNo + 1; i++)
                {
                    if (i == 1)
                    {
                        tmp = new T_MANIFEST_PT_UPN();

                        //運搬区間
                        tmp.UPN_ROUTE_NO = 1;

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_CD_1))
                        {
                            //運搬受託者CD
                            tmp.UPN_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_NAME_1))
                        {
                            //運搬受託者名称
                            tmp.UPN_GYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_POST_1))
                        {
                            //運搬受託者郵便番号
                            tmp.UPN_GYOUSHA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_TEL_1))
                        {
                            //運搬受託者電話番号
                            tmp.UPN_GYOUSHA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_ADDRESS_1))
                        {
                            //運搬受託者住所
                            tmp.UPN_GYOUSHA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_HOUHOU_CD_1))
                        {
                            //運搬方法CD
                            tmp.UPN_HOUHOU_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHASHU_CD_1))
                        {
                            //車種CD
                            tmp.SHASHU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHARYOU_CD_1))
                        {
                            //車輌CD
                            tmp.SHARYOU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.TMH_KBN_1))
                        {
                            // 積替保管有無
                            tmp.TMH_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].Value.ToString());
                        }

                        //運搬先区分
                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_1))
                        {
                            if (this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value == null ||
                                string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString()))
                            {
                                //tmp.UPN_SAKI_KBN = 1; //デフォルトはNULL
                            }
                            else
                            {
                                //入力ある場合はその値
                                tmp.UPN_SAKI_KBN = Int16.Parse(this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString());

                                if (int.Parse(this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString()) == ConstCls.UPN_SAKI_KBN_1_SBN)
                                {
                                    // №4655 実際には処分受託者CDを運搬先の事業者CDとして扱っているため、それにならう
                                    if (IsHeader(rowIndex, ConstCls.SBN_GYOUSHA_CD))
                                    {
                                        //運搬先の事業者CD（=処分受託者CD）
                                        tmp.UPN_SAKI_GYOUSHA_CD =
                                            this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_CD].Value.ToString();
                                    }
                                }
                                else if (int.Parse(this.form.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString()) == ConstCls.UPN_SAKI_KBN_1_UPN)
                                {
                                    if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_1))
                                    {
                                        //運搬先の事業者CD (=運搬先の事業者CD)
                                        tmp.UPN_SAKI_GYOUSHA_CD =
                                            this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_1].Value.ToString();
                                    }
                                }
                            }
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_1))
                        {
                            //運搬先の事業場CD
                            tmp.UPN_SAKI_GENBA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_1))
                        {
                            //運搬先の事業場名称
                            tmp.UPN_SAKI_GENBA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_1))
                        {
                            //運搬先の事業場郵便番号
                            tmp.UPN_SAKI_GENBA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_1))
                        {
                            //運搬先の事業場電話番号
                            tmp.UPN_SAKI_GENBA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_1))
                        {
                            //運搬先の事業場住所
                            tmp.UPN_SAKI_GENBA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_CD_1))
                        {
                            //運搬の受託者CD
                            tmp.UPN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_1))
                        {
                            //運搬の受託者名称
                            tmp.UPN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_CD_1))
                        {
                            //運転者CD
                            tmp.UNTENSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_NAME_1))
                        {
                            //運転者名
                            tmp.UNTENSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_1].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_END_DATE_1))
                        {
                            string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_1].Value.ToString();
                            // 運搬終了年月日
                            tmp.UPN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                     , Convert.ToInt32(date.Substring(5, 2))
                                                     , Convert.ToInt32(date.Substring(8, 2)));
                        }

                        var dataBinderEntry = new DataBinderLogic<T_MANIFEST_PT_UPN>(tmp);
                        dataBinderEntry.SetSystemProperty(tmp, false);

                        upnList.Add(tmp);
                    }

                    if (i == 2 && ("2".Equals(manifestShuruiCd) || "3".Equals(manifestShuruiCd)))
                    {
                        tmp = new T_MANIFEST_PT_UPN();

                        //運搬区間
                        tmp.UPN_ROUTE_NO = 2;

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_CD_2))
                        {
                            //運搬受託者CD
                            tmp.UPN_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_NAME_2))
                        {
                            //運搬受託者名称
                            tmp.UPN_GYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_POST_2))
                        {
                            //運搬受託者郵便番号
                            tmp.UPN_GYOUSHA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_TEL_2))
                        {
                            //運搬受託者電話番号
                            tmp.UPN_GYOUSHA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_ADDRESS_2))
                        {
                            //運搬受託者住所
                            tmp.UPN_GYOUSHA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_HOUHOU_CD_2))
                        {
                            //運搬方法CD
                            tmp.UPN_HOUHOU_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHASHU_CD_2))
                        {
                            //車種CD
                            tmp.SHASHU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHARYOU_CD_2))
                        {
                            //車輌CD
                            tmp.SHARYOU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.TMH_KBN_2))
                        {
                            // 積替保管有無
                            tmp.TMH_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].Value.ToString());
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_2))
                        {
                            // 運搬先区分
                            tmp.UPN_SAKI_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].Value.ToString());
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_2))
                        {
                            //運搬先の事業者CD
                            tmp.UPN_SAKI_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_2))
                        {
                            //運搬先の事業場CD
                            tmp.UPN_SAKI_GENBA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_2))
                        {
                            //運搬先の事業場名称
                            tmp.UPN_SAKI_GENBA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_2))
                        {
                            //運搬先の事業場郵便番号
                            tmp.UPN_SAKI_GENBA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_2))
                        {
                            //運搬先の事業場電話番号
                            tmp.UPN_SAKI_GENBA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_2))
                        {
                            //運搬先の事業場住所
                            tmp.UPN_SAKI_GENBA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_CD_2))
                        {
                            //運搬の受託者CD
                            tmp.UPN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_2))
                        {
                            //運搬の受託者名称
                            tmp.UPN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_CD_2))
                        {
                            //運転者CD
                            tmp.UNTENSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_NAME_2))
                        {
                            //運転者名
                            tmp.UNTENSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_2].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_END_DATE_2))
                        {
                            string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_2].Value.ToString();
                            // 運搬終了年月日
                            tmp.UPN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                     , Convert.ToInt32(date.Substring(5, 2))
                                                     , Convert.ToInt32(date.Substring(8, 2)));
                        }

                        var dataBinderEntry = new DataBinderLogic<T_MANIFEST_PT_UPN>(tmp);
                        dataBinderEntry.SetSystemProperty(tmp, false);

                        upnList.Add(tmp);
                    }

                    if (i == 3 && "3".Equals(manifestShuruiCd))
                    {
                        tmp = new T_MANIFEST_PT_UPN();

                        //運搬区間
                        tmp.UPN_ROUTE_NO = 3;

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_CD_3))
                        {
                            //運搬受託者CD
                            tmp.UPN_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_NAME_3))
                        {
                            //運搬受託者名称
                            tmp.UPN_GYOUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_POST_3))
                        {
                            //運搬受託者郵便番号
                            tmp.UPN_GYOUSHA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_TEL_3))
                        {
                            //運搬受託者電話番号
                            tmp.UPN_GYOUSHA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_GYOUSHA_ADDRESS_3))
                        {
                            //運搬受託者住所
                            tmp.UPN_GYOUSHA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_HOUHOU_CD_3))
                        {
                            //運搬方法CD
                            tmp.UPN_HOUHOU_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHASHU_CD_3))
                        {
                            //車種CD
                            tmp.SHASHU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.SHARYOU_CD_3))
                        {
                            //車輌CD
                            tmp.SHARYOU_CD
                                = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.TMH_KBN_3))
                        {
                            // 積替保管有無
                            tmp.TMH_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].Value.ToString());
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_KBN_3))
                        {
                            // 運搬先区分
                            tmp.UPN_SAKI_KBN = Convert.ToInt16(
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].Value.ToString());
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GYOUSHA_CD_3))
                        {
                            //運搬先の事業者CD
                            tmp.UPN_SAKI_GYOUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_CD_3))
                        {
                            //運搬先の事業場CD
                            tmp.UPN_SAKI_GENBA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_NAME_3))
                        {
                            //運搬先の事業場名称
                            tmp.UPN_SAKI_GENBA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_POST_3))
                        {
                            //運搬先の事業場郵便番号
                            tmp.UPN_SAKI_GENBA_POST =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_TEL_3))
                        {
                            //運搬先の事業場電話番号
                            tmp.UPN_SAKI_GENBA_TEL =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_SAKI_GENBA_ADDRESS_3))
                        {
                            //運搬先の事業場住所
                            tmp.UPN_SAKI_GENBA_ADDRESS =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_CD_3))
                        {
                            //運搬の受託者CD
                            tmp.UPN_JYUTAKUSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_JYUTAKUSHA_NAME_3))
                        {
                            //運搬の受託者名称
                            tmp.UPN_JYUTAKUSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_CD_3))
                        {
                            //運転者CD
                            tmp.UNTENSHA_CD =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UNTENSHA_NAME_3))
                        {
                            //運転者名
                            tmp.UNTENSHA_NAME =
                                this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_3].Value.ToString();
                        }

                        if (IsHeader(rowIndex, ConstCls.UPN_END_DATE_3))
                        {
                            string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_3].Value.ToString();
                            // 運搬終了年月日
                            tmp.UPN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                                     , Convert.ToInt32(date.Substring(5, 2))
                                                     , Convert.ToInt32(date.Substring(8, 2)));
                        }

                        var dataBinderEntry = new DataBinderLogic<T_MANIFEST_PT_UPN>(tmp);
                        dataBinderEntry.SetSystemProperty(tmp, false);

                        upnList.Add(tmp);
                    }
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(upnList);
            }
        }

        /// <summary>
        /// マニパターン明細(T_MANIFEST_PT_DETAIL)リストデータ作成
        /// </summary>
        private void MakeManifestPtDetailList(ref T_MANIFEST_PT_DETAIL tmp, int rowIndex)
        {
            LogUtility.DebugMethodStart(tmp, rowIndex);

            try
            {
                if (IsHeader(rowIndex, ConstCls.HAIKI_SHURUI_CD_RYAKU))
                {
                    //廃棄物種類CD 
                    tmp.HAIKI_SHURUI_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SHURUI_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HAIKI_CD_RYAKU))
                {
                    //廃棄物名称CD 
                    tmp.HAIKI_NAME_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.NISUGATA_CD_RYAKU))
                {
                    //荷姿CD
                    tmp.NISUGATA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.NISUGATA_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.HAIKI_SUU))
                {
                    //数量
                    tmp.HAIKI_SUU = Convert.ToDecimal(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value.ToString());
                }

                if (IsHeader(rowIndex, ConstCls.UNIT_CD_RYAKU))
                {
                    //単位CD 
                    tmp.HAIKI_UNIT_CD = Convert.ToInt16(
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNIT_CD_RYAKU].Value.ToString());
                }


                //非表示でも登録は行う

                if (IsHeader(rowIndex, ConstCls.GENNYOU_SUU))
                {
                    if (this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex].Value != null && !string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex].Value.ToString()))
                    {
                        //減容数
                        tmp.GENNYOU_SUU = Convert.ToDecimal(
                            this.form.NyuryokuIkkatsuItiran[ConstCls.GENNYOU_SUU, rowIndex].Value.ToString());
                    }
                }


                if (IsHeader(rowIndex, ConstCls.KANSAN_SUU))
                {
                    if (this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex].Value != null && !string.IsNullOrEmpty(this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex].Value.ToString()))
                    {
                        //換算数 
                        tmp.KANSAN_SUU = Convert.ToDecimal(
                            this.form.NyuryokuIkkatsuItiran[ConstCls.KANSAN_SUU, rowIndex].Value.ToString());
                    }
                }





                if (IsHeader(rowIndex, ConstCls.SHOBUN_HOUHOU_CD_RYAKU))
                {
                    //処分方法CD
                    tmp.SBN_HOUHOU_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHOBUN_HOUHOU_CD_RYAKU].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.SBN_END_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_END_DATE].Value.ToString();
                    // 処分終了日
                    tmp.SBN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_END_DATE))
                {
                    string date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_END_DATE].Value.ToString();
                    // 最終処分終了日
                    tmp.LAST_SBN_END_DATE = new DateTime(Convert.ToInt32(date.Substring(0, 4))
                                             , Convert.ToInt32(date.Substring(5, 2))
                                             , Convert.ToInt32(date.Substring(8, 2)));
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_GYOUSHA_CD))
                {
                    //最終処分業者CD
                    tmp.LAST_SBN_GYOUSHA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GYOUSHA_CD].Value.ToString();
                }

                if (IsHeader(rowIndex, ConstCls.LAST_SBN_GENBA_CD))
                {
                    //最終処分現場CD
                    tmp.LAST_SBN_GENBA_CD =
                        this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_CD].Value.ToString();
                }

                var dataBinderEntry = new DataBinderLogic<T_MANIFEST_PT_DETAIL>(tmp);
                dataBinderEntry.SetSystemProperty(tmp, false);
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd(tmp);
            }
        }

        /// <summary>
        /// タイトルチェック
        /// </summary>
        /// <returns></returns>
        private bool IsHeader(int rowIndex, string title)
        {
            LogUtility.DebugMethodStart(rowIndex, title);

            bool IsHeaderFlg = true;

            // タイトル以外項目
            if (!this.form.TitleList.Contains(title))
            {
                IsHeaderFlg = false;
            }
            else if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[title].Value == null
                || string.Empty.Equals(this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[title].Value.ToString()))
            {
                IsHeaderFlg = false;
            }

            LogUtility.DebugMethodEnd(IsHeaderFlg);

            return IsHeaderFlg;
        }

        /// <summary>
        /// コード項目リストの作成
        /// </summary>
        /// <returns></returns>
        private void CreateListCd(ArrayList listCd)
        {
            LogUtility.DebugMethodStart(listCd);
            // マニフェスト種類CD
            listCd.Add(ConstCls.MANIFEST_SHURUI_CD);
            // 一次二次区分
            listCd.Add(ConstCls.FIRST_SECOND_KBN);
            // 取引先CD
            listCd.Add(ConstCls.TORIHIKISAKI_CD);
            // 交付番号区分
            listCd.Add(ConstCls.KOUFU_KBN);
            // 交付番号
            listCd.Add(ConstCls.MANIFEST_ID);
            // 整理番号
            listCd.Add(ConstCls.SEIRI_ID);
            // 排出事業者CD
            listCd.Add(ConstCls.HST_GYOUSHA_CD);
            // 排出事業者郵便番号
            listCd.Add(ConstCls.HST_GYOUSHA_POST);
            // 排出事業者電話番号
            listCd.Add(ConstCls.HST_GYOUSHA_TEL);
            // 排出事業場CD
            listCd.Add(ConstCls.HST_GENBA_CD);
            // 排出事業場郵便番号
            listCd.Add(ConstCls.HST_GENBA_POST);
            // 排出事業場電話番号
            listCd.Add(ConstCls.HST_GENBA_TEL);
            // 最終処分の場所（予定）区分
            listCd.Add(ConstCls.LAST_SBN_YOTEI_KBN);
            // 最終処分の場所（予定）業者CD
            listCd.Add(ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD);
            // 最終処分の場所（予定）現場CD
            listCd.Add(ConstCls.LAST_SBN_YOTEI_GENBA_CD);
            // 最終処分の場所（予定）郵便番号
            listCd.Add(ConstCls.LAST_SBN_YOTEI_GENBA_POST);
            // 最終処分の場所（予定）電話番号
            listCd.Add(ConstCls.LAST_SBN_YOTEI_GENBA_TEL);
            // 処分受託者CD
            listCd.Add(ConstCls.SBN_GYOUSHA_CD);
            // 処分受託者郵便番号
            listCd.Add(ConstCls.SBN_GYOUSHA_POST);
            // 処分受託者電話番号
            listCd.Add(ConstCls.SBN_GYOUSHA_TEL);
            // 処分の受領者CD
            listCd.Add(ConstCls.SBN_JYURYOUSHA_CD);
            // 処分の受領担当者CD
            listCd.Add(ConstCls.SBN_JYURYOU_TANTOU_CD);
            // 処分の受託者CD
            listCd.Add(ConstCls.SBN_JYUTAKUSHA_CD);
            // 処分担当者CD
            listCd.Add(ConstCls.SBN_TANTOU_CD);
            // 区間1：運搬受託者CD
            listCd.Add(ConstCls.UPN_GYOUSHA_CD_1);
            // 区間1：運搬受託者郵便番号
            listCd.Add(ConstCls.UPN_GYOUSHA_POST_1);
            // 区間1：運搬受託者電話番号
            listCd.Add(ConstCls.UPN_GYOUSHA_TEL_1);
            // 区間1：運搬方法CD
            listCd.Add(ConstCls.UPN_HOUHOU_CD_1);
            // 区間1：車種CD
            listCd.Add(ConstCls.SHASHU_CD_1);
            // 区間1：車輌CD
            listCd.Add(ConstCls.SHARYOU_CD_1);
            // 区間1：積替保管有無
            listCd.Add(ConstCls.TMH_KBN_1);
            // 区間1：運搬先区分
            listCd.Add(ConstCls.UPN_SAKI_KBN_1);
            // 区間1：運搬先の事業者CD
            listCd.Add(ConstCls.UPN_SAKI_GYOUSHA_CD_1);
            // 区間1：運搬先の事業場CD
            listCd.Add(ConstCls.UPN_SAKI_GENBA_CD_1);
            // 区間1：運搬先の事業場郵便番号
            listCd.Add(ConstCls.UPN_SAKI_GENBA_POST_1);
            // 区間1：運搬先の事業場電話番号
            listCd.Add(ConstCls.UPN_SAKI_GENBA_TEL_1);
            // 区間1：運搬の受託者CD
            listCd.Add(ConstCls.UPN_JYUTAKUSHA_CD_1);
            // 区間1：運転者CD
            listCd.Add(ConstCls.UNTENSHA_CD_1);
            // 区間2：運搬受託者CD
            listCd.Add(ConstCls.UPN_GYOUSHA_CD_2);
            // 区間2：運搬受託者郵便番号
            listCd.Add(ConstCls.UPN_GYOUSHA_POST_2);
            // 区間2：運搬受託者電話番号
            listCd.Add(ConstCls.UPN_GYOUSHA_TEL_2);
            // 区間2：運搬方法CD
            listCd.Add(ConstCls.UPN_HOUHOU_CD_2);
            // 区間2：車種CD
            listCd.Add(ConstCls.SHASHU_CD_2);
            // 区間2：車輌CD
            listCd.Add(ConstCls.SHARYOU_CD_2);
            // 区間2：積替保管有無
            listCd.Add(ConstCls.TMH_KBN_2);
            // 区間2：運搬先区分
            listCd.Add(ConstCls.UPN_SAKI_KBN_2);
            // 区間2：運搬先の事業者CD
            listCd.Add(ConstCls.UPN_SAKI_GYOUSHA_CD_2);
            // 区間2：運搬先の事業場CD
            listCd.Add(ConstCls.UPN_SAKI_GENBA_CD_2);
            // 区間2：運搬先の事業場郵便番号
            listCd.Add(ConstCls.UPN_SAKI_GENBA_POST_2);
            // 区間2：運搬先の事業場電話番号
            listCd.Add(ConstCls.UPN_SAKI_GENBA_TEL_2);
            // 区間2：運搬の受託者CD
            listCd.Add(ConstCls.UPN_JYUTAKUSHA_CD_2);
            // 区間2：運転者CD
            listCd.Add(ConstCls.UNTENSHA_CD_2);
            // 区間3：運搬受託者CD
            listCd.Add(ConstCls.UPN_GYOUSHA_CD_3);
            // 区間3：運搬受託者郵便番号
            listCd.Add(ConstCls.UPN_GYOUSHA_POST_3);
            // 区間3：運搬受託者電話番号
            listCd.Add(ConstCls.UPN_GYOUSHA_TEL_3);
            // 区間3：運搬方法CD
            listCd.Add(ConstCls.UPN_HOUHOU_CD_3);
            // 区間3：車種CD
            listCd.Add(ConstCls.SHASHU_CD_3);
            // 区間3：車輌CD
            listCd.Add(ConstCls.SHARYOU_CD_3);
            // 区間3：積替保管有無
            listCd.Add(ConstCls.TMH_KBN_3);
            // 区間3：運搬先区分
            listCd.Add(ConstCls.UPN_SAKI_KBN_3);
            // 区間3：運搬先の事業者CD
            listCd.Add(ConstCls.UPN_SAKI_GYOUSHA_CD_3);
            // 区間3：運搬先の事業場CD
            listCd.Add(ConstCls.UPN_SAKI_GENBA_CD_3);
            // 区間3：運搬先の事業場郵便番号
            listCd.Add(ConstCls.UPN_SAKI_GENBA_POST_3);
            // 区間3：運搬先の事業場電話番号
            listCd.Add(ConstCls.UPN_SAKI_GENBA_TEL_3);
            // 区間3：運搬の受託者CD
            listCd.Add(ConstCls.UPN_JYUTAKUSHA_CD_3);
            // 区間3：運転者CD
            listCd.Add(ConstCls.UNTENSHA_CD_3);
            // 廃棄物種類CD
            listCd.Add(ConstCls.HAIKI_SHURUI_CD_RYAKU);
            // 廃棄物名称CD
            listCd.Add(ConstCls.HAIKI_CD_RYAKU);
            // 荷姿CD
            listCd.Add(ConstCls.NISUGATA_CD_RYAKU);
            // 単位CD
            listCd.Add(ConstCls.UNIT_CD_RYAKU);
            // 処分方法CD
            listCd.Add(ConstCls.SHOBUN_HOUHOU_CD_RYAKU);
            // 最終処分業者CD
            listCd.Add(ConstCls.LAST_SBN_GYOUSHA_CD);
            // 最終処分現場CD
            listCd.Add(ConstCls.LAST_SBN_GENBA_CD);
            
            // 積替保管業者CD
            listCd.Add(ConstCls.TMH_GYOUSHA_CD);
            // 積替保管場CD
            listCd.Add(ConstCls.TMH_GENBA_CD);
            
                


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 表示名称項目リストの作成
        /// </summary>
        /// <returns></returns>
        private void CreateListNm(ArrayList listNm)
        {
            LogUtility.DebugMethodStart(listNm);
            // マニフェスト種類名
            listNm.Add(ConstCls.MANIFEST_SHURUI_NAME);

            // 一次二次区分名
            listNm.Add(ConstCls.FIRST_SECOND_KBN_NAME);

            // 拠点名
            listNm.Add(ConstCls.KYOTEN_NAME);

            // 取引先名
            listNm.Add(ConstCls.TORIHIKISAKI_NAME);

            // 排出事業者名称
            listNm.Add(ConstCls.HST_GYOUSHA_NAME);

            // 排出事業場名称
            listNm.Add(ConstCls.HST_GENBA_NAME);

            // 最終処分の場所（予定）現場名称
            listNm.Add(ConstCls.LAST_SBN_YOTEI_GENBA_NAME);

            // 処分受託者名称
            listNm.Add(ConstCls.SBN_GYOUSHA_NAME);

            // 処分の受領者名称
            listNm.Add(ConstCls.SBN_JYURYOUSHA_NAME);

            // 処分の受領担当者名
            listNm.Add(ConstCls.SBN_JYURYOU_TANTOU_NAME);

            // 処分の受託者名称
            listNm.Add(ConstCls.SBN_JYUTAKUSHA_NAME);

            // 処分担当者名
            listNm.Add(ConstCls.SBN_TANTOU_NAME);
            // 区間1：運搬受託者名称
            listNm.Add(ConstCls.UPN_GYOUSHA_NAME_1);
            // 区間1：運搬方法名
            listNm.Add(ConstCls.UNPAN_HOUHOU_NAME_1);
            // 区間1：車種名
            listNm.Add(ConstCls.SHASHU_NAME_1);
            // 区間1：車輌名
            listNm.Add(ConstCls.SHARYOU_NAME_1);
            // 区間1：積替保管有無名称
            listNm.Add(ConstCls.TMH_KBN_NAME_1);
            // 区間1：運搬先区分名
            listNm.Add(ConstCls.UPN_SAKI_KBN_NAME_1);
            // 区間1：運搬先の事業場名称
            listNm.Add(ConstCls.UPN_SAKI_GENBA_NAME_1);
            // 区間1：運搬の受託者名称
            listNm.Add(ConstCls.UPN_JYUTAKUSHA_NAME_1);
            // 区間1：運転者名
            listNm.Add(ConstCls.UNTENSHA_NAME_1);

            // 区間2：運搬受託者名称
            listNm.Add(ConstCls.UPN_GYOUSHA_NAME_2);
            // 区間2：運搬方法名
            listNm.Add(ConstCls.UNPAN_HOUHOU_NAME_2);
            // 区間2：車種名
            listNm.Add(ConstCls.SHASHU_NAME_2);
            // 区間2：車輌名
            listNm.Add(ConstCls.SHARYOU_NAME_2);
            // 区間2：積替保管有無名称
            listNm.Add(ConstCls.TMH_KBN_NAME_2);
            // 区間2：運搬先区分名
            listNm.Add(ConstCls.UPN_SAKI_KBN_NAME_2);
            // 区間2：運搬先の事業場名称
            listNm.Add(ConstCls.UPN_SAKI_GENBA_NAME_2);
            // 区間2：運搬の受託者名称
            listNm.Add(ConstCls.UPN_JYUTAKUSHA_NAME_2);
            // 区間2：運転者名
            listNm.Add(ConstCls.UNTENSHA_NAME_2);

            // 区間3：運搬受託者名称
            listNm.Add(ConstCls.UPN_GYOUSHA_NAME_3);
            // 区間3：運搬方法名
            listNm.Add(ConstCls.UNPAN_HOUHOU_NAME_3);
            // 区間3：車種名
            listNm.Add(ConstCls.SHASHU_NAME_3);
            // 区間3：車輌名
            listNm.Add(ConstCls.SHARYOU_NAME_3);
            // 区間3：積替保管有無名称
            listNm.Add(ConstCls.TMH_KBN_NAME_3);
            // 区間3：運搬先区分名
            listNm.Add(ConstCls.UPN_SAKI_KBN_NAME_3);
            // 区間3：運搬先の事業場名称
            listNm.Add(ConstCls.UPN_SAKI_GENBA_NAME_3);
            // 区間3：運搬の受託者名称
            listNm.Add(ConstCls.UPN_JYUTAKUSHA_NAME_3);
            // 区間3：運転者名
            listNm.Add(ConstCls.UNTENSHA_NAME_3);

            // 廃棄物種類名
            listNm.Add(ConstCls.HAIKI_SHURUI_NAME_RYAKU);

            // 廃棄物名称
            listNm.Add(ConstCls.HAIKI_NAME_RYAKU);

            // 荷姿名
            listNm.Add(ConstCls.NISUGATA_NAME_RYAKU);

            // 単位名
            listNm.Add(ConstCls.UNIT_NAME_RYAKU);
            
            // 減容後数量（表示のみ）
            listNm.Add(ConstCls.GENNYOU_SUU);

            // 処分方法名
            listNm.Add(ConstCls.SHOBUN_HOUHOU_NAME_RYAKU);

            // 最終処分業者略称
            listNm.Add(ConstCls.LAST_SBN_GYOUSHA_NAME);
            // 最終処分現場略称
            listNm.Add(ConstCls.LAST_SBN_GENBA_NAME);

            // 積替保管業者名称
            listNm.Add(ConstCls.TMH_GYOUSHA_NAME);
            // 積替保管場名称
            listNm.Add(ConstCls.TMH_GENBA_NAME);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 数値項目リストの作成
        /// </summary>
        /// <returns></returns>
        private void CreateListNum(ArrayList listNum)
        {
            LogUtility.DebugMethodStart(listNum);
            // 拠点CD
            listNum.Add(ConstCls.KYOTEN_CD);

            // 数量
            listNum.Add(ConstCls.HAIKI_SUU);

            // 換算後数量
            listNum.Add(ConstCls.KANSAN_SUU);
            
            LogUtility.DebugMethodEnd(listNum);
        }

        /// <summary>
        /// 入力名称項目リストの作成
        /// </summary>
        /// <returns></returns>
        private void CreateListText(ArrayList listText)
        {
            LogUtility.DebugMethodStart(listText);

            // 交付担当者
            listText.Add(ConstCls.KOUFU_TANTOUSHA);
            // 交付担当者所属
            listText.Add(ConstCls.KOUFU_TANTOUSHA_SHOZOKU);
            // 排出事業者住所
            listText.Add(ConstCls.HST_GYOUSHA_ADDRESS);

            // 排出事業場住所
            listText.Add(ConstCls.HST_GENBA_ADDRESS);

            // 備考
            listText.Add(ConstCls.BIKOU);

            // 最終処分の場所（予定）住所
            listText.Add(ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS);

            // 処分受託者住所
            listText.Add(ConstCls.SBN_GYOUSHA_ADDRESS);

            // 区間1：運搬受託者住所
            listText.Add(ConstCls.UPN_GYOUSHA_ADDRESS_1);

            // 区間1：運搬先の事業場住所
            listText.Add(ConstCls.UPN_SAKI_GENBA_ADDRESS_1);

            // 区間2：運搬受託者住所
            listText.Add(ConstCls.UPN_GYOUSHA_ADDRESS_2);

            // 区間2：運搬先の事業場住所
            listText.Add(ConstCls.UPN_SAKI_GENBA_ADDRESS_2);

            // 区間3：運搬受託者住所
            listText.Add(ConstCls.UPN_GYOUSHA_ADDRESS_3);

            // 区間3：運搬先の事業場住所
            listText.Add(ConstCls.UPN_SAKI_GENBA_ADDRESS_3);

            LogUtility.DebugMethodEnd(listText);
        }

        /// <summary>
        /// 日付項目リストの作成
        /// </summary>
        /// <returns></returns>
        private void CreateListDate(ArrayList listDate)
        {
            LogUtility.DebugMethodStart(listDate);

            // 交付年月日
            listDate.Add(ConstCls.KOUFU_DATE);

            // 処分受領日
            listDate.Add(ConstCls.SBN_JYURYOU_DATE);

            // 照合確認B1票
            listDate.Add(ConstCls.CHECK_B1);

            // 照合確認B2票
            listDate.Add(ConstCls.CHECK_B2);

            // 照合確認B4票
            listDate.Add(ConstCls.CHECK_B4);

            // 照合確認B6票
            listDate.Add(ConstCls.CHECK_B6);

            // 照合確認D票
            listDate.Add(ConstCls.CHECK_D);

            // 照合確認E票
            listDate.Add(ConstCls.CHECK_E);

            // 区間1：運搬終了年月日
            listDate.Add(ConstCls.UPN_END_DATE_1);

            // 区間2：運搬終了年月日
            listDate.Add(ConstCls.UPN_END_DATE_2);

            // 区間3：運搬終了年月日
            listDate.Add(ConstCls.UPN_END_DATE_3);

            // 処分終了日
            listDate.Add(ConstCls.SBN_END_DATE);

            // 最終処分終了日
            listDate.Add(ConstCls.LAST_SBN_END_DATE);

            LogUtility.DebugMethodEnd(listDate);
        }

        /// <summary>
        /// HashTableの作成
        /// </summary>
        /// <returns></returns>
        private void CreateHashtable()
        {
            LogUtility.DebugMethodStart();

            // マニフェスト種類CD
            titleTable.Add("HAIKI_KBN_CD", ConstCls.MANIFEST_SHURUI_CD);

            // マニフェスト種類名
            titleTable.Add("HAIKI_KBN_NAME", ConstCls.MANIFEST_SHURUI_NAME);

            // 一次二次区分
            titleTable.Add("FIRST_MANIFEST_KBN", ConstCls.FIRST_SECOND_KBN);

            // 一次二次区分名
            titleTable.Add("FIRST_MANIFEST_KBN_NAME", ConstCls.FIRST_SECOND_KBN_NAME);

            // 拠点CD
            titleTable.Add("KYOTEN_CD", ConstCls.KYOTEN_CD);

            // 拠点名
            titleTable.Add("KYOTEN_NAME", ConstCls.KYOTEN_NAME);

            // 取引先CD
            titleTable.Add("TORIHIKISAKI_CD", ConstCls.TORIHIKISAKI_CD);

            // 取引先名
            titleTable.Add("TORIHIKISAKI_NAME", ConstCls.TORIHIKISAKI_NAME);

            // 交付年月日
            titleTable.Add("KOUFU_DATE", ConstCls.KOUFU_DATE);

            // 交付番号区分
            titleTable.Add("KOUFU_KBN", ConstCls.KOUFU_KBN);

            // 交付番号
            titleTable.Add("MANIFEST_ID", ConstCls.MANIFEST_ID);

            // 整理番号
            titleTable.Add("SEIRI_ID", ConstCls.SEIRI_ID);

            // 交付担当者
            titleTable.Add("KOUFU_TANTOUSHA", ConstCls.KOUFU_TANTOUSHA);
            // 交付担当者所属
            titleTable.Add("KOUFU_TANTOUSHA_SHOZOKU", ConstCls.KOUFU_TANTOUSHA_SHOZOKU);
            // 排出事業者CD
            titleTable.Add("HST_GYOUSHA_CD", ConstCls.HST_GYOUSHA_CD);

            // 排出事業者名称
            titleTable.Add("HST_GYOUSHA_NAME", ConstCls.HST_GYOUSHA_NAME);

            // 排出事業者郵便番号
            titleTable.Add("HST_GYOUSHA_POST", ConstCls.HST_GYOUSHA_POST);

            // 排出事業者電話番号
            titleTable.Add("HST_GYOUSHA_TEL", ConstCls.HST_GYOUSHA_TEL);

            // 排出事業者住所
            titleTable.Add("HST_GYOUSHA_ADDRESS", ConstCls.HST_GYOUSHA_ADDRESS);

            // 排出事業場CD
            titleTable.Add("HST_GENBA_CD", ConstCls.HST_GENBA_CD);

            // 排出事業場名称
            titleTable.Add("HST_GENBA_NAME", ConstCls.HST_GENBA_NAME);

            // 排出事業場郵便番号
            titleTable.Add("HST_GENBA_POST", ConstCls.HST_GENBA_POST);

            // 排出事業場電話番号
            titleTable.Add("HST_GENBA_TEL", ConstCls.HST_GENBA_TEL);

            // 排出事業場住所
            titleTable.Add("HST_GENBA_ADDRESS", ConstCls.HST_GENBA_ADDRESS);

            // 備考
            titleTable.Add("BIKOU", ConstCls.BIKOU);

            // 最終処分の場所（予定）区分
            titleTable.Add("LAST_SBN_YOTEI_KBN", ConstCls.LAST_SBN_YOTEI_KBN);

            // 最終処分の場所（予定）業者CD
            titleTable.Add("LAST_SBN_YOTEI_GYOUSHA_CD", ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD);

            // 最終処分の場所（予定）現場CD
            titleTable.Add("LAST_SBN_YOTEI_GENBA_CD", ConstCls.LAST_SBN_YOTEI_GENBA_CD);

            // 最終処分の場所（予定）現場名称
            titleTable.Add("LAST_SBN_YOTEI_GENBA_NAME", ConstCls.LAST_SBN_YOTEI_GENBA_NAME);

            // 最終処分の場所（予定）郵便番号
            titleTable.Add("LAST_SBN_YOTEI_GENBA_POST", ConstCls.LAST_SBN_YOTEI_GENBA_POST);

            // 最終処分の場所（予定）電話番号
            titleTable.Add("LAST_SBN_YOTEI_GENBA_TEL", ConstCls.LAST_SBN_YOTEI_GENBA_TEL);

            // 最終処分の場所（予定）住所
            titleTable.Add("LAST_SBN_YOTEI_GENBA_ADDRESS", ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS);

            // 処分受託者CD
            titleTable.Add("SBN_GYOUSHA_CD", ConstCls.SBN_GYOUSHA_CD);

            // 処分受託者名称
            titleTable.Add("SBN_GYOUSHA_NAME", ConstCls.SBN_GYOUSHA_NAME);

            // 処分受託者郵便番号
            titleTable.Add("SBN_GYOUSHA_POST", ConstCls.SBN_GYOUSHA_POST);

            // 処分受託者電話番号
            titleTable.Add("SBN_GYOUSHA_TEL", ConstCls.SBN_GYOUSHA_TEL);

            // 処分受託者住所
            titleTable.Add("SBN_GYOUSHA_ADDRESS", ConstCls.SBN_GYOUSHA_ADDRESS);

            // 処分の受領者CD
            titleTable.Add("SBN_JYURYOUSHA_CD", ConstCls.SBN_JYURYOUSHA_CD);

            // 処分の受領者名称
            titleTable.Add("SBN_JYURYOUSHA_NAME", ConstCls.SBN_JYURYOUSHA_NAME);

            // 処分の受領担当者CD
            titleTable.Add("SBN_JYURYOU_TANTOU_CD", ConstCls.SBN_JYURYOU_TANTOU_CD);

            // 処分の受領担当者名
            titleTable.Add("SBN_JYURYOU_TANTOU_NAME", ConstCls.SBN_JYURYOU_TANTOU_NAME);
            
            // 処分受領日
            titleTable.Add("SBN_JYURYOU_DATE", ConstCls.SBN_JYURYOU_DATE);
            
            // 処分の受託者CD
            titleTable.Add("SBN_JYUTAKUSHA_CD", ConstCls.SBN_JYUTAKUSHA_CD);
            
            // 処分の受託者名称
            titleTable.Add("SBN_JYUTAKUSHA_NAME", ConstCls.SBN_JYUTAKUSHA_NAME);
            
            // 処分担当者CD
            titleTable.Add("SBN_TANTOU_CD", ConstCls.SBN_TANTOU_CD);

            // 処分担当者名
            titleTable.Add("SBN_TANTOU_NAME", ConstCls.SBN_TANTOU_NAME);
            
            // 照合確認B1票
            titleTable.Add("CHECK_B1", ConstCls.CHECK_B1);
            
            // 照合確認B2票
            titleTable.Add("CHECK_B2", ConstCls.CHECK_B2);
            
            // 照合確認B4票
            titleTable.Add("CHECK_B4", ConstCls.CHECK_B4);
            
            // 照合確認B6票
            titleTable.Add("CHECK_B6", ConstCls.CHECK_B6);
            
            // 照合確認D票
            titleTable.Add("CHECK_D", ConstCls.CHECK_D);

            // 照合確認E票
            titleTable.Add("CHECK_E", ConstCls.CHECK_E);
            
            // 廃棄物種類CD
            titleTable.Add("HAIKI_SHURUI_CD", ConstCls.HAIKI_SHURUI_CD_RYAKU);

            // 廃棄物種類名
            titleTable.Add("HAIKI_SHURUI_NAME", ConstCls.HAIKI_SHURUI_NAME_RYAKU);

            // 廃棄物名称CD
            titleTable.Add("HAIKI_NAME_CD", ConstCls.HAIKI_CD_RYAKU);

            // 廃棄物名称
            titleTable.Add("HAIKI_NAME", ConstCls.HAIKI_NAME_RYAKU);

            // 荷姿CD
            titleTable.Add("NISUGATA_CD", ConstCls.NISUGATA_CD_RYAKU);

            // 荷姿名
            titleTable.Add("NISUGATA_NAME", ConstCls.NISUGATA_NAME_RYAKU);

            // 数量
            titleTable.Add("HAIKI_SUU", ConstCls.HAIKI_SUU);

            // 単位CD
            titleTable.Add("HAIKI_UNIT_CD", ConstCls.UNIT_CD_RYAKU);

            // 単位名
            titleTable.Add("UNIT_NAME", ConstCls.UNIT_NAME_RYAKU);

            //換算後数量
            titleTable.Add("KANSAN_SUU", ConstCls.KANSAN_SUU);

            // 処分方法CD
            titleTable.Add("SBN_HOUHOU_CD", ConstCls.SHOBUN_HOUHOU_CD_RYAKU);

            // 処分方法名
            titleTable.Add("SHOBUN_HOUHOU_NAME", ConstCls.SHOBUN_HOUHOU_NAME_RYAKU);

            // 処分終了日
            titleTable.Add("SBN_END_DATE", ConstCls.SBN_END_DATE);

            // 最終処分終了日
            titleTable.Add("LAST_SBN_END_DATE", ConstCls.LAST_SBN_END_DATE);

            // 最終処分業者CD
            titleTable.Add("LAST_SBN_GYOUSHA_CD", ConstCls.LAST_SBN_GYOUSHA_CD);
            titleTable.Add("GYOUSHA_NAME_RYAKU", ConstCls.LAST_SBN_GYOUSHA_NAME);

            // 最終処分現場CD
            titleTable.Add("LAST_SBN_GENBA_CD", ConstCls.LAST_SBN_GENBA_CD);
            titleTable.Add("GENBA_NAME_RYAKU", ConstCls.LAST_SBN_GENBA_NAME);

            //積替保管
            titleTable.Add("TMH_GYOUSHA_CD", ConstCls.TMH_GYOUSHA_CD);
            titleTable.Add("TMH_GYOUSHA_NAME", ConstCls.TMH_GYOUSHA_NAME);
            titleTable.Add("TMH_GENBA_CD", ConstCls.TMH_GENBA_CD);
            titleTable.Add("TMH_GENBA_NAME", ConstCls.TMH_GENBA_NAME);
            titleTable.Add("TMH_GENBA_POST", ConstCls.TMH_GENBA_POST);
            titleTable.Add("TMH_GENBA_TEL", ConstCls.TMH_GENBA_TEL);
            titleTable.Add("TMH_GENBA_ADDRESS", ConstCls.TMH_GENBA_ADDRESS);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HashTableの作成
        /// </summary>
        /// <returns></returns>
        private void CreateUpn1Table()
        {
            LogUtility.DebugMethodStart();
            // 運搬区間
            titleUpn1Table.Add("UPN_ROUTE_NO", ConstCls.UPN_ROUTE_NO_1);
            // 運搬受託者CD
            titleUpn1Table.Add("UPN_GYOUSHA_CD", ConstCls.UPN_GYOUSHA_CD_1);
            // 運搬受託者名称
            titleUpn1Table.Add("UPN_GYOUSHA_NAME", ConstCls.UPN_GYOUSHA_NAME_1);
            // 運搬受託者郵便番号
            titleUpn1Table.Add("UPN_GYOUSHA_POST", ConstCls.UPN_GYOUSHA_POST_1);
            // 運搬受託者電話番号
            titleUpn1Table.Add("UPN_GYOUSHA_TEL", ConstCls.UPN_GYOUSHA_TEL_1);
            // 運搬受託者住所
            titleUpn1Table.Add("UPN_GYOUSHA_ADDRESS", ConstCls.UPN_GYOUSHA_ADDRESS_1);
            // 運搬方法CD
            titleUpn1Table.Add("UPN_HOUHOU_CD", ConstCls.UPN_HOUHOU_CD_1);
            // 運搬方法名
            titleUpn1Table.Add("UNPAN_HOUHOU_NAME", ConstCls.UNPAN_HOUHOU_NAME_1);
            // 車種CD
            titleUpn1Table.Add("SHASHU_CD", ConstCls.SHASHU_CD_1);
            // 車種名
            titleUpn1Table.Add("SHASHU_NAME", ConstCls.SHASHU_NAME_1);
            // 車輌CD
            titleUpn1Table.Add("SHARYOU_CD", ConstCls.SHARYOU_CD_1);
            // 車輌名
            titleUpn1Table.Add("SHARYOU_NAME", ConstCls.SHARYOU_NAME_1);
            // 積替保管有無
            titleUpn1Table.Add("TMH_KBN", ConstCls.TMH_KBN_1);
            // 積替保管有無名称
            titleUpn1Table.Add("TMH_KBN_NAME", ConstCls.TMH_KBN_NAME_1);
            // 運搬先区分
            titleUpn1Table.Add("UPN_SAKI_KBN", ConstCls.UPN_SAKI_KBN_1);
            // 運搬先区分名
            titleUpn1Table.Add("UPN_SAKI_KBN_NAME", ConstCls.UPN_SAKI_KBN_NAME_1);
            // 運搬先の事業者CD
            titleUpn1Table.Add("UPN_SAKI_GYOUSHA_CD", ConstCls.UPN_SAKI_GYOUSHA_CD_1);
            // 運搬先の事業場CD
            titleUpn1Table.Add("UPN_SAKI_GENBA_CD", ConstCls.UPN_SAKI_GENBA_CD_1);
            // 運搬先の事業場名称
            titleUpn1Table.Add("UPN_SAKI_GENBA_NAME", ConstCls.UPN_SAKI_GENBA_NAME_1);
            // 運搬先の事業場郵便番号
            titleUpn1Table.Add("UPN_SAKI_GENBA_POST", ConstCls.UPN_SAKI_GENBA_POST_1);
            // 運搬先の事業場電話番号
            titleUpn1Table.Add("UPN_SAKI_GENBA_TEL", ConstCls.UPN_SAKI_GENBA_TEL_1);
            // 運搬先の事業場住所
            titleUpn1Table.Add("UPN_SAKI_GENBA_ADDRESS", ConstCls.UPN_SAKI_GENBA_ADDRESS_1);
            // 運搬の受託者CD
            titleUpn1Table.Add("UPN_JYUTAKUSHA_CD", ConstCls.UPN_JYUTAKUSHA_CD_1);
            // 運搬の受託者名称
            titleUpn1Table.Add("UPN_JYUTAKUSHA_NAME", ConstCls.UPN_JYUTAKUSHA_NAME_1);
            // 運転者CD
            titleUpn1Table.Add("UNTENSHA_CD", ConstCls.UNTENSHA_CD_1);
            // 運転者名
            titleUpn1Table.Add("UNTENSHA_NAME", ConstCls.UNTENSHA_NAME_1);
            // 運搬終了年月日
            titleUpn1Table.Add("UPN_END_DATE", ConstCls.UPN_END_DATE_1);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HashTableの作成
        /// </summary>
        /// <returns></returns>
        private void CreateUpn2Table()
        {
            LogUtility.DebugMethodStart();
            // 運搬区間
            titleUpn2Table.Add("UPN_ROUTE_NO", ConstCls.UPN_ROUTE_NO_2);
            // 運搬受託者CD
            titleUpn2Table.Add("UPN_GYOUSHA_CD", ConstCls.UPN_GYOUSHA_CD_2);
            // 運搬受託者名称
            titleUpn2Table.Add("UPN_GYOUSHA_NAME", ConstCls.UPN_GYOUSHA_NAME_2);
            // 運搬受託者郵便番号
            titleUpn2Table.Add("UPN_GYOUSHA_POST", ConstCls.UPN_GYOUSHA_POST_2);
            // 運搬受託者電話番号
            titleUpn2Table.Add("UPN_GYOUSHA_TEL", ConstCls.UPN_GYOUSHA_TEL_2);
            // 運搬受託者住所
            titleUpn2Table.Add("UPN_GYOUSHA_ADDRESS", ConstCls.UPN_GYOUSHA_ADDRESS_2);
            // 運搬方法CD
            titleUpn2Table.Add("UPN_HOUHOU_CD", ConstCls.UPN_HOUHOU_CD_2);
            // 運搬方法名
            titleUpn2Table.Add("UNPAN_HOUHOU_NAME", ConstCls.UNPAN_HOUHOU_NAME_2);
            // 車種CD
            titleUpn2Table.Add("SHASHU_CD", ConstCls.SHASHU_CD_2);
            // 車種名
            titleUpn2Table.Add("SHASHU_NAME", ConstCls.SHASHU_NAME_2);
            // 車輌CD
            titleUpn2Table.Add("SHARYOU_CD", ConstCls.SHARYOU_CD_2);
            // 車輌名
            titleUpn2Table.Add("SHARYOU_NAME", ConstCls.SHARYOU_NAME_2);
            // 積替保管有無
            titleUpn2Table.Add("TMH_KBN", ConstCls.TMH_KBN_2);
            // 積替保管有無名称
            titleUpn2Table.Add("TMH_KBN_NAME", ConstCls.TMH_KBN_NAME_2);
            // 運搬先区分
            titleUpn2Table.Add("UPN_SAKI_KBN", ConstCls.UPN_SAKI_KBN_2);
            // 運搬先区分名
            titleUpn2Table.Add("UPN_SAKI_KBN_NAME", ConstCls.UPN_SAKI_KBN_NAME_2);
            // 運搬先の事業者CD
            titleUpn2Table.Add("UPN_SAKI_GYOUSHA_CD", ConstCls.UPN_SAKI_GYOUSHA_CD_2);
            // 運搬先の事業場CD
            titleUpn2Table.Add("UPN_SAKI_GENBA_CD", ConstCls.UPN_SAKI_GENBA_CD_2);
            // 運搬先の事業場名称
            titleUpn2Table.Add("UPN_SAKI_GENBA_NAME", ConstCls.UPN_SAKI_GENBA_NAME_2);
            // 運搬先の事業場郵便番号
            titleUpn2Table.Add("UPN_SAKI_GENBA_POST", ConstCls.UPN_SAKI_GENBA_POST_2);
            // 運搬先の事業場電話番号
            titleUpn2Table.Add("UPN_SAKI_GENBA_TEL", ConstCls.UPN_SAKI_GENBA_TEL_2);
            // 運搬先の事業場住所
            titleUpn2Table.Add("UPN_SAKI_GENBA_ADDRESS", ConstCls.UPN_SAKI_GENBA_ADDRESS_2);
            // 運搬の受託者CD
            titleUpn2Table.Add("UPN_JYUTAKUSHA_CD", ConstCls.UPN_JYUTAKUSHA_CD_2);
            // 運搬の受託者名称
            titleUpn2Table.Add("UPN_JYUTAKUSHA_NAME", ConstCls.UPN_JYUTAKUSHA_NAME_2);
            // 運転者CD
            titleUpn2Table.Add("UNTENSHA_CD", ConstCls.UNTENSHA_CD_2);
            // 運転者名
            titleUpn2Table.Add("UNTENSHA_NAME", ConstCls.UNTENSHA_NAME_2);
            // 運搬終了年月日
            titleUpn2Table.Add("UPN_END_DATE", ConstCls.UPN_END_DATE_2);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// HashTableの作成
        /// </summary>
        /// <returns></returns>
        private void CreateUpn3Table()
        {
            LogUtility.DebugMethodStart();
            // 運搬区間
            titleUpn3Table.Add("UPN_ROUTE_NO", ConstCls.UPN_ROUTE_NO_3);
            // 運搬受託者CD
            titleUpn3Table.Add("UPN_GYOUSHA_CD", ConstCls.UPN_GYOUSHA_CD_3);
            // 運搬受託者名称
            titleUpn3Table.Add("UPN_GYOUSHA_NAME", ConstCls.UPN_GYOUSHA_NAME_3);
            // 運搬受託者郵便番号
            titleUpn3Table.Add("UPN_GYOUSHA_POST", ConstCls.UPN_GYOUSHA_POST_3);
            // 運搬受託者電話番号
            titleUpn3Table.Add("UPN_GYOUSHA_TEL", ConstCls.UPN_GYOUSHA_TEL_3);
            // 運搬受託者住所
            titleUpn3Table.Add("UPN_GYOUSHA_ADDRESS", ConstCls.UPN_GYOUSHA_ADDRESS_3);
            // 運搬方法CD
            titleUpn3Table.Add("UPN_HOUHOU_CD", ConstCls.UPN_HOUHOU_CD_3);
            // 運搬方法名
            titleUpn3Table.Add("UNPAN_HOUHOU_NAME", ConstCls.UNPAN_HOUHOU_NAME_3);
            // 車種CD
            titleUpn3Table.Add("SHASHU_CD", ConstCls.SHASHU_CD_3);
            // 車種名
            titleUpn3Table.Add("SHASHU_NAME", ConstCls.SHASHU_NAME_3);
            // 車輌CD
            titleUpn3Table.Add("SHARYOU_CD", ConstCls.SHARYOU_CD_3);
            // 車輌名
            titleUpn3Table.Add("SHARYOU_NAME", ConstCls.SHARYOU_NAME_3);
            // 積替保管有無
            titleUpn3Table.Add("TMH_KBN", ConstCls.TMH_KBN_3);
            // 積替保管有無名称
            titleUpn3Table.Add("TMH_KBN_NAME", ConstCls.TMH_KBN_NAME_3);
            // 運搬先区分
            titleUpn3Table.Add("UPN_SAKI_KBN", ConstCls.UPN_SAKI_KBN_3);
            // 運搬先区分名
            titleUpn3Table.Add("UPN_SAKI_KBN_NAME", ConstCls.UPN_SAKI_KBN_NAME_3);
            // 運搬先の事業者CD
            titleUpn3Table.Add("UPN_SAKI_GYOUSHA_CD", ConstCls.UPN_SAKI_GYOUSHA_CD_3);
            // 運搬先の事業場CD
            titleUpn3Table.Add("UPN_SAKI_GENBA_CD", ConstCls.UPN_SAKI_GENBA_CD_3);
            // 運搬先の事業場名称
            titleUpn3Table.Add("UPN_SAKI_GENBA_NAME", ConstCls.UPN_SAKI_GENBA_NAME_3);
            // 運搬先の事業場郵便番号
            titleUpn3Table.Add("UPN_SAKI_GENBA_POST", ConstCls.UPN_SAKI_GENBA_POST_3);
            // 運搬先の事業場電話番号
            titleUpn3Table.Add("UPN_SAKI_GENBA_TEL", ConstCls.UPN_SAKI_GENBA_TEL_3);
            // 運搬先の事業場住所
            titleUpn3Table.Add("UPN_SAKI_GENBA_ADDRESS", ConstCls.UPN_SAKI_GENBA_ADDRESS_3);
            // 運搬の受託者CD
            titleUpn3Table.Add("UPN_JYUTAKUSHA_CD", ConstCls.UPN_JYUTAKUSHA_CD_3);
            // 運搬の受託者名称
            titleUpn3Table.Add("UPN_JYUTAKUSHA_NAME", ConstCls.UPN_JYUTAKUSHA_NAME_3);
            // 運転者CD
            titleUpn3Table.Add("UNTENSHA_CD", ConstCls.UNTENSHA_CD_3);
            // 運転者名
            titleUpn3Table.Add("UNTENSHA_NAME", ConstCls.UNTENSHA_NAME_3);
            // 運搬終了年月日
            titleUpn3Table.Add("UPN_END_DATE", ConstCls.UPN_END_DATE_3);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コード項目
        /// DgvCustomAlphaNumTextBoxColumnコントロールの作成
        /// </summary>
        /// <returns></returns>
        private void CreateAlphaNumTextBoxColumn(string ronriName)
        {
            LogUtility.DebugMethodStart(ronriName);
            try
            {
                r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn customAlphaNumTextBoxCell
                    = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();

                customAlphaNumTextBoxCell.HeaderText = ronriName;
                customAlphaNumTextBoxCell.Name = ronriName;
                customAlphaNumTextBoxCell.Tag = "1";
                customAlphaNumTextBoxCell.DefaultCellStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                customAlphaNumTextBoxCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                r_framework.Dto.PopupSearchSendParamDto paramDto = new r_framework.Dto.PopupSearchSendParamDto();
                paramDto.And_Or = CONDITION_OPERATOR.AND;
                paramDto.KeyName = "TEKIYOU_BEGIN";
                paramDto.Control = ConstCls.KOUFU_DATE;

                r_framework.Dto.JoinMethodDto joinDto = new r_framework.Dto.JoinMethodDto();
                joinDto.Join = JOIN_METHOD.WHERE;
                joinDto.LeftTable = "";
                r_framework.Dto.SearchConditionsDto subDto = new r_framework.Dto.SearchConditionsDto();
                subDto.And_Or = CONDITION_OPERATOR.AND;
                subDto.LeftColumn = "TEKIYOU_FLG";
                subDto.Value = "FALSE";
                joinDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinDto.SearchCondition.Add(subDto);

                // マニフェスト種類CD
                if (ConstCls.MANIFEST_SHURUI_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    customAlphaNumTextBoxCell.HeaderText = ronriName + "※";
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.ToolTipText = "1:直行　2:建廃　3:積替　を設定してください";
                    customAlphaNumTextBoxCell.MinimumWidth = 160;
                }
                // 一次二次区分
                else if (ConstCls.FIRST_SECOND_KBN.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.HeaderText = ronriName + "※";
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.MinimumWidth = 8;
                    customAlphaNumTextBoxCell.ToolTipText = "1:一次　2:二次　を設定してください";
                }
                // 取引先CD
                else if (ConstCls.TORIHIKISAKI_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
                    customAlphaNumTextBoxCell.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.TORIHIKISAKI_CD + "," + ConstCls.TORIHIKISAKI_NAME;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                }
                // 交付番号区分
                else if (ConstCls.KOUFU_KBN.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.MinimumWidth = 8;
                    customAlphaNumTextBoxCell.ToolTipText = "1:通常　2:例外　を設定してください";
                }
                // 交付番号
                else if (ConstCls.MANIFEST_ID.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 11;
                    customAlphaNumTextBoxCell.MinimumWidth = 88;

                    //デフォルトはReadOnlyにし、交付番号区分の入力で有無で切り換える
                    customAlphaNumTextBoxCell.ReadOnly = true;
                }
                // 整理番号
                else if (ConstCls.SEIRI_ID.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 20;
                    customAlphaNumTextBoxCell.MinimumWidth = 160;
                }
                // 排出事業者CD
                else if (ConstCls.HST_GYOUSHA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "HstGyoushaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_GYOUSHA";

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                    searchDto1.Value = "True";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;

                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(searchDto1);

                    // PopupWindowSetting = 排出事業者区分=TRUE
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);
                }
                // 排出事業場CD
                else if (ConstCls.HST_GENBA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "HstGenbaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;

                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "複数キー用検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
                    //customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SetAddressGyousha";
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                }
                // 最終処分の場所（予定）区分
                else if (ConstCls.LAST_SBN_YOTEI_KBN.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.MinimumWidth = 8;
                    customAlphaNumTextBoxCell.ToolTipText = "0:設定なし　1:委託契約書記載のとおり　2:当欄記載のとおり　を設定してください";
                }
                // 最終処分の場所（予定）業者CD
                else if (ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "LastSbnYoteiGyoushaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                    searchDto1.Value = "True";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_GYOUSHA";
                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(searchDto1);

                    // 処分受託者区分=TRUE
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);
                }
                // 最終処分の場所（予定）現場CD
                else if (ConstCls.LAST_SBN_YOTEI_GENBA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "LastSbnYoteiGenbaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "複数キー用検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                }
                // 積替保管 業者CD
                else if (ConstCls.TMH_GYOUSHA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "TMH_GYOUSHA_CDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.GetCodeMasterField = "GYOUSHA_CD";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.TMH_GYOUSHA_CD;

                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    r_framework.Dto.PopupSearchSendParamDto searchDto = new r_framework.Dto.PopupSearchSendParamDto();
                    searchDto.KeyName = "GYOUSHAKBN_MANI";
                    searchDto.Value = "True";
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(searchDto);
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    var dto = new r_framework.Dto.JoinMethodDto() //現場をJOIN
                    {
                        Join = JOIN_METHOD.INNER_JOIN,
                        LeftTable = "M_GENBA",
                        LeftKeyColumn = "GYOUSHA_CD",
                        RightTable = "M_GYOUSHA",
                        RightKeyColumn = "GYOUSHA_CD",
                    };
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(dto);
                    
                    //JOINした上で、積替保管に絞る
                    dto = new r_framework.Dto.JoinMethodDto()
                    {
                        Join = JOIN_METHOD.WHERE,
                        LeftTable = "M_GENBA",
                        SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>()
                    };
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(dto);

                    dto.SearchCondition.Add(new r_framework.Dto.SearchConditionsDto() {
                        And_Or = CONDITION_OPERATOR.AND ,
                        LeftColumn = "TSUMIKAEHOKAN_KBN",
                        Value = "True",
                        ValueColumnType = DB_TYPE.BIT,
                        Condition = JUGGMENT_CONDITION.EQUALS
                    });


                }
                // 積替保管 現場CD
                else if (ConstCls.TMH_GENBA_CD.Equals(ronriName))
                {

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "TMH_GENBA_CDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "複数キー用検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.GetCodeMasterField = "GYOUSHA_CD,GENBA_CD";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.TMH_GYOUSHA_CD + "," + ConstCls.TMH_GENBA_CD;

                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(new r_framework.Dto.PopupSearchSendParamDto()
                        {
                            And_Or = CONDITION_OPERATOR.AND,
                            KeyName = "TSUMIKAEHOKAN_KBN",
                            Value = "True"
                        });

                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "M_GYOUSHA.GYOUSHA_CD",
                        Control = ConstCls.TMH_GYOUSHA_CD
                    });

                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "M_GYOUSHA.GYOUSHAKBN_MANI",
                        Value = "True"
                    });
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);
                }


                #region 処分
                // 処分受託者CD
                else if (ConstCls.SBN_GYOUSHA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SbnGyoushaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                    searchDto1.Value = "True";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_GYOUSHA";
                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(searchDto1);

                    // 処分受託者区分=TRUE
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);
                }
                // 処分の受領者CD
                else if (ConstCls.SBN_JYURYOUSHA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SbnJyuryoushaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                    searchDto1.Value = "True";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_GYOUSHA";
                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(searchDto1);

                    // 処分受託者区分=TRUE
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);
                }
                // 処分の受領担当者CD
                else if (ConstCls.SBN_JYURYOU_TANTOU_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHOBUN_TANTOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    customAlphaNumTextBoxCell.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.SBN_JYURYOU_TANTOU_CD + ","
                                                           + ConstCls.SBN_JYURYOU_TANTOU_NAME;

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                    r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "SHOBUN_TANTOU_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_SHAIN";
                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(subDto);

                    methodDto1.Join = JOIN_METHOD.INNER_JOIN;
                    methodDto1.LeftKeyColumn = "SHAIN_CD";
                    methodDto1.LeftTable = "M_SHOBUN_TANTOUSHA";
                    methodDto1.RightKeyColumn = "SHAIN_CD";
                    methodDto1.RightTable = "M_SHAIN";

                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto1);
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);

                    // 処分担当者ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "社員マスタコードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);
                }
                // 処分の受託者CD
                else if (ConstCls.SBN_JYUTAKUSHA_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SbnJyutakushaCDPopupAfter";

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                    searchDto1.Value = "True";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_GYOUSHA";
                    methodDto.SearchCondition.Add(searchDto);

                    // 処分受託者区分=TRUE
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);
                }
                // 処分担当者CD
                else if (ConstCls.SBN_TANTOU_CD.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHOBUN_TANTOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    customAlphaNumTextBoxCell.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.SBN_TANTOU_CD + ","
                                                           + ConstCls.SBN_TANTOU_NAME;

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();
                    r_framework.Dto.JoinMethodDto methodDto1 = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "SHOBUN_TANTOU_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_SHAIN";
                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(subDto);

                    methodDto1.Join = JOIN_METHOD.INNER_JOIN;
                    methodDto1.LeftKeyColumn = "SHAIN_CD";
                    methodDto1.LeftTable = "M_SHOBUN_TANTOUSHA";
                    methodDto1.RightKeyColumn = "SHAIN_CD";
                    methodDto1.RightTable = "M_SHAIN";

                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto1);
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);

                    // 処分担当者ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "社員マスタコードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);
                }
                #endregion
                #region 運搬
                // 区間1：運搬受託者CD、区間2：運搬受託者CD、区間3：運搬受託者CD
                else if (ConstCls.UPN_GYOUSHA_CD_1.Equals(ronriName)
                    || ConstCls.UPN_GYOUSHA_CD_2.Equals(ronriName)
                    || ConstCls.UPN_GYOUSHA_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    //2014-03-14 Upd ogawamut No.3506
                    if (ConstCls.UPN_GYOUSHA_CD_1.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "UpnGyoushaCD1PopupAfter";
                    }
                    else if (ConstCls.UPN_GYOUSHA_CD_2.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "UpnGyoushaCD2PopupAfter";
                    }
                    else if (ConstCls.UPN_GYOUSHA_CD_3.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "UpnGyoushaCD3PopupAfter";
                    }

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;

                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    r_framework.Dto.SearchConditionsDto searchDto = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.SearchConditionsDto searchDto1 = new r_framework.Dto.SearchConditionsDto();
                    r_framework.Dto.JoinMethodDto methodDto = new r_framework.Dto.JoinMethodDto();

                    searchDto.And_Or = CONDITION_OPERATOR.AND;
                    searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                    searchDto.Value = "True";
                    searchDto.ValueColumnType = DB_TYPE.BIT;

                    searchDto1.And_Or = CONDITION_OPERATOR.AND;
                    searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                    searchDto1.LeftColumn = "GYOUSHAKBN_MANI";
                    searchDto1.Value = "True";
                    searchDto1.ValueColumnType = DB_TYPE.BIT;

                    methodDto.Join = JOIN_METHOD.WHERE;
                    methodDto.LeftTable = "M_GYOUSHA";
                    methodDto.SearchCondition.Add(searchDto);
                    methodDto.SearchCondition.Add(searchDto1);

                    // 運搬受託者区分=TRUE
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto);
                }
                // 区間1：運搬方法CD、区間2：運搬方法CD、区間3：運搬方法CD
                else if (ConstCls.UPN_HOUHOU_CD_1.Equals(ronriName)
                    || ConstCls.UPN_HOUHOU_CD_2.Equals(ronriName)
                    || ConstCls.UPN_HOUHOU_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.MinimumWidth = 8;
                }
                // 区間1：車種CD、区間2：車種CD、区間3：車種CD
                else if (ConstCls.SHASHU_CD_1.Equals(ronriName)
                    || ConstCls.SHASHU_CD_2.Equals(ronriName)
                    || ConstCls.SHASHU_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 3;
                    customAlphaNumTextBoxCell.MinimumWidth = 24;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 3;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHASHU;
                    customAlphaNumTextBoxCell.GetCodeMasterField = "SHASHU_CD,SHASHU_NAME_RYAKU";
                    customAlphaNumTextBoxCell.ToolTipText = "3 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    if (ConstCls.SHASHU_CD_1.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SetShashu1PopUp";
                        customAlphaNumTextBoxCell.SetFormField = ConstCls.SHASHU_CD_1 + "," + ConstCls.SHASHU_NAME_1;
                    }
                    else if (ConstCls.SHASHU_CD_2.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SetShashu2PopUp";
                        customAlphaNumTextBoxCell.SetFormField = ConstCls.SHASHU_CD_2 + "," + ConstCls.SHASHU_NAME_2;
                    }
                    else if (ConstCls.SHASHU_CD_3.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "SetShashu3PopUp";
                        customAlphaNumTextBoxCell.SetFormField = ConstCls.SHASHU_CD_3 + "," + ConstCls.SHASHU_NAME_3;
                    }

                    // 車種ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "車種マスタコードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);

                    joinDto.LeftTable = "M_SHASHU";
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(joinDto);
                }
                // 区間1：車輌CD、区間2：車輌CD、区間3：車輌CD
                else if (ConstCls.SHARYOU_CD_1.Equals(ronriName)
                    || ConstCls.SHARYOU_CD_2.Equals(ronriName)
                    || ConstCls.SHARYOU_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "車両選択共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHARYOU;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    customAlphaNumTextBoxCell.GetCodeMasterField = "SHARYOU_CD,SHARYOU_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU,SHASHU_CD,SHASHU_NAME";

                    if (ConstCls.SHARYOU_CD_1.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.SetFormField
                            = ConstCls.SHARYOU_CD_1 + "," + ConstCls.SHARYOU_NAME_1 + "," + ConstCls.UPN_GYOUSHA_CD_1 + "," + ConstCls.UPN_GYOUSHA_NAME_1 + ","
                            + ConstCls.SHASHU_CD_1 + "," + ConstCls.SHASHU_NAME_1;
                    }
                    else if (ConstCls.SHARYOU_CD_2.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.SetFormField
                            = ConstCls.SHARYOU_CD_2 + "," + ConstCls.SHARYOU_NAME_2 + "," + ConstCls.UPN_GYOUSHA_CD_2 + "," + ConstCls.UPN_GYOUSHA_NAME_2 + ","
                            + ConstCls.SHASHU_CD_2 + "," + ConstCls.SHASHU_NAME_2;
                    }
                    else if (ConstCls.SHARYOU_CD_3.Equals(ronriName))
                    {
                        customAlphaNumTextBoxCell.SetFormField
                            = ConstCls.SHARYOU_CD_3 + "," + ConstCls.SHARYOU_NAME_3 + "," + ConstCls.UPN_GYOUSHA_CD_3 + "," + ConstCls.UPN_GYOUSHA_NAME_3 + ","
                            + ConstCls.SHASHU_CD_3 + "," + ConstCls.SHASHU_NAME_3;
                    }
                }
                // 区間1：積替保管有無、区間2：積替保管有無、区間3：積替保管有無
                else if (ConstCls.TMH_KBN_1.Equals(ronriName)
                    || ConstCls.TMH_KBN_2.Equals(ronriName)
                    || ConstCls.TMH_KBN_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.MinimumWidth = 8;
                    customAlphaNumTextBoxCell.ToolTipText = "1:有　2:無　を設定してください";
                }
                // 区間1：運搬先区分、区間2：運搬先区分、区間3：運搬先区分
                else if (ConstCls.UPN_SAKI_KBN_1.Equals(ronriName)
                    || ConstCls.UPN_SAKI_KBN_2.Equals(ronriName)
                    || ConstCls.UPN_SAKI_KBN_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 1;
                    customAlphaNumTextBoxCell.MinimumWidth = 8;
                    customAlphaNumTextBoxCell.ToolTipText = "1:処分施設　2:積替保管　を設定してください";
                }
                // 区間1：運搬先の事業者CD、区間2：運搬先の事業者CD、区間3：運搬先の事業者CD
                else if (ConstCls.UPN_SAKI_GYOUSHA_CD_1.Equals(ronriName)
                    || ConstCls.UPN_SAKI_GYOUSHA_CD_2.Equals(ronriName)
                    || ConstCls.UPN_SAKI_GYOUSHA_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
               
                }
                // 区間1：運搬先の事業場CD、区間2：運搬先の事業場CD、区間3：運搬先の事業場CD
                else if (ConstCls.UPN_SAKI_GENBA_CD_1.Equals(ronriName)
                    || ConstCls.UPN_SAKI_GENBA_CD_2.Equals(ronriName)
                    || ConstCls.UPN_SAKI_GENBA_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;

                    //現場
                    customAlphaNumTextBoxCell.PopupWindowName = "複数キー用検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    customAlphaNumTextBoxCell.PopupBeforeExecuteMethod = "UPN_SAKI_GENBA_CD_1PopupBefore";
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "UPN_SAKI_GENBA_CD_1PopupAfter";

                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(paramDto);

                }
                // 区間1：運搬の受託者CD、区間2：運搬の受託者CD、区間3：運搬の受託者CD
                else if (ConstCls.UPN_JYUTAKUSHA_CD_1.Equals(ronriName)
                    || ConstCls.UPN_JYUTAKUSHA_CD_2.Equals(ronriName)
                    || ConstCls.UPN_JYUTAKUSHA_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                }
                // 区間1：運転者CD、区間2：運転者CD、区間3：運転者CD
                else if (ConstCls.UNTENSHA_CD_1.Equals(ronriName)
                    || ConstCls.UNTENSHA_CD_2.Equals(ronriName)
                    || ConstCls.UNTENSHA_CD_3.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                }
                #endregion
                // 廃棄物種類CD
                else if (ConstCls.HAIKI_SHURUI_CD_RYAKU.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 4;
                    customAlphaNumTextBoxCell.MinimumWidth = 32;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 4;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI;
                    customAlphaNumTextBoxCell.ToolTipText = "4 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.GetCodeMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.HAIKI_SHURUI_CD_RYAKU + "," + ConstCls.HAIKI_SHURUI_NAME_RYAKU;

                    joinDto.LeftTable = "M_HAIKI_SHURUI";
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(joinDto);
                }
                // 廃棄物名称CD
                else if (ConstCls.HAIKI_CD_RYAKU.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_HAIKI_NAME;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.GetCodeMasterField = "HAIKI_NAME_CD,HAIKI_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.HAIKI_CD_RYAKU + "," + ConstCls.HAIKI_NAME_RYAKU;

                    // 廃棄物名称ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "廃棄物名称コードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);

                    joinDto.LeftTable = "M_HAIKI_NAME";
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(joinDto);
                }
                // 荷姿CD
                else if (ConstCls.NISUGATA_CD_RYAKU.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 2;
                    customAlphaNumTextBoxCell.MinimumWidth = 16;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 2;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_NISUGATA;
                    customAlphaNumTextBoxCell.ToolTipText = "2 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.GetCodeMasterField = "NISUGATA_CD,NISUGATA_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.NISUGATA_CD_RYAKU + "," + ConstCls.NISUGATA_NAME_RYAKU;

                    // 荷姿ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "荷姿コードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);

                    joinDto.LeftTable = "M_NISUGATA";
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(joinDto);
                }
                // 単位CD
                else if (ConstCls.UNIT_CD_RYAKU.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 2;
                    customAlphaNumTextBoxCell.MinimumWidth = 16;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 2;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNIT;
                    customAlphaNumTextBoxCell.ToolTipText = "2 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";
                    customAlphaNumTextBoxCell.AlphabetLimitFlag = false;
                    customAlphaNumTextBoxCell.GetCodeMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.UNIT_CD_RYAKU + "," + ConstCls.UNIT_NAME_RYAKU;

                    // 単位ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "単位マスタコードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);

                    joinDto.LeftTable = "M_UNIT";
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(joinDto);
                }
                // 処分方法CD
                else if (ConstCls.SHOBUN_HOUHOU_CD_RYAKU.Equals(ronriName))
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 3;
                    customAlphaNumTextBoxCell.MinimumWidth = 24;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 3;
                    customAlphaNumTextBoxCell.PopupWindowName = "マスタ共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHOBUN_HOUHOU;
                    customAlphaNumTextBoxCell.ToolTipText = "3 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.GetCodeMasterField = "SHOBUN_HOUHOU_CD,SHOBUN_HOUHOU_NAME_RYAKU";
                    customAlphaNumTextBoxCell.SetFormField = ConstCls.SHOBUN_HOUHOU_CD_RYAKU + "," + ConstCls.SHOBUN_HOUHOU_NAME_RYAKU;

                    // 処分方法ﾏｽﾀのチェック
                    customAlphaNumTextBoxCell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "処分方法マスタコードチェックandセッティング";
                    customAlphaNumTextBoxCell.FocusOutCheckMethod.Add(dto);

                    joinDto.LeftTable = "M_SHOBUN_HOUHOU";
                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(joinDto);
                }
                // 最終処分業者CD
                else if (ConstCls.LAST_SBN_GYOUSHA_CD == ronriName)
                {
                    customAlphaNumTextBoxCell.PopupAfterExecuteMethod = "LastSbnGyoushaCDPopupAfter";
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "GYOUSHAKBN_MANI",
                        Value = "True"
                    });
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "SHOBUN_NIOROSHI_GYOUSHA_KBN",
                        Value = "True"
                    });
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "TEKIYOU_BEGIN",
                        Control = ConstCls.KOUFU_DATE
                    });

                    var methodDto1 = new r_framework.Dto.JoinMethodDto()
                    {
                        Join = JOIN_METHOD.INNER_JOIN,
                        LeftKeyColumn = "GYOUSHA_CD",
                        LeftTable = "M_GENBA",
                        RightKeyColumn = "GYOUSHA_CD",
                        RightTable = "M_GYOUSHA"
                    };

                    var methodDto2 = new r_framework.Dto.JoinMethodDto()
                    {
                        Join = JOIN_METHOD.WHERE,
                        LeftTable = "M_GENBA",
                        SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>()
                    };

                    methodDto2.SearchCondition.Add(new r_framework.Dto.SearchConditionsDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        Condition = JUGGMENT_CONDITION.EQUALS,
                        LeftColumn = "SAISHUU_SHOBUNJOU_KBN",
                        Value = "True",
                        ValueColumnType = DB_TYPE.BIT
                    });

                    customAlphaNumTextBoxCell.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
                    //"最終処分業者CD,最終処分業者名";
                    customAlphaNumTextBoxCell.SetFormField = String.Join(",", new string[]{ConstCls.LAST_SBN_GYOUSHA_CD,
                                                                                           ConstCls.LAST_SBN_GYOUSHA_NAME});

                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto1);
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto2);
                }
                // 最終処分現場CD
                else if (ConstCls.LAST_SBN_GENBA_CD == ronriName)
                {
                    customAlphaNumTextBoxCell.MaxInputLength = 6;
                    customAlphaNumTextBoxCell.MinimumWidth = 48;
                    customAlphaNumTextBoxCell.ZeroPaddengFlag = true;
                    customAlphaNumTextBoxCell.CharactersNumber = 6;
                    customAlphaNumTextBoxCell.PopupWindowName = "複数キー用検索共通ポップアップ";
                    customAlphaNumTextBoxCell.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
                    customAlphaNumTextBoxCell.ToolTipText = "6 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    var methodDto1 = new r_framework.Dto.JoinMethodDto()
                    {
                        Join = JOIN_METHOD.WHERE,
                        LeftTable = "M_GYOUSHA",
                        SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>()
                    };

                    methodDto1.SearchCondition.Add(new r_framework.Dto.SearchConditionsDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        Condition = JUGGMENT_CONDITION.EQUALS,
                        LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN",
                        Value = "True",
                        ValueColumnType = DB_TYPE.BIT
                    });

                    methodDto1.SearchCondition.Add(new r_framework.Dto.SearchConditionsDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        Condition = JUGGMENT_CONDITION.EQUALS,
                        LeftColumn = "GYOUSHAKBN_MANI",
                        Value = "True",
                        ValueColumnType = DB_TYPE.BIT
                    });

                    customAlphaNumTextBoxCell.popupWindowSetting.Clear();
                    customAlphaNumTextBoxCell.popupWindowSetting.Add(methodDto1);

                    var sparam1 = new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        Control = ConstCls.LAST_SBN_GYOUSHA_CD,
                        KeyName = "GYOUSHA_CD"
                    };

                    var sparam2 = new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "SAISHUU_SHOBUNJOU_KBN",
                        Value = "True"
                    };

                    var sparam3 = new r_framework.Dto.PopupSearchSendParamDto()
                    {
                        And_Or = CONDITION_OPERATOR.AND,
                        KeyName = "TEKIYOU_BEGIN",
                        Control = ConstCls.KOUFU_DATE
                    };

                    customAlphaNumTextBoxCell.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
                    //"最終処分業者CD,最終処分業者名,最終処分現場CD,最終処分現場名";
                    customAlphaNumTextBoxCell.SetFormField = String.Join(",", new string[]{ConstCls.LAST_SBN_GYOUSHA_CD,
                                                                                           ConstCls.LAST_SBN_GYOUSHA_NAME,
                                                                                           ConstCls.LAST_SBN_GENBA_CD,
                                                                                           ConstCls.LAST_SBN_GENBA_NAME});
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Clear();
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(sparam1);
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(sparam2);
                    customAlphaNumTextBoxCell.PopupSearchSendParams.Add(sparam3);
                }

                // タイトルを追加
                this.form.NyuryokuIkkatsuItiran.Columns.Add(customAlphaNumTextBoxCell);
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Error(ex);
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(ronriName);
        }

        /// <summary>
        /// 表示名称項目
        /// DgvCustomTextBoxColumnコントロールの作成
        /// </summary>
        /// <returns></returns>
        private void CreateReadOnlyTextBoxColumn(string ronriName)
        {
            LogUtility.DebugMethodStart(ronriName);

            try
            {
                // 区間1（運搬区間）、区間2（運搬区間）、区間3（運搬区間）
                if (ronriName.Equals(ConstCls.UPN_ROUTE_NO_1)
                    || ronriName.Equals(ConstCls.UPN_ROUTE_NO_2)
                    || ronriName.Equals(ConstCls.UPN_ROUTE_NO_3))
                {
                    return;
                }
                r_framework.CustomControl.DgvCustomTextBoxColumn customTextBoxCell
                    = new r_framework.CustomControl.DgvCustomTextBoxColumn();

                customTextBoxCell.HeaderText = ronriName;
                customTextBoxCell.Name = ronriName;
                customTextBoxCell.Tag = "4";
                customTextBoxCell.ReadOnly = true;
                customTextBoxCell.DefaultCellStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                customTextBoxCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                customTextBoxCell.MinimumWidth = 88;

                // タイトルを追加
                this.form.NyuryokuIkkatsuItiran.Columns.Add(customTextBoxCell);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex, ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(ronriName);
        }

        /// <summary>
        /// 数値項目
        /// DgvCustomNumericTextBox2Columnコントロールの作成
        /// </summary>
        /// <returns></returns>
        private void CreateNumericNumTextBoxColumn(string ronriName)
        {
            LogUtility.DebugMethodStart(ronriName);

            try
            {
                r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column CustomNumericTextBox2Cell
                    = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();

                CustomNumericTextBox2Cell.HeaderText = ronriName;
                CustomNumericTextBox2Cell.Name = ronriName;
                CustomNumericTextBox2Cell.Tag = "1";
                CustomNumericTextBox2Cell.DefaultCellStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                CustomNumericTextBox2Cell.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                // 拠点CD
                if (ConstCls.KYOTEN_CD.Equals(ronriName))
                {
                    CustomNumericTextBox2Cell.MaxInputLength = 2;
                    CustomNumericTextBox2Cell.MinimumWidth = 16;
                    CustomNumericTextBox2Cell.ZeroPaddengFlag = true;
                    CustomNumericTextBox2Cell.CharactersNumber = 2;
                    CustomNumericTextBox2Cell.PopupWindowName = "マスタ共通ポップアップ";
                    CustomNumericTextBox2Cell.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
                    CustomNumericTextBox2Cell.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
                    CustomNumericTextBox2Cell.SetFormField = ConstCls.KYOTEN_CD + "," + ConstCls.KYOTEN_NAME;
                    CustomNumericTextBox2Cell.ToolTipText = "2 文字以内で入力してください。スペースキー押下にて、検索画面を表示します。";

                    //2014-03-13 Add ogawamut No.3020
                    CustomNumericTextBox2Cell.popupWindowSetting = new Collection<r_framework.Dto.JoinMethodDto>();
                    var JoinDto = new r_framework.Dto.JoinMethodDto();
                    JoinDto.Join = JOIN_METHOD.WHERE;
                    JoinDto.LeftKeyColumn = "KYOTEN_CD";
                    JoinDto.LeftTable = "M_KYOTEN";
                    var JoinDtoOption = new r_framework.Dto.SearchConditionsDto();
                    JoinDtoOption.And_Or = CONDITION_OPERATOR.AND;
                    JoinDtoOption.Condition = JUGGMENT_CONDITION.NOT_EQUALS;
                    JoinDtoOption.LeftColumn = "KYOTEN_CD";
                    JoinDtoOption.Value = "99";
                    JoinDtoOption.ValueColumnType = DB_TYPE.SMALLINT;
                    JoinDto.SearchCondition.Add(JoinDtoOption);
                    r_framework.Dto.SearchConditionsDto subDto = new r_framework.Dto.SearchConditionsDto();
                    subDto.And_Or = CONDITION_OPERATOR.AND;
                    subDto.LeftColumn = "TEKIYOU_FLG";
                    subDto.Value = "FALSE";
                    JoinDto.SearchCondition.Add(subDto);
                    CustomNumericTextBox2Cell.popupWindowSetting.Add(JoinDto);

                    // 拠点マスタのチェック
                    CustomNumericTextBox2Cell.FocusOutCheckMethod = new Collection<r_framework.Dto.SelectCheckDto>();
                    r_framework.Dto.SelectCheckDto dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "拠点マスタコードチェックandセッティング";
                    CustomNumericTextBox2Cell.FocusOutCheckMethod.Add(dto);

                    //2014-03-13 Add ogawamut No.3020
                    dto = new r_framework.Dto.SelectCheckDto();
                    dto.CheckMethodName = "拠点マスタコード非存在チェック(存在する場合エラー)";
                    dto.DisplayMessage = "この拠点コードは使用できません。";
                    dto.RunCheckMethod = new Collection<r_framework.Dto.SelectRunCheckDto>();
                    var DtoOption = new r_framework.Dto.SelectRunCheckDto();
                    DtoOption.CheckMethodName = "一致チェック";
                    DtoOption.SendParams = new string[] { "拠点CD" };
                    DtoOption.Condition = new string[] { "99" };
                    dto.RunCheckMethod.Add(DtoOption);
                    CustomNumericTextBox2Cell.FocusOutCheckMethod.Add(dto);
                    r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
                    rangeSettingDto3.Max = new decimal(new int[] {
                     99,
                     0,
                     0,
                     0});
                    CustomNumericTextBox2Cell.RangeSetting = rangeSettingDto3;
                    CustomNumericTextBox2Cell.FormatSetting = "カスタム";
                    CustomNumericTextBox2Cell.CustomFormatSetting = "00";

                }
                // 数量
                else if (ConstCls.HAIKI_SUU.Equals(ronriName))
                {
                    r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
                    rangeSettingDto4.Max = new decimal(new int[] {
                    99999999,
                    0,
                    0,
                    0});
                    CustomNumericTextBox2Cell.RangeSetting = rangeSettingDto4;
                    CustomNumericTextBox2Cell.MaxInputLength = 8;
                    CustomNumericTextBox2Cell.MinimumWidth = 88;
                }

                // 換算後数量
                else if (ConstCls.KANSAN_SUU.Equals(ronriName))
                {
                    r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
                    rangeSettingDto5.Max = new decimal(new int[] {
                    99999999,
                    0,
                    0,
                    0});
                    CustomNumericTextBox2Cell.RangeSetting = rangeSettingDto5;
                    CustomNumericTextBox2Cell.MaxInputLength = 8;
                    CustomNumericTextBox2Cell.MinimumWidth = 88;
                }


                // タイトルを追加
                this.form.NyuryokuIkkatsuItiran.Columns.Add(CustomNumericTextBox2Cell);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex, ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(ronriName);
        }

        /// <summary>
        /// 入力名称項目
        /// DgvCustomTextBoxColumnコントロールの作成
        /// </summary>
        /// <returns></returns>
        private void CreateTextBoxColumn(string ronriName)
        {
            LogUtility.DebugMethodStart(ronriName);

            try
            {
                r_framework.CustomControl.DgvCustomTextBoxColumn customTextBoxCell
                    = new r_framework.CustomControl.DgvCustomTextBoxColumn();

                customTextBoxCell.HeaderText = ronriName;
                customTextBoxCell.Name = ronriName;
                customTextBoxCell.Tag = "2";
                customTextBoxCell.DefaultCellStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                customTextBoxCell.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                customTextBoxCell.MinimumWidth = 240;

                // 交付担当者
                if (ConstCls.KOUFU_TANTOUSHA.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 8;
                    customTextBoxCell.MinimumWidth = 128;
                }
                // 交付担当者所属
                if (ConstCls.KOUFU_TANTOUSHA_SHOZOKU.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 20;
                    customTextBoxCell.MinimumWidth = 128;
                }
                // 排出事業者住所
                else if (ConstCls.HST_GYOUSHA_ADDRESS.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 44;
                }
                // 排出事業場住所
                else if (ConstCls.HST_GENBA_ADDRESS.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 44;
                }
                // 備考
                else if (ConstCls.BIKOU.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 50;
                }
                // 最終処分の場所（予定）住所
                else if (ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 44;
                }
                // 処分受託者住所
                else if (ConstCls.SBN_GYOUSHA_ADDRESS.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 44;
                }
                // 区間1：運搬受託者住所、区間2：運搬受託者住所、区間3：運搬受託者住所
                else if (ConstCls.UPN_GYOUSHA_ADDRESS_1.Equals(ronriName)
                    || ConstCls.UPN_GYOUSHA_ADDRESS_2.Equals(ronriName)
                    || ConstCls.UPN_GYOUSHA_ADDRESS_3.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 44;
                }
                // 区間1：運搬先の事業場住所、区間2：運搬先の事業場住所、区間3：運搬先の事業場住所
                else if (ConstCls.UPN_SAKI_GENBA_ADDRESS_1.Equals(ronriName)
                    || ConstCls.UPN_SAKI_GENBA_ADDRESS_2.Equals(ronriName)
                    || ConstCls.UPN_SAKI_GENBA_ADDRESS_3.Equals(ronriName))
                {
                    customTextBoxCell.MaxInputLength = 44;
                }
                // タイトルを追加
                this.form.NyuryokuIkkatsuItiran.Columns.Add(customTextBoxCell);
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Error(ex);
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(ronriName);
        }

        /// <summary>
        /// 日付項目
        /// DgvCustomDataTimeColumnコントロールの作成
        /// </summary>
        /// <returns></returns>
        private void CreateDataTimeColumn(string ronriName)
        {
            LogUtility.DebugMethodStart(ronriName);

            try
            {
                r_framework.CustomControl.DgvCustomDataTimeColumn customDataTime = new r_framework.CustomControl.DgvCustomDataTimeColumn();

                customDataTime.HeaderText = ronriName;
                customDataTime.Name = ronriName;
                customDataTime.Tag = "3";
                customDataTime.DefaultCellStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                customDataTime.Width = 138;

                // タイトルを追加
                this.form.NyuryokuIkkatsuItiran.Columns.Add(customDataTime);

                r_framework.CustomControl.DataGridCustomControl.DgvCustomDataTimeCell cell =
                    (r_framework.CustomControl.DataGridCustomControl.DgvCustomDataTimeCell)
                    customDataTime.DataGridView.Rows[customDataTime.DataGridView.Rows.Count - 1].Cells[customDataTime.DataGridView.ColumnCount - 1];

                if (cell != null)
                {
                    cell.Value = null;
                }
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Error(ex);
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(ronriName);
        }

        //2013.11.23 naitou add 交付番号重複チェック追加 start
        /// <summary>
        /// 交付番号存在チェック
        /// </summary>
        public string ExistKohuNo(string haikiKbn, string kohuNo)
        {
            LogUtility.DebugMethodStart(haikiKbn, kohuNo);

            if (haikiKbn == string.Empty || kohuNo == string.Empty)
            {
                return string.Empty;
            }

            string ret = string.Empty;
            SearchKohuDtoCls search = new SearchKohuDtoCls();
            search.HAIKI_KBN_CD = haikiKbn;
            search.MANIFEST_ID = kohuNo;
            DataTable dt = new DataTable();
            dt = this.SearchKohuDao.GetDataForEntity(search);
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString();
            }

            LogUtility.DebugMethodEnd(haikiKbn, kohuNo);
            return ret;
        }
        //2013.11.23 naitou add 交付番号重複チェック追加 end

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
        // 20140609 syunrei  EV004722_拠点について start
        public void CallPattern()
        {
            try
            {
                // 廃棄物区分CD
                string haikiKbnCd = this.form.GetItiranCellValue(this.form.NyuryokuIkkatsuItiran.CurrentRow.Index,
                    this.form.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index);

                string[] useInfo = new string[] { this.header.KYOTEN_CD.Text, this.header.KYOTEN_NAME.Text, };
                var callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.MANIFEST_IKKATU, "1", haikiKbnCd, "");
                var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader(haikiKbnCd, useInfo);


                var businessForm = new BusinessBaseForm(callForm, callHeader);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    businessForm.ShowDialog();
                }

                string systemId = callForm.ParamOut_SysID;

                if (!string.IsNullOrEmpty(systemId))
                {
                    this.PatternYobidasi(systemId);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CallPattern", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }
        // 20140609 syunrei EV004722_拠点について end

        /// <summary>
        /// 業者マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkGyousha(out bool catchErr)
        {
            bool result = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();
                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                string date = ((BusinessBaseForm)this.form.Parent).sysDate.ToString();
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value != null)
                {
                    date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                }

                sql.Append(" SELECT * FROM M_GYOUSHA ");
                sql.AppendFormat(" WHERE ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', ", date);
                sql.Append(" 111), 120) ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) OR (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" OR (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 ");
                sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = 'True'");
                sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = 'true'");
                sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_CD].Value.ToString().PadLeft(6, '0') + "'");

                this.searchResult = this.GYOUSHADao.GetDataForEntity(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyousha(", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 現場マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkGenba(out bool catchErr)
        {
            bool result = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();
                int rowIndex = this.form.NyuryokuIkkatsuItiran.CurrentRow.Index;
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                this.searchResult = new DataTable();

                this.searchCondition = new DTOClass();

                string date = ((BusinessBaseForm)this.form.Parent).sysDate.ToString();
                if (this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value != null)
                {
                    date = this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.KOUFU_DATE].Value.ToString();
                }


                sql.Append(" SELECT M_GENBA.* ");
                sql.Append(" FROM M_GENBA LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.AppendFormat(" AND ((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', ", date);
                sql.Append(" 111), 120) ");
                sql.AppendFormat(" and CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) or (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" and M_GYOUSHA.TEKIYOU_END IS NULL) ");
                sql.Append(" or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" and CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GYOUSHA.TEKIYOU_END) ");
                sql.Append(" or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" and M_GYOUSHA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 ");
                sql.AppendFormat(" WHERE ((M_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '{0}', ", date);
                sql.Append(" 111), 120) ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GENBA.TEKIYOU_END) OR (M_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, ");
                sql.AppendFormat(" CONVERT(nvarchar, '{0}', 111), 120) ", date);
                sql.Append(" AND M_GENBA.TEKIYOU_END IS NULL) ");
                sql.Append(" OR (M_GENBA.TEKIYOU_BEGIN IS NULL ");
                sql.AppendFormat(" AND CONVERT(DATETIME, CONVERT(nvarchar, '{0}', 111), 120) <= ", date);
                sql.Append(" M_GENBA.TEKIYOU_END) ");
                sql.Append(" OR (M_GENBA.TEKIYOU_BEGIN IS NULL ");
                sql.Append(" AND M_GENBA.TEKIYOU_END IS NULL)) ");
                sql.Append(" AND M_GENBA.DELETE_FLG = 0 ");
                sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = 'True'");
                sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = 'true'");
                sql.Append(" AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = 'true'");
                sql.Append(" AND M_GENBA.GYOUSHA_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_CD].Value.ToString().PadLeft(6, '0') + "'");

                sql.Append(" AND M_GENBA.GENBA_CD = '"
                    + this.form.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_CD].Value.ToString().PadLeft(6, '0') + "'");

                this.searchResult = this.GENBADao.GetDataForEntity(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

    }
}
