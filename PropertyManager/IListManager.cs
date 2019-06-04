using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager
{
    /// <summary>
    /// interface that manages a list. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IListManager<T>
    {
         
       
        int Count { get; }

        bool Add(T aType);

        bool DeleteAt(int anIndex);

        bool ChangeAt(T aType, int anIndex);

        T GetAt(int anIndex);

        bool CheckIndex(int index);
       
        void DeleteAll();

        List<string> ToStringList();
       
        string[] ToStringArray();
       
        bool BinarySerialize(string fileName);
        bool BinaryDeSerialize(string fileName);
        bool XMLSerialize(string fileName);
    }

}
