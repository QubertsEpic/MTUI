using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Exceptions
{
    public class FrameMissingException : Exception
    {
        public FrameMissingException()
        {

        }

        public FrameMissingException(string message) : base(message)
        {

        }

        public FrameMissingException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
