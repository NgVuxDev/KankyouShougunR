using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.Const;
using Seasar.Quill.Attrs;
using System.Collections;
using System.Collections.Generic;
using r_framework.Entity;
using r_framework.Dto;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Linq;

namespace Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu
{
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic = null;

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>
        /// 運搬区間
        /// </summary>
        public int UpnRouteNo { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string ShainCd { get; set; }

        /// <summary>
        /// タイトルリスト
        /// </summary>
        public ArrayList TitleList { get; set; }

        /// <summary>
        /// 編集前入力値
        /// </summary>
        internal string BeforeCellValue = String.Empty;

        /// <summary>
        /// 編集前入力値 行
        /// </summary>
        internal int BeforeRowIndex = 0;

        /// <summary>
        /// 編集前入力値 列
        /// </summary>
        internal int BeforeColumnIndex = 0;

        internal bool isRegistErr = false;

        public UIForm()
            : base(WINDOW_ID.T_MANIFEST_IKKATU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.ShainCd = SystemProperty.Shain.CD;
            // 3/14 暫定パターン固定化対応
            this.ShainCd = "000001";
            this.InitializeComponent();

            this.mlogic = new Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.logic.SelectItirannData(this.ShainCd);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// パターン登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternTouroku(object sender, EventArgs e)
        {

            MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

            var errList = new List<string>();

            r_framework.CustomControl.ICustomAutoChangeBackColor t; //色変え用インターフェース
 
            //新規行の場合、個別にチェックが必要
            if (this.NyuryokuIkkatsuItiran.CurrentRow.IsNewRow)
            {
                errList.Add( string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.MANIFEST_SHURUI_CD));
                errList.Add( string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.FIRST_SECOND_KBN));

                t = this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.MANIFEST_SHURUI_CD] as r_framework.CustomControl.ICustomAutoChangeBackColor;
                //赤くする
                if (t != null)
                {
                    t.IsInputErrorOccured = true;
                    t.UpdateBackColor();
                }
                t = this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.FIRST_SECOND_KBN] as r_framework.CustomControl.ICustomAutoChangeBackColor;
                //赤くする
                if (t != null)
                {
                    t.IsInputErrorOccured = true;
                    t.UpdateBackColor();
                }


