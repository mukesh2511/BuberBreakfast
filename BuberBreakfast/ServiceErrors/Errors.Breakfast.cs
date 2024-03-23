using System.ComponentModel;
using ErrorOr;
namespace BuberBreakfast.ServiceErrors;


public static class Errors{
    public static class Breakfast{
        public static Error NotFound=>Error.NotFound(
            code:"BreakFast Not Found!", 
            description:"Breakfast Not found"
        );
    }
}