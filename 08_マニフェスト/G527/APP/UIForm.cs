using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill.Attrs;
using r_framework.Utility;
using Seasar.Quill;
using r_framework.CustomControl;
using Microsoft.VisualBasic;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
{
     /// <summary>
    /// G527画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }       
        /// <summary>
        /// 
        /// </summary>
        MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_MANIFEST_SUII, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
             finally
             {
                 LogUtility.DebugMethodEnd();
             }
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit();   
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region Functionボタン 押下処理

        #region [F1]
        /// <summary>
        /// 前年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void previousNumber_Click(object sender, EventArgs e)
        {
           try
           {
               LogUtility.DebugMethodStart(sender, e);

               // 日付チェック
               if (this.logic.DateCheck())
               {
                   return;
               }

               // 翌年を取得
               this.logic.GetPreviousNumber();
           }
           catch (Exception ex)
           {
               // 例外エラー
               LogUtility.Error(ex);
               throw;
           }
           finally
           {
               LogUtility.DebugMethodEnd();
           }

        }
        #endregion

        #region [F2]
        /// <summary>
        /// 翌年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void nextNumber_Click(object sender, EventArgs e)
        {
             try
             {
                 LogUtility.DebugMethodStart(sender, e);

                 // 日付チェック
                 if (this.logic.DateCheck())
                 {
                     return;
                 }

                 // 翌年を取得
                 this.logic.GetNextNumber();
             }
             catch (Exception ex)
             {
                 // 例外エラー
                 LogUtility.Error(ex);
                 throw;
             }
             finally
             {
                 LogUtility.DebugMethodEnd();
             }

        }
        #endregion

        #region [F5]印刷
        /// <summary>
        /// [F5]印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.Print();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F6]CSV出力
        /// <summary>
        /// [F6]CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //CSV出力
                this.logic.CsvOutput();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F8]検索
        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                /// 20141209 teikyou 日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141209 teikyou 日付チェックを追加する　end
                // 画面表示
                this.logic.Search();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CloseForm();

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 出力内容設定
        private void txt_ShuturyokuNaiyoCD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //グリッドの設定
                this.logic.GridViewInit();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion     

        #endregion

        
        /// <summary>
        /// 年月日Toに年月日Fromと同一の日付セット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmDateTimePicker_NengappiTo_DoubleClick(object sender, EventArgs e)
        {
            //年月日Fromに年月日Toと同一の日付セット変更
            //if (!string.IsNullOrEmpty(this.cstmDateTimePicker_NengappiTo.Text))
            //{
            //    this.cstmDateTimePicker_NengappiFrom.Text = this.cstmDateTimePicker_NengappiTo.Text;
            //}
        }
        /// <summary>
        /// 排出事業者CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaishutuJigyoushaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                this.cstmTexBox_HaishutuJigyoushaFrom.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGyousha("排出事業者CDFrom", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "業者");

                    this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "1"))
            {
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();                
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// 排出事業者CDFromポップアップ後設定処理
        /// </summary>
        public void SetGyoushaFromPop()
        {
            // 排出事業場CD制御
            this.HaisyutsuJigyoubaSeigyo();
        }

        /// <summary>
        /// 排出事業者CDFromボタンポップアップ後設定処理
        /// </summary>
        public void SetGyoushaFromPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "1"))
            {
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();               
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// 排出事業者CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaishutuJigyoushaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaTo.Text))
            {
                this.cstmTexBox_HaishutuJigyoushaTo.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGyousha("排出事業者CDTo", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_HaishutuJigyoushaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "2"))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Focus();
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// 排出事業者CDToポップアップ後設定処理
        /// </summary>
        public void SetGyoushaToPop()
        {
            // 排出事業場CD制御
            this.HaisyutsuJigyoubaSeigyo();
        }

        /// <summary>
        /// 排出事業者CDToボタンポップアップ後設定処理
        /// </summary>
        public void SetGyoushaToPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaishutuJigyoushaFrom.Text,
                this.cstmANTexB_HaishutuJigyoushaTo.Text, "2"))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Focus();
            }
            else
            {
                // 排出事業場CD制御
                this.HaisyutsuJigyoubaSeigyo();
            }
        }

        /// <summary>
        /// FromCDとToCDのチェック
        /// </summary>
        /// <param name="fromVal">FromValue</param>
        /// <param name="toVal">ToValue</param>
        /// <param name="msgKbn">
        /// エラーメッセージ区分:1　fromVal > toVal
        ///                      2　toVal > fromVal
        /// </param>
        private bool ChkFromAndTo(string fromVal, string toVal, string msgKbn)
        {
            // 開始コード ＞ 終了コードの場合
            if (!string.IsNullOrEmpty(fromVal) && !string.IsNullOrEmpty(toVal)
                && fromVal.CompareTo(toVal) > 0)
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                if ("1".Equals(msgKbn))
                {
                    messageShowLogic.MessageBoxShow("E032", "終了コード", "開始コード");
                }
                else
                {
                    messageShowLogic.MessageBoxShow("E032", "終了コード", "開始コード");
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 排出事業場の設定
        /// </summary>
        internal void HaisyutsuJigyoubaSeigyo()
        {
            // 排出事業者CDFromと排出事業者CDToが両方未入力の場合
            // 排出事業者CDFromと排出事業者CDToが同一の場合
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text)
                && !string.IsNullOrEmpty(cstmANTexB_HaishutuJigyoushaTo.Text)
                && this.cstmANTexB_HaishutuJigyoushaFrom.Text.Equals(this.cstmANTexB_HaishutuJigyoushaTo.Text))
            {
                this.customPanel_HaisyutsuJigyouba.Enabled = true;
            }
            else
            {
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Text = string.Empty;
                this.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text = string.Empty;
                this.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
                this.customPanel_HaisyutsuJigyouba.Enabled = false;
            }
        }

        /// <summary>
        /// 排出事業場CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaisyutsuJigyoubaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();          

            if (!string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text)
                && string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                messageShowLogic.MessageBoxShow("E034", "排出事業者");
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
                //現場クリア
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Text = string.Empty;
                this.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text = string.Empty;
                this.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
                return;
            }


            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text))
            {
                this.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGenba("排出事業場CDFrom", out catchErr))
                {
                    if (catchErr) { return; }
                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_HaisyutsuJigyoubaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "1"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Focus();
            }
        }

        /// <summary>
        /// 排出事業場CDFromポップアップ後設定処理
        /// </summary>
        public void SetGenbaFromPop()
        {
            // 排出事業者CDFromが設定される場合
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
            }
        }

        /// <summary>
        /// 排出事業場CDFromボタンポップアップ後設定処理
        /// </summary>
        public void SetGenbaFromPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "1"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaFrom.Focus();
            }
            else
            {
                // 排出事業者CDFromが設定される場合
                if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
                {
                    this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                    this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
                }
            }
        }
        /// <summary>
        /// 排出事業場CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_HaisyutsuJigyoubaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaTo.Text)
                && string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                messageShowLogic.MessageBoxShow("E034", "排出事業者");
                this.cstmANTexB_HaishutuJigyoushaFrom.Focus();
                return;
            }

            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_HaisyutsuJigyoubaTo.Text))
            {
                this.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGenba("排出事業場CDTo", out catchErr))
                {
                    if (catchErr) { return; }
                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_HaisyutsuJigyoubaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "2"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaTo.Focus();
            }
        }

        /// <summary>
        /// 排出事業場CDToポップアップ後設定処理
        /// </summary>
        public void SetGenbaToPop()
        {
            // 排出事業者CDFromが設定される場合
            if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
            {
                this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
            }
        }

        /// <summary>
        /// 排出事業場CDToボタンポップアップ後設定処理
        /// </summary>
        public void SetGenbaToPopBtn()
        {
            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_HaisyutsuJigyoubaFrom.Text,
                this.cstmANTexB_HaisyutsuJigyoubaTo.Text, "2"))
            {
                this.cstmANTexB_HaisyutsuJigyoubaTo.Focus();
            }
            else
            {
                // 排出事業者CDFromが設定される場合
                if (!string.IsNullOrEmpty(this.cstmANTexB_HaishutuJigyoushaFrom.Text))
                {
                    this.cstmANTexB_HaishutuJigyoushaTo.Text = this.cstmANTexB_HaishutuJigyoushaFrom.Text;
                    this.cstmTexBox_HaishutuJigyoushaTo.Text = this.cstmTexBox_HaishutuJigyoushaFrom.Text;
                }
            }
        }

        /// <summary>
        /// 運搬受託者CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_UnpanJutakushaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_UnpanJutakushaFrom.Text))
            {
                this.cstmTexBox_UnpanJutakushaFrom.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGyousha("運搬受託者CDFrom", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_UnpanJutakushaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_UnpanJutakushaFrom.Text,
                this.cstmANTexB_UnpanJutakushaTo.Text, "1"))
            {
                this.cstmANTexB_UnpanJutakushaFrom.Focus();
            }
        }

        /// <summary>
        /// 運搬受託者CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_UnpanJutakushaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_UnpanJutakushaTo.Text))
            {
                this.cstmTexBox_UnpanJutakushaTo.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGyousha("運搬受託者CDTo", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_UnpanJutakushaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_UnpanJutakushaFrom.Text,
                this.cstmANTexB_UnpanJutakushaTo.Text, "2"))
            {
                this.cstmANTexB_UnpanJutakushaTo.Focus();
            }
        }

        /// <summary>
        /// 処分受託者CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_ShobunJutakushaFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_ShobunJutakushaFrom.Text))
            {
                this.cstmTexBox_ShobunJutakushaFrom.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGyousha("処分受託者CDFrom", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_ShobunJutakushaFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_ShobunJutakushaFrom.Text,
                this.cstmANTexB_ShobunJutakushaTo.Text, "1"))
            {
                this.cstmANTexB_ShobunJutakushaFrom.Focus();
            }
        }

        /// <summary>
        /// 処分受託者CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_ShobunJutakushaTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_ShobunJutakushaTo.Text))
            {
                this.cstmTexBox_ShobunJutakushaTo.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGyousha("処分受託者CDTo", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.cstmANTexB_ShobunJutakushaTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_ShobunJutakushaFrom.Text,
                this.cstmANTexB_ShobunJutakushaTo.Text, "2"))
            {
                this.cstmANTexB_ShobunJutakushaTo.Focus();
            }
        }

        /// <summary>
        /// 最終処分事業場CDFromのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_SaishuuShobunJouFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_SaishuuShobunJouFrom.Text))
            {
                this.cstmTexBox_SaishuuShobunJouFrom.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGenba("最終処分事業場CDFrom", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_SaishuuShobunJouFrom.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_SaishuuShobunJouFrom.Text,
                this.cstmANTexB_SaishuuShobunJouTo.Text, "1"))
            {
                this.cstmANTexB_SaishuuShobunJouFrom.Focus();
            }
        }

        /// <summary>
        /// 最終処分事業場CDToのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_SaishuuShobunJouTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_SaishuuShobunJouTo.Text))
            {
                this.cstmTexBox_SaishuuShobunJouTo.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkGenba("最終処分事業場CDTo", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "現場");
                    this.cstmANTexB_SaishuuShobunJouTo.Focus();
                    return;
                }
            }

            // FromCDとToCDのチェック
            if (!this.ChkFromAndTo(this.cstmANTexB_SaishuuShobunJouFrom.Text,
                this.cstmANTexB_SaishuuShobunJouTo.Text, "2"))
            {
                this.cstmANTexB_SaishuuShobunJouTo.Focus();
            }
        }

        /// <summary>
        /// 産廃（直行）廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Chokkou_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Chokkou.Text))
            {
                this.cstmTexBox_Chokkou.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkHaikiShurui("直行", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.cstmANTexB_Chokkou.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 産廃（積替）廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Tsumikae_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Tsumikae.Text))
            {
                this.cstmTexBox_Tsumikae.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkHaikiShurui("積替", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.cstmANTexB_Tsumikae.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 建廃廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Kenpai_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Kenpai.Text))
            {
                this.cstmTexBox_Kenpai.Text = string.Empty;
            }
            else
            {
                bool catchErr = false;
                if (!this.logic.ChkHaikiShurui("建廃", out catchErr))
                {
                    if (catchErr) { return; }
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    this.cstmANTexB_Kenpai.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 電子廃棄物種類CDのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Denshi_Enter(object sender, EventArgs e)
        {
            //検索結果
            DataTable dtSearch = new DataTable();
            DataTable dtResult = new DataTable();

            DenshiMasterDataLogic dsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
            dto.ISNOT_NEED_DELETE_FLG = true;
            dtSearch = dsMasterLogic.GetDenshiHaikiShuruiData(dto);

            dtResult.Columns.Add("HAIKI_SHURUI_CD");
            dtResult.Columns.Add("HAIKI_SHURUI_NAME");
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                DataRow dr = dtResult.NewRow();
                dr[0] = dtSearch.Rows[i]["HAIKI_SHURUI_CD"];
                dr[1] = dtSearch.Rows[i]["HAIKI_SHURUI_NAME"];
                dtResult.Rows.Add(dr);
            }

            //データが存在する場合
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                this.cstmANTexB_Denshi.PopupDataHeaderTitle = new string[] { "廃棄物種類コード", "廃棄物種類名" };
                this.cstmANTexB_Denshi.PopupDataSource = dtResult;
                this.cstmANTexB_Denshi.PopupDataSource.TableName = "廃棄物種類検索";
            }
        }

        /// <summary>
        /// 電子廃棄物種類検索ボタンのEnter
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void casbtn_Denshi_Enter(object sender, EventArgs e)
        {
            //検索結果
            DataTable dtSearch = new DataTable();
            DataTable dtResult = new DataTable();

            DenshiMasterDataLogic dsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
            dto.ISNOT_NEED_DELETE_FLG = true;
            dtSearch = dsMasterLogic.GetDenshiHaikiShuruiData(dto);
            dtResult.Columns.Add("HAIKI_SHURUI_CD");
            dtResult.Columns.Add("HAIKI_SHURUI_NAME");
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                DataRow dr = dtResult.NewRow();
                dr[0] = dtSearch.Rows[i]["HAIKI_SHURUI_CD"];
                dr[1] = dtSearch.Rows[i]["HAIKI_SHURUI_NAME"];
                dtResult.Rows.Add(dr);
            }

            //データが存在する場合
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                this.casbtn_Denshi.PopupDataHeaderTitle = new string[] { "廃棄物種類コード", "廃棄物種類名" };
                this.casbtn_Denshi.PopupDataSource = dtResult;
                this.casbtn_Denshi.PopupDataSource.TableName = "廃棄物種類検索";
            }
        }

        /// <summary>
        /// 電子廃棄物種類CDのロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmANTexB_Denshi_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力の場合
            if (string.IsNullOrEmpty(this.cstmANTexB_Denshi.Text))
            {
                this.cstmTexBox_Denshi.Text = string.Empty;
            }
            else
            {

                //検索結果
                DataTable dtSearch = new DataTable();

                DenshiMasterDataLogic dsMasterLogic = new DenshiMasterDataLogic();
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

                dto.HAIKI_SHURUI_CD = this.cstmANTexB_Denshi.Text;
                dto.ISNOT_NEED_DELETE_FLG = true;
                dtSearch = dsMasterLogic.GetDenshiHaikiShuruiData(dto);

                if (dtSearch.Rows.Count == 0)
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                    this.cstmTexBox_Denshi.Text = string.Empty;
                    messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.cstmTexBox_Denshi.Text = dtSearch.Rows[0]["HAIKI_SHURUI_NAME"].ToString();
                }
            }
        }

        /// <summary>
        /// 出力内容のロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmNmTxtB_ShuturyokuNaiyou_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 入力されない場合
            if (string.IsNullOrEmpty(this.cstmNmTxtB_ShuturyokuNaiyou.Text))
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E002", "出力内容", "1～5");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 出力区分のロストフォーカス
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void cstmNmTxtB_ShuturyokuKubun_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 入力されない場合
            if (string.IsNullOrEmpty(this.cstmNmTxtB_ShuturyokuKubun.Text))
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E002", "出力区分", "1～3");
                e.Cancel = true;
            }
        }

        /// <summary> 廃棄物種類活性非活性制御 </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <remark>出力区分：合算⇒すべて活性</remark>
        /// <remark>出力区分：紙のみ⇒電子のみ非活性</remark>
        /// <remark>出力区分：電子のみ⇒電子のみ活性</remark>
        private void cstmNmTxtB_ShuturyokuKubun_TextChanged(object sender, EventArgs e)
        {
            string shuturyokuKubun = this.cstmNmTxtB_ShuturyokuKubun.Text;

            // 出力区分により制御する
            switch (shuturyokuKubun)
            {
                case "1":                                                       // 合算
                    this.cstmPanel_ChokkouHaikibutusyurui.Enabled = true;       // 直行：活性
                    this.cstmPanel_TsumikaeHaikibutusyurui.Enabled = true;      // 積替：活性
                    this.cstmPanel_KenpaiHaikibutusyurui.Enabled = true;        // 建廃：活性
                    this.cstmPanel_DenshiHaikibutusyurui.Enabled = true;        // 電子：活性
                    this.cstmpanel_Kyoten.Enabled = true;                       // 拠点：活性
                    break;

                case "2":                                                       // 紙のみ
                    this.cstmPanel_ChokkouHaikibutusyurui.Enabled = true;       // 直行：活性
                    this.cstmPanel_TsumikaeHaikibutusyurui.Enabled = true;      // 積替：活性
                    this.cstmPanel_KenpaiHaikibutusyurui.Enabled = true;        // 建廃：活性
                    this.cstmPanel_DenshiHaikibutusyurui.Enabled = false;       // 電子：非活性
                    this.cstmpanel_Kyoten.Enabled = true;                       // 拠点：活性
                    break;

                case "3":                                                       // 電子のみ
                    this.cstmPanel_ChokkouHaikibutusyurui.Enabled = false;      // 直行：非活性
                    this.cstmPanel_TsumikaeHaikibutusyurui.Enabled = false;     // 積替：非活性
                    this.cstmPanel_KenpaiHaikibutusyurui.Enabled = false;       // 建廃：非活性
                    this.cstmPanel_DenshiHaikibutusyurui.Enabled = true;        // 電子：活性
                    this.cstmpanel_Kyoten.Enabled = false;                      // 拠点：非活性
                    break;

                default:
                    break;
            }

            //一覧データクリア
            this.customDataGridView1.DataSource = null;
        }
       
    }
}
