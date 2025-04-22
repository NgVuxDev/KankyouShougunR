using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.MasterAccess;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.CustomControl;
using r_framework.Utility;

namespace r_framework.Access
{
    class UketsukeSlipAccess : IMasterDataAccess
    {
        /// <summary>
        /// 日連番
        /// </summary>
        private IT_UKETSUKE_SLIPDao Dao;

        /// <summary>
        /// Entity
        /// </summary>
        public SuperEntity Entity { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        /// <summary>
        /// メッセージユーティリティ
        /// </summary>
        private MessageUtility Message { get; set; }

        public object[] Param { get; set; }

        public object[] SendParam { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control"></param>
        /// <param name="obj"></param>
        /// <param name="sendParam"></param>
        public UketsukeSlipAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            Dao = DaoInitUtility.GetComponent<IT_UKETSUKE_SLIPDao>();
        }

        /// <summary>
        /// 番号がMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="data">キーとなる情報を設定したEntity</param>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out int maxPlusKeyValue)
        {
            maxPlusKeyValue = -1;

            // 基本データ型の場合、Exceptionでエラーハンドリングしたくないので
            // メソッド内で判定
            int maxKey = this.Dao.GetMaxKey();

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
