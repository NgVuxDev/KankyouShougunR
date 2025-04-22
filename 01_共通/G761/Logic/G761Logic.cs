using System;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.DenpyouRireki.APP;
using Shougun.Core.Common.DenpyouRireki.DAO;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;
using System.Drawing;
using GrapeCity.Win.MultiRow;
using System.Linq;

namespace Shougun.Core.Common.DenpyouRireki.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class G761Logic : IBuisinessLogic
    {
        #region 定数

        //ボタン定義ファイル
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.DenpyouRireki.Setting.ButtonSetting.xml";

        #endregion

        #region 変数

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 受入DAO
        /// </summary>
        private IT_UkeireDao UkeireDao;

        /// <summary>
        /// 受入明細DAO
        /// </summary>
        private IT_UkeireDetailDao UkeireDetailDao;

        /// <summary>
        /// 出荷DAO
        /// </summary>
        private IT_ShukkaDao ShukkaDao;

        /// <summary>
        /// 出荷明細DAO
        /// </summary>
        private IT_ShukkaDetailDao ShukkaDetailDao;

        /// <summary>
        /// 売上/支払DAO
        /// </summary>
        public IT_UrShDao UrShDao;

        /// <summary>
        /// 売上/支払明細DAO
        /// </summary>
        private IT_UrShDetailDao UrShDetailDao;

        /// <summary>
        /// 請求明細DAO
        /// </summary>
        private IT_SeikyuuDetailDao SeikyuuDetailDao;

        /// <summary>
        /// 清算明細DAO
        /// </summary>
        private IT_SeisanDetailDao SeisanDetailDao;

        /// <summary>
        /// 取引先DAO
        /// </summary>
        private IM_TorihikisakiDao TorihikisakiDao;

        /// <summary>
        /// 業者DAO
        /// </summary>
        private IM_GyoushaDao GyoushaInfoDao;

        /// <summary>
        /// 現場DAO
        /// </summary>
        private IM_GenbaDao GenbaInfoDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 都道府県Dao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;

        /// <summary>
        /// Form
        /// </summary>
        private G761Form form;

        /// <summary>
        /// Header
        /// </summary>
        public G761HeaderForm header;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorisaki;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        private IM_SHARYOUDao sharyouDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 受入検索結果
        /// </summary>
        internal DataTable UkeireSearchResult { get; set; }

        /// <summary>
        /// 受入明細検索結果
        /// </summary>
        internal DataTable UkeireDetailSearchResult { get; set; }

        /// <summary>
        /// 出荷検索結果
        /// </summary>
        internal DataTable ShukkaSearchResult { get; set; }

        /// <summary>
        /// 出荷明細検索結果
        /// </summary>
        internal DataTable ShukkaDetailSearchResult { get; set; }

        /// <summary>
        /// 売上/支払検索結果
        /// </summary>
        internal DataTable UrShSearchResult { get; set; }

        /// <summary>
        /// 売上/支払明細検索結果
        /// </summary>
        internal DataTable UrShDetailSearchResult { get; set; }

        /// <summary>
        /// 取引先検索結果
        /// </summary>
        internal M_TORIHIKISAKI TorihikisakiSearchResult { get; set; }

        /// <summary>
        ///業者検索結果
        /// </summary>
        internal DataTable GyoushaSearchResult { get; set; }

        internal DataTable UpnGyoushaSearchResult { get; set; }

        /// <summary>
        /// 現場マスタ検索結果
        /// </summary>
        internal DataTable GenbaSearchResult { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        internal string TorihikisakiCD { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        internal string GyoushaCD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        internal string GenbaCD { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        internal string KyotenCD { get; set; }

        /// <summary>
        /// From date
        /// </summary>
        internal string FromDate { get; set; }

        /// <summary>
        /// To date
        /// </summary>
        internal string ToDate { get; set; }

        /// <summary>
        /// 現場検索条件
        /// </summary>
        internal M_GENBA GenbaSearchString { get; set; }

        internal string UpnGyoushaCD { get; set; }
        internal string SharyouCD { get; set; }
        internal string SharyouName { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G761Logic(G761Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            //DAOのインスタンス
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TorihikisakiDao>();
            this.GyoushaInfoDao = DaoInitUtility.GetComponent<IM_GyoushaDao>();
            this.GenbaInfoDao = DaoInitUtility.GetComponent<IM_GenbaDao>();
            this.UrShDao = DaoInitUtility.GetComponent<IT_UrShDao>();
            this.UrShDetailDao = DaoInitUtility.GetComponent<IT_UrShDetailDao>();
            this.SeikyuuDetailDao = DaoInitUtility.GetComponent<IT_SeikyuuDetailDao>();
            this.SeisanDetailDao = DaoInitUtility.GetComponent<IT_SeisanDetailDao>();
            this.UkeireDao = DaoInitUtility.GetComponent<IT_UkeireDao>();
            this.UkeireDetailDao = DaoInitUtility.GetComponent<IT_UkeireDetailDao>();
            this.ShukkaDao = DaoInitUtility.GetComponent<IT_ShukkaDao>();
            this.ShukkaDetailDao = DaoInitUtility.GetComponent<IT_ShukkaDetailDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoTorisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得
            this.GetSysInfoInit();

            //Header Init
            this.HeaderInit();

            // 売上／支払明細のフォーマット設定
            this.form.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_SHURYOU"].DefaultCellStyle.Format = "#,##0.###";
            this.form.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_TANKA"].DefaultCellStyle.Format = "#,##0.###";

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            LogUtility.DebugMethodEnd();
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
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //条件ｸﾘｱボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.Conditions_Clear_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.UketsukeSearch);

            //複写ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.Copy_Click);

            parentForm.bt_func10.Click += new EventHandler(this.form.DetailCopy_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //日付Toイベント生成
            this.header.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);

            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();

            this.form.Ukeire_Denpyou.CellDoubleClick += new DataGridViewCellEventHandler(Denpyou_CellDoubleClick);
            this.form.Shukka_Denpyou.CellDoubleClick += new DataGridViewCellEventHandler(Denpyou_CellDoubleClick);
            this.form.UriageShiharai_Denpyou.CellDoubleClick += new DataGridViewCellEventHandler(Denpyou_CellDoubleClick);

            this.form.MultiRow_UkeireMeisai.CellDoubleClick += new EventHandler<CellEventArgs>(Meisai_CellDoubleClick);
            this.form.MultiRow_ShukkaMeisai.CellDoubleClick += new EventHandler<CellEventArgs>(Meisai_CellDoubleClick);
            this.form.UriageShiharai_Meisai.CellDoubleClick += new DataGridViewCellEventHandler(UriageShiharai_Meisai_CellDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        void UriageShiharai_Meisai_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex < 0)
            {
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string copyNo = "";
            string detailSystemId = "";

            if ("G054" == this.form.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.form.UriageShiharai_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.form.UriageShiharai_Denpyou.CurrentRow.Cells["URIAGESIHARAI_NO"].Value.ToString();//売上/支払番号

                detailSystemId = this.form.UriageShiharai_Meisai.Rows[e.RowIndex].Cells["URIAGESIHARAI_MEISAI_DETAIL_SYSTEM_ID"].Value.ToString();
            }

            this.form.DenpyouRirekiDTO.DenpyouNumber = copyNo;
            this.form.DenpyouRirekiDTO.DetailSystemId = detailSystemId;
            this.form.DenpyouRirekiDTO.CopyMode = "2";

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        void Meisai_CellDoubleClick(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex < 0)
            {
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string copyNo = "";
            string detailSystemId = "";

            if ("G721" == this.form.DenpyouRirekiDTO.FormId || "G051" == this.form.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.form.Ukeire_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.form.Ukeire_Denpyou.CurrentRow.Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();//受入番号


                detailSystemId = this.form.MultiRow_UkeireMeisai.Rows[e.RowIndex].Cells["UKEIRE_DETAIL_SYSTEM_ID"].Value.ToString();
            }
            else if ("G722" == this.form.DenpyouRirekiDTO.FormId || "G053" == this.form.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.form.Shukka_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.form.Shukka_Denpyou.CurrentRow.Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();//出荷番号


                detailSystemId = this.form.MultiRow_ShukkaMeisai.Rows[e.RowIndex].Cells["SHUKKA_DETAIL_SYSTEM_ID"].Value.ToString();
            }

            this.form.DenpyouRirekiDTO.DenpyouNumber = copyNo;
            this.form.DenpyouRirekiDTO.DetailSystemId = detailSystemId;
            this.form.DenpyouRirekiDTO.CopyMode = "2";

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        public void Denpyou_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex < 0)
            {
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string copyNo = "";

            if ("G721" == this.form.DenpyouRirekiDTO.FormId || "G051" == this.form.DenpyouRirekiDTO.FormId)
            {
                copyNo = this.form.Ukeire_Denpyou.Rows[e.RowIndex].Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();//受入番号

            }
            else if ("G722" == this.form.DenpyouRirekiDTO.FormId || "G053" == this.form.DenpyouRirekiDTO.FormId)
            {
                copyNo = this.form.Shukka_Denpyou.Rows[e.RowIndex].Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();//出荷番号

            }
            else if ("G054" == this.form.DenpyouRirekiDTO.FormId)
            {
                copyNo = this.form.UriageShiharai_Denpyou.Rows[e.RowIndex].Cells["URIAGESIHARAI_NO"].Value.ToString();//売上/支払番号

            }

            this.form.DenpyouRirekiDTO.DenpyouNumber = copyNo;
            this.form.DenpyouRirekiDTO.DetailSystemId = "";
            this.form.DenpyouRirekiDTO.CopyMode = "1";

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// 各タブーの一覧、明細を初期化
        /// </summary>
        internal void MeisaiInit()
        {
            LogUtility.DebugMethodStart();
            
            //受入
            if (null != this.UkeireSearchResult)
            {
                this.UkeireSearchResult.Clear();
            }
            //受入明細
            if (null != this.UkeireDetailSearchResult)
            {
                this.UkeireDetailSearchResult.Clear();
            }
            //出荷
            if (null != this.ShukkaSearchResult)
            {
                this.ShukkaSearchResult.Clear();
            }
            //出荷明細
            if (null != this.ShukkaDetailSearchResult)
            {
                this.ShukkaDetailSearchResult.Clear();
            }
            //売上支払
            if (null != this.UrShSearchResult)
            {
                this.UrShSearchResult.Clear();
            }
            //売上支払明細
            if (null != this.UrShDetailSearchResult)
            {
                this.UrShDetailSearchResult.Clear();
            }
            
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件設定

        /// <summary>
        /// 検索条件:現場の設定
        /// </summary>
        public bool SetGenbaSearchString()
        {
            LogUtility.DebugMethodStart();

            M_GENBA GenbaEntity = new M_GENBA();

            //取引先CD
            string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;
            //業者CD
            string gyousha_cd = this.form.GYOUSHA_CD.Text;
            //現場CD
            string genba_cd = this.form.GENBA_CD.Text;

            //現場
            GenbaEntity.TORIHIKISAKI_CD = trihikisaki_cd;
            GenbaEntity.GYOUSHA_CD = gyousha_cd;
            GenbaEntity.GENBA_CD = genba_cd;
            this.GenbaSearchString = GenbaEntity;

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 検索条件:エンティティ毎にの設定
        /// </summary>
        /// <param name="entityName"></param>
        public bool SetSearchString(string entityName)
        {
            LogUtility.DebugMethodStart(entityName);
            //引数のエンティティ名を大文字にする
            string ENTITY_NAME = "";
            if (null != entityName && !"".Equals(entityName) && !DBNull.Value.Equals(entityName))
            {
                ENTITY_NAME = entityName.ToUpper();
            }
            //取引先CD
            string trihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 検索条件マスタ検索

        /// <summary>
        /// 取引先マスタ検索
        /// </summary>
        internal int TorihikisakiSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string torihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;

                //取引先検索結果
                this.TorihikisakiSearchResult = this.TorihikisakiDao.GetTorihikisakiData(torihikisaki_cd);
                //
                if (this.TorihikisakiSearchResult == null)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 業者マスタ検索
        /// </summary>
        internal int GyoushaSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD
                string gyousha_cd = this.form.GYOUSHA_CD.Text;

                //業者検索結果

                this.GyoushaSearchResult = this.GyoushaInfoDao.GetDataBySqlFile2(gyousha_cd);
                //

                if (this.GyoushaSearchResult == null || this.GyoushaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 現場マスタ検索
        /// </summary>
        internal int GenbaSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //現場検索の条件設定
                this.SetGenbaSearchString();

                //現場検索結果
                this.GenbaSearchResult = this.GenbaInfoDao.GetDataBySqlFile2(this.GenbaSearchString);
                //
                if (this.GenbaSearchResult == null || this.GenbaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region 検索：伝票タブ

        #region [伝票タブ]受入検索

        /// <summary>
        /// 受入検索
        /// </summary>
        internal int UkeireSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CDz
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;

            string upn_gyousha_cd = this.UpnGyoushaCD;

            string sharyou_cd = this.SharyouCD;

            string sharyou_name = this.SharyouName;

            try
            {
                //受入検索結果
                this.UkeireSearchResult = this.UkeireDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date, upn_gyousha_cd, sharyou_cd, sharyou_name);
                //
                if (this.UkeireSearchResult == null || this.UkeireSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.UkeireSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.UkeireSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.UkeireSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.UkeireSearchResult.Columns.Count; i++)
                        {
                            if (this.UkeireSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.UkeireSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.UkeireSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.UkeireSearchResult.Columns[i].ReadOnly = false;
                                this.UkeireSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(受入)
                        int denpyouShuruiCd = 1;

                        // 状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UkeireSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.UkeireSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }

                        // 状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UkeireSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.UkeireSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UkeireDetailSearch(long systemId, int seq)
        {
            //LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //受入明細検索結果
                this.UkeireDetailSearchResult = this.UkeireDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UkeireDetailSearchResult == null || this.UkeireDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]出荷検索

        /// <summary>
        /// 出荷検索
        /// </summary>
        internal int ShukkaSearch()
        {
            LogUtility.DebugMethodStart();
            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;

            string upn_gyousha_cd = this.UpnGyoushaCD;

            string sharyou_cd = this.SharyouCD;

            string sharyou_name = this.SharyouName;

            try
            {
                //出荷検索結果
                this.ShukkaSearchResult = this.ShukkaDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date, upn_gyousha_cd, sharyou_cd, sharyou_name);
                //
                if (this.ShukkaSearchResult == null || this.ShukkaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    // 請求明細存在チェック
                    for (int index = 0; index < this.ShukkaSearchResult.Rows.Count; index++)
                    {
                        // システムID
                        long denpyouSystemId = this.ShukkaSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        // 伝票番号
                        int denpyouSeq = this.ShukkaSearchResult.Rows[index].Field<int>("SEQ");
                        // 状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.ShukkaSearchResult.Columns.Count; i++)
                        {
                            if (this.ShukkaSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.ShukkaSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.ShukkaSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.ShukkaSearchResult.Columns[i].ReadOnly = false;
                                this.ShukkaSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        // 伝票種類CD(出荷)
                        int denpyouShuruiCd = 2;

                        // 状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkaSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.ShukkaSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }

                        // 状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.ShukkaSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.ShukkaSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int ShukkaDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //出荷明細検索結果
                this.ShukkaDetailSearchResult = this.ShukkaDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.ShukkaDetailSearchResult == null || this.ShukkaDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        #endregion

        #region [伝票タブ]売上/支払検索

        /// <summary>
        /// 売上/支払検索
        /// </summary>
        internal int UrShSearch()
        {
            LogUtility.DebugMethodStart();

            //取引先CD
            string torihikisaki_cd = this.TorihikisakiCD;
            //業者CD
            string gyousha_cd = this.GyoushaCD;
            //現場CD
            string genba_cd = this.GenbaCD;
            //拠点CD
            string kyoten_cd = this.KyotenCD;
            //From date
            string from_date = this.FromDate;
            //To date
            string to_date = this.ToDate;

            string upn_gyousha_cd = this.UpnGyoushaCD;

            string sharyou_cd = this.SharyouCD;

            string sharyou_name = this.SharyouName;

            try
            {
                //売上/支払検索結果
                this.UrShSearchResult = this.UrShDao.GetDataBySqlFile(torihikisaki_cd, gyousha_cd, genba_cd, kyoten_cd, from_date, to_date, upn_gyousha_cd, sharyou_cd, sharyou_name);
                //
                if (this.UrShSearchResult == null || this.UrShSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    //請求明細存在チェック
                    for (int index = 0; index < this.UrShSearchResult.Rows.Count; index++)
                    {
                        //システムID
                        long denpyouSystemId = this.UrShSearchResult.Rows[index].Field<long>("SYSTEM_ID");
                        //伝票枝番
                        int denpyouSeq = this.UrShSearchResult.Rows[index].Field<int>("SEQ");
                        //状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.UrShSearchResult.Columns.Count; i++)
                        {
                            if (this.UrShSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEIKYUU"
                                || this.UrShSearchResult.Columns[i].ColumnName == "JYOUKYOU_SEISAN"
                                || this.UrShSearchResult.Columns[i].ColumnName == "JYOUKYOU_SALES_ZAIKO")
                            {
                                this.UrShSearchResult.Columns[i].ReadOnly = false;
                                this.UrShSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        //伝票種類CD(売上支払)
                        int denpyouShuruiCd = 3;

                        //状況JYOUKYOU_SEIKYUUの値
                        if (this.SeikyuuDetailSearch2(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UrShSearchResult.Rows[index]["JYOUKYOU_SEIKYUU"] = "未締";
                        }
                        else
                        {
                            this.UrShSearchResult.Rows[index].SetField("JYOUKYOU_SEIKYUU", "締済");
                        }
                        //状況JYOUKYOU_SEISANの値
                        if (this.SeisanDetailSearch(denpyouSystemId, denpyouSeq, denpyouShuruiCd) == 0)
                        {
                            this.UrShSearchResult.Rows[index]["JYOUKYOU_SEISAN"] = "未締";
                        }
                        else
                        {
                            this.UrShSearchResult.Rows[index].SetField("JYOUKYOU_SEISAN", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払明細検索
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        internal int UrShDetailSearch(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //売上/支払明細検索結果
                this.UrShDetailSearchResult = this.UrShDetailDao.GetDataBySqlFile(systemId, seq);
                //
                if (this.UrShDetailSearchResult == null || this.UrShDetailSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                else
                {
                    //請求明細存在チェック
                    for (int index = 0; index < this.UrShDetailSearchResult.Rows.Count; index++)
                    {
                        //システムID
                        long denpyouSystemId = systemId;
                        //伝票枝番
                        int denpyouSeq = seq;
                        //明細システムID
                        long detailSystemId = UrShDetailSearchResult.Rows[index].Field<long>("DETAIL_SYSTEM_ID");
                        //伝票番号
                        long denpyouNumber = 0;
                        if (!this.UrShDetailSearchResult.Rows[index].IsNull("UR_SH_NUMBER"))
                        {
                            denpyouNumber = UrShDetailSearchResult.Rows[index].Field<long>("UR_SH_NUMBER");
                        }
                        //状況JYOUKYOUの属性変更
                        for (int i = 0; i < this.UrShDetailSearchResult.Columns.Count; i++)
                        {
                            if (this.UrShDetailSearchResult.Columns[i].ColumnName == "JYOUKYOU")
                            {
                                this.UrShDetailSearchResult.Columns[i].ReadOnly = false;
                                this.UrShDetailSearchResult.Columns[i].MaxLength = 10;
                            }
                        }
                        //伝票種類CD(売上支払)
                        int denpyouShuruiCd = 3;

                        //状況JYOUKYOUの値
                        if (this.SeikyuuDetailSearch(denpyouSystemId, denpyouSeq, detailSystemId, denpyouNumber, denpyouShuruiCd) == 0)
                        {
                            this.UrShDetailSearchResult.Rows[index]["JYOUKYOU"] = "未締";
                        }
                        else
                        {
                            this.UrShDetailSearchResult.Rows[index].SetField("JYOUKYOU", "締済");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        /// <summary>
        /// 請求明細存在チェック
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="urShNumber">伝票番号</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        internal int SeikyuuDetailSearch(long systemId, int seq, long detailSystemId, long denpyouNumber, int denpyouShuruiCd)
        {
            LogUtility.DebugMethodStart(systemId, seq, detailSystemId, denpyouNumber, denpyouShuruiCd);

            //請求明細検索結果
            int cnt = this.SeikyuuDetailDao.GetDataBySqlFile(systemId, seq, detailSystemId, denpyouNumber, denpyouShuruiCd);
            //
            if (cnt == 0)
            {
                LogUtility.DebugMethodEnd(0);
                return 0;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 請求明細存在チェック
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        internal int SeikyuuDetailSearch2(long systemId, int seq, int denpyouShuruiCd)
        {
            LogUtility.DebugMethodStart(systemId, seq, denpyouShuruiCd);

            //請求明細検索結果
            int cnt = this.SeikyuuDetailDao.GetDataBySqlFile2(systemId, seq, denpyouShuruiCd);
            //
            if (cnt == 0)
            {
                LogUtility.DebugMethodEnd(0);
                return 0;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 清算明細存在チェック
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        internal int SeisanDetailSearch(long systemId, int seq, int denpyouShuruiCd)
        {
            LogUtility.DebugMethodStart(systemId, seq, denpyouShuruiCd);

            //清算明細検索結果
            int cnt = this.SeisanDetailDao.GetDataBySqlFile(systemId, seq, denpyouShuruiCd);
            //
            if (cnt == 0)
            {
                LogUtility.DebugMethodEnd(0);
                return 0;
            }

            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        #endregion

        #endregion

        #region 検索条件の項目値セット

        /// <summary>
        /// 条件の項目値セット
        /// </summary>
        internal void TorihikisakiSet()
        {
            LogUtility.DebugMethodStart();
            //取引先
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_CD)
            {
                this.form.TORIHIKISAKI_CD.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_CD;
            }
            //取引先略名
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_NAME_RYAKU)
            {
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_NAME_RYAKU;
            }
            //住所１
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS1)
            {
                this.form.TORIHIKISAKI_ADDRESS1.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS1;
            }
            //住所２
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS2)
            {
                this.form.TORIHIKISAKI_ADDRESS2.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_ADDRESS2;
            }
            //電話番号
            if (null != this.TorihikisakiSearchResult.TORIHIKISAKI_TEL)
            {
                this.form.TORIHIKISAKI_TEL.Text = this.TorihikisakiSearchResult.TORIHIKISAKI_TEL;
            }
            //都道府県
            if (!this.TorihikisakiSearchResult.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
            {
                M_TODOUFUKEN entity = new M_TODOUFUKEN();
                entity = todoufukenDao.GetDataByCd(this.TorihikisakiSearchResult.TORIHIKISAKI_TODOUFUKEN_CD.Value.ToString());
                if (entity != null)
                {
                    this.form.TORIHIKISAKI_TODOUFUKEN.Text = entity.TODOUFUKEN_NAME_RYAKU;
                }
            }
            else
            {
                this.form.TORIHIKISAKI_TODOUFUKEN.Text = string.Empty;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の業者項目値セット
        /// </summary>
        internal void GyoushaSet()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;

                //業者CD
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                {
                    this.form.GYOUSHA_CD.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_CD");
                }

                //業者名
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_NAME_RYAKU");
                }
                //業者住所1
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS1"))
                {
                    this.form.GYOUSHA_ADDRESS1.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS1");
                }
                //業者住所2
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS2"))
                {
                    this.form.GYOUSHA_ADDRESS2.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS2");
                }
                //業者電話
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_TEL"))
                {
                    this.form.GYOUSHA_TEL.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_TEL");
                }
                //業者都道府県
                if (!this.GyoushaSearchResult.Rows[0].IsNull("GYOUSHA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = this.GyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;
                }

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) STR
                //取引先
                //if (null == trihikisaki_cd || "".Equals(trihikisaki_cd) || DBNull.Value.Equals(trihikisaki_cd))
                //{
                //    //取引先CD
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_CD"))
                //    {
                //        this.form.TORIHIKISAKI_CD.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD");
                //    }

                //    //取引先名
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME_RYAKU"))
                //    {
                //        this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME_RYAKU");
                //    }
                //    //取引先住所1
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS1"))
                //    {
                //        this.form.TORIHIKISAKI_ADDRESS1.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS1");
                //    }
                //    //取引先住所2
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS2"))
                //    {
                //        this.form.TORIHIKISAKI_ADDRESS2.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS2");
                //    }
                //    //取引先電話
                //    if (!this.GyoushaSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEL"))
                //    {
                //        this.form.TORIHIKISAKI_TEL.Text = this.GyoushaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_TEL");
                //    }
                //}
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) END
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の現場,業者、取引先項目値セット
        /// </summary>
        internal void GenbaSet()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string trihikisaki_cd = this.form.TORIHIKISAKI_CD.Text;
                //業者CD
                string gyousha_cd = this.form.GYOUSHA_CD.Text;

                //現場名
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_NAME_RYAKU"))
                {
                    this.form.GENBA_NAME_RYAKU.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_NAME_RYAKU");
                }
                //現場住所1
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_ADDRESS1"))
                {
                    this.form.GENBA_ADDRESS1.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_ADDRESS1");
                }
                //現場住所2
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_ADDRESS2"))
                {
                    this.form.GENBA_ADDRESS2.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_ADDRESS2");
                }
                //現場電話
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_TEL"))
                {
                    this.form.GENBA_TEL.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_TEL");
                }
                //現場都道府県
                if (!this.GenbaSearchResult.Rows[0].IsNull("GENBA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.GENBA_TODOUFUKEN.Text = this.GenbaSearchResult.Rows[0].Field<string>("GENBA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.GENBA_TODOUFUKEN.Text = string.Empty;
                }
                //業者
                //if (null == gyousha_cd || "".Equals(gyousha_cd) || DBNull.Value.Equals(gyousha_cd))
                //{
                //業者CD
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                {
                    this.form.GYOUSHA_CD.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_CD").ToUpper();
                }

                //業者名
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_NAME_RYAKU");
                }
                //業者住所1
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS1"))
                {
                    this.form.GYOUSHA_ADDRESS1.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS1");
                }
                //業者住所2
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS2"))
                {
                    this.form.GYOUSHA_ADDRESS2.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS2");
                }
                //業者電話
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_TEL"))
                {
                    this.form.GYOUSHA_TEL.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_TEL");
                }
                //業者都道府県
                if (!this.GenbaSearchResult.Rows[0].IsNull("GYOUSHA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = this.GenbaSearchResult.Rows[0].Field<string>("GYOUSHA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;
                }

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) STR
                //}
                ////取引先
                //if (null == trihikisaki_cd || "".Equals(trihikisaki_cd) || DBNull.Value.Equals(trihikisaki_cd))
                //{
                ////取引先CD
                //if (!string.IsNullOrEmpty(Convert.ToString(this.GenbaSearchResult.Rows[0]["TORIHIKISAKI_CD"])))
                //{
                //    this.form.TORIHIKISAKI_CD.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_CD").ToUpper();
                //}

                ////取引先名
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_NAME_RYAKU"))
                //{
                //    this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_NAME_RYAKU");
                //}
                ////取引先住所1
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS1"))
                //{
                //    this.form.TORIHIKISAKI_ADDRESS1.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS1");
                //}
                ////取引先住所2
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_ADDRESS2"))
                //{
                //    this.form.TORIHIKISAKI_ADDRESS2.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_ADDRESS2");
                //}
                ////取引先電話
                //if (!this.GenbaSearchResult.Rows[0].IsNull("TORIHIKISAKI_TEL"))
                //{
                //    this.form.TORIHIKISAKI_TEL.Text = this.GenbaSearchResult.Rows[0].Field<string>("TORIHIKISAKI_TEL");
                //}
                //}
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) END
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件の項目値クリア

        /// <summary>
        /// 条件の取引先項目値クリア
        /// </summary>
        internal void TorihikisakiCrear()
        {
            LogUtility.DebugMethodStart();

            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS1.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS2.Text = string.Empty;
            this.form.TORIHIKISAKI_TEL.Text = string.Empty;
            this.form.TORIHIKISAKI_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の業者項目値クリア
        /// </summary>
        internal void GyoushaClear()
        {
            LogUtility.DebugMethodStart();

            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.GYOUSHA_TEL.Text = string.Empty;
            this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件の現場項目値クリア
        /// </summary>
        internal void GenbaClear()
        {
            LogUtility.DebugMethodStart();

            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_ADDRESS1.Text = string.Empty;
            this.form.GENBA_ADDRESS2.Text = string.Empty;
            this.form.GENBA_TEL.Text = string.Empty;
            this.form.GENBA_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 各種メソッド

        /// <summary>
        /// フォーマット
        /// </summary>
        /// <param name="flg">1=金額、2=電話番号、3=郵便番号</param>
        /// <param name="text"></param>
        internal string textFormat(int flg, object text)
        {
            LogUtility.DebugMethodStart(flg, text);
            string strFormat = "";

            if (null != text)
            {
                strFormat = text.ToString().Trim();
                decimal num;
                switch (flg)
                {
                    case 1:
                        //金額
                        if (null != strFormat && !"".Equals(strFormat))
                        {
                            num = decimal.Parse(strFormat);
                            strFormat = string.Format("{0:N0}", num);
                        }
                        break;

                    case 2:
                        //電話番号
                        //  電話番号及びFAX番号にハイフンを自動で入れる処理を廃止しました
                        break;

                    case 3:
                        //郵便番号
                        if (null != strFormat && !"".Equals(strFormat))
                        {
                            int index = strFormat.IndexOf('-');
                            if (index <= 0)
                            {
                                strFormat = strFormat = strFormat.Substring(0, 3) + "-" + strFormat.Substring(3);
                            }
                        }
                        break;
                }
            }
            LogUtility.DebugMethodEnd(strFormat);

            return strFormat;
        }

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

        /// <summary>
        /// 取引先と拠点の関係をチェックします
        /// </summary>
        /// <param name="headerKyotenCd">拠点CD</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckTorihikisakiKyoten(string headerKyotenCd, string torihikisakiCd)
        {
            //取引先が空だったらReturn
            if (string.Empty == torihikisakiCd)
            {
                return true;
            }

            // 拠点をチェック
            if (String.IsNullOrEmpty(headerKyotenCd))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E146");

                return false;
            }

            var torihikisaki = this.TorihikisakiDao.GetTorihikisakiData(torihikisakiCd);
            if (null == torihikisaki)
            {
                return false;
            }

            var kyotenCd = (int)torihikisaki.TORIHIKISAKI_KYOTEN_CD;
            if (99 != kyotenCd && Convert.ToInt16(headerKyotenCd) != kyotenCd)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 検索条件をクリアする
        /// </summary>
        internal void searchConfirionsClear()
        {
            // 検索条件をクリアする
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS1.Text = string.Empty;
            this.form.TORIHIKISAKI_ADDRESS2.Text = string.Empty;
            this.form.TORIHIKISAKI_TEL.Text = string.Empty;
            this.form.TORIHIKISAKI_TODOUFUKEN.Text = string.Empty;

            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.GYOUSHA_TEL.Text = string.Empty;
            this.form.GYOUSHA_TODOUFUKEN.Text = string.Empty;

            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_ADDRESS1.Text = string.Empty;
            this.form.GENBA_ADDRESS2.Text = string.Empty;
            this.form.GENBA_TEL.Text = string.Empty;
            this.form.GENBA_TODOUFUKEN.Text = string.Empty;

            this.form.UPN_GYOUSHA_CD.Text = string.Empty;
            this.form.UPN_GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.UPN_GYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.UPN_GYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.UPN_GYOUSHA_TEL.Text = string.Empty;
            this.form.UPN_GYOUSHA_TODOUFUKEN.Text = string.Empty;

            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            this.header.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.header.DATE_TO.BackColor = Constans.NOMAL_COLOR;

            DateTime from = Convert.ToDateTime(this.header.DATE_FROM.Text);
            DateTime to = Convert.ToDateTime(this.header.DATE_TO.Text);
            //if from date > to date then return error
            if (from.CompareTo(to) > 0)
            {
                this.header.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.header.DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { this.header.label3.Text.Replace("※", "") + "From", this.header.label3.Text.Replace("※", "") + "To" };
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.header.DATE_FROM.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Header init
        /// </summary>
        public void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm form = (BusinessBaseForm)this.form.Parent;
            this.header = (G761HeaderForm)form.headerForm;

            DateTime now = DateUtility.GetCurrentDateTime();
            this.header.DATE_FROM.Value = now.AddMonths(-6);
            this.header.DATE_TO.Value = now;

            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            this.header.KYOTEN_CD.Text = configProfile.ItemSetVal1.PadLeft(2, '0');

            if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text))
            {
                M_KYOTEN data = new M_KYOTEN();
                data = kyotenDao.GetDataByCd(Convert.ToInt16(this.header.KYOTEN_CD.Text).ToString());
                if (data != null)
                {
                    this.header.KYOTEN_NAME_RYAKU.Text = data.KYOTEN_NAME_RYAKU;
                }
            }
            LogUtility.DebugMethodEnd();
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
            foreach (Control control in this.header.allControl)
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
        /// DATE_TO MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.header.DATE_FROM;
            var ToTextBox = this.header.DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        internal bool ChechSharyouCd()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
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
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.form.SHARYOU_CD.Text);
                
                if (sharyou == null)
                {
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                // 車輌名設定
                this.form.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                this.form.UPN_GYOUSHA_CD.Text = sharyou.GYOUSHA_CD;
                // 運搬業者が入力されてない場合
                if (!string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD.Text))
                {
                    this.form.maeUpnGyoushaCd = this.form.UPN_GYOUSHA_CD.Text;
                    this.form.UpnGyoushaFORGyoushaCD_Select();
                }

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechSharyouCd", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
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
        public M_SHARYOU GetSharyou(string sharyouCd)
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
                if (!string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.form.UPN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                
                keyEntity.ISNOT_NEED_DELETE_FLG = true;

                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidData(keyEntity);
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

        internal void UpnGyoushaClear()
        {
            LogUtility.DebugMethodStart();

            this.form.UPN_GYOUSHA_CD.Text = string.Empty;
            this.form.UPN_GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.UPN_GYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.UPN_GYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.UPN_GYOUSHA_TEL.Text = string.Empty;
            this.form.UPN_GYOUSHA_TODOUFUKEN.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        public void SharyouClear()
        {
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
        }

        internal int UpnGyoushaSearch()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD
                string gyousha_cd = this.form.UPN_GYOUSHA_CD.Text;

                this.UpnGyoushaSearchResult = this.GyoushaInfoDao.GetDataBySqlFileUpn(gyousha_cd);
                
                if (this.UpnGyoushaSearchResult == null || this.UpnGyoushaSearchResult.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd(1);
            return 1;
        }

        internal void UpnGyoushaSet()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD
                if (!this.UpnGyoushaSearchResult.Rows[0].IsNull("GYOUSHA_CD"))
                {
                    this.form.UPN_GYOUSHA_CD.Text = this.UpnGyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_CD");
                }

                //業者名
                if (!this.UpnGyoushaSearchResult.Rows[0].IsNull("GYOUSHA_NAME_RYAKU"))
                {
                    this.form.UPN_GYOUSHA_NAME_RYAKU.Text = this.UpnGyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_NAME_RYAKU");
                }
                //業者住所1
                if (!this.UpnGyoushaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS1"))
                {
                    this.form.UPN_GYOUSHA_ADDRESS1.Text = this.UpnGyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS1");
                }
                //業者住所2
                if (!this.UpnGyoushaSearchResult.Rows[0].IsNull("GYOUSHA_ADDRESS2"))
                {
                    this.form.UPN_GYOUSHA_ADDRESS2.Text = this.UpnGyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_ADDRESS2");
                }
                //業者電話
                if (!this.UpnGyoushaSearchResult.Rows[0].IsNull("GYOUSHA_TEL"))
                {
                    this.form.UPN_GYOUSHA_TEL.Text = this.UpnGyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_TEL");
                }
                //業者都道府県
                if (!this.UpnGyoushaSearchResult.Rows[0].IsNull("GYOUSHA_TODOUFUKEN_NAME_RYAKU"))
                {
                    this.form.UPN_GYOUSHA_TODOUFUKEN.Text = this.UpnGyoushaSearchResult.Rows[0].Field<string>("GYOUSHA_TODOUFUKEN_NAME_RYAKU");
                }
                else
                {
                    this.form.UPN_GYOUSHA_TODOUFUKEN.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// adjustColumnSize
        /// </summary>
        /// <param name="dgv"></param>
        internal void adjustColumnSize(CustomDataGridView dgv)
        {
            if (dgv == null || dgv.ColumnCount == 0)
            {
                return;
            }

            if (dgv.RowCount == 0 || dgv.DataSource == null || ((DataTable)dgv.DataSource).Rows.Count == 0)
            {
                return;
            }
            
            // TIME_STAMP列はバイナリなのでDataGridViewImageColumnとなり、AutoResizeColumnsメソッドでエラーとなってしまう
            // そのため、列名が"TIME_STAMP"でDataGridViewImageColumn以外をリサイズ対象とする
            // また、入力項目についてはリサイズを行わない(入力項目は初期状態ブランクの場合、幅が小さくなってしまため)
            // ※画面によってはCheckBoxも影響を受けてしまうため、返却日入力用にDgvCustomDataTimeColumnだけリサイズしないようにしている。
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                dgv.AutoResizeColumn(c.Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            }
            
        }

        internal void ExecuteAlignmentForDetailUkeire()
        {
            LogUtility.DebugMethodStart();

            bool zaikoHinmeiVisable = true;
            bool maniNumberVisible = true;
            bool chouseiVisible = true;
            bool youkiVisible = true;

            this.form.MultiRow_UkeireMeisai.SuspendLayout();
            var newTemplate = this.form.MultiRow_UkeireMeisai.Template;

            // 初期化
            // 割振(Kg)
            var warifuriHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader1"];
            var warifuriCell = newTemplate.Row.Cells["UKEIRE_MEISAI_WARIFURI_JYUURYOU"];
            // 調整(Kg)
            var chouseiHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader2"];
            var chouseiJCell = newTemplate.Row.Cells["UKEIRE_MEISAI_CHOUSEI_JYUURYOU"];
            //var chouseiPCell = newTemplate.Row.Cells["CHOUSEI_PERCENT"];
            // 容器引
            var youkiHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader3"];
            //var youkiCCell = newTemplate.Row.Cells["YOUKI_CD"];
            var youkiNCell = newTemplate.Row.Cells["UKEIRE_MEISAI_YOUKI_NAME"];
            var youkiSCell = newTemplate.Row.Cells["UKEIRE_MEISAI_YOUKI_SUURYOU"];
            //var youkiJCell = newTemplate.Row.Cells["YOUKI_JYUURYOU"];
            // 品名
            var hinmeiHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader5"];
            var hinmeiCell = newTemplate.Row.Cells["UKEIRE_MEISAI_HINMEI_CD"];
            var denpyouCell = newTemplate.Row.Cells["UKEIRE_MEISAI_DENPYOU_KBN_NAME"];
            var hinmeiNameCell = newTemplate.Row.Cells["UKEIRE_MEISAI_HINMEI_NAME"];
            // 数量
            var suuryouHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader9"];
            var suuryouCell = newTemplate.Row.Cells["UKEIRE_MEISAI_SUURYOU"];
            // 単位
            var unitHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader10"];
            var unitNameCell = newTemplate.Row.Cells["UKEIRE_MEISAI_UNIT_NAME"];
            //var unitCell = newTemplate.Row.Cells["UNIT_CD"];
            // 単価
            var tankaHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader11"];
            var tankaCell = newTemplate.Row.Cells["UKEIRE_MEISAI_TANKA"];
            // 正味/金額
            var netjyuuryouHedader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader12"];
            var netjyuuryouCell = newTemplate.Row.Cells["UKEIRE_MEISAI_NET_JYUURYOU"];
            var kingakuCell = newTemplate.Row.Cells["UKEIRE_MEISAI_KINGAKU"];
            // 荷姿数量
            var nisugataSuuryouHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader16"];
            var nisugataSuuryouCell = newTemplate.Row.Cells["NISUGATA_SUURYOU"];
            //var nisugataCdCell = newTemplate.Row.Cells["NISUGATA_UNIT_CD"];
            var nisugataNameCell = newTemplate.Row.Cells["NISUGATA_NAME_RYAKU"];
            // 在庫品名
            var zaikoHinmeiNameHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader21"];
            var zaikoHinmeiNameCell = newTemplate.Row.Cells["ZAIKO_HINMEI_RYAKU"];
            var zaikoHinmeiCdCell = newTemplate.Row.Cells["gcCustomTextBoxCell1"];
            // 明細備考/マニフェスト番号
            var hinmeiBikouHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader14"];
            var hinmeiBikouCell = newTemplate.Row.Cells["UKEIRE_MEISAI_BIKOU"];
            var maniNumberCell = newTemplate.Row.Cells["UKEIRE_MEISAI_MANIFEST_ID"];

            // 調整使用区分
            if (this.sysInfoEntity.KEIRYOU_CHOUSEI_USE_KBN != 1)
            {
                chouseiVisible = false;
            }
            // 容器使用区分
            if (this.sysInfoEntity.KEIRYOU_YOUKI_USE_KBN != 1)
            {
                youkiVisible = false;
            }
            // 在庫使用区分
            if (this.sysInfoEntity.ZAIKO_KANRI != 1)
            {
                zaikoHinmeiVisable = false;
            }
            // マニ登録形態区分
            if (this.sysInfoEntity.SYS_MANI_KEITAI_KBN == 1)
            {
                maniNumberVisible = false;
            }

            int x = warifuriHedader.Location.X + warifuriHedader.Width;

            if (!chouseiVisible)
            {
                chouseiHedader.Location = new Point(0, 1);
                chouseiJCell.Location = new Point(0, 0);
                //chouseiPCell.Location = new Point(0, 20);
                chouseiHedader.Visible = false;
                chouseiJCell.Visible = false;
                //chouseiPCell.Visible = false;
            }
            else
            {
                chouseiHedader.Location = new Point(x, 1);
                chouseiJCell.Location = new Point(x, 0);
                //chouseiPCell.Location = new Point(x, 20);
                x = x + chouseiHedader.Width;
            }

            if (!youkiVisible)
            {
                youkiHedader.Location = new Point(0, 1);
                //youkiCCell.Location = new Point(0, 0);
                youkiNCell.Location = new Point(0, 0);
                youkiHedader.Visible = false;
                //youkiCCell.Visible = false;
                youkiNCell.Visible = false;
                youkiSCell.Location = new Point(0, 20);
                //youkiJCell.Location = new Point(0, 20);
                youkiSCell.Visible = false;
                //youkiJCell.Visible = false;
            }
            else
            {
                youkiHedader.Location = new Point(x, 1);
                //youkiCCell.Location = new Point(x, 0);
                youkiNCell.Location = new Point(x, 0);
                youkiSCell.Location = new Point(x, 20);
                //youkiJCell.Location = new Point(x + youkiSCell.Width, 20);
                x = x + youkiHedader.Width;
            }

            hinmeiHedader.Location = new Point(x, 1);
            hinmeiCell.Location = new Point(x, 0);
            denpyouCell.Location = new Point(x, 20);

            x = x + hinmeiHedader.Width;

            suuryouHedader.Location = new Point(x, 1);
            unitHedader.Location = new Point(x + suuryouHedader.Width, 1);
            tankaHedader.Location = new Point(x + suuryouHedader.Width + unitHedader.Width, 1);
            hinmeiNameCell.Location = new Point(x, 0);
            suuryouCell.Location = new Point(x, 20);
            //unitCell.Location = new Point(x + suuryouCell.Width, 20);
            unitNameCell.Location = new Point(x + suuryouCell.Width, 20);
            tankaCell.Location = new Point(x + suuryouCell.Width + unitNameCell.Width, 20);

            x = x + hinmeiNameCell.Width;

            netjyuuryouHedader.Location = new Point(x, 1);
            netjyuuryouCell.Location = new Point(x, 0);
            kingakuCell.Location = new Point(x, 20);

            x = x + netjyuuryouHedader.Width;

            nisugataSuuryouHeader.Location = new Point(x, 1);
            nisugataSuuryouCell.Location = new Point(x, 0);
            nisugataNameCell.Location = new Point(x, 20);
            //nisugataCdCell.Location = new Point(x + nisugataNameCell.Width, 20);

            x = x + nisugataSuuryouHeader.Width;

            zaikoHinmeiNameHeader.Location = new Point(x, 1);
            zaikoHinmeiCdCell.Location = new Point(x, 0);
            zaikoHinmeiNameCell.Location = new Point(x, 20);

            x = x + zaikoHinmeiNameHeader.Width;

            hinmeiBikouHeader.Location = new Point(x, 1);
            hinmeiBikouCell.Location = new Point(x, 0);
            maniNumberCell.Location = new Point(x, 20);


            // 在庫品名のみ表示しない。明細備考は表示する。
            if (!zaikoHinmeiVisable && !maniNumberVisible)
            {
                // 在庫品名
                zaikoHinmeiNameHeader.Visible = false;
                zaikoHinmeiCdCell.Visible = false;
                zaikoHinmeiNameCell.Visible = false;
                // 明細備考移動
                hinmeiBikouHeader.Location = new Point(zaikoHinmeiNameHeader.Left, 1);
                hinmeiBikouCell.Location = new Point(zaikoHinmeiCdCell.Left, 0);
                maniNumberCell.Location = new Point(zaikoHinmeiNameCell.Left, 20);
                hinmeiBikouHeader.Value = "明細備考";

                zaikoHinmeiNameHeader.Location = new Point(0, 1);
                zaikoHinmeiCdCell.Location = new Point(0, 0);
                zaikoHinmeiNameCell.Location = new Point(0, 20);

                newTemplate.Width = x - 79 + hinmeiBikouHeader.Width;
            }
            // 在庫品名又はマニフェスト番号どちが表示されない場合、
            //          タイトル文字を空文字にし、セルをReadonlyに設定する(有価在庫不具合一覧105、114) Start
            // 在庫品名だけ表示されない場合
            else if (!zaikoHinmeiVisable)
            {
                // 在庫品名表示しない
                zaikoHinmeiNameHeader.Visible = false;
                zaikoHinmeiNameCell.Visible = false;
                zaikoHinmeiCdCell.Visible = false;
                // マニフェスト番号移動
                hinmeiBikouHeader.Location = new Point(zaikoHinmeiNameHeader.Left, 1);
                hinmeiBikouCell.Location = new Point(zaikoHinmeiCdCell.Left, 0);
                maniNumberCell.Location = new Point(zaikoHinmeiNameCell.Left, 20);

                zaikoHinmeiNameHeader.Location = new Point(0, 1);
                zaikoHinmeiCdCell.Location = new Point(0, 0);
                zaikoHinmeiNameCell.Location = new Point(0, 20);

                newTemplate.Width = x - 79 + hinmeiBikouHeader.Width;
            }
            else if (!maniNumberVisible && zaikoHinmeiVisable)
            {
                var maniNumberCellLocation = maniNumberCell.Location;

                // マニフェスト番号
                hinmeiBikouHeader.Value = "明細備考";
                hinmeiBikouHeader.BringToFront();
                maniNumberCell.Visible = false;
                maniNumberCell.ReadOnly = true;

                Cell filler = newTemplate.Row.Cells.FirstOrDefault(cell => cell.Name == zaikoHinmeiCdCell.Name + "_filler");
                if (filler == null)
                {
                    filler = new GcCustomTextBoxCell();
                    newTemplate.Row.Cells.Add(filler);
                }
                filler.Location = maniNumberCellLocation;
                filler.Size = maniNumberCell.Size; // マニと同じサイズで設定
                filler.Name = zaikoHinmeiCdCell.Name + "_filler";
                filler.ReadOnly = true;
                filler.BringToFront();
                newTemplate.Width = x + hinmeiBikouHeader.Width;
            }
            else if (maniNumberVisible)
            {
                newTemplate.Width = x + hinmeiBikouHeader.Width;
            }

            this.form.MultiRow_UkeireMeisai.Template = newTemplate;
            this.form.MultiRow_UkeireMeisai.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }

    }
}