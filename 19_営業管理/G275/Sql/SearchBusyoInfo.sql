    select 
         M1.BUSHO_CD         as BUSHO_CD
        ,M1.BUSHO_NAME_RYAKU as BUSHO_NAME_RYAKU
    from 
        M_BUSHO M1    -- 部署マスタ
    where
        1 = 1
/*IF data.busyouCD != null && data.busyouCD != ''*/
        and M1.BUSHO_CD           =  /*data.busyouCD*/ --部署コード
/*END*/
