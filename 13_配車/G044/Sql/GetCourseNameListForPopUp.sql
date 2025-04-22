SELECT DISTINCT
	MCN.COURSE_NAME_CD
	,MCN.COURSE_NAME_RYAKU
	,MC.DAY_CD
	,CASE MC.DAY_CD
	 WHEN 1 THEN '月'
	 WHEN 2 THEN '火'
	 WHEN 3 THEN '水'
	 WHEN 4 THEN '木'
	 WHEN 5 THEN '金'
	 WHEN 6 THEN '土'
	 WHEN 7 THEN '日'
	 END AS DAY_NM
FROM
	M_COURSE_NAME MCN
INNER JOIN (SELECT * FROM M_COURSE WHERE DELETE_FLG = 0) AS MC
	ON MCN.COURSE_NAME_CD = MC.COURSE_NAME_CD
WHERE
    MCN.DELETE_FLG = 0
/*IF data.DayCd != 0*/
    /*IF data.DayCd == 1*/
	AND MCN.MONDAY = 1
    /*END*/
    /*IF data.DayCd == 2*/
	AND MCN.TUESDAY = 1
    /*END*/
    /*IF data.DayCd == 3*/
	AND MCN.WEDNESDAY = 1
    /*END*/
    /*IF data.DayCd == 4*/
	AND MCN.THURSDAY = 1
    /*END*/
    /*IF data.DayCd == 5*/
	AND MCN.FRIDAY = 1
    /*END*/
    /*IF data.DayCd == 6*/
	AND MCN.SATURDAY = 1
    /*END*/
    /*IF data.DayCd == 7*/
	AND MCN.SUNDAY = 1
    /*END*/
    AND MC.DAY_CD = /*data.DayCd*/
/*END*/
	/*IF data.CourseNameCd != null && data.CourseNameCd != ''*/
	AND MCN.COURSE_NAME_CD = /*data.CourseNameCd*/
	/*END*/
	/*IF data.KyotenCd != null && data.KyotenCd != '' && data.KyotenCd != 99*/
	AND(MCN.KYOTEN_CD = /*data.KyotenCd*/ OR MCN.KYOTEN_CD = 99 )
	/*END*/
ORDER BY
	MCN.COURSE_NAME_CD,MC.DAY_CD