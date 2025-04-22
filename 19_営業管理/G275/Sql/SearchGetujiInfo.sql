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
--予算(月次)
YOSAN_GETUJI as
(
    select
         SHAIN.BUSHO_CD                      as BUSHO_CD
        ,SHAIN.SHAIN_CD                      as SHAIN_CD
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_01,0)) as YOSAN_MONTH_1
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_02,0)) as YOSAN_MONTH_2
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_03,0)) as YOSAN_MONTH_3
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_04,0)) as YOSAN_MONTH_4
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_05,0)) as YOSAN_MONTH_5
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_06,0)) as YOSAN_MONTH_6
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_07,0)) as YOSAN_MONTH_7
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_08,0)) as YOSAN_MONTH_8
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_09,0)) as YOSAN_MONTH_9
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_10,0)) as YOSAN_MONTH_10
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_11,0)) as YOSAN_MONTH_11
        ,sum(ISNULL(YOSAN.MONTH_YOSAN_12,0)) as YOSAN_MONTH_12
    from
         SHAIN
        ,T_EIGYO_YOSAN YOSAN
    where
            SHAIN.BUSHO_CD         =    YOSAN.BUSHO_CD
        and SHAIN.SHAIN_CD         =    YOSAN.SHAIN_CD
        and YOSAN.NUMBERED_YEAR    =    /*data.nendo*/ --年度
        and YOSAN.DELETE_FLG       =    0
		and YOSAN.DENPYOU_KBN_CD   =    /*data.denpyouKbn*/
    group by
        SHAIN.BUSHO_CD, SHAIN.SHAIN_CD
) ,
--実績
JISSEKI_ALL as
(
    --受入実績
    (
        select
             SHAIN.BUSHO_CD                 as BUSHO_CD
            ,SHAIN.SHAIN_CD                 as SHAIN_CD
            ,(
                  isnull(DETAIL.KINGAKU,0)
                - isnull(DETAIL.TAX_UCHI,0)
                + isnull(DETAIL.HINMEI_KINGAKU,0)
                - isnull(DETAIL.HINMEI_TAX_UCHI,0)
                )                           as SUMVALUE
            ,SUBSTRING(CONVERT(VARCHAR(20), ENTRY.DENPYOU_DATE, 112), 1, 6) as YYYYMM
        from
            SHAIN
            ,T_UKEIRE_ENTRY   ENTRY
            ,T_UKEIRE_DETAIL  DETAIL
        where
                  SHAIN.SHAIN_CD           =  ENTRY.EIGYOU_TANTOUSHA_CD
              and ENTRY.TAIRYUU_KBN = 0
              and ENTRY.DENPYOU_DATE    is not null
              and ENTRY.DENPYOU_DATE    <= /*data.nendoLastDay*/ --年度最終日
              and ENTRY.DENPYOU_DATE    >= /*data.nendoFirstDay*/ --年度最初日
              and ENTRY.DELETE_FLG      =  0
              and ENTRY.SYSTEM_ID       =  DETAIL.SYSTEM_ID
              and ENTRY.SEQ             =  DETAIL.SEQ
              /*IF data.denpyouKbn == '1'*/and DETAIL.DENPYOU_KBN_CD =  1/*END*/
              /*IF data.denpyouKbn == '2'*/and DETAIL.DENPYOU_KBN_CD =  2/*END*/
              and DETAIL.KAKUTEI_KBN    =  1
    )
    union all
    --出荷実績
    (
        select
             SHAIN.BUSHO_CD                 as BUSHO_CD
            ,SHAIN.SHAIN_CD                 as SHAIN_CD
            ,(
                  isnull(DETAIL.KINGAKU,0)
                - isnull(DETAIL.TAX_UCHI,0)
                + isnull(DETAIL.HINMEI_KINGAKU,0)
                - isnull(DETAIL.HINMEI_TAX_UCHI,0)
                )                           as SUMVALUE
            ,SUBSTRING(CONVERT(VARCHAR(20), ENTRY.DENPYOU_DATE, 112), 1, 6) as YYYYMM
        from
            SHAIN
            ,T_SHUKKA_ENTRY   ENTRY
            ,T_SHUKKA_DETAIL  DETAIL
        where
                  SHAIN.SHAIN_CD           =  ENTRY.EIGYOU_TANTOUSHA_CD
              and ENTRY.TAIRYUU_KBN = 0
              and ENTRY.DENPYOU_DATE    is not null
              and ENTRY.DENPYOU_DATE    <= /*data.nendoLastDay*/ --年度最終日
              and ENTRY.DENPYOU_DATE    >= /*data.nendoFirstDay*/ --年度最初日
              and ENTRY.DELETE_FLG      =  0
              and ENTRY.SYSTEM_ID       =  DETAIL.SYSTEM_ID
              and ENTRY.SEQ             =  DETAIL.SEQ
              /*IF data.denpyouKbn == '1'*/and DETAIL.DENPYOU_KBN_CD =  1/*END*/
              /*IF data.denpyouKbn == '2'*/and DETAIL.DENPYOU_KBN_CD =  2/*END*/
              and DETAIL.KAKUTEI_KBN    =  1
    )
    union all
    --売上／支払実績
    (
        select
             SHAIN.BUSHO_CD                 as BUSHO_CD
            ,SHAIN.SHAIN_CD                 as SHAIN_CD
            ,(
                  isnull(DETAIL.KINGAKU,0)
                - isnull(DETAIL.TAX_UCHI,0)
                + isnull(DETAIL.HINMEI_KINGAKU,0)
                - isnull(DETAIL.HINMEI_TAX_UCHI,0)
                )                           as SUMVALUE
            ,SUBSTRING(CONVERT(VARCHAR(20), ENTRY.DENPYOU_DATE, 112), 1, 6) as YYYYMM
        from
            SHAIN
            ,T_UR_SH_ENTRY   ENTRY
            ,T_UR_SH_DETAIL  DETAIL
        where
                  SHAIN.SHAIN_CD           =  ENTRY.EIGYOU_TANTOUSHA_CD
              and ENTRY.DENPYOU_DATE    is not null
              and ENTRY.DENPYOU_DATE    <= /*data.nendoLastDay*/ --年度最終日
              and ENTRY.DENPYOU_DATE    >= /*data.nendoFirstDay*/ --年度最初日
              and ENTRY.DELETE_FLG      =  0
              and ENTRY.SYSTEM_ID       =  DETAIL.SYSTEM_ID
              and ENTRY.SEQ             =  DETAIL.SEQ
             /*IF data.denpyouKbn == '1'*/ and DETAIL.DENPYOU_KBN_CD =  1/*END*/
             /*IF data.denpyouKbn == '2'*/ and DETAIL.DENPYOU_KBN_CD =  2/*END*/
              and DETAIL.KAKUTEI_KBN    =  1
    )
) ,
JISSEKI_GETUJI as
(
    select
         BUSHO_CD               as     BUSHO_CD
        ,SHAIN_CD               as     SHAIN_CD
        ,sum(JISSEKI_MONTH_1)   as     JISSEKI_MONTH_1
        ,sum(JISSEKI_MONTH_2)   as     JISSEKI_MONTH_2
        ,sum(JISSEKI_MONTH_3)   as     JISSEKI_MONTH_3
        ,sum(JISSEKI_MONTH_4)   as     JISSEKI_MONTH_4
        ,sum(JISSEKI_MONTH_5)   as     JISSEKI_MONTH_5
        ,sum(JISSEKI_MONTH_6)   as     JISSEKI_MONTH_6
        ,sum(JISSEKI_MONTH_7)   as     JISSEKI_MONTH_7
        ,sum(JISSEKI_MONTH_8)   as     JISSEKI_MONTH_8
        ,sum(JISSEKI_MONTH_9)   as     JISSEKI_MONTH_9
        ,sum(JISSEKI_MONTH_10)  as     JISSEKI_MONTH_10
        ,sum(JISSEKI_MONTH_11)  as     JISSEKI_MONTH_11
        ,sum(JISSEKI_MONTH_12)  as     JISSEKI_MONTH_12
    from (
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month1*/ --月1
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month2*/ --月2
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month3*/ --月3
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month4*/ --月4
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month5*/ --月5
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month6*/ --月6
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month7*/ --月7
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month8*/ --月8
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month9*/ --月9
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month10*/ --月10
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_11
        ,0                         as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month11*/ --月11
    )
    union all
    (
    select
         JISSEKI_ALL.BUSHO_CD      as BUSHO_CD
        ,JISSEKI_ALL.SHAIN_CD      as SHAIN_CD
        ,0                         as JISSEKI_MONTH_1
        ,0                         as JISSEKI_MONTH_2
        ,0                         as JISSEKI_MONTH_3
        ,0                         as JISSEKI_MONTH_4
        ,0                         as JISSEKI_MONTH_5
        ,0                         as JISSEKI_MONTH_6
        ,0                         as JISSEKI_MONTH_7
        ,0                         as JISSEKI_MONTH_8
        ,0                         as JISSEKI_MONTH_9
        ,0                         as JISSEKI_MONTH_10
        ,0                         as JISSEKI_MONTH_11
        ,JISSEKI_ALL.SUMVALUE      as JISSEKI_MONTH_12
    from
        JISSEKI_ALL
    where
        JISSEKI_ALL.YYYYMM = /*data.month12*/ --月12
    )
    ) temp
    group by 
        BUSHO_CD, SHAIN_CD
),
GETUJI_INFO as
(
    select
         ISNULL(T1.BUSHO_CD, T2.BUSHO_CD)         as    BUSHO_CD
        ,ISNULL(T1.SHAIN_CD, T2.SHAIN_CD)         as    SHAIN_CD
        ,ISNULL(T1.YOSAN_MONTH_1, 0)              as    YOSAN_MONTH_1
        ,ISNULL(T1.YOSAN_MONTH_2, 0)              as    YOSAN_MONTH_2
        ,ISNULL(T1.YOSAN_MONTH_3, 0)              as    YOSAN_MONTH_3
        ,ISNULL(T1.YOSAN_MONTH_4, 0)              as    YOSAN_MONTH_4
        ,ISNULL(T1.YOSAN_MONTH_5, 0)              as    YOSAN_MONTH_5
        ,ISNULL(T1.YOSAN_MONTH_6, 0)              as    YOSAN_MONTH_6
        ,ISNULL(T1.YOSAN_MONTH_7, 0)              as    YOSAN_MONTH_7
        ,ISNULL(T1.YOSAN_MONTH_8, 0)              as    YOSAN_MONTH_8
        ,ISNULL(T1.YOSAN_MONTH_9, 0)              as    YOSAN_MONTH_9
        ,ISNULL(T1.YOSAN_MONTH_10, 0)             as    YOSAN_MONTH_10
        ,ISNULL(T1.YOSAN_MONTH_11, 0)             as    YOSAN_MONTH_11
        ,ISNULL(T1.YOSAN_MONTH_12, 0)             as    YOSAN_MONTH_12
        ,(ISNULL(T1.YOSAN_MONTH_1, 0)
          + ISNULL(T1.YOSAN_MONTH_2, 0)
          + ISNULL(T1.YOSAN_MONTH_3, 0)
          + ISNULL(T1.YOSAN_MONTH_4, 0)
          + ISNULL(T1.YOSAN_MONTH_5, 0)
          + ISNULL(T1.YOSAN_MONTH_6, 0)
          + ISNULL(T1.YOSAN_MONTH_7, 0)
          + ISNULL(T1.YOSAN_MONTH_8, 0)
          + ISNULL(T1.YOSAN_MONTH_9, 0)
          + ISNULL(T1.YOSAN_MONTH_10, 0)
          + ISNULL(T1.YOSAN_MONTH_11, 0)
          + ISNULL(T1.YOSAN_MONTH_12, 0))         as     YOSAN_GOUKEI
        ,ISNULL(T2.JISSEKI_MONTH_1, 0)            as    JISSEKI_MONTH_1
        ,ISNULL(T2.JISSEKI_MONTH_2, 0)            as    JISSEKI_MONTH_2
        ,ISNULL(T2.JISSEKI_MONTH_3, 0)            as    JISSEKI_MONTH_3
        ,ISNULL(T2.JISSEKI_MONTH_4, 0)            as    JISSEKI_MONTH_4
        ,ISNULL(T2.JISSEKI_MONTH_5, 0)            as    JISSEKI_MONTH_5
        ,ISNULL(T2.JISSEKI_MONTH_6, 0)            as    JISSEKI_MONTH_6
        ,ISNULL(T2.JISSEKI_MONTH_7, 0)            as    JISSEKI_MONTH_7
        ,ISNULL(T2.JISSEKI_MONTH_8, 0)            as    JISSEKI_MONTH_8
        ,ISNULL(T2.JISSEKI_MONTH_9, 0)            as    JISSEKI_MONTH_9
        ,ISNULL(T2.JISSEKI_MONTH_10, 0)           as    JISSEKI_MONTH_10
        ,ISNULL(T2.JISSEKI_MONTH_11, 0)           as    JISSEKI_MONTH_11
        ,ISNULL(T2.JISSEKI_MONTH_12, 0)           as    JISSEKI_MONTH_12
        ,(ISNULL(T2.JISSEKI_MONTH_1, 0)
          + ISNULL(T2.JISSEKI_MONTH_2, 0)
          + ISNULL(T2.JISSEKI_MONTH_3, 0)
          + ISNULL(T2.JISSEKI_MONTH_4, 0)
          + ISNULL(T2.JISSEKI_MONTH_5, 0)
          + ISNULL(T2.JISSEKI_MONTH_6, 0)
          + ISNULL(T2.JISSEKI_MONTH_7, 0)
          + ISNULL(T2.JISSEKI_MONTH_8, 0)
          + ISNULL(T2.JISSEKI_MONTH_9, 0)
          + ISNULL(T2.JISSEKI_MONTH_10, 0)
          + ISNULL(T2.JISSEKI_MONTH_11, 0)
          + ISNULL(T2.JISSEKI_MONTH_12, 0))       as    JISSEKI_GOUKEI
    from
        YOSAN_GETUJI T1
    full outer join
        JISSEKI_GETUJI T2
      on
            T1.BUSHO_CD = T2.BUSHO_CD
        and T1.SHAIN_CD = T2.SHAIN_CD
)
select
     SHAIN.BUSHO_CD                    as    BUSHO_CD
    ,SHAIN.BUSHO_NAME_RYAKU            as    BUSHO_NAME
    ,SHAIN.SHAIN_CD                    as    SHAIN_CD
    ,SHAIN.SHAIN_NAME_RYAKU            as    SHAIN_NAME
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_1, 0)         as    YOSAN_1
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_2, 0)         as    YOSAN_2
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_3, 0)         as    YOSAN_3
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_4, 0)         as    YOSAN_4
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_5, 0)         as    YOSAN_5
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_6, 0)         as    YOSAN_6
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_7, 0)         as    YOSAN_7
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_8, 0)         as    YOSAN_8
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_9, 0)         as    YOSAN_9
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_10, 0)        as    YOSAN_10
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_11, 0)        as    YOSAN_11
    ,ISNULL(GETUJI_INFO.YOSAN_MONTH_12, 0)        as    YOSAN_12
    ,ISNULL(GETUJI_INFO.YOSAN_GOUKEI, 0)          as    YOSAN_GOUKEI
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_1, 0)       as    JISSEKI_1
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_2, 0)       as    JISSEKI_2
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_3, 0)       as    JISSEKI_3
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_4, 0)       as    JISSEKI_4
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_5, 0)       as    JISSEKI_5
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_6, 0)       as    JISSEKI_6
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_7, 0)       as    JISSEKI_7
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_8, 0)       as    JISSEKI_8
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_9, 0)       as    JISSEKI_9
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_10, 0)      as    JISSEKI_10
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_11, 0)      as    JISSEKI_11
    ,ISNULL(GETUJI_INFO.JISSEKI_MONTH_12, 0)      as    JISSEKI_12
    ,ISNULL(GETUJI_INFO.JISSEKI_GOUKEI, 0)        as    JISSEKI_GOUKEI
from
    SHAIN
left outer join
    GETUJI_INFO
  on
        SHAIN.BUSHO_CD = GETUJI_INFO.BUSHO_CD
    and SHAIN.SHAIN_CD = GETUJI_INFO.SHAIN_CD
order by BUSHO_CD, SHAIN_CD
