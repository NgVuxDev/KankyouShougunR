
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.MasterAccess;
using r_framework.Utility;
using Shougun.Core.Master.HikiaiGyousha.Dao;
using r_framework.Dao;

namespace Shougun.Core.Master.HikiaiGyousha.DBAccesser
{
    public class HikiaiGyoushaMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 取引先マスタのDao
        /// </summary>
        // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        private Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_GYOUSHADao Dao;
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

        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;
        // 201400709 syunrei #947 №19　end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiGyoushaMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
            Dao = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_GYOUSHADao>();
        }
        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiGyoushaMasterAccess(ICustomControl control, object[] obj, object[] sendParam, bool bl)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
        }
        // 201400709 syunrei #947 №19　end

        public void SettingFieldInit()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.InitCheckDateField();
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

            controlUtil.setCheckDate((M_GYOUSHA)Entity);
        }

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out int maxPlusKeyValue)
        {
            var maxPlusKey = this.Dao.GetMaxPlusKey();

            var allKeyDate = this.Dao.GetDateByChokuchiKbn1();

            foreach (M_HIKIAI_GYOUSHA gyousyaEntity in allKeyDate)
            {
                var gyoushacd = int.Parse(gyousyaEntity.GYOUSHA_CD);
                if (gyoushacd == maxPlusKey)
                {
                    maxPlusKey = gyoushacd + 1;
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
        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 通常マスタ、CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out int maxPlusKeyValue, bool bl)
        {
            var maxPlusKey = this.daoGyousha.GetMaxPlusKey();

            var allKeyDate = this.daoGyousha.GetDateByChokuchiKbn1();

            foreach (M_GYOUSHA gyousyaEntity in allKeyDate)
            {
                var gyoushacd = int.Parse(gyousyaEntity.GYOUSHA_CD);
                if (gyoushacd == maxPlusKey)
                {
                    maxPlusKey = gyoushacd + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.daoGyousha.GetMinBlankNo(null);
                if (this.CdMaxLength < maxPlusKey.ToString().Length)
                {
                    return true;
                }
            }
            maxPlusKeyValue = maxPlusKey;
            return false;
        }
        // 201400709 syunrei #947 №19　end
    }
}
