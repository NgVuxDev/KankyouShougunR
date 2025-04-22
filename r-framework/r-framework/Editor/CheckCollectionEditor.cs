using System;
using System.ComponentModel.Design;
using r_framework.Dto;

namespace r_framework.Editor
{
    /// <summary>
    /// エディタ定義
    /// </summary>
    internal class CheckCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CheckCollectionEditor(Type type)
            : base(type)
        {
        }
        /// <summary>
        /// 複数のコレクションの要素を一度に選択できるかどうかを示す値を取得します。
        /// </summary>
        protected override bool CanSelectMultipleInstances()
        {
            return true;
        }
        /// <summary>
        /// コレクションに格納されているデータ型を取得します。
        /// </summary>
        protected override Type CreateCollectionItemType()
        {
            return typeof(SelectCheckDto);
        }
        /// <summary>
        /// 特定のリスト項目の表示テキストを取得します。
        /// </summary>
        protected override string GetDisplayText(object value)
        {
            if (value.GetType().Name == typeof(string).Name)
            {
                return (string)value;
            }
            return value.GetType().Name;
        }
    }
}

