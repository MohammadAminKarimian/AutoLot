namespace AutoLot.Api.Filters;

public class LoggingResponseHeaderFilter : IResultFilter
{
    private readonly IAppLogging<LoggingResponseHeaderFilter> _logger;

    public LoggingResponseHeaderFilter(IAppLogging<LoggingResponseHeaderFilter> logger) =>
        _logger = logger;

    public void OnResultExecuting(ResultExecutingContext context)
    {
        _logger.LogAppInformation($"LogAppInformation: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuting)}");
        _logger.LogAppDebug($"LogAppDebug: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuting)}");
        _logger.LogAppWarning($"LogAppWarning: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuting)}");
        _logger.LogAppTrace($"LogAppTrace: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuting)}");
        _logger.LogAppError($"LogAppError: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuting)}");

        context.HttpContext.Response.Headers.Add(
            nameof(OnResultExecuting), nameof(LoggingResponseHeaderFilter));
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        _logger.LogAppInformation($"LogAppInformation: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuted)}");
        _logger.LogAppDebug($"LogAppDebug: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuted)}");
        _logger.LogAppWarning($"LogAppWarning: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuted)}");
        _logger.LogAppTrace($"LogAppTrace: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuted)}");
        _logger.LogAppError($"LogAppError: {nameof(LoggingResponseHeaderFilter)}.{nameof(OnResultExecuted)}");
    }
}
