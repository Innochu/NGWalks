using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace NGWalksCommon
{
	public class ExceptionHandling
	{
		private readonly ILogger<ExceptionHandling> _logger;
		private readonly RequestDelegate _next;

		public ExceptionHandling(ILogger<ExceptionHandling> logger, RequestDelegate next)

		{
			_logger = logger;
			_next = next;
		}


		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				var errorId = Guid.NewGuid();

				// Log This Exception
				_logger.LogError(ex, $"{errorId} : {ex.Message}");

				// Return A Custom Exrror Response
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var error = new
				{
					Id = errorId,
					ErrorMessage = "Something went wrong! We are looking into resolving this."
				};

				await httpContext.Response.WriteAsJsonAsync(error);
			}
		}

	}
}
