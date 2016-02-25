using System.Collections.Generic;

namespace DeclensionOfCases
{
    public class DeclensionOfCases
    {
        /// <summary>
        /// Називний відмінок
        /// </summary>
        /// <returns></returns>
        public string Nominative
        { get; protected set; }

        /// <summary>
        /// Родовий відмінок
        /// </summary>
        /// <returns></returns>
        public string Genitive
        { get; protected set; }

        /// <summary>
        /// Давальний відмінок
        /// </summary>
        public string Dative
        { get; protected set; }

        /// <summary>
        /// Знахідний відмінок
        /// </summary>
        public string Accusative
        { get; protected set; }

        /// <summary>
        /// Орудний відмінок
        /// </summary>
        public string Instrumental
        { get; protected set; }

        /// <summary>
        /// Місцевий відмінок
        /// </summary>
        public string Locative
        { get; protected set; }

        /// <summary>
        /// Роздільник
        /// </summary>
        public static string Separator = " ";

        /// <summary>
        /// Відмінювання ПІБ по відмінкам:
        ///     "Nominative" - Називний;
        ///     "Genitive" - Родовий;
        ///     "Dative" - Давальний;
        ///     "Accusative" - Знахідний;
        ///     "Instrumental" - Орудний;
        ///     "Locative" - Місцевий.
        /// </summary>
        /// <param name="Surname">  - Прізвище </param>
        /// <param name="Name"> - Ім'я </param>
        /// <param name="MiddleName"> - По-батькові </param>
        /// <param name="Shorted"> - Ознака скороченого ПІБ (за замовчанням "false")</param>
        public DeclensionOfCases(string Surname, string Name, string MiddleName, bool Shorted = false)
        {
            Dictionary<string, string> NameInCases = new Dictionary<string, string>();
            Dictionary<string, string> SurnameInCases = new Dictionary<string, string>();
            Dictionary<string, string> MiddleNameInCases = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(MiddleName))
            {
                if (MiddleName.Length > 4)
                {
                    if (MiddleName.Substring(MiddleName.Length - 2, 2).ToUpper() == "ИЧ" | MiddleName.Substring(MiddleName.Length - 2, 2).ToUpper() == "ІЧ")
                    {
                        if (!Shorted)
                        { NameInCases = NameToCases_Male(Name); }

                        SurnameInCases = SurnameToCases_Male(Surname);
                    }
                    else if (MiddleName.Substring(MiddleName.Length - 3, 3).ToUpper() == "ВНА")
                    {
                        if (!Shorted)
                        { NameInCases = NameToCases_Female(Name); }

                        SurnameInCases = SurnameToCases_Female(Surname);
                    }
                    if (!Shorted)
                    { MiddleNameInCases = MiddleNameToCases(MiddleName); }

                    if (!Shorted && NameInCases.Count > 0 && SurnameInCases.Count > 0 && MiddleNameInCases.Count > 0)
                    {
                        Nominative = string.Concat(Surname, Separator, Name, Separator, MiddleName);
                        Genitive = string.Concat(SurnameInCases["Genitive"], Separator, NameInCases["Genitive"], Separator, MiddleNameInCases["Genitive"]);
                        Dative = string.Concat(SurnameInCases["Dative"], Separator, NameInCases["Dative"], Separator, MiddleNameInCases["Dative"]);
                        Accusative = string.Concat(SurnameInCases["Accusative"], Separator, NameInCases["Accusative"], Separator, MiddleNameInCases["Accusative"]);
                        Instrumental = string.Concat(SurnameInCases["Instrumental"], Separator, NameInCases["Instrumental"], Separator, MiddleNameInCases["Instrumental"]);
                        Locative = string.Concat(SurnameInCases["Locative"], Separator, NameInCases["Locative"], Separator, MiddleNameInCases["Locative"]);
                    }
                    else if (Shorted && SurnameInCases.Count > 0)
                    {
                        string NameAndMiddleNameShorted = string.Concat(Name.Substring(0, 1), ".", MiddleName.Substring(0, 1), ".");

                        Nominative = string.Concat(Surname, Separator, NameAndMiddleNameShorted);
                        Genitive = string.Concat(SurnameInCases["Genitive"], NameAndMiddleNameShorted);
                        Dative = string.Concat(SurnameInCases["Dative"], NameAndMiddleNameShorted);
                        Accusative = string.Concat(SurnameInCases["Accusative"], NameAndMiddleNameShorted);
                        Instrumental = string.Concat(SurnameInCases["Instrumental"], NameAndMiddleNameShorted);
                        Locative = string.Concat(SurnameInCases["Locative"].ToString(), NameAndMiddleNameShorted);
                    }
                    else
                    {
                        string EmptyString = string.Empty;
                        Nominative = string.Concat(Surname, Separator, Name, Separator, MiddleName);
                        Genitive = EmptyString;
                        Dative = EmptyString;
                        Accusative = EmptyString;
                        Instrumental = EmptyString;
                        Locative = EmptyString;
                    }
                }
            }
        }

