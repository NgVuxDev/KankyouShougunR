with
--対象社員取得
SHAIN as
(
    select 
         M1.BUSHO_CD         as BUSHO_CD
        ,M2.BUSHO_NAME_RYAKU as BUSHO_NAME_RYAKU
        ,M1.SHAIN_CD         as SHAIN_CD
        ,M1.SHAIN_NAME_RYAKU as SHAIN_NAME_RYAKU
    from 
         M_SHAIN M1    -- 社員マスタ
        ,M_BUSHO M2    -- 部署マスタ
    where
            M1.EIGYOU_TANTOU_KBN  =  1
/*IF data.busyouCD != null && data.busyouCD != ''*/
        and M1.BUSHO_CD           =  /*data.busyouCD*/ --部署コード
/*END*/
        and M1.BUSHO_CD           =  M2.BUSHO_CD
) ,
--予算(年度)
YOSAN_TMP as
(
    (--年度1
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                               as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo1*/ --年度1
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen1*/ --年度の次の年1
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度2
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                               as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo2*/ --年度2
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen2*/ --年度の次の年2
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度3
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo3*/ --年度3
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen3*/ --年度の次の年3
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度4
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                               as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo4*/ --年度4
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen4*/ --年度の次の年4
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度5
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo5*/ --年度5
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen5*/ --年度の次の年5
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度6
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                               as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo6*/ --年度6
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen6*/ --年度の次の年6
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度7
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                               as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,0                                  as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo7*/ --年度7
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen7*/ --年度の次の年7
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度8
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
               0)                                as YOSAN_NENDO_8
            ,0                                   as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo8*/ --年度8
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen8*/ --年度の次の年8
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
    union all
    (--年度9
        select
             tmp1.BUSHO_CD                     as BUSHO_CD
            ,tmp1.SHAIN_CD                     as SHAIN_CD
            ,0                                  as YOSAN_NENDO_1
            ,0                                  as YOSAN_NENDO_2
            ,0                                  as YOSAN_NENDO_3
            ,0                                  as YOSAN_NENDO_4
            ,0                                  as YOSAN_NENDO_5
            ,0                                  as YOSAN_NENDO_6
            ,0                                  as YOSAN_NENDO_7
            ,0                                  as YOSAN_NENDO_8
            ,(
               /*IF !data.JINEN_FLG_01*/ISNULL(tmp1.MONTH_01,0) + /*END*/
               /*IF !data.JINEN_FLG_02*/ISNULL(tmp1.MONTH_02,0) + /*END*/
               /*IF !data.JINEN_FLG_03*/ISNULL(tmp1.MONTH_03,0) + /*END*/
               /*IF !data.JINEN_FLG_04*/ISNULL(tmp1.MONTH_04,0) + /*END*/
               /*IF !data.JINEN_FLG_05*/ISNULL(tmp1.MONTH_05,0) + /*END*/
               /*IF !data.JINEN_FLG_06*/ISNULL(tmp1.MONTH_06,0) + /*END*/
               /*IF !data.JINEN_FLG_07*/ISNULL(tmp1.MONTH_07,0) + /*END*/
               /*IF !data.JINEN_FLG_08*/ISNULL(tmp1.MONTH_08,0) + /*END*/
               /*IF !data.JINEN_FLG_09*/ISNULL(tmp1.MONTH_09,0) + /*END*/
               /*IF !data.JINEN_FLG_10*/ISNULL(tmp1.MONTH_10,0) + /*END*/
               /*IF !data.JINEN_FLG_11*/ISNULL(tmp1.MONTH_11,0) + /*END*/
               /*IF !data.JINEN_FLG_12*/ISNULL(tmp1.MONTH_12,0) + /*END*/
               /*IF data.JINEN_FLG_01*/ISNULL(tmp2.NEXT_MONTH_01,0) + /*END*/
               /*IF data.JINEN_FLG_02*/ISNULL(tmp2.NEXT_MONTH_02,0) + /*END*/
               /*IF data.JINEN_FLG_03*/ISNULL(tmp2.NEXT_MONTH_03,0) + /*END*/
               /*IF data.JINEN_FLG_04*/ISNULL(tmp2.NEXT_MONTH_04,0) + /*END*/
               /*IF data.JINEN_FLG_05*/ISNULL(tmp2.NEXT_MONTH_05,0) + /*END*/
               /*IF data.JINEN_FLG_06*/ISNULL(tmp2.NEXT_MONTH_06,0) + /*END*/
               /*IF data.JINEN_FLG_07*/ISNULL(tmp2.NEXT_MONTH_07,0) + /*END*/
               /*IF data.JINEN_FLG_08*/ISNULL(tmp2.NEXT_MONTH_08,0) + /*END*/
               /*IF data.JINEN_FLG_09*/ISNULL(tmp2.NEXT_MONTH_09,0) + /*END*/
               /*IF data.JINEN_FLG_10*/ISNULL(tmp2.NEXT_MONTH_10,0) + /*END*/
               /*IF data.JINEN_FLG_11*/ISNULL(tmp2.NEXT_MONTH_11,0) + /*END*/
               /*IF data.JINEN_FLG_12*/ISNULL(tmp2.NEXT_MONTH_12,0) + /*END*/
              0) as YOSAN_NENDO_9
        from
        (
        select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.nendo9*/ --年度9
                and YOSAN.DELETE_FLG       =    0
				and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
        ) tmp1 
        LEFT JOIN
        (
            select
                YOSAN.BUSHO_CD,
                YOSAN.SHAIN_CD,
                ISNULL(YOSAN.MONTH_YOSAN_01,0) AS NEXT_MONTH_01,
                ISNULL(YOSAN.MONTH_YOSAN_02,0) AS NEXT_MONTH_02,
                ISNULL(YOSAN.MONTH_YOSAN_03,0) AS NEXT_MONTH_03,
                ISNULL(YOSAN.MONTH_YOSAN_04,0) AS NEXT_MONTH_04,
                ISNULL(YOSAN.MONTH_YOSAN_05,0) AS NEXT_MONTH_05,
                ISNULL(YOSAN.MONTH_YOSAN_06,0) AS NEXT_MONTH_06,
                ISNULL(YOSAN.MONTH_YOSAN_07,0) AS NEXT_MONTH_07,
                ISNULL(YOSAN.MONTH_YOSAN_08,0) AS NEXT_MONTH_08,
                ISNULL(YOSAN.MONTH_YOSAN_09,0) AS NEXT_MONTH_09,
                ISNULL(YOSAN.MONTH_YOSAN_10,0) AS NEXT_MONTH_10,
                ISNULL(YOSAN.MONTH_YOSAN_11,0) AS NEXT_MONTH_11,
                ISNULL(YOSAN.MONTH_YOSAN_12,0) AS NEXT_MONTH_12
            from
                SHAIN,
                T_EIGYO_YOSAN YOSAN
            where
                    SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
                and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
                and YOSAN.NUMBERED_YEAR    =    /*data.jinen9*/ --年度の次の年9
                and YOSAN.DELETE_FLG       =    0
        ) tmp2        
        ON  tmp1.BUSHO_CD = tmp2.BUSHO_CD
        AND tmp1.SHAIN_CD = tmp2.SHAIN_CD  
    ) 
),
YOSAN_NENDO as
(
    select
         BUSHO_CD                  as BUSHO_CD
        ,SHAIN_CD                  as SHAIN_CD
        ,sum(YOSAN_NENDO_1)        as YOSAN_1
        ,sum(YOSAN_NENDO_2)        as YOSAN_2
        ,sum(YOSAN_NENDO_3)        as YOSAN_3
        ,sum(YOSAN_NENDO_4)        as YOSAN_4
        ,sum(YOSAN_NENDO_5)        as YOSAN_5
        ,sum(YOSAN_NENDO_6)        as YOSAN_6
        ,sum(YOSAN_NENDO_7)        as YOSAN_7
        ,sum(YOSAN_NENDO_8)        as YOSAN_8
        ,sum(YOSAN_NENDO_9)        as YOSAN_9
    from
        YOSAN_TMP
    group by BUSHO_CD, SHAIN_CD
),
--実績
JISSEKI_ALL as
(
    (
        select
             SHAIN.BUSHO_CD                            as BUSHO_CD
            ,SHAIN.SHAIN_CD                            as SHAIN_CD
            ,ENTRY.DENPYOU_DATE                        as DENPYOU_DATE
            ,(
                  isnull(DETAIL.KINGAKU,0)
                - isnull(DETAIL.TAX_UCHI,0)
                + isnull(DETAIL.HINMEI_KINGAKU,0)
                - isnull(DETAIL.HINMEI_TAX_UCHI,0))    as SUMVALUE
        from
            SHAIN
            ,T_UKEIRE_ENTRY   ENTRY
            ,T_UKEIRE_DETAIL  DETAIL
        where
                  SHAIN.SHAIN_CD           =  ENTRY.EIGYOU_TANTOUSHA_CD
              and ENTRY.TAIRYUU_KBN = 0
              and ENTRY.DENPYOU_DATE    is not null
              and ENTRY.DENPYOU_DATE    <= /*data.nendo9LastDay*/ --年度9最終日
              and ENTRY.DENPYOU_DATE    >= /*data.nendo1FirstDay*/ --年度1最初日
              and ENTRY.DELETE_FLG      =  0
              and ENTRY.SYSTEM_ID       =  DETAIL.SYSTEM_ID
              and ENTRY.SEQ             =  DETAIL.SEQ
              /*IF data.denpyouKbn == '1'*/and DETAIL.DENPYOU_KBN_CD =  1/*END*/
              /*IF data.denpyouKbn == '2'*/and DETAIL.DENPYOU_KBN_CD =  2/*END*/
              and DETAIL.KAKUTEI_KBN    =  1
    )
    union all
    (
        select
             SHAIN.BUSHO_CD                            as BUSHO_CD
            ,SHAIN.SHAIN_CD                            as SHAIN_CD
            ,ENTRY.DENPYOU_DATE                        as DENPYOU_DATE
            ,(
                  isnull(DETAIL.KINGAKU,0)
                - isnull(DETAIL.TAX_UCHI,0)
                + isnull(DETAIL.HINMEI_KINGAKU,0)
                - isnull(DETAIL.HINMEI_TAX_UCHI,0))    as SUMVALUE
        from
            SHAIN
            ,T_SHUKKA_ENTRY   ENTRY
            ,T_SHUKKA_DETAIL  DETAIL
        where
                  SHAIN.SHAIN_CD           =  ENTRY.EIGYOU_TANTOUSHA_CD
              and ENTRY.TAIRYUU_KBN = 0
              and ENTRY.DENPYOU_DATE    is not null
              and ENTRY.DENPYOU_DATE    <= /*data.nendo9LastDay*/ --年度9最終日
              and ENTRY.DENPYOU_DATE    >= /*data.nendo1FirstDay*/ --年度1最初日
              and ENTRY.DELETE_FLG      =  0
              and ENTRY.SYSTEM_ID       =  DETAIL.SYSTEM_ID
              and ENTRY.SEQ             =  DETAIL.SEQ
              /*IF data.denpyouKbn == '1'*/and DETAIL.DENPYOU_KBN_CD =  1/*END*/
              /*IF data.denpyouKbn == '2'*/and DETAIL.DENPYOU_KBN_CD =  2/*END*/
              and DETAIL.KAKUTEI_KBN    =  1
    )
    union all
    (
        select
             SHAIN.BUSHO_CD                            as BUSHO_CD
            ,SHAIN.SHAIN_CD                            as SHAIN_CD
            ,ENTRY.DENPYOU_DATE                        as DENPYOU_DATE
            ,(
                  isnull(DETAIL.KINGAKU,0)
                - isnull(DETAIL.TAX_UCHI,0)
                + isnull(DETAIL.HINMEI_KINGAKU,0)
                - isnull(DETAIL.HINMEI_TAX_UCHI,0))    as SUMVALUE
        from
            SHAIN
            ,T_UR_SH_ENTRY   ENTRY
            ,T_UR_SH_DETAIL  DETAIL
        where
                  SHAIN.SHAIN_CD           =  ENTRY.EIGYOU_TANTOUSHA_CD
              and ENTRY.DENPYOU_DATE    is not null
              and ENTRY.DENPYOU_DATE    <= /*data.nendo9LastDay*/ --年度9最終日
              and ENTRY.DENPYOU_DATE    >= /*data.nendo1FirstDay*/ --年度1最初日
              and ENTRY.DELETE_FLG      =  0
              and ENTRY.SYSTEM_ID       =  DETAIL.SYSTEM_ID
              and ENTRY.SEQ             =  DETAIL.SEQ
              /*IF data.denpyouKbn == '1'*/and DETAIL.DENPYOU_KBN_CD =  1/*END*/
              /*IF data.denpyouKbn == '2'*/and DETAIL.DENPYOU_KBN_CD =  2/*END*/
              and DETAIL.KAKUTEI_KBN    =  1
    )
),
JISSEKI_TMP as
(
    (-- 年度1
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,SUMVALUE                            as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo1FirstDay*/ --年度1開始日
            and DENPYOU_DATE                     <= /*data.nendo1LastDay*/ --年度1終了日
    )
    union all
    (-- 年度2
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,SUMVALUE                            as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo2FirstDay*/ --年度2開始日
            and DENPYOU_DATE                     <= /*data.nendo2LastDay*/ --年度2終了日
    )
    union all
    (-- 年度3
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,SUMVALUE                            as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo3FirstDay*/ --年度3開始日
            and DENPYOU_DATE                     <= /*data.nendo3LastDay*/ --年度3終了日
    )
    union all
    (-- 年度4
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,SUMVALUE                            as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo4FirstDay*/ --年度4開始日
            and DENPYOU_DATE                     <= /*data.nendo4LastDay*/ --年度4終了日
    )
    union all
    (-- 年度5
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,SUMVALUE                            as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo5FirstDay*/ --年度5開始日
            and DENPYOU_DATE                     <= /*data.nendo5LastDay*/ --年度5終了日
    )
    union all
    (-- 年度6
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,SUMVALUE                            as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo6FirstDay*/ --年度6開始日
            and DENPYOU_DATE                     <= /*data.nendo6LastDay*/ --年度6終了日
    )
    union all
    (-- 年度7
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,SUMVALUE                            as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo7FirstDay*/ --年度7開始日
            and DENPYOU_DATE                     <= /*data.nendo7LastDay*/ --年度7終了日
    )
    union all
    (-- 年度8
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,SUMVALUE                            as JISSEKI_NENDO_8
            ,0                                   as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo8FirstDay*/ --年度8開始日
            and DENPYOU_DATE                     <= /*data.nendo8LastDay*/ --年度8終了日
    )
    union all
    (-- 年度9
        select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
            ,0                                   as JISSEKI_NENDO_1
            ,0                                   as JISSEKI_NENDO_2
            ,0                                   as JISSEKI_NENDO_3
            ,0                                   as JISSEKI_NENDO_4
            ,0                                   as JISSEKI_NENDO_5
            ,0                                   as JISSEKI_NENDO_6
            ,0                                   as JISSEKI_NENDO_7
            ,0                                   as JISSEKI_NENDO_8
            ,SUMVALUE                            as JISSEKI_NENDO_9
        from
            JISSEKI_ALL
        where
                DENPYOU_DATE                     >= /*data.nendo9FirstDay*/ --年度開始日
            and DENPYOU_DATE                     <= /*data.nendo9LastDay*/ --年度終了日
    )
),
JISSEKI_NENDO as
(
	select
             BUSHO_CD                            as BUSHO_CD
            ,SHAIN_CD                            as SHAIN_CD
			,sum(JISSEKI_NENDO_1)                as JISSEKI_1
			,sum(JISSEKI_NENDO_2)                as JISSEKI_2
			,sum(JISSEKI_NENDO_3)                as JISSEKI_3
			,sum(JISSEKI_NENDO_4)                as JISSEKI_4
			,sum(JISSEKI_NENDO_5)                as JISSEKI_5
			,sum(JISSEKI_NENDO_6)                as JISSEKI_6
			,sum(JISSEKI_NENDO_7)                as JISSEKI_7
			,sum(JISSEKI_NENDO_8)                as JISSEKI_8
			,sum(JISSEKI_NENDO_9)                as JISSEKI_9
        from
            JISSEKI_TMP
        group by BUSHO_CD,SHAIN_CD
),
NENDO_INFO as
(
    select
         ISNULL(T1.BUSHO_CD, T2.BUSHO_CD)         as    BUSHO_CD
        ,ISNULL(T1.SHAIN_CD, T2.SHAIN_CD)         as    SHAIN_CD
        ,ISNULL(T1.YOSAN_1, 0)                    as    YOSAN_1
        ,ISNULL(T1.YOSAN_2, 0)                    as    YOSAN_2
        ,ISNULL(T1.YOSAN_3, 0)                    as    YOSAN_3
        ,ISNULL(T1.YOSAN_4, 0)                    as    YOSAN_4
        ,ISNULL(T1.YOSAN_5, 0)                    as    YOSAN_5
        ,ISNULL(T1.YOSAN_6, 0)                    as    YOSAN_6
        ,ISNULL(T1.YOSAN_7, 0)                    as    YOSAN_7
        ,ISNULL(T1.YOSAN_8, 0)                    as    YOSAN_8
        ,ISNULL(T1.YOSAN_9, 0)                    as    YOSAN_9
        ,(ISNULL(T1.YOSAN_1, 0)
          + ISNULL(T1.YOSAN_2, 0)
          + ISNULL(T1.YOSAN_3, 0)
          + ISNULL(T1.YOSAN_4, 0)
          + ISNULL(T1.YOSAN_5, 0)
          + ISNULL(T1.YOSAN_6, 0)
          + ISNULL(T1.YOSAN_7, 0)
          + ISNULL(T1.YOSAN_8, 0)
          + ISNULL(T1.YOSAN_9, 0))                as     YOSAN_GOUKEI
        ,ISNULL(T2.JISSEKI_1, 0)                  as    JISSEKI_1
        ,ISNULL(T2.JISSEKI_2, 0)                  as    JISSEKI_2
        ,ISNULL(T2.JISSEKI_3, 0)                  as    JISSEKI_3
        ,ISNULL(T2.JISSEKI_4, 0)                  as    JISSEKI_4
        ,ISNULL(T2.JISSEKI_5, 0)                  as    JISSEKI_5
        ,ISNULL(T2.JISSEKI_6, 0)                  as    JISSEKI_6
        ,ISNULL(T2.JISSEKI_7, 0)                  as    JISSEKI_7
        ,ISNULL(T2.JISSEKI_8, 0)                  as    JISSEKI_8
        ,ISNULL(T2.JISSEKI_9, 0)                  as    JISSEKI_9
        ,(ISNULL(T2.JISSEKI_1, 0)
          + ISNULL(T2.JISSEKI_2, 0)
          + ISNULL(T2.JISSEKI_3, 0)
          + ISNULL(T2.JISSEKI_4, 0)
          + ISNULL(T2.JISSEKI_5, 0)
          + ISNULL(T2.JISSEKI_6, 0)
          + ISNULL(T2.JISSEKI_7, 0)
          + ISNULL(T2.JISSEKI_8, 0)
          + ISNULL(T2.JISSEKI_9, 0))              as    JISSEKI_GOUKEI
    from
        YOSAN_NENDO T1
    full outer join
        JISSEKI_NENDO T2
      on
            T1.BUSHO_CD = T2.BUSHO_CD
        and T1.SHAIN_CD = T2.SHAIN_CD
)
select
     SHAIN.BUSHO_CD                   as    BUSHO_CD
    ,SHAIN.BUSHO_NAME_RYAKU           as    BUSHO_NAME
    ,SHAIN.SHAIN_CD                   as    SHAIN_CD
    ,SHAIN.SHAIN_NAME_RYAKU           as    SHAIN_NAME
    ,ISNULL(NENDO_INFO.YOSAN_1, 0)               as    YOSAN_1
    ,ISNULL(NENDO_INFO.YOSAN_2, 0)               as    YOSAN_2
    ,ISNULL(NENDO_INFO.YOSAN_3, 0)               as    YOSAN_3
    ,ISNULL(NENDO_INFO.YOSAN_4, 0)               as    YOSAN_4
    ,ISNULL(NENDO_INFO.YOSAN_5, 0)               as    YOSAN_5
    ,ISNULL(NENDO_INFO.YOSAN_6, 0)               as    YOSAN_6
    ,ISNULL(NENDO_INFO.YOSAN_7, 0)               as    YOSAN_7
    ,ISNULL(NENDO_INFO.YOSAN_8, 0)               as    YOSAN_8
    ,ISNULL(NENDO_INFO.YOSAN_9, 0)               as    YOSAN_9
    ,ISNULL(NENDO_INFO.YOSAN_GOUKEI, 0)          as    YOSAN_GOUKEI
    ,ISNULL(NENDO_INFO.JISSEKI_1, 0)             as    JISSEKI_1
    ,ISNULL(NENDO_INFO.JISSEKI_2, 0)             as    JISSEKI_2
    ,ISNULL(NENDO_INFO.JISSEKI_3, 0)             as    JISSEKI_3
    ,ISNULL(NENDO_INFO.JISSEKI_4, 0)             as    JISSEKI_4
    ,ISNULL(NENDO_INFO.JISSEKI_5, 0)             as    JISSEKI_5
    ,ISNULL(NENDO_INFO.JISSEKI_6, 0)             as    JISSEKI_6
    ,ISNULL(NENDO_INFO.JISSEKI_7, 0)             as    JISSEKI_7
    ,ISNULL(NENDO_INFO.JISSEKI_8, 0)             as    JISSEKI_8
    ,ISNULL(NENDO_INFO.JISSEKI_9, 0)             as    JISSEKI_9
    ,ISNULL(NENDO_INFO.JISSEKI_GOUKEI, 0)        as    JISSEKI_GOUKEI
from
    SHAIN
left outer join
    NENDO_INFO
  on
        SHAIN.BUSHO_CD = NENDO_INFO.BUSHO_CD
    and SHAIN.SHAIN_CD = NENDO_INFO.SHAIN_CD
order by BUSHO_CD, SHAIN_CD

