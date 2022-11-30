using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwDevIoc
{
    public interface IServiceCollection : ICollection<ServiceDescriptor>
    {
        ServiceDescriptor this[string index]
        {
            get;
            set;
        }
    }
}
