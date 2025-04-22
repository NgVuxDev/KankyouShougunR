SELECT * FROM dbo.M_UNCHIN_TANKA
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.UNPAN_GYOUSHA_CD != null*/AND UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*/''/*END*/
/*IF data.UNPAN_GYOUSHA_NAME != null*/AND UNPAN_GYOUSHA_NAME = /*data.UNPAN_GYOUSHA_NAME*/''/*END*/
/*IF data.UNCHIN_HINMEI_CD != null*/AND UNCHIN_HINMEI_CD = /*data.UNCHIN_HINMEI_CD*/''/*END*/
/*IF data.UNCHIN_HINMEI_NAME != null*/AND UNCHIN_HINMEI_NAME = /*data.UNCHIN_HINMEI_NAME*/''/*END*/
/*IF !data.UNIT_CD.IsNull*/AND UNIT_CD = /*data.UNIT_CD.Value*/''/*END*/
/*IF data.UNIT_NAME != null*/AND UNIT_NAME = /*data.UNIT_NAME*/''/*END*/
/*IF !data.TANKA.IsNull*/AND TANKA = /*data.TANKA.Value*/''/*END*/
/*IF data.SHASHU_CD != null*/AND SHASHU_CD = /*data.SHASHU_CD*/''/*END*/
/*IF data.SHASHU_NAME != null*/AND SHASHU_NAME = /*data.SHASHU_NAME*/''/*END*/
/*IF data.BIKOU != null*/AND BIKOU = /*data.BIKOU*/''/*END*/
