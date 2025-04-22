using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

namespace FixChouhyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private M_LIST_PATTERN dto;

        /// <summary>
        /// Form
        /// </summary>
        private FormMain form;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(FormMain targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new M_LIST_PATTERN();
            this.Dao = DaoInitUtility.GetComponent<IMLPDao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>DAO(マスターリストパターンのDTO)</summary>
        public IMLPDao Dao { get; private set; }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
    }
}
