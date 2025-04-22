SELECT * FROM dbo.T_UR_SH_ENTRY
WHERE DELETE_FLG = 0 
/*IF !data.SYSTEM_ID.IsNull*/AND SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ.Value*//*END*/
/*IF !data.UR_SH_NUMBER.IsNull*/AND UR_SH_NUMBER = /*data.UR_SH_NUMBER.Value*//*END*/
ORDER BY SEQ DESC