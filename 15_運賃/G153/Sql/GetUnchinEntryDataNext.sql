SELECT T_USSE.*
  FROM (SELECT TOP 1 *
          FROM (SELECT *,
                       0 AS KBN
                  FROM T_UNCHIN_ENTRY WITH(NOLOCK)
                 WHERE DELETE_FLG   = 'false'
                   /*IF !data.DENPYOU_NUMBER.IsNull */
                   AND DENPYOU_NUMBER    > /*data.DENPYOU_NUMBER.Value*/'0'
                   /*END*/
                 UNION ALL
                SELECT *,
                       1 AS KBN
                  FROM T_UNCHIN_ENTRY WITH(NOLOCK)
                 WHERE DELETE_FLG   = 'false'
                       ) AS TEMP
         WHERE DELETE_FLG     = 'false'
           /*IF !data.KYOTEN.IsNull */
           AND TEMP.KYOTEN_CD = /*data.KYOTEN.Value*/'0'
           /*END*/
      ORDER BY KBN,
               DENPYOU_NUMBER
       ) AS T_USSE