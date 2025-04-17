using System.Reflection;

namespace AvailabilityService.Services
{
    public class AvailabilityMappingService
    {
        public TTarget Map<TTarget>(object input) where TTarget : new()
        {
            ArgumentNullException.ThrowIfNull(input);

            var target = new TTarget();
            var inputProps = input.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var targetProp in targetProps)
            {
                var inputProp = inputProps.FirstOrDefault(p => p.Name == targetProp.Name &&
                                                               p.PropertyType == targetProp.PropertyType &&
                                                               p.CanRead && targetProp.CanWrite);
                if (inputProp != null)
                {
                    var value = inputProp.GetValue(input);
                    targetProp.SetValue(target, value);
                }
            }

            return target;
        }
    }
}
