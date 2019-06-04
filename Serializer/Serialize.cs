using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Serializer
{
    public class Serialize
    {

       /// <summary>
       /// takes an object and filepath to save the object to the filepath
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="filePath"></param>
        public static void BinaryFileSerialize(Object obj, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, obj);
                
            }
        }
        /// <summary>
        /// takes a filepath and returns the object from that filepath.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T BinaryFileDeSerialize<T>(string filePath)
        {
            Object obj = null;
            
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                    BinaryFormatter bin = new BinaryFormatter();
                    obj = bin.Deserialize(stream);
            }
            return (T)obj;
        }
    }
}
