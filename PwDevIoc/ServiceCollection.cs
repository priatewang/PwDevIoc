using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public class ServiceCollection : IServiceCollection
    {
        private Dictionary<string, ServiceDescriptor> _descriptors = new Dictionary<string, ServiceDescriptor>();

        public ServiceDescriptor this[string index]
        {
            get
            {
                if (_descriptors.ContainsKey(index))
                {
                    return _descriptors[index];
                }
                return null;
            }

            set
            {
                _descriptors[index] = value;
            }
        }

        public int Count => _descriptors.Count;

        public bool IsReadOnly => false;

        public void Add(ServiceDescriptor item)
        {
            _descriptors[item.Id] = item;
        }

        public void Clear()
        {
            _descriptors.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.ContainsKey(item.Id);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.Values.GetEnumerator();
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _descriptors.Remove(item.Id);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _descriptors.Values.GetEnumerator();
        }
    }
}
