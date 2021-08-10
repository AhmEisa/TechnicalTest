using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Contracts
{
    public interface IHasher
    {
        string Hash(string plainText);
        bool Verify(string plainText, string hash);
    }
}
