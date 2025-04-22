select
    M1.ZAIKO_HYOUKA_HOUHOU  as  RET_ZAIKO_HYOUKA_HOUHOU    --在庫評価方法
from
    M_SYS_INFO  M1    --システム設定
where
    SYS_ID = 0
    and DELETE_FLG  = 0