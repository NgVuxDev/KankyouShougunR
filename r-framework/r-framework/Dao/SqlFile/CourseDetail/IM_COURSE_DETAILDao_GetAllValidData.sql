SELECT * FROM dbo.M_COURSE_DETAIL
WHERE 
 1 = 1
/*IF !data.DAY_CD.IsNull*/AND DAY_CD = /*data.DAY_CD.Value*//*END*/
/*IF data.COURSE_NAME_CD != null*/AND COURSE_NAME_CD = /*data.COURSE_NAME_CD*//*END*/
/*IF !data.REC_ID.IsNull*/AND REC_ID = /*data.REC_ID*//*END*/
/*IF !data.ROW_NO.IsNull*/AND ROW_NO = /*data.ROW_NO*//*END*/
/*IF !data.ROUND_NO.IsNull*/AND ROUND_NO = /*data.ROUND_NO*//*END*/
/*IF data.GYOUSHA_CD != null*/AND GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
/*IF data.GENBA_CD != null*/AND GENBA_CD = /*data.GENBA_CD*//*END*/
/*IF data.BIKOU != null*/AND BIKOU = /*data.BIKOU*//*END*/
/*IF data.TIME_STAMP != null*/AND TIME_STAMP = /*data.TIME_STAMP*//*END*/
