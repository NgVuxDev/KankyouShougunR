UPDATE dbo.M_NYUUKINSAKI
   SET NYUUKINSAKI_POST = Replace(NYUUKINSAKI_POST, /*oldPost*/'', /*newPost*/'')
       /*IF oldAddress != null && newAddress != null*/, NYUUKINSAKI_ADDRESS1 = Replace(NYUUKINSAKI_ADDRESS1, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
       /*IF oldAddress != null && newAddress != null*/, NYUUKINSAKI_ADDRESS2 = Replace(NYUUKINSAKI_ADDRESS2, /*oldAddress*/'�����Ώ�', /*newAddress*/'�u�����e')/*END*/
 WHERE NYUUKINSAKI_CD = /*data.NYUUKINSAKI_CD*/1