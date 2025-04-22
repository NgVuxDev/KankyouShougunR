using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Intercepter;
using r_framework.Logic;
using r_framework.Entity;
using OboeGakiIkktuImputIchiran;

namespace OboeGakiIkktuImputIchiran
{
    public partial class M421Form :SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
       
        private M421Logic logic;
        /// <summary>
        /// Close処理の後に実行するメソッド
        /// 制約：戻り値なし、Publicなメソッド
        /// </summary>
        public delegate void LastRunMethod(WINDOW_TYPE type, T_ITAKU_MEMO_IKKATSU_ENTRY sendDto);

        /// <summary>
        /// Close処理の後に実行するメソッド
        /// </summary>
        public LastRunMethod closeMethod;

        /// <summary>
        /// 遷移元フラグ：0＝メニュー画面、1＝覚書一括入力画面
        /// </summary>
        internal int seniMotoFlg;

        /// <summary>
        /// コンストラクタ,メニューから起動の場合
        /// </summary>
        public M421Form()
         
        {
            this.InitializeComponent();
            //メニューから起動の場合
            this.seniMotoFlg = 0;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new M421Logic(this);
         
        }

        /// <summary>
        /// コンストラクタ,覚書一括入力画面から起動の場合
        /// </summary>
       /// <param name="lastRunMethod">受入入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public M421Form(LastRunMethod lastRunMethod = null)
        {
            this.InitializeComponent();
            this.closeMethod = lastRunMethod;
            //覚書一括入力画面から起動の場合
            this.seniMotoFlg = 1;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new M421Logic(this);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();
           
        }
       
    
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

       
    }
}
