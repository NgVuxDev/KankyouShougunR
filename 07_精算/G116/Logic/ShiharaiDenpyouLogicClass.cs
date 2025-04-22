using System;
using System.Data;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using System.Collections;

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
{
    /// <summary>
    /// 支払明細書印刷用の共通ロジック
    /// </summary>
    /// <remarks>
    /// G111:明細書確認から参照
    /// </remarks>
    public class ShiharaiDenpyouLogicClass
    {
        /// <summary>
        /// 支払書プレビュー処理
        /// </summary>
        /// <param name="tSeisanDenpyou">精算伝票Entity</param>
        /// <param name="dto">支払明細書発行用DTO</param>
        public static void PreviewShiharaiDenpyou(T_SEISAN_DENPYOU tSeisanDenpyou, ShiharaiDenpyouDto dto, bool printFlg, string invoiceKBN, bool ZeiHyouji)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (Validateing(tSeisanDenpyou, dto))
            {
                // 通常はありあえない。開発用
                msgLogic.MessageBoxShow("E027", "精算情報");
                return;
            }

            TSDDaoCls seisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            string seisanNumber = tSeisanDenpyou.SEISAN_NUMBER.ToString();
            string shoshikiKbn = tSeisanDenpyou.SHOSHIKI_KBN.ToString();
            string shoshikiMeisaiKbn = tSeisanDenpyou.SHOSHIKI_MEISAI_KBN.ToString();
            string shukkinMeisaiKbn = tSeisanDenpyou.SHUKKIN_MEISAI_KBN.ToString();

            //請求伝票データ取得
            DataTable seikyuDt = LogicClass.GetSeisandenpyo(seisanDenpyouDao, seisanNumber, shoshikiKbn, shoshikiMeisaiKbn, shukkinMeisaiKbn);

            //発行対象データが0件の場合はメッセージ表示
            if (seikyuDt.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("I008", "支払明細書");
                return;
            }


            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            //int count = LogicClass.SetSeisanDenpyo(seikyuDt, dto, reportR337, printFlg);

            //var result = LogicClass.SetSeisanDenpyo(seikyuDt, dto, printFlg);
            var result = new ArrayList();
            if (invoiceKBN == "2")
            {
                //適格請求書
                result = LogicClass.SetSeisanDenpyo_invoice(seikyuDt, dto, printFlg, false, "", ZeiHyouji);
            }
            else
            {
                //旧請求書式
                result = LogicClass.SetSeisanDenpyo(seikyuDt, dto, printFlg);
            } 
            int count = (int)result[0];
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
            

            //発行対象データが0件の場合はメッセージ表示
            if (count == 0)
            {
                msgLogic.MessageBoxShow("I008", "支払明細書");
                return;
            }

            //FormReport formReport = LogicClass.CreateFormReport(dto, aryPrint);

            // 印刷設定の取得（9:支払明細書）
            //formReport.SetPrintSetting(9);

            // 印刷アプリ初期動作(プレビュー)
            //formReport.PrintInitAction = 2;

            //formReport.ShowDialog();
            //formReport.PrintXPS();

            //if (formReport.IsPrintComplete)
            //{
            if(printFlg)
            {
                DataTable seikyuDenpyo = LogicClass.UpdateSeisanDenpyouHakkouKbn(seisanDenpyouDao, seisanNumber, tSeisanDenpyou.TIME_STAMP);
                if (seikyuDenpyo != null && seikyuDenpyo.Rows.Count > 0)
                {
                    //TimeStamp更新
                    tSeisanDenpyou.TIME_STAMP = (byte[])seikyuDenpyo.Rows[0]["TIME_STAMP"];
                }
            }
            //}

            //formReport.Dispose();
        }

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>
        /// 請求書プレビュー処理
        /// </summary>
        /// <param name="tSeisanDenpyou"></param>
        /// <param name="dto"></param>
        public static ArrayList PreViewShiharaiDenpyouRemote(T_SEISAN_DENPYOU tSeisanDenpyou, ShiharaiDenpyouDto dto, bool printFlg, bool isExportPDF = false, string path = "", bool IsZeroKingakuTaishogai = false)
        {
            if (Validateing(tSeisanDenpyou, dto))
            {
                return null;
            }

            TSDDaoCls seisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            string seisanNumber = tSeisanDenpyou.SEISAN_NUMBER.ToString();
            string shoshikiKbn = tSeisanDenpyou.SHOSHIKI_KBN.ToString();
            string shoshikiMeisaiKbn = tSeisanDenpyou.SHOSHIKI_MEISAI_KBN.ToString();
            string shukkinMeisaiKbn = tSeisanDenpyou.SHUKKIN_MEISAI_KBN.ToString();

            //請求伝票データ取得
            DataTable seikyuDt = LogicClass.GetSeisandenpyo(seisanDenpyouDao, seisanNumber, shoshikiKbn, shoshikiMeisaiKbn, shukkinMeisaiKbn, IsZeroKingakuTaishogai);

            //発行対象データが0件の場合はメッセージ表示
            if (seikyuDt.Rows.Count == 0)
            {
                return null;
            }

            return LogicClass.SetSeisanDenpyo(seikyuDt, dto, printFlg, isExportPDF, path);
        }

        public static DataTable UpdateSeisanDenpyouHakkouKbnRemote(string seisanNumber, byte[] timeStamp)
        {
            TSDDaoCls seisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            return LogicClass.UpdateSeisanDenpyouHakkouKbn(seisanDenpyouDao, seisanNumber, timeStamp);
        }
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

        /// <summary>
        /// 引数の必須チェック
        /// </summary>
        /// <param name="tSeisanDenpyou">精算伝票Entity</param>
        /// <param name="dto">支払明細書発行用DTO</param>
        /// <returns></returns>
        private static bool Validateing(T_SEISAN_DENPYOU tSeisanDenpyou, ShiharaiDenpyouDto dto)
        {
            if (tSeisanDenpyou == null
                || tSeisanDenpyou.SEISAN_NUMBER.IsNull
                || tSeisanDenpyou.SHOSHIKI_KBN.IsNull
                || tSeisanDenpyou.SHOSHIKI_MEISAI_KBN.IsNull
                || tSeisanDenpyou.SHUKKIN_MEISAI_KBN.IsNull)
            {
                return true;
            }

            if (dto == null
                || string.IsNullOrEmpty(dto.Meisai)
                || dto.MSysInfo == null
                || string.IsNullOrEmpty(dto.ShiharaiHakkou)
                || string.IsNullOrEmpty(dto.ShiharaiPaper)
                || string.IsNullOrEmpty(dto.ShiharaiPrintDay)
                || string.IsNullOrEmpty(dto.ShiharaiStyle)
                || string.IsNullOrEmpty(dto.TorihikisakiCd))
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 支払明細書発行用DTO
    /// </summary>
    public class ShiharaiDenpyouDto
    {
        /// <summary>
        /// 取引先CD
        /// </summary>
        public String TorihikisakiCd { get; set; }

        /// <summary>
        /// システム設定
        /// </summary>
        public M_SYS_INFO MSysInfo { get; set; }

        /// <summary>
        /// 発行日
        /// </summary>
        public string HakkoBi { get; set; }
        /// <summary>
        /// 明細
        /// </summary>
        /// <remarks>
        /// 1.有り
        /// 2.無し
        /// </remarks>
        public String Meisai { get; set; }

        /// <summary>
        /// 支払明細書発行日
        /// </summary>
        /// <remarks>
        /// 1.印刷する
        /// 2.印刷しない
        /// </remarks>
        public String ShiharaiHakkou { get; set; }

        /// <summary>
        /// 支払年月日指定
        /// </summary>
        /// <remarks>
        /// 1.締日
        /// 2.発行日
        /// 3.無し
        /// 4.指定
        /// </remarks>
        public String ShiharaiPrintDay { get; set; }

        /// <summary>
        /// 支払年月日
        /// </summary>
        /// <remarks>
        /// 支払年月日指定で「4.指定」が選択された時に
        /// 値を保持するプロパティ
        /// </remarks>
        public DateTime ShiharaiDate { get; set; }

        /// <summary>
        /// 支払形態
        /// </summary>
        /// <remarks>
        /// 1.支払データ作成時 → T_SEISAN_DENPYOU.SHIHARAI_KEITAI_KBN を利用して発行
        /// 2.単月精算         → 単月精算として発行
        /// 3.繰越精算         → 繰越精算として発行
        /// </remarks>
        public String ShiharaiStyle { get; set; }

        /// <summary>
        /// 支払用紙
        /// </summary>
        /// <remarks>
        /// 1.支払データ作成時/自社
        /// 2.支払データ作成時/指定
        /// 3.自社
        /// 4.指定
        /// </remarks>
        public string ShiharaiPaper { get; set; }

        /// <summary>
        /// 控え印刷flag
        /// </summary>
        public bool PrintDirectFlg { get; set; }
    }
}