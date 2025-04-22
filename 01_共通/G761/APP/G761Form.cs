using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.DenpyouRireki.Logic;
using System.ComponentModel;
using Shougun.Function.ShougunCSCommon.Dto;

namespace Shougun.Core.Common.DenpyouRireki.APP
{
    public partial class G761Form : SuperForm
    {
        //前取引先CD 比較用
        private string maeTorihikisakiCd = "";

        //前業者CD 比較用
        private string maeGyoushaCd = "";

        //前現場CD 比較用
        private string maeGenbaCd = "";

        public string maeUpnGyoushaCd = "";

        //初期表示フラグ
        private bool FormShowFlg = false;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G761Logic logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public string TorihikisakiCd = "";
        public string GyoushaCd = "";
        public string GenbaCd = "";
        public string UpnGyoushaCd = "";
        public string SharyouCd = "";
        public string SharyouName = "";
        public string FormId = "";

        public DenpyouRirekiDTOClass DenpyouRirekiDTO;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G761Form()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G761Logic(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        public G761Form(DenpyouRirekiDTOClass dto)
        {
            LogUtility.DebugMethodStart(dto);
            try
            {
                this.InitializeComponent();

                this.TorihikisakiCd = dto.TorihikisakiCd;
                this.GyoushaCd = dto.GyoushaCd;
                this.GenbaCd = dto.GenbaCd;
                this.UpnGyoushaCd = dto.UnpanGyoushaCd;
                this.SharyouCd = dto.SharyouCd;
                this.SharyouName = dto.SharyouName;
                this.FormId = dto.FormId;
                this.DenpyouRirekiDTO = dto;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G761Logic(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);

                this.Ukeire_Denpyou.AutoGenerateColumns = false;
                this.Shukka_Denpyou.AutoGenerateColumns = false;
                this.UriageShiharai_Denpyou.AutoGenerateColumns = false;
                this.UriageShiharai_Meisai.AutoGenerateColumns = false;

                //初期処理
                this.logic.WindowInit();

                if (this.FormId == "G721" || this.FormId == "G051")
                {
                    this.tableUkeire.Visible = true;
                    this.tableShukka.Visible = false;
                    this.tableUriageShiharai.Visible = false;
                    //this.logic.ExecuteAlignmentForDetailUkeire();
                }
                else if (this.FormId == "G722" || this.FormId == "G053")
                {
                    this.tableUkeire.Visible = false;
                    this.tableShukka.Visible = true;
                    this.tableUriageShiharai.Visible = false;
                }
                else
                {
                    this.tableUkeire.Visible = false;
                    this.tableShukka.Visible = false;
                    this.tableUriageShiharai.Visible = true;
                }
                
                //取引先CD
                if (!"".Equals(this.TorihikisakiCd) && null != this.TorihikisakiCd)
                {
                    this.TORIHIKISAKI_CD.Text = this.TorihikisakiCd;
                    this.TorihikisakiFORTorihikisakiCd_Select();
                }
                //業者CD
                if (!"".Equals(this.GyoushaCd) && null != this.GyoushaCd)
                {
                    this.GYOUSHA_CD.Text = this.GyoushaCd;
                    this.GyoushaFORGyoushaCD_Select();
                }
                //現場CD
                if (!"".Equals(this.GenbaCd) && null != this.GenbaCd)
                {
                    this.GENBA_CD.Text = this.GenbaCd;
                    this.GenbaFORGenbaCD_Select();
                }
                if (!"".Equals(this.UpnGyoushaCd) && null != this.UpnGyoushaCd)
                {
                    this.UPN_GYOUSHA_CD.Text = this.UpnGyoushaCd;
                    this.UpnGyoushaFORGyoushaCD_Select();
                }
                this.SHARYOU_CD.Text = this.SharyouCd;
                this.SHARYOU_NAME_RYAKU.Text = this.SharyouName;
                //初期の条件を保存
                this.logic.TorihikisakiCD = this.TorihikisakiCd;
                this.logic.GyoushaCD = this.GyoushaCd;
                this.logic.GenbaCD = this.GenbaCd;

                //内容を記憶,比較用
                this.maeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
                this.maeGenbaCd = this.GENBA_CD.Text;
                this.maeUpnGyoushaCd = this.UPN_GYOUSHA_CD.Text;

                //初期表示検索(伝票ー受付)
                this.UketsukeSearch(null, e);

                //初期表示フラグ
                this.FormShowFlg = true;

                //取引先CDにフォーカス
                this.TORIHIKISAKI_CD.Focus();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var parentForm = (BusinessBaseForm)this.Parent;

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            this.tableShukka.Top = this.tableUriageShiharai.Top = this.tableUkeire.Top;
            this.tableShukka.Left = this.tableUriageShiharai.Left = this.tableUkeire.Left;

            if (this.FormId == "G721" || this.FormId == "G051")
            {
                this.logic.adjustColumnSize(this.Ukeire_Denpyou);
                this.Ukeire_Denpyou.Columns["UKEIRE_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            else if (this.FormId == "G722" || this.FormId == "G053")
            {
                this.logic.adjustColumnSize(this.Shukka_Denpyou);
                this.Shukka_Denpyou.Columns["SHUKKA_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            else
            {
                this.logic.adjustColumnSize(this.UriageShiharai_Denpyou);
                this.UriageShiharai_Denpyou.Columns["URIAGESIHARAI_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.logic.adjustColumnSize(this.UriageShiharai_Meisai);
                this.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_URIAGESIHARAI_DATE"].Width = 110;
            }

            base.OnShown(e);
        }

        /// <summary>
        /// 条件ｸﾘｱボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Conditions_Clear_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 検索条件をクリア
                this.logic.searchConfirionsClear();

                //取引先CD
                if (!"".Equals(this.TorihikisakiCd) && null != this.TorihikisakiCd)
                {
                    this.TORIHIKISAKI_CD.Text = this.TorihikisakiCd;
                    this.TorihikisakiFORTorihikisakiCd_Select();
                }
                //業者CD
                if (!"".Equals(this.GyoushaCd) && null != this.GyoushaCd)
                {
                    this.GYOUSHA_CD.Text = this.GyoushaCd;
                    this.GyoushaFORGyoushaCD_Select();
                }
                //現場CD
                if (!"".Equals(this.GenbaCd) && null != this.GenbaCd)
                {
                    this.GENBA_CD.Text = this.GenbaCd;
                    this.GenbaFORGenbaCD_Select();
                }
                if (!"".Equals(this.UpnGyoushaCd) && null != this.UpnGyoushaCd)
                {
                    this.UPN_GYOUSHA_CD.Text = this.UpnGyoushaCd;
                    this.UpnGyoushaFORGyoushaCD_Select();
                }
                this.SHARYOU_CD.Text = this.SharyouCd;
                this.SHARYOU_NAME_RYAKU.Text = this.SharyouName;

                this.logic.HeaderInit();

                //初期の条件を保存
                this.logic.TorihikisakiCD = this.TorihikisakiCd;
                this.logic.GyoushaCD = this.GyoushaCd;
                this.logic.GenbaCD = this.GenbaCd;

                //内容を記憶,比較用
                this.maeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
                this.maeGenbaCd = this.GENBA_CD.Text;
                this.maeUpnGyoushaCd = this.UPN_GYOUSHA_CD.Text;

                this.Ukeire_Denpyou.DataSource = null;
                this.MultiRow_UkeireMeisai.DataSource = null;

                this.Shukka_Denpyou.DataSource = null;
                this.MultiRow_ShukkaMeisai.DataSource = null;

                this.UriageShiharai_Denpyou.DataSource = null;
                this.UriageShiharai_Meisai.DataSource = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UketsukeSearch(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.controlUtil.GetAllControls(this.logic.header));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.RegistErrorFlag)
                {
                    this.logic.SetErrorFocus();
                    return;
                }
                //Check denpyou date
                if (!this.logic.DateCheck())
                {
                    return;
                }
                //条件の取引先CD,業者CD,現場CD 保存
                //取引先CD
                this.logic.TorihikisakiCD = this.TORIHIKISAKI_CD.Text;
                //業者CD
                this.logic.GyoushaCD = this.GYOUSHA_CD.Text;
                //現場CD
                this.logic.GenbaCD = this.GENBA_CD.Text;
                //拠点CD
                if (!string.IsNullOrEmpty(this.logic.header.KYOTEN_CD.Text))
                {
                    this.logic.KyotenCD = int.Parse(this.logic.header.KYOTEN_CD.Text).ToString();
                }
                //From date
                if (this.logic.header.DATE_FROM.Value != null)
                {
                    this.logic.FromDate = ((DateTime)this.logic.header.DATE_FROM.Value).ToString("yyyy-MM-dd");
                }
                //To date
                if (this.logic.header.DATE_TO.Value != null)
                {
                    this.logic.ToDate = ((DateTime)this.logic.header.DATE_TO.Value).ToString("yyyy-MM-dd");
                }

                this.logic.UpnGyoushaCD = this.UPN_GYOUSHA_CD.Text;
                this.logic.SharyouCD = this.SHARYOU_CD.Text;
                this.logic.SharyouName = this.SHARYOU_NAME_RYAKU.Text;

                //フォーム起動するか
                if (this.FormShowFlg)
                {
                    //再検索時、各一覧の明細を初期化
                    this.logic.MeisaiInit();
                }

                if (this.FormId == "G721" || this.FormId == "G051")
                {
                    UkeireDate_Select();
                }
                else if (this.FormId == "G722" || this.FormId == "G053")
                {
                    ShukkaDate_Select();
                }
                else
                {
                    UrShDate_Select();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F9 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal void Copy_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string copyNo = "";

            if ("G721" == this.DenpyouRirekiDTO.FormId || "G051" == this.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.Ukeire_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.Ukeire_Denpyou.CurrentRow.Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();//受入番号
                
            }
            else if ("G722" == this.DenpyouRirekiDTO.FormId || "G053" == this.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.Shukka_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.Shukka_Denpyou.CurrentRow.Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();//出荷番号
                
            }
            else if ("G054" == this.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.UriageShiharai_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.UriageShiharai_Denpyou.CurrentRow.Cells["URIAGESIHARAI_NO"].Value.ToString();//売上/支払番号
                
            }

            this.DenpyouRirekiDTO.DenpyouNumber = copyNo;
            this.DenpyouRirekiDTO.DetailSystemId = "";
            this.DenpyouRirekiDTO.CopyMode = "1";

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        internal void DetailCopy_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string copyNo = "";
            string detailSystemId = "";

            if ("G721" == this.DenpyouRirekiDTO.FormId || "G051" == this.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.Ukeire_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.Ukeire_Denpyou.CurrentRow.Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();//受入番号

                if (this.MultiRow_UkeireMeisai.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                detailSystemId = this.MultiRow_UkeireMeisai.CurrentRow.Cells["UKEIRE_DETAIL_SYSTEM_ID"].Value.ToString();
            }
            else if ("G722" == this.DenpyouRirekiDTO.FormId || "G053" == this.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.Shukka_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.Shukka_Denpyou.CurrentRow.Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();//出荷番号

                if (this.MultiRow_ShukkaMeisai.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                detailSystemId = this.MultiRow_ShukkaMeisai.CurrentRow.Cells["SHUKKA_DETAIL_SYSTEM_ID"].Value.ToString();
            }
            else if ("G054" == this.DenpyouRirekiDTO.FormId)
            {
                //一覧で選択していない場合
                if (this.UriageShiharai_Denpyou.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                copyNo = this.UriageShiharai_Denpyou.CurrentRow.Cells["URIAGESIHARAI_NO"].Value.ToString();//売上/支払番号

                if (this.UriageShiharai_Meisai.CurrentRow == null)
                {
                    msgLogic.MessageBoxShow("E315");
                    return;
                }
                detailSystemId = this.UriageShiharai_Meisai.CurrentRow.Cells["URIAGESIHARAI_MEISAI_DETAIL_SYSTEM_ID"].Value.ToString();
            }

            this.DenpyouRirekiDTO.DenpyouNumber = copyNo;
            this.DenpyouRirekiDTO.DetailSystemId = detailSystemId;
            this.DenpyouRirekiDTO.CopyMode = "2";

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// 取引先CDテキストボックスのフォーカスが失い時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.TORIHIKISAKI_CD.Text && !"".Equals(this.TORIHIKISAKI_CD.Text))
                {
                    this.TORIHIKISAKI_CD.Text = this.TORIHIKISAKI_CD.Text.PadLeft(6, '0');
                }

                //検索
                this.TorihikisakiFORTorihikisakiCd_Select();

                //取引先CDテキストボックスの内容を記憶,比較用
                this.maeTorihikisakiCd = this.TORIHIKISAKI_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDにより取引先情報を取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TorihikisakiFORTorihikisakiCd_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                string torihikisaki_cd = this.TORIHIKISAKI_CD.Text;

                if (null == torihikisaki_cd || "".Equals(torihikisaki_cd) || DBNull.Value.Equals(torihikisaki_cd))
                {
                    //取引先、業者、現場が空にする
                    this.logic.TorihikisakiCrear();
                }
                else
                {
                    //取引先マスタ検索
                    if (this.logic.TorihikisakiSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "取引先");
                        //業者、現場を空欄にする
                        this.logic.TorihikisakiCrear();
                        //フォーカス
                        this.TORIHIKISAKI_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }
                    //表示
                    this.logic.TorihikisakiSet();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteTorihikisakiCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //取引先マスタ検索
                if (this.logic.TorihikisakiSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "取引先");
                    //業者、現場を空欄にする
                    this.logic.TorihikisakiCrear();
                    //フォーカス
                    this.TORIHIKISAKI_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }
                //表示
                this.logic.TorihikisakiSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDテキストボックスのフォーカスが失い時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.GYOUSHA_CD.Text && !"".Equals(this.GYOUSHA_CD.Text))
                {
                    this.GYOUSHA_CD.Text = this.GYOUSHA_CD.Text.PadLeft(6, '0');
                }
                //検索
                this.GyoushaFORGyoushaCD_Select();

                //業者CDテキストボックスの内容を記憶,比較用
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDにより業者情報を取得
        /// </summary>
        public void GyoushaFORGyoushaCD_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //取引先CD
                //string torihikisaki_cd = this.TORIHIKISAKI_CD.Text;
                //業者CD
                string gyousha_cd = this.GYOUSHA_CD.Text;

                if (null == gyousha_cd || "".Equals(gyousha_cd) || DBNull.Value.Equals(gyousha_cd))
                {
                    //業者、現場が空にする
                    this.logic.GyoushaClear();
                    this.logic.GenbaClear();
                }
                else
                {
                    //業者マスタ検索
                    if (this.logic.GyoushaSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "業者");
                        //業者、現場を空欄にする
                        this.logic.GyoushaClear();
                        this.logic.GenbaClear();
                        //フォーカス
                        this.GYOUSHA_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //表示
                    this.logic.GyoushaSet();

                    ////業者が変わったら、現場のクリア
                    if (!this.maeGyoushaCd.Equals(this.GYOUSHA_CD.Text))
                    {
                        this.logic.GenbaClear();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteGyoushaCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //業者マスタ検索
                if (this.logic.GyoushaSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "業者");
                    //業者、現場を空欄にする
                    this.logic.GyoushaClear();
                    this.logic.GenbaClear();
                    //フォーカス
                    this.GYOUSHA_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }

                //表示
                this.logic.GyoushaSet();

                ////業者が変わったら、現場のクリア
                if (!this.maeGyoushaCd.Equals(this.GYOUSHA_CD.Text))
                {
                    this.logic.GenbaClear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDテキストボックスのフォーカスが失い時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.GENBA_CD.Text && !"".Equals(this.GENBA_CD.Text))
                {
                    this.GENBA_CD.Text = this.GENBA_CD.Text.PadLeft(6, '0').ToUpper();
                }

                if (string.IsNullOrEmpty(this.GENBA_CD.Text))
                {
                    this.logic.GenbaClear();
                    return;
                }
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E051", "業者");
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_CD.Focus();
                    return;
                }
                
                this.GenbaFORGenbaCD_Select();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDにより現場情報を取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GenbaFORGenbaCD_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //現場CD
                string genba_cd = this.GENBA_CD.Text;
                
                if (null == genba_cd || "".Equals(genba_cd) || DBNull.Value.Equals(genba_cd))
                {
                    //現場が空にする
                    this.logic.GenbaClear();
                }
                else
                {
                    //現場マスタ検索
                    if (this.logic.GenbaSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "現場");
                        //業者、現場を空欄にする
                        this.logic.GenbaClear();
                        //フォーカス
                        this.GENBA_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //表示
                    this.logic.GenbaSet();

                }
                
                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteGenbaCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //現場マスタ検索
                if (this.logic.GenbaSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "現場");
                    //業者、現場を空欄にする
                    this.logic.GenbaClear();
                    //フォーカス
                    this.GENBA_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }

                //表示
                this.logic.GenbaSet();

                this.maeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受入タブー情報を取得
        /// </summary>
        private int UkeireDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                this.Ukeire_Denpyou.IsBrowsePurpose = false;
                //DB検索 :受入
                if (this.logic.UkeireSearch() == 0)
                {
                    this.Ukeire_Denpyou.DataSource = this.logic.UkeireSearchResult;

                    this.Ukeire_Denpyou.IsBrowsePurpose = true;

                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.UkeireSearchResult;
                table.BeginLoadData();
                this.Ukeire_Denpyou.DataSource = table;
                this.Ukeire_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Ukeire_Denpyou);
                this.Ukeire_Denpyou.Columns["UKEIRE_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int UkeireDetailDate_Select(long systemId, int seq)
        {
            try
            {
                //DB検索 :受入明細
                this.MultiRow_UkeireMeisai.IsBrowsePurpose = false;
                if (this.logic.UkeireDetailSearch(systemId, seq) == 0)
                {
                    this.MultiRow_UkeireMeisai.DataSource = this.logic.UkeireDetailSearchResult;

                    this.MultiRow_UkeireMeisai.IsBrowsePurpose = true;
                    return 0;
                }
                //バインド
                var table = this.logic.UkeireDetailSearchResult;
                table.BeginLoadData();
                this.MultiRow_UkeireMeisai.DataSource = table;
                this.MultiRow_UkeireMeisai.IsBrowsePurpose = true;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_UkeireMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return 1;
        }

        /// <summary>
        /// 出荷タブー情報を取得
        /// </summary>
        private int ShukkaDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //DB検索 :出荷
                this.Shukka_Denpyou.IsBrowsePurpose = false;
                if (this.logic.ShukkaSearch() == 0)
                {
                    this.Shukka_Denpyou.DataSource = this.logic.ShukkaSearchResult;

                    this.Shukka_Denpyou.IsBrowsePurpose = true;

                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                
                //バインド
                var table = this.logic.ShukkaSearchResult;
                table.BeginLoadData();
                this.Shukka_Denpyou.DataSource = table;
                this.Shukka_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.Shukka_Denpyou);
                this.Shukka_Denpyou.Columns["SHUKKA_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 出荷明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int ShukkaDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :受入明細
                this.MultiRow_ShukkaMeisai.IsBrowsePurpose = false;
                if (this.logic.ShukkaDetailSearch(systemId, seq) == 0)
                {
                    this.MultiRow_ShukkaMeisai.DataSource = this.logic.ShukkaDetailSearchResult;

                    this.MultiRow_ShukkaMeisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.ShukkaDetailSearchResult;
                table.BeginLoadData();
                this.MultiRow_ShukkaMeisai.DataSource = table;
                this.MultiRow_ShukkaMeisai.IsBrowsePurpose = true;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.All;
                this.MultiRow_ShukkaMeisai.HorizontalAutoSizeMode = GrapeCity.Win.MultiRow.HorizontalAutoSizeMode.None;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払タブー情報を取得
        /// </summary>
        private int UrShDate_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //すでに検索したら、再検索をしない
                this.UriageShiharai_Denpyou.IsBrowsePurpose = false;
                //DB検索 :売上/支払
                if (this.logic.UrShSearch() == 0)
                {
                    this.UriageShiharai_Denpyou.DataSource = this.logic.UrShSearchResult;

                    this.UriageShiharai_Denpyou.IsBrowsePurpose = true;

                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");

                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.UrShSearchResult;
                table.BeginLoadData();
                this.UriageShiharai_Denpyou.DataSource = table;
                this.UriageShiharai_Denpyou.IsBrowsePurpose = true;
                //20211228 Thanh 158919 s
                this.logic.adjustColumnSize(this.UriageShiharai_Denpyou);
                this.UriageShiharai_Denpyou.Columns["URIAGESIHARAI_DENPYOU_DENPYOU_DATE"].Width = 110;
                this.logic.adjustColumnSize(this.UriageShiharai_Meisai);
                this.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_URIAGESIHARAI_DATE"].Width = 110;
                //20211228 Thanh 158919 e
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 売上/支払明細情報を取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        private int UrShDetailDate_Select(long systemId, int seq)
        {
            LogUtility.DebugMethodStart(systemId, seq);
            try
            {
                //DB検索 :売上/支払明細
                this.UriageShiharai_Meisai.IsBrowsePurpose = false;
                if (this.logic.UrShDetailSearch(systemId, seq) == 0)
                {
                    this.UriageShiharai_Meisai.DataSource = this.logic.UrShDetailSearchResult;

                    this.UriageShiharai_Meisai.IsBrowsePurpose = true;
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //バインド
                var table = this.logic.UrShDetailSearchResult;
                table.BeginLoadData();
                this.UriageShiharai_Meisai.DataSource = table;
                this.UriageShiharai_Meisai.IsBrowsePurpose = true;
                this.logic.adjustColumnSize(this.UriageShiharai_Meisai);
                this.UriageShiharai_Meisai.Columns["URIAGESIHARAI_MEISAI_URIAGESIHARAI_DATE"].Width = 110;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(1);

            return 1;
        }

        /// <summary>
        /// 受入伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ukeire_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.UkeireSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.UkeireSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.UkeireDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出荷伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukka_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.ShukkaSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.ShukkaSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.ShukkaDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上/支払伝票一覧の行選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UriageShiharai_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = this.logic.UrShSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
                //SEQ
                int seq = this.logic.UrShSearchResult.Rows[rowIndex].Field<int>("SEQ");
                //明細
                this.UrShDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        #region 伝票を参照で開く

        /// <summary>
        /// 受入伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ukeire_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void MultiRow_UkeireMeisai_DoubleClick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 出荷伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukka_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void MultiRow_ShukkaMeisai_DoubleClick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 売上/支払伝票を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UriageShiharai_Denpyou_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void UriageShiharai_Meisai_DoubleClick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 伝票を参照モードで開く
        /// </summary>
        private void OpenSlipByReferenceMode()
        {
            string refNo = "";
            string tabName = "";
            string denName = "";

            #region 参照モードで画面を開く(伝票)

            if ("伝票" == tabName)
            {
                if ("受入" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Ukeire_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.Ukeire_Denpyou.CurrentRow.Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();//受入番号
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G721", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                }
                else if ("出荷" == denName)
                {
                    //一覧で選択していない場合
                    if (this.Shukka_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.Shukka_Denpyou.CurrentRow.Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();//出荷番号
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                    else
                    {
                        FormManager.OpenFormWithAuth("G722", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                    }
                }
                else if ("売上/支払" == denName)
                {
                    //一覧で選択していない場合
                    if (this.UriageShiharai_Denpyou.CurrentRow == null)
                    {
                        return;
                    }
                    refNo = this.UriageShiharai_Denpyou.CurrentRow.Cells["URIAGESIHARAI_NO"].Value.ToString();//売上/支払番号

                    // 20150602 代納伝票対応(代納不具合一覧52) Start
                    T_UR_SH_ENTRY entry = new T_UR_SH_ENTRY();
                    entry.UR_SH_NUMBER = Convert.ToInt64(refNo);
                    entry.DELETE_FLG = false;
                    entry = this.logic.UrShDao.GetDataForEntity(entry).FirstOrDefault();
                    if (entry != null)
                    {
                        if (entry.DAINOU_FLG.IsNull || entry.DAINOU_FLG.IsFalse)
                        {
                            FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                        }
                        else
                        {
                            FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, refNo);//参照モードで起動
                        }
                    }
                    
                }
                
            }

            #endregion
        }

        #endregion

        public void SharyouPopupAfter()
        {
            this.maeUpnGyoushaCd = this.UPN_GYOUSHA_CD.Text;
            if (this.UPN_GYOUSHA_CD.Text != "")
            {
                if (!this.logic.ChechSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }
            }
        }

        private void SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.ChechSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void UPN_GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ゼロ埋め処理
                if (null != this.UPN_GYOUSHA_CD.Text && !"".Equals(this.UPN_GYOUSHA_CD.Text))
                {
                    this.UPN_GYOUSHA_CD.Text = this.UPN_GYOUSHA_CD.Text.PadLeft(6, '0');
                }
                //検索
                this.UpnGyoushaFORGyoushaCD_Select();

                //業者CDテキストボックスの内容を記憶,比較用
                this.maeUpnGyoushaCd = this.UPN_GYOUSHA_CD.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDにより業者情報を取得
        /// </summary>
        public void UpnGyoushaFORGyoushaCD_Select()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //業者CD
                string gyousha_cd = this.UPN_GYOUSHA_CD.Text;

                if (null == gyousha_cd || "".Equals(gyousha_cd) || DBNull.Value.Equals(gyousha_cd))
                {
                    //業者、現場が空にする
                    this.logic.UpnGyoushaClear();
                    this.logic.SharyouClear();
                }
                else
                {
                    //業者マスタ検索
                    if (this.logic.UpnGyoushaSearch() == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "運搬業者");
                        //業者、現場を空欄にする
                        this.logic.UpnGyoushaClear();
                        this.logic.SharyouClear();
                        //フォーカス
                        this.UPN_GYOUSHA_CD.Focus();

                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //表示
                    this.logic.UpnGyoushaSet();

                    ////業者が変わったら、現場のクリア
                    if (!this.maeUpnGyoushaCd.Equals(this.UPN_GYOUSHA_CD.Text))
                    {
                        this.logic.SharyouClear();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CD_PopupAfterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteUpnGyoushaCd(object sender, DialogResult rlt)
        {
            LogUtility.DebugMethodStart(sender, rlt);
            try
            {
                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //業者マスタ検索
                if (this.logic.UpnGyoushaSearch() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "運搬業者");
                    //業者、現場を空欄にする
                    this.logic.UpnGyoushaClear();
                    this.logic.SharyouClear();
                    //フォーカス
                    this.UPN_GYOUSHA_CD.Focus();

                    LogUtility.DebugMethodEnd();
                    return;
                }

                //表示
                this.logic.UpnGyoushaSet();

                ////業者が変わったら、現場のクリア
                if (!this.maeUpnGyoushaCd.Equals(this.UPN_GYOUSHA_CD.Text))
                {
                    this.logic.SharyouClear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
    }
}