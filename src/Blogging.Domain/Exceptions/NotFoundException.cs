﻿namespace Blogging.Domain.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string resourceType, string resourceIdentifier) 
                            :base($"{resourceType} with id: {resourceIdentifier} doesn't exist")
    {
        
    }
}
