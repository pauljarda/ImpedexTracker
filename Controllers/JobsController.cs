using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImpedexTracker.Data;
using ImpedexTracker.Models;

namespace ImpedexTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }

        // GET /api/jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return NotFound();
            return job;
        }

        // POST /api/jobs
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }

        // PUT /api/jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, Job job)
        {
            if (id != job.Id) return BadRequest();
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return NotFound();
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET /api/jobs/{id}/turnaround
        [HttpGet("{id}/turnaround")]
        public async Task<ActionResult> GetTurnaround(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return NotFound();
            if (job.CompletedAt == null)
                return Ok(new { jobId = id, status = "In progress", turnaroundDays = (object)null });

            var days = (job.CompletedAt.Value - job.ReceivedAt).TotalDays;
            return Ok(new { jobId = id, status = "Completed", turnaroundDays = Math.Round(days, 1) });
        }

        // GET /api/jobs/kpi
        [HttpGet("kpi")]
        public async Task<ActionResult> GetKpi()
        {
            var jobs = await _context.Jobs.ToListAsync();
            var completed = jobs.Where(j => j.CompletedAt != null).ToList();

            return Ok(new
            {
                totalJobs = jobs.Count,
                byStatus = jobs.GroupBy(j => j.Status)
                               .Select(g => new { status = g.Key, count = g.Count() }),
                averageTurnaroundDays = completed.Any()
                    ? Math.Round(completed.Average(j => (j.CompletedAt!.Value - j.ReceivedAt).TotalDays), 1)
                    : 0
            });
        }
    }
}