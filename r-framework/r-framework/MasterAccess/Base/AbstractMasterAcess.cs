using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.MasterAccess.Base
{
    /// <summary>
    /// PKが１つで構成されるマスタ用の汎用データチェック抽象クラス（未実装）
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="E1"></typeparam>
    internal abstract class AbstractMasterAcess<T1, E1> : IMasterDataAccess<E1>
        where T1 : IMasterAccessDao<E1>
        where E1 : SuperEntity, new()
    {
        public E1 Entity { get; set; }

        protected T1 dao1;

        protected PropertyInfo p1;

        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        public object[] Param { get; set; }
        public object[] SendParam { get; set; }

        /// <summary>
        /// KEYが２つあるテーブルの１つめのキー列名
        /// （＝KEYが１つあるテーブルの１つめのキー列名）
        /// </summary>
        /// <returns></returns>
        protected string PK1 { get; private set; }
        protected string PK1_NAME { get; private set; }
 
        /// <summary>
        /// PK1の名前の列名 BANK_NAME など。
        /// </summary>
        protected string PK1_NAME_COL { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AbstractMasterAcess(ICustomControl control, object[] obj, object[] sendParam,
             string pk1, string pk1_name, string pk1_name_col)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;

            this.dao1 = DaoInitUtility.GetComponent<T1>();
 
            this.PK1 = pk1;
            this.PK1_NAME = pk1_name;
            this.PK1_NAME_COL = pk1_name_col;

            PropertyUtility.TryGetInfo(typeof(E1), this.PK1, out p1);


        }


        public string CodeCheckAndSetting()
        {
            this.SettingFieldInit();

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            string errorMessage = string.Empty;
            errorMessage = this.CodeCheck();
            if (errorMessage.Length == 0)
            {
                this.CodeDataSetting();
            }

            return errorMessage;
        }
        /// <summary>
        /// 値設定項目の初期化処理
        /// </summary>
        private void SettingFieldInit()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            //キー1つの場合は自分だけ残せばいい
            controlUtil.InitCheckDateField(new string[] 
                { this.PK1 });
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
        /// コード存在チェック
        /// </summary>
        /// <returns></returns>
        protected string CodeCheck()
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            E1 entity = new E1();
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

            //PK1をセット
            this.p1.SetValue(entity,CheckControl.GetResultText(),null);

            var returnEntitys = dao1.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (returnEntitys.Length == 0)
            {
                //コードが存在しない場合エラー
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE; //{0}マスタに存在しないコードが入力されました。
                errorMessage = String.Format(errorMessage, this.PK1_NAME.Replace("CD",""));
            }
            else
            {
                Entity = returnEntitys[0];
            }
            return errorMessage;
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

            E1 entity = new E1();
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

            this.p1.SetValue(entity, CheckControl.GetResultText(), null);

            var returnEntitys = dao1.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (presenceFlag)
            {
                if (returnEntitys.Length == 0)
                {
                    //コードが存在しない場合エラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E020").MESSAGE; //{0}マスタに存在しないコードが入力されました。
                    errorMessage = String.Format(errorMessage, this.PK1_NAME.Replace("CD", ""));

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
                    errorMessage = messageUtil.GetMessage("E005").MESSAGE; //該当の【{0}CD】は既に使用されています。<br>新たに【{0}CD】を設定し直してください。
                    errorMessage = String.Format(errorMessage, this.PK1_NAME.Replace("CD",""));
                }
            }
            return errorMessage;

        }
        /// <summary>
        /// 紐付くデータを設定する
        /// </summary>
        public virtual void CodeDataSetting()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.setCheckDate(this.Entity);
        }

    }

    /// <summary>
    /// PKが２つで構成されるマスタ用の汎用データチェック抽象クラス
    /// </summary>
    /// <typeparam name="T1">KEYが１つあるテーブルのDAO</typeparam>
    /// <typeparam name="E1">KEYが１つあるテーブルのENTITY</typeparam>
    /// <typeparam name="T2">KEYが２つあるテーブルのDAO</typeparam>
    /// <typeparam name="E2">KEYが２つあるテーブルのENTITY</typeparam>
    internal abstract class AbstractMasterAcess<T1, E1, T2, E2> : IMasterDataAccess<E2>
        where T1 : IMasterAccessDao<E1>
        where E1 : SuperEntity, new()
        where T2 : IMasterAccessDao<E2>
        where E2 : SuperEntity, new()
    {
        public E2 Entity { get; set; }

        protected T1 dao1;
        protected T2 dao2;

        protected PropertyInfo p1;
        protected PropertyInfo p2_1;
        protected PropertyInfo p2_2;

        /// <summary>
        /// チェックメソッドで複数件数取得されたかどうか
        /// true：複数件取得 false:1件以下
        /// </summary>
        protected bool isGotMultipleColumns { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        public object[] Param { get; set; }
        public object[] SendParam { get; set; }

        /// <summary>
        /// KEYが２つあるテーブルの１つめのキー列名
        /// （＝KEYが１つあるテーブルの１つめのキー列名）
        /// </summary>
        /// <returns></returns>
        protected string PK1 { get; private set; }
        /// <summary>
        /// KEYが２つあるテーブルの2つめのキー列名
        /// </summary>
        /// <returns></returns>
        protected string PK2 { get; private set; }

        protected string PK1_NAME { get; private set; }
        protected string PK2_NAME { get; private set; }

        /// <summary>
        /// PK1の名前の列名 BANK_NAME など。
        /// </summary>
        protected string PK1_NAME_COL { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AbstractMasterAcess(ICustomControl control, object[] obj, object[] sendParam,
             string pk1, string pk1_name, string pk2, string pk2_name, string pk1_name_col)
        {
            this.CheckControl = control;
            this.Param = obj;
            this.SendParam = sendParam;

            this.dao1 = DaoInitUtility.GetComponent<T1>();
            this.dao2 = DaoInitUtility.GetComponent<T2>();

            this.PK1 = pk1;
            this.PK1_NAME = pk1_name;
            this.PK2 = pk2;
            this.PK2_NAME = pk2_name;
            this.PK1_NAME_COL = pk1_name_col;

            PropertyUtility.TryGetInfo(typeof(E1), this.PK1, out p1);
            
            PropertyUtility.TryGetInfo(typeof(E2), this.PK1, out p2_1);
            PropertyUtility.TryGetInfo(typeof(E2), this.PK2, out p2_2);


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
        /// 対象コードのチェックを行った上で
        /// データが存在する場合は指定のControlへセットを行う
        /// </summary>
        virtual public string CodeCheckAndSetting()
        {
            this.SettingFieldInit();

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                this.SettingFieldInit();
                return string.Empty;
            }

            E2 data = new E2();
            //data.SetValue(this.CheckControl);//PK1つめセット
            //PK1をセット
            this.p2_2.SetValue(data, CheckControl.GetResultText(), null);


            //強制パラメータセット（キーの上書き等や追加条件がある場合）
            if (this.SendParam != null)
            {
                for (int i = 0; i < this.SendParam.Length; i++)
                {
                    data.SetValue(this.SendParam[i] as ICustomControl);
                }
            }

                //ここは未入力の場合くらいしか通らない（必須チェックがない場合）
            if (this.HasEmptyKey(data))
            {
                return string.Empty;
            }

            // 初期化
            isGotMultipleColumns = false;
            string errorMessage = string.Empty;

            errorMessage = this.CodeCheck();
            // 複数件取れたときは
            if (errorMessage.Length == 0 && !this.isGotMultipleColumns)
            {
                this.CodeDataSetting();
            }
            return errorMessage;
        }

        private void SettingFieldInit()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            //課題 #1547 複数キーの場合 必ずポップアップが出てしまう対応
            //キー＋ PK1側の名前列も残す必要あり（検索結果無しの場合、名前が消えてしまう）
            controlUtil.InitCheckDateField(new string[] 
                { this.PK1, this.PK2, 
                  PK1_NAME_COL, PK1_NAME_COL  + "_RYAKU" ,PK1_NAME_COL + "1",PK1_NAME_COL + "2"});
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        public string CodeCheck()
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            E2 entity = new E2();

            //PK1つめ（プライマリ2番目）セット →自分自身はPK2になる
            //entity.SetValue(CheckControl);
            p2_2.SetValue(entity, CheckControl.GetResultText(), null);

            //PK2つ目(プライマリ1番目)セット
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            foreach (ICustomControl c in setField)
            {
                if (this.PK1.Equals(c.DBFieldsName) && !string.IsNullOrEmpty(c.GetResultText())) //空の時は設定しない（空文字だと検索される）
                {
                    entity.SetValue(c);
                    break; //見つけたら後続ループは不要
                }
            }

            //強制パラメータセット（キーの上書き等や追加条件がある場合）
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    // 値なしの場合は検索条件に含めない
                    if (param != null && !string.IsNullOrEmpty(param.GetResultText()))
                    {
                        entity.SetValue(param);
                    }
                }
            }


            var returnEntitys = this.dao2.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (returnEntitys.Length == 0)
            {
                //コードが存在しない場合エラー
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E020").MESSAGE;//{0}マスタに存在しないコードが入力されました。
                errorMessage = String.Format(errorMessage, this.PK2_NAME.Replace("CD", ""));
            }
            else
            {
                if (returnEntitys.Length == 1)
                {
                    Entity = returnEntitys[0];
                }
                else
                {
                    //TODO:選択されなかった場合エラーにする必要あり
                    if ( ! ShowPopupWindow(entity))
                    {

                        //var messageUtil = new MessageUtility();
                        //errorMessage = messageUtil.GetMessage("E020").MESSAGE;
                        errorMessage = String.Format("【{0}、{1}】の組み合わせを確認してください。", this.PK1_NAME, this.PK2_NAME);
                    }
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// 複数検索結果時のポップアップウィンドウ表示処理
        /// </summary>
        /// <param name="entity"></param>
        private bool ShowPopupWindow(SuperEntity entity)
        {
            CustomControlLogic cstmLogic = new CustomControlLogic(this.CheckControl);
            ControlUtility ctrlUtil = new ControlUtility();
            object[] sendParamArray = null;
            object[] fields = null;

            // DataGrid
            if (this.CheckControl is ICustomDataGridControl)
            {
                var dataGridControl = this.CheckControl as ICustomDataGridControl;
                DataGridViewCell dataGridCell = this.CheckControl as DataGridViewCell;

                if (dataGridControl.PopupSendParams != null)
                {
                    sendParamArray = new object[dataGridControl.PopupSendParams.Length];
                    for (int i = 0; i < dataGridControl.PopupSendParams.Length; i++)
                    {
                        var sendParam = dataGridControl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(dataGridCell.DataGridView.FindForm(), sendParam);
                    }
                }
                var customDataGridView = dataGridCell.DataGridView as CustomDataGridView;
                fields = new object[customDataGridView.CurrentRow.Cells.Count];
                customDataGridView.CurrentRow.Cells.CopyTo(fields, 0);
            }
            // MultiRow
            else if (this.CheckControl is Cell)
            {
                var cell = this.CheckControl as Cell;
                Row row = cell.GcMultiRow.Rows[cell.RowIndex];

                if (this.CheckControl.PopupSendParams != null)
                {
                    sendParamArray = new Control[this.CheckControl.PopupSendParams.Length];
                    for (int i = 0; i < this.CheckControl.PopupSendParams.Length; i++)
                    {
                        var sendParam = this.CheckControl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(cell.GcMultiRow.FindForm(), sendParam);
                    }
                }
                fields = row.Cells.ToArray();
            }
            else
            {
                var control = this.CheckControl as Control;

                ctrlUtil.ControlCollection = control.Parent.Controls;

                if (this.CheckControl.PopupSendParams != null)
                {
                    sendParamArray = new object[this.CheckControl.PopupSendParams.Length];
                    for (int i = 0; i < this.CheckControl.PopupSendParams.Length; i++)
                    {
                        var sendParam = this.CheckControl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(control.FindForm(), sendParam);
                    }
                }
                fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(control));
            }

            var result = cstmLogic.ShowPopupWindow(this.CheckControl, fields, this.CheckControl, sendParamArray, true, entity);

            this.isGotMultipleColumns = true;
            return true;

            //TODO:現在Cancelしか戻ってこないのでダイアログ側対応が必要
            if (result == DialogResult.OK)
            {
                this.isGotMultipleColumns = false;
                return true;
            }
            else
            {
                this.isGotMultipleColumns = true;
                return false;
            }
        }

        /// <summary>
        /// DBに対象のコードが存在するかチェック
        /// </summary>
        /// <param name="presenceFlag"></param>
        protected virtual string RegistCodeCheck(bool presenceFlag)
        {
            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            E2 entity = new E2();
            if (SendParam != null)
            {
                for (int i = 0; i < SendParam.Length; i++)
                {
                    var param = SendParam[i] as ICustomControl;

                    // 値なしの場合は検索条件に含めない
                    if (param != null && !string.IsNullOrEmpty(param.GetResultText()))
                    {
                        entity.SetValue(param);
                    }
                }
            }


            PropertyInfo p2;
            PropertyUtility.TryGetInfo(entity, this.PK2, out p2);

            //PK2が取れなかったら異常終了
            p2.SetValue(entity, CheckControl.GetResultText(), null);


            var returnEntitys = dao2.GetAllValidData(entity);

            string errorMessage = string.Empty;
            if (presenceFlag)//存在すべき
            {
                if (returnEntitys.Length == 0)
                {
                    //コードが存在しない場合エラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E020").MESSAGE;//{0}マスタに存在しないコードが入力されました。
                    errorMessage = String.Format(errorMessage, this.PK2_NAME.Replace("CD", ""));
                }
            }
            else
            {
                if (returnEntitys.Length != 0)
                {
                    //コードが取得できた場合はエラー
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E037").MESSAGE;  //該当の【{0}】の組み合わせは既に使用されています。<br>新たに【{0}】を設定し直してください。
                    errorMessage = String.Format(errorMessage, this.PK1_NAME + "、" + this.PK2_NAME);
                }
            }
            return errorMessage;
        }


        /// <summary>
        /// すべてのデータを取得
        /// </summary>
        public E2[] GetMasterData()
        {
            return this.dao2.GetAllData();
        }

        /// <summary>
        /// 紐付くデータを設定する
        /// </summary>
        protected virtual void CodeDataSetting()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            controlUtil.setCheckDate(this.Entity);

            //チェック開始時にクリアはするが、業者名が再セットされなかった
            E1 e = new E1();

            //PK1をセット
            p1.SetValue(e, p2_1.GetValue(this.Entity, null), null);

            E1[] e1 = this.dao1.GetAllValidData(e);
            if (e1 == null || e1.Length == 0)
            {

                LogUtility.Error(
                    string.Format("親のマスタだけが存在しません：{0}={1},{2}={3}",
                        PK1_NAME,p2_1.GetValue(this.Entity,null),
                        PK2_NAME,p2_2.GetValue(this.Entity,null))
                    );
            }
            else
            {
                controlUtil.setCheckDate(e1[0]);
            }

        }
        /// <summary>
        /// 紐付くデータを設定する
        /// </summary>
        protected virtual void CodeDataSettingPK1()
        {
            var setField = ControlUtility.CreateFields(Param, CheckControl.SetFormField);
            var controlUtil = new ControlUtility(CheckControl, setField);

            //チェック開始時にクリアはするが、名前が再セットされなかったので、ここで登録。
            E1 e = new E1();

            //PK1をセット
            p1.SetValue(e, p2_1.GetValue(this.Entity, null), null);

            E1[] e1 = this.dao1.GetAllValidData(e);
            if (e1 == null || e1.Length == 0)
            {

                LogUtility.Error(
                    string.Format("親のマスタだけが存在しません：{0}={1},{2}={3}",
                        PK1_NAME, p2_1.GetValue(this.Entity, null),
                        PK2_NAME, p2_2.GetValue(this.Entity, null))
                    );
            }
            else
            {
                controlUtil.setCheckDate(e1[0]);
            }

        }

        /// <summary>
        /// PKに空データが設定されているか判定
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool HasEmptyKey(E2 data)
        {
            object v1 = null;
            object v2 = null;
            string s1 = null;
            string s2 = null;


            if (data == null) return true;

            v1 = p2_1.GetValue(data, null);
            v2 = p2_2.GetValue(data, null);

            if (v1 != null) s1 = v1.ToString();
            if (v2 != null) s2 = v2.ToString();

            if ((string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
