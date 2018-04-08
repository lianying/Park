using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Authorization.Users;
using Park.Users;

namespace Park.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
