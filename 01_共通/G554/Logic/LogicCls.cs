using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.FormManager;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.DenpyouhimozukePatternIchiran.Const;
using Shougun.Core.Common.DenpyouhimozukePatternIchiran.DAO;

namespace Shougun.Core.Common.DenpyouhimozukePatternIchiran
{
    /// <summary>
    /// ビジネスロジッククラス
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOCls SearchString { get; set; }

        /// <summary>
        /// デフォルトとして使用しているユーザの確認
        /// </summary>
        public DTOCls DefaultSearch { get; set; }

        /// <summary>
        /// 更新データ：パターン一覧
        /// </summary>
        public List<M_OUTPUT_PATTERN_HIMO> MopList { get; set; }

        /// <summary>
        /// 更新データ：パターン（個別）
        /// </summary>
        public List<M_OUTPUT_PATTERN_KOBETSU_HIMO> MopkList { get; set; }

        /// <summary>
        /// 新規データ：パターン（個別）
        /// </summary>
        public List<M_OUTPUT_PATTERN_KOBETSU_HIMO> MopkListInsert { get; set; }

        /// <summary>
        /// 削除データ：パターン（個別）
        /// 更新前のロジック削除用
        /// </summary>
        public List<M_OUTPUT_PATTERN_KOBETSU_HIMO> MopkUpdDelLis { get; set; }

        #endregion

        #region フィールド
        /// <summary>
        /// パターン一覧のDao
        /// </summary>
        private MOPDaoCls MopDaoPatern;

