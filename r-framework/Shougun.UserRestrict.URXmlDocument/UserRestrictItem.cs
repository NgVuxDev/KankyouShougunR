/*********************************************************************************
 *  概要 : 構成情報XMLの値を保持するクラス
 **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.UserRestrict.URXmlDocument
{
    /// <summary>
    /// UserRestrectItemクラス
    /// </summary>
    public class UserRestrictItem
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        private UserRestrictItem()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal UserRestrictItem(string id, string caption, 
                                  string description, string group, 
                                  Type type)
        {
            this.id = id;
            this.caption = caption;
            this.description = description;
            this.group = group;
            this.type = type;
        }

        /// <summary>
        /// ID
        /// </summary>
        public string id { get; private set; }
        
        /// <summary>
        /// Caption
        /// </summary>
        public string caption { get; private set; }
        
        /// <summary>
        /// Description
        /// </summary>
        public string description { get; private set; }
        
        /// <summary>
        /// Group
        /// </summary>
        public string group { get; private set; }
        
        /// <summary>
        /// Type
        /// </summary>
        public Type type { get; private set; }
    }
}
