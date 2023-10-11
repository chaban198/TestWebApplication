using System.Reflection;
using System.Runtime.Serialization;

namespace TaskListWebApplication.Models.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException()
    {
    }

    public ConfigurationException(MemberInfo optionsType) : base($"Ошибка конфигурации приложения в секции {optionsType.Name}")
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