SELECT DISTINCT
DELETE_FLG,
ZAIKO_HINMEI_CD,
ZAIKO_HINMEI_NAME,
ZAIKO_HINMEI_NAME_RYAKU,
ZAIKO_HINMEI_FURIGANA,
ZAIKO_TANKA,
--20150425 minhhoang edit #10156
--KAISHI_ZAIKO_RYOU,
--KAISHI_ZAIKO_KINGAKU,
--KAISHI_ZAIKO_TANKA,
--20150425 minhhoang end edit #10156
--UNIT_CD,
--UNIT_NAME,
--UNIT_NAME ZAIKO_UNIT_CD,
BIKOU,
CREATE_USER,
CREATE_DATE,
CREATE_PC,
UPDATE_USER,
UPDATE_DATE,
UPDATE_PC,
TIME_STAMP
FROM 
dbo.M_ZAIKO_HINMEI
/*BEGIN*/WHERE
/*IF data.FieldName != null && data.FieldName != '' && data.FieldName != 'default'  && data.ConditionValue != null && data.ConditionValue != ''*/
    /*IF (data.FieldName == 'CREATE_DATE' || data.FieldName == 'UPDATE_DATE')*/ 
		CONVERT(nvarchar, /*$data.FieldName*/, 111) LIKE '%' +  /*data.ConditionValue*/ + '%'
	--ELSE 
		/*$data.FieldName*/ LIKE '%' +  /*data.ConditionValue*/ + '%'
	/*END*/
/*END*/
/*IF !deletechuFlg*/AND DELETE_FLG = 0/*END*/
/*END*/
ORDER BY
ZAIKO_HINMEI_CD
