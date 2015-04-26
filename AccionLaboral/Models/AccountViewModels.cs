using System;
using System.Collections.Generic;

namespace AccionLaboral.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Cellphone { get; set; }
        public string HomePhone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string EmployeeAlias { get; set; }
        public int CareerId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public byte[] Photo { get; set; }


        public RoleInfo Role { get; set; }
        public UserInfo User { get; set; }
        public Career Career { get; set; }
    }

    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public bool Busy { get; set; }
    }

    public class RoleInfo
    {
        public string RolId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string UserName { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
