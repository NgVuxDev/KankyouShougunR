--- 前回取引 [出荷請求] 取得SQL ---

SELECT 
	-- 取引先CD
	konkai.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
	
	-- 前回金額
	, zenkaiDainoShukkaDetail.KINGAKU AS KINGAKU
	, zenkaiDainoShukkaDetail.HINMEI_KINGAKU AS HINMEI_KINGAKU
		
	-- 前回税額(外税の場合)
	, zenkaiDainoShukkaDetail.TAX_SOTO AS TAX_SOTO
	, zenkaiDainoShukkaDetail.HINMEI_TAX_SOTO AS HINMEI_TAX_SOTO
	
	-- 前回税額(内税の場合)
	, zenkaiDainoShukkaDetail.TAX_UCHI AS TAX_UCHI
	, zenkaiDainoShukkaDetail.HINMEI_TAX_UCHI AS HINMEI_TAX_UCHI
		
    -- デバッグ用 --
	-- 今回
    , konkai.SYSTEM_ID AS KONKAI_SYSTEM_ID
    , konkai.SEQ AS KONKAI_SEQ
	-- 前回
    , zenkaiDainoShukkaDetail.SYSTEM_ID AS ZENKAI_SYSTEM_ID
    , zenkaiDainoShukkaDetail.SEQ AS ZENKAI_SEQ
    , zenkaiDainoShukkaDetail.DETAIL_SYSTEM_ID AS ZENKAI_DETAIL_SYSTEM_ID
        
-- 代納番号に紐づく取引先CDを取得 --
FROM (
	SELECT 
		dainoShukkaEntry.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
		, dainoShukkaEntry.SYSTEM_ID AS SYSTEM_ID
		, dainoShukkaEntry.SEQ AS SEQ
	FROM T_DAINOU_ENTRY AS dainoEntry 
	INNER JOIN T_DAINOU_SHUKKA_ENTRY AS dainoShukkaEntry 
	ON
		dainoShukkaEntry.SYSTEM_ID = dainoEntry.SYSTEM_ID
		AND dainoShukkaEntry.SEQ = dainoEntry.SEQ
	WHERE
		dainoEntry.DELETE_FLG = 0
		AND dainoEntry.DAINOU_NUMBER = /*dainouNumber*/null
) AS konkai
	
-- 取引先CDに紐づく今回の伝票に紐づかない代納情報を取得--
INNER JOIN T_DAINOU_SHUKKA_ENTRY AS zenkaiDainoShukkaEntry 
ON
	zenkaiDainoShukkaEntry.TORIHIKISAKI_CD = konkai.TORIHIKISAKI_CD
	AND NOT(
        zenkaiDainoShukkaEntry.SYSTEM_ID = konkai.SYSTEM_ID
		AND zenkaiDainoShukkaEntry.SEQ = konkai.SEQ
    )
    -- 出荷テーブルの請求時は売上フィールドを見る
	AND zenkaiDainoShukkaEntry.URIAGE_TORIHIKI_KBN_CD = 2                       -- 1:現金／2:掛け ※代納は2固定
	
-- 代納明細を取得--
INNER JOIN T_DAINOU_SHUKKA_DETAIL AS zenkaiDainoShukkaDetail 
ON
	zenkaiDainoShukkaDetail.SYSTEM_ID = zenkaiDainoShukkaEntry.SYSTEM_ID
	AND zenkaiDainoShukkaDetail.SEQ = zenkaiDainoShukkaEntry.SEQ
	AND zenkaiDainoShukkaDetail.DENPYOU_KBN_CD = 1 								-- 1:売上(請求時)／2:支払(支払) ※変更箇所
		
WHERE
	-- 取得した代納情報が請求明細に紐つきがないことを確認(＝締められていないこと) --
	NOT EXISTS (
				SELECT 1
				FROM T_SEIKYUU_DETAIL AS seikyuDetail 
				WHERE
					seikyuDetail.DENPYOU_SHURUI_CD = 170			--170:代納
					AND seikyuDetail.DENPYOU_SYSTEM_ID = zenkaiDainoShukkaEntry.SYSTEM_ID
					AND seikyuDetail.DENPYOU_SEQ = zenkaiDainoShukkaEntry.SEQ
					/*IF meisaiChecked == true */
					AND seikyuDetail.DETAIL_SYSTEM_ID = zenkaiDainoShukkaDetail.DETAIL_SYSTEM_ID
					/*END*/
					AND seikyuDetail.DELETE_FLG = 0
	)
		