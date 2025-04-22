using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.Message;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
{
    /// <summary>
    /// JWNETエラーメッセージ表示ポップアップ
    /// 大した処理ではないため、Form内ロジックを記載する。
    /// もし、処理が膨大になる場合は、LogicClass等を作ること。
    /// </summary>
    public partial class JwnetErrorPopup : SuperForm
    {
        #region フィールド

        #region 定数

        /// <summary>正常コード</summary>
        private readonly string SuccessCode = "0000000";

        /// <summary>データ取得用SQL</summary>
        private readonly string SqlStr = "SELECT JWNET_SEND_LOG.ERROR_CODE AS ERROR_CODE FROM JWNET_SEND_LOG";

        /// <summary>Cell Name (エラーコード)</summary>
        private readonly string ErrorCodeCellName = "ERROR_CODE";

        /// <summary>Cell Name (エラーメッセージ)</summary>
        private readonly string ErrorMessageCellName = "ERROR_MESSAGE";

        #endregion

        /// <summary>dto</summary>
        private JwnetErrorDto dto = new JwnetErrorDto();

        /// <summary>画面に表示するデータ</summary>
        private DataTable dispData = new DataTable();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dto"></param>
        public JwnetErrorPopup(JwnetErrorDto dto)
        {
            InitializeComponent();
            this.dto = dto;
            // データ取得
            this.dispData = this.getDispdata();
        }
        #endregion

        #region 初期化
        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // イベント
            // キャンセルボタン(F12)イベント生成
            this.bt_func12.DialogResult = DialogResult.Cancel;
            this.bt_func12.Click += new EventHandler(this.FormClose);

            // 表示情報設定
            if (!string.IsNullOrEmpty(dto.manifestId))
            {
                this.MANIFEST_ID.Text = dto.manifestId;
            }

            if (!string.IsNullOrEmpty(dto.createDate))
            {
                this.CREATE_DATE.Text = dto.createDate;
            }

            this.customDataGridView1.DataSource = this.dispData;
        }
        #endregion

        #region イベント
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region ユーティリティ
        /// <summary>
        /// JwnetErrorDtoのデータを元にデータを取得する。
        /// 取得件数0等の場合、何もしない。
        /// データがある場合は
        /// </summary>
        /// <rereturns></rereturns>
        private DataTable getDispdata()
        {
            var data = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(this.dto.kanriId)
                    || string.IsNullOrEmpty(this.dto.queSeq))
                {
                    return data;
                }

                // 条件句作成
                string whereStr = string.Format(" WHERE KANRI_ID = '{0}' AND QUE_SEQ = '{1}'", this.dto.kanriId, this.dto.queSeq);
                GetMeisaiInfoDaoCls dao = DaoInitUtility.GetComponent<GetMeisaiInfoDaoCls>();
                var resultData = dao.GetDateForStringSql(SqlStr + whereStr);

                if (resultData == null || resultData.Rows.Count < 1)
                {
                    return data;
                }

                data.Columns.Add(ErrorCodeCellName);
                data.Columns.Add(ErrorMessageCellName);

                // メッセージセット
                foreach (DataRow row in resultData.Rows)
                {
                    if (row[ErrorCodeCellName] == null
                        || string.IsNullOrEmpty(row[ErrorCodeCellName].ToString())
                        || SuccessCode.Equals(row[ErrorCodeCellName].ToString()))
                    {
                        continue;
                    }

                    var dataRow = data.NewRow();
                    dataRow[ErrorCodeCellName] = row[ErrorCodeCellName].ToString();

                    var jwnetErrMesData = Shougun.Core.Message.MessageUtility.GetJwnetErrorMessage(row[ErrorCodeCellName].ToString());
                    if (jwnetErrMesData != null
                        && !string.IsNullOrEmpty(jwnetErrMesData.Id)
                        && !string.IsNullOrEmpty(jwnetErrMesData.Text))
                    {
                        dataRow[ErrorMessageCellName] = jwnetErrMesData.Text;
                    }

                    data.Rows.Add(dataRow);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getDispdata", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                data = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getDispdata", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                data = null;
            }
            finally
            {
                LogUtility.DebugMethodEnd(data);
            }
            return data;
        }

        /// <summary>
        /// 表示可能なエラーメッセージを持っているかどうか
        /// </summary>
        /// <returns>true:持っている、false:持ってない</returns>
        public bool hasDispErrorMessage()
        {
            return (this.dispData != null && this.dispData.Rows.Count > 0);
        }

        #endregion
    }
}
