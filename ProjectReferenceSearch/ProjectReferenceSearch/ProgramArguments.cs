using System;

namespace ProjectReferenceSearch
{
    public class ProgramArguments
    {
        public string SearchPath { get; }

        public ProgramArguments(string[] args)
        {
            if(args == null || args.Length < 1)
                throw new ApplicationException("Missing arguments");

            SearchPath = args[0];
        }
    }
}