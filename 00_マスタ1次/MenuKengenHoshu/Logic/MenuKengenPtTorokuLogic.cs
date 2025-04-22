// $Id: MenuKengenPtTorokuLogic.cs 36844 2014-12-09 06:46:55Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MenuKengenHoshu.APP;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;

namespace MenuKengenHoshu.Logic
{
    /// <summary>
    /// メニュー権限パターン登録画面のロジック
    /// </summary>
    public class MenuKengenPtTorokuLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>
        /// Form
        /// </summary>
        private MenuKengenPtTorokuForm form;

        /// <summary>
        /// ログインユーザー名
        /// </summary>
        internal string LoginUserName;

        /// <summary>
        /// 登録対象のメニュー権限パターン詳細リスト
        /// </summary>
        internal List<M_MENU_AUTH_PT_DETAIL> MenuAuthPtDetailList;

        /// <summary>
        /// パターンID
        /// </summary>
        /// <remarks>0は未登録扱い。</remarks>
        internal long PatternID;

        /// <summary>
        /// メニュー権限パターンDao
        /// </summary>
        private IM_MENU_AUTH_PT_ENTRYDao daoMenuAuthPtEntry;

        /// <summary>
        /// メニュー権限パターン詳細Dao
        /// </summary>
        private IM_MENU_AUTH_PT_DETAILDao daoMenuAuthPtDetail;

        #endregion

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public MenuKengenPtTorokuLogic(MenuKengenPtTorokuForm targetForm)
        {
            this.form = targetForm;

            // DAO
            this.daoMenuAuthPtEntry = DaoInitUtility.GetComponent<IM_MENU_AUTH_PT_ENTRYDao>();
            this.daoMenuAuthPtDetail = DaoInitUtility.GetComponent<IM_MENU_AUTH_PT_DETAILDao>();
        }

        #endregion

        #region - Method -

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンの初期化
                this.ButtonInit();

                // イベントの初期化
                this.EventInit();

                // メニュー権限パターンの反映
                this.SetPtEntryInfo();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化
        /// </summary>
        private void ButtonInit()
        {
            this.form.bt_func9.Enabled = true;
            this.form.bt_func12.Enabled = true;

            this.form.bt_func9.Text = "[F9]\r\n登録";
            this.form.bt_func12.Text = "F12]\r\n閉じる";
        }

        #endregion

        #region イベント初期化

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            // 登録ボタン(F9)イベント生成
            this.form.bt_func9.Click -= new EventHandler(this.form.Regist);
            this.form.bt_func9.Click += new EventHandler(this.form.Regist);

