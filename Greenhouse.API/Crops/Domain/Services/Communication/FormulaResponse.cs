using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class FormulaResponse : BaseResponse<Formula>
{
    public FormulaResponse(string message) : base(message)
    {
    }

    public FormulaResponse(Formula resource) : base(resource)
    {
    }
}