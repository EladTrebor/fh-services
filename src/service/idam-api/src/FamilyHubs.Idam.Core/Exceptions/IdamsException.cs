﻿using System.Runtime.Serialization;

namespace FamilyHubs.Idam.Core.Exceptions;

[Serializable]
public class IdamsException : Exception
{
    public string Title { get; set; } = "Server Error";
    public string ErrorCode { get; set; } = ExceptionCodes.UnhandledException;
    public int HttpStatusCode { get; set; } = 500;
        
    public IdamsException(string message) :base(message)
    {

    }

    protected IdamsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

