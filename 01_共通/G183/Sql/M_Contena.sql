SELECT
	 RIGHT('000' + MCS.CONTENA_SHURUI_CD,3) AS CONTENA_SHURUI_CD,
	 MCS.CONTENA_SHURUI_NAME_RYAKU,
	 RIGHT('0000000000' + MC.CONTENA_CD,10) AS CONTENA_CD,
	 MC.CONTENA_NAME_RYAKU,
	 CASE MC.GYOUSHA_CD
	 WHEN '' THEN ''
	 ELSE RIGHT('000000' + MC.GYOUSHA_CD,6) 
	 END AS GYOUSHA_CD,
	 MGS.GYOUSHA_NAME_RYAKU,
	 CASE MC.GENBA_CD
	 WHEN '' THEN ''
	 ELSE RIGHT('000000' + MC.GENBA_CD,6) 
	 END AS GENBA_CD,
	 MGB.GENBA_NAME_RYAKU,
	 MCS.CONTENA_SHURUI_FURIGANA
FROM M_CONTENA_SHURUI MCS
	 JOIN M_CONTENA MC
	   ON MCS.CONTENA_SHURUI_CD = MC.CONTENA_SHURUI_CD
	LEFT JOIN M_GYOUSHA MGS
	   ON MC.GYOUSHA_CD = MGS.GYOUSHA_CD
	LEFT JOIN M_GENBA MGB
	   ON MC.GYOUSHA_CD = MGB.GYOUSHA_CD
	  AND MC.GENBA_CD = MGB.GENBA_CD
WHERE MCS.DELETE_FLG <>'1'
  AND MC.DELETE_FLG <>'1'
/*IF data.Gyousya_Cd != null && data.Gyousya_Cd != ''*/
  AND MC.GYOUSHA_CD = /*data.Gyousya_Cd*/ /*END*/
/*IF data.Gennba_Cd != null && data.Gennba_Cd != ''*/
  AND MC.GENBA_CD = /*data.Gennba_Cd*/ /*END*/
/*IF data.Parent_Condition_Item != null && data.Parent_Condition_Item != ''*/
  /*IF data.Parent_Condition_Item == '1'*/
  AND MCS.CONTENA_SHURUI_CD LIKE '%' + /*data.Parent_Condition_Value*/ + '%'/*END*/
  /*IF data.Parent_Condition_Item == '2'*/
  AND MCS.CONTENA_SHURUI_NAME_RYAKU LIKE '%' + /*data.Parent_Condition_Value*/ + '%'/*END*/
  /*IF data.Parent_Condition_Item == '3'*/
  AND MCS.CONTENA_SHURUI_FURIGANA LIKE '%' + /*data.Parent_Condition_Value*/ + '%'/*END*/
  /*IF data.Parent_Condition_Item == '4'*/
  AND (MCS.CONTENA_SHURUI_CD LIKE '%' + /*data.Parent_Condition_Value*/ + '%'
	  OR MCS.CONTENA_SHURUI_NAME_RYAKU LIKE '%' + /*data.Parent_Condition_Value*/ + '%'
	  OR MCS.CONTENA_SHURUI_FURIGANA LIKE '%' + /*data.Parent_Condition_Value*/ + '%')
   /*END*/
/*END*/
/*IF data.Child_Condition_Item != null && data.Child_Condition_Item != ''*/
  /*IF data.Child_Condition_Item == '1'*/
  AND MC.CONTENA_CD LIKE '%' + /*data.Child_Condition_Value*/ + '%'/*END*/
  /*IF data.Child_Condition_Item == '2'*/
  AND MC.CONTENA_NAME_RYAKU LIKE '%' + /*data.Child_Condition_Value*/ + '%'/*END*/
  /*IF data.Child_Condition_Item == '3'*/
  AND (MC.CONTENA_CD LIKE '%' + /*data.Child_Condition_Value*/ + '%'
	  OR MC.CONTENA_NAME_RYAKU LIKE '%' + /*data.Child_Condition_Value*/ + '%')
   /*END*/
/*END*/
