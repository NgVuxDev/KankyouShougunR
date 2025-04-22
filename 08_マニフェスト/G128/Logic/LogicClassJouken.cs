using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.PaperManifest.Manifestsuiihyo.APP;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data;

namespace Shougun.Core.PaperManifest.Manifestsuiihyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClassJouken : IBuisinessLogic
    {

        ///<summary>
        ///業者マスタのDao
        ///</summary>
        private GET_GYOUSHA_DaoCls GetGyoushayDao;

        ///<summary>
        ///現場マスタのDao
        ///</summary>
        private GET_GENBA_DaoCls GetGenbaDao;

        ///<summary>
        ///廃棄物種類マスタのDao
        ///</summary>
        private GET_HAIKI_SHURUI_DaoCls GetHaikiShuruiDao;

        /// <summary>
        /// Form
        /// </summary>
        private JoukenPopupForm form;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClassJouken(JoukenPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.GetGyoushayDao = DaoInitUtility.GetComponent<GET_GYOUSHA_DaoCls>();
            this.GetGenbaDao = DaoInitUtility.GetComponent<GET_GENBA_DaoCls>();
            this.GetHaikiShuruiDao = DaoInitUtility.GetComponent<GET_HAIKI_SHURUI_DaoCls>();

            LogUtility.DebugMethodEnd();
        }

        #region 共通継承
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit(JoukenParam jouken)
        {
            LogUtility.DebugMethodStart(jouken);

            // イベントの初期化処理
            this.EventInit();

            // 画面の初期化
            this.form.cstmNmTxtB_IchijiNiji.Text = jouken.ichijinijiKbn;                    // 一時二次区分
            this.form.cstmDateTimePicker_NengappiFrom.Value = jouken.nengappiFrom;          // 年月日開始
            this.form.cstmDateTimePicker_NengappiTo.Value = jouken.nengappiTo;              // 年月日終了
            this.form.cstmANTexB_Kyoten.Text = jouken.kyoten;                               // 拠点CD
            this.form.cstmANTexB_HaishutuJigyoushaFrom.Text = jouken.haiJigyouShaFrom;      // 排出事業者CD開始
            this.form.cstmANTexB_HaishutuJigyoushaTo.Text = jouken.haiJigyouShaTo;          // 排出事業者CD終了
            this.form.cstmANTexB_HaisyutsuJigyoubaFrom.Text = jouken.haiJigyouBaFrom;       // 排出事業場CD開始
            this.form.cstmANTexB_HaisyutsuJigyoubaTo.Text = jouken.haiJigyouBaTo;           // 排出事業場CD終了
            this.form.cstmANTexB_UnpanJutakushaFrom.Text = jouken.unpanJutakuShaFrom;       // 搬受託者CD開始
            this.form.cstmANTexB_UnpanJutakushaTo.Text = jouken.unpanJutakuShaTo;           // 運搬受託者CD終了
            this.form.cstmANTexB_ShobunJutakushaFrom.Text = jouken.shobunJutakuShaFrom;     // 処分受託者CD開始
            this.form.cstmANTexB_ShobunJutakushaTo.Text = jouken.shobunJutakuShaTo;         // 処分受託者CD終了
            this.form.cstmANTexB_SaishuuShobunJouFrom.Text = jouken.saisyuuShobunBashoFrom; // 最終処分場所CD開始
            this.form.cstmANTexB_SaishuuShobunJouTo.Text = jouken.saisyuuShobunBashoTo;     // 最終処分場所CD終了
            this.form.cstmANTexB_Chokkou.Text = jouken.chokkouHaikibutuSyurui;              // 産廃（直行）廃棄物種類CD
            this.form.cstmANTexB_Tsumikae.Text = jouken.tsumikaeHaikibutuSyurui;            // 産廃（積替）廃棄物種類CD
            this.form.cstmANTexB_Kenpai.Text = jouken.kenpaiHaikibutuSyurui;                // 建廃廃棄物種類CD
            this.form.cstmANTexB_Denshi.Text = jouken.denshiHaikibutuSyurui;                // 電子廃棄物種類CD
            this.form.cstmNmTxtB_ShuturyokuNaiyou.Text = jouken.syuturyokuNaiyoiu;          // 出力内容
            this.form.cstmNmTxtB_ShuturyokuKubun.Text = jouken.syuturyokuKubun;             // 出力区分

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 検索実行ボタン(F9)イベント生成
            this.form.btn_kensakujikkou.DialogResult = DialogResult.OK;
            this.form.btn_kensakujikkou.Click += new EventHandler(this.form.SearchExec);

            // キャンセルボタン(F12)イベント生成
            this.form.btn_cancel.DialogResult = DialogResult.Cancel;
            this.form.btn_cancel.Click += new EventHandler(this.form.FormClose);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 設定値保存
        /// </summary>
        internal void SaveParams()
        {
            LogUtility.DebugMethodStart();

            var info = new JoukenParam();
            // 初期設定条件を設定
            var messageShowLogic = new MessageBoxShowLogic();

            DateTime dateFrom;
            DateTime.TryParse(this.form.cstmDateTimePicker_NengappiFrom.Value.ToString(), out dateFrom);

            DateTime dateTo;
            DateTime.TryParse(this.form.cstmDateTimePicker_NengappiTo.Value.ToString(), out dateTo);

            // 画面の初期化
            info.ichijinijiKbn = this.form.cstmNmTxtB_IchijiNiji.Text;                      // 一時二次区分
            info.nengappiFrom = dateFrom.ToString("yyyy/MM/dd");                            // 年月日開始
            info.nengappiTo = dateTo.ToString("yyyy/MM/dd");                                // 年月日終了
            info.kyoten = this.form.cstmANTexB_Kyoten.Text;                                 // 拠点CD
            info.haiJigyouShaFrom = this.form.cstmANTexB_HaishutuJigyoushaFrom.Text;        // 排出事業者CD開始
            info.haiJigyouShaTo = this.form.cstmANTexB_HaishutuJigyoushaTo.Text;            // 排出事業者CD終了
            info.haiJigyouBaFrom = this.form.cstmANTexB_HaisyutsuJigyoubaFrom.Text;         // 排出事業場CD開始
            info.haiJigyouBaTo = this.form.cstmANTexB_HaisyutsuJigyoubaTo.Text;             // 排出事業場CD終了
            info.unpanJutakuShaFrom = this.form.cstmANTexB_UnpanJutakushaFrom.Text;         // 搬受託者CD開始
            info.unpanJutakuShaTo = this.form.cstmANTexB_UnpanJutakushaTo.Text;             // 運搬受託者CD終了
            info.shobunJutakuShaFrom = this.form.cstmANTexB_ShobunJutakushaFrom.Text;       // 処分受託者CD開始
            info.shobunJutakuShaTo = this.form.cstmANTexB_ShobunJutakushaTo.Text;           // 処分受託者CD終了
            info.saisyuuShobunBashoFrom = this.form.cstmANTexB_SaishuuShobunJouFrom.Text;   // 最終処分場所CD開始
            info.saisyuuShobunBashoTo = this.form.cstmANTexB_SaishuuShobunJouTo.Text;       // 最終処分場所CD終了
            info.chokkouHaikibutuSyurui = this.form.cstmANTexB_Chokkou.Text;                // 産廃（直行）廃棄物種類CD
            info.tsumikaeHaikibutuSyurui = this.form.cstmANTexB_Tsumikae.Text;              // 産廃（積替）廃棄物種類CD
            info.kenpaiHaikibutuSyurui = this.form.cstmANTexB_Kenpai.Text;                  // 建廃廃棄物種類CD
            info.denshiHaikibutuSyurui = this.form.cstmANTexB_Denshi.Text;                  // 電子廃棄物種類CD
            info.syuturyokuNaiyoiu = this.form.cstmNmTxtB_ShuturyokuNaiyou.Text;            // 出力内容
            info.syuturyokuKubun = this.form.cstmNmTxtB_ShuturyokuKubun.Text;               // 出力区分

            this.form.joken = info;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkGyousha(string titleName)
        {
            LogUtility.DebugMethodStart();

            try
            {
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable searchResult = new DataTable();

                sql.Append(" SELECT M_GYOUSHA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_ADDRESS1, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_TEL ");
                sql.Append(" FROM M_GYOUSHA LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_CD ");

                // 排出事業者CDFrom
                if ("排出事業者CDFrom".Equals(titleName))
                {
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 排出事業者CDTo
                else if ("排出事業者CDTo".Equals(titleName))
                {
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaTo.Text.PadLeft(6, '0') + "'");
                }
                // 運搬受託者CDFrom
                else if ("運搬受託者CDFrom".Equals(titleName))
                {
                    sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_UnpanJutakushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 運搬受託者CDTo
                else if ("運搬受託者CDTo".Equals(titleName))
                {
                    sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_UnpanJutakushaTo.Text.PadLeft(6, '0') + "'");
                }
                // 処分受託者CDFrom
                else if ("処分受託者CDFrom".Equals(titleName))
                {
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_ShobunJutakushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 処分受託者CDTo
                else if ("処分受託者CDTo".Equals(titleName))
                {
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_ShobunJutakushaTo.Text.PadLeft(6, '0') + "'");
                }

                searchResult = this.GetGyoushayDao.GetGyoushay(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    // 排出事業者CDFrom
                    if ("排出事業者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaFrom.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 排出事業者CDTo
                    else if ("排出事業者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaTo.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 運搬受託者CDFrom
                    else if ("運搬受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaFrom.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 運搬受託者CDTo
                    else if ("運搬受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaTo.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 処分受託者CDFrom
                    else if ("処分受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaFrom.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 処分受託者CDTo
                    else if ("処分受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaTo.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                }
                else
                {
                    // 排出事業者CDFrom
                    if ("排出事業者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaFrom.Text = string.Empty;
                    }
                    // 排出事業者CDTo
                    else if ("排出事業者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaTo.Text = string.Empty;
                    }
                    // 運搬受託者CDFrom
                    else if ("運搬受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaFrom.Text = string.Empty;
                    }
                    // 運搬受託者CDTo
                    else if ("運搬受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaTo.Text = string.Empty;
                    }
                    // 処分受託者CDFrom
                    else if ("処分受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaFrom.Text = string.Empty;
                    }
                    // 処分受託者CDTo
                    else if ("処分受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaTo.Text = string.Empty;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 現場マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkGenba(string titleName)
        {
            LogUtility.DebugMethodStart();

            try
            {
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable searchResult = new DataTable();

                sql.Append(" SELECT M_GENBA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_CD, ");
                sql.Append(" M_GENBA.GENBA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_FURIGANA, ");
                sql.Append(" M_GENBA.GENBA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_ADDRESS1, ");
                sql.Append(" M_GENBA.GENBA_TEL ");
                sql.Append(" FROM M_GENBA LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sql.Append(" LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD ");
                sql.Append("  = M_TODOUFUKEN.TODOUFUKEN_CD ");

                // 排出事業場CDFrom
                if ("排出事業場CDFrom".Equals(titleName))
                {
                    sql.Append(" AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_HaisyutsuJigyoubaFrom.Text.PadLeft(6, '0') + "'");
                    sql.Append(" AND M_GENBA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 排出事業場CDTo
                else if ("排出事業場CDTo".Equals(titleName))
                {
                    sql.Append(" AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_HaisyutsuJigyoubaTo.Text.PadLeft(6, '0') + "'");
                    sql.Append(" AND M_GENBA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaTo.Text.PadLeft(6, '0') + "'");
                }
                // 最終処分事業場CDFrom
                else if ("最終処分事業場CDFrom".Equals(titleName))
                {
                    sql.Append(" AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_SaishuuShobunJouFrom.Text.PadLeft(6, '0') + "'");
                }
                // 最終処分事業場CDTo
                else if ("最終処分事業場CDTo".Equals(titleName))
                {
                    sql.Append(" AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_SaishuuShobunJouTo.Text.PadLeft(6, '0') + "'");
                }

                searchResult = this.GetGenbaDao.GetGenba(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    // 排出事業場CDFrom
                    if ("排出事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaFrom.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    // 排出事業場CDTo
                    else if ("排出事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaTo.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    // 最終処分事業場CDFrom
                    else if ("最終処分事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouFrom.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    // 最終処分事業場CDTo
                    else if ("最終処分事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouTo.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                }
                else
                {
                    // 排出事業場CDFrom
                    if ("排出事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
                    }
                    // 排出事業場CDTo
                    else if ("排出事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
                    }
                    // 最終処分事業場CDFrom
                    else if ("最終処分事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouFrom.Text = string.Empty;
                    }
                    // 最終処分事業場CDTo
                    else if ("最終処分事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouTo.Text = string.Empty;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 廃棄物種類マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkHaikiShurui(string titleName)
        {
            LogUtility.DebugMethodStart();

            try
            {
                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable searchResult = new DataTable();

                sql.Append(" select HAIKI_SHURUI_CD, ");
                sql.Append(" HAIKI_SHURUI_NAME_RYAKU ");
                sql.Append(" from M_HAIKI_SHURUI ");
                sql.Append(" WHERE  ");
                sql.Append(" 1 = 1 ");

                // 産廃（直行）廃棄物種類CD
                if ("直行".Equals(titleName))
                {
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_KBN_CD = CONVERT(smallint, '1') ");
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_SHURUI_CD = '"
                        + this.form.cstmANTexB_Chokkou.Text.PadLeft(4, '0') + "'");
                }
                // 建廃廃棄物種類CD
                else if ("建廃".Equals(titleName))
                {
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_KBN_CD = CONVERT(smallint, '2') ");
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_SHURUI_CD = '"
                        + this.form.cstmANTexB_Kenpai.Text.PadLeft(4, '0') + "'");
                }
                // 産廃（積替）廃棄物種類CD
                else if ("積替".Equals(titleName))
                {
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_KBN_CD = CONVERT(smallint, '3') ");
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_SHURUI_CD = '"
                        + this.form.cstmANTexB_Tsumikae.Text.PadLeft(4, '0') + "'");
                }

                sql.Append(" group by HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_SHURUI_NAME_RYAKU ");
                searchResult = this.GetHaikiShuruiDao.GetHaikiShurui(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    // 産廃（直行）廃棄物種類CD
                    if ("直行".Equals(titleName))
                    {
                        this.form.cstmTexBox_Chokkou.Text
                            = this.GetDbValue(searchResult.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    }
                    // 建廃廃棄物種類CD
                    else if ("建廃".Equals(titleName))
                    {
                        this.form.cstmTexBox_Kenpai.Text
                            = this.GetDbValue(searchResult.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    }
                    // 産廃（積替）廃棄物種類CD
                    else if ("積替".Equals(titleName))
                    {
                        this.form.cstmTexBox_Tsumikae.Text
                            = this.GetDbValue(searchResult.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    }
                }
                else
                {
                    // 産廃（直行）廃棄物種類CD
                    if ("直行".Equals(titleName))
                    {
                        this.form.cstmTexBox_Chokkou.Text = string.Empty;
                    }
                    // 建廃廃棄物種類CD
                    else if ("建廃".Equals(titleName))
                    {
                        this.form.cstmTexBox_Kenpai.Text = string.Empty;
                    }
                    // 産廃（積替）廃棄物種類CD
                    else if ("積替".Equals(titleName))
                    {
                        this.form.cstmTexBox_Tsumikae.Text = string.Empty;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// DBデータを取得
        /// </summary>
        private string GetDbValue(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return string.Empty;
            }

            return obj.ToString();
        }
    }
}
