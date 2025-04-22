--- 今回取引 [受入支払] 取得SQL ---
SELECT 
	-- 取引先CD
	dainoUkeireEntry.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
	
	-- 今回金額
	, dainoUkeireDetail.KINGAKU AS KINGAKU
	, dainoUkeireDetail.HINMEI_KINGAKU AS HINMEI_KINGAKU
		
	-- 今回税額(外税の場合)
	, dainoUkeireDetail.TAX_SOTO AS TAX_SOTO
	, dainoUkeireDetail.HINMEI_TAX_SOTO AS HINMEI_TAX_SOTO
	
	-- 今回税額(内税の場合)
	, dainoUkeireDetail.TAX_UCHI AS TAX_UCHI
	, dainoUkeireDetail.HINMEI_TAX_UCHI AS HINMEI_TAX_UCHI
		
    -- 帳票に使用(明細データ)
	, dainoUkeireDetail.NET_JYUURYOU AS NET_JYUURYOU
	, dainoUkeireDetail.SUURYOU AS SUURYOU
	, unit.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU
	, dainoUkeireDetail.HINMEI_CD AS HINMEI_CD
	, dainoUkeireDetail.HINMEI_NAME AS HINMEI_NAME
	, dainoUkeireDetail.TANKA AS TANKA
    , dainoUkeireDetail.ROW_NO AS ROW_NO
    , dainoUkeireDetail.DENPYOU_KBN_CD AS DENPYOU_KBN_CD
    
    -- デバッグ用
    , dainoUkeireEntry.SYSTEM_ID AS SYSTEM_ID
    , dainoUkeireEntry.SEQ AS SEQ
    , dainoUkeireDetail.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID
        
-- 代納入力(親) --
FROM T_DAINOU_ENTRY AS dainoEntry 

-- 代納入力(子) --
INNER JOIN T_DAINOU_UKEIRE_ENTRY AS dainoUkeireEntry 
ON
	dainoUkeireEntry.SYSTEM_ID = dainoEntry.SYSTEM_ID
	AND dainoUkeireEntry.SEQ = dainoEntry.SEQ
    -- 支払時は支払
	AND dainoUkeireEntry.SHIHARAI_TORIHIKI_KBN_CD = 2 	                    -- 1:現金／2:掛け ※代納は2固定

-- 代納明細を取得--
INNER JOIN T_DAINOU_UKEIRE_DETAIL AS dainoUkeireDetail 
ON
	dainoUkeireDetail.SYSTEM_ID = dainoUkeireEntry.SYSTEM_ID
	AND dainoUkeireDetail.SEQ = dainoUkeireEntry.SEQ
	AND dainoUkeireDetail.DENPYOU_KBN_CD = 2 								-- 1:売上(請求時)／2:支払(支払) ※変更箇所
		
-- 帳票用：品名マスタ --
LEFT OUTER JOIN M_UNIT AS unit
ON
	unit.UNIT_CD = dainoUkeireDetail.UNIT_CD
	AND unit.DELETE_FLG = 0
    
WHERE
	-- 取得した代納情報が清算明細に紐つきがないことを確認(＝締められていないこと) --
	NOT EXISTS (
				SELECT 1
				FROM T_SEISAN_DETAIL AS seisanDetail 
				WHERE
					seisanDetail.DENPYOU_SHURUI_CD = 170			--170:代納
					AND seisanDetail.DENPYOU_SYSTEM_ID = dainoUkeireDetail.SYSTEM_ID
					AND seisanDetail.DENPYOU_SEQ = dainoUkeireDetail.SEQ
					/*IF meisaiChecked == true*/
					AND seisanDetail.DETAIL_SYSTEM_ID = dainoUkeireDetail.DETAIL_SYSTEM_ID
					/*END*/
					AND seisanDetail.DELETE_FLG = 0
	)
    -- 今回情報 --
	AND dainoEntry.DELETE_FLG = 0
	AND dainoEntry.DAINOU_NUMBER = /*dainouNumber*/null
		