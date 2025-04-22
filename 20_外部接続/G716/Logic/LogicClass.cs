using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuRirekiIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiKeiyakuRirekiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// フォームオブジェクト
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 電子契約基本Dao
        /// </summary>
        private DenshiKeiyakuKihonDAO denshiKeiyakuKihonDao;

        /// <summary>
        /// 電子契約送付情報Dao
        /// </summary>
        private DenshiKeiyakuSouhuinfoDAO denshiKeiyakuSouhuinfoDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 電子契約APIのDao
        /// </summary>
        private IS_DENSHI_CONNECTDao denshiConnectDao;

        /// <summary>
        /// クライアントID
        /// </summary>
        internal string clientId;

        /// <summary>
        /// ドキュメントID
        /// </summary>
        internal string documentId;

        /// <summary>
        /// 電子契約APIロジッククラス
        /// </summary>
        private DenshiLogic denshiLogic;

        /// <summary>
        /// システム設定Entity
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 出力区分
        /// </summary>
        private SqlInt16 outputKbn;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // メッセージ用
            this.msgLogic = new MessageBoxShowLogic();

            this.denshiKeiyakuKihonDao = DaoInitUtility.GetComponent<DenshiKeiyakuKihonDAO>();
            this.denshiKeiyakuSouhuinfoDao = DaoInitUtility.GetComponent<DenshiKeiyakuSouhuinfoDAO>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.denshiConnectDao = DaoInitUtility.GetComponent<IS_DENSHI_CONNECTDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.denshiLogic = new DenshiLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 修正(F3)イベント作成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);
            // 削除(F4)イベント作成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            // CSV(F6)イベント作成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);
            // 条件クリア(F7)イベント作成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);
            // 検索(F8)イベント作成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            // 並び替え(F10)イベント作成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);
            // フィルタ(F11)イベント作成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);
            // 閉じる(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);
            //パターン一覧画面へ遷移
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
            // 契約書ﾀﾞｳﾝﾛｰﾄﾞ
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
            // 契約状況取得
            parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
            // 合意締結証明書ﾀﾞｳﾝﾛｰﾄﾞ
            parentForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);

            this.form.GYOUSHA_CD.Enter += new EventHandler(this.form.GYOUSHA_CD_Enter);
            this.form.GENBA_CD.Enter += new EventHandler(this.form.GENBA_CD_Enter);
            this.form.UNPANGYOUSHA_CD.Enter += new EventHandler(this.form.UNPANGYOUSHA_CD_Enter);
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Enter += new EventHandler(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD_Enter);
            this.form.SHOBUN_GENBA_CD.Enter += new EventHandler(this.form.SHOBUN_GENBA_CD_Enter);
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Enter += new EventHandler(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD_Enter);
            this.form.SAISHUU_SHOBUNJOU_CD.Enter += new EventHandler(this.form.SAISHUU_SHOBUNJOU_CD_Enter);
            this.form.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.GYOUSHA_CD_Validating);
            this.form.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.GENBA_CD_Validating);
            this.form.UNPANGYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.UNPANGYOUSHA_CD_Validating);
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD_Validating);
            this.form.SHOBUN_GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_GENBA_CD_Validating);
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD_Validating);
            this.form.SAISHUU_SHOBUNJOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SAISHUU_SHOBUNJOU_CD_Validating);
            this.form.KEIYAKUSHO_SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.KEIYAKUSHO_SHURUI_CD_Validating);
            this.form.KYOKASHOU_SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.KYOKASHOU_SHURUI_CD_Validating);

            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                //フォーム初期化
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)parentForm.headerForm;

                //ボタンのテキストを初期化
                this.ButtonInit();

                //ボタン制御
                this.ButtonEnabledControl();

                //イベントの初期化処理
                this.EventInit();

                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)
                //this.form.customDataGridView1.Size = new Size(997, 250);
                //this.form.customDataGridView1.Location = new Point(3, 200);
                //this.form.customDataGridView1.TabStop = false;

                // システム設定の読み込み
                this.GetSysInfo();

                // ヘッダー部の初期化
                this.InitHeaderArea();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        #endregion

        #region ボタン制御
        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        internal void ButtonEnabledControl()
        {
            // 初期化
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func10.Enabled = true;
            parentForm.bt_func11.Enabled = true;
            parentForm.bt_func12.Enabled = true;
        }
        #endregion

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        /// <summary>
        /// ヘッダー部の初期化
        /// </summary>
        private void InitHeaderArea()
        {
            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }
        }

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();
            if (!this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
            {
                if (this.form.PatternNo != 0)
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = ConstCls.CELL_CHECKBOX;
                    newColumn.HeaderText = "選択";
                    newColumn.Width = 70;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "選択   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 70;
                    newColumn.Resizable = DataGridViewTriState.False;

                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                    this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        /// <summary>
        /// 電子契約入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        internal void OpenWindow(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart(windowType);

            // 表示されている行のPKを取得する
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row == null)
            {
                this.msgLogic.MessageBoxShowError("データが選択されていません。");
                return;
            }
            else
            {
                // 削除済の場合、エラーとする。
                string deleteFlg = row.Cells[ConstCls.HIDDEN_DELETE_FLG].Value.ToString();
                if (deleteFlg.Equals("True"))
                {
                    this.msgLogic.MessageBoxShowError("削除済のデータのため、修正処理が行えません。");
                    return;
                }

                string cd1 = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                string cd2 = row.Cells[ConstCls.KEY_ID2].Value.ToString();

                // 権限チェック
                // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                    !r_framework.Authority.Manager.CheckAuthority("G715", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (!r_framework.Authority.Manager.CheckAuthority("G715", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        this.msgLogic.MessageBoxShow("E158", "修正");
                        return;
                    }

                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                r_framework.FormManager.FormManager.OpenFormWithAuth("G715", windowType, windowType, cd1, cd2);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            if (this.CheckDate())
            {
                return 0;
            }

            this.form.Table = null;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();
                this.form.Table = this.denshiKeiyakuKihonDao.getdateforstringsql(sql);
            }

            // 検索結果を画面に表示
            this.form.ShowData();

            // 読込データ件数を表示する。
            if (this.form.Table != null)
            {
                this.headerForm.readDataNumber.Text = this.form.Table.Rows.Count.ToString();
            }

            // 検索結果が0件の場合、メッセージを表示する。
            if (this.headerForm.readDataNumber.Text == "0")
            {
                this.msgLogic.MessageBoxShow("C001");
            }

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.DATE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrWhiteSpace(this.form.DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.form.DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.form.DATE_SELECT_1.Checked)
                {
                    string[] errorMsg = { "作成日From", "作成日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_2.Checked)
                {
                    string[] errorMsg = { "送付日From", "送付日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_3.Checked)
                {
                    string[] errorMsg = { "契約日From", "契約日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_4.Checked)
                {
                    string[] errorMsg = { "有効期間開始From", "有効期間開始To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_5.Checked)
                {
                    string[] errorMsg = { "有効期間終了From", "有効期間終了To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_6.Checked)
                {
                    string[] errorMsg = { "自動更新終了日From", "自動更新終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }

                this.form.DATE_FROM.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 検索文字列を作成
        /// </summary>
        private string MakeSearchCondition()
        {
            var selectQuery = this.CreateSelectQuery();
            var fromQuery = this.CreateFromQuery();
            var whereQuery = this.CreateWhereQuery();
            var orderByQuery = this.CreateOrderByQuery();

            return selectQuery + fromQuery + whereQuery + orderByQuery;
        }

        #region SELECT
        /// <summary>
        /// Select句作成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectQuery()
        {
            var sb = new StringBuilder();

            // 出力区分を取得する。
            this.outputKbn = this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                //表示用項目
                sb.Append("SELECT DISTINCT ");

                // 非活性項目
                sb.AppendFormat(" kihon.SYSTEM_ID AS {0}, ", ConstCls.KEY_ID1);
                sb.AppendFormat(" kihon.DENSHI_KEIYAKU_SYSTEM_ID AS {0}, ", ConstCls.KEY_ID2);
                sb.AppendFormat(" kihon.DOCUMENT_ID AS {0}, ", ConstCls.HIDDEN_DOCUMENT_ID);
                sb.AppendFormat(" kihon.DELETE_FLG AS {0}, ", ConstCls.HIDDEN_DELETE_FLG);

                // 出力区分が「2:明細」の場合、ファイルIDとファイルパスを取得する。
                if (this.outputKbn == 2)
                {
                    sb.AppendFormat(" info.FILE_ID AS {0}, ", ConstCls.HIDDEN_FILE_ID);
                    sb.AppendFormat(" info.FILE_PATH AS {0}, ", ConstCls.HIDDEN_FILE_PATH);
                }

                //パターン一覧からの表示列
                sb.Append(this.form.SelectQuery);


            }
            return sb.ToString();
        }
        #endregion
        #region FROM
        /// <summary>
        /// From句作成
        /// </summary>
        /// <returns></returns>
        private string CreateFromQuery()
        {
            var sb = new StringBuilder();

            // 電子契約基本
            sb.Append(" FROM T_DENSHI_KEIYAKU_KIHON kihon ");

            // 電子契約送付情報
            // 出力区分が「2:明細」の場合に結合する。
            if (this.outputKbn == 2)
            {
                sb.Append(" LEFT JOIN T_DENSHI_KEIYAKU_SOUHUINFO info ");
                sb.Append(" ON info.SYSTEM_ID = kihon.SYSTEM_ID ");
                sb.Append(" AND info.DENSHI_KEIYAKU_SYSTEM_ID = kihon.DENSHI_KEIYAKU_SYSTEM_ID ");
            }

            // 委託契約電子送付先マスタ
            sb.Append(" LEFT JOIN (SELECT DISTINCT SYSTEM_ID, ACCESS_CD FROM M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI) itakuDenshiSouhusaki ");
            sb.Append(" ON itakuDenshiSouhusaki.SYSTEM_ID = kihon.SYSTEM_ID ");

            // 委託契約基本
            sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_KIHON itakukihon ");
            sb.Append(" ON itakukihon.SYSTEM_ID = kihon.SYSTEM_ID ");

            // 業者マスタ
            sb.Append(" LEFT JOIN M_GYOUSHA MGYO ");
            sb.Append(" ON MGYO.GYOUSHA_CD = kihon.HAISHUTSU_JIGYOUSHA_CD ");

            // 現場マスタ
            sb.Append(" LEFT JOIN M_GENBA MGEN ");
            sb.Append(" ON MGEN.GYOUSHA_CD = kihon.HAISHUTSU_JIGYOUSHA_CD ");
            sb.Append(" AND MGEN.GENBA_CD = kihon.HAISHUTSU_JIGYOUJOU_CD ");

            // 都道府県マスタ
            sb.Append(" LEFT JOIN M_TODOUFUKEN MGTO ");
            sb.Append(" ON MGEN.GENBA_TODOUFUKEN_CD = MGTO.TODOUFUKEN_CD ");

            // 電子契約契約情報
            sb.Append(" LEFT JOIN T_DENSHI_KEIYAKU_KEIYAKUINFO keiyakuinfo");
            sb.Append(" ON keiyakuinfo.SYSTEM_ID = kihon.SYSTEM_ID ");

            // 委託契約別表2（運搬）
            sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_BETSU2 itakuBETSU2");
            sb.Append(" ON itakuBETSU2.SYSTEM_ID = itakukihon.SYSTEM_ID ");

            // パターンから作成したJOIN句
            sb.Append(this.form.JoinQuery);

            return sb.ToString();
        }
        #endregion
        #region WHERE
        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        private string CreateWhereQuery()
        {
            var strTemp = string.Empty;
            var sb = new StringBuilder();

            // 契約番号
            strTemp = this.form.ITAKU_KEIYAKU_NO.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND kihon.KEIYAKU_NO LIKE '%{0}%'", strTemp);
            }
            // 契約書種類
            strTemp = this.form.KEIYAKUSHO_SHURUI_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                switch (strTemp)
                {
                    case "11":
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_TYPE  = 1");
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_SHURUI  = 1");
                        break;
                    case "12":
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_TYPE  = 1");
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_SHURUI  = 2");
                        break;
                    case "13":
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_TYPE  = 1");
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_SHURUI  = 3");
                        break;
                    case "21":
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_TYPE  = 2");
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_SHURUI  = 1");
                        break;
                    case "22":
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_TYPE  = 2");
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_SHURUI  = 2");
                        break;
                    case "23":
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_TYPE  = 2");
                        sb.AppendFormat(" AND itakukihon.ITAKU_KEIYAKU_SHURUI  = 3");
                        break;
                    default:
                        break;
                }
            }
            // 許可証種類
            strTemp = this.form.KYOKASHOU_SHURUI_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                switch (strTemp)
                {
                    case "31":
                        sb.AppendFormat(" AND EXISTS(SELECT SYSTEM_ID FROM M_ITAKU_UPN_KYOKASHO WHERE M_ITAKU_UPN_KYOKASHO.SYSTEM_ID = kihon.SYSTEM_ID) ");
                        break;
                    case "32":
                        sb.AppendFormat(" AND EXISTS(SELECT SYSTEM_ID FROM M_ITAKU_SBN_KYOKASHO WHERE M_ITAKU_SBN_KYOKASHO.SYSTEM_ID = kihon.SYSTEM_ID AND (M_ITAKU_SBN_KYOKASHO.KYOKA_KBN = 3 OR M_ITAKU_SBN_KYOKASHO.KYOKA_KBN = 4)) ");
                        break;
                    case "33":
                        sb.AppendFormat(" AND EXISTS(SELECT SYSTEM_ID FROM M_ITAKU_SBN_KYOKASHO WHERE M_ITAKU_SBN_KYOKASHO.SYSTEM_ID = kihon.SYSTEM_ID AND (M_ITAKU_SBN_KYOKASHO.KYOKA_KBN = 5 OR M_ITAKU_SBN_KYOKASHO.KYOKA_KBN = 6)) ");
                        break;
                    default:
                        break;
                }
            }
            // 契約状況
            strTemp = this.form.KEIYAKU_JYOUKYOU_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND kihon.KEIYAKU_JYOUKYOU = '{0}'", strTemp);
            }

            // 日付
            strTemp = this.form.DATE_SELECT.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                string strFrom = Convert.ToString(this.form.DATE_FROM.Value);
                string strTo = Convert.ToString(this.form.DATE_TO.Value);
                if (strTemp.Equals("1"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND kihon.KEIYAKUSHO_CREATE_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND kihon.KEIYAKUSHO_CREATE_DATE <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("2"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND kihon.KEIYAKUSHO_SEND_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND kihon.KEIYAKUSHO_SEND_DATE <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("3"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND kihon.KEIYAKUSHO_KEIYAKU_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND kihon.KEIYAKUSHO_KEIYAKU_DATE <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("4"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND kihon.YUUKOU_BEGIN >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND kihon.YUUKOU_BEGIN <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("5"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND kihon.YUUKOU_END >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND kihon.YUUKOU_END <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("6"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND kihon.KOUSHIN_END_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND kihon.KOUSHIN_END_DATE <= '{0}'", strTo);
                    }
                }
            }

            // 排出事業者
            strTemp = this.form.GYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND kihon.HAISHUTSU_JIGYOUSHA_CD = '{0}'", strTemp);
            }
            // 排出事業場
            strTemp = this.form.GENBA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND (kihon.HAISHUTSU_JIGYOUJOU_CD = '{0}' OR keiyakuinfo.HAISHUTSU_JIGYOUJOU_CD = '{0}')", strTemp);
            }
            // 運搬業者
            strTemp = this.form.UNPANGYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND itakuBETSU2.UNPAN_GYOUSHA_CD = '{0}'", strTemp);
            }
            // 処分受託者（処分）、処分事業場
            if (!string.IsNullOrWhiteSpace(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.SHOBUN_GENBA_CD.Text))
            {
                sb.Append(" AND EXISTS ( ");
                sb.Append(" SELECT * FROM M_ITAKU_KEIYAKU_BETSU3 WHERE kihon.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU3.SYSTEM_ID ");

                // 処分受託者（処分）
                strTemp = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU3.SHOBUN_GYOUSHA_CD = '{0}' ", strTemp);
                }
                // 処分事業場
                strTemp = this.form.SHOBUN_GENBA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU3.SHOBUN_JIGYOUJOU_CD = '{0}' ", strTemp);
                }
                sb.Append(" ) ");
            }
            // 処分受託者（最終）、最終処分場
            if (!string.IsNullOrWhiteSpace(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.SAISHUU_SHOBUNJOU_CD.Text))
            {
                sb.Append(" AND EXISTS ( ");
                sb.Append(" SELECT * FROM M_ITAKU_KEIYAKU_BETSU4 WHERE kihon.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU4.SYSTEM_ID ");

                // 処分受託者（最終）
                strTemp = this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU4.LAST_SHOBUN_GYOUSHA_CD = '{0}' ", strTemp);
                }
                // 最終処分場
                strTemp = this.form.SAISHUU_SHOBUNJOU_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU4.LAST_SHOBUN_JIGYOUJOU_CD = '{0}'  ", strTemp);
                }
                sb.Append(" ) ");
            }

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                sb.Append(" AND kihon.DELETE_FLG = 0");
            }

            return sb.Length > 0 ? sb.Insert(0, " WHERE 1=1 ").ToString() : string.Empty;
        }
        #endregion
        #region ORDER BY
        /// <summary>
        /// OrderBy句作成
        /// </summary>
        /// <returns></returns>
        private string CreateOrderByQuery()
        {
            var query = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
            {
                query += " ORDER BY " + this.form.OrderByQuery;
            }

            return query;
        }
        #endregion

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        internal void OpenPatternIchiran()
        {
            try
            {
                var sysID = this.form.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }

                //一覧内のチェックボックスの設定
                this.HeaderCheckBoxSupport();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OpenPatternIchiran", ex);
                throw;
            }
        }

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        internal void ClearSearchJyouken()
        {
            LogUtility.DebugMethodStart();

            // 検索条件初期化
            this.form.beforGyousaCD = string.Empty;
            this.form.beforeGenbaCD = string.Empty;
            this.form.GYOUSHA_CD.Clear();
            this.form.GYOUSHA_NAME_RYAKU.Clear();
            this.form.GENBA_CD.Clear();
            this.form.GENBA_NAME_RYAKU.Clear();

            this.form.DATE_SELECT.Text = "7";

            this.form.KEIYAKUSHO_SHURUI_CD.Clear();
            this.form.KEIYAKUSHO_SHURUI_NAME.Clear();

            this.form.KYOKASHOU_SHURUI_CD.Clear();
            this.form.KYOKASHOU_SHURUI_NAME.Clear();

            this.form.KEIYAKU_JYOUKYOU_CD.Clear();
            this.form.KEIYAKU_JYOUKYOU_NAME.Clear();

            this.form.beforeUnpanGyousaCD = string.Empty;
            this.form.UNPANGYOUSHA_CD.Clear();
            this.form.UNPANGYOUSHA_NAME.Clear();

            this.form.beforeShobunJyutakushaShobunCD = string.Empty;
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Clear();
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Clear();

            this.form.beforeShobunGenbaCD = string.Empty;
            this.form.SHOBUN_GENBA_CD.Clear();
            this.form.SHOBUN_GENBA_NAME_RYAKU.Clear();

            this.form.beforeShobunJyutakushaSaishuCD = string.Empty;
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Clear();
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Clear();

            this.form.beforeSaishuShobunCD = string.Empty;
            this.form.SAISHUU_SHOBUNJOU_CD.Clear();
            this.form.SAISHUU_SHOBUNJOU_NAME.Clear();

            //開始
            this.form.DATE_FROM.Clear();
            //終了
            this.form.DATE_TO.Clear();
            this.form.ITAKU_KEIYAKU_NO.Clear();

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            // 削除済のチェックOFF
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;

            // フォーカスを契約番号にする。
            this.form.ITAKU_KEIYAKU_NO.Focus();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 社員CDからクライアントIDを取得する。
        /// </summary>
        internal void GetClientID()
        {
            // 自身の社員CDから、クライアントIDを取得する。
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" CI.DENSHI_KEIYAKU_CLIENT_ID ");
            sql.Append(" FROM M_DENSHI_KEIYAKU_CLIENT_ID CI ");
            sql.Append(" WHERE ");
            sql.AppendFormat(" CI.SHAIN_CD = '{0}' ", this.form.ShainCd);
            sql.Append(" AND CI.DELETE_FLG = 0 ");

            var dt = this.denshiKeiyakuKihonDao.getdateforstringsql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.clientId = dr["DENSHI_KEIYAKU_CLIENT_ID"].ToString();
            }
        }

        /// <summary>
        /// 契約書状況取得
        /// </summary>
        internal bool GetKeiyakuJyoukyou()
        {
            var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_DOCUMENTID);
            var api = string.Format(connect.URL, this.documentId);   // /documents/{0}
            var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

            REQ_COMMON req = new REQ_COMMON();
            req.client_id = this.clientId;
            req.errMessage = "契約状況取得中にエラーとなりました。\nシステム管理者へお問い合わせください。";

            DOCUMENT_MODEL dto;
            var result = this.denshiLogic.HttpGET<DOCUMENT_MODEL>(api, contentType, req, out dto);
            if (result)
            {
                this.form.keiyakuJyoukyouValue = dto.Status;
            }

            return result;
        }

        /// <summary>
        /// 契約状況を取得してテーブルを更新する。
        /// </summary>
        /// <returns></returns>
        internal bool KeiyakuJyoukyouUpdate()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;

            // クライアントIDを取得する。
            this.GetClientID();

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択行からドキュメントIDを取得する。
                bool checkFlg = false;
                if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                {
                    checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                }
                if (checkFlg)
                {
                    this.documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value.ToString();

                    // 契約状況を取得する。
                    ret = this.GetKeiyakuJyoukyou();
                    if (ret)
                    {
                        try
                        {
                            // T_DENSHI_KEIYAKU_KIHONからデータを取得
                            string systemId = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                            string denshiSystemId = row.Cells[ConstCls.KEY_ID2].Value.ToString();
                            T_DENSHI_KEIYAKU_KIHON kihon = this.denshiKeiyakuKihonDao.GetDataByCd(systemId, denshiSystemId);
                            if (kihon == null)
                            {
                                continue;
                            }

                            // 契約状況を設定して更新する。
                            kihon.KEIYAKU_JYOUKYOU = SqlInt32.Parse(this.form.keiyakuJyoukyouValue.ToString());

                            // 更新者情報設定
                            var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(kihon);
                            kihonDataBinderLogic.SetSystemProperty(kihon, false);

                            // 更新
                            using (Transaction tran = new Transaction())
                            {
                                this.denshiKeiyakuKihonDao.Update(kihon);
                                tran.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogUtility.Error("KeiyakuJyoukyouUpdate", ex);
                            if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                            {
                                this.msgLogic.MessageBoxShow("E080");
                            }
                            else if (ex is SQLRuntimeException)
                            {
                                this.msgLogic.MessageBoxShow("E093");
                            }
                            else
                            {
                                this.msgLogic.MessageBoxShow("E245");
                            }
                        }
                        finally
                        {
                            LogUtility.DebugMethodEnd();
                        }
                    }
                    else
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 一覧で選択がチェックされているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool CheckForCheckBox()
        {
            bool ret = false;

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                {
                    ret = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    if (ret)
                    {
                        return ret;
                    }
                }
            }
            if (!ret)
            {
                this.msgLogic.MessageBoxShowError("一覧の選択行が未チェックです。");
            }

            return ret;
        }

        /// <summary>
        /// 契約書ダウンロード前チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckBeforeFileDownload()
        {
            bool ret = false;
            bool notDownload = false;

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // チェックがONの行から、ドキュメントID、削除フラグを取得する。
                bool checkFlg = false;
                if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                {
                    checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                }
                if (checkFlg)
                {
                    var documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;
                    var deleteFlg = row.Cells[ConstCls.HIDDEN_DELETE_FLG].Value;

                    // 契約状況を確認する。
                    this.documentId = documentId.ToString();
                    bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                    // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                    if (!keiyakujyoukyouCheck)
                    {
                        return false;
                    }
                    // 「1:先方確認中」「2:締結済」以外、かつ削除済の場合は警告メッセージを表示する。
                    if (this.form.keiyakuJyoukyouValue > 2 && bool.Parse(deleteFlg.ToString()))
                    {
                        notDownload = true;
                        continue;
                    }
                    else
                    {
                        ret = true;
                    }
                }
            }
            if (notDownload)
            {
                this.msgLogic.MessageBoxShowWarn("削除済みの委託契約書ダウンロードは行えません。");
            }

            return ret;
        }

        /// <summary>
        /// 合意締結証明書ダウンロード前チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckBeforeCertificateDownload()
        {
            bool ret = false;
            bool notTarget = false;

            // 出力区分が「1:伝票」の場合
            if (outputKbn == 1)
            {
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    // チェックがONの行から、システムID、電子システムID、ドキュメントIDを取得する。
                    bool checkFlg = false;
                    if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                    {
                        checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    }
                    if (checkFlg)
                    {
                        var documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;

                        // 契約状況を確認する。
                        this.documentId = documentId.ToString();
                        bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                        // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                        if (!keiyakujyoukyouCheck)
                        {
                            return false;
                        }
                        // 「2:締結済」以外の場合は処理しない。
                        if (this.form.keiyakuJyoukyouValue != 2)
                        {
                            notTarget = true;
                            continue;
                        }
                        else
                        {
                            ret = true;
                        }
                    }
                }
            }
            // 出力区分が「2:明細」の場合
            else if (outputKbn == 2)
            {
                // 同一のドキュメントID（同一の電子契約）データを１つにまとめる。
                Dictionary<string, bool> list = new Dictionary<string, bool>();
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    bool checkFlg = false;
                    if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                    {
                        checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    }
                    // チェックONのデータのみリストに追加する。
                    if (checkFlg)
                    {
                        // 同一のキー（ドキュメントID）があるか確認する。
                        bool keyUmu = list.ContainsKey(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value.ToString());
                        if (keyUmu)
                        {
                            // 同一キーの場合、リストに追加しない。
                            continue;
                        }
                        list.Add(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value.ToString(), checkFlg);
                    }
                }
                foreach (var data in list)
                {
                    var documentId = data.Key;

                    // 契約状況を確認する。
                    this.documentId = documentId.ToString();
                    bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                    // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                    if (!keiyakujyoukyouCheck)
                    {
                        return false;
                    }
                    // 「2:締結済」以外の場合は処理しない。
                    if (this.form.keiyakuJyoukyouValue != 2)
                    {
                        notTarget = true;
                        continue;
                    }
                    else
                    {
                        ret = true;
                    }
                }
            }
            if (notTarget)
            {
                this.msgLogic.MessageBoxShowWarn("契約状況が締結済の委託契約書のみ、合意締結証明書のダウンロードが行えます。");
            }

            return ret;
        }

        /// <summary>
        /// クライアントIDが設定されているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool CheckClientId()
        {
            // クライアントIDを取得する。
            this.GetClientID();

            // クライアントIDが取得できない場合、エラーとする。
            if (this.clientId == null || string.IsNullOrEmpty(this.clientId))
            {
                this.msgLogic.MessageBoxShowError("クライアントIDの登録が行われていません。クラウドサインとの連携処理を中断します。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 電子契約関連ファイルの出力ダイアログ処理
        /// </summary>
        public string OutputDenshikeiyakuFile()
        {
            var directoryName = string.Empty;
            using (var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder())
            {
                var title = "ファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;

                directoryName = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            }

            if (string.IsNullOrWhiteSpace(directoryName))
            {
                return "";
            }

            return directoryName;
        }

        /// <summary>
        /// 契約書ダウンロード
        /// </summary>
        internal bool FileDownload()
        {
            bool ret = false;
            string dir = "";

            if (this.msgLogic.MessageBoxShowConfirm("契約書をダウンロードしてよろしいですか？") == DialogResult.Yes)
            {
                // クライアントIDを取得する。
                this.GetClientID();

                // 共通部品ダイアログを使用する。
                dir = this.OutputDenshikeiyakuFile();

                if (!string.IsNullOrEmpty(dir))
                {
                    // 出力区分によるダウンロードイベント
                    ret = this.OutputKbnForDownLoad(this.outputKbn, dir);
                }
            }
            return ret;
        }

        /// <summary>
        /// 出力区分によるダウンロードイベント
        /// </summary>
        /// <param name="outputKbn">出力区分</param>
        /// <param name="dir">フォルダパス</param>
        /// <returns></returns>
        internal bool OutputKbnForDownLoad(SqlInt16 outputKbn, string dir)
        {
            bool ret = false;
            bool notTarget = false;

            // 出力区分が「1:伝票」の場合
            if (outputKbn == 1)
            {
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    // チェックがONの行から、システムID、電子システムID、ドキュメントIDを取得する。
                    bool checkFlg = false;
                    if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                    {
                        checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    }
                    if (checkFlg)
                    {
                        var documentIdTmp = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;
                        var deleteFlg = row.Cells[ConstCls.HIDDEN_DELETE_FLG].Value;

                        // 契約状況を確認する。
                        this.documentId = documentIdTmp.ToString();
                        bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                        // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                        if (!keiyakujyoukyouCheck)
                        {
                            return false;
                        }
                        // 「1:先方確認中」「2:締結済」以外、かつ削除済の場合は処理しない。
                        if (this.form.keiyakuJyoukyouValue > 2 && bool.Parse(deleteFlg.ToString()))
                        {
                            continue;
                        }

                        string systemId = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                        string denshiSystemId = row.Cells[ConstCls.KEY_ID2].Value.ToString();

                        // T_DENSHI_KEIYAKU_SOUHUINFOを検索して、ファイルIDを取得する。
                        T_DENSHI_KEIYAKU_SOUHUINFO[] souhuInfoDatas = this.denshiKeiyakuSouhuinfoDao.GetDataByCdContainDel(systemId, denshiSystemId);
                        foreach (T_DENSHI_KEIYAKU_SOUHUINFO data in souhuInfoDatas)
                        {
                            // ファイルIDが設定されているファイルのみダウンロードする。
                            if (data.FILE_ID != null || !string.IsNullOrEmpty(data.FILE_ID))
                            {
                                var documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;
                                var fileID = data.FILE_ID;

                                // トークンを取得する。
                                var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_FILEID);
                                var api = string.Format(connect.URL, documentId, fileID);   // /documents/{0}/files/{1}
                                var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                                REQ_COMMON req = new REQ_COMMON();
                                req.client_id = this.clientId;
                                req.errMessage = "契約書ダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                                var fileName = Path.GetFileName(data.FILE_PATH);
                                var filePath = dir + "/" + fileName;
                                var createFilePath = @filePath;
                                //createFilePath = @"C:\edison\download.pdf";

                                bool downLoadResult = this.denshiLogic.HttpGET_DownLoadPDF(api, contentType, req, createFilePath);
                                if (downLoadResult)
                                {
                                    ret = true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            // 出力区分が「2:明細」の場合
            else if (outputKbn == 2)
            {
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    // チェックがONの行からドキュメントID、ファイルIDを取得する。
                    bool checkFlg = false;
                    if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                    {
                        checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    }
                    if (checkFlg)
                    {
                        var documentIdTmp = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;
                        var deleteFlg = row.Cells[ConstCls.HIDDEN_DELETE_FLG].Value;

                        // 契約状況を確認する。
                        this.documentId = documentIdTmp.ToString();
                        bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                        // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                        if (!keiyakujyoukyouCheck)
                        {
                            return false;
                        }
                        // 「1:先方確認中」「2:締結済」以外、かつ削除済の場合は処理しない。
                        if (this.form.keiyakuJyoukyouValue > 2 && bool.Parse(deleteFlg.ToString()))
                        {
                            continue;
                        }

                        var documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;
                        var fileID = row.Cells[ConstCls.HIDDEN_FILE_ID].Value;

                        // 送付していない（ファイルIDが設定されていない）場合、ダウンロード対象外とする。
                        if (fileID == null || string.IsNullOrEmpty(fileID.ToString()))
                        {
                            notTarget = true;
                            continue;
                        }

                        // トークンを取得する。
                        var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_FILEID);
                        var api = string.Format(connect.URL, documentId, fileID);   // /documents/{0}/files/{1}
                        var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                        REQ_COMMON req = new REQ_COMMON();
                        req.client_id = this.clientId;
                        req.errMessage = "契約書ダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                        var fileName = Path.GetFileName(row.Cells[ConstCls.HIDDEN_FILE_PATH].Value.ToString());
                        var filePath = dir + "/" + fileName;
                        var createFilePath = @filePath;
                        //createFilePath = @"C:\edison\download.pdf";

                        bool downLoadResult = this.denshiLogic.HttpGET_DownLoadPDF(api, contentType, req, createFilePath);
                        if (downLoadResult)
                        {
                            ret = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                if (notTarget)
                {
                    this.msgLogic.MessageBoxShowWarn("送付していないファイルはダウンロード対象外となります。");
                }
            }
            return ret;
        }

        /// <summary>
        /// 合意締結証明書ダウンロード
        /// </summary>
        internal bool CertificateDownload()
        {
            bool ret = false;
            string dir = "";

            if (this.msgLogic.MessageBoxShowConfirm("合意締結証明書をダウンロードしてよろしいですか？") == DialogResult.Yes)
            {
                // クライアントIDを取得する。
                this.GetClientID();

                // 共通部品ダイアログを使用する。
                dir = this.OutputDenshikeiyakuFile();

                if (!string.IsNullOrEmpty(dir))
                {
                    // 出力区分によるダウンロードイベント
                    ret = this.OutputKbnForCertificateDownLoad(this.outputKbn, dir);
                }
            }
            return ret;
        }

        /// <summary>
        /// 出力区分による合意締結証明書ダウンロードイベント
        /// </summary>
        /// <param name="outputKbn">出力区分</param>
        /// <param name="dir">フォルダパス</param>
        /// <returns></returns>
        internal bool OutputKbnForCertificateDownLoad(SqlInt16 outputKbn, string dir)
        {
            bool ret = false;

            // 出力区分が「1:伝票」の場合
            if (outputKbn == 1)
            {
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    // チェックがONの行から、システムID、電子システムID、ドキュメントIDを取得する。
                    bool checkFlg = false;
                    if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                    {
                        checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    }
                    if (checkFlg)
                    {
                        var documentId = row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value;

                        // 契約状況を確認する。
                        this.documentId = documentId.ToString();
                        bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                        // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                        if (!keiyakujyoukyouCheck)
                        {
                            return false;
                        }
                        // 「2:締結済」以外の場合は処理しない。
                        if (this.form.keiyakuJyoukyouValue != 2)
                        {
                            continue;
                        }

                        // トークンを取得する。
                        var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_CERTIFICATE);
                        var api = string.Format(connect.URL, documentId);   // /documents/{0}/certificate
                        var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                        REQ_COMMON req = new REQ_COMMON();
                        req.client_id = this.clientId;
                        req.errMessage = "合意締結証明書ダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                        var filePath = dir;
                        // ドキュメントIDをファイル名にする。
                        var createFilePath = @filePath + "/" + documentId + ".pdf";

                        bool downLoadResult = this.denshiLogic.HttpGET_DownLoadPDF(api, contentType, req, createFilePath);
                        if (downLoadResult)
                        {
                            ret = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            // 出力区分が「2:明細」の場合
            else if (outputKbn == 2)
            {
                // 同一のドキュメントID（同一の電子契約）データを１つにまとめる。
                Dictionary<string, bool> list = new Dictionary<string, bool>();
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    bool checkFlg = false;
                    if (row.Cells[ConstCls.CELL_CHECKBOX].Value != null)
                    {
                        checkFlg = bool.Parse(row.Cells[ConstCls.CELL_CHECKBOX].Value.ToString());
                    }
                    // チェックONのデータのみリストに追加する。
                    if (checkFlg)
                    {
                        // 同一のキー（ドキュメントID）があるか確認する。
                        bool keyUmu = list.ContainsKey(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value.ToString());
                        if (keyUmu)
                        {
                            // 同一キーの場合、リストに追加しない。
                            continue;
                        }
                        list.Add(row.Cells[ConstCls.HIDDEN_DOCUMENT_ID].Value.ToString(), checkFlg);
                    }
                }
                foreach (var data in list)
                {
                    var documentId = data.Key;

                    // 契約状況を確認する。
                    this.documentId = documentId.ToString();
                    bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();
                    // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                    if (!keiyakujyoukyouCheck)
                    {
                        return false;
                    }
                    // 「2:締結済」以外の場合は処理しない。
                    if (this.form.keiyakuJyoukyouValue != 2)
                    {
                        continue;
                    }

                    // トークンを取得する。
                    var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_CERTIFICATE);
                    var api = string.Format(connect.URL, documentId);   // /documents/{0}/certificate
                    var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                    REQ_COMMON req = new REQ_COMMON();
                    req.client_id = this.clientId;
                    req.errMessage = "合意締結証明書ダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                    var filePath = dir;
                    // ドキュメントIDをファイル名にする。
                    var createFilePath = @filePath + "/" + documentId + ".pdf";

                    bool downLoadResult = this.denshiLogic.HttpGET_DownLoadPDF(api, contentType, req, createFilePath);
                    if (downLoadResult)
                    {
                        ret = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return ret;
        }


        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        internal bool CheckListPopup(int nPopupID)
        {
            try
            {
                if (nPopupID == 1)
                {
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;

                    //11
                    row = dt.NewRow();
                    row["CD"] = "11";
                    row["VALUE"] = ConstCls.KEIYAKUSHO_SHURUI_NAME_11;
                    dt.Rows.Add(row);
                    //12
                    row = dt.NewRow();
                    row["CD"] = "12";
                    row["VALUE"] = ConstCls.KEIYAKUSHO_SHURUI_NAME_12;
                    dt.Rows.Add(row);
                    //13
                    row = dt.NewRow();
                    row["CD"] = "13";
                    row["VALUE"] = ConstCls.KEIYAKUSHO_SHURUI_NAME_13;
                    dt.Rows.Add(row);
                    //21
                    row = dt.NewRow();
                    row["CD"] = "21";
                    row["VALUE"] = ConstCls.KEIYAKUSHO_SHURUI_NAME_21;
                    dt.Rows.Add(row);
                    //22
                    row = dt.NewRow();
                    row["CD"] = "22";
                    row["VALUE"] = ConstCls.KEIYAKUSHO_SHURUI_NAME_22;
                    dt.Rows.Add(row);
                    //23
                    row = dt.NewRow();
                    row["CD"] = "23";
                    row["VALUE"] = ConstCls.KEIYAKUSHO_SHURUI_NAME_23;
                    dt.Rows.Add(row);

                    form.table = dt;
                    form.PopupTitleLabel = "契約書種類";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "契約書種類CD", "契約書種類名" };
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.KEIYAKUSHO_SHURUI_CD.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.KEIYAKUSHO_SHURUI_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                else if (nPopupID == 2)
                {
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;

                    //31
                    row = dt.NewRow();
                    row["CD"] = "31";
                    row["VALUE"] = ConstCls.KYOKASHOU_SHURUI_NAME_31;
                    dt.Rows.Add(row);
                    //32
                    row = dt.NewRow();
                    row["CD"] = "32";
                    row["VALUE"] = ConstCls.KYOKASHOU_SHURUI_NAME_32;
                    dt.Rows.Add(row);
                    //33
                    row = dt.NewRow();
                    row["CD"] = "33";
                    row["VALUE"] = ConstCls.KYOKASHOU_SHURUI_NAME_33;
                    dt.Rows.Add(row);

                    form.table = dt;
                    form.PopupTitleLabel = "許可証種類";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "許可証種類CD", "許可証種類名" };
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.KYOKASHOU_SHURUI_CD.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.KYOKASHOU_SHURUI_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                else if (nPopupID == 3)
                {
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;

                    //0
                    row = dt.NewRow();
                    row["CD"] = "0";
                    row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_0;
                    dt.Rows.Add(row);
                    //1
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_1;
                    dt.Rows.Add(row);
                    //2
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_2;
                    dt.Rows.Add(row);
                    //3
                    row = dt.NewRow();
                    row["CD"] = "3";
                    row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_3;
                    dt.Rows.Add(row);
                    //4
                    row = dt.NewRow();
                    row["CD"] = "4";
                    row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_4;
                    dt.Rows.Add(row);

                    form.table = dt;
                    form.PopupTitleLabel = "契約状況";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "契約状況CD", "契約状況名" };
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.KEIYAKU_JYOUKYOU_CD.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.KEIYAKU_JYOUKYOU_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckListPopup", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 業者チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckGyousha()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCd = this.form.GYOUSHA_CD.Text;

            // 業者を取得
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.daoGyousha.GetAllValidData(entity);
            if (null == gyousha || gyousha.Length == 0)
            {
                // 業者名設定
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                this.msgLogic.MessageBoxShow("E020", "業者");

                this.form.GYOUSHA_CD.Focus();
                return false;
            }

            // 業者を設定
            if (gyousha[0].HAISHUTSU_NIZUMI_GYOUSHA_KBN)
            {
                this.form.GYOUSHA_NAME_RYAKU.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
            }
            else
            {
                // 業者名設定
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                this.msgLogic.MessageBoxShow("E020", "業者");

                this.form.GYOUSHA_CD.Focus();
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 現場チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "排出事業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    return false;
                }

                var genbaEntityList = this.GetGenba(this.form.GENBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        if (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN)
                        {
                            isContinue = true;
                            this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "業者");
                    this.form.GENBA_CD.Focus();
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_GENBA[] GetGenba(string genbaCd)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd.PadLeft(6, '0');
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var genba = this.daoGenba.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            return genba;
        }
        #region 運搬業者チェック

        /// <summary>
        /// 業者運搬業者
        /// true:OK false:NG
        /// </summary>
        internal bool CheckUnpanGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var UnpangyoushaCd = this.form.UNPANGYOUSHA_CD.Text;
                var UnpangyoushaName = this.form.UNPANGYOUSHA_NAME.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(UnpangyoushaCd))
                {
                    // 関連項目クリア
                    this.form.UNPANGYOUSHA_NAME.Text = String.Empty;
                    this.form.beforeUnpanGyousaCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.UNPANGYOUSHA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.daoGyousha.GetAllValidData(entity);
                if (null == gyousha || gyousha.Length == 0)
                {
                    // 業者名設定
                    this.form.UNPANGYOUSHA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "運搬業者");

                    this.form.UNPANGYOUSHA_CD.Focus();
                    return false;
                }

                // 業者を設定
                if (gyousha[0].UNPAN_JUTAKUSHA_KAISHA_KBN)
                {
                    this.form.UNPANGYOUSHA_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 業者名設定
                    this.form.UNPANGYOUSHA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "運搬業者");

                    this.form.UNPANGYOUSHA_CD.Focus();
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckUnpanGyousha", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分受託者（処分）チェック

        /// <summary>
        /// 処分受託者（処分）
        /// true:OK false:NG
        /// </summary>
        internal bool CheckShobunJyutakushaShobun()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var ShobunJyutakushaShobunCd = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
                var ShobunJyutakushaShobunName = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(ShobunJyutakushaShobunCd))
                {
                    // 関連項目クリア
                    this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = String.Empty;
                    this.form.beforeShobunJyutakushaShobunCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.daoGyousha.GetAllValidData(entity);
                if (null == gyousha || gyousha.Length == 0)
                {
                    // 業者名設定
                    this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "処分受託者（処分）");

                    this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Focus();
                    return false;
                }

                // 業者を設定
                if (gyousha[0].SHOBUN_NIOROSHI_GYOUSHA_KBN)
                {
                    this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 業者名設定
                    this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "処分受託者（処分）");

                    this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Focus();
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShobunJyutakushaShobun", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunJyutakushaShobun", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分事業場チェック
        /// <summary>
        /// 処分事業場チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckShobunGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.SHOBUN_GENBA_CD.Text))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "処分受託者（処分）");
                    this.form.SHOBUN_GENBA_CD.Text = string.Empty;
                    this.form.SHOBUN_GENBA_CD.Focus();
                    return false;
                }

                var genbaEntityList = this.GetGenba(this.form.SHOBUN_GENBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    this.msgLogic.MessageBoxShow("E020", "処分事業場");
                    this.form.SHOBUN_GENBA_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "処分受託者（処分）");
                    this.form.SHOBUN_GENBA_CD.Text = string.Empty;
                    this.form.SHOBUN_GENBA_CD.Focus();
                    return false;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        if (genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN)
                        {
                            isContinue = true;
                            this.form.SHOBUN_GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "処分受託者（処分）");
                    this.form.SHOBUN_GENBA_CD.Focus();
                    this.form.SHOBUN_GENBA_NAME_RYAKU.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShobunGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分受託者（最終）チェック

        /// <summary>
        /// 処分受託者（最終）
        /// true:OK false:NG
        /// </summary>
        internal bool CheckShobunJyutakushaSaishu()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var ShobunJyutakushaSaishuCd = this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
                var ShobunJyutakushaSaishuName = this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(ShobunJyutakushaSaishuCd))
                {
                    // 関連項目クリア
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = String.Empty;
                    this.form.beforeShobunJyutakushaSaishuCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.daoGyousha.GetAllValidData(entity);
                if (null == gyousha || gyousha.Length == 0)
                {
                    // 業者名設定
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "処分受託者（最終）");

                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Focus();
                    return false;
                }

                // 業者を設定
                if (gyousha[0].SHOBUN_NIOROSHI_GYOUSHA_KBN)
                {
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 業者名設定
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "処分受託者（最終）");

                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Focus();
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShobunJyutakushaSaishu", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunJyutakushaSaishu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 最終処分場チェック
        /// <summary>
        /// 最終処分場チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckSaishuGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.SAISHUU_SHOBUNJOU_CD.Text))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "処分受託者（最終）");
                    this.form.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    return false;
                }

                var genbaEntityList = this.GetGenba(this.form.SAISHUU_SHOBUNJOU_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    this.msgLogic.MessageBoxShow("E020", "最終処分場");
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text))
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E051", "処分受託者（最終）");
                    this.form.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    return false;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        if (genbaEntity.SAISHUU_SHOBUNJOU_KBN)
                        {
                            isContinue = true;
                            this.form.SAISHUU_SHOBUNJOU_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    this.msgLogic.MessageBoxShow("E062", "処分受託者（最終）");
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    this.form.SAISHUU_SHOBUNJOU_NAME.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckSaishuGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSaishuGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 契約書種類CDチェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckKeiyakuShuruiCD()
        {
            if (string.IsNullOrEmpty(this.form.KEIYAKUSHO_SHURUI_CD.Text))
            {
                return true;
            }

            // コードチェック
            if (!this.form.KEIYAKUSHO_SHURUI_CD.Text.Equals("11")
                && !this.form.KEIYAKUSHO_SHURUI_CD.Text.Equals("12")
                && !this.form.KEIYAKUSHO_SHURUI_CD.Text.Equals("13")
                && !this.form.KEIYAKUSHO_SHURUI_CD.Text.Equals("21")
                && !this.form.KEIYAKUSHO_SHURUI_CD.Text.Equals("22")
                && !this.form.KEIYAKUSHO_SHURUI_CD.Text.Equals("23")
                )
            {
                this.msgLogic.MessageBoxShow("E020", "契約書種類");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 許可証種類CDチェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckKyokashouCD()
        {
            if (string.IsNullOrEmpty(this.form.KYOKASHOU_SHURUI_CD.Text))
            {
                return true;
            }

            // コードチェック
            if (!this.form.KYOKASHOU_SHURUI_CD.Text.Equals("31")
                && !this.form.KYOKASHOU_SHURUI_CD.Text.Equals("32")
                && !this.form.KYOKASHOU_SHURUI_CD.Text.Equals("33")
                )
            {
                this.msgLogic.MessageBoxShow("E020", "許可証種類");
                return false;
            }
            return true;
        }

        #region 未使用
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