        //Відмінювання і'мя чоловічого роду 
        private Dictionary<string, string> NameToCases_Male(string Name)
        {
            Dictionary<string, string> NameInCases = new Dictionary<string, string>();

            int lngth = 0;
            int del = 0;

            string Consonants = "БВГДЖЗКЛМНПРСТФХЦЧШЩҐ";

            string[] AddEnd = { "", "", "", "", "" };

            string Ending = "";

            string LastChar = Name.Substring(Name.Length - 1).ToUpper();

            if (Consonants.Contains(LastChar))
            {
                AddEnd = new string[] { "а", "у", "а", "ом", "у" };
            }
            else
            {

                Ending = LastChar;

                if (Ending == "Й" | Ending == "Ь")
                {
                    AddEnd = new string[] { "я", "ю", "я", "єм", "ю" };
                    del = 1;
                }
                else if (Ending == "Я")
                {
                    AddEnd = new string[] { "і", "і", "ю", "єю", "і" };
                    del = 1;
                }
                else if (Ending == "О")
                {
                    AddEnd = new string[] { "а", "ові", "а", "ом", "ові" };
                    del = 1;
                }
                else if (Ending == "А")
                {
                    AddEnd = new string[] { "и", "і", "у", "ою", "і" };
                    del = 1;
                }
            }

            lngth = Name.Length - del;

            string NameToChange = Name.Substring(0, lngth);

            NameInCases.Add("Genitive", string.Concat(NameToChange, AddEnd[0]));
            NameInCases.Add("Dative", string.Concat(NameToChange, AddEnd[1]));
            NameInCases.Add("Accusative", string.Concat(NameToChange, AddEnd[2]));
            NameInCases.Add("Instrumental", string.Concat(NameToChange, AddEnd[3]));
            NameInCases.Add("Locative", string.Concat(NameToChange, AddEnd[4]));

            return NameInCases;
        }

