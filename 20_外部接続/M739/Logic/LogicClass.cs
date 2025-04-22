using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.GeoCodingAPI;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.MapRenkei.Const;
using Shougun.Core.ExternalConnection.MapRenkei.DAO;

namespace Shougun.Core.ExternalConnection.MapRenkei.Logic
{
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// フォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// メッセージロジック
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// DAO
        /// </summary>
        private DAOClass dao;

        private IM_TORIHIKISAKIDao daoTorihikisaki;
        private IM_GYOUSHADao daoGyousha;
        private IM_GENBADao daoGenba;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm callForm)
        {
            LogUtility.DebugMethodStart(callForm);

            this.form = callForm;

            this.msgLogic = new MessageBoxShowLogic();

            // 各DAO初期化
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region メソッド

        #region 初期化

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;

            try
            {
                this.parentForm = this.form.Parent as BusinessBaseForm;

                // ボタン初期化
                this.ButtonInit();

                // イベント初期化
                this.EventInit();

                // 初期値
                this.form.ExportCheckbox2.Checked = false;
                this.form.ExportCheckbox2.Checked = true;
                this.form.InportCheckBox1.Checked = true;
                this.form.InportCheckBox2.Checked = true;

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");

                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタン初期化
        /// </summary>
        private void ButtonInit()
        {
            ButtonControlUtility.SetButtonInfo(
                new ButtonSetting().LoadButtonSetting(Assembly.GetExecutingAssembly(), ConstClass.ButtonInfoXmlPath),
                this.parentForm, this.form.WindowType);
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            // ファンクション
            this.parentForm.bt_func12.Click += this.form.bt_func12_Click;

            // 参照ボタン
            this.form.btnInportFileRef.Click += this.form.InportFileRefClick;

            // 出力
            this.form.btnExport.Click += this.form.ExportClick;
            this.form.btnInport.Click += this.form.InportClick;
        }

        #endregion

        #region ファンクション

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        /// <remarks>F12実処理</remarks>
        internal void FormClose()
        {
            this.form.Close();
            this.parentForm.Close();
        }

        #endregion

        #region 参照ボタン

        /// <summary>
        /// 参照ボタン押下処理(取込)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool InportFileRefClick()
        {
            try
            {

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "開くファイルを選択してください";
                //var initialPath = @"C:\Temp";
                var initialPath = this.form.INPORT_FILE_PATH.Text;
                var windowHandle = this.form.Handle;
                var isFileSelect = true;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.INPORT_FILE_PATH.Text = filePath;
                    string ext = Path.GetExtension(this.form.INPORT_FILE_PATH.Text);
                    if (!(ext.ToLower().Equals(".csv")))
                    {
                        this.msgLogic.MessageBoxShowWarn("CSV形式のファイルを選択してください。");
                    }
                }
              
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 出力

        /// <summary>
        /// 出力処理
        /// </summary>
        /// <returns></returns>
        internal bool ExportClick()
        {
            bool ret = false;

            try
            {
                DialogResult dr = this.msgLogic.MessageBoxShowConfirm("CSV出力しますか？");
                if (dr == DialogResult.No)
                {
                    return ret;
                }

                // データの抽出
                bool search = this.ExportDataSearch();
                if (!search)
                {
                    return ret;
                }

                // ファイル出力
                bool export = this.ExportFile();
                if (export)
                {
                    //this.msgLogic.MessageBoxShowInformation("出力が完了しました。");
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExportClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            return ret;
        }

        /// <summary>
        /// 出力するデータの抽出
        /// </summary>
        /// <returns></returns>
        private bool ExportDataSearch()
        {
            bool ret = false;

            try
            {
                // DGVの初期化
                this.form.customDataGridView1.Rows.Clear();

                // ファイル出力に必要な情報を収集
                string sql = string.Empty;
                StringBuilder sb = new StringBuilder();

                #region sql

                sb.AppendFormat("SELECT * FROM (");
                // 取引先
                sb.AppendFormat(" SELECT ");
                sb.AppendFormat(" 1 AS {0}, ", ConstClass.CSV_COLUMN_MASTER_KBN);
                sb.AppendFormat(" MT.TORIHIKISAKI_CD AS {0}, ", ConstClass.CSV_COLUMN_CD1);
                sb.AppendFormat(" '' AS {0}, ", ConstClass.CSV_COLUMN_NAME1);
                sb.AppendFormat(" '' AS {0}, ", ConstClass.CSV_COLUMN_CD2);
                sb.AppendFormat(" MT.TORIHIKISAKI_NAME1 AS {0}, ", ConstClass.CSV_COLUMN_NAME2);
                sb.AppendFormat(" MT.TORIHIKISAKI_NAME2 AS NAME3, ");
                sb.AppendFormat(" MT.TORIHIKISAKI_NAME_RYAKU AS {0}, ", ConstClass.CSV_COLUMN_NAME_RYAKU);
                sb.AppendFormat(" TOD.TODOUFUKEN_NAME + MT.TORIHIKISAKI_ADDRESS1 + MT.TORIHIKISAKI_ADDRESS2 AS {0}, ", ConstClass.CSV_COLUMN_ADDRESS);
                sb.AppendFormat(" MT.TORIHIKISAKI_LATITUDE AS {0}, ", ConstClass.CSV_COLUMN_LATITUDE);
                sb.AppendFormat(" MT.TORIHIKISAKI_LONGITUDE AS {0}, ", ConstClass.CSV_COLUMN_LONGITUDE);
                sb.AppendFormat(" MT.TORIHIKISAKI_LOCATION_INFO_UPDATE_NAME AS {0}, ", ConstClass.CSV_COLUMN_UPDATE_USER);
                sb.AppendFormat(" MT.TORIHIKISAKI_LOCATION_INFO_UPDATE_DATE AS {0}, ", ConstClass.CSV_COLUMN_UPDATE_DATE);
                sb.AppendFormat(" MT.TORIHIKISAKI_POST AS POST,");
                sb.AppendFormat(" MT.TORIHIKISAKI_TEL AS TEL,");
                sb.AppendFormat(" MT.BIKOU1,");
                sb.AppendFormat(" MT.BIKOU2,");
                sb.AppendFormat(" MT.DELETE_FLG");
                sb.AppendFormat(" FROM M_TORIHIKISAKI AS MT");
                sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN AS TOD ON MT.TORIHIKISAKI_TODOUFUKEN_CD = TOD.TODOUFUKEN_CD");

                sb.AppendFormat(" UNION");
                // 業者
                sb.AppendFormat(" SELECT");
                sb.AppendFormat(" 2 AS {0}, ", ConstClass.CSV_COLUMN_MASTER_KBN);
                sb.AppendFormat(" MT.GYOUSHA_CD AS {0}, ", ConstClass.CSV_COLUMN_CD1);
                sb.AppendFormat(" '' AS {0}, ", ConstClass.CSV_COLUMN_NAME1);
                sb.AppendFormat(" '' AS {0}, ", ConstClass.CSV_COLUMN_CD2);
                sb.AppendFormat(" MT.GYOUSHA_NAME1 AS {0}, ", ConstClass.CSV_COLUMN_NAME2);
                sb.AppendFormat(" MT.GYOUSHA_NAME2 AS NAME3, ");
                sb.AppendFormat(" MT.GYOUSHA_NAME_RYAKU AS {0}, ", ConstClass.CSV_COLUMN_NAME_RYAKU);
                sb.AppendFormat(" TOD.TODOUFUKEN_NAME + MT.GYOUSHA_ADDRESS1 + MT.GYOUSHA_ADDRESS2 AS {0}, ", ConstClass.CSV_COLUMN_ADDRESS);
                sb.AppendFormat(" MT.GYOUSHA_LATITUDE AS {0}, ", ConstClass.CSV_COLUMN_LATITUDE);
                sb.AppendFormat(" MT.GYOUSHA_LONGITUDE AS {0}, ", ConstClass.CSV_COLUMN_LONGITUDE);
                sb.AppendFormat(" MT.GYOUSHA_LOCATION_INFO_UPDATE_NAME AS {0}, ", ConstClass.CSV_COLUMN_UPDATE_USER);
                sb.AppendFormat(" MT.GYOUSHA_LOCATION_INFO_UPDATE_DATE AS {0}, ", ConstClass.CSV_COLUMN_UPDATE_DATE);
                sb.AppendFormat(" MT.GYOUSHA_POST AS POST,");
                sb.AppendFormat(" MT.GYOUSHA_TEL AS TEL,");
                sb.AppendFormat(" MT.BIKOU1,");
                sb.AppendFormat(" MT.BIKOU2,");
                sb.AppendFormat(" MT.DELETE_FLG");
                sb.AppendFormat(" FROM M_GYOUSHA AS MT");
                sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN AS TOD ON MT.GYOUSHA_TODOUFUKEN_CD = TOD.TODOUFUKEN_CD");

                sb.AppendFormat(" UNION");

                // 現場
                sb.AppendFormat(" SELECT");
                sb.AppendFormat(" 3 AS {0}, ", ConstClass.CSV_COLUMN_MASTER_KBN);
                sb.AppendFormat(" MT.GYOUSHA_CD AS {0}, ", ConstClass.CSV_COLUMN_CD1);
                sb.AppendFormat(" GYO.GYOUSHA_NAME_RYAKU AS {0}, ", ConstClass.CSV_COLUMN_NAME1);
                sb.AppendFormat(" MT.GENBA_CD AS {0}, ", ConstClass.CSV_COLUMN_CD2);
                sb.AppendFormat(" MT.GENBA_NAME1 AS {0}, ", ConstClass.CSV_COLUMN_NAME2);
                sb.AppendFormat(" MT.GENBA_NAME2 AS NAME3, ");
                sb.AppendFormat(" MT.GENBA_NAME_RYAKU AS {0}, ", ConstClass.CSV_COLUMN_NAME_RYAKU);
                sb.AppendFormat(" TOD.TODOUFUKEN_NAME + MT.GENBA_ADDRESS1 + MT.GENBA_ADDRESS2 AS {0}, ", ConstClass.CSV_COLUMN_ADDRESS);
                sb.AppendFormat(" MT.GENBA_LATITUDE AS {0}, ", ConstClass.CSV_COLUMN_LATITUDE);
                sb.AppendFormat(" MT.GENBA_LONGITUDE AS {0}, ", ConstClass.CSV_COLUMN_LONGITUDE);
                sb.AppendFormat(" MT.GENBA_LOCATION_INFO_UPDATE_NAME AS {0}, ", ConstClass.CSV_COLUMN_UPDATE_USER);
                sb.AppendFormat(" MT.GENBA_LOCATION_INFO_UPDATE_DATE AS {0}, ", ConstClass.CSV_COLUMN_UPDATE_DATE);
                sb.AppendFormat(" MT.GENBA_POST AS POST,");
                sb.AppendFormat(" MT.GENBA_TEL AS TEL,");
                sb.AppendFormat(" MT.BIKOU1,");
                sb.AppendFormat(" MT.BIKOU2,");
                sb.AppendFormat(" MT.DELETE_FLG");
                sb.AppendFormat(" FROM M_GENBA AS MT");
                sb.AppendFormat(" INNER JOIN M_GYOUSHA AS GYO ON MT.GYOUSHA_CD = GYO.GYOUSHA_CD");
                sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN AS TOD ON MT.GENBA_TODOUFUKEN_CD = TOD.TODOUFUKEN_CD");

                sb.AppendFormat(" ) AS CSV_DATA");

                sb.AppendFormat(" WHERE 1=1");

                if (!this.form.ExportCheckbox1.Checked)
                {
                    // 削除区分が立っていないデータのみ
                    sb.AppendFormat(" AND DELETE_FLG = 0");
                }

                if (this.form.ExportCheckbox2.Checked)
                {
                    // 緯度経度が未入力のもののみ対象
                    sb.AppendFormat(" AND ({0} IS NULL OR {0} = '')", ConstClass.CSV_COLUMN_LATITUDE);
                    sb.AppendFormat(" AND ({0} IS NULL OR {0} = '')", ConstClass.CSV_COLUMN_LONGITUDE);
                }

                #endregion

                // DGVにセット
                DataTable dt = this.dao.GetCsvOutputData(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.form.customDataGridView1.Rows.Add(dt.Rows.Count);
                    for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.form.customDataGridView1.Rows[i];
                        DataRow dr = dt.Rows[i];

                        // 社員
                        row.Cells[ConstClass.CSV_COLUMN_MASTER_KBN].Value = dr[ConstClass.CSV_COLUMN_MASTER_KBN];
                        row.Cells[ConstClass.CSV_COLUMN_CD1].Value = dr[ConstClass.CSV_COLUMN_CD1];
                        row.Cells[ConstClass.CSV_COLUMN_NAME1].Value = dr[ConstClass.CSV_COLUMN_NAME1];
                        row.Cells[ConstClass.CSV_COLUMN_CD2].Value = dr[ConstClass.CSV_COLUMN_CD2];
                        row.Cells[ConstClass.CSV_COLUMN_NAME2].Value = dr[ConstClass.CSV_COLUMN_NAME_RYAKU];
                        row.Cells[ConstClass.CSV_COLUMN_ADDRESS].Value = dr[ConstClass.CSV_COLUMN_ADDRESS];

                        if (string.IsNullOrEmpty(Convert.ToString(dr[ConstClass.CSV_COLUMN_LATITUDE])))
                        {
                            row.Cells[ConstClass.CSV_COLUMN_LATITUDE].Value = string.Empty;
                            row.Cells[ConstClass.CSV_COLUMN_LONGITUDE].Value = string.Empty;
                        }
                        else
                        {
                            row.Cells[ConstClass.CSV_COLUMN_LATITUDE].Value = dr[ConstClass.CSV_COLUMN_LATITUDE];
                            row.Cells[ConstClass.CSV_COLUMN_LONGITUDE].Value = dr[ConstClass.CSV_COLUMN_LONGITUDE];
                        }
                    }
                }

                ret = true;

                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExportDataSearch", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return ret;
            }
        }

        /// <summary>
        /// ファイルの出力処理
        /// </summary>
        /// <returns></returns>
        private bool ExportFile()
        {
            bool ret = false;

            try
            {
                CSVExport csvLogic = new CSVExport();
                string title = "map_master";
                ret = csvLogic.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, title, this.form);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExportFile", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            return ret;
        }

        #endregion

        #region 取込

        /// <summary>
        /// 取込処理
        /// </summary>
        /// <returns></returns>
        internal bool InportClick()
        {
            bool ret = false;

            try
            {
                // ファイルパスのチェック
                if (string.IsNullOrEmpty(this.form.INPORT_FILE_PATH.Text))
                {
                    msgLogic.MessageBoxShow("E001", "取込元ファイル");
                    return ret;
                }

                // ファイル拡張子のチェック
                string ext = Path.GetExtension(this.form.INPORT_FILE_PATH.Text);
                if (!(ext.ToLower().Equals(".csv")))
                {
                    this.msgLogic.MessageBoxShowWarn("CSV形式のファイルを選択してください。");
                    return ret;
                }

                DialogResult dr = this.msgLogic.MessageBoxShowConfirm("CSV取込を実行しますか？");
                if (dr == DialogResult.No)
                {
                    return ret;
                }

                this.form.INPORT_LOG.Text = string.Empty;

                // ENTITYのリスト
                List<M_TORIHIKISAKI> TorihikisakiEntityList = new List<M_TORIHIKISAKI>();
                List<M_GYOUSHA> GyoushaEntiryList = new List<M_GYOUSHA>();
                List<M_GENBA> GenbaEntityList = new List<M_GENBA>();

                // 登録に使用するシステム日付を取得
                SqlDateTime datetime = this.sysDate();

                using (var parser = new TextFieldParser(this.form.INPORT_FILE_PATH.Text, Encoding.GetEncoding("UTF-8")))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.Delimiters = new string[] { "," };

                    bool firstLine = true;

                    int i = 0;
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sbErr = new StringBuilder();

                    sb.AppendFormat("実行日時：{0}", Convert.ToString(datetime)).AppendLine();
                    sb.AppendFormat("取込ファイル名：{0}", this.form.INPORT_FILE_PATH.Text).AppendLine();

                    sb.AppendFormat("");

                    while (!parser.EndOfData)
                    {
                        i++;
                        var fields = parser.ReadFields();

                        // 1行目は飛ばす
                        if (firstLine)
                        {
                            firstLine = false;
                            continue;
                        }

                        // 緯度経度どちらかが未入力の行は取り込まない
                        if ((string.IsNullOrEmpty(Convert.ToString(fields[6])) &&
                            !string.IsNullOrEmpty(Convert.ToString(fields[7]))) ||
                            (string.IsNullOrEmpty(Convert.ToString(fields[7])) &&
                            !string.IsNullOrEmpty(Convert.ToString(fields[6]))))
                        {
                            continue;
                        }

                        switch (Convert.ToString(fields[0]))
                        {
                            case "1": // 取引先
                                M_TORIHIKISAKI torihikisakiEntity = this.daoTorihikisaki.GetDataByCd(Convert.ToString(fields[1]));
                                if (torihikisakiEntity == null)
                                {
                                    sbErr.AppendFormat("{0}行目：CDに該当するマスタが存在しません。", i).AppendLine();
                                    break;
                                }
                                if (!this.form.InportCheckBox2.Checked)
                                {
                                    if (!string.IsNullOrEmpty(torihikisakiEntity.TORIHIKISAKI_LATITUDE))
                                    {
                                        // 入力済みの場合は上書きする：falseなら抜ける
                                        break;
                                    }
                                }
                                torihikisakiEntity.TORIHIKISAKI_LATITUDE = Convert.ToString(fields[6]);
                                torihikisakiEntity.TORIHIKISAKI_LONGITUDE = Convert.ToString(fields[7]);
                                if (string.IsNullOrEmpty(Convert.ToString(fields[6])))
                                {
                                    if (this.form.InportCheckBox1.Checked)
                                    {
                                        // 緯度経度未入力は取り込まない：trueなら抜ける
                                        break;
                                    }
                                }
                                else
                                {
                                    torihikisakiEntity.TORIHIKISAKI_LOCATION_INFO_UPDATE_NAME = "メンテナンスツール";
                                    torihikisakiEntity.TORIHIKISAKI_LOCATION_INFO_UPDATE_DATE = datetime;
                                }
                                TorihikisakiEntityList.Add(torihikisakiEntity);
                                break;

                            case "2": // 業者
                                M_GYOUSHA gyoushaEntity = this.daoGyousha.GetDataByCd(Convert.ToString(fields[1]));
                                if (gyoushaEntity == null)
                                {
                                    sbErr.AppendFormat("{0}行目：CDに該当するマスタが存在しません。", i).AppendLine();
                                    break;
                                }
                                if (!this.form.InportCheckBox2.Checked)
                                {
                                    if (!string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_LATITUDE))
                                    {
                                        // 入力済みの場合は上書きする：ONなら抜ける
                                        break;
                                    }
                                }
                                gyoushaEntity.GYOUSHA_LATITUDE = Convert.ToString(fields[6]);
                                gyoushaEntity.GYOUSHA_LONGITUDE = Convert.ToString(fields[7]);
                                if (string.IsNullOrEmpty(Convert.ToString(fields[6])))
                                {
                                    if (this.form.InportCheckBox1.Checked)
                                    {
                                        // 緯度経度未入力は取り込まない：ONなら抜ける
                                        // いるか？強制抜けでいい気がする
                                        break;
                                    }
                                }
                                else
                                {
                                    gyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_NAME = "メンテナンスツール";
                                    gyoushaEntity.GYOUSHA_LOCATION_INFO_UPDATE_DATE = datetime;
                                }
                                GyoushaEntiryList.Add(gyoushaEntity);
                                break;

                            case "3": // 現場
                                M_GENBA genbaSearchString = new M_GENBA();
                                genbaSearchString.GYOUSHA_CD = Convert.ToString(fields[1]);
                                genbaSearchString.GENBA_CD = Convert.ToString(fields[3]);
                                M_GENBA genbaEntity = this.daoGenba.GetDataByCd(genbaSearchString);
                                if (genbaEntity == null)
                                {
                                    sbErr.AppendFormat("{0}行目：CDに該当するマスタが存在しません。", i).AppendLine();
                                    break;
                                }
                                if (!this.form.InportCheckBox2.Checked)
                                {
                                    if (!string.IsNullOrEmpty(genbaEntity.GENBA_LATITUDE))
                                    {
                                        // 入力済みの場合は上書きする：ONなら抜ける
                                        break;
                                    }
                                }
                                genbaEntity.GENBA_LATITUDE = Convert.ToString(fields[6]);
                                genbaEntity.GENBA_LONGITUDE = Convert.ToString(fields[7]);
                                if (string.IsNullOrEmpty(Convert.ToString(fields[6])))
                                {
                                    if (this.form.InportCheckBox1.Checked)
                                    {
                                        // 緯度経度未入力は取り込まない：ONなら抜ける
                                        // いるか？強制抜けでいい気がする
                                        break;
                                    }
                                }
                                else
                                {
                                    genbaEntity.GENBA_LOCATION_INFO_UPDATE_NAME = "メンテナンスツール";
                                    genbaEntity.GENBA_LOCATION_INFO_UPDATE_DATE = datetime;
                                }
                                GenbaEntityList.Add(genbaEntity);
                                break;

                            default:
                                break;
                        }
                    }
                    if (string.IsNullOrEmpty(sbErr.ToString()))
                    {
                        sbErr.Insert(0, "エラー内容：無し").AppendLine();
                    }
                    else
                    {
                        sbErr.Insert(0, "エラー内容：").AppendLine();
                    }
                    this.form.INPORT_LOG.Text = sb.ToString() + sbErr.ToString();

                }

                // 登録処理
                using (Transaction tran = new Transaction())
                {
                    // 取引先
                    foreach (M_TORIHIKISAKI entity in TorihikisakiEntityList)
                    {
                        var dataBindert = new DataBinderLogic<M_TORIHIKISAKI>(entity);
                        dataBindert.SetSystemProperty(entity, false);
                        this.daoTorihikisaki.Update(entity);
                    }

                    // 業者
                    foreach (M_GYOUSHA entity in GyoushaEntiryList)
                    {
                        var dataBindert = new DataBinderLogic<M_GYOUSHA>(entity);
                        dataBindert.SetSystemProperty(entity, false);
                        this.daoGyousha.Update(entity);
                    }

                    // 現場
                    foreach (M_GENBA entity in GenbaEntityList)
                    {
                        var dataBindert = new DataBinderLogic<M_GENBA>(entity);
                        dataBindert.SetSystemProperty(entity, false);
                        this.daoGenba.Update(entity);
                    }

                    tran.Commit();
                }
                this.msgLogic.MessageBoxShowInformation("取込が完了しました。");

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InportClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            return ret;
        }

        #endregion

        #region システム日付の取得

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SqlDateTime sysDate()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return SqlDateTime.Parse(now.ToString());
        }

        #endregion

        #endregion

        #region 未使用

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}