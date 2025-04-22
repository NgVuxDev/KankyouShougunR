using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.SalesPayment.Denpyouichiran.APP
{
    public partial class KensakuControl : UserControl
    {
        public KensakuControl(LogicClass logicclass)
        {
            InitializeComponent();
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = logicclass;
        }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;


        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        // 現場CD
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckGenba();
        }

        // 業者CD
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckGyousha();
        }

        // 荷卸現場CD
        private void NIOROSHI_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckNioroshiba();
        }

        // 荷卸業者CD
        private void NIOROSHIGYOUSYA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckNioroshiGyousha();
        }

        // 運搬業者CD
        private void UNNBANGYOUSYA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUnpanGyousha();
        }

        // 荷積現場CD
        private void NIDUMIGENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckNidumiGenba();
        }

        // 荷積業者CD
        private void NIDUMIGYOUSYA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckNidumiGyousya();
        }

        /// <summary>
        /// 出荷業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKA_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckShukkaGyousha();
        }

        /// <summary>
        /// 出荷現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKA_GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckShukkaGenba();
        }

        // 20160111 BUNN #12111 STR
        /// <summary>
        /// 業者 フォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSYA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.BEFORE_GYOUSYA_CD = this.GYOUSYA_CD.Text;
        }

        /// <summary>
        /// 出荷業者 フォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKA_GYOUSYA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.BEFORE_SHUKKA_GYOUSYA_CD = this.SHUKKA_GYOUSYA_CD.Text;
        }

        /// <summary>
        /// 荷積業者 フォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIDUMIGYOUSYA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.BEFORE_NIDUMIGYOUSYA_CD = this.NIDUMIGYOUSYA_CD.Text;
        }

        /// <summary>
        /// 荷降業者 フォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHIGYOUSYA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.BEFORE_NIOROSHIGYOUSYA_CD = this.NIOROSHIGYOUSYA_CD.Text;
        }
        // 20160111 BUNN #12111 END
    }
}
