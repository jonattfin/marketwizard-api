using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;

namespace MarketWizard.Data.Repositories;

public class UserRepository(MarketWizardContext context) : GenericRepository<User>(context), IUserRepository
{
    
}