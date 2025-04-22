using System;
using System.Collections;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Billing.SeikyuushoHakkou.DAO;
using Shougun.Core.Billing.SeikyuushoHakkou.Const;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using r_framework.Dao;
using Shougun.Core.Common.BusinessCommon.Xml;
using System.IO;

namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    /// <summary>
    /// 請求書印刷用の共通ロジック
    /// </summary>
    /// <remarks>
    /// G102:請求書確認から参照
    /// </remarks>
    public class SeikyuuDenpyouLogicClass
    {
        /// <summary>
        /// 請求書プレビュー処理
        /// </summary>
        /// <param name="tSeikyuuDenpyou"></param>
        /// <param name="dto"></param>
        public static void PreViewSeikyuDenpyou(T_SEIKYUU_DENPYOU tSeikyuuDenpyou, SeikyuuDenpyouDto dto, bool printFlg, string invoiceKBN, bool ZeiHyouji)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (Validateing(tSeikyuuDenpyou, dto))
            {
                // 通常はありあえない。開発用
                msgLogic.MessageBoxShow("E027", "請求情報");
                return;
            }

            TSDDaoCls seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            string seikyuNumber = tSeikyuuDenpyou.SEIKYUU_NUMBER.ToString();
            string shoshikiKbn = tSeikyuuDenpyou.SHOSHIKI_KBN.ToString();
            string shoshikiMeisaiKbn = tSeikyuuDenpyou.SHOSHIKI_MEISAI_KBN.ToString();
            string nyuukinMeisaiKbn = tSeikyuuDenpyou.NYUUKIN_MEISAI_KBN.ToString();

            //請求伝票データ取得
            DataTable seikyuDt = LogicClass.GetSeikyudenpyo(seikyuDenpyouDao, seikyuNumber, shoshikiKbn, shoshikiMeisaiKbn, nyuukinMeisaiKbn);

            //発行対象データが0件の場合はメッセージ表示
            if (seikyuDt.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("I008", "請求書");
                return;
            }
            
            M_FILE_LINK_SYS_INFO fileLink = new M_FILE_LINK_SYS_INFO();
            IM_FILE_LINK_SYS_INFODao fileLinkSysInfoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_SYS_INFODao>();
            T_FILE_DATA fileData = new T_FILE_DATA();
            FILE_DATADAO fileDataDao = DaoInitUtility.GetComponent<FILE_DATADAO>();
            FileUploadLogic uploadLogic = new FileUploadLogic();

            fileLink = fileLinkSysInfoDao.GetDataById("0");

            if (fileLink != null)
            {
                // ファイルIDからファイル情報を取得
                long fileId = (long)fileLink.FILE_ID;
                fileData = fileDataDao.GetDataByKey(fileId);

                // ファイルパスにファイルが存在しない場合
                if (!(File.Exists(fileData.FILE_PATH)))
                {
                    // ユーザ定義情報を取得
                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();

                    // ファイルアップロード参照先のフォルダを取得
                    string folderPath = uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                    //システム個別設定入力の初期フォルダの設定有無をチェックする。
                    if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                    {
                        MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
                        errmessage.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
                        return;
                    }
                }
            }
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            //int count = LogicClass.SetSeikyuDenpyo(seikyuDt, dto, reportR336, printFlg);
            //var result = LogicClass.SetSeikyuDenpyo(seikyuDt, dto, printFlg);
            var result = new ArrayList();
            if (invoiceKBN == "2")
            {
                //適格請求書
                result = LogicClass.SetSeikyuDenpyo_invoice(seikyuDt, dto, printFlg, false, "", ZeiHyouji);
            }
            else
            {
                //旧請求書式
                result = LogicClass.SetSeikyuDenpyo(seikyuDt, dto, printFlg);
            } 
            int count = (int)result[0];
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            //発行対象データが0件の場合はメッセージ表示
            if (count == 0)
            {
                msgLogic.MessageBoxShow("I008", "請求書");
                return;
            }

            //FormReport formReport = LogicClass.CreateFormReport(dto, aryPrint);

            // 印刷設定の取得（8:請求書）
            //formReport.SetPrintSetting(8);

            // 印刷アプリ初期動作(プレビュー)
            //formReport.PrintInitAction = 2;

            //formReport.ShowDialog();
            //formReport.PrintXPS();

            //if (formReport.IsPrintComplete)
            //{
            if(printFlg)
            {
                DataTable seikyuDenpyo = LogicClass.UpdateSeikyuDenpyouHakkouKbn(seikyuDenpyouDao, seikyuNumber, tSeikyuuDenpyou.TIME_STAMP);
                if (seikyuDenpyo != null && seikyuDenpyo.Rows.Count > 0)
                {
                    //TimeStamp更新
                    tSeikyuuDenpyou.TIME_STAMP = (byte[])seikyuDenpyo.Rows[0]["TIME_STAMP"];
                }
            }
            //}

            //formReport.Dispose();
        }

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        /// <summary>
        /// 請求書プレビュー処理
        /// </summary>
        /// <param name="tSeikyuuDenpyou"></param>
        /// <param name="dto"></param>
        public static ArrayList PreViewSeikyuDenpyouRemote(T_SEIKYUU_DENPYOU tSeikyuuDenpyou, SeikyuuDenpyouDto dto, bool printFlg, bool isExportPDF = false, string path = "", bool isZeroKingakuTaishogai = false)
        {
            if (Validateing(tSeikyuuDenpyou, dto))
            {
                return null;
            }

            TSDDaoCls seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            string seikyuNumber = tSeikyuuDenpyou.SEIKYUU_NUMBER.ToString();
            string shoshikiKbn = tSeikyuuDenpyou.SHOSHIKI_KBN.ToString();
            string shoshikiMeisaiKbn = tSeikyuuDenpyou.SHOSHIKI_MEISAI_KBN.ToString();
            string nyuukinMeisaiKbn = tSeikyuuDenpyou.NYUUKIN_MEISAI_KBN.ToString();

            //請求伝票データ取得
            DataTable seikyuDt = LogicClass.GetSeikyudenpyo(seikyuDenpyouDao, seikyuNumber, shoshikiKbn, shoshikiMeisaiKbn, nyuukinMeisaiKbn, false, isZeroKingakuTaishogai);

            //発行対象データが0件の場合はメッセージ表示
            if (seikyuDt.Rows.Count == 0)
            {
                return null;
            }


            return LogicClass.SetSeikyuDenpyo(seikyuDt, dto, printFlg, isExportPDF, path);
        }

        public static DataTable UpdateSeikyuDenpyouHakkouKbnRemote(string seikyuNumber, byte[] timeStamp)
        {
            TSDDaoCls seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            return LogicClass.UpdateSeikyuDenpyouHakkouKbn(seikyuDenpyouDao, seikyuNumber, timeStamp);
        }
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        /// <summary>
        /// 引数の必須チェック
        /// </summary>
        /// <param name="tSeikyuuDenpyou"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static bool Validateing(T_SEIKYUU_DENPYOU tSeikyuuDenpyou, SeikyuuDenpyouDto dto)
        {
            if (tSeikyuuDenpyou == null
                || tSeikyuuDenpyou.SEIKYUU_NUMBER.IsNull
                || tSeikyuuDenpyou.SHOSHIKI_KBN.IsNull
                || tSeikyuuDenpyou.SHOSHIKI_MEISAI_KBN.IsNull
                || tSeikyuuDenpyou.NYUUKIN_MEISAI_KBN.IsNull)
            {
                return true;
            }

            if (dto == null
                || string.IsNullOrEmpty(dto.Meisai)
                || dto.MSysInfo == null
                || string.IsNullOrEmpty(dto.SeikyuHakkou)
                || string.IsNullOrEmpty(dto.SeikyuPaper)
                || string.IsNullOrEmpty(dto.SeikyushoPrintDay)
                || string.IsNullOrEmpty(dto.SeikyuStyle)
                || string.IsNullOrEmpty(dto.TorihikisakiCd))
            {

                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 請求書発行用DTO
    /// </summary>
    public class SeikyuuDenpyouDto
    {
        /// <summary>
        /// 取引先CD
        /// </summary>
        public String TorihikisakiCd { get; set; }

        /// <summary>
        /// 帳票発行日
        /// </summary>
        public String HakkoBi { get; set; }

        /// <summary>
        /// システム設定
        /// </summary>
        public M_SYS_INFO MSysInfo { get; set; }

        /// <summary>
        /// 明細
        /// </summary>
        /// <remarks>
        /// 1.有り
        /// 2.無し
        /// </remarks>
        public String Meisai { get; set; }

        /// <summary>
        /// 請求書発行日
        /// </summary>
        /// <remarks>
        /// 1.印刷する
        /// 2.印刷しない
        /// </remarks>
        public String SeikyuHakkou { get; set; }

        /// <summary>
        /// 請求年月日指定
        /// </summary>
        /// <remarks>
        /// 1.締日
        /// 2.発行日
        /// 3.無し
        /// 4.指定
        /// </remarks>
        public String SeikyushoPrintDay { get; set; }

        /// <summary>
        /// 請求年月日
        /// </summary>
        /// <remarks>
        /// 請求年月日指定で「4.指定」が選択された時に
        /// 値を保持するプロパティ
        /// </remarks>
        public DateTime SeikyuDate { get; set; }

        /// <summary>
        /// 請求形態
        /// </summary>
        /// <remarks>
        /// 1.請求書データ作成時 → T_SEIKYUU_DENPYOU.SEIKYUU_KEITAI_KBN を利用して発行
        /// 2.単月請求           → 単月請求として発行
        /// 3.繰越請求           → 繰越請求として発行
        /// </remarks>
        public String SeikyuStyle { get; set; }

        /// <summary>
        /// 請求用紙
        /// </summary>
        /// <remarks>
        /// 1.請求データ作成時/自社
        /// 2.請求データ作成時/指定
        /// 3.自社
        /// 4.指定
        /// </remarks>
        public string SeikyuPaper { get; set; }

        /// <summary>
        /// 控え印刷flag refs #158002
        /// </summary>
        public bool PrintDirectFlg { get; set; }
    }
}
