using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;
using r_framework.CustomControl;
using r_framework.APP.PopUp.Base;
using System.Windows.Forms;
using r_framework.Entity;
using r_framework.Setting;
using System.Reflection;
using System.Data;
using r_framework.Const;
using System.Text.RegularExpressions;
using r_framework.Dto;
using System.Collections.ObjectModel;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// 検索、選択ポップアップのUtilクラス
    /// </summary>
    public class PopupUtility
    {
        #region ポップアップ表示メソッド
        #region 拠点
        public struct KyotenSentakuDialogReslut
        {
            public string kyotenCD;
            public string kyotenNameRyaku;

            public KyotenSentakuDialogReslut(string cd, string nameRyaku)
            {
                this.kyotenCD = cd;
                this.kyotenNameRyaku = nameRyaku;
            }
        }
        /// <summary>
        /// 拠点選択ポップアップ
        /// KyotenSentakuResultには拠点選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultKyotenCD">念のため定義</param>
        /// <param name="KyotenSentakuResult">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowKyotenSentakuDialog(string DefaultKyotenCD, out KyotenSentakuDialogReslut KyotenSentakuResult)
        {
            DialogResult result = DialogResult.None;
            KyotenSentakuResult = new KyotenSentakuDialogReslut(string.Empty, string.Empty);
            string popupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateMasterKyoutsuPopup2();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_KYOTEN, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 2 <= popUpReturnParams.Count)
            {
                // 拠点CD
                var kyotenCDParam = popUpReturnParams[0];
                if (kyotenCDParam != null && kyotenCDParam[0].Value != null)
                {
                    KyotenSentakuResult.kyotenCD = kyotenCDParam[0].Value.ToString();
                }

                // 拠点名
                var kyotenNameRyakuParam = popUpReturnParams[1];
                if (kyotenNameRyakuParam != null && kyotenNameRyakuParam[0].Value != null)
                {
                    KyotenSentakuResult.kyotenNameRyaku = kyotenNameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 排出事業者
        public struct HaishutsuJigyoushaDialogReslut
        {
            public string HaishutsuJigyoushaCD;
            public string HaishutsuJigyoushaNameRyaku;
            public string HaishutsuJigyoushaFurigana;
            public string HaishutsuJigyoushaPost;
            public string HaishutsuJigyoushaTodofukenNameRyaku;
            public string HaishutsuJigyoushaAddress1;
            public string HaishutsuJigyoushaTel;

            public HaishutsuJigyoushaDialogReslut(string cd, string nameRyaku, string furigana, string post, string todofukenNameRyaku, string address1, string tel)
            {
                HaishutsuJigyoushaCD = cd;
                HaishutsuJigyoushaNameRyaku = nameRyaku;
                HaishutsuJigyoushaFurigana = furigana;
                HaishutsuJigyoushaPost = post;
                HaishutsuJigyoushaTodofukenNameRyaku = todofukenNameRyaku;
                HaishutsuJigyoushaAddress1 = address1;
                HaishutsuJigyoushaTel = tel;
            }
        }

        /// <summary>
        /// 排出業者検索ポップアップ
        /// HaishutsuJigyoushaDialogには排出業者検索ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultJigyoushaCD">念のため定義</param>
        /// <param name="HaishutsuJigyoushaDialog">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowHaishutsuJigyoushaKensakuDialog(string DefaultJigyoushaCD, out HaishutsuJigyoushaDialogReslut HaishutsuJigyoushaDialog)
        {
            DialogResult result = DialogResult.None;
            HaishutsuJigyoushaDialog = new HaishutsuJigyoushaDialogReslut(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            var popupForm = PopupUtility.CreateKensakuKyoutsuuPopup();

            if (popupForm == null)
            {
                return result;
            }

            // 検索条件
            Collection<PopupSearchSendParamDto> popupWindowSetting = new Collection<PopupSearchSendParamDto>();
            PopupSearchSendParamDto condition = new PopupSearchSendParamDto();
            condition.KeyName = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
            condition.Value = "TRUE";
            popupWindowSetting.Add(condition);
            
            // タイトル変更
            popupForm.PopupTitleLabel = "排出事業者検索";

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_GYOUSHA, null, popupWindowSetting, "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_FURIGANA,GYOUSHA_POST,TODOUFUKEN_NAME_RYAKU,GYOUSHA_ADDRESS1,GYOUSHA_TEL", false, out popUpReturnParams);

            if (popUpReturnParams != null && 6 <= popUpReturnParams.Count)
            {

                // CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaCD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaNameRyaku = nameRyakuParam[0].Value.ToString();
                }

                // フリガナ
                var furigana = popUpReturnParams[2];
                if (furigana != null && furigana[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaFurigana = furigana[0].Value.ToString();
                }

                // 郵便番号
                var post = popUpReturnParams[3];
                if (post != null && post[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaPost = post[0].Value.ToString();
                }

                // 都道府県名
                var todohukenNameRyaku = popUpReturnParams[4];
                if (todohukenNameRyaku != null && todohukenNameRyaku[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaTodofukenNameRyaku = todohukenNameRyaku[0].Value.ToString();
                }

                // 住所1
                var address1 = popUpReturnParams[5];
                if (address1 != null && address1[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaAddress1 = address1[0].Value.ToString();
                }

                // 電話番号
                var tel = popUpReturnParams[6];
                if (tel != null && tel[0].Value != null)
                {
                    HaishutsuJigyoushaDialog.HaishutsuJigyoushaTel = tel[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 排出事業場
        public struct HaishutsuJigyoujouDialogReslut
        {

            public string GyoushaCD;
            public string GyoushaNameRyaku;
            public string GenbaCD;
            public string GenbaNameRyaku;
            public string GenbaFurigana;
            public string GenbaPost;
            public string GenbaTodofukenNameRyaku;
            public string GenbaAddress1;
            public string GenbaTel;

            public HaishutsuJigyoujouDialogReslut(string gyoushaCd, string gyoushaNameRyaku, string genbaCd, string genbaNameRyaku, string genbaFurigana, string genbaPost, string genbaTodofukenNameRyaku, string genbaAddress1, string genbaTel)
            {
                this.GyoushaCD = gyoushaCd;
                this.GyoushaNameRyaku = gyoushaNameRyaku;
                this.GenbaCD = genbaCd;
                this.GenbaNameRyaku = genbaNameRyaku;
                this.GenbaFurigana = genbaFurigana;
                this.GenbaPost = genbaPost;
                this.GenbaTodofukenNameRyaku = genbaTodofukenNameRyaku;
                this.GenbaAddress1 = genbaAddress1;
                this.GenbaTel = genbaTel;
            }
        }

        /// <summary>
        /// 排出事業場検索ポップアップ
        /// HaishutsuJigyoujouDialogReslutには排出事業場検索ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultJigyoushaCD">排出事業者の絞り込み用CD</param>
        /// <param name="DefaultJigyoujouCD">念のため定義</param>
        /// <param name="HaishutsuJigyoujouDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowHaishutsuJigyoujouKensakuDialog(string DefaultJigyoushaCD, string DefaultJigyoujouCD, out HaishutsuJigyoujouDialogReslut HaishutsuJigyoujouDialogReslut)
        {
            DialogResult result = DialogResult.None;
            HaishutsuJigyoujouDialogReslut = new HaishutsuJigyoujouDialogReslut(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            string popupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU,GENBA_FURIGANA,GENBA_POST,TODOUFUKEN_NAME_RYAKU,GENBA_ADDRESS1,GENBA_TEL";

            var popupForm = PopupUtility.CreateKensakuKyoutsuuPopupForMultiKey();

            if (popupForm == null)
            {
                return result;
            }

            // 検索条件
            Collection<PopupSearchSendParamDto> popupWindowSetting = new Collection<PopupSearchSendParamDto>();
            if (!string.IsNullOrEmpty(DefaultJigyoushaCD))
            {
                PopupSearchSendParamDto gyoushaCondition = new PopupSearchSendParamDto();
                gyoushaCondition.KeyName = "GYOUSHA_CD";
                gyoushaCondition.Value = DefaultJigyoushaCD;
                popupWindowSetting.Add(gyoushaCondition);
            }

            PopupSearchSendParamDto haishutsuJigyoujouCondition = new PopupSearchSendParamDto();
            haishutsuJigyoujouCondition.KeyName = "HAISHUTSU_NIZUMI_GENBA_KBN";
            haishutsuJigyoujouCondition.Value = "TRUE";
            popupWindowSetting.Add(haishutsuJigyoujouCondition);

            // タイトル変更
            popupForm.PopupTitleLabel = "排出事業場検索";

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_GENBA, null, popupWindowSetting, popupGetMasterField, false, out popUpReturnParams);

            if (popUpReturnParams != null && 8 <= popUpReturnParams.Count)
            {

                // 業者CD
                var gyoushaCdParam = popUpReturnParams[0];
                if (gyoushaCdParam != null && gyoushaCdParam[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GyoushaCD = gyoushaCdParam[0].Value.ToString();
                }

                // 業者名
                var gyoushaNameRyakuParam = popUpReturnParams[1];
                if (gyoushaNameRyakuParam != null && gyoushaNameRyakuParam[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GyoushaNameRyaku = gyoushaNameRyakuParam[0].Value.ToString();
                }

                // CD
                var cdParam = popUpReturnParams[2];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaCD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[3];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaNameRyaku = nameRyakuParam[0].Value.ToString();
                }

                // フリガナ
                var furigana = popUpReturnParams[4];
                if (furigana != null && furigana[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaFurigana = furigana[0].Value.ToString();
                }

                // 郵便番号
                var post = popUpReturnParams[5];
                if (post != null && post[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaPost = post[0].Value.ToString();
                }

                // 都道府県名
                var todohukenNameRyaku = popUpReturnParams[6];
                if (todohukenNameRyaku != null && todohukenNameRyaku[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaTodofukenNameRyaku = todohukenNameRyaku[0].Value.ToString();
                }

                // 住所1
                var address1 = popUpReturnParams[7];
                if (address1 != null && address1[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaAddress1 = address1[0].Value.ToString();
                }

                // 電話番号
                var tel = popUpReturnParams[8];
                if (tel != null && tel[0].Value != null)
                {
                    HaishutsuJigyoujouDialogReslut.GenbaTel = tel[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 種類
        public struct ShuruiSentakuDialogReslut
        {
            public string shuruiCD;
            public string shuruiNameRyaku;

            public ShuruiSentakuDialogReslut(string cd, string nameRyaku)
            {
                this.shuruiCD = cd;
                this.shuruiNameRyaku = nameRyaku;
            }
        }
        /// <summary>
        /// 種類選択ポップアップ
        /// ShuruiSentakuDialogReslutには種類選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultShuruiCD">念のため定義</param>
        /// <param name="ShuruiSentakuResult">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowShuruiSentakuDialog(string DefaultShuruiCD, out ShuruiSentakuDialogReslut ShuruiSentakuResult)
        {
            DialogResult result = DialogResult.None;
            ShuruiSentakuResult = new ShuruiSentakuDialogReslut(string.Empty, string.Empty);
            string popupGetMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateMasterKyoutsuPopup2();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_SHURUI, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 2 <= popUpReturnParams.Count)
            {
                // 種類CD
                var shuruiCDParam = popUpReturnParams[0];
                if (shuruiCDParam != null && shuruiCDParam[0].Value != null)
                {
                    ShuruiSentakuResult.shuruiCD = shuruiCDParam[0].Value.ToString();
                }

                // 種類名
                var shuruiNameRyakuParam = popUpReturnParams[1];
                if (shuruiNameRyakuParam != null && shuruiNameRyakuParam[0].Value != null)
                {
                    ShuruiSentakuResult.shuruiNameRyaku = shuruiNameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 社員
        public struct ShainSentakuDialogReslut
        {
            public string shainCD;
            public string shainNameRyaku;

            public ShainSentakuDialogReslut(string cd, string nameRyaku)
            {
                this.shainCD = cd;
                this.shainNameRyaku = nameRyaku;
            }
        }
        /// <summary>
        /// 社員選択ポップアップ
        /// ShuruiSentakuDialogReslutには社員選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultShainCD">念のため定義</param>
        /// <param name="ShainSentakuResult">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowShainSentakuDialog(string DefaultShainCD, out ShainSentakuDialogReslut ShainSentakuResult)
        {
            DialogResult result = DialogResult.None;
            ShainSentakuResult = new ShainSentakuDialogReslut(string.Empty, string.Empty);
            string popupGetMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateMasterKyoutsuPopup2();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_SHAIN, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 2 <= popUpReturnParams.Count)
            {
                // 社員CD
                var shainCDParam = popUpReturnParams[0];
                if (shainCDParam != null && shainCDParam[0].Value != null)
                {
                    ShainSentakuResult.shainCD = shainCDParam[0].Value.ToString();
                }

                // 社員名
                var shainNameRyakuParam = popUpReturnParams[1];
                if (shainNameRyakuParam != null && shainNameRyakuParam[0].Value != null)
                {
                    ShainSentakuResult.shainNameRyaku = shainNameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 運搬受託者
        public struct UnpanJutakushaDialogReslut
        {
            public string CD;
            public string NameRyaku;
            public string Furigana;
            public string Post;
            public string TodofukenNameRyaku;
            public string Address1;
            public string Tel;

            public UnpanJutakushaDialogReslut(string cd, string nameRyaku, string furigana, string post, string todofukenNameRyaku, string address1, string tel)
            {
                this.CD = cd;
                this.NameRyaku = nameRyaku;
                this.Furigana = furigana;
                this.Post = post;
                this.TodofukenNameRyaku = todofukenNameRyaku;
                this.Address1 = address1;
                this.Tel = tel;
            }
        }

        /// <summary>
        /// 運搬受託者検索ポップアップ
        /// UnpanJutakushaDialogReslutには運搬受託者検索ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultUnpanJutakushaCD">念のため定義</param>
        /// <param name="UnpanJutakushaDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowUnpanJutakushaKensakuDialog(string DefaultUnpanJutakushaCD, out UnpanJutakushaDialogReslut UnpanJutakushaDialogReslut)
        {
            DialogResult result = DialogResult.None;
            UnpanJutakushaDialogReslut = new UnpanJutakushaDialogReslut(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            string popupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_FURIGANA,GYOUSHA_POST,TODOUFUKEN_NAME_RYAKU,GYOUSHA_ADDRESS1,GYOUSHA_TEL";

            var popupForm = PopupUtility.CreateKensakuKyoutsuuPopup();

            if (popupForm == null)
            {
                return result;
            }

            // 検索条件
            Collection<PopupSearchSendParamDto> popupWindowSetting = new Collection<PopupSearchSendParamDto>();
            PopupSearchSendParamDto condition = new PopupSearchSendParamDto();
            condition.KeyName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
            condition.Value = "TRUE";
            popupWindowSetting.Add(condition);

            // タイトル変更
            popupForm.PopupTitleLabel = "運搬受託者検索";

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_GYOUSHA, null, popupWindowSetting, popupGetMasterField, false, out popUpReturnParams);

            if (popUpReturnParams != null && 6 <= popUpReturnParams.Count)
            {

                // CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.CD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.NameRyaku = nameRyakuParam[0].Value.ToString();
                }

                // フリガナ
                var furigana = popUpReturnParams[2];
                if (furigana != null && furigana[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.Furigana = furigana[0].Value.ToString();
                }

                // 郵便番号
                var post = popUpReturnParams[3];
                if (post != null && post[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.Post = post[0].Value.ToString();
                }

                // 都道府県名
                var todohukenNameRyaku = popUpReturnParams[4];
                if (todohukenNameRyaku != null && todohukenNameRyaku[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.TodofukenNameRyaku = todohukenNameRyaku[0].Value.ToString();
                }

                // 住所1
                var address1 = popUpReturnParams[5];
                if (address1 != null && address1[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.Address1 = address1[0].Value.ToString();
                }

                // 電話番号
                var tel = popUpReturnParams[6];
                if (tel != null && tel[0].Value != null)
                {
                    UnpanJutakushaDialogReslut.Tel = tel[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 処分受託者
        public struct ShobunJutakushaDialogReslut
        {
            public string CD;
            public string NameRyaku;
            public string Furigana;
            public string Post;
            public string TodofukenNameRyaku;
            public string Address1;
            public string Tel;

            public ShobunJutakushaDialogReslut(string cd, string nameRyaku, string furigana, string post, string todofukenNameRyaku, string address1, string tel)
            {
                this.CD = cd;
                this.NameRyaku = nameRyaku;
                this.Furigana = furigana;
                this.Post = post;
                this.TodofukenNameRyaku = todofukenNameRyaku;
                this.Address1 = address1;
                this.Tel = tel;
            }
        }

        /// <summary>
        /// 処分受託者検索ポップアップ
        /// ShobunJutakushaDialogReslutには処分受託者検索ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultShobunJutakushaCD">念のため定義</param>
        /// <param name="ShobunJutakushaDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowShobunJutakushaKensakuDialog(string DefaultShobunJutakushaCD, out ShobunJutakushaDialogReslut ShobunJutakushaDialogReslut)
        {
            DialogResult result = DialogResult.None;
            ShobunJutakushaDialogReslut = new ShobunJutakushaDialogReslut(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            string popupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_FURIGANA,GYOUSHA_POST,TODOUFUKEN_NAME_RYAKU,GYOUSHA_ADDRESS1,GYOUSHA_TEL";

            var popupForm = PopupUtility.CreateKensakuKyoutsuuPopup();

            if (popupForm == null)
            {
                return result;
            }

            // 検索条件
            Collection<PopupSearchSendParamDto> popupWindowSetting = new Collection<PopupSearchSendParamDto>();
            PopupSearchSendParamDto condition = new PopupSearchSendParamDto();
            condition.KeyName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
            condition.Value = "TRUE";
            popupWindowSetting.Add(condition);

            // タイトル変更
            popupForm.PopupTitleLabel = "処分受託者検索";

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_GYOUSHA, null, popupWindowSetting, popupGetMasterField, false, out popUpReturnParams);

            if (popUpReturnParams != null && 7 <= popUpReturnParams.Count)
            {

                // CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.CD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.NameRyaku = nameRyakuParam[0].Value.ToString();
                }

                // フリガナ
                var furigana = popUpReturnParams[2];
                if (furigana != null && furigana[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.Furigana = furigana[0].Value.ToString();
                }

                // 郵便番号
                var post = popUpReturnParams[3];
                if (post != null && post[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.Post = post[0].Value.ToString();
                }

                // 都道府県名
                var todohukenNameRyaku = popUpReturnParams[4];
                if (todohukenNameRyaku != null && todohukenNameRyaku[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.TodofukenNameRyaku = todohukenNameRyaku[0].Value.ToString();
                }

                // 住所1
                var address1 = popUpReturnParams[5];
                if (address1 != null && address1[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.Address1 = address1[0].Value.ToString();
                }

                // 電話番号
                var tel = popUpReturnParams[6];
                if (tel != null && tel[0].Value != null)
                {
                    ShobunJutakushaDialogReslut.Tel = tel[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 荷姿
        public struct NisugataDialogReslut
        {
            public string CD;
            public string NameRyaku;

            public NisugataDialogReslut(string cd, string nameRyaku)
            {
                this.CD = cd;
                this.NameRyaku = nameRyaku;
            }
        }
        /// <summary>
        /// 荷姿選択ポップアップ
        /// NisugataDialogReslutには荷姿選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultNisugataCD">念のため定義</param>
        /// <param name="NisugataDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowNisugataSentakuDialog(string DefaultNisugataCD, out NisugataDialogReslut NisugataDialogReslut)
        {
            DialogResult result = DialogResult.None;
            NisugataDialogReslut = new NisugataDialogReslut(string.Empty, string.Empty);
            string popupGetMasterField = "NISUGATA_CD, NISUGATA_NAME_RYAKU";

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateMasterKyoutsuPopup2();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_NISUGATA, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 2 <= popUpReturnParams.Count)
            {
                // CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    NisugataDialogReslut.CD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    NisugataDialogReslut.NameRyaku = nameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 単位
        public struct UnitDialogReslut
        {
            public string CD;
            public string NameRyaku;

            public UnitDialogReslut(string cd, string nameRyaku)
            {
                this.CD = cd;
                this.NameRyaku = nameRyaku;
            }
        }
        /// <summary>
        /// 単位選択ポップアップ
        /// UnitDialogReslutには単位選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultUnitCD">念のため定義</param>
        /// <param name="UnitDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowUnitSentakuDialog(string DefaultUnitCD, out UnitDialogReslut UnitDialogReslut)
        {
            DialogResult result = DialogResult.None;
            UnitDialogReslut = new UnitDialogReslut(string.Empty, string.Empty);
            string popupGetMasterField = "UNIT_CD, UNIT_NAME_RYAKU";

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateMasterKyoutsuPopup2();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_UNIT, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 2 <= popUpReturnParams.Count)
            {
                // CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    UnitDialogReslut.CD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    UnitDialogReslut.NameRyaku = nameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 車輌
        public struct SharyouDialogReslut
        {
            public string SharyouCD;
            public string SharyouNameRyaku;
            public string GyoushaCD;
            public string GyoushaNameRyaku;
            public string ShashuCD;
            public string ShashuNameRyaku;

            public SharyouDialogReslut(string sharyouCd, string sharyouNameRyaku, string gyoushaCd, string gyoushaNameRyaku, string shashuCd, string shashuNameRyaku)
            {
                this.SharyouCD = sharyouCd;
                this.SharyouNameRyaku = sharyouNameRyaku;
                this.GyoushaCD = gyoushaCd;
                this.GyoushaNameRyaku = gyoushaNameRyaku;
                this.ShashuCD = shashuCd;
                this.ShashuNameRyaku = shashuNameRyaku;
            }
        }
        /// <summary>
        /// 車輌選択ポップアップ
        /// SharyouDialogReslutには車輌選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultGyoushaCD">業者CD</param>
        /// <param name="DefaultShashuCD">念のため定義</param>
        /// <param name="SharyouDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowSharyouSentakuDialog(string DefaultGyoushaCD, string DefaultShashuCD, out SharyouDialogReslut SharyouDialogReslut)
        {
            DialogResult result = DialogResult.None;
            SharyouDialogReslut = new SharyouDialogReslut(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            string popupGetMasterField = string.Empty;

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateSyaryoSentaku();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_SHARYOU, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 6 <= popUpReturnParams.Count)
            {
                // 車輌CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    SharyouDialogReslut.SharyouCD = cdParam[0].Value.ToString();
                }

                // 車輌名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    SharyouDialogReslut.SharyouNameRyaku = nameRyakuParam[0].Value.ToString();
                }

                // 業者CD
                var gyoushaCdParam = popUpReturnParams[2];
                if (gyoushaCdParam != null && gyoushaCdParam[0].Value != null)
                {
                    SharyouDialogReslut.GyoushaCD = gyoushaCdParam[0].Value.ToString();
                }

                // 業者名称
                var gyoushaNameRyakuParam = popUpReturnParams[3];
                if (gyoushaNameRyakuParam != null && gyoushaNameRyakuParam[0].Value != null)
                {
                    SharyouDialogReslut.GyoushaNameRyaku = gyoushaNameRyakuParam[0].Value.ToString();
                }

                // 車種CD
                var ShashuCdParam = popUpReturnParams[4];
                if (ShashuCdParam != null && ShashuCdParam[0].Value != null)
                {
                    SharyouDialogReslut.ShashuCD = ShashuCdParam[0].Value.ToString();
                }

                // 車種名称
                var ShashuNameRyakuParam = popUpReturnParams[5];
                if (ShashuNameRyakuParam != null && ShashuNameRyakuParam[0].Value != null)
                {
                    SharyouDialogReslut.ShashuNameRyaku = ShashuNameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #region 処分方法
        public struct ShobunHouhouDialogReslut
        {
            public string CD;
            public string NameRyaku;

            public ShobunHouhouDialogReslut(string cd, string nameRyaku)
            {
                this.CD = cd;
                this.NameRyaku = nameRyaku;
            }
        }
        /// <summary>
        /// 処分方法選択ポップアップ
        /// ShobunHouhouDialogReslutには処分方法選択ポップアップで表示される情報が設定される
        /// </summary>
        /// <param name="DefaultShobunHouhouCD">念のため定義</param>
        /// <param name="ShobunHouhouDialogReslut">ポップアップで選択した情報</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowShobunHouhouSentakuDialog(string DefaultShobunHouhouCD, out ShobunHouhouDialogReslut ShobunHouhouDialogReslut)
        {
            DialogResult result = DialogResult.None;
            ShobunHouhouDialogReslut = new ShobunHouhouDialogReslut(string.Empty, string.Empty);
            string popupGetMasterField = "SHOBUN_HOUHOU_CD, SHOBUN_HOUHOU_NAME_RYAKU";

            // ポップアップクラス作成
            var popupForm = PopupUtility.CreateMasterKyoutsuPopup2();
            if (popupForm == null)
            {
                return result;
            }

            Dictionary<int, List<PopupReturnParam>> popUpReturnParams;

            result = PopupUtility.ShowPopupWindow(null, null, popupForm, WINDOW_ID.M_SHOBUN_HOUHOU, null, null, popupGetMasterField, false, out popUpReturnParams);

            if (result == DialogResult.Cancel)
            {
                return result;
            }

            if (popUpReturnParams != null && 2 <= popUpReturnParams.Count)
            {
                // CD
                var cdParam = popUpReturnParams[0];
                if (cdParam != null && cdParam[0].Value != null)
                {
                    ShobunHouhouDialogReslut.CD = cdParam[0].Value.ToString();
                }

                // 名称
                var nameRyakuParam = popUpReturnParams[1];
                if (nameRyakuParam != null && nameRyakuParam[0].Value != null)
                {
                    ShobunHouhouDialogReslut.NameRyaku = nameRyakuParam[0].Value.ToString();
                }
            }

            return result;
        }
        #endregion

        #endregion

        #region Utility

        #region ポップアップクラス生成
        /// <summary>
        /// マスタ検索ポップアップ(MasterKyoutsuPopup2)を生成
        /// </summary>
        /// <returns></returns>
        private static SuperPopupForm CreateMasterKyoutsuPopup2()
        {
            SuperPopupForm returnValue = null;
            string assenmblyName = "MasterKyoutsuPopup2.dll";
            Assembly assenmly = null;

            try
            {
                assenmly = Assembly.LoadFrom(assenmblyName);
            }
            catch (Exception e)
            {
                // アセンブリが読み込めないエラーはdllが無いだけのはず
                LogUtility.Error(e.Message);
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }

            if (assenmly == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            var objectHandler = Activator.CreateInstanceFrom(assenmly.CodeBase, "MasterKyoutsuPopup2.APP.MasterKyoutsuPopupForm");
            if (objectHandler == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            returnValue = objectHandler.Unwrap() as SuperPopupForm;
            return returnValue;
        }

        /// <summary>
        /// 検索共通ポップアップ(KensakuKyoutsuuPopup)を生成
        /// </summary>
        /// <returns></returns>
        private static SuperPopupForm CreateKensakuKyoutsuuPopup()
        {
            SuperPopupForm returnValue = null;
            string assenmblyName = "KensakuKyoutsuuPopup.dll";
            Assembly assenmly = null;

            try
            {
                assenmly = Assembly.LoadFrom(assenmblyName);
            }
            catch (Exception e)
            {
                // アセンブリが読み込めないエラーはdllが無いだけのはず
                LogUtility.Error(e.Message);
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }

            if (assenmly == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            var objectHandler = Activator.CreateInstanceFrom(assenmly.CodeBase, "KensakuKyoutsuuPopup.APP.KensakuKyoutsuuPopupForm");
            if (objectHandler == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            returnValue = objectHandler.Unwrap() as SuperPopupForm;
            return returnValue;
        }

        /// <summary>
        /// 複数キー用検索共通ポップアップ(KensakuKyoutsuuPopupForMultiKey)を生成
        /// </summary>
        /// <returns></returns>
        private static SuperPopupForm CreateKensakuKyoutsuuPopupForMultiKey()
        {
            SuperPopupForm returnValue = null;
            string assenmblyName = "KensakuKyoutsuuPopupForMultiKey.dll";
            Assembly assenmly = null;

            try
            {
                assenmly = Assembly.LoadFrom(assenmblyName);
            }
            catch (Exception e)
            {
                // アセンブリが読み込めないエラーはdllが無いだけのはず
                LogUtility.Error(e.Message);
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }

            if (assenmly == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            var objectHandler = Activator.CreateInstanceFrom(assenmly.CodeBase, "KensakuKyoutsuuPopupForMultiKey.APP.KensakuKyoutsuuPopupForMultiKeyForm");
            if (objectHandler == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            returnValue = objectHandler.Unwrap() as SuperPopupForm;
            return returnValue;
        }

        /// <summary>
        /// 車両選択共通ポップアップ(SyaryoSentaku)を生成
        /// </summary>
        /// <returns></returns>
        private static SuperPopupForm CreateSyaryoSentaku()
        {
            SuperPopupForm returnValue = null;
            string assenmblyName = "SyaryoSentaku.dll";
            Assembly assenmly = null;

            try
            {
                assenmly = Assembly.LoadFrom(assenmblyName);
            }
            catch (Exception e)
            {
                // アセンブリが読み込めないエラーはdllが無いだけのはず
                LogUtility.Error(e.Message);
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }

            if (assenmly == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            var objectHandler = Activator.CreateInstanceFrom(assenmly.CodeBase, "SyaryoSentaku.App.UIForm");
            if (objectHandler == null)
            {
                PopupUtility.ShowErrorMessage(assenmblyName);
                return returnValue;
            }
            returnValue = objectHandler.Unwrap() as SuperPopupForm;
            return returnValue;
        }
        #endregion

        /// <summary>
        /// エラーメッセージ表示
        /// </summary>
        /// <param name="dllName"></param>
        private static void ShowErrorMessage(string dllName)
        {
            new r_framework.Logic.MessageBoxShowLogic().MessageBoxShowError("ポップアップを表示するモジュール(" + dllName + ")が不足しています。モジュールが存在するか、モジュール名が変更になっていないか確認してください。");
        }

        /// <summary>
        /// カスタムコントロールにてスペースキーを押下されたときに
        /// 設定されているプロパティから自動的にポップアップを特定し
        /// 起動する処理
        /// </summary>
        /// <param name="allControls">呼び出し画面の全コントロール。画面の値を絞込み条件に設定する場合に必要</param>
        /// <param name="param"></param>
        /// <param name="popupWindowId">画面ID。どのテーブルを参照するかに関わる情報</param>
        /// <param name="popupWindowSetting">ポップアップの絞り込み条件</param>
        /// <param name="popupSearchSendParams">ポップアップの絞り込み条件</param>
        /// <param name="popupGetMasterField">ポップアップでどのカラム情報を取得するかを指定する</param>
        /// <param name="popupMultiSelect">複数選択するかどうか</param>
        /// <param name="returnParams"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowPopupWindow(object[] allControls, object[] param, SuperPopupForm popupForm,
            WINDOW_ID popupWindowId, Collection<JoinMethodDto> popupWindowSetting, Collection<PopupSearchSendParamDto> popupSearchSendParams,
            string popupGetMasterField, bool popupMultiSelect, out Dictionary<int, List<PopupReturnParam>> returnParams)
        {
            DialogResult result = DialogResult.None;
            returnParams = new Dictionary<int, List<PopupReturnParam>>();

            if (popupForm != null)
            {
                popupForm.WindowId = popupWindowId;
                if (allControls != null)
                {
                    popupForm.ParentControls = allControls;
                }
                else
                {
                    popupForm.ParentControls = new object[] { null };
                }

                popupForm.IsMasterAccessStartUp = false;
                if (param != null)
                {
                    //送信対象パラメータ
                    popupForm.Params = param;
                }
                else
                {
                    popupForm.Params = new object[] { null };
                }

                // ポップアップの表示条件を設定
                if (popupWindowSetting != null)
                {
                    popupForm.popupWindowSetting = popupWindowSetting;
                }
                else
                {
                    popupForm.popupWindowSetting = new Collection<JoinMethodDto>();
                }

                if (popupSearchSendParams != null)
                {
                    popupForm.PopupSearchSendParams = popupSearchSendParams;
                }
                else
                {
                    popupForm.PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();
                }

                if (popupGetMasterField != null)
                {
                    popupForm.PopupGetMasterField = popupGetMasterField;
                }
                else
                {
                    popupForm.PopupGetMasterField = string.Empty;
                }

                popupForm.PopupMultiSelect = popupMultiSelect;

                popupForm.StartPosition = FormStartPosition.CenterParent;
                // ポップアップ表示
                result = popupForm.ShowDialog();
                returnParams = popupForm.ReturnParams;
                popupForm.Dispose();
            }

            return result;
        }
        #endregion

    }

}
