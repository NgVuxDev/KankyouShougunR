SELECT * FROM dbo.M_UNIT
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF !data.UNIT_CD.IsNull*/AND UNIT_CD = /*data.UNIT_CD.Value*//*END*/
/*IF data.UNIT_NAME != null*/AND UNIT_NAME = /*data.UNIT_NAME*//*END*/
/*IF data.UNIT_NAME_RYAKU != null*/AND UNIT_NAME_RYAKU = /*data.UNIT_NAME_RYAKU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*IF !data.DENSHI_USE_KBN.IsNull*/AND DENSHI_USE_KBN = /*data.DENSHI_USE_KBN*//*END*/
/*IF !data.KAMI_USE_KBN.IsNull*/AND KAMI_USE_KBN = /*data.KAMI_USE_KBN*//*END*/