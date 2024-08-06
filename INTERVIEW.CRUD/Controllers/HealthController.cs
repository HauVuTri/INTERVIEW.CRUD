using INTERVIEW.CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTERVIEW.CRUD.Controllers;
public class HealthController : Controller
{
    private BankingContext _bankingContext;
    public HealthController(BankingContext bankingContext)
    {
        _bankingContext = bankingContext;
    }
    [HttpGet("fullhealth")]
    public string FullHealth()
    {
        return $"Banking Context: {_bankingContext.Database.CanConnect()}";

    }
}