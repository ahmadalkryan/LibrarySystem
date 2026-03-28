//using Application.IService;
//using Azure;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Infrastructure.Service
//{
//    public class AIService : IAIService
//    {

//        private readonly HttpClient _httpClient;
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<AIService> _logger;


//        public AIService(HttpClient httpClient, ILogger<AIService> logger,
//             IConfiguration configuration)
//        {
//            _httpClient = httpClient;
//            _configuration = configuration;
//            _logger = logger;
//        }


//        public async Task<string> GetAIresponseAsync(string prompt)
//        {

//            var apiKey = _configuration["OpenRouter:ApiKey"];
//            var apiUrl = _configuration["OpenRouter:ApiUrl"] ?? "https://openrouter.ai/api/v1/chat/completions";

//            try
//            {
//                //var jsonBody = new
//                //{
//                //    model = "mistralai/mistral-7b-instruct:free"
//                //    ,
//                //    messages = new[]
//                //    {
//                //        new {role ="system" ,  content=" أنت أمين مكتبة خبير. لديك معرفة واسعة بجميع الكتب العربية والعالم."},
//                //          new { role = "user", content = $"انا موظف بالمكتبة امين مكتبة اريد جواب صحيح وواضح {prompt} . " }
//                //    },

//                //         temperature = 0.3,
//                //    max_tokens = 800
//                //    ,



//                //};

//                var requestBody = new
//                {
//                    model = "mistralai/mistral-7b-instruct:free",
//                    messages = new[]
//                   {
//                        new {
//                            role = "system",
//                            content = @"أنت أمين مكتبة خبير في المكتبة الوطنية. 
//                                      لديك معرفة واسعة بجميع الكتب العربية والعالمية. 
//                                      أجب بدقة ووضوح، وركز على المعلومات المفيدة للمكتبة."
//                        },
//                        new {
//                            role = "user",
//                            content = prompt
//                        }
//                    },
//                    temperature = 0.3,
//                    max_tokens = 800,

//                };
//                _logger.LogInformation($"Sending message to AI: {prompt}");


//                //             var requestContent = new StringContent(
//                //                                      JsonSerializer.Serialize(jsonBody),
//                //                                                                        Encoding.UTF8,
//                //                                                                                       "application/json"
//                //);   

//                var jsonBody = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
//                {
//                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//                });

//                using var requestContent = new StringContent(
//                    jsonBody,
//                    Encoding.UTF8,
//                    "application/json"
//                );

//                _httpClient.DefaultRequestHeaders.Clear();
//                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

//                var response = await _httpClient.PostAsync(apiUrl, requestContent);

//                // استخدام await بشكل صحيح
//                if (response.IsSuccessStatusCode)
//                {
//                    var responseContent = await response.Content.ReadAsStringAsync();

//                    // استخراج المحتوى مباشرة باستخدام JsonDocument
//                    using var doc = JsonDocument.Parse(responseContent);
//                    var root = doc.RootElement;


//                    var content = root
//                        .GetProperty("choices")[0]
//                        .GetProperty("message")
//                        .GetProperty("content")
//                        .GetString();

//                    return content;
//                }
//                else
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    _logger.LogError($"AI Service Error: {response.StatusCode} - {errorContent}");
//                    return $"Error: Failed to get response (HTTP {response.StatusCode})";
//                }




//            }

//            catch (Exception ex)
//            {
//                _logger.LogError($"Exception in AI Service: {ex.Message}");
//                return $"Error: {ex.Message}";
//            }

