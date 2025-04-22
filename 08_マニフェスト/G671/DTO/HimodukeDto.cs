using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using r_framework.Dto;
using System.Windows.Forms;
namespace Shougun.Core.PaperManifest.ManifestImport
{
    ///
    /// 紐付用情報DTO
    /// </summary>
    public class RelationInfo_DTOCls
    {
        /// <summary>
        /// 二次マニ区分 1:直行 2:建廃 3:積替 4:電子
        /// </summary>
        public string SECOND_MANI_KBN { get; set; }

        /// <summary>
        /// 二次情報
        /// nullの場合新規（未登録）と判断
        /// ※既存の紐付テーブル検索で利用　SEQは最新を探します
        /// </summary>
        public SqlInt64 SECOND_SYSTEM_ID { get; set; }

        /// <summary>
        /// 二次情報
        /// </summary>
        public SqlInt64 SECOND_DETAIL_SYSTEM_ID { get; set; }

        /// <summary>
        /// 二次電マニ管理番号
        /// </summary>
        public string SECOND_KANRI_ID { get; set; }

        /// <summary>
        /// 紐付エラーフラグ(true:紐付エラー;false：紐付有効)
        /// </summary>
        public bool HimodukeErrorFlg { get; set; }
        /// <summary>
        ///  情報：一次マニフェストSYSTEM_ID(紙場合は有効する)
        /// </summary>
        public String FIRST_SYSTEM_ID { get; set; }
        /// <summary>
        ///  情報：電子マニ管理番号(電子場合は有効する)
        /// </summary>
        public String KANRI_ID { get; set; }
        /// <summary>
        ///  情報： 交付番号
        /// </summary>
        public String MANIFEST_ID { get; set; }
        /// <summary>
        /// 情報：マニ種類(電子場合４を固定；紙場合１，２，３)
        /// </summary>
        public String MANIFEST_TYPE { get; set; }
        /// <summary>
        /// 情報：廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }
        /// <summary>
        /// 情報：換算数量(電子場合は有効する)
        /// </summary>
        public String KANSAN_SUU { get; set; }

        /// <summary>
        /// 情報：紐付のSEQ
        /// </summary>
        public SqlInt32 RELATION_SEQ { get; set; }

        public SqlInt64 TME_SYSTEM_ID { get; set; }
        public SqlInt32 TME_SEQ { get; set; }
        public byte[] TME_TIME_STAMP { get; set; }

        public SqlInt64 DT_R18_EX_SYSTEM_ID { get; set; }
        public SqlInt32 DT_R18_EX_SEQ { get; set; }
        public byte[] DT_R18_EX_TIME_STAMP { get; set; }

        /// <summary>
        /// 自身のT_MANIFEST_RELATIONを複製する
        /// </summary>
        /// <returns></returns>
        public T_MANIFEST_RELATION ManifestRelationClone(T_MANIFEST_RELATION entity)
        {
            var returnEntity = new T_MANIFEST_RELATION();

            returnEntity.NEXT_SYSTEM_ID = entity.NEXT_SYSTEM_ID;
            returnEntity.SEQ = entity.SEQ;
            returnEntity.REC_SEQ = entity.REC_SEQ;
            returnEntity.NEXT_HAIKI_KBN_CD = entity.NEXT_HAIKI_KBN_CD;
            returnEntity.FIRST_SYSTEM_ID = entity.FIRST_SYSTEM_ID;
            returnEntity.FIRST_HAIKI_KBN_CD = entity.FIRST_HAIKI_KBN_CD;
            returnEntity.CREATE_USER = entity.CREATE_USER;
            returnEntity.CREATE_DATE = entity.CREATE_DATE;
            returnEntity.CREATE_PC = entity.CREATE_PC;
            returnEntity.UPDATE_USER = entity.UPDATE_USER;
            returnEntity.UPDATE_DATE = entity.UPDATE_DATE;
            returnEntity.UPDATE_PC = entity.UPDATE_PC;
            returnEntity.DELETE_FLG = entity.DELETE_FLG;
            returnEntity.TIME_STAMP = entity.TIME_STAMP;

            return returnEntity;
        }
    }
}
