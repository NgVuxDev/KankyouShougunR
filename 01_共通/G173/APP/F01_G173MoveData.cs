using System;
using System.Windows.Forms;
using r_framework.Authority;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using Shougun.Core.Common.KokyakuKarute.Const;
using Shougun.Core.Common.KokyakuKarute.Logic;

namespace Shougun.Core.Common.KokyakuKarute.APP
{
    public partial class F01_G173MoveData : Form
    {
        //データ移動用ボタン列挙
        Button[] buttonarray;

        /// <summary>
        /// データ移動用 取引先
        /// </summary>
        internal string moveData1;
        /// <summary>
        /// データ移動用 業者
        /// </summary>
        internal string moveData2;
        /// <summary>
        /// データ移動用 現場
        /// </summary>
        internal string moveData3;
        /// <summary>
        /// KeyUpイベント中か判定フラグ
        /// </summary>
        private bool isKeyUp;

        private int ukeireShukaGamenSizeKbn;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        /// <param name="ukeireShukaGamenSizeKbn">受入出荷画面サイズ区分</param>
        public F01_G173MoveData(string torihikisakiCd, string gyousyaCd, string genbaCd, int ukeireShukaGamenSizeKbn)
        {
            InitializeComponent();

            buttonInit();

            //データ移動引数
            this.moveData1 = torihikisakiCd;
            this.moveData2 = gyousyaCd;
            this.moveData3 = genbaCd;
            this.ukeireShukaGamenSizeKbn = ukeireShukaGamenSizeKbn;
        }

        /// <summary>
        /// ボタンイベント設定
        /// </summary>
        private void buttonInit()
        {
            buttonarray = new Button[] { this.customButton1, this.customButton2, this.customButton3 ,
                                         this.customButton4, this.customButton6 , this.customButton9 ,
                                         this.customButton10, this.customButton11, this.customButton12 ,
                                         this.customButton13, this.customButton14, this.customButton15 ,
                                         this.customButton16, this.customButton17, this.customButton18};

            //１～１5のボタンクリック設定
            for (int i = 0; i < buttonarray.Length; i++)
            {
                buttonarray[i].Click += new EventHandler(this.bt_Click);
            }
        }

