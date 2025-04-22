
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Setting;
using r_framework.Utility;
namespace r_framework.Logic
{
    /// <summary>
    /// プロパティにて指定されているチェックロジックを
    /// 呼び出し、チェック処理を実施するクラス
    /// </summary>
    internal class AutoCheckLogic
    {
        /// <summary>
        /// チェック対象コントロール
        /// </summary>
        internal ICustomControl CheckControl { get; set; }

        /// <summary>
        /// 起動確認チェック用情報
        /// </summary>
        private Collection<SelectRunCheckDto> RunCheckDtoList { get; set; }

        /// <summary>
        /// 画面区分
        /// </summary>
        internal WINDOW_TYPE WindowType { get; set; }

        /// <summary>
        /// 処理区分
        /// </summary>
        internal PROCESS_KBN ProcessKbn { get; set; }

        /// <summary>
        /// チェックに使用するコントロール配列
        /// </summary>
        internal object[] ParamControl { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="runCheckDtoList"></param>
        internal AutoCheckLogic(Collection<SelectRunCheckDto> runCheckDtoList)
        {
            this.RunCheckDtoList = runCheckDtoList;
        }

        /// <summary>
        /// チェックを実施してもいいかを判断するための
        /// チェック処理を起動するメソッド
        /// </summary>
        /// <returns>チェック結果</returns>
        internal bool CheckWhetherStartup()
        {

            foreach (var RunCheckDto in RunCheckDtoList)
            {
                var permitFlag = this.CheckWindowType(RunCheckDto);

                if (WINDOW_TYPE.ICHIRAN_WINDOW_FLAG == WindowType)
                {
                    permitFlag = this.CheckProcessKbn(RunCheckDto);
                }

                if (!permitFlag)
                {
                    var check = new RunCheckMethodSetting();

                    ControlUtility logic = new ControlUtility();

                    var checkControl = logic.FindControl(ParamControl, RunCheckDto.SendParams);

                    var methodSetting = check[RunCheckDto.CheckMethodName];

                    var assemblyName = methodSetting.AssemblyName;
                    var calassNameSpace = methodSetting.ClassNameSpace;

                    var t = Type.GetType(assemblyName + "." + calassNameSpace);
                    object classInstance = System.Activator.CreateInstance(t, new object[] { this.CheckControl, RunCheckDto.Condition, checkControl });

                    bool result = (bool)t.InvokeMember(methodSetting.MethodName, BindingFlags.InvokeMethod,
                        null, classInstance, new object[] { });

                    if (result)
                    {
                        return result;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 現在の画面区分から対象の処理を
        /// 実施してもいいかを取得する
        /// </summary>
        /// <param name="dto">チェックメソッド情報</param>
        /// <returns>可否フラグ</returns>
        internal bool CheckWindowType(SelectRunCheckDto dto)
        {
            switch (WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    return dto.NewWinUnChecked;

                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    return dto.UpdWinUnChecked;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    return dto.DelWinUnChecked;

                case WINDOW_TYPE.ICHIRAN_WINDOW_FLAG:
                    return dto.IchiranWinUnChecked;
            }
            return false;
        }

        /// <summary>
        /// 現在の処理区分から対象の処理を
        /// 実施してもいいかを取得する
        /// </summary>
        /// <param name="dto">チェックメソッド情報</param>
        /// <returns>起動可否</returns>
        internal bool CheckProcessKbn(SelectRunCheckDto dto)
        {
            switch (ProcessKbn)
            {
                case PROCESS_KBN.NONE:
                    return false;

                case PROCESS_KBN.NEW:
                    return dto.NewProcessUnChecked;

                case PROCESS_KBN.UPDATE:
                    return dto.UpdProcessUnChecked;

                case PROCESS_KBN.DELETE:
                    return dto.DelProcessUnChecked;
            }
            return false;
        }
    }
}
