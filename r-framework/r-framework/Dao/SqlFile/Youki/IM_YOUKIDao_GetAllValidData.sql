SELECT * FROM dbo.M_YOUKI
WHERE
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF data.YOUKI_CD != null*/AND YOUKI_CD = /*data.YOUKI_CD*//*END*/
/*IF data.YOUKI_NAME != null*/AND YOUKI_NAME = /*data.YOUKI_NAME*//*END*/
/*IF data.YOUKI_NAME_RYAKU != null*/AND YOUKI_NAME_RYAKU = /*data.YOUKI_NAME_RYAKU*//*END*/
/*IF data.YOUKI_FURIGANA != null*/AND YOUKI_FURIGANA = /*data.YOUKI_FURIGANA*//*END*/
/*IF !data.YOUKI_JYURYO.IsNull*/AND YOUKI_JYURYO = /*data.YOUKI_JYURYO.Value*//*END*/
/*IF data.YOUKI_BIKOU != null*/AND YOUKI_BIKOU = /*data.YOUKI_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
