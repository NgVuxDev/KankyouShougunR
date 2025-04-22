using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.CustomControl;
using System.Data.SqlTypes;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Data;

namespace Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou
{
    /// <summary>
    /// 請求明細表 出力画面クラス
    /// </summary>
    public partial class UIForm : SuperForm
    {
       
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public UIForm(WINDOW_ID windowID)
        {
            LogUtility.DebugMethodStart(windowID);

            this.InitializeComponent();

            this.WindowId = windowID;

            this.logic = new LogicClass(this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();
            var parentForm = (BusinessBaseForm)this.Parent;

            // 拠点CD
            this.KYOTEN_CD.Text = "99";
            // 拠点
            this.KYOTEN_NAME.Text = "全社";
            // 請求日付From
            this.HIDUKE_FROM.Value = parentForm.sysDate.Date;
            // 請求日付From
            this.HIDUKE_TO.Value = parentForm.sysDate.Date;
            // 取引先CDFrom
            this.TORIHIKISAKI_CD_FROM.Text = String.Empty;
            // 取引先From
            this.TORIHIKISAKI_NAME_FROM.Text = String.Empty;
            // 取引先CDTo
            this.TORIHIKISAKI_CD_TO.Text = String.Empty;
            // 取引先To
            this.TORIHIKISAKI_NAME_TO.Text = String.Empty;
            // 請求書書式
            this.SYOSIKI.Text = "1";
            // 並び順
            this.TORIHIKISAKI_SORT.Text = "1";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (!this.SetTorihikisakiCd())
            {
                return;
            }

            // 登録時チェックロジックを実行
            for (int i = this.HIDUKE_FROM.RegistCheckMethod.Count - 1; i >= 0; i--)
            {
                if (this.HIDUKE_FROM.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.HIDUKE_FROM.RegistCheckMethod.RemoveAt(i);
                }
            }
            for (int i = this.HIDUKE_TO.RegistCheckMethod.Count - 1; i >= 0; i--)
            {
                if (this.HIDUKE_TO.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.HIDUKE_TO.RegistCheckMethod.RemoveAt(i);
                }
            }
            //20151027 hoanghm #13695 start
            //if (String.IsNullOrEmpty(this.HIDUKE_FROM.Text) && String.IsNullOrEmpty(this.HIDUKE_TO.Text))
            //{
            //    this.HIDUKE_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            //    this.HIDUKE_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));

            //}
            if (string.IsNullOrEmpty(this.HIDUKE_FROM.Text))
            {
                this.HIDUKE_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            }
            if (string.IsNullOrEmpty(this.HIDUKE_TO.Text))
            {
                this.HIDUKE_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));

            }
            //20151027 hoanghm #13695 end
            for (int i = this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.Count - 1; i >= 0; i--)
            {
                if (this.TORIHIKISAKI_CD_FROM.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.RemoveAt(i);
                }
            }
            for (int i = this.TORIHIKISAKI_CD_TO.RegistCheckMethod.Count - 1; i >= 0; i--)
            {
                if (this.TORIHIKISAKI_CD_TO.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.TORIHIKISAKI_CD_TO.RegistCheckMethod.RemoveAt(i);
                }
            }
            //20151027 hoanghm #13695 start
            //if (String.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text) && String.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            //{
            //    this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            //    this.TORIHIKISAKI_CD_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));

