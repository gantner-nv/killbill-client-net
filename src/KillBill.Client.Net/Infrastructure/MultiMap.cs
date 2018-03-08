using System.Collections.Generic;

namespace KillBill.Client.Net.Infrastructure
{
    public class MultiMap<TV>
    {
        private Dictionary<string, List<TV>> _dictionary = new Dictionary<string, List<TV>>();

        public Dictionary<string, List<TV>> Dictionary => _dictionary;

        public IEnumerable<string> Keys => _dictionary.Keys;

        public List<TV> this[string key]
        {
            get
            {
                List<TV> list;

                if (_dictionary.TryGetValue(key, out list))
                    return list;

                list = new List<TV>();
                _dictionary[key] = list;
                return list;
            }
        }

        public MultiMap<TV> Create(MultiMap<TV> from)
        {
            _dictionary = from.Dictionary;
            return this;
        }

        public void Add(string key, TV value)
        {
            List<TV> list;
            if (_dictionary.TryGetValue(key, out list))
            {
                list.Add(value);
            }
            else
            {
                list = new List<TV> { value };
                _dictionary[key] = list;
            }
        }

        public void RemoveAll(string key)
        {
            _dictionary.Remove(key);
        }

        public void PutAll(MultiMap<TV> queryParams)
        {
            foreach (var key in queryParams.Keys)
            {
                Add(key, queryParams[key]);
            }
        }

        public void PutAll(string key, List<TV> queryParams)
        {
            foreach (var value in queryParams)
            {
                Add(key, value);
            }
        }

        private void Add(string key, List<TV> queryParam)
        {
            queryParam.ForEach(x => Add(key, x));
        }
    }
}