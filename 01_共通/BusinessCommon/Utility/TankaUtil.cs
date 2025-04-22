using System;
using System.Collections.Generic;
using System.Linq;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dto;

namespace Shougun.Core.Common.BusinessCommon
{
    /// <summary>
    /// 単価取得クラス
    /// </summary>
    public class TankaUtil
    {
        #region 個別品名単価検索

        /// <summary>
        /// 個別品名単価を取得
        /// </summary>
        /// <param name="dto">KobetsuHinmeiTankaParam</param>
        /// <returns>M_KOBETSU_HINMEI_TANKA</returns>
        public static M_KOBETSU_HINMEI_TANKA GetKobetsuhinmeiTanka(KobetsuHinmeiTankaParam dto)
        {
            LogUtility.DebugMethodStart(dto);
            #region "CongBinh 20150724 リビジョン55648の対応"
            // M_KOBETSU_HINMEI_TANKA ret;           
            M_KOBETSU_HINMEI_TANKA ret = null;
            List<M_KOBETSU_HINMEI_TANKA> ents = null;

            foreach (var rule in KOBETSU_HINMEI_TANKA_RULE.RULES())
            {
                //if (GetKobetsuhinmeiTanka(dto, rule, out ret))
                if (GetKobetsuhinmeiTanka(dto, rule, ref ret, ref ents))
            #endregion
                {
                    //データあり
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }

            //データ無
            LogUtility.DebugMethodEnd(null);
            return null;
        }

        /// <summary>
        /// 単価決定ルール構造体
        /// </summary>
        private struct KOBETSU_HINMEI_TANKA_RULE
        {
            public bool useHINMEI_CD;
            public bool useUNIT_CD;
            public bool useNIOROSHI_GENBA_CD;
            public bool useNIOROSHI_GYOUSHA_CD;
            public bool useUNPAN_GYOUSHA_CD;
            public bool useGENBA_CD;
            public bool useGYOUSHA_CD;
            public bool useTORIHIKISAKI_CD;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="useHINMEI_CD">品名CDを検索条件に利用するか</param>
            /// <param name="useUNIT_CD">単位を検索条件に利用するか</param>
            /// <param name="useNIOROSHI_GENBA_CD">荷卸現場CDを検索条件に利用するか</param>
            /// <param name="useNIOROSHI_GYOUSHA_CD">荷卸業者CDを検索条件に利用するか</param>
            /// <param name="useUNPAN_GYOUSHA_CD">運搬業者CDを検索条件に利用するか</param>
            /// <param name="useGENBA_CD">現場CDを検索条件に利用するか</param>
            /// <param name="useGYOUSHA_CD">業者CDを検索条件に利用するか</param>
            /// <param name="useTORIHIKISAKI_CD">取引先CDを検索条件に利用するか</param>
            public KOBETSU_HINMEI_TANKA_RULE(bool useHINMEI_CD, bool useUNIT_CD, bool useNIOROSHI_GENBA_CD,
                               bool useNIOROSHI_GYOUSHA_CD, bool useUNPAN_GYOUSHA_CD, bool useGENBA_CD,
                               bool useGYOUSHA_CD, bool useTORIHIKISAKI_CD)
            {
                this.useHINMEI_CD = useHINMEI_CD;
                this.useUNIT_CD = useUNIT_CD;
                this.useNIOROSHI_GENBA_CD = useNIOROSHI_GENBA_CD;
                this.useNIOROSHI_GYOUSHA_CD = useNIOROSHI_GYOUSHA_CD;
                this.useUNPAN_GYOUSHA_CD = useUNPAN_GYOUSHA_CD;
                this.useGENBA_CD = useGENBA_CD;
                this.useGYOUSHA_CD = useGYOUSHA_CD;
                this.useTORIHIKISAKI_CD = useTORIHIKISAKI_CD;
            }

            /// <summary>
            /// 優先ルール一覧を返します
            /// </summary>
            /// <returns></returns>
            static public KOBETSU_HINMEI_TANKA_RULE[] RULES()
            {
                //HACK:仕様変更時は 資料の「共通動作・単価決定」シートのマトリクスを転記すればOK
                return new KOBETSU_HINMEI_TANKA_RULE[] {
                     #region "CongBinh 20150724 リビジョン55648の対応"
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, true, true, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, true, true, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true, true, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, false, true, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, false, true, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, true, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, true, false, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, true, false, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true, false, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, false, false, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, false, false, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, false, true, true ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, true, true, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, true, true, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true, true, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, false, true, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, false, true, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, true, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, true, false, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, true, false, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true, false, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, true, true, false, false, true, false ),
                    //new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true, false, false, true, false ),
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  true,  true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  true,  true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  true,  true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, true,  true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, true,  true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, true,  true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  false, true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  false, true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  false, true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, false, true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, false, true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, false, true, true  ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  true,  true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  true,  true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  true,  true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, true,  true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, true,  true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, true,  true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  true,  false, true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  true,  false, true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, true,  false, true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, true,  true,  false, false, true, false ),		
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, true,  false, false, true, false ),		
#endregion
                    new KOBETSU_HINMEI_TANKA_RULE( true, true, false, false, false, false, true, false )
               };
            }

