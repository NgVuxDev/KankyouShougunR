using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;
using r_framework.Utility;
using System.Data;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.Logic;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using r_framework.Dto;
using System.ComponentModel;
using Shougun.Core.Common.BusinessCommon.Dao;
using r_framework.Dao;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    public partial class CustomPopupDataGridView : CustomDataGridView
    {
        #region  Properties
        [Category("EDISONプロパティ_フォーカスセット"), Description("前フォーカスコントロール名")]
        public String BeforeFocusControlName { get; set; }

        [Category("EDISONプロパティ_フォーカスセット"), Description("後フォーカスコントロール名")]
        public String AfterFocusControlName { get; set; }

        [Category("EDISONプロパティ_フォーカスセット"), Description("補足前フォーカスコントロール名")]
        public String SecondBeforeFocusControlName { get; set; }

        [Category("EDISONプロパティ_フォーカスセット"), Description("補足後フォーカスコントロール名")]
        public String SecondAfterFocusControlName { get; set; }
        #endregion  Properties  

        /// <summary>
        /// セルの前回値を保存するディクショナリ
        /// <para>Key:セル名 Value:セルの値</para>
        /// </summary>
        private Dictionary<string, string> prevCellValueDictionary = new Dictionary<string, string>();

        /// <summary>
        /// 不正な入力をされたかを示します
        /// エラー値を入力しエラーポップアップでOKボタンを押して項目にEnterされた場合に
        /// エラー値が前回値として保持されるため、エラーが発生したかどうかのチェックも行うためフラグを用意する。
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// 画面のインスタンス
        /// </summary>
        public UIForm Myform {get ; set;}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomPopupDataGridView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public CustomPopupDataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// メッセージ表示
        /// </summary>
        MessageBoxShowLogic msgLogic;

        // CustomDateGridView_KeyUpをoverride
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            //Delete、Up、Down、TabとEnterキーの例外処理
            switch (e.KeyData)
            {
                case Keys.Delete:
                case Keys.Up:
                case Keys.Down:
                    return base.ProcessDataGridViewKey(e);

                case Keys.Tab:
                case Keys.Enter:
                    return this.ProcessDataGridViewKeyTabEnter(e);
                default:
                    break;
            }

            //Shiftキーの例外処理
            if (e.Modifiers == Keys.Shift)
            {
                switch (e.KeyData & Keys.KeyCode)
                {
                    case Keys.Tab:
                    case Keys.Enter:
                        return this.ProcessDataGridViewKeyTabEnter(e);
                    default:
                        return base.ProcessDataGridViewKey(e);
                }
            }

            string whereSql = string.Empty;
            msgLogic = new MessageBoxShowLogic();

            // 継承元のイベントは削除
            //base.CustomDateGridView_KeyUp(sender, e);
            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //検索結果
            DataTable dtSearch = new DataTable();

            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //編集不可
            if (this.CurrentCell != null && this.Columns[this.CurrentCell.ColumnIndex].ReadOnly) return false;

            if (cell != null)
            {
                //数量確定者場合は設定する
                if ("SUU_KAKUTEI_CODE".Equals(cell.GetName()))
                {
                    //数量確定者
                    cell.PopupWindowName = "マスタ共通ポップアップ";
                    cell.FocusOutCheckMethod.Clear();

                    //DataTableの作成
                    dtSearch = new DataTable();
                    dtSearch.Columns.Add("CONFIRMER_CD", typeof(string));
                    dtSearch.Columns.Add("CONFIRMER_NAME", typeof(string));

                    dtSearch.Rows.Add(new object[] { "01", "排出事業者" });
                    dtSearch.Rows.Add(new object[] { "02", "処分業者" });
                    dtSearch.Rows.Add(new object[] { "03", "収集運搬業者（区間1）" });
                    dtSearch.Rows.Add(new object[] { "04", "収集運搬業者（区間2）" });
                    dtSearch.Rows.Add(new object[] { "05", "収集運搬業者（区間3）" });
                    dtSearch.Rows.Add(new object[] { "06", "収集運搬業者（区間4）" });
                    dtSearch.Rows.Add(new object[] { "07", "収集運搬業者（区間5）" });

                    //データが存在する場合
                    if (dtSearch != null && dtSearch.Rows.Count > 0)
                    {
                        //検索画面のタイトルを設定
                        cell.PopupDataHeaderTitle = new string[] { "数量確定者CD", "数量確定者名称" };
                        cell.PopupGetMasterField = "CONFIRMER_CD,CONFIRMER_NAME";
                        cell.PopupDataSource = dtSearch;
                        cell.PopupDataSource.TableName = "電子数量確定者";

                        DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();

                        if ("SUU_KAKUTEI_CODE".Equals(cell.GetName()))
                        {
                            cell2 = this.Rows[rowIndex].Cells["SUU_KAKUTEI_NAME"]
                                as DgvCustomTextBoxCell;
                        }

                        if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                            ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                        //値設定先コントロールを設定する
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                    }
                }

                //加入者番号取得
                Control ctl = FindControl(ConstCls.CTXT_HAISYUTU_KANYUSHANO);

                // 呼び出し機能ごとにPopupGetMasterFieldを設定
                switch (cell.PopupWindowId)
                {
                    case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                        //電子廃棄種類
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();


                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                            else
                            {
                                //加入者番号が空白の場合
                                dto.EDI_MEMBER_ID = string.Empty;
                            }
                        }

                        dtSearch = DsMasterLogic.GetDenshiHaikiShuruiData(dto);

                        //データが存在する場合
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名", "報告書分類CD", "報告書分類名" };
                            cell.PopupGetMasterField = "HAIKISHURUICD,HAIKI_SHURUI_NAME,HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME";
                            
                            // 廃棄物種類CD昇順でソート
                            var view = new DataView(dtSearch);
                            view.Sort = "HAIKISHURUICD";
                            var table = view.ToTable();
                            cell.PopupDataSource = table;
                            cell.PopupDataSource.TableName = "電子廃棄物種類";
                            
                            DgvCustomTextBoxCell cell2= new DgvCustomTextBoxCell();

                            if ("HAIKI_SHURUI_CD".Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["HAIKI_SHURUI_NAME"]
                                    as DgvCustomTextBoxCell;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = 
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;

                    case WINDOW_ID.M_DENSHI_HAIKI_NAME:
                        //電子廃棄名称
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();


                        if (ctl != null)
                        {
                            if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                            {
                                dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                            }
                            else
                            {
                                //加入者番号が空白の場合
                                msgLogic.MessageBoxShow("E012", "排出事業者CD");
                                //フォーカスを設定する
                                ctl.Focus();
                                return false;
                            }
                        }

                        dtSearch = DsMasterLogic.GetDenshiHaikiNameData(dto);
                        InitializeSearchDataTable(ref dtSearch);
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "廃棄物名称CD", "廃棄物名称" };
                            cell.PopupGetMasterField = "HAIKI_NAME_CD,HAIKI_NAME";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "電子廃棄物名称";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells["HAIKI_NAME"] 
                                as DgvCustomTextBoxCell;

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = 
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                            
                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;

                    case WINDOW_ID.M_UNIT:

                        if (!"FM_HAIKI_UNIT_CD".Equals(cell.GetName()))
                        {
                            break;
                        }

                        //単位共通
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        //共通クラス
                        IM_UNITDao unitdao = DaoInitUtility.GetComponent<IM_UNITDao>();

                        whereSql = string.Empty;
                     
                        dtSearch = unitdao.GetAllMasterDataForPopup(whereSql);

                        //データが存在する場合
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "単位CD", "単位名" };
                            cell.PopupGetMasterField = "CD,NAME";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "単位";

                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();

                            if ("FM_HAIKI_UNIT_CD".Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["FM_UNIT_NAME_RYAKU"]
                                    as DgvCustomTextBoxCell;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;

                    case WINDOW_ID.M_NISUGATA:

                        //荷姿共通
                        break;

                    case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                        //電子事業者
                        //基本設定
                        cell.PopupWindowName = "電マニデータ用検索ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        if (("LAST_SBN_GYOUSHA_CD").Equals(cell.GetName())) dto.SBN_KBN = "1";
                        if (("LAST_SBN_GYOUSHA_JISEKI_CD").Equals(cell.GetName())) dto.SBN_KBN = "1";
                        if (("UPN_SHA_CD").Equals(cell.GetName())) dto.UPN_KBN = "1";
                        if (("UNPANSAKI_GYOUSHA_CD").Equals(cell.GetName()))
                        {
                            dto.UPN_KBN = "1";
                            dto.SBN_KBN = "1";
                        }

                        dtSearch = DsMasterLogic.GetDenshiGyoushaData(dto);
                        InitializeSearchDataTable(ref dtSearch);
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "郵便番号", "都道府県", "住所", "電話番号" };

                            cell.PopupGetMasterField = 
                                "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";

                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "電子事業者";

                            //名称
                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();
                            //加入者番号「非表示列」
                            DgvCustomTextBoxCell cell3 = new DgvCustomTextBoxCell();

                            if (("LAST_SBN_GYOUSHA_CD").Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_NAME"] as DgvCustomTextBoxCell;
                                //加入者番号
                                cell3 = this.Rows[rowIndex].Cells["Sbn_KanyushaCD"] as DgvCustomTextBoxCell;
                               
                            }
                            else if (("LAST_SBN_GYOUSHA_JISEKI_CD").Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"] as DgvCustomTextBoxCell;
                                //加入者番号
                                cell3 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"] as DgvCustomTextBoxCell;
                            }
                            else if (("UPN_SHA_CD").Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["UPN_SHA_NAME"] as DgvCustomTextBoxCell;
                                //加入者番号
                                cell3 = this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"] as DgvCustomTextBoxCell;
                            }
                            else if (("UNPANSAKI_GYOUSHA_CD").Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"] as DgvCustomTextBoxCell;
                                //加入者番号Unpansaki_KanyushaCD
                                cell3 = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"] as DgvCustomTextBoxCell;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = 
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 ,cell3 };
                        }

                        break;

                    case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                        //(電子)事業場

                        //基本設定
                        cell.PopupWindowName = "電マニデータ複数キー用検索ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        if (((System.Windows.Forms.DataGridViewCell)(cell)).OwningColumn.Name.Equals("LAST_SBN_JOU_CD"))
                        {
                            if (string.IsNullOrEmpty(this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value == null ?
                                string.Empty : this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value.ToString()))
                            {
                                ////加入者番号が空白の場合
                                //msgLogic.MessageBoxShow("E012", "最終処分事業者CD");
                                ////フォーカスを設定する
                                //e.Handled = true;
                                //return false;
                                dto.JIGYOUJOU_KBN = "3";
                            }
                            else
                            {
                                dto.EDI_MEMBER_ID = this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value.ToString();
                                dto.JIGYOUJOU_KBN = "3";
                            }

                            if (this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_CD"].Value != null)
                            {
                                var sendParam = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                                PopupSearchSendParamDto paramDto = new PopupSearchSendParamDto();
                                paramDto.KeyName = "GYOUSHA_CD";
                                paramDto.Value = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_CD"].Value.ToString();
                                sendParam.Add(paramDto);
                                cell.PopupSearchSendParams = sendParam;
                            }
                            dto.SBN_KBN = "1";
                        }
                       
                        if (((System.Windows.Forms.DataGridViewCell)(cell)).OwningColumn.Name.Equals(
                            "LAST_SBN_JOU_JISEKI_CD"))
                        {

                            if (string.IsNullOrEmpty(this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value 
                                == null ? string.Empty : this.Rows[rowIndex].Cells[
                                "LAST_SBN_GYOUSHA_KanyushaCD"].Value.ToString()))
                            {
                                ////加入者番号が空白の場合
                                //msgLogic.MessageBoxShow("E012", "最終処分事業者CD");
                                ////フォーカスを設定する
                                //e.Handled = true;
                                //return false;
                                dto.JIGYOUJOU_KBN = "3";
                            }
                            else
                            {
                                dto.EDI_MEMBER_ID = this.Rows[rowIndex].Cells[
                                    "LAST_SBN_GYOUSHA_KanyushaCD"].Value.ToString();
                                dto.JIGYOUJOU_KBN = "3";
                            }

                            if (this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].Value != null)
                            {
                                var sendParam = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                                PopupSearchSendParamDto paramDto = new PopupSearchSendParamDto();
                                paramDto.KeyName = "GYOUSHA_CD";
                                paramDto.Value = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].Value.ToString();
                                sendParam.Add(paramDto);
                                cell.PopupSearchSendParams = sendParam;
                            }
                            dto.SBN_KBN = "1";
                        }

                        if (((System.Windows.Forms.DataGridViewCell)(cell)).OwningColumn.Name.Equals(
                            "UNPANSAKI_GENBA_CD"))
                        {
                            // ポップアップ選択後、Enterイベントが発生し、前回値チェックで不都合が発生するため
                            // ポップアップ選択が発生したかどうかのフラグを制御する処理を追加。
                            cell.PopupAfterExecute = this.Myform.PopupAfter_UNPANSAKI_GENBA_CD;

                            if (!string.IsNullOrEmpty(this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value == null ?
                                string.Empty : this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value.ToString()))
                            {
                                dto.EDI_MEMBER_ID = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value.ToString();
                            }

                            if (!string.IsNullOrEmpty(this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"].Value == null ?
                                string.Empty : this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"].Value.ToString()))
                            {
                                dto.GYOUSHA_CD = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"].Value.ToString();
                            }
                            //else
                            //{
                            //    //業者CDが空白の場合
                            //    msgLogic.MessageBoxShow("E012", "運搬先事業者CD");
                            //    //フォーカスを設定する
                            //    e.Handled = true;
                            //    return false;
                            //}

                            dto.JIGYOUJOU_KBN_LIST = new List<string>();
                            dto.JIGYOUJOU_KBN_LIST.Add("2");
                            dto.JIGYOUJOU_KBN_LIST.Add("3");
                            dto.JIGYOUSHA_KBN_LIST = new List<string>();
                            dto.JIGYOUSHA_KBN_LIST.Add("2");
                            dto.JIGYOUSHA_KBN_LIST.Add("3");

                            if (this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"].Value != null)
                            {
                                var sendParam = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                                PopupSearchSendParamDto paramDto = new PopupSearchSendParamDto();
                                paramDto.KeyName = "GYOUSHA_CD";
                                paramDto.Value = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"].Value.ToString();
                                sendParam.Add(paramDto);
                                cell.PopupSearchSendParams = sendParam;
                            }
                            dto.UPN_KBN = "1";
                            dto.SBN_KBN = "1";
                        }

                        //検索を行う
                        dtSearch = DsMasterLogic.GetDenshiGenbaData(dto);
                        InitializeSearchDataTable(ref dtSearch);//システムエラー回避ため
                        if (dtSearch != null && dtSearch.Rows.Count == 0)
                        {
                            for (int i = 0; i < dtSearch.Columns.Count; i++)
                            {
                                dtSearch.Columns[i].AllowDBNull = true;
                            }
                            dtSearch.Rows.Add();
                        }
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "事業場CD", "現場CD", "事業場名", "郵便番号", "都道府県", "住所", "電話番号" };

                            cell.PopupGetMasterField =
                                "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,GENBA_CD,JIGYOUJOU_NAME,JIGYOUJOU_POST,JIGYOUJOU_TEL,JIGYOUJOU_ADDRESS,JIGYOUJOU_CD";

                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "電子事業場";

                            //加入者番号(非表示列)
                            DgvCustomTextBoxCell cell1 = new DgvCustomTextBoxCell();
                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell(); 
                            //加入者番号「非表示列」
                            DgvCustomTextBoxCell cell3 = new DgvCustomTextBoxCell();
                            DgvCustomTextBoxCell cell4 = new DgvCustomTextBoxCell();
                            DgvCustomTextBoxCell cell5 = new DgvCustomTextBoxCell();
                            DgvCustomTextBoxCell cell6 = new DgvCustomTextBoxCell();
                            //業者情報
                            DgvCustomTextBoxCell cell7 = new DgvCustomTextBoxCell();
                            DgvCustomTextBoxCell cell8 = new DgvCustomTextBoxCell();
                            DgvCustomTextBoxCell cell9 = new DgvCustomTextBoxCell();

                            if (("LAST_SBN_JOU_CD").Equals(cell.GetName()))
                            {
                                //名称
                                cell2 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"] as DgvCustomTextBoxCell;
                                //郵便番号
                                cell3 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"] as DgvCustomTextBoxCell;
                                //電話
                                cell4 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"] as DgvCustomTextBoxCell;
                                //住所
                                cell5 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"] as DgvCustomTextBoxCell;
                                //業者CD
                                cell7 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_CD"] as DgvCustomTextBoxCell;
                                //業者名
                                cell8 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_NAME"] as DgvCustomTextBoxCell;
                                //加入者番号
                                cell9 = this.Rows[rowIndex].Cells["Sbn_KanyushaCD"] as DgvCustomTextBoxCell;

                                if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = 
                                    ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = 
                                    ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = 
                                    ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell5.Name)) cell5.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell7.Name)) cell7.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell7)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell8.Name)) cell8.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell8)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell9.Name)) cell9.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell9)).OwningColumn.Name;

                                //値設定先コントロールを設定する
                                cell.ReturnControls = new[] { cell7, cell8, cell9, this.CurrentCell as ICustomDataGridControl, cell2, cell3,
                                    cell4, cell5 };
                            }
                            else if (("LAST_SBN_JOU_JISEKI_CD").Equals(cell.GetName()))
                            {
                                //名称
                                cell2 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"] as DgvCustomTextBoxCell;
                                //郵便番号
                                cell3 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"] as DgvCustomTextBoxCell;
                                //住所
                                cell5 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"] as DgvCustomTextBoxCell;
                                //電話
                                cell4 = this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"] as DgvCustomTextBoxCell;
                                //業者CD
                                cell7 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"] as DgvCustomTextBoxCell;
                                //業者名
                                cell8 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"] as DgvCustomTextBoxCell;
                                //加入者番号
                                cell9 = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"] as DgvCustomTextBoxCell;

                                if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = 
                                    ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell3.Name)) cell3.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell4.Name)) cell4.Name = 
                                    ((System.Windows.Forms.DataGridViewCell)(cell4)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell5.Name)) cell5.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell5)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell7.Name)) cell7.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell7)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell8.Name)) cell8.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell8)).OwningColumn.Name;

                                if (string.IsNullOrEmpty(cell9.Name)) cell9.Name =
                                    ((System.Windows.Forms.DataGridViewCell)(cell9)).OwningColumn.Name;

                                cell.ReturnControls = new[] { cell7, cell8, cell9, this.CurrentCell as ICustomDataGridControl, cell2, 
                                    cell3, cell4, cell5 };
                                
                            }
                            else if (("UNPANSAKI_GENBA_CD").Equals(cell.GetName()))
                            {
                                //検索画面のタイトルを設定
                                cell.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "事業場CD", "現場CD", "事業場名", "郵便番号", "都道府県", "住所", "電話番号" };

                                cell.PopupGetMasterField =
                                    "EDI_MEMBER_ID,GYOUSHA_CD,JIGYOUSHA_NAME,JIGYOUJOU_CD,GENBA_CD,JIGYOUJOU_NAME,JIGYOUJOU_POST,TODOFUKEN_NAME,DISP_JIGYOUJOU_ADDRESS,JIGYOUJOU_TEL,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS,JIGYOUSHA_ADDRESS1";

                                // UNPANSAKI_GENBA_CDの場合、運搬か処分かまだ決まらないため、加入者番号や事業場番号を表示させ、特定しやすいようにする。
                                cell.PopupDataSource = this.CreateDataSourceForUpnSakiJigyoujouPopup(dtSearch, cell.PopupGetMasterField);

                                //加入者番号
                                cell1 = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"] as DgvCustomTextBoxCell;
                                if (string.IsNullOrEmpty(cell1.Name)) cell1.Name = ((System.Windows.Forms.DataGridViewCell)(cell1)).OwningColumn.Name;
                                //名称
                                cell2 = this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"] as DgvCustomTextBoxCell;
                                if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
                                //事業場番号
                                cell6 = this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"] as DgvCustomTextBoxCell;
                                if (string.IsNullOrEmpty(cell6.Name)) cell6.Name = ((System.Windows.Forms.DataGridViewCell)(cell6)).OwningColumn.Name;
                                //業者CD
                                cell7 = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"] as DgvCustomTextBoxCell;
                                //業者名
                                cell8 = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"] as DgvCustomTextBoxCell;

                                //値設定先コントロールを設定する
                                cell.ReturnControls = new[] { cell1, cell7, cell8, cell6, this.CurrentCell as ICustomDataGridControl, cell2, null, null, null, null };
                                
                            }
                        }

                        break;
                    case WINDOW_ID.M_DENSHI_TANTOUSHA:
                        //担当者

                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        if (string.IsNullOrEmpty(this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value == null ?
                                string.Empty : this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value.ToString()))
                        {
                            //加入者番号が空白の場合
                            msgLogic.MessageBoxShow("E012", "収集運搬事業者CD");
                            //フォーカスを設定する
                            e.Handled = true;
                            return false;
                        }
                        else
                        {
                            dto.EDI_MEMBER_ID = this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value.ToString();
                        }

                        //担当者区分を設定する
                        dto.TANTOUSHA_KBN = "3";

                        dtSearch = DsMasterLogic.GetTantoushaData(dto);
                        InitializeSearchDataTable(ref dtSearch);
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "担当者CD", "担当者名", "加入者番号" };
                            cell.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME,EDI_MEMBER_ID";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "電子担当者";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells["UPN_TAN_NAME"] as DgvCustomTextBoxCell;

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }
                    
                        break;
                    case WINDOW_ID.M_DENSHI_YUUGAI_BUSSHITSU:
                        //電子有害物質
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        dtSearch = DsMasterLogic.GetYougaibutujituData(dto);

                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "有害物質CD", "有害物質名称" };
                            cell.PopupGetMasterField = "YUUGAI_BUSSHITSU_CD,YUUGAI_BUSSHITSU_NAME";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "電子有害物質";

                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();

                            switch (cell.GetName())
                            {
                                case "YUUGAI_CODE1":
                                    cell2 = this.Rows[rowIndex].Cells["YUUGAI_NAME1"] as DgvCustomTextBoxCell;
                                    break;
                                case "YUUGAI_CODE2":
                                    cell2 = this.Rows[rowIndex].Cells["YUUGAI_NAME2"] as DgvCustomTextBoxCell;
                                    break;
                                case "YUUGAI_CODE3":
                                    cell2 = this.Rows[rowIndex].Cells["YUUGAI_NAME3"] as DgvCustomTextBoxCell;
                                    break;
                                case "YUUGAI_CODE4":
                                    cell2 = this.Rows[rowIndex].Cells["YUUGAI_NAME4"] as DgvCustomTextBoxCell;
                                    break;
                                case "YUUGAI_CODE5":
                                    cell2 = this.Rows[rowIndex].Cells["YUUGAI_NAME5"] as DgvCustomTextBoxCell;
                                    break;
                                case "YUUGAI_CODE6":
                                    cell2 = this.Rows[rowIndex].Cells["YUUGAI_NAME6"] as DgvCustomTextBoxCell;
                                    break;
                                default:
                                    break;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;

                    case WINDOW_ID.M_SHARYOU:

                        //車輌
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        if (string.IsNullOrEmpty(this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value == null ?
                            string.Empty : this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value.ToString()))
                        {
                            //業者が空白の場合
                            msgLogic.MessageBoxShow("E012", "業者コード");
                            //フォーカスを設定する
                            e.Handled = true;
                            return false;
                        }
                        else
                        {
                            dto.GYOUSHA_CD = this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value.ToString();
                        }

                        dtSearch = DsMasterLogic.GetSharyouData(dto);
                        InitializeSearchDataTable(ref dtSearch);
                        //データが存在する場合
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "車輌CD", "車輌名" };
                            cell.PopupGetMasterField = "SHARYOU_CD,SHARYOU_NAME_RYAKU";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "車輌";

                            DgvCustomTextBoxCell cell2 = this.Rows[rowIndex].Cells["CAR_NAME"]
                                as DgvCustomTextBoxCell;

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;
                    case WINDOW_ID.M_HAIKI_SHURUI:
                        //廃棄種類
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        //共通クラス
                        IM_HAIKI_SHURUIDao haikiShuruidao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();

                        whereSql = string.Empty;

                        if (!string.IsNullOrEmpty(this.Rows[rowIndex].Cells["Hidden_FM_MEDIA_TYPE"].Value == null ?
                            string.Empty : this.Rows[rowIndex].Cells["Hidden_FM_MEDIA_TYPE"].Value.ToString()))
                        {
                            whereSql = " WHERE HAIKI_KBN_CD = " + this.Rows[rowIndex].Cells["Hidden_FM_MEDIA_TYPE"].Value.ToString();
                        }

                        dtSearch = haikiShuruidao.GetAllMasterDataForPopup(whereSql);

                        //データが存在する場合
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名" };
                            cell.PopupGetMasterField = "CD,NAME";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "電子廃棄物種類";

                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();

                            if ("FM_HAIKI_SHURUI_CD".Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"]
                                    as DgvCustomTextBoxCell;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;
                    case WINDOW_ID.M_GYOUSHA:
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        //共通クラス
                        IM_GYOUSHADao gyoushadao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();

                        dtSearch = gyoushadao.GetAllMasterDataForPopup(string.Empty);

                        //データが存在する場合
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "業者CD", "業者名" };
                            cell.PopupGetMasterField = "CD,NAME";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "業者";

                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();

                            if ("FM_HST_GYOUSHA_CD".Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"]
                                    as DgvCustomTextBoxCell;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }

                        break;
                    case WINDOW_ID.M_GENBA:
                        //現場
                        cell.PopupWindowName = "マスタ共通ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        //共通クラス
                        IM_GENBADao genbadao = DaoInitUtility.GetComponent<IM_GENBADao>();

                        whereSql = string.Empty;

                        if ("FM_HST_GENBA_CD".Equals(cell.GetName()))
                        {
                            if (!string.IsNullOrEmpty(this.GetDbValue(this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_CD"].Value)))
                            {
                                whereSql = " WHERE GYOUSHA_CD = '" + this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_CD"].Value.ToString() + "'";
                            }
                            else
                            {
                                //加入者番号が空白の場合
                                msgLogic.MessageBoxShow("E012", "排出業者CD");
                                //フォーカスを設定する
                                e.Handled = true;
                                return false;
                            }
                        }

                        dtSearch = genbadao.GetAllMasterDataForPopup(whereSql);

                        //データが存在する場合
                        if (dtSearch != null && dtSearch.Rows.Count > 0)
                        {
                            //検索画面のタイトルを設定
                            cell.PopupDataHeaderTitle = new string[] { "現場CD", "現場名" };
                            cell.PopupGetMasterField = "CD,NAME";
                            cell.PopupDataSource = dtSearch;
                            cell.PopupDataSource.TableName = "現場";

                            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();

                            if ("FM_HST_GENBA_CD".Equals(cell.GetName()))
                            {
                                cell2 = this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"]
                                    as DgvCustomTextBoxCell;
                            }

                            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                            //値設定先コントロールを設定する
                            cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2 };
                        }
                        break;
                    default:
                        break;
                }

                // PopupGetMasterField に定義している順番で値の設定先を指定
                // 呼び出し元にGYOUSHA_CD、呼び出し元+1の列にGYOUSHA_NAME_RYAKUが設定される

            }
            // 継承元を呼び出ポップアップを表示させる

            return base.ProcessDataGridViewKey(e); ;
        }


        /// <summary>
        /// EnterとTab専用の処理（読み取り専用セルは飛ばす）
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ProcessDataGridViewKeyTabEnter(System.Windows.Forms.KeyEventArgs e)
        {
            var ret = base.ProcessDataGridViewKey(e);

            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && this.CurrentCell != null)
            {
                //可視の最後のセル
                var lastVisibleCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);
                //フォーカスのセットすべき最後のセル
                var lastFocusCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible && !x.ReadOnly).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);


                
                //移動先が読み取り専用セルだった場合、もう一回動かす ※全セルReadOnlyのグリッドにはこのロジック適用しないように

                if (this.Focused && this.CurrentCell != null && this.CurrentCell.ReadOnly)
                {
                    //タブで移動した場合、focused==trueのまま次コントロールに移動するため無限ループしてしまう。そのためEnterに差し替え
                    if (e.Shift)
                    {
                        ret = this.ProcessDataGridViewKey(new KeyEventArgs(Keys.Enter | Keys.Shift));
                    }
                    else
                    {
                        ret = this.ProcessDataGridViewKey(new KeyEventArgs(Keys.Enter));
                    }


                    //前フォーカスで、グリッドの外に出た時は、最後の入力可能セルに戻る(フォーカスはエディットコントロールも見る必要あり)
                    if (!e.Shift && !(this.Focused || (this.EditingControl != null && this.EditingControl.Focused)))
                    {
                        //可視の最後のセルにフォーカスセット
                        this.CurrentCell = lastFocusCell;
                    }
                }     //最終セルで前移動したときの対応
                else if (!e.Shift && this.CurrentCell != null && this.CurrentCell.ColumnIndex == lastVisibleCell.ColumnIndex && (this.CurrentRow.IsNewRow || this.CurrentRow.Index == this.Rows.Count - 1))
                {
                    //可視の最後のセルにフォーカスセット
                    this.CurrentCell = lastFocusCell;
                }

            }

            //終了
            return ret;

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            var ret = base.ProcessDialogKey(keyData);

            //エンターとタブの場合
            if ((((keyData & Keys.KeyCode) == Keys.Enter) || (keyData & Keys.KeyCode) == Keys.Tab) && this.CurrentCell != null)
            {

                //可視の最後のセル
                var lastVisibleCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);
                //フォーカスのセットすべき最後のセル
                var lastFocusCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible && !x.ReadOnly).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);


                //移動先が読み取り専用セルだった場合、もう一回動かす ※Focusedのチェックを漏らすと、グリッドの外に出ると無限ループするので注意
                if (this.Focused && this.CurrentCell != null && this.CurrentCell.ReadOnly)
                {
                    ret = this.ProcessDialogKey(keyData);
                    //前フォーカスで、グリッドの外に出た時は、最後の入力可能セルに戻る(フォーカスはエディットコントロールも見る必要あり)
                    if (((keyData & Keys.Shift) != Keys.Shift) && !(this.Focused || (this.EditingControl != null && this.EditingControl.Focused)))
                    {
                        //可視の最後のセルにフォーカスセット
                        this.CurrentCell = lastFocusCell;
                    }
                }     //最終セルで前移動したときの対応
                else if ((keyData & Keys.Shift) != Keys.Shift && this.CurrentCell != null && this.CurrentCell.ColumnIndex == lastVisibleCell.ColumnIndex && (this.CurrentRow.IsNewRow || this.CurrentRow.Index == this.Rows.Count - 1))
                {
                    //可視の最後のセルにフォーカスセット
                    this.CurrentCell = lastFocusCell;
                }
            }

            //終了
            return ret;
        }


        /// <summary>
        /// 電子廃棄種類チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkElectronicHaikiShurui(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //検索パラメータの電子廃棄物種類を設定
            var haikibutuHaikiShuruiCd = this.Rows[rowIndex].Cells["HAIKI_SHURUI_CD"].Value.ToString();
            dto.HAIKISHURUICD = haikibutuHaikiShuruiCd;

            //加入者番号取得
            Control ctl = FindControl(ConstCls.CTXT_HAISYUTU_KANYUSHANO);
            if (ctl != null)
            {
                if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                {
                    dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                }
                else
                {
                    // 加入者番号が空白の場合
                    dto.EDI_MEMBER_ID = string.Empty;
                }
            }

            //検索実行
            DataTable dt = DsMasterLogic.GetDenshiHaikiShuruiData(dto);

            if (dt != null && dt.Rows.Count == 1)
            {
                this.Rows[rowIndex].Cells["HAIKI_SHURUI_NAME"].Value
                       = dt.Rows[0]["HAIKI_SHURUI_NAME"];
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                this.Rows[rowIndex].Cells["HAIKI_SHURUI_NAME"].Value = null;
                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 電子廃棄名称チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkElectronicHaikiName(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //検索パラメータの電子廃棄物種類を設定
            dto.HAIKI_NAME_CD = this.Rows[rowIndex].Cells["HAIKI_NAME_CD"].Value.ToString();

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //加入者番号取得
            Control ctl = FindControl(ConstCls.CTXT_HAISYUTU_KANYUSHANO);
            if (ctl != null)
            {
                if (!string.IsNullOrEmpty((ctl as TextBox).Text))
                {
                    dto.EDI_MEMBER_ID = (ctl as TextBox).Text;
                }
                else
                {
                    //加入者番号が空白の場合
                    return false;
                }
            }

            //検索実行
            DataTable dt = DsMasterLogic.GetDenshiHaikiNameData(dto);

            if (dt != null && dt.Rows.Count == 1)
            {
                this.Rows[rowIndex].Cells["HAIKI_NAME"].Value
                       = dt.Rows[0]["HAIKI_NAME"];
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                this.Rows[rowIndex].Cells["HAIKI_NAME"].Value = null;
                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 有害物質CDチェック
        /// </summary>
        /// <returns></returns>
        private bool ChkElectronicYuugaiBusshitsu(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //検索パラメータを設定
            dto.YUUGAI_BUSSHITSU_CD = this.Rows[rowIndex].Cells[this.CurrentCell.OwningColumn.Name].Value.ToString();

            //検索実行
            DataTable dt = DsMasterLogic.GetYougaibutujituData(dto);

            if (dt != null && dt.Rows.Count == 1)
            {

                switch (cell.GetName())
                {
                    case "YUUGAI_CODE1":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME1"].Value = dt.Rows[0]["YUUGAI_BUSSHITSU_NAME"] ;
                        break;
                    case "YUUGAI_CODE2":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME2"].Value = dt.Rows[0]["YUUGAI_BUSSHITSU_NAME"];
                        break;
                    case "YUUGAI_CODE3":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME3"].Value = dt.Rows[0]["YUUGAI_BUSSHITSU_NAME"];
                        break;
                    case "YUUGAI_CODE4":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME4"].Value = dt.Rows[0]["YUUGAI_BUSSHITSU_NAME"];
                        break;
                    case "YUUGAI_CODE5":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME5"].Value = dt.Rows[0]["YUUGAI_BUSSHITSU_NAME"];
                        break;
                    case "YUUGAI_CODE6":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME6"].Value = dt.Rows[0]["YUUGAI_BUSSHITSU_NAME"];
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "YUUGAI_CODE1":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME1"].Value = null;
                        break;
                    case "YUUGAI_CODE2":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME2"].Value = null;
                        break;
                    case "YUUGAI_CODE3":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME3"].Value = null;
                        break;
                    case "YUUGAI_CODE4":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME4"].Value = null;
                        break;
                    case "YUUGAI_CODE5":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME5"].Value = null;
                        break;
                    case "YUUGAI_CODE6":
                        this.Rows[rowIndex].Cells["YUUGAI_NAME6"].Value = null;
                        break;
                    default:
                        break;
                }
                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 電子事業者CDチェック
        /// </summary>
        /// <returns></returns>
        private bool ChkElectronicJigyousha(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //検索パラメータを設定
            dto.GYOUSHA_CD = this.Rows[rowIndex].Cells[this.CurrentCell.OwningColumn.Name].Value.ToString();

            if (("LAST_SBN_GYOUSHA_CD").Equals(this.CurrentCell.OwningColumn.Name)) dto.SBN_KBN = "1";
            if (("LAST_SBN_GYOUSHA_JISEKI_CD").Equals(this.CurrentCell.OwningColumn.Name)) dto.SBN_KBN = "1";
            if (("UPN_SHA_CD").Equals(this.CurrentCell.OwningColumn.Name))
            {
                dto.UPN_KBN = "1";
                object tmp2 = this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value;
                if (tmp2 != null && !string.IsNullOrEmpty(tmp2.ToString()))
                {
                    dto.EDI_MEMBER_ID = tmp2.ToString();
                }
            }
            if (("UNPANSAKI_GYOUSHA_CD").Equals(this.CurrentCell.OwningColumn.Name))
            {
                dto.UPN_KBN = "1";
                dto.SBN_KBN = "1";
                object tmp = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value;
                if (tmp != null && !string.IsNullOrEmpty(tmp.ToString()))
                {
                    dto.EDI_MEMBER_ID = tmp.ToString();
                }
            }
            if (("FM_HST_GYOUSHA_CD").Equals(this.CurrentCell.OwningColumn.Name)) dto.HST_KBN = "1";
            //検索実行
            DataTable dt = DsMasterLogic.GetDenshiGyoushaData(dto);

            if (dt != null && dt.Rows.Count == 1)
            {
                object tmpobj = null;
                object obj = null;
                switch (cell.GetName())
                {
                        
                    case "LAST_SBN_GYOUSHA_CD":
                        //最終処分事業場「予定」
                        ////元加入者番号取得し現在の加入者番号を比較し、違うの場合は事業場情報をクリアする
                        //tmpobj = this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value;
                        //if (tmpobj != null && !string.IsNullOrEmpty(tmpobj.ToString()))
                        //{
                        //    string LAST_SBN_GYOUSHA_EDI_MEMBER_ID = tmpobj.ToString();
                        //    obj = dt.Rows[0]["EDI_MEMBER_ID"];
                        //    if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                        //    {
                        //        string EDI_MEMBER_ID = obj.ToString();
                        //        if (LAST_SBN_GYOUSHA_EDI_MEMBER_ID != EDI_MEMBER_ID)
                        //        {
                        //            //最終処分事業場情報「予定」が全部クリアする
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_CD"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                        //        }
                        //    }
                        //}
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"];
                        this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value = dt.Rows[0]["EDI_MEMBER_ID"];
                        break;
                    case "LAST_SBN_GYOUSHA_JISEKI_CD":
                        //元加入者番号取得し現在の加入者番号を比較し、違うの場合は事業場情報をクリアする
                        //tmpobj = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value;
                        //if (tmpobj != null && !string.IsNullOrEmpty(tmpobj.ToString()))
                        //{
                        //    string LAST_SBN_GYOUSHA_EDI_MEMBER_ID = tmpobj.ToString();
                        //    obj = dt.Rows[0]["EDI_MEMBER_ID"];
                        //    if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                        //    {
                        //        string EDI_MEMBER_ID = obj.ToString();
                        //        if (LAST_SBN_GYOUSHA_EDI_MEMBER_ID != EDI_MEMBER_ID)
                        //        {
                        //            //最終処分事業場情報「実績」が全部クリアする
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_CD"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                        //            this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                        //        }
                        //    }
                        //}
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"];
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value = dt.Rows[0]["EDI_MEMBER_ID"];
                        break;
                    case "UPN_SHA_CD":
                        
                        //元加入者番号取得し現在の加入者番号を比較し、違うの場合は車輌CDと名称クリアする
                        tmpobj = this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value;
                        if (tmpobj != null && !string.IsNullOrEmpty(tmpobj.ToString()))
                        {
                            string UPN_SHA_EDI_MEMBER_ID = tmpobj.ToString();
                            obj = dt.Rows[0]["EDI_MEMBER_ID"];
                            if(obj!=null && !string.IsNullOrEmpty(obj.ToString())){
                                string EDI_MEMBER_ID = obj.ToString();
                                if (UPN_SHA_EDI_MEMBER_ID != EDI_MEMBER_ID)
                                {
                                    //車輌CDと名称クリアする
                                    this.Rows[rowIndex].Cells["SHARYOU_CD"].Value = null;
                                    this.Rows[rowIndex].Cells["CAR_NAME"].Value = null;
                                }
                            }
                        }
                        this.Rows[rowIndex].Cells["UPN_SHA_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"];
                        this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value = dt.Rows[0]["EDI_MEMBER_ID"];
                        break;
                    case "UNPANSAKI_GYOUSHA_CD":
                        ////元加入者番号取得し現在の加入者番号を比較し、違うの場合は運搬先事業場情報が全部クリアする
                        //tmpobj = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value;
                        //if (tmpobj != null && !string.IsNullOrEmpty(tmpobj.ToString()))
                        //{
                        //    string UPN_SHA_EDI_MEMBER_ID = tmpobj.ToString();
                        //    obj = dt.Rows[0]["EDI_MEMBER_ID"];
                        //    if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                        //    {
                        //        string EDI_MEMBER_ID = obj.ToString();
                        //        if (UPN_SHA_EDI_MEMBER_ID != EDI_MEMBER_ID)
                        //        {
                        //            //搬先事業場情報が全部クリアする
                        //            this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_CD"].Value = null;
                        //            this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                        //            this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = null;
                        //        }
                        //    }
                        //}

                        this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"];
                        this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value = dt.Rows[0]["EDI_MEMBER_ID"];
                        break;
                    case "FM_HST_GYOUSHA_CD":
                        this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = dt.Rows[0]["JIGYOUSHA_NAME"];
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1)
                {
                    dataFukkusuuFlg = true;

                    if (("UNPANSAKI_GYOUSHA_CD").Equals(cell.GetName()))
                    {
                        // 運搬先事業者が複数ヒットした場合はポップアップで一意に決定させる
                        this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value = null;
                        cell.PopupWindowName = "電マニデータ用検索ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        dto.UPN_KBN = "1";
                        dto.SBN_KBN = "1";

                        var dtSearch = DsMasterLogic.GetDenshiGyoushaData(dto);
                        InitializeSearchDataTable(ref dtSearch);

                        //検索画面のタイトルを設定
                        cell.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "郵便番号", "都道府県", "住所", "電話番号" };

                        cell.PopupGetMasterField =
                            "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";

                        cell.PopupDataSource = dtSearch;
                        cell.PopupDataSource.TableName = "電子事業者";

                        //名称
                        DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();
                        //加入者番号「非表示列」
                        DgvCustomTextBoxCell cell3 = new DgvCustomTextBoxCell();

                        cell2 = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"] as DgvCustomTextBoxCell;
                        //加入者番号Unpansaki_KanyushaCD
                        cell3 = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"] as DgvCustomTextBoxCell;

                        if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                            ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                        if (string.IsNullOrEmpty(cell3.Name)) cell3.Name =
                            ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;

                        //値設定先コントロールを設定する
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3 };

                        // popup表示
                        CustomControlExtLogic.PopUp((ICustomControl)cell);

                        if (this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"].Value == null
                            || string.IsNullOrEmpty(this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"].Value.ToString()))
                        {
                            dataFukkusuuFlg = true;
                            return false;
                        }
                        else
                        {
                            dataFukkusuuFlg = false;
                            return true;
                        }
                    }
                    else if ("UPN_SHA_CD".Equals(cell.GetName()))
                    {
                        cell.PopupWindowName = "電マニデータ用検索ポップアップ";
                        cell.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "郵便番号", "都道府県", "住所", "電話番号" };
                        cell.PopupGetMasterField = "GYOUSHA_CD,JIGYOUSHA_NAME,EDI_MEMBER_ID,JIGYOUSHA_POST,JIGYOUSHA_TEL,JIGYOUSHA_ADDRESS";
                        cell.PopupDataSource = dt;
                        cell.PopupDataSource.TableName = "電子事業者";

                        //名称
                        DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();
                        //加入者番号「非表示列」
                        DgvCustomTextBoxCell cell3 = new DgvCustomTextBoxCell();

                        if (("UPN_SHA_CD").Equals(cell.GetName()))
                        {
                            cell2 = this.Rows[rowIndex].Cells["UPN_SHA_NAME"] as DgvCustomTextBoxCell;
                            //加入者番号
                            cell3 = this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"] as DgvCustomTextBoxCell;
                        }

                        if (string.IsNullOrEmpty(cell2.Name)) cell2.Name =
                            ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;

                        if (string.IsNullOrEmpty(cell3.Name)) cell3.Name =
                            ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;

                        //値設定先コントロールを設定する
                        cell.ReturnControls = new[] { this.CurrentCell as ICustomDataGridControl, cell2, cell3 };

                        // popup表示
                        CustomControlExtLogic.PopUp((ICustomControl)cell);

                        if (cell3.Value == null || string.IsNullOrEmpty(cell3.Value.ToString()))
                        {
                            dataFukkusuuFlg = true;
                            return false;
                        }
                        else
                        {
                            dataFukkusuuFlg = false;
                            return true;
                        }
                    }
                }

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "LAST_SBN_GYOUSHA_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value = null;
                        //最終処分事業場情報「予定」が全部クリアする
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_CD"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                        break;
                    case "LAST_SBN_GYOUSHA_JISEKI_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value = null;
                        //最終処分事業場情報[実績]クリア
                        this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_CD"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                        break;
                    case "UPN_SHA_CD":
                        this.Rows[rowIndex].Cells["UPN_SHA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value = null;
                        //車輌CDと名称クリアする
                        this.Rows[rowIndex].Cells["SHARYOU_CD"].Value = null;
                        this.Rows[rowIndex].Cells["CAR_NAME"].Value = null;
                        break;
                    case "UNPANSAKI_GYOUSHA_CD":
                        // 名称は1つのはず
                        this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value = null;
                        //事業場クリア
                        this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value = null;
                        this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_CD"].Value = null;
                        this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                        break;

                    case "FM_HST_GYOUSHA_CD":
                        this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = null;
                        //事業場クリア
                        this.Rows[rowIndex].Cells["FM_HST_GENBA_CD"].Value = null;
                        this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;
                        break;
                    default:
                        break;
                }
                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 電子事業場CDチェック
        /// </summary>
        /// <returns></returns>
        private bool ChkElectronicJigyoujou(ref bool dataFukkusuuFlg, ref bool isGyoushaNull)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
            isGyoushaNull = false;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //検索パラメータを設定
            dto.GENBA_CD = this.Rows[rowIndex].Cells[this.CurrentCell.OwningColumn.Name].Value.ToString();

            if (this.CurrentCell.OwningColumn.Name.Equals("LAST_SBN_JOU_CD"))
            {
                object tmp = this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value;
                if (tmp != null){
                    dto.EDI_MEMBER_ID = tmp.ToString();
                    dto.JIGYOUJOU_KBN = "3";
                }
                // 業者CD
                object lastSbnGyoushaCd = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_CD"].Value;
                if (lastSbnGyoushaCd != null)
                {
                    dto.GYOUSHA_CD = lastSbnGyoushaCd.ToString();
                }
                // 事業場番号
                object lastSbnJigyoujouCd = this.Rows[rowIndex].Cells["sbn_JigyoujouCD"].Value;
                if (lastSbnJigyoujouCd != null)
                {
                    dto.JIGYOUJOU_CD = lastSbnJigyoujouCd.ToString();
                }
            }

            if (this.CurrentCell.OwningColumn.Name.Equals("LAST_SBN_JOU_JISEKI_CD"))
            {
                object tmp = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value;
                if (tmp != null){
                    dto.EDI_MEMBER_ID = tmp.ToString();
                    dto.JIGYOUJOU_KBN = "3";
                }
                // 業者CD
                object lastSbnGyoushaJisekiCd = this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_CD"].Value;
                if (lastSbnGyoushaJisekiCd != null)
                {
                    dto.GYOUSHA_CD = lastSbnGyoushaJisekiCd.ToString();
                }
                // 事業場番号
                object lastSbnJigyoujouCd = this.Rows[rowIndex].Cells["LAST_SBN_GENBA_JigyoujouCD"].Value;
                if (lastSbnJigyoujouCd != null)
                {
                    dto.JIGYOUJOU_CD = lastSbnJigyoujouCd.ToString();
                }
            }

            if (this.CurrentCell.OwningColumn.Name.Equals("UNPANSAKI_GENBA_CD"))
            {
                object tmp = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value;
                if (tmp != null && !string.IsNullOrEmpty(tmp.ToString())){
                    dto.EDI_MEMBER_ID = tmp.ToString();
                }

                dto.JIGYOUJOU_KBN_LIST = new List<string>();
                dto.JIGYOUJOU_KBN_LIST.Add("2");
                dto.JIGYOUJOU_KBN_LIST.Add("3");
                dto.JIGYOUSHA_KBN_LIST = new List<string>();
                dto.JIGYOUSHA_KBN_LIST.Add("2");
                dto.JIGYOUSHA_KBN_LIST.Add("3");

                // 業者CD
                object upnansakiGyoushaCd = this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_CD"].Value;
                if (upnansakiGyoushaCd != null)
                {
                    dto.GYOUSHA_CD = upnansakiGyoushaCd.ToString();
                }
                // 事業場番号
                object unpansakiJigyoujouCd = this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value;
                if (unpansakiJigyoujouCd != null)
                {
                    dto.JIGYOUJOU_CD = unpansakiJigyoujouCd.ToString();
                }
            }

            if (string.IsNullOrEmpty(dto.GENBA_CD))
            {
                switch (cell.GetName())
                {
                    case "LAST_SBN_JOU_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["sbn_JigyoujouCD"].Value = string.Empty;
                        break;
                    case "LAST_SBN_JOU_JISEKI_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["LAST_SBN_GENBA_JigyoujouCD"].Value = string.Empty;
                        break;
                    case "UNPANSAKI_GENBA_CD":
                        this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = string.Empty;
                        this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = string.Empty;
                        break;
                    default:
                        break;
                }
                return true;
            }

            if (string.IsNullOrEmpty(dto.GYOUSHA_CD))
            {
                string strMsg = string.Empty;
                switch (cell.GetName())
                {
                    case "LAST_SBN_JOU_CD":
                    case "LAST_SBN_JOU_JISEKI_CD":
                        strMsg = "最終処分業者";
                        break;
                    case "UNPANSAKI_GENBA_CD":
                        strMsg = "運搬先業者";
                        break;
                    default:
                        break;
                }
                msgLogic.MessageBoxShow("E051", strMsg);
                this.Rows[rowIndex].Cells[this.CurrentCell.OwningColumn.Name].Value = string.Empty;
                if (this.EditingControl != null)
                {
                    this.EditingControl.Text = string.Empty;
                }
                isGyoushaNull = true;
                return false;
            }

            //検索実行
            DataTable dt = DsMasterLogic.GetDenshiGenbaData(dto);

            if (dt != null && dt.Rows.Count == 1)
            {
                switch (cell.GetName())
                {
                    case "LAST_SBN_JOU_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = dt.Rows[0]["JIGYOUJOU_NAME"];
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = dt.Rows[0]["JIGYOUJOU_POST"];
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = dt.Rows[0]["JIGYOUJOU_ADDRESS"];
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = dt.Rows[0]["JIGYOUJOU_TEL"];
                        this.Rows[rowIndex].Cells["sbn_JigyoujouCD"].Value = dt.Rows[0]["JIGYOUJOU_CD"];
                        //this.Rows[rowIndex].Cells["GenbaOK_KanyushaCD"].Value = dto.EDI_MEMBER_ID;
                        break;
                    case "LAST_SBN_JOU_JISEKI_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = dt.Rows[0]["JIGYOUJOU_NAME"];
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = dt.Rows[0]["JIGYOUJOU_POST"];
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = dt.Rows[0]["JIGYOUJOU_ADDRESS"];
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = dt.Rows[0]["JIGYOUJOU_TEL"];
                        this.Rows[rowIndex].Cells["LAST_SBN_GENBA_JigyoujouCD"].Value = dt.Rows[0]["JIGYOUJOU_CD"];
                       // this.Rows[rowIndex].Cells["GenbaOK_Jiseki_kanyushaCD"].Value = dto.EDI_MEMBER_ID;
                        break;
                    case "UNPANSAKI_GENBA_CD":
                        this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = dt.Rows[0]["JIGYOUJOU_NAME"];
                        this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = dt.Rows[0]["JIGYOUJOU_CD"];
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1)
                {
                    dataFukkusuuFlg = true;

                    if (("UNPANSAKI_GENBA_CD").Equals(cell.GetName()))
                    {
                        // 必須チェック
                        // この二件が無い場合は業者未入力
                        if (string.IsNullOrEmpty(dto.EDI_MEMBER_ID)
                            && string.IsNullOrEmpty(dto.GYOUSHA_CD))
                        {
                            msgLogic.MessageBoxShow("E012", "運搬先事業者CD");
                            return false;
                        }

                        // 運搬先事業場は複数ヒットを許す
                        this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = null;

                        var sendParam = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                        PopupSearchSendParamDto paramDto = new PopupSearchSendParamDto();
                        paramDto.KeyName = "GYOUSHA_CD";
                        paramDto.Value = dto.GYOUSHA_CD;
                        sendParam.Add(paramDto);
                        cell.PopupSearchSendParams = sendParam;

                        cell.PopupWindowName = "電マニデータ複数キー用検索ポップアップ";
                        cell.FocusOutCheckMethod.Clear();

                        // popupの設定変更
                        this.SetPopupSettingForUnpanSakiGenbaCd(cell, rowIndex);
                        cell.PopupDataSource = this.CreateDataSourceForUpnSakiJigyoujouPopup(dt, cell.PopupGetMasterField);
                        cell.PopupDataSource.TableName = "電子事業場";

                        // ポップアップ選択後、Enterイベントが発生し、前回値チェックで不都合が発生するため
                        // ポップアップ選択が発生したかどうかのフラグを制御する処理を追加。
                        cell.PopupAfterExecute = this.Myform.PopupAfter_UNPANSAKI_GENBA_CD;

                        // popup表示
                        CustomControlExtLogic.PopUp((ICustomControl)cell);

                        if (this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value == null
                            || string.IsNullOrEmpty(this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value.ToString()))
                        {
                            dataFukkusuuFlg = true;
                            return false;
                        }
                        else
                        {
                            dataFukkusuuFlg = false;
                            return true;
                        }
                    }
                }

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "LAST_SBN_JOU_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                        break;
                    case "LAST_SBN_JOU_JISEKI_CD":
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                        this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                        break;
                    case "UNPANSAKI_GENBA_CD":
                        this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = null;

                        break;
                    default:
                        break;
                }
                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 電子担当者チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkElectronicTantousha(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //加入者番号の取得
            object tmpobj = this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value;
            if (tmpobj!=null){
                dto.EDI_MEMBER_ID = tmpobj.ToString();
            }
            //担当者区分の取得
            dto.TANTOUSHA_KBN = "3";
            
            //担当者CDを設定
            tmpobj = this.Rows[rowIndex].Cells[this.CurrentCell.OwningColumn.Name].Value;
            if (tmpobj != null){
                dto.TANTOUSHA_CD = tmpobj.ToString();
            }
            //検索実行
            DataTable dt = DsMasterLogic.GetTantoushaData(dto);

            if (dt != null && dt.Rows.Count == 1){
                this.Rows[rowIndex].Cells["UPN_TAN_NAME"].Value = dt.Rows[0]["TANTOUSHA_NAME"];
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;
                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                this.Rows[rowIndex].Cells["UPN_TAN_NAME"].Value = null;

                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkSharyou(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();

            //加入者番号の取得
            dto.GYOUSHA_CD = this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value == null ? 
                null : this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value.ToString();

            //車輌コードを設定
            dto.SHARYOU_CD = this.Rows[rowIndex].Cells[this.CurrentCell.OwningColumn.Name].Value.ToString();

            //検索実行
            DataTable dt = DsMasterLogic.GetSharyouData(dto);

            if (dt != null && dt.Rows.Count == 1)
            {
                this.Rows[rowIndex].Cells["CAR_NAME"].Value = dt.Rows[0]["SHARYOU_NAME_RYAKU"];
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                this.Rows[rowIndex].Cells["CAR_NAME"].Value = null;

                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 単位チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkUnit(ref bool dataFukkusuuFlg)
        { 
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();

            //単位コードの取得
            string unitCd = string.Empty; 

            switch (cell.GetName())
            {
                case "UNIT_CD":
                    unitCd = this.Rows[rowIndex].Cells["UNIT_CD"].Value.ToString();
                    break;
                default:
                    break;
            }

            //検索実行
            string unitNm = DsMasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNIT, unitCd);

            if (!string.IsNullOrEmpty(unitNm))
            {
                switch (cell.GetName())
                {
                    case "UNIT_CD":
                        this.Rows[rowIndex].Cells["UNIT_NAME"].Value = unitNm;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (string.Empty.Equals(unitNm)) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "UNIT_CD":
                        this.Rows[rowIndex].Cells["UNIT_NAME"].Value = null;
                        break;
                    default:
                        break;
                }

                return false;
            }

            //正常の場合
            return true;

        }

        /// <summary>
        /// 紙の場合、単位チェック
        /// </summary>
        /// <param name="dataFukkusuuFlg"></param>
        /// <returns></returns>
        private bool ChkKamiUnit(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //単位コードの取得
            string unitCd = string.Empty;

            switch (cell.GetName())
            {
                case "FM_HAIKI_UNIT_CD":
                    unitCd = this.Rows[rowIndex].Cells["FM_HAIKI_UNIT_CD"].Value.ToString();
                    break;
                default:
                    break;
            }

            //検索実行
            //共通クラス
            IM_UNITDao unitdao = DaoInitUtility.GetComponent<IM_UNITDao>();

            string whereSql = string.Empty;

            if ("FM_HAIKI_UNIT_CD".Equals(cell.GetName()))
            {
                //紙の場合
                whereSql += " WHERE UNIT_CD = " + unitCd; ;
            }

            DataTable dt = unitdao.GetAllMasterDataForPopup(whereSql);

            if (dt != null && dt.Rows.Count == 1 )
            {
                switch (cell.GetName())
                {
                    case "FM_HAIKI_UNIT_CD":
                        this.Rows[rowIndex].Cells["FM_UNIT_NAME_RYAKU"].Value = this.GetDbValue(dt.Rows[0]["NAME"]);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "FM_HAIKI_UNIT_CD":
                        this.Rows[rowIndex].Cells["FM_UNIT_NAME_RYAKU"].Value = null;
                        break;
                    default:
                        break;
                }

                return false;
            }

            //正常の場合
            return true;
        }

        /// <summary>
        /// 荷姿チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkNisugata(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();

            //荷姿コードの取得
            string nisugataCd = this.Rows[rowIndex].Cells["NISUGATA_CD"].Value.ToString();

            //検索実行
            string nisugataNm = DsMasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_NISUGATA, nisugataCd);

            if (!string.IsNullOrEmpty(nisugataNm))
            {
                this.Rows[rowIndex].Cells["NISUGATA_NAME"].Value = nisugataNm;
            }
            else
            {
                //データが複数件の場合
                if (string.Empty.Equals(nisugataNm)) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                this.Rows[rowIndex].Cells["NISUGATA_NAME"].Value = null;

                return false;
            }

            //正常の場合
            return true;

        }


        /// <summary>
        /// 廃棄物種類チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkHaikiShurui(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //廃棄物種類コードの取得
            string HaikiShuruiCd = string.Empty;

            // 電マニの場合、チェックを行わない。
            if (this.Rows[rowIndex].Cells["Hidden_FM_MEDIA_TYPE"].Value.ToString().Equals(ConstCls.DENSHI_MEDIA_TYPE))
            {
                return true;
            }

            switch (cell.GetName())
            {
                case "FM_HAIKI_SHURUI_CD":
                    HaikiShuruiCd = this.Rows[rowIndex].Cells["FM_HAIKI_SHURUI_CD"].Value.ToString();
                    break;
                default:
                    break;
            }

            //検索実行
            //共通クラス
            IM_HAIKI_SHURUIDao HaikiShuruidao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();

            string whereSql = string.Empty;

            if ("FM_HAIKI_SHURUI_CD".Equals(cell.GetName()))
            {
                //紙の場合
                whereSql = " WHERE HAIKI_KBN_CD = " + this.Rows[rowIndex].Cells["Hidden_FM_MEDIA_TYPE"].Value.ToString();
                whereSql += " AND HAIKI_SHURUI_CD = '" + HaikiShuruiCd + "'"; 
            }

            DataTable dt = HaikiShuruidao.GetAllMasterDataForPopup(whereSql);

            if (dt != null && dt.Rows.Count == 1)
            {
                switch (cell.GetName())
                {
                    case "FM_HAIKI_SHURUI_CD":
                        this.Rows[rowIndex].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = this.GetDbValue(dt.Rows[0]["NAME"]);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "FM_HAIKI_SHURUI_CD":
                        this.Rows[rowIndex].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = null;
                        break;
                    default:
                        break;
                }

                return false;
            }

            //正常の場合
            return true;

        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkGyousha(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //業者コードの取得
            string cd = string.Empty;

            switch (cell.GetName())
            {
                case "FM_HST_GYOUSHA_CD":
                    cd = this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_CD"].Value.ToString();
                    break;
                default:
                    break;
            }

            //検索実行
            //共通クラス
            IM_GYOUSHADao Gyoushadao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();

            string whereSql = string.Empty;

            if ("FM_HST_GYOUSHA_CD".Equals(cell.GetName()))
            {
                //紙の場合
                whereSql = " WHERE GYOUSHA_CD = '" + cd + "'";
            }

            DataTable dt = Gyoushadao.GetAllMasterDataForPopup(whereSql);

            if (dt != null && dt.Rows.Count == 1)
            {
                switch (cell.GetName())
                {
                    case "FM_HST_GYOUSHA_CD":
                        this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = this.GetDbValue(dt.Rows[0]["NAME"]);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "FM_HST_GYOUSHA_CD":
                        this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = null;
                        this.Rows[rowIndex].Cells["FM_HST_GENBA_CD"].Value = null;
                        this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;
                        break;
                    default:
                        break;
                }

                return false;
            }

            //正常の場合
            return true;

        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <returns></returns>
        private bool ChkGenba(ref bool dataFukkusuuFlg)
        {
            //セルの親行のインデックスを取得する
            int rowIndex = this.CurrentCell.RowIndex;

            //セールを取得する
            var cell = this.CurrentCell as ICustomDataGridControl;

            //現場コードの取得
            string cd = string.Empty;

            switch (cell.GetName())
            {
                case "FM_HST_GENBA_CD":

                    string GyoushaCd = this.GetDbValue(this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_CD"].Value);
                    if (string.IsNullOrEmpty(GyoushaCd))
                    {
                        //業者が空白の場合
                        msgLogic.MessageBoxShow("E012", "排出業者CD");
                        return false;
                    }

                    cd = this.GetDbValue(this.Rows[rowIndex].Cells["FM_HST_GENBA_CD"].Value);
                    break;
                default:
                    break;
            }

            //検索実行
            //共通クラス
            IM_GENBADao genbadao = DaoInitUtility.GetComponent<IM_GENBADao>();

            string whereSql = string.Empty;

            if ("FM_HST_GENBA_CD".Equals(cell.GetName()))
            {
                //紙の場合
                whereSql = " WHERE GYOUSHA_CD = '" + this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_CD"].Value.ToString() +"'";
                whereSql += " AND GENBA_CD = '" + cd + "'" ;
            }

            DataTable dt = genbadao.GetAllMasterDataForPopup(whereSql);

            if (dt != null && dt.Rows.Count == 1)
            {
                switch (cell.GetName())
                {
                    case "FM_HST_GENBA_CD":
                        this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = this.GetDbValue(dt.Rows[0]["NAME"]);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //データが複数件の場合
                if (dt != null && dt.Rows.Count > 1) dataFukkusuuFlg = true;

                //データが存在しないまたはデータが複数件の場合
                //名称列クリア
                switch (cell.GetName())
                {
                    case "FM_HST_GENBA_CD":
                        this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;
                        break;
                    default:
                        break;
                }

                return false;
            }

            //正常の場合
            return true;

        }

        /// <summary>
        /// セルにフォーカスが移ったときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellEnter(e);

            // 入力されている値を保存
            var columnName = this.Columns[e.ColumnIndex].Name;
            var cellValue = String.Empty;
            if (this[e.ColumnIndex, e.RowIndex].Value != null)
            {
                cellValue = this[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
            if (prevCellValueDictionary.ContainsKey(columnName))
            {
                prevCellValueDictionary[columnName] = cellValue;
            }
            else
            {
                prevCellValueDictionary.Add(columnName, cellValue);
            }
        }

        /// <summary>
        /// セルのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnCellValidating(System.Windows.Forms.DataGridViewCellValidatingEventArgs e)
        {
            base.OnCellValidating(e);

            var columnName = this.Columns[e.ColumnIndex].Name;
            var cellValue = String.Empty;
            if (this[e.ColumnIndex, e.RowIndex].Value != null)
            {
                cellValue = this[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
            //if (prevCellValueDictionary[columnName] == cellValue)
            //{
            //    // 前回値と変更がない場合は、バリデート処理を行わない
            //    return;
            //}

            this.EditMode = DataGridViewEditMode.EditOnEnter;
            msgLogic = new MessageBoxShowLogic();
            
            bool checkErrFlg = false;
            string cellName = this.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name;
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            //編集不可
            if (this.CurrentCell != null && this.Columns[this.CurrentCell.ColumnIndex].ReadOnly) return;

            cellValue = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ? 
                string.Empty : this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            ICustomDataGridControl cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as ICustomDataGridControl;

            if (cell == null) return;

            //データ複数フラグ
            bool dataFukkusuuFlg = false;
            bool isGyoushaNull = false;

            switch (cell.PopupWindowId)
            {
                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                    //廃棄種類
                    if (prevCellValueDictionary[columnName] == cellValue && !this.isInputError)
                    {
                        // 前回値チェック
                        return;
                    }
                    else if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkElectronicHaikiShurui(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "廃棄種類コード");
                        if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "廃棄種類コード");
                        checkErrFlg = true;
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["HAIKI_SHURUI_NAME"].Value = null;

                        if ("HAIKI_SHURUI_CD".Equals(cell.GetName()))
                        {
                            this.Rows[e.RowIndex].Cells["HAIKI_SHURUI_NAME"].Value = null;
                        }
                        else if ("FM_HAIKI_SHURUI_CD".Equals(cell.GetName()))
                        {
                            this.Rows[e.RowIndex].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = null;
                        }
                    }

                    break;
                case WINDOW_ID.M_DENSHI_HAIKI_NAME:
                    //廃棄物名称
                    if (prevCellValueDictionary[columnName] == cellValue && !this.isInputError)
                    {
                        // 前回値チェック
                        return;
                    }
                    else if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkElectronicHaikiName(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "廃棄物名称コード");
                        if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "廃棄物名称コード");
                        checkErrFlg = true;
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["HAIKI_NAME"].Value = null;
                    }

                    break;

                case WINDOW_ID.M_DENSHI_YUUGAI_BUSSHITSU:
                    //有害物質

                    if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkElectronicYuugaiBusshitsu(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "有害物質コード");
                        if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "有害物質コード");
                        checkErrFlg = true;
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        switch (this.CurrentCell.OwningColumn.Name)
                        {
                            case "YUUGAI_CODE1":
                                this.Rows[rowIndex].Cells["YUUGAI_NAME1"].Value = null;
                                break;
                            case "YUUGAI_CODE2":
                                this.Rows[rowIndex].Cells["YUUGAI_NAME2"].Value = null;
                                break;
                            case "YUUGAI_CODE3":
                                this.Rows[rowIndex].Cells["YUUGAI_NAME3"].Value = null;
                                break;
                            case "YUUGAI_CODE4":
                                this.Rows[rowIndex].Cells["YUUGAI_NAME4"].Value = null;
                                break;
                            case "YUUGAI_CODE5":
                                this.Rows[rowIndex].Cells["YUUGAI_NAME5"].Value = null;
                                break;
                            case "YUUGAI_CODE6":
                                this.Rows[rowIndex].Cells["YUUGAI_NAME6"].Value = null;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    //事業者入力

                    if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkElectronicJigyousha(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "電子事業者コード");
                        if (dataFukkusuuFlg)
                        {
                            switch (this.CurrentCell.OwningColumn.Name)
                            {
                                case "UNPANSAKI_GYOUSHA_CD":
                                    // 運搬先業者の場合、複数ヒットしたら一意になるまでフォーカス移動させない
                                    break;

                                default:
                                    msgLogic.MessageBoxShow("E031", "電子事業者コード");
                                    break;
                            }
                        }
                        checkErrFlg = true;

                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        switch (this.CurrentCell.OwningColumn.Name)
                        {
                            case "LAST_SBN_GYOUSHA_CD":
                                this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["Sbn_KanyushaCD"].Value = null;
                                //事業場クリア
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_CD"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                                break;
                            case "LAST_SBN_GYOUSHA_JISEKI_CD":
                                this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value = null;
                                //事業場クリア
                                this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_KanyushaCD"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_CD"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                                break;
                            case "UPN_SHA_CD":
                                this.Rows[rowIndex].Cells["UPN_SHA_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["UPN_SHA_EDI_MEMBER_ID"].Value = null;
                                //車輌CDと名称クリアする
                                this.Rows[rowIndex].Cells["SHARYOU_CD"].Value = null;
                                this.Rows[rowIndex].Cells["CAR_NAME"].Value = null;
                                break;
                            case "UNPANSAKI_GYOUSHA_CD":
                                this.Rows[rowIndex].Cells["UNPANSAKI_GYOUSHA_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value = null;
                                //事業場クリア
                                this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"].Value = null;
                                this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_CD"].Value = null;
                                this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                                break;
                            case "FM_HST_GYOUSHA_CD":
                                this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = null;
                                //事業場クリア
                                this.Rows[rowIndex].Cells["FM_HST_GENBA_CD"].Value = null;
                                this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    //(電子)事業場

                    if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkElectronicJigyoujou(ref dataFukkusuuFlg, ref isGyoushaNull))
                    {
                        if (isGyoushaNull)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "電子事業場コード");
                            if (dataFukkusuuFlg)
                            {
                                if (!("UNPANSAKI_GENBA_CD").Equals(this.CurrentCell.OwningColumn.Name))
                                {
                                    msgLogic.MessageBoxShow("E031", "電子事業場コード");
                                }
                            }
                            checkErrFlg = true;
                        }
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        switch (this.CurrentCell.OwningColumn.Name)
                        {
                            case "LAST_SBN_JOU_CD":
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                                break;
                            case "LAST_SBN_JOU_JISEKI_CD":
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                                break;
                            case "UNPANSAKI_GENBA_CD":
                                this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = null;
                                break;
                            case "FM_HST_GENBA_CD":
                                this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                case WINDOW_ID.M_DENSHI_TANTOUSHA:
                    //(電子)担当者

                    if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkElectronicTantousha(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "電子担当者コード");
                        if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "電子担当者コード");
                        checkErrFlg = true;
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        switch (this.CurrentCell.OwningColumn.Name)
                        {
                            case "LAST_SBN_JOU_CD":
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                                break;
                            case "LAST_SBN_GYOUSHA_JISEKI_CD":
                                this.Rows[rowIndex].Cells["LAST_SBN_GYOUSHA_JISEKI_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                                break;
                            case "UNPANSAKI_GENBA_CD":
                                this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                                this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = null;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case WINDOW_ID.M_SHARYOU:
                    //車輌
                    if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkSharyou(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "車輌コード");
                        if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "車輌コード");
                        checkErrFlg = true;
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["CAR_NAME"].Value = null;
                    }

                    break;
                case WINDOW_ID.M_UNIT:
                    //単位
                    if (prevCellValueDictionary[columnName] == cellValue && !this.isInputError)
                    {
                        // 前回値チェック
                        return;
                    }
                    else if ("UNIT_CD".Equals(this.CurrentCell.OwningColumn.Name))
                    {
                        if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkUnit(ref dataFukkusuuFlg))
                        {
                            if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "単位コード");
                            if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "単位コード");
                            checkErrFlg = true;
                        }
                        else if (string.IsNullOrEmpty(cellValue))
                        {
                            ////名称列クリア
                            this.Rows[rowIndex].Cells["UNIT_NAME"].Value = null;
                        }

                    }
                    else if ("FM_HAIKI_UNIT_CD".Equals(this.CurrentCell.OwningColumn.Name))
                    {
                        if (!string.IsNullOrEmpty(cellValue)
                            && !this.ChkKamiUnit(ref dataFukkusuuFlg))
                        {
                            if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "単位コード");
                            if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "単位コード");
                            checkErrFlg = true;
                        }
                        else if (string.IsNullOrEmpty(cellValue))
                        {
                            //名称列クリア
                            this.Rows[rowIndex].Cells["FM_UNIT_NAME_RYAKU"].Value = null;
                        }
                    }

                    break;
                case WINDOW_ID.M_NISUGATA:
                    //荷姿
                    if (prevCellValueDictionary[columnName] == cellValue && !this.isInputError)
                    {
                        // 前回値チェック
                        return;
                    }
                    else if (!string.IsNullOrEmpty(cellValue)
                        && !this.ChkNisugata(ref dataFukkusuuFlg))
                    {
                        if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "荷姿コード");
                        if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "荷姿コード");
                        checkErrFlg = true;
                    }
                    else if (string.IsNullOrEmpty(cellValue))
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["NISUGATA_NAME"].Value = null;
                    }
                    break;
                case WINDOW_ID.M_HAIKI_SHURUI:

                    //中間処理産業廃棄物．廃棄物種類
                    if ("FM_HAIKI_SHURUI_CD".Equals(this.CurrentCell.OwningColumn.Name))
                    {
                        if (!string.IsNullOrEmpty(cellValue)
                            && !this.ChkHaikiShurui(ref dataFukkusuuFlg))
                        {
                            if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "廃棄物種類コード");
                            if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "廃棄物種類コード");
                            checkErrFlg = true;
                        }
                        else if (string.IsNullOrEmpty(cellValue))
                        {
                            //名称列クリア
                            this.Rows[rowIndex].Cells["FM_HAIKI_SHURUI_NAME_RYAKU"].Value = null;
                        }
                    }
                    break;
                case WINDOW_ID.M_GYOUSHA:

                    //業者
                    //中間処理産業廃棄物．排出事業者
                    if ("FM_HST_GYOUSHA_CD".Equals(this.CurrentCell.OwningColumn.Name))
                    {
                        if (!string.IsNullOrEmpty(cellValue)
                            && !this.ChkGyousha(ref dataFukkusuuFlg))
                        {
                            if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "排出業者コード");
                            if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "排出業者コード");
                            checkErrFlg = true;
                        }
                        else if (string.IsNullOrEmpty(cellValue))
                        {
                            //名称列クリア
                            this.Rows[rowIndex].Cells["FM_HST_GYOUSHA_NAME"].Value = null;
                            this.Rows[rowIndex].Cells["FM_HST_GENBA_CD"].Value = null;
                            this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;

                        }
                    }
                    break;
                case WINDOW_ID.M_GENBA:

                    //現場
                    //中間処理産業廃棄物．排出事業場
                    if ("FM_HST_GENBA_CD".Equals(this.CurrentCell.OwningColumn.Name))
                    {
                        if (!string.IsNullOrEmpty(cellValue)
                            && !this.ChkGenba(ref dataFukkusuuFlg))
                        {
                            if (!dataFukkusuuFlg) msgLogic.MessageBoxShow("E020", "排出事業場コード");
                            if (dataFukkusuuFlg) msgLogic.MessageBoxShow("E031", "排出事業場コード");
                            checkErrFlg = true;
                        }
                        else if (string.IsNullOrEmpty(cellValue))
                        {
                            //名称列クリア
                            this.Rows[rowIndex].Cells["FM_HST_GENBA_NAME"].Value = null;
                        }
                    }
                    break;
                default:
                    break;
            }

            //数量確認者コードチェック
            if ("SUU_KAKUTEI_CODE".Equals(cell.GetName()))
            {
                if (prevCellValueDictionary[columnName] == cellValue && !this.isInputError)
                {
                    // 前回値チェック
                    return;
                }
                else if (!string.IsNullOrEmpty(cellValue))
                {
                    //数量確認者名称を取得
                    string suuKakuteiNm = this.GetKakuteishaName(cellValue);

                    if (string.IsNullOrEmpty(suuKakuteiNm))
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["SUU_KAKUTEI_NAME"].Value = null;
                        msgLogic.MessageBoxShow("E020", "数量確認者コード");
                        checkErrFlg = true;
                    }
                    else
                    {
                        //名称列を設定
                        this.Rows[e.RowIndex].Cells["SUU_KAKUTEI_NAME"].Value = suuKakuteiNm;
                    }
                }
                else
                {
                    //名称列クリア
                    this.Rows[e.RowIndex].Cells["SUU_KAKUTEI_NAME"].Value = null;
                }
            }

            //運搬方法コード
            if ("UPN_WAY_CODE".Equals(cell.GetName()))
            {
                if (!string.IsNullOrEmpty(cellValue))
                {
                    //運搬方法名称を取得
                    string suuKakuteiNm = this.GetUpnWayName();

                    if (suuKakuteiNm == null || string.IsNullOrEmpty(suuKakuteiNm))
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["UPN_WAY_NAME"].Value = null;
                        //if (suuKakuteiNm == null) msgLogic.MessageBoxShow("E020", "運搬方法名コード");
                        //if (string.IsNullOrEmpty(suuKakuteiNm)) msgLogic.MessageBoxShow("E031", "運搬方法名コード");
                        checkErrFlg = true;
                    }
                    else
                    {
                        //名称列クリア
                        this.Rows[e.RowIndex].Cells["UPN_WAY_NAME"].Value = suuKakuteiNm;
                    }
                }
            }

            DgvCustomTextBoxCell c = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;
            if (c != null)
            {
                if (c.ReadOnly == false && c.OwningColumn.Name != "KANSAN_SUU")
                {
                    //フォマット未設定の場合、禁則文字チェックを行う
                    if (string.IsNullOrEmpty(c.CustomFormatSetting))
                    {
                        object tmpobj = c.EditedFormattedValue;
                        if (tmpobj != null)
                        {
                            if (this.KinsokuMoziCheck(tmpobj.ToString()) == false)
                            {
                                msgLogic.MessageBoxShow("E071", "該当箇所");
                                checkErrFlg = true;
                            }
                        }
                    }
                }
            }

            //異常の場合
            if (checkErrFlg)
            {
                if ((this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell) != null)
                {
                    //入力エラー
                    (this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell).IsInputErrorOccured = true;
                    (this.Rows[e.RowIndex].Cells[e.ColumnIndex] 
                        as DgvCustomTextBoxCell).AutoChangeBackColorEnabled = true;
                }

                //セルがフォーカスを受け取ったときに編集が開始されます
                this.EditMode = DataGridViewEditMode.EditOnEnter;
                e.Cancel = true;
                this.isInputError = true;
                return;
            }
            else
            {
                this.isInputError = false;
            }
            //換算数量と減容後数量計算処理を行う
            
            if (cell != null)
            {
                if ("HAIKI_SHURUI_CD".Equals(cell.GetName()) || "HAIKI_NAME_CD".Equals(cell.GetName()) ||
                "NISUGATA_CD".Equals(cell.GetName()) || "SUU_KAKUTEI_CODE".Equals(cell.GetName()) ||
                "HAIKI_SUU".Equals(cell.GetName()) || "UNIT_CD".Equals(cell.GetName()))
                {
                   
                    ////換算値の計算が前提条件を確認
                    //bool bIsValid = false;
                    ////廃棄種類CDが入力必須
                    //bIsValid = (this.Rows[e.RowIndex].Cells["HAIKI_SHURUI_CD"].Value != null);
                    //if (!bIsValid)
                    //{
                    //    this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //    return;
                    //}
                    ////廃棄物名称CDが入力必須
                    //bIsValid = (this.Rows[e.RowIndex].Cells["HAIKI_NAME_CD"].Value != null);
                    //if (!bIsValid)
                    //{
                    //    this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //    return;
                    //}
                    ////荷姿CDが入力必須
                    //bIsValid = (this.Rows[e.RowIndex].Cells["NISUGATA_CD"].Value != null);
                    //if (!bIsValid)
                    //{
                    //    this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //    return;
                    //}

                    ////数量確定者CDが入力必須
                    //bIsValid = (this.Rows[e.RowIndex].Cells["SUU_KAKUTEI_CODE"].Value != null);
                    //if (!bIsValid)
                    //{
                    //    this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //    return;
                    //}
                    ////廃棄物数量
                    //bIsValid = (this.Rows[e.RowIndex].Cells["HAIKI_SUU"].Value != null);
                    //if (bIsValid && SqlDecimal.Parse(this.Rows[e.RowIndex].Cells["HAIKI_SUU"].Value.ToString().Replace(",", "")) != 0)
                    //{
                    //    bIsValid = true;
                    //}
                    //else
                    //{
                    //    this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //    return;
                    //}
                    ////廃棄物数量の単位
                    ////確定者が排出事業者場合、廃棄物数量の単位が入力必須
                    //if (this.Rows[e.RowIndex].Cells["SUU_KAKUTEI_CODE"].Value.ToString() == "01")
                    //{
                    //    bIsValid = (this.Rows[e.RowIndex].Cells["UNIT_CD"].Value != null);
                    //    if (!bIsValid)
                    //    {
                    //        this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //        this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //        return;
                    //    }
                    //}

                    ////チェックNG場合、処理終了
                    //if (bIsValid == false)
                    //{
                    //    //チェックNG場合換算数量と減容後数量をクリアする
                    //    this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                    //    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                    //    return;
                    //}
                    if (!(prevCellValueDictionary[columnName] == cellValue && !this.isInputError))
                    {
                        // 前回値チェック
                        //計算を行う
                        this.KansanGenyou(e);
                    }
                }
                
             }
        }

        /// <summary>
        /// 数量確定者名称取得処理
        /// </summary>
        /// <param name="KakuteiCD">確定者コード</param>
        /// <returns>確定者名</returns>
        private string GetKakuteishaName(string KakuteiCD)
        {
            string Name = string.Empty;
            if (KakuteiCD == "01") return "排出事業者";
            if (KakuteiCD == "02") return "処分業者";
            if (KakuteiCD == "03") return "収集運搬業者（区間1）";
            if (KakuteiCD == "04") return "収集運搬業者（区間2）";
            if (KakuteiCD == "05") return "収集運搬業者（区間3）";
            if (KakuteiCD == "06") return "収集運搬業者（区間4）";
            if (KakuteiCD == "07") return "収集運搬業者（区間5）";

            return Name;
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

        /// <summary>
        /// 運搬方法名称を取得
        /// </summary>
        private string GetUpnWayName()
        {
            //コントロール内のバンドの相対位置を取得します
            int rowIndex = this.CurrentRow.Index;

            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();
            DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                        
            //運搬方法コード
            string unpanHouhouCd = Convert.ToString(this.Rows[rowIndex].Cells["UPN_WAY_CODE"].Value);

            //運搬方法名称を取得
            return DsMasterLogic.GetCodeNameFromMasterInfo(WINDOW_ID.M_UNPAN_HOUHOU, unpanHouhouCd);

        }
        
        /// <summary>
        /// DBデータを取得
        /// </summary>
        private string GetDbValue(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 換算値と減容後数量の計算処理
        /// </summary>
        /// <param name="e"></param>
        public void KansanGenyou(DataGridViewCellValidatingEventArgs e)
        {
            SqlDecimal kansan_suu = 0;
            ManifestoLogic maniLogic = new ManifestoLogic();
            if (this.Myform.Logic.GetKansan_suu(ref kansan_suu))
            {
                kansan_suu = maniLogic.Round((decimal)kansan_suu, SystemProperty.Format.ManifestSuuryou);
                // format:#,###の場合に、0の時に空白表示されない問題が発生。暫定対策としてint型でValueで上書く。なぜかSqlDecimal型だと0.0の時には空白表示されない。(標準版チケット#4182)
                this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = (decimal)kansan_suu;
                SqlDecimal genyou_suu = 0;
                bool catchErr = false;
                var refgenyou =this.Myform.Logic.GetGenYougou_suu(kansan_suu, ref genyou_suu, CurrentCell.RowIndex, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (refgenyou)
                {
                    genyou_suu = maniLogic.Round((decimal)genyou_suu, SystemProperty.Format.ManifestSuuryou);
                    // format:#,###の場合に、0の時に空白表示されない問題が発生。暫定対策としてint型でValueで上書く。なぜかSqlDecimal型だと0.0の時には空白表示されない。(標準版チケット#4182)
                    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = (decimal)genyou_suu;
                }
                else
                {
                    this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
                }
            }
            else
            {
                this.Rows[e.RowIndex].Cells["KANSAN_SUU"].Value = null;
                this.Rows[e.RowIndex].Cells["GENNYOU_SUU"].Value = null;
            }
        }
        /// <summary>
        /// CreateControlイベントハンドラ
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.EditMode = DataGridViewEditMode.EditOnEnter;
        }
        /// <summary>
        /// 加入者番号Cell変更イベント[事業場情報クリアするため]
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
            //セルの親行のインデックスを取得する
            int rowIndex = e.RowIndex;
            //セールを取得する
            if (e.RowIndex < 0)
            {
                return;
            }
            var cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;
            if(cell==null){
                return;
            }
            if (string.IsNullOrEmpty(cell.Name)) cell.Name =
                                ((System.Windows.Forms.DataGridViewCell)(cell)).OwningColumn.Name;
            //事業場CD関連加入者番号を比較し、違う場合、事業場情報クリアする
            if (("Sbn_KanyushaCD").Equals(cell.GetName()))
            {
                //現場情報をクリアする
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_CD"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_NAME"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_POST"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_TEL"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_ADDRESS"].Value = null;
                //this.Rows[rowIndex].Cells["GenbaCD_KayushaCD"].Value = null;
            }
            if (("LAST_SBN_GYOUSHA_KanyushaCD").Equals(cell.GetName()))
            {
               
                //現場情報をクリアする
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_CD"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_NAME"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_POST"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_ADDRESS"].Value = null;
                this.Rows[rowIndex].Cells["LAST_SBN_JOU_JISEKI_TEL"].Value = null;
                //this.Rows[rowIndex].Cells["GenbaOK_Jiseki_kanyushaCD"].Value = null;
            }

            if (("Unpansaki_KanyushaCD").Equals(cell.GetName()))
            {
                //現場情報をクリアする
                this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_CD"].Value = null;
                this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"].Value = null;
                this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"].Value = null;
                //this.Rows[rowIndex].Cells["UpnsakiGenbaOK_KanyushaCD"].Value = null;
            }
            //収集運搬業者の加入者番号変更時、運搬担当者と車輌情報クリアする
            if (("UPN_SHA_EDI_MEMBER_ID").Equals(cell.GetName()))
            {
                //運搬担当者情報をクリアする
                this.Rows[rowIndex].Cells["UNPANTAN_CD"].Value = null;
                this.Rows[rowIndex].Cells["UPN_TAN_NAME"].Value = null;
                //車輌情報クリアする
                this.Rows[rowIndex].Cells["SHARYOU_CD"].Value = null;
                this.Rows[rowIndex].Cells["CAR_NAME"].Value = null;
                //報告記載の運搬/報告担当者/車輌情報クリアする[画面]
                this.Myform.cantxt_UnpanTantoushaCd.Text = string.Empty;
                this.Myform.ctxt_UnpanTantoushaName.Text = string.Empty;
                this.Myform.cantxt_HoukokuTantoushaCD.Text = string.Empty;
                this.Myform.ctxt_HoukokuTantoushaName.Text = string.Empty;
                this.Myform.cantxt_SyaryoNo.Text = string.Empty;
                this.Myform.ctxt_UnpanSyaryoName.Text = string.Empty;
                if (this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value != null)
                {
                    this.Myform.Hidden_UnpanGyoushaCD.Text = this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value.ToString();
                }
                else
                {
                    this.Myform.Hidden_UnpanGyoushaCD.Text = string.Empty;
                }
                //報告記載の運搬/報告担当者クリアする[DTO]
                UnpanHoukokuDataDTOCls UnpanDto = this.Rows[rowIndex].Tag as UnpanHoukokuDataDTOCls;
                if (UnpanDto!=null)
                {
                    UnpanDto.cantxt_UnpanTantoushaCd = string.Empty;
                    UnpanDto.ctxt_UnpanTantoushaName = string.Empty;
                    UnpanDto.cantxt_HoukokuTantoushaCD = string.Empty;
                    UnpanDto.ctxt_HoukokuTantoushaName = string.Empty;
                    UnpanDto.cantxt_SyaryoNo = string.Empty;
                    UnpanDto.ctxt_UnpanSyaryoName = string.Empty;
                }
                //最新業者CDより運搬報告の車輌検索条件を設定する
                r_framework.Dto.JoinMethodDto methodDto = this.Myform.cantxt_SyaryoNo.popupWindowSetting[0];
                r_framework.Dto.SearchConditionsDto searchDto = methodDto.SearchCondition[0];
                if (methodDto != null && searchDto !=null)
                {
                    if (this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value != null)
                    {
                        searchDto.Value = this.Rows[rowIndex].Cells["UPN_SHA_CD"].Value.ToString();
                    }
                }
                

            }
            

        }
        #region コントロールのFind
        /// <summary>
        /// コントロールのFind
        /// </summary>
        /// <param name="target">コントロール名</param>
        /// <returns></returns>
        private Control FindControl(string target)
        {
            Control ctl = null;
            ctl = FindControl(this.Parent, target);
            if (ctl == null)
            {
                if (this.Parent == null) return ctl;
                ctl = FindControl(this.Parent.Parent, target);
                if (ctl == null)
                {
                    if (this.Parent.Parent == null) return ctl;
                    ctl = FindControl(this.Parent.Parent.Parent, target);
                    if (ctl == null)
                    {
                        if (this.Parent.Parent.Parent == null) return ctl;
                        ctl = FindControl(this.Parent.Parent.Parent.Parent, target);
                        if (ctl == null)
                        {
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
            if (root == null)
            {
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
        #endregion

        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="insertVal">登録項目</param>
        public bool KinsokuMoziCheck(string insertVal)
        {
            Validator v = new Validator();

            if (!v.isJWNetValidShiftJisCharForSign(insertVal))
            {
                return false;
            }
            return true;
        }

        #region 運搬先事業場のポップアップ設定
        /// <summary>
        /// 運搬先事業場用のポップアップの設定をする(Validatingチェック時用)
        /// </summary>
        /// <param name="cell">運搬先事業場CDのCell</param>
        /// <param name="rowIndex">現在選択行のindex</param>
        private void SetPopupSettingForUnpanSakiGenbaCd(ICustomDataGridControl cell, int rowIndex)
        {
            DenshiMasterDataLogic DsMasterLogic = new DenshiMasterDataLogic();

            //検索画面のタイトルを設定
            cell.PopupDataHeaderTitle = new string[] { "加入者番号", "業者CD", "事業者名", "事業場CD", "現場CD", "事業場名", "郵便番号", "都道府県", "住所", "電話番号" };

            cell.PopupGetMasterField =
                "EDI_MEMBER_ID,GYOUSHA_CD,JIGYOUSHA_NAME,JIGYOUJOU_CD,GENBA_CD,JIGYOUJOU_NAME,JIGYOUJOU_POST,TODOFUKEN_NAME,DISP_JIGYOUJOU_ADDRESS,JIGYOUJOU_TEL";

            // PopupDataSourceは別途設定

            //加入者番号(非表示列)
            DgvCustomTextBoxCell cell1 = new DgvCustomTextBoxCell();
            //名称
            DgvCustomTextBoxCell cell2 = new DgvCustomTextBoxCell();
            //事業場番号(非表示列)
            DgvCustomTextBoxCell cell3 = new DgvCustomTextBoxCell();

            //加入者番号
            cell1 = this.Rows[rowIndex].Cells["Unpansaki_KanyushaCD"] as DgvCustomTextBoxCell;
            if (string.IsNullOrEmpty(cell1.Name)) cell1.Name = ((System.Windows.Forms.DataGridViewCell)(cell1)).OwningColumn.Name;
            //名称
            cell2 = this.Rows[rowIndex].Cells["UNPANSAKI_GENBA_NAME"] as DgvCustomTextBoxCell;
            if (string.IsNullOrEmpty(cell2.Name)) cell2.Name = ((System.Windows.Forms.DataGridViewCell)(cell2)).OwningColumn.Name;
            //事業場番号
            cell3 = this.Rows[rowIndex].Cells["UNPANSAKI_JIGYOUJOU_CD"] as DgvCustomTextBoxCell;
            if (string.IsNullOrEmpty(cell3.Name)) cell3.Name = ((System.Windows.Forms.DataGridViewCell)(cell3)).OwningColumn.Name;

            //値設定先コントロールを設定する
            cell.ReturnControls = new[] { cell1, null, null, cell3, this.CurrentCell as ICustomDataGridControl, cell2, null, null, null };
        }

        /// <summary>
        /// ポップアップ表示用のDataTableを作成
        /// </summary>
        /// <param name="dt">元データ</param>
        /// <param name="cell">PopupGetMasterField</param>
        /// <returns></returns>
        private DataTable CreateDataSourceForUpnSakiJigyoujouPopup(DataTable dt, string getMasterField)
        {
            DataTable returnVal = new DataTable();

            // 表示Column作成
            var columnNames = getMasterField.Split(',');
            foreach (var columnName in columnNames)
            {
                string tempColName = columnName.Trim();
                var dtCol = dt.Columns[columnName];
                if (dtCol != null)
                {
                    returnVal.Columns.Add(dtCol.ColumnName, dtCol.DataType);
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                returnVal.Rows.Add(returnVal.Columns.OfType<DataColumn>().Select(s => row[s.ColumnName]).ToArray());
            }

            return returnVal;
        }
        #endregion
    }
}
