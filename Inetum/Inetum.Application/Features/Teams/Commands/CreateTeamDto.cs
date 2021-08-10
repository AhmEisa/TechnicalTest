using AutoMapper;
using FluentValidation;
using Inetum.Application.Contracts.Persistence;
using Inetum.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inetum.Application.Features.Teams.Commands
{
    public class CreateTeamDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
    }

    public class CreateTeamCommandResponse
    {
        public CreateTeamDto Team { get; set; }
    }
    public class CreateTeamCommand : IRequest<CreateTeamCommandResponse>
    {
        public string Name { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamCommandResponse>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
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
                //createTeamCommandResponse.Success = false;
                //createTeamCommandResponse.ValidationErrors = new List<string>();
                //foreach (var error in validationResult.Errors)
                //{
                //    createTeamCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                //}
            }
            bool isSuccess = true;//createTeamCommandResponse.Success
            if (isSuccess)
            {
                var team = new Team() { Name = request.Name };
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
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");
        }
    }

    /* update*/
    public class UpdateTeamCommand : IRequest
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class UpdateEventCommandHandler : IRequestHandler<UpdateTeamCommand>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
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

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }

    /* delete */
    public class DeleteTeamCommand : IRequest
    {
        public Guid TeamId { get; set; }
    }
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
            var teamToDelete = await _teamRepository.GetByIdAsync(request.TeamId);

            if (teamToDelete == null)
            {
                //throw new NotFoundException(nameof(Event), request.EventId);
            }

            await _teamRepository.DeleteAsync(teamToDelete);

            return Unit.Value;
        }
    }
}
