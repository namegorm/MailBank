using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

using Products.Application.ViewModels;

namespace Products.API.Formatters.Input
{
    public class ProductTextInputFormatter : TextInputFormatter
    {
        public ProductTextInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/product"));
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            string data = null;
            using (var streamReader = new StreamReader(context.HttpContext.Request.Body, encoding))
            {
                data = await streamReader.ReadToEndAsync();
            }

            var bodyProperties = data?.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var bodyPropertiesDictionary = bodyProperties.Select(x => x.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Count() == 2).ToDictionary(x => x.First(), y => y.Last());

            var productViewModel = new ProductViewModel();
            var productViewModelProperties = productViewModel.GetType().GetProperties();

            foreach (var productViewModelProperty in productViewModelProperties)
            {
                if (bodyPropertiesDictionary.ContainsKey(productViewModelProperty.Name))
                {
                    var converter = TypeDescriptor.GetConverter(productViewModelProperty.PropertyType);
                    var bodyPropertyValue = converter.ConvertFromString(bodyPropertiesDictionary[productViewModelProperty.Name]);
                    productViewModelProperty.SetValue(productViewModel, bodyPropertyValue);
                }
            }

            return await InputFormatterResult.SuccessAsync(productViewModel);
        }
    }
}
