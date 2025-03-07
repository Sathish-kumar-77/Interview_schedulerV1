using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands;

public class AllocateDateCommand : IRequest<string>
    {
        public AllocateDateDTO Model { get; }

        public AllocateDateCommand(AllocateDateDTO model)
        {
            this.Model = model;
        }
    }

    public class AllocateDateCommandHandler : IRequestHandler<AllocateDateCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AllocateDateCommandHandler(
            IUnitOfWork unitOfWork
            
        )
        {
            _unitOfWork = unitOfWork;
            
        } 

        public async Task<string> Handle(
            AllocateDateCommand request,
            CancellationToken cancellationToken
        )
        {
            AllocateDateDTO model = request.Model;

            // Validate the model
            

            // Check for overlapping dates
            
                var slot = new AllocateDate
                {
                     PanelMemberID= model.PanelMemberID,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                await _unitOfWork.PanelCoordinator.AddAllocationAsync(slot);
                await _unitOfWork.CommitAsync();
                return "Allocation added successfully";
            
        }
    }

