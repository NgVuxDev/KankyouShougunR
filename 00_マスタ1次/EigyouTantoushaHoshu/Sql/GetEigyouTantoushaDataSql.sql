SELECT 
    EIG.*
FROM 
    dbo.M_EIGYOU_TANTOUSHA EIG
/*BEGIN*/WHERE
/*IF data.SHAIN_CD != null && data.SHAIN_CD != '' */EIG.SHAIN_CD LIKE '%' +/*data.SHAIN_CD*/'000001' + '%'/*END*/
 /*IF data.EIGYOU_TANTOUSHA_BIKOU != null*/AND EIG.EIGYOU_TANTOUSHA_BIKOU LIKE '%' +  /*data.EIGYOU_TANTOUSHA_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND EIG.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, EIG.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND EIG.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, EIG.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*END*/
ORDER BY EIG.SHAIN_CD
