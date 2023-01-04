using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DocDiff
{
    /// <summary>
    /// Helper class for datetime texts
    /// </summary>
    public static class DlkDateTimeHelper
    {
        enum DaysAbbreviations
        {
            Mon, Tue, Wed, Thu, Fri, Sat, Sun
        }

        enum MonthsAbbreviations
        {
            Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec
        }

        enum TimeZones
        {
            ACDT, ACST, ACT, ACWST, ADT, AEDT, AEST, AET, AFT, AKDT, AKST, ALMT, AMST, AMT, ANAT, AQTT, ART, AST, AWST, AZOST, AZOT, AZT,
            BNT, BIOT, BIT, BOT, BRST, BRT, BST, BTT,
            CAT, CCT, CDT, CEST, CET, CHADT, CHAST, CHOT, CHOST, CHST, CHUT, CIST, CKT, CLST, CLT, COST, COT, CST, CT, CVT, CWST, CXT, DAVT, DDUT, DFT,
            EASST, EAST, EAT, ECT, EDT, EEST, EET, EGST, EGT, EST, ET,
            FET, FJT, FKST, FKT, FNT,
            GALT, GAMT, GET, GFT, GILT, GIT, GMT, GST, GYT,
            HDT, HAEC, HST, HKT, HMT, HOVST, HOVT,
            ICT, IDLW, IDT, IOT, IRDT, IRKT, IRST, IST,
            JST,
            KALT, KGT, KOST, KRAT, KST,
            LHST, LINT,
            MAGT, MART, MAWT, MDT, MET, MEST, MHT, MIST, MIT, MMT, MSK, MST, MUT, MVT, MYT,
            NCT, NDT, NFT, NOVT, NPT, NST, NT, NUT, NZDT, NZST,
            OMST, ORAT,
            PDT, PET, PETT, PGT, PHOT, PHT, PHST, PKT, PMDT, PMST, PONT, PST, PWT, PYST, PYT,
            RET, ROTT,
            SAKT, SAMT, SAST, SBT, SCT, SDT, SGT, SLST, SRET, SRT, SST, SYOT,
            TAHT, THA, TFT, TJT, TKT, TLT, TMT, TRT, TOT, TVT,
            ULAST, ULAT, UTC, UYST, UYT, UZT,
            VET, VLAT, VOLT, VOST, VUT,
            WAKT, WAST, WAT, WEST, WET, WIB, WIT, WITA, WGST, WGT, WST,
            YAKT, YEKT
        }

        /// <summary>
        /// Checks whether string contains datetime
        /// </summary>
        /// <param name="text">Text of cell</param>
        /// <param name="dateTimeTexts">Datetime texts found in the string</param>
        public static bool HasDateAndTime(string text, out List<string> dateTimeTexts)
        {
            dateTimeTexts = new List<string>();
            bool hasDateAndTime = false;
            List<string> months = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
            months.RemoveAt(12);
            string[] inputText = text.Split(' ');

            for (int i = 0; i < inputText.Length; i++)
            {
                // check dates with spaces
                if (months.Any(x => x.ToLower() == inputText[i].ToLower() ||
                Enum.GetValues(typeof(MonthsAbbreviations)).Cast<MonthsAbbreviations>().Any(monthAbbrev => monthAbbrev.ToString().ToLower() == inputText[i].ToLower())))
                {
                    string nextText1 = "";
                    string nextText2 = "";
                    string concatenatedDate = "";

                    if (i + 2 < inputText.Length)
                    {
                        nextText1 = inputText[i + 1];
                        nextText2 = inputText[i + 2];

                        concatenatedDate = inputText[i] + " " + nextText1 + " " + nextText2;
                        bool concatenatedDateIsDateTime = DateTime.TryParse(concatenatedDate, out _);

                        if (concatenatedDateIsDateTime)
                        {
                            dateTimeTexts.Add(concatenatedDate);
                        }
                    }

                    if (i + 1 < inputText.Length)
                    {
                        nextText1 = inputText[i + 1];

                        concatenatedDate = inputText[i] + " " + nextText1;
                        bool concatenatedDateIsDateTime = DateTime.TryParse(concatenatedDate, out _);

                        if (concatenatedDateIsDateTime)
                        {
                            dateTimeTexts.Add(concatenatedDate);
                        }
                    }
                }

                // check datetime
                hasDateAndTime = DateTime.TryParse(inputText[i], out _);
                if (hasDateAndTime)
                {
                    string datetime = inputText[i];

                    if (i + 1 < inputText.Length)
                    {
                        if (inputText[i + 1].ToLower() == "am" || inputText[i + 1].ToLower() == "pm")
                        {
                            datetime = inputText[i] + " " + inputText[i + 1];
                        }
                    }

                    dateTimeTexts.Add(datetime);
                }

                // check days
                if (Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Any(day => day.ToString().ToLower() == inputText[i].ToLower()))
                {
                    dateTimeTexts.Add(inputText[i]);
                }

                // check months
                if (months.Any(month => month.ToString().ToLower() == inputText[i].ToLower()))
                {
                    dateTimeTexts.Add(inputText[i]);
                }

                // check days abbreviations
                if (Enum.GetValues(typeof(DaysAbbreviations)).Cast<DaysAbbreviations>().Any(dayAbbrev => dayAbbrev.ToString().ToLower() == inputText[i].ToLower()))
                {
                    dateTimeTexts.Add(inputText[i]);
                }

                // check months abbreviations
                if (Enum.GetValues(typeof(MonthsAbbreviations)).Cast<MonthsAbbreviations>().Any(monthAbbrev => monthAbbrev.ToString().ToLower() == inputText[i].ToLower()))
                {
                    dateTimeTexts.Add(inputText[i]);
                }

                // check timezones
                if (Enum.GetValues(typeof(TimeZones)).Cast<TimeZones>().Any(timeZone => timeZone.ToString() == inputText[i]))
                {
                    dateTimeTexts.Add(inputText[i]);
                }
            }

            if (dateTimeTexts.Any())
            {
                hasDateAndTime = true;
            }

            return hasDateAndTime;
        }
    }
}
