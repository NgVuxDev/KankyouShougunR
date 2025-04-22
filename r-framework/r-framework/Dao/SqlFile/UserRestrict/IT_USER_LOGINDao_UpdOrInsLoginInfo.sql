-- ロックのタイムアウト(ミリ秒で設定)
SET LOCK_TIMEOUT 30000

/*IF !isTerminal*/
-- 同じPC名のデータがあった場合に最新LOGINデータ以外を削除
-- オンプレでログイン→クラウドでログイン→オンプレでログインで発生
-- ※同じPCを使用
IF (SELECT COUNT(*) FROM T_USER_LOGIN WHERE CLIENT_COMPUTER_NAME = /*computerName*/) > 1
 DELETE FROM T_USER_LOGIN 
 WHERE 
      CLIENT_COMPUTER_NAME = /*computerName*/
  AND LOGIN_TIME <> 
  (
   SELECT MAX(LOGIN_TIME) FROM T_USER_LOGIN WHERE CLIENT_COMPUTER_NAME = /*computerName*/
  );
;
/*END*/
/*IF isTerminal*/
-- 同じユーザーIDのデータがあった場合に最新LOGINデータ以外を削除
-- クラウドでログイン→オンプレでログイン→クラウドでログインで発生
-- ※Remote接続ユーザーIDとローカルのアカウント名が同じで異なるPC使用
IF (SELECT COUNT(*) FROM T_USER_LOGIN WHERE CLIENT_USER_NAME = /*userName*/) > 1
 DELETE FROM T_USER_LOGIN 
 WHERE 
      CLIENT_USER_NAME = /*userName*/
  AND LOGIN_TIME <> 
  (
   SELECT MAX(LOGIN_TIME) FROM T_USER_LOGIN WHERE CLIENT_USER_NAME = /*userName*/
  );
;
/*END*/

MERGE INTO dbo.T_USER_LOGIN AS TARGET
USING 
(
 SELECT
  -- CAL数チェックの結果
  CASE 
   WHEN LOGIN_COUNTER <= 0 THEN
    CASE
     WHEN (SELECT COUNT(*) FROM dbo.T_USER_LOGIN WITH(TABLOCK, HOLDLOCK) WHERE LOGIN_COUNTER > 0) < /*cal*/ THEN 
      -- CALチェックの結果、ログインOK
      0
     ELSE 
      -- CALチェックの結果、ログインNG
      -1
    END
   WHEN LOGIN_COUNTER > 0 THEN
    -- 再ログインの為、ポップアップ表示
    -1
  END RESULT
  ,LOGIN_ID
  /*IF !isTerminal*/
  ,CLIENT_COMPUTER_NAME
  /*END*/
  /*IF isTerminal*/
  ,CLIENT_USER_NAME
  /*END*/
 FROM
 (
  SELECT
    LOGIN_ID
   ,LOGIN_COUNTER
   /*IF !isTerminal*/
   ,CLIENT_COMPUTER_NAME
   /*END*/
   /*IF isTerminal*/
   ,CLIENT_USER_NAME
   /*END*/
  FROM
  (
    SELECT 
	  LOGIN_ID
	 ,LOGIN_COUNTER
	 /*IF !isTerminal*/
	 ,CLIENT_COMPUTER_NAME
	 /*END*/
	 /*IF isTerminal*/
	 ,CLIENT_USER_NAME
	 /*END*/
	FROM 
	 dbo.T_USER_LOGIN WITH(TABLOCK, HOLDLOCK) 
	WHERE 
	/*IF !isTerminal*/
	CLIENT_COMPUTER_NAME = /*computerName*/
    /*END*/
    /*IF isTerminal*/
	CLIENT_USER_NAME = /*userName*/
    /*END*/
    UNION ALL
    SELECT 
	  '' LOGIN_ID
	 , -1 LOGIN_COUNTER
	 /*IF !isTerminal*/
	 ,'' CLIENT_COMPUTER_NAME
	 /*END*/
	 /*IF isTerminal*/
	 ,'' CLIENT_USER_NAME
	 /*END*/
	WHERE
	 NOT EXISTS
	 (
	  SELECT 
	   *
	  FROM 
	   dbo.T_USER_LOGIN WITH(TABLOCK, HOLDLOCK) 
	  WHERE 
	   /*IF !isTerminal*/
	   CLIENT_COMPUTER_NAME = /*computerName*/
       /*END*/
       /*IF isTerminal*/
	   CLIENT_USER_NAME = /*userName*/
       /*END*/
	 )
  ) A
 ) LOGIN_COUNTER_RESULT
) AS SOURCE
ON (
    /*IF !isTerminal*/
	TARGET.CLIENT_COMPUTER_NAME = SOURCE.CLIENT_COMPUTER_NAME
	/*END*/
	/*IF isTerminal*/
	TARGET.CLIENT_USER_NAME = SOURCE.CLIENT_USER_NAME
	/*END*/
   )
