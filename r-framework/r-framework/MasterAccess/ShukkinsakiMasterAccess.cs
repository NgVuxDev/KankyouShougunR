using System;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace r_framework.MasterAccess
{
    public class ShukkinnsakiMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 入金先マスタのDao
        /// </summary>
        private IM_SYUKKINSAKIDao Dao;

        /// <summary>
        /// 取引先マスタのDao
        /// </summary>
        private IM_TORIHIKISAKIDao toriDao;

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
        /// CDのMax桁数
        /// </summary>
        public readonly int CdMaxLength = 6;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control"></param>
        /// <param name="obj"></param>
        /// <param name="sendParam"></param>
        public ShukkinnsakiMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;

            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();
            toriDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
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

            controlUtil.setCheckDate((M_SYUKKINSAKI)Entity);
        }

        public string CodeDeletedCheck()
        {
            string errorMessage = this.RegistCodeCheck(false);
            return errorMessage;
        }

        /// <summary>
        /// コード存在チェック
        /// </summary>
        /// <returns></returns>
        private string CodeCheck()
        {

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_SYUKKINSAKI entity = new M_SYUKKINSAKI();
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

            entity.SYUKKINSAKI_CD = CheckControl.GetResultText();

            var returnEntitys = Dao.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (returnEntitys.Length == 0)
            {
                //コードが存在しない場合エラー
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                errorMessage = String.Format(errorMessage, "出金先");
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
        /// <returns></returns>
        private string RegistCodeCheck(bool presenceFlag)
        {

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_SYUKKINSAKI entity = new M_SYUKKINSAKI();
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

            entity.SYUKKINSAKI_CD = CheckControl.GetResultText();

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
                    errorMessage = String.Format(errorMessage, "出金先");
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out int maxPlusKeyValue)
        {
            var maxPlusKey = this.Dao.GetMaxPlusKey();

            var allKeyDate = this.toriDao.GetDateByChokuchiKbn1();
            foreach (M_TORIHIKISAKI torihikiEntity in allKeyDate)
            {
                var torihikisakiCd = int.Parse(torihikiEntity.TORIHIKISAKI_CD);
                if (torihikisakiCd == maxPlusKey)
                {
                    maxPlusKey = torihikisakiCd + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.Dao.GetMinBlankNo(null);
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
