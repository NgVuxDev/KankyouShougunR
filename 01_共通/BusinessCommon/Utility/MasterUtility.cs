using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// 各マスタ情報を取得するのに各マスタ情報取得のメッソド
    /// </summary>
    public class MasterUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public enum DELETE_FLAG
        {
            NODELETE = 0,
            DELETE = 1,
            BOTH = 2
        }

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <param name="keyEntity">M_SYS_INFO</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_SYS_INFO</returns>
        public static M_SYS_INFO GetSysInfo(M_SYS_INFO keyEntity, DELETE_FLAG delflg)
        {
            M_SYS_INFO result = new M_SYS_INFO();
            IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var sysInfoResult = sysInfoDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (sysInfoResult == null || sysInfoResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = sysInfoResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.SYS_ID.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var sysInfoResultDelete = sysInfoDao.GetAllDataForCode(keyEntity.SYS_ID.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (sysInfoResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (sysInfoResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = sysInfoResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.SYS_ID.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var sysInfoResults = sysInfoDao.GetAllDataForCode(keyEntity.SYS_ID.ToString());

                    // PK指定のため1件
                    result = sysInfoResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// M_CORP_INFOを取得する
        /// </summary>
        /// <returns>M_CORP_INFO</returns>
        public static M_CORP_INFO GetCorpInfo()
        {
            IM_CORP_INFODao CorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            M_CORP_INFO returnEntity = CorpInfoDao.GetAllData().FirstOrDefault();
            return returnEntity;
        }

        /// <summary>
        /// 社員を取得する
        /// </summary>
        /// <param name="keyEntity">M_SHAIN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_SHAIN</returns>
        public static M_SHAIN GetShain(M_SHAIN keyEntity, DELETE_FLAG delflg)
        {
            M_SHAIN result = new M_SHAIN();
            IM_SHAINDao shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var shainResult = shainDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (shainResult == null || shainResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = shainResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.SHAIN_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shainResultDelete = shainDao.GetDataByCd(keyEntity.SHAIN_CD);

                    //DBから情報が取れない場合、return NULL
                    if (shainResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (shainResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = shainResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.SHAIN_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shainResults = shainDao.GetDataByCd(keyEntity.SHAIN_CD);

                    // PK指定のため1件
                    result = shainResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 集計項目を取得する
        /// </summary>
        /// <param name="keyEntity">M_SHUUKEI_KOUMOKU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_SHUUKEI_KOUMOKU</returns>
        public static M_SHUUKEI_KOUMOKU GetShuukei(M_SHUUKEI_KOUMOKU keyEntity,DELETE_FLAG delflg)
        {
            M_SHUUKEI_KOUMOKU result = new M_SHUUKEI_KOUMOKU();
            IM_SHUUKEI_KOUMOKUDao ShuukeiDao = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var shuukeisno = ShuukeiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (shuukeisno == null || shuukeisno.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = shuukeisno[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.SHUUKEI_KOUMOKU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shuukeisdelete = ShuukeiDao.GetDataByCd(keyEntity.SHUUKEI_KOUMOKU_CD);

                    //DBから情報が取れない場合、return NULL
                    if (shuukeisdelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (shuukeisdelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = shuukeisdelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if(string.IsNullOrEmpty(keyEntity.SHUUKEI_KOUMOKU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shuukeis = ShuukeiDao.GetDataByCd(keyEntity.SHUUKEI_KOUMOKU_CD);

                    // PK指定のため1件
                    result = shuukeis;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 部署を取得する
        /// </summary>
        /// <param name="keyEntity">M_BUSHO</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_BUSHO</returns>
        public static M_BUSHO GetBusho(M_BUSHO keyEntity, DELETE_FLAG delflg)
        {
            M_BUSHO result = new M_BUSHO();
            IM_BUSHODao BushoDao = DaoInitUtility.GetComponent<IM_BUSHODao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var bushoResult = BushoDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (bushoResult == null || bushoResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = bushoResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.BUSHO_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var bushoResultDelete = BushoDao.GetDataByCd(keyEntity.BUSHO_CD);

                    //DBから情報が取れない場合、return NULL
                    if (bushoResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (bushoResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = bushoResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.BUSHO_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var bushoResults = BushoDao.GetDataByCd(keyEntity.BUSHO_CD);

                    // PK指定のため1件
                    result = bushoResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 地域を取得する
        /// </summary>
        /// <param name="keyEntity">M_CHIIKI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_CHIIKI</returns>
        public static M_CHIIKI GetChiiki(M_CHIIKI keyEntity, DELETE_FLAG delflg)
        {
            M_CHIIKI result = new M_CHIIKI();
            IM_CHIIKIDao ChiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var chiikiResult = ChiikiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (chiikiResult == null || chiikiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = chiikiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.CHIIKI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var chiikiResultDelete = ChiikiDao.GetDataByCd(keyEntity.CHIIKI_CD);

                    //DBから情報が取れない場合、return NULL
                    if (chiikiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (chiikiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = chiikiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.CHIIKI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var chiikiResults = ChiikiDao.GetDataByCd(keyEntity.CHIIKI_CD);

                    // PK指定のため1件
                    result = chiikiResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 業種を取得する
        /// </summary>
        /// <param name="keyEntity">M_GYOUSHU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_GYOUSHU</returns>
        public static M_GYOUSHU GetGyoushu(M_GYOUSHU keyEntity, DELETE_FLAG delflg)
        {
            M_GYOUSHU result = new M_GYOUSHU();
            IM_GYOUSHUDao GyoushuDao = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var gyoushuResult = GyoushuDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (gyoushuResult == null || gyoushuResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = gyoushuResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var gyoushuResultDelete = GyoushuDao.GetDataByCd(keyEntity.GYOUSHU_CD);

                    //DBから情報が取れない場合、return NULL
                    if (gyoushuResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (gyoushuResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = gyoushuResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var gyoushuResults = GyoushuDao.GetDataByCd(keyEntity.GYOUSHU_CD);

                    // PK指定のため1件
                    result = gyoushuResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 営業担当者を取得する
        /// </summary>
        /// <param name="keyEntity">M_EIGYOU_TANTOUSHA</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_EIGYOU_TANTOUSHA</returns>
        public static M_EIGYOU_TANTOUSHA GetEigyouTantouSha(M_EIGYOU_TANTOUSHA keyEntity, DELETE_FLAG delflg)
        {
            M_EIGYOU_TANTOUSHA result = new M_EIGYOU_TANTOUSHA();
            IM_EIGYOU_TANTOUSHADao EigyouDao = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var eigyouResult = EigyouDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (eigyouResult == null || eigyouResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = eigyouResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.SHAIN_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var eigyouResultDelete = EigyouDao.GetDataByCd(keyEntity.SHAIN_CD);

                    //DBから情報が取れない場合、return NULL
                    if (eigyouResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (eigyouResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = eigyouResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.SHAIN_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var eigyouResults = EigyouDao.GetDataByCd(keyEntity.SHAIN_CD);

                    // PK指定のため1件
                    result = eigyouResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 都道府県を取得する
        /// </summary>
        /// <param name="keyEntity">M_TODOUFUKEN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_TODOUFUKEN</returns>
        public static M_TODOUFUKEN GetTodoufuken(M_TODOUFUKEN keyEntity, DELETE_FLAG delflg)
        {
            M_TODOUFUKEN result = new M_TODOUFUKEN();
            IM_TODOUFUKENDao TodoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var todoufukenResult = TodoufukenDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (todoufukenResult == null || todoufukenResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = todoufukenResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.TODOUFUKEN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var todoufukenResultDelete = TodoufukenDao.GetDataByCd(keyEntity.TODOUFUKEN_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (todoufukenResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (todoufukenResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = todoufukenResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.TODOUFUKEN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var todoufukenResults = TodoufukenDao.GetDataByCd(keyEntity.TODOUFUKEN_CD.ToString());

                    // PK指定のため1件
                    result = todoufukenResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// メニュー権限を取得する
        /// </summary>
        /// <param name="keyEntity">M_MENU_AUTH</param>
        /// <returns>M_MENU_AUTH</returns>
        public static M_MENU_AUTH GetMenuKengenHoshu(M_MENU_AUTH keyEntity)
        {
            IM_MENU_AUTHDao menuAuthDao = DaoInitUtility.GetComponent<IM_MENU_AUTHDao>();

            if (keyEntity == null)
            {
                return null;
            }

            var menu = menuAuthDao.GetAllValidData(keyEntity);
            if (menu == null || menu.Length < 1)
            {
                return null;
            }
            else
            {
                return menu[0];
            }
        }

        /// <summary>
        /// 業者を取得する
        /// </summary>
        /// <param name="keyEntity">M_GYOUSHA</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_GYOUSHA</returns>
        public static M_GYOUSHA GetGyousha(M_GYOUSHA keyEntity, DELETE_FLAG delflg)
        {
            M_GYOUSHA result = new M_GYOUSHA();
            IM_GYOUSHADao GyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var gyoushaResult = GyoushaDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (gyoushaResult == null || gyoushaResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = gyoushaResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var gyoushaResultDelete = GyoushaDao.GetDataByCd(keyEntity.GYOUSHA_CD);

                    //DBから情報が取れない場合、return NULL
                    if (gyoushaResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (gyoushaResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = gyoushaResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var gyoushaResults = GyoushaDao.GetDataByCd(keyEntity.GYOUSHA_CD);

                    // PK指定のため1件
                    result = gyoushaResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 現場を取得する
        /// </summary>
        /// <param name="keyEntity"></param>
        /// <returns>現場一覧</returns>
        public static M_GENBA[] GetGenbaByGyousha(M_GENBA keyEntity)
        {
            IM_GENBADao GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            //業者コードが入力されない場合、return NULL
            if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD))
            {
                return null;
            }

            var result = GenbaDao.GetAllValidData(keyEntity);

            //DBから情報が取れない場合、return NULL
            if (result == null || result.Length < 1)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// 現場を取得する
        /// </summary>
        /// <param name="keyEntity">M_GENBA</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_GENBA</returns>
        public static M_GENBA GetGenba(M_GENBA keyEntity, DELETE_FLAG delflg)
        {
            M_GENBA result = new M_GENBA();
            IM_GENBADao GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var genbaResult = GenbaDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (genbaResult == null || genbaResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = genbaResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD) || string.IsNullOrEmpty(keyEntity.GENBA_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var genbaResultDelete = GenbaDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (genbaResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (genbaResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = genbaResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD) || string.IsNullOrEmpty(keyEntity.GENBA_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var genbaResults = GenbaDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = genbaResults;
                    break;
            }
            return result;
        }
                
        /// <summary>
        /// 容器を取得する
        /// </summary>
        /// <param name="keyEntity">M_YOUKI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_YOUKI</returns>
        public static M_YOUKI GetYouki(M_YOUKI keyEntity, DELETE_FLAG delflg)
        {
            M_YOUKI result = new M_YOUKI();
            IM_YOUKIDao youkiDao = DaoInitUtility.GetComponent<IM_YOUKIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var youkiResult = youkiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (youkiResult == null || youkiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = youkiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.YOUKI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var youkiResultDelete = youkiDao.GetDataByCd(keyEntity.YOUKI_CD);

                    //DBから情報が取れない場合、return NULL
                    if (youkiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (youkiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = youkiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.YOUKI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var youkiResults = youkiDao.GetDataByCd(keyEntity.YOUKI_CD);

                    // PK指定のため1件
                    result = youkiResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 取引先を取得する
        /// </summary>
        /// <param name="keyEntity">M_TORIHIKISAKI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_TORIHIKISAKI</returns>
        public static M_TORIHIKISAKI GetTorihikisaki(M_TORIHIKISAKI keyEntity, DELETE_FLAG delflg)
        {
            M_TORIHIKISAKI result = new M_TORIHIKISAKI();
            IM_TORIHIKISAKIDao TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var torihikisakiResult = TorihikisakiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (torihikisakiResult == null || torihikisakiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = torihikisakiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.TORIHIKISAKI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var torihikisakiResultDelete = TorihikisakiDao.GetDataByCd(keyEntity.TORIHIKISAKI_CD);

                    //DBから情報が取れない場合、return NULL
                    if (torihikisakiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (torihikisakiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = torihikisakiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.TORIHIKISAKI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var torihikisakiResults = TorihikisakiDao.GetDataByCd(keyEntity.TORIHIKISAKI_CD);

                    // PK指定のため1件
                    result = torihikisakiResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 取引先_請求情報を取得する
        /// </summary>
        /// <param name="keyEntity">M_TORIHIKISAKI_SEIKYUU</param>
        /// <returns>M_TORIHIKISAKI_SEIKYUU</returns>
        public static M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuu(M_TORIHIKISAKI_SEIKYUU keyEntity)
        {
            IM_TORIHIKISAKI_SEIKYUUDao TorihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(keyEntity.TORIHIKISAKI_CD))
            {
                return null;
            }

            var torihikisakiSeikyuu = TorihikisakiSeikyuuDao.GetDataByCd(keyEntity.TORIHIKISAKI_CD);

            //DBから情報が取れない場合、return NULL
            if (torihikisakiSeikyuu == null)
            {
                return null;
            }

            return torihikisakiSeikyuu;
        }

        /// <summary>
        /// 取引先_支払情報を取得する
        /// </summary>
        /// <param name="keyEntity">M_TORIHIKISAKI_SHIHARAI</param>
        /// <returns>M_TORIHIKISAKI_SHIHARAI</returns>
        public static M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharai(M_TORIHIKISAKI_SHIHARAI keyEntity)
        {
            IM_TORIHIKISAKI_SHIHARAIDao TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(keyEntity.TORIHIKISAKI_CD))
            {
                return null;
            }

            var torihikisakiShiharai = TorihikisakiShiharaiDao.GetDataByCd(keyEntity.TORIHIKISAKI_CD);

            //DBから情報が取れない場合、return NULL
            if (torihikisakiShiharai == null)
            {
                return null;
            }

            return torihikisakiShiharai;
        }

        /// <summary>
        /// 車輌を取得する
        /// </summary>
        /// <param name="keyEntity">M_SHARYOU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_SHARYOU</returns>
        public static M_SHARYOU GetSharyou(M_SHARYOU keyEntity, DELETE_FLAG delflg)
        {
            M_SHARYOU result = new M_SHARYOU();
            IM_SHARYOUDao sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var sharyouResult = sharyouDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (sharyouResult == null || sharyouResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = sharyouResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD) || string.IsNullOrEmpty(keyEntity.SHARYOU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var sharyouResultDelete = sharyouDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (sharyouResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (sharyouResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = sharyouResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.GYOUSHA_CD) || string.IsNullOrEmpty(keyEntity.SHARYOU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var sharyouResults = sharyouDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = sharyouResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 単位区分を取得する
        /// </summary>
        /// <param name="keyEntity">M_UNIT</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_UNIT</returns>
        public static M_UNIT GetUnit(M_UNIT keyEntity, DELETE_FLAG delflg)
        {
            M_UNIT result = new M_UNIT();
            IM_UNITDao UnitDao = DaoInitUtility.GetComponent<IM_UNITDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var unitResult = UnitDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (unitResult == null || unitResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = unitResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.UNIT_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var unitResultDelete = UnitDao.GetDataByCd(int.Parse(keyEntity.UNIT_CD.ToString()));

                    //DBから情報が取れない場合、return NULL
                    if (unitResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (unitResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = unitResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.UNIT_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var unitResults = UnitDao.GetDataByCd(int.Parse(keyEntity.UNIT_CD.ToString()));

                    // PK指定のため1件
                    result = unitResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 拠点を取得する
        /// </summary>
        /// <param name="keyEntity">M_KYOTEN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_KYOTEN</returns>
        public static M_KYOTEN GetKyoten(M_KYOTEN keyEntity, DELETE_FLAG delflg)
        {
            M_KYOTEN result = new M_KYOTEN();
            IM_KYOTENDao kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var kyotenResult = kyotenDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (kyotenResult == null || kyotenResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = kyotenResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.KYOTEN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var kyotenResultDelete = kyotenDao.GetDataByCd(keyEntity.KYOTEN_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (kyotenResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (kyotenResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = kyotenResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.KYOTEN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var kyotenResults = kyotenDao.GetDataByCd(keyEntity.KYOTEN_CD.ToString());

                    // PK指定のため1件
                    result = kyotenResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// マニフェスト種類を取得する
        /// </summary>
        /// <param name="keyEntity">M_MANIFEST_SHURUI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_MANIFEST_SHURUI</returns>
        public static M_MANIFEST_SHURUI GetManifestShurui(M_MANIFEST_SHURUI keyEntity, DELETE_FLAG delflg)
        {
            M_MANIFEST_SHURUI result = new M_MANIFEST_SHURUI();
            IM_MANIFEST_SHURUIDao manifestShuruiDao = DaoInitUtility.GetComponent<IM_MANIFEST_SHURUIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var manifestShuruiResult = manifestShuruiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (manifestShuruiResult == null || manifestShuruiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = manifestShuruiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.MANIFEST_SHURUI_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var manifestShuruiResultDelete = manifestShuruiDao.GetDataByCd(keyEntity.MANIFEST_SHURUI_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (manifestShuruiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (manifestShuruiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = manifestShuruiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.MANIFEST_SHURUI_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var manifestShuruiResults = manifestShuruiDao.GetDataByCd(keyEntity.MANIFEST_SHURUI_CD.ToString());

                    // PK指定のため1件
                    result = manifestShuruiResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// マニフェスト手配を取得する
        /// </summary>
        /// <param name="keyEntity">M_MANIFEST_TEHAI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_MANIFEST_TEHAI</returns>
        public static M_MANIFEST_TEHAI GetManifestTehai(M_MANIFEST_TEHAI keyEntity, DELETE_FLAG delflg)
        {
            M_MANIFEST_TEHAI result = new M_MANIFEST_TEHAI();
            IM_MANIFEST_TEHAIDao manifestTehaiDao = DaoInitUtility.GetComponent<IM_MANIFEST_TEHAIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var manifestTehaiResult = manifestTehaiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (manifestTehaiResult == null || manifestTehaiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = manifestTehaiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.MANIFEST_TEHAI_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var manifestTehaiResultDelete = manifestTehaiDao.GetDataByCd(keyEntity.MANIFEST_TEHAI_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (manifestTehaiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (manifestTehaiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = manifestTehaiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.MANIFEST_TEHAI_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var manifestTehaiResults = manifestTehaiDao.GetDataByCd(keyEntity.MANIFEST_TEHAI_CD.ToString());

                    // PK指定のため1件
                    result = manifestTehaiResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 品名の情報を取得する
        /// </summary>
        /// <param name="keyEntity">M_HINMEI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_HINMEI</returns>
        public static M_HINMEI GetHinmei(M_HINMEI keyEntity, DELETE_FLAG delflg)
        {
            M_HINMEI result = new M_HINMEI();
            IM_HINMEIDao hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var hinmeiResult = hinmeiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (hinmeiResult == null || hinmeiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = hinmeiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.HINMEI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var hinmeiResultDelete = hinmeiDao.GetDataByCd(keyEntity.HINMEI_CD);

                    //DBから情報が取れない場合、return NULL
                    if (hinmeiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (hinmeiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = hinmeiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.HINMEI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var hinmeiResults = hinmeiDao.GetDataByCd(keyEntity.HINMEI_CD);

                    // PK指定のため1件
                    result = hinmeiResults;
                    break;
            }
            return result;
        }      

        /// <summary>
        /// 車種を取得する
        /// </summary>
        /// <param name="keyEntity">M_SHASHU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_SHASHU</returns>
        public static M_SHASHU GetShashu(M_SHASHU keyEntity, DELETE_FLAG delflg)
        {
            M_SHASHU result = new M_SHASHU();
            IM_SHASHUDao shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var shashuResult = shashuDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (shashuResult == null || shashuResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = shashuResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.SHASHU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shashuResultDelete = shashuDao.GetDataByCd(keyEntity.SHASHU_CD);

                    //DBから情報が取れない場合、return NULL
                    if (shashuResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (shashuResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = shashuResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.SHASHU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shashuResults = shashuDao.GetDataByCd(keyEntity.SHASHU_CD);

                    // PK指定のため1件
                    result = shashuResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// M_MANIFEST_KANSANを取得する
        /// </summary>
        /// <param name="keyEntity">M_MANIFEST_KANSAN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_MANIFEST_KANSAN</returns>
        public static M_MANIFEST_KANSAN GetManifestKansan(M_MANIFEST_KANSAN keyEntity, DELETE_FLAG delflg)
        {
            M_MANIFEST_KANSAN result = new M_MANIFEST_KANSAN();
            IM_MANIFEST_KANSANDao manifestkansanDao = DaoInitUtility.GetComponent<IM_MANIFEST_KANSANDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var manifestkansanResult = manifestkansanDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (manifestkansanResult == null || manifestkansanResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = manifestkansanResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.HOUKOKUSHO_BUNRUI_CD) || string.IsNullOrEmpty(keyEntity.HAIKI_NAME_CD)
                        || keyEntity.UNIT_CD.IsNull || string.IsNullOrEmpty(keyEntity.NISUGATA_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var manifestkansanResultDelete = manifestkansanDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (manifestkansanResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (manifestkansanResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = manifestkansanResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.HOUKOKUSHO_BUNRUI_CD) || string.IsNullOrEmpty(keyEntity.HAIKI_NAME_CD)
                        || keyEntity.UNIT_CD.IsNull || string.IsNullOrEmpty(keyEntity.NISUGATA_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var manifestkansanResults = manifestkansanDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = manifestkansanResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 廃棄物種類を取得する
        /// </summary>
        /// <param name="keyEntity">M_HAIKI_SHURUI</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_HAIKI_SHURUI</returns>
        public static M_HAIKI_SHURUI GetHaikiShurui(M_HAIKI_SHURUI keyEntity, DELETE_FLAG delflg)
        {
            M_HAIKI_SHURUI result = new M_HAIKI_SHURUI();
            IM_HAIKI_SHURUIDao haikishuruiDao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var haikishuruiResult = haikishuruiDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (haikishuruiResult == null || haikishuruiResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = haikishuruiResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.HAIKI_KBN_CD.IsNull || string.IsNullOrEmpty(keyEntity.HAIKI_SHURUI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var haikishuruiResultDelete = haikishuruiDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (haikishuruiResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (haikishuruiResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = haikishuruiResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.HAIKI_KBN_CD.IsNull || string.IsNullOrEmpty(keyEntity.HAIKI_SHURUI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var haikishuruiResults = haikishuruiDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = haikishuruiResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// コンテナ状況を取得する
        /// </summary>
        /// <param name="keyEntity">M_CONTENA_JOUKYOU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_CONTENA_JOUKYOU</returns>
        public static M_CONTENA_JOUKYOU GetContenaJoukyou(M_CONTENA_JOUKYOU keyEntity, DELETE_FLAG delflg)
        {
            M_CONTENA_JOUKYOU result = new M_CONTENA_JOUKYOU();
            IM_CONTENA_JOUKYOUDao contenaJoukyouDao = DaoInitUtility.GetComponent<IM_CONTENA_JOUKYOUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var contenaJoukyouResult = contenaJoukyouDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (contenaJoukyouResult == null || contenaJoukyouResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = contenaJoukyouResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.CONTENA_JOUKYOU_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var contenaJoukyouResultDelete = contenaJoukyouDao.GetDataByCd(keyEntity.CONTENA_JOUKYOU_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (contenaJoukyouResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (contenaJoukyouResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = contenaJoukyouResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.CONTENA_JOUKYOU_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var contenaJoukyouResults = contenaJoukyouDao.GetDataByCd(keyEntity.CONTENA_JOUKYOU_CD.ToString());

                    // PK指定のため1件
                    result = contenaJoukyouResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 形態区分を取得する
        /// </summary>
        /// <param name="keyEntity">M_KEITAI_KBN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_KEITAI_KBN</returns>
        public static M_KEITAI_KBN GetkeitaiKbn(M_KEITAI_KBN keyEntity, DELETE_FLAG delflg)
        {
            M_KEITAI_KBN result = new M_KEITAI_KBN();
            IM_KEITAI_KBNDao keitaiKbnDao = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var keitaiKbnResult = keitaiKbnDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (keitaiKbnResult == null || keitaiKbnResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = keitaiKbnResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.KEITAI_KBN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var keitaiKbnResultDelete = keitaiKbnDao.GetDataByCd(keyEntity.KEITAI_KBN_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (keitaiKbnResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (keitaiKbnResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = keitaiKbnResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.KEITAI_KBN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var keitaiKbnResults = keitaiKbnDao.GetDataByCd(keyEntity.KEITAI_KBN_CD.ToString());

                    // PK指定のため1件
                    result = keitaiKbnResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 廃棄区分を取得する
        /// </summary>
        /// <param name="keyEntity">M_HAIKI_KBN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_HAIKI_KBN</returns>
        public static M_HAIKI_KBN GetHaikiKbn(M_HAIKI_KBN keyEntity, DELETE_FLAG delflg)
        {
            M_HAIKI_KBN result = new M_HAIKI_KBN();
            IM_HAIKI_KBNDao haikiKbnDao = DaoInitUtility.GetComponent<IM_HAIKI_KBNDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var haikiKbnResult = haikiKbnDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (haikiKbnResult == null || haikiKbnResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = haikiKbnResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.HAIKI_KBN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var haikiKbnResultDelete = haikiKbnDao.GetDataByCd(keyEntity.HAIKI_KBN_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (haikiKbnResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (haikiKbnResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = haikiKbnResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.HAIKI_KBN_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var haikiKbnResults = haikiKbnDao.GetDataByCd(keyEntity.HAIKI_KBN_CD.ToString());

                    // PK指定のため1件
                    result = haikiKbnResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 廃棄名を取得する
        /// </summary>
        /// <param name="keyEntity">M_HAIKI_NAME</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_HAIKI_NAME</returns>
        public static M_HAIKI_NAME GetHaikiName(M_HAIKI_NAME keyEntity, DELETE_FLAG delflg)
        {
            M_HAIKI_NAME result = new M_HAIKI_NAME();
            IM_HAIKI_NAMEDao haikiNameDao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var haikiNameResult = haikiNameDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (haikiNameResult == null || haikiNameResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = haikiNameResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.HAIKI_NAME_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var haikiNameResultDelete = haikiNameDao.GetDataByCd(keyEntity.HAIKI_NAME_CD);

                    //DBから情報が取れない場合、return NULL
                    if (haikiNameResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (haikiNameResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = haikiNameResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.HAIKI_NAME_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var haikiNameResults = haikiNameDao.GetDataByCd(keyEntity.HAIKI_NAME_CD);

                    // PK指定のため1件
                    result = haikiNameResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// M_GENCHAKU_TIMEを取得する
        /// </summary>
        /// <param name="keyEntity">M_GENCHAKU_TIME</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_GENCHAKU_TIME</returns>
        public static M_GENCHAKU_TIME GetGenchakuTime(M_GENCHAKU_TIME keyEntity, DELETE_FLAG delflg)
        {
            M_GENCHAKU_TIME result = new M_GENCHAKU_TIME();
            IM_GENCHAKU_TIMEDao genchakuTimeDao = DaoInitUtility.GetComponent<IM_GENCHAKU_TIMEDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var genchakuTimeResult = genchakuTimeDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (genchakuTimeResult == null || genchakuTimeResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = genchakuTimeResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.GENCHAKU_TIME_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var genchakuTimeResultDelete = genchakuTimeDao.GetDataByCd(keyEntity.GENCHAKU_TIME_CD.ToString());

                    //DBから情報が取れない場合、return NULL
                    if (genchakuTimeResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (genchakuTimeResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = genchakuTimeResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.GENCHAKU_TIME_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var genchakuTimeResults = genchakuTimeDao.GetDataByCd(keyEntity.GENCHAKU_TIME_CD.ToString());

                    // PK指定のため1件
                    result = genchakuTimeResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 消費税率を取得する
        /// </summary>
        /// <param name="denpyouHiduke"></param>
        /// <returns>M_SHOUHIZEI</returns>
        public static M_SHOUHIZEI GetShouhizeiRate(DateTime denpyouHiduke)
        {
            M_SHOUHIZEI returnEntity = new M_SHOUHIZEI();
            IM_SHOUHIZEIDao shouhizeiDao = DaoInitUtility.GetComponent<IM_SHOUHIZEIDao>();

            //対象日付を入力されない場合、RETURN NULL
            if (denpyouHiduke == null)
            {
                return returnEntity;
            }

            returnEntity = shouhizeiDao.GetDataByDate(denpyouHiduke);

            return returnEntity;
        }

        /// <summary>
        /// M_KONGOU_HAIKIBUTSUを取得する
        /// </summary>
        /// <param name="keyEntity">M_KONGOU_HAIKIBUTSU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_KONGOU_HAIKIBUTSU</returns>
        public static M_KONGOU_HAIKIBUTSU GetKongouHaikibutsu(M_KONGOU_HAIKIBUTSU keyEntity, DELETE_FLAG delflg)
        {
            M_KONGOU_HAIKIBUTSU result = new M_KONGOU_HAIKIBUTSU();
            IM_KONGOU_HAIKIBUTSUDao KongouHaikibutuDao = DaoInitUtility.GetComponent<IM_KONGOU_HAIKIBUTSUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var kongouHaikibutuResult = KongouHaikibutuDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (kongouHaikibutuResult == null || kongouHaikibutuResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = kongouHaikibutuResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.HAIKI_KBN_CD.IsNull || string.IsNullOrEmpty(keyEntity.KONGOU_SHURUI_CD) 
                        || string.IsNullOrEmpty(keyEntity.HAIKI_SHURUI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var kongouHaikibutuResultDelete = KongouHaikibutuDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (kongouHaikibutuResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (kongouHaikibutuResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = kongouHaikibutuResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.HAIKI_KBN_CD.IsNull || string.IsNullOrEmpty(keyEntity.KONGOU_SHURUI_CD)
                        || string.IsNullOrEmpty(keyEntity.HAIKI_SHURUI_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var kongouHaikibutuResults = KongouHaikibutuDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = kongouHaikibutuResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 処分担当者を取得する
        /// </summary>
        /// <param name="keyEntity">M_SHOBUN_TANTOUSHA</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_SHOBUN_TANTOUSHA</returns>
        public static M_SHOBUN_TANTOUSHA GetShobunTantousha(M_SHOBUN_TANTOUSHA keyEntity, DELETE_FLAG delflg)
        {
            M_SHOBUN_TANTOUSHA result = new M_SHOBUN_TANTOUSHA();
            IM_SHOBUN_TANTOUSHADao ShobunTantoushaDaoCls = DaoInitUtility.GetComponent<IM_SHOBUN_TANTOUSHADao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var shobunTantoushaResult = ShobunTantoushaDaoCls.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (shobunTantoushaResult == null || shobunTantoushaResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = shobunTantoushaResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.SHAIN_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shobunTantoushaResultDelete = ShobunTantoushaDaoCls.GetDataByCd(keyEntity.SHAIN_CD);

                    //DBから情報が取れない場合、return NULL
                    if (shobunTantoushaResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (shobunTantoushaResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = shobunTantoushaResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.SHAIN_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var shobunTantoushaResults = ShobunTantoushaDaoCls.GetDataByCd(keyEntity.SHAIN_CD);

                    // PK指定のため1件
                    result = shobunTantoushaResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 換算を取得する
        /// </summary>
        /// <param name="keyEntity">M_KANSAN</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_KANSAN</returns>
        public static M_KANSAN GetKansan(M_KANSAN keyEntity, DELETE_FLAG delflg)
        {
            M_KANSAN result = new M_KANSAN();
            IM_KANSANDao KansanDataDao = DaoInitUtility.GetComponent<IM_KANSANDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var kansanResult = KansanDataDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (kansanResult == null || kansanResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = kansanResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (keyEntity.DENPYOU_KBN_CD.IsNull || string.IsNullOrEmpty(keyEntity.HINMEI_CD)
                        || keyEntity.UNIT_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var kansanResultDelete = KansanDataDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (kansanResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (kansanResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = kansanResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (keyEntity.DENPYOU_KBN_CD.IsNull || string.IsNullOrEmpty(keyEntity.HINMEI_CD)
                        || keyEntity.UNIT_CD.IsNull)
                    {
                        return null;
                    }

                    //DBから取得
                    var kansanResults = KansanDataDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = kansanResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// M_GENNYOURITSUを取得する
        /// </summary>
        /// <param name="keyEntity">M_GENNYOURITSU</param>
        /// <param name="delflg">DELETE_FLAG</param>
        /// <returns>M_GENNYOURITSU</returns>
        public static M_GENNYOURITSU GetGennyouritsu(M_GENNYOURITSU keyEntity, DELETE_FLAG delflg)
        {
            M_GENNYOURITSU result = new M_GENNYOURITSU();
            IM_GENNYOURITSUDao GenyouDataDao = DaoInitUtility.GetComponent<IM_GENNYOURITSUDao>();

            //データがなければ、return NULL
            if (keyEntity == null)
            {
                return null;
            }

            switch (delflg)
            {
                case DELETE_FLAG.NODELETE:
                    //DBから取得
                    var gennyouritsuResult = GenyouDataDao.GetAllValidData(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (gennyouritsuResult == null || gennyouritsuResult.Length < 1)
                    {
                        result = null;
                    }
                    else
                    {
                        // PK指定のため1件
                        result = gennyouritsuResult[0];
                    }
                    break;
                case DELETE_FLAG.DELETE:
                    if (string.IsNullOrEmpty(keyEntity.HOUKOKUSHO_BUNRUI_CD) || string.IsNullOrEmpty(keyEntity.HAIKI_NAME_CD)
                        || string.IsNullOrEmpty(keyEntity.SHOBUN_HOUHOU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var gennyouritsuResultDelete = GenyouDataDao.GetDataByCd(keyEntity);

                    //DBから情報が取れない場合、return NULL
                    if (gennyouritsuResultDelete == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (gennyouritsuResultDelete.DELETE_FLG == true)
                        {
                            // PK指定のため1件
                            result = gennyouritsuResultDelete;
                        }
                        else
                        {
                            result = null;
                        }
                    }
                    break;
                case DELETE_FLAG.BOTH:
                    if (string.IsNullOrEmpty(keyEntity.HOUKOKUSHO_BUNRUI_CD) || string.IsNullOrEmpty(keyEntity.HAIKI_NAME_CD)
                        || string.IsNullOrEmpty(keyEntity.SHOBUN_HOUHOU_CD))
                    {
                        return null;
                    }

                    //DBから取得
                    var gennyouritsuResults = GenyouDataDao.GetDataByCd(keyEntity);

                    // PK指定のため1件
                    result = gennyouritsuResults;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void CopyProperties<T, TU>(T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    {
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }
    }
}
