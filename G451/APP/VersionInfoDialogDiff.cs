using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Collections;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Configuration;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.Dao;
using Shougun.UserRestrict.URXmlDocument;

namespace Shougun.Core.Common.Login.APP
{
    /// <summary>
    /// バージョン情報差分表示ダイアログ
    /// </summary>
    public partial class VersionInfoDialogDiff : SuperForm
    {
        /// <summary>
        /// 古い構成情報
        /// </summary>
        public Dictionary<int, Dictionary<string, string>> oldConfDictionary { set; get; }

        /// <summary>
        /// 新しい構成情報
        /// </summary>
        public Dictionary<int, Dictionary<string, string>> newConfDictionary { set; get; }

        /// <summary>
        /// 差分有り無しFLG
        /// ture  : 差分有り
        /// false : 差分無し
        /// </summary>
        public bool hasDiff { set; get; } 

        public VersionInfoDialogDiff()
        {
            InitializeComponent();
        }

        public VersionInfoDialogDiff(Dictionary<int, Dictionary<string, string>> oldConfDictionary,
                                     Dictionary<int, Dictionary<string, string>> newConfDictionary)
        {
            InitializeComponent();
            this.oldConfDictionary = oldConfDictionary;
            this.newConfDictionary = newConfDictionary;
            CreateDiffRichText();
        }

        /// <summary>
        /// 画面読み込み時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.spConDiff.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.rtbOldConf.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.rtbNewConf.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.btnInsert.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnCancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);

 	        base.OnLoad(e);
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F11:
                    this.btnInsert.PerformClick();
                    break;
                case Keys.F12:
                    this.btnCancel.PerformClick();
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        /// <summary>
        /// RichTextBoxに表示する文字を作成する
        /// </summary>
        /// <returns></returns>
        private void CreateDiffRichText()
        {
            StringBuilder sbOldConf = new StringBuilder();
            StringBuilder sbNewConf = new StringBuilder();
            ArrayList diffLine = new ArrayList();

            // 最大項目数をLOOP上限にする
            int cnt = oldConfDictionary.Count;
            if (newConfDictionary.Count > oldConfDictionary.Count) 
            {
                cnt = newConfDictionary.Count; 
            }

            for (int i = 0; i < cnt; i++)
            {
                // 既存構成情報
                string oldDicKey = string.Empty;
                string oldDicValue = string.Empty;
                if (oldConfDictionary.Count > i)
                {
                    Dictionary<string, string> oldDicOnePair = oldConfDictionary[i];
                    oldDicKey = oldDicOnePair.Keys.ToArray()[0];
                    oldDicValue = oldDicOnePair.Values.ToArray()[0];

                    // 空白埋め
                    sbOldConf.Append(oldDicKey.PadRight(23 -
                                    (Encoding.GetEncoding("shift_jis").GetByteCount(oldDicKey) - oldDicKey.Length), ' ') + 
                                    ": " + oldDicValue);
                    sbOldConf.Append(Environment.NewLine);
                }

                // 新規構成情報
                string newDicKey = string.Empty;
                string newDicValue = string.Empty;
                if (newConfDictionary.Count > i)
                {
                    Dictionary<string, string> newDicOnePair = newConfDictionary[i];
                    newDicKey = newDicOnePair.Keys.ToArray()[0];
                    newDicValue = newDicOnePair.Values.ToArray()[0];

                    // 空白埋め
                    sbNewConf.Append(newDicKey.PadRight(23 -
                                    (Encoding.GetEncoding("shift_jis").GetByteCount(newDicKey) - newDicKey.Length), ' ') +
                                    ": " + newDicValue);
                    sbNewConf.Append(Environment.NewLine);
                }

                if (string.IsNullOrEmpty(oldDicValue) || 
                    string.IsNullOrEmpty(newDicValue) || 
                    oldDicValue != newDicValue)
                {
                    diffLine.Add(i);
                }
            }

            if (diffLine.Count == 0)
            {
                hasDiff = false;
            }
            else
            {
                hasDiff = true;
            }

            rtbOldConf.Text = sbOldConf.ToString();
            CreateTextOfDecoration(rtbOldConf, diffLine);

            rtbNewConf.Text = sbNewConf.ToString();
            CreateTextOfDecoration(rtbNewConf, diffLine);
        }

        /// <summary>
        /// 文字の装飾
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="diffLine"></param>
        private void CreateTextOfDecoration(RichTextBox rtb, ArrayList diffLine)
        {
            string[] line = rtb.Lines;
            int start = rtb.SelectionStart;
            int fromLength = 0;
            int toLength = 0;

            int lineNum = 0;
            foreach (string str in line)
            {
                toLength += str.Length;

                int valueFindIndex = str.IndexOf(":");
                if (valueFindIndex > 0)
                {
                    // 項目名
                    rtb.SelectionStart = fromLength;
                    rtb.SelectionLength = valueFindIndex + 1;
                    rtb.SelectionColor = Color.ForestGreen;
                    Font fnt = new Font(rtb.SelectionFont.FontFamily,
                                        rtb.SelectionFont.Size,
                                        rtb.SelectionFont.Style | FontStyle.Regular);
                    rtb.SelectionFont = fnt;
                    fnt.Dispose();

                    // 値
                    rtb.SelectionStart = fromLength + valueFindIndex + 1;
                    rtb.SelectionLength = str.Length - valueFindIndex;
                    if ((rtb.Tag ?? string.Empty).ToString() == "New" && hasDiff && diffLine.Contains(lineNum))
                    {
                        rtb.SelectionColor = Color.Red;
                        Font fntValue = new Font(rtb.SelectionFont.FontFamily,
                                                 rtb.SelectionFont.Size,
                                                 rtb.SelectionFont.Style | FontStyle.Bold);
                        rtb.SelectionFont = fntValue;
                        fntValue.Dispose();
                    }
                    rtb.SelectionFont.Dispose();
                    rtb.Select(0, start);
                }

                fromLength += str.Length + 1;
                lineNum++;
            }
        }
    }
}
