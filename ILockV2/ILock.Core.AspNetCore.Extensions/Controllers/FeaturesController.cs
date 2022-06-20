// <copyright file="FeaturesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.AspNetCore.Extensions.Helpers;
using ILock.Core.Data.Entities;
using ILock.Core.Data.Models;
using ILock.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Controllers
{
    /// <summary>
    /// The features controller.
    /// </summary>
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService featureService;


        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturesController"/> class.
        /// </summary>
        /// <param name="featuresService">The features service.</param>
        public FeaturesController(IFeatureService featuresService)
        {
            this.featureService = featuresService;
        }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("GetFeatures")]
        [ProducesResponseType(typeof(ApiResponseBase<List<Feature>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseBase<List<Feature>>), StatusCodes.Status404NotFound)]
        public IActionResult GetFeatures()
        {
            var data = featureService.GetAll();
            return this.Result(StatusCodes.Status200OK, data, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Gets the features by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("GetFeaturesByExternalId")]
        [ProducesResponseType(typeof(ApiResponseBase<List<Feature>>), StatusCodes.Status200OK)]

        public IActionResult GetFeaturesByExternalId([FromQuery] int id, [FromQuery] string type)
        {
            var data = featureService.GetFeaturesByExternalId(id, type);
            return this.Result(StatusCodes.Status200OK, data, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Assigns the features with external id.
        /// </summary>
        /// <param name="featureAssignmentPayload">The feature assignment payload.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        [Route("AssignFeaturesWithExternalId")]
        [ProducesResponseType(typeof(ApiResponseBase<Data.Models.FeatureAssignmentPayload>), StatusCodes.Status200OK)]

        public async Task<IActionResult> AssignFeaturesWithExternalId([FromBody] FeatureAssignmentPayload featureAssignmentPayload)
        {
            var res = await featureService.AssignFeaturesWithExternalId(featureAssignmentPayload);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }
    }
}
