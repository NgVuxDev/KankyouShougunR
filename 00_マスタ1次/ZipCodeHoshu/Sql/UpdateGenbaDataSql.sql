UPDATE dbo.M_GENBA
   SET GENBA_POST = Replace(GENBA_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, GENBA_ADDRESS1 = Replace(GENBA_ADDRESS1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
       /*IF oldAddress != null && newAddress != null*/, GENBA_ADDRESS2 = Replace(GENBA_ADDRESS2, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
   AND GENBA_CD = /*data.GENBA_CD*/'000001'