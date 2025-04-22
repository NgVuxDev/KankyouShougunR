// $Id: LogicCls.cs 26166 2014-07-18 13:27:01Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.MasterAccess;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.BusinessManagement.TorihikisakiKakunin.APP;
using Shougun.Core.BusinessManagement.TorihikisakiKakunin.Const;
using Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.TorihikisakiKakunin.Logic
{
    /// <summary>
    /// 引合取引先入力のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.TorihikisakiKakunin.Setting.ButtonSetting.xml";

        /// <summary>
        /// 引合取引先入力Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_HIKIAI_TORIHIKISAKI entitysTORIHIKISAKI;

        private M_HIKIAI_TORIHIKISAKI_SHIHARAI entitysTOROHIKISAKI_SHIHARAI;

        private M_HIKIAI_TORIHIKISAKI_SEIKYUU entitysTORIHIKISAKI_SEIKYUU;

        private M_KARI_TORIHIKISAKI entitysKARITORIHIKISAKI;

        private M_KARI_TORIHIKISAKI_SHIHARAI entitysKARITOROHIKISAKI_SHIHARAI;

        private M_KARI_TORIHIKISAKI_SEIKYUU entitysKARITORIHIKISAKI_SEIKYUU;

        private M_KYOTEN entitysM_KYOTEN;

        private M_BUSHO entitysM_BUSHO;

        private M_SHAIN entitysM_SHAIN;

        private M_SHUUKEI_KOUMOKU entitysM_SHUUKEI_KOUMOKU;

        private M_GYOUSHU entitysM_GYOUSHU;

        private M_TODOUFUKEN entitysM_TODOUFUKEN;

        private M_SYS_INFO entitysM_SYS_INFO;

        private M_BANK entitysM_BANK;

        private M_BANK_SHITEN entitysM_BANK_SHITEN;

        private M_NYUUSHUKKIN_KBN entitysM_NYUUSHUKKIN_KBN;

        private Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_HIKIAI_TORIHIKISAKIDao daoTORIHIKISAKI;

        private Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao daoTORIHIKISAKI_SHIHARAI;

        private Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao daoTORIHIKISAKI_SEIKYUU;

        private Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_KARI_TORIHIKISAKIDao daoKARITORIHIKISAKI;

        private Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_KARI_TORIHIKISAKI_SHIHARAIDao daoKARITORIHIKISAKI_SHIHARAI;

        private Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_KARI_TORIHIKISAKI_SEIKYUUDao daoKARITORIHIKISAKI_SEIKYUU;

        private IM_KYOTENDao daoIM_KYOTEN;

        private IM_BUSHODao daoIM_BUSHO;

        private IM_SHAINDao daoIM_SHAIN;

        private IM_SHUUKEI_KOUMOKUDao daoIM_SHUUKEI_KOUMOKU;

        private IM_GYOUSHADao daoIM_GYOUSHA;

        private IM_GYOUSHUDao daoIM_GYOUSHU;

        private IM_TODOUFUKENDao daoIM_TODOUFUKEN;

        private IM_SYS_INFODao daoIM_SYS_INFO;

        private IM_BANKDao daoIM_BANK;

        private IM_BANK_SHITENDao daoIM_BANK_SHITEN;

        private IM_NYUUSHUKKIN_KBNDao daoIM_NYUUSHUKKIN_KBN;

        private IS_ZIP_CODEDao daoIS_ZIP_CODE;

        private IM_CORP_INFODao daoIM_CORP_INFO;

        private int rowCntGyousha;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_HIKIAI_TORIHIKISAKI SearchString { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string torihikisakiCD { get; set; }

        /// <summary>
        /// 引合Flag
        /// </summary>
        public string hikiaiFLG { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// 検索結果(業者一覧)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                Console.WriteLine("G612Logic created!");

                this.form = targetForm;

                this.entitysTORIHIKISAKI = new M_HIKIAI_TORIHIKISAKI();
                this.entitysTOROHIKISAKI_SHIHARAI = new M_HIKIAI_TORIHIKISAKI_SHIHARAI();
                this.entitysTORIHIKISAKI_SEIKYUU = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();
                this.entitysKARITORIHIKISAKI = new M_KARI_TORIHIKISAKI();
                this.entitysKARITOROHIKISAKI_SHIHARAI = new M_KARI_TORIHIKISAKI_SHIHARAI();
                this.entitysKARITORIHIKISAKI_SEIKYUU = new M_KARI_TORIHIKISAKI_SEIKYUU();
                this.entitysM_KYOTEN = new M_KYOTEN();
                this.entitysM_BUSHO = new M_BUSHO();
                this.entitysM_SHAIN = new M_SHAIN();
                this.entitysM_SHUUKEI_KOUMOKU = new M_SHUUKEI_KOUMOKU();
                this.entitysM_GYOUSHU = new M_GYOUSHU();
                this.entitysM_TODOUFUKEN = new M_TODOUFUKEN();
                this.entitysM_SYS_INFO = new M_SYS_INFO();
                this.entitysM_BANK = new M_BANK();
                this.entitysM_BANK_SHITEN = new M_BANK_SHITEN();
                this.entitysM_NYUUSHUKKIN_KBN = new M_NYUUSHUKKIN_KBN();

                this.daoTORIHIKISAKI = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoTORIHIKISAKI_SHIHARAI = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao>();
                this.daoTORIHIKISAKI_SEIKYUU = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao>();
                this.daoKARITORIHIKISAKI = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_KARI_TORIHIKISAKIDao>();
                this.daoKARITORIHIKISAKI_SHIHARAI = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_KARI_TORIHIKISAKI_SHIHARAIDao>();
                this.daoKARITORIHIKISAKI_SEIKYUU = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.TorihikisakiKakunin.Dao.IM_KARI_TORIHIKISAKI_SEIKYUUDao>();
                this.daoIM_KYOTEN = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoIM_BUSHO = DaoInitUtility.GetComponent<IM_BUSHODao>();
                this.daoIM_SHAIN = DaoInitUtility.GetComponent<IM_SHAINDao>();
                this.daoIM_SHUUKEI_KOUMOKU = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
                this.daoIM_GYOUSHA = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.daoIM_GYOUSHU = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
                this.daoIM_TODOUFUKEN = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
                this.daoIM_SYS_INFO = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoIM_BANK = DaoInitUtility.GetComponent<IM_BANKDao>();
                this.daoIM_BANK_SHITEN = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                this.daoIM_NYUUSHUKKIN_KBN = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
                this.daoIS_ZIP_CODE = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
                this.daoIM_CORP_INFO = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                var parentForm = (BusinessBaseForm)this.form.Parent;


                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                // 画面初期化
                // 検索結果を画面に設定
                this.SetWindowData();
                this.SetDataForWindow();

                // functionボタン
                parentForm.bt_func12.Enabled = true;    // 閉じる

                this.allControl = this.form.allControl;

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                this.setOnlineBankVisible();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string cd;

                cd = this.torihikisakiCD;

                //仮マスタ
                if (this.hikiaiFLG == "0")
                {
                    this.entitysKARITORIHIKISAKI = daoKARITORIHIKISAKI.GetDataByCd(cd);
                    this.entitysKARITORIHIKISAKI_SEIKYUU = daoKARITORIHIKISAKI_SEIKYUU.GetDataByCd(cd);
                    this.entitysKARITOROHIKISAKI_SHIHARAI = daoKARITORIHIKISAKI_SHIHARAI.GetDataByCd(cd);

                    this.SetKARITORIHIKISAKIEntity();
                    this.SetKARITORIHIKISAKI_SEIKYUUEntity();
                    this.SetKARITORIHIKISAKI_SHIHARAIEntity();
                }//引合マスタ
                else
                {
                    this.entitysTORIHIKISAKI = daoTORIHIKISAKI.GetDataByCd(cd);
                    this.entitysTORIHIKISAKI_SEIKYUU = daoTORIHIKISAKI_SEIKYUU.GetDataByCd(cd);
                    this.entitysTOROHIKISAKI_SHIHARAI = daoTORIHIKISAKI_SHIHARAI.GetDataByCd(cd);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                throw;
            }
            finally
            {

                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 仮マスタのデータを引合マスタに保存する
        /// </summary>
        private void SetKARITORIHIKISAKIEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.entitysTORIHIKISAKI.TORIHIKISAKI_CD = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_CD;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1 = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2 = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_FURIGANA;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_TEL;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_FAX;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1 = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2 = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = this.entitysKARITORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD;
                this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD = this.entitysKARITORIHIKISAKI.EIGYOU_TANTOU_CD;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_POST = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_POST;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1 = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2 = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.entitysTORIHIKISAKI.TORIHIKI_JOUKYOU = this.entitysKARITORIHIKISAKI.TORIHIKI_JOUKYOU;
                this.entitysTORIHIKISAKI.CHUUSHI_RIYUU1 = this.entitysKARITORIHIKISAKI.CHUUSHI_RIYUU1;
                this.entitysTORIHIKISAKI.CHUUSHI_RIYUU2 = this.entitysKARITORIHIKISAKI.CHUUSHI_RIYUU2;
                this.entitysTORIHIKISAKI.BUSHO = this.entitysKARITORIHIKISAKI.BUSHO;
                this.entitysTORIHIKISAKI.TANTOUSHA = this.entitysKARITORIHIKISAKI.TANTOUSHA;
                this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD = this.entitysKARITORIHIKISAKI.SHUUKEI_ITEM_CD;
                this.entitysTORIHIKISAKI.GYOUSHU_CD = this.entitysKARITORIHIKISAKI.GYOUSHU_CD;
                this.entitysTORIHIKISAKI.BIKOU1 = this.entitysKARITORIHIKISAKI.BIKOU1;
                this.entitysTORIHIKISAKI.BIKOU2 = this.entitysKARITORIHIKISAKI.BIKOU2;
                this.entitysTORIHIKISAKI.BIKOU3 = this.entitysKARITORIHIKISAKI.BIKOU3;
                this.entitysTORIHIKISAKI.BIKOU4 = this.entitysKARITORIHIKISAKI.BIKOU4;
                this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN = this.entitysKARITORIHIKISAKI.NYUUKINSAKI_KBN;
                this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN = this.entitysKARITORIHIKISAKI.DAIHYOU_PRINT_KBN;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KBN = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_KBN;
                this.entitysTORIHIKISAKI.SHOKUCHI_KBN = this.entitysKARITORIHIKISAKI.SHOKUCHI_KBN;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1 = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_NAME1;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2 = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_NAME2;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_POST;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1 = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2 = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_BUSHO;
                this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU = this.entitysKARITORIHIKISAKI.MANI_HENSOUSAKI_TANTOU;
                this.entitysTORIHIKISAKI.TEKIYOU_BEGIN = this.entitysKARITORIHIKISAKI.TEKIYOU_BEGIN;
                this.entitysTORIHIKISAKI.SEARCH_TEKIYOU_BEGIN = this.entitysKARITORIHIKISAKI.SEARCH_TEKIYOU_BEGIN;
                this.entitysTORIHIKISAKI.TEKIYOU_END = this.entitysKARITORIHIKISAKI.TEKIYOU_END;
                this.entitysTORIHIKISAKI.SEARCH_TEKIYOU_END = this.entitysKARITORIHIKISAKI.SEARCH_TEKIYOU_END;
                this.entitysTORIHIKISAKI.DELETE_FLG = this.entitysKARITORIHIKISAKI.DELETE_FLG;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_CD_AFTER = this.entitysKARITORIHIKISAKI.TORIHIKISAKI_CD_AFTER;
                this.entitysTORIHIKISAKI.CREATE_DATE = this.entitysKARITORIHIKISAKI.CREATE_DATE;
                this.entitysTORIHIKISAKI.CREATE_USER = this.entitysKARITORIHIKISAKI.CREATE_USER;
                this.entitysTORIHIKISAKI.UPDATE_DATE = this.entitysKARITORIHIKISAKI.UPDATE_DATE;
                this.entitysTORIHIKISAKI.UPDATE_USER = this.entitysKARITORIHIKISAKI.UPDATE_USER;
                this.entitysTORIHIKISAKI.TIME_STAMP = this.entitysKARITORIHIKISAKI.TIME_STAMP;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKARITORIHIKISAKIEntity", ex);
                throw;
            }
            finally
            {

                LogUtility.DebugMethodEnd();
            }

        }


        /// <summary>
        /// 仮マスタのデータを引合マスタに保存する
        /// </summary>
        private void SetKARITORIHIKISAKI_SEIKYUUEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1 = this.entitysKARITORIHIKISAKI_SEIKYUU.SHIMEBI1;
                this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2 = this.entitysKARITORIHIKISAKI_SEIKYUU.SHIMEBI2;
                this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3 = this.entitysKARITORIHIKISAKI_SEIKYUU.SHIMEBI3;
                this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI = this.entitysKARITORIHIKISAKI_SEIKYUU.HICCHAKUBI;
                this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH = this.entitysKARITORIHIKISAKI_SEIKYUU.KAISHUU_MONTH;
                this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY = this.entitysKARITORIHIKISAKI_SEIKYUU.KAISHUU_DAY;
                this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU = this.entitysKARITORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2;
                this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = this.entitysKARITORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA;
                this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE = this.entitysKARITORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE;
                this.entitysTORIHIKISAKI_SEIKYUU.SEARCH_LAST_TORIHIKI_DATE = this.entitysKARITORIHIKISAKI_SEIKYUU.SEARCH_LAST_TORIHIKI_DATE;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.YOUSHI_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.ZEI_KBN_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.TAX_HASUU_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI = this.entitysKARITORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO = this.entitysKARITORIHIKISAKI_SEIKYUU.KOUZA_NO;
                this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME = this.entitysKARITORIHIKISAKI_SEIKYUU.KOUZA_NAME;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2 = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX;
                this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD;
                this.entitysTORIHIKISAKI_SEIKYUU.CREATE_DATE = this.entitysKARITORIHIKISAKI_SEIKYUU.CREATE_DATE;
                this.entitysTORIHIKISAKI_SEIKYUU.CREATE_USER = this.entitysKARITORIHIKISAKI_SEIKYUU.CREATE_USER;
                this.entitysTORIHIKISAKI_SEIKYUU.UPDATE_DATE = this.entitysKARITORIHIKISAKI_SEIKYUU.UPDATE_DATE;
                this.entitysTORIHIKISAKI_SEIKYUU.UPDATE_USER = this.entitysKARITORIHIKISAKI_SEIKYUU.UPDATE_USER;
                this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP = this.entitysKARITORIHIKISAKI_SEIKYUU.TIME_STAMP;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN = this.entitysKARITORIHIKISAKI_SEIKYUU.OUTPUT_KBN;
                this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = this.entitysKARITORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKARITORIHIKISAKI_SEIKYUUEntity", ex);
                throw;
            }
            finally
            {

                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 仮マスタのデータを引合マスタに保存する
        /// </summary>
        private void SetKARITORIHIKISAKI_SHIHARAIEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.entitysTOROHIKISAKI_SHIHARAI.TORIHIKISAKI_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.TORIHIKISAKI_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI1 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIMEBI1;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI2 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIMEBI2;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI3 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIMEBI3;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_MONTH = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_MONTH;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_DAY = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_DAY;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2;
                this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = this.entitysKARITOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA;
                this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_KBN = this.entitysKARITOROHIKISAKI_SHIHARAI.SHOSHIKI_KBN;
                this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN = this.entitysKARITOROHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN;
                this.entitysTOROHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE = this.entitysKARITOROHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE;
                this.entitysTOROHIKISAKI_SHIHARAI.SEARCH_LAST_TORIHIKI_DATE = this.entitysKARITOROHIKISAKI_SHIHARAI.SEARCH_LAST_TORIHIKI_DATE;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN;
                this.entitysTOROHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN = this.entitysKARITOROHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN;
                this.entitysTOROHIKISAKI_SHIHARAI.YOUSHI_KBN = this.entitysKARITOROHIKISAKI_SHIHARAI.YOUSHI_KBN;
                this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KBN_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.ZEI_KBN_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.TAX_HASUU_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.TAX_HASUU_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2 = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX;
                this.entitysTOROHIKISAKI_SHIHARAI.SYUUKINSAKI_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.SYUUKINSAKI_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD = this.entitysKARITOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD;
                this.entitysTOROHIKISAKI_SHIHARAI.CREATE_DATE = this.entitysKARITOROHIKISAKI_SHIHARAI.CREATE_DATE;
                this.entitysTOROHIKISAKI_SHIHARAI.CREATE_USER = this.entitysKARITOROHIKISAKI_SHIHARAI.CREATE_USER;
                this.entitysTOROHIKISAKI_SHIHARAI.UPDATE_DATE = this.entitysKARITOROHIKISAKI_SHIHARAI.UPDATE_DATE;
                this.entitysTOROHIKISAKI_SHIHARAI.UPDATE_USER = this.entitysKARITOROHIKISAKI_SHIHARAI.UPDATE_USER;
                this.entitysTOROHIKISAKI_SHIHARAI.TIME_STAMP = this.entitysKARITOROHIKISAKI_SHIHARAI.TIME_STAMP;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKARITORIHIKISAKI_SHIHARAIEntity", ex);
                throw;
            }
            finally
            {

                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// データをDBから取得
        /// </summary>
        public void SetDataForWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
                header.CreateDate.Text = this.entitysTORIHIKISAKI.CREATE_DATE.ToString();
                header.CreateUser.Text = this.entitysTORIHIKISAKI.CREATE_USER;
                header.LastUpdateDate.Text = this.entitysTORIHIKISAKI.UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.entitysTORIHIKISAKI.UPDATE_USER;

                // 共通情報
                this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI.Text = ConvertStrByte.ByteToString(this.entitysTORIHIKISAKI.TIME_STAMP);
                if (!this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.IsNull)
                {
                    this.form.NYUUKINSAKI_KBN.Text = this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.ToString();
                }
                if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.entitysM_KYOTEN = this.daoIM_KYOTEN.GetDataByCd(this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.ToString());
                    this.form.TORIHIKISAKI_KYOTEN_CD.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.ToString();
                    if (this.entitysM_KYOTEN != null)
                    {
                        this.form.KYOTEN_NAME_RYAKU.Text = this.entitysM_KYOTEN.KYOTEN_NAME_RYAKU;
                    }
                }
                this.form.TORIHIKISAKI_CD.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                this.form.TORIHIKISAKI_NAME1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.form.TORIHIKISAKI_FURIGANA.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA;
                this.form.TORIHIKISAKI_NAME2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU;
                this.form.TORIHIKISAKI_TEL.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
                this.form.TORIHIKISAKI_FAX.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX;
                this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD;
                if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD))
                {
                    this.entitysM_BUSHO = this.daoIM_BUSHO.GetDataByCd(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD);
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = this.entitysM_BUSHO.BUSHO_NAME_RYAKU;
                }
                this.form.EIGYOU_TANTOU_CD.Text = this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD;
                if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD))
                {
                    this.entitysM_SHAIN = this.daoIM_SHAIN.GetDataByCd(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD);
                    if (entitysM_SHAIN != null)
                    {
                        this.form.EIGYOU_TANTOU_NAME.Text = this.entitysM_SHAIN.SHAIN_NAME_RYAKU;
                    }
                }
                if (!this.entitysTORIHIKISAKI.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TEKIYOU_BEGIN.Value = (DateTime)this.entitysTORIHIKISAKI.TEKIYOU_BEGIN;
                }
                else
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                if (!this.entitysTORIHIKISAKI.TEKIYOU_END.IsNull)
                {
                    this.form.TEKIYOU_END.Value = (DateTime)this.entitysTORIHIKISAKI.TEKIYOU_END;
                }
                else
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                this.form.CHUUSHI_RIYUU1.Text = this.entitysTORIHIKISAKI.CHUUSHI_RIYUU1;
                this.form.CHUUSHI_RIYUU2.Text = this.entitysTORIHIKISAKI.CHUUSHI_RIYUU2;

                // 基本情報タブ
                this.form.TORIHIKISAKI_POST.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_POST;
                if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Format("{0:D" + this.form.TORIHIKISAKI_TODOUFUKEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString()));
                    this.entitysM_TODOUFUKEN = this.daoIM_TODOUFUKEN.GetDataByCd(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (this.entitysM_TODOUFUKEN != null)
                    {
                        this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = this.entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }
                this.form.TORIHIKISAKI_ADDRESS1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.form.TORIHIKISAKI_ADDRESS2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.form.BUSHO.Text = this.entitysTORIHIKISAKI.BUSHO;
                this.form.TANTOUSHA.Text = this.entitysTORIHIKISAKI.TANTOUSHA;
                this.form.SHUUKEI_ITEM_CD.Text = this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD;
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD))
                {
                    this.entitysM_SHUUKEI_KOUMOKU = this.daoIM_SHUUKEI_KOUMOKU.GetDataByCd(this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD);
                    if (this.entitysM_SHUUKEI_KOUMOKU != null)
                    {
                        this.form.SHUUKEI_KOUMOKU_NAME.Text = this.entitysM_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU;
                    }
                }
                this.form.GYOUSHU_CD.Text = this.entitysTORIHIKISAKI.GYOUSHU_CD;
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI.GYOUSHU_CD))
                {
                    this.entitysM_GYOUSHU = this.daoIM_GYOUSHU.GetDataByCd(this.entitysTORIHIKISAKI.GYOUSHU_CD);
                    if (this.entitysM_GYOUSHU != null)
                    {
                        this.form.GYOUSHU_NAME.Text = this.entitysM_GYOUSHU.GYOUSHU_NAME_RYAKU;
                    }
                }
                this.form.DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.DAIHYOU_PRINT_KBN.Text = this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.BIKOU1.Text = this.entitysTORIHIKISAKI.BIKOU1;
                this.form.BIKOU2.Text = this.entitysTORIHIKISAKI.BIKOU2;
                this.form.BIKOU3.Text = this.entitysTORIHIKISAKI.BIKOU3;
                this.form.BIKOU4.Text = this.entitysTORIHIKISAKI.BIKOU4;
                this.form.SHOKUCHI_KBN.Checked = (bool)this.entitysTORIHIKISAKI.SHOKUCHI_KBN;

                //請求情報1タブ
                if (this.entitysTORIHIKISAKI_SEIKYUU != null)
                {
                    this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI_SEIKYUU.Text = ConvertStrByte.ByteToString(this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP);
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD.IsNull)
                    {
                        this.form.TORIHIKI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.TORIHIKI_KBN.Text = "";
                    }
                    //this.ChangeTorihikiKbn();

                    if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1.IsNull)
                    {
                        this.form.SHIMEBI1.Text = "";
                    }
                    else
                    {
                        this.form.SHIMEBI1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1.ToString();
                    }

                    if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2.IsNull)
                    {
                        this.form.SHIMEBI2.Text = "";
                    }
                    else
                    {
                        this.form.SHIMEBI2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2.ToString();
                    }

                    if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3.IsNull)
                    {
                        this.form.SHIMEBI3.Text = "";
                    }
                    else
                    {
                        this.form.SHIMEBI3.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3.ToString();
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI.IsNull)
                    {
                        this.form.HICCHAKUBI.Text = this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI.ToString();
                    }
                    else
                    {
                        this.form.HICCHAKUBI.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH.IsNull)
                    {
                        this.form.KAISHUU_MONTH.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH.ToString();
                    }
                    else
                    {
                        this.form.KAISHUU_MONTH.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY.IsNull)
                    {
                        this.form.KAISHUU_DAY.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY.ToString();
                    }
                    else
                    {
                        this.form.KAISHUU_DAY.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU.IsNull)
                    {
                        this.form.KAISHUU_HOUHOU.Text = string.Format("{0:D" + this.form.KAISHUU_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU.ToString()));
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.KAISHUU_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        this.form.KAISHUU_HOUHOU.Text = "";
                        this.form.KAISHUU_HOUHOU_NAME.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA.IsNull)
                    {
                        this.SetZandakaFormat(this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA.ToString(), this.form.KAISHI_URIKAKE_ZANDAKA);
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHOSHIKI_KBN.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD.IsNull)
                    {
                        this.form.TAX_HASUU_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.TAX_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD.IsNull)
                    {
                        this.form.KINGAKU_HASUU_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.KINGAKU_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KEITAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SEIKYUU_KEITAI_KBN.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.NYUUKIN_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.NYUUKIN_MEISAI_KBN.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN.IsNull)
                    {
                        this.form.YOUSHI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN.ToString();
                    }
                    else
                    {
                        this.form.YOUSHI_KBN.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD.IsNull)
                    {
                        this.form.ZEI_KEISAN_KBN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.ZEI_KEISAN_KBN_CD.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD.IsNull)
                    {
                        this.form.ZEI_KBN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.ZEI_KBN_CD.Text = "";
                    }

                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN.IsNull)
                    {
                        this.form.OUTPUT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN.ToString();
                    }
                    else
                    {
                        this.form.OUTPUT_KBN.Text = "";
                    }

                    this.form.HAKKOUSAKI_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    // 請求情報2タブ
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                    {
                        this.form.FURIKOMI_BANK_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                    {
                        this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD);
                        if (this.entitysM_BANK != null)
                        {
                            this.form.FURIKOMI_BANK_NAME.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                    {
                        this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                        this.form.KOUZA_SHURUI.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                        this.form.KOUZA_NO.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                        this.form.KOUZA_NAME.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                    {
                        M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                        bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                        bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                        bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                        bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                        this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                        if (this.entitysM_BANK_SHITEN != null)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        }
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE.IsNull)
                    {
                        this.form.LAST_TORIHIKI_DATE.Text = string.Format("yyyy/MM/dd HH:mm:ss", this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE);
                    }
                    this.form.SEIKYUU_JOUHOU1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1;
                    this.form.SEIKYUU_JOUHOU2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2;
                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2;
                    this.form.SEIKYUU_SOUFU_POST.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2;
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX;
                    this.form.NYUUKINSAKI_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD;
                    if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD))
                    {
                        M_NYUUKINSAKI nyu = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>().GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD);
                        if (nyu != null)
                        {
                            this.form.NYUUKINSAKI_NAME1.Text = nyu.NYUUKINSAKI_NAME1;
                            this.form.NYUUKINSAKI_NAME2.Text = nyu.NYUUKINSAKI_NAME2;
                        }
                    }
                    this.form.SEIKYUU_TANTOU.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                    }
                    //this.ChangeSeikyuuKyotenPrintKbn();
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD.ToString()));
                        M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                    this.form.FURIKOMI_NAME1.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME1;
                    this.form.FURIKOMI_NAME2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME2;
                }

                //支払情報1タブ
                if (this.entitysTOROHIKISAKI_SHIHARAI != null)
                {
                    this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI_SHIHARAI.Text = ConvertStrByte.ByteToString(this.entitysTOROHIKISAKI_SHIHARAI.TIME_STAMP);
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD.IsNull)
                    {
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = "";
                    }
                    //this.ChangeSiharaiTorihikiKbn();

                    if (this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI1.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI1.Text = "";
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI1.ToString();
                    }

                    if (this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI2.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI2.Text = "";
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI2.ToString();
                    }

                    if (this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI3.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI3.Text = "";
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI3.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI3.ToString();
                    }

                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_MONTH.IsNull)
                    {
                        this.form.SHIHARAI_MONTH.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_MONTH.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_MONTH.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_DAY.IsNull)
                    {
                        this.form.SHIHARAI_DAY.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_DAY.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_DAY.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU.IsNull)
                    {
                        this.form.SHIHARAI_HOUHOU.Text = string.Format("{0:D" + this.form.SHIHARAI_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU.ToString()));
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.SHIHARAI_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        this.form.SHIHARAI_HOUHOU.Text = "";
                        this.form.SHIHARAI_HOUHOU_NAME.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                    {
                        this.SetZandakaFormat(this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA.ToString(), this.form.KAISHI_KAIKAKE_ZANDAKA);
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_SHOSHIKI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.TAX_HASUU_CD.IsNull)
                    {
                        this.form.SHIHARAI_TAX_HASUU_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.TAX_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_TAX_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD.IsNull)
                    {
                        this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KEITAI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_KEITAI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.SHUKKIN_MEISAI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHUKKIN_MEISAI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.YOUSHI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_YOUSHI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.YOUSHI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_YOUSHI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD.IsNull)
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KBN_CD.IsNull)
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE.IsNull)
                    {
                        this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Format("yyyy/MM/dd HH:mm:ss", this.entitysTOROHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE);
                    }

                    // 支払情報2タブ
                    this.form.SHIHARAI_JOUHOU1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1;
                    this.form.SHIHARAI_JOUHOU2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2;
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2;
                    this.form.SHIHARAI_SOUFU_POST.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU;
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL;
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                    }
                    //this.ChangeShiharaiKyotenPrintKbn();

                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD.ToString()));
                        M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                }

                // 取引先分類タブ
                this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KBN;
                if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                }
                if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    this.form.MANI_HENSOUSAKI_NAME1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.MANI_HENSHOUSAKI_POST.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU;
                }
                else
                {
                    if (!this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    }
                    this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.form.MANI_HENSHOUSAKI_POST.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                }

                // 業者一覧タブ
                if (this.entitysM_SYS_INFO.GYOUSHA_TORIHIKI_CHUUSHI.IsNull)
                {
                    this.form.TORIHIKI_STOP.Text = string.Empty;
                }
                else
                {
                    this.form.TORIHIKI_STOP.Text = this.entitysM_SYS_INFO.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
                }
                this.rowCntGyousha = this.SearchGyousha();

                if (this.rowCntGyousha > 0)
                {
                    this.SetIchiranGyousha();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForWindow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 残高項目のフォーマット処理
        /// </summary>
        public void SetZandakaFormat(string zan, CustomNumericTextBox2 target)
        {
            try
            {
                LogUtility.DebugMethodStart(zan, target);

                if (string.IsNullOrWhiteSpace(zan))
                {
                    return;
                }

                // マイナスが先頭以外に付与されていたら削除する
                var minus = zan.StartsWith("-");
                zan = (minus ? "-" : string.Empty) + zan.Replace("-", string.Empty).Replace(",", string.Empty);

                target.Text = string.Format("{0:#,##0}", Decimal.Parse(zan));
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZandakaFormat", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 検索結果を業者一覧に設定
        /// </summary>
        internal void SetIchiranGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var table = this.SearchResultGyousha;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.GYOUSHA_ICHIRAN.DataSource = table;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGyousha = new DataTable();

                string torihikisakiCd = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                if (string.IsNullOrWhiteSpace(torihikisakiCd))
                {
                    return 0;
                }

                if (this.hikiaiFLG == "1")
                {
                    M_HIKIAI_GYOUSHA condition = new M_HIKIAI_GYOUSHA();
                    condition.TORIHIKISAKI_CD = torihikisakiCd;
                    if (!string.IsNullOrWhiteSpace(this.form.TORIHIKI_STOP.Text))
                    {
                        condition.TORIHIKI_JOUKYOU = Int16.Parse(this.form.TORIHIKI_STOP.Text);
                    }

                    this.SearchResultGyousha = this.daoTORIHIKISAKI.GetIchiranHikiaiGyoushaData(condition);
                }
                else
                {
                    M_KARI_GYOUSHA condition = new M_KARI_GYOUSHA();
                    condition.TORIHIKISAKI_CD = torihikisakiCd;
                    if (!string.IsNullOrWhiteSpace(this.form.TORIHIKI_STOP.Text))
                    {
                        condition.TORIHIKI_JOUKYOU = Int16.Parse(this.form.TORIHIKI_STOP.Text);
                    }

                    this.SearchResultGyousha = this.daoKARITORIHIKISAKI.GetIchiranKariGyoushaData(condition);
                }

                int count = this.SearchResultGyousha.Rows.Count;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            try
            {
                LogUtility.DebugMethodStart(inputData);

                if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
                {
                    return inputData;
                }

                PropertyInfo pi = this.form.TORIHIKISAKI_CD.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.TORIHIKISAKI_CD, null);

                string padData = inputData.PadLeft((int)charNumber, '0');

                return padData;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZeroPadding", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //ボタン初期化処理
        //</summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //ボタン設定の読込
        //</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        #region IBuisinessLogicで必須実装(未使用)
        public int Search()
        {
            throw new NotImplementedException();
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        private bool setDensiSeikyushoVisible()
        {
            // densiVisible true場合表示false場合非表示
            bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();

            if (!densiVisible)
            {
                this.form.labelOutputKbn.Visible = densiVisible;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = densiVisible;
                this.form.labelHakkosaki.Visible = densiVisible;
                this.form.HAKKOUSAKI_CD.Visible = densiVisible;
                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 44);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 44);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 44);
                this.form.panel12.Location = new System.Drawing.Point(this.form.panel12.Location.X, this.form.panel12.Location.Y - 44);

            }
            return densiVisible;
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 en
        
        /// <summary>
        /// 画面起動時にオプション有無を確認し、オンラインバンク連携で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        /// <returns></returns>
        private bool setOnlineBankVisible()
        {
            bool onlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsOnlinebank();
            if (!onlineBankVisible)
            {
                this.form.FURIKOMI_NAME1.Visible = onlineBankVisible;
                this.form.FURIKOMI_NAME2.Visible = onlineBankVisible;
                this.form.label15.Visible = onlineBankVisible;
                this.form.label16.Visible = onlineBankVisible;
            }
            return onlineBankVisible;
        }
    }
}