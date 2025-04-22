SELECT * FROM dbo.T_UR_SH_ENTRY
WHERE 
1 = 1
/*IF data.SEQ.Value == 0*/
        AND DELETE_FLG = 0
-- ELSE AND SEQ = /*data.SEQ.Value*/
/*END*/
/*IF data.DAINOU_FLG.IsNull*/AND ISNULL(DAINOU_FLG,0) != 1
-- ELSE
   AND ISNULL(DAINOU_FLG,0) = 1/*data.DAINOU_FLG.Value*/
/*END*/
/*IF !data.SYSTEM_ID.IsNull*/AND SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.UR_SH_NUMBER.IsNull*/AND UR_SH_NUMBER = /*data.UR_SH_NUMBER.Value*//*END*/
ORDER BY SEQ DESC