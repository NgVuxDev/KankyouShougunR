using System;
using System.Collections.Generic;
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
using System.Reflection;
using r_framework.APP.PopUp.Base;
using Shougun.Core.BusinessManagement.Const.Common;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku;
using r_framework.FormManager;

namespace Shougun.Core.BusinessManagement.Mitumorisyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class G471Logic : IBuisinessLogic
    {
     
        /// <summary>
        /// Form
        /// </summary>
        private G471Form form;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string buttonInfoXmlPath = "Shougun.Core.BusinessManagement.Mitumorisyo.Setting.ButtonSetting.xml";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G471Logic(G471Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
                
            LogUtility.DebugMethodEnd();
        }
       
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //見積書種類をセット
                this.form.CUSOMLISTBOX_MITUMORISYO.Items.Add(MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME);
                this.form.CUSOMLISTBOX_MITUMORISYO.Items.Add(MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME);
                this.form.CUSOMLISTBOX_MITUMORISYO.Items.Add(MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME);
                this.form.CUSOMLISTBOX_MITUMORISYO.Items.Add(MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME);


                // イベントの初期化処理
                this.EventInit();

                //フォーカス当てる
                this.form.CUSOMLISTBOX_MITUMORISYO.SelectedIndex = 0;
                this.form.CUSOMLISTBOX_MITUMORISYO.Focus();

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

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // Formキーイベント生成
            this.form.KeyUp += new KeyEventHandler(this.form.ControlKeyUp);
           
            // 表示ボタン(F5)イベント生成
            this.form.bt_ptn5.Click += new EventHandler(this.bt_ptn5_Click);

            // 閉じるボタン(F12)イベント生成
             this.form.bt_ptn12.Click += new EventHandler(this.bt_ptn12_Click);

            LogUtility.DebugMethodEnd();

        }

        
        /// <summary>
        /// Enterキー処理
        /// </summary>
        internal void Enter_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            
            //int index = this.form.CUSOMLISTBOX_MITUMORISYO.SelectedIndex;

            //switch (index)
            //{
            //    case 0:
            //        this.form.CUSOMLISTBOX_MITUMORISYO.SelectedIndex = 1;
            //        break;
            //    case 1:
            //        this.form.CUSOMLISTBOX_MITUMORISYO.SelectedIndex = 2;
            //        break;
            //    case 2:
            //        this.form.CUSOMLISTBOX_MITUMORISYO.SelectedIndex = 3;
            //        break;
            //    case 3:
            //        // this.form.CUSOMLISTBOX_MITUMORISYO.SelectedIndex = 1;
            //        break;

            //}

            //G276フォーム起動
            this.bt_ptn5_Click(sender, e);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じるボタン[F12]処理
        /// </summary>
        internal void bt_ptn12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 表示ボタン[F5]処理
        /// </summary>
        internal void bt_ptn5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            string mitumorisyoName = (string)this.form.CUSOMLISTBOX_MITUMORISYO.SelectedItem;
            int mitumorisyoType;

            if (null == mitumorisyoName) { MessageBox.Show("見積書を選択してください"); }
            else
            {
                //転送情報を設定
                switch (mitumorisyoName)
                {
                    case MitumorisyoConst.MITUMOTISYO_TANKA_YOKO_NAME:
                        mitumorisyoType = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO;
                        break;
                    case MitumorisyoConst.MITUMOTISYO_TANKA_TATE_NAME:
                        mitumorisyoType = MitumorisyoConst.MITUMOTISYO_TANKA_TATE;
                        break;
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO_NAME:
                        mitumorisyoType = MitumorisyoConst.MITUMOTISYO_KINGAKU_YOKO;
                        break;
                    case MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE_NAME:
                        mitumorisyoType = MitumorisyoConst.MITUMOTISYO_KINGAKU_TATE;
                        break;
                    default:
                        mitumorisyoType = 0;
                        break;
                }
                

                // 引数遷移先パラメータ設定dto
                //帳票種別設定
                this.form.dto.cyohyoType = mitumorisyoType;
                //
                this.form.dto.windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                // 見積番号設定
                //this.form.dto.mitsumoriNumber = "18";

                //G276フォーム起動
                FormManager.OpenForm("G276", WINDOW_TYPE.NEW_WINDOW_FLAG, this.form.dto);

  
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
    }
}
