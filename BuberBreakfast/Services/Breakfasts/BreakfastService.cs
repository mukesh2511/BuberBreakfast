using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;
using System;
using System.Collections.Generic;


namespace BuberBreakfast.Services.Breakfasts
{
    public class BreakfastService : IBreakfastService
    {
        private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

        public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
        {
            _breakfasts.Add(breakfast.Id, breakfast);
            return Result.Created;
        }

        public ErrorOr<Breakfast> GetBreakfast(Guid id)
        {
            if (_breakfasts.TryGetValue(id, out var breakfast))
            {
                return breakfast;
            }
            return Errors.Breakfast.NotFound;
        }

        public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
        {
            var IsNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);

            _breakfasts[breakfast.Id] = breakfast;
            return new UpsertedBreakfast(IsNewlyCreated);
        }

        public ErrorOr<Deleted> DeleteBreakfast(Guid id)
        {
            _breakfasts.Remove(id);
            return Result.Deleted;
        }
    }
}