        //Відмінювання ім'я жіночого роду 
        private Dictionary<string, string> NameToCases_Female(string Name)
        {
            Dictionary<string, string> NameInCases = new Dictionary<string, string>();

            int lngth = 0;
            int del = 0;

            string[] AddEnd = { "", "", "", "", "" };

            string Ending = "";

            string Consonants = "БВГДЖЗКЛМНПРСТФХЦЧШЩҐ";

            string LastChar = Name.Substring(Name.Length - 1).ToUpper();

            if (Consonants.Contains(LastChar))
            {
                AddEnd = new string[] { "і", "і", "", "ю", "і" };
            }
            else if (LastChar == "В")
            {
                AddEnd = new string[] { "і", "і", "", "'ю", "і" };
            }
            else
            {

                Ending = LastChar;

                if (Ending == "Я")
                {
                    AddEnd = new string[] { "і", "і", "ю", "ею", "і" };
                    del = 1;
                }
                else if (Ending == "А")
                {
                    AddEnd = new string[] { "и", "і", "у", "ою", "і" };
                    del = 1;
                }

                Ending = Name.Substring(Name.Length - 2).ToUpper();

                if (Ending == "ІЯ")
                {
                    AddEnd = new string[] { "ї", "ї", "ю", "єю", "ї" };
                    del = 1;
                }
            }

            lngth = Name.Length - del;

            string NameToChange = Name.Substring(0, lngth);

            string NameInGenitive = string.Concat(NameToChange, AddEnd[0]);
            string NameInDative = string.Concat(NameToChange, AddEnd[1]);
            string NameInAccusative = string.Concat(NameToChange, AddEnd[2]);
            string NameInInstrumental = string.Concat(NameToChange, AddEnd[3]);
            string NameInLocative = string.Concat(NameToChange, AddEnd[4]);

            Ending = Name.Substring(Name.Length - 2).ToUpper();

            string g = "Г";
            string k = "К";
            string kh = "Х";

            string LetterForChanging = Ending.Substring(0, 1).ToUpper();

            if (LetterForChanging == g || LetterForChanging == k || LetterForChanging == kh)
            {
                string ChgLet = "";

                if (LetterForChanging == g)
                    ChgLet = "з";
                if (LetterForChanging == k)
                    ChgLet = "ц";
                if (LetterForChanging == kh)
                    ChgLet = "с";

                string EndingDat = NameInDative.Substring(NameInDative.Length - 1);
                string EndingLoc = NameInLocative.Substring(NameInLocative.Length - 1);

                NameInDative = NameInDative.Substring(0, NameInDative.Length - 2);
                NameInLocative = NameInLocative.Substring(0, NameInLocative.Length - 2);

                NameInDative = string.Concat(NameInDative, ChgLet, EndingDat);
                NameInLocative = string.Concat(NameInLocative, ChgLet, EndingLoc);
            }

            NameInCases.Add("Genitive", NameInGenitive);
            NameInCases.Add("Dative", NameInDative);
            NameInCases.Add("Accusative", NameInAccusative);
            NameInCases.Add("Instrumental", NameInInstrumental);
            NameInCases.Add("Locative", NameInLocative);

            return NameInCases;
        }

