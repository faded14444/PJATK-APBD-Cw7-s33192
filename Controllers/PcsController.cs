using Microsoft.AspNetCore.Mvc;
using ZadanieDomowe7.DTOs;
using ZadanieDomowe7.Services;

namespace ZadanieDomowe7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcsController : ControllerBase
{
    private readonly IPCService _pcService;

    public PcsController(IPCService pcService)
    {
        _pcService = pcService;
    }

    /// <summary>
    /// Get all PCs
    /// </summary>
    /// <returns>List of all PCs with basic information</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PCResponseDto>>> GetAllPCs()
    {
        var pcs = await _pcService.GetAllPCsAsync();
        return Ok(pcs);
    }

    /// <summary>
    /// Get PC with components by id
    /// </summary>
    /// <param name="id">PC id</param>
    /// <returns>PC with list of components</returns>
    [HttpGet("{id}/components")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PCWithComponentsDto>> GetPCWithComponents(int id)
    {
        var pc = await _pcService.GetPCWithComponentsAsync(id);
        if (pc == null)
            return NotFound($"PC with id {id} not found.");

        return Ok(pc);
    }

    /// <summary>
    /// Create new PC
    /// </summary>
    /// <param name="createPCDto">PC data</param>
    /// <returns>Created PC</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PCResponseDto>> CreatePC([FromBody] CreatePCDto createPCDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pc = await _pcService.CreatePCAsync(createPCDto);
        return CreatedAtAction(nameof(GetPCWithComponents), new { id = pc.Id }, pc);
    }

    /// <summary>
    /// Update existing PC
    /// </summary>
    /// <param name="id">PC id</param>
    /// <param name="updatePCDto">PC data to update</param>
    /// <returns>Updated PC</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PCResponseDto>> UpdatePC(int id, [FromBody] UpdatePCDto updatePCDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pc = await _pcService.UpdatePCAsync(id, updatePCDto);
        if (pc == null)
            return NotFound($"PC with id {id} not found.");

        return Ok(pc);
    }

    /// <summary>
    /// Delete PC by id
    /// </summary>
    /// <param name="id">PC id</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePC(int id)
    {
        var deleted = await _pcService.DeletePCAsync(id);
        if (!deleted)
            return NotFound($"PC with id {id} not found.");

        return NoContent();
    }
}

