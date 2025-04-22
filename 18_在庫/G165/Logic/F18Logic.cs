// $Id: F18Logic.cs 26434 2014-07-24 04:44:07Z ogawa@takumi-sys.co.jp $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.APP;
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.Const;
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.DTO;
using Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.DAO;
using System.Windows.Forms;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using System.Collections.ObjectModel;

namespace Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class F18Logic : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public F18_G165Dto SearchString { get; set; }

        #endregion

        #region フィールド

        /// <summary>
        /// UIForm
        /// </summary>
        private F18_G165Form myForm;
        /// <summary>
        /// 在庫明細一覧DataGridView
        /// </summary>
        private DataGridView myGridView;
        /// <summary>
        /// HeadForm
        /// </summary>
        internal F18_G165HeaderForm headerForm;
        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm parentForm;
        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic;
        ///// <summary>
        ///// 在庫単位同じか:同じ(true)　異なる(false)
        ///// </summary>
        //bool isAllUnitSame = true;

        /// <summary>
        /// 比率セットモード
        /// </summary>
        bool isHiritsuSetMode = false;

        #endregion

        #region 初期化処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">メインフォーム</param>
        public F18Logic(F18_G165Form targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.myForm = targetForm;
                // データ検索条件オブジェクト
                this.SearchString = new F18_G165Dto();
                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();
                // DataGridViewコントロール
                this.myGridView = this.myForm.ZaikoMeisaiIchiran;
            }
            catch (Exception ex)
            {
                LogUtility.Error("F18Logic", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                this.parentForm = (BusinessBaseForm)this.myForm.Parent;
                // タイトル設定
                //this.parentForm.Text = ConstCls.FORM_TITLE;
                // プロセスボタンを非表示
                this.parentForm.ProcessButtonPanel.Visible = false;
                // ポップアップモード
                this.parentForm.IsPopupType = true;

                // ヘッダフォームオブジェクト取得
                this.headerForm = (F18_G165HeaderForm)this.parentForm.headerForm;

                // headerformの値を設定
                this.headerForm.txtHinmeiCd.Text = Properties.Settings.Default.HinmeiCd;
                this.headerForm.txtHinmeiName.Text = Properties.Settings.Default.HinmeiName;
                // 明細Footerの値を設定
                this.myForm.txtJyuuryou.Text = Properties.Settings.Default.SyoumiJyuuryou.ToString();
                this.myForm.txtNisugata.Text = Properties.Settings.Default.NisugataSuuryou.ToString();
                this.myForm.txtNisugataUnit.Text = Properties.Settings.Default.NisugataUnitName;
                this.myForm.txtKingaku.Text = CommonCalc.DecimalFormat(Properties.Settings.Default.Kingaku);

                // ボタンのテキストを初期化
                this.ButtonInit();

                //// 明細データ取得
                //this.Search();
                // 明細一覧にデータをセット
                this.SetIchiran();

                // 比率モードより、画面コントロール制御
                this.CtrlSettingByHiritsuMode();

                // イベントの初期化処理
                this.EventInit();

                // 合計値取得し、画面に設定
                this.GetTotalToFooter();

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

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.myForm.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.myForm.WindowType);

            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ボタンのイベント初期化処理
        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //// すべて在庫単位同じ場合
                //if (isAllUnitSame)
                //{
                //    //[F7]比率セットボタンのイベント生成
                //    this.parentForm.bt_func7.Click += new EventHandler(this.Bt_Func7_Click);

                //    //[F8]比率クリアボタンのイベント生成
                //    this.parentForm.bt_func8.Click += new EventHandler(this.Bt_Func8_Click);
                //}
                //else
                //{
                //    //[F7]比率セットボタンを非活性
                //    this.parentForm.bt_func7.Enabled = false;
                //    //[F8]比率クリアボタンを非活性
                //    this.parentForm.bt_func8.Enabled = false;
                //}

                //[F7]比率セットボタンのイベント生成
                this.parentForm.bt_func7.Click += new EventHandler(this.Bt_Func7_Click);
                this.parentForm.bt_func7.CausesValidation = false;

                //[F8]比率クリアボタンのイベント生成
                this.parentForm.bt_func8.Click += new EventHandler(this.Bt_Func8_Click);

                //[F10]行挿入ボタンのイベント生成
                this.parentForm.bt_func10.Click += new EventHandler(this.Bt_Func10_Click);

                //[F11]行削除ボタンのイベント生成
                this.parentForm.bt_func11.Click += new EventHandler(this.Bt_Func11_Click);

                //[F12]閉じるボタンのイベント生成
                this.parentForm.bt_func12.CausesValidation = true;
                this.myForm.C_Regist(parentForm.bt_func12);
                this.parentForm.bt_func12.Click += new EventHandler(this.Bt_Func12_Click);
                parentForm.bt_func12.ProcessKbn = PROCESS_KBN.NEW;

            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 比率モードより、画面コントロール制御
        /// <summary>
        /// 比率モードより、画面コントロール制御
        /// </summary>
        internal void CtrlSettingByHiritsuMode()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 比率セットモード
                if (isHiritsuSetMode)
                {
                    // 在庫品名CDを入力可にする
                    this.myGridView.Columns["ZAIKO_HINMEI_CD"].ReadOnly = true;
                    this.myGridView.Columns["ZAIKO_HINMEI_CD"].ToolTipText = " ";
                    // 在庫数量を入力可にする
                    this.myGridView.Columns["ZAIKO_SUURYOU"].ReadOnly = true;
                    this.myGridView.Columns["ZAIKO_SUURYOU"].ToolTipText = " ";

                    // [F7 比率セット]
                    this.parentForm.bt_func7.Enabled = false;
                    // [F8 比率クリア]
                    this.parentForm.bt_func8.Enabled = true;
                    // [F10 行挿入]
                    this.parentForm.bt_func10.Enabled = false;
                    // [F11 行削除]
                    this.parentForm.bt_func11.Enabled = false;

                    // 在庫品名チェック
                    //this.myGridView.CellValidating -= this.myForm.ZaikoMeisaiIchiran_CellValidating;
                    // 金額、数量合計計算
                    //this.myGridView.CellValidated += this.myForm.ZaikoMeisaiIchiran_CellValidated;
                }
                // 比率クリアモード
                else
                {
                    // 在庫品名CDを入力可にする
                    this.myGridView.Columns["ZAIKO_HINMEI_CD"].ReadOnly = false;
                    this.myGridView.Columns["ZAIKO_HINMEI_CD"].ToolTipText = "半角6桁以内で入力してください";
                    // 在庫数量を入力可にする
                    this.myGridView.Columns["ZAIKO_SUURYOU"].ReadOnly = false;
                    this.myGridView.Columns["ZAIKO_SUURYOU"].ToolTipText = "半角6桁以内で入力してください";

                    // 在庫比率をクリア
                    for (int i = 0; i < this.myGridView.Rows.Count; i++)
                    {
                        this.myGridView["ZAIKO_HIRITSU", i].Value = "";
                    }

                    // [F7 比率セット]
                    this.parentForm.bt_func7.Enabled = true;
                    // [F8 比率クリア]
                    this.parentForm.bt_func8.Enabled = false;
                    // [F10 行挿入]
                    this.parentForm.bt_func10.Enabled = true;
                    // [F11 行削除]
                    this.parentForm.bt_func11.Enabled = true;

                    // 在庫品名チェック
                    //this.myGridView.CellValidating += this.myForm.ZaikoMeisaiIchiran_CellValidating;
                    // 金額、数量合計計算
                    //this.myGridView.CellValidated += this.myForm.ZaikoMeisaiIchiran_CellValidated;
                }

                this.myGridView.Focus();
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
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                return buttonSetting.LoadButtonSetting(thisAssembly, ConstCls.BUTTON_SETTING_XML);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 在庫明細データ取得処理
        /// <summary>
        /// 在庫明細データ取得処理
        /// </summary>
        /// <returns>count</returns>
        [Transaction]
        public int Search()
        {
            int cnt = 0;
            try
            {
                LogUtility.DebugMethodStart();

                M_ZaikoHiritsuDao dao = DaoInitUtility.GetComponent<M_ZaikoHiritsuDao>();
                M_ZAIKO_HIRITSU conEntity = new M_ZAIKO_HIRITSU();
                conEntity.DENSHU_KBN_CD = (Int16)this.myForm.denshuKBN;
                conEntity.HINMEI_CD = Properties.Settings.Default.HinmeiCd;

                this.SearchResult = dao.GetDataForEntity(conEntity);
                cnt = this.SearchResult.Rows.Count;

                return cnt;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(cnt);
            }

        }
        #endregion

        #region 検索結果を一覧に設定
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        /// <returns></returns>
        private void SetIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 画面表示用データエンティティ
                List<ZaikoDetailDto> dspEntitys = new List<ZaikoDetailDto>();
                // 受入データの場合
                if (this.myForm.denshuKBN == DENSHU_KBN.UKEIRE && this.myForm.recZaikoUkeireDetail != null)
                {
                    // 受入データ取得
                    foreach (var entity in this.myForm.recZaikoUkeireDetail)
                    {
                        ZaikoDetailDto temp = new ZaikoDetailDto();
                        temp.ZAIKO_HINMEI_CD = entity.ZAIKO_HINMEI_CD;
                        temp.ZAIKO_RITSU = entity.ZAIKO_RITSU;
                        temp.JYUURYOU = entity.JYUURYOU;
                        temp.TANKA = entity.TANKA;
                        temp.KINGAKU = entity.KINGAKU;

                        dspEntitys.Add(temp);
                    }
                }
                else if (this.myForm.recZaikoShukkaDetail != null)
                {
                    // 出荷データ取得
                    foreach (var entity in this.myForm.recZaikoShukkaDetail)
                    {
                        ZaikoDetailDto temp = new ZaikoDetailDto();
                        temp.ZAIKO_HINMEI_CD = entity.ZAIKO_HINMEI_CD;
                        temp.ZAIKO_RITSU = entity.ZAIKO_RITSU;
                        temp.JYUURYOU = entity.JYUURYOU;
                        temp.TANKA = entity.TANKA;
                        temp.KINGAKU = entity.KINGAKU;

                        dspEntitys.Add(temp);
                    }
                }

                // 0件の場合、1行空行で表示
                if (dspEntitys.Count == 0)
                {
                    // 比率マスタからデータ取得し、画面にセット
                    this.ReSetHiritsu();

                    return;
                }
                else
                {
                    // 編集状態から抜ける
                    this.myGridView.CurrentCell = null;
                    // 明細クリア
                    this.myGridView.Rows.Clear();

                    // 比率合計値を取得
                    var hiritsuTotal = dspEntitys.Sum(r => r.ZAIKO_RITSU.IsNull ? 0 : (decimal)r.ZAIKO_RITSU);
                    // 在庫比率合計 != 0の場合
                    if (hiritsuTotal != 0)
                    {
                        // 比率セットモード
                        isHiritsuSetMode = true;
                    }
                    else
                    {
                        // 比率クリアモード
                        isHiritsuSetMode = false;
                    }

                    //検索結果設定
                    foreach (ZaikoDetailDto entity in dspEntitys)
                    {
                        // 明細行を追加
                        this.myGridView.Rows.Add(1);
                        // 行インデックス
                        int index = this.myGridView.Rows.Count - 1;

                        // 在庫品名CD
                        this.myGridView["ZAIKO_HINMEI_CD", index].Value = entity.ZAIKO_HINMEI_CD;
                        // 在庫単価
                        decimal zaikoTanka = entity.TANKA.IsNull ? 0 : decimal.Parse(entity.TANKA.ToString());
                        this.myGridView["ZAIKO_TANKA", index].Value = CommonCalc.DecimalFormat(zaikoTanka);

                        // 在庫品名マスターからデータ取得
                        M_ZaikoHinmeiDao dao = DaoInitUtility.GetComponent<M_ZaikoHinmeiDao>();
                        M_ZAIKO_HINMEI conEntity = new M_ZAIKO_HINMEI();
                        conEntity.ZAIKO_HINMEI_CD = entity.ZAIKO_HINMEI_CD;
                        var result = dao.GetZaikoHinmei(conEntity);
                        if (result != null && result.Rows.Count > 0)
                        {
                            // 在庫品名略名
                            this.myGridView["ZAIKO_HINMEI_RYAKU", index].Value = result.Rows[0]["ZAIKO_HINMEI_RYAKU"];
                        }

                        // 比率セットモードの場合
                        if (isHiritsuSetMode)
                        {
                            // 在庫比率
                            decimal zaikoHiritsu = entity.ZAIKO_RITSU.IsNull ? 0 : (decimal)entity.ZAIKO_RITSU;
                            this.myGridView["ZAIKO_HIRITSU", index].Value = zaikoHiritsu;

                            // 在庫数量 = 正味重量 * (在庫比率 / 在庫比率合計)
                            decimal zaikoSuuryou = Properties.Settings.Default.SyoumiJyuuryou * (zaikoHiritsu / hiritsuTotal);
                            this.myGridView["ZAIKO_SUURYOU", index].Value = zaikoSuuryou;
                        }
                        else// 比率クリアモード
                        {
                            // 在庫数量 = 0
                            this.myGridView["ZAIKO_SUURYOU", index].Value = entity.JYUURYOU.IsNull ? 0 : (decimal)entity.JYUURYOU; ;
                        }

                        // 金額設定
                        this.SetZaikoKingaku(index);
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
                // フォーカス設定
                //this.myGridView["ZAIKO_HINMEI_CD", 0].Selected = true;
                //this.myGridView.Focus();

                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 比率セットのデータ処理
        /// <summary>
        /// 比率セットのデータ処理
        /// </summary>
        private void ReSetHiritsu()
        {
            // 在庫品名チェック
            this.myGridView.CellValidating -= this.myForm.ZaikoMeisaiIchiran_CellValidating;
            // 金額、数量合計計算
            this.myGridView.CellValidated -= this.myForm.ZaikoMeisaiIchiran_CellValidated;

            // 編集状態から抜ける
            this.myGridView.CurrentCell = null;
            // 明細クリア
            this.myGridView.Rows.Clear();

            // 在庫品名チェック
            this.myGridView.CellValidating += this.myForm.ZaikoMeisaiIchiran_CellValidating;
            // 金額、数量合計計算
            this.myGridView.CellValidated += this.myForm.ZaikoMeisaiIchiran_CellValidated;

            // 明細データ取得
            if (this.Search() == 0)
            {
                // 比率セットモード
                isHiritsuSetMode = false;

                // 空白1行を追加
                this.myGridView.Rows.Add(1);
                return;
            }

            // 比率合計値を取得
            var hiritsuTotal = this.SearchResult.AsEnumerable().Sum(x => x["ZAIKO_HIRITSU"] is DBNull ? 0 : decimal.Parse(x["ZAIKO_HIRITSU"].ToString()));
            // 在庫比率合計 != 0の場合
            if (hiritsuTotal != 0)
            {
                // 比率セットモード
                isHiritsuSetMode = true;
            }
            else
            {
                // 比率クリアモード
                isHiritsuSetMode = false;
            }

            foreach (DataRow row in this.SearchResult.Rows)
            {
                // 明細行を追加
                this.myGridView.Rows.Add(1);
                // 行インデックス
                int index = this.myGridView.Rows.Count - 1;

                // 在庫品名CD
                this.myGridView["ZAIKO_HINMEI_CD", index].Value = row["ZAIKO_HINMEI_CD"].ToString();
                // 在庫品名略名
                this.myGridView["ZAIKO_HINMEI_RYAKU", index].Value = row["ZAIKO_HINMEI_RYAKU"].ToString();
                // 在庫単価
                decimal zaikoTanka = row["ZAIKO_BASE_TANKA"] is DBNull ? 0 : decimal.Parse(row["ZAIKO_BASE_TANKA"].ToString());
                this.myGridView["ZAIKO_TANKA", index].Value = CommonCalc.DecimalFormat(zaikoTanka);

                // 比率セットモード
                if (isHiritsuSetMode)
                {
                    // 在庫比率
                    decimal zaikoHiritsu = row["ZAIKO_HIRITSU"] is DBNull ? 0m : Convert.ToDecimal(row["ZAIKO_HIRITSU"]);
                    this.myGridView["ZAIKO_HIRITSU", index].Value = zaikoHiritsu;

                    // 在庫数量 = 正味重量 * (在庫比率 / 在庫比率合計)
                    decimal zaikoSuuryou = Properties.Settings.Default.SyoumiJyuuryou * (zaikoHiritsu / hiritsuTotal);
                    this.myGridView["ZAIKO_SUURYOU", index].Value = zaikoSuuryou;
                }
                else// 比率クリアモード
                {
                    // 在庫数量 = 0
                    this.myGridView["ZAIKO_SUURYOU", index].Value = 0;
                }

                // 金額設定
                this.SetZaikoKingaku(index);
            }
        }
        #endregion

        #endregion

        #region 業務処理

        #region 合計値を取得し、明細のFooterに設定
        /// <summary>
        /// 合計値を取得し、明細のFooterに設定
        /// </summary>
        private void GetTotalToFooter()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 在庫数量
                decimal suuryou;
                // 在庫金額
                decimal kingaku;

                // 在庫数量合計
                decimal totalSuuryou = 0;
                // 在庫金額合計
                decimal totalKingaku = 0;

                for (int i = 0; i < this.myGridView.Rows.Count; i++)
                {
                    //if (this.myGridView["UNIT_NAME_RYAKU", i].FormattedValue.ToString().ToLower().Equals(ConstCls.UNIT_NAME_KG))
                    //{
                    //    // 在庫数量取得
                    //    float.TryParse(this.myGridView["ZAIKO_SUURYOU", i].FormattedValue.ToString(), out suuryou);
                    //    // 在庫数量を合計
                    //    totalSuuryou += suuryou;
                    //}

                    // 在庫数量取得
                    decimal.TryParse(this.myGridView["ZAIKO_SUURYOU", i].FormattedValue.ToString(), out suuryou);
                    // 在庫数量を合計
                    totalSuuryou += suuryou;

                    // 在庫金額取得
                    decimal.TryParse(this.myGridView["ZAIKO_KINGAKU", i].FormattedValue.ToString(), out kingaku);
                    // 在庫金額を合計
                    totalKingaku += kingaku;
                }

                // 在庫数量合計に値を設定
                this.myForm.txtTotalSuuryou.Text = totalSuuryou.ToString();
                // 在庫金額合計に値を設定
                this.myForm.txtTotalKingaku.Text = CommonCalc.DecimalFormat(totalKingaku);
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

        #region 在庫品名マスターデータを取得
        /// <summary>
        /// 在庫品名マスターデータを取得
        /// </summary>
        /// <param name="zaikoHinnmeiCd">在庫品名CD</param>
        /// <returns>DataTable</returns>
        private DataTable GetZaikoHinmei(string zaikoHinnmeiCd)
        {
            try
            {
                LogUtility.DebugMethodStart(zaikoHinnmeiCd);

                // データを退避テーブル
                DataTable dt = new DataTable();
                // 在庫品名マスターからデータを取得
                M_ZaikoHinmeiDao dao = DaoInitUtility.GetComponent<M_ZaikoHinmeiDao>();
                M_ZAIKO_HINMEI conEntity = new M_ZAIKO_HINMEI();
                conEntity.ZAIKO_HINMEI_CD = zaikoHinnmeiCd;

                // データ取得
                dt = dao.GetZaikoHinmei(conEntity);

                return dt;
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

        #region 比率計算し、設定する。(削除)
        ///// <summary>
        ///// 比率計算し、設定する。
        ///// </summary>
        ///// <param name="rowIndex">アクティブ行のインデックス</param>
        //private void SetZaikoHiritsu(int rowIndex)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(rowIndex);

        //        // 変数初期化
        //        float zaikoSuuryou;
        //        float zaikoHiritsu;

        //        // 在庫数量取得
        //        float.TryParse(this.myGridView["ZAIKO_SUURYOU", rowIndex].FormattedValue.ToString(), out zaikoSuuryou);
        //        // 在庫比率算出
        //        zaikoHiritsu = zaikoSuuryou / Properties.Settings.Default.Suuryou * 100;
        //        // 在庫比率設定
        //        this.myGridView["ZAIKO_HIRITSU", rowIndex].Value = zaikoHiritsu;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("SetZaikoKingaku", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        #endregion

        #region 金額計算し、設定する。
        /// <summary>
        /// 金額計算し、設定する。
        /// </summary>
        /// <param name="rowIndex">アクティブ行のインデックス</param>
        private void SetZaikoKingaku(int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(rowIndex);

                // 変数初期化
                decimal zaikoSuuryou;
                decimal zaikoTanka;
                decimal zaikoKingaku;

                // 在庫数量取得
                decimal.TryParse(this.myGridView["ZAIKO_SUURYOU", rowIndex].FormattedValue.ToString(), out zaikoSuuryou);
                // 在庫単価取得
                decimal.TryParse(this.myGridView["ZAIKO_TANKA", rowIndex].FormattedValue.ToString(), out zaikoTanka);

                // 金額計算し、セットする
                zaikoKingaku = (decimal)zaikoSuuryou * zaikoTanka;
                //this.myGridView.Rows[e.RowIndex].Cells["ZAIKO_KINGAKU"].Value = CommonCalc.DecimalFormat(zaikoKingaku);
                this.myGridView["ZAIKO_KINGAKU", rowIndex].Value = zaikoKingaku;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZaikoKingaku", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 数量合計値チェック
        /// <summary>
        /// 数量合計値チェック
        /// </summary>
        /// <returns></returns>
        private bool IsTotalSuuryouChkOK()
        {
            try
            {
                LogUtility.DebugMethodStart();

                decimal totalSuuryou;
                decimal.TryParse(this.myForm.txtTotalSuuryou.Text, out totalSuuryou);
                if (totalSuuryou > Properties.Settings.Default.NisugataSuuryou)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("isTotalSuuryouChkOK", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 金額合計値チェック
        /// <summary>
        /// 金額合計値チェック
        /// </summary>
        /// <returns></returns>
        private bool IsTotalKingakuChkOK()
        {
            try
            {
                LogUtility.DebugMethodStart();

                decimal totalKingaku;
                decimal.TryParse(this.myForm.txtTotalKingaku.Text, out totalKingaku);
                if (totalKingaku > Properties.Settings.Default.Kingaku)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("isTotalSuuryouChkOK", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 在庫品名CD入力チェック処理
        /// <summary>
        /// 在庫品名CD入力チェック処理
        /// </summary>
        /// <returns>bool(OK:true NG:false)</returns>
        private bool IsZaikoHinmeiChkOK()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // アクティブ行を取得
                DataGridViewRow row = this.myGridView.CurrentRow;
                // 在庫品名CDセル
                DataGridViewCell cellZaikoHinmei = this.myGridView.CurrentCell;

                // 在庫品名CD取得
                string zaikoHinmeiCd = cellZaikoHinmei.EditedFormattedValue.ToString();

                // 未入力の場合、処理中止
                if (string.IsNullOrEmpty(zaikoHinmeiCd))
                {
                    // 在庫品名設定
                    row.Cells["ZAIKO_HINMEI_RYAKU"].Value = string.Empty;
                    return true;
                }

                // 在庫品名CDに'0'埋め
                zaikoHinmeiCd = zaikoHinmeiCd.PadLeft(6, '0');
                this.myGridView.CurrentCell.Value = this.myGridView.CurrentCell.Value.ToString().PadLeft(6, '0').ToUpper();

                // 在庫品名取得
                DataTable dt = this.GetZaikoHinmei(zaikoHinmeiCd);
                if (dt.Rows.Count == 0)
                {
                    // 在庫品名クリア
                    row.Cells["ZAIKO_HINMEI_RYAKU"].Value = string.Empty;
                    ((DgvCustomAlphaNumTextBoxCell)cellZaikoHinmei).IsInputErrorOccured = true;
                    //cellZaikoHinmei.Selected = true;
                    msgLogic.MessageBoxShow("E020", "在庫品名");
                    return false;
                }

                // 在庫品名設定
                row.Cells["ZAIKO_HINMEI_RYAKU"].Value = dt.Rows[0]["ZAIKO_HINMEI_RYAKU"].ToString();

                return true;
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

        #region 在庫明細一覧チェック処理
        /// <summary>
        /// 在庫明細一覧チェック処理
        /// </summary>
        /// <returns></returns>
        private bool IsZaikoMeisaiIchiranChkOK()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //// 必須チェック設定
                //for (int i = 0; i < this.myGridView.Rows.Count; i++)
                //{
                //    DataGridViewRow row = this.myGridView.Rows[i];

                //    // 在庫品名必須チェック設定
                //    SelectCheckDto existCheck = new SelectCheckDto();
                //    existCheck.CheckMethodName = "必須チェック";
                //    Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
                //    existChecks.Add(existCheck);

                //    if (i == 0)
                //    {
                //        PropertyUtility.SetValue(row.Cells["ZAIKO_HINMEI_CD"], "RegistCheckMethod", existChecks);
                //    }
                //    else
                //    {
                //        PropertyUtility.SetValue(row.Cells["ZAIKO_HINMEI_CD"], "RegistCheckMethod", null);
                //    }
                //}

                // 必須チェックの項目を取得
                var autoCheckLogic = new AutoRegistCheckLogic(this.myForm.GetAllControl(), this.myForm.GetAllControl());
                // 必須チェックを実行する
                this.myForm.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                // エラーがある場合
                if (this.myForm.RegistErrorFlag)
                {
                    // 処理中止
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsZaikoMeisaiIchiranChkOK", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region DataGridViewのCellValidating処理
        /// <summary>
        /// DataGridViewのCellValidating処理
        /// </summary>
        /// <param name="e"></param>
        /// <returns>bool</returns>
        public bool DataGridViewCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 在庫品名CDの場合
                if (this.myGridView.Columns[e.ColumnIndex].Name.Equals("ZAIKO_HINMEI_CD")
                    && !this.myGridView.Columns["ZAIKO_HINMEI_CD"].ReadOnly)
                {
                    return this.IsZaikoHinmeiChkOK();
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
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region DataGridViewのCellValidated処理
        /// <summary>
        /// DataGridViewのCellValidated処理
        /// </summary>
        /// <param name="e"></param>
        public void DataGridViewCellValidated(DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 在庫数量の場合
                if (this.myGridView.Columns[e.ColumnIndex].Name.Equals("ZAIKO_SUURYOU"))
                {
                    // 読取専用の場合
                    if (this.myGridView.Columns["ZAIKO_SUURYOU"].ReadOnly)
                    {
                        // 処理中止
                        return;
                    }

                    // 金額設定
                    this.SetZaikoKingaku(e.RowIndex);

                    // 合計値を計算
                    this.GetTotalToFooter();

                    return;
                }

                // 在庫単価の場合
                if (this.myGridView.Columns[e.ColumnIndex].Name.Equals("ZAIKO_TANKA"))
                {
                    // 金額設定
                    this.SetZaikoKingaku(e.RowIndex);

                    // 在庫単価フォーマット設定
                    decimal zaikoTanka;
                    // 在庫単価取得
                    decimal.TryParse(this.myGridView["ZAIKO_TANKA", e.RowIndex].FormattedValue.ToString(), out zaikoTanka);
                    if (zaikoTanka > 0)
                    {
                        this.myGridView["ZAIKO_TANKA", e.RowIndex].Value = CommonCalc.DecimalFormat(zaikoTanka);
                    }

                    // 合計値を計算
                    this.GetTotalToFooter();

                    return;
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

        #region DataGridViewのCellEnter処理
        /// <summary>
        /// DataGridViewのCellEnter処理
        /// </summary>
        /// <param name="e"></param>
        public void DataGridViewCellEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 在庫単価の場合
                if (this.myGridView.Columns[e.ColumnIndex].Name.Equals("ZAIKO_TANKA"))
                {
                    // カンマ削除
                    this.myGridView["ZAIKO_TANKA", e.RowIndex].Value = this.myGridView["ZAIKO_TANKA", e.RowIndex].FormattedValue.ToString().Replace(",", "");
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

        #region [F7]比率セット処理
        /// <summary>
        /// [F7]比率セット処理
        /// </summary>
        private void Bt_Func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 確認メッセージ表示、「いいえ」の場合、処理中止
                if (msgLogic.MessageBoxShow("C046", "入力データを削除し在庫比率マスタから取り込み").Equals(DialogResult.No))
                {
                    // 処理中止
                    return;
                }

                // 比率マスタからデータを取得し、画面にセット
                this.ReSetHiritsu();

                // 比率モードより、画面コントロール制御
                this.CtrlSettingByHiritsuMode();

                // 合計値を計算
                this.GetTotalToFooter();
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

        #region [F8]比率クリア処理
        /// <summary>
        /// [F8]比率クリア処理
        /// </summary>
        private void Bt_Func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 比率クリアモード
                isHiritsuSetMode = false;

                // コントロール制御
                this.CtrlSettingByHiritsuMode();
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

        #region [F10]行挿入処理
        /// <summary>
        /// [F10]行挿入処理
        /// </summary>
        private void Bt_Func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 選択されていない場合
                if (this.myGridView.CurrentRow.Index < 0)
                {
                    // 処理終了
                    return;
                }

                // 選択行のインデックス取得
                int selectedIndex = this.myGridView.CurrentRow.Index;
                // 選択行の下に1行を挿入
                this.myGridView.Rows.Insert(selectedIndex + 1, 1);
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

        #region [F11]行削除処理
        /// <summary>
        /// [F11]行削除処理
        /// </summary>
        private void Bt_Func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 選択されていない場合
                if (this.myGridView.CurrentRow.Index < 0)
                {
                    // 処理終了
                    return;
                }

                // 確認メッセージ表示、「いいえ」の場合、処理中止
                if (msgLogic.MessageBoxShow("C024").Equals(DialogResult.No))
                {
                    // 処理中止
                    return;
                }

                // 選択行のインデックス取得
                int selectedIndex = this.myGridView.CurrentRow.Index;

                // 選択行を削除
                this.myGridView.Rows.RemoveAt(selectedIndex);

                // 全部削除された場合
                if (this.myGridView.Rows.Count.Equals(0))
                {
                    // 1行を追加
                    this.myGridView.Rows.Add();
                }

                // 合計値取得し、画面に設定
                this.GetTotalToFooter();

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

        #region [F12]閉じる処理
        /// <summary>
        /// [F12]閉じる処理
        /// </summary>
        private void Bt_Func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // エラーがある場合
                if (this.myForm.RegistErrorFlag)
                {
                    // 処理中止
                    return;
                }

                //// 必須項目チェックを行う
                //if (!this.IsZaikoMeisaiIchiranChkOK())
                //{
                //    this.myGridView.CurrentCell = tmpCell;
                //    return;
                //}


                //// アクティブセル
                //DataGridViewCell cell = this.myGridView.CurrentCell;

                //// 単価・数量フォーカスの場合
                //if (cell.OwningColumn.Name.Equals("ZAIKO_TANKA") || cell.OwningColumn.Name.Equals("ZAIKO_SUURYOU"))
                //{
                //    // 在庫単価、在庫数量が選択状態の場合、金額計算処理を行う
                //    this.DataGridViewCellValidated(new DataGridViewCellEventArgs(cell.ColumnIndex, cell.RowIndex));
                //}

                // 在庫数量合計、在庫金額合計
                decimal totalSuuryou;
                decimal totalKingaku;
                decimal.TryParse(this.myForm.txtTotalSuuryou.Text, out totalSuuryou);
                decimal.TryParse(this.myForm.txtTotalKingaku.Text, out totalKingaku);
                // メッセージ内容
                string msg = string.Empty;
                // 在庫数量合計と正味重量が合っていない場合
                if (Properties.Settings.Default.SyoumiJyuuryou != totalSuuryou)
                {
                    msg += "在庫数量合計と正味重量が合っていません。\r\n";
                }
                // 在庫金額合計と金額が合っていない場合
                if (Properties.Settings.Default.Kingaku != totalKingaku)
                {
                    msg += "在庫金額合計と金額が合っていません。\r\n";
                }
                // 数量または金額が合っていない場合
                if (!string.IsNullOrEmpty(msg))
                {
                    msg += "画面を終了";
                    // 確認メッセージ表示、「いいえ」の場合、処理中止
                    if (msgLogic.MessageBoxShow("C046", msg).Equals(DialogResult.No))
                    {
                        // 処理中止
                        return;
                    }
                }

                // 受入データの場合
                if (this.myForm.denshuKBN == DENSHU_KBN.UKEIRE)
                {
                    // 返却値初期化
                    this.myForm.RetZaikoUkeireDetail = new List<T_ZAIKO_UKEIRE_DETAIL>();

                    // 明細行ループ
                    for (int i = 0; i < this.myGridView.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.myGridView.Rows[i];

                        // 在庫明細_受入Entity
                        T_ZAIKO_UKEIRE_DETAIL entity = new T_ZAIKO_UKEIRE_DETAIL();

                        // 行番号
                        entity.ROW_NO = (Int16)(i + 1);

                        // 在庫品名CD
                        entity.ZAIKO_HINMEI_CD = row.Cells["ZAIKO_HINMEI_CD"].FormattedValue.ToString().PadLeft(6, '0');
                        // 比率
                        decimal hiritsu;
                        decimal.TryParse(row.Cells["ZAIKO_HIRITSU"].FormattedValue.ToString(), out hiritsu);
                        entity.ZAIKO_RITSU = hiritsu;
                        // 重量
                        decimal jyuuryou;
                        decimal.TryParse(row.Cells["ZAIKO_SUURYOU"].FormattedValue.ToString(), out jyuuryou);
                        entity.JYUURYOU = jyuuryou;
                        // 単価
                        decimal tanka;
                        decimal.TryParse(row.Cells["ZAIKO_TANKA"].FormattedValue.ToString(), out tanka);
                        entity.TANKA = tanka;
                        // 金額
                        decimal kingaku;
                        decimal.TryParse(row.Cells["ZAIKO_KINGAKU"].FormattedValue.ToString(), out kingaku);
                        entity.KINGAKU = kingaku;

                        // 削除フラグ
                        entity.DELETE_FLG = false;

                        // 返却値にセット
                        this.myForm.RetZaikoUkeireDetail.Add(entity);
                    }
                }
                else
                {
                    // 返却値初期化
                    this.myForm.RetZaikoShukkaDetail = new List<T_ZAIKO_SHUKKA_DETAIL>();

                    // 明細行ループ
                    for (int i = 0; i < this.myGridView.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.myGridView.Rows[i];

                        // 在庫明細_出荷Entity
                        T_ZAIKO_SHUKKA_DETAIL entity = new T_ZAIKO_SHUKKA_DETAIL();

                        // 行番号
                        entity.ROW_NO = (Int16)(i + 1);

                        // 在庫品名CD
                        entity.ZAIKO_HINMEI_CD = row.Cells["ZAIKO_HINMEI_CD"].FormattedValue.ToString().PadLeft(6, '0');
                        // 比率
                        decimal hiritsu;
                        decimal.TryParse(row.Cells["ZAIKO_HIRITSU"].FormattedValue.ToString(), out hiritsu);
                        entity.ZAIKO_RITSU = hiritsu;
                        // 重量
                        decimal jyuuryou;
                        decimal.TryParse(row.Cells["ZAIKO_SUURYOU"].FormattedValue.ToString(), out jyuuryou);
                        entity.JYUURYOU = jyuuryou;
                        // 単価
                        decimal tanka;
                        decimal.TryParse(row.Cells["ZAIKO_TANKA"].FormattedValue.ToString(), out tanka);
                        entity.TANKA = tanka;
                        // 金額
                        decimal kingaku;
                        decimal.TryParse(row.Cells["ZAIKO_KINGAKU"].FormattedValue.ToString(), out kingaku);
                        entity.KINGAKU = kingaku;

                        // 削除フラグ
                        entity.DELETE_FLG = false;

                        // 返却値にセット
                        this.myForm.RetZaikoShukkaDetail.Add(entity);
                    }
                }

                // フォームを閉じる(エラーになる)
                this.myForm.Close();

                // 親フォームを閉じる
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

        #region 必須メソッド
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

        //public int Search()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
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

        #endregion
    }
}
