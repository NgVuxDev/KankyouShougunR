select distinct
  kihon.ITAKU_KEIYAKU_FILE_PATH
  ,kihon.HAISHUTSU_JIGYOUSHA_CD
  ,kihon.HAISHUTSU_JIGYOUJOU_CD
  ,kihon.KOBETSU_SHITEI_CHECK
  ,kihon.BIKOU1
  ,kihon.BIKOU2
  ,kihon.ITAKU_KEIYAKU_TYPE
  ,kihon.ITAKU_KEIYAKU_SHURUI
  ,kihon.KEIYAKUSHO_KEIYAKU_DATE
  ,kihon.KEIYAKUSHO_CREATE_DATE
  ,kihon.KEIYAKUSHO_SEND_DATE
  ,kihon.ITAKU_KEIYAKU_NO
  ,kihon.KOUSHIN_SHUBETSU
  ,kihon.YUUKOU_BEGIN
  ,kihon.YUUKOU_END
  ,kihon.KOUSHIN_END_DATE
  ,kihon.HST_FREE_COMMENT
  ,sbn_kyokasho.KYOKA_KBN as SBN_KYOKA_KBN
  ,upn_kyokasho.KYOKA_KBN as UPN_KYOKA_KBN
  ,sbn_kyokasho.GYOUSHA_CD as SBN_GYOUSHA_CD
  ,sbn_gyousha.GYOUSHA_NAME_RYAKU as SBN_GYOUSHA_NAME
  ,sbn_kyokasho.GENBA_CD as SBN_GENBA_CD
  ,sbn_genba.GENBA_NAME_RYAKU as SBN_GENBA_NAME
  ,sbn_kyokasho.CHIIKI_CD as SBN_CHIIKI_CD
  ,upn_kyokasho.GYOUSHA_CD as UPN_GYOUSHA_CD
  ,upn_gyousha.GYOUSHA_NAME_RYAKU as UPN_GYOUSHA_NAME
  ,upn_kyokasho.GENBA_CD as UPN_GENBA_CD
  ,upn_genba.GENBA_NAME_RYAKU as UPN_GENBA_NAME
  ,upn_kyokasho.CHIIKI_CD as UPN_CHIIKI_CD

from M_ITAKU_KEIYAKU_KIHON kihon
left join M_ITAKU_SBN_KYOKASHO sbn_kyokasho
  on kihon.SYSTEM_ID = sbn_kyokasho.SYSTEM_ID
left join M_GYOUSHA sbn_gyousha
  on sbn_kyokasho.GYOUSHA_CD = sbn_gyousha.GYOUSHA_CD
left join M_GENBA sbn_genba
  on sbn_kyokasho.GYOUSHA_CD = sbn_genba.GYOUSHA_CD
 and sbn_kyokasho.GENBA_CD = sbn_genba.GENBA_CD
left join M_ITAKU_UPN_KYOKASHO upn_kyokasho
  on kihon.SYSTEM_ID = upn_kyokasho.SYSTEM_ID
left join M_GYOUSHA upn_gyousha
  on upn_kyokasho.GYOUSHA_CD = upn_gyousha.GYOUSHA_CD
left join M_GENBA upn_genba
  on upn_kyokasho.GYOUSHA_CD = upn_genba.GYOUSHA_CD
 and upn_kyokasho.GENBA_CD = upn_genba.GENBA_CD
where
  kihon.DELETE_FLG = 0
  AND kihon.SYSTEM_ID = /*searchData.SYSTEM_ID*/

