SELECT * FROM dbo.T_UKEIRE_DETAIL
WHERE
/*IF !data.SYSTEM_ID.IsNull*/ SYSTEM_ID = /*data.SYSTEM_ID.Value*//*END*/
/*IF !data.SEQ.IsNull*/AND SEQ = /*data.SEQ.Value*//*END*/
/*IF !data.DETAIL_SYSTEM_ID.IsNull*/AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID.Value*//*END*/
/*IF !data.UKEIRE_NUMBER.IsNull*/AND UKEIRE_NUMBER = /*data.UKEIRE_NUMBER.Value*//*END*/
