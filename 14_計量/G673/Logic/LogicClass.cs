using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Scale.KeiryouIchiran.APP;
using Shougun.Core.Scale.KeiryouIchiran.DAO;
using Shougun.Core.Scale.KeiryouIchiran.DBAccesser;

namespace Shougun.Core.Scale.KeiryouIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        // 基本計量
        private static readonly string KIHON_KEIRYOU_UKEIRE = "1";
        private static readonly string KIHON_KEIRYOU_SHUKKA = "2";
        private static readonly string KIHON_KEIRYOU_ALL = "3";

        // 計量区分
        private static readonly string KEIRYOU_KBN_TSUUJOU = "1";
        private static readonly string KEIRYOU_KBN_KARI = "2";
        private static readonly string KEIRYOU_KBN_KEIJOU = "3";
        private static readonly string KEIRYOU_KBN_ALL = "4";

        // 計量状況
        private static readonly string KEIRYOU_JYOUKYOU_KANRYOU = "1";
        private static readonly string KEIRYOU_JYOUKYOU_TAIRYUU = "2";

        // 滞留区分
        private static readonly string TAIRYUU_KBN_KANRYOU = "0";
        private static readonly string TAIRYUU_KBN_TAIRYUU = "1";

        #region 非表示にする必須項目

        /// <summary>
        /// 伝票番号
        /// </summary>
        private readonly string HIDDEN_DENPYOU_NUMBER = "HIDDEN_DENPYOU_NUMBER";

        /// <summary>
        /// 伝種区分CD
        /// </summary>
        private readonly string HIDDEN_DENSHU_KBN_CD = "HIDDEN_DENSHU_KBN_CD";

        /// <summary>
        /// システムID
        /// </summary>
        private readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// 明細システムID
        /// </summary>
        private readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        #endregion

        //初期表示フラグ
        private static bool InitialFlg = false;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Scale.KeiryouIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private DBAccessor accessor;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        /// 業者マスタ
        /// </summary>
        private IM_GYOUSHADao mgyoushaDao;

        /// <summary>
        /// 現場マスタ
        /// </summary>
        private IM_GENBADao mgenbaDao;

        /// <summary>
        /// 運搬業者マスタ
        /// </summary>
        private IM_GYOUSHADao munpanGyoushaDao;

        /// <summary>
        /// 車種マスタ
        /// </summary>
        private IM_SHASHUDao mshashuDao;

        /// <summary>
        /// 車輌マスタ
        /// </summary>
        private IM_SHARYOUDao msharyouDao;

        /// <summary>
        /// 計量一覧 form
        /// </summary>
        private KeiryouIchiranForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headForm;

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// コントロール
        /// </summary>
        private KensakuControl control;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// フッター
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 前回業者CD
        /// </summary>
        internal string BEFORE_GYOUSHA_CD;

        /// <summary>
        /// 前回車輌CD
        /// </summary>
        internal string BEFORE_SHARYOU_CD;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        internal DataTable searchResult { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        private string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        private string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        private string joinQuery { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(KeiryouIchiranForm targetForm)
        {

            this.form = targetForm;
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.mgyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mgenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.munpanGyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.msharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
            this.mshashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
            this.MsgBox = new MessageBoxShowLogic();

            // Accessor
            this.accessor = new DBAccessor();

            //検索方法によって、簡易検索／汎用検索を初期化する。
            KensakuControl hannyousearch = new KensakuControl(this);
            hannyousearch.Top = 3;
            hannyousearch.Left = 3;

            // 簡易検索をデフォルト
            this.form.searchString.Visible = false;
            this.control = hannyousearch;
            this.form.Controls.Add(hannyousearch.TORIHIKISAKI_LABEL);
            this.form.Controls.Add(hannyousearch.GYOUSHA_LABEL);
            this.form.Controls.Add(hannyousearch.GENBA_LABEL);
            this.form.Controls.Add(hannyousearch.UNPAN_GYOUSHA_LABEL);
            this.form.Controls.Add(hannyousearch.SHASHU_LABEL);
            this.form.Controls.Add(hannyousearch.SHARYOU_LABEL);

            this.form.Controls.Add(hannyousearch.TORIHIKISAKI_CD);
            this.form.Controls.Add(hannyousearch.GYOUSHA_CD);
            this.form.Controls.Add(hannyousearch.GENBA_CD);
            this.form.Controls.Add(hannyousearch.UNPAN_GYOUSHA_CD);
            this.form.Controls.Add(hannyousearch.SHASHU_CD);
            this.form.Controls.Add(hannyousearch.SHARYOU_CD);

            this.form.Controls.Add(hannyousearch.TORIHIKISAKI_NAME_RYAKU);
            this.form.Controls.Add(hannyousearch.GYOUSHA_NAME_RYAKU);
            this.form.Controls.Add(hannyousearch.GENBA_NAME_RYAKU);
            this.form.Controls.Add(hannyousearch.UNPAN_GYOUSHA_NAME_RYAKU);
            this.form.Controls.Add(hannyousearch.SHASHU_NAME_RYAKU);
            this.form.Controls.Add(hannyousearch.SHARYOU_NAME_RYAKU);

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
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                //headerFormにSettingsの値
                if (!this.SetDenpyouHidukeInit())
                {
                    return false;
                }

                #region ポップアップ設定

                //前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
                //拠点CDを取得  
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                // ユーザ拠点名称の取得
                if (this.headForm.KYOTEN_CD.Text != null)
                {
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                    if (mKyoten == null || this.headForm.KYOTEN_CD.Text == "")
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }

                // 基本計量
                this.form.KIHON_KEIRYOU_CD.Text = KIHON_KEIRYOU_ALL;      // 初期化
                var kihonKeiryou = Properties.Settings.Default.SET_KIHON_KEIRYOU_CD;
                if (kihonKeiryou != null && !string.IsNullOrEmpty(kihonKeiryou))
                {
                    if (KIHON_KEIRYOU_UKEIRE.Equals(kihonKeiryou))
                    {
                        this.form.KIHON_KEIRYOU_CD.Text = KIHON_KEIRYOU_UKEIRE;
                    }
                    else if (KIHON_KEIRYOU_SHUKKA.Equals(kihonKeiryou))
                    {
                        this.form.KIHON_KEIRYOU_CD.Text = KIHON_KEIRYOU_SHUKKA;
                    }
                }

                // 計量区分
                this.form.KEIRYOU_KBN_CD.Text = KEIRYOU_KBN_ALL;      // 初期化
                var keiryoukbn = Properties.Settings.Default.SET_KEIRYOU_KBN_CD;
                if (keiryoukbn != null && !string.IsNullOrEmpty(keiryoukbn))
                {
                    if (KEIRYOU_KBN_TSUUJOU.Equals(keiryoukbn))
                    {
                        this.form.KEIRYOU_KBN_CD.Text = KEIRYOU_KBN_TSUUJOU;
                    }
                    else if (KEIRYOU_KBN_KARI.Equals(keiryoukbn))
                    {
                        this.form.KEIRYOU_KBN_CD.Text = KEIRYOU_KBN_KARI;
                    }
                    else if (KEIRYOU_KBN_KEIJOU.Equals(keiryoukbn))
                    {
                        this.form.KEIRYOU_KBN_CD.Text = KEIRYOU_KBN_KEIJOU;
                    }
                }

                // 計量状況
                this.form.KEIRYOU_JOUKYOU_CD.Text = KEIRYOU_JYOUKYOU_KANRYOU;      // 初期化
                var keiryouJyoukyou = Properties.Settings.Default.SET_KEIRYOU_JYOUKYOU_CD;
                if (keiryouJyoukyou != null && !string.IsNullOrEmpty(keiryouJyoukyou))
                {
                    if (KEIRYOU_JYOUKYOU_KANRYOU.Equals(keiryouJyoukyou))
                    {
                        this.form.KEIRYOU_JOUKYOU_CD.Text = KEIRYOU_JYOUKYOU_KANRYOU;
                    }
                    else if (KEIRYOU_JYOUKYOU_TAIRYUU.Equals(keiryouJyoukyou))
                    {
                        this.form.KEIRYOU_JOUKYOU_CD.Text = KEIRYOU_JYOUKYOU_TAIRYUU;
                    }
                }
                #endregion

                this.allControl = this.form.allControl;
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                //検索方法によって、簡易検索／汎用検索を初期化する。

                ////汎用検索の取得
                control.TORIHIKISAKI_CD.Text = Properties.Settings.Default.SET_TORIHIKISAKI_CD;
                control.GYOUSHA_CD.Text = Properties.Settings.Default.SET_GYOUSHA_CD;
                BEFORE_GYOUSHA_CD = control.GYOUSHA_CD.Text;
                if (control.GYOUSHA_CD.Text != "")
                {
                    control.GENBA_CD.Text = Properties.Settings.Default.SET_GENBA_CD;
                }
                control.UNPAN_GYOUSHA_CD.Text = Properties.Settings.Default.SET_UNPAN_GYOUSHA_CD;
                control.SHASHU_CD.Text = Properties.Settings.Default.SET_SHASHU_CD;
                control.SHARYOU_CD.Text = Properties.Settings.Default.SET_SHARYOU_CD;
                BEFORE_SHARYOU_CD = control.SHARYOU_CD.Text;

                // ユーザ取引先名称の取得
                if (control.TORIHIKISAKI_CD.Text != string.Empty)
                {
                    M_TORIHIKISAKI mtorihikisaki = new M_TORIHIKISAKI();
                    mtorihikisaki = (M_TORIHIKISAKI)mtorihikisakiDao.GetDataByCd(control.TORIHIKISAKI_CD.Text);
                    if (mtorihikisaki == null)
                    {
                        control.TORIHIKISAKI_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.TORIHIKISAKI_NAME_RYAKU.Text = mtorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                // ユーザ業者名称の取得
                if (control.GYOUSHA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mGyousha = new M_GYOUSHA();
                    mGyousha = (M_GYOUSHA)mgyoushaDao.GetDataByCd(control.GYOUSHA_CD.Text);
                    if (mGyousha == null)
                    {
                        control.GYOUSHA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.GYOUSHA_NAME_RYAKU.Text = mGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // ユーザ現場名称の取得
                if (control.GYOUSHA_CD.Text != string.Empty && control.GENBA_CD.Text != string.Empty)
                {
                    M_GENBA mGenbaOut = new M_GENBA();
                    M_GENBA mGenbaIn = new M_GENBA();
                    mGenbaIn.GYOUSHA_CD = control.GYOUSHA_CD.Text;
                    mGenbaIn.GENBA_CD = control.GENBA_CD.Text;
                    mGenbaOut = (M_GENBA)mgenbaDao.GetDataByCd(mGenbaIn);
                    if (mGenbaOut == null)
                    {
                        control.GENBA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.GENBA_NAME_RYAKU.Text = mGenbaOut.GENBA_NAME_RYAKU;
                    }
                }

                // ユーザ運搬業者名称の取得
                if (control.UNPAN_GYOUSHA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mUnpanGyousha = new M_GYOUSHA();
                    mUnpanGyousha = (M_GYOUSHA)munpanGyoushaDao.GetDataByCd(control.UNPAN_GYOUSHA_CD.Text);
                    if (mUnpanGyousha == null)
                    {
                        control.UNPAN_GYOUSHA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.UNPAN_GYOUSHA_NAME_RYAKU.Text = mUnpanGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // ユーザ車種名称の取得
                if (control.SHASHU_CD.Text != string.Empty)
                {
                    M_SHASHU mshashu = new M_SHASHU();
                    mshashu = (M_SHASHU)mshashuDao.GetDataByCd(control.SHASHU_CD.Text);
                    if (mshashu == null)
                    {
                        control.SHASHU_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.SHASHU_NAME_RYAKU.Text = mshashu.SHASHU_NAME_RYAKU;
                    }
                }

                // ユーザ車輌名称の取得
                if (control.SHARYOU_CD.Text != string.Empty)
                {
                    M_SHARYOU msharyou = new M_SHARYOU();
                    msharyou.GYOUSHA_CD = control.UNPAN_GYOUSHA_CD.Text;
                    msharyou.SHARYOU_CD = control.SHARYOU_CD.Text;
                    msharyou = (M_SHARYOU)msharyouDao.GetDataByCd(msharyou);
                    if (msharyou == null)
                    {
                        control.SHARYOU_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.SHARYOU_NAME_RYAKU.Text = msharyou.SHARYOU_NAME_RYAKU;
                    }
                }

                // 上で子フォームへのデータバインドを行っている為、以下の初期化処理はそれより下に配置（二度呼ばれることを避ける）
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //検索ボタンの初期表示
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle("計量一覧");

                Control[] copy = new Control[this.form.allControl.GetLength(0)];
                this.form.allControl.CopyTo(copy, 0);
                this.allControl = copy;

                // 滞留件数等表示
                this.SetKensu();

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {

            parentForm = (BusinessBaseForm)this.form.Parent;

            //Functionボタンのイベント生成
            parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              //F2 新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);       //F3 修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);       //F4 削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);       //F5 複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);       //F6 CSV出力
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //F7 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //F8 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //F10 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

            //明細画面上でダブルクリック時のイベント生成
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

            //日付TOでダブルクリック時のイベント生成
            this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);

        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F2 新規
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            EditDetail(WINDOW_TYPE.NEW_WINDOW_FLAG, "");
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                this.EditDetail(WINDOW_TYPE.UPDATE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                EditDetail(WINDOW_TYPE.DELETE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                EditDetail(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 一覧にデータ行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        CSVExport exp = new CSVExport();
                        exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(this.form.DenshuKbn), this.form);
                    }
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {

            this.form.searchString.Clear();

            // 一覧クリア
            DataTable cre = (DataTable)this.form.customDataGridView1.DataSource;
            if (cre == null)
            {
                return;
            }
            cre.Clear();
            this.form.customDataGridView1.DataSource = cre;

            // ヘッダ部クリア
            if (!this.SetDenpyouHidukeInit())
            {
                return;
            }
            this.headForm.ReadDataNumber.Text = "0";
            this.headForm.alertNumber.Clear();
            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();
            if (this.control.Visible == true)
            {
                this.control.TORIHIKISAKI_CD.Clear();
                this.control.TORIHIKISAKI_NAME_RYAKU.Clear();
                this.control.GYOUSHA_CD.Clear();
                this.control.GYOUSHA_NAME_RYAKU.Clear();
                this.control.GENBA_CD.Clear();
                this.control.GENBA_NAME_RYAKU.Clear();
                this.control.UNPAN_GYOUSHA_CD.Clear();
                this.control.UNPAN_GYOUSHA_NAME_RYAKU.Clear();
                this.control.SHASHU_CD.Clear();
                this.control.SHASHU_NAME_RYAKU.Clear();
                this.control.SHARYOU_CD.Clear();
                this.control.SHARYOU_NAME_RYAKU.Clear();
            }
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {

            // 滞留件数等表示
            this.SetKensu();

            if (this.form.PatternNo == 0)
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            bool searchErrorFlag = false;
            this.form.HIDUKE_FROM.IsInputErrorOccured = false;
            this.form.HIDUKE_TO.IsInputErrorOccured = false;
            var allControlAndHeaderControls = allControl.ToList();
            allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headForm));
            var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (!this.form.RegistErrorFlag)
            {
                if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.GetResultText())
                    && !string.IsNullOrEmpty(this.form.HIDUKE_TO.GetResultText()))
                {
                    DateTime dtpFrom = DateTime.Parse(this.form.HIDUKE_FROM.GetResultText());
                    DateTime dtpTo = DateTime.Parse(this.form.HIDUKE_TO.GetResultText());
                    DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                    DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                    int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                    if (0 < diff)
                    {
                        //対象期間内でないならエラーメッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                        this.form.HIDUKE_TO.IsInputErrorOccured = true;
                        MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                        this.form.HIDUKE_FROM.Select();
                        this.form.HIDUKE_FROM.Focus();
                        searchErrorFlag = true;
                    }
                }
            }

            // Ditailの行数チェックはFWでできないので自前でチェック
            if (!this.form.RegistErrorFlag && !searchErrorFlag)
            {
                if (this.form.rb_KIHON_KEIRYOU_UKEIRE.Checked || this.form.rb_KIHON_KEIRYOU_SHUKKA.Checked || this.form.rb_KIHON_KEIRYOU_ALL.Checked)
                {
                    //読込データ件数を取得
                    int count = this.Search();
                    if (count == -1)
                    {
                        return;
                    }
                    this.headForm.ReadDataNumber.Text = count.ToString();
                    if (this.form.customDataGridView1 != null)
                    {
                        this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                        this.headForm.ReadDataNumber.Text = "0";
                    }
                    if (this.headForm.ReadDataNumber.Text == "0")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
            }
            //必須チェックエラーフォーカス処理
            this.SetErrorFocus();

        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
            //読込データ件数
            if (this.form.customDataGridView1 != null)
            {
                this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #endregion

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.searchResult = this.form.Table;
                this.form.ShowData();
            }
            this.form.baseSelectQuery = this.form.SelectQuery;
            this.form.baseOrderByQuery = this.form.OrderByQuery;
            this.form.baseJoinQuery = this.form.JoinQuery;
        }

        #endregion

        #region ESCキー押下イベント
        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();
            }

        }

        #endregion

        #region 明細データダブルクリックイベント

        private void customDataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewCellMouseEventArgs datagridviewcell = (DataGridViewCellMouseEventArgs)e;
            if (datagridviewcell.RowIndex >= 0)
            {
                string denpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                this.EditDetail(WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyouNum);
            }
        }

        #endregion

        #region 検索計量一覧

        /// <summary>
        /// Where　条件
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeWhereSql(StringBuilder sql)
        {

            sql.Append(" WHERE ");

            sql.Append(" T_KEIRYOU_ENTRY.DELETE_FLG = 0 ");

            if (this.form.HIDUKE_FROM.Value != null || this.form.HIDUKE_TO.Value != null)
            {
                sql.Append(" AND ");
            }

            // 伝票日付：日時は日付のみにしてから変換
            if (this.form.HIDUKE_FROM.Value != null)
            {
                sql.Append(" CONVERT(DATETIME, CONVERT(nvarchar, T_KEIRYOU_ENTRY.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '");
                sql.Append(DateTime.Parse(this.form.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
            }

            if (this.form.HIDUKE_FROM.Value != null && this.form.HIDUKE_TO.Value != null)
            {
                sql.Append(" AND ");
            }

            if (this.form.HIDUKE_TO.Value != null)
            {
                sql.Append(" CONVERT(DATETIME, CONVERT(nvarchar, T_KEIRYOU_ENTRY.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '");
                sql.Append(DateTime.Parse(this.form.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
            }

            // 拠点
            if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text)
                && this.headForm.KYOTEN_CD.Text != CommonConst.KYOTEN_CD_ZENSHA.ToString())
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.KYOTEN_CD = '{0}' ", this.headForm.KYOTEN_CD.Text);
            }

            // 基本計量
            if (KIHON_KEIRYOU_UKEIRE.Equals(this.form.KIHON_KEIRYOU_CD.Text)
                || KIHON_KEIRYOU_SHUKKA.Equals(this.form.KIHON_KEIRYOU_CD.Text))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.KIHON_KEIRYOU = {0} ", this.form.KIHON_KEIRYOU_CD.Text);
            }

            // 計量区分
            if (KEIRYOU_KBN_TSUUJOU.Equals(this.form.KEIRYOU_KBN_CD.Text)
                || KEIRYOU_KBN_KARI.Equals(this.form.KEIRYOU_KBN_CD.Text)
                || KEIRYOU_KBN_KEIJOU.Equals(this.form.KEIRYOU_KBN_CD.Text))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.KEIRYOU_KBN = {0} ", this.form.KEIRYOU_KBN_CD.Text);
            }

            // 計量状況
            if (KEIRYOU_JYOUKYOU_KANRYOU.Equals(this.form.KEIRYOU_JOUKYOU_CD.Text))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.TAIRYUU_KBN = {0} ", TAIRYUU_KBN_KANRYOU);
            }
            if (KEIRYOU_JYOUKYOU_TAIRYUU.Equals(this.form.KEIRYOU_JOUKYOU_CD.Text))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.TAIRYUU_KBN = {0} ", TAIRYUU_KBN_TAIRYUU);
            }

            // 検索条件コントロール
            // 取引先
            if ((!String.IsNullOrEmpty(this.control.TORIHIKISAKI_CD.Text)))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.TORIHIKISAKI_CD = '{0}' ", this.control.TORIHIKISAKI_CD.Text);
            }
            // 業者
            if ((!String.IsNullOrEmpty(this.control.GYOUSHA_CD.Text)))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.GYOUSHA_CD = '{0}' ", this.control.GYOUSHA_CD.Text);
            }
            // 現場
            if ((!String.IsNullOrEmpty(this.control.GENBA_CD.Text)))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.GENBA_CD = '{0}' ", this.control.GENBA_CD.Text);
            }
            // 運搬業者
            if ((!String.IsNullOrEmpty(this.control.UNPAN_GYOUSHA_CD.Text)))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.UNPAN_GYOUSHA_CD = '{0}' ", this.control.UNPAN_GYOUSHA_CD.Text);
            }
            // 車種
            if ((!String.IsNullOrEmpty(this.control.SHASHU_CD.Text)))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.SHASHU_CD = '{0}' ", this.control.SHASHU_CD.Text);
            }
            // 車輌
            if ((!String.IsNullOrEmpty(this.control.SHARYOU_CD.Text)))
            {
                sql.AppendFormat(" AND T_KEIRYOU_ENTRY.SHARYOU_CD = '{0}' ", this.control.SHARYOU_CD.Text);
            }

        }

        /// <summary>
        /// 計量検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchKeiryou(StringBuilder sql)
        {
            sql.Append(" T_KEIRYOU_ENTRY ");

            // パターンの区分が明細の場合、明細テーブルを結合する
            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 2)
            {
                sql.Append(" LEFT JOIN T_KEIRYOU_DETAIL ");
                sql.Append(" ON T_KEIRYOU_ENTRY.SYSTEM_ID = T_KEIRYOU_DETAIL.SYSTEM_ID AND T_KEIRYOU_ENTRY.SEQ = T_KEIRYOU_DETAIL.SEQ ");
            }
            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 3)
            {
                sql.Append(" LEFT JOIN T_UKEIRE_JISSEKI_ENTRY ");
                sql.Append(" ON T_KEIRYOU_ENTRY.SYSTEM_ID = T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID AND T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI = 1 AND T_UKEIRE_JISSEKI_ENTRY.DELETE_FLG = 0 ");
                sql.Append(" LEFT JOIN T_UKEIRE_JISSEKI_DETAIL ");
                sql.Append(" ON T_UKEIRE_JISSEKI_DETAIL.DENPYOU_SHURUI = T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI AND T_UKEIRE_JISSEKI_DETAIL.DENPYOU_SYSTEM_ID = T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID AND T_UKEIRE_JISSEKI_DETAIL.SEQ = T_UKEIRE_JISSEKI_ENTRY.SEQ ");
                sql.Append(" LEFT JOIN M_FILE_LINK_UKEIRE_JISSEKI_ENTRY ");
                sql.Append(" ON T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI = M_FILE_LINK_UKEIRE_JISSEKI_ENTRY.DENPYOU_SHURUI AND T_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID = M_FILE_LINK_UKEIRE_JISSEKI_ENTRY.DENPYOU_SYSTEM_ID AND T_UKEIRE_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            }
        }
        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret_cnt = 0;

            try
            {
                //SELECT句未取得なら検索できない
                if (!string.IsNullOrEmpty(this.form.baseSelectQuery))
                {
                    // ベースロジッククラスで作成したクエリをセット
                    this.selectQuery = this.form.baseSelectQuery;
                    this.orderByQuery = this.form.baseOrderByQuery;
                    this.joinQuery = this.form.baseJoinQuery;

                    string tblName1 = string.Empty;
                    string tblName2 = string.Empty;
                    string tblName3 = string.Empty;
                    string denpyouNum = string.Empty;

                    tblName1 = "T_KEIRYOU_ENTRY";
                    tblName2 = "T_KEIRYOU_DETAIL";
                    tblName3 = "T_UKEIRE_JISSEKI_DETAIL";
                    denpyouNum = "KEIRYOU_NUMBER";

                    var order = new StringBuilder();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT DISTINCT ");
                    sql.Append(this.selectQuery);

                    #region システム上必須な項目をSELECT句に追加する（後で非表示にする）
                    sql.AppendFormat(" , {0}.{1} AS {2} ", tblName1, denpyouNum, this.HIDDEN_DENPYOU_NUMBER);
                    order.AppendFormat(" , {0} ASC ", this.HIDDEN_DENPYOU_NUMBER);

                    // 出力区分が明細の場合
                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 2)
                    {
                        sql.AppendFormat(" , {0}.DETAIL_SYSTEM_ID AS {1} ", tblName2, this.HIDDEN_DETAIL_SYSTEM_ID);
                        order.AppendFormat(" , {0} ASC ", this.HIDDEN_DETAIL_SYSTEM_ID);
                    }

                    // 出力区分が明細の場合
                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 3)
                    {
                        sql.AppendFormat(" , {0}.DETAIL_SEQ AS {1} ", tblName3, this.HIDDEN_DETAIL_SYSTEM_ID);
                        order.AppendFormat(" , {0} ASC ", this.HIDDEN_DETAIL_SYSTEM_ID);
                    }

                    string errorDefault = "\'0\'";
                    sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
                    sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
                    sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR", errorDefault);
                    sql.AppendFormat(" , {0}.SYSTEM_ID AS {1} ", tblName1, "HIDDEN_SEARCH_SYSTEM_ID");

                    #endregion

                    sql.Append(" FROM ");

                    this.MakeSearchKeiryou(sql);

                    sql.Append(this.joinQuery);
                    this.MakeWhereSql(sql);
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                    sql.Append(order);

                    //検索実行
                    this.searchResult = new DataTable();
                    if (!string.IsNullOrEmpty(sql.ToString()))
                    {
                        this.searchResult = this.daoIchiran.getdateforstringsql(sql.ToString());
                    }
                    ret_cnt = searchResult.Rows.Count;

                    //bool flg = true;
                    //if (ret_cnt != 0)
                    //{
                    //    flg = this.ItakuKeiyakuCheck(this.searchResult);
                    //}

                    //検索結果表示
                    this.form.ShowData();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret_cnt = -1;
            }
            return ret_cnt;
        }
        #endregion

        /// <summary>
        /// システム上必須な項目を非表示にします
        /// </summary>
        internal void HideColumnHeader()
        {
            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (column.Name.Equals(this.HIDDEN_DENPYOU_NUMBER) ||
                    column.Name.Equals(this.HIDDEN_SYSTEM_ID) ||
                    column.Name.Equals(this.HIDDEN_DENSHU_KBN_CD) ||
                    column.Name.Equals(this.HIDDEN_DETAIL_SYSTEM_ID) ||
                    column.Name.Equals("HST_GYOUSHA_CD_ERROR") ||
                    column.Name.Equals("HST_GENBA_CD_ERROR") ||
                    column.Name.Equals("HAIKI_SHURUI_CD_ERROR") ||
                    column.Name.Equals("HIDDEN_DENPYOU_SHURUI_CD") ||
                    column.Name.Equals("HIDDEN_SEARCH_SYSTEM_ID")
                    )
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.control.GYOUSHA_NAME_RYAKU.ReadOnly = true;

                if (string.IsNullOrEmpty(this.control.GYOUSHA_CD.Text))
                {
                    this.control.GENBA_CD.Text = string.Empty;
                    this.control.GENBA_NAME_RYAKU.Text = string.Empty;
                    BEFORE_GYOUSHA_CD = this.control.GYOUSHA_CD.Text;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                if (this.control.GYOUSHA_CD.Text != BEFORE_GYOUSHA_CD)
                {
                    this.control.GENBA_CD.Text = string.Empty;
                    this.control.GENBA_NAME_RYAKU.Text = string.Empty;
                    BEFORE_GYOUSHA_CD = this.control.GYOUSHA_CD.Text;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var gyoushaEntity = this.accessor.GetGyousha((this.control.GYOUSHA_CD.Text));
                if (gyoushaEntity == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.control.GYOUSHA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.control.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    BEFORE_GYOUSHA_CD = this.control.GYOUSHA_CD.Text;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyousha", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.GENBA_NAME_RYAKU.Text = string.Empty;
                this.control.GENBA_NAME_RYAKU.ReadOnly = true;

                if (string.IsNullOrEmpty(this.control.GENBA_CD.Text))
                {
                    this.control.GENBA_NAME_RYAKU.Text = string.Empty;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (string.IsNullOrEmpty(this.control.GYOUSHA_NAME_RYAKU.Text))
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.control.GENBA_CD.Text = string.Empty;
                    this.control.GENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                var genbaEntityList = this.accessor.GetGenba(this.control.GENBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.control.GENBA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                bool isContinue = false;
                M_GENBA genba = new M_GENBA();
                if (string.IsNullOrEmpty(this.control.TORIHIKISAKI_NAME_RYAKU.Text))
                {
                    if (string.IsNullOrEmpty(this.control.GYOUSHA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.control.GENBA_CD.Text = string.Empty;
                        this.control.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.control.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            this.control.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                    }

                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.control.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(this.control.GYOUSHA_NAME_RYAKU.Text))
                    {
                        // エラーメッセージ
                        msgLogic.MessageBoxShow("E051", "業者");
                        this.control.GENBA_CD.Text = string.Empty;
                        this.control.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    foreach (M_GENBA genbaEntity in genbaEntityList)
                    {
                        if (this.control.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                        {
                            isContinue = true;
                            genba = genbaEntity;
                            this.control.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                    }

                    if (!isContinue)
                    {
                        // 一致するものがないのでエラー
                        msgLogic.MessageBoxShow("E062", "業者");
                        this.control.GENBA_CD.Focus();
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.MsgBox.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal bool CheckUnpanGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.UNPAN_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.control.UNPAN_GYOUSHA_NAME_RYAKU.ReadOnly = true;

                if (string.IsNullOrEmpty(this.control.UNPAN_GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                var gyousha = this.accessor.GetGyousha(this.control.UNPAN_GYOUSHA_CD.Text);
                if (gyousha == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.control.UNPAN_GYOUSHA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }


                // 事業場区分、荷卸現場区分チェック
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    this.control.UNPAN_GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.control.UNPAN_GYOUSHA_CD.Focus();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyousha", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #region 使わない

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
        /// header設定
        /// </summary>
        /// /// <returns></returns>
        internal void SetHeader(HeaderForm hs)
        {
            this.headForm = hs;
        }

        /// <summary>
        /// 入力画面表示
        /// </summary>
        /// <param name="wintype"></param>
        /// <param name="DenpyouNum"></param>
        private bool EditDetail(WINDOW_TYPE wintype, string denpyouNum)
        {
            try
            {
                long denpyo = -1;

                if (!string.IsNullOrEmpty(denpyouNum))
                {
                    denpyo = long.Parse(denpyouNum);
                }

                string strFormId = "";
                strFormId = "G672";

                switch (wintype)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規モードで起動
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG);
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 修正モードの権限チェック
                        if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyo);
                        }
                        // 参照モードの権限チェック
                        else if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyo);
                        }
                        else
                        {
                            // 修正モードの権限なしのアラームを上げる
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E158", "修正");
                        }
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 削除モードで起動
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.DELETE_WINDOW_FLAG, WINDOW_TYPE.DELETE_WINDOW_FLAG, denpyo);
                        break;
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 複写モードで起動（新規モード）
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, denpyo);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditDetail", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 抽出条件保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetPrevStatus(object sender, EventArgs e)
        {
            InitialFlg = true;
            //拠点、伝票日付From、伝票日付To、各種抽出条件の項目をセッティングファイルに保存する。

            if (this.headForm.KYOTEN_CD.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }

            Properties.Settings.Default.SET_TORIHIKISAKI_CD = this.control.TORIHIKISAKI_CD.Text;
            Properties.Settings.Default.SET_GYOUSHA_CD = this.control.GYOUSHA_CD.Text;
            Properties.Settings.Default.SET_GENBA_CD = this.control.GENBA_CD.Text;
            Properties.Settings.Default.SET_UNPAN_GYOUSHA_CD = this.control.UNPAN_GYOUSHA_CD.Text;
            Properties.Settings.Default.SET_SHASHU_CD = this.control.SHASHU_CD.Text;
            Properties.Settings.Default.SET_SHARYOU_CD = this.control.SHARYOU_CD.Text;

            DateTime resultDt;
            if (!String.IsNullOrEmpty(this.form.HIDUKE_FROM.Text.Trim()) && DateTime.TryParse(this.form.HIDUKE_FROM.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = this.form.HIDUKE_FROM.Text;
            }
            if (!String.IsNullOrEmpty(this.form.HIDUKE_TO.Text.Trim()) && DateTime.TryParse(this.form.HIDUKE_TO.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = this.form.HIDUKE_TO.Text;
            }

            Properties.Settings.Default.SET_KIHON_KEIRYOU_CD = this.form.KIHON_KEIRYOU_CD.Text;
            Properties.Settings.Default.SET_KEIRYOU_KBN_CD = this.form.KEIRYOU_KBN_CD.Text;
            Properties.Settings.Default.SET_KEIRYOU_JYOUKYOU_CD = this.form.KEIRYOU_JOUKYOU_CD.Text;

            // 保存
            Properties.Settings.Default.Save();
        }

        #region 必須チェックエラーフォーカス処理
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
            }
            //ヘッダーチェック
            foreach (Control control in this.headForm.allControl)
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

        #endregion 必須チェックエラーフォーカス処理

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HIDUKE_FROM;
            var ToTextBox = this.form.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 伝票日付初期値設定
        /// </summary>
        private bool SetDenpyouHidukeInit()
        {
            try
            {
                //headerFormにSettingsの値            
                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_TO) || InitialFlg == false)
                {
                    this.form.HIDUKE_TO.Value = this.parentForm.sysDate;
                }
                else
                {
                    this.form.HIDUKE_TO.Value = Convert.ToDateTime(Properties.Settings.Default.SET_HIDUKE_TO.ToString());
                }

                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_FROM) || InitialFlg == false)
                {
                    this.form.HIDUKE_FROM.Value = this.parentForm.sysDate;
                }
                else
                {
                    this.form.HIDUKE_FROM.Value = Convert.ToDateTime(Properties.Settings.Default.SET_HIDUKE_FROM.ToString());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDenpyouHidukeInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        #region 車輌チェック
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
                this.control.SHARYOU_NAME_RYAKU.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.control.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.control.SHARYOU_CD.Text);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (sharyou == null)
                {
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                // 車輌名設定
                this.control.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 車種入力されてない場合
                if (string.IsNullOrEmpty(this.control.SHASHU_CD.Text))
                {
                    // 車種情報取得
                    var shashu = this.GetShashu(sharyou.SHASYU_CD);
                    if (shashu != null)
                    {
                        // 車種情報設定
                        this.control.SHASHU_CD.Text = shashu.SHASHU_CD;
                        this.control.SHASHU_NAME_RYAKU.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                // 運搬業者が入力されてない場合
                if (string.IsNullOrEmpty(this.control.UNPAN_GYOUSHA_CD.Text))
                {
                    // 業者情報取得
                    var gyousha = this.GetGyousha(sharyou.GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 業者情報設定
                        this.control.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.control.UNPAN_GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyouCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
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
        private M_SHARYOU GetSharyou(string sharyouCd)
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
                if (!string.IsNullOrEmpty(this.control.UNPAN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.control.UNPAN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                // 車種入力されている場合
                if (!string.IsNullOrEmpty(this.control.SHASHU_CD.Text))
                {
                    keyEntity.SHASYU_CD = this.control.SHASHU_CD.Text;
                }
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = msharyouDao.GetAllValidData(keyEntity);
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
                        this.control.SHARYOU_CD.Focus();
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
        private M_SHASHU GetShashu(string shashuCd)
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

                // [車種CD]でM_SHASHUを検索する
                var returnEntitys = this.mshashuDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetShashu", ex);
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
        private M_GYOUSHA GetGyousha(string gyoushaCd)
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
                var gyousha = this.mgyoushaDao.GetAllValidData(keyEntity);

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
        #endregion

        #region 滞留件数等セット
        /// <summary>
        /// 滞留件数等セット
        /// </summary>
        private void SetKensu()
        {
            // 拠点CD条件作成
            string kyotenCd = null;
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text)
                && !this.headForm.KYOTEN_CD.Text.Equals(CommonConst.KYOTEN_CD_ZENSHA.ToString()))
            {
                kyotenCd = this.headForm.KYOTEN_CD.Text;
            }

            // 受入滞留数
            long cntUkeireTairyu = 0;
            // 「滞留登録区分=1」かつ「基本計量=1(受入)」
            cntUkeireTairyu = daoIchiran.GetEntryCount(1, 0, kyotenCd, 1);
            this.form.txtUkeireTairyuNumber.Text = cntUkeireTairyu.ToString();

            // 出荷滞留数
            long cntShukkaTairyu = 0;
            // 「滞留登録区分=1」かつ「基本計量=2(出荷)」
            cntShukkaTairyu = daoIchiran.GetEntryCount(1, 0, kyotenCd, 2);
            this.form.txtSyukkaTairyuNumber.Text = cntShukkaTairyu.ToString();

            // 本日受入台数
            long cntUkeireDaisuu = 0;
            // 「滞留登録区分=0」かつ「基本計量=1(受入)」かつ「作成日付=[SQLサーバーGetDate]」
            cntUkeireDaisuu = daoIchiran.GetEntryCount(0, 1, kyotenCd, 1);
            this.form.txtHonzituUkeireNumber.Text = cntUkeireDaisuu.ToString();

            // 本日出荷台数
            long cntShukkaDaisuu = 0;
            // 「滞留登録区分=0」かつ「基本計量=2(出荷)」かつ「作成日付=[SQLサーバーGetDate]」
            cntShukkaDaisuu = daoIchiran.GetEntryCount(0, 1, kyotenCd, 2);
            this.form.txtHonzituSyukkaNumber.Text = cntShukkaDaisuu.ToString();

            // 本日受入数量
            decimal cntUkeireMount = 0;
            // 「滞留登録区分=0」かつ「基本計量=1(受入)」かつ「作成日付=[SQLサーバーGetDate]」
            cntUkeireMount = daoIchiran.GetNetTotal(kyotenCd, 1);
            this.form.txtHonzituUkeireMount.Text = cntUkeireMount.ToString();

            // 本日出荷数量
            decimal cntShukkaMount = 0;
            // 「滞留登録区分=0」かつ「基本計量=2(出荷)」かつ「作成日付=[SQLサーバーGetDate]」
            cntShukkaMount = daoIchiran.GetNetTotal(kyotenCd, 2);
            this.form.txtHonzituSyukkaMount.Text = cntShukkaMount.ToString();
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
    }
}