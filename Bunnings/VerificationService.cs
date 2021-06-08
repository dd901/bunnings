using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bunnings.Interfaces;

namespace Bunnings
{
    public class VerificationService : IVerificationService
    {
        public string Message { get; private set; }

        public bool ValidInputs(List<string> args)
        {
            if (args.Count() != 4)
            {
                Message = "need to supply four arguments";
                return false;
            }

            foreach (var arg in args)
                if (!File.Exists(arg))
                {
                    Message = $"{arg} does not exist";
                    return false;
                }

            return true;
        }
    }
}