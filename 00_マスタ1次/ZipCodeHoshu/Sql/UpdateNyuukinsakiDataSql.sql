UPDATE dbo.M_NYUUKINSAKI
   SET NYUUKINSAKI_POST = Replace(NYUUKINSAKI_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, NYUUKINSAKI_ADDRESS1 = Replace(NYUUKINSAKI_ADDRESS1, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
       /*IF oldAddress != null && newAddress != null*/, NYUUKINSAKI_ADDRESS2 = Replace(NYUUKINSAKI_ADDRESS2, /*oldAddress*/'検索対象', /*newAddress*/'置換内容')/*END*/
 WHERE NYUUKINSAKI_CD = /*data.NYUUKINSAKI_CD*/1