using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.MasterAccess;
using r_framework.Utility;
using Shougun.Core.Master.HikiaiGenbaHoshu.Dao;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.DBAccesser
{
    /// <summary>
    /// 現場マスタアクセスクラス
    /// </summary>
    public class HikiaiGenbaMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 現場マスタのDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBADao Dao;
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

        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GENBADao daoGenba;
        // 201400709 syunrei #947 №19　end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiGenbaMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
            Dao = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBADao>();
            daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
        }
        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiGenbaMasterAccess(ICustomControl control, object[] obj, object[] sendParam, bool bl)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
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
        public bool IsOverCDLimit(string gyoushaCd,string hikiaiFlg, out int maxPlusKeyValue)
        {
            var maxPlusKey = this.Dao.GetMaxPlusKeyByGyoushaCd(gyoushaCd, hikiaiFlg);
            var allKeyDate = this.Dao.GetDataByShokuchiKbn1(gyoushaCd, hikiaiFlg);

            foreach (M_HIKIAI_GENBA genbaEntity in allKeyDate)
            {
                var genbaCd = int.Parse(genbaEntity.GENBA_CD);
                if (genbaCd == maxPlusKey)
                {
                    maxPlusKey = genbaCd + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.Dao.GetMinBlankNo(gyoushaCd, hikiaiFlg);
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
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="gyoushaCd">絞込みを行う業者CD</param>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(string gyoushaCd, out int maxPlusKeyValue, bool bl)
        {
            var maxPlusKey = this.daoGenba.GetMaxPlusKeyByGyoushaCd(gyoushaCd);
            var allKeyDate = this.daoGenba.GetDataByShokuchiKbn1(gyoushaCd);

            foreach (M_GENBA genbaEntity in allKeyDate)
            {
                var genbaCd = int.Parse(genbaEntity.GENBA_CD);
                if (genbaCd == maxPlusKey)
                {
                    maxPlusKey = genbaCd + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.daoGenba.GetMinBlankNo(gyoushaCd);
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
