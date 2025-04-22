using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using System.Windows.Forms;
using Seasar.Quill.Attrs;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Data.SqlTypes;
//using Shougun.Core.Billing.SeikyuShimeShori;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class ShimeCheckLogic
    {
        #region フィールド

        /// <summary>
        /// SeikyuShimeShoriDao
        /// </summary>
        private SeikyuShimeShoriDao shimeShoriDao;

        ///// <summary>
        ///// SeikyuShimeShoriDto
        ///// </summary>
        //SeikyuShimeShoriDto shimeDto;

        /// <summary>
        /// エラーメッセージ用DataTable
        /// </summary>
        DataTable errorTable;

        /// <summary>
        /// 明細Tbl用DataTable
        /// </summary>
        DataTable ukeireTable;

        /// <summary>
        /// 出荷・明細Tbl用DataTable
        /// </summary>
        DataTable shukkaTable;

        /// <summary>
        /// 売上・明細Tbl用DataTable
        /// </summary>
        DataTable uriageTable;

        /// <summary>
        /// 入出金・明細Tbl用DataTable
        /// </summary>
        DataTable nyuukinTable;

        /// <summary>
        /// 入出金Tbl金額用DataTable
        /// </summary>
        DataTable nyukingakuTable;

        /// <summary>
        /// 各伝票Tblの合計金額取得用DataTable
        /// </summary>
        DataTable kingakuTable;

        /// <summary>
        /// 各伝票Tblの未確定データ取得用DataTable
        /// </summary>
        DataTable mikakuteiTable;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public void ShimeShoriCheckLogic()
        {
            LogUtility.DebugMethodStart();

            this.shimeShoriDao = DaoInitUtility.GetComponent<SeikyuShimeShoriDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 締め処理前チェック処理

        /// <summary>
        /// 締め処理前チェック処理を実行する
        /// </summary>
        /// <param name="_CheckDtoList"></param>
        /// <returns></returns>
        public DataTable checkShimeData(List<CheckDto> _CheckDtoList)
        {
            LogUtility.DebugMethodStart(_CheckDtoList);

            this.shimeShoriDao = DaoInitUtility.GetComponent<SeikyuShimeShoriDao>();

            List<CheckDto> shimeDataList = _CheckDtoList;

            #region エラーメッセージ用DataTable作成
            errorTable = new DataTable();
            errorTable.Columns.Add("SHORI_KBN", Type.GetType("System.String"));//処理区分
            errorTable.Columns.Add("CHECK_KBN", Type.GetType("System.String"));//チェック区分
            errorTable.Columns.Add("DENPYOU_SHURUI_CD", Type.GetType("System.String"));//伝票書類CD
            errorTable.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));//システムID
            errorTable.Columns.Add("SEQ", Type.GetType("System.String"));//枝番
            errorTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.String"));//明細システムID
            errorTable.Columns.Add("GYO_NUMBER", Type.GetType("System.String"));//行番号
            errorTable.Columns.Add("ERROR_NAIYOU", Type.GetType("System.String"));//エラー内容
            errorTable.Columns.Add("TORIHIKISAKI_CD", Type.GetType("System.String"));//取引先CD
            errorTable.Columns.Add("RIYUU", Type.GetType("System.String"));//理由(締め処理エラーtblの値格納用)
            errorTable.Columns.Add("KYOTEN_CD", Type.GetType("System.String"));//取引先名称
            errorTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.String"));//伝票日付
            errorTable.Columns.Add("URIAGE_DATE", Type.GetType("System.String"));
            errorTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.String"));
            errorTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.String"));//伝票番号
            errorTable.Columns.Add("DAINOU_FLG", Type.GetType("System.String"));//代納
            #endregion

            foreach (CheckDto chkDto in _CheckDtoList)
            {
                SeikyuShimeShoriDto dto = new SeikyuShimeShoriDto();
                //締めチェック用Dtoへ入れ替え
                dto.SEIKYU_CD = chkDto.TORIHIKISAKI_CD;              // 取引(請求)先コード
                dto.SHIHARAI_CD = chkDto.TORIHIKISAKI_CD;            // 取引(支払)先コード
                dto.KYOTEN_CD = chkDto.KYOTEN_CD;                    // 拠点コード
                dto.DENPYO_SHURUI = chkDto.DENPYOU_SHURUI;           // (表示条件)伝票種類
                dto.SEIKYUSHIMEBI_FROM = chkDto.KIKAN_FROM;          // 期間FROM(請求)
                dto.SEIKYUSHIMEBI_TO = chkDto.KIKAN_TO;              // 期間TO(請求)
                dto.SHIHARAISHIMEBI_FROM = chkDto.KIKAN_FROM;        // 期間FROM(支払)
                dto.SHIHARAISHIMEBI_TO = chkDto.KIKAN_TO;              // 期間TO(支払)
                dto.SHIYOU_GAMEN = chkDto.SHIYOU_GAMEN;              // 使用画面
                dto.SHIME_TANI = chkDto.SHIME_TANI;                  // 締め単位
                dto.URIAGE_SHIHARAI_KBN = chkDto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                dto.DENPYO_TYPE = chkDto.DENPYOU_TYPE;               // 伝票種類
                dto.DENPYOU_BANGOU = chkDto.DENPYOU_NUMBER;          // 伝票番号
                dto.GYO_NUMBER = chkDto.ROW_NO;                      // 明細番号
                dto.SAISHIME_FLG = chkDto.SAISHIME_FLG;              // 再締フラグ
                dto.SAISHIME_NUMBER_LIST = chkDto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //売上or支払
                if (1 == dto.URIAGE_SHIHARAI_KBN)
                {
                    //売上処理
                    //使用画面
                    if (1 == dto.SHIYOU_GAMEN)
                    {
                        //締め処理画面
                        if (1 == dto.SHIME_TANI)
                        {
                            //期間締め
                            //errorTable = CheckKikanshime_ShieShori(dto);
                            CheckKikanshime_ShieShori(dto);
                        }
                        else if (2 == dto.SHIME_TANI)
                        {
                            //伝票締め
                            CheckDenpyoushime_ShieShori(dto);
                        }
                    }
                    else
                    {
                        //締めチェック画面
                        if (1 == dto.SHIME_TANI)
                        {
                            //期間締め
                            //errorTable = CheckKikanshime_ShieCheck(dto);
                            CheckKikanshime_ShieCheck(dto);
                        }
                        else if (2 == dto.SHIME_TANI)
                        {
                            //伝票締め
                            CheckDenpyoushime_ShieCheck(dto);
                        }
                    }
                }
                else
                {
                    //支払処理
                    //使用画面
                    if (1 == dto.SHIYOU_GAMEN)
                    {
                        //締め処理画面
                        if (1 == dto.SHIME_TANI)
                        {
                            //期間締め
                            //errorTable = CheckKikanshime_ShieShori(dto);
                            CheckKikanshime_ShieShori(dto);
                        }
                        else if (2 == dto.SHIME_TANI)
                        {
                            //伝票締め
                            CheckDenpyoushime_ShieShori(dto);
                        }
                    }
                    else
                    {
                        //締めチェック画面
                        if (1 == dto.SHIME_TANI)
                        {
                            //期間締め
                            //errorTable = CheckKikanshime_ShieCheck(dto);
                            CheckKikanshime_ShieCheck(dto);
                        }
                        else if (2 == dto.SHIME_TANI)
                        {
                            //伝票締め
                            CheckDenpyoushime_ShieCheck(dto);
                        }
                    }
                }

            }//foreach End
            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        //================================//
        // ■締め処理画面からのチェック■ //
        //================================//

        #region 【締処理画面】期間締めチェック処理

        /// <summary>
        /// 【締処理画面】期間締めチェック処理を実行する
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <returns></returns>
        DataTable CheckKikanshime_ShieShori(SeikyuShimeShoriDto _dto)
        {
            LogUtility.DebugMethodStart(_dto);

            //エラーテーブルを作成
            CreateErrorDataTable();

            if (1 == _dto.URIAGE_SHIHARAI_KBN)
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 全テーブルチェック(すべて)

                    //受入伝票チェック
                    CheckKikanshime_ShieShori_Ukeire_Seikyuu(_dto);

                    //出荷伝票チェック
                    CheckKikanshime_ShieShori_Shukka_Seikyuu(_dto);

                    //売上支払伝票チェック
                    CheckKikanshime_ShieShori_UrSh_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieShori_Nyuukin(_dto);

                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入テーブルチェック

                    //受入伝票チェック
                    CheckKikanshime_ShieShori_Ukeire_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieShori_Nyuukin(_dto);

                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷テーブルチェック

                    //出荷伝票チェック
                    CheckKikanshime_ShieShori_Shukka_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieShori_Nyuukin(_dto);

                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払テーブルチェック

                    //売上支払伝票チェック
                    CheckKikanshime_ShieShori_UrSh_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieShori_Nyuukin(_dto);

                    #endregion
                }
            }
            else
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 全テーブルチェック(すべて)

                    //受入伝票チェック
                    CheckKikanshime_ShieShori_Ukeire_Seisan(_dto);

                    //出荷伝票チェック
                    CheckKikanshime_ShieShori_Shukka_Seisan(_dto);

                    //売上支払伝票チェック
                    CheckKikanshime_ShieShori_UrSh_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieShori_Shukkin(_dto);

                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入テーブルチェック

                    //受入伝票チェック
                    CheckKikanshime_ShieShori_Ukeire_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieShori_Shukkin(_dto);

                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷テーブルチェック

                    //出荷伝票チェック
                    CheckKikanshime_ShieShori_Shukka_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieShori_Shukkin(_dto);

                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払テーブルチェック

                    //売上支払伝票チェック
                    CheckKikanshime_ShieShori_UrSh_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieShori_Shukkin(_dto);

                    #endregion
                }
            }

            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        #region 【締処理画面】伝票締めチェック処理

        /// <summary>
        /// 【締処理画面】伝票締めチェック処理を実行する
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <returns></returns>
        DataTable CheckDenpyoushime_ShieShori(SeikyuShimeShoriDto _dto)
        {
            LogUtility.DebugMethodStart(_dto);

            //エラーテーブルを作成
            CreateErrorDataTable();

            if (1 == _dto.URIAGE_SHIHARAI_KBN)
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 受入伝票チェック
                    if (_dto.DENPYO_TYPE == 1)
                    {
                        //受入伝票チェック
                        CheckDenpyoushime_ShieShori_Ukeire_Seikyuu(_dto);
                    }
                    #endregion

                    #region 出荷伝票チェック
                    if (_dto.DENPYO_TYPE == 2)
                    {
                        //出荷伝票チェック
                        CheckDenpyoushime_ShieShori_Shukka_Seikyuu(_dto);
                    }
                    #endregion

                    #region 売上/支払伝票チェック
                    if (_dto.DENPYO_TYPE == 3)
                    {
                        //売上支払伝票チェック
                        CheckDenpyoushime_ShieShori_UrSh_Seikyuu(_dto);
                    }
                    #endregion

                    #region 入金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_NYUUKIN)
                    {
                        CheckDenpyoushime_ShieShori_Nyuukin(_dto);
                    }
                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入伝票チェック
                    if (_dto.DENPYO_TYPE == 1)
                    {
                        //受入伝票チェック
                        CheckDenpyoushime_ShieShori_Ukeire_Seikyuu(_dto);
                    }
                    #endregion

                    #region 入金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_NYUUKIN)
                    {
                        CheckDenpyoushime_ShieShori_Nyuukin(_dto);
                    }
                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷伝票チェック
                    if (_dto.DENPYO_TYPE == 2)
                    {
                        //出荷伝票チェック
                        CheckDenpyoushime_ShieShori_Shukka_Seikyuu(_dto);
                    }
                    #endregion

                    #region 入金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_NYUUKIN)
                    {
                        CheckDenpyoushime_ShieShori_Nyuukin(_dto);
                    }
                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払伝票チェック
                    if (_dto.DENPYO_TYPE == 3)
                    {
                        //売上支払伝票チェック
                        CheckDenpyoushime_ShieShori_UrSh_Seikyuu(_dto);
                    }
                    #endregion

                    #region 入金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_NYUUKIN)
                    {
                        CheckDenpyoushime_ShieShori_Nyuukin(_dto);
                    }
                    #endregion
                }
            }
            else
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 受入伝票チェック
                    if (_dto.DENPYO_TYPE == 1)
                    {
                        //受入伝票チェック
                        CheckDenpyoushime_ShieShori_Ukeire_Seisan(_dto);
                    }
                    #endregion

                    #region 出荷伝票チェック
                    if (_dto.DENPYO_TYPE == 2)
                    {
                        //出荷伝票チェック
                        CheckDenpyoushime_ShieShori_Shukka_Seisan(_dto);
                    }
                    #endregion

                    #region 売上/支払伝票チェック
                    if (_dto.DENPYO_TYPE == 3)
                    {
                        //売上支払伝票チェック
                        CheckDenpyoushime_ShieShori_UrSh_Seisan(_dto);
                    }
                    #endregion

                    #region 出金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_SHUKKINN)
                    {
                        CheckDenpyoushime_ShieShori_Shukkin(_dto);
                    }
                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入伝票チェック
                    if (_dto.DENPYO_TYPE == 1)
                    {
                        //受入伝票チェック
                        CheckDenpyoushime_ShieShori_Ukeire_Seisan(_dto);
                    }
                    #endregion

                    #region 出金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_SHUKKINN)
                    {
                        CheckDenpyoushime_ShieShori_Shukkin(_dto);
                    }
                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷伝票チェック
                    if (_dto.DENPYO_TYPE == 2)
                    {
                        //出荷伝票チェック
                        CheckDenpyoushime_ShieShori_Shukka_Seisan(_dto);
                    }
                    #endregion

                    #region 出金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_SHUKKINN)
                    {
                        CheckDenpyoushime_ShieShori_Shukkin(_dto);
                    }
                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払伝票チェック
                    if (_dto.DENPYO_TYPE == 3)
                    {
                        //売上支払伝票チェック
                        CheckDenpyoushime_ShieShori_UrSh_Seisan(_dto);
                    }
                    #endregion

                    #region 出金チェック
                    if (_dto.DENPYO_TYPE == Const.CommonConst.DENSHU_KBN_SHUKKINN)
                    {
                        CheckDenpyoushime_ShieShori_Shukkin(_dto);
                    }
                    #endregion
                }
            }

            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        //====================================//
        // ■締めチェック画面からのチェック■ //
        //====================================//

        #region 【締チェック画面】期間締めチェック処理

        /// <summary>
        /// 【締チェック画面】期間締めチェック処理を実行する
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <returns></returns>
        DataTable CheckKikanshime_ShieCheck(SeikyuShimeShoriDto _dto)
        {
            LogUtility.DebugMethodStart(_dto);

            //エラーテーブルを作成
            CreateErrorDataTable();

            if (1 == _dto.URIAGE_SHIHARAI_KBN)
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 全テーブルチェック(すべて)

                    //受入伝票チェック
                    CheckKikanshime_ShieCheck_Ukeire_Seikyuu(_dto);

                    //出荷伝票チェック
                    CheckKikanshime_ShieCheck_Shukka_Seikyuu(_dto);

                    //売上支払伝票チェック
                    CheckKikanshime_ShieCheck_UrSh_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入テーブルチェック

                    //受入伝票チェック
                    CheckKikanshime_ShieCheck_Ukeire_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷テーブルチェック

                    //出荷伝票チェック
                    CheckKikanshime_ShieCheck_Shukka_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払テーブルチェック

                    //売上支払伝票チェック
                    CheckKikanshime_ShieCheck_UrSh_Seikyuu(_dto);

                    //入金チェック
                    CheckKikanshime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
            }
            else
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 全テーブルチェック(すべて)

                    //受入伝票チェック
                    CheckKikanshime_ShieCheck_Ukeire_Seisan(_dto);

                    //出荷伝票チェック
                    CheckKikanshime_ShieCheck_Shukka_Seisan(_dto);

                    //売上支払伝票チェック
                    CheckKikanshime_ShieCheck_UrSh_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入テーブルチェック

                    //受入伝票チェック
                    CheckKikanshime_ShieCheck_Ukeire_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷テーブルチェック

                    //出荷伝票チェック
                    CheckKikanshime_ShieCheck_Shukka_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払テーブルチェック

                    //売上支払伝票チェック
                    CheckKikanshime_ShieCheck_UrSh_Seisan(_dto);

                    //出金チェック
                    CheckKikanshime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
            }

            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        #region 【締チェック画面】伝票締めチェック処理

        /// <summary>
        /// 【締チェック画面】伝票締めチェック処理を実行する
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <returns></returns>
        DataTable CheckDenpyoushime_ShieCheck(SeikyuShimeShoriDto _dto)
        {
            LogUtility.DebugMethodStart(_dto);

            //エラーテーブルを作成
            CreateErrorDataTable();

            if (1 == _dto.URIAGE_SHIHARAI_KBN)
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 全テーブルチェック(すべて)

                    //受入伝票チェック
                    CheckDenpyoushime_ShieCheck_Ukeire_Seikyuu(_dto);

                    //出荷伝票チェック
                    CheckDenpyoushime_ShieCheck_Shukka_Seikyuu(_dto);

                    //売上支払伝票チェック
                    CheckDenpyoushime_ShieCheck_UrSh_Seikyuu(_dto);

                    //入金チェック
                    CheckDenpyoushime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入テーブルチェック

                    //受入伝票チェック
                    CheckDenpyoushime_ShieCheck_Ukeire_Seikyuu(_dto);

                    //入金チェック
                    CheckDenpyoushime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷テーブルチェック

                    //出荷伝票チェック
                    CheckDenpyoushime_ShieCheck_Shukka_Seikyuu(_dto);

                    //入金チェック
                    CheckDenpyoushime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払テーブルチェック

                    //売上支払伝票チェック
                    CheckDenpyoushime_ShieCheck_UrSh_Seikyuu(_dto);

                    //入金チェック
                    CheckDenpyoushime_ShieCheck_Nyuukin(_dto);

                    #endregion
                }
            }
            else
            {
                if (1 == _dto.DENPYO_SHURUI)
                {
                    #region 全テーブルチェック(すべて)

                    //受入伝票チェック
                    CheckDenpyoushime_ShieCheck_Ukeire_Seisan(_dto);

                    //出荷伝票チェック
                    CheckDenpyoushime_ShieCheck_Shukka_Seisan(_dto);

                    //売上支払伝票チェック
                    CheckDenpyoushime_ShieCheck_UrSh_Seisan(_dto);

                    //出金チェック
                    CheckDenpyoushime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
                else if (2 == _dto.DENPYO_SHURUI)
                {
                    #region 受入テーブルチェック

                    //受入伝票チェック
                    CheckDenpyoushime_ShieCheck_Ukeire_Seisan(_dto);

                    //出金チェック
                    CheckDenpyoushime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
                else if (3 == _dto.DENPYO_SHURUI)
                {
                    #region 出荷テーブルチェック

                    //出荷伝票チェック
                    CheckDenpyoushime_ShieCheck_Shukka_Seisan(_dto);

                    //出金チェック
                    CheckDenpyoushime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
                else if (4 == _dto.DENPYO_SHURUI)
                {
                    #region 売上/支払テーブルチェック

                    //売上支払伝票チェック
                    CheckDenpyoushime_ShieCheck_UrSh_Seisan(_dto);

                    //出金チェック
                    CheckDenpyoushime_ShieCheck_Shukkin(_dto);

                    #endregion
                }
            }

            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        //==============================//
        // ■エラーメッセージ作成処理■ //
        //==============================//

        #region 締処理前チェックでエラーとなった対象データのエラーメッセージ作成

        /// <summary>
        /// エラーメッセージ作成処理
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <param name="_checkKbn">実施しているチェックを表す数値(1:締漏れ売上データ/2:金額ゼロ円データ/3:未確定データ/10:締漏れ入金データ/11:金額ゼロ円データ)</param>
        /// <returns></returns>
        DataRow CreateErrorMessage(SeikyuShimeShoriDto _dto, int _checkKbn)
        {
            LogUtility.DebugMethodStart(_dto, _checkKbn);

            DataRow newRow = errorTable.NewRow();

            //エラーメッセージ作成

            newRow["DAINOU_FLG"] = "False";

            #region チェック対象テーブル名設定
            string denpyouName = string.Empty;
            if (1 == _dto.DENPYO_SHURUI_CD)
            {
                denpyouName = "受入";
            }
            else if (2 == _dto.DENPYO_SHURUI_CD)
            {
                denpyouName = "出荷";
            }
            else if (3 == _dto.DENPYO_SHURUI_CD)
            {
                /* 締め処理画面に合わせる */
                denpyouName = "売上/支払";

                //if (_dto.URIAGE_SHIHARAI_KBN == 1)
                //{
                //    denpyouName = "売上";
                //}
                //else
                //{
                //    denpyouName = "支払";
                //}
                if (_dto.DAINOU_FLG != null)
                {
                    if (_dto.DAINOU_FLG)
                    {
                        denpyouName = "代納";
                        newRow["DAINOU_FLG"] = "True";
                    }
                }
            }
            else if (10 == _dto.DENPYO_SHURUI_CD)
            {
                denpyouName = "入金";
            }
            else if (20 == _dto.DENPYO_SHURUI_CD)
            {
                denpyouName = "出金";
            }
            #endregion

            #region 実施チェック名称設定
            string errorTittle = string.Empty;
            if (1 == _checkKbn)
            {
                if (_dto.URIAGE_SHIHARAI_KBN == 1)
                {
                    //締漏れ売上データ
                    errorTittle = "締漏れ売上データ";
                }
                else
                {
                    //締漏れ支払データ
                    errorTittle = "締漏れ支払データ";
                }

                //チェック区分の設定
                newRow["CHECK_KBN"] = "1";
            }
            else if (2 == _checkKbn)
            {
                //金額ゼロ円データ
                errorTittle = "金額ゼロ円データ";

                //チェック区分の設定
                newRow["CHECK_KBN"] = "2";
            }
            else if (3 == _checkKbn)
            {
                //未確定データ
                errorTittle = "未確定データ";

                //チェック区分の設定
                newRow["CHECK_KBN"] = "3";
            }
            else if (10 == _checkKbn)
            {
                //締漏れ入金データ
                errorTittle = "締漏れ入金データ";

                //チェック区分の設定
                newRow["CHECK_KBN"] = "1";
            }
            else if (20 == _checkKbn)
            {
                //締漏れ出金データ
                errorTittle = "締漏れ出金データ";

                //チェック区分の設定
                newRow["CHECK_KBN"] = "1";
            }
            else if (31 == _checkKbn)
            {
                //品名外税
                errorTittle = "明細毎外税データ";
                //チェック区分の設定
                newRow["CHECK_KBN"] = "31";
            }
            else if (32 == _checkKbn)
            {
                //伝票毎税
                errorTittle = "伝票毎税データ";
                //チェック区分の設定
                newRow["CHECK_KBN"] = "32";
            }
            else if (33 == _checkKbn)
            {
                //明細毎外税
                errorTittle = "明細毎外税データ";
                //チェック区分の設定
                newRow["CHECK_KBN"] = "33";
            }
            #endregion
            
            if (_dto.URIAGE_SHIHARAI_KBN == 1)
            {
                newRow["SHORI_KBN"] = "1";
            }
            else
            {
                newRow["SHORI_KBN"] = "2";
            }
            
            newRow["DENPYOU_SHURUI_CD"] = _dto.DENPYO_SHURUI_CD.ToString();
            newRow["SYSTEM_ID"] = _dto.SYSTEM_ID.ToString();
            newRow["SEQ"] = _dto.SEQ.ToString();

            if (string.IsNullOrEmpty(_dto.DETAIL_SYSTEM_ID.ToString()) || "0".Equals(_dto.DETAIL_SYSTEM_ID.ToString()))
            {
                newRow["DETAIL_SYSTEM_ID"] = null;
            }
            else
            {
                newRow["DETAIL_SYSTEM_ID"] = _dto.DETAIL_SYSTEM_ID.ToString();
            }

            if (string.IsNullOrEmpty(_dto.GYO_NUMBER.ToString()) || "0".Equals(_dto.GYO_NUMBER.ToString()))
            {
                newRow["GYO_NUMBER"] = null;
            }
            else
            {
                newRow["GYO_NUMBER"] = _dto.GYO_NUMBER.ToString();
            }
            DateTime dt;
            string strdate = string.Empty;
            if (DateTime.TryParse(_dto.DENPYOU_DATE.ToString(), out dt))
            {
                newRow["DENPYOU_DATE"] = dt.ToString("yyyy/MM/dd");
                strdate = dt.ToString("yyyy/MM/dd");
            }
            if (DateTime.TryParse(_dto.URIAGE_DATE.ToString(), out dt))
            {
                newRow["URIAGE_DATE"] = dt.ToString("yyyy/MM/dd");
            }
            if (DateTime.TryParse(_dto.SHIHARAI_DATE.ToString(), out dt))
            {
                newRow["SHIHARAI_DATE"] = dt.ToString("yyyy/MM/dd");
            }

            if (_dto.SHIYOU_GAMEN == 1)
            {
                newRow["ERROR_NAIYOU"] = errorTittle + "　伝票種類：" + denpyouName + "伝票　" + denpyouName + "番号：" + _dto.DENPYOU_BANGOU.ToString() + "　伝票日付：" + strdate;
            }
            else
            {
                newRow["ERROR_NAIYOU"] = errorTittle + "　伝票種類：" + denpyouName + "　" + denpyouName + "番号：" + _dto.DENPYOU_BANGOU.ToString() + "　伝票日付：" + strdate;
            }

            if (_dto.URIAGE_SHIHARAI_KBN == 1)
            {
                newRow["TORIHIKISAKI_CD"] = _dto.SEIKYU_CD;
            }
            else
            {
                newRow["TORIHIKISAKI_CD"] = _dto.SHIHARAI_CD;
            }

            newRow["KYOTEN_CD"] = _dto.KYOTEN_CD.ToString();

            newRow["DENPYOU_NUMBER"] = _dto.DENPYOU_BANGOU.ToString();

            LogUtility.DebugMethodEnd(newRow);
            return newRow;
        }
        #endregion

        #region 請求/支払締処理が行われる直近の日付をパラメターを元に取得する
        // 2014/11/14 KOEC_HOU 追加 Start
        /// <summary>
        /// エラーメッセージ作成処理
        /// </summary>
        /// <param name="mlist">mlist</param>
        /// <param name="denpyouKbnCd">伝票区分(1:請求データ　2:精算データ)</param>
        /// <returns></returns>
        public List<ReturnDate> GetNearShimeDate(List<CheckDate> mlist, int denpyouKbnCd)
        {
            TSKDClass tskdDao;
            TSSDClass tssdDao;

            tskdDao = DaoInitUtility.GetComponent<TSKDClass>();
            tssdDao = DaoInitUtility.GetComponent<TSSDClass>();

            List<ReturnDate> returnDate = new List<ReturnDate>();
            ReturnDate rd = new ReturnDate();

            foreach (CheckDate checkDate in mlist)
            {
                try
                {
                    LogUtility.DebugMethodStart(checkDate.CHECK_DATE, checkDate.KYOTEN_CD, checkDate.TORIHIKISAKI_CD);

                    if (denpyouKbnCd == 1)
                    {
                        string strSeikyuuDate = tskdDao.GetSeikyuuData(checkDate.CHECK_DATE, checkDate.KYOTEN_CD, checkDate.TORIHIKISAKI_CD);
                        if (!string.IsNullOrEmpty(strSeikyuuDate))
                        {
                            rd.TORIHIKISAKI_CD = checkDate.TORIHIKISAKI_CD;
                            rd.dtDATE = Convert.ToDateTime(strSeikyuuDate);
                            returnDate.Add(rd);
                        }
                    }
                    else
                    {
                        string strSeisanDate = tssdDao.GetSeisanData(checkDate.CHECK_DATE, checkDate.KYOTEN_CD, checkDate.TORIHIKISAKI_CD);
                        if (!string.IsNullOrEmpty(strSeisanDate))
                        {
                            rd.TORIHIKISAKI_CD = checkDate.TORIHIKISAKI_CD;
                            rd.dtDATE = Convert.ToDateTime(strSeisanDate);
                            returnDate.Add(rd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 例外エラー
                    rd.TORIHIKISAKI_CD = checkDate.TORIHIKISAKI_CD;
                    rd.dtDATE = SqlDateTime.MinValue.Value;
                    returnDate.Add(rd);

                    LogUtility.Error(ex);
                }
                finally
                {
                    LogUtility.DebugMethodEnd(checkDate.CHECK_DATE, checkDate.KYOTEN_CD, checkDate.TORIHIKISAKI_CD);
                }
            }

            return returnDate;
        }
        // 2014/11/14 KOEC_HOU 追加 end
        #endregion

        //VAN 20210502 #148577, #148578 繰り返しロジックが多いため、共通的な機能を定義 S
        #region 【締処理画面】伝票エラーチェック
        /// <summary>
        /// 伝票締処理ー入金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_Nyuukin(SeikyuShimeShoriDto _dto)
        {
            #region 入金漏れチェック[入金：10]
            //入金漏れチェックの為、伝票種類CDを[入金：10]に設定
            _dto.DENPYO_SHURUI_CD = 10;
            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))//160013
            {
                //入金伝票/明細テーブルから対象データ取得
                nyuukinTable = shimeShoriDao.SelectNyuukinCheckDataForEntity(_dto);
                //取得レコード件数
                int nyucnt = nyuukinTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != nyuukinTable.Rows.Count)
                {
                    foreach (DataRow row in nyuukinTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[入金：10]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 10);

                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region 入金ゼロ円伝票チェック：入金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckNyuukinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締処理ー出金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_Shukkin(SeikyuShimeShoriDto _dto)
        {
            #region 出金漏れチェック[出金：20]
            //出金漏れチェックの為、伝票種類CDを[入金：20]に設定
            _dto.DENPYO_SHURUI_CD = 20;
            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))//160013
            {
                //出金伝票/明細テーブルから対象データ取得
                nyuukinTable = shimeShoriDao.SelectShukkinCheckDataForEntity(_dto);
                //取得レコード件数
                int nyucnt = nyuukinTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != nyuukinTable.Rows.Count)
                {
                    foreach (DataRow row in nyuukinTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分

                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[出金：20]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 20);

                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region 出金ゼロ円伝票チェック：出金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckShukkinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出金：20]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締処理ー受入チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_Ukeire_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))//160013
            {
                //受入伝票/明細テーブルから対象データ取得
                ukeireTable = shimeShoriDao.SelectUkeireCheckDataForEntity(_dto);
                //取得レコード件数
                int ukecnt = ukeireTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != ukeireTable.Rows.Count)
                {
                    foreach (DataRow row in ukeireTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                        //============================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        //伝票種類CDを[受入：1]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //チェック対象の拠点CDを設定
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //============================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締処理ー受入チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_Ukeire_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))//160013
            {
                //受入伝票/明細テーブルから対象データ取得
                ukeireTable = shimeShoriDao.SelectUkeireCheckDataForEntity(_dto);
                //取得レコード件数
                int ukecnt = ukeireTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != ukeireTable.Rows.Count)
                {
                    foreach (DataRow row in ukeireTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分

                        //============================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        //伝票種類CDを[受入：1]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //チェック対象の拠点CDを設定
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                //============================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        
        /// <summary>
        /// 伝票締処理ー出荷チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_Shukka_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;
            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))//160013
            {
                //出荷伝票/明細テーブルから対象データ取得
                shukkaTable = shimeShoriDao.SelectShukkaCheckDataForEntity(_dto);
                //取得レコード件数
                int shkcnt = shukkaTable.Rows.Count;
                //取得件数!=0の場合のみ処理続行
                if (0 != shukkaTable.Rows.Count)
                {
                    foreach (DataRow row in shukkaTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[出荷：2]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締処理ー出荷チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_Shukka_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;
            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))//160013
            {
                //出荷伝票/明細テーブルから対象データ取得
                shukkaTable = shimeShoriDao.SelectShukkaCheckDataForEntity(_dto);
                //取得レコード件数
                int shkcnt = shukkaTable.Rows.Count;
                //取得件数!=0の場合のみ処理続行
                if (0 != shukkaTable.Rows.Count)
                {
                    foreach (DataRow row in shukkaTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分

                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[出荷：2]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        
        /// <summary>
        /// 伝票締処理ー売上支払チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_UrSh_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;
            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))//160013
            {
                //売上伝票/明細テーブルから対象データ取得
                uriageTable = shimeShoriDao.SelectUriageCheckDataForEntity(_dto);
                //取得レコード件数
                int uscnt = uriageTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != uriageTable.Rows.Count)
                {
                    foreach (DataRow row in uriageTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[売上：3]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締処理ー売上支払チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieShori_UrSh_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;
            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))//160013
            {
                //売上伝票/明細テーブルから対象データ取得
                uriageTable = shimeShoriDao.SelectUriageCheckDataForEntity(_dto);
                //取得レコード件数
                int uscnt = uriageTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != uriageTable.Rows.Count)
                {
                    foreach (DataRow row in uriageTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分

                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[売上：3]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        #endregion

        #region 【締処理画面】期間エラーチェック
        /// <summary>
        /// 期間締処理ー入金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Nyuukin(SeikyuShimeShoriDto _dto)
        {
            #region 入金漏れチェック[入金：10]
            //入金漏れチェックの為、伝票種類CDを[入金：10]に設定
            _dto.DENPYO_SHURUI_CD = 10;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))
            {
                //入金伝票/明細テーブルから対象データ取得
                nyuukinTable = shimeShoriDao.SelectNyuukinCheckDataForEntity(_dto);
                //取得レコード件数
                int nyucnt = nyuukinTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != nyuukinTable.Rows.Count)
                {
                    foreach (DataRow row in nyuukinTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[入金：10]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 10);

                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region 入金ゼロ円伝票チェック：入金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckNyuukinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[入金：10]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締処理ー出金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Shukkin(SeikyuShimeShoriDto _dto)
        {
            #region 出金漏れチェック[出金：20]
            //出金漏れチェックの為、伝票種類CDを[出金：20]に設定
            _dto.DENPYO_SHURUI_CD = 20;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))
            {
                //出金伝票/明細テーブルから対象データ取得
                nyuukinTable = shimeShoriDao.SelectShukkinCheckDataForEntity(_dto);
                //取得レコード件数
                int nyucnt = nyuukinTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != nyuukinTable.Rows.Count)
                {
                    foreach (DataRow row in nyuukinTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[出金：20]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 20);

                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region 出金ゼロ円伝票チェック：出金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckShukkinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出金：20]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締処理ー受入チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Ukeire_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))
            {
                //受入伝票/明細テーブルから対象データ取得
                ukeireTable = shimeShoriDao.SelectUkeireCheckDataForEntity(_dto);
                //取得レコード件数
                int ukecnt = ukeireTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != ukeireTable.Rows.Count)
                {
                    foreach (DataRow row in ukeireTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //============================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        //伝票種類CDを[受入：1]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //チェック対象の拠点CDを設定
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //============================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                    resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                    resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締処理ー受入チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Ukeire_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))
            {
                //受入伝票/明細テーブルから対象データ取得
                ukeireTable = shimeShoriDao.SelectUkeireCheckDataForEntity(_dto);
                //取得レコード件数
                int ukecnt = ukeireTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != ukeireTable.Rows.Count)
                {
                    foreach (DataRow row in ukeireTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //============================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        //伝票種類CDを[受入：1]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //チェック対象の拠点CDを設定
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //============================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                    resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                    resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締処理ー出荷チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Shukka_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))
            {
                //出荷伝票/明細テーブルから対象データ取得
                shukkaTable = shimeShoriDao.SelectShukkaCheckDataForEntity(_dto);
                //取得レコード件数
                int shkcnt = shukkaTable.Rows.Count;
                //取得件数!=0の場合のみ処理続行
                if (0 != shukkaTable.Rows.Count)
                {
                    foreach (DataRow row in shukkaTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[出荷：2]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                    resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                    resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締処理ー出荷チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Shukka_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))
            {
                //出荷伝票/明細テーブルから対象データ取得
                shukkaTable = shimeShoriDao.SelectShukkaCheckDataForEntity(_dto);
                //取得レコード件数
                int shkcnt = shukkaTable.Rows.Count;
                //取得件数!=0の場合のみ処理続行
                if (0 != shukkaTable.Rows.Count)
                {
                    foreach (DataRow row in shukkaTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[出荷：2]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                    resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                    resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        
        /// <summary>
        /// 期間締処理ー売上支払チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_UrSh_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SEIKYUSHIMEBI_FROM))
            {
                //売上伝票/明細テーブルから対象データ取得
                uriageTable = shimeShoriDao.SelectUriageCheckDataForEntity(_dto);
                //取得レコード件数
                int uscnt = uriageTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != uriageTable.Rows.Count)
                {
                    foreach (DataRow row in uriageTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[売上：3]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                        //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                    resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                    resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締処理ー売上支払チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_UrSh_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //FROMの値が入力有り(締日 = 0)のみ締漏れチェック実施
            if (!string.IsNullOrEmpty(_dto.SHIHARAISHIMEBI_FROM))
            {
                //売上伝票/明細テーブルから対象データ取得
                uriageTable = shimeShoriDao.SelectUriageCheckDataForEntity(_dto);
                //取得レコード件数
                int uscnt = uriageTable.Rows.Count;

                //取得件数!=0の場合のみ処理続行
                if (0 != uriageTable.Rows.Count)
                {
                    foreach (DataRow row in uriageTable.Rows)
                    {
                        SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                      // 拠点コード
                        resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                // 伝票種類
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                  // 使用画面
                        resultDto.SHIME_TANI = _dto.SHIME_TANI;                      // 締め単位
                        resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;    // 売上・支払い区分
                        resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;                  // 再締フラグ
                        resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;  // 再締請求番号リスト
                        //=======================================================
                        resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                        resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                        resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                        resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                        resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                        resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                        if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                        {
                            resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                        }
                        if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                        {
                            resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                        }
                        resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                        //伝票種類CDを[売上：3]に設定
                        resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                        resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                        //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                        if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                        {
                            //エラーメッセージ作成
                            DataRow newRow = CreateErrorMessage(resultDto, 1);
                            errorTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                        // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;                  // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;    // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;        // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;                    // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                        // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;      // 売上・支払い区分
                    resultDto.SAISHIME_FLG = _dto.SAISHIME_FLG;              // 再締フラグ
                    resultDto.SAISHIME_NUMBER_LIST = _dto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        #endregion

        #region 【締チェック画面】伝票エラーチェック
        /// <summary>
        /// 伝票締チェックー入金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_Nyuukin(SeikyuShimeShoriDto _dto)
        {
            #region 入金漏れチェック[入金：10]
            //入金漏れチェックの為、伝票種類CDを[入金：10]に設定
            _dto.DENPYO_SHURUI_CD = 10;

            //入金伝票/明細テーブルから対象データ取得
            nyuukinTable = shimeShoriDao.Check_SelectNyuukinCheckDataForEntity(_dto);
            //取得レコード件数
            int nyucnt = nyuukinTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != nyuukinTable.Rows.Count)
            {
                foreach (DataRow row in nyuukinTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[入金：10]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 10);

                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region 入金ゼロ円伝票チェック：入金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckNyuukinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[入金：10]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー出金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_Shukkin(SeikyuShimeShoriDto _dto)
        {
            #region 出金漏れチェック[出金：20]
            //出金漏れチェックの為、伝票種類CDを[出金：20]に設定
            _dto.DENPYO_SHURUI_CD = 20;

            //出金伝票/明細テーブルから対象データ取得
            nyuukinTable = shimeShoriDao.Check_SelectShukkinCheckDataForEntity(_dto);
            //取得レコード件数
            int nyucnt = nyuukinTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != nyuukinTable.Rows.Count)
            {
                foreach (DataRow row in nyuukinTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出金：20]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 20);

                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region 出金ゼロ円伝票チェック：出金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckShukkinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出金：20]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー受入チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_Ukeire_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //受入伝票/明細テーブルから対象データ取得
            ukeireTable = shimeShoriDao.Check_SelectUkeireCheckDataForEntity(_dto);
            //取得レコード件数
            int ukecnt = ukeireTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != ukeireTable.Rows.Count)
            {
                foreach (DataRow row in ukeireTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //============================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //チェック対象の拠点CDを設定
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //============================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.Check_CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー受入チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_Ukeire_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //受入伝票/明細テーブルから対象データ取得
            ukeireTable = shimeShoriDao.Check_SelectUkeireCheckDataForEntity(_dto);
            //取得レコード件数
            int ukecnt = ukeireTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != ukeireTable.Rows.Count)
            {
                foreach (DataRow row in ukeireTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //============================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //チェック対象の拠点CDを設定
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //============================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.Check_CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー出荷チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_Shukka_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //出荷伝票/明細テーブルから対象データ取得
            shukkaTable = shimeShoriDao.Check_SelectShukkaCheckDataForEntity(_dto);
            //取得レコード件数
            int shkcnt = shukkaTable.Rows.Count;
            //取得件数!=0の場合のみ処理続行
            if (0 != shukkaTable.Rows.Count)
            {
                foreach (DataRow row in shukkaTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー出荷チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_Shukka_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //出荷伝票/明細テーブルから対象データ取得
            shukkaTable = shimeShoriDao.Check_SelectShukkaCheckDataForEntity(_dto);
            //取得レコード件数
            int shkcnt = shukkaTable.Rows.Count;
            //取得件数!=0の場合のみ処理続行
            if (0 != shukkaTable.Rows.Count)
            {
                foreach (DataRow row in shukkaTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー売上支払チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_UrSh_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //売上伝票/明細テーブルから対象データ取得
            uriageTable = shimeShoriDao.Check_SelectUriageCheckDataForEntity(_dto);
            //取得レコード件数
            int uscnt = uriageTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != uriageTable.Rows.Count)
            {
                foreach (DataRow row in uriageTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 伝票締チェックー売上支払チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckDenpyoushime_ShieCheck_UrSh_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //売上伝票/明細テーブルから対象データ取得
            uriageTable = shimeShoriDao.Check_SelectUriageCheckDataForEntity(_dto);
            //取得レコード件数
            int uscnt = uriageTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != uriageTable.Rows.Count)
            {
                foreach (DataRow row in uriageTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        #endregion

        #region 【締チェック画面】期間エラーチェック
        /// <summary>
        /// 期間締チェックー入金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_Nyuukin(SeikyuShimeShoriDto _dto)
        {
            #region 入金漏れチェック[入金：10]
            //入金漏れチェックの為、伝票種類CDを[入金：10]に設定
            _dto.DENPYO_SHURUI_CD = 10;

            //入金伝票/明細テーブルから対象データ取得
            nyuukinTable = shimeShoriDao.Check_SelectNyuukinCheckDataForEntity(_dto);
            //取得レコード件数
            int nyucnt = nyuukinTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != nyuukinTable.Rows.Count)
            {
                foreach (DataRow row in nyuukinTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[入金：10]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 10);

                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion

            #region 入金ゼロ円伝票チェック：入金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckNyuukinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[入金：10]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー出金チェック
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_Shukkin(SeikyuShimeShoriDto _dto)
        {
            #region 出金漏れチェック[出金：20]
            //出金漏れチェックの為、伝票種類CDを[出金：20]に設定
            _dto.DENPYO_SHURUI_CD = 20;

            //出金伝票/明細テーブルから対象データ取得
            nyuukinTable = shimeShoriDao.Check_SelectShukkinCheckDataForEntity(_dto);
            //取得レコード件数
            int nyucnt = nyuukinTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != nyuukinTable.Rows.Count)
            {
                foreach (DataRow row in nyuukinTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出金：20]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 20);

                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region 出金ゼロ円伝票チェック：出金入力テーブル

            nyukingakuTable = shimeShoriDao.CheckShukkinGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in nyukingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = null;
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.URIAGE_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.SHIHARAI_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出金：20]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー受入チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_Ukeire_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //受入伝票/明細テーブルから対象データ取得
            ukeireTable = shimeShoriDao.Check_SelectUkeireCheckDataForEntity(_dto);
            //取得レコード件数
            int ukecnt = ukeireTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != ukeireTable.Rows.Count)
            {
                foreach (DataRow row in ukeireTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //============================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //チェック対象の拠点CDを設定
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //============================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.Check_CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー受入チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_Ukeire_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ①受入テーブルチェック(未締)[受入：1]
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //受入伝票/明細テーブルから対象データ取得
            ukeireTable = shimeShoriDao.Check_SelectUkeireCheckDataForEntity(_dto);
            //取得レコード件数
            int ukecnt = ukeireTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != ukeireTable.Rows.Count)
            {
                foreach (DataRow row in ukeireTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //============================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //チェック対象の拠点CDを設定
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック①受入テーブル
            kingakuTable = shimeShoriDao.CheckUkeireGoukeiKingakuForEntity(_dto);
            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //============================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                //伝票種類CDを[受入：1]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }

            }
            #endregion

            #region 未確定データチェック①受入テーブル
            mikakuteiTable = shimeShoriDao.Check_CheckUkeireMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー出荷チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_Shukka_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ：②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //出荷伝票/明細テーブルから対象データ取得
            shukkaTable = shimeShoriDao.Check_SelectShukkaCheckDataForEntity(_dto);
            //取得レコード件数
            int shkcnt = shukkaTable.Rows.Count;
            //取得件数!=0の場合のみ処理続行
            if (0 != shukkaTable.Rows.Count)
            {
                foreach (DataRow row in shukkaTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー出荷チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_Shukka_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ：②出荷テーブルチェック(未締)[出荷：2]
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //出荷伝票/明細テーブルから対象データ取得
            shukkaTable = shimeShoriDao.Check_SelectShukkaCheckDataForEntity(_dto);
            //取得レコード件数
            int shkcnt = shukkaTable.Rows.Count;
            //取得件数!=0の場合のみ処理続行
            if (0 != shukkaTable.Rows.Count)
            {
                foreach (DataRow row in shukkaTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック②出荷テーブル

            kingakuTable = shimeShoriDao.CheckShukkaGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[出荷：2]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }

            #endregion

            #region 未確定データチェック②出荷テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckShukkaMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー売上支払チェックー請求
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_UrSh_Seikyuu(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //売上伝票/明細テーブルから対象データ取得
            uriageTable = shimeShoriDao.Check_SelectUriageCheckDataForEntity(_dto);
            //取得レコード件数
            int uscnt = uriageTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != uriageTable.Rows.Count)
            {
                foreach (DataRow row in uriageTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //請求明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                    resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SEIKYU_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SEIKYU_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }

        /// <summary>
        /// 期間締チェックー売上支払チェックー精算
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieCheck_UrSh_Seisan(SeikyuShimeShoriDto _dto)
        {
            #region ③売上/支払テーブルチェック(未締)[売上：3]
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //売上伝票/明細テーブルから対象データ取得
            uriageTable = shimeShoriDao.Check_SelectUriageCheckDataForEntity(_dto);
            //取得レコード件数
            int uscnt = uriageTable.Rows.Count;

            //取得件数!=0の場合のみ処理続行
            if (0 != uriageTable.Rows.Count)
            {
                foreach (DataRow row in uriageTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //=======================================================
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //精算明細テーブル検索(条件に一致しないレコードをエラー出力)
                    if (0 == shimeShoriDao.SelectSeikyuMeisaiDataForEntity(resultDto))
                    {
                        //エラーメッセージ作成
                        DataRow newRow = CreateErrorMessage(resultDto, 1);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion

            #region ゼロ円伝票チェック③売上/支払テーブル

            kingakuTable = shimeShoriDao.CheckUriageGoukeiKingakuForEntity(_dto);

            foreach (DataRow row in kingakuTable.Rows)
            {
                SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                //======================================================
                resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                {
                    resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                }
                if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                {
                    resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                }
                resultDto.GOUKEIGAKU = Decimal.Parse(row["GOUKEIGAKU"].ToString());
                //伝票種類CDを[売上：3]に設定
                resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                if (0 == resultDto.GOUKEIGAKU)
                {
                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 2);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion

            #region 未確定データチェック③売上/支払テーブル

            mikakuteiTable = shimeShoriDao.Check_CheckUriageMikakuteiDataForEntity(_dto);

            if (0 != mikakuteiTable.Rows.Count)
            {
                foreach (DataRow row in mikakuteiTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    //resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                    //resultDto.KYOTEN_CD = _dto.KYOTEN_CD;                    // 拠点コード
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                    resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    //======================================================
                    resultDto.SHIHARAI_CD = row["TORIHIKISAKI_CD"].ToString();
                    resultDto.SHIHARAI_NAME = row["TORIHIKISAKI_NAME"].ToString();
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    DataRow newRow = CreateErrorMessage(resultDto, 3);
                    errorTable.Rows.Add(newRow);
                }
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// エラーテーブルを作成
        /// </summary>
        private void CreateErrorDataTable()
        {
            #region 受入明細から対象を取得するテーブル作成
            ukeireTable = new DataTable();
            ukeireTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            ukeireTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            ukeireTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.Int64"));
            ukeireTable.Columns.Add("ROW_NO", Type.GetType("System.Int32"));
            ukeireTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            ukeireTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            ukeireTable.Columns.Add("URIAGE_DATE", Type.GetType("System.DateTime"));
            ukeireTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.DateTime"));
            ukeireTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int16"));
            #endregion

            #region 出荷明細から対象を取得するテーブル作成
            shukkaTable = new DataTable();
            shukkaTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            shukkaTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            shukkaTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.Int64"));
            shukkaTable.Columns.Add("ROW_NO", Type.GetType("System.Int32"));
            shukkaTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            shukkaTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            shukkaTable.Columns.Add("URIAGE_DATE", Type.GetType("System.DateTime"));
            shukkaTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.DateTime"));
            shukkaTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int16"));
            #endregion

            #region 売上明細から対象を取得するテーブル作成
            uriageTable = new DataTable();
            uriageTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            uriageTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            uriageTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.Int64"));
            uriageTable.Columns.Add("ROW_NO", Type.GetType("System.Int32"));
            uriageTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            uriageTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            uriageTable.Columns.Add("URIAGE_DATE", Type.GetType("System.DateTime"));
            uriageTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.DateTime"));
            uriageTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int16"));
            #endregion

            #region 金額チェックの為、各伝票テーブルから取得するテーブル作成
            kingakuTable = new DataTable();
            kingakuTable.Columns.Add("TORIHIKISAKI_CD", Type.GetType("System.String"));
            //kingakuTable.Columns.Add("TORIHIKISAKI_NAME", Type.GetType("System.String"));
            kingakuTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int32"));
            kingakuTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            kingakuTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            kingakuTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            kingakuTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            kingakuTable.Columns.Add("URIAGE_DATE", Type.GetType("System.DateTime"));
            kingakuTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.DateTime"));
            kingakuTable.Columns.Add("GOUKEIGAKU", Type.GetType("System.Decimal"));
            #endregion

            #region 未確定チェックの為、各伝票テーブルから取得するテーブル作成
            mikakuteiTable = new DataTable();
            mikakuteiTable.Columns.Add("TORIHIKISAKI_CD", Type.GetType("System.String"));
            //mikakuteiTable.Columns.Add("TORIHIKISAKI_NAME", Type.GetType("System.String"));
            mikakuteiTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int32"));
            mikakuteiTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            mikakuteiTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            mikakuteiTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            mikakuteiTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            mikakuteiTable.Columns.Add("URIAGE_DATE", Type.GetType("System.DateTime"));
            mikakuteiTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.DateTime"));
            #endregion

            #region 入金/出金明細から対象を取得するテーブル作成
            nyuukinTable = new DataTable();
            nyuukinTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            nyuukinTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            nyuukinTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.Int64"));
            nyuukinTable.Columns.Add("ROW_NO", Type.GetType("System.Int32"));
            nyuukinTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            nyuukinTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            nyuukinTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int16"));

            #endregion

            #region 金額チェックの為、入金/出金入力テーブルから金額を取得するテーブル作成
            nyukingakuTable = new DataTable();
            nyukingakuTable.Columns.Add("TORIHIKISAKI_CD", Type.GetType("System.String"));
            nyukingakuTable.Columns.Add("KYOTEN_CD", Type.GetType("System.Int32"));
            nyukingakuTable.Columns.Add("SYSTEM_ID", Type.GetType("System.Int64"));
            nyukingakuTable.Columns.Add("SEQ", Type.GetType("System.Int32"));
            nyukingakuTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            nyukingakuTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            nyukingakuTable.Columns.Add("GOUKEIGAKU", Type.GetType("System.Decimal"));
            #endregion
        }
        //VAN 20210502 #148577, #148578 繰り返しロジックが多いため、共通的な機能を定義 E

        #region 適格請求書用締め処理前チェック処理

        /// <summary>
        /// 適格請求書用締め処理前チェック処理を実行する
        /// </summary>
        /// <param name="_CheckDtoList"></param>
        /// <returns></returns>
        public DataTable checkShimeData_invoice(List<CheckDto> _CheckDtoList)
        {
            LogUtility.DebugMethodStart(_CheckDtoList);

            this.shimeShoriDao = DaoInitUtility.GetComponent<SeikyuShimeShoriDao>();

            List<CheckDto> shimeDataList = _CheckDtoList;

            #region エラーメッセージ用DataTable作成
            errorTable = new DataTable();
            errorTable.Columns.Add("SHORI_KBN", Type.GetType("System.String"));//処理区分
            errorTable.Columns.Add("CHECK_KBN", Type.GetType("System.String"));//チェック区分
            errorTable.Columns.Add("DENPYOU_SHURUI_CD", Type.GetType("System.String"));//伝票書類CD
            errorTable.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));//システムID
            errorTable.Columns.Add("SEQ", Type.GetType("System.String"));//枝番
            errorTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.String"));//明細システムID
            errorTable.Columns.Add("GYO_NUMBER", Type.GetType("System.String"));//行番号
            errorTable.Columns.Add("ERROR_NAIYOU", Type.GetType("System.String"));//エラー内容
            errorTable.Columns.Add("TORIHIKISAKI_CD", Type.GetType("System.String"));//取引先CD
            errorTable.Columns.Add("RIYUU", Type.GetType("System.String"));//理由(締め処理エラーtblの値格納用)
            errorTable.Columns.Add("KYOTEN_CD", Type.GetType("System.String"));//取引先名称
            errorTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.String"));//伝票日付
            errorTable.Columns.Add("URIAGE_DATE", Type.GetType("System.String"));
            errorTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.String"));
            errorTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.String"));//伝票番号
            errorTable.Columns.Add("DAINOU_FLG", Type.GetType("System.String"));//代納
            #endregion

            foreach (CheckDto chkDto in _CheckDtoList)
            {
                SeikyuShimeShoriDto dto = new SeikyuShimeShoriDto();
                //締めチェック用Dtoへ入れ替え
                dto.SEIKYU_CD = chkDto.TORIHIKISAKI_CD;              // 取引(請求)先コード
                dto.SHIHARAI_CD = chkDto.TORIHIKISAKI_CD;            // 取引(支払)先コード
                dto.KYOTEN_CD = chkDto.KYOTEN_CD;                    // 拠点コード
                dto.DENPYO_SHURUI = chkDto.DENPYOU_SHURUI;           // (表示条件)伝票種類
                dto.SEIKYUSHIMEBI_FROM = chkDto.KIKAN_FROM;          // 期間FROM(請求)
                dto.SEIKYUSHIMEBI_TO = chkDto.KIKAN_TO;              // 期間TO(請求)
                dto.SHIHARAISHIMEBI_FROM = chkDto.KIKAN_FROM;        // 期間FROM(支払)
                dto.SHIHARAISHIMEBI_TO = chkDto.KIKAN_TO;              // 期間TO(支払)
                dto.SHIYOU_GAMEN = chkDto.SHIYOU_GAMEN;              // 使用画面
                dto.SHIME_TANI = chkDto.SHIME_TANI;                  // 締め単位
                dto.URIAGE_SHIHARAI_KBN = chkDto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                dto.DENPYO_TYPE = chkDto.DENPYOU_TYPE;               // 伝票種類
                dto.DENPYOU_BANGOU = chkDto.DENPYOU_NUMBER;          // 伝票番号
                dto.GYO_NUMBER = chkDto.ROW_NO;                      // 明細番号
                dto.SAISHIME_FLG = chkDto.SAISHIME_FLG;              // 再締フラグ
                dto.SAISHIME_NUMBER_LIST = chkDto.SAISHIME_NUMBER_LIST;// 再締請求番号リスト
                //売上or支払
                if (1 == dto.URIAGE_SHIHARAI_KBN)
                {
                    //売上処理
                    //使用画面
                    if (1 == dto.SHIYOU_GAMEN)
                    {
                        //締め処理画面
                        if (1 == dto.SHIME_TANI)
                        {
                            //期間締め
                            CheckKikanshime_ShieShori_invoice(dto);
                        }
                        else if (2 == dto.SHIME_TANI)
                        {
                            //伝票締め
                            CheckDenpyoushime_ShieShori_invoice(dto);
                            if (errorTable.Rows.Count != 0)
                            {
                                return errorTable;
                            }

                        }
                    }
                    //else
                    //{
                    //    //締めチェック画面では、適格請求書チェックは行わない
                    //}
                }
                else
                {
                    //支払処理
                    //使用画面
                    if (1 == dto.SHIYOU_GAMEN)
                    {
                        //締め処理画面
                        if (1 == dto.SHIME_TANI)
                        {
                            CheckKikanshime_ShieShori_invoice(dto);
                        }
                        else if (2 == dto.SHIME_TANI)
                        {
                            //伝票締め
                            CheckDenpyoushime_ShieShori_invoice(dto);
                            if (errorTable.Rows.Count != 0)
                            {
                                return errorTable;
                            }
                        }
                    }
                    //else
                    //{
                    //    //締めチェック画面では、適格請求書チェックは行わない
                    //}
                }

            }//foreach End
            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        //================================//
        // ■締め処理画面からのチェック（適格請求書用）■ //
        //================================//

        #region 【締処理画面】期間締め/伝票締めチェック処理_適格請求書用

        /// <summary>
        /// 【締処理画面】期間締めチェック処理を実行する
        /// 　　　　　　　適格請求書用（売上/支払共通）
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <returns></returns>
        DataTable CheckKikanshime_ShieShori_invoice(SeikyuShimeShoriDto _dto)
        {
            LogUtility.DebugMethodStart(_dto);

            //エラーテーブルを作成
            CreateErrorDataTable();

            if (1 == _dto.DENPYO_SHURUI)
            {
                #region 全テーブルチェック(すべて)

                //受入伝票チェック
                CheckKikanshime_ShieShori_Ukeire_invoice(_dto);

                //出荷伝票チェック
                CheckKikanshime_ShieShori_Shukka_invoice(_dto);

                //売上支払伝票チェック
                CheckKikanshime_ShieShori_UrSh_invoice(_dto);

                #endregion
            }
            else if (2 == _dto.DENPYO_SHURUI)
            {
                #region 受入テーブルチェック

                //受入伝票チェック
                CheckKikanshime_ShieShori_Ukeire_invoice(_dto);

                #endregion
            }
            else if (3 == _dto.DENPYO_SHURUI)
            {
                #region 出荷テーブルチェック

                //出荷伝票チェック
                CheckKikanshime_ShieShori_Shukka_invoice(_dto);

                #endregion
            }
            else if (4 == _dto.DENPYO_SHURUI)
            {
                #region 売上/支払テーブルチェック

                //売上支払伝票チェック
                CheckKikanshime_ShieShori_UrSh_invoice(_dto);

                #endregion
            }

            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion

        /// <summary>
        /// 受入チェックー請求/精算（適格請求書）
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Ukeire_invoice(SeikyuShimeShoriDto _dto)
        {
            #region 税チェック
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //受入伝票/明細テーブルから対象データ取得
            ukeireTable = shimeShoriDao.SelectUkeireCheckTaxForEntity(_dto);
            //取得レコード件数
            int ukecnt = ukeireTable.Rows.Count;
            string tmpZeiKbn = string.Empty;
            string tmpZeiKeisanKbn = string.Empty;

            //取得件数!=0の場合のみ処理続行
            if (0 != ukeireTable.Rows.Count)
            {
                foreach (DataRow row in ukeireTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    if (1 == _dto.URIAGE_SHIHARAI_KBN)
                    {
                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        tmpZeiKbn = row["URIAGE_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    else
                    {
                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        tmpZeiKbn = row["SHIHARAI_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                    }

                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払区分
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    //伝票種類CDを[受入：1]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    //チェック対象の拠点CDを設定
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());

                    //エラーメッセージ作成
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == row["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名外税
                        DataRow newRow = CreateErrorMessage(resultDto, 31);
                        errorTable.Rows.Add(newRow);
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() == tmpZeiKeisanKbn)
                    {
                        //伝票毎税
                        DataRow newRow = CreateErrorMessage(resultDto, 32);
                        errorTable.Rows.Add(newRow);
                    }
                    else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == tmpZeiKeisanKbn) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == tmpZeiKbn) &&
                            (String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString())))
                    {
                        //明細毎外税
                        DataRow newRow = CreateErrorMessage(resultDto, 33);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 出荷チェックー請求/精算（適格請求書）
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_Shukka_invoice(SeikyuShimeShoriDto _dto)
        {
            #region 税チェック
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //出荷伝票/明細テーブルから対象データ取得
            shukkaTable = shimeShoriDao.SelectShukkaCheckTaxDataForEntity(_dto);
            //取得レコード件数
            int shkcnt = shukkaTable.Rows.Count;
            string tmpZeiKbn = string.Empty;
            string tmpZeiKeisanKbn = string.Empty;
            //取得件数!=0の場合のみ処理続行
            if (0 != shukkaTable.Rows.Count)
            {
                foreach (DataRow row in shukkaTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    if (1 == _dto.URIAGE_SHIHARAI_KBN)
                    {
                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        tmpZeiKbn = row["URIAGE_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    else
                    {
                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        tmpZeiKbn = row["SHIHARAI_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[出荷：2]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == row["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名外税
                        DataRow newRow = CreateErrorMessage(resultDto, 31);
                        errorTable.Rows.Add(newRow);
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() == tmpZeiKeisanKbn)
                    {
                        //伝票毎税
                        DataRow newRow = CreateErrorMessage(resultDto, 32);
                        errorTable.Rows.Add(newRow);
                    }
                    else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == tmpZeiKeisanKbn) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == tmpZeiKbn) &&
                            (String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString())))
                    {
                        //明細毎外税
                        DataRow newRow = CreateErrorMessage(resultDto, 33);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 売上支払チェックー請求/精算（適格請求書）
        /// </summary>
        /// <param name="_dto"></param>
        private void CheckKikanshime_ShieShori_UrSh_invoice(SeikyuShimeShoriDto _dto)
        {
            #region 税チェック
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //売上伝票/明細テーブルから対象データ取得
            uriageTable = shimeShoriDao.SelectUriageCheckTaxDataForEntity(_dto);
            //取得レコード件数
            int uscnt = uriageTable.Rows.Count;
            string tmpZeiKbn = string.Empty;
            string tmpZeiKeisanKbn = string.Empty;

            //取得件数!=0の場合のみ処理続行
            if (0 != uriageTable.Rows.Count)
            {
                foreach (DataRow row in uriageTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    if (1 == _dto.URIAGE_SHIHARAI_KBN)
                    {
                        resultDto.SEIKYU_CD = _dto.SEIKYU_CD;                    // 取引(請求)先コード
                        resultDto.SEIKYUSHIMEBI_FROM = _dto.SEIKYUSHIMEBI_FROM;  // 期間FROM
                        resultDto.SEIKYUSHIMEBI_TO = _dto.SEIKYUSHIMEBI_TO;      // 期間TO
                        tmpZeiKbn = row["URIAGE_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    else
                    {
                        resultDto.SHIHARAI_CD = _dto.SHIHARAI_CD;                    // 取引(支払)先コード
                        resultDto.SHIHARAISHIMEBI_FROM = _dto.SHIHARAISHIMEBI_FROM;  // 期間FROM
                        resultDto.SHIHARAISHIMEBI_TO = _dto.SHIHARAISHIMEBI_TO;      // 期間TO
                        tmpZeiKbn = row["SHIHARAI_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    resultDto.DENPYO_SHURUI = _dto.DENPYO_SHURUI;            // 伝票種類
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分

                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    if (row["URIAGE_DATE"] != null && !String.IsNullOrEmpty(row["URIAGE_DATE"].ToString()))
                    {
                        resultDto.URIAGE_DATE = DateTime.Parse(row["URIAGE_DATE"].ToString());
                    }
                    if (row["SHIHARAI_DATE"] != null && !String.IsNullOrEmpty(row["SHIHARAI_DATE"].ToString()))
                    {
                        resultDto.SHIHARAI_DATE = DateTime.Parse(row["SHIHARAI_DATE"].ToString());
                    }
                    resultDto.KYOTEN_CD = int.Parse(row["KYOTEN_CD"].ToString());
                    //伝票種類CDを[売上：3]に設定
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == row["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名外税
                        DataRow newRow = CreateErrorMessage(resultDto, 31);
                        errorTable.Rows.Add(newRow);
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString() == tmpZeiKeisanKbn)
                    {
                        //伝票毎税
                        DataRow newRow = CreateErrorMessage(resultDto, 32);
                        errorTable.Rows.Add(newRow);
                    }
                    else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == tmpZeiKeisanKbn) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == tmpZeiKbn) &&
                            (String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString())))
                    {
                        //明細毎外税
                        DataRow newRow = CreateErrorMessage(resultDto, 33);
                        errorTable.Rows.Add(newRow);
                    }
                }
            }
            #endregion
        }

        #region 【締処理画面】伝票締めチェック処理_適格請求書用

        /// <summary>
        /// 【締処理画面】期間締め/伝票締めチェック処理を実行する
        /// 　　　　　　　適格請求書用（売上/支払共通）
        /// </summary>
        /// <param name="_dto">_dto</param>
        /// <returns></returns>
        DataTable CheckDenpyoushime_ShieShori_invoice(SeikyuShimeShoriDto _dto)
        {
            LogUtility.DebugMethodStart(_dto);

            //エラーテーブルを作成
            CreateErrorDataTable();

            if (1 == _dto.DENPYO_SHURUI)
            {

                #region 受入伝票チェック
                if (_dto.DENPYO_TYPE == 1)
                {
                    CheckDenpyoushime_ShieShori_Ukeire_invoice(_dto);
                }
                #endregion

                #region 出荷伝票チェック
                if (_dto.DENPYO_TYPE == 2)
                {
                    CheckDenpyoushime_ShieShori_Shukka_invoice(_dto);
                }
                #endregion

                #region 売上/支払伝票チェック
                if (_dto.DENPYO_TYPE == 3)
                {
                    CheckDenpyoushime_ShieShori_UrSh_invoice(_dto);
                }
                #endregion
            }
            else if (2 == _dto.DENPYO_SHURUI)
            {
                #region 受入伝票チェック
                if (_dto.DENPYO_TYPE == 1)
                {
                    CheckDenpyoushime_ShieShori_Ukeire_invoice(_dto);
                }
                #endregion
            }
            else if (3 == _dto.DENPYO_SHURUI)
            {
                #region 出荷伝票チェック
                if (_dto.DENPYO_TYPE == 2)
                {
                    CheckDenpyoushime_ShieShori_Shukka_invoice(_dto);
                }
                #endregion
            }
            else if (4 == _dto.DENPYO_SHURUI)
            {
                #region 売上/支払伝票チェック
                if (_dto.DENPYO_TYPE == 3)
                {
                    CheckDenpyoushime_ShieShori_UrSh_invoice(_dto);
                }
                #endregion
            }

            LogUtility.DebugMethodEnd(errorTable);
            return errorTable;
        }
        #endregion
        /// <summary>
        /// 受入チェックー請求/精算（適格請求書）
        /// </summary>
        /// <param name="_dto"></param>
        private bool CheckDenpyoushime_ShieShori_Ukeire_invoice(SeikyuShimeShoriDto _dto)
        {
            #region 税チェック
            //受入チェックの為、伝票種類CDを[受入：1]に設定
            _dto.DENPYO_SHURUI_CD = 1;

            //受入伝票/明細テーブルから対象データ取得
            ukeireTable = shimeShoriDao.SelectUkeireCheckTaxForEntity(_dto);
            //取得レコード件数
            int ukecnt = ukeireTable.Rows.Count;
            string tmpZeiKbn = string.Empty;
            string tmpZeiKeisanKbn = string.Empty;
            int ErrCnt = 0;

            //取得件数!=0の場合のみ処理続行
            if (0 != ukeireTable.Rows.Count)
            {
                foreach (DataRow row in ukeireTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払区分
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());


                    if (1 == _dto.URIAGE_SHIHARAI_KBN)
                    {
                        tmpZeiKbn = row["URIAGE_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    else
                    {
                        tmpZeiKbn = row["SHIHARAI_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == row["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名外税
                        ErrCnt = 1;
                    }
                    else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == tmpZeiKeisanKbn) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == tmpZeiKbn) &&
                            (String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString())))
                    {
                        //明細毎外税
                        ErrCnt = 1;
                    }
                    if (ErrCnt == 1)
                    {
                        DataRow newRow = CreateErrorMessage(resultDto, 3);
                        newRow["ERROR_NAIYOU"] = "受入";
                        errorTable.Rows.Add(newRow);
                        return false;
                    }
                }
            }

            return true;

            #endregion
        }

        /// <summary>
        /// 出荷チェックー請求/精算（適格請求書）
        /// </summary>
        /// <param name="_dto"></param>
        private bool CheckDenpyoushime_ShieShori_Shukka_invoice(SeikyuShimeShoriDto _dto)
        {
            #region 税チェック
            //出荷チェックの為、伝票種類CDを[出荷：2]に設定
            _dto.DENPYO_SHURUI_CD = 2;

            //出荷伝票/明細テーブルから対象データ取得
            shukkaTable = shimeShoriDao.SelectShukkaCheckTaxDataForEntity(_dto);
            //取得レコード件数
            int shkcnt = shukkaTable.Rows.Count;
            string tmpZeiKbn = string.Empty;
            string tmpZeiKeisanKbn = string.Empty;
            int ErrCnt = 0;

            //取得件数!=0の場合のみ処理続行
            if (0 != shukkaTable.Rows.Count)
            {
                foreach (DataRow row in shukkaTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();

                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SHIME_TANI = _dto.SHIME_TANI;                  // 締め単位
                    resultDto.URIAGE_SHIHARAI_KBN = _dto.URIAGE_SHIHARAI_KBN;// 売上・支払い区分
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());

                    if (1 == _dto.URIAGE_SHIHARAI_KBN)
                    {
                        tmpZeiKbn = row["URIAGE_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    else
                    {
                        tmpZeiKbn = row["SHIHARAI_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;

                    //エラーメッセージ作成
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == row["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        //品名外税
                        ErrCnt = 1;
                    }
                    else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == tmpZeiKeisanKbn) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == tmpZeiKbn) &&
                            (String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString())))
                    {
                        //明細毎外税
                        ErrCnt = 1;
                    }
                    if (ErrCnt == 1)
                    {
                        DataRow newRow = CreateErrorMessage(resultDto, 3);
                        newRow["ERROR_NAIYOU"] = "出荷";
                        errorTable.Rows.Add(newRow);
                        return false;
                    }
                }
            }

            return true;
            #endregion
        }
        /// <summary>
        /// 売上支払チェックー請求/精算（適格請求書）
        /// </summary>
        /// <param name="_dto"></param>
        private bool CheckDenpyoushime_ShieShori_UrSh_invoice(SeikyuShimeShoriDto _dto)
        {
            #region 税チェック
            //売上チェックの為、伝票種類CDを[売上：3]に設定
            _dto.DENPYO_SHURUI_CD = 3;

            //売上伝票/明細テーブルから対象データ取得
            uriageTable = shimeShoriDao.SelectUriageCheckTaxDataForEntity(_dto);
            //取得レコード件数
            int uscnt = uriageTable.Rows.Count;
            string tmpZeiKbn = string.Empty;
            string tmpZeiKeisanKbn = string.Empty;
            int ErrCnt = 0;

            //取得件数!=0の場合のみ処理続行
            if (0 != uriageTable.Rows.Count)
            {
                foreach (DataRow row in uriageTable.Rows)
                {
                    SeikyuShimeShoriDto resultDto = new SeikyuShimeShoriDto();
                    resultDto.SHIYOU_GAMEN = _dto.SHIYOU_GAMEN;              // 使用画面
                    resultDto.SYSTEM_ID = long.Parse(row["SYSTEM_ID"].ToString());
                    resultDto.SEQ = int.Parse(row["SEQ"].ToString());
                    resultDto.DETAIL_SYSTEM_ID = long.Parse(row["DETAIL_SYSTEM_ID"].ToString());
                    resultDto.GYO_NUMBER = int.Parse(row["ROW_NO"].ToString());

                    if (1 == _dto.URIAGE_SHIHARAI_KBN)
                    {
                        tmpZeiKbn = row["URIAGE_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["URIAGE_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    else
                    {
                        tmpZeiKbn = row["SHIHARAI_ZEI_KBN_CD"].ToString();
                        tmpZeiKeisanKbn = row["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                    }
                    resultDto.DENPYOU_BANGOU = long.Parse(row["DENPYOU_NUMBER"].ToString());
                    resultDto.DENPYOU_DATE = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                    resultDto.DENPYO_SHURUI_CD = _dto.DENPYO_SHURUI_CD;
                    resultDto.DAINOU_FLG = bool.Parse(Convert.ToString(row["DAINOU_FLG"]));

                    //エラーメッセージ作成
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == row["HINMEI_ZEI_KBN_CD"].ToString())
                    {
                        ErrCnt = 1;

                        //品名外税
                    }
                    else if ((Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString() == tmpZeiKeisanKbn) &&
                            (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KBN_SOTO.ToString() == tmpZeiKbn) &&
                            (String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString())))
                    {
                        //明細毎外税
                        ErrCnt = 1;
                    }
                    if (ErrCnt == 1)
                    {
                        DataRow newRow = CreateErrorMessage(resultDto, 3);
                        newRow["ERROR_NAIYOU"] = "売上/支払";
                        if (resultDto.DAINOU_FLG != null)
                        {
                            if (resultDto.DAINOU_FLG)
                            {
                                newRow["ERROR_NAIYOU"] = "代納";
                            }
                        }
                        errorTable.Rows.Add(newRow);
                        return false;
                    }
                }
            }
            return true;
            #endregion
        }
    }
}