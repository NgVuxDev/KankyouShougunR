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

namespace Shougun.Core.PaperManifest.ManifestHimoduke
{
    public class FirstManifestDTOCls
    {
        /// <summary>
        /// 検索条件  :二次システムID
        /// </summary>
        public SqlInt64 NEXT_SYSTEM_ID { get; set; }
        /// <summary>
        /// 検索条件  :契約の有無
        /// </summary>
        public String KEIYAKU_FLG { get; set; }
        /// <summary>
        /// 検索条件  :マニ種類
        /// </summary>
        public String MANI_TYPE { get; set; }
        /// <summary>
        /// 検索条件  :日付範囲の種類
        /// </summary>
        public String DATETIME_TYPE { get; set; }
        /// <summary>
        /// 検索条件  :日付範囲の開始日
        /// </summary>
        public String START_DATETIME { get; set; }
        /// <summary>
        ///  検索条件  :日付範囲の終了日
        /// </summary>
        public String END_DATETIME { get; set; }
        /// <summary>
        /// 検索条件  :廃棄物種類
        /// </summary>
        public String HAIKI_SHURUI_CD { get; set; }
        /// <summary>
        /// 検索条件  :廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }
        /// <summary>
        /// 検索条件  :報告書分類CD
        /// </summary>
        public String HOUKOKUSHO_BUNRUI_CD { get; set; }
        /// <summary>
        /// 検索条件  :荷姿CD
        /// </summary>
        public String NISUGATA_CD { get; set; }
        /// <summary>
        /// 検索条件  :処分方法CD
        /// </summary>
        public String SBN_HOUHOU_CD { get; set; }
        /// <summary>
        /// 検索条件  :排出事業者CD
        /// </summary>
        public String HST_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件  :排出事業場CDCD
        /// </summary>
        public String HST_GENBA_CD { get; set; }
        /// <summary>
        /// 検索条件  :運搬受託者CD
        /// </summary>
        public String UPN_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件  :処分受託者CD
        /// </summary>
        public String SBN_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件  :処分事業場CD
        /// </summary>
        public String UPN_SAKI_GENBA_CD { get; set; }

        /// <summary>
        /// 検索条件  :最終処分業者CD
        /// </summary>
        public String LAST_SBN_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件  :最終処分場CD
        /// </summary>
        public String LAST_SBN_GENBA_CD { get; set; }

        // 20140519 kayo No.734 機能追加 start
        /// <summary>
        /// 検索条件  :最終処分場所抽出の種類
        /// </summary>
        public String LAST_SBN_GENBA_TYPE { get; set; }
        // 20140519 kayo No.734 機能追加 end
        
        /// <summary>
        /// 検索条件：紐付済み電子マニの管理番号
        /// </summary>
        public List<String> KANRI_ID { get; set; }
        /// <summary>
        /// 検索条件：紐付済み紙マニのDETAIL_SYSTEM_ID
        /// </summary>
        public List<SqlInt64> DETAIL_SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件：紐付け済み電マニのDT_R18_EX.SYSTEM_ID(混廃の場合はDT_R18_MIX.DETAIL_SYSTEM_ID)
        /// </summary>
        public List<SqlInt64> ELEC_SYSTEM_ID { get; set; }

        public string STRING_DETAIL_SYSTEM_ID { get; set; }
        public string STRING_ELEC_SYSTEM_ID { get; set; }

        /// <summary>
        /// チェック用：紐付け済みマニの情報
        /// </summary>
        public List<RelationInfo_DTOCls> relationInfoDtoList { get; set; }

        // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 start
        /// <summary>
        /// 揃え済みの管理番号
        /// </summary>
        public List<String> DSP_KANRI_ID { get; set; }
        /// <summary>
        /// 揃え済みの紙マニのDETAIL_SYSTEM_ID
        /// </summary>
        public List<SqlInt64> DSP_DETAIL_SYSTEM_ID { get; set; }
        // 20140609 kayo 不具合#4694 選択外クリアを押してから、再検索の表示不正 end

        /// <summary>
        /// 紙を検索する場合は1
        /// </summary>
        public int paper { get; set; }
        /// <summary>
        /// 電子を検索する場合は1
        /// </summary>
        public int elec { get; set; }

        /// <summary>
        /// 起動元の二次マニ区分
        /// </summary>
        public string MANI_KBN { get;  set; }
    }

    /// <summary>
    /// 検索条件を保存するクラス
    /// </summary>
    internal class GamenDTOCls
    {
        internal string cntxt_KeiyakuFlg;
        internal string cntxt_ManiType;
        internal string cntxt_DatetimeType;
        // 20140519 kayo No.734 機能追加 start
        internal string cntxt_LastShobunGenbaFlg;
        // 20140519 kayo No.734 機能追加 end

