// $Id:
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Carriage.UnchinMeisaihyouDto;

namespace Shougun.Core.Carriage.UnchinMeisaihyou
{
    /// <summary>
    /// 運賃明細表出力指定画面を表すクラス・コントロール
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region - Fields -

        /// <summary>画面ロジック</summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UIForm_UriageShiharaiMeisaihyou" /> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public UIForm(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new LogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        #region 画面を初期化します
        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();

            // 拠点CD
            this.KYOTEN_CD.Text = "99";
            // 拠点
            this.KYOTEN_NAME_RYAKU.Text = "全社";
            // 形態区分CD
            this.KEITAI_KBN_CD.Text = string.Empty;
            // 形態区分名
            this.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;
            // 伝種区分
            this.DENPYOU_SHURUI.Text = "6";
            // 日付CD
            this.HIDUKE_SHURUI.Text = "1";
            // 日付範囲
            this.HIDUKE.Text = "1";
            // 日付From
            this.HIDUKE_FROM.Text = string.Empty;
            // 日付To
            this.HIDUKE_TO.Text = string.Empty;
            // 並び順
            this.ORDER.Text = "1";

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region キー処理

        /// <summary>F5キー(CSV)ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
                return;
            }

            bool catchErr = false;
            if (this.customRadioButtonHidukeHaniShitei3.Checked && this.logic.CheckDate(out catchErr))
            {
                return;
            }
            if (catchErr)
            {
                return;
            }

            // FromToのチェックがうまくいかないので自前でチェックする
            var errMsg = string.Empty;
            if (!this.CheckGyoushaCdFromTo())
            {
                errMsg = "業者";
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");

                Cursor.Current = Cursors.Arrow;
                return;
            }

            var dto = CreateDto();

            Cursor.Current = Cursors.Arrow;

            //CSV出力
            this.logic.CSVPrint(dto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>F7キー(表示)ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
                return;
            }

            bool catchErr = false;
            if (this.customRadioButtonHidukeHaniShitei3.Checked && this.logic.CheckDate(out catchErr))
            {
                return;
            }
            if (catchErr)
            {
                return;
            }

            // 業者自動設定
            //this.logic.SetGyoushaCdFromTo();

            // FromToのチェックがうまくいかないので自前でチェックする
            var errMsg = string.Empty;
            if (!this.CheckGyoushaCdFromTo())
            {
                errMsg = "業者";
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");

                Cursor.Current = Cursors.Arrow;
                return;
            }

            var dto = CreateDto();

            Cursor.Current = Cursors.Arrow;

            this.logic.Search(dto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>DTO作成</summary>
        /// <param name=""></param>
        private DtoClass CreateDto()
        {
            // 帳票出力とCSVへ
            var dto = new DtoClass();
            dto.KyotenCd = int.Parse(this.KYOTEN_CD.Text);
            dto.KyotenName = this.KYOTEN_NAME_RYAKU.Text;
            dto.KeitaiKbnCd = this.KEITAI_KBN_CD.Text;
            dto.KeitaiKbnName = this.KEITAI_KBN_NAME_RYAKU.Text;

            if (this.DENPYOU_SHURUI.Text == "4")
            {
                dto.DenshuKbn = 170;
            }
            else if (this.DENPYOU_SHURUI.Text == "5")
            {
                dto.DenshuKbn = 160;
            }
            else
            {
                dto.DenshuKbn = int.Parse(this.DENPYOU_SHURUI.Text);
            }
            dto.DateShuruiCd = int.Parse(this.HIDUKE_SHURUI.Text);

            var parentForm = (BusinessBaseForm)this.Parent;

            // 日付範囲の選択状態で日付条件を設定
            if (this.customRadioButtonHidukeHaniShitei1.Checked)
            {
                dto.DateFrom = parentForm.sysDate.ToString("yyyy/MM/dd");
                dto.DateTo = parentForm.sysDate.ToString("yyyy/MM/dd");
            }
            else if (this.customRadioButtonHidukeHaniShitei2.Checked)
            {
                dto.DateFrom = new DateTime(parentForm.sysDate.Year, parentForm.sysDate.Month, 1).ToString("yyyy/MM/dd");
                dto.DateTo = new DateTime(parentForm.sysDate.Year, parentForm.sysDate.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd"); ;
            }
            else if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                dto.DateFrom = Convert.ToDateTime(this.HIDUKE_FROM.Value).ToString("yyyy/MM/dd");
                dto.DateTo = Convert.ToDateTime(this.HIDUKE_TO.Value).ToString("yyyy/MM/dd");
            }

            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD_FROM.Text))
            {
                dto.UnpanGyoushaCdFrom = this.UNPAN_GYOUSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD_TO.Text))
            {
                dto.UnpanGyoushaCdTo = this.UNPAN_GYOUSHA_CD_TO.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_NAME_FROM.Text))
            {
                dto.UnpanGyoushaFrom = this.UNPAN_GYOUSHA_NAME_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_NAME_TO.Text))
            {
                dto.UnpanGyoushaTo = this.UNPAN_GYOUSHA_NAME_TO.Text;
            }

            dto.Order = int.Parse(this.ORDER.Text);

            dto.IsGroupUnpanGyousha = this.GROUP_UNPAN_GYOUSHA.Checked;
            dto.IsGroupDenpyouNumber = this.GROUP_DENPYOU_NUMBER.Checked;

            return dto;
        }

        #region FromToの関係をチェックします
        /// <summary>
        /// 業者CDのFromToの関係をチェックします
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool CheckGyoushaCdFromTo()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;

            var cdFrom = this.UNPAN_GYOUSHA_CD_FROM.Text;
            var cdTo = this.UNPAN_GYOUSHA_CD_TO.Text;
            if (!string.IsNullOrEmpty(cdFrom) && !string.IsNullOrEmpty(cdTo))
            {
                if (0 < cdFrom.CompareTo(cdTo))
                {
                    this.UNPAN_GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                    this.UNPAN_GYOUSHA_CD_TO.IsInputErrorOccured = true;

                    this.UNPAN_GYOUSHA_CD_FROM.Focus();

                    ret = false;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        /// <summary>Ｆ１２キー（閉じる）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion - キー処理 -

        #region 画面Load処理
        /// <summary>画面Load処理</summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic.WindowInit();

            // 初期化処理
            this.Initialize();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region チェック状態が変更されたときに処理します
        /// <summary>
        /// 期間指定ラジオボタンのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void RadioButtonHidukeHaniShitei_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.HIDUKE_FROM.Text = string.Empty;
            this.HIDUKE_TO.Text = string.Empty;

            if (this.customRadioButtonHidukeHaniShitei3.Checked)
            {
                // 日付範囲指定テキストボックスに（登録時）必須チェックを追加する
                this.HIDUKE_FROM.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };
                this.HIDUKE_TO.RegistCheckMethod = new Collection<SelectCheckDto>() { new SelectCheckDto("必須チェック") };

                this.customPanelHidukeHaniShitei.Enabled = true;
            }
            else
            {
                // 日付範囲指定テキストボックスの必須チェックをはずす
                this.HIDUKE_FROM.RegistCheckMethod = new Collection<SelectCheckDto>();
                this.HIDUKE_TO.RegistCheckMethod = new Collection<SelectCheckDto>();

                this.customPanelHidukeHaniShitei.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion - Methods -

        #region バリデートが開始されたときに処理します
        /// <summary>
        /// 運搬業者CDFromのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void UNPAN_GYOUSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var unpanGyoushaCd = this.UNPAN_GYOUSHA_CD_FROM.Text;

            if (string.IsNullOrEmpty(unpanGyoushaCd))
            {
                this.UNPAN_GYOUSHA_NAME_FROM.Text = string.Empty;
            }
            else if (!string.IsNullOrEmpty(unpanGyoushaCd) && !this.CheckUnpanGyousha(unpanGyoushaCd, true))
            {
                e.Cancel = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者CDToのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void UNPAN_GYOUSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var unpanGyoushaCd = this.UNPAN_GYOUSHA_CD_TO.Text;

            if (string.IsNullOrEmpty(unpanGyoushaCd))
            {
                this.UNPAN_GYOUSHA_NAME_TO.Text = string.Empty;
            }
            else if (!string.IsNullOrEmpty(unpanGyoushaCd) && !this.CheckUnpanGyousha(unpanGyoushaCd, false))
            {
                e.Cancel = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力された運搬業者CDに対応する運搬業者が存在するかチェックします
        /// </summary>
        /// <param name="eigyoushaCd">運搬業者CD</param>
        /// <param name="isFromCd">From側の運搬業者CDをチェックする場合はTrue</param>
        /// <returns>チェック結果</returns>
        private bool CheckUnpanGyousha(string unpanGyoushaCd, bool isFromCd)
        {
            LogUtility.DebugMethodStart(unpanGyoushaCd, isFromCd);

            bool ret = true;

            var unpanGyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var mGyoushaList = unpanGyoushaDao.GetAllValidData(new M_GYOUSHA { GYOUSHA_CD = unpanGyoushaCd, ISNOT_NEED_DELETE_FLG = true, UNPAN_JUTAKUSHA_KAISHA_KBN = true });

            if (mGyoushaList == null || mGyoushaList.Length == 0)
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");

                if (isFromCd)
                {
                    this.UNPAN_GYOUSHA_NAME_FROM.Text = string.Empty;
                }
                else
                {
                    this.UNPAN_GYOUSHA_NAME_TO.Text = string.Empty;
                }

                ret = false;
            }
            else
            {
                if (isFromCd)
                {
                    this.UNPAN_GYOUSHA_NAME_FROM.Text = mGyoushaList[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.UNPAN_GYOUSHA_NAME_TO.Text = mGyoushaList[0].GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #region バリデートが完了したときに処理します

        /// <summary>
        /// 集計単位のチェックボックスの活性or不活性を切り替えます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ORDER_TextChanged(object sender, EventArgs e)
        {
            this.ChangeGroupCheckBoxEnabled();
        }

        /// <summary>
        /// 並び順の選択によって、集計単位のチェックボックスの活性or不活性を切り替えます
        /// </summary>
        private void ChangeGroupCheckBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            if (this.customRadioButtonSort1.Checked || this.customRadioButtonSort2.Checked)
            {
                this.GROUP_UNPAN_GYOUSHA.Enabled = true;
                this.GROUP_DENPYOU_NUMBER.Enabled = true;

                this.GROUP_UNPAN_GYOUSHA.Checked = true;
                this.GROUP_DENPYOU_NUMBER.Checked = true;
            }
            else if (this.customRadioButtonSort3.Checked || this.customRadioButtonSort4.Checked)
            {
                this.GROUP_UNPAN_GYOUSHA.Enabled = false;
                this.GROUP_DENPYOU_NUMBER.Enabled = true;

                this.GROUP_UNPAN_GYOUSHA.Checked = false;
                this.GROUP_DENPYOU_NUMBER.Checked = true;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// 日付Toテキストボックスでダブルクリックしたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HIDUKE_TO_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.HIDUKE_TO.Text))
            {
                this.HIDUKE_TO.Text = this.HIDUKE_FROM.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        private void GYOUSHA_CD_TO_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD_TO.Text))
            {
                this.UNPAN_GYOUSHA_CD_TO.Text = this.UNPAN_GYOUSHA_CD_FROM.Text;
                this.UNPAN_GYOUSHA_NAME_TO.Text = this.UNPAN_GYOUSHA_NAME_FROM.Text;
            }
        }
        #endregion
    }
}
