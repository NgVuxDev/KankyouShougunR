SELECT * FROM dbo.M_COURSE
WHERE 
DELETE_FLG = 0
/*IF !data.DAY_CD.IsNull*/AND DAY_CD = /*data.DAY_CD.Value*//*END*/
/*IF data.COURSE_NAME_CD != null*/AND COURSE_NAME_CD = /*data.COURSE_NAME_CD*//*END*/
/*IF data.COURSE_BIKOU != null*/AND COURSE_BIKOU = /*data.COURSE_BIKOU*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
/*IF data.SHASHU_CD != null*/AND SHASHU_CD = /*data.SHASHU_CD*//*END*/
/*IF data.SHARYOU_CD != null*/AND SHARYOU_CD = /*data.SHARYOU_CD*//*END*/
/*IF data.UNTENSHA_CD != null*/AND UNTENSHA_CD = /*data.UNTENSHA_CD*//*END*/
/*IF data.UNPAN_GYOUSHA_CD != null*/AND UNPAN_GYOUSHA_CD = /*data.UNPAN_GYOUSHA_CD*//*END*/
