using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Infrastructure.UnitOfWork;

namespace WanderlustService.Facade
{
    public class FacadeBase       
    {
        /// <summary>
        /// Mapper used for the conversion of entities to DTOs and vice versa
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Context for creating units of work
        /// </summary>
        protected IUnitOfWorkContext unitOfWorkContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWorkContext">Context for creating units of work</param>
        /// <param name="mapper">Mapper used for the conversion of entities to DTOs and vice versa</param>
        public FacadeBase(IUnitOfWorkContext unitOfWorkContext, IMapper mapper)
        {
            this.unitOfWorkContext = unitOfWorkContext;
            this.mapper = mapper;
        }
    }
}
