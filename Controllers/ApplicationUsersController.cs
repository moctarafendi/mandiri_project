using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mandiri_project.Entities;
using mandiri_project.Interfaces;
using mandiri_project.Enums;
using mandiri_project.Models;
using Microsoft.AspNetCore.Authorization;
using mandiri_project.Services;
using mandiri_project.RequestResponseModels.RequestModel.ApplicationUser;
using mandiri_project.RequestResponseModels.ResponseModel.ApplicationUser;
using mandiri_project.RequestResponseModels.ResponseModel.BusinessAreaMasters;

namespace mandiri_project.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IApplicationUser _applicationUser;
        private readonly UserIdentityService _userIdentityService;

        public ApplicationUsersController(AppDbContext context, IApplicationUser applicationUser, UserIdentityService userIdentityService)
        {
            _context = context;
            _applicationUser = applicationUser;
            _userIdentityService = userIdentityService;
        }

        // POST: AppicationUsers/Create
        // Only administrator
        [HttpPost("create")]
        public async Task<IActionResult> Create(ApplicationUserCreateRequest request)
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

                var userInDb = await _context.ApplicationUsers
                     .Where(q => q.UserId == request.UserId)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

                if (userInDb != null)
                {
                    return Problem(
                                     title: "User has been exist in DB.",
                                     detail: $"User Id sudah terdaftar di DB.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                }

                var emailInDb = await _context.ApplicationUsers
                  .Where(q => q.Email.ToLower() == request.Email.ToLower())
                  .AsNoTracking()
                  .FirstOrDefaultAsync();

                if (emailInDb != null)
                {
                    return Problem(
                                     title: "Email has been exist in DB.",
                                     detail: $"Email sudah terdaftar di DB.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                }

                var password = _applicationUser.HashPassword(request.Password);

                var appUser = new ApplicationUser();
                appUser.Email = request.Email;
                appUser.Name = request.Name;
                appUser.CreatedOn = DateTime.Now;
                appUser.ModifiedOn = DateTime.Now;
                appUser.UserId = request.UserId;
                appUser.CreatedBy = _userIdentityService.UserId;
                appUser.BusinessAreaCode = request.BusinessAreaCode;
                appUser.ModifiedBy = _userIdentityService.UserId;
                appUser.Password = password;
                appUser.Phone = request.Phone;
                appUser.Role = RolesType.Admin;
                appUser.Token = "";
                appUser.TokenExpireDate = DateTime.Now;

                _context.Add(appUser);
                await _context.SaveChangesAsync();


                return Ok(new ApplicationUserCreateResponse() { 
                    Acknowledge = 1,
                    Message = "User berhasil di create."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dataDb = await _context.ApplicationUsers
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
                    return NotFound("user tidak ditemukan");
                }

                var result = new ApplicationUserAllResponse();

                foreach (var dat in dataDb)
                {
                    var newDat = new ApplicationUserData();
                    newDat.BusinessAreaCode = dat.BusinessAreaCode;
                    newDat.Email = dat.Email;
                    newDat.Name = dat.Name;
                    newDat.Phone = dat.Phone;
                    newDat.UserId = dat.UserId;
                    result.appUserDataList.Add(newDat);
                }

                result.Acknowledge = 1;
                result.Message = "Success";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationUserAllResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }
        // only administrator or same business area and that user
        [HttpPost("details")]
        public async Task<IActionResult> Details(ApplicationUserDetailsRequest request)
        {
            try
            {
                if (request.UserId == null || _context.ApplicationUsers == null)
                {
                    return NotFound("User Id tidak boleh null");
                }
                              

                var appicationUser = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(m => m.UserId == request.UserId);

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    if (_userIdentityService.BusinessAreaCode != appicationUser.BusinessAreaCode)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }

                    if (_userIdentityService.Email != appicationUser.Email)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }
                }                

                if (appicationUser == null)
                {
                    return NotFound("User Id tidak ditemukan");
                }

                var result = new ApplicationUserDetailsResponse();
                result.BusinessAreaCode = appicationUser.BusinessAreaCode;
                result.Email = appicationUser.Email;
                result.Name = appicationUser.Name;
                result.Phone = appicationUser.Phone;
                result.UserId = appicationUser.UserId;
                result.Acknowledge = 1;
                result.Message = "Success";

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationUserDetailsResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }            
        }

        // GET: AppicationUsers/Edit/5
        // only administrator or same business area and that user
        [HttpPost("update")]      
        public async Task<IActionResult> Update(ApplicationUserEditRequest request)
        {
            try
            {
                var applicationUser = await _context.ApplicationUsers
                 .FirstOrDefaultAsync(m => m.UserId == request.UserId);

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    if (_userIdentityService.BusinessAreaCode != applicationUser.BusinessAreaCode)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }

                    if (_userIdentityService.Email != applicationUser.Email)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }
                }

                if (applicationUser == null)
                {
                    return NotFound("User Id tidak ditemukan");
                }

                applicationUser.Name = request.Name;
                applicationUser.Phone = request.Phone;
                applicationUser.Email = request.Email;
                applicationUser.ModifiedBy = _userIdentityService.UserId;
                applicationUser.ModifiedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "User berhasil di update."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // only administrator or same business area and that user
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ApplicationUserChangePasswordRequest request)
        {
            try
            {
                var applicationUser = await _context.ApplicationUsers
                 .FirstOrDefaultAsync(m => m.UserId == request.UserId);

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    if (_userIdentityService.BusinessAreaCode != applicationUser.BusinessAreaCode)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }

                    if (_userIdentityService.Email != applicationUser.Email)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }
                }

                if (applicationUser == null)
                {
                    return NotFound("User Id tidak ditemukan");
                }

                var password = _applicationUser.HashPassword(request.Password);
                applicationUser.Password = password;
                applicationUser.ModifiedBy = _userIdentityService.UserId;
                applicationUser.ModifiedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 1,
                    Message = "Password berhasil diubah."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }

        // GET: AppicationUsers/Delete/5
        // only administrator or same business area and that user
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(ApplicationUserDeleteRequest request)
        {
            try
            {
                if (request.UserId == null || _context.ApplicationUsers == null)
                {
                    return NotFound("User Id tidak boleh null");
                }

                var applicationUser = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(m => m.UserId == request.UserId);
                if (applicationUser == null)
                {
                    return NotFound("User Id tidak ditemukan");
                }

                if (_userIdentityService.Role != RolesType.Administrator)
                {
                    if (_userIdentityService.BusinessAreaCode != applicationUser.BusinessAreaCode)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }

                    if (_userIdentityService.Email != applicationUser.Email)
                    {
                        return Problem(
                                     title: "User doesn't have privileged.",
                                     detail: $"Anda tidak dapat melihat data business area lain.",
                                     statusCode: StatusCodes.Status403Forbidden,
                                     instance: HttpContext.Request.Path
                                 );
                    }
                }

                _context.Remove(applicationUser);
                await _context.SaveChangesAsync();

                return Ok(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 1,
                    Message = $"User {request.UserId} berhasil didelete."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationUserCreateResponse()
                {
                    Acknowledge = 0,
                    Message = ex.Message
                });
            }
        }
    }
}
