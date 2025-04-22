SELECT DT_R24.TUUCHI_CODE AS TUUCHI_CODE
       ,M_JW_NOTIFICATION.TYPE AS TUUCHI_NM
       ,COUNT(*) AS CNT
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
WHERE DT_R24.TUUCHI_STATUS = '2'
/*IF data.tuuchiBiFrom != null && data.tuuchiBiFrom != ''*/
AND DT_R24.TUUCHI_DATE >= /*data.tuuchiBiFrom*/ /*END*/ 
/*IF data.tuuchiBiTo != null && data.tuuchiBiTo != ''*/
AND DT_R24.TUUCHI_DATE <= /*data.tuuchiBiTo*/ /*END*/ 
/*IF data.readFlag != null && data.readFlag != ''*/
AND DT_R24.READ_FLAG = 0 /*END*/                    
GROUP BY DT_R24.TUUCHI_CODE,M_JW_NOTIFICATION.TYPE
ORDER BY DT_R24.TUUCHI_CODE