        internal DateTime? cDtPicker_StartDay;
        internal DateTime? cDtPicker_EndDay;

        internal string cantxt_HaikibutuTypeCD;
        internal string ctxt_HaikibutuTypeName;

        internal string cantxt_HaikibutuCD;
        internal string ctxt_HaikibutuName;

        internal string cantxt_HoukokushoTypeCD;
        internal string ctxt_HoukokushoTypeName;

        internal string cantxt_NisugataCD;
        internal string ctxt_NisugataName;

        internal string cantxt_ShobunHouhouCD;
        internal string ctxt_ShobunHouhouName;

        internal string cantxt_HaisyutugyoshaCD;
        internal string ctxt_HaisyutugyoshaName;

        internal string cantxt_HaisyutugenbaCD;
        internal string ctxt_HaisyutugenbaName;

        internal string cantxt_UnpangyoshaCD;
        internal string ctxt_UnpangyoshaName;

        internal string cantxt_ShobungyoshaCD;
        internal string ctxt_ShobungyoshaName;

        internal string cantxt_ShobunGenbaCD;
        internal string ctxt_ShobunGenbaName;

        // 20140519 kayo No.734 機能追加 start
        //internal string cantxt_LastShobunGyoShaCD;
        //internal string ctxt_LastShobunGyoShaName;

        //internal string cantxt_LastShobunGenbaCD;
        //internal string ctxt_LastShobunGenbaName;
        // 20140519 kayo No.734 機能追加 end

        internal GamenDTOCls(UIForm form)
        {
            this.Save(form);
        }