            //}
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text))
            {
                this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            }
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            {
                this.TORIHIKISAKI_CD_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            }
            //20151027 hoanghm #13695 end
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
            }
            else
            {

                var dto = new DTOClass();
                dto.SYOSIKI = int.Parse(this.SYOSIKI.Text);
                dto.KYOTEN_CD = SqlInt16.Parse(this.KYOTEN_CD.Text);
                dto.HIDUKE_FROM = this.HIDUKE_FROM.Value == null ? SqlDateTime.Null : SqlDateTime.Parse(this.HIDUKE_FROM.Value.ToString());
                dto.HIDUKE_TO = this.HIDUKE_TO.Value == null ? SqlDateTime.Null : SqlDateTime.Parse(this.HIDUKE_TO.Value.ToString());
                dto.TORIHIKISAKI_CD_FROM = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TORIHIKISAKI_CD_TO = this.TORIHIKISAKI_CD_TO.Text;
                //20151027 hoanghm #13688 start
                dto.TORIHIKISAKI_NAME_RYAKU_FROM = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TORIHIKISAKI_NAME_RYAKU_TO = this.TORIHIKISAKI_NAME_TO.Text;
                //20151027 hoanghm #13688 end
                dto.TORIHIKISAKI_SORT = this.TORIHIKISAKI_SORT.Text;

                this.logic.CSV(dto);
            }

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        public void CsvReport(DataTable dt)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(dt, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU), this);
            }
        }

        /// <summary>
        /// 表示ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Cursor.Current = Cursors.WaitCursor;

            if (!this.SetTorihikisakiCd())
            {
                return;
            }

            // 登録時チェックロジックを実行
            for (int i = this.HIDUKE_FROM.RegistCheckMethod.Count-1; i >= 0; i--)
            {
                if (this.HIDUKE_FROM.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.HIDUKE_FROM.RegistCheckMethod.RemoveAt(i);
                }
            }
            for (int i = this.HIDUKE_TO.RegistCheckMethod.Count-1; i >= 0; i--)
            {
                if (this.HIDUKE_TO.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.HIDUKE_TO.RegistCheckMethod.RemoveAt(i);
                }
            }
            //20151027 hoanghm #13695 start
            //if (String.IsNullOrEmpty(this.HIDUKE_FROM.Text) && String.IsNullOrEmpty(this.HIDUKE_TO.Text))
            //{
            //    this.HIDUKE_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            //    this.HIDUKE_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
        
            //}
            if (string.IsNullOrEmpty(this.HIDUKE_FROM.Text))
            {
                this.HIDUKE_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            }
            if (string.IsNullOrEmpty(this.HIDUKE_TO.Text))
            {
                this.HIDUKE_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));

            }
            //20151027 hoanghm #13695 end
            for (int i = this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.Count - 1; i >= 0; i--)
            {
                if (this.TORIHIKISAKI_CD_FROM.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.RemoveAt(i);
                }
            }
            for (int i = this.TORIHIKISAKI_CD_TO.RegistCheckMethod.Count - 1; i >= 0; i--)
            {
                if (this.TORIHIKISAKI_CD_TO.RegistCheckMethod[i].CheckMethodName == ConstClass.CHECKMETHODNAME_REQUIRED)
                {
                    this.TORIHIKISAKI_CD_TO.RegistCheckMethod.RemoveAt(i);
                }
            }
            //20151027 hoanghm #13695 start
            //if (String.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text) && String.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            //{
            //    this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            //    this.TORIHIKISAKI_CD_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));

            //}
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD_FROM.Text))
            {
                this.TORIHIKISAKI_CD_FROM.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            }
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD_TO.Text))
            {
                this.TORIHIKISAKI_CD_TO.RegistCheckMethod.Insert(0, new r_framework.Dto.SelectCheckDto(ConstClass.CHECKMETHODNAME_REQUIRED));
            }
            //20151027 hoanghm #13695 end
            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            if (this.RegistErrorFlag)
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;

                var focusControl = this.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
            }
            else
            {

                var dto = new DTOClass();
                dto.SYOSIKI = int.Parse(this.SYOSIKI.Text);
                dto.KYOTEN_CD = SqlInt16.Parse(this.KYOTEN_CD.Text);
                dto.HIDUKE_FROM = this.HIDUKE_FROM.Value == null? SqlDateTime.Null: SqlDateTime.Parse(this.HIDUKE_FROM.Value.ToString());
                dto.HIDUKE_TO = this.HIDUKE_TO.Value == null ? SqlDateTime.Null : SqlDateTime.Parse(this.HIDUKE_TO.Value.ToString());
                dto.TORIHIKISAKI_CD_FROM = this.TORIHIKISAKI_CD_FROM.Text;
                dto.TORIHIKISAKI_CD_TO = this.TORIHIKISAKI_CD_TO.Text;
                //20151027 hoanghm #13688 start
                dto.TORIHIKISAKI_NAME_RYAKU_FROM = this.TORIHIKISAKI_NAME_FROM.Text;
                dto.TORIHIKISAKI_NAME_RYAKU_TO = this.TORIHIKISAKI_NAME_TO.Text;
                //20151027 hoanghm #13688 end
                //20151027 hoanghm #123537 end
                dto.TORIHIKISAKI_SORT = this.TORIHIKISAKI_SORT.Text;
                //20190215 wangjm #123537 end

                this.logic.Preview(dto);
            }

            Cursor.Current = Cursors.Arrow;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じるボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit())
            {
                return;
            }

            this.Initialize();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

      
        /// <summary>
        /// 取引先CDToのテキストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            TORIHIKISAKI_CD_TO.Text = TORIHIKISAKI_CD_FROM.Text;
            TORIHIKISAKI_NAME_TO.Text = TORIHIKISAKI_NAME_FROM.Text;
 

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDをセットします（取引先CDが入力されていないとき）
        /// </summary>
        private bool SetTorihikisakiCd()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先が入力可で、入力されていなければ自動入力する
                if (this.TORIHIKISAKI_CD_FROM.Enabled && this.TORIHIKISAKI_CD_TO.Enabled)
                {
                    var torihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
                    var torihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
                    if (String.IsNullOrEmpty(torihikisakiCdFrom) && String.IsNullOrEmpty(torihikisakiCdTo))
                    {
                        var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                        var firstTorihikisaki = logic.GetMinTorihikisaki();
                        var lastTorihikisaki = logic.GetMaxTorihikisaki();
                        if (null != firstTorihikisaki)
                        {
                            this.TORIHIKISAKI_CD_FROM.Text = firstTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_FROM.Text = firstTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                        if (null != lastTorihikisaki)
                        {
                            this.TORIHIKISAKI_CD_TO.Text = lastTorihikisaki.TORIHIKISAKI_CD;
                            this.TORIHIKISAKI_NAME_TO.Text = lastTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisakiCd", ex1);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisakiCd", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }



    }
}
