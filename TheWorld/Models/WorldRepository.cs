﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop stop)
        {
            var trip = GetTripByName(tripName);

            if(trip != null)
            trip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            //logs information regarding the request - addition information will be provided by the system
            _logger.LogInformation("Getting all trips from Database");

            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            Trip trip = _context.Trips.Include(t => t.Stops).Where(x => x.Name == tripName).FirstOrDefault();
            return trip;
        }

        public async Task<bool> SaveChangesAsync()
        {
            //SaveChangesAsync returns an int refering the number of rows affected
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