        internal void Save(UIForm form)
        {
            this.cntxt_KeiyakuFlg = form.cntxt_KeiyakuFlg.Text;
            this.cntxt_ManiType = form.cntxt_ManiType.Text;
            this.cntxt_DatetimeType = form.cntxt_DatetimeType.Text;
            // 20140519 kayo No.734 機能追加 start
            this.cntxt_LastShobunGenbaFlg = form.cntxt_LastShobunGenbaFlg.Text;
            // 20140519 kayo No.734 機能追加 end
            if (form.cDtPicker_StartDay.Value == null)
            {
                this.cDtPicker_StartDay = null;
            }
            else
            {
                this.cDtPicker_StartDay = (DateTime)form.cDtPicker_StartDay.Value;
            }
            if (form.cDtPicker_EndDay.Value == null)
            {
                this.cDtPicker_EndDay = null;
            }
            else
            {
                this.cDtPicker_EndDay = (DateTime)form.cDtPicker_EndDay.Value;
            }
            this.cantxt_HaikibutuTypeCD = form.cantxt_HaikibutuTypeCD.Text;
            this.ctxt_HaikibutuTypeName = form.ctxt_HaikibutuTypeName.Text;
            this.cantxt_HaikibutuCD = form.cantxt_HaikibutuCD.Text;
            this.ctxt_HaikibutuName = form.ctxt_HaikibutuName.Text;
            this.cantxt_HoukokushoTypeCD = form.cantxt_HoukokushoTypeCD.Text;
            this.ctxt_HoukokushoTypeName = form.ctxt_HoukokushoTypeName.Text;
            this.cantxt_NisugataCD = form.cantxt_NisugataCD.Text;
            this.ctxt_NisugataName = form.ctxt_NisugataName.Text;
            this.cantxt_ShobunHouhouCD = form.cantxt_ShobunHouhouCD.Text;
            this.ctxt_ShobunHouhouName = form.ctxt_ShobunHouhouName.Text;
            this.cantxt_HaisyutugyoshaCD = form.cantxt_HaisyutugyoshaCD.Text;
            this.ctxt_HaisyutugyoshaName = form.ctxt_HaisyutugyoshaName.Text;
            this.cantxt_HaisyutugenbaCD = form.cantxt_HaisyutugenbaCD.Text;
            this.ctxt_HaisyutugenbaName = form.ctxt_HaisyutugenbaName.Text;
            this.cantxt_UnpangyoshaCD = form.cantxt_UnpangyoshaCD.Text;
            this.ctxt_UnpangyoshaName = form.ctxt_UnpangyoshaName.Text;
            this.cantxt_ShobungyoshaCD = form.cantxt_ShobungyoshaCD.Text;
            this.ctxt_ShobungyoshaName = form.ctxt_ShobungyoshaName.Text;
            this.cantxt_ShobunGenbaCD = form.cantxt_ShobunGenbaCD.Text;
            this.ctxt_ShobunGenbaName = form.ctxt_ShobunGenbaName.Text;
            // 20140519 kayo No.734 機能追加 start
            //this.cantxt_LastShobunGyoShaCD = form.cantxt_LastShobunGyoShaCD.Text;
            //this.ctxt_LastShobunGyoShaName = form.ctxt_LastShobunGyoShaName.Text;
            //this.cantxt_LastShobunGenbaCD = form.cantxt_LastShobunGenbaCD.Text;
            //this.ctxt_LastShobunGenbaName = form.ctxt_LastShobunGenbaName.Text;
            // 20140519 kayo No.734 機能追加 end
        }
        internal void Load(UIForm form)
        {
            form.cntxt_KeiyakuFlg.Text = this.cntxt_KeiyakuFlg;
            form.cntxt_ManiType.Text=this.cntxt_ManiType;
            form.cntxt_DatetimeType.Text = this.cntxt_DatetimeType;
            // 20140519 kayo No.734 機能追加 start
            form.cntxt_LastShobunGenbaFlg.Text = this.cntxt_LastShobunGenbaFlg;
            // 20140519 kayo No.734 機能追加 end

            if (this.cDtPicker_StartDay.HasValue)
            {
                form.cDtPicker_StartDay.Value = this.cDtPicker_StartDay.Value;
            }
            else
            {
                form.cDtPicker_StartDay.Value = null;
            }
            if (this.cDtPicker_EndDay.HasValue)
            {
                form.cDtPicker_EndDay.Value = this.cDtPicker_EndDay.Value;
            }
            else
            {
                form.cDtPicker_EndDay.Value = null;
            }

            form.cantxt_HaikibutuTypeCD.Text = this.cantxt_HaikibutuTypeCD;
            form.ctxt_HaikibutuTypeName.Text = this.ctxt_HaikibutuTypeName;
            form.cantxt_HaikibutuCD.Text = this.cantxt_HaikibutuCD;
            form.ctxt_HaikibutuName.Text = this.ctxt_HaikibutuName;
            form.cantxt_HoukokushoTypeCD.Text = this.cantxt_HoukokushoTypeCD;
            form.ctxt_HoukokushoTypeName.Text = this.ctxt_HoukokushoTypeName;
            form.cantxt_NisugataCD.Text = this.cantxt_NisugataCD;
            form.ctxt_NisugataName.Text = this.ctxt_NisugataName;
            form.cantxt_ShobunHouhouCD.Text = this.cantxt_ShobunHouhouCD;
            form.ctxt_ShobunHouhouName.Text = this.ctxt_ShobunHouhouName;
            form.cantxt_HaisyutugyoshaCD.Text = this.cantxt_HaisyutugyoshaCD;
            form.ctxt_HaisyutugyoshaName.Text = this.ctxt_HaisyutugyoshaName;
            form.cantxt_HaisyutugenbaCD.Text = this.cantxt_HaisyutugenbaCD;
            form.ctxt_HaisyutugenbaName.Text = this.ctxt_HaisyutugenbaName;
            form.cantxt_UnpangyoshaCD.Text = this.cantxt_UnpangyoshaCD;
            form.ctxt_UnpangyoshaName.Text = this.ctxt_UnpangyoshaName;
            form.cantxt_ShobungyoshaCD.Text = this.cantxt_ShobungyoshaCD;
            form.ctxt_ShobungyoshaName.Text = this.ctxt_ShobungyoshaName;
            form.cantxt_ShobunGenbaCD.Text = this.cantxt_ShobunGenbaCD;
            form.ctxt_ShobunGenbaName.Text = this.ctxt_ShobunGenbaName;
            // 20140519 kayo No.734 機能追加 start
            //form.cantxt_LastShobunGyoShaCD.Text = this.cantxt_LastShobunGyoShaCD;
            //form.ctxt_LastShobunGyoShaName.Text = this.ctxt_LastShobunGyoShaName;
            //form.cantxt_LastShobunGenbaCD.Text = this.cantxt_LastShobunGenbaCD;
            //form.ctxt_LastShobunGenbaName.Text = this.ctxt_LastShobunGenbaName;
            // 20140519 kayo No.734 機能追加 end

        }
    }

    /// <summary>
    /// 紐付された一次マニフェスト情報の検索条件DTO
    /// </summary>
    public class HIMODUKE_DTOCls
    {
        /// <summary>
        /// 検索条件 :二次マニフェストNEXT_SYSTEM_ID
        /// </summary>
        public String NEXT_SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件 :二次マニフェストNEXT_HAIKI_KBN_CD
        /// </summary>
        public int NEXT_HAIKI_KBN_CD { get; set; }
    }

    /// <summary>
    /// 存在するチェック検索条件DTO
    /// </summary>
    public class SearchExistDTOCls
    {
        /// <summary>
        /// 検索条件 :一次マニフェストSYSTEM_ID
        /// </summary>
        public String SYSTEM_ID { get; set; }
        /// <summary>
        /// 検索条件 :管理番号
        /// </summary>
        public String KANRI_ID { get; set; }
        /// <summary>
        /// 検索条件 :交付番号
        /// </summary>
        public String MANIFEST_ID { get; set; }
        /// <summary>
        /// 検索条件 :電子廃棄物種類CD 
        /// </summary>
        public String HAIKI_SHURUI_CD { get; set; }
        /// <summary>
        /// EDI加入者番号(電子廃棄物名称テーブルに複数主キー)
        /// </summary>
        public String EDI_MEMBER_ID { get; set; }
        /// <summary>
        /// 電子廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }

    }

