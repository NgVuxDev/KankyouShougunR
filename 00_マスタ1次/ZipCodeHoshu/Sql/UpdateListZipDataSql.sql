UPDATE dbo.S_ZIP_CODE
   SET POST7 = Replace(POST7, /*oldPost*/'', /*newPost*/'')
		/*IF oldAddress != null && newAddress != null*/, SIKUCHOUSON = Replace(SIKUCHOUSON, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
		/*IF oldAddress != null && newAddress != null*/, OTHER1 = Replace(OTHER1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 /*BEGIN*/
 WHERE
  /*IF oldPost != null*/POST7 = /*oldPost*/'000-0000'/*END*/
  /*IF oldAddress != null*/AND SIKUCHOUSON + OTHER1 LIKE '%' + /*oldAddress*/'�����Ώ�' + '%'/*END*/
 /*END*/