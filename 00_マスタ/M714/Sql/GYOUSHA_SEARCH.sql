/*IF data.ChiikiMasterName != null && data.ChiikiMasterName != '' && data.ChiikiMasterName == '1'*/
  SELECT 
      MG.GYOUSHA_CD AS GYOUSHA_CD
    , MG.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
    , NULL AS GENBA_CD
    , NULL AS GENBA_NAME
    , MG.GYOUSHA_ADDRESS1 AS JYUUSHO
    , MG.CHIIKI_CD AS CHIIKI_CD
    , MC.CHIIKI_NAME_RYAKU AS CHIIKI_NAME
    , MG.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS UPN_TEISHUTU_CD
    , UPN_MC.CHIIKI_NAME_RYAKU AS UPN_TEISHUTU_NAME
  FROM M_GYOUSHA MG
    LEFT JOIN M_CHIIKI MC
      ON MG.CHIIKI_CD = MC.CHIIKI_CD
    LEFT JOIN M_CHIIKI UPN_MC
      ON MG.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_MC.CHIIKI_CD
  
  WHERE 1 = 1
  /*IF data.ChiikiCdOld != null && data.ChiikiCdOld != ''*/
  AND MG.CHIIKI_CD = /*data.ChiikiCdOld*/'000001'
  /*END*/
  /*IF data.ChiikiJyuushoOld != null && data.ChiikiJyuushoOld != ''*/
  AND MG.GYOUSHA_ADDRESS1 LIKE /*data.ChiikiJyuushoOld*/'' + '%'
  /*END*/

/*END*/

/*IF data.ChiikiMasterName != null && data.ChiikiMasterName != '' && data.ChiikiMasterName == '3'*/
  SELECT 
      MHG.GYOUSHA_CD AS GYOUSHA_CD
    , MHG.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
    , NULL AS GENBA_CD
    , NULL AS GENBA_NAME
    , MHG.GYOUSHA_ADDRESS1 AS JYUUSHO
    , MHG.CHIIKI_CD AS CHIIKI_CD
    , MC.CHIIKI_NAME_RYAKU AS CHIIKI_NAME
    , MHG.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD AS UPN_TEISHUTU_CD
    , UPN_MC.CHIIKI_NAME_RYAKU AS UPN_TEISHUTU_NAME
  FROM M_HIKIAI_GYOUSHA MHG
    LEFT JOIN M_CHIIKI MC
      ON MHG.CHIIKI_CD = MC.CHIIKI_CD
    LEFT JOIN M_CHIIKI UPN_MC
      ON MHG.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = UPN_MC.CHIIKI_CD
  
  WHERE 1 = 1
  /*IF data.ChiikiCdOld != null && data.ChiikiCdOld != ''*/
  AND MHG.CHIIKI_CD = /*data.ChiikiCdOld*/'000001'
  /*END*/
  /*IF data.ChiikiJyuushoOld != null && data.ChiikiJyuushoOld != ''*/
  AND MHG.GYOUSHA_ADDRESS1 LIKE /*data.ChiikiJyuushoOld*/'' + '%'
  /*END*/

/*END*/

/*IF data.ChiikiMasterName != null && data.ChiikiMasterName != '' && data.ChiikiMasterName == '5'*/
  SELECT 
      MKG.GYOUSHA_CD AS GYOUSHA_CD
    , MKG.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME
    , NULL AS GENBA_CD
    , NULL AS GENBA_NAME
    , MKG.GYOUSHA_ADDRESS1 AS JYUUSHO
    , MKG.CHIIKI_CD AS CHIIKI_CD
    , MC.CHIIKI_NAME_RYAKU AS CHIIKI_NAME
    , NULL AS UPN_TEISHUTU_CD
    , NULL AS UPN_TEISHUTU_NAME
  FROM M_KARI_GYOUSHA MKG
    LEFT JOIN M_CHIIKI MC
      ON MKG.CHIIKI_CD = MC.CHIIKI_CD
  
  WHERE 1 = 1
  /*IF data.ChiikiCdOld != null && data.ChiikiCdOld != ''*/
  AND MKG.CHIIKI_CD = /*data.ChiikiCdOld*/'000001'
  /*END*/
  /*IF data.ChiikiJyuushoOld != null && data.ChiikiJyuushoOld != ''*/
  AND MKG.GYOUSHA_ADDRESS1 LIKE /*data.ChiikiJyuushoOld*/'' + '%'
  /*END*/

/*END*/