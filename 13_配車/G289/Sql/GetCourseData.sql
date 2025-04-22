 SELECT *
 FROM M_COURSE MC
 WHERE
	MC.DELETE_FLG = 0
	/*IF data.dayCd != 0*/AND MC.DAY_CD = /*data.dayCd*//*END*/
	AND MC.COURSE_NAME_CD = /*data.courseNameCd*/
 