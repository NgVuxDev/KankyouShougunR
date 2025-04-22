using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - SyuukeiKoumoku -

    /// <summary>集計項目を表すクラス・コントロール</summary>
    public class SyuukeiKoumoku
    {
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="SyuukeiKoumoku" /> class.</summary>
        /// <param name="type">集計項目タイプ</param>
        /// <param name="length">集計項目の桁数</param>
        /// <param name="name">集計項目名</param>
        /// <param name="masterTableID">マスターテーブルＩＤ</param>
        /// <param name="fieldCD">フィールドコード</param>
        /// <param name="fieldCDName">フィールドコード名</param>
        /// <param name="popupWindowName">ポップアップウィンドウ名</param>
        /// <param name="focusOutCheckMethod">フォーカスアウトメソッドを表す文字列</param>
        /// <param name="displayItemName">表示アイテム名</param>
        /// <param name="shortItemName">短縮アイテム名</param>
        /// <param name="tagName">タグ名</param>
        /// <param name="syuukeiJoken">集計条件リストを表す文字列</param>
        /// <param name="isSyuukeiKoumokuHani">集計項目範囲の有効。無効</param>
        /// <param name="syuukeiKoumokuHani">集計項目の範囲</param>
        public SyuukeiKoumoku(SYUKEUKOMOKU_TYPE type, int length, string name, WINDOW_ID masterTableID, string fieldCD, string fieldCDName, string popupWindowName, Collection<SelectCheckDto> focusOutCheckMethod, string displayItemName, string shortItemName, string tagName, bool isSyuukeiKoumokuHani, SyuukeiKoumokuHani syuukeiKoumokuHani)
        {
            // 集計項目タイプ
            this.Type = type;

            // 集計項目の桁数
            this.Length = length;

            // 集計項目名
            this.Name = name;

            // マスターテーブルＩＤ
            this.MasterTableID = masterTableID;

            // フィールドコード
            this.FieldCD = fieldCD;

            // フィールドコード名
            this.FieldCDName = fieldCDName;

            // ポップアップウィンドウ名
            this.PopupWindowName = popupWindowName;

            // フォーカスアウトメソッド名
            this.FocusOutCheckMethod = focusOutCheckMethod;

            // 表示アイテム名
            this.DisplayItemName = displayItemName;

            // 短縮アイテム名
            this.ShortItemName = shortItemName;

            // タグ名
            this.TagName = tagName;

            // 集計項目範囲の有効。無効を保持するプロパティ</summary>
            this.IsSyuukeiKoumokuHani = isSyuukeiKoumokuHani;

            // 集計項目の範囲
            this.SyuukeiKoumokuHani = syuukeiKoumokuHani;

            // ポップアップウィンドウ設定情報処理
            this.MakePopupWindowSettingInfo();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>集計項目の桁数を保持するプロパティ</summary>
        public int Length { get; set; }

        /// <summary>集計項目タイプを保持するプロパティ</summary>
        public SYUKEUKOMOKU_TYPE Type { get; set; }

        /// <summary>集計項目名を保持するプロパティ</summary>
        public string Name { get; set; }

        /// <summary>マスターテーブルＩＤを保持するプロパティ</summary>
        public WINDOW_ID MasterTableID { get; set; }

        /// <summary>フィールドコードを保持するプロパティ</summary>
        public string FieldCD { get; set; }

        /// <summary>フィールドコード名を保持するプロパティ</summary>
        public string FieldCDName { get; set; }

        /// <summary>ポップアップウィンドウ名を保持するプロパティ</summary>
        public string PopupWindowName { get; private set; }

        /// <summary>フォーカスアウトメソッド名を保持するプロパティ</summary>
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; private set; }

        /// <summary>表示アイテム名を保持するプロパティ</summary>
        public string DisplayItemName { get; private set; }

        /// <summary>短縮アイテム名を保持するプロパティ</summary>
        public string ShortItemName { get; private set; }

        /// <summary>タグ名を保持するプロパティ</summary>
        public string TagName { get; private set; }

        /// <summary>集計項目範囲の有効。無効を保持するプロパティ</summary>
        public bool IsSyuukeiKoumokuHani { get; private set; }

        /// <summary>集計項目の範囲を保持するプロパティ</summary>
        public SyuukeiKoumokuHani SyuukeiKoumokuHani { get; set; }

        /// <summary>ジョインメソッドを保持するプロパティ</summary>
        public Collection<JoinMethodDto> PopupWindowSetting { get; private set; }

        /// <summary>現場の選択状況を保持するプロパティ（Trueのときは全選択とみなす）</summary>
        public bool IsSelectGenbaAll { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>ポップアップウィンドウ設定情報処理を実行する</summary>
        private void MakePopupWindowSettingInfo()
        {
            try
            {
                JoinMethodDto joinMethodDto;
                SearchConditionsDto searchConditionsDto;

                switch (this.Type)
                {
                    case SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu:    // 運搬業者別

                        #region - 運搬業者別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_GYOUSHA";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "UNPAN_JUTAKUSHA_KAISHA_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        //searchConditionsDto = new SearchConditionsDto();
                        //searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        //searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        //searchConditionsDto.LeftColumn = "UNPAN_KAISHA_KBN";
                        //searchConditionsDto.Value = "True";
                        //searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        //joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 運搬業者別 -

                        break;
                    case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu: // 荷卸業者別

                        #region - 荷卸業者別

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_GYOUSHA";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 荷卸業者別

                        break;

                    case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別

                        #region - 荷積業者別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_GYOUSHA";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        //searchConditionsDto = new SearchConditionsDto();
                        //searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        //searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        //searchConditionsDto.LeftColumn = "NIOROSHI_GHOUSHA_KBN";
                        //searchConditionsDto.Value = "True";
                        //searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        //joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 荷積業者別 -

                        break;
                    case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷卸現場別

                        #region - 荷卸現場別

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_GENBA";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "SHOBUN_NIOROSHI_GENBA_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        //searchConditionsDto = new SearchConditionsDto();
                        //searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        //searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        //searchConditionsDto.LeftColumn = "NIOROSHI_GENBA_KBN";
                        //searchConditionsDto.Value = "True";
                        //searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        //joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 荷卸現場別

                        break;
                    case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別

                        #region - 荷積現場別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_GENBA";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "HAISHUTSU_NIZUMI_GENBA_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        //searchConditionsDto = new SearchConditionsDto();
                        //searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        //searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        //searchConditionsDto.LeftColumn = "SAISHUU_SHOBUNJOU_KBN";
                        //searchConditionsDto.Value = "True";
                        //searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        //joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        //searchConditionsDto = new SearchConditionsDto();
                        //searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        //searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        //searchConditionsDto.LeftColumn = "NIOROSHI_GENBA_KBN";
                        //searchConditionsDto.Value = "True";
                        //searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        //joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 荷積現場別 -

                        break;
                    case SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu:  // 営業担当者別

                        #region - 営業担当者別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = false;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_SHAIN";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "EIGYOU_TANTOU_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 営業担当者別 -

                        break;
                    case SYUKEUKOMOKU_TYPE.UntenshaBetsu:       // 運転者別

                        #region - 運転者別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_SHAIN";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "UNTEN_KBN";
                        searchConditionsDto.Value = "True";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 運転者別 -

                        break;
                    case SYUKEUKOMOKU_TYPE.DenpyoKubunBetsu:    // 伝票区分別

                        #region - 伝票区分別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_DENPYOU_KBN";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "DENPYOU_KBN_CD";
                        searchConditionsDto.Value = "1";
                        searchConditionsDto.ValueColumnType = DB_TYPE.SMALLINT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "DENPYOU_KBN_CD";
                        searchConditionsDto.Value = "2";
                        searchConditionsDto.ValueColumnType = DB_TYPE.SMALLINT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 伝票区分別 -

                        break;
                    case SYUKEUKOMOKU_TYPE.DensyuKubunBetsu:    // 伝種区分別

                        #region - 伝種区分別 -

                        this.PopupWindowSetting = new Collection<JoinMethodDto>();
                        joinMethodDto = new JoinMethodDto();

                        joinMethodDto.IsCheckLeftTable = true;
                        joinMethodDto.IsCheckRightTable = false;
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_DENSHU_KBN";

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "DENSHU_KBN_CD";
                        searchConditionsDto.Value = "1";
                        searchConditionsDto.ValueColumnType = DB_TYPE.SMALLINT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "DENSHU_KBN_CD";
                        searchConditionsDto.Value = "2";
                        searchConditionsDto.ValueColumnType = DB_TYPE.SMALLINT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        searchConditionsDto = new SearchConditionsDto();
                        searchConditionsDto.And_Or = CONDITION_OPERATOR.OR;
                        searchConditionsDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchConditionsDto.LeftColumn = "DENSHU_KBN_CD";
                        searchConditionsDto.Value = "3";
                        searchConditionsDto.ValueColumnType = DB_TYPE.SMALLINT;
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);

                        this.PopupWindowSetting.Add(joinMethodDto);

                        #endregion - 伝種区分別 -

                        break;
                    default:

                        break;
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #endregion - Methods -
    }

    #endregion - SyuukeiKoumoku -
}
