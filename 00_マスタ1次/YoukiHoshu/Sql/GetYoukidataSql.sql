SELECT
    YK.*
FROM
    dbo.M_YOUKI YK
/*BEGIN*/WHERE
 /*IF data.YOUKI_CD != null*/
  YK.YOUKI_CD LIKE '%' + /*data.YOUKI_CD*/'000001' + '%'
 /*END*/
 /*IF data.YOUKI_NAME != null && data.YOUKI_NAME != ''*/AND YK.YOUKI_NAME LIKE '%' +  /*data.YOUKI_NAME*/ + '%'/*END*/
 /*IF data.YOUKI_NAME_RYAKU != null && data.YOUKI_NAME_RYAKU != ''*/AND YK.YOUKI_NAME_RYAKU LIKE '%' +  /*data.YOUKI_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.YOUKI_FURIGANA != null && data.YOUKI_FURIGANA != ''*/AND YK.YOUKI_FURIGANA LIKE '%' +  /*data.YOUKI_FURIGANA*/ + '%'/*END*/
 /*IF !data.YOUKI_JYURYO.IsNull*/AND YK.YOUKI_JYURYO LIKE '%' +  /*data.YOUKI_JYURYO.Value*/ + '%'/*END*/
 /*IF data.YOUKI_BIKOU != null && data.YOUKI_BIKOU != ''*/AND YK.YOUKI_BIKOU LIKE '%' +  /*data.YOUKI_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null && data.CREATE_USER != ''*/AND YK.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, YK.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/AND YK.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, YK.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY YK.YOUKI_CD
