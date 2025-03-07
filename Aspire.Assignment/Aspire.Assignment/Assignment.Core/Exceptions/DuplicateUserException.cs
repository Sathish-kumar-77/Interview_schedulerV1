using System;

namespace Assignment.Core.Exceptions;

public class DuplicateUserException : Exception
    {
        public DuplicateUserException() : base("User with the same name and same Time already exists.")
        {
        }

        public DuplicateUserException(string message) : base(message)
        {
        }
    }
