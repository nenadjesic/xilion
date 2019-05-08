using System;
using System.Collections.Generic;
using System.Linq;

namespace Xilion.Framework.Extensions
{

    #region TimeZone Ids

    // [0]: {(UTC-12:00) International Date Line West}
    // [1]: {(UTC-11:00) Coordinated Universal Time-11}
    // [2]: {(UTC-11:00) Samoa}
    // [3]: {(UTC-10:00) Hawaii}
    // [4]: {(UTC-09:00) Alaska}
    // [5]: {(UTC-08:00) Baja California}
    // [6]: {(UTC-08:00) Pacific Time (US & Canada)}
    // [7]: {(UTC-07:00) Arizona}
    // [8]: {(UTC-07:00) Chihuahua, La Paz, Mazatlan}
    // [9]: {(UTC-07:00) Mountain Time (US & Canada)}
    //[10]: {(UTC-06:00) Central America}
    //[11]: {(UTC-06:00) Central Time (US & Canada)}
    //[12]: {(UTC-06:00) Guadalajara, Mexico City, Monterrey}
    //[13]: {(UTC-06:00) Saskatchewan}
    //[14]: {(UTC-05:00) Bogota, Lima, Quito}
    //[15]: {(UTC-05:00) Eastern Time (US & Canada)}
    //[16]: {(UTC-05:00) Indiana (East)}
    //[17]: {(UTC-04:30) Caracas}
    //[18]: {(UTC-04:00) Asuncion}
    //[19]: {(UTC-04:00) Atlantic Time (Canada)}
    //[20]: {(UTC-04:00) Cuiaba}
    //[21]: {(UTC-04:00) Georgetown, La Paz, Manaus, San Juan}
    //[22]: {(UTC-04:00) Santiago}
    //[23]: {(UTC-03:30) Newfoundland}
    //[24]: {(UTC-03:00) Brasilia}
    //[25]: {(UTC-03:00) Buenos Aires}
    //[26]: {(UTC-03:00) Cayenne, Fortaleza}
    //[27]: {(UTC-03:00) Greenland}
    //[28]: {(UTC-03:00) Montevideo}
    //[29]: {(UTC-02:00) Coordinated Universal Time-02}
    //[30]: {(UTC-02:00) Mid-Atlantic}
    //[31]: {(UTC-01:00) Azores}
    //[32]: {(UTC-01:00) Cape Verde Is.}
    //[33]: {(UTC) Casablanca}
    //[34]: {(UTC) Coordinated Universal Time}
    //[35]: {(UTC) Dublin, Edinburgh, Lisbon, London}
    //[36]: {(UTC) Monrovia, Reykjavik}
    //[37]: {(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna}
    //[38]: {(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague}
    //[39]: {(UTC+01:00) Brussels, Copenhagen, Madrid, Paris}
    //[40]: {(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb}
    //[41]: {(UTC+01:00) West Central Africa}
    //[42]: {(UTC+01:00) Windhoek}
    //[43]: {(UTC+02:00) Amman}
    //[44]: {(UTC+02:00) Athens, Bucharest, Istanbul}
    //[45]: {(UTC+02:00) Beirut}
    //[46]: {(UTC+02:00) Cairo}
    //[47]: {(UTC+02:00) Damascus}
    //[48]: {(UTC+02:00) Harare, Pretoria}
    //[49]: {(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius}
    //[50]: {(UTC+02:00) Jerusalem}
    //[51]: {(UTC+02:00) Minsk}
    //[52]: {(UTC+03:00) Baghdad}
    //[53]: {(UTC+03:00) Kuwait, Riyadh}
    //[54]: {(UTC+03:00) Moscow, St. Petersburg, Volgograd}
    //[55]: {(UTC+03:00) Nairobi}
    //[56]: {(UTC+03:30) Tehran}
    //[57]: {(UTC+04:00) Abu Dhabi, Muscat}
    //[58]: {(UTC+04:00) Baku}
    //[59]: {(UTC+04:00) Port Louis}
    //[60]: {(UTC+04:00) Tbilisi}
    //[61]: {(UTC+04:00) Yerevan}
    //[62]: {(UTC+04:30) Kabul}
    //[63]: {(UTC+05:00) Ekaterinburg}
    //[64]: {(UTC+05:00) Islamabad, Karachi}
    //[65]: {(UTC+05:00) Tashkent}
    //[66]: {(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi}
    //[67]: {(UTC+05:30) Sri Jayawardenepura}
    //[68]: {(UTC+05:45) Kathmandu}
    //[69]: {(UTC+06:00) Astana}
    //[70]: {(UTC+06:00) Dhaka}
    //[71]: {(UTC+06:00) Novosibirsk}
    //[72]: {(UTC+06:30) Yangon (Rangoon)}
    //[73]: {(UTC+07:00) Bangkok, Hanoi, Jakarta}
    //[74]: {(UTC+07:00) Krasnoyarsk}
    //[75]: {(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi}
    //[76]: {(UTC+08:00) Irkutsk}
    //[77]: {(UTC+08:00) Kuala Lumpur, Singapore}
    //[78]: {(UTC+08:00) Perth}
    //[79]: {(UTC+08:00) Taipei}
    //[80]: {(UTC+08:00) Ulaanbaatar}
    //[81]: {(UTC+09:00) Osaka, Sapporo, Tokyo}
    //[82]: {(UTC+09:00) Seoul}
    //[83]: {(UTC+09:00) Yakutsk}
    //[84]: {(UTC+09:30) Adelaide}
    //[85]: {(UTC+09:30) Darwin}
    //[86]: {(UTC+10:00) Brisbane}
    //[87]: {(UTC+10:00) Canberra, Melbourne, Sydney}
    //[88]: {(UTC+10:00) Guam, Port Moresby}
    //[89]: {(UTC+10:00) Hobart}
    //[90]: {(UTC+10:00) Vladivostok}
    //[91]: {(UTC+11:00) Magadan, Solomon Is., New Caledonia}
    //[92]: {(UTC+12:00) Auckland, Wellington}
    //[93]: {(UTC+12:00) Coordinated Universal Time+12}
    //[94]: {(UTC+12:00) Fiji}
    //[95]: {(UTC+12:00) Petropavlovsk-Kamchatsky - Old}
    //[96]: {(UTC+13:00) Nuku'alofa}

