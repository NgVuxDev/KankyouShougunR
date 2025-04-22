SELECT 
	BNK.*
	,ISNULL(BNK.BANK_NAME,N'') AS BANK_NAME
    ,ISNULL(BNK.BANK_NAME_RYAKU,N'') AS BANK_NAME_RYAKU
FROM 
	dbo.M_BANK BNK
/*BEGIN*/WHERE
	/*IF data.BANK_CD != null*/ BNK.BANK_CD LIKE '%' + /*data.BANK_CD*/ + '%'/*END*/
	/*IF data.BANK_NAME != null*/ AND BNK.BANK_NAME LIKE '%' + /*data.BANK_NAME*/ + '%'/*END*/
	/*IF data.BANK_NAME_RYAKU != null*/ AND BNK.BANK_NAME_RYAKU LIKE '%' + /*data.BANK_NAME_RYAKU*/ + '%'/*END*/
	/*IF data.BANK_FURIGANA != null*/ AND BNK.BANK_FURIGANA LIKE '%' + /*data.BANK_FURIGANA*/ + '%'/*END*/
	/*IF data.RENKEI_CD != null*/ AND BNK.RENKEI_CD LIKE '%' + /*data.RENKEI_CD*/ + '%'/*END*/
	/*IF data.BANK_BIKOU != null*/ AND BNK.BANK_BIKOU LIKE '%' + /*data.BANK_BIKOU*/ + '%'/*END*/
	/*IF data.UPDATE_USER != null*/AND BNK.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, BNK.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
	/*IF data.CREATE_USER != null*/AND BNK.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, BNK.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
	/*IF !deletechuFlg*/AND BNK.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY BNK.BANK_CD