    /// <summary>
    /// 紐付用情報DTO
    /// </summary>
    public class RelationInfo_DTOCls
    {
        /// <summary>
        /// 紐付したフラグ(0:未;１：紐付)
        /// </summary>
        public String HimodukeFlg { get; set; }
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


        public SqlInt64 TME_SYSTEM_ID { get; set; }
        public SqlInt32 TME_SEQ { get; set; }
        public byte[] TME_TIME_STAMP { get; set; }

        public SqlInt64 DT_R18_EX_SYSTEM_ID { get; set; }
        public SqlInt32 DT_R18_EX_SEQ { get; set; }
        public byte[] DT_R18_EX_TIME_STAMP { get; set; }

        // 最終処分場チェック用の値
        public string LAST_SBN_GENBA_NAME_AND_ADDRESS { get; set; }
        public string LAST_SBN_END_DATE { get; set; }

        public SqlInt32 LATEST_SEQ { get; set; }

    }


    #region 画面間パラメータ
    /// <summary>
    /// 画面呼び出し時に必要なパラメータ
    /// </summary>
    public class ManiRelrationCallParameter
    {
        /// <summary>マニ区分 1:直行 2:建廃 3:積替 4:電子</summary>
        public string MANI_KBN { get; private set; }

        /// <summary>
        /// 2次の情報
        /// nullの場合新規（未登録）と判断
        /// ※既存の紐付テーブル検索で利用　SEQは最新を探します
        /// </summary>
        public SqlInt64 SECOND_SYSTEM_ID { get; private set; }

        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
       public SqlInt64 SECOND_DETAIL_SYSTEM_ID { get; private set; }
       // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

        /// <summary>
        /// 二次電マニ管理番号
        /// </summary>
        public string SECOND_KANRI_ID { get; set; }

        /// <summary>
        /// 二次排出量
        /// </summary>
        public string NEXT_HAISYUTU_AMT{ get; private set; }


        // 20140519 kayo No.734 機能追加 start
        ///// <summary>
        ///// 前回紐付結果
        ///// 紐付を行って、登録/キャンセル 前であればセットすること。チェックの引継ぎ等行う。
        ///// </summary>
        //public ManiRelrationResult preResult { get; private set; }
        // 20140519 kayo No.734 機能追加 end

        /// <summary>
        /// 新規で呼ぶ場合のコンストラクタ
        /// </summary>
        /// <param name="mani_kbn"></param>
        /// <param name="second_system_id"></param>
        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
        //public ManiRelrationCallParameter(string mani_kbn, Int64? second_system_id, string Next_Haisyutu_Amt, string second_kanri_id)
        public ManiRelrationCallParameter(string mani_kbn, Int64? second_system_id, Int64? second_detail_system_id, string Next_Haisyutu_Amt, string second_kanri_id)
        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
        {
            this.MANI_KBN = mani_kbn;
            this.SECOND_SYSTEM_ID = second_system_id.HasValue ? second_system_id.Value : SqlInt64.Null;
            this.NEXT_HAISYUTU_AMT = Next_Haisyutu_Amt;
            this.SECOND_KANRI_ID = second_kanri_id;

            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
            this.SECOND_DETAIL_SYSTEM_ID = second_detail_system_id.HasValue ? second_detail_system_id.Value : SqlInt64.Null;
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

            // 20140519 kayo No.734 機能追加 start
            //this.preResult = null;
            // 20140519 kayo No.734 機能追加 end
        }

        /// <summary>
        /// 一度でも呼んだことがある場合のコンストラクタ
        /// </summary>
        /// <param name="mrl">結果</param>
        public ManiRelrationCallParameter(ManiRelrationResult mrl, string Next_Haisyutu_Amt)
        {
            // 20140519 kayo No.734 機能追加 start
            //this.MANI_KBN = mrl.callParameter.MANI_KBN;
            //this.SECOND_SYSTEM_ID = mrl.callParameter.SECOND_SYSTEM_ID;
            //this.NEXT_HAISYUTU_AMT = Next_Haisyutu_Amt;
            //this.preResult = mrl;
            // 20140519 kayo No.734 機能追加 end
        }

    }

    /// <summary>
    /// 紐付結果
    /// </summary>
    public class ManiRelrationResult
    {

        /// <summary>紐付された場合OK そうでない場合Cancel</summary>
        public System.Windows.Forms.DialogResult result { get; protected set; }

