SELECT 

mt.*

FROM dbo.T_TEIKI_JISSEKI_DETAIL AS tjd

-- 定期実績入力 --
INNER JOIN dbo.T_TEIKI_JISSEKI_ENTRY AS tje
    ON tje.SYSTEM_ID = tjd.SYSTEM_ID
    AND tje.SEQ = tjd.SEQ
    AND tje.DELETE_FLG = 0

    -- [入力条件] --
    /*IF data.KYOTEN_CD != null */ 
    AND tje.KYOTEN_CD =  /*data.KYOTEN_CD*/0 /*END*/

    --  [2014/01/29] DENPYOU_DATE->SAGYOU_DATE--
    AND CONVERT(nvarchar, tje.SAGYOU_DATE, 111) >=  CONVERT(nvarchar, /*data.KIKAN_DATE_FROM*/null, 111)
    AND CONVERT(nvarchar, tje.SAGYOU_DATE, 111) <=  CONVERT(nvarchar, /*data.KIKAN_DATE_TO*/null, 111)

-- 定期実績荷降 --
LEFT JOIN dbo.T_TEIKI_JISSEKI_NIOROSHI AS tjn
    ON tjn.SYSTEM_ID = tjd.SYSTEM_ID
    AND tjn.SEQ = tjd.SEQ
	AND tjn.NIOROSHI_NUMBER = tjd.NIOROSHI_NUMBER

-- 現場マスタ --
INNER JOIN dbo.M_GENBA AS mg
    ON mg.GYOUSHA_CD = tjd.GYOUSHA_CD
    AND mg.GENBA_CD = tjd.GENBA_CD

-- 取引先マスタ --
INNER JOIN dbo.M_TORIHIKISAKI AS mt
    ON mt.TORIHIKISAKI_CD = mg.TORIHIKISAKI_CD

-- 取引先請求情報マスタ --
INNER JOIN dbo.M_TORIHIKISAKI_SEIKYUU AS ts
    ON ts.TORIHIKISAKI_CD = mg.TORIHIKISAKI_CD

-- 取引先支払情報マスタ --
INNER JOIN dbo.M_TORIHIKISAKI_SHIHARAI as tsh
	ON tsh.TORIHIKISAKI_CD = mg.TORIHIKISAKI_CD

WHERE 
-- 月極区分：1(伝票)／2(合算) --
tjd.TSUKIGIME_KBN IN( 1 , 2 )
-- 取引先
    /*IF data.TORIHIKISAKI_CD_custom != null */ 
    AND mt.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD_custom*/'000000'
    /*END*/
-- 売上支払伝票：0(未確定) 固定 [2014/01/29] 0->NULLに変更 --
-- [2014/02/20] NULL->画面の条件によって動的に変更 --
/*IF data.FIX_CONDITION_VALUE == 2 */ 
AND tjd.KAKUTEI_FLG  = 0
/*END*/
/*IF data.FIX_CONDITION_VALUE != 2 */ 
AND tjd.KAKUTEI_FLG  = 1
/*END*/
-- 契約区分：2固定(1:定期、2単価) [2014/01/25] 追加--
AND tjd.KEIYAKU_KBN = 2
	
-- [入力条件] --
/*IF data.SHIMEBI != null */ 
AND (
	-- 伝票区分：売上、支払以外
	(
	  (tjd.DENPYOU_KBN_CD != '1' AND tjd.DENPYOU_KBN_CD != '2')
	  OR tjd.DENPYOU_KBN_CD is null
	)
	AND  (
	  -- 請求情報絞込み --
	  (ts.SHIMEBI1 = /*data.SHIMEBI*/0 )
	  OR
	  -- 支払情報絞込み --
	  (tsh.SHIMEBI1 = /*data.SHIMEBI*/0 )
	)
)
/*END*/

/*IF data.SHIMEBI == null */ 
AND (
	-- 伝票区分：売上、支払以外
	(
	  (tjd.DENPYOU_KBN_CD != '1' AND tjd.DENPYOU_KBN_CD != '2')
	  OR tjd.DENPYOU_KBN_CD is null
	)
	AND  (
	  -- 請求情報絞込み --
	  (ts.TORIHIKI_KBN_CD = 1 )
	  OR
	  -- 支払情報絞込み --
	  (tsh.TORIHIKI_KBN_CD = 1 )
	)
)
/*END*/
AND EXISTS (SELECT 1 FROM T_TEIKI_JISSEKI_DETAIL CHECK_TABLE 
			WHERE CHECK_TABLE.SUURYOU IS NOT NULL 
			AND CHECK_TABLE.SYSTEM_ID = tjd.SYSTEM_ID 
			AND CHECK_TABLE.SEQ = tjd.SEQ 
			AND CHECK_TABLE.DETAIL_SYSTEM_ID = tjd.DETAIL_SYSTEM_ID)

UNION

SELECT 

mt.*

