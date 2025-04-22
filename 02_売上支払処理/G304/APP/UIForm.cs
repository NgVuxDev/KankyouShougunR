using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using r_framework.APP.PopUp.Base;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using SyaryoSentaku.Logic;
using SyaryoSentaku.Const;
using r_framework.Utility;
using r_framework.Dto;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SyaryoSentaku.App
{
    /// <summary>UIForm</summary>
    public partial class UIForm : SuperPopupForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls Logic;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>車両コード</summary>
        public String ParamOut_SyaryouCode { get; set; }
        /// <summary>車両名</summary>
        public String ParamOut_SyaryouName { get; set; }
        /// <summary>業者コード</summary>
        public String ParamOut_GyosyaCode { get; set; }
        /// <summary>業者名</summary>
        public String ParamOut_GyosyaName { get; set; }
        /// <summary>車種コード</summary>
        public String ParamOut_ShashuCode { get; set; }
        /// <summary>車種名</summary>
        public String ParamOut_ShashuName { get; set; }
        /// <summary>運転者コード</summary>
        public String ParamOut_UntenCode { get; set; }
        /// <summary>運転者</summary>
        public String ParamOut_UntenName { get; set; }
        /// <summary>空車重量</summary>
        public String ParamOut_KuushaJyuryo { get; set; }   // No.3875

        #endregion

        public UIForm()
            : base(WINDOW_ID.T_SYARYOU_SENTAKU)
        {
            this.InitializeComponent();

            this.CarIchiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// UIForm
        /// </summary>
        public UIForm(String paramIn_GyosyaCode, string paramIn_CarCode)
            : base(WINDOW_ID.T_SYARYOU_SENTAKU)
        {
            this.InitializeComponent();

            this.CarIchiran.IsBrowsePurpose = true;

            //パラメータ設定
            //Properties.Settings.Default.ParamIn_GyosyaCode = paramIn_GyosyaCode;
            //Properties.Settings.Default.ParamIn_CarCode = paramIn_CarCode;
            //Properties.Settings.Default.Save();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            if (this.Logic.Search() != -1)
            {
                this.Logic.SetIchiran();
            }
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = true;
            this.Logic.WindowInit(out catchErr);
            if (!catchErr)
            {
                return;
            }

            // イベントバンディング
            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }
            this.bt_func9.Focus();

            this.CarIchiran.KeyDown += this.DetailKeyDown; //行選択のEnterがKeyUpだけでやるとフォーカスがした移動してから選択されるため、ガードが必要
            this.CarIchiran.KeyUp += this.DetailKeyUp; //KeyDownでポップアップがとじ、KeyUpで呼び出しもとがとじていた。 KeyUpで閉じるように統一し道連れにしないようにする。

        }

        /// <summary>
        /// 新規処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NewAdd(object sender, EventArgs e)
        {
            this.Logic.ClearCondition();

            this.Close();
        }

        /// <summary>
        /// 条件入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void JoukennNyuuryoku(object sender, EventArgs e)
        {
            // [F5]条件入力」ボタン押下時に、フォーカスを検索条件に設定する。
            this.KENCONDITION_ITEM.Focus();
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClear(object sender, EventArgs e)
        {
            // 20140620 ria EV004834 車輌CDを手入力した時、「車輌マスタに存在しないCDが入力されました。」とアラートが表示される。 start
            this.KENCONDITION_ITEM.Text = "1";
            this.KENCONDITION_VALUE.Text = string.Empty;
            // 20140620 ria EV004834 車輌CDを手入力した時、「車輌マスタに存在しないCDが入力されました。」とアラートが表示される。 end
            this.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormConfirm(object sender, EventArgs e)
        {
            this.Logic.FormConfirm();
            this.ReturnValSet();
            this.Logic.ClearCondition();

            this.Close();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            this.Logic.ClearCondition();

            this.Close();
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void Sort(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }
        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(this.Logic.CarIchiran_CellDoubleClick(sender, e))
            {
                this.ReturnValSet();
                this.Logic.ClearCondition();
                this.Close();
            }
        }

        /// <summary>
        /// 一覧明細のセルフォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarIchiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 空車重量
            if (e.ColumnIndex == this.CarIchiran.Columns["空車重量"].Index)
            {
                var v = this.CarIchiran["空車重量", e.RowIndex].Value;
                decimal gennyouSuu;
                if (v != null && !string.IsNullOrEmpty(v.ToString()) && Decimal.TryParse(v.ToString(), out gennyouSuu))
                {
                    e.Value = gennyouSuu.ToString(this.Logic.SysInfo.SYS_JYURYOU_FORMAT);
                }
            }
        }

        /// <summary>
        /// 戻り値設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnValSet()
        {
            // setParamList
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();

            // リターン値を設定
            List<PopupReturnParam> setParam0 = new List<PopupReturnParam>();
            PopupReturnParam popupParam0 = new PopupReturnParam();
            popupParam0.Key = "Value";
            popupParam0.Value = ParamOut_SyaryouCode;
            setParam0.Add(popupParam0);
            setParamList.Add(0, setParam0);

            List<PopupReturnParam> setParam1 = new List<PopupReturnParam>();
            PopupReturnParam popupParam1 = new PopupReturnParam();
            popupParam1.Key = "Value";
            popupParam1.Value = ParamOut_SyaryouName;
            setParam1.Add(popupParam1);
            setParamList.Add(1, setParam1);

            List<PopupReturnParam> setParam2 = new List<PopupReturnParam>();
            PopupReturnParam popupParam2 = new PopupReturnParam();
            popupParam2.Key = "Value";
            popupParam2.Value = ParamOut_GyosyaCode;
            setParam2.Add(popupParam2);
            setParamList.Add(2, setParam2);

            List<PopupReturnParam> setParam3 = new List<PopupReturnParam>();
            PopupReturnParam popupParam3 = new PopupReturnParam();
            popupParam3.Key = "Value";
            popupParam3.Value = ParamOut_GyosyaName;
            setParam3.Add(popupParam3);
            setParamList.Add(3, setParam3);

            List<PopupReturnParam> setParam4 = new List<PopupReturnParam>();
            PopupReturnParam popupParam4 = new PopupReturnParam();
            popupParam4.Key = "Value";
            popupParam4.Value = ParamOut_ShashuCode;
            setParam4.Add(popupParam4);
            setParamList.Add(4, setParam4);

            List<PopupReturnParam> setParam5 = new List<PopupReturnParam>();
            PopupReturnParam popupParam5 = new PopupReturnParam();
            popupParam5.Key = "Value";
            popupParam5.Value = ParamOut_ShashuName;
            setParam5.Add(popupParam5);
            setParamList.Add(5, setParam5);

            List<PopupReturnParam> setParam6 = new List<PopupReturnParam>();
            PopupReturnParam popupParam6 = new PopupReturnParam();
            popupParam6.Key = "Value";
            popupParam6.Value = ParamOut_UntenCode;
            setParam6.Add(popupParam6);
            setParamList.Add(6, setParam6);

            List<PopupReturnParam> setParam7 = new List<PopupReturnParam>();
            PopupReturnParam popupParam7 = new PopupReturnParam();
            popupParam7.Key = "Value";
            popupParam7.Value = ParamOut_UntenName;
            setParam7.Add(popupParam7);
            setParamList.Add(7, setParam7);

            // No.3875-->
            List<PopupReturnParam> setParam8 = new List<PopupReturnParam>();
            PopupReturnParam popupParam8 = new PopupReturnParam();
            popupParam8.Key = "Value";
            popupParam8.Value = ParamOut_KuushaJyuryo;
            setParam8.Add(popupParam8);
            setParamList.Add(8, setParam8);
            // No.3875<--

            //　戻る
            this.ReturnParams = setParamList;
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        protected void c_GotFocus(object sender, EventArgs e)
        {
            var activ = this.ActiveControl as SuperPopupForm;

            if (activ == null)
            {
                if (this.ActiveControl != null)
                {
                    this.lb_hint.Text = (string)this.ActiveControl.Tag;
                }
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            //機能なし
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            //機能なし
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            //機能なし
        }

        /// <summary>
        /// 登録/更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            //機能なし
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //グリッドでのEnterはKeyDownでカーソルが下移動するするため、Downで先にハンドルする必要がある
            if (e.KeyCode == Keys.Enter)
            {
                this.Logic.ElementDecision();
                this.ReturnValSet();
                this.Logic.ClearCondition();
                this.Close();
            }
        }
        /// <summary>
        /// キーアップ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                base.ReturnParams = null;
                e.Handled = true;
                this.Close();
            }
        }

        // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない start
        private void PARENT_KENCONDITION_ITEM_TextChanged(object sender, EventArgs e)
        {
            this.Logic.ImeControlCondition();
        }
        // 20140620 ria EV004826 車輌選択画面に検索条件が無い為、車輌の検索ができない end
    }
}