        // 20140519 kayo No.734 機能追加 start
        ///// <summary>起動時に渡された情報</summary>
        //public ManiRelrationCallParameter callParameter { get; private set; }
        // 20140519 kayo No.734 機能追加 end

        /// <summary>
        /// INSERTすべき紐付情報
        /// ※システムIDとかwhoカラムは空なので登録前にセット必要
        /// </summary>
        public T_MANIFEST_RELATION[] regist_relations { get; protected set; }

        /// <summary>
        /// 紐づけられた明細に対応するEntry
        /// ※タイムスタンプ付　更新日未セット。空更新して楽観排他を行う。
        /// </summary>
        public T_MANIFEST_ENTRY[] paperEntries { get; protected set; }

        /// <summary>
        /// 紐づけられた明細に対応するEntry（電子）
        /// ※タイムスタンプ付　更新日未セット。EXが無い場合は作成する。
        /// </summary>
        public DT_R18_EX[] elecEntriesIns { get; protected set; }

        /// <summary>
        /// 紐づけられた明細に対応するEntry（電子）
        /// ※タイムスタンプ付　更新日未セット。空更新して楽観排他を行う。
        /// </summary>
        public DT_R18_EX[] elecEntriesUpd { get; protected set; }

        /// <summary>
        /// 論理削除すべき紐付情報
        /// ※タイムスタンプ付 削除フラグセット済み。更新日未セット。 楽観排他行う。</summary>
        public T_MANIFEST_RELATION[] delete_relations { get; protected set; }

        /// <summary>
        /// 検索条件保持
        /// </summary>
        internal GamenDTOCls gamen { get; private set; }

        /// <summary>
        /// コンストラクタは外からは呼べ無いようにガードします。
        /// </summary>
        internal ManiRelrationResult(ManiRelrationCallParameter callParam)
        {
            // 20140519 kayo No.734 機能追加 start
            //this.callParameter = callParam;
            // 20140519 kayo No.734 機能追加 end
            this.result = System.Windows.Forms.DialogResult.Cancel;
            this.regist_relations = null;
            this.paperEntries = null;
            this.elecEntriesIns = null;
            this.elecEntriesUpd = null;
            this.delete_relations = null;

        }

        /// <summary>
        /// 選択キャンセルされた場合
        /// </summary>
        internal ManiRelrationResult Cancel()
        {
            this.result = System.Windows.Forms.DialogResult.Cancel;
            // 20140519 kayo No.734 機能追加 start
            //if (this.callParameter != null && this.callParameter.preResult != null)
            //{
            //    //キャンセルの場合
            //    this.regist_relations = this.callParameter.preResult.regist_relations;
            //    this.paperEntries = this.callParameter.preResult.paperEntries;
            //    this.elecEntriesIns = this.callParameter.preResult.elecEntriesIns;
            //    this.elecEntriesUpd = this.callParameter.preResult.elecEntriesUpd;
            //    this.delete_relations = this.callParameter.preResult.delete_relations;
            //}
            // 20140519 kayo No.734 機能追加 end
            return this;
        }
        

        /// <summary>
        /// 紐付選択したとき
        /// </summary>
        /// <param name="regTmr"></param>
        /// <param name="delTme"></param>
        /// <param name="tme"></param>
        /// <param name="r18ex"></param>
        /// <returns></returns>
        internal ManiRelrationResult Selected(T_MANIFEST_RELATION[] regTmr, T_MANIFEST_RELATION[] delTme, T_MANIFEST_ENTRY[] tme, DT_R18_EX[] r18exIns, DT_R18_EX[] r18exUpd,GamenDTOCls gamen)
        {
            this.result = System.Windows.Forms.DialogResult.OK;

            this.regist_relations = regTmr;
            this.paperEntries = tme;
            this.elecEntriesIns = r18exIns;
            this.elecEntriesUpd = r18exUpd;
            this.delete_relations = delTme;
            this.gamen = gamen;
            
            return this;
        }