FROM dbo.T_TEIKI_JISSEKI_DETAIL AS tjd

-- 定期実績入力 --
INNER JOIN dbo.T_TEIKI_JISSEKI_ENTRY AS tje
    ON tje.SYSTEM_ID = tjd.SYSTEM_ID
    AND tje.SEQ = tjd.SEQ
    AND tje.DELETE_FLG = 0
    /*IF data.KYOTEN_CD != null */ AND tje.KYOTEN_CD =  /*data.KYOTEN_CD*/0 /*END*/
    AND CONVERT(nvarchar, tje.SAGYOU_DATE, 111) >=  CONVERT(nvarchar, /*data.KIKAN_DATE_FROM*/null, 111)
    AND CONVERT(nvarchar, tje.SAGYOU_DATE, 111) <=  CONVERT(nvarchar, /*data.KIKAN_DATE_TO*/null, 111)

-- 定期実績荷降 --
LEFT JOIN dbo.T_TEIKI_JISSEKI_NIOROSHI AS tjn
    ON tjn.SYSTEM_ID = tjd.SYSTEM_ID
    AND tjn.SEQ = tjd.SEQ
	AND tjn.NIOROSHI_NUMBER = tjd.NIOROSHI_NUMBER

-- 現場マスタ --
INNER JOIN dbo.M_GENBA AS mg
    ON mg.GYOUSHA_CD = tjd.GYOUSHA_CD
    AND mg.GENBA_CD = tjd.GENBA_CD

-- 取引先マスタ --
INNER JOIN dbo.M_TORIHIKISAKI AS mt
    ON mt.TORIHIKISAKI_CD = mg.TORIHIKISAKI_CD

-- 取引先請求情報マスタ --
INNER JOIN dbo.M_TORIHIKISAKI_SEIKYUU AS ts
    ON ts.TORIHIKISAKI_CD = mg.TORIHIKISAKI_CD

-- 取引先支払情報マスタ --
INNER JOIN dbo.M_TORIHIKISAKI_SHIHARAI as tsh
	ON tsh.TORIHIKISAKI_CD = mg.TORIHIKISAKI_CD

INNER JOIN dbo.M_GENBA_TEIKI_HINMEI AS teiki
    ON teiki.GYOUSHA_CD = tjd.GYOUSHA_CD
    AND teiki.GENBA_CD = tjd.GENBA_CD
    AND teiki.DENPYOU_KBN_CD = tjd.DENPYOU_KBN_CD
    AND teiki.HINMEI_CD = tjd.HINMEI_CD
	  
INNER JOIN dbo.M_GENBA_TSUKI_HINMEI AS tsuki
    ON tsuki.GYOUSHA_CD = teiki.GYOUSHA_CD
    AND tsuki.GENBA_CD = teiki.GENBA_CD
    AND tsuki.DENPYOU_KBN_CD = teiki.DENPYOU_KBN_CD
    AND tsuki.HINMEI_CD = teiki.TSUKI_HINMEI_CD
    AND tsuki.CHOUKA_SETTING = 1

WHERE 
tjd.KEIYAKU_KBN = 1
-- 取引先
    /*IF data.TORIHIKISAKI_CD_custom != null */ 
    AND mt.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD_custom*/'000000'
    /*END*/
/*IF data.FIX_CONDITION_VALUE == 2 */ 
AND tjd.KAKUTEI_FLG  = 0
/*END*/
/*IF data.FIX_CONDITION_VALUE != 2 */ 
AND tjd.KAKUTEI_FLG  = 1
/*END*/
	
-- [入力条件] --
/*IF data.SHIMEBI != null */ 
AND (
	-- 伝票区分：売上、支払以外
	(
	  (tjd.DENPYOU_KBN_CD != '1' AND tjd.DENPYOU_KBN_CD != '2')
	  OR tjd.DENPYOU_KBN_CD is null
	)
	AND  (
	  -- 請求情報絞込み --
	  (ts.SHIMEBI1 = /*data.SHIMEBI*/0 )
	  OR
	  -- 支払情報絞込み --
	  (tsh.SHIMEBI1 = /*data.SHIMEBI*/0 )
	)
)
/*END*/

/*IF data.SHIMEBI == null */ 
AND (
	-- 伝票区分：売上、支払以外
	(
	  (tjd.DENPYOU_KBN_CD != '1' AND tjd.DENPYOU_KBN_CD != '2')
	  OR tjd.DENPYOU_KBN_CD is null
	)
	AND  (
	  -- 請求情報絞込み --
	  (ts.TORIHIKI_KBN_CD = 1 )
	  OR
	  -- 支払情報絞込み --
	  (tsh.TORIHIKI_KBN_CD = 1 )
	)
)
/*END*/
AND ((tjd.UNIT_CD = '3' AND tjd.SUURYOU IS NOT NULL )
    OR(tjd.KANSAN_UNIT_CD = '3' AND tjd.KANSAN_SUURYOU IS NOT NULL ))