using Microsoft.EntityFrameworkCore.Design;

namespace PhotostudioDLL;

/// <summary>
///     Класс для работы миграции
/// </summary>
public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        return new ApplicationContext(ApplicationContext.GetDb());
    }
}