        /// <summary>
        /// 1～15のボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Click(object sender, EventArgs e)
        {
            Button bt_clk = (Button)sender;
            int btNum = 0;
            foreach (Button bts in buttonarray)
            {
                if (bts.Text == bt_clk.Text)
                {
                    break;
                }
                btNum++;
            }

            // FormID取得
            var formID = GetFormID(btNum);

            switch (btNum)
            {
                case 0:
                    //[1]見積
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, moveData1, moveData2, moveData3);
                    break;
                case 1:
                    //[2]受付（収集）
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, moveData1, moveData2, moveData3);
                    break;
                case 2:
                    //[3]受付（持込）
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, moveData1, moveData2, moveData3);
                    break;
                case 3:
                    //[4]受付（出荷）
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, moveData1, moveData2, moveData3);
                    break;
                case 4:
                    //[5]受付（クレーム）
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, moveData1, moveData2, moveData3);
                    break;
                // Ver 2.0では計量入力は未リリースのため以下を非表示とする
                //case 5:
                //    //[6]計量(受入)
                //    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, moveData1, moveData2, moveData3, ((int)KokyakuKaruteConstans.IN_OUT_KBN.IN).ToString());
                //    break;
                //case 6:
                //    //[7]計量(出荷)
                //    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, moveData1, moveData2, moveData3, ((int)KokyakuKaruteConstans.IN_OUT_KBN.OUT).ToString());
                //    break;
                case 5:
                    //[8]受入
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, null, null, moveData1, moveData2, moveData3);
                    break;
                case 6:
                    //[9]出荷
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, null, null, moveData1, moveData2, moveData3);
                    break;
                case 7:
                    //[10]売上/支払
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, moveData1, moveData2, moveData3);
                    break;
                case 8:
                    //[11]入金
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, moveData1, moveData2, moveData3);
                    break;
                case 9:
                    //[12]出金
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, moveData1, moveData2, moveData3);
                    break;
                case 10:
                    //[13]産廃マニフェスト
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, moveData1, moveData2, moveData3);
                    break;
                case 11:
                    //[14]積替マニフェスト
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, moveData1, moveData2, moveData3);
                    break;
                case 12:
                    //[15]建廃マニフェスト
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, moveData1, moveData2, moveData3);
                    break;
                case 13:
                    //[16]電子マニフェスト
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, moveData1, moveData2, moveData3);
                    break;
                case 14:
                    //[17]運賃
                    FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, moveData1, moveData2, moveData3);
                    break;
                default:
                    // NOTHING
                    break;
            }

        }

        /// <summary>
        /// ボタン数値に該当するFormIDを取得
        /// </summary>
        /// <param name="btNum">ボタンに記載されている数値</param>
        /// <returns></returns>
        private string GetFormID(int btNum)
        {
            string formID = string.Empty;

            // 記載数値より-1で判定
            switch (btNum)
            {
                case 0:
                    //[1]見積
                    formID = "G276";
                    break;
                case 1:
                    //[2]受付（収集）
                    formID = "G015";
                    break;
                case 2:
                    //[3]受付（持込）
                    formID = "G018";
                    break;
                case 3:
                    //[4]受付（出荷）
                    formID = "G016";
                    break;
                case 4:
                    //[5]受付（クレーム）
                    formID = "G020";
                    break;
                // Ver 2.0では計量入力は未リリースのため以下を非表示とする
                //case 5:
                //    //[6]計量(受入)
                //    formID = "G045";
                //    break;
                //case 6:
                //    //[7]計量(出荷)
                //    formID = "G045";
                //    break;
                case 5:
                    //[8]受入
                    if (ukeireShukaGamenSizeKbn == 1)
                    {
                        formID = "G721";
                    }
                    else
                    {
                        formID = "G051";
                    }
                    break;
                case 6:
                    //[9]出荷
                    if (ukeireShukaGamenSizeKbn == 1)
                    {
                        formID = "G722";
                    }
                    else
                    {
                        formID = "G053";
                    }
                    break;
                case 7:
                    //[10]売上/支払
                    formID = "G054";
                    break;
                case 8:
                    //[11]入金
                    formID = "G619";
                    break;
                case 9:
                    //[12]出金
                    formID = "G090";
                    break;
                case 10:
                    //[13]産廃マニフェスト
                    formID = "G119";
                    break;
                case 11:
                    //[14]積替マニフェスト
                    formID = "G120";
                    break;
                case 12:
                    //[15]建廃マニフェスト
                    formID = "G121";
                    break;
                case 13:
                    //[16]電子マニフェスト
                    formID = "G141";
                    break;
                case 14:
                    //[17]運賃
                    formID = "G153";
                    break;
                default:
                    // NOTHING
                    break;
            }

            return formID;
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 数字入力でボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void execButtonNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isKeyUp)
            {
                // アラート表示後のEnter押下による無限ループ対策
                // 一度、KeyUpイベントをスキップさせる
                isKeyUp = false;
                return;
            }

            isKeyUp = true;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    // 未入力の場合は何もしない
                    if (string.IsNullOrWhiteSpace(execButtonNo.Text))
                    {
                        break;
                    }

                    // 実行
                    int i = Int32.Parse(execButtonNo.Text);

                    //1～15の範囲で入力
                    if (1 <= i && i <= 15)
                    {
                        i--;

                        var formID = GetFormID(i);
                        var hasAuth = Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG, false);

                        buttonarray[i].PerformClick();//ボタンクリック処理
                        execButtonNo.SelectAll();

                        if (!hasAuth)
                        {
                            // メニュー権限が無い場合、isKeyUpのフラグはまだ初期化しない
                            // アラート表示後、Enter押下すると無限ループとなり抜けれなくなるため
                            return;
                        }
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShowError("番号は1から15の間で入力してください。");
                    }
                    break;
                case Keys.F12:
                    // 閉じる
                    this.bt_ptn12_Click(sender, null);
                    break;
                default:
                    // NOTHING
                    break;
            }

            isKeyUp = false;
        }

        private void F01_G173MoveData_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    // 閉じる
                    this.bt_ptn12_Click(sender, null);
                    break;
                default:
                    // NOTHING
                    break;
            }
        }
    }
}
