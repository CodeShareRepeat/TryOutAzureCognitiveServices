using System.Collections.Generic;
using System.Drawing;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace BlazorApp.Data.Face
{
    public readonly record struct Result(Gender? Gender, double? Age, KeyValuePair<string, double> DominantEmotion, Rectangle FaceRectangle);
}