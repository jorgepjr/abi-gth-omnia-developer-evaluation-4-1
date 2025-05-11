using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Integration;

public class InMemoryDataBase : IDisposable
{
    public DefaultContext Context { get; set; }

    public InMemoryDataBase()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase("BaseTest")
            .Options;
        
        Context = new DefaultContext(options);
    }
    
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}