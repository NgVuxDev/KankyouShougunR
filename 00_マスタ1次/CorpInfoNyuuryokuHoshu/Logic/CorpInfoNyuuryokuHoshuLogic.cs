// $Id: CorpInfoNyuuryokuHoshuLogic.cs 38807 2015-01-07 04:41:20Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CorpInfoNyuuryokuHoshu.APP;
using MasterCommon.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace CorpInfoNyuuryokuHoshu.Logic
{
    /// <summary>
    /// 自社情報入力保守画面のビジネスロジック
    /// </summary>
    public class CorpInfoNyuuryokuHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "CorpInfoNyuuryokuHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 自社情報入力保守画面Form
        /// </summary>
        private CorpInfoNyuuryokuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_CORP_INFO[] entitys;

        /// <summary>
        /// 自社情報入力のDao
        /// </summary>
        private IM_CORP_INFODao daoCorpInfo;

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao daoKyoten;

        /// <summary>
        /// 銀行支店Data
        /// </summary>
        public IM_BANK_SHITENDao daoShiten;

        /// <summary>
        /// 銀行Data
        /// </summary>
        public IM_BANKDao daoBank;

        /// <summary>
        /// 入出金区分Data
        /// </summary>
        public IM_NYUUSHUKKIN_KBNDao daoNyuushukkin;

        /// <summary>
        /// 拠点Data
        /// </summary>
        public M_KYOTEN[] kyotenData;

        #endregion
        
        #region プロパティ

        /// <summary>
        /// 登録情報
        /// </summary>
        public M_CORP_INFO RegistString { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_KYOTEN SearchKyotenString { get; set; }

        #endregion
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public CorpInfoNyuuryokuHoshuLogic(CorpInfoNyuuryokuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoCorpInfo = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.daoShiten = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
            this.daoBank = DaoInitUtility.GetComponent<IM_BANKDao>();
            this.daoNyuushukkin = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M191", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.CORP_NAME.ReadOnly = true;
            this.form.CORP_RYAKU_NAME.ReadOnly = true;
            this.form.CORP_FURIGANA.ReadOnly = true;
            this.form.CORP_DAIHYOU.ReadOnly = true;
            this.form.KISHU_MONTH.ReadOnly = true;
            this.form.SHIMEBI.Enabled = false;
            this.form.SHIHARAI_MONTH.ReadOnly = true;
            this.form.rbt_tougetsu.Enabled = false;
            this.form.rbt_yokugetsu.Enabled = false;
            this.form.rbt_yokuyokugetsu.Enabled = false;
            this.form.rbt_3kagetsu.Enabled = false;
            this.form.rbt_4kagetsu.Enabled = false;
            this.form.rbt_5kegatsu.Enabled = false;
            this.form.rbt_6kagetsu.Enabled = false;
            this.form.SHIHARAI_DAY.Enabled = false;
            this.form.NYUUSHUKKIN_KBN_CD.ReadOnly = true;
            this.form.customPopupOpenButton1.Enabled = false;
            this.form.BANK_CD.ReadOnly = true;
            this.form.BANK_SHITEN_CD.ReadOnly = true;
            this.form.BANK_CD_2.ReadOnly = true;
            this.form.BANK_SHITEN_CD_2.ReadOnly = true;
            this.form.BANK_CD_3.ReadOnly = true;
            this.form.BANK_SHITEN_CD_3.ReadOnly = true;

            this.form.FURIKOMI_MOTO_BANK_CD.ReadOnly = true;
            this.form.FURIKOMI_MOTO_BANK_SHITEN_CD.ReadOnly = true;
            this.form.FURIKOMI_IRAIJIN_CD.ReadOnly = true;

            this.form.TOUROKU_NO.Enabled = false;

            // FunctionButton
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.entitys = daoCorpInfo.GetAllData();

                var kyoten = new M_KYOTEN();
                this.kyotenData = daoKyoten.GetAllValidData(kyoten);

                bool ResultCheck = false;

                if (this.entitys.Length != 0)
                {
                    ResultCheck = true;

                }

                int count = ResultCheck == false ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var corpInfoEntity = new M_CORP_INFO();

                if (this.entitys.Length == 0)
                {
                    corpInfoEntity.SYS_ID = 0;
                }
                else
                {
                    corpInfoEntity.SYS_ID = this.entitys[0].SYS_ID;
                    corpInfoEntity.TIME_STAMP = this.entitys[0].TIME_STAMP;
                }

                // 現在の入力内容でEntity作成
                corpInfoEntity.CORP_NAME = this.form.CORP_NAME.Text;
                corpInfoEntity.CORP_RYAKU_NAME = this.form.CORP_RYAKU_NAME.Text;
                corpInfoEntity.CORP_FURIGANA = this.form.CORP_FURIGANA.Text;
                corpInfoEntity.CORP_DAIHYOU = this.form.CORP_DAIHYOU.Text;
                corpInfoEntity.KISHU_MONTH = Int16.Parse(this.form.KISHU_MONTH.Text);
                corpInfoEntity.SHIMEBI = Int16.Parse(this.form.SHIMEBI.Text);
                corpInfoEntity.SHIHARAI_MONTH = Int16.Parse(this.form.SHIHARAI_MONTH.Text);
                corpInfoEntity.SHIHARAI_DAY = Int16.Parse(this.form.SHIHARAI_DAY.Text);
                corpInfoEntity.SHIHARAI_HOUHOU = Int16.Parse(this.form.NYUUSHUKKIN_KBN_CD.Text);
                corpInfoEntity.TOUROKU_NO = this.form.TOUROKU_NO.Text;
                corpInfoEntity.BANK_CD = this.form.BANK_CD.Text;
                corpInfoEntity.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
                corpInfoEntity.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
                corpInfoEntity.KOUZA_NO = this.form.KOUZA_NO.Text;
                corpInfoEntity.KOUZA_NAME = this.form.KOUZA_NAME.Text;
                corpInfoEntity.BANK_CD_2 = this.form.BANK_CD_2.Text;
                corpInfoEntity.BANK_SHITEN_CD_2 = this.form.BANK_SHITEN_CD_2.Text;
                corpInfoEntity.KOUZA_SHURUI_2 = this.form.KOUZA_SHURUI_2.Text;
                corpInfoEntity.KOUZA_NO_2 = this.form.KOUZA_NO_2.Text;
                corpInfoEntity.KOUZA_NAME_2 = this.form.KOUZA_NAME_2.Text;
                corpInfoEntity.BANK_CD_3 = this.form.BANK_CD_3.Text;
                corpInfoEntity.BANK_SHITEN_CD_3 = this.form.BANK_SHITEN_CD_3.Text;
                corpInfoEntity.KOUZA_SHURUI_3 = this.form.KOUZA_SHURUI_3.Text;
                corpInfoEntity.KOUZA_NO_3 = this.form.KOUZA_NO_3.Text;
                corpInfoEntity.KOUZA_NAME_3 = this.form.KOUZA_NAME_3.Text;

                corpInfoEntity.FURIKOMI_MOTO_BANK_CD = this.form.FURIKOMI_MOTO_BANK_CD.Text;
                corpInfoEntity.FURIKOMI_MOTO_BANK_SHITEN_CD = this.form.FURIKOMI_MOTO_BANK_SHITEN_CD.Text;
                corpInfoEntity.FURIKOMI_MOTO_KOUZA_SHURUI = this.form.FURIKOMI_MOTO_KOUZA_SHURUI.Text;
                corpInfoEntity.FURIKOMI_MOTO_KOUZA_NO = this.form.FURIKOMI_MOTO_KOUZA_NO.Text;
                corpInfoEntity.FURIKOMI_MOTO_KOUZA_NAME = this.form.FURIKOMI_MOTO_KOUZA_NAME.Text;
                corpInfoEntity.FURIKOMI_IRAIJIN_CD = this.form.FURIKOMI_IRAIJIN_CD.Text;

                corpInfoEntity.DELETE_FLG = false;

                // 更新者情報設定
                var dataBinderLogicNyuukin = new DataBinderLogic<r_framework.Entity.M_CORP_INFO>(corpInfoEntity);
                dataBinderLogicNyuukin.SetSystemProperty(corpInfoEntity, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), corpInfoEntity);

                this.RegistString = corpInfoEntity;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CORP_NAME.Clear();
                this.form.CORP_RYAKU_NAME.Clear();
                this.form.CORP_FURIGANA.Clear();
                this.form.CORP_DAIHYOU.Clear();
                this.form.SHIHARAI_MONTH.Clear();
                this.form.NYUUSHUKKIN_KBN_CD.Clear();
                this.form.NYUUSHUKKIN_KBN_NAME.Clear();
                this.form.BANK_CD.Clear();
                this.form.BANK_NAME_RYAKU.Clear();
                this.form.BANK_SHITEN_CD.Clear();
                this.form.BANK_SHIETN_NAME_RYAKU.Clear();
                this.form.KOUZA_SHURUI.Clear();
                this.form.KOUZA_NO.Clear();
                this.form.KOUZA_NAME.Clear();
                this.form.BANK_CD_2.Clear();
                this.form.BANK_NAME_RYAKU_2.Clear();
                this.form.BANK_SHITEN_CD_2.Clear();
                this.form.BANK_SHIETN_NAME_RYAKU_2.Clear();
                this.form.KOUZA_SHURUI_2.Clear();
                this.form.KOUZA_NO_2.Clear();
                this.form.KOUZA_NAME_2.Clear();
                this.form.BANK_CD_3.Clear();
                this.form.BANK_NAME_RYAKU_3.Clear();
                this.form.BANK_SHITEN_CD_3.Clear();
                this.form.BANK_SHIETN_NAME_RYAKU_3.Clear();
                this.form.KOUZA_SHURUI_3.Clear();
                this.form.KOUZA_NO_3.Clear();
                this.form.KOUZA_NAME_3.Clear();
                this.form.KISHU_MONTH.Text = "";
                this.form.SHIMEBI.Text = "";
                this.form.SHIHARAI_DAY.Text = "";
                this.form.TOUROKU_NO.Text = "";

                this.form.FURIKOMI_MOTO_BANK_CD.Clear();
                this.form.FURIKOMI_MOTO_BANK_NAME_RYAKU.Clear();
                this.form.FURIKOMI_MOTO_BANK_SHITEN_CD.Clear();
                this.form.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Clear();
                this.form.FURIKOMI_MOTO_KOUZA_SHURUI.Clear();
                this.form.FURIKOMI_MOTO_KOUZA_NO.Clear();
                this.form.FURIKOMI_MOTO_KOUZA_NAME.Clear();
                this.form.FURIKOMI_IRAIJIN_CD.Clear();

                this.form.Ichiran.DataSource = null;

                // 各テキストボックス設定(BaseHeader部)
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Clear();
                header.CreateUser.Clear();
                header.LastUpdateDate.Clear();
                header.LastUpdateUser.Clear();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #region 登録/更新/削除
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        if (this.entitys.Length == 0)
                        {
                            this.daoCorpInfo.Insert(this.RegistString);
                        }
                        else
                        {
                            this.daoCorpInfo.Update(this.RegistString);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equals/GetHashCode/ToString
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            CorpInfoNyuuryokuHoshuLogic localLogic = other as CorpInfoNyuuryokuHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetWindowData()
        {
            try
            {
                this.form.CORP_NAME.Text = this.entitys[0].CORP_NAME;
                this.form.CORP_RYAKU_NAME.Text = this.entitys[0].CORP_RYAKU_NAME;
                this.form.CORP_FURIGANA.Text = this.entitys[0].CORP_FURIGANA;
                this.form.CORP_DAIHYOU.Text = this.entitys[0].CORP_DAIHYOU;
                this.form.SHIMEBI.Text = this.entitys[0].SHIMEBI.Value.ToString();
                this.form.SHIHARAI_MONTH.Text = this.entitys[0].SHIHARAI_MONTH.Value.ToString();
                this.form.SHIHARAI_DAY.Text = this.entitys[0].SHIHARAI_DAY.Value.ToString();
                this.form.NYUUSHUKKIN_KBN_CD.Text = this.entitys[0].SHIHARAI_HOUHOU.Value.ToString();
                this.form.KISHU_MONTH.Text = this.entitys[0].KISHU_MONTH.Value.ToString();
                this.form.SHIMEBI.Text = this.entitys[0].SHIMEBI.Value.ToString();
                this.form.TOUROKU_NO.Text = this.entitys[0].TOUROKU_NO;
                this.form.BANK_CD.Text = this.entitys[0].BANK_CD;
                this.form.BANK_SHITEN_CD.Text = this.entitys[0].BANK_SHITEN_CD;
                this.form.KOUZA_SHURUI.Text = this.entitys[0].KOUZA_SHURUI;
                this.form.KOUZA_NO.Text = this.entitys[0].KOUZA_NO;
                //this.form.KOUZA_NAME.Text = this.entitys[0].KOUZA_NAME;

                this.form.BANK_CD_2.Text = this.entitys[0].BANK_CD_2;
                this.form.BANK_SHITEN_CD_2.Text = this.entitys[0].BANK_SHITEN_CD_2;
                this.form.KOUZA_SHURUI_2.Text = this.entitys[0].KOUZA_SHURUI_2;
                this.form.KOUZA_NO_2.Text = this.entitys[0].KOUZA_NO_2;
                this.form.BANK_CD_3.Text = this.entitys[0].BANK_CD_3;
                this.form.BANK_SHITEN_CD_3.Text = this.entitys[0].BANK_SHITEN_CD_3;
                this.form.KOUZA_SHURUI_3.Text = this.entitys[0].KOUZA_SHURUI_3;
                this.form.KOUZA_NO_3.Text = this.entitys[0].KOUZA_NO_3;

                this.form.FURIKOMI_MOTO_BANK_CD.Text = this.entitys[0].FURIKOMI_MOTO_BANK_CD;
                this.form.FURIKOMI_MOTO_BANK_SHITEN_CD.Text = this.entitys[0].FURIKOMI_MOTO_BANK_SHITEN_CD;
                this.form.FURIKOMI_MOTO_KOUZA_SHURUI.Text = this.entitys[0].FURIKOMI_MOTO_KOUZA_SHURUI;
                this.form.FURIKOMI_MOTO_KOUZA_NO.Text = this.entitys[0].FURIKOMI_MOTO_KOUZA_NO;
                this.form.FURIKOMI_IRAIJIN_CD.Text = this.entitys[0].FURIKOMI_IRAIJIN_CD;

                this.GetNyuushukkinKbnName();
                this.GetBankName();
                this.GetBankName_2();
                this.GetBankName_3();
                this.GetBankName_Furikomi();
                this.GetBankShitenName();
                this.GetBankShitenName_2();
                this.GetBankShitenName_3();
                this.GetBankShitenName_Furikomi();

                // 各テキストボックス設定(BaseHeader部)
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = this.entitys[0].CREATE_DATE.ToString();
                header.CreateUser.Text = this.entitys[0].CREATE_USER;
                header.LastUpdateDate.Text = this.entitys[0].UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.entitys[0].UPDATE_USER;

                if (this.kyotenData.Length == 0)
                {
                    return false;
                }

                this.form.Ichiran.DataSource = this.kyotenData;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 銀行支店CDバリデート処理
            this.form.BANK_SHITEN_CD.Validating += new CancelEventHandler(this.form.BANK_SHITEN_CD_Validating);

            // 銀行支店CD2バリデート処理
            this.form.BANK_SHITEN_CD_2.Validating += new CancelEventHandler(this.form.BANK_SHITEN_CD_2_Validating);

            // 銀行支店CD3バリデート処理
            this.form.BANK_SHITEN_CD_3.Validating += new CancelEventHandler(this.form.BANK_SHITEN_CD_3_Validating);

            this.form.FURIKOMI_MOTO_BANK_SHITEN_CD.Validating += new CancelEventHandler(this.form.FURIKOMI_MOTO_BANK_SHITEN_CD_Validating);
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 入出金区分名取得
        /// </summary>
        private void GetNyuushukkinKbnName()
        {
            M_NYUUSHUKKIN_KBN[] entity;

            entity = daoNyuushukkin.GetAllData();

            foreach (M_NYUUSHUKKIN_KBN entityNyuushukkin in entity)
            {
                if (entityNyuushukkin.NYUUSHUKKIN_KBN_CD == Int16.Parse(this.form.NYUUSHUKKIN_KBN_CD.Text))
                {
                    this.form.NYUUSHUKKIN_KBN_NAME.Text = entityNyuushukkin.NYUUSHUKKIN_KBN_NAME;
                }
            }
        }

        /// <summary>
        /// 銀行名取得
        /// </summary>
        private void GetBankName()
        {
            M_BANK[] entity;

            entity = daoBank.GetAllData();

            foreach (M_BANK entityBank in entity)
            {
                if (entityBank.BANK_CD == this.form.BANK_CD.Text)
                {
                    this.form.BANK_NAME_RYAKU.Text = entityBank.BANK_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// 銀行支店名取得
        /// </summary>
        private void GetBankShitenName()
        {
            //20151005 hoanghm #12152 start
            //M_BANK_SHITEN[] entity;

            //entity = daoShiten.GetAllData();

            //foreach (M_BANK_SHITEN entityBankShiten in entity)
            //{
            //    if (entityBankShiten.BANK_SHITEN_CD == this.form.BANK_SHITEN_CD.Text
            //            && entityBankShiten.BANK_CD == this.form.BANK_CD.Text)
            //    {
            //        this.form.BANK_SHIETN_NAME_RYAKU.Text = entityBankShiten.BANK_SHIETN_NAME_RYAKU;
            //        this.form.KOUZA_NAME.Text = entityBankShiten.KOUZA_NAME;
            //    }
            //}

            M_BANK_SHITEN searchCondition = new M_BANK_SHITEN();
            searchCondition.BANK_CD = this.form.BANK_CD.Text;
            searchCondition.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text;
            searchCondition.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
            searchCondition.KOUZA_NO = this.form.KOUZA_NO.Text;

            var entitys = daoShiten.GetAllValidData(searchCondition);

            if (entitys != null && entitys.Length > 0)
            {
                this.form.BANK_SHIETN_NAME_RYAKU.Text = entitys[0].BANK_SHIETN_NAME_RYAKU;
                this.form.KOUZA_NAME.Text = entitys[0].KOUZA_NAME;
            }
            //20151005 hoanghm #12152 end
        }

        /// <summary>
        /// 銀行情報設定処理
        /// </summary>
        public void SetBankInfo()
        {
            if (string.IsNullOrWhiteSpace(this.form.BANK_CD.Text) && !string.IsNullOrWhiteSpace(this.form.BANK_SHITEN_CD.Text))
            {
                M_BANK_SHITEN searchParams = new M_BANK_SHITEN();
                searchParams.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD.Text.PadLeft((int)this.form.BANK_SHITEN_CD.CharactersNumber, '0');
                M_BANK_SHITEN[] bankShiten = this.daoShiten.GetAllValidData(searchParams);
                if (bankShiten != null && bankShiten.Length == 1)
                {
                    M_BANK bank = this.daoBank.GetDataByCd(bankShiten[0].BANK_CD);
                    if (bank != null)
                    {
                        this.form.BANK_CD.Text = bank.BANK_CD;
                        this.form.BANK_NAME_RYAKU.Text = bank.BANK_NAME_RYAKU;
                    }
                }
            }
        }


        /// <summary>
        /// 銀行名2取得
        /// </summary>
        private void GetBankName_2()
        {
            M_BANK[] entity;

            entity = daoBank.GetAllData();

            foreach (M_BANK entityBank in entity)
            {
                if (entityBank.BANK_CD == this.form.BANK_CD_2.Text)
                {
                    this.form.BANK_NAME_RYAKU_2.Text = entityBank.BANK_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// 銀行支店名2取得
        /// </summary>
        private void GetBankShitenName_2()
        {
            M_BANK_SHITEN searchCondition = new M_BANK_SHITEN();
            searchCondition.BANK_CD = this.form.BANK_CD_2.Text;
            searchCondition.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD_2.Text;
            searchCondition.KOUZA_SHURUI = this.form.KOUZA_SHURUI_2.Text;
            searchCondition.KOUZA_NO = this.form.KOUZA_NO_2.Text;

            var entitys = daoShiten.GetAllValidData(searchCondition);

            if (entitys != null && entitys.Length > 0)
            {
                this.form.BANK_SHIETN_NAME_RYAKU_2.Text = entitys[0].BANK_SHIETN_NAME_RYAKU;
                this.form.KOUZA_NAME_2.Text = entitys[0].KOUZA_NAME;
            }
        }

        /// <summary>
        /// 銀行名3取得
        /// </summary>
        private void GetBankName_3()
        {
            M_BANK[] entity;

            entity = daoBank.GetAllData();

            foreach (M_BANK entityBank in entity)
            {
                if (entityBank.BANK_CD == this.form.BANK_CD_3.Text)
                {
                    this.form.BANK_NAME_RYAKU_3.Text = entityBank.BANK_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// 銀行支店名3取得
        /// </summary>
        private void GetBankShitenName_3()
        {
            M_BANK_SHITEN searchCondition = new M_BANK_SHITEN();
            searchCondition.BANK_CD = this.form.BANK_CD_3.Text;
            searchCondition.BANK_SHITEN_CD = this.form.BANK_SHITEN_CD_3.Text;
            searchCondition.KOUZA_SHURUI = this.form.KOUZA_SHURUI_3.Text;
            searchCondition.KOUZA_NO = this.form.KOUZA_NO_3.Text;

            var entitys = daoShiten.GetAllValidData(searchCondition);

            if (entitys != null && entitys.Length > 0)
            {
                this.form.BANK_SHIETN_NAME_RYAKU_3.Text = entitys[0].BANK_SHIETN_NAME_RYAKU;
                this.form.KOUZA_NAME_3.Text = entitys[0].KOUZA_NAME;
            }
        }

        /// <summary>
        /// 銀行名2取得
        /// </summary>
        private void GetBankName_Furikomi()
        {
            M_BANK[] entity;
            entity = daoBank.GetAllData();
            foreach (M_BANK entityBank in entity)
            {
                if (entityBank.BANK_CD == this.form.FURIKOMI_MOTO_BANK_CD.Text)
                {
                    this.form.FURIKOMI_MOTO_BANK_NAME_RYAKU.Text = entityBank.BANK_NAME_RYAKU;
                }
            }
        }
        /// <summary>
        /// 銀行支店名2取得
        /// </summary>
        private void GetBankShitenName_Furikomi()
        {
            M_BANK_SHITEN searchCondition = new M_BANK_SHITEN();
            searchCondition.BANK_CD = this.form.FURIKOMI_MOTO_BANK_CD.Text;
            searchCondition.BANK_SHITEN_CD = this.form.FURIKOMI_MOTO_BANK_SHITEN_CD.Text;
            searchCondition.KOUZA_SHURUI = this.form.FURIKOMI_MOTO_KOUZA_SHURUI.Text;
            searchCondition.KOUZA_NO = this.form.FURIKOMI_MOTO_KOUZA_NO.Text;
            var entitys = daoShiten.GetAllValidData(searchCondition);
            if (entitys != null && entitys.Length > 0)
            {
                this.form.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Text = entitys[0].BANK_SHIETN_NAME_RYAKU;
                this.form.FURIKOMI_MOTO_KOUZA_NAME.Text = entitys[0].KOUZA_NAME;
            }
        }

        /// <summary>
        /// 銀行支店リストを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店リスト</returns>
        internal List<M_BANK_SHITEN> GetBankShiten(string bankCd, string bankShitenCd,out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(bankCd, bankShitenCd);
                catchErr = false;
                var bankShitenList = this.daoShiten.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd }).ToList();

                LogUtility.DebugMethodEnd(bankShitenList, catchErr);

                return bankShitenList;
            }
            catch (Exception ex)
            {
                catchErr = true;
                var bankShitenList = new List<M_BANK_SHITEN>();
                LogUtility.Error("GetBankShiten", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(bankShitenList, catchErr);
                return bankShitenList;
            }
        }
    }
}
