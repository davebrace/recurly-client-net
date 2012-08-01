﻿using System;
using System.Text;

namespace Recurly
{
    /// <summary>
    /// Base class for exceptions thrown by Recurly's API.
    /// </summary>
    public class RecurlyException : ApplicationException
    {
        internal RecurlyException(RecurlyError[] errors)
        {
            Errors = errors;
        }

        internal RecurlyException(string message)
            : base(message)
        {
        }

        internal RecurlyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal RecurlyException(string message, RecurlyError[] errors)
            : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        /// Error details from Recurly
        /// </summary>
        public RecurlyError[] Errors { get; private set; }
    }
}