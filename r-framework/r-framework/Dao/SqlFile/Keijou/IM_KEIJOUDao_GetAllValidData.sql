﻿SELECT * FROM dbo.M_KEIJOU
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.KEIJOU_CD != null*/AND KEIJOU_CD = /*data.KEIJOU_CD*//*END*/
/*IF data.KEIJOU_NAME != null*/AND KEIJOU_NAME = /*data.KEIJOU_NAME*//*END*/
/*IF data.KEIJOU_NAME_RYAKU != null*/AND KEIJOU_NAME_RYAKU = /*data.KEIJOU_NAME_RYAKU*//*END*/
/*IF data.KEIJOU_BIKOU != null*/AND KEIJOU_BIKOU = /*data.KEIJOU_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
