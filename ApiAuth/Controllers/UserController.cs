

using ApiAuth.Dtos;
using ApiAuth.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers;
    public class UserController : BaseApiController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuth _auth;
        private readonly IMapper _mapper;
        public UserController
        (
            ILogger<UserController> logger,
            IUnitOfWork unitOfWork,
            IAuth auth,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _auth = auth;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]    
        public async Task<ActionResult> GetQR([FromBody] LoginDto data)
        {
            try
            {
                User user = await _unitOfWork.Users.FindFirst(p => p.UserName == data.Username && p.Password == data.Password);

                byte[] QR = _auth.CreateQR(ref user);

                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveAsync();
                
                await _auth.SendEmail(user, QR);

                return Ok("El QR se ha enviado al Email.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error, some error occurred");
            }
        }


        [HttpPost("VerifyCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]    
        [ProducesResponseType(StatusCodes.Status400BadRequest)]    
        public async Task<ActionResult> Verify([FromBody] AuthDto data)
        {        
            try
            {
                User u = await _unitOfWork.Users.FindFirst(p => p.UserName == data.Username);
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

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<User>> Post(RegisterDto userRegister)
        {
            bool IsExists = await _unitOfWork.Users.IsExists(userRegister.Username); 
            if(IsExists == true)
            {
                return BadRequest("El Usuario ya esta registrado");
            }
            else {
                 var user = this._mapper.Map<User>(userRegister);
                this._unitOfWork.Users.Add(user);
                await _unitOfWork.SaveAsync();
                if(user == null)
                {
                    return BadRequest();
                }
                userRegister.Id = user.Id;
                return CreatedAtAction(nameof(Post), new {id = user.Id}, user);
            }
        }
    }
