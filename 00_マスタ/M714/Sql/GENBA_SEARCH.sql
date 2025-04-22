/*IF data.ChiikiMasterName != null && data.ChiikiMasterName != '' && data.ChiikiMasterName == '2'*/
  SELECT 
      MG.GYOUSHA_CD AS GYOUSHA_CD
    , MG.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
    , MGB.GENBA_CD AS GENBA_CD
    , MGB.GENBA_NAME_RYAKU AS GENBA_NAME
    , MGB.GENBA_ADDRESS1 AS JYUUSHO
    , MGB.CHIIKI_CD AS CHIIKI_CD
    , MC.CHIIKI_NAME_RYAKU AS CHIIKI_NAME
    , MGB.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS UPN_TEISHUTU_CD
    , UPN_MC.CHIIKI_NAME_RYAKU AS UPN_TEISHUTU_NAME
  FROM M_GENBA MGB
    LEFT JOIN M_GYOUSHA MG
      ON MGB.GYOUSHA_CD = MG.GYOUSHA_CD
    LEFT JOIN M_CHIIKI MC
      ON MGB.CHIIKI_CD = MC.CHIIKI_CD
    LEFT JOIN M_CHIIKI UPN_MC
      ON MGB.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_MC.CHIIKI_CD
  
  WHERE 1 = 1
  /*IF data.ChiikiCdOld != null && data.ChiikiCdOld != ''*/
  AND MGB.CHIIKI_CD = /*data.ChiikiCdOld*/'000001'
  /*END*/
  /*IF data.ChiikiJyuushoOld != null && data.ChiikiJyuushoOld != ''*/
  AND MGB.GENBA_ADDRESS1 LIKE /*data.ChiikiJyuushoOld*/'' + '%'
  /*END*/

/*END*/

/*IF data.ChiikiMasterName != null && data.ChiikiMasterName != '' && data.ChiikiMasterName == '4'*/
  SELECT 
      MG.GYOUSHA_CD AS GYOUSHA_CD
    , MG.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
    , MGB.GENBA_CD AS GENBA_CD
    , MGB.GENBA_NAME_RYAKU AS GENBA_NAME
    , MGB.GENBA_ADDRESS1 AS JYUUSHO
    , MGB.CHIIKI_CD AS CHIIKI_CD
    , MC.CHIIKI_NAME_RYAKU AS CHIIKI_NAME
    , MGB.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS UPN_TEISHUTU_CD
    , UPN_MC.CHIIKI_NAME_RYAKU AS UPN_TEISHUTU_NAME
  FROM M_HIKIAI_GENBA MGB
    LEFT JOIN M_GYOUSHA MG
      ON MGB.GYOUSHA_CD = MG.GYOUSHA_CD
    LEFT JOIN M_CHIIKI MC
      ON MGB.CHIIKI_CD = MC.CHIIKI_CD
    LEFT JOIN M_CHIIKI UPN_MC
      ON MGB.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_MC.CHIIKI_CD
  
  WHERE 1 = 1
  /*IF data.ChiikiCdOld != null && data.ChiikiCdOld != ''*/
  AND MGB.CHIIKI_CD = /*data.ChiikiCdOld*/'000001'
  /*END*/
  /*IF data.ChiikiJyuushoOld != null && data.ChiikiJyuushoOld != ''*/
  AND MGB.GENBA_ADDRESS1 LIKE /*data.ChiikiJyuushoOld*/'' + '%'
  /*END*/

/*END*/

/*IF data.ChiikiMasterName != null && data.ChiikiMasterName != '' && data.ChiikiMasterName == '6'*/
  SELECT 
      MG.GYOUSHA_CD AS GYOUSHA_CD
    , MG.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
    , MGB.GENBA_CD AS GENBA_CD
    , MGB.GENBA_NAME_RYAKU AS GENBA_NAME
    , MGB.GENBA_ADDRESS1 AS JYUUSHO
    , MGB.CHIIKI_CD AS CHIIKI_CD
    , MC.CHIIKI_NAME_RYAKU AS CHIIKI_NAME
    , NULL AS UPN_TEISHUTU_CD
    , NULL AS UPN_TEISHUTU_NAME
  FROM M_KARI_GENBA MGB
    LEFT JOIN M_GYOUSHA MG
      ON MGB.GYOUSHA_CD = MG.GYOUSHA_CD
    LEFT JOIN M_CHIIKI MC
      ON MGB.CHIIKI_CD = MC.CHIIKI_CD
  
  WHERE 1 = 1
  /*IF data.ChiikiCdOld != null && data.ChiikiCdOld != ''*/
  AND MGB.CHIIKI_CD = /*data.ChiikiCdOld*/'000001'
  /*END*/
  /*IF data.ChiikiJyuushoOld != null && data.ChiikiJyuushoOld != ''*/
  AND MGB.GENBA_ADDRESS1 LIKE /*data.ChiikiJyuushoOld*/'' + '%'
  /*END*/

/*END*/