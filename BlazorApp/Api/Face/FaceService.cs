using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Data.Face;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace BlazorApp.Api.Face
{
    internal class FaceService : IFaceService
    {
        private readonly IFaceClient _client;

        internal FaceService(string apiEndpoint, string subscriptionKey)
        {
            _client = new FaceClient(new ApiKeyServiceClientCredentials(subscriptionKey)) {Endpoint = apiEndpoint};
        }

        async Task<List<Result>> IFaceService.AnalyseFaces(string recognitionModel, IBrowserFile? imageFile)
        {
            var faceResults = await _client.Face.DetectWithStreamAsync(imageFile?.OpenReadStream(),
                returnFaceAttributes: new List<FaceAttributeType>

                {
                    FaceAttributeType.Age,
                    FaceAttributeType.Emotion,
                    FaceAttributeType.Gender,
                },
                detectionModel: DetectionModel.Detection01,
                recognitionModel: recognitionModel);


            var resultList = new List<Result>();

            foreach (var face in faceResults)
            {
                resultList.Add(
                    new Result(
                        face.FaceAttributes.Gender,
                        face.FaceAttributes.Age, face.FaceAttributes.Emotion.ToRankedList().First(),
                        new Rectangle(face.FaceRectangle.Left, face.FaceRectangle.Top, face.FaceRectangle.Width,
                            face.FaceRectangle.Height)
                    )
                );
            }

            return resultList;
        }
    }
}