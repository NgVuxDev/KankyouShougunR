SELECT
   KEI.*
FROM
    dbo.M_KEIJOU KEI
/*BEGIN*/WHERE
 /*IF data.KEIJOU_CD != null && data.KEIJOU_CD != ''*/KEI.KEIJOU_CD LIKE '%' + /*data.KEIJOU_CD*/'01' + '%'/*END*/
 /*IF data.KEIJOU_NAME != null && data.KEIJOU_NAME != ''*/AND KEI.KEIJOU_NAME LIKE '%' +  /*data.KEIJOU_NAME*/ + '%'/*END*/
 /*IF data.KEIJOU_NAME_RYAKU != null && data.KEIJOU_NAME_RYAKU != ''*/AND KEI.KEIJOU_NAME_RYAKU LIKE '%' +  /*data.KEIJOU_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.KEIJOU_NAME_FURIGANA != null && data.KEIJOU_NAME_FURIGANA != ''*/AND KEI.KEIJOU_NAME_FURIGANA LIKE '%' +  /*data.KEIJOU_NAME_FURIGANA*/ + '%'/*END*/
 /*IF data.KEIJOU_BIKOU != null && data.KEIJOU_BIKOU != ''*/AND KEI.KEIJOU_BIKOU LIKE '%' +  /*data.KEIJOU_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null && data.CREATE_USER != ''*/AND KEI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, KEI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/AND KEI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, KEI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY KEI.KEIJOU_CD
