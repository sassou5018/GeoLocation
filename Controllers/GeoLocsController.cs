using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoLocation.Data;
using GeoLocation.Models;
using System.Xml;

namespace GeoLocation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocsController : ControllerBase
    {
        private readonly GeoLocationContext _context;

        public GeoLocsController(GeoLocationContext context)
        {
            _context = context;
        }

        // GET: api/GeoLocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoLoc>>> GetGeoLoc()
        {
            return await _context.GeoLoc.ToListAsync();
        }

        // GET: api/GeoLocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoLoc>> GetGeoLoc(int id)
        {
            var geoLoc = await _context.GeoLoc.FindAsync(id);

            if (geoLoc == null)
            {
                return NotFound();
            }

            return geoLoc;
        }

        // PUT: api/GeoLocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeoLoc(int id, GeoLoc geoLoc)
        {
            if (id != geoLoc.Id)
            {
                return BadRequest();
            }

            _context.Entry(geoLoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeoLocExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GeoLocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeoLoc>> PostGeoLoc(GeoLoc geoLoc)
        {
            _context.GeoLoc.Add(geoLoc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeoLoc", new { id = geoLoc.Id }, geoLoc);
        }

        // DELETE: api/GeoLocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeoLoc(int id)
        {
            var geoLoc = await _context.GeoLoc.FindAsync(id);
            if (geoLoc == null)
            {
                return NotFound();
            }

            _context.GeoLoc.Remove(geoLoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeoLocExists(int id)
        {
            return _context.GeoLoc.Any(e => e.Id == id);
        }
        [HttpGet("{x}/{y}")]

        // add e goe eli fl houma
        public ActionResult<IEnumerable<GeoLoc>> GetNearGeos(decimal x, decimal y)
        {
            //get near geolocation to a point <x,y> in google maps
            var geos = _context.GeoLoc.ToList();
            List<GeoLoc> nearGeaos = new List<GeoLoc>();
            foreach ( var geo in geos)
            {
                if(getDistanceFromLatLonInKm((double)x, (double)y, (double)geo.Latitude, (double)geo.Longitude) < 10)
                {   
                    Console.WriteLine("geo" + geo);
                    Console.WriteLine("Distance " + getDistanceFromLatLonInKm((double)x, (double)y, (double)geo.Latitude, (double)geo.Longitude));
                    nearGeaos.Add(geo);
                }
                
            }
            return nearGeaos;
        }

        private double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Radius of the earth in km
            double dLat = deg2rad(lat2 - lat1);  // deg2rad below
            double dLon = deg2rad(lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
                ;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km
            return d;
        }

        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
