using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Inspection.KensyuuIchiran
{
    public partial class KensyuuIchiranForm : SuperForm
    {
        
        /// <summary>
        /// 伝票エンティティ
        /// </summary>
        public KensyuuIchiranDTOCls ParameterDTO { get; private set; }

        private KensyuuIchiranLogicCls logic;

        public KensyuuIchiranForm(KensyuuIchiranHeader header)
            : base(WINDOW_ID.T_KENSYUU_ICHIRAN, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KensyuuIchiranLogicCls(this,header);
        }
       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.gcCustomMultiRow1 != null)
            {
                this.gcCustomMultiRow1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.grdGetMultiRow != null)
            {
                this.grdGetMultiRow.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.label20 != null)
            {
                this.label20.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.SHUKKA_NET_JYUURYOU_TOTAL != null)
            {
                this.SHUKKA_NET_JYUURYOU_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.label21 != null)
            {
                this.label21.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_NET_JYUURYOU_TOTAL != null)
            {
                this.KENSHU_NET_JYUURYOU_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.label22 != null)
            {
                this.label22.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_BUBIKI_TOTAL != null)
            {
                this.KENSHU_BUBIKI_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.label23 != null)
            {
                this.label23.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_URIAGE_KINGAKU_TOTAL != null)
            {
                this.KENSHU_URIAGE_KINGAKU_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.label24 != null)
            {
                this.label24.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_SHIHARAI_KINGAKU_TOTAL != null)
            {
                this.KENSHU_SHIHARAI_KINGAKU_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
            //open KensuyIchiran popup
            object sender = new object();
            this.logic.bt_func8_Click(sender, e);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }


        /// <summary>
        /// CSVボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public virtual void CSVOutput(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //CSV出力チェック

        //        this.logic.ChkCSVOutput();

        //        //CSV出力
        //        this.logic.CSVOutput();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
