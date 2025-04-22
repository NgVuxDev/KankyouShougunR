 SELECT 
     MCDI.INPUT_KBN
	,MCD.GYOUSHA_CD
	,MCD.GENBA_CD
	,MCDI.HINMEI_CD
	,MCDI.REC_ID
	,MCDI.UNIT_CD
	,UNIT.UNIT_NAME_RYAKU
	,MCDI.KANSANCHI
	,MCDI.KANSAN_UNIT_CD
	,UNITKANSAN.UNIT_NAME_RYAKU  AS UNITKANSAN_NAME
	,MCDI.KANSAN_UNIT_MOBILE_OUTPUT_FLG
 FROM M_COURSE_DETAIL_ITEMS MCDI
 INNER JOIN M_COURSE MC
 ON MCDI.DAY_CD = MC.DAY_CD
 AND MCDI.COURSE_NAME_CD = MC.COURSE_NAME_CD
 INNER JOIN M_COURSE_DETAIL MCD
 ON MCDI.DAY_CD = MCD.DAY_CD
 AND MCDI.COURSE_NAME_CD = MCD.COURSE_NAME_CD
 AND MCDI.REC_ID = MCD.REC_ID
LEFT JOIN M_UNIT AS UNIT
ON MCDI.UNIT_CD = UNIT.UNIT_CD
LEFT JOIN M_UNIT AS UNITKANSAN
ON MCDI.KANSAN_UNIT_CD = UNITKANSAN.UNIT_CD
 /*BEGIN*/WHERE
 /*IF data.GyoushaCd != null && data.GyoushaCd != ''*/ MCD.GYOUSHA_CD = /*data.GyoushaCd*/ /*END*/
 /*IF data.GenbaCd != null && data.GenbaCd != ''*/ AND  MCD.GENBA_CD = /*data.GenbaCd*/ /*END*/
 /*IF data.HinmeiCd != null && data.HinmeiCd != ''*/ AND MCDI.HINMEI_CD = /*data.HinmeiCd*/ /*END*/
 /*IF data.UnitCd != null && data.UnitCd != 0 */ AND MCDI.UNIT_CD = /*data.UnitCd*/ /*END*/
 /*IF data.DenpyouKbnCd != null && data.DenpyouKbnCd != 0 */ AND MCDI.DENPYOU_KBN_CD = /*data.DenpyouKbnCd*/ /*END*/
 /*IF data.dayYoNi != null && data.dayYoNi != '' */ AND MC.DAY_CD = /*data.dayYoNi*/ /*END*/
 /*IF data.RoundNo != null && data.RoundNo != 0 */ AND MCD.ROUND_NO = /*data.RoundNo*/ /*END*/
 /*IF data.courseNameCd != null && data.courseNameCd != '' */ AND MCDI.COURSE_NAME_CD = /*data.courseNameCd*/ /*END*/
 /*END*/
 ORDER BY MCD.GYOUSHA_CD,MCD.GENBA_CD,MCDI.REC_ID,MCDI.REC_SEQ