UPDATE dbo.M_KYOTEN
   SET KYOTEN_POST = Replace(KYOTEN_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, KYOTEN_ADDRESS1 = Replace(KYOTEN_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, KYOTEN_ADDRESS2 = Replace(KYOTEN_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE KYOTEN_CD = /*data.KYOTEN_CD*/1