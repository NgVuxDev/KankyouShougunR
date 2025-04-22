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

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass logic;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="kanriId"></param>
        /// <param name="isLastSbnEndrepFlg"></param>
        /// <param name="isRelationalMixMani"></param>
        public UIForm(long systemId, string kanriId, bool isLastSbnEndrepFlg, bool isRelationalMixMani)
            : base(WINDOW_ID.T_KONGOU_HAIKIBUTSU_FURIWAKE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.logic.SystemId = systemId;
            this.logic.KanriId = kanriId;
            this.logic.IsLastSbnEndrepFlg = isLastSbnEndrepFlg;
            this.logic.isRelationalMixMani = isRelationalMixMani;
        }
        #endregion

        #region OnLoad
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.logic.WindowInit())
            {
                return;
            }

            if (this.logic.Search() == -1)
            {
                return;
            }

            if (!this.logic.ChangeReadOnlyForOnLoad())
            {
                return;
            }

            if (this.logic.IsApplying())
            {
                return;
            }

        }
        #endregion

        #region イベント

        #region FunctionButtonイベント
        /// <summary>
        /// [F9]登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Regist(object sender, EventArgs e)
        {
            this.logic.Regist(base.RegistErrorFlag);
        }

        /// <summary>
        /// [F10]行挿入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddRow(object sender, EventArgs e)
        {
            //bool catchErr = false;
            //bool retCheck = this.logic.IsPossibleAddNewRow(out catchErr);
            //if (catchErr)
            //{
            //    return;
            //}

            //if (!retCheck)
            //{
            //    return;
            //}

            var row = this.haikiButsuDetail.CurrentRow;
            if (row == null || row.Index < 0)
            {
                this.haikiButsuDetail.Rows.Add();
            }
            else
            {
                int rowIndex = row.Index;
                this.haikiButsuDetail.Rows.Insert(row.Index);
                this.haikiButsuDetail.CurrentCell = this.haikiButsuDetail.Rows[rowIndex].Cells[0];
            }
        }

        /// <summary>
        /// [F11]行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteRow(object sender, EventArgs e)
        {
            var row = this.haikiButsuDetail.CurrentRow;

            if (row == null || row.Index < 0 || row.IsNewRow)
            {
                return;
            }
            else
            {
                if (this.logic.IsPossibleDeleteRow(row.Index))
                {
                    this.haikiButsuDetail.Rows.RemoveAt(row.Index);
                }
            }
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            this.logic.FormClose();
        }
        #endregion

        #region CellEnterイベント
        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void haikiButsuDetail_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            /**
             * IMEModeの設定
             */
            string[] disableTarget = new string[]
                                        {
                                            this.logic.CELL_NAME_HAIKI_SHURUI_CD,
                                            this.logic.CELL_NAME_SBN_HOUHOU_CD,
                                            this.logic.CELL_NAME_HAIKI_SUU,
                                            this.logic.CELL_NAME_HAIKI_UNIT_CD,
                                            this.logic.CELL_NAME_HAIKI_NAME_CD,
                                            this.logic.CELL_NAME_LAST_SBN_END_DATE,
                                            this.logic.CELL_NAME_SBN_ENDREP_KBN,
                                            this.logic.CELL_NAME_KANSAN_SUU
                                        };

            // 数値、英字のみ
            if (disableTarget.Contains(this.haikiButsuDetail.Columns[e.ColumnIndex].Name))
            {
                this.haikiButsuDetail.ImeMode = ImeMode.Disable;
            }
            else
            {
                this.haikiButsuDetail.ImeMode = ImeMode.NoControl;
            }

            this.logic.SetBeforeValue(e.RowIndex, e.ColumnIndex);
        }
        #endregion

        #region CellValidatingイベント
        /// <summary>
        /// CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void haikiButsuDetail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool catchErr = false;
            bool retCheck = this.logic.CellsValidating(e.RowIndex, e.ColumnIndex, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (!retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region CellValidatedイベント
        /// <summary>
        /// CellValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void haikiButsuDetail_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.CellsLeave(e.RowIndex, e.ColumnIndex);
        }
        #endregion

        #region RowsAddedイベント
        /// <summary>
        /// 行挿入時の規定値を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void haikiButsuDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // 最終処分日のRaodOnlyは動的に変更しており、
            // あるタイミングで規定値が「ReadOnly = false」になってしまうため、
            // このイベントで制御
            this.haikiButsuDetail.Rows[e.RowIndex].Cells[this.logic.CELL_NAME_LAST_SBN_END_DATE].ReadOnly = true;
        }
        #endregion
        #endregion

        private string oldValue = string.Empty;
        /// <summary>
        /// 混合種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_KongoCd_Validated(object sender, EventArgs e)
        {
            if (this.cantxt_KongoCd.Text.Equals(oldValue))//変更がない場合は何もしない
            {
                return;
            }

            if (!this.logic.SetKongouName()) { return; }
            if (!this.logic.SetSuu()) { return; }
            this.logic.SetJissekiTani();

            return;
        }

        internal bool KongoErr = false;
        private void cantxt_KongoCd_Enter(object sender, EventArgs e)
        {
            if (!KongoErr)
            {
                oldValue = this.cantxt_KongoCd.Text;
            }
        }
    }
}
