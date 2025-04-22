using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity
{
    /// <summary>
    /// �������f�[�^�N���X
    /// </summary>
    public class GetujiInfo : SuperEntity
    {
        // SQL���擾���鍀��
        public string BUSHO_CD { get; set; }          //�����R�[�h
        public string BUSHO_NAME { get; set; }  //������
        public string SHAIN_CD { get; set; }          //�Ј��R�[�h
        public string SHAIN_NAME { get; set; }  //�Ј���
        public decimal YOSAN_1 { get; set; }    //��1�̗\�Z
        public decimal YOSAN_2 { get; set; }    //��2�̗\�Z
        public decimal YOSAN_3 { get; set; }    //��3�̗\�Z
        public decimal YOSAN_4 { get; set; }    //��4�̗\�Z
        public decimal YOSAN_5 { get; set; }    //��5�̗\�Z
        public decimal YOSAN_6 { get; set; }    //��6�̗\�Z
        public decimal YOSAN_7 { get; set; }    //��7�̗\�Z
        public decimal YOSAN_8 { get; set; }    //��8�̗\�Z
        public decimal YOSAN_9 { get; set; }    //��9�̗\�Z
        public decimal YOSAN_10 { get; set; }   //��10�̗\�Z
        public decimal YOSAN_11 { get; set; }   //��11�̗\�Z
        public decimal YOSAN_12 { get; set; }   //��12�̗\�Z
        public decimal YOSAN_GOUKEI { get; set; }     //�\�Z���v
        public decimal JISSEKI_1 { get; set; }  //��1�̎���
        public decimal JISSEKI_2 { get; set; }  //��2�̎���
        public decimal JISSEKI_3 { get; set; }  //��3�̎���
        public decimal JISSEKI_4 { get; set; }  //��4�̎���
        public decimal JISSEKI_5 { get; set; }  //��5�̎���
        public decimal JISSEKI_6 { get; set; }  //��6�̎���
        public decimal JISSEKI_7 { get; set; }  //��7�̎���
        public decimal JISSEKI_8 { get; set; }  //��8�̎���
        public decimal JISSEKI_9 { get; set; }  //��9�̎���
        public decimal JISSEKI_10 { get; set; } //��10�̎���
        public decimal JISSEKI_11 { get; set; } //��11�̎���
        public decimal JISSEKI_12 { get; set; } //��12�̎���
        public decimal JISSEKI_GOUKEI { get; set; }   //���э��v

        // �ҏW���ʍ���
        public decimal TASSEI_RITSU_1 { get; set; }  //��1�̒B����
        public decimal TASSEI_RITSU_2 { get; set; }  //��2�̒B����
        public decimal TASSEI_RITSU_3 { get; set; }  //��3�̒B����
        public decimal TASSEI_RITSU_4 { get; set; }  //��4�̒B����
        public decimal TASSEI_RITSU_5 { get; set; }  //��5�̒B����
        public decimal TASSEI_RITSU_6 { get; set; }  //��6�̒B����
        public decimal TASSEI_RITSU_7 { get; set; }  //��7�̒B����
        public decimal TASSEI_RITSU_8 { get; set; }  //��8�̒B����
        public decimal TASSEI_RITSU_9 { get; set; }  //��9�̒B����
        public decimal TASSEI_RITSU_10 { get; set; } //��10�̒B����
        public decimal TASSEI_RITSU_11 { get; set; } //��11�̒B����
        public decimal TASSEI_RITSU_12 { get; set; } //��12�̒B����
        public decimal TASSEI_GOKEI { get; set; }   //�B�������v
    }
}