        //Відмінювання прізвище чоловічого роду 
        private Dictionary<string, string> SurnameToCases_Male(string Surname)
        {
            Dictionary<string, string> SurnameInCases = new Dictionary<string, string>();

            int lngth = 0;
            int del = 0;

            string[] AddEnd = { "", "", "", "", "" };

            string Ending = "";

            string Vowels = "ЙАОІЇЕЄЯЮУИЭЬ";

            //string Consonants = "БВГДЖЗКЛМНПРСТФХЦЧШЩҐ";
            string ConsonantsRinging = "БДГЗЖҐ";
            string ConsonantsVoiceless = "КПСТФХШЦЧ";
            string ConsonantsSonorent = "ВНЛР";

            string LastChar = Surname.Substring(Surname.Length - 1).ToUpper();

            if (Vowels.Contains(LastChar))
            {
                Ending = LastChar;

                if (Ending == "Я")
                {
                    AddEnd = new string[] { "і", "і", "ю", "ею", "і" };
                    del = 1;
                }
                else if (Ending == "А")
                {
                    AddEnd = new string[] { "и", "і", "у", "ою", "і" };
                    del = 1;
                }
                else if (Ending == "Й")
                {
                    AddEnd = new string[] { "я", "ю", "я", "єм", "ю" };
                    del = 1;

                    string EndingJ = Surname.Substring(Surname.Length - 2).ToUpper();

                    if (EndingJ == "ИЙ" || EndingJ == "ОЙ")
                    {
                        AddEnd = new string[] { "ого", "ому", "ого", "им", "ому" };
                        del = 2;
                    }
                }
                else if (Ending == "О")
                {
                    AddEnd = new string[] { "а", "у", "а", "ом", "у" };
                    del = 1;

                    string EndingO = Surname.Substring(Surname.Length - 2).ToUpper();

                    if (EndingO == "ЬО")
                    {
                        AddEnd = new string[] { "я", "еві", "я", "ем", "еві" };
                        del = 2;
                    }

                    EndingO = Surname.Substring(Surname.Length - 3).ToUpper();

                    if (EndingO == "АГО" || EndingO == "ОВО")
                    {
                        AddEnd = new string[] { "", "", "", "", "" };
                        del = 0;
                    }
                }
                else if (Ending == "Ь")
                {
                    AddEnd = new string[] { "я", "ю", "я", "ем", "ю" };
                    del = 1;

                    string EndingMZn = Surname.Substring(Surname.Length - 3).ToUpper();

                    if (EndingMZn == "ЕЦЬ")
                    {
                        AddEnd = new string[] { "ця", "цю", "ця", "цем", "цеві" };
                        del = 3;
                    }
                    else if (EndingMZn == "ЄЦЬ")
                    {
                        AddEnd = new string[] { "йця", "йцю", "йця", "йцем", "йцеві" };
                        del = 3;
                    }
                    else if (EndingMZn == "ЕНЬ")
                    {
                        AddEnd = new string[] { "ня", "ню", "ня", "нем", "неві" };
                        del = 3;
                    }
                    else if (EndingMZn == "ЄНЬ")
                    {
                        AddEnd = new string[] { "йня", "йню", "йня", "йнем", "йневі" };
                        del = 3;
                    }
                    else if (EndingMZn == "ІНЬ")
                    {
                        AddEnd = new string[] { "оня", "оню", "оня", "онем", "оневі" };
                        del = 3;
                    }
                }
                else if (Ending == "И")
                {
                    AddEnd = new string[] { "", "", "", "", "" };
                    del = 0;
                }
            }
            else if (ConsonantsRinging.Contains(LastChar))
            {
                AddEnd = new string[] { "а", "у", "а", "ом", "у" };
                del = 0;
            }
            else if (ConsonantsVoiceless.Contains(LastChar))
            {
                AddEnd = new string[] { "а", "у", "а", "ом", "у" };
                del = 0;

                string EndingVoiceless = Surname.Substring(Surname.Length - 2).ToUpper();

                if (EndingVoiceless == "ИХ")
                {
                    AddEnd = new string[] { "", "", "", "", "" };
                    del = 0;
                }
            }
            else if (ConsonantsSonorent.Contains(LastChar))
            {
                Ending = Surname.Substring(Surname.Length - 2).ToUpper();

                if (Ending == "ОВ" || Ending == "ЕВ" || Ending == "ЄВ" || Ending == "ИВ" || Ending == "ИН" || Ending == "ІН" || Ending == "ЇН")
                {
                    AddEnd = new string[] { "а", "у", "а", "им", "у" };
                    del = 0;
                }
                else if (Ending == "АР")
                {
                    AddEnd = new string[] { "а", "ю", "я", "ем", "ю" };
                    del = 0;
                }
                else if (Ending == "ЯР")
                {
                    AddEnd = new string[] { "а", "у", "а", "ем", "у" };
                    del = 0;
                }
                else if (Ending == "ІВ")
                {
                    AddEnd = new string[] { "ова", "ову", "ова", "овим", "ову" };
                    del = 2;
                }
                else if (Ending == "ІР")
                {
                    AddEnd = new string[] { "ора", "ору", "ора", "орем", "ору" };
                    del = 2;
                }
                else if (Ending == "ІЛ")
                {
                    AddEnd = new string[] { "ола", "олу", "ола", "олом", "олі" };
                    del = 2;
                }
                else
                {
                    AddEnd = new string[] { "а", "у", "а", "ом", "е" };
                    del = 0;
                }
            }
            else
            {
                AddEnd = new string[] { "а", "у", "а", "ом", "е" };
                del = 0;
            }

            lngth = Surname.Length - del;

            string SurnameToChange = Surname.Substring(0, lngth);

            string SurnameInGenitive = string.Concat(SurnameToChange, AddEnd[0]);
            string SurnameInDative = string.Concat(SurnameToChange, AddEnd[1]);
            string SurnameInAccusative = string.Concat(SurnameToChange, AddEnd[2]);
            string SurnameInInstrumental = string.Concat(SurnameToChange, AddEnd[3]);
            string SurnameInLocative = string.Concat(SurnameToChange, AddEnd[4]);

            Ending = Surname.Substring(Surname.Length - 2).ToUpper();

            string g = "Г";
            string k = "К";
            string kh = "Х";

            string LetterForChanging = Ending.Substring(0, 1).ToUpper();

            string Suffix = Surname.Substring(Surname.Length - 4).ToUpper().Substring(0, 3);

            if ((Surname.Substring(Surname.Length - 2).ToUpper() != "КО")
                && (Suffix != "СЬК" && Suffix != "ЦЬК")
                && (Suffix != "ЕНК" && Suffix != "ЄНК")
                && (LetterForChanging == g || LetterForChanging == k || LetterForChanging == kh))
            {
                string ChgLet = "";

                if (LetterForChanging == g)
                    ChgLet = "з";
                if (LetterForChanging == k)
                    ChgLet = "ц";
                if (LetterForChanging == kh)
                    ChgLet = "с";

                string EndingDat = SurnameInDative.Substring(SurnameInDative.Length - 1);
                string EndingLoc = SurnameInLocative.Substring(SurnameInLocative.Length - 1);

                SurnameInDative = SurnameInDative.Substring(0, SurnameInDative.Length - 2);
                SurnameInLocative = SurnameInLocative.Substring(0, SurnameInLocative.Length - 2);

                SurnameInDative = string.Concat(SurnameInDative, ChgLet, EndingDat);
                SurnameInLocative = string.Concat(SurnameInLocative, ChgLet, EndingLoc);
            }

            SurnameInCases.Add("Genitive", SurnameInGenitive);
            SurnameInCases.Add("Dative", SurnameInDative);
            SurnameInCases.Add("Accusative", SurnameInAccusative);
            SurnameInCases.Add("Instrumental", SurnameInInstrumental);
            SurnameInCases.Add("Locative", SurnameInLocative);

            return SurnameInCases;
        }