    #endregion

    /// <summary>
    /// Represent timezones by id
    /// Each time zone has unique id
    /// </summary>
    public class TimeZoneExtension
    {
        private static TimeZoneExtension _instance;
        private readonly IDictionary<int, TimeZoneInfo> _timeZones = new Dictionary<int, TimeZoneInfo>();

        /// <summary>
        /// Default constructor
        /// </summary>
        private TimeZoneExtension()
        {
            int i = 0;
            _timeZones.Add(i++, TimeZoneInfo.Utc);
            foreach (TimeZoneInfo item in TimeZoneInfo.GetSystemTimeZones())
                _timeZones.Add(i++, item);
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static TimeZoneExtension Instance
        {
            get { return _instance ?? (_instance = new TimeZoneExtension()); }
        }

        /// <summary>
        /// Collection of key value pair.
        /// Key is id and value is framework id
        /// </summary>
        public IDictionary<int, TimeZoneInfo> TimeZonesCollection
        {
            get { return _timeZones; }
        }

        /// <summary>
        /// Get time zone by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Time zone id from framework</returns>
        public TimeZoneInfo GetTimeZoneById(int id)
        {
            return _timeZones[id];
        }

        /// <summary>
        /// Gets the time zone id.
        /// </summary>
        /// <param name="timeZone">Time zone to get the id for.</param>
        /// <returns>An id of the given time zone.</returns>
        public int GetTimeZoneId(TimeZoneInfo timeZone)
        {
            Guard.IsNotNull(timeZone, "timeZone");
            return TimeZonesCollection
                .Where(x => x.Value.Id == timeZone.Id)
                .Select(x => x.Key)
                .FirstOrDefault();
        }
    }
}