SELECT 
    BUN.*
FROM 
    dbo.M_BUNRUI BUN
/*BEGIN*/WHERE
/*IF data.BUNRUI_CD != null && data.BUNRUI_CD != '' */ BUN.BUNRUI_CD LIKE '%' + /*data.BUNRUI_CD*/ + '%'/*END*/
 /*IF data.BUNRUI_NAME != null && data.BUNRUI_NAME != ''*/AND BUN.BUNRUI_NAME LIKE '%' +  /*data.BUNRUI_NAME*/ + '%'/*END*/
 /*IF data.BUNRUI_NAME_RYAKU != null && data.BUNRUI_NAME_RYAKU != ''*/AND BUN.BUNRUI_NAME_RYAKU LIKE '%' +  /*data.BUNRUI_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.BUNRUI_FURIGANA != null && data.BUNRUI_FURIGANA != ''*/AND BUN.BUNRUI_FURIGANA LIKE '%' +  /*data.BUNRUI_FURIGANA*/ + '%'/*END*/
 /*IF data.CREATE_USER != null && data.CREATE_USER != ''*/AND BUN.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, BUN.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/AND BUN.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, BUN.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY BUN.BUNRUI_CD
