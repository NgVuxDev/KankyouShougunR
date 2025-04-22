using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.PaperManifest.ManifestShukeihyo
{
    /// <summary>
    /// マニフェスト集計表パターン登録ロジック
    /// </summary>
    internal class ChouhyouPatternTourokuPopupLogic
    {
        private MessageBoxShowLogic MsgBox;
        private ChouhyouPatternTourokuPopupForm form;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal ChouhyouPatternTourokuPopupLogic(ChouhyouPatternTourokuPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.MsgBox = new MessageBoxShowLogic();
            this.form = targetForm;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データベースに対して更新を行います
        /// </summary>
        /// <param name="dto">登録するエンティティ</param>
        /// <returns>登録結果</returns>
        internal bool Regist(WINDOW_TYPE windowType, PatternDto dto)
        {
            var ret = false;
            try
            {
                LogUtility.DebugMethodStart(windowType, dto);
                this.form.isRegistErr = false;
                using (var tran = new Transaction())
                {
                    var mListPatternDao = DaoInitUtility.GetComponent<IM_LIST_PATTERNDao>();
                    var mListPatternColumnDao = DaoInitUtility.GetComponent<IM_LIST_PATTERN_COLUMNDao>();
                    var shori = String.Empty;

                    switch (windowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            {
                                // 新規
                                new DataBinderLogic<M_LIST_PATTERN>(dto.Pattern).SetSystemProperty(dto.Pattern, false);
                                dto.Pattern.SYSTEM_ID = this.GetSystemId();
                                dto.Pattern.SEQ = 1;
                                dto.Pattern.DELETE_FLG = false;

                                mListPatternDao.Insert(dto.Pattern);
                                shori = "登録";

                                break;
                            }
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            {
                                // 更新
                                dto.Pattern.DELETE_FLG = true;
                                mListPatternDao.Update(dto.Pattern);

                                var createUser = dto.Pattern.CREATE_USER;
                                var createDate = dto.Pattern.CREATE_DATE.Value;
                                var createPc = dto.Pattern.CREATE_PC;

                                new DataBinderLogic<M_LIST_PATTERN>(dto.Pattern).SetSystemProperty(dto.Pattern, false);
                                dto.Pattern.SEQ = dto.Pattern.SEQ.Value + 1;
                                dto.Pattern.CREATE_USER = createUser;
                                dto.Pattern.CREATE_DATE = createDate;
                                dto.Pattern.CREATE_PC = createPc;
                                dto.Pattern.DELETE_FLG = false;

                                mListPatternDao.Insert(dto.Pattern);
                                shori = "登録";

                                break;
                            }
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            {
                                // 削除
                                dto.Pattern.DELETE_FLG = true;
                                mListPatternDao.Update(dto.Pattern);

                                var createUser = dto.Pattern.CREATE_USER;
                                var createDate = dto.Pattern.CREATE_DATE.Value;
                                var createPc = dto.Pattern.CREATE_PC;

                                new DataBinderLogic<M_LIST_PATTERN>(dto.Pattern).SetSystemProperty(dto.Pattern, false);
                                dto.Pattern.SEQ = dto.Pattern.SEQ.Value + 1;
                                dto.Pattern.CREATE_USER = createUser;
                                dto.Pattern.CREATE_DATE = createDate;
                                dto.Pattern.CREATE_PC = createPc;
                                dto.Pattern.DELETE_FLG = true;

                                mListPatternDao.Insert(dto.Pattern);
                                shori = "削除";

                                break;
                            }
                    }

                    foreach (var patternColumn in dto.PatternColumnList)
                    {
                        patternColumn.SYSTEM_ID = dto.Pattern.SYSTEM_ID;
                        patternColumn.SEQ = dto.Pattern.SEQ;
                        patternColumn.DETAIL_SYSTEM_ID = this.GetSystemId();

                        mListPatternColumnDao.Insert(patternColumn);
                    }

                    tran.Commit();
                    ret = true;

                    new MessageBoxShowLogic().MessageBoxShow("I001", shori);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                this.form.isRegistErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 一覧表項目選択リストを取得します
        /// </summary>
        /// <param name="windowId">画面ID</param>
        /// <returns>一覧表項目選択リスト</returns>
        internal List<S_LIST_COLUMN_SELECT> GetShuukeiKoumokuList(WINDOW_ID windowId)
        {
            var ret = new List<S_LIST_COLUMN_SELECT>();
            try
            {
                LogUtility.DebugMethodStart();

                var dao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECTDao>();
                ret = dao.GetSListColumnSelectList(new S_LIST_COLUMN_SELECT() { WINDOW_ID = (int)windowId });

            }
            catch (Exception ex)
            {
                LogUtility.Error("GetShuukeiKoumokuList", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 汎用帳票用のSYSTEM_IDを取得します
        /// </summary>
        /// <returns>SYSTEM_ID</returns>
        private SqlInt64 GetSystemId()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var dao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            var entity = dao.GetNumberSystemData(new S_NUMBER_SYSTEM() { DENSHU_KBN_CD = (int)DENSHU_KBN.HANYOU_CHOUHYOU });
            ret = dao.GetMaxPlusKey(new S_NUMBER_SYSTEM() { DENSHU_KBN_CD = (int)DENSHU_KBN.HANYOU_CHOUHYOU });

            if (null == entity || 1 > entity.CURRENT_NUMBER)
            {
                entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (int)DENSHU_KBN.HANYOU_CHOUHYOU;
                entity.CURRENT_NUMBER = ret;
                entity.DELETE_FLG = false;
                new DataBinderLogic<S_NUMBER_SYSTEM>(entity).SetSystemProperty(entity, false);

                dao.Insert(entity);
            }
            else
            {
                entity.CURRENT_NUMBER = ret;

                dao.Update(entity);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
    }
}
