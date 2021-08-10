using AutoMapper;
using FluentValidation;
using Inetum.Application.Contracts.Persistence;
using Inetum.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inetum.Application.Features.Teams.Queries
{
    public class TeamListVm
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
    }

    public class GetTeamListQuery : IRequest<List<TeamListVm>>
    {
    }


    public class GetTeamsListQueryHandler : IRequestHandler<GetTeamListQuery, List<TeamListVm>>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public GetTeamsListQueryHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<List<TeamListVm>> Handle(GetTeamListQuery request, CancellationToken cancellationToken)
        {
            var allTeams = (await _teamRepository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<TeamListVm>>(allTeams);
        }
    }

    public class TeamListWithPlayersVm
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
        public List<PlayerListVm> Players { get; set; } = new List<PlayerListVm>();
    }
    public class PlayerListVm
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
    }
    public class GetTeamListWithPlayersQuery : IRequest<List<TeamListWithPlayersVm>>
    {
        public bool IncludePlayers { get; set; }
    }

    public class GetTeamListWithPlayersQueryHandler : IRequestHandler<GetTeamListWithPlayersQuery, List<TeamListWithPlayersVm>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public GetTeamListWithPlayersQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<List<TeamListWithPlayersVm>> Handle(GetTeamListWithPlayersQuery request, CancellationToken cancellationToken)
        {
            var allTeams = await _teamRepository.GetTeamsWithPlayers(request.IncludePlayers);
            return _mapper.Map<List<TeamListWithPlayersVm>>(allTeams);
        }
    }
    public class GetTeamWithPlayersQuery : IRequest<TeamListWithPlayersVm>
    {
        public Guid TeamId { get; set; }
    }
    public class GetTeamWithPlayersQueryHandler : IRequestHandler<GetTeamWithPlayersQuery, TeamListWithPlayersVm>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public GetTeamWithPlayersQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<TeamListWithPlayersVm> Handle(GetTeamWithPlayersQuery request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetTeamWithPlayers(request.TeamId);
            return _mapper.Map<TeamListWithPlayersVm>(team);
        }
    }
    public class CreateTeamDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
    }
    public class CreateTeamCommand : IRequest<CreateTeamCommandResponse>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
        public List<CreatePlayerDto> Players { get; set; }
    }

    public class CreatePlayerDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
    }
    public class CreateTeamCommandResponse
    {
        public CreateTeamDto Team { get; set; }
    }
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamCommandResponse>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public CreateTeamCommandHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<CreateTeamCommandResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var createTeamCommandResponse = new CreateTeamCommandResponse();

            var validator = new CreateTeamCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                //createCategoryCommandResponse.Success = false;
                //createCategoryCommandResponse.ValidationErrors = new List<string>();
                //foreach (var error in validationResult.Errors)
                //{
                //    createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                //}
            }
            if (true)
            {
                Team team = _mapper.Map<Team>(request);
                team = await _teamRepository.AddAsync(team);
                createTeamCommandResponse.Team = _mapper.Map<CreateTeamDto>(team);
            }

            return createTeamCommandResponse;
        }
    }
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.FoundationDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(new DateTime(1900, 01, 01)).WithMessage("{PropertyName} must be existed valid Date.");

            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.CoachName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LogoUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }

    public class UpdateTeamCommand : IRequest
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
    }
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public UpdateTeamCommandHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {

            var teamToUpdate = await _teamRepository.GetByIdAsync(request.TeamId);

            if (teamToUpdate == null)
            {
                // throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdateTeamCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                ;// throw new ValidationException(validationResult);

            _mapper.Map(request, teamToUpdate, typeof(UpdateTeamCommand), typeof(Team));

            await _teamRepository.UpdateAsync(teamToUpdate);

            return Unit.Value;
        }
    }
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.FoundationDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(new DateTime(1900, 01, 01)).WithMessage("{PropertyName} must be existed valid Date.");

            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.CoachName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LogoUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }

    public class DeleteTeamCommand : IRequest<Unit> { public Guid TeamId { get; set; } }
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public DeleteTeamCommandHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var playerToDelete = await _teamRepository.GetByIdAsync(request.TeamId);

            if (playerToDelete == null)
            {
                //throw new NotFoundException(nameof(Event), request.EventId);
            }

            await _teamRepository.DeleteAsync(playerToDelete);

            return Unit.Value;
        }
    }
}
