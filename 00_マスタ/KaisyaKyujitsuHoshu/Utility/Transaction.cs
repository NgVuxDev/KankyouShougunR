using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Seasar.Extension.ADO;
using Seasar.Extension.Tx;
using Seasar.Extension.Tx.Impl;
using Seasar.Framework.Container.Factory;
using System.Data;

namespace KaisyaKyujitsuHoshu.Utility
{

    public class Transaction : IDisposable
    {
        private ITransactionContext _context;
        private bool _commited;
        public Transaction()
        {
            _commited = false;

            _context = SingletonS2ContainerFactory.Container.GetComponent(typeof(ITransactionContext)) as ITransactionContext;
            _context.Begin();

        }

        public IDbCommand GetCommand(string sql)
        {
            return ((TransactionContext)_context).DataSouce.GetCommand(sql, _context.Connection, _context.Transaction);
        }

        public void Commit()
        {
            _commited = true;
            _context.Commit();
        }

        #region IDisposable メンバー

        public void Dispose()
        {
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
        }

        //~DB()
        //{
        //    Dispose();
        //}


        #endregion
    }
}
