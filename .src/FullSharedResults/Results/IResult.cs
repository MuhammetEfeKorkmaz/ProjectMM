﻿namespace FullSharedResults.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
