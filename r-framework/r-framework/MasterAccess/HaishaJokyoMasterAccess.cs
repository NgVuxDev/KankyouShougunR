
using System;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
namespace r_framework.MasterAccess
{
    public class HaishaJokyoMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 取引先マスタのDao
        /// </summary>
        private IM_HAISHA_JOKYODao Dao;
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
        public HaishaJokyoMasterAccess()
        {
            Dao = DaoInitUtility.GetComponent<IM_HAISHA_JOKYODao>();
        }
        public HaishaJokyoMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_HAISHA_JOKYODao>();
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

            var checkResultFlag = this.CodeCheck(this.CheckControl.GetResultText());
            if (checkResultFlag)
            {
                this.CodeDataSetting();
            }
            else
            {
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("W001").MESSAGE;
                errorMessage = String.Format(errorMessage, "配車状況");
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
        public bool CodeCheck(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return true;
            }

            Entity = Dao.GetDataByCd(code);
            return Entity != null;
        }

        /// <summary>
        /// 紐付くデータを設定する
        /// </summary>
        public virtual void CodeDataSetting()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.setCheckDate((M_HAISHA_JOKYO)Entity);
        }

        /// <summary>
        /// すべての配車状況レコードを取得する
        /// </summary>
        /// <returns>配車状況レコードのリスト</returns>
        public M_HAISHA_JOKYO[] GetMasterData()
        {
            return Dao.GetAllData();
        }
    }
}