-- 更新
WHEN MATCHED AND SOURCE.RESULT = 0 THEN
 UPDATE SET 
 TARGET.LOGIN_ID = /*loginId*/,
 TARGET.LOGIN_COUNTER = TARGET.LOGIN_COUNTER + 1,
 TARGET.CLIENT_COMPUTER_NAME = /*computerName*/,
 TARGET.CLIENT_USER_NAME = /*userName*/,
 TARGET.LOGIN_TIME = CURRENT_TIMESTAMP
-- 新規登録
WHEN NOT MATCHED AND SOURCE.RESULT = 0 THEN
 INSERT (LOGIN_ID, LOGIN_COUNTER, CLIENT_COMPUTER_NAME, CLIENT_USER_NAME, LOGIN_TIME, LOGOUT_TIME, CREATE_DATE)
 VALUES (/*loginId*/, 1, /*computerName*/,/*userName*/,CURRENT_TIMESTAMP, null, CURRENT_TIMESTAMP);
;

IF @@ROWCOUNT = 0 
 --更新件数0件でLOGIN_ID、CLIENT_COMPUTER_NAMEが存在し、LOGIN_COUNTER > 0
 IF 
   (
     SELECT
      LOGIN_COUNTER
     FROM
     (
       SELECT 
	    LOGIN_COUNTER
	   FROM 
	    dbo.T_USER_LOGIN 
	   WHERE 
        /*IF !isTerminal*/
	    CLIENT_COMPUTER_NAME = /*computerName*/
		/*END*/
		/*IF isTerminal*/
		CLIENT_USER_NAME = /*userName*/
		/*END*/
       UNION ALL
       SELECT 
	    -1 LOGIN_COUNTER 
	   WHERE 
        NOT EXISTS
		(
		 SELECT
		  *
		 FROM
		  dbo.T_USER_LOGIN
		 WHERE
          /*IF !isTerminal*/
	      CLIENT_COMPUTER_NAME = /*computerName*/
		  /*END*/
		  /*IF isTerminal*/
		  CLIENT_USER_NAME = /*userName*/
		  /*END*/
	    )
     ) A
   ) > 0
  SELECT 
    -9 RETURN_CODE
   ,CLIENT_COMPUTER_NAME 
  FROM 
   dbo.T_USER_LOGIN 
  WHERE 
   /*IF !isTerminal*/
   CLIENT_COMPUTER_NAME = /*computerName*/
   /*END*/
   /*IF isTerminal*/
   CLIENT_USER_NAME = /*userName*/
   /*END*/
 ELSE
  --更新件数0件でCAL数チェックNG
  SELECT 
    0 RETURN_CODE
   /*IF !isTerminal*/
   ,NULL CLIENT_COMPUTER_NAME
   /*END*/
   /*IF isTerminal*/
   ,NULL CLIENT_USER_NAME
   /*END*/
ELSE
 --更新件数>0でCAL数チェックOK
 SELECT 
   1 RETURN_CODE
   /*IF !isTerminal*/
   ,NULL CLIENT_COMPUTER_NAME
   /*END*/
   /*IF isTerminal*/
   ,NULL CLIENT_USER_NAME
   /*END*/

-- 最後のSELECT(RETURN_CODE)について
--  0 -> CALチェックNG
--  1 -> CALチェックOK
-- -9 -> 再ログインの為、ポップアップ表示
