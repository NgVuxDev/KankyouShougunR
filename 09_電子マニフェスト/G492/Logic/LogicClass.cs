using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using System.Data.SqlTypes;
using Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Dao;
using Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Dto;
using Seasar.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class DenshiManifestPatternTourokuLogic
    {
        #region フィールド
        /// <summary>
        /// 廃棄物区分検索Dao
        /// </summary>
        private IM_HAIKI_KBNDao GetHaikiKbnDao;

        /// <summary>
        /// 電子マニフェスト検索Dao
        /// </summary>
        private GetElectronicPatternDaoCls GetElectronicPatternDao;

        /// <summary>
        /// 電子マニフェストパターン
        /// </summary>
        private EpEntryDaoCls EpEntryDao;

        /// <summary>
        /// 電子マニフェストパターン収集運搬
        /// </summary>
        private EpUpnDaoCls EpUpnDao;

        /// <summary>
        /// 電子マニフェストパターン最終処分
        /// </summary>
        private EpLastSbnDaoCls EpLastSbnDao;

        /// <summary>
        /// 電子マニフェストパターン備考
        /// </summary>
        private EpBikouDaoCls EpBikouDao;

        /// <summary>
        /// 電子マニフェストパターン連絡番号
        /// </summary>
        private EpRenrakuDaoCls EpRenrakuDao;

        /// <summary>
        /// 電子マニフェストパターン最終処分(予定)
        /// </summary>
        private EpLastSbnYoteiDaoCls EpLastSbnYoteiDao;

        /// <summary>
        /// 電子マニフェストパターン有害物質
        /// </summary>
        private EpYuugaiDaoCls EpYuugaiDao;

        /// <summary>
        /// 電子マニフェストパターン拡張
        /// </summary>
        private EpEntryEXDaoCls EpEntryEXDao;


        /// <summary>
        /// Form
        /// </summary>
        private DenshiManifestPatternTouroku form;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// システムID
        /// </summary>
        private SqlInt64 delSystemId;

        /// <summary>
        /// seq
        /// </summary>
        private SqlInt32 delSeq;

        /// <summary>
        /// timesTamp
        /// </summary>
        private byte[] delTimesTamp;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenshiManifestPatternTourokuLogic(DenshiManifestPatternTouroku targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.GetHaikiKbnDao = DaoInitUtility.GetComponent<IM_HAIKI_KBNDao>();
            this.GetElectronicPatternDao = DaoInitUtility.GetComponent<GetElectronicPatternDaoCls>();

            this.EpEntryDao = DaoInitUtility.GetComponent<EpEntryDaoCls>();
            this.EpUpnDao = DaoInitUtility.GetComponent<EpUpnDaoCls>();
            this.EpLastSbnDao = DaoInitUtility.GetComponent<EpLastSbnDaoCls>();
            this.EpBikouDao = DaoInitUtility.GetComponent<EpBikouDaoCls>();
            this.EpRenrakuDao = DaoInitUtility.GetComponent<EpRenrakuDaoCls>();
            this.EpLastSbnYoteiDao = DaoInitUtility.GetComponent<EpLastSbnYoteiDaoCls>();
            this.EpYuugaiDao = DaoInitUtility.GetComponent<EpYuugaiDaoCls>();
            this.EpEntryEXDao = DaoInitUtility.GetComponent<EpEntryEXDaoCls>();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        internal void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.bt_func9.Enabled = true;
                this.form.bt_func12.Enabled = true;

                this.form.bt_func9.Text = Const.ConstCls.BUTTON_JIKKOU_NAME;
                this.form.bt_func12.Text = Const.ConstCls.BUTTON_CLOSE_NAME;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //ロジック初期化
                this.msgLogic = new MessageBoxShowLogic();

                //ボタンの初期化
                this.ButtonInit();

                //Headerの初期化
                this.SetHeaderText();

                //コントロール初期化
                this.ControlInit();

                // イベントの初期化処理
                this.EventInit();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// Headerの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            LogUtility.DebugMethodStart();

            //ヘッダを設定する
            StringBuilder sbTitle = new StringBuilder();

            try
            {

                M_HAIKI_KBN mHaikiKbnNm = GetHaikiKbnDao.GetDataByCd(Const.ConstCls.ELECTRONIC_HAIKI_KBN_CD);

                if (mHaikiKbnNm != null)
                {
                    sbTitle.Append(mHaikiKbnNm.HAIKI_KBN_NAME);
                }

                if (Properties.Settings.Default.firstManifestKbn.Equals((int)Const.ConstCls.enumFirstManifestKbn.ItijiManifestKbn))
                {
                    //一次マニフェスト
                    sbTitle.Append(Const.ConstCls.ITIJI_MANIFEST_KBN);
                }
                else
                {
                    //一次マニフェスト以外（2次）
                    sbTitle.Append(Const.ConstCls.NIJI_MANIFEST_KBN);
                }

                sbTitle.Append(Const.ConstCls.FORM_HEADER_TITLE);

                this.form.lbl_title.Text = sbTitle.ToString();

                // Formタイトルの設定
                this.form.Text = sbTitle.ToString();

                //画面コメント
                this.form.lbl_Comment.Text = Const.ConstCls.FORM_COMMENT;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            LogUtility.DebugMethodStart();

            this.form.txt_PatternNameKana.Enabled = true;
            this.form.txt_PatternName.Enabled = true;

            try
            {
                if (Properties.Settings.Default.firstManifestKbn.Equals(
                    (int)Const.ConstCls.enumFirstManifestKbn.ItijiManifestKbn))
                {
                    //一次マニフェスト
                    //緑背景
                    this.form.lbl_title.BackColor = System.Drawing.Color.FromArgb(0, 105, 51);
                    this.form.lbl_PatternName.BackColor = System.Drawing.Color.FromArgb(0, 105, 51);
                    this.form.lbl_PatternNameKana.BackColor = System.Drawing.Color.FromArgb(0, 105, 51);

                }
                else
                {
                    //一次マニフェスト以外（2次）
                    //青背景 
                    this.form.lbl_title.BackColor = System.Drawing.Color.FromArgb(0, 51, 160);
                    this.form.lbl_PatternName.BackColor = System.Drawing.Color.FromArgb(0, 51, 160);
                    this.form.lbl_PatternNameKana.BackColor = System.Drawing.Color.FromArgb(0, 51, 160);

                }

                //修正の場合
                if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                {
                    DataTable dt = new DataTable();
                    SerchParameterDtoCls serch = new SerchParameterDtoCls();
                    serch.SYSTEM_ID = this.form.EpEntrylist[0].SYSTEM_ID;
                    serch.SEQ = this.form.EpEntrylist[0].SEQ;

                    dt = GetElectronicPatternDao.GetDataForEntity(serch);

                    //データの存在場合
                    if (dt.Rows.Count > 0)
                    {
                        this.form.txt_PatternName.Text = dt.Rows[0][0].ToString();
                        this.form.txt_PatternNameKana.Text = dt.Rows[0][1].ToString();

                        this.delSeq = this.form.EpEntrylist[0].SEQ;
                        this.delSystemId = this.form.EpEntrylist[0].SYSTEM_ID;
                        this.delTimesTamp = (byte[])dt.Rows[0][2];
                    }
                    else
                    {
                        this.form.bt_func9.Enabled = false;
                        this.form.txt_PatternNameKana.Enabled = false;
                        this.form.txt_PatternName.Enabled = false;
                        //該当データは他ユーザーにより更新されています
                        this.msgLogic.MessageBoxShow("E080");
                        this.form.bt_func12.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 登録
        /// </summary>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            if (!errorFlag)
            {
                return;
            }

            try
            {
                //入力チェック
                if (this.ChkPattern())
                {
                    return;
                }

                //登録パターン
                //　新規（既存に名前なし）
                //　新規（既存に名前あり）→上書き確認
                //　更新（既存に名前あり(修正元と同じ)）→修正確認
                //　更新（既存に名前あり(修正元以外)）→上書き確認（元はそのまま）
                //　更新（既存に名前なし）→修正せずに、新規
                //※名前の変更する場合は、コピーで新規をつくり、その後消す。という手順になる。


                //存在チェック
                var sameNamePettern = EpEntryDao.SelectByName(this.form.txt_PatternName.Text, SqlBoolean.False);

                if (sameNamePettern.Length == 0)
                {
                    //重複無（新規 SEQ=1）
                    this.form.processKbn = PROCESS_KBN.NEW; //新規に強制変更
                }
                else if (!this.form.EpEntrylist[0].SYSTEM_ID.IsNull && sameNamePettern[0].SYSTEM_ID == this.form.EpEntrylist[0].SYSTEM_ID)
                {
                    //重複ありで、自分自身（確認＋削除フラグ＋新規（SEQ+1)）
                    this.form.processKbn = PROCESS_KBN.UPDATE; //更新に変更

                    //削除情報登録
                    this.form.EpEntrylist[0].SYSTEM_ID = sameNamePettern[0].SYSTEM_ID;
                    this.form.EpEntrylist[0].SEQ = sameNamePettern[0].SEQ;

                    this.delSystemId = sameNamePettern[0].SYSTEM_ID;
                    this.delSeq = sameNamePettern[0].SEQ;
                    this.delTimesTamp = sameNamePettern[0].TIME_STAMP;

                }
                else
                {
                    //重複ありで、自分でない（確認＋削除フラグ＋新規（SEQ+1)）
                    this.form.processKbn = PROCESS_KBN.UPDATE; //更新に変更


                    //削除情報登録
                    this.form.EpEntrylist[0].SYSTEM_ID = sameNamePettern[0].SYSTEM_ID;
                    this.form.EpEntrylist[0].SEQ = sameNamePettern[0].SEQ;

                    this.delSystemId = sameNamePettern[0].SYSTEM_ID;
                    this.delSeq = sameNamePettern[0].SEQ;
                    this.delTimesTamp = sameNamePettern[0].TIME_STAMP;
                }

                //修正モードの確認メッセージ
                if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                {
                    DialogResult dialogResult = this.msgLogic.MessageBoxShow("C038");

                    if (dialogResult != DialogResult.Yes)
                    {
                        return;
                    }
                }

                int count = 0;

                List<DT_PT_R18> epEntrylist = this.form.EpEntrylist;
                List<DT_PT_R19> epUpnlist = this.form.EpUpnlist;
                List<DT_PT_R13> epLastSbnlist = this.form.EpLastSbnlist;
                List<DT_PT_R06> epBikoulist = this.form.EpBikoulist;
                List<DT_PT_R05> epRenrakulist = this.form.EpRenrakulist;
                List<DT_PT_R04> epLastSbnYoteilist = this.form.EpLastSbnYoteilist;
                List<DT_PT_R02> epYugailist = this.form.EpYuugailist;
                List<DT_PT_R18_EX> epEntryEXlist = this.form.EpEntryEXlist;

                //登録データ作成
                this.MakePtData(ref epEntrylist, ref epUpnlist, ref epLastSbnlist, ref epBikoulist, ref epRenrakulist,
                    ref epLastSbnYoteilist, ref epYugailist, ref epEntryEXlist);

                //データ更新
                using (Transaction tran = new Transaction())
                {
                    if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                    {
                        //論理削除処理
                        this.LogicalEntityDel();
                    }

                    //電子マニフェストパターン
                    if (epEntrylist != null && epEntrylist.Count() > 0)
                    {
                        foreach (DT_PT_R18 epEntry in epEntrylist)
                        {
                            count = EpEntryDao.Insert(epEntry);
                        }
                    }

                    //電子マニフェストパターン収集運搬
                    if (epUpnlist != null && epUpnlist.Count() > 0)
                    {
                        foreach (DT_PT_R19 epUpn in epUpnlist)
                        {
                            count = EpUpnDao.Insert(epUpn);
                        }
                    }

                    //電子マニフェストパターン最終処分
                    if (epLastSbnlist != null && epLastSbnlist.Count() > 0)
                    {
                        foreach (DT_PT_R13 epLastSbn in epLastSbnlist)
                        {
                            count = EpLastSbnDao.Insert(epLastSbn);
                        }
                    }

                    //電子マニフェストパターン備考
                    if (epBikoulist != null && epBikoulist.Count() > 0)
                    {
                        foreach (DT_PT_R06 epBikou in epBikoulist)
                        {
                            count = EpBikouDao.Insert(epBikou);
                        }
                    }


                    //電子マニフェストパターン連絡番号
                    if (epRenrakulist != null && epRenrakulist.Count() > 0)
                    {
                        foreach (DT_PT_R05 epRenraku in epRenrakulist)
                        {
                            count = EpRenrakuDao.Insert(epRenraku);
                        }
                    }

                    //電子マニフェストパターン最終処分(予定)
                    if (epLastSbnYoteilist != null && epLastSbnYoteilist.Count() > 0)
                    {
                        foreach (DT_PT_R04 epLastSbnYotei in epLastSbnYoteilist)
                        {
                            count = EpLastSbnYoteiDao.Insert(epLastSbnYotei);
                        }
                    }

                    //電子マニフェストパターン有害物質
                    if (epYugailist != null && epYugailist.Count() > 0)
                    {
                        foreach (DT_PT_R02 epYuugai in epYugailist)
                        {
                            count = EpYuugaiDao.Insert(epYuugai);
                        }
                    }

                    //電子マニフェストパターン拡張
                    if (epEntryEXlist != null && epEntryEXlist.Count() > 0)
                    {
                        foreach (DT_PT_R18_EX epEntryEX in epEntryEXlist)
                        {
                            count = EpEntryEXDao.Insert(epEntryEX);
                        }
                    }

                    tran.Commit();
                    //登録結果をフォームに
                    this.form.RegistedSystemId = epEntrylist[0].SYSTEM_ID;
                    this.form.RegistedSeq = epEntrylist[0].SEQ;
                }

                //更新の場合
                if (this.form.processKbn == PROCESS_KBN.UPDATE)
                {
                    this.form.UpdModeUpdSuccessFlg = true;
                }

                //更新後は完了メッセージを表示する
                msgLogic.MessageBoxShow("I001", "登録");

                //自動で閉じる
                this.form.DialogResult = DialogResult.OK;
                this.form.Close();
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
                LogUtility.DebugMethodEnd(errorFlag);
            }
        }

        #region データ作成処理(マニパターン)
        /// <summary>
        /// データ作成
        /// </summary>
        /// <param name="entrylist">電子マニフェストパターン</param>
        /// <param name="upnlist">電子マニフェストパターン収集運搬</param>
        /// <param name="lastSbnlist">電子マニフェストパターン最終処分</param>
        /// <param name="bikoulist">電子マニフェストパターン備考</param>
        /// <param name="renrakulist">電子マニフェストパターン連絡番号</param>
        /// <param name="lastSbnYoteilist">電子マニフェストパターン最終処分(予定)</param>
        /// <param name="yuugailist">電子マニフェストパターン有害物質</param>
        /// <param name="entryEXlist">電子マニフェストパターン拡張</param>
        public void MakePtData(ref List<DT_PT_R18> entrylist,
            ref List<DT_PT_R19> upnlist,
            ref List<DT_PT_R13> lastSbnlist,
            ref List<DT_PT_R06> bikoulist,
            ref List<DT_PT_R05> renrakulist,
            ref List<DT_PT_R04> lastSbnYoteilist,
            ref List<DT_PT_R02> yuugailist,
            ref List<DT_PT_R18_EX> entryEXlist)
        {
            LogUtility.DebugMethodStart(entrylist, upnlist, lastSbnlist, bikoulist, renrakulist, lastSbnYoteilist, yuugailist, entryEXlist);

            SqlInt64 lSysId = 0;
            SqlInt32 iSeq = 0;

            Common.BusinessCommon.DBAccessor dba = null;
            dba = new Common.BusinessCommon.DBAccessor();

            try
            {
                if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                {
                    //更新
                    lSysId = entrylist[0].SYSTEM_ID;
                    iSeq = entrylist[0].SEQ;
                }
                else
                {
                    //新規登録：採番処理
                    lSysId = (long)dba.createSystemIdWithTableLock((int)DENSHU_KBN.DENSHI_MANIFEST);
                }

                iSeq = iSeq + 1;

                //電子マニフェストパターン
                if (entrylist != null && entrylist.Count() > 0)
                {
                    foreach (DT_PT_R18 entry in entrylist)
                    {
                        //システムID
                        entry.SYSTEM_ID = lSysId;
                        //枝番
                        entry.SEQ = iSeq;
                        //パターン名
                        entry.PATTERN_NAME = this.form.txt_PatternName.Text;
                        //パターンふりがな
                        entry.PATTERN_FURIGANA = this.form.txt_PatternNameKana.Text;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R18>(entry);
                        dataBinderEntry1.SetSystemProperty(entry, false);

                        //登録しない項目
                        //マニフェスト／予約番号	
                        entry.MANIFEST_ID = null;
                        //引渡し日	
                        entry.HIKIWATASHI_DATE = null;
                        //運搬終了報告済フラグ	
                        entry.UPN_ENDREP_FLAG = SqlDecimal.Null;
                        //処分終了報告済フラグ	
                        entry.SBN_ENDREP_FLAG = SqlDecimal.Null;
                        //最終処分終了報告済フラグ	
                        entry.LAST_SBN_ENDREP_FLAG = SqlDecimal.Null;
                        //課金日	
                        entry.KAKIN_DATE = null;
                        //登録日	
                        entry.REGI_DATE = null;
                        //運搬・処分終了報告期限日	
                        entry.UPN_SBN_REP_LIMIT_DATE = null;
                        //最終処分終了報告期限日	
                        entry.LAST_SBN_REP_LIMIT_DATE = null;
                        //予約情報有効期限日	
                        entry.RESV_LIMIT_DATE = null;
                        //処分報告情報承認待ちフラグ	
                        entry.SBN_SHOUNIN_FLAG = SqlDecimal.Null;
                        //処分終了日	
                        entry.SBN_END_DATE = null;
                        //廃棄物の受領日	
                        entry.HAIKI_IN_DATE = null;
                        //処分終了報告日	
                        entry.SBN_END_REP_DATE = null;
                        //最終処分終了日	
                        entry.LAST_SBN_END_DATE = null;
                        //最終処分終了報告日	
                        entry.LAST_SBN_END_REP_DATE = null;
                        //修正日	
                        entry.SHUSEI_DATE = null;
                        //取消フラグ	
                        entry.CANCEL_FLAG = SqlDecimal.Null;
                        //取消日	
                        entry.CANCEL_DATE = null;
                        //最終更新日	
                        entry.LAST_UPDATE_DATE = null;

                        // 電子マニフェストパターン一覧では、「DT_PT_R18.FIRST_MANIFEST_FLAGがNULL以外であれば二次」という判断をしている。
                        // ユーザの設定状況によって、一次マニと二次マニと判断ができなくなるため、
                        // 二次マニの場合はダミーデータとして0を設定する。
                        if (this.form.FirstManifestKbn == 1
                                && string.IsNullOrEmpty(entry.FIRST_MANIFEST_FLAG))
                        {
                            // 二次の場合はダミーデータを設定
                            entry.FIRST_MANIFEST_FLAG = "0";
                        }

                    }
                }

                //電子マニフェストパターン収集運搬
                if (upnlist != null && upnlist.Count() > 0)
                {
                    foreach (DT_PT_R19 upn in upnlist)
                    {
                        //システムID
                        upn.SYSTEM_ID = lSysId;
                        //枝番
                        upn.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R19>(upn);
                        dataBinderEntry1.SetSystemProperty(upn, false);

                        //登録しない項目
                        //マニフェスト／予約番号
                        upn.MANIFEST_ID = null;
                        //運搬報告情報承認待ちフラグ
                        upn.UPN_SHOUNIN_FLAG = SqlDecimal.Null;
                        //運搬終了日
                        upn.UPN_END_DATE = null;
                    }
                }

                //電子マニフェストパターン最終処分
                if (lastSbnlist != null && lastSbnlist.Count() > 0)
                {
                    foreach (DT_PT_R13 lastSbn in lastSbnlist)
                    {
                        //システムID
                        lastSbn.SYSTEM_ID = lSysId;
                        //枝番
                        lastSbn.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R13>(lastSbn);
                        dataBinderEntry1.SetSystemProperty(lastSbn, false);

                        //登録しない項目
                        //マニフェスト／予約番号
                        lastSbn.MANIFEST_ID = null;
                        //最終処分終了日
                        lastSbn.LAST_SBN_END_DATE = null;
                    }
                }

                //電子マニフェストパターン備考
                if (bikoulist != null && bikoulist.Count() > 0)
                {
                    foreach (DT_PT_R06 bikou in bikoulist)
                    {
                        //システムID
                        bikou.SYSTEM_ID = lSysId;
                        //枝番
                        bikou.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R06>(bikou);
                        dataBinderEntry1.SetSystemProperty(bikou, false);

                        //登録しない項目
                        //マニフェスト／予約番号
                        bikou.MANIFEST_ID = null;
                    }
                }

                //電子マニフェストパターン連絡番号
                if (renrakulist != null && renrakulist.Count() > 0)
                {
                    foreach (DT_PT_R05 renraku in renrakulist)
                    {
                        //システムID
                        renraku.SYSTEM_ID = lSysId;
                        //枝番
                        renraku.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R05>(renraku);
                        dataBinderEntry1.SetSystemProperty(renraku, false);

                        //登録しない項目
                        //マニフェスト／予約番号
                        renraku.MANIFEST_ID = null;
                    }
                }

                //電子マニフェストパターン最終処分(予定)
                if (lastSbnYoteilist != null && lastSbnYoteilist.Count() > 0)
                {
                    foreach (DT_PT_R04 lastSbnYotei in lastSbnYoteilist)
                    {
                        //システムID
                        lastSbnYotei.SYSTEM_ID = lSysId;
                        //枝番
                        lastSbnYotei.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R04>(lastSbnYotei);
                        dataBinderEntry1.SetSystemProperty(lastSbnYotei, false);

                        //登録しない項目
                        //マニフェスト／予約番号
                        lastSbnYotei.MANIFEST_ID = null;

                    }
                }

                //電子マニフェストパターン有害物質
                if (yuugailist != null && yuugailist.Count() > 0)
                {
                    foreach (DT_PT_R02 yuugai in yuugailist)
                    {
                        //システムID
                        yuugai.SYSTEM_ID = lSysId;
                        //枝番
                        yuugai.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R02>(yuugai);
                        dataBinderEntry1.SetSystemProperty(yuugai, false);

                        //登録しない項目
                        //マニフェスト／予約番号
                        yuugai.MANIFEST_ID = null;

                    }
                }

                //電子マニフェストパターン拡張
                if (entryEXlist != null && entryEXlist.Count() > 0)
                {
                    foreach (DT_PT_R18_EX entryEX in entryEXlist)
                    {
                        //システムID
                        entryEX.SYSTEM_ID = lSysId;
                        //枝番
                        entryEX.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<DT_PT_R18_EX>(entryEX);
                        dataBinderEntry1.SetSystemProperty(entryEX, false);

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entrylist, upnlist, lastSbnlist, bikoulist, renrakulist, lastSbnYoteilist, yuugailist, entryEXlist);
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public void LogicalEntityDel()
        {
            LogUtility.DebugMethodStart();
            int count = 0;

            try
            {
                DT_PT_R18 del = new DT_PT_R18();
                del.SYSTEM_ID = this.delSystemId;
                del.SEQ = this.delSeq;
                del.DELETE_FLG = true;
                del.TIME_STAMP = this.delTimesTamp;

                var dataBinderEntry1 = new DataBinderLogic<DT_PT_R18>(del);
                dataBinderEntry1.SetSystemProperty(del, true);

                count = EpEntryDao.Update(del);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初期表示の場合、画面クリア
        /// </summary>
        public void ClearScreen()
        {
            LogUtility.DebugMethodStart();

            //フリガナ
            this.form.txt_PatternNameKana.Text = string.Empty;

            //パターン名
            this.form.txt_PatternName.Text = string.Empty;

            //タイトル
            this.form.lbl_title.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //登録ボタン(F9)イベント生成
                this.form.bt_func9.Click += new EventHandler(this.form.Regist);

                //閉じるボタン(F12)イベント生成
                this.form.bt_func12.Click += new EventHandler(this.form.FormClose);

                this.form.txt_PatternName.KeyUp += new KeyEventHandler(this.form.DenshiManifestPatternTouroku_KeyUp);

                this.form.txt_PatternNameKana.KeyUp += new KeyEventHandler(this.form.DenshiManifestPatternTouroku_KeyUp);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region チェック
        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkPattern()
        {

            LogUtility.DebugMethodStart();

            string errorMsg = string.Empty;

            Const.ConstCls.enumChkErKbn errorKbnKana = Const.ConstCls.enumChkErKbn.None;
            Const.ConstCls.enumChkErKbn errorKbnPattern = Const.ConstCls.enumChkErKbn.None;

            try
            {
                MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();

                //パターンふりがなは空白の場合
                var msg1 = Shougun.Core.Message.MessageUtility.GetMessageString("E012");
                if (string.IsNullOrEmpty(this.form.txt_PatternNameKana.Text))
                {
                    msg1 = string.Format(msg1, "フリガナ");
                    errorKbnKana = Const.ConstCls.enumChkErKbn.PatternNameKana;
                }

                //パターン名は空白の場合
                var msg3 = Shougun.Core.Message.MessageUtility.GetMessageString("E012");
                if (string.IsNullOrEmpty(this.form.txt_PatternName.Text))
                {
                    msg3 = string.Format(msg3, "パターン名");
                    errorKbnPattern = Const.ConstCls.enumChkErKbn.PatternName;
                }

                switch (errorKbnKana)
                {
                    case Const.ConstCls.enumChkErKbn.PatternNameKana:
                        if (errorKbnPattern != Const.ConstCls.enumChkErKbn.PatternName)
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1);
                            this.form.txt_PatternNameKana.Focus();
                            return true;
                        }
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg1 + Environment.NewLine + msg3);
                        this.form.txt_PatternName.Focus();
                        return true;

                    case Const.ConstCls.enumChkErKbn.None:
                        if (errorKbnPattern != Const.ConstCls.enumChkErKbn.PatternName)
                        {
                            break;
                        }
                        Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(msg3);
                        this.form.txt_PatternName.Focus();
                        return true;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();

            return false;
        }
        #endregion
    }
}
