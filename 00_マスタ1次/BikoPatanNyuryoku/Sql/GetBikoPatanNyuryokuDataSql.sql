SELECT 
    BIKO.*
FROM 
    dbo.M_BIKO_PATAN_NYURYOKU BIKO
/*BEGIN*/WHERE
/*IF data.BIKO_CD != null && data.BIKO_CD != '' */ BIKO.BIKO_CD LIKE '%' + /*data.BIKO_CD*/ + '%'/*END*/
 /*IF data.BIKO_NAME != null && data.BIKO_NAME != ''*/AND BIKO.BIKO_NAME LIKE '%' +  /*data.BIKO_NAME*/ + '%'/*END*/
 /*IF data.BIKO_NAME_RYAKU != null && data.BIKO_NAME_RYAKU != ''*/AND BIKO.BIKO_NAME_RYAKU LIKE '%' +  /*data.BIKO_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.BIKO_FURIGANA != null && data.BIKO_FURIGANA != ''*/AND BIKO.BIKO_FURIGANA LIKE '%' +  /*data.BIKO_FURIGANA*/ + '%'/*END*/
 /*IF data.CREATE_USER != null && data.CREATE_USER != ''*/AND BIKO.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, BIKO.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/AND BIKO.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, BIKO.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY BIKO.BIKO_CD
