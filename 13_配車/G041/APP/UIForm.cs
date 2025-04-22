// $Id: UIForm.cs 32824 2014-10-20 10:31:57Z takeda $
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

namespace Shougun.Core.Allocation.ContenaIchiran
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 設置コンテナ一覧画面ロジック
        /// </summary>
        private LogicCls logic;

        private string oldGyousyuCd="";

        // <summary>
        /// header_new
        /// </summary>
        UIHeader header_new;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region UIForm
        // <summary>
        /// UIForm
        /// </summary>
        public UIForm( UIHeader headerForm)
            : base(WINDOW_ID.T_CONTENA_ICHIRAN,WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            //Header部の項目を初期化
            this.header_new = headerForm;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            //ロジックに、Header部情報を設定する
            logic.SetHeader(header_new);
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }
        #endregion

        #region 現場有効性チェック処理
        /// <summary>
        /// 現場有効性チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_HEADER_Validating(object sender, CancelEventArgs e)
        {
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.logic.GENBA_CD_HEADER_Validating(this.GENBA_CD_HEADER,e);

            var gyoushaCd = this.GYOUSHA_CD_HEADER.Text;
            var genbaCd = this.GENBA_CD_HEADER.Text;

            if (!this.logic.ErrorCheckGenba(gyoushaCd, genbaCd))
            {
                this.GENBA_CD_HEADER.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.GENBA_CD_HEADER.Text))
            {
                this.GENBA_CD_HEADER.Text = String.Empty;
                this.GENBA_NAME_RYAKU_HEADER.Text = String.Empty;
            }
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }
        #endregion

        #region コンテナ有効性チェック処理
        /// <summary>
        /// コンテナ有効性チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_CD_HEADER_Validating(object sender, CancelEventArgs e)
        {
            this.logic.CONTENA_CD_HEADER_Validating(this.CONTENA_CD_HEADER,e);
        }
        #endregion

        #region コンテナ種類有効性チェック処理
        /// <summary>
        /// コンテナ種類有効性チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_SHURUI_CD_HEADER_Validating(object sender, CancelEventArgs e)
        {
            // コンテナ固体管理がサポートされたら以下の条件文を有効にする。
            // 現在はコンテナ数量管理のみのため常にFalse
            if (true)
            {
                this.logic.CheckContenaShuruiMaster(this.CONTENA_SHURUI_CD_HEADER, e);
            }
            else
            {
                // コンテナ固体管理の場合、業者情報等を取得
                this.logic.CONTENA_SHURUI_CD_HEADER_Validating(this.CONTENA_SHURUI_CD_HEADER, e);
            }
        }
        #endregion

        #region 業者有効性チェック処理
        /// <summary>
        /// 業者有効性チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_HEADER_Validating(object sender, CancelEventArgs e)
        {
            this.logic.GYOUSHA_CD_HEADER_Validating(this.GYOUSHA_CD_HEADER, e);
        }
        #endregion

        #region 業者選択POPUP修了処理
        /// <summary>
        /// 業者選択POPUP修了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PopupAfterExecuteGyousyaCD()
        {
            r_framework.Utility.LogUtility.DebugMethodStart();

            if (oldGyousyuCd != this.GYOUSHA_CD_HEADER.Text)
            {
                this.GENBA_NAME_RYAKU_HEADER.Text = "";
                this.GENBA_CD_HEADER.Text = "";
            }

            r_framework.Utility.LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMenthod
        /// </summary>
        public void PopupBeforeExecuteGyousyaCD()
        {
            oldGyousyuCd = this.GYOUSHA_CD_HEADER.Text;
        }
        #endregion

        #region 業者Enterイベント
        /// <summary>
        /// 業者Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_HEADER_Enter(object sender, EventArgs e)
        {
            this.logic.beforeGyoushaCd = this.GYOUSHA_CD_HEADER.Text;
        }
        #endregion

        #region 現場Enterイベント
        /// <summary>
        /// 現場Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_HEADER_Enter(object sender, EventArgs e)
        {
            this.logic.beforeGenbaCd = this.GENBA_CD_HEADER.Text;
        }
        #endregion

        #region CellFormattignイベント
        /// <summary>
        /// CellFormattignイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (this.Ichiran.Columns[e.ColumnIndex].Name == "SecchiChouhuku")
            {
                // 目立つ色に変更
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        #endregion

        #region コンテナCD(検索条件)のEnterイベント
        /// <summary>
        /// コンテナCD(検索条件)のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_CD_HEADER_Enter(object sender, EventArgs e)
        {
            this.logic.SetPopupInfoForContenaCdHeader();
        }
        #endregion

        #region コンテナ種類CD(検索条件)のEnterイベント
        /// <summary>
        /// コンテナ種類CD(検索条件)のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_SHURUI_CD_HEADER_Enter(object sender, EventArgs e)
        {
            this.logic.beforeContenaShuruiCd = this.CONTENA_SHURUI_CD_HEADER.Text;
        }
        #endregion
    }
}
