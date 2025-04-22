using System;
using System.Reflection;
using DenshiKeiyakuHimodzukeHojo.App;
using r_framework.APP.PopUp.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DenshiKeiyakuHimodzukeHojo.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass
    {
        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "DenshiKeiyakuHimodzukeHojo.Setting.ButtonSetting.xml";

        /// <summary>
        /// メッセージ
        /// </summary>
        public static readonly string MsgA = "契約日で紐付を行う場合、　電子契約（紐付条件）/　環境将軍R（紐付条件）のどちらも契約日を選択してください。";

        /// <summary>
        /// 明細行が1件以上
        /// </summary>
        public static readonly string ITAKU_KEIYAKU_FUKUSUU = "該当委託契約書が複数存在";

        /// <summary>
        /// 明細行＝0件
        /// </summary>
        public static readonly string ITAKU_KEIYAKU_0 = "紐付の対象委託契約書無し";

        /// <summary>
        /// 電子契約（紐付条件）
        /// </summary>
        private Dictionary<string, string> listWanSign = new Dictionary<string, string>()
        {
                {"PARTNER_ORGANIZE_NM", "会社名（相手先）"},
                {"DOCUMENT_NAME", "文書名"},
                {"CONTROL_NUMBER", "関連コード"},
                {"CONTRACT_DATE", "契約日"},
                {"CONTRACT_DECIMAL", "契約金額"},
                {"STORAGE_LOCATION", "保管場所"},
                {"COMMENT_1", "備考1"},
                {"COMMENT_2", "備考2"},
                {"COMMENT_3", "備考3"},
                {"FIELD_1", "フィールド1"},
                {"FIELD_2", "フィールド2"},
                {"FIELD_3", "フィールド3"},
                {"FIELD_4", "フィールド4"},
                {"FIELD_5", "フィールド5"}
        };

        /// <summary>
        /// 電子契約（紐付条件）
        /// </summary>
        internal DataTable wanSignDataTable;

        /// <summary>
        /// 環境将軍Ｒ（紐付条件）
        /// </summary>
        private Dictionary<string, string> listShougunR = new Dictionary<string, string>()
        {
                {"GYOUSHA_NAME1", "排出事業者名"},
                {"GYOUSHA_NAME_RYAKU", "排出事業者名（略称）"},
                {"GYOUSHA_ADDRESS1", "排出事業者住所"},
                {"GYOUSHA_TEL", "排出事業者電話番号"},
                {"GYOUSHA_KEITAI_TEL", "排出事業者携帯番号"},
                {"GENBA_NAME1", "排出事業場名"},
                {"GENBA_NAME_RYAKU", "排出事業場名（略称）"},
                {"GENBA_ADDRESS1", "排出事業場住所"},
                {"GENBA_TEL", "排出事業場電話番号"},
                {"GENBA_KEITAI_TEL", "排出事業場携帯番号"},
                {"KEIYAKUSHO_KEIYAKU_DATE", "契約日（委託契約書）"},
                {"BIKOU1", "備考1（委託契約書）"},
                {"BIKOU2", "備考2（委託契約書）"},
                {"ITAKU_KEIYAKU_FILE_PATH", "委託契約書ファイルパス（委託契約書）"}
        };

        /// <summary>
        /// 環境将軍Ｒ（紐付条件）
        /// </summary>
        internal DataTable shougunRDataTable;

        /// <summary>
        /// システム設定
        /// </summary>
        internal M_SYS_INFO sysInfo;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 
        /// </summary>
        internal MessageBoxShowLogic errmessage;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData().FirstOrDefault();
            this.errmessage = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit(out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.LoadKoumokuMei();

                this.form.KENSAKU_HOUHOU.Text = "1";

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();

            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
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

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BasePopForm)this.form.Parent;

            //自動紐付ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            this.form.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 項目名
        /// </summary>
        private void LoadKoumokuMei()
        {
            #region 電子契約（紐付条件）
            var wanSignTable = new DataTable();
            wanSignTable.Columns.Add("WANSIGN_KOUMOKU_NAME");
            wanSignTable.Columns.Add("WANSIGN_KOUMOKU_CD");

            foreach (var item in this.listWanSign)
            {
                var row = wanSignTable.NewRow();
                switch (item.Value)
                {
                    case "備考1":
                        row["WANSIGN_KOUMOKU_NAME"] = string.IsNullOrEmpty(this.sysInfo.WAN_SIGN_BIKOU_1) ? item.Value : item.Value + "（" + this.sysInfo.WAN_SIGN_BIKOU_1 + "）";
                        break;
                    case "備考2":
                        row["WANSIGN_KOUMOKU_NAME"] = string.IsNullOrEmpty(this.sysInfo.WAN_SIGN_BIKOU_2) ? item.Value : item.Value + "（" + this.sysInfo.WAN_SIGN_BIKOU_2 + "）";
                        break;
                    case "備考3":
                        row["WANSIGN_KOUMOKU_NAME"] = string.IsNullOrEmpty(this.sysInfo.WAN_SIGN_BIKOU_3) ? item.Value : item.Value + "（" + this.sysInfo.WAN_SIGN_BIKOU_3 + "）";
                        break;
                    case "フィールド1":
                        row["WANSIGN_KOUMOKU_NAME"] = this.sysInfo.WAN_SIGN_FIELD_1 == 1 ? item.Value + "（" + this.sysInfo.WAN_SIGN_FIELD_NAME_1 + "）" : item.Value + "（使用不可）";
                        break;
                    case "フィールド2":
                        row["WANSIGN_KOUMOKU_NAME"] = this.sysInfo.WAN_SIGN_FIELD_2 == 1 ? item.Value + "（" + this.sysInfo.WAN_SIGN_FIELD_NAME_2 + "）" : item.Value + "（使用不可）";
                        break;
                    case "フィールド3":
                        row["WANSIGN_KOUMOKU_NAME"] = this.sysInfo.WAN_SIGN_FIELD_3 == 1 ? item.Value + "（" + this.sysInfo.WAN_SIGN_FIELD_NAME_3 + "）" : item.Value + "（使用不可）";
                        break;
                    case "フィールド4":
                        row["WANSIGN_KOUMOKU_NAME"] = this.sysInfo.WAN_SIGN_FIELD_4 == 1 ? item.Value + "（" + this.sysInfo.WAN_SIGN_FIELD_NAME_4 + "）" : item.Value + "（使用不可）";
                        break;
                    case "フィールド5":
                        row["WANSIGN_KOUMOKU_NAME"] = this.sysInfo.WAN_SIGN_FIELD_5 == 1 ? item.Value + "（" + this.sysInfo.WAN_SIGN_FIELD_NAME_5 + "）" : item.Value + "（使用不可）";
                        break;
                    default:
                        row["WANSIGN_KOUMOKU_NAME"] = item.Value;
                        break;

                }

                row["WANSIGN_KOUMOKU_CD"] = item.Key;
                wanSignTable.Rows.Add(row);
            }

            this.form.dgvWanSign.IsBrowsePurpose = true;
            this.form.dgvWanSign.DataSource = wanSignTable;
            #endregion

            #region 環境将軍Ｒ（紐付条件）
            var shougunTable = new DataTable();
            shougunTable.Columns.Add("R_KOUMOKU_NAME");
            shougunTable.Columns.Add("R_KOUMOKU_CD");

            foreach (var item in this.listShougunR)
            {
                var row = shougunTable.NewRow();
                row["R_KOUMOKU_NAME"] = item.Value;
                row["R_KOUMOKU_CD"] = item.Key;
                shougunTable.Rows.Add(row);
            }

            this.form.dgvShougun.IsBrowsePurpose = true;
            this.form.dgvShougun.DataSource = shougunTable;
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool Click_ButtonF9()
        {
            if (this.form.InOutSysId == null)
            {
                return false;
            }

            var dic = new Dictionary<string, List<string>>();

            //ドキュメントID
            var documnetId = "'" + string.Join("','", this.form.InOutSysId.Keys.ToList()) + "'";

            //電子契約（紐付条件）
            var sqlWanSign = @" SELECT DISTINCT
                                     A.DOCUMENT_ID 
                                    ,A.PARTNER_ORGANIZE_NM
                                    ,A.PARTNER_ORGANIZE_NM2
                                    ,A.PARTNER_ORGANIZE_NM3
                                    ,A.PARTNER_ORGANIZE_NM4
                                    ,A.PARTNER_ORGANIZE_NM5
                                    ,A.PARTNER_ORGANIZE_NM6
                                    ,A.PARTNER_ORGANIZE_NM7
                                    ,A.PARTNER_ORGANIZE_NM8
                                    ,A.PARTNER_ORGANIZE_NM9
                                    ,A.PARTNER_ORGANIZE_NM10
                                    ,A.PARTNER_ORGANIZE_NM11
                                    ,A.PARTNER_ORGANIZE_NM12
                                    ,A.PARTNER_ORGANIZE_NM13
                                    ,A.PARTNER_ORGANIZE_NM14
                                    ,A.PARTNER_ORGANIZE_NM15
                                    ,A.PARTNER_ORGANIZE_NM16
                                    ,A.DOCUMENT_NAME
                                    ,A.CONTROL_NUMBER
                                    ,A.CONTRACT_DATE
                                    ,A.CONTRACT_DECIMAL
                                    ,A.STORAGE_LOCATION
                                    ,A.COMMENT_1
                                    ,A.COMMENT_2
                                    ,A.COMMENT_3
                                    ,A.FIELD_1
                                    ,A.FIELD_2
                                    ,A.FIELD_3
                                    ,A.FIELD_4
                                    ,A.FIELD_5
                               FROM M_WANSIGN_KEIYAKU_INFO A
                               LEFT JOIN M_ITAKU_LINK_WANSIGN_KEIYAKU C ON A.WANSIGN_SYSTEM_ID = C.WANSIGN_SYSTEM_ID";

            //環境将軍Ｒ（紐付条件）
            var sqlShougunR = @" SELECT DISTINCT
                                     A.SYSTEM_ID
                                    ,B.GYOUSHA_NAME1
                                    ,B.GYOUSHA_NAME_RYAKU
                                    ,B.GYOUSHA_ADDRESS1
                                    ,B.GYOUSHA_TEL
                                    ,B.GYOUSHA_KEITAI_TEL
                                    ,C.GENBA_NAME1
                                    ,C.GENBA_NAME_RYAKU
                                    ,C.GENBA_ADDRESS1
                                    ,C.GENBA_TEL
                                    ,C.GENBA_KEITAI_TEL
                                    ,A.KEIYAKUSHO_KEIYAKU_DATE
                                    ,A.BIKOU1
                                    ,A.BIKOU2
                                    ,A.ITAKU_KEIYAKU_FILE_PATH
                               FROM M_ITAKU_KEIYAKU_KIHON A
                               LEFT JOIN M_ITAKU_KEIYAKU_KIHON_HST_GENBA D ON A.SYSTEM_ID = D.SYSTEM_ID
                               LEFT JOIN M_GYOUSHA B ON A.HAISHUTSU_JIGYOUSHA_CD = B.GYOUSHA_CD
                               LEFT JOIN M_GENBA C ON D.HAISHUTSU_JIGYOUSHA_CD = C.GYOUSHA_CD AND D.HAISHUTSU_JIGYOUJOU_CD = C.GENBA_CD
                              WHERE A.DELETE_FLG = 0 ";

            var wanSignCurrentCell = this.form.dgvWanSign.CurrentCell;
            var shougunRCurrentCell = this.form.dgvShougun.CurrentCell;

            if (wanSignCurrentCell == null ||
                shougunRCurrentCell == null)
            {
                return true;
            }

            //電子契約（紐付条件）
            var WCd = this.form.dgvWanSign["colWCd", wanSignCurrentCell.RowIndex].Value.ToString();

            //環境将軍Ｒ（紐付条件）
            var RCd = this.form.dgvShougun["colRCd", shougunRCurrentCell.RowIndex].Value.ToString();

            //契約日を選択している場合、契約日（委託契約書）以外が選択されている場合
            //契約日（委託契約書）を選択している場合、契約日以外が選択されている場合
            if ((WCd.Equals("CONTRACT_DATE") && !RCd.Equals("KEIYAKUSHO_KEIYAKU_DATE")) ||
                (!WCd.Equals("CONTRACT_DATE") && RCd.Equals("KEIYAKUSHO_KEIYAKU_DATE")))
            {
                //メッセージAを表示（処理終了）
                this.errmessage.MessageBoxShowError(MsgA);

                return false;
            }

            //SQL
            var sql = "SELECT DISTINCT SHOUGUNR.SYSTEM_ID, WANSIGN.DOCUMENT_ID FROM (" + sqlShougunR + " ) SHOUGUNR, (" + sqlWanSign + " ) WANSIGN ";

            //WHERE
            string where = string.Empty;

            //検索方法＝1.完全一致 
            //契約日を選択している場合
            //契約日（委託契約書）が選択されている場合
            if (WCd.Equals("CONTRACT_DATE") || "1".Equals(this.form.KENSAKU_HOUHOU.Text))
            {
                //#161406 20220314 CongBinh S
                if (WCd.Equals("CONTRACT_DECIMAL"))
                {
                    where = "WHERE CONVERT(NVARCHAR, WANSIGN." + WCd + ", 128) = REPLACE(SHOUGUNR." + RCd + ", ',', '')";//#161569 20220321 CongBinh
                }
                else if (WCd.Equals("PARTNER_ORGANIZE_NM"))
                {
                    where = " WHERE ( WANSIGN." + WCd + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM2 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM3 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM4 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM5 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM6 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM7 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM8 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM9 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM10 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM11 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM12 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM13 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM14 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM15 " + " = SHOUGUNR." + RCd;
                    where += " OR WANSIGN.PARTNER_ORGANIZE_NM16 " + " = SHOUGUNR." + RCd +" )";
                }
                else
                {
                    where = "WHERE WANSIGN." + WCd + " = SHOUGUNR." + RCd;
                }
                //#161406 20220314 CongBinh E
            }
            //検索方法＝2.部分一致         
            else
            {
                var whereOR = new StringBuilder();
                //#161407 20220314 CongBinh S
                if (WCd.Equals("PARTNER_ORGANIZE_NM"))
                {
                    var sqlTmp = " SELECT DISTINCT  WANSIGN." + WCd +
                        @", WANSIGN.PARTNER_ORGANIZE_NM2,
                            WANSIGN.PARTNER_ORGANIZE_NM3, 
                            WANSIGN.PARTNER_ORGANIZE_NM4, 
                            WANSIGN.PARTNER_ORGANIZE_NM5, 
                            WANSIGN.PARTNER_ORGANIZE_NM6, 
                            WANSIGN.PARTNER_ORGANIZE_NM7, 
                            WANSIGN.PARTNER_ORGANIZE_NM8, 
                            WANSIGN.PARTNER_ORGANIZE_NM9, 
                            WANSIGN.PARTNER_ORGANIZE_NM10, 
                            WANSIGN.PARTNER_ORGANIZE_NM11, 
                            WANSIGN.PARTNER_ORGANIZE_NM12, 
                            WANSIGN.PARTNER_ORGANIZE_NM13, 
                            WANSIGN.PARTNER_ORGANIZE_NM14, 
                            WANSIGN.PARTNER_ORGANIZE_NM15, 
                            WANSIGN.PARTNER_ORGANIZE_NM16, 
                            WANSIGN.DOCUMENT_ID FROM (" + sqlWanSign + " ) WANSIGN WHERE WANSIGN.DOCUMENT_ID IN (" + documnetId + ")";
                    var dtWanSign = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDateForStringSql(sqlTmp);

                    if (dtWanSign != null && dtWanSign.Rows.Count > 0)
                    {
                        var i = 0;
                        foreach (DataRow row in dtWanSign.Rows)
                        {
                            whereOR.AppendLine("( ( SHOUGUNR." + RCd + " LIKE '%" + row[WCd] + "%'");
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM2"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM2"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM3"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM3"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM4"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM4"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM5"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM5"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM6"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM6"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM7"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM7"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM8"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM8"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM9"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM9"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM10"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM10"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM11"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM11"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM12"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM12"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM13"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM13"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM14"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM14"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM15"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM15"] + "%'");
                            }
                            if (!string.IsNullOrEmpty(row["PARTNER_ORGANIZE_NM16"].ToString()))
                            {
                                whereOR.AppendLine(" OR SHOUGUNR." + RCd + " LIKE '%" + row["PARTNER_ORGANIZE_NM16"] + "%'");
                            }
                            whereOR.AppendLine(" ) AND  WANSIGN.DOCUMENT_ID ='" + row["DOCUMENT_ID"] + "')");
                            if (i < dtWanSign.Rows.Count - 1)
                            {
                                whereOR.AppendLine(" OR ");
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    var sqlTmp = "SELECT DISTINCT  WANSIGN." + WCd + ", WANSIGN.DOCUMENT_ID FROM (" + sqlWanSign + " ) WANSIGN WHERE WANSIGN.DOCUMENT_ID IN (" + documnetId + ")";
                    var dtWanSign = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDateForStringSql(sqlTmp);

                    if (dtWanSign != null && dtWanSign.Rows.Count > 0)
                    {
                        var i = 0;
                        foreach (DataRow row in dtWanSign.Rows)
                        {
                            whereOR.AppendLine("( SHOUGUNR." + RCd + " LIKE '%" + row[WCd] + "%' AND  WANSIGN.DOCUMENT_ID ='" + row["DOCUMENT_ID"] + "')");
                            if (i < dtWanSign.Rows.Count - 1)
                            {
                                whereOR.AppendLine(" OR ");
                            }
                            i++;
                        }
                    }
                }
                //#161407 20220314 CongBinh E

                where = "WHERE (" + (whereOR.ToString().TrimEnd('R')).TrimEnd('O') +")";
            }

            where = where + " AND WANSIGN.DOCUMENT_ID IN (" + documnetId + ")";

            sql = sql + " " + where + " ORDER BY WANSIGN.DOCUMENT_ID";

            //SQLを実施する
            var dtTmp = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDateForStringSql(sql);

            if (dtTmp != null)
            {
                foreach (var item in this.form.InOutSysId.Keys)
                {
                    //トランザクションIDリスト
                    var dataRows = dtTmp.Rows.Cast<DataRow>().Where(s => s["DOCUMENT_ID"].ToString() == item).ToList();

                    if (dataRows.Count == 0)
                    {
                        dic.Add(item, new List<string>() { "", ITAKU_KEIYAKU_0 });
                    }
                    else if (dataRows.Count == 1)
                    {
                        dic.Add(item, new List<string>() { dataRows[0]["SYSTEM_ID"].ToString(), "" });
                    }
                    else
                    {
                        dic.Add(item, new List<string>() { "", ITAKU_KEIYAKU_FUKUSUU });
                    }
                }
            }

            //トランザクションIDリスト
            this.form.InOutSysId = dic;
            return true;
        }
    }
}
