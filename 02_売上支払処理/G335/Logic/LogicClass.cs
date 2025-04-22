using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Seasar.Framework.Exceptions;
using Shougun.Function.ShougunCSCommon.Dto;


namespace Shougun.Core.SalesPayment.DenpyouHakou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        /// <summary>
        /// Form
        /// </summary>
        private UIForm MyForm;
        /// <summary>
        /// 取引先_請求情報マスタ
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao TorihikisakiSeikyuDao;
        /// <summary>
        /// 取引先_支払情報マスタ
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao TorihikisakiShiharaiDao;
        /// <summary>
        /// 取引先_情報マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao TorihikisakiDao;
        /// <summary>
        /// 請求伝票
        /// </summary>
        private TSEIKYUUDENPYOUDao SeikyuuDenpyouDao;
        /// <summary>
        /// 業者
        /// </summary>
        private IM_GYOUSHADao GyoushaDao;
        /// <summary>
        /// 消費税マスタ
        /// </summary>
        private MSHOUHIZEIDao ShouhizeiDao;
        /// <summary>
        /// 品名マスタマスタ
        /// </summary>
        private IM_HINMEIDao HinmeiDao;
        /// <summary>
        /// 受入金額
        /// </summary>
        private TUKEIREENTRYDao UkeireentryDao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.SalesPayment.DenpyouHakou.Setting.ButtonSetting.xml";

        /// <summary>
        /// Init中フラグ
        /// </summary>
        internal Boolean valueInitFlag;

        /// <summary>
        /// [F1]残高取得がクリックされたかを示します
        /// </summary>
        internal bool GetZandakaClickFlg = false;

        /// <summary>
        /// エラー表示用品名CD
        /// </summary>
        internal string ErrHinmeiCD;

        private MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 端数処理種別
        /// </summary>
        private enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.MyForm = targetForm;

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                //システム設定．受入情報差引基準
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                //画面レイアウト初期化
                LayoutInit(mSysInfo);
                // ToolTip設定
                SetTipTxt(mSysInfo);
                // 初期値設定
                if (!SetInitValue(mSysInfo))
                {
                    return false;
                }
                // ボタンのテキストを初期化
                ButtonInit();
                // イベントの初期化処理
                EventInit();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 初期値設定
        /// </summary>
        private bool SetInitValue(M_SYS_INFO mSysInfo)
        {
            this.TorihikisakiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.SeikyuuDenpyouDao = DaoInitUtility.GetComponent<TSEIKYUUDENPYOUDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.ShouhizeiDao = DaoInitUtility.GetComponent<MSHOUHIZEIDao>();
            this.HinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.UkeireentryDao = DaoInitUtility.GetComponent<TUKEIREENTRYDao>();

            // 個別設定読み込み
            var userProfile = CurrentUserCustomConfigProfile.Load();

            //PhuocLoc 2020/04/03 #134980 -Start
            const string UR_SH_SEIKYUU_PRINT_KBN = "売上支払請求伝票";
            const string UR_SH_SHIHARAI_PRINT_KBN = "売上支払支払伝票";
            const string UR_SH_DENPYOU_PRINT_KBN = "売上支払仕切書出力区分";
            const string SHUKKA_SEIKYUU_PRINT_KBN = "出荷請求伝票";
            const string SHUKKA_SHIHARAI_PRINT_KBN = "出荷支払伝票";
            const string SHUKKA_DENPYOU_PRINT_KBN = "出荷仕切書出力区分";
            const string SHUKKA_KEIRYOU_PRINT_KBN = "出荷計量票出力区分";
            const string UKEIRE_SEIKYUU_PRINT_KBN = "受入請求伝票";
            const string UKEIRE_SHIHARAI_PRINT_KBN = "受入支払伝票";
            const string UKEIRE_DENPYOU_PRINT_KBN = "受入仕切書出力区分";
            const string UKEIRE_KEIRYOU_PRIRNT_KBN = "受入計量票出力区分";
            //PhuocLoc 2020/04/03 #134980 -End

            //initFlagSet
            valueInitFlag = true;

            //伝票モード　（１．追加　２．修正）
            string tenpyoModel = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel;
            //伝種区分　（１．受入　２．出荷　３．売上/支払）
            string denshuKbn = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn;
            //取引先_請求情報マスタ
            M_TORIHIKISAKI_SEIKYUU mtorihikisakiseikyuu = (M_TORIHIKISAKI_SEIKYUU)TorihikisakiSeikyuDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);
            //取引先_支払情報マスタ
            M_TORIHIKISAKI_SHIHARAI mtorihikisakishiharai = (M_TORIHIKISAKI_SHIHARAI)TorihikisakiShiharaiDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);

            // 伝票モード＝１．追加 かつ、 システム設定 残高自動取得＝１．するの場合
            if (tenpyoModel == ConstClass.TENPYO_MODEL_1 && this.GetZandakaJidouKBN(denshuKbn) == 1)
            {
                this.GetZandakaClickFlg = true;
            }

            //税計算区分の初期値設定
            if (!mSysInfo.SYS_ZEI_KEISAN_KBN_USE_KBN.IsNull)
            {
                //請求税計算区分の設定
                this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text = mtorihikisakiseikyuu.ZEI_KEISAN_KBN_CD.ToString();
                //支払税計算区分の設定
                this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text = mtorihikisakishiharai.ZEI_KEISAN_KBN_CD.ToString();
            }

            //追加モード時の税区分のラジオボタン
            if (Const.ConstClass.TENPYO_MODEL_1.Equals(tenpyoModel) || this.MyForm.ParameterDTO.Tairyuu_Kbn)
            {

                if (mtorihikisakiseikyuu != null)
                {
                    //1012
                    if (!mtorihikisakiseikyuu.ZEI_KBN_CD.IsNull)
                    {
                        //請求税区分の設定
                        this.MyForm.SEIKYU_ZEI_VALUE.Text = mtorihikisakiseikyuu.ZEI_KBN_CD.ToString();
                    }
                    //1013
                    if (!mtorihikisakiseikyuu.TORIHIKI_KBN_CD.IsNull)
                    {
                        //請求取引区分の設定
                        this.MyForm.SEIKYU_TORIHIKI_VALUE.Text = mtorihikisakiseikyuu.TORIHIKI_KBN_CD.ToString();
                    }
                }

                if (mtorihikisakishiharai != null)
                {
                    //1012
                    if (!mtorihikisakishiharai.ZEI_KBN_CD.IsNull)
                    {
                        //支払税区分の設定
                        this.MyForm.SHIHARAI_ZEI_VALUE.Text = mtorihikisakishiharai.ZEI_KBN_CD.ToString();
                    }
                    //1013
                    if (!mtorihikisakishiharai.TORIHIKI_KBN_CD.IsNull)
                    {
                        //支払取引区分の設定
                        this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text = mtorihikisakishiharai.TORIHIKI_KBN_CD.ToString();
                    }
                }
                //1015 伝票発行のラジオボタン
                //1016発行区分のラジオボタン
                //パラメータの伝種区分＝１（受入）の場合
                if (Const.ConstClass.TENSYU_KBN_1.Equals(denshuKbn))
                {
                    //PhuocLoc 2020/04/03 #134980 -Start
                    string Ukeire_Seikyuu_Print_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_SEIKYUU_PRINT_KBN);
                    string Ukeire_Shiharai_Print_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_SHIHARAI_PRINT_KBN);
                    string Ukeire_Denpyou_Print_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_DENPYOU_PRINT_KBN);
                    string Ukeire_Keiryou_Prirnt_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_KEIRYOU_PRIRNT_KBN);
                    if (!String.IsNullOrEmpty(Ukeire_Seikyuu_Print_Kbn))
                    {
                        //システム設定マスタ．受入情報請求伝票発行区分
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = Ukeire_Seikyuu_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ukeire_Shiharai_Print_Kbn))
                    {
                        //システム設定マスタ．受入情報支払伝票発行区分
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = Ukeire_Shiharai_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ukeire_Denpyou_Print_Kbn))
                    {
                        //システム設定マスタ．受入情報伝票出力区分
                        this.MyForm.HAKOU_VALUE.Text = Ukeire_Denpyou_Print_Kbn;
                    }
                    if (!string.IsNullOrEmpty(this.MyForm.ParameterDTO.Keiryou_Prirnt_Kbn_Value))
                    {
                        this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = this.MyForm.ParameterDTO.Keiryou_Prirnt_Kbn_Value;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Ukeire_Keiryou_Prirnt_Kbn))
                        {
                            this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = Ukeire_Keiryou_Prirnt_Kbn;
                        }
                    }
                    //PhuocLoc 2020/04/03 #134980 -End
                }
                //パラメータ伝種区分が２（出荷）の場合
                else if (Const.ConstClass.TENSYU_KBN_2.Equals(denshuKbn))
                {
                    //PhuocLoc 2020/04/03 #134980 -Start
                    string Shukka_Seikyuu_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_SEIKYUU_PRINT_KBN);
                    string Shukka_Shiharai_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_SHIHARAI_PRINT_KBN);
                    string Shukka_Denpyou_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_DENPYOU_PRINT_KBN);
                    string Shukka_Keiryou_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_KEIRYOU_PRINT_KBN);
                    if (!String.IsNullOrEmpty(Shukka_Seikyuu_Print_Kbn))
                    {
                        //システム設定マスタ．出荷情報請求伝票発行区分
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = Shukka_Seikyuu_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Shukka_Shiharai_Print_Kbn))
                    {
                        //システム設定マスタ．出荷情報支払伝票発行区分
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = Shukka_Shiharai_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Shukka_Denpyou_Print_Kbn))
                    {
                        //システム設定マスタ．出荷情報伝票出力区分
                        this.MyForm.HAKOU_VALUE.Text = Shukka_Denpyou_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Shukka_Keiryou_Print_Kbn))
                    {
                        this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = Shukka_Keiryou_Print_Kbn;
                    }
                    //PhuocLoc 2020/04/03 #134980 -End
                }
                //パラメータ伝種区分が３（売上支払）の場合
                else if (Const.ConstClass.TENSYU_KBN_3.Equals(denshuKbn))
                {
                    //PhuocLoc 2020/04/03 #134980 -Start
                    string Ur_Sh_Seikyuu_Print_Kbn = this.GetUserProfileValue(userProfile, UR_SH_SEIKYUU_PRINT_KBN);
                    string Ur_Sh_Shiharai_Print_Kbn = this.GetUserProfileValue(userProfile, UR_SH_SHIHARAI_PRINT_KBN);
                    string Ur_Sh_Denpyou_Print_Kbn = this.GetUserProfileValue(userProfile, UR_SH_DENPYOU_PRINT_KBN);
                    if (!String.IsNullOrEmpty(Ur_Sh_Seikyuu_Print_Kbn))
                    {
                        //システム設定マスタ．売上支払情報請求伝票発行区分
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = Ur_Sh_Seikyuu_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ur_Sh_Shiharai_Print_Kbn))
                    {
                        //システム設定マスタ．売上支払情報支払伝票発行区分
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = Ur_Sh_Shiharai_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ur_Sh_Denpyou_Print_Kbn))
                    {
                        //　システム設定マスタ．売上支払情報伝票出力区分
                        this.MyForm.HAKOU_VALUE.Text = Ur_Sh_Denpyou_Print_Kbn;
                    }
                    //PhuocLoc 2020/04/03 #134980 -End
                    this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = Const.ConstClass.KEIRYOU_PRIRNT_KBN_2;
                    this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Enabled = false;
                    this.MyForm.KEIRYOU_PRIRNT_KBN_1.Enabled = false;
                    this.MyForm.KEIRYOU_PRIRNT_KBN_2.Enabled = false;
                }

                // 確定フラグ：未確定の場合は精算をできない仕様とする
                if (!this.MyForm.KakuteiKbn)
                {
                    //精算のラジオボタン編集不可とする
                    this.MyForm.SEIKYU_SEISAN_VALUE.Enabled = false;
                    this.MyForm.SEIKYU_SEISAN_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_SEISAN_KBN_2.Enabled = false;
                    //精算のラジオボタン編集不可とする
                    this.MyForm.SHIHARAI_SEISAN_VALUE.Enabled = false;
                    this.MyForm.SHIHARAI_SEISAN_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_SEISAN_KBN_2.Enabled = false;
                }

                // キャッシャ連動状態セット
                this.setCasherEnabled(true);
            }
            //修正モード時の税区分のラジオボタン
            else if (Const.ConstClass.TENPYO_MODEL_2.Equals(tenpyoModel))
            {
                //1012
                //請求税計算区分の設定
                this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text = this.MyForm.ParameterDTO.Seikyu_Zeikeisan_Kbn;
                //支払税計算区分の設定
                this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text = this.MyForm.ParameterDTO.Shiharai_Zeikeisan_Kbn;

                //1012
                //請求税区分の設定
                this.MyForm.SEIKYU_ZEI_VALUE.Text = this.MyForm.ParameterDTO.Seikyu_Zei_Kbn;
                //支払税区分の設定
                this.MyForm.SHIHARAI_ZEI_VALUE.Text = this.MyForm.ParameterDTO.Shiharai_Zei_Kbn;
                //1013
                //請求取引区分の設定
                this.MyForm.SEIKYU_TORIHIKI_VALUE.Text = this.MyForm.ParameterDTO.Seikyu_Rohiki_Kbn;
                //支払税区分の設定
                this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text = this.MyForm.ParameterDTO.Shiharai_Rohiki_Kbn;
                //1014
                //精算のラジオボタン編集不可とする
                this.MyForm.SEIKYU_SEISAN_VALUE.Enabled = false;
                this.MyForm.SEIKYU_SEISAN_KBN_1.Enabled = false;
                this.MyForm.SEIKYU_SEISAN_KBN_2.Enabled = false;
                //精算のラジオボタン編集不可とする
                this.MyForm.SHIHARAI_SEISAN_VALUE.Enabled = false;
                this.MyForm.SHIHARAI_SEISAN_KBN_1.Enabled = false;
                this.MyForm.SHIHARAI_SEISAN_KBN_2.Enabled = false;
                //1015 伝票発行のラジオボタン
                //1016発行区分のラジオボタン
                //パラメータの伝種区分＝１（受入）の場合
                if (Const.ConstClass.TENSYU_KBN_1.Equals(denshuKbn))
                {
                    //PhuocLoc 2020/04/03 #134980 -Start
                    string Ukeire_Seikyuu_Print_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_SEIKYUU_PRINT_KBN);
                    string Ukeire_Shiharai_Print_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_SHIHARAI_PRINT_KBN);
                    string Ukeire_Denpyou_Print_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_DENPYOU_PRINT_KBN);
                    string Ukeire_Keiryou_Prirnt_Kbn = this.GetUserProfileValue(userProfile, UKEIRE_KEIRYOU_PRIRNT_KBN);
                    if (!String.IsNullOrEmpty(Ukeire_Seikyuu_Print_Kbn))
                    {
                        //システム設定マスタ．受入情報請求伝票発行区分
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = Ukeire_Seikyuu_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ukeire_Shiharai_Print_Kbn))
                    {
                        //システム設定マスタ．受入情報支払伝票発行区分
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = Ukeire_Shiharai_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ukeire_Denpyou_Print_Kbn))
                    {
                        //システム設定マスタ．受入情報伝票出力区分
                        this.MyForm.HAKOU_VALUE.Text = Ukeire_Denpyou_Print_Kbn;
                    }
                    if (!string.IsNullOrEmpty(this.MyForm.ParameterDTO.Keiryou_Prirnt_Kbn_Value))
                    {
                        this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = this.MyForm.ParameterDTO.Keiryou_Prirnt_Kbn_Value;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Ukeire_Keiryou_Prirnt_Kbn))
                        {
                            this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = Ukeire_Keiryou_Prirnt_Kbn;
                        }
                    }
                    //PhuocLoc 2020/04/03 #134980 -End
                }
                //パラメータ伝種区分が２（出荷）の場合
                else if (Const.ConstClass.TENSYU_KBN_2.Equals(denshuKbn))
                {
                    //PhuocLoc 2020/04/03 #134980 -Start
                    string Shukka_Seikyuu_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_SEIKYUU_PRINT_KBN);
                    string Shukka_Shiharai_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_SHIHARAI_PRINT_KBN);
                    string Shukka_Denpyou_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_DENPYOU_PRINT_KBN);
                    string Shukka_Keiryou_Print_Kbn = this.GetUserProfileValue(userProfile, SHUKKA_KEIRYOU_PRINT_KBN);
                    if (!String.IsNullOrEmpty(Shukka_Seikyuu_Print_Kbn))
                    {
                        //システム設定マスタ．出荷情報請求伝票発行区分
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = Shukka_Seikyuu_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Shukka_Shiharai_Print_Kbn))
                    {
                        //システム設定マスタ．出荷情報支払伝票発行区分
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = Shukka_Shiharai_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Shukka_Denpyou_Print_Kbn))
                    {
                        //システム設定マスタ．出荷情報伝票出力区分
                        this.MyForm.HAKOU_VALUE.Text = Shukka_Denpyou_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Shukka_Keiryou_Print_Kbn))
                    {
                        this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = Shukka_Keiryou_Print_Kbn;
                    }
                    //PhuocLoc 2020/04/03 #134980 -End
                }
                //パラメータ伝種区分が３（売上支払）の場合
                else if (Const.ConstClass.TENSYU_KBN_3.Equals(denshuKbn))
                {
                    //PhuocLoc 2020/04/03 #134980 -Start
                    string Ur_Sh_Seikyuu_Print_Kbn = this.GetUserProfileValue(userProfile, UR_SH_SEIKYUU_PRINT_KBN);
                    string Ur_Sh_Shiharai_Print_Kbn = this.GetUserProfileValue(userProfile, UR_SH_SHIHARAI_PRINT_KBN);
                    string Ur_Sh_Denpyou_Print_Kbn = this.GetUserProfileValue(userProfile, UR_SH_DENPYOU_PRINT_KBN);
                    if (!String.IsNullOrEmpty(Ur_Sh_Seikyuu_Print_Kbn))
                    {
                        //システム設定マスタ．売上支払情報請求伝票発行区分
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = Ur_Sh_Seikyuu_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ur_Sh_Shiharai_Print_Kbn))
                    {
                        //システム設定マスタ．売上支払情報支払伝票発行区分
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = Ur_Sh_Shiharai_Print_Kbn;
                    }
                    if (!String.IsNullOrEmpty(Ur_Sh_Denpyou_Print_Kbn))
                    {
                        //　システム設定マスタ．売上支払情報伝票出力区分
                        this.MyForm.HAKOU_VALUE.Text = Ur_Sh_Denpyou_Print_Kbn;
                    }
                    //PhuocLoc 2020/04/03 #134980 -End
                    this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Text = Const.ConstClass.KEIRYOU_PRIRNT_KBN_2;
                    this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Enabled = false;
                    this.MyForm.KEIRYOU_PRIRNT_KBN_1.Enabled = false;
                    this.MyForm.KEIRYOU_PRIRNT_KBN_2.Enabled = false;
                }

                // キャッシャ連動状態セット
                if (this.MyForm.ParameterDTO.Kakute_Kbn == "1")
                {
                    this.setCasherEnabled(true);
                }
                else
                {
                    this.setCasherEnabled(false);
                }
            }

            if(mtorihikisakiseikyuu.TORIHIKI_KBN_CD.Value == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 現金取引フラグセット
                this.MyForm.seikyuGenkinTorihikiFlg = true;

                // 請求取引区分が現金の場合は税計算区分「2.請求毎」は無効
                this.MyForm.SEIKYU_ZEIKEISAN_KBN_2.Enabled = false;
                //this.MyForm.SEIKYU_ZEIKEISAN_VALUE.CharacterLimitList = new char[] { '1', '3' };
                if(this.MyForm.SEIKYU_ZEIKEISAN_KBN_2.Checked == true)
                {
                    // 「2.請求毎」が選択されていた場合は「1.伝票毎」をセット
                    this.MyForm.SEIKYU_ZEIKEISAN_KBN_1.Checked = true;
                }

                // 請求取引区分が現金の場合は取引区分「現金」固定
                this.MyForm.SEIKYU_TORIHIKI_KBN_1.Checked = true;
                this.MyForm.SEIKYU_TORIHIKI_KBN_1.Enabled = false;
                this.MyForm.SEIKYU_TORIHIKI_KBN_2.Enabled = false;
                this.MyForm.SEIKYU_TORIHIKI_VALUE.Enabled = false;
            }
            else
            {
                // 確定区分：未確定の場合は現金取引できないようにする
                if(!this.MyForm.KakuteiKbn)
                {
                    this.MyForm.SEIKYU_TORIHIKI_KBN_2.Checked = true;
                    this.MyForm.SEIKYU_TORIHIKI_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_TORIHIKI_KBN_2.Enabled = false;
                    this.MyForm.SEIKYU_TORIHIKI_VALUE.Enabled = false;
                }
            }

            if(mtorihikisakishiharai.TORIHIKI_KBN_CD.Value == CommonConst.TORIHIKI_KBN_GENKIN)
            {
                // 現金取引フラグセット
                this.MyForm.shiharaiGenkinTorihikiFlg = true;

                // 支払取引区分が現金の場合は税計算区分「2.精算毎」は無効
                this.MyForm.SHIHARAI_ZEIKEISAN_KBN_2.Enabled = false;
                //this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.CharacterLimitList = new char[] { '1', '3' };
                if(this.MyForm.SHIHARAI_ZEIKEISAN_KBN_2.Checked == true)
                {
                    // 「2.精算毎」が選択されていた場合は「1.伝票毎」をセット
                    this.MyForm.SHIHARAI_ZEIKEISAN_KBN_1.Checked = true;
                }

                // 支払取引区分が現金の場合は取引区分「現金」固定
                this.MyForm.SHIHARAI_TORIHIKI_KBN_1.Checked = true;
                this.MyForm.SHIHARAI_TORIHIKI_KBN_1.Enabled = false;
                this.MyForm.SHIHARAI_TORIHIKI_KBN_2.Enabled = false;
                this.MyForm.SHIHARAI_TORIHIKI_VALUE.Enabled = false;
            }
            else
            {
                // 確定区分：未確定の場合は現金取引できないようにする
                if(!this.MyForm.KakuteiKbn)
                {
                    this.MyForm.SHIHARAI_TORIHIKI_KBN_2.Checked = true;
                    this.MyForm.SHIHARAI_TORIHIKI_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_TORIHIKI_KBN_2.Enabled = false;
                    this.MyForm.SHIHARAI_TORIHIKI_VALUE.Enabled = false;
                }
            }

            //1013
            //「請求取引」の値が掛けの場合
            if (this.MyForm.SEIKYU_TORIHIKI_VALUE.Text == "2" && this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text == "2")
            {
                //【領収証】【領収証有り】【領収証無し】を編集不可とする
                this.MyForm.RYOSYUSYO_VALUE.Enabled = false;
                this.MyForm.RYOSYUSYO_KBN_1.Enabled = false;
                this.MyForm.RYOSYUSYO_KBN_2.Enabled = false;
            }
            // 個別設定から領収書の設定をセット
            var ryoushuusho = userProfile.Settings.DefaultValue.Where(s => s.Name == "領収書").Select(s => s.Value).DefaultIfEmpty(ConstClass.RYOSYUSYO_KBN_2).FirstOrDefault();
            this.MyForm.RYOSYUSYO_VALUE.Text = ryoushuusho;

            //1014
            //精算のラジオボタンデフォルトを「2.しない」とする
            this.MyForm.SEIKYU_SEISAN_VALUE.Text = Const.ConstClass.SEISAN_KBN_2;
            //精算のラジオボタンデフォルトを「2.しない」とする
            this.MyForm.SHIHARAI_SEISAN_VALUE.Text = Const.ConstClass.SEISAN_KBN_2;

            //initFlagRelease
            valueInitFlag = false;

            //1017 前回残高設定
            if (!SetZenkaiZentaka1017())
            {
                return false;
            }
            //1018今回金額
            //伝票エンティティの売上合計金額（税ぬき）
            this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(this.MyForm.ParameterDTO.Uriage_Amount_Total);
            //伝票エンティティの支払合計金額（税ぬき）
            this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(this.MyForm.ParameterDTO.Shiharai_Amount_Total);
            //1019今回税額 1020今回取引
            if (!SetKonkaiZeigaku1019())
            {
                return false;
            }
            //1021相殺金額
            this.MyForm.Seikyu_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
            this.MyForm.Shiharai_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
            //1022入出金金額
            this.MyForm.Seikyu_Nyusyu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
            this.MyForm.Shiharai_Nyusyu_Kingaku.Text = Const.ConstClass.KIGAKU_0;

            // 入出金額計算
            this.MyForm.SeikyuSeisanChange(this.MyForm.SEIKYU_SEISAN_VALUE, new EventArgs());
            this.MyForm.ShiharaiSeisanChange(this.MyForm.SHIHARAI_SEISAN_VALUE, new EventArgs());

            //1023差引残高(前回残高+今回取引-相殺金額-入出金金額)
            if (!SetSeikyuKingaku1022())
            {
                return false;
            }
            if (!SetShiharaiKingaku1022())
            {
                return false;
            }
            //1025敬称
            SetKeisyou1025();
            //合計金額計算（上段-下段）
            if (!SetGokei())
            {
                return false;
            }
            //相殺
            this.MyForm.SOUSATU_VALUE.Text = Const.ConstClass.SOUSATU_KBN_2;

            // No.2257
            // 伝票区分によるEnbale設定
            TorihikiEnable(denshuKbn);

            // 仕切書/計量票出力なしの場合
            if(this.MyForm.ParameterDTO.Print_Enable == true)
            {
                // 請求伝票出力なし
                this.MyForm.SEIKYU_DENPYO_KBN_2.Checked = true;
                this.MyForm.SEIKYU_DENPYO_VALUE.Enabled = false;
                this.MyForm.SEIKYU_DENPYO_KBN_1.Enabled = false;
                this.MyForm.SEIKYU_DENPYO_KBN_2.Enabled = false;

                // 支払伝票出力なし
                this.MyForm.SHIHARAI_DENPYO_KBN_2.Checked = true;
                this.MyForm.SHIHARAI_DENPYO_VALUE.Enabled = false;
                this.MyForm.SHIHARAI_DENPYO_KBN_1.Enabled = false;
                this.MyForm.SHIHARAI_DENPYO_KBN_2.Enabled = false;

                // 計量票出力なし
                this.MyForm.KEIRYOU_PRIRNT_KBN_2.Checked = true;
                this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Enabled = false;
                this.MyForm.KEIRYOU_PRIRNT_KBN_1.Enabled = false;
                this.MyForm.KEIRYOU_PRIRNT_KBN_2.Enabled = false;
            }

            this.MyForm.SaveOldZei();
            //明細毎外税
            if ((Const.ConstClass.ZEIKEISAN_KBN_3.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text) && Const.ConstClass.ZEI_KBN_1.Equals(this.MyForm.SEIKYU_ZEI_VALUE.Text))
                || (Const.ConstClass.ZEIKEISAN_KBN_3.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text) && Const.ConstClass.ZEI_KBN_1.Equals(this.MyForm.SHIHARAI_ZEI_VALUE.Text)))
            {
                errmessage.MessageBoxShowWarn("税計算区分＝3.明細毎　は、\r適格請求書の要件を満たした請求書や仕切書になりません。\r税計算区分、税区分の見直しを行ってください。");
            }
            //請求/支払毎
            this.SeiMaiSoto_Check();

            return true;
        }

        // No.2257
        /// <summary>
        /// 伝票区分によるEnbale設定
        /// </summary>
        private void TorihikiEnable(string tenpyoKbn)
        {
            if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
            {
                bool isUriage = false;
                bool isSiharai = false;
                foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                {
                    if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                    {	// 伝票区分が売上
                        isUriage = true;
                    }
                    else if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                    {	// 伝票区分が支払
                        isSiharai = true;
                    }
                }

                if (!isSiharai) // 支払が無い場合
                {   // 支払をグレイアウト
                    //this.MyForm.L2.Enabled = false;
                    this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Enabled = false;
                    this.MyForm.SHIHARAI_ZEIKEISAN_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_ZEIKEISAN_KBN_2.Enabled = false;
                    this.MyForm.SHIHARAI_ZEIKEISAN_KBN_3.Enabled = false;
                    this.MyForm.SHIHARAI_ZEI_VALUE.Enabled = false;
                    this.MyForm.SHIHARAI_ZEI_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_ZEI_KBN_2.Enabled = false;
                    this.MyForm.SHIHARAI_ZEI_KBN_3.Enabled = false;
                    this.MyForm.SHIHARAI_TORIHIKI_VALUE.Enabled = false;
                    this.MyForm.SHIHARAI_TORIHIKI_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_TORIHIKI_KBN_2.Enabled = false;
                    this.MyForm.SHIHARAI_SEISAN_VALUE.Enabled = false;
                    this.MyForm.SHIHARAI_SEISAN_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_SEISAN_KBN_2.Enabled = false;

                    this.MyForm.SHIHARAI_DENPYO_KBN_2.Checked = true;   // 伝票発行無にする
                    this.MyForm.SHIHARAI_DENPYO_VALUE.Enabled = false;
                    this.MyForm.SHIHARAI_DENPYO_KBN_1.Enabled = false;
                    this.MyForm.SHIHARAI_DENPYO_KBN_2.Enabled = false;
                }

                if (!isUriage)  // 請求が無い場合
                {   // 請求をグレイアウト
                    //this.MyForm.L1.Enabled = false;
                    this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Enabled = false;
                    this.MyForm.SEIKYU_ZEIKEISAN_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_ZEIKEISAN_KBN_2.Enabled = false;
                    this.MyForm.SEIKYU_ZEIKEISAN_KBN_3.Enabled = false;
                    this.MyForm.SEIKYU_ZEI_VALUE.Enabled = false;
                    this.MyForm.SEIKYU_ZEI_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_ZEI_KBN_2.Enabled = false;
                    this.MyForm.SEIKYU_ZEI_KBN_3.Enabled = false;
                    this.MyForm.SEIKYU_TORIHIKI_VALUE.Enabled = false;
                    this.MyForm.SEIKYU_TORIHIKI_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_TORIHIKI_KBN_2.Enabled = false;
                    this.MyForm.SEIKYU_SEISAN_VALUE.Enabled = false;
                    this.MyForm.SEIKYU_SEISAN_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_SEISAN_KBN_2.Enabled = false;

                    this.MyForm.SEIKYU_DENPYO_KBN_2.Checked = true;   // 伝票発行無にする
                    this.MyForm.SEIKYU_DENPYO_VALUE.Enabled = false;
                    this.MyForm.SEIKYU_DENPYO_KBN_1.Enabled = false;
                    this.MyForm.SEIKYU_DENPYO_KBN_2.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 1025敬称
        /// </summary>
        private void SetKeisyou1025()
        {
            M_TORIHIKISAKI mtorihikisaki = (M_TORIHIKISAKI)TorihikisakiDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);
            if (mtorihikisaki != null)
            {
                this.MyForm.Keisyou_1.Text = mtorihikisaki.TORIHIKISAKI_KEISHOU1;
                this.MyForm.Keisyou_2.Text = mtorihikisaki.TORIHIKISAKI_KEISHOU2;
            }
        }
        /// <summary>
        /// 1019今回税額
        /// </summary>
        private bool SetKonkaiZeigaku1019()
        {
            //請求分用の税率取得
            string seikyuShouhizeiRate = string.Empty;
            decimal tempSeikyuShouhizeiRate = 0;

            // 請求消費税率取得
            if (!string.IsNullOrEmpty(this.MyForm.ParameterDTO.Uriage_Shouhizei_Rate)
                && decimal.TryParse(this.MyForm.ParameterDTO.Uriage_Shouhizei_Rate, out tempSeikyuShouhizeiRate))
            {
                seikyuShouhizeiRate = this.MyForm.ParameterDTO.Uriage_Shouhizei_Rate;
            }
            else
            {
                seikyuShouhizeiRate = ShouhizeiDao.GetSyohizei(this.MyForm.ParameterDTO.Uriage_Date);
            }


            if (string.IsNullOrEmpty(seikyuShouhizeiRate))
            {
                this.MyForm.SeikyuShouhizeiRate = "0";
            }
            else
            {
                this.MyForm.SeikyuShouhizeiRate = seikyuShouhizeiRate;
            }

            string shiharaiShouhizeiRate = string.Empty;
            decimal tempShiharaiShouhizeiRate = 0;

            // 支払消費税率取得
            if (!string.IsNullOrEmpty(this.MyForm.ParameterDTO.Shiharai_Shouhizei_Rate)
                && decimal.TryParse(this.MyForm.ParameterDTO.Shiharai_Shouhizei_Rate, out tempShiharaiShouhizeiRate))
            {
                shiharaiShouhizeiRate = this.MyForm.ParameterDTO.Shiharai_Shouhizei_Rate;
            }
            else
            {
                shiharaiShouhizeiRate = ShouhizeiDao.GetSyohizei(this.MyForm.ParameterDTO.Shiharai_Date);
            }

            //支払分用の税率取得
            if (string.IsNullOrEmpty(shiharaiShouhizeiRate))
            {
                this.MyForm.ShiharaiShouhizeiRate = "0";
            }
            else
            {
                this.MyForm.ShiharaiShouhizeiRate = shiharaiShouhizeiRate;
            }

            //請求税額計算
            if (!SetSeikyuKonkaiZeigaku())
            {
                return false;
            }
            //支払税額計算
            if (!SetShiharaiKonkaiZeigaku())
            {
                return false;
            }
            return true;
        }

        /// <summary>請求分今回取引(品名.税区分が無い明細)</summary>
        private decimal konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbn = 0;

        /// <summary>支払分今回取引(品名.税区分が無い明細)</summary>
        private decimal konkaiShiharaiTorihikiForEmptyHinmeiZeiKbn = 0;

        /// <summary>
        /// 請求税額計算
        /// </summary>
        internal bool SetSeikyuKonkaiZeigaku()
        {
            try
            {
                konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbn = 0;      // 初期化

                //請求分用の税率取得
                string seikyuShouhizeiRate = this.MyForm.SeikyuShouhizeiRate;
                M_TORIHIKISAKI_SEIKYUU dataTorihikiSeikyu = TorihikisakiSeikyuDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);
                int iSeikyuHasuCd = 0;
                if (!dataTorihikiSeikyu.TAX_HASUU_CD.Equals(System.Data.SqlTypes.SqlInt16.Null))
                {
                    iSeikyuHasuCd = dataTorihikiSeikyu.TAX_HASUU_CD.Value;
                }

                //請求
                Decimal dKingaku = 0;
                Decimal dUTax = 0;
                //★適格請求書の変更あり→税額は、税抜金額に対して一括課税
                //2.[税計算区分]が[1.伝票毎]の場合
                if (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text))
                {
                    // 初期化
                    this.MyForm.Seikyu_Konkai_Zeigaku.Text = Const.ConstClass.KIGAKU_0;
                    this.MyForm.Seikyu_Konkai_Kingaku.Text = Const.ConstClass.KIGAKU_0;

                    //品名外税、品名内税(税抜き)、品名税なし/外税の金額
                    decimal KonkaiKazeiKingaku = 0;

                    /**
                     * 品名税区分が設定されている明細の金額を計算
                     */
                    if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                    {
                        foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                        {
                            if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                            {
                                //出力伝票明細
                                SyuturyokuMeiseiDTOClass syuturyokuMeiseiDto = new SyuturyokuMeiseiDTOClass();

                                //税区分
                                string zeiKbn = string.Empty;

                                if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                                {
                                    zeiKbn = meiseiDto.ZeiKbn;

                                    if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                    {
                                        // 外税
                                        // 請求今回金額
                                        this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Kingaku.Text, meiseiDto.Kingaku));
                                        //税計算用
                                        KonkaiKazeiKingaku = KonkaiKazeiKingaku + Convert.ToDecimal(meiseiDto.Kingaku);
                                    }
                                    else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                                    {
                                        // 内税
                                        syuturyokuMeiseiDto.Hinmei_Cd = meiseiDto.Hinmei_Cd;
                                        syuturyokuMeiseiDto.Hinmei_Tax_Uchi = SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                                                    Convert.ToDecimal(meiseiDto.Kingaku),
                                                                                    Convert.ToDecimal(seikyuShouhizeiRate),
                                                                                    iSeikyuHasuCd).ToString());
                                        // 請求今回金額(内税を差し引いて加算)
                                        this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Kingaku.Text, (KigakuSubstr(meiseiDto.Kingaku, syuturyokuMeiseiDto.Hinmei_Tax_Uchi))));

                                        //税計算用
                                        KonkaiKazeiKingaku = KonkaiKazeiKingaku + Convert.ToDecimal(KigakuSubstr(meiseiDto.Kingaku, syuturyokuMeiseiDto.Hinmei_Tax_Uchi));
                                    }
                                    else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
                                    {
                                        // 非課税
                                        // 請求今回金額
                                        this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Kingaku.Text, meiseiDto.Kingaku));
                                    }
                                }
                                else
                                {
                                    //品名税区分なし
                                    //外税：税計算用金額に加算する、内税：存在しない、非課税：税計算用金額に加算しない
                                    if (Const.ConstClass.ZEI_KBN_1.Equals(this.MyForm.SEIKYU_ZEI_VALUE.Text))
                                    {
                                        //税計算用
                                        KonkaiKazeiKingaku = KonkaiKazeiKingaku + Convert.ToDecimal(meiseiDto.Kingaku);
                                    }
                                    // 請求今回金額
                                    this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Kingaku.Text, meiseiDto.Kingaku));
                                }
                            }
                        }
                    }

                    //消費税計算は、税計算用金額に対して、一括で行う
                    Decimal dTax = CalcZeiKbnShohiZei(Const.ConstClass.ZEI_KBN_1,
                                Convert.ToDecimal(KonkaiKazeiKingaku),
                                Convert.ToDecimal(seikyuShouhizeiRate),
                                iSeikyuHasuCd);
                    this.MyForm.Seikyu_Konkai_Zeigaku.Text = SetComma(FractionCalc(dTax, iSeikyuHasuCd).ToString());

                }
                //★適格請求書の変更あり→税額：0
                //3.[税計算区分]が[2.請求毎]の場合
                else if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text))
                {
                    this.MyForm.Seikyu_Konkai_Zeigaku.Text = Const.ConstClass.KIGAKU_0;
                    // 今回金額の再設定
                    dKingaku = Convert.ToDecimal(this.MyForm.ParameterDTO.Uriage_Amount_Total);
                    this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(dKingaku.ToString());
                    // 税区分を設定している品名がある場合は、その分の税額を表示する
                    if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                    {
                        foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                        {
                            if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                            {
                                decimal tempKingaku = Convert.ToDecimal(meiseiDto.Kingaku);

                                //税区分
                                string zeiKbn = string.Empty;
                                //税額
                                decimal dTax = 0;

                                if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                                {
                                    zeiKbn = meiseiDto.ZeiKbn;
                                }
                                else
                                {
                                    // 差引残高計算のため格納
                                    konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbn += tempKingaku;
                                }

                                if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                {
                                    // 外税
                                    //エラー表示用品目CDセット
                                    ErrHinmeiCD = ErrHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                                }
                                else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                                {
                                    // 内税
                                    dTax = CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(seikyuShouhizeiRate),
                                                iSeikyuHasuCd);
                                    // 今回金額の再設定
                                    this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Konkai_Kingaku.Text, dTax.ToString()));
                                }
                            }
                        }
                    }
                }
                //★適格請求書の変更あり→内税の金額・税額の算出方法変更
                //4.[税計算区分]が[3.明細毎]の場合
                else if (Const.ConstClass.ZEIKEISAN_KBN_3.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text))
                {
                    this.MyForm.Seikyu_Konkai_Zeigaku.Text = Const.ConstClass.KIGAKU_0;
                    //dKingaku = Convert.ToDecimal(this.MyForm.ParameterDTO.Uriage_Amount_Total);
                    //this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(dKingaku.ToString());
                    dKingaku = 0;
                    Decimal uchiKingaku = 0;
                    Decimal uchiZeigaku = 0;
                    if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                    {
                        foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                        {
                            if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                            {
                                //出力伝票明細
                                SyuturyokuMeiseiDTOClass syuturyokuMeiseiDto = new SyuturyokuMeiseiDTOClass();

                                //税区分
                                string zeiKbn = string.Empty;

                                if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                                {
                                    zeiKbn = meiseiDto.ZeiKbn;
                                }
                                else
                                {
                                    zeiKbn = this.MyForm.SEIKYU_ZEI_VALUE.Text;
                                }

                                if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                {
                                    // 外税
                                    syuturyokuMeiseiDto.Hinmei_Cd = meiseiDto.Hinmei_Cd;
                                    syuturyokuMeiseiDto.Hinmei_Tax_Soto = SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(seikyuShouhizeiRate),
                                                iSeikyuHasuCd).ToString());
                                    //請求分今回税額
                                    this.MyForm.Seikyu_Konkai_Zeigaku.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Zeigaku.Text, syuturyokuMeiseiDto.Hinmei_Tax_Soto));
                                    dKingaku = dKingaku + Decimal.Parse(meiseiDto.Kingaku);
                                }
                                else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                                {
                                    // 内税（税抜金額の加算）
                                    uchiKingaku = uchiKingaku + Convert.ToDecimal(meiseiDto.Kingaku) -
                                                Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(seikyuShouhizeiRate),
                                                iSeikyuHasuCd).ToString()));
                                }
                                else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
                                {
                                    // 非課税
                                    syuturyokuMeiseiDto.Hinmei_Cd = meiseiDto.Hinmei_Cd;
                                    syuturyokuMeiseiDto.Hinmei_Tax_Soto = Const.ConstClass.KIGAKU_0;
                                    syuturyokuMeiseiDto.Hinmei_Tax_Uchi = Const.ConstClass.KIGAKU_0;
                                    dKingaku = dKingaku + Decimal.Parse(meiseiDto.Kingaku);
                                }
                                if (this.MyForm.ParameterDTO.Seikyu_Out_Tenpyo_Cnt == null)
                                {
                                    this.MyForm.ParameterDTO.Seikyu_Out_Tenpyo_Cnt = new List<SyuturyokuMeiseiDTOClass>();
                                }
                                //請求出力伝票明細リストに追加
                                this.MyForm.ParameterDTO.Seikyu_Out_Tenpyo_Cnt.Add(syuturyokuMeiseiDto);
                            }
                        }

                        uchiZeigaku = CalcZeiKbnShohiZei(Const.ConstClass.ZEI_KBN_1
                                                        , Convert.ToDecimal(uchiKingaku)
                                                        , Convert.ToDecimal(seikyuShouhizeiRate)
                                                        , iSeikyuHasuCd);
                        //請求分今回税額
                        this.MyForm.Seikyu_Konkai_Zeigaku.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Zeigaku.Text, uchiZeigaku.ToString()));

                        //今回金額の再設定
                        this.MyForm.Seikyu_Konkai_Kingaku.Text = SetComma(KigakuAdd(dKingaku.ToString(), uchiKingaku.ToString()));
                    }
                }
                //1020今回取引
                //今回金額+今回税額
                this.MyForm.Seikyu_Konkai_Rorihiki.Text = SetComma(KigakuAdd(this.MyForm.Seikyu_Konkai_Kingaku.Text, this.MyForm.Seikyu_Konkai_Zeigaku.Text));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetSeikyuKonkaiZeigaku", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSeikyuKonkaiZeigaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 支払税額計算
        /// </summary>
        internal bool SetShiharaiKonkaiZeigaku()
        {
            try
            {
                konkaiShiharaiTorihikiForEmptyHinmeiZeiKbn = 0;      // 初期化

                //支払分用の税率取得
                string shiharaiShouhizeiRate = this.MyForm.ShiharaiShouhizeiRate;
                M_TORIHIKISAKI_SHIHARAI dataTorihikiShiharai = TorihikisakiShiharaiDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);
                int iShiharaiHasuCd = 0;

                if (!dataTorihikiShiharai.TAX_HASUU_CD.Equals(System.Data.SqlTypes.SqlInt16.Null))
                {
                    iShiharaiHasuCd = dataTorihikiShiharai.TAX_HASUU_CD.Value;
                }
                //請求
                Decimal dKingaku = 0;
                Decimal dUTax = 0;
                //★適格請求書の変更あり→税額は、税抜金額に対して一括課税
                //2.[税計算区分]が[1.伝票毎]の場合
                if (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text))
                {
                    // 初期化
                    this.MyForm.Shiharai_Konkai_Zeigaku.Text = Const.ConstClass.KIGAKU_0;
                    this.MyForm.Shiharai_Konkai_Kingaku.Text = Const.ConstClass.KIGAKU_0;

                    //品名外税、品名内税(税抜き)、品名税なし/外税の金額
                    decimal KonkaiKazeiKingaku = 0;

                    /**
                     * 品名税区分が設定されている明細の金額を計算
                     */
                    if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                    {
                        foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                        {
                            if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                            {
                                //出力伝票明細
                                SyuturyokuMeiseiDTOClass syuturyokuMeiseiDto = new SyuturyokuMeiseiDTOClass();

                                //税区分
                                string zeiKbn = string.Empty;

                                if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                                {
                                    zeiKbn = meiseiDto.ZeiKbn;

                                    if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                    {
                                        // 外税
                                        // 今回金額の再設定
                                        this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Kingaku.Text, meiseiDto.Kingaku));

                                        //税計算用
                                        KonkaiKazeiKingaku = KonkaiKazeiKingaku + Convert.ToDecimal(meiseiDto.Kingaku);

                                    }
                                    else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                                    {
                                        // 内税
                                        syuturyokuMeiseiDto.Hinmei_Cd = meiseiDto.Hinmei_Cd;
                                        syuturyokuMeiseiDto.Hinmei_Tax_Uchi = SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                                                    Convert.ToDecimal(meiseiDto.Kingaku),
                                                                                    Convert.ToDecimal(shiharaiShouhizeiRate),
                                                                                    iShiharaiHasuCd).ToString());
                                        // 今回金額の再設定
                                        this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Kingaku.Text, KigakuSubstr(meiseiDto.Kingaku, syuturyokuMeiseiDto.Hinmei_Tax_Uchi)));

                                        //税計算用
                                        KonkaiKazeiKingaku = KonkaiKazeiKingaku + Convert.ToDecimal(KigakuSubstr(meiseiDto.Kingaku, syuturyokuMeiseiDto.Hinmei_Tax_Uchi));
                                    }
                                    else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
                                    {
                                        // 非課税
                                        // 今回金額の再設定
                                        this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Kingaku.Text, meiseiDto.Kingaku));
                                    }
                                }
                                else
                                {
                                    //品名税区分なし
                                    //外税：税計算用金額に加算する、内税：存在しない、非課税：税計算用金額に加算しない
                                    if (Const.ConstClass.ZEI_KBN_1.Equals(this.MyForm.SHIHARAI_ZEI_VALUE.Text))
                                    {
                                        //税計算用
                                        KonkaiKazeiKingaku = KonkaiKazeiKingaku + Convert.ToDecimal(meiseiDto.Kingaku);
                                    }
                                    // 請求今回金額
                                    this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Kingaku.Text, meiseiDto.Kingaku));
                                }
                            }
                        }
                    }

                    //消費税計算は、税計算用金額に対して、一括で行う
                    Decimal dTax = CalcZeiKbnShohiZei(Const.ConstClass.ZEI_KBN_1,
                                Convert.ToDecimal(KonkaiKazeiKingaku),
                                Convert.ToDecimal(shiharaiShouhizeiRate),
                                iShiharaiHasuCd);
                    this.MyForm.Shiharai_Konkai_Zeigaku.Text = SetComma(FractionCalc(dTax, iShiharaiHasuCd).ToString());

                }
                //★適格請求書の変更あり→税額：0
                //3.[税計算区分]が[2.請求毎]の場合
                else if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text))
                {
                    this.MyForm.Shiharai_Konkai_Zeigaku.Text = Const.ConstClass.KIGAKU_0;
                    // 今回金額の再設定
                    dKingaku = Convert.ToDecimal(this.MyForm.ParameterDTO.Shiharai_Amount_Total);
                    this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(dKingaku.ToString());

                    // 税区分を設定している品名がある場合は、その分の税額を表示する
                    if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                    {
                        foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                        {
                            if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                            {
                                decimal tempKingaku = Convert.ToDecimal(meiseiDto.Kingaku);

                                //税区分
                                string zeiKbn = string.Empty;
                                //税額
                                decimal dTax = 0;

                                if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                                {
                                    zeiKbn = meiseiDto.ZeiKbn;
                                }
                                else
                                {
                                    // 差引残高計算のため格納
                                    konkaiShiharaiTorihikiForEmptyHinmeiZeiKbn += tempKingaku;
                                }

                                if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                {
                                    // 外税
                                    //エラー表示用品目CDセット
                                    ErrHinmeiCD = ErrHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                                }
                                else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                                {
                                    // 内税
                                    dTax = CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(shiharaiShouhizeiRate),
                                                iShiharaiHasuCd);
                                    // 今回金額の再設定
                                    this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Konkai_Kingaku.Text, dTax.ToString()));
                                }
                            }
                        }
                    }
                }
                //★適格請求書の変更あり→内税の金額・税額の算出方法変更
                //4.[税計算区分]が[3.明細毎]の場合
                else if (Const.ConstClass.ZEIKEISAN_KBN_3.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text))
                {
                    this.MyForm.Shiharai_Konkai_Zeigaku.Text = Const.ConstClass.KIGAKU_0;
                    //dKingaku = Convert.ToDecimal(this.MyForm.ParameterDTO.Shiharai_Amount_Total);
                    //this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(dKingaku.ToString());
                    dKingaku = 0;
                    Decimal uchiKingaku = 0;
                    Decimal uchiZeigaku = 0;
                    if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                    {
                        foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                        {
                            if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                            {
                                //出力伝票明細
                                SyuturyokuMeiseiDTOClass syuturyokuMeiseiDto = new SyuturyokuMeiseiDTOClass();

                                //税区分
                                string zeiKbn = string.Empty;

                                if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                                {
                                    zeiKbn = meiseiDto.ZeiKbn;
                                }
                                else
                                {
                                    zeiKbn = this.MyForm.SHIHARAI_ZEI_VALUE.Text;
                                }

                                if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                {
                                    // 外税
                                    syuturyokuMeiseiDto.Hinmei_Cd = meiseiDto.Hinmei_Cd;
                                    syuturyokuMeiseiDto.Hinmei_Tax_Soto = SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                                                Convert.ToDecimal(shiharaiShouhizeiRate),
                                                                                iShiharaiHasuCd).ToString());
                                    //支払分今回税額
                                    this.MyForm.Shiharai_Konkai_Zeigaku.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Zeigaku.Text, syuturyokuMeiseiDto.Hinmei_Tax_Soto));
                                    dKingaku = dKingaku + Decimal.Parse(meiseiDto.Kingaku);
                                }
                                else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                                {
                                    // 内税（税抜金額の加算）
                                    uchiKingaku = uchiKingaku + Convert.ToDecimal(meiseiDto.Kingaku) -
                                                Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(shiharaiShouhizeiRate),
                                                iShiharaiHasuCd).ToString()));
                                }
                                else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
                                {
                                    // 非課税
                                    syuturyokuMeiseiDto.Hinmei_Cd = meiseiDto.Hinmei_Cd;
                                    syuturyokuMeiseiDto.Hinmei_Tax_Soto = Const.ConstClass.KIGAKU_0;
                                    syuturyokuMeiseiDto.Hinmei_Tax_Uchi = Const.ConstClass.KIGAKU_0;
                                    dKingaku = dKingaku + Decimal.Parse(meiseiDto.Kingaku);
                                }
                                if (this.MyForm.ParameterDTO.Shiharai_Out_Tenpyo_Cnt == null)
                                {
                                    this.MyForm.ParameterDTO.Shiharai_Out_Tenpyo_Cnt = new List<SyuturyokuMeiseiDTOClass>();
                                }
                                //支払出力伝票明細リストに追加
                                this.MyForm.ParameterDTO.Shiharai_Out_Tenpyo_Cnt.Add(syuturyokuMeiseiDto);
                            }
                        }
                        uchiZeigaku = CalcZeiKbnShohiZei(Const.ConstClass.ZEI_KBN_1
                                , Convert.ToDecimal(uchiKingaku)
                                , Convert.ToDecimal(shiharaiShouhizeiRate)
                                , iShiharaiHasuCd);
                        //請求分今回税額
                        this.MyForm.Shiharai_Konkai_Zeigaku.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Zeigaku.Text, uchiZeigaku.ToString()));

                        //今回金額の再設定
                        this.MyForm.Shiharai_Konkai_Kingaku.Text = SetComma(KigakuAdd(dKingaku.ToString(), uchiKingaku.ToString()));
                    }
                }
                //1020今回取引
                //今回金額+今回税額
                this.MyForm.Shiharai_Konkai_Rorihiki.Text = SetComma(KigakuAdd(this.MyForm.Shiharai_Konkai_Kingaku.Text, this.MyForm.Shiharai_Konkai_Zeigaku.Text));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetShiharaiKonkaiZeigaku", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiKonkaiZeigaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 1022入出金金額・差引残高(請求)
        /// </summary>
        public bool SetSeikyuKingaku1022()
        {
            try
            {
                if (valueInitFlag == true)
                {
                    return true;
                }
                string denshuKbn = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn;
                string seikyuzandaka = string.Empty;

                //if (((Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text)) && (Const.ConstClass.ZEI_KBN_1.Equals(this.MyForm.SEIKYU_ZEI_VALUE.Text)))
                //    && (this.GetZandakaJidouKBN(denshuKbn) != 2 || this.GetZandakaClickFlg))
                //{
                //    //請求毎外税の場合
                //    //残高計算について、さらに当データを混ぜて請求税額を算出する
                //    DateTime uriageDate;

                //    if (DateTime.TryParse(this.MyForm.ParameterDTO.Uriage_Date, out uriageDate))
                //    {
                //        // konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbnは品名税区分が設定されていないものだけをセットする
                //        // 品名税区分が設定されているものは別で渡してあげる。(konkaiTorihiki)
                //        decimal konkaiTorihiki = Convert.ToDecimal(this.MyForm.Seikyu_Konkai_Rorihiki.Text) - konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbn;
                //        seikyuzandaka = SetComma(
                //                            GetSeikyuZengetsuZandaka(
                //                                this.MyForm.ParameterDTO.Torihikisaki_Cd,
                //                                uriageDate,
                //                                konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbn,
                //                                konkaiTorihiki,
                //                                denshuKbn,
                //                                this.MyForm.ParameterDTO.System_Id).ToString()
                //                        );
                //    }
                //    else
                //    {
                //        //起算日がないため取得できない/当データを足すだけの処理にしておく。日付がないから税率も取得できない
                //        seikyuzandaka = KigakuAdd(this.MyForm.Seikyu_Zenkai_Zentaka.Text, this.MyForm.Seikyu_Konkai_Rorihiki.Text);
                //    }
                //}
                //else
                //{
                    //それ以外、請求毎内税・請求毎非課税・伝票毎外税・伝票毎内税・伝票毎非課税・明細毎外税・明細毎内税・明細毎非課税の場合
                    //繰越算について、当データを足すだけ
                    seikyuzandaka = KigakuAdd(this.MyForm.Seikyu_Zenkai_Zentaka.Text, this.MyForm.Seikyu_Konkai_Rorihiki.Text);
                //}

                if (this.MyForm.SEIKYU_SEISAN_VALUE.Text == Const.ConstClass.SEISAN_KBN_1)
                {
                    //入出金金額
                    this.MyForm.Seikyu_Nyusyu_Kingaku.Text = SetComma(KigakuSubstr(seikyuzandaka, this.MyForm.Seikyu_Sousatu_Kingaku.Text));
                }
                else
                {
                    if (this.MyForm.SEIKYU_TORIHIKI_VALUE.Text.Equals(Const.ConstClass.TORIHIKI_KBN_1))
                    {
                        // 取引区分が現金の場合は、入出金金額＝今回取引額
                        this.MyForm.Seikyu_Nyusyu_Kingaku.Text = this.MyForm.Seikyu_Konkai_Rorihiki.Text;
                    }
                    //差引残高(前回残高+今回取引-相殺金額-入出金金額)
                    this.MyForm.Seikyu_Sagaku_Zentaka.Text = SetComma(KigakuSubstr(seikyuzandaka, KigakuAdd(this.MyForm.Seikyu_Sousatu_Kingaku.Text, this.MyForm.Seikyu_Nyusyu_Kingaku.Text)));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSeikyuKingaku1022", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 1022入出金金額・差引残高(支払)
        /// </summary>
        public bool SetShiharaiKingaku1022()
        {
            try
            {
                if (valueInitFlag == true)
                {
                    return true;
                }

                string denshuKbn = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn;
                string shiharaizandaka = string.Empty;

                //if (((Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text)) && (Const.ConstClass.ZEI_KBN_1.Equals(this.MyForm.SHIHARAI_ZEI_VALUE.Text)))
                //    && ((this.GetZandakaJidouKBN(denshuKbn) != 2 || this.GetZandakaClickFlg)))
                //{
                //    //精算毎外税の場合
                //    //残高計算について、さらに当データを混ぜて精算税額を算出する
                //    DateTime shiharaiDate;

                //    if (DateTime.TryParse(this.MyForm.ParameterDTO.Shiharai_Date, out shiharaiDate))
                //    {
                //        // konkaiSeikyuuTorihikiForEmptyHinmeiZeiKbnは品名税区分が設定されていないものだけをセットする
                //        // 品名税区分が設定されているものは別で渡してあげる。(konkaiTorihiki)
                //        decimal konkaiTorihiki = Convert.ToDecimal(this.MyForm.Shiharai_Konkai_Rorihiki.Text) - konkaiShiharaiTorihikiForEmptyHinmeiZeiKbn;
                //        shiharaizandaka = SetComma(GetShiharaiZengetsuZandaka(this.MyForm.ParameterDTO.Torihikisaki_Cd, shiharaiDate, konkaiShiharaiTorihikiForEmptyHinmeiZeiKbn, konkaiTorihiki, denshuKbn, this.MyForm.ParameterDTO.System_Id).ToString());
                //    }
                //    else
                //    {
                //        //起算日がないため取得できない/当データを足すだけの処理にしておく。日付がないから税率も取得できない
                //        shiharaizandaka = KigakuAdd(this.MyForm.Shiharai_Zenkai_Zentaka.Text, this.MyForm.Shiharai_Konkai_Rorihiki.Text);
                //    }
                //}
                //else
                //{
                    //それ以外、精算毎内税・精算毎非課税・伝票毎外税・伝票毎内税・伝票毎非課税・明細毎外税・明細毎内税・明細毎非課税の場合
                    //繰越算について、当データを足すだけ
                    shiharaizandaka = KigakuAdd(this.MyForm.Shiharai_Zenkai_Zentaka.Text, this.MyForm.Shiharai_Konkai_Rorihiki.Text);
                //}

                if (this.MyForm.SHIHARAI_SEISAN_VALUE.Text == Const.ConstClass.SEISAN_KBN_1)
                {
                    //入出金金額
                    this.MyForm.Shiharai_Nyusyu_Kingaku.Text = SetComma(KigakuSubstr(shiharaizandaka, this.MyForm.Shiharai_Sousatu_Kingaku.Text));
                }
                else
                {
                    if (this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text.Equals(Const.ConstClass.TORIHIKI_KBN_1))
                    {
                        // 取引区分が現金の場合は、入出金金額＝今回取引額
                        this.MyForm.Shiharai_Nyusyu_Kingaku.Text = this.MyForm.Shiharai_Konkai_Rorihiki.Text;
                    }
                    //差引残高(前回残高+今回取引-相殺金額-入出金金額)
                    this.MyForm.Shiharai_Sagaku_Zentaka.Text = SetComma(KigakuSubstr(shiharaizandaka, KigakuAdd(this.MyForm.Shiharai_Sousatu_Kingaku.Text, this.MyForm.Shiharai_Nyusyu_Kingaku.Text)));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiKingaku1022", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// システム設定の残高自動区分を取得します
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns>伝種区分に対応したシステム設定の残高自動区分</returns>
        private int GetZandakaJidouKBN(string denshuKbn)
        {
            // システム設定
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            var zandakaJidouKbn = 0;
            switch (denshuKbn)
            {
                // 受入の場合
                case ("1"):
                    zandakaJidouKbn = mSysInfo.UKEIRE_ZANDAKA_JIDOU_KBN.Value == null ? 0 : mSysInfo.UKEIRE_ZANDAKA_JIDOU_KBN.Value;
                    break;

                // 出荷の場合
                case ("2"):
                    zandakaJidouKbn = mSysInfo.SHUKKA_ZANDAKA_JIDOU_KBN.Value == null ? 0 : mSysInfo.SHUKKA_ZANDAKA_JIDOU_KBN.Value;
                    break;

                // 売上/支払の場合
                case ("3"):
                    zandakaJidouKbn = mSysInfo.UR_SH_ZANDAKA_JIDOU_KBN.Value == null ? 0 : mSysInfo.UR_SH_ZANDAKA_JIDOU_KBN.Value;
                    break;
            }

            return zandakaJidouKbn;
        }

        /// <summary>
        /// 1017 前回残高設定
        /// </summary>
        public bool SetZenkaiZentaka1017()
        {
            try
            {
                if (valueInitFlag == true)
                {
                    return true;
                }

                DateTime uriageDate;
                DateTime shiharaiDate;
                string denshuKbn = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn;

                // 初期化
                this.MyForm.Seikyu_Zenkai_Zentaka.Text = "0";
                this.MyForm.Shiharai_Zenkai_Zentaka.Text = "0";

                // システム設定の残高自動区分＝２.しない の場合（F1が押下されたときは前回残高設定）
                if (this.GetZandakaJidouKBN(denshuKbn) == 2 && !this.GetZandakaClickFlg)
                {
                    return true;
                }

                if (String.IsNullOrEmpty(this.MyForm.ParameterDTO.System_Id) || this.MyForm.ParameterDTO.Tairyuu_Kbn)
                {
                    if (DateTime.TryParse(this.MyForm.ParameterDTO.Uriage_Date, out uriageDate))
                    {
                        try
                        {
                            this.MyForm.Seikyu_Zenkai_Zentaka.Text = SetComma(GetSeikyuZengetsuZandaka(this.MyForm.ParameterDTO.Torihikisaki_Cd, uriageDate, 0, 0, denshuKbn, this.MyForm.ParameterDTO.System_Id).ToString());
                        }
                        catch (Exception ex)
                        {
                            this.MyForm.Seikyu_Zenkai_Zentaka.Text = "0";
                        };
                    }
                    if (DateTime.TryParse(this.MyForm.ParameterDTO.Shiharai_Date, out shiharaiDate))
                    {
                        try
                        {
                            this.MyForm.Shiharai_Zenkai_Zentaka.Text = SetComma(GetShiharaiZengetsuZandaka(this.MyForm.ParameterDTO.Torihikisaki_Cd, shiharaiDate, 0, 0, denshuKbn, this.MyForm.ParameterDTO.System_Id).ToString());
                        }
                        catch (Exception ex)
                        {
                            this.MyForm.Shiharai_Zenkai_Zentaka.Text = "0";
                        };
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetZenkaiZentaka1017", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZenkaiZentaka1017", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定された取引先CDの前月繰越残高を取得
        /// G064\Accessor\DBAccessor.cs - GetZengetsuZandaka() を参照
        /// リアルタイム残高は多少の改変が発生
        /// ：今回伝票額を含むか含まないか（→請求毎税の算出）　パラメータ konkaiKingakuForEmptyZeiKbnCd により加算
        /// ：[当日から見た最後の締データ]＋[当日から見た最後の締翌日～前日までの伝票データ]→[当日から見た最後の締データ]＋[当日の段階で未締の伝票データ]
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="konkaiKingakuForEmptyZeiKbnCd">今回伝票額(品名.税区分CDが無しの金額)</param>
        /// <param name="konkaiKingaku">今回取引額(品名.税区分CDが有りの金額)</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="decimal">前月繰越残高</returns>
        private decimal GetSeikyuZengetsuZandaka(string torihikisakiCD, DateTime startDay, decimal konkaiKingakuForEmptyZeiKbnCd, decimal konkaiKingaku, string densyuKbn, string systemID)
        {
            decimal zandaka = 0;
            decimal seikyuuZandaka = 0;

            // Dateから日付を文字列にて取得
            string sDay = startDay.Date.ToString();

            DataTable table = new DataTable();
            DataTable uriageTbl = new DataTable();
            M_TORIHIKISAKI_SEIKYUU mtorihikisakiseikyuu = new M_TORIHIKISAKI_SEIKYUU();

            if (this.GetZandakaClickFlg)
            {
                // 指定された取引先CDの開始伝票日付より直近かつ請求番号が最大の請求データから請求差引残高、請求日付を抽出
                table = this.SeikyuuDenpyouDao.GetSeikyuZenkaiZentaka(torihikisakiCD, sDay);

                // 取引先請求情報取得                //取引先_請求情報マスタ
                mtorihikisakiseikyuu = (M_TORIHIKISAKI_SEIKYUU)TorihikisakiSeikyuDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);

                // 請求差引残高を基に繰越残高取得
                if (table.Rows.Count != 0)
                {
                    // 請求差引残高取得
                    // ※直近の請求データのため、該当する取引先は単一
                    seikyuuZandaka = decimal.Parse(table.Rows[0]["SEIKYUU_ZANDAKA"].ToString());
                }
                else
                {
                    if (!mtorihikisakiseikyuu.KAISHI_URIKAKE_ZANDAKA.IsNull)
                    {
                        // 請求差引残高が取得出来なかった場合、取引先請求情報より開始売掛残高を前月繰越残高とする
                        seikyuuZandaka = decimal.Parse(mtorihikisakiseikyuu.KAISHI_URIKAKE_ZANDAKA.ToString());
                    }
                }

                // 当日までの未締の売上/入金データテーブルの取得(単一取引先CD)
                uriageTbl = this.SeikyuuDenpyouDao.GetSeikyuIchiranData(torihikisakiCD, torihikisakiCD, sDay, sDay, densyuKbn, systemID);
            }
            string oldDenNum = string.Empty;
            string oldDenshu = string.Empty;
            decimal oldshohizeirate = 100;

            // 各金額を積算
            decimal uriKin = 0;　       //品名外税金額、伝票毎外税金額、明細毎外税金額(A+F+J)
            decimal uriSotoKin = 0;     //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)
            decimal meiZei = 0;         //品名外税、明細毎外税(B+K)
            decimal denZei = 0;         //伝票毎外税(G)
            decimal nyuKin = 0;
            decimal hikazeikin = 0;     //非課税金額(E+I+N+Q)
            decimal tempkazeikin = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)→別途税率を加算用
            decimal kazeikin_tax = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)の税率
            //税率でソート
            foreach (DataRow row in uriageTbl.Rows)
            {
                if ("10" != row["DENSHU_KBN"].ToString())
                {
                    //初回
                    if (oldshohizeirate == 100)
                    {
                        oldshohizeirate = decimal.Parse(row["URIAGE_SHOUHIZEI_RATE"].ToString());
                    }

                    //税率が変わった場合、税計算を行う
                    if (oldshohizeirate != decimal.Parse(row["URIAGE_SHOUHIZEI_RATE"].ToString()))
                    {
                        //税計算
                        //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
                        kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakiseikyuu.TAX_HASUU_CD.ToString()));
                        tempkazeikin = 0;
                        oldshohizeirate = decimal.Parse(row["URIAGE_SHOUHIZEI_RATE"].ToString());
                    }

                    // 売上金額を積算
                    if (false == string.IsNullOrEmpty(row["URIAGE_KINGAKU"].ToString()))
                    {
                        // 品名税区分が無く、税計算区分が請求毎税、税区分が外税で登録されていた場合、その金額は請求毎外税の算出対象となる
                        if (true == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            switch (row["ZEI_KBN_CD"].ToString())
                            {
                                case Const.ConstClass.ZEI_KBN_1://外税
                                    if (Const.ConstClass.ZEIKEISAN_KBN_2 == row["ZEI_KEISAN_KBN_CD"].ToString())
                                    {
                                        //請求毎 O
                                        uriSotoKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                        tempkazeikin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        //伝票毎・明細毎　F+J
                                        uriKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    }
                                    break;
                                case Const.ConstClass.ZEI_KBN_2://内税 L
                                    uriSotoKin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case Const.ConstClass.ZEI_KBN_3://非課税 I+N+Q
                                    hikazeikin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            //品名税区分あり
                            switch (row["HINMEI_ZEI_KBN_CD"].ToString())
                            {
                                case Const.ConstClass.ZEI_KBN_1://外税　A
                                    uriKin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    break;
                                case Const.ConstClass.ZEI_KBN_2://内税　C
                                    uriSotoKin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["URIAGE_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case Const.ConstClass.ZEI_KBN_3://非課税　E
                                    hikazeikin += decimal.Parse(row["URIAGE_KINGAKU"].ToString());
                                    break;
                            }
                        }
                    }

                    // 明細毎税を積算 B+K
                    if (false == string.IsNullOrEmpty(row["SHOUHI_SOTO_ZEI"].ToString()))
                    {
                        meiZei += decimal.Parse(row["SHOUHI_SOTO_ZEI"].ToString());
                    }

                    // 伝票毎税を積算 G
                    if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(row["DENPYOU_MAI_SOTO_ZEI"].ToString()))
                        {
                            denZei += decimal.Parse(row["DENPYOU_MAI_SOTO_ZEI"].ToString());
                        }
                    }
                }
                else
                {
                    // 入金金額を積算
                    if (false == string.IsNullOrEmpty(row["NYUUKIN_KINGAKU"].ToString()))
                    {
                        nyuKin += decimal.Parse(row["NYUUKIN_KINGAKU"].ToString());
                    }
                }

                // 比較値を更新
                oldDenNum = row["DENPYOU_NUMBER"].ToString();
                oldDenshu = row["DENSHU_KBN"].ToString();
            }

            //税計算
            //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
            kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakiseikyuu.TAX_HASUU_CD.ToString()));

            // 繰越残高を算出
            // ((請求差引残高＋非課税金額(E+I+N+Q)+売上金額(A+F+J)＋明細毎税(B+K)＋伝票毎税(G)＋税計算ありの金額(C+L+O)＋税計算ありの金額の消費税((C+L+O)*税率))‐入金額)
            zandaka = (seikyuuZandaka + hikazeikin + uriKin + meiZei + denZei + uriSotoKin + kazeikin_tax) - nyuKin;

            return zandaka;
        }

        /// <summary>
        /// 指定された取引先CDの前月繰越残高を取得
        /// G073\Accessor\DBAccessor.cs - GetZengetsuZandaka() と同等とすること（名前空間や関数名は伝票発行画面による独自実装）
        /// リアルタイム残高は多少の改変が発生
        /// ：今回伝票額を含むか含まないか（→請求毎税の算出）　パラメータ konkaiKingakuForEmptyZeiKbnCd により加算
        /// ：[当日から見た最後の締データ]＋[当日から見た最後の締翌日～前日までの伝票データ]→[当日から見た最後の締データ]＋[当日の段階で未締の伝票データ]
        /// ：[当日の段階で未締の伝票データ]のうち、自身を含まない
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <param name="startDay">開始伝票日付</param>
        /// <param name="konkaiKingakuForEmptyZeiKbnCd">今回伝票額(品名.税区分CDが無しの金額)</param>
        /// <param name="konkaiKingaku">今回取引額(品名.税区分CDが有りの金額)</param>
        /// <param name="densyuKbn">伝種区分</param>
        /// <param name="systemID">システムID</param>
        /// <returns name="decimal">前月繰越残高</returns>
        private decimal GetShiharaiZengetsuZandaka(string torihikisakiCD, DateTime startDay, decimal konkaiKingakuForEmptyZeiKbnCd, decimal konkaiKingaku, string densyuKbn, string systemID)
        {
            decimal zandaka = 0;
            decimal seisanZandaka = 0;

            // Dateから日付を文字列にて取得
            string sDay = startDay.Date.ToString();

            DataTable table = new DataTable();
            DataTable shiharaiTbl = new DataTable();
            M_TORIHIKISAKI_SHIHARAI mtorihikisakishiharai = new M_TORIHIKISAKI_SHIHARAI();

            if (this.GetZandakaClickFlg)
            {
                // 指定された取引先CDの開始伝票日付より直近かつ精算番号が最大の精算データから精算差引残高、精算日付を抽出
                table = this.SeikyuuDenpyouDao.GetShiharaiZenkaiZentaka(torihikisakiCD, sDay);

                // 取引先支払情報取得    //取引先_支払情報マスタ
                mtorihikisakishiharai = (M_TORIHIKISAKI_SHIHARAI)TorihikisakiShiharaiDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);

                // 精算差引残高を基に繰越残高取得
                if (table.Rows.Count != 0)
                {
                    // 精算差引残高取得
                    // ※直近の精算データのため、該当する取引先は単一
                    seisanZandaka = decimal.Parse(table.Rows[0]["SEISAN_ZANDAKA"].ToString());
                }
                else
                {
                    if (!mtorihikisakishiharai.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                    {
                        // 精算差引残高が取得出来なかった場合、取引先支払情報より開始売掛残高を前月繰越残高とする
                        seisanZandaka = decimal.Parse(mtorihikisakishiharai.KAISHI_KAIKAKE_ZANDAKA.ToString());
                    }
                }

                // 当日までの未締の支払/出金データテーブルの取得(単一取引先CD)
                shiharaiTbl = this.SeikyuuDenpyouDao.GetShiharaiIchiranData(torihikisakiCD, torihikisakiCD, sDay, sDay, densyuKbn, systemID);
            }

            string oldDenNum = string.Empty;
            string oldDenshu = string.Empty;
            decimal oldshohizeirate = 100;

            // 各金額を積算
            decimal shiKin = 0;         //品名外税金額、伝票毎外税金額、明細毎外税金額(A+F+J)
            decimal shiSotoKin = 0;     //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)
            decimal meiZei = 0;         //品名外税、明細毎外税(B+K)
            decimal denZei = 0;         //伝票毎外税(G)
            decimal shuKin = 0;
            decimal hikazeikin = 0;     //非課税金額(E+I+N+Q)
            decimal tempkazeikin = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)→別途税率を加算用
            decimal kazeikin_tax = 0;   //品名内税金額、明細毎内税金額、請求毎外税金額(C+L+O)の税率

            //税率でソート
            foreach (DataRow row in shiharaiTbl.Rows)
            {
                if ("20" != row["DENSHU_KBN"].ToString())
                {
                    //初回
                    if (oldshohizeirate == 100)
                    {
                        oldshohizeirate = decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                    }

                    //税率が変わった場合、税計算を行う
                    if (oldshohizeirate != decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                    {
                        //税計算
                        //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
                        kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakishiharai.TAX_HASUU_CD.ToString()));
                        tempkazeikin = 0;
                        oldshohizeirate = decimal.Parse(row["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                    }

                    // 支払金額を積算
                    if (false == string.IsNullOrEmpty(row["SHIHARAI_KINGAKU"].ToString()))
                    {
                        // 品名税区分が無く、税計算区分が請求毎税、税区分が外税で登録されていた場合、その金額は請求毎外税の算出対象となる
                        if (true == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            switch (row["ZEI_KBN_CD"].ToString())
                            {
                                case Const.ConstClass.ZEI_KBN_1: //外税
                                    if (Const.ConstClass.ZEIKEISAN_KBN_2 == row["ZEI_KEISAN_KBN_CD"].ToString())
                                    {
                                        //請求毎 O
                                        shiSotoKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                        tempkazeikin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        //伝票毎・明細毎　F+J
                                        shiKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    }
                                    break;
                                case Const.ConstClass.ZEI_KBN_2: //内税L
                                    shiSotoKin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case Const.ConstClass.ZEI_KBN_3: //非課税I+N+Q
                                    hikazeikin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            //品名税区分あり
                            switch (row["HINMEI_ZEI_KBN_CD"].ToString())
                            {
                                case Const.ConstClass.ZEI_KBN_1://外税　A
                                    shiKin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    break;
                                case Const.ConstClass.ZEI_KBN_2://内税　C
                                    shiSotoKin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    tempkazeikin += decimal.Parse(KigakuSubstr(row["SHIHARAI_KINGAKU"].ToString(), row["SHOUHIZEI"].ToString()));
                                    break;
                                case Const.ConstClass.ZEI_KBN_3://非課税　E
                                    hikazeikin += decimal.Parse(row["SHIHARAI_KINGAKU"].ToString());
                                    break;
                            }
                        }
                    }

                    // 明細毎税を積算 B+K
                    if (false == string.IsNullOrEmpty(row["SHOUHI_SOTO_ZEI"].ToString()))
                    {
                        meiZei += decimal.Parse(row["SHOUHI_SOTO_ZEI"].ToString());
                    }

                    // 伝票毎税を積算 G
                    if ((oldDenNum != row["DENPYOU_NUMBER"].ToString()) || (oldDenshu != row["DENSHU_KBN"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(row["DENPYOU_MAI_SOTO_ZEI"].ToString()))
                        {
                            denZei += decimal.Parse(row["DENPYOU_MAI_SOTO_ZEI"].ToString());
                        }
                    }
                }
                else
                {
                    // 出金金額を積算
                    if (false == string.IsNullOrEmpty(row["SHUKKIN_KINGAKU"].ToString()))
                    {
                        shuKin += decimal.Parse(row["SHUKKIN_KINGAKU"].ToString());
                    }
                }

                // 比較値を更新
                oldDenNum = row["DENPYOU_NUMBER"].ToString();
                oldDenshu = row["DENSHU_KBN"].ToString();
            }

            //税計算
            //tempkazeikinに対して税率を算出し、kazeikin_taxに加算、tempkazeikinは初期化
            kazeikin_tax += FractionCalc(tempkazeikin * oldshohizeirate, int.Parse(mtorihikisakishiharai.TAX_HASUU_CD.ToString()));

            // 繰越残高を算出
            // ((精算差引残高＋非課税金額(E+I+N+Q)＋支払金額(A+F+J)＋明細毎税(B+K)＋伝票毎税(G)＋税計算ありの金額(C+L+O)＋税計算ありの金額の消費税((C+L+O)*税率))‐出金額)
            zandaka = (seisanZandaka + hikazeikin + shiKin + meiZei + denZei + shiSotoKin + kazeikin_tax) - shuKin;

            return zandaka;
        }

        /// <summary>
        /// ToolTip設定
        /// </summary>
        private void SetTipTxt(M_SYS_INFO mSysInfo)
        {
            //上下段の税計算区分のラジオボタン
            this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SEIKYU_ZEIKEISAN_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SEIKYU_ZEIKEISAN_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SEIKYU_ZEIKEISAN_KBN_3.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SHIHARAI_ZEIKEISAN_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SHIHARAI_ZEIKEISAN_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SHIHARAI_ZEIKEISAN_KBN_3.Tag = Const.ConstClass.TOOL_TIP_TXT_3;

            //税区分WS
            this.MyForm.SEIKYU_ZEI_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SEIKYU_ZEI_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_4;
            this.MyForm.SEIKYU_ZEI_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_5;
            this.MyForm.SEIKYU_ZEI_KBN_3.Tag = Const.ConstClass.TOOL_TIP_TXT_6;
            this.MyForm.SHIHARAI_ZEI_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.SHIHARAI_ZEI_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_4;
            this.MyForm.SHIHARAI_ZEI_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_5;
            this.MyForm.SHIHARAI_ZEI_KBN_3.Tag = Const.ConstClass.TOOL_TIP_TXT_6;
            //取引区分
            this.MyForm.SEIKYU_TORIHIKI_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SEIKYU_TORIHIKI_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_7;
            this.MyForm.SEIKYU_TORIHIKI_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_8;
            this.MyForm.SHIHARAI_TORIHIKI_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SHIHARAI_TORIHIKI_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_7;
            this.MyForm.SHIHARAI_TORIHIKI_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_8;
            //精算区分
            this.MyForm.SEIKYU_SEISAN_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SEIKYU_SEISAN_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_9;
            this.MyForm.SEIKYU_SEISAN_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_10;
            this.MyForm.SHIHARAI_SEISAN_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SHIHARAI_SEISAN_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_9;
            this.MyForm.SHIHARAI_SEISAN_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_10;
            //伝票発行区分
            this.MyForm.SEIKYU_DENPYO_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SEIKYU_DENPYO_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_11;
            this.MyForm.SEIKYU_DENPYO_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_12;
            this.MyForm.SHIHARAI_DENPYO_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SHIHARAI_DENPYO_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_11;
            this.MyForm.SHIHARAI_DENPYO_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_12;
            //相殺
            this.MyForm.SOUSATU_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.SOUSATU_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_22;
            this.MyForm.SOUSATU_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_23;
            //発行区分
            this.MyForm.HAKOU_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_3;
            this.MyForm.HAKOU_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_24;
            this.MyForm.HAKOU_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_25;
            this.MyForm.HAKOU_KBN_3.Tag = Const.ConstClass.TOOL_TIP_TXT_26;
            //領収証区分
            this.MyForm.RYOSYUSYO_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.RYOSYUSYO_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_27;
            this.MyForm.RYOSYUSYO_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_28;
            //敬称
            this.MyForm.Keisyou_1.Tag = Const.ConstClass.TOOL_TIP_TXT_29;
            this.MyForm.Keisyou_2.Tag = Const.ConstClass.TOOL_TIP_TXT_30;

            this.MyForm.KEIRYOU_PRIRNT_KBN_VALUE.Tag = Const.ConstClass.TOOL_TIP_TXT_1;
            this.MyForm.KEIRYOU_PRIRNT_KBN_1.Tag = Const.ConstClass.TOOL_TIP_TXT_11;
            this.MyForm.KEIRYOU_PRIRNT_KBN_2.Tag = Const.ConstClass.TOOL_TIP_TXT_12;
        }
        /// <summary>
        /// 画面レイアウト初期化
        /// </summary>
        private void LayoutInit(M_SYS_INFO mSysInfo)
        {
            //伝種区分
            string tenpyoKbn = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn;
            //パラメータの伝種区分＝１（受入）の場合
            if (Const.ConstClass.TENSYU_KBN_1.Equals(tenpyoKbn))
            {
                //システム設定．受入情報差引基準＝２（支払）の場合
                if (Const.ConstClass.CALC_BASE_KBN_2.Equals(mSysInfo.UKEIRE_CALC_BASE_KBN.ToString()))
                {
                    SeikyuToSihai();
                }
            }
            else if (Const.ConstClass.TENSYU_KBN_2.Equals(tenpyoKbn))
            {
                //システム設定．出荷情報差引基準＝２（支払）の場合
                if (Const.ConstClass.CALC_BASE_KBN_2.Equals(mSysInfo.SHUKKA_CALC_BASE_KBN.ToString()))
                {
                    SeikyuToSihai();
                }
            }
            else if (Const.ConstClass.TENSYU_KBN_3.Equals(tenpyoKbn))
            {
                //システム設定．売上支払情報差引基準＝２（支払）の場合
                if (Const.ConstClass.CALC_BASE_KBN_2.Equals(mSysInfo.UR_SH_CALC_BASE_KBN.ToString()))
                {
                    SeikyuToSihai();
                }
            }

            // No.4087-->
            if (this.MyForm.RYOSYUSYO_VALUE.Text.Equals(this.MyForm.RYOSYUSYO_KBN_1.Value))
            {
                GetStatus();
            }
            // No.4087<--
        }

        // No.4087-->
        /// <summary>
        /// ステータス取得
        /// </summary>
        public void GetStatus()
        {
            string tadasigaki = Properties.Settings.Default.tadasigaki;
            if (!string.IsNullOrEmpty(tadasigaki))
            {
                if (string.IsNullOrEmpty(this.MyForm.Tadasi_Kaki.Text))
                {   // データが設定されていない場合のみ前回値入れる
                    this.MyForm.Tadasi_Kaki.Text = tadasigaki;
                }
            }
        }

        /// <summary>
        /// ステータス保存
        /// </summary>
        public void SetStatus()
        {
            Properties.Settings.Default.tadasigaki = this.MyForm.Tadasi_Kaki.Text;     //拠点CD
            Properties.Settings.Default.Save();
        }
        // No.4087<--

        /// <summary>
        /// 請求と支払の切り替え処理
        /// </summary>
        private void SeikyuToSihai()
        {
            //請求と支払取引の切り替え
            Point ueTorihikiLocation = this.MyForm.UE_TORIHIKI.Location;
            Point sitaTorihikiLocation = this.MyForm.SITA_TORIHIKI.Location;
            this.MyForm.UE_TORIHIKI.Location = sitaTorihikiLocation;
            this.MyForm.SITA_TORIHIKI.Location = ueTorihikiLocation;
            //請求と支払分の切り替え
            Point ueBunLocation = this.MyForm.UE_BUN.Location;
            Point sitaBunLocation = this.MyForm.SITA_BUN.Location;
            this.MyForm.UE_BUN.Location = sitaBunLocation;
            this.MyForm.SITA_BUN.Location = ueBunLocation;

            foreach (System.Windows.Forms.Control txtBox in this.MyForm.UE_BUN.Controls)
            {
                if (txtBox is r_framework.CustomControl.CustomTextBox)
                {
                    ((r_framework.CustomControl.CustomTextBox)txtBox).ForeColor = System.Drawing.Color.Red;
                }
            }

            foreach (System.Windows.Forms.Control txtBox in this.MyForm.SITA_BUN.Controls)
            {
                if (txtBox is r_framework.CustomControl.CustomTextBox)
                {
                    ((r_framework.CustomControl.CustomTextBox)txtBox).ForeColor = System.Drawing.Color.Black;
                }
            }

            //請求、支払転換フラグ
            this.MyForm.LayoutChangeFlg = true;

        }
        /// <summary>
        /// 合計金額計算（上段-下段）
        /// </summary>
        public bool SetGokei()
        {
            bool ret = true;
            try
            {
                //合計金額計算（上段-下段）
                if (this.MyForm.LayoutChangeFlg)
                {
                    //合計金額
                    this.MyForm.Gokei_Konkai_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Konkai_Kingaku.Text, this.MyForm.Seikyu_Konkai_Kingaku.Text));
                    this.MyForm.Gokei_Konkai_Zeigaku.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Konkai_Zeigaku.Text, this.MyForm.Seikyu_Konkai_Zeigaku.Text));
                    this.MyForm.Gokei_Konkai_Rorihiki.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Konkai_Rorihiki.Text, this.MyForm.Seikyu_Konkai_Rorihiki.Text));
                    this.MyForm.Gokei_Sousatu_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Sousatu_Kingaku.Text, this.MyForm.Seikyu_Sousatu_Kingaku.Text));
                    this.MyForm.Gokei_Nyusyu_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Nyusyu_Kingaku.Text, this.MyForm.Seikyu_Nyusyu_Kingaku.Text));
                    this.MyForm.Gokei_Sagaku_Zentaka.Text = SetComma(KigakuSubstr(this.MyForm.Shiharai_Sagaku_Zentaka.Text, this.MyForm.Seikyu_Sagaku_Zentaka.Text));
                }
                else
                {
                    //合計金額
                    this.MyForm.Gokei_Konkai_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Konkai_Kingaku.Text, this.MyForm.Shiharai_Konkai_Kingaku.Text));
                    this.MyForm.Gokei_Konkai_Zeigaku.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Konkai_Zeigaku.Text, this.MyForm.Shiharai_Konkai_Zeigaku.Text));
                    this.MyForm.Gokei_Konkai_Rorihiki.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Konkai_Rorihiki.Text, this.MyForm.Shiharai_Konkai_Rorihiki.Text));
                    this.MyForm.Gokei_Sousatu_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Sousatu_Kingaku.Text, this.MyForm.Shiharai_Sousatu_Kingaku.Text));
                    this.MyForm.Gokei_Nyusyu_Kingaku.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Nyusyu_Kingaku.Text, this.MyForm.Shiharai_Nyusyu_Kingaku.Text));
                    this.MyForm.Gokei_Sagaku_Zentaka.Text = SetComma(KigakuSubstr(this.MyForm.Seikyu_Sagaku_Zentaka.Text, this.MyForm.Shiharai_Sagaku_Zentaka.Text));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGokei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 相殺設定
        /// </summary>
        public bool SetSousatu()
        {
            try
            {
                // 精算が、請求と支払とどっちも一致している
                bool isEqualSeisanKbn = (Const.ConstClass.SEISAN_KBN_1.Equals(this.MyForm.SEIKYU_SEISAN_VALUE.Text)
                                        && Const.ConstClass.SEISAN_KBN_1.Equals(this.MyForm.SHIHARAI_SEISAN_VALUE.Text))
                                        || (Const.ConstClass.SEISAN_KBN_2.Equals(this.MyForm.SEIKYU_SEISAN_VALUE.Text)
                                        && Const.ConstClass.SEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_SEISAN_VALUE.Text));
                // 税計算区分が請求 or 精算毎か
                bool isSeikyuuMaiTorihiki = (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text)
                                        || Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text));
                // 取引区分：掛けか
                bool isKakeTorihiki = (Const.ConstClass.TORIHIKI_KBN_2.Equals(this.MyForm.SEIKYU_TORIHIKI_VALUE.Text) && Const.ConstClass.TORIHIKI_KBN_2.Equals(this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text));
                // 起動元が修正モードか
                bool isShyuuseiMode = Const.ConstClass.TENPYO_MODEL_2.Equals(Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel);

                if (isEqualSeisanKbn && !isSeikyuuMaiTorihiki && isKakeTorihiki && !isShyuuseiMode && this.MyForm.KakuteiKbn)
                {
                    //相殺編集可
                    this.MyForm.SOUSATU_VALUE.Enabled = true;
                    this.MyForm.SOUSATU_KBN_1.Enabled = true;
                    this.MyForm.SOUSATU_KBN_2.Enabled = true;
                }
                else
                {
                    //相殺編集不可
                    this.MyForm.SOUSATU_VALUE.Enabled = false;
                    this.MyForm.SOUSATU_KBN_1.Enabled = false;
                    this.MyForm.SOUSATU_KBN_2.Enabled = false;
                    this.MyForm.SOUSATU_VALUE.Text = Const.ConstClass.SEISAN_KBN_2;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSousatu", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 相殺計算
        /// </summary>
        public bool SousatuKeisan()
        {
            try
            {
                //相殺する１.するの場合
                if ((Const.ConstClass.SOUSATU_KBN_1.Equals(this.MyForm.SOUSATU_VALUE.Text)))
                {
                    //請求分の入出金額と支払分の入出金額の絶対値が小さい値
                    string sousatu = "0";
                    if (Const.ConstClass.SEISAN_KBN_1.Equals(this.MyForm.SEIKYU_SEISAN_VALUE.Text)
                        && Const.ConstClass.SEISAN_KBN_1.Equals(this.MyForm.SHIHARAI_SEISAN_VALUE.Text))
                    {
                        sousatu = KigakuSubstr(this.MyForm.Seikyu_Nyusyu_Kingaku.Text, this.MyForm.Shiharai_Nyusyu_Kingaku.Text);
                        if (sousatu.Contains(Const.ConstClass.MINUS_S))
                        {
                            //マイナス消し
                            sousatu = sousatu.Replace(Const.ConstClass.MINUS_S, Const.ConstClass.Empty_S);
                            //支払分入出金額
                            this.MyForm.Shiharai_Nyusyu_Kingaku.Text = SetComma(sousatu);
                            //支払分相殺金額
                            this.MyForm.Shiharai_Sousatu_Kingaku.Text = SetComma(this.MyForm.Seikyu_Nyusyu_Kingaku.Text);
                            //請求分入出金額
                            this.MyForm.Seikyu_Nyusyu_Kingaku.Text = SetComma(Const.ConstClass.KIGAKU_0);
                            //請求分相殺金額
                            this.MyForm.Seikyu_Sousatu_Kingaku.Text = SetComma(this.MyForm.Shiharai_Sousatu_Kingaku.Text);
                        }
                        else
                        {
                            //請求分入出金額
                            this.MyForm.Seikyu_Nyusyu_Kingaku.Text = SetComma(sousatu);
                            //請求分相殺金額
                            this.MyForm.Seikyu_Sousatu_Kingaku.Text = this.MyForm.Shiharai_Nyusyu_Kingaku.Text;
                            //支払分入出金額
                            this.MyForm.Shiharai_Nyusyu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                            //支払分相殺金額
                            this.MyForm.Shiharai_Sousatu_Kingaku.Text = this.MyForm.Seikyu_Sousatu_Kingaku.Text;
                        }
                    }
                    else if (Const.ConstClass.SEISAN_KBN_2.Equals(this.MyForm.SEIKYU_SEISAN_VALUE.Text)
                        && Const.ConstClass.SEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_SEISAN_VALUE.Text))
                    {
                        sousatu = KigakuSubstr(this.MyForm.Seikyu_Konkai_Rorihiki.Text, this.MyForm.Shiharai_Konkai_Rorihiki.Text);
                        if (sousatu.Contains(Const.ConstClass.MINUS_S))
                        {
                            //支払分相殺金額
                            this.MyForm.Shiharai_Sousatu_Kingaku.Text = SetComma(this.MyForm.Seikyu_Konkai_Rorihiki.Text);
                            //請求分相殺金額
                            this.MyForm.Seikyu_Sousatu_Kingaku.Text = SetComma(this.MyForm.Shiharai_Sousatu_Kingaku.Text);
                        }
                        else
                        {
                            //請求分相殺金額
                            this.MyForm.Seikyu_Sousatu_Kingaku.Text = this.MyForm.Shiharai_Konkai_Rorihiki.Text;
                            //支払分相殺金額
                            this.MyForm.Shiharai_Sousatu_Kingaku.Text = this.MyForm.Seikyu_Sousatu_Kingaku.Text;
                        }
                        this.MyForm.Shiharai_Nyusyu_Kingaku.Text = "0";
                        this.MyForm.Seikyu_Nyusyu_Kingaku.Text = "0";
                    }
                }
                //相殺する2.しないの場合
                else if ((Const.ConstClass.SOUSATU_KBN_2.Equals(this.MyForm.SOUSATU_VALUE.Text)))
                {
                    //請求分相殺金額に0を設定する。
                    this.MyForm.Seikyu_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                    //支払分相殺金額に0を設定する。
                    this.MyForm.Shiharai_Sousatu_Kingaku.Text = Const.ConstClass.KIGAKU_0;
                }
                //入出金金額・差引残高(請求)の再計算を行う
                if (!SetSeikyuKingaku1022())
                {
                    return false;
                }
                //入出金金額・差引残高(支払)の再計算を行う
                SetShiharaiKingaku1022();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SousatuKeisan", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SousatuKeisan", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 金額加算
        /// </summary>
        private string KigakuAdd(string a, string b)
        {
            return (Convert.ToDecimal(a == null || a.Equals("") ? "0" : a) + Convert.ToDecimal(b == null || b.Equals("") ? "0" : b)).ToString();

        }
        /// <summary>
        /// 金額引算
        /// </summary>
        private string KigakuSubstr(string a, string b)
        {
            return (Convert.ToDecimal(a == null || a.Equals("") ? "0" : a) - Convert.ToDecimal(b == null || b.Equals("") ? "0" : b)).ToString();

        }
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.MyForm.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.MyForm.WindowType);
            string denshuKbn = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoKbn;

            // 伝票モード取得　（１．追加　２．修正）
            string tenpyoModel = Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel;

            // 伝票モードが１.追加もしくは、システム設定の残高自動区分が２.しないの場合
            if (tenpyoModel == ConstClass.TENPYO_MODEL_1 && this.GetZandakaJidouKBN(denshuKbn) == 2)
            {
                parentForm.bt_func1.Enabled = true;
            }
            else
            {
                parentForm.bt_func1.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        /// <returns></returns>
        internal void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.MyForm.Parent;

            //閉じるボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.MyForm.GetZanda);

            //登録ボタン(F9)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.MyForm.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.MyForm.FormClose);

            // 領収証ありチェックイベント
            this.MyForm.RYOSYUSYO_KBN_1.CheckedChanged += new EventHandler(this.MyForm.RYOSYUSYO_KBN_1_CheckedChanged);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                return string.Format("{0:#,0}", Convert.ToDecimal(value));
            }
        }

        /// <summary>
        /// 指定された端数CDに従い、金額の端数処理を行う
        /// </summary>
        /// <param name="kingaku">端数処理対象金額</param>
        /// <param name="calcCD">端数CD</param>
        /// <returns name="decimal">端数処理後の金額</returns>
        public static decimal FractionCalc(decimal kingaku, int calcCD)
        {
            decimal returnVal = 0;		// 戻り値
            decimal sign = 1;           // 1(正) or -1(負)

            if (kingaku < 0)
                sign = -1;

            switch ((fractionType)calcCD)
            {
                case fractionType.CEILING:
                    returnVal = Math.Ceiling(Math.Abs(kingaku)) * sign;
                    break;
                case fractionType.FLOOR:
                    returnVal = Math.Floor(Math.Abs(kingaku)) * sign;
                    break;
                case fractionType.ROUND:
                    returnVal = Math.Round(Math.Abs(kingaku), 0, MidpointRounding.AwayFromZero) * sign;
                    break;
                default:
                    // 何もしない
                    returnVal = kingaku;
                    break;
            }

            return returnVal;
        }

        /// <summary>
        /// 消費税を税区分（外税、内税、非課税）別に計算
        /// </summary>
        /// <param name="zeiKbn">税区分</param>
        /// <param name="Kingaku">金額</param>
        /// <param name="ShouhizeiRate">消費税率</param>
        /// <param name="HasuCd">端数の処理方法</param>
        /// <returns>消費税額</returns>
        private decimal CalcZeiKbnShohiZei(string zeiKbn, decimal Kingaku, decimal ShouhizeiRate, int HasuCd)
        {
            decimal returnVal = 0;
            decimal dTax = 0;
            decimal dUTax = 0;
            if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
            {
                // 外税
                dTax = Kingaku * ShouhizeiRate;
                returnVal = FractionCalc(dTax, HasuCd);
            }
            else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
            {
                // 内税
                dUTax = Kingaku;
                dUTax = dUTax - (dUTax / (ShouhizeiRate + 1));
                returnVal = FractionCalc(dUTax, HasuCd);
            }
            else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
            {
                // 非課税
                returnVal = Convert.ToDecimal(Const.ConstClass.KIGAKU_0);
            }
            return returnVal;
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// 「キャッシャ連動」項目状態セット
        /// </summary>
        /// <param name="sts">TRUE:有効, FALSE:無効</param>
        /// <remarks>
        /// stsのがTRUE(有効)でも、請求・支払取引区分双方が「掛け」だった場合は無効となる
        /// </remarks>
        internal void setCasherEnabled(bool sts)
        {
            // キャッシャ連動の初期値は「2.しない」
            this.MyForm.KYASYA_VALUE.Text = CommonConst.CASHER_LINK_KBN_UNUSED;

            if(sts == true)
            {
                if((this.MyForm.SEIKYU_TORIHIKI_VALUE.Text == CommonConst.TORIHIKI_KBN_KAKE.ToString())
                && (this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text == CommonConst.TORIHIKI_KBN_KAKE.ToString()))
                {
                    // 請求・支払取引区分双方が「掛け」だった場合キャッシャ連動を無効にする
                    this.MyForm.KYASYA_VALUE.Enabled = false;
                    this.MyForm.KYASYA_KBN_1.Enabled = false;
                    this.MyForm.KYASYA_KBN_2.Enabled = false;
                }
                //else if(Const.ConstClass.TENPYO_MODEL_2.Equals(Shougun.Core.SalesPayment.DenpyouHakou.Properties.Settings.Default.tenpyoModel))
                //{
                //    // 修正モードだった場合キャッシャ連動を無効にする
                //    this.MyForm.KYASYA_VALUE.Enabled = false;
                //    this.MyForm.KYASYA_KBN_1.Enabled = false;
                //    this.MyForm.KYASYA_KBN_2.Enabled = false;
                //}
                else
                {
                    // 有効
                    bool isUriage = false;
                    bool isSiharai = false;
                    foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                    {
                        if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                        {	// 伝票区分が売上
                            isUriage = true;
                        }
                        else if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                        {	// 伝票区分が支払
                            isSiharai = true;
                        }
                        if (isUriage && isSiharai)
                        {
                            break;
                        }
                    }
                    this.MyForm.KYASYA_VALUE.Enabled = true;
                    this.MyForm.KYASYA_KBN_1.Enabled = true;
                    this.MyForm.KYASYA_KBN_2.Enabled = true;
                    if (isUriage && this.MyForm.SEIKYU_TORIHIKI_VALUE.Text != CommonConst.TORIHIKI_KBN_KAKE.ToString()
                    || isSiharai && this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text != CommonConst.TORIHIKI_KBN_KAKE.ToString())
                    {
                        const string CASHER_LINK = "キャッシャ連動";
                        CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                        this.MyForm.KYASYA_VALUE.Text = this.GetUserProfileValue(userProfile, CASHER_LINK);
                        if (this.MyForm.KYASYA_VALUE.Text.Length == 0)
                        {
                            this.MyForm.KYASYA_VALUE.Text = CommonConst.CASHER_LINK_KBN_UNUSED;
                        }
                    }
                }
            }
            else
            {
                // 無効
                this.MyForm.KYASYA_VALUE.Enabled = false;
                this.MyForm.KYASYA_KBN_1.Enabled = false;
                this.MyForm.KYASYA_KBN_2.Enabled = false;
            }
        }

        #region 月次処理 - 月次ロックチェック

        /// <summary>
        /// 月次処理 - 月次処理中 or 月次ロック済かのチェックを行います
        /// </summary>
        /// <returns>True：月次処理中 or 月次ロック済</returns>
        internal bool GetsujiLockCheck()
        {
            bool returnVal = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();

                // 修正モード時に伝票日付を変更して登録処理を行うまでの間に月次処理が行われる可能性があるため
                // 初期表示値の日付で月次処理が行われていないか確認する
                if (!string.IsNullOrEmpty(this.MyForm.ParameterDTO.DenpyouDate) &&
                    !string.IsNullOrEmpty(this.MyForm.ParameterDTO.BeforeDenpyouDate) &&
                    !this.MyForm.ParameterDTO.BeforeDenpyouDate.Equals(this.MyForm.ParameterDTO.DenpyouDate))
                {
                    DateTime beforeDate = DateTime.Parse(this.MyForm.ParameterDTO.BeforeDenpyouDate);
                    if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(beforeDate))
                    {
                        // 月次処理中は実行不可
                        msgLogic.MessageBoxShow("E224", "実行");
                        returnVal = true;
                    }
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(beforeDate.Year.ToString()), short.Parse(beforeDate.Month.ToString())))
                    {
                        // ロック中は実行不可
                        msgLogic.MessageBoxShow("E223", "実行");
                        returnVal = true;
                    }
                }

                // 現在の画面表示値で月次処理が行われていないか確認する
                if (!returnVal &&
                    !string.IsNullOrEmpty(this.MyForm.ParameterDTO.DenpyouDate))
                {
                    DateTime date = DateTime.Parse(this.MyForm.ParameterDTO.DenpyouDate);
                    if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(date))
                    {
                        // 月次処理中は実行不可
                        msgLogic.MessageBoxShow("E224", "実行");
                        returnVal = true;
                    }
                    else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(date.Year.ToString()), short.Parse(date.Month.ToString())))
                    {
                        // ロック中は実行不可
                        msgLogic.MessageBoxShow("E223", "実行");
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetsujiLockCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetsujiLockCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                returnVal = true;
            }

            return returnVal;
        }

        #endregion

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
        /// 【請求】税計算区分：2.請求毎のEnableとTextBoxの入力を制御します。
        /// </summary>
        internal void SeikyuuMaiZeiEnable()
        {
            // 精算
            this.MyForm.SEIKYU_ZEIKEISAN_KBN_2.Enabled = true;
            this.MyForm.SEIKYU_ZEIKEISAN_VALUE.CharacterLimitList = new char[] { '1', '2', '3' };
            if (Const.ConstClass.SEISAN_KBN_1.Equals(this.MyForm.SEIKYU_SEISAN_VALUE.Text) || this.MyForm.SEIKYU_TORIHIKI_VALUE.Text != "2")
            {
                this.MyForm.SEIKYU_ZEIKEISAN_KBN_2.Enabled = false;
                this.MyForm.SEIKYU_ZEIKEISAN_VALUE.CharacterLimitList = new char[] { '1', '3' };
                if (this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text == "2")
                {
                    this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text = "1";
                }
            }
            this.MyForm.SaveOldZei();
        }

        /// <summary>
        /// 【支払】税計算区分：2.精算毎のEnableとTextBoxの入力を制御します。
        /// </summary>
        internal void SeisanMaiZeiEnable()
        {
            // 精算
            this.MyForm.SHIHARAI_ZEIKEISAN_KBN_2.Enabled = true;
            this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.CharacterLimitList = new char[] { '1', '2', '3' };
            if (Const.ConstClass.SEISAN_KBN_1.Equals(this.MyForm.SHIHARAI_SEISAN_VALUE.Text) || this.MyForm.SHIHARAI_TORIHIKI_VALUE.Text != "2")
            {
                this.MyForm.SHIHARAI_ZEIKEISAN_KBN_2.Enabled = false;
                this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.CharacterLimitList = new char[] { '1', '3' };
                if (this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text == "2")
                {
                    this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text = "1";
                }
            }
        }

        /// <summary>
        /// 領収書/仕切書(売上)発行用
        /// </summary>
        internal void SetRYOSYUSYO()
        {
            decimal r_KAZEI_KINGAKU = 0;
            decimal r_KAZEI_SHOUHIZEI = 0;
            decimal r_KAZEI_KINGAKU_SUM = 0;
            decimal r_HIKAZEI_KINGAKU = 0;

            this.MyForm.ParameterDTO.R_KAZEI_KINGAKU = "0";
            this.MyForm.ParameterDTO.R_KAZEI_SHOUHIZEI = "0";
            this.MyForm.ParameterDTO.R_HIKAZEI_KINGAKU = "0";

            //請求分用の税率取得
            string seikyuShouhizeiRate = this.MyForm.SeikyuShouhizeiRate;
            M_TORIHIKISAKI_SEIKYUU dataTorihikiSeikyu = TorihikisakiSeikyuDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);
            int iSeikyuHasuCd = 0;
            if (!dataTorihikiSeikyu.TAX_HASUU_CD.Equals(System.Data.SqlTypes.SqlInt16.Null))
            {
                iSeikyuHasuCd = dataTorihikiSeikyu.TAX_HASUU_CD.Value;
            }

            if ((this.MyForm.ParameterDTO.Ryousyusyou == "1") || (this.MyForm.ParameterDTO.Seikyu_Hakou_Kbn == "1"))
            {
                if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                {
                    foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                    {
                        //TENPYO_KBN_1:売上
                        if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                        {
                            //税区分
                            string zeiKbn = string.Empty;
                            if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                            {
                                zeiKbn = meiseiDto.ZeiKbn;          //品名税区分
                            }
                            else
                            {
                                zeiKbn = this.MyForm.SEIKYU_ZEI_VALUE.Text;  //伝票税区分
                            }

                            //外税
                            if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                            {
                                if ((string.IsNullOrEmpty(meiseiDto.ZeiKbn)) && (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text)))
                                {
                                    //品名税区分なし、伝票毎外税
                                    //★F
                                    r_KAZEI_KINGAKU_SUM = r_KAZEI_KINGAKU_SUM + Convert.ToDecimal(meiseiDto.Kingaku);
                                }
                                else if ((string.IsNullOrEmpty(meiseiDto.ZeiKbn)) && (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text)))
                                {
                                    //品名税区分なし、請求毎外税
                                    //O
                                    r_KAZEI_KINGAKU = r_KAZEI_KINGAKU + Convert.ToDecimal(meiseiDto.Kingaku);
                                }
                                else
                                {
                                    //品名外税or明細毎外税
                                    //A/J
                                    r_KAZEI_KINGAKU = r_KAZEI_KINGAKU + Convert.ToDecimal(meiseiDto.Kingaku);
                                    //B/K
                                    r_KAZEI_SHOUHIZEI = r_KAZEI_SHOUHIZEI +
                                                    Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                    Convert.ToDecimal(meiseiDto.Kingaku),
                                                    Convert.ToDecimal(seikyuShouhizeiRate),
                                                    iSeikyuHasuCd).ToString()));
                                }
                            }
                            //内税
                            else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                            {
                                //★C/L
                                r_KAZEI_KINGAKU_SUM = r_KAZEI_KINGAKU_SUM + Convert.ToDecimal(meiseiDto.Kingaku) -
                                                Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(seikyuShouhizeiRate),
                                                iSeikyuHasuCd).ToString()));
                            }
                            //非課税
                            else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
                            {
                                //E/I/N/R
                                r_HIKAZEI_KINGAKU = r_HIKAZEI_KINGAKU + Convert.ToDecimal(meiseiDto.Kingaku);
                            }
                        }
                    }
                }

                //	領収書/仕切書_売上)課税金額 //A + J + (★C + F + L)
                this.MyForm.ParameterDTO.R_KAZEI_KINGAKU = SetComma(Convert.ToString(r_KAZEI_KINGAKU + r_KAZEI_KINGAKU_SUM));
                //	領収書/仕切書_売上)課税消費税
                if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text))
                {
                    //税計算区分：請求毎 【ゼロ】固定
                    this.MyForm.ParameterDTO.R_KAZEI_SHOUHIZEI = "0";
                }
                else
                {
                    //税計算区分：請求毎以外 (★C + F + L) * 税率 + B + K
                    this.MyForm.ParameterDTO.R_KAZEI_SHOUHIZEI = SetComma(Convert.ToString(
                                                    Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei("1",
                                                    Convert.ToDecimal(r_KAZEI_KINGAKU_SUM),
                                                    Convert.ToDecimal(seikyuShouhizeiRate),
                                                    iSeikyuHasuCd).ToString()))
                                                    + r_KAZEI_SHOUHIZEI));
                }

                //	領収書/仕切書_売上)非課税金額 //E+I+N+R
                this.MyForm.ParameterDTO.R_HIKAZEI_KINGAKU = SetComma(Convert.ToString(r_HIKAZEI_KINGAKU));
            }
        }

        /// <summary>
        /// 仕切書(支払)発行用
        /// </summary>
        internal void SetSHIKIRISHO_SHIHARAI()
        {
            decimal r_KAZEI_KINGAKU = 0;
            decimal r_KAZEI_SHOUHIZEI = 0;
            decimal r_KAZEI_KINGAKU_SUM = 0;
            decimal r_HIKAZEI_KINGAKU = 0;

            this.MyForm.ParameterDTO.R_KAZEI_KINGAKU_SHIHARAI = "0";
            this.MyForm.ParameterDTO.R_KAZEI_SHOUHIZEI_SHIHARAI = "0";
            this.MyForm.ParameterDTO.R_HIKAZEI_KINGAKU_SHIHARAI = "0";

            //支払分用の税率取得
            string shiharaiShouhizeiRate = this.MyForm.ShiharaiShouhizeiRate;
            M_TORIHIKISAKI_SHIHARAI dataTorihikiShiharai = TorihikisakiShiharaiDao.GetDataByCd(this.MyForm.ParameterDTO.Torihikisaki_Cd);
            int iShiharaiHasuCd = 0;
            if (!dataTorihikiShiharai.TAX_HASUU_CD.Equals(System.Data.SqlTypes.SqlInt16.Null))
            {
                iShiharaiHasuCd = dataTorihikiShiharai.TAX_HASUU_CD.Value;
            }

            if (this.MyForm.ParameterDTO.Shiharai_Hakou_Kbn == "1")
            {
                if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                {
                    foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                    {
                        //TENPYO_KBN_2:支払
                        if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                        {
                            //税区分
                            string zeiKbn = string.Empty;
                            if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                            {
                                zeiKbn = meiseiDto.ZeiKbn;          //品名税区分
                            }
                            else
                            {
                                zeiKbn = this.MyForm.SHIHARAI_ZEI_VALUE.Text;   //伝票税区分
                            }

                            //外税
                            if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                            {
                                if ((string.IsNullOrEmpty(meiseiDto.ZeiKbn)) && (Const.ConstClass.ZEIKEISAN_KBN_1.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text)))
                                {
                                    //品名税区分なし、伝票毎外税
                                    //★F
                                    r_KAZEI_KINGAKU_SUM = r_KAZEI_KINGAKU_SUM + Convert.ToDecimal(meiseiDto.Kingaku);
                                }
                                else if ((string.IsNullOrEmpty(meiseiDto.ZeiKbn)) && (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text)))
                                {
                                    //品名税区分なし、請求毎外税
                                    //O
                                    r_KAZEI_KINGAKU = r_KAZEI_KINGAKU + Convert.ToDecimal(meiseiDto.Kingaku);
                                }
                                else
                                {
                                    //品名外税or明細毎外税
                                    //A/J
                                    r_KAZEI_KINGAKU = r_KAZEI_KINGAKU + Convert.ToDecimal(meiseiDto.Kingaku);
                                    //B/K
                                    r_KAZEI_SHOUHIZEI = r_KAZEI_SHOUHIZEI +
                                                    Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                    Convert.ToDecimal(meiseiDto.Kingaku),
                                                    Convert.ToDecimal(shiharaiShouhizeiRate),
                                                    iShiharaiHasuCd).ToString()));
                                }
                            }
                            //内税
                            else if (Const.ConstClass.ZEI_KBN_2.Equals(zeiKbn))
                            {
                                //★C/L
                                r_KAZEI_KINGAKU_SUM = r_KAZEI_KINGAKU_SUM + Convert.ToDecimal(meiseiDto.Kingaku) -
                                                Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei(zeiKbn,
                                                Convert.ToDecimal(meiseiDto.Kingaku),
                                                Convert.ToDecimal(shiharaiShouhizeiRate),
                                                iShiharaiHasuCd).ToString()));
                            }
                            //非課税
                            else if (Const.ConstClass.ZEI_KBN_3.Equals(zeiKbn))
                            {
                                //E/I/N/R
                                r_HIKAZEI_KINGAKU = r_HIKAZEI_KINGAKU + Convert.ToDecimal(meiseiDto.Kingaku);
                            }
                        }
                    }
                }

                //	仕切書_支払)課税金額 //A + J + (★C + F + L)
                this.MyForm.ParameterDTO.R_KAZEI_KINGAKU_SHIHARAI = SetComma(Convert.ToString(r_KAZEI_KINGAKU + r_KAZEI_KINGAKU_SUM));
                //	領収書/仕切書_売上)課税消費税
                if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text))
                {
                    //税計算区分：請求毎 【ゼロ】固定
                    this.MyForm.ParameterDTO.R_KAZEI_SHOUHIZEI_SHIHARAI = "0";
                }
                else
                {
                    //税計算区分：請求毎以外 (★C + F + L) * 税率 + B + K
                    this.MyForm.ParameterDTO.R_KAZEI_SHOUHIZEI_SHIHARAI = SetComma(Convert.ToString(
                                                    Convert.ToDecimal(SetComma(CalcZeiKbnShohiZei("1",
                                                    Convert.ToDecimal(r_KAZEI_KINGAKU_SUM),
                                                    Convert.ToDecimal(shiharaiShouhizeiRate),
                                                    iShiharaiHasuCd).ToString()))
                                                    + r_KAZEI_SHOUHIZEI));
                }
                //	仕切書_支払)非課税金額 //E+I+N+R
                this.MyForm.ParameterDTO.R_HIKAZEI_KINGAKU_SHIHARAI = SetComma(Convert.ToString(r_HIKAZEI_KINGAKU));
            }
        }
        #region 領収書/仕切書チェック

        /// <summary>
        /// 領収書/仕切書チェック
        /// アラートに対して、
        /// 「はい」を選択した場合→以降のチェック処理を行わず、登録処理を続行する
        /// 「いいえ」を選択した場合→画面項目を変更し、登録処理を中断する
        /// </summary>
        internal bool Ryousyu_ShikiriCheck()
        {
            bool returnVal = false;
            DialogResult result = 0;

            string ErrUHinmeiCD = string.Empty;  //エラー品名用
            string ErrSHinmeiCD = string.Empty;  //エラー品名用

            #region ０）品名(明細)の税区分データ取得
            //１）品名(明細)の税区分チェック用のデータ取得
            //　　品名の税区分 = 1:外税の場合アラート
            if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
            {
                foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                {
                    //税区分
                    string zeiKbn = string.Empty;
                    if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                    {
                        zeiKbn = meiseiDto.ZeiKbn;  //品名税区分
                        if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                        {
                            switch(meiseiDto.Uriageshiharai_Kbn)
                            {
                                case Const.ConstClass.TENPYO_KBN_1:
                                    ErrUHinmeiCD = ErrUHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                                    break;
                                case Const.ConstClass.TENPYO_KBN_2:
                                    ErrSHinmeiCD = ErrSHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion ０）品名(明細)の税区分データ取得

            //領収書→仕切書の順にチェック
            for(int KB = 1 ; KB <= 2; KB++)
            {
                int ChkFlg = 0; //1 :(売上のみ)、2 :(支払のみ)、3 :(売上支払両方)
                string OutPutPaper = string.Empty;
                
                #region 処理分岐
                if (KB == 1)
                {
                    //領収書：なしの場合は処理なし
                    if ((this.MyForm.RYOSYUSYO_VALUE.Text != "1") || (!this.MyForm.RYOSYUSYO_VALUE.Enabled))
                    {
                        continue;
                    }
                    OutPutPaper = "領収書";
                    ChkFlg = 1;
                }
                if (KB == 2)
                {
                    //請求伝票/支払伝票：なしの場合は処理なし
                    if ((this.MyForm.SEIKYU_DENPYO_VALUE.Text != "1") && (this.MyForm.SHIHARAI_DENPYO_VALUE.Text != "1"))
                    {
                        continue;
                    }
                    OutPutPaper = "仕切書";
                    if (this.MyForm.SEIKYU_DENPYO_VALUE.Text == "1")
                    {
                        ChkFlg = 1;
                    }
                    if (this.MyForm.SHIHARAI_DENPYO_VALUE.Text == "1")
                    {
                        ChkFlg = ChkFlg + 2;
                    }
                }
                #endregion 処理分岐

                #region １）品名(明細)の税区分チェック
                //１）品名(明細)の税区分チェック
                //　　品名の税区分 = 1:外税の場合アラート
                ErrHinmeiCD = string.Empty;
                if ((KB == 1) || ((KB == 2) && (ChkFlg != 2))) //領収書or仕切書(売上)あり
                {
                    ErrHinmeiCD = ErrUHinmeiCD;
                }
                if ((KB == 2) && (ChkFlg != 1))//仕切書(支払あり)
                {
                    ErrHinmeiCD = ErrHinmeiCD + ErrSHinmeiCD;
                }

                if (ErrHinmeiCD != "")
                {
                    ErrHinmeiCD = ErrHinmeiCD.Substring(0, ErrHinmeiCD.Length - 1);
                    result = MessageBox.Show(string.Format("税区分に外税が登録されている品名は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？\r（品名CD={1}）", OutPutPaper, ErrHinmeiCD), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        if (KB == 1)
                        {
                            this.MyForm.RYOSYUSYO_VALUE.Text = "2";
                        }
                        else
                        {
                            this.MyForm.SEIKYU_DENPYO_VALUE.Text = "2";
                            this.MyForm.SHIHARAI_DENPYO_VALUE.Text = "2";
                        }
                        returnVal = true;
                        return returnVal;
                    }
                }
                #endregion １）品名(明細)の税区分チェック

                #region ２）税計算区分のチェック
                //２）税計算区分のチェック
                //　　請求取引-税計算区分 =3の場合アラート
                if ((ChkFlg != 2) && (this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text == "3"))
                {
                    result = MessageBox.Show(string.Format("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？", OutPutPaper), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        returnVal = true;
                        if (KB == 1)
                        {
                            this.MyForm.RYOSYUSYO_VALUE.Text = "2";
                        }
                        if (KB == 2)
                        {
                            this.MyForm.SEIKYU_DENPYO_VALUE.Text = "2";
                            if ((ChkFlg >= 2) && (this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text == "3"))
                            {
                                //支払も同じ条件であれば、[2.なし]に更新する
                                this.MyForm.SHIHARAI_DENPYO_VALUE.Text = "2";
                            }
                        }
                        return returnVal;
                    }
                }

                if ((ChkFlg >= 2) && (this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text == "3"))
                {
                    result = MessageBox.Show(string.Format("税計算区分＝3.明細毎 は、\r適格請求書の要件を満たした{0}になりませんがよろしいでしょうか？", OutPutPaper), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        returnVal = true;
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = "2";
                        return returnVal;
                    }
                }
                #endregion ２）税計算区分のチェック
            }

            #region ３）登録番号チェック
            //領収書：あり or 請求伝票：あり or 支払伝票：あり のいずれかの場合チェックを行う
            if (((this.MyForm.RYOSYUSYO_VALUE.Text == "1") && (this.MyForm.RYOSYUSYO_VALUE.Enabled)) ||
                (this.MyForm.SEIKYU_DENPYO_VALUE.Text == "1") ||
                (this.MyForm.SHIHARAI_DENPYO_VALUE.Text == "1"))
            {
                //自社情報入力ー登録番号が未設定の場合アラート 
                M_CORP_INFO entCorpInfo = CommonShogunData.CORP_INFO;
                if ((entCorpInfo != null) && (String.IsNullOrEmpty(entCorpInfo.TOUROKU_NO)))
                {
                    result = MessageBox.Show("登録番号が未入力です。\r登録番号が表示されませんがよろしいでしょうか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return returnVal;
                    }
                    else
                    {
                        returnVal = true;
                        this.MyForm.RYOSYUSYO_VALUE.Text = "2";
                        this.MyForm.SEIKYU_DENPYO_VALUE.Text = "2";
                        this.MyForm.SHIHARAI_DENPYO_VALUE.Text = "2";
                        return returnVal;
                    }
                }
            }
            #endregion ３）登録番号チェック

            return returnVal;

        }
        #endregion

        /// <summary>
        /// 品名外税明細あり、税計算区分：請求/支払毎の場合、アラート
        /// </summary>
        private void SeiMaiSoto_Check()
        {
            string ErrUHinmeiCD = string.Empty;
            string ErrSHinmeiCD = string.Empty;
            string ErrKBN = string.Empty;

            if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SEIKYU_ZEIKEISAN_VALUE.Text))
            {
                if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                {
                    foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                    {
                        if (Const.ConstClass.TENPYO_KBN_1.Equals(meiseiDto.Uriageshiharai_Kbn))
                        {
                            string zeiKbn = string.Empty;
                            if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                            {
                                zeiKbn = meiseiDto.ZeiKbn;
                                if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                {
                                    //エラー表示用品目CDセット
                                    ErrUHinmeiCD = ErrUHinmeiCD + meiseiDto.Hinmei_Cd + "、";
                                }
                            }
                        }
                    }
                }
            }
            if (Const.ConstClass.ZEIKEISAN_KBN_2.Equals(this.MyForm.SHIHARAI_ZEIKEISAN_VALUE.Text))
            {
                if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
                {
                    foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                    {
                        if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                        {
                            string zeiKbn = string.Empty;
                            if (!string.IsNullOrEmpty(meiseiDto.ZeiKbn))
                            {
                                zeiKbn = meiseiDto.ZeiKbn;
                                if (Const.ConstClass.ZEI_KBN_1.Equals(zeiKbn))
                                {
                                    //エラー表示用品目CDセット
                                    ErrSHinmeiCD = ErrSHinmeiCD + meiseiDto.Hinmei_Cd + "、";

                                }
                            }
                        }
                    }
                }
            }

            ErrHinmeiCD = ErrUHinmeiCD + ErrSHinmeiCD;

            if (!string.IsNullOrEmpty(ErrUHinmeiCD))
            {
                ErrKBN = ErrKBN + "請求書/";
            }
            if (!string.IsNullOrEmpty(ErrSHinmeiCD))
            {
                ErrKBN = ErrKBN + "支払明細書/";
            }

            if (!string.IsNullOrEmpty(ErrHinmeiCD))
            {
                ErrHinmeiCD = ErrHinmeiCD.Substring(0, ErrHinmeiCD.Length - 1);
                ErrKBN = ErrKBN.Substring(0, ErrKBN.Length - 1);
                errmessage.MessageBoxShowWarn(string.Format("税区分が登録されている品名は、\r適格請求書の要件を満たした{0}になりません。\r（品名CD={1}）", ErrKBN, ErrHinmeiCD));
            }
        }

        #region refs #163349
        /// <summary>
        /// 支払伝票の存在チェック
        /// </summary>
        internal bool ShiharaiDenpyouCheck()
        {
            bool checkflg = false;
            if (this.MyForm.ParameterDTO.Tenpyo_Cnt != null)
            {
                foreach (MeiseiDTOClass meiseiDto in this.MyForm.ParameterDTO.Tenpyo_Cnt)
                {
                    if (Const.ConstClass.TENPYO_KBN_2.Equals(meiseiDto.Uriageshiharai_Kbn))
                    {	// 伝票区分が支払
                        checkflg = true;
                    }
                }
            }
            return checkflg;
        }
        #endregion
    }
}
