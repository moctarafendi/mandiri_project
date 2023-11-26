using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mandiri_project.Entities;
using mandiri_project.Enums;
using mandiri_project.Interfaces;
using mandiri_project.RequestResponseModels.RequestModel.ApplicationUser;
using mandiri_project.RequestResponseModels.RequestModel.BusinessAreaMasters;
using mandiri_project.RequestResponseModels.ResponseModel.ApplicationUser;
using mandiri_project.RequestResponseModels.ResponseModel.BusinessAreaMasters;
using mandiri_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace mandiri_project.Controllers
{

    [Route("area")]
    [ApiController]
    [Authorize]
    public class BusinessAreaMastersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserIdentityService _userIdentityService;

        public BusinessAreaMastersController(AppDbContext context, UserIdentityService userIdentityService)
        {
            _context = context;
            _userIdentityService = userIdentityService;
        }

        // POST: BusinessAreaMasters/Create
        // Only administrator
        [HttpPost("create")]
        public async Task<IActionResult> Create(BusinessAreaMastersCreateRequest request)
        {
            try
            {
                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    return Problem(
                                  title: "User doesn't have privileged.",
                                  detail: $"Silahkan menghubungi Administrator.",
                                  statusCode: StatusCodes.Status403Forbidden,
                                  instance: HttpContext.Request.Path
                              );
                }

                var areaData = await _context.BusinessAreaMasters
                     .Where(q => q.AreaCode == request.AreaCode)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

                if (areaData != null)
                {
                    return Problem(
                                     title: "Area Code has been exist in DB.",
                                     detail: $"Area Code sudah terdaftar di DB.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                }
                
                var businessAreaNew = new BusinessAreaMaster();
                businessAreaNew.AreaCode = request.AreaCode;
                businessAreaNew.AreaName = request.AreaName;
                businessAreaNew.CreatedOn = DateTime.Now;
                businessAreaNew.ModifiedOn = DateTime.Now;              
                businessAreaNew.CreatedBy = _userIdentityService.UserId;
                businessAreaNew.ModifiedBy = _userIdentityService.UserId;

                _context.Add(businessAreaNew);
                await _context.SaveChangesAsync();


                return Ok(new BusinessAreaMastersCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "Business Area berhasil di create."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BusinessAreaMastersCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // only administrator 
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dataDb = await _context.BusinessAreaMasters
                    .AsNoTracking()
                    .ToListAsync();

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    return Problem(
                                    title: "User doesn't have privileged.",
                                    detail: $"Anda tidak dapat melihat data ini.",
                                    statusCode: StatusCodes.Status403Forbidden,
                                    instance: HttpContext.Request.Path
                                );
                }

                if (dataDb == null)
                {
                    return NotFound("Area Code tidak ditemukan");
                }

                var result = new BusinessAreaMastersAllResponse();

                foreach (var dat in dataDb)
                {
                    var newDat = new BusinessAreaMastersData();
                    newDat.AreaCode = dat.AreaCode;
                    newDat.AreaName = dat.AreaName;
                    result.areaMastersDataList.Add(newDat);
                }

                result.Acknowledge = 1;
                result.Message = "Success";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BusinessAreaMastersAllResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }
        // only administrator 
        [HttpPost("details")]
        public async Task<IActionResult> Details(BusinessAreaMastersDetailsRequest request)
        {
            try
            {
               var dataDb = await _context.BusinessAreaMasters
                    .FirstOrDefaultAsync(m => m.AreaCode == request.AreaCode);

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    return Problem(
                                    title: "User doesn't have privileged.",
                                    detail: $"Anda tidak dapat melihat data ini.",
                                    statusCode: StatusCodes.Status403Forbidden,
                                    instance: HttpContext.Request.Path
                                );
                }

                if (dataDb == null)
                {
                    return NotFound("Area Code tidak ditemukan");
                }

                var result = new BusinessAreaMastersDetailsResponse();
                result.AreaCode = dataDb.AreaCode;
                result.AreaName = dataDb.AreaName;
                result.Acknowledge = 1;
                result.Message = "Success";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BusinessAreaMastersDetailsResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // GET: BusinessAreaMasters/Edit/5
        // only administrator 
        [HttpPost("update")]
        public async Task<IActionResult> Update(BusinessAreaMastersEditRequest request)
        {
            try
            {
                var dataDb = await _context.BusinessAreaMasters
                 .FirstOrDefaultAsync(m => m.AreaCode == request.AreaCode);

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    return Problem(
                                    title: "User doesn't have privileged.",
                                    detail: $"Anda tidak dapat melihat data ini.",
                                    statusCode: StatusCodes.Status403Forbidden,
                                    instance: HttpContext.Request.Path
                                );
                }

                if (dataDb == null)
                {
                    return NotFound("Area tidak ditemukan");
                }

                dataDb.AreaName = request.AreaName;
                dataDb.ModifiedBy = _userIdentityService.UserId;
                dataDb.ModifiedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new BusinessAreaMastersCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "Area berhasil di update."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BusinessAreaMastersCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // GET: BusinessAreaMasters/Delete/5
        // only administrator 
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(BusinessAreaMastersDeleteRequest request)
        {
            try
            {
                var dataDb = await _context.BusinessAreaMasters
                  .FirstOrDefaultAsync(m => m.AreaCode == request.AreaCode);

                if (dataDb == null)
                {
                    return NotFound("Area tidak ditemukan");
                }

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    return Problem(
                                    title: "User doesn't have privileged.",
                                    detail: $"Anda tidak dapat melihat data ini.",
                                    statusCode: StatusCodes.Status403Forbidden,
                                    instance: HttpContext.Request.Path
                                );
                }

                _context.Remove(dataDb);
                await _context.SaveChangesAsync();

                return Ok(new BusinessAreaMastersCreateResponse()
                {
                    Acknowledge = 1,
                    Message = $"Area {request.AreaCode} berhasil didelete."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BusinessAreaMastersCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }
    }
}
