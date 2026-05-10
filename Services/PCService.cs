using Microsoft.EntityFrameworkCore;
using ZadanieDomowe7.Data;
using ZadanieDomowe7.DTOs;
using ZadanieDomowe7.Models;

namespace ZadanieDomowe7.Services;

public interface IPCService
{
    Task<IEnumerable<PCResponseDto>> GetAllPCsAsync();
    Task<PCWithComponentsDto?> GetPCWithComponentsAsync(int id);
    Task<PCResponseDto> CreatePCAsync(CreatePCDto createPCDto);
    Task<PCResponseDto?> UpdatePCAsync(int id, UpdatePCDto updatePCDto);
    Task<bool> DeletePCAsync(int id);
}

public class PCService : IPCService
{
    private readonly AppDbContext _context;

    public PCService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PCResponseDto>> GetAllPCsAsync()
    {
        return await _context.PCs
            .Select(p => new PCResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<PCWithComponentsDto?> GetPCWithComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            return null;

        return new PCWithComponentsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents
                .Select(pc => new ComponentDto
                {
                    Code = pc.Component.Code,
                    Name = pc.Component.Name,
                    Description = pc.Component.Description,
                    Amount = pc.Amount
                })
                .ToList()
        };
    }

    public async Task<PCResponseDto> CreatePCAsync(CreatePCDto createPCDto)
    {
        var pc = new PC
        {
            Name = createPCDto.Name,
            Weight = createPCDto.Weight,
            Warranty = createPCDto.Warranty,
            CreatedAt = createPCDto.CreatedAt,
            Stock = createPCDto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PCResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<PCResponseDto?> UpdatePCAsync(int id, UpdatePCDto updatePCDto)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null)
            return null;

        pc.Name = updatePCDto.Name;
        pc.Weight = updatePCDto.Weight;
        pc.Warranty = updatePCDto.Warranty;
        pc.CreatedAt = updatePCDto.CreatedAt;
        pc.Stock = updatePCDto.Stock;

        _context.PCs.Update(pc);
        await _context.SaveChangesAsync();

        return new PCResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> DeletePCAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null)
            return false;

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return true;
    }
}

