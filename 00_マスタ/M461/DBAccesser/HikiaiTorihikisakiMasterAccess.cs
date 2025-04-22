// $Id: HikiaiTorihikisakiMasterAccess.cs 27376 2014-08-07 11:13:45Z y-hosokawa@takumi-sys.co.jp $
using System;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.MasterAccess;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.DBAccesser
{
    /// <summary>
    /// 引合取引先マスタをチェックするクラス
    /// </summary>
    public class HikiaiTorihikisakiMasterAccess : IMasterDataAccess
    {
        /// <summary>
        /// 引合取引先マスタのDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKIDao Dao;

        /// <summary>
        /// Entity
        /// </summary>
        public SuperEntity Entity { get; set; }
        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }
        /// <summary>
        /// メッセージユーティリティ
        /// </summary>
        private MessageUtility Message { get; set; }

        public object[] Param { get; set; }

        public object[] SendParam { get; set; }

        /// <summary>
        /// CDのMax桁数
        /// </summary>
        public readonly int CdMaxLength = 6;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiTorihikisakiMasterAccess(ICustomControl control, object[] obj, object[] sendParam)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
            Dao = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
        }

        // 20140710 ria No.947 営業管理機能改修 start
        /// <summary>
        /// 引合取引先マスタのDao
        /// </summary>
        private IM_TORIHIKISAKIDao DaoM;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiTorihikisakiMasterAccess(ICustomControl control, object[] obj, object[] sendParam,bool flg)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;
            DaoM = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
        }

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out int maxPlusKeyValue, bool flg)
        {
            var maxPlusKey = this.DaoM.GetMaxPlusKey();

            var allKeyDate = this.DaoM.GetDateByChokuchiKbn1();
            foreach (M_TORIHIKISAKI torihikiEntity in allKeyDate)
            {
                var torihikisakiCd = int.Parse(torihikiEntity.TORIHIKISAKI_CD);
                if (torihikisakiCd == maxPlusKey)
                {
                    maxPlusKey = torihikisakiCd + 1;
                }
            }


            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.DaoM.GetMinBlankNo(null);
                if (this.CdMaxLength < maxPlusKey.ToString().Length)
                {
                    return true;
                }
            }

            maxPlusKeyValue = maxPlusKey;
            return false;
        }
        // 20140710 ria No.947 営業管理機能改修 end

        /// <summary>
        /// 対象コードのチェックを行った上で
        /// データが存在する場合は指定のControlへセットを行う
        /// </summary>
        public string CodeCheckAndSetting()
        {
            this.SettingFieldInit();

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            string errorMessage = string.Empty;

            var checkResultFlag = this.CodeCheck(this.CheckControl.GetResultText());
            if (checkResultFlag)
            {
                this.CodeDataSetting();
            }
            else
            {
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                errorMessage = String.Format(errorMessage, "引合取引先");
            }

            return errorMessage;
        }

        /// <summary>
        /// 値設定項目の初期化処理
        /// </summary>
        public void SettingFieldInit()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.InitCheckDateField();
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public string CodePresenceCheck()
        {
            string errorMessage = this.RegistCodeCheck(true);
            return errorMessage;
        }

        /// <summary>
        /// 対象のコードが削除されているかチェック
        /// </summary>
        public string CodeDeletedCheck()
        {
            string errorMessage = this.RegistCodeCheck(false);
            return errorMessage;
        }

        /// <summary>
        /// 取引先コード重複チェック
        /// 入力された取引先コードがすでにＤＢに登録されてるかをチェックする、登録されている場合はエラーとなる
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string CodeEnteredCheck()
        {
            var returnStr = "";

            if (this.CodeCheck(this.CheckControl.GetResultText()))
            {
                var messageUtil = new MessageUtility();
                returnStr = messageUtil.GetMessage("E022").MESSAGE;
                returnStr = String.Format(returnStr, this.CheckControl.GetResultText());
            }
            return returnStr;
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public bool CodeCheck(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            Entity = Dao.GetDataByCd(code);
            return Entity != null;
        }

        /// <summary>
        /// コード存在チェック
        /// </summary>
        /// <returns>エラーメッセージ。空の場合はエラーではないCD</returns>
        private string RegistCodeCheck(bool presenceFlag)
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            M_HIKIAI_TORIHIKISAKI entity = new M_HIKIAI_TORIHIKISAKI();
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    if (param != null)
                    {
                        entity.SetValue(param);
                    }
                }
            }

            entity.TORIHIKISAKI_CD = CheckControl.GetResultText();
            var returnEntitys = Dao.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (presenceFlag)
            {
                if (returnEntitys.Length == 0)
                {
                    //コードが存在しない場合エラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E006").MESSAGE;
                }
                else
                {
                    Entity = returnEntitys[0];
                }
            }
            else
            {
                if (returnEntitys.Length != 0)
                {
                    //コードが取得できた場合はエラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E005").MESSAGE;
                    errorMessage = String.Format(errorMessage, "引合取引先");
                }
            }
            return errorMessage;

        }

        /// <summary>
        /// すべてのデータを取得
        /// </summary>
        public SuperEntity[] GetMasterData()
        {
            return Dao.GetAllData();
        }

        /// <summary>
        /// 紐付くデータを設定する
        /// </summary>
        public virtual void CodeDataSetting()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.setCheckDate((M_HIKIAI_TORIHIKISAKI)Entity);
        }

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(out int maxPlusKeyValue)
        {
            var maxPlusKey = this.Dao.GetMaxPlusKey();

            var allKeyDate = this.Dao.GetDateByChokuchiKbn1();
            foreach (M_HIKIAI_TORIHIKISAKI torihikiEntity in allKeyDate)
            {
                var torihikisakiCd = int.Parse(torihikiEntity.TORIHIKISAKI_CD);
                if (torihikisakiCd == maxPlusKey)
                {
                    maxPlusKey = torihikisakiCd + 1;
                }
            }


            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.Dao.GetMinBlankNo(null);
                if (this.CdMaxLength < maxPlusKey.ToString().Length)
                {
                    return true;
                }
            }

            maxPlusKeyValue = maxPlusKey;
            return false;
        }
    }
}
