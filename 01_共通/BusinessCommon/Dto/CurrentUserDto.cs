using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// CurrentUserDtoクラスにXMLファイルとデータベースから取得したデータを保存する。
    /// </summary>
    public class CurrentUserDto
    {
        //拠点
        public SqlInt16 KYOTEN_CD { get; set; }
        public string KYOTEN_NAME { get; set; }
        public string KYOTEN_NAME_RYAKU { get; set; }
        //部門
        public string BUMON_CD { get; set; }
        public string BUMON_NAME { get; set; }
        public string BUMON_NAME_RYAKU { get; set; }
        //荷降業者
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_NAME1 { get; set; }
        public string NIOROSHI_GYOUSHA_NAME2 { get; set; }
        public string NIOROSHI_GYOUSHA_NAME_RYAKU { get; set; }
        //荷降現場
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_GENBA_NAME1 { get; set; }
        public string NIOROSHI_GENBA_NAME2 { get; set; }
        public string NIOROSHI_GENBA_NAME_RYAKU { get; set; }
        //荷積業者
        public string NITSUMI_GYOUSHA_CD { get; set; }
        public string NITSUMI_GYOUSHA_NAME1 { get; set; }
        public string NITSUMI_GYOUSHA_NAME2 { get; set; }
        public string NITSUMI_GYOUSHA_NAME_RYAKU { get; set; }
        //荷積現場
        public string NITSUMI_GENBA_CD { get; set; }
        public string NITSUMI_GENBA_NAME1 { get; set; }
        public string NITSUMI_GENBA_NAME2 { get; set; }
        public string NITSUMI_GENBA_NAME_RYAKU { get; set; }
        //領収書
        public string RYOUSHUUSHO { get; set; }
        //継続入力
        public string KEIZOKU_NYUURYOKU { get; set; }
        //終了日警告
        public string ENDDATE_USE_KBN_KOBETU { get; set; }
        // 契約アラート
        public string ITAKU_KEIYAKU_ALERT { get; set; }
    }
}
