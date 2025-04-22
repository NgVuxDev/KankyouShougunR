select distinct
  kihon_gyousha.GYOUSHA_NAME_RYAKU as KIHON_GYOUSHA_NAME_RYAKU
  ,kihon_genba.GENBA_NAME_RYAKU as KIHON_GENBA_NAME_RYAKU
  ,kihon_hst_genba.HAISHUTSU_JIGYOUSHA_CD as HTS_HAISHUTSU_JIGYOUSHA_CD
  ,kihon_hst_genba.HAISHUTSU_JIGYOUJOU_CD as HTS_HAISHUTSU_JIGYOUJOU_CD
  ,kihon_hst_genba.HAISHUTSU_JIGYOUJOU_NAME as HTS_HAISHUTSU_JIGYOUJOU_NAME
  ,kihon_hst_genba.HAISHUTSU_JIGYOUJOU_ADDRESS1 as HTS_HAISHUTSU_JIGYOUJOU_ADDRESS1
  ,kihon_hst_genba.HAISHUTSU_JIGYOUJOU_ADDRESS2 as HTS_HAISHUTSU_JIGYOUJOU_ADDRESS2
  ,hts_todouhuken.TODOUFUKEN_NAME as HTS_TODOUFUKEN_NAME

from M_ITAKU_KEIYAKU_KIHON kihon
left join M_GYOUSHA kihon_gyousha
  on kihon.HAISHUTSU_JIGYOUSHA_CD = kihon_gyousha.GYOUSHA_CD
left join M_GENBA kihon_genba
  on kihon.HAISHUTSU_JIGYOUSHA_CD = kihon_genba.GYOUSHA_CD
 and kihon.HAISHUTSU_JIGYOUJOU_CD = kihon_genba.GENBA_CD
left join M_ITAKU_KEIYAKU_KIHON_HST_GENBA kihon_hst_genba
  on kihon.SYSTEM_ID = kihon_hst_genba.SYSTEM_ID
left join M_GENBA hst_genba
  on kihon_hst_genba.HAISHUTSU_JIGYOUSHA_CD = hst_genba.GYOUSHA_CD
 and kihon_hst_genba.HAISHUTSU_JIGYOUJOU_CD = hst_genba.GENBA_CD
left join M_TODOUFUKEN hts_todouhuken
  on hst_genba.GENBA_TODOUFUKEN_CD = hts_todouhuken.TODOUFUKEN_CD
where
  kihon.DELETE_FLG = 0
  AND kihon.SYSTEM_ID = /*searchData.SYSTEM_ID*/
