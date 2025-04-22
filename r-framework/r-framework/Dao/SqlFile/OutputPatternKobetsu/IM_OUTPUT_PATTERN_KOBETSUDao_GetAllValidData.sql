SELECT * FROM dbo.M_OUTPUT_PATTERN_KOBETSU
WHERE 
DELETE_FLG = 0
/*IF data.SHAIN_CD != null*/AND SHAIN_CD = /*data.SHAIN_CD*//*END*/
/*IF !data.SYSTEM_ID.IsNull*/AND SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ.Value*//*END*/
/*IF !data.DEFAULT_KBN.IsNull*/AND DEFAULT_KBN = /*data.DEFAULT_KBN.Value*//*END*/
/*IF !data.DISP_NUMBER.IsNull*/AND DISP_NUMBER = /*data.DISP_NUMBER.Value*//*END*/
/*IF !data.DELETE_FLG.IsNull*/AND DELETE_FLG = /*data.DELETE_FLG.Value*//*END*/