            //デバッグログ用
            public override string ToString()
            {
                return string.Format("{0}{1} [ useHINMEI_CD = {2}, useUNIT_CD = {3}, useNIOROSHI_GENBA_CD = {4}, useNIOROSHI_GYOUSHA_CD = {5}, useUNPAN_GYOUSHA_CD = {6}, useGENBA_CD = {7}, useGYOUSHA_CD = {8}, useTORIHIKISAKI_CD = {9} ]",
                                   "", ""/*ダミー*/, useHINMEI_CD, useUNIT_CD, useNIOROSHI_GENBA_CD, useNIOROSHI_GYOUSHA_CD, useUNPAN_GYOUSHA_CD, useGENBA_CD, useGYOUSHA_CD, useTORIHIKISAKI_CD);
            }
        }

        /// <summary>
        /// 個別品名単価取得（単一ルール）
        /// </summary>
        /// <param name="param">入力された条件</param>
        /// <param name="rule">検索ルール</param>
        /// <param name="ent">trueの場合、見つかった場合エンティティを返す。falseの場合null</param>
        /// <returns>true:データあり/false:データ無</returns>
        #region "CongBinh 20150724 リビジョン55648の対応"
        //private static bool GetKobetsuhinmeiTanka(KobetsuHinmeiTankaParam param, 
        //                                          KOBETSU_HINMEI_TANKA_RULE rule, 
        //                                          out M_KOBETSU_HINMEI_TANKA ent)
        private static bool GetKobetsuhinmeiTanka(KobetsuHinmeiTankaParam param,
                                           KOBETSU_HINMEI_TANKA_RULE rule,
                                           ref M_KOBETSU_HINMEI_TANKA ent,
                                           ref List<M_KOBETSU_HINMEI_TANKA> ents)
        {
            // LogUtility.DebugMethodStart(param, rule);
            LogUtility.DebugMethodStart(param, rule, ent, ents);
        #endregion
            IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEI_TANKADao>();
            ent = null;

            //パラメータ必須チェック（使うものがnull等の場合即終了）
            if (rule.useHINMEI_CD && string.IsNullOrEmpty(param.HINMEI_CD)) return false;
            if (rule.useUNIT_CD && param.UNIT_CD < 0) return false;
            if (rule.useNIOROSHI_GENBA_CD && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)) return false;
            if (rule.useNIOROSHI_GYOUSHA_CD && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)) return false;
            if (rule.useUNPAN_GYOUSHA_CD && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD)) return false;
            if (rule.useGENBA_CD && string.IsNullOrEmpty(param.GENBA_CD)) return false;
            if (rule.useGYOUSHA_CD && string.IsNullOrEmpty(param.GYOUSHA_CD)) return false;
            if (rule.useTORIHIKISAKI_CD && string.IsNullOrEmpty(param.TORIHIKISAKI_CD)) return false;

            #region "CongBinh 20150724 リビジョン55648の対応"

            #region"削除"
            ////検索キー設定
            //var key = new M_KOBETSU_HINMEI_TANKA();
            //key.DENPYOU_KBN_CD = param.DENPYOU_KBN_CD;
            //if (param.DENSHU_KBN_CD > 0) key.DENSHU_KBN_CD = param.DENSHU_KBN_CD;
            //if (rule.useHINMEI_CD) key.HINMEI_CD = param.HINMEI_CD;
            //if (rule.useUNIT_CD) key.UNIT_CD = param.UNIT_CD;
            //if (rule.useNIOROSHI_GENBA_CD) key.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
            //if (rule.useNIOROSHI_GYOUSHA_CD) key.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
            //if (rule.useUNPAN_GYOUSHA_CD) key.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;
            //if (rule.useGENBA_CD) key.GENBA_CD = param.GENBA_CD;
            //if (rule.useGYOUSHA_CD) key.GYOUSHA_CD = param.GYOUSHA_CD;
            //if (rule.useTORIHIKISAKI_CD) key.TORIHIKISAKI_CD = param.TORIHIKISAKI_CD;

            ////検索実行
            //var results = kobetsuHinmeiTankaDao.GetAllValidData(key);           
            //if (results != null && 0 < results.Length)
            #endregion

            // DBアクセス削減のため、一回初期検索のみ行う、nullではない場合、検索済みと見なし、再検索しない	
            if (ents == null)
            {
                // 必要条件洗出し	
                var ruleRequired = new KOBETSU_HINMEI_TANKA_RULE(
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useHINMEI_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useUNIT_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useNIOROSHI_GENBA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useNIOROSHI_GYOUSHA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useUNPAN_GYOUSHA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useGENBA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useGYOUSHA_CD),
                    KOBETSU_HINMEI_TANKA_RULE.RULES().All(o => o.useTORIHIKISAKI_CD)
                    );

                #region"削除"
                ////理想はSQL修正して is null の条件追加だが、他への影響も考慮してnullチェックは自前で行う。
                //foreach (var result in results)
                //{
                //    //条件がfalseの部分はnullであることのチェックが必要
                //    if (!rule.useHINMEI_CD && !string.IsNullOrEmpty(result.HINMEI_CD)) continue;
                //    if (!rule.useUNIT_CD && !result.UNIT_CD.IsNull) continue;
                //    if (!rule.useNIOROSHI_GENBA_CD && !string.IsNullOrEmpty(result.NIOROSHI_GENBA_CD)) continue;
                //    if (!rule.useNIOROSHI_GYOUSHA_CD && !string.IsNullOrEmpty(result.NIOROSHI_GYOUSHA_CD)) continue;
                //    if (!rule.useUNPAN_GYOUSHA_CD && !string.IsNullOrEmpty(result.UNPAN_GYOUSHA_CD)) continue;
                //    if (!rule.useGENBA_CD && !string.IsNullOrEmpty(result.GENBA_CD)) continue;
                //    if (!rule.useGYOUSHA_CD && !string.IsNullOrEmpty(result.GYOUSHA_CD)) continue;
                //    if (!rule.useTORIHIKISAKI_CD && !string.IsNullOrEmpty(result.TORIHIKISAKI_CD)) continue;

                //    //データあり
                //    ent = result;
                //    LogUtility.DebugMethodEnd(true, ent);
                //    return true;
                //}
                #endregion

                //検索キー設定			
                var keyRequired = new M_KOBETSU_HINMEI_TANKA();
                keyRequired.DENPYOU_KBN_CD = param.DENPYOU_KBN_CD;

                if (ruleRequired.useHINMEI_CD) keyRequired.HINMEI_CD = param.HINMEI_CD;
                if (ruleRequired.useUNIT_CD) keyRequired.UNIT_CD = param.UNIT_CD;
                if (ruleRequired.useNIOROSHI_GENBA_CD) keyRequired.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                if (ruleRequired.useNIOROSHI_GYOUSHA_CD) keyRequired.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                if (ruleRequired.useUNPAN_GYOUSHA_CD) keyRequired.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;
                if (ruleRequired.useGENBA_CD) keyRequired.GENBA_CD = param.GENBA_CD;
                if (ruleRequired.useGYOUSHA_CD) keyRequired.GYOUSHA_CD = param.GYOUSHA_CD;
                if (ruleRequired.useTORIHIKISAKI_CD) keyRequired.TORIHIKISAKI_CD = param.TORIHIKISAKI_CD;

                // 初期化、結果が有るか問わず検索済みと見なし、再検索不要									
                ents = new List<M_KOBETSU_HINMEI_TANKA>();

                // 個別品名検索									
                // 指定伝種区分									
                if (param.DENSHU_KBN_CD > 0) keyRequired.DENSHU_KBN_CD = param.DENSHU_KBN_CD;
                ents.AddRange(kobetsuHinmeiTankaDao.GetAllValidData(keyRequired));
                // 「共通」伝種区分									
                keyRequired.DENSHU_KBN_CD = Convert.ToInt16(DENSHU_KBN.KYOUTSUU);
                ents.AddRange(kobetsuHinmeiTankaDao.GetAllValidData(keyRequired));
                // 上記検索に、param.DENSHU_KBN_CD<=0の場合、伝種条件が入力しないので、									
                // 検索結果に共通伝種検索と被るの可能性があるが、後続処理に影響がない
            }

            #region"削除"
            //// 伝種区分を「共通」にして再検索
            //key.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;
            //results = kobetsuHinmeiTankaDao.GetAllValidData(key);
            //if (results != null && 0 < results.Length)
            #endregion

            // 20150714 速度改善ため、再検索でなく、上記キャッシュを利用するように修正 Start		
            // 指定伝種と共通伝種は全部含むので、別々で共通を検索する必要ない		
            // 検索実行(フィルター適用)		
            var results = ents.
                // 条件を使う時は条件に合って、使わない時は必ず空白/null		
                // 元下部のループに有ったの判断条件もここに組込む		
                Where(o =>
                    ((rule.useHINMEI_CD && o.HINMEI_CD == param.HINMEI_CD) || (!rule.useHINMEI_CD && string.IsNullOrEmpty(o.HINMEI_CD))) &&
                    ((rule.useUNIT_CD && !o.UNIT_CD.IsNull && o.UNIT_CD.Value == param.UNIT_CD) || (!rule.useUNIT_CD && o.UNIT_CD.IsNull)) &&
                    ((rule.useNIOROSHI_GENBA_CD && o.NIOROSHI_GENBA_CD == param.NIOROSHI_GENBA_CD) || (!rule.useNIOROSHI_GENBA_CD && string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD))) &&
                    ((rule.useNIOROSHI_GYOUSHA_CD && o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD) || (!rule.useNIOROSHI_GYOUSHA_CD && string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD))) &&
                    ((rule.useUNPAN_GYOUSHA_CD && o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD) || (!rule.useUNPAN_GYOUSHA_CD && string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD))) &&
                    ((rule.useGENBA_CD && o.GENBA_CD == param.GENBA_CD) || (!rule.useGENBA_CD && string.IsNullOrEmpty(o.GENBA_CD))) &&
                    ((rule.useGYOUSHA_CD && o.GYOUSHA_CD == param.GYOUSHA_CD) || (!rule.useGYOUSHA_CD && string.IsNullOrEmpty(o.GYOUSHA_CD))) &&
                    ((rule.useTORIHIKISAKI_CD && o.TORIHIKISAKI_CD == param.TORIHIKISAKI_CD) || (!rule.useTORIHIKISAKI_CD && string.IsNullOrEmpty(o.TORIHIKISAKI_CD)))
                ).
                // ソート順(伝種区分：伝種は1~4及び100以上(運賃など)が可能ので、直接利用ではなく、0に変換し9より優先にする)		
                OrderBy(o => (o.DENSHU_KBN_CD.IsNull || o.DENSHU_KBN_CD.Value != Convert.ToInt16(DENSHU_KBN.KYOUTSUU)) ? 0 : 9);
            if (results != null && 0 < results.Count())
            {
                #region"削除"
                ////理想はSQL修正して is null の条件追加だが、他への影響も考慮してnullチェックは自前で行う。
                //foreach (var result in results)
                //{
                //    //条件がfalseの部分はnullであることのチェックが必要
                //    if (!rule.useHINMEI_CD && !string.IsNullOrEmpty(result.HINMEI_CD)) continue;
                //    if (!rule.useUNIT_CD && !result.UNIT_CD.IsNull) continue;
                //    if (!rule.useNIOROSHI_GENBA_CD && !string.IsNullOrEmpty(result.NIOROSHI_GENBA_CD)) continue;
                //    if (!rule.useNIOROSHI_GYOUSHA_CD && !string.IsNullOrEmpty(result.NIOROSHI_GYOUSHA_CD)) continue;
                //    if (!rule.useUNPAN_GYOUSHA_CD && !string.IsNullOrEmpty(result.UNPAN_GYOUSHA_CD)) continue;
                //    if (!rule.useGENBA_CD && !string.IsNullOrEmpty(result.GENBA_CD)) continue;
                //    if (!rule.useGYOUSHA_CD && !string.IsNullOrEmpty(result.GYOUSHA_CD)) continue;
                //    if (!rule.useTORIHIKISAKI_CD && !string.IsNullOrEmpty(result.TORIHIKISAKI_CD)) continue;

                //    //データあり
                //    ent = result;
                //    LogUtility.DebugMethodEnd(true, ent);
                //    return true;
                //}
                #endregion

                ent = results.FirstOrDefault();
                LogUtility.DebugMethodEnd(true, ent, ents);
                return true;

            }

            //データ無
            //   LogUtility.DebugMethodEnd(false, ent);
            LogUtility.DebugMethodEnd(false, ent, ents);
            return false;
            #endregion
        }
        #endregion

        #region 基本品名単価検索

        /// <summary>
        /// 基本品名単価を取得 (伝種区分CDがない場合)
        /// </summary>
        /// <param name="param">入力された条件</param>
        /// <param name="result">trueの場合、見つかった場合エンティティを返す。falseの場合null</param>
        /// <returns></returns>
        private static bool GetKihonHinmeitanka(KihonHinmeiTankaParam param,
                                                out M_KIHON_HINMEI_TANKA result)
        {
            LogUtility.DebugMethodStart(param);

            IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao = DaoInitUtility.GetComponent<IM_KIHON_HINMEI_TANKADao>();
            M_KIHON_HINMEI_TANKA keyEntity = new M_KIHON_HINMEI_TANKA();
            #region "CongBinh 20150724 リビジョン55648の対応"
            //M_KIHON_HINMEI_TANKA[] kihonHinmeiTankas = null;
            // 値あり：品名, 単位
            if (!string.IsNullOrEmpty(param.HINMEI_CD)
                && 0 < param.UNIT_CD
            #region"削除"
                //&& string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //               && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //               && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
            #endregion
                )
            #endregion
            {
                keyEntity.HINMEI_CD = param.HINMEI_CD;
                keyEntity.UNIT_CD = param.UNIT_CD;
                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //keyEntity.UNPAN_GYOUSHA_CD = string.Empty;              
                //kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                #endregion
                var kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity).AsEnumerable();
                // TODO: 下記条件の組合せは、個別品名のように、ルール化で順番処理可能のはず...		
                // 値あり：品名, 単位		
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                #endregion
                {
                    #region "CongBinh 20150724 リビジョン55648の対応"
                    #region"削除"
                    //  result = kihonHinmeiTankas[0];
                    //  return true;
                    #endregion
                    var resEntities000 = kihonHinmeiTankas.
                        // 条件空白ではない時は条件に合って、空白時は必ず空白/null
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD)).
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD)).
                        Where(o => string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD));
                    if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                    #endregion
                }

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //    keyEntity.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;

                //    for (int i = 0; i < 4; i++)
                //    {

                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GENBA_CD = null;
                //                break;

                //            case 2:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = null;
                //                break;

                //            case 3:
                //                keyEntity.UNPAN_GYOUSHA_CD = null;
                //                break;

                //            default:
                //                break;

                //        }

                //        kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //        if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //        {
                //            result = kihonHinmeiTankas[0];
                //            return true;
                //        }
                //    }
                //}
                #endregion
                // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者				
                if (!string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する				
                    // 荷卸現場＞荷卸業者＞運搬業者				
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない				
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD); // 運搬業者フィルター追加				
                    var resEntities011 = resEntities001.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 更に荷卸業者フィルター追加				
                    var resEntities111 = resEntities011.Where(o => o.NIOROSHI_GENBA_CD == param.NIOROSHI_GENBA_CD); // 更に荷卸現場フィルター追加				

                    // 荷卸現場と荷卸業者と運搬業者条件合ったら、優先				
                    if (resEntities111 != null && resEntities111.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities111.FirstOrDefault());
                        result = resEntities111.FirstOrDefault();
                        return true;
                    }
                    // 荷卸現場条件外す		
                    else if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities011.FirstOrDefault());
                        result = resEntities011.FirstOrDefault();
                        return true;
                    }
                    // 更に荷卸業者条件外す		
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities001.FirstOrDefault());
                        result = resEntities001.FirstOrDefault();
                        return true;
                    }
                    // 更に運搬業者条件外す(全条件外す)		
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 荷卸業者, 運搬業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //    keyEntity.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;

                //    for (int i = 0; i < 3; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = null;
                //                break;

                //            case 2:
                //                keyEntity.UNPAN_GYOUSHA_CD = null;
                //                break;

                //            default:
                //                break;

                //        }

                //        kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //        if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //        {
                //            result = kihonHinmeiTankas[0];
                //            return true;
                //        }
                //    }
                //}
                #endregion
                // 値あり：品名, 単位, 荷卸業者, 運搬業者		
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する		
                    // 荷卸業者＞運搬業者		
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない		
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD); // 運搬業者フィルター追加		
                    var resEntities011 = resEntities001.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 更に荷卸業者フィルター追加		

                    // 荷卸業者と運搬業者条件合ったら、優先		
                    if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities011.FirstOrDefault());
                        result = resEntities011.FirstOrDefault();
                        return true;
                    }
                    // 荷卸業者条件外す		
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities001.FirstOrDefault());
                        result = resEntities001.FirstOrDefault();
                        return true;
                    }
                    // 更に運搬業者条件外す(全条件外す)		
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 運搬業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;

                //    for (int i = 0; i < 2; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.UNPAN_GYOUSHA_CD = null;
                //                break;

                //            default:
                //                break;

                //        }

                //        kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //        if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //        {
                //            result = kihonHinmeiTankas[0];
                //            return true;
                //        }
                //    }
                //}
                #endregion

                // 値あり：品名, 単位, 運搬業者		
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する		
                    // 運搬業者		
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない		
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD); // 運搬業者フィルター追加		

                    // 運搬業者条件合ったら、優先		
                    if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities001.FirstOrDefault());
                        result = resEntities001.FirstOrDefault();
                        return true;
                    }
                    // 運搬業者条件外す(全条件外す)		
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 荷卸現場, 荷卸業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;

                //    for (int i = 0; i < 3; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GENBA_CD = null;
                //                break;

                //            case 2:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = null;
                //                break;

                //            default:
                //                break;

                //        }

                //        kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //        if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //        {
                //            result = kihonHinmeiTankas[0];
                //            return true;
                //        }
                //    }
                //}
                #endregion

                // 値あり：品名, 単位, 荷卸現場, 荷卸業者		
                if (!string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する		
                    // 荷卸現場＞荷卸業者		
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない		
                    var resEntities010 = resEntities000.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 荷卸業者フィルター追加		
                    var resEntities110 = resEntities010.Where(o => o.NIOROSHI_GENBA_CD == param.NIOROSHI_GENBA_CD); // 更に荷卸現場フィルター追加		

                    // 荷卸現場と荷卸業者条件合ったら、優先		
                    if (resEntities110 != null && resEntities110.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities110.FirstOrDefault());
                        result = resEntities110.FirstOrDefault();
                        return true;
                    }
                    // 荷卸現場条件外す		
                    else if (resEntities010 != null && resEntities010.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities010.FirstOrDefault());
                        result = resEntities010.FirstOrDefault();
                        return true;
                    }
                    // 更に荷卸業者条件外す(全条件外す)		
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 荷卸業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;

                //    for (int i = 0; i < 2; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = null;
                //                break;

                //            default:
                //                break;

                //        }

                //        kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //        if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //        {
                //            result = kihonHinmeiTankas[0];
                //            return true;
                //        }
                //    }
                //}
                #endregion

                // 値あり：品名, 単位, 荷卸業者			
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する			
                    // 荷卸業者			
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない			
                    var resEntities010 = resEntities000.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 荷卸業者フィルター追加			

                    // 荷卸業者条件合ったら、優先			
                    if (resEntities010 != null && resEntities010.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities010.FirstOrDefault());
                        result = resEntities010.FirstOrDefault();
                        return true;
                    }
                    // 荷卸業者条件外す(全条件外す)			
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
            }
                #endregion
            // 何も取得できなかった場合
            LogUtility.DebugMethodEnd(false, null);
            result = null;
            return false;
        }

        /// <summary>
        /// 基本品名単価を取得 (伝種区分CDがある場合)
        /// </summary>
        /// <param name="param">入力された条件</param>
        /// <param name="result">trueの場合、見つかった場合エンティティを返す。falseの場合null</param>
        /// <returns></returns>
        private static bool GetKihonHinmeitankaWithDenshuKbn(KihonHinmeiTankaParam param,
                                                             out M_KIHON_HINMEI_TANKA result)
        {
            IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao = DaoInitUtility.GetComponent<IM_KIHON_HINMEI_TANKADao>();
            M_KIHON_HINMEI_TANKA keyEntity = new M_KIHON_HINMEI_TANKA();
            #region "CongBinh 20150724 リビジョン55648の対応"
            LogUtility.DebugMethodStart(param);
            #region"削除"
            //    M_KIHON_HINMEI_TANKA[] kihonHinmeiTankas = null;
            //// 値あり：品名, 単位
            //if (!string.IsNullOrEmpty(param.HINMEI_CD)
            //    && 0 < param.UNIT_CD
            //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
            //    && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
            //    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
            #endregion
            if (!string.IsNullOrEmpty(param.HINMEI_CD) && 0 < param.UNIT_CD)
            #endregion
            {
                keyEntity.HINMEI_CD = param.HINMEI_CD;
                keyEntity.UNIT_CD = param.UNIT_CD;
                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //keyEntity.UNPAN_GYOUSHA_CD = string.Empty;

                //for (int j = 0; j < 4; j++)
                //{
                //    // 伝種区分、伝票区分の組み合わせを変更して再検索（共通も含むため）
                //    switch (j)
                //    {
                //        case 0:
                //            keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //            keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //            break;
                //        case 1:
                //            keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //            keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //            break;
                //        case 2:
                //            keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //            keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //            break;
                //        case 3:
                //            keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //            keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //            break;
                //    }
                //    kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //    {
                //        result = kihonHinmeiTankas[0];
                //        return true;
                //    }
                //}
                #endregion
                var kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity).AsEnumerable().
                    // 予備のため、先に伝票区分と伝種区分は合った又は共通のデータをフィルターする		
                    Where(o => !o.DENPYOU_KBN_CD.IsNull && (o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD || o.DENPYOU_KBN_CD.Value == (short)DENSHU_KBN.KYOUTSUU)).
                    Where(o => !o.DENSHU_KBN_CD.IsNull && (o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD || o.DENSHU_KBN_CD.Value == (short)DENSHU_KBN.KYOUTSUU));

                // TODO: 下記条件の組合せは、個別品名のように、ルール化で順番処理可能のはず...		
                // 値あり：品名, 単位		
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    var resEntities000 = kihonHinmeiTankas.
                        // 条件空白ではない時は条件に合って、空白時は必ず空白/null		
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GENBA_CD)).
                        Where(o => string.IsNullOrEmpty(o.NIOROSHI_GYOUSHA_CD)).
                        Where(o => string.IsNullOrEmpty(o.UNPAN_GYOUSHA_CD));
                    if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分		
                            // 利用する前OrderByする		
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加(伝種は1~4及び100以上(運賃など)が可能ので、直接利用ではなく、0に変換し9より優先にする)		
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加		

                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //    keyEntity.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                //    keyEntity.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;

                //    for (int i = 0; i < 6; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //                break;

                //            case 2:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //                break;

                //            case 3:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //                keyEntity.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                //                keyEntity.UNPAN_GYOUSHA_CD = string.Empty;
                //                break;

                //            case 4:
                //                keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //                break;

                //            case 5:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //                break;

                //            default:
                //                break;

                //        }
                //        for (int j = 0; j < 4; j++)
                //        {
                //            // 伝種区分、伝票区分の組み合わせを変更して再検索（共通も含むため）
                //            switch (j)
                //            {
                //                case 0:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 1:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 2:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //                case 3:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //            }
                //            kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //            if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //            {
                //                result = kihonHinmeiTankas[0];
                //                return true;
                //            }
                //        }
                //    }
                //}
                #endregion
                // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者	
                if (!string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する	
                    // 荷卸現場＞荷卸業者＞運搬業者	
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない	
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD); // 運搬業者フィルター追加	
                    var resEntities011 = resEntities001.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 更に荷卸業者フィルター追加	
                    var resEntities111 = resEntities011.Where(o => o.NIOROSHI_GENBA_CD == param.NIOROSHI_GENBA_CD); // 更に荷卸現場フィルター追加	

                    // 荷卸現場と荷卸業者と運搬業者条件合ったら、優先	
                    if (resEntities111 != null && resEntities111.Count() > 0)
                    {
                        resEntities111 = resEntities111.
                            // 優先順：伝種区分＞伝票区分	
                            // 利用する前OrderByする	
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加	
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加	

                        LogUtility.DebugMethodEnd(true, resEntities111.FirstOrDefault());
                        result = resEntities111.FirstOrDefault();
                        return true;
                    }
                    // 荷卸現場条件外す	
                    else if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        resEntities011 = resEntities011.
                            // 優先順：伝種区分＞伝票区分	
                            // 利用する前OrderByする	
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加	
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加	

                        LogUtility.DebugMethodEnd(true, resEntities011.FirstOrDefault());
                        result = resEntities011.FirstOrDefault();
                        return true;

                    }
                    // 更に荷卸業者条件外す	
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        resEntities001 = resEntities001.
                            // 優先順：伝種区分＞伝票区分	
                            // 利用する前OrderByする	
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加	
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加	

                        LogUtility.DebugMethodEnd(true, resEntities001.FirstOrDefault());
                        result = resEntities001.FirstOrDefault();
                        return true;
                    }
                    // 更に運搬業者条件外す(全条件外す)	
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分	
                            // 利用する前OrderByする	
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加	
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加	

                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 荷卸業者, 運搬業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //    keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //    keyEntity.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;

                //    for (int i = 0; i < 4; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //                break;

                //            case 2:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //                keyEntity.UNPAN_GYOUSHA_CD = string.Empty;
                //                break;

                //            case 3:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //                break;

                //            default:
                //                break;

                //        }
                //        for (int j = 0; j < 4; j++)
                //        {
                //            // 伝種区分、伝票区分の組み合わせを変更して再検索（共通も含むため）
                //            switch (j)
                //            {
                //                case 0:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 1:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 2:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //                case 3:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //            }
                //            kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //            if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //            {
                //                result = kihonHinmeiTankas[0];
                //                return true;
                //            }
                //        }
                //    }
                //}
                #endregion
                // 値あり：品名, 単位, 荷卸業者, 運搬業者		
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する		
                    // 荷卸業者＞運搬業者		
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない		
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD); // 運搬業者フィルター追加		
                    var resEntities011 = resEntities001.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 更に荷卸業者フィルター追加		

                    // 荷卸業者と運搬業者条件合ったら、優先		
                    if (resEntities011 != null && resEntities011.Count() > 0)
                    {
                        resEntities011 = resEntities011.
                            // 優先順：伝種区分＞伝票区分		
                            // 利用する前OrderByする		
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加		
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加		

                        LogUtility.DebugMethodEnd(true, resEntities011.FirstOrDefault());
                        result = resEntities011.FirstOrDefault();
                        return true;
                    }
                    // 荷卸業者条件外す		
                    else if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        resEntities001 = resEntities001.
                            // 優先順：伝種区分＞伝票区分		
                            // 利用する前OrderByする		
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加		
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加		

                        LogUtility.DebugMethodEnd(true, resEntities001.FirstOrDefault());
                        result = resEntities001.FirstOrDefault();
                        return true;
                    }
                    // 更に運搬業者条件外す(全条件外す)		
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分		
                            // 利用する前OrderByする		
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加		
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加		

                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 運搬業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //    keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //    keyEntity.UNPAN_GYOUSHA_CD = param.UNPAN_GYOUSHA_CD;

                //    for (int i = 0; i < 2; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.UNPAN_GYOUSHA_CD = string.Empty;
                //                break;

                //            default:
                //                break;

                //        }
                //        for (int j = 0; j < 4; j++)
                //        {
                //            // 伝種区分、伝票区分の組み合わせを変更して再検索（共通も含むため）
                //            switch (j)
                //            {
                //                case 0:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 1:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 2:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //                case 3:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //            }
                //            kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //            if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //            {
                //                result = kihonHinmeiTankas[0];
                //                return true;
                //            }
                //        }
                //    }
                //}
                #endregion
                // 値あり：品名, 単位, 運搬業者		
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                && string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                && !string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する		
                    // 運搬業者		
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない		
                    var resEntities001 = resEntities000.Where(o => o.UNPAN_GYOUSHA_CD == param.UNPAN_GYOUSHA_CD); // 運搬業者フィルター追加		

                    // 運搬業者条件合ったら、優先		
                    if (resEntities001 != null && resEntities001.Count() > 0)
                    {
                        resEntities001 = resEntities001.
                            // 優先順：伝種区分＞伝票区分		
                            // 利用する前OrderByする		
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加		
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加		

                        LogUtility.DebugMethodEnd(true, resEntities001.FirstOrDefault());
                        result = resEntities001.FirstOrDefault();
                        return true;
                    }
                    // 運搬業者条件外す(全条件外す)		
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分		
                            // 利用する前OrderByする		
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加		
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加		

                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 荷卸現場, 荷卸業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //    keyEntity.NIOROSHI_GENBA_CD = param.NIOROSHI_GENBA_CD;
                //    keyEntity.UNPAN_GYOUSHA_CD = string.Empty;

                //    for (int i = 0; i < 3; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //                break;

                //            case 2:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //                break;

                //            default:
                //                break;

                //        }
                //        for (int j = 0; j < 4; j++)
                //        {
                //            // 伝種区分、伝票区分の組み合わせを変更して再検索（共通も含むため）
                //            switch (j)
                //            {
                //                case 0:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 1:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 2:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //                case 3:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //            }
                //            kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //            if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //            {
                //                result = kihonHinmeiTankas[0];
                //                return true;
                //            }
                //        }
                //    }
                //}
                #endregion
                // 値あり：品名, 単位, 荷卸現場, 荷卸業者
                if (!string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities010 = resEntities000.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 荷卸業者フィルター追加
                    var resEntities110 = resEntities010.Where(o => o.NIOROSHI_GENBA_CD == param.NIOROSHI_GENBA_CD); // 更に荷卸現場フィルター追加

                    // 荷卸現場と荷卸業者条件合ったら、優先
                    if (resEntities110 != null && resEntities110.Count() > 0)
                    {
                        resEntities110 = resEntities110.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(true, resEntities110.FirstOrDefault());
                        result = resEntities110.FirstOrDefault();
                        return true;
                    }
                    // 荷卸現場条件外す
                    else if (resEntities010 != null && resEntities010.Count() > 0)
                    {

                        resEntities010 = resEntities010.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                                   OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加
                                   ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(true, resEntities010.FirstOrDefault());
                        result = resEntities010.FirstOrDefault();
                        return true;
                    }
                    // 更に荷卸業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                            OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion

                #region "CongBinh 20150724 リビジョン55648の対応"
                #region"削除"
                //// 値あり：品名, 単位, 荷卸業者
                //if (!string.IsNullOrEmpty(param.HINMEI_CD)
                //    && 0 < param.UNIT_CD
                //    && string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                //    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                //    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                //{
                //    keyEntity.HINMEI_CD = param.HINMEI_CD;
                //    keyEntity.UNIT_CD = param.UNIT_CD;
                //    keyEntity.NIOROSHI_GYOUSHA_CD = param.NIOROSHI_GYOUSHA_CD;
                //    keyEntity.NIOROSHI_GENBA_CD = string.Empty;
                //    keyEntity.UNPAN_GYOUSHA_CD = string.Empty;

                //    for (int i = 0; i < 2; i++)
                //    {
                //        // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                //        // 荷卸現場＞荷卸業者＞運搬業者
                //        switch (i)
                //        {
                //            case 0:
                //                break;

                //            case 1:
                //                keyEntity.NIOROSHI_GYOUSHA_CD = string.Empty;
                //                break;

                //            default:
                //                break;

                //        }
                //        for (int j = 0; j < 4; j++)
                //        {
                //            // 伝種区分、伝票区分の組み合わせを変更して再検索（共通も含むため）
                //            switch (j)
                //            {
                //                case 0:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 1:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)param.DENSHU_KBN_CD;
                //                    break;
                //                case 2:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)param.DENPYOU_KBN_CD;
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //                case 3:
                //                    keyEntity.DENPYOU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;    // 共通
                //                    keyEntity.DENSHU_KBN_CD = (SqlInt16)(short)DENSHU_KBN.KYOUTSUU;     // 共通
                //                    break;
                //            }
                //            kihonHinmeiTankas = kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                //            if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                //            {
                //                result = kihonHinmeiTankas[0];
                //                return true;
                //            }
                //        }
                //    }
                //}
                #endregion
                // 値あり：品名, 単位, 荷卸業者
                if (string.IsNullOrEmpty(param.NIOROSHI_GENBA_CD)
                    && !string.IsNullOrEmpty(param.NIOROSHI_GYOUSHA_CD)
                    && string.IsNullOrEmpty(param.UNPAN_GYOUSHA_CD))
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    // 荷卸業者
                    var resEntities000 = kihonHinmeiTankas; // フィルターしない
                    var resEntities010 = resEntities000.Where(o => o.NIOROSHI_GYOUSHA_CD == param.NIOROSHI_GYOUSHA_CD); // 荷卸業者フィルター追加

                    // 荷卸業者条件合ったら、優先
                    if (resEntities010 != null && resEntities010.Count() > 0)
                    {
                        resEntities010 = resEntities010.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                         OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加
                         ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(true, resEntities010.FirstOrDefault());
                        result = resEntities010.FirstOrDefault();
                        return true;
                    }
                    // 荷卸業者条件外す(全条件外す)
                    else if (resEntities000 != null && resEntities000.Count() > 0)
                    {
                        resEntities000 = resEntities000.
                            // 優先順：伝種区分＞伝票区分
                            // 利用する前OrderByする
                          OrderBy(o => o.DENSHU_KBN_CD.Value == param.DENSHU_KBN_CD ? 0 : 9). // 伝種区分順追加
                            ThenBy(o => o.DENPYOU_KBN_CD.Value == param.DENPYOU_KBN_CD ? 0 : 9); // 伝票区分順追加

                        LogUtility.DebugMethodEnd(true, resEntities000.FirstOrDefault());
                        result = resEntities000.FirstOrDefault();
                        return true;
                    }
                }
                #endregion
            }
            LogUtility.DebugMethodEnd(false, null);
            // 何も取得できなかった場合
            result = null;
            return false;
        }

        /// <summary>
        /// 基本品名単価を取得
        /// </summary>
        /// <param name="param">入力された条件</param>
        /// <param name="result">trueの場合、見つかった場合エンティティを返す。falseの場合null</param>
        /// <returns>M_KIHON_HINMEI_TANKA</returns>
        public static M_KIHON_HINMEI_TANKA GetKihonHinmeitanka(KihonHinmeiTankaParam dto)
        {
            LogUtility.DebugMethodStart(dto);

            M_KIHON_HINMEI_TANKA ret;

            //伝種区分が設定されない場合
            if (dto.DENSHU_KBN_CD == null)
            {
                if (GetKihonHinmeitanka(dto, out ret))
                {
                    //データあり
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }
            //伝種区分が設定される場合
            else
            {
                if (GetKihonHinmeitankaWithDenshuKbn(dto, out ret))
                {
                    //データあり
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }

            //データ無
            LogUtility.DebugMethodEnd(null);
            return null;
        }

        #endregion
    }
}
