namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceId) 
    : Exception($"{resourceType} with Id={resourceId} was not found.") { }