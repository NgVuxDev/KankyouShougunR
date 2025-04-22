UPDATE dbo.M_SYUKKINSAKI
   SET SYUKKINSAKI_POST = Replace(SYUKKINSAKI_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, SYUKKINSAKI_ADDRESS1 = Replace(SYUKKINSAKI_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, SYUKKINSAKI_ADDRESS2 = Replace(SYUKKINSAKI_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE SYUKKINSAKI_CD = /*data.SYUKKINSAKI_CD*/1