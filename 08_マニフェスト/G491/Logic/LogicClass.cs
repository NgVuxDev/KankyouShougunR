using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.ManifestPatternTouroku.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class ManifestPatternRegistLogic
    {
        #region フィールド
        /// <summary>
        /// マニフェスト更新Dao
        /// </summary>
        private PtEntryDaoCls PtEntryDao;

        /// <summary>
        /// マニ収集運搬更新Dao
        /// </summary>
        private PtUpnDaoCls PtUpnDao;

        /// <summary>
        /// マニ印字更新Dao
        /// </summary>
        private PtPrtDaoCls PtPrtDao;

        /// <summary>
        /// マニ印字明細更新Dao
        /// </summary>
        private PtDetailPrtDaoCls PtDetailPrtDao;

        /// <summary>
        /// マニ明細更新Dao
        /// </summary>
        private PtDetailDaoCls PtDetailDao;

        /// <summary>
        /// マニ印字_建廃_形状更新Dao
        /// </summary>
        private PtKeijyouDaoCls PtKeijyouDao;

        /// <summary>
        /// マニ印字_建廃_荷姿更新Dao
        /// </summary>
        private PtNisugataDaoCls PtNisugataDao;

        /// <summary>
        /// マニ印字_建廃_処分方法更新Dao
        /// </summary>
        private PtHouhouDaoCls PtHouhouDao;

        /// <summary>
        /// 廃棄物区分検索Dao
        /// </summary>
        private IM_HAIKI_KBNDao GetHaikiKbnDao;

        /// <summary>
        /// 電子マニフェスト検索Dao
        /// </summary>
        private GetDenshiPatternDaoCls GetDenshiPatternDao;

        /// <summary>
        /// マニフェスト検索Dao
        /// </summary>
        private GetPatternDaoCls GetPatternDao;

        /// <summary>
        /// Form
        /// </summary>
        private ManifestPatternTouroku form;

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
        public ManifestPatternRegistLogic(ManifestPatternTouroku targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.PtEntryDao = DaoInitUtility.GetComponent<PtEntryDaoCls>();
            this.PtUpnDao = DaoInitUtility.GetComponent<PtUpnDaoCls>();
            this.PtPrtDao = DaoInitUtility.GetComponent<PtPrtDaoCls>();
            this.PtDetailPrtDao = DaoInitUtility.GetComponent<PtDetailPrtDaoCls>();
            this.PtKeijyouDao = DaoInitUtility.GetComponent<PtKeijyouDaoCls>();
            this.PtNisugataDao = DaoInitUtility.GetComponent<PtNisugataDaoCls>();
            this.PtHouhouDao = DaoInitUtility.GetComponent<PtHouhouDaoCls>();
            this.PtDetailDao = DaoInitUtility.GetComponent<PtDetailDaoCls>();
            this.GetHaikiKbnDao = DaoInitUtility.GetComponent<IM_HAIKI_KBNDao>();
            this.GetDenshiPatternDao = DaoInitUtility.GetComponent<GetDenshiPatternDaoCls>();
            this.GetPatternDao = DaoInitUtility.GetComponent<GetPatternDaoCls>();

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
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
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
                if (!this.form.PtEntrylist[0].HAIKI_KBN_CD.IsNull)
                {
                    M_HAIKI_KBN mHaikiKbnNm = GetHaikiKbnDao.GetDataByCd(this.form.PtEntrylist[0].HAIKI_KBN_CD.ToString());

                    if (mHaikiKbnNm != null)
                    {
                        sbTitle.Append(mHaikiKbnNm.HAIKI_KBN_NAME);
                    }
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

                if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                {
                    DataTable dt = new DataTable();
                    SerchParameterDtoCls serch = new SerchParameterDtoCls();
                    serch.SYSTEM_ID = this.form.PtEntrylist[0].SYSTEM_ID;
                    serch.SEQ = this.form.PtEntrylist[0].SEQ;

                    if (Properties.Settings.Default.dentaneMode.Equals((int)DENSHU_KBN.DENSHI_MANIFEST))
                    {
                        dt = GetDenshiPatternDao.GetDataForEntity(serch);
                    }
                    else
                    {
                        dt = GetPatternDao.GetDataForEntity(serch);
                    }

                    //データの存在場合
                    if (dt.Rows.Count > 0)
                    {
                        this.form.txt_PatternName.Text = dt.Rows[0][0].ToString();
                        this.form.txt_PatternNameKana.Text = dt.Rows[0][1].ToString();

                        this.delSeq = this.form.PtEntrylist[0].SEQ;
                        this.delSystemId = this.form.PtEntrylist[0].SYSTEM_ID;
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
                var sameNamePettern = PtEntryDao.SelectByName(this.form.PtEntrylist[0].LIST_REGIST_KBN, this.form.PtEntrylist[0].HAIKI_KBN_CD, this.form.PtEntrylist[0].FIRST_MANIFEST_KBN, this.form.txt_PatternName.Text, SqlBoolean.False, this.form.PtEntrylist[0].KYOTEN_CD);

                if (sameNamePettern.Length == 0)
                {
                    //重複無（新規 SEQ=1）
                    this.form.processKbn = PROCESS_KBN.NEW; //新規に強制変更
                }
                else if (!this.form.PtEntrylist[0].SYSTEM_ID.IsNull && sameNamePettern[0].SYSTEM_ID == this.form.PtEntrylist[0].SYSTEM_ID)
                {
                    //重複ありで、自分自身（確認＋削除フラグ＋新規（SEQ+1)）
                    this.form.processKbn = PROCESS_KBN.UPDATE; //更新に変更

                    //削除情報登録
                    this.form.PtEntrylist[0].SYSTEM_ID = sameNamePettern[0].SYSTEM_ID;
                    this.form.PtEntrylist[0].SEQ = sameNamePettern[0].SEQ;
                    this.form.PtEntrylist[0].USE_DEFAULT_KBN = this.form.PtEntrylist[0].USE_DEFAULT_KBN;

                    this.delSystemId = sameNamePettern[0].SYSTEM_ID;
                    this.delSeq = sameNamePettern[0].SEQ;
                    this.delTimesTamp = sameNamePettern[0].TIME_STAMP;

                }
                else
                {
                    //重複ありで、自分でない（確認＋削除フラグ＋新規（SEQ+1)）
                    this.form.processKbn = PROCESS_KBN.UPDATE; //更新に変更
                    this.form.PtEntrylist[0].USE_DEFAULT_KBN = false;

                    //削除情報登録
                    this.form.PtEntrylist[0].SYSTEM_ID = sameNamePettern[0].SYSTEM_ID;
                    this.form.PtEntrylist[0].SEQ = sameNamePettern[0].SEQ;

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

                List<T_MANIFEST_PT_ENTRY> ptEntrylist = this.form.PtEntrylist;
                List<T_MANIFEST_PT_UPN> ptUpnlist = this.form.PtUpnlist;
                List<T_MANIFEST_PT_PRT> ptPrtlist = this.form.PtPrtlist;
                List<T_MANIFEST_PT_DETAIL_PRT> ptDetailprtlist = this.form.PtDetailprtlist;
                List<T_MANIFEST_PT_DETAIL> ptDetaillist = this.form.PtDetaillist;
                List<T_MANIFEST_PT_KP_KEIJYOU> ptKeijyoulist = this.form.PtKeijyoulist;
                List<T_MANIFEST_PT_KP_NISUGATA> ptNiugatalist = this.form.PtNiugatalist;
                List<T_MANIFEST_PT_KP_SBN_HOUHOU> ptHouhoulist = this.form.PtHouhoulist;

                //登録データ作成
                this.MakePtData(ref ptEntrylist, ref ptUpnlist, ref ptPrtlist, ref ptDetailprtlist, ref ptDetaillist,
                    ref ptKeijyoulist, ref ptNiugatalist, ref ptHouhoulist);

                //データ更新
                using (Transaction tran = new Transaction())
                {
                    if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                    {
                        //論理削除処理
                        this.LogicalEntityDel();
                    }

                    //マニフェスト
                    if (ptEntrylist != null && ptEntrylist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_ENTRY entry in ptEntrylist)
                        {
                            count = PtEntryDao.Insert(entry);
                        }
                    }

                    //マニ収集運搬
                    if (ptUpnlist != null && ptUpnlist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_UPN upn in ptUpnlist)
                        {
                            count = PtUpnDao.Insert(upn);
                        }
                    }

                    //マニ印字
                    if (ptPrtlist != null && ptPrtlist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_PRT prt in ptPrtlist)
                        {
                            count = PtPrtDao.Insert(prt);
                        }
                    }

                    //マニ印字明細
                    if (ptDetailprtlist != null && ptDetailprtlist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_DETAIL_PRT detailprt in ptDetailprtlist)
                        {
                            count = PtDetailPrtDao.Insert(detailprt);
                        }
                    }


                    //マニ印字_建廃_形状
                    if (ptKeijyoulist != null && ptKeijyoulist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_KP_KEIJYOU keijyou in ptKeijyoulist)
                        {
                            count = PtKeijyouDao.Insert(keijyou);
                        }
                    }

                    //マニ印字_建廃_荷姿
                    if (ptNiugatalist != null && ptNiugatalist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_KP_NISUGATA niugata in ptNiugatalist)
                        {
                            count = PtNisugataDao.Insert(niugata);
                        }
                    }

                    //マニ印字_建廃_処分方法
                    if (ptHouhoulist != null && ptHouhoulist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_KP_SBN_HOUHOU houhou in ptHouhoulist)
                        {
                            count = PtHouhouDao.Insert(houhou);
                        }
                    }

                    //マニ明細
                    if (ptDetaillist != null && ptDetaillist.Count() > 0)
                    {
                        foreach (T_MANIFEST_PT_DETAIL detail in ptDetaillist)
                        {
                            count = PtDetailDao.Insert(detail);
                        }
                    }

                    tran.Commit();

                    //登録結果をフォームに
                    this.form.RegistedSystemId = ptEntrylist[0].SYSTEM_ID;
                    this.form.RegistedSeq = ptEntrylist[0].SEQ;

                }

                //更新後は完了メッセージを表示する
                msgLogic.MessageBoxShow("I001", "登録");

                //自動で閉じる
                this.form.DialogResult = DialogResult.OK;
                this.form.Close();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
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
        public void MakePtData(ref List<T_MANIFEST_PT_ENTRY> entrylist,
                            ref List<T_MANIFEST_PT_UPN> upnlist,
                            ref List<T_MANIFEST_PT_PRT> prtlist,
                            ref List<T_MANIFEST_PT_DETAIL_PRT> detailprtlist,
                            ref List<T_MANIFEST_PT_DETAIL> detaillist,
                            ref List<T_MANIFEST_PT_KP_KEIJYOU> keijyoulist,
                            ref List<T_MANIFEST_PT_KP_NISUGATA> niugatalist,
                            ref List<T_MANIFEST_PT_KP_SBN_HOUHOU> houhoulist)
        {
            LogUtility.DebugMethodStart(entrylist, upnlist, prtlist, prtlist, detaillist, keijyoulist, niugatalist, houhoulist);

            SqlInt64 lSysId = 0;
            SqlInt32 iSeq = 0;
            SqlBoolean iUseDefalt = false;

            Common.BusinessCommon.DBAccessor dba = null;
            dba = new Common.BusinessCommon.DBAccessor();

            try
            {
                if (this.form.processKbn.Equals(PROCESS_KBN.UPDATE))
                {
                    //更新
                    lSysId = entrylist[0].SYSTEM_ID;
                    iSeq = entrylist[0].SEQ;
                    iUseDefalt = entrylist[0].USE_DEFAULT_KBN;
                }
                else
                {
                    //新規登録：採番処理
                    if (Properties.Settings.Default.dentaneMode.Equals((int)DENSHU_KBN.DENSHI_MANIFEST))
                    {
                        lSysId = (long)dba.createSystemId((int)DENSHU_KBN.DENSHI_MANIFEST);
                    }
                    else
                    {
                        lSysId = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                    }

                }

                iSeq = iSeq + 1;

                //マニフェストパターン
                if (entrylist != null && entrylist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_ENTRY entry in entrylist)
                    {
                        //システムID
                        entry.SYSTEM_ID = lSysId;
                        //枝番
                        entry.SEQ = iSeq;
                        //パターン名
                        entry.PATTERN_NAME = this.form.txt_PatternName.Text;
                        //パターンふりがな
                        entry.PATTERN_FURIGANA = this.form.txt_PatternNameKana.Text;
                        // 規定値
                        entry.USE_DEFAULT_KBN = iUseDefalt;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(entry);
                        dataBinderEntry1.SetSystemProperty(entry, false);

                        //登録しない項目
                        // 20140610 syunrei EV004723_建廃マニ入力において、パターンを登録する際に事前協議及び日付が登録されない start
                        ////事前協議番号
                        //entry.JIZEN_NUMBER = null;
                        ////事前協議年月日
                        //entry.JIZEN_DATE = SqlDateTime.Null;
                        // 20140610 syunrei EV004723_建廃マニ入力において、パターンを登録する際に事前協議及び日付が登録されない end
                        //交付年月日
                        entry.KOUFU_DATE = SqlDateTime.Null;
                        //交付番号
                        entry.MANIFEST_ID = null;
                        //処分受領日
                        entry.SBN_JYURYOU_DATE = SqlDateTime.Null;
                        //照合確認B1票
                        entry.CHECK_B1 = SqlDateTime.Null;
                        //照合確認B2票
                        entry.CHECK_B2 = SqlDateTime.Null;
                        //照合確認B4票
                        entry.CHECK_B4 = SqlDateTime.Null;
                        //照合確認B6票
                        entry.CHECK_B6 = SqlDateTime.Null;
                        //照合確認D票
                        entry.CHECK_D = SqlDateTime.Null;
                        //照合確認E票
                        entry.CHECK_E = SqlDateTime.Null;
                        //連携伝種区分CD
                        entry.RENKEI_DENSHU_KBN_CD = SqlInt16.Null;
                        //連携システムID
                        entry.RENKEI_SYSTEM_ID = SqlInt64.Null;
                        //連携明細システムID
                        entry.RENKEI_MEISAI_SYSTEM_ID = SqlInt64.Null;

                    }
                }

                //マニフェストパターン収集運搬(T_MANIFEST_PT_UPN)データ作成
                if (upnlist != null && upnlist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_UPN upn in upnlist)
                    {
                        //システムID
                        upn.SYSTEM_ID = lSysId;
                        //枝番
                        upn.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_UPN>(upn);
                        dataBinderEntry1.SetSystemProperty(upn, false);

                        //登録しない項目
                        //運搬終了年月日
                        upn.UPN_END_DATE = SqlDateTime.Null;
                    }
                }

                //マニフェストパターン印字(T_MANIFEST_PT_PRT)データ作成
                if (prtlist != null && prtlist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_PRT prt in prtlist)
                    {
                        //システムID
                        prt.SYSTEM_ID = lSysId;
                        //枝番
                        prt.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_PRT>(prt);
                        dataBinderEntry1.SetSystemProperty(prt, false);
                    }
                }

                //マニフェストパターン印字明細(T_MANIFEST_PT_DETAIL_PRT)データ作成
                if (detailprtlist != null && detailprtlist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_DETAIL_PRT detailprt in detailprtlist)
                    {
                        //システムID
                        detailprt.SYSTEM_ID = lSysId;
                        //枝番
                        detailprt.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_DETAIL_PRT>(detailprt);
                        dataBinderEntry1.SetSystemProperty(detailprt, false);

                    }
                }


                long detailsystemid = 0;

                //マニフェストパターン明細(T_MANIFEST_PT_DETAIL)データ作成
                if (detaillist != null && detaillist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_DETAIL detail in detaillist)
                    {
                        //システムID
                        detail.SYSTEM_ID = lSysId;
                        //枝番
                        detail.SEQ = iSeq;

                        //採番処理
                        if (Properties.Settings.Default.dentaneMode.Equals((int)DENSHU_KBN.DENSHI_MANIFEST))
                        {
                            detailsystemid = (long)dba.createSystemId((int)DENSHU_KBN.DENSHI_MANIFEST);
                        }
                        else
                        {
                            detailsystemid = (long)dba.createSystemId((int)DENSHU_KBN.KAMI_MANIFEST);
                        }

                        detail.DETAIL_SYSTEM_ID = detailsystemid;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_DETAIL>(detail);
                        dataBinderEntry1.SetSystemProperty(detail, false);

                        //登録しない項目
                        //処分終了日
                        detail.SBN_END_DATE = SqlDateTime.Null;
                        //最終処分終了日
                        detail.LAST_SBN_END_DATE = SqlDateTime.Null;

                    }
                }

                //マニパターン_印字_建廃_形状(T_MANIFEST_PT_KP_KEIJYOU)
                if (keijyoulist != null && keijyoulist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_KP_KEIJYOU keijyou in keijyoulist)
                    {
                        //システムID
                        keijyou.SYSTEM_ID = lSysId;
                        //枝番
                        keijyou.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_KP_KEIJYOU>(keijyou);
                        dataBinderEntry1.SetSystemProperty(keijyou, false);
                    }
                }

                //マニパターン印字_建廃_荷姿(T_MANIFEST_PT_KP_NISUGATA)
                if (niugatalist != null && niugatalist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_KP_NISUGATA niugata in niugatalist)
                    {
                        //システムID
                        niugata.SYSTEM_ID = lSysId;
                        //枝番
                        niugata.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_KP_NISUGATA>(niugata);
                        dataBinderEntry1.SetSystemProperty(niugata, false);

                    }
                }

                //マニパターン印字_建廃_処分方法(T_MANIFEST_PT_KP_SBN_HOUHOU)
                if (houhoulist != null && houhoulist.Count() > 0)
                {
                    foreach (T_MANIFEST_PT_KP_SBN_HOUHOU houhou in houhoulist)
                    {
                        //システムID
                        houhou.SYSTEM_ID = lSysId;
                        //枝番
                        houhou.SEQ = iSeq;

                        var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_KP_SBN_HOUHOU>(houhou);
                        dataBinderEntry1.SetSystemProperty(houhou, false);

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
            LogUtility.DebugMethodEnd(entrylist, upnlist, prtlist, prtlist, detaillist, keijyoulist, niugatalist, houhoulist);
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
                T_MANIFEST_PT_ENTRY del = new T_MANIFEST_PT_ENTRY();
                del.SYSTEM_ID = this.delSystemId;
                del.SEQ = this.delSeq;
                del.DELETE_FLG = true;
                del.TIME_STAMP = this.delTimesTamp;

                var dataBinderEntry1 = new DataBinderLogic<T_MANIFEST_PT_ENTRY>(del);
                dataBinderEntry1.SetSystemProperty(del, true);

                count = PtEntryDao.Update(del);
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
        /// 画面クリア
        /// </summary>
        public void ClearScreen(String Kbn)
        {
            LogUtility.DebugMethodStart();

            try
            {
                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //フリガナ
                        this.form.txt_PatternNameKana.Text = string.Empty;

                        //パターン名
                        this.form.txt_PatternName.Text = string.Empty;

                        //タイトル
                        this.form.lbl_title.Text = string.Empty;

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
