using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
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
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// フォームオブジェクト
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal UIHeader headerForm;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// クライアントID
        /// </summary>
        private string clientId;

        /// <summary>
        /// ドキュメントID
        /// </summary>
        private string documentId;
        private List<string> documentIds;

        /// <summary>
        /// 電子契約のシステムID
        /// </summary>
        internal string denshiSystemId;

        /// <summary>
        /// 電子契約APIロジッククラス
        /// </summary>
        private DenshiLogic denshiLogic;

        /// <summary>
        /// システム設定Entity
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 電子契約基本Entity
        /// </summary>
        private T_DENSHI_KEIYAKU_KIHON denshiKihon;

        /// <summary>
        /// 電子契約WebAPIの接続情報リスト
        /// </summary>
        private List<S_DENSHI_CONNECT> denshiConnectList;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 電子契約APIのDao
        /// </summary>
        private IS_DENSHI_CONNECTDao denshiConnectDao;

        /// <summary>
        /// 電子契約基本Dao
        /// </summary>
        private DenshiKeiyakuKihonDAO denshiKeiyakuKihonDao;

        /// <summary>
        /// 電子契約仮EntryDao
        /// </summary>
        private DenshiKeiyakuKariEntryDAO denshiKeiyakuKariEntryDao;

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

            this.denshiKeiyakuKariEntryDao = DaoInitUtility.GetComponent<DenshiKeiyakuKariEntryDAO>();
            this.denshiConnectDao = DaoInitUtility.GetComponent<IS_DENSHI_CONNECTDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.denshiKeiyakuKihonDao = DaoInitUtility.GetComponent<DenshiKeiyakuKihonDAO>();
            this.denshiLogic = new DenshiLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

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

                // フォーム初期化
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)parentForm.headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // ボタン制御
                this.ButtonEnabledControl();

                // イベントの初期化処理
                this.EventInit();

                // 行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // ヘッダ初期化
                this.headerForm.readDataNumber.Text = "0";

                // システム設定の読み込み
                this.GetSysInfo();

                S_DENSHI_CONNECT[] denshiConnectData = this.denshiConnectDao.GetAllData();
                this.denshiConnectList = new List<S_DENSHI_CONNECT>(denshiConnectData);

                // 検索条件ポップアップ内容生成
                this.CreateConditionPopup();
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

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

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
        /// ファンクションボタンの制御
        /// </summary>
        private void ButtonEnabledControl()
        {
            // 初期化
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func10.Enabled = true;
            parentForm.bt_func11.Enabled = true;
            parentForm.bt_func12.Enabled = true;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 電子契約照会(F1)イベント作成
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            // CSV(F6)イベント作成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);
            // 条件クリア(F7)イベント作成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);
            // 検索(F8)イベント作成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            //// 登録(F9)イベント作成
            //parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            // 並び替え(F10)イベント作成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);
            // フィルタ(F11)イベント作成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);
            // 閉じる(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);
            // パターン登録
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
            // ファイルダウンロード
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
            // 契約状況取得
            parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
            // 合意締結証明書
            parentForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        #endregion

        #region ファンクションイベントの詳細処理

        #region F7 条件クリア

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        internal void ClearSearchJyouken()
        {
            LogUtility.DebugMethodStart();

            // 検索条件初期化
            this.form.DATE_SELECT.Text = "6";
            this.form.DATE_FROM.Clear();
            this.form.DATE_TO.Clear();

            this.form.KEIYAKU_JYOUKYOU_CD.Clear();
            this.form.KEIYAKU_JYOUKYOU_NAME.Clear();

            this.form.SOUFU_TITLE.Clear();
            this.form.KEIYAKUSHA.Clear();
            this.form.FILE_NAME.Clear();

            this.form.CONDITION_ITEM1.Clear();
            this.form.CONDITION_VALUE1.Clear();
            this.form.CONDITION_ITEM2.Clear();
            this.form.CONDITION_VALUE2.Clear();
            this.form.CONDITION_ITEM3.Clear();
            this.form.CONDITION_VALUE3.Clear();
            this.form.CONDITION_ITEM4.Clear();
            this.form.CONDITION_VALUE4.Clear();

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            // フォーカスを契約番号にする。
            this.form.KEIYAKU_JYOUKYOU_CD.Focus();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F8 検索

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this.form.Table = null;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();
                this.form.Table = this.denshiKeiyakuKariEntryDao.getDateForStringSql(sql);
            }

            // 検索結果を画面に表示
            this.form.ShowData();

            // 読込データ件数を表示する。
            if (this.form.Table != null)
            {
                this.headerForm.readDataNumber.Text = this.form.Table.Rows.Count.ToString();
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
            if (string.IsNullOrWhiteSpace(this.form.DATE_FROM.Text)) { return true; }
            if (string.IsNullOrWhiteSpace(this.form.DATE_TO.Text)) { return true; }

            DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                if (this.form.DATE_SELECT_1.Checked)
                {
                    string[] errorMsg = { "作成日From", "作成日To" };
                    this.msgLogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_2.Checked)
                {
                    string[] errorMsg = { "送付日From", "送付日To" };
                    this.msgLogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_3.Checked)
                {
                    string[] errorMsg = { "契約日From", "契約日To" };
                    this.msgLogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_4.Checked)
                {
                    string[] errorMsg = { "有効期間開始From", "有効期間開始To" };
                    this.msgLogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_5.Checked)
                {
                    string[] errorMsg = { "有効期間終了From", "有効期間終了To" };
                    this.msgLogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_6.Checked)
                {
                    string[] errorMsg = { "自動更新終了日From", "自動更新終了日To" };
                    this.msgLogic.MessageBoxShow("E030", errorMsg);
                }

                this.form.DATE_FROM.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckCondition()
        {
            // 検索条件1
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE1.Text))
            {
                if (string.IsNullOrEmpty(this.form.CONDITION_ITEM1.Text))
                {
                    // 検索条件入力済みで検索対象が未入力の場合アラート
                    msgLogic.MessageBoxShowError("検索条件1の検索対象が未入力です。検索条件を指定してください。");
                    this.form.CONDITION_ITEM1.Focus();
                    return false;
                }
            }

            // 検索条件2
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE2.Text))
            {
                if (string.IsNullOrEmpty(this.form.CONDITION_ITEM2.Text))
                {
                    // 検索条件入力済みで検索対象が未入力の場合アラート
                    msgLogic.MessageBoxShowError("検索条件2の検索対象が未入力です。検索条件を指定してください。");
                    this.form.CONDITION_ITEM2.Focus();
                    return false;
                }
            }

            // 検索条件3
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE3.Text))
            {
                if (string.IsNullOrEmpty(this.form.CONDITION_ITEM3.Text))
                {
                    // 検索条件入力済みで検索対象が未入力の場合アラート
                    msgLogic.MessageBoxShowError("検索条件3の検索対象が未入力です。検索条件を指定してください。");
                    this.form.CONDITION_ITEM3.Focus();
                    return false;
                }
            }

            // 検索条件4
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE4.Text))
            {
                if (string.IsNullOrEmpty(this.form.CONDITION_ITEM4.Text))
                {
                    // 検索条件入力済みで検索対象が未入力の場合アラート
                    msgLogic.MessageBoxShowError("検索条件4の検索対象が未入力です。検索条件を指定してください。");
                    this.form.CONDITION_ITEM4.Focus();
                    return false;
                }
            }

            return true;
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

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                //表示用項目
                sb.Append("SELECT DISTINCT ");

                // 非活性項目
                sb.AppendFormat(" ENTRY.DOCUMENT_ID AS {0}, ", ConstCls.KEY_ID1);
                sb.AppendFormat(" ENTRY.SEND_MESSAGE AS {0}, ", ConstCls.HIDDEN_SEND_MESSAGE);
                sb.AppendFormat(" ENTRY.SEND_TITLE AS {0}, ", ConstCls.HIDDEN_SEND_TITLE);
                sb.AppendFormat(" CASE WHEN KIHON.SYSTEM_ID IS NULL THEN '' ELSE '済' END AS {0}, ", ConstCls.HIDDEN_RENKEI);
                sb.AppendFormat(" ENTRY.AUTO_UPDATE AS {0}, ", ConstCls.HIDDEN_AUTO_UPDATE);
                sb.AppendFormat(" ENTRY.DELETE_FLG AS {0}, ", ConstCls.HIDDEN_DELETE_FLG);
                sb.AppendFormat(" ENTRY.KEIYAKU_JYOUKYOU AS {0}, ", ConstCls.HIDDEN_KEIYAKU_JYOUKYOU);
                sb.AppendFormat(" ENTRY.SHANAI_BIKO AS {0}, ", ConstCls.HIDDEN_SHANAI_BIKO);
                sb.AppendFormat(" ENTRY.KEIYAKUSHO_KEIYAKU_DATE AS {0}, ", ConstCls.HIDDEN_KEIYAKUSHO_KEIYAKU_DATE);
                sb.AppendFormat(" ENTRY.KEIYAKUSHO_CREATE_DATE AS {0}, ", ConstCls.HIDDEN_KEIYAKUSHO_CREATE_DATE);
                sb.AppendFormat(" ENTRY.YUUKOU_BEGIN AS {0}, ", ConstCls.HIDDEN_YUUKOU_BEGIN);
                sb.AppendFormat(" ENTRY.YUUKOU_END AS {0}, ", ConstCls.HIDDEN_YUUKOU_END);
                sb.AppendFormat(" DETAIL.FILE_ID AS {0}, ", ConstCls.HIDDEN_FILE_ID);
                sb.AppendFormat(" DETAIL.FILE_NAME AS {0}, ", ConstCls.HIDDEN_FILE_NAME);


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

            // 仮電子契約伝票
            sb.Append(" FROM T_DENSHI_KEIYAKU_KARI_ENTRY ENTRY ");

            // 仮電子契約明細
            sb.Append(" LEFT JOIN T_DENSHI_KEIYAKU_KARI_DETAIL DETAIL ");
            sb.Append(" ON ENTRY.DOCUMENT_ID = DETAIL.DOCUMENT_ID ");

            // 電子契約伝票
            sb.Append(" LEFT JOIN T_DENSHI_KEIYAKU_KIHON KIHON ");
            sb.Append(" ON ENTRY.DOCUMENT_ID = KIHON.DOCUMENT_ID AND KIHON.DELETE_FLG = 0 ");

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

            // 契約状況
            strTemp = this.form.KEIYAKU_JYOUKYOU_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND ENTRY.KEIYAKU_JYOUKYOU = '{0}'", strTemp);
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
                        sb.AppendFormat(" AND ENTRY.CREATE_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        strTo = Convert.ToDateTime(this.form.DATE_TO.Value).ToString("yyyy/MM/dd 23:59:59");
                        sb.AppendFormat(" AND ENTRY.CREATE_DATE <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("2"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND ENTRY.UPDATE_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        strTo = Convert.ToDateTime(this.form.DATE_TO.Value).ToString("yyyy/MM/dd 23:59:59");
                        sb.AppendFormat(" AND ENTRY.UPDATE_DATE <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("3"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND ENTRY.KEIYAKUSHO_KEIYAKU_DATE >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND ENTRY.KEIYAKUSHO_KEIYAKU_DATE <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("4"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND ENTRY.YUUKOU_BEGIN >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND ENTRY.YUUKOU_BEGIN <= '{0}'", strTo);
                    }
                }
                else if (strTemp.Equals("5"))
                {
                    if (!string.IsNullOrWhiteSpace(strFrom))
                    {
                        sb.AppendFormat(" AND ENTRY.YUUKOU_END >= '{0}'", strFrom);
                    }
                    if (!string.IsNullOrWhiteSpace(strTo))
                    {
                        sb.AppendFormat(" AND ENTRY.YUUKOU_END <= '{0}'", strTo);
                    }
                }
            }

            // 送付タイトル
            strTemp = this.form.SOUFU_TITLE.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND ENTRY.SEND_TITLE LIKE '%{0}%'", strTemp);
            }

            // 契約者
            strTemp = this.form.KEIYAKUSHA.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND ENTRY.KEIYAKUSHA LIKE '%{0}%'", strTemp);
            }

            // ファイル名
            strTemp = this.form.FILE_NAME.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(" AND DETAIL.FILE_NAME LIKE '%{0}%'", strTemp);
            }

            // 検索条件
            strTemp = this.makeWere();
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                sb.AppendFormat(strTemp);
            }

            return sb.Length > 0 ? sb.Insert(0, " WHERE 1=1 ").ToString() : string.Empty;
        }

        #region 検索条件1～4

        /// <summary>
        /// 検索条件項目名毎に検索条件を作成する
        /// </summary>
        private string makeWere()
        {
            Dictionary<string, List<string>> condition = new Dictionary<string, List<string>>();
            addDictionary(ref condition, "SHORUI_INFO_NAME", this.form.CONDITION_VALUE1.Text, this.form.CONDITION_ITEM1.Text);
            addDictionary(ref condition, "SHORUI_INFO_NAME", this.form.CONDITION_VALUE2.Text, this.form.CONDITION_ITEM2.Text);
            addDictionary(ref condition, "SHORUI_INFO_NAME", this.form.CONDITION_VALUE3.Text, this.form.CONDITION_ITEM3.Text);
            addDictionary(ref condition, "SHORUI_INFO_NAME", this.form.CONDITION_VALUE4.Text, this.form.CONDITION_ITEM4.Text);

            string appendWhere = string.Empty;
            foreach (var fieldName in condition)
            {
                appendWhere += " AND (";
                for (var idx = 0; idx < fieldName.Value.Count; idx++)
                {
                    appendWhere += "ENTRY." + fieldName.Key + " LIKE '%" + fieldName.Value[idx] + "%'";
                    if (idx < fieldName.Value.Count - 1)
                    {
                        appendWhere += " OR ";
                    }
                }
                appendWhere += ")\n";
            }
            return appendWhere;
        }

        /// <summary>
        /// 検索条件項目名毎に検索条件を追加する
        /// </summary>
        private void addDictionary(ref Dictionary<string, List<string>> condition, string item, string value, string column)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!condition.ContainsKey(item))
                {
                    item = item + Regex.Replace(column.Substring(0, 6), @"[^0-9]", "");
                    condition.Add(item, new List<string>());
                }
                condition[item].Add(value);
            }
        }

        private void addDictionary(ref List<string> condition, string item, string value)
        {
            if (!string.IsNullOrWhiteSpace(item)
            && !string.IsNullOrWhiteSpace(value))
            {
                condition.Add(item + "," + value);
            }
        }

        /// <summary>
        /// 検索文字列の整形
        /// </summary>
        /// <param name="tmp">検索文字列</param>
        /// <returns></returns>
        private string SearchStringFix(string tmp)
        {
            // DateTime
            DateTime datetime;
            bool testTime = false;
            testTime = DateTime.TryParse(tmp, out datetime);
            if (testTime)
            {
                tmp = tmp.Replace('-', '/');
            }
            return tmp;
        }

        #endregion

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

        #endregion

        #region F9 電子契約登録

        /// <summary>
        /// 登録前のチェック処理
        /// </summary>
        /// <returns></returns>
        internal bool CheckBeforeRegistData()
        {
            bool ret = false;
            bool notRegist = false;

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
                    // 電子契約登録済みチェック
                    if (row.Cells[ConstCls.HIDDEN_RENKEI].Value.ToString() == "済")
                    {
                        this.msgLogic.MessageBoxShowWarn("送付先タイトル「" + row.Cells[ConstCls.HIDDEN_SEND_TITLE].Value.ToString() + "」は、既に電子契約が登録済みのため、登録処理をスキップします。");
                        continue;
                    }

                    // 契約状況チェック
                    var documentId = row.Cells[ConstCls.KEY_ID1].Value;

                    // 契約状況を確認する。
                    this.documentId = documentId.ToString();
                    bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();

                    // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                    if (!keiyakujyoukyouCheck) { return false; }

                    // 「1:先方確認中」「2:締結済」以外の場合は警告メッセージを表示する。
                    if (this.form.keiyakuJyoukyouValue > 2 || this.form.keiyakuJyoukyouValue == 0)
                    {
                        notRegist = true;
                        continue;
                    }
                    else
                    {
                        ret = true;
                    }
                }
            }
            if (notRegist)
            {
                this.msgLogic.MessageBoxShowWarn("契約状況が「下書き、取り消し、または却下、テンプレート」の場合は\r\n電子契約の登録が行えません。");
            }

            return ret;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <returns></returns>
        internal bool RegistData()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;

            try
            {
                this.documentIds = new List<string>();

                // 1件ずつチェック、登録していく
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
                        // 送付済みは飛ばす
                        if (row.Cells[ConstCls.HIDDEN_RENKEI].Value.ToString() == "済") { continue; }

                        // 契約状況チェック
                        this.documentId = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                        bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();

                        // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                        if (!keiyakujyoukyouCheck) { return false; }

                        // 「1:先方確認中」「2:締結済」以外の場合は飛ばす
                        if (this.form.keiyakuJyoukyouValue > 2 || this.form.keiyakuJyoukyouValue == 0) { continue; }

                        // 登録済みのデータは飛ばす
                        if (this.documentIds.Contains(this.documentId)) { continue; }

                        // 電子契約基本データ作成
                        this.CreateDenshiKihon(row);

                        // 他タブ情報は作らない(現時点では作りようがない)
                        using (Transaction tran = new Transaction())
                        {
                            // T_DENSHI_KEIYAKU_KIHONのデータを登録
                            this.denshiKeiyakuKihonDao.Insert(this.denshiKihon);

                            tran.Commit();
                            ret = true;
                        }

                        // リストに登録したDOCUMENT_IDをセット
                        this.documentIds.Add(this.documentId);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateDisplayData", ex);
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

            return ret;
        }

        #region CeateEntiry

        /// <summary>
        /// 電子契約基本データを作成する。
        /// </summary>
        /// <param name="newFlg"></param>
        /// <returns></returns>
        private void CreateDenshiKihon(DataGridViewRow row)
        {
            this.denshiKihon = new T_DENSHI_KEIYAKU_KIHON();

            // 紐付く委託契約伝票がこの段階では未定なのでシステムIDは未設定
            this.denshiKihon.SYSTEM_ID = string.Empty;

            // 電子契約システムIDを付番して設定
            this.CreateSystemIdForDenshi();
            this.denshiKihon.DENSHI_KEIYAKU_SYSTEM_ID = this.denshiSystemId;

            // T_DENSHI_KEIYAKU_KIHONに設定
            this.denshiKihon.SHAIN_CD = SystemProperty.Shain.CD;
            this.denshiKihon.DENSHI_KEIYAKU_CLIENT_ID = this.clientId;
            this.denshiKihon.KEIYAKU_JYOUKYOU = SqlInt32.Parse(row.Cells[ConstCls.HIDDEN_KEIYAKU_JYOUKYOU].Value.ToString());
            this.denshiKihon.SEND_TITLE = row.Cells[ConstCls.HIDDEN_SEND_TITLE].Value.ToString();
            this.denshiKihon.SEND_MESSAGE = row.Cells[ConstCls.HIDDEN_SEND_MESSAGE].Value.ToString();
            this.denshiKihon.SHANAI_BIKO = row.Cells[ConstCls.HIDDEN_SHANAI_BIKO].Value.ToString();
            this.denshiKihon.DOCUMENT_ID = row.Cells[ConstCls.KEY_ID1].Value.ToString();
            if (!string.IsNullOrEmpty(row.Cells[ConstCls.HIDDEN_YUUKOU_BEGIN].Value.ToString()))
            {
                this.denshiKihon.YUUKOU_BEGIN = SqlDateTime.Parse(row.Cells[ConstCls.HIDDEN_YUUKOU_BEGIN].Value.ToString());
            }
            if (!string.IsNullOrEmpty(row.Cells[ConstCls.HIDDEN_YUUKOU_END].Value.ToString()))
            {
                this.denshiKihon.YUUKOU_END = SqlDateTime.Parse(row.Cells[ConstCls.HIDDEN_YUUKOU_END].Value.ToString());
            }
            if (!string.IsNullOrEmpty(row.Cells[ConstCls.HIDDEN_KEIYAKUSHO_KEIYAKU_DATE].Value.ToString()))
            {
                this.denshiKihon.KEIYAKUSHO_KEIYAKU_DATE = SqlDateTime.Parse(row.Cells[ConstCls.HIDDEN_KEIYAKUSHO_KEIYAKU_DATE].Value.ToString());
            }
            if (!string.IsNullOrEmpty(row.Cells[ConstCls.HIDDEN_KEIYAKUSHO_CREATE_DATE].Value.ToString()))
            {
                this.denshiKihon.KEIYAKUSHO_CREATE_DATE = SqlDateTime.Parse(row.Cells[ConstCls.HIDDEN_KEIYAKUSHO_CREATE_DATE].Value.ToString());
            }

            // 更新者情報設定
            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KIHON>(this.denshiKihon);
            dataBinderLogic.SetSystemProperty(this.denshiKihon, false);
        }

        /// <summary>
        /// 電子契約システムID採番
        /// </summary>
        private void CreateSystemIdForDenshi()
        {
            // T_DENSHI_KEIYAKU_KIHONの電子契約システムIDを採番する。
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" MAX(DENSHI_KEIYAKU_SYSTEM_ID) AS DENSHI_KEIYAKU_SYSTEM_ID ");
            sql.Append(" FROM T_DENSHI_KEIYAKU_KIHON ");

            // 委託契約書のシステムIDに合致する電子契約システムIDがある場合、インクリメントして設定。
            // ない場合、新たに1を設定。
            var dt = this.denshiKeiyakuKihonDao.getdateforstringsql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string maxDenshiSystemID = dr["DENSHI_KEIYAKU_SYSTEM_ID"].ToString();

                if (!string.IsNullOrEmpty(maxDenshiSystemID))
                {
                    this.denshiSystemId = String.Format("{0:D9}", Int64.Parse(maxDenshiSystemID) + 1);
                }
                else
                {
                    this.denshiSystemId = String.Format("{0:D9}", 1);
                }
            }
            else
            {
                this.denshiSystemId = String.Format("{0:D9}", 1);
            }
        }

        #endregion

        #endregion

        #region subF1 パターン一覧
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
        #endregion

        #region subF2 ﾌｧｲﾙﾀﾞｳﾝﾛｰﾄﾞ

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
                    var documentId = row.Cells[ConstCls.KEY_ID1].Value;
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
                this.msgLogic.MessageBoxShowWarn("削除済みのファイルダウンロードは行えません。");
            }

            return ret;
        }

        /// <summary>
        /// ファイルダウンロード
        /// </summary>
        internal bool FileDownload()
        {
            bool ret = false;
            string dir = "";

            if (this.msgLogic.MessageBoxShowConfirm("ファイルをダウンロードしてよろしいですか？") == DialogResult.Yes)
            {
                // クライアントIDを取得する。
                this.GetClientID();

                // 共通部品ダイアログを使用する。
                dir = this.OutputDenshikeiyakuFile();

                if (!string.IsNullOrEmpty(dir))
                {
                    // ダウンロードイベント
                    ret = this.OutputForDownLoad(dir);
                }
            }
            return ret;
        }

        /// <summary>
        /// 電子契約関連ファイルの出力ダイアログ処理
        /// </summary>
        private string OutputDenshikeiyakuFile()
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

            if (string.IsNullOrWhiteSpace(directoryName)) { return string.Empty; }

            return directoryName;
        }

        /// <summary>
        /// ダウンロードイベント
        /// </summary>
        /// <param name="dir">フォルダパス</param>
        /// <returns></returns>
        private bool OutputForDownLoad(string dir)
        {
            bool ret = false;

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
                    this.documentId = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                    var fileID = row.Cells[ConstCls.HIDDEN_FILE_ID].Value;

                    // 契約状況を確認する。
                    bool keiyakujyoukyouCheck = this.GetKeiyakuJyoukyou();

                    // 契約状況取得でエラーとなった場合、以降の処理は行わない。
                    if (!keiyakujyoukyouCheck) { return false; }

                    // 「1:先方確認中」「2:締結済」以外の場合は処理しない。
                    if (this.form.keiyakuJyoukyouValue > 2) { continue; }

                    // 送付していない（ファイルIDが設定されていない）場合、ダウンロード対象外とする。
                    if (fileID == null || string.IsNullOrEmpty(fileID.ToString())) { continue; }

                    // トークンを取得する。
                    var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_FILEID);
                    var api = string.Format(connect.URL, this.documentId, fileID);   // /documents/{0}/files/{1}
                    var contentType = connect.CONTENT_TYPE;                     // multipart/form-data

                    REQ_COMMON req = new REQ_COMMON();
                    req.client_id = this.clientId;
                    req.errMessage = "ファイルダウンロード中にエラーとなりました。\nシステム管理者へお問い合わせください。";

                    var fileName = Path.GetFileName(row.Cells[ConstCls.HIDDEN_FILE_NAME].Value.ToString());
                    var filePath = dir + "/" + fileName;
                    var createFilePath = @filePath;

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

        #endregion

        #region subF3

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
                    this.documentId = row.Cells[ConstCls.KEY_ID1].Value.ToString();

                    // 契約状況を取得する。
                    ret = this.GetKeiyakuJyoukyou();
                    if (ret)
                    {
                        try
                        {
                            // T_DENSHI_KEIYAKU_KARI_ENTRYからデータを取得
                            string documentId = row.Cells[ConstCls.KEY_ID1].Value.ToString();
                            T_DENSHI_KEIYAKU_KARI_ENTRY kari = this.denshiKeiyakuKariEntryDao.GetDataByCd(documentId);
                            if (kari == null)
                            {
                                continue;
                            }

                            // 契約状況を設定して更新する。
                            kari.KEIYAKU_JYOUKYOU = SqlInt32.Parse(this.form.keiyakuJyoukyouValue.ToString());

                            // 更新者情報設定
                            var kihonDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_DENSHI_KEIYAKU_KARI_ENTRY>(kari);
                            kihonDataBinderLogic.SetSystemProperty(kari, false);

                            // 更新
                            using (Transaction tran = new Transaction())
                            {
                                this.denshiKeiyakuKariEntryDao.Update(kari);
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

        #endregion

        #region subF4

        /// <summary>
        /// 合意締結証明書ダウンロード前チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckBeforeCertificateDownload()
        {
            bool ret = false;
            bool notTarget = false;


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
                    bool keyUmu = list.ContainsKey(row.Cells[ConstCls.KEY_ID1].Value.ToString());
                    if (keyUmu)
                    {
                        // 同一キーの場合、リストに追加しない。
                        continue;
                    }
                    list.Add(row.Cells[ConstCls.KEY_ID1].Value.ToString(), checkFlg);
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

            if (notTarget)
            {
                this.msgLogic.MessageBoxShowWarn("契約状況が締結済の委託契約書のみ、合意締結証明書のダウンロードが行えます。");
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
                    ret = this.OutputKbnForCertificateDownLoad(dir);
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
        internal bool OutputKbnForCertificateDownLoad(string dir)
        {
            bool ret = false;


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
                    bool keyUmu = list.ContainsKey(row.Cells[ConstCls.KEY_ID1].Value.ToString());
                    if (keyUmu)
                    {
                        // 同一キーの場合、リストに追加しない。
                        continue;
                    }
                    list.Add(row.Cells[ConstCls.KEY_ID1].Value.ToString(), checkFlg);
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

            return ret;
        }

        #endregion

        #region 汎用

        /// <summary>
        /// 社員CDからクライアントIDを取得する。
        /// </summary>
        private void GetClientID()
        {
            // 自身の社員CDから、クライアントIDを取得する。
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" CI.DENSHI_KEIYAKU_CLIENT_ID ");
            sql.Append(" FROM M_DENSHI_KEIYAKU_CLIENT_ID CI ");
            sql.Append(" WHERE ");
            sql.AppendFormat(" CI.SHAIN_CD = '{0}' ", SystemProperty.Shain.CD);
            sql.Append(" AND CI.DELETE_FLG = 0 ");

            var dt = this.denshiKeiyakuKariEntryDao.getDateForStringSql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.clientId = dr["DENSHI_KEIYAKU_CLIENT_ID"].ToString();
            }
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
                    if (ret) { return ret; }
                }
            }
            if (!ret)
            {
                this.msgLogic.MessageBoxShowError("一覧の選択行が未チェックです。");
            }

            return ret;
        }

        /// <summary>
        /// 契約書状況取得
        /// </summary>
        private bool GetKeiyakuJyoukyou()
        {
            var connect = this.denshiConnectDao.GetDataByContentName(DenshiConst.CONTENT_NAME_DOCUMENTID);
            var api = string.Format(connect.URL, this.documentId);  // /documents/{0}
            var contentType = connect.CONTENT_TYPE;                 // application/x-www-form-urlencoded

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

        #endregion

        #endregion

        #region チェックボックス絡み
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }
        #endregion

        #region ポップアップ生成

        /// <summary>
        /// ポップアップ生成処理
        /// </summary>
        /// <param name="e"></param>
        internal bool CreateKeiyakuJyoukyouPopup()
        {
            try
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
        /// 検索条件1～4で使用するポップアップ
        /// 非表示のDGVでDSを設定する
        /// </summary>
        private void CreateConditionPopup()
        {
            string sql = "SELECT SHORUI_INFO_NAME FROM M_DENSHI_KEIYAKU_SHORUI_INFO INFO ORDER BY LEN(INFO.SHORUI_INFO_ID), INFO.SHORUI_INFO_ID";
            DataTable dgv = this.denshiKeiyakuKariEntryDao.getDateForStringSql(sql);

            DataTable dt = new DataTable();
            int i = 1;
            foreach (DataRow r in dgv.Rows)
            {
                dt.Columns.Add("書類項目" + i + "：" + r["SHORUI_INFO_NAME"], typeof(string));
                i++;
            }
            this.form.dgv_CONDITION.DataSource = dt;
        }

        #endregion

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
