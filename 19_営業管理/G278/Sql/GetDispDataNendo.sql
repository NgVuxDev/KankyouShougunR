SELECT
         JUCHU.SHAIN_CD AS EIGYOU_CD, 
         JUCHU.SHAIN_NAME AS EIGYOU_NAME, 
         JUCHU.BUSHO_CD AS BUSHO_CD, 
         JUCHU.BUSHO_NAME_RYAKU AS BUSHO_NAME, 
         ISNULL(JUCHU.YOTEI_NENDO_01,0) AS YOTEI_NENDO01, 
         ISNULL(JUCHU.YOTEI_NENDO_02,0) AS YOTEI_NENDO02, 
         ISNULL(JUCHU.YOTEI_NENDO_03,0) AS YOTEI_NENDO03, 
         ISNULL(JUCHU.YOTEI_NENDO_04,0) AS YOTEI_NENDO04, 
         ISNULL(JUCHU.YOTEI_NENDO_05,0) AS YOTEI_NENDO05, 
         ISNULL(JUCHU.YOTEI_NENDO_06,0) AS YOTEI_NENDO06, 
         ISNULL(JUCHU.YOTEI_NENDO_07,0) AS YOTEI_NENDO07, 
         ISNULL(JUCHU.YOTEI_NENDO_08,0) AS YOTEI_NENDO08, 
         ISNULL(JUCHU.YOTEI_NENDO_09,0) AS YOTEI_NENDO09, 
        (ISNULL(JUCHU.YOTEI_NENDO_01, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_02, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_03, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_04, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_05, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_06, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_07, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_08, 0) + 
          ISNULL(JUCHU.YOTEI_NENDO_09, 0)) AS YOTEI_GOUKEI,
          ISNULL(M01.NENDO_01,0) AS MITSUMORI_NENDO01,
          ISNULL(M02.NENDO_02,0) AS MITSUMORI_NENDO02,
          ISNULL(M03.NENDO_03,0) AS MITSUMORI_NENDO03,
          ISNULL(M04.NENDO_04,0) AS MITSUMORI_NENDO04,
          ISNULL(M05.NENDO_05,0) AS MITSUMORI_NENDO05,
          ISNULL(M06.NENDO_06,0) AS MITSUMORI_NENDO06,
          ISNULL(M07.NENDO_07,0) AS MITSUMORI_NENDO07,
          ISNULL(M08.NENDO_08,0) AS MITSUMORI_NENDO08,
          ISNULL(M09.NENDO_09,0) AS MITSUMORI_NENDO09,
          (ISNULL(M01.NENDO_01,0) +
           ISNULL(M02.NENDO_02,0) +
           ISNULL(M03.NENDO_03,0) +
           ISNULL(M04.NENDO_04,0) +
           ISNULL(M05.NENDO_05,0) +
           ISNULL(M06.NENDO_06,0) +
           ISNULL(M07.NENDO_07,0) +
           ISNULL(M08.NENDO_08,0) +
           ISNULL(M09.NENDO_09,0)) AS MITSUMORI_GOUKEI
     FROM
         (SELECT 
             SHN.SHAIN_CD, 
             SHN.SHAIN_NAME, 
             SHN.BUSHO_CD, 
             SHN.BUSHO_NAME_RYAKU,
             NEN01.YOTEI_NENDO_01,
             NEN02.YOTEI_NENDO_02,
             NEN03.YOTEI_NENDO_03,
             NEN04.YOTEI_NENDO_04,
             NEN05.YOTEI_NENDO_05,
             NEN06.YOTEI_NENDO_06,
             NEN07.YOTEI_NENDO_07,
             NEN08.YOTEI_NENDO_08,
             NEN09.YOTEI_NENDO_09
         FROM
             (SELECT
                 SH.SHAIN_CD, 
                 SH.SHAIN_NAME_RYAKU AS SHAIN_NAME, 
                 SH.BUSHO_CD, 
                 BU.BUSHO_NAME_RYAKU, 
                 SH.EIGYOU_TANTOU_KBN
              FROM
                 M_SHAIN AS SH LEFT OUTER JOIN
                 M_BUSHO AS BU ON SH.BUSHO_CD = BU.BUSHO_CD
             WHERE
                 /*IF !deletechuFlg*/ SH.DELETE_FLG = 0/*END*/
                 /*IF data.BUSHO_CD != null && data.BUSHO_CD != ""*/
                 AND SH.BUSHO_CD = /*data.BUSHO_CD*/null/*END*/
                 AND SH.EIGYOU_TANTOU_KBN = 1
             ) AS SHN 
             LEFT OUTER JOIN 
                     (
                    SELECT
                        N1.BUSHO_CD,
                        N1.SHAIN_CD,
                        (
                        /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                        /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                        /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                        /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                        /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                        /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                        /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                        /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                        /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                        /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                        /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                        /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                        /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                        /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                        /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                        /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                        /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                        /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                        /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                        /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                        /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                        /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                        /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                        /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                        0) AS YOTEI_NENDO_01
                    FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                (
                                    SELECT
                                        NUMBERED_YEAR,
                                        BUSHO_CD,
                                        SHAIN_CD,
                                        SYSTEM_ID,
                                        MAX (SEQ) AS SEQ
                            FROM
                                T_JUCHU_M_KENSU
                            WHERE
                                (DELETE_FLG = 0)
                                AND (NUMBERED_YEAR = /*data.NENDO_01*/null)
                            GROUP BY
                                NUMBERED_YEAR,
                                BUSHO_CD,
                                SHAIN_CD,
                                SYSTEM_ID
                                ) TJ2
                        ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                        AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                        AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                        AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                                AND (TJ1.NUMBERED_YEAR = /*data.NENDO_01*/null)
                        ) N1
                        LEFT JOIN
                        (
                            SELECT
                                TJ3.BUSHO_CD,
                                TJ3.SHAIN_CD,
                                ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ3
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.JINEN_01*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ4
                                ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                AND TJ3.SEQ = TJ4.SEQ
                            WHERE
                                (TJ3.DELETE_FLG = 0)
                            AND (TJ3.NUMBERED_YEAR = /*data.JINEN_01*/null)
                        ) N2
                    ON  N1.BUSHO_CD = N2.BUSHO_CD
                    AND N1.SHAIN_CD = N2.SHAIN_CD
                )
                     ) AS NEN01
             ON SHN.BUSHO_CD = NEN01.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN01.SHAIN_CD
             LEFT OUTER JOIN 
                 (SELECT
                    N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_02
                FROM
                (
                    (
                        SELECT
                            TJ1.BUSHO_CD,
                            TJ1.SHAIN_CD,
                            ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                            ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                            ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                            ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                            ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                            ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                            ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                            ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                            ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                            ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                            ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                            ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                        FROM
                            T_JUCHU_M_KENSU AS TJ1
                            RIGHT OUTER JOIN
                                (
                                    SELECT
                                        NUMBERED_YEAR,
                                        BUSHO_CD,
                                        SHAIN_CD,
                                        SYSTEM_ID,
                                        MAX (SEQ) AS SEQ
                                    FROM
                                        T_JUCHU_M_KENSU
                                    WHERE
                                        (DELETE_FLG = 0)
                                    AND (NUMBERED_YEAR = /*data.NENDO_02*/null)
                                    GROUP BY
                                        NUMBERED_YEAR,
                                        BUSHO_CD,
                                        SHAIN_CD,
                                        SYSTEM_ID
                                ) TJ2
                            ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                            AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                            AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                            AND TJ1.SEQ = TJ2.SEQ
                        WHERE
                            (TJ1.DELETE_FLG = 0)
                        AND (TJ1.NUMBERED_YEAR = /*data.NENDO_02*/null)
                    ) N1
                    LEFT JOIN
                        (
                            SELECT
                                TJ3.BUSHO_CD,
                                TJ3.SHAIN_CD,
                                ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ3
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.JINEN_02*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ4
                                ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                AND TJ3.SEQ = TJ4.SEQ
                            WHERE
                                (TJ3.DELETE_FLG = 0)
                            AND (TJ3.NUMBERED_YEAR = /*data.JINEN_02*/null)
                        ) N2
                    ON  N1.BUSHO_CD = N2.BUSHO_CD
                    AND N1.SHAIN_CD = N2.SHAIN_CD
                )
                     ) AS NEN02
             ON SHN.BUSHO_CD = NEN02.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN02.SHAIN_CD
			 
             LEFT OUTER JOIN 
                 (SELECT
                     N1.BUSHO_CD,
                     N1.SHAIN_CD,
                     (
                     /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                     /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                     /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                     /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                     /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                     /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                     /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                     /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                     /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                     /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                     /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                     /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                     /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                     /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                     /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                     /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                     /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                     /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                     /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                     /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                     /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                     /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                     /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                     /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                     0) AS YOTEI_NENDO_03
                FROM
                    (
                         (
                             SELECT
                                 TJ1.BUSHO_CD,
                                 TJ1.SHAIN_CD,
                                 ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                 ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                 ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                 ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                 ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                 ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                 ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                 ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                 ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                 ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                 ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                 ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                             FROM
                                 T_JUCHU_M_KENSU AS TJ1
                                 RIGHT OUTER JOIN
                                     (
                                         SELECT
                                             NUMBERED_YEAR,
                                             BUSHO_CD,
                                             SHAIN_CD,
                                             SYSTEM_ID,
                                             MAX (SEQ) AS SEQ
                                         FROM
                                             T_JUCHU_M_KENSU
                                         WHERE
                                             (DELETE_FLG = 0)
                                         AND (NUMBERED_YEAR = /*data.NENDO_03*/null)
                                         GROUP BY
                                             NUMBERED_YEAR,
                                             BUSHO_CD,
                                             SHAIN_CD,
                                             SYSTEM_ID
                                     ) TJ2
                                 ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                 AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                 AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                 AND TJ1.SEQ = TJ2.SEQ
                             WHERE
                                 (TJ1.DELETE_FLG = 0)
                             AND (TJ1.NUMBERED_YEAR = /*data.NENDO_03*/null)
                         ) N1
                         LEFT JOIN
                             (
                                 SELECT
                                     TJ3.BUSHO_CD,
                                     TJ3.SHAIN_CD,
                                     ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                     ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                     ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                     ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                     ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                     ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                     ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                     ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                     ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                     ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                     ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                     ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                 FROM
                                     T_JUCHU_M_KENSU AS TJ3
                                     RIGHT OUTER JOIN
                                         (
                                             SELECT
                                                 NUMBERED_YEAR,
                                                 BUSHO_CD,
                                                 SHAIN_CD,
                                                 SYSTEM_ID,
                                                 MAX (SEQ) AS SEQ
                                             FROM
                                                 T_JUCHU_M_KENSU
                                             WHERE
                                                 (DELETE_FLG = 0)
                                             AND (NUMBERED_YEAR = /*data.JINEN_03*/null)
                                             GROUP BY
                                                 NUMBERED_YEAR,
                                                 BUSHO_CD,
                                                 SHAIN_CD,
                                                 SYSTEM_ID
                                         ) TJ4
                                     ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                     AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                     AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                     AND TJ3.SEQ = TJ4.SEQ
                                 WHERE
                                     (TJ3.DELETE_FLG = 0)
                                 AND (TJ3.NUMBERED_YEAR = /*data.JINEN_03*/null)
                             ) N2
                         ON  N1.BUSHO_CD = N2.BUSHO_CD
                         AND N1.SHAIN_CD = N2.SHAIN_CD
                     )
                     ) AS NEN03
             ON SHN.BUSHO_CD = NEN03.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN03.SHAIN_CD 
             LEFT OUTER JOIN 
                 (SELECT
                    N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_04
                FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.NENDO_04*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ2
                                ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                            AND (TJ1.NUMBERED_YEAR = /*data.NENDO_04*/null)
                        ) N1
                        LEFT JOIN
                            (
                                SELECT
                                    TJ3.BUSHO_CD,
                                    TJ3.SHAIN_CD,
                                    ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                    ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                    ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                    ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                    ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                    ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                    ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                    ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                    ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                    ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                    ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                    ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                FROM
                                    T_JUCHU_M_KENSU AS TJ3
                                    RIGHT OUTER JOIN
                                        (
                                            SELECT
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID,
                                                MAX (SEQ) AS SEQ
                                            FROM
                                                T_JUCHU_M_KENSU
                                            WHERE
                                                (DELETE_FLG = 0)
                                            AND (NUMBERED_YEAR = /*data.JINEN_04*/null)
                                            GROUP BY
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID
                                        ) TJ4
                                    ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                    AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                    AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                    AND TJ3.SEQ = TJ4.SEQ
                                WHERE
                                    (TJ3.DELETE_FLG = 0)
                                AND (TJ3.NUMBERED_YEAR = /*data.JINEN_04*/null)
                            ) N2
                        ON  N1.BUSHO_CD = N2.BUSHO_CD
                        AND N1.SHAIN_CD = N2.SHAIN_CD
                    )
                     ) AS NEN04
             ON SHN.BUSHO_CD = NEN04.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN04.SHAIN_CD
  
             LEFT OUTER JOIN 
                 (SELECT
                    N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_05
                FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.NENDO_05*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ2
                                ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                            AND (TJ1.NUMBERED_YEAR = /*data.NENDO_05*/null)
                        ) N1
                        LEFT JOIN
                            (
                                SELECT
                                    TJ3.BUSHO_CD,
                                    TJ3.SHAIN_CD,
                                    ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                    ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                    ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                    ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                    ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                    ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                    ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                    ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                    ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                    ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                    ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                    ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                FROM
                                    T_JUCHU_M_KENSU AS TJ3
                                    RIGHT OUTER JOIN
                                        (
                                            SELECT
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID,
                                                MAX (SEQ) AS SEQ
                                            FROM
                                                T_JUCHU_M_KENSU
                                            WHERE
                                                (DELETE_FLG = 0)
                                            AND (NUMBERED_YEAR = /*data.JINEN_05*/null)
                                            GROUP BY
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID
                                        ) TJ4
                                    ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                    AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                    AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                    AND TJ3.SEQ = TJ4.SEQ
                                WHERE
                                    (TJ3.DELETE_FLG = 0)
                                AND (TJ3.NUMBERED_YEAR = /*data.JINEN_05*/null)
                            ) N2
                        ON  N1.BUSHO_CD = N2.BUSHO_CD
                        AND N1.SHAIN_CD = N2.SHAIN_CD
                    )
                     ) AS NEN05
             ON SHN.BUSHO_CD = NEN05.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN05.SHAIN_CD
  
             LEFT OUTER JOIN 
                 (SELECT
                    N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_06
                FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.NENDO_06*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ2
                                ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                            AND (TJ1.NUMBERED_YEAR = /*data.NENDO_06*/null)
                        ) N1
                        LEFT JOIN
                            (
                                SELECT
                                    TJ3.BUSHO_CD,
                                    TJ3.SHAIN_CD,
                                    ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                    ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                    ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                    ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                    ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                    ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                    ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                    ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                    ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                    ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                    ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                    ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                FROM
                                    T_JUCHU_M_KENSU AS TJ3
                                    RIGHT OUTER JOIN
                                        (
                                            SELECT
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID,
                                                MAX (SEQ) AS SEQ
                                            FROM
                                                T_JUCHU_M_KENSU
                                            WHERE
                                                (DELETE_FLG = 0)
                                            AND (NUMBERED_YEAR = /*data.JINEN_06*/null)
                                            GROUP BY
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID
                                        ) TJ4
                                    ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                    AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                    AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                    AND TJ3.SEQ = TJ4.SEQ
                                WHERE
                                    (TJ3.DELETE_FLG = 0)
                                AND (TJ3.NUMBERED_YEAR = /*data.JINEN_06*/null)
                            ) N2
                        ON  N1.BUSHO_CD = N2.BUSHO_CD
                        AND N1.SHAIN_CD = N2.SHAIN_CD
                    )
                     ) AS NEN06
             ON SHN.BUSHO_CD = NEN06.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN06.SHAIN_CD
  
             LEFT OUTER JOIN 
                 (SELECT
                            N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_07
                FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.NENDO_07*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ2
                                ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                            AND (TJ1.NUMBERED_YEAR = /*data.NENDO_07*/null)
                        ) N1
                        LEFT JOIN
                            (
                                SELECT
                                    TJ3.BUSHO_CD,
                                    TJ3.SHAIN_CD,
                                    ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                    ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                    ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                    ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                    ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                    ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                    ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                    ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                    ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                    ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                    ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                    ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                FROM
                                    T_JUCHU_M_KENSU AS TJ3
                                    RIGHT OUTER JOIN
                                        (
                                            SELECT
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID,
                                                MAX (SEQ) AS SEQ
                                            FROM
                                                T_JUCHU_M_KENSU
                                            WHERE
                                                (DELETE_FLG = 0)
                                            AND (NUMBERED_YEAR = /*data.JINEN_07*/null)
                                            GROUP BY
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID
                                        ) TJ4
                                    ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                    AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                    AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                    AND TJ3.SEQ = TJ4.SEQ
                                WHERE
                                    (TJ3.DELETE_FLG = 0)
                                AND (TJ3.NUMBERED_YEAR = /*data.JINEN_07*/null)
                            ) N2
                        ON  N1.BUSHO_CD = N2.BUSHO_CD
                        AND N1.SHAIN_CD = N2.SHAIN_CD
                    )
                     ) AS NEN07
             ON SHN.BUSHO_CD = NEN07.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN07.SHAIN_CD
  
             LEFT OUTER JOIN 
                 (SELECT
                    N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_08
                FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.NENDO_08*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ2
                                ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                            AND (TJ1.NUMBERED_YEAR = /*data.NENDO_08*/null)
                        ) N1
                        LEFT JOIN
                            (
                                SELECT
                                    TJ3.BUSHO_CD,
                                    TJ3.SHAIN_CD,
                                    ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                    ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                    ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                    ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                    ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                    ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                    ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                    ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                    ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                    ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                    ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                    ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                FROM
                                    T_JUCHU_M_KENSU AS TJ3
                                    RIGHT OUTER JOIN
                                        (
                                            SELECT
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID,
                                                MAX (SEQ) AS SEQ
                                            FROM
                                                T_JUCHU_M_KENSU
                                            WHERE
                                                (DELETE_FLG = 0)
                                            AND (NUMBERED_YEAR = /*data.JINEN_08*/null)
                                            GROUP BY
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID
                                        ) TJ4
                                    ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                    AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                    AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                    AND TJ3.SEQ = TJ4.SEQ
                                WHERE
                                    (TJ3.DELETE_FLG = 0)
                                AND (TJ3.NUMBERED_YEAR = /*data.JINEN_08*/null)
                            ) N2
                        ON  N1.BUSHO_CD = N2.BUSHO_CD
                        AND N1.SHAIN_CD = N2.SHAIN_CD
                    )
                     ) AS NEN08
             ON SHN.BUSHO_CD = NEN08.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN08.SHAIN_CD
  
             LEFT OUTER JOIN 
                 (SELECT
                    N1.BUSHO_CD,
                    N1.SHAIN_CD,
                    (
                    /*IF !data.JINEN_FLG_01*/ISNULL (N1.MONTH_01, 0) + /*END*/
                    /*IF !data.JINEN_FLG_02*/ISNULL (N1.MONTH_02, 0) + /*END*/
                    /*IF !data.JINEN_FLG_03*/ISNULL (N1.MONTH_03, 0) + /*END*/
                    /*IF !data.JINEN_FLG_04*/ISNULL (N1.MONTH_04, 0) + /*END*/
                    /*IF !data.JINEN_FLG_05*/ISNULL (N1.MONTH_05, 0) + /*END*/
                    /*IF !data.JINEN_FLG_06*/ISNULL (N1.MONTH_06, 0) + /*END*/
                    /*IF !data.JINEN_FLG_07*/ISNULL (N1.MONTH_07, 0) + /*END*/
                    /*IF !data.JINEN_FLG_08*/ISNULL (N1.MONTH_08, 0) + /*END*/
                    /*IF !data.JINEN_FLG_09*/ISNULL (N1.MONTH_09, 0) + /*END*/
                    /*IF !data.JINEN_FLG_10*/ISNULL (N1.MONTH_10, 0) + /*END*/
                    /*IF !data.JINEN_FLG_11*/ISNULL (N1.MONTH_11, 0) + /*END*/
                    /*IF !data.JINEN_FLG_12*/ISNULL (N1.MONTH_12, 0) + /*END*/
                    /*IF data.JINEN_FLG_01*/ISNULL (N2.MONTH_01, 0) + /*END*/
                    /*IF data.JINEN_FLG_02*/ISNULL (N2.MONTH_02, 0) + /*END*/
                    /*IF data.JINEN_FLG_03*/ISNULL (N2.MONTH_03, 0) + /*END*/
                    /*IF data.JINEN_FLG_04*/ISNULL (N2.MONTH_04, 0) + /*END*/
                    /*IF data.JINEN_FLG_05*/ISNULL (N2.MONTH_05, 0) + /*END*/
                    /*IF data.JINEN_FLG_06*/ISNULL (N2.MONTH_06, 0) + /*END*/
                    /*IF data.JINEN_FLG_07*/ISNULL (N2.MONTH_07, 0) + /*END*/
                    /*IF data.JINEN_FLG_08*/ISNULL (N2.MONTH_08, 0) + /*END*/
                    /*IF data.JINEN_FLG_09*/ISNULL (N2.MONTH_09, 0) + /*END*/
                    /*IF data.JINEN_FLG_10*/ISNULL (N2.MONTH_10, 0) + /*END*/
                    /*IF data.JINEN_FLG_11*/ISNULL (N2.MONTH_11, 0) + /*END*/
                    /*IF data.JINEN_FLG_12*/ISNULL (N2.MONTH_12, 0) + /*END*/
                    0) AS YOTEI_NENDO_09
                FROM
                    (
                        (
                            SELECT
                                TJ1.BUSHO_CD,
                                TJ1.SHAIN_CD,
                                ISNULL (TJ1.MONTH_KENSU_01, 0) AS MONTH_01,
                                ISNULL (TJ1.MONTH_KENSU_02, 0) AS MONTH_02,
                                ISNULL (TJ1.MONTH_KENSU_03, 0) AS MONTH_03,
                                ISNULL (TJ1.MONTH_KENSU_04, 0) AS MONTH_04,
                                ISNULL (TJ1.MONTH_KENSU_05, 0) AS MONTH_05,
                                ISNULL (TJ1.MONTH_KENSU_06, 0) AS MONTH_06,
                                ISNULL (TJ1.MONTH_KENSU_07, 0) AS MONTH_07,
                                ISNULL (TJ1.MONTH_KENSU_08, 0) AS MONTH_08,
                                ISNULL (TJ1.MONTH_KENSU_09, 0) AS MONTH_09,
                                ISNULL (TJ1.MONTH_KENSU_10, 0) AS MONTH_10,
                                ISNULL (TJ1.MONTH_KENSU_11, 0) AS MONTH_11,
                                ISNULL (TJ1.MONTH_KENSU_12, 0) AS MONTH_12
                            FROM
                                T_JUCHU_M_KENSU AS TJ1
                                RIGHT OUTER JOIN
                                    (
                                        SELECT
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID,
                                            MAX (SEQ) AS SEQ
                                        FROM
                                            T_JUCHU_M_KENSU
                                        WHERE
                                            (DELETE_FLG = 0)
                                        AND (NUMBERED_YEAR = /*data.NENDO_09*/null)
                                        GROUP BY
                                            NUMBERED_YEAR,
                                            BUSHO_CD,
                                            SHAIN_CD,
                                            SYSTEM_ID
                                    ) TJ2
                                ON  TJ1.BUSHO_CD = TJ2.BUSHO_CD
                                AND TJ1.SHAIN_CD = TJ2.SHAIN_CD
                                AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID
                                AND TJ1.SEQ = TJ2.SEQ
                            WHERE
                                (TJ1.DELETE_FLG = 0)
                            AND (TJ1.NUMBERED_YEAR = /*data.NENDO_09*/null)
                        ) N1
                        LEFT JOIN
                            (
                                SELECT
                                    TJ3.BUSHO_CD,
                                    TJ3.SHAIN_CD,
                                    ISNULL (TJ3.MONTH_KENSU_01, 0) AS MONTH_01,
                                    ISNULL (TJ3.MONTH_KENSU_02, 0) AS MONTH_02,
                                    ISNULL (TJ3.MONTH_KENSU_03, 0) AS MONTH_03,
                                    ISNULL (TJ3.MONTH_KENSU_04, 0) AS MONTH_04,
                                    ISNULL (TJ3.MONTH_KENSU_05, 0) AS MONTH_05,
                                    ISNULL (TJ3.MONTH_KENSU_06, 0) AS MONTH_06,
                                    ISNULL (TJ3.MONTH_KENSU_07, 0) AS MONTH_07,
                                    ISNULL (TJ3.MONTH_KENSU_08, 0) AS MONTH_08,
                                    ISNULL (TJ3.MONTH_KENSU_09, 0) AS MONTH_09,
                                    ISNULL (TJ3.MONTH_KENSU_10, 0) AS MONTH_10,
                                    ISNULL (TJ3.MONTH_KENSU_11, 0) AS MONTH_11,
                                    ISNULL (TJ3.MONTH_KENSU_12, 0) AS MONTH_12
                                FROM
                                    T_JUCHU_M_KENSU AS TJ3
                                    RIGHT OUTER JOIN
                                        (
                                            SELECT
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID,
                                                MAX (SEQ) AS SEQ
                                            FROM
                                                T_JUCHU_M_KENSU
                                            WHERE
                                                (DELETE_FLG = 0)
                                            AND (NUMBERED_YEAR = /*data.JINEN_09*/null)
                                            GROUP BY
                                                NUMBERED_YEAR,
                                                BUSHO_CD,
                                                SHAIN_CD,
                                                SYSTEM_ID
                                        ) TJ4
                                    ON  TJ3.BUSHO_CD = TJ4.BUSHO_CD
                                    AND TJ3.SHAIN_CD = TJ4.SHAIN_CD
                                    AND TJ3.SYSTEM_ID = TJ4.SYSTEM_ID
                                    AND TJ3.SEQ = TJ4.SEQ
                                WHERE
                                    (TJ3.DELETE_FLG = 0)
                                AND (TJ3.NUMBERED_YEAR = /*data.JINEN_09*/null)
                            ) N2
                        ON  N1.BUSHO_CD = N2.BUSHO_CD
                        AND N1.SHAIN_CD = N2.SHAIN_CD
                    )
                     ) AS NEN09
             ON SHN.BUSHO_CD = NEN09.BUSHO_CD AND 
             SHN.SHAIN_CD = NEN09.SHAIN_CD

         ) AS JUCHU
    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_01
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_01*/null AND /*data.ENDNENDO_01*/null )
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M01 ON JUCHU.SHAIN_CD = M01.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_02
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_02*/null AND /*data.ENDNENDO_02*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M02 ON JUCHU.SHAIN_CD = M02.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_03
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_03*/null AND /*data.ENDNENDO_03*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M03 ON JUCHU.SHAIN_CD = M03.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_04
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN  /*data.STARTNENDO_04*/null AND /*data.ENDNENDO_04*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M04 ON JUCHU.SHAIN_CD = M04.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_05
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_05*/null AND /*data.ENDNENDO_05*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M05 ON JUCHU.SHAIN_CD = M05.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_06
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_06*/null AND /*data.ENDNENDO_06*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M06 ON JUCHU.SHAIN_CD = M06.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_07
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_07*/null AND /*data.ENDNENDO_07*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M07 ON JUCHU.SHAIN_CD = M07.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_08
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_08*/null AND /*data.ENDNENDO_08*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M08 ON JUCHU.SHAIN_CD = M08.EIGYOU_TANTOU_CD
    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(TME.CREATE_DATE) AS NENDO_09
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) BETWEEN /*data.STARTNENDO_09*/null AND /*data.ENDNENDO_09*/null)
         GROUP BY
                TME.EIGYOU_TANTOU_CD
         ) M09 ON JUCHU.SHAIN_CD = M09.EIGYOU_TANTOU_CD
ORDER BY BUSHO_CD,EIGYOU_CD



