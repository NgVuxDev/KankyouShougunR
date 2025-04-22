SELECT 
    MUT.*
FROM 
    dbo.M_UNCHIN_TANKA MUT
/*BEGIN*/WHERE
 /*IF data.UNPAN_GYOUSHA_CD != null*/MUT.UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/''/*END*/
 /*IF data.UNCHIN_HINMEI_CD != null*/AND MUT.UNCHIN_HINMEI_CD = /*data.UNCHIN_HINMEI_CD*/''/*END*/
 /*IF data.UNIT_CD != null*/AND MUT.UNIT_CD = /*data.UNIT_CD*/''/*END*/
 /*IF data.TANKA != null*/AND MUT.TANKA = /*data.TANKA*/0/*END*/
 /*IF data.SHASHU_CD != null*/AND MUT.SHASHU_CD = /*data.SHASHU_CD*/''/*END*/
 /*IF !deletechuFlg*/AND MUT.DELETE_FLG = 0/*END*/
 /*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/AND (1 = 0/*END*/
 /*IF tekiyounaiFlg*/OR (((MUT.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MUT.TEKIYOU_END) or (MUT.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and MUT.TEKIYOU_END IS NULL) or (MUT.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MUT.TEKIYOU_END) or (MUT.TEKIYOU_BEGIN IS NULL and MUT.TEKIYOU_END IS NULL)) and MUT.DELETE_FLG = 0)/*END*/
 /*IF deletechuFlg*/OR MUT.DELETE_FLG = /*deletechuFlg*/0/*END*/
 /*IF tekiyougaiFlg*/OR ((MUT.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > MUT.TEKIYOU_END) and MUT.DELETE_FLG = 0)/*END*/
 /*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/ )/*END*/
/*END*/
ORDER BY MUT.UNCHIN_HINMEI_CD
