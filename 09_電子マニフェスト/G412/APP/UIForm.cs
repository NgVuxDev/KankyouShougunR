using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Message;

namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public UIForm()
            : base(WINDOW_ID.T_TUSHIN_RIREKI_SHOKAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.cdgrid_tuusinnrireki != null)
            {
                this.cdgrid_tuusinnrireki.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
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

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }

        /// <summary>
        /// エラー詳細画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowJwnetErrorMessagePopup(object sender, EventArgs e)
        {
            var row = this.cdgrid_tuusinnrireki.CurrentRow;

            if (row == null)
            {
                return;
            }

            string maniIdCellName = "Column9";
            string crateDateCellName = "Column10";
            string kanriId = "KANRI_ID";
            string queSeq = "QUE_SEQ";

            // 連携Dto生成
            var selectData = new JwnetErrorDto();
            // マニフェスト番号
            if (row.Cells[maniIdCellName].Value != null
                && !string.IsNullOrEmpty(row.Cells[maniIdCellName].Value.ToString()))
            {
                selectData.manifestId = row.Cells[maniIdCellName].Value.ToString();
            }
            // 登録日
            if (row.Cells[crateDateCellName].Value != null
                && !string.IsNullOrEmpty(row.Cells[crateDateCellName].Value.ToString()))
            {
                selectData.createDate = row.Cells[crateDateCellName].Value.ToString();
            }
            // 管理ID
            if (row.Cells[kanriId].Value != null
                && !string.IsNullOrEmpty(row.Cells[kanriId].Value.ToString()))
            {
                selectData.kanriId = row.Cells[kanriId].Value.ToString();
            }
            // キューシーケンス
            if (row.Cells[queSeq].Value != null
                && !string.IsNullOrEmpty(row.Cells[queSeq].Value.ToString()))
            {
                selectData.queSeq = row.Cells[queSeq].Value.ToString();
            }

            var popupForm = new JwnetErrorPopup(selectData);

            if (popupForm.hasDispErrorMessage())
            {
                popupForm.ShowDialog();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 検索ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                bool catchErr = false;
                bool retCheck = this.logic.DateCheck(out catchErr);
                if (catchErr)
                {
                    return;
                }

                if (retCheck)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                //検索条件DTOの作成
                SetSearchCondition();
                //検索実行
                if (this.logic.SearchLogic() == -1)
                {
                    return;
                }

                //画面に結果が反映する
                if (!SetDataToDgv())
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 検索DTOの設定処理
        /// </summary>
        private void SetSearchCondition()
        {
            // マニフェスト番号From
            this.logic.dto.MANIFEST_ID_FROM = cantxt_ManiFestFrom.Text.Trim();
            // マニフェスト番号To
            this.logic.dto.MANIFEST_ID_TO = cantxt_ManiFestTo.Text.Trim();
            // 状態を設定する
            if ("1".Equals(cantxt_Jyoutai.Text.Trim()))//1.全て表示
            {
                this.logic.dto.STATUS_FLAG = "指定なし";
            }
            else if ("2".Equals(cantxt_Jyoutai.Text.Trim()))// 2.送信保留
            {
                this.logic.dto.STATUS_FLAG = "7";
            }
            else if ("3".Equals(cantxt_Jyoutai.Text.Trim()))// 3.送信待ち
            {
                this.logic.dto.STATUS_FLAG = "0";
            }
            else if ("4".Equals(cantxt_Jyoutai.Text.Trim()))// 4.送信完了
            {
                this.logic.dto.STATUS_FLAG = "1";
            }
            else if ("5".Equals(cantxt_Jyoutai.Text.Trim()))// 5.JWNET送信完了
            {
                this.logic.dto.STATUS_FLAG = "2";
            }
            else if ("6".Equals(cantxt_Jyoutai.Text.Trim()))// 6.JWNETエラー
            {
                this.logic.dto.STATUS_FLAG = "9";
            }
            else if ("7".Equals(cantxt_Jyoutai.Text.Trim()))// 7.送信失敗
            {
                this.logic.dto.STATUS_FLAG = "8";
            }
            //20151015 hoanghm #11853 start
            //日付
            if (this.txt_Hidzuke.Text.Equals("1"))// 1.登録日
            {
                this.logic.dto.HIDZUKE_KBN = "1";
            }
            else// 2.更新日
            {
                this.logic.dto.HIDZUKE_KBN = "2";
            }
            this.logic.dto.HIDZUKE_FROM = (DateTime)this.txt_Hidzuke_From.Value;
            this.logic.dto.HIDZUKE_TO = (DateTime)this.txt_Hidzuke_To.Value;
            //20151015 hoanghm #11853 end
        }

        /// <summary>
        /// 検索結果をグリッドに設定
        /// </summary>
        private bool SetDataToDgv()
        {
            try
            {
                cdgrid_tuusinnrireki.IsBrowsePurpose = false;
                //行を全て削除
                cdgrid_tuusinnrireki.Rows.Clear();
                //スクロール位置初期化
                cdgrid_tuusinnrireki.HorizontalScrollingOffset = 0;

                //検索結果設定
                for (int i = 0; i < this.logic.SearchResult.Rows.Count; i++)
                {
                    cdgrid_tuusinnrireki.Rows.Add();
                    // 詳細ボタン
                    cdgrid_tuusinnrireki.Rows[i].Cells[0].Value = "詳細";

                    // 通信状態
                    cdgrid_tuusinnrireki.Rows[i].Cells[1].Value = this.logic.SearchResult.Rows[i]["TUUSINN_STATUS"].ToString();

                    // マニフェスト番号
                    cdgrid_tuusinnrireki.Rows[i].Cells[2].Value = this.logic.SearchResult.Rows[i]["MANIFEST_ID"].ToString();

                    // 内容
                    cdgrid_tuusinnrireki.Rows[i].Cells[3].Value = this.logic.SearchResult.Rows[i]["NAIYO"].ToString();

                    // 通信番号
                    cdgrid_tuusinnrireki.Rows[i].Cells[4].Value =
                        this.logic.SearchResult.Rows[i]["KANRI_ID"].ToString() + "-" +
                        this.logic.SearchResult.Rows[i]["QUE_SEQ"].ToString();

                    // 登録日
                    string createDate = this.logic.SearchResult.Rows[i]["CREATE_DATE"].ToString();
                    if (!string.IsNullOrEmpty(createDate))
                    {
                        cdgrid_tuusinnrireki.Rows[i].Cells[5].Value = createDate;
                    }
                    // 更新日
                    string updateDate = this.logic.SearchResult.Rows[i]["UPDATE_TS"].ToString();
                    if (!string.IsNullOrEmpty(updateDate))
                    {
                        cdgrid_tuusinnrireki.Rows[i].Cells[6].Value = updateDate;
                    }

                    //管理id、非表示列
                    this.cdgrid_tuusinnrireki.Rows[i].Cells["KANRI_ID"].Value = this.logic.SearchResult.Rows[i]["KANRI_ID"];
                    // 非表示列
                    this.cdgrid_tuusinnrireki.Rows[i].Cells["QUE_SEQ"].Value = this.logic.SearchResult.Rows[i]["QUE_SEQ"];
                }
                cdgrid_tuusinnrireki.IsBrowsePurpose = true;

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataToDgv", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #region セールクリック

        /// <summary>
        /// セールクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; //ヘッダのクリック
            }

            //ボタン列クリック
            if (this.cdgrid_tuusinnrireki.CurrentCell is System.Windows.Forms.DataGridViewButtonCell)
            {
                // 管理番号
                string kanriId = this.logic.SearchResult.Rows[
                    this.cdgrid_tuusinnrireki.CurrentRow.Index]["KANRI_ID"].ToString();
                //// レコード枝番
                //string seq = this.logic.SearchResult.Rows[
                //    this.cdgrid_tuusinnrireki.CurrentRow.Index]["QUE_SEQ"].ToString(); これはマニのSEQではない！
                //電子マニフェスト入力画面を表示する
                this.logic.ElectronicManifestNyuryokuShow(kanriId);
            }
        }

        #endregion

        /// <summary>
        /// マニフェスト番号Fromキー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ManiFestFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 入力制限処理
            logic.InputLimit(ref e);

            if (!e.Handled)
            {
                base.OnKeyPress(e);
            }
        }

        /// <summary>
        /// マニフェスト番号Toキー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ManiFestTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 入力制限処理
            logic.InputLimit(ref e);

            if (!e.Handled)
            {
                base.OnKeyPress(e);
            }
        }

        private void txt_Hidzuke_To_DoubleClick(object sender, EventArgs e)
        {
            var FromTextBox = this.txt_Hidzuke_From;
            var ToTextBox = this.txt_Hidzuke_To;

            ToTextBox.Text = FromTextBox.Text;
        }
    }
}