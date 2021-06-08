using System.Collections.Generic;

namespace Bunnings.Interfaces
{
    public interface IVerificationService
    {
        string Message { get; }
        bool ValidInputs(List<string> args);
    }
}