using System;
using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class T_UKETSUKE_SLIP : SuperEntity
    {
        private SqlDateTime _GENCHAKU_TIME;
        private SqlDateTime _SAGYOU_TIME;
        private SqlDateTime _IDOU_TIME_FROM;
        private SqlDateTime _IDOU_TIME_TO;

        public SqlInt32 UKETSUKE_NO { get; set; }
        public SqlInt32 SLIP_GROUP_NO { get; set; }
        public SqlInt32 UKETSUKE_SHURUI { get; set; }
        public SqlDateTime UKETSUKE_DATE { get; set; }
        public SqlInt32 HAISHA_JOUKYOU_CD { get; set; }
        public SqlInt32 HAISHA_SHURUI_CD { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string EIGYOU_TANTOU_CD { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_BA_CD { get; set; }
        public string NIZUMI_GYOUSHA_CD { get; set; }
        public string NIZUMI_BA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public SqlDateTime SAGYOU_DATE { get; set; }
        public SqlDateTime NIZUMI_DATE { get; set; }
        public string NIZUMI_BIKOU { get; set; }
        public SqlInt32 GENCHAKU_SHURUI_CD { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public SqlInt32 DAISUU_NUMERATOR { get; set; }
        public SqlInt32 DAISUU_DENOMINATOR { get; set; }
        public string DRIVER_CD { get; set; }
        public SqlInt32 MANI_SHURUI_CD { get; set; }
        public SqlInt32 MANI_TEHAI_CD { get; set; }
        public SqlInt32 KONTENA_SOUSA_CD { get; set; }
        public SqlInt32 COURSE_KUMIKOMI_CD { get; set; }
        public string UKETSUKE_BIKOU1 { get; set; }
        public string UKETSUKE_BIKOU2 { get; set; }
        public string UKETSUKE_BIKOU3 { get; set; }
        public string DRIVER_INSTRUCTION1 { get; set; }
        public string DRIVER_INSTRUCTION2 { get; set; }
        public string DRIVER_INSTRUCTION3 { get; set; }
        public string BUPPAN_BIKOU1 { get; set; }
        public string BUPPAN_BIKOU2 { get; set; }
        public string BUPPAN_BIKOU3 { get; set; }
        public SqlDecimal KINGAKU_KEI { get; set; }
        public SqlDecimal SHOUHIZEI_KEI { get; set; }
        public SqlDecimal KINGAKU_TOTAL { get; set; }


        public SqlDateTime GENCHAKU_TIME
        {
            get { return _GENCHAKU_TIME; }
            set
            {
                if (SqlDateTime.Null != value)
                {
                    DateTime time = (DateTime)value;
                    this._GENCHAKU_TIME_H = time.Hour.ToString();
                    this._GENCHAKU_TIME_M = time.Minute.ToString();
                }
                this._GENCHAKU_TIME = value;
            }
        }
        public SqlDateTime SAGYOU_TIME
        {
            get { return _SAGYOU_TIME; }
            set
            {
                if (SqlDateTime.Null != value)
                {
                    DateTime time = (DateTime)value;
                    this._SAGYOU_TIME_H = time.Hour.ToString();
                    this._SAGYOU_TIME_M = time.Minute.ToString();
                }
                this._SAGYOU_TIME = value;
            }
        }
        public SqlDateTime IDOU_TIME_FROM
        {
            get { return _IDOU_TIME_FROM; }
            set
            {
                if (SqlDateTime.Null != value)
                {
                    DateTime time = (DateTime)value;
                    this._IDOU_TIME_FROM_H = time.Hour.ToString();
                    this._IDOU_TIME_FROM_M = time.Minute.ToString();
                }
                this._IDOU_TIME_FROM = value;
            }
        }
        public SqlDateTime IDOU_TIME_TO
        {
            get { return _IDOU_TIME_TO; }
            set
            {
                if (SqlDateTime.Null != value)
                {
                    DateTime time = (DateTime)value;
                    this._IDOU_TIME_TO_H = time.Hour.ToString();
                    this._IDOU_TIME_TO_M = time.Minute.ToString();
                }
                this._IDOU_TIME_TO = value;
            }
        }
        private string _GENCHAKU_TIME_H;
        public virtual string GENCHAKU_TIME_H
        {
            set
            {
                _GENCHAKU_TIME_H = value;
                if (!string.IsNullOrEmpty(this._GENCHAKU_TIME_H) && !string.IsNullOrEmpty(this._GENCHAKU_TIME_M))
                {
                    this._GENCHAKU_TIME = DateTime.Parse(this._GENCHAKU_TIME_H + ":" + this._GENCHAKU_TIME_M);
                }
            }
            get
            {
                if (!this._GENCHAKU_TIME.IsNull)
                {
                    return this._GENCHAKU_TIME.Value.Hour.ToString();
                }
                return "";
            }
        }
        private string _GENCHAKU_TIME_M;
        public virtual string GENCHAKU_TIME_M
        {
            set
            {
                _GENCHAKU_TIME_M = value;
                if (!string.IsNullOrEmpty(this._GENCHAKU_TIME_H) && !string.IsNullOrEmpty(this._GENCHAKU_TIME_M))
                {
                    this._GENCHAKU_TIME = DateTime.Parse(this._GENCHAKU_TIME_H + ":" + this._GENCHAKU_TIME_M);
                }
                else
                {
                    this._GENCHAKU_TIME = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._GENCHAKU_TIME.IsNull)
                {
                    return this._GENCHAKU_TIME.Value.Minute.ToString();
                }
                return "";

            }
        }

        private string _SAGYOU_TIME_H;
        public virtual string SAGYOU_TIME_H
        {
            set
            {
                _SAGYOU_TIME_H = value;
                if (!string.IsNullOrEmpty(this._SAGYOU_TIME_H) && !string.IsNullOrEmpty(this._SAGYOU_TIME_M))
                {
                    this._SAGYOU_TIME = DateTime.Parse(this._SAGYOU_TIME_H + ":" + this._SAGYOU_TIME_M);
                }
                else
                {
                    this._SAGYOU_TIME = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._SAGYOU_TIME.IsNull)
                {
                    return this._SAGYOU_TIME.Value.Hour.ToString();
                }
                return "";
            }
        }
        private string _SAGYOU_TIME_M;
        public virtual string SAGYOU_TIME_M
        {
            set
            {
                _SAGYOU_TIME_M = value;
                if (!string.IsNullOrEmpty(this._SAGYOU_TIME_H) && !string.IsNullOrEmpty(this._SAGYOU_TIME_M))
                {
                    this._SAGYOU_TIME = DateTime.Parse(this._SAGYOU_TIME_H + ":" + this._SAGYOU_TIME_M);
                }
                else
                {
                    this._SAGYOU_TIME = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._SAGYOU_TIME.IsNull)
                {
                    return this._SAGYOU_TIME.Value.Minute.ToString();
                }
                return "";
            }
        }

        private string _IDOU_TIME_FROM_H;
        public virtual string IDOU_TIME_FROM_H
        {
            set
            {
                _IDOU_TIME_FROM_H = value;
                if (!string.IsNullOrEmpty(this._IDOU_TIME_FROM_H) && !string.IsNullOrEmpty(this._IDOU_TIME_FROM_M))
                {
                    this._IDOU_TIME_FROM = DateTime.Parse(this._IDOU_TIME_FROM_H + ":" + this._IDOU_TIME_FROM_M);
                }
                else
                {
                    this._IDOU_TIME_FROM = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._IDOU_TIME_FROM.IsNull)
                {
                    return this._IDOU_TIME_FROM.Value.Hour.ToString();
                }
                return "";
            }
        }
        private string _IDOU_TIME_FROM_M;
        public virtual string IDOU_TIME_FROM_M
        {
            set
            {
                _IDOU_TIME_FROM_M = value;
                if (!string.IsNullOrEmpty(this._IDOU_TIME_FROM_H) && !string.IsNullOrEmpty(this._IDOU_TIME_FROM_M))
                {
                    this._IDOU_TIME_FROM = DateTime.Parse(this._IDOU_TIME_FROM_H + ":" + this._IDOU_TIME_FROM_M);
                }
                else
                {
                    this._IDOU_TIME_FROM = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._IDOU_TIME_FROM.IsNull)
                {
                    return this._IDOU_TIME_FROM.Value.Minute.ToString();
                }
                return "";
            }
        }

        private string _IDOU_TIME_TO_H;
        public virtual string IDOU_TIME_TO_H
        {
            set
            {
                _IDOU_TIME_TO_H = value;
                if (!string.IsNullOrEmpty(this._IDOU_TIME_TO_H) && !string.IsNullOrEmpty(this._IDOU_TIME_TO_M))
                {
                    this._IDOU_TIME_TO = DateTime.Parse(this._IDOU_TIME_TO_H + ":" + this._IDOU_TIME_TO_M);
                }
                else
                {
                    this._IDOU_TIME_TO = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._IDOU_TIME_TO.IsNull)
                {
                    return this._IDOU_TIME_TO.Value.Hour.ToString();
                }
                return "";
            }
        }
        private string _IDOU_TIME_TO_M;
        public virtual string IDOU_TIME_TO_M
        {
            set
            {
                _IDOU_TIME_TO_M = value;

                if (!string.IsNullOrEmpty(this._IDOU_TIME_TO_H) && !string.IsNullOrEmpty(this._IDOU_TIME_TO_M))
                {
                    this._IDOU_TIME_TO = DateTime.Parse(this._IDOU_TIME_TO_H + ":" + this._IDOU_TIME_TO_M);
                }
                else
                {
                    this._IDOU_TIME_TO = SqlDateTime.Null;
                }
            }
            get
            {
                if (!this._IDOU_TIME_TO.IsNull)
                {
                    return this._IDOU_TIME_TO.Value.Minute.ToString();
                }
                return "";
            }
        }

    }
}