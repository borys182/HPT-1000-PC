using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source
{
    /*
     * Klasa reprezentuje pojedyńcza wiadomość generowaną przez dane zdarzenie:
     * - wystąpienie błędu
     * - wystąpinie warrningu
     * - koniecznoć wyświetlenia informacji
     */
    public class ItemLogger
    {
        /*  Jako ItemLogger moze byc przechowywana informacja na temat akcji wykonajne przez usera , wiadomosci generowanej do niego jak i blad.
         *  BLad moze pochodzic z 3 roznych zrodel: App , PLC , MX Components
         */ 
        private Types.MessageType       msgType         = Types.MessageType.Message;          // domyslnie na Error    
        private Types.EventCategory     eventCategory   = Types.EventCategory.APLICATION;   // okreslenie zrodla pochedzenia zdarzenia. Jest to potrzebne do poprawnego znalezienia textu odpowiadajacego danemu zdarzeniu. PLC/APP/MX Componetens moge posiadac te same kody bledow
        private Types.EventType         eventType       = Types.EventType.NONE;             // typ zdarzenia. Za blad uznajemy dopiero zdarzenie ktore posiada kod bledu rozny od 0
        private int                     errCode         = 0;                                // kod bledu (MX Components oraz PLC podaja co sie dokladnie stalo i posiadaja wlasne lsity bledow)
        private DateTime                time            = DateTime.Now;
        private int                     programID       = 0;
        private int                     subprogramID    = 0;
        private string                  text            = "";

        private bool                    confirmError    = false;                            // flaga okresla czy blad zostal potwierdzony
        private string                  userConfirm     = "";    //User ktroy potwierdzil nam blad
        //-----------------------------------------------------------------------------------------
        public bool ConfirmError
        {
            get { return confirmError; }
        }
        //-----------------------------------------------------------------------------------------
        public string UserConfirm
        {
            get { return userConfirm; }
        }
        //-----------------------------------------------------------------------------------------
        public Types.MessageType TypeLog
        {
            get { return msgType; }
        }
        //-----------------------------------------------------------------------------------------
        public Types.EventCategory EventCategory
        {
            get { return eventCategory; }
        }
        //-----------------------------------------------------------------------------------------
        public Types.EventType EventType
        {
            get { return eventType; }
        }
        //-----------------------------------------------------------------------------------------
        public int ErrCode
        {
            get { return errCode; }
        }
        //-----------------------------------------------------------------------------------------
        public DateTime Time
        {
            get { return time; }
        }
        //-----------------------------------------------------------------------------------------
        public int ProgramID
        {
            get { return programID; }
        }
        //-----------------------------------------------------------------------------------------
        public int SubprogramID
        {
            get { return subprogramID; }
        }
        //-----------------------------------------------------------------------------------------

        public ItemLogger()
        {
        }
        //-----------------------------------------------------------------------------------------
        public ItemLogger(Types.EventType aEventType, Types.EventCategory aEventCategory, int aErrCode)
        {
            eventCategory   = aEventCategory;
            eventType       = aEventType;
            errCode         = aErrCode;
            time            = DateTime.Now;

            if (errCode != 0)
                msgType         = Types.MessageType.Error;
        }
        //-----------------------------------------------------------------------------------------
        public ItemLogger(Types.EventType aEventType, Types.EventCategory aEventCategory, int aErrCode, DateTime aTime)
        {
            eventCategory   = aEventCategory;
            eventType       = aEventType;
            errCode         = aErrCode;
            time            = aTime;

            if (errCode != 0)
                msgType = Types.MessageType.Error;
        }
       //-----------------------------------------------------------------------------------------
       //Ustsaw dane na temat zdarzenia pochodzacego z MX Components
        public void SetErrorMXComponents(Types.EventType aEventType, int aErrCode)
        {
            eventCategory = Types.EventCategory.MX_COMPONENTS;
            eventType     = aEventType;
            errCode       = aErrCode;
            time          = DateTime.Now;

            if (aErrCode == 0)
                msgType  = Types.MessageType.Message;
            else
                msgType = Types.MessageType.Error;

            Logger.AddError(this);
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw informacje na temat wystapienia zdarzenia bledu pochodzacego z aplikacji
        public void SetErrorApp(Types.EventType aEventType)
        {
            eventCategory = Types.EventCategory.APLICATION;
            eventType     = aEventType;
            errCode       = (int)aEventType;
            time          = DateTime.Now;
            msgType       = Types.MessageType.Error;

            if (errCode == 0)
                msgType = Types.MessageType.Message;
            else
                msgType = Types.MessageType.Error;

            Logger.AddError(this);
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw zdarzenie wystapienia bledu pochodzacego z PLC
        public void SetErrorPLC(int aErrCode, DateTime aTime, int aProgramID, int aSubprogramID)
        {
            eventCategory = Types.EventCategory.PLC;
            eventType     = Types.EventType.PLC_ERROR;
            errCode       = aErrCode;
            time          = aTime;
            programID     = aProgramID;
            subprogramID  = aSubprogramID;

            if (errCode == 0)
                msgType = Types.MessageType.Message;
            else
                msgType = Types.MessageType.Error;

            Logger.AddError(this);
        }
        //-----------------------------------------------------------------------------------------
        public void SetMessage(string aTxt,Types.MessageType aType)
        {
            text          = aTxt;
            msgType       = aType;
            eventCategory = Types.EventCategory.MESSAGE;

            /*
            if (aType == Types.MessageType.Message)
                extCode = 0;

            if (aType == Types.MessageType.Error)
                extCode = 1;
            */
        }
        //-----------------------------------------------------------------------------------------
        //Funkcja zwraca kod bledu.
        public int GetErrorCode()
        {
            int aCode = errCode;

            return aCode;
        }
        //-----------------------------------------------------------------------------------------
        //Podaj informacje czy dany wpis jest bledem
        public bool IsError()
        {
            bool aRes = false;

            if (msgType != Types.MessageType.Message)
                aRes = true;
            /*
            if ((eventCategory == Types.EventCategory.MX_COMPONENTS || eventCategory == Types.EventCategory.PLC || eventCategory == Types.EventCategory.MESSAGE ) && errCode != 0)
                aRes = true;

            if (eventCategory == Types.EventCategory.APLICATION  && eventType != 0 )
                aRes = true;
            */
            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        public bool IsInformation()
        {
            bool aRes = false;

            if (msgType == Types.MessageType.Message)
                aRes = true;

       //     if ((eventCategory == Types.EventCategory.MX_COMPONENTS || eventCategory == Types.EventCategory.MESSAGE) && errCode == 0)
       //         aRes = true;

            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        //Funkcja podaje tekt jaki jest przypisany do danego kodu bledu
        public string GetText()
        {
            string aTxt = "";

            if (eventCategory == Types.EventCategory.MESSAGE)
                aTxt = text;
            else
                aTxt = Message.GetText(this);

            return aTxt;
        }
        //-----------------------------------------------------------------------------------------
        public override bool Equals(object other)
        {
            bool aRes = false;
            ItemLogger aOther = (ItemLogger)other;

            //Porownuje tylko po referencji bo w kilku miejscach sie odnosze do tej samej referencji i cos zmieniam.
            if (other != null && this.GetType() == other.GetType() && eventType == aOther.eventType && eventCategory == aOther.eventCategory && errCode == aOther.errCode && time == aOther.Time)
                aRes = true;
     
            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        public void SetConfirmError()
        {
            confirmError = true;
            userConfirm = ApplicationData.LoggedUser.ToString();
        }
        //-----------------------------------------------------------------------------------------
    }
}