                //新規行もマニフェスト種類と一次二次区分がnullなので、エラーメッセージを出して終了
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(string.Join(Environment.NewLine, errList));
                return;
            }

            
            //マニフェスト種類 必須チェック
            t = this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.MANIFEST_SHURUI_CD] as r_framework.CustomControl.ICustomAutoChangeBackColor;
            if (string.Empty.Equals(GetItiranCellValue(this.NyuryokuIkkatsuItiran.CurrentRow.Index, this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index)))
            {
                //赤くする
                if (t != null)
                {
                    t.IsInputErrorOccured = true;
                    t.UpdateBackColor();
                }
                //エラーメッセージ表示
                errList.Add(string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.MANIFEST_SHURUI_CD));
            }
            else
            {
                //白くする
                if (t != null)
                {
                    t.IsInputErrorOccured = false;
                    t.UpdateBackColor();
                }

            }
            //1次2次区分 必須チェック
            t = this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.FIRST_SECOND_KBN] as r_framework.CustomControl.ICustomAutoChangeBackColor;
            if (string.Empty.Equals(GetItiranCellValue(this.NyuryokuIkkatsuItiran.CurrentRow.Index, this.NyuryokuIkkatsuItiran.Columns[ConstCls.FIRST_SECOND_KBN].Index)))
            {
                //赤くする
                if (t != null)
                {
                    t.IsInputErrorOccured = true;
                    t.UpdateBackColor();
                }
                //エラーメッセージ表示
                errList.Add(string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E001"), ConstCls.FIRST_SECOND_KBN));
            }
            else
            {
                //白くする
                if (t != null)
                {
                    t.IsInputErrorOccured = false;
                    t.UpdateBackColor();
                }
            }

            //エラーがあればまとめて出す
            if (errList.Count > 0)
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(string.Join(Environment.NewLine, errList));
                return;
            }


            List<T_MANIFEST_PT_ENTRY> entryList = new List<T_MANIFEST_PT_ENTRY>();
            List<T_MANIFEST_PT_UPN> upnList = new List<T_MANIFEST_PT_UPN>();
            List<T_MANIFEST_PT_DETAIL> detailList = new List<T_MANIFEST_PT_DETAIL>();

            if (!this.logic.PatternTouroku(ref entryList, ref upnList, ref detailList)) { return; }

            // 一次二次区分
            int firstSecondKbn = Convert.ToInt32(this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.FIRST_SECOND_KBN].Value);

            if (firstSecondKbn > 0)
            {
                firstSecondKbn = firstSecondKbn - 1;
            }
            var callForm = new ManifestPatternTouroku.ManifestPatternTouroku(
                Convert.ToInt32(DENSHU_KBN.MANIFEST_IKKATU), firstSecondKbn, true,
                entryList, upnList, null, null, detailList, null, null, null);

            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                callForm.ShowDialog();
            }
        }

        /// <summary>
        /// データ複製イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataFukusei(object sender, EventArgs e)
        {
            int rowIndex = this.NyuryokuIkkatsuItiran.CurrentRow.Index;

            // 第一行を選択される場合
            if (rowIndex == 0)
            {
                return;
            }

            if (rowIndex == this.NyuryokuIkkatsuItiran.RowCount - 1)
            {
                this.NyuryokuIkkatsuItiran.Rows.Add();
            }

            int columnsLength = this.NyuryokuIkkatsuItiran.Columns.Count;

            for (int i = 0; i < columnsLength; i++)
            {
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[i].Value =
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex - 1].Cells[i].Value;
            }
        }

        /// <summary>
        /// 項目呼出複製イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KoumokuYobidasi(object sender, EventArgs e)
        {
            int denshu = (int)DENSHU_KBN.MANIFEST_IKKATU;

            var callForm = new Shougun.Core.Common.PatternIchiran.UIForm(this.ShainCd, denshu.ToString());
            var headerForm = new Shougun.Core.Common.PatternIchiran.APP.UIHeader();
            var popForm = new BasePopForm(callForm, headerForm);
            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                popForm.ShowDialog();
            }

            string systemId = callForm.ParamOut_SysID;

            if (!string.IsNullOrEmpty(systemId))
            {
                if (!this.logic.KoumokuYobidasi(systemId)) { return; }
            }
            this.NyuryokuIkkatsuItiran.Rows.Add(); //hack:パターン未登録時にパターン登録すると例外　おそらくパターン変更に対応できていないことが原因。
            this.NyuryokuIkkatsuItiran.Rows.RemoveAt(0);
        }

        /// <summary>
        /// パターン呼出イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternYobidasi(object sender, EventArgs e)
        {
            // 20140529 syunrei No.730 マニフェストパターン一覧 start
            this.logic.CallPattern();


            //// 廃棄物区分CD
            //string haikiKbnCd = GetItiranCellValue(this.NyuryokuIkkatsuItiran.CurrentRow.Index,
            //    this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index);
            
            //string[] useInfo = new string[] {string.Empty};

            //var callForm = new Shougun.Core.PaperManifest.ManifestPattern.UIForm(DENSHU_KBN.MANIFEST_IKKATU, "1", haikiKbnCd);

            //var callHeader = new Shougun.Core.PaperManifest.ManifestPattern.UIHeader();
          
            //var businessForm = new BusinessBaseForm(callForm, callHeader);
            //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            //if (!isExistForm)
            //{
            //    businessForm.ShowDialog();
            //}

            //string systemId = callForm.ParamOut_SysID;

            //if (!string.IsNullOrEmpty(systemId))
            //{
            //    this.logic.PatternYobidasi(systemId);
            //}
            // 20140529 syunrei No.730 マニフェストパターン一覧 end
        }

        /// <summary>
        /// 登録イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Regist(object sender, EventArgs e)
        {
            if (this.logic.RegistCheck())
            {
                return;
            }
            this.logic.Regist();
            if (this.isRegistErr) { return; }
            this.NyuryokuIkkatsuItiran.Focus();
        }

        /// <summary>
        /// 車輛ポップアップ後設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetSharyou1PopUp()
        {
            this.logic.SetSharyouPopUp("1");
        }

        /// <summary>
        /// 車輛ポップアップ後設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetSharyou2PopUp()
        {
            this.logic.SetSharyouPopUp("2");
        }

        /// <summary>
        /// 車輛ポップアップ後設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetSharyou3PopUp()
        {
            this.logic.SetSharyouPopUp("3");
        }

        /// <summary>
        /// 車種ポップアップ後設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetShashu1PopUp()
        {
            this.logic.SetShashuPopUp("1");
        }

        /// <summary>
        /// 車種ポップアップ後設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetShashu2PopUp()
        {
            this.logic.SetShashuPopUp("2");
        }

        /// <summary>
        /// 車種ポップアップ後設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetShashu3PopUp()
        {
            this.logic.SetShashuPopUp("3");
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormClose(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        //編集前の値を取得
        private void NyuryokuIkkatsuItiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            BeforeCellValue = String.Empty;

            BeforeRowIndex = e.RowIndex;

            BeforeColumnIndex = e.ColumnIndex;

            if (this.NyuryokuIkkatsuItiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                BeforeCellValue = string.Empty;
            }
            else
            {
                BeforeCellValue = this.NyuryokuIkkatsuItiran.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        /// <summary>
        /// CellValidating ロストフォーカスチェック(該当セルに値がある場合)
        /// </summary>
        public bool LostFocusCheck(DataGridViewCellValidatingEventArgs e)
        {

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            int columnIndex = 0;
            int rowIndex = e.RowIndex;
            string cellValue = GetItiranCellValue(rowIndex, e.ColumnIndex);
            string headerName = this.NyuryokuIkkatsuItiran.Columns[e.ColumnIndex].Name;
            bool catchErr = false;

            // マニフェスト種類CD/廃棄物区分CD
            if (ConstCls.MANIFEST_SHURUI_CD.Equals(headerName))
            {

                // [1]か[2]か[3]以外の数字が入力された場合
                if (!"1".Equals(cellValue) && !"2".Equals(cellValue) && !"3".Equals(cellValue))
                {
                    // マニフェスト種類CDを空白
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;

                    // マニフェスト種類名称を空白
                    if (this.TitleList.Contains(ConstCls.MANIFEST_SHURUI_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    }

                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "1～3");
                    return false;
                }

                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                // 産廃（直行）：[1]が入力された場合
                if ("1".Equals(cellValue))
                {
                    //マニフェスト種類CDの制御
                    ShuruiCdSeigyo("1");

                    // マニフェスト種類名称
                    if (this.TitleList.Contains(ConstCls.MANIFEST_SHURUI_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "直行";
                    }
                }
                // 建廃：[2]が入力された場合
                else if ("2".Equals(cellValue))
                {
                    //マニフェスト種類CDの制御
                    ShuruiCdSeigyo("2");

                    // マニフェスト種類名称
                    if (this.TitleList.Contains(ConstCls.MANIFEST_SHURUI_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "建廃";
                    }
                }
                // 産廃（積替）：[3]が入力された場合
                else if ("3".Equals(cellValue))
                {
                    //マニフェスト種類CDの制御
                    ShuruiCdSeigyo("3");

                    // マニフェスト種類名称
                    if (this.TitleList.Contains(ConstCls.MANIFEST_SHURUI_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "積替";
                    }
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return false; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            // 一次二次区分/一次マニフェスト区分
            else if (ConstCls.FIRST_SECOND_KBN.Equals(headerName))
            {
                // [1]か[2]以外の数字が入力された場合
                if (!"1".Equals(cellValue) && !"2".Equals(cellValue))
                {
                    // 一次二次区分CD
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;

                    // 一次二次区分名称
                    if (this.TitleList.Contains(ConstCls.FIRST_SECOND_KBN_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.FIRST_SECOND_KBN_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    }

                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "1～2");
                    return false;
                }

                // [1]が入力された場合
                if ("1".Equals(cellValue))
                {
                    // 一次二次区分名称
                    if (this.TitleList.Contains(ConstCls.FIRST_SECOND_KBN_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.FIRST_SECOND_KBN_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "一次";
                    }
                }
                // [2]が入力された場合
                else if ("2".Equals(cellValue))
                {
                    // 一次二次区分名称
                    if (this.TitleList.Contains(ConstCls.FIRST_SECOND_KBN_NAME))
                    {
                        columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.FIRST_SECOND_KBN_NAME].Index;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "二次";
                    }
                }
            }

            // 取引先CD
            else if (ConstCls.TORIHIKISAKI_CD.Equals(headerName))
            {
                if (!string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.TORIHIKISAKI_CD].Index))
                    && !this.logic.ChkTorihikisaki(out catchErr))
                {
                    if (!catchErr)
                    {
                        msgLogic.MessageBoxShow("E020", "取引先");
                    }
                    return false;
                }
                else if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.TORIHIKISAKI_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_NAME].Value = null;
                }
            }

            // 交付年月日
            else if (ConstCls.KOUFU_DATE.Equals(headerName))
            {
                if (!this.TitleList.Contains(ConstCls.UPN_END_DATE_1))
                {
                    return true;
                }

                // 運搬終了年月日(区間1)
                columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_1].Index;
                string upnEndDate = GetItiranCellValue(rowIndex, columnIndex);
                // 交付年月日>運搬終了年月日(区間1)の場合
                if (!string.Empty.Equals(upnEndDate) && cellValue.CompareTo(upnEndDate) > 0)
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E049", "交付年月日", "運搬終了年月日(区間1)");
                    return false;
                }
            }

            // 交付番号区分
            else if (ConstCls.KOUFU_KBN.Equals(headerName))
            {
                if (!this.TitleList.Contains(ConstCls.MANIFEST_ID))
                {
                    return true;
                }

                if (!"1".Equals(cellValue) && !"2".Equals(cellValue))
                {
                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "1～2");
                    return false;
                }


                //交付番号を入力可能にする
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].ReadOnly = false;

                // 交付番号
                columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_ID].Index;
                string manifestId = GetItiranCellValue(rowIndex, columnIndex);

                if (string.Empty.Equals(manifestId))
                {
                    return true;
                }

                // 交付番号区分が「1:通常」の場合
                if ("1".Equals(cellValue))
                {

                    string msg = Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic.ChkKoufuNo(manifestId, true);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        //エラーメッセージ表示
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg);
                        return false;
                    }

                }
                // 交付番号区分が「2:例外」の場合
                if ("2".Equals(cellValue))
                {
                    //大文字変換
                    manifestId = manifestId.ToUpper();
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].Value = manifestId;


                    string msg = Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic.ChkKoufuNo(manifestId, false);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        //エラーメッセージ表示
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg);
                        return false;
                    }

                }

            }

            // 交付番号
            else if (ConstCls.MANIFEST_ID.Equals(headerName))
            {
                //項目が無ければ、処理しない
                if (!this.TitleList.Contains(ConstCls.KOUFU_KBN))
                {
                    return true;
                }

                // 交付番号区分
                string koufuKbn = GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.KOUFU_KBN].Index);

                // 交付番号区分が「1:通常」の場合
                if ("1".Equals(koufuKbn))
                {

                    string msg = Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic.ChkKoufuNo(cellValue, true);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        //エラーメッセージ表示
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg);
                        return false;
                    }

                }
                // 交付番号区分が「2:例外」の場合
                if ("2".Equals(koufuKbn))
                {
                    //大文字変換
                    cellValue = cellValue.ToUpper();
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].Value = cellValue;


                    string msg = Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic.ChkKoufuNo(cellValue, false);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        //エラーメッセージ表示
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg);
                        return false;
                    }

                }
            }

            // 排出事業者CD
            else if (ConstCls.HST_GYOUSHA_CD.Equals(headerName))
            {
                return HstGyoushaCDPopupAfter();
            }

            // 排出事業場CD
            else if (ConstCls.HST_GENBA_CD.Equals(headerName))
            {
                return HstGenbaCDPopupAfter();
            }

            // 廃棄物区分CD
            else if (ConstCls.MANIFEST_SHURUI_CD.Equals(headerName))
            {
                // [1]か[2]以外の数字が入力された場合
                if (!"1".Equals(cellValue) && !"2".Equals(cellValue))
                {
                    // 廃棄物区分CD
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;

                    // 廃棄物区分名称
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;

                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "1～2");
                    return false;
                }

                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                // [1]が入力された場合
                if ("1".Equals(cellValue))
                {
                    // 廃棄物区分名称
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "普通";
                }
                // [2]が入力された場合
                else if ("2".Equals(cellValue))
                {
                    // 廃棄物区分名称
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_NAME].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "特管";
                }
            }

            // 廃棄物種類CD
            else if (ConstCls.HAIKI_SHURUI_CD_RYAKU.Equals(headerName))
            {
                //入力不正
                if (!string.Empty.Equals(this.GetItiranCellValue(
                    rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.HAIKI_SHURUI_CD_RYAKU].Index)) && !this.logic.ChkGridHaiki(out catchErr))
                {
                    if (!catchErr)
                    {
                        msgLogic.MessageBoxShow("E020", "廃棄物種類");
                    }
                    return false;
                }

                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return false; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            //廃棄物の名称CD
            else if (ConstCls.HAIKI_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return false; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            // 荷姿CD
            else if (ConstCls.NISUGATA_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return false; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            // 数量
            else if (ConstCls.HAIKI_SUU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                switch (this.logic.ChkGridSuryo(rowIndex))
                {
                    case 0://正常
                        break;

                    case 1://空
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value = (0).ToString(this.logic.ManifestSuuryoFormat);
                        return true;

                    case 2://エラー
                        return false;

                }
                //変換出来たら、dにその数値が入る
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value
                    = Convert.ToDecimal(this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HAIKI_SUU].Value).ToString(this.logic.ManifestSuuryoFormat);

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return false; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            // 単位CD
            else if (ConstCls.UNIT_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return false; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            // 処分方法CD
            else if (ConstCls.SHOBUN_HOUHOU_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return true;
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return false; }
                }
            }

            //中間処理産業廃棄物
            //最終処分の場所
            //運搬受託者(区間1)
            //運搬受託者(区間2)
            //運搬受託者(区間3)
            //処分受託者
            //運搬先の事業場(区間1)
            //運搬先の事業場(区間2)
            //運搬先の事業場(区間3)
            //積替え又は保管
            //運搬の受託(区間1)
            //運搬の受託(区間2)
            //運搬の受託(区間3)
            //処分の受託
            //最終処分を行った場所

            // 最終処分の場所（予定）業者CD
            else if (ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD.Equals(headerName))
            {
                return LastSbnYoteiGyoushaCDPopupAfter();
            }
            // 最終処分業者CD
            else if (ConstCls.LAST_SBN_GYOUSHA_CD == headerName)
            {
                this.LastSbnGyoushaCDPopupAfter();

                var colIdx = this.NyuryokuIkkatsuItiran.Columns[headerName].Index;
                var cellVal = this.GetItiranCellValue(rowIndex, colIdx);
                if (!string.IsNullOrEmpty(cellVal) && !this.logic.ChkGyousha(headerName, out catchErr))
                {
                    if (!catchErr)
                    {
                        msgLogic.MessageBoxShow("E020", "業者");
                    }
                    return false;
                }
                else if (string.IsNullOrEmpty(cellVal))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[colIdx + 1].Value = null;
                }
            }
            // 最終処分の場所（予定）現場CD
            else if (ConstCls.LAST_SBN_YOTEI_GENBA_CD.Equals(headerName))
            {
                return LastSbnYoteiGenbaCDPopupAfter();
            }
            // 最終処分現場CD
            else if (ConstCls.LAST_SBN_GENBA_CD == headerName)
            {
                if (!string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[headerName].Index))
                    && !this.logic.ChkGenba(headerName, out catchErr))
                {
                    if (!catchErr)
                    {
                    msgLogic.MessageBoxShow("E020", "現場");
                        }
                    return false;
                }
                else if (this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GYOUSHA_CD].Value == null)
                {
                    msgLogic.MessageBoxShow("E034", "最終処分業者");
                    return false;
                }
                else if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[headerName].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[headerName].Value = null;
                    if (ConstCls.LAST_SBN_YOTEI_GENBA_CD == headerName)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Value = null;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_POST].Value = null;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_TEL].Value = null;
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS].Value = null;
                    }
                }
            }
            // 処分受託者CD
            else if (ConstCls.SBN_GYOUSHA_CD.Equals(headerName))
            {
                return SbnGyoushaCDPopupAfter();
            }
            //運搬先事業場
            else if (ConstCls.UPN_SAKI_GENBA_CD_1.Equals(headerName))
            {
                return UPN_SAKI_GENBA_CD_1PopupAfter(this.NyuryokuIkkatsuItiran[e.ColumnIndex,e.RowIndex]);
            }
            // 処分の受領者CD
            else if (ConstCls.SBN_JYURYOUSHA_CD.Equals(headerName))
            {
                return SbnJyuryoushaCDPopupAfter();
            }
            // 処分の受託者CD
            else if (ConstCls.SBN_JYUTAKUSHA_CD.Equals(headerName))
            {
                return SbnJyutakushaCDPopupAfter();
            }
            // 区間1：運搬受託者CD
            else if (ConstCls.UPN_GYOUSHA_CD_1.Equals(headerName))
            {
                return UpnGyoushaCD1PopupAfter();
            }
            // 区間2：運搬受託者CD
            else if (ConstCls.UPN_GYOUSHA_CD_2.Equals(headerName))
            {
                return UpnGyoushaCD2PopupAfter();
            }
            // 区間3：運搬受託者CD
            else if (ConstCls.UPN_GYOUSHA_CD_3.Equals(headerName))
            {
                return UpnGyoushaCD3PopupAfter();
            }
            // 区間1：車輌CD
            else if (ConstCls.SHARYOU_CD_1.Equals(headerName))
            {
                if (!string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_1].Index))
                    && !this.logic.ChkSharyou(ConstCls.SHARYOU_CD_1, out catchErr))
                {
                    if (!catchErr)
                    {
                        msgLogic.MessageBoxShow("E020", "車輌");
                    }
                    return false;
                }
                else if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_1].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                }
            }
            // 区間2：車輌CD
            else if (ConstCls.SHARYOU_CD_2.Equals(headerName))
            {
                if (!string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_2].Index))
                    && !this.logic.ChkSharyou(ConstCls.SHARYOU_CD_2, out catchErr))
                {
                    if (!catchErr)
                    {
                        msgLogic.MessageBoxShow("E020", "車輌");
                    }
                    return false;
                }
                else if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_2].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                }
            }
            // 区間3：車輌CD
            else if (ConstCls.SHARYOU_CD_3.Equals(headerName))
            {
                if (!string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_3].Index))
                    && !this.logic.ChkSharyou(ConstCls.SHARYOU_CD_3, out catchErr))
                {
                    if (!catchErr)
                    {
                        msgLogic.MessageBoxShow("E020", "車輌");
                    }
                    return false;
                }
                else if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_3].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                }
            }
            // 運搬先区分１～３
            else if (ConstCls.UPN_SAKI_KBN_1.Equals(headerName)
                || ConstCls.UPN_SAKI_KBN_2.Equals(headerName)
                || ConstCls.UPN_SAKI_KBN_3.Equals(headerName))
            {

                if (this.TitleList.Contains(ConstCls.MANIFEST_SHURUI_CD))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index;
                }

                string manifestShuruiCd = GetItiranCellValue(rowIndex, columnIndex);

                if ("1".Equals(manifestShuruiCd) || "2".Equals(manifestShuruiCd)) //1と2の時は、固定になるのでここは基本通らないはず
                {
                    // [1]か[2]以外の数字が入力された場合
                    if (!"1".Equals(cellValue))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                        if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = null;
                        }
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E048", "1");
                        return false;
                    }
                    else
                    {
                        if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "処分施設";
                        }
                    }
                }
                else // if ("3".Equals(manifestShuruiCd)) 未入力時は、一旦1と2の両方に対応
                {
                    // [1]か[2]以外の数字が入力された場合
                    if (!"1".Equals(cellValue) && !"2".Equals(cellValue))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                        if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = null;
                        }
                        if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_2))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_2].Value = null;
                        }
                        if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_3))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_3].Value = null;
                        }
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E048", "1～2");
                        return false;
                    }
                    else if ("1".Equals(cellValue))
                    {
                        if (ConstCls.UPN_SAKI_KBN_1.Equals(headerName) && this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "処分施設";
                            //業者が入っている場合 積替になっているので一旦クリアする
                            if (this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GYOUSHA_CD_1, rowIndex].Value != null &&
                               !string.IsNullOrEmpty(this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GYOUSHA_CD_1, rowIndex].Value.ToString()))
                            {
                                //現場クリア
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GYOUSHA_CD_1, rowIndex].Value = null; //業者も非表示なので連動して消す
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_CD_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_NAME_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_POST_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_TEL_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_ADDRESS_1, rowIndex].Value = null;
                            }
                            else
                            {
                            }
                        }
                        if (ConstCls.UPN_SAKI_KBN_2.Equals(headerName) && this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_2))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_2].Value = "処分施設";
                        }
                        if (ConstCls.UPN_SAKI_KBN_3.Equals(headerName) && this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_3))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_3].Value = "処分施設";
                        }
                    }
                    else
                    {
                        if (ConstCls.UPN_SAKI_KBN_1.Equals(headerName) && this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "積替保管";
                            //業者が入っている場合 積替になっているのでそのまま
                            if (this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GYOUSHA_CD_1, rowIndex].Value != null &&
                                !string.IsNullOrEmpty(this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GYOUSHA_CD_1, rowIndex].Value.ToString()))
                            {
                            }
                            else
                            {
                                //現場クリア
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GYOUSHA_CD_1, rowIndex].Value = null; //業者も非表示なので連動して消す
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_CD_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_NAME_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_POST_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_TEL_1, rowIndex].Value = null;
                                this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_GENBA_ADDRESS_1, rowIndex].Value = null;

                            }

                        }
                        if (ConstCls.UPN_SAKI_KBN_2.Equals(headerName) && this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_2))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_2].Value = "積替保管";
                        }
                        if (ConstCls.UPN_SAKI_KBN_3.Equals(headerName) && this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_3))
                        {
                            this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_3].Value = "積替保管";
                        }
                    }
                }
            }
            // 区間1：運搬終了年月日
            else if (ConstCls.UPN_END_DATE_1.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.KOUFU_DATE))
                {
                    // 交付年月日
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KOUFU_DATE].Index;
                    string koufuDate = GetItiranCellValue(rowIndex, columnIndex);

                    // 交付年月日>運搬終了年月日(区間1)の場合
                    if (koufuDate.CompareTo(cellValue) > 0)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E030", "運搬終了年月日(区間1)", "交付年月日");
                        return false;
                    }
                }

                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_2))
                {
                    // 運搬終了年月日(区間2)
                    int columnIndex1 = this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_2].Index;
                    string upnEndDate2 = GetItiranCellValue(rowIndex, columnIndex1);

                    // 運搬終了年月日(区間1)>運搬終了年月日(区間2)の場合
                    if (!string.Empty.Equals(upnEndDate2) && cellValue.CompareTo(upnEndDate2) > 0)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E049", "運搬終了年月日(区間1)", "運搬終了年月日(区間2)");
                        return false;
                    }
                }
            }

            // 区間2：運搬終了年月日
            else if (ConstCls.UPN_END_DATE_2.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_1))
                {
                    // 運搬終了年月日(区間1)
                    int columnIndex1 = this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_1].Index;
                    string upnEndDate1 = GetItiranCellValue(rowIndex, columnIndex1);

                    // 運搬終了年月日(区間1)>運搬終了年月日(区間2)の場合
                    if (upnEndDate1.CompareTo(cellValue) > 0)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E030", "運搬終了年月日(区間2)", "運搬終了年月日(区間1)");
                        return false;
                    }
                }

                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_3))
                {
                    // 運搬終了年月日(区間3)
                    int columnIndex3 = this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_3].Index;
                    string upnEndDate3 = GetItiranCellValue(rowIndex, columnIndex3);

                    // 運搬終了年月日(区間2)>運搬終了年月日(区間3)の場合
                    if (!string.Empty.Equals(upnEndDate3) && cellValue.CompareTo(upnEndDate3) > 0)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E049", "運搬終了年月日(区間2)", "運搬終了年月日(区間3)");
                        return false;
                    }
                }
            }

            // 区間3：運搬終了年月日
            else if (ConstCls.UPN_END_DATE_3.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_2))
                {
                    // 運搬終了年月日(区間2)
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_2].Index;
                    string upnEndDate2 = GetItiranCellValue(rowIndex, columnIndex);

                    // 運搬終了年月日(区間2)>運搬終了年月日(区間3)の場合
                    if (upnEndDate2.CompareTo(cellValue) > 0)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E030", "運搬終了年月日(区間3)", "運搬終了年月日(区間2)");
                        return false;
                    }
                }

                if (this.TitleList.Contains(ConstCls.SBN_END_DATE))
                {
                    // 処分終了日
                    int columnIndex1 = this.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_END_DATE].Index;
                    string sbnEndDate = GetItiranCellValue(rowIndex, columnIndex1);

                    // 運搬終了年月日(区間3)>処分終了年月日の場合
                    if (!string.Empty.Equals(sbnEndDate) && cellValue.CompareTo(sbnEndDate) > 0)
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                        //エラーメッセージ表示
                        msgLogic.MessageBoxShow("E049", "運搬終了年月日(区間3)", "処分終了年月日");
                        return false;
                    }
                }
            }

            // 処分終了日
            else if (ConstCls.SBN_END_DATE.Equals(headerName))
            {
                string upnEndDate = string.Empty;
                string msg = string.Empty;

                // 運搬終了年月日(区間1)
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_1) && !string.Empty.Equals(
                    this.GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_1].Index)))
                {
                    msg = "運搬終了年月日(区間1)";
                    upnEndDate = this.GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_1].Index);
                }

                // 運搬終了年月日(区間2)
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_2) && !string.Empty.Equals(
                    this.GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_2].Index)))
                {
                    msg = "運搬終了年月日(区間2)";
                    upnEndDate = this.GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_2].Index);
                }

                // 運搬終了年月日(区間3)
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_3) && !string.Empty.Equals(
                    this.GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_3].Index)))
                {
                    msg = "運搬終了年月日(区間3)";
                    upnEndDate = this.GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_END_DATE_3].Index);
                }

                // 運搬終了年月日(区間)>処分終了年月日の場合
                if (upnEndDate.CompareTo(cellValue) > 0)
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = null;
                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E030", "処分終了年月日", msg);
                    return false;
                }
            }

            // 区間1：積替保管有無
            else if (ConstCls.TMH_KBN_1.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.TMH_KBN_NAME_1].Index;
                }

                // [1]か[2]以外の数字が入力された場合
                if (!"1".Equals(cellValue) && !"2".Equals(cellValue))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;

                    if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    }

                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "1～2");
                    return false;
                }
                // [1]が入力された場合
                else if ("1".Equals(cellValue))
                {
                    if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "有";
                    }
                }
                // [2]が入力された場合
                else if ("2".Equals(cellValue))
                {
                    if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "無";
                    }
                }
            }

            // 区間2：積替保管有無
            else if (ConstCls.TMH_KBN_2.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.TMH_KBN_NAME_2].Index;
                }

                // [1]か[2]以外の数字が入力された場合
                if (!"1".Equals(cellValue) && !"2".Equals(cellValue))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;

                    if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    }

                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "1～2");
                    return false;
                }
                // [1]が入力された場合
                else if ("1".Equals(cellValue))
                {
                    if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "有";
                    }
                }
                // [2]が入力された場合
                else if ("2".Equals(cellValue))
                {
                    if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = "無";
                    }
                }
            }

            // 最終処分の場所（予定）区分
            else if (ConstCls.LAST_SBN_YOTEI_KBN.Equals(headerName))
            {
                if (string.Empty.Equals(cellValue))
                {
                    return true;
                }
                // [0]、[1]か[2]以外の数字が入力された場合
                if (!"0".Equals(cellValue) && !"1".Equals(cellValue) && !"2".Equals(cellValue))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[e.ColumnIndex].Value = string.Empty;

                    //エラーメッセージ表示
                    msgLogic.MessageBoxShow("E048", "0～2");
                    return false;
                }
            }

            //積替保管業者
            else if (ConstCls.TMH_GYOUSHA_CD.Equals(headerName))
            {
                return TMH_GYOUSHA_CDPopupAfter(this.NyuryokuIkkatsuItiran[e.ColumnIndex,e.RowIndex]);
            }

            //積替保管現場
            else if (ConstCls.TMH_GENBA_CD.Equals(headerName))
            {
                return TMH_GENBA_CDPopupAfter(this.NyuryokuIkkatsuItiran[e.ColumnIndex, e.RowIndex]);
            }

            return true;
        }

        /// <summary>
        /// CellValidating ロストフォーカスチェック(該当セルが空白時)
        /// </summary>
        public void LostFocusInit(DataGridViewCellValidatingEventArgs e)
        {
            int columnIndex = 0;
            int rowIndex = e.RowIndex;
            string cellValue = GetItiranCellValue(rowIndex, e.ColumnIndex);
            string headerName = this.NyuryokuIkkatsuItiran.Columns[e.ColumnIndex].Name;

            // マニフェスト種類CD/廃棄物区分CD
            if (ConstCls.MANIFEST_SHURUI_CD.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.MANIFEST_SHURUI_NAME) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.MANIFEST_SHURUI_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_SHURUI_NAME].Value = null;
                }
                return;
            }
            // 一次二次区分/一次マニフェスト区分
            else if (ConstCls.FIRST_SECOND_KBN.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.FIRST_SECOND_KBN_NAME) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.FIRST_SECOND_KBN].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.FIRST_SECOND_KBN_NAME].Value = null;
                }
                return;
            }

            // 取引先CD
            else if (ConstCls.TORIHIKISAKI_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.TORIHIKISAKI_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TORIHIKISAKI_NAME].Value = null;
                }
                return;
            }

            // 交付区分
            else if (ConstCls.KOUFU_KBN.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.KOUFU_KBN) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.KOUFU_KBN].Index)))
                {
                    //交付番号を入力不可にしてクリアする
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].ReadOnly = true;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.MANIFEST_ID].Value = "";
                }
                return;
            }

            // 排出事業者CD
            else if (ConstCls.HST_GYOUSHA_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GYOUSHA_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_NAME].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_POST].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_TEL].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GYOUSHA_ADDRESS].Value = null;

                    if (this.NyuryokuIkkatsuItiran.Columns.Contains(ConstCls.HST_GENBA_CD))
                    {
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value = string.Empty;
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_NAME].Value = string.Empty;
                    }
                }
                return;
            }

            // 排出事業場CD
            else if (ConstCls.HST_GENBA_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.HST_GENBA_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_NAME].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_POST].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_TEL].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.HST_GENBA_ADDRESS].Value = null;
                }
                return;
            }

            //実績
            // 廃棄物種類CD
            else if (ConstCls.HAIKI_SHURUI_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return; }
                }

                return;
            }

            // 廃棄物の名称CD
            if (ConstCls.HAIKI_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return; }
                }

                return;
            }

            //荷姿CD
            else if (ConstCls.NISUGATA_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return; }
                }

                return;
            }

            //単位CD
            else if (ConstCls.UNIT_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return;
                }

                //換算後数量の算出
                if (this.TitleList.Contains(ConstCls.KANSAN_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.KANSAN_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectKansanData(rowIndex)) { return; }
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return; }
                }

                return;
            }

            //処分方法CD
            else if (ConstCls.SHOBUN_HOUHOU_CD_RYAKU.Equals(headerName))
            {
                //同じ値は処理しない。
                if (cellValue == BeforeCellValue)
                {
                    return;
                }

                //減容後数量の算出
                if (this.TitleList.Contains(ConstCls.GENNYOU_SUU))
                {
                    columnIndex = this.NyuryokuIkkatsuItiran.Columns[ConstCls.GENNYOU_SUU].Index;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value = string.Empty;
                    if (!this.logic.SelectGenyouData(rowIndex)) { return; }
                }

                return;
            }

            //中間処理産業廃棄物
            //最終処分の場所
            //運搬受託者(区間1)
            //運搬受託者(区間2)
            //運搬受託者(区間3)
            //処分受託者
            //運搬先の事業場(区間1)
            //運搬先の事業場(区間2)
            //運搬先の事業場(区間3)
            //積替え又は保管
            //運搬の受託(区間1)
            //運搬の受託(区間2)
            //運搬の受託(区間3)
            //処分の受託
            //最終処分を行った場所

            // 最終処分の場所（予定）現場CD
            else if (ConstCls.LAST_SBN_YOTEI_GENBA_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_POST].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_TEL].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS].Value = null;
                }
                return;
            }

            // 処分受託者CD
            else if (ConstCls.SBN_GYOUSHA_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_GYOUSHA_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_NAME].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_POST].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_TEL].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_GYOUSHA_ADDRESS].Value = null;

                    if (this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value != null)
                    {
                        switch (this.NyuryokuIkkatsuItiran[ConstCls.UPN_SAKI_KBN_1, rowIndex].Value.ToString())
                        {
                            case "1": //処分施設
                                //現場クリア
                                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_1].Value = null;
                                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value = null;
                                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_1].Value = null;
                                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_1].Value = null;
                                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_1].Value = null;

                                break;

                            case "2": //積替保管
                                //何もしない
                                break;
                        }
                    }
                }
                return;
            }

            // 処分の受領者CD
            else if (ConstCls.SBN_JYURYOUSHA_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_JYURYOUSHA_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value = null;
                }
                return;
            }

            // 処分の受託者CD
            else if (ConstCls.SBN_JYUTAKUSHA_CD.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SBN_JYUTAKUSHA_CD].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYUTAKUSHA_NAME].Value = null;
                }
                return;
            }

            // 区間1：運搬受託者CD
            else if (ConstCls.UPN_GYOUSHA_CD_1.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_CD_1].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                }
                return;
            }

            // 区間2：運搬受託者CD
            else if (ConstCls.UPN_GYOUSHA_CD_2.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_CD_2].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                }
                return;
            }

            // 区間3：運搬受託者CD
            else if (ConstCls.UPN_GYOUSHA_CD_3.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_GYOUSHA_CD_3].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                }
                return;
            }

            // 区間1：車輌CD
            else if (ConstCls.SHARYOU_CD_1.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_1].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                }
                return;
            }

            // 区間2：車輌CD
            else if (ConstCls.SHARYOU_CD_2.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_2].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                }
                return;
            }

            // 区間3：車輌CD
            else if (ConstCls.SHARYOU_CD_3.Equals(headerName))
            {
                if (string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.SHARYOU_CD_3].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                }
                return;
            }

            // 運搬先区分１
            else if (ConstCls.UPN_SAKI_KBN_1.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_KBN_1].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = null;
                }
                return;
            }

            // 運搬先区分２
            else if (ConstCls.UPN_SAKI_KBN_2.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_2) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_KBN_2].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_2].Value = null;
                }
                return;
            }
            // 運搬先区分３
            else if (ConstCls.UPN_SAKI_KBN_3.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_3) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.UPN_SAKI_KBN_3].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_3].Value = null;
                }
                return;
            }
            // 区間1：積替保管有無
            else if (ConstCls.TMH_KBN_1.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.TMH_KBN_1].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_1].Value = null;
                }
                return;
            }

            // 区間2：積替保管有無
            else if (ConstCls.TMH_KBN_2.Equals(headerName))
            {
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2) &&
                    string.Empty.Equals(GetItiranCellValue(rowIndex, this.NyuryokuIkkatsuItiran.Columns[ConstCls.TMH_KBN_2].Index)))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_2].Value = null;
                }
                return;
            }
            else if (headerName == ConstCls.LAST_SBN_GYOUSHA_CD)
            {
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GYOUSHA_NAME].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_CD].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_NAME].Value = null;
                return;
            }
            else if (headerName == ConstCls.LAST_SBN_GENBA_CD)
            {
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.LAST_SBN_GENBA_NAME].Value = null;
                return;
            }
            //区間1：運搬先現場
            else if (ConstCls.UPN_SAKI_GENBA_CD_1.Equals(headerName))
            {
                //現場クリア
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_1].Value = null; //業者も非表示なので連動して消す
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_1].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_1].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_1].Value = null;
                return;
            }
            //積替保管業者
            else if (ConstCls.TMH_GYOUSHA_CD.Equals(headerName))
            {
                //業者クリア
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GYOUSHA_CD].Value = null; //業者も非表示なので連動して消す
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GYOUSHA_NAME].Value = null;
                //現場クリア
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_CD].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_NAME].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_POST].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_TEL].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_ADDRESS].Value = null;
                return;
            }
            //積替保管現場
            else if (ConstCls.TMH_GENBA_CD.Equals(headerName))
            {
                //現場クリア
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_CD].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_NAME].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_POST].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_TEL].Value = null;
                this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_GENBA_ADDRESS].Value = null;
                return;
            }
        }

        /// <summary>
        /// 選択されるCell内容を取得
        /// </summary>
        internal string GetItiranCellValue(int rowIndex, int columnIndex)
        {
            string cellValue = null;

            if (this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value == null)
            {
                cellValue = string.Empty;
            }
            else
            {
                cellValue = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Value.ToString();
            }
            return cellValue;
        }

        /// <summary>
        /// 排出事業者CD PopupAfter
        /// </summary>
        public Boolean HstGyoushaCDPopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool catchErr = false;
            string currentVal = string.Empty;
            if (this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_CD].Value != null)
            {
                currentVal = this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
            }
            // 変更があれば、排出事業場情報を初期化する
            if ((currentVal != this.BeforeCellValue || string.IsNullOrEmpty(currentVal))
                && this.NyuryokuIkkatsuItiran.Columns.Contains(ConstCls.HST_GENBA_CD))
            {
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value = string.Empty;
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_NAME].Value = string.Empty;
            }

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        ConstCls.HST_GYOUSHA_CD, // "排出事業者CD",
                        ConstCls.HST_GYOUSHA_NAME, // "排出事業者名称",
                        null,
                        ConstCls.HST_GYOUSHA_POST, // "排出事業者郵便番号",
                        ConstCls.HST_GYOUSHA_TEL, // "排出事業者電話番号",
                        ConstCls.HST_GYOUSHA_ADDRESS)) // "排出事業者住所"))
            {
                case 0://正常
                    if(!this.logic.ChkGyousha(out catchErr))
                    {
                        if (!catchErr)
                        {
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_CD].Value = String.Empty;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_NAME].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_POST].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_TEL].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_ADDRESS].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value = String.Empty;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_NAME].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_POST].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_TEL].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_ADDRESS].Value = null;
                            msgLogic.MessageBoxShow("E020", "業者");
                        }
                        return false;
                    }
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    //this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GYOUSHA_CD].Value = String.Empty;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value = String.Empty;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_NAME].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_POST].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_TEL].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_ADDRESS].Value = null;
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;

            }
            return true;
        }

        /// <summary>
        /// 排出現場CD
        /// </summary>
        public Boolean HstGenbaCDPopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool catchErr = false;
            switch (this.mlogic.SetAddressGenbaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        ConstCls.HST_GYOUSHA_CD, // "排出事業者CD",
                        ConstCls.HST_GENBA_CD, // "排出事業場CD",
                        ConstCls.HST_GENBA_NAME, // "排出事業場名称",                     
                        null,
                        ConstCls.HST_GENBA_POST, // "排出事業場郵便番号",
                        ConstCls.HST_GENBA_TEL, // "排出事業場電話番号",
                        ConstCls.HST_GENBA_ADDRESS)) // "排出事業場住所"))
            {
                case 0://正常
                    if (!this.logic.ChkGenba(out catchErr))
                    {
                        if (!catchErr)
                        {
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value = String.Empty;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_NAME].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_POST].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_TEL].Value = null;
                            this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_ADDRESS].Value = null;
                            msgLogic.MessageBoxShow("E020", "現場");
                        }
                        return false;
                    }
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.HST_GENBA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;

                default: //業者が複数マッチ
                    msgLogic.MessageBoxShow("E034", "業者");
                    return false;
            }
            //this.HstGyoushaCDPopupAfter();
            return true;
        }

        /// <summary>
        /// 最終処分の場所（予定）業者CD
        /// </summary>
        public Boolean LastSbnYoteiGyoushaCDPopupAfter()
        {

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            string currentVal = string.Empty;
            if (this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value != null)
            {
                currentVal = this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
            }
            
            // 変更があれば、排出事業場情報を初期化する
            if ((currentVal != this.BeforeCellValue || string.IsNullOrEmpty(currentVal))
                && this.NyuryokuIkkatsuItiran.Columns.Contains(ConstCls.LAST_SBN_YOTEI_GENBA_CD))
            {
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Value = string.Empty;
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Value = string.Empty;
            }

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD, //"最終処分の場所（予定）業者CD",
                        null,
                        null,
                        null,
                        null,
                        null))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_KBN].Value = "2";
                    break;

                case 1://空白
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_KBN].Value = "2";
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Value = String.Empty;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_NAME].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_POST].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_TEL].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS].Value = null;
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 最終処分の場所（予定）現場CD
        /// </summary>
        public Boolean LastSbnYoteiGenbaCDPopupAfter()
        {

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            switch (this.mlogic.SetAddressGenbaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        ConstCls.LAST_SBN_YOTEI_GYOUSHA_CD, // "最終処分の場所（予定）業者CD",
                        ConstCls.LAST_SBN_YOTEI_GENBA_CD, // "最終処分の場所（予定）現場CD",
                        ConstCls.LAST_SBN_YOTEI_GENBA_NAME, // "最終処分の場所（予定）現場名称",
                        null,
                        ConstCls.LAST_SBN_YOTEI_GENBA_POST, // "最終処分の場所（予定）郵便番号",
                        ConstCls.LAST_SBN_YOTEI_GENBA_TEL, // "最終処分の場所（予定）電話番号",
                        ConstCls.LAST_SBN_YOTEI_GENBA_ADDRESS)) // "最終処分の場所（予定）住所"))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_YOTEI_GENBA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;

                default: //業者が複数マッチ
                    msgLogic.MessageBoxShow("E034", "業者");
                    return false;
            }
            //LastSbnYoteiGyoushaCDPopupAfter();
            return true;
        }

        /// <summary>
        /// 最終処分業者CD
        /// </summary>
        /// <returns></returns>
        public Boolean LastSbnGyoushaCDPopupAfter()
        {
            string currentVal = string.Empty;
            if (this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_GYOUSHA_CD].Value != null)
            {
                currentVal = this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
            }
            
            // 変更があれば、最終処分現場情報を初期化する
            if ((currentVal != this.BeforeCellValue || string.IsNullOrEmpty(currentVal))
                && this.NyuryokuIkkatsuItiran.Columns.Contains(ConstCls.LAST_SBN_GENBA_CD))
            {
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_GENBA_CD].Value = string.Empty;
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.LAST_SBN_GENBA_NAME].Value = string.Empty;
            }

            return true;
        }

        // 処分受託者CD
        public Boolean SbnGyoushaCDPopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        "All" ,
                        ConstCls.SBN_GYOUSHA_CD, // "処分受託者CD",
                        ConstCls.SBN_GYOUSHA_NAME, // "処分受託者名称",
                        null,
                        ConstCls.SBN_GYOUSHA_POST, // "処分受託者郵便番号",
                        ConstCls.SBN_GYOUSHA_TEL, // "処分受託者電話番号",
                        ConstCls.SBN_GYOUSHA_ADDRESS, // "処分受託者住所"))
                        "Part1",
                        ConstCls.SBN_JYURYOUSHA_CD,
                        ConstCls.SBN_JYURYOUSHA_NAME))
            {

                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.SBN_GYOUSHA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.SBN_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;
        }

        // 処分の受領者CD
        public Boolean SbnJyuryoushaCDPopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        "Part1",
                        ConstCls.SBN_JYURYOUSHA_CD, // "処分の受領者CD",
                        ConstCls.SBN_JYURYOUSHA_NAME, // "処分の受領者名称",
                        null,
                        null,
                        null,
                        null,
                        "",
                        null,
                        null))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;
        }

        // 処分の受託者CD
        //2014-03-14 Upd ogawamut No.3506
        public Boolean SbnJyutakushaCDPopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                        this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                        "Part1",
                        ConstCls.SBN_JYUTAKUSHA_CD, // "処分の受託者CD",
                        ConstCls.SBN_JYUTAKUSHA_NAME, // "処分の受託者名称",
                        null,
                        null,
                        null,
                        null,
                        "",
                        null,
                        null))
            {

                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.SBN_JYUTAKUSHA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.SBN_JYUTAKUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;

        }

        // 区間1：運搬受託者CD
        public Boolean UpnGyoushaCD1PopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            int rowIndex = this.NyuryokuIkkatsuItiran.CurrentRow.Index;

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                    this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                    "All",
                    ConstCls.UPN_GYOUSHA_CD_1, // "区間1：運搬受託者CD",
                    ConstCls.UPN_GYOUSHA_NAME_1, // "区間1：運搬受託者名称",
                    null,
                    ConstCls.UPN_GYOUSHA_POST_1, // "区間1：運搬受託者郵便番号",
                    ConstCls.UPN_GYOUSHA_TEL_1, // "区間1：運搬受託者電話番号",
                    ConstCls.UPN_GYOUSHA_ADDRESS_1,  // "区間1：運搬受託者住所"
                    "Part1",
                    ConstCls.UPN_JYUTAKUSHA_CD_1,
                    ConstCls.UPN_JYUTAKUSHA_NAME_1))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.UPN_GYOUSHA_CD_1].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.UPN_GYOUSHA_CD_1].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;

        }

        // 区間2：運搬受託者CD
        public Boolean UpnGyoushaCD2PopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            int rowIndex = this.NyuryokuIkkatsuItiran.CurrentRow.Index;

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                    this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                    "All",
                    ConstCls.UPN_GYOUSHA_CD_2, // "区間2：運搬受託者CD",
                    ConstCls.UPN_GYOUSHA_NAME_2, // "区間2：運搬受託者名称",
                    null,
                    ConstCls.UPN_GYOUSHA_POST_2, // "区間2：運搬受託者郵便番号",
                    ConstCls.UPN_GYOUSHA_TEL_2, // "区間2：運搬受託者電話番号",
                    ConstCls.UPN_GYOUSHA_ADDRESS_2,  // "区間2：運搬受託者住所"))
                    "Part1",
                    ConstCls.UPN_JYUTAKUSHA_CD_2,
                    ConstCls.UPN_JYUTAKUSHA_NAME_2))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;

        }

        // 区間3：運搬受託者CD
        public Boolean UpnGyoushaCD3PopupAfter()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            int rowIndex = this.NyuryokuIkkatsuItiran.CurrentRow.Index;

            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                    this.NyuryokuIkkatsuItiran.CurrentRow.Index,
                    "All",
                    ConstCls.UPN_GYOUSHA_CD_3, // "区間3：運搬受託者CD",
                    ConstCls.UPN_GYOUSHA_NAME_3, // "区間3：運搬受託者名称",
                    null,
                    ConstCls.UPN_GYOUSHA_POST_3, // "区間3：運搬受託者郵便番号",
                    ConstCls.UPN_GYOUSHA_TEL_3, // "区間3：運搬受託者電話番号",
                    ConstCls.UPN_GYOUSHA_ADDRESS_3,  // "区間3：運搬受託者住所"))
                    "Part1",
                    ConstCls.UPN_JYUTAKUSHA_CD_3,
                    ConstCls.UPN_JYUTAKUSHA_NAME_3))

            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }
            return true;

        }

        /// <summary>
        /// マニフェスト種類CDの制御
        /// </summary>
        private void ShuruiCdSeigyo(string shuruiCd)
        {
            int rowIndex = this.NyuryokuIkkatsuItiran.CurrentRow.Index;

            // マニフェスト種類CDが"1"の場合
            if ("1".Equals(shuruiCd))
            {
                // 処分の受領者CD
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOUSHA_CD))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].ReadOnly = true;
                }
                // 処分の受領者名称
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOUSHA_NAME))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value = null;
                }
                // 処分の受領担当者CD
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_TANTOU_CD))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].ReadOnly = true;
                }
                // 処分の受領担当者名
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_TANTOU_NAME))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_NAME].Value = null;
                }
                // 処分受領日
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_DATE))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_DATE].ReadOnly = true;
                }
                // 照合確認B1票
                if (this.TitleList.Contains(ConstCls.CHECK_B1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B1].ReadOnly = true;
                }
                // 照合確認B4票
                if (this.TitleList.Contains(ConstCls.CHECK_B4))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B4].ReadOnly = true;
                }
                // 照合確認B6票
                if (this.TitleList.Contains(ConstCls.CHECK_B6))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B6].ReadOnly = true;
                }
                // 区間1：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_1].ReadOnly = false;
                }
                // 区間1：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].ReadOnly = true;
                }
                // 区間1：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_1].Value = null;
                }
                // 区間1：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_1))
                {
                    //直行は1固定
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_1].ReadOnly = true;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_1].Value = "1";
                }
                // 区間1：運搬先区分名
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "処分施設";
                }

                // 区間2：運搬受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].ReadOnly = true;
                }

                // 区間2：運搬受託者名称
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_2].Value = null;
                }

                // 区間2：運搬受託者郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_POST_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].ReadOnly = true;
                }

                // 区間2：運搬受託者電話番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_TEL_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].ReadOnly = true;
                }

                // 区間2：運搬受託者住所
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_ADDRESS_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].ReadOnly = true;
                }

                // 区間2：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].ReadOnly = true;
                }

                // 区間2：運搬方法名
                if (this.TitleList.Contains(ConstCls.UNPAN_HOUHOU_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNPAN_HOUHOU_NAME_2].Value = null;
                }

                // 区間2：車種CD
                if (this.TitleList.Contains(ConstCls.SHASHU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].ReadOnly = true;
                }

                // 区間2：車種名
                if (this.TitleList.Contains(ConstCls.SHASHU_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_2].Value = null;
                }

                // 区間2：車輌CD
                if (this.TitleList.Contains(ConstCls.SHARYOU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].ReadOnly = true;
                }

                // 区間2：車輌名
                if (this.TitleList.Contains(ConstCls.SHARYOU_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                }

                // 区間2：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].ReadOnly = true;
                }

                // 区間2：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_2].Value = null;
                }

                // 区間2：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].ReadOnly = true;
                }

                // 区間2：運搬先区分名
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_2].Value = null;
                }

                // 区間2：運搬先の事業者CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GYOUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_2].ReadOnly = true;
                }

                // 区間2：運搬先の事業場CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_2].ReadOnly = true;
                }

                // 区間2：運搬先の事業場名称
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_2].Value = null;
                }

                // 区間2：運搬先の事業場郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_POST_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_2].ReadOnly = true;
                }

                // 区間2：運搬先の事業場電話番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_TEL_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_2].ReadOnly = true;
                }

                // 区間2：運搬先の事業場住所
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_ADDRESS_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].ReadOnly = true;
                }

                // 区間2：運搬の受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_2].ReadOnly = true;
                }

                // 区間2：運搬の受託者名称
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_2].Value = null;
                }

                // 区間2：運転者CD
                if (this.TitleList.Contains(ConstCls.UNTENSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_2].ReadOnly = true;
                }

                // 区間2：運転者名
                if (this.TitleList.Contains(ConstCls.UNTENSHA_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_2].Value = null;
                }

                // 区間2：運搬終了年月日
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_2].ReadOnly = true;
                }

                // 区間3：運搬受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬受託者名称
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value = null;
                }

                // 区間3：運搬受託者郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_POST_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].ReadOnly = true;
                }

                // 区間3：運搬受託者電話番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_TEL_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].ReadOnly = true;
                }

                // 区間3：運搬受託者住所
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_ADDRESS_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].ReadOnly = true;
                }

                // 区間3：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].ReadOnly = true;
                }

                // 区間3：運搬方法名
                if (this.TitleList.Contains(ConstCls.UNPAN_HOUHOU_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNPAN_HOUHOU_NAME_3].Value = null;
                }

                // 区間3：車種CD
                if (this.TitleList.Contains(ConstCls.SHASHU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].ReadOnly = true;
                }

                // 区間3：車種名
                if (this.TitleList.Contains(ConstCls.SHASHU_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_3].Value = null;
                }

                // 区間3：車輌CD
                if (this.TitleList.Contains(ConstCls.SHARYOU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].ReadOnly = true;
                }

                // 区間3：車輌名
                if (this.TitleList.Contains(ConstCls.SHARYOU_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                }

                // 区間3：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].ReadOnly = true;
                }

                // 区間3：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_3].Value = null;
                }

                // 区間3：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].ReadOnly = true;
                }

                // 区間3：運搬先区分名
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_3].Value = null;
                }

                // 区間3：運搬先の事業者CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GYOUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場名称
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_3].Value = null;
                }

                // 区間3：運搬先の事業場郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_POST_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場電話番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_TEL_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場住所
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_ADDRESS_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].ReadOnly = true;
                }

                // 区間3：運搬の受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬の受託者名称
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_3].Value = null;
                }

                // 区間3：運転者CD
                if (this.TitleList.Contains(ConstCls.UNTENSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運転者名
                if (this.TitleList.Contains(ConstCls.UNTENSHA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_3].Value = null;
                }

                // 区間3：運搬終了年月日
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_3].ReadOnly = true;
                }
            }
            // マニフェスト種類CDが"2"の場合
            else if ("2".Equals(shuruiCd))
            {
                // 処分の受領者CD
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOUSHA_CD))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].ReadOnly = false;
                }
                // 処分の受領担当者CD
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_TANTOU_CD))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].ReadOnly = false;
                }
                // 処分受領日
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_DATE))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_DATE].ReadOnly = false;
                }
                // 照合確認B1票
                if (this.TitleList.Contains(ConstCls.CHECK_B1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B1].ReadOnly = false;
                }
                // 照合確認B4票
                if (this.TitleList.Contains(ConstCls.CHECK_B4))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B4].ReadOnly = true;
                }
                // 照合確認B6票
                if (this.TitleList.Contains(ConstCls.CHECK_B6))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B6].ReadOnly = true;
                }
                // 区間1：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_1].ReadOnly = true;
                }
                // 区間1：運搬方法名
                if (this.TitleList.Contains(ConstCls.UNPAN_HOUHOU_NAME_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNPAN_HOUHOU_NAME_1].Value = null;
                }
                // 区間1：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].ReadOnly = false;
                }
                // 区間1：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_1))
                {
                    //産廃は1固定
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_1].ReadOnly = true;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_1].Value = "1";
                }
                // 区間1：運搬先区分名
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "処分施設";
                }
                // 区間2：運搬受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬受託者郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_POST_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].ReadOnly = false;
                }

                // 区間2：運搬受託者電話番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_TEL_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].ReadOnly = false;
                }

                // 区間2：運搬受託者住所
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_ADDRESS_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].ReadOnly = false;
                }

                // 区間2：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].ReadOnly = true;
                }

                // 区間2：運搬方法名
                if (this.TitleList.Contains(ConstCls.UNPAN_HOUHOU_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNPAN_HOUHOU_NAME_2].Value = null;
                }

                // 区間2：車種CD
                if (this.TitleList.Contains(ConstCls.SHASHU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].ReadOnly = false;
                }

                // 区間2：車輌CD
                if (this.TitleList.Contains(ConstCls.SHARYOU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].ReadOnly = false;
                }

                // 区間2：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].ReadOnly = false;
                }

                // 区間2：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].ReadOnly = true;
                }

                // 区間2：運搬先区分名
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_2].Value = null;
                }

                // 区間2：運搬先の事業者CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GYOUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_POST_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場電話番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_TEL_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場住所
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_ADDRESS_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].ReadOnly = false;
                }

                // 区間2：運搬の受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運転者CD
                if (this.TitleList.Contains(ConstCls.UNTENSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬終了年月日
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_2].ReadOnly = false;
                }

                // 区間3：運搬受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬受託者名称
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value = null;
                }

                // 区間3：運搬受託者郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_POST_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].ReadOnly = true;
                }

                // 区間3：運搬受託者電話番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_TEL_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].ReadOnly = true;
                }

                // 区間3：運搬受託者住所
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_ADDRESS_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].ReadOnly = true;
                }

                // 区間3：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].ReadOnly = true;
                }

                // 区間3：運搬方法名
                if (this.TitleList.Contains(ConstCls.UNPAN_HOUHOU_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNPAN_HOUHOU_NAME_3].Value = null;
                }

                // 区間3：車種CD
                if (this.TitleList.Contains(ConstCls.SHASHU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].ReadOnly = true;
                }

                // 区間3：車種名
                if (this.TitleList.Contains(ConstCls.SHASHU_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_NAME_3].Value = null;
                }

                // 区間3：車輌CD
                if (this.TitleList.Contains(ConstCls.SHARYOU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].ReadOnly = true;
                }

                // 区間3：車輌名
                if (this.TitleList.Contains(ConstCls.SHARYOU_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                }

                // 区間3：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].ReadOnly = true;
                }

                // 区間3：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_3].Value = null;
                }

                // 区間3：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].ReadOnly = true;
                }

                // 区間3：運搬先区分名
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_NAME_3].Value = null;
                }

                // 区間3：運搬先の事業者CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GYOUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場名称
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_NAME_3].Value = null;
                }

                // 区間3：運搬先の事業場郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_POST_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場電話番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_TEL_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].ReadOnly = true;
                }

                // 区間3：運搬先の事業場住所
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_ADDRESS_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].ReadOnly = true;
                }

                // 区間3：運搬の受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運搬の受託者名称
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_NAME_3].Value = null;
                }

                // 区間3：運転者CD
                if (this.TitleList.Contains(ConstCls.UNTENSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].ReadOnly = true;
                }

                // 区間3：運転者名
                if (this.TitleList.Contains(ConstCls.UNTENSHA_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_NAME_3].Value = null;
                }

                // 区間3：運搬終了年月日
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_3].ReadOnly = true;
                }
            }
            // マニフェスト種類CDが"3"の場合
            else if ("3".Equals(shuruiCd))
            {
                // 処分の受領者CD
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOUSHA_CD))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_CD].ReadOnly = true;
                }
                // 処分の受領者名称
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOUSHA_NAME))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOUSHA_NAME].Value = null;
                }
                // 処分の受領担当者CD
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_TANTOU_CD))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_CD].ReadOnly = true;
                }
                // 処分の受領担当者名
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_TANTOU_NAME))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_TANTOU_NAME].Value = null;
                }
                // 処分受領日
                if (this.TitleList.Contains(ConstCls.SBN_JYURYOU_DATE))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SBN_JYURYOU_DATE].ReadOnly = true;
                }
                // 照合確認B1票
                if (this.TitleList.Contains(ConstCls.CHECK_B1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B1].ReadOnly = true;
                }
                // 照合確認B4票
                if (this.TitleList.Contains(ConstCls.CHECK_B4))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B4].ReadOnly = false;
                }
                // 照合確認B6票
                if (this.TitleList.Contains(ConstCls.CHECK_B6))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.CHECK_B6].ReadOnly = false;
                }
                // 区間1：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_1].ReadOnly = false;
                }
                // 区間1：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_1].ReadOnly = true;
                }
                // 区間1：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_1].Value = null;
                }
                // 区間1：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_1))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_1].ReadOnly = false;
                }
                // 区間2：運搬受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬受託者郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_POST_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_2].ReadOnly = false;
                }

                // 区間2：運搬受託者電話番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_TEL_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_2].ReadOnly = false;
                }

                // 区間2：運搬受託者住所
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_ADDRESS_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].ReadOnly = false;
                }

                // 区間2：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_2].ReadOnly = false;
                }

                // 区間2：車種CD
                if (this.TitleList.Contains(ConstCls.SHASHU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_2].ReadOnly = false;
                }

                // 区間2：車輌CD
                if (this.TitleList.Contains(ConstCls.SHARYOU_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_2].ReadOnly = false;
                }

                // 区間2：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_2].ReadOnly = true;
                }

                // 区間2：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_2].Value = null;
                }

                // 区間2：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_2].ReadOnly = false;
                }


                // 区間2：運搬先の事業者CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GYOUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_POST_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場電話番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_TEL_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_2].ReadOnly = false;
                }

                // 区間2：運搬先の事業場住所
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_ADDRESS_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_2].ReadOnly = false;
                }

                // 区間2：運搬の受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運転者CD
                if (this.TitleList.Contains(ConstCls.UNTENSHA_CD_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_2].ReadOnly = false;
                }

                // 区間2：運搬終了年月日
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_2))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_2].ReadOnly = false;
                }

                // 区間3：運搬受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_CD_3].ReadOnly = false;
                }

                // 区間3：運搬受託者郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_POST_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_POST_3].ReadOnly = false;
                }

                // 区間3：運搬受託者電話番号
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_TEL_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_TEL_3].ReadOnly = false;
                }

                // 区間3：運搬受託者住所
                if (this.TitleList.Contains(ConstCls.UPN_GYOUSHA_ADDRESS_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].ReadOnly = false;
                }

                // 区間3：運搬方法CD
                if (this.TitleList.Contains(ConstCls.UPN_HOUHOU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_HOUHOU_CD_3].ReadOnly = false;
                }

                // 区間3：車種CD
                if (this.TitleList.Contains(ConstCls.SHASHU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHASHU_CD_3].ReadOnly = false;
                }

                // 区間3：車輌CD
                if (this.TitleList.Contains(ConstCls.SHARYOU_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.SHARYOU_CD_3].ReadOnly = false;
                }

                // 区間3：積替保管有無
                if (this.TitleList.Contains(ConstCls.TMH_KBN_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].Value = null;
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_3].ReadOnly = true;
                }

                // 区間3：積替保管有無名称
                if (this.TitleList.Contains(ConstCls.TMH_KBN_NAME_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.TMH_KBN_NAME_3].Value = null;
                }

                // 区間3：運搬先区分
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_KBN_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_KBN_3].ReadOnly = false;
                }

                // 区間3：運搬先の事業者CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GYOUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GYOUSHA_CD_3].ReadOnly = false;
                }

                // 区間3：運搬先の事業場CD
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_3].ReadOnly = false;
                }

                // 区間3：運搬先の事業場郵便番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_POST_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_POST_3].ReadOnly = false;
                }

                // 区間3：運搬先の事業場電話番号
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_TEL_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_TEL_3].ReadOnly = false;
                }

                // 区間3：運搬先の事業場住所
                if (this.TitleList.Contains(ConstCls.UPN_SAKI_GENBA_ADDRESS_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_SAKI_GENBA_ADDRESS_3].ReadOnly = false;
                }

                // 区間3：運搬の受託者CD
                if (this.TitleList.Contains(ConstCls.UPN_JYUTAKUSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_JYUTAKUSHA_CD_3].ReadOnly = false;
                }

                // 区間3：運転者CD
                if (this.TitleList.Contains(ConstCls.UNTENSHA_CD_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UNTENSHA_CD_3].ReadOnly = false;
                }

                // 区間3：運搬終了年月日
                if (this.TitleList.Contains(ConstCls.UPN_END_DATE_3))
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[ConstCls.UPN_END_DATE_3].ReadOnly = false;
                }
            }
        }

        /// <summary>
        /// 交付番号チェック
        /// </summary>
        private string ChkDigitKohuNo(string manifestId)
        {
            string ret = string.Empty;
            string tmp1 = string.Empty;
            string tmp2 = string.Empty;
            long lA = 0;
            long lB = 0;
            long lC = 0;
            long lD = 0;
            long lZ = 0;

            tmp1 = manifestId.Substring(0, 10);
            lA = Convert.ToInt64(tmp1);
            tmp2 = manifestId.Substring(10, 1);
            lZ = Convert.ToInt64(tmp2);
            lB = lA / 7;
            lC = lB * 7;
            lD = lA - lC;
            if (lD != lZ)
            {
                ret = lD.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 日付項目初期値
        /// </summary>
        private void NyuryokuIkkatsuItiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rowIndex = 0;
            rowIndex = this.NyuryokuIkkatsuItiran.Rows.Count - 1;
            for (int i = 0; i < this.NyuryokuIkkatsuItiran.ColumnCount; i++)
            {

                object obj = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[i] as r_framework.CustomControl.DataGridCustomControl.DgvCustomDataTimeCell;

                if (obj != null)
                {
                    this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[i].Value = null;
                }
            }
        }

        /// <summary>
        /// CellBeginEdit
        /// </summary>
        private void NyuryokuIkkatsuItiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string tag = this.NyuryokuIkkatsuItiran.Columns[this.NyuryokuIkkatsuItiran.CurrentCell.ColumnIndex].Tag.ToString();
            //IMEモード設定
            if ("1".Equals(tag) || "3".Equals(tag))
            {
                this.NyuryokuIkkatsuItiran.ImeMode = ImeMode.Alpha;
            }
            else
            {
                this.NyuryokuIkkatsuItiran.ImeMode = ImeMode.Hiragana;
            }
        }

        /// <summary>
        /// KeyDown
        /// </summary>
        private void NyuryokuIkkatsuItiran_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.NyuryokuIkkatsuItiran.CurrentCell == null)
            {
                return;
            }

            //エラーだったらフォーカス移動しない
            var c = this.NyuryokuIkkatsuItiran.CurrentCell as r_framework.CustomControl.ICustomAutoChangeBackColor;
            if (c != null && c.IsInputErrorOccured)
            {
                return;
            }

            int columnIndex = this.NyuryokuIkkatsuItiran.CurrentCell.ColumnIndex;
            int columnCount = this.NyuryokuIkkatsuItiran.ColumnCount;
            int rowIndex = this.NyuryokuIkkatsuItiran.CurrentCell.RowIndex;
            int rowCount = this.NyuryokuIkkatsuItiran.RowCount;

            int columnLoop = columnCount - columnIndex - 1;

            /*
                        if (e.Modifiers == Keys.Shift)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Enter:

                                    e.Handled = true;

                                    for (int i = columnIndex; i > 0; i--)
                                    {

                                        columnIndex -= 1;

                                        if (!this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].ReadOnly)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex];
                                            if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                            {
                                                this.NyuryokuIkkatsuItiran.Focus();
                                            }
                                            break;
                                        }
                                    }

                                    break;
                                case Keys.Tab:
                                    e.Handled = true;

                                    for (int i = columnIndex; i > 0; i--)
                                    {

                                        columnIndex -= 1;

                                        if (!this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].ReadOnly)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex];
                                            if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                            {
                                                this.NyuryokuIkkatsuItiran.Focus();
                                            }
                                            break;
                                        }
                                    }

                                    break;
                            }

                            return;

                        }
            */
            switch (e.KeyCode)
            {
                /*
                                case Keys.Enter:
                                    e.Handled = true;

                                    if (columnIndex == columnCount - 1 && rowIndex < rowCount - 1)
                                    {

                                        this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0];
                                        bool rowSelect = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Selected;
                                        this.NyuryokuIkkatsuItiran.Refresh();
                                        this.NyuryokuIkkatsuItiran.Focus();
                                        break;
                                    }

                                    for (int i = 0; i < columnLoop; i++)
                                    {
                                        columnIndex += 1;

                                        if (columnIndex >= columnCount)
                                        {

                                            if (rowIndex < rowCount - 1)
                                            {
                                                this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0];
                                                if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                                {
                                                    this.NyuryokuIkkatsuItiran.Focus();
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (!this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].ReadOnly)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex];
                                            if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                            {
                                                this.NyuryokuIkkatsuItiran.Focus();
                                            }
                                            break;
                                        }
                                    }

                                    break;

                                case Keys.Tab:
                                    e.Handled = true;

                                    if (columnIndex == columnCount - 1 && rowIndex < rowCount - 1)
                                    {
                                        if (this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0].Visible)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0];
                                            bool rowSelect = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Selected;
                                            this.NyuryokuIkkatsuItiran.Refresh();
                                            this.NyuryokuIkkatsuItiran.Focus();
                                        }
                                        break;
                                    }

                                    for (int i = 0; i < columnLoop; i++)
                                    {
                                        columnIndex += 1;

                                        if (columnIndex >= columnCount)
                                        {

                                            if (rowIndex < rowCount - 1)
                                            {

                                                this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0];
                                                if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                                {
                                                    this.NyuryokuIkkatsuItiran.Focus();
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (!this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].ReadOnly && this.NyuryokuIkkatsuItiran.Columns[columnIndex].Visible)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex];
                                            if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                            {
                                                this.NyuryokuIkkatsuItiran.Focus();
                                            }
                                            break;
                                        }
                                    }

                                    break;

                                case Keys.Right:
                                    e.Handled = true;

                                    if (columnIndex == columnCount - 1 && rowIndex < rowCount - 1)
                                    {

                                        this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0];
                                        bool rowSelect = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Selected;
                                        this.NyuryokuIkkatsuItiran.Refresh();
                                        this.NyuryokuIkkatsuItiran.Focus();
                                        break;
                                    }

                                    for (int i = 0; i < columnLoop; i++)
                                    {

                                        columnIndex += 1;

                                        if (columnIndex >= columnCount)
                                        {

                                            if (rowIndex < rowCount - 1)
                                            {

                                                this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex + 1].Cells[0];
                                                if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                                {
                                                    this.NyuryokuIkkatsuItiran.Focus();
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }

                                        if (!this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].ReadOnly)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex];
                                            if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                            {
                                                this.NyuryokuIkkatsuItiran.Focus();
                                            }
                                            break;
                                        }
                                    }

                                    break;

                                case Keys.Left:
                                    e.Handled = true;

                                    for (int i = columnIndex; i > 0; i--)
                                    {
                                        columnIndex -= 1;

                                        if (!this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].ReadOnly && this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex].Visible)
                                        {
                                            this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran.Rows[rowIndex].Cells[columnIndex];
                                            if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
                                            {
                                                this.NyuryokuIkkatsuItiran.Focus();
                                            }
                                            break;
                                        }
                                    }

                                    break;
                */
                case Keys.Delete:

                    if ("3".Equals(this.NyuryokuIkkatsuItiran.Columns[this.NyuryokuIkkatsuItiran.CurrentCell.ColumnIndex].Tag.ToString()))
                    {
                        this.NyuryokuIkkatsuItiran.CurrentCell.Value = null;
                    }
                    else
                    {
                        string headName = this.NyuryokuIkkatsuItiran.Columns[this.NyuryokuIkkatsuItiran.CurrentCell.ColumnIndex].Name;

                        if (headName.Equals(ConstCls.UPN_GYOUSHA_CD_1))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_CD_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_NAME_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_POST_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_TEL_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_ADDRESS_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_CD_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_NAME_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                        }
                        else if (headName.Equals(ConstCls.SHASHU_CD_1))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_CD_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_NAME_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                        }
                        else if (headName.Equals(ConstCls.SHARYOU_CD_1))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_1].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_1].Value = null;
                        }
                        else if (headName.Equals(ConstCls.UPN_GYOUSHA_CD_2))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_CD_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_NAME_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_POST_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_TEL_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_ADDRESS_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_CD_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_NAME_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                        }
                        else if (headName.Equals(ConstCls.SHASHU_CD_2))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_CD_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_NAME_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                        }
                        else if (headName.Equals(ConstCls.SHARYOU_CD_2))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_2].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_2].Value = null;
                        }
                        else if (headName.Equals(ConstCls.UPN_GYOUSHA_CD_3))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_CD_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_NAME_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_POST_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_TEL_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.UPN_GYOUSHA_ADDRESS_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_CD_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_NAME_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                        }
                        else if (headName.Equals(ConstCls.SHASHU_CD_3))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_CD_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHASHU_NAME_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                        }
                        else if (headName.Equals(ConstCls.SHARYOU_CD_3))
                        {
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_CD_3].Value = null;
                            this.NyuryokuIkkatsuItiran.CurrentRow.Cells[ConstCls.SHARYOU_NAME_3].Value = null;
                        }
                    }

                    break;
            }
        }

        private void NyuryokuIkkatsuItiran_KeyUp(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// MouseUp
        /// 自身が読み取り専用の場合次のセルへ移動させる
        /// </summary>
        private void NyuryokuIkkatsuItiran_MouseUp(object sender, MouseEventArgs e)
        {

            //        if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
            //        {
            //            this.NyuryokuIkkatsuItiran.Focus();
            //        }
            //        return;
            //    }
            //    else
            //    {
            //        //最終行以外は下の行の先頭行
            //        this.NyuryokuIkkatsuItiran.CurrentCell = this.NyuryokuIkkatsuItiran[0, rowIndex + 1];

            //        if (this.NyuryokuIkkatsuItiran.CurrentCell.Selected)
            //        {
            //            this.NyuryokuIkkatsuItiran.Focus();
            //        }
            //        return;
            //    }
            //}
        }

        public bool UPN_SAKI_GENBA_CD_1PopupAfter(object sender)
        {

            var cell = sender as r_framework.CustomControl.DgvCustomTextBoxCell;
            if (cell == null)
            {
                return true;
            }

            var r = cell.OwningRow;

            //空だと強制的に1とみなす
            if (r.Cells[ConstCls.UPN_SAKI_KBN_1].Value == null || string.IsNullOrEmpty(r.Cells[ConstCls.UPN_SAKI_KBN_1].Value.ToString()))
            {
                r.Cells[ConstCls.UPN_SAKI_KBN_1].Value = "1";
                r.Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "処分施設";
            }

            switch (r.Cells[ConstCls.UPN_SAKI_KBN_1].Value.ToString())
            {
                case "2": //積替

                    //現場
                    switch (this.mlogic.SetAddressGenbaForDgv(this.NyuryokuIkkatsuItiran,
                            cell.RowIndex,
                            ConstCls.UPN_SAKI_GYOUSHA_CD_1,
                            ConstCls.UPN_SAKI_GENBA_CD_1,
                            ConstCls.UPN_SAKI_GENBA_NAME_1,
                            null,
                            ConstCls.UPN_SAKI_GENBA_POST_1,
                            ConstCls.UPN_SAKI_GENBA_TEL_1,
                            ConstCls.UPN_SAKI_GENBA_ADDRESS_1
                            ))
                    {
                        case 0://正常
                            this.NyuryokuIkkatsuItiran.Rows[cell.RowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value =
                                this.NyuryokuIkkatsuItiran.Rows[cell.RowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value.ToString().PadLeft(6, '0').ToUpper();
                            break;

                        case 1://空白
                            return true;

                        case 2://エラー
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "現場");
                            return false;

                        default:
                            msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E034", "業者");
                            return false;
                    }

                    return true;
                    break;
                case "1": //処分施設
                default:

                    switch (this.mlogic.SetAddressGenbaForDgv(this.NyuryokuIkkatsuItiran,
                            cell.RowIndex,
                            ConstCls.SBN_GYOUSHA_CD,
                            ConstCls.UPN_SAKI_GENBA_CD_1,
                            ConstCls.UPN_SAKI_GENBA_NAME_1,
                            null,
                            ConstCls.UPN_SAKI_GENBA_POST_1,
                            ConstCls.UPN_SAKI_GENBA_TEL_1,
                            ConstCls.UPN_SAKI_GENBA_ADDRESS_1
                            ))
                    {
                        case 0://正常
                            this.NyuryokuIkkatsuItiran.Rows[cell.RowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value =
                                this.NyuryokuIkkatsuItiran.Rows[cell.RowIndex].Cells[ConstCls.UPN_SAKI_GENBA_CD_1].Value.ToString().PadLeft(6, '0').ToUpper();
                            break;

                        case 1://空白
                            return true;

                        case 2://エラー
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "現場");
                            return false;

                        default:
                            msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E034", "業者");
                            return false;
                    }
                    return this.SbnGyoushaCDPopupAfter();
                    break;
            }

        }

        public bool UPN_SAKI_GENBA_CD_1PopupBefore(object sender)
        {
            var cell = sender as r_framework.CustomControl.DgvCustomTextBoxCell;
            if (cell == null)
            {
                return false;
            }

            var r = cell.OwningRow;
 
            //空だと強制的に1とみなす
            if (r.Cells[ConstCls.UPN_SAKI_KBN_1].Value == null || string.IsNullOrEmpty(r.Cells[ConstCls.UPN_SAKI_KBN_1].Value.ToString()))
            {
                r.Cells[ConstCls.UPN_SAKI_KBN_1].Value = "1";
                r.Cells[ConstCls.UPN_SAKI_KBN_NAME_1].Value = "処分施設";
            }

            cell.popupWindowSetting.Clear();
            cell.PopupSearchSendParams.Clear();

            r_framework.Dto.PopupSearchSendParamDto paramDto = new r_framework.Dto.PopupSearchSendParamDto();
            paramDto.And_Or = CONDITION_OPERATOR.AND;
            paramDto.KeyName = "TEKIYOU_BEGIN";
            paramDto.Control = ConstCls.KOUFU_DATE;
            cell.PopupSearchSendParams.Add(paramDto);

            switch (r.Cells[ConstCls.UPN_SAKI_KBN_1].Value.ToString())
            {
                case "2": //積替
                    //区分で絞る
                    var sspDdto2 = new PopupSearchSendParamDto()
                    {
                        KeyName = "M_GENBA.TSUMIKAEHOKAN_KBN",
                        Value = "True",
                        And_Or = CONDITION_OPERATOR.AND
                    };
                    cell.PopupSearchSendParams.Add(sspDdto2);

                    cell.GetCodeMasterField = "GENBA_CD,GYOUSHA_CD";
                    cell.SetFormField = ConstCls.UPN_SAKI_GENBA_CD_1 + "," + ConstCls.UPN_SAKI_GYOUSHA_CD_1;
                    cell.PopupGetMasterField = "GENBA_CD,GYOUSHA_CD";
                    cell.PopupSetFormField = ConstCls.UPN_SAKI_GENBA_CD_1 + "," + ConstCls.UPN_SAKI_GYOUSHA_CD_1;

                    break;


                case "1": //処分施設
                default:
                    //区分で絞る
                    var sspDdto = new PopupSearchSendParamDto()
                    {
                        KeyName = "M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN",
                        Value = "True",
                        And_Or = CONDITION_OPERATOR.AND                       
                    };
                    cell.PopupSearchSendParams.Add(sspDdto);

                    sspDdto = new PopupSearchSendParamDto()
                    {
                        KeyName = "M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN",
                        Value = "True",
                        And_Or = CONDITION_OPERATOR.AND                       
                    };
                    cell.PopupSearchSendParams.Add(sspDdto);

                    sspDdto = new PopupSearchSendParamDto()
                    {
                        KeyName = "M_GYOUSHA.GYOUSHAKBN_MANI",
                        Value = "True",
                        And_Or = CONDITION_OPERATOR.AND                       
                    };
                    cell.PopupSearchSendParams.Add(sspDdto);

                    //業者コード連携
                    sspDdto = new PopupSearchSendParamDto()
                    {
                        KeyName = "GYOUSHA_CD",
                        Control = ConstCls.SBN_GYOUSHA_CD,
                        And_Or = CONDITION_OPERATOR.AND                       
                    };
                    cell.PopupSearchSendParams.Add(sspDdto);

                    cell.GetCodeMasterField = "GENBA_CD,GYOUSHA_CD";
                    cell.SetFormField = ConstCls.UPN_SAKI_GENBA_CD_1 + "," + ConstCls.SBN_GYOUSHA_CD;
                    cell.PopupGetMasterField = "GENBA_CD,GYOUSHA_CD";
                    cell.PopupSetFormField = ConstCls.UPN_SAKI_GENBA_CD_1 + "," + ConstCls.SBN_GYOUSHA_CD;

                    break;


            }

            return true;
            
        }

        public bool TMH_GYOUSHA_CDPopupAfter(object sender)
        {
            var cell = sender as r_framework.CustomControl.DgvCustomTextBoxCell;
            if (cell == null)
            {
                return true;
            }

            string currentVal = string.Empty;
            if (this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GYOUSHA_CD].Value != null)
            {
                currentVal = this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
            }
            
            // 変更があれば、排出事業場情報を初期化する
            if ((currentVal != this.BeforeCellValue || string.IsNullOrEmpty(currentVal))
                && this.NyuryokuIkkatsuItiran.Columns.Contains(ConstCls.TMH_GENBA_CD))
            {
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GENBA_CD].Value = string.Empty;
                this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GENBA_NAME].Value = string.Empty;
            }

            var r = cell.OwningRow;

            //業者
            switch (this.mlogic.SetAddressGyoushaForDgv(this.NyuryokuIkkatsuItiran,
                    cell.RowIndex,
                    ConstCls.TMH_GYOUSHA_CD,
                    ConstCls.TMH_GYOUSHA_NAME,
                    null,null,null,null
                    ))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GYOUSHA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                default:
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    return false;
            }

            return true;
        }

        public bool TMH_GENBA_CDPopupAfter(object sender)
        {

            var cell = sender as r_framework.CustomControl.DgvCustomTextBoxCell;
            if (cell == null)
            {
                return true;
            }

            var r = cell.OwningRow;

            //現場
            switch (this.mlogic.SetAddressGenbaForDgv(this.NyuryokuIkkatsuItiran,
                    cell.RowIndex,
                    ConstCls.TMH_GYOUSHA_CD,
                    ConstCls.TMH_GENBA_CD,
                    ConstCls.TMH_GENBA_NAME,
                    null,
                    ConstCls.TMH_GENBA_POST,
                    ConstCls.TMH_GENBA_TEL,
                    ConstCls.TMH_GENBA_ADDRESS
                    ))
            {
                case 0://正常
                    this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GENBA_CD].Value =
                        this.NyuryokuIkkatsuItiran.Rows[this.NyuryokuIkkatsuItiran.CurrentRow.Index].Cells[ConstCls.TMH_GENBA_CD].Value.ToString().PadLeft(6, '0').ToUpper();
                    break;

                case 1://空白
                    return true;

                case 2://エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    return false;

                default: //業者が複数マッチ
                    msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E034", "業者");
                    return false;
            }

            return true;
            //return this.TMH_GYOUSHA_CDPopupAfter(r.Cells[ConstCls.TMH_GYOUSHA_CD]);
        }
    }
}
