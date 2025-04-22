UPDATE dbo.M_GYOUSHA
   SET GYOUSHA_POST = Replace(GYOUSHA_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, GYOUSHA_ADDRESS1 = Replace(GYOUSHA_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, GYOUSHA_ADDRESS2 = Replace(GYOUSHA_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'