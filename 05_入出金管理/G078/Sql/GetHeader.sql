SELECT *
  FROM (SELECT '2' AS DataKbn
              ,SHUKKIN_NUMBER AS Num
          FROM T_SHUKKIN_ENTRY
         WHERE SHUKKIN_NUMBER = /*number*/''
           AND DELETE_FLG = 0

        /*IF nyuukinKbn == '1'*/
        UNION ALL

        SELECT '1' AS DataKbn
              ,NYUUKIN_NUMBER AS Num
          FROM T_NYUUKIN_ENTRY
         WHERE NYUUKIN_NUMBER = /*number*/''
           AND DELETE_FLG = 0
        /*END*/

        /*IF nyuukinKbn == '2'*/
        UNION ALL

        SELECT '1' AS DataKbn
              ,NYUUKIN_NUMBER AS Num
          FROM T_NYUUKIN_SUM_ENTRY
         WHERE NYUUKIN_NUMBER = /*number*/''
           AND DELETE_FLG = 0
        /*END*/
        ) tCount
 WHERE tCount.DataKbn = /*dataKbn*/''