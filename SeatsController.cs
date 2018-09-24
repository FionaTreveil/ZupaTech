using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zupa.Models;

namespace Zupa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ZupaContext _context;

        public SeatsController(ZupaContext context)
        {
            _context = context;
        }

        // GET: api/Seats
        [HttpGet]
        public IEnumerable<Seat> GetSeat()
        {
            IEnumerable<Seat> list = null;
            list = (from c in _context.Seat
                    select getSeat(c)
                    ).AsEnumerable<Seat>();
            return list;
        }

        // GET: api/Seats/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSeat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seat = await _context.Seat.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            return Ok(seat);
        }

        // GET: api/Seats/dateOrEmail
        [HttpGet("{dateoremail}")]
        public IActionResult GetSeat([FromRoute] string dateoremail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Seat> seat = null;
            if (dateoremail.Contains('@'))
            {
                seat = (from c in _context.Seat
                        where c.Email.Equals(dateoremail)
                        select getSeat(c)
                        ).AsEnumerable<Seat>();
            } else
            {
                seat = (from c in _context.Seat
                        where c.Meeting.Date.Equals(dateoremail)
                        select getSeat(c)
                        ).AsEnumerable<Seat>();
            }

            if (seat == null)
            {
                return NotFound();
            }

            return Ok(seat);
        }

        // GET: api/Seats/row/col
        [HttpGet("{row:int}/{col:int}/{meetingdate}")]
        public IActionResult GetSeat([FromRoute] int row, int col, string meetingdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seat = (from c in _context.Seat
                        where (c.Row == row && c.Col == col)
                        select getSeat(c)
                        ).FirstOrDefault<Seat>();

            if (seat == null)
            {
                return NotFound();
            }

            return Ok(seat);
        }

        // PUT: api/Seats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeat([FromRoute] int id, [FromBody] Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seat.Id)
            {
                return BadRequest();
            }

            _context.Entry(seat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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

        // POST: api/Seats
        [HttpPost]
        public async Task<IActionResult> BookSeat([FromBody] Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeat", new { id = seat.Id }, seat);
        }

        // DELETE: api/Seats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> UnbookSeat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seat = await _context.Seat.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            _context.Seat.Remove(seat);
            await _context.SaveChangesAsync();

            return Ok(seat);
        }

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.Id == id);
        }

        private Seat getSeat(Seat c)
        {
            return new Seat
            {
                Id = c.Id,
                Row = c.Row,
                Col = c.Col,
                MeetingId = c.MeetingId,
                Email = c.Email,
                //Meeting = new Meeting { Id = c.MeetingId, Date = c.Meeting.Date }
            };
        }
    }
}