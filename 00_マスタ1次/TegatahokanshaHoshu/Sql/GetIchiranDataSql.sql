SELECT
    TE.*
FROM
    dbo.M_TEGATA_HOKANSHA TE
    LEFT JOIN dbo.M_SHAIN SHA ON TE.SHAIN_CD = SHA.SHAIN_CD
/*BEGIN*/WHERE
 /*IF data.TEGATA_HOKANSHA_CD != null*/
 TE.SHAIN_CD LIKE '%' + /*data.TEGATA_HOKANSHA_CD*/'000001' + '%'
 /*END*/
 /*IF data.TEGATA_HOKANSHA_NAME != null*/AND SHA.SHAIN_NAME LIKE '%' +  /*data.SHUUKEI_KOUMOKU_NAME*/ + '%'/*END*/
 /*IF data.SHUUKEI_KOUMOKU_FURIGANA != null*/AND SHA.SHAIN_FURIGANA LIKE '%' +  /*data.SHUUKEI_KOUMOKU_FURIGANA*/ + '%'/*END*/
 /*IF data.LOGIN_ID != null*/AND SHA.LOGIN_ID LIKE '%' +  /*data.LOGIN_ID*/ + '%'/*END*/
 /*IF data.PASSWORD != null*/AND SHA.PASSWORD LIKE '%' +  /*data.PASSWORD*/ + '%'/*END*/
 /*IF data.SHUUKEI_KOUMOKU_BIKOU != null*/AND TE.TEGATA_HOKANSHA_BIKOU LIKE '%' +  /*data.SHUUKEI_KOUMOKU_BIKOU*/ + '%'/*END*/
 /*IF !data.TEKIYOU_BEGIN.IsNull*/ AND SHA.TEKIYOU_BEGIN LIKE '%' +  /*data.TEKIYOU_BEGIN.Value*/ + '%'/*END*/
 /*IF !data.TEKIYOU_END.IsNull*/AND SHA.TEKIYOU_END LIKE '%' +  /*data.TEKIYOU_END.Value*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND TE.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, TE.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND TE.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, TE.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND TE.DELETE_FLG = 0/*END*/
 /*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/AND (1 = 0/*END*/
 /*IF tekiyounaiFlg*/OR (((SHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= SHA.TEKIYOU_END) or (SHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and SHA.TEKIYOU_END IS NULL) or (SHA.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <=SHA.TEKIYOU_END) or (SHA.TEKIYOU_BEGIN IS NULL and SHA.TEKIYOU_END IS NULL)) and TE.DELETE_FLG = 0)/*END*/
 /*IF deletechuFlg*/OR TE.DELETE_FLG = /*deletechuFlg*/0/*END*/
 /*IF tekiyougaiFlg*/OR ((SHA.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > SHA.TEKIYOU_END) and TE.DELETE_FLG = 0)/*END*/
 /*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/ )/*END*/
/*END*/
ORDER BY TE.SHAIN_CD
