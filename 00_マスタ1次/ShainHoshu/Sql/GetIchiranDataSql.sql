SELECT 
    SHA.*
    ,ISNULL(BU.BUSHO_NAME_RYAKU,N'') AS BUSHO_NAME_RYAKU
FROM 
    dbo.M_SHAIN SHA
	LEFT JOIN dbo.M_BUSHO BU ON BU.BUSHO_CD = SHA.BUSHO_CD
/*BEGIN*/WHERE
 /*IF data.SHAIN_CD != null*/
 SHA.SHAIN_CD LIKE '%' + /*data.SHAIN_CD*/'000001' + '%'
 /*END*/
 /*IF data.SHAIN_NAME != null*/AND SHA.SHAIN_NAME LIKE '%' +  /*data.SHAIN_NAME*/ + '%'/*END*/
 /*IF data.SHAIN_NAME_RYAKU != null*/AND SHA.SHAIN_NAME_RYAKU LIKE '%' +  /*data.SHAIN_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.SHAIN_FURIGANA != null*/AND SHA.SHAIN_FURIGANA LIKE '%' +  /*data.SHAIN_FURIGANA*/ + '%'/*END*/
 /*IF data.BUSHO_CD != null*/AND SHA.BUSHO_CD LIKE '%' +  /*data.BUSHO_CD*/ + '%'/*END*/
 /*IF data.WARIATE_JUN != null*/AND SHA.WARIATE_JUN LIKE '%' +  /*data.WARIATE_JUN*/ + '%'/*END*/ --20250321
 /*IF data.LOGIN_ID != null*/AND SHA.LOGIN_ID LIKE '%' +  /*data.LOGIN_ID*/ + '%'/*END*/
 /*IF data.PASSWORD != null*/AND SHA.PASSWORD LIKE '%' +  /*data.PASSWORD*/ + '%'/*END*/
 /*IF data.MAIL_ADDRESS != null*/AND SHA.MAIL_ADDRESS LIKE '%' +  /*data.MAIL_ADDRESS*/ + '%'/*END*/
 /*IF data.SHAIN_BIKOU != null*/AND SHA.SHAIN_BIKOU LIKE '%' +  /*data.SHAIN_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND SHA.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, SHA.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, SHA.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND SHA.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND SHA.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY SHA.SHAIN_CD
