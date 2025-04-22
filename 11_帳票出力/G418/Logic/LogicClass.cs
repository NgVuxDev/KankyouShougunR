using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ReportOutput.CommonChouhyouViewer;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Enum -

    /// <summary>期間指定に関する列挙型</summary>
    public enum DATE_TIME_TYPE
    {
        /// <summary>当日</summary>
        Tojitsu = 1,

        /// <summary>当月</summary>
        Togetsu,

        /// <summary>期間指定</summary>
        Kikanshitei,
    }

    /// <summary>集計項目に関する列挙型</summary>
    public enum SYUKEUKOMOKU_TYPE
    {
        /// <summary>0：なし</summary>
        None,

        /// <summary>1：取引先別</summary>
        TorihikisakiBetsu,

        /// <summary>2：業者別</summary>
        GyoshaBetsu,

        /// <summary>3：現場別</summary>
        GenbaBetsu,

        /// <summary>4：運搬業者別</summary>
        UnpanGyoshaBetsu,

        /// <summary>5：荷降業者別</summary>
        NioroshiGyoshaBetsu,

        /// <summary>6：荷降現場別</summary>
        NioroshiGenbaBetsu,

        /// <summary>7：荷積業者別</summary>
        NizumiGyoshaBetsu,

        /// <summary>8：荷積現場別</summary>
        NizumiGenbaBetsu,

        /// <summary>9：営業担当者別</summary>
        EigyoTantoshaBetsu,

        /// <summary>10：拠点別</summary>
        KyotenBetsu,

        /// <summary>12：種類別</summary>
        SyuruiBetsu,

        /// <summary>13：分類別</summary>
        BunruiBetsu,

        /// <summary>14：品名別</summary>
        HinmeiBetsu,

        /// <summary>15：車種別</summary>
        ShasyuBetsu,

        /// <summary>16：車輌別</summary>
        SharyoBetsu,

        /// <summary>17：運転者別</summary>
        UntenshaBetsu,

        /// <summary>18：伝票区分別</summary>
        DenpyoKubunBetsu,

        /// <summary>19：伝種区分別</summary>
        DensyuKubunBetsu,

        /// <summary>20：入金先別</summary>
        NyukinsakiBetsu,

        /// <summary>21：銀行別</summary>
        GinkoBetsu,

        /// <summary>22：銀行支店別</summary>
        GinkoShitenBetsu,

        /// <summary>23：日付別</summary>
        HidukeBetsu,
    }

    #endregion - Enum -

    #region - Class -

    #region - LogicClass -

    /// <summary>ビジネスロジック</summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド</summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Setting.ButtonSetting.xml";

        /// <summary>マスターリストパターンDaoを保持するフィールド</summary>
        private IMLPDao mlpDao;

        /// <summary>マスターリストパターンフィルコンドDaoを保持するフィールド</summary>
        private IMLPFCDao mlpfcDao;

        /// <summary>マスターリストパターンカラムDaoを保持するフィールド</summary>
        private IMLPCDao mlpcDao;

        /// <summary>マスターリストパターンカラムロジック</summary>
        private IBuisinessLogic mlistPatternColumnLogic;

        /// <summary>マスターリストパターンフィルコンドロジック</summary>
        private IBuisinessLogic mlistPatternFillCondLogic;

        /// <summary>一覧表項目選択用ビジネスロジック</summary>
        private IBuisinessLogic mlistColumnSelectLogic;

        /// <summary>フォーム</summary>
        private G418_MeisaihyoSyukeihyoJokenShiteiPopupForm form;

        /// <summary>システムＩＤを保持するフィールド</summary>
        private long systemID = 0;

        /// <summary>最大シーケンス番号を保持するフィールド</summary>
        private int maxSeqNo = 0;

        /// <summary>メッセージロジックオブジェクトを保持するフィールド</summary>
        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        private M_LIST_PATTERN mlistPattern = new M_LIST_PATTERN();
        private M_LIST_PATTERN_FILL_COND mlistPatternFillCond = new M_LIST_PATTERN_FILL_COND();
        private M_LIST_PATTERN_COLUMN mlistPatternColumn = new M_LIST_PATTERN_COLUMN();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="LogicClass"/> class.</summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(G418_MeisaihyoSyukeihyoJokenShiteiPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // マスターリストパターンDao
            this.mlpDao = DaoInitUtility.GetComponent<IMLPDao>();

            // マスターリストパターンフィルコンドDao
            this.mlpfcDao = DaoInitUtility.GetComponent<IMLPFCDao>();

            // マスターリストパターンカラムDao
            this.mlpcDao = DaoInitUtility.GetComponent<IMLPCDao>();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MasterListPatternLogic = new MListPatternLogic(targetForm);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.mlistPatternColumnLogic = new MListPatternColumnLogic(targetForm);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.mlistPatternFillCondLogic = new MListPatternFillCondLogic(targetForm);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.mlistColumnSelectLogic = new SListColumnSelectLogic(targetForm);

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>マスターリストパターンロジックを保持するプロパティ</summary>
        internal IBuisinessLogic MasterListPatternLogic { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>画面初期化処理</summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region - Function Key Proc -

        /// <summary>出力伝票項目追加(F1)処理</summary>
        public bool Func1()
        {
            LogUtility.DebugMethodStart();

            try
            {
                int index;

                ListBox.SelectedIndexCollection selectedIndexCollection = this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndices;
                int count = selectedIndexCollection.Count;

                if (count <= 0)
                {
                    return false;
                }

                if (this.form.WindowId == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU || this.form.WindowId == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU || this.form.WindowId == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
                {   // 入金集計表・出金集計表・計量集計表
                    if ((this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count + count) + this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count > this.form.MaxOutputCount)
                    {   // 伝票＋明細の合計数が最大値を超えた

                        // {0}に追加できる最大項目数は{0}件です。
                        this.msgLogic.MessageBoxShow("E053", new string[] { "出力項目（伝票／明細）", "合計で8" });

                        return false;
                    }
                }
                else
                {
                    if ((this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count + count) > this.form.MaxOutputCount)
                    {   // 出力可能最大数を超えた

                        // {0}に追加できる最大項目数は{0}件です。
                        this.msgLogic.MessageBoxShow("E053", new string[] { "出力項目（伝票）", "8" });

                        return false;
                    }
                }

                int indexLast = selectedIndexCollection[count - 1];

                for (int i = 0; i < count; i++)
                {
                    index = selectedIndexCollection[i];

                    // 選択されている出力可能項目（伝票）文字列取得
                    string tmp = (string)this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items[index];
                    ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList[index];

                    // 出力項目（伝票）へ追加
                    this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Add(tmp);
                    this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.Add(chouhyouOutKoumokuGroup);
                    this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex = this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count - 1;
                }

                for (int i = 0; i < count; i++)
                {
                    index = selectedIndexCollection[count - (i + 1)];

                    // 選択されている出力可能項目（伝票）削除
                    this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.RemoveAt(index);
                    this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.RemoveAt(index);
                }

                if (this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Count > 0)
                {
                    index = indexLast;

                    if ((index + 1) > this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Count)
                    {
                        this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndex = this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Count - 1;
                    }
                    else
                    {
                        this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndex = 0;
                    }
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func1", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func1", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>出力伝票項目削除(F2)処理</summary>
        public bool Func2()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 選択されている出力項目（伝票）のインデックス取得
                int index = this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex;
                if (index < 0)
                {
                    return false;
                }

                // 選択されている出力項目（伝票）文字列取得
                string tmp = (string)this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index];
                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index];

                // 出力可能項目（伝票）へ追加
                this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Add(tmp);
                this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.Add(chouhyouOutKoumokuGroup);

                this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndices.Clear();

                this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndex = this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Count - 1;

                // 選択されている出力項目（伝票）削除
                this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.RemoveAt(index);
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.RemoveAt(index);
                if (this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count > 0)
                {
                    if ((index + 1) > this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count)
                    {
                        this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex = index - 1;
                    }
                    else
                    {
                        this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex = index;
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func2", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func2", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>出力伝票項目全削除(F3)処理</summary>
        public bool Func3()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count <= 0)
                {
                    return false;
                }

                // {0}から全項目削除します。よろしいですか？
                if (this.msgLogic.MessageBoxShow("C037", new string[] { "出力項目（伝票）" }) != DialogResult.Yes)
                {
                    return false;
                }

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup;
                for (int i = 0; i < this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count; i++)
                {
                    this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Add(this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[i]);
                    chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[i];
                    this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.Add(chouhyouOutKoumokuGroup);
                }
                this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndices.Clear();

                this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndex = this.form.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Count - 1;

                this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Clear();
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.Clear();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func3", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func3", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>出力明細項目追加(F4)処理</summary>
        public bool Func4()
        {
            LogUtility.DebugMethodStart();

            try
            {
                int index;

                ListBox.SelectedIndexCollection selectedIndexCollection = this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndices;
                int count = selectedIndexCollection.Count;
                if (count <= 0)
                {
                    return false;
                }

                if (this.form.WindowId == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU || this.form.WindowId == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU || this.form.WindowId == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
                {   // 入金集計表・出金集計表・計量集計表
                    if (this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count + (this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count + count) > this.form.MaxOutputCount)
                    {   // 伝票＋明細の合計数が最大値を超えた

                        // {0}に追加できる最大項目数は{0}件です。
                        this.msgLogic.MessageBoxShow("E053", new string[] { "出力項目（伝票／明細）", "合計で8" });

                        return false;
                    }
                }
                else
                {
                    if ((this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count + count) > this.form.MaxOutputCount)
                    {   // 出力可能最大数を超えた

                        // {0}に追加できる最大項目数は{0}件です。
                        this.msgLogic.MessageBoxShow("E053", new string[] { "出力項目（明細）", "8" });

                        return false;
                    }
                }

                int indexLast = selectedIndexCollection[count - 1];

                for (int i = 0; i < count; i++)
                {
                    index = selectedIndexCollection[i];

                    // 選択されている出力可能項目（明細）文字列取得
                    string tmp = (string)this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items[index];
                    ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList[index];

                    // 出力項目（明細）へ追加
                    this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Add(tmp);
                    this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.Add(chouhyouOutKoumokuGroup);
                    this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex = this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count - 1;
                }

                for (int i = 0; i < count; i++)
                {
                    index = selectedIndexCollection[count - (i + 1)];

                    // 選択されている出力可能項目（明細）削除
                    this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.RemoveAt(index);
                    this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.RemoveAt(index);
                }

                if (this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Count > 0)
                {
                    index = indexLast;

                    if ((index + 1) > this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Count)
                    {
                        this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndex = this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Count - 1;
                    }
                    else
                    {
                        this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndex = 0;
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func4", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func4", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>出力明細項目削除(F5)処理</summary>
        public bool Func5()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 選択されている出力項目（明細）のインデックス取得
                int index = this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex;
                if (index < 0)
                {
                    return false;
                }

                // 選択されている出力項目（明細）文字列取得
                string tmp = (string)this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index];
                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index];

                // 出力可能項目（明細）へ追加
                this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Add(tmp);
                this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Add(chouhyouOutKoumokuGroup);

                this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndices.Clear();

                this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndex = this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Count - 1;

                // 選択されている出力項目（明細）削除
                this.form.customListBoxSyutsuryokuKomokuMeisai.Items.RemoveAt(index);
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.RemoveAt(index);
                if (this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count > 0)
                {
                    if ((index + 1) > this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count)
                    {
                        this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex = index - 1;
                    }
                    else
                    {
                        this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex = index;
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func5", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func5", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>出力明細項目全削除(F6)処理</summary>
        public bool Func6()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count <= 0)
                {
                    return false;
                }

                // {0}から全項目削除します。よろしいですか？
                if (this.msgLogic.MessageBoxShow("C037", new string[] { "出力項目（明細）" }) != DialogResult.Yes)
                {
                    return false;
                }

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup;
                for (int i = 0; i < this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count; i++)
                {
                    this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Add(this.form.customListBoxSyutsuryokuKomokuMeisai.Items[i]);
                    chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[i];
                    this.form.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Add(chouhyouOutKoumokuGroup);
                }

                this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndices.Clear();

                this.form.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndex = this.form.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Count - 1;

                this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Clear();
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.Clear();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func6", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func6", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>表示(F7)処理</summary>
        public bool Func7(bool isUpdateData = true)
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 画面情報からの取得処理
                if (isUpdateData)
                {
                    if (this.UpdateData(7))
                    {   // 入力値エラー

                        return false;
                    }
                }

                // 帳票出力用データーテーブルの取得処理
                this.form.CommonChouhyou.GetOutDataTable();

                // 帳票用データーテーブル
                DataTable dataTableForForm = this.form.CommonChouhyou.ChouhyouDataTable;

                if (dataTableForForm.Rows.Count == 0)
                {
                    // 出力する該当データがありません。
                    this.msgLogic.MessageBoxShow("E044", new string[] { string.Empty });

                    return false;
                }

                ReportInfoBase reportInfo = null;
                DialogResult dialogResult;
                switch (this.form.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:             // R358(売上明細表)
                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:           // R362(支払明細表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:    // R355(売上／支払明細表)

                        reportInfo = new ReportInfoR358_R362_R355(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:            // R359(売上集計表)
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:          // R363(支払集計表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:   // R356(売上／支払集計表)

                        reportInfo = new ReportInfoR359_R363_R356(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:            // R366(入金明細表)
                    case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:           // R373(出金明細表)

                        reportInfo = new ReportInfoR366_R373(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:           // R367(入金集計表)
                    case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:          // R374(出金集計表)

                        reportInfo = new ReportInfoR367_R374(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:            // R379(請求明細表)
                    case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:     // R384(支払明細明細表)

                        reportInfo = new ReportInfoR379_R384(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:         // R369(未入金一覧表)
                    case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:         // R376(未出金一覧表)

                        reportInfo = new ReportInfoR369_R376(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:     // R370(入金予定一覧表)
                    case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:     // R377(出金予定一覧表)

                        reportInfo = new ReportInfoR370_R377(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                    case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR432(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                    case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR433(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR434(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:           // R342(受付明細表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR342(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:            // R398(運賃明細表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR398(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:            // R351(計量明細表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR351(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:           // R352(計量集計表)

                        if (this.form.CommonChouhyou.MaxRowCount > this.form.CommonChouhyou.IchiranAlertCount)
                        {
                            // 検索件数がアラート件数を超えました。<br>表示を行いますか？
                            dialogResult = this.msgLogic.MessageBoxShow("C025", new string[] { string.Empty });

                            if (dialogResult != DialogResult.Yes)
                            {   // 表示しない

                                return false;
                            }
                        }

                        reportInfo = new ReportInfoR352(this.form.WindowId, dataTableForForm, this.form.CommonChouhyou);

                        break;
                    default:
                        break;
                }

                if (reportInfo != null)
                {
                    // フォーム情報取得
                    string outputFormFullPathName = this.form.CommonChouhyou.OutputFormFullPathName;
                    string formLayoutName = this.form.CommonChouhyou.OutputFormLayout;
                    reportInfo.Create(outputFormFullPathName, formLayoutName, dataTableForForm);

                    // XPSファイルタイトル設定
                    reportInfo.Title = this.form.WindowId.ToTitleString();

                    switch (this.form.WindowId)
                    {
                        case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:   // R342(受付明細表)

                            #region - R342(受付明細表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する

                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG536_G537_G538(hearfrom, WINDOW_ID.R_UKETSUKE_MEISAIHYOU);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない

                                // 印刷ポップアップ画面表示
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R342(受付明細表) -

                            break;

                        case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:    // R398(運賃明細表)

                            #region - R398(運賃明細表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する

                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG536_G537_G538(hearfrom, WINDOW_ID.R_UNNCHIN_MEISAIHYOU);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R398(運賃明細表) -

                            break;

                        case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:    // R342(計量明細表)

                            #region - R342(計量明細表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する

                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG536_G537_G538(hearfrom, WINDOW_ID.R_KEIRYOU_MEISAIHYOU);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない

                                // 印刷ポップアップ画面表示
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R342(計量明細表) -

                            break;
                        case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:   // R352(計量集計表)

                            #region - R352(計量集計表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する
                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG539(hearfrom, WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R352(計量集計表) -

                            break;
                        case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                        case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                        case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)

                            #region - R432(売上推移表・支払推移表・売上/支払推移表・計量推移表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する
                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG533(hearfrom, this.form.WindowId);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R432(売上推移表・支払推移表・売上/支払推移表・計量推移表) -

                            break;
                        case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                        case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                        case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)

                            #region - R433(売上順位表・支払順位表・売上/支払順位表・計量順位表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する
                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG534(hearfrom, this.form.WindowId);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R433(売上順位表・支払順位表・売上/支払順位表・計量順位表) -

                            break;
                        case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                        case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                        case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)

                            #region - R433(売上前年対比表・支払前年対比表・売上/支払前年対比表・計量前年対比表) -

                            if (this.form.IsMultiRowExist)
                            {   // MultiRowが存在する

                                var hearfrom = new UIHeaderForm();
                                var callForm = new UIFormG535(hearfrom, this.form.WindowId);
                                callForm.ReportInfo = reportInfo;
                                var showForm = new ReportBaseForm(callForm, hearfrom);
                                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                                if (!isExistForm)
                                {
                                    showForm.ShowDialog();
                                    showForm.Dispose();
                                }
                            }
                            else
                            {   // MultiRowが存在しない
                                using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                                {
                                    popup.IsOutputPDF = this.form.IsOutputPDF;
                                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                    popup.ShowDialog();
                                    popup.Dispose();
                                }
                            }

                            #endregion - R433(売上前年対比表・支払前年対比表・売上/支払前年対比表・計量前年対比表) -

                            break;
                        default:
                            // 印刷ポップアップ画面表示
                            using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                            {
                                popup.IsOutputPDF = this.form.IsOutputPDF;
                                popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                                popup.ShowDialog();
                                popup.Dispose();
                            }

                            break;
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func7", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func7", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>登録(F9)処理</summary>
        public bool Func9()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 画面情報からの取得処理
                if (this.UpdateData(9))
                {   // 入力値エラー
                    return true;
                }

                // WHOカラム設定
                var who1 = new DataBinderLogic<M_LIST_PATTERN>(this.mlistPattern);
                who1.SetSystemProperty(this.mlistPattern, true);

                // WHOカラム設定
                var who2 = new DataBinderLogic<M_LIST_PATTERN_FILL_COND>(this.mlistPatternFillCond);
                who2.SetSystemProperty(this.mlistPatternFillCond, true);

                // WHOカラム設定
                var who3 = new DataBinderLogic<M_LIST_PATTERN_COLUMN>(this.mlistPatternColumn);
                who3.SetSystemProperty(this.mlistPatternColumn, true);

                int intTmp;
                if (this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.Modify)
                {   // 修正モード
                    string sql = string.Format("SELECT * FROM M_LIST_PATTERN WHERE M_LIST_PATTERN.SYSTEM_ID = {0} AND M_LIST_PATTERN.SEQ = {1} AND DELETE_FLG = 0", this.form.SystemID, this.form.SequenceID);
                    DataTable dataTable = this.mlpDao.GetDateForStringSql(sql);

                    int index = dataTable.Columns.IndexOf("TIME_STAMP");
                    this.mlistPattern.TIME_STAMP = (byte[])dataTable.Rows[0].ItemArray[index];

                    this.mlistPattern.SYSTEM_ID = this.form.SystemID;

                    // 枝番
                    this.mlistPattern.SEQ = this.form.SequenceID;

                    // 画面ID
                    intTmp = (int)this.form.WindowId;
                    this.mlistPattern.WINDOW_ID = (SqlInt32)intTmp;

                    // パターン名
                    this.mlistPattern.PATTERN_NAME = this.form.ChouhyouName;

                    // 削除フラグ
                    this.mlistPattern.DELETE_FLG = true;

                    this.mlistPattern.CREATE_USER = this.form.CreateUser;
                    this.mlistPattern.CREATE_PC = this.form.CreatePC;
                    this.mlistPattern.CREATE_DATE = this.form.CreateDate;
                    this.mlistPattern.TIME_STAMP = (byte[])this.form.TimeStamp;

                    // データーベースに新規追加
                    this.mlpDao.Update(this.mlistPattern);
                }

                #region - M_LIST_PATTERN -

                // システムID
                DBAccessor dbAccessor = new DBAccessor();
                int densyuKubun = (int)DENSHU_KBN.HANYOU_CHOUHYOU;

                // 枝番
                if (this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.New || this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify)
                {   // 新規モード又は新規編集モード
                    this.mlistPattern.SYSTEM_ID = dbAccessor.createSystemId((SqlInt16)densyuKubun);
                    this.mlistPattern.SEQ = 1;
                }
                else
                {   // 編集モード
                    this.mlistPattern.SYSTEM_ID = this.form.SystemID;
                    this.mlistPattern.SEQ = this.form.SequenceID + 1;
                    this.mlistPattern.TIME_STAMP = null;
                }

                // 画面ID
                intTmp = (int)this.form.WindowId;
                this.mlistPattern.WINDOW_ID = (SqlInt32)intTmp;

                // パターン名
                this.mlistPattern.PATTERN_NAME = this.form.CommonChouhyou.Name;

                // 削除フラグ
                this.mlistPattern.DELETE_FLG = false;

                // データーベースに新規追加
                this.mlpDao.Insert(this.mlistPattern);

                #endregion - M_LIST_PATTERN -

                #region - M_LIST_PATTERN_FILL_COND -

                // システムID
                this.mlistPatternFillCond.SYSTEM_ID = this.mlistPattern.SYSTEM_ID;

                // 枝番
                if (this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.New || this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify)
                {   // 新規モード又は新規編集モード
                    this.mlistPatternFillCond.SEQ = 1;
                }
                else
                {   // 編集モード
                    this.mlistPatternFillCond.SEQ = this.form.SequenceID + 1;
                }

                // 日付範囲条件
                intTmp = (int)this.form.CommonChouhyou.KikanShiteiType;
                this.mlistPatternFillCond.FILL_COND_DATE_KBN = (SqlInt16)intTmp;

                // 日付範囲BEGIN
                DateTime aaa = new DateTime(0);
                if (this.form.CommonChouhyou.DateTimeStart.Equals(new DateTime(0)))
                {
                    this.mlistPatternFillCond.FILL_COND_DATE_BEGIN = SqlDateTime.Null;
                }
                else
                {
                    this.mlistPatternFillCond.FILL_COND_DATE_BEGIN = this.form.CommonChouhyou.DateTimeStart;
                }

                // 日付範囲END
                if (this.form.CommonChouhyou.DateTimeEnd.Equals(new DateTime(0)))
                {
                    this.mlistPatternFillCond.FILL_COND_DATE_END = SqlDateTime.Null;
                }
                else
                {
                    this.mlistPatternFillCond.FILL_COND_DATE_END = this.form.CommonChouhyou.DateTimeEnd;
                }

                // 拠点CD
                intTmp = int.Parse(this.form.CommonChouhyou.KyotenCode);
                this.mlistPatternFillCond.FILL_COND_KYOTEN_CD = (SqlInt16)intTmp;

                // 伝票種類
                intTmp = (int)this.form.CommonChouhyou.DenpyouSyurui;
                if (this.form.CommonChouhyou.IsDenpyouSyuruiGroupKubun)
                {   // グループ区分有
                    this.mlistPatternFillCond.FILL_COND_DENPYOU_SBT = (SqlInt16)intTmp + 10;
                }
                else
                {   // グループ区分無
                    this.mlistPatternFillCond.FILL_COND_DENPYOU_SBT = (SqlInt16)intTmp;
                }

                // 伝票区分
                intTmp = (int)this.form.CommonChouhyou.DenpyouKubun;
                this.mlistPatternFillCond.FILL_COND_DENPYOU_KBN = (SqlInt16)intTmp;

                for (int i = 0; i < 4; i++)
                {
                    int tmp = this.form.CommonChouhyou.SelectSyuukeiKoumokuList[i];
                    SyuukeiKoumoku syuukeiKoumoku = this.form.CommonChouhyou.SyuukeiKomokuList[tmp];
                    SyuukeiKoumokuHani syuukeiKoumokuHani = syuukeiKoumoku.SyuukeiKoumokuHani;

                    switch (i)
                    {
                        case 0:
                            // 第１項目ID
                            intTmp = (int)syuukeiKoumoku.Type;
                            this.mlistPatternFillCond.FILL_COND_ID_1 = tmp;

                            // 第１項目CD_BEGIN
                            this.mlistPatternFillCond.FILL_COND_CD_BEGIN_1 = syuukeiKoumokuHani.CodeStart;

                            // 第１項目CD_END
                            this.mlistPatternFillCond.FILL_COND_CD_END_1 = syuukeiKoumokuHani.CodeEnd;

                            break;
                        case 1:
                            // 第２項目ID
                            intTmp = (int)syuukeiKoumoku.Type;
                            this.mlistPatternFillCond.FILL_COND_ID_2 = tmp;

                            // 第２項目CD_BEGIN
                            this.mlistPatternFillCond.FILL_COND_CD_BEGIN_2 = syuukeiKoumokuHani.CodeStart;

                            // 第２項目CD_END
                            this.mlistPatternFillCond.FILL_COND_CD_END_2 = syuukeiKoumokuHani.CodeEnd;

                            break;
                        case 2:
                            // 第３項目ID
                            intTmp = (int)syuukeiKoumoku.Type;
                            this.mlistPatternFillCond.FILL_COND_ID_3 = tmp;

                            // 第３項目CD_BEGIN
                            this.mlistPatternFillCond.FILL_COND_CD_BEGIN_3 = syuukeiKoumokuHani.CodeStart;

                            // 第３項目CD_END
                            this.mlistPatternFillCond.FILL_COND_CD_END_3 = syuukeiKoumokuHani.CodeEnd;

                            break;
                        case 3:
                            // 第４項目ID
                            intTmp = (int)syuukeiKoumoku.Type;
                            this.mlistPatternFillCond.FILL_COND_ID_4 = tmp;

                            // 第４項目CD_BEGIN
                            this.mlistPatternFillCond.FILL_COND_CD_BEGIN_4 = syuukeiKoumokuHani.CodeStart;

                            // 第４項目CD_END
                            this.mlistPatternFillCond.FILL_COND_CD_END_4 = syuukeiKoumokuHani.CodeEnd;

                            break;
                    }
                }

                // データーベースに新規追加
                this.mlpfcDao.Insert(this.mlistPatternFillCond);

                #endregion - M_LIST_PATTERN_FILL_COND -

                #region - M_LIST_PATTERN_COLUMN -

                // 伝票
                for (int i = 0; i < this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.Count; i++)
                {
                    ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[i];

                    ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                    if (chouhyouOutKoumoku == null)
                    {
                        continue;
                    }

                    // システムID
                    this.mlistPatternColumn.SYSTEM_ID = this.mlistPattern.SYSTEM_ID;

                    // 枝番
                    if (this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.New || this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify)
                    {   // 新規モード又は新規編集モード
                        this.mlistPatternColumn.SEQ = 1;
                    }
                    else
                    {   // 編集モード
                        this.mlistPatternColumn.SEQ = this.form.SequenceID + 1;
                    }

                    // 明細システムID
                    this.mlistPatternColumn.DETAIL_SYSTEM_ID = dbAccessor.createSystemId((SqlInt16)densyuKubun);

                    // 明細区分
                    this.mlistPatternColumn.DETAIL_KBN = false;

                    // 項番
                    this.mlistPatternColumn.ROW_NO = (SqlInt16)(i + 1);

                    // 画面ID
                    this.mlistPatternColumn.WINDOW_ID = this.mlistPattern.WINDOW_ID;

                    // 項目ID
                    this.mlistPatternColumn.KOUMOKU_ID = chouhyouOutKoumoku.ID;

                    // データーベースに新規追加
                    this.mlpcDao.Insert(this.mlistPatternColumn);
                }

                // 明細
                for (int i = 0; i < this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.Count; i++)
                {
                    ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[i];

                    ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[0];

                    if (chouhyouOutKoumoku == null)
                    {
                        continue;
                    }

                    // システムID
                    this.mlistPatternColumn.SYSTEM_ID = this.mlistPattern.SYSTEM_ID;

                    // 枝番
                    if (this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.New || this.form.DisplayMode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify)
                    {   // 新規モード又は新規編集モード
                        this.mlistPatternColumn.SEQ = 1;
                    }
                    else
                    {   // 編集モード
                        this.mlistPatternColumn.SEQ = this.form.SequenceID + 1;
                    }

                    // 明細システムID
                    this.mlistPatternColumn.DETAIL_SYSTEM_ID = dbAccessor.createSystemId((SqlInt16)densyuKubun);

                    // 明細区分
                    this.mlistPatternColumn.DETAIL_KBN = true;

                    // 項番
                    this.mlistPatternColumn.ROW_NO = (SqlInt16)(i + 1);

                    // 画面ID
                    this.mlistPatternColumn.WINDOW_ID = this.mlistPattern.WINDOW_ID;

                    // 項目ID
                    this.mlistPatternColumn.KOUMOKU_ID = chouhyouOutKoumoku.ID;

                    // データーベースに新規追加
                    this.mlpcDao.Insert(this.mlistPatternColumn);
                }

                #endregion - M_LIST_PATTERN_COLUMN -

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func7", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func7", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion - Function Key Proc -

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>登録処理を実行する</summary>
        /// <param name="errorFlag">かどうかを表す値</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>検索処理を実行し数値を取得する</summary>
        /// <returns>？？？？？</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>更新処理を実行する</summary>
        /// <param name="errorFlag">エラーフラグかどうかを表す値</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>出力伝票項目上移動処理を実行する</summary>
        internal bool SyutsuryokuKanoKomokuDenpyoUeIdo()
        {
            try
            {
                if (this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count < 2)
                {   // 選択項目がないか項目が１つ
                    return false;
                }

                int index = this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex;
                if (index == 0)
                {   // 先頭が選択されている
                    return false;
                }

                string tmp = (string)this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index - 1];
                this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index - 1] = this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index];
                this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index] = tmp;

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index - 1];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index - 1] = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index] = chouhyouOutKoumokuGroup;

                this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex = index - 1;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuDenpyoUeIdo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuDenpyoUeIdo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>出力伝票項目下移動処理を実行する</summary>
        internal bool SyutsuryokuKanoKomokuDenpyoShitaIdo()
        {
            try
            {
                if (this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count < 2)
                {   // 選択項目がないか項目が１つ
                    return false;
                }

                int index = this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex;
                if (index == this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count - 1)
                {   // 末尾が選択されている
                    return false;
                }

                string tmp = (string)this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index + 1];
                this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index + 1] = this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index];
                this.form.customListBoxSyutsuryokuKomokuDenpyo.Items[index] = tmp;

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index + 1];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index + 1] = this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList[index] = chouhyouOutKoumokuGroup;

                this.form.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex = index + 1;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuDenpyoShitaIdo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuDenpyoShitaIdo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>出力明細項目上移動処理を実行する</summary>
        internal bool SyutsuryokuKanoKomokuMeisaiUeIdo()
        {
            try
            {
                if (this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count < 2)
                {   // 選択項目がないか項目が１つ
                    return false;
                }

                int index = this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex;
                if (index == 0)
                {   // 先頭が選択されている
                    return false;
                }

                string tmp = (string)this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index - 1];
                this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index - 1] = this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index];
                this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index] = tmp;

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index - 1];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index - 1] = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index] = chouhyouOutKoumokuGroup;

                this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex = index - 1;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuMeisaiUeIdo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuMeisaiUeIdo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>出力明細項目下移動処理を実行する</summary>
        internal bool SyutsuryokuKanoKomokuMeisaiShitaIdo()
        {
            try
            {
                if (this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count < 2)
                {   // 選択項目がないか項目が１つ
                    return false;
                }

                int index = this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex;
                if (index == this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count - 1)
                {   // 末尾が選択されている
                    return false;
                }

                string tmp = (string)this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index + 1];
                this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index + 1] = this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index];
                this.form.customListBoxSyutsuryokuKomokuMeisai.Items[index] = tmp;

                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index + 1];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index + 1] = this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index];
                this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList[index] = chouhyouOutKoumokuGroup;

                this.form.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex = index + 1;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuMeisaiUeIdo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SyutsuryokuKanoKomokuMeisaiUeIdo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>システムＩＤ(SYSTEM_ID)及び最大シーケンス番号(SEQ)取得処理を取得する</summary>
        private void GetMaxSequenceNo()
        {
            try
            {
                // SEQ項目の最大値取得(Test)
                string sql = string.Format("SELECT * FROM M_LIST_PATTERN WHERE WINDOW_ID = {0} AND DELETE_FLG = 0 AND SEQ = (SELECT MAX(SEQ) FROM M_LIST_PATTERN WHERE WINDOW_ID = {1} AND DELETE_FLG = 0)", (int)this.form.WindowId, (int)this.form.WindowId);
                DataTable dataTable = this.mlpDao.GetDateForStringSql(sql);

                int index;
                if (dataTable.Rows.Count != 0)
                {
                    DataColumnCollection columns = dataTable.Columns;

                    // システムＩＤ
                    index = columns.IndexOf("SYSTEM_ID");
                    this.systemID = (long)dataTable.Rows[0].ItemArray[index];

                    // 最大シーケンス番号
                    index = columns.IndexOf("SEQ");
                    this.maxSeqNo = (int)dataTable.Rows[0].ItemArray[index];
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>ボタン設定の読込</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

        /// <summary>ボタン初期化処理</summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>イベントの初期化処理</summary>
        private void EventInit()
        {
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                // 伝票項目追加ボタン(F1)イベント生成
                this.form.bt_func1_1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);

                // 伝票項目削除ボタン(F2)イベント生成
                this.form.bt_func2_1.Click += new EventHandler(this.form.ButtonFunc2_Clicked);

                // 伝票項目全削除ボタン(F3)イベント生成
                this.form.bt_func3_1.Click += new EventHandler(this.form.ButtonFunc3_Clicked);

                // 明細項目追加ボタン(F4)イベント生成
                this.form.bt_func4_1.Click += new EventHandler(this.form.ButtonFunc4_Clicked);

                // 明細項目削除ボタン(F5)イベント生成
                this.form.bt_func5_1.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

                // 明細項目全削除ボタン(F6)イベント生成
                this.form.bt_func6_1.Click += new EventHandler(this.form.ButtonFunc6_Clicked);

                // 明細項目表示ボタン(F7)イベント生成
                //parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
                this.form.C_Regist(parentForm.bt_func7);
                parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

                // 明細項目登録ボタン(F9)イベント生成
                //parentForm.bt_func9.Click += new EventHandler(this.form.ButtonFunc9_Clicked);
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.ButtonFunc9_Clicked);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

                // 20141128 teikyou 「モバイル将軍出力」のダブルクリックを追加する　start
                this.form.customDateTimePickerHidukeHaniShiteiEnd.MouseDoubleClick += new MouseEventHandler(HidukeHaniShiteiEnd_MouseDoubleClick);
                this.form.customAlphaNumTextBoxSyukeiKomoku1EndCD.MouseDoubleClick += new MouseEventHandler(SyukeiKomoku1EndCD_MouseDoubleClick);
                this.form.customAlphaNumTextBoxSyukeiKomoku2EndCD.MouseDoubleClick += new MouseEventHandler(SyukeiKomoku2EndCD_MouseDoubleClick);
                this.form.customAlphaNumTextBoxSyukeiKomoku3EndCD.MouseDoubleClick += new MouseEventHandler(SyukeiKomoku3EndCD_MouseDoubleClick);
                this.form.customAlphaNumTextBoxSyukeiKomoku4EndCD.MouseDoubleClick += new MouseEventHandler(SyukeiKomoku4EndCD_MouseDoubleClick);
                // 20141128 teikyou 「モバイル将軍出力」のダブルクリックを追加する　end
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>画面情報からの取得処理を実行する</summary>
        /// <param name="funcNo">ファンクションキー番号を表す数値</param>
        /// <returns>真の場合：正常、偽の場合：データエラー</returns>
        private bool UpdateData(int funcNo)
        {
            try
            {
                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return true;
                }
                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

                this.form.CommonChouhyou.DateTimePrint = DateTime.Now;

                // 帳票名
                this.form.CommonChouhyou.Name = this.form.customTextBoxChohyoMei.Text;

                // 日付範囲指定
                DateTime today = DateTime.Today;
                string tmp;

                this.form.CommonChouhyou.KikanShiteiType = (CommonChouhyouBase.KIKAN_SHITEI_TYPE)int.Parse(this.form.CustomNumericTextBox2HidukeHaniShiteiHoho.Text);

                if (this.form.WindowId == WINDOW_ID.R_URIAGE_SUIIHYOU || this.form.WindowId == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                    this.form.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU || this.form.WindowId == WINDOW_ID.R_KEIRYOU_SUIIHYOU ||
                    this.form.WindowId == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU || this.form.WindowId == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU ||
                    this.form.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU || this.form.WindowId == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {   // 売上推移表・支払推移表・売上／支払推移表・計量推移表
                    // 売上前年対比表・支払前年対比表・売上／支払前年対比表・計量前年対比表

                    if (this.form.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Toujitsu)
                    {   // 当日
                        this.form.CommonChouhyou.DateTimeStart = today;

                        tmp = string.Format("{0:D4}/{1:D2}/{2:D2} 23:59:59", today.Year, today.Month, today.Day);
                        this.form.CommonChouhyou.DateTimeEnd = DateTime.Parse(tmp);
                    }
                    else if (this.form.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Tougets)
                    {   // 当月
                        this.form.CommonChouhyou.DateTimeStart = today.AddDays(-today.Day + 1);

                        DateTime firstDay = today.AddDays(-today.Day + 1);
                        DateTime endDay = firstDay.AddMonths(1).AddDays(-1);

                        tmp = string.Format("{0:D4}/{1:D2}/{2:D2} 23:59:59", endDay.Year, endDay.Month, endDay.Day);
                        this.form.CommonChouhyou.DateTimeEnd = DateTime.Parse(tmp);
                    }
                    else
                    {   // 期間指定

                        if (this.form.customDateTimePickerHidukeHaniShiteiStart.Value == null)
                        {   // 開始日付が指定されていない

                            // {0}は必須項目です。入力してください。
                            this.msgLogic.MessageBoxShow("E001", new string[] { "開始日付" });

                            return true;
                        }

                        if (this.form.customDateTimePickerHidukeHaniShiteiEnd.Value == null)
                        {   // 終了日付が指定されていない

                            // {0}は必須項目です。入力してください。
                            this.msgLogic.MessageBoxShow("E001", new string[] { "終了日付" });

                            return true;
                        }

                        DateTime dateTimeStartTmp = (DateTime)this.form.customDateTimePickerHidukeHaniShiteiStart.Value;
                        DateTime dateTimeEndTmp = (DateTime)this.form.customDateTimePickerHidukeHaniShiteiEnd.Value;
                        DateTime dateTimeTmp = dateTimeStartTmp.AddMonths(12);

                        if (dateTimeEndTmp >= dateTimeTmp)
                        {   // １年以上の範囲が指定されている

                            // 日付範囲指定は１年間の範囲で指定してください。
                            this.msgLogic.MessageBoxShow("E138", new string[] { string.Empty });

                            return true;
                        }

                        this.form.CommonChouhyou.DateTimeStart = dateTimeStartTmp;
                        this.form.CommonChouhyou.DateTimeEnd = dateTimeEndTmp;
                    }
                }
                else
                {
                    if (this.form.customDateTimePickerHidukeHaniShiteiStart.Value != null)
                    {
                        this.form.CommonChouhyou.DateTimeStart = (DateTime)this.form.customDateTimePickerHidukeHaniShiteiStart.Value;
                    }
                    else
                    {
                        if (this.form.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Toujitsu)
                        {   // 当日
                            this.form.CommonChouhyou.DateTimeStart = today;
                        }
                        else if (this.form.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Tougets)
                        {   // 当月
                            this.form.CommonChouhyou.DateTimeStart = today.AddDays(-today.Day + 1);
                        }
                        else
                        {   // 期間指定
                            this.form.CommonChouhyou.DateTimeStart = new DateTime(0);
                        }
                    }

                    if (this.form.customDateTimePickerHidukeHaniShiteiEnd.Value != null)
                    {
                        this.form.CommonChouhyou.DateTimeEnd = (DateTime)this.form.customDateTimePickerHidukeHaniShiteiEnd.Value;
                    }
                    else
                    {
                        if (this.form.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Toujitsu)
                        {   // 当日
                            tmp = string.Format("{0:D4}/{1:D2}/{2:D2} 23:59:59", today.Year, today.Month, today.Day);

                            this.form.CommonChouhyou.DateTimeEnd = DateTime.Parse(tmp);
                        }
                        else if (this.form.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Tougets)
                        {   // 当月
                            DateTime firstDay = today.AddDays(-today.Day + 1);
                            DateTime endDay = firstDay.AddMonths(1).AddDays(-1);

                            tmp = string.Format("{0:D4}/{1:D2}/{2:D2} 23:59:59", endDay.Year, endDay.Month, endDay.Day);
                            this.form.CommonChouhyou.DateTimeEnd = DateTime.Parse(tmp);
                        }
                        else
                        {   // 期間指定
                            this.form.CommonChouhyou.DateTimeEnd = new DateTime(0);
                        }
                    }
                }

                // 拠点指定
                this.form.CommonChouhyou.KyotenCode = this.form.CustomNumericTextBox2KyotenShiteiCD.Text;
                this.form.CommonChouhyou.KyotenCodeName = this.form.customTextBoxKyotenShiteiMei.Text;

                // 伝票種類指定
                switch (this.form.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:                     // R358(売上明細表)
                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:                   // R362(支払明細表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:            // R355(売上／支払明細表)
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:                    // R359(売上集計表)
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:                  // R363(支払集計表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:           // R356(売上／支払集計表)
                    case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:                    // R366(入金明細表)
                    case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:                   // R373(出金明細表)
                    case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:                   // R367(入金集計表)
                    case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:                  // R374(出金集計表)
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:                    // R379(請求明細表)
                    case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:             // R384(支払明細明細表)
                    case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:                 // R369(未入金一覧表)
                    case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:                 // R376(未出金一覧表)
                    case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:             // R370(入金予定一覧表)
                    case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:             // R377(出金予定一覧表)
                        this.form.CommonChouhyou.DenpyouSyurui = (CommonChouhyouBase.DENPYOU_SYURUI)int.Parse(this.form.CustomNumericTextBox2DenpyoSyuruiShitei.Text);

                        break;
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                    case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_SUIIHYOU:          // R432(売上・支払（全て）推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                    case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_JYUNNIHYOU:        // R433(売上・支払（全て）順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_ZENNEN_TAIHIHYOU:  // R434(売上・支払（全て）前年対比表)
                        this.form.CommonChouhyou.DenpyouSyurui = (CommonChouhyouBase.DENPYOU_SYURUI)int.Parse(this.form.CustomNumericTextBox2DenpyoSyuruiShitei2.Text);

                        break;
                    case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:                   // R342 受付明細表
                        this.form.CommonChouhyou.DenpyouSyurui = (CommonChouhyouBase.DENPYOU_SYURUI)int.Parse(this.form.CustomNumericTextBox2DenpyoSyuruiShitei3.Text);

                        break;
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:                    // R398 運賃明細表
                        this.form.CommonChouhyou.DenpyouSyurui = (CommonChouhyouBase.DENPYOU_SYURUI)int.Parse(this.form.CustomNumericTextBox2DenpyoSyuruiShitei4.Text);

                        break;
                    case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:                    // R351 計量明細表
                    case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:                   // R352 計量集計表
                        this.form.CommonChouhyou.DenpyouSyurui = (CommonChouhyouBase.DENPYOU_SYURUI)int.Parse(this.form.CustomNumericTextBox2DenpyoSyuruiShitei.Text);

                        break;

                    default:
                        break;
                }

                // 伝票種類グループ区分有無
                this.form.CommonChouhyou.IsDenpyouSyuruiGroupKubun = this.form.customCheckBoxSyuruiShiteiGroupKubun2.Checked;

                // 伝票区分指定
                this.form.CommonChouhyou.DenpyouKubun = (CommonChouhyouBase.DENPYOU_KUBUN)int.Parse(this.form.CustomNumericTextBox2DenpyoKubunShitei.Text);

                // 集計項目１～最大集計項目４
                int id;
                for (int i = 0; i < this.form.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount; i++)
                {
                    id = this.form.CommonChouhyou.SelectSyuukeiKoumokuList[i];

                    SyuukeiKoumoku syuukeiKoumoku = this.form.CommonChouhyou.SyuukeiKomokuList[id];

                    syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart = this.form.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                    syuukeiKoumoku.SyuukeiKoumokuHani.CodeStartName = this.form.CustomControlSyukeiKomokuList[i].TextBoxSyukeiKomokuStartMeisho.Text;

                    syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd = this.form.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                    syuukeiKoumoku.SyuukeiKoumokuHani.CodeEndName = this.form.CustomControlSyukeiKomokuList[i].TextBoxSyukeiKomokuEndMeisho.Text;
                }

                // 帳票名確認
                if (this.form.CommonChouhyou.Name.Equals(string.Empty))
                {   // 帳票名がない

                    // {0}は必須項目です。入力してしてください。
                    this.msgLogic.MessageBoxShow("E001", new string[] { "帳票名" });

                    return true;
                }

                if (funcNo != 7)
                {   // 表示でない

                    //if (this.form.DisplayMode != G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.Modify)
                    //{   // 修正モードでない

                    foreach (string patternName in this.form.PatternNameList)
                    {
                        if (patternName.Equals(this.form.CommonChouhyou.Name))
                        {   // 同一の帳票（パターン）名が存在している

                            // {0}はすでに登録されています。
                            this.msgLogic.MessageBoxShow("E022", new string[] { "帳票名" });

                            return true;
                        }
                    }
                    //}
                }

                // 日付範囲指定
                if (!this.form.CommonChouhyou.DateTimeStart.Equals(new DateTime(0)) && this.form.CommonChouhyou.DateTimeEnd.Equals(new DateTime(0)))
                {   // 開始日がある及び終了日がない
                }
                else
                {
                    if (this.form.CommonChouhyou.DateTimeEnd < this.form.CommonChouhyou.DateTimeStart)
                    {   // 日付範囲が逆

                        // {1}が{0}より前の値になっています。{1}には{0}以降の値を指定してください。
                        this.msgLogic.MessageBoxShow("E032", new string[] { "日付範囲指定開始日", "日付範囲指定終了日" });

                        return true;
                    }
                }

                // 拠点指定
                if (this.form.CommonChouhyou.KyotenCode.Equals(string.Empty))
                {
                    // {0}は必須項目です。入力してしてください。
                    this.msgLogic.MessageBoxShow("E001", new string[] { "拠点コード" });

                    return true;
                }

                // 伝票種類指定

                // 伝票区分指定

                // 集計項目１～最大集計項目４
                string codeStart;
                string codeEnd;
                for (int i = 0; i < this.form.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount; i++)
                {
                    id = this.form.CommonChouhyou.SelectSyuukeiKoumokuList[i];

                    SyuukeiKoumoku syuukeiKoumoku = this.form.CommonChouhyou.SyuukeiKomokuList[id];

                    codeStart = this.form.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                    codeEnd = this.form.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;

                    if (!codeStart.Equals(string.Empty) && !codeEnd.Equals(string.Empty))
                    {
                        int iCompare = string.Compare(codeStart, codeEnd, true);
                        if (iCompare > 0)
                        {
                            // {1}が{0}より前の値になっています。{1}には{0}以降の値を指定してください。
                            this.msgLogic.MessageBoxShow("E032", new string[] { "集計項目-終了", "集計項目-開始" });

                            return true;
                        }
                    }

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.DensyuKubunBetsu)
                    {   // 伝種区分別
                        if (this.form.CommonChouhyou.DenpyouSyurui != CommonChouhyouBase.DENPYOU_SYURUI.Subete)
                        {   // 全てでない

                            // {0}を範囲指定する場合は必ず{1}にしてください。
                            this.msgLogic.MessageBoxShow("E054", new string[] { "伝種区分", "伝票種類を「4.全て」" });

                            return true;
                        }
                    }

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu ||
                        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu ||
                        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu ||
                        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                    {   // 親の存在確認
                        int id2;

                        bool isExistParent = false;

                        for (int j = 0; j < this.form.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount; j++)
                        {
                            id2 = this.form.CommonChouhyou.SelectSyuukeiKoumokuList[j];

                            SyuukeiKoumoku syuukeiKoumoku2 = this.form.CommonChouhyou.SyuukeiKomokuList[id2];

                            if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                            {
                                isExistParent = true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                            {
                                isExistParent = true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                            {
                                isExistParent = true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                            {
                                isExistParent = true;
                            }
                        }

                        if (!isExistParent)
                        {   // 親が存在しない
                            if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                            {
                                // 「集計項目」に{0}が選択されていないため、{1}は選択できません。
                                this.msgLogic.MessageBoxShow("E087", new string[] { "業者別", "現場別" });

                                return true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                            {
                                // 「集計項目」に{0}が選択されていないため、{1}は選択できません。
                                this.msgLogic.MessageBoxShow("E087", new string[] { "荷降業者別", "荷降現場別" });

                                return true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                            {
                                // 「集計項目」に{0}が選択されていないため、{1}は選択できません。
                                this.msgLogic.MessageBoxShow("E087", new string[] { "荷積業者別", "荷積現場別" });

                                return true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                            {
                                // 「集計項目」に{0}が選択されていないため、{1}は選択できません。
                                this.msgLogic.MessageBoxShow("E087", new string[] { "銀行別", "銀行支店別" });

                                return true;
                            }
                        }
                    }

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu ||
                        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu ||
                        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu ||
                        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                    {   // 開始・終了コード確認

                        int id2;
                        int indexStartPos = i;
                        int indexEndPos = -1;
                        for (int j = 0; j < this.form.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount; j++)
                        {
                            id2 = this.form.CommonChouhyou.SelectSyuukeiKoumokuList[j];

                            SyuukeiKoumoku syuukeiKoumoku2 = this.form.CommonChouhyou.SyuukeiKomokuList[id2];
                            if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                            {   // 業者別、現場別
                                indexEndPos = j;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                            {   // 荷降業者別・荷降現場別
                                indexEndPos = j;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                            {   // 荷積業者別・荷積現場別
                                indexEndPos = j;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu && syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                            {   // 銀行別・銀行支店別
                                indexEndPos = j;
                            }
                        }

                        if (indexEndPos < indexStartPos && indexEndPos != -1)
                        {
                            if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                            {   // 業者別、現場別

                                // {0}は{1}より上の集計項目で選択してください。
                                this.msgLogic.MessageBoxShow("E089", new string[] { "業者別", "現場別" });

                                return true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                            {   // 荷降業者別・荷降現場別

                                // {0}は{1}より上の集計項目で選択してください。
                                this.msgLogic.MessageBoxShow("E089", new string[] { "荷降業者別", "荷降現場別" });

                                return true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                            {   // 荷積業者別・荷積現場別

                                // {0}は{1}より上の集計項目で選択してください。
                                this.msgLogic.MessageBoxShow("E089", new string[] { "荷積業者別", "荷積現場別" });

                                return true;
                            }
                            else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                            {   // 銀行別・銀行支店別

                                // {0}は{1}より上の集計項目で選択してください。
                                this.msgLogic.MessageBoxShow("E089", new string[] { "銀行別", "銀行支店別" });

                                return true;
                            }
                        }
                    }
                }

                if (this.form.WindowId == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU || this.form.WindowId == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU || this.form.WindowId == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
                {   // 入金集計表・集金集計表・計量集計表
                    if (this.form.customListBoxSyutsuryokuKomokuDenpyo.Items.Count + this.form.customListBoxSyutsuryokuKomokuMeisai.Items.Count <= 0)
                    {   // 伝票＋明細の合計数が０件

                        // {0}を選択してください。
                        this.msgLogic.MessageBoxShow("E051", new string[] { "出力可能項目（伝票）／（明細）" });

                        return true;
                    }
                }
                else
                {
                    // 出力項目（伝票）確認
                    if (this.form.CommonChouhyou.OutEnableKoumokuDenpyou)
                    {   // 出力項目（伝票）有効
                        if (this.form.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.Count == 0)
                        {   // 出力項目が選択されていない

                            // {0}を選択してください。
                            this.msgLogic.MessageBoxShow("E051", new string[] { "出力可能項目（伝票）" });

                            return true;
                        }
                    }

                    // 出力項目（明細）確認
                    if (this.form.CommonChouhyou.OutEnableKoumokuMeisai)
                    {   // 出力項目（明細）有効
                        if (this.form.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.Count == 0)
                        {   // 出力項目が選択されていない

                            // {0}を選択してください。
                            this.msgLogic.MessageBoxShow("E051", new string[] { "出力可能項目（明細）" });

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return true;
            }
        }

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.NOMAL_COLOR;
            this.form.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.customDateTimePickerHidukeHaniShiteiStart.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.customDateTimePickerHidukeHaniShiteiEnd.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.customDateTimePickerHidukeHaniShiteiStart.Text);
            DateTime date_to = DateTime.Parse(this.form.customDateTimePickerHidukeHaniShiteiEnd.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.customDateTimePickerHidukeHaniShiteiStart.IsInputErrorOccured = true;
                this.form.customDateTimePickerHidukeHaniShiteiEnd.IsInputErrorOccured = true;
                this.form.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.ERROR_COLOR;
                this.form.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "日付範囲指定From", "日付範囲指定To" };
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                msglogic.MessageBoxShow("E030", errorMsg);
                this.form.customDateTimePickerHidukeHaniShiteiStart.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141128 teikyou ダブルクリックを追加する　start
        // 日付のダブルクリック
        private void HidukeHaniShiteiEnd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeHaniShiteiStartTextBox = this.form.customDateTimePickerHidukeHaniShiteiStart;
            var hidukeHaniShiteiEndTextBox = this.form.customDateTimePickerHidukeHaniShiteiEnd;
            hidukeHaniShiteiEndTextBox.Text = hidukeHaniShiteiStartTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 第１項目のダブルクリック
        private void SyukeiKomoku1EndCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var syukeiKomoku1StartCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku1StartCD;
            var syukeiKomoku1EndCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku1EndCD;
            var syukeiKomoku1StartNameTextBox = this.form.customTextBoxSyukeiKomoku1StartMeisho;
            var syukeiKomoku1EndNameTextBox = this.form.customTextBoxSyukeiKomoku1EndMeisho;
            syukeiKomoku1EndCDTextBox.Text = syukeiKomoku1StartCDTextBox.Text;
            syukeiKomoku1EndNameTextBox.Text = syukeiKomoku1StartNameTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 第２項目のダブルクリック
        private void SyukeiKomoku2EndCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var syukeiKomoku2StartCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku2StartCD;
            var syukeiKomoku2EndCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku2EndCD;
            var syukeiKomoku2StartNameTextBox = this.form.customTextBoxSyukeiKomoku2StartMeisho;
            var syukeiKomoku2EndNameTextBox = this.form.customTextBoxSyukeiKomoku2EndMeisho;
            syukeiKomoku2EndCDTextBox.Text = syukeiKomoku2StartCDTextBox.Text;
            syukeiKomoku2EndNameTextBox.Text = syukeiKomoku2StartNameTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        // 第３項目のダブルクリック
        private void SyukeiKomoku3EndCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var syukeiKomoku3StartCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku3StartCD;
            var syukeiKomoku3EndCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku3EndCD;
            var syukeiKomoku3StartNameTextBox = this.form.customTextBoxSyukeiKomoku3StartMeisho;
            var syukeiKomoku3EndNameTextBox = this.form.customTextBoxSyukeiKomoku3EndMeisho;
            syukeiKomoku3EndCDTextBox.Text = syukeiKomoku3StartCDTextBox.Text;
            syukeiKomoku3EndNameTextBox.Text = syukeiKomoku3StartNameTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        // 第４項目のダブルクリック
        private void SyukeiKomoku4EndCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var syukeiKomoku4StartCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku4StartCD;
            var syukeiKomoku4EndCDTextBox = this.form.customAlphaNumTextBoxSyukeiKomoku4EndCD;
            var syukeiKomoku4StartNameTextBox = this.form.customTextBoxSyukeiKomoku4StartMeisho;
            var syukeiKomoku4EndNameTextBox = this.form.customTextBoxSyukeiKomoku4EndMeisho;
            syukeiKomoku4EndCDTextBox.Text = syukeiKomoku4StartCDTextBox.Text;
            syukeiKomoku4EndNameTextBox.Text = syukeiKomoku4StartNameTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141128 teikyou ダブルクリックを追加する　end
        #endregion
        #endregion - Methods -
    }

    #endregion - LogicClass -

    #region - MListPatternLogic -

    /// <summary>マスターリストパターン用ビジネスロジック</summary>
    internal class MListPatternLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DTO(マスターリストパターンのDTO)</summary>
        private M_LIST_PATTERN mlpDto;

        /// <summary>フォーム</summary>
        private G418_MeisaihyoSyukeihyoJokenShiteiPopupForm form;

        /// <summary>データーテーブルを保持するフィールド</summary>
        private DataTable dataTable;

        //// <summary>最大シーケンス番号を保持するフィールド</summary>
        ////private int maxSeqNo = 0;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public MListPatternLogic(G418_MeisaihyoSyukeihyoJokenShiteiPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            //this.mlpDto = new MLPDtoClass();
            this.mlpDto = new M_LIST_PATTERN();
            this.MasterListPatternDao = DaoInitUtility.GetComponent<IMLPDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>マスターリストパターンのDAOを保持するプロパティ</summary>
        internal IMLPDao MasterListPatternDao { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>削除処理を実行する</summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
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

        /// <summary>パターン名の取得処理を実行する</summary>
        public DataTable GetPatternName()
        {
            try
            {
                this.dataTable = this.MasterListPatternDao.GetMListPatternData((int)this.form.WindowId);

                return this.dataTable;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

        /// <summary>シーケンス番号の最大値取得処理を取得する</summary>
        /// <returns>シーケンス番号の最大値</returns>
        private int GetMaxSequenceNo()
        {
            try
            {
                // SEQ項目の最大値取得(Test)
                string sql = string.Format("SELECT * FROM M_LIST_PATTERN WHERE WINDOW_ID = {0} AND DELETE_FLG = 0 AND SEQ = (SELECT MAX(SEQ) FROM M_LIST_PATTERN WHERE WINDOW_ID = {1} AND DELETE_FLG = 0)", (int)this.form.WindowId, (int)this.form.WindowId);
                DataTable dataTable = this.MasterListPatternDao.GetDateForStringSql(sql);

                if (dataTable.Rows.Count != 0)
                {
                    DataColumnCollection columns = dataTable.Columns;
                    int index = columns.IndexOf("SEQ");

                    return (int)dataTable.Rows[0].ItemArray[index];
                }

                return 0;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return 0;
            }
        }

        #endregion - Methods -
    }

    #endregion - MListPatternLogic -

    #region - MListPatternColumnLogic -

    /// <summary>マスターリストパターンカラム用ビジネスロジック</summary>
    internal class MListPatternColumnLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DAO(マスターリストパターンカラムのDTO)</summary>
        private IMLPCDao mlpDao;

        /// <summary>DTO(マスターリストパターンカラムのDTO)</summary>
        private M_LIST_PATTERN_COLUMN mlpDto;

        /// <summary>フォーム</summary>
        private G418_MeisaihyoSyukeihyoJokenShiteiPopupForm form;

        //// <summary>データーテーブルを保持するフィールド</summary>
        ////private DataTable dataTable;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public MListPatternColumnLogic(G418_MeisaihyoSyukeihyoJokenShiteiPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.mlpDto = new M_LIST_PATTERN_COLUMN();
            this.mlpDao = DaoInitUtility.GetComponent<IMLPCDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>削除処理を実行する</summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
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

        #endregion - Methods -
    }

    #endregion - MListPatternColumnLogic -

    #region - MListPatternFillCondLogic -

    /// <summary>マスターリストパターンコンド用ビジネスロジック</summary>
    internal class MListPatternFillCondLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DAO(マスターリストパターンフィルコンドのDTO)</summary>
        private IMLPFCDao mlpDao;

        /// <summary>DTO(マスターリストパターンフィルコンドのDTO)</summary>
        private M_LIST_PATTERN_FILL_COND mlpDto;

        /// <summary>フォーム</summary>
        private G418_MeisaihyoSyukeihyoJokenShiteiPopupForm form;

        //// <summary>データーテーブルを保持するフィールド</summary>
        ////private DataTable dataTable;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public MListPatternFillCondLogic(G418_MeisaihyoSyukeihyoJokenShiteiPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.mlpDto = new M_LIST_PATTERN_FILL_COND();
            this.mlpDao = DaoInitUtility.GetComponent<IMLPFCDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>削除処理を実行する</summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
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

        #endregion - Methods -
    }

    #endregion - MListPatternFillCondLogic -

    #region - SListColumnSelectLogic -

    /// <summary>一覧表項目選択用ビジネスロジック</summary>
    internal class SListColumnSelectLogic : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>データーベースアクセサを保持するフィールド</summary>
        private DBAccessor dbAccessor = new DBAccessor();

        /// <summary>DAO(マスターリストパターンカラムのDTO)</summary>
        private ISLCSDao slcsDao;

        /// <summary>DTO(マスターリストパターンカラムのDTO)</summary>
        private S_LIST_COLUMN_SELECT slcsDto;

        /// <summary>フォーム</summary>
        private G418_MeisaihyoSyukeihyoJokenShiteiPopupForm form;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ</summary>
        public SListColumnSelectLogic(G418_MeisaihyoSyukeihyoJokenShiteiPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.slcsDto = new S_LIST_COLUMN_SELECT();
            this.slcsDao = DaoInitUtility.GetComponent<ISLCSDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>削除処理を実行する</summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
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

        #endregion - Methods -
    }

    #endregion - SListColumnSelectLogic -

    #endregion - Class -
}
