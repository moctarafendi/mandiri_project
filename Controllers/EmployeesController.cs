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
using mandiri_project.RequestResponseModels.RequestModel.BusinessAreaMasters;
using mandiri_project.RequestResponseModels.ResponseModel.BusinessAreaMasters;
using mandiri_project.Services;
using mandiri_project.RequestResponseModels.RequestModel.Employee;
using mandiri_project.Interfaces;
using mandiri_project.RequestResponseModels.ResponseModel.Employee;

namespace mandiri_project.Controllers
{
    [Route("employee")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserIdentityService _userIdentityService;

        public EmployeesController(AppDbContext context, UserIdentityService userIdentityService)
        {
            _context = context;
            _userIdentityService = userIdentityService;
        }

        // only administrator or same business area and that user
        [HttpPost("create")]
        public async Task<IActionResult> Create(EmployeeCreateRequest request)
        {
            try
            {
                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    if (_userIdentityService.BusinessAreaCode != request.BusinessAreaCode)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                        );
                    }
                   
                }

                var dataDb = await _context.Employees
                     .Where(q => q.EmployeeNo == request.EmployeeNo)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

                if (dataDb != null)
                {
                    return Problem(
                                     title: "Employee No has been exist in DB.",
                                     detail: $"Area Code sudah terdaftar di DB.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                }

                var newData = new Employee();
                newData.EmployeeNo = request.EmployeeNo;
                newData.HireDate = request.HireDate;
                newData.TerminationDate = null;
                newData.FirstName = request.FirstName;
                newData.BusinessAreaCode = request.BusinessAreaCode;
                newData.LastName = request.LastName;
                newData.Salary = request.Salary;
                newData.CreatedOn = DateTime.Now;
                newData.ModifiedOn = DateTime.Now;
                newData.CreatedBy = _userIdentityService.UserId;
                newData.ModifiedBy = _userIdentityService.UserId    ;

                _context.Add(newData);
                await _context.SaveChangesAsync();


                return Ok(new EmployeeCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "Employee berhasil di create."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new EmployeeCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // only data from they business area code
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(EmployeeAllRequest request)
        {
            try
            {
                var dataDb = await _context.Employees
                    .AsNoTracking()
                    .ToListAsync();

                var annualReviewFrDb = await _context.AnnualReviews
                    .Select(q=>q.EmployeeNo)
                   .AsNoTracking()
                    .ToListAsync();

                if (dataDb == null)
                {
                    return NotFound("Area Code tidak ditemukan");
                }

                var result = new EmployeeAllResponse();

                foreach (var dat in dataDb)
                {
                    if (_userIdentityService.Role != RolesType.Administrator)
                    {
                        if (_userIdentityService.BusinessAreaCode != dat.BusinessAreaCode)
                            continue;

                    }

                    if (request.IsReview)
                    {
                        if (!annualReviewFrDb.Contains(dat.EmployeeNo))
                            continue;
                    }

                    if (request.IsTerminate)
                    {
                        if (dat.TerminationDate == null)
                            continue;
                    }

                    var newDat = new EmployeeAllData();
                    newDat.EmployeeNo = dat.EmployeeNo;
                    newDat.HireDate = dat.HireDate;
                    newDat.TerminationDate = dat.TerminationDate;
                    newDat.FirstName = dat.FirstName;
                    newDat.BusinessAreaCode = dat.BusinessAreaCode;
                    newDat.LastName = dat.LastName;
                    newDat.Salary = dat.Salary;
                    result.empDataList.Add(newDat);
                }

                result.Acknowledge = 1;
                result.Message = "Success";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new EmployeeAllResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // only administrator or same business area and that user
        [HttpPost("details")]
        public async Task<IActionResult> Details(EmployeeDetailsRequest request)
        {
            try
            {
                var dataDb = await _context.Employees
                     .FirstOrDefaultAsync(m => m.EmployeeNo == request.EmployeeNo);

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
                    return NotFound("Employee tidak ditemukan");
                }

                var result = new EmployeeDetailsResponse();
                result.EmployeeNo = dataDb.EmployeeNo;
                result.HireDate = dataDb.HireDate;
                result.TerminationDate = dataDb.TerminationDate;
                result.FirstName = dataDb.FirstName;
                result.BusinessAreaCode = dataDb.BusinessAreaCode;
                result.LastName = dataDb.LastName;
                result.Salary = dataDb.Salary;

                result.Acknowledge = 1;
                result.Message = "Success";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new EmployeeDetailsResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // only administrator or same business area and that user
        [HttpPost("update")]
        public async Task<IActionResult> Update(EmployeeEditRequest request)
        {
            try
            {
                var dataDb = await _context.Employees
                 .FirstOrDefaultAsync(m => m.EmployeeNo == request.EmployeeNo);

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
                    return NotFound("Area tidak ditemukan");
                }

                dataDb.EmployeeNo = request.EmployeeNo;
                dataDb.FirstName = request.FirstName;
                dataDb.LastName = request.LastName;
                dataDb.Salary = request.Salary;
                dataDb.ModifiedBy = _userIdentityService.UserId;
                dataDb.ModifiedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new EmployeeCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "Employee berhasil di update."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new EmployeeCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // only administrator or same business area and that user
        [HttpPost("terminate")]
        public async Task<IActionResult> Terminate(EmployeeTerminateRequest request)
        {
            try
            {
                var dataDb = await _context.Employees
                  .FirstOrDefaultAsync(m => m.EmployeeNo == request.EmployeeNo);

                if (dataDb == null)
                {
                    return NotFound("Area tidak ditemukan");
                }

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

                dataDb.TerminationDate = DateTime.Now;
                dataDb.ModifiedBy = _userIdentityService.UserId;
                dataDb.ModifiedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new EmployeeCreateResponse()
                {
                    Acknowledge = 1,
                    Message = $"Area {request.EmployeeNo} berhasil diterminate."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new EmployeeCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }
    }
}
