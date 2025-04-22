SELECT 
    GYO.*
FROM 
    dbo.M_GYOUSHU GYO
/*BEGIN*/WHERE
 /*IF data.GYOUSHU_CD != null*/
 GYO.GYOUSHU_CD LIKE '%' + /*data.GYOUSHU_CD*/'0001' + '%'
 /*END*/
 /*IF data.GYOUSHU_NAME != null*/AND GYO.GYOUSHU_NAME LIKE '%' +  /*data.GYOUSHU_NAME*/ + '%'/*END*/
 /*IF data.GYOUSHU_NAME_RYAKU != null*/AND GYO.GYOUSHU_NAME_RYAKU LIKE '%' +  /*data.GYOUSHU_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.GYOUSHU_FURIGANA != null*/AND GYO.GYOUSHU_FURIGANA LIKE '%' +  /*data.GYOUSHU_FURIGANA*/ + '%'/*END*/
 /*IF data.GYOUSHU_BIKOU != null*/AND GYO.GYOUSHU_BIKOU LIKE '%' +  /*data.GYOUSHU_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND GYO.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, GYO.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND GYO.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, GYO.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY GYO.GYOUSHU_CD
