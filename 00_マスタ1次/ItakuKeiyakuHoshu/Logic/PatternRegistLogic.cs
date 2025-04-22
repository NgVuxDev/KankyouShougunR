using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Dao;

namespace ItakuKeiyakuHoshu.APP
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class PatternRegistLogic : IBuisinessLogic
    {
     
        /// <summary>
        /// Form
        /// </summary>
        private PatternRegistForm form;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PatternRegistLogic(PatternRegistForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
                
            LogUtility.DebugMethodEnd();
        }
       
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // イベントの初期化処理
            this.EventInit();

             LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 登録ボタン(F9)イベント生成
            this.form.bt_ptn9.Click += new EventHandler(this.bt_ptn9_Click);

            // 閉じるボタン(F12)イベント生成
             this.form.bt_ptn12.Click += new EventHandler(this.bt_ptn12_Click);

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 閉じるボタン[F12]処理
        /// </summary>
        internal void bt_ptn12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.DialogResult = DialogResult.Cancel;
            this.form.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録ボタン[F5]処理
        /// </summary>
        internal void bt_ptn9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            var msgLogic = new MessageBoxShowLogic();
            MessageUtility msgUtil = new MessageUtility();
            List<string> errorMessage = new List<string>();
            if (string.IsNullOrWhiteSpace(this.form.PATTERN_FURIGANA.Text))
            {
                M_ERROR_MESSAGE errorMsg = msgUtil.GetMessage("E001");
                errorMessage.Add(string.Format(errorMsg.MESSAGE, "フリガナ"));
            }
            if (string.IsNullOrWhiteSpace(this.form.PATTERN_NAME.Text))
            {
                M_ERROR_MESSAGE errorMsg = msgUtil.GetMessage("E001");
                errorMessage.Add(string.Format(errorMsg.MESSAGE, "パターン名"));
            }

            if (errorMessage.Count == 0)
            {
                // 名称が変更されたら別名保存するためSYSTEM_IDとSEQをリセットする
                if (this.form.SystemId > 0 && !this.form.beforePatternName.Equals(this.form.PATTERN_NAME.Text))
                {
                    this.form.SystemId = 0;
                    this.form.Seq = 0;
                }

                // 存在チェック
                M_SBNB_PATTERN searchCond = new M_SBNB_PATTERN();
                searchCond.LAST_SBN_KBN = this.form.LastSbnKbn;
                searchCond.ITAKU_KEIYAKU_TYPE = this.form.ItakuKeiyakuType;
                searchCond.PATTERN_NAME = form.PATTERN_NAME.Text;
                var sbnbPatternSbn = DaoInitUtility.GetComponent<IM_SBNB_PATTERNDao>().GetPatternDataSqlFile("ItakuKeiyakuHoshu.Sql.GetSbnbPatternSql.sql", searchCond);
                if (sbnbPatternSbn == null || sbnbPatternSbn.Rows.Count < 1)
                {

                }
                else
                {
                    // 既に登録されているデータの場合はそちらを更新する。(マニパターンに合わせている)
                    if (!this.form.SystemId.ToString().Equals(sbnbPatternSbn.Rows[0]["SYSTEM_ID"].ToString()))
                    {
                        // 自分自身を更新する場合はメッセージは出さない。
                        DialogResult dialogResult = msgLogic.MessageBoxShow("C038");

                        if (dialogResult != DialogResult.Yes)
                        {
                            return;
                        }

                        // 既に登録されているパターン名の場合は更新対象を変更
                        long tempSysId = 0;
                        if (long.TryParse(sbnbPatternSbn.Rows[0]["SYSTEM_ID"].ToString(), out tempSysId))
                        {
                            this.form.SystemId = tempSysId;
                        }
                        int tempSeq = 0;
                        if (int.TryParse(sbnbPatternSbn.Rows[0]["SEQ"].ToString(), out tempSeq))
                        {
                            this.form.Seq = tempSeq;
                        }
                    }
                }

                this.form.DialogResult = DialogResult.OK;
                this.form.Close();
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, errorMessage.ToArray()), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// フリガナ入力チェック
        /// </summary>
        public void CheckFurigana(CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            r_framework.Logic.Validator vali = new r_framework.Logic.Validator(this.form.PATTERN_FURIGANA, null);
            string ret = vali.ZenKatakanaCheck();
            if (!string.IsNullOrWhiteSpace(ret))
            {
                MessageBox.Show(ret, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            LogUtility.DebugMethodEnd();
        }
    }
}
