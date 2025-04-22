SELECT * FROM T_UKETSUKE_SK_DETAIL
WHERE
/*IF data.SYSTEM_ID != null*/ (SYSTEM_ID = /*data.SYSTEM_ID*/)/*END*/
/*IF data.SEQ != null*/ AND (SEQ = /*data.SEQ*/)/*END*/
/*IF data.UKETSUKE_NUMBER != null*/ AND (UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/)/*END*/
/*IF data.ROW_NO != 0*/ AND (ROW_NO = /*data.ROW_NO*/) /*END*/
/*IF data.HINMEI_CD != null*/ AND (HINMEI_CD = /*data.HINMEI_CD*/) /*END*/
/*IF data.UNIT_CD != 0*/ AND (UNIT_CD = /*data.UNIT_CD*/) /*END*/
