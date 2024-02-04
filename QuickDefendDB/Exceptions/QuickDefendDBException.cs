using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDefendDB.Exceptions;
public class QuickDefendDBException : Exception
{

    public QuickDefendDBException()
    {
    }

    public QuickDefendDBException(string? message) : base(message)
    {
    }

    public QuickDefendDBException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
