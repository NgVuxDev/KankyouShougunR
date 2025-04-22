// $Id: LogicClass.cs 54103 2015-06-30 08:35:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.SetchiContenaIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.SetchiContenaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private UIForm form;

        /// <summary>
        /// フォームオブジェクト
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// システム設定マスタのエンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 現場マスタのエンティティ
        /// </summary>
        private M_GENBA genbaEntity;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// 外部連携現場のDao
        /// </summary>
        private IM_GENBA_DIGIDao daoGenbaDigi;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao daoTodoufuken;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            //Form
            this.form = targetForm;

            //Dao
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoGenbaDigi = DaoInitUtility.GetComponent<IM_GENBA_DIGIDao>();

            //メッセージ
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //フォーム初期化
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)parentForm.headerForm;

                this.form.bt_ptn1.Visible = false;
                this.form.bt_ptn2.Visible = false;
                this.form.bt_ptn3.Visible = false;
                this.form.bt_ptn4.Visible = false;
                this.form.bt_ptn5.Visible = false;

                //システム設定情報読み込み
                this.GetSysInfo();

                // ヘッダーの初期化
                this.InitHeaderArea();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //Functionボタンのイベント生成
            this.parentForm.bt_func6.Click += new System.EventHandler(this.form.bt_func6_Click);      // CSV
            this.parentForm.bt_func7.Click += new System.EventHandler(this.form.bt_func7_Click);      // 検索条件クリア
            this.parentForm.bt_func8.Click += new System.EventHandler(this.form.bt_func8_Click);      // 検索
            this.parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);    // 並び替え
            this.parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);    // フィルタ
            this.parentForm.bt_func12.Click += new System.EventHandler(this.form.bt_func12_Click);    // 閉じる
            this.parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);       // 地図表示

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region システム設定取得

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        #endregion

        #region ヘッダーの初期化

        private void InitHeaderArea()
        {
            // 初期値設定
            this.form.GYOUSHA_CD.Text = SetchiContenaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT;
            this.form.GYOUSHA_RNAME.Text = SetchiContenaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT;
            this.form.GENBA_CD.Text = SetchiContenaIchiran.Properties.Settings.Default.GENBA_CD_TEXT;

            //現場名をセット
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                M_GENBA condition = new M_GENBA();

                condition.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                condition.GENBA_CD = this.form.GENBA_CD.Text;

                this.genbaEntity = daoGenba.GetDataByCd(condition);
                this.form.GENBA_RNAME.Text = this.genbaEntity == null ? string.Empty : genbaEntity.GENBA_NAME_RYAKU;
            }

            //前回業者CDを設定
            this.form.beforGyoushaCD = this.form.GYOUSHA_CD.Text;
        }

        #endregion

        #endregion

        #region ファンクション

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
                this.form.Table = this.daoGenba.GetDateForStringSql(sql);
            }

            this.form.ShowData();

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }
        #endregion

        #region SQL生成
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
                sb.AppendFormat(" MG.GYOUSHA_CD AS {0}, ", ConstCls.KEY_ID1);
                sb.AppendFormat(" MG.GENBA_CD AS {0}, ", ConstCls.KEY_ID2);

                // 連携状況
                sb.Append(" CASE WHEN MDOG.OUTPUT_DATE IS NULL THEN '未' ELSE '済' END 連携状況, ");

                //マスタ差分
                sb.Append(" CASE WHEN MDOG.OUTPUT_DATE IS NULL THEN '―'");
                sb.Append("      WHEN MG.UPDATE_DATE > MDOG.OUTPUT_DATE THEN '有'");
                sb.Append("      ELSE '―' END マスタ差分, ");

                //パターン一覧からの表示列
                sb.Append(this.form.SelectQuery);

                //以下、非表示項目
                //RequestBody作成用に、固定で非表示抽出させたい
                sb.AppendFormat(" ,MGD.POINT_ID AS {0}", ConstCls.HIDDEN_POINT_ID);
                sb.AppendFormat(" ,MGD.POINT_NAME AS {0}", ConstCls.HIDDEN_POINT_NAME);
                sb.AppendFormat(" ,MGD.POINT_KANA_NAME AS {0}", ConstCls.HIDDEN_POINT_KANA_NAME);
                sb.AppendFormat(" ,MGD.MAP_NAME AS {0}", ConstCls.HIDDEN_MAP_NAME);
                sb.AppendFormat(" ,MGD.POST_CODE AS {0}", ConstCls.HIDDEN_POST_CODE);
                sb.AppendFormat(" ,MGTO.TODOUFUKEN_NAME AS {0}", ConstCls.HIDDEN_PREFECTURES);
                sb.AppendFormat(" ,MGD.ADDRESS1 AS {0}", ConstCls.HIDDEN_ADDRESS1);
                sb.AppendFormat(" ,MGD.ADDRESS2 AS {0}", ConstCls.HIDDEN_ADDRESS2);
                sb.AppendFormat(" ,MGD.TEL_NO AS {0}", ConstCls.HIDDEN_TEL_NO);
                sb.AppendFormat(" ,MGD.FAX_NO AS {0}", ConstCls.HIDDEN_FAX_NO);
                sb.AppendFormat(" ,MGD.CONTACT_NAME AS {0}", ConstCls.HIDDEN_CONTACT_NAME);
                sb.AppendFormat(" ,MGD.MAIL_ADDRESS AS {0}", ConstCls.HIDDEN_MAIL_ADDRESS);
                sb.AppendFormat(" ,MGD.RANGE_RADIUS AS {0}", ConstCls.HIDDEN_RANGE_RADIUS);
                sb.AppendFormat(" ,MGD.REMARKS AS {0}", ConstCls.HIDDEN_REMARKS);

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

            // 現場マスタ
            sb.Append(" FROM M_GENBA MG ");

            // 都道府県マスタ(現場)
            sb.Append(" LEFT JOIN M_TODOUFUKEN MTO ");
            sb.Append(" ON MG.GENBA_TODOUFUKEN_CD = MTO.TODOUFUKEN_CD ");

            // 取引先マスタ
            sb.Append(" LEFT JOIN M_TORIHIKISAKI MT ");
            sb.Append(" ON MG.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD ");

            // 現場マスタ(外部連携用)
            sb.Append(" LEFT JOIN M_GENBA_DIGI MGD ");
            sb.Append(" ON MG.GYOUSHA_CD = MGD.GYOUSHA_CD ");
            sb.Append(" AND MG.GENBA_CD = MGD.GENBA_CD ");

            // 現場タイムスタンプテーブル
            sb.Append(" LEFT JOIN M_DIGI_OUTPUT_GENBA MDOG ");
            sb.Append(" ON MGD.GYOUSHA_CD = MDOG.GYOUSHA_CD ");
            sb.Append(" AND MGD.GENBA_CD = MDOG.GENBA_CD ");

            // 業者マスタ
            sb.Append(" LEFT JOIN M_GYOUSHA MGYO ");
            sb.Append(" ON MG.GYOUSHA_CD = MGYO.GYOUSHA_CD ");

            // 都道府県マスタ(外部連携現場)
            sb.Append(" LEFT JOIN M_TODOUFUKEN MGTO ");
            sb.Append(" ON MGD.GENBA_TODOUFUKEN_CD = MGTO.TODOUFUKEN_CD ");

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
            var result = new StringBuilder();
            string strTemp;

            result.Append(" 0 = 0 ");

            // 業者CD
            strTemp = this.form.GYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GYOUSHA_CD = '{0}'", strTemp);
            }
            // 現場CD
            strTemp = this.form.GENBA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" MG.GENBA_CD = '{0}'", strTemp);
            }

            return result.Length > 0 ? result.Insert(0, " WHERE ").ToString() : string.Empty;
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

        #endregion

        #region 表示条件

        /// <summary>
        /// 表示条件を次回呼出時のデフォルト値として保存します
        /// </summary>
        internal bool SaveHyoujiJoukenDefault()
        {
            LogUtility.DebugMethodStart();

            try
            {
                SetchiContenaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                SetchiContenaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                SetchiContenaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_RNAME.Text;
                SetchiContenaIchiran.Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                SetchiContenaIchiran.Properties.Settings.Default.Save();
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveHyoujiJoukenDefault", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion

        #endregion

        #region 現場のチェック
        internal bool CheckGenba()
        {
            try
            {
                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.daoGenba.GetAllValidData(entity);
                if (genba != null && genba.Length > 0)
                {
                    this.form.GENBA_RNAME.Text = genba[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.GENBA_RNAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }
        #endregion

        #region SubF1 地図表示

        /// <summary>
        /// 地図表示
        /// </summary>
        /// <returns></returns>
        internal int JsonConnection(HTTP_METHOD ReqMethod)
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                // 事前に必要情報を取得
                var sysInfo = this.daoSysInfo.GetAllDataForCode("0");

                var logic = new LogiLogic();

                //DELETEで使用するURL用変数初期化
                var apiURL = string.Empty;

                //リクエスト関連情報取得
                var genbaConnect = this.daoSysInfo.GetDateForStringSql("");
                if (genbaConnect == null)
                {
                    this.msgLogic.MessageBoxShowError("S_LOGI_CONNECTのPoints情報なし！！");
                    ret_cnt = -1;
                    return ret_cnt;
                }

                //DGVに表示されているデータ
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    //DVG内最初のカラム(連携)がONになっているものが対象
                    if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                    {

                        if (ReqMethod == HTTP_METHOD.PUT)
                        {
                            //現場PUT
                            var genbaDto = new ExternalCommon.DTO.Logicompass.INFO_POINT();
                            genbaDto.Company_Id = sysInfo.DIGI_CORP_ID;
                            genbaDto.Point_Id = dgvRow.Cells["POINT_ID_HIDDEN"].Value.ToString();
                            genbaDto.Point_Name = dgvRow.Cells["POINT_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Point_Kana_Name = dgvRow.Cells["POINT_KANA_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Map_Name = dgvRow.Cells["MAP_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Post_Code = dgvRow.Cells["POST_CODE_HIDDEN"].Value.ToString();
                            genbaDto.Prefectures = dgvRow.Cells["PREFECTURES_HIDDEN"].Value.ToString();
                            genbaDto.Address1 = dgvRow.Cells["ADDRESS1_HIDDEN"].Value.ToString() + dgvRow.Cells["ADDRESS2_HIDDEN"].Value.ToString();
                            genbaDto.Tel_No = dgvRow.Cells["TEL_NO_HIDDEN"].Value.ToString();
                            genbaDto.Fax_No = dgvRow.Cells["FAX_NO_HIDDEN"].Value.ToString();
                            genbaDto.Contact_Name = dgvRow.Cells["CONTACT_NAME_HIDDEN"].Value.ToString();
                            genbaDto.Mail_Address = dgvRow.Cells["MAIL_ADDRESS_HIDDEN"].Value.ToString();
                            genbaDto.Range_Radius = int.Parse(dgvRow.Cells["RANGE_RADIUS_HIDDEN"].Value.ToString());
                            genbaDto.Remarks = dgvRow.Cells["REMARKS_HIDDEN"].Value.ToString();

                        }

                        //完了
                        ret_cnt++;
                    }
                }
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                //エラーメッセージは各々出してるのでここでは表示しない
                LogUtility.Error("JsonConnection", ex);
                ret_cnt = -1;
            }

            LogUtility.DebugMethodEnd();

            return ret_cnt;

        }

        #endregion

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        internal void OutputCSV()
        {
            // 標準のCSV出力を持ってくる
        }

        #endregion

        #region 未使用

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
