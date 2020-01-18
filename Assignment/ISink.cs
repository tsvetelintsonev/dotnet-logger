using System;
using System.Threading.Tasks;

namespace Assignment
{
    public interface ISink
    {
        void Write(string line);
    }
}