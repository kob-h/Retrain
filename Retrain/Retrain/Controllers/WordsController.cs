using Microsoft.AspNetCore.Mvc;
using Retrain.BusinessService;
using Retrain.DataContracts;

namespace Retrain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly IWordsService _wordsService;
    private readonly ILogger<WordsController> _logger;

    public WordsController(
        [FromServices] IWordsService wordsService,
        ILogger<WordsController> logger)
    {
        _wordsService = wordsService;
        _logger = logger;
    }

    [HttpGet("statistics/{wordStr}")]
    public async Task<IActionResult> Statistics(string wordStr)
    {
        var count = await _wordsService.GetStatistics(wordStr);
        return Ok(count);
    }

    [HttpPost("counter")]
    public async Task<IActionResult> Counter([FromBody] CountRequest wordsRequest)
    {
        await _wordsService.Count(wordsRequest);
        return NoContent();
    }
}

