using HearPrediction.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearPrediction.Api.Data.Services.IRepository
{
	public interface ISpecializationRepository
	{
		Task<IEnumerable<Specialization>> GetSpecializations();
	}
}
