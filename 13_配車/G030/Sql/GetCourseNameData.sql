SELECT * FROM dbo.M_COURSE_NAME AS MCN
INNER JOIN (SELECT * FROM M_COURSE WHERE DELETE_FLG = 0) AS MC
	ON MCN.COURSE_NAME_CD = MC.COURSE_NAME_CD
WHERE 
MCN.DELETE_FLG = 0
/*IF data.CourseNameCd != null*/
AND MCN.COURSE_NAME_CD = /*data.CourseNameCd*/
/*END*/
/*IF data.KyotenCd != null*/
AND (MCN.KYOTEN_CD = /*data.KyotenCd*/ OR MCN.KYOTEN_CD = 99)
/*END*/
/*IF data.DayCd != null*/
AND MC.DAY_CD = /*data.DayCd*/
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