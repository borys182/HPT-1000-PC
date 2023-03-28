using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source
{
    /**
        Struktura jest odpowiedzialna za reprezentowania wiadomosci bledu/zdarzenia/informacji. W zaleznosci od kodu bledu/zdarzenia/informacji oraz jezyka podaje odpowiednia wiadomosci odczytana z bazy danych. 
        Z uwagi na fakt ze wiadomosci moga pochodzic z roznych zrodel w celu lepszego ich zorganizowania zostaly one podzielone na kategorie pochodzenia EventCategory: 
            EventCategory.Application - bledy zwiazane z uzytkowaniem aplikacji (brak zainstlowanych komponenotw, nie wybrano programu do uruchomnienia itp)
            EventCategory.MXComponets - bledy generowane przez biblioteke MXComponets sluzaca do komunikacji sie z PLC (niepoprawny adres komorki itp)
            EventCategory.PLC - bledy generowane przez PLC podczas uzytkownia apliakcji
            EventCategory.Message - zwykle wiadomosci nie traktowane jako bledy (nie sa zapisane w bazie bo nie dotycza zadnego bledu)
        Dodatkowe pole EventType pozwala na rozpoznanie akcji kotra byla wykonywana. Kazde zdarzenie (ustawienie jakiegos parametru przez usera) ktore moze wystapic posiada swoj kod na podstawie tego czy udalo sie ten parametr zapisac w plc czy nie genereuje odpowiedni tekst 
   */
    public struct MessageError
    {
        public int                  ID ;            //ID wiadomosci odczytane z bazy danych
        public Types.EventCategory  EventCategory;  //Kategoria zdarzenia - okresla pochodzenie danej wiadomosci {}
        public Types.EventType      EventType;      //Typ zdarzenia - okresla jaka akcje user podjal, co za paremrt chcial ustawic
        public int                  ErrCode;        //Kod bledu
        public string               Text;           //Wiadomosc bledu
        public Types.Language       Language;       //Okreslenie jezyka

        //------------------------------------------------------------------------------------------------------
        //Konstruktor sparametryzowany pozwalajacy odrazu utworzyc dany obiekt wiadomosci bledu
        public MessageError(int aCategory, int aType, int aErrCode, string aTxt, Types.Language aLanguage)
        {
            EventCategory   = (Types.EventCategory)aCategory;
            EventType       = (Types.EventType)aType;
            ErrCode         = (int)aType;// aErrCode; 
            Text            = aTxt;
            Language        = aLanguage;
            ID              = 0;
        }
    }
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //Struktura zdarzenia zawiera infomracje na temat akcji jaka zostala wykonana przez usera, ktory parametr zostal ustawiony
    public struct MessageEvent
    {
        public int              ID;
        public int              Code;       //Kategoria zdarzenia
        public string           Text;       //Typ zdarzenia
        public Types.Language   Language;   //Okreslenie jezyka
    }
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    /**
        Klasa jest odpowiedzialna za reprezentowania wiadomosci bledu/zdarzenia/informacji. W zaleznosci od kodu bledu/zdarzenia/informacji jaki jest zapisany w Logu oraz jezyka podaje odpowiednia wiadomosci odczytana z bazy danych. 
        Z uwagi na fakt ze wiadomosci moga pochodzic z roznych zrodel w celu lepszego ich zorganizowania zostaly one podzielone na kategorie pochodzenia EventCategory: 
            EventCategory.Application - bledy zwiazane z uzytkowaniem aplikacji (brak zainstlowanych komponenotw, nie wybrano programu do uruchomnienia itp)
            EventCategory.MXComponets - bledy generowane przez biblioteke MXComponets sluzaca do komunikacji sie z PLC (niepoprawny adres komorki itp)
            EventCategory.PLC - bledy generowane przez PLC podczas uzytkownia apliakcji
            EventCategory.Message - zwykle wiadomosci nie traktowane jako bledy (nie sa zapisane w bazie bo nie dotycza zadnego bledu)
        Dodatkowe pole EventType pozwala na rozpoznanie akcji kotra byla wykonywana. Kazde zdarzenie (ustawienie jakiegos parametru przez usera) ktore moze wystapic posiada swoj kod na podstawie tego czy udalo sie ten parametr zapisac w plc czy nie genereuje odpowiedni tekst 
   */
    public class Message
    {
        private static List<MessageError> messageErrors = new List<MessageError>();     ///< Lista wiadomosci bledow odczytana z bazy danych
        private static List<MessageEvent> messageEvents = new List<MessageEvent>();     ///< Lista wiadomosci zdarzen odczytana z bazy danych

        private DB dataBase = null;

        //------------------------------------------------------------------------------------------------------
        public DB DataBase
        {
            set { dataBase = value; }
        }
        //------------------------------------------------------------------------------------------------------
        /**
         * Metoda jest odpowiedzialna za odczytanie wiadomosci bledow oraz zdarzen z bazy danych. Na jej podstawie beda w aplikacji pojawialy sie komunikaty przy roznych okazajch
         */ 
        public void LoadErrorMessages()
        {
            if(dataBase != null)
            {
                //Odczytaj liste wiadomosci bledow z bazy danych
                messageErrors = dataBase.GetMessageErrors();
                //Odczytaj liste wiadomosci dla zdarzen (akcji usera ktore moga zostac wykoane w apliakcji - glownie ustawienie danego parametru) z bazy daych
                messageEvents = dataBase.GetMessageEvents();
            }
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie zwrocenie tekst dla danego obiektu logera w aktualnie wybranym jezyku. Loger zawiera informacje na temat kodu bledu,kategori oraz typu zdarzenia 
        */
        public static string GetText(ItemLogger aLog)
        {
            //Ustaw domyslna wartos tekstu jaka zostanie zwrocona w sytuacji gdy nie zostanie odnaleziony dany kod
            string aTxt = "No text in data base for error: Event type: " + aLog.EventType + " Event category: " + aLog.EventCategory + " Error code: " + aLog.GetErrorCode().ToString("X8");

            int aErrCode = aLog.ErrCode;
            string aExtText = ""; //Dodatkowe info na temat bledu. Jest tutaj zawarty kod bledu MX Components
            //Sprawdz czy nie jestem kategorai MXComponts. Jezeli tak to podaj mi dodatkowe informacje na temat bledu. MX Componts zawiera wlasny kod bledu jako Event poniewaz w polu bledu posiada informacje o tym ze wystapil blad biblitoeki MX Componts
            if (aLog.EventCategory == Types.EventCategory.MX_COMPONENTS)
            {
                aErrCode = (int)aLog.EventType;
                aExtText = " MX Componets reports error: " + aLog.ErrCode.ToString("X8");
            }
            //Sprawdz czy loger odnosi sie do zdarzenia czy bledu a nastepnie na podstawie kodu i jezyka zwroc odpowiednia wiadomosc
            foreach (MessageError error in messageErrors)
            {
                if (error.EventType == aLog.EventType && error.EventCategory == aLog.EventCategory && error.Language == Driver.HPT1000.LanguageApp)
                {
                    if (aErrCode == error.ErrCode)
                    {
                        //Jezeli error code jest rozny od 0 to znaczy ze wystapil blad w przeciwnym razie wystapilo zdarzenie ustawienia danego parametru
                        if (aLog.ErrCode != 0)
                            aTxt = error.Text + aExtText; // wystapil blad                       
                        else
                            aTxt = GetMessageEvent((int)aLog.EventType); // wystapila akcja poszukaj text dla niej
                    }
                }
            }
            return aTxt;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zwrocenie wiadomosci dla danego zdarzenia. Klasa zawiera dwie listy wiadomosci : bledow oraz zdarzen (akcji usera)
         */
        private static string GetMessageEvent(int aCode)
        {
            string aTxt = "";
            foreach(MessageEvent msgEvent in messageEvents)
            {
                if (msgEvent.Code == aCode)
                    aTxt = msgEvent.Text;
            }
            return aTxt;
        }
        //-------------------------------------------------------------------------------------

    }
}
