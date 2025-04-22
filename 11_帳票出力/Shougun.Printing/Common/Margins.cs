using System;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// 印刷設定の余白調整値を保持するクラス
    /// </summary>
    public struct Margins
    {
        public double Left, Top, Right, Bottom;

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}",
                    this.Top, this.Bottom, this.Left, this.Right );
        }

        static public Margins Parse(string str)
        {
            var margin = new Margins();
            var splits = str.Split(new char[] { ',' });
            for (int i = 0; i < splits.Length; i++)
            {
                var v = splits[i].Trim();
                switch (i)
                {
                    case 0: double.TryParse(v, out margin.Top); break;
                    case 1: double.TryParse(v, out margin.Bottom); break;
                    case 2: double.TryParse(v, out margin.Left); break;
                    case 3: double.TryParse(v, out margin.Right); break;
                }
            }
            return margin;
        }

        public void Factor(double factor)
        {
            this.Left *= factor;
            this.Top *= factor;
            this.Right *= factor;
            this.Bottom *= factor;
        }
    }
}
