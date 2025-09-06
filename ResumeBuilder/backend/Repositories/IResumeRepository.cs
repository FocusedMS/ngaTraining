using ResumeApi.Models;

namespace ResumeApi.Repositories;

public interface IResumeRepository
{
    Task<List<Resume>> ListByUserAsync(string userId);
    Task<List<Resume>> ListAllAsync();
    Task<Resume?> GetByIdAsync(int id);
    Task<int> CreateAsync(Resume resume);
    Task UpdateAsync(Resume resume);
    Task DeleteAsync(Resume resume);
}


