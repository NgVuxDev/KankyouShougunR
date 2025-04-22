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
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.PatternIchiran.APP;
using Shougun.Core.Common.PatternIchiran.Const;
using Shougun.Core.Common.PatternIchiran.DAO;

namespace Shougun.Core.Common.PatternIchiran
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
        /// 更新データ：パターン一覧
        /// </summary>
        public List<M_OUTPUT_PATTERN> MopList { get; set; }

        /// <summary>
        /// 更新データ：パターン（個別）
        /// </summary>
        public List<M_OUTPUT_PATTERN_KOBETSU> MopkList { get; set; }

        /// <summary>
        /// 新規データ：パターン（個別）
        /// </summary>
        public List<M_OUTPUT_PATTERN_KOBETSU> MopkListInsert { get; set; }

        /// <summary>
        /// 削除データ：パターン（個別）
        /// 更新前のロジック削除用
        /// </summary>
        public List<M_OUTPUT_PATTERN_KOBETSU> MopkUpdDelLis { get; set; }

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
        private string ButtonInfoXmlPath = "Shougun.Core.Common.PatternIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// MyForm
        /// </summary>
        private UIForm Myform;

        #endregion

        #region メソッド
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <returns></returns>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.Myform = targetForm;
            this.SearchString = new DTOCls();
            this.MopDaoPatern = DaoInitUtility.GetComponent<MOPDaoCls>();
            this.MopkDaoPatern = DaoInitUtility.GetComponent<MOPKDaoCls>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            if (this.Myform != null && this.Myform.DenshuKbn.StartsWith(Convert.ToInt32(DENSHU_KBN.DENPYOU_ICHIRAN).ToString()))
            {
                // ヘッダのテキストを初期化
                this.HeaderInit();
            }
            // ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();
            // 検索条件を設定
            this.Myform.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_txt;

            if (this.Search() > 0)
            {
                this.SetIchiran();
            }

            LogUtility.DebugMethodEnd();
        }

        internal void HeaderInit()
        {
            if (this.Myform != null)
            {
                var parentForm = (BusinessBaseForm)this.Myform.Parent;
                var header = (HeaderBaseForm)parentForm.headerForm;

                // 伝種区分によってタイトルを切り替える
                var denpyouIchiran = Convert.ToInt32(DENSHU_KBN.DENPYOU_ICHIRAN).ToString();
                if (this.Myform.DenshuKbn.StartsWith(denpyouIchiran))
                {
                    var denpyouShurui = string.Empty;
                    switch ((DENSHU_KBN)int.Parse(this.Myform.DenshuKbn))
                    {
                        case DENSHU_KBN.DENPYOU_ICHIRAN:
                            denpyouShurui = "受入";
                            break;
                        case DENSHU_KBN.SHUKKA_ICHIRAN:
                            denpyouShurui = "出荷";
                            break;
                        case DENSHU_KBN.URIAGE_SHIHARAI_ICHIRAN:
                            denpyouShurui = "売上/支払";
                            break;
                        case DENSHU_KBN.KEIRYOU_ICHIRAN:
                            denpyouShurui = "計量";
                            break;
                        case DENSHU_KBN.UNCHIN_ICHIRAN:
                            denpyouShurui = "運賃";
                            break;
                        case DENSHU_KBN.DAINOU_ICHIRAN:
                            denpyouShurui = "代納";
                            break;
                        default:
                            break;
                    }

                    header.lb_title.Text = denpyouShurui + header.lb_title.Text;
                }

                this.Myform.ParentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(header.lb_title.Text);
            }
        }

        /// <summary>
        /// 適用ボタン処理
        /// </summary>
        /// <returns>sysId</returns>
        internal void BottonTekiyou()
        {
            LogUtility.DebugMethodStart();

            if (this.Myform.PatternIchiran.RowCount > 0)
            {
                this.Myform.ParamOut_SysID = this.Myform.PatternIchiran.Rows[this.Myform.PatternIchiran.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 削除ボタン処理
        /// </summary>
        /// <returns></returns>
        internal void BottonDelete()
        {
            LogUtility.DebugMethodStart();

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
                            M_OUTPUT_PATTERN mop = MopList[i];
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

                        foreach (M_OUTPUT_PATTERN_KOBETSU mopk in MopkList)
                        {
                            if (!String.IsNullOrEmpty(mopk.SHAIN_CD) && mopk.SYSTEM_ID != SqlInt64.Zero && mopk.SEQ != SqlInt32.Zero)
                            {
                                int CntMopkUpd = MopkDaoPatern.Update(mopk);
                            }
                        }

                        tran.Commit();

                        this.Myform.ParamOut_UpdateFlag = true; //2013.12.15 naitou upd
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

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns>count</returns>
        public virtual int Search()
        {
            LogUtility.DebugMethodStart();

            this.SearchResult = new DataTable();

            // 全データを取得したいので、ここでパターン名による絞り込みは行わない
            //this.SearchString.Patern_Name = this.Myform.CONDITION_VALUE.Text;
            this.SearchString.Patern_Name = string.Empty;
            this.SearchString.Shain_Cd = r_framework.Dto.SystemProperty.Shain.CD;
            this.SearchString.Denshu_Kbn_Cd = Properties.Settings.Default.ParamIn_DenshuKb;
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

            //2013-11-25 Del ogawamut PT 東北
            //if (cnt == 0)
            //{
            //    MessageBox.Show(ConstCls.SearchEmptInfo, ConstCls.DialogTitle);
            //}

            LogUtility.DebugMethodEnd(cnt);
            return cnt;
        }

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
                        foreach (M_OUTPUT_PATTERN mop in MopList)
                        {
                            DataTable dt = MopDaoPatern.CheckExistRecord(mop.SYSTEM_ID, false);
                            if (dt.Rows.Count == 0)
                            {
                                //排他エラー発生
                                LogUtility.Warn("排他発生。画面表示後にM_OUTPUT_PATTERNが削除された。：" + mop.SYSTEM_ID + ":" + mop.PATTERN_NAME);
                                //MessageBox.Show(ConstCls.ErrStop7, ConstCls.DialogTitleErr, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                var msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E080");
                                return;
                            }
                        }



                        //個別論理削除
                        foreach (M_OUTPUT_PATTERN_KOBETSU mopk in MopkUpdDelLis)
                        {
                            int CntMopkUpd = MopkDaoPatern.Update(mopk);
                        }

                        //個別新規登録
                        foreach (M_OUTPUT_PATTERN_KOBETSU mopkIsrt in MopkListInsert)
                        {
                            int CntMopkUpd = MopkDaoPatern.Insert(mopkIsrt);
                        }

                        //トランザクション終了（未コミットの場合ロールバック）
                        tran.Commit();

                        this.Myform.ParamOut_UpdateFlag = true; //2013.12.15 naitou upd
                    }

                    //更新Successメッセージ
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("I001", ConstCls.UpdInfo);
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
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        /// <returns></returns>
        internal void EventInit()
        {
            LogUtility.DebugMethodStart();

            var ParentForm = (BusinessBaseForm)this.Myform.Parent;

            //適用ボタン(F1)イベント生成
            //ParentForm.bt_func1.Click += new EventHandler(this.Myform.FormTekiyou);   // No.1144

            //新規ボタン(F2)イベント生成
            ParentForm.bt_func2.Click += new EventHandler(this.Myform.NewAdd);

            //2013-11-19 Add ogawamut PT 東北 No.1203
            //修正ボタン(F3)イベント生成
            ParentForm.bt_func3.Click += new EventHandler(this.Myform.Update);

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

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        /// <returns></returns>
        private void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            DTOCls entity = new DTOCls();
            if (!string.IsNullOrEmpty(this.Myform.CONDITION_VALUE.Text))
            {
                entity.Patern_Name = this.Myform.CONDITION_VALUE.Text;
            }
            this.SearchString = entity;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <returns></returns>
        public void Cancel()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetSearchString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        /// <returns></returns>
        public void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            Properties.Settings.Default.ParamIn_ShaInCd = null;
            Properties.Settings.Default.ParamIn_DenshuKb = null;
            Properties.Settings.Default.ConditionValue_txt = null;
            Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd();
        }

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

            Boolean haveDelChk = false;
            Boolean haveDefltChk = false;
            for (int i = 0; i < this.Myform.PatternIchiran.RowCount; i++)
            {
                //削除チェックボックスとディフォルトチェックボックス
                String delChk = ((DataGridViewCheckBoxCell)this.Myform.PatternIchiran[0, i]).Value.ToString().ToLower();
                String defltChk = ((DataGridViewCheckBoxCell)this.Myform.PatternIchiran[1, i]).Value.ToString().ToLower();

                //削除行あるのか記録
                if ("true".Equals(delChk.ToLower()))
                {
                    haveDelChk = true;
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
                if (delChk.Equals(defltChk) && "true".Equals(delChk.ToLower()) && "del".Equals(kbn))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E036");
                    return false;
                }

                //表示順序重複
                String dispNum1 = "";
                String dispNum2 = "";
                if (((DataGridViewTextBoxCell)this.Myform.PatternIchiran[2, i]).Value != null)
                {
                    dispNum1 = ((DataGridViewTextBoxCell)this.Myform.PatternIchiran[2, i]).Value.ToString();
                }

                for (int j = i + 1; j < this.Myform.PatternIchiran.RowCount; j++)
                {
                    if (((DataGridViewTextBoxCell)this.Myform.PatternIchiran[2, j]).Value != null)
                    {
                        dispNum2 = ((DataGridViewTextBoxCell)this.Myform.PatternIchiran[2, j]).Value.ToString();
                    }
                    if (dispNum1.Equals(dispNum2) && !"".Equals(dispNum1) && "upd".Equals(kbn))
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E034", ConstCls.ErrStop4);
                        return false;
                    }
                    if ((String.Compare(dispNum1, "5") > 0 || String.Compare(dispNum2, "5") > 0 || "0".Equals(dispNum1) || "0".Equals(dispNum2))
                         && "upd".Equals(kbn))
                    {
                        MessageBox.Show("表示区分は１～５間の数字で入力してください。", ConstCls.DialogTitle);
                        return false;
                    }
                }

                //表示区分入力しないレコードをディフォルトとしてチェックされた時、エラー出る
                if ("true".Equals(defltChk.ToLower()) && String.Empty.Equals(dispNum1) && "upd".Equals(kbn))
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

            //削除チェックボックスを選択される場合
            if (haveDelChk && "upd".Equals(kbn))
            {
                MessageBox.Show(ConstCls.ErrStop8, ConstCls.DialogTitle);
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

        /// <summary>
        /// 削除前のチェック（排他チェック）
        /// </summary>
        /// <returns></returns>
        public Boolean ChkBefDel(M_OUTPUT_PATTERN mop)
        {
            //LogUtility.DebugMethodStart();
            try
            {
                DataTable dt = new DataTable();
                //排他検索SQL
                String inSql = @"SELECT * FROM M_OUTPUT_PATTERN_KOBETSU 
                                 WHERE DEFAULT_KBN = 'TRUE' AND DELETE_FLG = 0 AND SYSTEM_ID = '" + mop.SYSTEM_ID + "' ";
                //検索ロジック
                dt = MopDaoPatern.GetDateForStringSql(inSql);

                //LogUtility.DebugMethodEnd();
                if (dt.Rows.Count > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                LogUtility.Error(e);
                throw;
            }
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        /// <returns></returns>
        public void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            //前の結果をクリア
            this.Myform.PatternIchiran.Rows.Clear();

            //検索結果を設定する
            var table = this.SearchResult;
            table.BeginLoadData();

            //検索結果設定
            for (int i = 0; i < table.Rows.Count; i++)
            {
                //画面表示項目
                this.Myform.PatternIchiran.Rows.Add();
                this.Myform.PatternIchiran.Rows[i].Cells["DELETE_FLG"].Value = table.Rows[i]["DELETE_FLG"];
                this.Myform.PatternIchiran.Rows[i].Cells["DEFAULT_KBN"].Value = table.Rows[i]["DEFAULT_KBN"];
                this.Myform.PatternIchiran.Rows[i].Cells["DEFAULT_KBN_DEL"].Value = table.Rows[i]["DEFAULT_KBN"];
                this.Myform.PatternIchiran.Rows[i].Cells["DISP_NUMBER"].Value = table.Rows[i]["DISP_NUMBER"];
                this.Myform.PatternIchiran.Rows[i].Cells["DISP_NUMBER_DEL"].Value = table.Rows[i]["DISP_NUMBER"];
                //出力区分名：伝票・明細
                String OUTPUT_KBN = table.Rows[i]["OUTPUT_KBN"].ToString();
                switch (OUTPUT_KBN)
                {
                    case "1":
                        this.Myform.PatternIchiran.Rows[i].Cells["OUTPUT_KBN_NAME"].Value = ConstCls.OuptKbn_DenPyou;
                        break;
                    case "2":
                        this.Myform.PatternIchiran.Rows[i].Cells["OUTPUT_KBN_NAME"].Value = ConstCls.OuptKbn_Meisai;
                        break;
                }
                //パターン名
                this.Myform.PatternIchiran.Rows[i].Cells["PATTERN_NAME"].Value = table.Rows[i]["PATTERN_NAME"];
                //非表示項目
                this.Myform.PatternIchiran.Rows[i].Cells["SYSTEM_ID_MOP"].Value = table.Rows[i]["SYSTEM_ID_MOP"];
                this.Myform.PatternIchiran.Rows[i].Cells["SEQ_MOP"].Value = table.Rows[i]["SEQ_MOP"];
                this.Myform.PatternIchiran.Rows[i].Cells["SYSTEM_ID_MOPK"].Value = table.Rows[i]["SYSTEM_ID_MOPK"];
                this.Myform.PatternIchiran.Rows[i].Cells["SEQ_MOPK"].Value = table.Rows[i]["SEQ_MOPK"];
                this.Myform.PatternIchiran.Rows[i].Cells["SHAIN_CD"].Value = table.Rows[i]["SHAIN_CD"];
                this.Myform.PatternIchiran.Rows[i].Cells["OUTPUT_KBN"].Value = table.Rows[i]["OUTPUT_KBN"];
                this.Myform.PatternIchiran.Rows[i].Cells["DENSHU_KBN_CD"].Value = table.Rows[i]["DENSHU_KBN_CD"];
                //TIME_STAMP_MOPを設定
                if (String.IsNullOrEmpty(table.Rows[i]["TIME_STAMP_MOP"].ToString()))
                {
                    this.Myform.PatternIchiran.Rows[i].Cells["TIME_STAMP_MOP"].Value = null;
                }
                else
                {
                    this.Myform.PatternIchiran.Rows[i].Cells["TIME_STAMP_MOP"].Value = ConvertStrByte.In32ToByteArray((Int32)table.Rows[i]["TIME_STAMP_MOP"]);
                }
                //TIME_STAMP_MOPKを設定
                if (String.IsNullOrEmpty(table.Rows[i]["TIME_STAMP_MOPK"].ToString()))
                {
                    this.Myform.PatternIchiran.Rows[i].Cells["TIME_STAMP_MOPK"].Value = null;
                }
                else
                {
                    this.Myform.PatternIchiran.Rows[i].Cells["TIME_STAMP_MOPK"].Value = ConvertStrByte.In32ToByteArray((Int32)table.Rows[i]["TIME_STAMP_MOPK"]);
                }
                //ツールチープテクスト
                this.Myform.PatternIchiran.Rows[i].Cells["DELETE_FLG"].ToolTipText = ConstCls.ToolTipText1;
                this.Myform.PatternIchiran.Rows[i].Cells["DEFAULT_KBN"].ToolTipText = ConstCls.ToolTipText2;
                this.Myform.PatternIchiran.Rows[i].Cells["DISP_NUMBER"].ToolTipText = ConstCls.ToolTipText3;
                this.Myform.PatternIchiran.Rows[i].Cells["PATTERN_NAME"].ToolTipText = ConstCls.ToolTipText4;

                // ここで文字列検索の結果を反映する
                if (!this.Myform.PatternIchiran.Rows[i].Cells["PATTERN_NAME"].Value.ToString().Contains(this.Myform.CONDITION_VALUE.Text))
                {
                    // 検索文字列がパターン名に含まれる行を非表示にする
                    //　（データとしては存在しているので、各種チェックには引っかかる）
                    this.Myform.PatternIchiran.Rows[i].Visible = false;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一覧明細情報を取得する
        /// </summary>
        /// <returns></returns>
        public void GetMeisaiIchiranData(String kbn)
        {
            LogUtility.DebugMethodStart(kbn);

            List<M_OUTPUT_PATTERN> MpList = new List<M_OUTPUT_PATTERN>();
            List<M_OUTPUT_PATTERN_KOBETSU> MpkList = new List<M_OUTPUT_PATTERN_KOBETSU>();
            List<M_OUTPUT_PATTERN_KOBETSU> MpkLisInsrt = new List<M_OUTPUT_PATTERN_KOBETSU>();
            List<M_OUTPUT_PATTERN_KOBETSU> MopkUpdDelList = new List<M_OUTPUT_PATTERN_KOBETSU>();

            for (int i = 0; i < this.Myform.PatternIchiran.Rows.Count; i++)
            {
                M_OUTPUT_PATTERN mop = new M_OUTPUT_PATTERN();
                M_OUTPUT_PATTERN_KOBETSU mopk = new M_OUTPUT_PATTERN_KOBETSU();

                DataGridViewRow crtRow = this.Myform.PatternIchiran.Rows[i];
                mop.SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["SYSTEM_ID_MOP"].Value.ToString());
                mop.SEQ = SqlInt32.Parse(crtRow.Cells["SEQ_MOP"].Value.ToString());
                mop.OUTPUT_KBN = SqlInt16.Parse(crtRow.Cells["OUTPUT_KBN"].Value.ToString());
                mop.PATTERN_NAME = crtRow.Cells["PATTERN_NAME"].Value.ToString();
                mop.DELETE_FLG = SqlBoolean.Parse(crtRow.Cells["DELETE_FLG"].Value.ToString());
                mop.DENSHU_KBN_CD = SqlInt16.Parse(Properties.Settings.Default.ParamIn_DenshuKb);
                if (crtRow.Cells["TIME_STAMP_MOP"].Value == null)
                {
                    mop.TIME_STAMP = null;
                }
                else
                {
                    mop.TIME_STAMP = (byte[])crtRow.Cells["TIME_STAMP_MOP"].Value;
                }
                //更新時間、更新者、更新PCを設定
                var dataBinder1 = new DataBinderLogic<M_OUTPUT_PATTERN>(mop);
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
                var dataBinder2 = new DataBinderLogic<M_OUTPUT_PATTERN_KOBETSU>(mopk);
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
                        M_OUTPUT_PATTERN_KOBETSU mopkUpdDel = new M_OUTPUT_PATTERN_KOBETSU();
                        //更新時間、更新者、更新PCを設定
                        var dataBinder3 = new DataBinderLogic<M_OUTPUT_PATTERN_KOBETSU>(mopk);
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
                            //mopk.SEQ = mopk.SEQ + 1;
                            mopk.SEQ = this.GetMaxSeq_Mopk(mopk) + i;
                            mopk.DELETE_FLG = SqlBoolean.False;//更新の場合削除絶対行わないように修正
                            MpkLisInsrt.Add(mopk);
                        }
                    }
                    else if (mopk.DISP_NUMBER.ToString() != "Null")
                    {//パターン個別：SHAIN_CD、SYSTEM_ID、SEQは存在しない場合(ただ、出力区分が入力された場合)：新規登録リストに追加
                        mopk.SHAIN_CD = r_framework.Dto.SystemProperty.Shain.CD;
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

        /// <summary>
        /// 一覧明細中のディフォルトチェックボックスのクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Myform.PatternIchiran.CurrentRow == null)
            {
                return;
            }

            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                DataGridViewCheckBoxCell clickedCell = ((DataGridViewCheckBoxCell)(this.Myform.PatternIchiran[1, e.RowIndex]));
                clickedCell.Value = clickedCell.Value.ToString().ToLower() == "true" ? "false" : "true";
                for (int i = 0; i < this.Myform.PatternIchiran.RowCount; i++)
                {
                    if (e.RowIndex != i)
                    {
                        ((DataGridViewCheckBoxCell)this.Myform.PatternIchiran[e.ColumnIndex, i]).Value = "false";
                    }
                }
            }
        }

        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //2013-11-19 Upd ogawamut PT 東北 No.1203
        //public void PatternIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        public void PatternIchiran_CellDoubleClick()
        {
            if (this.Myform.PatternIchiran.CurrentRow == null)
            {
                return;
            }

            //2013-11-19 Upd ogawamut PT 東北 No.1203
            //if ((e.RowIndex != -1 && e.ColumnIndex == 4))
            //if ((this.Myform.PatternIchiran.CurrentCell.RowIndex != -1 && this.Myform.PatternIchiran.CurrentCell.ColumnIndex == 4))
            //2013-11-28 Upd ogawamut PT 東北 No.1378
            if (this.Myform.PatternIchiran.CurrentCell.RowIndex != -1)
            {
                //伝種区分、システムID
                String denSyuKb = this.Myform.PatternIchiran.Rows[this.Myform.PatternIchiran.CurrentCell.RowIndex].Cells["DENSHU_KBN_CD"].Value.ToString(); ;
                String sysId = this.Myform.PatternIchiran.Rows[this.Myform.PatternIchiran.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString(); ;

                //画面呼び出す
                //2013-11-19 Upd ogawamut PT 東北 No.1203
                //2013-11-26 Upd ogawamut PT 東北 No.1325
                var callForm = new Shougun.Core.Common.IchiranSyu.UIForm(denSyuKb, sysId);
                var callHeader = new Shougun.Core.Common.IchiranSyu.UIHeader();
                var popForm = new BasePopForm(callForm, callHeader);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    popForm.ShowDialog();
                }
                //FormManager.OpenForm("G187", denSyuKb, sysId);
            }
        }

        /// <summary>
        /// パターン(個別)：SEQ発番
        /// </summary>
        /// <returns></returns>
        private Int32 GetMaxSeq_Mopk(M_OUTPUT_PATTERN_KOBETSU mopk)
        {
            LogUtility.DebugMethodStart(mopk);

            // inSql
            String inSql = @"SELECT
                                    ISNULL(MAX(SEQ), 0) as SEQ 
                             FROM
                                    M_OUTPUT_PATTERN_KOBETSU 
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

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.Myform.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.Myform.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

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
