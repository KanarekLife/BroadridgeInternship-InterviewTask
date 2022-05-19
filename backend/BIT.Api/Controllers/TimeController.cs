using BIT.Api.DTOs;
using BIT.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BIT.Api.Controllers;

[ApiController]
[Route("/time/")]
public class TimeController : ControllerBase
{
    private readonly ITimeService _timeService;

    public TimeController(ITimeService timeService)
    {
        _timeService = timeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentTime()
    {
        var time = await _timeService.GetCurrentTime();
        var hours = time.Hour.ToString().PadLeft(2, '0');
        var minutes = time.Minute.ToString().PadLeft(2, '0');
        var seconds = time.Second.ToString().PadLeft(2, '0');
        return Ok(new CurrentTimeDto($"{hours}:{minutes}:{seconds}"));
    }
}