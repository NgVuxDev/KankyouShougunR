// $Id: DBAccessor.cs 56232 2015-07-21 06:20:31Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Master.DenManiKansanHoshu
{
    /// <summary>
    /// 電マニ換算値入力Accessor
    /// </summary>
    internal class DBAccessor
    {
        #region - Field -
        /// <summary>電マニ換算値入力用DAO</summary>
        private DAOClass dao;
        /// <summary>電マニ換算値DAO</summary>
        private IM_DENSHI_MANIFEST_KANSANDao denManiKansanDao;
        /// <summary>電子事業者DAO</summary>
        private IM_DENSHI_JIGYOUSHADao jigyoushaDao;
        /// <summary>電子廃棄物種類DAO</summary>
        private IM_DENSHI_HAIKI_SHURUIDao denshiShuruiDao;
        /// <summary>細分類マスタDAO</summary>
        private IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao saibunruiDao;
        /// <summary>単位マスタDAO</summary>
        private IM_UNITDao unitDao;
        /// <summary>一覧スキーマ情報</summary>
        private DataTable ichiranTableSchema;

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.denManiKansanDao = DaoInitUtility.GetComponent<IM_DENSHI_MANIFEST_KANSANDao>();
            this.jigyoushaDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.denshiShuruiDao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
            this.saibunruiDao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
        }

        #endregion - Constructor -

        #region - Utility -
        /// <summary>
        /// 電マニ換算値入力画面用の一覧データを取得
        /// </summary>
        /// <param name="dto">検索条件DTO</param>
        /// <returns name="DataTable">一覧表示用DataTable 該当無しの場合はnullを返却</returns>
        internal DataTable getIchiranData(findConditionDTO dto)
        {
            // 検索条件に合致する電マニ換算値マスタデータ取得
            var retTable = this.dao.getIchiranData(dto);

            // 制約を解除
            foreach(DataColumn col in retTable.Columns)
            {
                col.AllowDBNull = true;
                col.ReadOnly = false;
            }

            // スキーマ情報を保存
            this.ichiranTableSchema = retTable.Clone();

            if(retTable.Rows.Count <= 0)
            {
                // 該当無しの場合はnullを返却
                retTable = null;
            }

            return retTable;
        }

        /// <summary>
        /// 細分類読込用の一覧データを取得
        /// </summary>
        /// <param name="dto">検索条件DTO</param>
        /// <returns name="DataTable">一覧表示用DataTable 該当無しの場合はnullを返却</returns>
        internal DataTable getSaibunruiLoadData(findConditionDTO dto)
        {
            // スキーマ情報が存在しなかった場合
            if(this.ichiranTableSchema == null)
            {
                // 電マニ換算値マスタデータ取得
                dto.SHOW_CONDITION_DELETED = true;
                var table = this.dao.getIchiranData(dto);

                // 制約を解除
                foreach(DataColumn col in table.Columns)
                {
                    col.AllowDBNull = true;
                    col.ReadOnly = false;
                }

                // スキーマ情報を保存
                this.ichiranTableSchema = table.Clone();
            }

            // 細分類マスタデータ取得
            var findEntity = new M_DENSHI_HAIKI_SHURUI_SAIBUNRUI();
            findEntity.EDI_MEMBER_ID = dto.entity.EDI_MEMBER_ID;
            findEntity.HAIKI_SHURUI_CD = dto.entity.HAIKI_SHURUI_CD;
            var entitys = this.saibunruiDao.GetAllValidData(findEntity);

            // 一覧表示用DataTableを生成
            var retTable = this.ichiranTableSchema.Clone();
            if(entitys.Length > 0)
            {
                foreach(var entity in entitys)
                {
                    var row = retTable.NewRow();
                    row["DELETE_FLG"] = entity.DELETE_FLG.Value;
                    row["HAIKI_SHURUI_SAIBUNRUI_CD"] = entity.HAIKI_SHURUI_SAIBUNRUI_CD;
                    row["HAIKI_SHURUI_NAME"] = entity.HAIKI_SHURUI_NAME;
                    row["KANSANSHIKI"] = '×';
                    retTable.Rows.Add(row);
                }
            }
            else
            {
                // 細分類マスタに該当情報無しの場合はnullを返却
                retTable = null;
            }

            return retTable;
        }

        /// <summary>
        /// IDに対応した電子事業者名を返却
        /// </summary>
        /// <param name="ediID">加入者番号</param>
        /// <returns name="string">電子事業者名</returns>
        /// <remarks>取得できなかった場合はブランクを返却</remarks>
        internal string getJigyoushaName(string ediID)
        {
            var name = "";

            if(!string.IsNullOrEmpty(ediID))
            {
                // IDを基に電子事業者マスタを検索
                var findEntity = new M_DENSHI_JIGYOUSHA();
                findEntity.EDI_MEMBER_ID = ediID;
                var entitys = this.jigyoushaDao.GetAllValidData(findEntity);
                if(entitys.Length > 0)
                {
                    // 返却する電子事業者名をセット(キー検索のため検索結果は唯一)
                    name = entitys[0].JIGYOUSHA_NAME;
                }
            }

            return name;
        }

        /// <summary>
        /// CDに対応した電子廃棄物種類名を返却
        /// </summary>
        /// <param name="shuruiCD">電子廃棄物種類CD</param>
        /// <returns name="string">電子廃棄物種類名</param>
        /// <remarks>取得できなかった場合はブランクを返却</remarks>
        internal string getDenshiShuruiName(string cd)
        {
            var name = "";

            if(!string.IsNullOrEmpty(cd))
            {
                // CDを基に電子廃棄物種類マスタを検索
                var findEntity = new M_DENSHI_HAIKI_SHURUI();
                findEntity.HAIKI_SHURUI_CD = cd;
                var entitys = this.denshiShuruiDao.GetAllValidData(findEntity);
                if(entitys.Length > 0)
                {
                    // 返却する電子廃棄物種類名をセット(キー検索のため検索結果は唯一)
                    name = entitys[0].HAIKI_SHURUI_NAME;
                }
            }

            return name;
        }

        /// <summary>
        /// CDに対応した細分類名を返却
        /// </summary>
        /// <param name="dto">検索条件DTO</param>
        /// <returns name="string">電子廃棄物細分類名</returns>
        /// <remarks>取得できなかった場合はブランクを返却</remarks>
        internal string getSaibunruiName(findConditionDTO dto)
        {
            var name = "";

            // キーとなる情報が無い場合は処理無し
            if((!string.IsNullOrEmpty(dto.entity.EDI_MEMBER_ID)) &&
               (!string.IsNullOrEmpty(dto.entity.HAIKI_SHURUI_CD)) &&
               (!string.IsNullOrEmpty(dto.entity.HAIKI_SHURUI_SAIBUNRUI_CD)))
            {
                // 細分類マスタ検索
                var findEntity = new M_DENSHI_HAIKI_SHURUI_SAIBUNRUI();
                findEntity.EDI_MEMBER_ID = dto.entity.EDI_MEMBER_ID;
                findEntity.HAIKI_SHURUI_CD = dto.entity.HAIKI_SHURUI_CD;
                findEntity.HAIKI_SHURUI_SAIBUNRUI_CD = dto.entity.HAIKI_SHURUI_SAIBUNRUI_CD;
                var entitys = this.saibunruiDao.GetAllValidData(findEntity);
                if(entitys.Length > 0)
                {
                    // 該当する情報があればそれを返却
                    name = entitys[0].HAIKI_SHURUI_NAME;
                }
            }

            return name;
        }

        /// <summary>
        /// CDに対応した単位名を返却
        /// </summary>
        /// <param name="cd">単位CD</param>
        /// <returns name="string">単位名</returns>
        /// <remarks>取得できなかった場合はブランクを返却</remarks>
        internal string getUnitName(string cd)
        {
            var name = "";

            if(!string.IsNullOrEmpty(cd))
            {
                // CDを基に単位マスタを検索
                var findEntity = new M_UNIT();
                findEntity.UNIT_CD = short.Parse(cd);
                var entitys = this.unitDao.GetAllValidData(findEntity);
                if(entitys.Length > 0)
                {
                    // 返却する単位名をセット(キー検索のため検索結果は唯一)
                    name = entitys[0].UNIT_NAME_RYAKU;
                }
            }

            return name;
        }

        /// <summary>
        /// 渡されたEntityListの新規登録・更新/削除登録
        /// </summary>
        /// <param name="entityList">対象のEntityList</param>
        /// <param name="delete">TRUE:削除登録, FALSE:新規登録・更新</param>
        [Transaction]
        internal void registEntity(List<M_DENSHI_MANIFEST_KANSAN> entityList, bool delete)
        {
            // トランザクション開始
            using(var tran = new Transaction())
            {
                foreach(var regEntity in entityList)
                {
                    // 該当のEntityが既に存在されているか検索
                    var entity = this.denManiKansanDao.GetDataByCd(regEntity);

                    if(delete)
                    {
                        // 削除登録

                        if(entity != null)
                        {
                            // 削除時は削除フラグのみ書き換え更新
                            entity.DELETE_FLG = true;
                            this.denManiKansanDao.Update(entity);
                        }
                    }
                    else
                    {
                        // 新規登録・更新

                        if(entity == null)
                        {
                            // 該当Entityが存在しなければ、渡されたものをそのまま新規登録
                            this.denManiKansanDao.Insert(regEntity);
                        }
                        else
                        {
                            // 該当Entityが存在した場合、渡されたものを更新データとして更新
                            entity.KANSANSHIKI = regEntity.KANSANSHIKI;
                            entity.KANSANCHI = regEntity.KANSANCHI;
                            entity.MANIFEST_KANSAN_BIKOU = regEntity.MANIFEST_KANSAN_BIKOU;
                            entity.UPDATE_USER = regEntity.UPDATE_USER;
                            entity.UPDATE_DATE = regEntity.UPDATE_DATE;
                            entity.UPDATE_PC = regEntity.UPDATE_PC;
                            entity.DELETE_FLG = false;
                            this.denManiKansanDao.Update(entity);
                        }
                    }
                }

                // コミット
                tran.Commit();
            }
        }
        
        #endregion - Utility -

        #region - PrivateUtility -

        #endregion - PrivateUtility -
    }
}
