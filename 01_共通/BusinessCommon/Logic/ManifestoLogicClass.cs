using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Text.RegularExpressions;
using r_framework.Const;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class ManifestoLogic
    {
        #region フィールド

        /// <summary>拠点マスタDao</summary>
        private IM_KYOTENDao mkyotenDao;

        /// <summary>業者マスタ検索Dao</summary>
        private CommonGyoushaDaoCls GyoushaDao;

        /// <summary>現場マスタ検索Dao</summary>
        private CommonGenbaDaoCls GenbaDao;

        /// <summary>現場マスタ(全業者版)検索Dao</summary>
        private CommonGenbaAllDaoCls GenbaAllDao;

        /// <summary>混合廃棄物マスタ検索Dao</summary>
        private CommonKongouHaikibutuDaoCls KongouHaikibutuDao;

        /// <summary>単位マスタ検索Dao</summary>
        private CommonUnitDaoCls UnitDao;

        /// <summary>処分担当者Dao</summary>
        private CommonShobunTantoushaDaoCls ShobunTantoushaDaoCls;

        /// <summary>換算Dao</summary>
        private CommonKansanDaoCls KansanDataDao;

        /// <summary>減容Dao</summary>
        private CommonGenyouDaoCls GenyouDataDao;

        /// <summary>マニフェストパターン検索Dao</summary>
        private CommonPtEntryDaoCls PtEntryDao;

        /// <summary>マニ印字明細パターン検索Dao</summary>
        private CommonPtDetailPrtDaoCls PtDetailPrtDao;

        /// <summary>マニ明細パターン検索Dao</summary>
        private CommonPtDetailDaoCls PtDetailDao;

        /// <summary>マニフェスト検索Dao</summary>
        private CommonEntryDaoCls EntryDao;

        /// <summary>マニフェスト削除Dao</summary>
        private CommonEntryDelDaoCls EntryDelDao;

        /// <summary>マニフェスト更新Dao</summary>
        private CommonUpnDaoCls UpnDao;

        /// <summary>マニ印字更新Dao</summary>
        private CommonPrtDaoCls PrtDao;

        /// <summary>マニ印字明細更新Dao</summary>
        private CommonDetailPrtDaoCls DetailPrtDao;

        /// <summary>マニ印字_産廃_形状更新Dao</summary>
        private CommonKeijyouDaoCls KeijyouDao;

        /// <summary>マニ印字_産廃_荷姿更新Dao</summary>
        private CommonNisugataDaoCls NisugataDao;

        /// <summary>マニ印字_産廃_処分方法更新Dao</summary>
        private CommonHouhouDaoCls HouhouDao;

        /// <summary>マニ明細更新Dao</summary>
        private CommonDetailDaoCls DetailDao;

        /// <summary>マニフェスト更新Dao</summary>
        private CommonRetDateDaoCls RetDateDao;

        /// <summary>マニフェスト削除Dao</summary>
        private CommonRetDateDelDaoCls RetDateDelDao;

        /// <summary>マニフェスト紐付検索用Dao</summary>
        private GetManifestRelationDaoCls GetManiRelDao;

        /// <summary>
        /// 郵便辞書マスタのDao
        /// </summary>
        private IS_ZIP_CODEDao zipCodeDao;

        // 20150914 katen #12048 「システム日付」の基準作成、適用 start
        public DateTime sysDate;

        private GET_SYSDATEDao dateDao;
        // 20150914 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ManifestoLogic()
        {
            LogUtility.DebugMethodStart();

            //マスタ検索
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<CommonGyoushaDaoCls>();
            this.GenbaDao = DaoInitUtility.GetComponent<CommonGenbaDaoCls>();
            this.GenbaAllDao = DaoInitUtility.GetComponent<CommonGenbaAllDaoCls>();
            this.KongouHaikibutuDao = DaoInitUtility.GetComponent<CommonKongouHaikibutuDaoCls>();
            this.UnitDao = DaoInitUtility.GetComponent<CommonUnitDaoCls>();
            this.ShobunTantoushaDaoCls = DaoInitUtility.GetComponent<CommonShobunTantoushaDaoCls>();

            //計算
            this.KansanDataDao = DaoInitUtility.GetComponent<CommonKansanDaoCls>();
            this.GenyouDataDao = DaoInitUtility.GetComponent<CommonGenyouDaoCls>();

            //マニフェストパターン
            this.PtEntryDao = DaoInitUtility.GetComponent<CommonPtEntryDaoCls>();
            this.PtDetailPrtDao = DaoInitUtility.GetComponent<CommonPtDetailPrtDaoCls>();
            this.PtDetailDao = DaoInitUtility.GetComponent<CommonPtDetailDaoCls>();

            //マニフェスト
            this.EntryDelDao = DaoInitUtility.GetComponent<CommonEntryDelDaoCls>();
            this.EntryDao = DaoInitUtility.GetComponent<CommonEntryDaoCls>();
            this.UpnDao = DaoInitUtility.GetComponent<CommonUpnDaoCls>();
            this.PrtDao = DaoInitUtility.GetComponent<CommonPrtDaoCls>();
            this.DetailPrtDao = DaoInitUtility.GetComponent<CommonDetailPrtDaoCls>();
            this.KeijyouDao = DaoInitUtility.GetComponent<CommonKeijyouDaoCls>();
            this.NisugataDao = DaoInitUtility.GetComponent<CommonNisugataDaoCls>();
            this.HouhouDao = DaoInitUtility.GetComponent<CommonHouhouDaoCls>();
            this.DetailDao = DaoInitUtility.GetComponent<CommonDetailDaoCls>();
            this.RetDateDao = DaoInitUtility.GetComponent<CommonRetDateDaoCls>();
            this.RetDateDelDao = DaoInitUtility.GetComponent<CommonRetDateDelDaoCls>();
            this.GetManiRelDao = DaoInitUtility.GetComponent<GetManifestRelationDaoCls>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.zipCodeDao = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 拠点の初期設定
        /// </summary>
        public void SetKyoten(object ctxt_KyotenCd, object ctxt_KyotenName)
        {
            LogUtility.DebugMethodStart(ctxt_KyotenCd, ctxt_KyotenName);

            TextBox txt_KyotenCd = (TextBox)ctxt_KyotenCd;
            TextBox txt_KyotenName = (TextBox)ctxt_KyotenName;

            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            //ヘッダに表示予定の値
            string KyotenCd = string.Empty;
            if (configProfile.ItemSetVal1.Length == 1)
            {
                KyotenCd = "0" + configProfile.ItemSetVal1;
            }
            else
            {
                KyotenCd = configProfile.ItemSetVal1;
            }
            txt_KyotenCd.Text = KyotenCd;

            // ユーザ拠点名称の取得
            M_KYOTEN mKyoten = new M_KYOTEN();
            mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(KyotenCd);
            if (mKyoten == null)
            {
                txt_KyotenName.Text = string.Empty;
            }
            else
            {
                txt_KyotenName.Text = mKyoten.KYOTEN_NAME_RYAKU;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者マスタ検索
        /// </summary>
        [Transaction]
        public virtual DataTable GetGyousha(CommonGyoushaDtoCls SearchCondition)
        {
            return this.GyoushaDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 業者マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_gyoshaName">業者名</param>
        /// <param name="txt_gyoshaPost">郵便番号</param>
        /// <param name="txt_gyoshaTel">電話番号</param>
        /// <param name="txt_gyoshaAdr">住所</param>
        /// <param name="TenkiNameFlg">転記先 業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_TenkiGyoshaCd">転記先 業者CD</param>
        /// <param name="txt_TenkiGyoshaName">転記先 業者名</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">削除チェックいるかどうかの判断フラッグ</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        [Obsolete("staticなShougun.Core.Common.BusinessCommon.Logic.ManifestoLogic.SetAddrGyoushaを利用するようにしてください")]
        public int SetAddressGyousha(
            string NameFlg,
            CustomTextBox txt_gyoshaCd, CustomTextBox txt_gyoshaName, CustomTextBox txt_gyoshaPost, CustomTextBox txt_gyoshaTel, CustomTextBox txt_gyoshaAdr,
            string TenkiNameFlg,
            CustomTextBox txt_TenkiGyoshaCd, CustomTextBox txt_TenkiGyoshaName,
            bool HAISHUTSU_NIZUMI_GYOUSHA_KBN,
            bool SHOBUN_NIOROSHI_GYOUSHA_KBN,
            bool UNPAN_JUTAKUSHA_KAISHA_KBN,
            bool TSUMIKAEHOKAN_KBN,
            bool ISNOT_NEED_DELETE_FLG = false)
        {
            //処理を転送するのみ
            return SetAddrGyousha(
                NameFlg, txt_gyoshaCd, txt_gyoshaName, txt_gyoshaPost, txt_gyoshaTel, txt_gyoshaAdr,
                TenkiNameFlg, txt_TenkiGyoshaCd, txt_TenkiGyoshaName,
                HAISHUTSU_NIZUMI_GYOUSHA_KBN, SHOBUN_NIOROSHI_GYOUSHA_KBN, UNPAN_JUTAKUSHA_KAISHA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);
        }

        /// <summary>
        /// 業者マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_gyoshaName">業者名</param>
        /// <param name="txt_gyoshaPost">郵便番号</param>
        /// <param name="txt_gyoshaTel">電話番号</param>
        /// <param name="txt_gyoshaAdr">住所</param>
        /// <param name="TenkiNameFlg">転記先 業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_TenkiGyoshaCd">転記先 業者CD</param>
        /// <param name="txt_TenkiGyoshaName">転記先 業者名</param>
        /// <param name="HAISHUTSU_NIZUMI_GYOUSHA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GYOUSHA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="UNPAN_JUTAKUSHA_KAISHA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">削除チェックいるかどうかの判断フラッグ</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public static int SetAddrGyousha(
            string NameFlg,
            CustomTextBox txt_gyoshaCd, CustomTextBox txt_gyoshaName, CustomTextBox txt_gyoshaPost, CustomTextBox txt_gyoshaTel, CustomTextBox txt_gyoshaAdr,
            string TenkiNameFlg,
            CustomTextBox txt_TenkiGyoshaCd, CustomTextBox txt_TenkiGyoshaName,
            bool HAISHUTSU_NIZUMI_GYOUSHA_KBN,
            bool SHOBUN_NIOROSHI_GYOUSHA_KBN,
            bool UNPAN_JUTAKUSHA_KAISHA_KBN,
            bool TSUMIKAEHOKAN_KBN,
            bool ISNOT_NEED_DELETE_FLG = false)
        {
            LogUtility.DebugMethodStart(
                NameFlg, txt_gyoshaCd, txt_gyoshaName, txt_gyoshaPost, txt_gyoshaTel, txt_gyoshaAdr,
                TenkiNameFlg, txt_TenkiGyoshaCd, txt_TenkiGyoshaName,
                HAISHUTSU_NIZUMI_GYOUSHA_KBN, SHOBUN_NIOROSHI_GYOUSHA_KBN, UNPAN_JUTAKUSHA_KAISHA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

            //空
            if (txt_gyoshaCd.Text == string.Empty)
            {
                LogUtility.DebugMethodEnd(1);
                return 1;
            }

            //エラー
            var Serch = new CommonGyoushaDtoCls();
            Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
            Serch.GYOUSHAKBN_MANI = "1";
            Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

            //区分セット
            if (HAISHUTSU_NIZUMI_GYOUSHA_KBN)
            {
                Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
            }
            if (SHOBUN_NIOROSHI_GYOUSHA_KBN)
            {
                Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
            }
            if (UNPAN_JUTAKUSHA_KAISHA_KBN)
            {
                Serch.UNPAN_JUTAKUSHA_KAISHA_KBN = "1";
            }
            if (TSUMIKAEHOKAN_KBN)
            {
                Serch.TSUMIKAEHOKAN_KBN = "1";
            }

            var GyoushaDao = DaoInitUtility.GetComponent<CommonGyoushaDaoCls>();
            DataTable dt = GyoushaDao.GetDataForEntity(Serch);
            if (dt.Rows.Count == 0)
            {
                LogUtility.DebugMethodEnd(2);
                return 2;
            }

            //名称
            if (txt_gyoshaName != null)
            {
                switch (NameFlg)
                {
                    case "All":
                        //「正式名称1 + 正式名称2」をセットする。
                        txt_gyoshaName.Text = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                        break;

                    case "Part1":
                        //「正式名称1」をセットする。
                        txt_gyoshaName.Text = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                        break;

                    case "Ryakushou_Name":
                        //「略称名」をセットする。
                        txt_gyoshaName.Text = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                        break;

                    default:
                        break;
                }
            }

            //郵便番号
            if (txt_gyoshaPost != null)
            {
                txt_gyoshaPost.Text = dt.Rows[0]["GYOUSHA_POST"].ToString();
            }

            //電話番号
            if (txt_gyoshaTel != null)
            {
                txt_gyoshaTel.Text = dt.Rows[0]["GYOUSHA_TEL"].ToString();
            }

            //住所
            if (txt_gyoshaAdr != null)
            {
                txt_gyoshaAdr.Text = dt.Rows[0]["GYOUSHA_ADDRESS"].ToString();
            }

            //転記先 業者CD
            if (txt_TenkiGyoshaCd != null)
            {
                txt_TenkiGyoshaCd.Text = txt_gyoshaCd.Text;
            }

            //転記先 業者名
            if (txt_TenkiGyoshaName != null)
            {
                switch (TenkiNameFlg)
                {
                    case "All"://「正式名称1 + 正式名称2」をセットする。
                        txt_TenkiGyoshaName.Text = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                        break;

                    case "Part1"://「正式名称1」をセットする。
                        txt_TenkiGyoshaName.Text = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                        break;

                    default:
                        break;
                }
            }

            LogUtility.DebugMethodEnd(0);
            return 0;
        }

        /// <summary>
        /// 現場マスタ検索
        /// </summary>
        [Transaction]
        public virtual DataTable GetGenba(CommonGenbaDtoCls SearchCondition)
        {
            return this.GenbaDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 現場マスタ検索(全業者版)
        /// </summary>
        [Transaction]
        public virtual DataTable GetGenbaAll(CommonGenbaDtoCls SearchCondition)
        {
            return this.GenbaAllDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 現場マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">現場名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_JigyoubaCd">現場CD</param>
        /// <param name="txt_JigyoubaName">現場名</param>
        /// <param name="txt_JigyoubaPost">郵便番号</param>
        /// <param name="txt_JigyoubaTel">電話番号</param>
        /// <param name="txt_JigyoubaAdr">住所</param>
        /// <param name="txt_JigyoubaShobunNo">処分No.</param>
        /// <param name="HAISHUTSU_NIZUMI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SAISHUU_SHOBUNJOU_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetAddressJigyouba(
            string NameFlg,
            CustomTextBox txt_gyoshaCd, CustomTextBox txt_JigyoubaCd, CustomTextBox txt_JigyoubaName,
            CustomTextBox txt_JigyoubaPost, CustomTextBox txt_JigyoubaTel, CustomTextBox txt_JigyoubaAdr, CustomTextBox txt_JigyoubaShobunNo,
            bool HAISHUTSU_NIZUMI_GENBA_KBN,
            bool SAISHUU_SHOBUNJOU_KBN,
            bool SHOBUN_NIOROSHI_GENBA_KBN,
            bool TSUMIKAEHOKAN_KBN,
            bool ISNOT_NEED_DELETE_FLG = false)
        {
            LogUtility.DebugMethodStart(
                NameFlg, txt_gyoshaCd, txt_JigyoubaCd, txt_JigyoubaName, txt_JigyoubaPost, txt_JigyoubaTel, txt_JigyoubaAdr, txt_JigyoubaShobunNo,
                HAISHUTSU_NIZUMI_GENBA_KBN, SAISHUU_SHOBUNJOU_KBN, SHOBUN_NIOROSHI_GENBA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

            //空
            if (txt_gyoshaCd.Text == string.Empty || txt_JigyoubaCd.Text == string.Empty)
            {
                LogUtility.DebugMethodEnd(1);
                return 1;
            }

            var Serch = new CommonGenbaDtoCls();
            Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
            Serch.GENBA_CD = txt_JigyoubaCd.Text;
            Serch.GYOUSHAKBN_MANI = "1";
            Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

            //区分
            if (HAISHUTSU_NIZUMI_GENBA_KBN)
            {
                Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "1";
            }
            if (SAISHUU_SHOBUNJOU_KBN)
            {
                Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
            }
            if (SHOBUN_NIOROSHI_GENBA_KBN)
            {
                Serch.SHOBUN_NIOROSHI_GENBA_KBN = "1";
                Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
            }
            if (TSUMIKAEHOKAN_KBN)
            {
                Serch.TSUMIKAEHOKAN_KBN = "1";
            }

            DataTable dt = this.GenbaDao.GetDataForEntity(Serch);
            switch (dt.Rows.Count)
            {
                case 1://正常
                    break;

                default://エラー
                    LogUtility.DebugMethodEnd(2);
                    return 2;
            }

            //現場名
            if (txt_JigyoubaName != null)
            {
                switch (NameFlg)
                {
                    case "All"://「正式名称1 + 正式名称2」をセットする。
                        txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME"].ToString();
                        break;

                    case "Part1"://「正式名称1」をセットする。
                        txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                        break;

                    default:
                        break;
                }
            }

            //郵便番号
            if (txt_JigyoubaPost != null)
            {
                txt_JigyoubaPost.Text = dt.Rows[0]["GENBA_POST"].ToString();
            }

            //電話番号
            if (txt_JigyoubaTel != null)
            {
                txt_JigyoubaTel.Text = dt.Rows[0]["GENBA_TEL"].ToString();
            }

            //住所
            if (txt_JigyoubaAdr != null)
            {
                txt_JigyoubaAdr.Text = dt.Rows[0]["GENBA_ADDRESS"].ToString();
            }

            //処分No.
            if (txt_JigyoubaShobunNo != null)
            {
                txt_JigyoubaShobunNo.Text = dt.Rows[0]["SHOBUNSAKI_NO"].ToString();
            }

            LogUtility.DebugMethodEnd(0);
            return 0;
        }

        /// <summary>
        /// 廃棄物種類マスタ検索
        /// </summary>
        [Transaction]
        public virtual DataTable GetKongouHaikibutu(CommonKongouHaikibutsuDtoCls SearchCondition)
        {
            return this.KongouHaikibutuDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 単位マスタ検索
        /// </summary>
        [Transaction]
        public virtual DataTable GetUnit(CommonUnitDtoCls SearchCondition)
        {
            return this.UnitDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 処分担当者検索
        /// </summary>
        /// <param name="shaincd">社員CD</param>
        /// <param name="shainname">社員名</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetShobunTantousha(CustomTextBox txt_shaincd, CustomTextBox txt_shainname)
        {
            LogUtility.DebugMethodStart(txt_shaincd, txt_shainname);

            if (txt_shaincd.Text == string.Empty)
            {
                txt_shainname.Text = string.Empty;
                LogUtility.DebugMethodEnd(1);
                return 1;
            }

            CommonShobunTantoushaDtoCls Serch = new CommonShobunTantoushaDtoCls();
            Serch.SHAIN_CD = txt_shaincd.Text;

            DataTable dt = ShobunTantoushaDaoCls.GetDataForEntity(Serch);
            switch (dt.Rows.Count)
            {
                case 0://エラー
                    LogUtility.DebugMethodEnd(2);
                    return 2;

                default://正常
                    break;
            }

            //社員名
            if (txt_shainname != null)
            {
                txt_shainname.Text = dt.Rows[0]["SHAIN_NAME"].ToString();
            }
            LogUtility.DebugMethodEnd(0);
            return 0;
        }

        /// <summary>
        /// 数量換算のデータ取得処理
        /// </summary>
        /// <returns></returns>
        public DataTable GetKansanData(CommonKanSanDtoCls SearchCondition)
        {
            return this.KansanDataDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 数量減容のデータ取得処理
        /// </summary>
        /// <returns></returns>
        public DataTable GetGenyouData(CommonKanSanDtoCls SearchCondition)
        {
            return this.GenyouDataDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// マニフェストパターンのデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchPtData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.PtEntryDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// マニフェストパターン印字明細のデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchPtDetailPrtData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.PtDetailPrtDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// マニフェストパターン明細のデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchPtDetailData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.PtDetailDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// マニフェストのデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.EntryDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// 交付番号存在チェック
        /// </summary>
        public Boolean ExistKohuNo(string HaikiKbnCD, string ManifestId, ref string SystemId, ref string Seq, ref string SeqRD)
        {
            LogUtility.DebugMethodStart(HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);

            if (HaikiKbnCD == string.Empty || ManifestId == string.Empty)
            {
                LogUtility.DebugMethodEnd(HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);
                return true;
            }

            //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
            var Search = new CommonSerchParameterDtoCls();
            Search.MANIFEST_ID = ManifestId;
            Search.HAIKI_KBN_CD = HaikiKbnCD;

            DataTable dt = this.EntryDao.GetDataForEntity(Search);
            if (dt.Rows.Count == 0)
            {
                LogUtility.DebugMethodEnd(HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);
                return true;
            }

            SystemId = dt.Rows[0]["SYSTEM_ID"].ToString();
            Seq = dt.Rows[0]["SEQ"].ToString();
            SeqRD = dt.Rows[0]["TMRD_SEQ"].ToString();

            LogUtility.DebugMethodEnd(HaikiKbnCD, ManifestId, SystemId, Seq, SeqRD);
            return false;
        }

        /// <summary>
        /// 登録前チェック
        /// </summary>
        public Boolean RegistrationCheck(string HaikiKbnCD, string ManifestId)
        {
            LogUtility.DebugMethodStart(HaikiKbnCD, ManifestId);

            if (HaikiKbnCD == string.Empty || ManifestId == string.Empty)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            //マニフェスト一式（マニ明細、マニ印字明細除く）データ読み込み
            var Search = new CommonSerchParameterDtoCls();
            Search.MANIFEST_ID = ManifestId;
            Search.HAIKI_KBN_CD = HaikiKbnCD;

            DataTable dt = this.EntryDao.GetDataForEntity(Search);
            if (dt.Rows.Count == 0)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// マニフェスト印字明細のデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchDetailPrtData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.DetailPrtDao.GetDataForEntity(SearchCondition);
        }

        /// <summary>
        /// マニフェスト明細のデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchDetailData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.DetailDao.GetDataForEntity(SearchCondition);
        }

        // 20140710 ria EV005141 2次マニを修正モードにて登録しても紐づいた1次マニが更新されない start
        /// <summary>
        /// マニフェスト明細のデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchMaxSyobunDate(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.DetailDao.GetSyobunDate(SearchCondition);
        }

        /// <summary>
        /// マニフェスト明細の最大最終処分日取得
        /// </summary>
        public string GetMaxShobunDate(string system_id, string haikiKbnCD)
        {
            LogUtility.DebugMethodStart(system_id, haikiKbnCD);

            string ret = string.Empty;
            var Search = new CommonSerchParameterDtoCls();
            Search.SYSTEM_ID = system_id;
            Search.HAIKI_KBN_CD = haikiKbnCD;

            //マニ明細(T_MANIFEST_DETAIL)データ読み込み
            DataTable syobunDate = this.SearchMaxSyobunDate(Search);
            if (syobunDate.Rows.Count > 0)
            {
                ret = syobunDate.Rows[0]["LAST_SBN_END_DATE"].ToString();
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 二次マニフェスト明細のデータ取得
        /// </summary>
        [Transaction]
        public virtual DataTable SearchDetailSyobunData(CommonSerchParameterDtoCls SearchCondition)
        {
            return this.DetailDao.GetDetailSyobunData(SearchCondition);
        }

        /// <summary>
        /// 二次マニフェスト明細の最終処分業者と場所取得
        /// </summary>
        public DataTable GetDetailSyobunGyouBanData(string system_id, string haikiKbnCD)
        {
            LogUtility.DebugMethodStart(system_id, haikiKbnCD);

            DataTable returnDate = new DataTable();
            var Search = new CommonSerchParameterDtoCls();
            Search.SYSTEM_ID = system_id;
            Search.HAIKI_KBN_CD = haikiKbnCD;

            //マニ明細(T_MANIFEST_DETAIL)データ読み込み
            DataTable syobunDate = this.SearchDetailSyobunData(Search);
            if (syobunDate.Rows.Count > 0)
            {
                returnDate = syobunDate;
            }
            LogUtility.DebugMethodEnd();
            return returnDate;
        }

        /// <summary>
        /// 1次マニに1次マニを紐付けたとき、1次マニのデータ取得
        /// </summary>
        public DataTable SelectFirstManiSystemID(string system_id, string haikiKbnCD)
        {
            LogUtility.DebugMethodStart(system_id, haikiKbnCD);

            var returnDate = new DataTable();

            //マニRELATION(T_MANIFEST_RELATION)データ読み込み
            returnDate = this.GetManiRelDao.GetFirstManiInfo(SqlInt64.Parse(system_id), SqlInt16.Parse(haikiKbnCD));

            LogUtility.DebugMethodEnd();
            return returnDate;
        }

        /// <summary>
        /// 1次マニの最終処分の更新(二次が紙マニの場合のみ)
        /// </summary>
        public void UpdateFirstManiDetail(string system_id, string haikiKbnCD)
        {
            LogUtility.DebugMethodStart(system_id, haikiKbnCD);

            #region 電マニ用Dao生成

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

            var firstManiDetail = new T_MANIFEST_DETAIL();

            var firstManiData = new DataTable();
            firstManiData = this.SelectFirstManiSystemID(system_id, haikiKbnCD);

            if (firstManiData.Rows.Count > 0)
            {
                //二次の最終処分データを取得
                string ret = this.GetMaxShobunDate(system_id, haikiKbnCD);

                DataTable dtData = this.GetDetailSyobunGyouBanData(system_id, haikiKbnCD);

                // 一次電マニの重複更新防止用変数
                var executedKanriIds = new List<string>();

                for (int i = 0; i < firstManiData.Rows.Count; i++)
                {
                    if (CommonConst.RELATIION_HAIKI_KBN_CD_DENSHI.ToString().Equals(firstManiData.Rows[i]["FIRST_HAIKI_KBN_CD"].ToString()))
                    {
                        #region 一次電マニ

                        #region チェック処理

                        if (firstManiData.Rows[i]["KANRI_ID"] == null
                            || string.IsNullOrEmpty(firstManiData.Rows[i]["KANRI_ID"].ToString())
                            || firstManiData.Rows[i]["LATEST_SEQ"] == null
                            || string.IsNullOrEmpty(firstManiData.Rows[i]["LATEST_SEQ"].ToString())
                            || firstManiData.Rows[i]["EX_SEQ"] == null
                            || string.IsNullOrEmpty(firstManiData.Rows[i]["EX_SEQ"].ToString())
                            )
                        {
                            // この行に入るときは、SQLがいけてないか、DBのデータがおかしい
                            continue;
                        }

                        if (executedKanriIds.Contains(firstManiData.Rows[i]["KANRI_ID"].ToString()))
                        {
                            // 処理済み
                            continue;
                        }
                        else
                        {
                            executedKanriIds.Add(firstManiData.Rows[i]["KANRI_ID"].ToString());
                        }

                        #endregion

                        // 一次電マニの場合は紙マニと違い、一括で紐付いている二次マニを参照してDT_R13, DT_R13_EXを作成しないとならい
                        string kanriId = firstManiData.Rows[i]["KANRI_ID"].ToString();
                        string manifestId = firstManiData.Rows[i]["MANIFEST_ID"].ToString();
                        SqlInt32 latestSeq = SqlInt32.Parse(firstManiData.Rows[i]["LATEST_SEQ"].ToString());
                        SqlInt32 exSystemid = SqlInt32.Parse(firstManiData.Rows[i]["EX_SYSTEM_ID"].ToString());
                        SqlInt32 exSeq = SqlInt32.Parse(firstManiData.Rows[i]["EX_SEQ"].ToString());

                        #region 現在のデータを取得

                        DT_MF_TOC mfToc = mfTocDao.GetDataForEntity(new DT_MF_TOC() { KANRI_ID = kanriId });
                        DT_R18 r18 = r18Dao.GetDataForEntity(new DT_R18() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R19[] r19 = r19Dao.GetDataForEntity(new DT_R19() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R02[] r02 = r02Dao.GetDataForEntity(new DT_R02() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R04[] r04 = r04Dao.GetDataForEntity(new DT_R04() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R05[] r05 = r05Dao.GetDataForEntity(new DT_R05() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R08[] r08 = r08Dao.GetDataForEntity(new DT_R08() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R13[] r13 = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = kanriId, SEQ = latestSeq });
                        DT_R18_EX oldR18Ex = r18ExDao.GetDataForEntity(new DT_R18_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                        DT_R19_EX[] oldR19Ex = r19ExDao.GetDataForEntity(new DT_R19_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                        DT_R04_EX[] oldR04Ex = r04ExDao.GetDataForEntity(new DT_R04_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                        DT_R08_EX[] oldR08Ex = r08ExDao.GetDataForEntity(new DT_R08_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });
                        DT_R13_EX[] oldR13Ex = r13ExDao.GetDataForEntity(new DT_R13_EX() { SYSTEM_ID = exSystemid, SEQ = exSeq });

                        #endregion

                        if (mfToc.KIND.IsNull
                            || mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)
                        {
                            // 一次電マニがNot EDI(手動)以外の場合、最終処分情報は更新しない。
                            // Not EDI(手動)以外の場合は[3] 最終処分終了報告ボタンから最終処分情報を更新する。
                            continue;
                        }

                        SqlDecimal updateLatestSeq = mfToc.LATEST_SEQ + 1;
                        SqlInt32 updateExSeq = exSeq + 1;

                        #region DT_R13, DT_R13_EXを追加

                        // 二次マニ全件取得
                        DataTable nextManis = new DataTable();
                        nextManis = this.GetManiRelDao.GetLastSbnInfoForNexttMani(SqlInt64.Parse(firstManiData.Rows[i]["FIRST_SYSTEM_ID"].ToString()));

                        // 業者、現場の一覧を生成
                        var gyoushaAndGenbaList = nextManis.AsEnumerable().Select(result => new
                                                    {
                                                        SECOND_HAIKI_KBN_CD = result.Field<int>("SECOND_HAIKI_KBN_CD"),
                                                        SECOND_SYS_ID = result.Field<long>("SECOND_SYSTEM_ID"),
                                                        SECOND_DETAIL_SYS_ID = result.Field<decimal>("SECOND_DETAIL_SYSTEM_ID"),
                                                        LAST_SBN_JOU_NAME = result.Field<string>("LAST_SBN_JOU_NAME"),
                                                        LAST_SBN_JOU_ADDRESS = result.Field<string>("LAST_SBN_JOU_ADDRESS")
                                                    }).
                                                    GroupBy(gryoup => new { gryoup.SECOND_HAIKI_KBN_CD, gryoup.SECOND_SYS_ID, gryoup.SECOND_DETAIL_SYS_ID, gryoup.LAST_SBN_JOU_NAME, gryoup.LAST_SBN_JOU_ADDRESS });

                        int recSeq = 1;
                        // DT_R13_EX用のCreate情報をセット
                        string createUser = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_USER;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? DateTime.Now : oldR13Ex[0].CREATE_DATE;
                        SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? this.getDBDateTime() : oldR13Ex[0].CREATE_DATE;
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        string createPc = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_PC;
                        DateTime lastSbnEndDate = DateTime.MinValue;

                        // 業者、現場毎に最終処分終了報告情報を生成
                        foreach (var gyoushaAndGenbaRow in gyoushaAndGenbaList)
                        {
                            if (string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME)
                                || string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS))
                            {
                                // 最終処分場がない場合、データの作りようがないので、除外
                                continue;
                            }

                            // SQL Injectionが発生する可能性があるので、予約文字をエスケープ
                            string lastSbnJouName = Regex.Replace(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME.Replace("'", "''"), @"([\[\]*%])", "[$1]");
                            string lastSbnJouAddress = Regex.Replace(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS.Replace("'", "''"), @"([\[\]*%])", "[$1]");

                            DateTime tempLastSbnEndDate = DateTime.MinValue;
                            var groupData = nextManis.Select(string.Format(
                                "SECOND_HAIKI_KBN_CD = {0} AND SECOND_SYSTEM_ID = {1} AND SECOND_DETAIL_SYSTEM_ID = {2} AND LAST_SBN_JOU_NAME = '{3}' AND LAST_SBN_JOU_ADDRESS = '{4}'",
                                gyoushaAndGenbaRow.Key.SECOND_HAIKI_KBN_CD, gyoushaAndGenbaRow.Key.SECOND_SYS_ID, gyoushaAndGenbaRow.Key.SECOND_DETAIL_SYS_ID,
                                lastSbnJouName, lastSbnJouAddress)
                                );

                            foreach (var tempRow in groupData)
                            {
                                if (tempRow["LAST_SBN_END_DATE"] == null
                                    || string.IsNullOrEmpty(tempRow["LAST_SBN_END_DATE"].ToString()))
                                {
                                    // 最終処分終了日が設定されていないものがあれば最終処分未完了
                                    tempLastSbnEndDate = DateTime.MinValue;
                                    break;
                                }

                                // 一番新しい日付をセット
                                if (DateTime.Compare(tempLastSbnEndDate, DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString())) < 0)
                                {
                                    tempLastSbnEndDate = DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString());
                                }
                            }

                            //
                            var firstManiR13 = new DT_R13();
                            var firstManiR13EX = new DT_R13_EX();

                            // 住所分割
                            string tempAddress1;
                            string tempAddress2;
                            string tempAddress3;
                            string tempAddress4;
                            this.SetAddress1ToAddress4(groupData[0].Field<string>("LAST_SBN_JOU_ADDRESS"),
                                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                            // set keys
                            firstManiR13.KANRI_ID = kanriId;
                            firstManiR13.SEQ = updateLatestSeq;
                            firstManiR13.REC_SEQ = recSeq;
                            firstManiR13EX.KANRI_ID = kanriId;
                            firstManiR13EX.SYSTEM_ID = exSystemid;
                            firstManiR13EX.SEQ = updateExSeq;
                            firstManiR13EX.REC_SEQ = recSeq;

                            // DT_R18.LAST_SBN_END_DATE用の日付
                            lastSbnEndDate = DateTime.Compare(lastSbnEndDate, tempLastSbnEndDate) < 0 ? tempLastSbnEndDate : lastSbnEndDate;

                            // DT_R13
                            firstManiR13.LAST_SBN_END_DATE = tempLastSbnEndDate.Equals(DateTime.MinValue) ? null : tempLastSbnEndDate.ToString("yyyyMMdd");
                            firstManiR13.MANIFEST_ID = manifestId;
                            firstManiR13.LAST_SBN_JOU_NAME = groupData[0].Field<string>("LAST_SBN_JOU_NAME");
                            firstManiR13.LAST_SBN_JOU_POST = groupData[0].Field<string>("LAST_SBN_JOU_POST");
                            firstManiR13.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                            firstManiR13.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                            firstManiR13.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                            firstManiR13.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                            firstManiR13.LAST_SBN_JOU_TEL = groupData[0].Field<string>("LAST_SBN_JOU_TEL");
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            DateTime now = this.getDBDateTime();
                            //firstManiR13.CREATE_DATE = DateTime.Now;
                            //firstManiR13.UPDATE_TS = DateTime.Now;
                            firstManiR13.CREATE_DATE = now;
                            firstManiR13.UPDATE_TS = now;
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                            // DT_R13_EX
                            firstManiR13EX.MANIFEST_ID = manifestId;
                            firstManiR13EX.LAST_SBN_GYOUSHA_CD = groupData[0].Field<string>("LAST_SBN_GYOUSHA_CD");
                            firstManiR13EX.LAST_SBN_GENBA_CD = groupData[0].Field<string>("LAST_SBN_GENBA_CD");
                            firstManiR13EX.CREATE_USER = createUser;
                            firstManiR13EX.CREATE_DATE = createDate;
                            firstManiR13EX.CREATE_PC = createPc;
                            firstManiR13EX.UPDATE_USER = SystemProperty.UserName;
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                            //firstManiR13EX.UPDATE_DATE = DateTime.Now;
                            firstManiR13EX.UPDATE_DATE = now;
                            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                            firstManiR13EX.UPDATE_PC = SystemInformation.ComputerName;
                            firstManiR13EX.DELETE_FLG = false;

                            // DT_R13, DT_R13_EX追加
                            r13Dao.Insert(firstManiR13);
                            r13ExDao.Insert(firstManiR13EX);

                            recSeq++;
                        }

                        // DT_R18のLAST_SBNEND_REP_FLGを一番最後にチェックするため、DT_R18の追加も一番最後に行う
                        decimal lastSbnEndRepFlg = 0;
                        var blankLastSbnJou = nextManis.Select("ISNULL(LAST_SBN_JOU_NAME, '') = '' OR ISNULL(LAST_SBN_JOU_ADDRESS, '') = ''");
                        var blankLstSbnEndDate = nextManis.Select("LAST_SBN_END_DATE IS NULL");
                        lastSbnEndRepFlg = (nextManis.Rows.Count > 0 && blankLastSbnJou.Count() < 1 && blankLstSbnEndDate.Count() < 1) ? 1 : 0;

                        #region 現在の電マニデータを更新、拡張データを論理削除

                        // DT_R18, DT_R19, DT_R02, DT_R04, DT_R05, DT_R08, DT_R13
                        if (mfToc != null)
                        {
                            mfToc.LATEST_SEQ = updateLatestSeq;
                            mfTocDao.Update(mfToc);
                            if (r18 != null)
                            {
                                r18.SEQ = updateLatestSeq;
                                r18.LAST_SBN_ENDREP_FLAG = lastSbnEndRepFlg;
                                r18.LAST_SBN_END_DATE = lastSbnEndDate.Equals(DateTime.MinValue) ? null : lastSbnEndDate.ToString("yyyyMMdd");
                                r18.LAST_SBN_END_REP_DATE = null;
                                // 最終処分終了報告フラグが立っている場合はもっとも新しい日付をセット
                                if (lastSbnEndRepFlg == 1)
                                {
                                    // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                                    //r18.LAST_SBN_END_REP_DATE = DateTime.Now.ToString("yyyyMMdd");
                                    r18.LAST_SBN_END_REP_DATE = this.sysDate.ToString("yyyyMMdd");
                                    // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                                }
                                r18Dao.Insert(r18);
                            }
                            foreach (var tempR19 in r19)
                            {
                                tempR19.SEQ = updateLatestSeq;
                                r19Dao.Insert(tempR19);
                            }
                            foreach (var tempR02 in r02)
                            {
                                tempR02.SEQ = updateLatestSeq;
                                r02Dao.Insert(tempR02);
                            }
                            foreach (var tempR04 in r04)
                            {
                                tempR04.SEQ = updateLatestSeq;
                                r04Dao.Insert(tempR04);
                            }
                            foreach (var tempR05 in r05)
                            {
                                tempR05.SEQ = updateLatestSeq;
                                r05Dao.Insert(tempR05);
                            }
                            foreach (var tempR08 in r08)
                            {
                                tempR08.SEQ = updateLatestSeq;
                                r08Dao.Insert(tempR08);
                            }

                            // DT_R18_EX, DT_R19_EX, DT_R04_EX, DT_R08_EXを論理削除。同じ情報で(SEQ+1)追加
                            if (oldR18Ex != null)
                            {
                                oldR18Ex.DELETE_FLG = true;
                                r18ExDao.Update(oldR18Ex);
                                oldR18Ex.SEQ = updateExSeq;
                                oldR18Ex.UPDATE_USER = SystemProperty.UserName;
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                                //oldR18Ex.UPDATE_DATE = DateTime.Now;
                                oldR18Ex.UPDATE_DATE = this.getDBDateTime();
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                                oldR18Ex.UPDATE_PC = SystemInformation.ComputerName;
                                oldR18Ex.DELETE_FLG = false;
                                r18ExDao.Insert(oldR18Ex);
                            }
                            foreach (var tempR19ex in oldR19Ex)
                            {
                                tempR19ex.DELETE_FLG = true;
                                r19ExDao.Update(tempR19ex);
                                tempR19ex.SEQ = updateExSeq;
                                tempR19ex.UPDATE_USER = SystemProperty.UserName;
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                                //tempR19ex.UPDATE_DATE = DateTime.Now;
                                tempR19ex.UPDATE_DATE = this.getDBDateTime();
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                                tempR19ex.UPDATE_PC = SystemInformation.ComputerName;
                                tempR19ex.DELETE_FLG = false;
                                r19ExDao.Insert(tempR19ex);
                            }
                            foreach (var tempR04ex in oldR04Ex)
                            {
                                tempR04ex.DELETE_FLG = true;
                                r04ExDao.Update(tempR04ex);
                                tempR04ex.SEQ = updateExSeq;
                                tempR04ex.UPDATE_USER = SystemProperty.UserName;
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                                //tempR04ex.UPDATE_DATE = DateTime.Now;
                                tempR04ex.UPDATE_DATE = this.getDBDateTime();
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                                tempR04ex.UPDATE_PC = SystemInformation.ComputerName;
                                tempR04ex.DELETE_FLG = false;
                                r04ExDao.Insert(tempR04ex);
                            }
                            foreach (var tempR08ex in oldR08Ex)
                            {
                                tempR08ex.DELETE_FLG = true;
                                r08ExDao.Update(tempR08ex);
                                tempR08ex.SEQ = updateExSeq;
                                tempR08ex.UPDATE_USER = SystemProperty.UserName;
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                                //tempR08ex.UPDATE_DATE = DateTime.Now;
                                tempR08ex.UPDATE_DATE = this.getDBDateTime();
                                // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                                tempR08ex.UPDATE_PC = SystemInformation.ComputerName;
                                tempR08ex.DELETE_FLG = false;
                                r08ExDao.Insert(tempR08ex);
                            }

                            // DT_R13_EX論理削除(DT_MF_TOCのLATEST_SEQを+1しているため、DT_R13は削除不要)
                            foreach (var tempR13ex in oldR13Ex)
                            {
                                tempR13ex.DELETE_FLG = true;
                                r13ExDao.Update(tempR13ex);
                            }
                        }
                        else
                        {
                            continue;
                        }

                        #endregion

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region 一次紙マニ

                        if (!string.IsNullOrEmpty(ret))
                        {
                            firstManiDetail.LAST_SBN_END_DATE = DateTime.Parse(ret);
                        }
                        firstManiDetail.DETAIL_SYSTEM_ID = SqlInt64.Parse(firstManiData.Rows[i]["FIRST_SYSTEM_ID"].ToString());
                        firstManiDetail.SEQ = SqlInt32.Parse(firstManiData.Rows[i]["SEQ"].ToString());

                        if (dtData.Rows.Count <= 1)
                        {
                            if (dtData.Rows.Count > 0)
                            {
                                firstManiDetail.LAST_SBN_GYOUSHA_CD = string.IsNullOrEmpty(dtData.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString()) ? null : dtData.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                firstManiDetail.LAST_SBN_GENBA_CD = string.IsNullOrEmpty(dtData.Rows[0]["LAST_SBN_GENBA_CD"].ToString()) ? null : dtData.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                            }
                        }
                        else
                        {
                            // 二次マニフェストの実績が2行以上ある場合
                            firstManiDetail.LAST_SBN_GYOUSHA_CD = null;
                            firstManiDetail.LAST_SBN_GENBA_CD = null;
                            SqlInt64 SystemID = SqlInt64.Parse(dtData.Rows[0]["SYSTEM_ID"].ToString());
                            SqlInt32 SEQ = SqlInt32.Parse(dtData.Rows[0]["SEQ"].ToString());
                            DataTable SecondPaperLastsbnInfo = new DataTable();

                            // 二次マニの最終処分業者、最終処分場所再取得
                            SecondPaperLastsbnInfo = this.DetailDao.GetDetailSecondLastSbn(SystemID, SEQ);

                            if (SecondPaperLastsbnInfo.Rows.Count <= 1)
                            {
                                // 全ての行で最終処分業者、最終処分場所が一致している場合
                                if (SecondPaperLastsbnInfo.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString())
                                        && !string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString()))
                                    {
                                        firstManiDetail.LAST_SBN_GYOUSHA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                        firstManiDetail.LAST_SBN_GENBA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                // 最終処分業者、最終処分場所が異なる行がある場合
                                DataView SecondInfoDataView = new DataView(SecondPaperLastsbnInfo);
                                DataTable tblLastSbnGyousha = SecondInfoDataView.ToTable("DistinctTable", true, new string[] { "LAST_SBN_GYOUSHA_CD" });
                                if (tblLastSbnGyousha.Rows.Count <= 1)
                                {
                                    // 最終処分業者が全て同じ場合
                                    firstManiDetail.LAST_SBN_GYOUSHA_CD = tblLastSbnGyousha.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                }
                            }
                        }

                        //マニ明細(T_MANIFEST_DATAIL)データ更新
                        this.DetailDao.UpdateFirstDetail(firstManiDetail);

                        #endregion
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

        // 20140710 ria EV005141 2次マニを修正モードにて登録しても紐づいた1次マニが更新されない end

        #region 登録/更新/削除

        /// <summary>
        /// 論理削除処理
        /// </summary>
        /// <param name="SYSTEM_ID">システムID </param>
        /// <param name="HAIKI_KBN_CD">廃棄区分</param>
        /// <param name="TIME_STAMP_ENTRY">ENTRYのタイムスタンプ</param>
        /// <param name="TIME_STAMP_RET_DATE">RET_DATEのタイムスタンプ</param>
        [Transaction]
        public virtual int LogicalEntityDel(string SYSTEM_ID, string SEQ, byte[] TIME_STAMP)
        {
            LogUtility.DebugMethodStart(SYSTEM_ID, SEQ, TIME_STAMP);

            int count = 0;

            var del = new T_MANIFEST_ENTRY();
            del.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            del.SEQ = Convert.ToInt32(SEQ);
            del.DELETE_FLG = SqlBoolean.Parse("true");

            var WHO = new DataBinderLogic<T_MANIFEST_ENTRY>(del);
            WHO.SetSystemProperty(del, false);

            //タイムスタンプ
            del.TIME_STAMP = TIME_STAMP;

            count = EntryDelDao.Update(del);

            LogUtility.DebugMethodEnd(SYSTEM_ID, SEQ, TIME_STAMP);
            return count;
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        /// <param name="SYSTEM_ID">システムID </param>
        /// <param name="SEQ">SEQ</param>
        /// <param name="TIME_STAMP_RET_DATE">RET_DATEのTIME_STAMP</param>
        [Transaction]
        public virtual int LogicalRetDateDel(string SYSTEM_ID, string SEQ, byte[] TIME_STAMP)
        {
            LogUtility.DebugMethodStart(SYSTEM_ID, SEQ, TIME_STAMP);

            int count = 0;
            var delret = new T_MANIFEST_RET_DATE();
            delret.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            delret.SEQ = Convert.ToInt32(SEQ);
            delret.DELETE_FLG = true;

            var WHO = new DataBinderLogic<T_MANIFEST_RET_DATE>(delret);
            WHO.SetSystemProperty(delret, false);

            delret.TIME_STAMP = TIME_STAMP;

            count = RetDateDelDao.Update(delret);

            LogUtility.DebugMethodEnd(SYSTEM_ID, SEQ, TIME_STAMP);
            return count;
        }

        /// <summary>
        /// Insert処理
        /// </summary>
        [Transaction]
        public void Insert(
            List<T_MANIFEST_ENTRY> entrylist,
            List<T_MANIFEST_UPN> upnlist,
            List<T_MANIFEST_PRT> prtlist,
            List<T_MANIFEST_DETAIL_PRT> detailprtlist,
            List<T_MANIFEST_KP_KEIJYOU> keijyoulist,
            List<T_MANIFEST_KP_NISUGATA> nisugatalist,
            List<T_MANIFEST_KP_SBN_HOUHOU> houhoulist,
            List<T_MANIFEST_DETAIL> detaillist,
            List<T_MANIFEST_RET_DATE> retdatelist)
        {
            int cont = 0;

            LogUtility.DebugMethodStart(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, nisugatalist, houhoulist, detaillist, retdatelist);
            //データ更新

            //マニフェスト
            if (entrylist != null && entrylist.Count() > 0)
            {
                foreach (T_MANIFEST_ENTRY entry in entrylist)
                {
                    cont = EntryDao.Insert(entry);
                }
            }

            //マニフェスト収集運搬
            if (upnlist != null && upnlist.Count() > 0)
            {
                foreach (T_MANIFEST_UPN upn in upnlist)
                {
                    cont = UpnDao.Insert(upn);
                }
            }

            //マニフェスト印字
            if (prtlist != null && prtlist.Count() > 0)
            {
                foreach (T_MANIFEST_PRT prt in prtlist)
                {
                    cont = PrtDao.Insert(prt);
                }
            }

            //マニフェスト印字明細
            if (detailprtlist != null && detailprtlist.Count() > 0)
            {
                foreach (T_MANIFEST_DETAIL_PRT detailprt in detailprtlist)
                {
                    cont = DetailPrtDao.Insert(detailprt);
                }
            }

            //マニ印字_建廃_形状
            if (keijyoulist != null && keijyoulist.Count() > 0)
            {
                foreach (T_MANIFEST_KP_KEIJYOU keijyou in keijyoulist)
                {
                    cont = KeijyouDao.Insert(keijyou);
                }
            }

            //マニ印字_建廃_荷姿
            if (nisugatalist != null && nisugatalist.Count() > 0)
            {
                foreach (T_MANIFEST_KP_NISUGATA nisugata in nisugatalist)
                {
                    cont = NisugataDao.Insert(nisugata);
                }
            }

            //マニ印字_建廃_処分方法
            if (houhoulist != null && houhoulist.Count() > 0)
            {
                foreach (T_MANIFEST_KP_SBN_HOUHOU houhou in houhoulist)
                {
                    cont = HouhouDao.Insert(houhou);
                }
            }

            //マニフェスト明細
            if (detaillist != null && detaillist.Count() > 0)
            {
                foreach (T_MANIFEST_DETAIL detail in detaillist)
                {
                    cont = DetailDao.Insert(detail);
                }
            }

            //マニフェスト返却日
            if (retdatelist != null && retdatelist.Count() > 0)
            {
                foreach (T_MANIFEST_RET_DATE retdate in retdatelist)
                {
                    cont = RetDateDao.Insert(retdate);
                }
            }

            LogUtility.DebugMethodEnd(entrylist, upnlist, prtlist, detailprtlist, keijyoulist, nisugatalist, houhoulist, detaillist, retdatelist);
        }

        #endregion

        /// <summary>
        /// 数値丸め
        /// </summary>
        public decimal GetSuuryoRound(decimal Suu, String ManifestSuuryoFormatCD)
        {
            LogUtility.DebugMethodStart(Suu, ManifestSuuryoFormatCD);

            //丸め桁を判定
            int decimals = 0;
            switch (ManifestSuuryoFormatCD)
            {
                case "1"://整数
                case "2"://整数
                    break;

                case "3"://小数第1位
                    decimals = 1;
                    break;

                case "4"://小数第2位
                    decimals = 2;
                    break;

                case "5"://小数第3位
                    decimals = 3;
                    break;

                case "6"://小数第4位
                    decimals = 4;
                    break;
            }

            LogUtility.DebugMethodEnd(Suu, ManifestSuuryoFormatCD);

            return Math.Round(Suu, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 端数処理(四捨五入)
        /// マニフェスト関連以外で使用したい場合はさらに汎用的なクラスへ移動すること
        /// </summary>
        /// <param name="num">対象</param>
        /// <param name="format"></param>
        /// <returns>結果</returns>
        public decimal Round(decimal num, string format)
        {
            decimal result = num;

            // 小数点以下の桁数を取得
            var index = format.IndexOf('.');
            var decimals = 0;
            if (index <= 0 || index == format.Length - 1)
            {
            }
            else
            {
                decimals = format.Substring(index + 1).Length;
            }

            result = Math.Round(num, decimals, MidpointRounding.AwayFromZero);

            return result;
        }

        /// <summary>
        /// テキスト長さチェック
        /// </summary>
        public bool ChktxtLength(List<CustomTextBox> lTxt, int txtLength)
        {
            LogUtility.DebugMethodStart(lTxt, txtLength);

            Boolean Err = false;

            foreach (var t in lTxt)
            {
                if (t != null)
                {
                    t.IsInputErrorOccured = false;

                    if (t.Text.Length > txtLength)
                    {
                        t.IsInputErrorOccured = true;
                        Err = true;
                    }
                    t.UpdateBackColor();//色反映
                }
            }

            if (Err)
            {
                LogUtility.DebugMethodEnd(lTxt, txtLength);
                return true;
            }

            LogUtility.DebugMethodEnd(lTxt, txtLength);
            return false;
        }

        /// <summary>
        /// 名称長さチェック
        /// </summary>
        public bool ChkNameLength(List<CustomTextBox> lTxt, int length)
        {
            LogUtility.DebugMethodStart(lTxt, length);

            if (this.ChktxtLength(lTxt, length))
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        /// <summary>
        /// 住所長さチェック
        /// </summary>
        public bool ChkAddLength(List<CustomTextBox> lTxt)
        {
            LogUtility.DebugMethodStart(lTxt);

            // 20160123 chenzz start 
            if (this.ChktxtLength(lTxt, 88))
            {
                // 20160123 chenzz end
                LogUtility.DebugMethodEnd(lTxt);
                return true;
            }

            LogUtility.DebugMethodEnd(lTxt);
            return false;
        }

        #region DataGrid版

        /// <summary>
        /// 業者マスタから住所情報を取得してCellに設定(DataGrid版)
        /// 転記無い項目用
        /// </summary>
        /// <param name="CustomDataGrid">対象グリッド</param>
        /// <param name="CurrentRow">業者CD(行番号)</param>
        /// <param name="ColGyoushaCd">業者CD(列番号)</param>
        /// <param name="ColGyoushaName">格納先_業者_名称(列番号)</param>
        /// <param name="ColGyoushaFurigana">格納先_業者_名称フリガナ(列番号)</param>
        /// <param name="ColGyoushaPost">格納先_業者_郵便番号(列番号)</param>
        /// <param name="ColGyoushaTel">格納先_業者_電話番号(列番号)</param>
        /// <param name="ColGyoushaAddress">格納先_業者_住所(列番号)</param>
        /// <returns>0：正常,1:空,2：エラー</returns>
        public int SetAddressGyoushaForDgv(
            CustomDataGridView CustomDataGrid, int CurrentRow,
            string ColGyoushaCd, string ColGyoushaName, string ColGyoushaFurigana,
            string ColGyoushaPost, string ColGyoushaTel, string ColGyoushaAddress)
        {
            return SetAddressGyoushaForDgv(CustomDataGrid, CurrentRow, "All", ColGyoushaCd, ColGyoushaName, ColGyoushaFurigana, ColGyoushaPost, ColGyoushaTel, ColGyoushaAddress, "", null, null);
        }

        /// <summary>
        /// 業者マスタから住所情報を取得してCellに設定(DataGrid版)
        /// </summary>
        /// <param name="CustomDataGrid">対象グリッド</param>
        /// <param name="CurrentRow">業者CD(行番号)</param>
        /// <param name="NameFlg">業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="ColGyoushaCd">業者CD(列番号)</param>
        /// <param name="ColGyoushaName">格納先_業者_名称(列番号)</param>
        /// <param name="ColGyoushaFurigana">格納先_業者_名称フリガナ(列番号)</param>
        /// <param name="ColGyoushaPost">格納先_業者_郵便番号(列番号)</param>
        /// <param name="ColGyoushaTel">格納先_業者_電話番号(列番号)</param>
        /// <param name="ColGyoushaAddress">格納先_業者_住所(列番号)</param>
        /// <param name="TenkiNameFlg">転記先 業者名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="ColTenkiGyoushaCd">転記先 業者CD</param>
        /// <param name="ColTenkiGyoshaName">転記先 業者名</param>
        /// <returns>0：正常,1:空,2：エラー</returns>
        public int SetAddressGyoushaForDgv(
            CustomDataGridView CustomDataGrid, int CurrentRow,
            string NameFlg,
            string ColGyoushaCd, string ColGyoushaName, string ColGyoushaFurigana,
            string ColGyoushaPost, string ColGyoushaTel, string ColGyoushaAddress,
            string TenkiNameFlg,
            string ColTenkiGyoushaCd, string ColTenkiGyoshaName)
        {
            LogUtility.DebugMethodStart(CustomDataGrid, CurrentRow,
                NameFlg, ColGyoushaCd, ColGyoushaName, ColGyoushaFurigana, ColGyoushaPost, ColGyoushaTel, ColGyoushaAddress,
                TenkiNameFlg, ColTenkiGyoushaCd, ColTenkiGyoshaName);

            try
            {
                //業者CD
                string GyoshaCd = string.Empty;
                if (ColGyoushaCd != null && CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaCd].Index].Value != null)
                {
                    GyoshaCd = CustomDataGrid.Rows[CurrentRow].Cells[ColGyoushaCd].Value.ToString();
                }

                //格納先_業者_名称
                if (ColGyoushaName != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaName].Index].Value = String.Empty;
                }

                //格納先_業者_名称（フリガナ）
                if (ColGyoushaFurigana != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaFurigana].Index].Value = String.Empty;
                }

                //格納先_業者_郵便番号
                if (ColGyoushaPost != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaPost].Index].Value = String.Empty;
                }

                //格納先_業者_電話番号
                if (ColGyoushaTel != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaTel].Index].Value = String.Empty;
                }

                //格納先_業者_住所
                if (ColGyoushaAddress != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaAddress].Index].Value = String.Empty;
                }

                //転記先_業者_CD
                if (ColTenkiGyoushaCd != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColTenkiGyoushaCd].Index].Value = String.Empty;
                }

                //転記先_業者_名称
                if (ColTenkiGyoshaName != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColTenkiGyoshaName].Index].Value = String.Empty;
                }

                if (GyoshaCd == string.Empty)
                {
                    LogUtility.DebugMethodEnd(1);
                    return 1;
                }

                //業者データ抽出
                var Serch = new CommonGyoushaDtoCls();
                Serch.GYOUSHA_CD = GyoshaCd;
                Serch.GYOUSHAKBN_MANI = "True";
                switch (ColGyoushaCd)
                {
                    //case "HST_GYOUSHA_CD":
                    case "排出事業者CD":
                        Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "true";
                        break;

                    //case "LAST_SBN_YOTEI_GYOUSHA_CD":
                    case "最終処分の場所（予定）業者CD":
                    //case "SBN_GYOUSHA_CD":
                    case "処分受託者CD":
                    //case "SBN_JYURYOUSHA_CD":
                    case "処分の受領者CD":
                    //case "SBN_JYUTAKUSHA_CD":
                    case "処分の受託者CD":
                        Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "true";
                        break;

                    //case "UPN_GYOUSHA_CD_1":
                    case "運搬受託者CD":
                    //case "UPN_GYOUSHA_CD_2":
                    case "区間2：運搬受託者CD":
                    //case "UPN_GYOUSHA_CD_3":
                    case "区間3：運搬受託者CD":
                        Serch.UNPAN_JUTAKUSHA_KAISHA_KBN = "true";
                        break;

                    case "積替保管業者CD":
                        Serch.TSUMIKAEHOKAN_KBN = "true";
                        break;

                    default:
                        return 2;
                }

                DataTable dt = GetGyousha(Serch);
                if (dt.Rows.Count <= 0)
                {
                    LogUtility.DebugMethodEnd(2);
                    return 2;
                }

                //格納先_業者_名称
                if (ColGyoushaName != null)
                {
                    switch (NameFlg)
                    {
                        case "Part1":
                            //「正式名称1」をセットする。
                            CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaName].Index].Value = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                            break;

                        case "All":
                        default:
                            //「正式名称1 + 正式名称2」をセットする。
                            CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaName].Index].Value = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                            break;
                    }
                }

                //格納先_業者_名称（フリガナ）
                if (ColGyoushaFurigana != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaFurigana].Index].Value = dt.Rows[0]["GYOUSHA_FURIGANA"].ToString();
                }

                //格納先_業者_郵便番号
                if (ColGyoushaPost != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaPost].Index].Value = dt.Rows[0]["GYOUSHA_POST"].ToString();
                }

                //格納先_業者_電話番号
                if (ColGyoushaTel != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaTel].Index].Value = dt.Rows[0]["GYOUSHA_TEL"].ToString();
                }

                //格納先_業者_住所
                if (ColGyoushaAddress != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaAddress].Index].Value = dt.Rows[0]["GYOUSHA_ADDRESS"].ToString();
                }

                //転記先_業者CD
                if (ColTenkiGyoushaCd != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColTenkiGyoushaCd].Index].Value = dt.Rows[0]["GYOUSHA_CD"].ToString();
                }

                //転記先_業者_名称
                if (ColTenkiGyoshaName != null)
                {
                    switch (TenkiNameFlg)
                    {
                        case "Part1":
                            //「正式名称1」をセットする。
                            CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColTenkiGyoshaName].Index].Value = dt.Rows[0]["GYOUSHA_NAME1"].ToString();
                            break;

                        case "All":
                        default:
                            //「正式名称1 + 正式名称2」をセットする。
                            CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColTenkiGyoshaName].Index].Value = dt.Rows[0]["GYOUSHA_NAME"].ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex, ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(CustomDataGrid, CurrentRow, ColGyoushaCd, ColGyoushaName, ColGyoushaFurigana, ColGyoushaPost, ColGyoushaTel, ColGyoushaAddress);
            return 0;
        }

        /// <summary>
        /// 現場マスタから住所情報を取得してCellに設定(DataGrid版)
        /// </summary>
        /// <param name="CustomDataGrid">対象グリッド</param>
        /// <param name="CurrentRow">現場CD(行番号)</param>
        /// <param name="ColGyoushaCd">業者CD(列番号)</param>
        /// <param name="ColGenbaCd">現場CD(列番号)</param>
        /// <param name="ColGenbaName">格納先_現場_名称(列番号)</param>
        /// <param name="ColGenbaFurigana">格納先_現場_名称フリガナ(列番号)</param>
        /// <param name="ColGenbaPost">格納先_現場_郵便番号(列番号)</param>
        /// <param name="ColGenbaTel">格納先_現場_電話番号(列番号)</param>
        /// <param name="ColGenbaAddress">格納先_現場_住所(列番号)</param>
        /// <returns>0：正常,1:空,2：エラー（E20),3:業者が一意に絞れない（E034）</returns>

        public int SetAddressGenbaForDgv(
            CustomDataGridView CustomDataGrid, int CurrentRow,
            string ColGyoushaCd, string ColGenbaCd, string ColGenbaName, string ColGenbaFurigana,
            string ColGenbaPost, string ColGenbaTel, string ColGenbaAddress)
        {
            LogUtility.DebugMethodStart(CustomDataGrid, CurrentRow, ColGyoushaCd, ColGenbaCd, ColGenbaName, ColGenbaFurigana, ColGenbaPost, ColGenbaTel, ColGenbaAddress);

            try
            {
                //業者CD
                string GyoshaCd = string.Empty;
                if (ColGyoushaCd != null && CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaCd].Index].Value != null)
                {
                    GyoshaCd = CustomDataGrid.Rows[CurrentRow].Cells[ColGyoushaCd].Value.ToString();
                }

                //現場CD
                string GenbaCd = string.Empty;
                if (ColGenbaCd != null && CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaCd].Index].Value != null)
                {
                    GenbaCd = CustomDataGrid.Rows[CurrentRow].Cells[ColGenbaCd].Value.ToString();
                }

                //格納先_現場_名称
                if (ColGenbaName != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaName].Index].Value = String.Empty;
                }

                //格納先_現場_名称（フリガナ）
                if (ColGenbaFurigana != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaFurigana].Index].Value = String.Empty;
                }

                //格納先_現場_郵便番号
                if (ColGenbaPost != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaPost].Index].Value = String.Empty;
                }

                //格納先_現場_電話番号
                if (ColGenbaTel != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaTel].Index].Value = String.Empty;
                }

                //格納先_現場_住所
                if (ColGenbaAddress != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaAddress].Index].Value = String.Empty;
                }

                if (GenbaCd == string.Empty)
                {
                    LogUtility.DebugMethodEnd(CustomDataGrid, CurrentRow, ColGyoushaCd, ColGenbaCd, ColGenbaName, ColGenbaFurigana, ColGenbaPost, ColGenbaTel, ColGenbaAddress);
                    return 1;
                }

                //業者データ抽出
                var Serch = new CommonGenbaDtoCls();
                if (GyoshaCd != String.Empty)
                {
                    Serch.GYOUSHA_CD = GyoshaCd;
                }

                Serch.GYOUSHAKBN_MANI = "True";
                Serch.GENBA_CD = GenbaCd;

                switch (ColGenbaCd)
                {
                    //case "HST_GENBA_CD":
                    case "排出事業場CD":
                        Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "true";
                        Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "true";
                        break;

                    //case "LAST_SBN_YOTEI_GENBA_CD":
                    case "最終処分の場所（予定）現場CD":
                        Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "true";
                        Serch.SAISHUU_SHOBUNJOU_KBN = "true";
                        break;

                    case "運搬先の事業場CD":
                        if ("区間1：運搬先の事業者CD".Equals(ColGyoushaCd))//積替
                        {
                            Serch.TSUMIKAEHOKAN_KBN = "true";
                        }
                        else if ("処分受託者CD".Equals(ColGyoushaCd))//処分施設
                        {
                            Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "true";
                            Serch.SHOBUN_NIOROSHI_GENBA_KBN = "true"; //現場の条件
                        }
                        else
                        {
                            return 2;
                        }
                        break;

                    case "積替保管場CD":
                        Serch.TSUMIKAEHOKAN_KBN = "true";
                        break;

                    default:
                        return 2;
                }

                DataTable dt = GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 1:
                        break;

                    case 0: //対象無
                        LogUtility.DebugMethodEnd(2);
                        return 2;

                    default: //業者が2件以上マッチ
                        LogUtility.DebugMethodEnd(3);
                        return 3;
                }

                //格納先_現場_名称
                if (ColGenbaName != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaName].Index].Value = dt.Rows[0]["GENBA_NAME"].ToString();
                }

                //格納先_現場_名称（フリガナ）
                if (ColGenbaFurigana != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaFurigana].Index].Value = dt.Rows[0]["GENBA_FURIGANA"].ToString();
                }

                //格納先_現場_郵便番号
                if (ColGenbaPost != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaPost].Index].Value = dt.Rows[0]["GENBA_POST"].ToString();
                }

                //格納先_現場_電話番号
                if (ColGenbaTel != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaTel].Index].Value = dt.Rows[0]["GENBA_TEL"].ToString();
                }

                //格納先_現場_住所
                if (ColGenbaAddress != null)
                {
                    CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGenbaAddress].Index].Value = dt.Rows[0]["GENBA_ADDRESS"].ToString();
                }

                //格納先_業者CD
                if (ColGyoushaCd != null)
                {
                    //業者CDが空の場合、一緒にセット
                    if (CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaCd].Index].Value == null ||
                        string.IsNullOrEmpty(CustomDataGrid.Rows[CurrentRow].Cells[ColGyoushaCd].Value.ToString()))
                    {
                        CustomDataGrid.Rows[CurrentRow].Cells[CustomDataGrid.Columns[ColGyoushaCd].Index].Value = dt.Rows[0]["GYOUSHA_CD"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex, ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(CustomDataGrid, CurrentRow, ColGyoushaCd, ColGenbaCd, ColGenbaName, ColGenbaFurigana, ColGenbaPost, ColGenbaTel, ColGenbaAddress);
            return 0;
        }

        /// <summary>
        /// テキスト長さチェック(DataGrid版)
        /// </summary>
        public bool ChkGrdtxtLength(CustomDataGridView CustomDataGrid, int Row, List<int> lCol, int txtLength)
        {
            LogUtility.DebugMethodStart(CustomDataGrid, lCol, Row, txtLength);

            Boolean Err = false;

            foreach (var j in lCol)
            {
                var t = CustomDataGrid[j, Row] as ICustomAutoChangeBackColor;

                if (t != null)
                {
                    //白くする
                    t.IsInputErrorOccured = false;

                    //名称
                    if (CustomDataGrid[j, Row].Value != null
                        && CustomDataGrid[j, Row].Value.ToString().Length > txtLength)
                    {
                        //赤くする
                        t.IsInputErrorOccured = true;
                        Err = true;
                    }
                    t.UpdateBackColor();//色反映
                }
            }

            if (Err)
            {
                LogUtility.DebugMethodEnd(CustomDataGrid, lCol, Row, txtLength);
                return true;
            }

            LogUtility.DebugMethodEnd(CustomDataGrid, lCol, Row, txtLength);
            return false;
        }

        /// <summary>
        /// 名称長さチェック(DataGrid版)
        /// </summary>
        public bool ChkGrdNameLength(CustomDataGridView CustomDataGrid, int Row, List<int> lCol)
        {
            return ChkGrdNameLength(CustomDataGrid, Row, lCol, 40);
        }

        /// <summary>
        /// 名称長さチェック(DataGrid版)
        /// </summary>
        public bool ChkGrdNameLength(CustomDataGridView CustomDataGrid, int Row, List<int> lCol, int maxLength)
        {
            LogUtility.DebugMethodStart(CustomDataGrid, Row, lCol, maxLength);

            if (this.ChkGrdtxtLength(CustomDataGrid, Row, lCol, maxLength))
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        /// <summary>
        /// 住所長さチェック(DataGrid版)
        /// </summary>
        public bool ChkGrdAddLength(CustomDataGridView CustomDataGrid, int Row, List<int> lCol)
        {
            return ChkGrdAddLength(CustomDataGrid, Row, lCol, 44);
        }

        /// <summary>
        /// 住所長さチェック(DataGrid版)
        /// </summary>
        public bool ChkGrdAddLength(CustomDataGridView CustomDataGrid, int Row, List<int> lCol, int maxLength)
        {
            LogUtility.DebugMethodStart(CustomDataGrid, Row, lCol, maxLength);

            if (this.ChkGrdtxtLength(CustomDataGrid, Row, lCol, maxLength))
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        #endregion

        #region 実績タブ系計算ロジック 換算値・減容率

        /// <summary>
        /// 換算値計算　※必ず計算するので、呼び出し元はセルの値変更のない場合は呼ばないようにすること
        /// </summary>
        /// <param name="HAIKI_KBN_CD">直行、積替、建廃(必須)</param>
        /// <param name="HAIKI_SHURUI_CD">廃棄物種類CDのセル(必須)</param>
        /// <param name="HAIKI_NAME_CD">廃棄物名称CDのセル(空文字OK)</param>
        /// <param name="HAIKI_SUU">廃棄物数のセル(必須)</param>
        /// <param name="NISUGATA_CD">荷姿CDのセル(空文字OK)</param>
        /// <param name="UNIT_CD">単位のセル(必須)</param>
        /// <param name="ManiFormatCd">フォーマットのコード（1～5）</param>
        /// <param name="ManiFormatString">フォーマット文字列（#.000等）</param>
        /// <param name="KANSAN_SU">換算値（結果を代入するセル）</param>
        public void SetKansanti(
            string HAIKI_KBN_CD,
            DgvCustomTextBoxCell HAIKI_SHURUI_CD,
            DgvCustomTextBoxCell HAIKI_NAME_CD,
            DgvCustomTextBoxCell HAIKI_SUU,
            DgvCustomTextBoxCell NISUGATA_CD,
            DgvCustomTextBoxCell UNIT_CD,
            string ManiFormatCd,
            string ManiFormatString,
            DgvCustomTextBoxCell KANSAN_SU
            )
        {
            LogUtility.DebugMethodStart(HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_NAME_CD, HAIKI_SUU, NISUGATA_CD, UNIT_CD, ManiFormatCd, ManiFormatString, KANSAN_SU);

            //絶対必須項目

            //一括入力だと空の場合がある
            if (string.IsNullOrEmpty(HAIKI_KBN_CD)) return;

            //廃棄物種類
            if (HAIKI_SHURUI_CD.Value == null || string.IsNullOrEmpty(HAIKI_SHURUI_CD.Value.ToString())) return;
            //単位
            if (UNIT_CD.Value == null || string.IsNullOrEmpty(UNIT_CD.Value.ToString())) return;
            //数量
            if (HAIKI_SUU.Value == null || string.IsNullOrEmpty(HAIKI_SUU.Value.ToString())) return;

            //個別単価のように優先順位を考慮して検索
            bool find = false;
            var SearchString = new CommonKanSanDtoCls();
            DataTable dt = null;

            //廃棄物区分CD
            SearchString.HAIKI_KBN_CD = HAIKI_KBN_CD;
            //廃棄物種類CD
            SearchString.HAIKI_SHURUI_CD = HAIKI_SHURUI_CD.EditedFormattedValue.ToString(); //valueだと0100ではなく100が入る場合があるため。
            //数量
            SearchString.HAIKI_SUU = HAIKI_SUU.Value.ToString().Replace(",", "");
            //単位CD
            SearchString.UNIT_CD = UNIT_CD.Value.ToString();

            //１．廃棄物名称　あり 、荷姿CD あり
            if (HAIKI_NAME_CD.Value != null && NISUGATA_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD.Value.ToString();
                SearchString.NISUGATA_CD = NISUGATA_CD.Value.ToString();

                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //２．廃棄物名称　あり 、荷姿CD なし
            if (!find && HAIKI_NAME_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD.Value.ToString();
                SearchString.NISUGATA_CD = string.Empty;

                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //３．廃棄物名称　なし 、荷姿CD あり
            if (!find && NISUGATA_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.NISUGATA_CD = NISUGATA_CD.Value.ToString();

                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //４．廃棄物名称　なし 、荷姿CD なし
            if (!find)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.NISUGATA_CD = string.Empty;

                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //完全無
            if (!find)
            {
                LogUtility.Warn(string.Format("換算情報なし：廃棄区分={0},廃棄物種類={1},廃棄物名称={2},荷姿={3},廃棄数={4}",
                    HAIKI_KBN_CD, HAIKI_SHURUI_CD.Value, HAIKI_NAME_CD.Value, NISUGATA_CD.Value, HAIKI_SUU.Value));
                LogUtility.DebugMethodEnd();
                return;
            }

            //あり

            //数値の丸め
            decimal dKansanti = 0;
            dKansanti = this.GetSuuryoRound(Convert.ToDecimal(dt.Rows[0]["KANSAN_CHI"]), ManiFormatCd);

            //換算後数量
            KANSAN_SU.Value = dKansanti.ToString(ManiFormatString);

            LogUtility.DebugMethodEnd();
        }

        public void SetKansanti2(
            string HAIKI_KBN_CD,
            string HAIKI_SHURUI_CD,
            string HAIKI_NAME_CD,
            string HAIKI_SUU,
            string NISUGATA_CD,
            string UNIT_CD,
            string ManiFormatCd,
            string ManiFormatString,
            DgvCustomTextBoxCell KANSAN_SU
            )
        {
            LogUtility.DebugMethodStart(HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_NAME_CD, HAIKI_SUU, NISUGATA_CD, UNIT_CD, ManiFormatCd, ManiFormatString, KANSAN_SU);
            //絶対必須項目
            //一括入力だと空の場合がある
            if (string.IsNullOrEmpty(HAIKI_KBN_CD)) return;
            //廃棄物種類
            if (string.IsNullOrEmpty(HAIKI_SHURUI_CD)) return;
            //単位
            if (string.IsNullOrEmpty(UNIT_CD)) return;
            //数量
            if (string.IsNullOrEmpty(HAIKI_SUU)) return;
            //個別単価のように優先順位を考慮して検索
            bool find = false;
            var SearchString = new CommonKanSanDtoCls();
            DataTable dt = null;
            //廃棄物区分CD
            SearchString.HAIKI_KBN_CD = HAIKI_KBN_CD;
            //廃棄物種類CD
            SearchString.HAIKI_SHURUI_CD = HAIKI_SHURUI_CD;
            //数量
            SearchString.HAIKI_SUU = HAIKI_SUU.ToString().Replace(",", "");
            //単位CD
            SearchString.UNIT_CD = UNIT_CD.ToString();
            //１．廃棄物名称　あり 、荷姿CD あり
            if (HAIKI_NAME_CD != null && NISUGATA_CD != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD.ToString();
                SearchString.NISUGATA_CD = NISUGATA_CD.ToString();
                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //２．廃棄物名称　あり 、荷姿CD なし
            if (!find && HAIKI_NAME_CD != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD.ToString();
                SearchString.NISUGATA_CD = string.Empty;
                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //３．廃棄物名称　なし 、荷姿CD あり
            if (!find && NISUGATA_CD != null)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.NISUGATA_CD = NISUGATA_CD.ToString();
                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //４．廃棄物名称　なし 、荷姿CD なし
            if (!find)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.NISUGATA_CD = string.Empty;
                dt = GetKansanti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //完全無
            if (!find)
            {
                LogUtility.Warn(string.Format("換算情報なし：廃棄区分={0},廃棄物種類={1},廃棄物名称={2},荷姿={3},廃棄数={4}",
                    HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_NAME_CD, NISUGATA_CD, HAIKI_SUU));
                LogUtility.DebugMethodEnd();
                return;
            }
            //あり
            //数値の丸め
            decimal dKansanti = 0;
            dKansanti = this.GetSuuryoRound(Convert.ToDecimal(dt.Rows[0]["KANSAN_CHI"]), ManiFormatCd);
            //換算後数量
            KANSAN_SU.Value = dKansanti.ToString(ManiFormatString);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 換算値を検索（GetKansanData呼び出しをラップしてログ出力追加しています）
        /// </summary>
        /// <param name="SearchString">検索条件</param>
        /// <returns></returns>
        public DataTable GetKansanti(CommonKanSanDtoCls SearchString)
        {
            LogUtility.DebugMethodStart(SearchString);

            DataTable dt = this.GetKansanData(SearchString);

            if (dt.Rows.Count == 0)
            {
                LogUtility.Debug(string.Format("換算情報なし：廃棄区分={0},廃棄物種類={1},廃棄物名称={2},荷姿={3},廃棄数={4}",
                    SearchString.HAIKI_KBN_CD, SearchString.HAIKI_SHURUI_CD, SearchString.HAIKI_NAME_CD, SearchString.NISUGATA_CD, SearchString.HAIKI_SUU));
            }
            else
            {
                LogUtility.Debug(string.Format("換算情報あり：廃棄区分={0},廃棄物種類={1},廃棄物名称={2},荷姿={3},廃棄数={4}",
                    SearchString.HAIKI_KBN_CD, SearchString.HAIKI_SHURUI_CD, SearchString.HAIKI_NAME_CD, SearchString.NISUGATA_CD, SearchString.HAIKI_SUU));
            }

            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        /// <summary>
        /// 減容値計算　※必ず計算するので、呼び出し元はセルの値変更のない場合は呼ばないようにすること
        /// </summary>
        /// <param name="HAIKI_KBN_CD">直行、積替、建廃(必須)</param>
        /// <param name="HAIKI_SHURUI_CD">廃棄物種類CDのセル(必須)</param>
        /// <param name="HAIKI_NAME_CD">廃棄物名称CDのセル(空文字OK)</param>
        /// <param name="SHOBUN_CD">処分方法CDのセル(空文字OK)</param>
        /// <param name="KANSAN_SU">換算数のセル(必須)</param>
        /// <param name="ManiFormatCd">フォーマットのコード（1～5）</param>
        /// <param name="ManiFormatString">フォーマット文字列（#.000等）</param>
        /// <param name="GENYOU_SU">減容値（結果を代入するセル）</param>
        public void SetGenyouti(
            string HAIKI_KBN_CD, DgvCustomTextBoxCell HAIKI_SHURUI_CD, DgvCustomTextBoxCell HAIKI_NAME_CD, DgvCustomTextBoxCell SHOBUN_CD, DgvCustomTextBoxCell KANSAN_SU,
            string ManiFormatCd, string ManiFormatString, DgvCustomTextBoxCell GENYOU_SU)
        {
            LogUtility.DebugMethodStart(HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_NAME_CD, SHOBUN_CD, KANSAN_SU, ManiFormatCd, ManiFormatString, GENYOU_SU);

            //絶対必須項目

            //一括入力だと空の場合がある
            if (string.IsNullOrEmpty(HAIKI_KBN_CD)) return;

            //廃棄物種類
            if (HAIKI_SHURUI_CD.Value == null || string.IsNullOrEmpty(HAIKI_SHURUI_CD.Value.ToString())) return;
            //数量
            if (KANSAN_SU.Value == null || string.IsNullOrEmpty(KANSAN_SU.Value.ToString())) return;

            //個別単価のように優先順位を考慮して検索
            bool find = false;
            var SearchString = new CommonKanSanDtoCls();
            DataTable dt = null;

            //廃棄物区分CD
            SearchString.HAIKI_KBN_CD = HAIKI_KBN_CD;
            //廃棄物種類CD
            SearchString.HAIKI_SHURUI_CD = HAIKI_SHURUI_CD.EditedFormattedValue.ToString(); //valueだと0100ではなく100が入る場合があるため。
            //数量
            SearchString.KANSAN_SUU = KANSAN_SU.Value.ToString().Replace(",", "");

            //１．廃棄物名称　あり 、処分方法　あり
            if (HAIKI_NAME_CD.Value != null && SHOBUN_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD.Value.ToString();
                SearchString.SHOBUN_HOUHOU_CD = SHOBUN_CD.Value.ToString();

                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //２．廃棄物名称　あり 、処分方法　 なし
            if (!find && HAIKI_NAME_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD.Value.ToString();
                SearchString.SHOBUN_HOUHOU_CD = string.Empty;

                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //３．廃棄物名称　なし 、処分方法　 あり
            if (!find && SHOBUN_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.SHOBUN_HOUHOU_CD = SHOBUN_CD.Value.ToString();

                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //４．廃棄物名称　なし 、処分方法　 なし
            if (!find)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.SHOBUN_HOUHOU_CD = string.Empty;

                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }

            //完全無
            if (!find)
            {
                LogUtility.Warn(string.Format("減容情報なし：廃棄区分={0},廃棄物種類={1},処分方法={2},換算数={3}",
                    HAIKI_KBN_CD, HAIKI_SHURUI_CD.Value, HAIKI_NAME_CD.Value, SHOBUN_CD.Value, KANSAN_SU.Value));

                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう start
                GENYOU_SU.Value = KANSAN_SU.Value;
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう end

                LogUtility.DebugMethodEnd();

                return;
            }

            //あり

            //数値の丸め
            decimal dKansanti = 0;
            dKansanti = this.GetSuuryoRound(Convert.ToDecimal(dt.Rows[0]["GENYOU_CHI"]), ManiFormatCd);

            //換算後数量
            GENYOU_SU.Value = dKansanti.ToString(ManiFormatString);

            LogUtility.DebugMethodEnd();
        }

        public void SetGenyouti2(
            string HAIKI_KBN_CD, string HAIKI_SHURUI_CD, string HAIKI_NAME_CD, DgvCustomTextBoxCell SHOBUN_CD, DgvCustomTextBoxCell KANSAN_SU,
            string ManiFormatCd, string ManiFormatString, DgvCustomTextBoxCell GENYOU_SU)
        {
            LogUtility.DebugMethodStart(HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_NAME_CD, SHOBUN_CD, KANSAN_SU, ManiFormatCd, ManiFormatString, GENYOU_SU);
            //絶対必須項目
            //一括入力だと空の場合がある
            if (string.IsNullOrEmpty(HAIKI_KBN_CD)) return;
            //廃棄物種類
            if (string.IsNullOrEmpty(HAIKI_SHURUI_CD)) return;
            //数量
            if (KANSAN_SU.Value == null || string.IsNullOrEmpty(KANSAN_SU.Value.ToString())) return;
            //個別単価のように優先順位を考慮して検索
            bool find = false;
            var SearchString = new CommonKanSanDtoCls();
            DataTable dt = null;
            //廃棄物区分CD
            SearchString.HAIKI_KBN_CD = HAIKI_KBN_CD;
            //廃棄物種類CD
            SearchString.HAIKI_SHURUI_CD = HAIKI_SHURUI_CD;
            //数量
            SearchString.KANSAN_SUU = KANSAN_SU.Value.ToString().Replace(",", "");
            //１．廃棄物名称　あり 、処分方法　あり
            if ((!string.IsNullOrEmpty(HAIKI_NAME_CD)) && SHOBUN_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD;
                SearchString.SHOBUN_HOUHOU_CD = SHOBUN_CD.Value.ToString();
                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //２．廃棄物名称　あり 、処分方法　 なし
            if (!find && !string.IsNullOrEmpty(HAIKI_NAME_CD))
            {
                SearchString.HAIKI_NAME_CD = HAIKI_NAME_CD;
                SearchString.SHOBUN_HOUHOU_CD = string.Empty;
                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //３．廃棄物名称　なし 、処分方法　 あり
            if (!find && SHOBUN_CD.Value != null)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.SHOBUN_HOUHOU_CD = SHOBUN_CD.Value.ToString();
                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //４．廃棄物名称　なし 、処分方法　 なし
            if (!find)
            {
                SearchString.HAIKI_NAME_CD = string.Empty;
                SearchString.SHOBUN_HOUHOU_CD = string.Empty;
                dt = GetGenyouti(SearchString);
                if (dt.Rows.Count > 0)
                {
                    find = true;
                }
            }
            //完全無
            if (!find)
            {
                LogUtility.Warn(string.Format("減容情報なし：廃棄区分={0},廃棄物種類={1},処分方法={2},換算数={3}",
                    HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_NAME_CD, SHOBUN_CD.Value, KANSAN_SU.Value));
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう start
                GENYOU_SU.Value = KANSAN_SU.Value;
                // 20140624 kayo EV005028 減容率が登録されていない報告書分類を実績タブに入力すると減容後数量が0になってしまう end
                LogUtility.DebugMethodEnd();
                return;
            }
            //あり
            //数値の丸め
            decimal dKansanti = 0;
            dKansanti = this.GetSuuryoRound(Convert.ToDecimal(dt.Rows[0]["GENYOU_CHI"]), ManiFormatCd);
            //換算後数量
            GENYOU_SU.Value = dKansanti.ToString(ManiFormatString);
            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 減容値を検索（GetGenyouData呼び出しをラップしてログ出力追加しています）
        /// </summary>
        /// <param name="SearchString">検索条件</param>
        /// <returns></returns>
        public DataTable GetGenyouti(CommonKanSanDtoCls SearchString)
        {
            LogUtility.DebugMethodStart(SearchString);

            DataTable dt = this.GetGenyouData(SearchString);

            if (dt.Rows.Count == 0)
            {
                LogUtility.Warn(string.Format("減容情報なし：廃棄区分={0},廃棄物種類={1},処分方法={2},換算数={3}",
                    SearchString.HAIKI_KBN_CD, SearchString.HAIKI_SHURUI_CD, SearchString.HAIKI_NAME_CD, SearchString.SHOBUN_HOUHOU_CD, SearchString.KANSAN_SUU));
            }
            else
            {
                LogUtility.Warn(string.Format("減容情報あり：廃棄区分={0},廃棄物種類={1},処分方法={2},換算数={3}",
                    SearchString.HAIKI_KBN_CD, SearchString.HAIKI_SHURUI_CD, SearchString.HAIKI_NAME_CD, SearchString.SHOBUN_HOUHOU_CD, SearchString.KANSAN_SUU));
            }

            LogUtility.DebugMethodEnd(dt);
            return dt;
        }

        #endregion

        #region 正式名称対応

        #region 担当者系マスタ ポップアップ＋validatingセットアップ

        /// <summary>
        /// 社員マスタのbit項目
        /// </summary>
        public enum SHAIN_KBN
        {
            /// <summary>営業担当者</summary>
            EIGYOU_TANTOU,

            /// <summary>運転者</summary>
            UNTEN_KBN,

            /// <summary>処分担当者</summary>
            SHOBUN_TANTOU_KBN,

            /// <summary>手形保管者</summary>
            TEGATA_HOKAN_KBN,

            /// <summary>入力担当者</summary>
            NYUURYOKU_TANTOU_KBN,

            /// <summary>INXS担当者</summary>
            INXS_TANTOU_KBN
        }

        /// <summary>
        /// 社員系の項目のポップアップとValidatingイベントのセットアップ処理（複数回呼び出しはNGです。validatingが二重に増えてしまいます）
        /// </summary>
        /// <param name="cd">社員コードの入力のコントロール</param>
        /// <param name="name">名前を表示するコントロール</param>
        /// <param name="kbn">社員マスタのどの区分を見るか</param>
        public static void SetupShainCd(CustomTextBox cd, CustomTextBox name, SHAIN_KBN kbn)
        {
            cd.FocusOutCheckMethod.Clear();
            cd.RegistCheckMethod.Clear();

            cd.PopupWindowName = "マスタ共通ポップアップ";
            cd.PopupBeforeExecuteMethod = "";
            cd.PopupAfterExecuteMethod = "";
            cd.PopupDataHeaderTitle = null;
            cd.PopupDataSource = null;
            cd.PopupSearchSendParams.Clear();
            cd.PopupTitleLabel = "";
            cd.popupWindowSetting.Clear();

            switch (kbn)
            {
                case SHAIN_KBN.EIGYOU_TANTOU:
                    cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_EIGYOU_TANTOUSHA;
                    break;

                case SHAIN_KBN.NYUURYOKU_TANTOU_KBN:
                    cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_NYUURYOKU_TANTOUSHA;
                    break;

                case SHAIN_KBN.SHOBUN_TANTOU_KBN:
                    cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHOBUN_TANTOUSHA;
                    break;

                case SHAIN_KBN.TEGATA_HOKAN_KBN:
                    cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_TEGATA_HOKANSHA;
                    break;

                case SHAIN_KBN.UNTEN_KBN:
                    cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNTENSHA;
                    break;

                case SHAIN_KBN.INXS_TANTOU_KBN:
                    cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_INXS_TANTOUSHA;
                    break;
            }

            //追加テーブルjoin
            cd.popupWindowSetting.Add(new JoinMethodDto
            {
                Join = r_framework.Const.JOIN_METHOD.INNER_JOIN,
                LeftTable = cd.PopupWindowId.ToString(),
                RightTable = "M_SHAIN",
                IsCheckLeftTable = true,
                LeftKeyColumn = "SHAIN_CD",
                RightKeyColumn = "SHAIN_CD"
            });

            //M_SHAINの有効条件付与
            cd.popupWindowSetting.Add(new JoinMethodDto
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = "M_SHAIN",
                IsCheckLeftTable = true
            });

            //○○区分参照
            var dto = new JoinMethodDto()
            {
                Join = r_framework.Const.JOIN_METHOD.WHERE,
                LeftTable = "M_SHAIN",
                SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
            };
            dto.SearchCondition.Add(new SearchConditionsDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                LeftColumn = kbn.ToString(),
                Value = "True",
                ValueColumnType = r_framework.Const.DB_TYPE.BIT
            });
            cd.popupWindowSetting.Add(dto);

            //戻り設定
            cd.PopupGetMasterField = "CD,NAME";
            cd.GetCodeMasterField = cd.PopupGetMasterField;
            cd.PopupSetFormField = string.Join(",", cd.Name, name.Name);
            cd.SetFormField = cd.PopupSetFormField;

            //validatingイベント設定（手入力対応）

            cd.Validating += (sender, e) =>
            {
                if (e.Cancel)
                {
                    return;
                }

                e.Cancel = ShainPopupAfterBase(cd);
            };

            //ポップアップアフター対応
            cd.PopupAfterExecute = (sender, result) =>
                {
                    ShainPopupAfterBase(cd); //正式名取得目的
                };
        }

        /// <summary>
        /// 社員のvalidatingイベント。コード未入力だと名前をクリア。入力有だとマスタチェックし名前セットorエラーメッセージ
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public static bool ShainPopupAfterBase(CustomTextBox cd)
        {
            //変更なければ処理しない
            if (!cd.isChanged())
            {
                return false;
            }

            //名前コントロールを自動取得
            var name = cd.Parent.Controls.Find(cd.PopupSetFormField.Split(',')[1], true)[0] as CustomTextBox;

            //空になった場合
            if (string.IsNullOrEmpty(cd.Text))
            {
                name.Text = ""; //クリア
                return false; //正常はfalse
            }

            var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();

            string sql = "SELECT M_SHAIN.SHAIN_CD AS CD, M_SHAIN.SHAIN_NAME AS NAME ";
            sql += " FROM M_SHAIN ";
            sql += " INNER JOIN {0} M2 ON M2.SHAIN_CD = M_SHAIN.SHAIN_CD ";
            sql += " WHERE M_SHAIN.SHAIN_CD = '{1}' ";
            sql += "   AND M2.DELETE_FLG = 0 ";
            sql += "   AND M_SHAIN.DELETE_FLG = 0 ";
            sql += " GROUP BY M_SHAIN.SHAIN_CD,M_SHAIN.SHAIN_NAME ";
            sql += " ORDER BY M_SHAIN.SHAIN_CD ";

            //ForStringSql は継承したクラスでも宣言しないと使えない模様
            var dt = dao.GetDateForStringSql(string.Format(sql, cd.PopupWindowId.ToString(), cd.Text));

            //0件
            if (dt.Rows.Count == 0)
            {
                name.Text = ""; //クリア
                //メッセージ
                string tableName = "";
                switch (cd.PopupWindowId)
                {
                    case r_framework.Const.WINDOW_ID.M_SHOBUN_TANTOUSHA:
                        tableName = "処分担当者";
                        break;

                    case r_framework.Const.WINDOW_ID.M_EIGYOU_TANTOUSHA:
                        tableName = "営業担当者";
                        break;

                    case r_framework.Const.WINDOW_ID.M_UNTENSHA:
                        tableName = "運転者";
                        break;

                    case r_framework.Const.WINDOW_ID.M_TEGATA_HOKANSHA:
                        tableName = "手形保管者";
                        break;

                    case r_framework.Const.WINDOW_ID.M_NYUURYOKU_TANTOUSHA:
                        tableName = "入力担当者";
                        break;

                    case r_framework.Const.WINDOW_ID.M_INXS_TANTOUSHA:
                        tableName = "INXS担当者";
                        break;
                }

                cd.IsInputErrorOccured = true; //エラーフラグ
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E020", tableName);
                return true; //以上はtrue
            }

            //取得
            name.Text = dt.Rows[0]["NAME"].ToString();
            return false; //正常はfalse
        }

        #endregion

        #region 汎用 ポップアップ＋validatingセットアップ

        /// <summary>
        /// 単位など各種ポップアップとValidatingイベントのセットアップ処理（イベントハンドラを強制クリアするので注意）
        /// 対応範囲はXxxPopupAfterBaseの実装の分岐を参照してください
        /// </summary>
        /// <param name="ID">利用するテーブル名</param>
        /// <param name="cd">コードの入力のコントロール</param>
        /// <param name="name">名前を表示するコントロール</param>
        /// <param name="haikiKbnCd">廃棄物区分</param>
        /// <param name="haikiShuruiCdFrom">廃棄物種類でのみ利用　最小値（以上を許可）</param>
        /// <param name="haikiShuruiCdTo">廃棄物種類でのみ利用　最大値（以下を許可）</param>
        public static void SetupXxxCd(r_framework.Const.WINDOW_ID ID, CustomTextBox cd, CustomTextBox name, SqlInt16 haikiKbnCd, string haikiShuruiCdFrom, string haikiShuruiCdTo)
        {
            ClearEvent(cd); //イベントクリア

            cd.FocusOutCheckMethod.Clear();
            cd.RegistCheckMethod.Clear();

            cd.PopupWindowName = "マスタ共通ポップアップ";
            cd.PopupBeforeExecuteMethod = "";
            cd.PopupAfterExecuteMethod = "";
            cd.PopupDataHeaderTitle = null;
            cd.PopupDataSource = null;
            cd.PopupSearchSendParams.Clear();
            cd.popupWindowSetting.Clear();
            cd.PopupTitleLabel = "";
            cd.popupWindowSetting.Clear();
            cd.PopupWindowId = ID;

            switch (ID)
            {
                //廃棄物種類は 条件複雑
                case r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI:

                    var dto = new JoinMethodDto()
                    {
                        Join = r_framework.Const.JOIN_METHOD.WHERE,
                        LeftTable = ID.ToString(),
                        SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
                    };

                    //廃棄区分
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.EQUALS,
                        LeftColumn = "HAIKI_KBN_CD",
                        Value = haikiKbnCd.ToString(),
                        ValueColumnType = r_framework.Const.DB_TYPE.SMALLINT
                    });
                    //FROM
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.MORE_THAN,
                        LeftColumn = "HAIKI_SHURUI_CD",
                        Value = haikiShuruiCdFrom,
                        ValueColumnType = r_framework.Const.DB_TYPE.VARCHAR
                    });

                    //TO
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.LESS_THAN,
                        LeftColumn = "HAIKI_SHURUI_CD",
                        Value = haikiShuruiCdTo,
                        ValueColumnType = r_framework.Const.DB_TYPE.VARCHAR
                    });

                    cd.popupWindowSetting.Add(dto);

                    break;

                //荷姿はFROMTOチェック必要
                case r_framework.Const.WINDOW_ID.M_NISUGATA:
                    dto = new JoinMethodDto()
                    {
                        Join = r_framework.Const.JOIN_METHOD.WHERE,
                        LeftTable = ID.ToString(),
                        SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
                    };

                    //FROM
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.MORE_THAN,
                        LeftColumn = "NISUGATA_CD",
                        Value = haikiShuruiCdFrom,
                        ValueColumnType = r_framework.Const.DB_TYPE.VARCHAR
                    });

                    //TO
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.LESS_THAN,
                        LeftColumn = "NISUGATA_CD",
                        Value = haikiShuruiCdTo,
                        ValueColumnType = r_framework.Const.DB_TYPE.VARCHAR
                    });

                    cd.popupWindowSetting.Add(dto);

                    break;
                //計上はFROMTOチェック必要
                case r_framework.Const.WINDOW_ID.M_KEIJOU:
                    dto = new JoinMethodDto()
                  {
                      Join = r_framework.Const.JOIN_METHOD.WHERE,
                      LeftTable = ID.ToString(),
                      SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>()
                  };

                    //FROM
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.MORE_THAN,
                        LeftColumn = "KEIJOU_CD",
                        Value = haikiShuruiCdFrom,
                        ValueColumnType = r_framework.Const.DB_TYPE.VARCHAR
                    });

                    //TO
                    dto.SearchCondition.Add(new SearchConditionsDto()
                    {
                        And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                        Condition = r_framework.Const.JUGGMENT_CONDITION.LESS_THAN,
                        LeftColumn = "KEIJOU_CD",
                        Value = haikiShuruiCdTo,
                        ValueColumnType = r_framework.Const.DB_TYPE.VARCHAR
                    });

                    cd.popupWindowSetting.Add(dto);

                    break;
            }

            //戻り設定
            cd.PopupGetMasterField = "CD,NAME";
            cd.GetCodeMasterField = cd.PopupGetMasterField;
            cd.PopupSetFormField = string.Join(",", cd.Name, name.Name);
            cd.SetFormField = cd.PopupSetFormField;

            //validatingイベント設定（手入力対応）

            cd.Validating += (sender, e) =>
            {
                if (e.Cancel)
                {
                    return;
                }

                e.Cancel = XxxPopupAfterBase(cd, haikiKbnCd, haikiShuruiCdFrom, haikiShuruiCdTo);
            };

            //ポップアップアフター対応
            cd.PopupAfterExecute = (sender, result) =>
            {
                // ポップアップで選択された場合は前回値を初期化
                if (result == DialogResult.OK) cd.prevText = string.Empty;

                XxxPopupAfterBase(cd, haikiKbnCd, haikiShuruiCdFrom, haikiShuruiCdTo); ; //正式名取得目的
            };
        }

        /// <summary>
        /// 単位のvalidatingイベント。コード未入力だと名前をクリア。入力有だとマスタチェックし名前セットorエラーメッセージ
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public static bool XxxPopupAfterBase(CustomTextBox cd, SqlInt16 haikiKbnCd, string haikiShuruiCdFrom, string haikiShuruiCdTo)
        {
            //変更なければ処理しない
            if (!cd.isChanged())
            {
                return false;
            }

            //名前コントロールを自動取得
            var name = cd.Parent.Controls.Find(cd.PopupSetFormField.Split(',')[1], true)[0] as CustomTextBox;

            //空になった場合
            if (string.IsNullOrEmpty(cd.Text))
            {
                name.Text = ""; //クリア
                return false; //正常はfalse
            }

            string nameValue = "";
            string errTable = "";
            bool hasErr = false;
            switch (cd.PopupWindowId)
            {
                //単位
                case r_framework.Const.WINDOW_ID.M_UNIT:
                    {//スコープ分けて同じ変数名定義できるように波括弧つけています。
                        var dao = DaoInitUtility.GetComponent<IM_UNITDao>();
                        var dt = dao.GetAllValidData(new M_UNIT() { UNIT_CD = Int16.Parse(cd.Text), KAMI_USE_KBN = true });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "単位";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].UNIT_NAME; //正式名
                        }
                        break;
                    }
                //荷姿
                case r_framework.Const.WINDOW_ID.M_NISUGATA:
                    {
                        if (string.IsNullOrEmpty(haikiShuruiCdFrom) || string.IsNullOrEmpty(haikiShuruiCdTo))
                        {
                            //一方でも空だと何もしない
                        }
                        else
                        {
                            //両方入っている場合比較（最小値以上、最大値以下で判断する）
                            if (string.Compare(haikiShuruiCdFrom, cd.Text) <= 0 && string.Compare(haikiShuruiCdTo, cd.Text) >= 0)
                            {
                                //範囲以内ならOK
                            }
                            else
                            {
                                //範囲外になる場合はマスタなしとみなす
                                errTable = "荷姿";
                                hasErr = true;
                                break;
                            }
                        }

                        var dao = DaoInitUtility.GetComponent<IM_NISUGATADao>();
                        var dt = dao.GetAllValidData(new M_NISUGATA() { NISUGATA_CD = cd.Text, KAMI_USE_KBN = true });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "荷姿";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].NISUGATA_NAME; //正式名
                        }
                        break;
                    }

                //有害物質
                case r_framework.Const.WINDOW_ID.M_YUUGAI_BUSSHITSU:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_YUUGAI_BUSSHITSUDao>();
                        var dt = dao.GetAllValidData(new M_YUUGAI_BUSSHITSU() { YUUGAI_BUSSHITSU_CD = cd.Text });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "有害物質";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].YUUGAI_BUSSHITSU_NAME; //正式名
                        }
                        break;
                    }

                //廃棄物名称
                case r_framework.Const.WINDOW_ID.M_HAIKI_NAME:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
                        var dt = dao.GetAllValidData(new M_HAIKI_NAME() { HAIKI_NAME_CD = cd.Text });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "廃棄物名称";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].HAIKI_NAME; //正式名
                        }
                        break;
                    }

                //処分方法
                case r_framework.Const.WINDOW_ID.M_SHOBUN_HOUHOU:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
                        var dt = dao.GetAllValidData(new M_SHOBUN_HOUHOU() { SHOBUN_HOUHOU_CD = cd.Text, KAMI_USE_KBN = true });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "処分方法";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].SHOBUN_HOUHOU_NAME; //正式名
                        }
                        break;
                    }

                //車種
                case r_framework.Const.WINDOW_ID.M_SHASHU:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                        var dt = dao.GetAllValidData(new M_SHASHU() { SHASHU_CD = cd.Text });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "車種";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].SHASHU_NAME; //正式名
                        }
                        break;
                    }

                //運搬方法
                case r_framework.Const.WINDOW_ID.M_UNPAN_HOUHOU:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_UNPAN_HOUHOUDao>();
                        var dt = dao.GetAllValidData(new M_UNPAN_HOUHOU() { UNPAN_HOUHOU_CD = cd.Text, KAMI_USE_KBN = true });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "運搬方法";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].UNPAN_HOUHOU_NAME; //正式名
                        }
                        break;
                    }

                //取引先
                case r_framework.Const.WINDOW_ID.M_TORIHIKISAKI:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                        var dt = dao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = cd.Text });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "取引先";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].TORIHIKISAKI_NAME1 ?? "" + dt[0].TORIHIKISAKI_NAME2 ?? ""; //正式名
                        }
                        break;
                    }

                //廃棄物種類
                case r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI:
                    {
                        if (string.IsNullOrEmpty(haikiShuruiCdFrom) || string.IsNullOrEmpty(haikiShuruiCdTo))
                        {
                            //一方でも空だと何もしない
                        }
                        else
                        {
                            //両方入っている場合比較（最小値以上、最大値以下で判断する）
                            if (string.Compare(haikiShuruiCdFrom, cd.Text) <= 0 && string.Compare(haikiShuruiCdTo, cd.Text) >= 0)
                            {
                                //範囲以内ならOK
                            }
                            else
                            {
                                //範囲外になる場合はマスタなしとみなす
                                errTable = "廃棄物種類";
                                hasErr = true;
                                break;
                            }
                        }

                        var dao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
                        var dt = dao.GetAllValidData(new M_HAIKI_SHURUI() { HAIKI_KBN_CD = haikiKbnCd, HAIKI_SHURUI_CD = cd.Text });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "廃棄物種類";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].HAIKI_SHURUI_NAME; //正式名
                        }
                        break;
                    }

                //形状
                case r_framework.Const.WINDOW_ID.M_KEIJOU:
                    {
                        if (string.IsNullOrEmpty(haikiShuruiCdFrom) || string.IsNullOrEmpty(haikiShuruiCdTo))
                        {
                            //一方でも空だと何もしない
                        }
                        else
                        {
                            //両方入っている場合比較（最小値以上、最大値以下で判断する）
                            if (string.Compare(haikiShuruiCdFrom, cd.Text) <= 0 && string.Compare(haikiShuruiCdTo, cd.Text) >= 0)
                            {
                                //範囲以内ならOK
                            }
                            else
                            {
                                //範囲外になる場合はマスタなしとみなす
                                errTable = "形状";
                                hasErr = true;
                                break;
                            }
                        }

                        var dao = DaoInitUtility.GetComponent<IM_KEIJOUDao>();
                        var dt = dao.GetAllValidData(new M_KEIJOU() { KEIJOU_CD = cd.Text });
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "形状";
                            hasErr = true;
                        }
                        else
                        {
                            nameValue = dt[0].KEIJOU_NAME; //正式名
                        }
                        break;
                    }
            }

            if (hasErr)
            {
                name.Text = ""; //クリア
                cd.IsInputErrorOccured = true; //エラーフラグ
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E020", errTable);
                return true; //異常はtrue
            }

            name.Text = nameValue;
            return false; //正常はfalse
        }

        #endregion

        #region 車輌(車両) ポップアップ＋validatingセットアップ

        /// <summary>
        /// 車輌(車両)ポップアップとValidatingイベントのセットアップ処理（イベントハンドラを強制クリアするので注意）
        /// </summary>
        public static void SetupSyaryouCd(
            CustomTextBox cd, CustomTextBox name, CustomTextBox ShashuCd, CustomTextBox ShashuName,
            CustomTextBox GyoushaCd, CustomTextBox GyoushaName, CustomTextBox GyoushaTel, CustomTextBox GyoushaAddr, CustomTextBox GyoushaPost,
            CustomTextBox TenkiGyoshaCd, CustomTextBox TenkiGyoshaName, CustomDateTimePicker kofuDate)
        {
            ClearEvent(cd); //イベントクリア

            cd.FocusOutCheckMethod.Clear();
            cd.RegistCheckMethod.Clear();

            cd.PopupWindowName = "車両選択共通ポップアップ";
            cd.PopupBeforeExecuteMethod = "";
            cd.PopupAfterExecuteMethod = "";
            cd.PopupDataHeaderTitle = null;
            cd.PopupDataSource = null;
            cd.PopupSearchSendParams.Clear();
            cd.popupWindowSetting.Clear();
            cd.PopupTitleLabel = "";
            cd.popupWindowSetting.Clear();
            cd.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHARYOU;

            //業者コードと連携
            cd.PopupSearchSendParams.Add(new PopupSearchSendParamDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                KeyName = "key001",
                Control = GyoushaCd.Name
            });

            //車種と連携
            cd.PopupSearchSendParams.Add(new PopupSearchSendParamDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                KeyName = "key003",
                Control = ShashuCd.Name
            });

            //key002が車輌でKey004が運転者
            //削除フラグと連携
            cd.PopupSearchSendParams.Add(new PopupSearchSendParamDto()
            {
                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                KeyName = "TEKIYOU_FLG",
                Value = "FALSE" 
            });

            //戻り設定
            cd.PopupGetMasterField = "";//未使用
            cd.GetCodeMasterField = cd.PopupGetMasterField;
            // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 start
            //cd.PopupSetFormField = string.Join(",", new string[] { cd.Name, "xxxxxxxx", GyoushaCd.Name, "xxxxxxxx", ShashuCd.Name, "xxxxxxxx", "xxxxxxxx", "xxxxxxxx" }); //車輌、業者、車種、運転者の順
            cd.PopupSetFormField = string.Join(",", new string[] { cd.Name, "xxxxxxxx", GyoushaCd.Name, "xxxxxxxx", ShashuCd.Name, ShashuName.Name, "xxxxxxxx", "xxxxxxxx" }); //車輌、業者、車種、運転者の順
            // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 end
            cd.SetFormField = cd.PopupSetFormField;

            //validatingイベント設定（手入力対応）

            cd.Validating += (sender, e) =>
            {
                if (e.Cancel)
                {
                    return;
                }

                e.Cancel = SharyouPopupAfterBase(false, cd, name, ShashuCd, ShashuName, GyoushaCd, GyoushaName, GyoushaTel, GyoushaAddr, GyoushaPost, TenkiGyoshaCd, TenkiGyoshaName, null, kofuDate);
            };

            var bk = new bkupSharyou();

            //ポップアップビフォア対応
            cd.PopupBeforeExecute = (sender) =>
            {
                //前回値保存
                // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 start
                //bk.bkShashuCd = ShashuName.Text;
                bk.bkShashuCd = ShashuCd.Text;
                // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 end
                bk.bkGyoushaCd = GyoushaCd.Text;
            };

            //ポップアップアフター対応
            cd.PopupAfterExecute = (sender, result) =>
            {
                //車輌は対応できていなかった
                //if (result == DialogResult.Cancel)
                //{
                //    return;
                //}

                //車輌コード
                SharyouPopupAfterBase(true, cd, name, ShashuCd, ShashuName, GyoushaCd, GyoushaName, GyoushaTel, GyoushaAddr, GyoushaPost, TenkiGyoshaCd, TenkiGyoshaName, bk, kofuDate);
            };
        }

        public class bkupSharyou
        {
            public string bkGyoushaCd { get; set; }
            public string bkShashuCd { get; set; }
        }

        /// <summary>
        /// 単位のvalidatingイベント。コード未入力だと名前をクリア。入力有だとマスタチェックし名前セットorエラーメッセージ
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        public static bool SharyouPopupAfterBase(
            bool isPopupafter,
            CustomTextBox cd, CustomTextBox name, CustomTextBox ShashuCd, CustomTextBox ShashuName,
            CustomTextBox GyoushaCd, CustomTextBox GyoushaName, CustomTextBox GyoushaTel, CustomTextBox GyoushaAddr, CustomTextBox GyoushaPost,
            CustomTextBox TenkiGyoshaCd, CustomTextBox TenkiGyoshaName,
            bkupSharyou bk, CustomDateTimePicker kofuDate)
        {
            if (string.IsNullOrEmpty(cd.Text) && cd.prevText != null && cd.prevText.Equals(cd.Text))
            {
                return false;
            }

            //変更なければ処理しない
            // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 start
            //if (!cd.isChanged())
            if (!cd.isChanged() && bk != null && bk.bkGyoushaCd == GyoushaCd.Text && bk.bkShashuCd == ShashuCd.Text)
            // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 end
            {
                return false;
            }

            //空になった場合
            if (string.IsNullOrEmpty(cd.Text))
            {
                name.Text = ""; //クリア
                return false; //正常はfalse
            }

            string nameValue = "";
            string errTable = "";
            bool hasErr = false;

            switch (cd.PopupWindowId)
            {
                //形状
                case r_framework.Const.WINDOW_ID.M_SHARYOU:
                    {
                        var dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();

                        var search = new M_SHARYOU();

                        search.SHARYOU_CD = cd.Text;

                        if (string.IsNullOrEmpty(GyoushaCd.Text))
                        {
                            // 20140617 katen 不具合No.4834 start‏
                            //search.GYOUSHA_CD = ""; //nullではなく空文字！
                            search.GYOUSHA_CD = null;
                            // 20140617 katen 不具合No.4834 end‏
                        }
                        else
                        {
                            search.GYOUSHA_CD = GyoushaCd.Text; //nullではなく空文字！
                        }

                        if (string.IsNullOrEmpty(ShashuCd.Text))
                        {
                            // 20140617 katen 不具合No.4834 start‏
                            //search.SHASYU_CD = ""; //nullではなく空文字！
                            search.SHASYU_CD = null;
                            // 20140617 katen 不具合No.4834 end‏
                        }
                        else
                        {
                            search.SHASYU_CD = ShashuCd.Text; //nullではなく空文字！
                        }

                        SqlDateTime kofuDateValue = SqlDateTime.Null;
                        if (kofuDate.Value != null)
                        {
                            kofuDateValue = SqlDateTime.Parse(kofuDate.Value.ToString());
                        }
                        var dt = dao.GetAllValidDataForGyoushaKbn(search, "9", kofuDateValue, true, false, true);
                        //0件
                        if (dt == null || dt.Length == 0)
                        {
                            errTable = "車輌";
                            hasErr = true;
                        }
                        // 20140617 katen 不具合No.4834 start‏
                        //else
                        else if (dt.Length == 1)
                        // 20140617 katen 不具合No.4834 end‏
                        {
                            if (!(cd as CustomTextBox).isChanged())
                            {
                                return false;
                            }
                            // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                            bool setGyoshaData = (bk != null && string.IsNullOrEmpty(bk.bkGyoushaCd)) || (bk == null && string.IsNullOrEmpty(GyoushaCd.Text));
                            // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                            //hack:複数件でも一旦最初の行を採用
                            nameValue = dt[0].SHARYOU_NAME; //正式名

                            //業者と車種も反映
                            GyoushaCd.Text = dt[0].GYOUSHA_CD;

                            // 20140704 chinchisi EV004912_車種CD、車輌CDをクリアした後に再度車輌CDを入力し直すと車種名がセットされない。 start
                            string prevtext = ShashuCd.Text;
                            ShashuCd.prevText = prevtext;
                            // 20140704 chinchisi EV004912_車種CD、車輌CDをクリアした後に再度車輌CDを入力し直すと車種名がセットされない。 end

                            ShashuCd.Text = dt[0].SHASYU_CD;

                            //車輌の場合は、業者の区分は無視・・・？車輌マスタの
                            // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 start
                            //SetAddrGyousha("All", GyoushaCd, GyoushaName, GyoushaPost, GyoushaTel, GyoushaAddr, null, null, null, false, false, false, false);
                            // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない start
                            //if (bk != null && string.IsNullOrEmpty(bk.bkGyoushaCd))
                            //{
                            //    SetAddrGyousha("All", GyoushaCd, GyoushaName, GyoushaPost, GyoushaTel, GyoushaAddr, null, null, null, false, false, false, false);
                            //}
                            if (setGyoshaData)
                            {
                                SetAddrGyousha("All", GyoushaCd, GyoushaName, GyoushaPost, GyoushaTel, GyoushaAddr, "All", TenkiGyoshaCd, TenkiGyoshaName, false, false, false, false);
                            }
                            // 20140618 katen EV004735 車輌を入力した時に運搬受託者が自動入力されるが、運搬の受託が自動的に入らない end
                            // 20140603 kayo 不具合#4387　運搬受託者の車種や車輌を入力すると、名称や住所が再読み込みされてしまう対応 end
                            XxxPopupAfterBase(ShashuCd, 0, null, null);
                        }
                        // 20140617 katen 不具合No.4834 start‏
                        else if (bk == null)
                        {
                            //業者コードと連携
                            cd.PopupSearchSendParams.Add(new PopupSearchSendParamDto()
                            {
                                And_Or = r_framework.Const.CONDITION_OPERATOR.AND,
                                KeyName = "key002",
                                Control = cd.Name
                            });
                            // 検索ポップアップ起動
                            cd.PopUp();
                            cd.PopupSearchSendParams.RemoveAt(cd.PopupSearchSendParams.Count - 1);
                            cd.Focus();
                            nameValue = name.Text;
                        }
                        // 20140617 katen 不具合No.4834 end‏
                        break;
                    }
            }

            if (hasErr)
            {
                cd.IsInputErrorOccured = true; //エラーフラグ
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E020", errTable);
                return true; //異常はtrue
            }

            name.Text = nameValue;
            return false; //正常はfalse
        }

        //イベントクリア処理

        internal static void ClearEvent(System.ComponentModel.Component component)
        {
            if (component == null)
            {
                return;
            }

            System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
            System.ComponentModel.EventHandlerList evList = typeof(System.ComponentModel.Component).GetField("events", flag).GetValue(component) as System.ComponentModel.EventHandlerList;
            if (evList == null)
            {
                return;
            }

            object evEntryData = evList.GetType().GetField("head", flag).GetValue(evList);
            if (evEntryData == null)
            {
                return;
            }
            do
            {
                object key = evEntryData.GetType().GetField("key", flag).GetValue(evEntryData);
                if (key == null)
                {
                    break;
                }
                evList[key] = null;
                evEntryData = evEntryData.GetType().GetField("next", flag).GetValue(evEntryData);
            }
            while (evEntryData != null);
        }

        #endregion

        #endregion

        /// <summary>
        /// 交付担当者セット
        /// 交付所属（産廃のみの項目）は未対応
        /// </summary>
        /// <param name="GyoushCd">排出事業者</param>
        /// <param name="GenbaCd">排出事業場</param>
        /// <param name="KoufuTantousha">交付担当者</param>
        public static void GetKoufuTantousha(CustomTextBox GyoushCd, CustomTextBox GenbaCd, CustomTextBox KoufuTantousha)
        {
            //交付担当者を引用 ※建廃専用処理

            if (string.IsNullOrEmpty(GyoushCd.Text) || string.IsNullOrEmpty(GenbaCd.Text))
            {
                return; //業者と現場の片方でも空だと何もしない
            }

            var dto = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            var entity = dto.GetAllValidData(new r_framework.Entity.M_GENBA()
            {
                GYOUSHA_CD = GyoushCd.Text,
                GENBA_CD = GenbaCd.Text,
                HAISHUTSU_NIZUMI_GENBA_KBN = true
            });

            //値が入っていたら
            if (entity != null && entity.Length == 1 && !string.IsNullOrEmpty(entity[0].KOUFU_TANTOUSHA))
            {
                KoufuTantousha.Text = entity[0].KOUFU_TANTOUSHA;
            }
            else
            {
                // 該当情報が存在しない場合はブランク
                KoufuTantousha.Text = string.Empty;
            }
        }

        /// <summary>
        /// 交付担当者セット
        /// 交付所属（産廃のみの項目）は未対応
        /// </summary>
        /// <param name="GyoushCd">排出事業者</param>
        /// <param name="GenbaCd">排出事業場</param>
        /// <param name="KoufuTantousha">交付担当者</param>
        /// <param name="isNotNeedDeleteFlg">削除フラグの条件の有無</param>
        public static void GetKoufuTantousha(CustomTextBox GyoushCd, CustomTextBox GenbaCd, CustomTextBox KoufuTantousha, bool isNotNeedDeleteFlg)
        {
            //交付担当者を引用 ※建廃専用処理

            if (string.IsNullOrEmpty(GyoushCd.Text) || string.IsNullOrEmpty(GenbaCd.Text))
            {
                return; //業者と現場の片方でも空だと何もしない
            }

            var dto = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            var entity = dto.GetAllValidData(new r_framework.Entity.M_GENBA()
            {
                GYOUSHA_CD = GyoushCd.Text,
                GENBA_CD = GenbaCd.Text,
                HAISHUTSU_NIZUMI_GENBA_KBN = true,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg
            });

            //値が入っていたら
            if (entity != null && entity.Length == 1 && !string.IsNullOrEmpty(entity[0].KOUFU_TANTOUSHA))
            {
                KoufuTantousha.Text = entity[0].KOUFU_TANTOUSHA;
            }
            else
            {
                // 該当情報が存在しない場合はブランク
                KoufuTantousha.Text = string.Empty;
            }
        }

        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる start
        /// <summary>
        /// 1次,2次で、自身に紐付情報があるかチェック
        /// </summary>
        /// <param name="SYSTEM_ID"></param>
        /// <returns></returns>
        //public bool ChkRelation(SqlInt64 SYSTEM_ID)
        //{
        //    var dt = EntryDao.GetRelation(SYSTEM_ID);
        //    return (dt.Rows.Count > 0); //データがあったら紐付あり
        //}
        public bool ChkRelation(SqlInt64 SYSTEM_ID, int maniFlg)
        {
            var dt = new DataTable();

            if (maniFlg == 1)
            {
                // 一次
                dt = EntryDao.GetRelationF(SYSTEM_ID);
            }
            else
            {
                // 二次
                dt = EntryDao.GetRelationS(SYSTEM_ID);
            }
            return (dt.Rows.Count > 0); //データがあったら紐付あり
        }

        /// <summary>
        /// 1次detail自身に紐付情報があるかチェック
        /// </summary>
        /// <param name="FIRST_SYSTEM_ID"></param>
        /// <returns></returns>
        public bool ChkRelationDetail(Object FIRST_SYSTEM_ID)
        {
            if (FIRST_SYSTEM_ID == null)
            {
                return false;
            }

            var dt = EntryDao.GetRelationD(SqlInt64.Parse(FIRST_SYSTEM_ID.ToString()));

            return (dt.Rows.Count > 0); //データがあったら紐付あり
        }

        // 20140620 kayo 不具合#4926　紐付いたマニフェストが削除されたら、データの不整合に生じる end

        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
        /// <summary>
        /// 2次detail自身に紐付情報があるかチェック
        /// </summary>
        /// <param name="FIRST_SYSTEM_ID"></param>
        /// <returns></returns>
        public bool ChkRelationDetail2(Object SECOND_SYSTEM_ID)
        {
            if (SECOND_SYSTEM_ID == null)
            {
                return false;
            }

            var dt = EntryDao.GetRelationD2(SqlInt64.Parse(SECOND_SYSTEM_ID.ToString()));

            return (dt.Rows.Count > 0); //データがあったら紐付あり
        }
        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

        #region 交付番号 入力チェック関係

        /// <summary>
        /// 交付番号のテキストボックス
        /// </summary>
        public CustomAlphaNumTextBox cantxt_KohuNo { get; private set; }

        /// <summary>
        /// 交付番号の通常ラジオボタン
        /// </summary>
        public CustomRadioButton crdo_KohuTujyo { get; private set; }

        /// <summary>
        /// 交付番号の例外ラジオボタン
        /// </summary>
        public CustomRadioButton crdo_KohuReigai { get; private set; }

        /// <summary>
        /// 設定、イベントの初期化
        /// 基本的にはEventInitで呼び出してください。
        /// </summary>
        /// <param name="Tujyo">通常のラジオボタン</param>
        /// <param name="Reigai">例外のラジオボタン</param>
        /// <param name="koufuNo">交付番号を入力するテキストボックス</param>
        public void SetupKoufuNo(CustomRadioButton Tujyo, CustomRadioButton Reigai, CustomAlphaNumTextBox koufuNo)
        {
            this.cantxt_KohuNo = koufuNo;
            this.crdo_KohuTujyo = Tujyo;
            this.crdo_KohuReigai = Reigai;

            Tujyo.CheckedChanged -= crdo_KohuTujyo_CheckedChanged;
            Tujyo.CheckedChanged += crdo_KohuTujyo_CheckedChanged;

            //通常だけでON/OFF判断するので例外は不要

            //交付番号のテキストボックスは、画面側で制御する。（修正モード切り換えもあるので）
        }

        /// <summary>
        /// 交付番号の通常ラジオボタン用のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdo_KohuTujyo_CheckedChanged(object sender, EventArgs e)
        {
            var crdo = (CustomRadioButton)sender;

            SetupKoufuNo(crdo.Checked);
        }

        /// <summary>
        /// 交付番号のプロパティを設定します
        /// </summary>
        /// <param name="isNormal">true:通常 false:例外</param>
        private void SetupKoufuNo(bool isNormal)
        {
            LogUtility.DebugMethodStart(isNormal);
            if (isNormal)
            {
                //通常は11ケタ
                cantxt_KohuNo.CharactersNumber = 11;
                cantxt_KohuNo.MaxLength = 11;
                cantxt_KohuNo.AlphabetLimitFlag = false; //alphabet入力不可
                cantxt_KohuNo.Tag = "半角11桁以内で入力してください";
            }
            else
            {
                //hack:例外は20ケタだが 今は11で仮置き
                cantxt_KohuNo.CharactersNumber = 11;
                cantxt_KohuNo.MaxLength = 11;
                cantxt_KohuNo.AlphabetLimitFlag = true; //alphabet入力可
                cantxt_KohuNo.ChangeUpperCase = true; //自動大文字変換
                cantxt_KohuNo.Tag = "半角11桁以内で入力してください";
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 交付番号チェック
        /// </summary>
        /// <param name="KoufuNo">交付番号</param>
        /// <param name="isNormal">true:通常 false:例外</param>
        /// <returns>空文字だと異常なし。それ以外はメッセージを出すようにしてください。</returns>
        static public string ChkKoufuNo(string KoufuNo, bool isNormal)
        {
            LogUtility.DebugMethodStart(KoufuNo, isNormal);
            string ret = string.Empty;
            if (string.IsNullOrEmpty(KoufuNo))
            {
                //空の場合は正常扱い
                LogUtility.DebugMethodEnd(ret);
                return ret; //正常
            }

            if (isNormal)
            {
                //通常の場合

                //11ケタ数値であること
                if (KoufuNo.Length != 11 ||
                    System.Text.RegularExpressions.Regex.IsMatch(KoufuNo, "^[0-9]+$") == false)
                {
                    ret = string.Format(Message.MessageUtility.GetMessageString("E012"), "11桁の数値");
                    LogUtility.DebugMethodEnd(ret);
                    return ret; //異常
                }

                //CD判定
                string msg = ChkDigitKoufuNo(KoufuNo);
                if (!string.IsNullOrEmpty(msg))
                {
                    ret = string.Format(Message.MessageUtility.GetMessageString("E038"), msg);
                    LogUtility.DebugMethodEnd(ret);
                    return ret; //異常
                }
            }
            else
            {
                //特殊の場合 CDや長さ無視
                if (System.Text.RegularExpressions.Regex.IsMatch(KoufuNo, "^[A-Z0-9]+$") == false)
                {
                    ret = string.Format(Message.MessageUtility.GetMessageString("E012"), "11桁内の英数字");//hack:仮置きで11文字  最終的には20文字
                    LogUtility.DebugMethodEnd(ret);
                    return ret; //異常
                }

                //IOも許可するように修正（もしかしたら戻すかもしれないのでコメントで残す）
                //if (System.Text.RegularExpressions.Regex.IsMatch(KoufuNo, "[IO]+")) //IかOを含む場合
                //{
                //    ret = string.Format(Message.MessageUtility.GetMessageString("E012"), "11桁内の英数字(大文字、ただしIとOを除く)");//hack:仮置きで11文字  最終的には20文字
                //    LogUtility.DebugMethodEnd(ret);
                //    return ret; //異常
                //}
            }

            LogUtility.DebugMethodEnd(ret);
            return ret; //正常
        }

        /// <summary>
        /// 交付番号チェック
        /// </summary>
        /// <param name="KoufuNo">交付番号11ケタ</param>
        /// <returns>正常時は空、誤っている場合チェックディジット</returns>
        public static string ChkDigitKoufuNo(string KoufuNo)
        {
            LogUtility.DebugMethodStart(KoufuNo);
            string ret = string.Empty;
            string tmp1 = string.Empty;
            string tmp2 = string.Empty;
            long lA = 0;
            long lB = 0;
            long lC = 0;
            long lD = 0;
            long lZ = 0;

            if (string.IsNullOrEmpty(KoufuNo))
            {
                LogUtility.DebugMethodEnd(string.Empty);
                return string.Empty;
            }

            tmp1 = KoufuNo.Substring(0, 10);
            lA = Convert.ToInt64(tmp1);
            tmp2 = KoufuNo.Substring(10, 1);
            lZ = Convert.ToInt64(tmp2);
            lB = lA / 7;
            lC = lB * 7;
            lD = lA - lC;
            if (lD != lZ)
            {
                ret = lD.ToString();
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 数値フォーマット関係

        /// <summary>
        /// 数値コントロールフォーマット一括設定
        /// </summary>
        /// <param name="ManifestSuuryoFormat">マニフェストフォーマット（#,###.000 等）</param>
        /// <param name="txts">対象コントロール</param>
        public void SetupNumericFormat(string ManifestSuuryoFormat, params CustomNumericTextBox2[] txts)
        {
            LogUtility.DebugMethodStart(ManifestSuuryoFormat, txts);

            foreach (var t in txts)
            {
                LogUtility.Debug("Name=" + t.Name);

                // 数量コントロールの入力範囲を「0～999999999.9999」に設定
                t.MaxLength = 0;
                t.CharactersNumber = 0;
                t.RangeSetting.Max = 999999999.9999M;
                t.RangeSetting.Min = 0;
                // 20151027 呉 マニフォーマットを直接設定する Start
                //t.RangeLimitFlag = true;
                //t.FormatSetting = "カスタム";
                //t.CustomFormatSetting = ManifestSuuryoFormat;
                t.FormatSetting = "システム設定(マニフェスト書式)";
                // 20151027 呉 マニフォーマットを直接設定する End
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 数値コントロールフォーマット一括設定（DGV用）
        /// </summary>
        /// <param name="ManifestSuuryoFormat">マニフェストフォーマット（#,###.000 等）</param>
        /// <param name="txts">対象コントロール</param>
        public void SetupNumericFormatDgv(string ManifestSuuryoFormat, params DgvCustomNumericTextBox2Column[] txts)
        {
            LogUtility.DebugMethodStart(ManifestSuuryoFormat, txts);

            foreach (var t in txts)
            {
                LogUtility.Debug("Name=" + t.Name);

                // 数量コントロールの入力範囲を「0～999999999.9999」に設定
                t.MaxInputLength = 0;
                t.CharactersNumber = 0;
                t.RangeSetting.Max = 999999999.9999M;
                t.RangeSetting.Min = 0;
                // 20151027 呉 マニフォーマットを直接設定する Start
                //t.RangeLimitFlag = true;
                //t.FormatSetting = "カスタム";
                //t.CustomFormatSetting = ManifestSuuryoFormat;
                t.FormatSetting = "システム設定(マニフェスト書式)";
                // 20151027 呉 マニフォーマットを直接設定する End
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 最終処分終了報告、最終処分終了取消用エラーチェック

        /// <summary>
        /// LastSbnEndrepPopup内で使用するCELL名
        /// </summary>
        private readonly string CELL_NAME_KANRI_ID = "KANRI_ID";

        private readonly string CELL_NAME_LATEST_SEQ = "LATEST_SEQ";
        private readonly string CELL_NAME_STATUS_FLAG = "STATUS_FLAG";
        private readonly string CELL_NAME_STATUS_DETAIL = "STATUS_DETAIL";
        private readonly string CELL_NAME_APPROVAL_SEQ = "APPROVAL_SEQ";

        /// <summary>
        /// 最終処分終了報告、最終処分終了取消用エラーチェック
        /// dt内にKANRI_ID、STATUS_FLAG、APPROVAL_SEQ、FUNCTION_IDが必ず設定されていること
        /// </summary>
        /// <param name="dt">最終処分終了報告、最終処分終了取消対象</param>
        /// <param name="unModifiedKanriIdList">更新不可の管理番号リスト</param>
        /// <returns>true:正常、false:異常</returns>
        public bool ChkLastSbnEndrepReport(DataTable dt, bool isLastSbnEndrepFlg, out List<string> unModifiedKanriIdList)
        {
            unModifiedKanriIdList = new List<string>();

            if (!dt.Columns.Contains(CELL_NAME_KANRI_ID)
                || !dt.Columns.Contains(CELL_NAME_STATUS_FLAG)
                || !dt.Columns.Contains(CELL_NAME_APPROVAL_SEQ))
            {
                return false;
            }

            List<QUE_INFO> allSearchCoditionList = new List<QUE_INFO>();

            foreach (DataRow row in dt.Rows)
            {
                if (row == null)
                {
                    continue;
                }

                var kanriId = row[CELL_NAME_KANRI_ID].ToString();
                var latesSeq = row[CELL_NAME_LATEST_SEQ].ToString();
                var approvalSeq = row[CELL_NAME_APPROVAL_SEQ].ToString();

                if (string.IsNullOrEmpty(kanriId)
                    || string.IsNullOrEmpty(latesSeq))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(row[CELL_NAME_STATUS_FLAG].ToString())
                    && !CommonConst.DT_MF_TOC_STATUS_FLAG_REFISTERED.Equals(row[CELL_NAME_STATUS_FLAG].ToString()))
                {
                    // 状態フラグが4以外のときは新規登録や保留登録中
                    if (!unModifiedKanriIdList.Contains(kanriId))
                    {
                        unModifiedKanriIdList.Add(kanriId);
                    }
                }
                else if (!string.IsNullOrEmpty(row[CELL_NAME_STATUS_DETAIL].ToString())
                    && CommonConst.DT_MF_TOC_STATUS_FLAG_REFISTERED.Equals(row[CELL_NAME_STATUS_FLAG].ToString())
                    && CommonConst.DT_MF_TOC_STATUS_DETAIL_MODIFYING.Equals(row[CELL_NAME_STATUS_DETAIL].ToString()))
                {
                    // 状態フラグが4以外のときは新規登録や保留登録中
                    if (!unModifiedKanriIdList.Contains(kanriId))
                    {
                        unModifiedKanriIdList.Add(kanriId);
                    }
                }
                else if (!string.IsNullOrEmpty(row[CELL_NAME_APPROVAL_SEQ].ToString()))
                {
                    // APPROVAL_SEQがnull以外のときは修正中
                    if (!unModifiedKanriIdList.Contains(kanriId))
                    {
                        var commonUqeInfoDao2 = DaoInitUtility.GetComponent<CommonQueInfoDaoCls>();
                        var queInfo = commonUqeInfoDao2.GetQue_SeqInfoForHoryuDel(kanriId, approvalSeq);

                        // SQLにヒットしたら対象外
                        if (!(queInfo != null && queInfo.Rows.Count > 0))
                        {
                            unModifiedKanriIdList.Add(kanriId);
                        }
                    }
                }

                // QUE_INFOチェック用にセット
                var checkData = allSearchCoditionList.Where(w => kanriId.Equals(w.KANRI_ID));
                if (checkData == null || checkData.Count() < 1)
                {
                    QUE_INFO que = new QUE_INFO();
                    que.KANRI_ID = kanriId;
                    que.SEQ = Convert.ToDecimal(latesSeq);
                    if (isLastSbnEndrepFlg)
                    {
                        que.FUNCTION_ID = CommonConst.FUNCTION_ID_2000;
                    }
                    else
                    {
                        que.FUNCTION_ID = CommonConst.FUNCTION_ID_2100;
                    }
                    allSearchCoditionList.Add(que);
                }
            }

            // QUE_INFOのFUNCTION_IDチェック
            // 既に最終処分終了報告または最終処分終了取消が実行されていないかチェック
            foreach (var tempQueInfo in allSearchCoditionList)
            {
                if (unModifiedKanriIdList.Contains(tempQueInfo.KANRI_ID))
                {
                    // 無駄にDBアクセスさせないためのチェック
                    continue;
                }

                var commonUqeInfoDao = DaoInitUtility.GetComponent<CommonQueInfoDaoCls>();
                var queInfo = commonUqeInfoDao.GetQue_SeqInfoForLastSbnEndRepFunction(tempQueInfo);

                // ヒットしたらNG
                if ((queInfo != null && queInfo.Rows.Count > 0))
                {
                    unModifiedKanriIdList.Add(tempQueInfo.KANRI_ID);
                }
            }

            return !(unModifiedKanriIdList.Count > 0);
        }

        #endregion

        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start
        /// <summary>
        /// コントロールを作成
        /// </summary>
        /// <param name="parentbaseform">親フォーム</param>
        /// <param name="c">利用されるかもしれないコントロールリスト</param>
        /// <returns></returns>
        public static void MakeControl(r_framework.APP.Base.BusinessBaseForm parentbaseform, List<Control> c)
        {
            LogUtility.DebugMethodStart(parentbaseform, c);
            RangeSettingDto rangeSettingDto = new RangeSettingDto();
            Panel panel1 = new Panel();
            Panel panel2 = new Panel();
            Label lab = new Label();
            CustomNumericTextBox2 textBox = new CustomNumericTextBox2();
            CustomRadioButton radbtn_do = new CustomRadioButton();
            CustomRadioButton radbtn_no = new CustomRadioButton();

            panel1.SuspendLayout();
            panel2.SuspendLayout();
            parentbaseform.SuspendLayout();

            lab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            lab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lab.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            lab.ForeColor = System.Drawing.Color.White;
            lab.Location = new System.Drawing.Point(0, 0);
            lab.Name = "ManifestCommonLabel1";
            lab.Size = new System.Drawing.Size(150, 20);
            lab.TabIndex = 4;
            lab.Text = "パターン継続入力";
            lab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            textBox.BackColor = System.Drawing.SystemColors.Window;
            textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox.DefaultBackColor = System.Drawing.Color.Empty;
            textBox.DisplayItemName = "パターン継続入力";
            textBox.DisplayPopUp = null;
            textBox.FocusOutCheckMethod = null;
            textBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            textBox.ForeColor = System.Drawing.Color.Black;
            textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            textBox.IsInputErrorOccured = false;
            textBox.LinkedRadioButtonArray = new string[] {
        "radbtn_Do",
        "radbtn_No"};
            textBox.Location = new System.Drawing.Point(0, 22);
            textBox.MaxLength = 1;
            textBox.Name = "txtNum_PatternContinue";
            textBox.PopupAfterExecute = null;
            textBox.PopupBeforeExecute = null;
            textBox.PopupSearchSendParams = null;
            textBox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            textBox.popupWindowSetting = null;
            textBox.prevText = null;
            textBox.PrevText = null;
            rangeSettingDto.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            textBox.RangeSetting = rangeSettingDto;
            textBox.RegistCheckMethod = null;
            textBox.Size = new System.Drawing.Size(15, 20);
            textBox.TabIndex = 0;
            textBox.TabStop = false;
            textBox.Tag = "【1、2】のいずれかで入力してください";
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox.Validated += (sender, e) =>
            {
                CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
                if (string.IsNullOrEmpty(text.Text))
                {
                    //パタン継続入力が空の場合、メッセージ「パタン継続入力は必須項目です。入力してください。」を表示する
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                    //フォーカスを抽出対象区分へ移動
                    text.Select();
                }
            };

            radbtn_do.AutoSize = true;
            radbtn_do.DefaultBackColor = System.Drawing.Color.Empty;
            radbtn_do.FocusOutCheckMethod = null;
            radbtn_do.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            radbtn_do.LinkedTextBox = "txtNum_PatternContinue";
            radbtn_do.Location = new System.Drawing.Point(1, 1);
            radbtn_do.Name = "radbtn_Do";
            radbtn_do.PopupAfterExecute = null;
            radbtn_do.PopupBeforeExecute = null;
            radbtn_do.PopupSearchSendParams = null;
            radbtn_do.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            radbtn_do.popupWindowSetting = null;
            radbtn_do.RegistCheckMethod = null;
            radbtn_do.Size = new System.Drawing.Size(59, 16);
            radbtn_do.TabIndex = 0;
            radbtn_do.Tag = "パターン継続入力が「1.する」の場合にはチェックを付けてください";
            radbtn_do.Text = "1.する";
            radbtn_do.UseVisualStyleBackColor = true;
            radbtn_do.Value = "1";

            radbtn_no.AutoSize = true;
            radbtn_no.DefaultBackColor = System.Drawing.Color.Empty;
            radbtn_no.FocusOutCheckMethod = null;
            radbtn_no.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            radbtn_no.LinkedTextBox = "txtNum_PatternContinue";
            radbtn_no.Location = new System.Drawing.Point(62, 1);
            radbtn_no.Name = "radbtn_No";
            radbtn_no.PopupAfterExecute = null;
            radbtn_no.PopupBeforeExecute = null;
            radbtn_no.PopupSearchSendParams = null;
            radbtn_no.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            radbtn_no.popupWindowSetting = null;
            radbtn_no.RegistCheckMethod = null;
            radbtn_no.Size = new System.Drawing.Size(76, 16);
            radbtn_no.TabIndex = 302;
            radbtn_no.Tag = "パターン継続入力が「2.しない」の場合にはチェックを付けてください";
            radbtn_no.Text = "2.しない";
            radbtn_no.UseVisualStyleBackColor = true;
            radbtn_no.Value = "2";

            panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel2.Controls.Add(radbtn_do);
            panel2.Controls.Add(radbtn_no);
            panel2.Location = new System.Drawing.Point(14, 22);
            panel2.Name = "ManifestCommonPanel2";
            panel2.Size = new System.Drawing.Size(136, 20);
            panel2.TabIndex = 5;

            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            panel1.Controls.Add(lab);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(panel2);
            panel1.Location = new System.Drawing.Point(1024, 156);
            panel1.Name = "ManifestCommonPanel1";
            panel1.Size = new System.Drawing.Size(150, 44);
            panel1.TabIndex = 4;

            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();

            parentbaseform.Controls.Add(panel1);
            parentbaseform.ResumeLayout(false);
            parentbaseform.PerformLayout();
            if (c == null)
            {
                c = new List<Control>();
            }
            c.Add(lab);
            c.Add(textBox);
            LogUtility.DebugMethodEnd();
        }

        // 20140620 katen EV004842 パターンを呼び出しで連続入力したい場合がある start

        /// <summary>
        /// <summary>
        /// コントロール(業者CD/現場CD)を作成
        /// </summary>
        /// <param name="parentbaseform">親フォーム</param>
        /// <param name="c">利用されるかもしれないコントロールリスト</param>
        /// <returns></returns>
        public static void MakeGyoushaGenbaControl(r_framework.APP.Base.BusinessBaseForm parentbaseform, List<Control> c)
        {
            bool checkStatus = r_framework.Configuration.AppConfig.AppOptions.IsManiImport();
            LogUtility.DebugMethodStart(parentbaseform, c);
            RangeSettingDto rangeSettingDto = new RangeSettingDto();
            Panel panel1 = new Panel();
            Label lab = new Label();
            CustomNumericTextBox2 textBox = new CustomNumericTextBox2();
            CustomTextBox reiautoKbnName = new CustomTextBox();

            panel1.SuspendLayout();
            parentbaseform.SuspendLayout();

            lab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            lab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lab.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            lab.ForeColor = System.Drawing.Color.White;
            lab.Location = new System.Drawing.Point(0, 0);
            lab.Name = "ManifestCommonLabel2";
            lab.Size = new System.Drawing.Size(150, 20);
            lab.TabIndex = 8;
            lab.Text = "業者CD/現場CD";
            lab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            textBox.BackColor = System.Drawing.SystemColors.Window;
            textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox.DefaultBackColor = System.Drawing.Color.Empty;
            textBox.DisplayItemName = "業者CD/現場CD";
            textBox.DisplayPopUp = null;
            textBox.FocusOutCheckMethod = null;
            textBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            textBox.ForeColor = System.Drawing.Color.Black;
            textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            textBox.IsInputErrorOccured = false;
            textBox.Location = new System.Drawing.Point(0, 22);
            textBox.MaxLength = 1;
            textBox.Name = "txtNum_PatternContinue2";
            textBox.PopupAfterExecute = null;
            textBox.PopupBeforeExecute = null;
            textBox.PopupSearchSendParams = null;
            textBox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            textBox.popupWindowSetting = null;
            textBox.prevText = null;
            textBox.PrevText = null;
            rangeSettingDto.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            textBox.RangeSetting = rangeSettingDto;
            textBox.RegistCheckMethod = null;
            textBox.Size = new System.Drawing.Size(15, 20);
            textBox.TabIndex = 0;
            textBox.TabStop = false;
            textBox.Tag = "【1、2】のいずれかで入力してください";
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox.Validated += (sender, e) =>
            {
                CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
                if (string.IsNullOrEmpty(text.Text))
                {
                    reiautoKbnName.Text = "";
                    //パタン継続入力が空の場合、メッセージ「パタン継続入力は必須項目です。入力してください。」を表示する
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                    //フォーカスを抽出対象区分へ移動
                    text.Select();
                }
                else if (text.Text == "1")
                {
                    reiautoKbnName.Text = Constans.MANI_KBN_1;
                }
                else if (text.Text == "2")
                {
                    reiautoKbnName.Text = Constans.MANI_KBN_2;
                }
            };

            reiautoKbnName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            reiautoKbnName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            reiautoKbnName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            reiautoKbnName.DefaultBackColor = System.Drawing.Color.Empty;
            reiautoKbnName.DisplayPopUp = null;
            reiautoKbnName.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            reiautoKbnName.ForeColor = System.Drawing.Color.Black;
            reiautoKbnName.IsInputErrorOccured = false;
            reiautoKbnName.ItemDefinedTypes = "varchar";
            reiautoKbnName.Location = new System.Drawing.Point(14, 22);
            reiautoKbnName.MaxLength = 0;
            reiautoKbnName.Name = "reiautoKbnName";
            reiautoKbnName.PopupAfterExecute = null;
            reiautoKbnName.PopupBeforeExecute = null;
            reiautoKbnName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            reiautoKbnName.ReadOnly = true;
            reiautoKbnName.Size = new System.Drawing.Size(135, 20);
            reiautoKbnName.TabIndex = 0;
            reiautoKbnName.TabStop = false;
            reiautoKbnName.Tag = " ";

            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            panel1.Controls.Add(lab);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(reiautoKbnName);
            panel1.Location = new System.Drawing.Point(1024, 256);
            panel1.Name = "ManifestCommonPanel3";
            panel1.Size = new System.Drawing.Size(150, 44);
            panel1.TabIndex = 8;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            if (!checkStatus)
            {
                panel1.Visible = false;
            }

            parentbaseform.Controls.Add(panel1);
            parentbaseform.ResumeLayout(false);
            parentbaseform.PerformLayout();
            if (c == null)
            {
                c = new List<Control>();
            }
            c.Add(lab);
            c.Add(textBox);
            c.Add(reiautoKbnName);
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// コントロール(レイアウト区分)を作成
        /// </summary>
        /// <param name="parentbaseform">親フォーム</param>
        /// <param name="c">利用されるかもしれないコントロールリスト</param>
        /// <returns></returns>
        public static void MakeReiautoKbnControl(r_framework.APP.Base.BusinessBaseForm parentbaseform, List<Control> c)
        {
            LogUtility.DebugMethodStart(parentbaseform, c);
            RangeSettingDto rangeSettingDto = new RangeSettingDto();
            Panel panel1 = new Panel();
            Label lab = new Label();
            CustomNumericTextBox2 textBox = new CustomNumericTextBox2();
            CustomTextBox reiautoKbnName = new CustomTextBox();

            panel1.SuspendLayout();
            parentbaseform.SuspendLayout();

            lab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            lab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lab.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            lab.ForeColor = System.Drawing.Color.White;
            lab.Location = new System.Drawing.Point(0, 0);
            lab.Name = "ManifestCommonLabel2";
            lab.Size = new System.Drawing.Size(150, 20);
            lab.TabIndex = 8;
            lab.Text = "レイアウト区分";
            lab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            textBox.BackColor = System.Drawing.SystemColors.Window;
            textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox.DefaultBackColor = System.Drawing.Color.Empty;
            textBox.DisplayItemName = "レイアウト区分";
            textBox.DisplayPopUp = null;
            textBox.FocusOutCheckMethod = null;
            textBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            textBox.ForeColor = System.Drawing.Color.Black;
            textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            textBox.IsInputErrorOccured = false;
            textBox.Location = new System.Drawing.Point(0, 22);
            textBox.MaxLength = 1;
            textBox.Name = "txtNum_PatternContinue2";
            textBox.PopupAfterExecute = null;
            textBox.PopupBeforeExecute = null;
            textBox.PopupSearchSendParams = null;
            textBox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            textBox.popupWindowSetting = null;
            textBox.prevText = null;
            textBox.PrevText = null;
            rangeSettingDto.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            textBox.RangeSetting = rangeSettingDto;
            textBox.RegistCheckMethod = null;
            textBox.Size = new System.Drawing.Size(15, 20);
            textBox.TabIndex = 0;
            textBox.TabStop = false;
            textBox.Tag = "【1、2】のいずれかで入力してください";
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox.Validated += (sender, e) =>
            {
                CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
                if (string.IsNullOrEmpty(text.Text))
                {
                    reiautoKbnName.Text = "";
                    //パタン継続入力が空の場合、メッセージ「パタン継続入力は必須項目です。入力してください。」を表示する
                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                    //フォーカスを抽出対象区分へ移動
                    text.Select();
                }
                else if (text.Text == "1")
                {
                    reiautoKbnName.Text = Constans.REIAUTO_KBN_1;
                }
                else if (text.Text == "2")
                {
                    reiautoKbnName.Text = Constans.REIAUTO_KBN_2;
                }
            };

            reiautoKbnName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            reiautoKbnName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            reiautoKbnName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            reiautoKbnName.DefaultBackColor = System.Drawing.Color.Empty;
            reiautoKbnName.DisplayPopUp = null;
            reiautoKbnName.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            reiautoKbnName.ForeColor = System.Drawing.Color.Black;
            reiautoKbnName.IsInputErrorOccured = false;
            reiautoKbnName.ItemDefinedTypes = "varchar";
            reiautoKbnName.Location = new System.Drawing.Point(14, 22);
            reiautoKbnName.MaxLength = 0;
            reiautoKbnName.Name = "reiautoKbnName";
            reiautoKbnName.PopupAfterExecute = null;
            reiautoKbnName.PopupBeforeExecute = null;
            reiautoKbnName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            reiautoKbnName.ReadOnly = true;
            reiautoKbnName.Size = new System.Drawing.Size(135, 20);
            reiautoKbnName.TabIndex = 0;
            reiautoKbnName.TabStop = false;
            reiautoKbnName.Tag = " ";

            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            panel1.Controls.Add(lab);
            panel1.Controls.Add(textBox);
            panel1.Controls.Add(reiautoKbnName);
            panel1.Location = new System.Drawing.Point(1024, 206);
            panel1.Name = "ManifestCommonPanel3";
            panel1.Size = new System.Drawing.Size(150, 44);
            panel1.TabIndex = 8;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();

            parentbaseform.Controls.Add(panel1);
            parentbaseform.ResumeLayout(false);
            parentbaseform.PerformLayout();
            if (c == null)
            {
                c = new List<Control>();
            }
            c.Add(lab);
            c.Add(textBox);
            c.Add(reiautoKbnName);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 引数で渡された文字列を住所1～4に分割しセットする
        /// </summary>
        /// <param name="targetAddress">分割対象住所</param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="address3"></param>
        /// <param name="address4"></param>
        public bool SetAddress1ToAddress4(string targetAddress,
            out string address1, out string address2, out string address3, out string address4)
        {
            address1 = String.Empty;
            address2 = String.Empty;
            address3 = String.Empty;
            address4 = String.Empty;

            if (string.IsNullOrEmpty(targetAddress))
            {
                return false;
            }

            string s = targetAddress;
            System.Text.RegularExpressions.Match m;

            // 文字列（住所）を条件として郵便辞書マスタで検索
            int zipCodeCount = this.zipCodeDao.GetDataByJushoCountLikeSearch(s);
            if(zipCodeCount == 0)
            {
                return false;
            }

            List<S_ZIP_CODE> zipCodeList = this.zipCodeDao.GetDataByJushoSplitLikeSearch(s);
            
            // 検索結果が存在しない場合は、従来の住所分割処理（適切に分割されない可能性あり）
            if(!zipCodeList.Any())
            {
                m = System.Text.RegularExpressions.Regex.Match(s, "...??[都道府県]");
                address1 = string.IsNullOrEmpty(m.Value) ? null : m.Value;
                // 先頭の一回だけRplaceしたいのでRegexを生成する。
                var tempRegex = new Regex("...??[都道府県]");
                s = tempRegex.Replace(s, "", 1);

                m = System.Text.RegularExpressions.Regex.Match(s, ".+[市区郡]");
                address2 = string.IsNullOrEmpty(m.Value) ? null : m.Value;
                if ((address2 != null) && (address2.Length >= 15))
                {
                    m = System.Text.RegularExpressions.Regex.Match(address2.Substring(0, address2.Length - 1), ".+[市区郡]");
                    if (!string.IsNullOrEmpty(m.Value))
                    {
                        address2 = string.IsNullOrEmpty(m.Value) ? null : m.Value;
                    }
                }
                if (address2 != null)
                {
                    s = System.Text.RegularExpressions.Regex.Replace(s, address2, "");
                }
                m = System.Text.RegularExpressions.Regex.Match(s, ".+[町村丁目]");
                address3 = string.IsNullOrEmpty(m.Value) ? null : m.Value;
                address4 = string.IsNullOrEmpty(System.Text.RegularExpressions.Regex.Replace(s, ".+[町村丁目]", "")) ? null :
                    System.Text.RegularExpressions.Regex.Replace(s, ".+[町村丁目]", "");
            }
            else // 検索結果が存在する場合、郵便マスタに対応する値（都道府県、市区町村、町域）で分割処理を行う
            {
                // 検索結果を「町域」の文字数で降順にする
                S_ZIP_CODE entity = zipCodeList.OrderByDescending(x => x.OTHER1.Length)
                                               .First();

                address1 = entity.TODOUFUKEN;
                address2 = entity.SIKUCHOUSON;
                address3 = entity.OTHER1;
                address4 = System.Text.RegularExpressions.Regex.Replace(s, address1 + address2 + address3, ""); 
            }
            return true;
        }

        #region 終処分報告(取消)ポップアップの呼び出し用パラメータを作成

        /// <summary>
        /// 最終処分報告(取消)ポップアップの呼び出し用パラメータを作成
        /// エラーがある場合、メッセージはこのメソッド内で表示する。
        /// </summary>
        /// <param name="exeFlg">true:最終処分終了報告、false:最終処分終了報告の取消</param>
        /// <param name="maniSysId">最終処分終了報告対象の二次マニSYSTEM_ID</param>
        /// <param name="nextHaikiKbnCd">最終処分終了報告対象の二次マニ廃棄区分CD</param>
        /// <param name="lastSbnSusPendList"></param>
        /// <param name="queList"></param>
        /// <param name="manifastList"></param>
        /// <param name="jigyoubaList"></param>
        /// <param name="mokujiList"></param>
        /// <returns>true:成功、false:失敗</returns>
        public bool CreateSousinnHoryuuPopuUpParam(bool exeFlg, string maniSysId, int nextHaikiKbnCd, out List<T_LAST_SBN_SUSPEND> lastSbnSusPendList,
            out List<QUE_INFO> queList, out List<DT_D12> manifastList, out List<DT_D13> jigyoubaList, out List<DT_MF_TOC> mokujiList)
        {
            // 最終処分保留
            lastSbnSusPendList = new List<T_LAST_SBN_SUSPEND>();
            List<string> systemIdList = new List<string>();
            // キュー情報
            queList = new List<QUE_INFO>();
            List<string> kanriIdList = new List<string>();
            // D12 2次マニフェスト情報
            manifastList = new List<DT_D12>();
            // D13 最終処分終了日・事業場情報
            jigyoubaList = new List<DT_D13>();
            // マニフェスト目次情報
            mokujiList = new List<DT_MF_TOC>();

            try
            {
                // 検索条件を設定する
                GetManifestRelationDtoCls searchDto = new GetManifestRelationDtoCls();
                // マニフェストのシステムID
                searchDto.MANIFEST_SYSTEM_ID = maniSysId;
                searchDto.NEXT_HAIKI_KBN_CD = nextHaikiKbnCd;

                // 登録対象を検索する
                DataTable dt;
                if (exeFlg)
                {
                    // 最終処分終了報告
                    dt = this.GetManiRelDao.GetDataForEntity(searchDto);

                    // 電マニ混廃用ロジック
                    List<string> tempKanriIds = new List<string>();
                    foreach (DataRow tempDataRow in dt.Rows)
                    {
                        if (!tempKanriIds.Contains(tempDataRow["KANRI_ID"].ToString()))
                        {
                            tempKanriIds.Add(tempDataRow["KANRI_ID"].ToString());
                        }
                    }

                    if (tempKanriIds.Count > 0)
                    {
                        // 電マニ混廃振分で区分：最終にしたデータを取得
                        // 最終処分終了報告できるようにする
                        var tempDt = this.GetManiRelDao.GetMixManifestForLastSbnData(tempKanriIds);
                        if (tempDt != null && tempDt.Rows.Count > 0)
                        {
                            dt.Merge(tempDt);
                        }
                    }

                    // 混廃の場合は紐付いている全ての二次マニが処分終了していること
                    List<string> delKanriIds = new List<string>();
                    var filteringDt = dt.Select("LAST_SBN_END_DATE IS NULL OR GENBA_NAME1 IS NULL OR GENBA_ADDRESS1 = ''");
                    foreach (var tempRow in filteringDt)
                    {
                        if (!delKanriIds.Contains(tempRow["KANRI_ID"].ToString()))
                        {
                            delKanriIds.Add(tempRow["KANRI_ID"].ToString());
                        }
                    }

                    foreach (var kanriId in delKanriIds)
                    {
                        var tempDt = dt.Select("KANRI_ID = '" + kanriId + "'");
                        foreach (var row in tempDt)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                }
                else
                {
                    // 最終処分終了報告の取消
                    var manifestRelationCancelDao = DaoInitUtility.GetComponent<GetManifestRelationCancelDaoCls>();
                    dt = manifestRelationCancelDao.GetDataForEntity(searchDto);
                }

                /**
                 * 検索結果のエラーチェック
                 */
                List<string> unModifiedKanriIdList = new List<string>();
                ManifestoLogic maniLogic = new ManifestoLogic();

                // メッセージ表示
                if (!maniLogic.ChkLastSbnEndrepReport(dt, exeFlg, out unModifiedKanriIdList))
                {
                    if (DialogResult.Yes != Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("C070"))
                    {
                        return false;
                    }
                }

                // 最終処分終了日 ≦ 最終処分終了の報告日チェック
                if (exeFlg)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Convert.ToDateTime(row["LAST_SBN_END_DATE"]);
                        // 20150914 katen #12048 「システム日付」の基準作成、適用 start
                        //if (Convert.ToDateTime(eRow["LAST_SBN_END_DATE"]).Date.CompareTo(DateTime.Now.Date) > 0)
                        if (Convert.ToDateTime(row["LAST_SBN_END_DATE"]).Date.CompareTo(this.sysDate.Date) > 0)
                        // 20150914 katen #12048 「システム日付」の基準作成、適用 end
                        {
                            Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E218");
                            return false;
                        }
                    }
                }

                // 件数
                int index = 0;
                // 登録データ作成
                for (; index < dt.Rows.Count; index++)
                {
                    if (!unModifiedKanriIdList.Contains(dt.Rows[index]["KANRI_ID"].ToString()))
                    {
                        if (!systemIdList.Contains(dt.Rows[index]["SYSTEM_ID"].ToString())
                            && !kanriIdList.Contains(dt.Rows[index]["KANRI_ID"].ToString()))
                        {
                            // 最終処分保留(T_LAST_SBN_SUSPEND)データ作成
                            T_LAST_SBN_SUSPEND LastSbnSusPend = new T_LAST_SBN_SUSPEND();
                            // システムID
                            LastSbnSusPend.SYSTEM_ID = Convert.ToInt64(maniSysId);
                            systemIdList.Add(dt.Rows[index]["SYSTEM_ID"].ToString());
                            // 削除フラグ
                            LastSbnSusPend.DELETE_FLG = false;
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderLastSbnSusPend = new DataBinderLogic<T_LAST_SBN_SUSPEND>(LastSbnSusPend);
                            dataBinderLastSbnSusPend.SetSystemProperty(LastSbnSusPend, true);
                            lastSbnSusPendList.Add(LastSbnSusPend);
                        }

                        if (!kanriIdList.Contains(dt.Rows[index]["KANRI_ID"].ToString()))
                        {
                            // キュー情報(QUE_INFO)データ作成
                            QUE_INFO Que = new QUE_INFO();
                            // 管理番号
                            Que.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            kanriIdList.Add(dt.Rows[index]["KANRI_ID"].ToString());
                            // 枝番
                            Que.SEQ = Convert.ToInt16(dt.Rows[index]["LATEST_SEQ"].ToString());
                            if (exeFlg)
                            {
                                // 機能番号
                                Que.FUNCTION_ID = CommonConst.FUNCTION_ID_2000;
                            }
                            else
                            {
                                // 機能番号
                                Que.FUNCTION_ID = CommonConst.FUNCTION_ID_2100;
                            }
                            // キュー状態フラグ
                            Que.STATUS_FLAG = 0;
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderQue = new DataBinderLogic<QUE_INFO>(Que);
                            dataBinderQue.SetSystemProperty(Que, true);
                            queList.Add(Que);

                            // マニフェスト目次情報データ作成
                            DT_MF_TOC mokuji = new DT_MF_TOC();
                            // 管理番号
                            mokuji.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            // 状態詳細フラグ
                            if (exeFlg)
                            {
                                mokuji.STATUS_DETAIL = 0;
                            }
                            else
                            {
                                mokuji.STATUS_DETAIL = 1;
                            }
                            mokuji.UPDATE_TS = (DateTime)dt.Rows[index]["UPDATE_TS"];
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderMokuji = new DataBinderLogic<DT_MF_TOC>(mokuji);
                            dataBinderMokuji.SetSystemProperty(mokuji, false);
                            mokujiList.Add(mokuji);
                        }
                        // 最終処分終了報告の判断
                        if (exeFlg)
                        {
                            // D12 2次マニフェスト情報データ作成
                            DT_D12 manifast = new DT_D12();
                            // 管理番号
                            manifast.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            // 2次マニフェスト番号
                            manifast.SCND_MANIFEST_ID = dt.Rows[index]["MANIFEST_ID"].ToString();
                            // レコード作成日
                            // タイムスタンプ
                            var dataBinderManifast = new DataBinderLogic<DT_D12>(manifast);
                            dataBinderManifast.SetSystemProperty(manifast, true);
                            manifastList.Add(manifast);

                            // 最終処分終了日・事業場情報データ作成
                            DT_D13 jigyouba = new DT_D13();
                            // 管理番号
                            jigyouba.KANRI_ID = dt.Rows[index]["KANRI_ID"].ToString();
                            //2013/12/11 tyou add start

                            // 禁則文字チェック + 住所分割修正(CHIIKI_NAMEは都道府県+政令指定都市なので不適切。あとGENBA_ADDRESS1、2に都道府県が入っていた場合出さないようにする)
                            Validator v = new Validator();
                            string tempCheckName = (dt.Rows[index]["GENBA_NAME1"] == null) ? null : dt.Rows[index]["GENBA_NAME1"].ToString();
                            string tempCheckTodoufuken = (dt.Rows[index]["TODOUFUKEN_NAME"] == null) ? null : dt.Rows[index]["TODOUFUKEN_NAME"].ToString();
                            string tempCheckAddress1 = (dt.Rows[index]["GENBA_ADDRESS1"] == null) ? string.Empty : dt.Rows[index]["GENBA_ADDRESS1"].ToString();
                            string tempCheckAddress2 = (dt.Rows[index]["GENBA_ADDRESS2"] == null) ? string.Empty : dt.Rows[index]["GENBA_ADDRESS2"].ToString();
                            string tempCheckTel = (dt.Rows[index]["GENBA_TEL"] == null) ? null : dt.Rows[index]["GENBA_TEL"].ToString();

                            if (!v.isJWNetValidShiftJisCharForSign(tempCheckName))
                            {
                                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」", tempCheckName));
                                return false;
                            }

                            if (!v.isJWNetValidShiftJisCharForSign(tempCheckTodoufuken))
                            {
                                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の都道府県", tempCheckName));
                                return false;
                            }

                            // 住所1
                            if (!v.isJWNetValidShiftJisCharForSign(tempCheckAddress1))
                            {
                                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の住所(1行目)", tempCheckName));
                                return false;
                            }

                            // 住所2
                            if (!v.isJWNetValidShiftJisCharForSign(tempCheckAddress2))
                            {
                                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の住所(2行目)", tempCheckName));
                                return false;
                            }

                            if (!v.isTelNumberValid(tempCheckTel))
                            {
                                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E071", string.Format("実績タブに入力した最終処分場所名：「{0}」の登録情報の電話番号", tempCheckName));
                                return false;
                            }

                            /**
                             * データセット
                             */

                            // 最終処分事業場名称
                            jigyouba.LAST_SBN_JOU_NAME = tempCheckName;
                            // 最終処分事業場所在地の郵便番号
                            jigyouba.LAST_SBN_JOU_POST = (dt.Rows[index]["GENBA_POST"] == null) ? null : dt.Rows[index]["GENBA_POST"].ToString();
                            // 最終処分事業場所在地1
                            jigyouba.LAST_SBN_JOU_ADDRESS1 = tempCheckTodoufuken;

                            string tempAddress1;
                            string tempAddress2;
                            string tempAddress3;
                            string tempAddress4;
                            // 住所分割
                            this.SetAddress1ToAddress4(tempCheckTodoufuken + tempCheckAddress1 + tempCheckAddress2,
                                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                            // 所在地1に都道府県名が設定されている可能性があるので、都道府県が設定されていなかった場合には分割した住所をセットする
                            if (string.IsNullOrEmpty(jigyouba.LAST_SBN_JOU_ADDRESS1))
                            {
                                jigyouba.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                            }

                            // 最終処分事業場所在地2
                            jigyouba.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                            // 最終処分事業場所在地3
                            jigyouba.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                            // 最終処分事業場所在地4
                            jigyouba.LAST_SBN_JOU_ADDRESS4 = tempAddress4;

                            // 最終処分事業場電話番号
                            jigyouba.LAST_SBN_JOU_TEL = tempCheckTel;

                            // 最終処分終了日
                            if (!string.IsNullOrEmpty((dt.Rows[index]["LAST_SBN_END_DATE"] == null) ? string.Empty : dt.Rows[index]["LAST_SBN_END_DATE"].ToString()))
                                jigyouba.LAST_SBN_END_DATE = Convert.ToDateTime(dt.Rows[index]["LAST_SBN_END_DATE"]).ToString("d").Replace("/", "");
                            //2013/12/11 tyou add end
                            // レコード作成日時
                            // タイムスタンプ
                            var dataBinderjigyouba = new DataBinderLogic<DT_D13>(jigyouba);
                            dataBinderjigyouba.SetSystemProperty(jigyouba, true);
                            jigyoubaList.Add(jigyouba);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }

            return true;
        }

        #endregion

        #region 紐付けた一次マニが最終処分終了報告済みかチェック

        /// <summary>
        /// 紐付けた一次マニが最終処分終了報告済みかチェック
        /// </summary>
        /// <param name="nextSysId"></param>
        /// <param name="nextHaikiKbnCd"></param>
        /// <returns>true:最終処分終了報告済み、false:未報告</returns>
        public bool IsFixedRelationFirstMani(SqlInt64 nextSysId, int nextHaikiKbnCd)
        {
            bool returnVal = false;
            var resultData = this.GetManiRelDao.GetManiRelationForFixedEndRepElec(nextSysId, nextHaikiKbnCd);
            if (resultData != null && resultData.Rows.Count > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        #endregion

        #region 紐付けた一次マニが最終処分終了報告中かチェック

        /// <summary>
        /// 紐付けた一次マニが最終処分終了報告中かチェック
        /// </summary>
        /// <param name="nextSysId"></param>
        /// <param name="nextHaikiKbnCd"></param>
        /// <returns>true:最終処分終了報告中、false:未報告</returns>
        public bool IsExecutingLastSbnEndRep(SqlInt64 nextSysId, SqlInt16 nextHaikiKbnCd)
        {
            bool returnVal = false;

            // 一次マニのデータ取得
            var firstManiData = this.GetManiRelDao.GetFirstManiInfo(nextSysId, nextHaikiKbnCd);

            if (firstManiData == null || firstManiData.Rows.Count < 1)
            {
                return returnVal;
            }

            // QUE_INFOチェック用の引数を生成
            var dummyList = new List<string>();
            var dt = new DataTable();

            dt.Columns.Add("KANRI_ID");
            dt.Columns.Add("LATEST_SEQ");
            dt.Columns.Add("STATUS_FLAG");
            dt.Columns.Add("STATUS_DETAIL");
            dt.Columns.Add("APPROVAL_SEQ");

            List<QUE_INFO> allSearchCoditionList = new List<QUE_INFO>();

            foreach (DataRow firstMani in firstManiData.Rows)
            {
                if (firstMani["KANRI_ID"] == null
                    || string.IsNullOrEmpty(firstMani["KANRI_ID"].ToString())
                    || firstMani["LATEST_SEQ"] == null
                    || string.IsNullOrEmpty(firstMani["LATEST_SEQ"].ToString()))
                {
                    continue;
                }

                QUE_INFO execQue = new QUE_INFO();
                execQue.KANRI_ID = firstMani["KANRI_ID"].ToString();
                execQue.SEQ = Convert.ToDecimal(firstMani["LATEST_SEQ"].ToString());
                execQue.FUNCTION_ID = CommonConst.FUNCTION_ID_2000;

                QUE_INFO cancelQue = new QUE_INFO();
                cancelQue.KANRI_ID = firstMani["KANRI_ID"].ToString();
                cancelQue.SEQ = Convert.ToDecimal(firstMani["LATEST_SEQ"].ToString());
                cancelQue.FUNCTION_ID = CommonConst.FUNCTION_ID_2100;
                allSearchCoditionList.Add(execQue);
                allSearchCoditionList.Add(cancelQue);
            }

            // QUE_INFOチェック(this.ChkLastSbnEndrepReportメソッドを流用)
            // 既に最終処分終了報告または最終処分終了取消が実行されていないかチェック
            List<string> CheckedKanriIdList = new List<string>();
            foreach (var tempQueInfo in allSearchCoditionList)
            {
                if (CheckedKanriIdList.Contains(tempQueInfo.KANRI_ID))
                {
                    // 無駄にDBアクセスさせないためのチェック
                    continue;
                }

                var commonUqeInfoDao = DaoInitUtility.GetComponent<CommonQueInfoDaoCls>();
                var queInfo = commonUqeInfoDao.GetQue_SeqInfoForLastSbnEndRepFunction(tempQueInfo);

                // ヒットしたらNG
                if ((queInfo != null && queInfo.Rows.Count > 0))
                {
                    // 一件でもあれば、return
                    returnVal = true;
                    break;
                }

                CheckedKanriIdList.Add(tempQueInfo.KANRI_ID);
            }

            return returnVal;
        }

        #endregion

        /// <summary>
        /// 指定された文字列のバイト数を返します。
        /// </summary>
        /// <param name="str">指定文字列</param>
        /// <returns>バイト数</returns>
        public long GetByteLength(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            //Shift_JISに変更してバイト数をカウントする
            return Encoding.GetEncoding("Shift_JIS").GetByteCount(str);
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}