using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mandiri_project.Entities;
using Microsoft.AspNetCore.Authorization;
using mandiri_project.Enums;
using mandiri_project.RequestResponseModels.RequestModel.Employee;
using mandiri_project.RequestResponseModels.ResponseModel.Employee;
using mandiri_project.Services;
using mandiri_project.RequestResponseModels.RequestModel.AnnualReview;
using mandiri_project.RequestResponseModels.ResponseModel.AnnualReview;

namespace mandiri_project.Controllers
{
    [Route("review")]
    [ApiController]
    [Authorize]
    public class AnnualReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserIdentityService _userIdentityService;

        public AnnualReviewsController(AppDbContext context, UserIdentityService userIdentityService)
        {
            _context = context;
            _userIdentityService = userIdentityService;
        }

        // only administrator or same business area and that user
        [HttpPost("create")]
        public async Task<IActionResult> Create(AnnualReviewCreateRequest request)
        {
            try
            {
               

                var dataDb = await _context.Employees
                     .Where(q => q.EmployeeNo == request.EmployeeNo)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    if (_userIdentityService.BusinessAreaCode != dataDb.BusinessAreaCode)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                        );
                    }

                }

                if (dataDb == null)
                {
                    return Problem(
                                     title: "Employee No has not been exist in DB.",
                                     detail: $"Area Code tidak terdaftar di DB.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                }

                var dataDb2 = await _context.AnnualReviews
                   .Where(q => q.EmployeeNo == request.EmployeeNo)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();

                if (dataDb2 != null)
                {
                    return Problem(
                                     title: "Review has been exist in DB.",
                                     detail: $"Review sudah terdaftar di DB.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                }

                var newData = new AnnualReview();
                newData.Id = Guid.NewGuid();
                newData.EmployeeNo = request.EmployeeNo;
                newData.ReviewDate = request.ReviewDate;
                newData.IsDelete = false;
              
                newData.CreatedOn = DateTime.Now;
                newData.ModifiedOn = DateTime.Now;
                newData.CreatedBy = _userIdentityService.UserId;
                newData.ModifiedBy = _userIdentityService.UserId;

                _context.Add(newData);
                await _context.SaveChangesAsync();


                return Ok(new AnnualReviewCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "Review berhasil di create."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AnnualReviewCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

    }
}
