using System;

namespace r_framework.Logic
{
    /// <summary>
    /// 休日判定フラグ
    /// </summary>
    public static class HolidayCheckLogic
    {
        /// <summary>
        /// 春分の日を返すメソッド
        /// </summary>
        /// <prameparam name="yy">年情報</prameparam>
        public static int Syunbun(int yy)
        {
            int dd;
            if (yy <= 1947)
            {
                dd = 99;
            }
            else if (yy <= 1979)
            {
                dd = (int)(20.8357 + (0.242194 * (yy - 1980)) - (int)((yy - 1983) / 4));
            }
            else if (yy <= 2099)
            {
                dd = (int)(20.8431 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else if (yy <= 2150)
            {
                dd = (int)(21.851 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else
            {
                dd = 99;
            }
            return dd;
        }
        /// <summary>
        /// 秋分の日を返すメソッド
        /// </summary>
        /// <prameparam name="yy">年情報</prameparam>
        public static int Syubun(int yy)
        {
            int dd;
            if (yy <= 1947)
            {
                dd = 99;
            }
            else if (yy <= 1979)
            {
                dd = (int)(23.2588 + (0.242194 * (yy - 1980)) - (int)((yy - 1983) / 4));
            }
            else if (yy <= 2099)
            {
                dd = (int)(23.2488 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else if (yy <= 2150)
            {
                dd = (int)(24.2488 + (0.242194 * (yy - 1980)) - (int)((yy - 1980) / 4));
            }
            else
            {
                dd = 99;
            }
            return dd;
        }

        /// <summary>
        /// 祝日情報（戻り値）
        /// </summary>
        public struct HolidayInfo
        {
            /// <summary>
            /// 祝日情報Enum
            /// </summary>
            public enum HOLIDAY
            {
                WEEKDAY = 0, // 平日
                HOLIDAY = 1, // 休日
                C_HOLIDAY = 2, // 振休
                SYUKUJITSU = 3, // 祝日
            };
            /// <summary>
            /// 祝日の種類
            /// </summary>
            public HOLIDAY holiday; // その日の種類
            /// <summary>
            /// 曜日
            /// </summary>
            public DayOfWeek week; // その日の曜日
            /// <summary>
            /// 祝日名
            /// </summary>
            public String name; // その日に名前が付いている場合はその名前。
        };
        /// <summary>
        /// 祝日法施行日
        /// </summary>
        private static readonly DateTime SYUKUJITSU = new DateTime(1948, 7, 20);
        /// <summary>
        /// 振替休日制度の開始日
        /// </summary>
        private static readonly DateTime FURIKAE = new DateTime(1973, 07, 12);

        /// <summary>
        /// 祝日情報を取得するメソッド
        /// </summary>
        public static HolidayInfo Holiday(DateTime t)
        {
            int yy = t.Year;
            int mm = t.Month;
            int dd = t.Day;
            DayOfWeek ww = t.DayOfWeek;

            HolidayInfo result = new HolidayInfo();
            result.week = ww;
            result.holiday = HolidayInfo.HOLIDAY.WEEKDAY;

            // 祝日法施行以前
            if (t < SYUKUJITSU) return result;

            switch (mm)
            {
                // １月 //
                case 1:
                    if (dd == 1)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "元日";
                    }
                    else
                    {
                        if (yy >= 2000)
                        {
                            if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "成人の日";
                            }
                        }
                        else
                        {
                            if (dd == 15)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "成人の日";
                            }
                        }
                    }
                    break;
                // ２月 //
                case 2:
                    if (dd == 11)
                    {
                        if (yy >= 1967)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "建国記念の日";
                        }
                    }
                    else if ((yy == 1989) && (dd == 24))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "昭和天皇の大喪の礼";
                    }
                    break;
                // ３月 //
                case 3:
                    if (dd == Syunbun(yy))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "春分の日";
                    }
                    break;
                // ４月 //
                case 4:
                    if (dd == 29)
                    {
                        if (yy >= 2007)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "昭和の日";
                        }
                        else if (yy >= 1989)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "みどりの日";
                        }
                        else
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";
                        }
                    }
                    else if ((yy == 1959) && (dd == 10))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "皇太子明仁親王の結婚の儀";
                    }
                    break;
                // ５月 //
                case 5:
                    if (dd == 3)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "憲法記念日";
                    }
                    else if (dd == 4)
                    {
                        if (yy >= 2007)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "みどりの日";
                        }
                        else if (yy >= 1986)
                        {
                            /* 5/4が日曜日は『只の日曜』､月曜日は『憲法記念日の振替休日』(～2006年)*/
                            if (ww > DayOfWeek.Monday)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                                result.name = "国民の休日";
                            }
                        }
                    }
                    else if (dd == 5)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "こどもの日";
                    }
                    else if (dd == 6)
                    {
                        if ((yy >= 2007) && ((ww == DayOfWeek.Tuesday) || (ww == DayOfWeek.Wednesday)))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.C_HOLIDAY;
                            result.name = "振替休日";
                        }
                    }
                    break;
                // ６月 //
                case 6:
                    if ((yy == 1993) && (dd == 9))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "皇太子徳仁親王の結婚の儀";
                    }
                    break;
                // ７月 //
                case 7:
                    if (yy >= 2003)
                    {
                        if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    else if (yy >= 1996)
                    {
                        if (dd == 20)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "海の日";
                        }
                    }
                    break;
                // ８月 //
                case 8:
                    break;
                // ９月 //
                case 9:
                    if (dd == Syubun(yy))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "秋分の日";
                    }
                    else
                    {
                        if (yy >= 2003)
                        {
                            if (((int)((dd - 1) / 7) == 2) && (ww == DayOfWeek.Monday))
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "敬老の日";
                            }
                            else if (ww == DayOfWeek.Tuesday)
                            {
                                if (dd == Syubun(yy) - 1)
                                {
                                    result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                                    result.name = "国民の休日";
                                }
                            }
                        }
                        else if (yy >= 1966)
                        {
                            if (dd == 15)
                            {
                                result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                                result.name = "敬老の日";
                            }
                        }
                    }
                    break;
                // １０月 //
                case 10:
                    if (yy >= 2000)
                    {
                        if (((int)((dd - 1) / 7) == 1) && (ww == DayOfWeek.Monday))
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "体育の日";
                        }
                    }
                    else if (yy >= 1966)
                    {
                        if (dd == 10)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "体育の日";
                        }
                    }
                    break;
                // １１月 //
                case 11:
                    if (dd == 3)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "文化の日";
                    }
                    else if (dd == 23)
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "勤労感謝の日";
                    }
                    else if ((yy == 1990) && (dd == 12))
                    {
                        result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                        result.name = "即位礼正殿の儀";
                    }
                    break;
                // １２月 //
                case 12:
                    if (dd == 23)
                    {
                        if (yy >= 1989)
                        {
                            result.holiday = HolidayInfo.HOLIDAY.SYUKUJITSU;
                            result.name = "天皇誕生日";
                        }
                    }
                    break;
                default:
                    break;
            }

            if ((result.holiday == HolidayInfo.HOLIDAY.WEEKDAY
                 || result.holiday == HolidayInfo.HOLIDAY.HOLIDAY) &&
                (ww == DayOfWeek.Monday))
            {
                /*月曜以外は振替休日判定不要
                  5/6(火,水)の判定は上記ステップで処理済
                  5/6(月)はここで判定する  */
                if (t >= FURIKAE)
                {
                    if (Holiday(t.AddDays(-1)).holiday == HolidayInfo.HOLIDAY.SYUKUJITSU)
                    {    /* 再帰呼出 */
                        result.holiday = HolidayInfo.HOLIDAY.C_HOLIDAY;
                        result.name = "振替休日";
                    }
                }
            }

            if (result.week == DayOfWeek.Sunday)
            {
                result.holiday = HolidayInfo.HOLIDAY.HOLIDAY;
                result.name = "日曜日";
            }

            return result;
        }
    }
}
