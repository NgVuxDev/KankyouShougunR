using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DataGridViewCheckBoxColumnHeaderEx;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dao;
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dto;

namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class TuuchiJouhouMeisaiLogic
    {
        #region フィールド

        /// <summary>
        /// ボタンの設定用ファイルパス
        /// </summary>
        private readonly string ButtonInfoXmlPath =
            "Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Setting.ButtonSettingMeisai.xml";

        /// <summary>
        /// Form
        /// </summary>
        private TuuchiJouhouMeisai form;

        /// <summary>
        /// 明細取得DAO
        /// </summary>
        private GetMeisaiInfoDaoCls GetMeisaiInfoDao;

        /// <summary>
        /// 通知情報DAO
        /// </summary>
        private TuuchiInfoDaoCls TuuchiInfoDao;

        /// <summary>
        /// 目次情報DAO
        /// </summary>
        private MokujiInfoDaoCls MokujiInfoDao;

        /// <summary>
        /// キュー情報DAO
        /// </summary>
        private QueInfoDaoCls QueInfoDao;

        /// <summary>
        /// 画面上に表示するメッセージボックスをメッセージIDから検索し表示する処理
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 通知コードテーブル
        /// </summary>
        private Hashtable hsTuuchi { get; set; }

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeaderMeisai headerForm;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TuuchiJouhouMeisaiLogic(TuuchiJouhouMeisai targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            try
            {
                this.form = targetForm;
                this.GetMeisaiInfoDao = DaoInitUtility.GetComponent<GetMeisaiInfoDaoCls>();
                this.TuuchiInfoDao = DaoInitUtility.GetComponent<TuuchiInfoDaoCls>();
                this.MokujiInfoDao = DaoInitUtility.GetComponent<MokujiInfoDaoCls>();
                this.QueInfoDao = DaoInitUtility.GetComponent<QueInfoDaoCls>();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                //ロジック初期化
                this.msgLogic = new MessageBoxShowLogic();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // フォームインスタンスを取得
                this.ParentBaseForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeaderMeisai)ParentBaseForm.headerForm;

                this.CreateHashtable();

                //ボタンの初期化
                this.ButtonInit();

                //Headerの初期化
                this.SetHeaderText();

                //コントロール初期化
                this.ControlInit();

                //イベント初期化
                this.EventInit();

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //編集不可を設定する
                BusinessBaseForm parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.txb_process.Enabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public bool Kensaku(bool saiKensakuFlg = false)
        {
            try
            {
                LogUtility.DebugMethodStart(saiKensakuFlg);

                DataTable dtInfo = new DataTable();

                //データが存在しない場合
                if (!this.GetMeisaiInfo(ref dtInfo, saiKensakuFlg))
                    this.SetMeisaiInfo(dtInfo);

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Kensaku", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Kensaku", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Kensaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(saiKensakuFlg);
            }
        }

        /// <summary>
        /// Headerの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (this.form.ApprovalFlg)
                {
                    string headerMsg = hsTuuchi[Properties.Settings.Default.tuuchiCd].ToString() + (char)13
                        + Const.ConstCls.TITLE_MSG;
                    this.headerForm.cwtl_msg.Text = headerMsg;
                }

                this.headerForm.lb_title.Text = Const.ConstCls.TUUCHI_MEISAI_TITLE;

                BusinessBaseForm parentForm = (BusinessBaseForm)this.form.Parent;
                if (parentForm != null) parentForm.Text = parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.headerForm.lb_title.Text);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            buttonSetting = new ButtonSetting();

            thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                //登録ボタン(F9)イベント生成
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);

                //取消ボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                this.form.cdgv_TuuchiJouhouMeisai.CellMouseClick += new DataGridViewCellMouseEventHandler(this.form.cdgv_TuuchiJouhouMeisai_CellMouseClick);
                this.form.cdgv_TuuchiJouhouMeisai.CurrentCellDirtyStateChanged += new EventHandler(this.form.cdgv_TuuchiJouhouMeisai_CurrentCellDirtyStateChanged);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 電子マニフェスト入力画面を表示する

        /// <summary>
        /// 電子マニフェスト入力画面の参照モードで開く
        /// </summary>
        public void ElectronicManifestNyuryokuShow(string kanriId, string seq, string approvalSeq, string latestSeq, string tuuchiCd)
        {
            LogUtility.DebugMethodStart(kanriId, seq, approvalSeq, latestSeq, tuuchiCd);

            try
            {
                //処理モード=4「参照モード」を設定する
                //2013.12.18 naitou upd start
                //var callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, seq);
                //var callHeader = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIHeader();
                //var businessForm = new BusinessBaseForm(callForm, callHeader);
                //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                //if (!isExistForm)
                //{
                //    businessForm.Show();
                //}

                FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, seq, "TUUCHI_RIREKI", approvalSeq, latestSeq, tuuchiCd);
                //2013.12.18 naitou upd end
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("ElectronicManifestNyuryokuShow", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ElectronicManifestNyuryokuShow", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElectronicManifestNyuryokuShow", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(kanriId, seq, approvalSeq, latestSeq, tuuchiCd);
            }
        }

        #endregion

        #region 明細情報取得と設定

        /// <summary>
        /// データを設定する
        /// </summary>
        /// <param name="dtInfo"></param>
        public void SetMeisaiInfo(DataTable dtInfo)
        {
            int loopIndex = dtInfo.Rows.Count;
            int columnIndex = 0;

            try
            {
                this.form.cdgv_TuuchiJouhouMeisai.IsBrowsePurpose = false;
                //明細データ初期化
                this.form.cdgv_TuuchiJouhouMeisai.Rows.Clear();

                for (int i = 0; i < loopIndex; i++)
                {
                    columnIndex = 0;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows.Add();
                    columnIndex += 1;
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = false;

                    if (!this.form.ApprovalFlg)
                    {
                        //ヒントテキストクリア
                        this.form.cdgv_TuuchiJouhouMeisai.Columns[columnIndex].ToolTipText = string.Empty;
                    }
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = false;

                    if (!this.form.ApprovalFlg)
                    {
                        //ヒントテキストクリア
                        this.form.cdgv_TuuchiJouhouMeisai.Columns[columnIndex].ToolTipText = string.Empty;
                    }

                    columnIndex += 1;
                    if (string.Equals(Const.ConstCls.KAKUNIN_DEFAULT, Convert.ToString(dtInfo.Rows[i]["READ_FLAG"])))
                        this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = true;
                    else
                        this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = false;
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["MANIFEST_ID"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["TUUCHI_DATE_TIME"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["HIKIWATASHI_DATE"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["HST_JOU_NAME"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["UPN_ROUTE_NO"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["END_DATE"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["RENRAKU_ID1"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["RENRAKU_ID2"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["RENRAKU_ID3"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["TUUCHI_ID"];
                    //非表示
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["BIKOU"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["READ_FLAG"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["TUUCHI_UPDATE_TS"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["KANRI_ID"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["LATEST_SEQ"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["APPROVAL_SEQ"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["MOKUJI_UPDATE_TS"];
                    columnIndex += 1;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells[columnIndex].Value = dtInfo.Rows[i]["ACTION_FLAG"];
                    //非表示列
                    if (string.Equals(Const.ConstCls.KAKUNIN_DEFAULT, Convert.ToString(dtInfo.Rows[i]["READ_FLAG"])))
                        this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells["Hidden_KAKUNIN"].Value = true;
                    else
                        this.form.cdgv_TuuchiJouhouMeisai.Rows[i].Cells["Hidden_KAKUNIN"].Value = false;
                }

                //コントロールの表面全体を無効化して、コントロールを再描画します
                this.form.cdgv_TuuchiJouhouMeisai.IsBrowsePurpose = true;
                this.form.cdgv_TuuchiJouhouMeisai.Invalidate();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
        }

        /// <summary>
        /// 明細情報を取得
        /// </summary>
        /// <param name="dtInfo"></param>
        /// <returns></returns>
        public bool GetMeisaiInfo(ref DataTable dtInfo, bool saiKensakuFlg)
        {
            MeisaiInfoDTOCls search = new MeisaiInfoDTOCls();

            try
            {
                search.tuuchiBiFrom = Properties.Settings.Default.tuuchiBiFrom;
                search.tuuchiBiTo = Properties.Settings.Default.tuuchiBiTo;
                search.readFlag = Properties.Settings.Default.readFlag;
                search.tuuchiCd = Properties.Settings.Default.tuuchiCd;
                //修正/取消モードの場合、空白以外を設定する
                if (this.form.ApprovalFlg) search.ApprovalFlg = "1";

                //データを取得する
                dtInfo = this.GetMeisaiInfoDao.GetDataForEntity(search);

                if (dtInfo == null || dtInfo.Rows.Count == 0)
                {
                    var parentForm = (BusinessBaseForm)this.form.Parent;
                    //明細件数が0件の場合
                    parentForm.bt_func9.Enabled = false;
                    this.form.cdgv_TuuchiJouhouMeisai.Rows.Clear();
                    this.SetDataGridErrorState();

                    //再検索以外の場合、メッセージを表示する
                    if (!saiKensakuFlg) this.msgLogic.MessageBoxShow("E022", "該当の通知情報");
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            return false;
        }

        /// <summary>
        /// 異常の場合、データグリッドの状態を設定する
        /// </summary>
        private void SetDataGridErrorState()
        {
            DataGridviewCheckboxHeaderCell headerCell =
                        new DataGridviewCheckboxHeaderCell(Const.ConstCls.SHOUNIN_COLUMN_INDEX);

            headerCell.CheckBoxEnable = false;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].ReadOnly = true;

            this.form.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell = headerCell;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderText = Const.ConstCls.SHOUNIN_HEADERTEXT;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.TopCenter;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].ToolTipText = Const.ConstCls.SHOUNIN_TOOLTIPTEXT;

            //否認列のプロパティを設定する
            headerCell = new DataGridviewCheckboxHeaderCell(Const.ConstCls.HININ_COLUMN_INDEX);

            headerCell.CheckBoxEnable = false;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["HININ"].ReadOnly = true;

            this.form.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell = headerCell;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderText = Const.ConstCls.HININ_HEADERTEXT;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.TopCenter;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["HININ"].ToolTipText = Const.ConstCls.HININ_TOOLTIPTEXT;

            //確認列のプロパティを設定する
            headerCell = new DataGridviewCheckboxHeaderCell(Const.ConstCls.KAKUNIN_COLUMN_INDEX);
            headerCell.CheckBoxEnable = false;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].ReadOnly = true;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderCell = headerCell;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderText = Const.ConstCls.KAKUNIN_HEADERTEXT;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.TopCenter;
            this.form.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].ToolTipText = Const.ConstCls.KAKUNIN_TOOLTIPTEXT;

            this.form.cdgv_TuuchiJouhouMeisai.Invalidate();
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //チェック
                if (this.RegistCheck())
                {
                    return;
                }

                //データ更新
                using (Transaction tran = new Transaction())
                {
                    List<DT_R24> tuuchiInfoList = new List<DT_R24>();
                    List<DT_MF_TOC> mokujiInfoList = new List<DT_MF_TOC>();
                    List<QUE_INFO> queInfoList = new List<QUE_INFO>();
                    int cont = 0;

                    this.MakeData(ref tuuchiInfoList, ref mokujiInfoList, ref queInfoList);

                    if (tuuchiInfoList != null && tuuchiInfoList.Count() > 0)
                    {
                        foreach (DT_R24 tuuchiInfo in tuuchiInfoList)
                        {
                            cont = TuuchiInfoDao.Update(tuuchiInfo);
                        }
                    }

                    if (mokujiInfoList != null && mokujiInfoList.Count() > 0)
                    {
                        foreach (DT_MF_TOC mokujiInfo in mokujiInfoList)
                        {
                            cont = MokujiInfoDao.Update(mokujiInfo);
                        }
                    }

                    if (queInfoList != null && queInfoList.Count() > 0)
                    {
                        foreach (QUE_INFO queInfo in queInfoList)
                        {
                            cont = QueInfoDao.Insert(queInfo);
                        }
                    }

                    tran.Commit();
                }

                //更新後は完了メッセージを表示する
                msgLogic.MessageBoxShow("I001", "登録");

                //再検索処理
                this.Kensaku(true);
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録チェック
        /// </summary>
        /// <returns></returns>
        private bool RegistCheck()
        {
            LogUtility.DebugMethodStart();

            bool henkouFlg = true;

            try
            {
                foreach (DataGridViewRow dgvr in this.form.cdgv_TuuchiJouhouMeisai.Rows)
                {
                    if ((bool)dgvr.Cells[2].Value
                        || (bool)dgvr.Cells[3].Value
                            || (((bool)dgvr.Cells[4].Value != (bool)dgvr.Cells["Hidden_KAKUNIN"].Value))
                        )
                    {
                        henkouFlg = false;
                        break;
                    }
                }

                if (henkouFlg)
                {
                    //メッセージ
                    this.msgLogic.MessageBoxShow("E029", "選択状態を変更する承認/否認/確認", "明細一覧");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();

            return henkouFlg;
        }

        /// <summary>
        /// データを作成
        /// </summary>
        /// <param name="tuuchiInfoList">通知情報結果</param>
        /// <param name="mokujiInfoList">マニフェスト目次情報</param>
        /// <param name="queInfoList">キュー情報</param>
        private void MakeData(ref List<DT_R24> tuuchiInfoList,
            ref List<DT_MF_TOC> mokujiInfoList,
            ref List<QUE_INFO> queInfoList)
        {
            LogUtility.DebugMethodStart(tuuchiInfoList, mokujiInfoList, queInfoList);

            DT_R24 tuuchiInfo;
            DT_MF_TOC mokujiInfo;
            QUE_INFO queInfo;

            string[] tuuchiCd1 = new string[] { "203", "206" };
            string[] tuuchiCd2 = new string[] { "303", "306" };
            string[] tuuchiCd3 = new string[] { "110", "113", "118", "121" };

            try
            {
                foreach (DataGridViewRow dgvr in this.form.cdgv_TuuchiJouhouMeisai.Rows)
                {
                    if ((bool)dgvr.Cells[2].EditedFormattedValue
                        || (bool)dgvr.Cells[3].EditedFormattedValue
                        || (((bool)dgvr.Cells[4].EditedFormattedValue != (bool)dgvr.Cells["Hidden_KAKUNIN"].Value)))
                    {
                        tuuchiInfo = new DT_R24();
                        //通知番号
                        tuuchiInfo.TUUCHI_ID = Convert.ToInt64(dgvr.Cells["TUUCHI_ID"].Value);

                        //承認/否認フラグ
                        if ((bool)dgvr.Cells[2].EditedFormattedValue) tuuchiInfo.ACTION_FLAG = "1";
                        else if ((bool)dgvr.Cells[3].EditedFormattedValue) tuuchiInfo.ACTION_FLAG = "2";
                        else tuuchiInfo.ACTION_FLAG = Convert.ToString(dgvr.Cells["ACTION_FLAG"].Value);

                        //既読フラグ
                        if ((bool)dgvr.Cells[4].EditedFormattedValue) tuuchiInfo.READ_FLAG = 1;
                        else tuuchiInfo.READ_FLAG = 0;

                        //タイムスタンプ
                        tuuchiInfo.UPDATE_TS = (DateTime)dgvr.Cells["TUUCHI_UPDATE_TS"].Value;

                        // 既読フラグのみを有効にする場合は風魔へ送信しない
                        bool readOnlyFlg = false;

                        if (!(bool)dgvr.Cells[2].EditedFormattedValue
                            && !(bool)dgvr.Cells[3].EditedFormattedValue)

                        {
                            readOnlyFlg = true;
                        }

                        tuuchiInfoList.Add(tuuchiInfo);

                        if (!readOnlyFlg)
                        {
                            //目次情報リストを設定する
                            mokujiInfo = new DT_MF_TOC();

                            //管理番号
                            mokujiInfo.KANRI_ID = dgvr.Cells["KANRI_ID"].Value.ToString();

                            //状態詳細フラグ
                            mokujiInfo.STATUS_DETAIL = 3;

                            //タイムスタンプ
                            mokujiInfo.UPDATE_TS = (DateTime)dgvr.Cells["MOKUJI_UPDATE_TS"].Value;

                            bool isKanriIdExistsFlg = false;

                            foreach (DT_MF_TOC dmt in mokujiInfoList)
                            {
                                if (dmt.KANRI_ID.Equals(mokujiInfo.KANRI_ID))
                                {
                                    isKanriIdExistsFlg = true;
                                    break;
                                }
                            }

                            if (!isKanriIdExistsFlg) mokujiInfoList.Add(mokujiInfo);
                        }

                        if (!readOnlyFlg)
                        {
                            //キュー情報を設定する
                            queInfo = new QUE_INFO();
                            queInfo.KANRI_ID = dgvr.Cells["KANRI_ID"].Value.ToString();
                            queInfo.SEQ = Convert.ToInt16(dgvr.Cells["LATEST_SEQ"].Value);

                            //処理対象通知情報の【通知コード】が203,206の場合
                            if (tuuchiCd1.Contains(Properties.Settings.Default.tuuchiCd))
                            {
                                queInfo.FUNCTION_ID = "0701";
                                if (dgvr.Cells["UPN_ROUTE_NO"] == null ||
                                    string.IsNullOrEmpty(dgvr.Cells["UPN_ROUTE_NO"].Value.ToString()))
                                {
                                }
                                else
                                {
                                    queInfo.UPN_ROUTE_NO = Convert.ToInt16(dgvr.Cells["UPN_ROUTE_NO"].Value);
                                }
                            }

                            //処理対象通知情報の【通知コード】が303,306の場合
                            if (tuuchiCd2.Contains(Properties.Settings.Default.tuuchiCd))
                            {
                                queInfo.FUNCTION_ID = "0702";
                            }

                            //処理対象通知情報の【通知コード】が110,113,118,121の場合
                            if (tuuchiCd3.Contains(Properties.Settings.Default.tuuchiCd))
                            {
                                queInfo.FUNCTION_ID = "1200";
                            }

                            queInfo.TUUCHI_ID = Convert.ToInt64(dgvr.Cells["TUUCHI_ID"].Value);
                            queInfo.STATUS_FLAG = 0;

                            //枝番の取得
                            DataTable dt = new DataTable();

                            GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                            search.kanriId = queInfo.KANRI_ID;

                            dt = this.QueInfoDao.GetMaxSeq(search);

                            SqlInt16 seq = 0;

                            //seqを設定する
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                seq = Convert.ToInt16(dt.Rows[0][0]);
                            }

                            seq += 1;

                            queInfo.QUE_SEQ = seq;

                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //queInfo.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                            queInfo.CREATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                            foreach (QUE_INFO qi in queInfoList)
                            {
                                if (qi.KANRI_ID.Equals(queInfo.KANRI_ID))
                                {
                                    queInfo.QUE_SEQ += 1;
                                }
                            }

                            queInfoList.Add(queInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd(tuuchiInfoList, mokujiInfoList, queInfoList);
        }

        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #region 表示メッセージ

        /// <summary>
        /// 表示メッセージを設定する
        /// </summary>
        private void CreateHashtable()
        {
            LogUtility.DebugMethodStart();

            hsTuuchi = new Hashtable();

            try
            {
                hsTuuchi.Add("110", "マニフェストの該当区間の運搬終了報告修正が行われています。");
                hsTuuchi.Add("113", "マニフェストの該当区間の運搬終了報告取消が行われています。");
                hsTuuchi.Add("118", "マニフェストの処分報告修正が行われています。");
                hsTuuchi.Add("121", "マニフェストの処分報告取消が行われています。");
                hsTuuchi.Add("203", "マニフェストの修正が行われています。");
                hsTuuchi.Add("303", "マニフェストの修正が行われています。");
                hsTuuchi.Add("206", "マニフェストの取消が行われています。");
                hsTuuchi.Add("306", "マニフェストの取消が行われています。");
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion
    }
}