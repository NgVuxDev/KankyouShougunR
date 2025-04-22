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
using r_framework.CustomControl;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class ItakuKeiyakuCheckLogic
    {
        #region フィールド

        /// <summary>
        /// 委託契約基本情報Dao
        /// </summary>
        private ItakuKeiyakuDAO kihonDao;

        /// <summary>
        /// 品名マスタDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// 現場マスタDao
        /// </summary>
        private IM_GENBADao genbaDao;

        public static readonly string ID_MSG_CFM_ITAKU_NOT_FOUND = "C111";
        public static readonly string ID_MSG_CFM_ITAKU_YUUKOU_KIKAN = "C112";
        public static readonly string ID_MSG_CFM_ITAKU_GENBA_BLANK = "C113";
        public static readonly string ID_MSG_CFM_ITAKU_GENBA_NOT_FOUND = "C114";
        public static readonly string ID_MSG_CFM_ITAKU_HOUKOKUSHO_BUNRUI = "C115";

        public static readonly string ID_MSG_ERR_ITAKU_NOT_FOUND = "E306";
        public static readonly string ID_MSG_ERR_ITAKU_YUUKOU_KIKAN = "E307";
        public static readonly string ID_MSG_ERR_ITAKU_GENBA_BLANK = "E308";
        public static readonly string ID_MSG_ERR_ITAKU_GENBA_NOT_FOUND = "E309";
        public static readonly string ID_MSG_ERR_ITAKU_HOUKOKUSHO_BUNRUI = "E310";

        public static string ERROR_APPEND_DETAIL = "（{0}CD:{1}({2})）";
        public static string ERROR_APPEND_SUM_RECORD = "（計{0}件）";

        //最大表示エラー数
        public static int MAX_ERROR_SHOW = 5;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItakuKeiyakuCheckLogic()
        {
            LogUtility.DebugMethodStart();

            this.kihonDao = DaoInitUtility.GetComponent<ItakuKeiyakuDAO>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 委託契約チェック
        /// <summary>
        /// 委託契約チェック
        /// </summary>
        /// <param name="checkDto"></param>
        /// <returns></returns>
        public ItakuErrorDTO ItakuKeiyakuCheck(ItakuCheckDTO checkDto)
        {
            //エラーDto
            ItakuErrorDTO error = new ItakuErrorDTO();
            error.ERROR_KBN = (short)ITAKU_ERROR_KBN.NONE;
            error.DETAIL_ERROR = new List<DetailDTO>();

            //委託契約取得Dto
            ItakuKeiyakuDTO searchDto = new ItakuKeiyakuDTO();

            // 排出事業者チェック (GYOUSHA_CD)
            searchDto.GYOUSHA_CD = checkDto.GYOUSHA_CD;

            DataTable dataItaku = kihonDao.GetItakuKeiyaku(searchDto);
            if (dataItaku == null || dataItaku.Rows.Count == 0)
            {
                error.ERROR_KBN = (short)ITAKU_ERROR_KBN.GYOUSHA;
                return error;
            }

            //排出事業場チェック
            //画面の現場CD　≠　BLANK (GYOUSHA_CD + GENBA_CD)
            if (!String.IsNullOrEmpty(checkDto.GENBA_CD))
            {
                searchDto.GENBA_CD = checkDto.GENBA_CD;

                dataItaku = kihonDao.GetItakuKeiyaku(searchDto);
                if (dataItaku == null || dataItaku.Rows.Count == 0)
                {
                    error.ERROR_KBN = (short)ITAKU_ERROR_KBN.GENBA_NOT_FOUND;
                    return error;
                }
            }
            //画面の現場CD　＝　BLANK (GYOUSHA_CD + [GENBA_CD=BLANK])
            else
            {
                searchDto.GENBA_CD = string.Empty;

                DataTable dataGyoushaOnly = kihonDao.GetItakuKeiyaku(searchDto);
                //排出事業者のみ登録されている委託契約書がない場合　又は
                //排出事業場が登録されている委託契約書が存在する場合 ((GYOUSHA_CD)件 != (GYOUSHA_CD + [GENBA_CD=BLANK])件)
                if (dataGyoushaOnly == null || dataGyoushaOnly.Rows.Count == 0 ||
                    dataGyoushaOnly.Rows.Count != dataItaku.Rows.Count)
                {
                    error.ERROR_KBN = (short)ITAKU_ERROR_KBN.GENBA_BLANK;
                    return error;
                }
            }

            //有効期間のチェック
            //画面の現場CD　≠　BLANKの場合: (GYOUSHA_CD + GENBA_CD + SAGYOU_DATE)
            //画面の現場CD　＝　BLANKの場合: (GYOUSHA_CD + [GENBA_CD=BLANK] + SAGYOU_DATE)
            searchDto.SAGYOU_DATE = Convert.ToDateTime(checkDto.SAGYOU_DATE);
            dataItaku = kihonDao.GetItakuKeiyaku(searchDto);
            if (dataItaku == null || dataItaku.Rows.Count == 0)
            {
                error.ERROR_KBN = (short)ITAKU_ERROR_KBN.YUUKOU_KIKAN;
                return error;
            }

            //報告書分類チェック
            List<DetailDTO> errorHoukoku = new List<DetailDTO>();
            foreach (var item in checkDto.LIST_HINMEI_HAIKISHURUI)
            {
                //未入力場合、チェックしない
                if (String.IsNullOrEmpty(item.CD))
                {
                    continue;
                }

                //マニフェストの場合
                if (checkDto.MANIFEST_FLG)
                {
                    searchDto.HAIKI_KBN_CD = checkDto.HAIKI_KBN_CD;
                    //電子の場合、廃棄物種類CDが7桁のため、カットして廃棄物種類CDの頭4桁をチェック
                    if (searchDto.HAIKI_KBN_CD == Shougun.Core.Common.BusinessCommon.Const.CommonConst.HAIKI_KBN_DENSHI)//電子
                    {
                        string cd = item.CD;
                        if (!string.IsNullOrEmpty(cd) && cd.Length > 4)
                        {
                            cd = cd.Substring(0, 4);
                        }
                        searchDto.HAIKI_SHURUI_CD = cd;
                    }
                    else
                    {
                        searchDto.HAIKI_SHURUI_CD = item.CD;
                    }
                }
                //マニフェスト以外の場合
                else
                {
                    //品名入力に廃棄物種類が入力がない場合、チェックしない
                    if (!IsCheckHinmei(item.CD))
                    {
                        continue;
                    }

                    searchDto.HINMEI_CD = item.CD;
                }

                dataItaku = kihonDao.GetItakuKeiyaku(searchDto);
                if (dataItaku == null || dataItaku.Rows.Count == 0)
                {
                    errorHoukoku.Add(item);
                }
            }

            if (errorHoukoku != null && errorHoukoku.Count > 0)
            {
                error.ERROR_KBN = (short)ITAKU_ERROR_KBN.HOUKOKUSHO_BUNRUI;
                error.DETAIL_ERROR = errorHoukoku;
            }

            return error;
        }

        /// <summary>
        /// 品名の廃棄物種類をチェック
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <returns></returns>
        private bool IsCheckHinmei(string hinmeiCd)
        {
            bool isCheck = false;
            M_HINMEI hinmei = hinmeiDao.GetDataByCd(hinmeiCd);

            //廃棄物種類CD入力チェック
            if (hinmei == null || 
                (string.IsNullOrEmpty(hinmei.SP_CHOKKOU_HAIKI_SHURUI_CD)
                && string.IsNullOrEmpty(hinmei.SP_TSUMIKAE_HAIKI_SHURUI_CD)
                && string.IsNullOrEmpty(hinmei.KP_HAIKI_SHURUI_CD)
                && string.IsNullOrEmpty(hinmei.DM_HAIKI_SHURUI_CD)))
            {
                return isCheck;
            }

            isCheck = true;
            return isCheck;
        }

        /// <summary>
        /// 委託チェックエラーを表示
        /// </summary>
        /// <param name="error">委託チェックエラー区分</param>
        /// <param name="alertAuth">委託チェックのアラートが出た場合の登録： 1.出来る、2.出来ない</param>
        /// <param name="manifestFlg">true: マニフェスト伝票, false: マニフェスト伝票以外</param>
        /// <param name="txtGyoushaCd">業者CDテキストボックス（エラー背景色を設定使用）</param>
        /// <param name="txtGenbaCd">現場CDテキストボックス（エラー背景色を設定使用）</param>
        /// <param name="txtSagyouDate">作業日テキストボックス（エラー背景色を設定使用）</param>
        /// <param name="gridDetail">明細データグリッドビュー（エラー背景色を設定使用）</param>
        /// <param name="detailControlName">品名又は廃棄物種類のセル名（エラー背景色を設定使用）</param>
        /// <returns></returns>
        public bool ShowError(ItakuErrorDTO error,
                              System.Data.SqlTypes.SqlInt16 alertAuth,
                              bool manifestFlg,
                              CustomAlphaNumTextBox txtGyoushaCd,
                              CustomAlphaNumTextBox txtGenbaCd,
                              CustomDateTimePicker txtSagyouDate,
                              object gridDetail,
                              string detailControlName)
        {
            var msgLogic = new MessageBoxShowLogic();
            var messageUtil = new MessageUtility();
            string messageContent = string.Empty;

            string TITLE_GYOUSHA = string.Empty;
            string TITLE_GENBA = string.Empty;
            string TITLE_DETAIL = string.Empty;

            //マニフェストの場合
            if (manifestFlg)
            {
                TITLE_GYOUSHA = "排出事業者";
                TITLE_GENBA = "排出事業場";
                TITLE_DETAIL = "廃棄物種類";
            }
            else
            {
                TITLE_GYOUSHA = "業者";
                TITLE_GENBA = "現場";
                TITLE_DETAIL = "品名";
            }

            string CTL_NAME_DETAIL = detailControlName;

            switch (error.ERROR_KBN)
            {
                case (short)ITAKU_ERROR_KBN.GYOUSHA://「業者 (委託契約書が未登録)」エラー
                    if (alertAuth == 1)
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_NOT_FOUND).MESSAGE;
                    }
                    else
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_NOT_FOUND).MESSAGE;
                        //背景色変更（入力Error）
                        if (txtGyoushaCd != null)
                        {
                            txtGyoushaCd.IsInputErrorOccured = true;
                            txtGyoushaCd.BackColor = Constans.ERROR_COLOR;
                        }
                    }
                    break;
                case (short)ITAKU_ERROR_KBN.GENBA_BLANK://「委託契約の排出事業場≠BLANK, 画面の現場＝BLANK」エラー
                    if (alertAuth == 1)
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_GENBA_BLANK).MESSAGE;
                        messageContent = String.Format(messageContent, TITLE_GYOUSHA, TITLE_GENBA);
                    }
                    else
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_GENBA_BLANK).MESSAGE;
                        messageContent = String.Format(messageContent, TITLE_GYOUSHA, TITLE_GENBA);
                        //背景色変更（入力Error）
                        if (txtGenbaCd != null)
                        {
                            txtGenbaCd.IsInputErrorOccured = true;
                            txtGenbaCd.BackColor = Constans.ERROR_COLOR;
                        }
                    }
                    break;
                case (short)ITAKU_ERROR_KBN.GENBA_NOT_FOUND://「委託契約に未登録の排出事業場」エラー
                    if (alertAuth == 1)
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_GENBA_NOT_FOUND).MESSAGE;
                        messageContent = String.Format(messageContent, TITLE_GYOUSHA);
                    }
                    else
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_GENBA_NOT_FOUND).MESSAGE;
                        messageContent = String.Format(messageContent, TITLE_GYOUSHA);
                        //背景色変更（入力Error）
                        if (txtGenbaCd != null)
                        {
                            txtGenbaCd.IsInputErrorOccured = true;
                            txtGenbaCd.BackColor = Constans.ERROR_COLOR;
                        }
                    }
                    break;
                case (short)ITAKU_ERROR_KBN.YUUKOU_KIKAN://「有効期間」エラー
                    if (alertAuth == 1)
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_YUUKOU_KIKAN).MESSAGE;
                    }
                    else
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_YUUKOU_KIKAN).MESSAGE;
                        //背景色変更（入力Error）
                        if (txtSagyouDate != null)
                        {
                            txtSagyouDate.IsInputErrorOccured = true;
                            txtSagyouDate.BackColor = Constans.ERROR_COLOR;
                        }
                    }
                    break;
                case (short)ITAKU_ERROR_KBN.HOUKOKUSHO_BUNRUI://「報告書分類」エラー
                    if (alertAuth == 1)
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_CFM_ITAKU_HOUKOKUSHO_BUNRUI).MESSAGE;
                    }
                    else
                    {
                        messageContent = messageUtil.GetMessage(ItakuKeiyakuCheckLogic.ID_MSG_ERR_ITAKU_HOUKOKUSHO_BUNRUI).MESSAGE;

                        List<string> listDetailCd = new List<string>();
                        if (error.DETAIL_ERROR != null && error.DETAIL_ERROR.Count > 0)
	                    {
                            listDetailCd = error.DETAIL_ERROR.Select(s => s.CD).ToList();
	                    }

                        //背景色変更（入力Error）
                        if (gridDetail is CustomDataGridView)
                        {
                            foreach (DataGridViewRow row in ((CustomDataGridView)gridDetail).Rows)
                            {
                                if (listDetailCd.Contains(Convert.ToString(row.Cells[CTL_NAME_DETAIL].Value)))
                                {
                                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[CTL_NAME_DETAIL], true);
                                    row.Cells[CTL_NAME_DETAIL].Style.BackColor = Constans.ERROR_COLOR;
                                }
                            }
                        }
                        else if (gridDetail is GcCustomMultiRow)
                        {
                            foreach (var row in ((GcCustomMultiRow)gridDetail).Rows)
                            {
                                if (listDetailCd.Contains(Convert.ToString(row.Cells[CTL_NAME_DETAIL].Value)))
                                {
                                    ControlUtility.SetInputErrorOccuredForMultiRow(row.Cells[CTL_NAME_DETAIL], true);
                                    row.Cells[CTL_NAME_DETAIL].Style.BackColor = Constans.ERROR_COLOR;
                                }
                            }
                        }
                    }

                    //エラー明細
                    StringBuilder showMsg = new StringBuilder();
                    int showCount = error.DETAIL_ERROR.Count;
                    if (showCount > ItakuKeiyakuCheckLogic.MAX_ERROR_SHOW)
                    {
                        showCount = ItakuKeiyakuCheckLogic.MAX_ERROR_SHOW;
                    }

                    for (int i = 0; i < showCount; i++)
                    {
                        showMsg.AppendFormat(ItakuKeiyakuCheckLogic.ERROR_APPEND_DETAIL, TITLE_DETAIL, error.DETAIL_ERROR[i].CD, error.DETAIL_ERROR[i].NAME);
                        showMsg.AppendLine();
                    }

                    //明細エラー数 ＞ 最大表示エラー数の場合、計件を表示
                    if (error.DETAIL_ERROR.Count > ItakuKeiyakuCheckLogic.MAX_ERROR_SHOW)
                    {
                        showMsg.AppendFormat(ItakuKeiyakuCheckLogic.ERROR_APPEND_SUM_RECORD, error.DETAIL_ERROR.Count);
                        showMsg.AppendLine();
                    }

                    messageContent = String.Format(messageContent, TITLE_DETAIL, showMsg.ToString());
                    break;
            }

            //エラーを表示
            if (alertAuth == 1)
            {
                if (msgLogic.MessageBoxShowConfirm(messageContent) != DialogResult.Yes)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                msgLogic.MessageBoxShowError(messageContent);
                return false;
            }
        }

        /// <summary>
        /// 委託契約チェックするかチェックしないかをチェック
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <param name="checkDto"></param>
        /// <returns>false: 委託チェックしない, true: 委託チェックする</returns>
        public bool IsCheckItakuKeiyaku(M_SYS_INFO sysInfo, ItakuCheckDTO checkDto)
        {
            bool isCheck = false;

            //【システム設定入力】[委託チェック]　≠　「1.する」の場合、チェックしない
            if (sysInfo.ITAKU_KEIYAKU_CHECK != 1)
            {
                return isCheck;
            }

            //業者又は作業日が未入力の場合、チェックしない
            if (string.IsNullOrEmpty(checkDto.GYOUSHA_CD) || string.IsNullOrEmpty(checkDto.SAGYOU_DATE))
            {
                return isCheck;
            }

            //【現場入力】[委託契約チェック]　≠　「1.する」の場合、チェックしない
            if (!String.IsNullOrEmpty(checkDto.GYOUSHA_CD) && !String.IsNullOrEmpty(checkDto.GENBA_CD))
            {
                M_GENBA cond = new M_GENBA();
                cond.GYOUSHA_CD = checkDto.GYOUSHA_CD;
                cond.GENBA_CD = checkDto.GENBA_CD;
                var genbaData = genbaDao.GetDataByCd(cond);

                if (genbaData != null && genbaData.ITAKU_KEIYAKU_USE_KBN != 1)
                {
                    return isCheck;
                }
            }

            isCheck = true;
            return isCheck;
        }
        #endregion
    }
}