delete
from T_ZAIKO_TANK_DETAIL    --在庫締明細
where SYSTEM_ID in
(
    select
        A1.SYSTEM_ID as SYSTEM_ID    --システムID
    from
        T_ZAIKO_TANK A1    --在庫締データ
    where
            A1.ZAIKO_SHIME_DATE    >=    /*data.simeTaisyouKikanFrom*/
        and A1.ZAIKO_SHIME_DATE    <=    /*data.simeTaisyouKikanTo*/
    /*IF data.gyoushaCD != null && data.gyoushaCD != ''*/
        and A1.GYOUSHA_CD          =     /*data.gyoushaCD*/
    /*END*/
    /*IF data.genbaCD != null && data.genbaCD != ''*/
        and A1.GENBA_CD            =     /*data.genbaCD*/
    /*END*/
)
