using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Seasar.Extension.ADO;
using Seasar.Extension.Tx;
using Seasar.Extension.Tx.Impl;
using Seasar.Framework.Container.Factory;
using System.Data;
using r_framework.Utility;

namespace Shougun.Core.Common.BusinessCommon
{

    public class Transaction : IDisposable
    {
        TransactionUtility tranUtil;
        
        //private ITransactionContext _context;
        //private bool _commited;

        /// <summary>
        /// 互換性のために残しています。
        /// 代わりにTransactionUtilityを使用してください。
        /// r_framework.Utility.TransactionUtility.Transaction
        /// </summary>
        public Transaction()
        {
            tranUtil = new TransactionUtility();

            //_commited = false;

            //_context = SingletonS2ContainerFactory.Container.GetComponent(typeof(ITransactionContext)) as ITransactionContext;
            //_context.Begin();
        }

        /// <summary>
        /// 互換性のために残しています。
        /// 代わりにTransactionUtilityを使用してください。
        /// r_framework.Utility.TransactionUtility.GetCommand
        /// </summary>
        public IDbCommand GetCommand(string sql)
        {
            //return ((TransactionContext)_context).DataSouce.GetCommand(sql, _context.Connection, _context.Transaction);
            return tranUtil.GetCommand(sql);
        }

        /// <summary>
        /// 互換性のために残しています。
        /// 代わりにTransactionUtilityを使用してください。
        /// r_framework.Utility.TransactionUtility.Commit
        /// </summary>
        public void Commit()
        {
            tranUtil.Commit();
            //_commited = true;
            //_context.Commit();
        }

        #region IDisposable メンバー
        /// <summary>
        /// 互換性のために残しています。
        /// 代わりにTransactionUtilityを使用してください。
        /// r_framework.Utility.TransactionUtility.Dispose
        /// </summary>
        public void Dispose()
        {
            tranUtil.Dispose();
            /*
            if (_context != null)
            {
                lock (_context)
                {
                    if (_context == null)
                    {
                        return;
                    }

                    if (!_commited)
                    {
                        _context.Rollback();
                        //_context.Transaction.Dispose();ロールバックを呼ぶとログがでるのでそちらを採用
                    }
                    //_context.Dispose(); これを呼ぶと 後続selectが動かなくなる
                    _context = null;
                }
            }
            */
        }

        //~DB()
        //{
        //    Dispose();
        //}
        #endregion
    }
}
