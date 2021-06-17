using AutoMapper;
using EduTestWebApplication.Common.Services;
using EduTestWebApplication.Controllers;
using EduTestWebApplication.Models;
using EduTestWebApplication.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduTestWebApplication.Test
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<SignInManager<IdentityUser>> _mockSignInManager;
        private readonly StudentsController _studentController;
        private Student _studentFromSetup;
        private IdentityUser _appUser;
        private StudentViewModel _studentViewModel;
        private Student _student;

        public StudentsControllerTests()
        {
            _mockStudentService = new Mock<IStudentService>();
            _mockMapper = new Mock<IMapper>();

            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                /* IUserStore<TUser> store */Mock.Of<IUserStore<IdentityUser>>(),
                /* IOptions<IdentityOptions> optionsAccessor */null,
                /* IPasswordHasher<TUser> passwordHasher */null,
                /* IEnumerable<IUserValidator<TUser>> userValidators */null,
                /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
                /* ILookupNormalizer keyNormalizer */null,
                /* IdentityErrorDescriber errors */null,
                /* IServiceProvider services */null,
                /* ILogger<UserManager<TUser>> logger */null);

            _mockSignInManager = new Mock<SignInManager<IdentityUser>>(
                _mockUserManager.Object,
                /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
                /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
                /* IOptions<IdentityOptions> optionsAccessor */null,
                /* ILogger<SignInManager<TUser>> logger */null,
                /* IAuthenticationSchemeProvider schemes */null,
                /* IUserConfirmation<TUser> confirmation */null);

            _studentController = new StudentsController(
                _mockStudentService.Object,
                _mockMapper.Object,
                _mockUserManager.Object,
                _mockSignInManager.Object
                );

            _studentFromSetup = new Student();

            _appUser = new IdentityUser()
            {
                Id = "00000000-0000-0000-0000-000000000000"
            };

            _studentViewModel = new StudentViewModel
            {
                Name = "Test Student Name",
                YearGroup = 1
            };

            _student = new Student
            {
                Name = "Test Student Name",
                YearGroup = 1
            };
        }

        [Fact]
        public async Task Create_ActionExecutes_ReturnsCreatedStudentAsync()
        {
            _mockStudentService.Setup(s => s.AddStudent(It.IsAny<Student>(), new Guid()))
                .Callback<Student, Guid>((student, userId) => _studentFromSetup = student);
            _mockUserManager
                .Setup(_ => _.GetUserAsync(null))
                .ReturnsAsync(_appUser);
            _mockMapper
                .Setup(_ => _.Map<Student>(_studentViewModel))
                .Returns(_student);

            var result = await _studentController.Create(_studentViewModel);

            _mockStudentService.Verify(x =>
            x.AddStudent(It.IsAny<Student>(), new Guid()), Times.Once);

            Assert.Equal(_studentFromSetup.Name, _student.Name);
            Assert.Equal(_studentFromSetup.YearGroup, _student.YearGroup);
        }

        [Fact]
        public async Task Create_ActionExecutes_Returns_Created_Student_With_Matching_CreatedBy_UserId_Async()
        {
            

            _mockStudentService.Setup(s => s.AddStudent(It.IsAny<Student>(), new Guid()))
                .Callback<Student, Guid>((student, userId) => _studentFromSetup = student);
            _mockUserManager
                .Setup(_ => _.GetUserAsync(null))
                .ReturnsAsync(_appUser);
            _mockMapper
                .Setup(_ => _.Map<Student>(_studentViewModel))
                .Returns(_student);

            var result = await _studentController.Create(_studentViewModel);

            _mockStudentService.Verify(x =>
            x.AddStudent(It.IsAny<Student>(), new Guid()), Times.Once);

            Assert.Equal(_studentFromSetup.CreatedBy, Guid.Parse(_appUser.Id));
        }

        [Fact]
        public async Task Create_ActionExecutes_Returns_Created_Student_With_Correct_Name_And_YearGroup_Async()
        {


            _mockStudentService.Setup(s => s.AddStudent(It.IsAny<Student>(), new Guid()))
                .Callback<Student, Guid>((student, userId) => _studentFromSetup = student);
            _mockUserManager
                .Setup(_ => _.GetUserAsync(null))
                .ReturnsAsync(_appUser);
            _mockMapper
                .Setup(_ => _.Map<Student>(_studentViewModel))
                .Returns(_student);

            var result = await _studentController.Create(_studentViewModel);

            Assert.True(_student.Name.Length <= 100);
            Assert.InRange(_student.YearGroup,1,8);
        }

        [Fact]
        public void Create_Action_Has_Authorize_Attribute_()
        {
            _mockStudentService.Setup(s => s.AddStudent(It.IsAny<Student>(), new Guid()))
                   .Callback<Student, Guid>((student, userId) => _studentFromSetup = student);
            _mockUserManager
                .Setup(_ => _.GetUserAsync(null))
                .ReturnsAsync(_appUser);
            _mockMapper
                .Setup(_ => _.Map<Student>(_studentViewModel))
                .Returns(_student);
            var actualAttribute = _studentController.GetType().GetMethods()
                .Where(m => m.CustomAttributes.Count() == 5 && m.Name == "Create")
                .First()
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute[0].GetType());//Ensure [Authorize] Attribute exist
        }
    }
}
