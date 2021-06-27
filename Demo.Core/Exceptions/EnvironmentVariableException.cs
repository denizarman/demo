using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Exceptions
{
    public class EnvironmentVariableException : Exception
    {
        public EnvironmentVariableException(string environmentVariable) : 
            base(string.Format("{0} environment variable not found !!!", environmentVariable))
        {
            Console.WriteLine(this.Message);
        }
    }
}
