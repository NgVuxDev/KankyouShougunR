using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP;
using Seasar.Framework.Exceptions;
using System.Data.SqlTypes;
using Shougun.Function.ShougunCSCommon.Const;
using r_framework.Entity;
using System.Collections.Generic;
using r_framework.CustomControl;
using System.Text;
using r_framework.Dto;
using r_framework.Dao;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Logic
{
    /// <summary> ビジネスロジック </summary>
    internal class LogicClassShukka : IBuisinessLogic
    {
        /// <summary> Form </summary>
        private ShukkaDenpyouikkatsuPopupForm form;

        public MessageBoxShowLogic msgLogic;

        internal string tmpTorihikisakiCd = string.Empty;
        internal string tmpGyousyaCd = string.Empty;
        internal string tmpNizumiGyoushaCd = string.Empty;
        internal string tmpNizumiGenbaCd = string.Empty;
        internal string tmpUnpanGyoushaCd = string.Empty;
        internal string tmpGenbaCd = string.Empty;  // No.3587
        internal string tmpKeitaiKbnCd = string.Empty;
        internal string tmpUntenshaCd = string.Empty;
        private string sharyouCd = string.Empty;
        private string shaShuCd = string.Empty;
        private string unpanGyousha = string.Empty;

        private string torihikisakiHintText = "全角20桁以内で入力してください";
        private string gyoushaHintText = "全角20桁以内で入力してください";
        private string genbaHintText = "全角20桁以内で入力してください";
        private string nizumiGyoushaHintText = "全角20桁以内で入力してください";
        private string nizumiGenbaHintText = "全角20桁以内で入力してください";
        private string sharyouHinttext = "全角10桁以内で入力してください";
        private string unpanGyoushaHintText = "全角20桁以内で入力してください";
        private Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);
        private Color sharyouCdBackColorBlue = Color.FromArgb(0, 255, 255);


        // No.3822-->
        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames =
            {
                "DENPYOU_DATE",
                "URIAGE_DATE",
                "SHIHARAI_DATE",
                "TORIHIKISAKI_CD",
                "TORIHIKISAKI_NAME_RYAKU",
                "GYOUSHA_CD",
                "GYOUSHA_NAME_RYAKU",
                "GENBA_CD",
                "GENBA_NAME_RYAKU",
                "EIGYOU_TANTOUSHA_CD",
                "NIZUMI_GYOUSHA_CD",
                "NIZUMI_GYOUSHA_NAME_RYAKU",
                "NIZUMI_GENBA_CD",
                "NIZUMI_GENBA_NAME_RYAKU",
                "UNPAN_GYOUSHA_CD",
                "UNPAN_GYOUSHA_NAME",
                "SHASHU_CD",
                "SHARYOU_CD",
                "SHARYOU_NAME_RYAKU",
                "UNTENSHA_CD",
                "KEITAI_KBN_CD",
                "DAIKAN_KBN",
                "MANIFEST_SHURUI_CD",
                "MANIFEST_TEHAI_CD",
                "DENPYOU_BIKOU",
                "TAIRYUU_BIKOU"
            };

        /// <summary>
        /// 取得した取引先エンティティを保持する
        /// </summary>
        private List<M_TORIHIKISAKI> torihikisakiList = new List<M_TORIHIKISAKI>();

        /// <summary>
        /// 取得したマニフェスト種類エンティティを保持する
        /// </summary>
        private List<M_MANIFEST_SHURUI> manifestShuruiList = new List<M_MANIFEST_SHURUI>();

        /// <summary>
        /// 取得したマニフェスト手配エンティティを保持する
        /// </summary>
        private List<M_MANIFEST_TEHAI> manifestTehaiList = new List<M_MANIFEST_TEHAI>();

        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;

        /// <summary>
        /// 出荷入力専用DBアクセッサー
        /// </summary>
        internal Shougun.Core.SalesPayment.SyukkaNyuuryoku.Accessor.DBAccessor accessor;

        private System.Collections.Specialized.StringCollection DenpyouCtrl = new System.Collections.Specialized.StringCollection();

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary> コンストラクタ </summary>
        public LogicClassShukka(ShukkaDenpyouikkatsuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フィールドの初期化
            this.form = targetForm;
            this.msgLogic = new MessageBoxShowLogic();
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();

            // Accessor
            this.accessor = new Shougun.Core.SalesPayment.SyukkaNyuuryoku.Accessor.DBAccessor();
            // Utility
            this.controlUtil = new ControlUtility();

            LogUtility.DebugMethodEnd();
        }

        /// <summary> 論理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 物理削除処理 </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary> 登録処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> 検索処理 </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary> 更新処理 </summary>
        /// <param name="errorFlag"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary> window初期化 </summary>
        /// <param name="joken">joken</param>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // イベントの初期化処理
                this.EventInit();

                // 画面の初期化

                this.getListTorihikisakiDefault();
                this.getListManifestShuruiDefault();
                this.getListManifestTeihaiDefault();

                this.DisplayInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary> イベント初期化 </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            /// 一括入力(F8)イベント生成
            this.form.bt_func8.Click -= new EventHandler(this.form.Nyuuryoku);
            this.form.bt_func8.Click += new EventHandler(this.form.Nyuuryoku);

            // 検索条件クリア(F11)イベント生成
            this.form.bt_func11.Click -= new EventHandler(this.form.kuria);
            this.form.bt_func11.Click += new EventHandler(this.form.kuria);

            // キャンセルボタン(F12)イベント生成
            this.form.bt_func12.DialogResult = DialogResult.Cancel;
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);

            // コントロールのイベント
            this.form.TORIHIKISAKI_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.NIZUMI_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.NIZUMI_GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.UNPAN_GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.SHARYOU_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            this.form.SHARYOU_CD.TextChanged += new EventHandler(this.SHARYOU_CD_TextChanged);

            this.form.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form.OnKeyPress);
            // 全てのコントロールのEnterイベントに追加
            foreach (Control ctrl in this.form.Controls)
            {
                ctrl.Enter -= new EventHandler(this.GetControlEnter);
                ctrl.Enter += new EventHandler(this.GetControlEnter);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 全コントロールのEnterイベントで必ず通る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetControlEnter(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if ((ctrl is TextBox || ctrl is GrapeCity.Win.MultiRow.GcMultiRow))
            {
                this.form.beforbeforControlName = this.form.beforControlName;
                this.form.beforControlName = ctrl.Name;
            }
        }

        /// <summary>
        /// 次のタブストップのコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public void GotoNextControl(bool foward)
        {
            Control control = NextFormControl(foward);
            if (control != null)
            {
                control.Focus();
            }
        }

        /// <summary>
        /// 現在のコントロールの次のタブストップコントールを探す
        /// </summary>
        /// <param name="foward"></param>
        /// <returns></returns>
        public Control NextFormControl(bool foward)
        {
            Control control = null;
            ICustomAutoChangeBackColor autochange = null;
            bool startflg = false;
            List<string> formControlNameList = new List<string>();

            formControlNameList.AddRange(tabUiFormControlNames);
            if (foward == false)
            {
                formControlNameList.Reverse();
            }
            foreach (var controlName in formControlNameList)
            {
                control = controlUtil.FindControl(this.form, controlName);
                autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                if (control != null)
                {
                    if (startflg)
                    {
                        // 次のコントロール
                        if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                        {
                            return control;
                        }
                    }
                    else if (this.form.ActiveControl != null && this.form.ActiveControl.Equals(control))
                    {   // 現在のactiveコントロ－ル
                        startflg = true;
                    }
                }
            }

            // 詳細でタブストップが無い場合最初から検索
            foreach (var controlName in formControlNameList)
            {
                control = controlUtil.FindControl(this.form, controlName);
                autochange = (ICustomAutoChangeBackColor)controlUtil.FindControl(this.form, controlName);
                if (control != null)
                {
                    if (control.TabStop == true && control.Visible == true && autochange.ReadOnly == false)
                    {
                        return control;
                    }
                }
            }
            return control;
        }

        /// <summary>
        /// 表示制御
        /// </summary>
        private void DisplayInit()
        {
            this.form.KEITAI_KBN_CD.PopupDataHeaderTitle = new string[] { "形態区分CD", "形態区分名" };
            this.form.KEITAI_KBN_CD.PopupDataSource = this.CreateKeitaiKbnPopupDataSource();
        }

        /// <summary>
        /// 形態区分選択ポップアップ用DataSource生成
        /// デザイナのプロパティ設定からでは絞り込み条件が作れないため、
        /// DataSourceを渡す方法でポップアップを表示する。
        /// </summary>
        /// <returns></returns>
        internal DataTable CreateKeitaiKbnPopupDataSource()
        {
            var allKeitaiKbn = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>().GetAllValidData(new M_KEITAI_KBN());
            var dt = EntityUtility.EntityToDataTable(allKeitaiKbn);

            if (dt.Rows.Count == 0)
            {
                return dt;
            }

            var sortedDt = new DataTable();
            sortedDt.Columns.Add(dt.Columns["KEITAI_KBN_CD"].ColumnName, dt.Columns["KEITAI_KBN_CD"].DataType);
            sortedDt.Columns.Add(dt.Columns["KEITAI_KBN_NAME_RYAKU"].ColumnName, dt.Columns["KEITAI_KBN_NAME_RYAKU"].DataType);

            foreach (DataRow r in dt.Rows)
            {
                if (r["DENSHU_KBN_CD"] != null
                    && (r["DENSHU_KBN_CD"].ToString().Equals(SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA.ToString())
                        || r["DENSHU_KBN_CD"].ToString().Equals(SalesPaymentConstans.DENSHU_KBN_CD_KYOTU.ToString()))
                    )
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
            }

            return sortedDt;
        }

        /// <summary>
        /// 諸口区分用プレビューキーダウンイベント
        /// 諸口区分が存在する取引先、業者、現場で使用する
        /// ※例外として車輌でも使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDownForShokuchikbnCheck(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    this.form.pressedEnterOrTab = true;
                    break;

                default:
                    this.form.pressedEnterOrTab = false;
                    break;
            }
        }

        public void SHARYOU_CD_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.isSelectingSharyouCd = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                return;
            }
        }

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        internal void Kuria()
        {
            this.form.KYOTEN_CD.Text = string.Empty;
            this.form.KYOTEN_NAME_RYAKU.Text = string.Empty;
            this.form.DENPYOU_DATE.Value = string.Empty;
            this.form.URIAGE_DATE.Value = string.Empty;
            this.form.SHIHARAI_DATE.Value = string.Empty;
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.ReadOnly = true;
            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;

            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.ReadOnly = true;

            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME.Text = string.Empty;
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;

            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;

            this.form.UNTENSHA_CD.Text = string.Empty;
            this.form.UNTENSHA_NAME.Text = string.Empty;

            this.form.KEITAI_KBN_CD.Text = string.Empty;
            this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

            this.form.DAIKAN_KBN.Text = string.Empty;
            this.form.DAIKAN_KBN_NAME.Text = string.Empty;

            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;

            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;

            this.form.DENPYOU_BIKOU.Text = string.Empty;
            this.form.TAIRYUU_BIKOU.Text = string.Empty;
        }

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki(out bool catchErr)
        {
            catchErr = false;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                ret = true;
                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var oldTorihikisakiCd = this.tmpTorihikisakiCd;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (this.form.isInputError || (String.IsNullOrEmpty(inputTorihikisakiCd) || !this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) ||
                    (this.tmpTorihikisakiCd.Equals(inputTorihikisakiCd) && string.IsNullOrEmpty(this.form.TORIHIKISAKI_NAME_RYAKU.Text)))
                    || this.form.isFromSearchButton)
                {
                    //　初期化
                    //this.tmpTorihikisakiCd = string.Empty;
                    this.form.isInputError = false;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Tag = string.Empty;

                    if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                    {
                        var torihikisakiEntity = this.accessor.GetTorihikisaki(inputTorihikisakiCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                        if (catchErr)
                        {
                            return false;
                        }
                        if (null == torihikisakiEntity)
                        {
                            this.form.isInputError = true;
                            msgLogic.MessageBoxShow("E020", "取引先");
                            this.form.TORIHIKISAKI_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            if (CheckTorihikisakiAndKyotenCd(torihikisakiEntity, this.form.TORIHIKISAKI_CD.Text))
                            {
                                // 取引先の拠点と入力された拠点コードの関連チェックOK
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                                this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;
                            }
                            else
                            {
                                this.form.isInputError = true;
                                this.form.TORIHIKISAKI_CD.Focus();
                                isError = true;
                                ret = false;
                            }
                        }

                        if (ret)
                        {
                            // 取引先名
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                            this.tmpTorihikisakiCd = torihikisakiEntity.TORIHIKISAKI_CD;

                            // 諸口区分チェック
                            if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 取引先名編集可
                                this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = false;
                                //this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = true;
                                this.form.TORIHIKISAKI_NAME_RYAKU.TabStop = GetTabStop("TORIHIKISAKI_NAME_RYAKU");    // No.3822
                                this.form.TORIHIKISAKI_NAME_RYAKU.Tag = this.torihikisakiHintText;
                                this.form.TORIHIKISAKI_NAME_RYAKU.Focus();

                                ret = false;
                            }
                            else
                            {
                                if (!this.form.oldShokuchiKbn)
                                {
                                    ret = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // 関連項目クリア
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }

                    if (!isError)
                    {
                        if (!oldTorihikisakiCd.Equals(inputTorihikisakiCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("CheckTorihikisaki", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckTorihikisaki", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        /// <summary>
        /// 取引先の拠点コードと入力された拠点コードの関連チェック
        /// </summary>
        /// <param name="torihikisakiEntity">取引先エンティティ</param>
        /// <param name="TorihikisakiCd">取引先CD</param>
        /// <returns>True：チェックOK False：チェックNG</returns>
        internal bool CheckTorihikisakiAndKyotenCd(M_TORIHIKISAKI torihikisakiEntity, string TorihikisakiCd)
        {
            bool returnVal = false;

            if (string.IsNullOrEmpty(TorihikisakiCd))
            {
                // 取引先の入力がない場合はチェック対象外
                returnVal = true;
                return returnVal;
            }

            if (torihikisakiEntity == null)
            {
                // 取引先マスタを引数の取引先CDで取得しなおす
                bool catchErr = false;
                torihikisakiEntity = this.accessor.GetTorihikisaki(TorihikisakiCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr) { return returnVal; }
            }

            if (torihikisakiEntity != null)
            {
                if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
                {
                    if (SqlInt16.Parse(this.form.KYOTEN_CD.Text) == torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD
                        || torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString().Equals(SalesPaymentConstans.KYOTEN_ZENSHA))
                    {
                        // 入力画面の拠点コードと取引先の拠点コードが等しいか、取引先の拠点コードが99（全社)の場合
                        returnVal = true;
                    }
                    else
                    {
                        // 入力画面の拠点コードと取引先の拠点コードが等しくない場合
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E146");
                        this.form.TORIHIKISAKI_CD.Focus();
                    }
                }
                else
                {   // 拠点が指定されていない場合
                    returnVal = true;   // No.2865
                }
            }
            else
            {
                returnVal = true;
            }

            return returnVal;
        }

        /// <summary>
        /// タブストップ情報取得(詳細含まず)
        /// </summary>
        /// <returns></returns>
        private bool GetTabStop(string cname)
        {
            //bool tabstop = false;
            //for (var i = 0; i < DenpyouCtrl.Count; i++)
            //{
            //    string str = DenpyouCtrl[i];
            //    int ctpos = str.IndexOf(':');
            //    string controlName = str.Substring(0, ctpos);

            //    if (cname.Equals(controlName))
            //    {
            //        int nmpos = str.IndexOf(':', ctpos + 1);
            //        int tspos = str.IndexOf(':', nmpos + 1);
            //        string tbstop = str.Substring(nmpos + 1, tspos - nmpos - 1);

            //        Control control = controlUtil.FindControl(this.form, controlName);
            //        if (control == null)
            //        {
            //            continue;
            //        }
            //        if (tbstop.Equals("True"))
            //        {
            //            tabstop = true;
            //        }
            //        break;
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha(out bool catchErr)
        {
            catchErr = false;
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputGyoushaCd = this.form.GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (this.form.isInputError || (String.IsNullOrEmpty(inputGyoushaCd) || !this.tmpGyousyaCd.Equals(inputGyoushaCd) ||
                    (this.tmpGyousyaCd.Equals(inputGyoushaCd) && string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU.Text)))
                    || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.isInputError = false;
                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                    this.form.GYOUSHA_NAME_RYAKU.Tag = String.Empty;
                    this.form.GYOUSHA_NAME_RYAKU.TabStop = false;
                    if (!this.tmpGyousyaCd.Equals(inputGyoushaCd))
                    {
                        this.form.GENBA_CD.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;
                    }
                    // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    if (String.IsNullOrEmpty(inputGyoushaCd))
                    {
                        // 同時に現場コードもクリア
                        this.form.GENBA_CD.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                        this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                        this.form.GENBA_NAME_RYAKU.Tag = String.Empty;
                        this.form.GENBA_NAME_RYAKU.TabStop = false;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }
                    else
                    {
                        var gyoushaEntity = this.accessor.GetGyousha(inputGyoushaCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                        if (catchErr)
                        {
                            return false;
                        }
                        if (null == gyoushaEntity)
                        {
                            // エラーメッセージ
                            msgLogic.MessageBoxShow("E020", "業者");
                            this.form.GYOUSHA_CD.Focus();
                            this.form.isInputError = true;
                            ret = false;
                        }
                        else if (false == gyoushaEntity.GYOUSHAKBN_SHUKKA)
                        {
                            // エラーメッセージ
                            // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            msgLogic.MessageBoxShow("E020", "業者");
                            this.form.GYOUSHA_CD.Focus();
                            this.form.isInputError = true;
                            ret = false;
                        }
                        else
                        {
                            // 業者名
                            this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;

                            // 諸口区分チェック
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 業者名編集可
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GYOUSHA_NAME_RYAKU.Focus();

                                ret = false;
                            }
                            else
                            {
                                if (!this.form.oldShokuchiKbn)
                                {
                                    ret = false;
                                }
                            }

                            // 取引先を取得
                            var torihikisakiEntity = this.accessor.GetTorihikisaki(gyoushaEntity.TORIHIKISAKI_CD, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                            if (catchErr)
                            {
                                return false;
                            }
                            if (null != torihikisakiEntity)
                            {
                                this.form.TORIHIKISAKI_CD.Text = gyoushaEntity.TORIHIKISAKI_CD;
                                // 取引先チェック呼び出し
                                ret = this.CheckTorihikisaki(out catchErr);
                                if (catchErr)
                                {
                                    throw new Exception("");
                                }
                            }

                            if (true == ret)
                            {
                                // 現場が入力されていれば現場との関連チェック
                                var genbaCd = this.form.GENBA_CD.Text;
                                if (!String.IsNullOrEmpty(genbaCd))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(inputGyoushaCd);
                                    var genbaEntity = genbaEntityList.Where(g => g.GENBA_CD == genbaCd).FirstOrDefault();
                                    if (null != genbaEntity)
                                    {
                                        // 現場チェック呼び出し
                                        ret = this.CheckGenba(out catchErr);
                                        if (catchErr)
                                        {
                                            throw new Exception("");
                                        }
                                    }
                                    else
                                    {
                                        // 一致するものがなければ、入力されている現場を消す
                                        this.form.GENBA_CD.Text = String.Empty;
                                        this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                                    }
                                }
                            }
                            // 諸口区分チェック
                            this.form.isSetShokuchiForcus = false;
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 現場を再設定
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");    // No.3822
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GYOUSHA_NAME_RYAKU.Focus();
                                this.form.isSetShokuchiForcus = true;
                            }
                        }
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                if (!string.IsNullOrEmpty(ex2.Message))
                {
                    LogUtility.Error("CheckGyousha", ex2);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                catchErr = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckGyousha", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputGenbaCd = this.form.GENBA_CD.Text;
                var inputGyoushaCd = this.form.GYOUSHA_CD.Text;

                // 検索での呼び出しに時間がかかる START
                var gyoushaEntity = this.accessor.GetGyousha(this.form.GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr)
                {
                    return false;
                }
                // 検索での呼び出しに時間がかかる END

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if (this.form.isInputError || (String.IsNullOrEmpty(inputGenbaCd) || !this.tmpGenbaCd.Equals(inputGenbaCd)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.isInputError = false;
                    this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                    this.form.GENBA_NAME_RYAKU.ReadOnly = true;
                    this.form.GENBA_NAME_RYAKU.Tag = string.Empty;
                    this.form.GENBA_NAME_RYAKU.TabStop = false;

                    if (String.IsNullOrEmpty(inputGenbaCd))
                    {
                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(inputGyoushaCd))
                        {
                            msgLogic.MessageBoxShow("E051", "業者");
                            this.form.GENBA_CD.Text = string.Empty;
                            this.form.GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }

                        // 検索での呼び出しに時間がかかる START
                        var genbaEntityList = this.accessor.GetGenbaList(inputGyoushaCd, inputGenbaCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now);
                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            // エラーメッセージ
                            this.form.isInputError = true;
                            // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            msgLogic.MessageBoxShow("E020", "現場");
                            // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                            this.form.GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                        }
                        else
                        {
                            M_GENBA genba = new M_GENBA();
                            genba = genbaEntityList[0];
                            this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;

                            if (null == gyoushaEntity)
                            {
                                ret = false;
                            }
                            // 業者の諸口区分チェック
                            else if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                // 業者名編集可
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME1;
                                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = false;
                                this.form.GYOUSHA_NAME_RYAKU.TabStop = GetTabStop("GYOUSHA_NAME_RYAKU");
                                this.form.GYOUSHA_NAME_RYAKU.Tag = this.gyoushaHintText;
                                this.form.GENBA_NAME_RYAKU.Focus();
                            }
                            else
                            {
                                this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                            }

                            // 取引先を取得
                            M_TORIHIKISAKI torihikisakiEntity = null;
                            if (!string.IsNullOrEmpty(genba.TORIHIKISAKI_CD))
                            {
                                // 検索での呼び出しに時間がかかる START
                                torihikisakiEntity = this.torihikisakiList.Where(t => t.TORIHIKISAKI_CD == genba.TORIHIKISAKI_CD).FirstOrDefault();
                                // 検索での呼び出しに時間がかかる END
                                if (torihikisakiEntity != null)
                                {
                                    // 取引先設定
                                    this.form.TORIHIKISAKI_CD.Text = torihikisakiEntity.TORIHIKISAKI_CD;
                                    if (torihikisakiEntity.SHOKUCHI_KBN.IsTrue)
                                    {
                                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME1;
                                    }
                                    else
                                    {
                                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                                    }
                                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                                    this.form.pressedEnterOrTab = false;
                                    ret = this.CheckTorihikisaki(out catchErr);
                                    if (catchErr)
                                    {
                                        throw new Exception("");
                                    }
                                }
                            }

                            // TODO: 【2次】営業担当者チェックの呼び出し

                            // 現場：諸口区分チェック
                            this.form.isSetShokuchiForcus = false;
                            if (genba.SHOKUCHI_KBN.IsTrue)
                            {
                                // 現場名編集可
                                this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                                this.form.GENBA_NAME_RYAKU.ReadOnly = false;
                                this.form.GENBA_NAME_RYAKU.TabStop = GetTabStop("GENBA_NAME_RYAKU");    // No.3822
                                this.form.GENBA_NAME_RYAKU.Tag = genbaHintText;
                                this.form.GENBA_CD.Focus();
                                this.form.isSetShokuchiForcus = true;
                            }

                            //// Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                            if (ret)
                                this.MoveToNextControlForShokuchikbnCheck(this.form.GENBA_CD);

                            ret = true;

                            // マニ種類の自動表示
                            // 初期化
                            this.form.MANIFEST_SHURUI_CD.Text = string.Empty;
                            this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = string.Empty;

                            if (!genba.MANIFEST_SHURUI_CD.IsNull)
                            {
                                // 検索での呼び出しに時間がかかる START
                                var manifestShuruiEntity = this.manifestShuruiList.Where(t => t.MANIFEST_SHURUI_CD.ToString() == genba.MANIFEST_SHURUI_CD.ToString()).FirstOrDefault();
                                // 検索での呼び出しに時間がかかる END
                                if (manifestShuruiEntity != null && !string.IsNullOrEmpty(manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU))
                                {
                                    this.form.MANIFEST_SHURUI_CD.Text = Convert.ToString(genba.MANIFEST_SHURUI_CD);
                                    this.form.MANIFEST_SHURUI_NAME_RYAKU.Text = manifestShuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                                }
                            }

                            // マニ手配の自動表示
                            // 初期化
                            this.form.MANIFEST_TEHAI_CD.Text = string.Empty;
                            this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = string.Empty;

                            if (!genba.MANIFEST_TEHAI_CD.IsNull)
                            {
                                // 検索での呼び出しに時間がかかる START
                                var manifestTehaiEntity = this.manifestTehaiList.Where(t => t.MANIFEST_TEHAI_CD.ToString() == genba.MANIFEST_TEHAI_CD.ToString()).FirstOrDefault();
                                // 検索での呼び出しに時間がかかる END
                                if (manifestTehaiEntity != null && !string.IsNullOrEmpty(manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU))
                                {
                                    this.form.MANIFEST_TEHAI_CD.Text = Convert.ToString(genba.MANIFEST_TEHAI_CD);
                                    this.form.MANIFEST_TEHAI_NAME_RYAKU.Text = manifestTehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                                }
                            }
                        }
                    }

                    if (!isError)
                    {
                        if (!this.tmpGenbaCd.Equals(inputGenbaCd))
                        {
                            // 営業担当者の設定
                            this.SetEigyouTantousha(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, this.form.TORIHIKISAKI_CD.Text);
                        }
                    }
                }
                else
                {
                    ret = false;
                }

            }
            catch (SQLRuntimeException ex2)
            {
                if (!string.IsNullOrEmpty(ex2.Message))
                {
                    LogUtility.Error("CheckGenba", ex2);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckGenba", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
                this.form.isInputError = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;

        }

        /// <summary>
        /// 営業担当者の表示（現場マスタ、業者マスタ、取引先マスタを元に）
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        internal void SetEigyouTantousha(string genbaCd, string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(genbaCd, gyoushaCd, torihikisakiCd);

            M_GENBA genbaEntity = new M_GENBA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                // 業者CD入力あり
                if (!string.IsNullOrEmpty(genbaCd))
                {
                    // 現場CD入力あり
                    bool catchErr = false;
                    genbaEntity = this.accessor.GetGenba(gyoushaCd, genbaCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                    if (catchErr) { throw new Exception(""); }
                    if (genbaEntity != null)
                    {
                        // コードに対応する現場マスタが存在する
                        eigyouTantouCd = genbaEntity.EIGYOU_TANTOU_CD;
                        if (!string.IsNullOrEmpty(eigyouTantouCd))
                        {
                            // 現場マスタに営業担当者の設定がある場合
                            shainEntity = this.accessor.GetShain(eigyouTantouCd);
                            if (shainEntity != null)
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できた場合
                                if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                                {
                                    // 取得した社員マスタの社員名略が設定されている場合
                                    this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                                }
                                else
                                {
                                    // 取得した社員マスタの社員名略が設定されていない場合
                                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                                }
                            }
                            else
                            {
                                // 現場CDで取得した現場マスタの営業担当者コードで、社員マスタを取得できない場合
                                GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                            }
                        }
                        else
                        {
                            // 現場マスタに営業担当者の設定がない場合
                            GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                        }
                    }
                }
                else
                {
                    // 現場CD入力なし
                    GetEigyou_TantoushaOfGyousha(gyoushaCd, torihikisakiCd);
                }
            }
            else
            {
                // 業者CD入力なし
                GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタの営業担当者コードからの営業担当者取得(業者CD入力あり、業者マスタに存在することが前提)
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfGyousha(string gyoushaCd, string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, torihikisakiCd);

            M_GYOUSHA gyoushaEntity = new M_GYOUSHA();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;
            bool catchErr = false;
            gyoushaEntity = this.accessor.GetGyousha(gyoushaCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
            if (catchErr) { throw new Exception(""); }
            if (gyoushaEntity != null)
            {
                // コードに対応する業者マスタが存在する
                eigyouTantouCd = gyoushaEntity.EIGYOU_TANTOU_CD;
                if (!string.IsNullOrEmpty(eigyouTantouCd))
                {
                    // 業者マスタに営業担当者の設定がある場合
                    shainEntity = this.accessor.GetShain(eigyouTantouCd);
                    if (shainEntity != null)
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できた場合
                        if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                        {
                            // 取得した社員マスタの社員名略が設定されている場合
                            this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                            this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                        }
                        else
                        {
                            // 取得した社員マスタの社員名略が設定されていない場合
                            GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                        }
                    }
                    else
                    {
                        // 業者CDで取得した業者マスタの営業担当者コードで、社員マスタを取得できない場合
                        GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                    }
                }
                else
                {
                    // 業者マスタに営業担当者の設定がない場合
                    GetEigyou_TantoushaOfTorihikisaki(torihikisakiCd);
                }
            }
            else
            {
                // コードに対応する業者マスタが存在しない
                // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "業者");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先マスタの営業担当者コードからの営業担当者取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        private void GetEigyou_TantoushaOfTorihikisaki(string torihikisakiCd)
        {
            LogUtility.DebugMethodStart(torihikisakiCd);

            M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
            M_SHAIN shainEntity = new M_SHAIN();
            string eigyouTantouCd = null;

            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                // 取引先CD入力あり
                bool catchErr = false;
                torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisakiCd, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr) { throw new Exception(""); }
                if (torihikisakiEntity != null)
                {
                    // コードに対応する取引先マスタが存在する
                    eigyouTantouCd = torihikisakiEntity.EIGYOU_TANTOU_CD;
                    if (!string.IsNullOrEmpty(eigyouTantouCd))
                    {
                        // 取引先マスタに営業担当者の設定がある場合
                        shainEntity = this.accessor.GetShain(eigyouTantouCd);
                        if (shainEntity != null)
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できた場合
                            if (!string.IsNullOrEmpty(shainEntity.SHAIN_NAME_RYAKU))
                            {
                                // 取得した社員マスタの社員名略が設定されている場合
                                this.form.EIGYOU_TANTOUSHA_CD.Text = shainEntity.SHAIN_CD;
                                this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                            }
                            else
                            {
                                // 取得した社員マスタの社員名略が設定されていない場合
                                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                            }
                        }
                        else
                        {
                            // 取引先CDで取得した取引先マスタの営業担当者コードで、社員マスタを取得できない場合
                            this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                            this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                        }
                    }
                    else
                    {
                        // 取引先マスタに営業担当者の設定がない場合
                        this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                    }
                }
                else
                {
                    // コードに対応する取引先マスタが存在しない
                    // ただし、マスタ存在チェックはこの前になされているので、ここを通ることはない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    return;
                }
            }
            else
            {
                // 取引先CD入力なし
                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積業者CDの存在チェック
        /// </summary>
        public virtual bool CheckNizumiGyoushaCd(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputNioroshiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNioroshiGyoushaCd) || !this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd) ||
                       (this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd) && string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_NAME.Text)))
                       || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GYOUSHA_NAME.Tag = string.Empty;
                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = false;
                    if (!this.tmpNizumiGyoushaCd.Equals(inputNioroshiGyoushaCd))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                        this.form.NIZUMI_GENBA_NAME.TabStop = false;
                        this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;
                    }
                    // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                        this.form.NIZUMI_GENBA_NAME.TabStop = false;
                        this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                        if (!this.form.oldShokuchiKbn || this.form.keyEventArgs.Shift)
                        {
                            // フレームワーク側の再フォーカス処理を行わない
                            ret = false;
                        }
                        else
                        {
                            // フレームワーク側の再フォーカス処理を行う
                            ret = true;
                        }
                    }
                    else
                    {
                        var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                        if (catchErr) { return ret; }
                        if (gyousha != null)
                        {
                            // PKは1つなので複数ヒットしない
                            if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue
                                && (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue
                                || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                            {
                                // 荷積業者名
                                this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                    this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");    // No.3822
                                    this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                    this.form.NIZUMI_GYOUSHA_NAME.Focus();
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }

                                // 入力済の荷積現場との関連チェック
                                bool isContinue = false;
                                M_GENBA genba = new M_GENBA();
                                if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                                {
                                    var genbaEntityList = this.accessor.GetGenbaByGyousha(this.form.NIZUMI_GYOUSHA_CD.Text);
                                    if (genbaEntityList != null || genbaEntityList.Length >= 1)
                                    {
                                        foreach (M_GENBA genbaEntity in genbaEntityList)
                                        {
                                            if (this.form.NIZUMI_GENBA_CD.Text.Equals(genbaEntity.GENBA_CD)
                                                && (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue))
                                            {
                                                isContinue = true;
                                                genba = genbaEntity;
                                                ret = true;
                                                break;
                                            }
                                        }
                                        if (!isContinue)
                                        {
                                            // 一致するものがないので、入力されている現場CDを消す
                                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                                            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                                        }
                                        else
                                        {
                                            // 一致する現場CDがあれば、現場名を再設定する
                                            if (genba.SHOKUCHI_KBN.IsTrue)
                                            {
                                                this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                                this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                                this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                                this.form.NIZUMI_GENBA_NAME.Focus();
                                            }
                                            else
                                            {
                                                this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // エラーメッセージ
                                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                msgLogic.MessageBoxShow("E020", "荷卸業者");
                                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                this.form.NIZUMI_GYOUSHA_CD.Focus();
                            }
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "荷積業者");
                            this.form.NIZUMI_GYOUSHA_CD.Focus();
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        // <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        internal bool CheckNizumiGenbaCd(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();
                bool isError = false;

                var msgLogic = new MessageBoxShowLogic();
                var inputNIZUMIGenbaCd = this.form.NIZUMI_GENBA_CD.Text;

                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputNIZUMIGenbaCd) || !this.tmpNizumiGenbaCd.Equals(inputNIZUMIGenbaCd)) ||
                    (this.tmpNizumiGenbaCd.Equals(inputNIZUMIGenbaCd) && string.IsNullOrEmpty(this.form.NIZUMI_GENBA_NAME.Text))
                    || this.form.isFromSearchButton)
                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                {
                    // 初期化
                    this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                    this.form.NIZUMI_GENBA_NAME.ReadOnly = true;
                    this.form.NIZUMI_GENBA_NAME.TabStop = false;
                    this.form.NIZUMI_GENBA_NAME.Tag = string.Empty;

                    if (string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                    {
                        this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
                        if (this.form.oldShokuchiKbn)
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                        {
                            msgLogic.MessageBoxShow("E051", "荷積業者");
                            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                            ret = false;
                            return ret;
                        }

                        //var genbaEntityList = this.accessor.GetGenba(this.form.NIZUMI_GENBA_CD.Text);
                        var genbaEntityList = this.accessor.GetGenbaList(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text, this.form.DENPYOU_DATE.Value, System.DateTime.Now);
                        M_GENBA genba = new M_GENBA();

                        if (genbaEntityList == null || genbaEntityList.Length < 1)
                        {
                            this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                            // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                            msgLogic.MessageBoxShow("E020", "荷積現場");
                            // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                            this.form.NIZUMI_GENBA_CD.Focus();
                            isError = true;
                        }
                        else
                        {
                            //genba = this.accessor.GetGenba(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text);
                            genba = genbaEntityList[0];
                            // 荷積業者名入力チェック
                            if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                            {
                                // エラーメッセージ
                                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                msgLogic.MessageBoxShow("E020", "荷積現場");
                                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                            // 荷積業者と荷積現場の関連チェック
                            else if (genba == null)
                            {
                                // 一致するデータがないのでエラー
                                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                msgLogic.MessageBoxShow("E020", "荷積現場");
                                // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                this.form.NIZUMI_GENBA_CD.Focus();
                                isError = true;
                            }
                            else
                            {
                                // 業者設定
                                var gyousha = this.accessor.GetGyousha(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                                if (catchErr) { return ret; }
                                if (gyousha != null)
                                {
                                    // PKは1つなので複数ヒットしない
                                    if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue
                                        && (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue
                                        || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                                    {
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        // 荷卸業者名
                                        this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                        if (gyousha.SHOKUCHI_KBN.IsTrue)
                                        {
                                            this.form.NIZUMI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                            this.form.NIZUMI_GYOUSHA_NAME.ReadOnly = false;
                                            this.form.NIZUMI_GYOUSHA_NAME.TabStop = GetTabStop("NIZUMI_GYOUSHA_NAME");
                                            this.form.NIZUMI_GYOUSHA_NAME.Tag = this.nizumiGyoushaHintText;
                                        }
                                    }
                                }

                                // 事業場区分、現場区分チェック
                                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                                {
                                    this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;

                                    // 諸口区分チェック
                                    if (genba.SHOKUCHI_KBN.IsTrue)
                                    {
                                        // 荷積現場名編集可
                                        this.form.NIZUMI_GENBA_NAME.Text = genba.GENBA_NAME1;
                                        this.form.NIZUMI_GENBA_NAME.ReadOnly = false;
                                        this.form.NIZUMI_GENBA_NAME.TabStop = GetTabStop("NIZUMI_GENBA_NAME");    // No.3822
                                        this.form.NIZUMI_GENBA_NAME.Tag = this.nizumiGenbaHintText;
                                        this.form.NIZUMI_GENBA_NAME.Focus();
                                    }

                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                                else
                                {
                                    // 一致するデータがないのでエラー
                                    this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = true;
                                    // 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                    msgLogic.MessageBoxShow("E020", "荷積現場");
                                    // 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                    this.form.NIZUMI_GENBA_CD.Focus();
                                    isError = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGenbaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGenbaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        internal void CheckSharyou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_SHARYOU[] sharyouEntitys = null;

                // 何もしないとポップアップが起動されてしまう可能性があるため
                // 変更されたかチェックする
                if (sharyouCd.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 複数ヒットするCDを入力→ポップアップで何もしない→一度ポップアップを閉じて再度ポップアップからデータを選択
                    // したときに色が戻らない問題の対策のため、存在チェックだけは実施する。
                    sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null);
                    if (sharyouEntitys != null && sharyouEntitys.Length == 1)
                    {
                        // 一意に識別できる場合は色を戻す
                        this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                        this.form.oldSharyouShokuchiKbn = false;
                        this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;
                    }
                    return;
                }

                // 初期化
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                }
                this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                this.form.oldSharyouShokuchiKbn = false;
                this.form.SHARYOU_NAME_RYAKU.Tag = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.TabStop = false;
                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    sharyouCd = string.Empty;
                    this.form.isSelectingSharyouCd = false;
                    return;
                }

                sharyouCd = this.form.SHARYOU_CD.Text;
                unpanGyousha = this.form.UNPAN_GYOUSHA_CD.Text;

                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null);

                // マスタ存在チェック
                if (sharyouEntitys == null || sharyouEntitys.Length < 1)
                {
                    // 車輌名を編集可
                    this.ChangeShokuchiSharyouDesign();
                    // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                    this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                    this.form.SHARYOU_NAME_RYAKU.Focus();

                    this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

                    if (!this.form.isSelectingSharyouCd)
                    {
                        this.form.isSelectingSharyouCd = true;
                        return;
                    }
                    return;
                }
                else
                {
                    this.form.oldSharyouShokuchiKbn = false;
                }

                // ポップアップから戻ってきたときに運搬業者名が無いため取得
                bool catchErr = false;
                M_GYOUSHA unpanGyousya = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                if (catchErr) { return; }
                if (unpanGyousya != null)
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousya.GYOUSHA_NAME_RYAKU;
                }

                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_NAME.Text))
                {
                    M_SHARYOU sharyou = new M_SHARYOU();

                    // 運搬業者チェック
                    bool isCheck = false;
                    foreach (M_SHARYOU sharyouEntity in sharyouEntitys)
                    {
                        if (sharyouEntity.GYOUSHA_CD.Equals(this.form.UNPAN_GYOUSHA_CD.Text))
                        {
                            isCheck = true;
                            sharyou = sharyouEntity;
                            // 諸口区分チェック
                            if (unpanGyousya != null)
                            {
                                if (unpanGyousya.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 運搬業者名編集可
                                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                    //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                                    this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
                                    this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                }
                            }
                            break;
                        }
                    }

                    if (isCheck)
                    {
                        // 車輌データセット
                        SetSharyou(sharyou);
                        return;
                    }
                    else
                    {
                        // エラーメッセージ
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E062", "運搬業者");
                        this.form.SHARYOU_CD.Focus();
                        return;
                    }
                }
                else
                {
                    if (sharyouEntitys.Length > 1)
                    {
                        // 複数レコード
                        // 車輌名を編集可
                        this.form.oldSharyouShokuchiKbn = true;
                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                        //this.form.SHARYOU_NAME_RYAKU.TabStop = true;
                        this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");    // No.3822
                        this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
                        // 自由入力可能であるため車輌名の色を変更
                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColorBlue;

                        if (!this.form.isSelectingSharyouCd)
                        {
                            sharyouCd = string.Empty;
                            unpanGyousha = string.Empty;
                            this.form.isSelectingSharyouCd = true;
                            this.form.SHARYOU_CD.Focus();

                            this.form.FocusOutErrorFlag = true;

                            // この時は車輌CDを検索条件に含める
                            this.PopUpConditionsSharyouSwitch(true);

                            // 検索ポップアップ起動
                            CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                            this.PopUpConditionsSharyouSwitch(false);

                            // PopUpでF12押下された場合
                            //（戻り値でF12が押下されたか判断できない為、運搬業者の有無で判断）
                            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                            {
                                // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                                this.ChangeShokuchiSharyouDesign();
                                if (string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
                                    this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                            }

                            this.form.FocusOutErrorFlag = false;
                            return;
                        }
                        else
                        {
                            // ポップアアップから戻ってきて車輌名へ遷移した場合
                            // マスタに存在しない場合、ユーザに車輌名を自由入力させる
                            this.ChangeShokuchiSharyouDesign();
                            this.form.SHARYOU_NAME_RYAKU.Text = ZeroSuppress(this.form.SHARYOU_CD);
                        }

                    }
                    else
                    {
                        // 一意レコード
                        // 車輌データセット
                        SetSharyou(sharyouEntitys[0]);
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                if (!string.IsNullOrEmpty(ex1.Message))
                {
                    LogUtility.Error("CheckSharyou", ex1);
                    this.msgLogic.MessageBoxShow("E093", "");
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("CheckSharyou", ex);
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌情報をセット
        /// </summary>
        /// <param name="sharyouEntity"></param>
        private void SetSharyou(M_SHARYOU sharyouEntity)
        {
            this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
            this.form.UNTENSHA_CD.Text = sharyouEntity.SHAIN_CD;
            this.form.SHASHU_CD.Text = sharyouEntity.SHASYU_CD;
            this.form.UNPAN_GYOUSHA_CD.Text = sharyouEntity.GYOUSHA_CD;

            // 運転者情報セット
            var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
            if (untensha != null)
            {
                this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
            }
            else
            {
                this.form.UNTENSHA_NAME.Text = string.Empty;
            }

            //車種情報セット
            var shashu = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
            if (shashu != null)
            {
                this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
            }
            else
            {
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME.Text = string.Empty;
            }

            this.MoveToNextControlForShokuchikbnCheck(this.form.SHARYOU_CD);

            bool catchErr = false;
            bool ret = this.CheckUnpanGyoushaCd(out catchErr);
            if (catchErr)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 運搬業者CDの存在チェック
        /// </summary>
        public virtual bool CheckUnpanGyoushaCd(out bool catchErr)
        {
            catchErr = false;
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();
                var inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(inputUnpanGyoushaCd) || !this.tmpUnpanGyoushaCd.Equals(inputUnpanGyoushaCd)) || this.form.isFromSearchButton || this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured)
                {
                    // 初期化
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
                    this.form.UNPAN_GYOUSHA_NAME.TabStop = false;
                    this.form.UNPAN_GYOUSHA_NAME.Tag = string.Empty;
                    var gyousha = this.accessor.GetGyousha(this.form.UNPAN_GYOUSHA_CD.Text, this.form.DENPYOU_DATE.Value, System.DateTime.Now, out catchErr);
                    if (catchErr) { return ret; }
                    if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                    {
                        if (gyousha != null)
                        {
                            if (gyousha.GYOUSHAKBN_SHUKKA.IsTrue && gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                            {
                                M_SHARYOU[] sharyouEntitys = null;
                                sharyouEntitys = this.accessor.GetSharyou(this.form.SHARYOU_CD.Text, this.form.UNPAN_GYOUSHA_CD.Text, null, null);

                                this.form.SHARYOU_CD.BackColor = SystemColors.Window;
                                this.form.SHARYOU_CD.AutoChangeBackColorEnabled = true;

                                if (sharyouEntitys == null)
                                {
                                    if (!this.form.oldSharyouShokuchiKbn)
                                    {
                                        // 車輌・車種をクリア
                                        this.form.SHARYOU_CD.Text = string.Empty;
                                        this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                                    }
                                    else
                                    {
                                        // 車輌名を編集可
                                        this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
                                        this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
                                        this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
                                    }
                                }
                                else if (sharyouEntitys != null)
                                {
                                    var sharyouEntity = sharyouEntitys[0];
                                    this.form.SHARYOU_CD.Text = sharyouEntity.SHARYOU_CD;
                                    this.form.oldSharyouShokuchiKbn = false;
                                    this.form.SHARYOU_NAME_RYAKU.Text = sharyouEntity.SHARYOU_NAME_RYAKU;
                                    this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;

                                    // 運転者情報セット
                                    var untensha = this.accessor.GetShain(sharyouEntity.SHAIN_CD);
                                    if (untensha != null)
                                    {
                                        this.form.UNTENSHA_CD.Text = untensha.SHAIN_CD;
                                        this.form.UNTENSHA_NAME.Text = untensha.SHAIN_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        this.form.UNTENSHA_CD.Text = string.Empty;
                                        this.form.UNTENSHA_NAME.Text = string.Empty;
                                    }

                                    // 車輌情報セット
                                    var shashuEntity = this.accessor.GetShashu(sharyouEntity.SHASYU_CD);
                                    if (shashuEntity != null)
                                    {
                                        this.form.SHASHU_CD.Text = shashuEntity.SHASHU_CD;
                                        this.form.SHASHU_NAME.Text = shashuEntity.SHASHU_NAME_RYAKU;
                                    }
                                    else
                                    {
                                        this.form.SHASHU_CD.Text = string.Empty;
                                        this.form.SHASHU_NAME.Text = string.Empty;
                                    }
                                }

                                this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                                // 諸口区分チェック
                                if (gyousha.SHOKUCHI_KBN.IsTrue)
                                {
                                    // 運搬業者名編集可
                                    this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                                    this.form.UNPAN_GYOUSHA_NAME.ReadOnly = false;
                                    //this.form.UNPAN_GYOUSHA_NAME.TabStop = true;
                                    this.form.UNPAN_GYOUSHA_NAME.TabStop = GetTabStop("UNPAN_GYOUSHA_NAME");    // No.3822
                                    this.form.UNPAN_GYOUSHA_NAME.Tag = this.unpanGyoushaHintText;
                                }
                                else
                                {
                                    if (this.form.oldShokuchiKbn)
                                    {
                                        ret = true;
                                    }
                                }
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E020", "運搬業者");
                                this.form.UNPAN_GYOUSHA_CD.Focus();
                                this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                            }
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "運搬業者");
                            this.form.UNPAN_GYOUSHA_CD.Focus();
                            this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.tmpUnpanGyoushaCd) && !this.form.oldSharyouShokuchiKbn)
                        {
                            this.form.SHARYOU_CD.Text = string.Empty;
                            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                            this.form.SHARYOU_NAME_RYAKU.ReadOnly = true;
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
        #region 車輌休動チェック
        internal bool SharyouDateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                string inputUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                string inputSharyouCd = this.form.SHARYOU_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
                //運搬業者CD
                workclosedsharyouEntry.GYOUSHA_CD = inputUnpanGyoushaCd;
                //車輌CD取得
                workclosedsharyouEntry.SHARYOU_CD = inputSharyouCd;
                //伝票日付取得
                workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "車輌", "伝票日付：" + workclosedsharyouEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!this.form.SHARYOU_CD.PopupSearchSendParams.Contains(sharyouParam))
                {
                    this.form.SHARYOU_CD.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = this.form.SHARYOU_CD.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (this.form.SHARYOU_CD.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        this.form.SHARYOU_CD.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        this.form.SHARYOU_CD.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 車輌CD、車輌名を諸口状態のデザインへ変更する
        /// </summary>
        internal void ChangeShokuchiSharyouDesign()
        {
            this.form.oldSharyouShokuchiKbn = true;
            this.form.SHARYOU_NAME_RYAKU.ReadOnly = false;
            this.form.SHARYOU_NAME_RYAKU.TabStop = GetTabStop("SHARYOU_NAME_RYAKU");
            this.form.SHARYOU_NAME_RYAKU.Tag = this.sharyouHinttext;
            // 自由入力可能であるため車輌名の色を変更
            this.form.SHARYOU_CD.AutoChangeBackColorEnabled = false;
            this.form.SHARYOU_CD.BackColor = sharyouCdBackColor;
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal void CheckUntensha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //参照モード、削除モードの場合は処理を行わない
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                    this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    return;
                }

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.UNTENSHA_CD.Text) || !this.tmpUntenshaCd.Equals(this.form.UNTENSHA_CD.Text)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.UNTENSHA_NAME.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                    {
                        // 運転者CDがなければ既にエラーが表示されているので何もしない。
                        return;
                    }

                    var shainEntity = this.accessor.GetShain(this.form.UNTENSHA_CD.Text);
                    if (shainEntity == null)
                    {
                        // エラーメッセージ
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "社員");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                        return;
                    }
                    else if (shainEntity.UNTEN_KBN.Equals(SqlBoolean.False))
                    {
                        // エラーメッセージ
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.form.UNTENSHA_CD.Focus();
                        this.tmpUntenshaCd = string.Empty;
                    }
                    else
                    {
                        this.form.UNTENSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUntensha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 運転者休動チェック
        internal bool UntenshaDateCheck(out bool catchErr)
        {
            catchErr = false;
            try
            {
                string inputUntenshaCd = this.form.UNTENSHA_CD.Text;
                string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
                //運転者CD取得
                workcloseduntenshaEntry.SHAIN_CD = inputUntenshaCd;
                //作業日取得
                workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);

                //取得テータ
                if (workcloseduntenshaList.Count() >= 1)
                {
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    msgLogic.MessageBoxShow("E206", "運転者", "伝票日付：" + workcloseduntenshaEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 形態区分チェック処理
        /// </summary>
        internal void CheckKeitaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 前回値と比較して変更がある場合 又は 検索ボタンから入力された場合
                if ((String.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text) || !this.tmpKeitaiKbnCd.Equals(this.form.KEITAI_KBN_CD.Text)) || this.form.isFromSearchButton)
                {
                    // 初期化
                    this.form.KEITAI_KBN_NAME_RYAKU.Text = string.Empty;

                    if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                    {
                        return;
                    }

                    short keitaiKbnCd;

                    if (!short.TryParse(this.form.KEITAI_KBN_CD.Text, out keitaiKbnCd))
                    {
                        return;
                    }

                    M_KEITAI_KBN kakuteiKbn = this.accessor.GetkeitaiKbn(keitaiKbnCd);
                    if (kakuteiKbn == null)
                    {
                        // エラーメッセージ
                        this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                        this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "形態区分");
                        this.form.KEITAI_KBN_CD.Focus();
                        tmpKeitaiKbnCd = string.Empty;
                        return;
                    }

                    var denshuKbnCd = (DENSHU_KBN)Enum.ToObject(typeof(DENSHU_KBN), (int)kakuteiKbn.DENSHU_KBN_CD);

                    switch (denshuKbnCd)
                    {
                        case DENSHU_KBN.SHUKKA:
                        case DENSHU_KBN.KYOUTSUU:
                            this.form.KEITAI_KBN_NAME_RYAKU.Text = kakuteiKbn.KEITAI_KBN_NAME_RYAKU;
                            break;

                        default:
                            // エラーメッセージ
                            this.form.KEITAI_KBN_CD.IsInputErrorOccured = true;
                            this.form.KEITAI_KBN_CD.BackColor = Constans.ERROR_COLOR;
                            this.form.KEITAI_KBN_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "形態区分");
                            this.form.KEITAI_KBN_CD.Focus();
                            tmpKeitaiKbnCd = string.Empty;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKeitaiKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ゼロサプレス処理
        /// </summary>
        /// <param name="source">入力コントロール</param>
        /// <returns>ゼロサプレス後の文字列</returns>
        private string ZeroSuppress(object source)
        {
            string result = string.Empty;

            // 該当コントロールの最大桁数を取得
            object obj;
            decimal charactersNumber;
            string text = PropertyUtility.GetTextOrValue(source);
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
                // 最大桁数が取得できない場合はそのまま
                return text;

            charactersNumber = (decimal)obj;
            if (charactersNumber == 0 || source == null || string.IsNullOrEmpty(text))
                // 最大桁数が0または入力値が空の場合はそのまま
                return text;

            var strCharactersUmber = text;
            if (strCharactersUmber.Contains("."))
                // 小数点を含む場合はそのまま
                return text;

            // ゼロサプレスした値を返す
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            string format = sb.Append('#', (int)charactersNumber).ToString();
            long val = 0;
            if (long.TryParse(text, out val))
                result = val == 0 ? "0" : val.ToString(format);
            else
                // 入力値が数値ではない場合はそのまま
                result = text;

            return result;
        }

        /// <summary>
        /// 諸口区分用フォーカス移動処理
        /// </summary>
        /// <param name="control"></param>
        private void MoveToNextControlForShokuchikbnCheck(ICustomControl control)
        {
            if (this.form.pressedEnterOrTab)
            {
                var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                this.form.SelectNextControl((Control)control, !isPressShift, true, true, true);
            }

            // マウス操作を考慮するためpressedEnterOrTabを初期化
            this.form.pressedEnterOrTab = false;
        }

        /// <summary>
        /// 取引先CD初期セット
        /// </summary>
        internal void TorihikisakiCdSet()
        {
            tmpTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
        }

        /// <summary>
        /// 業者CD初期セット
        /// </summary>
        internal void GyousyaCdSet()
        {
            tmpGyousyaCd = this.form.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積業者CD初期セット
        /// </summary>
        internal void NizumiGyoushaCdSet()
        {
            tmpNizumiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場CD初期セット
        /// </summary>
        internal void NizumiGenbaCdSet()
        {
            tmpNizumiGenbaCd = this.form.NIZUMI_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷積現場CD初期セット
        /// </summary>
        internal void UnpanGyoushaCdSet()
        {
            tmpUnpanGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 現場CD初期セット
        /// </summary>
        internal void GenbaCdSet()
        {
            tmpGenbaCd = this.form.GENBA_CD.Text;
        }

        /// <summary>
        /// 車輌CD初期セット
        /// </summary>
        internal void ShayouCdSet()
        {
            sharyouCd = this.form.SHARYOU_CD.Text;
        }

        /// <summary>
        /// 車種Cd初期セット
        /// </summary>
        internal void ShashuCdSet()
        {
            shaShuCd = this.form.SHASHU_CD.Text;
        }

        /// <summary>
        /// 運転者CD初期セット
        /// </summary>
        internal void UntenshaCdSet()
        {
            tmpUntenshaCd = this.form.UNTENSHA_CD.Text;
        }

        /// <summary>
        /// 形態区分CD初期セット
        /// </summary>
        internal void KeitaiKbnCdSet()
        {
            tmpKeitaiKbnCd = this.form.KEITAI_KBN_CD.Text;
        }

        public void getListTorihikisakiDefault()
        {
            var keyEntity = new M_TORIHIKISAKI();
            r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.torihikisakiList = torihikisakiDao.GetAllValidData(keyEntity).ToList();
        }

        public void getListManifestShuruiDefault()
        {
            var keyEntity = new M_MANIFEST_SHURUI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_SHURUIDao>();
            this.manifestShuruiList = dao.GetAllValidData(keyEntity).ToList();
        }

        public void getListManifestTeihaiDefault()
        {
            var keyEntity = new M_MANIFEST_TEHAI();
            var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_TEHAIDao>();
            this.manifestTehaiList = dao.GetAllValidData(keyEntity).ToList();
        }

        /// <summary> 設定値保存 </summary>
        internal bool SaveParams()
        {
            bool ret = true;
            try
            {
                var nyuuryokuParam = new NyuuryokuParamDto();

                if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
                {
                    nyuuryokuParam.kyotenCd = Convert.ToInt16(this.form.KYOTEN_CD.Text);
                }

                nyuuryokuParam.kyotenName = this.form.KYOTEN_NAME_RYAKU.Text;
                if (this.form.DENPYOU_DATE.Value != null)
                {
                    nyuuryokuParam.denpyouDate = this.form.DENPYOU_DATE.Value.ToString();
                }
                else
                {
                    nyuuryokuParam.denpyouDate = "";
                }

                if (this.form.URIAGE_DATE.Value != null)
                {
                    nyuuryokuParam.uriageDate = this.form.URIAGE_DATE.Value.ToString();
                }
                else
                {
                    nyuuryokuParam.uriageDate = "";
                }
                if (this.form.SHIHARAI_DATE.Value != null)
                {
                    nyuuryokuParam.shiharaiDate = this.form.SHIHARAI_DATE.Value.ToString();
                }
                else
                {
                    nyuuryokuParam.shiharaiDate = "";
                }

                nyuuryokuParam.torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                nyuuryokuParam.torihikisakiName = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                nyuuryokuParam.gyoushaCd = this.form.GYOUSHA_CD.Text;
                nyuuryokuParam.gyoushaName = this.form.GYOUSHA_NAME_RYAKU.Text;
                nyuuryokuParam.genbaCd = this.form.GENBA_CD.Text;
                nyuuryokuParam.genbaName = this.form.GENBA_NAME_RYAKU.Text;
                nyuuryokuParam.eigyouTantoushaCd = this.form.EIGYOU_TANTOUSHA_CD.Text;
                nyuuryokuParam.eigyouTantoushaName = this.form.EIGYOU_TANTOUSHA_NAME.Text;
                nyuuryokuParam.nizumiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
                nyuuryokuParam.nizumiGyoushaName = this.form.NIZUMI_GYOUSHA_NAME.Text;
                nyuuryokuParam.nizumiGenbaCd = this.form.NIZUMI_GENBA_CD.Text;
                nyuuryokuParam.nizumiGenbaName = this.form.NIZUMI_GENBA_NAME.Text;
                nyuuryokuParam.shashuCd = this.form.SHASHU_CD.Text;
                nyuuryokuParam.shashuName = this.form.SHASHU_NAME.Text;
                nyuuryokuParam.sharyouCd = this.form.SHARYOU_CD.Text;
                nyuuryokuParam.sharyouName = this.form.SHARYOU_NAME_RYAKU.Text;
                nyuuryokuParam.upnGyoushaCd = this.form.UNPAN_GYOUSHA_CD.Text;
                nyuuryokuParam.upnGyoushaName = this.form.UNPAN_GYOUSHA_NAME.Text;
                nyuuryokuParam.untenshaCd = this.form.UNTENSHA_CD.Text;
                nyuuryokuParam.untenshaName = this.form.UNTENSHA_NAME.Text;

                if (!string.IsNullOrEmpty(this.form.KEITAI_KBN_CD.Text))
                {
                    nyuuryokuParam.keitaiKbnCd = Convert.ToInt16(this.form.KEITAI_KBN_CD.Text);
                }

                nyuuryokuParam.keitaiKbnName = this.form.KEITAI_KBN_NAME_RYAKU.Text;

                if (!string.IsNullOrEmpty(this.form.DAIKAN_KBN.Text))
                {
                    nyuuryokuParam.daikanKbn = Convert.ToInt16(this.form.DAIKAN_KBN.Text);
                }

                nyuuryokuParam.daikanKbnName = this.form.DAIKAN_KBN_NAME.Text;
                nyuuryokuParam.manifestShuruiCd = this.form.MANIFEST_SHURUI_CD.Text;
                nyuuryokuParam.manifestShuruiName = this.form.MANIFEST_SHURUI_NAME_RYAKU.Text;
                nyuuryokuParam.manifestTehaiCd = this.form.MANIFEST_TEHAI_CD.Text;
                nyuuryokuParam.manifestTehaiName = this.form.MANIFEST_TEHAI_NAME_RYAKU.Text;
                nyuuryokuParam.denpyouBikou = this.form.DENPYOU_BIKOU.Text;
                nyuuryokuParam.taipyuuBikou = this.form.TAIRYUU_BIKOU.Text;

                this.form.NyuuryokuParam = nyuuryokuParam;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveParams", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
    }
}
