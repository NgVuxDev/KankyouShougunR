// $Id: $
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

namespace MasterCommon.Logic
{
    public class MasterCommonLogic
    {
        /// <summary>
        /// フォームからメニューオブジェクトを取得する
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static r_framework.APP.Base.RibbonMainMenu GetRibbonMainMenu(SuperForm form)
        {
            Form parentForm = form.ParentForm;
            r_framework.APP.Base.RibbonMainMenu menu = null;

            if (parentForm.GetType() == typeof(BusinessBaseForm))
            {
                menu = (r_framework.APP.Base.RibbonMainMenu)((BusinessBaseForm)parentForm).ribbonForm;
            }
            if (parentForm.GetType() == typeof(MasterBaseForm))
            {
                menu = (r_framework.APP.Base.RibbonMainMenu)((MasterBaseForm)parentForm).ribbonForm;
            }
            if (parentForm.GetType() == typeof(IchiranBaseForm))
            {
                menu = (r_framework.APP.Base.RibbonMainMenu)((IchiranBaseForm)parentForm).ribbonForm;
            }

            return menu;
        }

        /// <summary>
        /// メニューから現在ログイン中の社員情報を取得する
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static M_SHAIN GetCurrentShain(SuperForm form)
        {
            var menu = GetRibbonMainMenu(form);
            var shain = new M_SHAIN();

            if (menu != null)
            {
                var _shain = menu.GlobalCommonInformation.CurrentShain;
                shain.BUSHO_CD = _shain.BUSHO_CD;
                shain.CREATE_DATE = _shain.CREATE_DATE;
                shain.CREATE_PC = _shain.CREATE_PC;
                shain.CREATE_USER = _shain.CREATE_USER;
                shain.DELETE_FLG = _shain.DELETE_FLG;
                shain.EIGYOU_TANTOU_KBN = _shain.EIGYOU_TANTOU_KBN;
                shain.LOGIN_ID = _shain.LOGIN_ID;
                shain.MAIL_ADDRESS = _shain.MAIL_ADDRESS;
                shain.NYUURYOKU_TANTOU_KBN = _shain.NYUURYOKU_TANTOU_KBN;
                shain.PASSWORD = _shain.PASSWORD;
                shain.SEARCH_CREATE_DATE = _shain.SEARCH_CREATE_DATE;
                shain.SEARCH_UPDATE_DATE = _shain.SEARCH_UPDATE_DATE;
                shain.SHAIN_BIKOU = _shain.SHAIN_BIKOU;
                shain.SHAIN_CD = _shain.SHAIN_CD;
                shain.SHAIN_FURIGANA = _shain.SHAIN_FURIGANA;
                shain.SHAIN_NAME = _shain.SHAIN_NAME;
                shain.SHAIN_NAME_RYAKU = _shain.SHAIN_NAME_RYAKU;
                shain.SHOBUN_TANTOU_KBN = _shain.SHOBUN_TANTOU_KBN;
                shain.TEGATA_HOKAN_KBN = _shain.TEGATA_HOKAN_KBN;
                shain.INXS_TANTOU_FLG = _shain.INXS_TANTOU_FLG;
                shain.UNTEN_KBN = _shain.UNTEN_KBN;
                shain.UPDATE_DATE = _shain.UPDATE_DATE;
                shain.UPDATE_PC = _shain.UPDATE_PC;
                shain.UPDATE_USER = _shain.UPDATE_USER;

                //20250310
                shain.WARIATE_JUN = _shain.WARIATE_JUN;

                //20250311
                shain.NIN_I_TORIHIKISAKI_FUKA = _shain.NIN_I_TORIHIKISAKI_FUKA;
            }

            return shain;
        }

        /// <summary>
        /// エンティティのフッタープロパティを設定する
        /// </summary>
        /// <param name="shain"></param>
        /// <param name="entity"></param>
        public static void SetFooterProperty(M_SHAIN shain, SuperEntity entity)
        {
            entity.CREATE_USER = shain.SHAIN_NAME_RYAKU;
            entity.UPDATE_USER = shain.SHAIN_NAME_RYAKU;
        }

