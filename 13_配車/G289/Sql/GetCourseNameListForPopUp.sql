SELECT DISTINCT
    MC.COURSE_NAME_CD,
    MCN.COURSE_NAME_RYAKU,
    MC.DAY_CD,
	CASE MC.DAY_CD
	 WHEN 1 THEN '月'
	 WHEN 2 THEN '火'
	 WHEN 3 THEN '水'
	 WHEN 4 THEN '木'
	 WHEN 5 THEN '金'
	 WHEN 6 THEN '土'
	 WHEN 7 THEN '日'
	 END AS DAY_NM
FROM M_COURSE MC
INNER JOIN (SELECT * FROM M_COURSE_NAME WHERE DELETE_FLG = 0) AS MCN
	ON MCN.COURSE_NAME_CD = MC.COURSE_NAME_CD
WHERE 
MC.DELETE_FLG = 0
/*IF dto.DayCd != 0*/
    /*IF dto.DayCd == 1*/
	AND MCN.MONDAY = 1
    /*END*/
    /*IF dto.DayCd == 2*/
	AND MCN.TUESDAY = 1
    /*END*/
    /*IF dto.DayCd == 3*/
	AND MCN.WEDNESDAY = 1
    /*END*/
    /*IF dto.DayCd == 4*/
	AND MCN.THURSDAY = 1
    /*END*/
    /*IF dto.DayCd == 5*/
	AND MCN.FRIDAY = 1
    /*END*/
    /*IF dto.DayCd == 6*/
	AND MCN.SATURDAY = 1
    /*END*/
    /*IF dto.DayCd == 7*/
	AND MCN.SUNDAY = 1
    /*END*/
    AND MC.DAY_CD = /*dto.DayCd*/
/*END*/
/*IF data.DayCd == null || data.DayCd == ''*/
    AND (
        CASE MC.DAY_CD
            WHEN 1 THEN MCN.MONDAY
            WHEN 2 THEN MCN.TUESDAY
            WHEN 3 THEN MCN.WEDNESDAY
            WHEN 4 THEN MCN.THURSDAY
            WHEN 5 THEN MCN.FRIDAY
            WHEN 6 THEN MCN.SATURDAY
            WHEN 7 THEN MCN.SUNDAY
            ELSE 0
        END
    ) = 1
/*END*/
/*IF dto.CourseNameCd != null && dto.CourseNameCd != ''*/
AND MC.COURSE_NAME_CD = /*dto.CourseNameCd*/''
/*END*/
/*IF dto.KyotenCd != null && dto.KyotenCd != '' && dto.KyotenCd != '99'*/
AND MCN.KYOTEN_CD = /*dto.KyotenCd*/0
/*END*/
ORDER BY MC.COURSE_NAME_CD,MC.DAY_CD