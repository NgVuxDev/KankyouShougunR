SELECT 
	-- 業者情報
	sha.GYOUSHA_CD AS 業者CD
	,sha.GYOUSHA_NAME1 AS 業者名1
	,sha.GYOUSHA_NAME2 AS 業者名2
	,sha.POST AS 業者郵便番号
	,sha.ADDRESS1 AS 業者住所1
	,sha.ADDRESS2 AS 業者住所2
	,sha.GYOUSHA_TEL AS 業者電話番号
	,sha.GYOUSHA_FAX AS 業者FAX
	,shaShain.SHAIN_NAME AS 業者営業担当者
	-- 現場情報
	,ba.GENBA_CD AS 現場CD
	,ba.GENBA_NAME1 AS 現場名1
	,ba.GENBA_NAME2 AS 現場名2
	,ba.POST AS 現場郵便番号
	,ba.ADDRESS1 AS 現場住所1
	,ba.ADDRESS2 AS 現場住所2
	,ba.GENBA_TEL AS 現場電話番号
	,ba.GENBA_FAX AS 現場FAX
	,baShain.SHAIN_NAME AS 現場営業担当者
	-- 業者-基本情報
	,shaTorihiki.TORIHIKISAKI_CD AS 業者取引先CD
	,shaTorihiki.TORIHIKISAKI_NAME1 AS 業者取引先名1
	,shaTorihiki.TORIHIKISAKI_NAME2 AS 業者取引先名2
	,shaTorihiki.BUSHO AS 業者部署
	,shaTorihiki.TANTOUSHA AS 業者担当者
	,shaTorihiki.SHUUKEI_ITEM_CD AS 業者集計項目CD
	,shaItem.FREE_ITEM_NAME AS 業者集計項目名
	,shaTorihiki.GYOUSHU_CD AS 業者業種CD
	,shaG.GYOUSHU_NAME AS 業者業種名
	,shaTorihiki.BIKOU1 AS 業者備考1
	,shaTorihiki.BIKOU2 AS 業者備考2
	,shaTorihiki.BIKOU3 AS 業者備考3
	,shaTorihiki.BIKOU4 AS 業者備考4
	-- 現場-基本情報
	,baTorihiki.TORIHIKISAKI_CD AS 現場取引先CD
	,baTorihiki.TORIHIKISAKI_NAME1 AS 現場取引先名1
	,baTorihiki.TORIHIKISAKI_NAME2 AS 現場取引先名2
	,baTorihiki.BUSHO AS 現場部署
	,baTorihiki.TANTOUSHA AS 現場担当者
	,baTorihiki.SHUUKEI_ITEM_CD AS 現場集計項目CD
	,baItem.FREE_ITEM_NAME AS 現場集計項目名
	,baTorihiki.GYOUSHU_CD AS 現場業種CD
	,baG.GYOUSHU_NAME AS 現場業種名
	,baTorihiki.BIKOU1 AS 現場備考1
	,baTorihiki.BIKOU2 AS 現場備考2
	,baTorihiki.BIKOU3 AS 現場備考3
	,baTorihiki.BIKOU4 AS 現場備考4
	-- 業者-請求情報
	,shaTorihiki.SEIKYUU_SHIMEBI1 AS 業者締日1
	,shaTorihiki.SEIKYUU_SHIMEBI2 AS 業者締日2
	,shaTorihiki.SEIKYUU_SHIMEBI3 AS 業者締日3
	,shaTorihiki.SEIKYUU_HICCHAKUBI AS 業者請求書必着日
	,shaTorihiki.KAISHUU_MONTH AS 業者回収月
	,shaTorihiki.KAISHUU_DAY AS 業者回収日
	,shaK.NYUUSHUKKIN_KBN_NAME AS 業者回収方法
	,shaTorihiki.SEIKYUU_JOUHOU1 AS 業者請求情報1
	,shaTorihiki.SEIKYUU_JOUHOU2 AS 業者請求情報2
	,shaTorihiki.KAISHI_URIKAKE_ZANDAKA AS 業者開始売掛残高
	,shaTorihiki.SEIKYUUSHO_SHOSHIKI AS 業者請求書書式1
	,shaTorihiki.SEIKYUUSHO_SHOSHIKI_MEISAI AS 業者請求書書式2
	-- 現場-請求情報
	,baTorihiki.SEIKYUU_SHIMEBI1 AS 現場締日1
	,baTorihiki.SEIKYUU_SHIMEBI2 AS 現場締日2
	,baTorihiki.SEIKYUU_SHIMEBI3 AS 現場締日3
	,baTorihiki.SEIKYUU_HICCHAKUBI AS 現場請求書必着日
	,baTorihiki.KAISHUU_MONTH AS 現場回収月
	,baTorihiki.KAISHUU_DAY AS 現場回収日
	,baK.NYUUSHUKKIN_KBN_NAME AS 現場回収方法
	,baTorihiki.SEIKYUU_JOUHOU1 AS 現場請求情報1
	,baTorihiki.SEIKYUU_JOUHOU2 AS 現場請求情報2
	,baTorihiki.KAISHI_URIKAKE_ZANDAKA AS 現場開始売掛残高
	,baTorihiki.SEIKYUUSHO_SHOSHIKI AS 現場請求書書式1
	,baTorihiki.SEIKYUUSHO_SHOSHIKI_MEISAI AS 現場請求書書式2
FROM M_GYOUSHA AS sha 
	INNER JOIN M_GENBA AS ba ON sha.GYOUSHA_CD = ba.GENBA_CD 
	LEFT OUTER JOIN M_TORIHIKISAKI AS shaTorihiki ON sha.TORIHIKISAKI_CD = shaTorihiki.TORIHIKISAKI_CD 
	LEFT OUTER JOIN M_TORIHIKISAKI AS baTorihiki ON ba.TORIHIKISAKI_CD = baTorihiki.TORIHIKISAKI_CD 
	LEFT OUTER JOIN M_SHAIN AS shaShain ON sha.EIGYOU_TANTOU_CD = shaShain.SHAIN_CD 
	LEFT OUTER JOIN M_SHAIN AS baShain ON ba.EIGYOU_TANTOU_CD = baShain.SHAIN_CD 
	LEFT OUTER JOIN M_FREE_ITEM AS shaItem ON shaTorihiki.SHUUKEI_ITEM_CD = shaItem.FREE_ITEM_CD 
	LEFT OUTER JOIN M_FREE_ITEM AS baItem ON baTorihiki.SHUUKEI_ITEM_CD = baItem.FREE_ITEM_CD 
	LEFT OUTER JOIN M_GYOUSHU AS shaG ON shaTorihiki.GYOUSHU_CD = shaG.GYOUSHU_CD 
	LEFT OUTER JOIN M_GYOUSHU AS baG ON baTorihiki.GYOUSHU_CD = baG.GYOUSHU_CD
	LEFT OUTER JOIN M_NYUUSHUKKIN_KBN AS shaK ON shaTorihiki.KAISHUU_HOUHOU = shaK.NYUUSHUKKIN_KBN_CD
	LEFT OUTER JOIN M_NYUUSHUKKIN_KBN AS baK ON baTorihiki.KAISHUU_HOUHOU = baK.NYUUSHUKKIN_KBN_CD
WHERE 
	(ba.GYOUSHA_CD = /*gyoushaCD*/'') 
	AND (ba.GENBA_CD = /*genbaCD*/'')