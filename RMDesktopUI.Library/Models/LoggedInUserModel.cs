﻿namespace RMDesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string? Token { get; set; }
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