        /// <summary>
        /// パターン一覧のDao
        /// </summary>
        private MOPKDaoCls MopkDaoPatern;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Common.DenpyouhimozukePatternIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// MyForm
        /// </summary>
        private UIForm Myform;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <returns></returns>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            try
            {
                this.Myform = targetForm;
                this.SearchString = new DTOCls();
                this.MopDaoPatern = DaoInitUtility.GetComponent<MOPDaoCls>();
                this.MopkDaoPatern = DaoInitUtility.GetComponent<MOPKDaoCls>();

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                // 検索条件を設定
                this.Myform.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_txt;
                // 処理No制御
                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.Myform.Parent;
                this.parentForm.txb_process.Enabled = false;
                if (this.Search() > 0)
                {
                    this.SetIchiran();
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region F1適用ボタン処理
        /// <summary>
        /// 適用ボタン処理
        /// </summary>
        /// <returns>sysId</returns>
        internal void BottonTekiyou()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (this.Myform.dgvDenpyouhimozuke.RowCount > 0)
                {
                    this.Myform.ParamOut_SysID = this.Myform.dgvDenpyouhimozuke.Rows[this.Myform.dgvDenpyouhimozuke.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString();

                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region F4削除ボタン処理
        /// <summary>
        /// 削除ボタン処理
        /// </summary>
        /// <returns></returns>
        internal void BottonDelete()
        {
            LogUtility.DebugMethodStart();
            try
            {
                Boolean okOrNo = this.ChkBefUpdOrDel("del");
                if (okOrNo)
                {
                    //ロジック削除
                    Boolean isOK = this.LogicalDel();

                    if (isOK)
                    {
                        //再検索
                        this.Search();
                        this.SetIchiran();

                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("I001", ConstCls.DelInfo);
                    }
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 論理削除処理
        /// <summary>
        /// 論理削除処理
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public Boolean LogicalDel()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //削除データ取得
                this.GetMeisaiIchiranData("LgcDel");

                //削除実行
                if (MopList != null && MopkList != null && MopList.Count() > 0 && MopkList.Count() > 0)
                {
                    using (Transaction tran = new Transaction())
                    { //トランザクション開始

                        //排他チェック
                        for (int i = 0; i < MopList.Count; i++)
                        {
                            //データを取得
                            M_OUTPUT_PATTERN_HIMO mop = MopList[i];
                            //チェックを実行する
                            Boolean chkFlg = this.ChkBefDel(mop);

                            //エラーメッセージ出る
                            if (!chkFlg)
                            {
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E035");
                                return false;
                            }

                            //削除実行
                            int CntMopUpd = MopDaoPatern.Update(mop);
                        }

                        foreach (M_OUTPUT_PATTERN_KOBETSU_HIMO mopk in MopkList)
                        {
                            if (!String.IsNullOrEmpty(mopk.SHAIN_CD) && mopk.SYSTEM_ID != SqlInt64.Zero && mopk.SEQ != SqlInt32.Zero)
                            {
                                int CntMopkUpd = MopkDaoPatern.Update(mopk);
                            }
                        }

                        tran.Commit();
                    }//トランザクション終了（未コミットの場合ロールバック）
                }
            }
            catch (Exception ex)
            {
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 登録/更新取得処理
        /// <summary>
        /// 登録／更新取得処理
        /// </summary>
        /// <returns></returns>
        public void UpdRegist()
        {
            LogUtility.DebugMethodStart();

            //チェックする。チェック区分：更新
            Boolean okOrNo = this.ChkBefUpdOrDel("upd");

            //チェック結果によって更新実行
            if (okOrNo)
            {
                //更新
                this.UpdAll();

                //再検索
                this.Search();
                this.SetIchiran();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns>count</returns>
        public virtual int Search()
        {
            LogUtility.DebugMethodStart();

            this.SearchResult = new DataTable();
            this.SearchString.Patern_Name = this.Myform.CONDITION_VALUE.Text;
            this.SearchString.Shain_Cd = Properties.Settings.Default.ParamIn_ShaInCd;
            //this.SearchString.Denshu_Kbn_Cd = Properties.Settings.Default.ParamIn_DenshuKb;
            try
            {
                this.SearchResult = MopDaoPatern.GetDataForEntity(this.SearchString);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //例外はここで処理
                throw;
            }

            int cnt = this.SearchResult.Rows.Count;
            Properties.Settings.Default.ConditionValue_txt = this.Myform.CONDITION_VALUE.Text;
            Properties.Settings.Default.Save();

            if (cnt == 0)
            {
                MessageBox.Show(ConstCls.SearchEmptInfo, ConstCls.DialogTitle);
            }

            LogUtility.DebugMethodEnd(cnt);
            return cnt;
        }
        #endregion

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public void UpdAll()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //更新データを取得
                this.GetMeisaiIchiranData("UPD");

                //更新／登録実行
                if (MopList != null && (MopkList != null || MopkListInsert != null) && MopList.Count() > 0 && (MopkList.Count() > 0 || MopkListInsert.Count() > 0))
                {
                    using (Transaction tran = new Transaction())
                    { //トランザクション開始

                        //更新時は M_OUTPUT_PATTERNはUPDATEしない（別の社員が並行してメンテされると楽観排他が多発するため）
                        ////パターン一覧更新
                        //foreach (M_OUTPUT_PATTERN mop in MopList)
                        //{
                        //    int CntMopUpd = MopDaoPatern.Update(mop);
                        //}

                        //M_OUTPUT_PATTERNが削除されていないかのチェックは事前行う
                        foreach (M_OUTPUT_PATTERN_HIMO mop in MopList)
                        {
                            DataTable dt = MopDaoPatern.CheckExistRecord(mop.SYSTEM_ID,false);
                            if (dt.Rows.Count == 0)
                            {
                                //排他エラー発生
                                LogUtility.Warn("排他発生。画面表示後にM_OUTPUT_PATTERN_HIMOが削除された。：" + mop.SYSTEM_ID + ":" + mop.PATTERN_NAME );
                                //MessageBox.Show(ConstCls.ErrStop7, ConstCls.DialogTitleErr, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                var msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E080");
                                return;
                            }
                        }

                        //個別論理削除
                        foreach (M_OUTPUT_PATTERN_KOBETSU_HIMO mopk in MopkUpdDelLis)
                        {
                            int CntMopkUpd = MopkDaoPatern.Update(mopk);
                        }

                        //個別新規登録
                        foreach (M_OUTPUT_PATTERN_KOBETSU_HIMO mopkIsrt in MopkListInsert)
                        {
                            int CntMopkUpd = MopkDaoPatern.Insert(mopkIsrt);
                        }

                        //トランザクション終了（未コミットの場合ロールバック）
                        tran.Commit();
                    }

                    //更新Successメッセージ
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("I001", ConstCls.UpdInfo);
                }
                LogUtility.DebugMethodEnd();

            }
            catch (Exception ex)
            {

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
            }

        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        /// <returns></returns>
        internal void EventInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var ParentForm = (BusinessBaseForm)this.Myform.Parent;

                //適用ボタン(F1)イベント生成
                ParentForm.bt_func1.Click += new EventHandler(this.Myform.FormTekiyou);

                //新規ボタン(F2)イベント生成
                ParentForm.bt_func2.Click += new EventHandler(this.Myform.NewAdd);

                //削除ボタン(F4)イベント生成
                this.Myform.C_Regist(ParentForm.bt_func4);
                ParentForm.bt_func4.Click += new EventHandler(this.Myform.LogicalDelete);
                ParentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

                //検索ボタン(F8)イベント生成
                ParentForm.bt_func8.Click += new EventHandler(this.Myform.Search);

                //登録ボタン(F9)イベント生成
                this.Myform.C_Regist(ParentForm.bt_func9);
                ParentForm.bt_func9.Click += new EventHandler(this.Myform.Regist);
                ParentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //閉じるボタン(F12)イベント生成
                ParentForm.bt_func12.Click += new EventHandler(this.Myform.FormClose);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        /// <returns></returns>
        private void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            try
            {
                DTOCls entity = new DTOCls();
                if (!string.IsNullOrEmpty(this.Myform.CONDITION_VALUE.Text))
                {
                    entity.Patern_Name = this.Myform.CONDITION_VALUE.Text;
                }
                this.SearchString = entity;

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        /// <returns></returns>
        public void Cancel()
        {
            LogUtility.DebugMethodStart();

            try
            {
                ClearCondition();
                SetSearchString();

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 検索条件初期化
        /// </summary>
        /// <returns></returns>
        public void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            try
            {
                Properties.Settings.Default.ParamIn_ShaInCd = null;
                Properties.Settings.Default.ParamIn_DenshuKb = null;
                Properties.Settings.Default.ConditionValue_txt = null;
                Properties.Settings.Default.Save();

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 登録、削除前のチェック
        /// <summary>
        /// 登録、論理削除前のチェック
        /// ①削除の場合：削除ボックスがチェックされるのか
        /// 　　　　　　　削除行はディフォルト行のか
        /// ②更新の場合：表示区分重複あるのか
        /// </summary>
        /// <returns></returns>
        public Boolean ChkBefUpdOrDel(String kbn)
        {
            LogUtility.DebugMethodStart(kbn);

            try
            {
                Boolean haveDelChk = false;
                Boolean haveDefltChk = false;
                for (int i = 0; i < this.Myform.dgvDenpyouhimozuke.RowCount; i++)
                {
                    //削除チェックボックスとディフォルトチェックボックス
                    String delChk = ((DataGridViewCheckBoxCell)this.Myform.dgvDenpyouhimozuke[0, i]).Value.ToString().ToLower();
                    String defltChk = ((DataGridViewCheckBoxCell)this.Myform.dgvDenpyouhimozuke[1, i]).Value.ToString().ToLower();

                    //削除行あるのか記録
                    if ("true".Equals(delChk.ToLower()))
                    {
                        haveDelChk = true;

                        if ("upd".Equals(kbn))
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("I007", "削除チェックされているレコードがあるため登録");
                            return false;
                        }

                    }

                    //ディフォルト行あるのか記録
                    if ("true".Equals(defltChk.ToLower()))
                    {
                        //ディフォルト行がもう既に存在してる場合
                        if (haveDefltChk)
                        {
                            MessageBox.Show("デフォルト行は一行しか選択できません。修正してください。", ConstCls.DialogTitle);
                            return false;
                        }
                        haveDefltChk = true;
                    }

                    //削除チェックボックスとディフォルトチェックボックス制御
                    if (delChk.Equals(defltChk) && "true".Equals(delChk.ToLower()))
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E036");
                        return false;
                    }

                    //表示順序重複
                    String dispNum1 = "";
                    String dispNum2 = "";
                    if (((DataGridViewTextBoxCell)this.Myform.dgvDenpyouhimozuke[2, i]).Value != null)
                    {
                        dispNum1 = ((DataGridViewTextBoxCell)this.Myform.dgvDenpyouhimozuke[2, i]).Value.ToString();
                    }

                    for (int j = i + 1; j < this.Myform.dgvDenpyouhimozuke.RowCount; j++)
                    {
                        if (((DataGridViewTextBoxCell)this.Myform.dgvDenpyouhimozuke[2, j]).Value != null)
                        {
                            dispNum2 = ((DataGridViewTextBoxCell)this.Myform.dgvDenpyouhimozuke[2, j]).Value.ToString();
                        }
                        if (dispNum1.Equals(dispNum2) && !"".Equals(dispNum1))
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E034", ConstCls.ErrStop4);
                            return false;
                        }
                        if (String.Compare(dispNum1, "5") > 0 || String.Compare(dispNum2, "5") > 0 || "0".Equals(dispNum1) || "0".Equals(dispNum2))
                        {
                            MessageBox.Show("表示区分は１～５間の数字で入力してください。", ConstCls.DialogTitle);
                            return false;
                        }
                    }

                    //表示区分入力しないレコードをディフォルトとしてチェックされた時、エラー出る
                    if ("true".Equals(defltChk.ToLower()) && String.Empty.Equals(dispNum1))
                    {
                        MessageBox.Show("表示区分は１～５間のレコードのみディフォルトチェックしてください。", ConstCls.DialogTitle);
                        return false;
                    }

                }


                //削除チェックボックスを選択しない場合
                if (!haveDelChk && "del".Equals(kbn))
                {
                    MessageBox.Show(ConstCls.ErrStop3, ConstCls.DialogTitle);
                    return false;
                }

                //ディフォルトチェックボックスを選択しない場合
                if (!haveDefltChk && "upd".Equals(kbn))
                {
                    MessageBox.Show(ConstCls.ErrStop5, ConstCls.DialogTitle);
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 削除前チェック（排他チェック）
        /// <summary>
        /// 削除前のチェック（排他チェック）
        /// </summary>
        /// <returns></returns>
        public Boolean ChkBefDel(M_OUTPUT_PATTERN_HIMO mop)
        {
            //LogUtility.DebugMethodStart();
            try
            {

                if (0 < MopDaoPatern.GetDataDefaultEntity(mop.SYSTEM_ID))
                {
                    return false;
                }

                DataTable dt = new DataTable();
                //排他検索SQL
                String inSql = @"SELECT * FROM M_OUTPUT_PATTERN_KOBETSU 
                                 WHERE DEFAULT_KBN = 'TRUE' AND DELETE_FLG = 0 AND SYSTEM_ID = '" + mop.SYSTEM_ID+"' ";
                //検索ロジック
                dt = MopDaoPatern.GetDateForStringSql(inSql);

                //LogUtility.DebugMethodEnd();
                if (dt.Rows.Count > 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 検索結果を一覧に設定
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        /// <returns></returns>
        public void SetIchiran()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //前の結果をクリア
                int k = this.Myform.dgvDenpyouhimozuke.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.Myform.dgvDenpyouhimozuke.Rows.RemoveAt(this.Myform.dgvDenpyouhimozuke.Rows[i - 1].Index);
                }

                //検索結果を設定する
                var table = this.SearchResult;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //画面表示項目
                    this.Myform.dgvDenpyouhimozuke.Rows.Add();
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DELETE_FLG"].Value = table.Rows[i]["DELETE_FLG"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DEFAULT_KBN"].Value = table.Rows[i]["DEFAULT_KBN"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DEFAULT_KBN_DEL"].Value = table.Rows[i]["DEFAULT_KBN"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DISP_NUMBER"].Value = table.Rows[i]["DISP_NUMBER"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DISP_NUMBER_DEL"].Value = table.Rows[i]["DISP_NUMBER"];
                    //出力区分名：伝票・明細
                    String OUTPUT_KBN = table.Rows[i]["OUTPUT_KBN"].ToString();
                    switch (OUTPUT_KBN)
                    {
                        case "1":
                            this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["OUTPUT_KBN_NAME"].Value = ConstCls.OuptKbn_DenPyou;
                            break;
                        case "2":
                            this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["OUTPUT_KBN_NAME"].Value = ConstCls.OuptKbn_Meisai;
                            break;
                    }
                    //パターン名

                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["PATTERN_NAME"].Value = table.Rows[i]["PATTERN_NAME"];
                    //非表示項目
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["SYSTEM_ID_MOP"].Value = table.Rows[i]["SYSTEM_ID_MOP"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["SEQ_MOP"].Value = table.Rows[i]["SEQ_MOP"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["SYSTEM_ID_MOPK"].Value = table.Rows[i]["SYSTEM_ID_MOPK"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["SEQ_MOPK"].Value = table.Rows[i]["SEQ_MOPK"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["SHAIN_CD"].Value = table.Rows[i]["SHAIN_CD"];
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["OUTPUT_KBN"].Value = table.Rows[i]["OUTPUT_KBN"];
                    //this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DENSHU_KBN_CD"].Value = table.Rows[i]["DENSHU_KBN_CD"];
                    //TIME_STAMP_MOPを設定
                    if (String.IsNullOrEmpty(table.Rows[i]["TIME_STAMP_MOP"].ToString()))
                    {
                        this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["TIME_STAMP_MOP"].Value = null;
                    }
                    else
                    {
                        this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["TIME_STAMP_MOP"].Value = ConvertStrByte.In32ToByteArray((Int32)table.Rows[i]["TIME_STAMP_MOP"]);
                    }
                    //TIME_STAMP_MOPKを設定
                    if (String.IsNullOrEmpty(table.Rows[i]["TIME_STAMP_MOPK"].ToString()))
                    {
                        this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["TIME_STAMP_MOPK"].Value = null;
                    }
                    else
                    {
                        this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["TIME_STAMP_MOPK"].Value = ConvertStrByte.In32ToByteArray((Int32)table.Rows[i]["TIME_STAMP_MOPK"]);
                    }
                    //ツールチープテクスト
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DELETE_FLG"].ToolTipText = ConstCls.ToolTipText1;
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DEFAULT_KBN"].ToolTipText = ConstCls.ToolTipText2;
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["DISP_NUMBER"].ToolTipText = ConstCls.ToolTipText3;
                    this.Myform.dgvDenpyouhimozuke.Rows[i].Cells["PATTERN_NAME"].ToolTipText = ConstCls.ToolTipText4;
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 一覧明細情報を取得
        /// <summary>
        /// 一覧明細情報を取得する
        /// </summary>
        /// <returns></returns>
        public void GetMeisaiIchiranData(String kbn)
        {
            LogUtility.DebugMethodStart(kbn);
            try
            {
                List<M_OUTPUT_PATTERN_HIMO> MpList = new List<M_OUTPUT_PATTERN_HIMO>();
                List<M_OUTPUT_PATTERN_KOBETSU_HIMO> MpkList = new List<M_OUTPUT_PATTERN_KOBETSU_HIMO>();
                List<M_OUTPUT_PATTERN_KOBETSU_HIMO> MpkLisInsrt = new List<M_OUTPUT_PATTERN_KOBETSU_HIMO>();
                List<M_OUTPUT_PATTERN_KOBETSU_HIMO> MopkUpdDelList = new List<M_OUTPUT_PATTERN_KOBETSU_HIMO>();

                for (int i = 0; i < this.Myform.dgvDenpyouhimozuke.Rows.Count; i++)
                {
                    M_OUTPUT_PATTERN_HIMO mop = new M_OUTPUT_PATTERN_HIMO();
                    M_OUTPUT_PATTERN_KOBETSU_HIMO mopk = new M_OUTPUT_PATTERN_KOBETSU_HIMO();

                    DataGridViewRow crtRow = this.Myform.dgvDenpyouhimozuke.Rows[i];
                    mop.SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["SYSTEM_ID_MOP"].Value.ToString());
                    mop.SEQ = SqlInt32.Parse(crtRow.Cells["SEQ_MOP"].Value.ToString());
                    mop.OUTPUT_KBN = SqlInt16.Parse(crtRow.Cells["OUTPUT_KBN"].Value.ToString());
                    mop.PATTERN_NAME = crtRow.Cells["PATTERN_NAME"].Value.ToString();
                    mop.DELETE_FLG = SqlBoolean.Parse(crtRow.Cells["DELETE_FLG"].Value.ToString());
                    //mop.DENSHU_KBN_CD = SqlInt16.Parse(Properties.Settings.Default.ParamIn_DenshuKb);
                    if (crtRow.Cells["TIME_STAMP_MOP"].Value == null)
                    {
                        mop.TIME_STAMP = null;
                    }
                    else
                    {
                        mop.TIME_STAMP = (byte[])crtRow.Cells["TIME_STAMP_MOP"].Value;
                    }
                    //更新時間、更新者、更新PCを設定
                    var dataBinder1 = new DataBinderLogic<M_OUTPUT_PATTERN_HIMO>(mop);
                    dataBinder1.SetSystemProperty(mop, false);

                    if (!String.IsNullOrEmpty(crtRow.Cells["SYSTEM_ID_MOPK"].Value.ToString()))
                        mopk.SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["SYSTEM_ID_MOPK"].Value.ToString());
                    if (!String.IsNullOrEmpty(crtRow.Cells["SEQ_MOPK"].Value.ToString()))
                        mopk.SEQ = SqlInt32.Parse(crtRow.Cells["SEQ_MOPK"].Value.ToString());
                    mopk.SHAIN_CD = crtRow.Cells["SHAIN_CD"].Value.ToString();
                    mopk.DELETE_FLG = SqlBoolean.Parse(crtRow.Cells["DELETE_FLG"].Value.ToString());
                    mopk.DEFAULT_KBN = SqlBoolean.Parse(crtRow.Cells["DEFAULT_KBN"].Value.ToString());
                    if (crtRow.Cells["DISP_NUMBER"].Value == null || String.Empty.Equals(crtRow.Cells["DISP_NUMBER"].Value.ToString()))
                    {
                        mopk.DISP_NUMBER = SqlByte.Null;
                    }
                    else
                    {
                        mopk.DISP_NUMBER = SqlInt16.Parse(crtRow.Cells["DISP_NUMBER"].Value.ToString());
                    }
                    if (crtRow.Cells["TIME_STAMP_MOPK"].Value == null)
                    {
                        mopk.TIME_STAMP = null;
                    }
                    else
                    {
                        mopk.TIME_STAMP = (byte[])crtRow.Cells["TIME_STAMP_MOPK"].Value;
                    }
                    //更新時間、更新者、更新PCを設定
                    var dataBinder2 = new DataBinderLogic<M_OUTPUT_PATTERN_KOBETSU_HIMO>(mopk);
                    dataBinder2.SetSystemProperty(mopk, false);

                    //一部を削除の場合：削除対象情報をリストに追加
                    if ("LgcDel".Equals(kbn) && mopk.DELETE_FLG)
                    {
                        MpList.Add(mop);
                        MpkList.Add(mopk);
                    }
                    else if ("UPD".Equals(kbn))
                    {//更新の場合

                        //パターン一覧：更新リストに追加
                        mop.DELETE_FLG = SqlBoolean.False;//更新の場合削除絶対行わないように修正
                        MpList.Add(mop);

                        //パターン個別：SHAIN_CD、SYSTEM_ID、SEQは既に存在する場合：更新リストに追加
                        if (!String.IsNullOrEmpty(mopk.SHAIN_CD) && (mopk.SYSTEM_ID != 0) && (mopk.SEQ != 0))
                        {
                            //仕様変更：更新する時に、該当レコードを更新するんじゃなくて、
                            //既存のレコードをロジック削除して、SEQを増やして、インサートする。
                            //ロジック削除用
                            M_OUTPUT_PATTERN_KOBETSU_HIMO mopkUpdDel = new M_OUTPUT_PATTERN_KOBETSU_HIMO();
                            //更新時間、更新者、更新PCを設定
                            var dataBinder3 = new DataBinderLogic<M_OUTPUT_PATTERN_KOBETSU_HIMO>(mopk);
                            dataBinder3.SetSystemProperty(mopkUpdDel, false);
                            mopkUpdDel.SHAIN_CD = mopk.SHAIN_CD;
                            mopkUpdDel.SYSTEM_ID = mopk.SYSTEM_ID;
                            mopkUpdDel.SEQ = mopk.SEQ;
                            mopkUpdDel.DELETE_FLG = SqlBoolean.True;
                            mopkUpdDel.TIME_STAMP = mopk.TIME_STAMP;
                            mopkUpdDel.DEFAULT_KBN = SqlBoolean.Parse(crtRow.Cells["DEFAULT_KBN_DEL"].Value.ToString());

                            if (crtRow.Cells["DISP_NUMBER_DEL"].Value == null || String.Empty.Equals(crtRow.Cells["DISP_NUMBER_DEL"].Value.ToString()))
                            {
                                mopkUpdDel.DISP_NUMBER = SqlByte.Null;
                            }
                            else
                            {
                                mopkUpdDel.DISP_NUMBER = SqlInt16.Parse(crtRow.Cells["DISP_NUMBER_DEL"].Value.ToString());
                            }
                            //更新前の削除用
                            MopkUpdDelList.Add(mopkUpdDel);

                            //新規インサート用()
                            if (mopk.DISP_NUMBER.ToString() != "Null")
                            {
                                mopk.SEQ = mopk.SEQ + 1;
                                mopk.DELETE_FLG = SqlBoolean.False;//更新の場合削除絶対行わないように修正
                                MpkLisInsrt.Add(mopk);
                            }
                        }
                        else if (mopk.DISP_NUMBER.ToString() != "Null")
                        {//パターン個別：SHAIN_CD、SYSTEM_ID、SEQは存在しない場合(ただ、出力区分が入力された場合)：新規登録リストに追加
                            mopk.SHAIN_CD = Properties.Settings.Default.ParamIn_ShaInCd;
                            mopk.SYSTEM_ID = mop.SYSTEM_ID;
                            mopk.SEQ = this.GetMaxSeq_Mopk(mopk);
                            mopk.DELETE_FLG = SqlBoolean.False;//更新の場合削除絶対行わないように修正
                            MpkLisInsrt.Add(mopk);
                        }
                    }
                }
                //更新用リストに設定する
                this.MopList = MpList; 　//削除・更新用
                this.MopkList = MpkList;　//削除用
                this.MopkUpdDelLis = MopkUpdDelList; //更新前の削除用
                this.MopkListInsert = MpkLisInsrt;　//新規登録用

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 一覧明細中のデフォルトチェックのクリック制御
        /// <summary>
        /// 一覧明細中のディフォルトチェックボックスのクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (this.Myform.dgvDenpyouhimozuke.CurrentRow == null)
                {
                    return;
                }

                if (e.RowIndex != -1 && e.ColumnIndex == 1)
                {
                    DataGridViewCheckBoxCell clickedCell = ((DataGridViewCheckBoxCell)
                        (this.Myform.dgvDenpyouhimozuke[1, e.RowIndex]));
                    clickedCell.Value = clickedCell.Value.ToString().ToLower() == "true" ? "false" : "true";
                    for (int i = 0; i < this.Myform.dgvDenpyouhimozuke.RowCount; i++)
                    {
                        if (e.RowIndex != i)
                        {
                            ((DataGridViewCheckBoxCell)this.Myform.dgvDenpyouhimozuke[e.ColumnIndex, i]).Value = "false";
                        }
                    }
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 一覧明細のダブルクリック制御
        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (this.Myform.dgvDenpyouhimozuke.CurrentRow == null)
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }

                if ((e.RowIndex != -1 && e.ColumnIndex == 4))
                {
                    //伝種区分、システムID

                    //String denSyuKb = this.Myform.dgvDenpyouhimozuke.Rows[this.Myform.dgvDenpyouhimozuke.CurrentCell.RowIndex].Cells["DENSHU_KBN_CD"].Value.ToString(); ;
                                ////フォーム起動
            
                    
                    String sysId = this.Myform.dgvDenpyouhimozuke.Rows[this.Myform.dgvDenpyouhimozuke.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString(); ;
                    //object[] args = new object[] { DENSHU_KBN.DENPYOU_HIMODUKE_ICHIRAN, sysId };
                    //FormManager.OpenForm("G480", args);
                    FormManager.OpenForm("G480", sysId);
                    //String denSyuKb = DENSHU_KBN.ICHIRANSYUTSURYOKU_KOUMOKU.ToString();
                    //String sysId = this.Myform.dgvDenpyouhimozuke.Rows[this.Myform.dgvDenpyouhimozuke.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString(); ;

                    ////画面呼び出す
                    //var 
                    //var callForm = new Shougun.Core.Common.DenpyouHimodukeIchiran.UIForm(denSyuKb, sysId);
                    //var callHeader = new Shougun.Core.Common.IchiranSyu.UIHeader();
                    //var popForm = new BasePopForm(callForm, callHeader);
                    //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                    //if (!isExistForm)
                    //{
                    //    popForm.ShowDialog();
                    //}
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region コンテナ種類CDの重複チェック
        /// <summary>
        /// コンテナ種類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string contena_Shurui_Cd, DataTable dtDetailList)
        {
            try
            {
                LogUtility.DebugMethodStart(contena_Shurui_Cd, dtDetailList);

                // 画面で種類CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.Myform.dgvDenpyouhimozuke.Rows.Count - 1; i++)
                {
                    if (this.Myform.dgvDenpyouhimozuke.Rows[i].Cells[ConstCls.DispNumber].
                        Value.Equals(Convert.ToString(contena_Shurui_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(contena_Shurui_Cd, dtDetailList);
            }
        }
        #endregion

        #region SEQ発番
        /// <summary>
        /// パターン(個別)：SEQ発番
        /// </summary>
        /// <returns></returns>
        private Int32 GetMaxSeq_Mopk(M_OUTPUT_PATTERN_KOBETSU_HIMO mopk)
        {
            LogUtility.DebugMethodStart(mopk);
            try
            {
                // inSql
                String inSql = @"SELECT
                                    ISNULL(MAX(SEQ), 0) as SEQ 
                             FROM
                                    M_OUTPUT_PATTERN_KOBETSU_HIMO 
                             WHERE
                                    SHAIN_CD = '" + mopk.SHAIN_CD + @"' 
                                    AND SYSTEM_ID = '" + mopk.SYSTEM_ID + "'";
                // DB検索
                DataTable dt = MopkDaoPatern.GetDateForStringSql(inSql);
                // 最大SEQを取る
                Int32 rtnSqu = Int32.Parse(dt.Rows[0]["SEQ"].ToString());
                // 最大SEQをプラス１
                rtnSqu++;

                LogUtility.DebugMethodEnd(rtnSqu);
                return rtnSqu;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.Myform.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.Myform.WindowType);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                LogUtility.DebugMethodEnd();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion 
        #region CancelCondition
        public void CancelCondition()
        {
            //throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            //throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
