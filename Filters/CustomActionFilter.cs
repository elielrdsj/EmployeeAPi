using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NovoProjeto.Models;
using System.Text.Json;

namespace NovoProjeto.Filters
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Executado depois da controller");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Executado antes da controller");
            //var resposta = new List<NovoProjetoModel>();
            //if(resposta.Count > 0)
            //{
            //    var model = new NovoProjetoModel();
            //    model.Id = 96;
            //    model.Name = "Short Circuit";
            //    resposta.Add(model);
            //    var shortCircuit = JsonSerializer.Serialize(resposta);
            //    context.Result = new ContentResult
            //    {
            //        Content = shortCircuit
            //    };
            //}
            
        }
    }
}
