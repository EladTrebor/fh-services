﻿using AutoMapper;
using FamilyHubs.Referral.Core.Interfaces.Commands;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Referral.Core.Commands.UpdateUserAccount;

public class UpdateUserAccountsCommand : IRequest<bool>, IUpdateUserAccountsCommand
{
    public UpdateUserAccountsCommand(List<UserAccountDto> userAccounts)
    {
        UserAccounts = userAccounts;
    }

    public List<UserAccountDto> UserAccounts { get; }
}

public class UpdateUserAccountsCommandHandler(
    ApplicationDbContext context,
    IMapper mapper,
    ILogger<UpdateUserAccountsCommandHandler> logger)
    : BaseUserAccountHandler(context), IRequestHandler<UpdateUserAccountsCommand, bool>
{
    public async Task<bool> Handle(UpdateUserAccountsCommand request, CancellationToken cancellationToken)
    {
        bool result;
        if (_context.Database.IsSqlServer())
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                result = await UpdateAndUpdateUserAccounts(request, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        else
        {
            result = await UpdateAndUpdateUserAccounts(request, cancellationToken);
        }

        return result;
    }

    private async Task<bool> UpdateAndUpdateUserAccounts(UpdateUserAccountsCommand request, CancellationToken cancellationToken)
    {
        foreach (var account in request.UserAccounts)
        {
            UserAccount entity = mapper.Map<UserAccount>(account);
            ArgumentNullException.ThrowIfNull(entity);

            entity.OrganisationUserAccounts = mapper.Map<List<UserAccountOrganisation>>(account.OrganisationUserAccounts);

            entity = await AttatchExistingUserAccountRoles(entity, cancellationToken);
            entity = await AttatchExistingService(entity, cancellationToken);
            entity = await AttatchExistingOrgansiation(entity, cancellationToken);

            entity.Name = account.Name;
            entity.EmailAddress = account.EmailAddress;
            entity.PhoneNumber = account.PhoneNumber;
            entity.Team = account.Team;


            await _context.SaveChangesAsync(cancellationToken);

            if (entity.Id < 1)
            {
                return false;
            }
        }

        return true;
    }
}



