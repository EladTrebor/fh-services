﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectory.Core.Queries.Organisations.ListOrganisations;

public class ListOrganisationsCommand : IRequest<List<OrganisationDto>>
{
    public string? Name { get; set; }
    public List<long> Ids { get; set; }
    
    public ListOrganisationsCommand(List<long> ids, string? name)
    {
        Ids = ids;
        Name = name;
    }
}

public class ListOrganisationCommandHandler : IRequestHandler<ListOrganisationsCommand, List<OrganisationDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ListOrganisationCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrganisationDto>> Handle(ListOrganisationsCommand request, CancellationToken cancellationToken)
    {
        var organisationsQuery = _context.Organisations
            .IgnoreAutoIncludes()
            .AsNoTracking();

        if (request.Ids.Any())
        {
            organisationsQuery = organisationsQuery.Where(org => request.Ids.Contains(org.Id) || (org.AssociatedOrganisationId.HasValue && request.Ids.Contains(org.AssociatedOrganisationId.Value)));
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            organisationsQuery = organisationsQuery.Where(org => org.Name.ToLower().Contains(request.Name.ToLower()));
        }

        var organisations = await organisationsQuery
            .ProjectTo<OrganisationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return organisations;
    }
}
