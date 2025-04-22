
using System;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
namespace r_framework.MasterAccess
{
    public class KobetsuHinmeiTankaMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// Dao
        /// </summary>
        private IM_KOBETSU_HINMEI_TANKADao Dao;
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
        public KobetsuHinmeiTankaMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEI_TANKADao>();
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
                errorMessage = String.Format(errorMessage, "個別品名単価");
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

            controlUtil.setCheckDate((M_KOBETSU_HINMEI_TANKA)Entity);
        }

        /// <summary>
        /// SYS_IDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out long maxPlusKeyValue)
        {
            maxPlusKeyValue = -1;

            // 基本データ型の場合、Exceptionでエラーハンドリングしたくないので
            // メソッド内で判定
            long maxKey = this.Dao.GetMaxKey();

            try
            {
                checked
                {
                    maxKey += 1;
                }
            }
            catch (OverflowException e)
            {
                return true;
            }

            maxPlusKeyValue = maxKey;
            return false;
        }

    }
}
