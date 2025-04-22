using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// G127 マニフェスト明細表 条件入力ポップアップ
    /// </summary>
    public partial class JoukenPopupForm : SuperForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private JoukenPopupLogic joukenPopUpLogic;

        /// <summary>
        /// 条件DTO
        /// </summary>
        public ManifestMeisaihyouDto ManifestMeisaihyouDto { get; set; }

        private string haishutsuGyoushaFromBef { get; set; }
        private string haishutsuGyoushaFromAft { get; set; }
        private string haishutsuGyoushaToBef { get; set; }
        private string haishutsuGyoushaToAft { get; set; }
        private string shobunGyoushaFromBef { get; set; }
        private string shobunGyoushaFromAft { get; set; }
        private string shobunGyoushaToBef { get; set; }
        private string shobunGyoushaToAft { get; set; }
        private string lastShobunGyoushaFromBef { get; set; }
        private string lastShobunGyoushaFromAft { get; set; }
        private string lastShobunGyoushaToBef { get; set; }
        private string lastShobunGyoushaToAft { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public JoukenPopupForm()
        {
            InitializeComponent();

            this.joukenPopUpLogic = new JoukenPopupLogic(this);
        }

        /// <summary>
        /// 画面がロードされたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.joukenPopUpLogic.WindowInit()) { return; }
            this.Initialize();
        }

        /// <summary>
        /// キーが押されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.F7:
                    /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　start
                    if (this.joukenPopUpLogic.DateCheck())
                    {
                        break;
                    }
                    else if (this.joukenPopUpLogic.KofuDateCheck())
                    {
                        break;
                    }
                    else if (this.joukenPopUpLogic.UnpanDateCheck())
                    {
                        break;
                    }
                    else if (this.joukenPopUpLogic.ShobunDateCheck())
                    {
                        break;
                    }
                    else if (this.joukenPopUpLogic.LastShobunDateCheck())
                    {
                        break;
                    }
                    /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　end
                    this.Search();
                    break;
                case Keys.F12:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        private void Initialize()
        {
            LogUtility.DebugMethodStart();

            if (null == this.ManifestMeisaihyouDto)
            {
                this.ManifestMeisaihyouDto = new ManifestMeisaihyouDto();
            }

            this.KYOTEN_CD.Text = this.ManifestMeisaihyouDto.KyotenCd.ToString();
            this.KYOTEN_NAME.Text = this.ManifestMeisaihyouDto.Kyoten;
            this.HIDUKE_FROM.Text = this.ManifestMeisaihyouDto.DateFrom;
            this.HIDUKE_TO.Text = this.ManifestMeisaihyouDto.DateTo;
            this.KBN_KAMI_MANI.Checked = this.ManifestMeisaihyouDto.IsKamiMani;
            this.KBN_DEN_MANI.Checked = this.ManifestMeisaihyouDto.IsDenMani;
            this.ICHIJI_NIJI_KBN.Text = this.ManifestMeisaihyouDto.IchijiKbn.ToString();
            this.NIJI_HIMODUKE.Text = this.ManifestMeisaihyouDto.NijiHimozuke.ToString();
            this.KOFU_DATE_FROM.Text = this.ManifestMeisaihyouDto.KofuDateFrom;
            this.KOFU_DATE_TO.Text = this.ManifestMeisaihyouDto.KofuDateTo;
            this.UNPAN_DATE_FROM.Text = this.ManifestMeisaihyouDto.UnpanEndDateFrom;
            this.UNPAN_DATE_TO.Text = this.ManifestMeisaihyouDto.UnpanEndDateTo;
            this.SHOBUN_END_DATE_FROM.Text = this.ManifestMeisaihyouDto.ShobunEndDateFrom;
            this.SHOBUN_END_DATE_TO.Text = this.ManifestMeisaihyouDto.ShobunEndDateTo;
            this.LAST_SHOBUN_END_DATE_FROM.Text = this.ManifestMeisaihyouDto.LastShobunEndDateFrom;
            this.LAST_SHOBUN_END_DATE_TO.Text = this.ManifestMeisaihyouDto.LastShobunEndDateTo;
            this.HAISHUTSU_GYOUSHA_CD_FROM.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoushaCdFrom;
            this.HAISHUTSU_GYOUSHA_NAME_FROM.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoushaFrom;
            this.HAISHUTSU_GYOUSHA_CD_TO.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoushaCdTo;
            this.HAISHUTSU_GYOUSHA_NAME_TO.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoushaTo;
            this.HAISHUTSU_GENBA_CD_FROM.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoujouCdFrom;
            this.HAISHUTSU_GENBA_NAME_FROM.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoujouFrom;
            this.HAISHUTSU_GENBA_CD_TO.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoujouCdTo;
            this.HAISHUTSU_GENBA_NAME_TO.Text = this.ManifestMeisaihyouDto.HaishutsuJigyoujouTo;
            this.UNPAN_GYOUSHA_CD_FROM.Text = this.ManifestMeisaihyouDto.UnpanJutakushaCdFrom;
            this.UNPAN_GYOUSHA_NAME_FROM.Text = this.ManifestMeisaihyouDto.UnpanJutakushaFrom;
            this.UNPAN_GYOUSHA_CD_TO.Text = this.ManifestMeisaihyouDto.UnpanJutakushaCdTo;
            this.UNPAN_GYOUSHA_NAME_TO.Text = this.ManifestMeisaihyouDto.UnpanJutakushaTo;
            this.SHOBUN_GYOUSHA_CD_FROM.Text = this.ManifestMeisaihyouDto.ShobunJigyoushaCdFrom;
            this.SHOBUN_GYOUSHA_NAME_FROM.Text = this.ManifestMeisaihyouDto.ShobunJigyoushaFrom;
            this.SHOBUN_GYOUSHA_CD_TO.Text = this.ManifestMeisaihyouDto.ShobunJigyoushaCdTo;
            this.SHOBUN_GYOUSHA_NAME_TO.Text = this.ManifestMeisaihyouDto.ShobunJigyoushaTo;
            this.SHOBUN_GENBA_CD_FROM.Text = this.ManifestMeisaihyouDto.ShobunJigyoujouCdFrom;
            this.SHOBUN_GENBA_NAME_FROM.Text = this.ManifestMeisaihyouDto.ShobunJigyoujouFrom;
            this.SHOBUN_GENBA_CD_TO.Text = this.ManifestMeisaihyouDto.ShobunJigyoujouCdTo;
            this.SHOBUN_GENBA_NAME_TO.Text = this.ManifestMeisaihyouDto.ShobunJigyoujouTo;
            this.LAST_SHOBUN_GYOUSHA_CD_FROM.Text = this.ManifestMeisaihyouDto.LastShobunJigyoushaCdFrom;
            this.LAST_SHOBUN_GYOUSHA_NAME_FROM.Text = this.ManifestMeisaihyouDto.LastShobunJigyoushaFrom;
            this.LAST_SHOBUN_GYOUSHA_CD_TO.Text = this.ManifestMeisaihyouDto.LastShobunJigyoushaCdTo;
            this.LAST_SHOBUN_GYOUSHA_NAME_TO.Text = this.ManifestMeisaihyouDto.LastShobunJigyoushaTo;
            this.LAST_SHOBUN_GENBA_CD_FROM.Text = this.ManifestMeisaihyouDto.LastShobunJigyoujouCdFrom;
            this.LAST_SHOBUN_GENBA_NAME_FROM.Text = this.ManifestMeisaihyouDto.LastShobunJigyoujouFrom;
            this.LAST_SHOBUN_GENBA_CD_TO.Text = this.ManifestMeisaihyouDto.LastShobunJigyoujouCdTo;
            this.LAST_SHOBUN_GENBA_NAME_TO.Text = this.ManifestMeisaihyouDto.LastShobunJigyoujouTo;
            this.HOUKOKUSHO_CD_FROM.Text = this.ManifestMeisaihyouDto.HoukokushoBunruiCdFrom;
            this.HOUKOKUSHO_NAME_FROM.Text = this.ManifestMeisaihyouDto.HoukokushoBunruiFrom;
            this.HOUKOKUSHO_CD_TO.Text = this.ManifestMeisaihyouDto.HoukokushoBunruiCdTo;
            this.HOUKOKUSHO_NAME_TO.Text = this.ManifestMeisaihyouDto.HoukokushoBunruiTo;
            this.HAIKIBUTSU_CD_FROM.Text = this.ManifestMeisaihyouDto.HaikibutsuMeishouCdFrom;
            this.HAIKIBUTSU_NAME_FROM.Text = this.ManifestMeisaihyouDto.HaikibutsuMeishouFrom;
            this.HAIKIBUTSU_CD_TO.Text = this.ManifestMeisaihyouDto.HaikibutsuMeishouCdTo;
            this.HAIKIBUTSU_NAME_TO.Text = this.ManifestMeisaihyouDto.HaikibutsuMeishouTo;
            this.NISUGATA_CD_FROM.Text = this.ManifestMeisaihyouDto.NisugataCdFrom;
            this.NISUGATA_NAME_FROM.Text = this.ManifestMeisaihyouDto.NisugataFrom;
            this.NISUGATA_CD_TO.Text = this.ManifestMeisaihyouDto.NisugataCdTo;
            this.NISUGATA_NAME_TO.Text = this.ManifestMeisaihyouDto.NisugataTo;
            this.SHOBUN_HOUHOU_CD_FROM.Text = this.ManifestMeisaihyouDto.ShobunHouhouCdFrom;
            this.SHOBUN_HOUHOU_NAME_FROM.Text = this.ManifestMeisaihyouDto.ShobunHouhouFrom;
            this.SHOBUN_HOUHOU_CD_TO.Text = this.ManifestMeisaihyouDto.ShobunHouhouCdTo;
            this.SHOBUN_HOUHOU_NAME_TO.Text = this.ManifestMeisaihyouDto.ShobunHouhouTo;
            this.TORIHIKISAKI_CD_FROM.Text = this.ManifestMeisaihyouDto.TorihikisakiCdFrom;
            this.TORIHIKISAKI_NAME_FROM.Text = this.ManifestMeisaihyouDto.TorihikisakiFrom;
            this.TORIHIKISAKI_CD_TO.Text = this.ManifestMeisaihyouDto.TorihikisakiCdTo;
            this.TORIHIKISAKI_NAME_TO.Text = this.ManifestMeisaihyouDto.TorihikisakiTo;
            this.SORT.Text = this.ManifestMeisaihyouDto.Sort.ToString();
            this.GROUP_DATE.Checked = this.ManifestMeisaihyouDto.IsGroupDate;
            this.GROUP_HST_GYOUSHA.Checked = this.ManifestMeisaihyouDto.IsGroupHaishutsuJigyousha;
            this.GROUP_HST_GENBA.Checked = this.ManifestMeisaihyouDto.IsGroupHaishutsuJigyoujou;
            this.GROUP_UPN_GYOUSHA_1.Checked = this.ManifestMeisaihyouDto.IsGroupUnpanJutakusha1;
            this.GROUP_UPN_GYOUSHA_2.Checked = this.ManifestMeisaihyouDto.IsGroupUnpanJutakusha2;
            this.GROUP_SBN_GENBA.Checked = this.ManifestMeisaihyouDto.IsGroupShobunJigyoujou;
            this.GROUP_LAST_SBN_GENBA.Checked = this.ManifestMeisaihyouDto.IsGroupLastShobunGenba;
            this.GROUP_HOUKOKUSHO.Checked = this.ManifestMeisaihyouDto.IsGroupHoukokushoBunrui;
            this.GROUP_SBN_HOUHOU.Checked = this.ManifestMeisaihyouDto.IsGroupShobunHouhou;

            this.ChangeHaishutsuGenbaState();
            this.ChangeShobunGenbaState();
            this.ChangeLastShobunGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void btn_kensakujikkou_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　start
            if (this.joukenPopUpLogic.DateCheck())
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            else if (this.joukenPopUpLogic.KofuDateCheck())
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            else if (this.joukenPopUpLogic.UnpanDateCheck())
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            else if (this.joukenPopUpLogic.ShobunDateCheck())
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            else if (this.joukenPopUpLogic.LastShobunDateCheck())
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　end

            this.Search();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キャンセルボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void btn_cancel_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.DialogResult = DialogResult.Cancel;
            this.Close();

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 排出事業者CDFromテキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HAISHUTSU_GYOUSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeHaishutsuGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出事業者CDToテキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HAISHUTSU_GYOUSHA_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeHaishutsuGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出事業者CDテキストボックスの入力状態に応じて排出事業場項目の状態を変更します
        /// </summary>
        public void ChangeHaishutsuGenbaState()
        {
            LogUtility.DebugMethodStart();

            var haishutsuGyoushaCdFrom = this.HAISHUTSU_GYOUSHA_CD_FROM.Text;
            var haishutsuGyoushaCdTo = this.HAISHUTSU_GYOUSHA_CD_TO.Text;

            if (!String.IsNullOrEmpty(haishutsuGyoushaCdFrom) && !String.IsNullOrEmpty(haishutsuGyoushaCdTo) && haishutsuGyoushaCdFrom == haishutsuGyoushaCdTo)
            {
                this.HAISHUTSU_GENBA_CD_FROM.Enabled = true;
                this.HAISHUTSU_GENBA_NAME_FROM.Enabled = true;
                this.HAISHUTSU_GENBA_FROM_POPUP.Enabled = true;
                this.HAISHUTSU_GENBA_CD_TO.Enabled = true;
                this.HAISHUTSU_GENBA_NAME_TO.Enabled = true;
                this.HAISHUTSU_GENBA_TO_POPUP.Enabled = true;
            }
            else
            {
                this.HAISHUTSU_GENBA_CD_FROM.Enabled = false;
                this.HAISHUTSU_GENBA_NAME_FROM.Enabled = false;
                this.HAISHUTSU_GENBA_FROM_POPUP.Enabled = false;
                this.HAISHUTSU_GENBA_CD_TO.Enabled = false;
                this.HAISHUTSU_GENBA_NAME_TO.Enabled = false;
                this.HAISHUTSU_GENBA_TO_POPUP.Enabled = false;

                this.HAISHUTSU_GENBA_CD_FROM.Text = String.Empty;
                this.HAISHUTSU_GENBA_NAME_FROM.Text = String.Empty;
                this.HAISHUTSU_GENBA_CD_TO.Text = String.Empty;
                this.HAISHUTSU_GENBA_NAME_TO.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分事業者CDFromテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHOBUN_GYOUSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShobunGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分事業者CDToテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHOBUN_GYOUSHA_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShobunGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分事業者CDテキストボックスの入力状態に応じて処分事業場項目の状態を変更します
        /// </summary>
        public void ChangeShobunGenbaState()
        {
            LogUtility.DebugMethodStart();

            var shobunGyoushaCdFrom = this.SHOBUN_GYOUSHA_CD_FROM.Text;
            var shobunGyoushaCdTo = this.SHOBUN_GYOUSHA_CD_TO.Text;

            if (!String.IsNullOrEmpty(shobunGyoushaCdFrom) && !String.IsNullOrEmpty(shobunGyoushaCdTo) && shobunGyoushaCdFrom == shobunGyoushaCdTo)
            {
                this.SHOBUN_GENBA_CD_FROM.Enabled = true;
                this.SHOBUN_GENBA_NAME_FROM.Enabled = true;
                this.SHOBUN_GENBA_FROM_POPUP.Enabled = true;
                this.SHOBUN_GENBA_CD_TO.Enabled = true;
                this.SHOBUN_GENBA_NAME_TO.Enabled = true;
                this.SHOBUN_GENBA_TO_POPUP.Enabled = true;
            }
            else
            {
                this.SHOBUN_GENBA_CD_FROM.Enabled = false;
                this.SHOBUN_GENBA_NAME_FROM.Enabled = false;
                this.SHOBUN_GENBA_FROM_POPUP.Enabled = false;
                this.SHOBUN_GENBA_CD_TO.Enabled = false;
                this.SHOBUN_GENBA_NAME_TO.Enabled = false;
                this.SHOBUN_GENBA_TO_POPUP.Enabled = false;

                this.SHOBUN_GENBA_CD_FROM.Text = String.Empty;
                this.SHOBUN_GENBA_NAME_FROM.Text = String.Empty;
                this.SHOBUN_GENBA_CD_TO.Text = String.Empty;
                this.SHOBUN_GENBA_NAME_TO.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終処分事業者CDFromテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void LAST_SHOBUN_GYOUSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeLastShobunGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終処分事業者CDToテキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void LAST_SHOBUN_GYOUSHA_CD_TO_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeLastShobunGenbaState();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終処分事業者テキストボックスの入力状態に応じて最終処分事業場項目の状態を変更します
        /// </summary>
        public void ChangeLastShobunGenbaState()
        {
            LogUtility.DebugMethodStart();

            var lastShobunGyoushaCdFrom = this.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
            var lastShobunGyoushaCdTo = this.LAST_SHOBUN_GYOUSHA_CD_TO.Text;

            if (!String.IsNullOrEmpty(lastShobunGyoushaCdFrom) && !String.IsNullOrEmpty(lastShobunGyoushaCdTo) && lastShobunGyoushaCdFrom == lastShobunGyoushaCdTo)
            {
                this.LAST_SHOBUN_GENBA_CD_FROM.Enabled = true;
                this.LAST_SHOBUN_GENBA_NAME_FROM.Enabled = true;
                this.LAST_SHOBUN_GENBA_FROM_POPUP.Enabled = true;
                this.LAST_SHOBUN_GENBA_CD_TO.Enabled = true;
                this.LAST_SHOBUN_GENBA_NAME_TO.Enabled = true;
                this.LAST_SHOBUN_GENBA_TO_POPUP.Enabled = true;
            }
            else
            {
                this.LAST_SHOBUN_GENBA_CD_FROM.Enabled = false;
                this.LAST_SHOBUN_GENBA_NAME_FROM.Enabled = false;
                this.LAST_SHOBUN_GENBA_FROM_POPUP.Enabled = false;
                this.LAST_SHOBUN_GENBA_CD_TO.Enabled = false;
                this.LAST_SHOBUN_GENBA_NAME_TO.Enabled = false;
                this.LAST_SHOBUN_GENBA_TO_POPUP.Enabled = false;

                this.LAST_SHOBUN_GENBA_CD_FROM.Text = String.Empty;
                this.LAST_SHOBUN_GENBA_NAME_FROM.Text = String.Empty;
                this.LAST_SHOBUN_GENBA_CD_TO.Text = String.Empty;
                this.LAST_SHOBUN_GENBA_NAME_TO.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニフェスト明細表データを抽出します
        /// </summary>
        private void Search()
        {
            LogUtility.DebugMethodStart();

            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            if (false == this.RegistErrorFlag)
            {
                this.SetManifestMeisaihyouDto();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // FromToの整合性チェックで、CausesValidation=falseのままになってしまうのでtrueに戻す
                this.HAISHUTSU_GYOUSHA_CD_FROM.CausesValidation = true;
                this.HAISHUTSU_GYOUSHA_CD_TO.CausesValidation = true;
                this.HAISHUTSU_GENBA_CD_TO.CausesValidation = true;
                this.HAISHUTSU_GENBA_CD_TO.CausesValidation = true;
                this.UNPAN_GYOUSHA_CD_FROM.CausesValidation = true;
                this.UNPAN_GYOUSHA_CD_TO.CausesValidation = true;
                this.SHOBUN_GYOUSHA_CD_FROM.CausesValidation = true;
                this.SHOBUN_GYOUSHA_CD_TO.CausesValidation = true;
                this.SHOBUN_GENBA_CD_FROM.CausesValidation = true;
                this.SHOBUN_GENBA_CD_TO.CausesValidation = true;
                this.LAST_SHOBUN_GYOUSHA_CD_FROM.CausesValidation = true;
                this.LAST_SHOBUN_GYOUSHA_CD_TO.CausesValidation = true;
                this.LAST_SHOBUN_GENBA_CD_FROM.CausesValidation = true;
                this.LAST_SHOBUN_GENBA_CD_TO.CausesValidation = true;
                this.HOUKOKUSHO_CD_FROM.CausesValidation = true;
                this.HOUKOKUSHO_CD_TO.CausesValidation = true;
                this.HAIKIBUTSU_CD_FROM.CausesValidation = true;
                this.HAIKIBUTSU_CD_TO.CausesValidation = true;
                this.NISUGATA_CD_FROM.CausesValidation = true;
                this.NISUGATA_CD_TO.CausesValidation = true;
                this.SHOBUN_HOUHOU_CD_FROM.CausesValidation = true;
                this.SHOBUN_HOUHOU_CD_TO.CausesValidation = true;
                this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
                this.TORIHIKISAKI_CD_TO.CausesValidation = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件DTOに画面から値をセットします
        /// </summary>
        private void SetManifestMeisaihyouDto()
        {
            LogUtility.DebugMethodStart();

            this.ManifestMeisaihyouDto.KyotenCd = Int32.Parse(this.KYOTEN_CD.Text);
            this.ManifestMeisaihyouDto.Kyoten = this.KYOTEN_NAME.Text;
            if (null != this.HIDUKE_FROM.Value)
            {
                this.ManifestMeisaihyouDto.DateFrom = ((DateTime)this.HIDUKE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.DateFrom = null;
            }
            if (null != this.HIDUKE_TO.Value)
            {
                this.ManifestMeisaihyouDto.DateTo = ((DateTime)this.HIDUKE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.DateTo = null;
            }
            this.ManifestMeisaihyouDto.IsKamiMani = this.KBN_KAMI_MANI.Checked;
            this.ManifestMeisaihyouDto.IsDenMani = this.KBN_DEN_MANI.Checked;
            this.ManifestMeisaihyouDto.IchijiKbn = Int32.Parse(this.ICHIJI_NIJI_KBN.Text);
            this.ManifestMeisaihyouDto.NijiHimozuke = Int32.Parse(this.NIJI_HIMODUKE.Text);
            if (null != this.KOFU_DATE_FROM.Value)
            {
                this.ManifestMeisaihyouDto.KofuDateFrom = ((DateTime)this.KOFU_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.KofuDateFrom = null;
            }
            if (null != this.KOFU_DATE_TO.Value)
            {
                this.ManifestMeisaihyouDto.KofuDateTo = ((DateTime)this.KOFU_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.KofuDateTo = null;
            }
            if (null != this.UNPAN_DATE_FROM.Value)
            {
                this.ManifestMeisaihyouDto.UnpanEndDateFrom = ((DateTime)this.UNPAN_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.UnpanEndDateFrom = null;
            }
            if (null != this.UNPAN_DATE_TO.Value)
            {
                this.ManifestMeisaihyouDto.UnpanEndDateTo = ((DateTime)this.UNPAN_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.UnpanEndDateTo = null;
            }
            if (null != this.SHOBUN_END_DATE_FROM.Value)
            {
                this.ManifestMeisaihyouDto.ShobunEndDateFrom = ((DateTime)this.SHOBUN_END_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.ShobunEndDateFrom = null;
            }
            if (null != this.SHOBUN_END_DATE_TO.Value)
            {
                this.ManifestMeisaihyouDto.ShobunEndDateTo = ((DateTime)this.SHOBUN_END_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.ShobunEndDateTo = null;
            }
            if (null != this.LAST_SHOBUN_END_DATE_FROM.Value)
            {
                this.ManifestMeisaihyouDto.LastShobunEndDateFrom = ((DateTime)this.LAST_SHOBUN_END_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.LastShobunEndDateFrom = null;
            }
            if (null != this.LAST_SHOBUN_END_DATE_TO.Value)
            {
                this.ManifestMeisaihyouDto.LastShobunEndDateTo = ((DateTime)this.LAST_SHOBUN_END_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestMeisaihyouDto.LastShobunEndDateTo = null;
            }
            this.ManifestMeisaihyouDto.HaishutsuJigyoushaCdFrom = this.HAISHUTSU_GYOUSHA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoushaFrom = this.HAISHUTSU_GYOUSHA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoushaCdTo = this.HAISHUTSU_GYOUSHA_CD_TO.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoushaTo = this.HAISHUTSU_GYOUSHA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoujouCdFrom = this.HAISHUTSU_GENBA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoujouFrom = this.HAISHUTSU_GENBA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoujouCdTo = this.HAISHUTSU_GENBA_CD_TO.Text;
            this.ManifestMeisaihyouDto.HaishutsuJigyoujouTo = this.HAISHUTSU_GENBA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.UnpanJutakushaCdFrom = this.UNPAN_GYOUSHA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.UnpanJutakushaFrom = this.UNPAN_GYOUSHA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.UnpanJutakushaCdTo = this.UNPAN_GYOUSHA_CD_TO.Text;
            this.ManifestMeisaihyouDto.UnpanJutakushaTo = this.UNPAN_GYOUSHA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoushaCdFrom = this.SHOBUN_GYOUSHA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoushaFrom = this.SHOBUN_GYOUSHA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoushaCdTo = this.SHOBUN_GYOUSHA_CD_TO.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoushaTo = this.SHOBUN_GYOUSHA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoujouCdFrom = this.SHOBUN_GENBA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoujouFrom = this.SHOBUN_GENBA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoujouCdTo = this.SHOBUN_GENBA_CD_TO.Text;
            this.ManifestMeisaihyouDto.ShobunJigyoujouTo = this.SHOBUN_GENBA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoushaCdFrom = this.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoushaFrom = this.LAST_SHOBUN_GYOUSHA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoushaCdTo = this.LAST_SHOBUN_GYOUSHA_CD_TO.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoushaTo = this.LAST_SHOBUN_GYOUSHA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoujouCdFrom = this.LAST_SHOBUN_GENBA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoujouFrom = this.LAST_SHOBUN_GENBA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoujouCdTo = this.LAST_SHOBUN_GENBA_CD_TO.Text;
            this.ManifestMeisaihyouDto.LastShobunJigyoujouTo = this.LAST_SHOBUN_GENBA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.HoukokushoBunruiCdFrom = this.HOUKOKUSHO_CD_FROM.Text;
            this.ManifestMeisaihyouDto.HoukokushoBunruiFrom = this.HOUKOKUSHO_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.HoukokushoBunruiCdTo = this.HOUKOKUSHO_CD_TO.Text;
            this.ManifestMeisaihyouDto.HoukokushoBunruiTo = this.HOUKOKUSHO_NAME_TO.Text;
            this.ManifestMeisaihyouDto.HaikibutsuMeishouCdFrom = this.HAIKIBUTSU_CD_FROM.Text;
            this.ManifestMeisaihyouDto.HaikibutsuMeishouFrom = this.HAIKIBUTSU_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.HaikibutsuMeishouCdTo = this.HAIKIBUTSU_CD_TO.Text;
            this.ManifestMeisaihyouDto.HaikibutsuMeishouTo = this.HAIKIBUTSU_NAME_TO.Text;
            this.ManifestMeisaihyouDto.NisugataCdFrom = this.NISUGATA_CD_FROM.Text;
            this.ManifestMeisaihyouDto.NisugataFrom = this.NISUGATA_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.NisugataCdTo = this.NISUGATA_CD_TO.Text;
            this.ManifestMeisaihyouDto.NisugataTo = this.NISUGATA_NAME_TO.Text;
            this.ManifestMeisaihyouDto.ShobunHouhouCdFrom = this.SHOBUN_HOUHOU_CD_FROM.Text;
            this.ManifestMeisaihyouDto.ShobunHouhouFrom = this.SHOBUN_HOUHOU_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.ShobunHouhouCdTo = this.SHOBUN_HOUHOU_CD_TO.Text;
            this.ManifestMeisaihyouDto.ShobunHouhouTo = this.SHOBUN_HOUHOU_NAME_TO.Text;
            this.ManifestMeisaihyouDto.TorihikisakiCdFrom = this.TORIHIKISAKI_CD_FROM.Text;
            this.ManifestMeisaihyouDto.TorihikisakiFrom = this.TORIHIKISAKI_NAME_FROM.Text;
            this.ManifestMeisaihyouDto.TorihikisakiCdTo = this.TORIHIKISAKI_CD_TO.Text;
            this.ManifestMeisaihyouDto.TorihikisakiTo = this.TORIHIKISAKI_NAME_TO.Text;
            this.ManifestMeisaihyouDto.Sort = Int32.Parse(this.SORT.Text);
            this.ManifestMeisaihyouDto.IsGroupDate = this.GROUP_DATE.Checked;
            this.ManifestMeisaihyouDto.IsGroupHaishutsuJigyousha = this.GROUP_HST_GYOUSHA.Checked;
            this.ManifestMeisaihyouDto.IsGroupHaishutsuJigyoujou = this.GROUP_HST_GENBA.Checked;
            this.ManifestMeisaihyouDto.IsGroupUnpanJutakusha1 = this.GROUP_UPN_GYOUSHA_1.Checked;
            this.ManifestMeisaihyouDto.IsGroupUnpanJutakusha2 = this.GROUP_UPN_GYOUSHA_2.Checked;
            this.ManifestMeisaihyouDto.IsGroupShobunJigyoujou = this.GROUP_SBN_GENBA.Checked;
            this.ManifestMeisaihyouDto.IsGroupLastShobunGenba = this.GROUP_LAST_SBN_GENBA.Checked;
            this.ManifestMeisaihyouDto.IsGroupHoukokushoBunrui = this.GROUP_HOUKOKUSHO.Checked;
            this.ManifestMeisaihyouDto.IsGroupShobunHouhou = this.GROUP_SBN_HOUHOU.Checked;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 並び順テキストボックスのテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SORT_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.GROUP_DATE.Enabled = false;
            this.GROUP_HST_GYOUSHA.Enabled = false;
            this.GROUP_HST_GENBA.Enabled = false;
            this.GROUP_UPN_GYOUSHA_1.Enabled = false;
            this.GROUP_UPN_GYOUSHA_2.Enabled = false;
            this.GROUP_SBN_GENBA.Enabled = false;
            this.GROUP_LAST_SBN_GENBA.Enabled = false;
            this.GROUP_HOUKOKUSHO.Enabled = false;
            this.GROUP_SBN_HOUHOU.Enabled = false;

            this.GROUP_DATE.Checked = false;
            this.GROUP_HST_GYOUSHA.Checked = false;
            this.GROUP_HST_GENBA.Checked = false;
            this.GROUP_UPN_GYOUSHA_1.Checked = false;
            this.GROUP_UPN_GYOUSHA_2.Checked = false;
            this.GROUP_SBN_GENBA.Checked = false;
            this.GROUP_LAST_SBN_GENBA.Checked = false;
            this.GROUP_HOUKOKUSHO.Checked = false;
            this.GROUP_SBN_HOUHOU.Checked = false;

            if (!String.IsNullOrEmpty(this.SORT.Text))
            {
                var sort = Int32.Parse(this.SORT.Text);
                if (ConstClass.SORT_KOFU_DATE == sort)
                {
                    // 日付
                    this.GROUP_DATE.Enabled = true;
                    this.GROUP_DATE.Checked = true;
                    this.GROUP_DATE.Text = ConstClass.SORT_KOFU_DATE_SUB_TITLE;
                }
                else if (ConstClass.SORT_UPN_END_DATE == sort)
                {
                    // 日付
                    this.GROUP_DATE.Enabled = true;
                    this.GROUP_DATE.Checked = true;
                    this.GROUP_DATE.Text = ConstClass.SORT_UPN_END_DATE_SUB_TITLE;
                }
                else if (ConstClass.SORT_SBN_END_DATE == sort)
                {
                    // 日付
                    this.GROUP_DATE.Enabled = true;
                    this.GROUP_DATE.Checked = true;
                    this.GROUP_DATE.Text = ConstClass.SORT_SBN_END_DATE_SUB_TITLE;
                }
                else if (ConstClass.SORT_LAST_SBN_END_DATE == sort)
                {
                    // 日付
                    this.GROUP_DATE.Enabled = true;
                    this.GROUP_DATE.Checked = true;
                    this.GROUP_DATE.Text = ConstClass.SORT_LAST_SBN_END_DATE_SUB_TITLE;
                }
                else if (ConstClass.SORT_HST_GYOUSHA == sort)
                {
                    // 排出事業者・排出事業場
                    this.GROUP_HST_GYOUSHA.Enabled = true;
                    this.GROUP_HST_GYOUSHA.Checked = true;
                    this.GROUP_HST_GENBA.Enabled = true;
                    this.GROUP_HST_GENBA.Checked = true;
                }
                else if (ConstClass.SORT_UPN_GYOUSHA_1 == sort)
                {
                    // 運搬受託者１
                    this.GROUP_UPN_GYOUSHA_1.Enabled = true;
                    this.GROUP_UPN_GYOUSHA_1.Checked = true;
                }
                else if (ConstClass.SORT_UPN_GYOUSHA_2 == sort)
                {
                    // 運搬受託者２
                    this.GROUP_UPN_GYOUSHA_2.Enabled = true;
                    this.GROUP_UPN_GYOUSHA_2.Checked = true;
                }
                else if (ConstClass.SORT_SBN_GYOUSHA == sort)
                {
                    // 処分事業場
                    this.GROUP_SBN_GENBA.Enabled = true;
                    this.GROUP_SBN_GENBA.Checked = true;
                }
                else if (ConstClass.SORT_LAST_SBN_GYOUSHA == sort)
                {
                    // 最終処分場
                    this.GROUP_LAST_SBN_GENBA.Enabled = true;
                    this.GROUP_LAST_SBN_GENBA.Checked = true;
                }
                else if (ConstClass.SORT_HOUKOKUSHO_BUNRUI == sort)
                {
                    // 廃棄物種類
                    this.GROUP_HOUKOKUSHO.Enabled = true;
                    this.GROUP_HOUKOKUSHO.Checked = true;
                }
                else if (ConstClass.SORT_SBN_HOUHOU == sort)
                {
                    // 処分方法
                    this.GROUP_SBN_HOUHOU.Enabled = true;
                    this.GROUP_SBN_HOUHOU.Checked = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDUKE_TO.Text))
            {
                this.HIDUKE_TO.IsInputErrorOccured = false;
                this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDUKE_FROM.Text))
            {
                this.HIDUKE_FROM.IsInputErrorOccured = false;
                this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void KOFU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.KOFU_DATE_TO.Text))
            {
                this.KOFU_DATE_TO.IsInputErrorOccured = false;
                this.KOFU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void KOFU_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.KOFU_DATE_FROM.Text))
            {
                this.KOFU_DATE_FROM.IsInputErrorOccured = false;
                this.KOFU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void UNPAN_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UNPAN_DATE_TO.Text))
            {
                this.UNPAN_DATE_TO.IsInputErrorOccured = false;
                this.UNPAN_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void UNPAN_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UNPAN_DATE_FROM.Text))
            {
                this.UNPAN_DATE_FROM.IsInputErrorOccured = false;
                this.UNPAN_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void SHOBUN_END_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SHOBUN_END_DATE_TO.Text))
            {
                this.SHOBUN_END_DATE_TO.IsInputErrorOccured = false;
                this.SHOBUN_END_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void SHOBUN_END_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SHOBUN_END_DATE_FROM.Text))
            {
                this.SHOBUN_END_DATE_FROM.IsInputErrorOccured = false;
                this.SHOBUN_END_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void LAST_SHOBUN_END_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LAST_SHOBUN_END_DATE_TO.Text))
            {
                this.LAST_SHOBUN_END_DATE_TO.IsInputErrorOccured = false;
                this.LAST_SHOBUN_END_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void LAST_SHOBUN_END_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LAST_SHOBUN_END_DATE_FROM.Text))
            {
                this.LAST_SHOBUN_END_DATE_FROM.IsInputErrorOccured = false;
                this.LAST_SHOBUN_END_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141022 Houkakou 「マニ明細表」の日付チェックを追加する　end
         
        /// <summary>
        /// 排出事業者FROM_POPUP_BEFイベント
        /// </summary>
        public void HAISHUTSU_GYOUSHA_FROM_POPUP_BEF()
        {
            haishutsuGyoushaFromBef = this.HAISHUTSU_GYOUSHA_CD_FROM.Text;
        }

        /// <summary>
        /// 排出事業者FROM_POPUP_AFTイベント
        /// </summary>
        public void HAISHUTSU_GYOUSHA_FROM_POPUP_AFT()
        {
            haishutsuGyoushaFromAft = this.HAISHUTSU_GYOUSHA_CD_FROM.Text;
            if (haishutsuGyoushaFromBef != haishutsuGyoushaFromAft)
            {
                this.HAISHUTSU_GENBA_CD_FROM.Text = string.Empty;
                this.HAISHUTSU_GENBA_NAME_FROM.Text = string.Empty;
                this.HAISHUTSU_GENBA_CD_TO.Text = string.Empty;
                this.HAISHUTSU_GENBA_NAME_TO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 排出事業者TO_POPUP_BEFイベント
        /// </summary>
        public void HAISHUTSU_GYOUSHA_TO_POPUP_BEF()
        {
            haishutsuGyoushaToBef = this.HAISHUTSU_GYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// 排出事業者TO_POPUP_AFTイベント
        /// </summary>
        public void HAISHUTSU_GYOUSHA_TO_POPUP_AFT()
        {
            haishutsuGyoushaToAft = this.HAISHUTSU_GYOUSHA_CD_TO.Text;
            if (haishutsuGyoushaToBef != haishutsuGyoushaToAft)
            {
                this.HAISHUTSU_GENBA_CD_FROM.Text = string.Empty;
                this.HAISHUTSU_GENBA_NAME_FROM.Text = string.Empty;
                this.HAISHUTSU_GENBA_CD_TO.Text = string.Empty;
                this.HAISHUTSU_GENBA_NAME_TO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者FROM_POPUP_BEFイベント
        /// </summary>
        public void SHOBUN_GYOUSHA_FROM_POPUP_BEF()
        {
            shobunGyoushaFromBef = this.SHOBUN_GYOUSHA_CD_FROM.Text;
        }

        /// <summary>
        /// 処分受託者FROM_POPUP_AFTイベント
        /// </summary>
        public void SHOBUN_GYOUSHA_FROM_POPUP_AFT()
        {
            shobunGyoushaFromAft = this.SHOBUN_GYOUSHA_CD_FROM.Text;
            if (shobunGyoushaFromBef != shobunGyoushaFromAft)
            {
                this.SHOBUN_GENBA_CD_FROM.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_FROM.Text = string.Empty;
                this.SHOBUN_GENBA_CD_TO.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_TO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者TO_POPUP_BEFイベント
        /// </summary>
        public void SHOBUN_GYOUSHA_TO_POPUP_BEF()
        {
            shobunGyoushaToBef = this.SHOBUN_GYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// 処分受託者TO_POPUP_AFTイベント
        /// </summary>
        public void SHOBUN_GYOUSHA_TO_POPUP_AFT()
        {
            shobunGyoushaToAft = this.SHOBUN_GYOUSHA_CD_TO.Text;
            if (shobunGyoushaToBef != shobunGyoushaToAft)
            {
                this.SHOBUN_GENBA_CD_FROM.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_FROM.Text = string.Empty;
                this.SHOBUN_GENBA_CD_TO.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_TO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 最終処分業者FROM_POPUP_BEFイベント
        /// </summary>
        public void LAST_SHOBUN_GYOUSHA_FROM_POPUP_BEF()
        {
            lastShobunGyoushaFromBef = this.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
        }

        /// <summary>
        /// 最終処分業者FROM_POPUP_AFTイベント
        /// </summary>
        public void LAST_SHOBUN_GYOUSHA_FROM_POPUP_AFT()
        {
            lastShobunGyoushaFromAft = this.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
            if (lastShobunGyoushaFromBef != lastShobunGyoushaFromAft)
            {
                this.LAST_SHOBUN_GENBA_CD_FROM.Text = string.Empty;
                this.LAST_SHOBUN_GENBA_NAME_FROM.Text = string.Empty;
                this.LAST_SHOBUN_GENBA_CD_TO.Text = string.Empty;
                this.LAST_SHOBUN_GENBA_NAME_TO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 最終処分業者TO_POPUP_BEFイベント
        /// </summary>
        public void LAST_SHOBUN_GYOUSHA_TO_POPUP_BEF()
        {
            lastShobunGyoushaToBef = this.LAST_SHOBUN_GYOUSHA_CD_TO.Text;
        }

        /// <summary>
        /// 最終処分業者TO_POPUP_AFTイベント
        /// </summary>
        public void LAST_SHOBUN_GYOUSHA_TO_POPUP_AFT()
        {
            lastShobunGyoushaToAft = this.LAST_SHOBUN_GYOUSHA_CD_TO.Text;
            if (lastShobunGyoushaToBef != lastShobunGyoushaToAft)
            {
                this.LAST_SHOBUN_GENBA_CD_FROM.Text = string.Empty;
                this.LAST_SHOBUN_GENBA_NAME_FROM.Text = string.Empty;
                this.LAST_SHOBUN_GENBA_CD_TO.Text = string.Empty;
                this.LAST_SHOBUN_GENBA_NAME_TO.Text = string.Empty;
            }
        }
    }
}
