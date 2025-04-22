SELECT
	TKD.*
FROM
	T_KENSHU_DETAIL AS TKD
WHERE 
	/*IF !data.SYSTEM_ID.IsNull*/ TKD.SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
	/*IF !data.SEQ.IsNull*/ AND TKD.SEQ = /*data.SEQ.Value*//*END*/
