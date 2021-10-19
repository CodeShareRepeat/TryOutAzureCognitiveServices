using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;

namespace BlazorApp.Api.Text
{
    public class TextService : ITextService
    {
        
        private readonly TextAnalyticsClient _client;
        
        internal TextService(string apiEndpoint, string subscriptionKey)
        {
            _client = new TextAnalyticsClient(new Uri(apiEndpoint), new AzureKeyCredential(subscriptionKey));
        }


        public async Task<string?> SummerizeText(string text)
        {
            var resultStringBuilder = new StringBuilder();
            
            var batchInput = new List<string>
            {
                text
            };

            var actions = new TextAnalyticsActions()
            {
                ExtractSummaryActions = new List<ExtractSummaryAction>() { new ExtractSummaryAction() }
            };

            var textAnalysisOperation = await _client.StartAnalyzeActionsAsync(batchInput, actions);
            await textAnalysisOperation.WaitForCompletionAsync();

            
            
            // View operation results.
            await foreach (AnalyzeActionsResult documentsInPage in textAnalysisOperation.Value)
            {
                IReadOnlyCollection<ExtractSummaryActionResult> summaryResults = documentsInPage.ExtractSummaryResults;

                foreach (ExtractSummaryActionResult summaryActionResults in summaryResults)
                {
                    if (summaryActionResults.HasError)
                    {
                        // do nothing with error...
                        continue;
                    }
                    
                    var results =
                        summaryActionResults.DocumentsResults
                            .Where(result => result.HasError == false && result.Sentences.Any())
                            .SelectMany((s) => s.Sentences);

                    foreach (var result in results)
                    {
                        resultStringBuilder.AppendLine(result.Text);
                    }

                }
            }
            
            return resultStringBuilder.ToString();
     
        }
    }
}