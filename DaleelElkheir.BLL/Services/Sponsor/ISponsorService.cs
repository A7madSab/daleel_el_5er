using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.Sponsors
{
    public interface ISponsorService
    {
        Sponsor GetSponsor(int id);
        List<Sponsor> GetSponsor(Expression<Func<Sponsor, bool>> Predicate);

        List<Sponsor> GetSponsors();
        void InsertSponsor(Sponsor _Sponsor);
        void UpdateSponsor(Sponsor _Sponsor);
        void DeleteSponsor(int id);

    }
}
