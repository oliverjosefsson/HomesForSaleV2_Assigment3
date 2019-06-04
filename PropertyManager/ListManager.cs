using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer;


namespace PropertyManager
{
    /// <summary>
    ///Listmanager that manages a list with generic type T,
    ///with methods to handle the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ListManager<T> : IListManager<T>
    {
        private List<T> list;

        public ListManager()
        {
            list = new List<T>();
        }
       
        public int Count => list.Count;

        /// <summary>
        /// adds an object to the list.
        /// </summary>
        /// <param name="aType"></param>
        /// <returns></returns>
        public bool Add(T aType)
        {
            try
            {
                list.Add(aType);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// adds objects to the list from a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool BinaryDeSerialize(string fileName)
        {
            list = Serialize.BinaryFileDeSerialize<List<T>>(fileName);
            return true;
        }

        /// <summary>
        /// saves the list to a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool BinarySerialize(string fileName)
        {
            Serialize.BinaryFileSerialize(list, fileName);
            return true;
        }

        /// <summary>
        ///  replaces object in list with new object given in parameter.
        /// </summary>
        /// <param name="aType"></param>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public bool ChangeAt(T aType, int anIndex)
        {
            try
            {
                list.RemoveAt(anIndex);
                list.Insert(anIndex, aType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool CheckIndex(int index)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// clears the list.
        /// </summary>
        public void DeleteAll()
        {
            list.Clear();
        }

        /// <summary>
        /// deletes type in given position
        /// </summary>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public bool DeleteAt(int anIndex)
        {
            try
            {
                list.RemoveAt(anIndex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// returns index of given position
        /// </summary>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public T GetAt(int anIndex)
        {
            try
            {
                return list[anIndex];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list[anIndex];
        }

        /// <summary>
        /// returns an array with all objects.
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            string[] array = new string[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = list[i].ToString();
            }
            return array;
        }
        /// <summary>
        /// returns a list with all the objects.
        /// </summary>
        /// <returns></returns>
        public List<string> ToStringList()
        {
            List<string> stringList = new List<string>();
            for (int i = 0; i < Count; i++)
            {
                stringList.Add(list[i].ToString());
            }
            return stringList;
        }

        public bool XMLSerialize(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
