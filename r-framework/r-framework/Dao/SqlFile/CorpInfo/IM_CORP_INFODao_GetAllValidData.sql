SELECT * FROM dbo.M_CORP_INFO
WHERE 
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
 DELETE_FLG = 0
-- ELSE
 1 = 1
/*END*/
/*IF !data.SYS_ID.IsNull*/AND SYS_ID = /*data.SYS_ID.Value*//*END*/
/*IF data.CORP_NAME != null*/AND CORP_NAME = /*data.CORP_NAME*//*END*/
/*IF data.CORP_RYAKU_NAME != null*/AND CORP_RYAKU_NAME = /*data.CORP_RYAKU_NAME*//*END*/
/*IF data.CORP_FURIGANA != null*/AND CORP_FURIGANA = /*data.CORP_FURIGANA*//*END*/
/*IF data.CORP_DAIHYOU != null*/AND CORP_DAIHYOU = /*data.CORP_DAIHYOU*//*END*/
/*IF !data.KISHU_MONTH.IsNull*/AND KISHU_MONTH = /*data.KISHU_MONTH.Value*//*END*/
/*IF !data.SHIMEBI.IsNull*/AND SHIMEBI = /*data.SHIMEBI.Value*//*END*/
/*IF !data.SHIHARAI_MONTH.IsNull*/AND SHIHARAI_MONTH = /*data.SHIHARAI_MONTH.Value*//*END*/
/*IF !data.SHIHARAI_DAY.IsNull*/AND SHIHARAI_DAY = /*data.SHIHARAI_DAY.Value*//*END*/
/*IF !data.SHIHARAI_HOUHOU.IsNull*/AND SHIHARAI_HOUHOU = /*data.SHIHARAI_HOUHOU.Value*//*END*/
/*IF data.BANK_CD != null*/AND BANK_CD = /*data.BANK_CD*//*END*/
/*IF data.BANK_SHITEN_CD != null*/AND BANK_SHITEN_CD = /*data.BANK_SHITEN_CD*//*END*/
/*IF data.KOUZA_SHURUI != null*/AND KOUZA_SHURUI = /*data.KOUZA_SHURUI*//*END*/
/*IF data.KOUZA_NO != null*/AND KOUZA_NO = /*data.KOUZA_NO*//*END*/
/*IF data.KOUZA_NAME != null*/AND KOUZA_NAME = /*data.KOUZA_NAME*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
