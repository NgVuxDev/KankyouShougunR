using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.Scale.Keiryou.Dto
{
    internal class DTOClass
    {
       
        /// <summary>
        /// T_KEIRYOU_ENTRY用のEntity
        /// </summary>
        internal T_KEIRYOU_ENTRY entryEntity;

        /// <summary>
        /// T_KEIRYOU_DETAIL
        /// </summary>
        internal T_KEIRYOU_DETAIL[] detailEntity;

        /// <summary>
        /// T_UKETSUKE_SS_ENTRY用のEntity
        /// </summary>
        internal T_UKETSUKE_SS_ENTRY uketsukeEntity;

        /// <summary>
        /// T_UKETSUKE_SS_DETAIL
        /// </summary>
        internal T_UKETSUKE_SS_DETAIL[] uketsukeDetailEntity;


        /// <summary>
        /// M_SYS_INFO
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// M_CONTENA_JOUKYOU
        /// </summary>
        internal M_CONTENA_JOUKYOU contenaEntity;

        /// <summary>
        /// M_MANIFEST_SHURUI
        /// </summary>
        internal M_MANIFEST_SHURUI manifestShuruiEntity;

        /// <summary>
        /// M_MANIFEST_TEHAI
        /// </summary>
        internal M_MANIFEST_TEHAI manifestTehaiEntity;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity;

        /// <summary>
        /// S_NUMBER_DAY
        /// </summary>
        internal S_NUMBER_DAY numberDay;

        /// <summary>
        /// S_NUMBER_YEAR
        /// </summary>
        internal S_NUMBER_YEAR numberYear;

        /// <summary>
        /// S_NUMBER_RECEIPT
        /// </summary>
        internal S_NUMBER_RECEIPT numberReceipt;

        /// <summary>
        /// マニフェスト
        /// </summary>
        internal DataTable manifestEntrys;

        /// <summary>
        /// M_KEITAI_KBN
        /// </summary>
        internal M_KEITAI_KBN keitaiKbnEntity;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity1;

        /// <summary>
        /// M_KYOTEN
        /// </summary>
        internal M_KYOTEN kyotenEntity2;

        /// <summary>
        /// M_CORP_INFO
        /// </summary>
        internal M_CORP_INFO corpEntity;

        public DTOClass()
        {
            this.entryEntity = new T_KEIRYOU_ENTRY();
            this.detailEntity = new T_KEIRYOU_DETAIL[] { new T_KEIRYOU_DETAIL() }; ;
            this.uketsukeEntity = new T_UKETSUKE_SS_ENTRY();
            this.uketsukeDetailEntity = new T_UKETSUKE_SS_DETAIL[] { new T_UKETSUKE_SS_DETAIL() };
            this.sysInfoEntity = new M_SYS_INFO();
            this.contenaEntity = new M_CONTENA_JOUKYOU();
            this.manifestShuruiEntity = new M_MANIFEST_SHURUI();
            this.manifestTehaiEntity = new M_MANIFEST_TEHAI();
            this.kyotenEntity = new M_KYOTEN();
            this.numberDay = new S_NUMBER_DAY();
            this.numberYear = new S_NUMBER_YEAR();
            this.numberReceipt = new S_NUMBER_RECEIPT();
            this.manifestEntrys = new DataTable();
            this.keitaiKbnEntity = new M_KEITAI_KBN();
            this.kyotenEntity1 = new M_KYOTEN();
            this.kyotenEntity2 = new M_KYOTEN(); 
            this.corpEntity = new M_CORP_INFO();
        }

        /// <summary>
        /// Dtoの中身をコピーする
        /// T_KEIRYOU_ENTRYとT_KEIRYOU_DETAILのみデータコピーをする。
        /// それ以外は参照渡し。
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public DTOClass Clone()
        {

            DTOClass returnDto = new DTOClass();
            returnDto.entryEntity = this.KeiryouEntityClone();
            List<T_KEIRYOU_DETAIL> copyList = new List<T_KEIRYOU_DETAIL>();
            if (this.detailEntity != null)
            {
                foreach (var targetEntity in this.detailEntity)
                {
                    copyList.Add(this.ukeireDetailClone(targetEntity));
                }

            }
            returnDto.detailEntity = copyList.ToArray();

            returnDto.sysInfoEntity = this.sysInfoEntity;
            returnDto.contenaEntity = this.contenaEntity;
            returnDto.manifestShuruiEntity = this.manifestShuruiEntity;
            returnDto.manifestTehaiEntity = this.manifestTehaiEntity;
            returnDto.kyotenEntity = this.kyotenEntity;
            returnDto.numberDay = this.numberDay;
            returnDto.numberYear = this.numberYear;
            returnDto.numberReceipt = this.numberReceipt;
            returnDto.manifestEntrys = this.manifestEntrys;
            returnDto.keitaiKbnEntity = this.keitaiKbnEntity;
            returnDto.kyotenEntity1 = this.kyotenEntity1;
            returnDto.kyotenEntity2 = this.kyotenEntity2;
            returnDto.corpEntity = this.corpEntity;
            returnDto.uketsukeEntity = this.uketsukeEntity;
            returnDto.uketsukeDetailEntity = this.uketsukeDetailEntity;

            return returnDto;
        }

        /// <summary>
        /// 自身のT_KEIRYOU_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        private T_KEIRYOU_ENTRY KeiryouEntityClone()
        {
            var returnEntity = new T_KEIRYOU_ENTRY();

            returnEntity.DATE_NUMBER = this.entryEntity.DATE_NUMBER;
            returnEntity.DELETE_FLG = this.entryEntity.DELETE_FLG;
            returnEntity.DENPYOU_BIKOU = this.entryEntity.DENPYOU_BIKOU;
            returnEntity.DENPYOU_DATE = this.entryEntity.DENPYOU_DATE;
            returnEntity.EIGYOU_TANTOUSHA_CD = this.entryEntity.EIGYOU_TANTOUSHA_CD;
            returnEntity.EIGYOU_TANTOUSHA_NAME = this.entryEntity.EIGYOU_TANTOUSHA_NAME;
            returnEntity.GENBA_CD = this.entryEntity.GENBA_CD;
            returnEntity.GENBA_NAME = this.entryEntity.GENBA_NAME;
            returnEntity.GYOUSHA_CD = this.entryEntity.GYOUSHA_CD;
            returnEntity.GYOUSHA_NAME = this.entryEntity.GYOUSHA_NAME;
            returnEntity.KEIRYOU_NUMBER = this.entryEntity.KEIRYOU_NUMBER;
            returnEntity.KEITAI_KBN_CD = this.entryEntity.KEITAI_KBN_CD;
            returnEntity.KYOTEN_CD = this.entryEntity.KYOTEN_CD;
            returnEntity.MANIFEST_SHURUI_CD = this.entryEntity.MANIFEST_SHURUI_CD;
            returnEntity.MANIFEST_TEHAI_CD = this.entryEntity.MANIFEST_TEHAI_CD;
            returnEntity.NIOROSHI_GENBA_CD = this.entryEntity.NIOROSHI_GENBA_CD;
            returnEntity.NIOROSHI_GENBA_NAME = this.entryEntity.NIOROSHI_GENBA_NAME;
            returnEntity.NIOROSHI_GYOUSHA_CD = this.entryEntity.NIOROSHI_GYOUSHA_CD;
            returnEntity.NIOROSHI_GYOUSHA_NAME = this.entryEntity.NIOROSHI_GYOUSHA_NAME;
            returnEntity.NYUURYOKU_TANTOUSHA_CD = this.entryEntity.NYUURYOKU_TANTOUSHA_CD;
            returnEntity.NYUURYOKU_TANTOUSHA_NAME = this.entryEntity.NYUURYOKU_TANTOUSHA_NAME;
            returnEntity.SEARCH_DENPYOU_DATE = this.entryEntity.SEARCH_DENPYOU_DATE;
            returnEntity.SEQ = this.entryEntity.SEQ;
            returnEntity.SHARYOU_CD = this.entryEntity.SHARYOU_CD;
            returnEntity.SHARYOU_NAME = this.entryEntity.SHARYOU_NAME;
            returnEntity.SHASHU_CD = this.entryEntity.SHASHU_CD;
            returnEntity.SHASHU_NAME = this.entryEntity.SHASHU_NAME;
            returnEntity.SYSTEM_ID = this.entryEntity.SYSTEM_ID;
            returnEntity.TORIHIKISAKI_CD = this.entryEntity.TORIHIKISAKI_CD;
            returnEntity.TORIHIKISAKI_NAME = this.entryEntity.TORIHIKISAKI_NAME;
            returnEntity.UNPAN_GYOUSHA_CD = this.entryEntity.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.entryEntity.UNPAN_GYOUSHA_NAME;
            returnEntity.UNTENSHA_CD = this.entryEntity.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.entryEntity.UNTENSHA_NAME;
            returnEntity.YEAR_NUMBER = this.entryEntity.YEAR_NUMBER;
            returnEntity.CREATE_USER = this.entryEntity.CREATE_USER;
            returnEntity.CREATE_DATE = this.entryEntity.CREATE_DATE;
            returnEntity.CREATE_PC = this.entryEntity.CREATE_PC;
            returnEntity.UPDATE_USER = this.entryEntity.UPDATE_USER;
            returnEntity.UPDATE_DATE = this.entryEntity.UPDATE_DATE;
            returnEntity.UPDATE_PC = this.entryEntity.UPDATE_PC;
            returnEntity.DELETE_FLG = this.entryEntity.DELETE_FLG;
            returnEntity.TIME_STAMP = this.entryEntity.TIME_STAMP;
            return returnEntity;
        }

        private T_KEIRYOU_DETAIL ukeireDetailClone(T_KEIRYOU_DETAIL copyTarget)
        {
            var returnEntity = new T_KEIRYOU_DETAIL();

            returnEntity.YOUKI_SUURYOU = copyTarget.YOUKI_SUURYOU;
            returnEntity.YOUKI_JYUURYOU = copyTarget.YOUKI_JYUURYOU;
            returnEntity.YOUKI_CD = copyTarget.YOUKI_CD;
            returnEntity.KEIRYOU_NUMBER = copyTarget.KEIRYOU_NUMBER;
            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.STACK_JYUURYOU = copyTarget.STACK_JYUURYOU;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.NET_JYUURYOU = copyTarget.NET_JYUURYOU;
            returnEntity.MEISAI_BIKOU = copyTarget.MEISAI_BIKOU;
            returnEntity.HINMEI_NAME = copyTarget.HINMEI_NAME;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.EMPTY_JYUURYOU = copyTarget.EMPTY_JYUURYOU;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.DENPYOU_KBN_CD = copyTarget.DENPYOU_KBN_CD;
            returnEntity.CHOUSEI_PERCENT = copyTarget.CHOUSEI_PERCENT;
            returnEntity.CHOUSEI_JYUURYOU = copyTarget.CHOUSEI_JYUURYOU;

            return returnEntity;
        }



        /// <summary>
        /// T_UKETSUKE_SS_ENTRYを複製する
        /// </summary>
        /// <returns></returns>
        public T_KEIRYOU_ENTRY ukeireEntityClone()
        {
            var returnEntity = new T_KEIRYOU_ENTRY();

             //returnEntity.DATE_NUMBER = this.uketsukeEntity.DATE_NUMBER;
            returnEntity.DELETE_FLG = this.uketsukeEntity.DELETE_FLG;
            //returnEntity.DENPYOU_BIKOU = this.uketsukeEntity.DENPYOU_BIKOU;
            //returnEntity.DENPYOU_DATE = this.uketsukeEntity.DENPYOU_DATE;
            returnEntity.EIGYOU_TANTOUSHA_CD = this.uketsukeEntity.EIGYOU_TANTOUSHA_CD;
            returnEntity.EIGYOU_TANTOUSHA_NAME = this.uketsukeEntity.EIGYOU_TANTOUSHA_NAME;
            returnEntity.GENBA_CD = this.uketsukeEntity.GENBA_CD;
            returnEntity.GENBA_NAME = this.uketsukeEntity.GENBA_NAME;
            returnEntity.GYOUSHA_CD = this.uketsukeEntity.GYOUSHA_CD;
            returnEntity.GYOUSHA_NAME = this.uketsukeEntity.GYOUSHA_NAME;
            //returnEntity.KEIRYOU_NUMBER = this.uketsukeEntity.KEIRYOU_NUMBER;
            //returnEntity.KEITAI_KBN_CD = this.uketsukeEntity.KEITAI_KBN_CD;
            returnEntity.KYOTEN_CD = this.uketsukeEntity.KYOTEN_CD;
            returnEntity.MANIFEST_SHURUI_CD = this.uketsukeEntity.MANIFEST_SHURUI_CD;
            returnEntity.MANIFEST_TEHAI_CD = this.uketsukeEntity.MANIFEST_TEHAI_CD;
            returnEntity.NIOROSHI_GENBA_CD = this.uketsukeEntity.NIOROSHI_GENBA_CD;
            returnEntity.NIOROSHI_GENBA_NAME = this.uketsukeEntity.NIOROSHI_GENBA_NAME;
            returnEntity.NIOROSHI_GYOUSHA_CD = this.uketsukeEntity.NIOROSHI_GYOUSHA_CD;
            returnEntity.NIOROSHI_GYOUSHA_NAME = this.uketsukeEntity.NIOROSHI_GYOUSHA_NAME;
            //returnEntity.NYUURYOKU_TANTOUSHA_CD = this.uketsukeEntity.NYUURYOKU_TANTOUSHA_CD;
            //returnEntity.NYUURYOKU_TANTOUSHA_NAME = this.uketsukeEntity.NYUURYOKU_TANTOUSHA_NAME;
            //returnEntity.SEARCH_DENPYOU_DATE = this.uketsukeEntity.SEARCH_DENPYOU_DATE;
            returnEntity.SEQ = this.uketsukeEntity.SEQ;
            returnEntity.SHARYOU_CD = this.uketsukeEntity.SHARYOU_CD;
            returnEntity.SHARYOU_NAME = this.uketsukeEntity.SHARYOU_NAME;
            returnEntity.SHASHU_CD = this.uketsukeEntity.SHASHU_CD;
            returnEntity.SHASHU_NAME = this.uketsukeEntity.SHASHU_NAME;
            returnEntity.SYSTEM_ID = this.uketsukeEntity.SYSTEM_ID;
            //returnEntity.TAIRYUU_BIKOU = this.uketsukeEntity.TAIRYUU_BIKOU;
            returnEntity.TORIHIKISAKI_CD = this.uketsukeEntity.TORIHIKISAKI_CD;
            returnEntity.TORIHIKISAKI_NAME = this.uketsukeEntity.TORIHIKISAKI_NAME;
            returnEntity.UNPAN_GYOUSHA_CD = this.uketsukeEntity.UNPAN_GYOUSHA_CD;
            returnEntity.UNPAN_GYOUSHA_NAME = this.uketsukeEntity.UNPAN_GYOUSHA_NAME;
            returnEntity.UNTENSHA_CD = this.uketsukeEntity.UNTENSHA_CD;
            returnEntity.UNTENSHA_NAME = this.uketsukeEntity.UNTENSHA_NAME;
            returnEntity.KIHON_KEIRYOU = this.uketsukeEntity.GENCHAKU_TIME_CD;

            return returnEntity;
        }

        private T_KEIRYOU_DETAIL uketsukeDetailClone(T_UKETSUKE_SS_DETAIL copyTarget)
        {
            var returnEntity = new T_KEIRYOU_DETAIL();

            returnEntity.SYSTEM_ID = copyTarget.SYSTEM_ID;
            returnEntity.SEQ = copyTarget.SEQ;
            returnEntity.ROW_NO = copyTarget.ROW_NO;
            returnEntity.MEISAI_BIKOU = copyTarget.MEISAI_BIKOU;
            returnEntity.HINMEI_NAME = copyTarget.HINMEI_NAME;
            returnEntity.HINMEI_CD = copyTarget.HINMEI_CD;
            returnEntity.DETAIL_SYSTEM_ID = copyTarget.DETAIL_SYSTEM_ID;
            returnEntity.DENPYOU_KBN_CD = copyTarget.DENPYOU_KBN_CD;


            return returnEntity;
        }


        /// <summary>
        /// Dtoの中身をコピーする
        /// T_UKETSUKE_SS_DETAILのみデータコピーをする。
        /// それ以外は参照渡し。
        /// もし上記以外の値コピーをしたい場合は適宜追加。
        /// </summary>
        /// <returns></returns>
        public T_KEIRYOU_DETAIL[] uketsukeClone(T_UKETSUKE_SS_DETAIL[] uketsukeDetails)
        {
            T_KEIRYOU_DETAIL[] copyList = null;

            if (uketsukeDetails != null)
            {
                copyList = new T_KEIRYOU_DETAIL[uketsukeDetails.Length];

                for (int i = 0; i < copyList.Length; i++)
                {
                    // 受付明細⇒計量明細
                    copyList[i] = this.uketsukeDetailClone(uketsukeDetails[i]);

                }


            }

            return copyList;


        }


        #region Utility
        /// <summary>
        /// 自身のT_KEIRYOU_DETAILのDETAIL_SYSTEM_IDのリストを取得する
        /// </summary>
        /// <returns>T_KEIRYOU_DETAILが一件もない</returns>
        public List<SqlInt64> getDetailSysIds()
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (this.detailEntity == null || this.detailEntity.Length < 1)
            {
                return returnList;
            }

            foreach (T_KEIRYOU_DETAIL dtail in this.detailEntity)
            {
                returnList.Add(dtail.DETAIL_SYSTEM_ID);
            }

            return returnList;
        }
        #endregion
    }
}
