SELECT
         JUCHU.SHAIN_CD AS EIGYOU_CD, 
         JUCHU.SHAIN_NAME AS EIGYOU_NAME, 
         JUCHU.BUSHO_CD AS BUSHO_CD, 
         JUCHU.BUSHO_NAME_RYAKU AS BUSHO_NAME, 
         ISNULL(JUCHU.MONTH_KENSU_01,0) AS YOTEI_MONTH01, 
         ISNULL(JUCHU.MONTH_KENSU_02,0) AS YOTEI_MONTH02, 
         ISNULL(JUCHU.MONTH_KENSU_03,0) AS YOTEI_MONTH03, 
         ISNULL(JUCHU.MONTH_KENSU_04,0) AS YOTEI_MONTH04, 
         ISNULL(JUCHU.MONTH_KENSU_05,0) AS YOTEI_MONTH05, 
         ISNULL(JUCHU.MONTH_KENSU_06,0) AS YOTEI_MONTH06, 
         ISNULL(JUCHU.MONTH_KENSU_07,0) AS YOTEI_MONTH07, 
         ISNULL(JUCHU.MONTH_KENSU_08,0) AS YOTEI_MONTH08, 
         ISNULL(JUCHU.MONTH_KENSU_09,0) AS YOTEI_MONTH09, 
         ISNULL(JUCHU.MONTH_KENSU_10,0) AS YOTEI_MONTH10, 
         ISNULL(JUCHU.MONTH_KENSU_11,0) AS YOTEI_MONTH11, 
         ISNULL(JUCHU.MONTH_KENSU_12,0) AS YOTEI_MONTH12, 
     	(ISNULL(JUCHU.MONTH_KENSU_01, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_02, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_03, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_04, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_05, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_06, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_07, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_08, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_09, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_10, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_11, 0) + 
          ISNULL(JUCHU.MONTH_KENSU_12, 0)) AS YOTEI_GOUKEI,
          ISNULL(M01.MONTH_01,0) AS MITSUMORI_MONTH01,
          ISNULL(M02.MONTH_02,0) AS MITSUMORI_MONTH02,
          ISNULL(M03.MONTH_03,0) AS MITSUMORI_MONTH03,
          ISNULL(M04.MONTH_04,0) AS MITSUMORI_MONTH04,
          ISNULL(M05.MONTH_05,0) AS MITSUMORI_MONTH05,
          ISNULL(M06.MONTH_06,0) AS MITSUMORI_MONTH06,
          ISNULL(M07.MONTH_07,0) AS MITSUMORI_MONTH07,
          ISNULL(M08.MONTH_08,0) AS MITSUMORI_MONTH08,
          ISNULL(M09.MONTH_09,0) AS MITSUMORI_MONTH09,
          ISNULL(M10.MONTH_10,0) AS MITSUMORI_MONTH10,
          ISNULL(M11.MONTH_11,0) AS MITSUMORI_MONTH11,
          ISNULL(M12.MONTH_12,0) AS MITSUMORI_MONTH12,
          (ISNULL(M01.MONTH_01,0) +
           ISNULL(M02.MONTH_02,0) +
           ISNULL(M03.MONTH_03,0) +
           ISNULL(M04.MONTH_04,0) +
           ISNULL(M05.MONTH_05,0) +
           ISNULL(M06.MONTH_06,0) +
           ISNULL(M07.MONTH_07,0) +
           ISNULL(M08.MONTH_08,0) +
           ISNULL(M09.MONTH_09,0) +
           ISNULL(M10.MONTH_10,0) +
           ISNULL(M11.MONTH_11,0) +
           ISNULL(M12.MONTH_12,0)) AS MITSUMORI_GOUKEI
     FROM
         (SELECT        
             SHN.SHAIN_CD, 
             SHN.SHAIN_NAME, 
             SHN.BUSHO_CD, 
             SHN.BUSHO_NAME_RYAKU, 
             JK.MONTH_KENSU_01,
             JK.MONTH_KENSU_02,
             JK.MONTH_KENSU_03,
             JK.MONTH_KENSU_04,
             JK.MONTH_KENSU_05,
             JK.MONTH_KENSU_06,
             JK.MONTH_KENSU_07,
             JK.MONTH_KENSU_08,
             JK.MONTH_KENSU_09,
             JK.MONTH_KENSU_10,
             JK.MONTH_KENSU_11,
             JK.MONTH_KENSU_12,
             JK.SYSTEM_ID, 
             JK.SEQ
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
                 1 = 1
    		     /*IF data.BUSHO_CD != null && data.BUSHO_CD != ""*/
                 AND SH.BUSHO_CD = /*data.BUSHO_CD*/null/*END*/
    		     AND SH.EIGYOU_TANTOU_KBN = 1
             ) AS SHN 
             LEFT OUTER JOIN 
                     (SELECT
                         TJ1.BUSHO_CD, 
                         TJ1.SHAIN_CD, 
                         TJ1.MONTH_KENSU_01, 
                         TJ1.MONTH_KENSU_02, 
                         TJ1.MONTH_KENSU_03, 
                         TJ1.MONTH_KENSU_04, 
                         TJ1.MONTH_KENSU_05, 
                         TJ1.MONTH_KENSU_06, 
                         TJ1.MONTH_KENSU_07, 
                         TJ1.MONTH_KENSU_08, 
                         TJ1.MONTH_KENSU_09, 
                         TJ1.MONTH_KENSU_10, 
                         TJ1.MONTH_KENSU_11, 
                         TJ1.MONTH_KENSU_12, 
                         TJ1.SYSTEM_ID, 
                         TJ1.SEQ
                     FROM
                         T_JUCHU_M_KENSU AS TJ1 RIGHT OUTER JOIN
         				(SELECT
                                    NUMBERED_YEAR, 
                                    BUSHO_CD, 
                                    SHAIN_CD, 
                                    SYSTEM_ID, 
                                    MAX(SEQ) AS SEQ
                                FROM
                                    T_JUCHU_M_KENSU
                                WHERE
                                    (DELETE_FLG = 0) 
                                    AND (NUMBERED_YEAR = /*data.NENDO_01*/null)
                                    
                               GROUP BY
                                    NUMBERED_YEAR, BUSHO_CD, SHAIN_CD, SYSTEM_ID
                            ) TJ2
                            ON TJ1.BUSHO_CD = TJ2.BUSHO_CD 
         				   AND TJ1.SHAIN_CD = TJ2.SHAIN_CD 
         				   AND TJ1.SYSTEM_ID = TJ2.SYSTEM_ID 
         				   AND TJ1.SEQ = TJ2.SEQ
                     WHERE
                         (TJ1.DELETE_FLG = 0) 
                         AND (TJ1.NUMBERED_YEAR = /*data.NENDO_01*/null)
                     ) AS JK 
             ON SHN.BUSHO_CD = JK.BUSHO_CD AND 
             SHN.SHAIN_CD = JK.SHAIN_CD
         ) AS JUCHU
    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_01
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_01*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M01 ON JUCHU.SHAIN_CD = M01.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_02
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_02*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M02 ON JUCHU.SHAIN_CD = M02.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_03
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_03*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M03 ON JUCHU.SHAIN_CD = M03.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_04
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_04*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M04 ON JUCHU.SHAIN_CD = M04.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_05
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_05*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M05 ON JUCHU.SHAIN_CD = M05.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_06
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_06*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M06 ON JUCHU.SHAIN_CD = M06.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_07
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_07*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M07 ON JUCHU.SHAIN_CD = M07.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_08
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_08*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M08 ON JUCHU.SHAIN_CD = M08.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_09
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_09*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M09 ON JUCHU.SHAIN_CD = M09.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_10
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_10*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M10 ON JUCHU.SHAIN_CD = M10.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_11
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_11*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M11 ON JUCHU.SHAIN_CD = M11.EIGYOU_TANTOU_CD

    LEFT OUTER JOIN 
         (SELECT
                TME.EIGYOU_TANTOU_CD,
                COUNT(SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6)) AS MONTH_12
         FROM   M_TORIHIKISAKI AS TME
         WHERE  (1 = 1)
           AND  ( SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6) = /*data.MONTH_12*/null)
         GROUP BY
                SUBSTRING(CONVERT(varchar, TME.CREATE_DATE, 112), 1, 6),TME.EIGYOU_TANTOU_CD
         ) M12 ON JUCHU.SHAIN_CD = M12.EIGYOU_TANTOU_CD
ORDER BY BUSHO_CD,EIGYOU_CD



