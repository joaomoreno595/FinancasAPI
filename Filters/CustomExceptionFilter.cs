using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Financeiro.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FluentValidation.ValidationException validationException)
            {
                var errors = validationException.Errors
                    .Select(error => $"{error.PropertyName}:{error.ErrorMessage}")
                    .ToList();

                var result = new ObjectResult(new
                {
                    StatusCode = 400 //Bad Request
                });
                context.Result = result;
                context.ExceptionHandled = true;
            }
            else if (context.Exception is ArgumentNullException || context.Exception is KeyNotFoundException)
            {
                //Tratar erro 404 -- Not Found
                context.Result = new NotFoundObjectResult(new { Erro = "Recurso não encontrado!" });
                context.ExceptionHandled = true;
            }
            else if (context.Exception is HttpRequestException || context.Exception is InvalidOperationException)
            {
                //Tratar o erro 500 -- Internal Server Error
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                context.ExceptionHandled = true;
            }

        }
    }
}
