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
using Seasar.Dao.Attrs;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Dto;


// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.SampaiManifestoThumiKae
{

    [Bean(typeof(M_SYS_INFO))]
    public interface SysInfoDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetSysInfo.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }

    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface ParameterDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.SerchParameter.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

        // 20140601 katen 不具合No.4133 start‏
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetDataForPrevious.sql")]
        new DataTable GetDataForPrevious(SerchParameterDtoCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetDataForNext.sql")]
        new DataTable GetDataForNext(SerchParameterDtoCls data);
        // 20140601 katen 不具合No.4133 end‏

        //20140611 syunrei マニフェスト入力画面からの連携仕様_20140610 start‏
        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="RENKEI_DENSHU_KBN_CD">画面．伝種区分</param>
        /// <param name="RENKEI_SYSTEM_ID">画面．システムID</param>
        /// <param name="RENKEI_MEISAI_SYSTEM_ID">画面．SEQ</param>
        /// <param name="DELETE_FLG">DELETE_FLG</param>
        /// <returns></returns>
        T_MANIFEST_ENTRY GetManiFestDataForEntity(int RENKEI_DENSHU_KBN_CD, int RENKEI_SYSTEM_ID, int RENKEI_MEISAI_SYSTEM_ID, int RENKEI_MEISAI_MODE, bool DELETE_FLG);
        //20140611 syunrei マニフェスト入力画面からの連携仕様_20140610 end‏
    }

    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface MaxSeqDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetMaxSeq.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface UkeireDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUkeire.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUkeireJisseki.sql")]
        new DataTable GetDataForJissekiEntity(SerchParameterDtoCls data);    
        
    }

    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface ShukkaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetShukka.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }

    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface UriageDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUriage.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }

    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface HaishaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUHaisha.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }

    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface KeiryouDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetKeiryo.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetKeiryoJisseki.sql")]
        new DataTable GetDataForJissekiEntity(SerchParameterDtoCls data);

    }

    // 20140523 syunrei No.679 産廃マニフェスト（積替）入力連携 start
    /// <summary>
    /// 連携データ検索:(4)伝種区分が100（受付）の場合,①収集受付Data（T_UKETSUKE_SS_ENTRY,T_UKETSUKE_SS_DETAIL）を取得する。
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface GetUketsukeSsDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUketsukeSs.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(4)伝種区分が100（受付）の場合,②収集受付Dataが取得できなければ、次に持込受付Data（T_UKETSUKE_MK_ENTRY,T_UKETSUKE_MK_DETAIL）を取得する。
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    public interface GetUketsukeMkDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUketsukeMk.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(4)伝種区分が100（受付）の場合,③持込受付Dataが取得できなければ、次に出荷受付Data（T_UKETSUKE_SK_ENTRY,T_UKETSUKE_SK_DETAIL）を取得する。
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    public interface GetUketsukeSkDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUketsukeSk.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    /// <summary>
    ///  伝種区分名称を取得する
    /// </summary>
    [Bean(typeof(M_DENSHU_KBN))]
    public interface GetDenshuKbnNameDaoCls : IS2Dao
    {
        /// <summary>
        /// 伝種区分名称を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetDenshuKbnName.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }
    // 20140523 syunrei No.679 産廃マニフェスト（積替）入力連携 end
    
    /// <summary>
    /// 混合種別名検索
    /// </summary>
    [Bean(typeof(M_KONGOU_SHURUI))]
    public interface GetKongouNameDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetKongouName.sql")]
        new DataTable GetDataForEntity(GetKongouNameDtoCls data);
    }

    /// <summary>
    /// マニ印字_産廃_形状更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_KEIJYOU))]
    public interface KeijyouDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_KEIJYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_KP_KEIJYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_KP_KEIJYOU data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.SerchKeijyou.sql")]
        new DataTable GetDataForEntity(GetSysIdSeqDtoCls data);
    }

    /// <summary>
    /// マニ印字_産廃_荷姿更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_NISUGATA))]
    public interface NisugataDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_NISUGATA data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_KP_NISUGATA data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_KP_NISUGATA data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.SerchNisugata.sql")]
        new DataTable GetDataForEntity(GetSysIdSeqDtoCls data);
    }

    /// <summary>
    /// マニ印字_産廃_処分方法更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_KP_SBN_HOUHOU))]
    public interface HouhouDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_KP_SBN_HOUHOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_KP_SBN_HOUHOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_KP_SBN_HOUHOU data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.SerchHouhou.sql")]
        new DataTable GetDataForEntity(GetSysIdSeqDtoCls data);
    }

    [Bean(typeof(M_DENSHU_KBN))]
    public interface GetDenshuDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetDenshu.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    /// <summary>
    /// マニ返却日の枝番の最大値取得
    /// </summary>
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface MaxRetDateDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetMaxRetDate.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);

    }

    ///// <summary>
    ///// 交付番号検索
    ///// </summary>
    //[Bean(typeof(T_MANIFEST_ENTRY))]
    //public interface SerchKohuDaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Entityで絞り込んで値を取得する
    //    /// </summary>
    //    [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.SerchKohu.sql")]
    //    new DataTable GetDataForEntity(SerchKohuDtoCls data);
    //}

    /// <summary>
    /// 単位名称取得
    /// </summary>
    [Bean(typeof(M_UNIT))]
    public interface GetUnitDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUnit.sql")]
        new DataTable GetDataForEntity(GetUnitDtoCls data);

    }

    #region パターン

    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface PtMaxSeqDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetMaxPtSeq.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }

    #endregion

    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface GetHaikiShuruiDaoCls : IS2Dao
    {
        /// <summary>
        /// 開始残高値を取得する。
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetHaikiShurui.sql")]
        DataTable GetDataForEntity(GetHaikiShuruiDtoCls data);
    }

    /// <summary>
    /// 運転者検索
    /// </summary>
    [Bean(typeof(M_UNTENSHA))]
    public interface GetUntenshaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetUntensha.sql")]
        new DataTable GetDataForEntity(GetUntenshaDtoCls data);

    }

    ///// <summary>
    ///// 処分担当者検索
    ///// </summary>
    //[Bean(typeof(M_SHOBUN_TANTOUSHA))]
    //public interface GetShobunTantoushaDaoCls : IS2Dao
    //{
    //    /// <summary>
    //    /// Entityで絞り込んで値を取得する
    //    /// </summary>
    //    [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetShobunTantousha.sql")]
    //    new DataTable GetDataForEntity(GetShobunTantoushaDtoCls data);

    //}

    /// <summary>
    /// 車輌検索
    /// </summary>
    [Bean(typeof(M_SHARYOU))]
    public interface GetCarDataDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetCarData.sql")]
        new DataTable GetDataForEntity(GetCarDataDtoCls data);

    }

    // 20140513 syunrei No.679 産廃マニフェスト（積替）入力連携 start

    /// <summary>
    /// 連携データ検索:(1)伝種区分が1（受入）の場合
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface GetRenkeiUkeireiDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiUkeirei.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiUkeireiJisseki.sql")]
        new DataTable GetDataForJissekiEntity(GetRenkeiDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(2)伝種区分が2（出荷）の場合
    /// </summary>
    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface GetRenkeiShukkaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiShukka.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

    }
    
    /// <summary>
    /// 連携データ検索:(3)伝種区分が3（売上支払）の場合
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface GetRenkeiUrShDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiUrSh.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(4)伝種区分が100（受付）の場合,①収集受付Data（T_UKETSUKE_SS_ENTRY,T_UKETSUKE_SS_DETAIL）を取得する。
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface GetRenkeiUketsukeSsDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiUketsukeSs.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(4)伝種区分が100（受付）の場合,②収集受付Dataが取得できなければ、次に持込受付Data（T_UKETSUKE_MK_ENTRY,T_UKETSUKE_MK_DETAIL）を取得する。
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    public interface GetRenkeiUketsukeMkDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiUketsukeMk.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(4)伝種区分が100（受付）の場合,③持込受付Dataが取得できなければ、次に出荷受付Data（T_UKETSUKE_SK_ENTRY,T_UKETSUKE_SK_DETAIL）を取得する。
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    public interface GetRenkeiUketsukeSkDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiUketsukeSk.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

    }

    /// <summary>
    /// 連携データ検索:(5)伝種区分が140（計量）の場合
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface GetRenkeiKeiryouDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiKeiryou.sql")]
        new DataTable GetDataForEntity(GetRenkeiDtoCls data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetRenkeiKeiryouJisseki.sql")]
        new DataTable GetDataForJissekiEntity(GetRenkeiDtoCls data);

    }

    // 20140513 syunrei No.679 産廃マニフェスト（積替）入力連携 end

    // 20140606 syunrei No.730 規定値機能の追加について start
    /// <summary>
    /// 規定値を取得する
    /// </summary>
    [Bean(typeof(T_MANIFEST_PT_ENTRY))]
    public interface KiteiValueDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetKitei.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }
    // 20140606 syunrei No.730 規定値機能の追加について end

    // 20140611 katen 不具合No.4469 start‏
    /// <summary>
    /// 業者マスタ検索
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface GyoushaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.Manifest_Gyousha.sql")]
        new DataTable GetDataForEntity(CommonGyoushaDtoCls data);
    }

    /// <summary>
    /// 現場マスタ検索
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface GenbaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.Manifest_Genba.sql")]
        new DataTable GetDataForEntity(CommonGenbaDtoCls data);
    }
    // 20140611 katen 不具合No.4469 end‏
    // 20140616 syunrei EV004383_積替マニフェスト入力の原本タブの産業廃棄物種類の登録するカラムが間違っている start
    /// <summary>
    /// 規定値を取得する
    /// </summary>
    [Bean(typeof(T_MANIFEST_PRT))]
    public interface maniFestPRTDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.SampaiManifestoThumiKae.Sql.GetManiFestPRT.sql")]
        new DataTable GetDataForEntity(SerchParameterDtoCls data);
    }
    // 20140616 syunrei EV004383_積替マニフェスト入力の原本タブの産業廃棄物種類の登録するカラムが間違っている start


    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
    /// <summary>
    /// マニフェスト
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface T_MANIFEST_ENTRYdaocls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"
                            , "TIME_STAMP")]
        int Update(T_MANIFEST_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_ENTRY data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_ENTRY GetDataByPrimaryKey(T_MANIFEST_ENTRY data);
    }

    /// <summary>
    /// マニ明細更新
    /// </summary>
    [Bean(typeof(T_MANIFEST_DETAIL))]
    public interface T_MANIFEST_DETAILdaocls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_DETAIL data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        List<T_MANIFEST_DETAIL> GetDataListByPrimaryKey(T_MANIFEST_DETAIL data);
    }

    /// <summary>
    /// マニ返却日(T_MANIFEST_RET_DATE)更新用
    /// </summary>
    [Bean(typeof(T_MANIFEST_RET_DATE))]
    public interface T_MANIFEST_RELATIONDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_RET_DATE data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_RET_DATE GetDataByPrimaryKey(T_MANIFEST_RET_DATE data);
    }

    /// <summary>
    /// マニ運搬(T_MANIFEST_UPN)更新用
    /// </summary>
    [Bean(typeof(T_MANIFEST_UPN))]
    public interface T_MANIFEST_UPNDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_UPN data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_UPN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_UPN data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        List<T_MANIFEST_UPN> GetDataListByPrimaryKey(T_MANIFEST_UPN data);
    }

    /// <summary>
    /// マニ印刷(T_MANIFEST_PRT)更新用
    /// </summary>
    [Bean(typeof(T_MANIFEST_PRT))]
    public interface T_MANIFEST_PRTDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MANIFEST_PRT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_MANIFEST_PRT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">条件</param>
        /// <returns></returns>
        int Delete(T_MANIFEST_PRT data);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ AND SEQ = /*data.SEQ*/")]
        T_MANIFEST_PRT GetDataByPrimaryKey(T_MANIFEST_PRT data);
    }
    // 20141205 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

    /// <summary>
    /// 検索二次マニ
    /// </summary>
    [Bean(typeof(T_MANIFEST_RELATION))]
    public interface T_MANIFEST_RELATIONSDaoCls : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_MANIFEST_RELATION WHERE NEXT_SYSTEM_ID = /*systemId*/ AND NEXT_HAIKI_KBN_CD = /*haikikbnCd*/ AND DELETE_FLG = 0 ")]
        T_MANIFEST_RELATION[] GetDataBySystemId(Int64 systemId, Int16 haikikbnCd);
    }
}
