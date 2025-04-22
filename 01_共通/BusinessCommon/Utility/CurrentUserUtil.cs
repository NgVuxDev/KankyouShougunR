using System;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;


namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public class CurrentUserUtil
    {
        /// <summary>
        /// 拠点CD
        /// </summary>
        private const string KYOTEN_CD = "拠点CD";

        /// <summary>
        /// 部門CD
        /// </summary>
        private const string BUMON_CD = "部門CD";

        /// <summary>
        /// 荷降業者CD
        /// </summary>
        private const string NIOROSHI_GYOUSHA_CD = "荷降業者CD";

        /// <summary>
        /// 荷降現場CD
        /// </summary>
        private const string NIOROSHI_GENBA_CD = "荷降現場CD";

        /// <summary>
        /// 荷積業者CD
        /// </summary>
        private const string NITSUMI_GYOUSHA_CD = "荷積業者CD";

        /// <summary>
        /// 荷積現場CD
        /// </summary>
        private const string NITSUMI_GENBA_CD = "荷積現場CD";

        /// <summary>
        /// 領収書
        /// </summary>
        private const string RYOUSHUUSHO = "領収書";

        /// <summary>
        /// 継続入力
        /// </summary>
        private const string KEIZOKU_NYUURYOKU = "継続入力";

        /// <summary>
        /// 終了日警告
        /// </summary>
        private const string ENDDATE_USE_KBN_KOBETU = "終了日警告";

        /// <summary>
        /// 契約アラート
        /// </summary>
        private const string ITAKU_KEIYAKU_ALERT = "契約アラート";

        /// <summary>
        /// CurrentUserDtoクラスにXMLファイルとデータベースから取得したデータを保存する。
        /// </summary>
        public static CurrentUserDto CURRENTUSERINFO { get; set; }

        /// <summary>
        /// XMLファイルからコードを取得する。
        /// </summary>
        /// <param name="itemCd">拠点CD、部門CD、荷降業者CD、荷降現場CD、
        /// 荷積業者CD、荷積現場CD、領収書、継続入力、終了日警告、契約アラート</param>
        /// <returns>拠点CD、部門CD、荷降業者CD、荷降現場CD、
        /// 荷積業者CD、荷積現場CD、領収書、継続入力、終了日警告、契約アラート</returns>
        private static string GetDataCd(string itemCd)
        {
            string sResult = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(itemCd);

                CurrentUserCustomConfigProfile profile = CurrentUserCustomConfigProfile.Load();

                foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
                {
                    if (item.Name.Equals(itemCd))
                    {
                        sResult = item.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return sResult;
        }

        /// <summary>
        /// CurrentUserDtoクラスにXMLファイルとデータベースから取得したデータを保存する。
        /// </summary>
        public static void GetDataCurrentUserProfile()
        {

            CurrentUserDto currentUserDto = new CurrentUserDto();
            try
            {
                LogUtility.DebugMethodStart();

                #region "拠点"
                //拠点CD
                string kyotenCd = string.Empty;

                //ファイルから拠点CDを取得する。
                kyotenCd = GetDataCd(KYOTEN_CD);

                //拠点CD != NULL、拠点CD!= Empty
                if (!string.IsNullOrEmpty(kyotenCd))
                {
                    //拠点情報を取得する
                    M_KYOTEN kyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>().GetDataByCd(kyotenCd);

                    //データがある場合
                    if (kyoten != null)
                    {
                        currentUserDto.KYOTEN_CD = kyoten.KYOTEN_CD;
                        currentUserDto.KYOTEN_NAME = kyoten.KYOTEN_NAME;
                        currentUserDto.KYOTEN_NAME_RYAKU = kyoten.KYOTEN_NAME_RYAKU;
                    }
                }
                #endregion

                //#region "部門"
                ////部門CD
                //string bumonCD = string.Empty;

                ////ファイルから部門CDを取得する。
                //bumonCD = GetDataCd(BUMON_CD);

                ////部門CD != NULL、部門CD!= Empty
                //if (!string.IsNullOrEmpty(bumonCD))
                //{
                //    //部門情報を取得する
                //    M_BUMON bumon = DaoInitUtility.GetComponent<IM_BUMONDao>().GetDataByCd(bumonCD);

                //    //データがある場合
                //    if (bumon != null)
                //    {
                //        currentUserDto.BUMON_CD = bumon.BUMON_CD;
                //        currentUserDto.BUMON_NAME = bumon.BUMON_NAME;
                //        currentUserDto.BUMON_NAME_RYAKU = bumon.BUMON_NAME_RYAKU;
                //    }
                //}
                //#endregion

                #region"荷降業者"
                //荷降業者CD
                string niorishiGyoushaCd = string.Empty;

                //ファイルから荷降業者CDを取得する。
                niorishiGyoushaCd = GetDataCd(NIOROSHI_GYOUSHA_CD);

                //荷降業者CD != NULL、荷降業者CD!= Empty
                if (!string.IsNullOrEmpty(niorishiGyoushaCd))
                {
                    //荷降業者情報を取得する
                    M_GYOUSHA gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(niorishiGyoushaCd);

                    //データがある場合
                    if (gyousha != null)
                    {
                        currentUserDto.NIOROSHI_GYOUSHA_CD = gyousha.GYOUSHA_CD;
                        currentUserDto.NIOROSHI_GYOUSHA_NAME1 = gyousha.GYOUSHA_NAME1;
                        currentUserDto.NIOROSHI_GYOUSHA_NAME2 = gyousha.GYOUSHA_NAME2;
                        currentUserDto.NIOROSHI_GYOUSHA_NAME_RYAKU = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                #endregion

                #region "荷降現場"
                //荷降現場CD
                string niorishiGenbaCd = string.Empty;

                //ファイルから荷降現場CDを取得する。
                niorishiGenbaCd = GetDataCd(NIOROSHI_GENBA_CD);

                //荷降現場CD != NULL、荷降現場CD!= Empty
                if (!string.IsNullOrEmpty(niorishiGenbaCd))
                {
                    //検索条件
                    M_GENBA condGenba = new M_GENBA();
                    condGenba.GENBA_CD = niorishiGenbaCd;

                    //荷降現場情報を取得する
                    M_GENBA genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetDataByCd(condGenba);

                    //データがある場合
                    if (genba != null)
                    {
                        currentUserDto.NIOROSHI_GENBA_CD = genba.GENBA_CD;
                        currentUserDto.NIOROSHI_GENBA_NAME1 = genba.GENBA_NAME1;
                        currentUserDto.NIOROSHI_GENBA_NAME2 = genba.GENBA_NAME2;
                        currentUserDto.NIOROSHI_GENBA_NAME_RYAKU = genba.GENBA_NAME_RYAKU;
                    }
                }
                #endregion

                #region "荷積業者"
                //荷積業者CD
                string nitsumiGyoushaCd = string.Empty;

                //ファイルから荷積業者CDを取得する。
                nitsumiGyoushaCd = GetDataCd(NITSUMI_GYOUSHA_CD);

                //荷積業者CD != NULL、荷積業者CD!= Empty
                if (!string.IsNullOrEmpty(nitsumiGyoushaCd))
                {
                    //荷積業者情報を取得する
                    M_GYOUSHA gyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(nitsumiGyoushaCd);

                    //データがある場合
                    if (gyousha != null)
                    {
                        currentUserDto.NITSUMI_GYOUSHA_CD = gyousha.GYOUSHA_CD;
                        currentUserDto.NITSUMI_GYOUSHA_NAME1 = gyousha.GYOUSHA_NAME1;
                        currentUserDto.NITSUMI_GYOUSHA_NAME2 = gyousha.GYOUSHA_NAME2;
                        currentUserDto.NITSUMI_GYOUSHA_NAME_RYAKU = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                #endregion

                #region "荷積現場"
                //荷積現場CD
                string nitsumiGenbaCd = string.Empty;

                //ファイルから荷積現場CDを取得する。
                nitsumiGenbaCd = GetDataCd(NITSUMI_GENBA_CD);

                //荷積現場CD != NULL、荷積現場CD!= Empty
                if (!string.IsNullOrEmpty(nitsumiGenbaCd))
                {
                    //検索条件
                    M_GENBA condGenba = new M_GENBA();
                    condGenba.GENBA_CD = nitsumiGenbaCd;

                    //荷積現場情報を取得する
                    M_GENBA genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetDataByCd(condGenba);

                    //データがある場合
                    if (genba != null)
                    {
                        currentUserDto.NITSUMI_GENBA_CD = genba.GENBA_CD;
                        currentUserDto.NITSUMI_GENBA_NAME1 = genba.GENBA_NAME1;
                        currentUserDto.NITSUMI_GENBA_NAME2 = genba.GENBA_NAME2;
                        currentUserDto.NITSUMI_GENBA_NAME_RYAKU = genba.GENBA_NAME_RYAKU;
                    }
                }
                #endregion

                #region "領収書"
                //領収書
                string ryouShuuSho = string.Empty;

                //ファイルから領収書を取得する。
                ryouShuuSho = GetDataCd(RYOUSHUUSHO);

                //領収書 != NULL、領収書!= Empty
                if (!string.IsNullOrEmpty(ryouShuuSho))
                {
                    currentUserDto.RYOUSHUUSHO = ryouShuuSho;
                }
                #endregion

                #region "継続入力"
                //継続入力
                string keiZokuNyuuRyouku = string.Empty;

                //ファイルから継続入力を取得する。
                keiZokuNyuuRyouku = GetDataCd(KEIZOKU_NYUURYOKU);

                //継続入力 != NULL、継続入力!= Empty
                if (!string.IsNullOrEmpty(keiZokuNyuuRyouku))
                {
                    currentUserDto.KEIZOKU_NYUURYOKU = keiZokuNyuuRyouku;
                }
                #endregion

                #region "終了日警告"
                //終了日警告
                string endDateUseKbnKobetu = string.Empty;

                //ファイルから終了日警告を取得する。
                endDateUseKbnKobetu = GetDataCd(ENDDATE_USE_KBN_KOBETU);

                //終了日警告 != NULL、終了日警告!= Empty
                if (!string.IsNullOrEmpty(endDateUseKbnKobetu))
                {
                    currentUserDto.ENDDATE_USE_KBN_KOBETU = endDateUseKbnKobetu;
                }
                #endregion

                #region "契約アラート"
                //契約アラート
                string itakuKeiyakuAlert = string.Empty;

                //ファイルから契約アラートを取得する。
                itakuKeiyakuAlert = GetDataCd(ITAKU_KEIYAKU_ALERT);

                //契約アラート != NULL、契約アラート!= Empty
                if (!string.IsNullOrEmpty(itakuKeiyakuAlert))
                {
                    currentUserDto.ITAKU_KEIYAKU_ALERT = itakuKeiyakuAlert;
                }
                #endregion

                #region "CurrentUserDtoクラス"
                CURRENTUSERINFO = currentUserDto;
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}
