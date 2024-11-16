using domain.dto;
using domain.entities;
using infrastructure.contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilities.constants;

namespace infrastructure.repositories
{
    public class ParkingSlotRepository : Repository<ParkingSlot>, IParkingSlotRepository
    {
        private readonly DataContext _context;

        public ParkingSlotRepository(DataContext context):base(context)
        {
            _context = context;
        }

      
    }
}
