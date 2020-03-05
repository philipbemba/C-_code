using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experian.Api
{
    public interface IExperianService
    {
        public ExperianResponse CheckCredit(List<Applicant> applicants, Settings Settings);
    }
}
