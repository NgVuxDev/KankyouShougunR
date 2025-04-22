using System;
using System.Data;
using System.Windows.Forms;
using r_framework.FormManager;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.Stock.ZaikoHinmeiHuriwake
{
    public class G633 : IShougunForm
    {
        public DataTable ZaikoTable { get; private set; }

        public Form CreateForm(params object[] args)
        {
            if (args.Length != 5 ||
                !(args[0] is short) || !(args[1] is string) || !(args[2] is string) || !(args[3] is decimal) || !(args[4] is DataTable))
            {
                throw new ArgumentException();
            }

            UIForm form = new UIForm(Convert.ToInt16(args[0]), Convert.ToDecimal(args[3]), args[4] as DataTable);
            UIHeader header = new UIHeader(args[1] as string, args[2] as string);

            this.ZaikoTable = form.ZaikoTable;

            return new BasePopForm(form, header);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            // noop
            return false;
        }

        public void UpdateForm(Form form)
        {
            // noop
        }
    }
}
