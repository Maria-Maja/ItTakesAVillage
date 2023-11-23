using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ItTakesAVillage.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ItTakesAVillageUser class
public class ItTakesAVillageUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }
}

