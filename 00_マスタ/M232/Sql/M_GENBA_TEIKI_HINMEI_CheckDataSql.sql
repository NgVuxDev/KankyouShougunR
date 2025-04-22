SELECT
MCD.DAY_CD
FROM
	M_COURSE_DETAIL MCD
	left join  M_COURSE_DETAIL_ITEMS MCDI
	ON MCD.DAY_CD = MCDI.DAY_CD
	AND MCD.COURSE_NAME_CD = MCDI.COURSE_NAME_CD
	AND MCD.REC_ID = MCDI.REC_ID
	LEFT JOIN M_GENBA_TEIKI_HINMEI MTH
	ON MTH.GYOUSHA_CD = MCD.GYOUSHA_CD
	AND MTH.GENBA_CD = MCD.GENBA_CD
	AND MTH.HINMEI_CD = MCDI.HINMEI_CD
	AND MTH.UNIT_CD = MCDI.UNIT_CD
	WHERE
      1 = 1
      /*IF GYOUSHA_CD != null*/AND MCD.GYOUSHA_CD = /*GYOUSHA_CD*/'000001'/*END*/
      /*IF GENBA_CD != null*/AND MCD.GENBA_CD = /*GENBA_CD*/'000001'/*END*/
      /*IF DAY_CD != null*/AND MCD.DAY_CD = /*DAY_CD*//*END*/
	  /*IF COURSE_NAME_CD != null*/AND MCD.COURSE_NAME_CD = /*COURSE_NAME_CD*/'000001'/*END*/
	  /*IF REC_ID != null*/AND MCD.REC_ID = /*REC_ID*//*END*/
      /*IF dayCd != null && dayCd == '1' */AND MTH.MONDAY = 1 /*END*/
      /*IF dayCd != null && dayCd == '2' */AND MTH.TUESDAY = 1 /*END*/
      /*IF dayCd != null && dayCd == '3' */AND MTH.WEDNESDAY = 1 /*END*/
      /*IF dayCd != null && dayCd == '4' */AND MTH.THURSDAY = 1 /*END*/
      /*IF dayCd != null && dayCd == '5' */AND MTH.FRIDAY = 1 /*END*/
      /*IF dayCd != null && dayCd == '6' */AND MTH.SATURDAY = 1 /*END*/
      /*IF dayCd != null && dayCd == '7' */AND MTH.SUNDAY = 1 /*END*/