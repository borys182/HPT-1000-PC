using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HPT1000.Source
{
    /// <summary>
    /// Klas sluzy do zapisu danych do pliku poprzez serializacje 
    /// </summary>
    [Serializable]
    public class DataFile
    {
        static private string                   filePath = "d:\\ConfigHPT1000.dat";
        static private Dictionary<string, int>  listID   = new Dictionary<string, int>();

        static DataFile objectToSave = new DataFile();

        //----------------------------------------------------------------------------------------------------------------------------------
        public static void SetID(string name,int aId)
        {
            listID.Add(name,aId);
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        public static int GetID(string name)
        {
            int aId = 0;

                if (!listID.TryGetValue(name, out aId))
                    aId = 0;

            return aId;
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //Funkcja poprzez serializacje danych zapisuje dane do pliku binarnego
        public static int SaveFile()
        {
            FileStream file = null;
            int aRes = 0;
            try
            {
                file = new FileStream(filePath, FileMode.Create);
                BinaryFormatter binary = new BinaryFormatter(); //obiekt serializacji
                binary.Serialize(file, objectToSave);                // serialziacja
                file.Close();
            }
            catch(Exception ex)
            {
                aRes = 1;
                Logger.AddException(ex);
            }
            finally{
                if (file != null)
                    file.Close();                 
            }
            return aRes;
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        public static int LoadFile()
        {
            int aRes = 0;
            FileStream file = null;
            try
            {
                BinaryFormatter binary = new BinaryFormatter(); //obiekt serializacji
                file = new FileStream(filePath, FileMode.Open);
                objectToSave = (DataFile)binary.Deserialize(file);              //deserialziacja
                file.Close();
            }
            catch (Exception ex)
            {
                aRes = 1;
                Logger.AddException(ex);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
            return aRes;
        }
    }
}
