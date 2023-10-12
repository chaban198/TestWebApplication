using System.Reflection;
using System.Runtime.Serialization;

namespace GlobalDomain.Models.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException()
    {
    }

    public ConfigurationException(MemberInfo optionsType, string? message = null)
        : base($"Ошибка конфигурации приложения в секции {optionsType.Name}." + message)
    {
    }

    public ConfigurationException(string? message) : base(message)
    {
    }

    public ConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}