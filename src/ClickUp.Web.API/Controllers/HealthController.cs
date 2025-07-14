using Microsoft.AspNetCore.Mvc;
using ClickUp.Core.Interfaces;

namespace ClickUp.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IClickUpService _clickUpService;

    public HealthController(IClickUpService clickUpService)
    {
        _clickUpService = clickUpService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var health = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Service = "ClickUp Integration API",
            Version = "1.0.0"
        };

        return Ok(health);
    }

    [HttpGet("clickup")]
    public async Task<IActionResult> CheckClickUpConnection()
    {
        try
        {
            var teams = await _clickUpService.GetTeamsAsync();
            
            return Ok(new
            {
                Status = "Connected",
                TeamsFound = teams.Count,
                Timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Status = "Failed",
                Error = ex.Message,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}