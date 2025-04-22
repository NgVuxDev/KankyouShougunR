SELECT * FROM M_GENBA_TEIKI_HINMEI
 where 
 /*IF data.GYOUSHA_CD != null*/ GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
 /*IF data.GENBA_CD != null*/ AND GENBA_CD = /*data.GENBA_CD*//*END*/
 /*IF data.HINMEI_CD != null*/ AND HINMEI_CD = /*data.HINMEI_CD*//*END*/
 /*IF !data.UNIT_CD.IsNull*/ AND UNIT_CD = /*data.UNIT_CD.Value*//*END*/
 /*IF !data.DENPYOU_KBN_CD.IsNull*/AND DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD.Value*//*END*/

