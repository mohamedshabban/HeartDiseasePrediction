﻿using HearPrediction.Api.DTO;
using HearPrediction.Api.Helpers;
using HearPrediction.Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HearPrediction.Api.Data.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMailServices _mailServices;
		private readonly JWT _jwt;
		private readonly AppDbContext _context;
		public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,

			RoleManager<IdentityRole> roleManager
			, IOptions<JWT> jwt, IUnitOfWork unitOfWork, AppDbContext context, IMailServices mailServices)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_jwt = jwt.Value;
			_unitOfWork = unitOfWork;
			_mailServices = mailServices;
			_context = context;
		}
		public async Task<AuthModel> LoginTokenAsync(TokenRequestModel model)
		{
			var authModel = new AuthModel();
			var user = await _userManager.FindByEmailAsync(model.Email);
			//if(user.TwoFactorEnabled)
			//{
			//  await _signInManager.SignOutAsync();
			//  await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
			//	var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
			//	var message = new MailRequestDto(user.Email, "OTP Confirmation",token);
			//	_mailServices.SendEmail(message);
			//	return new AuthModel { Message = $"we have sent an OTP to your Email {user.Email}" };
			//}
			if (await IsUserNameAndPasswordInCorrect(user, model.Password))
			{
				authModel.Message = "Email or Password is incorrect";
				return authModel;
			}
			var jwtSecuritToken = await CreateJwtToken(user);
			var roleList = await _userManager.GetRolesAsync(user);

			authModel.IsAuthenticated = true;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecuritToken);
			authModel.Email = user.Email;
			authModel.ExpireOn = jwtSecuritToken.ValidTo;
			authModel.Roles = roleList.ToList();
			var result = await _signInManager.PasswordSignInAsync(user,
				model.Password, false, false);
			if (result.Succeeded)
			{
				authModel.Message = "Login Success";
				return authModel;
			}

			if (user.RefreshTokens.Any(t => t.IsActive))
			{
				var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
				authModel.RefreshToken = activeRefreshToken.Token;
				authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
			}
			else
			{
				var refreshToken = GenerateRefreshToken();
				authModel.RefreshToken = refreshToken.Token;
				authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
				user.RefreshTokens.Add(refreshToken);
				await _userManager.UpdateAsync(user);
			}
			return authModel;
		}

		public async Task<AuthModel> LoginWithOTP(string code, string Email)
		{
			var authModel = new AuthModel();
			var user = await _userManager.FindByNameAsync(Email);
			var signIn = await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
			if (signIn.Succeeded)
			{
				if (user != null)
				{
					var jwtSecuritToken = await CreateJwtToken(user);
					var roleList = await _userManager.GetRolesAsync(user);
					authModel.IsAuthenticated = true;
					authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecuritToken);
					authModel.Email = user.Email;
					authModel.ExpireOn = jwtSecuritToken.ValidTo;
					authModel.Roles = roleList.ToList();
				}

			}
			if (user.RefreshTokens.Any(t => t.IsActive))
			{
				var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
				authModel.RefreshToken = activeRefreshToken.Token;
				authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
			}
			else
			{
				var refreshToken = GenerateRefreshToken();
				authModel.RefreshToken = refreshToken.Token;
				authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
				user.RefreshTokens.Add(refreshToken);
				await _userManager.UpdateAsync(user);
			}
			return authModel;
		}
		private async Task<bool> IsUserNameAndPasswordInCorrect(ApplicationUser user, string password)
		{
			return user is null || !await _userManager.CheckPasswordAsync(user, password);

		}

		//public async Task<AuthModel> ForgetPassword(string email)
		//{
		//	var user = await _userManager.FindByNameAsync(email);
		//	if(user != null)
		//	{

		//	}
		//}
		//public async Task<AuthModel> LogoutAsync()
		//{
		//    return await _signInManager.SignOutAsync();
		//}

		public async Task<AuthModel> RegisterDoctorAsync(RegisterDoctorDTO registerDoctorDTO)
		{
			if (await _userManager.FindByEmailAsync(registerDoctorDTO.Email) is not null)
				return new AuthModel { Message = "Email is already exist" };

			//if (await _userManager.FindByNameAsync(registerDoctorDTO.FullName) is not null)
			//	return new AuthModel { Message = "Username is already exist" };
			var user = new ApplicationUser
			{
				Email = registerDoctorDTO.Email,
				UserName = registerDoctorDTO.Email,
				FullName = registerDoctorDTO.FullName,
				FirstName = registerDoctorDTO.FirstName,
				LastName = registerDoctorDTO.LastName,
				Gender = registerDoctorDTO.Gender,
				BirthDate = registerDoctorDTO.BirthDate,
				PhoneNumber = registerDoctorDTO.PhoneNumber,
				//TwoFactorEnabled = true,
			};
			var result = await _userManager.CreateAsync(user, registerDoctorDTO.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Doctor");
				//var doctorVM = new RegisterDoctorDTO
				//{
				//	Specializations =await _context.Specializations
				//};
				var doctor = new Doctor
				{
					UserId = user.Id,
					SpecializationId = registerDoctorDTO.Specialization,
					Address = registerDoctorDTO.Address,
					IsAvailable = true,
				};
				await _unitOfWork.Doctors.Add(doctor);
				await _unitOfWork.Complete();
				var jwtSecurityToken = await CreateJwtToken(user);
				return new AuthModel
				{
					Email = user.Email,
					ExpireOn = jwtSecurityToken.ValidTo,
					IsAuthenticated = true,
					Roles = new List<string> { "Doctor" },
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				};
			}

			var errors = string.Empty;
			foreach (var error in result.Errors)
				errors += $"{error.Description}";

			return new AuthModel { Message = errors };
		}

		public async Task<AuthModel> RegisterMedicalAnalystAsync(RegisterMedicalAnalystDTO registerMedicalAnalystDTO)
		{
			if (await _userManager.FindByEmailAsync(registerMedicalAnalystDTO.Email) is not null)
				return new AuthModel { Message = "Email is already exist" };

			var user = new ApplicationUser
			{
				Email = registerMedicalAnalystDTO.Email,
				UserName = registerMedicalAnalystDTO.Email,
				FullName = registerMedicalAnalystDTO.FullName,
				FirstName = registerMedicalAnalystDTO.FirstName,
				LastName = registerMedicalAnalystDTO.LastName,
				Gender = registerMedicalAnalystDTO.Gender,
				BirthDate = registerMedicalAnalystDTO.BirthDate,
				PhoneNumber = registerMedicalAnalystDTO.PhoneNumber,
				//TwoFactorEnabled = true,
			};
			var result = await _userManager.CreateAsync(user, registerMedicalAnalystDTO.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "MedicalAnalyst");
				var medicalAnalyst = new MedicalAnalyst
				{
					UserId = user.Id,
					LabId = registerMedicalAnalystDTO.Lab
				};
				await _unitOfWork.medicalAnalyst.Add(medicalAnalyst);
				await _unitOfWork.Complete();

				var jwtSecurityToken = await CreateJwtToken(user);
				return new AuthModel
				{
					Email = user.Email,
					ExpireOn = jwtSecurityToken.ValidTo,
					IsAuthenticated = true,
					Roles = new List<string> { "MedicalAnalyst" },
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				};
			}

			var errors = string.Empty;
			foreach (var error in result.Errors)
				errors += $"{error.Description}";

			return new AuthModel { Message = errors };
		}

		public async Task<AuthModel> RegisterReceptionistAsync(RegisterReciptionistDTO registerReciptionistDTO)
		{
			if (await _userManager.FindByEmailAsync(registerReciptionistDTO.Email) is not null)
				return new AuthModel { Message = "Email is already exist" };

			var user = new ApplicationUser
			{
				Email = registerReciptionistDTO.Email,
				UserName = registerReciptionistDTO.Email,
				FullName = registerReciptionistDTO.FullName,
				FirstName = registerReciptionistDTO.FirstName,
				LastName = registerReciptionistDTO.LastName,
				Gender = registerReciptionistDTO.Gender,
				BirthDate = registerReciptionistDTO.BirthDate,
				PhoneNumber = registerReciptionistDTO.PhoneNumber,
				//TwoFactorEnabled = true,
			};
			var result = await _userManager.CreateAsync(user, registerReciptionistDTO.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Admin");
				var reciptionist = new Reciptionist
				{
					UserId = user.Id,
				};
				await _unitOfWork.reciptionist.Add(reciptionist);
				await _unitOfWork.Complete();

				var jwtSecurityToken = await CreateJwtToken(user);
				return new AuthModel
				{
					Email = user.Email,
					ExpireOn = jwtSecurityToken.ValidTo,
					IsAuthenticated = true,
					Roles = new List<string> { "Admin" },
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				};
			}

			var errors = string.Empty;
			foreach (var error in result.Errors)
				errors += $"{error.Description}";

			return new AuthModel { Message = errors };
		}

		public async Task<AuthModel> RegisterUserAsync(RegisterUserDTO registerUserDTO)
		{
			if (await _userManager.FindByEmailAsync(registerUserDTO.Email) is not null)
				return new AuthModel { Message = "Email is already exist" };

			var user = new ApplicationUser
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				Email = registerUserDTO.Email,
				UserName = registerUserDTO.Email,
				FullName = registerUserDTO.FullName,
				FirstName = registerUserDTO.FirstName,
				LastName = registerUserDTO.LastName,
				Gender = registerUserDTO.Gender,
				BirthDate = registerUserDTO.BirthDate,
				PhoneNumber = registerUserDTO.PhoneNumber,
				//TwoFactorEnabled = true,
			};
			var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "User");
				var patient = new Patient
				{
					UserId = user.Id,
					SSN = registerUserDTO.SSN,
					Insurance_No = registerUserDTO.Insurance_No,
				};
				//var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				//var confirmationLink = Url.Action(nameof(ConfirmationOfEmail),"Authentication",new ConfirmEmailDto { Token = token,Email = user.Email});
				//var message = new MailRequestDto(user.Email, "Confirmation Email Link",confirmationLink);
				//_mailServices.SendEmail(message);
				await _unitOfWork.Patients.Add(patient);
				await _unitOfWork.Complete();

				var jwtSecurityToken = await CreateJwtToken(user);
				return new AuthModel
				{
					Email = user.Email,
					ExpireOn = jwtSecurityToken.ValidTo,
					IsAuthenticated = true,
					Roles = new List<string> { "User" },
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				};
			}

			var errors = string.Empty;
			foreach (var error in result.Errors)
				errors += $"{error.Description}";

			return new AuthModel { Message = errors };
		}
		public async Task<AuthModel> ConfirmationOfEmail(ConfirmEmailDto confirm)
		{
			var user = await _userManager.FindByNameAsync(confirm.Email);
			if (user != null)
			{
				var result = await _userManager.ConfirmEmailAsync(user, confirm.Token);
				if (result.Succeeded)
				{
					return new AuthModel
					{
						Message = "Email Verfied Successfully"
					};
				}
			}

			return new AuthModel { Message = "This User Doesn't exist!" };
		}

		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email,user.Email),
				new Claim("uid",user.Id)
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmerticSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
			var signingCredentials = new SigningCredentials(symmerticSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwt.Issuer,
				audience: _jwt.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddDays(1),//.AddMinutes(_jwt.DurationInMinutes)
				signingCredentials: signingCredentials,
				notBefore: DateTime.UtcNow);

			return jwtSecurityToken;
		}

		public async Task<AuthModel> RefreshTokenAsync(string token)
		{
			var authModel = new AuthModel();

			var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

			if (user == null)
			{
				authModel.Message = "Invalid token";
				return authModel;
			}

			var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

			if (!refreshToken.IsActive)
			{
				authModel.Message = "Inactive token";
				return authModel;
			}

			refreshToken.RevokedOn = DateTime.UtcNow;

			var newRefreshToken = GenerateRefreshToken();
			user.RefreshTokens.Add(newRefreshToken);
			await _userManager.UpdateAsync(user);

			var jwtToken = await CreateJwtToken(user);
			authModel.IsAuthenticated = true;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			authModel.Email = user.Email;
			//authModel.Email = user.UserName;
			var roles = await _userManager.GetRolesAsync(user);
			authModel.Roles = roles.ToList();
			authModel.RefreshToken = newRefreshToken.Token;
			authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

			return authModel;
		}



		public async Task<bool> RevokeTokenAsync(string token)
		{
			var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

			if (user == null)
				return false;

			var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

			if (!refreshToken.IsActive)
				return false;

			refreshToken.RevokedOn = DateTime.UtcNow;

			await _userManager.UpdateAsync(user);

			return true;
		}

		private RefreshToken GenerateRefreshToken()
		{
			var randomNumber = new byte[32];

			using var generator = new RNGCryptoServiceProvider();

			generator.GetBytes(randomNumber);

			return new RefreshToken
			{
				Token = Convert.ToBase64String(randomNumber),
				ExpiresOn = DateTime.UtcNow.AddDays(10),
				CreatedOn = DateTime.UtcNow
			};
		}


	}
}
