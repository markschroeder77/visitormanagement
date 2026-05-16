// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Designations.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Designations.Queries.GetAll;

    public class GetAllDesignationsQuery : IRequest<IEnumerable<DesignationDto>>
    {
    }
    
    public class GetAllDesignationsQueryHandler :
         IRequestHandler<GetAllDesignationsQuery, IEnumerable<DesignationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllDesignationsQueryHandler> _localizer;

        public GetAllDesignationsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllDesignationsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<DesignationDto>> Handle(GetAllDesignationsQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Designations.OrderBy(x=>x.Name)
                         .ProjectTo<DesignationDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


