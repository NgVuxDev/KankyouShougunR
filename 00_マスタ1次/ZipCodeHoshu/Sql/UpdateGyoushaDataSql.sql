UPDATE dbo.M_GYOUSHA
   SET GYOUSHA_POST = Replace(GYOUSHA_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, GYOUSHA_ADDRESS1 = Replace(GYOUSHA_ADDRESS1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
       /*IF oldAddress != null && newAddress != null*/, GYOUSHA_ADDRESS2 = Replace(GYOUSHA_ADDRESS2, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'