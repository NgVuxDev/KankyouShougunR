using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity
{
    /// <summary>
    /// �N�x���f�[�^�N���X
    /// </summary>
    public class NendoInfo : SuperEntity
    {
        // SQL���擾���鍀��
        public string BUSHO_CD { get; set; }    //�����R�[�h
        public string BUSHO_NAME { get; set; }  //������
        public string SHAIN_CD { get; set; }    //�Ј��R�[�h
        public string SHAIN_NAME { get; set; }  //�Ј���
        public decimal YOSAN_1 { get; set; }    //�N1�̗\�Z
        public decimal YOSAN_2 { get; set; }    //�N2�̗\�Z
        public decimal YOSAN_3 { get; set; }    //�N3�̗\�Z
        public decimal YOSAN_4 { get; set; }    //�N4�̗\�Z
        public decimal YOSAN_5 { get; set; }    //�N5�̗\�Z
        public decimal YOSAN_6 { get; set; }    //�N6�̗\�Z
        public decimal YOSAN_7 { get; set; }    //�N7�̗\�Z
        public decimal YOSAN_8 { get; set; }    //�N8�̗\�Z
        public decimal YOSAN_9 { get; set; }    //�N9�̗\�Z
        public decimal YOSAN_GOUKEI { get; set; }     //�\�Z���v
        public decimal JISSEKI_1 { get; set; }  //�N1�̎���
        public decimal JISSEKI_2 { get; set; }  //�N2�̎���
        public decimal JISSEKI_3 { get; set; }  //�N3�̎���
        public decimal JISSEKI_4 { get; set; }  //�N4�̎���
        public decimal JISSEKI_5 { get; set; }  //�N5�̎���
        public decimal JISSEKI_6 { get; set; }  //�N6�̎���
        public decimal JISSEKI_7 { get; set; }  //�N7�̎���
        public decimal JISSEKI_8 { get; set; }  //�N8�̎���
        public decimal JISSEKI_9 { get; set; }  //�N9�̎���
        public decimal JISSEKI_GOUKEI { get; set; }   //���э��v

        // �ҏW���ʍ���
        public decimal TASSEI_RITSU_1 { get; set; }  //�N1�̒B����
        public decimal TASSEI_RITSU_2 { get; set; }  //�N2�̒B����
        public decimal TASSEI_RITSU_3 { get; set; }  //�N3�̒B����
        public decimal TASSEI_RITSU_4 { get; set; }  //�N4�̒B����
        public decimal TASSEI_RITSU_5 { get; set; }  //�N5�̒B����
        public decimal TASSEI_RITSU_6 { get; set; }  //�N6�̒B����
        public decimal TASSEI_RITSU_7 { get; set; }  //�N7�̒B����
        public decimal TASSEI_RITSU_8 { get; set; }  //�N8�̒B����
        public decimal TASSEI_RITSU_9 { get; set; }  //�N9�̒B����
        public decimal TASSEI_GOKEI { get; set; }   //�B�������v
    }
}