using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP;
using Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.DAO;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        ///  一覧グリッド削除カラムヘッダー名
        /// </summary>
        private readonly string DEF_GRID_CHK_DELETE_COL_HEADER_TEXT = "削除";

        /// <summary>
        /// 一覧グリッド非表示カラム：システムID
        /// </summary>
        private readonly string DEF_GRID_HIDDEN_COL_NAME_SYSTEMID = "SYSTEM_ID";

        /// <summary>
        /// 一覧グリッド非表示カラム：枝番
        /// </summary>
        private readonly string DEF_GRID_HIDDEN_COL_NAME_SEQ = "SEQ";

        /// <summary>
        /// 一覧グリッド非表示カラム：行番号
        /// </summary>
        private readonly string DEF_GRID_HIDDEN_COL_NAME_ROWNO = "ROW_NO";
        /// <summary>
        /// 一覧グリッド非表示カラム：タイムスタンプ
        /// </summary>
        private readonly string DEF_GRID_HIDDEN_COL_NAME_TIMESTAMP = "TIME_STAMP";

        /// <summary>
        /// 一覧グリッド非表示カラム：パターン名
        /// </summary>
        private readonly string DEF_GRID_HIDDEN_COL_NAME_SELECTPATTERNNAME = "SELECT_PATTERN_NAME";
        
        #endregion

        #region enum

        /// <summary>
        /// 中間・最終区分
        /// </summary>
        internal enum LAST_SBN_KBN
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// 中間処分(1)
            /// </summary>
            Cyukan = 1,
            /// <summary>
            /// 最終処分(2)
            /// </summary>
            Saisyu = 2,
        }

        /// <summary>
        /// 契約書区分
        /// </summary>
        internal enum KEIYAKUSYO_KBN
        {
            /// <summary>
            /// なし
            /// </summary>
            None = 0,
            /// <summary>
            /// 全般連様式（産廃）
            /// </summary>
            Sanpai = 1,
            /// <summary>
            /// 建廃個別様式（建廃）
            /// </summary>
            Kenpai = 2,
        }

        #endregion

        #region 内部変数

        /// <summary>
        /// DBアクセサ
        /// </summary>
        private DBAccessor dbAccess;

        /// <summary>
        /// 最終処分場所パターン一覧DTO
        /// </summary>
        private SaisyuSyoriSyobunBasyoPatternIchiranDao shobunBasyoDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// DGV表示データ有無
        /// </summary>
        private bool dispDataRecord = false;

        /// <summary>
        /// 論理削除レコードリスト
        /// </summary>
        private List<M_SBNB_PATTERN> deleteDataList;

        #endregion

        #region プロパティ

        /// <summary>
        /// 中間最終区分
        /// </summary>
        internal LAST_SBN_KBN WorkLastSbnKbn
        {
            get
            {
                // 伝種区分から中間最終区分を取得する
                LAST_SBN_KBN res = LAST_SBN_KBN.None;
                if (this.form != null)
                {
                    if (this.form.DenshuKbn == DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN ||
                        this.form.DenshuKbn == DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI ||
                        this.form.DenshuKbn == DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI)
                    {
                        res = LAST_SBN_KBN.Cyukan;
                    }
                    else if (this.form.DenshuKbn == DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN ||
                        this.form.DenshuKbn == DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI ||
                        this.form.DenshuKbn == DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI)
                    {
                        res = LAST_SBN_KBN.Saisyu;
                    }

                }
                return res;
            }
        }

        /// <summary>
        /// 契約書区分
        /// </summary>
        internal KEIYAKUSYO_KBN WorkLastKeiyakusyoKbn
        {
            get
            {
                KEIYAKUSYO_KBN res = KEIYAKUSYO_KBN.None;
                if (this.form != null)
                {
                    if (this.form.DenshuKbn == DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN ||
                        this.form.DenshuKbn == DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN ||
                        this.form.DenshuKbn == DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI ||
                        this.form.DenshuKbn == DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI)
                    {
                        res = KEIYAKUSYO_KBN.Sanpai;
                    }
                    else if (this.form.DenshuKbn == DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI ||
                        this.form.DenshuKbn == DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI)
                    {
                        res = KEIYAKUSYO_KBN.Kenpai;
                    }
                }
                return res;
            }
        }

        /// <summary>
        /// 本ロジックで生成するSQL
        /// </summary>
        public string CreateSql { set; get; }

        /// <summary>
        /// 本ロジックで検索結果のDataTable
        /// </summary>
        public DataTable SearchResult { set; get; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // 参照設定
            this.form = targetForm;
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
            try
            {
                LogUtility.DebugMethodStart();

                // 画面の表示タイプを設定
                this.SetWindowType();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // Dao初期化
                this.DaoInit();

                // イベントの初期化処理
                this.EventInit();

                // システム情報からアラート件数を設定
                M_SYS_INFO sysInfo = this.dbAccess.GetSysInfo();
                if (sysInfo != null)
                {
                    this.form.HeaderForm.alertNumber.Text = sysInfo.ICHIRAN_ALERT_KENSUU.ToString();
                }

                // ----------------------------------------------------
                // デザイナから変更できないためここで設定

                // グリッド縦幅変更禁止
                this.form.customDataGridView1.AllowUserToResizeRows = false;

                // ユーザーによる行追加禁止
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // 右よせ
                this.form.customDataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // サイズ
                this.form.customDataGridView1.Location = new System.Drawing.Point(4, 130);
                this.form.customDataGridView1.Size = new System.Drawing.Size(this.form.Width - 17, this.form.Height - this.form.customDataGridView1.Location.Y - 38);

                // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 start
                this.form.ParentBaseForm.bt_process2.Enabled = false;
                // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 end

                // ----------------------------------------------------
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 画面の表示タイプを設定します
        /// </summary>
        internal void SetWindowType()
        {
            switch (this.form.DenshuKbn)
            {
                case DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                case DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                case DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                case DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    this.form.numtxt_KeiyakushoTypeCd.Enabled = false;
                    this.form.rdb_Zenpan.Enabled = false;
                    this.form.rdb_Kenpai.Enabled = false;
                    break;
                case DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN:
                    this.form.numtxt_KeiyakushoTypeCd.Enabled = true;
                    this.form.rdb_Zenpan.Enabled = true;
                    this.form.rdb_Kenpai.Enabled = true;
                    this.form.DenshuKbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI;
                    break;
                case DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN:
                default:
                    this.form.numtxt_KeiyakushoTypeCd.Enabled = true;
                    this.form.rdb_Zenpan.Enabled = true;
                    this.form.rdb_Kenpai.Enabled = true;
                    this.form.DenshuKbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI;
                    break;
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.form.ParentBaseForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            this.form.ParentBaseForm.bt_func1.Enabled = false;
            this.form.ParentBaseForm.bt_func2.Enabled = false;
            this.form.ParentBaseForm.bt_func3.Enabled = true;
            this.form.ParentBaseForm.bt_func4.Enabled = true;
            this.form.ParentBaseForm.bt_func5.Enabled = false;
            this.form.ParentBaseForm.bt_func6.Enabled = false;
            this.form.ParentBaseForm.bt_func7.Enabled = true;
            this.form.ParentBaseForm.bt_func8.Enabled = true;
            this.form.ParentBaseForm.bt_func9.Enabled = false;
            this.form.ParentBaseForm.bt_func10.Enabled = true;
            this.form.ParentBaseForm.bt_func11.Enabled = true;
            this.form.ParentBaseForm.bt_func12.Enabled = true;

            this.form.bt_ptn1.Visible = false;
            this.form.bt_ptn2.Visible = false;
            this.form.bt_ptn3.Visible = false;
            this.form.bt_ptn4.Visible = false;
            this.form.bt_ptn5.Visible = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }

        #endregion

        #region Daoの初期化

        /// <summary>
        /// Daoの初期化
        /// </summary>
        private void DaoInit()
        {
            LogUtility.DebugMethodStart();

            // DBアクセス作成
            this.dbAccess = new DBAccessor();
            this.shobunBasyoDao = DaoInitUtility.GetComponent<SaisyuSyoriSyobunBasyoPatternIchiranDao>();
                
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // ベースフォーム用のイベント処理
            //this.form.ParentBaseForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移

            // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 start
            //this.form.ParentBaseForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移
            // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 end

            this.form.ParentBaseForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)
            //this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown);      //form上でのESCキー押下でFocus移動

            // Functionボタンのイベント生成
            this.form.ParentBaseForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);              //修正
            this.form.ParentBaseForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);              //削除
            this.form.ParentBaseForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //条件クリア
            this.form.C_Regist(this.form.ParentBaseForm.bt_func8);                                   // 登録メソッドの上書き
            this.form.ParentBaseForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
            this.form.ParentBaseForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //並び替え
            this.form.ParentBaseForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            this.form.ParentBaseForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる

            // グリッドのイベント生成
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(customDataGridView1_CellClick);
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);

            // ラジオボタン変更イベント生成
            this.form.rdb_KensakuCdPatten.CheckedChanged += new EventHandler(rdb_CheckedChanged);
            this.form.rdb_KensakuCdFurigana.CheckedChanged += new EventHandler(rdb_CheckedChanged);
            this.form.rdb_Zenpan.CheckedChanged += new EventHandler(rdb_Type_CheckedChanged);
            this.form.rdb_Kenpai.CheckedChanged += new EventHandler(rdb_Type_CheckedChanged);

            // 画面表示後イベント生成
            this.form.ParentBaseForm.Shown += new EventHandler(UIForm_Shown);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventDelete()
        {
            LogUtility.DebugMethodStart();

            // ベースフォーム用のイベント処理
            //this.form.ParentBaseForm.bt_process1.Click -= new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移

            // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 start
            //this.form.ParentBaseForm.bt_process2.Click -= new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移
            // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 end

            this.form.ParentBaseForm.txb_process.KeyDown -= new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)
            //this.form.PreviewKeyDown -= new PreviewKeyDownEventHandler(form_PreviewKeyDown);      //form上でのESCキー押下でFocus移動

            // Functionボタンのイベント生成
            this.form.ParentBaseForm.bt_func3.Click -= new EventHandler(this.bt_func3_Click);              //修正
            this.form.ParentBaseForm.bt_func4.Click -= new EventHandler(this.bt_func4_Click);              //削除
            this.form.ParentBaseForm.bt_func7.Click -= new System.EventHandler(this.bt_func7_Click);       //条件クリア
            this.form.ParentBaseForm.bt_func8.Click -= new System.EventHandler(this.bt_func8_Click);       //検索
            this.form.ParentBaseForm.bt_func10.Click -= new System.EventHandler(this.bt_func10_Click);     //並び替え
            this.form.ParentBaseForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);     //閉じる

            // グリッドのイベント生成
            this.form.customDataGridView1.CellClick -= new DataGridViewCellEventHandler(customDataGridView1_CellClick);
            this.form.customDataGridView1.CellDoubleClick -= new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);

            // ラジオボタン変更イベント生成
            this.form.rdb_KensakuCdPatten.CheckedChanged -= new EventHandler(rdb_CheckedChanged);
            this.form.rdb_KensakuCdFurigana.CheckedChanged -= new EventHandler(rdb_CheckedChanged);
            this.form.rdb_Zenpan.CheckedChanged -= new EventHandler(rdb_Type_CheckedChanged);
            this.form.rdb_Kenpai.CheckedChanged -= new EventHandler(rdb_Type_CheckedChanged);

            // 画面表示イベント生成
            this.form.ParentBaseForm.Shown -= new EventHandler(UIForm_Shown);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期表示後イベント

        /// <summary>
        /// 初期表示後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // ラジオボタン初期値
                this.form.rdb_KensakuCdPatten.Checked = true;
                this.form.rdb_KensakuCdFurigana.Checked = false;
                if (this.WorkLastKeiyakusyoKbn == KEIYAKUSYO_KBN.Kenpai)
                {
                    this.form.rdb_Zenpan.Checked = false;
                    this.form.rdb_Kenpai.Checked = true;
                }
                else
                {
                    this.form.rdb_Kenpai.Checked = false;
                    this.form.rdb_Zenpan.Checked = true;
                }

                // 検索文字列初期値
                this.form.txt_SerchPattern.Text = string.Empty;

                // 初期フォーカス
                this.form.txt_SerchPattern.Select();
                this.form.txt_SerchPattern.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ラジオボタン変更イベント

        /// <summary>
        /// ラジオボタンクリック変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.form.rdb_KensakuCdPatten.Checked)
                {
                    // 全角ひらがな
                    this.form.txt_SerchPattern.ImeMode = ImeMode.Hiragana;
                    // 2013/12/25 ラベルも変更する
                    this.form.lbl_PattenName.Text = "パターン名";
                }
                else if (this.form.rdb_KensakuCdFurigana.Checked)
                {
                    // 全角フリガナ
                    this.form.txt_SerchPattern.ImeMode = ImeMode.Katakana;
                    // 2013/12/25 ラベルも変更する
                    this.form.lbl_PattenName.Text = "フリガナ";
                }

                // パターン名検索条件クリア
                this.form.txt_SerchPattern.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ラジオボタンクリック変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb_Type_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.form.rdb_Zenpan.Checked)
                {
                    if (this.WorkLastSbnKbn == LAST_SBN_KBN.Saisyu)
                    {
                        this.form.DenshuKbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI;
                    }
                    else
                    {
                        this.form.DenshuKbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI;
                    }
                }
                else if (this.form.rdb_Kenpai.Checked)
                {
                    if (this.WorkLastSbnKbn == LAST_SBN_KBN.Saisyu)
                    {
                        this.form.DenshuKbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI;
                    }
                    else
                    {
                        this.form.DenshuKbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI;
                    }
                }

                // 伝種区分から画面タイトルを取得
                this.form.HeaderForm.lb_title.Text = DENSHU_KBNExt.ToTitleString(this.form.DenshuKbn);

                // パターンを更新する（デフォルトパターン選択）
                this.form.PatternReload(true);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面遷移：パターン一覧画面（プロセスボタン押下イベント）

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        //private void bt_process1_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        var sysID = this.form.OpenPatternIchiran();

        //        if (!string.IsNullOrEmpty(sysID))
        //        {
        //            this.form.SetPatternBySysId(sysID);
        //            this.form.ShowIchiranData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Fatal(ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        #endregion

        // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 start
        #region 【未実装】画面遷移:検索条件設定画面（プロセスボタン押下イベント）

        ///// <summary>
        ///// 検索条件設定画面へ遷移
        ///// </summary>
        //private void bt_process2_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        #endregion
        // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 end

        #region 処理No(ESC)コントロール：エンターキー押下イベント（テキスト内容に従った画面遷移)

        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
                {
                    string seniSaki = this.form.ParentBaseForm.txb_process.Text.Trim();

                    //if ("1".Equals(seniSaki))
                    //{
                    //    // パターン一覧画面へ遷移
                    //    this.bt_process1_Click(sender, e);
                    //}
                    // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 start
                    //else if ("2".Equals(seniSaki))
                    //{
                    //    // 検索条件設定画面へ遷移
                    //    this.bt_process2_Click(sender, e);
                    //}
                    // 2014/01/22 検索条件設定画面へ遷移する機能は非活性 end
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 削除：FWに任せるため(ESCキーダウンイベント)

        ///// <summary>
        ///// キーダウンイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        if (e.KeyCode == Keys.Escape)
        //        {
        //            //処理No(ESC)へカーソル移動
        //            this.form.ParentBaseForm.txb_process.Focus();
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        #endregion

        #region [F3キー]押下イベント

        /// <summary>
        /// F3 適用
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 選択されたセルを取得
                DataGridViewCell currentCell = this.form.customDataGridView1.CurrentCell;

                // 表示データ有り、選択されていること
                if (this.dispDataRecord && currentCell != null)
                {
                    // 有効なセルを確認
                    if (currentCell.RowIndex >= 0 && currentCell.ColumnIndex >= 0)
                    {
                        // 選択されたパターン名を取得
                        this.form.OutSelectedPatternName = this.form.customDataGridView1.Rows[currentCell.RowIndex].Cells[DEF_GRID_HIDDEN_COL_NAME_SELECTPATTERNNAME].Value.ToString();

                        // 画面を閉じる
                        this.bt_func12_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F4キー]押下イベント

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 削除レコードを初期化
                this.deleteDataList = new List<M_SBNB_PATTERN>();

                // データ件数0件以上を確認
                if (this.dispDataRecord)
                {
                    // グリッドで選択されたパターン名をグループ化
                    var selectedGroupList = this.form.customDataGridView1.Rows.Cast<DataGridViewRow>()
                                        .Where(t => t.Cells[this.DEF_GRID_CHK_DELETE_COL_HEADER_TEXT].Value != null)
                                        .Where(t => true.Equals(t.Cells[this.DEF_GRID_CHK_DELETE_COL_HEADER_TEXT].Value))
                                        .Select<DataGridViewRow, string>(t => (string)t.Cells[this.DEF_GRID_HIDDEN_COL_NAME_SELECTPATTERNNAME].Value)
                                        .GroupBy(t => t)
                                        .ToList();

                    // グループループ
                    foreach (var groupSet in selectedGroupList)
                    {
                        // チェックされたパターン名と一致するレコードを取得
                        var deleteList = this.form.customDataGridView1.Rows.Cast<DataGridViewRow>()
                                        .Where(t => groupSet.Key.Equals(t.Cells[this.DEF_GRID_HIDDEN_COL_NAME_SELECTPATTERNNAME].Value))
                                        .ToList();

                        foreach (var deleteRecord in deleteList)
                        {
                            M_SBNB_PATTERN record = new M_SBNB_PATTERN();

                            // 主キー
                            record.SYSTEM_ID = new SqlInt64((long)deleteRecord.Cells[this.DEF_GRID_HIDDEN_COL_NAME_SYSTEMID].Value);
                            record.SEQ = new SqlInt32((int)deleteRecord.Cells[this.DEF_GRID_HIDDEN_COL_NAME_SEQ].Value);
                            record.ROW_NO = new SqlInt16((short)deleteRecord.Cells[this.DEF_GRID_HIDDEN_COL_NAME_ROWNO].Value);

                            // 更新キー
                            record.TIME_STAMP = (byte[])deleteRecord.Cells[this.DEF_GRID_HIDDEN_COL_NAME_TIMESTAMP].Value;

                            // 論理削除
                            record.DELETE_FLG = SqlBoolean.True;

                            // システム設定(Whoカラム)
                            var binder = new DataBinderLogic<M_SBNB_PATTERN>(record);
                            binder.SetSystemProperty(record, false);
                            record = binder.Entitys[0];

                            // 削除対象として登録
                            deleteDataList.Add(record);
                        }

                    }

                    // 削除対象があれば
                    if (deleteDataList.Count > 0)
                    {
                        // 論理削除
                        this.LogicalDelete();

                        // 再検索
                        this.form.ParentBaseForm.bt_func8.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F7キー]押下イベント

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // パターン名検索条件 初期化 
                this.form.numtxt_KensakuCd.Text = "1";
                this.form.rdb_KensakuCdFurigana.Checked = false;
                this.form.rdb_KensakuCdPatten.Checked = true;
                this.form.txt_SerchPattern.Text = string.Empty;

                // 詳細検索条件初期化
                this.form.searchString.Text = string.Empty;

                this.form.customSearchHeader1.ClearCustomSearchSetting();
                this.form.customSortHeader1.ClearCustomSortSetting();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F8キー]押下イベント

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // FW必須チェック
                if (this.form.RegistErrorFlag)
                {
                    return;
                }

                // FWでチェックできない必須入力の確認
                /*if (!this.IsInputDataEnableAndShowErrMessage())
                {
                    return;
                }*/

                // データ検索
                int recordCnt = this.Search();

                // 0件時の処理
                if (0 >= recordCnt)
                {
                    // 0件メッセージ表示
                    MessageBoxUtility.MessageBoxShow("C001");

                    this.form.SetDataGridViewColumns(); //ThangNguyen [Add] 20150831 #12363

                    // ゼロ件設定(空1行レコード出力)
                    this.SetZeroGridData();

                    // 一覧グリッド更新
                    this.form.ShowIchiranData();

                    // ヘッダの読み込み件数更新
                    if (this.form.customDataGridView1 != null)
                    {
                        this.form.HeaderForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                        this.form.HeaderForm.readDataNumber.Text = "0";
                    }

                    return;
                }

                // 一覧グリッド更新
                this.form.ShowIchiranData();

                // ヘッダの読み込み件数更新
                if (this.form.customDataGridView1 != null)
                {
                    this.form.HeaderForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.form.HeaderForm.readDataNumber.Text = "0";
                }

                // アラート件数はIchiranFormでやってくれるため画面は意識しない
                if (this.form.HeaderForm.readDataNumber.Text.Equals("0"))
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F10キー]押下イベント

        /// <summary>
        /// F10 並び替え
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.form.customSortHeader1.ShowCustomSortSettingDialog();
                this.form.customDataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F11キー]押下イベント

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.form.HeaderForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.form.HeaderForm.readDataNumber.Text = "0";
            }
        }

        #endregion

        #region [F12キー]押下イベント

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

                var parentBaseForm = this.form.ParentBaseForm; 
                this.form.Close();
                parentBaseForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region グリッド クリックイベント

        /// <summary>
        /// セルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // グリッド取得
                CustomDataGridView grid = sender as CustomDataGridView;

                // 削除セルクリックを確認
                if (grid != null && e.RowIndex >= 0 && this.dispDataRecord
                    && grid.Columns[e.ColumnIndex].Name.Equals(DEF_GRID_CHK_DELETE_COL_HEADER_TEXT))
                {
                    DataTable tbl = grid.DataSource as DataTable;
                    tbl.DefaultView[e.RowIndex][0] = (bool)tbl.DefaultView[e.RowIndex][0] == true ? false : true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region グリッド ダブルクリックイベント

        /// <summary>
        /// セルダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // グリッド取得
                CustomDataGridView grid = sender as CustomDataGridView;

                // セルクリックを確認（削除セルクリックは除く)
                if (grid != null && e.RowIndex >= 0 && this.dispDataRecord
                    && !grid.Columns[e.ColumnIndex].Name.Equals(DEF_GRID_CHK_DELETE_COL_HEADER_TEXT))
                {
                    // F3キー：適用と同じ動きをする
                    this.bt_func3_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion
        
        #region DB検索

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            // 0件
            int recordCnt = 0;
            this.SearchResult = new DataTable();
            this.dispDataRecord = false;

            //SELECT句未取得チェック
            if (!string.IsNullOrEmpty(this.form.selectSql))
            {
                // 最終処分場所パターン一覧SQL取得
                this.CreateSql = this.MakeSelectSql();

                // 最終処分場所パターン一覧検索実行
                this.SearchResult = this.shobunBasyoDao.GetDateForStringSql(this.CreateSql);

                // データテーブル変更（削除フラグを追加)
                this.SearchResult = this.ToChangeAddDataTbl(this.SearchResult);

                // 読込データ件数を取得
                recordCnt = SearchResult.Rows.Count;

                // 複数件取得
                if (recordCnt > 0)
                {
                    this.dispDataRecord = true;
                }
            }

            LogUtility.DebugMethodEnd(recordCnt);
            return recordCnt;
        }

        #endregion

        #region 検索SQL

        /// <summary>
        /// 一覧検索SQL作成
        /// </summary>
        private string MakeSelectSql()
        {
            LogUtility.DebugMethodStart();

            // 本日を取得
            string today_yyyyMMdd = DateTime.Now.ToString("yyyy/MM/dd");
            // 検索パターン名を取得
            string patternName = this.form.txt_SerchPattern.Text;

            // Sql
            var sql = new StringBuilder();

            // SELECT句
            sql.Append(" SELECT DISTINCT ");

            // - 定義されたSELECT
            sql.Append(this.form.selectSql);

            // - 削除処理用に取得
            sql.Append(string.Format(" ,M_SBNB_PATTERN.SYSTEM_ID AS {0} ", this.DEF_GRID_HIDDEN_COL_NAME_SYSTEMID));
            sql.Append(string.Format(" ,M_SBNB_PATTERN.SEQ AS {0} ", this.DEF_GRID_HIDDEN_COL_NAME_SEQ));
            sql.Append(string.Format(" ,M_SBNB_PATTERN.ROW_NO AS {0} ", this.DEF_GRID_HIDDEN_COL_NAME_ROWNO));
            sql.Append(string.Format(" ,M_SBNB_PATTERN.TIME_STAMP AS {0} ", this.DEF_GRID_HIDDEN_COL_NAME_TIMESTAMP));

            // - 選択されたパターン名返却用に取得
            sql.Append(string.Format(" ,M_SBNB_PATTERN.PATTERN_NAME AS {0} ", this.DEF_GRID_HIDDEN_COL_NAME_SELECTPATTERNNAME));

            // FROM句
            sql.Append(" FROM ");
            sql.Append(" M_SBNB_PATTERN ");

            // JOIN句
            sql.Append(this.form.joinSql);

            // WHERE句
            sql.Append(" WHERE ");

            // - 削除フラグ
            sql.Append(" M_SBNB_PATTERN.DELETE_FLG = 0 ");
            // - 中間最終区分
            sql.Append(string.Format(" AND M_SBNB_PATTERN.LAST_SBN_KBN = {0} ", ((int)this.WorkLastSbnKbn).ToString()));
            sql.Append(string.Format(" AND M_SBNB_PATTERN.ITAKU_KEIYAKU_TYPE = {0} ", ((int)this.WorkLastKeiyakusyoKbn).ToString()));
            // - パターン名 部分一致
            if (patternName.Length > 0)
            {
                string sqlPatternName = SqlCreateUtility.CounterplanEscapeSequence(patternName);
                if (this.form.rdb_KensakuCdFurigana.Checked)
                {
                    // ふりがな
                    sql.Append(string.Format(" AND M_SBNB_PATTERN.PATTERN_FURIGANA like '%{0}%' ", sqlPatternName));
                }
                else if (this.form.rdb_KensakuCdPatten.Checked)
                {
                    // パターン名
                    sql.Append(string.Format(" AND M_SBNB_PATTERN.PATTERN_NAME like '%{0}%' ", sqlPatternName));
                }
            }

            // ORDERBY句
            if (!string.IsNullOrEmpty(this.form.orderSql))
            {
                // - 定義されたORDERBY
                sql.Append(" ORDER BY ");
                sql.Append(this.form.orderSql);
            }

            // 返却
            string res = sql.ToString();
            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region 空白１行データ設定
        
        /// <summary>
        /// 表示データ0件時／初期起動時の表示処理
        /// </summary>
        internal void SetZeroGridData()
        {
            LogUtility.DebugMethodStart();

            // データ0件設定
            this.dispDataRecord = false;

            // 0件初期化
            this.SearchResult = this.GetZeroDataTable();

            // 削除フラグを追加
            this.SearchResult = this.ToChangeAddDataTbl(this.SearchResult);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ゼロ件DataTable取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetZeroDataTable()
        {
            LogUtility.DebugMethodStart();

            DataTable tbl = new DataTable();

            // パターン一覧で定義されたカラム名を取得
            if (this.form.Table != null && this.form.Table.Columns.Count > 0)
            {
                foreach (DataColumn colInfo in this.form.Table.Columns)
                {
                    Type type = Type.GetType("System.String");
                    tbl.Columns.Add(colInfo.Caption, type);
                }
            }

            // 空行追加は行わないため以下を削除
            //tbl.Rows.Add(); // 空行追加

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        #endregion

        #region データテーブルの加工（削除フラグを追加）

        /// <summary>
        /// データテーブルに削除フラグを追加
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private DataTable ToChangeAddDataTbl(DataTable original)
        {
            LogUtility.DebugMethodStart(original);

            // データテーブル生成
            DataTable tbl = new DataTable();

            // 削除フラグカラムを追加
            if (!original.Columns.Contains(DEF_GRID_CHK_DELETE_COL_HEADER_TEXT))
            {
                tbl.Columns.Add(DEF_GRID_CHK_DELETE_COL_HEADER_TEXT, typeof(bool));
            }

            // もともとのカラムを追加
            foreach (DataColumn colInfo in original.Columns)
            {
                tbl.Columns.Add(colInfo.ColumnName, colInfo.DataType);
            }

            // もともとのデータの挿入
            foreach (DataRow rowInfo in original.Rows)
            {
                DataRow addRow = tbl.NewRow();

                // 削除カラムデータ
                addRow[DEF_GRID_CHK_DELETE_COL_HEADER_TEXT] = false;

                // もともとのデータ
                foreach (DataColumn colInfo in original.Columns)
                {
                    addRow[colInfo.ColumnName] = rowInfo[colInfo.ColumnName];
                }

                // 登録
                tbl.Rows.Add(addRow);
            }

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        #endregion

        #region グリッド表示カラムの加工

        /// <summary>
        /// 一覧グリッドの表示制御
        /// </summary>
        internal void RefreshIchiranGrid()
        {
            LogUtility.DebugMethodStart();

            // ----------------------------------------------
            // カラム設定
            foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns)
            {
                // ヘッダのソートは無効
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                // 文字列、日付は左よせ
                if (col.ValueType.Equals(typeof(string)) || col.ValueType.Equals(typeof(DateTime)))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                // チェックボックスは真ん中
                else if (col.ValueType.Equals(typeof(bool)))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // 内部処理(UPDATE、返却値)に使用するカラムは非表示化
                if (col.Name.Equals(DEF_GRID_HIDDEN_COL_NAME_SYSTEMID)
                    || col.Name.Equals(DEF_GRID_HIDDEN_COL_NAME_SEQ)
                    || col.Name.Equals(DEF_GRID_HIDDEN_COL_NAME_ROWNO)
                    || col.Name.Equals(DEF_GRID_HIDDEN_COL_NAME_TIMESTAMP)
                    || col.Name.Equals(DEF_GRID_HIDDEN_COL_NAME_SELECTPATTERNNAME)
                    )
                {
                    col.Visible = false;
                }
            }

            // 再表示
            this.form.customDataGridView1.Refresh();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region DB論理削除

        /// <summary>
        /// DB論理削除
        /// </summary>
        [Transaction]
        public void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // トランザクション
                using (Transaction ts = new Transaction())
                {
                    // 論理削除ループ
                    foreach (M_SBNB_PATTERN delete in this.deleteDataList)
                    {
                        // 論理削除
                        this.shobunBasyoDao.Update(delete);
                    }

                    // コミット
                    ts.Commit();
                }
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Error("排他エラー", ex); //排他はエラー
                    MessageBoxUtility.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Fatal(ex); //その他は例外
                    MessageBoxUtility.MessageBoxShow("E093");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 必須入力チェック
        /// <summary>
        /// 必須入力チェック（エラーメッセージ表示付）
        /// </summary>
        /// <returns>true：問題なし、false：必須エラー</returns>
        private bool IsInputDataEnableAndShowErrMessage()
        {
            LogUtility.DebugMethodStart();

            bool res = true;

            if (!this.form.RegistErrorFlag)
            {
                // 選択パターン確認
                if (res && this.form.PatternNo == 0)
                {
                    res = false;
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                }

                // FWでチェック
                //// 検索コード必須確認
                //if (res && this.form.numtxt_KensakuCd.Text.Trim().Length <= 0)
                //{
                //    res = false;

                //    // 必須エラーメッセージ表示
                //    var messageShowLogic = new MessageBoxShowLogic();
                //    messageShowLogic.MessageBoxShow("E001", "検索CD");

                //    // フォーカス
                //    this.form.numtxt_KensakuCd.Focus();
                //}
            }

            LogUtility.DebugMethodEnd(res);
            return res;
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

        #region 未使用
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

    }
}
