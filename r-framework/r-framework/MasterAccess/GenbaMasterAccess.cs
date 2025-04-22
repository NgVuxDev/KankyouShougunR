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
    /// <summary>
    /// 現場マスタアクセスクラス
    /// </summary>
    public class GenbaMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        private IM_GENBADao Dao;
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
        /// CDのMax桁数
        /// </summary>
        public readonly int CdMaxLength = 6;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GenbaMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_GENBADao>();
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
                return string.Empty;
            }

            M_GENBA data = new M_GENBA();

            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    data.SetValue(SendParam[i] as ICustomControl);
                }
            }

            data.GENBA_CD = this.CheckControl.GetResultText();

            // 初期化
            isGotMultipleColumns = false;
            string errorMessage = string.Empty;
            errorMessage = this.CodeCheck(data);
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

            controlUtil.InitCheckDateField();
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public string CodePresenceCheck()
        {
            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_GENBA data = new M_GENBA();
            data.SetValue(this.CheckControl);
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    data.SetValue(SendParam[i] as ICustomControl);
                }
            }

            if (HasEmptyKey(data))
            {
                return string.Empty;
            }

            string errorMessage = this.RegistCodeCheck(data, true);
            return errorMessage;
        }

        /// <summary>
        /// 対象のコードが削除されているかチェック
        /// </summary>
        public string CodeDeletedCheck()
        {
            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_GENBA data = new M_GENBA();
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    data.SetValue(SendParam[i] as ICustomControl);
                }
            }

            data.GENBA_CD = this.CheckControl.GetResultText();

            if (HasEmptyKey(data))
            {
                return string.Empty;
            }

            string errorMessage = this.RegistCodeCheck(data, false);
            return errorMessage;
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public string CodeCheck(M_GENBA data)
        {
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    if (param != null)
                    {
                        data.SetValue(param);
                    }
                }
            }

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            data.GENBA_CD = CheckControl.GetResultText();

            var returnEntitys = Dao.GetAllValidData(data);

            string errorMessage = string.Empty;
            if (returnEntitys.Length == 0)
            {
                //コードが存在しない場合エラー
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                errorMessage = String.Format(errorMessage, "現場");
            }
            else
            {
                if (returnEntitys.Length == 1)
                {
                    Entity = returnEntitys[0];
                }
                else
                {
                    ShowPopupWindow(data);
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
        public string RegistCodeCheck(M_GENBA data, bool presenceFlag)
        {
            if (HasEmptyKey(data))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            data.GENBA_CD = CheckControl.GetResultText();

            var returnEntitys = Dao.GetAllValidData(data);

            string errorMessage = string.Empty;
            if (presenceFlag)
            {
                if (returnEntitys.Length == 0)
                {
                    //コードが存在しない場合エラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E006").MESSAGE;
                }
                else
                {
                    if (returnEntitys.Length == 1)
                    {
                        Entity = returnEntitys[0];
                    }
                }
            }
            else
            {
                if (returnEntitys.Length != 0)
                {
                    //コードが取得できた場合はエラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E037").MESSAGE;
                    errorMessage = String.Format(errorMessage, "業者CD、現場CD");
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

            controlUtil.setCheckDate((M_GENBA)Entity);
        }

        /// <summary>
        /// PKに空データが設定されているか判定
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool HasEmptyKey(M_GENBA data)
        {
            if (data == null
                || string.IsNullOrEmpty(data.GYOUSHA_CD)
                || string.IsNullOrEmpty(data.GENBA_CD))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="gyoushaCd">絞込みを行う業者CD</param>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(string gyoushaCd, out int maxPlusKeyValue)
        {
            var maxPlusKey = this.Dao.GetMaxPlusKeyByGyoushaCd(gyoushaCd);
            var allKeyDate = this.Dao.GetDataByShokuchiKbn1(gyoushaCd);

            foreach (M_GENBA genbaaEntity in allKeyDate)
            {
                var genbaCd = int.Parse(genbaaEntity.GENBA_CD);
                if (genbaCd == maxPlusKey)
                {
                    maxPlusKey = genbaCd + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.Dao.GetMinBlankNo(gyoushaCd);
                if (this.CdMaxLength < maxPlusKey.ToString().Length)
                {
                    return true;
                }
            }

            maxPlusKeyValue = maxPlusKey;
            return false;
        }
    }
}
