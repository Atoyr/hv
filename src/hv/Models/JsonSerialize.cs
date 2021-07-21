using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hv.Models
{
    public static class JsonSerialize
    {
        public static string ToJson(this VirtualMachine[] self) => JsonConvert.SerializeObject(self, hv.Models.Converter.VirtualMachneSettings);
    }
}

