// $Id: UIForm.cs 4357 2013-10-22 00:18:55Z sys_dev_12 $
using System;
using r_framework.APP.Base;
using r_framework.Const;
using System.Windows.Forms;
using Shougun.Core.SalesPayment.TankaRirekiIchiran;

namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
{
	/// <summary>
	/// 検収入力画面
	/// </summary>
	public partial class KenshuMeisaiNyuryokuForm : SuperForm
    {
		/// <summary>
		/// 検収入力用DTO
		/// </summary>
		internal KenshuNyuuryokuDTOClass returnDto;

		/// <summary>
		/// 検収入力ロジック
		/// </summary>
		private KenshuMeisaiNyuryokuLogic logic;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KenshuMeisaiNyuryokuForm(KenshuNyuuryokuDTOClass kenshuDto)
            : base(WINDOW_ID.T_KENSYUU_MEISAI_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
			// コンポーネントの初期化
			this.InitializeComponent();

			// 返却用DTOのセット
			this.returnDto = kenshuDto;

            // 検収入力ロジック
            this.logic = new KenshuMeisaiNyuryokuLogic(this);
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
			// 親クラスのロード
			base.OnLoad(e);

            // 画面情報の初期化
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.KENSHU_ICHIRAN != null)
            {
                this.KENSHU_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.SHUKKA_NET_TOTAL_LBL != null)
            {
                this.SHUKKA_NET_TOTAL_LBL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.SHUKKA_NET_TOTAL != null)
            {
                this.SHUKKA_NET_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_NET_TOTAL_LBL != null)
            {
                this.KENSHU_NET_TOTAL_LBL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_NET_TOTAL != null)
            {
                this.KENSHU_NET_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.NET_SABUN_LBL != null)
            {
                this.NET_SABUN_LBL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.NET_SABUN != null)
            {
                this.NET_SABUN.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_KINGAKU_TOTAL_LBL != null)
            {
                this.KENSHU_KINGAKU_TOTAL_LBL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
            if (this.KENSHU_KINGAKU_TOTAL != null)
            {
                this.KENSHU_KINGAKU_TOTAL.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            }
        }

		/// <summary>
		/// 売上消費税率変更時
		/// </summary>
		private void KENSHU_URIAGE_SHOUHIZEI_RATE_TextChanged(object sender, EventArgs e)
		{
			this.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = this.logic.percentForShouhizeiRate(this.KENSHU_URIAGE_SHOUHIZEI_RATE.Text);
		}

		/// <summary>
		/// 支払消費税率変更時
		/// </summary>
		private void KENSHU_SHIHARAI_SHOUHIZEI_RATE_TextChanged(object sender, EventArgs e)
		{
			this.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = this.logic.percentForShouhizeiRate(this.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text);

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

            // 単価と金額の活性/非活性制御
            this.logic.SetDetailReadOnly();
            
            // 数量の活性/非活性制御
            this.logic.SetDetailSuuryouReadonly();
        }

        private void KENSHU_ICHIRAN_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch (this.KENSHU_ICHIRAN.Columns[e.ColumnIndex].Name)
            { 
                case ConstClass.CELL_HINMEI_CD:
                case ConstClass.CELL_BUBIKI:
                case ConstClass.CELL_KENSHU_NET:
                case ConstClass.CELL_SUURYOU:
                case ConstClass.CELL_UNIT_CD:
                case ConstClass.CELL_TANKA:
                case ConstClass.CELL_SHOW_KINGAKU:
                    this.KENSHU_ICHIRAN.ImeMode = ImeMode.Disable;
                    break;
                case ConstClass.CELL_HINMEI_NAME:
                    this.KENSHU_ICHIRAN.ImeMode = ImeMode.Hiragana;
                    break;
                default:
                    this.KENSHU_ICHIRAN.ImeMode = ImeMode.NoControl;
                    break;
            }
        }

        // 20160125 chenzz #13337 品名手入力に関する機能修正 start
        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal string hinmeiCd = "";
        internal string hinmeiName = "";
        public void HINMEI_CD_PopupBeforeExecuteMethod()
        {
            this.hinmeiCd = Convert.ToString(this.KENSHU_ICHIRAN.CurrentRow.Cells[ConstClass.CELL_HINMEI_CD].Value);
            this.hinmeiName = Convert.ToString(this.KENSHU_ICHIRAN.CurrentRow.Cells[ConstClass.CELL_HINMEI_NAME].Value);
        }
        public void HINMEI_CD_PopupAfterExecuteMethod()
        {
            if (this.hinmeiCd == Convert.ToString(this.KENSHU_ICHIRAN.CurrentRow.Cells[ConstClass.CELL_HINMEI_CD].Value) && this.hinmeiName == Convert.ToString(this.KENSHU_ICHIRAN.CurrentRow.Cells[ConstClass.CELL_HINMEI_CD].Value))
            {
                return;
            }
            this.logic.hinmeiPop(this.KENSHU_ICHIRAN.CurrentCell.RowIndex);
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end
        // 20160125 chenzz #13337 品名手入力に関する機能修正 end

        //20211230 Thanh 158916 s
        /// <summary>
        /// UIForm_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.KENSHU_ICHIRAN.CurrentCell != null && this.KENSHU_ICHIRAN.Columns[this.KENSHU_ICHIRAN.CurrentCell.ColumnIndex].Name == "TANKA")
            {
                if (this.KENSHU_ICHIRAN.CurrentCell.IsInEditMode)
                {
                    if (e.KeyChar == (Char)Keys.Space)
                    {
                        this.OpenTankaRireki(this.KENSHU_ICHIRAN.CurrentRow.Index);
                    }
                }
            }
        }

        /// <summary>
        /// OpenTankaRireki
        /// </summary>
        private void OpenTankaRireki(int index)
        {
            string kyotenCd = string.Empty;
            string torihikisakiCd = string.Empty;
            string gyoushaCd = string.Empty;
            string genbaCd = string.Empty;
            string unpanGyoushaCd = string.Empty;
            string nizumiGyoushaCd = string.Empty;
            string nizumiGenbaCd = string.Empty;
            string nioroshiGyoushaCd = string.Empty;
            string nioroshiGenbaCd = string.Empty;
            string HinmeiCd = Convert.ToString(this.KENSHU_ICHIRAN.Rows[index].Cells["HINMEI_CD"].Value);
            string UnitCd = Convert.ToString(this.KENSHU_ICHIRAN.Rows[index].Cells["UNIT_CD"].Value);

            if (!this.returnDto.shukkaEntryEntity.KYOTEN_CD.IsNull)
            {
                kyotenCd = this.returnDto.shukkaEntryEntity.KYOTEN_CD.Value.ToString().PadLeft(2, '0');
            }
            if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                torihikisakiCd = this.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                gyoushaCd = this.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                genbaCd = this.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                unpanGyoushaCd = this.UNPAN_GYOUSHA_CD.Text;
            }
            nizumiGyoushaCd = Convert.ToString(this.returnDto.shukkaEntryEntity.NIZUMI_GYOUSHA_CD);

            nizumiGenbaCd = Convert.ToString(this.returnDto.shukkaEntryEntity.NIZUMI_GENBA_CD);

            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G157",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, HinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
            if (tankaForm.dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tankaForm.returnTanka.IsNull)
                {
                    this.KENSHU_ICHIRAN.EditingControl.Text = string.Empty;
                }
                else
                {
                    this.KENSHU_ICHIRAN.EditingControl.Text = tankaForm.returnTanka.Value.ToString(this.logic.sysInfoEntity.SYS_TANKA_FORMAT);
                }

                if (!UnitCd.Equals(tankaForm.returnUnitCd))
                {
                    if (string.IsNullOrEmpty(tankaForm.returnUnitCd))
                    {
                        this.KENSHU_ICHIRAN.Rows[index].Cells["UNIT_CD"].Value = string.Empty;
                        this.KENSHU_ICHIRAN.Rows[index].Cells["UNIT_NAME"].Value = string.Empty;
                    }
                    else
                    {
                        var unitEntity = this.logic.unitDao.GetDataByCd(Convert.ToInt16(tankaForm.returnUnitCd));
                        if (unitEntity != null)
                        {
                            this.KENSHU_ICHIRAN.Rows[index].Cells["UNIT_CD"].Value = unitEntity.UNIT_CD.ToString();
                            this.KENSHU_ICHIRAN.Rows[index].Cells["UNIT_NAME"].Value = unitEntity.UNIT_NAME_RYAKU.ToString();
                        }
                    }
                }
            }
        }
        //20211230 Thanh 158916 e
    }
}
