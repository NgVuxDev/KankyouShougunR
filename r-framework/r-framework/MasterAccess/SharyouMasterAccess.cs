using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.MasterAccess
{
    public class SharyouMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 車輌
        /// </summary>
        private IM_SHARYOUDao Dao;
        /// <summary>
        /// 業者
        /// </summary>
        private IM_GYOUSHADao DaoGyosya;
        /// <summary>
        /// Entity
        /// </summary>
        public SuperEntity Entity { get; set; }
        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        public object[] Param { get; set; }

        public object[] SendParam { get; set; }

        /// <summary>
        /// チェックメソッドで複数件数取得されたかどうか
        /// true：複数件取得 false:1件以下
        /// </summary>
        private bool isGotMultipleColumns { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SharyouMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            DaoGyosya = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public string CodePresenceCheck()
        {
            string errorMessage = this.RegistCodeCheck(true);
            return errorMessage;
        }

        /// <summary>
        /// 対象のコードが削除されているかチェック
        /// </summary>
        public string CodeDeletedCheck()
        {
            string errorMessage = this.RegistCodeCheck(false);
            return errorMessage;
        }

        /// <summary>
        /// 対象コードのチェックを行った上で
        /// データが存在する場合は指定のControlへセットを行う
        /// </summary>
        public string CodeCheckAndSetting()
        {
            this.SettingFieldInit();

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                this.SettingFieldInit();
                return string.Empty;
            }

            M_SHARYOU data = new M_SHARYOU();
            data.SetValue(this.CheckControl);//PK1つめ（車両コード）セット

            //強制パラメータセット（キーの上書き等や追加条件がある場合）
            for (int i = 0; i < SendParam.Length; i++)
            {
                data.SetValue(SendParam[i] as ICustomControl);
            }

            //ここは未入力の場合くらいしか通らない（必須チェックがない場合）
            if (HasEmptyKey(data))
            {
                return string.Empty;
            }

            // 初期化
            isGotMultipleColumns = false;
            string errorMessage = string.Empty;

            errorMessage = this.CodeCheck();
            // 複数件取れたときは
            if (errorMessage.Length == 0 && !isGotMultipleColumns)
            {
                this.CodeDataSetting();
            }
            return errorMessage;
        }

        public void SettingFieldInit()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            //課題 #1547 複数キーの場合 必ずポップアップが出てしまう対応
            controlUtil.InitCheckDateField(new string[]{"GYOUSHA_CD" , "SHARYOU_CD"});
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public string CodeCheck()
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_SHARYOU entity = new M_SHARYOU();

            //PK1つめ（車両コード）セット
            entity.SHARYOU_CD = CheckControl.GetResultText();


            //PK2つ目(業者コード)セット
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            foreach (ICustomControl c in setField)
            {
                if ("GYOUSHA_CD".Equals(c.DBFieldsName))
                {
                    entity.SetValue(c);
                    break; //見つけたら後続ループは不要
                }
            }

            //強制パラメータセット（キーの上書き等や追加条件がある場合）
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    // 値なしの場合は検索条件に含めない
                    if (param != null && !string.IsNullOrEmpty(param.GetResultText()))
                    {
                        entity.SetValue(param);
                    }
                }
            }


            var returnEntitys = Dao.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (returnEntitys.Length == 0)
            {
                //コードが存在しない場合エラー
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                errorMessage = String.Format(errorMessage, "車輌");
            }
            else
            {
                if (returnEntitys.Length == 1)
                {
                    Entity = returnEntitys[0];
                }
                else
                {
                    //TODO:選択されなかった場合エラーにする必要あり
                    ShowPopupWindow(entity);
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// 複数検索結果時のポップアップウィンドウ表示処理
        /// </summary>
        /// <param name="entity"></param>
        private void ShowPopupWindow(SuperEntity entity)
        {
            CustomControlLogic cstmLogic = new CustomControlLogic(this.CheckControl);
            ControlUtility ctrlUtil = new ControlUtility();
            object[] sendParamArray = null;
            object[] fields = null;

            // DataGrid
            if (this.CheckControl is ICustomDataGridControl)
            {
                var dataGridControl = this.CheckControl as ICustomDataGridControl;
                DataGridViewCell dataGridCell = this.CheckControl as DataGridViewCell;

                if (dataGridControl.PopupSendParams != null)
                {
                    sendParamArray = new object[dataGridControl.PopupSendParams.Length];
                    for (int i = 0; i < dataGridControl.PopupSendParams.Length; i++)
                    {
                        var sendParam = dataGridControl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(dataGridCell.DataGridView.FindForm(), sendParam);
                    }
                }
                var customDataGridView = dataGridCell.DataGridView as CustomDataGridView;
                fields = new object[customDataGridView.CurrentRow.Cells.Count];
            }
            // MultiRow
            else if (this.CheckControl is Cell)
            {
                var cell = this.CheckControl as Cell;
                Row row = cell.GcMultiRow.Rows[cell.RowIndex];

                if (this.CheckControl.PopupSendParams != null)
                {
                    sendParamArray = new Control[this.CheckControl.PopupSendParams.Length];
                    for (int i = 0; i < this.CheckControl.PopupSendParams.Length; i++)
                    {
                        var sendParam = this.CheckControl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(cell.GcMultiRow.FindForm(), sendParam);
                    }
                }
                fields = row.Cells.ToArray();
            }
            else
            {
                var control = this.CheckControl as Control;

                ctrlUtil.ControlCollection = control.Parent.Controls;

                if (this.CheckControl.PopupSendParams != null)
                {
                    sendParamArray = new object[this.CheckControl.PopupSendParams.Length];
                    for (int i = 0; i < this.CheckControl.PopupSendParams.Length; i++)
                    {
                        var sendParam = this.CheckControl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(control.FindForm(), sendParam);
                    }
                }
                fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(control));
            }

            cstmLogic.ShowPopupWindow(this.CheckControl, fields, this.CheckControl, sendParamArray, true, entity);
            this.isGotMultipleColumns = true;
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        /// <param name="presenceFlag"></param>
        public string RegistCodeCheck(bool presenceFlag)
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_SHARYOU entity = new M_SHARYOU();
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    // 値なしの場合は検索条件に含めない
                    if (param != null && !string.IsNullOrEmpty(param.GetResultText()))
                    {
                        entity.SetValue(param);
                    }
                }
            }

            entity.SHARYOU_CD = CheckControl.GetResultText();

            var returnEntitys = Dao.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (presenceFlag)
            {
                if (returnEntitys.Length == 0)
                {
                    //コードが存在しない場合エラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E006").MESSAGE;
                }
            }
            else
            {
                if (returnEntitys.Length != 0)
                {
                    //コードが取得できた場合はエラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E037").MESSAGE;
                    errorMessage = String.Format(errorMessage, "業者CD、車輌CD");
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// すべてのデータを取得
        /// </summary>
        public SuperEntity[] GetMasterData()
        {
            return Dao.GetAllData();
        }

        /// <summary>
        /// 紐付くデータを設定する
        /// </summary>
        public virtual void CodeDataSetting()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.setCheckDate((M_SHARYOU)Entity);

            //チェック開始時にクリアはするが、業者名が再セットされなかった
            M_GYOUSHA gyosya = DaoGyosya.GetDataByCd(((M_SHARYOU)Entity).GYOUSHA_CD );
            controlUtil.setCheckDate(gyosya);

        }

        /// <summary>
        /// PKに空データが設定されているか判定
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool HasEmptyKey(M_SHARYOU data)
        {
            if (data == null || (string.IsNullOrEmpty(data.GYOUSHA_CD) && string.IsNullOrEmpty(data.SHARYOU_CD)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
