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
using Shougun.Core.ElectronicManifest.SyobunnShuryouHoukokuIkkatuNyuuryoku;


namespace Shougun.Core.ElectronicManifest.CustomControls_Ex
{
    public partial class CustomForMasterKyoutuuPopup_Ex : CustomAlphaNumTextBox
    {
        #region  Properties  
        [Category("EDISON 電子加入者プロパティ"), Description("加入者番号所属コントロール名")]
        public String EDI_MEMBER_ID_ControlName { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("加入者番号が必須入力チェックフラグ")]
        public bool IsInputMustbeCheck { get; set; }
        [Category("EDISON 電子加入者プロパティ"), Description("業者場合は関連現場CD所属コントロール名")]
        public String LinkedGENBA_CD_ControlName { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：排出事業者区分")]
        public bool HST_KBN { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：運搬業者区分")]
        public bool UPN_KBN { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：処分事業者区分")]
        public bool SBN_KBN { get; set; }
        [Category("EDISON 電子事業者区分プロパティ"), Description("電子業者：報告不要区分")]
        public bool HOUKOKU_HUYOU_KBN { get; set; }

        [Category("EDISON 電子現場プロパティ"), Description("電子現場：事業者区分[1.排出事業者　2.運搬業者　3.処分業者]")]
        public string JIGYOUSHA_KBN { get; set; }
        [Category("EDISON 電子現場プロパティ"), Description("電子現場：事業場区分[1.排出事業場　2.運搬事業場　3.処分事業場]")]
        public string JIGYOUJOU_KBN { get; set; }

        [Category("EDISON 電子担当者区分プロパティ"), Description("電子担当者区分[1.引渡　2.登録　3.運搬　4.報告　5.処分]")]
        public String TANTOUSHA_KBN { get; set; }  
      
        #endregion  Properties  
        
        private string _CheckOK_KanyushaCD;
        public string CheckOK_KanyushaCD
        {
            get{return _CheckOK_KanyushaCD;}
            set
            {
                if (_CheckOK_KanyushaCD!=value)
                {
                    _CheckOK_KanyushaCD = value;
                }
            }
        }
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
        /// 事業者コントロールのFind
        /// </summary>
        /// <param name="target">事業者CDコントロール名</param>
        /// <returns></returns>
        private Control FindControl(string target)
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
                this.PopupWindowName = "マスタ共通ポップアップ";
                this.FocusOutCheckMethod.Clear();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
          
                DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

                //電子事業者検索の場合
                if (this.PopupWindowId == WINDOW_ID.M_DENSHI_JIGYOUSHA)
                {
                    this.PopupWindowName = "電マニデータ用検索ポップアップ";

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
                        this.PopupDataHeaderTitle = new string[] { "業者CD", "事業者名", "加入者番号", "郵便番号", "電話番号", "都道府県", "住所" };
                        this.PopupGetMasterField = "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";
                        this.PopupDataSource = dt;
                        this.PopupDataSource.TableName = "電子事業者";
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
                        this.PopupDataSource = dt;
                        switch (this.TANTOUSHA_KBN) { 
                            case "3":
                                this.PopupDataHeaderTitle = new string[] { "運搬担当者CD", "運搬担当者名" };
                                this.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
                                this.PopupDataSource.TableName = "運搬担当者検索";
                                break;
                            case "4":
                                this.PopupDataHeaderTitle = new string[] { "報告担当者CD", "報告担当者名" };
                                this.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
                                this.PopupDataSource.TableName = "報告担当者検索";
                                break;
                            case "5":
                                this.PopupDataHeaderTitle = new string[] { "処分担当者CD", "処分担当者名" };
                                this.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
                                this.PopupDataSource.TableName = "処分担当者検索";
                                break;
                        }
                    }
                }
                ////電子有害物質検索場合    
                //else if (this.PopupWindowId == WINDOW_ID.M_DENSHI_YUUGAI_BUSSHITSU)
                //{
                //    DataTable dt = new DataTable();
                //    dt = DsMasterLogic.GetYougaibutujituData(dto);
                //    if (dt != null)
                //    {
                //        //検索画面のタイトルを設定
                //        this.PopupDataHeaderTitle = new string[] { "有害物質CD", "有害物質名称" };
                //        this.PopupGetMasterField = "YUUGAI_BUSSHITSU_CD,YUUGAI_BUSSHITSU_NAME";
                //        this.PopupDataSource = dt;
                //        this.PopupDataSource.TableName = "電子有害物質";
                //    }
                //}
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
                if (!string.IsNullOrEmpty(this.EDI_MEMBER_ID_ControlName)){
                    Control ctl = FindControl(this.EDI_MEMBER_ID_ControlName);
                    if (ctl != null){
                        if (string.IsNullOrEmpty((ctl as TextBox).Text)){
                            msgLogic.MessageBoxShow("E001","業者CD");
                            e.Handled = true;
                            //(ctl as TextBox).Focus();
                            return;
                        }
                    }
                }
            }
            base.OnKeyPress(e);
        }
         /// <summary>
        /// Validatingイベントのオウバーリード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //空白はチェックOKです
            if (!string.IsNullOrEmpty(this.Text))
            {
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
                    dto.GENBA_CD = this.Text;
                    dto.JIGYOUJOU_KBN = this.JIGYOUJOU_KBN;
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
            if (this.SetFormField!=null)
            {
                string[] ctls = this.SetFormField.Split(',');
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

            var UForm = this.Parent as UIForm;
            UForm.FocusOutErrorFlag = false;

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E020", CodeName);
                    //リンクされたControl内容をクリアする
                    this.ClearLikedControlText();
                    e.Cancel = true;
                    UForm.FocusOutErrorFlag = true;
                }
                else if (dt.Rows.Count > 1)
                {
                    msgLogic.MessageBoxShow("E031", CodeName);
                    //リンクされたControl内容をクリアする
                    this.ClearLikedControlText();
                    e.Cancel = true;
                    UForm.FocusOutErrorFlag = true;
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

        private string GetKaNyuShaNo()
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
        #endregion Privateのメソッド




    }  
}  

