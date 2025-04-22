using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Data;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.Dto;


namespace Shougun.Core.ElectronicManifest.CustomControls_Ex
{
    public partial class CustomForMasterKyoutuuPopup_Ex : CustomAlphaNumTextBox
    {
        #region  Properties
        /**
         * EDISON 電子加入者プロパティ
         */
        [Category("EDISON 電子加入者プロパティ"), Description("加入者番号所属コントロール名")]
        public String EDI_MEMBER_ID_ControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("加入者番号が必須入力チェックフラグ")]
        public bool IsInputMustbeCheck { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("業者の場合は関連する現場CDの所属コントロール名")]
        public String LinkedGENBA_CD_ControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("事業場番号所属コントロール名")]
        public String JIGYOUJOU_CD_ControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("現場の場合は関連する業者CDの所属コントロール名")]
        public String LinkedGYOUSHA_CD_ControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("コード項目の連携情報コントロール、内容クリアを行うコントロール名を「,」区切りで入力してください。")]
        public String ClearLikedControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("コード項目の連携情報コントロール、内容クリアを行うコントロール名を「,」区切りで入力してください。")]
        public String ClearLikedControlNameForErr { get; set; }

        /**
         * EDISON 電子事業者区分プロパティ
         */
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：排出事業者区分")]
        public bool HST_KBN { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：運搬業者区分")]
        public bool UPN_KBN { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：処分事業者区分")]
        public bool SBN_KBN { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：報告不要区分")]
        public bool HOUKOKU_HUYOU_KBN { get; set; }

        /**
         * EDISON 電子現場プロパティ
         */
        [Category("EDISON 電子現場プロパティ"), Description("電子現場：事業者区分[1.排出事業者　2.運搬業者　3.処分業者]")]
        public string JIGYOUSHA_KBN { get; set; }
        [Category("EDISON 電子現場プロパティ"), Description("電子現場：事業場区分[1.排出事業場　2.運搬事業場　3.処分事業場]")]
        public string JIGYOUJOU_KBN { get; set; }

        /**
         * EDISON 電子担当者区分プロパティ
         */
        [Category("EDISON 電子担当者区分プロパティ"), Description("電子担当者区分[1.引渡　2.登録　3.運搬　4.報告　5.処分]")]
        public String TANTOUSHA_KBN { get; set; }

        /**
         * EDISON チェック済加入者番号プロパティ
         */
        [Browsable(false), Category("EDISON チェック済加入者番号プロパティ"), Description("チェック済加入者番号の値を設定と取得する")]
        public string CheckOK_KanyushaCD { get; set; }  

        #endregion  Properties  
        

       
        //===============================================================================================  

        // Constructor  
        public CustomForMasterKyoutuuPopup_Ex()
            : base()  
        {
            this.HST_KBN = false;
            this.UPN_KBN = false;
            this.SBN_KBN = false;
            this.HOUKOKU_HUYOU_KBN = false;

            this.IsInputMustbeCheck = false;

            this.CheckOK_KanyushaCD = string.Empty;

        }
        
        /// <summary>
        /// コントロールのFindメソッド
        /// </summary>
        /// <param name="target">コントロール名</param>
        /// <returns></returns>
        public Control FindControl(string target)
        {
            Control ctl = null;
            ctl = FindControl(this.Parent, target);
            if (ctl == null){
                if (this.Parent == null) return ctl;
                ctl = FindControl(this.Parent.Parent, target);
                if (ctl == null){
                    if (this.Parent.Parent == null) return ctl;
                    ctl = FindControl(this.Parent.Parent.Parent, target);
                    if (ctl == null){
                        if (this.Parent.Parent.Parent == null) return ctl;
                        ctl = FindControl(this.Parent.Parent.Parent.Parent, target);
                        if (ctl == null){
                            if (this.Parent.Parent.Parent.Parent == null) return ctl;
                            ctl = FindControl(this.Parent.Parent.Parent.Parent.Parent, target);
                            if (ctl == null) return ctl;
                        }
                    }
                }
            }
            return ctl;
        }

        /// <summary>
        /// Find Control
        /// </summary>
        /// <param name="root">root control</param>
        /// <param name="target">controlName</param>
        /// <returns></returns>
        static Control FindControl(Control root, string target)
        {
            if (root == null){
                return null;
            }
            if (root.Name.Equals(target))
                return root;
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                if (root.Controls[i].Name.Equals(target))
                    return root.Controls[i];
            }
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                Control result;
                for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
                {
                    result = FindControl(root.Controls[i].Controls[k], target);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }      
        #region  OverrideMethods  

        /// <summary>前回のフォーカス</summary>
        private string prevControlName = string.Empty;

        /// <summary>
        /// OnEnter Event
        /// 汎用的なコントロールであるため、前回値の他に前回フォーカスしたコントロールを保持する。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            this.prevControlName = this.Name;
            base.OnEnter(e);
        }

        /// <summary>
        /// PreviewKeyDownイベントのオウバーリード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
           
            //必須入力チェックフラグの判断
            if (IsInputMustbeCheck){
                if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName)){
                    Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                    if (ctl != null){
                        if (string.IsNullOrEmpty((ctl as TextBox).Text)){
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                //基本設定
                switch(this.PopupWindowId)
                {
                    case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                        this.PopupWindowName = "電マニデータ用検索ポップアップ";
                        break;

                    case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                        this.PopupWindowName = "電マニデータ複数キー用検索ポップアップ";
                        break;

                    default:
                        this.PopupWindowName = "マスタ共通ポップアップ";
                        break;
                }
                this.FocusOutCheckMethod.Clear();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
          
                DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

                //電子事業者検索の場合
                if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                {
                    dto.HST_KBN = (this.HST_KBN) ? "1" : null;
                    dto.UPN_KBN = (this.UPN_KBN) ? "1" : null;
                    dto.SBN_KBN = (this.SBN_KBN) ? "1" : null;
                    dto.HOUKOKU_HUYOU_KBN = (this.HOUKOKU_HUYOU_KBN) ? "1" : null;
                    if (dto.HST_KBN != null && dto.SBN_KBN!=null)
                    {
                        //OR条件判断
                        dto.JIGYOUSHA_KBN_OR = "1";
                        dto.HST_KBN = null;
                        dto.SBN_KBN = null;
                    }
                    DataTable dt = DsMasterLogic.GetDenshiGyoushaData(dto);
                    if (dt != null)
                    {
                        //検索画面のタイトルを設定
                        this.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "郵便番号", "都道府県", "住所", "電話番号" };
                        this.PopupGetMasterField = "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子事業者";
                    }
                }

                //電子事業場検索の場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUJOU)
                {
                    //加入者番号取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                            //else
                            //{
                            //    if(this.ReadOnly == false)
                            //    {
                            //        msgLogic.MessageBoxShow("E001", "業者CD");
                            //    }
                            //    return;
                            //}
                        }
                        //else
                        //{//業者CDコントロール見つかりません場合
                        //    if (this.ReadOnly == false)
                        //    {
                        //        msgLogic.MessageBoxShow("E001", "業者CD");
                        //    }
                        //    return;
                        //}
                    }

                    // 業者CDがあればセット
                    if (!string.IsNullOrEmpty(this.LinkedGYOUSHA_CD_ControlName))
                    {
                        Control gyoushaCtl = FindControl(this.LinkedGYOUSHA_CD_ControlName);
                        if (gyoushaCtl != null)
                        {
                            if (!string.IsNullOrEmpty((gyoushaCtl as TextBox).Text))
                            {
                                dto.GYOUSHA_CD = (gyoushaCtl as TextBox).Text;
                            }
                        }
                    }

                    //事業場区分
                    if (!string.IsNullOrEmpty(this.JIGYOUJOU_KBN)){
                        dto.JIGYOUJOU_KBN = this.JIGYOUJOU_KBN;
                    }
                    //排出事業者
                    if (this.HST_KBN != null && this.HST_KBN)
                    {
                        dto.HST_KBN = "1";
                    }
                    //収集運搬業者
                    if (this.UPN_KBN != null && this.UPN_KBN)
                    {
                        dto.UPN_KBN = "1";
                    }
                    //処分事業者
                    if (this.SBN_KBN != null && this.SBN_KBN)
                    {
                        dto.SBN_KBN = "1";
                    }
                    dto.ISNEED_SAME_GYOUSHA_FLG = true;
                    DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);
                    if (dt != null)
                    {
                        //検索画面のタイトルを設定  
                        this.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "事業場CD", "現場CD", "事業場名", "郵便番号", "都道府県", "住所", "電話番号" };
                        this.PopupGetMasterField = "GENBA_CD,JIGYOUJOU_NAME,JIGYOUJOU_POST,JIGYOUJOU_TEL,JIGYOUJOU_ADDRESS,JIGYOUJOU_CD,GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子事業場";

                        // 検索画面に表示する業者CDをセット(ダミーデータ)
                        var sendParam = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                        PopupSearchSendParamDto paramDto = new PopupSearchSendParamDto();
                        paramDto.KeyName = "GYOUSHA_CD";
                        paramDto.Value = dto.GYOUSHA_CD;
                        sendParam.Add(paramDto);
                        this.PopupSearchSendParams = sendParam;
                    }
                }
                //電子廃棄物種類検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_HAIKI_SHURUI)
                {
                    DataTable dt = DsMasterLogic.GetDenshiHaikiShuruiData(dto);
                    if (dt != null)
                    {
                        //検索画面のタイトルを設定
                        this.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
                        this.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子廃棄物種類";
                    }
                }
                //電子廃棄物名称検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_HAIKI_NAME)
                {
                    //加入者番号取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                        }
                    }
                    DataTable dt = DsMasterLogic.GetDenshiHaikiNameData(dto);
                    if (dt != null)
                    {
                        //検索画面のタイトルを設定
                        this.PopupDataHeaderTitle = new string[] { "廃棄物名称CD", "廃棄物名称" };
                        this.PopupGetMasterField = "HAIKI_NAME_CD,HAIKI_NAME";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子廃棄物名称";
                    }
                }
                //電子担当者検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_TANTOUSHA)
                {
                    //加入者番号の取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                            else
                            {
                                if (this.ReadOnly == false)
                                {
                                    msgLogic.MessageBoxShow("E001", "業者CD");
                                }
                                return;
                            }
                        }
                        else
                        {
                            if (this.ReadOnly == false)
                            {
                                msgLogic.MessageBoxShow("E001", "業者CD");
                            }
                            return;
                        }
                    }
                    //担当者区分の取得
                    if (!string.IsNullOrEmpty(this.TANTOUSHA_KBN))
                    {
                        dto.TANTOUSHA_KBN = this.TANTOUSHA_KBN;
                    }
                    DataTable dt = DsMasterLogic.GetTantoushaData(dto);
                    if (dt != null)
                    {
                        //検索画面のタイトルを設定
                        this.PopupDataHeaderTitle = new string[] { "担当者CD", "担当者名", "加入者番号" };
                        this.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME,EDI_MEMBER_ID";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子担当者";
                    }
                }
                //電子有害物質検索場合    
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_YUUGAI_BUSSHITSU)
                {
                    DataTable dt = new DataTable();
                    dt = DsMasterLogic.GetYougaibutujituData(dto);
                    if (dt != null)
                    {
                        //検索画面のタイトルを設定
                        this.PopupDataHeaderTitle = new string[] { "有害物質CD", "有害物質名称" };
                        this.PopupGetMasterField = "YUUGAI_BUSSHITSU_CD,YUUGAI_BUSSHITSU_NAME";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子有害物質";
                    }
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(e.KeyChar==(Char)Keys.Tab){
                return;
            }
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //加入者番号入力必須チェックの判断
            if (IsInputMustbeCheck){
                if ((this as TextBox) != null)
                {
                    //入力可の場合
                    if (this.ReadOnly == false)
                    {
                        if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                        {
                            Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                            if (ctl != null)
                            {
                                if (string.IsNullOrEmpty((ctl as TextBox).Text))
                                {
                                    msgLogic.MessageBoxShow("E001", "業者CD");
                                    e.Handled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            base.OnKeyPress(e);
        }

        /// <summary>
        /// TextChangedイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.ReadOnly)
            {
                return;
            }

            //if (!string.IsNullOrEmpty(this.Text))
            //{
            //    if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
            //    {
            //        //加入者番号初期化
            //        if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
            //        {
            //            Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
            //            if (ctl != null
            //                && !string.IsNullOrEmpty((ctl as TextBox).Text))
            //            {
            //                (ctl as TextBox).Text = string.Empty;
            //            }
            //        }
            //    }
            //}
        }

         /// <summary>
        /// Validatingイベントのオウバーリード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            //編集不可の場合は、チェックを行わない
            if(this.ReadOnly){
                return;
            }

            if (this.Name.Equals(this.prevControlName)
                && this.Text.Equals(this.prevText))
            {
                return;
            }

            base.OnValidating(e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //空白はチェックOKです
            if (!string.IsNullOrEmpty(this.Text))
            {
                if (this.PopupWindowId != WINDOW_ID.M_DENSHI_JIGYOUSHA && this.PopupWindowId != WINDOW_ID.M_DENSHI_JIGYOUJOU)
                {
                    if (this.prevText == this.Text)
                    {
                        return;
                    }
                }
                DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

                //電子事業者検索の場合
                if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                {
                    //事業者区分の設定
                    dto.HST_KBN = (this.HST_KBN) ? "1" : null;
                    dto.UPN_KBN = (this.UPN_KBN) ? "1" : null;
                    dto.SBN_KBN = (this.SBN_KBN) ? "1" : null;
                    dto.HOUKOKU_HUYOU_KBN = (this.HOUKOKU_HUYOU_KBN) ? "1" : null;
                    if (dto.HST_KBN != null && dto.SBN_KBN != null)
                    {
                        //OR条件判断
                        dto.JIGYOUSHA_KBN_OR = "1";
                        dto.HST_KBN = null;
                        dto.SBN_KBN = null;
                    }
                    //業者CDの設定
                    dto.GYOUSHA_CD = this.Text;

                    //加入者番号取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                //加入者番号の設定
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                        }
                    }

                    DataTable dt = DsMasterLogic.GetDenshiGyoushaData(dto);

                    this.SetControlTextValue(ref e, dt, "電子事業者");
                }

                //電子事業場検索の場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUJOU)
                {
                    //加入者番号取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                //加入者番号の設定
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                        }
                    }

                    // 現場情報セット
                    if (!string.IsNullOrEmpty(this.JIGYOUJOU_CD_ControlName))
                    {
                        Control jigyoujouCdCtl = FindControl(this.JIGYOUJOU_CD_ControlName);
                        if (jigyoujouCdCtl != null)
                        {
                            if (!string.IsNullOrEmpty((jigyoujouCdCtl as TextBox).Text))
                            {
                                //事業場番号の設定
                                dto.JIGYOUJOU_CD = (jigyoujouCdCtl as TextBox).Text;
                            }
                        }
                    }

                    // 事業場番号が無い場合は業者CD + 現場CDで検索する
                    string gyoushaNm = string.Empty;
                    if (!string.IsNullOrEmpty(this.LinkedGYOUSHA_CD_ControlName))
                    {
                        Control gyoushaCdCtl = FindControl(this.LinkedGYOUSHA_CD_ControlName);
                        if (gyoushaCdCtl != null)
                        {
                            gyoushaNm = (gyoushaCdCtl as CustomAlphaNumTextBox).DisplayItemName;
                            if (!string.IsNullOrEmpty((gyoushaCdCtl as TextBox).Text))
                            {
                                //業者CDの設定
                                dto.GYOUSHA_CD = (gyoushaCdCtl as TextBox).Text;
                            }
                        }
                    }

                    dto.GENBA_CD = this.Text;

                    // 20160107 BUNN #12111 取引先業者現場の親子関係 STR
                    if (string.IsNullOrEmpty(dto.GYOUSHA_CD) && !string.IsNullOrEmpty(dto.GENBA_CD))
                    {
                        msgLogic.MessageBoxShow("E051", (string.IsNullOrEmpty(gyoushaNm)) ? "電子事業者" : gyoushaNm);
                        e.Cancel = true;
                        this.Text = string.Empty;
                        return;
                    }
                    // 20160107 BUNN #12111 取引先業者現場の親子関係 END

                    dto.JIGYOUJOU_KBN = this.JIGYOUJOU_KBN;
                    dto.ISNEED_SAME_GYOUSHA_FLG = true;
                    DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);

                    this.SetControlTextValue(ref e, dt, "電子事業場");

                }
                //電子廃棄物種類検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_HAIKI_SHURUI)
                {
                    //検索パラメータの電子廃棄物種類を設定
                    dto.HAIKI_SHURUI_CD = this.Text;
                    //検索実行
                    DataTable dt = DsMasterLogic.GetDenshiHaikiShuruiData(dto);
                    this.SetControlTextValue(ref e, dt, "電子廃棄物種類");

                }
                //電子廃棄物名称検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_HAIKI_NAME)
                {
                    //加入者番号取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                        }
                    }
                    //検索パラメータの電子廃棄物名称を設定
                    dto.HAIKI_NAME_CD = this.Text;
                    //検索実行
                    DataTable dt = DsMasterLogic.GetDenshiHaikiNameData(dto);
                    this.SetControlTextValue(ref e, dt, "電子廃棄物名称");
                }
                //電子担当者検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_TANTOUSHA)
                {
                    //加入者番号の取得
                    if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                    {
                        Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                        }
                    }
                    //担当者区分の取得
                    if (!string.IsNullOrEmpty(this.TANTOUSHA_KBN))
                    {
                        dto.TANTOUSHA_KBN = this.TANTOUSHA_KBN;
                    }
                    //検索パラメータの担当者CDを設定
                    dto.TANTOUSHA_CD = this.Text;
                    //検索実行
                    DataTable dt = DsMasterLogic.GetTantoushaData(dto);
                    //画面に反映する
                    this.SetControlTextValue(ref e, dt, "電子担当者");
                }
                //電子有害物質検索場合
                else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_YUUGAI_BUSSHITSU)
                {
                    //検索パラメータの電子有害物質を設定
                    dto.YUUGAI_BUSSHITSU_CD = this.Text;
                    //検索実行
                    DataTable dt = DsMasterLogic.GetYougaibutujituData(dto);
                    this.SetControlTextValue(ref e, dt, "電子有害物質");
                }
            }
            //空白はOKで、全てクリアする
            else
            {
                this.ClearLikedControlText();
                //関連現場CD情報が設定されている場合、該当情報をクリアする
                if (!string.IsNullOrEmpty(this.LinkedGENBA_CD_ControlName))
                {
                    Control ctl = FindControl(this.LinkedGENBA_CD_ControlName);
                    if (ctl != null && (ctl as CustomForMasterKyoutuuPopup_Ex)!=null)
                    {
                        (ctl as CustomForMasterKyoutuuPopup_Ex).Text = string.Empty;
                        (ctl as CustomForMasterKyoutuuPopup_Ex).ClearLikedControlText();
                    }
                }
            }
            //BackColor設定
            if (e.Cancel)
            {
                this.IsInputErrorOccured = true;
            }
        }
        



        #endregion  OverrideMethods  

        #region Privateのメソッド
        /// <summary>
        /// 前頭設定するリンクされたコントロール内容をクリアする
        /// </summary>
        public void ClearLikedControlText()
        {
            //前頭設定内容をクリアする
            if (this.ClearLikedControlName != null)
            {
                string[] ctls = this.ClearLikedControlName.Split(',');
                for (int i = 0; i < ctls.Length; i++)
                {
                    Control ctl = FindControl(ctls[i]);
                    if (ctl != null){
                        if (!ctl.Equals(this)){
                            (ctl as TextBox).Text = string.Empty;
                        }
                    }
                }
                this.SelectAll();
            }
        }
        /// <summary>
        /// 前頭設定するリンクされたコントロール内容をクリアする
        /// </summary>
        public void ClearLikedControlTextForEr()
        {
            //前頭設定内容をクリアする
            if (this.ClearLikedControlNameForErr != null)
            {
                string[] ctls = this.ClearLikedControlNameForErr.Split(',');
                for (int i = 0; i < ctls.Length; i++)
                {
                    Control ctl = FindControl(ctls[i]);
                    if (ctl != null)
                    {
                        if (!ctl.Equals(this))
                        {
                            (ctl as TextBox).Text = string.Empty;
                        }
                    }
                }
                this.SelectAll();
            }
        }
        /// <summary>
        /// 前頭設定するリンクされたコントロールをReadOnlyに設定する
        /// </summary>
        public void SetLikedControlsReadOnlyStatus(bool IsReadOnly)
        {
            string[] ctls = this.SetFormField.Split(',');
            for (int i = 0; i < ctls.Length; i++)
            {
                Control ctl = FindControl(ctls[i]);
                if (ctl != null){
                    if (!ctl.Equals(this)){
                       (ctl as TextBox).ReadOnly = IsReadOnly; 
                    }
                }
            }
        }
        /// <summary>
        /// LostFocus時にチェックOK場合はコントロールのテキスト値を設定
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CodeName"></param>
        private void SetControlTextValue(ref CancelEventArgs e,DataTable dt, string CodeName)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E020", CodeName);
                    //リンクされたControl内容をクリアする
                    this.ClearLikedControlTextForEr();
                    e.Cancel = true;
                }
                else if (dt.Rows.Count > 1)
                {
                    // リンクされた内容を一度初期化
                    this.ClearLikedControlText();

                    // 複数ヒットする場合はヒットした情報を絞り込んでポップアップを表示する。
                    #region 複数ヒット
                    if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                    {
                        #region 電子事業者
                        // 検索ポップアップの設定
                        this.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "郵便番号", "都道府県", "住所", "電話番号" };
                        this.PopupGetMasterField = "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";
                        this.PopupWindowName = "電マニデータ用検索ポップアップ";
                        InitializeSearchDataTable(ref dt);
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子事業者";

                        // popup表示
                        CustomControlExtLogic.PopUp((ICustomControl)this);

                        // 加入者番号チェック(ポップアップで何も選択されなかった場合)
                        if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                        {
                            Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                            if (ctl != null
                                && string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                e.Cancel = true;
                            }
                        }

                        //電子事業者チェックOK場合
                        if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                        {
                            string sKanyusha = this.GetKaNyuShaNo();
                            if (sKanyusha != null)
                            {
                                //関連現場CDコントロールのインスタンスを取得
                                if (!string.IsNullOrEmpty(this.LinkedGENBA_CD_ControlName))
                                {
                                    Control ctl = FindControl(this.LinkedGENBA_CD_ControlName);
                                    if (ctl != null && (ctl as CustomForMasterKyoutuuPopup_Ex) != null)
                                    {
                                        CustomForMasterKyoutuuPopup_Ex ctl_Ex = (CustomForMasterKyoutuuPopup_Ex)ctl;
                                        if (ctl_Ex.CheckOK_KanyushaCD != sKanyusha)
                                        {
                                            ctl_Ex.Text = string.Empty;
                                            ctl_Ex.ClearLikedControlText();
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUJOU)
                    {
                        #region 電子事業場
                        // 検索ポップアップの設定
                        this.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "事業場CD", "現場CD", "事業場名", "郵便番号", "都道府県", "住所", "電話番号" };
                        this.PopupGetMasterField = "GENBA_CD,JIGYOUJOU_NAME,JIGYOUJOU_POST,JIGYOUJOU_TEL,JIGYOUJOU_ADDRESS,JIGYOUJOU_CD,GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";
                        this.PopupWindowName = "電マニデータ複数キー用検索ポップアップ";
                        InitializeSearchDataTable(ref dt);
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子事業場";

                        // popup表示
                        CustomControlExtLogic.PopUp((ICustomControl)this);

                        // 加入者番号チェック(ポップアップで何も選択されなかった場合)
                        if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName))
                        {
                            Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                            if (ctl != null)
                            {
                                if (string.IsNullOrEmpty((ctl as TextBox).Text))
                                {
                                    e.Cancel = true;
                                }
                            }
                        }

                        // 事業場番号チェック(ポップアップで何も選択されなかった場合)
                        if (!string.IsNullOrEmpty(this.JIGYOUJOU_CD_ControlName))
                        {
                            Control jigyoujouCdCtl = FindControl(this.JIGYOUJOU_CD_ControlName);
                            if (jigyoujouCdCtl != null)
                            {
                                if (string.IsNullOrEmpty((jigyoujouCdCtl as TextBox).Text))
                                {
                                    e.Cancel = true;
                                }
                            }
                        }

                        //電子事業現場チェックOK場合
                        if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUJOU)
                        {
                            string sKanyusha = this.GetKaNyuShaNo();
                            this.CheckOK_KanyushaCD = sKanyusha;
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {//一件正常内容を設定
                    string[] ctls = this.SetFormField.Split(',');
                    string[] colName = this.GetCodeMasterField.Split(',');
                    //取得カラム数量が設定したいコントロール数量不整合の場合、メッセージを出す
                    if (ctls.Length > colName.Length)
                    {
                        msgLogic.MessageBoxShow("E084", "リンクされたコントロール");
                        e.Cancel = false;
                        return;
                    }
                    //チェックOKしたら取得されたデータをコントロールに設定する
                    for (int i = 0; i < ctls.Length; i++)
                    {
                        Control ctl = FindControl(ctls[i]);
                        if (ctl != null){
                            (ctl as TextBox).Text = (DBNull.Value == dt.Rows[0][colName[i]]) ? 
                                string.Empty : (string)dt.Rows[0][colName[i]];
                        }
                    }
                    //電子事業者チェックOK場合
                    if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                    {
                        string sKanyusha = this.GetKaNyuShaNo();
                        if (sKanyusha!=null)
                        {
                            //関連現場CDコントロールのインスタンスを取得
                            if (!string.IsNullOrEmpty(this.LinkedGENBA_CD_ControlName))
                            {
                                Control ctl = FindControl(this.LinkedGENBA_CD_ControlName);
                                if (ctl != null&& (ctl as CustomForMasterKyoutuuPopup_Ex)!=null)
                                {
                                   CustomForMasterKyoutuuPopup_Ex ctl_Ex = (CustomForMasterKyoutuuPopup_Ex)ctl;
                                   if (ctl_Ex.CheckOK_KanyushaCD != sKanyusha){
                                       ctl_Ex.Text = string.Empty;
                                       ctl_Ex.ClearLikedControlText();
                                   }
                                }
                            }
                        }
                    }

                    //電子事業現場チェックOK場合
                    if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUJOU)
                    {
                        string sKanyusha = this.GetKaNyuShaNo();
                        this.CheckOK_KanyushaCD = sKanyusha;
                    }
                    
                }
            }
        }
        /// <summary>
        /// 加入者番号取得処理
        /// </summary>
        /// <returns></returns>
        public string GetKaNyuShaNo()
        {
            string sRet = null;
            //加入者番号を取得
            if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName)){
                Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                if (ctl != null){
                    if (!string.IsNullOrEmpty((ctl as TextBox).Text)){
                        //チェックOKの加入者番号の退避
                        sRet = (ctl as TextBox).Text;
                    }
                }
            }
            return sRet;
        }

        /// <summary>
        /// システムエラー防止のため、一行に追加
        /// </summary>
        /// <param name="dtSearch"></param>
        private void InitializeSearchDataTable(ref DataTable dtSearch)
        {
            if (dtSearch != null && dtSearch.Rows.Count == 0)
            {
                for (int i = 0; i < dtSearch.Columns.Count; i++)
                {
                    dtSearch.Columns[i].AllowDBNull = true;
                }
                dtSearch.Rows.Add();
            }
        }
        #endregion Privateのメソッド




    }  
}  

