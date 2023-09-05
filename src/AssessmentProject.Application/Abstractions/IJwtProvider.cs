using AssessmentProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(PersonEntity person);
    }
}
