select
     M2.GYOUSHA_CD          as  RET_GYOUSHA_CD            --業者CD
    ,M2.GYOUSHA_NAME_RYAKU  as  RET_GYOUSHA_NAME_RYAKU    --業者略称名
    ,M1.GENBA_CD            as  RET_GENBA_CD              --現場CD
    ,M1.GENBA_NAME_RYAKU    as  RET_GENBA_NAME_RYAKU      --現場略称名
from
    dbo.M_GENBA   M1,    --現場
    dbo.M_GYOUSHA M2     --業者
where
        M1.GENBA_CD   = /*genbaCD*/
/*IF gyoushaCD != null && gyoushaCD != ''*/
    and M1.GYOUSHA_CD = /*gyoushaCD*/
/*END*/
	and M1.JISHA_KBN = 1
	and M1.SHOBUN_NIOROSHI_GENBA_KBN = 1
    and M1.GYOUSHA_CD = M2.GYOUSHA_CD
	and M2.JISHA_KBN = 1
	and M2.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1