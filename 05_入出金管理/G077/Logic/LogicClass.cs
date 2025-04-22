using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Data;
using System.Reflection;
using Seasar.Quill.Attrs;
using System.Windows.Forms;
using System.Data.SqlTypes;
using Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.DAO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.FormManager;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku
{
    /// <summary>
    /// ビジネスロジック 
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        #region 定数

        // ボタン定義ファイルパス
        private string ButtonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Setting.ButtonSetting.xml";

        #endregion

        #region コントローラなどの変数

        // システムID自動取得クラス
        private DBAccessor CommonDBAccessor;

        // メッセージボックス
        MessageBoxShowLogic MessageShowLogic;

        // 画面モード
        private int Gamen_Mode;

        // headerのコントローラ
        public HeaderSample Header;

        //各ボタンのコントローラ
        public BusinessBaseForm ParentForm;

        // Form
        private UIForm Form;

        #endregion

        #region 一時データを保存する変数

        // 消込一覧更新用変数
        T_NYUUKIN_KESHIKOMI Kesikomi_data_new;

        // 消込一覧更新用変数
        T_NYUUKIN_KESHIKOMI Kesikomi_data_old;

        // 明細更新用LIST変数(追加した情報)
        List<T_NYUUKIN_DETAIL> List_meisai_data_new;

        // header更新用変数
        T_NYUUKIN_ENTRY Insert_entry;

        // header元のデータを保存する
        T_NYUUKIN_ENTRY Old_insert_entry;

        // 消込一覧更新用LIST変数(元の情報)
        List<T_NYUUKIN_KESHIKOMI> List_kesikomi_data_old;

        // 消込一覧更新用LIST変数(元の情報)
        List<T_NYUUKIN_KESHIKOMI> List_kesikomi_data_new;

        // 明細更新用変数
        T_NYUUKIN_DETAIL Meisai_data;

        // 入金番号がある場合の元のSYSTEM_ID
        public String System_Id;

        // 引数の入金番号
        public String Nyuukin_Number;

        // 枝番変数
        public String Seq;

        // 変更される前の消込一覧の値
        public String OldNyukingaku;

        // 消込初期値示フラグ
        internal Boolean First_Kesikomi_Flg = true;

        // 日付の変更フラグ
        internal Boolean IsDateChanged = false;

        //取引先変更フラグ
        internal Boolean IsTorihikiChanged = false;

        #endregion

        #region DataTable変数

        // 検索結果(header部分)
        public DataTable SearchResult_header { get; set; }

        // 検索結果(明細部分)
        public DataTable SearchResult_torihiki { get; set; }

        // 検索結果(明細部分)
        public DataTable SearchResult_detail { get; set; }

        // 検索結果(前回御請求額)
        public DataTable SearchResult_Zenkai { get; set; }

        // 検索結果(締処理状況)
        public DataTable SearchResult_ShoriChoukyou { get; set; }

        // 検索結果(開始売掛)
        public DataTable SearchResult_kaishi_urikake { get; set; }

        // 検索結果(消込明細)
        public DataTable SearchResult_kesikomi { get; set; }

        // 検索結果(Max消込SEQ)
        public DataTable SearchResult_MaxKesikomiSeq { get; set; }

        // 検索結果(締処理中)
        public DataTable SearchResult_ShimeChuu { get; set; }

        // 検索結果(締済み結果)
        public DataTable SearchResult_zumi { get; set; }

        // 検索結果(消込請求NO)
        public DataTable SearchResult_Kesikomi_Seq { get; set; }

        #endregion

        #region Dao変数

        // 入金入力HEADERのDao
        public DAOClass_header Dao_header;

        // 開始残高のDao
        public DAOClass_Kaishi Dao_Kaishi;

        // 取引先のマスタ情報
        public DAOClass_Torihiki Dao_torihiki;

        // 入金入力明細のDao
        public DAOClass_meisai Dao_meisai;

        // 締処理の判断Dao
        public DAOClass_Shimeshori Dao_Shimeshori;

        // 締済みの判断Dao
        public DAOClass_Shimezumi Dao_Shimezumi;

        // 拠点チェックDAO
        public DAOClass_CheckKyoten Dao_CheckKyoten;

        // 取引先チェックDAO
        public DAOClass_CheckTorihikisaki Dao_CheckTorihikisaki;

        // 営業担当者チェックDAO
        public DAOClass_CheckShain Dao_CheckShain;

        // 入金入力明細のDao
        public DAOClass_Kesikomi Dao_Kesikomi;

        // 入金入力明細のDao
        public Dao_MaxKesikomiSeq Dao_MaxKesikomiSeq;

        // 入金入力明細のDao
        public DAOClass_Zenkai Dao_Zenkai;

        // Popup営業担当者
        public DAOClass_PopupEikyouTantou Dao_PopupEikyouTantou;

        // Popup取引先CD
        public DAOClass_PopupTorihikisaki Dao_PopupTorihikisaki;

        // Popup取引先CD
        public DAOClass_PopupKyoten Dao_PopupKyoten;

        // Popup取引先CD
        public DAOClass_PopupNyushuukkin_Kbn Dao_PopupNyuushukkin_Kbn;

        // Popup取引先CD
        public DAOClass_CheckNyuushukinKbn Dao_CheckNyuushukinKbn;

        // Popup取引先CD
        public Dao_Seikyuu_Number Dao_Seikyuu_Number;

        // 拠点マスタ	
        private IM_KYOTENDao MkyotenDao;

        #endregion

        #endregion

        #region コンストラクタ

        // コンストラクタ
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.Form = targetForm;
            this.Dao_header = DaoInitUtility.GetComponent<DAO.DAOClass_header>();
            this.Dao_Kaishi = DaoInitUtility.GetComponent<DAO.DAOClass_Kaishi>();
            this.Dao_torihiki = DaoInitUtility.GetComponent<DAO.DAOClass_Torihiki>();
            this.Dao_meisai = DaoInitUtility.GetComponent<DAO.DAOClass_meisai>();
            this.Dao_Shimeshori = DaoInitUtility.GetComponent<DAO.DAOClass_Shimeshori>();
            this.Dao_Shimezumi = DaoInitUtility.GetComponent<DAO.DAOClass_Shimezumi>();
            this.Dao_Kesikomi = DaoInitUtility.GetComponent<DAO.DAOClass_Kesikomi>();
            this.Dao_Zenkai = DaoInitUtility.GetComponent<DAO.DAOClass_Zenkai>();
            this.MkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.Dao_MaxKesikomiSeq = DaoInitUtility.GetComponent<Dao_MaxKesikomiSeq>();
            this.Dao_Seikyuu_Number = DaoInitUtility.GetComponent<Dao_Seikyuu_Number>();
            this.Dao_CheckKyoten = DaoInitUtility.GetComponent<DAOClass_CheckKyoten>();
            this.Dao_CheckTorihikisaki = DaoInitUtility.GetComponent<DAOClass_CheckTorihikisaki>();
            this.Dao_CheckShain = DaoInitUtility.GetComponent<DAOClass_CheckShain>();
            this.Dao_CheckNyuushukinKbn = DaoInitUtility.GetComponent<DAOClass_CheckNyuushukinKbn>();
            this.MessageShowLogic = new MessageBoxShowLogic();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region headerのコントロールを設定する。

        /// <summary>
        /// header設定
        /// </summary>
        /// /// <returns></returns>
        public void SetHeader(HeaderSample hs)
        {
            LogUtility.DebugMethodStart(hs);
            this.Header = hs;
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                //各項目のロストフォカスイベント初期化
                LostfocusInit();

                //各項目のPopup画面のデータソースを設定
                PopupInit();

                // 各ボタンのイベントの初期化処理
                this.EventInit();

                //初期表示
                if (!this.FirstSetInit())
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //this.form.DENPYOU_DATE.Value = System.DateTime.Now;
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        public void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.Form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.Form.WindowType);

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
        /// ボタンイベントの初期化処理
        /// </summary>
        internal void LostfocusInit()
        {
            LogUtility.DebugMethodStart();
            this.Form.DENPYOU_DATE.LostFocus += new EventHandler(this.Form.DENPYOU_DATE_LostFocus);

            this.Form.TORIHIKISAKI_NO.LostFocus += new EventHandler(this.Form.TORIHIKISAKI_NO_LostFocus);

            this.Header.KYOTEN_CD.LostFocus += new EventHandler(Kyoten_LostFocus);
            this.Header.KYOTEN_CD.Validated += new EventHandler(Kyoten_Validated);

            this.Form.EIGYOUTANTOU_NO.LostFocus += new EventHandler(Eigyou_LostFocus);
            this.Form.EIGYOUTANTOU_NO.Validated += new EventHandler(Eigyou_Validated);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// popupイベント初期化
        /// </summary>
        public bool PopupInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 営業担当者
                this.Form.EIGYOUTANTOU_NO.PopupWindowId = WINDOW_ID.M_SHAIN;
                this.Form.EigyouPopupOpenButton.PopupWindowId = WINDOW_ID.M_SHAIN;
                // ポップアップに表示するデータ列(物理名)
                this.Form.EIGYOUTANTOU_NO.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
                this.Form.EigyouPopupOpenButton.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
                // 表示用データ取得＆加工
                var ShainDataTable = this.GetPopUpShainData(this.Form.EIGYOUTANTOU_NO.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // 列名とデータソース設定
                this.Form.EIGYOUTANTOU_NO.PopupDataHeaderTitle = new string[] { "社員コード", "社員略称名" };
                this.Form.EIGYOUTANTOU_NO.PopupDataSource = ShainDataTable;
                this.Form.EigyouPopupOpenButton.PopupDataSource = ShainDataTable;

                // 取引先CD(テキストボックスとボタン)
                this.Form.TORIHIKISAKI_NO.PopupWindowId = WINDOW_ID.M_TORIHIKISAKI;
                this.Form.TorihikiPopupButton.PopupWindowId = WINDOW_ID.M_TORIHIKISAKI;
                // ポップアップに表示するデータ列(物理名)
                this.Form.TORIHIKISAKI_NO.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
                this.Form.TorihikiPopupButton.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
                // 表示用データ取得＆加工
                var TorihikiDataTable = this.GetPopUpTorihikiData(this.Form.TORIHIKISAKI_NO.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // 列名とデータソース設定
                this.Form.TORIHIKISAKI_NO.PopupDataHeaderTitle = new string[] { "取引先コード", "取引先名" };
                this.Form.TORIHIKISAKI_NO.PopupDataSource = TorihikiDataTable;
                this.Form.TorihikiPopupButton.PopupDataSource = TorihikiDataTable;


                // 拠点CD(テキストボックスとボタン)
                this.Header.KYOTEN_CD.PopupWindowId = WINDOW_ID.M_KYOTEN;
                // ポップアップに表示するデータ列(物理名)
                this.Header.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
                // 表示用データ取得＆加工
                var KyotenDataTable = this.GetPopUpKyotenData(this.Header.KYOTEN_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // 列名とデータソース設定
                this.Header.KYOTEN_CD.PopupDataHeaderTitle = new string[] { "拠点CD", "拠点名" };
                this.Header.KYOTEN_CD.PopupDataSource = KyotenDataTable;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PopupInit", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupInit", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタンイベントの初期化処理
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.Form.Parent;

            //新規ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.Form.NewAdd);

            //修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.Form.ReSearch);

            //一覧ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.Form.OpenIchiran);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.Form.RegistData);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.Form.FormClose);

            //抽出ボタンイベント生成
            parentForm.bt_process2.Click += new EventHandler(this.Form.SearchKeshikomi);

            //手型ボタンイベント生成
            parentForm.bt_process3.Click += new EventHandler(this.Form.Tekata);

            //headerタイトル設定
            this.Header.Gamen_Tittle.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_NYUKIN);

            //ウインドウタイトル設定
            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.Header.Gamen_Tittle.Text);

            LogUtility.DebugMethodEnd();
        }

        //明細一覧の入出金区分コードと入出金区分略称cellを結合する。
        public bool ChangeCell(object sender, DataGridViewCellPaintingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // ガード句
                if (e.RowIndex > -1)
                {
                    // ヘッダー以外は処理なし
                    return ret;
                }

                //2～3列目を結合する処理
                // 処理対象セルが、2列目の場合のみ処理を行う
                if (e.ColumnIndex == 1)
                {
                    // セルの矩形を取得
                    Rectangle rect = e.CellBounds;

                    DataGridView dgv = (DataGridView)sender;

                    // 3列目の幅を取得して、2列目の幅に足す
                    rect.Width += dgv.Columns[2].Width;

                    // 背景、枠線、セルの値を描画
                    using (SolidBrush brush = new SolidBrush(this.Form.MeisaiIchiran.ColumnHeadersDefaultCellStyle.BackColor))
                    {
                        // 背景の描画
                        e.Graphics.FillRectangle(brush, rect);

                        using (Pen pen = new Pen(dgv.GridColor))
                        {
                            // 枠線の描画
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                    }

                    // セルに表示するテキストを描画
                    TextRenderer.DrawText(e.Graphics,
                                    "入金区分※",
                                    e.CellStyle.Font,
                                    rect,
                                    e.CellStyle.ForeColor,
                                    TextFormatFlags.Left
                                    | TextFormatFlags.VerticalCenter);
                }

                // 結合セル以外は既定の描画を行う
                if (!(e.ColumnIndex == 1 || e.ColumnIndex == 2))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeCell", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        /// <summary>
        /// 初期表示処理
        /// </summary>
        public bool FirstSetInit()
        {
            LogUtility.DebugMethodStart();
            //消込一覧と消込一覧に関連する項目をクリア。
            ClearKesikomi();
            //明細一覧と明細一覧に関連する項目をクリア。
            ClearMeisai();
            //伝票日付を現在の時間にする
            this.Form.DENPYOU_DATE.Value = this.ParentForm.sysDate;
            //Paramから入金番号取得しheaderデータを検索する。
            if (this.Form.NYUKIN_NO.Text != "")
            {
                //header部検索
                this.Search_header(this.Form.NYUKIN_NO.Text);
                //データがない場合、メッセージを表示しフォーカスを移動する。
                if (this.SearchResult_header.Rows.Count == 0)
                {
                    this.Form.NYUKIN_NO.Text = "";
                    MessageShowLogic.MessageBoxShow("E045", "");
                    this.Gamen_Mode = 1;
                    if (!Clear())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    this.Form.NYUKIN_NO.Focus();
                    this.Form.NYUKIN_NO.ReadOnly = false;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else
                {
                    //新規画面で入力した場合、修正画面に切り替える
                    if (this.Gamen_Mode == 1)
                    {
                        this.Gamen_Mode = 2;
                    }
                    this.Form.NYUKIN_NO.ReadOnly = true;
                }

                SetGamenHeader(this.Gamen_Mode);
                //header部セット
                this.SetResHeader();

                //取引区分名を取得
                this.Search_torihiki(this.Form.TORIHIKISAKI_NO.Text);


                //明細部検索
                this.Search_detail(this.Form.NYUKIN_NO.Text);
                //明細部セット
                this.SetResDetail();

                //前回御請求額検索
                if (this.Form.TORIHIKISAKI_NO.Text != "")
                {
                    this.Search_zenkai(this.Form.TORIHIKISAKI_NO.Text, this.Form.DENPYOU_DATE.Value.ToString());
                    //前回御請求額セット
                    this.SetResZenkai();
                }

                //今回入金額、調整額、今回合計額のセット
                this.SetReskonkai_nyuukin();

                //締処理状況の検索
                this.Search_shorijyoukyou(this.Form.NYUKIN_NO.Text);

                //締処理状況のセット
                this.SetResJyoukyou();

                //消込初期値を格納するlistを初期化する
                List_kesikomi_data_old = new List<T_NYUUKIN_KESHIKOMI>();

                //初期表示（元の）時の消込一覧のデータを取得するフラグ：抽出ボタンを押すと元の消込データがなくなる為
                First_Kesikomi_Flg = true;

                //消込一覧セット
                if (!this.SearchKeshikomi())
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                //消込一覧セット（初期表示値）が終わったらフラグをfalseにする。
                First_Kesikomi_Flg = false;

                //拠点名セット
                SetKyoten();

                this.Nyuukin_Number = this.Form.NYUKIN_NO.Text;

                //伝票日付、取引先変更フラグを初期化
                this.IsDateChanged = false;
                this.IsTorihikiChanged = false;
            }
            else
            {
                this.Form.DENPYOU_DATE.Value = this.ParentForm.sysDate;
                SetGamenHeader(this.Gamen_Mode);
                SetKyoten();
            }
            if (this.Gamen_Mode == 3)
            {
                var parentForm = (BusinessBaseForm)this.Form.Parent;
                parentForm.bt_func9.Focus();
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 画面のヘッダをセット
        /// </summary>
        public bool SetGamenHeader(int i)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(i);
                if (i == 1)
                {
                    XMLAccessor fileAccess = new XMLAccessor();
                    CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                    //ヘッダに表示予定の値
                    if (configProfile.ItemSetVal1.Length == 1)
                    {
                        this.Header.KYOTEN_CD.Text = "0" + configProfile.ItemSetVal1;
                    }
                    else
                    {
                        this.Header.KYOTEN_CD.Text = configProfile.ItemSetVal1;
                    }
                    //初期表示する時拠点コードあれば略称を取得
                    SetKyoten();
                    this.Gamen_Mode = 1;
                    this.Header.Gamen_Mode.Visible = true;
                    this.Header.Gamen_Mode.Text = "新規";
                    this.Header.Gamen_Mode.BackColor = System.Drawing.Color.Aqua;
                    this.Header.Gamen_Mode.ForeColor = System.Drawing.Color.Black;
                    this.Form.NYUKIN_NO.ReadOnly = false;
                    this.Form.NYUKIN_NO.TabStop = true;
                    //初期表示の場合は更新しないため消込フラグをfalseにする。
                    First_Kesikomi_Flg = false;
                    ReadonlyFalse();
                }
                if (i == 2)
                {
                    this.Gamen_Mode = 2;
                    this.Header.Gamen_Mode.Visible = true;
                    this.Header.Gamen_Mode.Text = "修正";
                    this.Form.NYUKIN_NO.ReadOnly = true;
                    this.Form.NYUKIN_NO.TabStop = false;
                    this.Header.Gamen_Mode.BackColor = System.Drawing.Color.Orange;
                    this.Header.Gamen_Mode.ForeColor = System.Drawing.Color.Black;
                    ReadonlyFalse();
                }
                if (i == 3)
                {
                    this.Gamen_Mode = 3;
                    this.Header.Gamen_Mode.Visible = true;
                    this.Header.Gamen_Mode.Text = "削除";
                    this.Form.NYUKIN_NO.ReadOnly = true;
                    this.Form.NYUKIN_NO.TabStop = false;
                    this.Header.Gamen_Mode.BackColor = System.Drawing.Color.Red;
                    this.Header.Gamen_Mode.ForeColor = System.Drawing.Color.White;
                    ReadonlyTrue();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGamenHeader", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region 各検索処理

        /// <summary>
        /// データ取得処理(header部分)
        /// </summary>
        /// <param>入金番号
        public void Search_header(String nyukin_no)
        {
            LogUtility.DebugMethodStart(nyukin_no);
            DTOClass search_nyuukin = new DTOClass();
            search_nyuukin.Nyukin_number = nyukin_no;
            SearchResult_header = new DataTable();
            this.SearchResult_header = Dao_header.GetDataForEntity(search_nyuukin);
            int count = this.SearchResult_header.Rows.Count;
            LogUtility.DebugMethodEnd(count);
        }

        /// <summary>
        /// 取引区分名を取得
        /// </summary>
        /// <param>取引先cd
        public void Search_torihiki(String torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            DTOClass search_detail = new DTOClass();

            search_detail.Torihikisaki_cd = torihikisakiCd;
            SearchResult_torihiki = new DataTable();
            this.SearchResult_torihiki = Dao_torihiki.GetDataForEntity(search_detail);

            if (this.SearchResult_torihiki.Rows.Count > 0)
            {
                var table = this.SearchResult_torihiki;
                String s1 = "";
                s1 = table.Rows[0]["TORIHIKI_KBN_CD"].ToString();
                if (s1 == "1")
                {
                    this.Form.TORIHIKI_KUBUN.Text = "現金";
                }
                if (s1 == "2")
                {
                    this.Form.TORIHIKI_KUBUN.Text = "掛け";
                }
                this.Form.KAISYUBI_DATE.Text = table.Rows[0]["KAISHUU_DAY"].ToString();
                this.Form.SIMEBI_1.Text = table.Rows[0]["SHIMEBI1"].ToString();
                this.Form.SIMEBI_2.Text = table.Rows[0]["SHIMEBI2"].ToString();
                this.Form.SIMEBI_3.Text = table.Rows[0]["SHIMEBI3"].ToString();
                this.Form.TORIHIKISAKI_NAME.Text = table.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
            }
            else
            {
                this.Form.TORIHIKISAKI_NAME.Text = "";
                this.Form.TORIHIKI_KUBUN.Text = "";
                this.Form.KAISYUBI_DATE.Text = "";
                this.Form.SIMEBI_1.Text = "";
                this.Form.SIMEBI_2.Text = "";
                this.Form.SIMEBI_3.Text = "";
            }

            LogUtility.DebugMethodEnd(1);
        }

        /// <summary>
        /// 再検索処理
        /// </summary>
        public bool ReSearch()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.Gamen_Mode = 2;
                if (this.Nyuukin_Number == null || this.Nyuukin_Number == "")
                {
                    ret = true;
                    return ret;
                }
                if (!Clear())
                {
                    ret = false;
                    return ret;
                }
                this.Form.NYUKIN_NO.Text = this.Nyuukin_Number;
                if (this.Nyuukin_Number != "")
                {
                    this.Form.NYUKIN_NO.Text = this.Nyuukin_Number;
                    if (!this.FirstSetInit())
                    {
                        ret = false;
                        return ret;
                    }
                    var parentForm = (BusinessBaseForm)this.Form.Parent;
                    parentForm.bt_func2.Enabled = true;
                    parentForm.bt_func9.Enabled = true;
                    this.Gamen_Mode = 2;
                    this.Header.Gamen_Mode.Text = "修正";
                    this.Form.NYUKIN_NO.ReadOnly = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ReSearch", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ReSearch", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得処理(明細部分)
        /// </summary>
        /// <param>入金番号
        public void Search_detail(String nyukin_no)
        {
            LogUtility.DebugMethodStart(nyukin_no);
            DTOClass search_detail = new DTOClass();
            search_detail.Nyukin_number = nyukin_no;
            SearchResult_detail = new DataTable();
            this.SearchResult_detail = Dao_meisai.GetDataForEntity(search_detail);
            LogUtility.DebugMethodEnd(1);
        }

        /// <summary>
        /// データ取得処理(前回御請求額)
        /// </summary>
        /// <param>取引先CD
        public void Search_zenkai(String torihikisaki_cd, String denpyou_date)
        {
            LogUtility.DebugMethodStart(torihikisaki_cd, denpyou_date);

            DTOClass search_zenkai = new DTOClass();
            search_zenkai.Torihikisaki_cd = torihikisaki_cd;
            search_zenkai.Denpyou_Date = DateTime.Parse(denpyou_date);
            SearchResult_Zenkai = new DataTable();
            this.SearchResult_Zenkai = Dao_Zenkai.GetDataForEntity(search_zenkai);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処理状況取得
        /// </summary>
        /// <param>取引先CD
        public void Search_shorijyoukyou(String nyukin_no)
        {
            LogUtility.DebugMethodStart(nyukin_no);

            DTOClass search_shorijyoukyou = new DTOClass();
            search_shorijyoukyou.Nyukin_number = nyukin_no;
            SearchResult_ShoriChoukyou = new DataTable();
            this.SearchResult_ShoriChoukyou = Dao_Shimezumi.GetDataForEntity(search_shorijyoukyou);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 開始売掛取得
        /// </summary>
        /// <param>取引先CD
        /// <param>入金番号
        /// <param>伝票日付
        public void Search_kaishi_urikake(String nyukin_no, String torihikisaki_cd, String denpyou_date)
        {
            LogUtility.DebugMethodStart(nyukin_no, torihikisaki_cd, denpyou_date);
            DTOClass search_kaishi_urikake = new DTOClass();
            search_kaishi_urikake.Nyukin_number = nyukin_no;
            search_kaishi_urikake.Torihikisaki_cd = torihikisaki_cd;
            search_kaishi_urikake.Denpyou_Date = DateTime.Parse(denpyou_date);
            SearchResult_kaishi_urikake = new DataTable();
            this.SearchResult_kaishi_urikake = Dao_Kaishi.GetDataForEntity(search_kaishi_urikake);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 消込明細部
        /// </summary>
        /// <param>取引先CD
        /// <param>入金番号
        /// <param>伝票日付
        public void Search_kesikomi(String nyukin_no, String torihikisaki_cd, String denpyou_date)
        {
            LogUtility.DebugMethodStart(nyukin_no, torihikisaki_cd, denpyou_date);
            DTOClass search_kesikomi = new DTOClass();
            search_kesikomi.Nyukin_number = nyukin_no;
            search_kesikomi.Torihikisaki_cd = torihikisaki_cd;
            search_kesikomi.Denpyou_Date = DateTime.Parse(denpyou_date);
            SearchResult_kesikomi = new DataTable();
            this.SearchResult_kesikomi = Dao_Kesikomi.GetDataForEntity(search_kesikomi);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 消込Seq
        /// </summary>
        /// <param>システムID
        public String Search_MaxKesikomeSeq(SqlInt64 systemId)
        {
            LogUtility.DebugMethodStart();

            DTOClass search_MaxKesikomiSeq = new DTOClass();
            search_MaxKesikomiSeq.System_Id = systemId.ToString();
            SearchResult_MaxKesikomiSeq = new DataTable();
            this.SearchResult_MaxKesikomiSeq = Dao_MaxKesikomiSeq.GetDataForEntity(search_MaxKesikomiSeq);

            var table = this.SearchResult_MaxKesikomiSeq;
            if (table.Rows.Count > 0)
            {
                LogUtility.DebugMethodEnd();
                return table.Rows[0]["KESHIKOMI_SEQ"].ToString();
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return "0";
            }
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(営業担当者)</param>
        /// <returns></returns>
        public DataTable GetPopUpShainData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart();

            this.Dao_PopupEikyouTantou = DaoInitUtility.GetComponent<DAO.DAOClass_PopupEikyouTantou>();

            DTOClass search_Eikyoutantou = new DTOClass();
            search_Eikyoutantou.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupEikyouTantou.GetDataForEntity(search_Eikyoutantou);
            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            foreach (var col in displayCol)
            {
                // 表示対象の列だけを順番に追加
                sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
            }

            foreach (DataRow r in dt.Rows)
            {
                sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
            }

            LogUtility.DebugMethodEnd();
            return sortedDt;
            //return null;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(取引先)</param>
        /// <returns></returns>
        public DataTable GetPopUpTorihikiData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart();

            this.Dao_PopupTorihikisaki = DaoInitUtility.GetComponent<DAO.DAOClass_PopupTorihikisaki>();

            DTOClass search_PopupTorihikisaki = new DTOClass();
            search_PopupTorihikisaki.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupTorihikisaki.GetDataForEntity(search_PopupTorihikisaki); ;
            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            foreach (var col in displayCol)
            {
                // 表示対象の列だけを順番に追加
                sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
            }

            foreach (DataRow r in dt.Rows)
            {
                sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
            }

            LogUtility.DebugMethodEnd();
            return sortedDt;
            //return null;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(拠点)</param>
        /// <returns></returns>
        public DataTable GetPopUpKyotenData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart();

            this.Dao_PopupKyoten = DaoInitUtility.GetComponent<DAO.DAOClass_PopupKyoten>();

            DTOClass search_PopupKyoten = new DTOClass();
            search_PopupKyoten.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupKyoten.GetDataForEntity(search_PopupKyoten); ;
            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            foreach (var col in displayCol)
            {
                // 表示対象の列だけを順番に追加
                sortedDt.Columns.Add(dt.Columns[col].ColumnName, Type.GetType("System.String"));
            }

            foreach (DataRow r in dt.Rows)
            {
                sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
            }

            LogUtility.DebugMethodEnd();
            return sortedDt;
            //return null;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列</param>
        /// <returns></returns>
        public DataTable GetPopUpNYUUSHUKKIN_KBNData(IEnumerable<string> displayCol)
        {
            LogUtility.DebugMethodStart();

            this.Dao_PopupNyuushukkin_Kbn = DaoInitUtility.GetComponent<DAO.DAOClass_PopupNyushuukkin_Kbn>();

            DTOClass search_PopupNyuushukkin_Kbn = new DTOClass();
            search_PopupNyuushukkin_Kbn.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = Dao_PopupNyuushukkin_Kbn.GetDataForEntity(search_PopupNyuushukkin_Kbn); ;
            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            foreach (var col in displayCol)
            {
                // 表示対象の列だけを順番に追加
                sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
            }

            foreach (DataRow r in dt.Rows)
            {
                sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
            }

            LogUtility.DebugMethodEnd();
            return sortedDt;
            //return null;
        }

        #endregion

        #region 各検索結果をformにセットする

        /// <summary>
        /// 入金番号が存在(修正、削除)する時の初期処理:header部分
        /// </summary>
        public void SetResHeader()
        {
            LogUtility.DebugMethodStart();
            var table = this.SearchResult_header;
            Old_insert_entry = new T_NYUUKIN_ENTRY();
            if (table.Rows.Count > 0)
            {
                //headerform
                this.Header.KYOTEN_CD.Text = table.Rows[0]["KYOTEN_CD"].ToString();
                //明細header
                this.Form.DENPYOU_DATE.Value = table.Rows[0]["DENPYOU_DATE"];
                this.Form.GINKOU_CD.Text = table.Rows[0]["BANK_CD"].ToString();
                this.Form.GINKOU_NAME.Text = table.Rows[0]["BANK_NAME_RYAKU"].ToString();
                this.Form.GINKOU_SHITEN_CD.Text = table.Rows[0]["BANK_SHITEN_CD"].ToString();
                this.Form.GINKOU_SHITEN_NAME.Text = table.Rows[0]["BANK_SHIETN_NAME_RYAKU"].ToString();
                this.Form.KOUZASYURUI_NAME.Text = table.Rows[0]["KOUZA_SHURUI"].ToString();
                this.Form.KOUZASYURUI_NO.Text = table.Rows[0]["KOUZA_NO"].ToString();
                this.Form.TORIHIKISAKI_NO.Text = table.Rows[0]["TORIHIKISAKI_CD"].ToString();
                this.Form.TORIHIKISAKI_NAME.Text = table.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                this.Form.TORIHIKI_KUBUN.Text = table.Rows[0]["TORIHIKI_KBN_CD"].ToString();
                this.Form.KAISYUBI_DATE.Text = table.Rows[0]["KAISHUU_DAY"].ToString();
                this.Form.SIMEBI_1.Text = table.Rows[0]["SHIMEBI1"].ToString();
                this.Form.SIMEBI_2.Text = table.Rows[0]["SHIMEBI2"].ToString();
                this.Form.SIMEBI_3.Text = table.Rows[0]["SHIMEBI3"].ToString();
                this.Form.EIGYOUTANTOU_NO.Text = table.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                this.Form.EIGYOUTANTOU_NAME.Text = table.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                this.Form.BIKOU_TXT.Text = table.Rows[0]["DENPYOU_BIKOU"].ToString();
                this.System_Id = table.Rows[0]["SYSTEM_ID"].ToString();
                this.Seq = table.Rows[0]["SEQ"].ToString();
                this.Nyuukin_Number = table.Rows[0]["NYUUKIN_NUMBER"].ToString();

                //元のデータを取得
                Old_insert_entry.KYOTEN_CD = SqlInt16.Parse(table.Rows[0]["KYOTEN_CD"].ToString());
                Old_insert_entry.CREATE_USER = table.Rows[0]["CREATE_USER"].ToString();
                Old_insert_entry.CREATE_DATE = SqlDateTime.Parse(table.Rows[0]["CREATE_DATE"].ToString());
                Old_insert_entry.UPDATE_USER = table.Rows[0]["UPDATE_USER"].ToString();
                Old_insert_entry.UPDATE_DATE = SqlDateTime.Parse(table.Rows[0]["UPDATE_DATE"].ToString());
                Old_insert_entry.CREATE_PC = table.Rows[0]["CREATE_PC"].ToString();
                Old_insert_entry.UPDATE_PC = table.Rows[0]["UPDATE_PC"].ToString();
                //明細header
                Old_insert_entry.DENPYOU_DATE = SqlDateTime.Parse(table.Rows[0]["DENPYOU_DATE"].ToString());
                Old_insert_entry.NYUUKIN_NUMBER = SqlInt64.Parse(table.Rows[0]["NYUUKIN_NUMBER"].ToString());
                Old_insert_entry.BANK_CD = table.Rows[0]["BANK_CD"].ToString();
                Old_insert_entry.BANK_SHITEN_CD = table.Rows[0]["BANK_SHITEN_CD"].ToString();
                Old_insert_entry.KOUZA_SHURUI = table.Rows[0]["KOUZA_SHURUI"].ToString();
                Old_insert_entry.KOUZA_NO = table.Rows[0]["KOUZA_NO"].ToString();
                Old_insert_entry.TORIHIKISAKI_CD = table.Rows[0]["TORIHIKISAKI_CD"].ToString();
                Old_insert_entry.EIGYOU_TANTOUSHA_CD = table.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                Old_insert_entry.DENPYOU_BIKOU = table.Rows[0]["DENPYOU_BIKOU"].ToString();
                Old_insert_entry.SYSTEM_ID = SqlInt64.Parse(table.Rows[0]["SYSTEM_ID"].ToString());
                Old_insert_entry.SEQ = SqlInt32.Parse(table.Rows[0]["SEQ"].ToString());
                Old_insert_entry.NYUUKIN_AMOUNT_TOTAL = SqlDecimal.Parse(BlankToZero(table.Rows[0]["NYUUKIN_AMOUNT_TOTAL"].ToString()));
                Old_insert_entry.CHOUSEI_AMOUNT_TOTAL = SqlDecimal.Parse(BlankToZero(table.Rows[0]["CHOUSEI_AMOUNT_TOTAL"].ToString()));
                Old_insert_entry.TIME_STAMP = (byte[])table.Rows[0]["TIME_STAMP"];
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入金番号が存在(修正、削除)する時の初期処理:Detail部分
        /// </summary>
        public void SetResDetail()
        {
            LogUtility.DebugMethodStart();
            this.Form.MeisaiIchiran.DataSource = null;
            var table = this.SearchResult_detail;
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.Form.MeisaiIchiran.Rows.Add();
                    this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_CD"].Value = table.Rows[i]["NYUUSHUKKIN_KBN_CD"];
                    this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_NAME_RYAKU"].Value = table.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"];
                    this.Form.MeisaiIchiran.Rows[i].Cells["KINGAKU"].Value = KingakuSiki(BlankToZero(table.Rows[i]["KINGAKU"].ToString())); ;
                    this.Form.MeisaiIchiran.Rows[i].Cells["MEISAI_BIKOU"].Value = table.Rows[i]["MEISAI_BIKOU"];
                    this.Form.MeisaiIchiran.Rows[i].Cells["DETAIL_SYSTEM_ID"].Value = table.Rows[i]["DETAIL_SYSTEM_ID"];
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 前回御請求額のセット
        /// </summary>
        public void SetResZenkai()
        {
            LogUtility.DebugMethodStart();
            var table = this.SearchResult_Zenkai;
            if (table.Rows.Count > 0)
            {
                this.Form.ZENKAIGOSEIKYUGAKU_TXT.Text = KingakuSiki(table.Rows[0]["ZENKAI_SEIKYUUGAKU"].ToString());
            }
            else
            {
                this.Form.ZENKAIGOSEIKYUGAKU_TXT.Text = "";
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ヘッダの拠点名セット
        /// </summary>
        public void SetKyoten()
        {
            LogUtility.DebugMethodStart();
            M_KYOTEN mKyoten = new M_KYOTEN();
            mKyoten = (M_KYOTEN)MkyotenDao.GetDataByCd(this.Header.KYOTEN_CD.Text);
            if (mKyoten == null)
            {
                this.Header.KYOTEN_NAME_RYAKU.Text = "";
            }
            else
            {
                this.Header.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 今回入金額、調整額、今回合計額のセット
        /// </summary>
        public void SetReskonkai_nyuukin()
        {
            LogUtility.DebugMethodStart();
            decimal konkai_nyukin = 0;
            decimal chousei = 0;
            if (this.SearchResult_detail.Rows.Count > 0)
            {
                for (int i = 0; i < this.SearchResult_detail.Rows.Count; i++)
                {
                    if (int.Parse(this.SearchResult_detail.Rows[i]["NYUUSHUKKIN_KBN_CD"].ToString()) <= 20)
                    {
                        konkai_nyukin = konkai_nyukin + decimal.Parse(this.SearchResult_detail.Rows[i]["KINGAKU"].ToString());
                    }
                    else
                    {
                        chousei = chousei + decimal.Parse(this.SearchResult_detail.Rows[i]["KINGAKU"].ToString());
                    }
                }
            }
            this.Form.KONKAINYUKINGAKU_TXT.Text = KingakuSiki(konkai_nyukin.ToString());
            this.Form.CHOUSEIGAKU_TXT.Text = KingakuSiki(chousei.ToString());
            this.Form.KONKAIGOUKEIGAKU_TXT.Text = KingakuSiki((konkai_nyukin + chousei).ToString());
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 締処理状況のセット
        /// </summary>
        public void SetResJyoukyou()
        {
            LogUtility.DebugMethodStart();
            var table = this.SearchResult_ShoriChoukyou;
            if (table.Rows.Count > 0)
            {
                this.Form.SIMESYORIJYOUKYOU_TXT.Text = "締済";
            }
            else
            {
                this.Form.SIMESYORIJYOUKYOU_TXT.Text = "未締";
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 開始売掛金残高がある場合、消込明細にセット
        /// </summary>
        public void SetResKaishiUrikake()
        {
            LogUtility.DebugMethodStart();
            var table = this.SearchResult_kaishi_urikake;

            if (table.Rows.Count > 0)
            {
                //消込データが初期表示の場合のみ更新用listに追加する。
                if (First_Kesikomi_Flg)
                {
                    //存在する場合のみ更新用listに追加する。
                    if (table.Rows[0]["KESIKOMI_SYSTEM_ID"].ToString() != "")
                    {
                        Kesikomi_data_old = new T_NYUUKIN_KESHIKOMI();
                        Kesikomi_data_old.SYSTEM_ID = SqlInt64.Parse(table.Rows[0]["KESIKOMI_SYSTEM_ID"].ToString());
                        Kesikomi_data_old.SEIKYUU_NUMBER = SqlInt64.Parse(table.Rows[0]["SEIKYUU_NUMBER"].ToString());
                        Kesikomi_data_old.KESHIKOMI_SEQ = SqlInt32.Parse(table.Rows[0]["KESHIKOMI_SEQ"].ToString());
                        Int32 TimeStamp = (Int32)table.Rows[0]["TIME_STAMP"];
                        byte[] ts = ConvertStrByte.In32ToByteArray(TimeStamp);
                        Kesikomi_data_old.TIME_STAMP = ts;
                        Kesikomi_data_old.DELETE_FLG = true;
                        List_kesikomi_data_old.Add(Kesikomi_data_old);
                    }
                }
                this.Form.KesikomiIchiran.Rows.Add();
                if (this.Form.NYUKIN_NO.Text != "")
                {
                    this.Form.KesikomiIchiran.Rows[0].Cells["NEW_KESHIKOMI_GAKU"].Value = KingakuSiki(table.Rows[0]["KESHIKOMI_GAKU"].ToString());
                    this.Form.KesikomiIchiran.Rows[0].Cells["KESHIKOMI_SEQ"].Value = table.Rows[0]["KESHIKOMI_SEQ"];
                    this.Form.KesikomiIchiran.Rows[0].Cells["KESIKOMI_SYSTEM_ID"].Value = table.Rows[0]["KESIKOMI_SYSTEM_ID"];
                    this.Form.KesikomiIchiran.Rows[0].Cells["OLD_KESIKOMI_GAKU"].Value = table.Rows[0]["KESHIKOMI_GAKU"];
                    this.Form.KesikomiIchiran.Rows[0].Cells["KESIKOMI_NYUKIN_SEQ"].Value = table.Rows[0]["NYUUKIN_SEQ"];
                    this.Form.KesikomiIchiran.Rows[0].Cells["KESIKOMI_NYUKIN_NUMBER"].Value = table.Rows[0]["NYUUKIN_NUMBER"];
                }
                //this.Form.KesikomiIchiran.Rows[0].Cells["TIME_STAMP"].Value = table.Rows[0]["TIME_STAMP"];
                this.Form.KesikomiIchiran.Rows[0].Cells["OLD_MINYU_GAKU"].Value = table.Rows[0]["MINYUUKIN_GAKU"];
                this.Form.KesikomiIchiran.Rows[0].Cells["SEIKYUBI"].Value = table.Rows[0]["SEIKYUU_DATE"];
                this.Form.KesikomiIchiran.Rows[0].Cells["SEIKYUUGAKU"].Value = KingakuSiki(table.Rows[0]["SEIKYUU_GAKU"].ToString());
                this.Form.KesikomiIchiran.Rows[0].Cells["MINYU_GAKU"].Value = KingakuSiki(table.Rows[0]["MINYUUKIN_GAKU"].ToString());
                this.Form.KesikomiIchiran.Rows[0].Cells["SEIKYUU_NUMBER"].Value = table.Rows[0]["SEIKYUU_NUMBER"];
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 消込明細をセット
        /// </summary>
        public void SetResKesiKomi()
        {
            LogUtility.DebugMethodStart();
            var table = this.SearchResult_kesikomi;
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //消込データが初期表示の場合のみ更新用listに追加する。
                    if (First_Kesikomi_Flg)
                    {
                        //存在する場合のみ更新用listに追加する。
                        if (table.Rows[i]["KESIKOMI_SYSTEM_ID"].ToString() != "")
                        {
                            Kesikomi_data_old = new T_NYUUKIN_KESHIKOMI();
                            Kesikomi_data_old.SYSTEM_ID = SqlInt64.Parse(table.Rows[i]["KESIKOMI_SYSTEM_ID"].ToString());
                            Kesikomi_data_old.SEIKYUU_NUMBER = SqlInt64.Parse(table.Rows[i]["SEIKYUU_NUMBER"].ToString());
                            Kesikomi_data_old.KESHIKOMI_SEQ = SqlInt32.Parse(table.Rows[i]["KESHIKOMI_SEQ"].ToString());
                            Int32 TimeStamp = (Int32)table.Rows[i]["TIME_STAMP"];
                            byte[] ts = ConvertStrByte.In32ToByteArray(TimeStamp);
                            Kesikomi_data_old.TIME_STAMP = ts;
                            Kesikomi_data_old.DELETE_FLG = true;
                            List_kesikomi_data_old.Add(Kesikomi_data_old);
                        }
                    }

                    int j = i;
                    if (this.SearchResult_kaishi_urikake.Rows.Count > 0)
                    {
                        j = i + 1;
                    }
                    this.Form.KesikomiIchiran.Rows.Add();
                    if (this.Form.NYUKIN_NO.Text != "")
                    {
                        this.Form.KesikomiIchiran.Rows[j].Cells["KESHIKOMI_SEQ"].Value = table.Rows[i]["KESHIKOMI_SEQ"];
                        this.Form.KesikomiIchiran.Rows[j].Cells["NEW_KESHIKOMI_GAKU"].Value = KingakuSiki(table.Rows[i]["KESHIKOMI_GAKU"].ToString());
                        this.Form.KesikomiIchiran.Rows[j].Cells["KESIKOMI_SYSTEM_ID"].Value = table.Rows[i]["KESIKOMI_SYSTEM_ID"];
                        this.Form.KesikomiIchiran.Rows[j].Cells["OLD_KESIKOMI_GAKU"].Value = table.Rows[i]["KESHIKOMI_GAKU"];
                        this.Form.KesikomiIchiran.Rows[j].Cells["KESIKOMI_NYUKIN_SEQ"].Value = table.Rows[i]["NYUUKIN_SEQ"];
                        this.Form.KesikomiIchiran.Rows[j].Cells["KESIKOMI_NYUKIN_NUMBER"].Value = table.Rows[i]["NYUUKIN_NUMBER"];
                    }
                    this.Form.KesikomiIchiran.Rows[j].Cells["OLD_MINYU_GAKU"].Value = table.Rows[i]["MINYU_GAKU"];
                    this.Form.KesikomiIchiran.Rows[j].Cells["SEIKYUBI"].Value = (DateTime.Parse(table.Rows[i]["SEIKYUU_DATE"].ToString())).ToString("yyyy/MM/dd");
                    this.Form.KesikomiIchiran.Rows[j].Cells["SEIKYUUGAKU"].Value = KingakuSiki(table.Rows[i]["SEIKYUU_GAKU"].ToString());
                    this.Form.KesikomiIchiran.Rows[j].Cells["SEIKYUU_NUMBER"].Value = table.Rows[i]["SEIKYUU_NUMBER"].ToString();
                    this.Form.KesikomiIchiran.Rows[j].Cells["MINYU_GAKU"].Value = KingakuSiki(table.Rows[i]["MINYU_GAKU"].ToString());
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 入金番号が空(新規)の時の初期処理
        /// </summary>
        public void SetKesikomiHeader()
        {
            LogUtility.DebugMethodStart();
            //消込一覧のsettingが終わらないと何もしない。
            int i1 = 0;
            int i2 = 0;
            if (this.SearchResult_kaishi_urikake != null)
            {
                i1 = this.SearchResult_kaishi_urikake.Rows.Count;
            }
            if (this.SearchResult_kesikomi != null)
            {
                i2 = this.SearchResult_kesikomi.Rows.Count;
            }
            if (this.Form.KesikomiIchiran.Rows.Count != i1 + i2)
            {
                return;
            }
            //入金額の合計
            decimal d1 = 0;
            for (int i = 0; i < this.Form.KesikomiIchiran.Rows.Count; i++)
            {
                d1 = d1 + decimal.Parse(BlankToZero(NullToBlank(this.Form.KesikomiIchiran.Rows[i].Cells[3].Value).ToString()));
            }
            this.Form.NyukinGoukeigaku.Text = KingakuSiki(d1.ToString());
            this.Form.NYUKINGAKUSAGAKU_TXT.Text = KingakuSiki((decimal.Parse(BlankToZero(this.Form.KONKAIGOUKEIGAKU_TXT.Text.Replace(",", ""))) - decimal.Parse(BlankToZero(this.Form.NyukinGoukeigaku.Text.Replace(",", "")))).ToString());
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public bool Regist()
        {
            LogUtility.DebugMethodStart();
            if (this.Gamen_Mode == 1)
            {
                if (!this.InsertData(1))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            else if (this.Gamen_Mode == 2)
            {
                if (!this.UpdateData(2))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            else
            {
                if (!this.DeleteData(3))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #region 論理削除前のチェック

        /// <summary>
        /// 更新処理前のチェック
        /// </summary>
        public bool ChkBefUpdOrDel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                //画面モードが新規、修正の場合
                if (this.Gamen_Mode == 1 || this.Gamen_Mode == 2)
                {
                    //入力チェック
                    if (!Nyuryoku_check())
                    {
                        ret = false;
                        return ret;
                    }
                }
                //画面モードが削除の場合
                else
                {
                    DialogResult dialogResult = MessageBox.Show("削除してよろしいですか？", "削除", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        ret = false;
                        return ret;
                    }
                }

                //締済のチェック
                if (this.Form.NYUKIN_NO.Text != "")
                {
                    if (!ShimeZumi())
                    {
                        String[] message = new string[2];
                        message[0] = "既に締処理済み";
                        message[1] = "";
                        MessageShowLogic.MessageBoxShow("E046", message);
                        this.Form.DENPYOU_DATE.Focus();
                        ret = false;
                        return ret;
                    }
                }

                //締処理中のチェック
                if (!ShimeChuu())
                {
                    String[] message = new string[2];
                    message[0] = "締処理実行中";
                    message[1] = "現在締処理実行中の範囲に含まれる為、";
                    MessageShowLogic.MessageBoxShow("E046", message);
                    this.Form.DENPYOU_DATE.Focus();
                    ret = false;
                    return ret;
                }


                this.Regist();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("ChkBefUpdOrDel", ex1);
                this.MessageShowLogic.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChkBefUpdOrDel", ex2);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkBefUpdOrDel", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 入力チェック

        /// <summary>
        /// 入力チェック
        /// </summary>
        public Boolean Nyuryoku_check()
        {
            LogUtility.DebugMethodStart();
            //拠点のチェック
            if (this.Header.KYOTEN_CD.Text == "")
            {
                MessageShowLogic.MessageBoxShow("E001", "拠点");
                this.Header.KYOTEN_CD.Focus();
                return false;
            }

            //取引先のチェック
            if (this.Form.TORIHIKISAKI_NO.Text == "")
            {
                MessageShowLogic.MessageBoxShow("E001", "取引先");
                this.Form.TORIHIKISAKI_NO.Focus();
                return false;
            }

            //画面モードが新規の場合
            if (this.Form.MeisaiIchiran.Rows.Count == 1)
            {
                MessageShowLogic.MessageBoxShow("E012", "明細");
                this.Form.MeisaiIchiran.CurrentCell = this.Form.MeisaiIchiran.Rows[0].Cells["NYUUSHUKKIN_KBN_CD"];
                return false;
            }

            //入金区分と入金額チェック
            if (this.Form.MeisaiIchiran.Rows.Count > 0)
            {
                int CountRow = 0;

                //最後の行は除く
                for (int i = 0; i < this.Form.MeisaiIchiran.Rows.Count - 1; i++)
                {
                    if (this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_CD"].Value == null && this.Form.MeisaiIchiran.Rows[i].Cells["KINGAKU"].Value == null)
                    {
                        //両方空の場合はチェックしない。
                    }
                    else
                    {
                        //入金CDのチェック(最後のrowは除く)
                        if (this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_CD"].Value == null && i != this.Form.MeisaiIchiran.Rows.Count - 1)
                        {
                            MessageShowLogic.MessageBoxShow("E001", "入金区分");
                            this.Form.MeisaiIchiran.CurrentCell = this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_CD"];
                            LogUtility.DebugMethodEnd();
                            return false;
                        }
                        //入金金額のチェック(最後のrowは除く)
                        if (this.Form.MeisaiIchiran.Rows[i].Cells["KINGAKU"].Value == null && i != this.Form.MeisaiIchiran.Rows.Count - 1)
                        {
                            MessageShowLogic.MessageBoxShow("E001", "金額");
                            this.Form.MeisaiIchiran.CurrentCell = this.Form.MeisaiIchiran.Rows[i].Cells["KINGAKU"];
                            LogUtility.DebugMethodEnd();
                            return false;
                        }
                        CountRow = CountRow + 1;
                    }
                }
                if (CountRow == 0)
                {
                    MessageShowLogic.MessageBoxShow("E012", "明細");
                    this.Form.MeisaiIchiran.CurrentCell = this.Form.MeisaiIchiran.Rows[0].Cells["NYUUSHUKKIN_KBN_CD"];
                    LogUtility.DebugMethodEnd();
                    return false;
                }
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 締処理中チェック

        //締処理中チェック
        public Boolean ShimeChuu()
        {
            LogUtility.DebugMethodStart();
            DTOClass search_ShimeChuu = new DTOClass();
            search_ShimeChuu.Torihikisaki_cd = this.Form.TORIHIKISAKI_NO.Text;
            search_ShimeChuu.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Value.ToString().Substring(0, 10));
            search_ShimeChuu.Kyoten_cd = this.Header.KYOTEN_CD.Text;
            SearchResult_ShimeChuu = new DataTable();
            this.SearchResult_ShimeChuu = Dao_Shimeshori.GetDataForEntity(search_ShimeChuu);
            if (this.SearchResult_ShimeChuu.Rows.Count > 0)
            {
                LogUtility.DebugMethodEnd();
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 締済チェック

        //締済チェック
        public Boolean ShimeZumi()
        {
            LogUtility.DebugMethodStart();
            DTOClass search_zumi = new DTOClass();
            search_zumi.Nyukin_number = this.Form.NYUKIN_NO.Text;
            SearchResult_zumi = new DataTable();
            this.SearchResult_zumi = Dao_Shimezumi.GetDataForEntity(search_zumi);
            if (this.SearchResult_zumi.Rows.Count > 0)
            {
                LogUtility.DebugMethodEnd();
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 拠点コードロストフォカスチェック

        //拠点コードロストフォカスチェック
        private void Kyoten_LostFocus(object sender, EventArgs e)
        {
            //Kyoten_Validatedを発生するため
        }

        private void Kyoten_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.Header.KYOTEN_CD.Text == "")
            {
                this.Header.KYOTEN_NAME_RYAKU.Text = "";
            }
            else
            {
                DTOClass Dto_Kyoten = new DTOClass();
                Dto_Kyoten.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));
                Dto_Kyoten.Kyoten_cd = this.Header.KYOTEN_CD.Text;
                DataTable Check_Kyoten = new DataTable();
                Check_Kyoten = Dao_CheckKyoten.GetDataForEntity(Dto_Kyoten);
                var table = Check_Kyoten;
                if (table.Rows.Count > 0)
                {
                    this.Header.KYOTEN_NAME_RYAKU.Text = table.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                }
                else
                {
                    MessageShowLogic.MessageBoxShow("E020", "拠点");
                    this.Header.KYOTEN_CD.Focus();
                    this.Header.KYOTEN_NAME_RYAKU.Text = "";
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 営業担当者コードロストフォカスチェック

        //営業担当者コードロストフォカスチェック
        private void Eigyou_LostFocus(object sender, EventArgs e)
        {
            //Kyoten_Validatedを発生するため
        }

        private void Eigyou_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.Form.EIGYOUTANTOU_NO.Text == "")
            {
                this.Form.EIGYOUTANTOU_NAME.Text = "";
            }
            else
            {
                DTOClass Dto_Shain = new DTOClass();
                Dto_Shain.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));
                Dto_Shain.Shain_cd = this.Form.EIGYOUTANTOU_NO.Text;
                DataTable Check_Shain = new DataTable();
                Check_Shain = Dao_CheckShain.GetDataForEntity(Dto_Shain);
                var table = Check_Shain;
                if (table.Rows.Count > 0)
                {
                    this.Form.EIGYOUTANTOU_NAME.Text = table.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                }
                else
                {
                    MessageShowLogic.MessageBoxShow("E020", "社員");
                    this.Form.EIGYOUTANTOU_NO.Focus();
                    this.Form.EIGYOUTANTOU_NAME.Text = "";
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 新規登録処理

        /// <summary>
        /// 新規
        /// </summary>
        [Transaction]
        public bool InsertData(int gamen_Mode)
        {
            LogUtility.DebugMethodStart(gamen_Mode);

            try
            {
                //各データを取得
                SetUpdataData(gamen_Mode);
                using (Transaction tran = new Transaction())
                {
                    //新規登録実行
                    int CntNyuukinIns = Dao_header.Insert(Insert_entry);

                    //入金明細の登録
                    foreach (T_NYUUKIN_DETAIL meisai_data in List_meisai_data_new)
                    {
                        int CntMeisaiIns = Dao_meisai.Insert(meisai_data);
                    }

                    //消込一覧の登録
                    foreach (T_NYUUKIN_KESHIKOMI kesikomi in List_kesikomi_data_new)
                    {
                        kesikomi.DELETE_FLG = false;
                        int CntKesikomiIns = Dao_Kesikomi.Insert(kesikomi);
                    }
                    //成功したら画面をクリアする。
                    if (!Clear())
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    tran.Commit();
                    MessageShowLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageShowLogic.MessageBoxShow("E080", "");
                    this.Form.DENPYOU_DATE.Focus();
                }
                else
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        [Transaction]
        public bool UpdateData(int gamen_Mode)
        {
            LogUtility.DebugMethodStart(gamen_Mode);

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //元のデータを論理削除する。
                    if (!ButsuriDeleteData(2))
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    //新規登録実行
                    int CntNyuukinIns = Dao_header.Insert(Insert_entry);

                    //****************************************************明細部*********************************************************
                    //新しいデータはinsertする。
                    foreach (T_NYUUKIN_DETAIL meisai in List_meisai_data_new)
                    {
                        int CntMeisaiIns = Dao_meisai.Insert(meisai);
                    }

                    //******************************************************消込一覧部*********************************************************

                    //すべての新にの消込一覧のデータを登録。
                    foreach (T_NYUUKIN_KESHIKOMI kesikomi_new in List_kesikomi_data_new)
                    {
                        int CntKesikomiUpd = Dao_Kesikomi.Insert(kesikomi_new);
                    }

                    //更新が終了した後の処理。
                    this.Header.Gamen_Mode.Text = "";
                    this.Header.Gamen_Mode.Visible = false;
                    this.Header.windowTypeLabel.Visible = false;
                    var parentForm = (BusinessBaseForm)this.Form.Parent;
                    parentForm.bt_func9.Enabled = false;
                    this.Seq = Insert_entry.SEQ.ToString();
                    this.Nyuukin_Number = this.Form.NYUKIN_NO.Text;
                    this.Old_insert_entry = null;
                    FormManager.UpdateForm("G078");
                    tran.Commit();
                    MessageShowLogic.MessageBoxShow("I001", "修正");

                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageShowLogic.MessageBoxShow("E080", "");
                    this.Form.DENPYOU_DATE.Focus();
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 論理削除


        /// <summary>
        /// 論理削除
        /// </summary>
        [Transaction]
        public bool DeleteData(int gamen_Mode)
        {
            LogUtility.DebugMethodStart(gamen_Mode);

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //元のデータを論理削除する。
                    if (!ButsuriDeleteData(3))
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                    MessageShowLogic.MessageBoxShow("I001", "削除");
                    FormManager.UpdateForm("G078");
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageShowLogic.MessageBoxShow("E080", "");
                    this.Form.DENPYOU_DATE.Focus();
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 論理削除処理

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public bool ButsuriDeleteData(int gamen_Mode)
        {
            LogUtility.DebugMethodStart(gamen_Mode);

            //データ取得
            SetUpdataData(gamen_Mode);

            //明細ヘッダを削除する
            Old_insert_entry.DELETE_FLG = true;
            Old_insert_entry.UPDATE_DATE = System.DateTime.Now;
            int CntMeisaiIns = Dao_header.Update(Old_insert_entry);

            //すべての元の消込一覧のデータを消す。
            foreach (T_NYUUKIN_KESHIKOMI kesikomi_old in List_kesikomi_data_old)
            {
                int CntKesikomiUpd = Dao_Kesikomi.Update(kesikomi_old);
            }

            if (gamen_Mode != 2)
            {
                //修正の場合はそのまま残す。
                if (!Clear())
                {
                    return false;
                }
            }

            this.Old_insert_entry = null;

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 新規、修正の場合、更新情報のセット

        /// <summary>
        /// 新規、修正の場合、更新情報のセット
        /// </summary>
        public void SetUpdataData(int gamen_Mode)
        {
            LogUtility.DebugMethodStart(gamen_Mode);
            CommonDBAccessor = new DBAccessor();

            //***********************************************header部******************************************
            //入金入力の新規登録
            Insert_entry = new T_NYUUKIN_ENTRY();
            Insert_entry.KYOTEN_CD = SqlInt16.Parse(BlankToZero(this.Header.KYOTEN_CD.Text));
            if (gamen_Mode == 1)
            {
                //新規の場合のみ、新しいsystemidをセットする。
                Insert_entry.SYSTEM_ID = this.CommonDBAccessor.createSystemId(SqlInt16.Parse(DENSHU_KBN.NYUUKIN.GetHashCode().ToString()));
                Insert_entry.SEQ = 1;
                Insert_entry.NYUUKIN_NUMBER = this.CommonDBAccessor.createDenshuNumber(SqlInt16.Parse(DENSHU_KBN.NYUUKIN.GetHashCode().ToString()));
            }
            else
            {
                Insert_entry.SYSTEM_ID = SqlInt64.Parse(this.System_Id);
                Insert_entry.SEQ = SqlInt32.Parse(this.Seq) + 1;
                Insert_entry.NYUUKIN_NUMBER = SqlInt64.Parse(this.Nyuukin_Number);
            }
            Insert_entry.KYOTEN_CD = SqlInt16.Parse(BlankToZero(this.Header.KYOTEN_CD.Text));
            Insert_entry.DENPYOU_DATE = SqlDateTime.Parse(this.Form.DENPYOU_DATE.Value.ToString().Substring(0, 10));
            Insert_entry.TORIHIKISAKI_CD = this.Form.TORIHIKISAKI_NO.Text;
            Insert_entry.NYUUKINSAKI_CD = null;
            Insert_entry.NYUUKIN_AMOUNT_TOTAL = SqlDecimal.Parse(BlankToZero(this.Form.KONKAINYUKINGAKU_TXT.Text.Replace(",", "")));
            Insert_entry.CHOUSEI_AMOUNT_TOTAL = SqlDecimal.Parse(BlankToZero(this.Form.CHOUSEIGAKU_TXT.Text.Replace(",", "")));
            Insert_entry.EIGYOU_TANTOUSHA_CD = this.Form.EIGYOUTANTOU_NO.Text;
            Insert_entry.NYUUKIN_AMOUNT_TOTAL = SqlDecimal.Parse(BlankToZero(this.Form.KONKAINYUKINGAKU_TXT.Text.Replace(",", "")));
            Insert_entry.CHOUSEI_AMOUNT_TOTAL = SqlDecimal.Parse(BlankToZero(this.Form.CHOUSEIGAKU_TXT.Text.Replace(",", "")));
            Insert_entry.DELETE_FLG = false;
            // 更新者情報設定
            var dataBinderLogicNyuukin = new DataBinderLogic<r_framework.Entity.T_NYUUKIN_ENTRY>(Insert_entry);
            dataBinderLogicNyuukin.SetSystemProperty(Insert_entry, false);
            //新規じゃない場合、createuser,createdate,createpcは更新しない
            if (this.Old_insert_entry != null)
            {
                Insert_entry.CREATE_DATE = Old_insert_entry.CREATE_DATE;
                Insert_entry.CREATE_PC = Old_insert_entry.CREATE_PC;
                Insert_entry.CREATE_USER = Old_insert_entry.CREATE_USER;
            }
            //***********************************************header部End******************************************


            //***********************************************明細部Start******************************************
            //入金明細の登録（画面モードが削除の場合、データ取得しない。）
            if (gamen_Mode != 3)
            {
                List_meisai_data_new = new List<T_NYUUKIN_DETAIL>();
                SqlInt16 RowNumber = 0;
                for (int i = 0; i < this.Form.MeisaiIchiran.Rows.Count - 1; i++)
                {

                    if (this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_CD"].Value != null && this.Form.MeisaiIchiran.Rows[i].Cells["KINGAKU"].Value != null)
                    {
                        RowNumber = RowNumber + 1;
                        Meisai_data = new T_NYUUKIN_DETAIL();
                        Meisai_data.SYSTEM_ID = Insert_entry.SYSTEM_ID;
                        //元の枝番ば削除されるからこちらで＋１にする
                        if (this.Gamen_Mode == 1)
                        {
                            Meisai_data.SEQ = 1;
                        }
                        else
                        {
                            Meisai_data.SEQ = SqlInt32.Parse(this.Seq) + 1;
                        }
                        Meisai_data.ROW_NUMBER = RowNumber;
                        Meisai_data.NYUUSHUKKIN_KBN_CD = SqlInt16.Parse(this.Form.MeisaiIchiran.Rows[i].Cells["NYUUSHUKKIN_KBN_CD"].Value.ToString());
                        Meisai_data.KINGAKU = SqlDecimal.Parse(this.Form.MeisaiIchiran.Rows[i].Cells["KINGAKU"].Value.ToString().Replace(",", ""));
                        //meisai_data.MEISAI_BIKOU = this.form.MeisaiIchiran.Rows[i].Cells["MEISAI_BIKOU"].ToString();
                        Meisai_data.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(SqlInt16.Parse(DENSHU_KBN.NYUUKIN.GetHashCode().ToString()));
                        //新しい明細を更新listに追加
                        List_meisai_data_new.Add(Meisai_data);
                    }
                }
            }
            //***********************************************明細部End******************************************

            //***********************************************取消部Start******************************************  
            List_kesikomi_data_new = new List<T_NYUUKIN_KESHIKOMI>();

            String MaxKeshikomiSeq = "0";
            for (int i = 0; i < this.Form.KesikomiIchiran.Rows.Count; i++)
            {
                if (i == 0)
                {
                    MaxKeshikomiSeq = Search_MaxKesikomeSeq(Insert_entry.SYSTEM_ID);
                }

                Kesikomi_data_new = new T_NYUUKIN_KESHIKOMI();
                Kesikomi_data_new.SEIKYUU_NUMBER = SqlInt64.Parse(BlankToZero(this.Form.KesikomiIchiran.Rows[i].Cells["SEIKYUU_NUMBER"].Value.ToString()));
                //開始残高の場合は請求番号を0に固定
                if (SearchResult_kaishi_urikake.Rows.Count > 0 && i == 0)
                {
                    //Kesikomi_data_old.SEIKYUU_NUMBER = 0;
                    Kesikomi_data_new.SEIKYUU_NUMBER = 0;
                }

                //変更された行のデータを取得する
                if (this.Gamen_Mode != 1)//新規じゃない場合の消込一覧のデータを取得
                {
                    //画面モードが修正の場合、新しい消込一覧のデータを取得
                    if (gamen_Mode == 2)
                    {
                        Kesikomi_data_new.KESHIKOMI_SEQ = SqlInt32.Parse(MaxKeshikomiSeq) + 1;
                        Kesikomi_data_new.SYSTEM_ID = Insert_entry.SYSTEM_ID;
                        Kesikomi_data_new.NYUUKIN_NUMBER = Insert_entry.NYUUKIN_NUMBER;
                        Kesikomi_data_new.KESHIKOMI_GAKU = SqlDecimal.Parse(NullToZero(this.Form.KesikomiIchiran.Rows[i].Cells["NEW_KESHIKOMI_GAKU"].Value).ToString().Replace(",", ""));
                        Kesikomi_data_new.NYUUKIN_SEQ = Insert_entry.SEQ;
                        Kesikomi_data_new.SEIKYUU_NUMBER = SqlInt64.Parse(BlankToZero(this.Form.KesikomiIchiran.Rows[i].Cells["SEIKYUU_NUMBER"].Value.ToString()));
                        Kesikomi_data_new.DELETE_FLG = false;
                        List_kesikomi_data_new.Add(Kesikomi_data_new);
                    }
                }
                else//新規の場合の消込一覧のデータを取得
                {
                    Kesikomi_data_new.KESHIKOMI_SEQ = 0;
                    Kesikomi_data_new.SYSTEM_ID = Insert_entry.SYSTEM_ID;
                    Kesikomi_data_new.NYUUKIN_NUMBER = Insert_entry.NYUUKIN_NUMBER;
                    Kesikomi_data_new.KESHIKOMI_GAKU = SqlDecimal.Parse(NullToZero(this.Form.KesikomiIchiran.Rows[i].Cells["NEW_KESHIKOMI_GAKU"].Value).ToString().Replace(",", ""));
                    Kesikomi_data_new.NYUUKIN_SEQ = Insert_entry.SEQ;
                    Kesikomi_data_new.DELETE_FLG = false;
                    List_kesikomi_data_new.Add(Kesikomi_data_new);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region クリア処理

        /// <summary>
        /// 初期化
        /// </summary>
        /// /// <returns></returns>
        public bool Clear()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                this.Form.DENPYOU_DATE.Value = this.ParentForm.sysDate;
                this.Form.NYUKIN_NO.Text = "";
                this.Form.GINKOU_CD.Text = "";
                this.Form.GINKOU_SHITEN_CD.Text = "";
                this.Form.GINKOU_NAME.Text = "";
                this.Form.GINKOU_SHITEN_NAME.Text = "";
                this.Form.KOUZASYURUI_NAME.Text = "";
                this.Form.KOUZASYURUI_NO.Text = "";
                this.Form.TORIHIKISAKI_NO.Text = "";
                this.Form.TORIHIKISAKI_NAME.Text = "";
                this.Form.TORIHIKI_KUBUN.Text = "";
                this.Form.KAISYUBI_DATE.Text = "";
                this.Form.SIMEBI_1.Text = "";
                this.Form.SIMEBI_2.Text = "";
                this.Form.SIMEBI_3.Text = "";
                this.Form.EIGYOUTANTOU_NO.Text = "";
                this.Form.EIGYOUTANTOU_NAME.Text = "";
                this.Form.ZENKAIGOSEIKYUGAKU_TXT.Text = "";
                this.Form.KONKAIGOUKEIGAKU_TXT.Text = "";
                this.Form.KONKAINYUKINGAKU_TXT.Text = "";
                this.Form.CHOUSEIGAKU_TXT.Text = "";
                this.Form.BIKOU_TXT.Text = "";
                this.Form.SIMESYORIJYOUKYOU_TXT.Text = "";
                this.Form.NyukinGoukeigaku.Text = "";
                this.Form.NYUKINGAKUSAGAKU_TXT.Text = "";

                //明細一覧クリア
                if (this.Form.MeisaiIchiran.Rows.Count > 1)
                {
                    for (int i = this.Form.MeisaiIchiran.Rows.Count; i > 1; i--)
                    {
                        this.Form.MeisaiIchiran.Rows.Remove(this.Form.MeisaiIchiran.Rows[i - 2]);
                    }
                }
                //消込一覧クリア
                for (int i = this.Form.KesikomiIchiran.Rows.Count; i > 0; i--)
                {
                    this.Form.KesikomiIchiran.Rows.Remove(this.Form.KesikomiIchiran.Rows[i - 1]);
                }

                if (this.Gamen_Mode == 3)
                {
                    SetGamenHeader(1);
                }
                else
                {
                    SetGamenHeader(this.Gamen_Mode);
                }
                if (this.Gamen_Mode == 1)
                {
                    this.Nyuukin_Number = "";
                }

                //登録ボタンが使える
                var parentForm = (BusinessBaseForm)this.Form.Parent;
                parentForm.bt_func2.Enabled = true;
                this.Form.DENPYOU_DATE.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Clear", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 画面モードを変更する.

        //画面モードを変更する.
        public void ChangeGamenMode()
        {
            LogUtility.DebugMethodStart();
            this.Gamen_Mode = 1;
            this.Header.CREATE_DATE.Text = "";
            this.Header.CREATE_USER.Text = "";
            this.Header.UPDATE_DATE.Text = "";
            this.Header.UPDATE_USER.Text = "";
            this.Form.NYUKIN_NO.ReadOnly = false;
            this.Header.Gamen_Mode.Text = "新規";
            var parentForm = (BusinessBaseForm)this.Form.Parent;
            parentForm.bt_func9.Enabled = true;
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 今回入金額と調整額のセット

        /// <summary>
        /// 今回入金額と調整額のセット
        /// </summary>
        /// /// <returns></returns>
        public bool DoChangeKingaku()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                //今回入金額
                decimal d1 = 0;
                //調整額
                decimal d2 = 0;

                for (int i = 0; i < this.Form.MeisaiIchiran.Rows.Count - 1; i++)
                {
                    if (this.Form.MeisaiIchiran.Rows[i].Cells[1].Value != null && this.Form.MeisaiIchiran.Rows[i].Cells[3].Value != null)
                    {
                        if (int.Parse(this.Form.MeisaiIchiran.Rows[i].Cells[1].Value.ToString()) > 20)
                        {
                            d2 = d2 + decimal.Parse(this.Form.MeisaiIchiran.Rows[i].Cells[3].Value.ToString());
                        }
                        else
                        {
                            d1 = d1 + decimal.Parse(this.Form.MeisaiIchiran.Rows[i].Cells[3].Value.ToString());
                        }
                    }
                    if (this.Form.MeisaiIchiran.Rows[i].Cells[1].Value == null && this.Form.MeisaiIchiran.Rows[i].Cells[3].Value == null)
                    {
                        this.Form.MeisaiIchiran.Rows[i].Cells[2].Value = null;
                    }
                }
                this.Form.CHOUSEIGAKU_TXT.Text = KingakuSiki(ZeroToBlank(d2));
                this.Form.KONKAINYUKINGAKU_TXT.Text = KingakuSiki(ZeroToBlank(d1));
                this.Form.KONKAIGOUKEIGAKU_TXT.Text = KingakuSiki(ZeroToBlank(d2 + d1));
                if (this.Form.NyukinGoukeigaku.Text != "")
                {
                    this.Form.NYUKINGAKUSAGAKU_TXT.Text = KingakuSiki((decimal.Parse(BlankToZero(this.Form.KONKAIGOUKEIGAKU_TXT.Text.Replace(",", ""))) - decimal.Parse(BlankToZero(this.Form.NyukinGoukeigaku.Text.Replace(",", "")))).ToString());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DoChangeKingaku", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 取引先コードが変更されるときの処理

        /// <summary>
        /// 取引先コードが変更されるときの処理
        /// </summary>
        /// /// <returns></returns>
        public bool ChangeTorihikisaki(String torihikisaki)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisaki);
                //取引先名が空の場合、クリアする

                this.Form.ZENKAIGOSEIKYUGAKU_TXT.Text = "";
                this.Form.KAISYUBI_DATE.Text = "";
                this.Form.SIMEBI_1.Text = "";
                this.Form.SIMEBI_2.Text = "";
                this.Form.SIMEBI_3.Text = "";
                this.Form.TORIHIKI_KUBUN.Text = "";
                this.Form.NyukinGoukeigaku.Text = "";
                this.Form.NYUKINGAKUSAGAKU_TXT.Text = "";
                this.Form.TORIHIKISAKI_NAME.Text = "";
                ClearKesikomi();

                if (this.Form.TORIHIKISAKI_NO.Text != "")
                {

                    DTOClass Dto_Torihikisaki = new DTOClass();
                    Dto_Torihikisaki.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));
                    Dto_Torihikisaki.Torihikisaki_cd = this.Form.TORIHIKISAKI_NO.Text;
                    DataTable Check_Kyoten = new DataTable();
                    Check_Kyoten = Dao_CheckTorihikisaki.GetDataForEntity(Dto_Torihikisaki);
                    var table = Check_Kyoten;
                    if (table.Rows.Count > 0)
                    {
                        this.Form.TORIHIKISAKI_NAME.Text = table.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();

                        //前回御請求額再検索
                        this.Search_zenkai(this.Form.TORIHIKISAKI_NO.Text, this.Form.DENPYOU_DATE.Value.ToString());
                        //前回御請求額セット
                        this.SetResZenkai();

                        //取引区分名を取得
                        this.Search_torihiki(this.Form.TORIHIKISAKI_NO.Text);
                        this.IsTorihikiChanged = false;
                    }
                    else
                    {
                        MessageShowLogic.MessageBoxShow("E020", "取引先");
                        this.Form.TORIHIKISAKI_NAME.Text = "";
                        this.Form.TORIHIKISAKI_NO.Focus();
                        //return;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeTorihikisaki", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikisaki", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 取引先コードが変更されるときの処理
        /// </summary>
        /// /// <returns></returns>
        public bool ChangeTorihikisakiRyakusyo(String torihikisaki)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisaki);
                //取引先名が空の場合、クリアする

                if (this.Form.TORIHIKISAKI_NAME.Text == "")
                {
                    this.Form.ZENKAIGOSEIKYUGAKU_TXT.Text = "";
                    this.Form.KAISYUBI_DATE.Text = "";
                    this.Form.SIMEBI_1.Text = "";
                    this.Form.SIMEBI_2.Text = "";
                    this.Form.SIMEBI_3.Text = "";
                    this.Form.TORIHIKI_KUBUN.Text = "";
                    this.Form.NyukinGoukeigaku.Text = "";
                    this.Form.NYUKINGAKUSAGAKU_TXT.Text = "";
                    return ret;
                }
                ClearKesikomi();

                if (this.Form.TORIHIKISAKI_NO.Text != "")
                {
                    DTOClass Dto_Torihikisaki = new DTOClass();
                    Dto_Torihikisaki.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));
                    Dto_Torihikisaki.Torihikisaki_cd = this.Form.TORIHIKISAKI_NO.Text;
                    DataTable Check_Kyoten = new DataTable();
                    Check_Kyoten = Dao_CheckTorihikisaki.GetDataForEntity(Dto_Torihikisaki);
                    var table = Check_Kyoten;
                    if (table.Rows.Count > 0)
                    {
                        this.Form.TORIHIKISAKI_NAME.Text = table.Rows[0]["TORIHIKISAKI_NAME_RYAKU"].ToString();

                        //前回御請求額再検索
                        this.Search_zenkai(this.Form.TORIHIKISAKI_NO.Text, this.Form.DENPYOU_DATE.Value.ToString());
                        //前回御請求額セット
                        this.SetResZenkai();

                        //取引区分名を取得
                        this.Search_torihiki(this.Form.TORIHIKISAKI_NO.Text);
                        this.IsTorihikiChanged = false;
                    }
                    else
                    {
                        MessageShowLogic.MessageBoxShow("E020", "取引先");
                        this.Form.TORIHIKISAKI_NAME.Text = "";
                        this.Form.TORIHIKISAKI_NO.Focus();
                        //return;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeTorihikisakiRyakusyo", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikisakiRyakusyo", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 消込入金額の修正するときの処理

        /// <summary>
        /// 消込入金額の修正するときの処理
        /// </summary>
        /// /// <returns></returns>
        public bool ChangeKesikomiGaku(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                decimal Sagaku = 0;
                string MinyuGaku = NullToBlank(this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells["MINYU_GAKU"].Value).ToString();
                string Nyukingaku = NullToBlank(this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells["NEW_KESHIKOMI_GAKU"].Value).ToString();
                string OldNyukingaku = NullToBlank(this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells["OLD_KESIKOMI_GAKU"].Value).ToString();
                string OldMinyukingaku = NullToBlank(this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells["OLD_MINYU_GAKU"].Value).ToString();
                Sagaku = decimal.Parse(BlankToZero(NullToBlank(OldNyukingaku))) - decimal.Parse(BlankToZero(Nyukingaku));

                this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells["MINYU_GAKU"].Value =
                    decimal.Parse(BlankToZero(OldMinyukingaku)) + Sagaku;

                SetKesikomiHeader();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeKesikomiGaku", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 消込情報を再検索(抽出ボタン)

        /// <summary>
        /// 消込情報を再検索
        /// </summary>
        /// /// <returns></returns>
        public bool SearchKeshikomi()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //取引先が空の場合
                if (this.Form.TORIHIKISAKI_NO.Text == "")
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E001", "取引先");
                    this.Form.TORIHIKISAKI_NO.Focus();
                    return ret;
                }

                //消込一覧をクリアする。

                ClearKesikomi();
                this.Form.NyukinGoukeigaku.Text = "";
                this.Form.NYUKINGAKUSAGAKU_TXT.Text = "";
                //開始残高
                this.Search_kaishi_urikake(this.Form.NYUKIN_NO.Text, this.Form.TORIHIKISAKI_NO.Text, this.Form.DENPYOU_DATE.Value.ToString());
                this.SetResKaishiUrikake();

                //消込明細部の検索
                this.Search_kesikomi(this.Form.NYUKIN_NO.Text, this.Form.TORIHIKISAKI_NO.Text, this.Form.DENPYOU_DATE.Value.ToString());

                //消込明細部のセット
                this.SetResKesiKomi();

                this.SetKesikomiHeader();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchKeshikomi", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKeshikomi", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 格式変換

        /// <summary>
        /// 空白をゼロに代わる
        /// </summary>
        /// /// <returns></returns>
        public String BlankToZero(string checkText)
        {
            LogUtility.DebugMethodStart(checkText);
            if (checkText == "")
            {
                LogUtility.DebugMethodEnd();
                return "0";
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return checkText;
            }
        }

        /// <summary>
        /// 空白をゼロに代わる
        /// </summary>
        /// /// <returns></returns>
        public String ZeroToBlank(decimal checkText)
        {
            LogUtility.DebugMethodStart(checkText);
            if (checkText == 0)
            {
                LogUtility.DebugMethodEnd();
                return "";
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return checkText.ToString();
            }
        }

        /// <summary>
        /// 金額の右の0を消す
        /// </summary>
        /// /// <returns></returns>
        public String KingakuSiki(string s)
        {
            LogUtility.DebugMethodStart(s);

            if (s == "" || s == "0")
            {
                return "0";
            }
            //"."がある時0を消す(例え10.000。10000の場合は消さない)
            if (s.IndexOf(".") > 0)
            {
                s = s.TrimEnd(new char[] { '0' });
            }
            if (s.EndsWith("."))
            {
                s = s.Replace(".", "");
            }
            //金額式で入った文字列は数字に変更する。
            s = s.Replace(",", "");
            LogUtility.DebugMethodEnd();
            return (int.Parse(s)).ToString("#,0");
        }

        //nullを空白に変更する。
        public String NullToBlank(Object obj)
        {
            LogUtility.DebugMethodStart(obj);
            if (obj == null)
            {
                LogUtility.DebugMethodEnd();
                return "";
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return obj.ToString();
            }
        }

        //nullをゼロに変更する。
        public String NullToZero(Object obj)
        {
            LogUtility.DebugMethodStart(obj);

            if (obj == null)
            {
                LogUtility.DebugMethodEnd();
                return "0";
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return obj.ToString();
            }
        }

        #endregion

        #region 明細一覧,消込一覧をクリアする


        /// <summary>
        /// 明細一覧をクリアする
        /// </summary>
        /// /// <returns></returns>
        private void ClearMeisai()
        {
            LogUtility.DebugMethodStart();
            if (this.Form.MeisaiIchiran.Rows.Count != 1)
            {
                for (int i = this.Form.MeisaiIchiran.Rows.Count; i > 1; i--)
                {
                    this.Form.MeisaiIchiran.Rows.Remove(this.Form.MeisaiIchiran.Rows[i - 2]);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 消込一覧をクリアする
        /// </summary>
        /// /// <returns></returns>
        private void ClearKesikomi()
        {
            LogUtility.DebugMethodStart();
            for (int i = this.Form.KesikomiIchiran.Rows.Count; i > 0; i--)
            {
                this.Form.KesikomiIchiran.Rows.Remove(this.Form.KesikomiIchiran.Rows[i - 1]);
            }
            this.Form.NyukinGoukeigaku.Text = "";
            this.Form.NYUKINGAKUSAGAKU_TXT.Text = "";
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 入出金一覧画面ボタン

        /// <summary>
        /// 一覧画面表示
        /// </summary>
        public bool Ichiran()
        {
            bool ret = true;
            try
            {
                FormManager.OpenForm("G078");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 実装しない

        /// <summary>
        /// 実装しない
        /// </summary>
        public int Search()
        {
            return 1;
        }

        /// <summary>
        /// 実装しない
        /// </summary>
        public void LogicalDelete()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 実装しない
        /// </summary>
        public void PhysicalDelete()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 実装しない
        /// </summary>
        public void Regist(bool bl)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 実装しない
        /// </summary>
        public void Update(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region 項目入力制御

        /// <summary>
        /// 入力不可能
        /// </summary>
        public void ReadonlyTrue()
        {
            LogUtility.DebugMethodStart();

            this.Form.DENPYOU_DATE.Enabled = false;
            this.Form.TorihikiPopupButton.Enabled = false;
            this.Form.TORIHIKISAKI_NO.ReadOnly = true;
            this.Form.EIGYOUTANTOU_NO.ReadOnly = true;
            this.Form.EigyouPopupOpenButton.Enabled = false;
            this.Form.MeisaiIchiran.ReadOnly = true;
            this.Form.KesikomiIchiran.ReadOnly = true;
            this.Header.KYOTEN_CD.ReadOnly = true;
            this.Form.GINKOU_CD.ReadOnly = true;
            this.Form.GINKOU_SHITEN_CD.ReadOnly = true;
            this.Form.BIKOU_TXT.ReadOnly = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力可能
        /// </summary>
        public void ReadonlyFalse()
        {
            LogUtility.DebugMethodStart();

            this.Form.DENPYOU_DATE.Enabled = true;
            this.Form.TorihikiPopupButton.Enabled = true;
            this.Form.EigyouPopupOpenButton.Enabled = true;
            this.Form.TORIHIKISAKI_NO.ReadOnly = false;
            this.Form.EIGYOUTANTOU_NO.ReadOnly = false;
            this.Form.MeisaiIchiran.ReadOnly = false;
            this.Form.MeisaiIchiran.Columns[0].ReadOnly = true;
            this.Form.MeisaiIchiran.Columns[2].ReadOnly = true;
            this.Form.KesikomiIchiran.ReadOnly = false;
            this.Form.KesikomiIchiran.Columns[0].ReadOnly = true;
            this.Form.KesikomiIchiran.Columns[1].ReadOnly = true;
            this.Form.KesikomiIchiran.Columns[2].ReadOnly = true;
            this.Form.KesikomiIchiran.Columns[4].ReadOnly = true;
            this.Header.KYOTEN_CD.ReadOnly = false;
            this.Form.GINKOU_CD.ReadOnly = false;
            this.Form.GINKOU_SHITEN_CD.ReadOnly = false;
            this.Form.BIKOU_TXT.ReadOnly = false;
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 金額のカンマ切替(消込一覧)

        //金額セルをクリックしたらカンマなしで表示
        internal bool ChangeMoneyValue_NoGanma_Meisai(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                if (this.Form.MeisaiIchiran.CurrentCell.Value != null && this.Form.MeisaiIchiran.Columns[e.ColumnIndex].HeaderText == "金額※")
                {
                    this.Form.MeisaiIchiran.CurrentCell.Value = this.Form.MeisaiIchiran.CurrentCell.Value.ToString().Replace(",", "");
                }
                this.Form.MeisaiIchiran.BeginEdit(true);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeMoneyValue_NoGanma_Meisai", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        //金額のセルからフォカスを離れたらカンマ付　&&　入金区分チェック
        internal bool ChangeMoneyValue_HaveGanma_Meisai(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.Form.MeisaiIchiran.CurrentCell.Value != null && this.Form.MeisaiIchiran.Columns[e.ColumnIndex].HeaderText == "金額※")
                {
                    this.Form.MeisaiIchiran.CurrentCell.Value = KingakuSiki(this.Form.MeisaiIchiran.CurrentCell.Value.ToString().Replace(",", ""));
                }

                //入金区分のロストフォカス処理で存在チェック
                if (e.ColumnIndex == 1)
                {
                    //if (this.Form.MeisaiIchiran.CurrentCell.Value == null)
                    if (this.Form.MeisaiIchiran.CurrentCell.EditedFormattedValue == null || this.Form.MeisaiIchiran.CurrentCell.EditedFormattedValue == "")
                    {
                        this.Form.MeisaiIchiran.Rows[e.RowIndex].Cells[2].Value = null;
                    }
                    else
                    {
                        DTOClass Dto_NyuushukinKbn = new DTOClass();
                        Dto_NyuushukinKbn.Denpyou_Date = DateTime.Parse(this.Form.DENPYOU_DATE.Text.Substring(0, 10));
                        //Dto_NyuushukinKbn.NyuushukinKbn = this.Form.MeisaiIchiran.CurrentCell.Value.ToString();
                        Dto_NyuushukinKbn.NyuushukinKbn = this.Form.MeisaiIchiran.CurrentCell.EditedFormattedValue.ToString();
                        DataTable NyuushukinKbn = new DataTable();
                        NyuushukinKbn = Dao_CheckNyuushukinKbn.GetDataForEntity(Dto_NyuushukinKbn);
                        var table = NyuushukinKbn;
                        if (table.Rows.Count > 0)
                        {
                            this.Form.MeisaiIchiran.Rows[e.RowIndex].Cells[2].Value = table.Rows[0]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString();
                        }
                        else
                        {
                            MessageShowLogic.MessageBoxShow("E020", "入金区分");
                            this.Form.MeisaiIchiran.Rows[e.RowIndex].Cells[2].Value = null;
                            this.Form.MeisaiIchiran.CurrentCell = this.Form.MeisaiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeMoneyValue_HaveGanma_Meisai", ex1);
                this.MessageShowLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeMoneyValue_HaveGanma_Meisai", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 金額のカンマ切替(消込一覧)

        //金額セルをクリックしたらカンマなしで表示
        internal bool ChangeMoneyValue_NoGanma_Kesikomi(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //クリックする時カンマ付け金額をカンマなして表示する。
                if (this.Form.KesikomiIchiran.CurrentCell.Value != null && this.Form.KesikomiIchiran.Columns[e.ColumnIndex].HeaderText == "入金額")
                {
                    this.Form.KesikomiIchiran.CurrentCell.Value = this.Form.KesikomiIchiran.CurrentCell.Value.ToString().Replace(",", "");
                }
                this.Form.KesikomiIchiran.BeginEdit(true);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeMoneyValue_NoGanma_Kesikomi", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        //金額のセルからフォカスを離れたらカンマ付
        internal bool ChangeMoneyValue_HaveGanma_Kesikomi(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.Form.KesikomiIchiran.CurrentCell.Value != null && this.Form.KesikomiIchiran.Columns[e.ColumnIndex].HeaderText == "入金額")
                {
                    //入金額のカンマ修正
                    this.Form.KesikomiIchiran.CurrentCell.Value = KingakuSiki(this.Form.KesikomiIchiran.CurrentCell.Value.ToString().Replace(",", ""));
                    //未入金額のカンマ修正
                    if (this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value != null)
                    {
                        this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = KingakuSiki(this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString().Replace(",", ""));
                    }
                    else
                    {
                        this.Form.KesikomiIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeMoneyValue_HaveGanma_Kesikomi", ex);
                this.MessageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion
    }
}