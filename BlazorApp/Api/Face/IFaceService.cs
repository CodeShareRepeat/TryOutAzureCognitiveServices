using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Data.Face;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Api.Face
{
    internal interface IFaceService
    { 
        Task<List<Result>> AnalyseFaces(string recognitionModel, IBrowserFile? imageFile);
    }
}