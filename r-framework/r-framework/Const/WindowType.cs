using System;
using System.Drawing;
using r_framework.OriginalException;

namespace r_framework.Const
{

    /// <summary>
    /// 画面区分
    /// </summary>
    public enum WINDOW_TYPE : int
    {
        NONE = 0,
        /// <summary>新規</summary>
        NEW_WINDOW_FLAG = 1,
        /// <summary>参照</summary>
        REFERENCE_WINDOW_FLAG = 2,
        /// <summary>削除</summary>
        DELETE_WINDOW_FLAG = 3,
        /// <summary>修正</summary>
        UPDATE_WINDOW_FLAG = 4,
        /// <summary>一覧</summary>
        ICHIRAN_WINDOW_FLAG = 5
    }
    /// <summary>
    /// 画面タイプ拡張
    /// </summary>
    public static class WINDOW_TYPEExt
    {
        /// <summary>
        /// 画面タイプから画面区分名を取得
        /// </summary>
        /// <param name="e">画面タイプ</param>
        /// <returns>画面タイプ名</returns>
        public static string ToTypeString(this WINDOW_TYPE e)
        {
            switch (e)
            {
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    return "削除";
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    return "新規";
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    return "参照";
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    return "修正";
            }
            return String.Empty;
        }

        /// <summary>
        /// ラベルの背景色を取得
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Color ToLabelColor(this WINDOW_TYPE e)
        {
            switch (e)
            {
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    return Color.Red;
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    return Color.Aqua;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    return Color.Orange;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    return Color.Yellow;
                default:
                    return Color.Cornsilk;
            }
        }

        /// <summary>
        /// ラベルの文字色を取得
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Color ToLabelForeColor(this WINDOW_TYPE e)
        {
            switch (e)
            {
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    return Color.White;
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    return Color.Black;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    return Color.Black;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    return Color.Black;
                default:
                    return Color.Black;
            }
        }


    }
}
