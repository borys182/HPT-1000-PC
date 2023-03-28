using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
namespace HPT1000.Source
{
    /// <summary>
    /// Klasa służy do logowania wszystkich bledow/zdarzeń występjacych w programie i zbierania ich w jednym miejscu. Daje to możliwosc latwego zarzadzania komunkatami i erorami jaki ich tekstami
    /// Klasa takze podaje jaki jest ostatnio nie potwierdzony blad oraz jakie wystapilo zdarzenie 
    /// </summary>
    class Logger
    {
        private static List<ItemLogger> logList = new List<ItemLogger>(); ///< Lista wszystkich zgloszonych logow 
        private static object sync = new object();
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Sprawdz czy dany log juz istnieje na liscie - nie dubluj go
         */ 
        private static bool IsContains(ItemLogger aItemLog)
        {
            bool aRes = false;

            lock (sync)
            {
                foreach (ItemLogger itemLog in logList)
                {
                    if (itemLog.Equals(aItemLog))
                    {
                        aRes = true;
                        break;
                    }
                }
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Dodaj log jako blad
        */
        public static void AddError(ItemLogger itemLog)
        {
            if(!IsContains(itemLog))
                logList.Add(itemLog);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Dodaj log jako wyjatek generowany przez aplikacje
        */
        public static void AddException(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Dodaj log jako wiadomosc aplikacji
        */
        public static void AddMsg(string aText, Types.MessageType aTypeMsg)
        {
            ItemLogger itemLog = new ItemLogger();

            itemLog.SetMessage(aText, aTypeMsg);

            if (!IsContains(itemLog))
                logList.Add(itemLog);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Zwroc liste logow zarejestrownych przez aplikacje
        */
        public static List<ItemLogger> GetLogList()
        {
            return logList;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Wyczysc liste logow
        */
        public static void ClearLogger()
        {
            lock (sync)
            {
                logList.Clear();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Funkcja ma za zadanie podanie ostatniej akcji zakonczonej bledem. Kolejne bledy nie beda prezentowane dopuki aktulny nie zostanie potwierdzony
        */
        public static ItemLogger GetLastError()
        {
            ItemLogger itemLogRes = null;
            lock (sync)
            {        
                //Szukaj od konca listy
                for (int i = logList.Count - 1; i >= 0; i--)
                {
                    ItemLogger itemLog = logList[i];
                    //znalazlem wpis z niepotwierdzonym bledem. Ustawiam go jako rezulta funkcji
                    if (itemLog.IsError() && !itemLog.ConfirmError)
                    {
                        itemLogRes = itemLog;
                        break;
                    }
                }
            }
            return itemLogRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie podanie ostatniej akcji bedacej informacja
         */
        public static ItemLogger GetLastInformation()
        {
            ItemLogger itemLogRes = null;
            lock (sync)
            {
                foreach (ItemLogger itemLog in logList)
                {
                    //znalazlem wpis z niepotwierdzonym bledem. Ustawiam go jako rezulta funkcji
                    if (itemLog.IsInformation())
                        itemLogRes = itemLog;
                }
            }
            return itemLogRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------
   }
}
