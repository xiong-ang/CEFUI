using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefServer.Operations
{
    public abstract class Operation
    {
        public string Name;
        public List<Object> Paras;
        public Object Result;
        public abstract void Execute();
    }
}
