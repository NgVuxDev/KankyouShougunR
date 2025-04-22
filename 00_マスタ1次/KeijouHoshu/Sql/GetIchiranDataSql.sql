SELECT
   KEI.*
FROM
    dbo.M_KEIJOU KEI
/*BEGIN*/WHERE
 /*IF data.KEIJOU_CD != null*/
 KEI.KEIJOU_CD LIKE '%' + /*data.KEIJOU_CD*/'000001' + '%'
 /*END*/
 /*IF data.KEIJOU_NAME != null*/AND KEI.KEIJOU_NAME LIKE '%' +  /*data.KEIJOU_NAME*/ + '%'/*END*/
 /*IF data.KEIJOU_NAME_RYAKU != null*/AND KEI.KEIJOU_NAME_RYAKU LIKE '%' +  /*data.KEIJOU_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.KEIJOU_FURIGANA != null*/AND KEI.KEIJOU_FURIGANA LIKE '%' +  /*data.KEIJOU_FURIGANA*/ + '%'/*END*/
 /*IF data.KEIJOU_BIKOU != null*/AND KEI.KEIJOU_BIKOU LIKE '%' +  /*data.KEIJOU_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND KEI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, KEI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND KEI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, KEI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND KEI.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY KEI.KEIJOU_CD
