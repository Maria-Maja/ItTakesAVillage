using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ItTakesAVillage.Models;

// Add profile data for application users by adding properties to the ItTakesAVillageUser class
public class ItTakesAVillageUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; } = string.Empty;

    [PersonalData]
    public string LastName { get; set; } = string.Empty;

    public ICollection<UserGroup>? UserGroups { get; set; }

}

