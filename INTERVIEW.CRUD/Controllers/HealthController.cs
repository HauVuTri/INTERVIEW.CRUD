using INTERVIEW.CRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INTERVIEW.CRUD.Controllers;
public class HealthController : ControllerBase
{
    private BankingContext _bankingContext;
    public HealthController(BankingContext bankingContext)
    {
        _bankingContext = bankingContext;
    }
    [Authorize(Roles = "Admin",Policy ="ConsistDBA")]
    [HttpGet("fullhealth")]
    public string FullHealth()
    {
        return $"Banking Context: {_bankingContext.Database.CanConnect()}";

    }
}