        //Відмінювання прізвище жіночого роду 
        private Dictionary<string, string> SurnameToCases_Female(string Surname)
        {
            Dictionary<string, string> SurnameInCases = new Dictionary<string, string>();

            int lngth = 0;
            int del = 0;

            string[] AddEnd = { "", "", "", "", "" };
            string Ending = "";

            string Consonants = "БВГДЖЗКЛМНПРСТФХЦЧШЩҐ";
            string LastChar = Surname.Substring(Surname.Length - 1).ToUpper();

            if (!Consonants.Contains(LastChar))
            {
                Ending = Surname.Substring(Surname.Length - 1).ToUpper();

                if (Ending == "Я")
                {
                    AddEnd = new string[] { "і", "і", "ю", "ею", "і" };
                    del = 1;
                }
                else if (Ending == "А")
                {
                    AddEnd = new string[] { "и", "і", "у", "ою", "і" };
                    del = 1;

                    string EndingA = Surname.Substring(Surname.Length - 3).ToUpper();

                    if (EndingA == "ОВА" || EndingA == "ЕВА" || EndingA == "ЄВА" || EndingA == "ИНА" || EndingA == "ІНА" || EndingA == "ЇНА" || EndingA.Contains("НА"))
                    {
                        AddEnd = new string[] { "ої", "ій", "у", "ою", "ій" };
                        del = 1;
                    }

                    EndingA = Surname.Substring(Surname.Length - 4).ToUpper();

                    if (EndingA == "СЬКА" || EndingA == "ЦЬКА")
                    {
                        AddEnd = new string[] { "ої", "ій", "у", "ою", "ій" };
                        del = 1;
                    }
                }
                else if (Ending == "Й")
                {
                    AddEnd = new string[] { "ої", "ій", "ої", "ою", "ій" };
                    del = 1;
                }
                else if (Ending == "И")
                {
                    AddEnd = new string[] { "", "", "", "", "" };
                    del = 0;
                }

                Ending = Surname.Substring(Surname.Length - 2).ToUpper();

                if (Ending == "КО" || Ending == "ЬО")
                {
                    AddEnd = new string[] { "", "", "", "", "" };
                    del = 0;
                }

                Ending = Surname.Substring(Surname.Length - 3).ToUpper();

                if (Ending == "АГО" || Ending == "ОВО")
                {
                    AddEnd = new string[] { "", "", "", "", "" };
                    del = 0;
                }
            }

            lngth = Surname.Length - del;

            string SurnameToChange = Surname.Substring(0, lngth);

            string SurnameInGenitive = string.Concat(SurnameToChange, AddEnd[0]);
            string SurnameInDative = string.Concat(SurnameToChange, AddEnd[1]);
            string SurnameInAccusative = string.Concat(SurnameToChange, AddEnd[2]);
            string SurnameInInstrumental = string.Concat(SurnameToChange, AddEnd[3]);
            string SurnameInLocative = string.Concat(SurnameToChange, AddEnd[4]);

            Ending = Surname.Substring(Surname.Length - 2).ToUpper();

            string g = "Г";
            string k = "К";
            string kh = "Х";

            string LetterForChanging = Ending.Substring(0, 1).ToUpper();

            string Suffix = Surname.Substring(Surname.Length - 4).ToUpper().Substring(0, 3);

            if ((Surname.Substring(Surname.Length - 2).ToUpper() != "КО")
                && (Suffix != "СЬК" && Suffix != "ЦЬК")
                && (Suffix != "ЕНК" && Suffix != "ЄНК")
                && (LetterForChanging == g || LetterForChanging == k || LetterForChanging == kh))
            {
                string ChgLet = "";

                if (LetterForChanging == g)
                    ChgLet = "з";
                if (LetterForChanging == k)
                    ChgLet = "ц";
                if (LetterForChanging == kh)
                    ChgLet = "с";

                string EndingDat = SurnameInDative.Substring(SurnameInDative.Length - 1);
                string EndingLoc = SurnameInLocative.Substring(SurnameInLocative.Length - 1);

                SurnameInDative = SurnameInDative.Substring(0, SurnameInDative.Length - 2);
                SurnameInLocative = SurnameInLocative.Substring(0, SurnameInLocative.Length - 2);

                SurnameInDative = string.Concat(SurnameInDative, ChgLet, EndingDat);
                SurnameInLocative = string.Concat(SurnameInLocative, ChgLet, EndingLoc);
            }

            SurnameInCases.Add("Genitive", SurnameInGenitive);
            SurnameInCases.Add("Dative", SurnameInDative);
            SurnameInCases.Add("Accusative", SurnameInAccusative);
            SurnameInCases.Add("Instrumental", SurnameInInstrumental);
            SurnameInCases.Add("Locative", SurnameInLocative);

            return SurnameInCases;
        }

