using Microsoft.EntityFrameworkCore;
using ResumeApi.Data;
using ResumeApi.Models;

namespace ResumeApi.Repositories;

public class ResumeRepository : IResumeRepository
{
    private readonly AppDbContext _db;
    public ResumeRepository(AppDbContext db) { _db = db; }

    public Task<List<Resume>> ListByUserAsync(string userId) =>
        _db.Resumes.Where(r => r.UserId == userId).OrderByDescending(r => r.UpdatedAt).ToListAsync();

    public Task<List<Resume>> ListAllAsync() =>
        _db.Resumes.OrderByDescending(r => r.UpdatedAt).ToListAsync();

    public Task<Resume?> GetByIdAsync(int id) =>
        _db.Resumes.FirstOrDefaultAsync(x => x.ResumeId == id);

    public async Task<int> CreateAsync(Resume resume)
    {
        _db.Resumes.Add(resume);
        await _db.SaveChangesAsync();
        return resume.ResumeId;
    }

    public async Task UpdateAsync(Resume resume)
    {
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Resume resume)
    {
        _db.Resumes.Remove(resume);
        await _db.SaveChangesAsync();
    }
}


