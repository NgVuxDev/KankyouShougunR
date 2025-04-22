using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon
{
    public class ItakuErrorDTO
    {
        /// <summary>
        /// エラー区分: 委託契約エラー区分を参照
        /// </summary>
        public short ERROR_KBN { get; set; }

        /// <summary>
        /// 廃棄種類又は品名のエラーリスト
        /// </summary>
        public List<DetailDTO> DETAIL_ERROR { get; set; }
    }

    /// <summary>
    /// 委託契約エラー区分
    /// </summary>
    public enum ITAKU_ERROR_KBN : short
    {
        /// <summary>エラーなし</summary>
        NONE = 0,
        /// <summary>「業者 (委託契約書が未登録)」エラー</summary>
        GYOUSHA,
        /// <summary>「委託契約の排出事業場≠BLANK, 画面の現場＝BLANK」エラー</summary>
        GENBA_BLANK,
        /// <summary>「委託契約に未登録の排出事業場」エラー</summary>
        GENBA_NOT_FOUND,
        /// <summary>「有効期間」エラー</summary>
        YUUKOU_KIKAN,
        /// <summary>「報告書分類」エラー</summary>
        HOUKOKUSHO_BUNRUI,
    }
}
