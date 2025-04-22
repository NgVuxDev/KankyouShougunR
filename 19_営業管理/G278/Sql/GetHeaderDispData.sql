SELECT        
    JK.CREATE_USER AS CREATE_USER, 
	JK.CREATE_DATE AS CREATE_DATE, 
	JK.UPDATE_USER AS UPDATE_USER, 
	MAX(JK.UPDATE_DATE) AS UPDATE_DATE
FROM
    (SELECT
        SH.SHAIN_CD, 
        SH.SHAIN_NAME, 
        SH.BUSHO_CD, 
        BU.BUSHO_NAME_RYAKU, 
        SH.EIGYOU_TANTOU_KBN
     FROM
        M_SHAIN AS SH LEFT OUTER JOIN
        M_BUSHO AS BU ON SH.BUSHO_CD = BU.BUSHO_CD
    WHERE
	    /*IF !deletechuFlg*/ SH.DELETE_FLG = 0/*END*/
		/*IF data.BUSHO_CD != null && data.BUSHO_CD != ""*/
        AND SH.BUSHO_CD = /*data.BUSHO_CD*//*END*/
		AND SH.EIGYOU_TANTOU_KBN = 1
    ) AS SHN 
    LEFT OUTER JOIN 
            (SELECT
                BUSHO_CD, 
                SHAIN_CD, 
                MONTH_KENSU_01, 
                MONTH_KENSU_02, 
                MONTH_KENSU_03, 
                MONTH_KENSU_04, 
                MONTH_KENSU_05, 
                MONTH_KENSU_06, 
                MONTH_KENSU_07, 
                MONTH_KENSU_08, 
                MONTH_KENSU_09, 
                MONTH_KENSU_10, 
                MONTH_KENSU_11, 
                MONTH_KENSU_12, 
                SYSTEM_ID, 
                SEQ,
				CREATE_USER, 
				CREATE_DATE, 
				UPDATE_USER, 
				UPDATE_DATE
            FROM
                T_JUCHU_M_KENSU
            WHERE
                (DELETE_FLG = 0) 
                AND (NUMBERED_YEAR = /*data.NENDO*/)
            ) AS JK 
   ON SHN.BUSHO_CD = JK.BUSHO_CD AND 
   SHN.SHAIN_CD = JK.SHAIN_CD
   GROUP BY      JK.CREATE_USER, JK.CREATE_DATE, JK.UPDATE_USER, JK.UPDATE_DATE
   ORDER BY       JK.UPDATE_DATE DESC