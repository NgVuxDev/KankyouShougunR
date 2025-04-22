
using System;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
namespace r_framework.MasterAccess
{
    public class GyoushuMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 取引先マスタのDao
        /// </summary>
        private IM_GYOUSHUDao Dao;
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
        /// コンストラクタ
        /// </summary>
        public GyoushuMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
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

            string errorMessage = string.Empty;
            errorMessage = this.CodeCheck();
            if (errorMessage.Length == 0)
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
        /// コード存在チェック
        /// </summary>
        /// <returns>エラーメッセージ。空の場合はエラーではないCD</returns>
        private string CodeCheck()
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_GYOUSHU entity = new M_GYOUSHU();
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    if (param != null)
                    {
                        entity.SetValue(param);
                    }
                }
            }

            entity.GYOUSHU_CD = CheckControl.GetResultText();
            var returnEntitys = Dao.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (returnEntitys.Length == 0)
            {
                //コードが存在しない場合エラー
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                errorMessage = String.Format(errorMessage, "業種");
            }
            else
            {
                Entity = returnEntitys[0];
            }
            return errorMessage;

        }

        /// <summary>
        /// コード存在チェック
        /// </summary>
        /// <returns>エラーメッセージ。空の場合はエラーではないCD</returns>
        private string RegistCodeCheck(bool presenceFlag)
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_GYOUSHU entity = new M_GYOUSHU();
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    if (param != null)
                    {
                        entity.SetValue(param);
                    }
                }
            }

            entity.GYOUSHU_CD = CheckControl.GetResultText();
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
                else
                {
                    Entity = returnEntitys[0];
                }
            }
            else
            {
                if (returnEntitys.Length != 0)
                {
                    //コードが取得できた場合はエラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E005").MESSAGE;
                    errorMessage = String.Format(errorMessage, "業種");
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

            controlUtil.setCheckDate((M_GYOUSHU)Entity);
        }
    }
}