        /// <summary>
        /// 紐付関係テーブルの登録処理を行う。
        /// トランザクション中に呼ぶこと
        /// </summary>
        public void Regist(Shougun.Core.Common.BusinessCommon.Transaction tran,SqlInt64 systemId)
        {
            r_framework.Utility.LogUtility.DebugMethodStart(tran, systemId);

            //リレーション更新用
            var mrlDao = r_framework.Utility.DaoInitUtility.GetComponent<MRLDaoCls>();

            //Entry更新用(紙)
            var tmeDao = r_framework.Utility.DaoInitUtility.GetComponent<Shougun.Core.Common.BusinessCommon.Dao.CommonEntryDaoCls>();

            //Entry更新用(電子)
            var r18exDao = r_framework.Utility.DaoInitUtility.GetComponent<ElecDaoCls>();

            //whoカラム更新用
            var bind = new r_framework.Logic.DataBinderLogic<SuperEntity>(null as SuperEntity);

            //重複チェック用
            SqlInt16 maniKbn = 0;
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
            SqlInt64 detailSystemId = 0;
            // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

            var chokkouDupList = new List<SqlInt64>();
            var kenpaiDupList = new List<SqlInt64>();
            var tsumikaeDupList = new List<SqlInt64>();
            var elecDupList = new List<SqlInt64>();

            //紙
            if (this.paperEntries != null)
            {
                foreach (var e in this.paperEntries)
                {
                    bind.SetSystemProperty(e, false);
                    tmeDao.UpdateForRelation(e);
                }
            }
            //電子
            if (this.elecEntriesUpd != null)
            {
                foreach (var e in this.elecEntriesUpd)
                {
                    e.DELETE_FLG = true;
                    bind.SetSystemProperty(e, false);
                    r18exDao.UpdateForRelation(e);
                }
            }
            //電子
            if (this.elecEntriesIns != null)
            {
                foreach (var e in this.elecEntriesIns)
                {
                    bind.SetSystemProperty(e, false);
                    r18exDao.Insert(e);
                }
            }

            //紐付
            if (this.delete_relations != null)
            {
                foreach (var e in this.delete_relations)
                {
                    e.DELETE_FLG = true;
                    bind.SetSystemProperty(e, false);
                    mrlDao.Update(e);
                }
            }
            if (this.regist_relations != null)
            {
                maniKbn = this.regist_relations.Count() > 0 ? this.regist_relations[0].NEXT_HAIKI_KBN_CD : 0;

                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                detailSystemId = this.regist_relations.Count() > 0 ? this.regist_relations[0].NEXT_SYSTEM_ID : 0;
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

                foreach (var e in this.regist_relations)
                {
                    // 2016.11.23 chinkeigen マニフェスト紐付 #101095 del start
                    //e.NEXT_SYSTEM_ID = systemId;
                    // 2016.11.23 chinkeigen マニフェスト紐付 #101095 del end

                    bind.SetSystemProperty(e, false);
                    mrlDao.Insert(e);

                    switch (e.FIRST_HAIKI_KBN_CD.ToString())
                    {
                        case "1":
                            chokkouDupList.Add(e.FIRST_SYSTEM_ID);
                            break;

                        case "2":
                            kenpaiDupList.Add(e.FIRST_SYSTEM_ID);
                            break;

                        case "3":
                            tsumikaeDupList.Add(e.FIRST_SYSTEM_ID);
                            break;

                        case "4":
                            elecDupList.Add(e.FIRST_SYSTEM_ID);
                            break;
                    }
                }
            }

            //二重紐付チェック
            var dup = new List<T_MANIFEST_RELATION>();
            if (chokkouDupList != null && chokkouDupList.Count > 0)
            {
                // 産廃(直行)
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                //dup.AddRange(mrlDao.GetDuplications(1, chokkouDupList, systemId, maniKbn));
                dup.AddRange(mrlDao.GetDuplications(1, chokkouDupList, detailSystemId, maniKbn));
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
            }
            if (kenpaiDupList != null && kenpaiDupList.Count > 0)
            {
                // 建廃
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                //dup.AddRange(mrlDao.GetDuplications(2, kenpaiDupList, systemId, maniKbn));
                dup.AddRange(mrlDao.GetDuplications(2, kenpaiDupList, detailSystemId, maniKbn));
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
            }
            if (tsumikaeDupList != null && tsumikaeDupList.Count > 0)
            {
                // 産廃(積替)
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                //dup.AddRange(mrlDao.GetDuplications(3, tsumikaeDupList, systemId, maniKbn));
                dup.AddRange(mrlDao.GetDuplications(3, tsumikaeDupList, detailSystemId, maniKbn));
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
            }
            if (elecDupList != null && elecDupList.Count > 0)
            {
                // 電子
                //dup.AddRange(mrlDao.GetDuplications(4, elecDupList, systemId, maniKbn));
                dup.AddRange(mrlDao.GetDuplications(4, elecDupList, detailSystemId, maniKbn));
                //dup.AddRange(mrlDao.GetDuplications(3, tsumikaeDupList, systemId, maniKbn));
            }

            //var dup = mrlDao.GetDuplications(dupList, SqlInt64.Null); //デバッグ用  既存紐付がエラーとして表示されます
            if (dup != null && dup.Count > 0)
            {
                //重複あり

                //交付番号を取得
                var lKoufu = new List<string>();

                foreach (var e in dup)
                {
                    if (e.FIRST_HAIKI_KBN_CD == 4)
                    {
                        //電子の場合

                        //システムIDをマッチ
                        var result = this.elecEntriesIns.Where(R18EX => (R18EX.SYSTEM_ID == e.FIRST_SYSTEM_ID).Value);
                        if (result.Count() > 0)
                        {
                            var id = result.First().MANIFEST_ID;

                            if (!lKoufu.Contains(id))
                            {
                                lKoufu.Add(id);
                            }
                        }
                        else
                        {

                            result = this.elecEntriesUpd.Where(R18EX => (R18EX.SYSTEM_ID == e.FIRST_SYSTEM_ID).Value);
                            var id = result.First().MANIFEST_ID;
                            if (!lKoufu.Contains(id))
                            {
                                lKoufu.Add(id);
                            }
                        }

                    }
                    else
                    {
                        //紙の場合
                        var sysID = tmeDao.GetSystemIdByDetailSystemId(e.FIRST_SYSTEM_ID); //DETAILからシステムIDを取得

                        var result = this.paperEntries.Where(TME => (TME.SYSTEM_ID == sysID).Value); //取得したシステムIDから交付番号を探す
                        if (result.Count() > 0)
                        {
                            var id = result.First().MANIFEST_ID;

                            if (!lKoufu.Contains(id))
                            {
                                lKoufu.Add(id);
                            }
                        }
                    }

                }

                throw RelationDupulicateException.Create(string.Join(",", lKoufu));
            } //重複チェック終わり

            r_framework.Utility.LogUtility.DebugMethodEnd();
        }