        //Відмінювання По-батькові не залежно від статі 
        private Dictionary<string, string> MiddleNameToCases(string MiddleName)
        {
            Dictionary<string, string> MiddleNameInCases = new Dictionary<string, string>();

            int lngth = 0;
            int del = 0;

            string[] AddEnd = { "", "", "", "", "" };

            string Ending = "";

            Ending = MiddleName.Substring(MiddleName.Length - 2).ToUpper();

            if ("ИЧ" == Ending | "ІЧ" == Ending)
            {
                AddEnd = new string[] { "а", "у", "а", "ем", "у" };
                del = 0;
            }

            Ending = MiddleName.Substring(MiddleName.Length - 3).ToUpper();

            if ("ВНА" == Ending)
            {
                AddEnd = new string[] { "и", "і", "у", "ою", "і" };
                del = 1;
            }

            lngth = MiddleName.Length - del;

            string MiddleNameToChange = MiddleName.Substring(0, lngth);

            MiddleNameInCases.Add("Genitive", string.Concat(MiddleNameToChange, AddEnd[0]));
            MiddleNameInCases.Add("Dative", string.Concat(MiddleNameToChange, AddEnd[1]));
            MiddleNameInCases.Add("Accusative", string.Concat(MiddleNameToChange, AddEnd[2]));
            MiddleNameInCases.Add("Instrumental", string.Concat(MiddleNameToChange, AddEnd[3]));
            MiddleNameInCases.Add("Locative", string.Concat(MiddleNameToChange, AddEnd[4]));

            return MiddleNameInCases;
        }
    }
}
