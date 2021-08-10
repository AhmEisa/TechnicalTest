using AutoMapper;
using FluentValidation;
using Inetum.Application.Contracts.Persistence;
using Inetum.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inetum.Application.Features.Players.Commands
{
    public class AddPlayerCommand : IRequest<AddPlayerCommandResponse>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
        public Guid TeamId { get; set; }
    }
    public class AddPlayerCommandResponse
    {
        public AddPlayerDto Player { get; set; }
    }
    public class AddPlayerDto
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
    }
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, AddPlayerCommandResponse>
    {
        private readonly IAsyncRepository<Player> _playerRepository;
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public AddPlayerCommandHandler(IMapper mapper, IAsyncRepository<Player> playerRepository, IAsyncRepository<Team> teamRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        public async Task<AddPlayerCommandResponse> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            var response = new AddPlayerCommandResponse();

            var teamOfplayer = await _teamRepository.GetByIdAsync(request.TeamId);

            if (teamOfplayer == null)
            {
                return response;
                // throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new AddPlayerCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                return response;// throw new ValidationException(validationResult);

            var playerToAdd = _mapper.Map<Player>(request);

            await _playerRepository.AddAsync(playerToAdd);
            response.Player = _mapper.Map<AddPlayerDto>(playerToAdd);

            return response;
        }
    }
    public class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerCommandValidator()
        {
            RuleFor(p => p.TeamId)
              .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(new DateTime(1970, 01, 01)).WithMessage("{PropertyName} must be valid date.");

            RuleFor(p => p.Nationality)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }

    /* update*/
    public class UpdatePlayerCommand : IRequest
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
    }
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IAsyncRepository<Player> _playerRepository;
        private readonly IMapper _mapper;

        public UpdatePlayerCommandHandler(IMapper mapper, IAsyncRepository<Player> playerRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {

            var playerToUpdate = await _playerRepository.GetByIdAsync(request.PlayerId);

            if (playerToUpdate == null)
            {
                // throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdatePlayerCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                ;// throw new ValidationException(validationResult);

            _mapper.Map(request, playerToUpdate, typeof(UpdatePlayerCommand), typeof(Player));

            await _playerRepository.UpdateAsync(playerToUpdate);

            return Unit.Value;
        }
    }
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(new DateTime(1970, 01, 01)).WithMessage("{PropertyName} must be valid date.");

            RuleFor(p => p.Nationality)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }

    /* delete */
    public class DeletePlayerCommand : IRequest
    {
        public Guid PlayerId { get; set; }
    }
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
    {
        private readonly IAsyncRepository<Player> _playerRepository;
        private readonly IMapper _mapper;

        public DeletePlayerCommandHandler(IMapper mapper, IAsyncRepository<Player> playerRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerToDelete = await _playerRepository.GetByIdAsync(request.PlayerId);

            if (playerToDelete == null)
            {
                //throw new NotFoundException(nameof(Event), request.EventId);
            }

            await _playerRepository.DeleteAsync(playerToDelete);

            return Unit.Value;
        }
    }
}
