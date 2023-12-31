﻿using System.Text.RegularExpressions;

namespace Mensajeria_Linux.Infrastructure.Routing
{
    /// <summary>
    /// Transforma Url para que sea más amigable con el usuario
    /// </summary>
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound (object? value)
        {
            // Slugify value
            //From https://stackoverflow.com/questions/40334515/automatically-generate-lowercase-dashed-routes-in-asp-net-core
            return value == null ? null : Regex.Replace(value.ToString(),
                "([a-z])([A-Z])", "$1-$2", RegexOptions.None, TimeSpan.FromMilliseconds(100)).ToLower();
        }
    }
}
