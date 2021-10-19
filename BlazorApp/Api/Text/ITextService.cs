using System.Threading.Tasks;

namespace BlazorApp.Api.Text
{
    internal interface ITextService
    {
        Task<string?> SummerizeText(string text);

    }
}