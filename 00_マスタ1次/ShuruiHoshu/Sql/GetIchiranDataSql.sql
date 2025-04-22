SELECT
    SHU.*
FROM
    dbo.M_SHURUI SHU
/*BEGIN*/WHERE
 /*IF data.SHURUI_CD != null*/ SHU.SHURUI_CD LIKE '%' + /*data.SHURUI_CD*/ + '%'/*END*/
 /*IF data.SHURUI_NAME != null*/AND SHU.SHURUI_NAME LIKE '%' +  /*data.SHURUI_NAME*/ + '%'/*END*/
 /*IF data.SHURUI_NAME_RYAKU != null*/AND SHU.SHURUI_NAME_RYAKU LIKE '%' +  /*data.SHURUI_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.SHURUI_FURIGANA != null*/AND SHU.SHURUI_FURIGANA LIKE '%' +  /*data.SHURUI_FURIGANA*/ + '%'/*END*/
 /*IF data.SHURUI_BIKOU != null*/AND SHU.SHURUI_BIKOU LIKE '%' +  /*data.SHURUI_BIKOU*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND SHU.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, SHU.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND SHU.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, SHU.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND SHU.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY SHU.SHURUI_CD