        public static M_CHIIKI SearchChiikiFromAddress(string todoufukenCd, string address)
        {
            M_CHIIKI chiiki = null;
            M_TODOUFUKEN todoufuken = null;

            string chiikiCode = string.Empty;

            // 都道府県コードが入力されている場合、一旦都道府県を地域とする
            if (!string.IsNullOrWhiteSpace(todoufukenCd))
            {
                chiikiCode = todoufukenCd.PadLeft(6, '0');
                todoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>().GetDataByCd(chiikiCode);
            }

            // 住所1が入力されている場合、最初の市区町村で区切って、その市区町村が地域にあるかチェックする
            if (!string.IsNullOrWhiteSpace(address))
            {
                string addr = address;

                // 都道府県を取得し、除去
                if (todoufuken != null)
                {
                    addr = Regex.Replace(addr, todoufuken.TODOUFUKEN_NAME_RYAKU, "");
                }
                else
                {
                    if (Regex.Match(addr, ".{2,3}?[都道府県]").Length > 0)
                    {
                        string tmpAddr = "";
                        tmpAddr = Regex.Match(addr, ".{2,3}?[都道府県]").Value;
                        //"("が残っていると後続の正規表現でエラーするため置換する。他の正規表現文字列も同様かもしれないが、"("だけとりあえず対応。
                        tmpAddr = tmpAddr.Replace("(", "");

                        todoufuken = new M_TODOUFUKEN();
                        todoufuken.TODOUFUKEN_NAME_RYAKU = tmpAddr;
                    }
                }

                // 市区を取得する
                MatchCollection shikuArray;

                // 市区を元に地域マスタをチェックする
                // ※都道府県文字以前の除去前でチェック
                shikuArray = Regex.Matches(addr, ".*?[市区]");
                M_CHIIKI cond = new M_CHIIKI();
                cond.CHIIKI_NAME = string.Empty;
                for (int i = 0; i < shikuArray.Count; i++)
                {
                    cond.CHIIKI_NAME += shikuArray[i].Value.ToString();
                    M_CHIIKI[] chiikiArray = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllValidData(cond);
                    if (chiikiArray != null && chiikiArray.Length > 0)
                    {
                        // 最初に検索できた時点でループは終了する
                        chiiki = chiikiArray[0];
                        break;
                    }
                }

                // 市区を元に地域マスタをチェックする
                // ※都道府県文字以前の除去後でチェック
                if (chiiki == null && todoufuken != null && todoufuken.TODOUFUKEN_NAME_RYAKU != null && !string.IsNullOrWhiteSpace(todoufuken.TODOUFUKEN_NAME_RYAKU))
                {
                    addr = Regex.Replace(addr, todoufuken.TODOUFUKEN_NAME_RYAKU, "");
                    shikuArray = Regex.Matches(addr, ".*?[市区]");
                    cond.CHIIKI_NAME = string.Empty;
                    for (int i = 0; i < shikuArray.Count; i++)
                    {
                        cond.CHIIKI_NAME += shikuArray[i].Value.ToString();
                        M_CHIIKI[] chiikiArray = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllValidData(cond);
                        if (chiikiArray != null && chiikiArray.Length > 0)
                        {
                            // 最初に検索できた時点でループは終了する
                            chiiki = chiikiArray[0];
                            break;
                        }
                    }
                }

                // 都道府県名から地域を検索する
                if (chiiki == null && todoufuken != null && todoufuken.TODOUFUKEN_NAME_RYAKU != null && !string.IsNullOrWhiteSpace(todoufuken.TODOUFUKEN_NAME_RYAKU))
                {
                    cond.CHIIKI_NAME = todoufuken.TODOUFUKEN_NAME_RYAKU;
                    M_CHIIKI[] chiikiArray = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllValidData(cond);
                    if (chiikiArray != null && chiikiArray.Length > 0)
                    {
                        // 最初に検索できた時点でループは終了する
                        chiiki = chiikiArray[0];
                    }
                }
            }

            // 地域が検索されておらず、地域コードが設定されている場合、地域マスタをチェック
            // ※都道府県が対象となる場合が該当
            if (chiiki == null && !string.IsNullOrWhiteSpace(chiikiCode))
            {
                chiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetDataByCd(chiikiCode);
            }

            return chiiki;
        }


        // Begin: LANDUONG - 20220209 - refs#160050
        public static bool CheckRakurakuCustomerCode(string code)
        {
            LogUtility.DebugMethodStart(code);

            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return true;
                }

                var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                var torihikisakiData = torihikisakiDao.GetDataByRakurakuCode(code);
                if (torihikisakiData != null && torihikisakiData.Length > 0)
                {
                    return false;
                }

                var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                var gyoushaData = gyoushaDao.GetDataByRakurakuCode(code);
                if (gyoushaData != null && gyoushaData.Length > 0)
                {
                    return false;
                }

                var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                var genbaData = genbaDao.GetDataByRakurakuCode(code);
                if (genbaData != null && genbaData.Length > 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRakurakuCustomerCode", ex);
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return true;
        }
        public static decimal IsOverRakurakuCodeLimit()
        {
            var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();

            var maxPlusCode = dao.GetMaxPlusRakurakuCode(null);

            var allCodeData = dao.GetAllRakurakuCodeData(null);
            foreach (string code in allCodeData)
            {
                var customerCode = decimal.Parse(code);
                if (customerCode == maxPlusCode)
                {
                    maxPlusCode = customerCode + 1;
                }
            }

            if (20 < maxPlusCode.ToString().Length)
            {
                if (allCodeData.Length == 1)
                {
                    return 1;
                }

                maxPlusCode = dao.GetMinBlankRakurakuCode(null);
                if (20 < maxPlusCode.ToString().Length)
                {
                    return -1;
                }
            }

            return maxPlusCode;
        }
        // End: LANDUONG - 20220209 - refs#160050

    }
}
