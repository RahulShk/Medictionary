using Medictionary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using payment_gateway_nepal;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Medictionary.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string _khaltiKey;
        private readonly string _eSewaKey;
        private readonly bool _sandBoxMode;

        public PaymentController(IConfiguration config)
        {
            _sandBoxMode = config.GetValue<bool>("PaymentSettings:UseSandbox");
            _khaltiKey = _sandBoxMode 
                ? config["PaymentSettings:Khalti_SandboxKey"] ?? string.Empty
                : config["PaymentSettings:Khalti_ProductionKey"] ?? string.Empty;
            _eSewaKey = _sandBoxMode 
                ? config["PaymentSettings:eSewa_SandboxKey"] ?? string.Empty
                : config["PaymentSettings:eSewa_ProductionKey"] ?? string.Empty;
        }

        public async Task<IActionResult> PayWitheSewa(int cartSubtotal, int cartTax, int cartTotal, string productDetails)
        {
            try
            {
                PaymentManager paymentManager = new PaymentManager(
                    PaymentMethod.eSewa,
                    PaymentVersion.v2,
                    _sandBoxMode ? PaymentMode.Sandbox : PaymentMode.Production,
                    _eSewaKey
                );
                string currentUrl = new Uri($"{Request.Scheme}://{Request.Host}").AbsoluteUri;

                dynamic request = new
                {
                    Amount = 10,
                    TaxAmount = 10,
                    TotalAmount = 20,
                    TransactionUuid = $"tx-{Guid.NewGuid().ToString("N").Substring(0, 8)}",
                    ProductCode = _sandBoxMode ? "EPAYTEST" : "YOUR_PRODUCT_CODE",
                    ProductServiceCharge = 0,
                    ProductDeliveryCharge = 0,
                    SuccessUrl = $"{currentUrl}Payment/Success",
                    FailureUrl = $"{currentUrl}Payment/Failure",
                    // ProductDetails = productDetails,
                    SignedFieldNames = "total_amount,transaction_uuid,product_code,product_details"
                };

                Debug.WriteLine($"eSewa Request: {System.Text.Json.JsonSerializer.Serialize(request)}");
                var response = await paymentManager.InitiatePaymentAsync<ApiResponse>(request);
                return Redirect(response.data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initiating eSewa payment: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                return BadRequest("Failed to initiate payment. Please try again.");
            }
        }

        [HttpGet]
        [Route("Payment/Success")]
        public async Task<IActionResult> VerifyEsewaPayment(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                ViewBag.Message = "Payment verification failed: No data received.";
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            try
            {
                PaymentManager paymentManager = new PaymentManager(
                    PaymentMethod.eSewa,
                    PaymentVersion.v2,
                    _sandBoxMode ? PaymentMode.Sandbox : PaymentMode.Production,
                    _eSewaKey
                );

                // Log the received data for debugging
                Console.WriteLine($"Received eSewa response data: {data}");

                eSewaResponse response = await paymentManager.VerifyPaymentAsync<eSewaResponse>(data);

                if (response != null && string.Equals(response.status, "complete", StringComparison.OrdinalIgnoreCase))
                {
                    ViewBag.Message = $"Payment successful: {response.transaction_code}, Amount: {response.total_amount}";
                    return View("Success");
                }
                else
                {
                    ViewBag.Message = "Payment verification failed.";
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Payment verification error: {ex.Message}");
                ViewBag.Message = "An error occurred while verifying the payment.";
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}