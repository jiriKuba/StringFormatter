using StringFormatter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter.Wpf.Interfaces
{
    public interface IPage
    {
        void SetProfilesModels(List<Profile> profiles);
    }
}
