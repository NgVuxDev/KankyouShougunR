SELECT 
    BU.*
    ,ISNULL(BU.BUSHO_NAME_RYAKU,N'') AS BUSHO_NAME_RYAKU
FROM 
    dbo.M_BUSHO BU

/*BEGIN*/WHERE
 /*IF data.BUSHO_CD != null*/
 BU.BUSHO_CD LIKE '%' + /*data.BUSHO_CD*/ + '%'
 /*END*/
 /*IF data.BUSHO_NAME != null*/AND BU.BUSHO_NAME LIKE '%' +  /*data.BUSHO_NAME*/ + '%'/*END*/
 /*IF data.BUSHO_NAME_RYAKU != null*/AND BU.BUSHO_NAME_RYAKU LIKE '%' +  /*data.BUSHO_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.BUSHO_FURIGANA != null*/AND BU.BUSHO_FURIGANA LIKE '%' +  /*data.BUSHO_FURIGANA*/ + '%'/*END*/
 /*IF data.BUSHO_CD != null*/AND BU.BUSHO_CD LIKE '%' +  /*data.BUSHO_CD*/ + '%'/*END*/
 /*IF data.BUSHO_BIKOU != null*/AND BU.BUSHO_BIKOU LIKE '%' +  /*data.BUSHO_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND BU.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, BU.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND BU.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, BU.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND BU.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY BU.BUSHO_CD
