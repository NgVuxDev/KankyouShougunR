using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.Message;


namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku
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
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 許可区分
        /// </summary>
        private int kyokaKbn = 0;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        private string mcreateSql { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable chiikiKyokaNoSearchResult { get; set; }

        /// <summary>
        /// 電子契約基本Dao
        /// </summary>
        private DenshiKeiyakuKihonDAO denshiKeiyakuKihonDao;

        /// <summary>
        /// 電子契約送付情報Dao
        /// </summary>
        private DenshiKeiyakuSouhuinfoDAO denshiKeiyakuSouhuinfoDao;

        /// <summary>
        /// 電子契約入力送付情報Dao
        /// </summary>
        private DenshiKeiyakuNyuuryokuSouhusakiDAO denshiKeiyakuNyuuryokuSouhuinfoDao;

        /// <summary>
        /// 電子契約契約情報Dao
        /// </summary>
        private DenshiKeiyakuKeiyakuinfoDAO denshiKeiyakuKeiyakuinfoDao;

        /// <summary>
        /// 電子契約送付先Dao
        /// </summary>
        private DenshiKeiyakuSouhusakiDAO denshiKeiyakuSouhusakiDao;

        /// <summary>
        /// 電子契約共有先情報Dao
        /// </summary>
        private DenshiKeiyakuKyoyusakiDAO denshiKeiyakuKyoyusakiDao;

        /// <summary>
        /// 共有先先Dao
        /// </summary>
        private KyoyusakiDAO kyoyusakiDao;

        /// <summary>
        /// 電子契約API
        /// </summary>
        private IS_DENSHI_CONNECTDao denshiConnectDao;

        /// <summary>
        /// 電子契約委託情報DAO
        /// </summary>
        private DenshiKeiyakuItakuDataDao daoDenshiKeiyakuItakuData;

        /// <summary>
        /// 電子契約入力の契約情報タブで使用する情報DAO
        /// </summary>
        private KeiyakuInfoDataDAO daoKeiyakuInfoData;

        /// <summary>
        /// システム設定
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 電子契約APIロジッククラス
        /// </summary>
        private DenshiLogic denshiLogic;

        /// <summary>
        /// クライアントID
        /// </summary>
        private string clientId;

        /// <summary>
        /// ドキュメントID
        /// </summary>
        private string documentId;

        /// <summary>
        /// 電子契約基本Entity
        /// </summary>
        private T_DENSHI_KEIYAKU_KIHON denshiKihon;

        /// <summary>
        /// 電子契約送付情報Entity
        /// </summary>
        private T_DENSHI_KEIYAKU_SOUHUINFO[] denshiSouhuinfo;

        /// <summary>
        /// 電子契約入力送付情報Entity
        /// </summary>
        private T_DENSHI_KEIYAKU_SOUHUSAKI[] denshiNyuuryokuSouhusaki;

        /// <summary>
        /// 電子契約契約情報Entity
        /// </summary>
        private T_DENSHI_KEIYAKU_KEIYAKUINFO[] denshiKeiyakuinfo;

        /// <summary>
        /// 電子契約送付先マスタEntity
        /// </summary>
        internal M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] denshiSouhusaki;

        /// <summary>
        /// 共有先情報Entity
        /// </summary>
        private T_DENSHI_KEIYAKU_KYOYUSAKI[] denshiKeiyakuKyoyusaki;

        /// <summary>
        /// 共有先マスタEntity
        /// </summary>
        private M_KYOYUSAKI[] kyoyusaki;

        /// <summary>
        /// 委託契約書のシステムID
        /// </summary>
        internal string systemId;

        /// <summary>
        /// 電子契約のシステムID
        /// </summary>
        internal string denshiSystemId;

        /// <summary>
        /// システム設定のEntity
        /// </summary>
        private M_SYS_INFO SysInfo;

        /// <summary>
        /// 電子契約情報の取得リスト
        /// </summary>
        private List<DenshiKeiyakuItakuDataDTO> denshiItakuDataList;

        /// <summary>
        /// 電子契約の契約情報タブのデータ取得リスト
        /// </summary>
        private List<KeiyakuInfoDataDTO> keiyakuInfoDataList;

        /// <summary>
        /// 電子契約WebAPIの接続情報リスト
        /// </summary>
        private List<S_DENSHI_CONNECT> denshiConnectList;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader HeaderForm;

        #endregion

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            return 0;
        }

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // メッセージ用
            this.msgLogic = new MessageBoxShowLogic();

            this.denshiKeiyakuKihonDao = DaoInitUtility.GetComponent<DenshiKeiyakuKihonDAO>();
            this.denshiKeiyakuSouhuinfoDao = DaoInitUtility.GetComponent<DenshiKeiyakuSouhuinfoDAO>();
            this.denshiKeiyakuSouhusakiDao = DaoInitUtility.GetComponent<DenshiKeiyakuSouhusakiDAO>();
            this.denshiConnectDao = DaoInitUtility.GetComponent<IS_DENSHI_CONNECTDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoDenshiKeiyakuItakuData = DaoInitUtility.GetComponent<DenshiKeiyakuItakuDataDao>();
            this.daoKeiyakuInfoData = DaoInitUtility.GetComponent<KeiyakuInfoDataDAO>();
            this.denshiKeiyakuNyuuryokuSouhuinfoDao = DaoInitUtility.GetComponent<DenshiKeiyakuNyuuryokuSouhusakiDAO>();
            this.denshiKeiyakuKeiyakuinfoDao = DaoInitUtility.GetComponent<DenshiKeiyakuKeiyakuinfoDAO>();
            this.denshiKeiyakuKyoyusakiDao = DaoInitUtility.GetComponent<DenshiKeiyakuKyoyusakiDAO>();
            this.kyoyusakiDao = DaoInitUtility.GetComponent<KyoyusakiDAO>();

            this.denshiLogic = new DenshiLogic();

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

                //ボタンのテキストを初期化
                this.ButtonInit();

                //検索条件の初期化処理
                this.SetInitialRenkeiCondition();

                //ボタン制御
                this.ButtonEnabledControl();

                //イベントの初期化処理
                this.EventInit();

                // キー入力設定
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.parentForm.headerForm;

                // クライアントIDを取得する。
                this.GetClientID();

                // システム設定を取得する。
                this.SysInfo = this.daoSysInfo.GetAllDataForCode("0");

                // S_DENSHI_CONNECTデータを取得
                S_DENSHI_CONNECT[] denshiConnectData = this.denshiConnectDao.GetAllData();
                this.denshiConnectList = new List<S_DENSHI_CONNECT>(denshiConnectData);
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
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 契約書取消(F4)イベント作成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            // 部分更新(F5)イベント作成
            parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);
            // 契約書送付(F9)イベント作成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            // 行削除(F11)イベント作成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);
            // 閉じる(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 契約書ダウンロードボタン(process2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
            // 契約状況取得ボタン(process3)イベント生成
            parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
            // 合意締結証明書ボタン(process4)イベント生成
            parentForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);

            // 送付一覧のEnterイベント生成
            this.form.customDataGridView1.CellEnter += new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);

            // 共有先一覧のEnterイベント生成
            this.form.kyoyusakiIchiran.CellEnter += new DataGridViewCellEventHandler(this.form.KyoyusakiIchiran_CellEnter);

            // 共有先追加する/しない選択イベント
            this.form.KYOYUSAKI_TSUIKA.TextChanged += new EventHandler(this.form.KYOYUSAKI_TSUIKA_TextChanged);

        }

        /// <summary>
        /// 抽出条件の初期化
        /// </summary>
        private void SetInitialRenkeiCondition()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 修正モード、削除モードで変更不可となる項目の設定
            if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                || this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                // 送付タイトル
                this.form.SendTitle.Enabled = false;
                // 送付メッセージ
                this.form.SendMessage.Enabled = false;

                // さらに削除モードのみで変更不可となる項目の設定
                if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    // 社内備考
                    this.form.SyanaiBiko.Enabled = false;
                }

                // 共有先タブ
                this.form.KYOYUSAKI_TSUIKA.ReadOnly = true;
                this.form.KYOYUSAKI_TSUIKA_1.Enabled = false;
                this.form.KYOYUSAKI_TSUIKA_2.Enabled = false;
                this.form.kyoyusakiIchiran.ReadOnly = true;
            }
        }

        #endregion

        #region ボタン制御
        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        private void ButtonEnabledControl()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 初期化
            // 新規モードの場合
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func9.Enabled = true;
                parentForm.bt_func11.Enabled = true;
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
            }
            // 修正モードの場合
            else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                parentForm.bt_func4.Enabled = true;
                parentForm.bt_func5.Enabled = true;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func11.Enabled = false;
                parentForm.bt_process2.Enabled = true;
                parentForm.bt_process3.Enabled = true;
                parentForm.bt_process4.Enabled = false;
            }
            // 削除モードの場合
            else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                parentForm.bt_func4.Enabled = true;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func11.Enabled = false;
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
            }
            // 参照モードの場合
            else
            {
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func11.Enabled = false;
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
            }

            parentForm.bt_func12.Enabled = true;
        }
        #endregion

        /// <summary>
        /// テーブルからデータを取得する。
        /// </summary>
        private void GetTableData()
        {
            // 委託契約書のシステムIDから、委託契約書関連データを取得する。
            M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
            cond.SYSTEM_ID = this.systemId;

            this.denshiItakuDataList = this.daoDenshiKeiyakuItakuData.GetDenshiKeiyakuItakuData(cond);
            this.keiyakuInfoDataList = this.daoKeiyakuInfoData.GetKeiyakuInfoData(cond);

            // 委託契約書のシステムIDから、電子契約関連データを取得する。
            this.GetDenshiKeiyakuJyouhou();
        }

        /// <summary>
        /// 電子契約関連データの取得
        /// </summary>
        internal void GetDenshiKeiyakuJyouhou()
        {
            // T_DENSHI_KEIYAKU_KIHON
            this.denshiKihon = denshiKeiyakuKihonDao.GetDataByCd(this.systemId, this.denshiSystemId);

            // T_DENSHI_KEIYAKU_SOUHUINFO
            this.denshiSouhuinfo = denshiKeiyakuSouhuinfoDao.GetDataByCd(this.systemId, this.denshiSystemId);

            // T_DENSHI_KEIYAKU_SOUHUSAKI
            this.denshiNyuuryokuSouhusaki = denshiKeiyakuNyuuryokuSouhuinfoDao.GetDataByCd(this.systemId, this.denshiSystemId);

            // T_DENSHI_KEIYAKU_KEIYAKUINFO
            this.denshiKeiyakuinfo = denshiKeiyakuKeiyakuinfoDao.GetDataByCd(this.systemId, this.denshiSystemId);

            // M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI
            this.denshiSouhusaki = GetDenshiKeiyakuSouhusaki(this.systemId);

            // T_DENSHI_KEIYAKU_KYOYUSAKI
            this.denshiKeiyakuKyoyusaki = denshiKeiyakuKyoyusakiDao.GetDataByCd(this.systemId, this.denshiSystemId);

            // M_KYOYUSAKI
            this.kyoyusaki = kyoyusakiDao.GetAllData().ToList().Where(n => n.DELETE_FLG.IsFalse).ToArray();

        }

        /// <summary>
        /// 社内、社外で重複している優先番号を一意に再付番した電子送付先リストを取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns></returns>
        private M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] GetDenshiKeiyakuSouhusaki(string systemId)
        {
            if (string.IsNullOrEmpty(systemId))
            {
                return null;
            }

            var list = denshiKeiyakuSouhusakiDao.GetDataByCd(systemId).ToList();
            var result = new M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[list.Count];

            short i = 0;
            // 社内かつ優先度の昇順で再付番
            foreach (var entity in list.Where(n => !string.IsNullOrEmpty(n.SHAIN_CD)).OrderBy(n => n.PRIORITY_NO))
            {
                result[i] = entity;
                var priority = i + 1;
                result[i].PRIORITY_NO = short.Parse(priority.ToString());
                i++;
            }
            // 社外かつ優先度の昇順で再付番
            foreach (var entity in list.Where(n => string.IsNullOrEmpty(n.SHAIN_CD)).OrderBy(n => n.PRIORITY_NO))
            {
                result[i] = entity;
                var priority = i + 1;
                result[i].PRIORITY_NO = short.Parse(priority.ToString());
                i++;
            }

            return result;
        }

        /// <summary>
        /// 各タブの情報を設定する。
        /// </summary>
        /// <returns></returns>
        private bool SetTabData()
        {
            bool ret = false;

            // 送付情報タブの一覧に値を設定
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 委託契約情報にファイルパスが設定されている場合
                if (!String.IsNullOrEmpty(this.denshiItakuDataList[0].ITAKU_KEIYAKU_FILE_PATH))
                {
                    // ファイル種類、ファイルパスの設定（11,12,13,21,22,23）
                    ret = this.SetFileSetting();
                }
                // ファイル種類（31,32,33）、地域名、許可番号、ファイルパスの設定
                ret = this.SetFileSettingEtc();
            }
            else
            {
                ret = this.SetSendInfoTab();
            }

            // 契約情報タブに値を設定
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (!String.IsNullOrEmpty(this.denshiItakuDataList[0].HAISHUTSU_JIGYOUSHA_CD))
                {
                    // 排出事業者CD
                    this.form.GyoushaCD.Text = this.denshiItakuDataList[0].HAISHUTSU_JIGYOUSHA_CD;
                    // 排出業者名
                    this.form.GyoushaName.Text = this.keiyakuInfoDataList[0].KIHON_GYOUSHA_NAME_RYAKU;
                }

                // 備考１
                this.form.Biko1.Text = this.denshiItakuDataList[0].BIKOU1;
                // 備考２
                this.form.Biko2.Text = this.denshiItakuDataList[0].BIKOU2;

                // 委託契約の排出事業場（明細）にデータがある場合、排出事業場リストを表示する。
                if (this.keiyakuInfoDataList[0].HTS_HAISHUTSU_JIGYOUJOU_CD != null)
                {
                    for (int i = 0; i < this.keiyakuInfoDataList.Count; i++)
                    {
                        this.form.customDataGridView2.Rows.Add();
                        // 排出事業場CD
                        this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouCD"].Value = this.keiyakuInfoDataList[i].HTS_HAISHUTSU_JIGYOUJOU_CD;
                        // 排出事業場名
                        this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouName"].Value = this.keiyakuInfoDataList[i].HTS_HAISHUTSU_JIGYOUJOU_NAME;
                        // 都道府県名
                        this.form.customDataGridView2.Rows[i].Cells["colTodouhukenName"].Value = this.keiyakuInfoDataList[i].HTS_TODOUFUKEN_NAME;
                        // 住所１
                        this.form.customDataGridView2.Rows[i].Cells["colJuusho1"].Value = this.keiyakuInfoDataList[i].HTS_HAISHUTSU_JIGYOUJOU_ADDRESS1;
                        // 住所２
                        this.form.customDataGridView2.Rows[i].Cells["colJuusho2"].Value = this.keiyakuInfoDataList[i].HTS_HAISHUTSU_JIGYOUJOU_ADDRESS2;
                    }
                }
            }
            // 修正/削除モードの場合
            else
            {
                // 排出事業者CD
                this.form.GyoushaCD.Text = this.denshiKihon.HAISHUTSU_JIGYOUSHA_CD;
                // 排出業者名
                this.form.GyoushaName.Text = this.denshiKihon.HAISHUTSU_JIGYOUSHA_NAME;
                // 備考１
                this.form.Biko1.Text = this.denshiKihon.BIKOU1;
                // 備考２
                this.form.Biko2.Text = this.denshiKihon.BIKOU2;
                // 排出事業場一覧
                for (int i = 0; i < this.denshiKeiyakuinfo.Length; i++)
                {
                    this.form.customDataGridView2.Rows.Add();
                    // 排出事業場CD
                    this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouCD"].Value = this.denshiKeiyakuinfo[i].HAISHUTSU_JIGYOUJOU_CD;
                    // 排出事業場名
                    this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouName"].Value = this.denshiKeiyakuinfo[i].HAISHUTSU_JIGYOUJOU_NAME;
                    // 都道府県名
                    this.form.customDataGridView2.Rows[i].Cells["colTodouhukenName"].Value = this.denshiKeiyakuinfo[i].TODOUFUKEN_NAME;
                    // 住所１
                    this.form.customDataGridView2.Rows[i].Cells["colJuusho1"].Value = this.denshiKeiyakuinfo[i].HAISHUTSU_JIGYOUJOU_ADDRESS1;
                    // 住所２
                    this.form.customDataGridView2.Rows[i].Cells["colJuusho2"].Value = this.denshiKeiyakuinfo[i].HAISHUTSU_JIGYOUJOU_ADDRESS2;
                }
            }

            // 電子契約タブの設定
            // 新規モードの場合
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 電子契約送付先マスタから設定する。
                if (0 < this.denshiSouhusaki.Length)
                {
                    for (int i = 0; i < this.denshiSouhusaki.Length; i++)
                    {
                        this.form.denshiKeiyakuIchiran.Rows.Add();
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["YUUSEN_NO"].Value = this.denshiSouhusaki[i].PRIORITY_NO;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_CD"].Value = this.denshiSouhusaki[i].SHAIN_CD;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_NAME"].Value = this.denshiSouhusaki[i].SOUHU_TANTOUSHA_NAME;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value = this.denshiSouhusaki[i].MAIL_ADDRESS;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["TEL_NO"].Value = this.denshiSouhusaki[i].TEL_NO;
                        if (string.IsNullOrEmpty(this.denshiSouhusaki[i].SHAIN_CD))
                        {
                            this.form.denshiKeiyakuIchiran.Rows[i].Cells["ATESAKI_NAME"].Value = this.denshiSouhusaki[i].KEIYAKUSAKI_CORP_NAME;
                        }
                        else
                        {
                            this.form.denshiKeiyakuIchiran.Rows[i].Cells["ATESAKI_NAME"].Value = this.denshiSouhusaki[i].ATESAKI_NAME;
                        }
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["BUSHO_NAME"].Value = this.denshiSouhusaki[i].BUSHO_NAME;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["SOUHUSAKI_BIKO"].Value = this.denshiSouhusaki[i].SOUHUSAKI_BIKO;
                    }
                }
            }
            // 修正/削除モードの場合
            else
            {
                // 電子契約送付先情報から設定する。
                if (0 < this.denshiNyuuryokuSouhusaki.Length)
                {
                    for (int i = 0; i < this.denshiNyuuryokuSouhusaki.Length; i++)
                    {
                        this.form.denshiKeiyakuIchiran.Rows.Add();
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["YUUSEN_NO"].Value = this.denshiNyuuryokuSouhusaki[i].PRIORITY_NO;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_CD"].Value = this.denshiNyuuryokuSouhusaki[i].SHAIN_CD;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_NAME"].Value = this.denshiNyuuryokuSouhusaki[i].SHAIN_NAME;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value = this.denshiNyuuryokuSouhusaki[i].MAIL_ADDRESS;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["TEL_NO"].Value = this.denshiNyuuryokuSouhusaki[i].TEL_NO;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["ATESAKI_NAME"].Value = this.denshiNyuuryokuSouhusaki[i].ATESAKI_NAME;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["BUSHO_NAME"].Value = this.denshiNyuuryokuSouhusaki[i].BUSHO_NAME;
                        this.form.denshiKeiyakuIchiran.Rows[i].Cells["SOUHUSAKI_BIKO"].Value = this.denshiNyuuryokuSouhusaki[i].SOUHUSAKI_BIKO;
                    }
                }
            }

            // 共有先タブの設定
            // 新規モードの場合
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 共有先追加（する/しない）
                this.form.KYOYUSAKI_TSUIKA.Text = this.SysInfo.DENSHI_KEIYAKU_KYOUYUUSAKI_CC.ToString();
            }
            // 修正/削除モードの場合
            else
            {
                // 電子契約送付先情報から設定する。
                if (0 < this.denshiKeiyakuKyoyusaki.Length)
                {
                    // 共有先追加（する）
                    this.form.KYOYUSAKI_TSUIKA.Text = "1";
                    this.form.kyoyusakiIchiran.Rows.Clear();

                    for (int i = 0; i < this.denshiKeiyakuKyoyusaki.Length; i++)
                    {
                        this.form.kyoyusakiIchiran.Rows.Add();
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_YUUSEN_NO"].Value = this.denshiKeiyakuKyoyusaki[i].PRIORITY_NO;
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_CORP_NAME"].Value = this.denshiKeiyakuKyoyusaki[i].KYOYUSAKI_CORP_NAME;
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_NAME"].Value = this.denshiKeiyakuKyoyusaki[i].KYOYUSAKI_NAME;
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value = this.denshiKeiyakuKyoyusaki[i].KYOYUSAKI_MAIL_ADDRESS;
                    }
                }
                else
                {
                    // 共有先追加（しない）
                    this.form.KYOYUSAKI_TSUIKA.Text = "2";
                }
            }
            return ret;
        }

        /// <summary>
        /// 電子契約データの設定（修正モード、削除モードの場合）
        /// </summary>
        private void SetDenshiKeiyakuData()
        {
            // 作成日、作成者、最終更新日、最終更新者
            this.HeaderForm.CreateDate.Text = this.denshiKihon.CREATE_DATE.ToString();
            this.HeaderForm.CreateUser.Text = this.denshiKihon.CREATE_USER;
            this.HeaderForm.LastUpdateDate.Text = this.denshiKihon.UPDATE_DATE.ToString();
            this.HeaderForm.LastUpdateUser.Text = this.denshiKihon.UPDATE_USER;

            // 契約日
            if (!this.denshiKihon.KEIYAKUSHO_KEIYAKU_DATE.IsNull)
            {
                this.HeaderForm.KeiyakuDate.Text = DateTime.Parse(this.denshiKihon.KEIYAKUSHO_KEIYAKU_DATE.ToString()).ToString("yyyy/MM/dd");
            }
            // 作成日
            if (!this.denshiKihon.KEIYAKUSHO_CREATE_DATE.IsNull)
            {
                this.HeaderForm.Createbi.Text = DateTime.Parse(this.denshiKihon.KEIYAKUSHO_CREATE_DATE.ToString()).ToString("yyyy/MM/dd");
            }
            // 送付日
            if (!this.denshiKihon.KEIYAKUSHO_SEND_DATE.IsNull)
            {
                this.HeaderForm.SendDate.Text = DateTime.Parse(this.denshiKihon.KEIYAKUSHO_SEND_DATE.ToString()).ToString("yyyy/MM/dd");
            }

            // 契約番号
            this.form.KeiyakuNo.Text = this.denshiKihon.KEIYAKU_NO;
            // 更新種別
            if (this.denshiKihon.KOUSHIN_SHUBETSU == 1)
            {
                this.form.UpdateShubetsu.Text = "自動更新";
            }
            else if (this.denshiKihon.KOUSHIN_SHUBETSU == 2)
            {
                this.form.UpdateShubetsu.Text = "単発";
            }
            // 有効期限（開始）
            if (!this.denshiKihon.YUUKOU_BEGIN.IsNull)
            {
                this.form.YuukoKigen_Begin.Text = DateTime.Parse(this.denshiKihon.YUUKOU_BEGIN.ToString()).ToString("yyyy/MM/dd");
            }
            // 有効期限（終了）
            if (!this.denshiKihon.YUUKOU_END.IsNull)
            {
                this.form.YuukoKigen_End.Text = DateTime.Parse(this.denshiKihon.YUUKOU_END.ToString()).ToString("yyyy/MM/dd");
            }
            // 自動更新終了日
            if (!this.denshiKihon.KOUSHIN_END_DATE.IsNull)
            {
                this.form.JidoUpdateSyuryoDate.Text = DateTime.Parse(this.denshiKihon.KOUSHIN_END_DATE.ToString()).ToString("yyyy/MM/dd");
            }

            // アクセスコード
            this.form.AccessCode.Text = this.denshiKihon.ACCESS_CD;

            // 契約状況
            if (!this.denshiKihon.KEIYAKU_JYOUKYOU.IsNull)
            {
                this.SetKeiyakuJyoukyou(this.denshiKihon.KEIYAKU_JYOUKYOU.Value.ToString());

                // 「2:締結済」の場合、合意締結証明書ボタンを活性化する。
                if (this.denshiKihon.KEIYAKU_JYOUKYOU.Value.ToString().Equals("2"))
                {
                    parentForm.bt_process4.Enabled = true;
                }
            }
            // 送付タイトル
            this.form.SendTitle.Text = this.denshiKihon.SEND_TITLE;
            // 送付メッセージ
            this.form.SendMessage.Text = this.denshiKihon.SEND_MESSAGE;
            // 社内備考
            this.form.SyanaiBiko.Text = this.denshiKihon.SHANAI_BIKO;
        }

        /// <summary>
        /// 修正モード、削除モードの場合の送付情報タブへの設定
        /// </summary>
        private bool SetSendInfoTab()
        {
            bool ret = false;
            int num = 0;

            for (int i = 0; i < this.denshiSouhuinfo.Length; i++)
            {
                if (this.denshiSouhuinfo[i].SEND_FLG)
                {
                    this.form.customDataGridView1.Rows.Add();
                    this.form.customDataGridView1.Rows[num].Cells["colCheckBox"].Value = true;

                    this.form.customDataGridView1.Rows[num].Cells["colFileShuruiCD"].Value = this.denshiSouhuinfo[i].FILE_SHURUI_CD;
                    this.form.customDataGridView1.Rows[num].Cells["colFileShuruiName"].Value = this.denshiSouhuinfo[i].FILE_SHURUI_NAME;
                    this.form.customDataGridView1.Rows[num].Cells["colGyousha"].Value = this.denshiSouhuinfo[i].GYOUSHA_NAME;
                    this.form.customDataGridView1.Rows[num].Cells["colGenba"].Value = this.denshiSouhuinfo[i].GENBA_NAME;
                    this.form.customDataGridView1.Rows[num].Cells["colChiikiName"].Value = this.denshiSouhuinfo[i].CHIIKI_NAME;
                    this.form.customDataGridView1.Rows[num].Cells["colKyokaNo"].Value = this.denshiSouhuinfo[i].KYOKA_NO;
                    this.form.customDataGridView1.Rows[num].Cells["colBiko"].Value = this.denshiSouhuinfo[i].BIKO;
                    this.form.customDataGridView1.Rows[num].Cells["colFilePath"].Value = this.denshiSouhuinfo[i].FILE_PATH;
                    num++;
                }

                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 画面に反映
        /// </summary>
        internal void SetValue()
        {
            // 必要なデータをテーブルから取得する。
            this.GetTableData();

            if (this.denshiItakuDataList.Count == 0)
            {
                this.HeaderForm.ItakuSyoshiki.Text = string.Empty;
                this.HeaderForm.ItakuShurui.Text = string.Empty;
            }
            else
            {
                // 委託契約情報から画面に値を設定
                // 委託契約書書式
                if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_TYPE.Equals("1"))
                {
                    this.HeaderForm.ItakuSyoshiki.Text = ConstCls.SANPAI_SHOSHIKI_NAME;
                }
                else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_TYPE.Equals("2"))
                {
                    this.HeaderForm.ItakuSyoshiki.Text = ConstCls.KENPAI_SHOSHIKI_NAME;
                }

                // 委託契約書種類
                if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("1"))
                {
                    this.HeaderForm.ItakuShurui.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_1;
                }
                else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("2"))
                {
                    this.HeaderForm.ItakuShurui.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_2;
                }
                else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("3"))
                {
                    this.HeaderForm.ItakuShurui.Text = ConstCls.KEIYAKUSHO_SHURUI_NAME_3;
                }
            }
            bool newRowFlg = false;

            // 各タブの情報を設定する。
            newRowFlg = this.SetTabData();

            // 新規モード時
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 契約日
                if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KEIYAKUSHO_KEIYAKU_DATE))
                {
                    this.HeaderForm.KeiyakuDate.Text = DateTime.Parse(this.denshiItakuDataList[0].KEIYAKUSHO_KEIYAKU_DATE).ToString("yyyy/MM/dd");
                }
                // 作成日
                if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KEIYAKUSHO_CREATE_DATE))
                {
                    this.HeaderForm.Createbi.Text = DateTime.Parse(this.denshiItakuDataList[0].KEIYAKUSHO_CREATE_DATE).ToString("yyyy/MM/dd");
                }
                // 送付日
                if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KEIYAKUSHO_SEND_DATE))
                {
                    this.HeaderForm.SendDate.Text = DateTime.Parse(this.denshiItakuDataList[0].KEIYAKUSHO_SEND_DATE).ToString("yyyy/MM/dd");
                }

                // 契約番号
                this.form.KeiyakuNo.Text = this.denshiItakuDataList[0].ITAKU_KEIYAKU_NO;
                // 更新種別
                if (this.denshiItakuDataList[0].KOUSHIN_SHUBETSU.Equals("1"))
                {
                    this.form.UpdateShubetsu.Text = "自動更新";
                }
                else if (this.denshiItakuDataList[0].KOUSHIN_SHUBETSU.Equals("2"))
                {
                    this.form.UpdateShubetsu.Text = "単発";
                }
                // 有効期限（開始）
                if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].YUUKOU_BEGIN))
                {
                    this.form.YuukoKigen_Begin.Text = DateTime.Parse(this.denshiItakuDataList[0].YUUKOU_BEGIN).ToString("yyyy/MM/dd");
                }
                // 有効期限（終了）
                if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].YUUKOU_END))
                {
                    this.form.YuukoKigen_End.Text = DateTime.Parse(this.denshiItakuDataList[0].YUUKOU_END).ToString("yyyy/MM/dd");
                }
                // 自動更新終了日
                if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KOUSHIN_END_DATE))
                {
                    this.form.JidoUpdateSyuryoDate.Text = DateTime.Parse(this.denshiItakuDataList[0].KOUSHIN_END_DATE).ToString("yyyy/MM/dd");
                }

                // アクセスコード
                if (this.denshiSouhusaki != null && this.denshiSouhusaki.Length > 0)
                {
                    this.form.AccessCode.Text = this.denshiSouhusaki[0].ACCESS_CD;
                }

                // 送付メッセージ
                this.form.SendMessage.Text = this.SysInfo.DENSHI_KEIYAKU_MESSAGE;

                if (newRowFlg)
                {
                    foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                    {
                        // ヘッダーのチェックボックスをONにする。
                        this.form.chkSelect.Checked = true;

                        // チェックボックスをONにする。
                        if (!row.IsNewRow)
                        {
                            row.Cells["colCheckBox"].Value = true;
                        }
                    }
                }

                // 送付情報一覧に新規行の追加を許可する。
                this.form.customDataGridView1.AllowUserToAddRows = true;
            }
            // 修正モード時
            else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                // 電子契約データの設定
                this.SetDenshiKeiyakuData();

                // 送付情報一覧に新規行の追加を許可しない。
                this.form.customDataGridView1.AllowUserToAddRows = false;

                if (newRowFlg)
                {
                    for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                    {
                        // チェックボックスをOFFにする。
                        this.form.customDataGridView1.Rows[i].Cells["colCheckBox"].Value = false;
                    }

                    // 参照ボタンを非活性にする。
                    this.form.customDataGridView1.Columns["colFilePathSansyo"].ReadOnly = true;
                }
            }
            // 削除モード時
            else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                // 電子契約データの設定
                this.SetDenshiKeiyakuData();

                // 送付情報一覧に新規行の追加を許可しない。
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // 送付情報タブのヘッダーの選択チェックボックスを変更不可にする。
                this.form.chkSelect.Enabled = false;

                if (newRowFlg)
                {
                    // 以下の項目を変更不可にする。
                    // チェックボックス
                    this.form.customDataGridView1.Columns["colCheckBox"].ReadOnly = true;
                    // ファイル種類CD
                    this.form.customDataGridView1.Columns["colFileShuruiCD"].ReadOnly = true;
                    // 備考
                    this.form.customDataGridView1.Columns["colBiko"].ReadOnly = true;
                    // 参照ボタンを非活性にする。
                    this.form.customDataGridView1.Columns["colFilePathSansyo"].ReadOnly = true;
                }
            }
        }



        /// <summary>
        /// ファイル種類（1～6）、ファイルパスを設定する。
        /// </summary>
        private bool SetFileSetting()
        {
            bool ret = false;

            // 委託契約書書式、委託契約書種類から、ファイル種類の設定値を決定する。
            int shoshiki = int.Parse(this.denshiItakuDataList[0].ITAKU_KEIYAKU_TYPE);
            switch (shoshiki)
            {
                case 1:
                    this.form.customDataGridView1.Rows.Add();
                    if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("1"))
                    {
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiCD"].Value = "11";
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_11;
                    }
                    else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("2"))
                    {
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiCD"].Value = "12";
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_12;
                    }
                    else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("3"))
                    {
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiCD"].Value = "13";
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_13;
                    }
                    ret = true;
                    break;
                case 2:
                    this.form.customDataGridView1.Rows.Add();
                    if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("1"))
                    {
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiCD"].Value = "21";
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_21;
                    }
                    else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("2"))
                    {
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiCD"].Value = "22";
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_22;
                    }
                    else if (this.denshiItakuDataList[0].ITAKU_KEIYAKU_SHURUI.Equals("3"))
                    {
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiCD"].Value = "23";
                        this.form.customDataGridView1.CurrentRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_23;
                    }
                    ret = true;
                    break;
                default:
                    break;
            }

            // 業者、現場を設定する。
            this.form.customDataGridView1.CurrentRow.Cells["colGyousha"].Value = this.keiyakuInfoDataList[0].KIHON_GYOUSHA_NAME_RYAKU;
            // 個別指定のチェックがONの場合、委託契約書の排出事業場名を設定
            if (this.denshiItakuDataList[0].KOBETSU_SHITEI_CHECK)
            {
                this.form.customDataGridView1.CurrentRow.Cells["colGenba"].Value = this.keiyakuInfoDataList[0].KIHON_GENBA_NAME_RYAKU;
            }
            // 個別指定のチェックがOFFの場合、委託契約書の排出事業場メモを設定
            else
            {
                this.form.customDataGridView1.CurrentRow.Cells["colGenba"].Value = this.denshiItakuDataList[0].HST_FREE_COMMENT;
            }

            // ファイルパスを設定する。
            this.form.customDataGridView1.CurrentRow.Cells["colFilePath"].Value = this.denshiItakuDataList[0].ITAKU_KEIYAKU_FILE_PATH;

            return ret;
        }

        /// <summary>
        /// ファイル種類（7～9）、地域名、許可番号、ファイルパスを設定する。
        /// </summary>
        private bool SetFileSettingEtc()
        {
            int kyokaKbn = 0;
            bool ret = false;
            int upnKyokaKbn = 0;
            int sbnKyokaKbn = 0;

            // 運搬許可証情報がある
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].UPN_KYOKA_KBN))
            {
                kyokaKbn = 1;

                // 運搬許可証データを抽出する。
                var upnList = this.denshiItakuDataList.Select(n => new { n.UPN_KYOKA_KBN, n.UPN_GYOUSHA_CD, n.UPN_GENBA_CD, n.UPN_CHIIKI_CD, n.UPN_GYOUSHA_NAME }).Distinct();

                foreach (var upnData in upnList)
                {
                    upnKyokaKbn = int.Parse(upnData.UPN_KYOKA_KBN);

                    // 検索条件の設定
                    KyokaNoSearchDataDTO dto = new KyokaNoSearchDataDTO();
                    dto.GYOUSHA_CD = upnData.UPN_GYOUSHA_CD;
                    dto.GENBA_CD = upnData.UPN_GENBA_CD;
                    dto.CHIIKI_CD = upnData.UPN_CHIIKI_CD;

                    // M_CHIIKIBETSU_KYOKAにファイルパスが設定されているか検索する。
                    this.GetChiikiKyokaNoData(kyokaKbn, dto);

                    // 検索結果を一覧に反映する。
                    if (this.chiikiKyokaNoSearchResult != null && this.chiikiKyokaNoSearchResult.Rows.Count > 0)
                    {
                        foreach (DataRow row in this.chiikiKyokaNoSearchResult.Rows)
                        {
                            DataRow dr = row;

                            // 一覧の行数を取得する。
                            int cnt = this.form.customDataGridView1.RowCount;

                            // 許可区分が「1:普通産廃」の場合
                            if (upnKyokaKbn == 1)
                            {
                                ret = true;
                                this.form.customDataGridView1.Rows.Add();

                                // 追加行を除いた許可番号データを挿入する行を特定する。
                                DataGridViewRow addRow = this.form.customDataGridView1.Rows[cnt];

                                addRow.Cells["colFileShuruiCD"].Value = "31";
                                addRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_31;

                                // 業者名
                                addRow.Cells["colGyousha"].Value = upnData.UPN_GYOUSHA_NAME;

                                // 地域名
                                addRow.Cells["colChiikiName"].Value = dr["CHIIKI_NAME"];
                                // 許可番号
                                addRow.Cells["colKyokaNo"].Value = dr["FUTSUU_KYOKA_NO"];
                                // ファイルパス
                                addRow.Cells["colFilePath"].Value = dr["FUTSUU_KYOKA_FILE_PATH"];

                            }
                            // 許可区分が「2:特別産廃」の場合
                            else if (upnKyokaKbn == 2)
                            {
                                ret = true;
                                this.form.customDataGridView1.Rows.Add();

                                // 追加行を除いた許可番号データを挿入する行を特定する。
                                DataGridViewRow addRow = this.form.customDataGridView1.Rows[cnt];

                                addRow.Cells["colFileShuruiCD"].Value = "31";
                                addRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_31;

                                // 業者名
                                addRow.Cells["colGyousha"].Value = upnData.UPN_GYOUSHA_NAME;

                                // 地域名
                                addRow.Cells["colChiikiName"].Value = dr["CHIIKI_NAME"];
                                // 許可番号
                                addRow.Cells["colKyokaNo"].Value = dr["TOKUBETSU_KYOKA_NO"];
                                // ファイルパス
                                addRow.Cells["colFilePath"].Value = dr["TOKUBETSU_KYOKA_FILE_PATH"];
                            }
                        }
                    }
                }
            }

            // 処分許可証情報がある
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].SBN_KYOKA_KBN))
            {
                // 処分許可証データを抽出する。
                var sbnList = this.denshiItakuDataList.Select(n => new { n.SBN_KYOKA_KBN, n.SBN_GYOUSHA_CD, n.SBN_GENBA_CD, n.SBN_CHIIKI_CD, n.SBN_GYOUSHA_NAME, n.SBN_GENBA_NAME }).Distinct();

                foreach (var sbnData in sbnList)
                {
                    sbnKyokaKbn = int.Parse(sbnData.SBN_KYOKA_KBN);

                    if (sbnKyokaKbn == 3 || sbnKyokaKbn == 4)
                    {
                        kyokaKbn = 2;
                    }
                    else if (sbnKyokaKbn == 5 || sbnKyokaKbn == 6)
                    {
                        kyokaKbn = 3;
                    }

                    // 検索条件の設定
                    KyokaNoSearchDataDTO dto = new KyokaNoSearchDataDTO();
                    dto.GYOUSHA_CD = sbnData.SBN_GYOUSHA_CD;
                    dto.GENBA_CD = sbnData.SBN_GENBA_CD;
                    dto.CHIIKI_CD = sbnData.SBN_CHIIKI_CD;

                    // M_CHIIKIBETSU_KYOKAにファイルパスが設定されているか検索する。
                    this.GetChiikiKyokaNoData(kyokaKbn, dto);

                    // 検索結果を一覧に反映する。
                    if (this.chiikiKyokaNoSearchResult != null && this.chiikiKyokaNoSearchResult.Rows.Count > 0)
                    {
                        foreach (DataRow row in this.chiikiKyokaNoSearchResult.Rows)
                        {
                            DataRow dr = row;

                            // 一覧の行数を取得する。
                            int cnt = this.form.customDataGridView1.RowCount;

                            // 許可区分が「3:普通中間」、「5:普通最終」の場合
                            if (sbnKyokaKbn == 3 || sbnKyokaKbn == 5)
                            {
                                ret = true;
                                this.form.customDataGridView1.Rows.Add();

                                // 追加行を除いた許可番号データを挿入する行を特定する。
                                DataGridViewRow addRow = this.form.customDataGridView1.Rows[cnt];

                                if (sbnKyokaKbn == 3)
                                {
                                    addRow.Cells["colFileShuruiCD"].Value = "32";
                                    addRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_32;
                                }
                                else if (sbnKyokaKbn == 5)
                                {
                                    addRow.Cells["colFileShuruiCD"].Value = "33";
                                    addRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_33;
                                }

                                // 業者
                                addRow.Cells["colGyousha"].Value = sbnData.SBN_GYOUSHA_NAME;
                                // 現場
                                addRow.Cells["colGenba"].Value = sbnData.SBN_GENBA_NAME;
                                // 地域名
                                addRow.Cells["colChiikiName"].Value = dr["CHIIKI_NAME"];
                                // 許可番号
                                addRow.Cells["colKyokaNo"].Value = dr["FUTSUU_KYOKA_NO"];
                                // ファイルパス
                                addRow.Cells["colFilePath"].Value = dr["FUTSUU_KYOKA_FILE_PATH"];
                            }
                            // 許可区分が「4:特別中間」、「6:特別最終」の場合
                            else if (sbnKyokaKbn == 4 || sbnKyokaKbn == 6)
                            {
                                ret = true;
                                this.form.customDataGridView1.Rows.Add();

                                // 追加行を除いた許可番号データを挿入する行を特定する。
                                DataGridViewRow addRow = this.form.customDataGridView1.Rows[cnt];

                                if (sbnKyokaKbn == 4)
                                {
                                    addRow.Cells["colFileShuruiCD"].Value = "32";
                                    addRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_32;
                                }
                                else if (sbnKyokaKbn == 6)
                                {
                                    addRow.Cells["colFileShuruiCD"].Value = "33";
                                    addRow.Cells["colFileShuruiName"].Value = ConstCls.FILE_SHURUI_NAME_33;
                                }
                                // 業者
                                addRow.Cells["colGyousha"].Value = sbnData.SBN_GYOUSHA_NAME;
                                // 現場
                                addRow.Cells["colGenba"].Value = sbnData.SBN_GENBA_NAME;
                                // 地域名
                                addRow.Cells["colChiikiName"].Value = dr["CHIIKI_NAME"];
                                // 許可番号
                                addRow.Cells["colKyokaNo"].Value = dr["TOKUBETSU_KYOKA_NO"];
                                // ファイルパス
                                addRow.Cells["colFilePath"].Value = dr["TOKUBETSU_KYOKA_FILE_PATH"];
                            }
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        internal void CheckPopup(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.customDataGridView1.Columns[this.form.customDataGridView1.CurrentCell.ColumnIndex].Name.Equals("colFileShuruiCD"))
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;

                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "11";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_11;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "12";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_12;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "13";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_13;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "21";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_21;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "22";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_22;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "23";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_23;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "31";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_31;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "32";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_32;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "33";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_33;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "99";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_99;
                    dt.Rows.Add(row);

                    dt.TableName = "ファイル種類";
                    form.table = dt;
                    form.PopupTitleLabel = "ファイル種類";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "ファイル種類CD", "ファイル種類名" };

                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.customDataGridView1.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentCell.RowIndex].Cells["colFileShuruiName"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
            }
            LogUtility.DebugMethodEnd(e);
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        private void GetChiikiKyokaNoData(int kyokaKbn, KyokaNoSearchDataDTO searchDataDto)
        {
            this.kyokaKbn = kyokaKbn;

            var sql = new StringBuilder();

            //SQL生成
            //SELECT
            this.CreateSQLSelect(sql);
            //JOIN
            this.CreateSQLJoin(sql);
            //WHERE
            this.CreateSQLWhere(sql, searchDataDto);

            this.mcreateSql = sql.ToString();

            //検索実行
            this.chiikiKyokaNoSearchResult = new DataTable();
            if (!string.IsNullOrEmpty(this.mcreateSql))
            {
                this.chiikiKyokaNoSearchResult = this.denshiKeiyakuKihonDao.getdateforstringsql(this.mcreateSql);
            }
        }

        #region SQL生成
        #region SELECT
        /// <summary>
        /// SELECT句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLSelect(StringBuilder sql)
        {
            //M_CHIIKIBETSU_KYOKA
            sql.Append(" SELECT ");
            sql.Append(" CK.KYOKA_KBN ");
            sql.Append(" ,CK.FUTSUU_KYOKA_NO ");
            sql.Append(" ,CK.FUTSUU_KYOKA_FILE_PATH ");
            sql.Append(" ,CK.TOKUBETSU_KYOKA_NO ");
            sql.Append(" ,CK.TOKUBETSU_KYOKA_FILE_PATH ");
            //M_CHIIKI
            sql.Append(" ,MC.CHIIKI_NAME ");

            sql.Append(" FROM M_CHIIKIBETSU_KYOKA CK ");
        }
        #endregion
        #region JOIN
        /// <summary>
        /// JOIN句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLJoin(StringBuilder sql)
        {
            //JOIN句
            sql.Append(" LEFT JOIN M_CHIIKI MC ON CK.CHIIKI_CD = MC.CHIIKI_CD ");
        }
        #endregion
        #region WHERE
        /// <summary>
        /// WHERE句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLWhere(StringBuilder sql, KyokaNoSearchDataDTO dto)
        {
            string gyoushaCD = "";
            string genbaCD = "";
            string chiikiCD = "";

            gyoushaCD = dto.GYOUSHA_CD;
            genbaCD = dto.GENBA_CD;
            chiikiCD = dto.CHIIKI_CD;

            //WHERE句
            sql.Append(" WHERE ");

            // 許可区分
            sql.AppendFormat(" CK.KYOKA_KBN = {0} ", this.kyokaKbn);
            // 業者CD
            sql.AppendFormat(" AND CK.GYOUSHA_CD = {0} ", "'" + gyoushaCD + "'");
            // 現場CD
            if (!String.IsNullOrEmpty(genbaCD))
            {
                sql.AppendFormat(" AND CK.GENBA_CD = '{0}' ", genbaCD);
            }
            // 地域CD
            sql.AppendFormat(" AND CK.CHIIKI_CD = '{0}' ", chiikiCD);
            //削除フラグ
            sql.Append(" AND CK.DELETE_FLG = 0 ");
        }
        #endregion

        #endregion

        /// <summary>
        /// 委託契約書参照ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool FileRefClick()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = true;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.customDataGridView1.CurrentRow.Cells["colFilePath"].Value = filePath;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約書閲覧ボタン押下処理
        /// </summary>
        internal bool BrowseClick()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.customDataGridView1.CurrentRow.Cells["colFilePath"].Value != null
                    && !string.IsNullOrEmpty(this.form.customDataGridView1.CurrentRow.Cells["colFilePath"].Value.ToString()))
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
                                    writer.Write(this.form.customDataGridView1.CurrentRow.Cells["colFilePath"].Value);
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
                        System.Diagnostics.Process.Start(this.form.customDataGridView1.CurrentRow.Cells["colFilePath"].Value.ToString());
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    // SQLエラー用メッセージを出力
                    this.msgLogic.MessageBoxShow("E200");
                    return true;
                }
                else
                {
                    LogUtility.Error("BrowseClick", ex);
                    this.msgLogic.MessageBoxShow("E245");
                    return true;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 契約書送付の必須チェック
        /// </summary>
        /// <returns></returns>
        internal bool SendBeforeCheck()
        {
            bool ret = false;

            // 送付タイトル
            if (String.IsNullOrEmpty(this.form.SendTitle.Text))
            {
                this.form.SendTitle.BackColor = ConstCls.ERROR_COLOR;
                this.msgLogic.MessageBoxShowError("送付タイトルを入力してください。");
                return ret;
            }
            // 送付情報一覧にデータがない場合
            if (this.form.customDataGridView1.Rows.Count == 0)
            {
                this.msgLogic.MessageBoxShowError("送付情報一覧にデータがありません。");
                return ret;
            }
            // 送付情報一覧で送付チェックが全て未チェックの場合
            bool checkON = false;
            long totalFileSize = 0;
            bool itakuKeiyakushoAri = false;
            bool kyokashouAri = false;

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                if (!row.IsNewRow
                    && row.Cells["colCheckBox"].Value != null
                    && bool.Parse(row.Cells["colCheckBox"].Value.ToString()))
                {
                    checkON = true;

                    // ファイル種類が入力されていること。
                    if (row.Cells["colFileShuruiCD"].Value == null)
                    {
                        row.Cells["colFileShuruiCD"].Style.BackColor = ConstCls.ERROR_COLOR;
                        this.msgLogic.MessageBoxShowError("送付対象のファイル種類が未入力です。");
                        return ret;
                    }
                    // ファイルパスが入力されていること。
                    if (row.Cells["colFilePath"].Value == null || string.IsNullOrEmpty(row.Cells["colFilePath"].Value.ToString()))
                    {
                        row.Cells["colFilePath"].Style.BackColor = ConstCls.ERROR_COLOR;
                        this.msgLogic.MessageBoxShowError("送付対象のファイルパスが未入力です。");
                        return ret;
                    }
                    // ファイルが存在すること。
                    string filePath = row.Cells["colFilePath"].Value.ToString();
                    if (!File.Exists(filePath))
                    {
                        row.Cells["colFilePath"].Style.BackColor = ConstCls.ERROR_COLOR;
                        this.msgLogic.MessageBoxShowError("送付対象のファイルは存在しません。");
                        return ret;
                    }
                    // 全角スペースが含まれてないこと。
                    if (Path.GetFileName(filePath).Contains("　"))
                    {
                        row.Cells["colFilePath"].Style.BackColor = ConstCls.ERROR_COLOR;
                        // 全角スペースをエンコードしても半角スペースとなり、ファイル名不一致となるのでアラートで弾く
                        this.msgLogic.MessageBoxShowError("ファイル名に全角スペースが含まれています。\r\n削除するか半角スペースに変更してください。");
                        return ret;
                    }

                    // 委託契約書がある場合
                    if (row.Cells["colFileShuruiCD"].Value.ToString().Equals("11")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("12")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("13")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("21")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("22")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("23")
                        )
                    {
                        itakuKeiyakushoAri = true;
                    }
                    // 許可証がある場合
                    else if (row.Cells["colFileShuruiCD"].Value.ToString().Equals("31")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("32")
                        || row.Cells["colFileShuruiCD"].Value.ToString().Equals("33")
                        )
                    {
                        kyokashouAri = true;
                    }

                    // ファイルサイズが10MB未満であること。
                    FileInfo fi = new FileInfo(filePath);
                    long fileSize = fi.Length;
                    fileSize = fileSize / 1024 / 1024;
                    totalFileSize += fileSize;
                    if (fileSize >= 10)
                    {
                        row.Cells["colFilePath"].Style.BackColor = ConstCls.ERROR_COLOR;
                        this.msgLogic.MessageBoxShowError("送付対象のファイルサイズが10MBを超えています。");
                        return ret;
                    }

                    // PDFの拡張子をチェック
                    string extension = Path.GetExtension(row.Cells["colFilePath"].Value.ToString());
                    // PDFのフォーマットをチェック
                    bool pdfCheck = false;
                    using (var reader = new PDFReader())
                    {
                        pdfCheck = reader.IsValid(row.Cells["colFilePath"].Value.ToString());
                        // PDFReader変数を破棄する。
                        reader.Dispose();
                    }
                    if (extension != ".pdf" || !pdfCheck)
                    {
                        // 対象のファイルパスのセル背景色を赤くする。
                        row.Cells["colFilePath"].Style.BackColor = ConstCls.ERROR_COLOR;
                        this.msgLogic.MessageBoxShowError("契約書、許可証ファイルは、ＰＤＦファイルで作成してください。");
                        return ret;
                    }
                }
            }

            // システム設定 - 電子契約タブの「電子契約（送付方法）」が【1.委託契約書と許可証をセットで送付】である場合、
            // 委託契約書と許可証が送付対象になっているかチェックする。
            if (this.SysInfo.DENSHI_KEIYAKU_SOUFUHOUHOU == 1
                && itakuKeiyakushoAri && !kyokashouAri)
            {
                this.msgLogic.MessageBoxShowError("委託契約書のみの送付は行えません。\n関連する許可証も送付対象として選択してください。");
                return ret;
            }

            if (totalFileSize >= 50)
            {
                this.msgLogic.MessageBoxShowError("送付対象のファイルサイズの合計が50MBを超えています。");
                return ret;
            }

            if (!checkON)
            {
                this.msgLogic.MessageBoxShowError("送付情報一覧の送付が全て未チェックです。");
                return ret;
            }
            ret = true;
            return ret;
        }

        /// <summary>
        /// 同一ファイル名チェック
        /// </summary>
        /// <returns></returns>
        internal bool SameFileNameCheck()
        {
            HashSet<string> hashFileList = new HashSet<string>();
            int yuukouRowCount = 0;

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                if (!row.IsNewRow
                    && row.Cells["colCheckBox"].Value != null
                    && bool.Parse(row.Cells["colCheckBox"].Value.ToString()))
                {
                    // ファイル名を取得する。
                    string fileName = Path.GetFileName(row.Cells["colFilePath"].Value.ToString());
                    hashFileList.Add(fileName);
                    yuukouRowCount++;
                }
            }

            // 一覧の件数と重複を取り除いた件数が異なっている場合は、重複ありとしてエラーとする。
            if (yuukouRowCount != hashFileList.Count)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ダウンロード前チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckBeforeDownload()
        {
            bool ret = false;

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                if (!row.IsNewRow
                    && bool.Parse(row.Cells["colCheckBox"].Value.ToString()))
                {
                    ret = true;

                    if (row.Cells["colFilePath"].Value == null)
                    {
                        this.msgLogic.MessageBoxShowError("送付情報一覧のファイルパスが設定されていません。");
                        return false;
                    }
                }
            }
            if (!ret)
            {
                this.msgLogic.MessageBoxShowError("送付情報一覧のダウンロードが全て未チェックです。");
            }
            return ret;
        }

        /// <summary>
        /// 社員CDからクライアントIDを取得する。
        /// </summary>
        private void GetClientID()
        {
            // 自身の社員CDから、クライアントIDを取得する。
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" CI.DENSHI_KEIYAKU_CLIENT_ID ");
            sql.Append(" FROM M_DENSHI_KEIYAKU_CLIENT_ID CI ");
            sql.Append(" WHERE ");
            sql.AppendFormat(" CI.SHAIN_CD = '{0}' ", SystemProperty.Shain.CD);
            sql.Append(" AND CI.DELETE_FLG = 0 ");

            var dt = this.denshiKeiyakuKihonDao.getdateforstringsql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.clientId = dr["DENSHI_KEIYAKU_CLIENT_ID"].ToString();
            }
        }

        /// <summary>
        /// 書類作成
        /// </summary>
        internal bool ShoruiCreate()
        {
            LogUtility.DebugMethodStart();

            // トークンを取得する。
            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_DOCUMENTS));
            var api = connect.URL;                  // /documents
            var contentType = connect.CONTENT_TYPE; // application/x-www-form-urlencoded

            REQ_DOCUMENTS_POST req = new REQ_DOCUMENTS_POST();
            req.client_id = this.clientId;
            req.title = this.form.SendTitle.Text;
            req.message = this.form.SendMessage.Text;

            //req.title = "タイトル";
            //req.note = "ノート";
            //req.message = "メッセージ";

            DOCUMENT_MODEL dto;
            var result = this.denshiLogic.HttpPOST<DOCUMENT_MODEL>(api, contentType, req, out dto);

            if (result)
            {
                // ドキュメントID保存
                this.documentId = dto.Id;

                this.denshiKihon = this.denshiKeiyakuKihonDao.GetDataByCd(this.systemId, this.denshiSystemId);

                // ドキュメントIDを電子契約基本データに保持
                this.denshiKihon.DOCUMENT_ID = dto.Id;
                // 契約状況を「0:下書き」に設定する。
                this.denshiKihon.KEIYAKU_JYOUKYOU = SqlInt32.Parse(dto.Status.ToString());

                // 更新者情報設定
                var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
                kihonDataBinderLogic.SetSystemProperty(this.denshiKihon, false);

                // テーブル更新
                try
                {
                    using (Transaction tran = new Transaction())
                    {
                        this.denshiKeiyakuKihonDao.Update(this.denshiKihon);
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("ShoruiCreate", ex);
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
            }

            return result;
        }

        /// <summary>
        /// ファイル追加
        /// </summary>
        /// <returns></returns>
        internal bool FileAdd()
        {
            LogUtility.DebugMethodStart();

            bool result = false;

            // トークンを取得する。
            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_FILES));
            var api = string.Format(connect.URL, this.documentId);   // /documents/{0}/files
            var contentType = connect.CONTENT_TYPE;             // multipart/form-data

            REQ_FILES_POST req = new REQ_FILES_POST();
            req.client_id = this.clientId;

            // 送付情報一覧でチェックONの行のファイルパスを取得して設定する。
            string fileName;
            DOCUMENT_MODEL dto = new DOCUMENT_MODEL();

            // ファイル種類CDの昇順ソート用に、ファイル種類CDとファイルパスをリストに格納する。
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                if (!row.IsNewRow
                    && bool.Parse(row.Cells["colCheckBox"].Value.ToString()))
                {
                    // 同一のキー（ファイルパス）があるか確認する。
                    bool keyUmu = list.ContainsKey(row.Cells["colFilePath"].Value.ToString());
                    if (keyUmu)
                    {
                        // 同一キーの場合、リストに追加しない。
                        continue;
                    }

                    list.Add(row.Cells["colFilePath"].Value.ToString(), int.Parse(row.Cells["colFileShuruiCD"].Value.ToString()));
                }
            }

            // ファイル種類CDの昇順でソートする。
            var orderList = list.OrderBy((x) => x.Value);

            // ソートしたリストをもとにファイルを追加する。
            foreach (var data in orderList)
            {
                // ファイルパス、ファイル名を取得する。
                string path = data.Key;
                fileName = Path.GetFileName(path);

                req.name = fileName;
                req.uploadfile = @path;

                //req.name = "なまえ";
                //req.uploadfile = @"C:\edison\\TEST.pdf";

                result = this.denshiLogic.HttpPOST<DOCUMENT_MODEL>(api, contentType, req, out dto);
                if (!result)
                {
                    break;
                }
            }

            // ファイルIDを設定する。
            if (result)
            {
                this.denshiSouhuinfo = this.denshiKeiyakuSouhuinfoDao.GetDataByCd(this.systemId, this.denshiSystemId);

                // チェックONのデータを抜き出す。
                var checkOnDatas = this.denshiSouhuinfo.Where(n => n.SEND_FLG.IsTrue);

                // チェックONのデータにファイルIDを設定する。
                foreach (var checkOnData in checkOnDatas)
                {
                    // チェックONのデータのファイルパスからファイル名を取得する。
                    fileName = Path.GetFileName(checkOnData.FILE_PATH);

                    // APIのファイル名と取得したファイル名が一致する場合、ファイルIDを設定する。
                    var file = dto.Files.Where(n => n.Name.Equals(fileName)).First();
                    checkOnData.FILE_ID = file.Id;
                }

                // チェックOFFのデータを抜き出し、ファイルIDを設定したチェックONのデータと結合する。
                var resultDatas = this.denshiSouhuinfo.Where(n => n.SEND_FLG.IsFalse)
                                                     .Concat(checkOnDatas)
                                                     .ToArray();

                this.denshiSouhuinfo = resultDatas;

                // テーブル更新
                try
                {
                    using (Transaction tran = new Transaction())
                    {
                        foreach (T_DENSHI_KEIYAKU_SOUHUINFO data in this.denshiSouhuinfo)
                        {
                            this.denshiKeiyakuSouhuinfoDao.Update(data);
                        }
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("FileAdd", ex);
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
            }

            return result;
        }

        /// <summary>
        /// 宛先追加
        /// </summary>
        /// <returns></returns>
        internal bool AtesakiAdd()
        {
            LogUtility.DebugMethodStart();

            bool result = false;

            // トークンを取得する。
            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_PARTICIPANTS));
            var api = string.Format(connect.URL, this.documentId);   // /documents/{0}/participants
            var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

            REQ_PARTICIPANTS_POST req = new REQ_PARTICIPANTS_POST();
            req.client_id = this.clientId;

            // 電子契約基本テーブル、電子契約送付先テーブルのデータをもとに、優先番号の順序で宛先を設定する。
            M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] souhusakiList = denshiKeiyakuSouhusakiDao.GetDataSortNo(this.systemId);
            DOCUMENT_MODEL dto = new DOCUMENT_MODEL();
            foreach (var data in souhusakiList)
            {
                req.email = data.MAIL_ADDRESS;
                req.name = data.SOUHU_TANTOUSHA_NAME;
                if (string.IsNullOrEmpty(data.SHAIN_CD))
                {
                    req.organization = data.KEIYAKUSAKI_CORP_NAME;
                }
                else
                {
                    req.organization = data.ATESAKI_NAME;
                }
                req.access_code = data.ACCESS_CD;
                req.language_code = "ja";

                //req.email = "ueyama@e-mall.co.jp";
                //req.name = "太郎";
                //req.organization = "えじそん";
                //req.access_code = "1234";
                //req.language_code = "ja";

                result = this.denshiLogic.HttpPOST<DOCUMENT_MODEL>(api, contentType, req, out dto);
                if (!result)
                {
                    break;
                }
            }

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// 共有先追加
        /// </summary>
        /// <returns></returns>
        internal bool KyoyusakiAdd()
        {
            LogUtility.DebugMethodStart();

            bool result = false;

            // トークンを取得する。
            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_REPORTEES));
            var api = string.Format(connect.URL, this.documentId);   // /documents/{0}/reportees
            var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

            REQ_REPORTEES_POST req = new REQ_REPORTEES_POST();
            req.client_id = this.clientId;

            DOCUMENT_MODEL dto = new DOCUMENT_MODEL();
            foreach (DataGridViewRow row in this.form.kyoyusakiIchiran.Rows)
            {
                if ((row.Cells["KYOYUSAKI_CORP_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_CORP_NAME"].Value.ToString()))
                    && (row.Cells["KYOYUSAKI_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_NAME"].Value.ToString()))
                    && (row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString()))
                    )
                {
                    continue;
                }
                // 削除チェックボックス=ONである明細行は登録除外
                if (row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    continue;
                }

                req.email = row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString();
                req.name = row.Cells["KYOYUSAKI_NAME"].Value.ToString();
                req.organization = row.Cells["KYOYUSAKI_CORP_NAME"].Value.ToString();

                result = this.denshiLogic.HttpPOST<DOCUMENT_MODEL>(api, contentType, req, out dto);
                if (!result)
                {
                    break;
                }
            }

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// 書類の送信
        /// </summary>
        /// <returns></returns>
        internal bool ShoruiSend()
        {
            LogUtility.DebugMethodStart();

            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_DOCUMENTID));
            var api = string.Format(connect.URL, this.documentId);   // /documents/{0}
            var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

            REQ_COMMON req = new REQ_COMMON();
            req.client_id = this.clientId;
            req.errMessage = "契約書送付中にエラーとなりました。\nシステム管理者へお問い合わせください。";
            DOCUMENT_MODEL dto;
            var result = this.denshiLogic.HttpPOST<DOCUMENT_MODEL>(api, contentType, req, out dto);

            if (result)
            {
                this.denshiKihon = this.denshiKeiyakuKihonDao.GetDataByCd(this.systemId, this.denshiSystemId);

                // 契約状況を「1:先方確認中」に設定する。
                this.denshiKihon.KEIYAKU_JYOUKYOU = SqlInt32.Parse(dto.Status.ToString());

                // 更新者情報設定
                var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
                kihonDataBinderLogic.SetSystemProperty(this.denshiKihon, false);

                // テーブル更新
                try
                {
                    using (Transaction tran = new Transaction())
                    {
                        this.denshiKeiyakuKihonDao.Update(this.denshiKihon);
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("ShoruiSend", ex);
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
            }

            return result;
        }

        /// <summary>
        /// 申請取消
        /// </summary>
        /// <returns></returns>
        internal bool ShinseiTorikeshi()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;

            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_DECLINE));
            var api = string.Format(connect.URL, this.denshiKihon.DOCUMENT_ID);   // /documents/{0}/decline
            var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

            REQ_DECLINE_PUT req = new REQ_DECLINE_PUT();
            req.client_id = this.clientId;

            DOCUMENT_MODEL dto;
            var result = this.denshiLogic.HttpPUT<DOCUMENT_MODEL>(api, contentType, req, out dto);
            if (result)
            {
                try
                {
                    // T_DENSHI_KEIYAKU_KIHONの契約状況を「3:取消、または却下」、削除フラグをtrueに設定する。
                    this.denshiKihon.KEIYAKU_JYOUKYOU = SqlInt32.Parse(dto.Status.ToString());
                    this.denshiKihon.DELETE_FLG = true;

                    // 更新者情報設定
                    var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
                    kihonDataBinderLogic.SetSystemProperty(this.denshiKihon, false);

                    // T_DENSHI_KEIYAKU_SOUHUINFOの削除フラグをtrueに設定する。
                    foreach (T_DENSHI_KEIYAKU_SOUHUINFO souhuInfo in this.denshiSouhuinfo)
                    {
                        souhuInfo.DELETE_FLG = true;

                        // 更新者情報設定
                        var souhuinfoDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_SOUHUINFO>(souhuInfo);
                        souhuinfoDataBinderLogic.SetSystemProperty(souhuInfo, false);
                    }

                    // 更新
                    using (Transaction tran = new Transaction())
                    {
                        // T_DENSHI_KEIYAKU_KIHON
                        this.denshiKeiyakuKihonDao.Update(this.denshiKihon);

                        // T_DENSHI_KEIYAKU_SOUHUINFO
                        foreach (T_DENSHI_KEIYAKU_SOUHUINFO souhuInfo in this.denshiSouhuinfo)
                        {
                            this.denshiKeiyakuSouhuinfoDao.Update(souhuInfo);
                        }

                        tran.Commit();
                        ret = true;
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("ShinseiTorikeshi", ex);
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
            }

            return ret;
        }

        /// <summary>
        /// 契約書状況取得
        /// </summary>
        /// <param name="num">1:[3]契約書状況取得、2:[F4]契約書取消</param>
        internal bool GetKeiyakuJyoukyou(int num)
        {
            LogUtility.DebugMethodStart();

            var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_DOCUMENTID));
            var api = string.Format(connect.URL, this.denshiKihon.DOCUMENT_ID);   // /documents/{0}
            var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

            REQ_COMMON req = new REQ_COMMON();
            req.client_id = this.clientId;
            req.errMessage = "契約状況取得中にエラーとなりました。\nシステム管理者へお問い合わせください。";

            DOCUMENT_MODEL dto;
            var result = this.denshiLogic.HttpGET<DOCUMENT_MODEL>(api, contentType, req, out dto);
            if (result)
            {
                this.form.keiyakuJyoukyouValue = dto.Status;

                if (num == 1)
                {
                    // 契約状況に設定する。
                    this.SetKeiyakuJyoukyou(dto.Status.ToString());

                    // 契約状況が「2:締結済」の場合、合意締結証明書ボタンを活性化する。
                    if (dto.Status.ToString().Equals("2"))
                    {
                        this.parentForm.bt_process4.Enabled = true;
                    }

                    try
                    {
                        this.denshiKihon.KEIYAKU_JYOUKYOU = SqlInt32.Parse(dto.Status.ToString());

                        // 更新者情報設定
                        var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
                        kihonDataBinderLogic.SetSystemProperty(this.denshiKihon, false);

                        using (Transaction tran = new Transaction())
                        {
                            // T_DENSHI_KEIYAKU_KIHON
                            this.denshiKeiyakuKihonDao.Update(this.denshiKihon);

                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Error("GetKeiyakuJyoukyou", ex);
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
                }
            }

            return result;
        }

        /// <summary>
        /// 画面上の契約状況に設定
        /// </summary>
        /// <param name="status"></param>
        private void SetKeiyakuJyoukyou(string status)
        {
            switch (status)
            {
                case "0":
                    this.form.KeiyakuJyoukyou.Text = "下書き";
                    break;
                case "1":
                    this.form.KeiyakuJyoukyou.Text = "先方確認中";
                    break;
                case "2":
                    this.form.KeiyakuJyoukyou.Text = "締結済";
                    break;
                case "3":
                    this.form.KeiyakuJyoukyou.Text = "取消、または却下";
                    break;
                case "4":
                    this.form.KeiyakuJyoukyou.Text = "テンプレート";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 画面データを電子契約関連テーブルに登録・更新する。
        /// </summary>
        internal bool UpdateDisplayData()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;

            bool newFlg = false;

            try
            {
                // 電子契約基本データ作成
                newFlg = this.CreateDenshiKihon(newFlg);

                // 電子契約送付情報データ作成
                this.CreateDenshiSouhuInfo();

                // 電子契約入力送付先データ作成
                this.CreateDenshiSouhusaki();

                // 電子契約契約情報データ作成
                this.CreateDenshiKeiyakuInfo();

                // 共有先データ作成
                this.CreateDenshiKyoyusaki();

                using (Transaction tran = new Transaction())
                {
                    // T_DENSHI_KEIYAKU_KIHONのデータを登録もしくは更新
                    if (newFlg)
                    {
                        // 登録
                        this.denshiKeiyakuKihonDao.Insert(this.denshiKihon);
                    }
                    else
                    {
                        // 更新
                        this.denshiKeiyakuKihonDao.Update(this.denshiKihon);
                    }

                    // T_DENSHI_KEIYAKU_SOUHUINFOのデータを削除
                    var sql = new StringBuilder();
                    sql.Append(" DELETE FROM T_DENSHI_KEIYAKU_SOUHUINFO");
                    sql.Append(" WHERE ");
                    sql.AppendFormat(" SYSTEM_ID = '{0}' ", this.systemId);
                    sql.AppendFormat(" AND DENSHI_KEIYAKU_SYSTEM_ID = '{0}' ", this.denshiSystemId);
                    this.denshiKeiyakuKihonDao.getdateforstringsql(sql.ToString());

                    // T_DENSHI_KEIYAKU_SOUHUINFOを登録
                    foreach (T_DENSHI_KEIYAKU_SOUHUINFO infoData in this.denshiSouhuinfo)
                    {
                        this.denshiKeiyakuSouhuinfoDao.Insert(infoData);
                    }

                    // T_DENSHI_KEIYAKU_SOUHUSAKIを登録
                    foreach (T_DENSHI_KEIYAKU_SOUHUSAKI sakiData in this.denshiNyuuryokuSouhusaki)
                    {
                        this.denshiKeiyakuNyuuryokuSouhuinfoDao.Insert(sakiData);
                    }

                    // T_DENSHI_KEIYAKU_KEIYAKUINFOを登録
                    foreach (T_DENSHI_KEIYAKU_KEIYAKUINFO keiyakuinfoData in this.denshiKeiyakuinfo)
                    {
                        this.denshiKeiyakuKeiyakuinfoDao.Insert(keiyakuinfoData);
                    }

                    // T_DENSHI_KEIYAKU_KYOYUSAKIを登録
                    foreach (T_DENSHI_KEIYAKU_KYOYUSAKI sakiData in this.denshiKeiyakuKyoyusaki)
                    {
                        this.denshiKeiyakuKyoyusakiDao.Insert(sakiData);
                    }

                    tran.Commit();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateDisplayData", ex);
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

        /// <summary>
        /// 電子契約基本データを作成する。
        /// </summary>
        /// <param name="newFlg"></param>
        /// <returns></returns>
        private bool CreateDenshiKihon(bool newFlg)
        {
            // 存在しない場合は作成⇒登録
            if (this.denshiKihon == null)
            {
                this.denshiKihon = new T_DENSHI_KEIYAKU_KIHON();

                // システムIDを設定
                this.denshiKihon.SYSTEM_ID = this.systemId;

                // 電子契約システムIDを付番して設定
                this.CreateSystemIdForDenshi();
                this.denshiKihon.DENSHI_KEIYAKU_SYSTEM_ID = this.denshiSystemId;

                newFlg = true;
            }

            // T_DENSHI_KEIYAKU_KIHONに設定
            this.denshiKihon.SHAIN_CD = SystemProperty.Shain.CD;
            this.denshiKihon.DENSHI_KEIYAKU_CLIENT_ID = this.clientId;
            this.denshiKihon.SEND_TITLE = this.form.SendTitle.Text;
            this.denshiKihon.SEND_MESSAGE = this.form.SendMessage.Text;
            this.denshiKihon.SHANAI_BIKO = this.form.SyanaiBiko.Text;
            this.denshiKihon.ACCESS_CD = this.denshiSouhusaki[0].ACCESS_CD;
            this.denshiKihon.KEIYAKU_NO = this.denshiItakuDataList[0].ITAKU_KEIYAKU_NO;
            this.denshiKihon.KOUSHIN_SHUBETSU = SqlInt16.Parse(this.denshiItakuDataList[0].KOUSHIN_SHUBETSU);
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].YUUKOU_BEGIN))
            {
                this.denshiKihon.YUUKOU_BEGIN = SqlDateTime.Parse(this.denshiItakuDataList[0].YUUKOU_BEGIN);
            }
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].YUUKOU_END))
            {
                this.denshiKihon.YUUKOU_END = SqlDateTime.Parse(this.denshiItakuDataList[0].YUUKOU_END);
            }
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KOUSHIN_END_DATE))
            {
                this.denshiKihon.KOUSHIN_END_DATE = SqlDateTime.Parse(this.denshiItakuDataList[0].KOUSHIN_END_DATE);
            }
            this.denshiKihon.HAISHUTSU_JIGYOUSHA_CD = this.denshiItakuDataList[0].HAISHUTSU_JIGYOUSHA_CD;
            this.denshiKihon.HAISHUTSU_JIGYOUSHA_NAME = this.keiyakuInfoDataList[0].KIHON_GYOUSHA_NAME_RYAKU;
            this.denshiKihon.HAISHUTSU_JIGYOUJOU_CD = this.denshiItakuDataList[0].HAISHUTSU_JIGYOUJOU_CD;
            this.denshiKihon.BIKOU1 = this.denshiItakuDataList[0].BIKOU1;
            this.denshiKihon.BIKOU2 = this.denshiItakuDataList[0].BIKOU2;
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KEIYAKUSHO_KEIYAKU_DATE))
            {
                this.denshiKihon.KEIYAKUSHO_KEIYAKU_DATE = SqlDateTime.Parse(this.denshiItakuDataList[0].KEIYAKUSHO_KEIYAKU_DATE);
            }
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KEIYAKUSHO_CREATE_DATE))
            {
                this.denshiKihon.KEIYAKUSHO_CREATE_DATE = SqlDateTime.Parse(this.denshiItakuDataList[0].KEIYAKUSHO_CREATE_DATE);
            }
            if (!string.IsNullOrEmpty(this.denshiItakuDataList[0].KEIYAKUSHO_SEND_DATE))
            {
                this.denshiKihon.KEIYAKUSHO_SEND_DATE = SqlDateTime.Parse(this.denshiItakuDataList[0].KEIYAKUSHO_SEND_DATE);
            }
            this.denshiKihon.KOBETSU_SHITEI_CHECK = this.denshiItakuDataList[0].KOBETSU_SHITEI_CHECK;

            // 更新者情報設定
            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
            dataBinderLogic.SetSystemProperty(this.denshiKihon, false);

            return newFlg;
        }

        /// <summary>
        /// 電子契約システムID採番
        /// </summary>
        private void CreateSystemIdForDenshi()
        {
            // T_DENSHI_KEIYAKU_KIHONの電子契約システムIDを採番する。
            var sql = new StringBuilder();
            sql.Append(" select ");
            sql.Append(" MAX(DENSHI_KEIYAKU_SYSTEM_ID) as DENSHI_KEIYAKU_SYSTEM_ID ");
            sql.Append(" from T_DENSHI_KEIYAKU_KIHON ");

            // 委託契約書のシステムIDに合致する電子契約システムIDがある場合、インクリメントして設定。
            // ない場合、新たに1を設定。
            var dt = this.denshiKeiyakuKihonDao.getdateforstringsql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string maxDenshiSystemID = dr["DENSHI_KEIYAKU_SYSTEM_ID"].ToString();

                if (!string.IsNullOrEmpty(maxDenshiSystemID))
                {
                    this.denshiSystemId = String.Format("{0:D9}", Int64.Parse(maxDenshiSystemID) + 1);
                }
                else
                {
                    this.denshiSystemId = String.Format("{0:D9}", 1);
                }
            }
            else
            {
                this.denshiSystemId = String.Format("{0:D9}", 1);
            }
        }

        /// <summary>
        /// 電子契約送付情報の設定
        /// </summary>
        private void CreateDenshiSouhuInfo()
        {
            List<T_DENSHI_KEIYAKU_SOUHUINFO> list = new List<T_DENSHI_KEIYAKU_SOUHUINFO>();
            int seq = 1;

            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                if (!this.form.customDataGridView1.Rows[i].IsNewRow
                    && !this.IsAllNullOrEmpty(this.form.customDataGridView1.Rows[i]))
                {
                    T_DENSHI_KEIYAKU_SOUHUINFO souhuinfo = new T_DENSHI_KEIYAKU_SOUHUINFO();
                    souhuinfo.SYSTEM_ID = this.systemId;
                    souhuinfo.DENSHI_KEIYAKU_SYSTEM_ID = this.denshiSystemId;
                    souhuinfo.SEQ = seq++;

                    souhuinfo.SEND_FLG = bool.Parse(this.form.customDataGridView1.Rows[i].Cells["colCheckBox"].Value.ToString());
                    if (this.form.customDataGridView1.Rows[i].Cells["colFileShuruiCD"].Value != null)
                    {
                        souhuinfo.FILE_SHURUI_CD = this.form.customDataGridView1.Rows[i].Cells["colFileShuruiCD"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colFileShuruiName"].Value != null)
                    {
                        souhuinfo.FILE_SHURUI_NAME = this.form.customDataGridView1.Rows[i].Cells["colFileShuruiName"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colGyousha"].Value != null)
                    {
                        souhuinfo.GYOUSHA_NAME = this.form.customDataGridView1.Rows[i].Cells["colGyousha"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colGenba"].Value != null)
                    {
                        souhuinfo.GENBA_NAME = this.form.customDataGridView1.Rows[i].Cells["colGenba"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colChiikiName"].Value != null)
                    {
                        souhuinfo.CHIIKI_NAME = this.form.customDataGridView1.Rows[i].Cells["colChiikiName"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colKyokaNo"].Value != null)
                    {
                        souhuinfo.KYOKA_NO = this.form.customDataGridView1.Rows[i].Cells["colKyokaNo"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colBiko"].Value != null)
                    {
                        souhuinfo.BIKO = this.form.customDataGridView1.Rows[i].Cells["colBiko"].Value.ToString();
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["colFilePath"].Value != null)
                    {
                        souhuinfo.FILE_PATH = this.form.customDataGridView1.Rows[i].Cells["colFilePath"].Value.ToString();
                    }

                    // 更新者情報設定
                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_SOUHUINFO>(souhuinfo);
                    dataBinderLogic.SetSystemProperty(souhuinfo, false);

                    // リストに追加
                    list.Add(souhuinfo);
                }
            }
            if (list.Count != 0)
            {
                this.denshiSouhuinfo = list.ToArray();
            }
        }

        /// <summary>
        /// 電子契約送付先の設定
        /// </summary>
        private void CreateDenshiSouhusaki()
        {
            List<T_DENSHI_KEIYAKU_SOUHUSAKI> list = new List<T_DENSHI_KEIYAKU_SOUHUSAKI>();

            for (int i = 0; i < this.form.denshiKeiyakuIchiran.Rows.Count; i++)
            {
                T_DENSHI_KEIYAKU_SOUHUSAKI souhusaki = new T_DENSHI_KEIYAKU_SOUHUSAKI();
                souhusaki.SYSTEM_ID = this.systemId;
                souhusaki.DENSHI_KEIYAKU_SYSTEM_ID = this.denshiSystemId;

                souhusaki.PRIORITY_NO = SqlInt16.Parse(this.form.denshiKeiyakuIchiran.Rows[i].Cells["YUUSEN_NO"].Value.ToString());
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_CD"].Value != null)
                {
                    souhusaki.SHAIN_CD = this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_CD"].Value.ToString();
                }
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_NAME"].Value != null)
                {
                    souhusaki.SHAIN_NAME = this.form.denshiKeiyakuIchiran.Rows[i].Cells["SHAIN_NAME"].Value.ToString();
                }
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value != null)
                {
                    souhusaki.MAIL_ADDRESS = this.form.denshiKeiyakuIchiran.Rows[i].Cells["MAIL_ADDRESS"].Value.ToString();
                }
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["TEL_NO"].Value != null)
                {
                    souhusaki.TEL_NO = this.form.denshiKeiyakuIchiran.Rows[i].Cells["TEL_NO"].Value.ToString();
                }
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["ATESAKI_NAME"].Value != null)
                {
                    souhusaki.ATESAKI_NAME = this.form.denshiKeiyakuIchiran.Rows[i].Cells["ATESAKI_NAME"].Value.ToString();
                }
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["BUSHO_NAME"].Value != null)
                {
                    souhusaki.BUSHO_NAME = this.form.denshiKeiyakuIchiran.Rows[i].Cells["BUSHO_NAME"].Value.ToString();
                }
                if (this.form.denshiKeiyakuIchiran.Rows[i].Cells["SOUHUSAKI_BIKO"].Value != null)
                {
                    souhusaki.SOUHUSAKI_BIKO = this.form.denshiKeiyakuIchiran.Rows[i].Cells["SOUHUSAKI_BIKO"].Value.ToString();
                }
                
                // 更新者情報設定
                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_SOUHUSAKI>(souhusaki);
                dataBinderLogic.SetSystemProperty(souhusaki, false);

                // リストに追加
                list.Add(souhusaki);
            }
            if (list.Count != 0)
            {
                this.denshiNyuuryokuSouhusaki = list.ToArray();
            }
        }

        /// <summary>
        /// 電子契約契約情報の設定
        /// </summary>
        private void CreateDenshiKeiyakuInfo()
        {
            List<T_DENSHI_KEIYAKU_KEIYAKUINFO> list = new List<T_DENSHI_KEIYAKU_KEIYAKUINFO>();
            int seq = 1;

            for (int i = 0; i < this.form.customDataGridView2.Rows.Count; i++)
            {
                if (!this.form.customDataGridView2.Rows[i].IsNewRow)
                {
                    T_DENSHI_KEIYAKU_KEIYAKUINFO keiyakuInfo = new T_DENSHI_KEIYAKU_KEIYAKUINFO();
                    keiyakuInfo.SYSTEM_ID = this.systemId;
                    keiyakuInfo.DENSHI_KEIYAKU_SYSTEM_ID = this.denshiSystemId;
                    keiyakuInfo.SEQ = seq++;

                    if (this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouCD"].Value != null)
                    {
                        keiyakuInfo.HAISHUTSU_JIGYOUJOU_CD = this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouCD"].Value.ToString();
                    }
                    if (this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouName"].Value != null)
                    {
                        keiyakuInfo.HAISHUTSU_JIGYOUJOU_NAME = this.form.customDataGridView2.Rows[i].Cells["colHaishutsuJigyoujyouName"].Value.ToString();
                    }
                    if (this.form.customDataGridView2.Rows[i].Cells["colTodouhukenName"].Value != null)
                    {
                        keiyakuInfo.TODOUFUKEN_NAME = this.form.customDataGridView2.Rows[i].Cells["colTodouhukenName"].Value.ToString();
                    }
                    if (this.form.customDataGridView2.Rows[i].Cells["colJuusho1"].Value != null)
                    {
                        keiyakuInfo.HAISHUTSU_JIGYOUJOU_ADDRESS1 = this.form.customDataGridView2.Rows[i].Cells["colJuusho1"].Value.ToString();
                    }
                    if (this.form.customDataGridView2.Rows[i].Cells["colJuusho2"].Value != null)
                    {
                        keiyakuInfo.HAISHUTSU_JIGYOUJOU_ADDRESS2 = this.form.customDataGridView2.Rows[i].Cells["colJuusho2"].Value.ToString();
                    }

                    // 更新者情報設定
                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KEIYAKUINFO>(keiyakuInfo);
                    dataBinderLogic.SetSystemProperty(keiyakuInfo, false);

                    // リストに追加
                    list.Add(keiyakuInfo);
                }
            }
            if (list.Count != 0)
            {
                this.denshiKeiyakuinfo = list.ToArray();
            }
        }

        /// <summary>
        /// 部分更新
        /// </summary>
        internal bool BubunUpdate()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;

            try
            {
                // 部分更新用のデータを作成
                this.CreateBuBunUpdateData();

                using (Transaction tran = new Transaction())
                {
                    // T_DENSHI_KEIYAKU_KIHONを更新
                    this.denshiKeiyakuKihonDao.Update(this.denshiKihon);

                    // T_DENSHI_KEIYAKU_SOUHUINFOを更新
                    foreach (T_DENSHI_KEIYAKU_SOUHUINFO infoData in this.denshiSouhuinfo)
                    {
                        this.denshiKeiyakuSouhuinfoDao.Update(infoData);
                    }

                    tran.Commit();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("BubunUpdate", ex);
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

        /// <summary>
        /// 部分更新のデータを作成
        /// </summary>
        private void CreateBuBunUpdateData()
        {
            // 社内備考をセット
            this.denshiKihon.SHANAI_BIKO = this.form.SyanaiBiko.Text;

            // 更新者情報設定
            var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
            kihonDataBinderLogic.SetSystemProperty(this.denshiKihon, false);

            // 送付情報一覧の備考を設定
            if (this.denshiSouhuinfo.Length > 0)
            {
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells["colBiko"].Value == null
                        || string.IsNullOrEmpty(this.form.customDataGridView1.Rows[i].Cells["colBiko"].Value.ToString()))
                    {
                        this.denshiSouhuinfo[i].BIKO = null;
                    }
                    else
                    {
                        this.denshiSouhuinfo[i].BIKO = this.form.customDataGridView1.Rows[i].Cells["colBiko"].Value.ToString();
                    }

                    // 更新者情報設定
                    var souhuinfoDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_SOUHUINFO>(this.denshiSouhuinfo[i]);
                    souhuinfoDataBinderLogic.SetSystemProperty(this.denshiSouhuinfo[i], false);
                }
            }

        }

        /// <summary>
        /// 電子契約関連ファイルの出力ダイアログ処理
        /// </summary>
        public string OutputDenshikeiyakuFile()
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

            if (string.IsNullOrWhiteSpace(directoryName))
            {
                return "";
            }

            return directoryName;
        }

        /// <summary>
        /// 契約書ダウンロード
        /// </summary>
        internal bool FileDownload()
        {
            bool ret = false;
            bool notTarget = false;
            string dir = "";

            if (this.msgLogic.MessageBoxShowConfirm("契約書をダウンロードしてよろしいですか？") == DialogResult.Yes)
            {
                // 共通部品ダイアログを使用する。
                dir = this.OutputDenshikeiyakuFile();

                if (!string.IsNullOrEmpty(dir))
                {
                    for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
                    {
                        DataGridViewRow row = this.form.customDataGridView1.Rows[i];

                        // ダウンロードのチェックがONである場合
                        if (bool.Parse(row.Cells["colCheckBox"].Value.ToString()))
                        {
                            var documentId = this.denshiKihon.DOCUMENT_ID;
                            string fileID = "";
                            // ファイルパスからファイルIDを取得する。
                            for (int j = 0; j < this.denshiSouhuinfo.Length; j++)
                            {
                                if (row.Cells["colFilePath"].Value.ToString().Equals(this.denshiSouhuinfo[j].FILE_PATH))
                                {
                                    fileID = this.denshiSouhuinfo[j].FILE_ID;
                                }
                            }

                            // 送付していない（ファイルIDが設定されていない）場合、ダウンロード対象外とする。
                            if (string.IsNullOrEmpty(fileID))
                            {
                                notTarget = true;
                                continue;
                            }

                            // トークンを取得する。
                            var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_FILEID);
                            var api = string.Format(connect.URL, documentId, fileID);   // /documents/{0}/files/{1}
                            var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                            REQ_COMMON req = new REQ_COMMON();
                            req.client_id = this.clientId;
                            req.errMessage = "契約書ダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                            var fileName = Path.GetFileName(row.Cells["colFilePath"].Value.ToString());
                            var filePath = dir + "/" + fileName;
                            var createFilePath = @filePath;
                            //createFilePath = @"C:\edison\download.pdf";

                            bool downLoadResult = this.denshiLogic.HttpGET_DownLoadPDF(api, contentType, req, createFilePath);
                            if (downLoadResult)
                            {
                                ret = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    if (notTarget)
                    {
                        this.msgLogic.MessageBoxShowWarn("送付していないファイルはダウンロード対象外となります。");
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 合意締結証明書ダウンロード
        /// </summary>
        internal bool CertificateDownload()
        {
            bool ret = false;
            string dir = "";

            if (this.msgLogic.MessageBoxShowConfirm("合意締結証明書をダウンロードしてよろしいですか？") == DialogResult.Yes)
            {
                // 共通部品ダイアログを使用する。
                dir = this.OutputDenshikeiyakuFile();

                if (!string.IsNullOrEmpty(dir))
                {
                    var documentId = this.denshiKihon.DOCUMENT_ID;

                    // トークンを取得する。
                    var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_CERTIFICATE);
                    var api = string.Format(connect.URL, documentId);   // /documents/{0}/certificate
                    var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                    REQ_COMMON req = new REQ_COMMON();
                    req.client_id = this.clientId;
                    req.errMessage = "契約書ダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                    var filePath = dir;
                    // ドキュメントIDをファイル名にする。
                    var createFilePath = @filePath + "/" + documentId + ".pdf";

                    bool downLoadResult = this.denshiLogic.HttpGET_DownLoadPDF(api, contentType, req, createFilePath);
                    if (downLoadResult)
                    {
                        ret = true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return ret;
        }

        /// <summary>
        /// 一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void CellEnter(int columnIndex)
        {
            string cellName = this.form.customDataGridView1.Columns[columnIndex].Name;

            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }

            // IME制御
            switch (cellName)
            {
                case "colFileShuruiCD":
                    this.form.customDataGridView1.ImeMode = ImeMode.Disable;
                    break;

                default:
                    this.form.customDataGridView1.ImeMode = ImeMode.Hiragana;
                    break;
            }
        }

        /// <summary>
        /// 有効行であるか判断する。
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsAllNullOrEmpty(DataGridViewRow row)
        {
            if ((row.Cells["colFileShuruiCD"].Value == null || string.IsNullOrEmpty(row.Cells["colFileShuruiCD"].Value.ToString()))
                && (row.Cells["colBiko"].Value == null || string.IsNullOrEmpty(row.Cells["colBiko"].Value.ToString()))
                && (row.Cells["colFilePath"].Value == null || string.IsNullOrEmpty(row.Cells["colFilePath"].Value.ToString()))
                )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// クライアントIDが設定されているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool CheckClientId()
        {
            // クライアントIDを取得する。
            this.GetClientID();

            // クライアントIDが取得できない場合、エラーとする。
            if (this.clientId == null || string.IsNullOrEmpty(this.clientId))
            {
                this.msgLogic.MessageBoxShowError("クライアントIDの登録が行われていません。クラウドサインとの連携処理を中断します。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 社員CDとクライアントIDが合致しているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool MatchingClientId()
        {
            // 作成者のクライアントID != ログイン者のクライアントIDの場合、エラーとする。
            if (!this.denshiKihon.DENSHI_KEIYAKU_CLIENT_ID.Equals(this.clientId))
            {
                this.msgLogic.MessageBoxShowError("電子契約を行った担当者のみ、電子契約書の取消が行えます。");
                return false;
            }

            // 作成者のクライアントID = ログイン者のクライアントID かつ 作成者の社員CD != ログイン者の社員CDの場合、確認ダイアログを表示する。
            if (this.denshiKihon.DENSHI_KEIYAKU_CLIENT_ID.Equals(this.clientId)
                && !this.denshiKihon.SHAIN_CD.Equals(SystemProperty.Shain.CD))
            {
                DialogResult result = this.msgLogic.MessageBoxShowConfirm("電子契約を行った担当者と異なる社員CDです。電子契約の取消を行いますか。");
                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 共有先追加テキスト変更
        /// </summary>
        internal void KyoyusakiTsuika_TextChanged(string kyoyusakiCC)
        {
            this.form.kyoyusakiIchiran.Rows.Clear();

            //Entityの値を表示
            if (kyoyusakiCC == "1")
            {
                for (int i = 0; i < this.kyoyusaki.Length; i++)
                {
                    this.form.kyoyusakiIchiran.Rows.Add();
                    this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_YUUSEN_NO"].Value = i + 1;

                    if (!string.IsNullOrEmpty(this.kyoyusaki[i].KYOYUSAKI_CORP_NAME))
                    {
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_CORP_NAME"].Value = this.kyoyusaki[i].KYOYUSAKI_CORP_NAME;
                    }
                    if (!string.IsNullOrEmpty(this.kyoyusaki[i].KYOYUSAKI_NAME))
                    {
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_NAME"].Value = this.kyoyusaki[i].KYOYUSAKI_NAME;
                    }
                    if (!string.IsNullOrEmpty(this.kyoyusaki[i].KYOYUSAKI_MAIL_ADDRESS))
                    {
                        this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value = this.kyoyusaki[i].KYOYUSAKI_MAIL_ADDRESS; ;
                    }
                }

                this.form.kyoyusakiIchiran.AllowUserToAddRows = true;
            }
            else
            {
                this.form.kyoyusakiIchiran.AllowUserToAddRows = false;
            }
        }

        /// <summary>
        /// 共有先の設定
        /// </summary>
        internal bool CheckKyoyusaki()
        {

            bool corpNameErr = false;
            bool shainNameErr = false;
            bool addressErr = false;
            string message = "";

            foreach (DataGridViewRow row in this.form.kyoyusakiIchiran.Rows)
            {
                if ((row.Cells["KYOYUSAKI_CORP_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_CORP_NAME"].Value.ToString()))
                    && (row.Cells["KYOYUSAKI_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_NAME"].Value.ToString()))
                    && (row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString()))
                    )
                {
                    continue;
                }
                // 共有先会社名
                if (row.Cells["KYOYUSAKI_CORP_NAME"].Value == null)
                {
                    corpNameErr = true;
                }
                // 共有先氏名
                if (row.Cells["KYOYUSAKI_NAME"].Value == null)
                {
                    shainNameErr = true;
                }
                // メールアドレス
                if (row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value == null)
                {
                    addressErr = true;
                }
            }
            if (corpNameErr)
            {
                message = message + "会社名は必須項目です。入力してください。\n";
            }
            if (shainNameErr)
            {
                message = message + "氏名は必須項目です。入力してください。\n";
            }
            if (addressErr)
            {
                message = message + "メールアドレスは必須項目です。入力してください。\n";
            }
            if (!string.IsNullOrEmpty(message))
            {
                this.msgLogic.MessageBoxShowError(message);
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 共有先の設定
        /// </summary>
        private void CreateDenshiKyoyusaki()
        {
            List<T_DENSHI_KEIYAKU_KYOYUSAKI> list = new List<T_DENSHI_KEIYAKU_KYOYUSAKI>();
            Int16 kyoyusakiNo = 0;
            for (int i = 0; i < this.form.kyoyusakiIchiran.Rows.Count; i++)
            {
                T_DENSHI_KEIYAKU_KYOYUSAKI kyoyusaki = new T_DENSHI_KEIYAKU_KYOYUSAKI();
                kyoyusaki.SYSTEM_ID = this.systemId;
                kyoyusaki.DENSHI_KEIYAKU_SYSTEM_ID = this.denshiSystemId;

                DataGridViewRow row = this.form.kyoyusakiIchiran.Rows[i];

                if ((row.Cells["KYOYUSAKI_CORP_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_CORP_NAME"].Value.ToString()))
                    && (row.Cells["KYOYUSAKI_NAME"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_NAME"].Value.ToString()))
                    && (row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value == null || string.IsNullOrEmpty(row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString()))
                    )
                {
                    continue;
                }
                // 削除チェックボックス=ONである明細行は登録除外
                if (row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    continue;
                }
                kyoyusakiNo ++;
                kyoyusaki.PRIORITY_NO = kyoyusakiNo;
                if (row.Cells["KYOYUSAKI_CORP_NAME"].Value != null)
                {
                    kyoyusaki.KYOYUSAKI_CORP_NAME = row.Cells["KYOYUSAKI_CORP_NAME"].Value.ToString();
                }
                if (row.Cells["KYOYUSAKI_NAME"].Value != null)
                {
                    kyoyusaki.KYOYUSAKI_NAME = row.Cells["KYOYUSAKI_NAME"].Value.ToString();
                }
                if (row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value != null)
                {
                    kyoyusaki.KYOYUSAKI_MAIL_ADDRESS = row.Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString();
                }

                // 更新者情報設定
                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KYOYUSAKI>(kyoyusaki);
                dataBinderLogic.SetSystemProperty(kyoyusaki, false);

                // リストに追加
                list.Add(kyoyusaki);
            }
            if (list.Count != 0)
            {
                this.denshiKeiyakuKyoyusaki = list.ToArray();
            }
        }

        /// <summary>
        /// メールアドレスの重複チェック
        /// </summary>
        /// <returns></returns>
        internal bool DuplicationCheck(string address, out bool catchErr)
        {
            catchErr = true;
            try
            {
                // 画面でメールアドレス重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.kyoyusakiIchiran.Rows.Count - 1; i++)
                {
                    string strCD1 = string.Empty;
                    if (this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value != null)
                    {
                        strCD1 = this.form.kyoyusakiIchiran.Rows[i].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString();
                    }
                    if (strCD1.Equals(address))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
            }
            return false;
        }

        /// <summary>
        /// メールアドレスの重複チェック（承認経路）
        /// </summary>
        /// <param name="address"></param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool DuplicationCheckApprovalRoute(string address, out bool catchErr)
        {
            catchErr = true;
            try
            {
                // 委託契約書画面-電子契約タブからメールアドレス重複チェック（承認経路）
                int recCount = 0;
                for (int i = 0; i < this.denshiSouhusaki.Length; i++)
                {
                    string strCD2 = string.Empty;
                    if (this.denshiSouhusaki[i].MAIL_ADDRESS != null)
                    {
                        strCD2 = this.denshiSouhusaki[i].MAIL_ADDRESS.ToString();
                    }
                    if (strCD2.Equals(address))
                    {
                        recCount++;
                    }
                }

                if (recCount > 0)
                {
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheckApprovalRoute", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheckApprovalRoute", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
            }
            return false;
        }

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
    }
}
