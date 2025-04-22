SELECT
    --①－１
    --マニフェスト番号
    R18.MANIFEST_ID,
    --登録の状態
    CASE
        WHEN TOC.STATUS_FLAG IS NULL OR TOC.STATUS_DETAIL IS NULL THEN ''
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '10' THEN '予約(システム)'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '11' THEN '予約未送信'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '21' THEN '登録未送信'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '30' THEN '予約(JW)'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '32' THEN '予約(JW)-修正/取消'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '31' THEN '確定中'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '40' THEN '登録'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '42' THEN '登録-修正/取消'
        WHEN LTRIM(RTRIM(STR(TOC.STATUS_FLAG))) + LTRIM(RTRIM(STR(TOC.STATUS_DETAIL))) = '99' THEN '無効データ'
        ELSE ''
    END AS REGIST_STATUS,
    --引渡し日
    CASE
        WHEN R18.HIKIWATASHI_DATE is null THEN ''
        WHEN LTRIM(RTRIM(R18.HIKIWATASHI_DATE))= '' THEN ''
        ELSE SUBSTRING(R18.HIKIWATASHI_DATE,1,4) + '/' +
            SUBSTRING(R18.HIKIWATASHI_DATE,5,2) + '/' +
            SUBSTRING(R18.HIKIWATASHI_DATE,7,2)
    END AS HIKIWATASHI_DATE,
    --引渡し担当者
    R18.HIKIWATASHI_TAN_NAME,
    --連絡番号１
    R05_1.RENRAKU_ID AS RENRAKU_ID1,
    --連絡番号２
    R05_2.RENRAKU_ID AS RENRAKU_ID2,
    --連絡番号３
    R05_3.RENRAKU_ID AS RENRAKU_ID3,
    --①－２
    --排出事業者名
    R18.HST_SHA_NAME,
    --排出事業者郵便番号
    CASE
        WHEN R18.HST_SHA_POST IS NULL THEN ''
        ELSE '〒' + R18.HST_SHA_POST
    END AS HST_SHA_POST,
    --排出事業者住所
    CASE
        WHEN R18.HST_SHA_ADDRESS1 IS NULL THEN ''
        ELSE R18.HST_SHA_ADDRESS1
    END +
    CASE
        WHEN R18.HST_SHA_ADDRESS2 IS NULL THEN ''
        ELSE R18.HST_SHA_ADDRESS2
    END +
    CASE
        WHEN R18.HST_SHA_ADDRESS3 IS NULL THEN ''
        ELSE R18.HST_SHA_ADDRESS3
    END +
    CASE
        WHEN R18.HST_SHA_ADDRESS4 IS NULL THEN ''
        ELSE R18.HST_SHA_ADDRESS4
    END  AS HST_SHA_ADDRESS,
    --排出事業者電話番号
    R18.HST_SHA_TEL,
    --排出事業者加入者番号
    R18.HST_SHA_EDI_MEMBER_ID,
    --排出事業場名称
    R18.HST_JOU_NAME,
    --排出事業場郵便番号
    CASE
        WHEN R18.HST_JOU_POST_NO IS NULL THEN ''
        ELSE '〒' + R18.HST_JOU_POST_NO
    END AS HST_JOU_POST_NO,
    --排出事業場所在地
    CASE
        WHEN R18.HST_JOU_ADDRESS1 IS NULL THEN ''
        ELSE R18.HST_JOU_ADDRESS1
    END +
    CASE
        WHEN R18.HST_JOU_ADDRESS2 IS NULL THEN ''
        ELSE R18.HST_JOU_ADDRESS2
    END +
    CASE
        WHEN R18.HST_JOU_ADDRESS3 IS NULL THEN ''
        ELSE R18.HST_JOU_ADDRESS3
    END +
    CASE
        WHEN R18.HST_JOU_ADDRESS4 IS NULL THEN ''
        ELSE R18.HST_JOU_ADDRESS4
    END  AS HST_JOU_ADDRESS,
    --排出事業場電話番号
    R18.HST_JOU_TEL,
    --①－３
    --産業廃棄物種類CD
        CASE
        WHEN R18.HAIKI_DAI_CODE IS NULL THEN ''
        ELSE R18.HAIKI_DAI_CODE
    END +
    CASE
        WHEN R18.HAIKI_CHU_CODE IS NULL THEN ''
        ELSE R18.HAIKI_CHU_CODE
    END +
    CASE
        WHEN R18.HAIKI_SHO_CODE IS NULL THEN ''
        ELSE R18.HAIKI_SHO_CODE
    END +
    CASE
        WHEN R18.HAIKI_SAI_CODE IS NULL THEN ''
        ELSE R18.HAIKI_SAI_CODE
    END  AS HAIKI_CODE,
    --産業廃棄物種類
    R18.HAIKI_SHURUI,
    --産業廃棄物大分類名称
    R18.HAIKI_BUNRUI,
    --産業廃棄物有害物質名称CD1
    R02_1.YUUGAI_CODE AS YUUGAI_CODE1,
    --産業廃棄物有害物質名称1
    R02_1.YUUGAI_NAME AS YUUGAI_NAME1,
    --産業廃棄物有害物質名称CD2
    R02_2.YUUGAI_CODE AS YUUGAI_CODE2,
    --産業廃棄物有害物質名称2
    R02_2.YUUGAI_NAME AS YUUGAI_NAME2,
    --産業廃棄物有害物質名称CD3
    R02_3.YUUGAI_CODE AS YUUGAI_CODE3,
    --産業廃棄物有害物質名称3
    R02_3.YUUGAI_NAME AS YUUGAI_NAME3,
    --産業廃棄物有害物質名称他件数
    CASE
        WHEN R02_SONOTA.CNT IS NULL THEN ''
        WHEN R02_SONOTA.CNT =0 THEN '他' + '０' + '件'
		WHEN R02_SONOTA.CNT =1 THEN '他' + '１' + '件'
		WHEN R02_SONOTA.CNT =2 THEN '他' + '２' + '件'
		WHEN R02_SONOTA.CNT =3 THEN '他' + '３' + '件'
    END AS SONOTA_CNT,
    --廃棄物の名称
    R18.HAIKI_NAME,
    --数量
    R18.HAIKI_SUU,
    --数量単位
    SUU_UNIT.UNIT_NAME AS HAIKI_UNIT_NAME,
    --荷姿
    R18.NISUGATA_SUU,
    --荷姿単位
    R18.NISUGATA_NAME,
    --確定数量
    R18.HAIKI_KAKUTEI_SUU,
    --確定数量単位
    KAKUTEI_UNIT.UNIT_NAME AS HAIKI_KAKUTEI_UNIT_NAME,
    --数量の確定者
    CASE
        WHEN SUU_KAKUTEI_CODE IS NULL THEN ''
        WHEN SUU_KAKUTEI_CODE = '01' THEN '排出事業者'
        WHEN SUU_KAKUTEI_CODE = '02' THEN '処分業者'
        WHEN SUU_KAKUTEI_CODE = '03' THEN '収集運搬業者（区間1）'
        WHEN SUU_KAKUTEI_CODE = '04' THEN '収集運搬業者（区間2）'
        WHEN SUU_KAKUTEI_CODE = '05' THEN '収集運搬業者（区間3）'
        WHEN SUU_KAKUTEI_CODE = '06' THEN '収集運搬業者（区間4）'
        WHEN SUU_KAKUTEI_CODE = '07' THEN '収集運搬業者（区間5）'
        ELSE ''
    END AS HAIKI_KAKUTEI_UNIT_NM,
    --①－４
    --中間処理産業廃棄物
    '' AS FIRST_MANIFEST,
    --①－９
    --中間処理産業廃棄物(２行目
    '' AS FIRST_MANIFEST_2,
    --①－５
    --最終処分場所（予定）
    '' AS LAST_SBN_JOU_YOTEI,
    --①－６
    --収集運搬業者区間名
    CASE
        WHEN DT_R19.UPN_ROUTE_NO = 1 THEN '区間１'
        WHEN DT_R19.UPN_ROUTE_NO = 2 THEN '区間２'
        WHEN DT_R19.UPN_ROUTE_NO = 3 THEN '区間３'
        WHEN DT_R19.UPN_ROUTE_NO = 4 THEN '区間４'
        WHEN DT_R19.UPN_ROUTE_NO = 5 THEN '区間５'
        ELSE ''
    END AS UPN_ROUTE_NM,
    --収集運搬業者区間１名称
    DT_R19.UPN_SHA_NAME,
    --収集運搬業者区間１郵便番号
    DT_R19.UPN_SHA_POST,
    --収集運搬業者区間１住所
    CASE
        WHEN DT_R19.UPN_SHA_ADDRESS1 IS NULL THEN ''
        ELSE DT_R19.UPN_SHA_ADDRESS1
    END +
    CASE
        WHEN DT_R19.UPN_SHA_ADDRESS2 IS NULL THEN ''
        ELSE DT_R19.UPN_SHA_ADDRESS2
    END +
    CASE
        WHEN DT_R19.UPN_SHA_ADDRESS3 IS NULL THEN ''
        ELSE DT_R19.UPN_SHA_ADDRESS3
    END +
    CASE
        WHEN DT_R19.UPN_SHA_ADDRESS4 IS NULL THEN ''
        ELSE DT_R19.UPN_SHA_ADDRESS4
    END  AS UPN_SHA_ADDRESS,
    --収集運搬業者区間１電話番号
    DT_R19.UPN_SHA_TEL,
    --収集運搬業者区間１加入者番号
    DT_R19.UPN_SHA_EDI_MEMBER_ID,
    --収集運搬業者区間１許可番号
    DT_R19.UPN_SHA_KYOKA_ID,
    --収集運搬業者区間１備考
    DT_R19.BIKOU,
    --運搬先の事業場名称
    DT_R19.UPNSAKI_JOU_NAME,
    --運搬先の事業場所郵便番号
    CASE
        WHEN DT_R19.UPNSAKI_JOU_POST IS NULL THEN ''
        ELSE '〒' + DT_R19.UPNSAKI_JOU_POST
    END AS UPNSAKI_JOU_POST,
    --運搬先の事業場所在地
    CASE
        WHEN DT_R19.UPNSAKI_JOU_ADDRESS1 IS NULL THEN ''
        ELSE DT_R19.UPNSAKI_JOU_ADDRESS1
    END +
    CASE
        WHEN DT_R19.UPNSAKI_JOU_ADDRESS2 IS NULL THEN ''
        ELSE DT_R19.UPNSAKI_JOU_ADDRESS2
    END +
    CASE
        WHEN DT_R19.UPNSAKI_JOU_ADDRESS3 IS NULL THEN ''
        ELSE DT_R19.UPNSAKI_JOU_ADDRESS3
    END +
    CASE
        WHEN DT_R19.UPNSAKI_JOU_ADDRESS4 IS NULL THEN ''
        ELSE DT_R19.UPNSAKI_JOU_ADDRESS4
    END  AS UPNSAKI_JOU_ADDRESS,
    --運搬先の事業場電話番号
    DT_R19.UPNSAKI_JOU_TEL,
    --運搬方法
    M_UNPAN_HOUHOU.UNPAN_HOUHOU_NAME AS UPN_WAY_NAME,
    --車両番号（排出）
    DT_R19.CAR_NO,
    --運搬量
    DT_R19.UPN_SUU,
    --運搬量単位
    UPN_UNIT.UNIT_NAME AS UPN_UNIT_NAME,
    --有価物拾集量
    DT_R19.YUUKA_SUU,
    --有価物拾集量単位
    YUUKA_UNIT.UNIT_NAME AS YUUKA_UNIT_NAME,
    --運搬担当者
    CASE 
		WHEN DT_R19.UPN_END_DATE IS NULL THEN ''
		WHEN DT_R19.UPN_END_DATE = '' THEN ''
		ELSE DT_R19.UPNREP_UPN_TAN_NAME END AS UPN_TAN_NAME,
    --運搬終了日
    CASE
        WHEN DT_R19.UPN_END_DATE is null THEN ''
        WHEN LTRIM(RTRIM(DT_R19.UPN_END_DATE))= '' THEN ''
        ELSE SUBSTRING(DT_R19.UPN_END_DATE,1,4) + '/' +
            SUBSTRING(DT_R19.UPN_END_DATE,5,2) + '/' +
            SUBSTRING(DT_R19.UPN_END_DATE,7,2)
    END AS UPN_END_DATE,
    --最終処分事業場記載フラグ
    CASE
        WHEN R18.LAST_SBN_JOU_KISAI_FLAG IS NULL THEN -1
        ELSE R18.LAST_SBN_JOU_KISAI_FLAG
    END AS LAST_SBN_JOU_KISAI_FLAG,
    --①－７
    --処分業者名称
    R18.SBN_SHA_NAME,
    --処分業者郵便番号
    CASE
        WHEN R18.SBN_SHA_POST IS NULL THEN ''
        ELSE '〒' + R18.SBN_SHA_POST
    END AS SBN_SHA_POST,
    --処分業者住所
    CASE
        WHEN R18.SBN_SHA_ADDRESS1 IS NULL THEN ''
        ELSE R18.SBN_SHA_ADDRESS1
    END +
    CASE
        WHEN R18.SBN_SHA_ADDRESS2 IS NULL THEN ''
        ELSE R18.SBN_SHA_ADDRESS2
    END +
    CASE
        WHEN R18.SBN_SHA_ADDRESS3 IS NULL THEN ''
        ELSE R18.SBN_SHA_ADDRESS3
    END +
    CASE
        WHEN R18.SBN_SHA_ADDRESS4 IS NULL THEN ''
        ELSE R18.SBN_SHA_ADDRESS4
    END AS SBN_SHA_ADDRESS,
    --処分業者電話番号
    R18.SBN_SHA_TEL    ,
    --処分業者加入番号
    R18.SBN_SHA_MEMBER_ID,
    --処分業者許可番号
    R18.SBN_SHA_KYOKA_ID,
    --処分業者備考
    R18.SBN_REP_BIKOU,
    --処分事業場名称
    MAXR19.UPNSAKI_JOU_NAME AS MAX_UPNSAKI_JOU_NAME,
    --処分事業場郵便番号
    CASE
        WHEN MAXR19.UPNSAKI_JOU_POST IS NULL THEN ''
        ELSE '〒' + MAXR19.UPNSAKI_JOU_POST
    END AS MAX_UPNSAKI_JOU_POST,
    --処分事業場所在地
    CASE
        WHEN MAXR19.UPNSAKI_JOU_ADDRESS1 IS NULL THEN ''
        ELSE MAXR19.UPNSAKI_JOU_ADDRESS1
    END +
    CASE
        WHEN MAXR19.UPNSAKI_JOU_ADDRESS2 IS NULL THEN ''
        ELSE MAXR19.UPNSAKI_JOU_ADDRESS2
    END +
    CASE
        WHEN MAXR19.UPNSAKI_JOU_ADDRESS3 IS NULL THEN ''
        ELSE MAXR19.UPNSAKI_JOU_ADDRESS3
    END +
    CASE
        WHEN MAXR19.UPNSAKI_JOU_ADDRESS4 IS NULL THEN ''
        ELSE MAXR19.UPNSAKI_JOU_ADDRESS4
    END AS MAX_UPNSAKI_JOU_ADDRESS,
    --処分事業場電話番号
    MAXR19.UPNSAKI_JOU_TEL AS MAX_UPNSAKI_JOU_TEL,
    --処分方法
    R18.SBN_WAY_NAME,
    --報告区分
    CASE
        WHEN R18.SBN_ENDREP_KBN IS NULL THEN ''
        WHEN R18.SBN_ENDREP_KBN = 1 THEN '処分（中間）'
        WHEN R18.SBN_ENDREP_KBN = 2 THEN '処分（最終）'
    END AS SBN_ENDREP_KBN_NAME,
    --処分終了日
    CASE
        WHEN R18.SBN_END_DATE is null THEN ''
        WHEN LTRIM(RTRIM(R18.SBN_END_DATE))= '' THEN ''
        ELSE SUBSTRING(R18.SBN_END_DATE,1,4) + '/' +
             SUBSTRING(R18.SBN_END_DATE,5,2) + '/' +
             SUBSTRING(R18.SBN_END_DATE,7,2)
    END AS SBN_END_DATE,
    --廃棄物受領日 HAIKI_IN_DATE
    CASE
        WHEN R18.HAIKI_IN_DATE is null THEN ''
        WHEN LTRIM(RTRIM(R18.HAIKI_IN_DATE))= '' THEN ''
        ELSE SUBSTRING(R18.HAIKI_IN_DATE,1,4) + '/' +
             SUBSTRING(R18.HAIKI_IN_DATE,5,2) + '/' +
             SUBSTRING(R18.HAIKI_IN_DATE,7,2)
    END AS HAIKI_IN_DATE,
    --処分担当者
    R18.SBN_TAN_NAME,
    --受入量
    R18.RECEPT_SUU,
    --受入量単位
    RECEPT_UNIT.UNIT_NAME AS RECEPT_UNIT_NAME,
    --①－８
    --最終処分の場所（実績）
    '' AS LAST_SBN_JOU_JISSEKI,
    --最終処分終了日
    CASE
        WHEN R18.LAST_SBN_END_DATE is null THEN ''
        WHEN LTRIM(RTRIM(R18.LAST_SBN_END_DATE))= '' THEN ''
        ELSE SUBSTRING(R18.LAST_SBN_END_DATE,1,4) + '/' +
             SUBSTRING(R18.LAST_SBN_END_DATE,5,2) + '/' +
             SUBSTRING(R18.LAST_SBN_END_DATE,7,2)
    END AS LAST_SBN_END_DATE,
    --備考１
    R06_1.BIKOU AS BIKOU1,
    --備考２
    R06_2.BIKOU AS BIKOU2,
    --備考３
    R06_3.BIKOU AS BIKOU3,
    --備考４
    R06_4.BIKOU AS BIKOU4,
    --備考５
    R06_5.BIKOU AS BIKOU5
  --マニフェスト目次情報
  FROM DT_MF_TOC TOC

    --INNER JOIN R18 マニフェスト情報
    --  ON マニフェスト目次情報.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND マニフェスト目次情報.最新SEQ　＝　R18 マニフェスト情報.枝番
    INNER JOIN DT_R18 R18 ON TOC.KANRI_ID = R18.KANRI_ID
                         AND TOC.LATEST_SEQ = R18.SEQ

    --LEFT JOIN R02 有害物質情報1
    --  ON R02 有害物質情報1.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R02 有害物質情報1.最新SEQ　＝　R18 マニフェスト情報.枝番
    --  AND R02 有害物質情報1.レコード連番　＝　1
    LEFT JOIN DT_R02 R02_1 ON R02_1.KANRI_ID = R18.KANRI_ID
                          AND R02_1.SEQ = R18.SEQ
                          AND R02_1.REC_SEQ = 1

    --LEFT JOIN R02 有害物質情報2
    --  ON R02 有害物質情報2.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R02 有害物質情報2.最新SEQ　＝　R18 マニフェスト情報.枝番
    --  AND R02 有害物質情報2.レコード連番　＝　2
    LEFT JOIN DT_R02 R02_2 ON R02_2.KANRI_ID = R18.KANRI_ID
                          AND R02_2.SEQ = R18.SEQ
                          AND R02_2.REC_SEQ = 2

    --LEFT JOIN R02 有害物質情報3
    --  ON R02 有害物質情報3.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R02 有害物質情報3.最新SEQ　＝　R18 マニフェスト情報.枝番
    --  AND R02 有害物質情報3.レコード連番　＝　3
    LEFT JOIN DT_R02 R02_3 ON R02_3.KANRI_ID = R18.KANRI_ID
                          AND R02_3.SEQ = R18.SEQ
                          AND R02_3.REC_SEQ = 3

    --LEFT JOIN R02 有害物質情報4
    --  ON R02 有害物質情報4.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R02 有害物質情報4.最新SEQ　＝　R18 マニフェスト情報.枝番
    --  AND R02 有害物質情報4.レコード連番　＝　4,5,6
    LEFT JOIN  (SELECT COUNT(0) AS CNT,
                       KANRI_ID,
                       SEQ
                FROM DT_R02 R02_4
                         WHERE ( R02_4.REC_SEQ = 4
                            OR   R02_4.REC_SEQ = 5
                            OR   R02_4.REC_SEQ = 6 )
                          GROUP BY KANRI_ID,SEQ
                ) R02_SONOTA
        ON R02_SONOTA.KANRI_ID= R18.KANRI_ID
        AND R02_SONOTA.SEQ = R18.SEQ
    --LEFT JOIN 収集運搬情報(区間１)
    --  ON R18 収集運搬情報(区間１).管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND 収集運搬情報(区間１).最新SEQ　＝　R18 マニフェスト情報.枝番
    --  AND 収集運搬情報(区間１).区間番号　＝　1～5
    LEFT JOIN DT_R19  ON DT_R19.KANRI_ID = R18.KANRI_ID
                     AND DT_R19.SEQ = R18.SEQ
                     AND ( DT_R19.UPN_ROUTE_NO = 1
                     OR   DT_R19.UPN_ROUTE_NO = 2
                     OR   DT_R19.UPN_ROUTE_NO = 3
                     OR   DT_R19.UPN_ROUTE_NO = 4
                     OR   DT_R19.UPN_ROUTE_NO = 5 )

    --LEFT JOIN R05 連絡番号情報1
    --  ON R05 連絡番号情報1.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R05 連絡番号情報1.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R05 連絡番号情報1.連絡番号No　＝　1
    LEFT JOIN DT_R05 R05_1 ON R05_1.KANRI_ID = R18.KANRI_ID
                          AND R05_1.SEQ = R18.SEQ
                          AND R05_1.RENRAKU_ID_NO = 1

    --LEFT JOIN R05 連絡番号情報2
    --  ON R05 連絡番号情報2.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R05 連絡番号情報2.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R05 連絡番号情報2.連絡番号No　＝　2
    LEFT JOIN DT_R05 R05_2 ON R05_2.KANRI_ID = R18.KANRI_ID
                          AND R05_2.SEQ = R18.SEQ
                          AND R05_2.RENRAKU_ID_NO = 2

    --LEFT JOIN R05 連絡番号情報3
    --  ON R05 連絡番号情報3.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R05 連絡番号情報3.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R05 連絡番号情報3.連絡番号No　＝　3
    LEFT JOIN DT_R05 R05_3 ON R05_3.KANRI_ID = R18.KANRI_ID
                          AND R05_3.SEQ = R18.SEQ
                          AND R05_3.RENRAKU_ID_NO = 3

    --LEFT JOIN R06 備考情報１
    --  ON R06 備考情報１.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R06 備考情報１.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R06 備考情報１.連絡番号No　＝　1
    LEFT JOIN DT_R06 R06_1 ON R06_1.KANRI_ID = R18.KANRI_ID
                          AND R06_1.SEQ = R18.SEQ
                          AND R06_1.BIKOU_NO = 1

    --LEFT JOIN R06 備考情報２
    --  ON R06 備考情報２.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R06 備考情報２.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R06 備考情報２.連絡番号No　＝　2
    LEFT JOIN DT_R06 R06_2 ON R06_2.KANRI_ID = R18.KANRI_ID
                          AND R06_2.SEQ = R18.SEQ
                          AND R06_2.BIKOU_NO = 2

    --LEFT JOIN R06 備考情報３
    --  ON R06 備考情報３.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R06 備考情報３.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R06 備考情報３.連絡番号No　＝　3
    LEFT JOIN DT_R06 R06_3 ON R06_3.KANRI_ID = R18.KANRI_ID
                          AND R06_3.SEQ = R18.SEQ
                          AND R06_3.BIKOU_NO = 3

    --LEFT JOIN R06 備考情報４
    --  ON R06 備考情報４.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R06 備考情報４.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R06 備考情報４.連絡番号No　＝　4
    LEFT JOIN DT_R06 R06_4 ON R06_4.KANRI_ID = R18.KANRI_ID
                          AND R06_4.SEQ = R18.SEQ
                          AND R06_4.BIKOU_NO = 4

    --LEFT JOIN R06 備考情報５
    --  ON R06 備考情報５.管理番号　＝　R18 マニフェスト情報.管理番号
    --  AND R06 備考情報５.枝番　＝　R18 マニフェスト情報.枝番
    --  AND R06 備考情報５.連絡番号No　＝　5
    LEFT JOIN DT_R06 R06_5 ON R06_5.KANRI_ID = R18.KANRI_ID
                          AND R06_5.SEQ = R18.SEQ
                          AND R06_5.BIKOU_NO = 5
    --単位マスタ（数量単位）
    LEFT JOIN M_UNIT SUU_UNIT ON R18.HAIKI_UNIT_CODE = SUU_UNIT.UNIT_CD
    --単位マスタ（確定数量）
    LEFT JOIN M_UNIT KAKUTEI_UNIT ON R18.HAIKI_KAKUTEI_UNIT_CODE = KAKUTEI_UNIT.UNIT_CD
    --単位マスタ（運搬量）
    LEFT JOIN M_UNIT UPN_UNIT ON DT_R19.UPN_UNIT_CODE = UPN_UNIT.UNIT_CD
    --単位マスタ（有価物拾集量）
    LEFT JOIN M_UNIT YUUKA_UNIT ON DT_R19.YUUKA_UNIT_CODE = YUUKA_UNIT.UNIT_CD
    --単位マスタ（受入量）
    LEFT JOIN M_UNIT RECEPT_UNIT ON R18.RECEPT_UNIT_CODE = RECEPT_UNIT.UNIT_CD
    --運搬方法名を取得
    LEFT JOIN M_UNPAN_HOUHOU ON DT_R19.UPN_WAY_CODE = M_UNPAN_HOUHOU.UNPAN_HOUHOU_CD
    --収集運搬情報.区間番号の最大値の情報
    LEFT JOIN DT_R19 AS MAXR19 ON MAXR19.KANRI_ID = R18.KANRI_ID
                              AND MAXR19.SEQ = R18.SEQ
                              AND MAXR19.UPN_ROUTE_NO = ( SELECT DISTINCT MAX(UPN_ROUTE_NO)
                                                         FROM DT_R19
                                                         WHERE DT_R19.KANRI_ID = R18.KANRI_ID
                                                           AND DT_R19.SEQ = R18.SEQ )

  --マニフェスト目次情報.管理番号　＝　画面で選択した行の管理番号
  --マニフェスト目次情報.状態フラグ　<> 1:削除
 WHERE TOC.KANRI_ID = /*data.kanriId*/
   AND TOC.STATUS_FLAG <> 9

