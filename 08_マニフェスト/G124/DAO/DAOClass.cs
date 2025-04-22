using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestCheckHyo
{
    /// <summary>
    /// 会社名
    /// </summary>
    [Bean(typeof(M_CORP_INFO))]
    public interface IM_CORP_NAMEDaoCls : IS2Dao
    {
        [Sql("SELECT TOP 1 CORP_RYAKU_NAME FROM M_CORP_INFO")]
        //DataTable GetCorpName();
        string GetCorpName();
    }

    /// <summary>
    /// 業者マスタ
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface CheckGyoushaMasterDaoCls : IS2Dao
    {
        /// <summary>
        /// 業者マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckGyoushaMaster.sql")]
        new DataTable GetGyoushaMasterCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 業者マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckGyoushaMasterUnpan.sql")]
        new DataTable GetGyoushaMasterCheckDataU(SerchCheckManiDtoCls data);

        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);
    }

    /// <summary>
    /// 現場マスタ
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface CheckGenbaMasterDaoCls : IS2Dao
    {
        /// <summary>
        /// 現場マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckGenbaMaster.sql")]
        new DataTable GetGenbaMasterCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 現場マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckGenbaMasterUnpan.sql")]
        new DataTable GetGenbaMasterCheckDataU(SerchCheckManiDtoCls data);

        /// <summary>
        /// 現場マスタ_地域妥当性チェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckGenbaChiikiDatousei.sql")]
        new DataTable GetGenbaChiikiDatouseiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 現場_運搬報告書提出先妥当性チェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckGenbaUnpanHoukokushoDatousei.sql")]
        new DataTable GetGenbaUnpanHoukokushoDatouseiCheckData(SerchCheckManiDtoCls data);

        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);
    }

    /// <summary>
    /// 廃棄物種類マスタ
    /// </summary>
    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface CheckHaikiShuruiMasterDaoCls : IS2Dao
    {
        /// <summary>
        /// 廃棄物種類マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckHaikiShuruiMaster.sql")]
        new DataTable GetHaikiShuruiCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 電子廃棄物種類マスタ
    /// </summary>
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface CheckDenHaikiShuruiMasterDaoCls : IS2Dao
    {
        /// <summary>
        /// 電子廃棄物種類マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenHaikiShuruiMaster.sql")]
        new DataTable GetDenHaikiShuruiCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 電子事業者マスタ
    /// </summary>
    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface CheckDenshiJigyoushaDaoCls : IS2Dao
    {
        /// <summary>
        /// 電子事業者マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenshiJigyousha.sql")]
        new DataTable GetDenshiJigyoushaCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 電子事業場マスタ
    /// </summary>
    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface CheckDenshiJigyoujouDaoCls : IS2Dao
    {
        /// <summary>
        /// 電子事業者マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenshiJigyoujou.sql")]
        new DataTable GetDenshiJigyoujouCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 連携事業場_地域妥当性チェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenshiJigyoujouChiiki.sql")]
        new DataTable GetDenshiJigyoujouChiikiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 連携事業場_運搬報告書提出先妥当性チェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenshiJigyoujouUnpanHoukokusho.sql")]
        new DataTable GetDenshiJigyoujouUnpanHoukokushoCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 地域別業種マスタ
    /// </summary>
    [Bean(typeof(M_CHIIKIBETSU_GYOUSHU))]
    public interface CheckChiikibetsuGyoushuDaoCls : IS2Dao
    {
        /// <summary>
        /// 地域別業種マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckChiikibetsuGyoushu.sql")]
        new DataTable GetChiikiBetsuGyoushuCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 地域別施設マスタ
    /// </summary>
    [Bean(typeof(M_CHIIKIBETSU_SHISETSU))]
    public interface CheckChiikibetsuShisetsuDaoCls : IS2Dao
    {
        /// <summary>
        /// 地域別施設マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckChiikibetsuShisetsu.sql")]
        new DataTable GetChiikiBetsuShisetsuCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 地域別住所マスタ
    /// </summary>
    [Bean(typeof(M_CHIIKIBETSU_JUUSHO))]
    public interface CheckChiikibetsuJuushoDaoCls : IS2Dao
    {
        /// <summary>
        /// 地域別住所マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckChiikibetsuJuusho.sql")]
        new DataTable GetChiikiBetsuJuushoCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 地域別処分マスタ
    /// </summary>
    [Bean(typeof(M_CHIIKIBETSU_SHOBUN))]
    public interface CheckChiikibetsuShobunDaoCls : IS2Dao
    {
        /// <summary>
        /// 地域別処分マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckChiikibetsuShobun.sql")]
        new DataTable GetChiikiBetsuShobunCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 地域別分類マスタ
    /// </summary>
    [Bean(typeof(M_CHIIKIBETSU_BUNRUI))]
    public interface CheckChiikibetsuBunruiDaoCls : IS2Dao
    {
        /// <summary>
        /// 地域別分類マスタチェックデータ取得
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckChiikibetsuBunrui.sql")]
        new DataTable GetChiikiBetsuBunruiCheckData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// マニフェスト
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface CheckManifestDaoCls : IS2Dao
    {
        /// <summary>
        /// 交付番号チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiKofuBangou.sql")]
        new DataTable GetManiKofuBangouCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 交付年月日チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiKofuNengappi.sql")]
        new DataTable GetManiKofuNengappiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CDマニフェストチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoushaCD.sql")]
        new DataTable GetManiHaishutsuJigyoushaCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CD業者マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoushaCDGyoushaM.sql")]
        new DataTable GetManiHaishutsuJigyoushaCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CD業者マスタ_マニ記載業者チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoushaCDGyoushaMCheckMani.sql")]
        new DataTable GetManiHaishutsuJigyoushaCDGyoushaMCheckManiData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CD業者マスタ_業者分類（排出事業者）チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoushaCDGyoushaMCheckGyoushaBunrui.sql")]
        new DataTable GetManiHaishutsuJigyoushaCDGyoushaMCheckGyoushaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業場CDマニフェストチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoujouCD.sql")]
        new DataTable GetManiHaishutsuJigyoujouCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業場CD現場マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoujouCDGenbaM.sql")]
        new DataTable GetManiHaishutsuJigyoujouCDGenbaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業場CD現場マスタ_現場分類（排出事業場）チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaishutsuJigyoujouCDGenbaMCheckGenbaBunrui.sql")]
        new DataTable GetManiHaishutsuJigyoujouCDGenbaMCheckGenbaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 廃棄物種類CDチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiHaikiButsuShuruiCD.sql")]
        new DataTable GetManiHaikiButsuShuruiCDCheckData(SerchCheckManiDtoCls data);


        /// <summary>
        /// 換算後数量チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiKansangoSuuryou.sql")]
        new DataTable GetManiKansangoSuuryouCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分方法CDチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunHouhouCD.sql")]
        new DataTable GetManiShobunHouhouCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 地域処分方法CDチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiChiikiShobunHouhouCD.sql")]
        new DataTable GetManiChiikiShobunHouhouCDCheckData(SerchCheckManiDtoCls data);

        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        /// <summary>
        /// 数量チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiSuuryou.sql")]
        new DataTable GetManiSuuryouCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 単位CDチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiTaniCD.sql")]
        new DataTable GetManiTaniCDCheckData(SerchCheckManiDtoCls data);
        // 20140623 ria EV004852 一覧と抽出条件の変更 end

        /// <summary>
        /// 運搬受託者CDマニフェストチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiUnpanJutakushaCD.sql")]
        new DataTable GetManiUnpanJutakushaCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬受託者CD業者マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiUnpanJutakushaCDGyoushaM.sql")]
        new DataTable GetManiUnpanJutakushaCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬受託者CD業者マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiUnpanJutakushaCDGyoushaMCheckMani.sql")]
        new DataTable GetManiUnpanJutakushaCDGyoushaMCheckManiData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬受託者CD業者マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiUnpanJutakushaCDGyoushaMCheckGyoushaBunrui.sql")]
        new DataTable GetManiUnpanJutakushaCDGyoushaMCheckGyoushaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者CDマニフェストチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJutakushaCD.sql")]
        new DataTable GetManiShobunJutakushaCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者CD業者マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJutakushaCDGyoushaM.sql")]
        new DataTable GetManiShobunJutakushaCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者_マニ記載業者チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJutakushaCDGyoushaMCheckMani.sql")]
        new DataTable GetManiShobunJutakushaCDGyoushaMCheckManiData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者_業者分類（処分受託者）チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJutakushaCDGyoushaMCheckGyoushaBunrui.sql")]
        new DataTable GetManiShobunJutakushaCDGyoushaMCheckGyoushaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分事業場CDマニフェストチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJigyoujouCD.sql")]
        new DataTable GetManiShobunJigyoujouCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分事業場CD業者マスタチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJigyoujouCDGyoushaM.sql")]
        new DataTable GetManiShobunJigyoujouCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分事業場CD業者マスタチェックデータ取得(紙：建廃)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJigyoujouCDGyoushaMKP.sql")]
        new DataTable GetManiShobunJigyoujouCDGyoushaMKenpaiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬先の事業場CD_現場分類（運搬先事業場）チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJigyoujouCDGyoushaMCheckGenbaBunrui.sql")]
        new DataTable GetManiShobunJigyoujouCDGyoushaMCheckGenbaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬先の事業場CD_現場分類（運搬先事業場）チェックデータ取得(紙：建廃)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunJigyoujouCDGyoushaMKPCheckGenbaBunrui.sql")]
        new DataTable GetManiShobunJigyoujouCDGyoushaMKenpaiCheckGenbaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分施設のチェックデータ取得(紙：積替)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunShisetuTumikae.sql")]
        new DataTable GetManiShobunShisetuTumikaeCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬終了何月日チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiUnpanShuuryouNengappi.sql")]
        new DataTable GetManiUnpanShuuryouNengappiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分終了何月日チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiShobunShuuryouNengappi.sql")]
        new DataTable GetManiShobunShuuryouNengappiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 最終処分終了何月日チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiSaishuuShobunShuuryouNengappi.sql")]
        new DataTable GetManiSaishuuShobunShuuryouNengappiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 交付年月日なしチェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckManiKoufuNengappiNashi.sql")]
        new DataTable GetManiKoufuNengappiNashiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 紐付不整合チェックデータ取得(紙)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckHimodukeFuseigouKami.sql")]
        new DataTable GetHimodukeFuseigouKamiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// マニフェスト／予約番号チェックデータ取得(電子)表示順SEQ：10
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiManiID.sql")]
        new DataTable GetDenManiManiIDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 引き渡し日チェックデータ取得(電子)表示順SEQ：20
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHikiwatashiBi.sql")]
        new DataTable GetDenManiHikiwatashiBiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CD(マニフェスト)チェックデータ取得(電子)表示順SEQ：30
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoushaCDMani.sql")]
        new DataTable GetDenManiHaishutuJigyoushaCDManiCheckData(SerchCheckManiDtoCls data);
        
        /// <summary>
        /// 排出事業者CD(業者マスタ)チェックデータ取得(電子)表示順SEQ：31
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoushaCDGyoushaM.sql")]
        new DataTable GetDenManiHaishutuJigyoushaCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CD_マニ記載業者チェックデータ取得(電子)表示順SEQ：31
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoushaCDGyoushaMCheckMani.sql")]
        new DataTable GetDenManiHaishutuJigyoushaCDGyoushaMCheckManiData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業者CD_業者分類（排出事業者）チェックデータ取得(電子)表示順SEQ：31
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoushaCDGyoushaMCheckGyoushaBunrui.sql")]
        new DataTable GetDenManiHaishutuJigyoushaCDGyoushaMCheckGyoushaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業場CD(マニフェスト)チェックデータ取得(電子)表示順SEQ：40
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoujouCDMani.sql")]
        new DataTable GetDenManiHaishutuJigyoujouCDManiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業場CD(現場マスタ)チェックデータ取得(電子)表示順SEQ：41
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoujouCDGenbaM.sql")]
        new DataTable GetDenManiHaishutuJigyoujouCDGenbaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 排出事業場CD_現場分類（排出事業場）チェックデータ取得(電子)表示順SEQ：41
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaishutuJigyoujouCDGenbaMCheckGenbaBunrui.sql")]
        new DataTable GetDenManiHaishutuJigyoujouCDGenbaMCheckGenbaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 廃棄物種類(大分類)チェックデータ取得(電子)表示順SEQ：51
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaikibutuShuruiDai.sql")]
        new DataTable GetDenManiHaikibutuShuruiDaiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 廃棄物種類(中分類)チェックデータ取得(電子)表示順SEQ：52
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaikibutuShuruiChu.sql")]
        new DataTable GetDenManiHaikibutuShuruiChuCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 廃棄物種類(小分類)チェックデータ取得(電子)表示順SEQ：53
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaikibutuShuruiSho.sql")]
        new DataTable GetDenManiHaikibutuShuruiShoCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 廃棄物種類(細分類)チェックデータ取得(電子)表示順SEQ：54
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiHaikibutuShuruiSai.sql")]
        new DataTable GetDenManiHaikibutuShuruiSaiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 換算後数量チェックデータ取得(電子)表示順SEQ：60
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiKansanGoSuuryou.sql")]
        new DataTable GetDenManiKansanGoSuuryouCheckData(SerchCheckManiDtoCls data);
        
        /// <summary>
        /// 処分方法CDチェックデータ取得(電子)表示順SEQ：70
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunHouhouCD.sql")]
        new DataTable GetDenManiShobunHouhouCDCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 地域処分方法CDチェックデータ取得(電子)表示順SEQ：70
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiChiikiShobunHouhouCD.sql")]
        new DataTable GetDenManiChiikiShobunHouhouCDCheckData(SerchCheckManiDtoCls data);

        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        /// <summary>
        /// 数量チェックデータ取得(電子)表示順SEQ：61
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiSuuryou.sql")]
        new DataTable GetDenManiSuuryouCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 単位CDチェックデータ取得(電子)表示順SEQ：71
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiTaniCD.sql")]
        new DataTable GetDenManiTaniCDCheckData(SerchCheckManiDtoCls data);
        // 20140623 ria EV004852 一覧と抽出条件の変更 end

        /// <summary>
        /// 収集運搬業者CD(マニフェスト)チェックデータ取得(電子)表示順SEQ：80
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShuushuuUnpanGyousyaCDMani.sql")]
        new DataTable GetDenManiShuushuuUnpanGyousyaCDManiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 収集運搬業者CD(業者マスタ)チェックデータ取得(電子)表示順SEQ：81
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShuushuuUnpanGyousyaCDGyoushaM.sql")]
        new DataTable GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 収集運搬業者CD_マニ記載業者チェックデータ取得(電子)表示順SEQ：81
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShuushuuUnpanGyousyaCDGyoushaMCheckMani.sql")]
        new DataTable GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckManiData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 収集運搬業者CD_業者分類（運搬受託者）チェックデータ取得(電子)表示順SEQ：81
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui.sql")]
        new DataTable GetDenManiShuushuuUnpanGyousyaCDGyoushaMCheckGyoushaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者CD(マニフェスト)チェックデータ取得(電子)表示順SEQ：90
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJutakushaCDMani.sql")]
        new DataTable GetDenManiShobunJutakushaCDManiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者CD(業者マスタ)チェックデータ取得(電子)表示順SEQ：91
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJutakushaCDGyoushaM.sql")]
        new DataTable GetDenManiShobunJutakushaCDGyoushaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者CD_マニ記載業者チェックデータ取得(電子)表示順SEQ：91
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJutakushaCDGyoushaMCheckMani.sql")]
        new DataTable GetDenManiShobunJutakushaCDGyoushaMCheckManiData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分受託者CD業者分類（処分受託者）チェックデータ取得(電子)表示順SEQ：91
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJutakushaCDGyoushaMCheckGyoushaBunrui.sql")]
        new DataTable GetDenManiShobunJutakushaCDGyoushaMCheckGyoushaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分事業場CD(マニフェスト)チェックデータ取得(電子)表示順SEQ：100
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJigyoujouCDMani.sql")]
        new DataTable GetDenManiShobunJigyoujouCDManiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分事業場CD(業者マスタ)チェックデータ取得(電子)表示順SEQ：101
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJigyoujouCDGembaM.sql")]
        new DataTable GetDenManiShobunJigyoujouCDGembaMCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分事業場CD_現場分類（運搬先事業場）チェックデータ取得(電子)表示順SEQ：101
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui.sql")]
        new DataTable GetDenManiShobunJigyoujouCDGembaMCheckGenbaBunrui(SerchCheckManiDtoCls data);

        /// <summary>
        /// 運搬終了日チェックデータ取得(電子)表示順SEQ：110
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiUnpanshuuryouBi.sql")]
        new DataTable GetDenManiUnpanshuuryouBiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 処分終了日チェックデータ取得(電子)表示順SEQ：120
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiShobunshuuryouBi.sql")]
        new DataTable GetDenManiShobunshuuryouBiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 最終処分終了日チェックデータ取得(電子)表示順SEQ：130
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiSaishuuShobunshuuryouBi.sql")]
        new DataTable GetDenManiSaishuuShobunshuuryouBiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 交付年月日なしチェックデータ取得(電子)表示順SEQ：210
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckDenManiKoufuNengappiNashi.sql")]
        new DataTable GetDenManiKoufuNengappiNashiCheckData(SerchCheckManiDtoCls data);

        /// <summary>
        /// 紐付不整合チェックデータ取得(電子)表示順SEQ：310
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.CheckHimodukeFuseigouDenshi.sql")]
        new DataTable GetHimodukeFuseigouDenshiCheckData(SerchCheckManiDtoCls data);

        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        /// <summary>
        /// チェックデータSYSTEM_ID取得(紙マニ)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.SearchManiSystemID.sql")]
        new DataTable GetManiSystemID(SerchCheckManiDtoCls data);

        /// <summary>
        /// チェックデータSYSTEM_ID取得(電子マニ)
        /// </summary>
        [SqlFile("Shougun.Core.PaperManifest.ManifestCheckHyo.Sql.SearchDenManiSystemID.sql")]
        new DataTable GetDenManiSystemID(SerchCheckManiDtoCls data);
        // 20140623 ria EV004852 一覧と抽出条件の変更 end
    }
}
