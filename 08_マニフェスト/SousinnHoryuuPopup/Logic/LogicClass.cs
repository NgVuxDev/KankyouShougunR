using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.PaperManifest.SousinnHoryuuPopup
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 最終処分保留
        /// </summary>
        List<T_LAST_SBN_SUSPEND> lastSbnSusPendList;

        /// <summary>
        /// キュー情報
        /// </summary>
        List<QUE_INFO> queList;

        /// <summary>
        /// D12 2次マニフェスト情報
        /// </summary>
        List<DT_D12> manifastList;

        /// <summary>
        /// D13 最終処分終了日・事業場情報
        /// </summary>
        List<DT_D13> jigyoubaList;

        /// <summary>
        /// マニフェスト目次情報
        /// </summary>
        List<DT_MF_TOC> mokujiList;
        
        /// <summary>
        /// 実行区分
        /// </summary>
        string  pageExeKbn;
        
        /// <summary>
        /// 最終処分保留DAO
        /// </summary>
        private GetlastSbnSusPendDaoCls lastSbnSusPendDao;

        /// <summary>
        /// キュー情報DAO
        /// </summary>
        private GetQueDaoCls queDao;

        /// <summary>
        /// D12 2次マニフェスト情報DAO
        /// </summary>
        private GetmanifastDaoCls manifastDao;

        /// <summary>
        /// D13 最終処分終了日・事業場情報DAO
        /// </summary>
        private GetjigyoubaDaoCls jigyoubaDao;

        /// <summary>
        /// マニフェスト目次情報DAO
        /// </summary>
        private GetmokujiDaoCls mokujiDao;

        /// <summary>
        /// 電子マニフェストDao
        /// </summary>
        private GeElecManiDaoCls elecManiDao;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm, object[] targetList)
        {
            LogUtility.DebugMethodStart(targetForm, targetList);

            // Formをlogicに設定する
            this.form = targetForm;
            // 最終処分保留
            lastSbnSusPendList = targetList[0] as List<T_LAST_SBN_SUSPEND>;
            // キュー情報
            queList = targetList[1] as List<QUE_INFO>;
            // D12 2次マニフェスト情報
            manifastList = targetList[2] as List<DT_D12>;
            // D13 最終処分終了日・事業場情報
            jigyoubaList = targetList[3] as List<DT_D13>;
            // マニフェスト目次情報
            mokujiList = targetList[4] as List<DT_MF_TOC>;
            // 実行区分
            pageExeKbn = targetList[5] as string;
            // 最終処分終了報告の場合
            if ("1".Equals(pageExeKbn))
            {
                this.form.Text = "電子マニフェスト（１次）最終処分終了報告";
            }
            else
            {
                this.form.Text = "電子マニフェスト（１次）最終処分終了報告の取消";
            }
            this.form.lb_title.Text = this.form.Text;
            // Dao作成
            lastSbnSusPendDao = DaoInitUtility.GetComponent<GetlastSbnSusPendDaoCls>();
            queDao = DaoInitUtility.GetComponent<GetQueDaoCls>();
            manifastDao = DaoInitUtility.GetComponent<GetmanifastDaoCls>();
            jigyoubaDao = DaoInitUtility.GetComponent<GetjigyoubaDaoCls>();
            mokujiDao = DaoInitUtility.GetComponent<GetmokujiDaoCls>();
            elecManiDao = DaoInitUtility.GetComponent<GeElecManiDaoCls>();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm, targetList);
        }

        /// <summary>
        /// 一覧に表示するデータをセットする
        /// jigyoubaListから必要なデータを取得し表示する
        /// </summary>
        internal void SetDisplayData()
        {
            try
            {
                this.form.customDataGridView1.Rows.Clear();

                if (this.queList == null
                    || queList.Count < 1)
                {
                    return;
                }

                List<string> kanriIdList = new List<string>();

                // WHERE句作成
                foreach (QUE_INFO queInfo in this.queList)
                {
                    if (!kanriIdList.Contains(queInfo.KANRI_ID))
                    {
                        kanriIdList.Add(queInfo.KANRI_ID);
                    }
                }

                if (kanriIdList.Count > 0)
                {
                    this.form.customDataGridView1.DataSource = elecManiDao.GetElecManiData(kanriIdList);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDisplayData", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// JWNET送信処理
        /// </summary>
        [Transaction]
        public void JWNETsend()
        {
            LogUtility.DebugMethodStart();

            // DB登録処理
            this.dbInsert(pageExeKbn,"1");

            LogUtility.DebugMethodEnd();
        }
        
        /// <summary>
        /// 保留保存処理
        /// </summary>
        [Transaction]
        public void saveSyori()
        {
            LogUtility.DebugMethodStart();

            // DB登録処理
            this.dbInsert(pageExeKbn, "2");

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キュー情報レコード最大枝番検索
        /// </summary>
        private SqlInt16 GetQueSeq(string kanriId, GetQueDaoCls dao)
        {
            SqlInt16 seq = 0;
            try
            {
                // レコード枝番
                DataTable dtSeq = new DataTable();
                GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                search.kanriId = kanriId;
                dtSeq = dao.GetMaxSeq(search);
                //seqを設定する
                if (dtSeq != null && dtSeq.Rows.Count > 0)
                {
                    seq = Convert.ToInt16(dtSeq.Rows[0][0]);
                }
                seq += 1;
                // 取得した連番を返す
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            return seq;
        }

        /// <summary>
        /// D12 2次マニフェスト情報レコード最大枝番検索
        /// </summary>
        private SqlDecimal GetmanifastSeq(string kanriId, GetmanifastDaoCls dao)
        {
            SqlDecimal seq = 1;
            try
            {
                // レコード枝番
                DataTable dtSeq = new DataTable();
                GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                search.kanriId = kanriId;
                dtSeq = dao.GetMaxSeq(search);
                //seqを設定する
                if (dtSeq != null && dtSeq.Rows.Count > 0)
                {
                    seq = Convert.ToInt16(dtSeq.Rows[0][0]) + 1; 

                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            // 取得した連番を返す
            return seq;
        }

        /// <summary>
        /// D13 最終処分終了日・事業場情報レコード最大枝番検索
        /// </summary>
        private SqlInt16 GetjigyoubaSeq(string kanriId, GetjigyoubaDaoCls dao)
        {
            SqlInt16 seq = 0;
            try
            {
                // レコード枝番
                DataTable dtSeq = new DataTable();
                GetMaxSeqDtoCls search = new GetMaxSeqDtoCls();
                search.kanriId = kanriId;
                dtSeq = dao.GetMaxSeq(search);
                //seqを設定する
                if (dtSeq != null && dtSeq.Rows.Count > 0)
                {
                    seq = Convert.ToInt16(dtSeq.Rows[0][0]);
                }
                seq += 1;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            // 取得した連番を返す
            return seq;
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        public void windowClose()
        {
            LogUtility.DebugMethodStart();

            form.Close();

            LogUtility.DebugMethodEnd();
        }
        
        /// <summary>
        /// DB登録処理
        /// <param name="exeKbn">実行区分（１：最終処分終了報告、2：最終処分終了報告の取消）</param>
        /// <param name="btnKbn">ボタン押下区分（１：JWNET送信、2：保留保存）</param>
        /// </summary>
        private void dbInsert(string exeKbn, string btnKbn)
        {
            LogUtility.DebugMethodStart(exeKbn, btnKbn);

            List<string> kanriIds = new List<string>();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // 最終処分保留
                    if (lastSbnSusPendList != null && queList.Count() > 0 && btnKbn.Equals("2"))
                    {
                        foreach (T_LAST_SBN_SUSPEND lastSbnSusPend in lastSbnSusPendList)
                        {
                            if (lastSbnSusPend.TIME_STAMP == null)
                            {
                                // DBへ登録する
                                lastSbnSusPendDao.Insert(lastSbnSusPend);
                            }
                            else
                            { 
                                // DBへ更新する
                                lastSbnSusPendDao.Update(lastSbnSusPend);
                            }
                        }
                    }

                    // キュー情報
                    if (queList != null && queList.Count() > 0)
                    {
                        foreach (QUE_INFO que in queList)
                        {
                            // D12, D13のゴミデータ削除用にKANRI_IDをセット
                            kanriIds.Add(que.KANRI_ID);

                            // レコード枝番
                            que.QUE_SEQ = this.GetQueSeq(que.KANRI_ID, this.queDao);
                            // キュー状態フラグ
                            if ("2".Equals(btnKbn))
                            {
                                // 保留保存の場合
                                que.STATUS_FLAG = 7;
                            }
                            // DBへ登録する
                            queDao.Insert(que);
                        }
                    }

                    // D12, D13に登録されているデータがそのままJWNETへ送信されるため
                    // ゴミが残っている場合は削除する
                    foreach (var kanriId in kanriIds)
                    {
                        // D12削除
                        var d12s = this.manifastDao.GetD12(kanriId);
                        foreach(var d12 in d12s)
                        {
                            this.manifastDao.Delete(d12);
                        }

                        // D13削除
                        var d13s = this.jigyoubaDao.GetD13(kanriId);
                        foreach (var d13 in d13s)
                        {
                            this.jigyoubaDao.Delete(d13);
                        }
                    }

                    // D12 2次マニフェスト情報
                    // D13 最終処分終了日・事業場情報
                    for (int i = 0; i < manifastList.Count() && exeKbn.Equals("1"); i++)
                    {
                        // D12 2次マニフェスト情報
                        DT_D12 manifast = manifastList[i];
                        // レコード枝番
                        manifast.D12_SEQ = this.GetmanifastSeq(manifast.KANRI_ID, this.manifastDao);
                        // DBへ登録する
                        manifastDao.Insert(manifast);

                        // D13 最終処分終了日・事業場情報
                        DT_D13 jigyouba = jigyoubaList[i];
                        // レコード枝番
                        jigyouba.D12_SEQ = SqlInt16.Parse(Convert.ToString(manifast.D12_SEQ));
                        // レコード枝番
                        jigyouba.D13_SEQ = this.GetjigyoubaSeq(jigyouba.KANRI_ID, this.jigyoubaDao);
                        // DBへ登録する
                        jigyoubaDao.Insert(jigyouba);
                      
                    }

                    // マニフェスト目次情報
                    if (mokujiList != null && mokujiList.Count() > 0)
                    {
                        foreach (DT_MF_TOC mokuji in mokujiList)
                        {
                            // 保留の場合はSTATUS_DTAIL=0にする。JWNET送信時に1にする。
                            if (btnKbn.Equals("2"))
                            {
                                mokuji.STATUS_DETAIL = 0;
                            }
                            // DB更新
                            mokujiDao.Update(mokuji);
                        }
                    }

                    tran.Commit();
                }
                // 登録成功
                form.messageShowLogic.MessageBoxShow("I001", "登録");
                form.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                // 登録失敗
                form.messageShowLogic.MessageBoxShow("I007", "登録");
            }

            LogUtility.DebugMethodEnd(exeKbn, btnKbn);
        }


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
    }
}
