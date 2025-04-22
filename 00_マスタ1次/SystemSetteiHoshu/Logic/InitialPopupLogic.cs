using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Exceptions;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.DTO;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using SystemSetteiHoshu.APP;

namespace SystemSetteiHoshu.Logic
{
    public class InitialPopupFormLogic
    {
        internal SuperEntity[] entity { get; set; }

        private InitialPopupForm form;

        internal Control[] popupViewControls { get; set; }

        private static readonly string ButtonInfoXmlPath = "SystemSetteiHoshu.Setting.PopupButtonSetting.xml";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InitialPopupFormLogic(InitialPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                // ボタンの初期化
                this.ButtonInit();

                // イベント初期化
                this.EventInit();

                CustomNumericTextBox2 initTextBox;

                // 税区分利用形態
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[0];
                this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text = initTextBox.Text;

                // 連番方法
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[1];
                this.form.SYS_RENBAN_HOUHOU_KBN.Text = initTextBox.Text;

                // マニ登録形態
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[2];
                this.form.SYS_MANI_KEITAI_KBN.Text = initTextBox.Text;

                // 在庫管理
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[5];
                this.form.ZAIKO_KANRI.Text = initTextBox.Text;

                // 評価方法
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[6];
                this.form.ZAIKO_HYOUKA_HOUHOU.Text = initTextBox.Text;

                // コンテナ管理方法
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[7];
                this.form.CONTENA_KANRI_HOUHOU.Text = initTextBox.Text;

                // 領収書連番方法
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[8];
                this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text = initTextBox.Text;

                // システム 重量設定
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[9];
                this.form.SYS_JYURYOU_FORMAT_CD.Text = initTextBox.Text;

                // システム 数量設定
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[10];
                this.form.SYS_SUURYOU_FORMAT_CD.Text = initTextBox.Text;

                // システム 単価設定
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[11];
                this.form.SYS_TANKA_FORMAT_CD.Text = initTextBox.Text;

                // 委託契約書 数量書式
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[12];
                this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text = initTextBox.Text;

                // 委託契約書 単価書式
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[13];
                this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text = initTextBox.Text;

                // マニフェスト 数量書式
                initTextBox = (CustomNumericTextBox2)this.form.ParentControls[14];
                this.form.MANIFEST_SUURYO_FORMAT_CD.Text = initTextBox.Text;

                // デジタコ 連携用会社ID
                this.form.DIGI_RENKEI_KAISYA_ID.Text = this.form.ParentControls[15].ToString();

                // デジタコ ユーザーID
                this.form.USER_ID.Text = this.form.ParentControls[16].ToString();

                // デジタコ パスワード
                this.form.PASSWORD.Text = this.form.ParentControls[17].ToString();

                // デジタコ 地点範囲半径
                this.form.CHITEN_HANI_HANKEI.Text = this.form.ParentControls[18].ToString();

                // デジタコ 翌日持越区分
                this.form.CARRY_OVER_NEXT_DAY.Text = this.form.ParentControls[19].ToString();

                // NAVITIME 会社ID
                this.form.NAVI_CORP_ID.Text = this.form.ParentControls[20].ToString();

                // NAVITIME アカウント
                this.form.NAVI_ACCOUNT.Text = this.form.ParentControls[21].ToString();

                // NAVITIME パスワード
                this.form.NAVI_PWD.Text = this.form.ParentControls[22].ToString();

                // NAVITIME 作業時間(分)
                this.form.NAVI_SAGYOU_TIME.Text = this.form.ParentControls[23].ToString();

                // NAVITIME 渋滞考慮
                this.form.NAVI_TRAFFIC.Checked = (bool)this.form.ParentControls[24];

                // NAVITIME スマートIC優先
                this.form.NAVI_SMART_IC.Checked = (bool)this.form.ParentControls[25];

                // NAVITIME 有料優先
                this.form.NAVI_TOLL.Checked = (bool)this.form.ParentControls[26];

                // NAVITIME 時間取得
                this.form.NAVI_GET_TIME.Text = this.form.ParentControls[27].ToString();

                // 初期最大表示画面数
                this.form.MAX_WINDOW_COUNT.Text = this.form.ParentControls[28].ToString();

                // 受入出荷画面サイズ
                this.form.UKEIRESHUKA_GAMEN_SIZE.Text = this.form.ParentControls[29].ToString();

                //伝票発行表示有無
                this.form.DENPYOU_HAKOU_HYOUJI.Text = this.form.ParentControls[30].ToString();

                // 最大登録容量
                this.form.MAX_INSERT_CAPACITY.Text = this.form.ParentControls[31].ToString();

                // 最大ファイルサイズ
                this.form.MAX_FILE_SIZE.Text = this.form.ParentControls[32].ToString();

                // 接続先データベース
                this.SetComboBox(this.form.ParentControls[33].ToString());

                //Set list inxs subapp database connection list
                this.SetInxsListDBComboBox(this.form.ParentControls[34].ToString());

                //CongBinh 20210712 #152799 S
                this.form.MOD_SUPPORT_KBN.Text = this.form.ParentControls[36].ToString();
                this.form.MOD_URL.Text = this.form.ParentControls[37].ToString();
                //CongBinh 20210712 #152799 E

                //QN_QUAN add 20211229 #158952 S
                this.SetComboBoxLOG(this.form.ParentControls[38].ToString());
                //QN_QUAN add 20211229 #158952 E
                //160029 S
                this.form.BARCODO_SHINKAKU.Text = this.form.ParentControls[39].ToString();
                //160029 E

                //PhuocLoc 2022/01/04 #158897, #158898 -Start
                // ｼｰｸﾚｯﾄｷｰ
                this.form.SECRET_KEY.Text = this.form.ParentControls[40].ToString();

                // 顧客ID
                this.form.CUSTOMER_ID.Text = this.form.ParentControls[41].ToString();
                //PhuocLoc 2022/01/04 #158897, #158898 -End

                // 空電プッシュ　アクセスキー
                this.form.KARADEN_ACCESS_KEY.Text = this.form.ParentControls[42].ToString();

                // 空電プッシュ　セキュリティコード
                this.form.KARADEN_SECURITY_CODE.Text = this.form.ParentControls[43].ToString();

                // 空電プッシュ　最大送信文字数
                this.form.KARADEN_MAX_WORD_COUNT.Text = this.form.ParentControls[44].ToString();

                // 楽楽明細連携　楽楽顧客コード採番方法
                this.form.SYS_RAKURAKU_CODE_NUMBERING_CD.Text = this.form.ParentControls[45].ToString();

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M261", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
        	//画面制御ボタン(F1)イベント生成
            this.form.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            
            //反映ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Reflection);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);

            // コンボボックスのイベント
            this.form.DB_FILE_CONNECT.GotFocus += new EventHandler(this.form.DatabaseFileConnection_GotFocus);
            this.form.DB_FILE_CONNECT.SelectionChangeCommitted += new EventHandler(this.form.DatabaseFileConnection_SelectionChangeCommitted);
            this.form.DB_FILE_CONNECT.Leave += new EventHandler(this.form.DatabaseFileConnection_Leave);

            //InxsSubapp DB Connection Combobox
            this.form.DB_SUBAPP_CONNECT.GotFocus += new EventHandler(this.form.DatabaseInxsSubappConnection_GotFocus);
            this.form.DB_SUBAPP_CONNECT.SelectionChangeCommitted += new EventHandler(this.form.DatabaseInxsSubappConnection_SelectionChangeCommitted);
            this.form.DB_SUBAPP_CONNECT.Leave += new EventHandler(this.form.DatabaseInxsSubappConnection_Leave);

            //QN_QUAN add 20211229 #158952 S
            this.form.DB_LOG_CONNECT.GotFocus += new EventHandler(this.form.DatabaseLOGConnection_GotFocus);
            this.form.DB_LOG_CONNECT.SelectionChangeCommitted += new EventHandler(this.form.DatabaseLOGConnection_SelectionChangeCommitted);
            this.form.DB_LOG_CONNECT.Leave += new EventHandler(this.form.DatabaseLOGConnection_Leave);
            //QN_QUAN add 20211229 #158952 E
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.SYS_RENBAN_HOUHOU_KBN.ReadOnly = true;
            this.form.rb_SYS_RENBAN_HOUHOU_KBN_1.Enabled = false;
            this.form.rb_SYS_RENBAN_HOUHOU_KBN_2.Enabled = false;
            this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.ReadOnly = true;
            this.form.rb_SYS_RECEIPT_RENBAN_HOUHOU_KBN_1.Enabled = false;
            this.form.rb_SYS_RECEIPT_RENBAN_HOUHOU_KBN_2.Enabled = false;
            this.form.SYS_MANI_KEITAI_KBN.ReadOnly = true;
            this.form.rb_SYS_MANI_KEITAI_KBN_1.Enabled = false;
            this.form.rb_SYS_MANI_KEITAI_KBN_2.Enabled = false;
            this.form.ZAIKO_KANRI.ReadOnly = true;
            this.form.rb_ZAIKO_KANRI_1.Enabled = false;
            this.form.rb_ZAIKO_KANRI_2.Enabled = false;
            this.form.ZAIKO_HYOUKA_HOUHOU.ReadOnly = true;
            this.form.rb_ZAIKO_HYOUKA_HOUHOU_1.Enabled = false;
            this.form.rb_ZAIKO_HYOUKA_HOUHOU_2.Enabled = false;
            this.form.CONTENA_KANRI_HOUHOU.ReadOnly = true;
            this.form.rb_SUURYOU_KANRHI.Enabled = false;
            this.form.rb_KOTAI_KANRI.Enabled = false;
            this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.ReadOnly = true;
            this.form.rb_SYS_ZEI_KEISAN_KBN_USE_KBN_1.Enabled = false;
            this.form.rb_SYS_ZEI_KEISAN_KBN_USE_KBN_2.Enabled = false;
            this.form.DIGI_RENKEI_KAISYA_ID.ReadOnly = true;
            this.form.CHITEN_HANI_HANKEI.ReadOnly = true;
            this.form.USER_ID.ReadOnly = true;
            this.form.PASSWORD.ReadOnly = true;
            this.form.CARRY_OVER_NEXT_DAY.ReadOnly = true;
            this.form.NAVI_CORP_ID.ReadOnly = true;
            this.form.NAVI_ACCOUNT.ReadOnly = true;
            this.form.NAVI_PWD.ReadOnly = true;
            this.form.NAVI_SAGYOU_TIME.ReadOnly = true;
            this.form.NAVI_TRAFFIC.Enabled = false;
            this.form.NAVI_SMART_IC.Enabled = false;
            this.form.NAVI_TOLL.Enabled = false;
            this.form.NAVI_GET_TIME.ReadOnly = true;
            this.form.rb_NAVI_GET_TIME_1.Enabled = false;
            this.form.rb_NAVI_GET_TIME_2.Enabled = false;
            this.form.MAX_INSERT_CAPACITY.ReadOnly = true;

            //CongBinh 20210712 #152799 S
            this.form.MOD_SUPPORT_KBN.ReadOnly = true;
            this.form.MOD_SUPPORT_KBN1.Enabled = false;
            this.form.MOD_SUPPORT_KBN2.Enabled = false;
            this.form.MOD_URL.ReadOnly = true;
            //CongBinh 20210712 #152799 E

            //PhuocLoc 2022/01/04 #158897, #158898 -Start
            this.form.SECRET_KEY.ReadOnly = true;
            this.form.CUSTOMER_ID.ReadOnly = true;
            //PhuocLoc 2022/01/04 #158897, #158898 -End

            // 空電プッシュ
            this.form.KARADEN_ACCESS_KEY.ReadOnly = true;
            this.form.KARADEN_SECURITY_CODE.ReadOnly = true;
            this.form.KARADEN_MAX_WORD_COUNT.ReadOnly = true;

            // 楽楽明細連携
            this.form.SYS_RAKURAKU_CODE_NUMBERING_CD.ReadOnly = true;

            // FunctionButton
            this.form.bt_func9.Enabled = false;
        }

