--- 前回取引 [受入請求] 取得SQL ---

SELECT 
	-- 取引先CD
	konkai.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
	
	-- 前回金額
	, zenkaiDainoUkeireDetail.KINGAKU AS KINGAKU
	, zenkaiDainoUkeireDetail.HINMEI_KINGAKU AS HINMEI_KINGAKU
		
	-- 前回税額(外税の場合)
	, zenkaiDainoUkeireDetail.TAX_SOTO AS TAX_SOTO
	, zenkaiDainoUkeireDetail.HINMEI_TAX_SOTO AS HINMEI_TAX_SOTO
	
	-- 前回税額(内税の場合)
	, zenkaiDainoUkeireDetail.TAX_UCHI AS TAX_UCHI
	, zenkaiDainoUkeireDetail.HINMEI_TAX_UCHI AS HINMEI_TAX_UCHI
		
    -- デバッグ用 --
	-- 今回
    , konkai.SYSTEM_ID AS KONKAI_SYSTEM_ID
    , konkai.SEQ AS KONKAI_SEQ
	-- 前回
    , zenkaiDainoUkeireDetail.SYSTEM_ID AS ZENKAI_SYSTEM_ID
    , zenkaiDainoUkeireDetail.SEQ AS ZENKAI_SEQ
    , zenkaiDainoUkeireDetail.DETAIL_SYSTEM_ID AS ZENKAI_DETAIL_SYSTEM_ID
        
-- 代納番号に紐づく取引先CDを取得 --
FROM (
	SELECT 
		dainoUkeireEntry.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
		, dainoUkeireEntry.SYSTEM_ID AS SYSTEM_ID
		, dainoUkeireEntry.SEQ AS SEQ
	FROM T_DAINOU_ENTRY AS dainoEntry 
	INNER JOIN T_DAINOU_UKEIRE_ENTRY AS dainoUkeireEntry 
	ON
		dainoUkeireEntry.SYSTEM_ID = dainoEntry.SYSTEM_ID
		AND dainoUkeireEntry.SEQ = dainoEntry.SEQ
	WHERE
		dainoEntry.DELETE_FLG = 0
		AND dainoEntry.DAINOU_NUMBER = /*dainouNumber*/null
) AS konkai
	
-- 前回の代納情報を取得(取引先CDに紐づく今回の伝票番号に紐づかない)--
INNER JOIN T_DAINOU_UKEIRE_ENTRY AS zenkaiDainoUkeireEntry 
ON
	zenkaiDainoUkeireEntry.TORIHIKISAKI_CD = konkai.TORIHIKISAKI_CD
	AND NOT(
        zenkaiDainoUkeireEntry.SYSTEM_ID = konkai.SYSTEM_ID
		AND zenkaiDainoUkeireEntry.SEQ = konkai.SEQ
    )
    -- 受入テーブルの請求時は売上フィールドを見る
	AND zenkaiDainoUkeireEntry.URIAGE_TORIHIKI_KBN_CD = 2                       -- 1:現金／2:掛け ※代納は2固定
	
-- 前回の代納明細を取得(取引先CDに紐づく今回の伝票番号に紐づかない)--
INNER JOIN T_DAINOU_UKEIRE_DETAIL AS zenkaiDainoUkeireDetail 
ON
	zenkaiDainoUkeireDetail.SYSTEM_ID = zenkaiDainoUkeireEntry.SYSTEM_ID
	AND zenkaiDainoUkeireDetail.SEQ = zenkaiDainoUkeireEntry.SEQ
	AND zenkaiDainoUkeireDetail.DENPYOU_KBN_CD = 1 								-- 1:売上(請求時)／2:支払(支払) ※変更箇所
		
WHERE
	-- 取得した代納情報が請求明細に紐つきがないことを確認(＝締められていないこと) --
	NOT EXISTS (
				SELECT 1
				FROM T_SEIKYUU_DETAIL AS seikyuDetail 
				WHERE
					seikyuDetail.DENPYOU_SHURUI_CD = 170			--170:代納
					AND seikyuDetail.DENPYOU_SYSTEM_ID = zenkaiDainoUkeireEntry.SYSTEM_ID
					AND seikyuDetail.DENPYOU_SEQ = zenkaiDainoUkeireEntry.SEQ
					/*IF meisaiChecked == true */
					AND seikyuDetail.DETAIL_SYSTEM_ID = zenkaiDainoUkeireDetail.DETAIL_SYSTEM_ID
					/*END*/
					AND seikyuDetail.DELETE_FLG = 0
	)
