

using ApiAuth.Dtos;
using ApiAuth.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers;
    public class UserController : BaseApiController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuth _auth;
        public UserController
        (
            ILogger<UserController> logger,
            IUnitOfWork unitOfWork,
            IAuth auth
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _auth = auth;
        }

        [HttpGet("CreateQR/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]    
        public async Task<ActionResult> GetQR(long id)
        {        
            try
            {
                User user = await _unitOfWork.Users.FindFirst(p => p.Id == id);
                byte[] QR = _auth.CreateQR(ref user);            

                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveAsync();
                return File(QR,"image/png");
            }
            catch (Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest("error, some error occurred");
            }                        
        }

        [HttpGet("VerifyCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]    
        public async Task<ActionResult> Verify([FromBody] AuthDto data)
        {        
            try
            {
                User u = await _unitOfWork.Users.FindFirst(p => p.Id == data.Id);
                if(u.TwoStepSecret == null)
                {
                    throw new ArgumentNullException(u.TwoStepSecret);
                }
                var isVerified = _auth.VerifyCode(u.TwoStepSecret, data.Code);            

                if(isVerified == true)
                {
                    return Ok("authenticated, checked");
                }
                return Unauthorized();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("error, some error occurred");
            }                        
        }
    }
