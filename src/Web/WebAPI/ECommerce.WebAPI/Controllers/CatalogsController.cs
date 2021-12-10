﻿using System.Threading;
using System.Threading.Tasks;
using ECommerce.Contracts.Catalog;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
public class CatalogsController : ApplicationController
{
    public CatalogsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetCatalogItemsWithPagination([FromQuery] Queries.GetCatalogItemsDetailsWithPagination query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCatalogItemsDetailsWithPagination, Responses.CatalogItemsDetailsPagedResult>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateCatalog(Commands.CreateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> AddCatalogItem(Commands.AddCatalogItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RemoveCatalogItem(Commands.RemoveCatalogItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> ActivateCatalog(Commands.ActivateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> DeactivateCatalog(Commands.DeactivateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateCatalog(Commands.UpdateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateCatalogItem(Commands.UpdateCatalogItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete]
    public Task<IActionResult> DeleteCatalog(Commands.DeleteCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}