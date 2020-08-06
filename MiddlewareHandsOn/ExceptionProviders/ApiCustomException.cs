using System;

namespace MiddlewareHandsOn.Api.ExceptionProviders
{
    /// <summary>
    /// Exception customizada utilizada para atribuirmos atributos próprios
    /// para que seja possível padronizar as Exceptions causadas pela aplicação.
    /// </summary>
    public class ApiCustomException : Exception
    {
        public const string XPTO_ERROR = "XPTO_ERROR_CODE";

        public ApiCustomException(
            string message,
            string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; set; }

    }
}
