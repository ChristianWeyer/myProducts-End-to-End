using System.Collections.Generic;
using System.Dynamic;

namespace Thinktecture.Applications.Framework
{
    public static class DictionaryExtensions
    {
        public static dynamic ToDynamic(this object dictionary)
        {
            return new DynamicDictionary(dictionary);
        }
    }

    public class DynamicDictionary : DynamicObject
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public DynamicDictionary()
        {
        }

        public DynamicDictionary(Dictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }

        public DynamicDictionary(object dictionary)
        {
            this.dictionary = dictionary as Dictionary<string, object>;
        }

        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dictionary.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            dictionary[binder.Name] = value;

            return true;
        }
    }
}
