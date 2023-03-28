using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source
{
    /*
     * Klasa jest odpowiedzialna za dostarczanie tekstu w danym jezyku. Podajemy tekst w jezyky polski zas tekst jezt zwracany w jezyky zadanym.
     * Tlumaczony tekst jest pobierany z bazy danych. Jezeli nie ma jakiegos tekstu przetlumaczonego to dodaje go do listy tekstow nie przetlumacoznych ktora z czasem jest zapisywany w tabeli bazy danych 
    */ 
    public class Translate
    {
        private static Dictionary<string, string>         dictionary_PL_EN    = new Dictionary<string, string>();           ///< Slownik zdan przetlumaczonych 
        private static Dictionary<string, Types.Language> listTextToTranslate = new Dictionary<string, Types.Language>();   ///< Slownik zdan wymaganych do tlumaczenia

        //-----------------------------------------------------------------------
        private void LoadTextFromDB()
        {

        }
        //-----------------------------------------------------------------------
        /**
         * Metoda ma za zadanie konwersje tekstu z jezyka PL na EN. Jezeli danej wrazu nie ma w slowniku to tekst jest zwracany w formie oryginalnej zas brakujaca fraza trafia na liste rzeczy do tlumaczenia  
         */ 
        public static string GetText(string aTxt)
        {
            string aMsg = "";

            bool aRes = dictionary_PL_EN.TryGetValue(aTxt, out aMsg);

            if (aRes == false)
            {
                AddTextToTranslate(aTxt,Driver.HPT1000.LanguageApp);
                aMsg = aTxt;    
            }    
            return aMsg;
        }
        //-----------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dodanie danej frazy do listy tlumaczen
         */ 
        private static void AddTextToTranslate(string txt, Types.Language targetLanguage)
        {
            if(!listTextToTranslate.ContainsKey(txt))
                listTextToTranslate.Add(txt, targetLanguage);
        }
        //-----------------------------------------------------------------------
    }
}
