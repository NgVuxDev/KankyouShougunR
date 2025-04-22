using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;
using Seasar.Framework.Exceptions;
using Shougun.Core.SalesPayment.TankaRirekiIchiran;

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    public partial class G161Form : SuperForm
    {

        /// <summary>
        /// G161画面ロジック
        /// </summary>
        private G161Logic Logic;
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 前回値チェック用変数(header用)
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        private List<Dictionary<string, string>> beforeValuesForDetail = new List<Dictionary<string, string>>();

        internal GrapeCity.Win.MultiRow.Cell errorCell;

        /// <summary>
        /// 伝票発行ポップアップ用DTO
        /// </summary>
        public ParameterDTOClass denpyouHakouPopUpDTO = new ParameterDTOClass();

        private string strCellOldValue = string.Empty;

        /// <summary>
        /// 運賃入力実行フラッグ
        /// </summary>
        private bool IsExacuteUnchin;

        private string tmpDenpyouDate = string.Empty;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public G161Form()
            : base(WINDOW_ID.T_DAINO)
        {
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.Logic = new G161Logic(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// コンストラクタで渡された代納番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistDainoData(long DainouNumber, out bool catchErr)
        {
            catchErr = true;
            try
            {
                return this.Logic.IsExistUkeireData(DainouNumber);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistDainoData", ex1);
                this.Logic.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistDainoData", ex);
                this.Logic.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return true;
        }

        #region コンストラクタ(新規修正削除用)
        /// <summary>
        /// コンストラクタ(新規修正削除用)
        /// </summary>
        public G161Form(WINDOW_TYPE windowType, long DainouNumber)
            : base(WINDOW_ID.T_DAINO, windowType)
        {
            try
            {

                InitializeComponent();

                this.Logic = new G161Logic(this);
                //モード
                if (null != windowType)
                {
                    this.WindowType = windowType;

                }
                //伝票番号
                if (null != DainouNumber)
                {
                    this.Logic.PrmDainouNumber = DainouNumber;
                }

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                // 完全に固定。ここには変更を入れない
                //   QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region 画面ロード
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                ParentBaseForm = (BusinessBaseForm)this.Parent;
                base.OnLoad(e);

                //PhuocLoc 2020/05/20 #137147 -Start
                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                //PhuocLoc 2020/05/20 #137147 -End

                // 画面情報の初期化
                this.Logic.WindowInit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            try
            {
	            // この画面を最大化したくない場合は下記のように
	            // OnShownでWindowStateをNomalに指定する
	            //this.ParentForm.WindowState = FormWindowState.Normal;

                base.OnShown(e);

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm.headerForm));
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm));

            return allControl.ToArray();
        }
        #endregion

        #region 品名CDにより品名情報を取得

        /// <summary>
        /// 品名CDにより品名情報を取得
        /// <param name="rowIndex"></param>
        /// <param name="hinmeiName"></param>
        /// <param name="hinmeiCd"></param>
        /// <param name="flg"></param>
        /// </summary>
        public bool HinmeiFORHinmeiCd_Select(int rowIndex, string hinmeiName, string hinmeiCd, int flg)
        {
            LogUtility.DebugMethodStart(rowIndex, hinmeiName, hinmeiCd, flg);
            try
            {
                // 品名CD 
                if (!string.IsNullOrEmpty(hinmeiCd))
                {
                    // ゼロ埋め
                    hinmeiCd = hinmeiCd.PadLeft(6, '0');
                }

                if (!string.IsNullOrEmpty(hinmeiCd))
                {

                    // 品名マスタ検索
                    if (this.Logic.HinmeiSearch(hinmeiCd, flg) == 0)
                    {
                        // 品名関連項目初期化
                        if (this.Logic.HinmeiInit(rowIndex, flg))
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E020", "品名");
                        }
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HinmeiFORHinmeiCd_Select", ex1);
                this.Logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HinmeiFORHinmeiCd_Select", ex);
                this.Logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        /// <summary>
        /// 単位CDにより単位情報を取得
        /// <param name="rowIndex"></param>
        /// <param name="cellName"></param>
        /// <param name="unitCd"></param>
        /// <param name="flg"></param>
        /// </summary>
        public bool UnitCheck(int rowIndex, string cellName, string unitCd, int flg)
        {
            LogUtility.DebugMethodStart(rowIndex, cellName, unitCd, flg);
            try
            {
                // 単位CD 
                if (!string.IsNullOrEmpty(unitCd))
                {
                    // ゼロ埋め
                    unitCd = unitCd.PadLeft(3, '0');
                }

                if (!string.IsNullOrEmpty(unitCd))
                {

                    // 単位マスタ検索
                    var unitInfo = this.Logic.accessor.GetUnit(short.Parse(unitCd));
                    if (unitInfo == null || unitInfo.Length <= 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "単位");

                        // フォーカス
                        this.Ichiran.CurrentCell = this.Ichiran.Rows[rowIndex].Cells[cellName];

                        // 単位関連項目初期化
                        this.Logic.UnitInit(rowIndex, flg);

                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }
                    else
                    {
                        // 単位名
                        if (!string.IsNullOrEmpty(unitInfo[0].UNIT_NAME_RYAKU))
                        {
                            // 表示
                            if (flg == 1)
                            {
                                this.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_UKEIRE_UNIT_NAME].Value = unitInfo[0].UNIT_NAME_RYAKU;
                            }
                            else if (flg == 2)
                            {
                                this.Ichiran.Rows[rowIndex].Cells[ConstClass.CONTROL_SHUKKA_UNIT_NAME].Value = unitInfo[0].UNIT_NAME_RYAKU;
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UnitCheck", ex1);
                this.Logic.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UnitCheck", ex);
                this.Logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        #endregion

        #region 各CELLの更新処理
        /// <summary>
        /// 各CELLの更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                int flg = 0; //0:ブランク,1:受入,2：支払
                // 行、列位置
                int rowIndex = e.RowIndex;
                int cellIndex = e.CellIndex;
                //セール名、値
                String cellname = this.Ichiran.Columns[e.CellIndex].Name;
                String cellvalue = Convert.ToString(e.FormattedValue);

                //変更前の値
                string beforeCellValue = string.Empty;
                if (null != this.Logic.BeforeDetailResultList && this.Logic.BeforeDetailResultList.Count > 0 && rowIndex < this.Logic.BeforeDetailResultList.Count)
                {
                    var obj = this.Logic.BeforeDetailResultList[rowIndex];
                    if (null != obj.GetType().GetProperty(cellname).GetValue(obj, null))
                    {
                        beforeCellValue = obj.GetType().GetProperty(cellname).GetValue(obj, null).ToString();
                    }
                }

                if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_HINMEI_CD) || e.CellName.Equals(ConstClass.CONTROL_SHUKKA_HINMEI_CD))
                {
                    // 品名設定
                    if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_HINMEI_CD)) flg = 1;
                    if (e.CellName.Equals(ConstClass.CONTROL_SHUKKA_HINMEI_CD)) flg = 2;

                    if (!beforeCellValue.Equals(cellvalue))
                    {
                        if (!string.IsNullOrEmpty(cellvalue))
                        {
                            if (this.HinmeiFORHinmeiCd_Select(e.RowIndex, cellname, cellvalue, flg))
                            {
                                this.errorCell = this.Ichiran.Rows[e.RowIndex].Cells[cellname];
                                this.Logic.HinmeiInit(rowIndex, flg);
                                e.Cancel = true;
                                return;
                            }
                        }
                        else
                        {
                            // 品名関連項目初期化
                            if (!this.Logic.HinmeiInit(rowIndex, flg))
                            {
                                return;
                            }
                        }
                    }
                }

                if (cellname.Equals(ConstClass.CONTROL_UKEIRE_UNIT_CD) || cellname.Equals(ConstClass.CONTROL_SHUKKA_UNIT_CD))
                {
                    // 単位設定
                    if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_UNIT_CD)) flg = 1;
                    if (e.CellName.Equals(ConstClass.CONTROL_SHUKKA_UNIT_CD)) flg = 2;
                    if (cellvalue == "")
                    {
                        //this.Logic.BeforeIchiranChengeValuesで単位がブランクの時は-1にしているため、ブランクなら-1で判断する必要がある
                        cellvalue = "-1";
                    }
                    if (!beforeCellValue.Equals(cellvalue))
                    {
                        if (cellvalue != "-1")
                        {
                            if (this.UnitCheck(rowIndex, cellname, cellvalue, flg))
                            {
                                this.errorCell = this.Ichiran.Rows[e.RowIndex].Cells[cellname];
                                this.Logic.UnitInit(e.RowIndex, flg);
                                e.Cancel = true;
                                return;
                            }
                        }
                        else
                        {
                            // 初期化
                            if (flg == 1)
                            {
                                // 単価が読取専用→金額のみ入力されている。この場合は金額は再計算しないので放置。
                                // 単価が編集可能→単価×数量で計上されるため、リセットするなら金額もリセット。
                                if (!this.Ichiran[e.RowIndex, ConstClass.CONTROL_UKEIRE_TANKA].ReadOnly)
                                {
                                    this.Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTROL_UKEIRE_TANKA].Value = DBNull.Value;
                                    this.Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTROL_UKEIRE_KINGAKU].Value = DBNull.Value;
                                }

                            }
                            else
                            {
                                if (!this.Ichiran[e.RowIndex, ConstClass.CONTROL_SHUKKA_TANKA].ReadOnly)
                                {
                                    this.Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTROL_SHUKKA_TANKA].Value = DBNull.Value;
                                    this.Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTROL_SHUKKA_KINGAKU].Value = DBNull.Value;
                                }
                            }

                            if (!this.Logic.UnitInit(e.RowIndex, flg))
                            {
                                return;
                            }
                        }
                    }
                }

                if (cellname.Equals(ConstClass.CONTROL_UKEIRE_TANKA) || cellname.Equals(ConstClass.CONTROL_SHUKKA_TANKA))
                {
                        string tmpCellName = "";

                        if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_TANKA)) tmpCellName = ConstClass.CONTROL_UKEIRE_TANKA;
                        if (e.CellName.Equals(ConstClass.CONTROL_SHUKKA_TANKA)) tmpCellName = ConstClass.CONTROL_SHUKKA_TANKA;

                        if (!this.Ichiran[e.RowIndex, tmpCellName].ReadOnly)
                        {
                            if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_TANKA)) tmpCellName = ConstClass.CONTROL_UKEIRE_KINGAKU;
                            if (e.CellName.Equals(ConstClass.CONTROL_SHUKKA_TANKA)) tmpCellName = ConstClass.CONTROL_SHUKKA_KINGAKU;

                            if (cellvalue == "")
                            {
                                if (!beforeCellValue.Equals(cellvalue))
                                {
                                    //単価を有る状態から消したときは金額も先に消す
                                    this.Ichiran.Rows[e.RowIndex].Cells[tmpCellName].Value = DBNull.Value;
                                }
                        }
                    }
                }


                // 単価と金額の活性/非活性制御
                if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_TANKA))
                {
                    if (!this.Ichiran[e.RowIndex, ConstClass.CONTROL_UKEIRE_TANKA].ReadOnly)
                    {
                        // 単価の場合のみCellValidatedでReadOnly設定が変わる場合があるのでここで一旦計算を行う
                        this.Logic.CalcDetailKingaku(rowIndex, 1);
                        this.Logic.CalcAllDetailAndTotal();
                    }
                }
                else if (e.CellName.Equals(ConstClass.CONTROL_SHUKKA_TANKA))
                {
                    if (!this.Ichiran[e.RowIndex, ConstClass.CONTROL_SHUKKA_TANKA].ReadOnly)
                    {
                        // 単価の場合のみCellValidatedでReadOnly設定が変わる場合があるのでここで一旦計算を行う
                        this.Logic.CalcDetailKingaku(rowIndex, 2);
                        this.Logic.CalcAllDetailAndTotal();
                    }
                }

                // 単価が入力された状態で数量をブランクにした時に金額をブランクにする。
                if (cellname.Equals(ConstClass.CONTROL_UKEIRE_SUURYOU) || cellname.Equals(ConstClass.CONTROL_SHUKKA_SUURYOU))
                {
                    string tmpCellName = "";

                    if (e.CellName.Equals(ConstClass.CONTROL_UKEIRE_SUURYOU)) tmpCellName = ConstClass.CONTROL_UKEIRE_KINGAKU;
                    if (e.CellName.Equals(ConstClass.CONTROL_SHUKKA_SUURYOU)) tmpCellName = ConstClass.CONTROL_SHUKKA_KINGAKU;

                    if (this.Ichiran[e.RowIndex, tmpCellName].ReadOnly)
                    {
                        if (cellvalue == "")
                        {
                            if (!beforeCellValue.Equals(cellvalue))
                            {
                                //単価を有る状態から消したときは金額も先に消す
                                this.Ichiran.Rows[e.RowIndex].Cells[tmpCellName].Value = DBNull.Value;
                            }
                        }
                    }
                }

                this.SetIchiranReadOnly(rowIndex);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 各CELLの更新後処理
        /// <summary>
        /// 各CELLの更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (e.RowIndex == this.Ichiran.RowCount - this.Logic.newRowNum) { return; }

                //フォーマット用
                string name = string.Empty;

                // 行、列位置
                int rowIndex = e.RowIndex;
                int cellIndex = e.CellIndex;
                //セール名、値
                String cellname = this.Ichiran.Columns[e.CellIndex].Name;
                String cellvalue = Convert.ToString(this.Ichiran[e.RowIndex, cellname].Value);

                // セルの状態が読み取り専用の場合、更新後処理は行わない
                if (this.Ichiran[e.RowIndex, cellname].ReadOnly) { return; }

                //変更前の値
                string beforeCellValue = string.Empty;
                if (null != this.Logic.BeforeDetailResultList && this.Logic.BeforeDetailResultList.Count > 0 && rowIndex < this.Logic.BeforeDetailResultList.Count)
                {
                    var obj = this.Logic.BeforeDetailResultList[rowIndex];
                    if (null != obj.GetType().GetProperty(cellname).GetValue(obj, null))
                    {
                        beforeCellValue = obj.GetType().GetProperty(cellname).GetValue(obj, null).ToString();
                    }
                }

                bool bExecuteKingakuCalc = false;
                bool bExecuteJyuuryouCalc = false;
                bool bExecuteTotalKingakuCalc = false;
                bool bExecuteHinmeiSet = false;
                bool bExecuteTankaSet = false;
                int flg = 0; //0:ブランク,1:受入,2：支払

                switch (cellname)
                {
                    #region 受入
                    // 品名CD
                    case ConstClass.CONTROL_UKEIRE_HINMEI_CD:
                        flg = 1;
                        if (!beforeCellValue.Equals(cellvalue))
                        {
                            if (!string.IsNullOrEmpty(cellvalue))
                            {
                                if (!this.HinmeiFORHinmeiCd_Select(e.RowIndex, cellname, cellvalue, flg))
                                {
                                    bExecuteHinmeiSet = true;
                                    bExecuteTotalKingakuCalc = true;
                                }
                                else
                                {
                                    this.Ichiran.Focus();
                                    this.errorCell = this.Ichiran.Rows[e.RowIndex].Cells[cellname];
                                    return;
                                }
                                // 変更した値を一時保存
                                if (!this.Logic.BeforeIchiranChengeValues(2, rowIndex, flg))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                // 品名関連項目初期化
                                if (!this.Logic.HinmeiInit(rowIndex, flg))
                                {
                                    return;
                                }

                                // 合計系の計算
                                if (!this.Logic.CalcAllDetailAndTotal())
                                {
                                    return;
                                }
                            }
                        }

                        break;
                    // 正味
                    case ConstClass.CONTROL_UKEIRE_STACK_JYUURYOU:
                        flg = 1;
                        bExecuteJyuuryouCalc = true;

                        break;
                    // 調整
                    case ConstClass.CONTROL_UKEIRE_CHOUSEI_JYUURYOU:
                        flg = 1;
                        bExecuteJyuuryouCalc = true;
                        break;
                    // 単位CD
                    case ConstClass.CONTROL_UKEIRE_UNIT_CD:
                        // 単位設定
                        flg = 1;

                        if (cellvalue == "")
                        {
                            //this.Logic.BeforeIchiranChengeValuesで単位がブランクの時は-1にしているため、ブランクなら-1で判断する必要がある
                            cellvalue = "-1";
                        }
                        if (!beforeCellValue.Equals(cellvalue))
                        {
                            if (cellvalue != "-1")
                            {
                                if (!this.UnitCheck(e.RowIndex, cellname, cellvalue, flg))
                                {
                                    bExecuteTankaSet = true;
                                    bExecuteTotalKingakuCalc = true;
                                }
                                else
                                {
                                    this.Ichiran.Focus();
                                    this.errorCell = this.Ichiran.Rows[e.RowIndex].Cells[cellname];
                                    return;
                                }
                                // 変更した値を一時保存
                                if (!this.Logic.BeforeIchiranChengeValues(2, rowIndex, flg))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                if (!this.Logic.UnitInit(e.RowIndex, flg))
                                {
                                    return;
                                }
                            }
                        }

                        break;
                    // 数量
                    case ConstClass.CONTROL_UKEIRE_SUURYOU:
                        flg = 1;
                        bExecuteKingakuCalc = true;
                        bExecuteTotalKingakuCalc = true;
                        break;
                    // 単価
                    case ConstClass.CONTROL_UKEIRE_TANKA:
                        flg = 1;
                        //変更した値を一時保存
                        if (!this.Logic.BeforeIchiranChengeValues(2, rowIndex, flg))
                        {
                            return;
                        }
                        bExecuteKingakuCalc = true;
                        bExecuteTotalKingakuCalc = true;
                        break;
                    // 金額
                    case ConstClass.CONTROL_UKEIRE_KINGAKU:
                        flg = 1;
                        bExecuteTotalKingakuCalc = true;
                        break;
                    #endregion

                    #region 出荷
                    // 品名
                    case ConstClass.CONTROL_SHUKKA_HINMEI_CD:
                        flg = 2;
                        if (!beforeCellValue.Equals(cellvalue))
                        {
                            if (!string.IsNullOrEmpty(cellvalue))
                            {
                                if (!this.HinmeiFORHinmeiCd_Select(e.RowIndex, cellname, cellvalue, flg))
                                {
                                    bExecuteHinmeiSet = true;
                                    bExecuteTotalKingakuCalc = true;
                                }
                                else
                                {
                                    this.Ichiran.Focus();
                                    this.errorCell = this.Ichiran.Rows[e.RowIndex].Cells[cellname];
                                    return;
                                }
                                //変更した値を一時保存
                                if (!this.Logic.BeforeIchiranChengeValues(2, rowIndex, flg))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                // 品名関連項目初期化
                                if (!this.Logic.HinmeiInit(rowIndex, flg))
                                {
                                    return;
                                }

                                // 合計系の計算
                                if (!this.Logic.CalcAllDetailAndTotal())
                                {
                                    return;
                                }
                            }
                        }

                        break;
                    // 正味
                    case ConstClass.CONTROL_SHUKKA_STACK_JYUURYOU:
                        flg = 2;
                        bExecuteJyuuryouCalc = true;
                        break;
                    // 調整
                    case ConstClass.CONTROL_SHUKKA_CHOUSEI_JYUURYOU:
                        flg = 2;
                        bExecuteJyuuryouCalc = true;
                        break;
                    // 単位
                    case ConstClass.CONTROL_SHUKKA_UNIT_CD:
                        // 単位設定
                        flg = 2;

                        if (cellvalue == "")
                        {
                            //this.Logic.BeforeIchiranChengeValuesで単位がブランクの時は-1にしているため、ブランクなら-1で判断する必要がある
                            cellvalue = "-1";
                        }
                        if (!beforeCellValue.Equals(cellvalue))
                        {
                            if (cellvalue != "-1")
                            {
                                if (!this.UnitCheck(e.RowIndex, cellname, cellvalue, flg))
                                {
                                    bExecuteTankaSet = true;
                                    bExecuteTotalKingakuCalc = true;
                                }
                                else
                                {
                                    this.Ichiran.Focus();
                                    this.errorCell = this.Ichiran.Rows[e.RowIndex].Cells[cellname];
                                    return;
                                }
                                // 変更した値を一時保存
                                if (!this.Logic.BeforeIchiranChengeValues(2, rowIndex, flg))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                if (!this.Logic.UnitInit(e.RowIndex, flg))
                                {
                                    return;
                                }
                            }
                        }

                        break;
                    // 数量
                    case ConstClass.CONTROL_SHUKKA_SUURYOU:
                        flg = 2;
                        bExecuteKingakuCalc = true;
                        bExecuteTotalKingakuCalc = true;
                        break;
                    // 単価
                    case ConstClass.CONTROL_SHUKKA_TANKA:
                        flg = 2;
                        //変更した値を一時保存
                        if (!this.Logic.BeforeIchiranChengeValues(2, rowIndex, flg))
                        {
                            return;
                        }
                        bExecuteKingakuCalc = true;
                        bExecuteTotalKingakuCalc = true;
                        break;
                    // 金額
                    case ConstClass.CONTROL_SHUKKA_KINGAKU:
                        flg = 2;
                        bExecuteTotalKingakuCalc = true;
                        break;

                    #endregion

                    default:

                        break;
                }

                if (strCellOldValue.Equals(cellvalue))
                {
                    bExecuteKingakuCalc = false;
                    bExecuteJyuuryouCalc = false;
                    bExecuteTotalKingakuCalc = false;
                    bExecuteHinmeiSet = false;
                    bExecuteTankaSet = false;
                }
                // 再計算対象の項目だった場合、実正味の再計算を行う
                if (bExecuteJyuuryouCalc)
                {
                    if (!this.Logic.CalcDetailNetJyuuryou(e.RowIndex, flg))
                    {
                        return;
                    }
                }
                // 品名CD変更あり場合、品名関連項目設定を行う
                // 単位CD変更あり場合、単位関連項目設定を行う
                if (bExecuteHinmeiSet || bExecuteTankaSet)
                {
                    // 品名設定
                    if (bExecuteHinmeiSet)
                    {
                        if (!this.Logic.hinmeiSet(rowIndex, flg))
                        {
                            return;
                        }
                    }
                    // 単価設定
                    if (!this.Logic.CalcTanka(rowIndex, flg))
                    {
                        this.Logic.ResetTankaCheck(); // MAILAN #158994 START
                        return;
                    }
                    this.Logic.ResetTankaCheck(); // MAILAN #158994 START
                    // 数量制御
                    if (!this.Logic.SetHinmeiSuuryou(rowIndex, flg))
                    {
                        return;
                    }
                    // 数量計算
                    if (!this.Logic.CalcSuuryou(rowIndex, flg))
                    {
                        return;
                    }
                    // 金額計算
                    if (!this.Logic.CalcDetailKingaku(rowIndex, flg))
                    {
                        return;
                    }
                }
                // 数量、または単価変更有場合、金額の再計算を行う
                if (bExecuteKingakuCalc)
                {
                    if (!this.Logic.CalcDetailKingaku(rowIndex, flg))
                    {
                        return;
                    }
                }
                // 再計算対象の項目だった場合、金額合計の再計算を行う
                if (bExecuteTotalKingakuCalc)
                {
                    // 合計系の計算
                    this.Logic.CalcAllDetailAndTotal();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F2新規モード
        /// <summary>
        /// F2新規モード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NewMode(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E158", "新規");
                    return;
                }

                //モードを新規に変更
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                // 伝票番号は空にする
                this.Logic.PrmDainouNumber = 0;

                // ヘッダー部クリア
                this.Logic.HeaderClear();
                // 明細クリア
                this.Logic.MeisaiClear();

                // 初期表示
                if (!this.Logic.DisplayInit())
                {
                    return;
                }

                // フォーカス設定
                this.DAINOU_NUMBER.Focus();
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region F3修正モード
        /// <summary>
        /// F3修正モード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ModifyMode(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (!string.IsNullOrEmpty(this.DAINOU_NUMBER.Text))
                {
                    //伝票番号
                    this.Logic.PrmDainouNumber = Convert.ToInt32(this.DAINOU_NUMBER.Text);
                    // 修正前に該当伝票番号存在チェック
                    int count = this.Logic.GetDainoNumberExists();
                    if (count < 0)
                    {
                        return;
                    }
                    if (count == 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E067");
                        return;
                    }
                    //モード
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    //base.OnLoad(e);

                    //初期表示
                    this.Logic.DisplayInit();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();


        }
        #endregion

        #region F7一覧画面へ遷移
        /// <summary>
        /// F7一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void IchiranHyouji(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //伝種CD (170)
                int denshuCd = 170;
                //社員CD (ログイン情報より）
                string shainCd = SystemProperty.Shain.CD;
                // 伝票一覧画面へ遷移する
                FormManager.OpenFormWithAuth("G055", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denshuCd, shainCd);
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F9登録処理
        /// <summary>
        /// F9登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 遷移フラグ
            bool okFlg = false;

            // 初期化
            base.RegistErrorFlag = false;

            this.IsExacuteUnchin = false;

            // 登録前チェック
            if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) ||
                this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            }

            /* 月次処理中 or 月次処理ロックチェック */
            if (!base.RegistErrorFlag)
            {
                if (this.GetsujiLockCheck())
                {
                    base.RegistErrorFlag = true;
                }
            }

            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        if (base.RegistErrorFlag)
                        {
                            return;
                        }

                        if (!this.Logic.CheckRequiredDataForDeital())
                        {
                            return;
                        }

                        // 取引先と拠点コードの関連チェック
                        //if (!this.Logic.CheckTorihikisakiAndKyotenCd(null, this.UKEIRE_TORIHIKISAKI_CD))
                        //{
                        //    return;
                        //}
                        //if (!this.Logic.CheckTorihikisakiAndKyotenCd(null, this.SHUKKA_TORIHIKISAKI_CD))
                        //{
                        //    return;
                        //}

                        if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                        {
                            var upResult = msgLogic.MessageBoxShow("C038");
                            if (upResult == DialogResult.No)
                            {
                                return;
                            }
                        }

                        if (this.ShowDenpyouHakouPopup())
                        {
                            //締済チェック start
                            if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                            {
                                this.Logic.CheckAllShimeStatus();
                                if (this.Logic.ukeireShimeiCheckFlg || this.Logic.shukkaShimeiCheckFlg)
                                {
                                    // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                    msgLogic.MessageBoxShow("I011", "修正");
                                    return;
                                }
                            }

                            if (!this.Logic.SeikyuuDateCheck())
                            {
                                return;
                            }
                            else if (!this.Logic.SeisanDateCheck())
                            {
                                return;
                            }
                            //締済チェック end

                            // 伝票番号記憶
                            string denpyouNumber = this.DAINOU_NUMBER.Text;
                            if (null == denpyouNumber || string.IsNullOrEmpty(denpyouNumber))
                            {
                                this.Logic.PrmDainouNumber = 0;
                            }
                            else
                            {
                                this.Logic.PrmDainouNumber = Convert.ToInt32(this.DAINOU_NUMBER.Text);
                            }

                            okFlg = this.Logic.UpdRegist();

                            if (okFlg)
                            {
                                // モードを新規に変更
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                ///伝票番号クリア
                                this.Logic.PrmDainouNumber = 0;
                                this.DAINOU_NUMBER.Text = string.Empty;
                                //ヘッダー部クリア
                                this.Logic.HeaderClear();
                                //明細クリア
                                this.Logic.MeisaiClear();
                                //再表示
                                if (!this.Logic.DisplayInit())
                                {
                                    return;
                                }

                                this.IsExacuteUnchin = true;
                            }
                        }

                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:

                        var delResult = msgLogic.MessageBoxShow("C026");
                        if (delResult == DialogResult.No)
                        {
                            return;
                        }

                        //締済チェック start
                        this.Logic.CheckAllShimeStatus();
                        if (this.Logic.ukeireShimeiCheckFlg || this.Logic.shukkaShimeiCheckFlg)
                        {
                            // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                            msgLogic.MessageBoxShow("I011", "削除");
                            return;
                        }
                        //締済チェック end

                        //削除
                        okFlg = this.Logic.UpdRegist();
                        if (okFlg)
                        {
                            // モードを新規に変更
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                            //伝票番号クリア
                            this.Logic.PrmDainouNumber = 0;
                            this.DAINOU_NUMBER.Text = string.Empty;
                            //ヘッダー部クリア
                            this.Logic.HeaderClear();
                            //明細クリア
                            this.Logic.MeisaiClear();
                            //再表示
                            this.Logic.DisplayInit();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票発行ポップアップ表示
        /// </summary>
        /// <returns>true:実行された場合, false:キャンセルされた場合</returns>
        private bool ShowDenpyouHakouPopup()
        {
            bool returnVal = false;
            // TODO: 伝票発行ポップアップ起動
            string denpyouMode = string.Empty;
            // TODO: 伝票モードのConstを定義してもらうよう依頼
            // G507代納伝票発行画面を呼び出しのため
            this.denpyouHakouPopUpDTO = this.SetdenpyouHakouPopUpDTO();

            // 伝票発行をモーダル表示
            var assembly = Assembly.LoadFrom("DainoDenpyoHakkou.dll");

            var callForm = (SuperForm)assembly.CreateInstance(
                    "Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP.UIForm",
                    false,
                    BindingFlags.CreateInstance,
                    null,
                    new object[] { this.denpyouHakouPopUpDTO },
                    null,
                    null
                  );
            var callHeader = (HeaderBaseForm)assembly.CreateInstance(
                    "Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP.UIHeader",
                    false,
                    BindingFlags.CreateInstance,
                    null,
                    null,
                    null,
                    null
            );
            if (callForm.IsDisposed)
            {
                return false;
            }

            var callBaseForm = new BasePopForm(callForm, callHeader);
            var result = callBaseForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                PropertyInfo val = callForm.GetType().GetProperty("ParameterDTO");
                this.denpyouHakouPopUpDTO = (ParameterDTOClass)val.GetValue(callForm, null);
                returnVal = true;
            }
            callBaseForm.Dispose();

            return returnVal;
        }

        /// <summary>
        /// 代納伝票発行画面用パラメータの設定
        /// </summary>
        /// <returns></returns>
        private ParameterDTOClass SetdenpyouHakouPopUpDTO()
        {
            ParameterDTOClass hakouPopUpDto = new ParameterDTOClass();
            hakouPopUpDto.Ukeire_Out_Tenpyo_Cnt = new List<MeiseiDTOClass>();
            hakouPopUpDto.Shukka_Out_Tenpyo_Cnt = new List<MeiseiDTOClass>();
            hakouPopUpDto.WindowType = this.WindowType;
            // ヘッダー情報設定
            hakouPopUpDto.Ukeire_Date = this.SHIHARAI_DATE.Value.ToString();
            hakouPopUpDto.Ukeire_Torihikisaki_Cd = this.UKEIRE_TORIHIKISAKI_CD.Text;
            hakouPopUpDto.Shiharai_Shouhizei_Rate = this.Logic.ukeireTaxRate.ToString();

            // システムID設定
            if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                hakouPopUpDto.Ukeire_SystemId = this.Logic.dainouUkeireEntry.SYSTEM_ID.ToString();
                hakouPopUpDto.Shukka_SystemId = this.Logic.dainouShukkaEntry.SYSTEM_ID.ToString();
            }

            // 税計算区分CD、税区分CD(売上)
            int shiharaiKeisaiZeiKbnCd = 0;
            int shiharaiZeiKbnCd = 0;
            if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                shiharaiKeisaiZeiKbnCd = (int)this.Logic.dainouUkeireEntry.SHIHARAI_ZEI_KEISAN_KBN_CD;
                shiharaiZeiKbnCd = (int)this.Logic.dainouUkeireEntry.SHIHARAI_ZEI_KBN_CD;
            }
            else
            {
                // 取引先_支払情報マスタ
                var torihikisakiShiharai = this.Logic.accessor.GetTorihikisakiShiharai(this.UKEIRE_TORIHIKISAKI_CD.Text);
                if (null != torihikisakiShiharai)
                {
                    // 税計算区分CD
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KEISAN_KBN_CD), out shiharaiKeisaiZeiKbnCd);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KBN_CD), out shiharaiZeiKbnCd);
                }
            }
            hakouPopUpDto.Ukeire_Zeikeisan_Kbn = shiharaiKeisaiZeiKbnCd.ToString();
            hakouPopUpDto.Ukeire_Zei_Kbn = shiharaiZeiKbnCd.ToString();

            hakouPopUpDto.Shukka_Date = this.URIAGE_DATE.Value.ToString();
            hakouPopUpDto.Shukka_Torihikisaki_Cd = this.SHUKKA_TORIHIKISAKI_CD.Text;
            hakouPopUpDto.Uriage_Shouhizei_Rate = this.Logic.shukkaTaxRate.ToString();

            // 税計算区分CD、税区分CD(支払)
            int seikyuuKeisaiZeiKbnCd = 0;
            int seikyuuZeiKbnCd = 0;
            if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                seikyuuKeisaiZeiKbnCd = (int)this.Logic.dainouShukkaEntry.URIAGE_ZEI_KEISAN_KBN_CD;
                seikyuuZeiKbnCd = (int)this.Logic.dainouShukkaEntry.URIAGE_ZEI_KBN_CD;
            }
            else
            {
                // 取引先_請求情報マスタ
                var torihikisakiSeikyuu = this.Logic.accessor.GetTorihikisakiSeikyuu(this.SHUKKA_TORIHIKISAKI_CD.Text);
                if (null != torihikisakiSeikyuu)
                {
                    // 税計算区分CD
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KEISAN_KBN_CD), out seikyuuKeisaiZeiKbnCd);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KBN_CD), out seikyuuZeiKbnCd);
                }
            }
            hakouPopUpDto.Shukka_Zeikeisan_Kbn = seikyuuKeisaiZeiKbnCd.ToString();
            hakouPopUpDto.Shukka_Zei_Kbn = seikyuuZeiKbnCd.ToString();

            // 明細情報設定
            for (int i = 0; i < this.Ichiran.Rows.Count - this.Logic.newRowNum; i++)
            {
                //行
                GrapeCity.Win.MultiRow.Row mRow = this.Ichiran.Rows[i];

                MeiseiDTOClass ukeireInfo = new MeiseiDTOClass();
                MeiseiDTOClass shukkaInfo = new MeiseiDTOClass();

                #region 受入情報

                T_UR_SH_DETAIL ukeireDetail = this.Logic.CreateDainouUkeireDetail(i);

                // 売上/支払区分
                ukeireInfo.Uriageshiharai_Kbn = ukeireDetail.DENPYOU_KBN_CD.ToString();
                // 品名CD
                ukeireInfo.Hinmei_Cd = ukeireDetail.HINMEI_CD;
                // 金額
                ukeireInfo.Kingaku = (((ukeireDetail.KINGAKU.IsNull) ? 0 : ukeireDetail.KINGAKU) +
                    ((ukeireDetail.HINMEI_KINGAKU.IsNull) ? 0 : ukeireDetail.HINMEI_KINGAKU)).ToString();
                // 消費税外税
                ukeireInfo.Tax_Soto = (ukeireDetail.TAX_SOTO.IsNull) ? string.Empty : ukeireDetail.TAX_SOTO.ToString();
                // 消費税内税
                ukeireInfo.Tax_Uchi = (ukeireDetail.TAX_UCHI.IsNull) ? string.Empty : ukeireDetail.TAX_UCHI.ToString();
                // 品名別税区分CD
                ukeireInfo.Hinmei_Zei_Kbn_Cd = (ukeireDetail.HINMEI_ZEI_KBN_CD.IsNull) ? string.Empty : ukeireDetail.HINMEI_ZEI_KBN_CD.ToString();
                // 品名別税消費税外税
                ukeireInfo.Hinmei_Tax_Soto = (ukeireDetail.HINMEI_TAX_SOTO.IsNull) ? string.Empty : ukeireDetail.HINMEI_TAX_SOTO.ToString();
                // 品名別税消費税内税
                ukeireInfo.Hinmei_Tax_Uchi = (ukeireDetail.HINMEI_TAX_UCHI.IsNull) ? string.Empty : ukeireDetail.HINMEI_TAX_UCHI.ToString();

                #endregion

                #region 出荷情報

                T_UR_SH_DETAIL shukkaDetail = this.Logic.CreateDainouShukkaDetail(i);

                // 売上/支払区分
                shukkaInfo.Uriageshiharai_Kbn = shukkaDetail.DENPYOU_KBN_CD.ToString();
                // 品名CD
                shukkaInfo.Hinmei_Cd = shukkaDetail.HINMEI_CD;
                // 金額
                shukkaInfo.Kingaku = (((shukkaDetail.KINGAKU.IsNull) ? 0 : shukkaDetail.KINGAKU) +
                    ((shukkaDetail.HINMEI_KINGAKU.IsNull) ? 0 : shukkaDetail.HINMEI_KINGAKU)).ToString();
                // 消費税外税
                shukkaInfo.Tax_Soto = (shukkaDetail.TAX_SOTO.IsNull) ? string.Empty : shukkaDetail.TAX_SOTO.ToString();
                // 消費税内税
                shukkaInfo.Tax_Uchi = (shukkaDetail.TAX_UCHI.IsNull) ? string.Empty : shukkaDetail.TAX_UCHI.ToString();
                // 品名別税区分CD
                shukkaInfo.Hinmei_Zei_Kbn_Cd = (shukkaDetail.HINMEI_ZEI_KBN_CD.IsNull) ? string.Empty : shukkaDetail.HINMEI_ZEI_KBN_CD.ToString();
                // 品名別税消費税外税
                shukkaInfo.Hinmei_Tax_Soto = (shukkaDetail.HINMEI_TAX_SOTO.IsNull) ? string.Empty : shukkaDetail.HINMEI_TAX_SOTO.ToString();
                // 品名別税消費税内税
                shukkaInfo.Hinmei_Tax_Uchi = (shukkaDetail.HINMEI_TAX_UCHI.IsNull) ? string.Empty : shukkaDetail.HINMEI_TAX_UCHI.ToString();

                #endregion

                hakouPopUpDto.Ukeire_Out_Tenpyo_Cnt.Add(ukeireInfo);
                hakouPopUpDto.Shukka_Out_Tenpyo_Cnt.Add(shukkaInfo);
            }

            return hakouPopUpDto;
        }

        #endregion

        #region [F10]行挿入
        /// <summary>
        /// 明細に行を追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AddRow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 行未選択の場合
                if (this.Ichiran.CurrentRow == null)
                {
                    // メッセージを表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E029", "明細", "一覧");
                    // 処理中止
                    return;
                }

                this.Logic.AddNewRow();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F11]行削除
        /// <summary>
        /// 明細の行を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RemoveRow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 行未選択の場合
                if (this.Ichiran.CurrentRow == null)
                {
                    // メッセージを表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E029", "明細", "一覧");
                    // 処理中止
                    return;
                }

                this.Logic.RemoveSelectedRow();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F12クローズ処理
        /// <summary>
        /// F12クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region [1]運賃入力ボタンのクリック
        /// <summary>
        /// [1]運賃入力ボタンのクリック
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            // 新規の場合代納登録処理を行う
            if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.Regist(null, null);

                if (!this.IsExacuteUnchin)
                {
                    return;
                }
            }

            this.Logic.UpdateWindowShow();
        }
        #endregion

        #region [2]出荷量セットボタンのクリック
        /// <summary>
        /// [2]出荷量セットボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process2_Click(object sender, EventArgs e)
        {
            this.Logic.SetShukkaRyouInfo();
        }
        #endregion

        #region IME制御処理＆前回値保存
        /// <summary>
        /// IME制御処理＆前回値保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                string errCellName = string.Empty;
                if (this.errorCell != null)
                {
                    this.Ichiran.Focus();
                    this.Ichiran.CurrentCell = this.errorCell;
                    errCellName = this.errorCell.Name;
                    this.errorCell = null;
                }

                string columName = Ichiran.Columns[e.CellIndex].Name;
                if (null != this.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex].Value)
                {
                    strCellOldValue = this.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex].Value.ToString();
                }
                else
                {
                    strCellOldValue = string.Empty;
                }

                // フォーカスアウト時によるエラーの場合は前回値を保存しない
                if (!errCellName.Equals(e.CellName))
                {
                    this.Logic.BeforeIchiranChengeValues(2, e.RowIndex, 0);
                }

                switch (columName)
                {
                    case ConstClass.CONTROL_UKEIRE_MEISAI_BIKOU:
                    case ConstClass.CONTROL_SHUKKA_MEISAI_BIKOU:
                        this.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex].Style.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Hiragana;
                        break;
                    default:
                        this.Ichiran.ImeMode = ImeMode.NoControl;
                        break;
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                Type type = sender.GetType();
                if (type.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                }
                else if (type.Name == "CustomNumericTextBox2")
                {
                    CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 受入取引先更新後処理
        /// <summary>
        /// 受入取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKEIRE_TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.SetUkeireTorihikisaki())
                {
                    return;
                }
                this.Control_Enter(this.UKEIRE_TORIHIKISAKI_CD, null);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 受入取引先情報の設定
        /// </summary>
        public bool SetUkeireTorihikisaki()
        {
            bool ret = true;
            try
            {
                // チェック処理
                if (!this.Logic.CheckTorihikisaki(1))
                {
                    return ret;
                }

                // 変更なしの場合
                if (this.dicControl.ContainsKey(ConstClass.CONTROL_UKEIRE_TORIHIKISAKI_CD) &&
                    this.dicControl[ConstClass.CONTROL_UKEIRE_TORIHIKISAKI_CD].Equals(this.UKEIRE_TORIHIKISAKI_CD.Text))
                {
                    // 処理終了
                    return ret;
                }

                // 単価再取得 & 全ての明細と合計の計算
                ret = this.ichiranReload(1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUkeireTorihikisaki", ex);
                this.Logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 出荷取引先更新後処理
        /// <summary>
        /// 出荷取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKA_TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.SetShukkaTorihikisaki())
                {
                    return;
                }
                this.Control_Enter(this.SHUKKA_TORIHIKISAKI_CD, null);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 出荷取引先情報設定
        /// </summary>
        public bool SetShukkaTorihikisaki()
        {
            bool ret = true;
            try
            {
                // チェック処理
                if (!this.Logic.CheckTorihikisaki(2))
                {
                    return ret;
                }

                // 変更なしの場合
                if (this.dicControl.ContainsKey(ConstClass.CONTROL_SHUKKA_TORIHIKISAKI_CD) &&
                    this.dicControl[ConstClass.CONTROL_SHUKKA_TORIHIKISAKI_CD].Equals(this.SHUKKA_TORIHIKISAKI_CD.Text))
                {
                    // 処理終了
                    return ret;
                }

                // 単価再取得 & 全ての明細と合計の計算
                ret = this.ichiranReload(2);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShukkaTorihikisaki", ex);
                this.Logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion

        #region 受入業者更新後処理
        /// <summary>
        /// 受入業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                // チェックNGの場合
                if (!this.Logic.CheckGyousha(1))
                {
                    this.UKEIRE_GYOUSHA_CD.Focus();
                    return;
                }

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (!this.dicControl.ContainsKey(ConstClass.CONTROL_UKEIRE_GYOUSHA_CD) ||
                    !this.dicControl[ConstClass.CONTROL_UKEIRE_GYOUSHA_CD].Equals(this.UKEIRE_GYOUSHA_CD.Text))
                {
                    // 現場の関連情報をクリア
                    this.UKEIRE_GENBA_CD.Text = string.Empty;
                    this.UKEIRE_GENBA_NAME_RYAKU.Text = string.Empty;

                    if (this.Ichiran.Rows.Count > 1)
                    {
                        // currentCellが単価再読み込みや、再計算の対象だった場合、
                        // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                        if (this.Ichiran.CurrentCell != null
                            && (this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_UKEIRE_TANKA)
                            || this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_UKEIRE_KINGAKU)))
                        {
                            rowindex = this.Ichiran.CurrentRow.Index;
                            cellindex = this.Ichiran.CurrentCell.CellIndex;
                            this.Ichiran.CurrentCell = null;
                            isChageCurrentCell = true;
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        DialogResult dr = msgLogic.MessageBoxShow("C097", "業者");
                        if (dr == DialogResult.OK || dr == DialogResult.Yes)
                        {
                            this.Ichiran.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r =>
                            {
                                // 品名マスタ検索
                                int result = this.Logic.HinmeiSearch(Convert.ToString(r.Cells[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value), 1);
                                if (result == -1)
                                {
                                    return;
                                }
                                else if (result != 0)
                                {
                                    if (!this.Logic.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME"))
                                    {
                                        r.Cells[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = this.Logic.SearchHinmeiResult.Rows[0]["HINMEI_NAME"];
                                    }
                                }

                                this.Logic.CalcTanka(r.Index, 1);
                                this.Logic.SetHinmeiSuuryou(r.Index, 1);
                                this.Logic.CalcSuuryou(r.Index, 1);
                                this.Logic.CalcDetailKingaku(r.Index, 1);
                            });
                            this.Logic.ResetTankaCheck(); // MAILAN #158994 START

                            // 全ての明細と合計の計算
                            if (!this.Logic.CalcAllDetailAndTotal())
                            {
                                return;
                            }
                        }
                    }
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                //this.Control_Enter(this.UKEIRE_TORIHIKISAKI_CD, null);
                //this.Control_Enter(this.UKEIRE_GYOUSHA_CD, null);
                dicControl[this.UKEIRE_TORIHIKISAKI_CD.Name] = this.UKEIRE_TORIHIKISAKI_CD.Text;
                dicControl[this.UKEIRE_GYOUSHA_CD.Name] = this.UKEIRE_GYOUSHA_CD.Text;
                dicControl[this.UKEIRE_GENBA_CD.Name] = this.UKEIRE_GENBA_CD.Text;

                if (isChageCurrentCell)
                {
                    this.Ichiran.CurrentCell = Ichiran.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 出荷業者更新後処理
        /// <summary>
        /// 出荷業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                // チェックNGの場合
                if (!this.Logic.CheckGyousha(2))
                {
                    this.SHUKKA_GYOUSHA_CD.Focus();
                    return;
                }

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (!this.dicControl.ContainsKey(ConstClass.CONTROL_SHUKKA_GYOUSHA_CD) ||
                    !this.dicControl[ConstClass.CONTROL_SHUKKA_GYOUSHA_CD].Equals(this.SHUKKA_GYOUSHA_CD.Text))
                {
                    // 現場の関連情報をクリア
                    this.SHUKKA_GENBA_CD.Text = string.Empty;
                    this.SHUKKA_GENBA_NAME_RYAKU.Text = string.Empty;

                    if (this.Ichiran.Rows.Count > 1)
                    {
                        // currentCellが単価再読み込みや、再計算の対象だった場合、
                        // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                        if (this.Ichiran.CurrentCell != null
                            && (this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_SHUKKA_TANKA)
                            || this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_SHUKKA_KINGAKU)))
                        {
                            rowindex = this.Ichiran.CurrentRow.Index;
                            cellindex = this.Ichiran.CurrentCell.CellIndex;
                            this.Ichiran.CurrentCell = null;
                            isChageCurrentCell = true;
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        DialogResult dr = msgLogic.MessageBoxShow("C097", "業者");
                        if (dr == DialogResult.OK || dr == DialogResult.Yes)
                        {
                            this.Ichiran.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r =>
                            {
                                // 品名マスタ検索
                                int result = this.Logic.HinmeiSearch(Convert.ToString(r.Cells[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value), 2);
                                if (result == -1)
                                {
                                    return;
                                }
                                else if (result != 0)
                                {
                                    if (!this.Logic.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME"))
                                    {
                                        r.Cells[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = this.Logic.SearchHinmeiResult.Rows[0]["HINMEI_NAME"];
                                    }
                                }

                                this.Logic.CalcTanka(r.Index, 2);
                                this.Logic.SetHinmeiSuuryou(r.Index, 2);
                                this.Logic.CalcSuuryou(r.Index, 2);
                                this.Logic.CalcDetailKingaku(r.Index, 2);
                            });
                            this.Logic.ResetTankaCheck(); // MAILAN #158994 START
                            // 全ての明細と合計の計算
                            if (!this.Logic.CalcAllDetailAndTotal())
                            {
                                return;
                            }
                        }
                    }
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                this.Control_Enter(this.SHUKKA_TORIHIKISAKI_CD, null);
                this.Control_Enter(this.SHUKKA_GYOUSHA_CD, null);

                if (isChageCurrentCell)
                {
                    this.Ichiran.CurrentCell = Ichiran.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 受入現場更新後処理
        /// <summary>
        /// 受入現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKEIRE_GENBA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                // チェックNGの場合
                if (!this.Logic.CheckGenba(1))
                {
                    this.UKEIRE_GENBA_CD.Focus();
                    return;
                }

                // 現場CDブランクの状態でEnterもしくはTabでフォーカスアウトすると
                // 背景色色が青くなったままになってしまうためここで初期化
                this.UKEIRE_GENBA_CD.BackColor = new System.Drawing.Color();

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (this.dicControl.ContainsKey(ConstClass.CONTROL_UKEIRE_GYOUSHA_CD) &&
                    this.dicControl[ConstClass.CONTROL_UKEIRE_GYOUSHA_CD].Equals(this.UKEIRE_GYOUSHA_CD.Text) &&
                    this.dicControl.ContainsKey(ConstClass.CONTROL_UKEIRE_GENBA_CD) &&
                    this.dicControl[ConstClass.CONTROL_UKEIRE_GENBA_CD].Equals(this.UKEIRE_GENBA_CD.Text))
                {
                }
                else
                {
                    if (this.Ichiran.Rows.Count > 1)
                    {
                        // currentCellが単価再読み込みや、再計算の対象だった場合、
                        // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                        if (this.Ichiran.CurrentCell != null
                            && (this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_UKEIRE_TANKA)
                            || this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_UKEIRE_KINGAKU)))
                        {
                            rowindex = this.Ichiran.CurrentRow.Index;
                            cellindex = this.Ichiran.CurrentCell.CellIndex;
                            this.Ichiran.CurrentCell = null;
                            isChageCurrentCell = true;
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        DialogResult dr = msgLogic.MessageBoxShow("C097", "現場");
                        if (dr == DialogResult.OK || dr == DialogResult.Yes)
                        {
                            this.Ichiran.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r =>
                            {
                                // 品名マスタ検索
                                int result = this.Logic.HinmeiSearch(Convert.ToString(r.Cells[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value), 1);
                                if (result == -1)
                                {
                                    return;
                                }
                                else if (result != 0)
                                {
                                    if (!this.Logic.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME"))
                                    {
                                        r.Cells[ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = this.Logic.SearchHinmeiResult.Rows[0]["HINMEI_NAME"];
                                    }
                                }

                                this.Logic.CalcTanka(r.Index, 1);
                                this.Logic.SetHinmeiSuuryou(r.Index, 1);
                                this.Logic.CalcSuuryou(r.Index, 1);
                                this.Logic.CalcDetailKingaku(r.Index, 1);
                            });
                            this.Logic.ResetTankaCheck(); // MAILAN #158994 START

                            // 全ての明細と合計の計算
                            if (!this.Logic.CalcAllDetailAndTotal())
                            {
                                return;
                            }
                        }
                    }
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                //this.Control_Enter(this.UKEIRE_TORIHIKISAKI_CD, null);
                //this.Control_Enter(this.UKEIRE_GYOUSHA_CD, null);
                //this.Control_Enter(this.UKEIRE_GENBA_CD, null);

                dicControl[this.UKEIRE_TORIHIKISAKI_CD.Name] = this.UKEIRE_TORIHIKISAKI_CD.Text;
                dicControl[this.UKEIRE_GYOUSHA_CD.Name] = this.UKEIRE_GYOUSHA_CD.Text;
                dicControl[this.UKEIRE_GENBA_CD.Name] = this.UKEIRE_GENBA_CD.Text;

                if (isChageCurrentCell)
                {
                    this.Ichiran.CurrentCell = Ichiran.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 出荷現場更新後処理
        /// <summary>
        /// 出荷現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUKKA_GENBA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                int rowindex = 0;
                int cellindex = 0;
                bool isChageCurrentCell = false;

                // チェックNGの場合
                if (!this.Logic.CheckGenba(2))
                {
                    this.SHUKKA_GENBA_CD.Focus();
                    return;
                }

                // 現場CDブランクの状態でEnterもしくはTabでフォーカスアウトすると
                // 背景色色が青くなったままになってしまうためここで初期化
                this.SHUKKA_GENBA_CD.BackColor = new System.Drawing.Color();

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (this.dicControl.ContainsKey(ConstClass.CONTROL_SHUKKA_GYOUSHA_CD) &&
                    this.dicControl[ConstClass.CONTROL_SHUKKA_GYOUSHA_CD].Equals(this.SHUKKA_GYOUSHA_CD.Text) &&
                    this.dicControl.ContainsKey(ConstClass.CONTROL_SHUKKA_GENBA_CD) &&
                    this.dicControl[ConstClass.CONTROL_SHUKKA_GENBA_CD].Equals(this.SHUKKA_GENBA_CD.Text))
                {
                }
                else
                {
                    // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                    this.Ichiran.Rows.Cast<Row>()
                        .Where(w => !w.IsNewRow).ToList()
                        .ForEach(r =>
                        {
                            this.Logic.CalcTanka(r.Index, 2);
                            this.Logic.SetHinmeiSuuryou(r.Index, 2);
                            this.Logic.CalcSuuryou(r.Index, 2);
                            this.Logic.CalcDetailKingaku(r.Index, 2);
                        });

                    // 全ての明細と合計の計算
                    this.Logic.CalcAllDetailAndTotal();

                    if (this.Ichiran.Rows.Count > 1)
                    {
                        // currentCellが単価再読み込みや、再計算の対象だった場合、
                        // ポップアップが上がった後にCurrentCellがEditModeになってしまう問題の対策。
                        if (this.Ichiran.CurrentCell != null
                            && (this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_SHUKKA_TANKA)
                            || this.Ichiran.CurrentCell.Name.Equals(ConstClass.CONTROL_SHUKKA_KINGAKU)))
                        {
                            rowindex = this.Ichiran.CurrentRow.Index;
                            cellindex = this.Ichiran.CurrentCell.CellIndex;
                            this.Ichiran.CurrentCell = null;
                            isChageCurrentCell = true;
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        DialogResult dr = msgLogic.MessageBoxShow("C097", "現場");
                        if (dr == DialogResult.OK || dr == DialogResult.Yes)
                        {
                            this.Ichiran.Rows.Where(r => !r.IsNewRow).ToList().ForEach(r =>
                            {
                                // 品名マスタ検索
                                int result = this.Logic.HinmeiSearch(Convert.ToString(r.Cells[ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value), 2);
                                if (result == -1)
                                {
                                    return;
                                }
                                else if (result != 0)
                                {
                                    if (!this.Logic.SearchHinmeiResult.Rows[0].IsNull("HINMEI_NAME"))
                                    {
                                        r.Cells[ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = this.Logic.SearchHinmeiResult.Rows[0]["HINMEI_NAME"];
                                    }
                                }

                                this.Logic.CalcTanka(r.Index, 2);
                                this.Logic.SetHinmeiSuuryou(r.Index, 2);
                                this.Logic.CalcSuuryou(r.Index, 2);
                                this.Logic.CalcDetailKingaku(r.Index, 2);
                            });
                            this.Logic.ResetTankaCheck(); // MAILAN #158994 START

                            // 全ての明細と合計の計算
                            if (!this.Logic.CalcAllDetailAndTotal())
                            {
                                return;
                            }
                        }
                    }
                    this.Logic.ResetTankaCheck(); // MAILAN #158994 START
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                this.Control_Enter(this.SHUKKA_TORIHIKISAKI_CD, null);
                this.Control_Enter(this.SHUKKA_GYOUSHA_CD, null);
                this.Control_Enter(this.SHUKKA_GENBA_CD, null);

                if (isChageCurrentCell)
                {
                    this.Ichiran.CurrentCell = Ichiran.Rows[rowindex].Cells[cellindex];
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 受入業者ポッポアップ画面からの処理
        /// <summary>
        /// 受入業者ポッポアップ画面からの処理
        /// </summary>
        public void PopupAfte_UKEIRE_GYOUSHA_CD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.UKEIRE_GYOUSHA_CD_Validated(null, null);

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 出荷業者ポッポアップ画面からの処理
        /// <summary>
        /// 出荷業者ポッポアップ画面からの処理
        /// </summary>
        public void PopupAfte_SHUKKA_GYOUSHA_CD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SHUKKA_GYOUSHA_CD_Validated(null, null);

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 受入現場ポッポアップ画面からの処理
        /// <summary>
        /// 受入現場ポッポアップ画面からの処理
        /// </summary>
        public void PopupAfte_UKEIRE_GENBA_CD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.UKEIRE_GENBA_CD_Validated(null, null);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 出荷現場ポッポアップ画面からの処理
        /// <summary>
        /// 出荷現場ポッポアップ画面からの処理
        /// </summary>
        public void PopupAfte_SHUKKA_GENBA_CD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SHUKKA_GENBA_CD_Validated(null, null);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region RowsAddedイベント
        /// <summary>
        /// RowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            try
            {
                // 行NO設定
                this.Logic.SetRowNo();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }
        #endregion

        #region RowsRemovedイベント
        /// <summary>
        /// RowsRemovedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_RowsRemoved(object sender, RowsRemovedEventArgs e)
        {
            try
            {
                // 行NO設定
                this.Logic.SetRowNo();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 入力担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUURYOKU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.Logic.CheckNyuuryokuTantousha();
        }

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.Logic.CheckUnpanGyousha();
        }

        /// <summary>
        /// 車種更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_Validated(object sender, EventArgs e)
        {
            this.Logic.CheckShashu();
        }

        /// <summary>
        /// 車輌更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            this.Logic.CheckSharyou();
        }

        /// <summary>
        /// 運転者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            this.Logic.CheckUntensha();
        }

        /// <summary>
        /// 代納伝票番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAINOU_NUMBER_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 伝票番号記憶
                if (!this.DAINOU_NUMBER.Enabled || string.IsNullOrEmpty(this.DAINOU_NUMBER.Text))
                {
                    return;
                }

                this.Logic.PrmDainouNumber = 0;

                WINDOW_TYPE tmpType = this.WindowType;

                if (!string.IsNullOrEmpty(this.DAINOU_NUMBER.Text))
                {
                    // 代納入力があるか確認
                    int result = this.Logic.GetDainoNumberExists();
                    if (result == -1)
                    {
                        this.DAINOU_NUMBER.Focus();
                        return;
                    }
                    else if (result > 0)
                    {
                        // モードを修正に変更
                        this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // 修正権限は無いが参照権限がある場合は参照モードで起動
                                this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            }
                            else
                            {
                                // どちらも無い場合はアラートを表示して処理中断
                                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E158", "修正");
                                this.WindowType = tmpType;
                                this.DAINOU_NUMBER.Focus();
                                return;
                            }
                        }

                        this.Logic.PrmDainouNumber = long.Parse(this.DAINOU_NUMBER.Text);
                        // ヘッダーセット
                        this.Logic.SetWindowTypeLabel(this.WindowType);
                        // 画面表示
                        this.Logic.DisplayInit();
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        int count = this.Logic.GetUrshNumberExists();
                        if (count == -1)
                        {
                            this.DAINOU_NUMBER.Focus();
                            return;
                        }
                        else if (count > 0)
                        {
                            msgLogic.MessageBoxShowError("該当のデータを修正するには、売上/支払入力画面で行ってください。\nこの画面では修正できません。");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E045");
                            this.DAINOU_NUMBER.Focus();
                            return;
                        }

                        //フォーカス移動しない
                        this.DAINOU_NUMBER.Text = string.Empty;
                        this.DAINOU_NUMBER.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKEIRE_EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 前回値チェック
                if (dicControl.ContainsKey(this.UKEIRE_EIGYOU_TANTOUSHA_CD.Name))
                {
                    string beforeValue = dicControl[this.UKEIRE_EIGYOU_TANTOUSHA_CD.Name];
                    if (beforeValue.Equals(this.UKEIRE_EIGYOU_TANTOUSHA_CD.Text)
                        && !this.Logic.eigyoushaCd.IsInputErrorOccured)
                    {
                        return;
                    }
                }

                // チェックNGの場合
                if (!this.Logic.CheckEigyousha(1))
                {
                    return;
                }

                this.Control_Enter(this.UKEIRE_EIGYOU_TANTOUSHA_CD, null);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 出荷営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUKKA_EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 前回値チェック
                if (dicControl.ContainsKey(this.SHUKKA_EIGYOU_TANTOUSHA_CD.Name))
                {
                    string beforeValue = dicControl[this.SHUKKA_EIGYOU_TANTOUSHA_CD.Name];
                    if (beforeValue.Equals(this.SHUKKA_EIGYOU_TANTOUSHA_CD.Text)
                        && !this.Logic.eigyoushaCd.IsInputErrorOccured)
                    {
                        return;
                    }
                }

                // チェックNGの場合
                if (!this.Logic.CheckEigyousha(2))
                {
                    return;
                }

                this.Control_Enter(this.SHUKKA_EIGYOU_TANTOUSHA_CD, null);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHOUHIZEI_RATE_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_SHOUHIZEI_RATE.Text = this.Logic.ToPercentForShiharaiShouhizeiRate();
            this.Logic.GetShiharaiShouhizeiRate();
        }

        /// <summary>
        /// 売上消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_SHOUHIZEI_RATE_TextChanged(object sender, EventArgs e)
        {
            this.URIAGE_SHOUHIZEI_RATE.Text = this.Logic.ToPercentForUriageShouhizeiRate();
            this.Logic.GetUriageShouhizeiRate();
        }

        /// <summary>
        /// 支払日付更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_DATE_Validated(object sender, EventArgs e)
        {
            this.Logic.SetShiharaiShouhizeiRate();
        }

        /// <summary>
        /// 売上日付更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_DATE_Validated(object sender, EventArgs e)
        {
            this.Logic.SetUriageShouhizeiRate();
        }

        private void KAKUTEI_KBN_Validated(object sender, EventArgs e)
        {
            this.Logic.CheckKakuteiKbn();
        }
        #region 月次ロックチェック

        /// <summary>
        /// [登録処理用] 月次ロックされているのかの判定を行います
        /// </summary>
        /// <returns>月次ロック中：True</returns>
        internal bool GetsujiLockCheck()
        {
            bool returnVal = false;

            try
            {
                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if ((this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG) ||
                    (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG))
                {
                    // 新規・削除は画面に表示されている伝票日付を使用
                    DateTime getsujiShoriCheckDate = DateTime.Parse(this.DENPYOU_DATE.Value.ToString());

                    // 月次処理中チェック
                    if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                    {
                        returnVal = true;
                        if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                        {
                            msgLogic.MessageBoxShow("E224", "登録");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E224", "削除");
                        }
                    }
                    // 月次ロックチェック
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                    {
                        returnVal = true;
                        if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                        {
                            msgLogic.MessageBoxShow("E223", "登録");
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E222", "削除");
                        }
                    }
                }
                else
                {
                    // 修正は伝票日付が変更されている可能性があるため変更前データと違う場合は画面起動から
                    // 登録までの間に月次処理が行われていないか確認する。
                    // 上記が問題なければ現在表示されている変更後の日付が月次処理中、月次処理済期間内かをチェックする
                    DateTime beforDate = DateTime.Parse(this.Logic.beforeDenpyouDate);
                    DateTime updateDate = DateTime.Parse(this.DENPYOU_DATE.Value.ToString());

                    // 月次処理中チェック
                    if ((beforDate.CompareTo(updateDate) != 0) &&
                        getsujiShoriCheckLogic.CheckGetsujiShoriChu(beforDate))
                    {
                        returnVal = true;
                        msgLogic.MessageBoxShow("E224", "修正");
                    }
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(updateDate))
                    {
                        returnVal = true;
                        msgLogic.MessageBoxShow("E224", "修正");
                    }
                    // 月次ロックチェック
                    else if ((beforDate.CompareTo(updateDate) != 0) &&
                        getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(beforDate.Year.ToString()), short.Parse(beforDate.Month.ToString())))
                    {
                        returnVal = true;
                        msgLogic.MessageBoxShow("E223", "修正");
                    }
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(updateDate.Year.ToString()), short.Parse(updateDate.Month.ToString())))
                    {
                        returnVal = true;
                        msgLogic.MessageBoxShow("E223", "修正");
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetsujiLockCheck", ex1);
                this.Logic.errmessage.MessageBoxShow("E093", "");
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetsujiLockCheck", ex);
                this.Logic.errmessage.MessageBoxShow("E245", "");
                returnVal = true;
            }

            return returnVal;
        }

        #endregion

        //ThangNguyen [Add] 20150828 #12553 Start
        private void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            var inputDenpyouDate = this.DENPYOU_DATE.Text;
            if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.tmpDenpyouDate.Equals(inputDenpyouDate))
            {
                //this.logic.CheckDenpyouDate();
                this.URIAGE_DATE.Value = this.DENPYOU_DATE.Value;
                this.SHIHARAI_DATE.Value = this.DENPYOU_DATE.Value;
                this.URIAGE_DATE_Validated(sender, e);
                this.SHIHARAI_DATE_Validated(sender, e);

                // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                this.Ichiran.Rows.Cast<Row>()
                    .Where(w => !w.IsNewRow).ToList()
                    .ForEach(r =>
                    {
                        this.Logic.CalcTanka(r.Index, 1);
                        this.Logic.CalcTanka(r.Index, 2);
                        this.Logic.SetHinmeiSuuryou(r.Index, 1);
                        this.Logic.SetHinmeiSuuryou(r.Index, 2);
                        this.Logic.CalcSuuryou(r.Index, 1);
                        this.Logic.CalcSuuryou(r.Index, 2);
                        this.Logic.CalcDetailKingaku(r.Index, 1);
                        this.Logic.CalcDetailKingaku(r.Index, 2);
                    });
                this.Logic.ResetTankaCheck(); // MAILAN #158994 START

                // 合計系の再計算
                this.Logic.CalcAllDetailAndTotal();
            }
        }

        private void DENPYOU_DATE_Enter(object sender, EventArgs e)
        {
            tmpDenpyouDate = this.DENPYOU_DATE.Text;
        }
        //ThangNguyen [Add] 20150828 #12553 End

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        private string hinmeiCd = "";
        private string hinmeiName = "";
        public void UKEIRE_HINMEI_CD_PopupBeforeExecuteMethod()
        {
            this.hinmeiCd = Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value);
            this.hinmeiName = Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value);
            //20211230 Thanh 158918 s
            this.TORIHIKISAKI_CD.Text = UKEIRE_TORIHIKISAKI_CD.Text;
            this.TORIHIKISAKI_NAME_RYAKU.Text = UKEIRE_TORIHIKISAKI_NAME_RYAKU.Text;
            this.GYOUSHA_CD.Text = UKEIRE_GYOUSHA_CD.Text;
            this.GYOUSHA_NAME_RYAKU.Text = UKEIRE_GYOUSHA_NAME_RYAKU.Text;
            this.GENBA_CD.Text = UKEIRE_GENBA_CD.Text;
            this.GENBA_NAME_RYAKU.Text = UKEIRE_GENBA_NAME_RYAKU.Text;
            this.UNPAN_GYOUSHA_NAME.Text = UNPAN_GYOUSHA_NAME_RYAKU.Text;
            //20211230 Thanh 158918 e
        }
        public void SHUKKA_HINMEI_CD_PopupBeforeExecuteMethod()
        {
            this.hinmeiCd = Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value);
            this.hinmeiName = Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value);
            //20211230 Thanh 158918 s
            this.TORIHIKISAKI_CD.Text = SHUKKA_TORIHIKISAKI_CD.Text;
            this.TORIHIKISAKI_NAME_RYAKU.Text = SHUKKA_TORIHIKISAKI_NAME_RYAKU.Text;
            this.GYOUSHA_CD.Text = SHUKKA_GYOUSHA_CD.Text;
            this.GYOUSHA_NAME_RYAKU.Text = SHUKKA_GYOUSHA_NAME_RYAKU.Text;
            this.GENBA_CD.Text = SHUKKA_GENBA_CD.Text;
            this.GENBA_NAME_RYAKU.Text = SHUKKA_GENBA_NAME_RYAKU.Text;
            this.UNPAN_GYOUSHA_NAME.Text = UNPAN_GYOUSHA_NAME_RYAKU.Text;
            //20211230 Thanh 158918 e
        }
        public void UKEIRE_HINMEI_CD_PopupAfterExecuteMethod()
        {
            if(string.IsNullOrEmpty(Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value))
                || (hinmeiCd == Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value)
                && hinmeiName == Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value)))
            {
                return;
            }
            if (this.Logic.HinmeiSearch(Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_CD].Value), 1) == 1)
            {
                this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_UKEIRE_HINMEI_NAME].Value = Convert.ToString(this.Logic.SearchHinmeiResult.Rows[0]["HINMEI_NAME"]);
            }
        }

        public void SHUKKA_HINMEI_CD_PopupAfterExecuteMethod()
        {
            if (string.IsNullOrEmpty(Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value))
                || (hinmeiCd == Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value)
                && hinmeiName == Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value)))
            {
                return;
            }
            if (this.Logic.HinmeiSearch(Convert.ToString(this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_CD].Value), 2) == 1)
            {
                this.Ichiran[this.Ichiran.CurrentCell.RowIndex, ConstClass.CONTROL_SHUKKA_HINMEI_NAME].Value = Convert.ToString(this.Logic.SearchHinmeiResult.Rows[0]["HINMEI_NAME"]);
            }
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>
        /// 単価と金額の活性/非活性制御
        /// </summary>
        /// <param name="rowIndex"></param>
        internal void SetIchiranReadOnly(int rowIndex)
        {
            LogUtility.DebugMethodStart(rowIndex);

            if (rowIndex < 0) return;

            var row = this.Ichiran.Rows[rowIndex];

            // 受入
            {
                if ((row.Cells["UKEIRE_TANKA"].Value == null || string.IsNullOrEmpty(row.Cells["UKEIRE_TANKA"].Value.ToString())) &&
                    (row.Cells["UKEIRE_KINGAKU"].Value == null || string.IsNullOrEmpty(row.Cells["UKEIRE_KINGAKU"].Value.ToString())))
                {
                    // 「単価」、「金額」どちらも空の場合、両方操作可
                    this.Ichiran.Rows[rowIndex].Cells["UKEIRE_TANKA"].ReadOnly = false;
                    this.Ichiran.Rows[rowIndex].Cells["UKEIRE_KINGAKU"].ReadOnly = false;
                }
                else if (row.Cells["UKEIRE_TANKA"].Value != null && !string.IsNullOrEmpty(row.Cells["UKEIRE_TANKA"].Value.ToString()))
                {
                    // 「単価」のみ入力済みの場合、「金額」操作不可
                    this.Ichiran.Rows[rowIndex].Cells["UKEIRE_TANKA"].ReadOnly = false;
                    this.Ichiran.Rows[rowIndex].Cells["UKEIRE_KINGAKU"].ReadOnly = true;
                }
                else if (row.Cells["UKEIRE_KINGAKU"].Value != null && !string.IsNullOrEmpty(row.Cells["UKEIRE_KINGAKU"].Value.ToString()))
                {
                    // 「金額」のみ入力済みの場合、「単価」操作不可
                    this.Ichiran.Rows[rowIndex].Cells["UKEIRE_TANKA"].ReadOnly = true;
                    this.Ichiran.Rows[rowIndex].Cells["UKEIRE_KINGAKU"].ReadOnly = false;
                }

                row.Cells["UKEIRE_TANKA"].UpdateBackColor(false);
                row.Cells["UKEIRE_KINGAKU"].UpdateBackColor(false);
            }

            // 出荷
            {
                if ((row.Cells["SHUKKA_TANKA"].Value == null || string.IsNullOrEmpty(row.Cells["SHUKKA_TANKA"].Value.ToString())) &&
                    (row.Cells["SHUKKA_KINGAKU"].Value == null || string.IsNullOrEmpty(row.Cells["SHUKKA_KINGAKU"].Value.ToString())))
                {
                    // 「単価」、「金額」どちらも空の場合、両方操作可
                    this.Ichiran.Rows[rowIndex].Cells["SHUKKA_TANKA"].ReadOnly = false;
                    this.Ichiran.Rows[rowIndex].Cells["SHUKKA_KINGAKU"].ReadOnly = false;
                }
                else if (row.Cells["SHUKKA_TANKA"].Value != null && !string.IsNullOrEmpty(row.Cells["SHUKKA_TANKA"].Value.ToString()))
                {
                    // 「単価」のみ入力済みの場合、「金額」操作不可
                    this.Ichiran.Rows[rowIndex].Cells["SHUKKA_TANKA"].ReadOnly = false;
                    this.Ichiran.Rows[rowIndex].Cells["SHUKKA_KINGAKU"].ReadOnly = true;
                }
                else if (row.Cells["SHUKKA_KINGAKU"].Value != null && !string.IsNullOrEmpty(row.Cells["SHUKKA_KINGAKU"].Value.ToString()))
                {
                    // 「金額」のみ入力済みの場合、「単価」操作不可
                    this.Ichiran.Rows[rowIndex].Cells["SHUKKA_TANKA"].ReadOnly = true;
                    this.Ichiran.Rows[rowIndex].Cells["SHUKKA_KINGAKU"].ReadOnly = false;
                }

                row.Cells["SHUKKA_TANKA"].UpdateBackColor(false);
                row.Cells["SHUKKA_KINGAKU"].UpdateBackColor(false);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細欄の単価･金額をすべて再計算します
        /// </summary>
        /// <param name="flg">1:受入,2：支払</param>
        internal bool ichiranReload(int flg)
        {
            bool ret = true;

            var cellName = flg == 1 ? ConstClass.CONTROL_UKEIRE_HINMEI_CD : ConstClass.CONTROL_SHUKKA_HINMEI_CD;

            // 単価再取得
            for (int i = 0; i < this.Ichiran.Rows.Count - this.Logic.newRowNum; i++)
            {
                //品名CD
                var hinmeiCd = this.Ichiran.Rows[i].Cells[cellName].Value;
                if (null != hinmeiCd && !string.IsNullOrEmpty(hinmeiCd.ToString()))
                {
                    if (!this.Logic.CalcTanka(i, flg))
                    {
                        ret = false;
                        this.Logic.ResetTankaCheck(); // MAILAN #158994 START
                        return ret;
                    }
                    if (!this.Logic.SetHinmeiSuuryou(i, flg))
                    {
                        ret = false;
                        return ret;
                    }
                    if (!this.Logic.CalcSuuryou(i, flg))
                    {
                        ret = false;
                        return ret;
                    }
                    if (!this.Logic.CalcDetailKingaku(i, flg))
                    {
                        ret = false;
                        return ret;
                    }
                }
            }
            this.Logic.ResetTankaCheck(); // MAILAN #158994 START

            // 全ての明細と合計の計算
            if (!this.Logic.CalcAllDetailAndTotal())
            {
                ret = false;
            }

            return ret;
        }

        //20211230 Thanh 158916 s
        /// <summary>
        /// G161Form_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void G161Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                if (this.Ichiran.CurrentCell != null && this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
                {
                    if (this.Ichiran.CurrentCell.IsInEditMode)
                    {
                        if (e.KeyChar == (Char)Keys.Space)
                        {
                            this.OpenTankaRirekiUkeire(this.Ichiran.CurrentRow.Index);
                        }
                    }
                }
                if (this.Ichiran.CurrentCell != null && this.Ichiran.CurrentCell.Name == "SHUKKA_TANKA")
                {
                    if (this.Ichiran.CurrentCell.IsInEditMode)
                    {
                        if (e.KeyChar == (Char)Keys.Space)
                        {
                            this.OpenTankaRirekiShukka(this.Ichiran.CurrentRow.Index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// OpenTankaRirekiUkeire
        /// </summary>
        private void OpenTankaRirekiUkeire(int index)
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
            string HinmeiCd = string.Empty;
            string UnitCd = string.Empty;
            int flg = 1;
            if (this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
            {
                HinmeiCd = Convert.ToString(this.Ichiran.Rows[index].Cells["UKEIRE_HINMEI_CD"].Value);
                UnitCd = Convert.ToString(this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_CD"].Value);
            }
            else if (this.Ichiran.CurrentCell.Name == "SHUKKA_TANKA")
            {
                HinmeiCd = Convert.ToString(this.Ichiran.Rows[index].Cells["SHUKKA_HINMEI_CD"].Value);
                UnitCd = Convert.ToString(this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_CD"].Value);
                flg = 2;
            }
            if (!string.IsNullOrEmpty((this.Logic.headerForm).KYOTEN_CD.Text))
            {
                kyotenCd = this.Logic.headerForm.KYOTEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UKEIRE_TORIHIKISAKI_CD.Text))
            {
                torihikisakiCd = this.UKEIRE_TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UKEIRE_GYOUSHA_CD.Text))
            {
                gyoushaCd = this.UKEIRE_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UKEIRE_GENBA_CD.Text))
            {
                genbaCd = this.UKEIRE_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                unpanGyoushaCd = this.UNPAN_GYOUSHA_CD.Text;
            }
            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G161_1",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, HinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
            if (tankaForm.dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tankaForm.returnTanka.IsNull)
                {
                    this.Ichiran.EditingControl.Text = string.Empty;
                }
                else
                {
                    this.Ichiran.EditingControl.Text = tankaForm.returnTanka.Value.ToString(this.Logic.sysInfoEntity.SYS_TANKA_FORMAT);
                }

                if (!UnitCd.Equals(tankaForm.returnUnitCd))
                {
                    if (string.IsNullOrEmpty(tankaForm.returnUnitCd))
                    {
                        if (this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
                        {
                            this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_CD"].Value = string.Empty;
                            this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_NAME"].Value = string.Empty;
                        }
                        else
                        {
                            this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_CD"].Value = string.Empty;
                            this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_NAME"].Value = string.Empty;
                        }
                    }
                    else
                    {
                        var unitInfo = this.Logic.accessor.GetUnit(short.Parse(tankaForm.returnUnitCd));
                        if (unitInfo.Length > 0)
                        {
                            if (this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
                            {
                                this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_CD"].Value = unitInfo[0].UNIT_CD.ToString();
                                this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_NAME"].Value = unitInfo[0].UNIT_NAME_RYAKU.ToString();
                            }
                            else
                            {
                                this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_CD"].Value = unitInfo[0].UNIT_CD.ToString();
                                this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_NAME"].Value = unitInfo[0].UNIT_NAME_RYAKU.ToString();
                            }
                            this.Logic.SetHinmeiSuuryou(index, flg);
                            if (!this.Logic.CalcSuuryou(index, flg))
                            {
                                return;
                            }

                            // 明細金額計算
                            if (!this.Logic.CalcDetailKingaku(index, flg))
                            {
                                return;
                            }

                            // 合計系の計算
                            if (!this.Logic.CalcAllDetailAndTotal())
                            {
                                return;
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// OpenTankaRirekiShukka
        /// </summary>
        private void OpenTankaRirekiShukka(int index)
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
            string HinmeiCd = string.Empty;
            string UnitCd = string.Empty;
            int flg = 1;
            if (this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
            {
                HinmeiCd = Convert.ToString(this.Ichiran.Rows[index].Cells["UKEIRE_HINMEI_CD"].Value);
                UnitCd = Convert.ToString(this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_CD"].Value);
            }
            else if (this.Ichiran.CurrentCell.Name == "SHUKKA_TANKA")
            {
                HinmeiCd = Convert.ToString(this.Ichiran.Rows[index].Cells["SHUKKA_HINMEI_CD"].Value);
                UnitCd = Convert.ToString(this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_CD"].Value);
                flg = 2;
            }
            if (!string.IsNullOrEmpty((this.Logic.headerForm).KYOTEN_CD.Text))
            {
                kyotenCd = this.Logic.headerForm.KYOTEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.SHUKKA_TORIHIKISAKI_CD.Text))
            {
                torihikisakiCd = this.SHUKKA_TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.SHUKKA_GYOUSHA_CD.Text))
            {
                gyoushaCd = this.SHUKKA_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.SHUKKA_GENBA_CD.Text))
            {
                genbaCd = this.SHUKKA_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                unpanGyoushaCd = this.UNPAN_GYOUSHA_CD.Text;
            }
            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G161_2",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, HinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
            if (tankaForm.dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tankaForm.returnTanka.IsNull)
                {
                    this.Ichiran.EditingControl.Text = string.Empty;
                }
                else
                {
                    this.Ichiran.EditingControl.Text = tankaForm.returnTanka.Value.ToString(this.Logic.sysInfoEntity.SYS_TANKA_FORMAT);
                }

                if (!UnitCd.Equals(tankaForm.returnUnitCd))
                {
                    if (string.IsNullOrEmpty(tankaForm.returnUnitCd))
                    {
                        if (this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
                        {
                            this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_CD"].Value = string.Empty;
                            this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_NAME"].Value = string.Empty;
                        }
                        else
                        {
                            this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_CD"].Value = string.Empty;
                            this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_NAME"].Value = string.Empty;
                        }
                    }
                    else
                    {
                        var unitInfo = this.Logic.accessor.GetUnit(short.Parse(tankaForm.returnUnitCd));
                        if (unitInfo.Length > 0)
                        {
                            if (this.Ichiran.CurrentCell.Name == "UKEIRE_TANKA")
                            {
                                this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_CD"].Value = unitInfo[0].UNIT_CD.ToString();
                                this.Ichiran.Rows[index].Cells["UKEIRE_UNIT_NAME"].Value = unitInfo[0].UNIT_NAME_RYAKU.ToString();
                            }
                            else
                            {
                                this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_CD"].Value = unitInfo[0].UNIT_CD.ToString();
                                this.Ichiran.Rows[index].Cells["SHUKKA_UNIT_NAME"].Value = unitInfo[0].UNIT_NAME_RYAKU.ToString();
                            }
                            this.Logic.SetHinmeiSuuryou(index, flg);
                            if (!this.Logic.CalcSuuryou(index, flg))
                            {
                                return;
                            }

                            // 明細金額計算
                            if (!this.Logic.CalcDetailKingaku(index, flg))
                            {
                                return;
                            }

                            // 合計系の計算
                            if (!this.Logic.CalcAllDetailAndTotal())
                            {
                                return;
                            }

                        }
                    }
                }
            }
        }
        //20211230 Thanh 158916 e
    }
}
