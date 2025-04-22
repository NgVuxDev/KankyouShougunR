using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using FWK = r_framework.Logic;
using System.Data;
using System;
using r_framework.Entity;
using Shougun.Core.Master.ContenaHoshu.Dao;
using r_framework.Utility;
using ContenaHoshu.Validator;

namespace Shougun.Core.Master.ContenaHoshu.Validator
{
    /// <summary>
    /// コンテナ検証ロジック
    /// </summary>
    public class ContenaHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContenaHoshuValidator()
        {
        }

        /// <summary>
        /// キー項目(コンテナCD,コンテナ種類CD)の重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <returns>
        /// 重複チェックの結果
        /// true = 重複なし
        /// false = 重複あり
        /// </returns>
        public bool contenaCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtCheck, DataTable dtAll)
        {
            LogUtility.DebugMethodStart(gcMultiRow, dt, dtCheck, dtAll);
            try
            {
                var ren = true;

                //コンテナCD
                GcCustomAlphaNumTextBoxCell cntContenaCd = gcMultiRow[gcMultiRow.CurrentRow.Index, "txtContenaCd"] as GcCustomAlphaNumTextBoxCell;
                //コンテナ種類CD
                GcCustomAlphaNumTextBoxCell cntContenaSyuruiCd = gcMultiRow[gcMultiRow.CurrentRow.Index, "txtContenaSyuruiCd"] as GcCustomAlphaNumTextBoxCell;

                if (cntContenaCd == null
                    || cntContenaCd.EditedFormattedValue == null
                    || string.IsNullOrEmpty(cntContenaCd.EditedFormattedValue.ToString())
                    || cntContenaSyuruiCd == null
                    || cntContenaSyuruiCd.EditedFormattedValue == null
                    || string.IsNullOrEmpty(cntContenaSyuruiCd.EditedFormattedValue.ToString()))
                {
                    return true;
                }

                // 重複チェック
                {
                    DataTable SearchResult;
                    M_CONTENA entity = new M_CONTENA();
                    int rowCount = 0;
                    int? currentRowIndex;
                    if (gcMultiRow.CurrentRow != null)
                    {
                        currentRowIndex = gcMultiRow.CurrentRow.Index;
                    }
                    else
                    {
                        currentRowIndex = null;
                    }

                    entity.CONTENA_CD = cntContenaCd.EditedFormattedValue.ToString().PadLeft(10, '0');
                    entity.CONTENA_SHURUI_CD = cntContenaSyuruiCd.EditedFormattedValue.ToString();

                    ContenaDao contenaDao;
                    contenaDao = DaoInitUtility.GetComponent<ContenaDao>();
                    SearchResult = contenaDao.GetContenaAllDataForEntity(entity);


                    // 画面で重複チェック
                    foreach (DataRow row in dt.Rows)
                    {
                        // カレントROW以外に同一の値があった場合
                        if (currentRowIndex != null && rowCount != currentRowIndex)
                        {
                            if (row["CONTENA_SHURUI_CD"].ToString().Equals(entity.CONTENA_SHURUI_CD.ToString()))
                            {
                                if (row["CONTENA_CD"].ToString().Equals(entity.CONTENA_CD.ToString()))
                                {
                                    ren = false;
                                    break;
                                }
                            }
                        }
                        rowCount++;
                    }

                    //非表示分（検索条件から漏れたデータ）
                    if (ren)
                    {
                        IEnumerable<DataRow> enumRow = dtCheck.AsEnumerable();
                        IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                        var rows = enumRowAll.Except(enumRow, new DataRowContenaHoshuCompare());

                        foreach (DataRow row in rows)
                        {
                            if (row["CONTENA_SHURUI_CD"].ToString().Equals(entity.CONTENA_SHURUI_CD.ToString()))
                            {
                                if (row["CONTENA_CD"].ToString().Equals(entity.CONTENA_CD.ToString()))
                                {
                                    ren = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (!ren)
                    {
                        ////同じCDがある場合、エラー
                        var messageShowLogic = new MessageBoxShowLogic();
                        MessageBox.Show("入力されたコンテナ種類CD、コンテナCDはすでに登録されています。", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                LogUtility.DebugMethodEnd(true);
                return ren;

            }
            catch(Exception ex)
            {
                LogUtility.Error("contenaCDValidator", ex);
                throw;
            }

        }
    }
}