        internal bool ElementDecision()
        {
            try
            {
                // 入力チェック
                if (this.inputCheck())
                {
                    return true;
                }

                CustomNumericTextBox2 decisionTextBox;

                //税区分利用形態
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[0];
                decisionTextBox.Text = this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text;

                //連番方法
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[1];
                decisionTextBox.Text = this.form.SYS_RENBAN_HOUHOU_KBN.Text;

                //マニ登録形態
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[2];
                decisionTextBox.Text = this.form.SYS_MANI_KEITAI_KBN.Text;

                //請求締処理
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[3];
                if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text == "1")
                {
                    decisionTextBox.Text = "4";
                }
                else if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text == "2")
                {
                    decisionTextBox.Text = "5";
                }

                //支払締処理
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[4];
                if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text == "1")
                {
                    decisionTextBox.Text = "4";
                }
                else if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text == "2")
                {
                    decisionTextBox.Text = "5";
                }

                //在庫管理
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[5];
                decisionTextBox.Text = this.form.ZAIKO_KANRI.Text;

                //評価方法
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[6];
                decisionTextBox.Text = this.form.ZAIKO_HYOUKA_HOUHOU.Text;

                // コンテナ管理方法
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[7];
                decisionTextBox.Text = this.form.CONTENA_KANRI_HOUHOU.Text;

                //領収書連番方法
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[8];
                decisionTextBox.Text = this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text;

                // システム 重量設定
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[9];
                decisionTextBox.Text = this.form.SYS_JYURYOU_FORMAT_CD.Text;

                // システム 数量設定
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[10];
                decisionTextBox.Text = this.form.SYS_SUURYOU_FORMAT_CD.Text;

                // システム 単価設定
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[11];
                decisionTextBox.Text = this.form.SYS_TANKA_FORMAT_CD.Text;

                // 委託契約書 数量書式
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[12];
                decisionTextBox.Text = this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text;

                // 委託契約書 単価書式
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[13];
                decisionTextBox.Text = this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text;

                // マニフェスト 数量書式
                decisionTextBox = (CustomNumericTextBox2)this.form.ParentControls[14];
                decisionTextBox.Text = this.form.MANIFEST_SUURYO_FORMAT_CD.Text;

                // デジタコ 連携用会社ID
                this.form.ParentControls[15] = this.form.DIGI_RENKEI_KAISYA_ID.Text;

                // デジタコ ユーザーID
                this.form.ParentControls[16] = this.form.USER_ID.Text;

                // デジタコ パスワード
                this.form.ParentControls[17] = this.form.PASSWORD.Text;

                // デジタコ 地点範囲半径
                this.form.ParentControls[18] = this.form.CHITEN_HANI_HANKEI.Text;

                // デジタコ 翌日持越区分
                this.form.ParentControls[19] = this.form.CARRY_OVER_NEXT_DAY.Text;

                // NAVITIME 会社ID
                this.form.ParentControls[20] = this.form.NAVI_CORP_ID.Text;

                // NAVITIME アカウント
                this.form.ParentControls[21] = this.form.NAVI_ACCOUNT.Text;

                // NAVITIME パスワード
                this.form.ParentControls[22] = this.form.NAVI_PWD.Text;

                // NAVITIME 作業時間(分)
                this.form.ParentControls[23] = this.form.NAVI_SAGYOU_TIME.Text;

                // NAVITIME 渋滞考慮
                this.form.ParentControls[24] = this.form.NAVI_TRAFFIC.Checked;

                // NAVITIME スマートIC優先
                this.form.ParentControls[25] = this.form.NAVI_SMART_IC.Checked;

                // NAVITIME 有料優先
                this.form.ParentControls[26] = this.form.NAVI_TOLL.Checked;

                // NAVITIME 時間取得
                this.form.ParentControls[27] = this.form.NAVI_GET_TIME.Text;

                // 初期最大表示画面数
                this.form.ParentControls[28] = this.form.MAX_WINDOW_COUNT.Text;

                // 受入出荷画面サイズ
                this.form.ParentControls[29] = this.form.UKEIRESHUKA_GAMEN_SIZE.Text;

                // 伝表発行表示有無
                this.form.ParentControls[30] = this.form.DENPYOU_HAKOU_HYOUJI.Text;

                // 最大登録容量
                this.form.ParentControls[31] = this.form.MAX_INSERT_CAPACITY.Text;

                // 最大ファイルサイズ
                this.form.ParentControls[32] = this.form.MAX_FILE_SIZE.Text;

                // 接続先データベース
                var dto = (DBConnectionDTO)this.form.DB_FILE_CONNECT.SelectedItem;
                if (dto != null)
                {
                    this.form.ParentControls[33] = dto.ConnectionString;

                }

                // INXSSubAppデータベース
                var subappDbConnectionDto = (InxsSubappConnectionDto)this.form.DB_SUBAPP_CONNECT.SelectedItem;
                if (subappDbConnectionDto != null)
                {                    
                    this.form.ParentControls[34] = subappDbConnectionDto.ConnectionString;
                    this.form.ParentControls[35] = subappDbConnectionDto.DispName;
                }

                //CongBinh 20210712 #152799 S
                this.form.ParentControls[36] = this.form.MOD_SUPPORT_KBN.Text;
                this.form.ParentControls[37] = this.form.MOD_URL.Text;
                //CongBinh 20210712 #152799 E

                //QN_QUAN add 20211229 #158952 S
                var dtolog = (DBConnectionDTOLOG)this.form.DB_LOG_CONNECT.SelectedItem;
                if (dtolog != null)
                {
                    this.form.ParentControls[38] = dtolog.ConnectionString;
                }
                //QN_QUAN add 20211229 #158952 E

                this.form.ParentControls[39] = this.form.BARCODO_SHINKAKU.Text;//160029

                //PhuocLoc 2022/01/04 #158897, #158898 -Start
                // ｼｰｸﾚｯﾄｷｰ
                this.form.ParentControls[40] = this.form.SECRET_KEY.Text;

                // 顧客ID
                this.form.ParentControls[41] = this.form.CUSTOMER_ID.Text;
                //PhuocLoc 2022/01/04 #158897, #158898 -End

                // 空電プッシュ　アクセスキー
                this.form.ParentControls[42] = this.form.KARADEN_ACCESS_KEY.Text;

                // 空電プッシュ　セキュリティコード
                this.form.ParentControls[43] = this.form.KARADEN_SECURITY_CODE.Text;

                // 空電プッシュ　最大送信文字数
                this.form.ParentControls[44] = this.form.KARADEN_MAX_WORD_COUNT.Text;

                // 楽楽明細連携　楽楽顧客コード採番方法
                this.form.ParentControls[45] = this.form.SYS_RAKURAKU_CODE_NUMBERING_CD.Text;

                this.form.Close();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 反映ボタン押下時の入力チェック
        /// </summary>
        /// <returns>true:エラーあり、false:エラーなし</returns>
        private bool inputCheck()
        {
            bool errorFlg = false;

            // 必須チェック
            if (string.IsNullOrEmpty(this.form.SYS_RENBAN_HOUHOU_KBN.Text))
            {
                this.form.SYS_RENBAN_HOUHOU_KBN.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "連番方法");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text))
            {
                this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "領収書連番方法");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.SYS_MANI_KEITAI_KBN.Text))
            {
                this.form.SYS_MANI_KEITAI_KBN.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "マニ登録形態");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.ZAIKO_KANRI.Text))
            {
                this.form.ZAIKO_KANRI.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "在庫管理");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.ZAIKO_HYOUKA_HOUHOU.Text))
            {
                this.form.ZAIKO_HYOUKA_HOUHOU.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "評価方法");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.CONTENA_KANRI_HOUHOU.Text))
            {
                this.form.CONTENA_KANRI_HOUHOU.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "コンテナ管理方法");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.UKEIRESHUKA_GAMEN_SIZE.Text))
            {
                this.form.UKEIRESHUKA_GAMEN_SIZE.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "受入出荷画面サイズ");
                return true;
            }

            if (this.form.DENPYOU_HAKOU_HYOUJI.ReadOnly == false && string.IsNullOrEmpty(this.form.DENPYOU_HAKOU_HYOUJI.Text))
            {
                this.form.DENPYOU_HAKOU_HYOUJI.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "伝票発行表示有無");
                return true;
            }

            //CongBinh 20210712 #152799 S
            if (string.IsNullOrEmpty(this.form.MOD_SUPPORT_KBN.Text))
            {
                this.form.MOD_SUPPORT_KBN.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "サポート保守");
                return true;
            }
            if ("1".Equals(this.form.MOD_SUPPORT_KBN.Text) && string.IsNullOrEmpty(this.form.MOD_URL.Text))
            {
                this.form.MOD_URL.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "サポートサイトのURL");
                return true;
            }
            //CongBinh 20210712 #152799 E
            //160029 S
            if (this.form.BARCODO_SHINKAKU.ReadOnly == false && string.IsNullOrEmpty(this.form.BARCODO_SHINKAKU.Text))
            {
                this.form.BARCODO_SHINKAKU.IsInputErrorOccured = true;                
                this.form.errmessage.MessageBoxShow("E001", "バーコード規格");
                return true;
            }
            //160029 E

            if (string.IsNullOrEmpty(this.form.SYS_JYURYOU_FORMAT_CD.Text))
            {
                this.form.SYS_JYURYOU_FORMAT_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "システム - 重量書式");
                return true;
            }
            if (string.IsNullOrEmpty(this.form.SYS_SUURYOU_FORMAT_CD.Text))
            {
                this.form.SYS_SUURYOU_FORMAT_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "システム - 数量書式");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.SYS_TANKA_FORMAT_CD.Text))
            {
                this.form.SYS_TANKA_FORMAT_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "システム - 単価書式");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.MANIFEST_SUURYO_FORMAT_CD.Text))
            {
                this.form.MANIFEST_SUURYO_FORMAT_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "マニフェスト - 数量書式");
                return true;
            }
            if (string.IsNullOrEmpty(this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text))
            {
                this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "委託契約書 - 数量書式");
                return true;
            }
            if (string.IsNullOrEmpty(this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text))
            {
                this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "委託契約書 - 単価書式");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.CHITEN_HANI_HANKEI.Text))
            {
                this.form.CHITEN_HANI_HANKEI.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "地点範囲半径");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.CARRY_OVER_NEXT_DAY.Text))
            {
                this.form.CARRY_OVER_NEXT_DAY.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "翌日持越区分");
                return true;
            }

            // 地点範囲半径は50～9999の範囲内とする。
            if (!this.form.CHITEN_HANI_HANKEI.Text.Equals("") && Convert.ToInt16(this.form.CHITEN_HANI_HANKEI.Text) < 50)
            {
                this.form.CHITEN_HANI_HANKEI.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E002", "地点範囲半径", "50～9999");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.MAX_WINDOW_COUNT.Text))
            {
                this.form.MAX_WINDOW_COUNT.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "初期最大表示画面数");
                this.form.MAX_WINDOW_COUNT.Focus();
                return true;
            }

            // INXSSubAppデータベース名チェック
            var subappDbConnectionDto = (InxsSubappConnectionDto)this.form.DB_SUBAPP_CONNECT.SelectedItem;
            if (subappDbConnectionDto != null && !string.IsNullOrEmpty(subappDbConnectionDto.DispName) && subappDbConnectionDto.DispName.Length > 300)
            {
                this.form.errmessage.MessageBoxShowError("データベースのDispNameの入力文字数が制限数を超えました。\r\n300文字以内で入力してください。");
                return true;
            }

            // 空電タブの入力チェック
            if ((!string.IsNullOrEmpty(this.form.KARADEN_ACCESS_KEY.Text) && string.IsNullOrEmpty(this.form.KARADEN_SECURITY_CODE.Text))
                || (string.IsNullOrEmpty(this.form.KARADEN_ACCESS_KEY.Text) && !string.IsNullOrEmpty(this.form.KARADEN_SECURITY_CODE.Text)))
            {
                if (string.IsNullOrEmpty(this.form.KARADEN_ACCESS_KEY.Text))
                {
                    this.form.KARADEN_ACCESS_KEY.IsInputErrorOccured = true;
                }
                else
                {
                    this.form.KARADEN_SECURITY_CODE.IsInputErrorOccured = true;
                }
                this.form.errmessage.MessageBoxShowError("アクセスキー、ｾｷｭﾘﾃｨｺｰﾄﾞのどちらも入力が必須です。");
                return true;
            }

            if (string.IsNullOrEmpty(this.form.KARADEN_MAX_WORD_COUNT.Text))
            {
                this.form.KARADEN_MAX_WORD_COUNT.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShow("E001", "最大送信文字数");
                return true;
            }

            if (AppConfig.AppOptions.IsRakurakuMeisai())
            {
                if (string.IsNullOrEmpty(this.form.SYS_RAKURAKU_CODE_NUMBERING_CD.Text))
                {
                    this.form.SYS_RAKURAKU_CODE_NUMBERING_CD.IsInputErrorOccured = true;
                    this.form.errmessage.MessageBoxShow("E001", "楽楽顧客コード採番方法");
                    return true;
                }
            }

            return errorFlg;

        }

        /// <summary>
        /// コンボボックスの初期化
        /// </summary>
        /// <param name="dbConnectionString"></param>
        private void SetComboBox(string dbConnectionString)
        {
            // コンボボックスの初期化
            var dblist = this.GetDBConnectionList();
            if (dblist == null)
            {
                this.form.DB_FILE_CONNECT.Enabled = false;
                this.SetAllControlsEnabled(false);
                this.form.errmessage.MessageBoxShowError(string.Format("{0}ファイルが存在しません", XmlManager.DBConfigFile));
                return;
            }
            else if (dblist.Count() == 0)
            {
                this.form.DB_FILE_CONNECT.Enabled = false;
                this.SetAllControlsEnabled(false);
                this.form.errmessage.MessageBoxShowError("データベース定義がありません");
                return;
            }
            else
            {
                this.form.DB_FILE_CONNECT.DisplayMember = "DispName";
                this.form.DB_FILE_CONNECT.ValueMember = "ConnectionString";
                this.form.DB_FILE_CONNECT.DataSource = dblist;
            }

            // 初期値設定
            var dbDto = new DBConnectionDTO(string.Empty, dbConnectionString);
            if (!string.IsNullOrEmpty(dbConnectionString) && dbDto.CanConnect())
            {
                this.form.DB_FILE_CONNECT.SelectedValue = dbConnectionString;
            }
        }

        /// <summary>
        /// XMLからDB接続情報取得
        /// </summary>
        /// <returns></returns>
        private List<DBConnectionDTO> GetDBConnectionList()
        {
            LogUtility.DebugMethodStart();

            List<DBConnectionDTO> connectionList = null;

            try
            {
                var dbconfig = XmlManager.GetDatabaseConnectList();
                if (dbconfig == null)
                {
                    return connectionList;
                }

                connectionList = dbconfig.Select(s => XmlManager.GetDbConnectionDto(s)).ToList();
                if (connectionList.Any())
                {
                    // ブランク行追加
                    connectionList.Insert(0, new DBConnectionDTO(string.Empty, string.Empty));
                }
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", Path.GetFileName(XmlManager.DBConfigFile)));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(connectionList);
            }

            return connectionList;
        }

        /// <summary>
        /// コンボボックスの選択値チェック
        /// </summary>
        internal void CheckSelectedItem()
        {
            var dto = (DBConnectionDTO)this.form.DB_FILE_CONNECT.SelectedItem;
            if (string.IsNullOrEmpty(dto.ConnectionString) && string.IsNullOrEmpty(dto.ConnectionString))
            {
                // ブランク選択時
                this.SetAllControlsEnabled(true);
            }
            else if (dto.CanConnect())
            {
                // 選択した接続先DBが接続可能の時だけ登録可
                this.SetAllControlsEnabled(true);
            }
            else
            {
                this.SetAllControlsEnabled(false);

                this.form.errmessage.MessageBoxShowError("指定された接続先データベースに接続出来ませんでした。");
            }
        }

        /// <summary>
        /// コンボボックス、閉じるボタン以外のコントロールの活性制御
        /// </summary>
        /// <param name="enabled"></param>
        private void SetAllControlsEnabled(bool enabled)
        {
            // 「接続先コンボボックス」と「F12 閉じる」ボタン以外のコントロールが作成されたら追加
            this.form.bt_func9.Enabled = enabled;
        }

        #region InxsSubapplication Database connection setting

        /// <summary>
        /// Set list database connection to combobox
        /// </summary>
        /// <param name="dbConnectionString"></param>
        private void SetInxsListDBComboBox(string dbConnectionString)
        {
            if (!r_framework.Configuration.AppConfig.AppOptions.IsInxs())
            {
                this.form.DB_SUBAPP_CONNECT.Enabled = false;
                return;
            }
            // コンボボックスの初期化
            var dblist = this.GetInxsSubappDBConnectionList();
            if (dblist == null)
            {
                this.form.DB_SUBAPP_CONNECT.Enabled = false;
                this.SetAllControlsEnabled(false);
                this.form.errmessage.MessageBoxShowError(string.Format("{0}ファイルが存在しません", Constans.INXS_DBCONNECTION_LIST_FILE_PATH));
                return;
            }
            else if (dblist.Count() == 0)
            {
                this.form.DB_SUBAPP_CONNECT.Enabled = false;
                this.SetAllControlsEnabled(false);
                this.form.errmessage.MessageBoxShowError("データベース定義がありません");
                return;
            }
            else
            {
                this.form.DB_SUBAPP_CONNECT.DisplayMember = "DispName";
                this.form.DB_SUBAPP_CONNECT.ValueMember = "ConnectionString";
                this.form.DB_SUBAPP_CONNECT.DataSource = dblist;
            }

            // 初期値設定
            var dbDto = new InxsSubappConnectionDto(string.Empty, dbConnectionString, string.Empty);
            if (!string.IsNullOrEmpty(dbConnectionString) && dbDto.CanConnect())
            {
                this.form.DB_SUBAPP_CONNECT.SelectedValue = dbConnectionString;
            }
        }

        /// <summary>
        /// Get list InxsSubapp DB connection list
        /// </summary>
        /// <returns></returns>
        private List<InxsSubappConnectionDto> GetInxsSubappDBConnectionList()
        {
            LogUtility.DebugMethodStart();

            List<InxsSubappConnectionDto> connectionList = null;

            XmlHelper xmlHelper = new XmlHelper()
            {
                DBConfigFile = Constans.INXS_DBCONNECTION_LIST_FILE_PATH
            };

            try
            {
                var dbconfig = xmlHelper.GetDatabaseConnectList();
                if (dbconfig == null)
                {
                    return connectionList;
                }

                var container = (IS2Container)SingletonS2ContainerFactory.Container.GetComponent(Constans.DAO);
                var dataSource = (Seasar.Extension.Tx.Impl.TxDataSource)container.GetComponent("DataSource");

                connectionList = dbconfig.Select(s => xmlHelper.GetDbConnectionDto(s))
                                        .Where(w => w.ShougunRConnectionString.Equals(dataSource.ConnectionString))
                                        .ToList();
                if (connectionList.Any())
                {
                    // ブランク行追加
                    connectionList.Insert(0, new InxsSubappConnectionDto(string.Empty, string.Empty, string.Empty));
                }
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", Path.GetFileName(xmlHelper.DBConfigFile)));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return connectionList;
        }

        /// <summary>
        /// Check subapp database connection
        /// </summary>
        internal void CheckSelectedConnection()
        {
            var dto = (InxsSubappConnectionDto)this.form.DB_SUBAPP_CONNECT.SelectedItem;
            if (string.IsNullOrEmpty(dto.ConnectionString) && string.IsNullOrEmpty(dto.ConnectionString))
            {
                // ブランク選択時
                this.SetAllControlsEnabled(true);
            }
            else if (dto.CanConnect())
            {
                // 選択した接続先DBが接続可能の時だけ登録可
                this.SetAllControlsEnabled(true);
            }
            else
            {
                this.SetAllControlsEnabled(false);

                this.form.errmessage.MessageBoxShowError("指定された接続先データベースに接続出来ませんでした。");
            }
        }

        #endregion

        internal void CheckSelectedItemLOG()
        {
            var dto = (DBConnectionDTOLOG)this.form.DB_LOG_CONNECT.SelectedItem;
            if (string.IsNullOrEmpty(dto.ConnectionString))
            {
                // ブランク選択時
                this.SetAllControlsEnabled(true);
            }
            else if (dto.CanConnect())
            {
                if (checkTableLog(dto))
                {
                    // 選択した接続先DBが接続可能の時だけ登録可
                    this.SetAllControlsEnabled(true);
                }
                else
                {
                    this.SetAllControlsEnabled(false);
                }
            }
            else
            {
                this.SetAllControlsEnabled(false);

                this.form.errmessage.MessageBoxShow("E350");
            }
        }
        private void SetComboBoxLOG(string dbConnectionString)
        {
            // コンボボックスの初期化
            var dblist = this.GetDBLOGConnectionList();
            if (dblist == null)
            {
                this.form.DB_LOG_CONNECT.Enabled = false;
                this.SetAllControlsEnabled(false);
                this.form.errmessage.MessageBoxShowError(string.Format("{0}ファイルが存在しません", XmlManager.DBConfigLOG));
                return;
            }
            else if (dblist.Count() == 0)
            {
                this.form.DB_LOG_CONNECT.Enabled = false;
                this.SetAllControlsEnabled(false);
                this.form.errmessage.MessageBoxShowError("データベース定義がありません");
                return;
            }
            else
            {
                this.form.DB_LOG_CONNECT.DisplayMember = "DispName";
                this.form.DB_LOG_CONNECT.ValueMember = "ConnectionString";
                this.form.DB_LOG_CONNECT.DataSource = dblist;
            }

            // 初期値設定
            var dbDto = new DBConnectionDTOLOG(string.Empty, dbConnectionString);
            if (!string.IsNullOrEmpty(dbConnectionString) && dbDto.CanConnect())
            {
                this.form.DB_LOG_CONNECT.SelectedValue = dbConnectionString;
            }
        }
        private List<DBConnectionDTOLOG> GetDBLOGConnectionList()
        {
            LogUtility.DebugMethodStart();

            List<DBConnectionDTOLOG> connectionList = null;

            try
            {
                var dbconfig = XmlManager.GetDatabaseLOGConnectList();
                if (dbconfig == null)
                {
                    return connectionList;
                }

                connectionList = dbconfig.Select(s => XmlManager.GetDbConnectionLOGDto(s)).ToList();
                if (connectionList.Any())
                {
                    // ブランク行追加
                    connectionList.Insert(0, new DBConnectionDTOLOG(string.Empty, string.Empty));
                }
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", Path.GetFileName(XmlManager.DBConfigLOG)));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(connectionList);
            }

            return connectionList;
        }

        private bool checkTableLog(DBConnectionDTOLOG dto)
        {
            try
            {
                var daoLog = (IS2Container)SingletonS2ContainerFactory.Container.GetComponent(Constans.DAO_LOG);
                var dataSourceFile = (Seasar.Extension.Tx.Impl.TxDataSource)daoLog.GetComponent("DataSource");
                dataSourceFile.ConnectionString = dto.ConnectionString;
                using (TransactionUtility tran = new TransactionUtility())
                {
                    r_framework.Dao.IT_OPERATE_LOGDao daoentry = DaoInitUtilityLOG.GetComponent<r_framework.Dao.IT_OPERATE_LOGDao>();
                    var incheck= daoentry.CheckTableConnect();
                    tran.Commit();
                }
                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("checkTableLog", ex1);
                this.form.errmessage.MessageBoxShow("E350", "");
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("checkTableLog", ex1);
                this.form.errmessage.MessageBoxShow("E350", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkTableLog", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
    }
    
}