        #region 一次電マニ更新処理
        /// <summary>
        /// 一次電マニ更新処理
        /// </summary>
        /// <param name="mfTocList"></param>
        /// <param name="dtR18List"></param>
        /// <param name="dtR19List"></param>
        /// <param name="dtR02List"></param>
        /// <param name="dtR04List"></param>
        /// <param name="dtR05List"></param>
        /// <param name="dtR08List"></param>
        /// <param name="dtR13List"></param>
        /// <param name="dtR18ExList"></param>
        /// <param name="dtR19ExList"></param>
        /// <param name="dtR04ExList"></param>
        /// <param name="dtR08ExList"></param>
        /// <param name="oldDtR13ExList"></param>
        /// <param name="newDtR13ExList"></param>
        internal void UpdateFirstElecMani(List<DT_MF_TOC> mfTocList, List<DT_R18> dtR18List, List<DT_R19[]> dtR19List,
                                            List<DT_R02[]> dtR02List, List<DT_R04[]> dtR04List, List<DT_R05[]> dtR05List,
                                            List<DT_R08[]> dtR08List, List<DT_R13[]> dtR13List, List<DT_R18_EX> dtR18ExList,
                                            List<DT_R19_EX[]> dtR19ExList, List<DT_R04_EX[]> dtR04ExList, List<DT_R08_EX[]> dtR08ExList, 
                                            List<DT_R13_EX[]> oldDtR13ExList, List<DT_R13_EX[]> newDtR13ExList)
        {
            #region Dao生成
            var mfTocDao = DaoInitUtility.GetComponent<CommonDT_MF_TOCDaoCls>();
            var r18Dao = DaoInitUtility.GetComponent<CommonDT_R18DaoCls>();
            var r19Dao = DaoInitUtility.GetComponent<CommonDT_R19DaoCls>();
            var r02Dao = DaoInitUtility.GetComponent<CommonDT_R02DaoCls>();
            var r04Dao = DaoInitUtility.GetComponent<CommonDT_R04DaoCls>();
            var r05Dao = DaoInitUtility.GetComponent<CommonDT_R05DaoCls>();
            var r06Dao = DaoInitUtility.GetComponent<CommonDT_R06DaoCls>();
            var r08Dao = DaoInitUtility.GetComponent<CommonDT_R08DaoCls>();
            var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
            var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
            var r19ExDao = DaoInitUtility.GetComponent<CommonDT_R19_EXDaoCls>();
            var r04ExDao = DaoInitUtility.GetComponent<CommonDT_R04_EXDaoCls>();
            var r08ExDao = DaoInitUtility.GetComponent<CommonDT_R08_EXDaoCls>();
            var r13ExDao = DaoInitUtility.GetComponent<CommonDT_R13_EXDaoCls>();
            #endregion

            // データ追加、更新
            #region XX_EX以外の更新
            foreach (var mfToc in mfTocList)
            {
                mfTocDao.Update(mfToc);
            }

            foreach (var tempR18 in dtR18List)
            {
                r18Dao.Insert(tempR18);
            }

            foreach(var tempR19s in dtR19List)
            {
                foreach(var tempR19 in tempR19s)
                {
                    r19Dao.Insert(tempR19);
                }
            }

            foreach (var tempR02s in dtR02List)
            {
                foreach (var tempR02 in tempR02s)
                {
                    r02Dao.Insert(tempR02);
                }
            }

            foreach (var tempR04s in dtR04List)
            {
                foreach (var tempR04 in tempR04s)
                {
                    r04Dao.Insert(tempR04);
                }
            }

            foreach (var tempR05s in dtR05List)
            {
                foreach (var tempR05 in tempR05s)
                {
                    r05Dao.Insert(tempR05);
                }
            }

            foreach (var tempR08s in dtR08List)
            {
                foreach (var tempR08 in tempR08s)
                {
                    r08Dao.Insert(tempR08);
                }
            }

            foreach (var tempR13s in dtR13List)
            {
                foreach (var tempR13 in tempR13s)
                {
                    r13Dao.Insert(tempR13);
                }
            }
            #endregion

            #region XX_EXの更新
            foreach(var tempR18Ex in dtR18ExList)
            {
                if (tempR18Ex != null)
                {
                    tempR18Ex.DELETE_FLG = true;
                    r18ExDao.Update(tempR18Ex);
                    tempR18Ex.SEQ = tempR18Ex.SEQ + 1;
                    tempR18Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR18Ex.UPDATE_DATE = DateTime.Now;
                    tempR18Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR18Ex.DELETE_FLG = false;
                    r18ExDao.Insert(tempR18Ex);
                }
            }

            foreach (var tempR19Exs in dtR19ExList)
            {
                foreach (var tempR19Ex in tempR19Exs)
                {
                    tempR19Ex.DELETE_FLG = true;
                    r19ExDao.Update(tempR19Ex);
                    tempR19Ex.SEQ = tempR19Ex.SEQ + 1;
                    tempR19Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR19Ex.UPDATE_DATE = DateTime.Now;
                    tempR19Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR19Ex.DELETE_FLG = false;
                    r19ExDao.Insert(tempR19Ex);
                }
            }

            foreach (var tempR04Exs in dtR04ExList)
            {
                foreach (var tempR04Ex in tempR04Exs)
                {
                    tempR04Ex.DELETE_FLG = true;
                    r04ExDao.Update(tempR04Ex);
                    tempR04Ex.SEQ = tempR04Ex.SEQ + 1;
                    tempR04Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR04Ex.UPDATE_DATE = DateTime.Now;
                    tempR04Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR04Ex.DELETE_FLG = false;
                    r04ExDao.Insert(tempR04Ex);
                }
            }

            foreach (var tempR08Exs in dtR08ExList)
            {
                foreach (var tempR08Ex in tempR08Exs)
                {
                    tempR08Ex.DELETE_FLG = true;
                    r08ExDao.Update(tempR08Ex);
                    tempR08Ex.SEQ = tempR08Ex.SEQ + 1;
                    tempR08Ex.UPDATE_USER = SystemProperty.UserName;
                    tempR08Ex.UPDATE_DATE = DateTime.Now;
                    tempR08Ex.UPDATE_PC = SystemInformation.ComputerName;
                    tempR08Ex.DELETE_FLG = false;
                    r08ExDao.Insert(tempR08Ex);
                }
            }

            foreach (var oldTempR13Exs in oldDtR13ExList)
            {
                foreach (var oldTempR13Ex in oldTempR13Exs)
                {
                    oldTempR13Ex.DELETE_FLG = true;
                    r13ExDao.Update(oldTempR13Ex);
                }
            }

            foreach (var newTempR13Exs in newDtR13ExList)
            {
                foreach (var newTempR13Ex in newTempR13Exs)
                {
                    r13ExDao.Insert(newTempR13Ex);
                }
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 一度でも紐付されたかどうか
        /// </summary>
        /// <returns></returns>
        public bool IsSelected()
        {
            //どれか一つでもデータがあったら選択済み
            if (this.delete_relations != null && this.delete_relations.Length > 0)
            {
                return true;
            }
            if (this.regist_relations != null && this.regist_relations.Length > 0)
            {
                return true;
            }
            if (this.paperEntries != null && this.paperEntries.Length > 0)
            {
                return true;
            }
            if (this.elecEntriesIns != null && this.elecEntriesIns.Length > 0)
            {
                return true;
            }
            if (this.elecEntriesUpd != null && this.elecEntriesUpd.Length > 0)
            {
                return true;
            }

            //何もなければ最初から閉じるしか押されていない
            return false;
        }

    }

    /// <summary>
    /// 紐付重複発生時の例外(登録時にこれをキャッチしてメッセージを出すこと)
    /// </summary>
    public class RelationDupulicateException :Exception
    {
        public string KOUFU_NO { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private RelationDupulicateException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// 例外を生成します
        /// </summary>
        /// <param name="koufuNo"></param>
        /// <returns></returns>
        internal static RelationDupulicateException Create(string koufuNo)
        {
            string msg = string.Format(Shougun.Core.Message.MessageUtility.GetMessageString("E156"), koufuNo);
            var ex = new RelationDupulicateException(msg);
            ex.KOUFU_NO = koufuNo;
            return ex;
        }
        
    }

    #endregion

}
