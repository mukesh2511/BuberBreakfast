
using BuberBreakfast.Contracts.BuberBreakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastsControllers : ApiController
{
    private readonly IBreakfastService _breakfastService;
    public BreakfastsControllers(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
           Guid.NewGuid(),
           request.Name,
           request.Description,
           request.StartDateTime,
           request.EndDateTime,
           DateTime.UtcNow,
           request.Savory,
           request.Sweet
       );

        ///save breakfast to database
        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);
        return createBreakfastResult.Match(
         created => CreatedAsGetBreakfast(breakfast),
         errors => Problem(errors)
        );

        // return CreatedAtAction(
        //     actionName: nameof(GetBreakfast),
        //     routeValues: new { id = breakfast.Id },
        //     value: MapBreakfastResponse(breakfast)
        // );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {

        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);
        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );
    }
    private static CreateBreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new CreateBreakfastResponse(
            breakfast.Id,
        breakfast.Name,
        breakfast.Description,
        breakfast.StartDateTime,
        breakfast.EndDateTime,
        breakfast.LastModifiedDateTime,
        breakfast.Savory,
        breakfast.Sweet
        );
    }
    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
        return upsertBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAsGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors));

    }
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedBreakfastResult = _breakfastService.DeleteBreakfast(id);

        return deletedBreakfastResult.Match(deleted => NoContent(), errors => Problem(errors));
    }

    private CreatedAtActionResult CreatedAsGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
           value: MapBreakfastResponse(breakfast)
        );
    }
}