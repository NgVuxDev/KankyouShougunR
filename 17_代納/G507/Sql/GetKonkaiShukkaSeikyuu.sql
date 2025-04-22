--- 今回取引 [出荷請求] 取得SQL ---
SELECT 
	-- 取引先CD
	dainoShukkaEntry.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
	
	-- 今回金額
	, dainoShukkaDetail.KINGAKU AS KINGAKU
	, dainoShukkaDetail.HINMEI_KINGAKU AS HINMEI_KINGAKU
		
	-- 今回税額(外税の場合)
	, dainoShukkaDetail.TAX_SOTO AS TAX_SOTO
	, dainoShukkaDetail.HINMEI_TAX_SOTO AS HINMEI_TAX_SOTO
	
	-- 今回税額(内税の場合)
	, dainoShukkaDetail.TAX_UCHI AS TAX_UCHI
	, dainoShukkaDetail.HINMEI_TAX_UCHI AS HINMEI_TAX_UCHI
		
    -- 帳票に使用(明細データ)
	, dainoShukkaDetail.NET_JYUURYOU AS NET_JYUURYOU
	, dainoShukkaDetail.SUURYOU AS SUURYOU
	, unit.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU
	, dainoShukkaDetail.HINMEI_CD AS HINMEI_CD
	, dainoShukkaDetail.HINMEI_NAME AS HINMEI_NAME
	, dainoShukkaDetail.TANKA AS TANKA
    , dainoShukkaDetail.ROW_NO AS ROW_NO
    , dainoShukkaDetail.DENPYOU_KBN_CD AS DENPYOU_KBN_CD
    
    -- デバッグ用
    , dainoShukkaEntry.SYSTEM_ID AS SYSTEM_ID
    , dainoShukkaEntry.SEQ AS SEQ
    , dainoShukkaDetail.DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID
        
-- 代納入力(親) --
FROM T_DAINOU_ENTRY AS dainoEntry 

-- 代納入力(子)を取得 --
INNER JOIN T_DAINOU_SHUKKA_ENTRY AS dainoShukkaEntry 
ON
	dainoShukkaEntry.SYSTEM_ID = dainoEntry.SYSTEM_ID
	AND dainoShukkaEntry.SEQ = dainoEntry.SEQ
    -- 請求時は売上
	AND dainoShukkaEntry.URIAGE_TORIHIKI_KBN_CD = 2                       -- 1:現金／2:掛け ※代納は2固定

-- 代納明細を取得--
INNER JOIN T_DAINOU_SHUKKA_DETAIL AS dainoShukkaDetail 
ON
	dainoShukkaDetail.SYSTEM_ID = dainoShukkaEntry.SYSTEM_ID
	AND dainoShukkaDetail.SEQ = dainoShukkaEntry.SEQ
	AND dainoShukkaDetail.DENPYOU_KBN_CD = 1 								-- 1:売上(請求時)／2:支払(支払) ※変更箇所
		
-- 帳票用：品名マスタ --
LEFT OUTER JOIN M_UNIT AS unit
ON
	unit.UNIT_CD = dainoShukkaDetail.UNIT_CD
	AND unit.DELETE_FLG = 0
    
WHERE
	-- 取得した代納情報が請求明細に紐つきがないことを確認(＝締められていないこと) --
	NOT EXISTS (
				SELECT 1
				FROM T_SEIKYUU_DETAIL AS seikyuDetail 
				WHERE
					seikyuDetail.DENPYOU_SHURUI_CD = 170			--170:代納
					AND seikyuDetail.DENPYOU_SYSTEM_ID = dainoShukkaDetail.SYSTEM_ID
					AND seikyuDetail.DENPYOU_SEQ = dainoShukkaDetail.SEQ
					/*IF meisaiChecked == true*/
					AND seikyuDetail.DETAIL_SYSTEM_ID = dainoShukkaDetail.DETAIL_SYSTEM_ID
					/*END*/
					AND seikyuDetail.DELETE_FLG = 0
	)
    -- 今回情報 --
	AND dainoEntry.DELETE_FLG = 0
	AND dainoEntry.DAINOU_NUMBER = /*dainouNumber*/null
