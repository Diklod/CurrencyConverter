using CurrencyConversion.Database;
using CurrencyConversion.Global;
using CurrencyConversion.Model;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConversion.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("HandShake")]
        public string HandShake()
        {
            return "OK";
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult Add(User user)
        {
            UserDb userDb = new UserDb();
            Security security = new Security();

            user.Email = security.EncryptTripleDES(user.Email);
            user.Password = security.EncryptTripleDES(user.Password);

            if (userDb.EmailExists(user.Email))
            {
                return new JsonResult(new { success = false, data = "O e-mail informado já está cadastrado." });
            }

            bool response = userDb.Add(user);

            if (response)
            {
                return new JsonResult(new { success = true, data = "Cadastrado" });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }
        [HttpPost]
        [Route("GetToken")]
        public JsonResult GetToken(string email, string password)
        {
            UserDb userDb = new UserDb();
            Security security = new Security();

            email = security.EncryptTripleDES(email);
            password = security.EncryptTripleDES(password);

            bool response = userDb.GenerateToken(email, password);
            if (response)
            {
                return new JsonResult(new { success = true, data = "Token gerado com sucesso." });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Erro" });
            }
        }

        [HttpPost]
        [Route("ConvertCurrency")]
        public JsonResult ConvertCurrency(string token, double real)
        {
            UserDb userDb = new UserDb();
            if (userDb.ValidateToken(token))
            {
                Currency convertedcurrencies = userDb.ConvertCurrency(real);

                return new JsonResult(new { success = true, data = convertedcurrencies });
            }

            else
            {
                return new JsonResult(new { success = false, data = "Token inválido!" });
            }
        }
    }
}
