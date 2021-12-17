using System;
using System.Collections.Generic;

namespace SOL.Core.Commands
{
    public class Result
    {
        public readonly IEnumerable<string> Errors;
        public readonly bool Success;
        public readonly object Data;
        
        public Result(bool success, IEnumerable<string> errors, object data = null)
        {
            Success = success;
            Errors = errors;
            Data = data;
        }
    }
}