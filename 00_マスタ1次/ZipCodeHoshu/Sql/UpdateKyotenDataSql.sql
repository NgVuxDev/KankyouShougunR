UPDATE dbo.M_KYOTEN
   SET KYOTEN_POST = Replace(KYOTEN_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, KYOTEN_ADDRESS1 = Replace(KYOTEN_ADDRESS1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
       /*IF oldAddress != null && newAddress != null*/, KYOTEN_ADDRESS2 = Replace(KYOTEN_ADDRESS2, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 WHERE KYOTEN_CD = /*data.KYOTEN_CD*/1