// $Id: UIHeader.cs 15728 2014-02-07 07:17:55Z koga $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            #region 拠点選択で99:全社を除く＆不可
            this.KYOTEN_CD.popupWindowSetting = new System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>();

            this.KYOTEN_CD.popupWindowSetting.Add(new r_framework.Dto.JoinMethodDto()
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = "M_KYOTEN",
                SearchCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto>()
                {
                    new r_framework.Dto.SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.NOT_EQUALS,
                        LeftColumn = "KYOTEN_CD",
                        Value = "99",
                        ValueColumnType = r_framework.Const.DB_TYPE.IN_SMALLINT
                    },                    
                    new r_framework.Dto.SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.NOT_EQUALS,
                        LeftColumn = "TEKIYOU_FLG",
                        Value = "FALSE",
                        ValueColumnType = r_framework.Const.DB_TYPE.NONE
                    }                    
                }
            });

            this.KYOTEN_CD.FocusOutCheckMethod = new System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>();

            this.KYOTEN_CD.FocusOutCheckMethod.Add(new r_framework.Dto.SelectCheckDto()
            {
                CheckMethodName = "拠点マスタコードチェックandセッティング"
            });

            this.KYOTEN_CD.FocusOutCheckMethod.Add(new r_framework.Dto.SelectCheckDto()
            {
                CheckMethodName = "拠点マスタコード非存在チェック(存在する場合エラー)",
                DisplayMessage = "この拠点コードは使用できません。",
                RunCheckMethod = new System.Collections.ObjectModel.Collection<r_framework.Dto.SelectRunCheckDto>()
                {
                    new r_framework.Dto.SelectRunCheckDto()
                    {
                        CheckMethodName = "一致チェック",
                        SendParams = new string[]{"KYOTEN_CD"},
                        Condition =  new string[]{"99"}
                    }
                }

            });
            #endregion
        }
    }
}
