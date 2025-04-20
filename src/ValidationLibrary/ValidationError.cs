namespace ValidationLibrary;

public record ValidationError(IProperty Property, Error Error);