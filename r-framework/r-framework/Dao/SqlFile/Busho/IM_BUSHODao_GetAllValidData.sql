SELECT * FROM dbo.M_BUSHO
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.BUSHO_CD != null*/AND BUSHO_CD = /*data.BUSHO_CD*//*END*/
/*IF data.BUSHO_NAME != null*/AND BUSHO_NAME = /*data.BUSHO_NAME*//*END*/
/*IF data.BUSHO_NAME_RYAKU != null*/AND BUSHO_NAME_RYAKU = /*data.BUSHO_NAME_RYAKU*//*END*/
/*IF data.BUSHO_FURIGANA != null*/AND BUSHO_FURIGANA = /*data.BUSHO_FURIGANA*//*END*/
/*IF data.BUSHO_BIKOU != null*/AND BUSHO_BIKOU = /*data.BUSHO_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
