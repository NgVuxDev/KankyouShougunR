SELECT        
    SHN.SHAIN_CD AS EIGYOU_CD, 
    SHN.SHAIN_NAME_RYAKU AS EIGYOU_NAME, 
    SHN.BUSHO_CD AS BUSHO_CD, 
    SHN.BUSHO_NAME_RYAKU AS BUSHO_NAME, 
    ISNULL(JK.MONTH_YOSAN_01,0) AS MONTH1, 
    ISNULL(JK.MONTH_YOSAN_02,0) AS MONTH2, 
    ISNULL(JK.MONTH_YOSAN_03,0) AS MONTH3, 
    ISNULL(JK.MONTH_YOSAN_04,0) AS MONTH4, 
    ISNULL(JK.MONTH_YOSAN_05,0) AS MONTH5, 
    ISNULL(JK.MONTH_YOSAN_06,0) AS MONTH6, 
    ISNULL(JK.MONTH_YOSAN_07,0) AS MONTH7, 
    ISNULL(JK.MONTH_YOSAN_08,0) AS MONTH8, 
    ISNULL(JK.MONTH_YOSAN_09,0) AS MONTH9, 
    ISNULL(JK.MONTH_YOSAN_10,0) AS MONTH10, 
    ISNULL(JK.MONTH_YOSAN_11,0) AS MONTH11, 
    ISNULL(JK.MONTH_YOSAN_12,0) AS MONTH12, 
	(  ISNULL(JK.MONTH_YOSAN_01, 0) + ISNULL(JK.MONTH_YOSAN_02, 0) + ISNULL(JK.MONTH_YOSAN_03, 0) + ISNULL(JK.MONTH_YOSAN_04, 0) 
     + ISNULL(JK.MONTH_YOSAN_05, 0) + ISNULL(JK.MONTH_YOSAN_06, 0) + ISNULL(JK.MONTH_YOSAN_07, 0) + ISNULL(JK.MONTH_YOSAN_08, 0) 
     + ISNULL(JK.MONTH_YOSAN_09, 0) + ISNULL(JK.MONTH_YOSAN_10, 0) + ISNULL(JK.MONTH_YOSAN_11, 0) + ISNULL(JK.MONTH_YOSAN_12, 0)
     ) AS GOUKEI, 
    JK.SYSTEM_ID, 
    JK.SEQ, 
	JK.TIME_STAMP, 
	JK.CREATE_USER,
    JK.CREATE_DATE,
    JK.CREATE_PC,
	JK.UPDATE_DATE,
	JK.UPDATE_USER,
    JK.UPDATE_PC
FROM
    (SELECT
        SH.SHAIN_CD, 
        SH.SHAIN_NAME_RYAKU, 
        SH.BUSHO_CD, 
        BU.BUSHO_NAME_RYAKU, 
        SH.EIGYOU_TANTOU_KBN
     FROM
        M_SHAIN AS SH LEFT OUTER JOIN
        M_BUSHO AS BU ON SH.BUSHO_CD = BU.BUSHO_CD
    WHERE
        SH.EIGYOU_TANTOU_KBN = 1
	    /*IF !deletechuFlg*/ AND SH.DELETE_FLG = 0 /*END*/
		/*IF data.BUSHO_CD != null && data.BUSHO_CD != ""*/ AND SH.BUSHO_CD = /*data.BUSHO_CD*/0 /*END*/
    ) AS SHN 
    LEFT OUTER JOIN 
            (SELECT
                TJ1.BUSHO_CD, 
                TJ1.SHAIN_CD, 
                TJ1.MONTH_YOSAN_01, 
                TJ1.MONTH_YOSAN_02, 
                TJ1.MONTH_YOSAN_03, 
                TJ1.MONTH_YOSAN_04, 
                TJ1.MONTH_YOSAN_05, 
                TJ1.MONTH_YOSAN_06, 
                TJ1.MONTH_YOSAN_07, 
                TJ1.MONTH_YOSAN_08, 
                TJ1.MONTH_YOSAN_09, 
                TJ1.MONTH_YOSAN_10, 
                TJ1.MONTH_YOSAN_11, 
                TJ1.MONTH_YOSAN_12, 
                TJ1.SYSTEM_ID, 
                TJ1.SEQ,
                TJ1.CREATE_USER,
                TJ1.CREATE_DATE,
				TJ1.CREATE_PC, 
                TJ1.UPDATE_USER,
                TJ1.UPDATE_DATE, 
                TJ1.UPDATE_PC, 
                CAST(TJ1.TIME_STAMP AS int) AS TIME_STAMP 
            FROM
                T_EIGYO_YOSAN AS TJ1 RIGHT OUTER JOIN
				(SELECT
                           NUMBERED_YEAR, 
                           BUSHO_CD, 
                           SHAIN_CD, 
                           SYSTEM_ID, 
                           MAX(SEQ) AS SEQ
                       FROM
                           T_EIGYO_YOSAN
                       WHERE
                           (DELETE_FLG = 0) 
                           AND (NUMBERED_YEAR = /*data.NENDO*/)
                           
                      GROUP BY
                           NUMBERED_YEAR, BUSHO_CD, SHAIN_CD, SYSTEM_ID
                   ) TJ2
                   ON TJ1.BUSHO_CD = TJ2.BUSHO_CD 
				   AND TJ1.SHAIN_CD = TJ2.SHAIN_CD 
				   AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID 
				   AND TJ1.SEQ = TJ2.SEQ
            WHERE
                (TJ1.DELETE_FLG = 0) 
                AND (TJ1.NUMBERED_YEAR = /*data.NENDO*/)
                /*IF data.DENPYOU_KBN_CD == '1'*/and TJ1.DENPYOU_KBN_CD =  1/*END*/
                /*IF data.DENPYOU_KBN_CD == '2'*/and TJ1.DENPYOU_KBN_CD =  2/*END*/
            ) AS JK 
   ON SHN.BUSHO_CD = JK.BUSHO_CD AND 
   SHN.SHAIN_CD = JK.SHAIN_CD
   ORDER BY BUSHO_CD,EIGYOU_CD