//        }
//    }
//}
using Application.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AIService> _logger;
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public AIService(HttpClient httpClient, ILogger<AIService> logger,
             IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _apiKey = _configuration["OpenRouter:ApiKey"];
            _apiUrl = "https://openrouter.ai/api/v1/chat/completions"; // ثابت من الدوكيومنتيشن
        }

        public async Task<string> GetAIresponseAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                _logger.LogError("OpenRouter API Key is missing");
                return "خطأ: مفتاح API غير موجود. يرجى التحقق من الإعدادات.";
            }

            if (string.IsNullOrWhiteSpace(prompt))
            {
                return "الرجاء إدخال سؤال صحيح.";
            }

            try
            {
                // ✅ استخدام exact model name من الدوكيومنتيشن
                var requestBody = new
                {
                    model = "mistralai/mistral-7b-instruct-v0.1", // نفس الاسم في الدوكيومنتيشن
                    messages = new[]
                    {
                        new {
                            role = "system",
                            content = "أنت أمين مكتبة خبير في المكتبة الوطنية. لديك معرفة واسعة بجميع الكتب العربية والعالمية."
                        },
                        new {
                            role = "user",
                            content = prompt
                        }
                    }
                    // ملاحظة: temperature و max_tokens اختيارية، يمكن إضافتها إذا أردت
                };

                var jsonBody = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                _logger.LogInformation($"Sending to OpenRouter. URL: {_apiUrl}");
                _logger.LogDebug($"Request body: {jsonBody}");

                // ✅ إنشاء HttpRequestMessage جديد (أنظف من DefaultRequestHeaders)
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, _apiUrl);

                // ✅ إضافة جميع الـ headers المطلوبة
                requestMessage.Headers.Add("Authorization", $"Bearer {_apiKey}");
                requestMessage.Headers.Add("HTTP-Referer", "https://localhost:7060"); // مهم!
                requestMessage.Headers.Add("X-Title", "Library AI Assistant"); // مهم!

                requestMessage.Content = new StringContent(
                    jsonBody,
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.SendAsync(requestMessage);

                _logger.LogInformation($"Response Status: {(int)response.StatusCode} {response.StatusCode}");

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Response content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return ExtractContentFromResponse(responseContent);
                }
                else
                {
                    _logger.LogError($"OpenRouter Error: {response.StatusCode}");
                    _logger.LogError($"Error Details: {responseContent}");

                    return response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.Unauthorized =>
                            "خطأ في المصادقة: مفتاح API غير صالح",
                        System.Net.HttpStatusCode.BadRequest =>
                            $"خطأ في الطلب: {responseContent}",
                        System.Net.HttpStatusCode.TooManyRequests =>
                            "تم تجاوز حد الطلبات المسموح به. حاول لاحقاً.",
                        System.Net.HttpStatusCode.NotFound =>
                            "الخدمة غير متوفرة. تحقق من الرابط.",
                        _ => $"عذراً، تعذر الاتصال بالمساعد الذكي. (خطأ: {(int)response.StatusCode})"
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error connecting to OpenRouter");
                return "خطأ في الاتصال بالإنترنت. تحقق من اتصالك وحاول مجدداً.";
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Request timeout");
                return "انتهت مهلة الطلب. الخدمة بطيئة حالياً، حاول لاحقاً.";
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error");
                return "خطأ في معالجة رد المساعد الذكي.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in AI Service");
                return $"حدث خطأ غير متوقع: {ex.Message}";
            }
        }

        private string ExtractContentFromResponse(string jsonResponse)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonResponse);
                var root = doc.RootElement;

                if (root.TryGetProperty("choices", out var choices) &&
                    choices.GetArrayLength() > 0)
                {
                    var firstChoice = choices[0];

                    if (firstChoice.TryGetProperty("message", out var message) &&
                        message.TryGetProperty("content", out var content))
                    {
                        var result = content.GetString();
                        return result ?? "لم يتم الحصول على رد.";
                    }
                }

                _logger.LogWarning("Unexpected JSON structure from OpenRouter");
                return "لم يتمكن المساعد الذكي من صياغة رد مناسب.";
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to parse OpenRouter response");
                return "خطأ في تحليل رد المساعد الذكي.";
            }
        }

        // دالة اختبار
        //public async Task<string> TestConnectionAsync()
        //{
        //    try
        //    {
        //        var testRequest = new HttpRequestMessage(HttpMethod.Get, "https://openrouter.ai/api/v1/auth/key");
        //        testRequest.Headers.Add("Authorization", $"Bearer {_apiKey}");

        //        var response = await _httpClient.SendAsync(testRequest);
        //        var content = await response.Content.ReadAsStringAsync();

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return $"✅ متصل - الحساب مجاني ويعمل";
        //        }
        //        else
        //        {
        //            return $"❌ خطأ: {response.StatusCode} - {content}";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"❌ استثناء: {ex.Message}";
        //    }
        //}
    }
}