            // 閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click -= new EventHandler(this.form.FormClose);
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
        }

        #endregion

        #region メニュー権限パターンの反映

        /// <summary>
        /// メニュー権限パターンの反映
        /// </summary>
        private void SetPtEntryInfo()
        {
            // パターンIDは1以上
            if (this.PatternID == 0)
            {
                return;
            }

            var entity = new M_MENU_AUTH_PT_ENTRY();
            entity.PATTERN_ID = this.PatternID;

            var result = daoMenuAuthPtEntry.GetAllValidData(entity).FirstOrDefault();
            if (result != null)
            {
                this.form.txt_PatternNameKana.Text = result.PATTERN_FURIGANA;
                this.form.txt_PatternName.Text = result.PATTERN_NAME;
            }
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            // 未使用
            return 0;
        }

        #endregion

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            try
            {
                if (MenuAuthPtDetailList == null)
                {
                    return;
                }

                if (RequiredCheck())
                {
                    // エラー有の場合は処理終了
                    return;
                }

                var msgLogic = new MessageBoxShowLogic();

                // トランザクション開始
                using (var tran = new Transaction())
                {

                    // メニューパターン
                    var data = new M_MENU_AUTH_PT_ENTRY();
                    data.PATTERN_NAME = this.form.txt_PatternName.Text;

                    var sameNameEntry = this.daoMenuAuthPtEntry.GetAllValidData(data).FirstOrDefault();
                    if (sameNameEntry == null)
                    {
                        // 新規登録

                        // パターンID取得
                        var patternID = this.daoMenuAuthPtEntry.GetMaxPatternID() + 1;

                        // メニューパターン
                        var entry = new M_MENU_AUTH_PT_ENTRY();

                        // バインドロジック作成
                        var dataBinderEntryLogic = new DataBinderLogic<M_MENU_AUTH_PT_ENTRY>(entry);

                        entry.PATTERN_ID = patternID;
                        entry.PATTERN_NAME = this.form.txt_PatternName.Text;
                        entry.PATTERN_FURIGANA = this.form.txt_PatternNameKana.Text;

                        dataBinderEntryLogic.SetSystemProperty(entry, false);
                        entry.DELETE_FLG = SqlBoolean.False;
                        // リボンメニューを保持してないため、渡されたログインユーザー名で設定
                        entry.CREATE_USER = LoginUserName;
                        entry.UPDATE_USER = LoginUserName;

                        this.daoMenuAuthPtEntry.Insert(entry);

                        // メニューパターン詳細
                        foreach (var menuAuthPtDetail in MenuAuthPtDetailList)
                        {
                            menuAuthPtDetail.PATTERN_ID = patternID;

                            this.daoMenuAuthPtDetail.Insert(menuAuthPtDetail);
                        }
                    }
                    else
                    {
                        // 重複有
                        DialogResult dialogResult = msgLogic.MessageBoxShow("C038");

                        if (dialogResult != DialogResult.Yes)
                        {
                            return;
                        }

                        // メニューパターン
                        // バインドロジック作成
                        var dataBinderEntryLogic = new DataBinderLogic<M_MENU_AUTH_PT_ENTRY>(sameNameEntry);

                        // フリガナは更新
                        sameNameEntry.PATTERN_FURIGANA = this.form.txt_PatternNameKana.Text;

                        dataBinderEntryLogic.SetSystemProperty(sameNameEntry, false);
                        sameNameEntry.DELETE_FLG = SqlBoolean.False;
                        // リボンメニューを保持してないため、渡されたログインユーザー名で設定
                        sameNameEntry.UPDATE_USER = LoginUserName;

                        this.daoMenuAuthPtEntry.Update(sameNameEntry);

                        // メニューパターン詳細
                        // データ存在チェック用に該当データ取得
                        var detailData = new M_MENU_AUTH_PT_DETAIL();
                        detailData.PATTERN_ID = sameNameEntry.PATTERN_ID;

                        var detailList = this.daoMenuAuthPtDetail.GetAllValidData(detailData);

                        foreach (var menuAuthPtDetail in MenuAuthPtDetailList)
                        {
                            // データ存在チェック
                            bool exist = detailList.Any(n => n.PATTERN_ID.Value == sameNameEntry.PATTERN_ID.Value
                                                          && n.FORM_ID.Equals(menuAuthPtDetail.FORM_ID)
                                                          && n.WINDOW_ID.Value == menuAuthPtDetail.WINDOW_ID.Value);

                            // 同パターン名が存在する場合も上書き登録するため、
                            // Update時でも常にパターンIDは上書き
                            menuAuthPtDetail.PATTERN_ID = sameNameEntry.PATTERN_ID;
                            var dataBindermenuAuthPtDetail = new DataBinderLogic<M_MENU_AUTH_PT_DETAIL>(menuAuthPtDetail);
                            dataBindermenuAuthPtDetail.SetSystemProperty(menuAuthPtDetail, false);

                            if (exist)
                            {
                                var result = detailList.Where(n => n.PATTERN_ID.Value == sameNameEntry.PATTERN_ID.Value
                                                          && n.FORM_ID.Equals(menuAuthPtDetail.FORM_ID)
                                                          && n.WINDOW_ID.Value == menuAuthPtDetail.WINDOW_ID.Value).FirstOrDefault();
                                menuAuthPtDetail.TIME_STAMP = result.TIME_STAMP;

                                this.daoMenuAuthPtDetail.Update(menuAuthPtDetail);
                            }
                            else
                            {
                                this.daoMenuAuthPtDetail.Insert(menuAuthPtDetail);
                            }
                        }
                    }

                    // トランザクション終了
                    tran.Commit();
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
                this.form.errmessage.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(errorFlag);
            }
        }

        #region 未使用
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region 必須チェック

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        private bool RequiredCheck()
        {
            bool hasError = false;
            List<string> errMsgList = new List<string>();

            // フリガナ
            if (string.IsNullOrEmpty(this.form.txt_PatternNameKana.Text))
            {
                var errMsg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E012"), "フリガナ");
                errMsgList.Add(errMsg);

                this.form.txt_PatternNameKana.Focus();

                hasError = true;
            }

            // パターン名
            if (string.IsNullOrEmpty(this.form.txt_PatternName.Text))
            {
                var errMsg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E012"), "パターン名");
                errMsgList.Add(errMsg);

                this.form.txt_PatternName.Focus();

                hasError = true;
            }

            // エラーメッセージ表示
            if (hasError)
            {
                var sb = new StringBuilder();
                foreach (var errMsg in errMsgList)
                {
                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        // エラーメッセージ毎に改行で区切る
                        sb.Append(Environment.NewLine);
                    }

                    sb.Append(errMsg);
                }

                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(sb.ToString());
            }

            return hasError;
        }

        #endregion

        #endregion
    }
}
