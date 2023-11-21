// <copyright file="ApiControllerBase.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Common Controller Base class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
