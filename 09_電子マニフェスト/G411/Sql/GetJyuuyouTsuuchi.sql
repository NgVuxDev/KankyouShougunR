SELECT T1.TUUCHI_CODE,T1.TUUCHI_NM,COUNT(*) AS DATA_CNT
FROM 
(
SELECT DT_R24.TUUCHI_CODE AS TUUCHI_CODE
       ,M_JW_NOTIFICATION.TYPE AS TUUCHI_NM
From DT_R24 
               INNER JOIN M_JW_NOTIFICATION
                     ON DT_R24.TUUCHI_CODE = M_JW_NOTIFICATION.NOTIFICATION_CODE
                    AND M_JW_NOTIFICATION.DELETE_FLG = '0'
               INNER JOIN DT_MF_TOC
                     ON DT_R24.MANIFEST_ID = DT_MF_TOC.MANIFEST_ID
               INNER JOIN DT_R18
                     ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID
                    AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ
               INNER JOIN MS_JWNET_MEMBER
                     ON DT_R24.MEMBER_ID = MS_JWNET_MEMBER.EDI_MEMBER_ID
			   INNER JOIN DT_R18_EX
					 ON DT_R18_EX.KANRI_ID = DT_R18.KANRI_ID AND DT_R18_EX.DELETE_FLG = 0
WHERE DT_R24.TUUCHI_STATUS = '1'
AND   DT_R24.TUUCHI_CODE NOT IN (110, 113, 118, 121, 203, 206, 303, 306)    
/*IF data.tuuchiBiFrom != null && data.tuuchiBiFrom != ''*/
AND DT_R24.TUUCHI_DATE >= /*data.tuuchiBiFrom*/ /*END*/ 
/*IF data.tuuchiBiTo != null && data.tuuchiBiTo != ''*/
AND DT_R24.TUUCHI_DATE <= /*data.tuuchiBiTo*/ /*END*/ 
/*IF data.readFlag != null && data.readFlag == '2'*/
AND DT_R24.READ_FLAG = 0 /*END*/ 
UNION ALL
SELECT DT_R24.TUUCHI_CODE AS TUUCHI_CODE
      ,M_JW_NOTIFICATION.TYPE AS TUUCHI_NM
From DT_R24 
               INNER JOIN M_JW_NOTIFICATION
                     ON DT_R24.TUUCHI_CODE = M_JW_NOTIFICATION.NOTIFICATION_CODE
                    AND M_JW_NOTIFICATION.DELETE_FLG = '0'
               INNER JOIN DT_MF_TOC
                     ON DT_R24.MANIFEST_ID = DT_MF_TOC.MANIFEST_ID
               INNER JOIN DT_R18
                     ON DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID
                    AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ
               INNER JOIN MS_JWNET_MEMBER
                     ON DT_R24.MEMBER_ID = MS_JWNET_MEMBER.EDI_MEMBER_ID
			   INNER JOIN DT_R18_EX
					 ON DT_R18_EX.KANRI_ID = DT_R18.KANRI_ID AND DT_R18_EX.DELETE_FLG = 0
WHERE DT_R24.TUUCHI_STATUS = '1'
AND   DT_R24.TUUCHI_CODE IN (110, 113, 118, 121, 203, 206, 303, 306) 
AND   (DT_R24.ACTION_FLAG IS NULL OR DT_R24.ACTION_FLAG = 0)
AND   DT_MF_TOC.APPROVAL_SEQ IS NOT NULL
/*IF data.tuuchiBiFrom != null && data.tuuchiBiFrom != ''*/
AND   DT_R24.TUUCHI_DATE >= /*data.tuuchiBiFrom*/ /*END*/ 
/*IF data.tuuchiBiTo != null && data.tuuchiBiTo != ''*/
AND   DT_R24.TUUCHI_DATE <= /*data.tuuchiBiTo*/ /*END*/ 
/*IF data.readFlag != null && data.readFlag != ''*/
AND   DT_R24.READ_FLAG = 0 /*END*/ 
) AS T1                    
GROUP BY T1.TUUCHI_CODE,T1.TUUCHI_NM
ORDER BY T1.TUUCHI_CODE