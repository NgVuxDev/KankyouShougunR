SELECT 
    TORIHIKISAKI.TORIHIKISAKI_CD AS 取引先CD,
    TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS 取引先名,
    TORIHIKISAKI.TORIHIKISAKI_FURIGANA AS 取引先フリガナ,
    TORIHIKISAKI.SHAIN_NAME_RYAKU AS 営業者名,
    TORIHIKISAKI.TORIHIKISAKI_POST AS 郵便番号,
    TORIHIKISAKI.TORIHIKISAKI_TEL AS 電話番号,
    TORIHIKISAKI.TORIHIKISAKI_FAX AS FAX番号,
    TORIHIKISAKI.BIKOU1 AS 備考1,
    TORIHIKISAKI.BIKOU2 AS 備考2,
    GENGYOU.GYO_GYOUSHA_CD AS 業者CD,
    GENGYOU.GYO_GYOUSHA_NAME_RYAKU AS 業者名,
    GENGYOU.GYO_GYOUSHA_FURIGANA AS 業者フリガナ,
    GENGYOU.GYO_SHAIN_NAME_RYAKU AS 営業者名,
    GENGYOU.GYO_GYOUSHA_POST AS 郵便番号,
    GENGYOU.GYO_GYOUSHA_TEL AS 電話番号,
    GENGYOU.GYO_GYOUSHA_FAX AS FAX番号,
    GENGYOU.GYO_BIKOU1 AS 備考1,
    GENGYOU.GYO_BIKOU2 AS 備考2,
    GENGYOU.GEN_GENBA_CD AS 現場CD,
    GENGYOU.GEN_GENBA_NAME_RYAKU AS 現場名,
    GENGYOU.GEN_GENBA_FURIGANA AS 現場フリガナ,
    GENGYOU.GEN_SHAIN_NAME_RYAKU AS 営業者名,
    GENGYOU.GEN_GENBA_POST AS 郵便番号,
    GENGYOU.GEN_GENBA_TEL AS 電話番号,
    GENGYOU.GEN_GENBA_FAX AS FAX番号,
    GENGYOU.GEN_BIKOU1 AS 備考1,
    GENGYOU.GEN_BIKOU2 AS 備考2 

FROM 

    (
        SELECT 
            GENBA.TORIHIKISAKI_CD      AS GYO_TORIHIKISAKI_CD,
            GYOUSHA.GYOUSHA_CD         AS GYO_GYOUSHA_CD,
            GYOUSHA.GYOUSHA_NAME_RYAKU AS GYO_GYOUSHA_NAME_RYAKU,
            GYOUSHA.GYOUSHA_FURIGANA   AS GYO_GYOUSHA_FURIGANA,
            GYOUSHA.SHAIN_NAME_RYAKU   AS GYO_SHAIN_NAME_RYAKU,
            GYOUSHA.GYOUSHA_POST       AS GYO_GYOUSHA_POST,
            GYOUSHA.GYOUSHA_TEL        AS GYO_GYOUSHA_TEL,
            GYOUSHA.GYOUSHA_FAX        AS GYO_GYOUSHA_FAX,
            GYOUSHA.BIKOU1             AS GYO_BIKOU1,
            GYOUSHA.BIKOU2             AS GYO_BIKOU2,
            GENBA.GENBA_CD             AS GEN_GENBA_CD,
            GENBA.GENBA_NAME_RYAKU     AS GEN_GENBA_NAME_RYAKU,
            GENBA.GENBA_FURIGANA       AS GEN_GENBA_FURIGANA,
            GENBA.SHAIN_NAME_RYAKU     AS GEN_SHAIN_NAME_RYAKU,
            GENBA.GENBA_POST           AS GEN_GENBA_POST,
            GENBA.GENBA_TEL            AS GEN_GENBA_TEL,
            GENBA.GENBA_FAX            AS GEN_GENBA_FAX,
            GENBA.BIKOU1               AS GEN_BIKOU1,
            GENBA.BIKOU2               AS GEN_BIKOU2 

        FROM

            (
                SELECT            
                    M_GYOUSHA.TORIHIKISAKI_CD, 
                    M_GYOUSHA.GYOUSHA_CD, 
                    M_GYOUSHA.GYOUSHA_NAME_RYAKU, 
                    M_GYOUSHA.GYOUSHA_FURIGANA, 
                    M_GYOUSHA.EIGYOU_TANTOU_CD, 
                    M_SHAIN.SHAIN_NAME_RYAKU, 
                    M_GYOUSHA.GYOUSHA_POST, 
                    M_GYOUSHA.GYOUSHA_TEL, 
                    M_GYOUSHA.GYOUSHA_FAX, 
                    M_GYOUSHA.BIKOU1, 
                    M_GYOUSHA.BIKOU2
                FROM              
                    M_GYOUSHA LEFT OUTER JOIN M_SHAIN 
                ON M_GYOUSHA.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD
            ) AS GYOUSHA
            
                FULL OUTER JOIN
            
                    (
                        SELECT            
                            M_GENBA.TORIHIKISAKI_CD,
                            M_GENBA.GYOUSHA_CD, 
                            M_GENBA.GENBA_CD, 
                            M_GENBA.GENBA_NAME_RYAKU, 
                            M_GENBA.GENBA_FURIGANA, 
                            M_GENBA.EIGYOU_TANTOU_CD, 
                            M_SHAIN.SHAIN_NAME_RYAKU, 
                            M_GENBA.GENBA_POST, 
                            M_GENBA.GENBA_TEL, 
                            M_GENBA.GENBA_FAX, 
                            M_GENBA.BIKOU1, 
                            M_GENBA.BIKOU2
                        FROM              
                            M_GENBA LEFT OUTER JOIN M_SHAIN 
                            ON M_GENBA.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD
                    ) AS GENBA
        
                    ON GYOUSHA.GYOUSHA_CD = GENBA.GYOUSHA_CD
        
        ) AS GENGYOU

        FULL OUTER JOIN 

            (
                SELECT            
                    M_TORIHIKISAKI.TORIHIKISAKI_CD, 
                    M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU, 
                    M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA, 
                    M_TORIHIKISAKI.EIGYOU_TANTOU_CD, 
                    M_SHAIN.SHAIN_NAME_RYAKU, 
                    M_TORIHIKISAKI.TORIHIKISAKI_POST, 
                    M_TORIHIKISAKI.TORIHIKISAKI_TEL, 
                    M_TORIHIKISAKI.TORIHIKISAKI_FAX, 
                    M_TORIHIKISAKI.BIKOU1, 
                    M_TORIHIKISAKI.BIKOU2
                FROM              
                    M_TORIHIKISAKI LEFT OUTER JOIN M_SHAIN 
                ON M_TORIHIKISAKI.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD
            ) AS TORIHIKISAKI

ON TORIHIKISAKI.TORIHIKISAKI_CD = GENGYOU.GYO_TORIHIKISAKI_CD
