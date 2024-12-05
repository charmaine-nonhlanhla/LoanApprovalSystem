using Application.Applicants;
using Application.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LoanApplicantsController : BaseAPIController
    {
        [HttpGet]
        public async Task<ActionResult<List<LoanApplicants>>> GetApplicants()
        {
            return await Mediator.Send(new GetApplicants.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanApplicants>> GetApplicant(int id)
        {
            return await Mediator.Send(new GetApplicant.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplicant(LoanApplicantDTO applicant)
        {
            await Mediator.Send(new CreateApplicant.Command { LoanApplicant = applicant });

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicantDetails(int id, LoanApplicantDTO applicant)
        {
            applicant.Id = id;

            await Mediator.Send(new UpdateApplicant.Command { LoanApplicant = applicant });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            await Mediator.Send(new DeleteApplicant.Command { Id = id });

            return Ok();
        }
    }
}