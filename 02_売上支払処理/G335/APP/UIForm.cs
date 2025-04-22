using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;

namespace Shougun.Core.SalesPayment.DenpyouHakou
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        private DialogResult m_Result = DialogResult.Cancel;

        //変更前税計算区分、税区分情報
        internal string tempSeiZeikeisanKBN = string.Empty;
        internal string tempSeiZeiKBN = string.Empty;
        internal string tempShiZeikeisanKBN = string.Empty;
        internal string tempShiZeiKBN = string.Empty;

        public UIForm(ParameterDTOClass ParameterDTO, String tenpyoModel, String tenpyoKbn, bool kakuteiKbn)
            : base(WINDOW_ID.T_DENPYOU_HAKKOU, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            //パラメータ設定
            Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.Uriage_Date = ParameterDTO.Uriage_Date;
            //伝票モード
            Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel = tenpyoModel;
            //伝種区分
            Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn = tenpyoKbn;
            //伝票エンティティ
            this.ParameterDTO = ParameterDTO;

            this.KakuteiKbn = kakuteiKbn;

            this.seikyuGenkinTorihikiFlg = false;
            this.shiharaiGenkinTorihikiFlg = false;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Logic.WindowInit();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 伝票エンティティ
        /// </summary>
        public ParameterDTOClass ParameterDTO { get; private set; }

        /// <summary>
        /// 呼び出し元の確定フラグ
        /// ※明細毎になったらこのフラグだけでは駄目と思われます
        /// </summary>
        internal bool KakuteiKbn { get; set; }

        /// <summary>
        /// 請求、支払転換フラグ
        /// </summary>
        private Boolean layoutChangeFlg = false;
        public Boolean LayoutChangeFlg
        {
            get
            {
                return layoutChangeFlg;
            }
            set
            {
                layoutChangeFlg = value;
            }
        }
        /// <summary>請求取引先現金取引フラグ</summary>
        internal bool seikyuGenkinTorihikiFlg { get; set; }
        /// <summary>支払取引先現金取引フラグ</summary>
        internal bool shiharaiGenkinTorihikiFlg { get; set; }
        /// <summary>
        ///請求分用の税率
        /// </summary>
        public string SeikyuShouhizeiRate { get; set; }
        /// <summary>
        ///支払分用の税率
        /// </summary>
        public string ShiharaiShouhizeiRate { get; set; }
        /// <summary>
        /// 請求取引の精算区分連動
        /// </summary>
        public void SeikyuSeisanChange(object sender, EventArgs e)
        {
            //相殺設定
            if (!this.Logic.SetSousatu())
            {
                return;
            }
            if (Const.ConstClass.SEISAN_KBN_1.Equals(this.SEIKYU_SEISAN_VALUE.Text))
            {
                //請求分の入出金額に初期保存の請求差引残高を設定する。
                this.Seikyu_Nyusyu_Kingaku.Text = this.Seikyu_Sagaku_Zentaka.Text;
                //請求分の差引残高に0を設定する。
                this.Seikyu_Sagaku_Zentaka.Text = Const.ConstClass.KIGAKU_0;
            }
            else if (Const.ConstClass.SEISAN_KBN_2.Equals(this.SEIKYU_SEISAN_VALUE.Text))
            {
                if (Const.ConstClass.TORIHIKI_KBN_1.Equals(this.SEIKYU_TORIHIKI_VALUE.Text))
                {
                    //現金
                    //請求分の入出金額に今回取引の請求額を設定する。
                    this.Seikyu_Nyusyu_Kingaku.Text = this.Seikyu_Konkai_Rorihiki.Text;
                    //請求分の差引残高に0を設定する。
                    this.Seikyu_Sagaku_Zentaka.Text = Const.ConstClass.KIGAKU_0;
                }
                else if (Const.ConstClass.TORIHIKI_KBN_2.Equals(this.SEIKYU_TORIHIKI_VALUE.Text))
                {
                    //掛け取引
                    //請求分の入出金額に0を設定する。
                    this.Seikyu_Nyusyu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                }
                //請求分相殺金額に0を設定する。
                this.Seikyu_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                //支払分相殺金額に0を設定する。
                this.Shiharai_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                // 差引残高を再計算
                if (!this.Logic.SetSeikyuKingaku1022())
                {
                    return;
                }
            }

            //【請求】税計算区分：2.請求毎のEnableとTextBoxの入力を制御します。
            this.Logic.SeikyuuMaiZeiEnable();

            //合計金額計算（上段-下段）
            this.Logic.SetGokei();
        }
        /// <summary>
        /// 支払取引の精算区分連動
        /// </summary>
        public void ShiharaiSeisanChange(object sender, EventArgs e)
        {
            //相殺設定
            if (!this.Logic.SetSousatu())
            {
                return;
            }
            if (Const.ConstClass.SEISAN_KBN_1.Equals(this.SHIHARAI_SEISAN_VALUE.Text))
            {
                //支払分の入出金額に初期保存の支払差引残高を設定する。
                this.Shiharai_Nyusyu_Kingaku.Text = this.Shiharai_Sagaku_Zentaka.Text;
                //支払分の差引残高に0を設定する。
                this.Shiharai_Sagaku_Zentaka.Text = Const.ConstClass.KIGAKU_0;
            }
            else if (Const.ConstClass.SEISAN_KBN_2.Equals(this.SHIHARAI_SEISAN_VALUE.Text))
            {
                if (Const.ConstClass.TORIHIKI_KBN_1.Equals(this.SHIHARAI_TORIHIKI_VALUE.Text))
                {
                    //現金
                    //支払分の入出金額に今回取引の支払額を設定する。
                    this.Shiharai_Nyusyu_Kingaku.Text = this.Shiharai_Konkai_Rorihiki.Text;
                    //支払分の差引残高に0を設定する。
                    this.Shiharai_Sagaku_Zentaka.Text = Const.ConstClass.KIGAKU_0;
                }
                else if (Const.ConstClass.TORIHIKI_KBN_2.Equals(this.SHIHARAI_TORIHIKI_VALUE.Text))
                {
                    //掛け取引
                    //支払分の入出金額に0を設定する。
                    this.Shiharai_Nyusyu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                }
                //支払分相殺金額に0を設定する。
                this.Shiharai_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                //支払分相殺金額に0を設定する。
                this.Seikyu_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                // 差引残高を再計算
                if (!this.Logic.SetShiharaiKingaku1022())
                {
                    return;
                }
            }

            //【支払】税計算区分：2.精算毎のEnableとTextBoxの入力を制御します。
            this.Logic.SeisanMaiZeiEnable();

            //合計金額計算（上段-下段）
            this.Logic.SetGokei();
        }
        /// <summary>
        /// 請求取引の取引区分連動
        /// </summary>
        private void SeikyuTorihikiChanged(object sender, EventArgs e)
        {
            bool isShuuseiMode = Const.ConstClass.TENPYO_MODEL_2.Equals(Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel);

            //「請求取引」の値が現金の場合
            if (Const.ConstClass.TORIHIKI_KBN_1.Equals(this.SEIKYU_TORIHIKI_VALUE.Text))
            {
                //【領収証】【領収証有り】【領収証無し】を編集可とする
                this.RYOSYUSYO_VALUE.Enabled = true;
                this.RYOSYUSYO_KBN_1.Enabled = true;
                this.RYOSYUSYO_KBN_2.Enabled = true;

                // 精算区分の値を「しない」にし、使用不可にする
                this.SEIKYU_SEISAN_KBN_2.Checked = true;
                this.SEIKYU_SEISAN_KBN_1.Enabled = false;
                this.SEIKYU_SEISAN_KBN_2.Enabled = false;
                this.SEIKYU_SEISAN_VALUE.Enabled = false;
            }
            //「請求取引」の値が掛けの場合
            else
            {
                //【領収証】【領収証有り】【領収証無し】を編集不可とする
                this.RYOSYUSYO_VALUE.Enabled = false;
                this.RYOSYUSYO_KBN_1.Enabled = false;
                this.RYOSYUSYO_KBN_2.Enabled = false;

                // 新規登録かつ確定時のみ精算可能
                if (!isShuuseiMode && this.KakuteiKbn)
                {
                    // 精算区分の値を「しない」にし、精算区分を使用可にする
                    this.SEIKYU_SEISAN_KBN_2.Checked = true;
                    this.SEIKYU_SEISAN_KBN_1.Enabled = true;
                    this.SEIKYU_SEISAN_KBN_2.Enabled = true;
                    this.SEIKYU_SEISAN_VALUE.Enabled = true;
                }
            }

            //【請求】税計算区分：2.請求毎のEnableとTextBoxの入力を制御します。
            this.Logic.SeikyuuMaiZeiEnable();

            this.RYOSYUSYO_KBN_1_CheckedChanged(this.RYOSYUSYO_KBN_1, new EventArgs());
            this.SeikyuSeisanChange(this.SEIKYU_SEISAN_VALUE, new EventArgs());

            // キャッシャ連動状態セット
            this.Logic.setCasherEnabled(true);
        }
        /// <summary>
        /// 支払取引の取引区分連動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiharaiTorihikiChanged(object sender, EventArgs e)
        {
            bool isShuuseiMode = Const.ConstClass.TENPYO_MODEL_2.Equals(Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel);

            //「支払取引」の値が現金の場合
            if (Const.ConstClass.TORIHIKI_KBN_1.Equals(this.SHIHARAI_TORIHIKI_VALUE.Text))
            {
                // 精算区分の値を「しない」にし、使用不可にする
                this.SHIHARAI_SEISAN_KBN_2.Checked = true;
                this.SHIHARAI_SEISAN_KBN_1.Enabled = false;
                this.SHIHARAI_SEISAN_KBN_2.Enabled = false;
                this.SHIHARAI_SEISAN_VALUE.Enabled = false;

                // 支払明細が存在する(支払取引)場合
                if ((this.Logic.ShiharaiDenpyouCheck()))
                {
                    //【領収証】【領収証有り】【領収証無し】を編集不可
                    this.RYOSYUSYO_VALUE.Enabled = false;
                    this.RYOSYUSYO_KBN_1.Enabled = false;
                    this.RYOSYUSYO_KBN_2.Enabled = false;
                }
            }
            //「請求取引」の値が掛けの場合
            else
            {
                // 新規登録かつ確定時のみ精算可能
                if (!isShuuseiMode && this.KakuteiKbn)
                {
                    // 精算区分の値を「しない」にし、精算区分を使用可にする
                    this.SHIHARAI_SEISAN_KBN_2.Checked = true;
                    this.SHIHARAI_SEISAN_KBN_1.Enabled = true;
                    this.SHIHARAI_SEISAN_KBN_2.Enabled = true;
                    this.SHIHARAI_SEISAN_VALUE.Enabled = true;
                }
                // 支払明細が存在する(支払取引)場合
                if ((this.Logic.ShiharaiDenpyouCheck()))
                {
                    //【領収証】【領収証有り】【領収証無し】を編集不可
                    this.RYOSYUSYO_VALUE.Enabled = false;
                    this.RYOSYUSYO_KBN_1.Enabled = false;
                    this.RYOSYUSYO_KBN_2.Enabled = false;
                }
            }

            //【支払】税計算区分：2.精算毎のEnableとTextBoxの入力を制御します。
            this.Logic.SeisanMaiZeiEnable();

            this.RYOSYUSYO_KBN_1_CheckedChanged(this.RYOSYUSYO_KBN_1, new EventArgs());
            this.ShiharaiSeisanChange(this.SHIHARAI_SEISAN_VALUE, new EventArgs());

            // キャッシャ連動状態セット
            this.Logic.setCasherEnabled(true);
        }
        /// <summary>
        /// 3331相殺連動
        /// </summary>
        private void SousatuChange(object sender, EventArgs e)
        {
            //相殺計算
            this.Logic.SousatuKeisan();
        }

        /// <summary>
        /// [F1] 残高取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void GetZanda(object sender, EventArgs e)
        {
            // システム設定を無視して強制的に残高を取得するフラグ
            this.Logic.GetZandakaClickFlg = true;

            // 請求毎の場合相殺はできない
            if (!this.Logic.SetSousatu())
            {
                return;
            }
            if (!this.Logic.SetZenkaiZentaka1017())
            {
                return;
            }
            if (!this.Logic.SetSeikyuKonkaiZeigaku())
            {
                return;
            }
            if (!this.Logic.SetSeikyuKingaku1022())
            {
                return;
            }
            if (!this.Logic.SousatuKeisan())
            {
                return;
            }
            //合計再計算
            if (!this.Logic.SetGokei())
            {
                return;
            }
        }

        /// <summary>
        /// 実行処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            // 月次処理 - 月次ロックチェック
            if (this.Logic.GetsujiLockCheck())
            {
                return;
            }

            //領収書/仕切書チェック、登録番号チェック
            if (this.Logic.Ryousyu_ShikiriCheck())
            {
                return;
            }

            if (!this.RegistErrorFlag)
            {
                //請求取引_税計算区分
                this.ParameterDTO.Seikyu_Zeikeisan_Kbn = this.SEIKYU_ZEIKEISAN_VALUE.Text;
                //請求取引_税区分
                this.ParameterDTO.Seikyu_Zei_Kbn = this.SEIKYU_ZEI_VALUE.Text;
                //請求取引_取引区分
                this.ParameterDTO.Seikyu_Rohiki_Kbn = this.SEIKYU_TORIHIKI_VALUE.Text;
                //請求取引_精算区分
                this.ParameterDTO.Seikyu_Seisan_Kbn = this.SEIKYU_SEISAN_VALUE.Text;
                //請求取引_伝票発行区分
                this.ParameterDTO.Seikyu_Hakou_Kbn = this.SEIKYU_DENPYO_VALUE.Text;
                //支払取引_税計算区分
                this.ParameterDTO.Shiharai_Zeikeisan_Kbn = this.SHIHARAI_ZEIKEISAN_VALUE.Text;
                //支払取引_税区分
                this.ParameterDTO.Shiharai_Zei_Kbn = this.SHIHARAI_ZEI_VALUE.Text;
                //支払取引_取引区分
                this.ParameterDTO.Shiharai_Rohiki_Kbn = this.SHIHARAI_TORIHIKI_VALUE.Text;
                //支払取引_精算区分
                this.ParameterDTO.Shiharai_Seisan_Kbn = this.SHIHARAI_SEISAN_VALUE.Text;
                //支払取引_伝票発行区分
                this.ParameterDTO.Shiharai_Hakou_Kbn = this.SHIHARAI_DENPYO_VALUE.Text;
                //相殺
                this.ParameterDTO.Sosatu = this.SOUSATU_VALUE.Text;
                //発行区分
                this.ParameterDTO.Hakou_Kbn = this.HAKOU_VALUE.Text;
                //請求分前回残高
                this.ParameterDTO.Seikyu_Zenkai_Zentaka = this.Seikyu_Zenkai_Zentaka.Text;
                //請求分今回金額
                this.ParameterDTO.Seikyu_Konkai_Kingaku = this.Seikyu_Konkai_Kingaku.Text;
                //請求分今回税額
                this.ParameterDTO.Seikyu_Konkai_Zeigaku = this.Seikyu_Konkai_Zeigaku.Text;
                //請求分今回取引
                this.ParameterDTO.Seikyu_Konkai_Rorihiki = this.Seikyu_Konkai_Rorihiki.Text;
                //請求分相殺金額
                this.ParameterDTO.Seikyu_Sousatu_Kingaku = this.Seikyu_Sousatu_Kingaku.Text;
                //請求分入出金額
                this.ParameterDTO.Seikyu_Nyusyu_Kingaku = this.Seikyu_Nyusyu_Kingaku.Text;
                //請求分差引残高
                this.ParameterDTO.Seikyu_Sagaku_Zentaka = this.Seikyu_Sagaku_Zentaka.Text;
                //請求分消費税率
                this.ParameterDTO.Seikyu_Syohizei_Ritu = this.SeikyuShouhizeiRate;
                //支払分前回残高
                this.ParameterDTO.Shiharai_Zenkai_Zentaka = this.Shiharai_Zenkai_Zentaka.Text;
                //支払分今回金額
                this.ParameterDTO.Shiharai_Konkai_Kingaku = this.Shiharai_Konkai_Kingaku.Text;
                //支払分今回税額
                this.ParameterDTO.Shiharai_Konkai_Zeigaku = this.Shiharai_Konkai_Zeigaku.Text;
                //支払分今回取引
                this.ParameterDTO.Shiharai_Konkai_Rorihiki = this.Shiharai_Konkai_Rorihiki.Text;
                //支払分相殺金額
                this.ParameterDTO.Shiharai_Sousatu_Kingaku = this.Shiharai_Sousatu_Kingaku.Text;
                //支払分入出金額
                this.ParameterDTO.Shiharai_Nyusyu_Kingaku = this.Shiharai_Nyusyu_Kingaku.Text;
                //支払分差引残高
                this.ParameterDTO.Shiharai_Sagaku_Zentaka = this.Shiharai_Sagaku_Zentaka.Text;
                //支払分消費税率
                this.ParameterDTO.Shiharai_Syohizei_Ritu = this.ShiharaiShouhizeiRate;
                //今回金額合計
                this.ParameterDTO.Gokei_Konkai_Kingaku = this.Gokei_Konkai_Kingaku.Text;
                //今回税額合計
                this.ParameterDTO.Gokei_Konkai_Zeigaku = this.Gokei_Konkai_Zeigaku.Text;
                //今回取引合計
                this.ParameterDTO.Gokei_Konkai_Rorihiki = this.Gokei_Konkai_Rorihiki.Text;
                //相殺金額合計
                this.ParameterDTO.Gokei_Sousatu_Kingaku = this.Gokei_Sousatu_Kingaku.Text;
                //入出金額合計
                this.ParameterDTO.Gokei_Nyusyu_Kingaku = this.Gokei_Nyusyu_Kingaku.Text;
                //差引残高合計
                this.ParameterDTO.Gokei_Sagaku_Zentaka = this.Gokei_Sagaku_Zentaka.Text;
                //領収証（取引区分が掛けの時は選択にかかわらず領収書を発行しない）
                if (ConstClass.TORIHIKI_KBN_2 == this.SEIKYU_TORIHIKI_VALUE.Text)
                {
                    this.ParameterDTO.Ryousyusyou = ConstClass.RYOSYUSYO_KBN_2;
                }
                else
                {
                    this.ParameterDTO.Ryousyusyou = this.RYOSYUSYO_VALUE.Text;
                }
                //敬称1
                this.ParameterDTO.Keisyou_1 = this.Keisyou_1.Text;
                //敬称2
                this.ParameterDTO.Keisyou_2 = this.Keisyou_2.Text;

                if (!this.Tadasi_Kaki.ReadOnly)
                {
                    this.ParameterDTO.Tadasi_Kaki = this.Tadasi_Kaki.Text.Trim();
                }

                // 計量票発行区分
                this.ParameterDTO.Keiryou_Prirnt_Kbn_Value = this.KEIRYOU_PRIRNT_KBN_VALUE.Text;

                // キャッシャ連動
                this.ParameterDTO.Kyasya = this.KYASYA_VALUE.Text;
                
                m_Result = DialogResult.OK;

                if (RYOSYUSYO_VALUE.Enabled && RYOSYUSYO_VALUE.Text.Equals(RYOSYUSYO_KBN_1.Value))
                {
                    this.Logic.SetStatus();    // No.4087
                }

                //	領収書/仕切書_売上)課税金額
                //	領収書/仕切書_売上)課税消費税
                //	領収書/仕切書_売上)非課税金額
                this.Logic.SetRYOSYUSYO();

                //仕切書_支払)課税金額
                //仕切書_支払)課税消費税
                //仕切書_支払)非課税金額
                this.Logic.SetSHIKIRISHO_SHIHARAI();

                // Formクローズ処理
                FormClose(sender, e);
            }
        }
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            //画面クローズ
            parentForm.DialogResult = m_Result;
            this.Close();
            parentForm.Close();
        }
        /// <summary>
        ///請求税額計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSeikyuKonkaiZeigaku(object sender, EventArgs e)
        {
            bool isShuuseiMode = Const.ConstClass.TENPYO_MODEL_2.Equals(Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel);

            //適格請求書用チェック
            //税計算区分=3 : 明細毎
            //税区分 = 1:外税
            if (!this.Logic.valueInitFlag)
            {
                if ((Const.ConstClass.ZEIKEISAN_KBN_3.Equals(this.SEIKYU_ZEIKEISAN_VALUE.Text)) &&
                    (Const.ConstClass.ZEI_KBN_1.Equals(this.SEIKYU_ZEI_VALUE.Text)))
                {

                    DialogResult result = 0;

                    result = MessageBox.Show("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした請求書や仕切書になりませんがよろしいでしょうか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        //税計算区分、税区分を元に戻す
                        this.SEIKYU_ZEIKEISAN_VALUE.Text = tempSeiZeikeisanKBN;
                        this.SEIKYU_ZEI_VALUE.Text = tempSeiZeiKBN;
                        return;
                    }
                }
            }


            if (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.SEIKYU_ZEIKEISAN_VALUE.Text) ||
                Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.SEIKYU_ZEIKEISAN_VALUE.Text))
            {
                // 税計算区分が「伝票毎」または「請求毎」の場合、税区分の「内税」を使用不可 税区分に「内税」が指定されていれば「外税」にする
                this.SEIKYU_ZEI_KBN_2.Enabled = false;
                if (Const.ConstClass.ZEI_KBN_2.Equals(this.SEIKYU_ZEI_VALUE.Text))
                {
                    this.SEIKYU_ZEI_VALUE.Text = Const.ConstClass.ZEI_KBN_1;
                }

                // 現金取引の場合は取引区分の有効/無効操作を行わない
                if(this.seikyuGenkinTorihikiFlg == false)
                {
                    // 取引区分の値を「掛け」で固定して、使用不可にする
                    if(this.KakuteiKbn)
                    {
                        // 税計算区分が「請求毎」の場合
                        if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.SEIKYU_ZEIKEISAN_VALUE.Text))
                        {
                            this.SEIKYU_TORIHIKI_KBN_2.Checked = true;
                            this.SEIKYU_TORIHIKI_KBN_1.Enabled = false;
                            this.SEIKYU_TORIHIKI_KBN_2.Enabled = false;
                            this.SEIKYU_TORIHIKI_VALUE.Enabled = false;
                            //20150615 #1332 hoanghm start
                            //請求毎のときは税金があいまいなため精算不可
                            this.SEIKYU_SEISAN_KBN_1.Enabled = false;
                            this.SEIKYU_SEISAN_KBN_2.Enabled = false;
                            this.SEIKYU_SEISAN_VALUE.Enabled = false;
                            //20150615 #1332 hoanghm end
                        }
                        // 税計算区分が「伝票毎」の場合
                        else if (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.SEIKYU_ZEIKEISAN_VALUE.Text))
                        {
                            // 取引区分を使用可にする
                            this.SEIKYU_TORIHIKI_KBN_1.Enabled = true;
                            this.SEIKYU_TORIHIKI_KBN_2.Enabled = true;
                            this.SEIKYU_TORIHIKI_VALUE.Enabled = true;
                            //20150615 #1332 hoanghm start
                            // 新規登録かつ確定時のみ精算可能
                            if (!isShuuseiMode && this.KakuteiKbn)
                            {
                                // 精算区分の値を「しない」にし、使用不可にする
                                this.SEIKYU_SEISAN_KBN_2.Checked = true;
                                this.SEIKYU_SEISAN_KBN_1.Enabled = true;
                                this.SEIKYU_SEISAN_KBN_2.Enabled = true;
                                this.SEIKYU_SEISAN_VALUE.Enabled = true;
                            }
                            //20150615 #1332 hoanghm end
                        }
                    }
                }
            }
            else
            {
                // 税計算区分が「伝票毎」または「請求毎」以外の場合
                this.SEIKYU_ZEI_KBN_2.Enabled = true;

                // 現金取引の場合は取引区分の有効/無効操作を行わない
                if(this.seikyuGenkinTorihikiFlg == false)
                {
                    if(this.KakuteiKbn)
                    {
                        // 取引区分を使用可にする
                        this.SEIKYU_TORIHIKI_KBN_1.Enabled = true;
                        this.SEIKYU_TORIHIKI_KBN_2.Enabled = true;
                        this.SEIKYU_TORIHIKI_VALUE.Enabled = true;
                    }
                    // 新規登録かつ確定時のみ精算可能
                    if (!isShuuseiMode && this.KakuteiKbn)
                    {
                        // 精算区分の値を「しない」にし、使用不可にする
                        this.SEIKYU_SEISAN_KBN_2.Checked = true;
                        this.SEIKYU_SEISAN_KBN_1.Enabled = true;
                        this.SEIKYU_SEISAN_KBN_2.Enabled = true;
                        this.SEIKYU_SEISAN_VALUE.Enabled = true;
                    }
                }
            }

            //税計算区分、税区分の保持
            this.SaveOldZei();

            // 請求毎の場合相殺はできない
            if (!this.Logic.SetSousatu())
            {
                return;
            }
            if (!this.Logic.SetZenkaiZentaka1017())
            {
                return;
            }
            this.Logic.ErrHinmeiCD = string.Empty;  //エラー品名用
            if (!this.Logic.SetSeikyuKonkaiZeigaku())
            {
                return;
            }
            if ((!this.Logic.valueInitFlag) && (!string.IsNullOrEmpty(this.Logic.ErrHinmeiCD)))
            {
                this.Logic.ErrHinmeiCD = this.Logic.ErrHinmeiCD.Substring(0, this.Logic.ErrHinmeiCD.Length - 1);
                MessageBox.Show(string.Format("税区分が登録されている品名は、\r適格請求書の要件を満たした請求書になりません。\r（品名CD={0}）", this.Logic.ErrHinmeiCD), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (!this.Logic.SetSeikyuKingaku1022())
            {
                return;
            }
            if (!this.Logic.SousatuKeisan())
            {
                return;
            }
            //合計再計算
            if (!this.Logic.SetGokei())
            {
                return;
            }
        }
        /// <summary>
        /// 支払税額計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetShiharaiKonkaiZeigaku(object sender, EventArgs e)
        {
            bool isShuuseiMode = Const.ConstClass.TENPYO_MODEL_2.Equals(Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel);

            //適格請求書用チェック
            //税計算区分=3 : 明細毎
            //税区分 = 1:外税
            if (!this.Logic.valueInitFlag)
            {
                if ((Const.ConstClass.ZEIKEISAN_KBN_3.Equals(this.SHIHARAI_ZEIKEISAN_VALUE.Text)) &&
                    (Const.ConstClass.ZEI_KBN_1.Equals(this.SHIHARAI_ZEI_VALUE.Text)))
                {

                    DialogResult result = 0;

                    result = MessageBox.Show("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした仕切書になりませんがよろしいでしょうか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        //税計算区分、税区分を元に戻す
                        this.SHIHARAI_ZEIKEISAN_VALUE.Text = tempShiZeikeisanKBN;
                        this.SHIHARAI_ZEI_VALUE.Text = tempShiZeiKBN;
                        return;
                    }
                }
            }

            if (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.SHIHARAI_ZEIKEISAN_VALUE.Text) ||
                Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.SHIHARAI_ZEIKEISAN_VALUE.Text))
            {
                // 税計算区分が「伝票毎」または「請求毎」の場合、税区分の「内税」を使用不可 税区分に「内税」が指定されていれば「外税」にする
                this.SHIHARAI_ZEI_KBN_2.Enabled = false;
                if (Const.ConstClass.ZEI_KBN_2.Equals(this.SHIHARAI_ZEI_VALUE.Text))
                {
                    this.SHIHARAI_ZEI_VALUE.Text = Const.ConstClass.ZEI_KBN_1;
                }

                // 現金取引の場合は取引区分の有効/無効操作を行わない
                if(this.shiharaiGenkinTorihikiFlg == false)
                {
                    // 取引区分の値を「掛け」で固定して、使用不可にする
                    if(this.KakuteiKbn)
                    {
                        // 税計算区分が「請求毎」の場合
                        if(Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.SHIHARAI_ZEIKEISAN_VALUE.Text))
                        {
                            this.SHIHARAI_TORIHIKI_KBN_2.Checked = true;
                            this.SHIHARAI_TORIHIKI_KBN_1.Enabled = false;
                            this.SHIHARAI_TORIHIKI_KBN_2.Enabled = false;
                            this.SHIHARAI_TORIHIKI_VALUE.Enabled = false;
                            //20150615 #1332 hoanghm start
                            //請求毎のときは税金があいまいなため精算不可
                            this.SHIHARAI_SEISAN_KBN_1.Enabled = false;
                            this.SHIHARAI_SEISAN_KBN_2.Enabled = false;
                            this.SHIHARAI_SEISAN_VALUE.Enabled = false;
                            //20150615 #1332 hoanghm end
                        }
                        // 税計算区分が「伝票毎」の場合
                        if(Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.SHIHARAI_ZEIKEISAN_VALUE.Text))
                        {
                            // 取引区分を使用可にする
                            this.SHIHARAI_TORIHIKI_KBN_1.Enabled = true;
                            this.SHIHARAI_TORIHIKI_KBN_2.Enabled = true;
                            this.SHIHARAI_TORIHIKI_VALUE.Enabled = true;
                            //20150615 #1332 hoanghm start
                            // 新規登録かつ確定時のみ精算可能
                            if (!isShuuseiMode && this.KakuteiKbn)
                            {
                                // 精算区分の値を「しない」にし、精算区分を使用可にする
                                this.SHIHARAI_SEISAN_KBN_2.Checked = true;
                                this.SHIHARAI_SEISAN_KBN_1.Enabled = true;
                                this.SHIHARAI_SEISAN_KBN_2.Enabled = true;
                                this.SHIHARAI_SEISAN_VALUE.Enabled = true;
                            }
                            //20150615 #1332 hoanghm end
                        }
                    }
                }
            }
            else
            {
                // 税計算区分が「伝票毎」または「請求毎」以外の場合
                this.SHIHARAI_ZEI_KBN_2.Enabled = true;

                // 現金取引の場合は取引区分の有効/無効操作を行わない
                if(this.shiharaiGenkinTorihikiFlg == false)
                {
                    if(this.KakuteiKbn)
                    {
                        // 取引区分を使用可にする
                        this.SHIHARAI_TORIHIKI_KBN_1.Enabled = true;
                        this.SHIHARAI_TORIHIKI_KBN_2.Enabled = true;
                        this.SHIHARAI_TORIHIKI_VALUE.Enabled = true;
                    }
                    if (!isShuuseiMode && this.KakuteiKbn)
                    {
                        // 精算区分の値を「しない」にし、精算区分を使用可にする
                        this.SHIHARAI_SEISAN_KBN_2.Checked = true;
                        this.SHIHARAI_SEISAN_KBN_1.Enabled = true;
                        this.SHIHARAI_SEISAN_KBN_2.Enabled = true;
                        this.SHIHARAI_SEISAN_VALUE.Enabled = true;
                    }
                }
            }

            //税計算区分、税区分の保持
            this.SaveOldZei();

            // 請求毎の場合相殺はできない
            if (!this.Logic.SetSousatu())
            {
                return;
            }
            if (!this.Logic.SetZenkaiZentaka1017())
            {
                return;
            }
            this.Logic.ErrHinmeiCD = string.Empty;  //エラー品名用
            if (!this.Logic.SetShiharaiKonkaiZeigaku())
            {
                return;
            }
            if ((!this.Logic.valueInitFlag) && (!string.IsNullOrEmpty(this.Logic.ErrHinmeiCD)))
            {
                this.Logic.ErrHinmeiCD = this.Logic.ErrHinmeiCD.Substring(0, this.Logic.ErrHinmeiCD.Length - 1);
                MessageBox.Show(string.Format("税区分が登録されている品名は、\r適格請求書の要件を満たした支払明細書になりません。\r（品名CD={0}）", this.Logic.ErrHinmeiCD), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (!this.Logic.SetShiharaiKingaku1022())
            {
                return;
            }
            if (!this.Logic.SousatuKeisan())
            {
                return;
            }
            //合計再計算
            this.Logic.SetGokei();
        }

        /// <summary>
        /// 領収証ありチェック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void RYOSYUSYO_KBN_1_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeRyoushuushoStatus();
        }

        /// <summary>
        /// 領収書無しのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void RYOSYUSYO_KBN_2_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeRyoushuushoStatus();
        }

        /// <summary>
        /// 領収書の「有り」「無し」に応じて関連項目の状態を変更します
        /// </summary>
        private void ChangeRyoushuushoStatus()
        {
            // 請求取引区分が「掛け」の場合
            // 「但し書き」「敬称」は非活性とする

            bool isReadOnly = false;
            bool isTabStop = true;

            if (this.SEIKYU_TORIHIKI_VALUE.Text == ConstClass.TORIHIKI_KBN_2)
            {
                isReadOnly = true;
                isTabStop = false;
            }
            else if (this.RYOSYUSYO_KBN_2.Checked)
            {
                isReadOnly = true;
                isTabStop = false;
            }

            this.Tadasi_Kaki.ReadOnly = isReadOnly;
            this.Keisyou_1.ReadOnly = isReadOnly;
            this.Keisyou_2.ReadOnly = isReadOnly;

            this.Tadasi_Kaki.TabStop = isTabStop;
            this.Keisyou_1.TabStop = isTabStop;
            this.Keisyou_2.TabStop = isTabStop;

            // 但書き復元
            if (RYOSYUSYO_VALUE.Text.Equals(RYOSYUSYO_KBN_1.Value) && Tadasi_Kaki.ReadOnly == false)
            {
                this.Logic.GetStatus();
            }
        }

        /// <summary>
        /// 伝票発行の発行区分連動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenpyouHakkouChange(object sender, EventArgs e)
        {
            this.HAKOU_VALUE.Enabled = true;   // No.3239
            if (Const.ConstClass.TENPYO_KBN_1.Equals(this.SEIKYU_DENPYO_VALUE.Text))
            {   // 請求伝票発行有
                if (Const.ConstClass.TENPYO_KBN_1.Equals(this.SHIHARAI_DENPYO_VALUE.Text))
                {   // 支払伝票発行有
                    this.HAKOU_KBN_1.Enabled = true;
                    this.HAKOU_KBN_2.Enabled = true;
                    this.HAKOU_KBN_3.Enabled = true;
                }
                else if (Const.ConstClass.TENPYO_KBN_2.Equals(this.SHIHARAI_DENPYO_VALUE.Text))
                {   // 支払伝票発行無
                    this.HAKOU_KBN_1.Checked = true;
                    this.HAKOU_KBN_1.Enabled = true;
                    this.HAKOU_KBN_2.Enabled = false;
                    this.HAKOU_KBN_3.Enabled = false;
                }
            }
            else if (Const.ConstClass.TENPYO_KBN_2.Equals(this.SEIKYU_DENPYO_VALUE.Text))
            {   // 請求伝票発行無し
                if (Const.ConstClass.TENPYO_KBN_1.Equals(this.SHIHARAI_DENPYO_VALUE.Text))
                {   // 支払伝票発行有
                    this.HAKOU_KBN_1.Checked = true;
                    this.HAKOU_KBN_1.Enabled = true;
                    this.HAKOU_KBN_2.Enabled = false;
                    this.HAKOU_KBN_3.Enabled = false;
                }
                else if (Const.ConstClass.TENPYO_KBN_2.Equals(this.SHIHARAI_DENPYO_VALUE.Text))
                {   // 支払伝票発行無
                    this.HAKOU_KBN_1.Enabled = false;
                    this.HAKOU_KBN_2.Enabled = false;
                    this.HAKOU_KBN_3.Enabled = false;
                    this.HAKOU_VALUE.Enabled = false;   // No.3239
                }
            }
        }

        // No.4087-->
        /// <summary>
        /// 領収書有無変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ryosyusyo_TextChanged(object sender, EventArgs e)
        {
            if (RYOSYUSYO_VALUE.Text.Equals(RYOSYUSYO_KBN_1.Value) && Tadasi_Kaki.ReadOnly == false)
            {
                this.Logic.GetStatus();
            }
        }
        // No.4087<--

        /// <summary>
        ///受入出荷入力から実行処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public DialogResult UKEIRE_SHUKKA_Regist()
        {
            base.OnLoad(new EventArgs());
            this.Logic.WindowInit();

            //領収書/仕切書チェック、登録番号チェック
            if (this.Logic.Ryousyu_ShikiriCheck())
            {
                return DialogResult.Cancel;
            }

            //請求取引_税計算区分
            this.ParameterDTO.Seikyu_Zeikeisan_Kbn = this.SEIKYU_ZEIKEISAN_VALUE.Text;
            //請求取引_税区分
            this.ParameterDTO.Seikyu_Zei_Kbn = this.SEIKYU_ZEI_VALUE.Text;
            //請求取引_取引区分
            this.ParameterDTO.Seikyu_Rohiki_Kbn = this.SEIKYU_TORIHIKI_VALUE.Text;
            //請求取引_精算区分
            this.ParameterDTO.Seikyu_Seisan_Kbn = this.SEIKYU_SEISAN_VALUE.Text;
            //請求取引_伝票発行区分
            this.ParameterDTO.Seikyu_Hakou_Kbn = this.SEIKYU_DENPYO_VALUE.Text;
            //支払取引_税計算区分
            this.ParameterDTO.Shiharai_Zeikeisan_Kbn = this.SHIHARAI_ZEIKEISAN_VALUE.Text;
            //支払取引_税区分
            this.ParameterDTO.Shiharai_Zei_Kbn = this.SHIHARAI_ZEI_VALUE.Text;
            //支払取引_取引区分
            this.ParameterDTO.Shiharai_Rohiki_Kbn = this.SHIHARAI_TORIHIKI_VALUE.Text;
            //支払取引_精算区分
            this.ParameterDTO.Shiharai_Seisan_Kbn = this.SHIHARAI_SEISAN_VALUE.Text;
            //支払取引_伝票発行区分
            this.ParameterDTO.Shiharai_Hakou_Kbn = this.SHIHARAI_DENPYO_VALUE.Text;
            //相殺
            this.ParameterDTO.Sosatu = this.SOUSATU_VALUE.Text;
            //発行区分
            this.ParameterDTO.Hakou_Kbn = this.HAKOU_VALUE.Text;
            //請求分前回残高
            this.ParameterDTO.Seikyu_Zenkai_Zentaka = this.Seikyu_Zenkai_Zentaka.Text;
            //請求分今回金額
            this.ParameterDTO.Seikyu_Konkai_Kingaku = this.Seikyu_Konkai_Kingaku.Text;
            //請求分今回税額
            this.ParameterDTO.Seikyu_Konkai_Zeigaku = this.Seikyu_Konkai_Zeigaku.Text;
            //請求分今回取引
            this.ParameterDTO.Seikyu_Konkai_Rorihiki = this.Seikyu_Konkai_Rorihiki.Text;
            //請求分相殺金額
            this.ParameterDTO.Seikyu_Sousatu_Kingaku = this.Seikyu_Sousatu_Kingaku.Text;
            //請求分入出金額
            this.ParameterDTO.Seikyu_Nyusyu_Kingaku = this.Seikyu_Nyusyu_Kingaku.Text;
            //請求分差引残高
            this.ParameterDTO.Seikyu_Sagaku_Zentaka = this.Seikyu_Sagaku_Zentaka.Text;
            //請求分消費税率
            this.ParameterDTO.Seikyu_Syohizei_Ritu = this.SeikyuShouhizeiRate;
            //支払分前回残高
            this.ParameterDTO.Shiharai_Zenkai_Zentaka = this.Shiharai_Zenkai_Zentaka.Text;
            //支払分今回金額
            this.ParameterDTO.Shiharai_Konkai_Kingaku = this.Shiharai_Konkai_Kingaku.Text;
            //支払分今回税額
            this.ParameterDTO.Shiharai_Konkai_Zeigaku = this.Shiharai_Konkai_Zeigaku.Text;
            //支払分今回取引
            this.ParameterDTO.Shiharai_Konkai_Rorihiki = this.Shiharai_Konkai_Rorihiki.Text;
            //支払分相殺金額
            this.ParameterDTO.Shiharai_Sousatu_Kingaku = this.Shiharai_Sousatu_Kingaku.Text;
            //支払分入出金額
            this.ParameterDTO.Shiharai_Nyusyu_Kingaku = this.Shiharai_Nyusyu_Kingaku.Text;
            //支払分差引残高
            this.ParameterDTO.Shiharai_Sagaku_Zentaka = this.Shiharai_Sagaku_Zentaka.Text;
            //支払分消費税率
            this.ParameterDTO.Shiharai_Syohizei_Ritu = this.ShiharaiShouhizeiRate;
            //今回金額合計
            this.ParameterDTO.Gokei_Konkai_Kingaku = this.Gokei_Konkai_Kingaku.Text;
            //今回税額合計
            this.ParameterDTO.Gokei_Konkai_Zeigaku = this.Gokei_Konkai_Zeigaku.Text;
            //今回取引合計
            this.ParameterDTO.Gokei_Konkai_Rorihiki = this.Gokei_Konkai_Rorihiki.Text;
            //相殺金額合計
            this.ParameterDTO.Gokei_Sousatu_Kingaku = this.Gokei_Sousatu_Kingaku.Text;
            //入出金額合計
            this.ParameterDTO.Gokei_Nyusyu_Kingaku = this.Gokei_Nyusyu_Kingaku.Text;
            //差引残高合計
            this.ParameterDTO.Gokei_Sagaku_Zentaka = this.Gokei_Sagaku_Zentaka.Text;
            //領収証（取引区分が掛けの時は選択にかかわらず領収書を発行しない）
            if (ConstClass.TORIHIKI_KBN_2 == this.SEIKYU_TORIHIKI_VALUE.Text)
            {
                this.ParameterDTO.Ryousyusyou = ConstClass.RYOSYUSYO_KBN_2;
            }
            else
            {
                this.ParameterDTO.Ryousyusyou = this.RYOSYUSYO_VALUE.Text;
            }
            //敬称1
            this.ParameterDTO.Keisyou_1 = this.Keisyou_1.Text;
            //敬称2
            this.ParameterDTO.Keisyou_2 = this.Keisyou_2.Text;

            if (!this.Tadasi_Kaki.ReadOnly)
            {
                this.ParameterDTO.Tadasi_Kaki = this.Tadasi_Kaki.Text.Trim();
            }

            // 計量票発行区分
            if (string.IsNullOrEmpty(this.ParameterDTO.Keiryou_Prirnt_Kbn_Value))
            {
                this.ParameterDTO.Keiryou_Prirnt_Kbn_Value = this.KEIRYOU_PRIRNT_KBN_VALUE.Text;
            }

            // キャッシャ連動
            this.ParameterDTO.Kyasya = this.KYASYA_VALUE.Text;

            m_Result = DialogResult.OK;

            if (RYOSYUSYO_VALUE.Enabled && RYOSYUSYO_VALUE.Text.Equals(RYOSYUSYO_KBN_1.Value))
            {
                this.Logic.SetStatus();
            }

            //	領収書/仕切書_売上)課税金額
            //	領収書/仕切書_売上)課税消費税
            //	領収書/仕切書_売上)非課税金額
            this.Logic.SetRYOSYUSYO();

            //仕切書_支払)課税金額
            //仕切書_支払)課税消費税
            //仕切書_支払)非課税金額
            this.Logic.SetSHIKIRISHO_SHIHARAI();

            return m_Result;

        }

        internal void SaveOldZei()
        {
            tempSeiZeikeisanKBN = this.SEIKYU_ZEIKEISAN_VALUE.Text;
            tempSeiZeiKBN = this.SEIKYU_ZEI_VALUE.Text;
            tempShiZeikeisanKBN = this.SHIHARAI_ZEIKEISAN_VALUE.Text;
            tempShiZeiKBN = this.SHIHARAI_ZEI_VALUE.Text;
        }
    }
}
