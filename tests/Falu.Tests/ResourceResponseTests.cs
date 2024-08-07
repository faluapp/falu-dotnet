﻿using Falu.Core;
using System.Net;
using Xunit;

namespace Falu.Tests;

public class ResourceResponseTests
{
    [Fact]
    public void GetHeader_Works()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000"); // correct case
        headers.Add("X-TRACE-ID", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00"); // uppercase
        headers.Add("x-continuation-token", "123"); // lowercase

        // Act
        var continuationToken = ResourceResponse<object>.GetHeader(response.Headers, HeadersNames.XContinuationToken);
        var requestId = ResourceResponse<object>.GetHeader(response.Headers, HeadersNames.XRequestId);
        var traceId = ResourceResponse<object>.GetHeader(response.Headers, HeadersNames.XTraceId);

        // Assert
        Assert.Equal("req_000000000000000000000000", requestId);
        Assert.Equal("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00", traceId);
        Assert.Equal("123", continuationToken);
    }

    [Fact]
    public void RelevantHeadersAreExtracted()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000");
        headers.Add("X-Trace-Id", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00");
        headers.Add("X-Continuation-Token", "123");

        // Act
        var rr = new ResourceResponse<object>(response, new { }, null);

        // Assert
        Assert.Equal("req_000000000000000000000000", rr.RequestId);
        Assert.Equal("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00", rr.TraceId);
        Assert.Equal("123", rr.ContinuationToken);
        Assert.Null(rr.CachedResponse);
    }

    [Fact]
    public void RelevantHeadersAreExtracted_WithCached()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000");
        headers.Add("X-Trace-Id", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00");
        headers.Add("X-Continuation-Token", "123");
        headers.Add("X-Cached-Response", "true");

        // Act
        var rr = new ResourceResponse<object>(response, new { }, null);

        // Assert
        Assert.Equal("req_000000000000000000000000", rr.RequestId);
        Assert.Equal("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00", rr.TraceId);
        Assert.Equal("123", rr.ContinuationToken);
        Assert.True(rr.CachedResponse);
    }

    [Theory]
    [InlineData(HttpStatusCode.OK, true)] // 200
    [InlineData(HttpStatusCode.Created, true)] // 201
    [InlineData(HttpStatusCode.NoContent, true)] // 204
    [InlineData(HttpStatusCode.BadRequest, false)] // 400
    [InlineData(HttpStatusCode.NotFound, false)] // 404
    [InlineData(HttpStatusCode.UnsupportedMediaType, false)] // 415
    [InlineData(HttpStatusCode.InternalServerError, false)] // 500
    [InlineData(HttpStatusCode.BadGateway, false)] // 502
    public void IsSuccessful_Works(HttpStatusCode code, bool expected)
    {
        // Prepare
        var response = new HttpResponseMessage(code);

        // Act
        var rr = new ResourceResponse<object>(response, new { }, null);

        // Assert
        Assert.Equal(expected, rr.IsSuccessful);
    }

    [Fact]
    public void HasMoreResults_Returns_Null_NotEnumerable()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Headers.Add("X-Continuation-Token", "123");

        // Act
        var rr = new ResourceResponse<object>(response, new { }, null);

        // Assert
        Assert.Equal("123", rr.ContinuationToken);
        Assert.Null(rr.HasMoreResults);
    }

    [Fact]
    public void HasMoreResults_Returns_False_NoContinuationToken()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        // Act
        var rr = new ResourceResponse<object[]>(response, [], null);

        // Assert
        Assert.False(rr.HasMoreResults);
    }

    [Fact]
    public void HasMoreResults_Returns_True()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Headers.Add("X-Continuation-Token", "123");

        // Act
        var rr = new ResourceResponse<object[]>(response, [], null);

        // Assert
        Assert.Equal("123", rr.ContinuationToken);
        Assert.True(rr.HasMoreResults);
    }

    [Fact]
    public void EnsureSuccess_DoesNotThrowException()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000");
        headers.Add("X-Trace-Id", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00");
        headers.Add("X-Continuation-Token", "123");
        headers.Add("X-Cached-Response", "true");

        // Act
        var rr = new ResourceResponse<object>(response, new { }, null);

        // Assert
        rr.EnsureSuccess();
    }

    [Fact]
    public void EnsureSuccess_ThrowsException_1()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000");
        headers.Add("X-Trace-Id", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00");
        headers.Add("X-Continuation-Token", "123");
        headers.Add("X-Cached-Response", "true");

        // Act
        var rr = new ResourceResponse<object>(response, new { }, null);

        // Assert
        var ex = Assert.Throws<FaluException>(rr.EnsureSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
        Assert.Equal(response, ex.Response);
        Assert.Equal("req_000000000000000000000000", ex.RequestId);
        Assert.Equal("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00", ex.TraceId);
        Assert.Null(ex.Error);
        var msg = "Request failed - BadRequest (400)\r\n"
                + "StatusCode: 400 (BadRequest)\r\n"
                + "RequestId: req_000000000000000000000000\r\n"
                + "TraceId: 00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00";
        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void EnsureSuccess_ThrowsException_2()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000");
        headers.Add("X-Trace-Id", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00");
        headers.Add("X-Continuation-Token", "123");
        headers.Add("X-Cached-Response", "true");

        // Act
        var error = new FaluError
        {
            Title = "One or more validation errors",
            Detail = null,
            Errors = new Dictionary<string, string[]>
            {
                ["[137].To"] = ["The To field is required."],
                ["[268].To"] = ["The To field is required."],
            },
        };
        var rr = new ResourceResponse<object>(response, new { }, error);

        // Assert
        var ex = Assert.Throws<FaluException>(rr.EnsureSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
        Assert.Equal(response, ex.Response);
        Assert.Equal("req_000000000000000000000000", ex.RequestId);
        Assert.Equal("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00", ex.TraceId);
        Assert.Equal(error, ex.Error);
        var msg = "One or more validation errors\r\n"
                + "StatusCode: 400 (BadRequest)\r\n"
                + "RequestId: req_000000000000000000000000\r\n"
                + "TraceId: 00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00\r\n"
                + "Error: One or more validation errors";
        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void EnsureSuccess_ThrowsException_3()
    {
        // Prepare
        var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        var headers = response.Headers;
        headers.Add("X-Request-Id", "req_000000000000000000000000");
        headers.Add("X-Trace-Id", "00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00");
        headers.Add("X-Continuation-Token", "123");
        headers.Add("X-Cached-Response", "true");

        // Act
        var error = new FaluError
        {
            Title = "stream_not_found",
            Detail = "The target message stream could not be found.",
        };
        var rr = new ResourceResponse<object>(response, new { }, error);

        // Assert
        var ex = Assert.Throws<FaluException>(rr.EnsureSuccess);
        Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);
        Assert.Equal(response, ex.Response);
        Assert.Equal("req_000000000000000000000000", ex.RequestId);
        Assert.Equal("00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00", ex.TraceId);
        Assert.Equal(error, ex.Error);
        var msg = "The target message stream could not be found.\r\n"
                + "StatusCode: 400 (BadRequest)\r\n"
                + "RequestId: req_000000000000000000000000\r\n"
                + "TraceId: 00-982607166a542147b435be3a847ddd71-fc75498eb9f09d48-00\r\n"
                + "Error: stream_not_found\r\n"
                + "Message: The target message stream could not be found.";
        Assert.Equal(msg, ex.Message);
    }
}
