using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.Event
{
    public class ConvertedEventArgs
    {
        /// <summary>
        /// 読み
        /// </summary>
        private string yomi;

        /// <summary>
        /// 半角文字か
        /// </summary>
        private bool bSingle;

        /// <summary>
        /// // 読みの文字数
        /// </summary>
        private int yomiLength = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f"></param>
        /// <param name="singleFlag"></param>
        /// <param name="friLen"></param>
        public ConvertedEventArgs(string f, bool singleFlag, int friLen)
        {
            yomi = f;
            bSingle = singleFlag;
            yomiLength = friLen;
        }

        public string YomiString
        {
            get { return yomi; }
        }

        public int YomiLen
        {
            get { return yomiLength; }
        }

        public bool IsSingleChar
        {
            get { return bSingle; }
        }
    }
}
